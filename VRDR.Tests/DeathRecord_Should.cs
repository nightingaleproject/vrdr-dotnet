using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Xunit;

using System.Linq;

namespace VRDR.Tests
{
    public class DeathRecord_Should
    {
        private ArrayList XMLRecords;

        private ArrayList JSONRecords;

        private DeathRecord SetterDeathRecord;

        public DeathRecord_Should()
        {
            XMLRecords = new ArrayList();
            XMLRecords.Add(new DeathRecord(File.ReadAllText(FixturePath("fixtures/xml/DeathRecord1.xml"))));
            XMLRecords.Add(new DeathRecord(File.ReadAllText(FixturePath("fixtures/xml/Bundle-DeathCertificateDocument-Example2.xml"))));
            JSONRecords = new ArrayList();
            JSONRecords.Add(new DeathRecord(File.ReadAllText(FixturePath("fixtures/json/DeathRecord1.json"))));
            JSONRecords.Add(new DeathRecord(File.ReadAllText(FixturePath("fixtures/json/Bundle-DeathCertificateDocument-Example2.json"))));
            JSONRecords.Add(new DeathRecord(File.ReadAllText(FixturePath("fixtures/json/Bundle-DeathCertificateDocument-Example1.json"))));
            JSONRecords.Add(new DeathRecord(File.ReadAllText(FixturePath("fixtures/json/Bundle-CauseOfDeathCodedContentBundle-Example1.json"))));
            JSONRecords.Add(new DeathRecord(File.ReadAllText(FixturePath("fixtures/json/Bundle-DemographicCodedContentBundle-Example1.json"))));
            //Console.WriteLine("TEST1: " + ((DeathRecord)JSONRecords[0]).ToJSON());
            SetterDeathRecord = new DeathRecord();
        }

        [Fact]
        public void FailInvalidInput()
        {
            Exception ex = Assert.Throws<System.ArgumentException>(() => new DeathRecord("foobar"));
            Assert.Equal("The given input does not appear to be a valid XML or JSON FHIR record.", ex.Message);
        }

        [Fact]
        public void FailMissingValue()
        {
            string bundle = File.ReadAllText(FixturePath("fixtures/xml/MissingValue.xml"));
            Exception ex = Assert.Throws<System.ArgumentException>(() => new DeathRecord(bundle));
            Assert.Equal("Parser: The attribute 'value' in element 'status' has an empty value, which is not allowed. (at line 21, 17)", ex.Message);
        }


        [Fact]
        public void FailMissingObservationCode()
        {
            string bundle = File.ReadAllText(FixturePath("fixtures/json/MissingObservationCode.json"));
            Exception ex = Assert.Throws<System.ArgumentException>(() => new DeathRecord(bundle));
            Assert.Equal("Found an Observation resource that did not contain a code. All Observations must include a code to specify what the Observation is referring to.", ex.Message);
        }

        [Fact]
        public void FailMissingRelatedPersonRelationshipCode()
        {
            string bundle = File.ReadAllText(FixturePath("fixtures/json/MissingRelatedPersonRelationshipCode.json"));
            Exception ex = Assert.Throws<System.ArgumentException>(() => new DeathRecord(bundle));
            Assert.Equal("Found a RelatedPerson resource that did not contain a relationship code. All RelatedPersons must include a relationship code to specify how the RelatedPerson is related to the subject.", ex.Message);
        }

        [Fact]
        public void InvalidDeathLocationJurisdiction()
        {
            // In input file's http://hl7.org/fhir/us/vrdr/StructureDefinition/Location-Jurisdiction-Id extension uses an old format from version 3.0.0 RC5
            // if the input is invalid, return null, no error is thrown
            DeathRecord deathRecord = new DeathRecord(File.ReadAllText(FixturePath("fixtures/json/InvalidJurisdictionId.json")));
            Assert.Null(deathRecord.DeathLocationJurisdiction);
        }

        [Fact]
        public void EmptyRecordToDescription()
        {
            DeathRecord deathRecord = new DeathRecord();
            string description = deathRecord.ToDescription();
            Assert.NotNull(description);
        }

        [Fact]
        public void EmptyRecordToIJE()
        {
            DeathRecord deathRecord = new DeathRecord();
            string ije = new IJEMortality(deathRecord, false).ToString(); // Don't validate since empty record
            Assert.NotNull(ije);
        }

        [Fact]
        public void ToFromDescription()
        {
            DeathRecord first = (DeathRecord)XMLRecords[0];
            string firstDescription = first.ToDescription();
            DeathRecord second = DeathRecord.FromDescription(firstDescription);
            Assert.Equal(first.Identifier, second.Identifier);
            Assert.Equal(first.GivenNames, second.GivenNames);
            Assert.Equal(first.AutopsyResultsAvailable, second.AutopsyResultsAvailable);
            Assert.Equal(first.Race, second.Race);
            Assert.Equal(first.COD1A, second.COD1A);
            Assert.Equal(first.INTERVAL1B, second.INTERVAL1B);
            // Assert.Equal(first.CODE1A, second.CODE1A);
            Assert.Equal(first.CertifierAddress, second.CertifierAddress);
            Assert.Equal(first.CausesOfDeath, second.CausesOfDeath);
        }

        [Fact]
        public void ToFromCodedBundleViaDescription()
        {
            DeathRecord record = (DeathRecord)JSONRecords[1];
            DeathRecord codedCODRecord = new DeathRecord(record.GetCauseOfDeathCodedContentBundle());
            DeathRecord codedDemoRecord = new DeathRecord(record.GetDemographicCodedContentBundle());
            string CodedCODDescription = codedCODRecord.ToDescription();
            string CodedDemoDescription = codedCODRecord.ToDescription();
            DeathRecord record2 = new DeathRecord(codedCODRecord.ToJSON());
            DeathRecord record3 = new DeathRecord(codedDemoRecord.ToJSON());
        }

        [Fact]
        public void SetPatientAfterParse()
        {
            DeathRecord sample1 = new DeathRecord(File.ReadAllText(FixturePath("fixtures/xml/DeathRecord1.xml")));
            DeathRecord sample2 = new DeathRecord(File.ReadAllText(FixturePath("fixtures/json/DeathRecord1.json")));
            Assert.Equal("Patel", sample1.FamilyName);
            Assert.Equal("Patel", sample2.FamilyName);
            sample1.FamilyName = "1changed2abc";
            sample2.FamilyName = "2changed1xyz";
            Assert.Equal("1changed2abc", sample1.FamilyName);
            Assert.Equal("2changed1xyz", sample2.FamilyName);
        }

        [Fact]
        public void ParseDeathLocationJurisdictionIJEtoJson()
        {
            DeathRecord dxml = new DeathRecord(File.ReadAllText(FixturePath("fixtures/xml/DeathRecord1.xml")));
            DeathRecord djson = new DeathRecord(File.ReadAllText(FixturePath("fixtures/json/DeathRecord1.json")));
            IJEMortality ijefromjson = new IJEMortality(djson);
            IJEMortality ijefromxml = new IJEMortality(dxml);
            DeathRecord fromijefromjson = ijefromjson.ToDeathRecord();
            DeathRecord fromijefromxml = ijefromjson.ToDeathRecord();

            Assert.Equal("YC", fromijefromjson.DeathLocationJurisdiction);
            Assert.Equal("YC", fromijefromxml.DeathLocationJurisdiction);
        }

        [Fact]
        public void NoDeathLocationJurisdictionJsontoIJE()
        {
            DeathRecord deathRecord = new DeathRecord();
            IJEMortality ije = new IJEMortality(deathRecord, false); // Don't raise validation errors on empty record
            Assert.Equal("  ", ije.DSTATE);
        }

        [Fact]
        public void ParseDeathLocationTypeIJEtoJson()
        {
            DeathRecord djson = new DeathRecord(File.ReadAllText(FixturePath("fixtures/json/DeathLocationType.json")));
            IJEMortality ijefromjson = new IJEMortality(djson);
            DeathRecord fromijefromjson = ijefromjson.ToDeathRecord();

            Assert.NotEqual(fromijefromjson.DeathLocationTypeHelper, VRDR.ValueSets.PlaceOfDeath.Death_In_Hospice);
            Assert.Equal(fromijefromjson.DeathLocationTypeHelper, VRDR.ValueSets.PlaceOfDeath.Death_In_Home);
            Assert.Equal("Death in home", fromijefromjson.DeathLocationType["display"]);
        }

        [Fact]
        public void ParseRaceEthnicityJsonToIJE()
        {
            // Hispanic or Latino
            DeathRecord d = new DeathRecord(File.ReadAllText(FixturePath("fixtures/json/RaceEthnicityCaseRecord.json")));
            IJEMortality ije1 = new IJEMortality(d);
            Assert.Equal("H", ije1.DETHNIC1);
            Assert.Equal("H", ije1.DETHNIC2);
            Assert.Equal("H", ije1.DETHNIC3);
            Assert.Equal("H", ije1.DETHNIC4);
            Assert.Equal("Y", ije1.RACE1);
            Assert.Equal("N", ije1.RACE2);
            Assert.Equal("N", ije1.RACE3);
            Assert.Equal("N", ije1.RACE4);
            Assert.Equal("N", ije1.RACE5);
            Assert.Equal("N", ije1.RACE6);
            Assert.Equal("N", ije1.RACE7);
            Assert.Equal("N", ije1.RACE8);
            Assert.Equal("N", ije1.RACE9);
            Assert.Equal("N", ije1.RACE10);
            Assert.Equal("N", ije1.RACE11);
            Assert.Equal("N", ije1.RACE12);
            Assert.Equal("N", ije1.RACE13);
            Assert.Equal("N", ije1.RACE14);
            Assert.Equal("N", ije1.RACE15);

            // Non Hispanic or Latino
            DeathRecord d2 = new DeathRecord(File.ReadAllText(FixturePath("fixtures/json/RaceEthnicityCaseRecord2.json")));
            IJEMortality ije2 = new IJEMortality(d2);
            Assert.Equal("N", ije2.DETHNIC1);
            Assert.Equal("N", ije2.DETHNIC2);
            Assert.Equal("N", ije2.DETHNIC3);
            Assert.Equal("N", ije2.DETHNIC4);
            Assert.Equal("Y", ije2.RACE10);
            Assert.Equal("Hmong", ije2.RACE18);

            // From VRDR IG
            DeathRecord d3 = (((DeathRecord)JSONRecords[0]));
            IJEMortality ije3 = new IJEMortality(d3);
            Assert.Equal("H", ije3.DETHNIC1);
            Assert.Equal("U", ije3.DETHNIC2);
            Assert.Equal("U", ije3.DETHNIC3);
            Assert.Equal("U", ije3.DETHNIC4);
            Assert.Equal("", ije3.RACE18);
            Assert.Equal("Y", ije3.RACE1);
            Assert.Equal("N", ije3.RACE2);
            Assert.Equal("N", ije3.RACE3);
            Assert.Equal("N", ije3.RACE4);
            Assert.Equal("N", ije3.RACE5);
            Assert.Equal("N", ije3.RACE6);
            Assert.Equal("N", ije3.RACE7);
            Assert.Equal("N", ije3.RACE8);
            Assert.Equal("N", ije3.RACE9);
            Assert.Equal("N", ije3.RACE10);
            Assert.Equal("N", ije3.RACE11);
            Assert.Equal("N", ije3.RACE12);
            Assert.Equal("N", ije3.RACE13);
            Assert.Equal("N", ije3.RACE14);
            Assert.Equal("N", ije3.RACE15);
        }

        [Fact]
        public void ParseRaceEthnicityIJEtoJson()
        {
            DeathRecord d = new DeathRecord(File.ReadAllText(FixturePath("fixtures/json/RaceEthnicityCaseRecord.json")));
            IJEMortality ije1 = new IJEMortality(d);
            IJEMortality ije2 = new IJEMortality(ije1.ToString(), true);
            DeathRecord d2 = ije2.ToDeathRecord();

            // Ethnicity tuple
            Assert.Equal("Y", d2.Ethnicity2Helper);

            // Race tuple
            foreach (var pair in d2.Race)
            {
                switch (pair.Item1)
                {
                    case NvssRace.White:
                        Assert.Equal("Y", pair.Item2);
                        break;
                    case NvssRace.AmericanIndianOrAlaskaNative:
                        Assert.Equal("N", pair.Item2);
                        break;
                    default:
                        break;
                }
            }
            Assert.Equal(15, d2.Race.Length);
        }

        [Fact]
        public void HandleNoEthnicityDataInJSON()
        {
            // if no ethnicity data is provided in FHIR, IJE should have unknowns and a blank literal
            DeathRecord d = new DeathRecord(File.ReadAllText(FixturePath("fixtures/json/MissingEthnicityData.json")));
            IJEMortality ije1 = new IJEMortality(d);
            Assert.Equal("U", ije1.DETHNIC1);
            Assert.Equal("U", ije1.DETHNIC2);
            Assert.Equal("U", ije1.DETHNIC3);
            Assert.Equal("U", ije1.DETHNIC4);
            Assert.Equal("", ije1.DETHNIC5);

        }

        [Fact]
        public void SetPractitionerAfterParse()
        {
            DeathRecord sample1 = new DeathRecord(File.ReadAllText(FixturePath("fixtures/xml/DeathRecord1.xml")));
            DeathRecord sample2 = new DeathRecord(File.ReadAllText(FixturePath("fixtures/json/DeathRecord1.json")));
            Assert.Equal("Last", sample1.CertifierFamilyName);
            Assert.Equal("Last", sample2.CertifierFamilyName);
            sample1.CertifierFamilyName = "1diff2abc";
            sample2.CertifierFamilyName = "2diff1xyz";
            Assert.Equal("1diff2abc", sample1.CertifierFamilyName);
            Assert.Equal("2diff1xyz", sample2.CertifierFamilyName);
        }

        [Fact]
        public void Set_DeathLocationTypeHelper()
        {
            SetterDeathRecord.DeathLocationTypeHelper = VRDR.ValueSets.PlaceOfDeath.Death_In_Nursing_Home_Or_Long_Term_Care_Facility;
            Assert.Equal(VRDR.ValueSets.PlaceOfDeath.Death_In_Nursing_Home_Or_Long_Term_Care_Facility, SetterDeathRecord.DeathLocationTypeHelper);
            Assert.Equal("Death in nursing home or long term care facility", SetterDeathRecord.DeathLocationType["display"]);
            Exception ex = Assert.Throws<System.ArgumentException>(() => SetterDeathRecord.DeathLocationTypeHelper = "NotAValidValue");
        }

        [Fact]
        public void Get_DeathLocationType()
        {
            Assert.Equal(ValueSets.PlaceOfDeath.Death_In_Home, ((DeathRecord)JSONRecords[0]).DeathLocationTypeHelper);
            Assert.Equal(ValueSets.PlaceOfDeath.Death_In_Hospital, ((DeathRecord)JSONRecords[2]).DeathLocationTypeHelper);
            Assert.Equal(ValueSets.PlaceOfDeath.Death_In_Home, ((DeathRecord)XMLRecords[0]).DeathLocationTypeHelper);
        }
        [Fact]
        public void Set_Identifier()
        {
            SetterDeathRecord.Identifier = "1337";
            Assert.Equal("1337", SetterDeathRecord.Identifier);
        }

        [Fact]
        public void Get_Identifier()
        {
            Assert.Equal("000182", ((DeathRecord)JSONRecords[0]).Identifier);
            Assert.Equal("000182", ((DeathRecord)JSONRecords[1]).Identifier);
            Assert.Equal("000182", ((DeathRecord)XMLRecords[0]).Identifier);
        }

        [Fact]
        public void Set_DeathRecordIdentifier()
        {
            DeathRecord empty = new DeathRecord();
            Assert.Equal("0000XX000000", empty.DeathRecordIdentifier);
            empty.DateOfDeath = "2019-10-01";
            Assert.Equal("2019XX000000", empty.DeathRecordIdentifier);
            empty.Identifier = "101";
            Assert.Equal("2019XX000101", empty.DeathRecordIdentifier);
            empty.DeathLocationJurisdiction = "MA";  // 25 is the code for MA
            Assert.Equal("2019MA000101", empty.DeathRecordIdentifier);
        }

        [Fact]
        public void Get_DeathRecordIdentifier()
        {
            Assert.Equal("2019YC000182", ((DeathRecord)JSONRecords[0]).DeathRecordIdentifier);
            Assert.Equal("2020NY000182", ((DeathRecord)JSONRecords[1]).DeathRecordIdentifier);
            Assert.Equal("2019YC000182", ((DeathRecord)XMLRecords[0]).DeathRecordIdentifier);
        }

        [Fact]
        public void Set_StateLocalIdentifier()
        {
            SetterDeathRecord.StateLocalIdentifier1 = "000000000042";
            SetterDeathRecord.StateLocalIdentifier2 = "100000000042";
            Assert.Equal("000000000042", SetterDeathRecord.StateLocalIdentifier1);
            Assert.Equal("100000000042", SetterDeathRecord.StateLocalIdentifier2);
        }

        [Fact]
        public void Get_StateLocalIdentifier()
        {
            Assert.Equal("000000000042", ((DeathRecord)JSONRecords[0]).StateLocalIdentifier1);
            Assert.Equal("000000000042", ((DeathRecord)XMLRecords[0]).StateLocalIdentifier1);
            Assert.Equal("000000000001", ((DeathRecord)JSONRecords[1]).StateLocalIdentifier1);
            Assert.Equal("100000000001", ((DeathRecord)JSONRecords[1]).StateLocalIdentifier2);
        }

        [Fact]
        public void Set_CertifiedTime()
        {
            SetterDeathRecord.CertifiedTime = "2019-01-28T16:48:06-05:00";
            Assert.Equal("2019-01-28T16:48:06-05:00", SetterDeathRecord.CertifiedTime);
        }

        [Fact]
        public void Get_CertifiedTime()
        {
            Assert.Equal("2019-01-29T16:48:06-05:00", ((DeathRecord)JSONRecords[0]).CertifiedTime);
            Assert.Equal("2020-11-14T16:39:40-05:00", ((DeathRecord)JSONRecords[1]).CertifiedTime);
            Assert.Equal("2019-01-29T16:48:06-05:00", ((DeathRecord)XMLRecords[0]).CertifiedTime);
        }

        [Fact]
        public void Set_RegisteredTime()
        {
            SetterDeathRecord.RegisteredTime = "2019-01-29T16:48:06-05:00";
            Assert.Equal("2019-01-29T16:48:06-05:00", SetterDeathRecord.RegisteredTime);
        }

        [Fact]
        public void Get_RegisteredTime()
        {
            Assert.Equal("2019-02-01T16:47:04-05:00", ((DeathRecord)JSONRecords[0]).RegisteredTime);
            Assert.Equal("2019-02-01T16:47:04-05:00", ((DeathRecord)XMLRecords[0]).RegisteredTime);
        }

        [Fact]
        public void Get_RegisteredTime_ConvertIJE()
        {
            DeathRecord dr = new DeathRecord(File.ReadAllText(FixturePath("fixtures/json/DeathRecord1.json")));
            IJEMortality ije1 = new IJEMortality(dr);
            Assert.Equal("2019", ije1.DOR_YR);
            Assert.Equal("02", ije1.DOR_MO);
            Assert.Equal("01", ije1.DOR_DY);
        }

        [Fact]
        public void Set_CertificationRole()
        {
            Dictionary<string, string> CertificationRole = new Dictionary<string, string>();
            CertificationRole.Add("code", "434641000124105");
            CertificationRole.Add("system", CodeSystems.SCT);
            CertificationRole.Add("display", "Physician certified and pronounced death certificate");
            SetterDeathRecord.CertificationRole = CertificationRole;
            Assert.Equal("434641000124105", SetterDeathRecord.CertificationRole["code"]);
            Assert.Equal(CodeSystems.SCT, SetterDeathRecord.CertificationRole["system"]);
            Assert.Equal("Physician certified and pronounced death certificate", SetterDeathRecord.CertificationRole["display"]);
            SetterDeathRecord.CertificationRoleHelper = VRDR.ValueSets.CertifierTypes.Pronouncing_Certifying_Physician;
            Assert.Equal("434641000124105", SetterDeathRecord.CertificationRole["code"]);
            Assert.Equal(CodeSystems.SCT, SetterDeathRecord.CertificationRole["system"]);
            Assert.Equal("Pronouncing & Certifying physician-To the best of my knowledge, death occurred at the time, date, and place, and due to the cause(s) and manner stated.", SetterDeathRecord.CertificationRole["display"]);
            SetterDeathRecord.CertificationRoleHelper = "Barber";
            Assert.Equal("OTH", SetterDeathRecord.CertificationRole["code"]);
            Assert.Equal(CodeSystems.NullFlavor_HL7_V3, SetterDeathRecord.CertificationRole["system"]);
            Assert.Equal("Other", SetterDeathRecord.CertificationRole["display"]);
            Assert.Equal("Barber", SetterDeathRecord.CertificationRole["text"]);

        }

        [Fact]
        public void Get_CertificationRole()
        {
            Assert.Equal("434641000124105", ((DeathRecord)JSONRecords[0]).CertificationRole["code"]);
            Assert.Equal(CodeSystems.SCT, ((DeathRecord)XMLRecords[0]).CertificationRole["system"]);
            Assert.Equal("Physician certified and pronounced death certificate", ((DeathRecord)JSONRecords[0]).CertificationRole["display"]);
            Assert.Equal("434641000124105", ((DeathRecord)XMLRecords[0]).CertificationRole["code"]);
            Assert.Equal(CodeSystems.SCT, ((DeathRecord)JSONRecords[0]).CertificationRole["system"]);
            Assert.Equal("Physician certified and pronounced death certificate", ((DeathRecord)XMLRecords[0]).CertificationRole["display"]);
        }

        // [Fact]
        // public void Set_InterestedPartyIdentifier()
        // {
        //     var id = new Dictionary<string, string>();
        //     id["system"] = "foo";
        //     id["value"] = "0000000000";
        //     SetterDeathRecord.InterestedPartyIdentifier = id;
        //     Assert.Equal("foo", SetterDeathRecord.InterestedPartyIdentifier["system"]);
        //     Assert.Equal("0000000000", SetterDeathRecord.InterestedPartyIdentifier["value"]);
        // }

        // [Fact]
        // public void Get_InterestedPartyIdentifier()
        // {
        //     Assert.Equal("1010101", ((DeathRecord)JSONRecords[0]).InterestedPartyIdentifier["value"]);
        //     Assert.Equal("1010101", ((DeathRecord)XMLRecords[0]).InterestedPartyIdentifier["value"]);
        // }

