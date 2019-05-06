using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using Xunit;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;

namespace FhirDeathRecord.Tests
{
    public class DeathRecord_Should
    {
        private ArrayList XMLRecords;

        private ArrayList JSONRecords;

        private DeathRecord SetterDeathRecord;

        public DeathRecord_Should()
        {
            XMLRecords = new ArrayList();
            XMLRecords.Add(new DeathRecord(File.ReadAllText(FixturePath("fixtures/xml/1.xml"))));
            JSONRecords = new ArrayList();
            JSONRecords.Add(new DeathRecord(File.ReadAllText(FixturePath("fixtures/json/1.json"))));
            SetterDeathRecord = new DeathRecord();
        }

        [Fact]
        public void FailGivenInvalidRecord()
        {
            Exception ex = Assert.Throws<System.FormatException>(() => new DeathRecord("foobar"));
            Assert.Equal("Invalid Json encountered. Details: Error parsing boolean value. Path '', line 1, position 1.", ex.Message);
        }

        // [Fact]
        // public void ToFromDescription()
        // {
        //     DeathRecord first = (DeathRecord)XMLRecords[0];
        //     string firstDescription = first.ToDescription();
        //     DeathRecord second = DeathRecord.FromDescription(firstDescription);
        //     Assert.Equal(first.Id, second.Id);
        //     Assert.Equal(first.ServedInArmedForces, second.ServedInArmedForces);
        //     Assert.Equal(first.GivenNames, second.GivenNames);
        //     Assert.Equal(first.AutopsyPerformed, second.AutopsyPerformed);
        //     Assert.Equal(first.Race, second.Race);
        //     Assert.Equal(first.CausesOfDeath, second.CausesOfDeath);
        //     Assert.Equal(first.DeathFromTransportInjury, second.DeathFromTransportInjury);
        //     Assert.Equal(first.DetailsOfInjury, second.DetailsOfInjury);
        // }

        [Fact]
        public void SetPatientAfterParse()
        {
            DeathRecord sample1 = new DeathRecord(File.ReadAllText(FixturePath("fixtures/xml/1.xml")));
            DeathRecord sample2 = new DeathRecord(File.ReadAllText(FixturePath("fixtures/json/1.json")));
            Assert.Equal("Last", sample1.FamilyName);
            Assert.Equal("Last", sample2.FamilyName);
            sample1.FamilyName = "1changed2abc";
            sample2.FamilyName = "2changed1xyz";
            Assert.Equal("1changed2abc", sample1.FamilyName);
            Assert.Equal("2changed1xyz", sample2.FamilyName);
        }

        [Fact]
        public void SetPractitionerAfterParse()
        {
            DeathRecord sample1 = new DeathRecord(File.ReadAllText(FixturePath("fixtures/xml/1.xml")));
            DeathRecord sample2 = new DeathRecord(File.ReadAllText(FixturePath("fixtures/json/1.json")));
            Assert.Equal("Last", sample1.CertifierFamilyName);
            Assert.Equal("Last", sample2.CertifierFamilyName);
            sample1.CertifierFamilyName = "1diff2abc";
            sample2.CertifierFamilyName = "2diff1xyz";
            Assert.Equal("1diff2abc", sample1.CertifierFamilyName);
            Assert.Equal("2diff1xyz", sample2.CertifierFamilyName);
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
            Assert.Equal("25e016fb-e93b-47a4-bc6a-293ae125e078", ((DeathRecord)JSONRecords[0]).Identifier);
            Assert.Equal("78ec4209-c593-4fe7-858b-26740b0bb56e", ((DeathRecord)XMLRecords[0]).Identifier);
        }

        [Fact]
        public void Set_BundleIdentifier()
        {
            SetterDeathRecord.BundleIdentifier = "1234567890";
            Assert.Equal("1234567890", SetterDeathRecord.BundleIdentifier);
        }

        [Fact]
        public void Get_BundleIdentifier()
        {
            Assert.Equal("b7849468-710e-4d8b-9e25-49451b4c12ac", ((DeathRecord)JSONRecords[0]).BundleIdentifier);
            Assert.Equal("22089b3e-c6e5-41e5-95ca-d56fcb94149b", ((DeathRecord)XMLRecords[0]).BundleIdentifier);
        }

        [Fact]
        public void Set_CertifiedTime()
        {
            SetterDeathRecord.CertifiedTime = "2019-01-28T16:48:06.4988220-05:00";
            Assert.Equal("2019-01-28T16:48:06.4988220-05:00", SetterDeathRecord.CertifiedTime);
        }

        [Fact]
        public void Get_CertifiedTime()
        {
            // TODO
        }

        [Fact]
        public void Set_CreatedTime()
        {
            SetterDeathRecord.CreatedTime = "2019-01-29T16:48:06.4988220-05:00";
            Assert.Equal("2019-01-29T16:48:06.4988220-05:00", SetterDeathRecord.CreatedTime);
        }

        [Fact]
        public void Get_CreatedTime()
        {
            // TODO
        }

