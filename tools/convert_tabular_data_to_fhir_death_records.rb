# coding: utf-8

# This script takes statistical and literal tabular death datasets and converts them to IJE using an input file that defines
# the mappings of IJE fields to the dataset headers. It then converts the IJE records to FHIR using the vrdr-dotnet CLI ije2json.

require 'creek'
require 'csv'
require 'faker'

### CLI Arguments.
# ARG1: The tabular data to IJE mappings file.
ije_data_mappings_file = ARGV.shift
# ARG2: The statistical death data tabular records file (Excel).
death_stat_file = ARGV.shift
# ARG3: The literal death data tabular records file (CSV).
death_lit_file = ARGV.shift
# ARG4: The number of records to convert.
num_records = ARGV.shift
# ARG5: The output directory.
output_dir = ARGV.shift
# ARG6: Optional, the data year to submit
data_year = ARGV.shift
# ARG7: Optional, the jurisdiction
jurisdiction = ARGV.shift
# ARG8: Optional, the input record number number to start with
first_record_number = ARGV.shift&.to_i
# ARG9: Optional, the output certificate number to start with
first_cert_number = ARGV.shift&.to_i

### Example CLI command with tabular dataset:
# ruby convert_tabular_data_to_fhir_death_records.rb IJE_File_Layouts_Tabular_Input_Mapping_Version_2021.xlsx state/DeathStat2017Q3.xlsx state/DeathLit2017Q3.csv 1 ./output/ 2022 WA 1 1

# Import IJE to tabular data headers mapping file.
ije_data_mappings = Creek::Book.new ije_data_mappings_file, with_headers: true
# Import statistical Excel file.
data = Creek::Book.new death_stat_file, with_headers: true
# Import literal CSV file.
csv_file = File.read(death_lit_file)
# Remove invalid non utf-8 characters such as †. 
csv_file = csv_file.scrub('')
data_lit = CSV.parse(csv_file, headers: true, encoding: 'ISO-8859-1')
# Import county name mappings CSV file.
county_csv_file = File.read("./state_county_codes.csv")
county_mappings = CSV.parse(county_csv_file, headers: true, encoding: 'ISO-8859-1')


# Intiailize the state abbreviation to full name mappings.
$state_abbr_to_name = {
  'AL' => 'Alabama', 'AK' => 'Alaska', 'AS' => 'America Samoa', 'AZ' => 'Arizona', 'AR' => 'Arkansas', 'CA' => 'California', 'CO' => 'Colorado', 'CT' => 'Connecticut', 'DE' => 'Delaware', 'DC' => 'District of Columbia', 'FM' => 'Federated States Of Micronesia', 'FL' => 'Florida', 'GA' => 'Georgia', 'GU' => 'Guam', 'HI' => 'Hawaii', 'ID' => 'Idaho', 'IL' => 'Illinois', 'IN' => 'Indiana', 'IA' => 'Iowa', 'KS' => 'Kansas', 'KY' => 'Kentucky', 'LA' => 'Louisiana', 'ME' => 'Maine', 'MH' => 'Marshall Islands', 'MD' => 'Maryland', 'MA' => 'Massachusetts', 'MI' => 'Michigan', 'MN' => 'Minnesota', 'MS' => 'Mississippi', 'MO' => 'Missouri', 'MT' => 'Montana', 'NE' => 'Nebraska', 'NV' => 'Nevada', 'NH' => 'New Hampshire', 'NJ' => 'New Jersey', 'NM' => 'New Mexico', 'NY' => 'New York', 'NC' => 'North Carolina', 'ND' => 'North Dakota', 'OH' => 'Ohio', 'OK' => 'Oklahoma', 'OR' => 'Oregon', 'PW' => 'Palau', 'PA' => 'Pennsylvania', 'PR' => 'Puerto Rico', 'RI' => 'Rhode Island', 'SC' => 'South Carolina', 'SD' => 'South Dakota', 'TN' => 'Tennessee', 'TX' => 'Texas', 'UT' => 'Utah', 'VT' => 'Vermont', 'VI' => 'Virgin Island', 'VA' => 'Virginia', 'WA' => 'Washington', 'WV' => 'West Virginia', 'WI' => 'Wisconsin', 'WY' => 'Wyoming'
}
$state_abbr_to_name.default = ''

# Initialize the Faker generators.
Faker::UniqueGenerator.clear # Clears used values for all generators

