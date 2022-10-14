# Pull and process messages from the NVSS API; takes as arguments the jurisdiction to check for (e.g., MA) and
# an optional number of hours to use with the _since parameter; saves incoming messages to files and sends
# acks as appropriate (saving those to files as well).

require 'yaml'
require 'oauth2'
require 'active_support/time'
require 'parallel'

CLI_PATH = File.join(__dir__, '..', '..', 'VRDR.CLI', 'bin', 'Debug', 'netcoreapp6.0', 'DeathRecord.CLI.dll')
if (!File.exists?(CLI_PATH))
  puts "Cannot find the CLI application at #{CLI_PATH}, you may need to build it"
  exit
end

CONFIG_PATH = File.join(__dir__, 'config.yml')
if (!File.exists?(CONFIG_PATH))
  puts "Cannot find the config file at #{CONFIG_PATH}, you may need to create it"
  puts "It should look something like this (including the ---):"
  puts ['client_id', 'client_secret', 'username', 'password'].inject({}) { |h, k| h[k] = k ; h }.to_yaml
  exit
end

credentials = YAML.load(File.read(CONFIG_PATH))

jurisdiction = ARGV.shift
if jurisdiction.nil? || !jurisdiction.match(/^[A-Z][A-Z]$/)
  puts "You must provide the jurisdiction code (e.g., MA) as the first argument"
  exit
end

if ARGV.length > 0
  hours = ARGV.shift.to_i
  last_updated = DateTime.now - hours.hours
end

client = OAuth2::Client.new(credentials['client_id'],
                            credentials['client_secret'],
                            site: 'https://apigw.cdc.gov/',
                            token_url: '/auth/oauth/v2/token')

token = client.password.get_token(credentials['username'], credentials['password'])

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

Parallel.each(searchset['entry']) do |entry|
  header = nil
  identifier = nil
  begin
    message = entry['resource']
    if message['type'] != 'message'
      puts "Found a resource of type #{message['type']}, skipping"
      next
    end
    header = message['entry'][0]['resource']
    if header['resourceType'] != 'MessageHeader'
      puts "Found a message with a first entry of type #{header['resourceType']}, skipping"
      next
    end
    params = message['entry'][1]['resource'] rescue nil
    cert_no = params['parameter'].detect { |p| p['name'] == 'cert_no' }&.[]('valueUnsignedInt') rescue 0
    jurisdiction_id = params['parameter'].detect { |p| p['name'] == 'jurisdiction_id' }&.[]('valueString') rescue 'XX'
    death_year = params['parameter'].detect { |p| p['name'] == 'death_year' }&.[]('valueUnsignedInt') rescue 0
    identifier = "#{'%04d' % death_year}#{jurisdiction_id}#{'%06d' % cert_no}"
    case header['eventUri']
    when 'http://nchs.cdc.gov/vrdr_acknowledgement'
      puts "Found an acknowledgement message for message #{header['response']['identifier']} for certificate #{identifier}"
      File.write("#{identifier}_submission_acknowledgement.json", message.to_json)
    when 'http://nchs.cdc.gov/vrdr_status'
      puts "Found a status message for message #{header['response']['identifier']} for certificate #{identifier}"
      File.write("#{identifier}_status.json", message.to_json)
    when 'http://nchs.cdc.gov/vrdr_extraction_error'
      puts "Found an extraction error message for message #{header['response']['identifier']} for certificate #{identifier}, acknowledging"
      message_filename = "#{identifier}_extraction_error.json"
      File.write(message_filename, message.to_json)
      ack = `dotnet #{CLI_PATH} ack #{message_filename}`
      File.write("#{identifier}_extraction_error_acknowledgement.json", ack)
      response = token.post("/OSELS/NCHS/NVSSFHIRAPI/#{jurisdiction}/Bundles",
                            headers: { 'Content-Type' => 'application/json' },
                            body: ack)
      puts "Server response acknowledging error message for certificate #{identifier}: #{response.status}"
    when 'http://nchs.cdc.gov/vrdr_causeofdeath_coding', 'http://nchs.cdc.gov/vrdr_demographics_coding'
      id = header['id']
      puts "Found a coding response message of type #{header['eventUri']} with ID #{id} for certificate #{identifier}, acknowledging"
      message_filename = "#{identifier}_#{header['eventUri'] == 'http://nchs.cdc.gov/vrdr_causeofdeath_coding' ? 'cause_of_death' : 'demographics'}_coding.json"
      File.write(message_filename, message.to_json)
      ack = `dotnet #{CLI_PATH} ack #{message_filename}`
      File.write("#{identifier}_#{header['eventUri'] == 'http://nchs.cdc.gov/vrdr_causeofdeath_coding' ? 'cause_of_death' : 'demographics'}_coding_acknowledgement.json", ack)
      response = token.post("/OSELS/NCHS/NVSSFHIRAPI/#{jurisdiction}/Bundles",
                            headers: { 'Content-Type' => 'application/json' },
                            body: ack)
      puts "Server response acknowledging #{header['eventUri']} message for certificate #{identifier}: #{response.status}"
    else
      puts "Found a message of type #{header['eventUri']} for certificate #{identifier}, skipping"
    end
  rescue StandardError => e
    if header && identifier
      puts "Error processing message of type #{header['eventUri']} for certificate #{identifier}: #{e}"
    else
      puts "Error processing message: #{e}"
    end
  end
end
