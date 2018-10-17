using System;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

namespace FhirDeathRecord
{
    /// <summary>Property attribute used to describe a field in the IJE Mortality format.</summary>
    [System.AttributeUsage(System.AttributeTargets.Property)]
    public class IJEField : System.Attribute
    {
        /// <summary>Field number.</summary>
        public int Field;

        /// <summary>Beggining location.</summary>
        public int Location;

        /// <summary>Field length.</summary>
        public int Length;

        /// <summary>Description of what the field contains.</summary>
        public string Contents;

        /// <summary>Field name.</summary>
        public string Name;

        /// <summary>Priority - lower will be "GET" and "SET" earlier.</summary>
        public int Priority;

        public IJEField(int field, int location, int length, string contents, string name, int priority)
        {
            this.Field = field;
            this.Location = location;
            this.Length = length;
            this.Contents = contents;
            this.Name = name;
            this.Priority = priority;
        }
    }

    /// <summary>A "wrapper" class to convert between a FHIR based <c>DeathRecord</c> and
    /// a record in IJE Mortality format. Each property of this class corresponds exactly
    /// with a field in the IJE Mortality format. The getters convert from the embedded
    /// FHIR based <c>DeathRecord</c> to the IJE format for a specific field, and
    /// the setters convert from IJE format for a specific field and set that value
    /// on the embedded FHIR based <c>DeathRecord<c>.</summary>
    public class IJEMortality
    {
        /// <summary>FHIR based death record.</summary>
        private DeathRecord record;

        /// <summary>IJE data lookup helper. Thread-safe singleton!</summary>
        private IJEMortalityData dataLookup = IJEMortalityData.Instance;

        /// <summary>Constructor that takes a <c>DeathRecord</c>.</summary>
        public IJEMortality(DeathRecord record)
        {
            this.record = record;
        }

        /// <summary>Constructor that takes an IJE string and builds a corresponding internal <c>DeathRecord</c>.</summary>
        public IJEMortality(string ije)
        {
            this.record = new DeathRecord();
            // Loop over every property (these are the fields); Order by priority
            List<PropertyInfo> properties = typeof(IJEMortality).GetProperties().ToList().OrderBy(p => ((IJEField)p.GetCustomAttributes().First()).Priority).ToList();
            foreach(PropertyInfo property in properties)
            {
                // Grab the field attributes
                IJEField info = (IJEField)property.GetCustomAttributes().First();
                // Grab the field value
                string field = ije.Substring(info.Location - 1, info.Length);
                // Set the value on this IJEMortality (and the embedded record)
                property.SetValue(this, field);
            }
        }

        /// <summary>Converts the internal <c>DeathRecord</c> into an IJE string.</summary>
        public override string ToString()
        {
            // Start with empty IJE Mortality record
            StringBuilder ije = new StringBuilder(new String(' ', 5000), 5000);
            // Loop over every property (these are the fields)
            foreach(PropertyInfo property in typeof(IJEMortality).GetProperties())
            {
                // Grab the field value
                string field = Convert.ToString(property.GetValue(this, null));
                // Grab the field attributes
                IJEField info = (IJEField)property.GetCustomAttributes().First();
                // Be mindful about lengths
                if (field.Length > info.Length)
                {
                    field = field.Substring(0, info.Length);
                }
                // Insert the field value into the record
                ije.Insert(info.Location - 1, field);
            }
            return ije.ToString();
        }

        /// <summary>Returns the corresponding <c>DeathRecord</c> for this IJE string.</summary>
        public DeathRecord ToDeathRecord()
        {
            return this.record;
        }


        /////////////////////////////////////////////////////////////////////////////////
        //
        // Class helper methods for getting and settings IJE fields.
        //
        /////////////////////////////////////////////////////////////////////////////////

        // <summary>Truncates the given string to the given length.</summary>
        private static string Truncate(string value, int length)
        {
            if (string.IsNullOrEmpty(value) || value.Length <= length)
            {
                return value;
            }
            else
            {
                return value.Substring(0, length);
            }
        }

        // <summary>Grabs the IJEInfo for a specific IJE field name.</summary>
        private static IJEField FieldInfo(string ijeFieldName)
        {
            return (IJEField)typeof(IJEMortality).GetProperty(ijeFieldName).GetCustomAttributes().First();
        }

        // <summary>Helps decompose a DateTime into individual parts (year, month, day, time).</summary>
        private string DateTimeStringHelper(IJEField info, string value, string type, DateTime date)
        {
            if (type == "yyyy")
            {
                return date.ToString($"{Truncate(value, info.Length)}-MM-dd HH:mm");
            }
            else if (type == "MM")
            {
                return date.ToString($"yyyy-{Truncate(value, info.Length)}-dd HH:mm");
            }
            else if (type == "dd")
            {
                return date.ToString($"yyyy-MM-{Truncate(value, info.Length)} HH:mm");
            }
            else if (type == "HHmm")
            {
                return date.ToString($"yyyy-MM-dd {Truncate(value, info.Length).Substring(0, 2)}:{Truncate(value, info.Length).Substring(2, 2)}");
            }
            return "";
        }

        // <summary>Get a value on the DeathRecord whose type is some part of a DateTime.</summary>
        private string DateTime_Get(string ijeFieldName, string dateTimeType, string fhirFieldName)
        {
            IJEField info = FieldInfo(ijeFieldName);
            DateTime date;
            string current = Convert.ToString(typeof(DeathRecord).GetProperty(fhirFieldName).GetValue(this.record));
            if (DateTime.TryParse(current, out date))
            {
                return Truncate(date.ToString(dateTimeType), info.Length);
            }
            else
            {
                return new String(' ', info.Length);
            }
        }

        // <summary>Set a value on the DeathRecord whose type is some part of a DateTime.</summary>
        private void DateTime_Set(string ijeFieldName, string dateTimeType, string fhirFieldName, string value)
        {
            IJEField info = FieldInfo(ijeFieldName);
            string current = Convert.ToString(typeof(DeathRecord).GetProperty(fhirFieldName).GetValue(this.record));
            DateTime date;
            if (current != null && DateTime.TryParse(current, out date))
            {
                typeof(DeathRecord).GetProperty(fhirFieldName).SetValue(this.record, DateTimeStringHelper(info, value, dateTimeType, date));
            }
            else
            {
                typeof(DeathRecord).GetProperty(fhirFieldName).SetValue(this.record, DateTimeStringHelper(info, value, dateTimeType, new DateTime()));
            }
        }

        // <summary>Get a value on the DeathRecord whose IJE type is a left justified string.</summary>
        private string RightJustifiedZeroed_Get(string ijeFieldName, string fhirFieldName)
        {
            IJEField info = FieldInfo(ijeFieldName);
            string current = Convert.ToString(typeof(DeathRecord).GetProperty(fhirFieldName).GetValue(this.record));
            if (current != null)
            {
                return Truncate(current, info.Length).PadLeft(info.Length, '0');
            }
            else
            {
                return new String('0', info.Length);
            }
        }

        // <summary>Set a value on the DeathRecord whose IJE type is a right justified, zeroed filled string.</summary>
        private void RightJustifiedZeroed_Set(string ijeFieldName, string fhirFieldName, string value)
        {
            IJEField info = FieldInfo(ijeFieldName);
            typeof(DeathRecord).GetProperty(fhirFieldName).SetValue(this.record, value.TrimStart('0'));
        }

        // <summary>Get a value on the DeathRecord whose IJE type is a left justified string.</summary>
        private string LeftJustified_Get(string ijeFieldName, string fhirFieldName)
        {
            IJEField info = FieldInfo(ijeFieldName);
            string current = Convert.ToString(typeof(DeathRecord).GetProperty(fhirFieldName).GetValue(this.record));
            if (current != null)
            {
                return Truncate(current, info.Length).PadRight(info.Length, ' ');
            }
            else
            {
                return new String(' ', info.Length);
            }
        }

        // <summary>Set a value on the DeathRecord whose IJE type is a left justified string.</summary>
        private void LeftJustified_Set(string ijeFieldName, string fhirFieldName, string value)
        {
            IJEField info = FieldInfo(ijeFieldName);
            typeof(DeathRecord).GetProperty(fhirFieldName).SetValue(this.record, value.Trim());
        }

