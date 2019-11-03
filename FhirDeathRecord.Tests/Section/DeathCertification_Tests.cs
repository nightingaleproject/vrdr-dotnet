using System.Collections.Generic;
using FhirDeathRecord.Extensions;
using FhirDeathRecord.Section;
using Hl7.Fhir.Model;
using Xunit;

namespace FhirDeathRecord.Tests.Section
{
    public class DeathCertification_Tests
    {
        [Fact]
        public void UrlShallBeUuid()
        {
            var instance = new DeathCertification();
            Assert.True(instance.Url.StartsWith("urn:uuid:"));
        }

       [Fact]
        public void CertifiedTimeShallBeString()
        {
            var instance = new DeathCertification();
            instance.CertifiedTime = "2019-01-29T16:48:06.498822-05:00";
            Assert.Equal("2019-01-29T16:48:06.498822-05:00", instance.CertifiedTime);
        }
    }
}