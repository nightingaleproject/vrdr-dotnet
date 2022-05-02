using System;
using System.Collections.Generic;

namespace VRDR
{
    /// <summary>Class <c>Connectathon</c> provides static methods for generating records used in Connectathon testing, used in Canary and in the CLI tool</summary>
    public class Connectathon
    {
        /// <summary>Generate a DeathRecord from one of 5 pre-set records, providing an optional certificate number and state</summary>
        public static DeathRecord FromId(int id, int? certificateNumber = null, string state = null)
        {
            DeathRecord record = null;
            switch (id)
            {
                case 1:
                    record = TwilaHilty();
                    break;
                case 2:
                    record = FideliaAlsup();
                    break;
                case 3:
                    record = DavisLineberry();
                    break;
            }

            if (record != null && state != null)
            {
                record.DeathLocationJurisdiction = state;
            }

            if (record != null && certificateNumber != null)
            {
                record.Identifier = certificateNumber.ToString();
            }

            return record;
        }

    /// <summary>Generate the Twila Hilty example record</summary>
    public static DeathRecord TwilaHilty()
    {
    	IJEMortality ije = new IJEMortality(""); // blank IJE
		ije.DOD_YR = "2022";
		ije.DSTATE = "CT";
		ije.FILENO = "000001";
		ije.MFILED = "0";
		ije.GNAME = "Twila";
		ije.MNAME = "R";
		ije.LNAME = "Hilty";
		ije.FLNAME = "Brown";
		ije.DMIDDLE = "Roxanne";
		ije.SEX = "F";
		ije.SSN = "531869507";
		ije.AGETYPE = "1";
		ije.AGE  = "020";
		ije.AGE_BYPASS = "0";
		ije.DOB_YR = "2002";
		ije.DOB_MO = "01";
		ije.DOB_DY = "01";
		ije.BPLACE_CNT = "US";
		ije.BPLACE_ST = "CT";
		ije.CITYC = "37000";
		ije.COUNTYC = "003";
		ije.STATEC = "CT";
		ije.COUNTRYC = "US";
		ije.LIMITS = "Y";
		ije.MARITAL = "S";
		ije.MARITAL_BYPASS = "0";
		ije.DPLACE = "1";
		ije.COD = "001";
		ije.DISP = "B";
		ije.DOD_MO = "01";
		ije.DOD_DY = "10";
		ije.TOD = "1000";
		ije.DEDUC = "8";
		ije.DEDUC_BYPASS = "0";
		ije.DETHNIC1 = "N";
		ije.DETHNIC2 = "N";
		ije.DETHNIC3 = "N";
		ije.DETHNIC4 = "N";
		ije.RACE1 = "Y";
		ije.RACE2 = "N";
		ije.RACE3 = "N";
		ije.RACE4 = "N";
		ije.RACE5 = "N";
		ije.RACE6 = "N";
		ije.RACE7 = "N";
		ije.RACE8 = "N";
		ije.RACE9 = "N";
		ije.RACE10 = "N";
		ije.RACE11 = "N";
		ije.RACE12 = "N";
		ije.RACE13 = "N";
		ije.RACE14 = "N";
		ije.RACE15 = "N";
		ije.OCCUP = "Teacher";
		ije.INDUST = "Education";
		ije.MANNER = "N";
		ije.AUTOP = "N";
		ije.AUTOPF = "X";
		ije.TOBAC = "U";
		ije.PREG = "2";
		ije.PREG_BYPASS = "0";
		ije.CERTL = "D";
		ije.SUR_MO = "01";
		ije.SUR_DY = "10";
		ije.SUR_YR = "2022";
		ije.STNUM_R = "4437";
		ije.PREDIR_R = "North";
		ije.STNAME_R = "Charles";
		ije.STDESIG_R = "Avenue";
		ije.POSTDIR_R = "Southeast";
		ije.UNITNUM_R = "Apt 2B";
		ije.CITYTEXT_R = "Hartford";
		ije.ZIP9_R = "06107";
		ije.COUNTYTEXT_R = "Hartford";
		ije.STATETEXT_R  = "Connecticut";
		ije.COUNTRYTEXT_R = "United States";
		ije.ADDRESS_R = "4437 North Charles Avenue Southeast Apt 2B";
		ije.COD1A = "Cardiopulmonary arrest";
		ije.INTERVAL1A = "4 Hours";
		ije.COD1B = "Eclampsia";
		ije.INTERVAL1B = "3 Months";
		ije.PLACE1_1 = "Y";
	    DeathRecord record = ije.ToDeathRecord();
    	return record;
    }