        [Fact]
        public void Set_CertifierRole()
        {
            Dictionary<string, string> certifierRole = new Dictionary<string, string>();
            certifierRole.Add("code", "309343006");
            certifierRole.Add("system", "http://snomed.info/sct");
            certifierRole.Add("display", "Physician");
            SetterDeathRecord.CertifierRole = certifierRole;
            Assert.Equal("309343006", SetterDeathRecord.CertifierRole["code"]);
            Assert.Equal("http://snomed.info/sct", SetterDeathRecord.CertifierRole["system"]);
            Assert.Equal("Physician", SetterDeathRecord.CertifierRole["display"]);
        }

        [Fact]
        public void Get_CertifierRole()
        {
            // TODO
        }

        [Fact]
        public void Set_InterestedPartyIdentifier()
        {
            SetterDeathRecord.InterestedPartyIdentifier = "123abc";
            Assert.Equal("123abc", SetterDeathRecord.InterestedPartyIdentifier);
        }

        [Fact]
        public void Get_InterestedPartyIdentifier()
        {
            // TODO
        }

        [Fact]
        public void Set_InterestedPartyName()
        {
            SetterDeathRecord.InterestedPartyName = "123abc123xyz";
            Assert.Equal("123abc123xyz", SetterDeathRecord.InterestedPartyName);
        }

        [Fact]
        public void Get_InterestedPartyName()
        {
            // TODO
        }

        [Fact]
        public void Set_InterestedPartyAddress()
        {
            Dictionary<string, string> address = new Dictionary<string, string>();
            address.Add("addressLine1", "12 Example Street");
            address.Add("addressLine2", "Line 2");
            address.Add("addressCity", "Bedford");
            address.Add("addressCounty", "Middlesex");
            address.Add("addressState", "Massachusetts");
            address.Add("addressZip", "01730");
            address.Add("addressCountry", "United States");
            SetterDeathRecord.InterestedPartyAddress = address;
            Assert.Equal("12 Example Street", SetterDeathRecord.InterestedPartyAddress["addressLine1"]);
            Assert.Equal("Line 2", SetterDeathRecord.InterestedPartyAddress["addressLine2"]);
            Assert.Equal("Bedford", SetterDeathRecord.InterestedPartyAddress["addressCity"]);
            Assert.Equal("Middlesex", SetterDeathRecord.InterestedPartyAddress["addressCounty"]);
            Assert.Equal("Massachusetts", SetterDeathRecord.InterestedPartyAddress["addressState"]);
            Assert.Equal("01730", SetterDeathRecord.InterestedPartyAddress["addressZip"]);
            Assert.Equal("United States", SetterDeathRecord.InterestedPartyAddress["addressCountry"]);
        }

        [Fact]
        public void Get_InterestedPartyAddress()
        {
            // TODO
        }

        [Fact]
        public void Set_InterestedPartyType()
        {
            Dictionary<string, string> type = new Dictionary<string, string>();
            type.Add("code", "prov");
            type.Add("system", "http://terminology.hl7.org/CodeSystem/organization-type");
            type.Add("display", "Healthcare Provider");
            SetterDeathRecord.InterestedPartyType = type;
            Assert.Equal("prov", SetterDeathRecord.InterestedPartyType["code"]);
            Assert.Equal("http://terminology.hl7.org/CodeSystem/organization-type", SetterDeathRecord.InterestedPartyType["system"]);
            Assert.Equal("Healthcare Provider", SetterDeathRecord.InterestedPartyType["display"]);
        }

        [Fact]
        public void Get_InterestedPartyType()
        {
            // TODO
        }

        [Fact]
        public void Set_MannerOfDeathType()
        {
            Dictionary<string, string> type = new Dictionary<string, string>();
            type.Add("code", "7878000");
            type.Add("display", "Accident");
            SetterDeathRecord.MannerOfDeathType = type;
            Assert.Equal("7878000", SetterDeathRecord.MannerOfDeathType["code"]);
            Assert.Equal("Accident", SetterDeathRecord.MannerOfDeathType["display"]);
        }

        [Fact]
        public void Get_MannerOfDeathType()
        {
            // TODO
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
            // TODO
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
            // TODO
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
            // TODO
        }

        [Fact]
        public void Set_CertifierAddress()
        {
            Dictionary<string, string> caddress = new Dictionary<string, string>();
            caddress.Add("addressLine1", "11 Example Street");
            caddress.Add("addressLine2", "Line 2");
            caddress.Add("addressCity", "Bedford");
            caddress.Add("addressCounty", "Middlesex");
            caddress.Add("addressState", "Massachusetts");
            caddress.Add("addressZip", "01730");
            caddress.Add("addressCountry", "United States");
            SetterDeathRecord.CertifierAddress = caddress;
            Assert.Equal("11 Example Street", SetterDeathRecord.CertifierAddress["addressLine1"]);
            Assert.Equal("Line 2", SetterDeathRecord.CertifierAddress["addressLine2"]);
            Assert.Equal("Bedford", SetterDeathRecord.CertifierAddress["addressCity"]);
            Assert.Equal("Middlesex", SetterDeathRecord.CertifierAddress["addressCounty"]);
            Assert.Equal("Massachusetts", SetterDeathRecord.CertifierAddress["addressState"]);
            Assert.Equal("01730", SetterDeathRecord.CertifierAddress["addressZip"]);
            Assert.Equal("United States", SetterDeathRecord.CertifierAddress["addressCountry"]);
        }