        // <summary>Get a value on the DeathRecord whose property is a Dictionary type.</summary>
        private string Dictionary_Get(string ijeFieldName, string fhirFieldName, string key)
        {
            IJEField info = FieldInfo(ijeFieldName);
            Dictionary<string, string> dictionary = (Dictionary<string, string>)typeof(DeathRecord).GetProperty(fhirFieldName).GetValue(this.record);
            string current = Convert.ToString(dictionary[key]);
            if (current != null)
            {
                return Truncate(current, info.Length).PadRight(info.Length, ' ');
            }
            else
            {
                return new String(' ', info.Length);
            }
        }

        // <summary>Get a value on the DeathRecord whose property is a Dictionary type, with NO truncating.</summary>
        private string Dictionary_Get_Full(string ijeFieldName, string fhirFieldName, string key)
        {
            IJEField info = FieldInfo(ijeFieldName);
            Dictionary<string, string> dictionary = (Dictionary<string, string>)typeof(DeathRecord).GetProperty(fhirFieldName).GetValue(this.record);
            string current = Convert.ToString(dictionary[key]);
            if (current != null)
            {
                return current;
            }
            else
            {
                return "";
            }
        }

        // <summary>Set a value on the DeathRecord whose property is a Dictionary type.</summary>
        private void Dictionary_Set(string ijeFieldName, string fhirFieldName, string key, string value)
        {
            IJEField info = FieldInfo(ijeFieldName);
            Dictionary<string, string> dictionary = (Dictionary<string, string>)typeof(DeathRecord).GetProperty(fhirFieldName).GetValue(this.record);
            if (dictionary != null && (!dictionary.ContainsKey(key) || string.IsNullOrEmpty(dictionary[key])))
            {
                dictionary[key] = value;
            }
            else
            {
                dictionary = new Dictionary<string, string>();
                dictionary[key] = value;
            }
            typeof(DeathRecord).GetProperty(fhirFieldName).SetValue(this.record, dictionary);
        }

        // <summary>Get a value on the DeathRecord whose property is a geographic type (and is contained in a dictionary).</summary>
        private string Dictionary_Geo_Get(string ijeFieldName, string fhirFieldName, string keyPrefix, string geoType, bool isCoded)
        {
            IJEField info = FieldInfo(ijeFieldName);
            Dictionary<string, string> dictionary = (Dictionary<string, string>)typeof(DeathRecord).GetProperty(fhirFieldName).GetValue(this.record);
            string key = keyPrefix + char.ToUpper(geoType[0]) + geoType.Substring(1);
            string current = Convert.ToString(dictionary[key]);
            if (isCoded)
            {
                if (geoType == "place"|| geoType == "city")
                {
                    string state = null;
                    string county = null;
                    dictionary.TryGetValue(keyPrefix + "State", out state);
                    dictionary.TryGetValue(keyPrefix + "County", out county);
                    if (state != null && county != null)
                    {
                        current = dataLookup.StateNameAndCountyNameAndPlaceNameToPlaceCode(state, county, current);
                    }
                }
                else if (geoType == "county")
                {
                    string state = null;
                    dictionary.TryGetValue(keyPrefix + "State", out state);
                    if (state != null)
                    {
                        current = dataLookup.StateNameAndCountyNameToCountyCode(state, current);
                    }
                }
                else if (geoType == "state")
                {
                    current = dataLookup.StateNameToStateCode(current);
                }
                else if (geoType == "country")
                {
                    current = dataLookup.CountryNameToCountryCode(current);
                }
                else if (geoType == "insideCityLimits")
                {
                    if (string.IsNullOrEmpty(current))
                    {
                        current = "U";
                    }
                    else if (current == "true" || current == "True")
                    {
                        current = "Y";
                    }
                    else if (current == "false" || current == "False")
                    {
                        current = "N";
                    }
                }
            }
            if (current != null)
            {
                return Truncate(current, info.Length).PadRight(info.Length, ' ');
            }
            else
            {
                return new String(' ', info.Length);
            }
        }

        // <summary>Set a value on the DeathRecord whose property is a geographic type (and is contained in a dictionary).</summary>
        private void Dictionary_Geo_Set(string ijeFieldName, string fhirFieldName, string keyPrefix, string geoType, bool isCoded, string value)
        {
            IJEField info = FieldInfo(ijeFieldName);
            Dictionary<string, string> dictionary = (Dictionary<string, string>)typeof(DeathRecord).GetProperty(fhirFieldName).GetValue(this.record);
            string key = keyPrefix + char.ToUpper(geoType[0]) + geoType.Substring(1);
            if (dictionary != null && (!dictionary.ContainsKey(key) || string.IsNullOrEmpty(dictionary[key])))
            {
                if (isCoded)
                {
                    if (geoType == "place" || geoType == "city") // This is a tricky case, we need to know about county and state!
                    {
                        string state = null;
                        string county = null;
                        dictionary.TryGetValue(keyPrefix + "State", out state);
                        dictionary.TryGetValue(keyPrefix + "County", out county);
                        if (state != null && county != null)
                        {
                            dictionary[key] = dataLookup.StateNameAndCountyNameAndPlaceCodeToPlaceName(state, county, value);
                        }
                    }
                    else if (geoType == "county") // This is a tricky case, we need to know about state!
                    {
                        string state = null;
                        dictionary.TryGetValue(keyPrefix + "State", out state);
                        if (state != null)
                        {
                            dictionary[key] = dataLookup.StateNameAndCountyCodeToCountyName(state, value);
                        }
                    }
                    else if (geoType == "state")
                    {
                        dictionary[key] = dataLookup.StateCodeToStateName(value);
                    }
                    else if (geoType == "country")
                    {
                        dictionary[key] = dataLookup.CountryCodeToCountryName(value);
                    }
                    else if (geoType == "insideCityLimits")
                    {
                        if (value != null && value == "Y")
                        {
                            dictionary[key] = "True";
                        }
                        else if (value != null && value == "N")
                        {
                            dictionary[key] = "False";
                        }
                    }
                }
                else
                {
                    dictionary[key] = value;
                }
            }
            else
            {
                dictionary = new Dictionary<string, string>();
                dictionary[key] = value;
            }
            typeof(DeathRecord).GetProperty(fhirFieldName).SetValue(this.record, dictionary);
        }


        /////////////////////////////////////////////////////////////////////////////////
        //
        // Class Properties that provide getters and setters for each of the IJE
        // Mortality fields.
        //
        // Getters look at the embedded DeathRecord and convert values to IJE style.
        // Setters convert and store IJE style values to the embedded DeathRecord.
        //
        /////////////////////////////////////////////////////////////////////////////////

        // <summary>Date of Death--Year</summary>
        [IJEField(1, 1, 4, "Date of Death--Year", "DOD_YR", 1)]
        public string DOD_YR
        {
            get
            {
                return DateTime_Get("DOD_YR", "yyyy", "DateOfDeath") ?? DateTime_Get("DOD_YR", "yyyy", "ActualOrPresumedDateOfDeath");
            }
            set
            {
                DateTime_Set("DOD_YR", "yyyy", "DateOfDeath", value);
                DateTime_Set("DOD_YR", "yyyy", "ActualOrPresumedDateOfDeath", value);
            }
        }

        // <summary>State, U.S. Territory or Canadian Province of Death - code</summary>
        [IJEField(2, 5, 2, "State, U.S. Territory or Canadian Province of Death - code", "DSTATE", 1)]
        public string DSTATE
        {
            get
            {
                return Dictionary_Geo_Get("DSTATE", "PlaceOfDeath", "placeOfDeath", "state", true);
            }
            set
            {
                Dictionary_Geo_Set("DSTATE", "PlaceOfDeath", "placeOfDeath", "state", true, value);
            }
        }

        // <summary>Certificate Number</summary>
        [IJEField(3, 7, 6, "Certificate Number", "FILENO", 1)]
        public string FILENO
        {
            get
            {
                return RightJustifiedZeroed_Get("FILENO", "Id");
            }
            set
            {
                RightJustifiedZeroed_Set("FILENO", "Id", value);
            }
        }

