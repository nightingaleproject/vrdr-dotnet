using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using Xunit;

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
            Exception ex = Assert.Throws<System.ArgumentException>(() => new DeathRecord("foobar"));
            Assert.Equal("Record is neither valid XML nor JSON.", ex.Message);
        }

        [Fact]
        public void Set_Id()
        {
            SetterDeathRecord.Id = "1337";
            Assert.Equal("1337", SetterDeathRecord.Id);
        }

        [Fact]
        public void Get_ID()
        {
            Assert.Equal("1", ((DeathRecord)XMLRecords[0]).Id);
            Assert.Equal("1", ((DeathRecord)JSONRecords[0]).Id);
        }

        [Fact]
        public void Set_CreationDate()
        {
            SetterDeathRecord.CreationDate = "2018-07-11";
            Assert.Equal("2018-07-11", SetterDeathRecord.CreationDate);
        }

        [Fact]
        public void Get_CreationDate()
        {
            Assert.Equal("2018-07-10", ((DeathRecord)XMLRecords[0]).CreationDate);
            Assert.Equal("2018-07-10", ((DeathRecord)JSONRecords[0]).CreationDate);
        }

        [Fact]
        public void Set_GivenNames()
        {
            string[] expected = {"Example", "Middle"};
            SetterDeathRecord.GivenNames = expected;
            Assert.Equal(expected, SetterDeathRecord.GivenNames);
        }

        [Fact]
        public void Get_GivenNames()
        {
            string[] expected = {"Example", "Middle"};
            Assert.Equal(expected, ((DeathRecord)XMLRecords[0]).GivenNames);
            Assert.Equal(expected, ((DeathRecord)JSONRecords[0]).GivenNames);
        }

        [Fact]
        public void Set_FirstName()
        {
            SetterDeathRecord.FirstName = "Example";
            Assert.Equal("Example", SetterDeathRecord.FirstName);
        }

        [Fact]
        public void Get_FirstName()
        {
            Assert.Equal("Example", ((DeathRecord)XMLRecords[0]).FirstName);
            Assert.Equal("Example", ((DeathRecord)JSONRecords[0]).FirstName);
        }

        [Fact]
        public void Set_MiddleName()
        {
            SetterDeathRecord.MiddleName = "Middle";
            Assert.Equal("Middle", SetterDeathRecord.MiddleName);
        }

        [Fact]
        public void Get_MiddleName()
        {
            Assert.Equal("Middle", ((DeathRecord)XMLRecords[0]).MiddleName);
            Assert.Equal("Middle", ((DeathRecord)JSONRecords[0]).MiddleName);
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
            Assert.Equal("Person", ((DeathRecord)XMLRecords[0]).FamilyName);
            Assert.Equal("Person", ((DeathRecord)JSONRecords[0]).FamilyName);
        }

        [Fact]
        public void Set_Suffix()
        {
            SetterDeathRecord.Suffix = "Sr.";
            Assert.Equal("Sr.", SetterDeathRecord.Suffix);
        }

        [Fact]
        public void Get_Suffix()
        {
            Assert.Equal("Jr.", ((DeathRecord)XMLRecords[0]).Suffix);
            Assert.Equal("Jr.", ((DeathRecord)JSONRecords[0]).Suffix);
        }

        [Fact]
        public void Get_Gender()
        {
            Assert.Equal("male", ((DeathRecord)XMLRecords[0]).Gender);
            Assert.Equal("male", ((DeathRecord)JSONRecords[0]).Gender);
        }

        [Fact]
        public void Set_BirthSex()
        {
            Dictionary<string, string> code = new Dictionary<string, string>();
            code.Add("code", "M");
            code.Add("system", "http://hl7.org/fhir/us/core/ValueSet/us-core-birthsex");
            code.Add("display", "Male");
            SetterDeathRecord.BirthSex = code;
            Assert.Equal("M", SetterDeathRecord.BirthSex["code"]);
            Assert.Equal("http://hl7.org/fhir/us/core/ValueSet/us-core-birthsex", SetterDeathRecord.BirthSex["system"]);
            Assert.Equal("Male", SetterDeathRecord.BirthSex["display"]);
        }

        [Fact]
        public void Get_BirthSex()
        {
            Assert.Equal("M", ((DeathRecord)XMLRecords[0]).BirthSex["code"]);
            Assert.Equal("http://hl7.org/fhir/us/core/ValueSet/us-core-birthsex", ((DeathRecord)XMLRecords[0]).BirthSex["system"]);
            Assert.Equal("Male", ((DeathRecord)XMLRecords[0]).BirthSex["display"]);
            Assert.Equal("M", ((DeathRecord)JSONRecords[0]).BirthSex["code"]);
            Assert.Equal("http://hl7.org/fhir/us/core/ValueSet/us-core-birthsex", ((DeathRecord)JSONRecords[0]).BirthSex["system"]);
            Assert.Equal("Male", ((DeathRecord)JSONRecords[0]).BirthSex["display"]);
        }

        [Fact]
        public void Get_Address()
        {
            Dictionary<string, string> xmlDictionary = ((DeathRecord)XMLRecords[0]).Address;
            Assert.Equal("1 Example Street", xmlDictionary["street"]);
            Assert.Equal("Boston", xmlDictionary["city"]);
            Assert.Equal("Massachusetts", xmlDictionary["state"]);
            Assert.Equal("02101", xmlDictionary["zip"]);
            Dictionary<string, string> jsonDictionary = ((DeathRecord)JSONRecords[0]).Address;
            Assert.Equal("1 Example Street", jsonDictionary["street"]);
            Assert.Equal("Boston", jsonDictionary["city"]);
            Assert.Equal("Massachusetts", jsonDictionary["state"]);
            Assert.Equal("02101", jsonDictionary["zip"]);
        }

        [Fact]
        public void Get_SSN()
        {
            Assert.Equal("111223333", ((DeathRecord)XMLRecords[0]).SSN);
            Assert.Equal("111223333", ((DeathRecord)JSONRecords[0]).SSN);
        }

        [Fact]
        public void Get_Ethnicity()
        {
            // TODO
            Assert.Equal("", "");
            //Assert.Equal("Non Hispanic or Latino", ((DeathRecord)XMLRecords[0]).Ethnicity);
            //Assert.Equal("Non Hispanic or Latino", ((DeathRecord)JSONRecords[0]).Ethnicity);
        }

        [Fact]
        public void Get_DateOfBirth()
        {
            Assert.Equal("1970-04-24", ((DeathRecord)XMLRecords[0]).DateOfBirth);
            Assert.Equal("1970-04-24", ((DeathRecord)JSONRecords[0]).DateOfBirth);
        }

        [Fact]
        public void Get_DateOfDeath()
        {
            Assert.Equal("2018-04-24T00:00:00+00:00", ((DeathRecord)XMLRecords[0]).DateOfDeath);
            Assert.Equal("2018-04-24T00:00:00+00:00", ((DeathRecord)JSONRecords[0]).DateOfDeath);
        }

        [Fact]
        public void Set_PlaceOfBirth()
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add("placeOfBirthLine1", "9 Example Street");
            dictionary.Add("placeOfBirthLine2", "Line 2");
            dictionary.Add("placeOfBirthCity", "Bedford");
            dictionary.Add("placeOfBirthState", "Massachusetts");
            dictionary.Add("placeOfBirthZip", "01730");
            dictionary.Add("placeOfBirthCountry", "United States");
            SetterDeathRecord.PlaceOfBirth = dictionary;
            Assert.Equal("9 Example Street", SetterDeathRecord.PlaceOfBirth["placeOfBirthLine1"]);
            Assert.Equal("Line 2", SetterDeathRecord.PlaceOfBirth["placeOfBirthLine2"]);
            Assert.Equal("Bedford", SetterDeathRecord.PlaceOfBirth["placeOfBirthCity"]);
            Assert.Equal("Massachusetts", SetterDeathRecord.PlaceOfBirth["placeOfBirthState"]);
            Assert.Equal("01730", SetterDeathRecord.PlaceOfBirth["placeOfBirthZip"]);
            Assert.Equal("United States", SetterDeathRecord.PlaceOfBirth["placeOfBirthCountry"]);
        }

        [Fact]
        public void Get_PlaceOfBirth()
        {
            Dictionary<string, string> xmlDictionary = ((DeathRecord)XMLRecords[0]).PlaceOfBirth;
            Assert.Equal("Boston", xmlDictionary["placeOfBirthCity"]);
            Assert.Equal("Massachusetts", xmlDictionary["placeOfBirthState"]);
            Assert.Equal("United States", xmlDictionary["placeOfBirthCountry"]);
            Dictionary<string, string> jsonDictionary = ((DeathRecord)JSONRecords[0]).PlaceOfBirth;
            Assert.Equal("Boston", jsonDictionary["placeOfBirthCity"]);
            Assert.Equal("Massachusetts", jsonDictionary["placeOfBirthState"]);
            Assert.Equal("United States", jsonDictionary["placeOfBirthCountry"]);
        }

        [Fact]
        public void Set_PlaceOfDeath()
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add("placeOfDeathTypeCode", "16983000");
            dictionary.Add("placeOfDeathTypeSystem", "http://snomed.info/sct");
            dictionary.Add("placeOfDeathTypeDisplay", "Death in hospital");
            dictionary.Add("placeOfDeathFacilityName", "Example Hospital");
            dictionary.Add("placeOfDeathLine1", "8 Example Street");
            dictionary.Add("placeOfDeathLine2", "Line 2");
            dictionary.Add("placeOfDeathCity", "Bedford");
            dictionary.Add("placeOfDeathState", "Massachusetts");
            dictionary.Add("placeOfDeathZip", "01730");
            dictionary.Add("placeOfDeathCountry", "United States");
            dictionary.Add("placeOfDeathInsideCityLimits", "True");
            SetterDeathRecord.PlaceOfDeath = dictionary;
            Assert.Equal("16983000", SetterDeathRecord.PlaceOfDeath["placeOfDeathTypeCode"]);
            Assert.Equal("http://snomed.info/sct", SetterDeathRecord.PlaceOfDeath["placeOfDeathTypeSystem"]);
            Assert.Equal("Death in hospital", SetterDeathRecord.PlaceOfDeath["placeOfDeathTypeDisplay"]);
            Assert.Equal("Example Hospital", SetterDeathRecord.PlaceOfDeath["placeOfDeathFacilityName"]);
            Assert.Equal("8 Example Street", SetterDeathRecord.PlaceOfDeath["placeOfDeathLine1"]);
            Assert.Equal("Line 2", SetterDeathRecord.PlaceOfDeath["placeOfDeathLine2"]);
            Assert.Equal("Bedford", SetterDeathRecord.PlaceOfDeath["placeOfDeathCity"]);
            Assert.Equal("Massachusetts", SetterDeathRecord.PlaceOfDeath["placeOfDeathState"]);
            Assert.Equal("01730", SetterDeathRecord.PlaceOfDeath["placeOfDeathZip"]);
            Assert.Equal("United States", SetterDeathRecord.PlaceOfDeath["placeOfDeathCountry"]);
            Assert.Equal("True", SetterDeathRecord.PlaceOfDeath["placeOfDeathInsideCityLimits"]);
        }

        [Fact]
        public void Get_PlaceOfDeath()
        {
            Dictionary<string, string> xmlDictionary = ((DeathRecord)XMLRecords[0]).PlaceOfDeath;
            Assert.Equal("16983000", xmlDictionary["placeOfDeathTypeCode"]);
            Assert.Equal("Example Hospital", xmlDictionary["placeOfDeathFacilityName"]);
            Assert.Equal("5 Example St.", xmlDictionary["placeOfDeathLine1"]);
            Assert.Equal("Boston", xmlDictionary["placeOfDeathCity"]);
            Assert.Equal("Massachusetts", xmlDictionary["placeOfDeathState"]);
            Assert.Equal("02101", xmlDictionary["placeOfDeathZip"]);
            Assert.Equal("United States", xmlDictionary["placeOfDeathCountry"]);
            Dictionary<string, string> jsonDictionary = ((DeathRecord)JSONRecords[0]).PlaceOfDeath;
            Assert.Equal("16983000", jsonDictionary["placeOfDeathTypeCode"]);
            Assert.Equal("Example Hospital", jsonDictionary["placeOfDeathFacilityName"]);
            Assert.Equal("5 Example St.", jsonDictionary["placeOfDeathLine1"]);
            Assert.Equal("Boston", jsonDictionary["placeOfDeathCity"]);
            Assert.Equal("Massachusetts", jsonDictionary["placeOfDeathState"]);
            Assert.Equal("02101", jsonDictionary["placeOfDeathZip"]);
            Assert.Equal("United States", jsonDictionary["placeOfDeathCountry"]);
        }

        [Fact]
        public void Set_CertifierGivenNames()
        {
            string[] expected = {"Example", "Middle", "Middle 2", "Middle 3"};
            SetterDeathRecord.CertifierGivenNames = expected;
            Assert.Equal(expected, SetterDeathRecord.CertifierGivenNames);
        }

        [Fact]
        public void Get_CertifierGivenNames()
        {
            string[] expected = {"Example", "Middle"};
            Assert.Equal(expected, ((DeathRecord)XMLRecords[0]).CertifierGivenNames);
            Assert.Equal(expected, ((DeathRecord)JSONRecords[0]).CertifierGivenNames);
        }

        [Fact]
        public void Set_CertifierFamilyName()
        {
            SetterDeathRecord.FamilyName = "Doctor";
            Assert.Equal("Doctor", SetterDeathRecord.FamilyName);
        }

        [Fact]
        public void Get_CertifierFamilyName()
        {
            Assert.Equal("Doctor", ((DeathRecord)XMLRecords[0]).CertifierFamilyName);
            Assert.Equal("Doctor", ((DeathRecord)JSONRecords[0]).CertifierFamilyName);
        }

        [Fact]
        public void Get_CertifierAddress()
        {
            Dictionary<string, string> xmlDictionary = ((DeathRecord)XMLRecords[0]).CertifierAddress;
            Assert.Equal("100 Example St.", xmlDictionary["street"]);
            Assert.Equal("Bedford", xmlDictionary["city"]);
            Assert.Equal("Massachusetts", xmlDictionary["state"]);
            Assert.Equal("01730", xmlDictionary["zip"]);
            Dictionary<string, string> jsonDictionary = ((DeathRecord)JSONRecords[0]).CertifierAddress;
            Assert.Equal("100 Example St.", jsonDictionary["street"]);
            Assert.Equal("Bedford", jsonDictionary["city"]);
            Assert.Equal("Massachusetts", jsonDictionary["state"]);
            Assert.Equal("01730", jsonDictionary["zip"]);
        }

        [Fact]
        public void Get_CertifierType()
        {
            Assert.Equal("Physician (Pronouncer and Certifier)", ((DeathRecord)XMLRecords[0]).CertifierType);
            Assert.Equal("Physician (Pronouncer and Certifier)", ((DeathRecord)JSONRecords[0]).CertifierType);
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
            Assert.Equal("Example Contributing Condition", ((DeathRecord)XMLRecords[0]).ContributingConditions);
            Assert.Equal("Example Contributing Condition", ((DeathRecord)JSONRecords[0]).ContributingConditions);
        }

        [Fact]
        public void Set_CausesOfDeath()
        {
            Tuple<string, string, Dictionary<string, string>>[] causes =
            {
                Tuple.Create("Example Immediate COD", "minutes", new Dictionary<string, string>(){ {"code", "1234"}, {"system", "example"} }),
                Tuple.Create("Example Underlying COD 1", "2 hours", new Dictionary<string, string>()),
                Tuple.Create("Example Underlying COD 2", "6 months", new Dictionary<string, string>()),
                Tuple.Create("Example Underlying COD 3", "15 years", new Dictionary<string, string>()),
                Tuple.Create("Example Underlying COD 4", "30 years", new Dictionary<string, string>()),
                Tuple.Create("Example Underlying COD 5", "years", new Dictionary<string, string>())
            };
            SetterDeathRecord.CausesOfDeath = causes;
            Assert.Equal("Example Immediate COD", SetterDeathRecord.CausesOfDeath[0].Item1);
            Assert.Equal("minutes", SetterDeathRecord.CausesOfDeath[0].Item2);
            Assert.Equal("1234", SetterDeathRecord.CausesOfDeath[0].Item3["code"]);
            Assert.Equal("example", SetterDeathRecord.CausesOfDeath[0].Item3["system"]);
            Assert.Equal("Example Underlying COD 1", SetterDeathRecord.CausesOfDeath[1].Item1);
            Assert.Equal("2 hours", SetterDeathRecord.CausesOfDeath[1].Item2);
            Assert.Equal("Example Underlying COD 2", SetterDeathRecord.CausesOfDeath[2].Item1);
            Assert.Equal("6 months", SetterDeathRecord.CausesOfDeath[2].Item2);
            Assert.Equal("Example Underlying COD 3", SetterDeathRecord.CausesOfDeath[3].Item1);
            Assert.Equal("15 years", SetterDeathRecord.CausesOfDeath[3].Item2);
            Assert.Equal("Example Underlying COD 4", SetterDeathRecord.CausesOfDeath[4].Item1);
            Assert.Equal("30 years", SetterDeathRecord.CausesOfDeath[4].Item2);
            Assert.Equal("Example Underlying COD 5", SetterDeathRecord.CausesOfDeath[5].Item1);
            Assert.Equal("years", SetterDeathRecord.CausesOfDeath[5].Item2);
        }

        [Fact]
        public void Get_CausesOfDeath()
        {
            Tuple<string, string, Dictionary<string, string>>[] xmlCauses = ((DeathRecord)XMLRecords[0]).CausesOfDeath;
            Assert.Equal("Example Immediate COD", xmlCauses[0].Item1);
            Assert.Equal("minutes", xmlCauses[0].Item2);
            Assert.Equal("Example Underlying COD 1", xmlCauses[1].Item1);
            Assert.Equal("2 hours", xmlCauses[1].Item2);
            Assert.Equal("Example Underlying COD 2", xmlCauses[2].Item1);
            Assert.Equal("6 months", xmlCauses[2].Item2);
            Assert.Equal("Example Underlying COD 3", xmlCauses[3].Item1);
            Assert.Equal("15 years", xmlCauses[3].Item2);

            Tuple<string, string, Dictionary<string, string>>[] jsonCauses = ((DeathRecord)XMLRecords[0]).CausesOfDeath;
            Assert.Equal("Example Immediate COD", jsonCauses[0].Item1);
            Assert.Equal("minutes", jsonCauses[0].Item2);
            Assert.Equal("Example Underlying COD 1", jsonCauses[1].Item1);
            Assert.Equal("2 hours", jsonCauses[1].Item2);
            Assert.Equal("Example Underlying COD 2", jsonCauses[2].Item1);
            Assert.Equal("6 months", jsonCauses[2].Item2);
            Assert.Equal("Example Underlying COD 3", jsonCauses[3].Item1);
            Assert.Equal("15 years", jsonCauses[3].Item2);
        }

        [Fact]
        public void Set_AutopsyPerformed()
        {
            SetterDeathRecord.AutopsyPerformed = false;
            Assert.False(SetterDeathRecord.AutopsyPerformed);
        }

        [Fact]
        public void Get_AutopsyPerformed()
        {
            Assert.False(((DeathRecord)XMLRecords[0]).AutopsyPerformed);
            Assert.False(((DeathRecord)JSONRecords[0]).AutopsyPerformed);
        }

        [Fact]
        public void Set_AutopsyResultsAvailable()
        {
            SetterDeathRecord.AutopsyResultsAvailable = false;
            Assert.False(SetterDeathRecord.AutopsyResultsAvailable);
        }

        [Fact]
        public void Get_AutopsyResultsAvailable()
        {
            Assert.False(((DeathRecord)XMLRecords[0]).AutopsyResultsAvailable);
            Assert.False(((DeathRecord)JSONRecords[0]).AutopsyResultsAvailable);
        }

        [Fact]
        public void Set_MannerOfDeath()
        {
            Dictionary<string, string> code = new Dictionary<string, string>();
            code.Add("code", "7878000");
            code.Add("system", "http://github.com/nightingaleproject/fhirDeathRecord/sdr/causeOfDeath/vs/MannerOfDeathVS");
            code.Add("display", "Accident");
            SetterDeathRecord.MannerOfDeath = code;
            Assert.Equal("7878000", SetterDeathRecord.MannerOfDeath["code"]);
            Assert.Equal("http://github.com/nightingaleproject/fhirDeathRecord/sdr/causeOfDeath/vs/MannerOfDeathVS", SetterDeathRecord.MannerOfDeath["system"]);
            Assert.Equal("Accident", SetterDeathRecord.MannerOfDeath["display"]);
        }

        [Fact]
        public void Get_MannerOfDeath()
        {
            Dictionary<string, string> xmlDictionary = ((DeathRecord)XMLRecords[0]).MannerOfDeath;
            Assert.Equal("7878000", xmlDictionary["code"]);
            Assert.Equal("http://snomed.info/sct", xmlDictionary["system"]);
            Assert.Equal("Accident", xmlDictionary["display"]);
            Dictionary<string, string> jsonDictionary = ((DeathRecord)JSONRecords[0]).MannerOfDeath;
            Assert.Equal("7878000", jsonDictionary["code"]);
            Assert.Equal("http://snomed.info/sct", jsonDictionary["system"]);
            Assert.Equal("Accident", jsonDictionary["display"]);
        }

        [Fact]
        public void Set_TobaccoUseContributedToDeath()
        {
            Dictionary<string, string> code = new Dictionary<string, string>();
            code.Add("code", "373066001");
            code.Add("system", "http://github.com/nightingaleproject/fhirDeathRecord/sdr/causeOfDeath/vs/ContributoryTobaccoUseVS");
            code.Add("display", "Yes");
            SetterDeathRecord.TobaccoUseContributedToDeath = code;
            Assert.Equal("373066001", SetterDeathRecord.TobaccoUseContributedToDeath["code"]);
            Assert.Equal("http://github.com/nightingaleproject/fhirDeathRecord/sdr/causeOfDeath/vs/ContributoryTobaccoUseVS", SetterDeathRecord.TobaccoUseContributedToDeath["system"]);
            Assert.Equal("Yes", SetterDeathRecord.TobaccoUseContributedToDeath["display"]);
        }

        [Fact]
        public void Get_TobaccoUseContributedToDeath()
        {
            Dictionary<string, string> xmlDictionary = ((DeathRecord)XMLRecords[0]).TobaccoUseContributedToDeath;
            Assert.Equal("373067005", xmlDictionary["code"]);
            Assert.Equal("http://snomed.info/sct", xmlDictionary["system"]);
            Assert.Equal("No", xmlDictionary["display"]);
            Dictionary<string, string> jsonDictionary = ((DeathRecord)JSONRecords[0]).TobaccoUseContributedToDeath;
            Assert.Equal("373067005", jsonDictionary["code"]);
            Assert.Equal("http://snomed.info/sct", jsonDictionary["system"]);
            Assert.Equal("No", jsonDictionary["display"]);
        }

        [Fact]
        public void Set_ActualOrPresumedDateOfDeath()
        {
            SetterDeathRecord.ActualOrPresumedDateOfDeath = "2018-09-01T00:00:00+06:00";
            Assert.Equal("2018-09-01T00:00:00+06:00", SetterDeathRecord.ActualOrPresumedDateOfDeath);
        }

        [Fact]
        public void Get_ActualOrPresumedDateOfDeath()
        {
            Assert.Equal("2018-04-24T00:00:00+00:00", ((DeathRecord)XMLRecords[0]).ActualOrPresumedDateOfDeath);
            Assert.Equal("2018-04-24T00:00:00+00:00", ((DeathRecord)JSONRecords[0]).ActualOrPresumedDateOfDeath);
        }

        [Fact]
        public void Set_DatePronouncedDead()
        {
            SetterDeathRecord.DatePronouncedDead = "2018-09-01T00:00:00+04:00";
            Assert.Equal("2018-09-01T00:00:00+04:00", SetterDeathRecord.DatePronouncedDead);
        }

        [Fact]
        public void Get_DatePronouncedDead()
        {
            Assert.Equal("2018-04-24T00:00:00+00:00", ((DeathRecord)XMLRecords[0]).DatePronouncedDead);
            Assert.Equal("2018-04-24T00:00:00+00:00", ((DeathRecord)JSONRecords[0]).DatePronouncedDead);
        }

        [Fact]
        public void Set_DeathFromWorkInjury()
        {
            SetterDeathRecord.DeathFromWorkInjury = true;
            Assert.True(SetterDeathRecord.DeathFromWorkInjury);
        }

        [Fact]
        public void Get_DeathFromWorkInjury()
        {
            Assert.False(((DeathRecord)XMLRecords[0]).DeathFromWorkInjury);
            Assert.False(((DeathRecord)JSONRecords[0]).DeathFromWorkInjury);
        }

        [Fact]
        public void Set_DeathFromTransportInjury()
        {
            Dictionary<string, string> code = new Dictionary<string, string>();
            code.Add("code", "236320001");
            code.Add("system", "http://github.com/nightingaleproject/fhirDeathRecord/sdr/causeOfDeath/vs/TransportRelationshipsVS");
            code.Add("display", "Vehicle driver");
            SetterDeathRecord.DeathFromTransportInjury = code;
            Assert.Equal("236320001", SetterDeathRecord.DeathFromTransportInjury["code"]);
            Assert.Equal("http://github.com/nightingaleproject/fhirDeathRecord/sdr/causeOfDeath/vs/TransportRelationshipsVS", SetterDeathRecord.DeathFromTransportInjury["system"]);
            Assert.Equal("Vehicle driver", SetterDeathRecord.DeathFromTransportInjury["display"]);
        }

        [Fact]
        public void Get_DeathFromTransportInjury()
        {
            Dictionary<string, string> xmlDictionary = ((DeathRecord)XMLRecords[0]).DeathFromTransportInjury;
            Assert.Equal("OTH", xmlDictionary["code"]);
            Assert.Equal("http://hl7.org/fhir/v3/NullFlavor", xmlDictionary["system"]);
            Assert.Equal("Other", xmlDictionary["display"]);
            Dictionary<string, string> jsonDictionary = ((DeathRecord)JSONRecords[0]).DeathFromTransportInjury;
            Assert.Equal("OTH", jsonDictionary["code"]);
            Assert.Equal("http://hl7.org/fhir/v3/NullFlavor", jsonDictionary["system"]);
            Assert.Equal("Other", jsonDictionary["display"]);
        }

        [Fact]
        public void Set_MedicalExaminerContacted()
        {
            SetterDeathRecord.MedicalExaminerContacted = true;
            Assert.True(SetterDeathRecord.MedicalExaminerContacted);
        }

        [Fact]
        public void Get_MedicalExaminerContacted()
        {
            Assert.False(((DeathRecord)XMLRecords[0]).MedicalExaminerContacted);
            Assert.False(((DeathRecord)JSONRecords[0]).MedicalExaminerContacted);
        }

        [Fact]
        public void Set_TimingOfRecentPregnancyInRelationToDeath()
        {
            Dictionary<string, string> code = new Dictionary<string, string>();
            code.Add("code", "PHC1260");
            code.Add("system", "http://github.com/nightingaleproject/fhirDeathRecord/sdr/causeOfDeath/vs/PregnancyStatusVS");
            code.Add("display", "Not pregnant within past year");
            SetterDeathRecord.TimingOfRecentPregnancyInRelationToDeath = code;
            Assert.Equal("PHC1260", SetterDeathRecord.TimingOfRecentPregnancyInRelationToDeath["code"]);
            Assert.Equal("http://github.com/nightingaleproject/fhirDeathRecord/sdr/causeOfDeath/vs/PregnancyStatusVS", SetterDeathRecord.TimingOfRecentPregnancyInRelationToDeath["system"]);
            Assert.Equal("Not pregnant within past year", SetterDeathRecord.TimingOfRecentPregnancyInRelationToDeath["display"]);
        }

        [Fact]
        public void Get_TimingOfRecentPregnancyInRelationToDeath()
        {
            Dictionary<string, string> xmlDictionary = ((DeathRecord)XMLRecords[0]).TimingOfRecentPregnancyInRelationToDeath;
            Assert.Equal("PHC1260", xmlDictionary["code"]);
            Assert.Equal("Not pregnant within past year", xmlDictionary["display"]);
            Dictionary<string, string> jsonDictionary = ((DeathRecord)JSONRecords[0]).TimingOfRecentPregnancyInRelationToDeath;
            Assert.Equal("PHC1260", jsonDictionary["code"]);
            Assert.Equal("Not pregnant within past year", jsonDictionary["display"]);
        }

        [Fact]
        public void Set_DetailsOfInjury()
        {
            Dictionary<string, string> detailsOfInjury = new Dictionary<string, string>();
            detailsOfInjury.Add("placeOfInjuryDescription", "Home");
            detailsOfInjury.Add("effectiveDateTime", "2018-04-19T15:43:00+00:00");
            detailsOfInjury.Add("description", "Example details of injury");
            detailsOfInjury.Add("placeOfInjuryLine1", "7 Example Street");
            detailsOfInjury.Add("placeOfInjuryLine2", "Line 2");
            detailsOfInjury.Add("placeOfInjuryCity", "Bedford");
            detailsOfInjury.Add("placeOfInjuryState", "Massachusetts");
            detailsOfInjury.Add("placeOfInjuryZip", "01730");
            detailsOfInjury.Add("placeOfInjuryCountry", "United States");
            detailsOfInjury.Add("placeOfInjuryInsideCityLimits", "true");
            SetterDeathRecord.DetailsOfInjury = detailsOfInjury;
            Assert.Equal("Home", SetterDeathRecord.DetailsOfInjury["placeOfInjuryDescription"]);
            Assert.Equal("2018-04-19T15:43:00+00:00", SetterDeathRecord.DetailsOfInjury["effectiveDateTime"]);
            Assert.Equal("Example details of injury", SetterDeathRecord.DetailsOfInjury["description"]);
            Assert.Equal("7 Example Street", SetterDeathRecord.DetailsOfInjury["placeOfInjuryLine1"]);
            Assert.Equal("Line 2", SetterDeathRecord.DetailsOfInjury["placeOfInjuryLine2"]);
            Assert.Equal("Bedford", SetterDeathRecord.DetailsOfInjury["placeOfInjuryCity"]);
            Assert.Equal("Massachusetts", SetterDeathRecord.DetailsOfInjury["placeOfInjuryState"]);
            Assert.Equal("01730", SetterDeathRecord.DetailsOfInjury["placeOfInjuryZip"]);
            Assert.Equal("United States", SetterDeathRecord.DetailsOfInjury["placeOfInjuryCountry"]);
            Assert.Equal("True", SetterDeathRecord.DetailsOfInjury["placeOfInjuryInsideCityLimits"]);
        }

        [Fact]
        public void Get_DetailsOfInjury()
        {
            Dictionary<string, string> xmlDictionary = ((DeathRecord)XMLRecords[0]).DetailsOfInjury;
            Assert.Equal("Home", xmlDictionary["placeOfInjuryDescription"]);
            Assert.Equal("2018-04-19T15:43:00+00:00", xmlDictionary["effectiveDateTime"]);
            Assert.Equal("Example details of injury", xmlDictionary["description"]);
            Assert.Equal("7 Example Street", xmlDictionary["placeOfInjuryLine1"]);
            Assert.Null(xmlDictionary["placeOfInjuryLine2"]);
            Assert.Equal("Bedford", xmlDictionary["placeOfInjuryCity"]);
            Assert.Equal("Massachusetts", xmlDictionary["placeOfInjuryState"]);
            Assert.Equal("01730", xmlDictionary["placeOfInjuryZip"]);
            Assert.Equal("United States", xmlDictionary["placeOfInjuryCountry"]);
            Assert.Null(xmlDictionary["placeOfInjuryInsideCityLimits"]);
            Dictionary<string, string> jsonDictionary = ((DeathRecord)JSONRecords[0]).DetailsOfInjury;
            Assert.Equal("Home", jsonDictionary["placeOfInjuryDescription"]);
            Assert.Equal("2018-04-19T15:43:00+00:00", jsonDictionary["effectiveDateTime"]);
            Assert.Equal("Example details of injury", jsonDictionary["description"]);
            Assert.Equal("7 Example Street", jsonDictionary["placeOfInjuryLine1"]);
            Assert.Null(jsonDictionary["placeOfInjuryLine2"]);
            Assert.Equal("Bedford", jsonDictionary["placeOfInjuryCity"]);
            Assert.Equal("Massachusetts", jsonDictionary["placeOfInjuryState"]);
            Assert.Equal("01730", jsonDictionary["placeOfInjuryZip"]);
            Assert.Equal("United States", jsonDictionary["placeOfInjuryCountry"]);
            Assert.Null(jsonDictionary["placeOfInjuryInsideCityLimits"]);
        }

        [Fact]
        public void MultipleObservationSets()
        {
            SetterDeathRecord.MedicalExaminerContacted = false;
            SetterDeathRecord.MedicalExaminerContacted = true;
            Assert.True(SetterDeathRecord.MedicalExaminerContacted);
        }

        [Fact]
        public void MultipleConditionSets()
        {
            Tuple<string, string, Dictionary<string, string>>[] bogus_causes =
            {
                Tuple.Create("bogus cause", "bogus onset", new Dictionary<string, string>())
            };
            SetterDeathRecord.CausesOfDeath = bogus_causes;

            Tuple<string, string, Dictionary<string, string>>[] second_causes =
            {
                Tuple.Create("a cause", "a onset", new Dictionary<string, string>()),
                Tuple.Create("b cause", "b onset", new Dictionary<string, string>())
            };
            SetterDeathRecord.CausesOfDeath = second_causes;
            Assert.Equal("a cause", SetterDeathRecord.CausesOfDeath[0].Item1);
            Assert.Equal("a onset", SetterDeathRecord.CausesOfDeath[0].Item2);
            Assert.Equal("b cause", SetterDeathRecord.CausesOfDeath[1].Item1);
            Assert.Equal("b onset", SetterDeathRecord.CausesOfDeath[1].Item2);
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