        [Fact]
        public void Get_CertifierAddress()
        {
            // TODO
        }

        [Fact]
        public void Set_CertifierQualification()
        {
            Dictionary<string, string> qualification = new Dictionary<string, string>();
            qualification.Add("code", "MD");
            qualification.Add("system", "http://hl7.org/fhir/v2/0360/2.7");
            qualification.Add("display", "Doctor of Medicine");
            SetterDeathRecord.CertifierQualification = qualification;
            Assert.Equal("MD", SetterDeathRecord.CertifierQualification["code"]);
            Assert.Equal("http://hl7.org/fhir/v2/0360/2.7", SetterDeathRecord.CertifierQualification["system"]);
            Assert.Equal("Doctor of Medicine", SetterDeathRecord.CertifierQualification["display"]);
        }

        [Fact]
        public void Get_CertifierQualification()
        {
            // TODO
        }

        [Fact]
        public void Set_ContributingConditions()
        {
            SetterDeathRecord.ContributingConditions = "Example Contributing Condition";
            Assert.Equal("Example Contributing Condition", SetterDeathRecord.ContributingConditions);
        }

        [Fact]
        public void Get_ContributingConditions()
        {
            // TODO
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
            // TODO
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
            // TODO
        }

        [Fact]
        public void Set_CODE1A()
        {
            Dictionary<string, string> code = new Dictionary<string, string>();
            code.Add("code", "I21.0");
            code.Add("system", "http://hl7.org/fhir/sid/icd-10");
            code.Add("display", "Acute transmural myocardial infarction of anterior wall");
            SetterDeathRecord.CODE1A = code;
            Assert.Equal("I21.0", SetterDeathRecord.CODE1A["code"]);
            Assert.Equal("http://hl7.org/fhir/sid/icd-10", SetterDeathRecord.CODE1A["system"]);
            Assert.Equal("Acute transmural myocardial infarction of anterior wall", SetterDeathRecord.CODE1A["display"]);
        }

        [Fact]
        public void Get_CODE1A()
        {
            // TODO
        }

        [Fact]
        public void Set_COD1B()
        {
            SetterDeathRecord.COD1B = "cause 2";
            Assert.Equal("cause 2", SetterDeathRecord.COD1B);
        }

        [Fact]
        public void Get_COD1B()
        {
            // TODO
        }

        [Fact]
        public void Set_INTERVAL1B()
        {
            SetterDeathRecord.INTERVAL1B = "interval 2";
            Assert.Equal("interval 2", SetterDeathRecord.INTERVAL1B);
        }

        [Fact]
        public void Get_INTERVAL1B()
        {
            // TODO
        }

        [Fact]
        public void Set_CODE1B()
        {
            Dictionary<string, string> code = new Dictionary<string, string>();
            code.Add("code", "code 2");
            code.Add("system", "system 2");
            code.Add("display", "display 2");
            SetterDeathRecord.CODE1B = code;
            Assert.Equal("code 2", SetterDeathRecord.CODE1B["code"]);
            Assert.Equal("system 2", SetterDeathRecord.CODE1B["system"]);
            Assert.Equal("display 2", SetterDeathRecord.CODE1B["display"]);
        }

        [Fact]
        public void Get_CODE1B()
        {
            // TODO
        }

        [Fact]
        public void Set_COD1C()
        {
            SetterDeathRecord.COD1C = "cause 3";
            Assert.Equal("cause 3", SetterDeathRecord.COD1C);
        }

        [Fact]
        public void Get_COD1C()
        {
            // TODO
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
            // TODO
        }

        [Fact]
        public void Set_CODE1C()
        {
            Dictionary<string, string> code = new Dictionary<string, string>();
            code.Add("code", "code 3");
            code.Add("system", "system 3");
            code.Add("display", "display 3");
            SetterDeathRecord.CODE1C = code;
            Assert.Equal("code 3", SetterDeathRecord.CODE1C["code"]);
            Assert.Equal("system 3", SetterDeathRecord.CODE1C["system"]);
            Assert.Equal("display 3", SetterDeathRecord.CODE1C["display"]);
        }

        [Fact]
        public void Get_CODE1C()
        {
            // TODO
        }

        [Fact]
        public void Set_COD1D()
        {
            SetterDeathRecord.COD1A = "cause 4";
            Assert.Equal("cause 4", SetterDeathRecord.COD1A);
        }

        [Fact]
        public void Get_COD1D()
        {
            // TODO
        }

        [Fact]
        public void Set_INTERVAL1D()
        {
            SetterDeathRecord.INTERVAL1A = "interval 4";
            Assert.Equal("interval 4", SetterDeathRecord.INTERVAL1A);
        }

        [Fact]
        public void Get_INTERVAL1D()
        {
            // TODO
        }

