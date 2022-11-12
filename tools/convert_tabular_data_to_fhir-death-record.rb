# coding: utf-8

require 'creek'
require 'csv'
require 'faker'

# Files:
# - DeathLitx201x.csv = Literal Strings
# - DeathStatx201x.csv = Statistical Data

# $year = 'F2016'
$year = '2017Q3'
death_stat_file = "./wa_state/DeathStat#{$year}.xlsx"
death_lit_file = "./wa_state/DeathLit#{$year}.csv"
ije_data_mappings_file = './IJE_File_Layouts_Input_Mapping_WA_Version_2021.xlsx'
$output_dir = './wa_output/'

$file_no_start = 7
file_no_len = 6
$file_no_end = $file_no_start + file_no_len

data = Creek::Book.new death_stat_file, with_headers: true
csv_file = File.read(death_lit_file)
# Remove invalid non utf-8 characters such as †. 
csv_file = csv_file.scrub('')
data_lit = CSV.parse(csv_file, headers: true, encoding: 'windows-1251:utf-8')
ije_data_mappings = Creek::Book.new ije_data_mappings_file, with_headers: true

Faker::Name.unique.clear # Clears used values for Faker::Name
Faker::UniqueGenerator.clear # Clears used values for all generators

# Checks for and returns generated faked data. Mostly used for fake identifying attributes that have been removed from the input dataset.
def gen_faked_value(ije_header)
  case ije_header
    # Faked identifying fields.
  when 'SSN'
    return Faker::Number.number(digits: 9).to_s
  when 'GNAME', 'DDADF', 'DMOMF', 'CERTFIRST'
    return Faker::Name.first_name
  when 'MNAME', 'DMIDDLE', 'DDADMID', 'DMOMMID', 'SPOUSEMIDNAME', 'CERTMIDDLE'
    return Faker::Name.middle_name
  when 'LNAME', 'DMOMMDN', 'CERTLAST'
    return Faker::Name.last_name
  when 'SUFF', 'SPOUSESUFFIX', 'FATHERSUFFIX', 'MOTHERSSUFFIX', 'CERTSUFFIX'
    return Faker::Name.suffix
  when 'FLNAME'
    return Faker::Name.last_name
  when 'CITYTEXT_R', 'DBPLACECITY', 'FUNCITYTEXT', 'CERTCITYTEXT'
    return Faker::Address.city
  when 'STNUM_R', 'FUNFACSTNUM', 'CERTSTNUM'
    return Faker::Number.number(digits: 3).to_s
  when 'STNAME_R', 'FUNFACSTRNAME', 'CERTSTRNAME'
    return Faker::Address.street_name
  when 'STDESIG_R', 'FUNFACSTRDESIG', 'CERTSTRDESIG'
    return Faker::Address.street_suffix
  when 'UNITNUM_R', 'FUNUNITNUM', 'CERTUNITNUM'
    return Faker::Address.building_number
  when 'ADDRESS_R', 'FUNFACADDRESS', 'CERTADDRESS'
    return Faker::Address.full_address
  when 'FUNSTATECD', 'CERTSTATECD'
    return Faker::Address.state_abbr
  when 'FUNSTATE', 'CERTSTATE'
    return Faker::Address.state
  when 'FUNZIP', 'CERTZIP'
    return Faker::Address.zip_code
  end
  return nil
end

# Parses a single IJE field from a data row with the given header.
def parse_ije_field(data_row, header, ije_key, field_properties)
  if !data_row.key?(header) && field_properties['default'] == nil
    puts("ERROR: IJE Field `#{ije_key}` does not map to any input data file headers.")
    ije_field_string = ''
    return ije_field_string
  end
  ije_field_string = data_row[header].to_s
  if ije_field_string == nil
    # If there is no data for this data value, set it to blank.
    # puts("Record `#{data_row['State file number']}` is missing data for IJE Field `#{ije_key}` with infile header `#{header}`.")
    ije_field_string = ''
  elsif field_properties['special value mapping'] == 'ethnicity'
    ije_field_string = map_ethnicity_value(data_row[header])
  end
  return ije_field_string
end

# Maps the ethncitiy value to an IJE-accepted format.
def map_ethnicity_value(value)
  if value == 'N'
    return 'N'
  elsif value == 'Y'
    return 'H'
  end
  return 'U'
end

# Returns whether a given string is numeric.
def is_number?(string)
  true if Float(string) rescue false
end

# Formats the given ije data based on the length and whether it is a number or text.
def format_ije_data(ije_field_string, ije_length)
  # Truncate and warn of overlong IJE field values.
  if ije_field_string.length > ije_length
    # puts "Record `#{data_row['State file number']}` contains an overlong data field `#{ije_field_string}` for `#{ije_key}`. Expected `#{ije_length}`. Truncating."
    ije_field_string = ije_field_string[0,ije_length]
  end
  if ije_field_string == "."
    ije_field_string = ""
  end
  # Add padding spaces to the of the string according to its length.
  if is_number?(ije_field_string)
    # Numbers are right justified with 0s.
    ije_field_string = ije_field_string.rjust(ije_length, '0')
  else
    # Strings are left justified.
    # But it should be automatic, no need to manually do this.
    ije_field_string = ije_field_string.ljust(ije_length, ' ')
  end