# Checks for and returns generated faked data based on the given ije key.
# Mostly used for fake identifying attributes that have been removed from the input dataset.
def generate_faked_value(ije_key)
  case ije_key
    # Faked identifying fields.
  when 'SSN'
    return Faker::Number.number(digits: 9).to_s
  when 'GNAME', 'SPOUSEF', 'DDADF', 'DMOMF', 'CERTFIRST'
    return Faker::Name.first_name
  when 'MNAME', 'DMIDDLE', 'DDADMID', 'DMOMMID', 'SPOUSEMIDNAME', 'CERTMIDDLE'
    return Faker::Name.middle_name
  when 'LNAME', 'SPOUSEL', 'DMOMMDN', 'CERTLAST'
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
  when 'FUNZIP', 'CERTZIP'
    return Faker::Address.zip_code
  end
  return nil
end

# Parses a single IJE field from a data row with the given header.
def parse_ije_field(data_row, header, ije_key, field_properties, county_mappings, ije_string)
  if !data_row.key?(header) && field_properties['default'] == nil
    puts("ERROR: IJE Field `#{ije_key}` does not map to any input data file headers.")
    ije_field_string = ''
    return ije_field_string
  end
  ije_field_string = data_row[header].to_s
  # Catch and convert any potential invalid IJE fields.
  ije_field_string = catch_invalid_values(ije_key, ije_field_string, field_properties, data_row, county_mappings, ije_string)
  return ije_field_string
end

# Maps fields with a a special case mapping.
def check_special_mappings(ije_field_string, field_properties, data_row, county_mappings, ije_string)
  case field_properties['special value mapping']
  when'ethnicity'
    ethncitiy_mapping = { 'N' => 'N', 'Y' => 'H' }
    ethncitiy_mapping.default = 'U'
    ije_field_string = ethncitiy_mapping[ije_field_string]
  when 'extract_month'
    ije_field_string = ije_field_string.split('-')[1]
  when 'extract_day'
    ije_field_string = ije_field_string.split('-')[2]
  when 'military_time'
    # If the time type is "P" (PM), then add 12 hours to the hour for military time.
    if data_row['Time of Injury Modifier'] == "P"
      ije_field_string = (ije_field_string.to_i() + 12).to_s()
    end
  when 'capitalize_first_letter'
    ije_field_string = ije_field_string.downcase()
    ije_field_string[0] = ije_field_string[0].capitalize()
  when 'reuse_funstatecd'
    state_abbr = ije_string[3851..3852]
    ije_field_string = $state_abbr_to_name[state_abbr]
  when 'reuse_certstatecd'
    state_abbr = ije_string[4215..4216]
    ije_field_string = $state_abbr_to_name[state_abbr]
  when 'reuse_certstatecd'
    state_abbr = ije_string[4215..4216]
    ije_field_string = $state_abbr_to_name[state_abbr]
  when 'use_2022_if_no_injury'
    # If there is time of injury data, set it to 2022.
    if ije_string[981..982].empty?() && ije_string[983..984].empty?()
      ije_field_string = '2022'
    end
  when 'two_digit_country'
    if ije_field_string.downcase() == 'united states' || ije_field_string.downcase() == 'US'
      ije_field_string = 'US';
    else
      ije_field_string = ''
    end
  when 'full_state_name'
    ije_field_string = $state_abbr_to_name[ije_field_string]
  when 'county_code_to_name'
    county_code = ije_field_string.rjust(3, '0')
    residence_state_fips = data_row['Residence state FIPS code']
    county_row = county_mappings.select { |row| row["county_fips"][row["county_fips"].length-3, row["county_fips"].length] == county_code && row["state_abbr"] == residence_state_fips }[0]
    if county_row == nil && county_code != "000"
      puts("ERROR: Missing a county name mapping for State #{residence_state_fips} and County Code #{county_code}.")
    end
    if county_row != nil && county_code != "000"
      ije_field_string = county_row["county_name"]
    end
  when 'toi_unit_modifier'
    # Time of injury gets coverted to military time.
    if !ije_field_string.strip.empty?
      ije_field_string = 'M'
    end
  end
  return ije_field_string
end

