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

namespace FhirDeathRecord.CLI
{
    class Program
    {
        static int Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("No filepath given; Constructing a fake record and printing its XML and JSON output...\n");
                DeathRecord deathRecord = new DeathRecord();
                deathRecord.Id = "1337";
                deathRecord.DateOfRegistration = "2018-07-11";
                deathRecord.GivenNames = new string[] { "First", "Middle" };
                deathRecord.FamilyName = "Last";
                deathRecord.MaidenName = "Last Maiden";
                deathRecord.Suffix = "Sr.";
                deathRecord.FatherFamilyName = "FTHLast";
                deathRecord.Gender = "male";
                Dictionary<string, string> code = new Dictionary<string, string>();
                code.Add("code", "M");
                code.Add("system", "http://hl7.org/fhir/us/core/ValueSet/us-core-birthsex");
                code.Add("display", "Male");
                deathRecord.BirthSex = code;
                Dictionary<string, string> dictionary = new Dictionary<string, string>();
                dictionary.Add("residenceLine1", "19 Example Street");
                dictionary.Add("residenceLine2", "Line 2");
                dictionary.Add("residenceCity", "Bedford");
                dictionary.Add("residenceCounty", "Middlesex");
                dictionary.Add("residenceState", "Massachusetts");
                dictionary.Add("residenceZip", "01730");
                dictionary.Add("residenceCountry", "United States");
                dictionary.Add("residenceInsideCityLimits", "True");
                deathRecord.Residence = dictionary;
                deathRecord.SSN = "111223333";
                Tuple<string, string>[] ethnicity = { Tuple.Create("Hispanic or Latino", "2135-2"), Tuple.Create("Puerto Rican", "2180-8") };
                deathRecord.Ethnicity = ethnicity;
                Tuple<string, string>[] race = {Tuple.Create("White", "2106-3"), Tuple.Create("Native Hawaiian or Other Pacific Islander", "2076-8")};
                deathRecord.Race = race;
                deathRecord.DateOfBirth = "1970-04-24";
                deathRecord.DateOfDeath = "1970-04-24";
                dictionary = new Dictionary<string, string>();
                dictionary.Add("placeOfBirthLine1", "9 Example Street");
                dictionary.Add("placeOfBirthLine2", "Line 2");
                dictionary.Add("placeOfBirthCity", "Bedford");
                dictionary.Add("placeOfBirthCounty", "Middlesex");
                dictionary.Add("placeOfBirthState", "Massachusetts");
                dictionary.Add("placeOfBirthZip", "01730");
                dictionary.Add("placeOfBirthCountry", "United States");
                deathRecord.PlaceOfBirth = dictionary;
                dictionary = new Dictionary<string, string>();
                dictionary.Add("placeOfDeathTypeCode", "16983000");
                dictionary.Add("placeOfDeathTypeSystem", "http://snomed.info/sct");
                dictionary.Add("placeOfDeathTypeDisplay", "Death in hospital");
                dictionary.Add("placeOfDeathFacilityName", "Example Hospital");
                dictionary.Add("placeOfDeathLine1", "8 Example Street");
                dictionary.Add("placeOfDeathLine2", "Line 2");
                dictionary.Add("placeOfDeathCity", "Bedford");
                dictionary.Add("placeOfDeathCounty", "Middlesex");
                dictionary.Add("placeOfDeathState", "Massachusetts");
                dictionary.Add("placeOfDeathZip", "01730");
                dictionary.Add("placeOfDeathCountry", "United States");
                dictionary.Add("placeOfDeathInsideCityLimits", "True");
                deathRecord.PlaceOfDeath = dictionary;
                code = new Dictionary<string, string>();
                code.Add("code", "S");
                code.Add("system", "http://hl7.org/fhir/v3/MaritalStatus");
                code.Add("display", "Never Married");
                deathRecord.MaritalStatus = code;
                code = new Dictionary<string, string>();
                code.Add("code", "PHC1453");
                code.Add("system", "http://github.com/nightingaleproject/fhirDeathRecord/sdr/decedent/cs/EducationCS");
                code.Add("display", "Bachelor's Degree");
                deathRecord.Education = code;
                deathRecord.Age = "100";
                Dictionary<string, string> occupation = new Dictionary<string, string>();
                occupation.Add("jobDescription", "Software Engineer");
                occupation.Add("industryDescription", "Information Technology");
                deathRecord.Occupation = occupation;
                deathRecord.ServedInArmedForces = false;
                dictionary = new Dictionary<string, string>();
                dictionary.Add("dispositionTypeCode", "449971000124106");
                dictionary.Add("dispositionTypeSystem", "http://snomed.info/sct");
                dictionary.Add("dispositionTypeDisplay", "Burial");
                dictionary.Add("dispositionPlaceName", "Example disposition place name");
                dictionary.Add("dispositionPlaceLine1", "100 Example Street");
                dictionary.Add("dispositionPlaceLine2", "Line 2");
                dictionary.Add("dispositionPlaceCity", "Bedford");
                dictionary.Add("dispositionPlaceCounty", "Middlesex");
                dictionary.Add("dispositionPlaceState", "Massachusetts");
                dictionary.Add("dispositionPlaceZip", "01730");
                dictionary.Add("dispositionPlaceCountry", "United States");
                dictionary.Add("dispositionPlaceInsideCityLimits", "True");
                dictionary.Add("funeralFacilityName", "Example funeral facility name");
                dictionary.Add("funeralFacilityLine1", "50 Example Street");
                dictionary.Add("funeralFacilityLine2", "Line 2a");
                dictionary.Add("funeralFacilityCity", "Watertown");
                dictionary.Add("funeralFacilityCounty", "Middlesex");
                dictionary.Add("funeralFacilityState", "Massachusetts");
                dictionary.Add("funeralFacilityZip", "02472");
                dictionary.Add("funeralFacilityCountry", "United States");
                dictionary.Add("funeralFacilityInsideCityLimits", "False");
                deathRecord.Disposition = dictionary;
                deathRecord.CertifierFamilyName = "Doctor";
                deathRecord.CertifierGivenNames = new string[] { "Certifier", "Middle" };
                deathRecord.CertifierSuffix = "Sr.";
                Dictionary<string, string> address = new Dictionary<string, string>();
                address.Add("certifierAddressStreet", "123 Test Street");
                address.Add("certifierAddressCity", "Boston");
                address.Add("certifierAddressCounty", "Suffolk");
                address.Add("certifierAddressState", "Massachusetts");
                address.Add("certifierAddressZip", "12345");
                deathRecord.CertifierAddress = address;
                code = new Dictionary<string, string>();
                code.Add("code", "434651000124107");
                code.Add("display", "Physician (Pronouncer and Certifier)");
                deathRecord.CertifierType = code;
                code = new Dictionary<string, string>();
                code.Add("code", "MD");
                code.Add("system", "http://hl7.org/fhir/v2/0360/2.7");
                code.Add("display", "Doctor of Medicine");
                deathRecord.CertifierQualification = code;
                deathRecord.ContributingConditions = "Example Contributing Condition";
                Tuple<string, string, Dictionary<string, string>>[] causes =
                {
                    Tuple.Create("Example Immediate COD", "minutes", new Dictionary<string, string>(){ {"code", "1234"}, {"system", "example"} }),
                    Tuple.Create("Example Underlying COD 1", "2 hours", new Dictionary<string, string>()),
                    Tuple.Create("Example Underlying COD 2", "6 months", new Dictionary<string, string>()),
                    Tuple.Create("Example Underlying COD 3", "15 years", new Dictionary<string, string>())
                };
                deathRecord.CausesOfDeath = causes;
                deathRecord.AutopsyPerformed = false;
                deathRecord.AutopsyResultsAvailable = false;
                code = new Dictionary<string, string>();
                code.Add("code", "7878000");
                code.Add("system", "http://github.com/nightingaleproject/fhirDeathRecord/sdr/causeOfDeath/vs/MannerOfDeathVS");
                code.Add("display", "Accident");
                deathRecord.MannerOfDeath = code;
                code = new Dictionary<string, string>();
                code.Add("code", "373066001");
                code.Add("system", "http://github.com/nightingaleproject/fhirDeathRecord/sdr/causeOfDeath/vs/ContributoryTobaccoUseVS");
                code.Add("display", "Yes");
                deathRecord.TobaccoUseContributedToDeath = code;
                deathRecord.ActualOrPresumedDateOfDeath = "2018-09-01T00:00:00+06:00";
                deathRecord.DatePronouncedDead = "2018-09-01T00:00:00+04:00";
                deathRecord.DeathFromWorkInjury = true;
                code = new Dictionary<string, string>();
                code.Add("code", "236320001");
                code.Add("system", "http://github.com/nightingaleproject/fhirDeathRecord/sdr/causeOfDeath/vs/TransportRelationshipsVS");
                code.Add("display", "Vehicle driver");
                deathRecord.DeathFromTransportInjury = code;
                deathRecord.MedicalExaminerContacted = true;
                code = new Dictionary<string, string>();
                code.Add("code", "PHC1260");
                code.Add("system", "http://github.com/nightingaleproject/fhirDeathRecord/sdr/causeOfDeath/vs/PregnancyStatusVS");
                code.Add("display", "Not pregnant within past year");
                deathRecord.TimingOfRecentPregnancyInRelationToDeath = code;
                Dictionary<string, string> detailsOfInjury = new Dictionary<string, string>();
                detailsOfInjury.Add("detailsOfInjuryPlaceDescription", "Home");
                detailsOfInjury.Add("detailsOfInjuryEffectiveDateTime", "2018-04-19T15:43:00+00:00");
                detailsOfInjury.Add("detailsOfInjuryDescription", "Example details of injury");
                detailsOfInjury.Add("detailsOfInjuryLine1", "7 Example Street");
                detailsOfInjury.Add("detailsOfInjuryLine2", "Line 2");
                detailsOfInjury.Add("detailsOfInjuryCity", "Bedford");
                detailsOfInjury.Add("detailsOfInjuryCounty", "Middlesex");
                detailsOfInjury.Add("detailsOfInjuryState", "Massachusetts");
                detailsOfInjury.Add("detailsOfInjuryZip", "01730");
                detailsOfInjury.Add("detailsOfInjuryCountry", "United States");
                deathRecord.DetailsOfInjury = detailsOfInjury;
                Console.WriteLine(XDocument.Parse(deathRecord.ToXML()).ToString() + "\n\n");
                Console.WriteLine(deathRecord.ToJSON() + "\n\n");
                return 0;
            }
            else if (args.Length == 2 && args[0] == "description")
            {
                DeathRecord d = new DeathRecord(File.ReadAllText(args[1]));
                Console.WriteLine(d.ToDescription());
                return 0;
            }
            else if (args.Length == 2 && args[0] == "2ije")
            {
                DeathRecord d = new DeathRecord(File.ReadAllText(args[1]));
                IJEMortality ije1 = new IJEMortality(d);
                Console.WriteLine(ije1.ToString());
                return 0;
            }
            else if (args.Length == 2 && args[0] == "2naaccr")
            {
                DeathRecord d = new DeathRecord(File.ReadAllText(args[1]));
                NAACCRRecord naaccr = new NAACCRRecord(d);
                naaccr.ConsultNLPService();
                Console.WriteLine(naaccr.ToString());
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
                foreach(PropertyInfo property in properties)
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
                foreach(PropertyInfo property in properties)
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
                Console.WriteLine("Converting FHIR to IJE...\n");
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
                return 0;
            }
            else if (args.Length == 2 && args[0] == "roundtrip-all")
            {
                DeathRecord d1 = new DeathRecord(File.ReadAllText(args[1]));
                DeathRecord d2 = new DeathRecord(d1.ToJSON());
                DeathRecord d3 = new DeathRecord();
                List<PropertyInfo> properties = typeof(DeathRecord).GetProperties().ToList();
                foreach(PropertyInfo property in properties)
                {
                    if (property.Name.Contains("CausesOfDeath") || property.Name.Contains("CertifierQualification"))
                    {
                        continue;
                    }
                    property.SetValue(d3, property.GetValue(d2));
                }
                IJEMortality ije1 = new IJEMortality(d3);
                IJEMortality ije2 = new IJEMortality(ije1.ToString());
                DeathRecord d4 = ije2.ToDeathRecord();

                // We KNOW certain fields just aren't in the IJE, so make sure to ignore them.
                string[] ignoreKeys = { "placeOfDeathFacilityName" };

                int good = 0;
                int bad = 0;

                foreach (PropertyInfo property in properties)
                {
                    if (property.Name.Contains("CausesOfDeath") || property.Name.Contains("CertifierQualification"))
                    {
                        continue;
                    }
                    string one;
                    string two;
                    string three;
                    string four;
                    if (property.PropertyType.ToString() == "System.Collections.Generic.Dictionary`2[System.String,System.String]")
                    {
                        Dictionary<string,string> oneDict = (Dictionary<string,string>)property.GetValue(d1);
                        foreach (string ignoreKey in ignoreKeys)
                        {
                            if (oneDict.ContainsKey(ignoreKey))
                            {
                                oneDict[ignoreKey] = "";
                            }
                        }
                        one = String.Join(", ", oneDict.Select(x => x.Key + "=" + x.Value).ToArray());
                        two = String.Join(", ", ((Dictionary<string,string>)property.GetValue(d2)).Select(x => x.Key + "=" + x.Value).ToArray());
                        three = String.Join(", ", ((Dictionary<string,string>)property.GetValue(d3)).Select(x => x.Key + "=" + x.Value).ToArray());
                        four = String.Join(", ", ((Dictionary<string,string>)property.GetValue(d4)).Select(x => x.Key + "=" + x.Value).ToArray());
                    }
                    else if (property.PropertyType.ToString() == "System.String[]")
                    {
                        one = String.Join(", ", (string[])property.GetValue(d1));
                        two = String.Join(", ", (string[])property.GetValue(d2));
                        three = String.Join(", ", (string[])property.GetValue(d3));
                        four = String.Join(", ", (string[])property.GetValue(d4));
                    }
                    else
                    {
                        one = Convert.ToString(property.GetValue(d1));
                        two = Convert.ToString(property.GetValue(d2));
                        three = Convert.ToString(property.GetValue(d3));
                        four = Convert.ToString(property.GetValue(d4));
                    }
                    if (one.ToLower() != four.ToLower())
                    {
                        Console.WriteLine("[MISMATCH]\t" + $"\"{one}\" (property: {property.Name}) does not equal \"{four}\"" + $"      1:\"{one}\" 2:\"{two}\" 3:\"{three}\" 4:\"{four}\"");
                        bad++;
                        //return 1;
                    }
                    else
                    {
                        Console.WriteLine("[MATCH]\t" + $"\"{one}\" (property: {property.Name}) equals \"{four}\"" + $"      1:\"{one}\" 2:\"{two}\" 3:\"{three}\" 4:\"{four}\"");
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
                List<PropertyInfo> properties = typeof(IJEMortality).GetProperties().ToList().OrderBy(p => ((IJEField)p.GetCustomAttributes().First()).Field).ToList();

                foreach(PropertyInfo property in properties)
                {
                    IJEField info = (IJEField)property.GetCustomAttributes().First();
                    string field = ijeString.Substring(info.Location - 1, info.Length);
                    Console.WriteLine($"{info.Field, -5} {info.Name,-15} {Truncate(info.Contents, 75), -75}: \"{field + "\"",-80}");
                }
            }
            else
            {
                foreach (var path in args)
                {
                    return ReadFile(path);
                }
            }
            return 0;
        }

        private static int ReadFile(string path)
        {
            if (File.Exists(path))
            {
                Console.WriteLine($"Reading file '{path}'");
                string contents = File.ReadAllText(path);
                DeathRecord deathRecord = new DeathRecord(contents);

                // Record Information
                Console.WriteLine($"\tRecord ID: {deathRecord.Id}");

                // Decedent Information
                Console.WriteLine($"\tGiven Name: {string.Join(", ", deathRecord.GivenNames)}");
                Console.WriteLine($"\tLast Name: {deathRecord.FamilyName}");
                Console.WriteLine($"\tGender: {deathRecord.Gender}");
                Console.WriteLine($"\tSSN: {deathRecord.SSN}");
                Console.WriteLine($"\tEthnicity: {deathRecord.Ethnicity}");
                Console.WriteLine($"\tDate of Birth: {deathRecord.DateOfBirth}");
                Console.WriteLine($"\tDate of Death: {deathRecord.DateOfDeath}");

                // Certifier Information
                Console.WriteLine($"\tCertifier Given Name: {deathRecord.CertifierGivenNames}");
                Console.WriteLine($"\tCertifier Last Name: {deathRecord.CertifierFamilyName}");
                Console.WriteLine($"\tCertifier Type: {deathRecord.CertifierType}");

                // Conditions
                Tuple<string, string, Dictionary<string, string>>[] causes = deathRecord.CausesOfDeath;
                foreach (var cause in causes)
                {
                    Console.WriteLine($"\tCause: {cause.Item1}, Onset: {cause.Item2}, Code: {string.Join(", ", cause.Item3)}");
                }
                Console.WriteLine($"\tContributing Conditions: {deathRecord.ContributingConditions}");

                // Observations
                Console.WriteLine($"\tAutopsy Performed: {deathRecord.AutopsyPerformed}");
                Console.WriteLine($"\tAutopsy Results Available: {deathRecord.AutopsyResultsAvailable}");
                Console.WriteLine($"\tActual Or Presumed Date of Death: {deathRecord.ActualOrPresumedDateOfDeath}");
                Console.WriteLine($"\tDate Pronounced Dead: {deathRecord.DatePronouncedDead}");
                Console.WriteLine($"\tDeath Resulted from Injury at Work: {deathRecord.DeathFromWorkInjury}");
                Console.WriteLine($"\tDeath From Transport Injury: {string.Join(", ", deathRecord.DeathFromTransportInjury)}");
                Console.WriteLine($"\tDetails of Injury: {string.Join(", ", deathRecord.DetailsOfInjury)}");
                Console.WriteLine($"\tMedical Examiner Contacted: {deathRecord.MedicalExaminerContacted}");
                Console.WriteLine($"\tTiming of Recent Pregnancy In Relation to Death: {string.Join(", ", deathRecord.TimingOfRecentPregnancyInRelationToDeath)}");
                foreach(var pair in deathRecord.MannerOfDeath)
                {
                    Console.WriteLine($"\tManner of Death key: {pair.Key}: value: {pair.Value}");
                }

                foreach(var pair in deathRecord.TobaccoUseContributedToDeath)
                {
                    Console.WriteLine($"\tTobacco Use Contributed to Death key: {pair.Key}: value: {pair.Value}");
                }

                return 0;
            }
            else
            {
                Console.WriteLine($"Error: File '{path}' does not exist");
                return 1;
            }
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
    }
}
