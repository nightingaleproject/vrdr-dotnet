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
    public class DemographicCodedContentBundle_Should
    {
        private List<DemographicCodedContentBundle> XMLRecords;

        private List<DemographicCodedContentBundle> JSONRecords;

        private DemographicCodedContentBundle SetterContentBundle;

        public DemographicCodedContentBundle_Should()
        {
            XMLRecords = new List<DemographicCodedContentBundle>();
            // XMLRecords.Add(new DeathRecord(File.ReadAllText(FixturePath("fixtures/xml/DeathRecord1.xml"))));
            JSONRecords = new List<DemographicCodedContentBundle>();
            //JSONRecords.Add(new DeathRecord(File.ReadAllText(FixturePath("fixtures/json/DeathRecord1.json"))));
            SetterContentBundle = new DemographicCodedContentBundle();
        }

        [Fact]
        public void BundleGetsInitialized()
        {
            DemographicCodedContentBundle demographicContentBundle = new DemographicCodedContentBundle();
            Assert.NotNull(demographicContentBundle.GetBundle());
        }

        [Fact]
        public void ProvidedBundleGetsInitialized()
        {
            Bundle bundle = new Bundle();
            DemographicCodedContentBundle demographicContentBundle = new DemographicCodedContentBundle(bundle);
            Assert.Equal(bundle, demographicContentBundle.GetBundle());
        }

        [Fact]
        public void ProfileUrlIsCorrect()
        {
            DemographicCodedContentBundle demographicContentBundle = new DemographicCodedContentBundle();
            Assert.Equal(ProfileURL.DemographicCodedContentBundle, demographicContentBundle.GetContentType());
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
