using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Reflection;
using System.Net.Http;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.ElementModel;
using Hl7.FhirPath;
using VRDR;

namespace VRDR.CLI
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

                // InterestedPartyIdentifier
                var ipId = new Dictionary<string, string>();
                ipId["system"] = "http://hl7.org/fhir/sid/us-npi";
                ipId["value"] = "0000000000";
                deathRecord.InterestedPartyIdentifier = ipId;

                // InterestedPartyName
                deathRecord.InterestedPartyName = "Example Hospital";

                // InterestedPartyAddress
                Dictionary<string, string> address = new Dictionary<string, string>();
                address.Add("addressLine1", "10 Example Street");
                address.Add("addressLine2", "Line 2");
                address.Add("addressCity", "Bedford");
                address.Add("addressCounty", "Middlesex");
                address.Add("addressState", "MA");
                address.Add("addressZip", "01730");
                address.Add("addressCountry", "US");
                deathRecord.InterestedPartyAddress = address;

                // InterestedPartyType
                Dictionary<string, string> type = new Dictionary<string, string>();
                type.Add("code", "prov");
                type.Add("system", "http://terminology.hl7.org/CodeSystem/organization-type");
                type.Add("display", "Healthcare Provider");
                deathRecord.InterestedPartyType = type;

                // State Local Identifier
                deathRecord.StateLocalIdentifier = "42";

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

                // CertifierLicenseNumber
                deathRecord.CertifierLicenseNumber = "789123456";

                // CertifierQualification
                Dictionary<string, string> qualification = new Dictionary<string, string>();
                qualification.Add("code", "434641000124105");
                qualification.Add("system", "http://snomed.info/sct");
                qualification.Add("display", "Physician certified and pronounced death certificate");
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
                deathRecord.BirthSex = "F";

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
                deathRecord.ResidenceWithinCityLimitsBoolean = false;

                // SSN
                deathRecord.SSN = "123456789";

                // Ethnicity
                Tuple<string, string>[] ethnicity = { Tuple.Create("Hispanic or Latino", "2135-2"), Tuple.Create("Puerto Rican", "2180-8") };
                deathRecord.Ethnicity = ethnicity;

                // Race
                Tuple<string, string>[] race = { Tuple.Create("White", "2106-3"), Tuple.Create("Native Hawaiian or Other Pacific Islander", "2076-8"), Tuple.Create("Native Hawaiian", "2079-2") };
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
                Dictionary<string, string> brs = new Dictionary<string, string>();
                brs.Add("code", "US-MA");
                brs.Add("system", "urn:iso:std:iso:3166:-2");
                brs.Add("display", "Massachusetts");
                deathRecord.BirthRecordState = brs;

                // BirthRecordYear
                deathRecord.BirthRecordYear = "1940";

                // UsualOccupation
                deathRecord.UsualOccupation = "secretary";
                deathRecord.UsualOccupationStart = "1965-01-01";
                deathRecord.UsualOccupationEnd = "2010-01-01";

                // UsualIndustry
                deathRecord.UsualIndustry = "State agency";

                // MilitaryService
                Dictionary<string, string> mserv = new Dictionary<string, string>();
                mserv.Add("code", "Y");
                mserv.Add("system", "http://terminology.hl7.org/CodeSystem/v2-0136");
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
                var mortId = new Dictionary<string, string>();
                mortId["value"] = "9876543210";
                mortId["system"] = "http://hl7.org/fhir/sid/us-npi";
                deathRecord.MorticianIdentifier = mortId;

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
                deathRecord.FuneralDirectorPhone = "000-000-0000";

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
                deathRecord.ExaminerContactedBoolean = false;

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
                deathRecord.InjuryLocationDescription = "Example Injury Location Description";

                // InjuryDate
                deathRecord.InjuryDate = "2018-02-19T16:48:06-05:00";

                // InjuryAtWork
                Dictionary<string, string> codeIW = new Dictionary<string, string>();
                codeIW.Add("code", "N");
                codeIW.Add("system", "http://terminology.hl7.org/CodeSystem/v2-0136");
                codeIW.Add("display", "No");
                deathRecord.InjuryAtWork = codeIW;

                // TransportationInjury
                Dictionary<string, string> codeTI = new Dictionary<string, string>();
                codeTI.Add("code", "Y");
                codeTI.Add("system", "http://terminology.hl7.org/CodeSystem/v2-0136");
                codeTI.Add("display", "Yes");
                deathRecord.TransportationEvent = codeTI;

                // InjuryPlace
                Dictionary<string, string> ip = new Dictionary<string, string>();
                ip["code"] = "0";
                ip["system"] = "urn:oid:2.16.840.1.114222.4.5.320";
                ip["display"] = "Home";
                deathRecord.InjuryPlace = ip;

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
                aad.Add("unit", "a");
                aad.Add("value", "79");
                deathRecord.AgeAtDeath = aad;

                // DateOfDeathPronouncement
                deathRecord.DateOfDeathPronouncement = "2018-02-20T16:48:06-05:00";

                // PronouncerGivenNames
                string[] pronouncer_gnames = { "FD", "Middle" };
                deathRecord.PronouncerGivenNames = pronouncer_gnames;

                // PronouncerFamilyName
                deathRecord.PronouncerFamilyName = "Last";

                // PronouncerSuffix
                deathRecord.PronouncerSuffix = "Jr.";

                // PronouncerIdentifier
                var pronouncerId = new Dictionary<string, string>();
                pronouncerId["value"] = "0000000000";
                pronouncerId["system"] = "http://hl7.org/fhir/sid/us-npi";
                deathRecord.PronouncerIdentifier = pronouncerId;

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
                // Console.WriteLine("Converting FHIR to IJE...\n");
                DeathRecord d = new DeathRecord(File.ReadAllText(args[1]));
                IJEMortality ije1, ije2, ije3;
                try {
                    ije1 = new IJEMortality(d);
                    ije2 = new IJEMortality(ije1.ToString());
                    ije3 = new IJEMortality(new DeathRecord(ije2.ToDeathRecord().ToXML()));
                } catch (Exception e){
                    Console.Error.WriteLine(e.Message);
                    return (1);
                }

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
                    if (property.Name.Contains("CausesOfDeath") || property.Name.Contains("Boolean"))
                    {
                        continue;
                    }
                    property.SetValue(d3, property.GetValue(d2));
                }

                int good = 0;
                int bad = 0;

                foreach (PropertyInfo property in properties)
                {
                    if (property.Name.Contains("CausesOfDeath") || property.Name.Contains("Boolean"))
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
            else if (args.Length == 3 && args[0] == "compare")
            {
                string ijeString1 = File.ReadAllText(args[1]);

                DeathRecord record2 = new DeathRecord(File.ReadAllText(args[2]));
                IJEMortality ije2 = new IJEMortality(record2);
                string ijeString2 = ije2.ToString();

                List<PropertyInfo> properties = typeof(IJEMortality).GetProperties().ToList().OrderBy(p => ((IJEField)p.GetCustomAttributes().First()).Field).ToList();

                int differences = 0;

                foreach(PropertyInfo property in properties)
                {
                    IJEField info = (IJEField)property.GetCustomAttributes().First();
                    string field1 = ijeString1.Substring(info.Location - 1, info.Length);
                    string field2 = ijeString2.Substring(info.Location - 1, info.Length);
                    if (field1 != field2)
                    {
                        differences += 1;
                        Console.WriteLine($"1: {info.Field, -5} {info.Name,-15} {Truncate(info.Contents, 75), -75}: \"{field1 + "\"",-80}");
                        Console.WriteLine($"2: {info.Field, -5} {info.Name,-15} {Truncate(info.Contents, 75), -75}: \"{field2 + "\"",-80}");
                        Console.WriteLine();
                    }
                }
                Console.WriteLine($"Differences detected: {differences}");
                return differences;
            }
            else if (args.Length == 2 && args[0] == "extract")
            {
                BaseMessage message = BaseMessage.Parse(File.ReadAllText(args[1]));
                switch(message)
                {
                    case DeathRecordSubmission submission:
                        var record = submission.DeathRecord;
                        Console.WriteLine(record.ToJSON());
                        break;
                }
                return 0;
            }
            else if (args.Length == 2 && args[0] == "submit")
            {
                DeathRecord record = new DeathRecord(File.ReadAllText(args[1]));
                DeathRecordSubmission message = new DeathRecordSubmission(record);
                message.MessageSource = "http://mitre.org/vrdr";
                Console.WriteLine(message.ToJSON(true));
                return 0;
            }
            else if (args.Length == 2 && args[0] == "resubmit")
            {
                DeathRecord record = new DeathRecord(File.ReadAllText(args[1]));
                DeathRecordUpdate message = new DeathRecordUpdate(record);
                message.MessageSource = "http://mitre.org/vrdr";
                Console.WriteLine(message.ToJSON(true));
                return 0;
            }
            else if (args.Length == 2 && args[0] == "ack")
            {
                BaseMessage message = BaseMessage.Parse(File.ReadAllText(args[1]));
                AckMessage ackMessage = new AckMessage(message);
                Console.WriteLine(ackMessage.ToJSON(true));
                return 0;
            }
            else if (args.Length == 2 && args[0] == "showcodes")
            {
                BaseMessage message = BaseMessage.Parse(File.ReadAllText(args[1]));
                switch(message)
                {
                    case CodingResponseMessage codingResponse:
                        Console.WriteLine($"\nUnderlying COD: {codingResponse.UnderlyingCauseOfDeath}\n");
                        Console.WriteLine($"Record Axis Codes: {String.Join(", ", codingResponse.CauseOfDeathRecordAxis.ToArray())}\n");
                        Console.WriteLine("Entity Axis Codes:");
                        foreach (var entry in codingResponse.CauseOfDeathEntityAxisList)
                        {
                            Console.WriteLine($"  Line: {entry.Line}  Sequence: {entry.Position}  Code: {entry.Code}");
                        }
                        Console.WriteLine();
                        break;
                    default:
                        Console.WriteLine("Message does not appear to be a coding response message");
                        break;
                }
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
    }
}
