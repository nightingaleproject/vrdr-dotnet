# Submit messages to the NVSS API; takes as arguments the jurisdiction to submit for (e.g., MA) and the files
# to send (must be valid FHIR messages)

require 'oauth2'
require 'active_support/time'
require 'securerandom'
require 'time'
require 'json'

jurisdiction = ARGV.shift
if jurisdiction.nil? || !jurisdiction.match(/^[A-Z][A-Z]$/)
  puts "You must provide the jurisdiction code (e.g., MA) as the first argument"
  exit
end

if ARGV.length < 1
  puts "Please provide files to submit"
  exit
end

client_id = File.read('clientid.txt')
client_secret = File.read('clientsecret.txt')
username = File.read('username.txt')
password = File.read('password.txt')

# Load all the files into memory first
files = []
ARGV.each do |filename|
  files << { filename: filename, message: File.read(filename) }
end

# Request the token using the OAuth client
client = OAuth2::Client.new(client_id,
                            client_secret,
                            site: 'https://apigw.cdc.gov/',
                            token_url: '/auth/oauth/v2/token')

token = client.password.get_token(username, password)

# Submit the files in chunks of 20
files.each_slice(20).each do |slice|

  puts "Sending files:"
  slice.each { |s| puts "  #{s[:filename]}" }

  # Build the package
  url = "/OSELS/NCHS/NVSSFHIRAPI/#{jurisdiction}/Bundles"
  messages = slice.map { |s| JSON.parse(s[:message]) }
  message_entries = messages.map { |message| { id: SecureRandom.uuid, request: { method: 'POST', url: url }, resource: message } }
  submission = { resourceType: 'Bundle', type: 'batch', id: SecureRandom.uuid, timestamp: Time.now.iso8601, entry: message_entries }

  body = submission.to_json

  response = token.post("/OSELS/NCHS/NVSSFHIRAPI/#{jurisdiction}/Bundles",
                        headers: { 'Content-Type' => 'application/json' },
                        body: body)

  puts "Server response: #{response.status}"
  JSON.parse(response.body)['entry'].each do |entry|
    puts "  Record response: #{entry['response']['status']}"
  end

end
