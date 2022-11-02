# coding: utf-8

# Files:
# - DeathLit20xx.csv = Literal Strings
# - DeathStat20xx.csv = Statistical Data

require 'json'
mappings_file = File.read('./wa_to_ije_mappings.json')
mappings_hash = JSON.parse(mappings_file)

year = 2016
death_stat_file = "./wa_state/DeathStatF#{year}.xlsx"

require 'creek'
data = Creek::Book.new death_stat_file, with_headers: true
# Check that headers are being imported properly.
# data.sheets[0].simple_rows.each do |data_row|
#   puts(data_row)
# end

require 'faker'
Faker::Name.unique.clear # Clears used values for Faker::Name
Faker::UniqueGenerator.clear # Clears used values for all generators

# Checks for and returns special cases. Mostly used for fake identifying attributes that have been removed from the input dataset.
def check_special_cases(ije_header)
  case ije_header
    # Faked identifying fields.
  when 'SSN'
    return Faker::Number.number(digits: 9).to_s
  when 'GNAME'
    return Faker::Name.first_name
  when 'MNAME'
    return Faker::Name.middle_name
  when 'LNAME'
    return Faker::Name.last_name
  when 'SUFF'
    return Faker::Name.suffix
  when 'FLNAME'
    return Faker::Name.last_name
  when 'CITYC'
    return Faker::Address.city
  when 'VOID'
    return '0'
  when 'COUNTRYC'
    # Assuming USA since there's no residence country in the data.
    return 'US'
  when 'ALIAS'
    return '0'
  when 'SEX_BYPASS'
    return ''
  when 'AGE_BYPASS'
    return ''
  when 'MARITAL_BYPASS'
    return ''
  when 'MFILED'
    # Defaults to electronic filed mode.
    return '0'
  end
  return nil
end

# Parses a single IJE field from a data row with the given header.
def parse_ije_field(data_row, header, ije_key)
  ije_field_string = data_row[header]
  if ije_field_string == nil
    # If there is no data for this data value, set it to blank.
    # puts("Record `#{data_row['State file number']}` is missing data for IJE Field `#{ije_key}` with infile header `#{header}`.")
    return ''
  end
  return ije_field_string
end

# Returns whether a given string is numeric.
def is_number? string
  true if Float(string) rescue false
end

# Converts an input data sheet into IJE records based on the given mappings of IJE->infile_headers.
def convert_ije_records(data_sheet, mappings_hash)
   # Array of IJE record strings.
  ije_records = Array.new { '' }
  # Skip column headers row flag.
  skip_headers = true
  # Iterate over the input data sheet which contains all the excel-based death records.
  data_sheet.simple_rows.each do |data_row|
    # Skip column headers row.
    if skip_headers
      skip_headers = false
      next
    end
    # puts(data_row)
    # Initialize this record's IJE record.
    ije_record_string = ''.rjust(5000, ' ')
    mappings_hash.each do |ije_key, field_properties|
      # Initialize this IJE field string.
      ije_field_string = nil
      special_case = check_special_cases(ije_key)
      if special_case != nil
        # If this is a special case, set the ije field string to that value.
        ije_field_string = special_case
      else
        infile_header = field_properties['infile_header']
        if infile_header.kind_of?(Array) && infile_header.length() > 1
          # If this input is an field_properties, iterate over and append it for this IJE field value.
          ije_field_string = ''
          infile_header.each do |header|
            ije_field_string = ije_field_string + parse_ije_field(data_row, header, ije_key)
          end
        else
          if !data_row.key?(infile_header)
            puts("ERROR: IJE Field `#{ije_key}` does not map to any infile headers.")
            ije_field_string = ''
          end
          # If this is not a special case or an field_properties of headers, use the input dataset and ije->infile_header mapping.
          ije_field_string = parse_ije_field(data_row, infile_header, ije_key)
        end
      end
      ije_length = field_properties['ije_loc']['length']
      # Truncate and warn of overlong IJE field values.
      if ije_key == 'FILENO' && ije_field_string.start_with?('2016')
        ije_field_string = ije_field_string[4, ije_field_string.length()]
      end
      if ije_field_string.length > ije_length
        # puts "Record `#{data_row['State file number']}` contains an overlong data field `#{ije_field_string}` for `#{ije_key}`. Expected `#{ije_length}`. Truncating."
        ije_field_string = ije_field_string[0,ije_length]
      end
      # Add padding spaces to the of the string according to its length.
      if is_number?(ije_field_string)
        # Numbers are right justified.
        ije_field_string = ije_field_string.rjust(ije_length, ' ')
      else
        # Strings are left justified.
        ije_field_string = ije_field_string.ljust(ije_length, ' ')
      end
      # Insert IJE field data into record.
      ije_loc = field_properties['ije_loc']['location']
      ije_record_string[ije_loc..(ije_loc+ije_loc)] = ije_field_string
    end
    # puts(ije_record_string)
    ije_records.push(ije_record_string)
  end
  return ije_records
end

ije_records = convert_ije_records(data.sheets[0], mappings_hash)
File.open("input-to-ije-records-#{year}.txt", "w+") do |f|
  f.truncate(0)
  ije_records.each do |record|
    f.puts(record)
  end
end