end

# Converts an input data sheet into IJE records based on the given mappings of IJE->datafile_headers.
def convert_ije_records(data_sheet, ije_data_mappings, data_lit)

  records_to_run = 10
  records_run = 0

  # Array of IJE record strings.
  ije_records = Array.new { '' }
  # Skip column headers row flag.
  skip_headers = true
  # Iterate over the input data sheet which contains all the excel-based death records.
  data_sheet.simple_rows.each do |data_row|
    records_run = records_run + 1
    if records_run > records_to_run
      return ije_records
    end
    # Skip column headers row.
    if skip_headers
      skip_headers = false
      next
    end
    # puts(data_row)
    skip_mappings_headers = true
    # Initialize this record's IJE record.
    ije_record_string = ' ' * 5000
    ije_data_mappings.simple_rows.each do |mapping_row|
      # Skip column headers row.
      if skip_mappings_headers
        skip_mappings_headers = false
        next
      end
      if mapping_row['Input Data Field Name'] == '?' && mapping_row['default'] == 'blank'
        # puts("Skipped #{mapping_row['Input Data Field Name']}")
        next
      end
      ije_key = mapping_row['IJE Field Name']
      if ije_key == nil
        next
      end
      # Initialize this IJE field string.
      ije_field_string = nil
      if mapping_row['Input Data Field Name'] == 'faker'
        # If this is a faked case, set the ije field string to a faked value.
        ije_field_string = gen_faked_value(ije_key)
      else
        if !mapping_row.key?('Input Data Field Name')
          puts("ERROR: IJE Field `#{ije_key}` has not defined an input data header to map to.")
        end
        datafile_header = mapping_row['Input Data Field Name']
        if mapping_row['file'] == 'stat'
          if datafile_header.start_with?('[')
            # If this input has multiple mappings, iterate over and append it for this IJE field value.
            ije_field_string = ''
            datafile_header = datafile_header.tr('[]', '')
            datafile_header.split(',').each do |header|
              ije_length = mapping_row['Length'].to_i()
              ije_field_string = ije_field_string + format_ije_data(parse_ije_field(data_row, header, ije_key, mapping_row), ije_length)
            end
          else
            # If it does not have multiple mappings, treat it as a single mapping.
            ije_field_string = parse_ije_field(data_row, datafile_header, ije_key, mapping_row)
          end
        elsif mapping_row['file'] == 'lit'
          # Since this data is in the literal data file, get the record number and extract the relevant row from the literal data.
          record_num = data_row['State file number']
          lit_data_row = data_lit.select { |row| row['State File Number'] == record_num }[0]
          # puts("Literal Row: `#{lit_data_row}` for IJE Field `#{ije_key}`")
          ije_field_string = parse_ije_field(lit_data_row, datafile_header, ije_key, mapping_row)
        elsif datafile_header == '?' && mapping_row['default'] != nil
          # If there is a default value due to a lack of mapping in the file, use it.
          ije_field_string = mapping_row['default']
          if ije_field_string == 'blank'
            ije_field_string = ''
          end
        else
          puts("ERROR: IJE Field mapping `#{ije_key}` does not include a file to map to and has no default value.")
        end
      end
      # Convert the fileno field data if needed.
      if ije_key == 'FILENO'
        # Remove the first 4 digits which are the year.
        ije_field_string = ije_field_string[4, ije_field_string.length]
      end
      # Convert the place of death type field data if needed.
      if ije_key == 'DPLACE' && ije_field_string == "0"
        # 0 is not a valid value for DPLACE but is used occasionally in the wa data. Set to '9' for unknown.
        ije_field_string = "9"
      end
      ije_length = mapping_row['Length'].to_i()
      ije_field_string = format_ije_data(ije_field_string, ije_length)
      # Insert IJE field data into record.
      ije_loc = mapping_row['Beginning Location'].to_i()
      ije_record_string[ije_loc..(ije_loc+ije_length)] = ije_field_string
    end
    # puts(ije_record_string)
    # Remove first character since ije is 1-based not 0.
    ije_record_string = ije_record_string[1, ije_record_string.length()]
    ije_records.push(ije_record_string)
    puts("Parsed Record " + ije_record_string[0..12])
  end
  return ije_records
end

def export_records(ije_records)
  puts("Starting export and conversion to FHIR.")
  cli_path = File.expand_path(File.join(__dir__, '..', 'VRDR.CLI'))
  output_dir = "./wa_output/"
  ije_args = ije_records.join("\" \"")
  command = "dotnet run --project #{cli_path} ije2json #{output_dir} \"#{ije_args}\""
  # puts(command)
  system(command)
end

puts("Starting the data parsing...")
ije_records = convert_ije_records(data.sheets[0], ije_data_mappings.sheets[0], data_lit)
puts("Finished parsing.")
export_records(ije_records)
puts("Exporting complete.")

# def export_record(record)
#   file_no = record[$file_no_start, $file_no_end]
#   # Remove first character since ije is 1-based.
#   record = record[1, record.length()]
#   export_file = "#{$output_dir}ije-record-#{$year}-#{file_no}.txt"
#   File.open(export_file, "w+") do |f|
#     f.truncate(0)
#     f.puts(record.ljust(5000, ' '))
#   end
# end