        // <summary>Decedent's Legal Name--Given</summary>
        [IJEField(7, 27, 50, "Decedent's Legal Name--Given", "GNAME", 1)]
        public string GNAME
        {
            get
            {
                return LeftJustified_Get("GNAME", "FirstName");
            }
            set
            {
                LeftJustified_Set("GNAME", "FirstName", value);
            }
        }

        // <summary>Decedent's Legal Name--Middle</summary>
        [IJEField(8, 77, 1, "Decedent's Legal Name--Middle", "MNAME", 1)]
        public string MNAME
        {
            get
            {
                return LeftJustified_Get("MNAME", "MiddleName");
            }
            set
            {
                LeftJustified_Set("MNAME", "MiddleName", value);
            }
        }

        // <summary>Decedent's Legal Name--Last</summary>
        [IJEField(9, 78, 50, "Decedent's Legal Name--Last", "LNAME", 1)]
        public string LNAME
        {
            get
            {
                return LeftJustified_Get("LNAME", "FamilyName");
            }
            set
            {
                LeftJustified_Set("LNAME", "FamilyName", value);
            }
        }

        // <summary>Decedent's Legal Name--Suffix</summary>
        [IJEField(10, 128, 10, "Decedent's Legal Name--Suffix", "SUFF", 1)]
        public string SUFF
        {
            get
            {
                return LeftJustified_Get("SUFF", "Suffix");
            }
            set
            {
                LeftJustified_Set("SUFF", "Suffix", value);
            }
        }

        // <summary>Sex</summary>
        [IJEField(13, 189, 1, "Sex", "SEX", 1)]
        public string SEX
        {
            get
            {
                return Dictionary_Get("SEX", "BirthSex", "code");
            }
            set
            {
                string code = value == "U" ? "UNK" : value;
                string display = "";
                if (code == "M")
                {
                    display = "Male";
                }
                else if (code == "F")
                {
                    display = "Female";
                }
                else if (code == "UNK")
                {
                    display = "Unknown";
                }
                Dictionary_Set("SEX", "BirthSex", "code", code);
                Dictionary_Set("SEX", "BirthSex", "display", display);
                Dictionary_Set("SEX", "BirthSex", "system", "http://hl7.org/fhir/us/core/ValueSet/us-core-birthsex");
            }
        }

        // <summary>Social Security Number</summary>
        [IJEField(15, 191, 9, "Social Security Number", "SSN", 1)]
        public string SSN
        {
            get
            {
                return LeftJustified_Get("SSN", "SSN");
            }
            set
            {
                LeftJustified_Set("SSN", "SSN", value);
            }
        }

        // <summary>Decedent's Age--Type</summary>
        [IJEField(16, 200, 1, "Decedent's Age--Type", "AGETYPE", 1)]
        public string AGETYPE
        {
            get
            {
                return "1"; // Always years.
            }
            set
            {
                // NOOP; this should always be years in this implementation.
            }
        }

        // <summary>Decedent's Age--Units</summary>
        [IJEField(17, 201, 3, "Decedent's Age--Units", "AGE", 1)]
        public string AGE
        {
            get
            {
                return RightJustifiedZeroed_Get("AGE", "Age");
            }
            set
            {
                RightJustifiedZeroed_Set("AGE", "Age", value);
            }
        }

        // <summary>Date of Birth--Year</summary>
        [IJEField(19, 205, 4, "Date of Birth--Year", "DOB_YR", 1)]
        public string DOB_YR
        {
            get
            {
                return DateTime_Get("DOB_YR", "yyyy", "DateOfBirth");
            }
            set
            {
                DateTime_Set("DOB_YR", "yyyy", "DateOfBirth", value);
            }
        }

        // <summary>Date of Birth--Month</summary>
        [IJEField(20, 209, 2, "Date of Birth--Month", "DOB_MO", 1)]
        public string DOB_MO
        {
            get
            {
                return DateTime_Get("DOB_YR", "MM", "DateOfBirth");
            }
            set
            {
                DateTime_Set("DOB_YR", "MM", "DateOfBirth", value);
            }
        }

        // <summary>Date of Birth--Day</summary>
        [IJEField(21, 211, 2, "Date of Birth--Day", "DOB_DY", 1)]
        public string DOB_DY
        {
            get
            {
                return DateTime_Get("DOB_YR", "dd", "DateOfBirth");
            }
            set
            {
                DateTime_Set("DOB_YR", "dd", "DateOfBirth", value);
            }
        }

        // <summary>Birthplace--Country</summary>
        [IJEField(22, 213, 2, "Birthplace--Country", "BPLACE_CNT", 1)]
        public string BPLACE_CNT
        {
            get
            {
                return Dictionary_Geo_Get("BPLACE_CNT", "PlaceOfBirth", "placeOfBirth", "country", true);
            }
            set
            {
                Dictionary_Geo_Set("BPLACE_CNT", "PlaceOfBirth", "placeOfBirth", "country", true, value);
            }
        }

        // <summary>State, U.S. Territory or Canadian Province of Birth - code</summary>
        [IJEField(23, 215, 2, "State, U.S. Territory or Canadian Province of Birth - code", "BPLACE_ST", 1)]
        public string BPLACE_ST
        {
            get
            {
                return Dictionary_Geo_Get("BPLACE_ST", "PlaceOfBirth", "placeOfBirth", "state", true);
            }
            set
            {
                Dictionary_Geo_Set("BPLACE_ST", "PlaceOfBirth", "placeOfBirth", "state", true, value);
            }
        }

        // <summary>Decedent's Residence--City</summary>
        [IJEField(24, 217, 5, "Decedent's Residence--City", "CITYC", 3)]
        public string CITYC
        {
            get
            {
                return Dictionary_Geo_Get("CITYC", "Residence", "residence", "city", true);
            }
            set
            {
                Dictionary_Geo_Set("CITYC", "Residence", "residence", "city", true, value);
            }
        }

        // <summary>Decedent's Residence--County</summary>
        [IJEField(25, 222, 3, "Decedent's Residence--County", "COUNTYC", 2)]
        public string COUNTYC
        {
            get
            {
                return Dictionary_Geo_Get("COUNTYC", "Residence", "residence", "county", true);
            }
            set
            {
                Dictionary_Geo_Set("COUNTYC", "Residence", "residence", "county", true, value);
            }
        }

        // <summary>State, U.S. Territory or Canadian Province of Decedent's residence - code</summary>
        [IJEField(26, 225, 2, "State, U.S. Territory or Canadian Province of Decedent's residence - code", "STATEC", 1)]
        public string STATEC
        {
            get
            {
                return Dictionary_Geo_Get("STATEC", "Residence", "residence", "state", true);
            }
            set
            {
                Dictionary_Geo_Set("STATEC", "Residence", "residence", "state", true, value);
            }
        }

        // <summary>Decedent's Residence--Country</summary>
        [IJEField(27, 227, 2, "Decedent's Residence--Country", "COUNTRYC", 1)]
        public string COUNTRYC
        {
            get
            {
                return Dictionary_Geo_Get("COUNTRYC", "Residence", "residence", "country", true);
            }
            set
            {
                Dictionary_Geo_Set("COUNTRYC", "Residence", "residence", "country", true, value);
            }
        }

        // <summary>Decedent's Residence--Inside City Limits</summary>
        [IJEField(28, 229, 1, "Decedent's Residence--Inside City Limits", "LIMITS", 1)]
        public string LIMITS
        {
            get
            {
                return Dictionary_Geo_Get("LIMITS", "Residence", "residence", "insideCityLimits", true);
            }
            set
            {
                Dictionary_Geo_Set("LIMITS", "Residence", "residence", "insideCityLimits", true, value);
            }
        }

