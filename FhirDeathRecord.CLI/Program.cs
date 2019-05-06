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

                // Identifier
                deathRecord.Identifier = Guid.NewGuid().ToString();

                // BundleIdentifier
                deathRecord.BundleIdentifier = Guid.NewGuid().ToString();

                // CertifiedTime
                deathRecord.CertifiedTime = "2019-01-29T16:48:06.4988220-05:00";

                // CreatedTime
                deathRecord.CreatedTime = "2019-01-20T16:47:04.4988220-05:00";

                // CertifierRole
                Dictionary<string, string> certifierRole = new Dictionary<string, string>();
                certifierRole.Add("code", "309343006");
                certifierRole.Add("system", "http://snomed.info/sct");
                certifierRole.Add("display", "Physician");
                deathRecord.CertifierRole = certifierRole;

                // InterestedPartyIdentifier
                deathRecord.InterestedPartyIdentifier = Guid.NewGuid().ToString();

                // InterestedPartyName
                deathRecord.InterestedPartyName = "Example Hospital";

                // InterestedPartyAddress
                Dictionary<string, string> address = new Dictionary<string, string>();
                address.Add("addressLine1", "10 Example Street");
                address.Add("addressLine2", "Line 2");
                address.Add("addressCity", "Bedford");
                address.Add("addressCounty", "Middlesex");
                address.Add("addressState", "Massachusetts");
                address.Add("addressZip", "01730");
                address.Add("addressCountry", "United States");
                deathRecord.InterestedPartyAddress = address;

                // InterestedPartyType
                Dictionary<string, string> type = new Dictionary<string, string>();
                type.Add("code", "prov");
                type.Add("system", "http://terminology.hl7.org/CodeSystem/organization-type");
                type.Add("display", "Healthcare Provider");
                deathRecord.InterestedPartyType = type;

                // MannerOfDeathType
                Dictionary<string, string> mannerOfDeathType = new Dictionary<string, string>();
                mannerOfDeathType.Add("code", "7878000");
                mannerOfDeathType.Add("display", "Accident");
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
                caddress.Add("addressState", "Massachusetts");
                caddress.Add("addressZip", "01730");
                caddress.Add("addressCountry", "United States");
                deathRecord.CertifierAddress = caddress;

                // CertifierQualification
                Dictionary<string, string> qualification = new Dictionary<string, string>();
                qualification.Add("code", "MD");
                qualification.Add("system", "http://hl7.org/fhir/v2/0360/2.7");
                qualification.Add("display", "Doctor of Medicine");
                deathRecord.CertifierQualification = qualification;

                // ContributingConditions
                deathRecord.ContributingConditions = "Example Contributing Conditions";

                // COD1A
                deathRecord.COD1A = "Rupture of myocardium";

                // INTERVAL1A
                deathRecord.INTERVAL1A = "minutes";

                // CODE1A
                Dictionary<string, string> code1a = new Dictionary<string, string>();
                code1a.Add("code", "I21.0");
                code1a.Add("system", "http://hl7.org/fhir/sid/icd-10");
                code1a.Add("display", "Acute transmural myocardial infarction of anterior wall");
                deathRecord.CODE1A = code1a;

                // COD1B
                deathRecord.COD1B = "Acute myocardial infarction";

                // INTERVAL1B
                deathRecord.INTERVAL1B = "6 days";

                // CODE1B
                Dictionary<string, string> code1b = new Dictionary<string, string>();
                code1b.Add("code", "I21.9");
                code1b.Add("system", "http://hl7.org/fhir/sid/icd-10");
                code1b.Add("display", "Acute myocardial infarction, unspecified");
                deathRecord.CODE1B = code1b;

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
                deathRecord.Gender = "male";

                // BirthSex
                Dictionary<string, string> bscode = new Dictionary<string, string>();
                bscode.Add("code", "M");
                bscode.Add("system", "http://hl7.org/fhir/us/core/ValueSet/us-core-birthsex");
                bscode.Add("display", "Male");
                deathRecord.BirthSex = bscode;

                // DateOfBirth
                deathRecord.DateOfBirth = "1940-02-19T16:48:06.4988220-05:00";

                // Residence
                Dictionary<string, string> raddress = new Dictionary<string, string>();
                raddress.Add("addressLine1", "101 Example Street");
                raddress.Add("addressLine2", "Line 2");
                raddress.Add("addressCity", "Bedford");
                raddress.Add("addressCounty", "Middlesex");
                raddress.Add("addressState", "Massachusetts");
                raddress.Add("addressZip", "01730");
                raddress.Add("addressCountry", "United States");
                deathRecord.Residence = raddress;

                // SSN
                deathRecord.SSN = "123456789";

                // Ethnicity
                Tuple<string, string>[] ethnicity = { Tuple.Create("Hispanic or Latino", "2135-2"), Tuple.Create("Puerto Rican", "2180-8") };
                deathRecord.Ethnicity = ethnicity;

                // Race
                Tuple<string, string>[] race = { Tuple.Create("White", "2106-3"), Tuple.Create("Native Hawaiian or Other Pacific Islander", "2076-8") };
                deathRecord.Race = race;

                // PlaceOfBirth
                Dictionary<string, string> pobaddress = new Dictionary<string, string>();
                pobaddress.Add("addressLine1", "1011 Example Street");
                pobaddress.Add("addressLine2", "Line 2");
                pobaddress.Add("addressCity", "Bedford");
                pobaddress.Add("addressCounty", "Middlesex");
                pobaddress.Add("addressState", "Massachusetts");
                pobaddress.Add("addressZip", "01730");
                pobaddress.Add("addressCountry", "United States");
                deathRecord.PlaceOfBirth = pobaddress;

                // MaritalStatus
                Dictionary<string, string> mscode = new Dictionary<string, string>();
                mscode.Add("code", "S");
                mscode.Add("system", "http://hl7.org/fhir/v3/MaritalStatus");
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
                elevel.Add("system", "http://hl7.org/fhir/v3/EducationLevel");
                elevel.Add("display", "College or baccalaureate degree complete");
                deathRecord.EducationLevel = elevel;

                // BirthRecordId
                deathRecord.BirthRecordId = "4242123";

                // UsualOccupation
                Dictionary<string, string> uocc = new Dictionary<string, string>();
                uocc.Add("code", "7280");
                uocc.Add("system", "http://www.hl7.org/fhir/ValueSet/Usual-occupation");
                uocc.Add("display", "Accounting, tax preparation, bookkeeping, and payroll services");
                deathRecord.UsualOccupation = uocc;

                // UsualIndustry
                Dictionary<string, string> uind = new Dictionary<string, string>();
                uind.Add("code", "1320");
                uind.Add("system", "http://www.hl7.org/fhir/ValueSet/industry-cdc-census-2010");
                uind.Add("display", "Aerospace engineers");
                deathRecord.UsualIndustry = uind;

                // MilitaryService
                Dictionary<string, string> mserv = new Dictionary<string, string>();
                mserv.Add("code", "Y");
                mserv.Add("system", "http://www.hl7.org/fhir/ValueSet/v2-0532");
                mserv.Add("display", "Yes");
                deathRecord.MilitaryService = mserv;

                // MorticianGivenNames
                string[] fdnames = { "FD", "Middle" };
                deathRecord.MorticianGivenNames = fdnames;

                // MorticianFamilyName
                deathRecord.MorticianFamilyName = "Last";

                // MorticianSuffix
                deathRecord.MorticianSuffix = "Jr.";

                // MorticianIdentifier
                deathRecord.MorticianIdentifier = "9876543210";

                // FuneralHomeAddress
                Dictionary<string, string> fdaddress = new Dictionary<string, string>();
                fdaddress.Add("addressLine1", "1011010 Example Street");
                fdaddress.Add("addressLine2", "Line 2");
                fdaddress.Add("addressCity", "Bedford");
                fdaddress.Add("addressCounty", "Middlesex");
                fdaddress.Add("addressState", "Massachusetts");
                fdaddress.Add("addressZip", "01730");
                fdaddress.Add("addressCountry", "United States");
                deathRecord.FuneralHomeAddress = fdaddress;

                // FuneralHomeName
                deathRecord.FuneralHomeName = "Smith Funeral Home";

                // DispositionLocationAddress
                Dictionary<string, string> dladdress = new Dictionary<string, string>();
                dladdress.Add("addressLine1", "603 Example Street");
                dladdress.Add("addressLine2", "Line 2");
                dladdress.Add("addressCity", "Bedford");
                dladdress.Add("addressCounty", "Middlesex");
                dladdress.Add("addressState", "Massachusetts");
                dladdress.Add("addressZip", "01730");
                dladdress.Add("addressCountry", "United States");
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
                api.Add("system", "http://www.hl7.org/fhir/ValueSet/v2-0532");
                api.Add("display", "Yes");
                deathRecord.AutopsyPerformedIndicator = api;

                // AutopsyResultsAvailable
                Dictionary<string, string> ara = new Dictionary<string, string>();
                ara.Add("code", "Y");
                ara.Add("system", "http://www.hl7.org/fhir/ValueSet/v2-0532");
                ara.Add("display", "Yes");
                deathRecord.AutopsyResultsAvailable = ara;

                // AgeAtDeath
                Dictionary<string, string> aad = new Dictionary<string, string>();
                aad.Add("unit", "a");
                aad.Add("value", "100");
                deathRecord.AgeAtDeath = aad;

                // PregnanacyStatus
                Dictionary<string, string> ps = new Dictionary<string, string>();
                ps.Add("code", "PHC1260");
                ps.Add("system", "http://www.hl7.org/fhir/stu3/valueset-PregnancyStatusVS");
                ps.Add("display", "Not pregnant within past year");
                deathRecord.PregnanacyStatus = ps;

                // TransportationRole
                Dictionary<string, string> tr = new Dictionary<string, string>();
                tr.Add("code", "example-code");
                tr.Add("system", "http://www.hl7.org/fhir/stu3/valueset-TransportationRelationships");
                tr.Add("display", "Example Code");
                deathRecord.TransportationRole = tr;

                // ExaminerContacted
                deathRecord.ExaminerContacted = false;

                // TobaccoUse
                Dictionary<string, string> tbu = new Dictionary<string, string>();
                tbu.Add("code", "Y");
                tbu.Add("system", "http://www.hl7.org/fhir/ValueSet/v2-0532");
                tbu.Add("display", "Yes");
                deathRecord.TobaccoUse = tbu;

                // InjuryLocationAddress
                Dictionary<string, string> iladdress = new Dictionary<string, string>();
                iladdress.Add("addressLine1", "781 Example Street");
                iladdress.Add("addressLine2", "Line 2");
                iladdress.Add("addressCity", "Bedford");
                iladdress.Add("addressCounty", "Middlesex");
                iladdress.Add("addressState", "Massachusetts");
                iladdress.Add("addressZip", "01730");
                iladdress.Add("addressCountry", "United States");
                deathRecord.InjuryLocationAddress = iladdress;

                // InjuryLocationName
                deathRecord.InjuryLocationName = "Example Injury Location Name";

                // InjuryLocationDescription
                deathRecord.InjuryLocationDescription = "Example Injury Location Description";

                // DeathLocationAddress
                Dictionary<string, string> dtladdress = new Dictionary<string, string>();
                dtladdress.Add("addressLine1", "671 Example Street");
                dtladdress.Add("addressLine2", "Line 2");
                dtladdress.Add("addressCity", "Bedford");
                dtladdress.Add("addressCounty", "Middlesex");
                dtladdress.Add("addressState", "Massachusetts");
                dtladdress.Add("addressZip", "01730");
                dtladdress.Add("addressCountry", "United States");
                deathRecord.DeathLocationAddress = dtladdress;

                // DeathLocationName
                deathRecord.DeathLocationName = "Example Death Location Name";

                // DeathLocationDescription
                deathRecord.DeathLocationDescription = "Example Death Location Description";

                // DateOfDeath
                deathRecord.DateOfDeath = "2018-02-19T16:48:06.4988220-05:00";

                // DateOfDeathPronouncement
                deathRecord.DateOfDeathPronouncement = "2018-02-20T16:48:06.4988220-05:00";

                Console.WriteLine(XDocument.Parse(deathRecord.ToXML()).ToString() + "\n\n");
                //Console.WriteLine(deathRecord.ToJSON() + "\n\n");
                return 0;
            }
            // else if (args.Length == 2 && args[0] == "description")
            // {
            //     DeathRecord d = new DeathRecord(File.ReadAllText(args[1]));
            //     Console.WriteLine(d.ToDescription());
            //     return 0;
            // }
            // else if (args.Length == 2 && args[0] == "2ije")
            // {
            //     DeathRecord d = new DeathRecord(File.ReadAllText(args[1]));
            //     IJEMortality ije1 = new IJEMortality(d);
            //     Console.WriteLine(ije1.ToString());
            //     return 0;
            // }
            // else if (args.Length == 2 && args[0] == "ije2xml")
            // {
            //     IJEMortality ije1 = new IJEMortality(File.ReadAllText(args[1]));
            //     DeathRecord d = ije1.ToDeathRecord();
            //     Console.WriteLine(XDocument.Parse(d.ToXML()).ToString());
            //     return 0;
            // }
            // else if (args.Length == 2 && args[0] == "ije2json")
            // {
            //     IJEMortality ije1 = new IJEMortality(File.ReadAllText(args[1]));
            //     DeathRecord d = ije1.ToDeathRecord();
            //     Console.WriteLine(d.ToJSON());
            //     return 0;
            // }
            // else if (args.Length == 2 && args[0] == "json2xml")
            // {
            //     DeathRecord d = new DeathRecord(File.ReadAllText(args[1]));
            //     Console.WriteLine(XDocument.Parse(d.ToXML()).ToString());
            //     return 0;
            // }
            // else if (args.Length == 2 && args[0] == "checkXml")
            // {
            //     DeathRecord d = new DeathRecord(File.ReadAllText(args[1]), true);
            //     Console.WriteLine(XDocument.Parse(d.ToXML()).ToString());
            //     return 0;
            // }
            // else if (args.Length == 2 && args[0] == "checkJson")
            // {
            //     DeathRecord d = new DeathRecord(File.ReadAllText(args[1]), true);
            //     Console.WriteLine(d.ToJSON());
            //     return 0;
            // }
            // else if (args.Length == 2 && args[0] == "xml2json")
            // {
            //     DeathRecord d = new DeathRecord(File.ReadAllText(args[1]));
            //     Console.WriteLine(d.ToJSON());
            //     return 0;
            // }
            // else if (args.Length == 2 && args[0] == "xml2xml")
            // {
            //     // Forces record through getters and then setters, prints as xml
            //     DeathRecord indr = new DeathRecord(File.ReadAllText(args[1]));
            //     DeathRecord outdr = new DeathRecord();
            //     List<PropertyInfo> properties = typeof(DeathRecord).GetProperties().ToList();
            //     foreach(PropertyInfo property in properties)
            //     {
            //         if (property.Name.Contains("CausesOfDeath") || property.Name.Contains("CertifierQualification"))
            //         {
            //             continue;
            //         }
            //         property.SetValue(outdr, property.GetValue(indr));
            //     }
            //     Console.WriteLine(XDocument.Parse(outdr.ToXML()).ToString());
            //     return 0;
            // }
            // else if (args.Length == 2 && args[0] == "json2json")
            // {
            //     // Forces record through getters and then setters, prints as JSON
            //     DeathRecord indr = new DeathRecord(File.ReadAllText(args[1]));
            //     DeathRecord outdr = new DeathRecord();
            //     List<PropertyInfo> properties = typeof(DeathRecord).GetProperties().ToList();
            //     foreach(PropertyInfo property in properties)
            //     {
            //         if (property.Name.Contains("CausesOfDeath") || property.Name.Contains("CertifierQualification"))
            //         {
            //             continue;
            //         }
            //         property.SetValue(outdr, property.GetValue(indr));
            //     }
            //     Console.WriteLine(outdr.ToJSON());
            //     return 0;
            // }
            // else if (args.Length == 2 && args[0] == "roundtrip-ije")
            // {
            //     Console.WriteLine("Converting FHIR to IJE...\n");
            //     DeathRecord d = new DeathRecord(File.ReadAllText(args[1]));
            //     //Console.WriteLine(XDocument.Parse(d.ToXML()).ToString() + "\n");
            //     IJEMortality ije1 = new IJEMortality(d);
            //     //Console.WriteLine(ije1.ToString() + "\n\n");
            //     IJEMortality ije2 = new IJEMortality(ije1.ToString());
            //     //Console.WriteLine(ije2.ToString() + "\n\n");
            //     //Console.WriteLine(XDocument.Parse(ije2.ToDeathRecord().ToXML()).ToString() + "\n");
            //     IJEMortality ije3 = new IJEMortality(new DeathRecord(ije2.ToDeathRecord().ToXML()));
            //     int issues = 0;
            //     int total = 0;
            //     foreach(PropertyInfo property in typeof(IJEMortality).GetProperties())
            //     {
            //         string val1 = Convert.ToString(property.GetValue(ije1, null));
            //         string val2 = Convert.ToString(property.GetValue(ije2, null));
            //         string val3 = Convert.ToString(property.GetValue(ije3, null));

            //         IJEField info = (IJEField)property.GetCustomAttributes().First();

            //         if (val1.ToUpper() != val2.ToUpper() || val1.ToUpper() != val3.ToUpper() || val2.ToUpper() != val3.ToUpper())
            //         {
            //             issues++;
            //             Console.WriteLine($"[MISMATCH]\t{info.Name}: {info.Contents} \t\t\"{val1}\" != \"{val2}\" != \"{val3}\"");
            //         }
            //         total++;
            //     }
            //     Console.WriteLine($"\n{issues} issues out of {total} total fields.");
            //     return 0;
            // }
            // else if (args.Length == 2 && args[0] == "roundtrip-all")
            // {
            //     DeathRecord d1 = new DeathRecord(File.ReadAllText(args[1]));
            //     DeathRecord d2 = new DeathRecord(d1.ToJSON());
            //     DeathRecord d3 = new DeathRecord();
            //     List<PropertyInfo> properties = typeof(DeathRecord).GetProperties().ToList();
            //     foreach(PropertyInfo property in properties)
            //     {
            //         if (property.Name.Contains("CausesOfDeath") || property.Name.Contains("CertifierQualification"))
            //         {
            //             continue;
            //         }
            //         property.SetValue(d3, property.GetValue(d2));
            //     }
            //     IJEMortality ije1 = new IJEMortality(d3);
            //     IJEMortality ije2 = new IJEMortality(ije1.ToString());
            //     DeathRecord d4 = ije2.ToDeathRecord();

            //     // We KNOW certain fields just aren't in the IJE, so make sure to ignore them.
            //     string[] ignoreKeys = { "placeOfDeathFacilityName" };

            //     int good = 0;
            //     int bad = 0;

            //     foreach (PropertyInfo property in properties)
            //     {
            //         if (property.Name.Contains("CausesOfDeath") || property.Name.Contains("CertifierQualification"))
            //         {
            //             continue;
            //         }
            //         string one;
            //         string two;
            //         string three;
            //         string four;
            //         if (property.PropertyType.ToString() == "System.Collections.Generic.Dictionary`2[System.String,System.String]")
            //         {
            //             Dictionary<string,string> oneDict = (Dictionary<string,string>)property.GetValue(d1);
            //             foreach (string ignoreKey in ignoreKeys)
            //             {
            //                 if (oneDict.ContainsKey(ignoreKey))
            //                 {
            //                     oneDict[ignoreKey] = "";
            //                 }
            //             }
            //             one = String.Join(", ", oneDict.Select(x => x.Key + "=" + x.Value).ToArray());
            //             two = String.Join(", ", ((Dictionary<string,string>)property.GetValue(d2)).Select(x => x.Key + "=" + x.Value).ToArray());
            //             three = String.Join(", ", ((Dictionary<string,string>)property.GetValue(d3)).Select(x => x.Key + "=" + x.Value).ToArray());
            //             four = String.Join(", ", ((Dictionary<string,string>)property.GetValue(d4)).Select(x => x.Key + "=" + x.Value).ToArray());
            //         }
            //         else if (property.PropertyType.ToString() == "System.String[]")
            //         {
            //             one = String.Join(", ", (string[])property.GetValue(d1));
            //             two = String.Join(", ", (string[])property.GetValue(d2));
            //             three = String.Join(", ", (string[])property.GetValue(d3));
            //             four = String.Join(", ", (string[])property.GetValue(d4));
            //         }
            //         else
            //         {
            //             one = Convert.ToString(property.GetValue(d1));
            //             two = Convert.ToString(property.GetValue(d2));
            //             three = Convert.ToString(property.GetValue(d3));
            //             four = Convert.ToString(property.GetValue(d4));
            //         }
            //         if (one.ToLower() != four.ToLower())
            //         {
            //             Console.WriteLine("[MISMATCH]\t" + $"\"{one}\" (property: {property.Name}) does not equal \"{four}\"" + $"      1:\"{one}\" 2:\"{two}\" 3:\"{three}\" 4:\"{four}\"");
            //             bad++;
            //             //return 1;
            //         }
            //         else
            //         {
            //             Console.WriteLine("[MATCH]\t" + $"\"{one}\" (property: {property.Name}) equals \"{four}\"" + $"      1:\"{one}\" 2:\"{two}\" 3:\"{three}\" 4:\"{four}\"");
            //             good++;
            //         }
            //     }
            //     Console.WriteLine($"\n{bad} mismatches out of {good + bad} total properties checked.");
            //     if (bad > 0)
            //     {
            //         return 1;
            //     }
            //     else
            //     {
            //         return 0;
            //     }
            // }
            // else if (args.Length == 2 && args[0] == "ije")
            // {
            //     string ijeString = File.ReadAllText(args[1]);
            //     List<PropertyInfo> properties = typeof(IJEMortality).GetProperties().ToList().OrderBy(p => ((IJEField)p.GetCustomAttributes().First()).Field).ToList();

            //     foreach(PropertyInfo property in properties)
            //     {
            //         IJEField info = (IJEField)property.GetCustomAttributes().First();
            //         string field = ijeString.Substring(info.Location - 1, info.Length);
            //         Console.WriteLine($"{info.Field, -5} {info.Name,-15} {Truncate(info.Contents, 75), -75}: \"{field + "\"",-80}");
            //     }
            // }
            // else
            // {
            //     foreach (var path in args)
            //     {
            //         return ReadFile(path);
            //     }
            // }
            return 0;
        }

        private static int ReadFile(string path)
        {
            if (File.Exists(path))
            {
                // Console.WriteLine($"Reading file '{path}'");
                // string contents = File.ReadAllText(path);
                // DeathRecord deathRecord = new DeathRecord(contents);

                // // Record Information
                // Console.WriteLine($"\tRecord ID: {deathRecord.Id}");

                // // Decedent Information
                // Console.WriteLine($"\tGiven Name: {string.Join(", ", deathRecord.GivenNames)}");
                // Console.WriteLine($"\tLast Name: {deathRecord.FamilyName}");
                // Console.WriteLine($"\tGender: {deathRecord.Gender}");
                // Console.WriteLine($"\tSSN: {deathRecord.SSN}");
                // Console.WriteLine($"\tEthnicity: {deathRecord.Ethnicity}");
                // Console.WriteLine($"\tDate of Birth: {deathRecord.DateOfBirth}");
                // Console.WriteLine($"\tDate of Death: {deathRecord.DateOfDeath}");

                // // Certifier Information
                // Console.WriteLine($"\tCertifier Given Name: {deathRecord.CertifierGivenNames}");
                // Console.WriteLine($"\tCertifier Last Name: {deathRecord.CertifierFamilyName}");
                // Console.WriteLine($"\tCertifier Type: {deathRecord.CertifierType}");

                // // Conditions
                // Tuple<string, string, Dictionary<string, string>>[] causes = deathRecord.CausesOfDeath;
                // foreach (var cause in causes)
                // {
                //     Console.WriteLine($"\tCause: {cause.Item1}, Onset: {cause.Item2}, Code: {string.Join(", ", cause.Item3)}");
                // }
                // Console.WriteLine($"\tContributing Conditions: {deathRecord.ContributingConditions}");

                // // Observations
                // Console.WriteLine($"\tAutopsy Performed: {deathRecord.AutopsyPerformed}");
                // Console.WriteLine($"\tAutopsy Results Available: {deathRecord.AutopsyResultsAvailable}");
                // Console.WriteLine($"\tActual Or Presumed Date of Death: {deathRecord.ActualOrPresumedDateOfDeath}");
                // Console.WriteLine($"\tDate Pronounced Dead: {deathRecord.DatePronouncedDead}");
                // Console.WriteLine($"\tDeath Resulted from Injury at Work: {deathRecord.DeathFromWorkInjury}");
                // Console.WriteLine($"\tDeath From Transport Injury: {string.Join(", ", deathRecord.DeathFromTransportInjury)}");
                // Console.WriteLine($"\tDetails of Injury: {string.Join(", ", deathRecord.DetailsOfInjury)}");
                // Console.WriteLine($"\tMedical Examiner Contacted: {deathRecord.MedicalExaminerContacted}");
                // Console.WriteLine($"\tTiming of Recent Pregnancy In Relation to Death: {string.Join(", ", deathRecord.TimingOfRecentPregnancyInRelationToDeath)}");
                // foreach(var pair in deathRecord.MannerOfDeath)
                // {
                //     Console.WriteLine($"\tManner of Death key: {pair.Key}: value: {pair.Value}");
                // }

                // foreach(var pair in deathRecord.TobaccoUseContributedToDeath)
                // {
                //     Console.WriteLine($"\tTobacco Use Contributed to Death key: {pair.Key}: value: {pair.Value}");
                // }

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
