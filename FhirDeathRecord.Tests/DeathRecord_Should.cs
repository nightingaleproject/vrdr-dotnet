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

        // [Fact]
        // public void SetPatientAfterParse()
        // {
        //     DeathRecord sample1 = new DeathRecord(File.ReadAllText(FixturePath("fixtures/xml/1.xml")));
        //     DeathRecord sample2 = new DeathRecord(File.ReadAllText(FixturePath("fixtures/json/1.json")));
        //     Assert.Equal("Person", sample1.FamilyName);
        //     Assert.Equal("Person", sample2.FamilyName);
        //     sample1.FamilyName = "1changed2abc";
        //     sample2.FamilyName = "2changed1xyz";
        //     Assert.Equal("1changed2abc", sample1.FamilyName);
        //     Assert.Equal("2changed1xyz", sample2.FamilyName);
        // }

        // [Fact]
        // public void SetPractitionerAfterParse()
        // {
        //     DeathRecord sample1 = new DeathRecord(File.ReadAllText(FixturePath("fixtures/xml/1.xml")));
        //     DeathRecord sample2 = new DeathRecord(File.ReadAllText(FixturePath("fixtures/json/1.json")));
        //     Assert.Equal("Doctor", sample1.CertifierFamilyName);
        //     Assert.Equal("Doctor", sample2.CertifierFamilyName);
        //     sample1.CertifierFamilyName = "1diff2abc";
        //     sample2.CertifierFamilyName = "2diff1xyz";
        //     Assert.Equal("1diff2abc", sample1.CertifierFamilyName);
        //     Assert.Equal("2diff1xyz", sample2.CertifierFamilyName);
        // }

        [Fact]
        public void Set_Identifier()
        {
            SetterDeathRecord.Identifier = "1337";
            Assert.Equal("1337", SetterDeathRecord.Identifier);
        }

        [Fact]
        public void Get_Identifier()
        {
            // TODO
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
            // TODO
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
