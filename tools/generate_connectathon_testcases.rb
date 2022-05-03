# This script takes an excel file provided by NCHS (see FHIRTestCases*.xlsx in this directory)
# and produces csharp code for inclusion in the VRDR dot net library.
# The process is semi-automated.
# Other limitations/shortcuts taken:
#   - the number of testcases is hardcoded to 3, so adding more test cases would involve changes to the code
#   - the rows with the name information are hardcoded, rather than searched-for/found in the file, so changes to the structure of the input would require code changes.
#
# Usage: ruby tools/generate_connectathon_testcases.rb <path-to-excel-files>
#
# Output is produced in the generated subdirectory and needs to be pasted into the Connectahon.cs file.  The Files are named connectathon_<name>.cs
#
#

require "pry"
require "roo"
require "byebug"


def get_file_type(file)
  File.extname(file).gsub(".", "")
end

def open_spreadsheet(file)
  extension = get_file_type(file)
  if ["csv", "xls", "xlsx"].include? extension
    Roo::Spreadsheet.open(file, extension: extension)
  else
    raise "Unknown file type: #{file}"
  end
end
# Columns
IJEField = 0
IJEBegin = 1
IJELength = 2
Description = 3
IJEName = 4

#rows
GNAME = 7
FNAME = 9
record_col = [5,6,7]



puts ARGV[0]

xlsx = open_spreadsheet(ARGV[0])
xlsx.default_sheet = "Sample Clean Records"

record_col.each do |col|
    name = (xlsx.cell(GNAME,col) + xlsx.cell(FNAME,col)).strip
    filename = "generated/connectathon_#{name}.cs"
    puts filename
    out = File.open(filename, "w")
    out.puts "/// <summary>Generate the #{xlsx.cell(GNAME,col)} #{xlsx.cell(FNAME,col).strip} example record</summary>"
    out.puts "public static DeathRecord #{name}()
    {
    \tIJEMortality ije = new IJEMortality(\"\"); // blank IJE
"
    for row in (2..128) do
        fieldName = xlsx.cell(row,IJEName)
        fieldValue = xlsx.cell(row,col).to_s.split('(').first   # spreadsheet includes parenthetical information for humans that we ignore
        next if fieldValue == "␢"
        next if fieldValue == nil
        out.puts "\t\tije.#{fieldName} = \"#{fieldValue.strip}\";"
    end
    out.puts "\tDeathRecord record = ije.ToDeathRecord();
    \treturn record;
    }"
    out.close
end
