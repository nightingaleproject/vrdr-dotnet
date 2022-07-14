# Pull and process messages from the NVSS API; takes as arguments the jurisdiction to check for (e.g., MA) and
# an optional number of hours to use with the _since parameter; saves incoming messages to files and sends
# acks as appropriate (saving those to files as well).

require 'oauth2'
require 'active_support/time'
require 'tempfile'

jurisdiction = ARGV.shift
if jurisdiction.nil? || !jurisdiction.match(/^[A-Z][A-Z]$/)
  puts "You must provide the jurisdiction code (e.g., MA) as the first argument"
  exit
end

if ARGV.length > 0
  hours = ARGV.shift.to_i
  last_updated = DateTime.now - hours.hours
end

client_id = File.read('clientid.txt')
client_secret = File.read('clientsecret.txt')
username = File.read('username.txt')
password = File.read('password.txt')

client = OAuth2::Client.new(client_id,
                            client_secret,
                            site: 'https://apigw.cdc.gov/',
                            token_url: '/auth/oauth/v2/token')

token = client.password.get_token(username, password)

request = if last_updated
            "/OSELS/NCHS/NVSSFHIRAPI/#{jurisdiction}/Bundles?_since=#{last_updated.to_s}"
          else
            "/OSELS/NCHS/NVSSFHIRAPI/#{jurisdiction}/Bundles"
          end

response = token.get(request)

searchset = JSON.parse(response.body)

if searchset['entry'].nil?
  puts "No messages found"
  exit
end

searchset['entry'].each do |entry|
  header = nil
  certno = nil
  begin
    resource = entry['resource']
    if resource['type'] != 'message'
      puts "Found a resource of type #{resource['type']}, skipping"
      next
    end
    header = resource['entry'][0]['resource']
    if header['resourceType'] != 'MessageHeader'
      puts "Found a message with a first entry of type #{header['resourceType']}, skipping"
      next
    end
    params = resource['entry'][1]['resource'] rescue nil
    certno = params['parameter'].detect { |p| p['name'] == 'cert_no' }&.[]('valueUnsignedInt') rescue 'UNKNOWN'
    case header['eventUri']
    when 'http://nchs.cdc.gov/vrdr_acknowledgement'
      puts "Found an acknowledgement message for message #{header['response']['identifier']} for certificate #{certno}, skipping"
      File.write("submission_acknowledgement_message_#{certno}_example.json", resource.to_json)
    when 'http://nchs.cdc.gov/vrdr_extraction_error'
      puts "Found an extraction error message for message #{header['response']['identifier']} for certificate #{certno}, skipping"
      File.write("extraction_error_message_#{certno}_example.json", resource.to_json)
    when 'http://nchs.cdc.gov/vrdr_causeofdeath_coding', 'http://nchs.cdc.gov/vrdr_demographics_coding'
      id = header['id']
      puts "Found a coding response message of type #{header['eventUri']} with ID #{id} for certificate #{certno}, acknowledging"
      File.write("#{header['eventUri'] == 'http://nchs.cdc.gov/vrdr_causeofdeath_coding' ? 'cause_of_death' : 'demographics'}_coding_response_message_#{certno}_example.json", resource.to_json)
      Tempfile.create do |f|
        f << resource.to_json
        f.flush
        ack = `dotnet run --project /Users/krautscheid/git/vrdr-dotnet/VRDR.CLI ack #{f.path}`
        File.write("#{header['eventUri'] == 'http://nchs.cdc.gov/vrdr_causeofdeath_coding' ? 'cause_of_death' : 'demographics'}_acknowledgement_message_#{certno}_example.json", ack)
        response = token.post("/OSELS/NCHS/NVSSFHIRAPI/#{jurisdiction}/Bundles",
                              headers: { 'Content-Type' => 'application/json' },
                              body: ack)
        puts "Server response: #{response.status}"
      end
    else
      puts "Found a message of type #{header['eventUri']} for certificate #{certno}, skipping"
    end
  rescue StandardError => e
    if header && certno
      puts "Error processing message of type #{header['eventUri']} for certificate #{certno}: #{e}"
    else
      puts "Error processing message: #{e}"
    end
  end
end
