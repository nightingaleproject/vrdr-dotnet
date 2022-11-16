# coding: utf-8

require 'creek'
require 'csv'
require 'faker'

# Files:
# - DeathLitx201x.csv = Literal Strings
# - DeathStatx201x.csv = Statistical Data

$year = 'F2016'
# $year = '2017Q3'
death_stat_file = "./wa_state/DeathStat#{$year}.xlsx"
death_lit_file = "./wa_state/DeathLit#{$year}.csv"
ije_data_mappings_file = './IJE_File_Layouts_Input_Mapping_WA_Version_2021.xlsx'
$output_dir = './wa_output/'
puts($output_dir)

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
  elsif ije_key == 'INJPL' && ije_field_string != ""
    # Injury Place is in a full-phrase and inconsistent format. IJE is expecting a one-character value. Setting to '9' for unknown.
    ije_field_string = "9"
  elsif ije_key == 'TOI_HR' && ije_field_string == "99"
    # The wa input unknown time of injury hour is "99" but IJE expects a 4-digit value.
    ije_field_string = "9999"
  elsif ije_key == 'TOI_HR' && ije_field_string.include?(":")
    # Convert any hour data that contains a colon.
    ije_field_string = ije_field_string.split(":")[0]
    ije_field_string = ije_field_string[ije_field_string.length()-2, ije_field_string.length()]
    puts(ije_field_string)
  elsif ije_key == 'MARITAL' && ije_field_string == "P"
    # "P" is not a valid IJE MARITAL value. Set to "U" for unknown.
    ije_field_string = "U"
  elsif ije_key == 'DPLACE' && ije_field_string == "8"
    # "8" is not a valid IJE DPLACE value. Set to "9" for unknown.
    ije_field_string = '9'
  elsif ije_key == 'DPLACE' && ije_field_string == "0"
    # "0" is not a valid IJE DPLACE value. Set to "9" for unknown.
    ije_field_string = "9"
  elsif ije_key == 'DISP' && ije_field_string == "N"
    # "N" is not a valid IJE DISP value. Set to "U" for unknown.
    ije_field_string = "U"
  elsif ije_key == 'DOI_MO' && ije_field_string.include?("/")
    # Sometimes there's a malformed date, just set it to "99" for unknown. We could just remove the "/" but there are cases where the dates are further malformed so this is safer.
    ije_field_string = "99"
  elsif ije_key == 'DOI_DY' && ije_field_string.include?("/")
    # Sometimes there's a malformed date, just set it to "99" for unknown. We could just remove the "/" but there are cases where the dates are further malformed so this is safer.
    ije_field_string = "99"
  elsif ije_key == 'DOI_MO' && !is_number?(ije_field_string) && ije_field_string != ""
    # Sometimes the date of month is in the form "Ap", "Ma". Need to take the full `Injury date` field to get the proper month data.
    ije_field_string = convert_month_str(data_row['Injury date'])
  elsif ije_key == 'DSTATE' && ije_field_string == ""
    # Towards the end of the dataset, they stopped including DSTATE for some reason. I'm adding back as "WA".
    ije_field_string = "WA"
  end
  return ije_field_string
end

# Converts a 2-char month string to a number. Obviously, there are overalapping months but that's how it is in the data.
def convert_month_str(string)
  if string.include?("Jan")
    return "1"
  elsif string.include?("Feb")
    return "2"
  elsif string.include?("Mar")
    return "3"
  elsif string.include?("Apr")
    return "4"
  elsif string.include?("May")
    return "5"
  elsif string.include?("Jun")
    return "6"
  elsif string.include?("Jul")
    return "7"
  elsif string.include?("Aug")
    return "8"
  elsif string.include?("Sep")
    return "9"
  elsif string.include?("Oct")
    return "10"
  elsif string.include?("Nov")
    return "11"
  elsif string.include?("Dec")
    return "12"
  end
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

  # Only so many records at a time can be export via the vrdr CLI.
  records_before_export = 15

  # Array of IJE record strings.
  ije_records = Array.new { '' }
  # Skip column headers row flag.
  skip_headers = true
  # Iterate over the input data sheet which contains all the excel-based death records.
  data_sheet.simple_rows.each do |data_row|

    if ije_records.length() > records_before_export
      export_records(ije_records)
      ije_records = Array.new { '' }
    end
    # Skip column headers row.
    if skip_headers
      skip_headers = false
      next
    end

    # Check if this file has already been exported:
    file_no = data_row['State file number']
    file_name = '2022' + 'WA' + file_no[4..12]
    if File.exists?($output_dir + file_name + '.json')
      # puts("Aready exported, skipping " + file_name)
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
              raw_ije = parse_ije_field(data_row, header, ije_key, mapping_row)
              length = raw_ije.length()
              if ije_key == "TOD"
                length = 2
              end
              ije_field_string = ije_field_string + format_ije_data(raw_ije, length)
            end
            if ije_key == 'TOD' && ije_field_string == "2400"
              # "2400" is not a valid IJE TOD value. Set to "2359" for one minute previous.
              ije_field_string = "2359"
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
      # Convert the fileno field data.
      if ije_key == 'FILENO'
        # Remove the first 4 digits which are the year.
        ije_field_string = ije_field_string[4, ije_field_string.length]
      end
      ije_length = mapping_row['Length'].to_i()
      ije_field_string = format_ije_data(ije_field_string, ije_length)
      # Insert IJE field data into record.
      ije_loc = mapping_row['Beginning Location'].to_i()
      ije_record_string[ije_loc..(ije_loc+ije_length)] = ije_field_string
    end
    # Remove first character since ije is 1-based not 0.
    ije_record_string = ije_record_string[1, ije_record_string.length()]
    # Escape any invalid characters.
    ije_record_string = ije_record_string.gsub("`", " ")
    ije_record_string = ije_record_string.gsub("\"", " ")
    ije_records.push(ije_record_string)
    # puts(ije_record_string)
    puts("Parsed Record " + ije_record_string[0..11])
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
  puts("Finished export set.")
end

puts("Starting data parsing and converting...")
ije_records = convert_ije_records(data.sheets[0], ije_data_mappings.sheets[0], data_lit)
export_records(ije_records)

puts("Conversion and exporting complete.")
