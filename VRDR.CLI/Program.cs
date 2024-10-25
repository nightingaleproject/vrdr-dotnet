using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Reflection;
using System.Net.Http;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.ElementModel;
using Hl7.FhirPath;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using VRDR;

namespace VRDR.CLI
{
    class Program
    {
        static string commands =
@"* VRDR Command Line Interface - commands
  - help:  prints this help message  (no arguments)
  - fakerecord: prints a fake XML death record (no arguments)
  - connectathon: prints one of the 3 connectathon records (3 arguments: the number (1,2, or 3) of the connectathon record, the certificate number, and the jurisdiction)
  - description: prints a verbose JSON description of the record (in the format used to drive Canary) (1 argument: the path to the death record)
  - 2ije: Read in the FHIR XML or JSON death record and print out as IJE (1 argument: path to death record in JSON or XML format)
  - 2ijecontent: Read in the FHIR XML or JSON death record and dump content  in key/value IJE format (1 argument: path to death record in JSON or XML format)
  - ack: Create an acknowledgement FHIR message for a submission FHIR message (1 argument: submission FHIR message; many arguments: output directory and FHIR messages)
  - checkJson: Read in the given FHIR json (being permissive) and print out the same; useful for doing validation diffs (1 argument: FHIR JSon file)
  - checkXml: Read in the given FHIR xml (being permissive) and print out the same; useful for doing validation diffs (1 argument: FHIR XML file)
  - compare: Compare an IJE record with a FHIR record by each IJE field (2 arguments:  IJE record, FHIR Record)
  - compareMREtoJSON: Compare a MRE file to a Coded Demographic Bundle (2 arguments: MRE file, FHIR Record)
  - compareTRXtoJSON: Compare a TRX file to a Coded Cause of Death Bundle (2 arguments: TRX file, FHIR Record)
  - connectathon: prints one of the 3 connectathon records (1 argument: the number (1,2, or 3) of the connectathon record)
  - description: prints a verbose JSON description of the record (in the format used to drive Canary) (1 argument: the path to the death record)
  - extract2ijecontent: Dump content of a submission message in key/value IJE format (1 argument: submission message)
  - extract: Extract a FHIR record from a FHIR message (1 argument: FHIR message)
  - fakerecord: prints a fake XML death record (no arguments)
  - generaterecords: Generate records for bulk testing based on the 3 connectathon testing records. (4+ arguments: initial certificate number, number of records to generate (each with cert_no one greater than its predecessor), submitting jurisdiction, output directory (must exist), optional year)
  - ije2json: Read in the IJE death record and print out as JSON (1 argument: path to death record in IJE format)
  - ije2xml: Read in the IJE death record and print out as XML (1 argument: path to death record in IJE format)
  - ije: Read in and parse an IJE death record and print out the values for every (supported) field (1 argument: path to death record in IJE format)
  - json2json: Read in the FHIR JSON death record, completely disassemble then reassemble, and print as FHIR JSON (1 argument: FHIR JSON Death Record)
  - json2mre: Read in the FHIR JSON COded Demographic Bundle, write MRE file (1 argument: FHIR JSON Demographic Bundle)
  - json2trx: Read in the FHIR JSON COded Cause of Death Bundle, write TRX file (1 argument: FHIR JSON Coded Cause Of Death Bundle)
  - json2xml: Read in the FHIR JSON death record, completely disassemble then reassemble, and print as FHIR XML (1 argument: FHIR JSON Death Record)
  - mre2json: Creates a Demographic Coding Bundle from a MRE Message (1 argument: TRX file)
  - resubmit: Create an update FHIR message wrapping a FHIR death record (1 argument:  FHIR death record)
  - roundtrip-all: Convert a record to JSON and back and check field by field to identify any conversion issues (1 argument: FHIR Death Record)
  - roundtrip-ije: Convert a record to IJE and back and check field by field to identify any conversion issues (1 argument: FHIR Death Record)
  - showcodes: Extract and show the codes in a coding response message (1 argument: coding response message)
  - submit: Create a submission FHIR message wrapping a FHIR death record (1 argument: FHIR death record; many arguments: output directory and FHIR death records)
  - alias: Create an alias FHIR message for a FHIR death record (1 argument: FHIR death record)
  - showalias: Read in an alias FHIR message and display the contents (1 argument: FHIR alias message)
  - toMortalityRoster: Create and print a mortality roster bundle from a death record (1 argument: FHIR death record)
  - trx2json: Creates a Cause of Death Coding Bundle from a TRX Message (1 argument: TRX file)
  - void: Creates a Void message for a Death Record (1 argument: FHIR death record; one optional argument: number of records to void)
  - xml2json: Read in the IJE death record and print out as JSON (1 argument: path to death record in XML format)
  - xml2xml: Read in the IJE death record and print out as XML (1 argument: path to death record in XML format)
  - batch: Read in IJE messages and create a batch submission bundle (2+ arguments: submission URL (for inside bundle) and one or more messages)
  - filter: Read in the FHIR death record and filter based on filter array (1 argument: path to death record to filter)
";
        static int Main(string[] args)
        {
            if ((args.Length == 0) || ((args.Length == 1) && (args[0] == "help")))
            {
                Console.WriteLine(commands);
                return (0);
            }
            else if (args.Length == 1 && args[0] == "fakerecord")
            {
                DeathRecord deathRecord = new DeathRecord();

                // Identifier
                deathRecord.Identifier = "1";

                // CertifiedTime
                deathRecord.CertifiedTime = "2019-01-29T16:48:06-05:00";

                // RegisteredTime
                deathRecord.RegisteredTime = "2019-02-01T16:47:04-05:00";

                // CertificationRole
                Dictionary<string, string> certificationRole = new Dictionary<string, string>();
                certificationRole.Add("code", "434641000124105");
                certificationRole.Add("system", "http://snomed.info/sct");
                certificationRole.Add("display", "Physician");
                deathRecord.CertificationRole = certificationRole;

                // State Local Identifier
                deathRecord.StateLocalIdentifier1 = "000000000042";

                // MannerOfDeathType
                Dictionary<string, string> mannerOfDeathType = new Dictionary<string, string>();
                mannerOfDeathType.Add("code", "7878000");
                mannerOfDeathType.Add("system", "http://snomed.info/sct");
                mannerOfDeathType.Add("display", "Accidental death");
                deathRecord.MannerOfDeathType = mannerOfDeathType;

                // CertifierGivenNames
                string[] cnames = { "Doctor", "Middle" };
                deathRecord.CertifierGivenNames = cnames;

                // CertifierFamilyName
                deathRecord.CertifierFamilyName = "Last";

                // CertifierSuffix
                deathRecord.CertifierSuffix = "Jr.";

                // CertifierAddress
                Dictionary<string, string> caddress = new Dictionary<string, string>();
                caddress.Add("addressLine1", "11 Example Street");
                caddress.Add("addressLine2", "Line 2");
                caddress.Add("addressCity", "Bedford");
                caddress.Add("addressCounty", "Middlesex");
                caddress.Add("addressState", "MA");
                caddress.Add("addressZip", "01730");
                caddress.Add("addressCountry", "US");
                deathRecord.CertifierAddress = caddress;

                Dictionary<string, string> certifierIdentifier = new Dictionary<string, string>();
                certifierIdentifier.Add("system", "http://hl7.org/fhir/sid/us-npi");
                certifierIdentifier.Add("value", "1234567890");
                deathRecord.CertifierIdentifier = certifierIdentifier;

                // // CertifierLicenseNumber
                // deathRecord.CertifierLicenseNumber = "789123456";

                // ContributingConditions
                deathRecord.ContributingConditions = "Example Contributing Conditions";

                // COD1A
                deathRecord.COD1A = "Rupture of myocardium";

                // INTERVAL1A
                deathRecord.INTERVAL1A = "minutes";

                // // CODE1A
                // Dictionary<string, string> code1a = new Dictionary<string, string>();
                // code1a.Add("code", "I21.0");
                // code1a.Add("system", "http://hl7.org/fhir/sid/icd-10");
                // code1a.Add("display", "Acute transmural myocardial infarction of anterior wall");
                // deathRecord.CODE1A = code1a;

                // COD1B
                deathRecord.COD1B = "Acute myocardial infarction";

                // INTERVAL1B
                deathRecord.INTERVAL1B = "6 days";

                // // CODE1B
                // Dictionary<string, string> code1b = new Dictionary<string, string>();
                // code1b.Add("code", "I21.9");
                // code1b.Add("system", "http://hl7.org/fhir/sid/icd-10");
                // code1b.Add("display", "Acute myocardial infarction, unspecified");
                // deathRecord.CODE1B = code1b;

                // COD1C
                deathRecord.COD1C = "Coronary artery thrombosis";

                // INTERVAL1C
                deathRecord.INTERVAL1C = "5 years";

                // COD1D
                deathRecord.COD1D = "Atherosclerotic coronary artery disease";

                // INTERVAL1D
                deathRecord.INTERVAL1D = "7 years";

                // GivenNames
                deathRecord.GivenNames = new string[] { "Example", "Something", "Middle" };

                // FamilyName
                deathRecord.FamilyName = "Last";

                // Suffix
                deathRecord.Suffix = "Jr.";

                // Gender
                deathRecord.SexAtDeathHelper = ValueSets.AdministrativeGender.Male;

                // BirthSex
                //deathRecord.BirthSex = "F";

                // DateOfBirth
                deathRecord.DateOfBirth = "1940-02-19";

                // Residence
                Dictionary<string, string> raddress = new Dictionary<string, string>();
                raddress.Add("addressLine1", "101 Example Street");
                raddress.Add("addressLine2", "Line 2");
                raddress.Add("addressCity", "Bedford");
                raddress.Add("addressCounty", "Middlesex");
                raddress.Add("addressState", "MA");
                raddress.Add("addressZip", "01730");
                raddress.Add("addressCountry", "US");
                deathRecord.Residence = raddress;

                // ResidenceWithinCityLimits
                deathRecord.ResidenceWithinCityLimitsHelper = ValueSets.YesNoUnknown.No;

                // SSN
                deathRecord.SSN = "123456789";

                // Ethnicity
                deathRecord.Ethnicity2Helper = ValueSets.HispanicNoUnknown.Yes;

                // Race
                Tuple<string, string>[] race = { Tuple.Create(NvssRace.White, "Y"), Tuple.Create(NvssRace.NativeHawaiian, "Y"), Tuple.Create(NvssRace.OtherPacificIslander, "Y") };
                deathRecord.Race = race;

                // MaritalStatus
                Dictionary<string, string> mscode = new Dictionary<string, string>();
                mscode.Add("code", "S");
                mscode.Add("system", "http://terminology.hl7.org/CodeSystem/v3-MaritalStatus");
                mscode.Add("display", "Never Married");
                deathRecord.MaritalStatus = mscode;

                // FatherGivenNames
                string[] fnames = { "Father", "Middle" };
                deathRecord.FatherGivenNames = fnames;

                // FatherFamilyName
                deathRecord.FatherFamilyName = "Last";

                // FatherSuffix
                deathRecord.FatherSuffix = "Sr.";

                // MotherGivenNames
                string[] mnames = { "Mother", "Middle" };
                deathRecord.MotherGivenNames = mnames;

                // MotherMaidenName
                deathRecord.MotherMaidenName = "Maiden";

                // MotherSuffix
                deathRecord.MotherSuffix = "Dr.";

                // SpouseGivenNames
                string[] spnames = { "Spouse", "Middle" };
                deathRecord.SpouseGivenNames = spnames;

                // SpouseFamilyName
                deathRecord.SpouseFamilyName = "Last";

                // SpouseSuffix
                deathRecord.SpouseSuffix = "Ph.D.";

                // EducationLevel
                Dictionary<string, string> elevel = new Dictionary<string, string>();
                elevel.Add("code", "BD");
                elevel.Add("system", "http://terminology.hl7.org/CodeSystem/v3-EducationLevel");
                elevel.Add("display", "College or baccalaureate degree complete");
                deathRecord.EducationLevel = elevel;

                // BirthRecordId
                deathRecord.BirthRecordId = "4242123";

                // BirthRecordState
                deathRecord.BirthRecordState = "MA";

                // BirthRecordYear
                deathRecord.BirthRecordYear = "1940";

                // UsualOccupation
                deathRecord.UsualOccupation = "secretary";

                // UsualIndustry
                deathRecord.UsualIndustry = "State agency";

                // MilitaryService
                Dictionary<string, string> mserv = new Dictionary<string, string>();
                mserv.Add("code", "Y");
                mserv.Add("system", "http://terminology.hl7.org/CodeSystem/v2-0136");
                mserv.Add("display", "Yes");
                deathRecord.MilitaryService = mserv;

                // // MorticianGivenNames
                // string[] fdnames = { "FD", "Middle" };
                // deathRecord.MorticianGivenNames = fdnames;

                // // MorticianFamilyName
                // deathRecord.MorticianFamilyName = "Last";

                // // MorticianSuffix
                // deathRecord.MorticianSuffix = "Jr.";

                // // MorticianIdentifier
                // var mortId = new Dictionary<string, string>();
                // mortId["value"] = "9876543210";
                // mortId["system"] = "http://hl7.org/fhir/sid/us-npi";
                // deathRecord.MorticianIdentifier = mortId;

                // FuneralHomeAddress
                Dictionary<string, string> fdaddress = new Dictionary<string, string>();
                fdaddress.Add("addressLine1", "1011010 Example Street");
                fdaddress.Add("addressLine2", "Line 2");
                fdaddress.Add("addressCity", "Bedford");
                fdaddress.Add("addressCounty", "Middlesex");
                fdaddress.Add("addressState", "MA");
                fdaddress.Add("addressZip", "01730");
                fdaddress.Add("addressCountry", "US");
                deathRecord.FuneralHomeAddress = fdaddress;

                // FuneralHomeName
                deathRecord.FuneralHomeName = "Smith Funeral Home";

                // FuneralDirectorPhone
                //deathRecord.FuneralDirectorPhone = "000-000-0000";

                // DispositionLocationAddress
                Dictionary<string, string> dladdress = new Dictionary<string, string>();
                dladdress.Add("addressLine1", "603 Example Street");
                dladdress.Add("addressLine2", "Line 2");
                dladdress.Add("addressCity", "Bedford");
                dladdress.Add("addressCounty", "Middlesex");
                dladdress.Add("addressState", "MA");
                dladdress.Add("addressZip", "01730");
                dladdress.Add("addressCountry", "US");
                deathRecord.DispositionLocationAddress = dladdress;

                // DispositionLocationName
                deathRecord.DispositionLocationName = "Bedford Cemetery";

                // DecedentDispositionMethod
                Dictionary<string, string> ddm = new Dictionary<string, string>();
                ddm.Add("code", "449971000124106");
                ddm.Add("system", "http://snomed.info/sct");
                ddm.Add("display", "Burial");
                deathRecord.DecedentDispositionMethod = ddm;

                // AutopsyPerformedIndicator
                Dictionary<string, string> api = new Dictionary<string, string>();
                api.Add("code", "Y");
                api.Add("system", "http://terminology.hl7.org/CodeSystem/v2-0136");
                api.Add("display", "Yes");
                deathRecord.AutopsyPerformedIndicator = api;

                // AutopsyResultsAvailable
                Dictionary<string, string> ara = new Dictionary<string, string>();
                ara.Add("code", "Y");
                ara.Add("system", "http://terminology.hl7.org/CodeSystem/v2-0136");
                ara.Add("display", "Yes");
                deathRecord.AutopsyResultsAvailable = ara;

                // PregnancyStatus
                Dictionary<string, string> ps = new Dictionary<string, string>();
                ps.Add("code", "NA");
                ps.Add("system", "http://terminology.hl7.org/CodeSystem/v3-NullFlavor");
                ps.Add("display", "not applicable");
                deathRecord.PregnancyStatus = ps;

		        // TransportationRole
                Dictionary<string, string> tr = new Dictionary<string, string>();
                tr.Add("code", "257500003");
                tr.Add("system", "http://snomed.info/sct");
                tr.Add("display", "Passenger");
                deathRecord.TransportationRole = tr;

                // ExaminerContacted
                deathRecord.ExaminerContactedHelper = "N";

                // TobaccoUse
                Dictionary<string, string> tbu = new Dictionary<string, string>();
                tbu.Add("code", "373066001");
                tbu.Add("system", "http://snomed.info/sct");
                tbu.Add("display", "Yes");
                deathRecord.TobaccoUse = tbu;

                // InjuryLocationAddress
                Dictionary<string, string> iladdress = new Dictionary<string, string>();
                iladdress.Add("addressLine1", "781 Example Street");
                iladdress.Add("addressLine2", "Line 2");
                iladdress.Add("addressCity", "Bedford");
                iladdress.Add("addressCounty", "Middlesex");
                iladdress.Add("addressState", "MA");
                iladdress.Add("addressZip", "01730");
                iladdress.Add("addressCountry", "US");
                deathRecord.InjuryLocationAddress = iladdress;

                // InjuryLocationName
                deathRecord.InjuryLocationName = "Example Injury Location Name";

                // InjuryDescription
                deathRecord.InjuryDescription = "Example Injury Description";

                // InjuryLocationDescription
                //deathRecord.InjuryLocationDescription = "Example Injury Location Description";

                // InjuryDate
                deathRecord.InjuryDate = "2018-02-19T16:48:06-05:00";

                // InjuryAtWork
                Dictionary<string, string> codeIW = new Dictionary<string, string>();
                codeIW.Add("code", "N");
                codeIW.Add("system", "http://terminology.hl7.org/CodeSystem/v2-0136");
                codeIW.Add("display", "No");
                deathRecord.InjuryAtWork = codeIW;

                // InjuryPlace
                deathRecord.InjuryPlaceDescription = "Home";

                // DeathLocationAddress
                Dictionary<string, string> dtladdress = new Dictionary<string, string>();
                dtladdress.Add("addressLine1", "671 Example Street");
                dtladdress.Add("addressLine2", "Line 2");
                dtladdress.Add("addressCity", "Bedford");
                dtladdress.Add("addressCounty", "Middlesex");
                dtladdress.Add("addressState", "MA");
                dtladdress.Add("addressZip", "01730");
                dtladdress.Add("addressCountry", "US");
                deathRecord.DeathLocationAddress = dtladdress;

                // DeathLocationName
                deathRecord.DeathLocationName = "Example Death Location Name";

                // DeathLocationDescription
                deathRecord.DeathLocationDescription = "Example Death Location Description";

                // DeathLocationType
                Dictionary<string, string> deathLocationCode = new Dictionary<string, string>();
                deathLocationCode.Add("code", "16983000");
                deathLocationCode.Add("system", "http://snomed.info/sct");
                deathLocationCode.Add("display", "Death in hospital");
                deathRecord.DeathLocationType = deathLocationCode;

                // DeathLocationJurisdiction
                deathRecord.DeathLocationJurisdiction = "MA";

                // DateOfDeath
                deathRecord.DateOfDeath = "2018-02-19T16:48:06-05:00";

                // AgeAtDeath
                Dictionary<string, string> aad = new Dictionary<string, string>();
                aad.Add("code", "a");
                aad.Add("value", "79");
                deathRecord.AgeAtDeath = aad;

                // DateOfDeathPronouncement
                deathRecord.DateOfDeathPronouncement = "2018-02-20T16:48:06-05:00";

                // PronouncerGivenNames
                // string[] pronouncer_gnames = { "FD", "Middle" };
                // deathRecord.PronouncerGivenNames = pronouncer_gnames;

                // // PronouncerFamilyName
                // deathRecord.PronouncerFamilyName = "Last";

                // // PronouncerSuffix
                // deathRecord.PronouncerSuffix = "Jr.";

                // // PronouncerIdentifier
                // var pronouncerId = new Dictionary<string, string>();
                // pronouncerId["value"] = "0000000000";
                // pronouncerId["system"] = "http://hl7.org/fhir/sid/us-npi";
                // deathRecord.PronouncerIdentifier = pronouncerId;

                Console.WriteLine(XDocument.Parse(deathRecord.ToXML()).ToString() + "\n\n");
                //Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(Newtonsoft.Json.JsonConvert.DeserializeObject(deathRecord.ToJSON()), Newtonsoft.Json.Formatting.Indented) + "\n\n");
                return 0;
            }
            else if (args.Length == 4 && args[0] == "connectathon")
            {
                DeathRecord d = Connectathon.FromId(Int16.Parse(args[1]), Int16.Parse(args[2]), args[3]);
                Console.WriteLine(d.ToJson());
                return 0;
            }
            else if (args.Length == 2 && args[0] == "description")
            {
                DeathRecord d = new DeathRecord(File.ReadAllText(args[1]));
                Console.WriteLine(d.ToDescription());
                return 0;
            }
            else if (args.Length == 2 && args[0] == "2ijecontent")
            { // dumps content of a death record in key/value IJE format
                DeathRecord d = new DeathRecord(File.ReadAllText(args[1]));
                IJEMortality ije1 = new IJEMortality(d, false);
                // Loop over every property (these are the fields); Order by priority
                List<PropertyInfo> properties = typeof(IJEMortality).GetProperties().ToList().OrderBy(p => p.GetCustomAttribute<IJEField>().Location).ToList();
                foreach (PropertyInfo property in properties)
                {
                    // Grab the field attributes
                    IJEField info = property.GetCustomAttribute<IJEField>();
                    // Grab the field value
                    string field = Convert.ToString(property.GetValue(ije1, null));
                    // Print the key/value pair to console
                    Console.WriteLine($"{info.Name}: {field.Trim()}");
                }

                return 0;
            }
            else if (args.Length == 2 && args[0] == "2ije")
            {
                DeathRecord d = new DeathRecord(File.ReadAllText(args[1]));
                IJEMortality ije1 = new IJEMortality(d, false);
                Console.WriteLine(ije1.ToString());
                return 0;
            }
            else if (args.Length == 2 && args[0] == "ije2xml")
            {
                IJEMortality ije1 = new IJEMortality(File.ReadAllText(args[1]));
                DeathRecord d = ije1.ToDeathRecord();
                Console.WriteLine(XDocument.Parse(d.ToXML()).ToString());
                return 0;
            }
            else if (args.Length == 2 && args[0] == "ije2json")
            {
                IJEMortality ije1 = new IJEMortality(File.ReadAllText(args[1]));
                DeathRecord d = ije1.ToDeathRecord();
                Console.WriteLine(d.ToJSON());
                return 0;
            }
            else if (args.Length > 2 && args[0] == "ije2json")
            {
              // This command will export the files to the same directory they were imported from.
              for (int i = 1; i < args.Length; i++)
              {
                  string ijeFile = args[i];
                  string ijeRawRecord = File.ReadAllText(ijeFile);
                  IJEMortality ije = new IJEMortality(ijeRawRecord);
                  DeathRecord d = ije.ToDeathRecord();
                  string outputFilename = ijeFile.Replace(".ije", ".json");
                  StreamWriter sw = new StreamWriter(outputFilename);
                  sw.WriteLine(d.ToJSON());
                  sw.Flush();
              }
              return 0;
            }
            else if (args.Length == 2 && args[0] == "json2xml")
            {
                DeathRecord d = new DeathRecord(File.ReadAllText(args[1]));
                Console.WriteLine(XDocument.Parse(d.ToXML()).ToString());
                return 0;
            }
            else if (args.Length == 2 && args[0] == "checkXml")
            {
                DeathRecord d = new DeathRecord(File.ReadAllText(args[1]), true);
                Console.WriteLine(XDocument.Parse(d.ToXML()).ToString());
                return 0;
            }
            else if (args.Length == 2 && args[0] == "checkJson")
            {
                DeathRecord d = new DeathRecord(File.ReadAllText(args[1]), true);
                Console.WriteLine(d.ToJSON());
                return 0;
            }
            else if (args.Length == 2 && args[0] == "xml2json")
            {
                DeathRecord d = new DeathRecord(File.ReadAllText(args[1]));
                Console.WriteLine(d.ToJSON());
                return 0;
            }
            else if (args.Length == 2 && args[0] == "xml2xml")
            {
                // Forces record through getters and then setters, prints as xml
                DeathRecord indr = new DeathRecord(File.ReadAllText(args[1]));
                DeathRecord outdr = new DeathRecord();
                List<PropertyInfo> properties = typeof(DeathRecord).GetProperties().ToList();
                foreach (PropertyInfo property in properties)
                {
                    if (property.Name.Contains("CausesOfDeath") || property.Name.Contains("CertifierQualification"))
                    {
                        continue;
                    }
                    property.SetValue(outdr, property.GetValue(indr));
                }
                Console.WriteLine(XDocument.Parse(outdr.ToXML()).ToString());
                return 0;
            }
            else if (args.Length == 2 && args[0] == "json2json")
            {
                // Forces record through getters and then setters, prints as JSON
                DeathRecord indr = new DeathRecord(File.ReadAllText(args[1]));
                DeathRecord outdr = new DeathRecord();
                List<PropertyInfo> properties = typeof(DeathRecord).GetProperties().ToList();
                foreach (PropertyInfo property in properties)
                {
                    if (property.Name.Contains("CausesOfDeath") || property.Name.Contains("CertifierQualification"))
                    {
                        continue;
                    }
                    property.SetValue(outdr, property.GetValue(indr));
                }
                Console.WriteLine(outdr.ToJSON());
                return 0;
            }
            else if (args.Length == 2 && args[0] == "roundtrip-ije")
            {
                // Console.WriteLine("Converting FHIR to IJE...\n");
                DeathRecord d = new DeathRecord(File.ReadAllText(args[1]));
                IJEMortality ije1, ije2, ije3;
                try
                {
                    ije1 = new IJEMortality(d);
                    ije2 = new IJEMortality(ije1.ToString());
                    ije3 = new IJEMortality(new DeathRecord(ije2.ToDeathRecord().ToXML()));
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.Message);
                    return (1);
                }

                int issues = 0;
                int total = 0;
                foreach (PropertyInfo property in typeof(IJEMortality).GetProperties())
                {
                    string val1 = Convert.ToString(property.GetValue(ije1, null));
                    string val2 = Convert.ToString(property.GetValue(ije2, null));
                    string val3 = Convert.ToString(property.GetValue(ije3, null));

                    IJEField info = property.GetCustomAttribute<IJEField>();

                    if (val1.ToUpper() != val2.ToUpper() || val1.ToUpper() != val3.ToUpper() || val2.ToUpper() != val3.ToUpper())
                    {
                        issues++;
                        Console.WriteLine($"[***** MISMATCH *****]\t{info.Name}: {info.Contents} \t\t\"{val1}\" != \"{val2}\" != \"{val3}\"");
                    }
                    total++;
                }
                Console.WriteLine($"\n{issues} issues out of {total} total fields.");
                return issues;
            }
            else if (args.Length == 2 && args[0] == "roundtrip-all")
            {
                DeathRecord d1 = new DeathRecord(File.ReadAllText(args[1]));
                DeathRecord d2 = new DeathRecord(d1.ToJSON());
                DeathRecord d3 = new DeathRecord();
                List<PropertyInfo> properties = typeof(DeathRecord).GetProperties().ToList();
                HashSet<string> skipPropertyNames = new HashSet<string>() { "CausesOfDeath", "AgeAtDeathYears", "AgeAtDeathMonths", "AgeAtDeathDays", "AgeAtDeathHours", "AgeAtDeathMinutes" };
                foreach (PropertyInfo property in properties)
                {
                    if (skipPropertyNames.Contains(property.Name))
                    {
                        continue;
                    }
                    property.SetValue(d3, property.GetValue(d2));
                }

                int good = 0;
                int bad = 0;

                foreach (PropertyInfo property in properties)
                {
                    if (skipPropertyNames.Contains(property.Name))
                    {
                        continue;
                    }
                    // Console.WriteLine($"Property: Name: {property.Name.ToString()} Type: {property.PropertyType.ToString()}");
                    string one;
                    string two;
                    string three;
                    if (property.PropertyType.ToString() == "System.Collections.Generic.Dictionary`2[System.String,System.String]")
                    {
                        Dictionary<string, string> oneDict = (Dictionary<string, string>)property.GetValue(d1);
                        Dictionary<string, string> twoDict = (Dictionary<string, string>)property.GetValue(d2);
                        Dictionary<string, string> threeDict = (Dictionary<string, string>)property.GetValue(d3);
                        // Ignore empty entries in the dictionary so they don't throw off comparisons.
                        one = String.Join(", ", oneDict.Select(x => (x.Value != "") ? (x.Key + "=" + x.Value) : ("")).ToArray()).Replace(" ,", "");
                        two = String.Join(", ", twoDict.Select(x => (x.Value != "") ? (x.Key + "=" + x.Value) : ("")).ToArray()).Replace(" ,", "");
                        three = String.Join(", ", threeDict.Select(x => (x.Value != "") ? (x.Key + "=" + x.Value) : ("")).ToArray()).Replace(" ,", "");
                    }
                    else if (property.PropertyType.ToString() == "System.String[]")
                    {
                        one = String.Join(", ", (string[])property.GetValue(d1));
                        two = String.Join(", ", (string[])property.GetValue(d2));
                        three = String.Join(", ", (string[])property.GetValue(d3));
                    }
                    else
                    {
                        one = Convert.ToString(property.GetValue(d1));
                        two = Convert.ToString(property.GetValue(d2));
                        three = Convert.ToString(property.GetValue(d3));
                    }
                    if (one.ToLower() != three.ToLower())
                    {
                        Console.WriteLine("[***** MISMATCH *****]\t" + $"\"{one}\" (property: {property.Name}) does not equal \"{three}\"" + $"      1:\"{one}\" 2:\"{two}\" 3:\"{three}\"");
                        bad++;
                    }
                    else
                    {
                        // We don't actually need to see all the matches and it makes it hard to see the mismatches
                        // Console.WriteLine("[MATCH]\t" + $"\"{one}\" (property: {property.Name}) equals \"{three}\"" + $"      1:\"{one}\" 2:\"{two}\" 3:\"{three}\"");
                        good++;
                    }
                }
                Console.WriteLine($"\n{bad} mismatches out of {good + bad} total properties checked.");
                if (bad > 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            else if (args.Length == 2 && args[0] == "ije")
            {
                string ijeString = File.ReadAllText(args[1]);
                List<PropertyInfo> properties = typeof(IJEMortality).GetProperties().ToList().OrderBy(p => p.GetCustomAttribute<IJEField>().Field).ToList();

                foreach (PropertyInfo property in properties)
                {
                    IJEField info = property.GetCustomAttribute<IJEField>();
                    string field = ijeString.Substring(info.Location - 1, info.Length);
                    Console.WriteLine($"{info.Field,-5} {info.Name,-15} {Truncate(info.Contents, 75),-75}: \"{field + "\"",-80}");
                }
            }
            else if (args[0] == "ijebuilder")
            {
                IJEMortality ije = new IJEMortality();
                foreach (string arg in args)
                {
                    string[] keyAndValue = arg.Split('=');
                    if (keyAndValue.Length == 2)
                    {
                        typeof(IJEMortality).GetProperty(keyAndValue[0]).SetValue(ije, keyAndValue[1]);
                    }
                }
                DeathRecord dr = ije.ToDeathRecord();
                Console.WriteLine(dr.ToJson());
            }
            else if (args.Length == 3 && args[0] == "compare")
            {
                string ijeString1 = File.ReadAllText(args[1]);

                DeathRecord record2 = new DeathRecord(File.ReadAllText(args[2]));
                IJEMortality ije2 = new IJEMortality(record2);
                string ijeString2 = ije2.ToString();

                List<PropertyInfo> properties = typeof(IJEMortality).GetProperties().ToList().OrderBy(p => p.GetCustomAttribute<IJEField>().Field).ToList();

                int differences = 0;

                foreach (PropertyInfo property in properties)
                {
                    IJEField info = property.GetCustomAttribute<IJEField>();
                    string field1 = ijeString1.Substring(info.Location - 1, info.Length);
                    string field2 = ijeString2.Substring(info.Location - 1, info.Length);
                    if (field1 != field2)
                    {
                        differences += 1;
                        Console.WriteLine($" IJE: {info.Field,-5} {info.Name,-15} {Truncate(info.Contents, 75),-75}: \"{field1 + "\"",-80}");
                        Console.WriteLine($"FHIR: {info.Field,-5} {info.Name,-15} {Truncate(info.Contents, 75),-75}: \"{field2 + "\"",-80}");
                        Console.WriteLine();
                    }
                }
                Console.WriteLine($"Differences detected: {differences}");
                return differences;
            }
            else if (args.Length == 2 && args[0] == "extract")
            {
                BaseMessage message = BaseMessage.Parse(File.ReadAllText(args[1]));
                DeathRecord record;
                switch (message)
                {
                    case DeathRecordSubmissionMessage submission:
                        record = submission.DeathRecord;
                        Console.WriteLine(record.ToJSON());
                        break;
                    case CauseOfDeathCodingMessage coding:
                        record = coding.DeathRecord;
                        Console.WriteLine(record.ToJSON());
                        break;
                    case DemographicsCodingMessage coding:
                        record = coding.DeathRecord;
                        Console.WriteLine(record.ToJSON());
                        break;
                }
                return 0;
            }
            else if (args.Length == 2 && args[0] == "extract2ijecontent")
            {  // dumps content of a submission message in key/value IJE format
                BaseMessage message = BaseMessage.Parse(File.ReadAllText(args[1]), true);
                switch (message)
                {
                    case DeathRecordSubmissionMessage submission:
                        var d = submission.DeathRecord;
                        IJEMortality ije1 = new IJEMortality(d, false);
                        // Loop over every property (these are the fields); Order by priority
                        List<PropertyInfo> properties = typeof(IJEMortality).GetProperties().ToList().OrderBy(p => p.GetCustomAttribute<IJEField>().Location).ToList();
                        foreach (PropertyInfo property in properties)
                        {
                            // Grab the field attributes
                            IJEField info = property.GetCustomAttribute<IJEField>();
                            // Grab the field value
                            string field = Convert.ToString(property.GetValue(ije1, null));
                            // Print the key/value pair to console
                            Console.WriteLine($"{info.Name}: {field.Trim()}");
                        }
                        break;
                }
                return 0;
            }
            else if (args.Length == 2 && args[0] == "submit")
            {
                DeathRecord record = new DeathRecord(File.ReadAllText(args[1]));
                DeathRecordSubmissionMessage message = new DeathRecordSubmissionMessage(record);
                message.MessageSource = "http://mitre.org/vrdr";
                Console.WriteLine(message.ToJSON(true));
                return 0;
            }
            else if (args.Length > 2 && args[0] == "submit")
            {
                string outputDirectory = args[1];
                if (!Directory.Exists(outputDirectory))
                {
                    Console.WriteLine("Must supply a valid output directory");
                    return (1);
                }
                for (int i = 2; i < args.Length; i++)
                {
                    string outputFilename = args[i].Replace(".json", "_submission.json");
                    DeathRecord record = new DeathRecord(File.ReadAllText(args[i]));
                    DeathRecordSubmissionMessage message = new DeathRecordSubmissionMessage(record);
                    message.MessageSource = "http://mitre.org/vrdr";
                    Console.WriteLine($"Writing record to {outputFilename}");
                    StreamWriter sw = new StreamWriter(outputFilename);
                    sw.WriteLine(message.ToJSON(true));
                    sw.Flush();
                }
                return 0;
            }
            else if (args.Length == 2 && args[0] == "alias")
            {
                DeathRecord record = new DeathRecord(File.ReadAllText(args[1]));
                DeathRecordAliasMessage message = new DeathRecordAliasMessage(record);
                message.MessageSource = "http://mitre.org/vrdr";
                Console.WriteLine(message.ToJSON(true));
                return 0;
            }
            else if (args.Length == 2 && args[0] == "showalias")
            {
                DeathRecordAliasMessage message = BaseMessage.Parse(File.ReadAllText(args[1])) as DeathRecordAliasMessage;
                Console.WriteLine($"AliasDecedentFirstName: {message.AliasDecedentFirstName}");
                Console.WriteLine($"AliasDecedentLastName: {message.AliasDecedentLastName}");
                Console.WriteLine($"AliasDecedentMiddleName: {message.AliasDecedentMiddleName}");
                Console.WriteLine($"AliasDecedentNameSuffix: {message.AliasDecedentNameSuffix}");
                Console.WriteLine($"AliasFatherSurname: {message.AliasFatherSurname}");
                Console.WriteLine($"AliasSocialSecurityNumber: {message.AliasSocialSecurityNumber}");
                return 0;
            }
            else if (args.Length == 2 && args[0] == "toMortalityRoster")
            {
                DeathRecord record = new DeathRecord(File.ReadAllText(args[1]));
                Bundle mortalityRoster = record.GetMortalityRosterBundle(false);
                Console.WriteLine(mortalityRoster.ToJson());
                return 0;
            }
            else if (args.Length == 2 && args[0] == "resubmit")
            {
                DeathRecord record = new DeathRecord(File.ReadAllText(args[1]));
                DeathRecordUpdateMessage message = new DeathRecordUpdateMessage(record);
                message.MessageSource = "http://mitre.org/vrdr";
                Console.WriteLine(message.ToJSON(true));
                return 0;
            }
            else if (args.Length == 2 && args[0] == "void")
            {
                DeathRecord record = new DeathRecord(File.ReadAllText(args[1]));
                DeathRecordVoidMessage message = new DeathRecordVoidMessage(record);
                message.MessageSource = "http://mitre.org/vrdr";
                Console.WriteLine(message.ToJSON(true));
                return 0;
            }
            else if (args.Length == 3 && args[0] == "void")
            {
                DeathRecord record = new DeathRecord(File.ReadAllText(args[1]));
                DeathRecordVoidMessage message = new DeathRecordVoidMessage(record);
                message.BlockCount = UInt32.Parse(args[2]);
                message.MessageSource = "http://mitre.org/vrdr";
                Console.WriteLine(message.ToJSON(true));
                return 0;
            }
            else if (args.Length == 2 && args[0] == "ack")
            {
                BaseMessage message = BaseMessage.Parse(File.ReadAllText(args[1]));
                AcknowledgementMessage ackMessage = new AcknowledgementMessage(message);
                Console.WriteLine(ackMessage.ToJSON(true));
                return 0;
            }
            else if (args.Length > 2 && args[0] == "ack")
            {
                string outputDirectory = args[1];
                if (!Directory.Exists(outputDirectory))
                {
                    Console.WriteLine("Must supply a valid output directory");
                    return (1);
                }
                for (int i = 2; i < args.Length; i++)
                {
                    string outputFilename = args[i].Replace(".json", "_acknowledgement.json");
                    BaseMessage message = BaseMessage.Parse(File.ReadAllText(args[i]));
                    AcknowledgementMessage ackMessage = new AcknowledgementMessage(message);
                    Console.WriteLine($"Writing acknowledgement to {outputFilename}");
                    StreamWriter sw = new StreamWriter(outputFilename);
                    sw.WriteLine(ackMessage.ToJSON(true));
                    sw.Flush();
                }
                return 0;
            }
            else if (args[0] == "trx2json")
            {
                if (args.Length == 2)
                {
                    IJEMortality ijeRecord = trx2ije(args[1]);
                    ijeRecord.trx.CS = "3";
                    ijeRecord.trx.SHIP = "555";
                    CauseOfDeathCodingMessage cod = new CauseOfDeathCodingMessage(ijeRecord.ToDeathRecord());
                    //CauseOfDeathCodingUpdateMessage cod = new CauseOfDeathCodingUpdateMessage(ijeRecord.ToDeathRecord());
                    Console.WriteLine(cod.ToJSON(true));
                }
                else
                {
                    Console.WriteLine("Error: command trx2json requires a single TRX file argument");
                }
            }
            else if (args[0] == "json2mre") //  Demographic Coding Content Bundle to MRE
            {
                if (args.Length == 2)
                {
                    DeathRecord d = new DeathRecord(File.ReadAllText(args[1]));
                    if (d.DeathRecordIdentifier == null)
                    {
                        Console.WriteLine("Error: command json2mre requires a Coded Demographic Bundle; did you pass in a message?");
                        return(1);
                    }
                    IJEMortality ije = new IJEMortality(d, false);
                    ije.DOD_YR = d.DeathRecordIdentifier.Substring(0, 4);
                    ije.DSTATE = d.DeathRecordIdentifier.Substring(4, 2);
                    ije.FILENO = d.DeathRecordIdentifier.Substring(6, 6);
                    string MREString = ije2mre(ije);
                    Console.WriteLine(MREString);
                }
                else
                {
                    Console.WriteLine("Error: command json2mre requires a single Coded Demographic Bundle argument");
                }
            }
            else if (args[0] == "json2trx") //  CauseOfDeathCodingMessage to TRX
            {
                if (args.Length == 2)
                {
                    DeathRecord d = new DeathRecord(File.ReadAllText(args[1]));
                    if (d.DeathRecordIdentifier == null)
                    {
                        Console.WriteLine("Error: command json2trx requires a Coded Cause Of Death Bundle; did you pass in a message?");
                        return(1);
                    }
                    IJEMortality ije = new IJEMortality(d, false);
                    ije.DOD_YR = d.DeathRecordIdentifier.Substring(0, 4);
                    ije.DSTATE = d.DeathRecordIdentifier.Substring(4, 2);
                    ije.FILENO = d.DeathRecordIdentifier.Substring(6, 6);
                    string TRXString = ije2trx(ije);
                    Console.WriteLine(TRXString);
                }
                else
                {
                    Console.WriteLine("Error: command json2trx requires a single Coded Cause Of Death Bundle argument");
                }
            }
            else if (args[0] == "compareTRXtoJSON")
            {
                if (args.Length == 3)
                {
                    // Read the TRX file and convert to IJE
                    IJEMortality ije1 = trx2ije(args[1]);
                    // REad the FHIR JSON file and convert to IJE
                    DeathRecord record2 = new DeathRecord(File.ReadAllText(args[2]), false);
                    IJEMortality ije2 = new IJEMortality(record2, false);
                    // These data elements aren't populated in a CodedContent bundle, so we pull them from the identifier and stuff them into the ije record
                    ije2.DOD_YR = record2.DeathRecordIdentifier.Substring(0, 4);
                    ije2.DSTATE = record2.DeathRecordIdentifier.Substring(4, 2);
                    ije2.FILENO = record2.DeathRecordIdentifier.Substring(6, 6);
                    string[] ijeonlyfields = new String[] { "AUXNO2", "POILITRL", "HOWINJ", "TRANSPRT", "COD1A", "INTERVAL1A", "COD1B", "INTERVAL1B", "OTHERCONDITION", "CERTDATE", "SUR_YR", "SUR_MO", "SUR_DY" };
 		    return (CompareIJEtoIJE(ije1, "TRX", ije2, "JSON", ijeonlyfields));
                }
                else
                {
                    Console.WriteLine("Error: compareTRXtoJSON requires two arguments (a TRX file, and a JSON Coded Cause Of Death Bundle)");
                }

            }
            else if (args[0] == "compareMREtoJSON")
            {
                if (args.Length == 3)
                {
                    // Read the MRE file and convert to IJE
                    IJEMortality ije1 = mre2ije(args[1]);
                    // Read the FHIR JSON file and convert to IJE
                    DeathRecord record2 = new DeathRecord(File.ReadAllText(args[2]), false);
                    IJEMortality ije2 = new IJEMortality(record2, false);
                    // These data elements aren't populated in a CodedContent bundle, so we pull them from the identifier and stuff them into the ije record
                    ije2.DOD_YR = record2.DeathRecordIdentifier.Substring(0, 4);
                    ije2.DSTATE = record2.DeathRecordIdentifier.Substring(4, 2);
                    ije2.FILENO = record2.DeathRecordIdentifier.Substring(6, 6);
                    string[] ijeonlyfields = new String[] { "AUXNO", "AUXNO2", "OCCUP" };
                    return (CompareIJEtoIJE(ije1, "MRE", ije2, "JSON", ijeonlyfields));
                }
                else
                {
                    Console.WriteLine("Error: compareMREtoJSON requires two arguments (an MRE file, and a JSON Coded Cause Of Death Bundle)");
                }

            }
            else if (args.Length == 2 && args[0] == "showcodes")
            {
                BaseMessage message = BaseMessage.Parse(File.ReadAllText(args[1]));
                switch (message)
                {
                    case CauseOfDeathCodingMessage codingResponse:
                        Console.WriteLine($"\nUnderlying COD: {codingResponse.DeathRecord.AutomatedUnderlyingCOD}\n");
                        Console.WriteLine("Record Axis Codes:");
                        foreach (var entry in codingResponse.DeathRecord.RecordAxisCauseOfDeath)
                        {
                            Console.WriteLine($"  Position: {entry.Item1}  Code: {entry.Item2}");
                        }
                        Console.WriteLine("\nEntity Axis Codes:");
                        foreach (var entry in codingResponse.DeathRecord.EntityAxisCauseOfDeath)
                        {
                            Console.WriteLine($"  Line: {entry.Item1}  Position: {entry.Item2}  Code: {entry.Item3}");
                        }
                        Console.WriteLine();
                        break;
                    case DemographicsCodingMessage codingResponse:
                        Console.WriteLine($"First Edited Race Code: {codingResponse.DeathRecord.FirstEditedRaceCodeHelper}");
                        Console.WriteLine($"Second Edited Race Code: {codingResponse.DeathRecord.SecondEditedRaceCodeHelper}");
                        Console.WriteLine($"Third Edited Race Code: {codingResponse.DeathRecord.ThirdEditedRaceCodeHelper}");
                        Console.WriteLine($"Fourth Edited Race Code: {codingResponse.DeathRecord.FourthEditedRaceCodeHelper}");
                        Console.WriteLine($"Fifth Edited Race Code: {codingResponse.DeathRecord.FifthEditedRaceCodeHelper}");
                        Console.WriteLine($"Sixth Edited Race Code: {codingResponse.DeathRecord.SixthEditedRaceCodeHelper}");
                        Console.WriteLine($"Seventh Edited Race Code: {codingResponse.DeathRecord.SeventhEditedRaceCodeHelper}");
                        Console.WriteLine($"Eighth Edited Race Code: {codingResponse.DeathRecord.EighthEditedRaceCodeHelper}");
                        Console.WriteLine($"First American Indian Race Code: {codingResponse.DeathRecord.FirstAmericanIndianRaceCodeHelper}");
                        Console.WriteLine($"Second American Indian Race Code: {codingResponse.DeathRecord.SecondAmericanIndianRaceCodeHelper}");
                        break;
                    default:
                        Console.WriteLine("Message does not appear to be a CodingMessage");
                        break;
                }
                return 0;
            }
            else if (args.Length >= 5 && args[0] == "generaterecords")
            {
                int start_certificate_number = Int16.Parse(args[1]);
                int count = Int16.Parse(args[2]);
                string state = args[3];
                string output_directory = args[4];
                int year = DateTime.Today.Year;
                if (args.Length > 5)
                {
                    year = Int16.Parse(args[5]);
                }
                if (start_certificate_number <= 0)
                {
                    Console.WriteLine("Must supply a starting certificate number greater than 0");
                    return (1);
                }
                else if (count <= 0)
                {
                    Console.WriteLine("Must supply a count greater than 0");
                    return (1);
                }
                else if (!Regex.IsMatch(state, "^[A-Z][A-Z]$"))
                {
                    Console.WriteLine("Must supply a valid two character jurisdiction code");
                    return (1);
                }
                else if (String.IsNullOrWhiteSpace(output_directory) || !Directory.Exists(output_directory))
                {
                    Console.WriteLine("Must supply a valid output directory");
                    return (1);
                }
                else if (year < 1900 || year > 3000)
                {
                    Console.WriteLine("Must supply a valid year if supplying a year");
                }
                for (int i = 0; i < count; i += 1)
                {
                    int certificate_number = start_certificate_number + i;
                    string cert6 = certificate_number.ToString("D6");
                    int record_selector = (i % 3) + 1;
                    DeathRecord record = Connectathon.FromId(record_selector, certificate_number, state, year);
                    String file_name = $"{output_directory}/{year}{state}{cert6}.json";
                    Console.WriteLine($"Writing record to {file_name}");
                    StreamWriter sw = new StreamWriter(file_name);
                    sw.WriteLine(record.ToJson());
                    sw.Flush();
                }
            }
            else if (args.Length >= 3 && args[0] == "batch")
            {
                string url = args[1];
                List<BaseMessage> messages = new List<BaseMessage>();
                for (int i = 2; i < args.Length; i++)
                {
                    messages.Add(BaseMessage.Parse(File.ReadAllText(args[i])));
                }
                string payload = Client.CreateBulkUploadPayload(messages, url, true);
                Console.WriteLine(payload);
                return 0;
            }
            else if (args.Length == 3 && args[0] == "filter")
            {
                Console.WriteLine($"Filtering file {args[1]}");

                BaseMessage baseMessage = BaseMessage.Parse(File.ReadAllText(args[1]));

                FilterService FilterService = new FilterService("./VRDR.Filter/NCHSIJEFilter.json", "./VRDR.Filter/IJEToFHIRMapping.json");

                var filteredFile = FilterService.filterMessage(baseMessage).ToJson();
                BaseMessage.Parse(filteredFile);
                Console.WriteLine($"File successfully filtered and saved to {args[2]}");

                File.WriteAllText(args[2], filteredFile);

                return 0;
            }
            else
            {
                Console.WriteLine($"**** No such command {args[0]} with the number of arguments supplied");
            }
            return 0;
        }

        private static async System.Threading.Tasks.Task CallEndpoint(string endpoint, StringContent content)
        {
            var response = await new HttpClient().PostAsync(endpoint, content);
            var responseString = await response.Content.ReadAsStringAsync();
        }

        private static string Truncate(string value, int length)
        {
            if (String.IsNullOrWhiteSpace(value) || value.Length <= length)
            {
                return value;
            }
            else
            {
                return value.Substring(0, length);
            }
        }
        private static IJEMortality mre2ije(string mrefilename)
        {
            // Mapping a MRE file to an IJE file:
            string mre = File.ReadAllText(mrefilename);
            mre = mre.PadRight(350);
            string ije = string.Empty.PadRight(5000);
            ije = ije.Insert(246, mre.Substring(15, 324));
            IJEMortality ijeRecord = new IJEMortality(ije);
            ijeRecord.DOD_YR = mre.Substring(0, 4);
            ijeRecord.DSTATE = mre.Substring(4, 2);
            ijeRecord.FILENO = mre.Substring(6, 6);
            ijeRecord.DETHNICE = mre.Substring(342, 3);
            ijeRecord.DETHNIC5C = mre.Substring(345, 3);

            return (ijeRecord);
        }
        private static string ije2mre(IJEMortality ije)
        {
            string ijeString = ije.ToString();
            string mreString = string.Empty.PadRight(500);
            mreString = mreString.Insert(0, ije.DOD_YR);
            mreString = mreString.Insert(4, ije.DSTATE);
            mreString = mreString.Insert(6, ije.FILENO);
            mreString = mreString.Insert(15, ijeString.Substring(246, 324));
            mreString = mreString.Insert(342, ije.DETHNICE);
            mreString = mreString.Insert(345, ije.DETHNIC5C);
            return (mreString);
        }

        private static IJEMortality trx2ije(string trxfilename)
        {
            // Mapping a TRX file to an IJE file:
            string trx = File.ReadAllText(trxfilename);
            trx = trx.PadRight(500);
            string ije = string.Empty.PadRight(5000);
            IJEMortality ijeRecord = new IJEMortality();
            ijeRecord.DOD_YR = trx.Substring(0, 4);
            ijeRecord.DSTATE = trx.Substring(4, 2);
            ijeRecord.FILENO = trx.Substring(6, 6);
            ijeRecord.R_YR = trx.Substring(25, 4);
            ijeRecord.R_DY = trx.Substring(23, 2);
            ijeRecord.R_MO = trx.Substring(21, 2);
            ijeRecord.MANNER = trx.Substring(41, 1);
            ijeRecord.INT_REJ = trx.Substring(42, 1);
            ijeRecord.SYS_REJ = trx.Substring(43, 1);
            ijeRecord.INJPL = trx.Substring(44, 1);
            ijeRecord.MAN_UC = trx.Substring(45, 5);
            ijeRecord.ACME_UC = trx.Substring(50, 5);
            ijeRecord.EAC = trx.Substring(55, 160);
            ijeRecord.TRX_FLG = trx.Substring(215, 1);
            ijeRecord.RAC = trx.Substring(216, 100);
            ijeRecord.AUTOP = trx.Substring(316, 1);
            ijeRecord.AUTOPF = trx.Substring(317, 1);
            ijeRecord.TOBAC = trx.Substring(318, 1);
            ijeRecord.PREG = trx.Substring(319, 1);
            ijeRecord.PREG_BYPASS = trx.Substring(320, 1);
            ijeRecord.DOI_MO = trx.Substring(321, 2);
            ijeRecord.DOI_DY = trx.Substring(323, 2);
            ijeRecord.DOI_YR = trx.Substring(325, 4);
            ijeRecord.TOI_HR = trx.Substring(329, 4);
            ijeRecord.WORKINJ = trx.Substring(333, 1);
            ijeRecord.CERTL = trx.Substring(334, 30);
            ijeRecord.INACT = trx.Substring(364, 1);
            ijeRecord.AUXNO = trx.Substring(365, 12);
            ijeRecord.STATESP = trx.Substring(377, 30);
            return (ijeRecord);
        }
        private static string ije2trx(IJEMortality ije)
        {
            string ijeString = ije.ToString();
            string trxString = string.Empty.PadRight(500);
            trxString = trxString.Insert(0, ije.DOD_YR);
            trxString = trxString.Insert(4, ije.DSTATE);
            trxString = trxString.Insert(6, ije.FILENO);
            trxString = trxString.Insert(21, ije.R_MO);
            trxString = trxString.Insert(23, ije.R_DY);
            trxString = trxString.Insert(25, ije.R_YR);
            trxString = trxString.Insert(41, ije.MANNER);
            trxString = trxString.Insert(42, ije.INT_REJ);
            trxString = trxString.Insert(43, ije.SYS_REJ);
            trxString = trxString.Insert(44, ije.INJPL);
            trxString = trxString.Insert(45, ije.MAN_UC);
            trxString = trxString.Insert(50, ije.ACME_UC);
            trxString = trxString.Insert(55, ije.EAC);
            trxString = trxString.Insert(215, ije.TRX_FLG);
            trxString = trxString.Insert(216, ije.RAC);
            trxString = trxString.Insert(316, ije.AUTOP);
            trxString = trxString.Insert(317, ije.AUTOPF);
            trxString = trxString.Insert(318, ije.TOBAC);
            trxString = trxString.Insert(319, ije.PREG);
            trxString = trxString.Insert(320, ije.PREG_BYPASS);
            trxString = trxString.Insert(321, ije.DOI_MO);
            trxString = trxString.Insert(323, ije.DOI_DY);
            trxString = trxString.Insert(325, ije.DOI_YR);
            trxString = trxString.Insert(329, ije.TOI_HR);
            trxString = trxString.Insert(333, ije.WORKINJ);
            trxString = trxString.Insert(334, ije.CERTL);
            trxString = trxString.Insert(364, ije.INACT);
            trxString = trxString.Insert(365, ije.AUXNO);
            trxString = trxString.Insert(377, ije.STATESP);
            return (trxString);
        }


        private static int CompareIJEtoIJE(IJEMortality ije1, string ije1name, IJEMortality ije2, string ije2name, string[] excludefields = null)
        {
            string ijeString1 = ije1.ToString();
            string ijeString2 = ije2.ToString();

            int namePadding = ije1name.Length > ije2name.Length ? ije1name.Length : ije2name.Length;

            List<PropertyInfo> properties = typeof(IJEMortality).GetProperties().ToList().OrderBy(p => p.GetCustomAttribute<IJEField>().Field).ToList();

            int differences = 0;

            foreach (PropertyInfo property in properties)
            {
                IJEField info = property.GetCustomAttribute<IJEField>();
                if (excludefields != null && excludefields.Contains(info.Name))
                {
                    continue;
                }
                string field1 = ijeString1.Substring(info.Location - 1, info.Length);
                string field2 = ijeString2.Substring(info.Location - 1, info.Length);
                if (field1 != field2)
                {
                    differences += 1;
                    Console.WriteLine($"{ije1name.PadLeft(namePadding)}: {info.Field,-5} {info.Name,-15} {Truncate(info.Contents, 75),-75}: \"{field1 + "\"",-80}");
                    Console.WriteLine($"{ije2name.PadLeft(namePadding)}: {info.Field,-5} {info.Name,-15} {Truncate(info.Contents, 75),-75}: \"{field2 + "\"",-80}");
                    Console.WriteLine();
                }
            }
            Console.WriteLine($"Differences detected: {differences}");
            return differences;
        }
    }
}
