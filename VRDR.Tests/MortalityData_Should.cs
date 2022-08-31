using Xunit;
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace VRDR.Tests
{
    public class MortalityData_Should
    {
        MortalityData mortalityData;

        public MortalityData_Should()
        {
            mortalityData = MortalityData.Instance;
        }

        [Fact]
        public void HandleOtherEthnicityDataInIJE()
        {
            IJEMortality ije1 = new IJEMortality(File.ReadAllText(FixturePath("fixtures/ije/EthnicityOtherCase.ije")), true);
            DeathRecord dr1 = ije1.ToDeathRecord();
            IJEMortality ije1rt = new IJEMortality(dr1);
            Assert.Equal("N", ije1rt.DETHNIC1);
            Assert.Equal("N", ije1rt.DETHNIC2);
            Assert.Equal("N", ije1rt.DETHNIC3);
            Assert.Equal("H", ije1rt.DETHNIC4);
            Assert.Equal("Guatemalan", ije1rt.DETHNIC5);

            IJEMortality ije2 = new IJEMortality(File.ReadAllText(FixturePath("fixtures/ije/EthnicityOtherCaseNoWriteIn.ije")), true);
            DeathRecord dr2 = ije2.ToDeathRecord();
            IJEMortality ije2rt = new IJEMortality(dr2);
            Assert.Equal("N", ije2rt.DETHNIC1);
            Assert.Equal("N", ije2rt.DETHNIC2);
            Assert.Equal("N", ije2rt.DETHNIC3);
            Assert.Equal("H", ije2rt.DETHNIC4);

            IJEMortality ije3 = new IJEMortality(File.ReadAllText(FixturePath("fixtures/ije/EthnicityPlusOtherCase.ije")), true);
            DeathRecord dr3 = ije3.ToDeathRecord();
            IJEMortality ije3rt = new IJEMortality(dr3);
            Assert.Equal("H", ije3rt.DETHNIC1);
            Assert.Equal("N", ije3rt.DETHNIC2);
            Assert.Equal("N", ije3rt.DETHNIC3);
            Assert.Equal("H", ije3rt.DETHNIC4);
            Assert.Equal("Guatemalan", ije3rt.DETHNIC5);

            IJEMortality ije4 = new IJEMortality(File.ReadAllText(FixturePath("fixtures/ije/EthnicityAllH.ije")), true);
            DeathRecord dr4 = ije4.ToDeathRecord();
            IJEMortality ije4rt = new IJEMortality(dr4);
            Assert.Equal("H", ije4rt.DETHNIC1);
            Assert.Equal("H", ije4rt.DETHNIC2);
            Assert.Equal("H", ije4rt.DETHNIC3);
            Assert.Equal("H", ije4rt.DETHNIC4);

            // the only time unkown are preserved in a roundtrip is when all DETHNIC fields are unknown
            IJEMortality ije5 = new IJEMortality(File.ReadAllText(FixturePath("fixtures/ije/EthnicityAllUnknown.ije")), true);
            DeathRecord dr5 = ije5.ToDeathRecord();
            IJEMortality ije5rt = new IJEMortality(dr5);
            Assert.Equal("U", ije5rt.DETHNIC1);
            Assert.Equal("U", ije5rt.DETHNIC2);
            Assert.Equal("U", ije5rt.DETHNIC3);
            Assert.Equal("U", ije5rt.DETHNIC4);

        }
        [Fact]
        public void HandleDeathLocationIJE()
        {
            IJEMortality ije1 = new IJEMortality(File.ReadAllText(FixturePath("fixtures/ije/DeathLocation.ije")), true);
            Assert.Equal("MA", ije1.DSTATE);
            Assert.Equal("4", ije1.DPLACE);
            DeathRecord dr = ije1.ToDeathRecord();
            Dictionary<string,string> age = new Dictionary<string,string>();
            age.Add("value", "10");
            age.Add("unit", "Months");
            age.Add("code", "mo");
            dr.AgeAtDeath = age;
            Assert.Equal("mo", dr.AgeAtDeath["code"]);
            IJEMortality ije1rt = new IJEMortality(dr);
            Assert.Equal("mo", dr.AgeAtDeath["code"]);
            Assert.Equal("mo", ije1rt.ToDeathRecord().AgeAtDeath["code"]);
            Assert.Equal("2", ije1rt.AGETYPE);
            Assert.Equal("010",ije1rt.AGE);
            Assert.Equal("4", ije1rt.DPLACE);
            ije1.DSTATE = "YC";
            ije1.AUXNO = "000000000001";
            ije1.AUXNO2 = "000000000002";
            Assert.Equal("YC", ije1.DSTATE);
            Assert.Equal("000000000001", ije1.AUXNO);
            Assert.Equal("000000000002", ije1.AUXNO2);
            DeathRecord dr2 = ije1.ToDeathRecord();
            Assert.Equal("1", dr2.StateLocalIdentifier1);
            Assert.Equal("2", dr2.StateLocalIdentifier2);
            Assert.Equal("NY", dr2.DeathLocationAddress["addressState"]);
            Assert.Equal("YC", dr2.DeathLocationJurisdiction);
            IJEMortality ije1rt2 = new IJEMortality(dr2);
            DeathRecord dr3 = ije1rt2.ToDeathRecord();
            Assert.Equal("NY", dr3.DeathLocationAddress["addressState"]);
            Assert.Equal("YC", dr3.DeathLocationJurisdiction);
            Assert.Equal("1", dr3.StateLocalIdentifier1);
            Assert.Equal("2", dr3.StateLocalIdentifier2);
            IJEMortality ije3 = new IJEMortality(ije1.ToString());
            Assert.Equal("000000000001", ije3.AUXNO);
            Assert.Equal("000000000002", ije3.AUXNO2);
            Assert.Equal("YC", ije3.DSTATE);
            ije3.AGE = "010";
            ije3.AGETYPE = "2";
            DeathRecord dr4 = ije3.ToDeathRecord();
            Assert.Equal("NY", dr4.DeathLocationAddress["addressState"]);
            Assert.Equal("YC", dr4.DeathLocationJurisdiction);
            Assert.Equal("mo", dr4.AgeAtDeath["code"]);
            Assert.Equal("10", dr4.AgeAtDeath["value"]);
        }

        [Fact]
        public void HandleAddressUpdateIJE()
        {
            IJEMortality ije1 = new IJEMortality(File.ReadAllText(FixturePath("fixtures/ije/DeathLocation.ije")), true);
            Assert.Equal("582 Dustin Centers", ije1.ADDRESS_D.Trim());
            Assert.Equal("902101111", ije1.ZIP9_D);

            // Update dictionary fields
            ije1.ADDRESS_D = "580 Dustin Center";
            Assert.Equal("580 Dustin Center", ije1.ADDRESS_D.Trim());
            Assert.Equal("902101111", ije1.ZIP9_D);

        }
        [Fact]
        public void SetSTNAME_R()
        {
            IJEMortality ije1 = new IJEMortality();
            Assert.Equal("", ije1.AUXNO.Trim());

            ije1.COUNTRYC = "  ";
            Assert.Equal("", ije1.COUNTRYTEXT_R.Trim());

            ije1.COUNTRYC = "US";
            Assert.Equal("United States", ije1.COUNTRYTEXT_R.Trim());

            ije1.STNAME_R = "St-Jean";
            Assert.Equal("St-Jean", ije1.STNAME_R.Trim());

        }

        [Fact]
        public void SetTOI_HR()
        {
            IJEMortality ije1 = new IJEMortality();
            Assert.Equal("    ", ije1.TOI_HR);
            ije1.TOI_HR = "615";  // should be 4 digits... do we need a check that it is less than 2400?
            Assert.Equal("    ", ije1.TOI_HR);
            ije1.TOI_HR = "5550";  // should be 4 digits... do we need a check that it is less than 2400?
            Assert.Equal("    ", ije1.TOI_HR);
        }

        [Fact]
        public void SetCOUNTRY_C()
        {
            IJEMortality ije1 = new IJEMortality();
            ije1.COUNTRYC = "UR";  // not a legitimate country of residence, since it is defunct... need to check
            Assert.Equal("X", ije1.COUNTRYC.Trim());
        }

        [Fact]
        public void HandleUnknownBirthRecordId()
        {
            IJEMortality ije1 = new IJEMortality(File.ReadAllText(FixturePath("fixtures/ije/UnknownBirthRecordId.ije")), true);
            DeathRecord dr1 = ije1.ToDeathRecord();
            Assert.Null(dr1.BirthRecordId);
            IJEMortality ije1rt = new IJEMortality(dr1);
            Assert.Equal("", ije1rt.BCNO);
        }
        [Fact]
        public void HandleUnknownDOBParts()
        {
            IJEMortality ije1 = new IJEMortality(File.ReadAllText(FixturePath("fixtures/ije/DOBDatePartAbsent.ije")), true);
            Assert.Equal("9999", ije1.DOB_YR);
            Assert.Equal("06", ije1.DOB_MO);
            Assert.Equal("02", ije1.DOB_DY);
            DeathRecord dr1 = ije1.ToDeathRecord();
            Assert.Null(dr1.BirthYear);
            Assert.Equal(6, (int)dr1.BirthMonth);
            Assert.Equal(2, (int)dr1.BirthDay);
            Assert.Null(dr1.DateOfBirth);
        }

        [Fact]
        public void HandleUnknownDOBPartsCustom()
        {
            IJEMortality ije1 = new IJEMortality(File.ReadAllText(FixturePath("fixtures/ije/DOBPartsAllUnknown.ije")), true);
            Assert.Equal("9999", ije1.DOB_YR);
            Assert.Equal("99", ije1.DOB_MO);
            Assert.Equal("99", ije1.DOB_DY);
            DeathRecord dr1 = ije1.ToDeathRecord();
            Assert.Null(dr1.BirthYear);
            Assert.Null(dr1.BirthMonth);
            Assert.Null(dr1.BirthDay);
            Assert.Null(dr1.DateOfBirth);
        }

        [Fact]
        public void HandleUnknownCODandCOUNTYC()
        {
            IJEMortality ije1 = new IJEMortality(File.ReadAllText(FixturePath("fixtures/ije/CODandCOUNTYCUnknown.ije")), true);
            Assert.Equal("999", ije1.COD);
            Assert.Equal("999", ije1.COUNTYC);

            DeathRecord dr1 = ije1.ToDeathRecord();
            Assert.Equal("999", dr1.DeathLocationAddress["addressCountyC"]);
            Assert.Equal("999", dr1.Residence["addressCountyC"]);
        }

        [Fact]
        public void HandleOtherCODandCOUNTYC()
        {
            IJEMortality ije1 = new IJEMortality(File.ReadAllText(FixturePath("fixtures/ije/CODandCOUNTYCOther.ije")), true);
            Assert.Equal("000", ije1.COD);
            Assert.Equal("000", ije1.COUNTYC);

            DeathRecord dr1 = ije1.ToDeathRecord();
            Assert.Equal("0", dr1.DeathLocationAddress["addressCountyC"]); // no padding int the FHIR world
            Assert.Equal("0", dr1.Residence["addressCountyC"]);
        }

        [Fact]
        public void HandleCountyText()
        {
            IJEMortality ije1 = new IJEMortality(File.ReadAllText(FixturePath("fixtures/ije/DeathLocation.ije")), true);
            Assert.Equal("Middlesex", ije1.COUNTYTEXT_R.Trim());
        }

        [Fact]
        public void HandleCityText()
        {
            IJEMortality ije1 = new IJEMortality(File.ReadAllText(FixturePath("fixtures/ije/DeathLocation.ije")), true);
            Assert.Equal("Tyngsborough", ije1.CITYTEXT_R.Trim());
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