        // <summary>Marital Status</summary>
        [IJEField(29, 230, 1, "Marital Status", "MARITAL", 1)]
        public string MARITAL
        {
            get
            {
                string code = Dictionary_Get("MARITAL", "MaritalStatus", "code");
                switch (code)
                {
                    case "M":
                    case "A":
                    case "W":
                    case "D":
                    case "S":
                        return code;
                    case "I":
                    case "L":
                    case "P":
                    case "T":
                    case "U":
                    case "UNK":
                        return "U";
                }
                return "";
            }
            set
            {
                switch (value)
                {
                    case "M":
                        Dictionary_Set("MARITAL", "MaritalStatus", "code", value);
                        Dictionary_Set("MARITAL", "MaritalStatus", "system", "http://hl7.org/fhir/v3/MaritalStatus");
                        Dictionary_Set("MARITAL", "MaritalStatus", "display", "Married");
                        break;
                    case "A":
                        Dictionary_Set("MARITAL", "MaritalStatus", "code", value);
                        Dictionary_Set("MARITAL", "MaritalStatus", "system", "http://hl7.org/fhir/v3/MaritalStatus");
                        Dictionary_Set("MARITAL", "MaritalStatus", "display", "Annulled");
                        break;
                    case "W":
                        Dictionary_Set("MARITAL", "MaritalStatus", "code", value);
                        Dictionary_Set("MARITAL", "MaritalStatus", "system", "http://hl7.org/fhir/v3/MaritalStatus");
                        Dictionary_Set("MARITAL", "MaritalStatus", "display", "Widowed");
                        break;
                    case "D":
                        Dictionary_Set("MARITAL", "MaritalStatus", "code", value);
                        Dictionary_Set("MARITAL", "MaritalStatus", "system", "http://hl7.org/fhir/v3/MaritalStatus");
                        Dictionary_Set("MARITAL", "MaritalStatus", "display", "Divorced");
                        break;
                    case "S":
                        Dictionary_Set("MARITAL", "MaritalStatus", "code", value);
                        Dictionary_Set("MARITAL", "MaritalStatus", "system", "http://hl7.org/fhir/v3/MaritalStatus");
                        Dictionary_Set("MARITAL", "MaritalStatus", "display", "Never Married");
                        break;
                    case "U":
                        Dictionary_Set("MARITAL", "MaritalStatus", "code", "UNK");
                        Dictionary_Set("MARITAL", "MaritalStatus", "system", "http://hl7.org/fhir/v3/NullFlavor");
                        Dictionary_Set("MARITAL", "MaritalStatus", "display", "unknown");
                        break;
                }
            }
        }

        // <summary>Place of Death</summary>
        [IJEField(31, 232, 1, "Place of Death", "DPLACE", 1)]
        public string DPLACE
        {
            get
            {
                string code = Dictionary_Get_Full("DPLACE", "PlaceOfDeath", "placeOfDeathTypeCode");
                switch (code)
                {
                    case "63238001": // Dead on arrival at hospital
                        return "3";
                    case "440081000124100": // Death in home
                        return "4";
                    case "440071000124103": // Death in hospice
                        return "5";
                    case "16983000": // Death in hospital
                        return "1";
                    case "450391000124102": // Death in hospital-based emergency department or outpatient department
                        return "2";
                    case "450381000124100": // Death in nursing home or long term care facility
                        return "6";
                    case "UNK": // Unknown
                        return "9";
                    case "OTH": // Other
                        return "7";
                }
                return "";
            }
            set
            {
                switch (value)
                {
                    case "1":
                        Dictionary_Set("DPLACE", "PlaceOfDeath", "placeOfDeathTypeCode", "16983000");
                        Dictionary_Set("DPLACE", "PlaceOfDeath", "placeOfDeathTypeSystem", "http://snomed.info/sct");
                        Dictionary_Set("DPLACE", "PlaceOfDeath", "placeOfDeathTypeDisplay", "Death in hospital");
                        break;
                    case "2":
                        Dictionary_Set("DPLACE", "PlaceOfDeath", "placeOfDeathTypeCode", "450391000124102");
                        Dictionary_Set("DPLACE", "PlaceOfDeath", "placeOfDeathTypeSystem", "http://snomed.info/sct");
                        Dictionary_Set("DPLACE", "PlaceOfDeath", "placeOfDeathTypeDisplay", "Death in hospital-based emergency department or outpatient department");
                        break;
                    case "3":
                        Dictionary_Set("DPLACE", "PlaceOfDeath", "placeOfDeathTypeCode", "63238001");
                        Dictionary_Set("DPLACE", "PlaceOfDeath", "placeOfDeathTypeSystem", "http://snomed.info/sct");
                        Dictionary_Set("DPLACE", "PlaceOfDeath", "placeOfDeathTypeDisplay", "Dead on arrival at hospital");
                        break;
                    case "4":
                        Dictionary_Set("DPLACE", "PlaceOfDeath", "placeOfDeathTypeCode", "440081000124100");
                        Dictionary_Set("DPLACE", "PlaceOfDeath", "placeOfDeathTypeSystem", "http://snomed.info/sct");
                        Dictionary_Set("DPLACE", "PlaceOfDeath", "placeOfDeathTypeDisplay", "Death in home");
                        break;
                    case "5":
                        Dictionary_Set("DPLACE", "PlaceOfDeath", "placeOfDeathTypeCode", "440071000124103");
                        Dictionary_Set("DPLACE", "PlaceOfDeath", "placeOfDeathTypeSystem", "http://snomed.info/sct");
                        Dictionary_Set("DPLACE", "PlaceOfDeath", "placeOfDeathTypeDisplay", "Death in hospice");
                        break;
                    case "6":
                        Dictionary_Set("DPLACE", "PlaceOfDeath", "placeOfDeathTypeCode", "450381000124100");
                        Dictionary_Set("DPLACE", "PlaceOfDeath", "placeOfDeathTypeSystem", "http://snomed.info/sct");
                        Dictionary_Set("DPLACE", "PlaceOfDeath", "placeOfDeathTypeDisplay", "Death in nursing home or long term care facility");
                        break;
                    case "7":
                        Dictionary_Set("DPLACE", "PlaceOfDeath", "placeOfDeathTypeCode", "UNK");
                        Dictionary_Set("DPLACE", "PlaceOfDeath", "placeOfDeathTypeSystem", "http://hl7.org/fhir/v3/NullFlavor");
                        Dictionary_Set("DPLACE", "PlaceOfDeath", "placeOfDeathTypeDisplay", "Unknown");
                        break;
                    case "9":
                        Dictionary_Set("DPLACE", "PlaceOfDeath", "placeOfDeathTypeCode", "OTH");
                        Dictionary_Set("DPLACE", "PlaceOfDeath", "system", "http://hl7.org/fhir/v3/NullFlavor");
                        Dictionary_Set("DPLACE", "PlaceOfDeath", "placeOfDeathTypeDisplay", "Other");
                        break;
                }
            }
        }

        // <summary>County of Death Occurrence</summary>
        [IJEField(32, 233, 3, "County of Death Occurrence", "COD", 2)]
        public string COD
        {
            get
            {
                return Dictionary_Geo_Get("COD", "PlaceOfDeath", "placeOfDeath", "county", true);
            }
            set
            {
                Dictionary_Geo_Set("COD", "PlaceOfDeath", "placeOfDeath", "county", true, value);
            }
        }