        // [Fact]
        // public void Set_InterestedPartyName()
        // {
        //     SetterDeathRecord.InterestedPartyName = "123abc123xyz";
        //     Assert.Equal("123abc123xyz", SetterDeathRecord.InterestedPartyName);
        // }

        // [Fact]
        // public void Get_InterestedPartyName()
        // {
        //     Assert.Equal("Example Hospital", ((DeathRecord)JSONRecords[0]).InterestedPartyName);
        //     Assert.Equal("Example Hospital", ((DeathRecord)XMLRecords[0]).InterestedPartyName);
        // }

        // [Fact]
        // public void Set_InterestedPartyAddress()
        // {
        //     Dictionary<string, string> address = new Dictionary<string, string>();
        //     address.Add("addressLine1", "12 Example Street");
        //     address.Add("addressLine2", "Line 2");
        //     address.Add("addressCity", "Bedford");
        //     address.Add("addressCounty", "Middlesex");
        //     address.Add("addressState", "MA");
        //     address.Add("addressZip", "01730");
        //     address.Add("addressCountry", "US");
        //     SetterDeathRecord.InterestedPartyAddress = address;
        //     Assert.Equal("12 Example Street", SetterDeathRecord.InterestedPartyAddress["addressLine1"]);
        //     Assert.Equal("Line 2", SetterDeathRecord.InterestedPartyAddress["addressLine2"]);
        //     Assert.Equal("Bedford", SetterDeathRecord.InterestedPartyAddress["addressCity"]);
        //     Assert.Equal("Middlesex", SetterDeathRecord.InterestedPartyAddress["addressCounty"]);
        //     Assert.Equal("MA", SetterDeathRecord.InterestedPartyAddress["addressState"]);
        //     Assert.Equal("01730", SetterDeathRecord.InterestedPartyAddress["addressZip"]);
        //     Assert.Equal("US", SetterDeathRecord.InterestedPartyAddress["addressCountry"]);
        // }

        // [Fact]
        // public void Get_InterestedPartyAddress()
        // {
        //     Assert.Equal("10 Example Street", ((DeathRecord)JSONRecords[0]).InterestedPartyAddress["addressLine1"]);
        //     Assert.Equal("Line 2", ((DeathRecord)JSONRecords[0]).InterestedPartyAddress["addressLine2"]);
        //     Assert.Equal("Bedford", ((DeathRecord)JSONRecords[0]).InterestedPartyAddress["addressCity"]);
        //     Assert.Equal("Middlesex", ((DeathRecord)JSONRecords[0]).InterestedPartyAddress["addressCounty"]);
        //     Assert.Equal("MA", ((DeathRecord)JSONRecords[0]).InterestedPartyAddress["addressState"]);
        //     Assert.Equal("01730", ((DeathRecord)JSONRecords[0]).InterestedPartyAddress["addressZip"]);
        //     Assert.Equal("US", ((DeathRecord)JSONRecords[0]).InterestedPartyAddress["addressCountry"]);
        //     Assert.Equal("10 Example Street", ((DeathRecord)XMLRecords[0]).InterestedPartyAddress["addressLine1"]);
        //     Assert.Equal("Line 2", ((DeathRecord)XMLRecords[0]).InterestedPartyAddress["addressLine2"]);
        //     Assert.Equal("Bedford", ((DeathRecord)XMLRecords[0]).InterestedPartyAddress["addressCity"]);
        //     Assert.Equal("Middlesex", ((DeathRecord)XMLRecords[0]).InterestedPartyAddress["addressCounty"]);
        //     Assert.Equal("MA", ((DeathRecord)XMLRecords[0]).InterestedPartyAddress["addressState"]);
        //     Assert.Equal("01730", ((DeathRecord)XMLRecords[0]).InterestedPartyAddress["addressZip"]);
        //     Assert.Equal("US", ((DeathRecord)XMLRecords[0]).InterestedPartyAddress["addressCountry"]);
        // }

        // [Fact]
        // public void Set_InterestedPartyType()
        // {
        //     Dictionary<string, string> type = new Dictionary<string, string>();
        //     type.Add("code", "prov");
        //     type.Add("system", CodeSystems.HL7_organization_type);
        //     type.Add("display", "Healthcare Provider");
        //     SetterDeathRecord.InterestedPartyType = type;
        //     Assert.Equal("prov", SetterDeathRecord.InterestedPartyType["code"]);
        //     Assert.Equal(CodeSystems.HL7_organization_type, SetterDeathRecord.InterestedPartyType["system"]);
        //     Assert.Equal("Healthcare Provider", SetterDeathRecord.InterestedPartyType["display"]);
        // }

        // [Fact]
        // public void Get_InterestedPartyType()
        // {
        //     Assert.Equal("prov", ((DeathRecord)JSONRecords[0]).InterestedPartyType["code"]);
        //     Assert.Equal(CodeSystems.HL7_organization_type, ((DeathRecord)XMLRecords[0]).InterestedPartyType["system"]);
        //     Assert.Equal("Healthcare Provider", ((DeathRecord)JSONRecords[0]).InterestedPartyType["display"]);
        //     Assert.Equal("prov", ((DeathRecord)XMLRecords[0]).InterestedPartyType["code"]);
        //     Assert.Equal(CodeSystems.HL7_organization_type, ((DeathRecord)JSONRecords[0]).InterestedPartyType["system"]);
        //     Assert.Equal("Healthcare Provider", ((DeathRecord)XMLRecords[0]).InterestedPartyType["display"]);
        // }

        [Fact]
        public void Set_MannerOfDeathType()
        {
            Dictionary<string, string> type = new Dictionary<string, string>();
            SetterDeathRecord.MannerOfDeathTypeHelper = ValueSets.MannerOfDeath.Accidental_Death;
            Assert.Equal(CodeSystems.SCT, SetterDeathRecord.MannerOfDeathType["system"]);
            Assert.Equal(ValueSets.MannerOfDeath.Accidental_Death, SetterDeathRecord.MannerOfDeathTypeHelper);
            Assert.Equal("Accidental death", SetterDeathRecord.MannerOfDeathType["display"]);
        }

        [Fact]
        public void Get_MannerOfDeathType()
        {
            Assert.Equal(CodeSystems.SCT, ((DeathRecord)JSONRecords[0]).MannerOfDeathType["system"]);
            Assert.Equal(ValueSets.MannerOfDeath.Accidental_Death, ((DeathRecord)JSONRecords[0]).MannerOfDeathType["code"]);
            Assert.Equal("Accidental death", ((DeathRecord)JSONRecords[0]).MannerOfDeathType["display"]);
            Assert.Equal(CodeSystems.SCT, ((DeathRecord)XMLRecords[0]).MannerOfDeathType["system"]);
            Assert.Equal(ValueSets.MannerOfDeath.Accidental_Death, ((DeathRecord)XMLRecords[0]).MannerOfDeathType["code"]);
            Assert.Equal("Accidental death", ((DeathRecord)XMLRecords[0]).MannerOfDeathType["display"]);
        }

        [Fact]
        public void Set_CertifierGivenNames()
        {
            string[] cnames = { "Doctor", "Middle" };
            SetterDeathRecord.CertifierGivenNames = cnames;
            Assert.Equal("Doctor", SetterDeathRecord.CertifierGivenNames[0]);
            Assert.Equal("Middle", SetterDeathRecord.CertifierGivenNames[1]);
        }

        [Fact]
        public void Get_CertifierGivenNames()
        {
            string[] cnamesjson = ((DeathRecord)JSONRecords[0]).CertifierGivenNames;
            Assert.Equal("Doctor", cnamesjson[0]);
            Assert.Equal("Middle", cnamesjson[1]);
            string[] cnamesxml = ((DeathRecord)XMLRecords[0]).CertifierGivenNames;
            Assert.Equal("Doctor", cnamesxml[0]);
            Assert.Equal("Middle", cnamesxml[1]);
        }

        [Fact]
        public void Set_CertifierFamilyName()
        {
            SetterDeathRecord.CertifierFamilyName = "Last";
            Assert.Equal("Last", SetterDeathRecord.CertifierFamilyName);
        }

        [Fact]
        public void Get_CertifierFamilyName()
        {
            Assert.Equal("Last", ((DeathRecord)XMLRecords[0]).CertifierFamilyName);
            Assert.Equal("Last", ((DeathRecord)XMLRecords[0]).CertifierFamilyName);
        }

        [Fact]
        public void Set_CertifierSuffix()
        {
            SetterDeathRecord.CertifierSuffix = "Jr.";
            Assert.Equal("Jr.", SetterDeathRecord.CertifierSuffix);
        }

        [Fact]
        public void Get_CertifierSuffix()
        {
            Assert.Equal("Jr.", ((DeathRecord)XMLRecords[0]).CertifierSuffix);
            Assert.Equal("Jr.", ((DeathRecord)XMLRecords[0]).CertifierSuffix);
        }

        [Fact]
        public void Set_CertifierAddress()
        {
            Dictionary<string, string> caddress = new Dictionary<string, string>();
            caddress.Add("addressLine1", "11 Example Street");
            caddress.Add("addressLine2", "Line 2");
            caddress.Add("addressCity", "Bedford");
            caddress.Add("addressCounty", "Middlesex");
            caddress.Add("addressState", "MA");
            caddress.Add("addressZip", "01730");
            caddress.Add("addressCountry", "US");
            caddress.Add("addressPredir", "W");
            caddress.Add("addressPostdir", "E");
            caddress.Add("addressStname", "Example");
            caddress.Add("addressStnum", "11");
            caddress.Add("addressStdesig", "Street");
            caddress.Add("addressUnitnum", "3");
            SetterDeathRecord.CertifierAddress = caddress;
            Assert.Equal("11 Example Street", SetterDeathRecord.CertifierAddress["addressLine1"]);
            Assert.Equal("Line 2", SetterDeathRecord.CertifierAddress["addressLine2"]);
            Assert.Equal("Bedford", SetterDeathRecord.CertifierAddress["addressCity"]);
            Assert.Equal("Middlesex", SetterDeathRecord.CertifierAddress["addressCounty"]);
            Assert.Equal("MA", SetterDeathRecord.CertifierAddress["addressState"]);
            Assert.Equal("01730", SetterDeathRecord.CertifierAddress["addressZip"]);
            Assert.Equal("US", SetterDeathRecord.CertifierAddress["addressCountry"]);
            Assert.Equal("W", SetterDeathRecord.CertifierAddress["addressPredir"]);
            Assert.Equal("E", SetterDeathRecord.CertifierAddress["addressPostdir"]);
            Assert.Equal("Example", SetterDeathRecord.CertifierAddress["addressStname"]);
            Assert.Equal("11", SetterDeathRecord.CertifierAddress["addressStnum"]);
            Assert.Equal("Street", SetterDeathRecord.CertifierAddress["addressStdesig"]);
            Assert.Equal("3", SetterDeathRecord.CertifierAddress["addressUnitnum"]);
        }

        [Fact]
        public void Get_CertifierAddress()
        {
            Assert.Equal("11 Example Street", ((DeathRecord)JSONRecords[0]).CertifierAddress["addressLine1"]);
            Assert.Equal("Line 2", ((DeathRecord)JSONRecords[0]).CertifierAddress["addressLine2"]);
            Assert.Equal("Bedford", ((DeathRecord)JSONRecords[0]).CertifierAddress["addressCity"]);
            Assert.Equal("Middlesex", ((DeathRecord)JSONRecords[0]).CertifierAddress["addressCounty"]);
            Assert.Equal("MA", ((DeathRecord)JSONRecords[0]).CertifierAddress["addressState"]);
            Assert.Equal("01730", ((DeathRecord)JSONRecords[0]).CertifierAddress["addressZip"]);
            Assert.Equal("US", ((DeathRecord)JSONRecords[0]).CertifierAddress["addressCountry"]);
            Assert.Equal("11 Example Street", ((DeathRecord)XMLRecords[0]).CertifierAddress["addressLine1"]);
            Assert.Equal("Line 2", ((DeathRecord)XMLRecords[0]).CertifierAddress["addressLine2"]);
            Assert.Equal("Bedford", ((DeathRecord)XMLRecords[0]).CertifierAddress["addressCity"]);
            Assert.Equal("Middlesex", ((DeathRecord)XMLRecords[0]).CertifierAddress["addressCounty"]);
            Assert.Equal("MA", ((DeathRecord)XMLRecords[0]).CertifierAddress["addressState"]);
            Assert.Equal("01730", ((DeathRecord)XMLRecords[0]).CertifierAddress["addressZip"]);
            Assert.Equal("US", ((DeathRecord)XMLRecords[0]).CertifierAddress["addressCountry"]);
        }

        // [Fact]
        // public void Set_CertifierQualification()
        // {
        //     Dictionary<string, string> qualification = new Dictionary<string, string>();
        //     qualification.Add("code", "3060");
        //     qualification.Add("system", "urn:oid:2.16.840.1.114222.4.11.7186");
        //     qualification.Add("display", "Physicians and surgeons");
        //     SetterDeathRecord.CertifierQualification = qualification;
        //     Assert.Equal("3060", SetterDeathRecord.CertifierQualification["code"]);
        //     Assert.Equal("urn:oid:2.16.840.1.114222.4.11.7186", SetterDeathRecord.CertifierQualification["system"]);
        //     Assert.Equal("Physicians and surgeons", SetterDeathRecord.CertifierQualification["display"]);
        // }

        // [Fact]
        // public void Get_CertifierQualification()
        // {
        //     Assert.Equal("434641000124105", ((DeathRecord)JSONRecords[0]).CertifierQualification["code"]);
        //     Assert.Equal(CodeSystems.SCT, ((DeathRecord)XMLRecords[0]).CertifierQualification["system"]);
        //     Assert.Equal("Physician certified and pronounced death certificate", ((DeathRecord)JSONRecords[0]).CertifierQualification["display"]);
        //     Assert.Equal("434641000124105", ((DeathRecord)XMLRecords[0]).CertifierQualification["code"]);
        //     Assert.Equal(CodeSystems.SCT, ((DeathRecord)JSONRecords[0]).CertifierQualification["system"]);
        //     Assert.Equal("Physician certified and pronounced death certificate", ((DeathRecord)XMLRecords[0]).CertifierQualification["display"]);
        // }

        // [Fact]
        // public void Set_CertifierLicenseNumber()
        // {
        //     SetterDeathRecord.CertifierLicenseNumber = "789123456";
        //     Assert.Equal("789123456", SetterDeathRecord.CertifierLicenseNumber);
        // }

        // [Fact]
        // public void Get_CertifierLicenseNumber()
        // {
        //     Assert.Equal("789123456", ((DeathRecord)JSONRecords[0]).CertifierLicenseNumber);
        //     Assert.Equal("789123456", ((DeathRecord)XMLRecords[0]).CertifierLicenseNumber);
        // }

        [Fact]
        public void Set_ContributingConditions()
        {
            SetterDeathRecord.ContributingConditions = "Example Contributing Condition";
            Assert.Equal("Example Contributing Condition", SetterDeathRecord.ContributingConditions);
        }

        [Fact]
        public void Get_ContributingConditions()
        {
            Assert.Equal("hypertensive heart disease", ((DeathRecord)JSONRecords[1]).ContributingConditions);
            Assert.Equal("Example Contributing Conditions", ((DeathRecord)XMLRecords[0]).ContributingConditions);
        }

        [Fact]
        public void Set_COD1A()
        {
            SetterDeathRecord.COD1A = "Rupture of myocardium";
            Assert.Equal("Rupture of myocardium", SetterDeathRecord.COD1A);
        }

        [Fact]
        public void Get_COD1A()
        {
            Assert.Equal("Cardiopulmonary arrest", ((DeathRecord)JSONRecords[1]).COD1A);
            Assert.Equal("Cardiopulmonary arrest", ((DeathRecord)XMLRecords[1]).COD1A);
        }

        [Fact]
        public void Set_INTERVAL1A()
        {
            SetterDeathRecord.INTERVAL1A = "minutes";
            Assert.Equal("minutes", SetterDeathRecord.INTERVAL1A);
        }

        [Fact]
        public void Get_INTERVAL1A()
        {
            Assert.Equal("4 hours", ((DeathRecord)JSONRecords[1]).INTERVAL1A);
            Assert.Equal("4 hours", ((DeathRecord)XMLRecords[1]).INTERVAL1A);
        }

        // [Fact]
        // public void Set_CODE1A()
        // {
        //     Dictionary<string, string> code = new Dictionary<string, string>();
        //     code.Add("code", "I21.0");
        //     code.Add("system", "http://hl7.org/fhir/sid/icd-10");
        //     code.Add("display", "Acute transmural myocardial infarction of anterior wall");
        //     SetterDeathRecord.CODE1A = code;
        //     Assert.Equal("I21.0", SetterDeathRecord.CODE1A["code"]);
        //     Assert.Equal("http://hl7.org/fhir/sid/icd-10", SetterDeathRecord.CODE1A["system"]);
        //     Assert.Equal("Acute transmural myocardial infarction of anterior wall", SetterDeathRecord.CODE1A["display"]);
        // }

        // [Fact]
        // public void Get_CODE1A()
        // {
        //     Assert.Equal("I21.0", ((DeathRecord)JSONRecords[0]).CODE1A["code"]);
        //     Assert.Equal("http://hl7.org/fhir/sid/icd-10", ((DeathRecord)XMLRecords[0]).CODE1A["system"]);
        //     Assert.Equal("Acute transmural myocardial infarction of anterior wall", ((DeathRecord)JSONRecords[0]).CODE1A["display"]);
        //     Assert.Equal("I21.0", ((DeathRecord)XMLRecords[0]).CODE1A["code"]);
        //     Assert.Equal("http://hl7.org/fhir/sid/icd-10", ((DeathRecord)JSONRecords[0]).CODE1A["system"]);
        //     Assert.Equal("Acute transmural myocardial infarction of anterior wall", ((DeathRecord)XMLRecords[0]).CODE1A["display"]);
        // }

        [Fact]
        public void Set_COD1B()
        {
            SetterDeathRecord.COD1B = "cause 2";
            Assert.Equal("cause 2", SetterDeathRecord.COD1B);
        }

        [Fact]
        public void Get_COD1B()
        {
            Assert.Equal("Eclampsia", ((DeathRecord)JSONRecords[1]).COD1B);
            Assert.Equal("Eclampsia", ((DeathRecord)XMLRecords[1]).COD1B);
        }

        [Fact]
        public void Set_INTERVAL1B()
        {
            SetterDeathRecord.INTERVAL1B = "days";
            Assert.Equal("days", SetterDeathRecord.INTERVAL1B);
        }

        [Fact]
        public void Get_INTERVAL1B()
        {
            Assert.Equal("3 months", ((DeathRecord)JSONRecords[1]).INTERVAL1B);
            Assert.Equal("3 months", ((DeathRecord)XMLRecords[1]).INTERVAL1B);
        }

        // [Fact]
        // public void Set_CODE1B()
        // {
        //     Dictionary<string, string> code = new Dictionary<string, string>();
        //     code.Add("code", "code 2");
        //     code.Add("system", "system 2");
        //     code.Add("display", "display 2");
        //     SetterDeathRecord.CODE1B = code;
        //     Assert.Equal("code 2", SetterDeathRecord.CODE1B["code"]);
        //     Assert.Equal("system 2", SetterDeathRecord.CODE1B["system"]);
        //     Assert.Equal("display 2", SetterDeathRecord.CODE1B["display"]);
        // }

        // [Fact]
        // public void Get_CODE1B()
        // {
        //     Assert.Equal("I21.9", ((DeathRecord)JSONRecords[0]).CODE1B["code"]);
        //     Assert.Equal("http://hl7.org/fhir/sid/icd-10", ((DeathRecord)XMLRecords[0]).CODE1B["system"]);
        //     Assert.Equal("Acute myocardial infarction, unspecified", ((DeathRecord)JSONRecords[0]).CODE1B["display"]);
        //     Assert.Equal("I21.9", ((DeathRecord)XMLRecords[0]).CODE1B["code"]);
        //     Assert.Equal("http://hl7.org/fhir/sid/icd-10", ((DeathRecord)JSONRecords[0]).CODE1B["system"]);
        //     Assert.Equal("Acute myocardial infarction, unspecified", ((DeathRecord)XMLRecords[0]).CODE1B["display"]);
        // }

        [Fact]
        public void Set_COD1C()
        {
            SetterDeathRecord.COD1C = "cause 3";
            Assert.Equal("cause 3", SetterDeathRecord.COD1C);
        }

        [Fact]
        public void Get_COD1C()
        {
            Assert.Equal("Coronary artery thrombosis", ((DeathRecord)JSONRecords[1]).COD1C);
            Assert.Equal("Coronary artery thrombosis", ((DeathRecord)XMLRecords[1]).COD1C);
        }

        [Fact]
        public void Set_INTERVAL1C()
        {
            SetterDeathRecord.INTERVAL1C = "interval 3";
            Assert.Equal("interval 3", SetterDeathRecord.INTERVAL1C);
        }

        [Fact]
        public void Get_INTERVAL1C()
        {
            Assert.Equal("3 months", ((DeathRecord)JSONRecords[1]).INTERVAL1C);
            Assert.Equal("3 months", ((DeathRecord)XMLRecords[1]).INTERVAL1C);
        }

        // [Fact]
        // public void Set_CODE1C()
        // {
        //     Dictionary<string, string> code = new Dictionary<string, string>();
        //     code.Add("code", "code 3");
        //     code.Add("system", "system 3");
        //     code.Add("display", "display 3");
        //     SetterDeathRecord.CODE1C = code;
        //     Assert.Equal("code 3", SetterDeathRecord.CODE1C["code"]);
        //     Assert.Equal("system 3", SetterDeathRecord.CODE1C["system"]);
        //     Assert.Equal("display 3", SetterDeathRecord.CODE1C["display"]);
        // }

        [Fact]
        public void Set_COD1D()
        {
            SetterDeathRecord.COD1D = "cause 4";
            Assert.Equal("cause 4", SetterDeathRecord.COD1D);
        }

        [Fact]
        public void Get_COD1D()
        {
            Assert.Equal("Atherosclerotic coronary artery disease", ((DeathRecord)JSONRecords[1]).COD1D);
            Assert.Equal("Atherosclerotic coronary artery disease", ((DeathRecord)XMLRecords[1]).COD1D);
        }

        [Fact]
        public void Set_INTERVAL1D()
        {
            SetterDeathRecord.INTERVAL1D = "interval 4";
            Assert.Equal("interval 4", SetterDeathRecord.INTERVAL1D);
        }

        [Fact]
        public void Get_INTERVAL1D()
        {
            Assert.Equal("3 months", ((DeathRecord)JSONRecords[1]).INTERVAL1D);
            Assert.Equal("3 months", ((DeathRecord)XMLRecords[1]).INTERVAL1D);
        }

