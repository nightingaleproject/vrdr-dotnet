# Submit a message to the NVSS API; takes as arguments the jurisdiction to submit for (e.g., MA) and the file
# to send (must be a valid FHIR message)

require 'oauth2'
require 'active_support/time'

jurisdiction = ARGV.shift
if jurisdiction.nil? || !jurisdiction.match(/^[A-Z][A-Z]$/)
  puts "You must provide the jurisdiction code (e.g., MA) as the first argument"
  exit
end

if ARGV.length < 1
  puts "Please provide a file to submit"
  exit
end

body = ARGF.to_a.join

client_id = File.read('clientid.txt')
client_secret = File.read('clientsecret.txt')
username = File.read('username.txt')
password = File.read('password.txt')

client = OAuth2::Client.new(client_id,
                            client_secret,
                            site: 'https://apigw.cdc.gov/',
                            token_url: '/auth/oauth/v2/token')

token = client.password.get_token(username, password)

response = token.post("/OSELS/NCHS/NVSSFHIRAPI/#{jurisdiction}/Bundles",
                      headers: { 'Content-Type' => 'application/json' },
                      body: body)

puts "Server response: #{response.status}"
