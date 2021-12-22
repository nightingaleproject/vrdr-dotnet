namespace VRDR
{
    /// <summary> ValueSet Helpers </summary>
    public static class ValueSets
    {
        /// <summary> EducationLevel </summary>
        public static class EducationLevel {
            /// <summary> Codes </summary>
            public static string[,] Codes = {
                { "PHC1448", "8th grade or less", VRDR.CodeSystems.PH_PHINVS_CDC },
                { "PHC1449", "9th through 12th grade; no diploma", VRDR.CodeSystems.PH_PHINVS_CDC },
                { "PHC1450", "High School Graduate or GED Completed", VRDR.CodeSystems.PH_PHINVS_CDC },
                { "PHC1451", "Some college credit, but no degree", VRDR.CodeSystems.PH_PHINVS_CDC },
                { "PHC1452", "Associate Degree", VRDR.CodeSystems.PH_PHINVS_CDC },
                { "PHC1453", "Bachelor's Degree", VRDR.CodeSystems.PH_PHINVS_CDC },
                { "PHC1454", "Master's Degree", VRDR.CodeSystems.PH_PHINVS_CDC },
                { "PHC1455", "Doctorate Degree or Professional Degree", VRDR.CodeSystems.PH_PHINVS_CDC },
                { "UNK", "unknown", VRDR.CodeSystems.PH_NullFlavor_HL7_V3 }
            };
            /// <summary> _8th_Grade_Or_Less </summary>
            public static string  _8th_Grade_Or_Less = "PHC1448";
            /// <summary> _9th_Through_12th_Grade_No_Diploma </summary>
            public static string  _9th_Through_12th_Grade_No_Diploma = "PHC1449";
            /// <summary> High_School_Graduate_Or_Ged_Completed </summary>
            public static string  High_School_Graduate_Or_Ged_Completed = "PHC1450";
            /// <summary> Some_College_Credit_But_No_Degree </summary>
            public static string  Some_College_Credit_But_No_Degree = "PHC1451";
            /// <summary> Associate_Degree </summary>
            public static string  Associate_Degree = "PHC1452";
            /// <summary> Bachelors_Degree </summary>
            public static string  Bachelors_Degree = "PHC1453";
            /// <summary> Masters_Degree </summary>
            public static string  Masters_Degree = "PHC1454";
            /// <summary> Doctorate_Degree_Or_Professional_Degree </summary>
            public static string  Doctorate_Degree_Or_Professional_Degree = "PHC1455";
            /// <summary> Unknown </summary>
            public static string  Unknown = "UNK";
        };
        /// <summary> MannerOfDeath </summary>
        public static class MannerOfDeath {
            /// <summary> Codes </summary>
            public static string[,] Codes = {
                { "38605008", "Natural", VRDR.CodeSystems.SCT },
                { "7878000", "Accident", VRDR.CodeSystems.SCT },
                { "44301001", "Suicide", VRDR.CodeSystems.SCT },
                { "27935005", "Homicide", VRDR.CodeSystems.SCT },
                { "185973002", "Pending Investigation", VRDR.CodeSystems.SCT },
                { "65037004", "Could not be determined", VRDR.CodeSystems.SCT }
            };
            /// <summary> Natural </summary>
            public static string  Natural = "38605008";
            /// <summary> Accident </summary>
            public static string  Accident = "7878000";
            /// <summary> Suicide </summary>
            public static string  Suicide = "44301001";
            /// <summary> Homicide </summary>
            public static string  Homicide = "27935005";
            /// <summary> Pending_Investigation </summary>
            public static string  Pending_Investigation = "185973002";
            /// <summary> Could_Not_Be_Determined </summary>
            public static string  Could_Not_Be_Determined = "65037004";
        };
        /// <summary> MaritalStatus </summary>
        public static class MaritalStatus {
            /// <summary> Codes </summary>
            public static string[,] Codes = {
                { "M", "Married", VRDR.CodeSystems.PH_MaritalStatus_HL7_2x },
                { "A", "Married but Separated", VRDR.CodeSystems.PH_MaritalStatus_HL7_2x },
                { "W", "Widowed", VRDR.CodeSystems.PH_MaritalStatus_HL7_2x },
                { "D", "Divorced", VRDR.CodeSystems.PH_MaritalStatus_HL7_2x },
                { "S", "Never Married", VRDR.CodeSystems.PH_MaritalStatus_HL7_2x },
                { "U", "Not Classifiable", VRDR.CodeSystems.PH_MaritalStatus_HL7_2x }
            };
            /// <summary> Married </summary>
            public static string  Married = "M";
            /// <summary> Married_But_Separated </summary>
            public static string  Married_But_Separated = "A";
            /// <summary> Widowed </summary>
            public static string  Widowed = "W";
            /// <summary> Divorced </summary>
            public static string  Divorced = "D";
            /// <summary> Never_Married </summary>
            public static string  Never_Married = "S";
            /// <summary> Not_Classifiable </summary>
            public static string  Not_Classifiable = "U";
        };
        /// <summary> PlaceOfInjury </summary>
        public static class PlaceOfInjury {
            /// <summary> Codes </summary>
            public static string[,] Codes = {
                { "0", "Home", VRDR.CodeSystems.PH_PlaceOfOccurrence_ICD_10_WHO },
                { "1", "Residential Institution", VRDR.CodeSystems.PH_PlaceOfOccurrence_ICD_10_WHO },
                { "2", "School, Other Institutions, Public Administrative Area", VRDR.CodeSystems.PH_PlaceOfOccurrence_ICD_10_WHO },
                { "3", "Sports and Athletics Area", VRDR.CodeSystems.PH_PlaceOfOccurrence_ICD_10_WHO },
                { "4", "Street/Highway", VRDR.CodeSystems.PH_PlaceOfOccurrence_ICD_10_WHO },
                { "5", "Trade and Service Area", VRDR.CodeSystems.PH_PlaceOfOccurrence_ICD_10_WHO },
                { "6", "Industrial and Construction Area", VRDR.CodeSystems.PH_PlaceOfOccurrence_ICD_10_WHO },
                { "7", "Farm", VRDR.CodeSystems.PH_PlaceOfOccurrence_ICD_10_WHO },
                { "8", "Other Specified Place", VRDR.CodeSystems.PH_PlaceOfOccurrence_ICD_10_WHO },
                { "9", "Unspecified Place", VRDR.CodeSystems.PH_PlaceOfOccurrence_ICD_10_WHO },
                { "NI", "No Information", VRDR.CodeSystems.PH_NullFlavor_HL7_V3 }
            };
            /// <summary> Home </summary>
            public static string  Home = "0";
            /// <summary> Residential_Institution </summary>
            public static string  Residential_Institution = "1";
            /// <summary> School_Other_Institutions_Public_Administrative_Area </summary>
            public static string  School_Other_Institutions_Public_Administrative_Area = "2";
            /// <summary> Sports_And_Athletics_Area </summary>
            public static string  Sports_And_Athletics_Area = "3";
            /// <summary> Street_Highway </summary>
            public static string  Street_Highway = "4";
            /// <summary> Trade_And_Service_Area </summary>
            public static string  Trade_And_Service_Area = "5";
            /// <summary> Industrial_And_Construction_Area </summary>
            public static string  Industrial_And_Construction_Area = "6";
            /// <summary> Farm </summary>
            public static string  Farm = "7";
            /// <summary> Other_Specified_Place </summary>
            public static string  Other_Specified_Place = "8";
            /// <summary> Unspecified_Place </summary>
            public static string  Unspecified_Place = "9";
            /// <summary> No_Information </summary>
            public static string  No_Information = "NI";
        };
        /// <summary> PlaceOfDeath </summary>
        public static class PlaceOfDeath {
            /// <summary> Codes </summary>
            public static string[,] Codes = {
                { "63238001", "Hospital Dead on Arrival", VRDR.CodeSystems.SCT },
                { "440081000124100", "Decedent's Home", VRDR.CodeSystems.SCT },
                { "440071000124103", "Hospice", VRDR.CodeSystems.SCT },
                { "16983000", "Hospital Inpatient", VRDR.CodeSystems.SCT },
                { "450391000124102", "Death in emergency Room/Outpatient", VRDR.CodeSystems.SCT },
                { "450381000124100", "Death in nursing home/Long term care facility", VRDR.CodeSystems.SCT },
                { "OTH", "other", VRDR.CodeSystems.PH_NullFlavor_HL7_V3 },
                { "UNK", "unknown", VRDR.CodeSystems.PH_NullFlavor_HL7_V3 }
            };
            /// <summary> Hospital_Dead_On_Arrival </summary>
            public static string  Hospital_Dead_On_Arrival = "63238001";
            /// <summary> Decedents_Home </summary>
            public static string  Decedents_Home = "440081000124100";
            /// <summary> Hospice </summary>
            public static string  Hospice = "440071000124103";
            /// <summary> Hospital_Inpatient </summary>
            public static string  Hospital_Inpatient = "16983000";
            /// <summary> Death_In_Emergency_Room_Outpatient </summary>
            public static string  Death_In_Emergency_Room_Outpatient = "450391000124102";
            /// <summary> Death_In_Nursing_Home_Long_Term_Care_Facility </summary>
            public static string  Death_In_Nursing_Home_Long_Term_Care_Facility = "450381000124100";
            /// <summary> Other </summary>
            public static string  Other = "OTH";
            /// <summary> Unknown </summary>
            public static string  Unknown = "UNK";
        };
        /// <summary> TransportationRoles </summary>
        public static class TransportationRoles {
            /// <summary> Codes </summary>
            public static string[,] Codes = {
                { "236320001", "Driver/Operator", VRDR.CodeSystems.SCT },
                { "257500003", "Passenger", VRDR.CodeSystems.SCT },
                { "257518000", "Pedestrian", VRDR.CodeSystems.SCT },
                { "OTH", "Other", VRDR.CodeSystems.PH_NullFlavor_HL7_V3 }
            };
            /// <summary> Driver_Operator </summary>
            public static string  Driver_Operator = "236320001";
            /// <summary> Passenger </summary>
            public static string  Passenger = "257500003";
            /// <summary> Pedestrian </summary>
            public static string  Pedestrian = "257518000";
            /// <summary> Other </summary>
            public static string  Other = "OTH";
        };
        /// <summary> PregnancyStatus </summary>
        public static class PregnancyStatus {
            /// <summary> Codes </summary>
            public static string[,] Codes = {
                { "PHC1260", "Not pregnant within past year", VRDR.CodeSystems.PH_PHINVS_CDC },
                { "PHC1261", "Pregnant at time of death", VRDR.CodeSystems.PH_PHINVS_CDC },
                { "PHC1262", "Not pregnant, but pregnant within 42 days of death", VRDR.CodeSystems.PH_PHINVS_CDC },
                { "PHC1263", "Not pregnant, but pregnant 43 days to 1 year before death", VRDR.CodeSystems.PH_PHINVS_CDC },
                { "PHC1264", "Unknown if pregnant within the past year", VRDR.CodeSystems.PH_PHINVS_CDC },
                { "NA", "Not Applicable", VRDR.CodeSystems.PH_NullFlavor_HL7_V3 }
            };
            /// <summary> Not_Pregnant_Within_Past_Year </summary>
            public static string  Not_Pregnant_Within_Past_Year = "PHC1260";
            /// <summary> Pregnant_At_Time_Of_Death </summary>
            public static string  Pregnant_At_Time_Of_Death = "PHC1261";
            /// <summary> Not_Pregnant_But_Pregnant_Within_42_Days_Of_Death </summary>
            public static string  Not_Pregnant_But_Pregnant_Within_42_Days_Of_Death = "PHC1262";
            /// <summary> Not_Pregnant_But_Pregnant_43_Days_To_1_Year_Before_Death </summary>
            public static string  Not_Pregnant_But_Pregnant_43_Days_To_1_Year_Before_Death = "PHC1263";
            /// <summary> Unknown_If_Pregnant_Within_The_Past_Year </summary>
            public static string  Unknown_If_Pregnant_Within_The_Past_Year = "PHC1264";
            /// <summary> Not_Applicable </summary>
            public static string  Not_Applicable = "NA";
        };
        /// <summary> MethodsOfDisposition </summary>
        public static class MethodsOfDisposition {
            /// <summary> Codes </summary>
            public static string[,] Codes = {
                { "449971000124106", "Burial", VRDR.CodeSystems.SCT },
                { "449961000124104", "Cremation", VRDR.CodeSystems.SCT },
                { "449951000124101", "Donation", VRDR.CodeSystems.SCT },
                { "449931000124108", "Entombment", VRDR.CodeSystems.SCT },
                { "449941000124103", "Removal from State", VRDR.CodeSystems.SCT },
                { "OTH", "other", VRDR.CodeSystems.PH_NullFlavor_HL7_V3 },
                { "UNK", "unknown", VRDR.CodeSystems.PH_NullFlavor_HL7_V3 }
            };
            /// <summary> Burial </summary>
            public static string  Burial = "449971000124106";
            /// <summary> Cremation </summary>
            public static string  Cremation = "449961000124104";
            /// <summary> Donation </summary>
            public static string  Donation = "449951000124101";
            /// <summary> Entombment </summary>
            public static string  Entombment = "449931000124108";
            /// <summary> Removal_From_State </summary>
            public static string  Removal_From_State = "449941000124103";
            /// <summary> Other </summary>
            public static string  Other = "OTH";
            /// <summary> Unknown </summary>
            public static string  Unknown = "UNK";
        };
        /// <summary> CertificationRole </summary>
        public static class CertificationRole {
            /// <summary> Codes </summary>
            public static string[,] Codes = {
                { "455381000124109", "Medical Examiner/Coroner", VRDR.CodeSystems.SCT },
                { "434641000124105", "Physician certified and pronounced death certificate", VRDR.CodeSystems.SCT },
                { "434651000124107", "Physician certified death certificate", VRDR.CodeSystems.SCT },
                { "OTH", "Other", VRDR.CodeSystems.PH_NullFlavor_HL7_V3 }
            };
            /// <summary> Medical Examiner/Coroner </summary>
            public static string  Medical_Examiner_Coroner = "455381000124109";
            /// <summary> Physician certified and pronounced death certificate </summary>
            public static string  Physician_Certified_And_Pronounced_Death_Certificate = "434641000124105";
            /// <summary> Physician certified death certificate </summary>
            public static string  Physician_Certified_Death_Certificate = "434651000124107";
            /// <summary> Other </summary>
            public static string  Other = "OTH";
        };
    }
}




