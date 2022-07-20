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

        [Theory]
        [InlineData("FL", "FL")]
        [InlineData("Florida", "FL")]
        [InlineData("FLOrida", "FL")]
        [InlineData("  FLOrida ", "FL")]
        [InlineData("NOT A STATE", null)]
        public void StateNameToStateCode_Test(string input, string expected)
        {
            Assert.Equal(expected, mortalityData.StateNameToStateCode(input));
        }

        [Theory]
        [InlineData("MD", "Maryland")]
        [InlineData("MO", "Missouri")]
        [InlineData("NOTSTATE", null)]
        public void StateCodeToStateName_Test(string input, string expected)
        {
            Assert.Equal(expected, mortalityData.StateCodeToStateName(input));
        }

        [Theory]
        [InlineData("Åland, Finland", "FI")]
        [InlineData("Central And Southern Line Islands", "CL")]
        [InlineData("Ethiopia", "ET")]
        [InlineData("United States", "US")]
        [InlineData("Fake Place", null)]
        public void CountryNameToCountryCode_Test(string input, string expected)
        {
            Assert.Equal(expected, mortalityData.CountryNameToCountryCode(input));
        }

        [Theory]
        [InlineData("FI", "Åland, Finland")]
        [InlineData("\t   fi ", "Åland, Finland")]
        [InlineData("CF", "Congo (brazzaville), Republic Of The Congo")]
        [InlineData("ZZ", "Not Classifiable")]
        [InlineData("   ", null)]
        public void CountryCodeToCountryName_Test(string input, string expected)
        {
            Assert.Equal(expected, mortalityData.CountryCodeToCountryName(input));
        }

        [Theory]
        [InlineData("Florida", "Sarasota", "115")]
        // Multiple New Castle's exist, but none in FL.
        [InlineData(" florida\t", "new castle\n", null)]
        [InlineData("Not a state", "Not a county", null)]
        [InlineData("Florida", "Not a county", null)]
        public void StateNameAndCountyNameToCountyCode_Test(string stateName, string countyName, string expected)
        {
            Assert.Equal(expected, mortalityData.StateNameAndCountyNameToCountyCode(stateName, countyName));
        }

        [Theory]
        [InlineData("Florida", "115", "Sarasota")]
        [InlineData(" florida\t", "115\n", "Sarasota")]
        [InlineData(" florida\t", "9999\n", null)]
        [InlineData("Not a state", "9999", null)]
        [InlineData("Florida", "9999", null)]
        public void StateNameAndCountyCodeToCountyName_Test(string stateName, string countyCode, string expected)
        {
            Assert.Equal(expected, mortalityData.StateNameAndCountyCodeToCountyName(stateName, countyCode));
        }

        [Theory]
        [InlineData("Delaware", "New Castle", "Brandywine Hills", "77580")]
        [InlineData("Indiana", "Bartholomew", "Columbus", "14734")]
        [InlineData("NA", "Bartholomew", "Columbus", null)]
        [InlineData("Indiana", "NOTACOUNTY", "Columbus", null)]
        [InlineData("Indiana", "Bartholomew", "NOTASTATE", null)]
        [InlineData("Wyoming", "Uinta", "Evanston", "25620")]
        public void StateNameAndCountyNameAndPlaceNameToPlaceCode_Test(string state, string county, string place, string expected)
        {
            Assert.Equal(expected, mortalityData.StateNameAndCountyNameAndPlaceNameToPlaceCode(state, county, place));
        }

        [Theory]
        [InlineData("MI", "Lincoln Park", "Wayne")]
        [InlineData("   mi ", "Lincoln Park", "Wayne")]
        [InlineData("NONE", "Lincoln Park", null)]
        [InlineData("FL", "Lincoln Park", null)]
        [InlineData("FL", "NOTACOUNTY", null)]
        public void StateCodeAndCityNameToCountyName_Test(string stateCode, string cityName, string expected)
        {
            Assert.Equal(expected, mortalityData.StateCodeAndCityNameToCountyName(stateCode, cityName));
        }

        [Theory]
        [InlineData("Belearic Islander", "2142-8")]
        [InlineData("  Guatemalan  ", "2157-6")]
        [InlineData("NOTVALID", null)]
        public void EthnicityNameToEthnicityCode_Test(string input, string expected)
        {
            Assert.Equal(expected, mortalityData.EthnicityNameToEthnicityCode(input));
        }

        [Theory]
        [InlineData("  2142-8 ", "Belearic Islander")]
        [InlineData("\t2157-6", "Guatemalan")]
        [InlineData("NOTVALID", null)]
        public void EthnicityCodeToEthnicityName_Test(string input, string expected)
        {
            Assert.Equal(expected, mortalityData.EthnicityCodeToEthnicityName(input));
        }

        [Theory]
        [InlineData("Chickahominy", "1108-0")]
        [InlineData("  chickahominy  ", "1108-0")]
        [InlineData("NOTVALID", null)]
        public void AIANRaceNameToRaceCode_Test(string input, string expected)
        {
            Assert.Equal(expected, mortalityData.AIANRaceNameToRaceCode(input));
        }

        [Theory]
        [InlineData("1962-0", "Georgetown (Yupik-Eskimo)")]
        [InlineData("   1962-0  ", "Georgetown (Yupik-Eskimo)")]
        [InlineData("NOTVALID", null)]
        public void AIANRaceCodeToRaceName_Test(string input, string expected)
        {
            Assert.Equal(expected, mortalityData.AIANRaceCodeToRaceName(input));
        }

        [Theory]
        [InlineData("Asian", "2028-9")]
        [InlineData("  maldivian   ", "2049-5")]
        [InlineData("NOTVALID", null)]
        public void ARaceNameToRaceCode_Test(string input, string expected)
        {
            Assert.Equal(expected, mortalityData.ARaceNameToRaceCode(input));
        }

        [Theory]
        [InlineData("2028-9", "Asian")]
        [InlineData("    2049-5    ", "Maldivian")]
        [InlineData("NOTVALID", null)]
        public void ARaceCodeToRaceName_Test(string input, string expected)
        {
            Assert.Equal(expected, mortalityData.ARaceCodeToRaceName(input));
        }

        [Theory]
        [InlineData("Dominica Islander", "2070-1")]
        [InlineData("  botswanan  ", "2061-0")]
        [InlineData("NOTVALID", null)]
        public void BAARaceNameToRaceCode_Test(string input, string expected)
        {
            Assert.Equal(expected, mortalityData.BAARaceNameToRaceCode(input));
        }

        [Theory]
        [InlineData("2070-1", "Dominica Islander")]
        [InlineData("2061-0", "Botswanan")]
        [InlineData("NOTVALID", null)]
        public void BAARaceCodeToRaceName_Test(string input, string expected)
        {
            Assert.Equal(expected, mortalityData.BAARaceCodeToRaceName(input));
        }

        [Theory]
        [InlineData("Guamanian or Chamorro", "2086-7")]
        [InlineData("  fijian   ", "2101-4")]
        [InlineData("NOTVALID", null)]
        public void NHOPIRaceNameToRaceCode_Test(string input, string expected)
        {
            Assert.Equal(expected, mortalityData.NHOPIRaceNameToRaceCode(input));
        }

        [Theory]
        [InlineData("2086-7", "Guamanian or Chamorro")]
        [InlineData("2101-4", "Fijian")]
        [InlineData("NOTVALID", null)]
        public void NHOPIRaceCodeToRaceName_Test(string input, string expected)
        {
            Assert.Equal(expected, mortalityData.NHOPIRaceCodeToRaceName(input));
        }

        [Theory]
        [InlineData("English", "2110-5")]
        [InlineData("  israeili ", "2127-9")]
        [InlineData("NOTVALID", null)]
        public void WRaceNameToRaceCode_Test(string input, string expected)
        {
            Assert.Equal(expected, mortalityData.WRaceNameToRaceCode(input));
        }

        [Theory]
        [InlineData("2110-5", "English")]
        [InlineData("2127-9", "Israeili")]
        [InlineData("NOTVALID", null)]
        public void WRaceCodeToRaceName_Test(string input, string expected)
        {
            Assert.Equal(expected, mortalityData.WRaceCodeToRaceName(input));
        }

        [Theory]
        [InlineData("Chickahominy", "1108-0")]
        [InlineData("Asian", "2028-9")]
        [InlineData("Dominica Islander", "2070-1")]
        [InlineData("Guamanian or Chamorro", "2086-7")]
        [InlineData("English", "2110-5")]
        [InlineData("NOTVALID", null)]
        public void RaceNameToRaceCode_Test(string input, string expected)
        {
            Assert.Equal(expected, mortalityData.RaceNameToRaceCode(input));
        }

        [Theory]
        [InlineData("1962-0", "Georgetown (Yupik-Eskimo)")]
        [InlineData("2028-9", "Asian")]
        [InlineData("2070-1", "Dominica Islander")]
        [InlineData("2086-7", "Guamanian or Chamorro")]
        [InlineData("2110-5", "English")]
        [InlineData("NOTVALID", null)]
        public void RaceCodeToRaceName_Test(string input, string expected)
        {
            Assert.Equal(expected, mortalityData.RaceCodeToRaceName(input));
        }

        [Theory]
        [InlineData("AK")]
        public void StateCodeToRandomPlace_Test(string state)
        {
            PlaceCode randomPlace = mortalityData.StateCodeToRandomPlace(state);
            IEnumerable<PlaceCode> allPlaces = mortalityData.PlaceCodes.Where(t => t.State.Equals(state));
            // This assertion is actually backwards from how it is intended to be used, however
            // it works for our use case.
            Assert.Contains(randomPlace, allPlaces);
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
            IJEMortality ije1rt = new IJEMortality(dr);
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
            DeathRecord dr4 = ije3.ToDeathRecord();
            Assert.Equal("NY", dr4.DeathLocationAddress["addressState"]);
            Assert.Equal("YC", dr4.DeathLocationJurisdiction);
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
