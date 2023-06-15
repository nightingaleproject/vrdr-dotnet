using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace VRDR
{
    /// <summary>Class <c>Connectathon</c> provides static methods for generating records used in Connectathon testing, used in Canary and in the CLI tool</summary>
    public class Connectathon
    {
        /// <summary>Retrieve all available pre-set records</summary>
        public static DeathRecord[] Records
        {
            get { 
                return new DeathRecord[] {
                    TwilaHilty(),
                    FideliaAlsup(),
                    DavisLineberry()
                }; 
            }
        }

        /// <summary>Generate a DeathRecord from one of 3 pre-set records, providing an optional certificate number and state</summary>
        public static DeathRecord FromId(int id, int? certificateNumber = null, string state = null, int? year = null)
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

            if (record != null && certificateNumber != null)
            {
                record.Identifier = certificateNumber.ToString();
            }

            if (record != null && state != null)
            {
                record.DeathLocationJurisdiction = state;
            }

            if (record != null && year != null)
            {
                record.DeathYear = year;
            }

            return record;
        }

        /// <summary>Generate the Twila Hilty example record</summary>
        public static DeathRecord TwilaHilty()
        {
            IJEMortality ije = new IJEMortality();
            ije.DOD_YR = "2023";
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
            ije.AGE = "021";
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
            ije.PREG = "8";
            ije.PREG_BYPASS = "0";
            ije.CERTL = "D";
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
            ije.COD1A = "Hypoxemia";
            ije.INTERVAL1A = "4 Days";
            ije.COD1B = "MRSA Pneumonia";
            ije.INTERVAL1B = "4 Days";
            ije.PLACE1_1 = "Y";
            DeathRecord record = ije.ToDeathRecord();
            return record;
        }


        /// <summary>Generate the Fidelia Alsup example record</summary>
        public static DeathRecord FideliaAlsup()
        {
            IJEMortality ije = new IJEMortality();
            ije.DOD_YR = "2023";
            ije.DSTATE = "CT";
            ije.FILENO = "000002";
            ije.MFILED = "2";
            ije.GNAME = "Fidelia";
            ije.LNAME = "Alsup";
            ije.FLNAME = "Dill";
            ije.SEX = "F";
            ije.SSN = "478151044";
            ije.AGETYPE = "1";
            ije.AGE = "063";
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
            ije.DOR_YR = "2022";
            ije.DOR_MO = "03";
            ije.DOR_DY = "18";
            ije.MANNER = "N";
            ije.AUTOP = "Y";
            ije.AUTOPF = "Y";
            ije.TOBAC = "U";
            ije.PREG = "1";
            ije.PREG_BYPASS = "0";
            ije.CERTL = "P";
            ije.STATESP = "20220101";
            ije.STNUM_R = "1829";
            ije.STNAME_R = "Main";
            ije.STDESIG_R = "St";
            ije.CITYTEXT_R = "Chico";
            ije.ZIP9_R = "95926";
            ije.COUNTYTEXT_R = "Butte";
            ije.STATETEXT_R = "California";
            ije.COUNTRYTEXT_R = "United States";
            ije.ADDRESS_R = "1829 Main St";
            ije.COD1A = "Hepatorenal Syndrome";
            ije.INTERVAL1A = "Days to Weeks";
            ije.COD1B = "Alcoholic Hepatitis";
            ije.INTERVAL1B = "Weeks";
            ije.COD1C = "Acute Liver Failure";
            ije.INTERVAL1C = "Weeks";
            ije.COD1D = "Hepatic Encephalopathy";
            ije.INTERVAL1D = "Weeks";
            ije.OTHERCONDITION = "Alcoholism";
            ije.PLACE8_1 = "00000033";
            DeathRecord record = ije.ToDeathRecord();
            return record;
        }

        /// <summary>Generate the Davis Lineberry example record</summary>
        public static DeathRecord DavisLineberry()
        {
            IJEMortality ije = new IJEMortality();
            ije.DOD_YR = "2023";//"2022";
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
            ije.AGE = "003";
            ije.AGE_BYPASS = "0";
            ije.DOB_YR = "2023";
            ije.DOB_MO = "01";
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
            ije.IDOB_YR = "2023";
            ije.BSTATE = "CO";
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
            ije.STATETEXT_R = "Wyoming";
            ije.COUNTRYTEXT_R = "United States";
            ije.ADDRESS_R = "2722 N Pin Oak Dr";
            ije.COD1A = "Pending";
            ije.PLACE20 = "043-A-110234";
            DeathRecord record = ije.ToDeathRecord();
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