 /// <summary>Generate the Fidelia Alsup example record</summary>
public static DeathRecord FideliaAlsup()
    {
    	IJEMortality ije = new IJEMortality(""); // blank IJE
		ije.DOD_YR = "2022";
		ije.DSTATE = "CT";
		ije.FILENO = "000002";
		ije.MFILED = "2";
		ije.GNAME = "Fidelia";
		ije.LNAME = "Alsup";
		ije.FLNAME = "Dill";
		ije.SEX = "F";
		ije.SSN = "478151044";
		ije.AGETYPE = "1";
		ije.AGE  = "62";
		ije.AGE_BYPASS = "0";
		ije.DOB_YR = "1960";
		ije.DOB_MO = "02";
		ije.DOB_DY = "29";
		ije.BPLACE_CNT = "US";
		ije.BPLACE_ST = "CA";
		ije.CITYC = "13014";
		ije.COUNTYC = "007";
		ije.STATEC = "CA";
		ije.COUNTRYC = "US";
		ije.LIMITS = "Y";
		ije.MARITAL = "A";
		ije.MARITAL_BYPASS = "0";
		ije.DPLACE = "3";
		ije.COD = "001";
		ije.DISP = "O";
		ije.DOD_MO = "03";
		ije.DOD_DY = "16";
		ije.TOD = "1040";
		ije.DEDUC = "4";
		ije.DEDUC_BYPASS = "0";
		ije.DETHNIC1 = "N";
		ije.DETHNIC2 = "H";
		ije.DETHNIC3 = "H";
		ije.DETHNIC4 = "N";
		ije.RACE1 = "N";
		ije.RACE2 = "Y";
		ije.RACE3 = "N";
		ije.RACE4 = "N";
		ije.RACE5 = "N";
		ije.RACE6 = "N";
		ije.RACE7 = "N";
		ije.RACE8 = "N";
		ije.RACE9 = "N";
		ije.RACE10 = "N";
		ije.RACE11 = "N";
		ije.RACE12 = "N";
		ije.RACE13 = "N";
		ije.RACE14 = "N";
		ije.RACE15 = "N";
		ije.OCCUP = "Carpenter";
		ije.INDUST = "Construction";
		ije.DOR_YR = "03";
		ije.DOR_MO = "18";
		ije.DOR_DY = "2022";
		ije.MANNER = "A";
		ije.AUTOP = "N";
		ije.AUTOPF = "X";
		ije.TOBAC = "U";
		ije.PREG = "8";
		ije.PREG_BYPASS = "0";
		ije.DOI_MO = "03";
		ije.DOI_DY = "16";
		ije.DOI_YR = "2022";
		ije.TOI_HR = "1015";
		ije.WORKINJ = "Y";
		ije.CERTL = "P";
		ije.STATESP = "20220101";
		ije.STNUM_R = "1829";
		ije.STNAME_R = "Main";
		ije.STDESIG_R = "St";
		ije.CITYTEXT_R = "Chico";
		ije.ZIP9_R = "95926";
		ije.COUNTYTEXT_R = "Butte";
		ije.STATETEXT_R  = "California";
		ije.COUNTRYTEXT_R = "United States";
		ije.ADDRESS_R = "1829 Main St";
		ije.POILITRL = "Street";
		ije.HOWINJ = "Unrestrained ejected driver in rollover motor vehicle accident";
		ije.TRANSPRT = "DR";
		ije.COD1A = "Blunt head trauma";
		ije.INTERVAL1A = "30 Minutes";
		ije.COD1B = "Automobile accident";
		ije.INTERVAL1B = "30 Minutes";
		ije.COD1C = "Epilepsy";
		ije.INTERVAL1C = "20 Years";
		ije.OTHERCONDITION = "Hypertension, Depression, Migraine";
		ije.PLACE8_1 = "00000033";
		DeathRecord record = ije.ToDeathRecord();
    	return record;
    }

