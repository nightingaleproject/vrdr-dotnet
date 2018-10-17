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
        public void Set_Gender()
        {
            SetterDeathRecord.Gender = "male";
            Assert.Equal("male", SetterDeathRecord.Gender);
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
        public void Set_Residence()
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add("residenceLine1", "19 Example Street");
            dictionary.Add("residenceLine2", "Line 2");
            dictionary.Add("residenceCity", "Bedford");
            dictionary.Add("residenceCounty", "Middlesex");
            dictionary.Add("residenceState", "Massachusetts");
            dictionary.Add("residenceZip", "01730");
            dictionary.Add("residenceCountry", "United States");
            dictionary.Add("residenceInsideCityLimits", "True");
            SetterDeathRecord.Residence = dictionary;
            Assert.Equal("19 Example Street", SetterDeathRecord.Residence["residenceLine1"]);
            Assert.Equal("Line 2", SetterDeathRecord.Residence["residenceLine2"]);
            Assert.Equal("Bedford", SetterDeathRecord.Residence["residenceCity"]);
            Assert.Equal("Middlesex", SetterDeathRecord.Residence["residenceCounty"]);
            Assert.Equal("Massachusetts", SetterDeathRecord.Residence["residenceState"]);
            Assert.Equal("01730", SetterDeathRecord.Residence["residenceZip"]);
            Assert.Equal("United States", SetterDeathRecord.Residence["residenceCountry"]);
            Assert.Equal("True", SetterDeathRecord.Residence["residenceInsideCityLimits"]);
        }

        [Fact]
        public void Get_Residence()
        {
            Dictionary<string, string> xmlDictionary = ((DeathRecord)XMLRecords[0]).Residence;
            Assert.Equal("1 Example Street", xmlDictionary["residenceLine1"]);
            Assert.Equal("Boston", xmlDictionary["residenceCity"]);
            Assert.Equal("Suffolk", xmlDictionary["residenceCounty"]);
            Assert.Equal("Massachusetts", xmlDictionary["residenceState"]);
            Assert.Equal("02101", xmlDictionary["residenceZip"]);
            Assert.Equal("United States", xmlDictionary["residenceCountry"]);
            Dictionary<string, string> jsonDictionary = ((DeathRecord)JSONRecords[0]).Residence;
            Assert.Equal("1 Example Street", jsonDictionary["residenceLine1"]);
            Assert.Equal("Boston", jsonDictionary["residenceCity"]);
            Assert.Equal("Suffolk", jsonDictionary["residenceCounty"]);
            Assert.Equal("Massachusetts", jsonDictionary["residenceState"]);
            Assert.Equal("02101", jsonDictionary["residenceZip"]);
            Assert.Equal("United States", jsonDictionary["residenceCountry"]);
        }

        [Fact]
        public void Set_SSN()
        {
            SetterDeathRecord.SSN = "111223333";
            Assert.Equal("111223333", SetterDeathRecord.SSN);
        }

        [Fact]
        public void Get_SSN()
        {
            Assert.Equal("111223333", ((DeathRecord)XMLRecords[0]).SSN);
            Assert.Equal("111223333", ((DeathRecord)JSONRecords[0]).SSN);
        }

        [Fact]
        public void Set_Ethnicity()
        {
            Tuple<string, string>[] ethnicity = { Tuple.Create("Non Hispanic or Latino", "2186-5"), Tuple.Create("Salvadoran", "2161-8") };
            SetterDeathRecord.Ethnicity = ethnicity;
            Assert.Equal(ethnicity[0], SetterDeathRecord.Ethnicity[0]);
            Assert.Equal(ethnicity[1], SetterDeathRecord.Ethnicity[1]);
        }

        [Fact]
        public void Get_Ethnicity()
        {
            Tuple<string, string>[] ethnicity =
            {
                Tuple.Create("Non Hispanic or Latino", "2186-5"),
                Tuple.Create("Salvadoran", "2161-8"),
            };

            Assert.Equal(ethnicity, ((DeathRecord)XMLRecords[0]).Ethnicity);
            Assert.Equal(ethnicity, ((DeathRecord)JSONRecords[0]).Ethnicity);
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
            Tuple<string, string>[] race =
            {
                Tuple.Create("White", "2106-3"),
                Tuple.Create("Native Hawaiian or Other Pacific Islander", "2076-8"),
                Tuple.Create("Asian", "2028-5"),
                Tuple.Create("American Indian or Alaskan Native", "1002-5"),
                Tuple.Create("Black or African American", "2054-5"),
            };

            Assert.Equal(race, ((DeathRecord)XMLRecords[0]).Race);
            Assert.Equal(race, ((DeathRecord)JSONRecords[0]).Race);
        }

        [Fact]
        public void Set_DateOfBirth()
        {
            SetterDeathRecord.DateOfBirth = "1970-04-24";
            Assert.Equal("1970-04-24", SetterDeathRecord.DateOfBirth);
        }

        [Fact]
        public void Get_DateOfBirth()
        {
            Assert.Equal("1970-04-24", ((DeathRecord)XMLRecords[0]).DateOfBirth);
            Assert.Equal("1970-04-24", ((DeathRecord)JSONRecords[0]).DateOfBirth);
        }

        [Fact]
        public void Set_DateOfDeath()
        {
            SetterDeathRecord.DateOfDeath = "1970-04-24";
            Assert.Equal("1970-04-24", SetterDeathRecord.DateOfDeath);
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
        public void Set_MaritalStatus()
        {
            Dictionary<string, string> code = new Dictionary<string, string>();
            code.Add("code", "S");
            code.Add("system", "http://hl7.org/fhir/v3/MaritalStatus	");
            code.Add("display", "Never Married");
            SetterDeathRecord.MaritalStatus = code;
            Assert.Equal("S", SetterDeathRecord.MaritalStatus["code"]);
            Assert.Equal("http://hl7.org/fhir/v3/MaritalStatus	", SetterDeathRecord.MaritalStatus["system"]);
            Assert.Equal("Never Married", SetterDeathRecord.MaritalStatus["display"]);
        }

        [Fact]
        public void Get_MaritalStatus()
        {
            Dictionary<string, string> xmlDictionary = ((DeathRecord)XMLRecords[0]).MaritalStatus;
            Assert.Equal("S", xmlDictionary["code"]);
            Assert.Equal("http://hl7.org/fhir/v3/MaritalStatus", xmlDictionary["system"]);
            Dictionary<string, string> jsonDictionary = ((DeathRecord)JSONRecords[0]).MaritalStatus;
            Assert.Equal("S", jsonDictionary["code"]);
            Assert.Equal("http://hl7.org/fhir/v3/MaritalStatus", jsonDictionary["system"]);
        }

        [Fact]
        public void Set_Education()
        {
            Dictionary<string, string> code = new Dictionary<string, string>();
            code.Add("code", "PHC1453");
            code.Add("system", "http://github.com/nightingaleproject/fhirDeathRecord/sdr/decedent/cs/EducationCS");
            code.Add("display", "Bachelor's Degree");
            SetterDeathRecord.Education = code;
            Assert.Equal("PHC1453", SetterDeathRecord.Education["code"]);
            Assert.Equal("http://github.com/nightingaleproject/fhirDeathRecord/sdr/decedent/cs/EducationCS", SetterDeathRecord.Education["system"]);
            Assert.Equal("Bachelor's Degree", SetterDeathRecord.Education["display"]);
        }

        [Fact]
        public void Get_Education()
        {
            Dictionary<string, string> xmlDictionary = ((DeathRecord)XMLRecords[0]).Education;
            Assert.Equal("PHC1454", xmlDictionary["code"]);
            Dictionary<string, string> jsonDictionary = ((DeathRecord)JSONRecords[0]).Education;
            Assert.Equal("PHC1454", jsonDictionary["code"]);
        }

        [Fact]
        public void Set_Age()
        {
            SetterDeathRecord.Age = "100";
            Assert.Equal("100", SetterDeathRecord.Age);
        }

        [Fact]
        public void Get_Age()
        {
            Assert.Equal("48", ((DeathRecord)XMLRecords[0]).Age);
            Assert.Equal("48", ((DeathRecord)JSONRecords[0]).Age);
        }

        [Fact]
        public void Set_Occupation()
        {
            Dictionary<string, string> occupation = new Dictionary<string, string>();
            occupation.Add("jobDescription", "Software Engineer");
            occupation.Add("industryDescription", "Information Technology");
            SetterDeathRecord.Occupation = occupation;
            Assert.Equal("Software Engineer", SetterDeathRecord.Occupation["jobDescription"]);
            Assert.Equal("Information Technology", SetterDeathRecord.Occupation["industryDescription"]);
        }

        [Fact]
        public void Get_Occupation()
        {
            Assert.Equal("Example usual occupation.", ((DeathRecord)XMLRecords[0]).Occupation["jobDescription"]);
            Assert.Equal("Example kind of business.", ((DeathRecord)XMLRecords[0]).Occupation["industryDescription"]);
            Assert.Equal("Example usual occupation.", ((DeathRecord)JSONRecords[0]).Occupation["jobDescription"]);
            Assert.Equal("Example kind of business.", ((DeathRecord)JSONRecords[0]).Occupation["industryDescription"]);
        }

        [Fact]
        public void Set_ServedInArmedForces()
        {
            SetterDeathRecord.ServedInArmedForces = false;
            Assert.False(SetterDeathRecord.ServedInArmedForces);
        }

        [Fact]
        public void Get_ServedInArmedForces()
        {
            Assert.False(((DeathRecord)XMLRecords[0]).ServedInArmedForces);
            Assert.False(((DeathRecord)JSONRecords[0]).ServedInArmedForces);
        }

        [Fact]
        public void Set_Disposition()
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
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
            SetterDeathRecord.Disposition = dictionary;
            Assert.Equal("449971000124106", SetterDeathRecord.Disposition["dispositionTypeCode"]);
            Assert.Equal("http://snomed.info/sct", SetterDeathRecord.Disposition["dispositionTypeSystem"]);
            Assert.Equal("Burial", SetterDeathRecord.Disposition["dispositionTypeDisplay"]);
            Assert.Equal("Example disposition place name", SetterDeathRecord.Disposition["dispositionPlaceName"]);
            Assert.Equal("100 Example Street", SetterDeathRecord.Disposition["dispositionPlaceLine1"]);
            Assert.Equal("Line 2", SetterDeathRecord.Disposition["dispositionPlaceLine2"]);
            Assert.Equal("Bedford", SetterDeathRecord.Disposition["dispositionPlaceCity"]);
            Assert.Equal("Middlesex", SetterDeathRecord.Disposition["dispositionPlaceCounty"]);
            Assert.Equal("Massachusetts", SetterDeathRecord.Disposition["dispositionPlaceState"]);
            Assert.Equal("01730", SetterDeathRecord.Disposition["dispositionPlaceZip"]);
            Assert.Equal("United States", SetterDeathRecord.Disposition["dispositionPlaceCountry"]);
            Assert.Equal("True", SetterDeathRecord.Disposition["dispositionPlaceInsideCityLimits"]);
            Assert.Equal("Example funeral facility name", SetterDeathRecord.Disposition["funeralFacilityName"]);
            Assert.Equal("50 Example Street", SetterDeathRecord.Disposition["funeralFacilityLine1"]);
            Assert.Equal("Line 2a", SetterDeathRecord.Disposition["funeralFacilityLine2"]);
            Assert.Equal("Watertown", SetterDeathRecord.Disposition["funeralFacilityCity"]);
            Assert.Equal("Middlesex", SetterDeathRecord.Disposition["funeralFacilityCounty"]);
            Assert.Equal("Massachusetts", SetterDeathRecord.Disposition["funeralFacilityState"]);
            Assert.Equal("02472", SetterDeathRecord.Disposition["funeralFacilityZip"]);
            Assert.Equal("United States", SetterDeathRecord.Disposition["funeralFacilityCountry"]);
            Assert.Equal("False", SetterDeathRecord.Disposition["funeralFacilityInsideCityLimits"]);
        }

        [Fact]
        public void Get_Disposition()
        {
            Dictionary<string, string> xmlDictionary = ((DeathRecord)XMLRecords[0]).Disposition;
            Assert.Equal("449971000124106", xmlDictionary["dispositionTypeCode"]);
            Assert.Equal("Example Cemetery", xmlDictionary["dispositionPlaceName"]);
            Assert.Equal("Boston", xmlDictionary["dispositionPlaceCity"]);
            Assert.Equal("Massachusetts", xmlDictionary["dispositionPlaceState"]);
            Assert.Equal("United States", xmlDictionary["dispositionPlaceCountry"]);
            Assert.Equal("Example Funeral Home", xmlDictionary["funeralFacilityName"]);
            Assert.Equal("2 Example Street", xmlDictionary["funeralFacilityLine1"]);
            Assert.Equal("Boston", xmlDictionary["funeralFacilityCity"]);
            Assert.Equal("Massachusetts", xmlDictionary["funeralFacilityState"]);
            Assert.Equal("02101", xmlDictionary["funeralFacilityZip"]);
            Assert.Equal("United States", xmlDictionary["funeralFacilityCountry"]);

            Dictionary<string, string> jsonDictionary = ((DeathRecord)JSONRecords[0]).Disposition;
            Assert.Equal("449971000124106", jsonDictionary["dispositionTypeCode"]);
            Assert.Equal("Example Cemetery", jsonDictionary["dispositionPlaceName"]);
            Assert.Equal("Boston", jsonDictionary["dispositionPlaceCity"]);
            Assert.Equal("Massachusetts", jsonDictionary["dispositionPlaceState"]);
            Assert.Equal("United States", jsonDictionary["dispositionPlaceCountry"]);
            Assert.Equal("Example Funeral Home", jsonDictionary["funeralFacilityName"]);
            Assert.Equal("2 Example Street", jsonDictionary["funeralFacilityLine1"]);
            Assert.Equal("Boston", jsonDictionary["funeralFacilityCity"]);
            Assert.Equal("Massachusetts", jsonDictionary["funeralFacilityState"]);
            Assert.Equal("02101", jsonDictionary["funeralFacilityZip"]);
            Assert.Equal("United States", jsonDictionary["funeralFacilityCountry"]);
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
        public void Set_CertifierAddress()
        {
            Dictionary<string, string> address = new Dictionary<string, string>();
            address.Add("street", "123 Test Street");
            address.Add("city", "Boston");
            address.Add("state", "Massachusetts");
            address.Add("zip", "12345");
            SetterDeathRecord.CertifierAddress = address;
            Assert.Equal("123 Test Street", SetterDeathRecord.CertifierAddress["street"]);
            Assert.Equal("Boston", SetterDeathRecord.CertifierAddress["city"]);
            Assert.Equal("Massachusetts", SetterDeathRecord.CertifierAddress["state"]);
            Assert.Equal("12345", SetterDeathRecord.CertifierAddress["zip"]);
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
        public void Set_CertifierType()
        {
            Dictionary<string, string> code = new Dictionary<string, string>();
            code.Add("code", "434651000124107");
            code.Add("display", "Physician (Pronouncer and Certifier)");
            SetterDeathRecord.BirthSex = code;
            Assert.Equal("434651000124107", SetterDeathRecord.BirthSex["code"]);
            Assert.Equal("Physician (Pronouncer and Certifier)", SetterDeathRecord.BirthSex["display"]);
        }

        [Fact]
        public void Get_CertifierType()
        {
            Assert.Equal("Physician (Pronouncer and Certifier)", ((DeathRecord)XMLRecords[0]).CertifierType["display"]);
            Assert.Equal("434651000124107", ((DeathRecord)XMLRecords[0]).CertifierType["code"]);

            Assert.Equal("Physician (Pronouncer and Certifier)", ((DeathRecord)JSONRecords[0]).CertifierType["display"]);
            Assert.Equal("434651000124107", ((DeathRecord)JSONRecords[0]).CertifierType["code"]);
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
