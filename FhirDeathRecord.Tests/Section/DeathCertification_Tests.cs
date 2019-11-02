using FhirDeathRecord.Section;
using Xunit;

namespace FhirDeathRecord.Tests.Section
{
    public class DeathCertification_Tests
    {
        [Fact]
        public void UrlShallBeUuid()
        {
            var target = new DeathCertification();
            Assert.True(target.Url.StartsWith("urn:uuid:"));
        }

       [Fact]
        public void CertifiedTimeShallBeString()
        {
            var target = new DeathCertification();
            target.CertifiedTime = "2019-01-29T16:48:06.498822-05:00";
            Assert.Equal("2019-01-29T16:48:06.498822-05:00", target.CertifiedTime);
        }
    }
}