        [Fact]
        public void Set_CODE1D()
        {
            Dictionary<string, string> code = new Dictionary<string, string>();
            code.Add("code", "code 4");
            code.Add("system", "system 4");
            code.Add("display", "display 4");
            SetterDeathRecord.CODE1A = code;
            Assert.Equal("code 4", SetterDeathRecord.CODE1A["code"]);
            Assert.Equal("system 4", SetterDeathRecord.CODE1A["system"]);
            Assert.Equal("display 4", SetterDeathRecord.CODE1A["display"]);
        }

        [Fact]
        public void Get_CODE1D()
        {
            // TODO
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
            // TODO
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
            // TODO
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
            // TODO
        }

        [Fact]
        public void Set_Gender()
        {
            SetterDeathRecord.Gender = "male";
            Assert.Equal("male", SetterDeathRecord.Gender);
        }

        [Fact]
        public void Get_Gender()
        {
            // TODO
        }

        [Fact]
        public void Set_BirthSex()
        {
            Dictionary<string, string> bscode = new Dictionary<string, string>();
            bscode.Add("code", "M");
            bscode.Add("system", "http://hl7.org/fhir/us/core/ValueSet/us-core-birthsex");
            bscode.Add("display", "Male");
            SetterDeathRecord.BirthSex = bscode;
            Assert.Equal("M", SetterDeathRecord.BirthSex["code"]);
            Assert.Equal("http://hl7.org/fhir/us/core/ValueSet/us-core-birthsex", SetterDeathRecord.BirthSex["system"]);
            Assert.Equal("Male", SetterDeathRecord.BirthSex["display"]);
        }

        [Fact]
        public void Get_BirthSex()
        {
            // TODO
        }

        [Fact]
        public void Set_DateOfBirth()
        {
            SetterDeathRecord.DateOfBirth = "1940-02-19T16:48:06.4988220-05:00";
            Assert.Equal("1940-02-19T16:48:06.4988220-05:00", SetterDeathRecord.DateOfBirth);
        }

        [Fact]
        public void Get_DateOfBirth()
        {
            // TODO
        }

        [Fact]
        public void Set_Residence()
        {
            Dictionary<string, string> raddress = new Dictionary<string, string>();
            raddress.Add("addressLine1", "101 Example Street");
            raddress.Add("addressLine2", "Line 2");
            raddress.Add("addressCity", "Bedford");
            raddress.Add("addressCounty", "Middlesex");
            raddress.Add("addressState", "Massachusetts");
            raddress.Add("addressZip", "01730");
            raddress.Add("addressCountry", "United States");
            SetterDeathRecord.Residence = raddress;
            Assert.Equal("101 Example Street", SetterDeathRecord.Residence["addressLine1"]);
            Assert.Equal("Line 2", SetterDeathRecord.Residence["addressLine2"]);
            Assert.Equal("Bedford", SetterDeathRecord.Residence["addressCity"]);
            Assert.Equal("Middlesex", SetterDeathRecord.Residence["addressCounty"]);
            Assert.Equal("Massachusetts", SetterDeathRecord.Residence["addressState"]);
            Assert.Equal("01730", SetterDeathRecord.Residence["addressZip"]);
            Assert.Equal("United States", SetterDeathRecord.Residence["addressCountry"]);
        }

        [Fact]
        public void Get_Residence()
        {
            // TODO
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
            // TODO
        }

        [Fact]
        public void Set_Ethnicity()
        {
            Tuple<string, string>[] ethnicity = { Tuple.Create("Hispanic or Latino", "2135-2"), Tuple.Create("Puerto Rican", "2180-8") };
            SetterDeathRecord.Ethnicity = ethnicity;
            Assert.Equal(ethnicity[0], SetterDeathRecord.Ethnicity[0]);
            Assert.Equal(ethnicity[1], SetterDeathRecord.Ethnicity[1]);
        }

        [Fact]
        public void Get_Ethnicity()
        {
            // TODO
        }

        [Fact]
        public void Set_Race()
        {
            Tuple<string, string>[] race = {Tuple.Create("White", "2106-3"), Tuple.Create("Native Hawaiian or Other Pacific Islander", "2076-8")};
            SetterDeathRecord.Race = race;
            Assert.Equal(race[0], SetterDeathRecord.Race[0]);
            Assert.Equal(race[1], SetterDeathRecord.Race[1]);
        }

        [Fact]
        public void Get_Race()
        {
            // TODO
        }

        [Fact]
        public void Set_PlaceOfBirth()
        {
            Dictionary<string, string> pobaddress = new Dictionary<string, string>();
            pobaddress.Add("addressLine1", "1011 Example Street");
            pobaddress.Add("addressLine2", "Line 2");
            pobaddress.Add("addressCity", "Bedford");
            pobaddress.Add("addressCounty", "Middlesex");
            pobaddress.Add("addressState", "Massachusetts");
            pobaddress.Add("addressZip", "01730");
            pobaddress.Add("addressCountry", "United States");
            SetterDeathRecord.PlaceOfBirth = pobaddress;
            Assert.Equal("1011 Example Street", SetterDeathRecord.PlaceOfBirth["addressLine1"]);
            Assert.Equal("Line 2", SetterDeathRecord.PlaceOfBirth["addressLine2"]);
            Assert.Equal("Bedford", SetterDeathRecord.PlaceOfBirth["addressCity"]);
            Assert.Equal("Middlesex", SetterDeathRecord.PlaceOfBirth["addressCounty"]);
            Assert.Equal("Massachusetts", SetterDeathRecord.PlaceOfBirth["addressState"]);
            Assert.Equal("01730", SetterDeathRecord.PlaceOfBirth["addressZip"]);
            Assert.Equal("United States", SetterDeathRecord.PlaceOfBirth["addressCountry"]);
        }

