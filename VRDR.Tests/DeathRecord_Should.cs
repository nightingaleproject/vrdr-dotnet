using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using Xunit;

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
            XMLRecords.Add(new DeathRecord(File.ReadAllText(FixturePath("fixtures/xml/1.xml"))));
            JSONRecords = new ArrayList();
            JSONRecords.Add(new DeathRecord(File.ReadAllText(FixturePath("fixtures/json/1.json"))));
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
        public void FailMissingComposition()
        {
            string bundle = File.ReadAllText(FixturePath("fixtures/json/MissingComposition.json"));
            Exception ex = Assert.Throws<System.ArgumentException>(() => new DeathRecord(bundle));
            Assert.Equal("Failed to find a Composition. The first entry in the FHIR Bundle should be a Composition.", ex.Message);
        }

        [Fact]
        public void FailMissingCompositionSubject()
        {
            string bundle = File.ReadAllText(FixturePath("fixtures/json/MissingCompositionSubject.json"));
            Exception ex = Assert.Throws<System.ArgumentException>(() => new DeathRecord(bundle));
            Assert.Equal("The Composition is missing a subject (a reference to the Decedent resource).", ex.Message);
        }

        [Fact]
        public void FailMissingCompositionAttestor()
        {
            string bundle = File.ReadAllText(FixturePath("fixtures/json/MissingCompositionAttestor.json"));
            Exception ex = Assert.Throws<System.ArgumentException>(() => new DeathRecord(bundle));
            Assert.Equal("The Composition is missing an attestor (a reference to the Certifier/Practitioner resource).", ex.Message);
        }

        [Fact]
        public void FailMissingDecedent()
        {
            string bundle = File.ReadAllText(FixturePath("fixtures/json/MissingDecedent.json"));
            Exception ex = Assert.Throws<System.ArgumentException>(() => new DeathRecord(bundle));
            Assert.Equal("Failed to find a Decedent (Patient). The second entry in the FHIR Bundle is usually the Decedent (Patient).", ex.Message);
        }

        [Fact]
        public void FailMissingCertifier()
        {
            string bundle = File.ReadAllText(FixturePath("fixtures/json/MissingCertifier.json"));
            Exception ex = Assert.Throws<System.ArgumentException>(() => new DeathRecord(bundle));
            Assert.Equal("Failed to find a Certifier (Practitioner). The third entry in the FHIR Bundle is usually the Certifier (Practitioner). Either the Certifier is missing from the Bundle, or the attestor reference specified in the Composition is incorrect.", ex.Message);
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
        public void FailBadConditions()
        {
            string bundle = File.ReadAllText(FixturePath("fixtures/json/BadConditions.json"));
            Exception ex = Assert.Throws<System.ArgumentException>(() => new DeathRecord(bundle));
            Assert.Equal("There are multiple Condition Contributing to Death resources present. Condition Contributing to Death resources are identified by not being referenced in the Cause of Death Pathway resource, so please confirm that all Cause of Death Conditions are correctly referenced in the Cause of Death Pathway to ensure they are not mistaken for a Condition Contributing to Death resource.", ex.Message);
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
            string ije = new IJEMortality(deathRecord).ToString();
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
            Assert.Equal(first.CODE1A, second.CODE1A);
            Assert.Equal(first.CertifierAddress, second.CertifierAddress);
            Assert.Equal(first.CausesOfDeath, second.CausesOfDeath);
        }

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
            Assert.Equal("1", ((DeathRecord)JSONRecords[0]).Identifier);
            Assert.Equal("1", ((DeathRecord)XMLRecords[0]).Identifier);
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
            Assert.Equal("42", ((DeathRecord)JSONRecords[0]).BundleIdentifier);
            Assert.Equal("42", ((DeathRecord)XMLRecords[0]).BundleIdentifier);
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
        public void Set_CertificationRole()
        {
            Dictionary<string, string> CertificationRole = new Dictionary<string, string>();
            CertificationRole.Add("code", "434641000124105");
            CertificationRole.Add("system", "http://snomed.info/sct");
            CertificationRole.Add("display", "Physician");
            SetterDeathRecord.CertificationRole = CertificationRole;
            Assert.Equal("434641000124105", SetterDeathRecord.CertificationRole["code"]);
            Assert.Equal("http://snomed.info/sct", SetterDeathRecord.CertificationRole["system"]);
            Assert.Equal("Physician", SetterDeathRecord.CertificationRole["display"]);
        }

        [Fact]
        public void Get_CertificationRole()
        {
            Assert.Equal("434641000124105", ((DeathRecord)JSONRecords[0]).CertificationRole["code"]);
            Assert.Equal("http://snomed.info/sct", ((DeathRecord)XMLRecords[0]).CertificationRole["system"]);
            Assert.Equal("Physician", ((DeathRecord)JSONRecords[0]).CertificationRole["display"]);
            Assert.Equal("434641000124105", ((DeathRecord)XMLRecords[0]).CertificationRole["code"]);
            Assert.Equal("http://snomed.info/sct", ((DeathRecord)JSONRecords[0]).CertificationRole["system"]);
            Assert.Equal("Physician", ((DeathRecord)XMLRecords[0]).CertificationRole["display"]);
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
            Assert.Equal("1010101", ((DeathRecord)JSONRecords[0]).InterestedPartyIdentifier);
            Assert.Equal("1010101", ((DeathRecord)XMLRecords[0]).InterestedPartyIdentifier);
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
            Assert.Equal("Example Hospital", ((DeathRecord)JSONRecords[0]).InterestedPartyName);
            Assert.Equal("Example Hospital", ((DeathRecord)XMLRecords[0]).InterestedPartyName);
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
            Assert.Equal("10 Example Street", ((DeathRecord)JSONRecords[0]).InterestedPartyAddress["addressLine1"]);
            Assert.Equal("Line 2", ((DeathRecord)JSONRecords[0]).InterestedPartyAddress["addressLine2"]);
            Assert.Equal("Bedford", ((DeathRecord)JSONRecords[0]).InterestedPartyAddress["addressCity"]);
            Assert.Equal("Middlesex", ((DeathRecord)JSONRecords[0]).InterestedPartyAddress["addressCounty"]);
            Assert.Equal("Massachusetts", ((DeathRecord)JSONRecords[0]).InterestedPartyAddress["addressState"]);
            Assert.Equal("01730", ((DeathRecord)JSONRecords[0]).InterestedPartyAddress["addressZip"]);
            Assert.Equal("United States", ((DeathRecord)JSONRecords[0]).InterestedPartyAddress["addressCountry"]);
            Assert.Equal("10 Example Street", ((DeathRecord)XMLRecords[0]).InterestedPartyAddress["addressLine1"]);
            Assert.Equal("Line 2", ((DeathRecord)XMLRecords[0]).InterestedPartyAddress["addressLine2"]);
            Assert.Equal("Bedford", ((DeathRecord)XMLRecords[0]).InterestedPartyAddress["addressCity"]);
            Assert.Equal("Middlesex", ((DeathRecord)XMLRecords[0]).InterestedPartyAddress["addressCounty"]);
            Assert.Equal("Massachusetts", ((DeathRecord)XMLRecords[0]).InterestedPartyAddress["addressState"]);
            Assert.Equal("01730", ((DeathRecord)XMLRecords[0]).InterestedPartyAddress["addressZip"]);
            Assert.Equal("United States", ((DeathRecord)XMLRecords[0]).InterestedPartyAddress["addressCountry"]);
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
            Assert.Equal("prov", ((DeathRecord)JSONRecords[0]).InterestedPartyType["code"]);
            Assert.Equal("http://terminology.hl7.org/CodeSystem/organization-type", ((DeathRecord)XMLRecords[0]).InterestedPartyType["system"]);
            Assert.Equal("Healthcare Provider", ((DeathRecord)JSONRecords[0]).InterestedPartyType["display"]);
            Assert.Equal("prov", ((DeathRecord)XMLRecords[0]).InterestedPartyType["code"]);
            Assert.Equal("http://terminology.hl7.org/CodeSystem/organization-type", ((DeathRecord)JSONRecords[0]).InterestedPartyType["system"]);
            Assert.Equal("Healthcare Provider", ((DeathRecord)XMLRecords[0]).InterestedPartyType["display"]);
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
            Assert.Equal("7878000", ((DeathRecord)JSONRecords[0]).MannerOfDeathType["code"]);
            Assert.Equal("Accident", ((DeathRecord)JSONRecords[0]).MannerOfDeathType["display"]);
            Assert.Equal("7878000", ((DeathRecord)XMLRecords[0]).MannerOfDeathType["code"]);
            Assert.Equal("Accident", ((DeathRecord)XMLRecords[0]).MannerOfDeathType["display"]);
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
            Assert.Equal("11 Example Street", ((DeathRecord)JSONRecords[0]).CertifierAddress["addressLine1"]);
            Assert.Equal("Line 2", ((DeathRecord)JSONRecords[0]).CertifierAddress["addressLine2"]);
            Assert.Equal("Bedford", ((DeathRecord)JSONRecords[0]).CertifierAddress["addressCity"]);
            Assert.Equal("Middlesex", ((DeathRecord)JSONRecords[0]).CertifierAddress["addressCounty"]);
            Assert.Equal("Massachusetts", ((DeathRecord)JSONRecords[0]).CertifierAddress["addressState"]);
            Assert.Equal("01730", ((DeathRecord)JSONRecords[0]).CertifierAddress["addressZip"]);
            Assert.Equal("United States", ((DeathRecord)JSONRecords[0]).CertifierAddress["addressCountry"]);
            Assert.Equal("11 Example Street", ((DeathRecord)XMLRecords[0]).CertifierAddress["addressLine1"]);
            Assert.Equal("Line 2", ((DeathRecord)XMLRecords[0]).CertifierAddress["addressLine2"]);
            Assert.Equal("Bedford", ((DeathRecord)XMLRecords[0]).CertifierAddress["addressCity"]);
            Assert.Equal("Middlesex", ((DeathRecord)XMLRecords[0]).CertifierAddress["addressCounty"]);
            Assert.Equal("Massachusetts", ((DeathRecord)XMLRecords[0]).CertifierAddress["addressState"]);
            Assert.Equal("01730", ((DeathRecord)XMLRecords[0]).CertifierAddress["addressZip"]);
            Assert.Equal("United States", ((DeathRecord)XMLRecords[0]).CertifierAddress["addressCountry"]);
        }

        [Fact]
        public void Set_CertifierQualification()
        {
            Dictionary<string, string> qualification = new Dictionary<string, string>();
            qualification.Add("code", "3060");
            qualification.Add("system", "urn:oid:2.16.840.1.114222.4.11.7186");
            qualification.Add("display", "Physicians and surgeons");
            SetterDeathRecord.CertifierQualification = qualification;
            Assert.Equal("3060", SetterDeathRecord.CertifierQualification["code"]);
            Assert.Equal("urn:oid:2.16.840.1.114222.4.11.7186", SetterDeathRecord.CertifierQualification["system"]);
            Assert.Equal("Physicians and surgeons", SetterDeathRecord.CertifierQualification["display"]);
        }

        [Fact]
        public void Get_CertifierQualification()
        {
            Assert.Equal("3060", ((DeathRecord)JSONRecords[0]).CertifierQualification["code"]);
            Assert.Equal("urn:oid:2.16.840.1.114222.4.11.7186", ((DeathRecord)XMLRecords[0]).CertifierQualification["system"]);
            Assert.Equal("Physicians and surgeons", ((DeathRecord)JSONRecords[0]).CertifierQualification["display"]);
            Assert.Equal("3060", ((DeathRecord)XMLRecords[0]).CertifierQualification["code"]);
            Assert.Equal("urn:oid:2.16.840.1.114222.4.11.7186", ((DeathRecord)JSONRecords[0]).CertifierQualification["system"]);
            Assert.Equal("Physicians and surgeons", ((DeathRecord)XMLRecords[0]).CertifierQualification["display"]);
        }

        [Fact]
        public void Set_CertifierLicenseNumber()
        {
            SetterDeathRecord.CertifierLicenseNumber = "789123456";
            Assert.Equal("789123456", SetterDeathRecord.CertifierLicenseNumber);
        }

        [Fact]
        public void Get_CertifierLicenseNumber()
        {
            Assert.Equal("789123456", ((DeathRecord)JSONRecords[0]).CertifierLicenseNumber);
            Assert.Equal("789123456", ((DeathRecord)XMLRecords[0]).CertifierLicenseNumber);
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
            Assert.Equal("Example Contributing Conditions", ((DeathRecord)JSONRecords[0]).ContributingConditions);
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
            Assert.Equal("Rupture of myocardium", ((DeathRecord)JSONRecords[0]).COD1A);
            Assert.Equal("Rupture of myocardium", ((DeathRecord)XMLRecords[0]).COD1A);
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
            Assert.Equal("minutes", ((DeathRecord)JSONRecords[0]).INTERVAL1A);
            Assert.Equal("minutes", ((DeathRecord)XMLRecords[0]).INTERVAL1A);
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
            Assert.Equal("I21.0", ((DeathRecord)JSONRecords[0]).CODE1A["code"]);
            Assert.Equal("http://hl7.org/fhir/sid/icd-10", ((DeathRecord)XMLRecords[0]).CODE1A["system"]);
            Assert.Equal("Acute transmural myocardial infarction of anterior wall", ((DeathRecord)JSONRecords[0]).CODE1A["display"]);
            Assert.Equal("I21.0", ((DeathRecord)XMLRecords[0]).CODE1A["code"]);
            Assert.Equal("http://hl7.org/fhir/sid/icd-10", ((DeathRecord)JSONRecords[0]).CODE1A["system"]);
            Assert.Equal("Acute transmural myocardial infarction of anterior wall", ((DeathRecord)XMLRecords[0]).CODE1A["display"]);
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
            Assert.Equal("Acute myocardial infarction", ((DeathRecord)JSONRecords[0]).COD1B);
            Assert.Equal("Acute myocardial infarction", ((DeathRecord)XMLRecords[0]).COD1B);
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
            Assert.Equal("6 days", ((DeathRecord)JSONRecords[0]).INTERVAL1B);
            Assert.Equal("6 days", ((DeathRecord)XMLRecords[0]).INTERVAL1B);
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
            Assert.Equal("I21.9", ((DeathRecord)JSONRecords[0]).CODE1B["code"]);
            Assert.Equal("http://hl7.org/fhir/sid/icd-10", ((DeathRecord)XMLRecords[0]).CODE1B["system"]);
            Assert.Equal("Acute myocardial infarction, unspecified", ((DeathRecord)JSONRecords[0]).CODE1B["display"]);
            Assert.Equal("I21.9", ((DeathRecord)XMLRecords[0]).CODE1B["code"]);
            Assert.Equal("http://hl7.org/fhir/sid/icd-10", ((DeathRecord)JSONRecords[0]).CODE1B["system"]);
            Assert.Equal("Acute myocardial infarction, unspecified", ((DeathRecord)XMLRecords[0]).CODE1B["display"]);
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
            Assert.Equal("Coronary artery thrombosis", ((DeathRecord)JSONRecords[0]).COD1C);
            Assert.Equal("Coronary artery thrombosis", ((DeathRecord)XMLRecords[0]).COD1C);
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
            Assert.Equal("5 years", ((DeathRecord)JSONRecords[0]).INTERVAL1C);
            Assert.Equal("5 years", ((DeathRecord)XMLRecords[0]).INTERVAL1C);
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
        public void Set_COD1D()
        {
            SetterDeathRecord.COD1D = "cause 4";
            Assert.Equal("cause 4", SetterDeathRecord.COD1D);
        }

        [Fact]
        public void Get_COD1D()
        {
            Assert.Equal("Atherosclerotic coronary artery disease", ((DeathRecord)JSONRecords[0]).COD1D);
            Assert.Equal("Atherosclerotic coronary artery disease", ((DeathRecord)XMLRecords[0]).COD1D);
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
            Assert.Equal("7 years", ((DeathRecord)JSONRecords[0]).INTERVAL1D);
            Assert.Equal("7 years", ((DeathRecord)XMLRecords[0]).INTERVAL1D);
        }

        [Fact]
        public void Set_CODE1D()
        {
            Dictionary<string, string> code = new Dictionary<string, string>();
            code.Add("code", "code 4");
            code.Add("system", "system 4");
            code.Add("display", "display 4");
            SetterDeathRecord.CODE1D = code;
            Assert.Equal("code 4", SetterDeathRecord.CODE1D["code"]);
            Assert.Equal("system 4", SetterDeathRecord.CODE1D["system"]);
            Assert.Equal("display 4", SetterDeathRecord.CODE1D["display"]);
        }

        [Fact]
        public void Set_COD1E()
        {
            SetterDeathRecord.COD1E = "exampleE";
            Assert.Equal("exampleE", SetterDeathRecord.COD1E);
        }

        [Fact]
        public void Set_INTERVAL1E()
        {
            SetterDeathRecord.INTERVAL1E = "exampleE";
            Assert.Equal("exampleE", SetterDeathRecord.INTERVAL1E);
        }

        [Fact]
        public void Set_CODE1E()
        {
            Dictionary<string, string> code = new Dictionary<string, string>();
            code.Add("code", "exampleE");
            code.Add("system", "exampleE");
            code.Add("display", "exampleE");
            SetterDeathRecord.CODE1E = code;
            Assert.Equal("exampleE", SetterDeathRecord.CODE1E["code"]);
            Assert.Equal("exampleE", SetterDeathRecord.CODE1E["system"]);
            Assert.Equal("exampleE", SetterDeathRecord.CODE1E["display"]);
        }

        [Fact]
        public void Set_COD1F()
        {
            SetterDeathRecord.COD1F = "exampleF";
            Assert.Equal("exampleF", SetterDeathRecord.COD1F);
        }

        [Fact]
        public void Set_INTERVAL1F()
        {
            SetterDeathRecord.INTERVAL1F = "exampleF";
            Assert.Equal("exampleF", SetterDeathRecord.INTERVAL1F);
        }

        [Fact]
        public void Set_CODE1F()
        {
            Dictionary<string, string> code = new Dictionary<string, string>();
            code.Add("code", "exampleF");
            code.Add("system", "exampleF");
            code.Add("display", "exampleF");
            SetterDeathRecord.CODE1F = code;
            Assert.Equal("exampleF", SetterDeathRecord.CODE1F["code"]);
            Assert.Equal("exampleF", SetterDeathRecord.CODE1F["system"]);
            Assert.Equal("exampleF", SetterDeathRecord.CODE1F["display"]);
        }

        [Fact]
        public void Set_COD1G()
        {
            SetterDeathRecord.COD1G = "exampleG";
            Assert.Equal("exampleG", SetterDeathRecord.COD1G);
        }

        [Fact]
        public void Set_INTERVAL1G()
        {
            SetterDeathRecord.INTERVAL1G = "exampleG";
            Assert.Equal("exampleG", SetterDeathRecord.INTERVAL1G);
        }

        [Fact]
        public void Set_CODE1G()
        {
            Dictionary<string, string> code = new Dictionary<string, string>();
            code.Add("code", "exampleG");
            code.Add("system", "exampleG");
            code.Add("display", "exampleG");
            SetterDeathRecord.CODE1G = code;
            Assert.Equal("exampleG", SetterDeathRecord.CODE1G["code"]);
            Assert.Equal("exampleG", SetterDeathRecord.CODE1G["system"]);
            Assert.Equal("exampleG", SetterDeathRecord.CODE1G["display"]);
        }

        [Fact]
        public void Set_COD1H()
        {
            SetterDeathRecord.COD1H = "exampleH";
            Assert.Equal("exampleH", SetterDeathRecord.COD1H);
        }

        [Fact]
        public void Set_INTERVAL1H()
        {
            SetterDeathRecord.INTERVAL1H = "exampleH";
            Assert.Equal("exampleH", SetterDeathRecord.INTERVAL1H);
        }

        [Fact]
        public void Set_CODE1H()
        {
            Dictionary<string, string> code = new Dictionary<string, string>();
            code.Add("code", "exampleH");
            code.Add("system", "exampleH");
            code.Add("display", "exampleH");
            SetterDeathRecord.CODE1H = code;
            Assert.Equal("exampleH", SetterDeathRecord.CODE1H["code"]);
            Assert.Equal("exampleH", SetterDeathRecord.CODE1H["system"]);
            Assert.Equal("exampleH", SetterDeathRecord.CODE1H["display"]);
        }

        [Fact]
        public void Set_COD1I()
        {
            SetterDeathRecord.COD1I = "exampleI";
            Assert.Equal("exampleI", SetterDeathRecord.COD1I);
        }

        [Fact]
        public void Set_INTERVAL1I()
        {
            SetterDeathRecord.INTERVAL1I = "exampleI";
            Assert.Equal("exampleI", SetterDeathRecord.INTERVAL1I);
        }

        [Fact]
        public void Set_CODE1I()
        {
            Dictionary<string, string> code = new Dictionary<string, string>();
            code.Add("code", "exampleI");
            code.Add("system", "exampleI");
            code.Add("display", "exampleI");
            SetterDeathRecord.CODE1I = code;
            Assert.Equal("exampleI", SetterDeathRecord.CODE1I["code"]);
            Assert.Equal("exampleI", SetterDeathRecord.CODE1I["system"]);
            Assert.Equal("exampleI", SetterDeathRecord.CODE1I["display"]);
        }

        [Fact]
        public void Set_COD1J()
        {
            SetterDeathRecord.COD1J = "exampleJ";
            Assert.Equal("exampleJ", SetterDeathRecord.COD1J);
        }

        [Fact]
        public void Set_INTERVAL1J()
        {
            SetterDeathRecord.INTERVAL1J = "exampleJ";
            Assert.Equal("exampleJ", SetterDeathRecord.INTERVAL1J);
        }

        [Fact]
        public void Set_CODE1J()
        {
            Dictionary<string, string> code = new Dictionary<string, string>();
            code.Add("code", "exampleJ");
            code.Add("system", "exampleJ");
            code.Add("display", "exampleJ");
            SetterDeathRecord.CODE1J = code;
            Assert.Equal("exampleJ", SetterDeathRecord.CODE1J["code"]);
            Assert.Equal("exampleJ", SetterDeathRecord.CODE1J["system"]);
            Assert.Equal("exampleJ", SetterDeathRecord.CODE1J["display"]);
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
            Assert.Equal("Example", ((DeathRecord)JSONRecords[0]).GivenNames[0]);
            Assert.Equal("Something", ((DeathRecord)JSONRecords[0]).GivenNames[1]);
            Assert.Equal("Middle", ((DeathRecord)JSONRecords[0]).GivenNames[2]);
            Assert.Equal("Example", ((DeathRecord)XMLRecords[0]).GivenNames[0]);
            Assert.Equal("Something", ((DeathRecord)XMLRecords[0]).GivenNames[1]);
            Assert.Equal("Middle", ((DeathRecord)XMLRecords[0]).GivenNames[2]);
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
            Assert.Equal("Last", ((DeathRecord)JSONRecords[0]).FamilyName);
            Assert.Equal("Last", ((DeathRecord)XMLRecords[0]).FamilyName);
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

        [Fact]
        public void Set_AliasGivenNames()
        {
            SetterDeathRecord.AliasGivenNames = new string[] { "FirstNameAlias", "MiddleAlias" };
            Assert.Equal("FirstNameAlias", SetterDeathRecord.AliasGivenNames[0]);
            Assert.Equal("MiddleAlias", SetterDeathRecord.AliasGivenNames[1]);
        }

        [Fact]
        public void Get_AliasGivenNames()
        {
            Assert.Equal("FirstNameAlias", ((DeathRecord)JSONRecords[0]).AliasGivenNames[0]);
            Assert.Equal("MiddleAlias", ((DeathRecord)JSONRecords[0]).AliasGivenNames[1]);
            Assert.Equal("FirstNameAlias", ((DeathRecord)XMLRecords[0]).AliasGivenNames[0]);
            Assert.Equal("MiddleAlias", ((DeathRecord)XMLRecords[0]).AliasGivenNames[1]);
        }

        [Fact]
        public void Set_AliasFamilyName()
        {
            SetterDeathRecord.AliasFamilyName = "LastNameAlias";
            Assert.Equal("LastNameAlias", SetterDeathRecord.AliasFamilyName);
        }

        [Fact]
        public void Get_AliasFamilyName()
        {
            Assert.Equal("LastNameAlias", ((DeathRecord)JSONRecords[0]).AliasFamilyName);
            Assert.Equal("LastNameAlias", ((DeathRecord)XMLRecords[0]).AliasFamilyName);
        }

        [Fact]
        public void Set_AliasSuffix()
        {
            SetterDeathRecord.AliasSuffix = "Jr.";
            Assert.Equal("Jr.", SetterDeathRecord.AliasSuffix);
        }

        [Fact]
        public void Get_AliasSuffix()
        {
            Assert.Equal("Jr.", ((DeathRecord)JSONRecords[0]).AliasSuffix);
            Assert.Equal("Jr.", ((DeathRecord)XMLRecords[0]).AliasSuffix);
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
            Assert.Equal("male", ((DeathRecord)JSONRecords[0]).Gender);
            Assert.Equal("male", ((DeathRecord)XMLRecords[0]).Gender);
        }

        [Fact]
        public void Set_BirthSex()
        {
            SetterDeathRecord.BirthSex = "F";
            Assert.Equal("F", SetterDeathRecord.BirthSex);
        }

        [Fact]
        public void Get_BirthSex()
        {
            Assert.Equal("F", ((DeathRecord)JSONRecords[0]).BirthSex);
            Assert.Equal("F", ((DeathRecord)XMLRecords[0]).BirthSex);
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
            Assert.Equal("101 Example Street", ((DeathRecord)JSONRecords[0]).Residence["addressLine1"]);
            Assert.Equal("Line 2", ((DeathRecord)JSONRecords[0]).Residence["addressLine2"]);
            Assert.Equal("Bedford", ((DeathRecord)JSONRecords[0]).Residence["addressCity"]);
            Assert.Equal("Middlesex", ((DeathRecord)JSONRecords[0]).Residence["addressCounty"]);
            Assert.Equal("Massachusetts", ((DeathRecord)JSONRecords[0]).Residence["addressState"]);
            Assert.Equal("01730", ((DeathRecord)JSONRecords[0]).Residence["addressZip"]);
            Assert.Equal("United States", ((DeathRecord)JSONRecords[0]).Residence["addressCountry"]);
            Assert.Equal("101 Example Street", ((DeathRecord)XMLRecords[0]).Residence["addressLine1"]);
            Assert.Equal("Line 2", ((DeathRecord)XMLRecords[0]).Residence["addressLine2"]);
            Assert.Equal("Bedford", ((DeathRecord)XMLRecords[0]).Residence["addressCity"]);
            Assert.Equal("Middlesex", ((DeathRecord)XMLRecords[0]).Residence["addressCounty"]);
            Assert.Equal("Massachusetts", ((DeathRecord)XMLRecords[0]).Residence["addressState"]);
            Assert.Equal("01730", ((DeathRecord)XMLRecords[0]).Residence["addressZip"]);
            Assert.Equal("United States", ((DeathRecord)XMLRecords[0]).Residence["addressCountry"]);
        }

        [Fact]
        public void Get_ResidenceWithinCityLimits()
        {
            SetterDeathRecord.ResidenceWithinCityLimits = false;
            Assert.False(SetterDeathRecord.ResidenceWithinCityLimits);
        }

        [Fact]
        public void Set_ResidenceWithinCityLimits()
        {
            Assert.False(((DeathRecord)JSONRecords[0]).ResidenceWithinCityLimits);
            Assert.False(((DeathRecord)XMLRecords[0]).ResidenceWithinCityLimits);
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
            Assert.Equal("123456789", ((DeathRecord)JSONRecords[0]).SSN);
            Assert.Equal("123456789", ((DeathRecord)XMLRecords[0]).SSN);
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
            Assert.Equal(Tuple.Create("Hispanic or Latino", "2135-2"), ((DeathRecord)JSONRecords[0]).Ethnicity[0]);
            Assert.Equal(Tuple.Create("Puerto Rican", "2180-8"), ((DeathRecord)JSONRecords[0]).Ethnicity[1]);
            Assert.Equal(Tuple.Create("Hispanic or Latino", "2135-2"), ((DeathRecord)XMLRecords[0]).Ethnicity[0]);
            Assert.Equal(Tuple.Create("Puerto Rican", "2180-8"), ((DeathRecord)XMLRecords[0]).Ethnicity[1]);
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
            Assert.Equal(Tuple.Create("White", "2106-3"), ((DeathRecord)JSONRecords[0]).Race[0]);
            Assert.Equal(Tuple.Create("Native Hawaiian or Other Pacific Islander", "2076-8"), ((DeathRecord)JSONRecords[0]).Race[1]);
            Assert.Equal(Tuple.Create("White", "2106-3"), ((DeathRecord)XMLRecords[0]).Race[0]);
            Assert.Equal(Tuple.Create("Native Hawaiian or Other Pacific Islander", "2076-8"), ((DeathRecord)XMLRecords[0]).Race[1]);
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
            Assert.Equal("1011 Example Street", ((DeathRecord)JSONRecords[0]).PlaceOfBirth["addressLine1"]);
            Assert.Equal("Line 2", ((DeathRecord)JSONRecords[0]).PlaceOfBirth["addressLine2"]);
            Assert.Equal("Bedford", ((DeathRecord)JSONRecords[0]).PlaceOfBirth["addressCity"]);
            Assert.Equal("Middlesex", ((DeathRecord)JSONRecords[0]).PlaceOfBirth["addressCounty"]);
            Assert.Equal("Massachusetts", ((DeathRecord)JSONRecords[0]).PlaceOfBirth["addressState"]);
            Assert.Equal("01730", ((DeathRecord)JSONRecords[0]).PlaceOfBirth["addressZip"]);
            Assert.Equal("United States", ((DeathRecord)JSONRecords[0]).PlaceOfBirth["addressCountry"]);
            Assert.Equal("1011 Example Street", ((DeathRecord)XMLRecords[0]).PlaceOfBirth["addressLine1"]);
            Assert.Equal("Line 2", ((DeathRecord)XMLRecords[0]).PlaceOfBirth["addressLine2"]);
            Assert.Equal("Bedford", ((DeathRecord)XMLRecords[0]).PlaceOfBirth["addressCity"]);
            Assert.Equal("Middlesex", ((DeathRecord)XMLRecords[0]).PlaceOfBirth["addressCounty"]);
            Assert.Equal("Massachusetts", ((DeathRecord)XMLRecords[0]).PlaceOfBirth["addressState"]);
            Assert.Equal("01730", ((DeathRecord)XMLRecords[0]).PlaceOfBirth["addressZip"]);
            Assert.Equal("United States", ((DeathRecord)XMLRecords[0]).PlaceOfBirth["addressCountry"]);
        }

        [Fact]
        public void Set_MaritalStatus()
        {
            Dictionary<string, string> code = new Dictionary<string, string>();
            code.Add("code", "S");
            code.Add("system", "http://terminology.hl7.org/CodeSystem/v3-MaritalStatus");
            code.Add("display", "Never Married");
            SetterDeathRecord.MaritalStatus = code;
            Assert.Equal("S", SetterDeathRecord.MaritalStatus["code"]);
            Assert.Equal("http://terminology.hl7.org/CodeSystem/v3-MaritalStatus", SetterDeathRecord.MaritalStatus["system"]);
            Assert.Equal("Never Married", SetterDeathRecord.MaritalStatus["display"]);
        }

        [Fact]
        public void Get_MaritalStatus()
        {
            Assert.Equal("S", ((DeathRecord)JSONRecords[0]).MaritalStatus["code"]);
            Assert.Equal("http://terminology.hl7.org/CodeSystem/v3-MaritalStatus", ((DeathRecord)JSONRecords[0]).MaritalStatus["system"]);
            Assert.Equal("Never Married", ((DeathRecord)JSONRecords[0]).MaritalStatus["display"]);
            Assert.Equal("S", ((DeathRecord)XMLRecords[0]).MaritalStatus["code"]);
            Assert.Equal("http://terminology.hl7.org/CodeSystem/v3-MaritalStatus", ((DeathRecord)XMLRecords[0]).MaritalStatus["system"]);
            Assert.Equal("Never Married", ((DeathRecord)XMLRecords[0]).MaritalStatus["display"]);
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
            Dictionary<string, string> elevel = new Dictionary<string, string>();
            elevel.Add("code", "BD");
            elevel.Add("system", "http://terminology.hl7.org/CodeSystem/v3-EducationLevel");
            elevel.Add("display", "College or baccalaureate degree complete");
            SetterDeathRecord.EducationLevel = elevel;
            Assert.Equal("BD", SetterDeathRecord.EducationLevel["code"]);
            Assert.Equal("http://terminology.hl7.org/CodeSystem/v3-EducationLevel", SetterDeathRecord.EducationLevel["system"]);
            Assert.Equal("College or baccalaureate degree complete", SetterDeathRecord.EducationLevel["display"]);
        }

        [Fact]
        public void Get_EducationLevel()
        {
            Assert.Equal("BD", ((DeathRecord)JSONRecords[0]).EducationLevel["code"]);
            Assert.Equal("http://terminology.hl7.org/CodeSystem/v3-EducationLevel", ((DeathRecord)JSONRecords[0]).EducationLevel["system"]);
            Assert.Equal("College or baccalaureate degree complete", ((DeathRecord)JSONRecords[0]).EducationLevel["display"]);
            Assert.Equal("BD", ((DeathRecord)XMLRecords[0]).EducationLevel["code"]);
            Assert.Equal("http://terminology.hl7.org/CodeSystem/v3-EducationLevel", ((DeathRecord)XMLRecords[0]).EducationLevel["system"]);
            Assert.Equal("College or baccalaureate degree complete", ((DeathRecord)XMLRecords[0]).EducationLevel["display"]);
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
            Assert.Equal("4242123", ((DeathRecord)JSONRecords[0]).BirthRecordId);
            Assert.Equal("4242123", ((DeathRecord)XMLRecords[0]).BirthRecordId);
        }

        [Fact]
        public void Set_BirthRecordState()
        {
            Dictionary<string, string> brs = new Dictionary<string, string>();
            brs.Add("code", "MA");
            brs.Add("system", "urn:iso:std:iso:3166:-2");
            brs.Add("display", "Massachusetts");
            SetterDeathRecord.BirthRecordState = brs;
            Assert.Equal("MA", SetterDeathRecord.BirthRecordState["code"]);
            Assert.Equal("urn:iso:std:iso:3166:-2", SetterDeathRecord.BirthRecordState["system"]);
            Assert.Equal("Massachusetts", SetterDeathRecord.BirthRecordState["display"]);
        }

        [Fact]
        public void Get_BirthRecordState()
        {
            Assert.Equal("MA", ((DeathRecord)JSONRecords[0]).BirthRecordState["code"]);
            Assert.Equal("urn:iso:std:iso:3166:-2", ((DeathRecord)JSONRecords[0]).BirthRecordState["system"]);
            Assert.Equal("Massachusetts", ((DeathRecord)JSONRecords[0]).BirthRecordState["display"]);
            Assert.Equal("MA", ((DeathRecord)XMLRecords[0]).BirthRecordState["code"]);
            Assert.Equal("urn:iso:std:iso:3166:-2", ((DeathRecord)XMLRecords[0]).BirthRecordState["system"]);
            Assert.Equal("Massachusetts", ((DeathRecord)XMLRecords[0]).BirthRecordState["display"]);
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
            Assert.Equal("secretary", ((DeathRecord)JSONRecords[0]).UsualOccupation);
            Assert.Equal("secretary", ((DeathRecord)XMLRecords[0]).UsualOccupation);
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
            Assert.Equal("State agency", ((DeathRecord)JSONRecords[0]).UsualIndustry);
            Assert.Equal("State agency", ((DeathRecord)XMLRecords[0]).UsualIndustry);
        }

        [Fact]
        public void Set_MilitaryService()
        {
            Dictionary<string, string> mserv = new Dictionary<string, string>();
            mserv.Add("code", "Y");
            mserv.Add("system", "http://terminology.hl7.org/CodeSystem/v2-0136");
            mserv.Add("display", "Yes");
            SetterDeathRecord.MilitaryService = mserv;
            Assert.Equal("Y", SetterDeathRecord.MilitaryService["code"]);
            Assert.Equal("http://terminology.hl7.org/CodeSystem/v2-0136", SetterDeathRecord.MilitaryService["system"]);
            Assert.Equal("Yes", SetterDeathRecord.MilitaryService["display"]);
        }

        [Fact]
        public void Get_MilitaryService()
        {
            Assert.Equal("Y", ((DeathRecord)JSONRecords[0]).MilitaryService["code"]);
            Assert.Equal("http://terminology.hl7.org/CodeSystem/v2-0136", ((DeathRecord)JSONRecords[0]).MilitaryService["system"]);
            Assert.Equal("Yes", ((DeathRecord)JSONRecords[0]).MilitaryService["display"]);
            Assert.Equal("Y", ((DeathRecord)XMLRecords[0]).MilitaryService["code"]);
            Assert.Equal("http://terminology.hl7.org/CodeSystem/v2-0136", ((DeathRecord)XMLRecords[0]).MilitaryService["system"]);
            Assert.Equal("Yes", ((DeathRecord)XMLRecords[0]).MilitaryService["display"]);
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
            Assert.Equal("FD", ((DeathRecord)JSONRecords[0]).MorticianGivenNames[0]);
            Assert.Equal("Middle", ((DeathRecord)JSONRecords[0]).MorticianGivenNames[1]);
            Assert.Equal("FD", ((DeathRecord)XMLRecords[0]).MorticianGivenNames[0]);
            Assert.Equal("Middle", ((DeathRecord)XMLRecords[0]).MorticianGivenNames[1]);
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
            Assert.Equal("Last", ((DeathRecord)JSONRecords[0]).MorticianFamilyName);
            Assert.Equal("Last", ((DeathRecord)XMLRecords[0]).MorticianFamilyName);
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
            Assert.Equal("Jr.", ((DeathRecord)JSONRecords[0]).MorticianSuffix);
            Assert.Equal("Jr.", ((DeathRecord)XMLRecords[0]).MorticianSuffix);
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
            Assert.Equal("9876543210", ((DeathRecord)JSONRecords[0]).MorticianIdentifier);
            Assert.Equal("9876543210", ((DeathRecord)XMLRecords[0]).MorticianIdentifier);
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
            Assert.Equal("1011010 Example Street", ((DeathRecord)JSONRecords[0]).FuneralHomeAddress["addressLine1"]);
            Assert.Equal("Line 2", ((DeathRecord)JSONRecords[0]).FuneralHomeAddress["addressLine2"]);
            Assert.Equal("Bedford", ((DeathRecord)JSONRecords[0]).FuneralHomeAddress["addressCity"]);
            Assert.Equal("Middlesex", ((DeathRecord)JSONRecords[0]).FuneralHomeAddress["addressCounty"]);
            Assert.Equal("Massachusetts", ((DeathRecord)JSONRecords[0]).FuneralHomeAddress["addressState"]);
            Assert.Equal("01730", ((DeathRecord)JSONRecords[0]).FuneralHomeAddress["addressZip"]);
            Assert.Equal("United States", ((DeathRecord)JSONRecords[0]).FuneralHomeAddress["addressCountry"]);
            Assert.Equal("1011010 Example Street", ((DeathRecord)XMLRecords[0]).FuneralHomeAddress["addressLine1"]);
            Assert.Equal("Line 2", ((DeathRecord)XMLRecords[0]).FuneralHomeAddress["addressLine2"]);
            Assert.Equal("Bedford", ((DeathRecord)XMLRecords[0]).FuneralHomeAddress["addressCity"]);
            Assert.Equal("Middlesex", ((DeathRecord)XMLRecords[0]).FuneralHomeAddress["addressCounty"]);
            Assert.Equal("Massachusetts", ((DeathRecord)XMLRecords[0]).FuneralHomeAddress["addressState"]);
            Assert.Equal("01730", ((DeathRecord)XMLRecords[0]).FuneralHomeAddress["addressZip"]);
            Assert.Equal("United States", ((DeathRecord)XMLRecords[0]).FuneralHomeAddress["addressCountry"]);
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
            Assert.Equal("603 Example Street", ((DeathRecord)JSONRecords[0]).DispositionLocationAddress["addressLine1"]);
            Assert.Equal("Line 2", ((DeathRecord)JSONRecords[0]).DispositionLocationAddress["addressLine2"]);
            Assert.Equal("Bedford", ((DeathRecord)JSONRecords[0]).DispositionLocationAddress["addressCity"]);
            Assert.Equal("Middlesex", ((DeathRecord)JSONRecords[0]).DispositionLocationAddress["addressCounty"]);
            Assert.Equal("Massachusetts", ((DeathRecord)JSONRecords[0]).DispositionLocationAddress["addressState"]);
            Assert.Equal("01730", ((DeathRecord)JSONRecords[0]).DispositionLocationAddress["addressZip"]);
            Assert.Equal("United States", ((DeathRecord)JSONRecords[0]).DispositionLocationAddress["addressCountry"]);
            Assert.Equal("603 Example Street", ((DeathRecord)XMLRecords[0]).DispositionLocationAddress["addressLine1"]);
            Assert.Equal("Line 2", ((DeathRecord)XMLRecords[0]).DispositionLocationAddress["addressLine2"]);
            Assert.Equal("Bedford", ((DeathRecord)XMLRecords[0]).DispositionLocationAddress["addressCity"]);
            Assert.Equal("Middlesex", ((DeathRecord)XMLRecords[0]).DispositionLocationAddress["addressCounty"]);
            Assert.Equal("Massachusetts", ((DeathRecord)XMLRecords[0]).DispositionLocationAddress["addressState"]);
            Assert.Equal("01730", ((DeathRecord)XMLRecords[0]).DispositionLocationAddress["addressZip"]);
            Assert.Equal("United States", ((DeathRecord)XMLRecords[0]).DispositionLocationAddress["addressCountry"]);
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
            Assert.Equal("449971000124106", ((DeathRecord)JSONRecords[0]).DecedentDispositionMethod["code"]);
            Assert.Equal("http://snomed.info/sct", ((DeathRecord)JSONRecords[0]).DecedentDispositionMethod["system"]);
            Assert.Equal("Burial", ((DeathRecord)JSONRecords[0]).DecedentDispositionMethod["display"]);
            Assert.Equal("449971000124106", ((DeathRecord)XMLRecords[0]).DecedentDispositionMethod["code"]);
            Assert.Equal("http://snomed.info/sct", ((DeathRecord)XMLRecords[0]).DecedentDispositionMethod["system"]);
            Assert.Equal("Burial", ((DeathRecord)XMLRecords[0]).DecedentDispositionMethod["display"]);
        }

        [Fact]
        public void Set_AutopsyPerformedIndicator()
        {
            Dictionary<string, string> api = new Dictionary<string, string>();
            api.Add("code", "Y");
            api.Add("system", "http://terminology.hl7.org/CodeSystem/v2-0136");
            api.Add("display", "Yes");
            SetterDeathRecord.AutopsyPerformedIndicator = api;
            Assert.Equal("Y", SetterDeathRecord.AutopsyPerformedIndicator["code"]);
            Assert.Equal("http://terminology.hl7.org/CodeSystem/v2-0136", SetterDeathRecord.AutopsyPerformedIndicator["system"]);
            Assert.Equal("Yes", SetterDeathRecord.AutopsyPerformedIndicator["display"]);
        }

        [Fact]
        public void Get_AutopsyPerformedIndicator()
        {
            Assert.Equal("Y", ((DeathRecord)JSONRecords[0]).AutopsyPerformedIndicator["code"]);
            Assert.Equal("http://terminology.hl7.org/CodeSystem/v2-0136", ((DeathRecord)JSONRecords[0]).AutopsyPerformedIndicator["system"]);
            Assert.Equal("Yes", ((DeathRecord)JSONRecords[0]).AutopsyPerformedIndicator["display"]);
            Assert.Equal("Y", ((DeathRecord)XMLRecords[0]).AutopsyPerformedIndicator["code"]);
            Assert.Equal("http://terminology.hl7.org/CodeSystem/v2-0136", ((DeathRecord)XMLRecords[0]).AutopsyPerformedIndicator["system"]);
            Assert.Equal("Yes", ((DeathRecord)XMLRecords[0]).AutopsyPerformedIndicator["display"]);
        }

        [Fact]
        public void Set_AutopsyResultsAvailable()
        {
            Dictionary<string, string> ara = new Dictionary<string, string>();
            ara.Add("code", "Y");
            ara.Add("system", "http://terminology.hl7.org/CodeSystem/v2-0136");
            ara.Add("display", "Yes");
            SetterDeathRecord.AutopsyResultsAvailable = ara;
            Assert.Equal("Y", SetterDeathRecord.AutopsyResultsAvailable["code"]);
            Assert.Equal("http://terminology.hl7.org/CodeSystem/v2-0136", SetterDeathRecord.AutopsyResultsAvailable["system"]);
            Assert.Equal("Yes", SetterDeathRecord.AutopsyResultsAvailable["display"]);
        }

        [Fact]
        public void Get_AutopsyResultsAvailable()
        {
            Assert.Equal("Y", ((DeathRecord)JSONRecords[0]).AutopsyResultsAvailable["code"]);
            Assert.Equal("http://terminology.hl7.org/CodeSystem/v2-0136", ((DeathRecord)JSONRecords[0]).AutopsyResultsAvailable["system"]);
            Assert.Equal("Yes", ((DeathRecord)JSONRecords[0]).AutopsyResultsAvailable["display"]);
            Assert.Equal("Y", ((DeathRecord)XMLRecords[0]).AutopsyResultsAvailable["code"]);
            Assert.Equal("http://terminology.hl7.org/CodeSystem/v2-0136", ((DeathRecord)XMLRecords[0]).AutopsyResultsAvailable["system"]);
            Assert.Equal("Yes", ((DeathRecord)XMLRecords[0]).AutopsyResultsAvailable["display"]);
        }

        [Fact]
        public void Set_AgeAtDeath()
        {
            Dictionary<string, string> aad = new Dictionary<string, string>();
            aad.Add("unit", "a");
            aad.Add("value", "79");
            SetterDeathRecord.AgeAtDeath = aad;
            Assert.Equal("a", SetterDeathRecord.AgeAtDeath["unit"]);
            Assert.Equal("79", SetterDeathRecord.AgeAtDeath["value"]);
        }

        [Fact]
        public void Get_AgeAtDeath()
        {
            Assert.Equal("a", ((DeathRecord)JSONRecords[0]).AgeAtDeath["unit"]);
            Assert.Equal("79", ((DeathRecord)JSONRecords[0]).AgeAtDeath["value"]);
            Assert.Equal("a", ((DeathRecord)XMLRecords[0]).AgeAtDeath["unit"]);
            Assert.Equal("79", ((DeathRecord)XMLRecords[0]).AgeAtDeath["value"]);
        }

        [Fact]
        public void Set_PregnancyStatus()
        {
            Dictionary<string, string> ps = new Dictionary<string, string>();
            ps.Add("code", "PHC1260");
            ps.Add("system", "urn:oid:2.16.840.1.114222.4.5.274");
            ps.Add("display", "Not pregnant within past year");
            SetterDeathRecord.PregnancyStatus = ps;
            Assert.Equal("PHC1260", SetterDeathRecord.PregnancyStatus["code"]);
            Assert.Equal("urn:oid:2.16.840.1.114222.4.5.274", SetterDeathRecord.PregnancyStatus["system"]);
            Assert.Equal("Not pregnant within past year", SetterDeathRecord.PregnancyStatus["display"]);
        }

        [Fact]
        public void Get_PregnancyStatus()
        {
            Assert.Equal("NA", ((DeathRecord)JSONRecords[0]).PregnancyStatus["code"]);
            Assert.Equal("http://hl7.org/fhir/v3/NullFlavor", ((DeathRecord)JSONRecords[0]).PregnancyStatus["system"]);
            Assert.Equal("not applicable", ((DeathRecord)JSONRecords[0]).PregnancyStatus["display"]);
            Assert.Equal("NA", ((DeathRecord)XMLRecords[0]).PregnancyStatus["code"]);
            Assert.Equal("http://hl7.org/fhir/v3/NullFlavor", ((DeathRecord)XMLRecords[0]).PregnancyStatus["system"]);
            Assert.Equal("not applicable", ((DeathRecord)XMLRecords[0]).PregnancyStatus["display"]);
        }

        [Fact]
        public void Set_InjuryPlace()
        {
            SetterDeathRecord.InjuryPlace = "home";
            Assert.Equal("home", SetterDeathRecord.InjuryPlace);
        }

        [Fact]
        public void Get_InjuryPlace()
        {
            Assert.Equal("home", ((DeathRecord)JSONRecords[0]).InjuryPlace);
            Assert.Equal("home", ((DeathRecord)XMLRecords[0]).InjuryPlace);
        }

        [Fact]
        public void Set_TransportationRole()
        {
            Dictionary<string, string> tr = new Dictionary<string, string>();
            tr.Add("code", "257500003");
            tr.Add("system", "urn:oid:2.16.840.1.114222.4.11.6005");
            tr.Add("display", "Passenger");
            SetterDeathRecord.TransportationRole = tr;
            Assert.Equal("257500003", SetterDeathRecord.TransportationRole["code"]);
            Assert.Equal("urn:oid:2.16.840.1.114222.4.11.6005", SetterDeathRecord.TransportationRole["system"]);
            Assert.Equal("Passenger", SetterDeathRecord.TransportationRole["display"]);
        }

        [Fact]
        public void Get_TransportationRole()
        {
            Assert.Equal("257500003", ((DeathRecord)JSONRecords[0]).TransportationRole["code"]);
            Assert.Equal("urn:oid:2.16.840.1.114222.4.11.6005", ((DeathRecord)JSONRecords[0]).TransportationRole["system"]);
            Assert.Equal("Passenger", ((DeathRecord)JSONRecords[0]).TransportationRole["display"]);
            Assert.Equal("257500003", ((DeathRecord)XMLRecords[0]).TransportationRole["code"]);
            Assert.Equal("urn:oid:2.16.840.1.114222.4.11.6005", ((DeathRecord)XMLRecords[0]).TransportationRole["system"]);
            Assert.Equal("Passenger", ((DeathRecord)XMLRecords[0]).TransportationRole["display"]);
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
            Assert.False(((DeathRecord)JSONRecords[0]).ExaminerContacted);
            Assert.False(((DeathRecord)XMLRecords[0]).ExaminerContacted);
        }

        [Fact]
        public void Set_TobaccoUse()
        {
            Dictionary<string, string> tbu = new Dictionary<string, string>();
            tbu.Add("code", "373066001");
            tbu.Add("system", "http://snomed.info/sct");
            tbu.Add("display", "Yes");
            SetterDeathRecord.TobaccoUse = tbu;
            Assert.Equal("373066001", SetterDeathRecord.TobaccoUse["code"]);
            Assert.Equal("http://snomed.info/sct", SetterDeathRecord.TobaccoUse["system"]);
            Assert.Equal("Yes", SetterDeathRecord.TobaccoUse["display"]);
        }

        [Fact]
        public void Get_TobaccoUse()
        {
            Assert.Equal("373066001", ((DeathRecord)JSONRecords[0]).TobaccoUse["code"]);
            Assert.Equal("http://snomed.info/sct", ((DeathRecord)JSONRecords[0]).TobaccoUse["system"]);
            Assert.Equal("Yes", ((DeathRecord)JSONRecords[0]).TobaccoUse["display"]);
            Assert.Equal("373066001", ((DeathRecord)XMLRecords[0]).TobaccoUse["code"]);
            Assert.Equal("http://snomed.info/sct", ((DeathRecord)XMLRecords[0]).TobaccoUse["system"]);
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
            Assert.Equal("781 Example Street", ((DeathRecord)JSONRecords[0]).InjuryLocationAddress["addressLine1"]);
            Assert.Equal("Line 2", ((DeathRecord)JSONRecords[0]).InjuryLocationAddress["addressLine2"]);
            Assert.Equal("Bedford", ((DeathRecord)JSONRecords[0]).InjuryLocationAddress["addressCity"]);
            Assert.Equal("Middlesex", ((DeathRecord)JSONRecords[0]).InjuryLocationAddress["addressCounty"]);
            Assert.Equal("Massachusetts", ((DeathRecord)JSONRecords[0]).InjuryLocationAddress["addressState"]);
            Assert.Equal("01730", ((DeathRecord)JSONRecords[0]).InjuryLocationAddress["addressZip"]);
            Assert.Equal("United States", ((DeathRecord)JSONRecords[0]).InjuryLocationAddress["addressCountry"]);
            Assert.Equal("781 Example Street", ((DeathRecord)XMLRecords[0]).InjuryLocationAddress["addressLine1"]);
            Assert.Equal("Line 2", ((DeathRecord)XMLRecords[0]).InjuryLocationAddress["addressLine2"]);
            Assert.Equal("Bedford", ((DeathRecord)XMLRecords[0]).InjuryLocationAddress["addressCity"]);
            Assert.Equal("Middlesex", ((DeathRecord)XMLRecords[0]).InjuryLocationAddress["addressCounty"]);
            Assert.Equal("Massachusetts", ((DeathRecord)XMLRecords[0]).InjuryLocationAddress["addressState"]);
            Assert.Equal("01730", ((DeathRecord)XMLRecords[0]).InjuryLocationAddress["addressZip"]);
            Assert.Equal("United States", ((DeathRecord)XMLRecords[0]).InjuryLocationAddress["addressCountry"]);
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
            Assert.Equal("Example Injury Location Name", ((DeathRecord)JSONRecords[0]).InjuryLocationName);
            Assert.Equal("Example Injury Location Name", ((DeathRecord)XMLRecords[0]).InjuryLocationName);
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
            Assert.Equal("Example Injury Location Description", ((DeathRecord)JSONRecords[0]).InjuryLocationDescription);
            Assert.Equal("Example Injury Location Description", ((DeathRecord)XMLRecords[0]).InjuryLocationDescription);
        }

        [Fact]
        public void Set_InjuryDate()
        {
            SetterDeathRecord.InjuryDate = "2018-02-19T16:48:06.498822-05:00";
            Assert.Equal("2018-02-19T16:48:06.498822-05:00", SetterDeathRecord.InjuryDate);
        }

        [Fact]
        public void Get_InjuryDate()
        {
            Assert.Equal("2018-02-19T16:48:06-05:00", ((DeathRecord)JSONRecords[0]).InjuryDate);
            Assert.Equal("2018-02-19T16:48:06-05:00", ((DeathRecord)XMLRecords[0]).InjuryDate);
        }

        [Fact]
        public void Set_InjuryAtWork()
        {
            Dictionary<string, string> iaw = new Dictionary<string, string>();
            iaw.Add("code", "N");
            iaw.Add("system", "http://terminology.hl7.org/CodeSystem/v2-0136");
            iaw.Add("display", "No");
            SetterDeathRecord.InjuryAtWork = iaw;
            Assert.Equal("N", SetterDeathRecord.InjuryAtWork["code"]);
            Assert.Equal("http://terminology.hl7.org/CodeSystem/v2-0136", SetterDeathRecord.InjuryAtWork["system"]);
            Assert.Equal("No", SetterDeathRecord.InjuryAtWork["display"]);
        }

        [Fact]
        public void Get_InjuryAtWork()
        {
            Assert.Equal("N", ((DeathRecord)JSONRecords[0]).InjuryAtWork["code"]);
            Assert.Equal("http://terminology.hl7.org/CodeSystem/v2-0136", ((DeathRecord)JSONRecords[0]).InjuryAtWork["system"]);
            Assert.Equal("No", ((DeathRecord)JSONRecords[0]).InjuryAtWork["display"]);
            Assert.Equal("N", ((DeathRecord)XMLRecords[0]).InjuryAtWork["code"]);
            Assert.Equal("http://terminology.hl7.org/CodeSystem/v2-0136", ((DeathRecord)XMLRecords[0]).InjuryAtWork["system"]);
            Assert.Equal("No", ((DeathRecord)XMLRecords[0]).InjuryAtWork["display"]);
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
            Assert.Equal("671 Example Street", ((DeathRecord)JSONRecords[0]).DeathLocationAddress["addressLine1"]);
            Assert.Equal("Line 2", ((DeathRecord)JSONRecords[0]).DeathLocationAddress["addressLine2"]);
            Assert.Equal("Bedford", ((DeathRecord)JSONRecords[0]).DeathLocationAddress["addressCity"]);
            Assert.Equal("Middlesex", ((DeathRecord)JSONRecords[0]).DeathLocationAddress["addressCounty"]);
            Assert.Equal("Massachusetts", ((DeathRecord)JSONRecords[0]).DeathLocationAddress["addressState"]);
            Assert.Equal("01730", ((DeathRecord)JSONRecords[0]).DeathLocationAddress["addressZip"]);
            Assert.Equal("United States", ((DeathRecord)JSONRecords[0]).DeathLocationAddress["addressCountry"]);
            Assert.Equal("671 Example Street", ((DeathRecord)XMLRecords[0]).DeathLocationAddress["addressLine1"]);
            Assert.Equal("Line 2", ((DeathRecord)XMLRecords[0]).DeathLocationAddress["addressLine2"]);
            Assert.Equal("Bedford", ((DeathRecord)XMLRecords[0]).DeathLocationAddress["addressCity"]);
            Assert.Equal("Middlesex", ((DeathRecord)XMLRecords[0]).DeathLocationAddress["addressCounty"]);
            Assert.Equal("Massachusetts", ((DeathRecord)XMLRecords[0]).DeathLocationAddress["addressState"]);
            Assert.Equal("01730", ((DeathRecord)XMLRecords[0]).DeathLocationAddress["addressZip"]);
            Assert.Equal("United States", ((DeathRecord)XMLRecords[0]).DeathLocationAddress["addressCountry"]);
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
            Assert.Equal("Example Death Location Name", ((DeathRecord)JSONRecords[0]).DeathLocationName);
            Assert.Equal("Example Death Location Name", ((DeathRecord)XMLRecords[0]).DeathLocationName);
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
            SetterDeathRecord.DateOfDeath = "2018-02-19T16:48:06.498822-05:00";
            Assert.Equal("2018-02-19T16:48:06.498822-05:00", SetterDeathRecord.DateOfDeath);
        }

        [Fact]
        public void Get_DateOfDeath()
        {
            Assert.Equal("2018-02-19T16:48:06-05:00", ((DeathRecord)JSONRecords[0]).DateOfDeath);
            Assert.Equal("2018-02-19T16:48:06-05:00", ((DeathRecord)XMLRecords[0]).DateOfDeath);
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
            Assert.Equal("2018-02-20T16:48:06-05:00", ((DeathRecord)XMLRecords[0]).DateOfDeathPronouncement);
        }

        [Fact]
        public void CreateSubmissionFromDeathRecord()
        {
            DeathRecordSubmission submission = new DeathRecordSubmission((DeathRecord)XMLRecords[0]);
            Assert.Equal("2018-02-20T16:48:06-05:00", submission.MessagePayload.DateOfDeathPronouncement);
        }

        [Fact]
        public void CreateSubmissionFromJSON()
        {
            DeathRecordSubmission submission = new DeathRecordSubmission(File.ReadAllText(FixturePath("fixtures/json/DeathRecordSubmission.json")));
            Assert.Equal("2018-02-20T16:48:06-05:00", submission.MessagePayload.DateOfDeathPronouncement);
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
