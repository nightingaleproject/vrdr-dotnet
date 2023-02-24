# Tools

This directory contains some small tools useful for maintaining the VRDR .NET library and working
with it to generate or submit data. Tool usage is documented within the source file for each tool.

* convert_NCHS_DACEB_pilot_data_to_fhir.rb - takes an excel file from NCHS and converts it to FHIR records

* convert_tabular_data_to_fhir_death_records.rb - takes statistical and literal tabular death datasets and converts them to IJE

* generate_concept_mappings_from_VRDR_IG.rb - takes the concept map JSON files that are generated as part of the VRDR IG and creates an output file with mapping dictionaries used for IJE <-> FHIR value set translation

* generate_connectathon_testcases.rb - takes an excel file provided by NCHS and produces C# code for inclusion in the VRDR dot net library

* generate_race_ethnicity_methods.rb - generates code for all the race and ethnicity methods in DeathRecord.cs

* generate_url_strings_from_VRDR_IG.rb - takes the JSON files that are generated as part of the VRDR IG and creates an output file with static URL strings for each StructureDefinition, Extension, and IG HTML page

* generate_value_set_lookups_from_VRDR_IG.rb - reads ValueSet files and generates a C# file with a class describing each ValueSet

* manual_api_testing - a directory containing additional tools for manually testing a VRDR FHIR API