        [Fact]
        public void Get_PlaceOfBirth()
        {
            // TODO
        }

        [Fact]
        public void Set_MaritalStatus()
        {
            Dictionary<string, string> code = new Dictionary<string, string>();
            code.Add("code", "S");
            code.Add("system", "http://hl7.org/fhir/v3/MaritalStatus");
            code.Add("display", "Never Married");
            SetterDeathRecord.MaritalStatus = code;
            Assert.Equal("S", SetterDeathRecord.MaritalStatus["code"]);
            Assert.Equal("http://hl7.org/fhir/v3/MaritalStatus", SetterDeathRecord.MaritalStatus["system"]);
            Assert.Equal("Never Married", SetterDeathRecord.MaritalStatus["display"]);
        }

        [Fact]
        public void Get_MaritalStatus()
        {
            // TODO
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
            // TODO
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
            // TODO
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
            // TODO
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
            // TODO
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
            // TODO
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
            // TODO
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
            // TODO
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
            // TODO
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
            // TODO
        }

        [Fact]
        public void Set_EducationLevel()
        {
            Dictionary<string, string> elevel = new Dictionary<string, string>();
            elevel.Add("code", "BD");
            elevel.Add("system", "http://hl7.org/fhir/v3/EducationLevel");
            elevel.Add("display", "College or baccalaureate degree complete");
            SetterDeathRecord.EducationLevel = elevel;
            Assert.Equal("BD", SetterDeathRecord.EducationLevel["code"]);
            Assert.Equal("http://hl7.org/fhir/v3/EducationLevel", SetterDeathRecord.EducationLevel["system"]);
            Assert.Equal("College or baccalaureate degree complete", SetterDeathRecord.EducationLevel["display"]);
        }

        [Fact]
        public void Get_EducationLevel()
        {
            // TODO
        }

        [Fact]
        public void Set_BirthRecordId()
        {
            SetterDeathRecord.BirthRecordId = "4242123";
            Assert.Equal("4242123", SetterDeathRecord.BirthRecordId);
        }

        [Fact]
        public void Get_BirthRecordId()
        {
            // TODO
        }

        [Fact]
        public void Set_UsualOccupation()
        {
            Dictionary<string, string> uocc = new Dictionary<string, string>();
            uocc.Add("code", "7280");
            uocc.Add("system", "http://www.hl7.org/fhir/ValueSet/Usual-occupation");
            uocc.Add("display", "Accounting, tax preparation, bookkeeping, and payroll services");
            SetterDeathRecord.UsualOccupation = uocc;
            Assert.Equal("7280", SetterDeathRecord.UsualOccupation["code"]);
            Assert.Equal("http://www.hl7.org/fhir/ValueSet/Usual-occupation", SetterDeathRecord.UsualOccupation["system"]);
            Assert.Equal("Accounting, tax preparation, bookkeeping, and payroll services", SetterDeathRecord.UsualOccupation["display"]);
        }

        [Fact]
        public void Get_UsualOccupation()
        {
            // TODO
        }

        [Fact]
        public void Set_UsualIndustry()
        {
            Dictionary<string, string> uind = new Dictionary<string, string>();
            uind.Add("code", "1320");
            uind.Add("system", "http://www.hl7.org/fhir/ValueSet/industry-cdc-census-2010");
            uind.Add("display", "Aerospace engineers");
            SetterDeathRecord.UsualIndustry = uind;
            Assert.Equal("1320", SetterDeathRecord.UsualIndustry["code"]);
            Assert.Equal("http://www.hl7.org/fhir/ValueSet/industry-cdc-census-2010", SetterDeathRecord.UsualIndustry["system"]);
            Assert.Equal("Aerospace engineers", SetterDeathRecord.UsualIndustry["display"]);
        }

        [Fact]
        public void Get_UsualIndustry()
        {
            // TODO
        }

        [Fact]
        public void Set_MilitaryService()
        {
            Dictionary<string, string> mserv = new Dictionary<string, string>();
            mserv.Add("code", "Y");
            mserv.Add("system", "http://www.hl7.org/fhir/ValueSet/v2-0532");
            mserv.Add("display", "Yes");
            SetterDeathRecord.MilitaryService = mserv;
            Assert.Equal("Y", SetterDeathRecord.MilitaryService["code"]);
            Assert.Equal("http://www.hl7.org/fhir/ValueSet/v2-0532", SetterDeathRecord.MilitaryService["system"]);
            Assert.Equal("Yes", SetterDeathRecord.MilitaryService["display"]);
        }