        // [Fact]
        // public void Set_CODE1D()
        // {
        //     Dictionary<string, string> code = new Dictionary<string, string>();
        //     code.Add("code", "code 4");
        //     code.Add("system", "system 4");
        //     code.Add("display", "display 4");
        //     SetterDeathRecord.CODE1D = code;
        //     Assert.Equal("code 4", SetterDeathRecord.CODE1D["code"]);
        //     Assert.Equal("system 4", SetterDeathRecord.CODE1D["system"]);
        //     Assert.Equal("display 4", SetterDeathRecord.CODE1D["display"]);
        // }

        [Fact]
        public void GetCompositionReferencesJson()
        {
            // Grab Composition
            ParserSettings parserSettings = new ParserSettings
            {
                AcceptUnknownMembers = true,
                AllowUnrecognizedEnums = true,
                PermissiveParsing = true
            };
            string bundle = File.ReadAllText(FixturePath("fixtures/json/DeathRecord1.json"));
            FhirJsonParser parser = new FhirJsonParser(parserSettings);
            Bundle b = parser.Parse<Bundle>(bundle);
            var compositionEntry = b.Entry.FirstOrDefault(entry => entry.Resource.ResourceType == ResourceType.Composition);
            var covered = false;
            if (compositionEntry != null)
            {
                Composition comp = (Composition)compositionEntry.Resource;
                Assert.Equal(4, comp.Section.Count);

                Composition.SectionComponent demographics = comp.Section.Where(s => s.Code.Coding.First().Code == "DecedentDemographics").First();
                Assert.Equal(11, demographics.Entry.Count);

                Composition.SectionComponent investigation = comp.Section.Where(s => s.Code.Coding.First().Code == "DeathInvestigation").First();
                Assert.Equal(8, investigation.Entry.Count);

                Composition.SectionComponent certification = comp.Section.Where(s => s.Code.Coding.First().Code == "DeathCertification").First();
                Assert.Equal(9, certification.Entry.Count);

                Composition.SectionComponent disposition = comp.Section.Where(s => s.Code.Coding.First().Code == "DecedentDisposition").First();
                Assert.Equal(4, disposition.Entry.Count);

                covered = true;
            }
            Assert.True(covered);

        }

        [Fact]
        public void GetCompositionReferencesXml()
        {
            // Grab Composition
            ParserSettings parserSettings = new ParserSettings
            {
                AcceptUnknownMembers = true,
                AllowUnrecognizedEnums = true,
                PermissiveParsing = true
            };
            string bundle = File.ReadAllText(FixturePath("fixtures/xml/DeathRecord1.xml"));
            FhirXmlParser parser = new FhirXmlParser(parserSettings);
            Bundle b = parser.Parse<Bundle>(bundle);
            var compositionEntry = b.Entry.FirstOrDefault(entry => entry.Resource.ResourceType == ResourceType.Composition);
            var covered = false;
            if (compositionEntry != null)
            {
                Composition comp = (Composition)compositionEntry.Resource;
                Assert.Equal(4, comp.Section.Count);

                Composition.SectionComponent demographics = comp.Section.Where(s => s.Code.Coding.First().Code == "DecedentDemographics").First();
                Assert.Equal(13, demographics.Entry.Count);

                Composition.SectionComponent investigation = comp.Section.Where(s => s.Code.Coding.First().Code == "DeathInvestigation").First();
                Assert.Equal(8, investigation.Entry.Count);

                Composition.SectionComponent certification = comp.Section.Where(s => s.Code.Coding.First().Code == "DeathCertification").First();
                Assert.Equal(8, certification.Entry.Count);

                Composition.SectionComponent disposition = comp.Section.Where(s => s.Code.Coding.First().Code == "DecedentDisposition").First();
                Assert.Equal(4, disposition.Entry.Count);

                covered = true;
            }
            Assert.True(covered);

        }

        [Fact]
        public void Set_StateSpecific()
        {
            SetterDeathRecord.StateSpecific = "State Specific Info Test";
            Assert.Equal("State Specific Info Test", SetterDeathRecord.StateSpecific);
        }

        [Fact]
        public void Get_StateSpecific()
        {
            Assert.Equal("State Specific Content", ((DeathRecord)JSONRecords[0]).StateSpecific);
            Assert.Equal("State Specific Content", ((DeathRecord)XMLRecords[0]).StateSpecific);
        }

        [Fact]
        public void Set_FilingFormat()
        {
            SetterDeathRecord.FilingFormatHelper = ValueSets.FilingFormat.Electronic;
            Assert.Equal("electronic", SetterDeathRecord.FilingFormat["code"]);
            Assert.Equal("Electronic", SetterDeathRecord.FilingFormat["display"]);
            Assert.Equal(VRDR.CodeSystems.FilingFormat, SetterDeathRecord.FilingFormat["system"]);

        }

        [Fact]
        public void Get_FilingFormat()
        {
            Assert.Equal("electronic", ((DeathRecord)JSONRecords[0]).FilingFormatHelper);
            Assert.Equal("electronic", ((DeathRecord)XMLRecords[0]).FilingFormatHelper);
        }

        [Fact]
        public void Set_ReplaceStatus()
        {
            SetterDeathRecord.ReplaceStatusHelper = ValueSets.ReplaceStatus.Original_Record;
            Assert.Equal("original", SetterDeathRecord.ReplaceStatus["code"]);
            Assert.Equal("original record", SetterDeathRecord.ReplaceStatus["display"]);
            Assert.Equal(VRDR.CodeSystems.ReplaceStatus, SetterDeathRecord.ReplaceStatus["system"]);

        }

        [Fact]
        public void Get_ReplaceStatus()
        {
            Assert.Equal("original", ((DeathRecord)JSONRecords[0]).ReplaceStatusHelper);
            Assert.Equal("original", ((DeathRecord)XMLRecords[0]).ReplaceStatusHelper);
        }
        [Fact]
        public void Set_GivenNames()
        {
            SetterDeathRecord.GivenNames = new string[] { "Example", "Something", "Middle" };
            Assert.Equal("Example", SetterDeathRecord.GivenNames[0]);
            Assert.Equal("Something", SetterDeathRecord.GivenNames[1]);
            Assert.Equal("Middle", SetterDeathRecord.GivenNames[2]);
        }

        [Fact]
        public void Get_GivenNames()
        {
            Assert.Equal("Madelyn", ((DeathRecord)JSONRecords[0]).GivenNames[0]);
            Assert.Equal("Middle", ((DeathRecord)JSONRecords[0]).GivenNames[1]);
            Assert.Equal("Madelyn", ((DeathRecord)XMLRecords[0]).GivenNames[0]);
            Assert.Equal("Middle", ((DeathRecord)XMLRecords[0]).GivenNames[1]);
        }

        [Fact]
        public void Set_FamilyName()
        {
            SetterDeathRecord.FamilyName = "Last";
            Assert.Equal("Last", SetterDeathRecord.FamilyName);
        }

        [Fact]
        public void Get_FamilyName()
        {
            Assert.Equal("Patel", ((DeathRecord)JSONRecords[0]).FamilyName);
            Assert.Equal("Patel", ((DeathRecord)XMLRecords[0]).FamilyName);
        }

        [Fact]
        public void Set_Suffix()
        {
            SetterDeathRecord.Suffix = "Jr.";
            Assert.Equal("Jr.", SetterDeathRecord.Suffix);
        }

        [Fact]
        public void Get_Suffix()
        {
            Assert.Equal("Jr.", ((DeathRecord)JSONRecords[0]).Suffix);
            Assert.Equal("Jr.", ((DeathRecord)XMLRecords[0]).Suffix);
        }

        // v1.3 OBE tests
        // [Fact]
        // public void Set_Gender()
        // {
        //     SetterDeathRecord.Gender = "male";
        //     Assert.Equal("male", SetterDeathRecord.Gender);
        // }

        // [Fact]
        // public void Get_Gender()
        // {
        //     Assert.Equal("male", ((DeathRecord)JSONRecords[0]).Gender);
        //     Assert.Equal("male", ((DeathRecord)XMLRecords[0]).Gender);
        // }

        [Fact]
        public void Set_SexAtDeath()
        {
            SetterDeathRecord.SexAtDeathHelper = "female";
            Assert.Equal("female", SetterDeathRecord.SexAtDeathHelper);
        }

        [Fact]
        public void Get_SexAtDeath()
        {
            Assert.Equal("unknown", ((DeathRecord)JSONRecords[0]).SexAtDeathHelper);
            Assert.Equal("unknown", ((DeathRecord)XMLRecords[0]).SexAtDeathHelper);
        }

        [Fact]
        public void Set_DateOfBirth()
        {
            SetterDeathRecord.DateOfBirth = "1940-02-19";
            Assert.Equal("1940-02-19", SetterDeathRecord.DateOfBirth);
        }

        [Fact]
        public void Get_DateOfBirth()
        {
            Assert.Equal("1940-02-19", ((DeathRecord)JSONRecords[0]).DateOfBirth);
            Assert.Equal("1940-02-19", ((DeathRecord)XMLRecords[0]).DateOfBirth);
        }

        [Fact]
        public void Set_Residence()
        {
            Dictionary<string, string> raddress = new Dictionary<string, string>();
            raddress.Add("addressLine1", "101 Example Street");
            raddress.Add("addressLine2", "Line 2");
            raddress.Add("addressCity", "Bedford");
            raddress.Add("addressCounty", "Middlesex");
            raddress.Add("addressState", "MA");
            raddress.Add("addressZip", "01730");
            raddress.Add("addressCountry", "US");
            raddress.Add("addressCityC", "1234");
            raddress.Add("addressCountyC", "123");
            raddress.Add("addressStnum", "101");
            raddress.Add("addressPredir", "N");
            raddress.Add("addressStname", "Example");
            raddress.Add("addressStdesig", "St");
            raddress.Add("addressPostdir", "W");
            raddress.Add("addressUnitnum", "A");

            SetterDeathRecord.Residence = raddress;

            Assert.Equal("101 Example Street", SetterDeathRecord.Residence["addressLine1"]);
            Assert.Equal("Line 2", SetterDeathRecord.Residence["addressLine2"]);
            Assert.Equal("Bedford", SetterDeathRecord.Residence["addressCity"]);
            Assert.Equal("Middlesex", SetterDeathRecord.Residence["addressCounty"]);
            Assert.Equal("MA", SetterDeathRecord.Residence["addressState"]);
            Assert.Equal("01730", SetterDeathRecord.Residence["addressZip"]);
            Assert.Equal("US", SetterDeathRecord.Residence["addressCountry"]);

            Assert.Equal("1234", SetterDeathRecord.Residence["addressCityC"]);
            Assert.Equal("123", SetterDeathRecord.Residence["addressCountyC"]);
            Assert.Equal("101", SetterDeathRecord.Residence["addressStnum"]);
            Assert.Equal("N", SetterDeathRecord.Residence["addressPredir"]);
            Assert.Equal("Example", SetterDeathRecord.Residence["addressStname"]);
            Assert.Equal("St", SetterDeathRecord.Residence["addressStdesig"]);
            Assert.Equal("W", SetterDeathRecord.Residence["addressPostdir"]);
            Assert.Equal("A", SetterDeathRecord.Residence["addressUnitnum"]);
        }

        [Fact]
        public void Get_Residence()
        {
            Assert.Equal("5590 Lockwood Drive", ((DeathRecord)JSONRecords[0]).Residence["addressLine1"]);
            Assert.Equal("", ((DeathRecord)JSONRecords[0]).Residence["addressLine2"]);
            Assert.Equal("Danville", ((DeathRecord)JSONRecords[0]).Residence["addressCity"]);
            Assert.Equal("Fairfax", ((DeathRecord)JSONRecords[0]).Residence["addressCounty"]);
            Assert.Equal("VA", ((DeathRecord)JSONRecords[0]).Residence["addressState"]);
            Assert.Equal("01730", ((DeathRecord)JSONRecords[0]).Residence["addressZip"]);
            Assert.Equal("US", ((DeathRecord)JSONRecords[0]).Residence["addressCountry"]);
            Assert.Equal("5590 Lockwood Drive", ((DeathRecord)XMLRecords[0]).Residence["addressLine1"]);
            Assert.Equal("", ((DeathRecord)XMLRecords[0]).Residence["addressLine2"]);
            Assert.Equal("Danville", ((DeathRecord)XMLRecords[0]).Residence["addressCity"]);
            Assert.Equal("Fairfax", ((DeathRecord)XMLRecords[0]).Residence["addressCounty"]);
            Assert.Equal("VA", ((DeathRecord)XMLRecords[0]).Residence["addressState"]);
            Assert.Equal("01730", ((DeathRecord)XMLRecords[0]).Residence["addressZip"]);
            Assert.Equal("US", ((DeathRecord)XMLRecords[0]).Residence["addressCountry"]);
        }

        [Fact]
        public void Get_ResidenceWithinCityLimits()
        {
            SetterDeathRecord.ResidenceWithinCityLimitsHelper = ValueSets.YesNoUnknown.No;
            Assert.Equal("N", SetterDeathRecord.ResidenceWithinCityLimits["code"]);
            SetterDeathRecord.ResidenceWithinCityLimitsHelper = ValueSets.YesNoUnknown.Yes;
            Assert.Equal("Y", SetterDeathRecord.ResidenceWithinCityLimits["code"]);
            SetterDeathRecord.ResidenceWithinCityLimitsHelper = ValueSets.YesNoUnknown.Unknown;
            Assert.Equal("UNK", SetterDeathRecord.ResidenceWithinCityLimits["code"]);
        }

        [Fact]
        public void Set_ResidenceWithinCityLimits()
        {
            Assert.Equal("Y", ((DeathRecord)JSONRecords[0]).ResidenceWithinCityLimitsHelper);
            Assert.Equal("Y", ((DeathRecord)XMLRecords[0]).ResidenceWithinCityLimitsHelper);
        }

        [Fact]
        public void Set_SSN()
        {
            SetterDeathRecord.SSN = "123456789";
            Assert.Equal("123456789", SetterDeathRecord.SSN);
        }

        [Fact]
        public void Get_SSN()
        {
            Assert.Equal("987654321", ((DeathRecord)JSONRecords[0]).SSN);
            Assert.Equal("987654321", ((DeathRecord)XMLRecords[0]).SSN);
        }

        [Fact]
        public void Set_Ethnicity()
        {
            SetterDeathRecord.EthnicityLiteral = "Hispanic or Latino, Puerto Rican";
            SetterDeathRecord.Ethnicity2Helper = "Y";
            Assert.Equal("Y", SetterDeathRecord.Ethnicity2Helper);
            Assert.Equal("Hispanic or Latino, Puerto Rican", SetterDeathRecord.EthnicityLiteral);
        }

        [Fact]
        public void Get_Ethnicity()
        {
            Assert.Equal("Y", ((DeathRecord)JSONRecords[0]).Ethnicity1Helper);
            Assert.Equal("Y", ((DeathRecord)XMLRecords[0]).Ethnicity1Helper);
        }

        [Fact]
        public void Set_BirthDate_Partial_Date()
        {
            SetterDeathRecord.BirthYear = 1950;
            SetterDeathRecord.BirthMonth = null;
            SetterDeathRecord.BirthDay = null;
            Assert.Equal(1950, (int)SetterDeathRecord.BirthYear);
            Assert.Null(SetterDeathRecord.BirthMonth);
            Assert.Null(SetterDeathRecord.BirthDay);
        }

        [Fact]
        public void Get_BirthDate_Partial_Date()
        {
            DeathRecord dr = new DeathRecord(File.ReadAllText(FixturePath("fixtures/json/BirthAndDeathDateDataAbsent.json")));
            Assert.Null(dr.BirthYear);
            Assert.Null(dr.BirthMonth);
            Assert.Equal(24, (int)dr.BirthDay);
        }


        [Fact]
        public void Get_BirthDate_Partial_Date_Roundtrip()
        {
            DeathRecord dr = new DeathRecord(File.ReadAllText(FixturePath("fixtures/json/BirthAndDeathDateDataAbsent.json")));
            IJEMortality ije1 = new IJEMortality(dr);
            Assert.Equal("9999", ije1.DOB_YR);
            Assert.Equal("99", ije1.DOB_MO);
            Assert.Equal("24", ije1.DOB_DY);
            DeathRecord dr1 = ije1.ToDeathRecord();
            Assert.Null(dr1.BirthYear);
            Assert.Null(dr1.BirthMonth);
            Assert.Equal(24, (int)dr1.BirthDay);
            Assert.Null(dr1.DateOfBirth);
        }

        [Fact]
        public void Set_Race()
        {
            Tuple<string, string>[] race = new Tuple<string, string>[] { Tuple.Create(NvssRace.White, "Y"), Tuple.Create(NvssRace.NativeHawaiian, "Y"), Tuple.Create(NvssRace.OtherPacificIslandLiteral1, "White, Native Hawaiian or Other Pacific Islander") };
            SetterDeathRecord.Race = race;
            Assert.Equal(race[0], SetterDeathRecord.Race[0]);
            Assert.Equal(race[1], SetterDeathRecord.Race[1]);
            Assert.Equal(race[2], SetterDeathRecord.Race[2]);
        }

        [Fact]
        public void Get_Race()
        {
            Assert.Equal(Tuple.Create(NvssRace.White, "Y"), ((DeathRecord)JSONRecords[0]).Race[0]);
            Assert.Equal(Tuple.Create(NvssRace.BlackOrAfricanAmerican, "N"), ((DeathRecord)JSONRecords[0]).Race[1]);
            Assert.Equal(Tuple.Create(NvssRace.AmericanIndianOrAlaskaNative, "N"), ((DeathRecord)JSONRecords[0]).Race[2]);
            Assert.Equal(Tuple.Create(NvssRace.AsianIndian, "N"), ((DeathRecord)JSONRecords[0]).Race[3]);
            Assert.Equal(Tuple.Create(NvssRace.Chinese, "N"), ((DeathRecord)JSONRecords[0]).Race[4]);
            Assert.Equal(Tuple.Create(NvssRace.Filipino, "N"), ((DeathRecord)JSONRecords[0]).Race[5]);
            Assert.Equal(Tuple.Create(NvssRace.Japanese, "N"), ((DeathRecord)JSONRecords[0]).Race[6]);
            Assert.Equal(Tuple.Create(NvssRace.Korean, "N"), ((DeathRecord)JSONRecords[0]).Race[7]);
            Assert.Equal(Tuple.Create(NvssRace.Vietnamese, "N"), ((DeathRecord)JSONRecords[0]).Race[8]);
            Assert.Equal(Tuple.Create(NvssRace.OtherAsian, "N"), ((DeathRecord)JSONRecords[0]).Race[9]);
            Assert.Equal(Tuple.Create(NvssRace.NativeHawaiian, "N"), ((DeathRecord)JSONRecords[0]).Race[10]);
            Assert.Equal(Tuple.Create(NvssRace.GuamanianOrChamorro, "N"), ((DeathRecord)JSONRecords[0]).Race[11]);
            Assert.Equal(Tuple.Create(NvssRace.Samoan, "N"), ((DeathRecord)JSONRecords[0]).Race[12]);
            Assert.Equal(Tuple.Create(NvssRace.OtherPacificIslander, "N"), ((DeathRecord)JSONRecords[0]).Race[13]);
            Assert.Equal(Tuple.Create(NvssRace.OtherRace, "N"), ((DeathRecord)JSONRecords[0]).Race[14]);

            Assert.Equal(Tuple.Create(NvssRace.White, "Y"), ((DeathRecord)XMLRecords[0]).Race[0]);
            Assert.Equal(Tuple.Create(NvssRace.BlackOrAfricanAmerican, "N"), ((DeathRecord)XMLRecords[0]).Race[1]);
            Assert.Equal(Tuple.Create(NvssRace.AmericanIndianOrAlaskaNative, "N"), ((DeathRecord)XMLRecords[0]).Race[2]);
            Assert.Equal(Tuple.Create(NvssRace.AsianIndian, "N"), ((DeathRecord)XMLRecords[0]).Race[3]);
            Assert.Equal(Tuple.Create(NvssRace.Chinese, "N"), ((DeathRecord)XMLRecords[0]).Race[4]);
            Assert.Equal(Tuple.Create(NvssRace.Filipino, "N"), ((DeathRecord)XMLRecords[0]).Race[5]);
            Assert.Equal(Tuple.Create(NvssRace.Japanese, "N"), ((DeathRecord)XMLRecords[0]).Race[6]);
            Assert.Equal(Tuple.Create(NvssRace.Korean, "N"), ((DeathRecord)XMLRecords[0]).Race[7]);
            Assert.Equal(Tuple.Create(NvssRace.Vietnamese, "N"), ((DeathRecord)XMLRecords[0]).Race[8]);
            Assert.Equal(Tuple.Create(NvssRace.OtherAsian, "N"), ((DeathRecord)XMLRecords[0]).Race[9]);
            Assert.Equal(Tuple.Create(NvssRace.NativeHawaiian, "N"), ((DeathRecord)XMLRecords[0]).Race[10]);
            Assert.Equal(Tuple.Create(NvssRace.GuamanianOrChamorro, "N"), ((DeathRecord)XMLRecords[0]).Race[11]);
            Assert.Equal(Tuple.Create(NvssRace.Samoan, "N"), ((DeathRecord)XMLRecords[0]).Race[12]);
            Assert.Equal(Tuple.Create(NvssRace.OtherPacificIslander, "N"), ((DeathRecord)XMLRecords[0]).Race[13]);
            Assert.Equal(Tuple.Create(NvssRace.OtherRace, "N"), ((DeathRecord)XMLRecords[0]).Race[14]);
        }

        [Fact]
        public void Set_PlaceOfBirth()
        {
            Dictionary<string, string> pobaddress = new Dictionary<string, string>();
            pobaddress.Add("addressLine1", "1011 Example Street");
            pobaddress.Add("addressLine2", "Line 2");
            pobaddress.Add("addressCity", "Bedford");
            pobaddress.Add("addressCounty", "Middlesex");
            pobaddress.Add("addressState", "MA");
            pobaddress.Add("addressZip", "01730");
            pobaddress.Add("addressCountry", "US");
            SetterDeathRecord.PlaceOfBirth = pobaddress;
            Assert.Equal("1011 Example Street", SetterDeathRecord.PlaceOfBirth["addressLine1"]);
            Assert.Equal("Line 2", SetterDeathRecord.PlaceOfBirth["addressLine2"]);
            Assert.Equal("Bedford", SetterDeathRecord.PlaceOfBirth["addressCity"]);
            Assert.Equal("Middlesex", SetterDeathRecord.PlaceOfBirth["addressCounty"]);
            Assert.Equal("MA", SetterDeathRecord.PlaceOfBirth["addressState"]);
            Assert.Equal("01730", SetterDeathRecord.PlaceOfBirth["addressZip"]);
            Assert.Equal("US", SetterDeathRecord.PlaceOfBirth["addressCountry"]);
        }

