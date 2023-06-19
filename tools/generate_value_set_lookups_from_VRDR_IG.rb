=begin
This ruby script reads an enumerated set of Valueset files (keys in the 'valuesets' hash) and generates a
single CSharp file in the specified location that includes a class describing each valueset (named according
to the values in the 'valuesets' hash).

Currently, the script assumes a FSH/SUSHI directory structure, with the valuesets being in the
fsh-generated/resource subdirectory. This should probably be changed to assume the content is in the flat
structure that is part of an IG download. This change will be made when the VRDR IG has been rebuilt to
include all of the necessary valuesets.

All references to codesystems use the definitions in VRDR.CodeSystems.

Inputs:   <input directory of an IG download> <output directory for file ValueSets.cs>
Output:   ValueSets.cs in the specified directory

Directions:
  1) Install sushi (https://github.com/FHIR/sushi)
  1) Download and build the IG:

     git clone https://github.com/HL7/vrdr.git
     cd vrdr
     sushi

  3) use the resulting fsh-generated directory as the first argument for this script
  4) decide where you want the output generated, and use that as the second argument for this script

Example run: ruby tools/generate_value_set_lookups_from_VRDR_IG.rb ../vrdr/fsh-generated/ VRDR/

Example of generated output:

namespace VRDR
{
    public static class ValueSets
    {
        public static class DeathLocationType {
            public static string[,] Codes = {
                { "63238001", "Hospital Dead on Arrival", VRDR.CodeSystems.SCT },
                { "440081000124100", "Decedent's Home", VRDR.CodeSystems.SCT },
                { "440071000124103", "Hospice", VRDR.CodeSystems.SCT },
                { "16983000", "Hospital Inpatient", VRDR.CodeSystems.SCT },
                { "450391000124102", "Death in emergency Room/Outpatient", VRDR.CodeSystems.SCT },
                { "450381000124100", "Death in nursing home/Long term care facility", VRDR.CodeSystems.SCT },
                { "OTH", "Other (Specify)", CodeSystems.PH_NullFlavor_HL7_V3 },
                { "UNK", "Unknown", CodeSystems.PH_NullFlavor_HL7_V3 }
            };
            public static string Hospital_Dead_on_Arrival = "63238001";
            public static string Decedents_Home = "440081000124100";
            ...
        }
    }
}
=end
require 'json'
require 'pry'
codesystems = {
    "http://snomed.info/sct" => "VRDR.CodeSystems.SCT",
    "http://terminology.hl7.org/CodeSystem/v3-NullFlavor" => "VRDR.CodeSystems.NullFlavor_HL7_V3",
    "http://terminology.hl7.org/CodeSystem/v3-MaritalStatus" => "VRDR.CodeSystems.PH_MaritalStatus_HL7_2x",
    "http://hl7.org/fhir/administrative-gender" => "VRDR.CodeSystems.AdministrativeGender",
    "http://hl7.org/fhir/us/vrdr/CodeSystem/vrdr-bypass-edit-flag-cs" => "VRDR.CodeSystems.BypassEditFlag",
    "http://terminology.hl7.org/CodeSystem/v3-EducationLevel" => "VRDR.CodeSystems.EducationLevel",
    "http://hl7.org/fhir/us/vrdr/CodeSystem/vrdr-filing-format-cs" => "VRDR.CodeSystems.FilingFormat",
    "http://terminology.hl7.org/CodeSystem/v2-0360" => "VRDR.CodeSystems.DegreeLicenceAndCertificate",
    "http://hl7.org/fhir/us/vrdr/CodeSystem/vrdr-pregnancy-status-cs" => "VRDR.CodeSystems.PregnancyStatus",
    "http://hl7.org/fhir/us/vrdr/CodeSystem/vrdr-replace-status-cs" => "VRDR.CodeSystems.ReplaceStatus",
    "http://hl7.org/fhir/us/vrdr/CodeSystem/vrdr-missing-value-reason-cs" => "VRDR.CodeSystems.MissingValueReason",
    "http://unitsofmeasure.org" => "VRDR.CodeSystems.UnitsOfMeasure",
    "http://terminology.hl7.org/CodeSystem/v2-0136" => "VRDR.CodeSystems.YesNo",
    "http://hl7.org/fhir/us/vrdr/CodeSystem/vrdr-activity-at-time-of-death-cs" => "VRDR.CodeSystems.ActivityAtTimeOfDeath",
    "http://loinc.org" => "VRDR.CodeSystems.LOINC",
    "http://hl7.org/fhir/us/vrdr/CodeSystem/vrdr-race-code-cs" => "VRDR.CodeSystems.RaceCode",
    "http://hl7.org/fhir/us/vrdr/CodeSystem/vrdr-race-recode-40-cs" => "VRDR.CodeSystems.RaceRecode40",
    "http://hl7.org/fhir/us/vrdr/CodeSystem/vrdr-hispanic-origin-cs" => "VRDR.CodeSystems.HispanicOrigin",
    "http://hl7.org/fhir/us/vrdr/CodeSystem/vrdr-intentional-reject-cs" => "VRDR.CodeSystems.IntentionalReject",
    "http://hl7.org/fhir/us/vrdr/CodeSystem/vrdr-system-reject-cs" => "VRDR.CodeSystems.SystemReject",
    "http://hl7.org/fhir/us/vrdr/CodeSystem/vrdr-transax-conversion-cs" => "VRDR.CodeSystems.TransaxConversion",
}