        [Fact]
        public void Get_MilitaryService()
        {
            // TODO
        }

        [Fact]
        public void Set_MorticianGivenNames()
        {
            string[] fdnames = { "FD", "Middle" };
            SetterDeathRecord.MorticianGivenNames = fdnames;
            Assert.Equal("FD", SetterDeathRecord.MorticianGivenNames[0]);
            Assert.Equal("Middle", SetterDeathRecord.MorticianGivenNames[1]);
        }

        [Fact]
        public void Get_MorticianGivenNames()
        {
            // TODO
        }

        [Fact]
        public void Set_MorticianFamilyName()
        {
            SetterDeathRecord.MorticianFamilyName = "Last";
            Assert.Equal("Last", SetterDeathRecord.MorticianFamilyName);
        }

        [Fact]
        public void Get_MorticianFamilyName()
        {
            // TODO
        }

        [Fact]
        public void Set_MorticianSuffix()
        {
            SetterDeathRecord.MorticianSuffix = "Sr.";
            Assert.Equal("Sr.", SetterDeathRecord.MorticianSuffix);
        }

        [Fact]
        public void Get_MorticianSuffix()
        {
            // TODO
        }

        [Fact]
        public void Set_MorticianIdentifier()
        {
            SetterDeathRecord.MorticianIdentifier = "9876543210";
            Assert.Equal("9876543210", SetterDeathRecord.MorticianIdentifier);
        }

        [Fact]
        public void Get_MorticianIdentifier()
        {
            // TODO
        }

        [Fact]
        public void Set_FuneralHomeAddress()
        {
            Dictionary<string, string> fdaddress = new Dictionary<string, string>();
            fdaddress.Add("addressLine1", "1011010 Example Street");
            fdaddress.Add("addressLine2", "Line 2");
            fdaddress.Add("addressCity", "Bedford");
            fdaddress.Add("addressCounty", "Middlesex");
            fdaddress.Add("addressState", "Massachusetts");
            fdaddress.Add("addressZip", "01730");
            fdaddress.Add("addressCountry", "United States");
            SetterDeathRecord.FuneralHomeAddress = fdaddress;
            Assert.Equal("1011010 Example Street", SetterDeathRecord.FuneralHomeAddress["addressLine1"]);
            Assert.Equal("Line 2", SetterDeathRecord.FuneralHomeAddress["addressLine2"]);
            Assert.Equal("Bedford", SetterDeathRecord.FuneralHomeAddress["addressCity"]);
            Assert.Equal("Middlesex", SetterDeathRecord.FuneralHomeAddress["addressCounty"]);
            Assert.Equal("Massachusetts", SetterDeathRecord.FuneralHomeAddress["addressState"]);
            Assert.Equal("01730", SetterDeathRecord.FuneralHomeAddress["addressZip"]);
            Assert.Equal("United States", SetterDeathRecord.FuneralHomeAddress["addressCountry"]);
        }

        [Fact]
        public void Get_FuneralHomeAddress()
        {
            // TODO
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
            // TODO
        }

        [Fact]
        public void Set_DispositionLocationAddress()
        {
            Dictionary<string, string> dladdress = new Dictionary<string, string>();
            dladdress.Add("addressLine1", "999 Example Street");
            dladdress.Add("addressLine2", "Line 2");
            dladdress.Add("addressCity", "Bedford");
            dladdress.Add("addressCounty", "Middlesex");
            dladdress.Add("addressState", "Massachusetts");
            dladdress.Add("addressZip", "01730");
            dladdress.Add("addressCountry", "United States");
            SetterDeathRecord.DispositionLocationAddress = dladdress;
            Assert.Equal("999 Example Street", SetterDeathRecord.DispositionLocationAddress["addressLine1"]);
            Assert.Equal("Line 2", SetterDeathRecord.DispositionLocationAddress["addressLine2"]);
            Assert.Equal("Bedford", SetterDeathRecord.DispositionLocationAddress["addressCity"]);
            Assert.Equal("Middlesex", SetterDeathRecord.DispositionLocationAddress["addressCounty"]);
            Assert.Equal("Massachusetts", SetterDeathRecord.DispositionLocationAddress["addressState"]);
            Assert.Equal("01730", SetterDeathRecord.DispositionLocationAddress["addressZip"]);
            Assert.Equal("United States", SetterDeathRecord.DispositionLocationAddress["addressCountry"]);
        }

        [Fact]
        public void Get_DispositionLocationAddress()
        {
            // TODO
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
            // TODO
        }

        [Fact]
        public void Set_DecedentDispositionMethod()
        {
            Dictionary<string, string> ddm = new Dictionary<string, string>();
            ddm.Add("code", "449971000124106");
            ddm.Add("system", "http://snomed.info/sct");
            ddm.Add("display", "Burial");
            SetterDeathRecord.DecedentDispositionMethod = ddm;
            Assert.Equal("449971000124106", SetterDeathRecord.DecedentDispositionMethod["code"]);
            Assert.Equal("http://snomed.info/sct", SetterDeathRecord.DecedentDispositionMethod["system"]);
            Assert.Equal("Burial", SetterDeathRecord.DecedentDispositionMethod["display"]);
        }