        [Fact]
        public void Get_PlaceOfBirth()
        {
            Assert.Equal("", ((DeathRecord)JSONRecords[0]).PlaceOfBirth["addressLine1"]);
            Assert.Equal("", ((DeathRecord)JSONRecords[0]).PlaceOfBirth["addressLine2"]);
            Assert.Equal("Roanoke", ((DeathRecord)JSONRecords[0]).PlaceOfBirth["addressCity"]);
            Assert.Equal("", ((DeathRecord)JSONRecords[0]).PlaceOfBirth["addressCounty"]);
            Assert.Equal("VA", ((DeathRecord)JSONRecords[0]).PlaceOfBirth["addressState"]);
            Assert.Equal("", ((DeathRecord)JSONRecords[0]).PlaceOfBirth["addressZip"]);
            Assert.Equal("US", ((DeathRecord)JSONRecords[0]).PlaceOfBirth["addressCountry"]);
            Assert.Equal("", ((DeathRecord)XMLRecords[0]).PlaceOfBirth["addressLine1"]);
            Assert.Equal("", ((DeathRecord)XMLRecords[0]).PlaceOfBirth["addressLine2"]);
            Assert.Equal("Roanoke", ((DeathRecord)XMLRecords[0]).PlaceOfBirth["addressCity"]);
            Assert.Equal("", ((DeathRecord)XMLRecords[0]).PlaceOfBirth["addressCounty"]);
            Assert.Equal("VA", ((DeathRecord)XMLRecords[0]).PlaceOfBirth["addressState"]);
            Assert.Equal("", ((DeathRecord)XMLRecords[0]).PlaceOfBirth["addressZip"]);
            Assert.Equal("US", ((DeathRecord)XMLRecords[0]).PlaceOfBirth["addressCountry"]);
        }

        [Fact]
        public void Set_MaritalStatus()
        {
            SetterDeathRecord.MaritalStatusHelper = ValueSets.MaritalStatus.Never_Married;
            Assert.Equal(ValueSets.MaritalStatus.Never_Married, SetterDeathRecord.MaritalStatus["code"]);
            Assert.Equal(VRDR.CodeSystems.PH_MaritalStatus_HL7_2x, SetterDeathRecord.MaritalStatus["system"]);
            Assert.Equal("Never Married", SetterDeathRecord.MaritalStatus["display"]);
        }

        [Fact]
        public void Get_MaritalStatus()
        {
            Assert.Equal(ValueSets.MaritalStatus.Never_Married, ((DeathRecord)JSONRecords[0]).MaritalStatus["code"]);
            Assert.Equal(VRDR.CodeSystems.PH_MaritalStatus_HL7_2x, ((DeathRecord)JSONRecords[0]).MaritalStatus["system"]);
            Assert.Equal("Never Married", ((DeathRecord)JSONRecords[0]).MaritalStatus["display"]);
            Assert.Equal(ValueSets.MaritalStatus.Never_Married, ((DeathRecord)XMLRecords[0]).MaritalStatus["code"]);
            Assert.Equal(VRDR.CodeSystems.PH_MaritalStatus_HL7_2x, ((DeathRecord)XMLRecords[0]).MaritalStatus["system"]);
            Assert.Equal("Never Married", ((DeathRecord)XMLRecords[0]).MaritalStatus["display"]);
        }

        [Fact]
        public void Set_MaritalStatusEditFlag()
        {
            SetterDeathRecord.MaritalStatusEditFlagHelper = ValueSets.EditBypass0124.Edit_Passed;
            Assert.Equal(ValueSets.EditBypass0124.Edit_Passed, SetterDeathRecord.MaritalStatusEditFlag["code"]);
            Assert.Equal(VRDR.CodeSystems.BypassEditFlag, SetterDeathRecord.MaritalStatusEditFlag["system"]);
            Assert.Equal("Edit Passed", SetterDeathRecord.MaritalStatusEditFlag["display"]);
        }

        [Fact]
        public void Get_MaritalStatusEditFlag()
        {
            Assert.Equal(ValueSets.EditBypass0124.Edit_Passed, ((DeathRecord)JSONRecords[0]).MaritalStatusEditFlag["code"]);
            Assert.Equal(VRDR.CodeSystems.BypassEditFlag, ((DeathRecord)JSONRecords[0]).MaritalStatusEditFlag["system"]);
            Assert.Equal("Edit Passed", ((DeathRecord)JSONRecords[0]).MaritalStatusEditFlag["display"]);
            Assert.Equal(ValueSets.EditBypass0124.Edit_Passed, ((DeathRecord)XMLRecords[0]).MaritalStatusEditFlag["code"]);
            Assert.Equal(VRDR.CodeSystems.BypassEditFlag, ((DeathRecord)XMLRecords[0]).MaritalStatusEditFlag["system"]);
            Assert.Equal("Edit Passed", ((DeathRecord)XMLRecords[0]).MaritalStatusEditFlag["display"]);
        }

        [Fact]
        public void Get_MaritalStatusEditFlagHelper()
        {
            SetterDeathRecord.MaritalStatusEditFlagHelper = ValueSets.EditBypass0124.Edit_Passed;
            Assert.Equal(ValueSets.EditBypass0124.Edit_Passed, SetterDeathRecord.MaritalStatusEditFlagHelper);
        }

        [Fact]
        public void Get_MaritalStatusAndBypass()
        {
            SetterDeathRecord.MaritalStatusHelper = ValueSets.MaritalStatus.Never_Married;
            SetterDeathRecord.MaritalStatusEditFlagHelper = ValueSets.EditBypass0124.Edit_Passed;

            Assert.Equal(ValueSets.MaritalStatus.Never_Married, SetterDeathRecord.MaritalStatus["code"]);
            Assert.Equal(VRDR.CodeSystems.PH_MaritalStatus_HL7_2x, SetterDeathRecord.MaritalStatus["system"]);
            Assert.Equal("Never Married", SetterDeathRecord.MaritalStatus["display"]);
            Assert.Equal(ValueSets.EditBypass0124.Edit_Passed, SetterDeathRecord.MaritalStatusEditFlagHelper);
        }

        [Fact]
        public void Set_MaritalDescriptor()
        {
            SetterDeathRecord.MaritalStatusLiteral = "Single";
            Assert.Equal("Single", SetterDeathRecord.MaritalStatusLiteral);
        }

        [Fact]
        public void Get_MaritalDescriptor()
        {
            Assert.Equal("Single", ((DeathRecord)JSONRecords[0]).MaritalStatusLiteral);
            Assert.Equal("Single", ((DeathRecord)XMLRecords[0]).MaritalStatusLiteral);
        }

        [Fact]
        public void Set_FatherGivenNames()
        {
            string[] fnames = { "Father", "Middle" };
            SetterDeathRecord.FatherGivenNames = fnames;
            Assert.Equal("Father", SetterDeathRecord.FatherGivenNames[0]);
            Assert.Equal("Middle", SetterDeathRecord.FatherGivenNames[1]);
        }

        [Fact]
        public void Get_FatherGivenNames()
        {
            Assert.Equal("Father", ((DeathRecord)JSONRecords[0]).FatherGivenNames[0]);
            Assert.Equal("Middle", ((DeathRecord)JSONRecords[0]).FatherGivenNames[1]);
            Assert.Equal("Father", ((DeathRecord)XMLRecords[0]).FatherGivenNames[0]);
            Assert.Equal("Middle", ((DeathRecord)XMLRecords[0]).FatherGivenNames[1]);
        }

        [Fact]
        public void Set_FatherFamilyName()
        {
            SetterDeathRecord.FatherFamilyName = "Last";
            Assert.Equal("Last", SetterDeathRecord.FatherFamilyName);
        }

        [Fact]
        public void Get_FatherFamilyName()
        {
            Assert.Equal("Last", ((DeathRecord)JSONRecords[0]).FatherFamilyName);
            Assert.Equal("Last", ((DeathRecord)XMLRecords[0]).FatherFamilyName);
        }

        [Fact]
        public void Set_FatherSuffix()
        {
            SetterDeathRecord.FatherSuffix = "Sr.";
            Assert.Equal("Sr.", SetterDeathRecord.FatherSuffix);
        }

        [Fact]
        public void Get_FatherSuffix()
        {
            Assert.Equal("Sr.", ((DeathRecord)JSONRecords[0]).FatherSuffix);
            Assert.Equal("Sr.", ((DeathRecord)XMLRecords[0]).FatherSuffix);
        }

        [Fact]
        public void Set_MotherGivenNames()
        {
            string[] mnames = { "Mother", "Middle" };
            SetterDeathRecord.MotherGivenNames = mnames;
            Assert.Equal("Mother", SetterDeathRecord.MotherGivenNames[0]);
            Assert.Equal("Middle", SetterDeathRecord.MotherGivenNames[1]);
        }

        [Fact]
        public void Get_MotherGivenNames()
        {
            Assert.Equal("Mother", ((DeathRecord)JSONRecords[0]).MotherGivenNames[0]);
            Assert.Equal("Middle", ((DeathRecord)JSONRecords[0]).MotherGivenNames[1]);
            Assert.Equal("Mother", ((DeathRecord)XMLRecords[0]).MotherGivenNames[0]);
            Assert.Equal("Middle", ((DeathRecord)XMLRecords[0]).MotherGivenNames[1]);
        }

        [Fact]
        public void Set_MotherMaidenName()
        {
            SetterDeathRecord.MotherMaidenName = "Maiden";
            Assert.Equal("Maiden", SetterDeathRecord.MotherMaidenName);
        }

        [Fact]
        public void Get_MotherMaidenName()
        {
            Assert.Equal("Maiden", ((DeathRecord)JSONRecords[0]).MotherMaidenName);
            Assert.Equal("Maiden", ((DeathRecord)XMLRecords[0]).MotherMaidenName);
        }

        [Fact]
        public void Set_MotherSuffix()
        {
            SetterDeathRecord.MotherSuffix = "Dr.";
            Assert.Equal("Dr.", SetterDeathRecord.MotherSuffix);
        }

        [Fact]
        public void Get_MotherSuffix()
        {
            Assert.Equal("Dr.", ((DeathRecord)JSONRecords[0]).MotherSuffix);
            Assert.Equal("Dr.", ((DeathRecord)XMLRecords[0]).MotherSuffix);
        }

        [Fact]
        public void Set_ContactRelationship()
        {
            Dictionary<string, string> relationship = new Dictionary<string, string>();
            relationship.Add("text", "sibling");
            SetterDeathRecord.ContactRelationship = relationship;
            Assert.Equal("sibling", SetterDeathRecord.ContactRelationship["text"]);
        }

        [Fact]
        public void Get_ContactRelationship()
        {
            Assert.Equal("Friend of family", ((DeathRecord)JSONRecords[0]).ContactRelationship["text"]);
            Assert.Equal("Friend of family", ((DeathRecord)XMLRecords[0]).ContactRelationship["text"]);
        }

        [Fact]
        public void Set_SpouseLiving()
        {
            SetterDeathRecord.SpouseAliveHelper = ValueSets.YesNoUnknownNotApplicable.Yes;
            Assert.Equal(ValueSets.YesNoUnknownNotApplicable.Yes, SetterDeathRecord.SpouseAlive["code"]);
            Assert.Equal(VRDR.CodeSystems.YesNo, SetterDeathRecord.SpouseAlive["system"]);
            Assert.Equal("Yes", SetterDeathRecord.SpouseAlive["display"]);
        }

        [Fact]
        public void Get_SpouseLiving()
        {
            Assert.Equal(ValueSets.YesNoUnknownNotApplicable.Yes, ((DeathRecord)JSONRecords[0]).SpouseAlive["code"]);
            Assert.Equal(VRDR.CodeSystems.YesNo, ((DeathRecord)JSONRecords[0]).SpouseAlive["system"]);
            Assert.Equal("Yes", ((DeathRecord)JSONRecords[0]).SpouseAlive["display"]);
            Assert.Equal(ValueSets.YesNoUnknownNotApplicable.Yes, ((DeathRecord)XMLRecords[0]).SpouseAlive["code"]);
            Assert.Equal(VRDR.CodeSystems.YesNo, ((DeathRecord)XMLRecords[0]).SpouseAlive["system"]);
            Assert.Equal("Yes", ((DeathRecord)XMLRecords[0]).SpouseAlive["display"]);
        }

        [Fact]
        public void Set_SpouseGivenNames()
        {
            string[] spnames = { "Spouse", "Middle" };
            SetterDeathRecord.SpouseGivenNames = spnames;
            Assert.Equal("Spouse", SetterDeathRecord.SpouseGivenNames[0]);
            Assert.Equal("Middle", SetterDeathRecord.SpouseGivenNames[1]);
        }

        [Fact]
        public void Get_SpouseGivenNames()
        {
            Assert.Equal("Spouse", ((DeathRecord)JSONRecords[0]).SpouseGivenNames[0]);
            Assert.Equal("Middle", ((DeathRecord)JSONRecords[0]).SpouseGivenNames[1]);
            Assert.Equal("Spouse", ((DeathRecord)XMLRecords[0]).SpouseGivenNames[0]);
            Assert.Equal("Middle", ((DeathRecord)XMLRecords[0]).SpouseGivenNames[1]);
        }

        [Fact]
        public void Set_SpouseFamilyName()
        {
            SetterDeathRecord.SpouseFamilyName = "Last";
            Assert.Equal("Last", SetterDeathRecord.SpouseFamilyName);
        }

        [Fact]
        public void Get_SpouseFamilyName()
        {
            Assert.Equal("Last", ((DeathRecord)JSONRecords[0]).SpouseFamilyName);
            Assert.Equal("Last", ((DeathRecord)XMLRecords[0]).SpouseFamilyName);
        }

        [Fact]
        public void Set_SpouseMaidenName()
        {
            SetterDeathRecord.SpouseMaidenName = "Maiden";
            Assert.Equal("Maiden", SetterDeathRecord.SpouseMaidenName);
        }

        [Fact]
        public void Get_SpouseMaidenName()
        {
            Assert.Equal("Maiden", ((DeathRecord)JSONRecords[0]).SpouseMaidenName);
            Assert.Equal("Maiden", ((DeathRecord)XMLRecords[0]).SpouseMaidenName);
        }
        [Fact]
        public void Set_SpouseSuffix()
        {
            SetterDeathRecord.SpouseSuffix = "Ph.D.";
            Assert.Equal("Ph.D.", SetterDeathRecord.SpouseSuffix);
        }

        [Fact]
        public void Get_SpouseSuffix()
        {
            Assert.Equal("Ph.D.", ((DeathRecord)JSONRecords[0]).SpouseSuffix);
            Assert.Equal("Ph.D.", ((DeathRecord)XMLRecords[0]).SpouseSuffix);
        }

        [Fact]
        public void Set_EducationLevel()
        {
            SetterDeathRecord.EducationLevelHelper = VRDR.ValueSets.EducationLevel.Bachelors_Degree;
            Assert.Equal(VRDR.ValueSets.EducationLevel.Bachelors_Degree, SetterDeathRecord.EducationLevel["code"]);
            Assert.Equal(VRDR.CodeSystems.DegreeLicenceAndCertificate, SetterDeathRecord.EducationLevel["system"]);
            Assert.Equal("Bachelor's degree", SetterDeathRecord.EducationLevel["display"]);
            SetterDeathRecord.EducationLevelHelper = VRDR.ValueSets.EducationLevel.Associates_Or_Technical_Degree_Complete;
            Assert.Equal(VRDR.ValueSets.EducationLevel.Associates_Or_Technical_Degree_Complete, SetterDeathRecord.EducationLevelHelper);

        }

        [Fact]
        public void Get_EducationLevel()
        {
            Assert.Equal(VRDR.ValueSets.EducationLevel.Bachelors_Degree, ((DeathRecord)JSONRecords[0]).EducationLevelHelper);
            Assert.Equal(VRDR.CodeSystems.DegreeLicenceAndCertificate, ((DeathRecord)JSONRecords[0]).EducationLevel["system"]);
            Assert.Equal("Bachelor's Degree", ((DeathRecord)JSONRecords[0]).EducationLevel["display"]);
            Assert.Equal(VRDR.ValueSets.EducationLevel.Bachelors_Degree, ((DeathRecord)XMLRecords[0]).EducationLevelHelper);
            Assert.Equal(VRDR.CodeSystems.DegreeLicenceAndCertificate, ((DeathRecord)XMLRecords[0]).EducationLevel["system"]);
            Assert.Equal("Bachelor's Degree", ((DeathRecord)XMLRecords[0]).EducationLevel["display"]);
        }

        [Fact]
        public void Set_EducationLevelEditFlag()
        {
            Dictionary<string, string> elef = new Dictionary<string, string>();
            elef.Add("code", VRDR.ValueSets.EditBypass01234.Edit_Failed_Data_Queried_And_Verified);
            elef.Add("system", VRDR.CodeSystems.BypassEditFlag);
            elef.Add("display", "Edit Failed, Data Queried, and Verified");
            SetterDeathRecord.EducationLevelEditFlag = elef;
            Assert.Equal(VRDR.ValueSets.EditBypass01234.Edit_Failed_Data_Queried_And_Verified, SetterDeathRecord.EducationLevelEditFlag["code"]);
            Assert.Equal(VRDR.CodeSystems.BypassEditFlag, SetterDeathRecord.EducationLevelEditFlag["system"]);
            Assert.Equal("Edit Failed, Data Queried, and Verified", SetterDeathRecord.EducationLevelEditFlag["display"]);
            SetterDeathRecord.EducationLevelEditFlagHelper = VRDR.ValueSets.EditBypass01234.Edit_Passed;
            Assert.Equal(VRDR.ValueSets.EditBypass01234.Edit_Passed, SetterDeathRecord.EducationLevelEditFlagHelper);
            Assert.Equal(VRDR.CodeSystems.BypassEditFlag, SetterDeathRecord.EducationLevelEditFlag["system"]);
            Assert.Equal("Edit Passed", SetterDeathRecord.EducationLevelEditFlag["display"]);
        }

        [Fact]
        public void Get_EducationLevelEditFlag()
        {
            Assert.Equal(VRDR.ValueSets.EditBypass01234.Edit_Passed, ((DeathRecord)JSONRecords[0]).EducationLevelEditFlagHelper);
            Assert.Equal(VRDR.CodeSystems.BypassEditFlag, ((DeathRecord)JSONRecords[0]).EducationLevelEditFlag["system"]);
            Assert.Equal("Edit Passed", ((DeathRecord)JSONRecords[0]).EducationLevelEditFlag["display"]);
            Assert.Equal(VRDR.ValueSets.EditBypass01234.Edit_Passed, ((DeathRecord)XMLRecords[0]).EducationLevelEditFlagHelper);
            Assert.Equal(VRDR.CodeSystems.BypassEditFlag, ((DeathRecord)XMLRecords[0]).EducationLevelEditFlag["system"]);
            Assert.Equal("Edit Passed", ((DeathRecord)XMLRecords[0]).EducationLevelEditFlag["display"]);
        }

        [Fact]
        public void Set_ActivityAtTimeOfDeath()
        {
            SetterDeathRecord.ActivityAtDeathHelper = VRDR.ValueSets.ActivityAtTimeOfDeath.While_Resting_Sleeping_Eating_Or_Engaging_In_Other_Vital_Activities;
            Assert.Equal(VRDR.ValueSets.ActivityAtTimeOfDeath.While_Resting_Sleeping_Eating_Or_Engaging_In_Other_Vital_Activities, SetterDeathRecord.ActivityAtDeath["code"]);
            Assert.Equal(VRDR.CodeSystems.ActivityAtTimeOfDeath, SetterDeathRecord.ActivityAtDeath["system"]);
            Assert.Equal("While resting, sleeping, eating, or engaging in other vital activities", SetterDeathRecord.ActivityAtDeath["display"]);
            SetterDeathRecord.ActivityAtDeathHelper = VRDR.ValueSets.ActivityAtTimeOfDeath.While_Resting_Sleeping_Eating_Or_Engaging_In_Other_Vital_Activities;
            Assert.Equal(VRDR.ValueSets.ActivityAtTimeOfDeath.While_Resting_Sleeping_Eating_Or_Engaging_In_Other_Vital_Activities, SetterDeathRecord.ActivityAtDeathHelper);

        }
        [Fact]
        public void Get_ActivityAtTimeOfDeath()
        {
            Assert.Equal(VRDR.ValueSets.ActivityAtTimeOfDeath.While_Engaged_In_Leisure_Activities, ((DeathRecord)JSONRecords[1]).ActivityAtDeathHelper);
        }

        [Fact]
        public void Set_AutomatedUnderlyingCOD()
        {
            SetterDeathRecord.AutomatedUnderlyingCOD = "I131";
            Assert.Equal("I131", SetterDeathRecord.AutomatedUnderlyingCOD);
            SetterDeathRecord.AutomatedUnderlyingCOD = "I13.1";
            Assert.Equal("I13.1", SetterDeathRecord.AutomatedUnderlyingCOD);
            SetterDeathRecord.AutomatedUnderlyingCOD = "I13.";
            Assert.Equal("I13.", SetterDeathRecord.AutomatedUnderlyingCOD);
            SetterDeathRecord.AutomatedUnderlyingCOD = "I13";
            Assert.Equal("I13", SetterDeathRecord.AutomatedUnderlyingCOD);
        }

        [Fact]
        public void Get_AutomatedUnderlyingCOD()
        {
            Assert.Equal("J96.0", ((DeathRecord)JSONRecords[1]).AutomatedUnderlyingCOD);
        }

        [Fact]
        public void Set_ManUnderlyingCOD()
        {
            SetterDeathRecord.ManUnderlyingCOD = "I131";
            Assert.Equal("I131", SetterDeathRecord.ManUnderlyingCOD);
            SetterDeathRecord.ManUnderlyingCOD = "I13.1";
            Assert.Equal("I13.1", SetterDeathRecord.ManUnderlyingCOD);
            SetterDeathRecord.ManUnderlyingCOD = "I13.";
            Assert.Equal("I13.", SetterDeathRecord.ManUnderlyingCOD);
            SetterDeathRecord.ManUnderlyingCOD = "I13";
            Assert.Equal("I13", SetterDeathRecord.ManUnderlyingCOD);
        }
        [Fact]
        public void Get_ManUnderlyingCOD()
        {
            Assert.Equal("J96.0", ((DeathRecord)JSONRecords[1]).ManUnderlyingCOD);
        }