valuesets = {
    "ValueSet-vrdr-activity-at-time-of-death-vs.json" => "ActivityAtTimeOfDeath",
    "ValueSet-vrdr-place-of-injury-vs.json" => "PlaceOfInjury",
    "ValueSet-vrdr-administrative-gender-vs.json" => "AdministrativeGender",
    "ValueSet-vrdr-certifier-types-vs.json" => "CertifierTypes",
    "ValueSet-vrdr-contributory-tobacco-use-vs.json" => "ContributoryTobaccoUse",
    "ValueSet-vrdr-edit-bypass-01-vs.json" => "EditBypass01",
    "ValueSet-vrdr-edit-bypass-012-vs.json" => "EditBypass012",
    "ValueSet-vrdr-edit-bypass-01234-vs.json" => "EditBypass01234",
    "ValueSet-vrdr-edit-bypass-0124-vs.json" => "EditBypass0124",
    "ValueSet-vrdr-education-level-vs.json" => "EducationLevel",
    "ValueSet-vrdr-filing-format-vs.json" => "FilingFormat",
    "ValueSet-vrdr-manner-of-death-vs.json" => "MannerOfDeath",
    "ValueSet-vrdr-marital-status-vs.json" => "MaritalStatus",
    "ValueSet-vrdr-method-of-disposition-vs.json" => "MethodOfDisposition",
    "ValueSet-vrdr-place-of-death-vs.json" => "PlaceOfDeath",
    "ValueSet-vrdr-pregnancy-status-vs.json" => "PregnancyStatus",
    "ValueSet-vrdr-race-missing-value-reason-vs.json" => "RaceMissingValueReason",
    "ValueSet-vrdr-replace-status-vs.json" => "ReplaceStatus",
    "ValueSet-vrdr-transportation-incident-role-vs.json" => "TransportationIncidentRole",
    "ValueSet-vrdr-units-of-age-vs.json" => "UnitsOfAge",
    "ValueSet-vrdr-yes-no-unknown-not-applicable-vs.json" => "YesNoUnknownNotApplicable",
    "ValueSet-vrdr-yes-no-unknown-vs.json" => "YesNoUnknown",
    "ValueSet-vrdr-race-code-vs.json" => "RaceCode",
    "ValueSet-vrdr-race-recode-40-vs.json" => "RaceRecode40",
    "ValueSet-vrdr-hispanic-origin-vs.json" => "HispanicOrigin",
    "ValueSet-vrdr-transax-conversion-vs.json" => "TransaxConversion",
    "ValueSet-vrdr-system-reject-vs.json" => "AcmeSystemReject",
    "ValueSet-vrdr-intentional-reject-vs.json" => "IntentionalReject",
    "ValueSet-vrdr-spouse-alive-vs.json" => "SpouseAlive",
    "ValueSet-vrdr-hispanic-no-unknown-vs.json" => "HispanicNoUnknown",
}
# These are special cases that we want to shorten the string produced by the general approach, for practical reasons
special_cases =
    {
    "Medical_Examiner_Coroner_On_The_Basis_Of_Examination_And_Or_Investigation_In_My_Opinion_Death_Occurred_At_The_Time_Date_And_Place_And_Due_To_The_Cause_S_And_Manner_Stated" => "Medical_Examiner_Coroner",
    "Pronouncing_Certifying_Physician_To_The_Best_Of_My_Knowledge_Death_Occurred_At_The_Time_Date_And_Place_And_Due_To_The_Cause_S_And_Manner_Stated" => "Pronouncing_Certifying_Physician",
    "Certifying_Physician_To_The_Best_Of_My_Knowledge_Death_Occurred_Due_To_The_Cause_S_And_Manner_Stated" => "Certifying_Physician"
}

outfilename = ARGV[1] + "/ValueSets.cs"
puts "Output in #{outfilename}"
file = file=File.open(outfilename,"w")
systems_without_constants = []

file.puts "// DO NOT EDIT MANUALLY! This file was generated by the script \"#{__FILE__}\"

namespace VRDR
{
    /// <summary> ValueSet Helpers </summary>
    public static class ValueSets"
file.puts "    {"
valuesets.each do |vsfile, fieldname|
        puts "Generating output for #{vsfile}"
        filename = ARGV[0] + "/resources/" + vsfile
        value_set_data = JSON.parse(File.read(filename))
        file.puts "        /// <summary> #{fieldname} </summary>
        public static class #{fieldname} {
            /// <summary> Codes </summary>
            public static string[,] Codes = {"
        groups = value_set_data["compose"]["include"]
        first = true
        groups.each { | group |
            system = group["system"]
            if codesystems[system]
                system = codesystems[system]
            else
              systems_without_constants << system
            end
            for concept in group["concept"]
                file.puts "," if first == false
                first = false
                file.print "                { \"#{concept["code"]}\", \"#{concept["display"]}\", #{system} }"
            end
        }
        file.puts "\n            };"
        groups.each { | group |
            system = group["system"]
            if codesystems[system]
                system = codesystems[system]
            end
            for concept in group["concept"]
                display = concept["display"].gsub("'", '').split(/[^a-z0-9]+/i).map(&:capitalize).join('_')
                if display[0][/\d/] then display = "_" + display end
                if special_cases[display] != nil then display = special_cases[display] end
                file.puts "            /// <summary> #{display} </summary>"
                file.puts "            public static string  #{display} = \"#{concept["code"]}\";"
            end
        }
        file.puts "        };"

    end
file.puts "   }
}"

puts
puts "Saw the following code systems that don't have constants:"
puts
puts systems_without_constants.uniq
puts
puts "Suggestions:"
puts
systems_without_constants.uniq.each do |system|
  puts "        /// <summary> #{system} </summary>"
  puts "        public static string XYZ = \"#{system}\";"
end
