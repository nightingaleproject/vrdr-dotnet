using FhirDeathRecord.Section;
using Xunit;

namespace FhirDeathRecord.Tests.Section
{
    public class DeathCertification_Tests
    {
        private DeathCertification Instance {get; set;}

        public DeathCertification_Tests()
        {
            Instance = new DeathCertification();
        }

        [Fact]
        public void UrlShallBeUuid()
        {
            Assert.True(Instance.Url.StartsWith("urn:uuid:"));
        }

       [Fact]
        public void CertifiedTimeShallBeString()
        {
            const string expected = "2019-01-29T16:48:06.498822-05:00";

            Instance.CertifiedTime = expected;

            Assert.Equal(expected, Instance.CertifiedTime);
        }
    }
}