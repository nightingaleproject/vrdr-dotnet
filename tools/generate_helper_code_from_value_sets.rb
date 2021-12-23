
=begin
This ruby script reads an enumerated set of Valueset files (keys in the 'valuesets' hash) and generates a single CSharp file (stdout) that
includes a class describing each valueset (named according to the values in the 'valuesets' hash).

Currently, the script assumes a FSH/SUSHI directory structure, with the valuesets being in the fsh-generated/resource subdirectory.
This should probably be changed to assume the content is in the flat structure that is part of an IG download.   This change will be made
when the VRDR IG has been rebuilt to include all of the necessary valuesets.

All references to codesystems use the definitions in VRDR.CodeSystems.


Inputs:   <input directory of an IG download> <output directory for file ValueSets.cs>
Output:   ValueSets.cs in the specified directory

Directions:
  1) Download Saul's version of the VRDR IG (for now) -- http://build.fhir.org/ig/saulakravitz/vrdr/branches/FSHversion/full-ig.zip .   Once there is a stable build of the VRDR
     IG, this script will need to be updated with the names of the relevant valuesets in the valuesets hash
  2) Unzip the file
  3) use the resulting full-ig directory as the first argument for this script
  4) decide where you want the output generated, and use that as the second argument for this script

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
    "ValueSet-PHVS-TransportationRelationships-NCHS.json" => "TransportationRoles",
    "ValueSet-PHVS-PregnancyStatus-NCHS.json" => "PregnancyStatus",
    "ValueSet-PHVS-MethodsOfDisposition-NCHS.json" => "MethodsOfDisposition",
    "ValueSet-PHVS-CertifierTypes-NCHS.json" => "CertificationRole"
}
basedir = ARGV[0]
outfilename = ARGV[1] + "/ValueSets.cs"
file = file=File.open(outfilename,"w")

file.puts "namespace VRDR
{
    /// <summary> ValueSet Helpers </summary>
    public static class ValueSets"
file.puts "    {"
valuesets.each do |vsfile, fieldname|
        filename = ARGV[0] + "/site/" + vsfile
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
                display = concept["display"].gsub("/"," ")
                display = display.gsub(","," ")
                display = display.gsub(";"," ")
                display = display.gsub("'","")
                display = display.split(" ").map(&:capitalize).join("_")
                if display[0][/\d/] then display = "_" + display end
                file.puts "            /// <summary> #{display} </summary>"
                file.puts "            public static string  #{display} = \"#{concept["code"]}\";"
            end
        }
        file.puts "        };"

    end
file.puts "   }
}"