        // <summary>Method of Disposition</summary>
        [IJEField(33, 236, 1, "Method of Disposition", "DISP", 1)]
        public string DISP
        {
            get
            {
                string code = Dictionary_Get_Full("DISP", "Disposition", "dispositionTypeCode");
                switch (code)
                {
                    case "449951000124101": // Donation
                        return "D";
                    case "449971000124106": // Burial
                        return "B";
                    case "449961000124104": // Cremation
                        return "C";
                    case "449931000124108": // Entombment
                        return "E";
                    case "449941000124103": // Removal from state
                        return "R";
                    case "UNK": // Unknown
                        return "U";
                    case "455401000124109": // Hospital Disposition
                    case "OTH": // Other
                        return "O";
                }
                return "";
            }
            set
            {
                switch (value)
                {
                    case "D":
                        Dictionary_Set("DISP", "Disposition", "dispositionTypeCode", "449951000124101");
                        Dictionary_Set("DISP", "Disposition", "dispositionTypeSystem", "http://snomed.info/sct");
                        Dictionary_Set("DISP", "Disposition", "dispositionTypeDisplay", "Donation");
                        break;
                    case "B":
                        Dictionary_Set("DISP", "Disposition", "dispositionTypeCode", "449971000124106");
                        Dictionary_Set("DISP", "Disposition", "dispositionTypeSystem", "http://snomed.info/sct");
                        Dictionary_Set("DISP", "Disposition", "dispositionTypeDisplay", "Burial");
                        break;
                    case "C":
                        Dictionary_Set("DISP", "Disposition", "dispositionTypeCode", "449961000124104");
                        Dictionary_Set("DISP", "Disposition", "dispositionTypeSystem", "http://snomed.info/sct");
                        Dictionary_Set("DISP", "Disposition", "dispositionTypeDisplay", "Cremation");
                        break;
                    case "E":
                        Dictionary_Set("DISP", "Disposition", "dispositionTypeCode", "449931000124108");
                        Dictionary_Set("DISP", "Disposition", "dispositionTypeSystem", "http://snomed.info/sct");
                        Dictionary_Set("DISP", "Disposition", "dispositionTypeDisplay", "Entombment");
                        break;
                    case "R":
                        Dictionary_Set("DISP", "Disposition", "dispositionTypeCode", "449941000124103");
                        Dictionary_Set("DISP", "Disposition", "dispositionTypeSystem", "http://snomed.info/sct");
                        Dictionary_Set("DISP", "Disposition", "dispositionTypeDisplay", "Removal from state");
                        break;
                    case "U":
                        Dictionary_Set("DISP", "Disposition", "dispositionTypeCode", "UNK");
                        Dictionary_Set("DISP", "Disposition", "dispositionTypeSystem", "http://hl7.org/fhir/v3/NullFlavor");
                        Dictionary_Set("DISP", "Disposition", "dispositionTypeDisplay", "Unknown");
                        break;
                    case "O":
                        Dictionary_Set("DISP", "Disposition", "dispositionTypeCode", "OTH");
                        Dictionary_Set("DISP", "Disposition", "dispositionTypeSystem", "http://hl7.org/fhir/v3/NullFlavor");
                        Dictionary_Set("DISP", "Disposition", "dispositionTypeDisplay", "Other");
                        break;
                }
            }
        }

        // <summary>Date of Death--Month</summary>
        [IJEField(34, 237, 2, "Date of Death--Month", "DOD_MO", 1)]
        public string DOD_MO
        {
            get
            {
                return DateTime_Get("DOD_MO", "MM", "DateOfDeath") ?? DateTime_Get("DOD_MO", "MM", "ActualOrPresumedDateOfDeath");
            }
            set
            {
                DateTime_Set("DOD_MO", "MM", "DateOfDeath", value);
                DateTime_Set("DOD_MO", "MM", "ActualOrPresumedDateOfDeath", value);
            }
        }

        // <summary>Date of Death--Day</summary>
        [IJEField(35, 239, 2, "Date of Death--Day", "DOD_DY", 1)]
        public string DOD_DY
        {
            get
            {
                return DateTime_Get("DOD_DY", "dd", "DateOfDeath") ?? DateTime_Get("DOD_DY", "dd", "ActualOrPresumedDateOfDeath");
            }
            set
            {
                DateTime_Set("DOD_DY", "dd", "DateOfDeath", value);
                DateTime_Set("DOD_DY", "dd", "ActualOrPresumedDateOfDeath", value);
            }
        }

        // <summary>Time of Death</summary>
        [IJEField(36, 241, 4, "Time of Death", "TOD", 1)]
        public string TOD
        {
            get
            {
                return DateTime_Get("TOD", "HHmm", "DateOfDeath") ?? DateTime_Get("TOD", "HHmm", "ActualOrPresumedDateOfDeath");
            }
            set
            {
                DateTime_Set("TOD", "HHmm", "DateOfDeath", value);
                DateTime_Set("TOD", "HHmm", "ActualOrPresumedDateOfDeath", value);
            }
        }

        // <summary>Decedent's Education</summary>
        [IJEField(37, 245, 1, "Decedent's Education", "DEDUC", 1)]
        public string DEDUC
        {
            get
            {
                string code = Dictionary_Get_Full("DEDUC", "Education", "code");
                switch (code)
                {
                    case "PHC1448": // 8th grade or less
                        return "1";
                    case "PHC1449": // 9th through 12th grade; no diploma
                        return "2";
                    case "PHC1450": // High School Graduate or GED Completed
                        return "3";
                    case "PHC1451": // Some college credit, but no degree
                        return "4";
                    case "PHC1452": // Associate Degree
                        return "5";
                    case "PHC1453": // Bachelor's Degree
                        return "6";
                    case "PHC1454": // Master's Degree
                        return "7";
                    case "PHC1455": // Doctorate Degree or Professional Degree
                        return "8";
                    case "UNK": // Unknown
                        return "9";
                }
                return "";
            }
            set
            {
                switch (value)
                {
                    case "1":
                        Dictionary_Set("DEDUC", "Education", "code", "PHC1448");
                        Dictionary_Set("DEDUC", "Education", "system", "http://github.com/nightingaleproject/fhirDeathRecord/sdr/decedent/cs/EducationCS");
                        Dictionary_Set("DEDUC", "Education", "display", "8th grade or less");
                        break;
                    case "2":
                        Dictionary_Set("DEDUC", "Education", "code", "PHC1449");
                        Dictionary_Set("DEDUC", "Education", "system", "http://github.com/nightingaleproject/fhirDeathRecord/sdr/decedent/cs/EducationCS");
                        Dictionary_Set("DEDUC", "Education", "display", "9th through 12th grade; no diploma");
                        break;
                    case "3":
                        Dictionary_Set("DEDUC", "Education", "code", "PHC1450");
                        Dictionary_Set("DEDUC", "Education", "system", "http://github.com/nightingaleproject/fhirDeathRecord/sdr/decedent/cs/EducationCS");
                        Dictionary_Set("DEDUC", "Education", "display", "High School Graduate or GED Completed");
                        break;
                    case "4":
                        Dictionary_Set("DEDUC", "Education", "code", "PHC1451");
                        Dictionary_Set("DEDUC", "Education", "system", "http://github.com/nightingaleproject/fhirDeathRecord/sdr/decedent/cs/EducationCS");
                        Dictionary_Set("DEDUC", "Education", "display", "Some college credit, but no degree");
                        break;
                    case "5":
                        Dictionary_Set("DEDUC", "Education", "code", "PHC1452");
                        Dictionary_Set("DEDUC", "Education", "system", "http://github.com/nightingaleproject/fhirDeathRecord/sdr/decedent/cs/EducationCS");
                        Dictionary_Set("DEDUC", "Education", "display", "Associate Degree");
                        break;
                    case "6":
                        Dictionary_Set("DEDUC", "Education", "code", "PHC1453");
                        Dictionary_Set("DEDUC", "Education", "system", "http://github.com/nightingaleproject/fhirDeathRecord/sdr/decedent/cs/EducationCS");
                        Dictionary_Set("DEDUC", "Education", "display", "Bachelor's Degree");
                        break;
                    case "7":
                        Dictionary_Set("DEDUC", "Education", "code", "PHC1454");
                        Dictionary_Set("DEDUC", "Education", "system", "http://github.com/nightingaleproject/fhirDeathRecord/sdr/decedent/cs/EducationCS");
                        Dictionary_Set("DEDUC", "Education", "display", "Master's Degree");
                        break;
                    case "8":
                        Dictionary_Set("DEDUC", "Education", "code", "PHC1455");
                        Dictionary_Set("DEDUC", "Education", "system", "http://github.com/nightingaleproject/fhirDeathRecord/sdr/decedent/cs/EducationCS");
                        Dictionary_Set("DEDUC", "Education", "display", "Doctorate Degree or Professional Degree");
                        break;
                    case "9":
                        Dictionary_Set("DEDUC", "Education", "code", "UNK");
                        Dictionary_Set("DEDUC", "Education", "system", "http://hl7.org/fhir/v3/NullFlavor");
                        Dictionary_Set("DEDUC", "Education", "display", "Unknown");
                        break;
                }
            }
        }

