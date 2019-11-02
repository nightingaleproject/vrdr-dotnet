using System;
using System.IO;
using Xunit;

namespace FhirDeathRecord.Tests
{
    public class DeathRecord_Parser_Should
    {
        [Fact]
        public void FailInvalidInput()
        {
            Exception ex = Assert.Throws<System.ArgumentException>(() => new DeathRecord("foobar"));
            Assert.Equal("The given input does not appear to be a valid XML or JSON FHIR record.", ex.Message);
        }

        [Fact]
        public void FailMissingArray()
        {
            string bundle = File.ReadAllText(FixturePath("fixtures/json/MissingArray.json"));
            Exception ex = Assert.Throws<System.ArgumentException>(() => new DeathRecord(bundle));
            Assert.Equal("Parser: Since element 'identifier' repeats, an array must be used here.", ex.Message);
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