 /// <summary>Generate the Davis Lineberry example record</summary>
public static DeathRecord DavisLineberry()
    {
    	IJEMortality ije = new IJEMortality(""); // blank IJE
		ije.DOD_YR = "2022";
		ije.DSTATE = "CT";
		ije.FILENO = "000003";
		ije.MFILED = "0";
		ije.GNAME = "Davis";
		ije.LNAME = "Lineberry";
		ije.SUFF = "Jr";
		ije.FLNAME = "Lineberry";
		ije.SEX = "M";
		ije.SSN = "429471420";
		ije.AGETYPE = "2";
		ije.AGE  = "010";
		ije.AGE_BYPASS = "0";
		ije.DOB_YR = "2021";
		ije.DOB_MO = "03";
		ije.DOB_DY = "04";
		ije.BPLACE_CNT = "US";
		ije.BPLACE_ST = "CO";
		ije.CITYC = "45050";
		ije.COUNTYC = "001";
		ije.STATEC = "WY";
		ije.COUNTRYC = "US";
		ije.LIMITS = "Y";
		ije.MARITAL = "S";
		ije.MARITAL_BYPASS = "0";
		ije.DPLACE = "1";
		ije.COD = "001";
		ije.DISP = "B";
		ije.DOD_MO = "01";
		ije.DOD_DY = "17";
		ije.TOD = "1823";
		ije.DEDUC = "1";
		ije.DEDUC_BYPASS = "0";
		ije.DETHNIC1 = "N";
		ije.DETHNIC2 = "N";
		ije.DETHNIC3 = "N";
		ije.DETHNIC4 = "N";
		ije.RACE1 = "Y";
		ije.RACE2 = "N";
		ije.RACE3 = "Y";
		ije.RACE4 = "N";
		ije.RACE5 = "N";
		ije.RACE6 = "N";
		ije.RACE7 = "N";
		ije.RACE8 = "N";
		ije.RACE9 = "N";
		ije.RACE10 = "N";
		ije.RACE11 = "N";
		ije.RACE12 = "N";
		ije.RACE13 = "N";
		ije.RACE14 = "N";
		ije.RACE15 = "N";
		ije.RACE16 = "Cheyenne";
		ije.OCCUP = "Infant";
		ije.INDUST = "Never Worked";
		ije.BCNO = "001023";
		ije.IDOB_YR = "2021";
		ije.BSTATE = "CA";
		ije.MANNER = "P";
		ije.AUTOP = "U";
		ije.AUTOPF = "U";
		ije.TOBAC = "N";
		ije.PREG = "8";
		ije.PREG_BYPASS = "0";
		ije.STNUM_R = "2722";
		ije.PREDIR_R = "N";
		ije.STNAME_R = "Pin Oak";
		ije.STDESIG_R = "Dr";
		ije.CITYTEXT_R = "Laramie";
		ije.ZIP9_R = "82070";
		ije.COUNTYTEXT_R = "Albany";
		ije.STATETEXT_R  = "Wyoming";
		ije.COUNTRYTEXT_R = "United States";
		ije.ADDRESS_R = "2722 N Pin Oak Dr";
		ije.COD1A = "Pending";
		ije.PLACE20 = "043-A-110234";
	    DeathRecord record = ije.ToDeathRecord();
    	return record;
    }

