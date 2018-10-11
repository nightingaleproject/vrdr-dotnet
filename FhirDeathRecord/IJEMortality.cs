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

        public IJEField(int field, int location, int length, string contents, string name)
        {
            this.Field = field;
            this.Location = location;
            this.Length = length;
            this.Contents = contents;
            this.Name = name;
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

        /// <summary>IJE data lookup helper.</summary>
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
            // Loop over every property (these are the fields)
            foreach(PropertyInfo property in typeof(IJEMortality).GetProperties())
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

        // <summary>Helps decompose a DateTime into individual parts (year, month, day).</summary>
        private string DateTimeStringHelper(IJEField info, string value, string type, DateTime date)
        {
            if (type == "yyyy")
            {
                return date.ToString($"{Truncate(value, info.Length)}-MM-dd");
            }
            else if (type == "MM")
            {
                return date.ToString($"yyyy-{Truncate(value, info.Length)}-dd");
            }
            else if (type == "dd")
            {
                return date.ToString($"yyyy-MM-{Truncate(value, info.Length)}");
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
        private string Dictionary_Geo_Get(string ijeFieldName, string fhirFieldName, string key, string geoType, bool isCoded)
        {
            IJEField info = FieldInfo(ijeFieldName);
            Dictionary<string, string> dictionary = (Dictionary<string, string>)typeof(DeathRecord).GetProperty(fhirFieldName).GetValue(this.record);
            string current = Convert.ToString(dictionary[key]);
            if (isCoded)
            {
                if (geoType == "state")
                {
                    current = dataLookup.StateTerritoryProvinceToCode(current);
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
        private void Dictionary_Geo_Set(string ijeFieldName, string fhirFieldName, string key, string geoType, bool isCoded, string value)
        {
            IJEField info = FieldInfo(ijeFieldName);
            Dictionary<string, string> dictionary = (Dictionary<string, string>)typeof(DeathRecord).GetProperty(fhirFieldName).GetValue(this.record);
            if (dictionary != null && (!dictionary.ContainsKey(key) || string.IsNullOrEmpty(dictionary[key])))
            {
                if (isCoded)
                {
                    if (geoType == "state")
                    {
                        dictionary[key] = dataLookup.CodeToStateTerritoryProvince(value);;
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
        /////////////////////////////////////////////////////////////////////////////////

        // <summary>Date of Death--Year</summary>
        [IJEField(1, 1, 4, "Date of Death--Year", "DOD_YR")]
        public string DOD_YR
        {
            get
            {
                return DateTime_Get("DOD_YR", "yyyy", "DateOfDeath");
            }
            set
            {
                DateTime_Set("DOD_YR", "yyyy", "DateOfDeath", value);
            }
        }

        // <summary>State, U.S. Territory or Canadian Province of Death - code</summary>
        [IJEField(2, 5, 2, "State, U.S. Territory or Canadian Province of Death - code", "DSTATE")]
        public string DSTATE
        {
            get
            {
                return Dictionary_Geo_Get("DSTATE", "PlaceOfDeath", "placeOfDeathState", "state", true);
            }
            set
            {
                Dictionary_Geo_Set("DSTATE", "PlaceOfDeath", "placeOfDeathState", "state", true, value);
            }
        }

        // <summary>Certificate Number</summary>
        [IJEField(3, 7, 6, "Certificate Number", "FILENO")]
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
        [IJEField(7, 27, 50, "Decedent's Legal Name--Given", "GNAME")]
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
        [IJEField(8, 77, 1, "Decedent's Legal Name--Middle", "MNAME")]
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
        [IJEField(9, 78, 50, "Decedent's Legal Name--Last", "LNAME")]
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
        [IJEField(10, 128, 10, "Decedent's Legal Name--Suffix", "SUFF")]
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
        [IJEField(13, 189, 1, "Sex", "SEX")]
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
        [IJEField(15, 191, 9, "Social Security Number", "SSN")]
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
        [IJEField(16, 200, 1, "Decedent's Age--Type", "AGETYPE")]
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
        [IJEField(17, 201, 3, "Decedent's Age--Units", "AGE")]
        public string AGE
        {
            get
            {
                IJEField info = FieldInfo("AGE");
                string dateOfDeathCurrent = Convert.ToString(typeof(DeathRecord).GetProperty("DateOfDeath").GetValue(this.record));
                string dateOfBirthCurrent = Convert.ToString(typeof(DeathRecord).GetProperty("DateOfBirth").GetValue(this.record));
                DateTime dateOfDeath;
                DateTime dateOfBirth;
                int years = 0;
                if (dateOfDeathCurrent != null && DateTime.TryParse(dateOfDeathCurrent, out dateOfDeath) &&
                    dateOfBirthCurrent != null && DateTime.TryParse(dateOfBirthCurrent, out dateOfBirth))
                {
                    years = (int)((dateOfDeath - dateOfBirth).Days/365.2425);
                }
                return Truncate(Convert.ToString(years), info.Length).PadLeft(info.Length, '0');
            }
            set
            {
                // NOOP; Date of Birth and Date of Death should be set individually.
            }
        }

        // <summary>Date of Birth--Year</summary>
        [IJEField(19, 205, 4, "Date of Birth--Year", "DOB_YR")]
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
        [IJEField(20, 209, 2, "Date of Birth--Month", "DOB_MO")]
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
        [IJEField(21, 211, 2, "Date of Birth--Day", "DOB_DY")]
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
        [IJEField(22, 213, 2, "Birthplace--Country", "BPLACE_CNT")]
        public string BPLACE_CNT
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>State, U.S. Territory or Canadian Province of Birth - code</summary>
        [IJEField(23, 215, 2, "State, U.S. Territory or Canadian Province of Birth - code", "BPLACE_ST")]
        public string BPLACE_ST
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Decedent's Residence--City</summary>
        [IJEField(24, 217, 5, "Decedent's Residence--City", "CITYC")]
        public string CITYC
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Decedent's Residence--County</summary>
        [IJEField(25, 222, 3, "Decedent's Residence--County", "COUNTYC")]
        public string COUNTYC
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>State, U.S. Territory or Canadian Province of Decedent's residence - code</summary>
        [IJEField(26, 225, 2, "State, U.S. Territory or Canadian Province of Decedent's residence - code", "STATEC")]
        public string STATEC
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Decedent's Residence--Country</summary>
        [IJEField(27, 227, 2, "Decedent's Residence--Country", "COUNTRYC")]
        public string COUNTRYC
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Decedent's Residence--Inside City Limits</summary>
        [IJEField(28, 229, 1, "Decedent's Residence--Inside City Limits", "LIMITS")]
        public string LIMITS
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Marital Status</summary>
        [IJEField(29, 230, 1, "Marital Status", "MARITAL")]
        public string MARITAL
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Place of Death</summary>
        [IJEField(31, 232, 1, "Place of Death", "DPLACE")]
        public string DPLACE
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>County of Death Occurrence</summary>
        [IJEField(32, 233, 3, "County of Death Occurrence", "COD")]
        public string COD
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Method of Disposition</summary>
        [IJEField(33, 236, 1, "Method of Disposition", "DISP")]
        public string DISP
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Date of Death--Month</summary>
        [IJEField(34, 237, 2, "Date of Death--Month", "DOD_MO")]
        public string DOD_MO
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Date of Death--Day</summary>
        [IJEField(35, 239, 2, "Date of Death--Day", "DOD_DY")]
        public string DOD_DY
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Time of Death</summary>
        [IJEField(36, 241, 4, "Time of Death", "TOD")]
        public string TOD
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Decedent's Education</summary>
        [IJEField(37, 245, 1, "Decedent's Education", "DEDUC")]
        public string DEDUC
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Decedent of Hispanic Origin?--Mexican</summary>
        [IJEField(39, 247, 1, "Decedent of Hispanic Origin?--Mexican", "DETHNIC1")]
        public string DETHNIC1
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Decedent of Hispanic Origin?--Puerto Rican</summary>
        [IJEField(40, 248, 1, "Decedent of Hispanic Origin?--Puerto Rican", "DETHNIC2")]
        public string DETHNIC2
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Decedent of Hispanic Origin?--Cuban</summary>
        [IJEField(41, 249, 1, "Decedent of Hispanic Origin?--Cuban", "DETHNIC3")]
        public string DETHNIC3
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Decedent of Hispanic Origin?--Other</summary>
        [IJEField(42, 250, 1, "Decedent of Hispanic Origin?--Other", "DETHNIC4")]
        public string DETHNIC4
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Decedent of Hispanic Origin?--Other, Literal</summary>
        [IJEField(43, 251, 20, "Decedent of Hispanic Origin?--Other, Literal", "DETHNIC5")]
        public string DETHNIC5
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Decedent's Race--White</summary>
        [IJEField(44, 271, 1, "Decedent's Race--White", "RACE1")]
        public string RACE1
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Decedent's Race--Black or African American</summary>
        [IJEField(45, 272, 1, "Decedent's Race--Black or African American", "RACE2")]
        public string RACE2
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Decedent's Race--American Indian or Alaska Native</summary>
        [IJEField(46, 273, 1, "Decedent's Race--American Indian or Alaska Native", "RACE3")]
        public string RACE3
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Decedent's Race--Asian Indian</summary>
        [IJEField(47, 274, 1, "Decedent's Race--Asian Indian", "RACE4")]
        public string RACE4
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Decedent's Race--Chinese</summary>
        [IJEField(48, 275, 1, "Decedent's Race--Chinese", "RACE5")]
        public string RACE5
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Decedent's Race--Filipino</summary>
        [IJEField(49, 276, 1, "Decedent's Race--Filipino", "RACE6")]
        public string RACE6
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Decedent's Race--Japanese</summary>
        [IJEField(50, 277, 1, "Decedent's Race--Japanese", "RACE7")]
        public string RACE7
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Decedent's Race--Korean</summary>
        [IJEField(51, 278, 1, "Decedent's Race--Korean", "RACE8")]
        public string RACE8
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Decedent's Race--Vietnamese</summary>
        [IJEField(52, 279, 1, "Decedent's Race--Vietnamese", "RACE9")]
        public string RACE9
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Decedent's Race--Other Asian</summary>
        [IJEField(53, 280, 1, "Decedent's Race--Other Asian", "RACE10")]
        public string RACE10
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Decedent's Race--Native Hawaiian</summary>
        [IJEField(54, 281, 1, "Decedent's Race--Native Hawaiian", "RACE11")]
        public string RACE11
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Decedent's Race--Guamanian or Chamorro</summary>
        [IJEField(55, 282, 1, "Decedent's Race--Guamanian or Chamorro", "RACE12")]
        public string RACE12
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Decedent's Race--Samoan</summary>
        [IJEField(56, 283, 1, "Decedent's Race--Samoan", "RACE13")]
        public string RACE13
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Decedent's Race--Other Pacific Islander</summary>
        [IJEField(57, 284, 1, "Decedent's Race--Other Pacific Islander", "RACE14")]
        public string RACE14
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Decedent's Race--Other</summary>
        [IJEField(58, 285, 1, "Decedent's Race--Other", "RACE15")]
        public string RACE15
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Occupation -- Literal (OPTIONAL)</summary>
        [IJEField(84, 575, 40, "Occupation -- Literal (OPTIONAL)", "OCCUP")]
        public string OCCUP
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Industry -- Literal (OPTIONAL)</summary>
        [IJEField(86, 618, 40, "Industry -- Literal (OPTIONAL)", "INDUST")]
        public string INDUST
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Manner of Death</summary>
        [IJEField(99, 701, 1, "Manner of Death", "MANNER")]
        public string MANNER
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Was Autopsy performed</summary>
        [IJEField(108, 976, 1, "Was Autopsy performed", "AUTOP")]
        public string AUTOP
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Were Autopsy Findings Available to Complete the Cause of Death?</summary>
        [IJEField(109, 977, 1, "Were Autopsy Findings Available to Complete the Cause of Death?", "AUTOPF")]
        public string AUTOPF
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Did Tobacco Use Contribute to Death?</summary>
        [IJEField(110, 978, 1, "Did Tobacco Use Contribute to Death?", "TOBAC")]
        public string TOBAC
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Pregnancy</summary>
        [IJEField(111, 979, 1, "Pregnancy", "PREG")]
        public string PREG
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Date of injury--month</summary>
        [IJEField(113, 981, 2, "Date of injury--month", "DOI_MO")]
        public string DOI_MO
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Date of injury--day</summary>
        [IJEField(114, 983, 2, "Date of injury--day", "DOI_DY")]
        public string DOI_DY
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Date of injury--year</summary>
        [IJEField(115, 985, 4, "Date of injury--year", "DOI_YR")]
        public string DOI_YR
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Time of injury</summary>
        [IJEField(116, 989, 4, "Time of injury", "TOI_HR")]
        public string TOI_HR
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Injury at work</summary>
        [IJEField(117, 993, 1, "Injury at work", "WORKINJ")]
        public string WORKINJ
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Title of Certifier</summary>
        [IJEField(118, 994, 30, "Title of Certifier", "CERTL")]
        public string CERTL
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Time of Injury Unit</summary>
        [IJEField(125, 1075, 1, "Time of Injury Unit", "TOI_UNIT")]
        public string TOI_UNIT
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Decedent ever served in Armed Forces?</summary>
        [IJEField(127, 1081, 1, "Decedent ever served in Armed Forces?", "ARMEDF")]
        public string ARMEDF
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Death Institution name</summary>
        [IJEField(128, 1082, 30, "Death Institution name", "DINSTI")]
        public string DINSTI
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Long String address for place of death</summary>
        [IJEField(129, 1112, 50, "Long String address for place of death", "ADDRESS_D")]
        public string ADDRESS_D
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Place of death. Street number</summary>
        [IJEField(130, 1162, 10, "Place of death. Street number", "STNUM_D")]
        public string STNUM_D
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Place of death. Pre Directional</summary>
        [IJEField(131, 1172, 10, "Place of death. Pre Directional", "PREDIR_D")]
        public string PREDIR_D
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Place of death. Street name</summary>
        [IJEField(132, 1182, 50, "Place of death. Street name", "STNAME_D")]
        public string STNAME_D
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Place of death. Street designator</summary>
        [IJEField(133, 1232, 10, "Place of death. Street designator", "STDESIG_D")]
        public string STDESIG_D
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Place of death. Post Directional</summary>
        [IJEField(134, 1242, 10, "Place of death. Post Directional", "POSTDIR_D")]
        public string POSTDIR_D
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Place of death. City or Town name</summary>
        [IJEField(135, 1252, 28, "Place of death. City or Town name", "CITYTEXT_D")]
        public string CITYTEXT_D
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Place of death. State name literal</summary>
        [IJEField(136, 1280, 28, "Place of death. State name literal", "STATETEXT_D")]
        public string STATETEXT_D
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Place of death. Zip code</summary>
        [IJEField(137, 1308, 9, "Place of death. Zip code", "ZIP9_D")]
        public string ZIP9_D
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Place of death. County of Death</summary>
        [IJEField(138, 1317, 28, "Place of death. County of Death", "COUNTYTEXT_D")]
        public string COUNTYTEXT_D
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Place of death. City FIPS code</summary>
        [IJEField(139, 1345, 5, "Place of death. City FIPS code", "CITYCODE_D")]
        public string CITYCODE_D
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Place of death. Longitude</summary>
        [IJEField(140, 1350, 17, "Place of death. Longitude", "LONG_D")]
        public string LONG_D
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Place of Death. Latitude</summary>
        [IJEField(141, 1367, 17, "Place of Death. Latitude", "LAT_D")]
        public string LAT_D
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Spouse's First Name</summary>
        [IJEField(143, 1385, 50, "Spouse's First Name", "SPOUSEF")]
        public string SPOUSEF
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Husband's Surname/Wife's Maiden Last Name</summary>
        [IJEField(144, 1435, 50, "Husband's Surname/Wife's Maiden Last Name", "SPOUSEL ")]
        public string SPOUSEL
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Decedent's Residence - Street number</summary>
        [IJEField(145, 1485, 10, "Decedent's Residence - Street number", "STNUM_R")]
        public string STNUM_R
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Decedent's Residence - Pre Directional</summary>
        [IJEField(146, 1495, 10, "Decedent's Residence - Pre Directional", "PREDIR_R")]
        public string PREDIR_R
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Decedent's Residence - Street name</summary>
        [IJEField(147, 1505, 28, "Decedent's Residence - Street name", "STNAME_R")]
        public string STNAME_R
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Decedent's Residence - Street designator</summary>
        [IJEField(148, 1533, 10, "Decedent's Residence - Street designator", "STDESIG_R")]
        public string STDESIG_R
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Decedent's Residence - Post Directional</summary>
        [IJEField(149, 1543, 10, "Decedent's Residence - Post Directional", "POSTDIR_R")]
        public string POSTDIR_R
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Decedent's Residence - Unit or apt number</summary>
        [IJEField(150, 1553, 7, "Decedent's Residence - Unit or apt number", "UNITNUM_R")]
        public string UNITNUM_R
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Decedent's Residence - City or Town name</summary>
        [IJEField(151, 1560, 28, "Decedent's Residence - City or Town name", "CITYTEXT_R")]
        public string CITYTEXT_R
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Decedent's Residence - ZIP code</summary>
        [IJEField(152, 1588, 9, "Decedent's Residence - ZIP code", "ZIP9_R")]
        public string ZIP9_R
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Decedent's Residence - County</summary>
        [IJEField(153, 1597, 28, "Decedent's Residence - County", "COUNTYTEXT_R")]
        public string COUNTYTEXT_R
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Decedent's Residence - State name</summary>
        [IJEField(154, 1625, 28, "Decedent's Residence - State name", "STATETEXT_R ")]
        public string STATETEXT_R
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Decedent's Residence - COUNTRY name</summary>
        [IJEField(155, 1653, 28, "Decedent's Residence - COUNTRY name", "COUNTRYTEXT_R")]
        public string COUNTRYTEXT_R
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Long string address for decedent's place of residence same as above but allows states to choose the way they capture information.</summary>
        [IJEField(156, 1681, 50, "Long string address for decedent's place of residence same as above but allows states to choose the way they capture information.", "ADDRESS_R")]
        public string ADDRESS_R
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Hispanic Origin - Specify </summary>
        [IJEField(163, 1743, 15, "Hispanic Origin - Specify ", "HISPSTSP")]
        public string HISPSTSP
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Race - Specify</summary>
        [IJEField(164, 1758, 50, "Race - Specify", "RACESTSP")]
        public string RACESTSP
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Middle Name of Decedent </summary>
        [IJEField(165, 1808, 50, "Middle Name of Decedent ", "DMIDDLE")]
        public string DMIDDLE
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Father's First Name</summary>
        [IJEField(166, 1858, 50, "Father's First Name", "DDADF")]
        public string DDADF
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Father's Middle Name</summary>
        [IJEField(167, 1908, 50, "Father's Middle Name", "DDADMID")]
        public string DDADMID
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Mother's First Name</summary>
        [IJEField(168, 1958, 50, "Mother's First Name", "DMOMF")]
        public string DMOMF
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Mother's Middle Name</summary>
        [IJEField(169, 2008, 50, "Mother's Middle Name", "DMOMMID")]
        public string DMOMMID
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Mother's Maiden Surname</summary>
        [IJEField(170, 2058, 50, "Mother's Maiden Surname", "DMOMMDN")]
        public string DMOMMDN
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Was case Referred to Medical Examiner/Coroner?</summary>
        [IJEField(171, 2108, 1, "Was case Referred to Medical Examiner/Coroner?", "REFERRED")]
        public string REFERRED
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Place of Injury- literal</summary>
        [IJEField(172, 2109, 50, "Place of Injury- literal", "POILITRL")]
        public string POILITRL
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Describe How Injury Occurred</summary>
        [IJEField(173, 2159, 250, "Describe How Injury Occurred", "HOWINJ")]
        public string HOWINJ
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>If Transportation Accident, Specify</summary>
        [IJEField(174, 2409, 30, "If Transportation Accident, Specify", "TRANSPRT")]
        public string TRANSPRT
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>County of Injury - literal</summary>
        [IJEField(175, 2439, 28, "County of Injury - literal", "COUNTYTEXT_I")]
        public string COUNTYTEXT_I
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>County of Injury code</summary>
        [IJEField(176, 2467, 3, "County of Injury code", "COUNTYCODE_I")]
        public string COUNTYCODE_I
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Town/city of Injury - literal</summary>
        [IJEField(177, 2470, 28, "Town/city of Injury - literal", "CITYTEXT_I")]
        public string CITYTEXT_I
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Town/city of Injury code</summary>
        [IJEField(178, 2498, 5, "Town/city of Injury code", "CITYCODE_I")]
        public string CITYCODE_I
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>State, U.S. Territory or Canadian Province of Injury - code</summary>
        [IJEField(179, 2503, 2, "State, U.S. Territory or Canadian Province of Injury - code", "STATECODE_I")]
        public string STATECODE_I
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Place of injury. Longitude</summary>
        [IJEField(180, 2505, 17, "Place of injury. Longitude", "LONG_I")]
        public string LONG_I
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Place of injury. Latitude</summary>
        [IJEField(181, 2522, 17, "Place of injury. Latitude", "LAT_I")]
        public string LAT_I
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Cause of Death Part I Line a</summary>
        [IJEField(184, 2542, 120, "Cause of Death Part I Line a", "COD1A")]
        public string COD1A
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Cause of Death Part I Interval, Line a</summary>
        [IJEField(185, 2662, 20, "Cause of Death Part I Interval, Line a", "INTERVAL1A")]
        public string INTERVAL1A
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Cause of Death Part I Line b</summary>
        [IJEField(186, 2682, 120, "Cause of Death Part I Line b", "COD1B")]
        public string COD1B
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Cause of Death Part I Interval, Line b</summary>
        [IJEField(187, 2802, 20, "Cause of Death Part I Interval, Line b", "INTERVAL1B")]
        public string INTERVAL1B
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Cause of Death Part I Line c</summary>
        [IJEField(188, 2822, 120, "Cause of Death Part I Line c", "COD1C")]
        public string COD1C
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Cause of Death Part I Interval, Line c</summary>
        [IJEField(189, 2942, 20, "Cause of Death Part I Interval, Line c", "INTERVAL1C")]
        public string INTERVAL1C
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Cause of Death Part I Line d</summary>
        [IJEField(190, 2962, 120, "Cause of Death Part I Line d", "COD1D")]
        public string COD1D
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Cause of Death Part I Interval, Line d</summary>
        [IJEField(191, 3082, 20, "Cause of Death Part I Interval, Line d", "INTERVAL1D")]
        public string INTERVAL1D
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Cause of Death Part II</summary>
        [IJEField(192, 3102, 240, "Cause of Death Part II", "OTHERCONDITION")]
        public string OTHERCONDITION
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Decedent's Maiden Name</summary>
        [IJEField(193, 3342, 50, "Decedent's Maiden Name", "DMAIDEN")]
        public string DMAIDEN
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Decedent's Birth Place City - Code</summary>
        [IJEField(194, 3392, 5, "Decedent's Birth Place City - Code", "DBPLACECITYCODE")]
        public string DBPLACECITYCODE
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Decedent's Birth Place City - Literal</summary>
        [IJEField(195, 3397, 28, "Decedent's Birth Place City - Literal", "DBPLACECITY")]
        public string DBPLACECITY
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Spouse's Middle Name</summary>
        [IJEField(196, 3425, 50, "Spouse's Middle Name", "SPOUSEMIDNAME")]
        public string SPOUSEMIDNAME
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Spouse's Suffix</summary>
        [IJEField(197, 3475, 10, "Spouse's Suffix", "SPOUSESUFFIX")]
        public string SPOUSESUFFIX
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Father's Suffix</summary>
        [IJEField(198, 3485, 10, "Father's Suffix", "FATHERSUFFIX")]
        public string FATHERSUFFIX
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Mother's Suffix</summary>
        [IJEField(199, 3495, 10, "Mother's Suffix", "MOTHERSSUFFIX")]
        public string MOTHERSSUFFIX
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Informant's Relationship</summary>
        [IJEField(200, 3505, 30, "Informant's Relationship", "INFORMRELATE")]
        public string INFORMRELATE
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>State, U.S. Territory or Canadian Province of Disposition - code</summary>
        [IJEField(201, 3535, 2, "State, U.S. Territory or Canadian Province of Disposition - code", "DISPSTATECD")]
        public string DISPSTATECD
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Disposition State or Territory - Literal</summary>
        [IJEField(202, 3537, 28, "Disposition State or Territory - Literal", "DISPSTATE")]
        public string DISPSTATE
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Disposition City - Code</summary>
        [IJEField(203, 3565, 5, "Disposition City - Code", "DISPCITYCODE")]
        public string DISPCITYCODE
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Disposition City - Literal</summary>
        [IJEField(204, 3570, 28, "Disposition City - Literal", "DISPCITY")]
        public string DISPCITY
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Funeral Facility Name</summary>
        [IJEField(205, 3598, 100, "Funeral Facility Name", "FUNFACNAME")]
        public string FUNFACNAME
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Funeral Facility - Street number</summary>
        [IJEField(206, 3698, 10, "Funeral Facility - Street number", "FUNFACSTNUM")]
        public string FUNFACSTNUM
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Funeral Facility - Pre Directional</summary>
        [IJEField(207, 3708, 10, "Funeral Facility - Pre Directional", "FUNFACPREDIR")]
        public string FUNFACPREDIR
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Funeral Facility - Street name</summary>
        [IJEField(208, 3718, 28, "Funeral Facility - Street name", "FUNFACSTRNAME")]
        public string FUNFACSTRNAME
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Funeral Facility - Street designator</summary>
        [IJEField(209, 3746, 10, "Funeral Facility - Street designator", "FUNFACSTRDESIG")]
        public string FUNFACSTRDESIG
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Funeral Facility - Post Directional</summary>
        [IJEField(210, 3756, 10, "Funeral Facility - Post Directional", "FUNPOSTDIR")]
        public string FUNPOSTDIR
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Funeral Facility - Unit or apt number</summary>
        [IJEField(211, 3766, 7, "Funeral Facility - Unit or apt number", "FUNUNITNUM")]
        public string FUNUNITNUM
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Long string address for Funeral Facility same as above but allows states to choose the way they capture information.</summary>
        [IJEField(212, 3773, 50, "Long string address for Funeral Facility same as above but allows states to choose the way they capture information.", "FUNFACADDRESS")]
        public string FUNFACADDRESS
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Funeral Facility - City or Town name</summary>
        [IJEField(213, 3823, 28, "Funeral Facility - City or Town name", "FUNCITYTEXT")]
        public string FUNCITYTEXT
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>State, U.S. Territory or Canadian Province of Funeral Facility - code</summary>
        [IJEField(214, 3851, 2, "State, U.S. Territory or Canadian Province of Funeral Facility - code", "FUNSTATECD")]
        public string FUNSTATECD
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>State, U.S. Territory or Canadian Province of Funeral Facility - literal</summary>
        [IJEField(215, 3853, 28, "State, U.S. Territory or Canadian Province of Funeral Facility - literal", "FUNSTATE")]
        public string FUNSTATE
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Funeral Facility - ZIP</summary>
        [IJEField(216, 3881, 9, "Funeral Facility - ZIP", "FUNZIP")]
        public string FUNZIP
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Person Pronouncing Date Signed</summary>
        [IJEField(217, 3890, 8, "Person Pronouncing Date Signed", "PPDATESIGNED")]
        public string PPDATESIGNED
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Person Pronouncing Time Pronounced</summary>
        [IJEField(218, 3898, 4, "Person Pronouncing Time Pronounced", "PPTIME")]
        public string PPTIME
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Certifier's First Name</summary>
        [IJEField(219, 3902, 50, "Certifier's First Name", "CERTFIRST")]
        public string CERTFIRST
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Certifier's Middle Name</summary>
        [IJEField(220, 3952, 50, "Certifier's Middle Name", "CERTMIDDLE")]
        public string CERTMIDDLE
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Certifier's Last Name</summary>
        [IJEField(221, 4002, 50, "Certifier's Last Name", "CERTLAST")]
        public string CERTLAST
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Certifier's Suffix Name</summary>
        [IJEField(222, 4052, 10, "Certifier's Suffix Name", "CERTSUFFIX")]
        public string CERTSUFFIX
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Certifier - Street number</summary>
        [IJEField(223, 4062, 10, "Certifier - Street number", "CERTSTNUM")]
        public string CERTSTNUM
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Certifier - Pre Directional</summary>
        [IJEField(224, 4072, 10, "Certifier - Pre Directional", "CERTPREDIR")]
        public string CERTPREDIR
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Certifier - Street name</summary>
        [IJEField(225, 4082, 28, "Certifier - Street name", "CERTSTRNAME")]
        public string CERTSTRNAME
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Certifier - Street designator</summary>
        [IJEField(226, 4110, 10, "Certifier - Street designator", "CERTSTRDESIG")]
        public string CERTSTRDESIG
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Certifier - Post Directional</summary>
        [IJEField(227, 4120, 10, "Certifier - Post Directional", "CERTPOSTDIR")]
        public string CERTPOSTDIR
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Certifier - Unit or apt number</summary>
        [IJEField(228, 4130, 7, "Certifier - Unit or apt number", "CERTUNITNUM")]
        public string CERTUNITNUM
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Long string address for Certifier same as above but allows states to choose the way they capture information.</summary>
        [IJEField(229, 4137, 50, "Long string address for Certifier same as above but allows states to choose the way they capture information.", "CERTADDRESS")]
        public string CERTADDRESS
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Certifier - City or Town name</summary>
        [IJEField(230, 4187, 28, "Certifier - City or Town name", "CERTCITYTEXT")]
        public string CERTCITYTEXT
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>State, U.S. Territory or Canadian Province of Certifier - code</summary>
        [IJEField(231, 4215, 2, "State, U.S. Territory or Canadian Province of Certifier - code", "CERTSTATECD")]
        public string CERTSTATECD
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>State, U.S. Territory or Canadian Province of Certifier - literal</summary>
        [IJEField(232, 4217, 28, "State, U.S. Territory or Canadian Province of Certifier - literal", "CERTSTATE")]
        public string CERTSTATE
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Certifier - Zip</summary>
        [IJEField(233, 4245, 9, "Certifier - Zip", "CERTZIP")]
        public string CERTZIP
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Certifier Date Signed</summary>
        [IJEField(234, 4254, 8, "Certifier Date Signed", "CERTDATE")]
        public string CERTDATE
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>State, U.S. Territory or Canadian Province of Injury - literal</summary>
        [IJEField(236, 4270, 28, "State, U.S. Territory or Canadian Province of Injury - literal", "STINJURY")]
        public string STINJURY
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>State, U.S. Territory or Canadian Province of Birth - literal</summary>
        [IJEField(237, 4298, 28, "State, U.S. Territory or Canadian Province of Birth - literal", "STATEBTH")]
        public string STATEBTH
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Country of Death - Code</summary>
        [IJEField(238, 4326, 2, "Country of Death - Code", "DTHCOUNTRYCD")]
        public string DTHCOUNTRYCD
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

        // <summary>Country of Death - Literal</summary>
        [IJEField(239, 4328, 28, "Country of Death - Literal", "DTHCOUNTRY")]
        public string DTHCOUNTRY
        {
            get
            {
                return "TODO";
            }
            set
            {
                // TODO
            }
        }

    }
}
