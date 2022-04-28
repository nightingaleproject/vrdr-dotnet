using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Xunit;

using System.Linq;

namespace VRDR.Tests
{
    public class CauseOfDeathCodedContentBundle_Should
    {
        private List<CauseOfDeathCodedContentBundle> XMLRecords;

        private List<CauseOfDeathCodedContentBundle> JSONRecords;

        private DemographicCodedContentBundle SetterContentBundle;

        public CauseOfDeathCodedContentBundle_Should()
        {
            XMLRecords = new List<CauseOfDeathCodedContentBundle>();
            // XMLRecords.Add(new DeathRecord(File.ReadAllText(FixturePath("fixtures/xml/DeathRecord1.xml"))));
            JSONRecords = new List<CauseOfDeathCodedContentBundle>();
            //JSONRecords.Add(new DeathRecord(File.ReadAllText(FixturePath("fixtures/json/DeathRecord1.json"))));
            SetterContentBundle = new DemographicCodedContentBundle();
        }

        [Fact]
        public void BundleGetsInitialized()
        {
            CauseOfDeathCodedContentBundle causeOfDeathCodedContentBundle = new CauseOfDeathCodedContentBundle();
            Assert.NotNull(causeOfDeathCodedContentBundle.GetBundle());
        }

        [Fact]
        public void ProvidedBundleGetsInitialized()
        {
            Bundle bundle = new Bundle();
            CauseOfDeathCodedContentBundle causeOfDeathCodedContentBundle = new CauseOfDeathCodedContentBundle(bundle);
            Assert.Equal(bundle, causeOfDeathCodedContentBundle.GetBundle());
        }

        [Fact]
        public void ProfileUrlIsCorrect()
        {
            CauseOfDeathCodedContentBundle causeOfDeathCodedContentBundle = new CauseOfDeathCodedContentBundle();
            Assert.Equal(ProfileURL.CauseOfDeathCodedContentBundle, causeOfDeathCodedContentBundle.GetContentType());
        }

        // TODO: Test cases

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
