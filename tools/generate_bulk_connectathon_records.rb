require 'parallel'

# Generate quantities of connectathon records
#
# Usage: ruby generate_bulk_connectathon_records.rb <StartCertificateNumber> <Count> <Directory>
#
# Generates <Count> records, starting with the specified <StartCertificateNumber>, rotating
# through the 4 distinct connectathon records, and writing them to the specified <Directory>
#
# Uses the parallel gem to run using all cores

start_certificate_number = (ARGV.shift || 0).to_i
count = (ARGV.shift || 0).to_i
output_directory = ARGV.shift

raise "Must supply a starting certificate number greater than 0" unless start_certificate_number > 0
raise "Must supply a count greater than 0" unless count > 0
raise "Must supply a valid output directory" unless output_directory && Dir.exists?(output_directory)

# It's faster to run the built command line app than to repeatedly call dotnet run... make sure it's built
cli_path = File.expand_path(File.join(__dir__, '..', 'VRDR.CLI'))
current_path = Dir.pwd
command = "cd #{cli_path} ; dotnet build --configuration Release ; cd #{current_path}"
puts command
system command

Parallel.each(0...count) do |i|
  certificate_number = start_certificate_number + i
  record_selector = (i % 4) + 1
  file_name = File.join(output_directory, "#{Time.now.year}MA#{'%06d' % certificate_number}.json")
  command = "#{cli_path}/bin/Release/netcoreapp3.1/DeathRecord.CLI connectathon #{record_selector} #{certificate_number} MA | jq > #{file_name}"
  puts command
  system command
end