        /// <summary>Generate the Javier Luis Perez example record; this can be used to generate either a partial or full record</summary>
        public static DeathRecord JavierLuisPerez(bool partialRecord = false)
        {
            bool fullRecord = !partialRecord;
            DeathRecord record = new DeathRecord();

            record.Identifier = "000004";

            record.RegisteredTime = "2021-03-15";

            record.GivenNames = new string[] { "Javier", "Luis" };

            record.FamilyName = "Perez";
            record.Suffix = "Jr.";

            record.Race = new Tuple<string, string>[] { Tuple.Create(NvssRace.White, "Y"), Tuple.Create(NvssRace.BlackOrAfricanAmerican, "Y") };

            record.Ethnicity3Helper = "Y";

            record.BirthSex = "M";

            record.SSN = "456123789";

            Dictionary<string, string> age = new Dictionary<string, string>();


            if (fullRecord)
            {
                age.Add("value", "57");
                age.Add("unit", "a");
                record.AgeAtDeath = age;
                record.DateOfBirth = "1964-02-24";

                Dictionary<string, string> addressB = new Dictionary<string, string>();
                addressB.Add("addressCountry", "US");
                addressB.Add("addressState", "TX");
                record.PlaceOfBirth = addressB;

                record.BirthRecordId = "818181";
                record.BirthRecordState = "TX";
            }
            else
            {
                record.BirthMonth = 2;
                record.BirthDay = 24;
                Dictionary<string, string> addressB = new Dictionary<string, string>();
                addressB.Add("addressCountry", "AS");  // This is an abomination
                addressB.Add("addressState", "");
                record.PlaceOfBirth = addressB;
                record.BirthRecordIdentifierDataAbsentBoolean = true;
                record.AgeAtDeathDataAbsentBoolean = true;
            }

            if (fullRecord)
            {
                record.MotherGivenNames = new String[] { "Liliana" };
                record.MotherMaidenName = "Jones";
            }

            record.FatherFamilyName = "Perez";

            Dictionary<string, string> code = new Dictionary<string, string>();
            code.Add("code", "M");
            code.Add("system", VRDR.CodeSystems.PH_MaritalStatus_HL7_2x);
            code.Add("display", "Married");
            record.MaritalStatus = code;

            Dictionary<string, string> addressR = new Dictionary<string, string>();
            if (fullRecord)
            {
                addressR.Add("addressLine1", "143 Taylor Street");
                addressR.Add("addressCountry", "US");
            }else{
                addressR.Add("addressCountry", "ZZ");
            }
            addressR.Add("addressCity", "Annapolis");
            addressR.Add("addressCounty", "Anne Arundel");
            addressR.Add("addressState", "MD");

            record.Residence = addressR;
            record.ResidenceWithinCityLimitsBoolean = false;

            Dictionary<string, string> elevel = new Dictionary<string, string>();
            elevel.Add("code", "PHC1454");
            elevel.Add("system", VRDR.CodeSystems.PH_PHINVS_CDC);
            elevel.Add("display", "Master's Degree");
            record.EducationLevel = elevel;

            Dictionary<string, string> uocc = new Dictionary<string, string>();
            uocc.Add("code", "6230");
            uocc.Add("system", VRDR.CodeSystems.PH_Occupation_CDC_Census2010);
            uocc.Add("display", "carpenters");
            uocc.Add("text", "carpenter");
            record.UsualOccupationCode = uocc;

            Dictionary<string, string> uind = new Dictionary<string, string>();
            uind.Add("code", "0770");
            uind.Add("system", VRDR.CodeSystems.PH_Industry_CDC_Census2010);
            uind.Add("display", "Construction");    // actual text is "Construction (the cleaning of buildings and dwellings is incidental during construction and immediately after construction)"
            uind.Add("text", "construction");
            record.UsualIndustryCode = uind;

            record.DateOfDeath = "2021-03-14T11:25:00";

            record.DeathLocationName = "County Hospital";
            Dictionary<string, string> locType = new Dictionary<string, string>();
            locType.Add("code", "63238001");
            locType.Add("system", VRDR.CodeSystems.SCT);
            locType.Add("display", "Hospital Dead on Arrival");
            record.DeathLocationType = locType;

            record.DeathLocationDescription = "Dead On Arrival";
            record.DeathLocationJurisdiction = "DE";

            Dictionary<string, string> role = new Dictionary<string, string>();
            role.Add("code", "434641000124105");
            role.Add("system", VRDR.CodeSystems.SCT);
            role.Add("display", "Death certification and verification by physician");
            record.CertificationRole = role;

            record.CertifierGivenNames = new string[] { "Hope" };
            record.CertifierFamilyName = "Lost";

            Dictionary<string, string> address = new Dictionary<string, string>();
            address.Add("addressLine1", "RR1");
            address.Add("addressCity", "Dover");
            address.Add("addressState", "DE");
            address.Add("addressCountry", "US");
            record.CertifierAddress = address;

            record.CertifiedTime = "2021-03-14";

            record.DateOfDeathPronouncement = "2021-03-14T11:35:00";

            // if (fullRecord)
            // {
            //     record.PronouncerGivenNames = new string[] { "Hope" };
            //     record.PronouncerFamilyName = "Lost";
            // }

            record.COD1A = "Blunt head trauma";
            record.COD1B = "Automobile accident";
            record.ExaminerContactedHelper = "UNK"; // use null to set to unknown
            if (fullRecord)
            {
                record.INTERVAL1A = "30 min";
                record.INTERVAL1B = "30 min";
                record.COD1C = "Epilepsy";
                record.INTERVAL1C = "20 years";
                record.ExaminerContactedHelper = "Y";
                record.InjuryLocationDescription = "15 Industrial Drive 19901 US";
            }


            Dictionary<string, string> manner = new Dictionary<string, string>();
            manner.Add("code", "7878000");
            manner.Add("system", VRDR.CodeSystems.SCT);
            manner.Add("display", "Accidental death");
            record.MannerOfDeathType = manner;

            record.InjuryDate = "2021-03-14T11:15:00";
            record.InjuryAtWorkHelper = "Y";

            // Dictionary<string, string> injuryPlace = new Dictionary<string, string>();
            // injuryPlace.Add("code", "4");
            // injuryPlace.Add("system", VRDR.CodeSystems.PH_PlaceOfOccurrence_ICD_10_WHO);
            // injuryPlace.Add("display", "street");
            // record.InjuryPlace = injuryPlace;

            record.InjuryDescription = "unrestrained ejected driver in rollover motor vehicle accident";



            Dictionary<string, string> codeT = new Dictionary<string, string>();
            codeT.Add("code", "236320001");
            codeT.Add("system", VRDR.CodeSystems.SCT);
            codeT.Add("display", "Vehicle driver");
            record.TransportationRole = codeT;
            // record.TransportationEventBoolean = true;
            record.AutopsyPerformedIndicatorHelper = "N";
            record.AutopsyResultsAvailableHelper = "N";
            if (fullRecord)
            {
                Dictionary<string, string> fdaddress = new Dictionary<string, string>();
                fdaddress.Add("addressLine1", "15 Furnace Drive");
                fdaddress.Add("addressCity", "San Antonio");
                fdaddress.Add("addressState", "TX");
                fdaddress.Add("addressZip", " 78201");
                fdaddress.Add("addressCountry", "US");
                record.FuneralHomeAddress = fdaddress;
                record.FuneralHomeName = "River Funeral Home";

                // Dictionary<string, string> morticianId = new Dictionary<string, string>();
                // morticianId.Add("system", VRDR.CodeSystems.US_NPI_HL7);
                // morticianId.Add("value", "313333AB");
                // record.MorticianIdentifier = morticianId;

                // record.MorticianGivenNames = new string[] { "Pedro", "A" };
                // record.MorticianFamilyName = "Jimenez";

                Dictionary<string, string> dladdress = new Dictionary<string, string>();
                dladdress.Add("addressLine1", "15 Furnace Drive");
                dladdress.Add("addressCity", "San Antonio");
                dladdress.Add("addressState", "TX");
                dladdress.Add("addressZip", " 78201");
                dladdress.Add("addressCountry", "US");
                record.DispositionLocationAddress = dladdress;
                record.DispositionLocationName = "River Cemetary";
            }

            Dictionary<string, string> dmethod = new Dictionary<string, string>();
            dmethod.Add("code", "449941000124103");
            dmethod.Add("system", VRDR.CodeSystems.SCT);
            dmethod.Add("display", "Patient status determination, deceased and removed from state");
            record.DecedentDispositionMethod = dmethod;

            Dictionary<string, string> pregnancyStatus = new Dictionary<string, string>();
            pregnancyStatus.Add("code", "NA");
            pregnancyStatus.Add("system", CodeSystems.PH_NullFlavor_HL7_V3);
            pregnancyStatus.Add("display", "not applicable");
            record.PregnancyStatus = pregnancyStatus;

            if (fullRecord)
            {
                record.TobaccoUse = new Dictionary<string, string>() {
                    { "code", "373067005" },
                    { "system", VRDR.CodeSystems.SCT },
                    { "display", "No" } };
            }
            record.MilitaryService = new Dictionary<string, string>() {
                { "code", "N" },
                { "system", CodeSystems.PH_YesNo_HL7_2x },
                { "display", "No" } };

            return record;
        }

        // Writes record to a file named filename in a subdirectory of the current working directory
        // Note that you do this with docker, you will have to set a bind mount on the container
        /// <summary>Of historical interest for writing a record to a file</summary>
        public static string WriteRecordAsXml(DeathRecord record, string filename)
        {
            string parentPath = System.IO.Directory.GetCurrentDirectory() + "/connectathon_files";
            System.IO.Directory.CreateDirectory(parentPath);  // in case the directory does not exist
            string fullPath = parentPath + "/" + filename;
            Console.WriteLine("writing record to " + fullPath + " as XML");
            string xml = record.ToXml();
            System.IO.File.WriteAllText(@fullPath, xml);
            // Console.WriteLine(xml);
            return xml;
        }

    }
}
