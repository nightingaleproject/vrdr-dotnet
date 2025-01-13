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
            Dictionary<string, string> age = new Dictionary<string, string>();
            age.Add("value", "10");
            age.Add("code", "mo");
            dr.AgeAtDeath = age;
            Assert.Equal("mo", dr.AgeAtDeath["code"]);
            IJEMortality ije1rt = new IJEMortality(dr);
            Assert.Equal("mo", dr.AgeAtDeath["code"]);
            Assert.Equal("mo", ije1rt.ToDeathRecord().AgeAtDeath["code"]);
            Assert.Equal("2", ije1rt.AGETYPE);
            Assert.Equal("010", ije1rt.AGE);
            Assert.Equal("4", ije1rt.DPLACE);
            ije1.DSTATE = "YC";
            ije1.AUXNO = "000000000001";
            ije1.AUXNO2 = "000000000002";
            Assert.Equal("YC", ije1.DSTATE);
            Assert.Equal("000000000001", ije1.AUXNO);
            Assert.Equal("000000000002", ije1.AUXNO2);
            DeathRecord dr2 = ije1.ToDeathRecord();
            Assert.Equal("000000000001", dr2.StateLocalIdentifier1);
            Assert.Equal("000000000002", dr2.StateLocalIdentifier2);
            Assert.Equal("NY", dr2.DeathLocationAddress["addressState"]);
            Assert.Equal("YC", dr2.DeathLocationJurisdiction);
            IJEMortality ije1rt2 = new IJEMortality(dr2);
            DeathRecord dr3 = ije1rt2.ToDeathRecord();
            Assert.Equal("NY", dr3.DeathLocationAddress["addressState"]);
            Assert.Equal("YC", dr3.DeathLocationJurisdiction);
            Assert.Equal("000000000001", dr3.StateLocalIdentifier1);
            Assert.Equal("000000000002", dr3.StateLocalIdentifier2);
            IJEMortality ije3 = new IJEMortality(ije1.ToString());
            Assert.Equal("000000000001", ije3.AUXNO);
            Assert.Equal("000000000002", ije3.AUXNO2);
            Assert.Equal("YC", ije3.DSTATE);
            ije3.AGE = "010";
            ije3.AGETYPE = "2";
            ije3.AGE_BYPASS = ValueSets.EditBypass01.Edit_Failed_Data_Queried_And_Verified;
            Assert.Equal(ValueSets.EditBypass01.Edit_Failed_Data_Queried_And_Verified, ije3.AGE_BYPASS);
            DeathRecord dr4 = ije3.ToDeathRecord();
            Assert.Equal("NY", dr4.DeathLocationAddress["addressState"]);
            Assert.Equal("YC", dr4.DeathLocationJurisdiction);
            Assert.Equal("mo", dr4.AgeAtDeath["code"]);
            Assert.Equal("10", dr4.AgeAtDeath["value"]);
            Assert.Equal(ValueSets.EditBypass01.Edit_Failed_Data_Queried_And_Verified, dr4.AgeAtDeathEditFlagHelper);
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
            Assert.Equal("    ", ije1.SUR_YR);
            ije1.TOI_HR = "615";  // should be 4 digits... creates validation error, and results in blanks
            Assert.Equal("    ", ije1.TOI_HR);
            ije1.TOI_HR = "5550";  // should be 2359 or less... creates validation error, and results in blanks
            Assert.Equal("    ", ije1.TOI_HR);
        }

        [Fact]
        public void SetVOID()
        {
            IJEMortality ije = new IJEMortality();
            Assert.Equal("0", ije.VOID);
            ije.VOID = "123";
            Assert.Equal("0", ije.VOID);
            ije.VOID = " ";
            Assert.Equal("0", ije.VOID);
            ije.VOID = "abc #$@";
            Assert.Equal("0", ije.VOID);
            ije.VOID = " 0 ";
            Assert.Equal("0", ije.VOID);
            ije.VOID = "0";
            Assert.Equal("0", ije.VOID);
            ije.VOID = " 1 ";
            Assert.Equal("1", ije.VOID);
            ije.VOID = "1";
            Assert.Equal("1", ije.VOID);
            ije.VOID = "2";
            Assert.Equal("0", ije.VOID);
        }

        [Fact]
        public void SetALIAS()
        {
            IJEMortality ije = new IJEMortality();
            Assert.Equal("0", ije.ALIAS);
            ije.ALIAS = "123";
            Assert.Equal("0", ije.ALIAS);
            ije.ALIAS = " ";
            Assert.Equal("0", ije.ALIAS);
            ije.ALIAS = "abc #$@";
            Assert.Equal("0", ije.ALIAS);
            ije.ALIAS = " 0 ";
            Assert.Equal("0", ije.ALIAS);
            ije.ALIAS = "0";
            Assert.Equal("0", ije.ALIAS);
            ije.ALIAS = " 1 ";
            Assert.Equal("1", ije.ALIAS);
            ije.ALIAS = "1";
            Assert.Equal("1", ije.ALIAS);
            ije.ALIAS = "2";
            Assert.Equal("0", ije.ALIAS);
        }


        // [Fact]
        // public void SetCOUNTRY_C()
        // {
        //     IJEMortality ije1 = new IJEMortality();
        //     ije1.COUNTRYC = "UR";  // not a legitimate country of residence, since it is defunct... need to check
        //     Assert.Equal("X", ije1.COUNTRYC.Trim());
        // }

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
            Assert.Equal(-1, dr1.BirthYear);
            Assert.Equal(6, dr1.BirthMonth);
            Assert.Equal(2, dr1.BirthDay);
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
            Assert.Equal(-1, dr1.BirthYear);
            Assert.Equal(-1, dr1.BirthMonth);
            Assert.Equal(-1, dr1.BirthDay);
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

        [Fact]
        public void SetSSNValid()
        {
            IJEMortality ije = new IJEMortality();
            ije.SSN = "112223333";
            Assert.Equal("112223333", ije.SSN);
            DeathRecord record = ije.ToDeathRecord();
            Assert.Equal("112223333", record.SSN);
        }

        [Fact]
        public void GetSSNValid()
        {
            DeathRecord record = new DeathRecord();
            record.SSN = "112223333";
            record.DeathLocationJurisdiction = "MA";
            Assert.Equal("112223333", record.SSN);
            IJEMortality ije = new IJEMortality(record);
            Assert.Equal("112223333", ije.SSN);

            record.SSN = "11-222-3333";
            Assert.Equal("112223333", record.SSN);
            IJEMortality ije2 = new IJEMortality(record);
            Assert.Equal("112223333", ije2.SSN);

            record.SSN = "11 222 3333";
            Assert.Equal("112223333", record.SSN);
            IJEMortality ije3 = new IJEMortality(record);
            Assert.Equal("112223333", ije3.SSN);
        }

        [Fact]
        public void SetSSNTooShort()
        {
            IJEMortality ije1 = new IJEMortality();
            ije1.SSN = "11222    ";
            ije1.DSTATE = "MA";
            DeathRecord record1 = ije1.ToDeathRecord();
            ArgumentOutOfRangeException ex1 = Assert.Throws<ArgumentOutOfRangeException>(() => new IJEMortality(record1));
            Assert.Equal("Specified argument was out of the range of valid values. (Parameter 'Found 1 validation errors:\nError: FHIR field SSN contains string '11222' which is not the expected length (without dashes or spaces) for IJE field SSN of length 9')", ex1.Message);
            Assert.Equal("11222    ", ije1.SSN);
            Assert.Equal("11222", record1.SSN);
        }

        [Fact]
        public void GetSSNTooLong()
        {
            DeathRecord record = new DeathRecord();
            record.SSN = "1122233334";
            record.DeathLocationJurisdiction = "MA";
            Assert.Equal("1122233334", record.SSN);
            ArgumentOutOfRangeException ex = Assert.Throws<ArgumentOutOfRangeException>(() => new IJEMortality(record));
            Assert.Equal("Specified argument was out of the range of valid values. (Parameter 'Found 1 validation errors:\nError: FHIR field SSN contains string '1122233334' which is not the expected length (without dashes or spaces) for IJE field SSN of length 9')", ex.Message);
        }

        [Fact]
        public void GetSSNTooShort()
        {
            DeathRecord record = new DeathRecord();
            record.DeathLocationJurisdiction = "MA";
            record.SSN = "11222333";
            Assert.Equal("11222333", record.SSN);
            ArgumentOutOfRangeException ex = Assert.Throws<ArgumentOutOfRangeException>(() => new IJEMortality(record));
            Assert.Equal("Specified argument was out of the range of valid values. (Parameter 'Found 1 validation errors:\nError: FHIR field SSN contains string '11222333' which is not the expected length (without dashes or spaces) for IJE field SSN of length 9')", ex.Message);
        }

        [Fact]
        public void AddressDoesNotOverwriteCityLimits()
        {
            // Regression test: make sure that setting an address field does not erase the WithinCityLimits data
            IJEMortality ije = new IJEMortality();
            ije.LIMITS = "Y";
            ije.STNUM_R = "4437";
            ije.PREDIR_R = "North";
            ije.STNAME_R = "Charles";
            ije.STDESIG_R = "Avenue";
            ije.POSTDIR_R = "Southeast";
            ije.UNITNUM_R = "Apt 2B";
            ije.CITYTEXT_R = "Hartford";
            ije.ZIP9_R = "06107";
            ije.COUNTYTEXT_R = "Hartford";
            ije.STATETEXT_R = "Connecticut";
            ije.COUNTRYTEXT_R = "United States";
            ije.ADDRESS_R = "4437 North Charles Avenue Southeast Apt 2B";
            Assert.Equal("Y", ije.LIMITS);
        }

        // FHIR manages names in a way that there is a fundamental incompatibility with IJE: the "middle name" is the second element in
        // an array of given names. That means that it's not possible to set a middle name without first having a first name. The library
        // handles this by 1) raising an exception if a middle name is set before a first name and 2) resetting the middle name if the first
        // name is set again. If a user sets the first name and then the middle name then no problems will occur.

        [Fact]
        public void SettingMiddleNameFirstRaisesException()
        {
            IJEMortality ije = new IJEMortality();
            Exception ex = Assert.Throws<System.ArgumentException>(() => ije.MNAME = "M");
            Assert.Equal("Middle name cannot be set before first name", ex.Message);
            ex = Assert.Throws<System.ArgumentException>(() => ije.DMIDDLE = "M");
            Assert.Equal("Middle name cannot be set before first name", ex.Message);
            ex = Assert.Throws<System.ArgumentException>(() => ije.DDADMID = "M");
            Assert.Equal("Middle name cannot be set before first name", ex.Message);
            ex = Assert.Throws<System.ArgumentException>(() => ije.DMOMMID = "M");
            Assert.Equal("Middle name cannot be set before first name", ex.Message);
            ex = Assert.Throws<System.ArgumentException>(() => ije.SPOUSEMIDNAME = "M");
            Assert.Equal("Middle name cannot be set before first name", ex.Message);
            ex = Assert.Throws<System.ArgumentException>(() => ije.CERTMIDDLE = "M");
            Assert.Equal("Middle name cannot be set before first name", ex.Message);
        }

        [Fact]
        public void GivenNameOverwritesMiddleName()
        {
            IJEMortality ije = new IJEMortality();
            ije.GNAME = "Given";
            ije.MNAME = "M";
            ije.GNAME = "Given2";
            Assert.Equal("", ije.MNAME.Trim());
            ije = new IJEMortality();
            ije.GNAME = "Given";
            ije.DMIDDLE = "M";
            ije.GNAME = "Given2";
            Assert.Equal("", ije.DMIDDLE.Trim());
            ije = new IJEMortality();
            ije.DDADF = "Given";
            ije.DDADMID = "M";
            ije.DDADF = "Given2";
            Assert.Equal("", ije.DDADMID.Trim());
            ije = new IJEMortality();
            ije.DMOMF = "Given";
            ije.DMOMMID = "M";
            ije.DMOMF = "Given2";
            Assert.Equal("", ije.DMOMMID.Trim());
            ije = new IJEMortality();
            ije.SPOUSEF = "Given";
            ije.SPOUSEMIDNAME = "M";
            ije.SPOUSEF = "Given2";
            Assert.Equal("", ije.SPOUSEMIDNAME.Trim());
            ije = new IJEMortality();
            ije.CERTFIRST = "Given";
            ije.CERTMIDDLE = "M";
            ije.CERTFIRST = "Given2";
            Assert.Equal("", ije.CERTMIDDLE.Trim());
        }

        // There are two middle name fields, one for just the initial and one for the full middle name; setting the initial should not
        // overwrite the full name if present, but setting the full name should overwrite the initial

        [Fact]
        public void MiddleNameOverwriteRules()
        {
            IJEMortality ije = new IJEMortality();
            ije.GNAME = "Given";
            ije.DMIDDLE = "Dmiddle";
            ije.MNAME = "M";
            Assert.Equal("D", ije.MNAME.Trim());
            Assert.Equal("Dmiddle", ije.DMIDDLE.Trim());
            ije = new IJEMortality();
            ije.GNAME = "Given";
            ije.MNAME = "M";
            ije.DMIDDLE = "Dmiddle";
            Assert.Equal("D", ije.MNAME.Trim());
            Assert.Equal("Dmiddle", ije.DMIDDLE.Trim());
        }

        // If the DSTATE is not set we should get a different message from it being set to an invalid value
        [Fact]
        public void DSTATE_Errors()
        {
            DeathRecord record = new DeathRecord();
            record.DeathLocationJurisdiction = ""; // No jurisdiction code
            ArgumentOutOfRangeException e = Assert.Throws<ArgumentOutOfRangeException>(() => new IJEMortality(record));
            Assert.Equal("Specified argument was out of the range of valid values. (Parameter 'Found 1 validation errors:\nError: FHIR field DeathLocationJurisdiction is blank, which is invalid for IJE field DSTATE.')", e.Message);
            record.DeathLocationJurisdiction = "QQ"; // Not a valid jurisdiction code
            e = Assert.Throws<ArgumentOutOfRangeException>(() => new IJEMortality(record));
            Assert.Equal("Specified argument was out of the range of valid values. (Parameter 'Found 1 validation errors:\nError: FHIR field DeathLocationJurisdiction has value 'QQ', which is invalid for IJE field DSTATE.')", e.Message);
        }
        // Birth Country and State
        [Fact]
        public void BirthCountryState()
        {
            DeathRecord record = new DeathRecord();
            record.DeathLocationJurisdiction = "MI";
            Dictionary<string, string> address = new Dictionary<string, string>();
            IJEMortality ije;

            // US/CA; valid state/province
            address["addressCountry"] = "US";
            address["addressState"] = "MA";
            record.PlaceOfBirth = address;
            ije = new IJEMortality(record);
            Assert.Equal("US", ije.BPLACE_CNT);
            Assert.Equal("MA", ije.BPLACE_ST);

            address["addressCountry"] = "CA";
            address["addressState"] = "ON";
            record.PlaceOfBirth = address;
            ije = new IJEMortality(record);
            Assert.Equal("CA", ije.BPLACE_CNT);
            Assert.Equal("ON", ije.BPLACE_ST);

            // US/CA; invalid state/province
            address["addressCountry"] = "US";
            address["addressState"] = "A1";
            record.PlaceOfBirth = address;
            ije = new IJEMortality(record);
            Assert.Equal("US", ije.BPLACE_CNT);
            Assert.Equal("ZZ", ije.BPLACE_ST);

            address["addressCountry"] = "US";
            address["addressState"] = "UNK";
            record.PlaceOfBirth = address;
            ije = new IJEMortality(record);
            Assert.Equal("US", ije.BPLACE_CNT);
            Assert.Equal("ZZ", ije.BPLACE_ST);

            address["addressCountry"] = "CA";
            address["addressState"] = "A1";
            record.PlaceOfBirth = address;
            ije = new IJEMortality(record);
            Assert.Equal("CA", ije.BPLACE_CNT);
            Assert.Equal("XX", ije.BPLACE_ST);

            address["addressCountry"] = "CA";
            address["addressState"] = "UNK";
            record.PlaceOfBirth = address;
            ije = new IJEMortality(record);
            Assert.Equal("CA", ije.BPLACE_CNT);
            Assert.Equal("XX", ije.BPLACE_ST);

            // valid country; no state/province
            address["addressCountry"] = "AS";
            address["addressState"] = "";
            record.PlaceOfBirth = address;
            ije = new IJEMortality(record);
            Assert.Equal("AS", ije.BPLACE_CNT);
            Assert.Equal("XX", ije.BPLACE_ST);

            // valid country; unknown state/province
            address["addressCountry"] = "AS";
            address["addressState"] = "UNK";
            record.PlaceOfBirth = address;
            ije = new IJEMortality(record);
            Assert.Equal("AS", ije.BPLACE_CNT);
            Assert.Equal("XX", ije.BPLACE_ST);

            // invalid country; valid state/province
            address["addressCountry"] = "Z1";
            address["addressState"] = "CA";
            record.PlaceOfBirth = address;
            ije = new IJEMortality(record);
            Assert.Equal("ZZ", ije.BPLACE_CNT);
            Assert.Equal("ZZ", ije.BPLACE_ST);
        }

        // Residence Country and State
        [Fact]
        public void ResidenceCountryState()
        {
            DeathRecord record = new DeathRecord();
            record.DeathLocationJurisdiction = "MI";
            Dictionary<string, string> address = new Dictionary<string, string>();
            IJEMortality ije;

            // US/CA; valid state/province
            address["addressCountry"] = "US";
            address["addressState"] = "MA";
            record.Residence = address;
            ije = new IJEMortality(record);
            Assert.Equal("US", ije.COUNTRYC);
            Assert.Equal("MA", ije.STATEC);

            address["addressCountry"] = "CA";
            address["addressState"] = "ON";
            record.Residence = address;
            ije = new IJEMortality(record);
            Assert.Equal("CA", ije.COUNTRYC);
            Assert.Equal("ON", ije.STATEC);

            // US/CA; invalid state/province
            address["addressCountry"] = "US";
            address["addressState"] = "A1";
            record.Residence = address;
            ije = new IJEMortality(record);
            Assert.Equal("US", ije.COUNTRYC);
            Assert.Equal("ZZ", ije.STATEC);

            address["addressCountry"] = "US";
            address["addressState"] = "UNK";
            record.Residence = address;
            ije = new IJEMortality(record);
            Assert.Equal("US", ije.COUNTRYC);
            Assert.Equal("ZZ", ije.STATEC);

            address["addressCountry"] = "CA";
            address["addressState"] = "A1";
            record.Residence = address;
            ije = new IJEMortality(record);
            Assert.Equal("CA", ije.COUNTRYC);
            Assert.Equal("XX", ije.STATEC);

            address["addressCountry"] = "CA";
            address["addressState"] = "UNK";
            record.Residence = address;
            ije = new IJEMortality(record);
            Assert.Equal("CA", ije.COUNTRYC);
            Assert.Equal("XX", ije.STATEC);

            // valid country; no state/province
            address["addressCountry"] = "AS";
            address["addressState"] = "";
            record.Residence = address;
            ije = new IJEMortality(record);
            Assert.Equal("AS", ije.COUNTRYC);
            Assert.Equal("XX", ije.STATEC);

            // valid country; unknown state/province
            address["addressCountry"] = "AS";
            address["addressState"] = "UNK";
            record.Residence = address;
            ije = new IJEMortality(record);
            Assert.Equal("AS", ije.COUNTRYC);
            Assert.Equal("XX", ije.STATEC);

            // invalid country; valid state/province
            address["addressCountry"] = "Z1";
            address["addressState"] = "CA";
            record.Residence = address;
            ije = new IJEMortality(record);
            Assert.Equal("ZZ", ije.COUNTRYC);
            Assert.Equal("ZZ", ije.STATEC);
        }
        // NCHS has quirky ICD10 codes.  If someone tries to use an actual ICD10 code with periods it should throw an exception
        [Fact]
        public void ICD10_Errors()
        {
            // Create an empty IJE Mortality record
            IJEMortality ije = new IJEMortality();
            // Populate the IJE fields
            ije.DOD_YR = "2022";
            ije.DSTATE = "YC";
            ije.FILENO = "123";
            ije.AUXNO = "500";
            ArgumentException e1 = Assert.Throws<ArgumentException>(() => ije.RAC = "T27.3T27.0");
            ije.EAC = "11T273  21T270 &";
            Assert.Equal("11T273  21T270 &", ije.EAC.Trim());
            ije.EAC = "11T27   21T27  &";
            Assert.Equal("11T27   21T27  &", ije.EAC.Trim());
            ije.ACME_UC = "T273";
            Assert.Equal("T273", ije.ACME_UC);
            ArgumentException e2 = Assert.Throws<ArgumentException>(() => ije.ACME_UC = "T27.3");
            ije.MAN_UC = "T273";
            Assert.Equal("T273", ije.MAN_UC);
            ArgumentException e3 = Assert.Throws<ArgumentException>(() => ije.MAN_UC = "T27.3");
            ije.RAC = "T27  T27 1";
            Assert.Equal("T27  T27 1", ije.RAC.Trim());
            ije.RAC = "T273 T2701";
            Assert.Equal("T273 T2701", ije.RAC.Trim());
            ArgumentException e4 = Assert.Throws<ArgumentException>(() => ije.EAC = "11T27.321T27.0&");
        }

        [Fact]
        public void IDC10_Valid_Codes()
        {
            Assert.True(IJEMortality.ValidNCHSICD10("U071"));
            Assert.True(IJEMortality.ValidNCHSICD10("A379"));
            Assert.True(IJEMortality.ValidNCHSICD10("G309"));
            Assert.True(IJEMortality.ValidNCHSICD10("J189"));
            Assert.True(IJEMortality.ValidNCHSICD10("K2710"));
            Assert.True(IJEMortality.ValidNCHSICD10("A1B00"));

            Assert.False(IJEMortality.ValidNCHSICD10("A1C00"));
            Assert.False(IJEMortality.ValidNCHSICD10("1234"));
            Assert.False(IJEMortality.ValidNCHSICD10("Q101234"));
        }

        [Fact]
        public void LeapYearDate()
        {
            IJEMortality ije = new IJEMortality();
            ije.CERTDATE = "02292024";
            Assert.Equal("02292024", ije.CERTDATE);
        }

        [Fact]
        public void AuxiliaryNo()
        {
            IJEMortality ije = new IJEMortality();

            // simple round trip
            ije.AUXNO = "A1B2C3";
            Assert.Equal("A1B2C3      ", ije.AUXNO);
            ije.AUXNO2 = "E1F2G3";
            Assert.Equal("E1F2G3      ", ije.AUXNO2);

            // oversized input
            ije.AUXNO = "1234567890123456";
            Assert.Equal("123456789012", ije.AUXNO);
            ije.AUXNO2 = "1234567890123456";
            Assert.Equal("123456789012", ije.AUXNO2);
        }

        [Fact]
        public void TestDeathRecordMortalityTranslation()
        {
            DeathRecord deathRecord = new DeathRecord();

            // Identifier
            deathRecord.Identifier = "1";

            // CertifiedTime
            deathRecord.CertifiedTime = "2019-01-29T16:48:06-05:00";

            // RegisteredTime
            deathRecord.RegisteredTime = "2019-02-01T16:47:04-05:00";

            // CertificationRole
            Dictionary<string, string> certificationRole = new Dictionary<string, string>();
            certificationRole.Add("code", "434641000124105");
            certificationRole.Add("system", "http://snomed.info/sct");
            certificationRole.Add("display", "Physician");
            deathRecord.CertificationRole = certificationRole;

            // State Local Identifier
            deathRecord.StateLocalIdentifier1 = "000000000042";

            // MannerOfDeathType
            Dictionary<string, string> mannerOfDeathType = new Dictionary<string, string>();
            mannerOfDeathType.Add("code", "7878000");
            mannerOfDeathType.Add("system", "http://snomed.info/sct");
            mannerOfDeathType.Add("display", "Accidental death");
            deathRecord.MannerOfDeathType = mannerOfDeathType;

            // CertifierGivenNames
            string[] cnames = { "Doctor", "Middle" };
            deathRecord.CertifierGivenNames = cnames;

            // CertifierFamilyName
            deathRecord.CertifierFamilyName = "Last";

            // CertifierSuffix
            deathRecord.CertifierSuffix = "Jr.";

            // CertifierAddress
            Dictionary<string, string> caddress = new Dictionary<string, string>();
            caddress.Add("addressLine1", "11 Example Street");
            caddress.Add("addressLine2", "Line 2");
            caddress.Add("addressCity", "Bedford");
            caddress.Add("addressCounty", "Middlesex");
            caddress.Add("addressState", "MA");
            caddress.Add("addressZip", "01730");
            caddress.Add("addressCountry", "US");
            deathRecord.CertifierAddress = caddress;

            Dictionary<string, string> certifierIdentifier = new Dictionary<string, string>();
            certifierIdentifier.Add("system", "http://hl7.org/fhir/sid/us-npi");
            certifierIdentifier.Add("value", "1234567890");
            deathRecord.CertifierIdentifier = certifierIdentifier;

            // // CertifierLicenseNumber
            // deathRecord.CertifierLicenseNumber = "789123456";

            // ContributingConditions
            deathRecord.ContributingConditions = "Example Contributing Conditions";

            // COD1A
            deathRecord.COD1A = "Rupture of myocardium";

            // INTERVAL1A
            deathRecord.INTERVAL1A = "minutes";

            // // CODE1A
            // Dictionary<string, string> code1a = new Dictionary<string, string>();
            // code1a.Add("code", "I21.0");
            // code1a.Add("system", "http://hl7.org/fhir/sid/icd-10");
            // code1a.Add("display", "Acute transmural myocardial infarction of anterior wall");
            // deathRecord.CODE1A = code1a;

            // COD1B
            deathRecord.COD1B = "Acute myocardial infarction";

            // INTERVAL1B
            deathRecord.INTERVAL1B = "6 days";

            // // CODE1B
            // Dictionary<string, string> code1b = new Dictionary<string, string>();
            // code1b.Add("code", "I21.9");
            // code1b.Add("system", "http://hl7.org/fhir/sid/icd-10");
            // code1b.Add("display", "Acute myocardial infarction, unspecified");
            // deathRecord.CODE1B = code1b;

            // COD1C
            deathRecord.COD1C = "Coronary artery thrombosis";

            // INTERVAL1C
            deathRecord.INTERVAL1C = "5 years";

            // COD1D
            deathRecord.COD1D = "Atherosclerotic coronary artery disease";

            // INTERVAL1D
            deathRecord.INTERVAL1D = "7 years";

            // GivenNames
            deathRecord.GivenNames = new string[] { "Example", "Something", "Middle" };

            // FamilyName
            deathRecord.FamilyName = "Last";

            // Suffix
            deathRecord.Suffix = "Jr.";

            // Gender
            deathRecord.SexAtDeathHelper = ValueSets.AdministrativeGender.Male;

            // BirthSex
            //deathRecord.BirthSex = "F";

            // DateOfBirth
            deathRecord.DateOfBirth = "1940-02-19";

            // Residence
            Dictionary<string, string> raddress = new Dictionary<string, string>();
            raddress.Add("addressLine1", "101 Example Street");
            raddress.Add("addressLine2", "Line 2");
            raddress.Add("addressCity", "Bedford");
            raddress.Add("addressCounty", "Middlesex");
            raddress.Add("addressState", "MA");
            raddress.Add("addressZip", "01730");
            raddress.Add("addressCountry", "US");
            deathRecord.Residence = raddress;

            // ResidenceWithinCityLimits
            deathRecord.ResidenceWithinCityLimitsHelper = ValueSets.YesNoUnknown.No;

            // SSN
            deathRecord.SSN = "123456789";

            // Ethnicity
            deathRecord.Ethnicity2Helper = ValueSets.HispanicNoUnknown.Yes;

            // Race
            Tuple<string, string>[] race = { Tuple.Create(NvssRace.White, "Y"), Tuple.Create(NvssRace.NativeHawaiian, "Y"), Tuple.Create(NvssRace.OtherPacificIslander, "Y") };
            deathRecord.Race = race;

            // MaritalStatus
            Dictionary<string, string> mscode = new Dictionary<string, string>();
            mscode.Add("code", "S");
            mscode.Add("system", "http://terminology.hl7.org/CodeSystem/v3-MaritalStatus");
            mscode.Add("display", "Never Married");
            deathRecord.MaritalStatus = mscode;

            // FatherGivenNames
            string[] fnames = { "Father", "Middle" };
            deathRecord.FatherGivenNames = fnames;

            // FatherFamilyName
            deathRecord.FatherFamilyName = "Last";

            // FatherSuffix
            deathRecord.FatherSuffix = "Sr.";

            // MotherGivenNames
            string[] mnames = { "Mother", "Middle" };
            deathRecord.MotherGivenNames = mnames;

            // MotherMaidenName
            deathRecord.MotherMaidenName = "Maiden";

            // MotherSuffix
            deathRecord.MotherSuffix = "Dr.";

            // SpouseGivenNames
            string[] spnames = { "Spouse", "Middle" };
            deathRecord.SpouseGivenNames = spnames;

            // SpouseFamilyName
            deathRecord.SpouseFamilyName = "Last";

            // SpouseSuffix
            deathRecord.SpouseSuffix = "Ph.D.";

            // BirthRecordId
            deathRecord.BirthRecordId = "4242123";

            // BirthRecordState
            deathRecord.BirthRecordState = "MA";

            // BirthRecordYear
            deathRecord.BirthRecordYear = "1940";

            // UsualOccupation
            deathRecord.UsualOccupation = "secretary";

            // UsualIndustry
            deathRecord.UsualIndustry = "State agency";

            // MilitaryService
            Dictionary<string, string> mserv = new Dictionary<string, string>();
            mserv.Add("code", "Y");
            mserv.Add("system", "http://terminology.hl7.org/CodeSystem/v2-0136");
            mserv.Add("display", "Yes");
            deathRecord.MilitaryService = mserv;

            // // MorticianGivenNames
            // string[] fdnames = { "FD", "Middle" };
            // deathRecord.MorticianGivenNames = fdnames;

            // // MorticianFamilyName
            // deathRecord.MorticianFamilyName = "Last";

            // // MorticianSuffix
            // deathRecord.MorticianSuffix = "Jr.";

            // // MorticianIdentifier
            // var mortId = new Dictionary<string, string>();
            // mortId["value"] = "9876543210";
            // mortId["system"] = "http://hl7.org/fhir/sid/us-npi";
            // deathRecord.MorticianIdentifier = mortId;

            // FuneralHomeAddress
            Dictionary<string, string> fdaddress = new Dictionary<string, string>();
            fdaddress.Add("addressLine1", "1011010 Example Street");
            fdaddress.Add("addressLine2", "Line 2");
            fdaddress.Add("addressCity", "Bedford");
            fdaddress.Add("addressCounty", "Middlesex");
            fdaddress.Add("addressState", "MA");
            fdaddress.Add("addressZip", "01730");
            fdaddress.Add("addressCountry", "US");
            deathRecord.FuneralHomeAddress = fdaddress;

            // FuneralHomeName
            deathRecord.FuneralHomeName = "Smith Funeral Home";

            // FuneralDirectorPhone
            //deathRecord.FuneralDirectorPhone = "000-000-0000";

            // DispositionLocationAddress
            Dictionary<string, string> dladdress = new Dictionary<string, string>();
            dladdress.Add("addressLine1", "603 Example Street");
            dladdress.Add("addressLine2", "Line 2");
            dladdress.Add("addressCity", "Bedford");
            dladdress.Add("addressCounty", "Middlesex");
            dladdress.Add("addressState", "MA");
            dladdress.Add("addressZip", "01730");
            dladdress.Add("addressCountry", "US");
            deathRecord.DispositionLocationAddress = dladdress;

            // DispositionLocationName
            deathRecord.DispositionLocationName = "Bedford Cemetery";

            // DecedentDispositionMethod
            Dictionary<string, string> ddm = new Dictionary<string, string>();
            ddm.Add("code", "449971000124106");
            ddm.Add("system", "http://snomed.info/sct");
            ddm.Add("display", "Burial");
            deathRecord.DecedentDispositionMethod = ddm;

            // AutopsyPerformedIndicator
            Dictionary<string, string> api = new Dictionary<string, string>();
            api.Add("code", "Y");
            api.Add("system", "http://terminology.hl7.org/CodeSystem/v2-0136");
            api.Add("display", "Yes");
            deathRecord.AutopsyPerformedIndicator = api;

            // AutopsyResultsAvailable
            Dictionary<string, string> ara = new Dictionary<string, string>();
            ara.Add("code", "Y");
            ara.Add("system", "http://terminology.hl7.org/CodeSystem/v2-0136");
            ara.Add("display", "Yes");
            deathRecord.AutopsyResultsAvailable = ara;

            // PregnancyStatus
            Dictionary<string, string> ps = new Dictionary<string, string>();
            ps.Add("code", "NA");
            ps.Add("system", "http://terminology.hl7.org/CodeSystem/v3-NullFlavor");
            ps.Add("display", "not applicable");
            deathRecord.PregnancyStatus = ps;

            // TransportationRole
            Dictionary<string, string> tr = new Dictionary<string, string>();
            tr.Add("code", "257500003");
            tr.Add("system", "http://snomed.info/sct");
            tr.Add("display", "Passenger");
            deathRecord.TransportationRole = tr;

            // ExaminerContacted
            deathRecord.ExaminerContactedHelper = "N";

            // TobaccoUse
            Dictionary<string, string> tbu = new Dictionary<string, string>();
            tbu.Add("code", "373066001");
            tbu.Add("system", "http://snomed.info/sct");
            tbu.Add("display", "Yes");
            deathRecord.TobaccoUse = tbu;

            // InjuryLocationAddress
            Dictionary<string, string> iladdress = new Dictionary<string, string>();
            iladdress.Add("addressLine1", "781 Example Street");
            iladdress.Add("addressLine2", "Line 2");
            iladdress.Add("addressCity", "Bedford");
            iladdress.Add("addressCounty", "Hamilton");
            iladdress.Add("addressState", "IN");
            iladdress.Add("addressZip", "46032");
            iladdress.Add("addressCountry", "US");
            deathRecord.InjuryLocationAddress = iladdress;

            // InjuryLocationName
            deathRecord.InjuryLocationName = "Example Injury Location Name";

            // InjuryDescription
            deathRecord.InjuryDescription = "Example Injury Description";

            // InjuryLocationDescription
            //deathRecord.InjuryLocationDescription = "Example Injury Location Description";

            // InjuryDate
            deathRecord.InjuryDate = "2018-02-19T16:48:06-05:00";

            // InjuryAtWork
            Dictionary<string, string> codeIW = new Dictionary<string, string>();
            codeIW.Add("code", "N");
            codeIW.Add("system", "http://terminology.hl7.org/CodeSystem/v2-0136");
            codeIW.Add("display", "No");
            deathRecord.InjuryAtWork = codeIW;

            // InjuryPlace
            deathRecord.InjuryPlaceDescription = "Home";

            // DeathLocationAddress
            Dictionary<string, string> dtladdress = new Dictionary<string, string>();
            dtladdress.Add("addressLine1", "671 Example Street");
            dtladdress.Add("addressLine2", "Line 2");
            dtladdress.Add("addressCity", "Bedford");
            dtladdress.Add("addressCounty", "Middlesex");
            dtladdress.Add("addressState", "MA");
            dtladdress.Add("addressZip", "01730");
            dtladdress.Add("addressCountry", "US");
            deathRecord.DeathLocationAddress = dtladdress;

            // DeathLocationName
            deathRecord.DeathLocationName = "Example Death Location Name";

            // DeathLocationDescription
            deathRecord.DeathLocationDescription = "Example Death Location Description";

            // DeathLocationType
            Dictionary<string, string> deathLocationCode = new Dictionary<string, string>();
            deathLocationCode.Add("code", "16983000");
            deathLocationCode.Add("system", "http://snomed.info/sct");
            deathLocationCode.Add("display", "Death in hospital");
            deathRecord.DeathLocationType = deathLocationCode;

            // DeathLocationJurisdiction
            deathRecord.DeathLocationJurisdiction = "MA";

            // DateOfDeath
            deathRecord.DateOfDeath = "2018-02-19T16:48:06-05:00";

            // AgeAtDeath
            Dictionary<string, string> aad = new Dictionary<string, string>();
            aad.Add("code", "a");
            aad.Add("value", "79");
            deathRecord.AgeAtDeath = aad;

            // DateOfDeathPronouncement
            deathRecord.DateOfDeathPronouncement = "2018-02-20T16:48:06-05:00";


            IJEMortality ije = new IJEMortality(deathRecord);

            Assert.Equal("Massachusetts", ije.DISPSTATE);
        }

        [Fact]
        public void TestTimeValues()
        {
            IJEMortality ijem;
            string deathTime;

            ijem = new IJEMortality();
            ijem.TOD = "1239";
            deathTime = ijem.ToDeathRecord().DeathTime;
            Assert.Matches("^\\d{2}:\\d{2}:\\d{2}$", deathTime);

            ijem = new IJEMortality();
            ijem.TOD = "0255";
            deathTime = ijem.ToDeathRecord().DeathTime;
            Assert.Matches("^\\d{2}:\\d{2}:\\d{2}$", deathTime);

            ijem = new IJEMortality();
            ijem.TOD = "0299";
            deathTime = ijem.ToDeathRecord().DeathTime;
            Assert.DoesNotMatch("^\\d{2}:\\d{2}:\\d{2}$", deathTime);

            ijem = new IJEMortality();
            ijem.TOD = "not a valid date";
            deathTime = ijem.ToDeathRecord().DeathTime;
            Assert.DoesNotMatch("^\\d{2}:\\d{2}:\\d{2}$", deathTime);
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
