using System.Collections.Generic;
using FhirDeathRecord.Section;
using Xunit;

namespace FhirDeathRecord.Tests.Section
{
    public class FuneralHome_Tests
    {
        private FuneralHome Instance { get; set; }

        public FuneralHome_Tests()
        {
            Instance = new FuneralHome();
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
    }
}