        // <summary>Decedent of Hispanic Origin?--Mexican</summary>
        [IJEField(39, 247, 1, "Decedent of Hispanic Origin?--Mexican", "DETHNIC1", 1)]
        public string DETHNIC1
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Decedent of Hispanic Origin?--Puerto Rican</summary>
        [IJEField(40, 248, 1, "Decedent of Hispanic Origin?--Puerto Rican", "DETHNIC2", 1)]
        public string DETHNIC2
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Decedent of Hispanic Origin?--Cuban</summary>
        [IJEField(41, 249, 1, "Decedent of Hispanic Origin?--Cuban", "DETHNIC3", 1)]
        public string DETHNIC3
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Decedent of Hispanic Origin?--Other</summary>
        [IJEField(42, 250, 1, "Decedent of Hispanic Origin?--Other", "DETHNIC4", 1)]
        public string DETHNIC4
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Decedent of Hispanic Origin?--Other, Literal</summary>
        [IJEField(43, 251, 20, "Decedent of Hispanic Origin?--Other, Literal", "DETHNIC5", 1)]
        public string DETHNIC5
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Decedent's Race--White</summary>
        [IJEField(44, 271, 1, "Decedent's Race--White", "RACE1", 1)]
        public string RACE1
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Decedent's Race--Black or African American</summary>
        [IJEField(45, 272, 1, "Decedent's Race--Black or African American", "RACE2", 1)]
        public string RACE2
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Decedent's Race--American Indian or Alaska Native</summary>
        [IJEField(46, 273, 1, "Decedent's Race--American Indian or Alaska Native", "RACE3", 1)]
        public string RACE3
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Decedent's Race--Asian Indian</summary>
        [IJEField(47, 274, 1, "Decedent's Race--Asian Indian", "RACE4", 1)]
        public string RACE4
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Decedent's Race--Chinese</summary>
        [IJEField(48, 275, 1, "Decedent's Race--Chinese", "RACE5", 1)]
        public string RACE5
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Decedent's Race--Filipino</summary>
        [IJEField(49, 276, 1, "Decedent's Race--Filipino", "RACE6", 1)]
        public string RACE6
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Decedent's Race--Japanese</summary>
        [IJEField(50, 277, 1, "Decedent's Race--Japanese", "RACE7", 1)]
        public string RACE7
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Decedent's Race--Korean</summary>
        [IJEField(51, 278, 1, "Decedent's Race--Korean", "RACE8", 1)]
        public string RACE8
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Decedent's Race--Vietnamese</summary>
        [IJEField(52, 279, 1, "Decedent's Race--Vietnamese", "RACE9", 1)]
        public string RACE9
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Decedent's Race--Other Asian</summary>
        [IJEField(53, 280, 1, "Decedent's Race--Other Asian", "RACE10", 1)]
        public string RACE10
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Decedent's Race--Native Hawaiian</summary>
        [IJEField(54, 281, 1, "Decedent's Race--Native Hawaiian", "RACE11", 1)]
        public string RACE11
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Decedent's Race--Guamanian or Chamorro</summary>
        [IJEField(55, 282, 1, "Decedent's Race--Guamanian or Chamorro", "RACE12", 1)]
        public string RACE12
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Decedent's Race--Samoan</summary>
        [IJEField(56, 283, 1, "Decedent's Race--Samoan", "RACE13", 1)]
        public string RACE13
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Decedent's Race--Other Pacific Islander</summary>
        [IJEField(57, 284, 1, "Decedent's Race--Other Pacific Islander", "RACE14", 1)]
        public string RACE14
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Decedent's Race--Other</summary>
        [IJEField(58, 285, 1, "Decedent's Race--Other", "RACE15", 1)]
        public string RACE15
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Occupation -- Literal (OPTIONAL)</summary>
        [IJEField(84, 575, 40, "Occupation -- Literal (OPTIONAL)", "OCCUP", 1)]
        public string OCCUP
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Industry -- Literal (OPTIONAL)</summary>
        [IJEField(86, 618, 40, "Industry -- Literal (OPTIONAL)", "INDUST", 1)]
        public string INDUST
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Manner of Death</summary>
        [IJEField(99, 701, 1, "Manner of Death", "MANNER", 1)]
        public string MANNER
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Was Autopsy performed</summary>
        [IJEField(108, 976, 1, "Was Autopsy performed", "AUTOP", 1)]
        public string AUTOP
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Were Autopsy Findings Available to Complete the Cause of Death?</summary>
        [IJEField(109, 977, 1, "Were Autopsy Findings Available to Complete the Cause of Death?", "AUTOPF", 1)]
        public string AUTOPF
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Did Tobacco Use Contribute to Death?</summary>
        [IJEField(110, 978, 1, "Did Tobacco Use Contribute to Death?", "TOBAC", 1)]
        public string TOBAC
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Pregnancy</summary>
        [IJEField(111, 979, 1, "Pregnancy", "PREG", 1)]
        public string PREG
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Date of injury--month</summary>
        [IJEField(113, 981, 2, "Date of injury--month", "DOI_MO", 1)]
        public string DOI_MO
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Date of injury--day</summary>
        [IJEField(114, 983, 2, "Date of injury--day", "DOI_DY", 1)]
        public string DOI_DY
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Date of injury--year</summary>
        [IJEField(115, 985, 4, "Date of injury--year", "DOI_YR", 1)]
        public string DOI_YR
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Time of injury</summary>
        [IJEField(116, 989, 4, "Time of injury", "TOI_HR", 1)]
        public string TOI_HR
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Injury at work</summary>
        [IJEField(117, 993, 1, "Injury at work", "WORKINJ", 1)]
        public string WORKINJ
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Title of Certifier</summary>
        [IJEField(118, 994, 30, "Title of Certifier", "CERTL", 1)]
        public string CERTL
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Time of Injury Unit</summary>
        [IJEField(125, 1075, 1, "Time of Injury Unit", "TOI_UNIT", 1)]
        public string TOI_UNIT
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Decedent ever served in Armed Forces?</summary>
        [IJEField(127, 1081, 1, "Decedent ever served in Armed Forces?", "ARMEDF", 1)]
        public string ARMEDF
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Death Institution name</summary>
        [IJEField(128, 1082, 30, "Death Institution name", "DINSTI", 1)]
        public string DINSTI
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Long String address for place of death</summary>
        [IJEField(129, 1112, 50, "Long String address for place of death", "ADDRESS_D", 1)]
        public string ADDRESS_D
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Place of death. Street number</summary>
        [IJEField(130, 1162, 10, "Place of death. Street number", "STNUM_D", 1)]
        public string STNUM_D
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Place of death. Pre Directional</summary>
        [IJEField(131, 1172, 10, "Place of death. Pre Directional", "PREDIR_D", 1)]
        public string PREDIR_D
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Place of death. Street name</summary>
        [IJEField(132, 1182, 50, "Place of death. Street name", "STNAME_D", 1)]
        public string STNAME_D
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Place of death. Street designator</summary>
        [IJEField(133, 1232, 10, "Place of death. Street designator", "STDESIG_D", 1)]
        public string STDESIG_D
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Place of death. Post Directional</summary>
        [IJEField(134, 1242, 10, "Place of death. Post Directional", "POSTDIR_D", 1)]
        public string POSTDIR_D
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Place of death. City or Town name</summary>
        [IJEField(135, 1252, 28, "Place of death. City or Town name", "CITYTEXT_D", 1)]
        public string CITYTEXT_D
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Place of death. State name literal</summary>
        [IJEField(136, 1280, 28, "Place of death. State name literal", "STATETEXT_D", 1)]
        public string STATETEXT_D
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Place of death. Zip code</summary>
        [IJEField(137, 1308, 9, "Place of death. Zip code", "ZIP9_D", 1)]
        public string ZIP9_D
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Place of death. County of Death</summary>
        [IJEField(138, 1317, 28, "Place of death. County of Death", "COUNTYTEXT_D", 2)]
        public string COUNTYTEXT_D
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Place of death. City FIPS code</summary>
        [IJEField(139, 1345, 5, "Place of death. City FIPS code", "CITYCODE_D", 3)]
        public string CITYCODE_D
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Place of death. Longitude</summary>
        [IJEField(140, 1350, 17, "Place of death. Longitude", "LONG_D", 1)]
        public string LONG_D
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Place of Death. Latitude</summary>
        [IJEField(141, 1367, 17, "Place of Death. Latitude", "LAT_D", 1)]
        public string LAT_D
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Spouse's First Name</summary>
        [IJEField(143, 1385, 50, "Spouse's First Name", "SPOUSEF", 1)]
        public string SPOUSEF
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Husband's Surname/Wife's Maiden Last Name</summary>
        [IJEField(144, 1435, 50, "Husband's Surname/Wife's Maiden Last Name", "SPOUSEL", 1)]
        public string SPOUSEL
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Decedent's Residence - Street number</summary>
        [IJEField(145, 1485, 10, "Decedent's Residence - Street number", "STNUM_R", 1)]
        public string STNUM_R
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Decedent's Residence - Pre Directional</summary>
        [IJEField(146, 1495, 10, "Decedent's Residence - Pre Directional", "PREDIR_R", 1)]
        public string PREDIR_R
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Decedent's Residence - Street name</summary>
        [IJEField(147, 1505, 28, "Decedent's Residence - Street name", "STNAME_R", 1)]
        public string STNAME_R
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Decedent's Residence - Street designator</summary>
        [IJEField(148, 1533, 10, "Decedent's Residence - Street designator", "STDESIG_R", 1)]
        public string STDESIG_R
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Decedent's Residence - Post Directional</summary>
        [IJEField(149, 1543, 10, "Decedent's Residence - Post Directional", "POSTDIR_R", 1)]
        public string POSTDIR_R
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Decedent's Residence - Unit or apt number</summary>
        [IJEField(150, 1553, 7, "Decedent's Residence - Unit or apt number", "UNITNUM_R", 1)]
        public string UNITNUM_R
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Decedent's Residence - City or Town name</summary>
        [IJEField(151, 1560, 28, "Decedent's Residence - City or Town name", "CITYTEXT_R", 1)]
        public string CITYTEXT_R
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Decedent's Residence - ZIP code</summary>
        [IJEField(152, 1588, 9, "Decedent's Residence - ZIP code", "ZIP9_R", 1)]
        public string ZIP9_R
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Decedent's Residence - County</summary>
        [IJEField(153, 1597, 28, "Decedent's Residence - County", "COUNTYTEXT_R", 1)]
        public string COUNTYTEXT_R
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Decedent's Residence - State name</summary>
        [IJEField(154, 1625, 28, "Decedent's Residence - State name", "STATETEXT_R", 1)]
        public string STATETEXT_R
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Decedent's Residence - COUNTRY name</summary>
        [IJEField(155, 1653, 28, "Decedent's Residence - COUNTRY name", "COUNTRYTEXT_R", 1)]
        public string COUNTRYTEXT_R
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Long string address for decedent's place of residence same as above but allows states to choose the way they capture information.</summary>
        [IJEField(156, 1681, 50, "Long string address for decedent's place of residence same as above but allows states to choose the way they capture information.", "ADDRESS_R", 1)]
        public string ADDRESS_R
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Hispanic Origin - Specify</summary>
        [IJEField(163, 1743, 15, "Hispanic Origin - Specify ", "HISPSTSP", 1)]
        public string HISPSTSP
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Race - Specify</summary>
        [IJEField(164, 1758, 50, "Race - Specify", "RACESTSP", 1)]
        public string RACESTSP
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Middle Name of Decedent</summary>
        [IJEField(165, 1808, 50, "Middle Name of Decedent ", "DMIDDLE", 1)]
        public string DMIDDLE
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Father's First Name</summary>
        [IJEField(166, 1858, 50, "Father's First Name", "DDADF", 1)]
        public string DDADF
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Father's Middle Name</summary>
        [IJEField(167, 1908, 50, "Father's Middle Name", "DDADMID", 1)]
        public string DDADMID
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Mother's First Name</summary>
        [IJEField(168, 1958, 50, "Mother's First Name", "DMOMF", 1)]
        public string DMOMF
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Mother's Middle Name</summary>
        [IJEField(169, 2008, 50, "Mother's Middle Name", "DMOMMID", 1)]
        public string DMOMMID
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Mother's Maiden Surname</summary>
        [IJEField(170, 2058, 50, "Mother's Maiden Surname", "DMOMMDN", 1)]
        public string DMOMMDN
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Was case Referred to Medical Examiner/Coroner?</summary>
        [IJEField(171, 2108, 1, "Was case Referred to Medical Examiner/Coroner?", "REFERRED", 1)]
        public string REFERRED
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Place of Injury- literal</summary>
        [IJEField(172, 2109, 50, "Place of Injury- literal", "POILITRL", 1)]
        public string POILITRL
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Describe How Injury Occurred</summary>
        [IJEField(173, 2159, 250, "Describe How Injury Occurred", "HOWINJ", 1)]
        public string HOWINJ
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>If Transportation Accident, Specify</summary>
        [IJEField(174, 2409, 30, "If Transportation Accident, Specify", "TRANSPRT", 1)]
        public string TRANSPRT
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>County of Injury - literal</summary>
        [IJEField(175, 2439, 28, "County of Injury - literal", "COUNTYTEXT_I", 1)]
        public string COUNTYTEXT_I
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>County of Injury code</summary>
        [IJEField(176, 2467, 3, "County of Injury code", "COUNTYCODE_I", 2)]
        public string COUNTYCODE_I
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Town/city of Injury - literal</summary>
        [IJEField(177, 2470, 28, "Town/city of Injury - literal", "CITYTEXT_I", 1)]
        public string CITYTEXT_I
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Town/city of Injury code</summary>
        [IJEField(178, 2498, 5, "Town/city of Injury code", "CITYCODE_I", 3)]
        public string CITYCODE_I
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>State, U.S. Territory or Canadian Province of Injury - code</summary>
        [IJEField(179, 2503, 2, "State, U.S. Territory or Canadian Province of Injury - code", "STATECODE_I", 1)]
        public string STATECODE_I
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Place of injury. Longitude</summary>
        [IJEField(180, 2505, 17, "Place of injury. Longitude", "LONG_I", 1)]
        public string LONG_I
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Place of injury. Latitude</summary>
        [IJEField(181, 2522, 17, "Place of injury. Latitude", "LAT_I", 1)]
        public string LAT_I
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Cause of Death Part I Line a</summary>
        [IJEField(184, 2542, 120, "Cause of Death Part I Line a", "COD1A", 1)]
        public string COD1A
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Cause of Death Part I Interval, Line a</summary>
        [IJEField(185, 2662, 20, "Cause of Death Part I Interval, Line a", "INTERVAL1A", 1)]
        public string INTERVAL1A
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Cause of Death Part I Line b</summary>
        [IJEField(186, 2682, 120, "Cause of Death Part I Line b", "COD1B", 1)]
        public string COD1B
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Cause of Death Part I Interval, Line b</summary>
        [IJEField(187, 2802, 20, "Cause of Death Part I Interval, Line b", "INTERVAL1B", 1)]
        public string INTERVAL1B
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Cause of Death Part I Line c</summary>
        [IJEField(188, 2822, 120, "Cause of Death Part I Line c", "COD1C", 1)]
        public string COD1C
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Cause of Death Part I Interval, Line c</summary>
        [IJEField(189, 2942, 20, "Cause of Death Part I Interval, Line c", "INTERVAL1C", 1)]
        public string INTERVAL1C
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Cause of Death Part I Line d</summary>
        [IJEField(190, 2962, 120, "Cause of Death Part I Line d", "COD1D", 1)]
        public string COD1D
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Cause of Death Part I Interval, Line d</summary>
        [IJEField(191, 3082, 20, "Cause of Death Part I Interval, Line d", "INTERVAL1D", 1)]
        public string INTERVAL1D
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Cause of Death Part II</summary>
        [IJEField(192, 3102, 240, "Cause of Death Part II", "OTHERCONDITION", 1)]
        public string OTHERCONDITION
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Decedent's Maiden Name</summary>
        [IJEField(193, 3342, 50, "Decedent's Maiden Name", "DMAIDEN", 1)]
        public string DMAIDEN
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Decedent's Birth Place City - Code</summary>
        [IJEField(194, 3392, 5, "Decedent's Birth Place City - Code", "DBPLACECITYCODE", 3)]
        public string DBPLACECITYCODE
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Decedent's Birth Place City - Literal</summary>
        [IJEField(195, 3397, 28, "Decedent's Birth Place City - Literal", "DBPLACECITY", 1)]
        public string DBPLACECITY
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Spouse's Middle Name</summary>
        [IJEField(196, 3425, 50, "Spouse's Middle Name", "SPOUSEMIDNAME", 1)]
        public string SPOUSEMIDNAME
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Spouse's Suffix</summary>
        [IJEField(197, 3475, 10, "Spouse's Suffix", "SPOUSESUFFIX", 1)]
        public string SPOUSESUFFIX
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Father's Suffix</summary>
        [IJEField(198, 3485, 10, "Father's Suffix", "FATHERSUFFIX", 1)]
        public string FATHERSUFFIX
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Mother's Suffix</summary>
        [IJEField(199, 3495, 10, "Mother's Suffix", "MOTHERSSUFFIX", 1)]
        public string MOTHERSSUFFIX
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Informant's Relationship</summary>
        [IJEField(200, 3505, 30, "Informant's Relationship", "INFORMRELATE", 1)]
        public string INFORMRELATE
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>State, U.S. Territory or Canadian Province of Disposition - code</summary>
        [IJEField(201, 3535, 2, "State, U.S. Territory or Canadian Province of Disposition - code", "DISPSTATECD", 1)]
        public string DISPSTATECD
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Disposition State or Territory - Literal</summary>
        [IJEField(202, 3537, 28, "Disposition State or Territory - Literal", "DISPSTATE", 1)]
        public string DISPSTATE
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Disposition City - Code</summary>
        [IJEField(203, 3565, 5, "Disposition City - Code", "DISPCITYCODE", 3)]
        public string DISPCITYCODE
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Disposition City - Literal</summary>
        [IJEField(204, 3570, 28, "Disposition City - Literal", "DISPCITY", 1)]
        public string DISPCITY
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Funeral Facility Name</summary>
        [IJEField(205, 3598, 100, "Funeral Facility Name", "FUNFACNAME", 1)]
        public string FUNFACNAME
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Funeral Facility - Street number</summary>
        [IJEField(206, 3698, 10, "Funeral Facility - Street number", "FUNFACSTNUM", 1)]
        public string FUNFACSTNUM
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Funeral Facility - Pre Directional</summary>
        [IJEField(207, 3708, 10, "Funeral Facility - Pre Directional", "FUNFACPREDIR", 1)]
        public string FUNFACPREDIR
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Funeral Facility - Street name</summary>
        [IJEField(208, 3718, 28, "Funeral Facility - Street name", "FUNFACSTRNAME", 1)]
        public string FUNFACSTRNAME
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Funeral Facility - Street designator</summary>
        [IJEField(209, 3746, 10, "Funeral Facility - Street designator", "FUNFACSTRDESIG", 1)]
        public string FUNFACSTRDESIG
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Funeral Facility - Post Directional</summary>
        [IJEField(210, 3756, 10, "Funeral Facility - Post Directional", "FUNPOSTDIR", 1)]
        public string FUNPOSTDIR
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Funeral Facility - Unit or apt number</summary>
        [IJEField(211, 3766, 7, "Funeral Facility - Unit or apt number", "FUNUNITNUM", 1)]
        public string FUNUNITNUM
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Long string address for Funeral Facility same as above but allows states to choose the way they capture information.</summary>
        [IJEField(212, 3773, 50, "Long string address for Funeral Facility same as above but allows states to choose the way they capture information.", "FUNFACADDRESS", 1)]
        public string FUNFACADDRESS
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Funeral Facility - City or Town name</summary>
        [IJEField(213, 3823, 28, "Funeral Facility - City or Town name", "FUNCITYTEXT", 1)]
        public string FUNCITYTEXT
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>State, U.S. Territory or Canadian Province of Funeral Facility - code</summary>
        [IJEField(214, 3851, 2, "State, U.S. Territory or Canadian Province of Funeral Facility - code", "FUNSTATECD", 1)]
        public string FUNSTATECD
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>State, U.S. Territory or Canadian Province of Funeral Facility - literal</summary>
        [IJEField(215, 3853, 28, "State, U.S. Territory or Canadian Province of Funeral Facility - literal", "FUNSTATE", 1)]
        public string FUNSTATE
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Funeral Facility - ZIP</summary>
        [IJEField(216, 3881, 9, "Funeral Facility - ZIP", "FUNZIP", 1)]
        public string FUNZIP
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Person Pronouncing Date Signed</summary>
        [IJEField(217, 3890, 8, "Person Pronouncing Date Signed", "PPDATESIGNED", 1)]
        public string PPDATESIGNED
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Person Pronouncing Time Pronounced</summary>
        [IJEField(218, 3898, 4, "Person Pronouncing Time Pronounced", "PPTIME", 1)]
        public string PPTIME
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Certifier's First Name</summary>
        [IJEField(219, 3902, 50, "Certifier's First Name", "CERTFIRST", 1)]
        public string CERTFIRST
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Certifier's Middle Name</summary>
        [IJEField(220, 3952, 50, "Certifier's Middle Name", "CERTMIDDLE", 1)]
        public string CERTMIDDLE
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Certifier's Last Name</summary>
        [IJEField(221, 4002, 50, "Certifier's Last Name", "CERTLAST", 1)]
        public string CERTLAST
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Certifier's Suffix Name</summary>
        [IJEField(222, 4052, 10, "Certifier's Suffix Name", "CERTSUFFIX", 1)]
        public string CERTSUFFIX
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Certifier - Street number</summary>
        [IJEField(223, 4062, 10, "Certifier - Street number", "CERTSTNUM", 1)]
        public string CERTSTNUM
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Certifier - Pre Directional</summary>
        [IJEField(224, 4072, 10, "Certifier - Pre Directional", "CERTPREDIR", 1)]
        public string CERTPREDIR
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Certifier - Street name</summary>
        [IJEField(225, 4082, 28, "Certifier - Street name", "CERTSTRNAME", 1)]
        public string CERTSTRNAME
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Certifier - Street designator</summary>
        [IJEField(226, 4110, 10, "Certifier - Street designator", "CERTSTRDESIG", 1)]
        public string CERTSTRDESIG
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Certifier - Post Directional</summary>
        [IJEField(227, 4120, 10, "Certifier - Post Directional", "CERTPOSTDIR", 1)]
        public string CERTPOSTDIR
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Certifier - Unit or apt number</summary>
        [IJEField(228, 4130, 7, "Certifier - Unit or apt number", "CERTUNITNUM", 1)]
        public string CERTUNITNUM
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Long string address for Certifier same as above but allows states to choose the way they capture information.</summary>
        [IJEField(229, 4137, 50, "Long string address for Certifier same as above but allows states to choose the way they capture information.", "CERTADDRESS", 1)]
        public string CERTADDRESS
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Certifier - City or Town name</summary>
        [IJEField(230, 4187, 28, "Certifier - City or Town name", "CERTCITYTEXT", 1)]
        public string CERTCITYTEXT
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>State, U.S. Territory or Canadian Province of Certifier - code</summary>
        [IJEField(231, 4215, 2, "State, U.S. Territory or Canadian Province of Certifier - code", "CERTSTATECD", 1)]
        public string CERTSTATECD
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>State, U.S. Territory or Canadian Province of Certifier - literal</summary>
        [IJEField(232, 4217, 28, "State, U.S. Territory or Canadian Province of Certifier - literal", "CERTSTATE", 1)]
        public string CERTSTATE
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Certifier - Zip</summary>
        [IJEField(233, 4245, 9, "Certifier - Zip", "CERTZIP", 1)]
        public string CERTZIP
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Certifier Date Signed</summary>
        [IJEField(234, 4254, 8, "Certifier Date Signed", "CERTDATE", 1)]
        public string CERTDATE
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>State, U.S. Territory or Canadian Province of Injury - literal</summary>
        [IJEField(236, 4270, 28, "State, U.S. Territory or Canadian Province of Injury - literal", "STINJURY", 1)]
        public string STINJURY
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>State, U.S. Territory or Canadian Province of Birth - literal</summary>
        [IJEField(237, 4298, 28, "State, U.S. Territory or Canadian Province of Birth - literal", "STATEBTH", 1)]
        public string STATEBTH
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Country of Death - Code</summary>
        [IJEField(238, 4326, 2, "Country of Death - Code", "DTHCOUNTRYCD", 1)]
        public string DTHCOUNTRYCD
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Country of Death - Literal</summary>
        [IJEField(239, 4328, 28, "Country of Death - Literal", "DTHCOUNTRY", 1)]
        public string DTHCOUNTRY
        {
            get
            {
                return "";
            }
            set
            {
                // TODO
            }
        }

    }
}
