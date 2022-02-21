using VRDR;
using System;
using System.Collections.Generic;

namespace VRDR
{
    /// <summary>vrdr-dotnet versions of connectathon records.</summary>
    public class Connectathon
    {
        public static string fhirVersion = "R4";    // used to generate files
        public Connectathon() { }

        public static DeathRecord FromId(int id, int? certificateNumber = null, string state = null)
        {
            DeathRecord record = null;
            switch (id)
            {
                case 1:
                    record = JanetPage();
                    break;
                case 2:
                    record = MadelynPatel();
                    break;
                case 3:
                    record = VivienneLeeWright();
                    break;
                case 4:
                    record = JavierLuisPerez(partialRecord: false);
                    break;
                case 5:
                    record = JavierLuisPerez(partialRecord: true);
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

        public static DeathRecord JanetPage()
        {
            DeathRecord record = new DeathRecord();

            record.Identifier = "000001";

            record.RegisteredTime = "2022-01-09";

            record.GivenNames = new string[] { "Janet" };

            record.FamilyName = "Page";

            record.Race = new Tuple<string, string>[] { Tuple.Create("White", "2106-3") };

            record.Ethnicity = new Tuple<string, string>[] { Tuple.Create("Puerto Rican", "2180-8") };

            record.BirthSex = "F";

            record.SSN = "555111234";

            Dictionary<string, string> age = new Dictionary<string, string>();
            age.Add("value", "72");
            age.Add("unit", "a");
            record.AgeAtDeath = age;

            record.DateOfBirth = "1949-01-15";

            Dictionary<string, string> addressB = new Dictionary<string, string>();
            addressB.Add("addressCity", "Atlanta");
            addressB.Add("addressState", "GA");
            addressB.Add("addressCountry", "US");
            record.PlaceOfBirth = addressB;

            record.BirthRecordId = "515151";
            record.BirthRecordState = new Dictionary<string, string>() {
                { "code", "US-GA" },
                { "system", CodeSystems.ISO_3166_2 },
                { "display", "US-GA" } };

            record.MotherGivenNames = new String[] { "Linda" };
            record.MotherMaidenName = "Shay";

            record.FatherFamilyName = "Page";

            Dictionary<string, string> code = new Dictionary<string, string>();
            code.Add("code", "D");
            code.Add("system", VRDR.CodeSystems.PH_MaritalStatus_HL7_2x);
            code.Add("display", "Divorced");
            record.MaritalStatus = code;

            Dictionary<string, string> addressR = new Dictionary<string, string>();
            addressR.Add("addressLine1", "25 Hope Street");
            addressR.Add("addressCity", "Atlanta");
            addressR.Add("addressCounty", "Fulton");
            addressR.Add("addressState", "GA");
            addressR.Add("addressCountry", "US");
            record.Residence = addressR;
            record.ResidenceWithinCityLimitsBoolean = true;

            Dictionary<string, string> elevel = new Dictionary<string, string>();
            elevel.Add("code", "PHC1455");
            elevel.Add("system", VRDR.CodeSystems.PH_PHINVS_CDC);
            elevel.Add("display", "Doctorate Degree or Professional Degree");
            elevel.Add("text", "Doctorate");
            record.EducationLevel = elevel;

            Dictionary<string, string> uocc = new Dictionary<string, string>();
            uocc.Add("code", "1010");
            uocc.Add("system", VRDR.CodeSystems.PH_Occupation_CDC_Census2010);
            uocc.Add("display", "Computer Programmers");
            uocc.Add("text", "Programmer");
            record.UsualOccupationCode = uocc;

            Dictionary<string, string> uind = new Dictionary<string, string>();
            uind.Add("code", "6990");
            uind.Add("system", VRDR.CodeSystems.PH_Industry_CDC_Census2010);
            uind.Add("display", "Insurance carriers and related activities");
            uind.Add("text", "Health Insurance");
            record.UsualIndustryCode = uind;

            record.DateOfDeath = "2022-01-08T05:30:00";

            Dictionary<string, string> deathLoc = new Dictionary<string, string>();
            deathLoc.Add("addressCity", "Atlanta");
            deathLoc.Add("addressCountry", "US");
            record.DeathLocationAddress = deathLoc;

            record.DeathLocationName = "Pecan Grove Nursing Home";
            Dictionary<string, string> locType = new Dictionary<string, string>();
            locType.Add("code", "450381000124100");
            locType.Add("system", VRDR.CodeSystems.SCT);
            locType.Add("display", "Nursing Home");
            record.DeathLocationType = locType;
            record.DeathLocationDescription = "nursing home";
            record.DeathLocationJurisdiction = "GA";

            Dictionary<string, string> role = new Dictionary<string, string>();
            role.Add("code", "434641000124105");
            role.Add("system", VRDR.CodeSystems.SCT);
            role.Add("display", "Death certification and verification by physician");
            record.CertificationRole = role;

            record.CertifierGivenNames = new string[] { "Sam" };
            record.CertifierFamilyName = "Jones";

            Dictionary<string, string> address = new Dictionary<string, string>();
            address.Add("addressLine1", "1 Main Street");
            address.Add("addressCity", "Atlanta");
            address.Add("addressState", "GA");
            address.Add("addressZip", "30303");
            address.Add("addressCountry", "US");
            record.CertifierAddress = address;

            record.CertifiedTime = "2022-01-08";

            record.DateOfDeathPronouncement = "2022-01-08T05:30:00";
            record.PronouncerGivenNames = new string[] { "Sam" };
            record.PronouncerFamilyName = "Jones";

            record.COD1A = "Congestive heart failure";
            record.INTERVAL1A = "1 hour";

            record.COD1B = "breast cancer";
            record.INTERVAL1B = "20 years";

            record.ExaminerContactedBoolean = false;

            Dictionary<string, string> manner = new Dictionary<string, string>();
            manner.Add("code", "38605008");
            manner.Add("system", VRDR.CodeSystems.SCT);
            manner.Add("display", "Natural death");
            record.MannerOfDeathType = manner;

            record.AutopsyPerformedIndicatorBoolean = false;

            Dictionary<string, string> fdaddress = new Dictionary<string, string>();
            fdaddress.Add("addressLine1", "15 Pecan Street");
            fdaddress.Add("addressCity", "Atlanta");
            fdaddress.Add("addressState", "GA");
            fdaddress.Add("addressZip", " 30301");
            fdaddress.Add("addressCountry", "US");
            record.FuneralHomeAddress = fdaddress;
            record.FuneralHomeName = "Pecan Street Funeral Home and Crematory";
            // record.FuneralDirectorPhone = "000-000-0000";    // unknown property?????

            Dictionary<string, string> morticianId = new Dictionary<string, string>();
            morticianId.Add("system", VRDR.CodeSystems.US_NPI_HL7);
            morticianId.Add("value", "111111AB");
            record.MorticianIdentifier = morticianId;

            record.MorticianGivenNames = new string[] { "Maureen", "P" };
            record.MorticianFamilyName = "Winston";

            Dictionary<string, string> dladdress = new Dictionary<string, string>();
            dladdress.Add("addressLine1", "15 Pecan Street");
            dladdress.Add("addressCity", "Atlanta");
            dladdress.Add("addressState", "GA");
            dladdress.Add("addressZip", " 30301");
            dladdress.Add("addressCountry", "US");
            record.DispositionLocationAddress = dladdress;
            record.DispositionLocationName = "Pecan Street Funeral Home and Crematory";

            Dictionary<string, string> dmethod = new Dictionary<string, string>();
            dmethod.Add("code", "449961000124104");
            dmethod.Add("system", VRDR.CodeSystems.SCT);
            dmethod.Add("display", "Patient status determination, deceased and cremated");
            record.DecedentDispositionMethod = dmethod;

            Dictionary<string, string> pregnancyStatus = new Dictionary<string, string>();
            pregnancyStatus.Add("code", "NA");
            pregnancyStatus.Add("system", CodeSystems.PH_NullFlavor_HL7_V3);
            pregnancyStatus.Add("display", "not applicable");
            record.PregnancyStatus = pregnancyStatus;

            record.TobaccoUse = new Dictionary<string, string>() {
                { "code", "UNK" },
                { "system", CodeSystems.PH_NullFlavor_HL7_V3 },
                { "display", "unknown" } };

            record.MilitaryService = new Dictionary<string, string>() {
                { "code", "UNK" },
                { "system", CodeSystems.PH_NullFlavor_HL7_V3 },
                { "display", "unknown" } };

            // uncomment to generate file
            // string filename = "1_janet_page_cancer_" + fhirVersion + ".xml";
            // WriteRecordAsXml(record, filename);

            return record;
        }

        public static DeathRecord MadelynPatel()
        {
            DeathRecord record = new DeathRecord();

            record.Identifier = "000002";

            record.RegisteredTime = "2022-01-11";

            record.GivenNames = new string[] { "Madelyn" };

            record.FamilyName = "Patel";

            record.Race = new Tuple<string, string>[] { Tuple.Create("Asian Indian", "2029-7") };

            record.Ethnicity = new Tuple<string, string>[] { Tuple.Create("Not Hispanic or Latino", "2186-5") };

            record.BirthSex = "F";

            record.SSN = "187654321";

            Dictionary<string, string> age = new Dictionary<string, string>();
            age.Add("value", "43");
            age.Add("unit", "a");
            record.AgeAtDeath = age;

            record.DateOfBirth = "1978-03-12";

            Dictionary<string, string> addressB = new Dictionary<string, string>();
            addressB.Add("addressCity", "Roanoke");
            addressB.Add("addressState", "VA");
            addressB.Add("addressCountry", "US");
            record.PlaceOfBirth = addressB;

            record.BirthRecordId = "616161";
            record.BirthRecordState = new Dictionary<string, string>() {
                { "code", "US-VA" },
                { "system", CodeSystems.ISO_3166_2 },
                { "display", "US-VA" } };
            record.MotherGivenNames = new String[] { "Jennifer" };
            record.MotherMaidenName = "May";

            record.FatherFamilyName = "Patel";

            Dictionary<string, string> code = new Dictionary<string, string>();
            code.Add("code", "S");
            code.Add("system", VRDR.CodeSystems.PH_MaritalStatus_HL7_2x);
            code.Add("display", "Never Married");
            record.MaritalStatus = code;

            Dictionary<string, string> addressR = new Dictionary<string, string>();
            addressR.Add("addressLine1", "5590 Lockwood Drive");
            addressR.Add("addressCity", "Danville");
            addressR.Add("addressState", "NS");
            addressR.Add("addressCountry", "CA");
            record.Residence = addressR;
            record.ResidenceWithinCityLimitsBoolean = null;

            Dictionary<string, string> elevel = new Dictionary<string, string>();
            elevel.Add("code", "PHC1452");
            elevel.Add("system", VRDR.CodeSystems.PH_PHINVS_CDC);
            elevel.Add("display", "Associate Degree");
            record.EducationLevel = elevel;

            Dictionary<string, string> uocc = new Dictionary<string, string>();
            uocc.Add("code", "4030");
            uocc.Add("system", VRDR.CodeSystems.PH_Occupation_CDC_Census2010);
            uocc.Add("display", "Food preparation workers");
            uocc.Add("text", "Food Prep");
            record.UsualOccupationCode = uocc;

            Dictionary<string, string> uind = new Dictionary<string, string>();
            uind.Add("code", "8680");
            uind.Add("system", VRDR.CodeSystems.PH_Industry_CDC_Census2010);
            uind.Add("display", "Restaurants and other food services");
            uind.Add("text", "Fast food");
            record.UsualIndustryCode = uind;

            record.DateOfDeath = "2022-01-05T14:04:00";

            Dictionary<string, string> deathLoc = new Dictionary<string, string>();
            deathLoc.Add("addressCity", "Danville");
            deathLoc.Add("addressCountry", "US");
            record.DeathLocationAddress = deathLoc;
            record.DeathLocationJurisdiction = "VA";

            record.DeathLocationName = "home";
            Dictionary<string, string> locType = new Dictionary<string, string>();
            locType.Add("code", "440081000124100");
            locType.Add("system", VRDR.CodeSystems.SCT);
            locType.Add("display", "Descedent's Home");
            record.DeathLocationType = locType;
            record.DeathLocationDescription = "home";

            Dictionary<string, string> role = new Dictionary<string, string>();
            role.Add("code", "440051000124108");
            role.Add("system", VRDR.CodeSystems.SCT);
            role.Add("display", "Medical Examiner");
            record.CertificationRole = role;

            record.CertifierGivenNames = new string[] { "Constance" };
            record.CertifierFamilyName = "Green";

            Dictionary<string, string> address = new Dictionary<string, string>();
            address.Add("addressLine1", "123 12th St.");
            address.Add("addressCity", "Danville");
            address.Add("addressState", "VA");
            address.Add("addressCountry", "US");
            record.CertifierAddress = address;

            record.CertifiedTime = "2022-01-05";

            record.DateOfDeathPronouncement = "2022-01-05T15:30:00";
            record.PronouncerGivenNames = new string[] { "Adam" };
            record.PronouncerFamilyName = "Revel";

            record.COD1A = "Cocaine toxicity";
            record.INTERVAL1A = "1 hour";

            record.ContributingConditions = "hypertensive heart disease";

            record.ExaminerContactedBoolean = true;

            Dictionary<string, string> manner = new Dictionary<string, string>();
            manner.Add("code", "7878000");
            manner.Add("system", VRDR.CodeSystems.SCT);
            manner.Add("display", "Accidental death");
            record.MannerOfDeathType = manner;

            record.InjuryDate = "2022-01-05T13:00:00";
            record.InjuryAtWorkBoolean = false;

            // @check
            Dictionary<string, string> injuryPlace = new Dictionary<string, string>();
            injuryPlace.Add("code", "0");
            injuryPlace.Add("system", VRDR.CodeSystems.PH_PlaceOfOccurrence_ICD_10_WHO);
            injuryPlace.Add("display", "Home");
            record.InjuryPlace = injuryPlace;

            record.InjuryDescription = "drug toxicity";

            record.InjuryLocationDescription = "5590 Lockwood Drive 20621 US";

            record.AutopsyPerformedIndicatorBoolean = true;

            record.AutopsyResultsAvailableBoolean = true;

            Dictionary<string, string> fdaddress = new Dictionary<string, string>();
            fdaddress.Add("addressLine1", "Lilly Lane");
            fdaddress.Add("addressCity", "Danville");
            fdaddress.Add("addressState", "VA");
            fdaddress.Add("addressZip", " 24541");
            fdaddress.Add("addressCountry", "US");
            record.FuneralHomeAddress = fdaddress;
            record.FuneralHomeName = "Rosewood Funeral Home";

            Dictionary<string, string> morticianId = new Dictionary<string, string>();
            morticianId.Add("system", VRDR.CodeSystems.US_NPI_HL7);
            morticianId.Add("value", "212222AB");
            record.MorticianIdentifier = morticianId;

            record.MorticianGivenNames = new string[] { "Ronald", "Q" };
            record.MorticianFamilyName = "Smith";

            Dictionary<string, string> dladdress = new Dictionary<string, string>();
            dladdress.Add("addressLine1", "303 Rosewood Ave");
            dladdress.Add("addressCity", "Danville");
            dladdress.Add("addressState", "VA");
            dladdress.Add("addressZip", " 24541");
            dladdress.Add("addressCountry", "US");
            record.DispositionLocationAddress = dladdress;
            record.DispositionLocationName = "Rosewood Cemetary";

            Dictionary<string, string> dmethod = new Dictionary<string, string>();
            dmethod.Add("code", "449971000124106");
            dmethod.Add("system", VRDR.CodeSystems.SCT);
            dmethod.Add("display", "Patient status determination, deceased and buried");
            record.DecedentDispositionMethod = dmethod;

            Dictionary<string, string> pregnancyStatus = new Dictionary<string, string>();
            pregnancyStatus.Add("code", "PHC1260");
            pregnancyStatus.Add("system", VRDR.CodeSystems.PH_PHINVS_CDC);
            pregnancyStatus.Add("display", "Not pregnant within the past year");
            record.PregnancyStatus = pregnancyStatus;
            record.TransportationEventBoolean = false;
            record.TobaccoUse = new Dictionary<string, string>() {
                { "code", "373067005" },
                { "system", VRDR.CodeSystems.SCT },
                { "display", "No" } };
            record.MilitaryService = new Dictionary<string, string>() {
                { "code", "Y" },
                { "system", CodeSystems.PH_YesNo_HL7_2x },
                { "display", "Yes" } };
            // uncomment to generate file
            // string filename = "2_madelyn_patel_opiod_" + fhirVersion + ".xml";
            // WriteRecordAsXml(record, filename);

            return record;
        }

        public static DeathRecord VivienneLeeWright()
        {
            DeathRecord record = new DeathRecord();

            record.Identifier = "000003";

            record.RegisteredTime = "2022-01-14";

            record.GivenNames = new string[] { "Vivienne", "L" };

            record.FamilyName = "Wright";

            record.Race = new Tuple<string, string>[] { Tuple.Create("White", "2106-3"), Tuple.Create("American Indian or Alaska Native", "1002-5") };

            record.Ethnicity = new Tuple<string, string>[] { Tuple.Create("Hispanic or Latino", "2135-2"), Tuple.Create("Salvadoran", "2161-8")};

            record.BirthSex = "F";

            record.SSN = "789456123";

            Dictionary<string, string> age = new Dictionary<string, string>();
            age.Add("value", "19");
            age.Add("unit", "a");
            record.AgeAtDeath = age;

            record.DateOfBirth = "2001-04-11";

            Dictionary<string, string> addressB = new Dictionary<string, string>();
            addressB.Add("addressCity", "Hinsdale");
            addressB.Add("addressState", "IL");
            addressB.Add("addressCountry", "US");
            record.PlaceOfBirth = addressB;

            record.BirthRecordId = "717171";
            record.BirthRecordState = new Dictionary<string, string>() {
                { "code", "US-IL" },
                { "system", CodeSystems.ISO_3166_2 },
                { "display", "US-IL" } };
            record.MotherGivenNames = new String[] { "Martha" };
            record.MotherMaidenName = "White";

            record.FatherFamilyName = "Wright";

            Dictionary<string, string> code = new Dictionary<string, string>();
            code.Add("code", "M");
            code.Add("system", VRDR.CodeSystems.PH_MaritalStatus_HL7_2x);
            code.Add("display", "Married");
            record.MaritalStatus = code;

            Dictionary<string, string> addressR = new Dictionary<string, string>();
            addressR.Add("addressLine1", "101 Liberty Lane");
            addressR.Add("addressCity", "Harrisburg ");
            addressR.Add("addressState", "PA");
            addressR.Add("addressCountry", "US");
            record.Residence = addressR;
            record.ResidenceWithinCityLimitsBoolean = true;

            Dictionary<string, string> elevel = new Dictionary<string, string>();
            elevel.Add("code", "PHC1449");
            elevel.Add("system", VRDR.CodeSystems.PH_PHINVS_CDC);
            elevel.Add("display", "9th through 12th grade; no diploma");
            elevel.Add("text", "11th grade");
            record.EducationLevel = elevel;

            Dictionary<string, string> uocc = new Dictionary<string, string>();
            uocc.Add("code", "5700");
            uocc.Add("system", VRDR.CodeSystems.PH_Occupation_CDC_Census2010);
            uocc.Add("display", "Secretaries and administrative assistants");
            uocc.Add("text", "secretary");
            record.UsualOccupationCode = uocc;

            Dictionary<string, string> uind = new Dictionary<string, string>();
            uind.Add("code", "9390");
            uind.Add("system", VRDR.CodeSystems.PH_Industry_CDC_Census2010);
            uind.Add("display", "Other general government and support");
            uind.Add("text", "State agency");
            record.UsualIndustryCode = uind;

            record.DateOfDeath = "2022-01-13T21:00:00";

            Dictionary<string, string> deathLoc = new Dictionary<string, string>();
            deathLoc.Add("addressCity", "Lancaster");
            deathLoc.Add("addressCounty", "Lancaster");
            deathLoc.Add("addressCountry", "US");
            record.DeathLocationAddress = deathLoc;
            record.DeathLocationJurisdiction = "PA";

            record.DeathLocationName = "Mt. Olive Hospital";
            Dictionary<string, string> locType = new Dictionary<string, string>();
            locType.Add("code", "450391000124102");
            locType.Add("system", VRDR.CodeSystems.SCT);
            locType.Add("display", "Death in emergency Room/Outpatient");
            record.DeathLocationType = locType;
            record.DeathLocationDescription = "Emergency room";

            Dictionary<string, string> role = new Dictionary<string, string>();
            role.Add("code", "310193003");
            role.Add("system", VRDR.CodeSystems.SCT);
            role.Add("display", "Coroner");
            record.CertificationRole = role;

            record.CertifierGivenNames = new string[] { "Jim" };
            record.CertifierFamilyName = "Black";

            Dictionary<string, string> address = new Dictionary<string, string>();
            address.Add("addressLine1", "44 South Street");
            address.Add("addressCity", "Bird in Hand");
            address.Add("addressState", "PA");
            address.Add("addressZip", "17505");
            address.Add("addressCountry", "US");
            record.CertifierAddress = address;

            record.CertifiedTime = "2022-01-13";

            record.DateOfDeathPronouncement = "2022-01-13T21:00:00";
            record.PronouncerGivenNames = new string[] { "Jim" };
            record.PronouncerFamilyName = "Black";

            record.COD1A = "Cardiopulmonary arrest";
            record.INTERVAL1A = "4 hours";

            record.COD1B = "Eclampsia";
            record.INTERVAL1B = "3 months";

            record.ExaminerContactedBoolean = true;

            Dictionary<string, string> manner = new Dictionary<string, string>();
            manner.Add("code", "38605008");
            manner.Add("system", VRDR.CodeSystems.SCT);
            manner.Add("display", "Natural death");
            record.MannerOfDeathType = manner;

            record.AutopsyPerformedIndicatorBoolean = true;
            record.AutopsyResultsAvailableBoolean = true;

            Dictionary<string, string> morticianId = new Dictionary<string, string>();
            morticianId.Add("system", VRDR.CodeSystems.US_NPI_HL7);
            morticianId.Add("value", "414444AB");
            record.MorticianIdentifier = morticianId;

            record.MorticianGivenNames = new string[] { "Joseph", "M" };
            record.MorticianFamilyName = "Clark";

            Dictionary<string, string> fdaddress = new Dictionary<string, string>();
            fdaddress.Add("addressLine1", "211 High Street");
            fdaddress.Add("addressCity", "Lancaster");
            fdaddress.Add("addressState", "PA");
            fdaddress.Add("addressZip", " 17573");
            fdaddress.Add("addressCountry", "US");
            record.FuneralHomeAddress = fdaddress;
            record.FuneralHomeName = "Lancaster Funeral Home and Crematory";

            Dictionary<string, string> dladdress = new Dictionary<string, string>();
            dladdress.Add("addressLine1", "211 High Street");
            dladdress.Add("addressCity", "Lancaster");
            dladdress.Add("addressState", "PA");
            dladdress.Add("addressZip", " 17573");
            dladdress.Add("addressCountry", "US");
            record.DispositionLocationAddress = dladdress;
            record.DispositionLocationName = "Lancaster Funeral Home and Crematory";

            Dictionary<string, string> dmethod = new Dictionary<string, string>();
            dmethod.Add("code", "449961000124104");
            dmethod.Add("system", VRDR.CodeSystems.SCT);
            dmethod.Add("display", "Patient status determination, deceased and cremated");
            record.DecedentDispositionMethod = dmethod;

            Dictionary<string, string> pregnancyStatus = new Dictionary<string, string>();
            pregnancyStatus.Add("code", "PHC1261");
            pregnancyStatus.Add("system", VRDR.CodeSystems.PH_PHINVS_CDC);
            pregnancyStatus.Add("display", "Pregnant at the time of death");
            record.PregnancyStatus = pregnancyStatus;

            record.TobaccoUse = new Dictionary<string, string>() {
                { "code", "373066001" },
                { "system", VRDR.CodeSystems.SCT },
                { "display", "Yes" } };
           record.MilitaryService = new Dictionary<string, string>() {
                { "code", "N" },
                { "system", CodeSystems.PH_YesNo_HL7_2x },
                { "display", "No" } };

            // uncomment to generate file
            // string filename = "3_vivienne_wright_pregnant_" + fhirVersion + ".xml";
            // WriteRecordAsXml(record, filename);

            return record;
        }

        /** this can be used 2 ways, with partialRecord set to
         *                                  false for a full record
         *                               or true for a partial record
         */
        public static DeathRecord JavierLuisPerez(bool partialRecord = false)
        {
            bool fullRecord = !partialRecord;
            DeathRecord record = new DeathRecord();

            record.Identifier = "000004";

            record.RegisteredTime = "2022-01-15";

            record.GivenNames = new string[] { "Javier", "Luis" };

            record.FamilyName = "Perez";
            record.Suffix = "Jr.";

            record.Race = new Tuple<string, string>[] { Tuple.Create("White", "2106-3"), Tuple.Create("Black", "2054-5") };

            record.Ethnicity = new Tuple<string, string>[] { Tuple.Create("Cuban", "2182-4") };

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
                record.BirthRecordState = new Dictionary<string, string>() {
                { "code", "US-TX" },
                { "system", CodeSystems.ISO_3166_2 },
                { "display", "US-TX" } };
            }
            else
            {
                Tuple<string, string>[] datePart = { Tuple.Create("year-absent-reason", "asked-unknown"), Tuple.Create("date-month", "02"), Tuple.Create("date-day", "24")};
                record.DateOfBirthDatePartAbsent = datePart;
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

            record.DateOfDeath = "2022-01-14T11:25:00";

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

            record.CertifiedTime = "2022-01-14";

            record.DateOfDeathPronouncement = "2022-01-14T11:35:00";

            if (fullRecord)
            {
                record.PronouncerGivenNames = new string[] { "Hope" };
                record.PronouncerFamilyName = "Lost";
            }

            record.COD1A = "Blunt head trauma";
            record.COD1B = "Automobile accident";
            record.ExaminerContactedBoolean = null; // use null to set to unknown
            if (fullRecord)
            {
                record.INTERVAL1A = "30 min";
                record.INTERVAL1B = "30 min";
                record.COD1C = "Epilepsy";
                record.INTERVAL1C = "20 years";
                record.ExaminerContactedBoolean = true;
                record.InjuryLocationDescription = "15 Industrial Drive 19901 US";
            }


            Dictionary<string, string> manner = new Dictionary<string, string>();
            manner.Add("code", "7878000");
            manner.Add("system", VRDR.CodeSystems.SCT);
            manner.Add("display", "Accidental death");
            record.MannerOfDeathType = manner;

            record.InjuryDate = "2022-01-14T11:15:00";
            record.InjuryAtWorkBoolean = true;

            Dictionary<string, string> injuryPlace = new Dictionary<string, string>();
            injuryPlace.Add("code", "4");
            injuryPlace.Add("system", VRDR.CodeSystems.PH_PlaceOfOccurrence_ICD_10_WHO);
            injuryPlace.Add("display", "street");
            record.InjuryPlace = injuryPlace;

            record.InjuryDescription = "unrestrained ejected driver in rollover motor vehicle accident";



            Dictionary<string, string> codeT = new Dictionary<string, string>();
            codeT.Add("code", "236320001");
            codeT.Add("system", VRDR.CodeSystems.SCT);
            codeT.Add("display", "Vehicle driver");
            record.TransportationRole = codeT;
            record.TransportationEventBoolean = true;
            record.AutopsyPerformedIndicatorBoolean = false;
            record.AutopsyResultsAvailableBoolean = false;
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

                Dictionary<string, string> morticianId = new Dictionary<string, string>();
                morticianId.Add("system", VRDR.CodeSystems.US_NPI_HL7);
                morticianId.Add("value", "313333AB");
                record.MorticianIdentifier = morticianId;

                record.MorticianGivenNames = new string[] { "Pedro", "A" };
                record.MorticianFamilyName = "Jimenez";

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
            // uncomment to generate file
            // string filename = "";
            // if (fullRecord)
            // {
            //     filename += "4_javier_perez_accident_full_" + fhirVersion + ".xml";
            // }
            // else
            // {
            //     filename += "5_javier_perez_accident_partial_" + fhirVersion + ".xml";
            // }
            // WriteRecordAsXml(record, filename);

            return record;
        }


        // Writes record to a file named filename in a subdirectory of the current working directory
        //  Note that you do this with docker, you will have to set a bind mount on the container
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
