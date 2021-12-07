namespace VRDR
{
     /// <summary> ValueSet Helpers </summary>
     public static class ValueSets
    {
        /// <summary> PHVS_PlaceOfDeath_NCHS </summary>
        public static class PHVS_PlaceOfDeath_NCHS {
            /// <summary> Codes </summary>
            public static string[,] Codes = {
                {"63238001", "Hospital Dead on Arrival", VRDR.CodeSystems.SCT},
                {"440081000124100", "Decedent's Home", VRDR.CodeSystems.SCT},
                {"440071000124103", "Hospice", VRDR.CodeSystems.SCT},
                {"16983000", "Hospital Inpatient", VRDR.CodeSystems.SCT},
                {"450391000124102", "Death in emergency Room/Outpatient", VRDR.CodeSystems.SCT},
                {"450381000124100", "Death in nursing home/Long term care facility", VRDR.CodeSystems.SCT},
                {"OTH", "other", VRDR.CodeSystems.PH_NullFlavor_HL7_V3},
                {"UNK", "unknown", VRDR.CodeSystems.PH_NullFlavor_HL7_V3}
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
        }
    }
}