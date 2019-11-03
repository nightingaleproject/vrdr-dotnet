using System.Collections.Generic;
using FhirDeathRecord.Section;
using Xunit;

namespace FhirDeathRecord.Tests.Section
{
    public class InterestedParty_Tests
    {
        private InterestedParty Instance { get; set; }

        public InterestedParty_Tests()
        {
            Instance = new InterestedParty();
        }

        [Fact]
        public void SetAddressOk()
        {
            var expected = new Dictionary<string, string>
            {
                { "addressLine1", "12 Example Street" },
                { "addressLine2", "Line 2" },
                { "addressCity", "Bedford" },
                { "addressCounty", "Middlesex" },
                { "addressState", "Massachusetts" },
                { "addressZip", "01730" },
                { "addressCountry", "United States" }
            };

            Instance.Address = expected;

            Assert.Equal(expected, Instance.Address);
        }


        [Fact]
        public void SetIdentifierOk()
        {
            const string expected = "123abc";

            Instance.Identifier = expected;

            Assert.Equal(expected, Instance.Identifier);
        }

        [Fact]
        public void SetNameOk()
        {
            const string expected = "123abc123xyz";

            Instance.Name = expected;

            Assert.Equal(expected, Instance.Name);
        }

        [Fact]
        public void SetTypeOk()
        {
            var expected = new Dictionary<string, string>
            {
                { "code", "prov" },
                { "system", "http://terminology.hl7.org/CodeSystem/organization-type" },
                { "display", "Healthcare Provider" }
            };

            Instance.Type = expected;

            Assert.Equal(expected, Instance.Type);
        }
    }
}