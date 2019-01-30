using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Reflection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.ElementModel;
using Hl7.FhirPath;
using FhirDeathRecord;

namespace csharp_fhir_death_record
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {

            }
            else if (args.Length == 2 && args[0] == "ije")
            {
                Console.WriteLine("Converting FHIR SDR to IJE...\n");
                DeathRecord d = new DeathRecord(File.ReadAllText(args[1]));
                //Console.WriteLine(XDocument.Parse(d.ToXML()).ToString() + "\n");
                IJEMortality ije1 = new IJEMortality(d);
                //Console.WriteLine(ije1.ToString() + "\n\n");
                IJEMortality ije2 = new IJEMortality(ije1.ToString());
                //Console.WriteLine(ije2.ToString() + "\n\n");
                //Console.WriteLine(XDocument.Parse(ije2.ToDeathRecord().ToXML()).ToString() + "\n");
                IJEMortality ije3 = new IJEMortality(new DeathRecord(ije2.ToDeathRecord().ToXML()));
                int issues = 0;
                int total = 0;
                foreach(PropertyInfo property in typeof(IJEMortality).GetProperties())
                {
                    string val1 = Convert.ToString(property.GetValue(ije1, null));
                    string val2 = Convert.ToString(property.GetValue(ije2, null));
                    string val3 = Convert.ToString(property.GetValue(ije3, null));

                    IJEField info = (IJEField)property.GetCustomAttributes().First();

                    if (val1.ToUpper() != val2.ToUpper() || val1.ToUpper() != val3.ToUpper() || val2.ToUpper() != val3.ToUpper())
                    {
                        issues++;
                        Console.WriteLine($"[MISMATCH]\t{info.Name}: {info.Contents} \t\t\"{val1}\" != \"{val2}\" != \"{val3}\"");
                    }
                    total++;
                }
                Console.WriteLine($"\n{issues} issues out of {total} total fields.");
            }
            else if (args.Length == 2 && args[0] == "json2xml")
            {
                Console.WriteLine("Converting FHIR JSON to FHIR XML...\n");
                DeathRecord d = new DeathRecord(File.ReadAllText(args[1]));
                Console.WriteLine(XDocument.Parse(d.ToXML()).ToString());
            }
            else if (args.Length == 2 && args[0] == "xml2json")
            {
                Console.WriteLine("Converting FHIR XML to FHIR JSON...\n");
                DeathRecord d = new DeathRecord(File.ReadAllText(args[1]));
                Console.WriteLine(d.ToJSON());
            }
            else if (args.Length == 2 && args[0] == "xml2xml")
            {
                // Forces record through getters and then setters, prints as xml
                DeathRecord indr = new DeathRecord(File.ReadAllText(args[1]));
                DeathRecord outdr = new DeathRecord();
                List<PropertyInfo> properties = typeof(DeathRecord).GetProperties().ToList();
                foreach(PropertyInfo property in properties)
                {
                    if (property.Name.Contains("GivenNames") || property.Name.Contains("CertifierGivenNames") || property.Name.Contains("CausesOfDeath"))
                    {
                        continue;
                    }
                    property.SetValue(outdr, property.GetValue(indr));
                }
                Console.WriteLine(XDocument.Parse(outdr.ToXML()).ToString());
            }
            else if (args.Length == 2 && args[0] == "json2json")
            {
                // Forces record through getters and then setters, prints as JSON
                DeathRecord indr = new DeathRecord(File.ReadAllText(args[1]));
                DeathRecord outdr = new DeathRecord();
                List<PropertyInfo> properties = typeof(DeathRecord).GetProperties().ToList();
                foreach(PropertyInfo property in properties)
                {
                    if (property.Name.Contains("GivenNames") || property.Name.Contains("CertifierGivenNames") || property.Name.Contains("CausesOfDeath"))
                    {
                        continue;
                    }
                    property.SetValue(outdr, property.GetValue(indr));
                }
                Console.WriteLine(outdr.ToJSON());
            }
            else if (args.Length == 2 && args[0] == "vrdr")
            {
                DeathRecord vrdr = new DeathRecord();

                // Decedent

                string[] givenNames = {"Robert", "John"};
                vrdr.GivenNames = givenNames;
                vrdr.FamilyName = "Smith";
                vrdr.Suffix = "Jr.";
                vrdr.Nickname = "Bobby";

                vrdr.Gender = "male";

                vrdr.DateOfBirth = "1974-12-31T00:00:00+00:00";

                vrdr.SSN = "123-45-6789";

                vrdr.DateOfDeath = "2017-12-31T00:00:00+00:00";

                Dictionary<string, string> mscode = new Dictionary<string, string>();
                mscode.Add("code", "S");
                mscode.Add("system", "http://hl7.org/fhir/ValueSet/marital-status");
                mscode.Add("display", "Never Married");
                vrdr.MaritalStatus = mscode;

                Tuple<string, string>[] ethnicity = { Tuple.Create("Hispanic or Latino", "2135-2"), Tuple.Create("Puerto Rican", "2180-8") };
                vrdr.Ethnicity = ethnicity;

                Tuple<string, string>[] race = { Tuple.Create("Native Hawaiian or Other Pacific Islander", "2076-8"), Tuple.Create("Tahitian", "2081-8") };
                vrdr.Race = race;

                Dictionary<string, string> bcode = new Dictionary<string, string>();
                bcode.Add("code", "M");
                bcode.Add("system", "http://hl7.org/fhir/us/core/ValueSet/us-core-birthsex");
                bcode.Add("display", "Male");
                vrdr.BirthSex = bcode;

                Dictionary<string, string> residence = new Dictionary<string, string>();
                residence.Add("residenceLine1", "9 Example Street");
                residence.Add("residenceCity", "Bedford");
                residence.Add("residenceCounty", "Middlesex");
                residence.Add("residenceState", "Massachusetts");
                residence.Add("residenceZip", "01730");
                residence.Add("residenceCountry", "United States");
                residence.Add("residenceInsideCityLimits", "True");
                vrdr.Residence = residence;

                Dictionary<string, string> birthPlace = new Dictionary<string, string>();
                birthPlace.Add("placeOfBirthLine1", "42 Example Street");
                birthPlace.Add("placeOfBirthCity", "Boston");
                birthPlace.Add("placeOfBirthCounty", "Suffolk");
                birthPlace.Add("placeOfBirthState", "Massachusetts");
                birthPlace.Add("placeOfBirthZip", "02101");
                birthPlace.Add("placeOfBirthCountry", "United States");
                vrdr.PlaceOfBirth = birthPlace;

                // Decedent Age

                Dictionary<string, string> age = new Dictionary<string, string>();
                age.Add("value", "70");
                age.Add("unit", "a");
                vrdr.Age = age;

                // Decedent Death Location

                Dictionary<string, string> placeOfDeath = new Dictionary<string, string>();
                placeOfDeath.Add("placeOfDeathTypeCode", "HOSP");
                placeOfDeath.Add("placeOfDeathTypeSystem", "http://hl7.org/fhir/ValueSet/v3-ServiceDeliveryLocationRoleType");
                placeOfDeath.Add("placeOfDeathTypeDisplay", "Hospital");
                placeOfDeath.Add("placeOfDeathPhysicalTypeCode", "wa");
                placeOfDeath.Add("placeOfDeathPhysicalTypeSystem", "http://hl7.org/fhir/ValueSet/location-physical-type");
                placeOfDeath.Add("placeOfDeathPhysicalTypeDisplay", "Ward");
                placeOfDeath.Add("placeOfDeathName", "Example Hospital");
                placeOfDeath.Add("placeOfDeathDescription", "Example Hospital Wing B");
                placeOfDeath.Add("placeOfDeathLine1", "8 Example Street");
                placeOfDeath.Add("placeOfDeathCity", "Bedford");
                placeOfDeath.Add("placeOfDeathCounty", "Middlesex");
                placeOfDeath.Add("placeOfDeathState", "Massachusetts");
                placeOfDeath.Add("placeOfDeathZip", "01730");
                placeOfDeath.Add("placeOfDeathCountry", "United States");
                vrdr.PlaceOfDeath = placeOfDeath;

                // Decedent Death Date

                vrdr.ActualOrPresumedDateOfDeath = "2018-04-24T00:00:00+00:00";
                vrdr.DatePronouncedDead = "2018-04-25T00:00:00+00:00";

                // Mortician

                vrdr.MorticianFirstName = "Mortician";
                vrdr.MorticianMiddleName = "MMiddle";
                vrdr.MorticianFamilyName = "MFamily";
                vrdr.MorticianSuffix = "Jr.";
                vrdr.MorticianId = "1234567890";

                // Disposition (Disposition Method, Disposition Location, Funeral Home, Funeral Home Director)
                Dictionary<string, string> ddictionary = new Dictionary<string, string>();
                ddictionary.Add("dispositionTypeCode", "449971000124106");
                ddictionary.Add("dispositionTypeSystem", "http://snomed.info/sct");
                ddictionary.Add("dispositionTypeDisplay", "Burial");
                ddictionary.Add("dispositionPlaceName", "Example disposition place name");
                ddictionary.Add("dispositionPlaceLine1", "100 Example Street");
                ddictionary.Add("dispositionPlaceLine2", "Line 2");
                ddictionary.Add("dispositionPlaceCity", "Bedford");
                ddictionary.Add("dispositionPlaceCounty", "Middlesex");
                ddictionary.Add("dispositionPlaceState", "Massachusetts");
                ddictionary.Add("dispositionPlaceZip", "01730");
                ddictionary.Add("dispositionPlaceCountry", "United States");
                ddictionary.Add("funeralFacilityName", "Example funeral facility name");
                ddictionary.Add("funeralFacilityLine1", "50 Example Street");
                ddictionary.Add("funeralFacilityLine2", "Line 2a");
                ddictionary.Add("funeralFacilityCity", "Watertown");
                ddictionary.Add("funeralFacilityCounty", "Middlesex");
                ddictionary.Add("funeralFacilityState", "Massachusetts");
                ddictionary.Add("funeralFacilityZip", "02472");
                ddictionary.Add("funeralFacilityCountry", "United States");
                vrdr.Disposition = ddictionary;

                // Injury Details (Injury Incident, Injury Location)

                Dictionary<string, string> idictionary = new Dictionary<string, string>();
                idictionary.Add("injuryDescription", "Example details of injury");
                idictionary.Add("injuryEffectiveDateTime", "2018-04-19T15:43:00+00:00");
                idictionary.Add("placeOfInjuryName", "Decedent's Home");
                idictionary.Add("placeOfInjuryLine1", "7 Example Street");
                idictionary.Add("placeOfInjuryLine2", "Unit 1234");
                idictionary.Add("placeOfInjuryCity", "Bedford");
                idictionary.Add("placeOfInjuryCounty", "Middlesex");
                idictionary.Add("placeOfInjuryState", "Massachusetts");
                idictionary.Add("placeOfInjuryZip", "01730");
                idictionary.Add("placeOfInjuryCountry", "United States");
                vrdr.DetailsOfInjury = idictionary;

                // Transportation Event Indicator

                Dictionary<string, string> tdictionary = new Dictionary<string, string>();
                tdictionary.Add("code", "236320001");
                tdictionary.Add("system", "http://hl7.org/fhir/ValueSet/TransportationRelationships");
                tdictionary.Add("display", "Vehicle driver");
                vrdr.DeathFromTransportInjury = tdictionary;

                // Work Injury Indicator

                vrdr.DeathFromWorkInjury = true;

                // Examiner contacted

                vrdr.MedicalExaminerContacted = true;

                // Autopsy performed

                Dictionary<string, string> apdictionary = new Dictionary<string, string>();
                apdictionary.Add("performedCode", "N");
                apdictionary.Add("performedSystem", "http://terminology.hl7.org/CodeSystem/v2-0136");
                apdictionary.Add("performedDisplay", "No");
                apdictionary.Add("resultsAvailableCode", "N");
                apdictionary.Add("resultsAvailableSystem", "http://terminology.hl7.org/CodeSystem/v2-0136");
                apdictionary.Add("resultsAvailableDisplay", "No");
                vrdr.AutopsyPerformedAndResultsAvailable = apdictionary;

                // Cause Of Death Condition
                Tuple<string, string, Dictionary<string, string>>[] causes =
                {
                    Tuple.Create("Acute transmural myocardial infarction of anterior wall", "minutes", new Dictionary<string, string>(){ {"code", "I21.0"}, {"system", "ICD-10"}, {"display", "Acute transmural myocardial infarction of anterior wall"} })
                };
                vrdr.CausesOfDeath = causes;

                // Death Pronouncement Performer & Certifier

                vrdr.CertifierFamilyName = "Doctor";
                vrdr.CertifierGivenNames = new string[] { "Certifier", "Middle" };
                vrdr.CertifierSuffix = "MD";
                Dictionary<string, string> caddress = new Dictionary<string, string>();
                caddress.Add("certifierAddressStreet", "123 Example Street");
                caddress.Add("certifierAddressCity", "Boston");
                caddress.Add("certifierAddressCounty", "Suffolk");
                caddress.Add("certifierAddressState", "Massachusetts");
                caddress.Add("certifierAddressZip", "02101");
                vrdr.CertifierAddress = caddress;
                Dictionary<string, string> cqcode = new Dictionary<string, string>();
                cqcode.Add("code", "MD");
                cqcode.Add("system", "http://hl7.org/fhir/v2/0360/2.7");
                cqcode.Add("display", "Doctor of Medicine");
                vrdr.CertifierQualification = cqcode;
                vrdr.CertifierId = "1234567890";


                // Contributing conditions

                vrdr.ContributingConditions = "Example Contributing Conditions";


                // Education level

                Dictionary<string, string> ecode = new Dictionary<string, string>();
                ecode.Add("code", "GD");
                ecode.Add("system", "http://hl7.org/fhir/ValueSet/v3-EducationLevel");
                ecode.Add("display", "Graduate or professional Degree complete");
                vrdr.Education = ecode;


                // Employment History

                Dictionary<string, string> ocode = new Dictionary<string, string>();
                ocode.Add("code", "GD");
                ocode.Add("system", "http://hl7.org/fhir/ValueSet/v3-EducationLevel");
                ocode.Add("display", "Graduate or professional Degree complete");
                vrdr.Occupation = ocode;


                // Pregnancy

                Dictionary<string, string> pcode = new Dictionary<string, string>();
                pcode.Add("code", "PHC1260");
                pcode.Add("system", "http://hl7.org/fhir/STU3/valueset-PregnancyStatusVS");
                pcode.Add("display", "Not pregnant within past year");
                vrdr.TimingOfRecentPregnancyInRelationToDeath = pcode;


                // Manner

                Dictionary<string, string> mcode = new Dictionary<string, string>();
                mcode.Add("code", "7878000");
                mcode.Add("system", "http://hl7.org/fhir/STU3/valueset-MannerTypeVS");
                mcode.Add("display", "Accident");
                vrdr.MannerOfDeath = mcode;


                // Tobacco
                Dictionary<string, string> tcode = new Dictionary<string, string>();
                tcode.Add("code", "373066001");
                tcode.Add("system", "http://hl7.org/fhir/STU3/valueset-ContributoryTobaccoUseVS");
                tcode.Add("display", "Yes");
                vrdr.TobaccoUseContributedToDeath = tcode;


                if (args[1] == "json")
                {
                    Console.WriteLine(FormatJson(vrdr.ToJSON()));
                }
                else if (args[1] == "xml")
                {
                    Console.WriteLine(XDocument.Parse(vrdr.ToXML()).ToString());
                }
            }
            else
            {
                foreach (var path in args)
                {
                    ReadFile(path);
                }
            }
        }

        private static void ReadFile(string path)
        {
            if (File.Exists(path))
            {

            }
            else
            {
                Console.WriteLine($"Error: File '{path}' does not exist");
            }
        }

        private const string INDENT_STRING = "    ";
        static string FormatJson(string json) {

            int indentation = 0;
            int quoteCount = 0;
            var result =
                from ch in json
                let quotes = ch == '"' ? quoteCount++ : quoteCount
                let lineBreak = ch == ',' && quotes % 2 == 0 ? ch + Environment.NewLine +  String.Concat(Enumerable.Repeat(INDENT_STRING, indentation)) : null
                let openChar = ch == '{' || ch == '[' ? ch + Environment.NewLine + String.Concat(Enumerable.Repeat(INDENT_STRING, ++indentation)) : ch.ToString()
                let closeChar = ch == '}' || ch == ']' ? Environment.NewLine + String.Concat(Enumerable.Repeat(INDENT_STRING, --indentation)) + ch : ch.ToString()
                select lineBreak == null
                            ? openChar.Length > 1
                                ? openChar
                                : closeChar
                            : lineBreak;

            return String.Concat(result);
        }

    }
}