        [Fact]
        public void Set_PlaceOfInjury()
        {
            SetterDeathRecord.PlaceOfInjuryHelper = ValueSets.PlaceOfInjury.Home;
            Assert.Equal(ValueSets.PlaceOfInjury.Home, SetterDeathRecord.PlaceOfInjuryHelper);
        }
        [Fact]
        public void Get_PlaceOfInjury()
        {
            Assert.Equal(ValueSets.PlaceOfInjury.Home, ((DeathRecord)JSONRecords[1]).PlaceOfInjuryHelper);
        }
        [Fact]
        public void Set_EditedRaceCodes()
        {
            SetterDeathRecord.FirstEditedRaceCodeHelper = ValueSets.RaceCode.African;
            SetterDeathRecord.SecondEditedRaceCodeHelper = ValueSets.RaceCode.Asian;
            SetterDeathRecord.ThirdEditedRaceCodeHelper = ValueSets.RaceCode.Blackfeet;
            SetterDeathRecord.FourthEditedRaceCodeHelper = ValueSets.RaceCode.Jamestown_Sklallam;
            SetterDeathRecord.FifthEditedRaceCodeHelper = ValueSets.RaceCode.Kaw;
            SetterDeathRecord.SixthEditedRaceCodeHelper = ValueSets.RaceCode.Madagascar;
            SetterDeathRecord.SeventhEditedRaceCodeHelper = ValueSets.RaceCode.Okinawan;
            SetterDeathRecord.EighthEditedRaceCodeHelper = ValueSets.RaceCode.Zaire;
            SetterDeathRecord.FirstAmericanIndianRaceCodeHelper = ValueSets.RaceCode.Navajo;
            SetterDeathRecord.SecondAmericanIndianRaceCodeHelper = ValueSets.RaceCode.Stockbridgemunsee_Community_Of_Mohican_Indians_Of_Wisconsin;
            SetterDeathRecord.FirstOtherAsianRaceCodeHelper = ValueSets.RaceCode.Malaysian;
            SetterDeathRecord.SecondOtherAsianRaceCodeHelper = ValueSets.RaceCode.Burmese;
            SetterDeathRecord.FirstOtherPacificIslanderRaceCodeHelper = ValueSets.RaceCode.Taiwanese;
            SetterDeathRecord.SecondOtherPacificIslanderRaceCodeHelper = ValueSets.RaceCode.New_Hebrides;
            SetterDeathRecord.FirstOtherRaceCodeHelper = ValueSets.RaceCode.Lebanese;
            SetterDeathRecord.SecondOtherRaceCodeHelper = ValueSets.RaceCode.Palestinian;
            SetterDeathRecord.HispanicCodeForLiteralHelper = ValueSets.HispanicOrigin.Canal_Zone;
            SetterDeathRecord.HispanicCodeHelper = ValueSets.HispanicOrigin.Cuban;
            SetterDeathRecord.RaceRecode40Helper = ValueSets.RaceRecode40.Aian_And_Asian;
            Assert.Equal(ValueSets.RaceCode.African, SetterDeathRecord.FirstEditedRaceCodeHelper);
            Assert.Equal(ValueSets.RaceCode.Asian, SetterDeathRecord.SecondEditedRaceCodeHelper);
            Assert.Equal(ValueSets.RaceCode.Blackfeet, SetterDeathRecord.ThirdEditedRaceCodeHelper);
            Assert.Equal(ValueSets.RaceCode.Jamestown_Sklallam, SetterDeathRecord.FourthEditedRaceCodeHelper);
            Assert.Equal(ValueSets.RaceCode.Kaw, SetterDeathRecord.FifthEditedRaceCodeHelper);
            Assert.Equal(ValueSets.RaceCode.Madagascar, SetterDeathRecord.SixthEditedRaceCodeHelper);
            Assert.Equal(ValueSets.RaceCode.Okinawan, SetterDeathRecord.SeventhEditedRaceCodeHelper);
            Assert.Equal(ValueSets.RaceCode.Zaire, SetterDeathRecord.EighthEditedRaceCodeHelper);
            Assert.Equal(ValueSets.RaceCode.Navajo, SetterDeathRecord.FirstAmericanIndianRaceCodeHelper);
            Assert.Equal(ValueSets.RaceCode.Stockbridgemunsee_Community_Of_Mohican_Indians_Of_Wisconsin, SetterDeathRecord.SecondAmericanIndianRaceCodeHelper);
            Assert.Equal(ValueSets.RaceCode.Malaysian, SetterDeathRecord.FirstOtherAsianRaceCodeHelper);
            Assert.Equal(ValueSets.RaceCode.Burmese, SetterDeathRecord.SecondOtherAsianRaceCodeHelper);
            Assert.Equal(ValueSets.RaceCode.Taiwanese, SetterDeathRecord.FirstOtherPacificIslanderRaceCodeHelper);
            Assert.Equal(ValueSets.RaceCode.New_Hebrides, SetterDeathRecord.SecondOtherPacificIslanderRaceCodeHelper);
            Assert.Equal(ValueSets.RaceCode.Lebanese, SetterDeathRecord.FirstOtherRaceCodeHelper);
            Assert.Equal(ValueSets.RaceCode.Palestinian, SetterDeathRecord.SecondOtherRaceCodeHelper);
            Assert.Equal(ValueSets.HispanicOrigin.Canal_Zone, SetterDeathRecord.HispanicCodeForLiteralHelper);
            Assert.Equal(ValueSets.HispanicOrigin.Cuban, SetterDeathRecord.HispanicCodeHelper);
            Assert.Equal(ValueSets.RaceRecode40.Aian_And_Asian, SetterDeathRecord.RaceRecode40Helper);
        }
        [Fact]
        public void Get_EditedRaceCodes()
        {
            Assert.Equal(ValueSets.RaceCode.White, ((DeathRecord)JSONRecords[1]).FirstEditedRaceCodeHelper);
            Assert.Equal(ValueSets.RaceCode.Israeli, ((DeathRecord)JSONRecords[1]).SecondEditedRaceCodeHelper);
            Assert.Equal(ValueSets.HispanicOrigin.Chilean, ((DeathRecord)JSONRecords[1]).HispanicCodeHelper);
            Assert.Equal(ValueSets.RaceRecode40.Aian_And_Asian, ((DeathRecord)JSONRecords[1]).RaceRecode40Helper);
        }



        [Fact]
        public void Set_BirthRecordId()
        {
            SetterDeathRecord.BirthRecordId = "242123";
            Assert.Equal("242123", SetterDeathRecord.BirthRecordId);
        }

        [Fact]
        public void Get_BirthRecordId()
        {
            Assert.Equal("242123", ((DeathRecord)JSONRecords[0]).BirthRecordId);
            Assert.Equal("242123", ((DeathRecord)XMLRecords[0]).BirthRecordId);
        }

        [Fact]
        public void Set_BirthRecord_Absent()
        {
            SetterDeathRecord.BirthRecordId = "";
            Assert.Null(SetterDeathRecord.BirthRecordId);
        }

        [Fact]
        public void Get_BirthRecord_Absent()
        {
            DeathRecord dr = new DeathRecord(File.ReadAllText(FixturePath("fixtures/json/DeathRecordBirthRecordDataAbsent.json")));
            Assert.Null(dr.BirthRecordId);
        }

        [Fact]
        public void Get_BirthRecord_Roundtrip()
        {
            DeathRecord dr = ((DeathRecord)JSONRecords[0]);
            Assert.Equal("242123", dr.BirthRecordId);
            IJEMortality ije1 = new IJEMortality(dr);
            Assert.Equal("242123", ije1.BCNO);
            DeathRecord dr2 = ije1.ToDeathRecord();
            Assert.Equal("242123", dr2.BirthRecordId);
        }

        [Fact]
        public void BirthRecord_Absent_Roundtrip()
        {
            DeathRecord dr = new DeathRecord(File.ReadAllText(FixturePath("fixtures/json/DeathRecordBirthRecordDataAbsent.json")));
            IJEMortality ije1 = new IJEMortality(dr);
            Assert.Equal("", ije1.BCNO);
            DeathRecord dr2 = ije1.ToDeathRecord();
            Assert.Null(dr.BirthRecordId);
        }
        [Theory]
        [InlineData("MA")]
        [InlineData("YC")]
        public void Set_BirthRecordState(string state)
        {
            SetterDeathRecord.BirthRecordState = state;
            Assert.Equal(state, SetterDeathRecord.BirthRecordState);
        }

        [Fact]
        public void Get_BirthRecordState()
        {
            Assert.Equal("MA", ((DeathRecord)JSONRecords[0]).BirthRecordState);
            Assert.Equal("MA", ((DeathRecord)XMLRecords[0]).BirthRecordState);
        }

        [Fact]
        public void Set_BirthRecordYear()
        {
            SetterDeathRecord.BirthRecordYear = "1940";
            Assert.Equal("1940", SetterDeathRecord.BirthRecordYear);
        }

        [Fact]
        public void Get_BirthRecordYear()
        {
            Assert.Equal("1940", ((DeathRecord)JSONRecords[0]).BirthRecordYear);
            Assert.Equal("1940", ((DeathRecord)XMLRecords[0]).BirthRecordYear);
        }

        [Fact]
        public void Set_UsualOccupation()
        {
            SetterDeathRecord.UsualOccupation = "secretary";
            Assert.Equal("secretary", SetterDeathRecord.UsualOccupation);
        }

        [Fact]
        public void Get_UsualOccupation()
        {
            Assert.Equal("Executive secretary", ((DeathRecord)JSONRecords[0]).UsualOccupation);
            Assert.Equal("Executive secretary", ((DeathRecord)XMLRecords[0]).UsualOccupation);

        }

        [Fact]
        public void Set_UsualIndustry()
        {
            SetterDeathRecord.UsualIndustry = "State agency";
            Assert.Equal("State agency", SetterDeathRecord.UsualIndustry);
        }

        [Fact]
        public void Get_UsualIndustry()
        {
            Assert.Equal("State department of agriculture", ((DeathRecord)JSONRecords[0]).UsualIndustry);
            Assert.Equal("State department of agriculture", ((DeathRecord)XMLRecords[0]).UsualIndustry);
        }

        [Fact]
        public void Set_MilitaryService()
        {
            SetterDeathRecord.MilitaryServiceHelper = VRDR.ValueSets.YesNoUnknown.Yes;
            Assert.Equal(VRDR.ValueSets.YesNoUnknown.Yes, SetterDeathRecord.MilitaryServiceHelper);
        }

        [Fact]
        public void Get_MilitaryService()
        {
            Assert.Equal(VRDR.ValueSets.YesNoUnknown.Yes, ((DeathRecord)JSONRecords[0]).MilitaryServiceHelper);
            Assert.Equal(VRDR.ValueSets.YesNoUnknown.Yes, ((DeathRecord)XMLRecords[0]).MilitaryServiceHelper);
        }

        // [Fact]
        // public void Set_MorticianGivenNames()
        // {
        //     string[] fdnames = { "FD", "Middle" };
        //     SetterDeathRecord.MorticianGivenNames = fdnames;
        //     Assert.Equal("FD", SetterDeathRecord.MorticianGivenNames[0]);
        //     Assert.Equal("Middle", SetterDeathRecord.MorticianGivenNames[1]);
        // }

        // [Fact]
        // public void Get_MorticianGivenNames()
        // {
        //     Assert.Equal("FD", ((DeathRecord)JSONRecords[0]).MorticianGivenNames[0]);
        //     Assert.Equal("Middle", ((DeathRecord)JSONRecords[0]).MorticianGivenNames[1]);
        //     Assert.Equal("FD", ((DeathRecord)XMLRecords[0]).MorticianGivenNames[0]);
        //     Assert.Equal("Middle", ((DeathRecord)XMLRecords[0]).MorticianGivenNames[1]);
        // }

        // [Fact]
        // public void Set_MorticianFamilyName()
        // {
        //     SetterDeathRecord.MorticianFamilyName = "Last";
        //     Assert.Equal("Last", SetterDeathRecord.MorticianFamilyName);
        // }

        // [Fact]
        // public void Get_MorticianFamilyName()
        // {
        //     Assert.Equal("Last", ((DeathRecord)JSONRecords[0]).MorticianFamilyName);
        //     Assert.Equal("Last", ((DeathRecord)XMLRecords[0]).MorticianFamilyName);
        // }

        // [Fact]
        // public void Set_MorticianSuffix()
        // {
        //     SetterDeathRecord.MorticianSuffix = "Sr.";
        //     Assert.Equal("Sr.", SetterDeathRecord.MorticianSuffix);
        // }

        // [Fact]
        // public void Get_MorticianSuffix()
        // {
        //     Assert.Equal("Jr.", ((DeathRecord)JSONRecords[0]).MorticianSuffix);
        //     Assert.Equal("Jr.", ((DeathRecord)XMLRecords[0]).MorticianSuffix);
        // }

        // [Fact]
        // public void Set_MorticianIdentifier()
        // {
        //     var id = new Dictionary<string, string>();
        //     id["system"] = "foo";
        //     id["value"] = "9876543210";
        //     SetterDeathRecord.MorticianIdentifier = id;
        //     Assert.Equal("foo", SetterDeathRecord.MorticianIdentifier["system"]);
        //     Assert.Equal("9876543210", SetterDeathRecord.MorticianIdentifier["value"]);
        // }

        // [Fact]
        // public void Get_MorticianIdentifier()
        // {
        //     Assert.Equal("9876543210", ((DeathRecord)JSONRecords[0]).MorticianIdentifier["value"]);
        //     Assert.Equal("9876543210", ((DeathRecord)XMLRecords[0]).MorticianIdentifier["value"]);
        // }

        // [Fact]
        // public void Set_PronouncerGivenNames()
        // {
        //     string[] fdnames = { "FD", "Middle" };
        //     SetterDeathRecord.PronouncerGivenNames = fdnames;
        //     Assert.Equal("FD", SetterDeathRecord.PronouncerGivenNames[0]);
        //     Assert.Equal("Middle", SetterDeathRecord.PronouncerGivenNames[1]);
        // }

        // [Fact]
        // public void Get_PronouncerGivenNames()
        // {
        //     Assert.Equal("FD", ((DeathRecord)JSONRecords[0]).PronouncerGivenNames[0]);
        //     Assert.Equal("Middle", ((DeathRecord)JSONRecords[0]).PronouncerGivenNames[1]);
        //     Assert.Equal("FD", ((DeathRecord)XMLRecords[0]).PronouncerGivenNames[0]);
        //     Assert.Equal("Middle", ((DeathRecord)XMLRecords[0]).PronouncerGivenNames[1]);
        // }

        // [Fact]
        // public void Set_PronouncerFamilyName()
        // {
        //     SetterDeathRecord.PronouncerFamilyName = "Last";
        //     Assert.Equal("Last", SetterDeathRecord.PronouncerFamilyName);
        // }

        // [Fact]
        // public void Get_PronouncerFamilyName()
        // {
        //     Assert.Equal("Last", ((DeathRecord)JSONRecords[0]).PronouncerFamilyName);
        //     Assert.Equal("Last", ((DeathRecord)XMLRecords[0]).PronouncerFamilyName);
        // }

        // [Fact]
        // public void Set_PronouncerSuffix()
        // {
        //     SetterDeathRecord.PronouncerSuffix = "Sr.";
        //     Assert.Equal("Sr.", SetterDeathRecord.PronouncerSuffix);
        // }

        // [Fact]
        // public void Get_PronouncerSuffix()
        // {
        //     Assert.Equal("Jr.", ((DeathRecord)JSONRecords[0]).PronouncerSuffix);
        //     Assert.Equal("Jr.", ((DeathRecord)XMLRecords[0]).PronouncerSuffix);
        // }

        // [Fact]
        // public void Set_PronouncerIdentifier()
        // {
        //     var id = new Dictionary<string, string>();
        //     id["system"] = "foo";
        //     id["value"] = "0000000000";
        //     SetterDeathRecord.PronouncerIdentifier = id;
        //     Assert.Equal("foo", SetterDeathRecord.PronouncerIdentifier["system"]);
        //     Assert.Equal("0000000000", SetterDeathRecord.PronouncerIdentifier["value"]);
        // }

        // [Fact]
        // public void Get_PronouncerIdentifier()
        // {
        //     Assert.Equal("0000000000", ((DeathRecord)JSONRecords[0]).PronouncerIdentifier["value"]);
        //     Assert.Equal("0000000000", ((DeathRecord)XMLRecords[0]).PronouncerIdentifier["value"]);
        // }

        [Fact]
        public void Set_FuneralHomeAddress()
        {
            Dictionary<string, string> fdaddress = new Dictionary<string, string>();
            fdaddress.Add("addressLine1", "1011010 Example Street");
            fdaddress.Add("addressLine2", "Line 2");
            fdaddress.Add("addressCity", "Bedford");
            fdaddress.Add("addressCounty", "Middlesex");
            fdaddress.Add("addressState", "MA");
            fdaddress.Add("addressZip", "01730");
            fdaddress.Add("addressCountry", "US");
            fdaddress.Add("addressPredir", "W");
            fdaddress.Add("addressPostdir", "E");
            fdaddress.Add("addressStname", "Example");
            fdaddress.Add("addressStnum", "11");
            fdaddress.Add("addressStdesig", "Street");
            fdaddress.Add("addressUnitnum", "3");
            SetterDeathRecord.FuneralHomeAddress = fdaddress;
            Assert.Equal("1011010 Example Street", SetterDeathRecord.FuneralHomeAddress["addressLine1"]);
            Assert.Equal("Line 2", SetterDeathRecord.FuneralHomeAddress["addressLine2"]);
            Assert.Equal("Bedford", SetterDeathRecord.FuneralHomeAddress["addressCity"]);
            Assert.Equal("Middlesex", SetterDeathRecord.FuneralHomeAddress["addressCounty"]);
            Assert.Equal("MA", SetterDeathRecord.FuneralHomeAddress["addressState"]);
            Assert.Equal("01730", SetterDeathRecord.FuneralHomeAddress["addressZip"]);
            Assert.Equal("US", SetterDeathRecord.FuneralHomeAddress["addressCountry"]);
            Assert.Equal("W", SetterDeathRecord.FuneralHomeAddress["addressPredir"]);
            Assert.Equal("E", SetterDeathRecord.FuneralHomeAddress["addressPostdir"]);
            Assert.Equal("Example", SetterDeathRecord.FuneralHomeAddress["addressStname"]);
            Assert.Equal("11", SetterDeathRecord.FuneralHomeAddress["addressStnum"]);
            Assert.Equal("Street", SetterDeathRecord.FuneralHomeAddress["addressStdesig"]);
            Assert.Equal("3", SetterDeathRecord.FuneralHomeAddress["addressUnitnum"]);
        }

        [Fact]
        public void Get_FuneralHomeAddress()
        {
            Assert.Equal("1011010 Example Street", ((DeathRecord)XMLRecords[0]).FuneralHomeAddress["addressLine1"]);
            Assert.Equal("1011010 Example Street", ((DeathRecord)JSONRecords[0]).FuneralHomeAddress["addressLine1"]);
            Assert.Equal("Line 2", ((DeathRecord)JSONRecords[0]).FuneralHomeAddress["addressLine2"]);
            Assert.Equal("Bedford", ((DeathRecord)JSONRecords[0]).FuneralHomeAddress["addressCity"]);
            Assert.Equal("Middlesex", ((DeathRecord)JSONRecords[0]).FuneralHomeAddress["addressCounty"]);
            Assert.Equal("MA", ((DeathRecord)JSONRecords[0]).FuneralHomeAddress["addressState"]);
            Assert.Equal("01730", ((DeathRecord)JSONRecords[0]).FuneralHomeAddress["addressZip"]);
            Assert.Equal("US", ((DeathRecord)JSONRecords[0]).FuneralHomeAddress["addressCountry"]);

            Assert.Equal("Line 2", ((DeathRecord)XMLRecords[0]).FuneralHomeAddress["addressLine2"]);
            Assert.Equal("Bedford", ((DeathRecord)XMLRecords[0]).FuneralHomeAddress["addressCity"]);
            Assert.Equal("Middlesex", ((DeathRecord)XMLRecords[0]).FuneralHomeAddress["addressCounty"]);
            Assert.Equal("MA", ((DeathRecord)XMLRecords[0]).FuneralHomeAddress["addressState"]);
            Assert.Equal("01730", ((DeathRecord)XMLRecords[0]).FuneralHomeAddress["addressZip"]);
            Assert.Equal("US", ((DeathRecord)XMLRecords[0]).FuneralHomeAddress["addressCountry"]);
        }

        [Fact]
        public void Set_FuneralHomeName()
        {
            SetterDeathRecord.FuneralHomeName = "Smith Funeral Home";
            Assert.Equal("Smith Funeral Home", SetterDeathRecord.FuneralHomeName);
        }

        [Fact]
        public void Get_FuneralHomeName()
        {
            Assert.Equal("Smith Funeral Home", ((DeathRecord)JSONRecords[0]).FuneralHomeName);
            Assert.Equal("Smith Funeral Home", ((DeathRecord)XMLRecords[0]).FuneralHomeName);
        }

        [Fact]
        public void Set_DispositionLocationAddress()
        {
            Dictionary<string, string> dladdress = new Dictionary<string, string>();
            dladdress.Add("addressLine1", "999 Example Street");
            dladdress.Add("addressLine2", "Line 2");
            dladdress.Add("addressCity", "Bedford");
            dladdress.Add("addressCounty", "Middlesex");
            dladdress.Add("addressState", "MA");
            dladdress.Add("addressZip", "01730");
            dladdress.Add("addressCountry", "US");
            SetterDeathRecord.DispositionLocationAddress = dladdress;
            Assert.Equal("999 Example Street", SetterDeathRecord.DispositionLocationAddress["addressLine1"]);
            Assert.Equal("Line 2", SetterDeathRecord.DispositionLocationAddress["addressLine2"]);
            Assert.Equal("Bedford", SetterDeathRecord.DispositionLocationAddress["addressCity"]);
            Assert.Equal("Middlesex", SetterDeathRecord.DispositionLocationAddress["addressCounty"]);
            Assert.Equal("MA", SetterDeathRecord.DispositionLocationAddress["addressState"]);
            Assert.Equal("01730", SetterDeathRecord.DispositionLocationAddress["addressZip"]);
            Assert.Equal("US", SetterDeathRecord.DispositionLocationAddress["addressCountry"]);
        }

