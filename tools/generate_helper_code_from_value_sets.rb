
=begin
This ruby script reads an enumerated set of Valueset files (keys in the 'valuesets' hash) and generates a single CSharp file (stdout) that
includes a class describing each valueset (named according to the values in the 'valuesets' hash).

Currently, the script assumes a FSH/SUSHI directory structure, with the valuesets being in the fsh-generated/resource subdirectory.
This should probably be changed to assume the content is in the flat structure that is part of an IG download.   This change will be made
when the VRDR IG has been rebuilt to include all of the necessary valuesets.

All references to codesystems use the definitions in VRDR.CodeSystems.


Input: FHIR ValueSet file in JSON format.   For example:  http://build.fhir.org/ig/saulakravitz/vrdr/branches/FSHversion/ValueSet-PHVS-PlaceOfDeath-NCHS.json

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
    "http://terminology.hl7.org/CodeSystem/v3-NullFlavor" => "VRDR.CodeSystems.PH_NullFlavor_HL7_V3",
    "http://terminology.hl7.org/CodeSystem/v3-MaritalStatus" => "VRDR.CodeSystems.PH_MaritalStatus_HL7_2x",
    "urn:oid:2.16.840.1.114222.4.5.320" => "VRDR.CodeSystems.PH_PlaceOfOccurrence_ICD_10_WHO",
    "http://hl7.org/fhir/us/vrdr/CodeSystem/PH-PHINVS-CDC" =>  "VRDR.CodeSystems.PH_PHINVS_CDC"
}
valuesets = {
    "ValueSet-PHVS-DecedentEducationLevel-NCHS.json" => "EducationLevel",
    "ValueSet-PHVS-MannerOfDeath-NCHS.json" => "MannerOfDeath",
    "ValueSet-PHVS-MaritalStatus-NCHS.json" => "MaritalStatus",
    "ValueSet-PHVS-PlaceOfInjury-NCHS.json" => "PlaceOfInjury",
    "ValueSet-PHVS-PlaceOfDeath-NCHS.json" => "PlaceOfDeath",
    "ValueSet-PHVS-TransportationRelationships-NCHS.json" => "TransportationRelationships",
    "ValueSet-PHVS-PregnancyStatus-NCHS.json" => "PregnancyStatus",
    "ValueSet-PHVS-MethodsOfDisposition-NCHS.json" => "MethodsOfDisposition"
}
basedir = ARGV[0]

puts "namespace VRDR
    {
         /// <summary> ValueSet Helpers </summary>
         public static class ValueSets"
puts "    {"
valuesets.each do |vsfile, fieldname|
        filename = ARGV[0] + "/fsh-generated/resources/" + vsfile
        value_set_data = JSON.parse(File.read(filename))
        puts "            /// <summary> #{fieldname} </summary>
            public static class #{fieldname} {
                /// <summary> Codes </summary>
                public static string[,] Codes = {"
        groups = value_set_data["compose"]["include"]
        first = true
        groups.each { | group |
            system = group["system"]
            if codesystems[system]
                system = codesystems[system]
            end
            for concept in group["concept"]
                puts "," if first == false
                first = false
                print "                    { \"#{concept["code"]}\", \"#{concept["display"]}\", #{system} }"
            end
        }
        puts "\n            };"
        groups.each { | group |
            system = group["system"]
            if codesystems[system]
                system = codesystems[system]
            end
            for concept in group["concept"]
                display = concept["display"].gsub("/"," ")
                display = display.gsub(","," ")
                display = display.gsub(";"," ")
                display = display.gsub("'","")
                display = display.split(" ").map(&:capitalize).join("_")
                if display[0][/\d/] then display = "_" + display end
                puts "            /// <summary> #{display} </summary>"
                puts "            public static string  #{display} = \"#{concept["code"]}\";"
            end
        }
        puts "        };"

    end
puts "   }
}"