# Catches invalid IJE values for certain headers and converts them to a valid format.
def catch_invalid_values(ije_key, ije_field_string, field_properties, data_row, county_mappings, ije_string)
  if ije_field_string == nil
    # If there is no data for this data value, set it to blank.
    ije_field_string = ''
  end
  ije_field_string = check_special_mappings(ije_field_string, field_properties, data_row, county_mappings, ije_string)
  if ije_key == 'INJPL' && ije_field_string != ""
    # Injury Place is in a full-phrase and inconsistent format. IJE is expecting a one-character value. Setting to '9' for unknown.
    ije_field_string = "9"
  elsif ije_key == 'TOI_HR' && (ije_field_string == "99" || ije_field_string == "0099")
    # The WA data input unknown "Time of Injury Hour" is "99" but IJE expects a 4-digit value.
    ije_field_string = "9999"
  elsif ije_key == 'TOI_HR' && ije_field_string.include?(":")
    # Convert hour data that contains a colon.
    ije_field_string = ije_field_string.split(":")[0]
    ije_field_string = ije_field_string[ije_field_string.length()-2, ije_field_string.length()]
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
  elsif (ije_key == 'DOI_DY' || ije_key == 'DOI_MO') && ije_field_string.include?("/")
    # Sometimes there's a malformed date, set it to "99" for unknown.
    # We could remove the "/" but there are cases where the dates are further malformed so this is safer.
    ije_field_string = "99"
  elsif ije_key == 'DOI_MO' && !is_number?(ije_field_string) && ije_field_string != ""
    # Sometimes "Date of Injury Month" is in a 2-char form "Ap", "Ma", etc rather than a number.
    # Use the first 3 chars of the `Injury date` field to convert that month data to valid integer.
    ije_field_string = Date::ABBR_MONTHNAMES.index(data_row['Injury date'][0..4])
  elsif ije_key == 'TOD' && ije_field_string == "2400"
    # "2400" is not a valid IJE TOD value. Set to "2359" for one minute previous.
    ije_field_string = "2359"
  elsif ije_key == 'DSTATE' && ije_field_string == ""
    # Towards the end of the dataset, it stops including DSTATE.
    # Add it back as "WA"
    ije_field_string = "WA"
  end
  return ije_field_string
end

# Formats the given ije data based on length and whether it is a number or text.
def format_ije_data(ije_field_string, ije_length)
  # Truncate and warn of overlong IJE field values.
  if ije_field_string.length > ije_length
    # puts "Record contains an overlong data field `#{ije_field_string}`. Expected `#{ije_length}`. Truncating."
    # Truncate an overlong IJE data field.
    ije_field_string = ije_field_string[0,ije_length]
  end
  if ije_field_string == "."
    # In some cases, the datasets contain "." for blank fields. Convert them to blank.
    ije_field_string = ""
  end
  # Add padding spaces to the string according to its length.
  if is_number?(ije_field_string)
    # Numbers are right justified with 0s.
    ije_field_string = ije_field_string.rjust(ije_length, '0')
  else
    # Strings are left justified.
    ije_field_string = ije_field_string.ljust(ije_length, ' ')
  end
end

# Returns whether a given string is numeric.
def is_number?(string)
  true if Float(string) rescue false
end

