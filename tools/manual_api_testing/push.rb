# Submit messages to the NVSS API; takes as arguments the jurisdiction to submit for (e.g., MA) and the files
# to send (must be valid FHIR messages)

require 'yaml'
require 'oauth2'
require 'active_support/time'
require 'securerandom'
require 'time'
require 'json'
require 'parallel'

CONFIG_PATH = File.join(__dir__, 'config.yml')
if (!File.exists?(CONFIG_PATH))
  puts "Cannot find the config file at #{CONFIG_PATH}, you may need to create it"
  puts "It should look something like this (including the ---):"
  puts ['client_id', 'client_secret', 'username', 'password'].inject({}) { |h, k| h[k] = k ; h }.to_yaml
  exit
end

credentials = YAML.load(File.read(CONFIG_PATH))

if ARGV.include?('--single')
  ARGV.delete('--single')
  single = true
end

if ARGV.include?('--sequential')
  ARGV.delete('--sequential')
  sequential = true
end

jurisdiction = ARGV.shift
if jurisdiction.nil? || !jurisdiction.match(/^[A-Z][A-Z]$/)
  puts "You must provide the jurisdiction code (e.g., MA) as the first argument"
  exit
end

if ARGV.length < 1
  puts "Please provide files to submit"
  exit
end

# Load all the files into memory first
files = []
ARGV.each do |filename|
  files << { filename: filename, message: File.read(filename) }
end

# Request the token using the OAuth client
client = OAuth2::Client.new(credentials['client_id'],
                            credentials['client_secret'],
                            site: 'https://apigw.cdc.gov/',
                            token_url: '/auth/oauth/v2/token')

token = client.password.get_token(credentials['username'], credentials['password'])

# Submit the files in chunks of 20
failures = []
options = {}
if sequential # Don't use parallel processing
  options[:in_processes] = 0
end
slice_size = single ? 1 : 20
Parallel.each(files.each_slice(slice_size), options) do |slice|

  output = "Sending files:\n"
  slice.each { |s| output += "  #{s[:filename]}\n" }

  # Build the package
  if slice.size > 1
    url = "/OSELS/NCHS/NVSSFHIRAPI/#{jurisdiction}/Bundles"
    messages = slice.map { |s| JSON.parse(s[:message]) }
    message_entries = messages.map { |message| { id: SecureRandom.uuid, request: { method: 'POST', url: url }, resource: message } }
    submission = { resourceType: 'Bundle', type: 'batch', id: SecureRandom.uuid, timestamp: Time.now.iso8601, entry: message_entries }
  else
    submission = JSON.parse(slice.first[:message])
  end

  body = submission.to_json

  begin
    response = token.post("/OSELS/NCHS/NVSSFHIRAPI/#{jurisdiction}/Bundles",
                          headers: { 'Content-Type' => 'application/json' },
                          body: body)
    output += "Server response for files #{slice.first[:filename]}-#{slice.last[:filename]}: #{response.status}\n"
    if slice.size > 1
      File.write("#{slice.first[:filename]}-#{slice.last[:filename]}", response.body)
      JSON.parse(response.body)['entry'].each do |entry|
        output += "  Record response: #{entry['response']['status']}\n"
      end
    end
  rescue Faraday::Error, OAuth2::Error => e
    output += "Failed to send files in slice #{slice.first[:filename]}-#{slice.last[:filename]}\n"
    output += e.message
    output += e.backtrace.join("\n")
    output += "\n"
    failures += slice
  ensure
    # We've collected up the output so that different parallel processes are less likely to step on each other's output
    puts output
  end
end

if failures.size > 0
  puts "Failed to send the following #{failures.size} records:"
  failures.each { |f| puts "  #{f[:filename]}\n" }
end
