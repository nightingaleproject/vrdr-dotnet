using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using Xunit;

namespace VRDR.Tests
{
    public class CauseCodes_Should
    {
        private ArrayList JSONRecords;

        private CauseCodes SetterCauseCodes;

        public CauseCodes_Should()
        {
            JSONRecords = new ArrayList();
            JSONRecords.Add(new CauseCodes(File.ReadAllText(FixturePath("fixtures/json/causecodes.json"))));
            SetterCauseCodes = new CauseCodes();
        }

        [Fact]
        public void Set_Identifier()
        {
            SetterCauseCodes.Identifier = "321";
            Assert.Equal("321", SetterCauseCodes.Identifier);
        }

        [Fact]
        public void Get_Identifier()
        {
            Assert.Equal("321", ((CauseCodes)JSONRecords[0]).Identifier);
        }

        [Fact]
        public void Set_BundleIdentifier()
        {
            SetterCauseCodes.BundleIdentifier = "123";
            Assert.Equal("123", SetterCauseCodes.BundleIdentifier);
        }

        [Fact]
        public void Get_BundleIdentifier()
        {
            Assert.Equal("123", ((CauseCodes)JSONRecords[0]).BundleIdentifier);
        }

        [Fact]
        public void Get_Codes()
        {
            Assert.Equal("I251", ((CauseCodes)JSONRecords[0]).Codes[0]);
            Assert.Equal("I259", ((CauseCodes)JSONRecords[0]).Codes[1]);
            Assert.Equal("I250", ((CauseCodes)JSONRecords[0]).Codes[2]);
        }

        [Fact]
        public void Set_Codes()
        {
            List<string> codes = new List<string>();
            codes.Add("I251");
            codes.Add("I259");
            codes.Add("I250");
            SetterCauseCodes.Codes = codes.ToArray();
            Assert.Equal("I251", SetterCauseCodes.Codes[0]);
            Assert.Equal("I259", SetterCauseCodes.Codes[1]);
            Assert.Equal("I250", SetterCauseCodes.Codes[2]);
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
