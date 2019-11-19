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
                DeathRecord deathRecord = new DeathRecord();

                // Identifier
                deathRecord.Identifier = "1";

                // BundleIdentifier
                deathRecord.BundleIdentifier = "42";

                // CertifiedTime
                deathRecord.CertifiedTime = "2019-01-29T16:48:06-05:00";

                // CreatedTime
                deathRecord.CreatedTime = "2019-01-20T16:47:04-05:00";

                // CertificationRole
                Dictionary<string, string> certificationRole = new Dictionary<string, string>();
                certificationRole.Add("code", "76899008");
                certificationRole.Add("system", "http://hl7.org/fhir/ValueSet/performer-role");
                certificationRole.Add("display", "Infectious diseases physician");
                deathRecord.CertificationRole = certificationRole;

                // InterestedPartyIdentifier
                deathRecord.InterestedPartyIdentifier = "1010101";

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

                // CertifierLicenseNumber
                deathRecord.CertifierLicenseNumber = "789123456";

                // CertifierQualification
                Dictionary<string, string> qualification = new Dictionary<string, string>();
                qualification.Add("code", "3060");
                qualification.Add("system", "urn:oid:2.16.840.1.114222.4.11.7186");
                qualification.Add("display", "Physicians and surgeons");
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

                // AliasGivenNames
                deathRecord.AliasGivenNames = new string[] { "FirstNameAlias", "MiddleAlias" };

                // AliasFamilyName
                deathRecord.AliasFamilyName = "LastNameAlias";

                // AliasSuffix
                deathRecord.AliasSuffix = "Jr.";

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
                deathRecord.DateOfBirth = "1940-02-19";

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
                Tuple<string, string>[] race = { Tuple.Create("White", "2106-3"), Tuple.Create("Native Hawaiian or Other Pacific Islander", "2076-8"), Tuple.Create("Native Hawaiian", "2079-2") };
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

                // BirthRecordState
                Dictionary<string, string> brs = new Dictionary<string, string>();
                brs.Add("code", "MA");
                brs.Add("system", "urn:iso:std:iso:3166:-2");
                brs.Add("display", "Massachusetts");
                deathRecord.BirthRecordState = brs;

                // BirthRecordYear
                deathRecord.BirthRecordYear = "1940";

                // UsualOccupation
                Dictionary<string, string> uocc = new Dictionary<string, string>();
                uocc.Add("code", "1340");
                uocc.Add("system", "urn:oid:2.16.840.1.114222.4.11.7186");
                uocc.Add("display", "Biomedical engineers");
                deathRecord.UsualOccupation = uocc;

                // UsualIndustry
                Dictionary<string, string> uind = new Dictionary<string, string>();
                uind.Add("code", "7280");
                uind.Add("system", "urn:oid:2.16.840.1.114222.4.11.7187");
                uind.Add("display", "Accounting, tax preparation, bookkeeping, and payroll services");
                deathRecord.UsualIndustry = uind;

                // MilitaryService
                Dictionary<string, string> mserv = new Dictionary<string, string>();
                mserv.Add("code", "Y");
                mserv.Add("system", "http://hl7.org/fhir/ValueSet/v2-0532");
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
                ddm.Add("system", "urn:oid:2.16.840.1.114222.4.11.7379");
                ddm.Add("display", "Burial");
                deathRecord.DecedentDispositionMethod = ddm;

                // AutopsyPerformedIndicator
                Dictionary<string, string> api = new Dictionary<string, string>();
                api.Add("code", "Y");
                api.Add("system", "http://hl7.org/fhir/ValueSet/v2-0532");
                api.Add("display", "Yes");
                deathRecord.AutopsyPerformedIndicator = api;

                // AutopsyResultsAvailable
                Dictionary<string, string> ara = new Dictionary<string, string>();
                ara.Add("code", "Y");
                ara.Add("system", "http://hl7.org/fhir/ValueSet/v2-0532");
                ara.Add("display", "Yes");
                deathRecord.AutopsyResultsAvailable = ara;

                // AgeAtDeath
                Dictionary<string, string> aad = new Dictionary<string, string>();
                aad.Add("unit", "a");
                aad.Add("value", "79");
                deathRecord.AgeAtDeath = aad;

                // PregnanacyStatus
                Dictionary<string, string> ps = new Dictionary<string, string>();
                ps.Add("code", "NA");
                ps.Add("system", "urn:oid:2.16.840.1.114222.4.11.6003");
                ps.Add("display", "not applicable");
                deathRecord.PregnanacyStatus = ps;

                // TransportationRole
                Dictionary<string, string> tr = new Dictionary<string, string>();
                tr.Add("code", "257500003");
                tr.Add("system", "urn:oid:2.16.840.1.114222.4.11.6005");
                tr.Add("display", "Passenger");
                deathRecord.TransportationRole = tr;

                // ExaminerContacted
                deathRecord.ExaminerContacted = false;

                // TobaccoUse
                Dictionary<string, string> tbu = new Dictionary<string, string>();
                tbu.Add("code", "373066001");
                tbu.Add("system", "urn:oid:2.16.840.1.114222.4.11.6004");
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

                // InjuryDate
                deathRecord.InjuryDate = "2018-02-19T16:48:06-05:00";

                // InjuryPlace
                Dictionary<string, string> ip = new Dictionary<string, string>();
                ip.Add("code", "0");
                ip.Add("system", "urn:oid:2.16.840.1.114222.4.11.7374");
                ip.Add("display", "Home");
                deathRecord.InjuryPlace = ip;

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
                deathRecord.DateOfDeath = "2018-02-19T16:48:06-05:00";

                // DateOfDeathPronouncement
                deathRecord.DateOfDeathPronouncement = "2018-02-20T16:48:06-05:00";

                Console.WriteLine(XDocument.Parse(deathRecord.ToXML()).ToString() + "\n\n");
                //Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(Newtonsoft.Json.JsonConvert.DeserializeObject(deathRecord.ToJSON()), Newtonsoft.Json.Formatting.Indented) + "\n\n");
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
            else if (args.Length == 1 && args[0] == "fakejson")
            {
                DeathRecordFaker faker = new DeathRecordFaker();
                DeathRecord d = faker.Generate();
                Console.WriteLine(d.ToJSON());
                return 0;
            }
            else if (args.Length == 1 && args[0] == "fakexml")
            {
                DeathRecordFaker faker = new DeathRecordFaker();
                DeathRecord d = faker.Generate();
                Console.WriteLine(d.ToXML());
                return 0;
            }
            else if (args.Length == 1 && args[0] == "fakeije")
            {
                DeathRecordFaker faker = new DeathRecordFaker();
                DeathRecord d = faker.Generate();
                IJEMortality ije = new IJEMortality(d);
                Console.WriteLine(ije.ToString());
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
                return issues;
            }
            else if (args.Length == 2 && args[0] == "roundtrip-all")
            {
                DeathRecord d1 = new DeathRecord(File.ReadAllText(args[1]));
                DeathRecord d2 = new DeathRecord(d1.ToJSON());
                DeathRecord d3 = new DeathRecord();
                List<PropertyInfo> properties = typeof(DeathRecord).GetProperties().ToList();
                foreach(PropertyInfo property in properties)
                {
                    if (property.Name.Contains("CausesOfDeath"))
                    {
                        continue;
                    }
                    property.SetValue(d3, property.GetValue(d2));
                }

                int good = 0;
                int bad = 0;

                foreach (PropertyInfo property in properties)
                {
                    if (property.Name.Contains("CausesOfDeath"))
                    {
                        continue;
                    }
                    string one;
                    string two;
                    string three;
                    if (property.PropertyType.ToString() == "System.Collections.Generic.Dictionary`2[System.String,System.String]")
                    {
                        Dictionary<string,string> oneDict = (Dictionary<string,string>)property.GetValue(d1);
                        one = String.Join(", ", oneDict.Select(x => x.Key + "=" + x.Value).ToArray());
                        two = String.Join(", ", ((Dictionary<string,string>)property.GetValue(d2)).Select(x => x.Key + "=" + x.Value).ToArray());
                        three = String.Join(", ", ((Dictionary<string,string>)property.GetValue(d3)).Select(x => x.Key + "=" + x.Value).ToArray());
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
                        Console.WriteLine("[MISMATCH]\t" + $"\"{one}\" (property: {property.Name}) does not equal \"{three}\"" + $"      1:\"{one}\" 2:\"{two}\" 3:\"{three}\"");
                        bad++;
                    }
                    else
                    {
                        Console.WriteLine("[MATCH]\t" + $"\"{one}\" (property: {property.Name}) equals \"{three}\"" + $"      1:\"{one}\" 2:\"{two}\" 3:\"{three}\"");
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
            return 0;
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
