using System;
using Xunit;

namespace FhirDeathRecord.Tests
{
    public class DeathRecord_Should
    {
        [Fact]
        public void FailGivenInvalidRecord()
        {
            Exception ex = Assert.Throws<System.ArgumentException>(() => new DeathRecord("foobar"));
            Assert.Equal("Record is neither valid XML nor JSON.", ex.Message);
        }
    }
}