        [Fact]
        public void Get_DispositionLocationAddress()
        {
            Assert.Equal("603 Example Street", ((DeathRecord)JSONRecords[0]).DispositionLocationAddress["addressLine1"]);
            Assert.Equal("Line 2", ((DeathRecord)JSONRecords[0]).DispositionLocationAddress["addressLine2"]);
            Assert.Equal("Bedford", ((DeathRecord)JSONRecords[0]).DispositionLocationAddress["addressCity"]);
            Assert.Equal("Middlesex", ((DeathRecord)JSONRecords[0]).DispositionLocationAddress["addressCounty"]);
            Assert.Equal("MA", ((DeathRecord)JSONRecords[0]).DispositionLocationAddress["addressState"]);
            Assert.Equal("01730", ((DeathRecord)JSONRecords[0]).DispositionLocationAddress["addressZip"]);
            Assert.Equal("US", ((DeathRecord)JSONRecords[0]).DispositionLocationAddress["addressCountry"]);
            Assert.Equal("603 Example Street", ((DeathRecord)XMLRecords[0]).DispositionLocationAddress["addressLine1"]);
            Assert.Equal("Line 2", ((DeathRecord)XMLRecords[0]).DispositionLocationAddress["addressLine2"]);
            Assert.Equal("Bedford", ((DeathRecord)XMLRecords[0]).DispositionLocationAddress["addressCity"]);
            Assert.Equal("Middlesex", ((DeathRecord)XMLRecords[0]).DispositionLocationAddress["addressCounty"]);
            Assert.Equal("MA", ((DeathRecord)XMLRecords[0]).DispositionLocationAddress["addressState"]);
            Assert.Equal("01730", ((DeathRecord)XMLRecords[0]).DispositionLocationAddress["addressZip"]);
            Assert.Equal("US", ((DeathRecord)XMLRecords[0]).DispositionLocationAddress["addressCountry"]);
        }

        [Fact]
        public void Set_DispositionLocationName()
        {
            SetterDeathRecord.DispositionLocationName = "Bedford Cemetery";
            Assert.Equal("Bedford Cemetery", SetterDeathRecord.DispositionLocationName);
        }

        [Fact]
        public void Get_DispositionLocationName()
        {
            Assert.Equal("Bedford Cemetery", ((DeathRecord)JSONRecords[0]).DispositionLocationName);
            Assert.Equal("Bedford Cemetery", ((DeathRecord)XMLRecords[0]).DispositionLocationName);
        }

        [Fact]
        public void Set_DecedentDispositionMethod()
        {
            Dictionary<string, string> ddm = new Dictionary<string, string>();
            SetterDeathRecord.DecedentDispositionMethodHelper = VRDR.ValueSets.MethodOfDisposition.Burial;
            Assert.Equal(VRDR.ValueSets.MethodOfDisposition.Burial, SetterDeathRecord.DecedentDispositionMethodHelper);
            Assert.Equal(VRDR.CodeSystems.SCT, SetterDeathRecord.DecedentDispositionMethod["system"]);
            Assert.Equal("Burial", SetterDeathRecord.DecedentDispositionMethod["display"]);
        }

        [Fact]
        public void Get_DecedentDispositionMethod()
        {
            Assert.Equal("449971000124106", ((DeathRecord)JSONRecords[0]).DecedentDispositionMethod["code"]);
            Assert.Equal(CodeSystems.SCT, ((DeathRecord)JSONRecords[0]).DecedentDispositionMethod["system"]);
            Assert.Equal("Burial", ((DeathRecord)JSONRecords[0]).DecedentDispositionMethod["display"]);
            Assert.Equal("449971000124106", ((DeathRecord)XMLRecords[0]).DecedentDispositionMethod["code"]);
            Assert.Equal(CodeSystems.SCT, ((DeathRecord)XMLRecords[0]).DecedentDispositionMethod["system"]);
            Assert.Equal("Burial", ((DeathRecord)XMLRecords[0]).DecedentDispositionMethod["display"]);
        }

        [Fact]
        public void Set_AutopsyPerformedIndicator()
        {
            Dictionary<string, string> api = new Dictionary<string, string>();
            api.Add("code", "Y");
            api.Add("system", VRDR.CodeSystems.YesNo);
            api.Add("display", "Yes");
            SetterDeathRecord.AutopsyPerformedIndicator = api;
            Assert.Equal("Y", SetterDeathRecord.AutopsyPerformedIndicator["code"]);
            Assert.Equal(VRDR.CodeSystems.YesNo, SetterDeathRecord.AutopsyPerformedIndicator["system"]);
            Assert.Equal("Yes", SetterDeathRecord.AutopsyPerformedIndicator["display"]);
            Assert.Equal("Y", SetterDeathRecord.AutopsyPerformedIndicatorHelper);
            SetterDeathRecord.AutopsyPerformedIndicatorHelper = "N";
            Assert.Equal("N", SetterDeathRecord.AutopsyPerformedIndicator["code"]);
            Assert.Equal(VRDR.CodeSystems.YesNo, SetterDeathRecord.AutopsyPerformedIndicator["system"]);
            Assert.Equal("No", SetterDeathRecord.AutopsyPerformedIndicator["display"]);
            Assert.Equal("N", SetterDeathRecord.AutopsyPerformedIndicatorHelper);
            SetterDeathRecord.AutopsyPerformedIndicatorHelper = "UNK";
            Assert.Equal("UNK", SetterDeathRecord.AutopsyPerformedIndicator["code"]);
            Assert.Equal(VRDR.CodeSystems.NullFlavor_HL7_V3, SetterDeathRecord.AutopsyPerformedIndicator["system"]);
            Assert.Equal("unknown", SetterDeathRecord.AutopsyPerformedIndicator["display"]);
            Assert.Equal("UNK", SetterDeathRecord.AutopsyPerformedIndicatorHelper);
        }

        [Fact]
        public void Get_AutopsyPerformedIndicator()
        {
            Assert.Equal("Y", ((DeathRecord)JSONRecords[0]).AutopsyPerformedIndicator["code"]);
            Assert.Equal(VRDR.CodeSystems.YesNo, ((DeathRecord)JSONRecords[0]).AutopsyPerformedIndicator["system"]);
            Assert.Equal("Yes", ((DeathRecord)JSONRecords[0]).AutopsyPerformedIndicator["display"]);
            Assert.Equal("Y", ((DeathRecord)JSONRecords[0]).AutopsyPerformedIndicatorHelper);
            Assert.Equal("Y", ((DeathRecord)XMLRecords[0]).AutopsyPerformedIndicator["code"]);
            Assert.Equal(VRDR.CodeSystems.YesNo, ((DeathRecord)XMLRecords[0]).AutopsyPerformedIndicator["system"]);
            Assert.Equal("Yes", ((DeathRecord)XMLRecords[0]).AutopsyPerformedIndicator["display"]);
            Assert.Equal("Y", ((DeathRecord)XMLRecords[0]).AutopsyPerformedIndicatorHelper);
        }

        [Fact]
        public void Set_AutopsyResultsAvailable()
        {
            SetterDeathRecord.AutopsyResultsAvailableHelper = VRDR.ValueSets.YesNoUnknown.Yes;
            Assert.Equal("Y", SetterDeathRecord.AutopsyResultsAvailableHelper);
            SetterDeathRecord.AutopsyResultsAvailableHelper = "N";
            Assert.Equal("N", SetterDeathRecord.AutopsyResultsAvailableHelper);
            SetterDeathRecord.AutopsyResultsAvailableHelper = "NA";
            Assert.Equal("NA", SetterDeathRecord.AutopsyResultsAvailable["code"]);
            Assert.Equal(VRDR.CodeSystems.NullFlavor_HL7_V3, SetterDeathRecord.AutopsyResultsAvailable["system"]);
            Assert.Equal("not applicable", SetterDeathRecord.AutopsyResultsAvailable["display"]);
            Assert.Equal("NA", SetterDeathRecord.AutopsyResultsAvailableHelper);
        }

        [Fact]
        public void Get_AutopsyResultsAvailable()
        {
            Assert.Equal("Y", ((DeathRecord)JSONRecords[0]).AutopsyResultsAvailable["code"]);
            Assert.Equal(VRDR.CodeSystems.YesNo, ((DeathRecord)JSONRecords[0]).AutopsyResultsAvailable["system"]);
            Assert.Equal("Yes", ((DeathRecord)JSONRecords[0]).AutopsyResultsAvailable["display"]);
            Assert.Equal("Y", ((DeathRecord)JSONRecords[0]).AutopsyResultsAvailableHelper);
            Assert.Equal("Y", ((DeathRecord)XMLRecords[0]).AutopsyResultsAvailable["code"]);
            Assert.Equal(VRDR.CodeSystems.YesNo, ((DeathRecord)XMLRecords[0]).AutopsyResultsAvailable["system"]);
            Assert.Equal("Yes", ((DeathRecord)XMLRecords[0]).AutopsyResultsAvailable["display"]);
            Assert.Equal("Y", ((DeathRecord)XMLRecords[0]).AutopsyResultsAvailableHelper);
        }

        [Fact]
        public void Set_AgeAtDeath()
        {
            Dictionary<string, string> aad = new Dictionary<string, string>();
            aad.Add("type", "a");
            aad.Add("units", "79");
            SetterDeathRecord.AgeAtDeath = aad;
            Assert.Equal("a", SetterDeathRecord.AgeAtDeath["type"]);
            Assert.Equal("79", SetterDeathRecord.AgeAtDeath["units"]);
        }

        [Fact]
        public void Get_AgeAtDeath()
        {
            Assert.Equal("a", ((DeathRecord)JSONRecords[0]).AgeAtDeath["type"]);
            Assert.Equal("79", ((DeathRecord)JSONRecords[0]).AgeAtDeath["units"]);
            Assert.False(((DeathRecord)JSONRecords[0]).AgeAtDeathDataAbsentBoolean);
            Assert.Equal("a", ((DeathRecord)XMLRecords[0]).AgeAtDeath["type"]);
            Assert.Equal("79", ((DeathRecord)XMLRecords[0]).AgeAtDeath["units"]);
            Assert.False(((DeathRecord)XMLRecords[0]).AgeAtDeathDataAbsentBoolean);
        }

        [Fact]
        public void Set_AgeAtDeath_Data_Absent()
        {
            Dictionary<string, string> aad1 = new Dictionary<string, string>();
            aad1.Add("type", "");
            aad1.Add("units", "");
            SetterDeathRecord.AgeAtDeath = aad1;
            Assert.Equal("", SetterDeathRecord.AgeAtDeath["type"]);
            Assert.Equal("", SetterDeathRecord.AgeAtDeath["units"]);

            Dictionary<string, string> aad2 = new Dictionary<string, string>();
            SetterDeathRecord.AgeAtDeathDataAbsentBoolean = true;
            SetterDeathRecord.AgeAtDeath = aad2;
            Assert.Equal("", SetterDeathRecord.AgeAtDeath["type"]);
            Assert.Equal("", SetterDeathRecord.AgeAtDeath["units"]);
            Assert.True(SetterDeathRecord.AgeAtDeathDataAbsentBoolean);
        }

        [Fact]
        public void Get_AgeAtDeath_Data_Absent()
        {
            DeathRecord json = new DeathRecord(File.ReadAllText(FixturePath("fixtures/json/MissingAge.json")));
            Assert.Equal("", json.AgeAtDeath["type"]);
            Assert.Equal("", json.AgeAtDeath["units"]);
        }

        [Fact]
        public void Set_AgeAtDeath_EditBypassFlag()
        {
            SetterDeathRecord.AgeAtDeathEditFlagHelper = ValueSets.EditBypass01.Edit_Passed;
            Assert.Equal(ValueSets.EditBypass01.Edit_Passed, SetterDeathRecord.AgeAtDeathEditFlagHelper);

            SetterDeathRecord.AgeAtDeathEditFlagHelper = ValueSets.EditBypass01.Edit_Failed_Data_Queried_And_Verified;
            Assert.Equal(ValueSets.EditBypass01.Edit_Failed_Data_Queried_And_Verified, SetterDeathRecord.AgeAtDeathEditFlagHelper);
        }

        [Fact]
        public void AgeAtDeath_RoundTrip()
        {
            DeathRecord dr = new DeathRecord(File.ReadAllText(FixturePath("fixtures/json/MissingAge.json")));
            IJEMortality ije = new IJEMortality(dr);
            Assert.Equal("999", ije.AGE);
            Assert.Equal("9", ije.AGETYPE);
            DeathRecord dr2 = ije.ToDeathRecord();
            Assert.Equal("", dr2.AgeAtDeath["type"]);
            Assert.Equal("", dr2.AgeAtDeath["units"]);
            Assert.True(dr2.AgeAtDeathDataAbsentBoolean);
        }

        [Fact]
        public void AgeAtDeath_EditFlag()
        {
            Dictionary<string, string> flag = new Dictionary<string, string>();
            flag.Add("system", CodeSystems.BypassEditFlag);
            flag.Add("code", "0");
            flag.Add("display", "Edit Passed");
            SetterDeathRecord.AgeAtDeathEditFlag = flag;
            Assert.Equal(CodeSystems.BypassEditFlag, SetterDeathRecord.AgeAtDeathEditFlag["system"]);
            Assert.Equal("0", SetterDeathRecord.AgeAtDeathEditFlag["code"]);
            Assert.Equal("Edit Passed", SetterDeathRecord.AgeAtDeathEditFlag["display"]);

            flag = new Dictionary<string, string>();
            flag.Add("system", CodeSystems.BypassEditFlag);
            flag.Add("code", "1");
            flag.Add("display", "Edit Failed, Data Queried, and Verified");
            SetterDeathRecord.AgeAtDeathEditFlag = flag;
            Assert.Equal(CodeSystems.BypassEditFlag, SetterDeathRecord.AgeAtDeathEditFlag["system"]);
            Assert.Equal("1", SetterDeathRecord.AgeAtDeathEditFlag["code"]);
            Assert.Equal("Edit Failed, Data Queried, and Verified", SetterDeathRecord.AgeAtDeathEditFlag["display"]);
        }

        [Fact]
        public void AgeAtDeath_EditFlag_RoundTrip()
        {
            DeathRecord dr = new DeathRecord(File.ReadAllText(FixturePath("fixtures/json/MissingAge.json")));
            Dictionary<string, string> flag = new Dictionary<string, string>();
            flag.Add("system", CodeSystems.BypassEditFlag);
            flag.Add("code", "0");
            flag.Add("display", "Edit Passed");
            dr.AgeAtDeathEditFlag = flag;

            IJEMortality ije = new IJEMortality(dr);
            Assert.Equal("999", ije.AGE);
            Assert.Equal("9", ije.AGETYPE);
            Assert.Equal("0", ije.AGE_BYPASS);
            DeathRecord dr2 = ije.ToDeathRecord();
            Assert.Equal(CodeSystems.BypassEditFlag, dr2.AgeAtDeathEditFlag["system"]);
            Assert.Equal("0", dr2.AgeAtDeathEditFlag["code"]);
            Assert.Equal("Edit Passed", dr2.AgeAtDeathEditFlag["display"]);
        }

        [Fact]
        public void Set_PregnancyStatus()
        {
            Dictionary<string, string> ps = new Dictionary<string, string>();
            ps.Add("code", "1");
            ps.Add("system", VRDR.CodeSystems.PregnancyStatus);
            ps.Add("display", "Not pregnant within past year");
            SetterDeathRecord.PregnancyStatus = ps;
            Assert.Equal("1", SetterDeathRecord.PregnancyStatus["code"]);
            Assert.Equal(VRDR.CodeSystems.PregnancyStatus, SetterDeathRecord.PregnancyStatus["system"]);
            Assert.Equal("Not pregnant within past year", SetterDeathRecord.PregnancyStatus["display"]);
        }

        [Fact]
        public void Get_PregnancyStatus()
        {
            Assert.Equal("1", ((DeathRecord)JSONRecords[0]).PregnancyStatus["code"]);
            Assert.Equal(VRDR.CodeSystems.PregnancyStatus, ((DeathRecord)JSONRecords[0]).PregnancyStatus["system"]);
            Assert.Equal("Not pregnant within past year", ((DeathRecord)JSONRecords[0]).PregnancyStatus["display"]);
            Assert.Equal("1", ((DeathRecord)XMLRecords[0]).PregnancyStatus["code"]);
            Assert.Equal(VRDR.CodeSystems.PregnancyStatus, ((DeathRecord)XMLRecords[0]).PregnancyStatus["system"]);
            Assert.Equal("Not pregnant within past year", ((DeathRecord)XMLRecords[0]).PregnancyStatus["display"]);
        }

        [Fact]
        public void Set_PregnancyStatusEditFlag()
        {
            Dictionary<string, string> elef = new Dictionary<string, string>();
            elef.Add("code", VRDR.ValueSets.EditBypass012.Edit_Failed_Data_Queried_And_Verified);
            elef.Add("system", VRDR.CodeSystems.BypassEditFlag);
            elef.Add("display", "Edit Failed Data Queried And Verified");
            SetterDeathRecord.PregnancyStatusEditFlag = elef;
            Assert.Equal(VRDR.ValueSets.EditBypass012.Edit_Failed_Data_Queried_And_Verified, SetterDeathRecord.PregnancyStatusEditFlag["code"]);
            Assert.Equal(VRDR.CodeSystems.BypassEditFlag, SetterDeathRecord.PregnancyStatusEditFlag["system"]);
            Assert.Equal("Edit Failed Data Queried And Verified", SetterDeathRecord.PregnancyStatusEditFlag["display"]);
            SetterDeathRecord.PregnancyStatusEditFlagHelper = VRDR.ValueSets.EditBypass012.Edit_Passed;
            Assert.Equal(VRDR.ValueSets.EditBypass012.Edit_Passed, SetterDeathRecord.PregnancyStatusEditFlagHelper);
            Assert.Equal(VRDR.CodeSystems.BypassEditFlag, SetterDeathRecord.PregnancyStatusEditFlag["system"]);
            Assert.Equal("Edit Passed", SetterDeathRecord.PregnancyStatusEditFlag["display"]);
        }

        [Fact]
        public void Get_PregnancyStatusEditFlag()
        {
            Assert.Equal(VRDR.ValueSets.EditBypass012.Edit_Passed, ((DeathRecord)JSONRecords[0]).PregnancyStatusEditFlagHelper);
            Assert.Equal(VRDR.CodeSystems.BypassEditFlag, ((DeathRecord)JSONRecords[0]).PregnancyStatusEditFlag["system"]);
            Assert.Equal("Edit Passed", ((DeathRecord)JSONRecords[0]).PregnancyStatusEditFlag["display"]);
            Assert.Equal(VRDR.ValueSets.EditBypass012.Edit_Passed, ((DeathRecord)XMLRecords[0]).PregnancyStatusEditFlagHelper);
            Assert.Equal(VRDR.CodeSystems.BypassEditFlag, ((DeathRecord)XMLRecords[0]).PregnancyStatusEditFlag["system"]);
            Assert.Equal("Edit Passed", ((DeathRecord)XMLRecords[0]).PregnancyStatusEditFlag["display"]);
        }

        [Fact]
        public void Set_InjuryPlace()
        {
            Dictionary<string, string> code = new Dictionary<string, string>();

            // no description
            SetterDeathRecord.InjuryPlaceDescription = "At home, in the kitchen";
            Assert.Equal("At home, in the kitchen", SetterDeathRecord.InjuryPlaceDescription);
        }

        [Fact]
        public void Get_InjuryPlace()
        {
            Assert.Equal("At home, in the kitchen", ((DeathRecord)JSONRecords[0]).InjuryPlaceDescription);
            Assert.Equal("At home, in the kitchen", ((DeathRecord)XMLRecords[0]).InjuryPlaceDescription);
        }

        [Fact]
        public void Set_TransportationRole()
        {
            SetterDeathRecord.TransportationRoleHelper = ValueSets.TransportationIncidentRole.Passenger;
            Assert.Equal(ValueSets.TransportationIncidentRole.Passenger, SetterDeathRecord.TransportationRole["code"]);
            Assert.Equal(CodeSystems.SCT, SetterDeathRecord.TransportationRole["system"]);
            Assert.Equal("Passenger", SetterDeathRecord.TransportationRole["display"]);
            SetterDeathRecord.DeathLocationJurisdiction = "MA";
            IJEMortality ije1 = new IJEMortality(SetterDeathRecord);
            Assert.Equal("PA", ije1.TRANSPRT);
            ije1.TRANSPRT = "PAP";
            Assert.Equal("PAP", ije1.TRANSPRT);
            DeathRecord d = ije1.ToDeathRecord();
            IJEMortality ije2 = new IJEMortality(d);
            Assert.Equal("PAP", ije2.TRANSPRT);
            SetterDeathRecord.TransportationRoleHelper = "Hover Board Rider";
            Assert.Equal("Hover Board Rider", SetterDeathRecord.TransportationRoleHelper);
            Assert.Equal("Hover Board Rider", SetterDeathRecord.TransportationRole["text"]);
            Assert.Equal("OTH", SetterDeathRecord.TransportationRole["code"]);
            Assert.Equal(CodeSystems.NullFlavor_HL7_V3, SetterDeathRecord.TransportationRole["system"]);
            Assert.Equal("Other", SetterDeathRecord.TransportationRole["display"]);



        }

        [Fact]
        public void Get_TransportationRole()
        {
            Assert.Equal(ValueSets.TransportationIncidentRole.Passenger, ((DeathRecord)JSONRecords[0]).TransportationRole["code"]);
            Assert.Equal(CodeSystems.SCT, ((DeathRecord)JSONRecords[0]).TransportationRole["system"]);
            Assert.Equal("Passenger", ((DeathRecord)JSONRecords[0]).TransportationRole["display"]);
            Assert.Equal(ValueSets.TransportationIncidentRole.Passenger, ((DeathRecord)XMLRecords[0]).TransportationRole["code"]);
            Assert.Equal(CodeSystems.SCT, ((DeathRecord)XMLRecords[0]).TransportationRole["system"]);
            Assert.Equal("Passenger", ((DeathRecord)XMLRecords[0]).TransportationRole["display"]);
        }

        [Fact]
        public void Set_ExaminerContacted()
        {
            Dictionary<string, string> ec = new Dictionary<string, string>();
            ec.Add("code", "Y");
            ec.Add("system", VRDR.CodeSystems.YesNo);
            ec.Add("display", "Yes");
            SetterDeathRecord.ExaminerContacted = ec;
            Assert.Equal("Y", SetterDeathRecord.ExaminerContacted["code"]);
            Assert.Equal(VRDR.CodeSystems.YesNo, SetterDeathRecord.ExaminerContacted["system"]);
            Assert.Equal("Yes", SetterDeathRecord.ExaminerContacted["display"]);
            Assert.Equal("Y", SetterDeathRecord.ExaminerContactedHelper);
            SetterDeathRecord.ExaminerContactedHelper = "N";
            Assert.Equal("N", SetterDeathRecord.ExaminerContacted["code"]);
            Assert.Equal(VRDR.CodeSystems.YesNo, SetterDeathRecord.ExaminerContacted["system"]);
            Assert.Equal("No", SetterDeathRecord.ExaminerContacted["display"]);
            Assert.Equal("N", SetterDeathRecord.ExaminerContactedHelper);
            SetterDeathRecord.ExaminerContactedHelper = "UNK";
            Assert.Equal("UNK", SetterDeathRecord.ExaminerContacted["code"]);
            Assert.Equal(VRDR.CodeSystems.NullFlavor_HL7_V3, SetterDeathRecord.ExaminerContacted["system"]);
            Assert.Equal("unknown", SetterDeathRecord.ExaminerContacted["display"]);
            Assert.Equal("UNK", SetterDeathRecord.ExaminerContactedHelper);
        }

