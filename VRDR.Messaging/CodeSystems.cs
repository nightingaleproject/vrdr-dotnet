namespace VRDR
{
    /// <summary>Single definitions for URIs used throughout. </summary>
    public static class URIs
    {
        /// <summary>Submission URI</summary>
        public static string Submission = "http://nchs.cdc.gov/vrdr_submission";
        /// <summary>Submission Update</summary>
        public static string SubmissionUpdate = "http://nchs.cdc.gov/vrdr_submission_update";

        /// <summary>Demographics Coding</summary>
        public static string DemographicsCoding = "http://nchs.cdc.gov/vrdr_demographics_coding";
        /// <summary>Demographics Coding Update</summary>
        public static string DemographicsCodingUpdate = "http://nchs.cdc.gov/vrdr_demographics_coding_update";

        /// <summary>Cause of Death Coding  URI</summary>
        public static string CauseOfDeathCoding = "http://nchs.cdc.gov/vrdr_causeofdeath_coding";
            /// <summary>Cause of Death Coding Update URI</summary>
        public static string CauseOfDeathCodingUpdate = "http://nchs.cdc.gov/vrdr_causeofdeath_coding_update";

        /// <summary>Acknowledgement</summary>
        public static string Acknowledgement = "http://nchs.cdc.gov/vrdr_acknowledgement";
        /// <summary>Extraction Error</summary>
        public static string ExtractionError = "http://nchs.cdc.gov/vrdr_extraction_error";

        /// <summary>Submission Message URI</summary>
        public static string Alias = "http://nchs.cdc.gov/vrdr_submission_alias";

        /// <summary>Submission Message URI</summary>
        public static string Void = "http://nchs.cdc.gov/vrdr_submission_void";
    }
    /// <summary>Single definitions for CodeSystem OIDs and URLs used throughout. </summary>
    public static class CodeSystems
        /// <summary>SNOMEDCT.</summary>
        public static string SCT = "http://snomed.info/sct";

        /// <summary>LOINC.</summary>
        public static string LOINC = "http://loinc.org";

        /// <summary>Social Security Numbers.</summary>
        public static string US_SSN = "http://hl7.org/fhir/sid/us-ssn";

        /// <summary>PHINVADS Race and Ethnicity.</summary>
        public static string PH_RaceAndEthnicity_CDC = "urn:oid:2.16.840.1.113883.6.238";

        /// <summary>PHINVADS Null Flavor.</summary>
        public static string PH_NullFlavor_HL7_V3 = "http://terminology.hl7.org/CodeSystem/v3-NullFlavor"; // "urn:oid:2.16.840.1.113883.5.1008";

        /// <summary>HL7 V3 Null Flavor.</summary>
        public static string NullFlavor_HL7_V3 = "http://terminology.hl7.org/CodeSystem/v3-NullFlavor";

        /// <summary>HL7 Data Absent reason.</summary>
        public static string Data_Absent_Reason_HL7_V3 = "http://terminology.hl7.org/CodeSystem/data-absent-reason";

        /// <summary>PHINVADS Local Coding System.</summary>
        public static string PH_PHINVS_CDC = "urn:oid:2.16.840.1.114222.4.5.274";

        /// <summary>PHINVADS Yes/No.</summary>
        public static string PH_YesNo_HL7_2x = "urn:oid:2.16.840.1.113883.12.136";

        /// <summary>PHINVADS County FIPS 6-4</summary>
        public static string PH_County_FIPS_6_4 = "2.16.840.1.113883.6.93";

        /// <summary>PHINVADS Country GEC</summary>
        public static string PH_Country_GEC = "2.16.840.1.113883.13.250";
        /// <summary>PHINVADS Place of Occurrence.</summary>
        public static string PH_PlaceOfOccurrence_ICD_10_WHO = "urn:oid:2.16.840.1.114222.4.5.320";

        /// <summary>PHINVADS SNOMED.</summary>
        public static string PH_SNOMED_CT = "urn:oid:2.16.840.1.113883.6.96";
        /// <summary>PHINVADS Marital Satus.</summary>
        public static string PH_MaritalStatus_HL7_2x = "http://terminology.hl7.org/CodeSystem/v3-MaritalStatus";
        /// <summary>PHINVADS USGS GNIS.</summary>
        public static string PH_USGS_GNIS = "urn:oid:2.16.840.1.113883.6.245";
        /// <summary>PHINVADS FIPS 5 2.</summary>
        public static string PH_State_FIPS_5_2 = "urn:oid:2.16.840.1.113883.6.92";
        /// <summary>HL7 Location Physical Type.</summary>
        public static string HL7_location_physical_type = "http://terminology.hl7.org/CodeSystem/location-physical-type";
        /// <summary> CDC Census Occupation Code  </summary>
        public static string PH_Occupation_CDC_Census2010 = "urn:oid:2.16.840.1.114222.4.5.314";
        /// <summary> CDC Census Industry Code  </summary>
        public static string PH_Industry_CDC_Census2010 = "urn:oid:2.16.840.1.114222.4.5.315";
        /// <summary> US NPI HL7  </summary>
        public static string US_NPI_HL7 = "http://hl7.org/fhir/sid/us-npi";
        /// <summary>HL7 Identifier Type.</summary>
        public static string HL7_identifier_type = "http://terminology.hl7.org/CodeSystem/v2-0203";
       /// <summary>HL7 Organization Type.</summary>
        public static string HL7_organization_type =  "http://terminology.hl7.org/CodeSystem/organization-type";


        /// <summary>PHINVADS HL7 RoleCode.</summary>
        public static string PH_RoleCode_HL7_V3 = "urn:oid:2.16.840.1.113883.5.111";

        /// <summary>HL7 RoleCode.</summary>
        public static string RoleCode_HL7_V3 = "http://terminology.hl7.org/CodeSystem/v3-RoleCode";

        /// <summary> ISO 3166-2  </summary>
        public static string ISO_3166_2 = "urn:iso:std:iso:3166:-2";

        /// <summary> Hispanic Origin CS (from messaging IG)  </summary>
        public static string RaceCodes = "http://cdc.gov/nchs/nvss/fhir/vital-records-messaging/CodeSystem/VRDR-RaceCodeList-cs";

        /// <summary> Race Code CS (from Messaging IG)  </summary>
        public static string HispanicOriginCodes = "http://cdc.gov/nchs/nvss/fhir/vital-records-messaging/CodeSystem/VRDR-HispanicOrigin-cs";


    }

}
