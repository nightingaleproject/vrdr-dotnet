# coding: utf-8

# Take an excel file from DACEB and convert it to FHIR records; we parse the excel file and extract the data
# for each record, with the goal of a simple IJE mapping; we write one file per record

filename = ARGV.shift
raise "Please provide the Excel file name as an argument to this script" unless filename

require 'creek'
data = Creek::Book.new(filename)

# Helper method: Given a sheet, the first row that has data, and the expected number of records, return an
# array of all the IJE records from that sheet
def extract_ije_records(sheet, first_data_row, record_count)

  # Data starts on row N (0 based); each row contains position (offset), length (we use this to determine the
  # amount of padding), description (ignored), IJE field name (ignored), data for N records (one column each)

  rows = sheet.rows.to_a
  row_number = first_data_row
  ije_records = Array.new(record_count) { '' } # Array of strings
  while row = rows[row_number] do
    row_values = row.to_a.map(&:last) # Convert from hash of cell name to value
    offset = row_values[0].to_i - 1 # Make offset 0 based
    length = row_values[1].to_i
    values = row_values[4, record_count]
    # If there's a linefeed in a value, take everything before the linefeed
    values.map! { |v| v.to_s.split("\n").first.to_s.strip }
    # There appears to be a special character for blank
    values.map! { |v| v == '␢' ? '' : v }
    # puts "#{length} #{values.join(',')}"
    record_count.times do |record_number|
      data = values[record_number]
      # Truncate data if needed (and inform user)
      if data.length > length
        puts "Note: Record #{ije_records[record_number][0,12]} contains an overlong data field #{row_values[3]}, you'll have to rewrite the JSON to contain #{data}"
        data = data[0,length]
      end
      # Append data, padded with correct amount of space before (based on offset) and after (based on length)
      ije_records[record_number] += ' ' * (offset - ije_records[record_number].length)
      ije_records[record_number] << "%-#{length}s" % data
    end
    row_number += 1
  end

  # Pad each record to 5000 characters and return the result
  ije_records.map { |record| record += ' ' * (5000 - record.length) }
end

# Helper method: write IJE and JSON records to files based on the certificate identifier information in the record
def write_record(record, annotation)
  filename = record[0,12] # First 12 bytes are the year, jurisdiction, and certificate number
  filename << "_#{annotation}" if annotation # If we have an extra label add it to the filename
  ije_filename = "#{filename}.ije"
  json_filename = "#{filename}.json"
  puts "Writing record to #{ije_filename}"
  File.open(ije_filename, 'w') { |file| file.write(record) }
  puts "Writing record to #{json_filename}"
  cli_path = File.expand_path(File.join(__dir__, '..', 'VRDR.CLI'))
  command = "dotnet run --project #{cli_path} ije2json #{ije_filename} > #{json_filename}"
  puts command
  system(command)
end

# Two sheets; sheet one is good records (10 total), sheet two is bad records (10 total)

#  Only if the first sheet is present and has the right number of records...
if data.sheets.length > 0 && data.sheets[0].rows.count > 9
  good_ije_records = extract_ije_records(data.sheets[0], 1, 10)
  10.times do |i|
    write_record(good_ije_records[i], "good")
  end
else
  puts "No 'good' records processed"
end

#  Only if the second sheet is present and has the right number of records...
if data.sheets.length > 1 && data.sheets[1].rows.count > 9
  bad_ije_records = extract_ije_records(data.sheets[1], 2, 10) # First row of data is 2
  10.times do |i|
    write_record(bad_ije_records[i], "bad")
  end
else
  puts "No 'bad' records processed"
end

#  Only if the third sheet is present and has the right number of records...
if data.sheets.length > 2 && data.sheets[2].rows.count > 9
  bad_ije_records = extract_ije_records(data.sheets[2], 3, 10) # First row of data is 3
  10.times do |i|
    write_record(bad_ije_records[i], "change")
  end
else
  puts "No 'change' records processed"
end
