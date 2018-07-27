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

        public DeathRecord_Should()
        {
            XMLRecords = new ArrayList();
            XMLRecords.Add(new DeathRecord(File.ReadAllText(FixturePath("fixtures/xml/1.xml"))));
            JSONRecords = new ArrayList();
            JSONRecords.Add(new DeathRecord(File.ReadAllText(FixturePath("fixtures/json/1.json"))));
        }

        [Fact]
        public void FailGivenInvalidRecord()
        {
            Exception ex = Assert.Throws<System.ArgumentException>(() => new DeathRecord("foobar"));
            Assert.Equal("Record is neither valid XML nor JSON.", ex.Message);
        }

        [Fact]
        public void Get_GivenName()
        {
            Assert.Equal("Example", ((DeathRecord)XMLRecords[0]).GivenName);
            Assert.Equal("Example", ((DeathRecord)JSONRecords[0]).GivenName);
        }

        [Fact]
        public void Get_LastName()
        {
            Assert.Equal("Person", ((DeathRecord)XMLRecords[0]).LastName);
            Assert.Equal("Person", ((DeathRecord)JSONRecords[0]).LastName);
        }

        public void Get_Gender()
        {
            Assert.Equal("male", ((DeathRecord)XMLRecords[0]).Gender);
            Assert.Equal("male", ((DeathRecord)JSONRecords[0]).Gender);
        }

        [Fact]
        public void Get_SSN()
        {
            Assert.Equal("111223333", ((DeathRecord)XMLRecords[0]).SSN);
            Assert.Equal("111223333", ((DeathRecord)JSONRecords[0]).SSN);
        }

        public void Get_DateOfBirth()
        {
            Assert.Equal("", ((DeathRecord)XMLRecords[0]).DateOfBirth);
            Assert.Equal("", ((DeathRecord)JSONRecords[0]).DateOfBirth);
        }

        public void Get_DateOfDeath()
        {
            Assert.Equal("1970-04-24", ((DeathRecord)XMLRecords[0]).DateOfDeath);
            Assert.Equal("1970-04-24", ((DeathRecord)JSONRecords[0]).DateOfDeath);
        }

        [Fact]
        public void Get_CertifierGivenName()
        {
            Assert.Equal("Example", ((DeathRecord)XMLRecords[0]).CertifierGivenName);
            Assert.Equal("Example", ((DeathRecord)JSONRecords[0]).CertifierGivenName);
        }

        [Fact]
        public void Get_CertifierLastName()
        {
            Assert.Equal("Doctor", ((DeathRecord)XMLRecords[0]).CertifierLastName);
            Assert.Equal("Doctor", ((DeathRecord)JSONRecords[0]).CertifierLastName);
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