        [Fact]
        public void Get_ExaminerContacted()
        {
            Assert.Equal("N", ((DeathRecord)JSONRecords[0]).ExaminerContacted["code"]);
            Assert.Equal(VRDR.CodeSystems.YesNo_0136HL7_V2, ((DeathRecord)JSONRecords[0]).ExaminerContacted["system"]);
            Assert.Equal("No", ((DeathRecord)JSONRecords[0]).ExaminerContacted["display"]);
            Assert.Equal("N", ((DeathRecord)JSONRecords[0]).ExaminerContactedHelper);
            Assert.Equal("N", ((DeathRecord)XMLRecords[0]).ExaminerContacted["code"]);
            Assert.Equal(VRDR.CodeSystems.YesNo_0136HL7_V2, ((DeathRecord)XMLRecords[0]).ExaminerContacted["system"]);
            Assert.Equal("No", ((DeathRecord)XMLRecords[0]).ExaminerContacted["display"]);
            Assert.Equal("N", ((DeathRecord)XMLRecords[0]).ExaminerContactedHelper);
        }

        [Fact]
        public void Set_TobaccoUse()
        {
            Dictionary<string, string> tbu = new Dictionary<string, string>();
            tbu.Add("code", "373066001");
            tbu.Add("system", CodeSystems.SCT);
            tbu.Add("display", "Yes");
            SetterDeathRecord.TobaccoUse = tbu;
            Assert.Equal("373066001", SetterDeathRecord.TobaccoUse["code"]);
            Assert.Equal(CodeSystems.SCT, SetterDeathRecord.TobaccoUse["system"]);
            Assert.Equal("Yes", SetterDeathRecord.TobaccoUse["display"]);
        }

        [Fact]
        public void Get_TobaccoUse()
        {
            Assert.Equal("373066001", ((DeathRecord)JSONRecords[0]).TobaccoUse["code"]);
            Assert.Equal(CodeSystems.SCT, ((DeathRecord)JSONRecords[0]).TobaccoUse["system"]);
            Assert.Equal("Yes", ((DeathRecord)JSONRecords[0]).TobaccoUse["display"]);
            Assert.Equal("373066001", ((DeathRecord)XMLRecords[0]).TobaccoUse["code"]);
            Assert.Equal(CodeSystems.SCT, ((DeathRecord)XMLRecords[0]).TobaccoUse["system"]);
            Assert.Equal("Yes", ((DeathRecord)XMLRecords[0]).TobaccoUse["display"]);
        }

        [Fact]
        public void Set_InjuryLocationAddress()
        {
            Dictionary<string, string> iladdress = new Dictionary<string, string>();
            iladdress.Add("addressLine1", "99912 Example Street");
            iladdress.Add("addressLine2", "Line 2");
            iladdress.Add("addressCity", "Bedford");
            iladdress.Add("addressCounty", "Middlesex");
            iladdress.Add("addressCityC", "12345");
            iladdress.Add("addressCountyC", "123");
            iladdress.Add("addressState", "MA");
            iladdress.Add("addressZip", "01730");
            iladdress.Add("addressCountry", "US");
            iladdress.Add("addressPredir", "W");
            iladdress.Add("addressPostdir", "E");
            iladdress.Add("addressStname", "Example");
            iladdress.Add("addressStnum", "11");
            iladdress.Add("addressStdesig", "Street");
            iladdress.Add("addressUnitnum", "3");
            SetterDeathRecord.InjuryLocationAddress = iladdress;
            Assert.Equal("99912 Example Street", SetterDeathRecord.InjuryLocationAddress["addressLine1"]);
            Assert.Equal("Line 2", SetterDeathRecord.InjuryLocationAddress["addressLine2"]);
            Assert.Equal("Bedford", SetterDeathRecord.InjuryLocationAddress["addressCity"]);
            Assert.Equal("Middlesex", SetterDeathRecord.InjuryLocationAddress["addressCounty"]);
            Assert.Equal("12345", SetterDeathRecord.InjuryLocationAddress["addressCityC"]);
            Assert.Equal("123", SetterDeathRecord.InjuryLocationAddress["addressCountyC"]);
            Assert.Equal("MA", SetterDeathRecord.InjuryLocationAddress["addressState"]);
            Assert.Equal("01730", SetterDeathRecord.InjuryLocationAddress["addressZip"]);
            Assert.Equal("US", SetterDeathRecord.InjuryLocationAddress["addressCountry"]);
            Assert.Equal("W", SetterDeathRecord.InjuryLocationAddress["addressPredir"]);
            Assert.Equal("E", SetterDeathRecord.InjuryLocationAddress["addressPostdir"]);
            Assert.Equal("Example", SetterDeathRecord.InjuryLocationAddress["addressStname"]);
            Assert.Equal("11", SetterDeathRecord.InjuryLocationAddress["addressStnum"]);
            Assert.Equal("Street", SetterDeathRecord.InjuryLocationAddress["addressStdesig"]);
            Assert.Equal("3", SetterDeathRecord.InjuryLocationAddress["addressUnitnum"]);

        }

        [Fact]
        public void Get_InjuryLocationAddress()
        {
            Assert.Equal("781 Example Street", ((DeathRecord)JSONRecords[0]).InjuryLocationAddress["addressLine1"]);
            Assert.Equal("Line 2", ((DeathRecord)JSONRecords[0]).InjuryLocationAddress["addressLine2"]);
            Assert.Equal("Bedford", ((DeathRecord)JSONRecords[0]).InjuryLocationAddress["addressCity"]);
            Assert.Equal("Middlesex", ((DeathRecord)JSONRecords[0]).InjuryLocationAddress["addressCounty"]);
            Assert.Equal("MA", ((DeathRecord)JSONRecords[0]).InjuryLocationAddress["addressState"]);
            Assert.Equal("01730", ((DeathRecord)JSONRecords[0]).InjuryLocationAddress["addressZip"]);
            Assert.Equal("US", ((DeathRecord)JSONRecords[0]).InjuryLocationAddress["addressCountry"]);
            Assert.Equal("781 Example Street", ((DeathRecord)XMLRecords[0]).InjuryLocationAddress["addressLine1"]);
            Assert.Equal("Line 2", ((DeathRecord)XMLRecords[0]).InjuryLocationAddress["addressLine2"]);
            Assert.Equal("Bedford", ((DeathRecord)XMLRecords[0]).InjuryLocationAddress["addressCity"]);
            Assert.Equal("Middlesex", ((DeathRecord)XMLRecords[0]).InjuryLocationAddress["addressCounty"]);
            Assert.Equal("MA", ((DeathRecord)XMLRecords[0]).InjuryLocationAddress["addressState"]);
            Assert.Equal("01730", ((DeathRecord)XMLRecords[0]).InjuryLocationAddress["addressZip"]);
            Assert.Equal("US", ((DeathRecord)XMLRecords[0]).InjuryLocationAddress["addressCountry"]);
        }

        [Fact]
        public void Set_InjuryLocationName()
        {
            SetterDeathRecord.InjuryLocationName = "Example Injury Location Name";
            Assert.Equal("Example Injury Location Name", SetterDeathRecord.InjuryLocationName);
        }

        [Fact]
        public void Set_InjuryLocationLatLong()
        {
            SetterDeathRecord.InjuryLocationLatitude = "38.889248";
            SetterDeathRecord.InjuryLocationLongitude = "-77.050636";
            Assert.Equal("-77.050636", SetterDeathRecord.InjuryLocationLongitude);
            Assert.Equal("38.889248", SetterDeathRecord.InjuryLocationLatitude);
        }
        [Fact]
        public void Get_InjuryLocationLatLong()
        {
            Assert.Equal("-77.050636", ((DeathRecord)JSONRecords[0]).InjuryLocationLongitude);
            Assert.Equal("38.889248", ((DeathRecord)JSONRecords[0]).InjuryLocationLatitude);
            Assert.Equal("-77.050636", ((DeathRecord)XMLRecords[0]).InjuryLocationLongitude);
            Assert.Equal("38.889248", ((DeathRecord)XMLRecords[0]).InjuryLocationLatitude);
        }

        [Fact]
        public void Get_InjuryLocationName()
        {
            Assert.Equal("Example Injury Location Name", ((DeathRecord)JSONRecords[0]).InjuryLocationName);
            Assert.Equal("Example Injury Location Name", ((DeathRecord)XMLRecords[0]).InjuryLocationName);
        }

        // [Fact]
        // public void Set_InjuryLocationDescription()
        // {
        //     SetterDeathRecord.InjuryLocationDescription = "Example Injury Location Description";
        //     Assert.Equal("Example Injury Location Description", SetterDeathRecord.InjuryLocationDescription);
        // }

        // [Fact]
        // public void Get_InjuryLocationDescription()
        // {
        //     Assert.Equal("Example Injury Location Description", ((DeathRecord)JSONRecords[0]).InjuryLocationDescription);
        //     Assert.Equal("Example Injury Location Description", ((DeathRecord)XMLRecords[0]).InjuryLocationDescription);
        // }

        [Fact]
        public void Set_InjuryDate()
        {
            SetterDeathRecord.InjuryDate = "2018-02-19T16:48:00";
            Assert.Equal("2018-02-19T16:48:00", SetterDeathRecord.InjuryDate);
            Assert.Equal(2018, (int)SetterDeathRecord.InjuryYear);
            Assert.Equal(2, (int)SetterDeathRecord.InjuryMonth);
            Assert.Equal(19, (int)SetterDeathRecord.InjuryDay);
            Assert.Equal("16:48", SetterDeathRecord.InjuryTime);
        }

        [Fact]
        public void Get_InjuryDate()
        {
            Assert.Equal("2018-02-19T16:48:00", ((DeathRecord)JSONRecords[0]).InjuryDate);
            Assert.Equal(2018, (int)((DeathRecord)JSONRecords[0]).InjuryYear);
            Assert.Equal(2, (int)((DeathRecord)JSONRecords[0]).InjuryMonth);
            Assert.Equal(19, (int)((DeathRecord)JSONRecords[0]).InjuryDay);
            Assert.Equal("16:48", ((DeathRecord)JSONRecords[0]).InjuryTime);
            Assert.Equal("2018-02-19T16:48:00", ((DeathRecord)XMLRecords[0]).InjuryDate);
            Assert.Equal(2018, (int)((DeathRecord)XMLRecords[0]).InjuryYear);
            Assert.Equal(2, (int)((DeathRecord)XMLRecords[0]).InjuryMonth);
            Assert.Equal(19, (int)((DeathRecord)XMLRecords[0]).InjuryDay);
            Assert.Equal("16:48", ((DeathRecord)XMLRecords[0]).InjuryTime);
        }

        [Fact]
        public void Get_InjuryDate_Roundtrip()
        {
            DeathRecord dr = new DeathRecord(File.ReadAllText(FixturePath("fixtures/json/DeathRecord1.json")));
            IJEMortality ije1 = new IJEMortality(dr);
            Assert.Equal("2018", ije1.DOI_YR);
            Assert.Equal("02", ije1.DOI_MO);
            Assert.Equal("19", ije1.DOI_DY);
            DeathRecord dr2 = ije1.ToDeathRecord();
            Assert.Equal("2018-02-19T16:48:00", dr2.InjuryDate);
            Assert.Equal(2018, (int)dr2.InjuryYear);
            Assert.Equal(02, (int)dr2.InjuryMonth);
            Assert.Equal(19, (int)dr2.InjuryDay);
        }

        [Fact]
        public void Set_InjuryAtWork()
        {
            Dictionary<string, string> iaw = new Dictionary<string, string>();
            iaw.Add("code", "N");
            iaw.Add("system", VRDR.CodeSystems.YesNo);
            iaw.Add("display", "No");
            SetterDeathRecord.InjuryAtWork = iaw;
            Assert.Equal("N", SetterDeathRecord.InjuryAtWork["code"]);
            Assert.Equal(VRDR.CodeSystems.YesNo, SetterDeathRecord.InjuryAtWork["system"]);
            Assert.Equal("No", SetterDeathRecord.InjuryAtWork["display"]);
            Assert.Equal("N", SetterDeathRecord.InjuryAtWorkHelper);
            SetterDeathRecord.InjuryAtWorkHelper = "Y";
            Assert.Equal("Y", SetterDeathRecord.InjuryAtWork["code"]);
            Assert.Equal(VRDR.CodeSystems.YesNo, SetterDeathRecord.InjuryAtWork["system"]);
            Assert.Equal("Yes", SetterDeathRecord.InjuryAtWork["display"]);
            Assert.Equal("Y", SetterDeathRecord.InjuryAtWorkHelper);
            SetterDeathRecord.InjuryAtWorkHelper = "NA";
            Assert.Equal("NA", SetterDeathRecord.InjuryAtWork["code"]);
            Assert.Equal(VRDR.CodeSystems.NullFlavor_HL7_V3, SetterDeathRecord.InjuryAtWork["system"]);
            Assert.Equal("not applicable", SetterDeathRecord.InjuryAtWork["display"]);
            Assert.Equal("NA", SetterDeathRecord.InjuryAtWorkHelper);
        }

        [Fact]
        public void Get_InjuryAtWork()
        {
            Assert.Equal("N", ((DeathRecord)JSONRecords[0]).InjuryAtWork["code"]);
            Assert.Equal(VRDR.CodeSystems.YesNo_0136HL7_V2, ((DeathRecord)JSONRecords[0]).InjuryAtWork["system"]);
            Assert.Equal("No", ((DeathRecord)JSONRecords[0]).InjuryAtWork["display"]);
            Assert.Equal("N", ((DeathRecord)JSONRecords[0]).InjuryAtWorkHelper);
            Assert.Equal("N", ((DeathRecord)XMLRecords[0]).InjuryAtWork["code"]);
            Assert.Equal(VRDR.CodeSystems.YesNo_0136HL7_V2, ((DeathRecord)XMLRecords[0]).InjuryAtWork["system"]);
            Assert.Equal("No", ((DeathRecord)XMLRecords[0]).InjuryAtWork["display"]);
            Assert.Equal("N", ((DeathRecord)XMLRecords[0]).InjuryAtWorkHelper);
        }



        [Fact]
        public void Set_DeathLocationLatLong()
        {
            SetterDeathRecord.DeathLocationLatitude = "38.889248";
            SetterDeathRecord.DeathLocationLongitude = "-77.050636";
            Assert.Equal("-77.050636", SetterDeathRecord.DeathLocationLongitude);
            Assert.Equal("38.889248", SetterDeathRecord.DeathLocationLatitude);
        }
        [Fact]
        public void Get_DeathLocationLatLong()
        {
            Assert.Equal("-77.050636", ((DeathRecord)JSONRecords[0]).DeathLocationLongitude);
            Assert.Equal("38.889248", ((DeathRecord)JSONRecords[0]).DeathLocationLatitude);
            Assert.Equal("-77.050636", ((DeathRecord)XMLRecords[0]).DeathLocationLongitude);
            Assert.Equal("38.889248", ((DeathRecord)XMLRecords[0]).DeathLocationLatitude);
        }

        [Fact]
        public void Set_DeathLocationAddress()
        {
            Dictionary<string, string> dtladdress = new Dictionary<string, string>();

            dtladdress.Add("addressLine1", "671 Example Street");
            dtladdress.Add("addressLine2", "Line 2");
            dtladdress.Add("addressCity", "Bedford");
            dtladdress.Add("addressCounty", "Middlesex");
            dtladdress.Add("addressCountyC", "123");
            dtladdress.Add("addressCityC", "12345");
            dtladdress.Add("addressState", "MA");
            dtladdress.Add("addressZip", "01730");
            dtladdress.Add("addressCountry", "US");
            dtladdress.Add("addressPredir", "W");
            dtladdress.Add("addressPostdir", "E");
            dtladdress.Add("addressStname", "Example");
            dtladdress.Add("addressStnum", "11");
            dtladdress.Add("addressStdesig", "Street");
            dtladdress.Add("addressUnitnum", "3");
            SetterDeathRecord.DeathLocationAddress = dtladdress;
            Assert.Equal("671 Example Street", SetterDeathRecord.DeathLocationAddress["addressLine1"]);
            Assert.Equal("Line 2", SetterDeathRecord.DeathLocationAddress["addressLine2"]);
            Assert.Equal("Bedford", SetterDeathRecord.DeathLocationAddress["addressCity"]);
            Assert.Equal("Middlesex", SetterDeathRecord.DeathLocationAddress["addressCounty"]);
            Assert.Equal("12345", SetterDeathRecord.DeathLocationAddress["addressCityC"]);
            Assert.Equal("123", SetterDeathRecord.DeathLocationAddress["addressCountyC"]);
            Assert.Equal("MA", SetterDeathRecord.DeathLocationAddress["addressState"]);
            Assert.Equal("01730", SetterDeathRecord.DeathLocationAddress["addressZip"]);
            Assert.Equal("US", SetterDeathRecord.DeathLocationAddress["addressCountry"]);
            Assert.Equal("W", SetterDeathRecord.DeathLocationAddress["addressPredir"]);
            Assert.Equal("E", SetterDeathRecord.DeathLocationAddress["addressPostdir"]);
            Assert.Equal("Example", SetterDeathRecord.DeathLocationAddress["addressStname"]);
            Assert.Equal("11", SetterDeathRecord.DeathLocationAddress["addressStnum"]);
            Assert.Equal("Street", SetterDeathRecord.DeathLocationAddress["addressStdesig"]);
            Assert.Equal("3", SetterDeathRecord.DeathLocationAddress["addressUnitnum"]);
        }

        [Fact]
        public void Get_DeathLocationAddress()
        {
            Assert.Equal("671 Example Street", ((DeathRecord)JSONRecords[0]).DeathLocationAddress["addressLine1"]);
            Assert.Equal("Line 2", ((DeathRecord)JSONRecords[0]).DeathLocationAddress["addressLine2"]);
            Assert.Equal("Bedford", ((DeathRecord)JSONRecords[0]).DeathLocationAddress["addressCity"]);
            Assert.Equal("Middlesex", ((DeathRecord)JSONRecords[0]).DeathLocationAddress["addressCounty"]);
            Assert.Equal("NY", ((DeathRecord)JSONRecords[0]).DeathLocationAddress["addressState"]);
            Assert.Equal("YC", ((DeathRecord)JSONRecords[0]).DeathLocationAddress["addressJurisdiction"]);
            Assert.Equal("01730", ((DeathRecord)JSONRecords[0]).DeathLocationAddress["addressZip"]);
            Assert.Equal("US", ((DeathRecord)JSONRecords[0]).DeathLocationAddress["addressCountry"]);
            Assert.Equal("671 Example Street", ((DeathRecord)XMLRecords[0]).DeathLocationAddress["addressLine1"]);
            Assert.Equal("Line 2", ((DeathRecord)XMLRecords[0]).DeathLocationAddress["addressLine2"]);
            Assert.Equal("Bedford", ((DeathRecord)XMLRecords[0]).DeathLocationAddress["addressCity"]);
            Assert.Equal("Middlesex", ((DeathRecord)XMLRecords[0]).DeathLocationAddress["addressCounty"]);
            Assert.Equal("NY", ((DeathRecord)XMLRecords[0]).DeathLocationAddress["addressState"]);
            Assert.Equal("01730", ((DeathRecord)XMLRecords[0]).DeathLocationAddress["addressZip"]);
            Assert.Equal("US", ((DeathRecord)XMLRecords[0]).DeathLocationAddress["addressCountry"]);
            Assert.Equal("12345", ((DeathRecord)XMLRecords[0]).DeathLocationAddress["addressCityC"]);
            Assert.Equal("123", ((DeathRecord)JSONRecords[0]).DeathLocationAddress["addressCountyC"]);
            Assert.Equal("12345", ((DeathRecord)XMLRecords[0]).DeathLocationAddress["addressCityC"]);
            Assert.Equal("123", ((DeathRecord)JSONRecords[0]).DeathLocationAddress["addressCountyC"]);
            Assert.Equal("W", ((DeathRecord)JSONRecords[0]).DeathLocationAddress["addressPredir"]);
            Assert.Equal("E", ((DeathRecord)JSONRecords[0]).DeathLocationAddress["addressPostdir"]);
            Assert.Equal("Example", ((DeathRecord)JSONRecords[0]).DeathLocationAddress["addressStname"]);
            Assert.Equal("671", ((DeathRecord)JSONRecords[0]).DeathLocationAddress["addressStnum"]);
            Assert.Equal("Street", ((DeathRecord)JSONRecords[0]).DeathLocationAddress["addressStdesig"]);
            Assert.Equal("3", ((DeathRecord)JSONRecords[0]).DeathLocationAddress["addressUnitnum"]);
            Assert.Equal("W", ((DeathRecord)XMLRecords[0]).DeathLocationAddress["addressPredir"]);
            Assert.Equal("E", ((DeathRecord)XMLRecords[0]).DeathLocationAddress["addressPostdir"]);
            Assert.Equal("Example", ((DeathRecord)XMLRecords[0]).DeathLocationAddress["addressStname"]);
            Assert.Equal("671", ((DeathRecord)XMLRecords[0]).DeathLocationAddress["addressStnum"]);
            Assert.Equal("Street", ((DeathRecord)XMLRecords[0]).DeathLocationAddress["addressStdesig"]);
            Assert.Equal("3", ((DeathRecord)XMLRecords[0]).DeathLocationAddress["addressUnitnum"]);
        }

        [Fact]
        public void Set_DeathLocationName()
        {
            SetterDeathRecord.DeathLocationName = "Example Death Location Name";
            Assert.Equal("Example Death Location Name", SetterDeathRecord.DeathLocationName);
        }

        [Fact]
        public void Set_DeathLocationJurisdiction()
        {
            SetterDeathRecord.DeathLocationJurisdiction = "MA";
            Assert.Equal("MA", SetterDeathRecord.DeathLocationJurisdiction);
            SetterDeathRecord.DeathLocationJurisdiction = "YC";
            Assert.Equal("YC", SetterDeathRecord.DeathLocationJurisdiction);
        }

