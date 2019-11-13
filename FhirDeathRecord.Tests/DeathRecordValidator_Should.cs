using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using Xunit;

namespace FhirDeathRecord.Tests
{
    public class DeathRecordValidator_Should
    {
        private ArrayList XMLRecords;

        private ArrayList JSONRecords;

        private DeathRecordValidator deathRecordValidator;

        public DeathRecordValidator_Should()
        {
            XMLRecords = new ArrayList();
            XMLRecords.Add(new DeathRecord(File.ReadAllText(FixturePath("fixtures/xml/1.xml"))));
            JSONRecords = new ArrayList();
            JSONRecords.Add(new DeathRecord(File.ReadAllText(FixturePath("fixtures/json/1.json"))));
            deathRecordValidator = new DeathRecordValidator();
        }

        [Fact]
        public void Validate_Decedent()
        {
            deathRecordValidator.Validate(((DeathRecord)JSONRecords[0]));
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
