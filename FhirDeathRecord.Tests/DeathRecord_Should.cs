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
        public void Set_GivenName()
        {
            string[] expected = {"Example", "Middle"};
            SetterDeathRecord.GivenName = expected;
            Assert.Equal(expected, SetterDeathRecord.GivenName);
        }

        [Fact]
        public void Get_GivenName()
        {
            string[] expected = {"Example", "Middle"};
            Assert.Equal(expected, ((DeathRecord)XMLRecords[0]).GivenName);
            Assert.Equal(expected, ((DeathRecord)JSONRecords[0]).GivenName);
        }

        [Fact]
        public void Set_LastName()
        {
            SetterDeathRecord.LastName = "Last";
            Assert.Equal("Last", SetterDeathRecord.LastName);
        }

        [Fact]
        public void Get_LastName()
        {
            Assert.Equal("Person", ((DeathRecord)XMLRecords[0]).LastName);
            Assert.Equal("Person", ((DeathRecord)JSONRecords[0]).LastName);
        }

        [Fact]
        public void Get_Gender()
        {
            Assert.Equal("male", ((DeathRecord)XMLRecords[0]).Gender);
            Assert.Equal("male", ((DeathRecord)JSONRecords[0]).Gender);
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
        public void Set_CertifierGivenName()
        {
            string[] expected = {"Example", "Middle", "Middle 2", "Middle 3"};
            SetterDeathRecord.CertifierGivenName = expected;
            Assert.Equal(expected, SetterDeathRecord.CertifierGivenName);
        }

        [Fact]
        public void Get_CertifierGivenName()
        {
            string[] expected = {"Example", "Middle"};
            Assert.Equal(expected, ((DeathRecord)XMLRecords[0]).CertifierGivenName);
            Assert.Equal(expected, ((DeathRecord)JSONRecords[0]).CertifierGivenName);
        }

        [Fact]
        public void Set_CertifierLastName()
        {
            SetterDeathRecord.LastName = "Doctor";
            Assert.Equal("Doctor", SetterDeathRecord.LastName);
        }

        [Fact]
        public void Get_CertifierLastName()
        {
            Assert.Equal("Doctor", ((DeathRecord)XMLRecords[0]).CertifierLastName);
            Assert.Equal("Doctor", ((DeathRecord)JSONRecords[0]).CertifierLastName);
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
        public void Get_ContributingConditions()
        {
            Assert.Equal("Example Contributing Condition", ((DeathRecord)XMLRecords[0]).ContributingConditions);
            Assert.Equal("Example Contributing Condition", ((DeathRecord)JSONRecords[0]).ContributingConditions);
        }

        [Fact]
        public void Get_CausesOfDeath()
        {
            Tuple<string, string>[] xmlCauses = ((DeathRecord)XMLRecords[0]).CausesOfDeath;
            Assert.Equal("Example Immediate COD", xmlCauses[0].Item1);
            Assert.Equal("minutes", xmlCauses[0].Item2);
            Assert.Equal("Example Underlying COD 1", xmlCauses[1].Item1);
            Assert.Equal("2 hours", xmlCauses[1].Item2);
            Assert.Equal("Example Underlying COD 2", xmlCauses[2].Item1);
            Assert.Equal("6 months", xmlCauses[2].Item2);
            Assert.Equal("Example Underlying COD 3", xmlCauses[3].Item1);
            Assert.Equal("15 years", xmlCauses[3].Item2);

            Tuple<string, string>[] jsonCauses = ((DeathRecord)XMLRecords[0]).CausesOfDeath;
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
        public void Get_AutopsyPerformed()
        {
            Assert.False(((DeathRecord)XMLRecords[0]).AutopsyPerformed);
            Assert.False(((DeathRecord)JSONRecords[0]).AutopsyPerformed);
        }

        [Fact]
        public void Get_AutopsyResultsAvailable()
        {
            Assert.False(((DeathRecord)XMLRecords[0]).AutopsyResultsAvailable);
            Assert.False(((DeathRecord)JSONRecords[0]).AutopsyResultsAvailable);
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