# Converts an input data sheet into IJE records based on the given mappings of IJE->datafile_headers.
# ije_data_mappings: The file with the mappings of IJE records to input dataset headers. Includes IJE lengths and locations.
# data_stat_sheet: Input statistical tabular dataset. Expects Excel.
# data_lit: Input literal tabular dataset. Expects CSV.
# num_records: The number of records to convert and export.
def convert_ije_records(ije_data_mappings, data_stat_sheet, data_lit, county_mappings, num_records, data_year, jurisdiction, first_cert_number, first_record_number)

  # Array of IJE record strings.
  ije_records = Array.new { '' }
  # Skip column headers row flag.
  skip_headers = true
  # Iterate over the input data sheet which contains all the excel-based death records.
  data_stat_sheet.simple_rows.each_with_index do |data_row, index|
    # Skip column headers row.
    if skip_headers
      skip_headers = false
      next
    end

    # Skip appropriate number of rows if we're not starting at the first record number
    if first_record_number && index < first_record_number
      puts "SKIPPING ROW #{index}"
      next
    end

    if ije_records.length >= num_records
      # Generated the requested number of records, stop generating more.
      break;
    end

    skip_mappings_headers = true
    # Initialize this record's IJE record.
    ije_record_string = ' ' * 5000
    # Track this record's middle name.
    middle_name = nil
    ije_data_mappings.simple_rows.each do |mapping_row|
      # Skip column headers row.
      if skip_mappings_headers
        skip_mappings_headers = false
        next
      end
      if mapping_row['Input Data Field Name'] == '?' && mapping_row['default'] == 'blank'
        # Skip this mapping since there's no IJE-data mapping and no default value.
        next
      end
      # The IJE field key/header.
      ije_key = mapping_row['IJE Field Name']
      # Initialize this IJE field string.
      ije_field_string = nil
      if mapping_row['Input Data Field Name'] == 'faker'
        # If this is a faked case, set the ije field string to a faked value.
        ije_field_string = generate_faked_value(ije_key)
      else
        if !mapping_row.key?('Input Data Field Name')
          puts("ERROR: IJE Field `#{ije_key}` has not defined an input data header to map to.")
        end
        datafile_header = mapping_row['Input Data Field Name']
        if mapping_row['file'] == 'stat'
          # Perform the mapping for this ije key using the stat dataset.
          if datafile_header.start_with?('[')
            # If this input has multiple mappings, iterate over and append it for this IJE field value.
            ije_field_string = ''
            datafile_header = datafile_header.tr('[]', '')
            datafile_header.split(',').each do |header|
              ije_length = mapping_row['Length'].to_i()
              raw_ije = parse_ije_field(data_row, header, ije_key, mapping_row, county_mappings, ije_record_string)
              length = raw_ije.length()
              if ije_key == "TOD"
                length = 2
              end
              ije_field_string = ije_field_string + format_ije_data(raw_ije, length)
            end
            ije_field_string = catch_invalid_values(ije_key, ije_field_string, mapping_row, data_row, county_mappings, ije_record_string)
          else
            # If it does not have multiple mappings, treat it as a single mapping.
            ije_field_string = parse_ije_field(data_row, datafile_header, ije_key, mapping_row, county_mappings, ije_record_string)
          end
        elsif mapping_row['file'] == 'lit'
          # Perform the mapping for this ije key using the literal dataset.
          # Since this data is in the literal data file, get the record number and extract the relevant row from the literal data.
          record_num = data_row['State file number']
          lit_data_row = data_lit.select { |row| row['State File Number'] == record_num }[0]
          ije_field_string = parse_ije_field(lit_data_row, datafile_header, ije_key, mapping_row, county_mappings, ije_record_string)
        elsif datafile_header == '?' && mapping_row['default'] != nil
          # If there is a default value due to a lack of mapping in the file, use it.
          ije_field_string = mapping_row['default']
          if ije_field_string == 'blank'
            ije_field_string = ''
          end
        elsif mapping_row['special value mapping'] != nil
          ije_field_string = catch_invalid_values(ije_key, '', mapping_row, data_row, county_mappings, ije_record_string)
        else
          puts("ERROR: IJE Field mapping `#{ije_key}` does not include a file to map to and has no default value.")
        end
      end
      # Convert the fileno field data.
      if ije_key == 'FILENO'
        # Remove the first 4 digits which are the year.
        ije_field_string = ije_field_string[4, ije_field_string.length]
      end
      # Account for consistent middle name.
      if ije_key == 'MNAME' || ije_key == 'DMIDDLE'
        if middle_name == nil
          middle_name = ije_field_string
        else
          ije_field_string = middle_name
        end
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
    original_record_identifier = ije_record_string[0..11]
    # Use specified data year, jurisdiction, and starting certificate number if provided
    if data_year
      ije_record_string[0..3] = data_year[0,4]
    end
    if jurisdiction
      ije_record_string[4..5] = jurisdiction[0,2]
    end
    if first_cert_number
      ije_record_string[6..11] = '%06d' % (index + first_cert_number - first_record_number)
    end
    # Pad the string to the required 5000 characters
    ije_record_string = ije_record_string.ljust(5000)
    puts("Parsed Record #{original_record_identifier} recorded as #{ije_record_string[0..11]}")
    ije_records.push(ije_record_string)
  end
  return ije_records
end

# Exports the given IJE records to the given output directory.
def export_records(ije_records, output_dir)
  # VRDR CLI path.
  cli_path = File.expand_path(File.join(__dir__, '..', 'VRDR.CLI'))
  # Beginning of VRDR command which will be appended with ije string file names.
  vrdr_command = "dotnet run --project #{cli_path} ije2json"
  puts("Starting export of IJE strings to IJE files for #{ije_records.length()} records.")
  ije_records.each do |ije_record|
    record_num = ije_record[0..11]
    ije_file_name = "#{output_dir}#{record_num}.ije"
    File.write(ije_file_name, ije_record)
    vrdr_command = vrdr_command + " " + ije_file_name
  end
  puts("IJE record exporting complete.")
  puts("Starting conversion and export of IJE files to FHIR JSONs to directory #{output_dir}.")
  # Export and convert to FHIR ije record file using vrdr ije2json.
  # Execute the vrdr ije2json command.
  system(vrdr_command)
  puts("Finished converting and exporting tabular data to FHIR Death Records.")
end

puts("Starting data parsing and converting...")
# Convert records to IJE.
ije_records = convert_ije_records(ije_data_mappings.sheets[0], data.sheets[0], data_lit, county_mappings, num_records.to_i, data_year, jurisdiction, first_cert_number, first_record_number)
# Export records to FHIR.
export_records(ije_records, output_dir)
puts("Conversion and exporting complete.")