        [Fact]
        public void Get_DecedentDispositionMethod()
        {
            // TODO
        }

        [Fact]
        public void Set_AutopsyPerformedIndicator()
        {
            Dictionary<string, string> api = new Dictionary<string, string>();
            api.Add("code", "Y");
            api.Add("system", "http://www.hl7.org/fhir/ValueSet/v2-0532");
            api.Add("display", "Yes");
            SetterDeathRecord.AutopsyPerformedIndicator = api;
            Assert.Equal("Y", SetterDeathRecord.AutopsyPerformedIndicator["code"]);
            Assert.Equal("http://www.hl7.org/fhir/ValueSet/v2-0532", SetterDeathRecord.AutopsyPerformedIndicator["system"]);
            Assert.Equal("Yes", SetterDeathRecord.AutopsyPerformedIndicator["display"]);
        }

        [Fact]
        public void Get_AutopsyPerformedIndicator()
        {
            // TODO
        }

        [Fact]
        public void Set_AutopsyResultsAvailable()
        {
            Dictionary<string, string> ara = new Dictionary<string, string>();
            ara.Add("code", "Y");
            ara.Add("system", "http://www.hl7.org/fhir/ValueSet/v2-0532");
            ara.Add("display", "Yes");
            SetterDeathRecord.AutopsyResultsAvailable = ara;
            Assert.Equal("Y", SetterDeathRecord.AutopsyResultsAvailable["code"]);
            Assert.Equal("http://www.hl7.org/fhir/ValueSet/v2-0532", SetterDeathRecord.AutopsyResultsAvailable["system"]);
            Assert.Equal("Yes", SetterDeathRecord.AutopsyResultsAvailable["display"]);
        }

        [Fact]
        public void Get_AutopsyResultsAvailable()
        {
            // TODO
        }

        [Fact]
        public void Set_AgeAtDeath()
        {
            Dictionary<string, string> aad = new Dictionary<string, string>();
            aad.Add("unit", "a");
            aad.Add("value", "100");
            SetterDeathRecord.AgeAtDeath = aad;
            Assert.Equal("a", SetterDeathRecord.AgeAtDeath["unit"]);
            Assert.Equal("100", SetterDeathRecord.AgeAtDeath["value"]);
        }

        [Fact]
        public void Get_AgeAtDeath()
        {
            // TODO
        }

        [Fact]
        public void Set_PregnanacyStatus()
        {
            Dictionary<string, string> ps = new Dictionary<string, string>();
            ps.Add("code", "PHC1260");
            ps.Add("system", "http://www.hl7.org/fhir/stu3/valueset-PregnancyStatusVS");
            ps.Add("display", "Not pregnant within past year");
            SetterDeathRecord.PregnanacyStatus = ps;
            Assert.Equal("PHC1260", SetterDeathRecord.PregnanacyStatus["code"]);
            Assert.Equal("http://www.hl7.org/fhir/stu3/valueset-PregnancyStatusVS", SetterDeathRecord.PregnanacyStatus["system"]);
            Assert.Equal("Not pregnant within past year", SetterDeathRecord.PregnanacyStatus["display"]);
        }

        [Fact]
        public void Get_PregnanacyStatus()
        {
            // TODO
        }

        [Fact]
        public void Set_TransportationRole()
        {
            Dictionary<string, string> tr = new Dictionary<string, string>();
            tr.Add("code", "example-code");
            tr.Add("system", "http://www.hl7.org/fhir/stu3/valueset-TransportationRelationships");
            tr.Add("display", "Example Code");
            SetterDeathRecord.TransportationRole = tr;
            Assert.Equal("example-code", SetterDeathRecord.TransportationRole["code"]);
            Assert.Equal("http://www.hl7.org/fhir/stu3/valueset-TransportationRelationships", SetterDeathRecord.TransportationRole["system"]);
            Assert.Equal("Example Code", SetterDeathRecord.TransportationRole["display"]);
        }

        [Fact]
        public void Get_TransportationRole()
        {
            // TODO
        }

        [Fact]
        public void Set_ExaminerContacted()
        {
            SetterDeathRecord.ExaminerContacted = true;
            Assert.True(SetterDeathRecord.ExaminerContacted);
            SetterDeathRecord.ExaminerContacted = false;
            Assert.False(SetterDeathRecord.ExaminerContacted);
        }

        [Fact]
        public void Get_ExaminerContacted()
        {
            // TODO
        }

        [Fact]
        public void Set_TobaccoUse()
        {
            Dictionary<string, string> tbu = new Dictionary<string, string>();
            tbu.Add("code", "Y");
            tbu.Add("system", "http://www.hl7.org/fhir/ValueSet/v2-0532");
            tbu.Add("display", "Yes");
            SetterDeathRecord.TobaccoUse = tbu;
            Assert.Equal("Y", SetterDeathRecord.TobaccoUse["code"]);
            Assert.Equal("http://www.hl7.org/fhir/ValueSet/v2-0532", SetterDeathRecord.TobaccoUse["system"]);
            Assert.Equal("Yes", SetterDeathRecord.TobaccoUse["display"]);
        }

