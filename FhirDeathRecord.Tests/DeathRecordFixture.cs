using System.Collections;
using System.IO;

namespace FhirDeathRecord.Tests
{
    public class DeathRecordFixture
    {
        internal ArrayList XMLRecords { get; set; }

        internal ArrayList JSONRecords { get; set; }
        public DeathRecordFixture()
        {
            XMLRecords = new ArrayList()
            {
                new DeathRecord(File.ReadAllText(FixturePath("fixtures/xml/1.xml")))
            };
            JSONRecords = new ArrayList()
            {
                new DeathRecord(File.ReadAllText(FixturePath("fixtures/json/1.json")))
            };

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