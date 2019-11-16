using System.Collections.Generic;
using FhirDeathRecord.Section;
using Xunit;

namespace FhirDeathRecord.Tests.Section
{
    public class DispositionLocation_Tests
    {
        private DispositionLocation Instance { get; set; }

        public DispositionLocation_Tests()
        {
            Instance = new DispositionLocation();
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
        public void SetNameOk()
        {
            const string expected = "123abc123xyz";

            Instance.Name = expected;

            Assert.Equal(expected, Instance.Name);
        }
    }
}