        [Fact]
        public void Get_TobaccoUse()
        {
            // TODO
        }

        [Fact]
        public void Set_InjuryLocationAddress()
        {
            Dictionary<string, string> iladdress = new Dictionary<string, string>();
            iladdress.Add("addressLine1", "99912 Example Street");
            iladdress.Add("addressLine2", "Line 2");
            iladdress.Add("addressCity", "Bedford");
            iladdress.Add("addressCounty", "Middlesex");
            iladdress.Add("addressState", "Massachusetts");
            iladdress.Add("addressZip", "01730");
            iladdress.Add("addressCountry", "United States");
            SetterDeathRecord.InjuryLocationAddress = iladdress;
            Assert.Equal("99912 Example Street", SetterDeathRecord.InjuryLocationAddress["addressLine1"]);
            Assert.Equal("Line 2", SetterDeathRecord.InjuryLocationAddress["addressLine2"]);
            Assert.Equal("Bedford", SetterDeathRecord.InjuryLocationAddress["addressCity"]);
            Assert.Equal("Middlesex", SetterDeathRecord.InjuryLocationAddress["addressCounty"]);
            Assert.Equal("Massachusetts", SetterDeathRecord.InjuryLocationAddress["addressState"]);
            Assert.Equal("01730", SetterDeathRecord.InjuryLocationAddress["addressZip"]);
            Assert.Equal("United States", SetterDeathRecord.InjuryLocationAddress["addressCountry"]);
        }

        [Fact]
        public void Get_InjuryLocationAddress()
        {
            // TODO
        }

        [Fact]
        public void Set_InjuryLocationName()
        {
            SetterDeathRecord.InjuryLocationName = "Example Injury Location Name";
            Assert.Equal("Example Injury Location Name", SetterDeathRecord.InjuryLocationName);
        }

        [Fact]
        public void Get_InjuryLocationName()
        {
            // TODO
        }

        [Fact]
        public void Set_InjuryLocationDescription()
        {
            SetterDeathRecord.InjuryLocationDescription = "Example Injury Location Description";
            Assert.Equal("Example Injury Location Description", SetterDeathRecord.InjuryLocationDescription);
        }

        [Fact]
        public void Get_InjuryLocationDescription()
        {
            // TODO
        }

        [Fact]
        public void Set_DeathLocationAddress()
        {
            Dictionary<string, string> dtladdress = new Dictionary<string, string>();
            dtladdress.Add("addressLine1", "671 Example Street");
            dtladdress.Add("addressLine2", "Line 2");
            dtladdress.Add("addressCity", "Bedford");
            dtladdress.Add("addressCounty", "Middlesex");
            dtladdress.Add("addressState", "Massachusetts");
            dtladdress.Add("addressZip", "01730");
            dtladdress.Add("addressCountry", "United States");
            SetterDeathRecord.DeathLocationAddress = dtladdress;
            Assert.Equal("671 Example Street", SetterDeathRecord.DeathLocationAddress["addressLine1"]);
            Assert.Equal("Line 2", SetterDeathRecord.DeathLocationAddress["addressLine2"]);
            Assert.Equal("Bedford", SetterDeathRecord.DeathLocationAddress["addressCity"]);
            Assert.Equal("Middlesex", SetterDeathRecord.DeathLocationAddress["addressCounty"]);
            Assert.Equal("Massachusetts", SetterDeathRecord.DeathLocationAddress["addressState"]);
            Assert.Equal("01730", SetterDeathRecord.DeathLocationAddress["addressZip"]);
            Assert.Equal("United States", SetterDeathRecord.DeathLocationAddress["addressCountry"]);
        }

        [Fact]
        public void Get_DeathLocationAddress()
        {
            // TODO
        }

        [Fact]
        public void Set_DeathLocationName()
        {
            SetterDeathRecord.DeathLocationName = "Example Death Location Name";
            Assert.Equal("Example Death Location Name", SetterDeathRecord.DeathLocationName);
        }

        [Fact]
        public void Get_DeathLocationName()
        {
            // TODO
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
            // TODO
        }

        [Fact]
        public void Get_DateOfDeath()
        {
            // TODO
        }

        [Fact]
        public void Set_DateOfDeath()
        {
            SetterDeathRecord.DateOfDeath = "2019-01-30T16:48:07.4988220-05:00";
            Assert.Equal("2019-01-30T16:48:07.4988220-05:00", SetterDeathRecord.DateOfDeath);
        }

        [Fact]
        public void Get_DateOfDeathPronouncement()
        {
            // TODO
        }

        [Fact]
        public void Set_DateOfDeathPronouncement()
        {
            SetterDeathRecord.DateOfDeathPronouncement = "2019-01-31T17:48:07.4988220-05:00";
            Assert.Equal("2019-01-31T17:48:07.4988220-05:00", SetterDeathRecord.DateOfDeathPronouncement);
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