        [Fact]
        public void Get_DeathLocationName()
        {
            Assert.Equal("Example Death Location Name", ((DeathRecord)JSONRecords[0]).DeathLocationName);
            Assert.Equal("Example Death Location Name", ((DeathRecord)XMLRecords[0]).DeathLocationName);
        }

        [Fact]
        public void Get_DeathLocationJurisdiction()
        {
            Assert.Equal("YC", ((DeathRecord)JSONRecords[0]).DeathLocationJurisdiction);
            Assert.Equal("YC", ((DeathRecord)XMLRecords[0]).DeathLocationJurisdiction);
        }

        [Fact]
        public void Set_DeathLocationDescription()
        {
            SetterDeathRecord.DeathLocationDescription = "Example Death Location Description";
            Assert.Equal("Example Death Location Description", SetterDeathRecord.DeathLocationDescription);
        }

        [Fact]
        public void Get_DeathLocationDescription()
        {
            Assert.Equal("Example Death Location Description", ((DeathRecord)JSONRecords[0]).DeathLocationDescription);
            Assert.Equal("Example Death Location Description", ((DeathRecord)XMLRecords[0]).DeathLocationDescription);
        }

        [Fact]
        public void Set_DateOfDeath()
        {
            SetterDeathRecord.DateOfDeath = "2018-02-19T16:48:00";
            Assert.Equal("2018-02-19T16:48:00", SetterDeathRecord.DateOfDeath);
        }

        [Fact]
        public void Get_DateOfDeath()
        {
            Assert.Null(((DeathRecord)JSONRecords[1]).DateOfDeath);
            Assert.Null(((DeathRecord)JSONRecords[1]).DeathDay);
            Assert.Equal((uint)2020,(((DeathRecord)JSONRecords[1]).DeathYear));
            Assert.Equal("2020-11-12T00:00:00", ((DeathRecord)JSONRecords[2]).DateOfDeath);
            Assert.Equal((uint)2020,(((DeathRecord)JSONRecords[2]).DeathYear));
            Assert.Null( ((DeathRecord)JSONRecords[2]).DeathTime);
            Assert.Equal("2019-02-19T16:48:00", ((DeathRecord)XMLRecords[0]).DateOfDeath);
            Assert.Equal((uint)2019,(((DeathRecord)JSONRecords[0]).DeathYear));
        }

        [Fact]
        public void Get_DateOfDeath_Roundtrip()
        {
            DeathRecord dr = new DeathRecord(File.ReadAllText(FixturePath("fixtures/json/DeathRecord1.json")));
            IJEMortality ije1 = new IJEMortality(dr);
            Assert.Equal("2019", ije1.DOD_YR);
            Assert.Equal("02", ije1.DOD_MO);
            Assert.Equal("19", ije1.DOD_DY);
            DeathRecord dr2 = ije1.ToDeathRecord();
            Assert.Equal("2019-02-19T16:48:00", dr2.DateOfDeath);
            Assert.Equal(2019, (int)dr2.DeathYear);
            Assert.Equal(02, (int)dr2.DeathMonth);
            Assert.Equal(19, (int)dr2.DeathDay);
        }

        [Fact]
        public void Set_DateOfDeath_Partial_Date()
        {
            //Tuple<string, string>[] datePart = { Tuple.Create("date-year", "2021"), Tuple.Create("date-month", "5"), Tuple.Create("day-absent-reason", "asked-unknown")};
            SetterDeathRecord.DeathYear = 2021;
            SetterDeathRecord.DeathMonth = 5;
            SetterDeathRecord.DeathDay = null;
            Assert.Equal(2021, (int)SetterDeathRecord.DeathYear);
            Assert.Equal(5, (int)SetterDeathRecord.DeathMonth);
            Assert.Null(SetterDeathRecord.DeathDay);
        }

        [Fact]
        public void Get_DateOfDeath_Partial_Date()
        {
            DeathRecord dr = new DeathRecord(File.ReadAllText(FixturePath("fixtures/json/BirthAndDeathDateDataAbsent.json")));
            Assert.Equal(2021, (int)dr.DeathYear);
            Assert.Equal(2, (int)dr.DeathMonth);
            Assert.Null(dr.DeathDay);
        }

        [Fact]
        public void Get_DateOfDeath_Partial_Date_Roundtrip()
        {
            DeathRecord dr = new DeathRecord(File.ReadAllText(FixturePath("fixtures/json/BirthAndDeathDateDataAbsent.json")));
            IJEMortality ije1 = new IJEMortality(dr);
            Assert.Equal("2021", ije1.DOD_YR);
            Assert.Equal("02", ije1.DOD_MO);
            Assert.Equal("99", ije1.DOD_DY);
        }

        [Fact]
        public void Set_DateOfDeathPronouncement()
        {
            SetterDeathRecord.DateOfDeathPronouncement = "2019-01-31T17:48:07.498822-05:00";
            Assert.Equal("2019-01-31T17:48:07.498822-05:00", SetterDeathRecord.DateOfDeathPronouncement);
        }

        [Fact]
        public void Get_DateOfDeathPronouncement()
        {
            Assert.Equal("2018-02-20T16:48:06-05:00", ((DeathRecord)JSONRecords[0]).DateOfDeathPronouncement);
            Assert.Equal("2019-02-20T16:48:06-05:00", ((DeathRecord)XMLRecords[0]).DateOfDeathPronouncement);
        }

        [Fact]
        public void Set_SurgeryDate()
        {
            SetterDeathRecord.SurgeryDate = "2017-03-18";
            Assert.Equal("2017-03-18", SetterDeathRecord.SurgeryDate);
        }

        [Fact]
        public void Get_SurgeryDate()
        {
            Assert.Equal("2017-03-18", ((DeathRecord)JSONRecords[0]).SurgeryDate);
            Assert.Equal("2017-03-18", ((DeathRecord)XMLRecords[0]).SurgeryDate);
        }

        [Fact]
        public void Get_SurgeryDate_Roundtrip()
        {
            DeathRecord dr = new DeathRecord(File.ReadAllText(FixturePath("fixtures/json/DeathRecord1.json")));
            IJEMortality ije1 = new IJEMortality(dr);
            Assert.Equal("2017", ije1.SUR_YR);
            Assert.Equal("03", ije1.SUR_MO);
            Assert.Equal("18", ije1.SUR_DY);
            DeathRecord dr2 = ije1.ToDeathRecord();
            Assert.Equal("2017-03-18", dr2.SurgeryDate);
            Assert.Equal(2017, (int)dr2.SurgeryYear);
            Assert.Equal(03, (int)dr2.SurgeryMonth);
            Assert.Equal(18, (int)dr2.SurgeryDay);
        }

        [Fact]
        public void Set_SurgeryDate_Partial_Date()
        {
            SetterDeathRecord.SurgeryYear = 2017;
            SetterDeathRecord.SurgeryMonth = 3;
            SetterDeathRecord.SurgeryDay = null;
            Assert.Equal(2017, (int)SetterDeathRecord.SurgeryYear);
            Assert.Equal(3, (int)SetterDeathRecord.SurgeryMonth);
            Assert.Null(SetterDeathRecord.SurgeryDay);
        }
        [Fact]
        public void Set_ReceiptDate_Partial_Date()
        {
            SetterDeathRecord.ReceiptYear = 2017;
            SetterDeathRecord.ReceiptMonth = 3;
            SetterDeathRecord.ReceiptDay = null;
            Assert.Equal(2017, (int)SetterDeathRecord.ReceiptYear);
            Assert.Equal(3, (int)SetterDeathRecord.ReceiptMonth);
            Assert.Null(SetterDeathRecord.ReceiptDay);
        }

        [Fact]
        public void Get_ReceiptDate_Roundtrip()
        {
            DeathRecord dr = new DeathRecord(File.ReadAllText(FixturePath("fixtures/json/Bundle-DeathCertificateDocument-Example2.json")));
            IJEMortality ije1 = new IJEMortality(dr, false);
            Assert.Equal("2021", ije1.R_YR);
            Assert.Equal("12", ije1.R_MO);
            Assert.Equal("12", ije1.R_DY);
            DeathRecord dr2 = ije1.ToDeathRecord();
            Assert.Equal("2021-12-12", dr2.ReceiptDate);
            Assert.Equal(2021, (int)dr2.ReceiptYear);
            Assert.Equal(12, (int)dr2.ReceiptMonth);
            Assert.Equal(12, (int)dr2.ReceiptDay);
        }
        [Fact]
        public void Get_SurgeryDate_Partial_Date()
        {
            DeathRecord dr = new DeathRecord(File.ReadAllText(FixturePath("fixtures/json/BirthAndDeathDateDataAbsent.json")));
            IJEMortality ije1 = new IJEMortality(dr);
            Assert.Equal("2017", ije1.SUR_YR);
            Assert.Equal("03", ije1.SUR_MO);
            Assert.Equal("99", ije1.SUR_DY);
        }

        [Fact]
        public void Set_ReceiptDate()
        {
            SetterDeathRecord.ReceiptDate = "2017-03-18";
            Assert.Equal("2017-03-18", SetterDeathRecord.ReceiptDate);
        }

        [Fact]
        public void Get_ReceiptDate()
        {
            Assert.Equal("2021-12-12", ((DeathRecord)JSONRecords[1]).ReceiptDate);
        }

        [Fact]
        public void Set_TransaxConversion()
        {
            SetterDeathRecord.TransaxConversionHelper = ValueSets.TransaxConversion.Artificial_Code_Conversion_No_Other_Action;
            Assert.Equal(ValueSets.TransaxConversion.Artificial_Code_Conversion_No_Other_Action, SetterDeathRecord.TransaxConversionHelper);
        }

        [Fact]
        public void Get_TransaxConversion()
        {
            Assert.Equal(VRDR.ValueSets.TransaxConversion.Conversion_Using_Non_Ambivalent_Table_Entries, ((DeathRecord)JSONRecords[1]).TransaxConversionHelper);
        }

        [Fact]
        public void Set_AcmeSystemReject()
        {
            SetterDeathRecord.AcmeSystemRejectHelper = ValueSets.AcmeSystemReject.Not_Rejected;
            Assert.Equal(ValueSets.AcmeSystemReject.Not_Rejected, SetterDeathRecord.AcmeSystemRejectHelper);
        }

        [Fact]
        public void Get_AcmeSystemReject()
        {
            Assert.Equal(VRDR.ValueSets.AcmeSystemReject.Not_Rejected, ((DeathRecord)JSONRecords[1]).AcmeSystemRejectHelper);
        }

        [Fact]
        public void Set_IntentionalReject()
        {
            SetterDeathRecord.IntentionalRejectHelper = ValueSets.IntentionalReject.Reject1;
            Assert.Equal(ValueSets.IntentionalReject.Reject1, SetterDeathRecord.IntentionalRejectHelper);
        }

        [Fact]
        public void Get_IntentionalReject()
        {
            Assert.Equal(VRDR.ValueSets.IntentionalReject.Reject1, ((DeathRecord)JSONRecords[1]).IntentionalRejectHelper);
        }

        [Fact]
        public void Set_ShipmentNumber()
        {
            SetterDeathRecord.ShipmentNumber = "3";
            Assert.Equal("3", SetterDeathRecord.ShipmentNumber);
        }

        [Fact]
        public void Get_ShipmentNumber()
        {
            Assert.Equal("A2B2", ((DeathRecord)JSONRecords[1]).ShipmentNumber);
        }


        [Fact]
        public void Set_EmergingIssues()
        {
            SetterDeathRecord.EmergingIssue1_1 = "A";
            SetterDeathRecord.EmergingIssue1_2 = "B";
            SetterDeathRecord.EmergingIssue1_3 = "C";
            SetterDeathRecord.EmergingIssue1_4 = "D";
            SetterDeathRecord.EmergingIssue1_5 = "E";
            SetterDeathRecord.EmergingIssue1_6 = "F";
            SetterDeathRecord.EmergingIssue8_1 = "AAAAAAAA";
            SetterDeathRecord.EmergingIssue8_2 = "BBBBBBBB";
            SetterDeathRecord.EmergingIssue8_3 = "CCCCCCCC";
            SetterDeathRecord.EmergingIssue20 = "AAAAAAAAAAAAAAAAAAAA";
            Assert.Equal("A", SetterDeathRecord.EmergingIssue1_1);
            Assert.Equal("B", SetterDeathRecord.EmergingIssue1_2);
            Assert.Equal("C", SetterDeathRecord.EmergingIssue1_3);
            Assert.Equal("D", SetterDeathRecord.EmergingIssue1_4);
            Assert.Equal("E", SetterDeathRecord.EmergingIssue1_5);
            Assert.Equal("F", SetterDeathRecord.EmergingIssue1_6);
            Assert.Equal("AAAAAAAA", SetterDeathRecord.EmergingIssue8_1);
            Assert.Equal("BBBBBBBB", SetterDeathRecord.EmergingIssue8_2);
            Assert.Equal("CCCCCCCC", SetterDeathRecord.EmergingIssue8_3);
            Assert.Equal("AAAAAAAAAAAAAAAAAAAA", SetterDeathRecord.EmergingIssue20);
            IJEMortality ije = new IJEMortality(SetterDeathRecord, false); // Don't validate since we don't care about most fields
            Assert.Equal("A", ije.PLACE1_1);
            Assert.Equal("B", ije.PLACE1_2);
            Assert.Equal("C", ije.PLACE1_3);
            Assert.Equal("D", ije.PLACE1_4);
            Assert.Equal("E", ije.PLACE1_5);
            Assert.Equal("F", ije.PLACE1_6);
            Assert.Equal("AAAAAAAA", ije.PLACE8_1);
            Assert.Equal("BBBBBBBB", ije.PLACE8_2);
            Assert.Equal("CCCCCCCC", ije.PLACE8_3);
            Assert.Equal("AAAAAAAAAAAAAAAAAAAA", ije.PLACE20);
        }

        [Fact]
        public void Get_EmergingIssues()
        {
            Assert.Equal("A", ((DeathRecord)JSONRecords[0]).EmergingIssue1_1);
            Assert.Equal("B", ((DeathRecord)JSONRecords[0]).EmergingIssue1_2);
            Assert.Equal("C", ((DeathRecord)JSONRecords[0]).EmergingIssue1_3);
            Assert.Equal("D", ((DeathRecord)JSONRecords[0]).EmergingIssue1_4);
            Assert.Equal("E", ((DeathRecord)JSONRecords[0]).EmergingIssue1_5);
            Assert.Equal("F", ((DeathRecord)JSONRecords[0]).EmergingIssue1_6);
            Assert.Equal("AAAAAAAA", ((DeathRecord)JSONRecords[0]).EmergingIssue8_1);
            Assert.Equal("BBBBBBBB", ((DeathRecord)JSONRecords[0]).EmergingIssue8_2);
            Assert.Equal("CCCCCCCC", ((DeathRecord)JSONRecords[0]).EmergingIssue8_3);
            Assert.Equal("AAAAAAAAAAAAAAAAAAAA", ((DeathRecord)JSONRecords[0]).EmergingIssue20);
            Assert.Equal("A", ((DeathRecord)XMLRecords[0]).EmergingIssue1_1);
            Assert.Equal("B", ((DeathRecord)XMLRecords[0]).EmergingIssue1_2);
            Assert.Equal("C", ((DeathRecord)XMLRecords[0]).EmergingIssue1_3);
            Assert.Equal("D", ((DeathRecord)XMLRecords[0]).EmergingIssue1_4);
            Assert.Equal("E", ((DeathRecord)XMLRecords[0]).EmergingIssue1_5);
            Assert.Equal("F", ((DeathRecord)XMLRecords[0]).EmergingIssue1_6);
            Assert.Equal("AAAAAAAA", ((DeathRecord)XMLRecords[0]).EmergingIssue8_1);
            Assert.Equal("BBBBBBBB", ((DeathRecord)XMLRecords[0]).EmergingIssue8_2);
            Assert.Equal("CCCCCCCC", ((DeathRecord)XMLRecords[0]).EmergingIssue8_3);
            Assert.Equal("AAAAAAAAAAAAAAAAAAAA", ((DeathRecord)XMLRecords[0]).EmergingIssue20);
        }

        [Fact]
        public void Set_EntityAxisCodes()
        {
            SetterDeathRecord.EntityAxisCauseOfDeath = new[] { (LineNumber: 2, Position: 1, Code: "T27.3", ECode: true) };
            var eacGet = SetterDeathRecord.EntityAxisCauseOfDeath;
            Assert.Single(eacGet);
            Assert.Equal(2, eacGet.ElementAt(0).LineNumber);
            Assert.Equal(1, eacGet.ElementAt(0).Position);
            Assert.Equal("T27.3", eacGet.ElementAt(0).Code);
            Assert.True(eacGet.ElementAt(0).ECode);

            IJEMortality ije = new IJEMortality(SetterDeathRecord, false); // Don't validate since we don't care about most fields
            string fmtEac = "21T273 &".PadRight(160, ' ');
            Assert.Equal(fmtEac, ije.EAC);
        }

        [Fact]
        public void Get_EntityAxisCodes()
        {
            var eacGet = ((DeathRecord)JSONRecords[1]).EntityAxisCauseOfDeath;
            Assert.Single(eacGet);
            Assert.Equal(1, eacGet.ElementAt(0).LineNumber);
            Assert.Equal(1, eacGet.ElementAt(0).Position);
            Assert.Equal("J96.0", eacGet.ElementAt(0).Code);
            Assert.False(eacGet.ElementAt(0).ECode);
            // Add more items to IG example
        }

        [Fact]
        public void Set_RecordAxisCodes()
        {
            SetterDeathRecord.RecordAxisCauseOfDeath = new[] { (Position: 1, Code: "T27.3", Pregnancy: true), (Position: 2, Code: "T27.5", Pregnancy: true) };
            var racGet = SetterDeathRecord.RecordAxisCauseOfDeath;
            Assert.Equal(2, racGet.Count());
            Assert.Equal(1, racGet.ElementAt(0).Position);
            Assert.Equal("T27.3", racGet.ElementAt(0).Code);
            Assert.False(racGet.ElementAt(0).Pregnancy); // Pregnancy flag is only allowed in position 2
            Assert.Equal(2, racGet.ElementAt(1).Position);
            Assert.Equal("T27.5", racGet.ElementAt(1).Code);
            Assert.True(racGet.ElementAt(1).Pregnancy);

            IJEMortality ije = new IJEMortality(SetterDeathRecord, false); // Don't validate since we don't care about most fields
            string fmtRac = "T273 T2751".PadRight(100, ' ');
            Assert.Equal(fmtRac, ije.RAC);
        }

        [Fact]
        public void Get_RecordAxisCodes()
        {
            var racGet = ((DeathRecord)JSONRecords[1]).RecordAxisCauseOfDeath;
            Assert.Single(racGet);
            Assert.Equal(1, racGet.ElementAt(0).Position);
            Assert.Equal("J96.0", racGet.ElementAt(0).Code);
            Assert.False(racGet.ElementAt(0).Pregnancy);
        }

        [Fact]
        public void CheckConnectathonRecord1()
        {
            DeathRecord dr1 = VRDR.Connectathon.FideliaAlsup();
            Assert.NotNull(dr1.ToDescription()); // This endpoint is used by Canary
            IJEMortality ije = new IJEMortality(dr1, false); // Don't validate since we don't care about most fields
            Assert.Equal("062", ije.AGE);
            Assert.Equal("478151044", ije.SSN);
            Assert.Equal("Unrestrained ejected driver in rollover motor vehicle accident", ije.HOWINJ.Trim());
            Assert.Equal("H", ije.DETHNIC2);
        }

        [Fact]
        public void Test_GetCauseOfDeathCodedContentBundle()
        {
            Bundle bundle = ((DeathRecord)JSONRecords[0]).GetCauseOfDeathCodedContentBundle();
            Assert.NotNull(bundle);
            // TODO: Fill out tests
        }

        [Fact]
        public void Test_GetDemographicCodedContentBundle()
        {
            Bundle bundle = ((DeathRecord)JSONRecords[0]).GetDemographicCodedContentBundle();
            Assert.NotNull(bundle);
            // TODO: Fill out tests
        }

        [Fact]
        public void TestTRXRoundTrip()
        {
            IJEMortality ije = new IJEMortality();
            ije.DOD_YR = "2021";
            ije.DSTATE = "MA";
            ije.FILENO = "578660";
            ije.MAN_UC = "I219";
            ije.EAC = "21I219  31I251  61E119  62F179  63I10   64E780";
            ije.RAC = "I219 E119 E780 F179 I10  I251";
            ije.AUXNO = "579927";
            ije.MFILED = "0";
            ije.MANNER = "N";
            ije.trx.CS = "1";
            ije.trx.SHIP = "497";
            ije.AUTOP = "Y";
            ije.AUTOPF = "Y";
            ije.TOBAC = "Y";
            ije.PREG = "8";
            ije.CERTL = "D";
            Assert.Equal("D", ije.CERTL);
            ije.CERTL = "DDDD";
            Assert.Equal("DDDD", ije.CERTL);
            ije.TRANSPRT = "Hover Board Rider";
            ije.INACT = "9";
            DeathRecord record = ije.ToDeathRecord();
            DeathRecord record1 = new DeathRecord(record.GetCauseOfDeathCodedContentBundle());
            IJEMortality ije2 = new IJEMortality(record);
            Assert.Equal("2021", ije2.DOD_YR);
            Assert.Equal("MA", ije2.DSTATE);
            Assert.Equal("578660", ije2.FILENO);
            Assert.Equal("I219", ije2.MAN_UC);
            Assert.Equal("21I219  31I251  61E119  62F179  63I10   64E780".PadRight(160), ije2.EAC);
            Assert.Equal("I219 E119 E780 F179 I10  I251".PadRight(100), ije2.RAC);
            Assert.Equal("579927".PadLeft(12, '0'), ije2.AUXNO);
            Assert.Equal("0", ije2.MFILED);
            Assert.Equal("N", ije2.MANNER);
            Assert.Equal("1", ije2.trx.CS);
            Assert.Equal("497", ije2.trx.SHIP);
            Assert.Equal("Y", ije2.AUTOP);
            Assert.Equal("Y", ije2.AUTOPF);
            Assert.Equal("Y", ije2.TOBAC);
            Assert.Equal("8", ije2.PREG);
            Assert.Equal("DDDD", ije2.CERTL);
            Assert.Equal("Hover Board Rider", ije2.TRANSPRT);
            Assert.Equal("9", ije2.INACT);
        }


        private string FixturePath(string filePath)
        {
            if (Path.IsPathRooted(filePath))
            {
                return filePath;
            }
            else
            {
                return Path.GetRelativePath(Directory.GetCurrentDirectory(), filePath);
            }
        }
    }
}
