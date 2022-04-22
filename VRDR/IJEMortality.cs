using System;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Collections.Generic;

namespace VRDR
{
    /// <summary>Property attribute used to describe a field in the IJE Mortality format.</summary>
    [System.AttributeUsage(System.AttributeTargets.Property)]
    public class IJEField : System.Attribute
    {
        /// <summary>Field number.</summary>
        public int Field;

        /// <summary>Beginning location.</summary>
        public int Location;

        /// <summary>Field length.</summary>
        public int Length;

        /// <summary>Description of what the field contains.</summary>
        public string Contents;

        /// <summary>Field name.</summary>
        public string Name;

        /// <summary>Priority - lower will be "GET" and "SET" earlier.</summary>
        public int Priority;

        /// <summary>Constructor.</summary>
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
    /// on the embedded FHIR based <c>DeathRecord</c>.</summary>
    public class IJEMortality
    {
        /// <summary>FHIR based death record.</summary>
        private DeathRecord record;

        /// <summary>IJE data lookup helper. Thread-safe singleton!</summary>
        private MortalityData dataLookup = MortalityData.Instance;

        /// <summary>Validation errors encountered while converting a record</summary>
        private List<string> validationErrors = new List<string>();

        /// <summary>Constructor that takes a <c>DeathRecord</c>.</summary>
        public IJEMortality(DeathRecord record, bool validate = true)
        {
            this.record = record;
            if (validate)
            {
                // We need to force a conversion to happen by calling ToString() if we want to validate
                ToString();
                if (validationErrors.Count > 0)
                {
                    string errorString = $"Found {validationErrors.Count} validation errors:\n{String.Join("\n", validationErrors)}";
                    throw new ArgumentOutOfRangeException(errorString);
                }
            }
        }

        /// <summary>Constructor that takes an IJE string and builds a corresponding internal <c>DeathRecord</c>.</summary>
        public IJEMortality(string ije, bool validate = true)
        {
            if (ije == null)
            {
                throw new ArgumentException("IJE string cannot be null.");
            }
            if (ije.Length < 5000)
            {
                ije = ije.PadRight(5000, ' ');
            }
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
            if (validate && validationErrors.Count > 0)
            {
                string errorString = $"Found {validationErrors.Count} validation errors:\n{String.Join("\n", validationErrors)}";
                throw new ArgumentOutOfRangeException(errorString);
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
                ije.Remove(info.Location - 1, field.Length);
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

        /// <summary>Truncates the given string to the given length.</summary>
        private static string Truncate(string value, int length)
        {
            if (String.IsNullOrWhiteSpace(value) || value.Length <= length)
            {
                return value;
            }
            else
            {
                return value.Substring(0, length);
            }
        }

        /// <summary>Grabs the IJEInfo for a specific IJE field name.</summary>
        private static IJEField FieldInfo(string ijeFieldName)
        {
            return (IJEField)typeof(IJEMortality).GetProperty(ijeFieldName).GetCustomAttributes().First();
        }

        /// <summary>Helps decompose a DateTime into individual parts (year, month, day, time).</summary>
        private string DateTimeStringHelper(IJEField info, string value, string type, DateTimeOffset date, bool dateOnly = false, bool withTimezoneOffset = false)
        {
            if (type == "yyyy")
            {
                if (value == null || value.Length < 4)
                {
                    return "";
                }
                int year;
                if (Int32.TryParse(Truncate(value, info.Length), out year))
                {
                    date = new DateTimeOffset(year, date.Month, date.Day, date.Hour, date.Minute, date.Second, date.Millisecond, TimeSpan.Zero);
                }
            }
            else if (type == "MM")
            {
                if (value == null || value.Length < 2)
                {
                    return "";
                }
                int month;
                if (Int32.TryParse(Truncate(value, info.Length), out month))
                {
                    date = new DateTimeOffset(date.Year, month, date.Day, date.Hour, date.Minute, date.Second, date.Millisecond, TimeSpan.Zero);
                }
            }
            else if (type == "dd")
            {
                if (value == null || value.Length < 2)
                {
                    return "";
                }
                int day;
                if (Int32.TryParse(Truncate(value, info.Length), out day))
                {
                    date = new DateTimeOffset(date.Year, date.Month, day, date.Hour, date.Minute, date.Second, date.Millisecond, TimeSpan.Zero);
                }
            }
            else if (type == "HHmm")
            {
                if (value == null || value.Length < 4)
                {
                    return "";
                }
                int hour;
                if (Int32.TryParse(Truncate(value, info.Length).Substring(0, 2), out hour))
                {
                    // Treat 99 as blank
                    if (hour != 99)
                    {
                        date = new DateTimeOffset(date.Year, date.Month, date.Day, hour, date.Minute, date.Second, date.Millisecond, TimeSpan.Zero);
                    }
                }
                int minute;
                if (Int32.TryParse(Truncate(value, info.Length).Substring(2, 2), out minute))
                {
                    // Treat 99 as blank
                    if (minute != 99)
                    {
                        date = new DateTimeOffset(date.Year, date.Month, date.Day, date.Hour, minute, date.Second, date.Millisecond, TimeSpan.Zero);
                    }
                }
            }
            else if (type == "MMddyyyy")
            {
                if (value == null || value.Length < 8)
                {
                    return "";
                }
                int month;
                if (Int32.TryParse(Truncate(value, info.Length).Substring(0, 2), out month))
                {
                    // Treat 99 as blank
                    if (month != 99)
                    {
                        date = new DateTimeOffset(date.Year, month, date.Day, date.Hour, date.Minute, date.Second, date.Millisecond, TimeSpan.Zero);
                    }
                }
                int day;
                if (Int32.TryParse(Truncate(value, info.Length).Substring(2, 2), out day))
                {
                    // Treat 99 as blank
                    if (day != 99)
                    {
                        date = new DateTimeOffset(date.Year, date.Month, day, date.Hour, date.Minute, date.Second, date.Millisecond, TimeSpan.Zero);
                    }
                }
                int year;
                if (Int32.TryParse(Truncate(value, info.Length).Substring(4, 4), out year))
                {
                    // Treat 9999 as blank
                    if (year != 9999)
                    {
                        date = new DateTimeOffset(year, date.Month, date.Day, date.Hour, date.Minute, date.Second, date.Millisecond, TimeSpan.Zero);
                    }
                }
            }
            if (dateOnly)
            {
                return date == null ? null : date.ToString("yyyy-MM-dd");
            }
            else if (withTimezoneOffset)
            {
                return date == null ? null : date.ToString("o");
            }
            else
            {
                return date == null ? null : date.ToString("s");
            }
        }

        /// <summary>Get a value on the DeathRecord whose type is some part of a DateTime.</summary>
        private string DateTime_Get(string ijeFieldName, string dateTimeType, string fhirFieldName)
        {
            IJEField info = FieldInfo(ijeFieldName);
            DateTimeOffset date;
            string current = this.record == null ? null : Convert.ToString(typeof(DeathRecord).GetProperty(fhirFieldName).GetValue(this.record));
            if (DateTimeOffset.TryParse(current, out date))
            {
                date = date.ToUniversalTime();
                date = new DateTimeOffset(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, date.Millisecond, TimeSpan.Zero);
                return Truncate(date.ToString(dateTimeType), info.Length);
            }
            else
            {
                return new String(' ', info.Length);
            }
        }

        /// <summary>Get the date part value from DateOfBirthDatePartAbsent if it's populated</summary>
        private string BirthDate_Part_Absent_Get(string ijeFieldName, string dateType, string dateAbsentType)
        {
            IJEField info = FieldInfo(ijeFieldName);
            Tuple<string, string>[] dpa = this.record?.DateOfBirthDatePartAbsent;
            if (dpa != null) {
                List<Tuple<string, string>> dateParts = dpa.ToList();
                Tuple<string, string> datePart = dateParts.Find(x => x.Item1 == dateType);
                if (datePart != null){
                    return datePart.Item2.PadLeft(info.Length, '0');
                }
                Tuple<string, string> datePartAbsent = dateParts.Find(x => x.Item1 == dateAbsentType);
                if (datePartAbsent != null){
                    return string.Concat(Enumerable.Repeat("9", info.Length));
                }
            }
            return "";
        }

        /// <summary>Get the date part value from DateOfDeathDatePartAbsent if it's populated</summary>
        private string DeathDate_Part_Absent_Get(string ijeFieldName, string dateType, string dateAbsentType)
        {
            IJEField info = FieldInfo(ijeFieldName);
            Tuple<string, string>[] dpa = this.record?.DateOfDeathDatePartAbsent;
            if (dpa != null) {
                List<Tuple<string, string>> dateParts = dpa.ToList();
                Tuple<string, string> datePart = dateParts.Find(x => x.Item1 == dateType);
                if (datePart != null){
                    return datePart.Item2.PadLeft(info.Length, '0');
                }
                Tuple<string, string> datePartAbsent = dateParts.Find(x => x.Item1 == dateAbsentType);
                if (datePartAbsent != null){
                    return string.Concat(Enumerable.Repeat("9", info.Length));
                }
            }

            return "";
        }


        /// <summary>Set a value on the DeathRecord whose type is some part of a DateTime.</summary>
        private void DateTime_Set(string ijeFieldName, string dateTimeType, string fhirFieldName, string value, bool dateOnly = false, bool withTimezoneOffset = false)
        {
            IJEField info = FieldInfo(ijeFieldName);
            string current = Convert.ToString(typeof(DeathRecord).GetProperty(fhirFieldName).GetValue(this.record));
            DateTimeOffset date;
            if (current != null && DateTimeOffset.TryParse(current, out date))
            {
                date = date.ToUniversalTime();
                date = new DateTimeOffset(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, date.Millisecond, TimeSpan.Zero);
                typeof(DeathRecord).GetProperty(fhirFieldName).SetValue(this.record, DateTimeStringHelper(info, value, dateTimeType, date, dateOnly, withTimezoneOffset));
            }
            else
            {
                date = new DateTimeOffset(1, 1, 1, 0, 0, 0, 0, TimeSpan.Zero);
                typeof(DeathRecord).GetProperty(fhirFieldName).SetValue(this.record, DateTimeStringHelper(info, value, dateTimeType, date, dateOnly, withTimezoneOffset));
            }
        }

        /// <summary>Get a value on the DeathRecord whose IJE type is a left justified string.</summary>
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

        /// <summary>Set a value on the DeathRecord whose IJE type is a right justified, zeroed filled string.</summary>
        private void RightJustifiedZeroed_Set(string ijeFieldName, string fhirFieldName, string value)
        {
            IJEField info = FieldInfo(ijeFieldName);
            typeof(DeathRecord).GetProperty(fhirFieldName).SetValue(this.record, value.TrimStart('0'));
        }

        /// <summary>Get a value on the DeathRecord whose IJE type is a left justified string.</summary>
        private string LeftJustified_Get(string ijeFieldName, string fhirFieldName)
        {
            IJEField info = FieldInfo(ijeFieldName);
            string current = Convert.ToString(typeof(DeathRecord).GetProperty(fhirFieldName).GetValue(this.record));
            if (current != null)
            {
                if (current.Length > info.Length)
                {
                    validationErrors.Add($"Error: FHIR field {fhirFieldName} containst string '{current}' too long for IJE field {ijeFieldName} of length {info.Length}");
                }
                return Truncate(current, info.Length).PadRight(info.Length, ' ');
            }
            else
            {
                return new String(' ', info.Length);
            }
        }

        /// <summary>Set a value on the DeathRecord whose IJE type is a left justified string.</summary>
        private void LeftJustified_Set(string ijeFieldName, string fhirFieldName, string value)
        {
            if (!String.IsNullOrWhiteSpace(value))
            {
                IJEField info = FieldInfo(ijeFieldName);
                typeof(DeathRecord).GetProperty(fhirFieldName).SetValue(this.record, value.Trim());
            }
        }

        /// <summary>Get a value on the DeathRecord whose property is a Dictionary type.</summary>
        private string Dictionary_Get(string ijeFieldName, string fhirFieldName, string key)
        {
            IJEField info = FieldInfo(ijeFieldName);
            Dictionary<string, string> dictionary = (Dictionary<string, string>)typeof(DeathRecord).GetProperty(fhirFieldName).GetValue(this.record);
            if (dictionary == null || !dictionary.ContainsKey(key))
            {
                return "";
            }
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

        /// <summary>Get a value on the DeathRecord whose property is a Dictionary type, with NO truncating.</summary>
        private string Dictionary_Get_Full(string ijeFieldName, string fhirFieldName, string key)
        {
            IJEField info = FieldInfo(ijeFieldName);
            Dictionary<string, string> dictionary = (Dictionary<string, string>)typeof(DeathRecord).GetProperty(fhirFieldName).GetValue(this.record);
            if (dictionary != null && dictionary.ContainsKey(key))
            {
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
            return "";
        }

        /// <summary>Set a value on the DeathRecord whose property is a Dictionary type.</summary>
        private void Dictionary_Set(string ijeFieldName, string fhirFieldName, string key, string value)
        {
            IJEField info = FieldInfo(ijeFieldName);
            Dictionary<string, string> dictionary = (Dictionary<string, string>)typeof(DeathRecord).GetProperty(fhirFieldName).GetValue(this.record);
            if (dictionary != null && (!dictionary.ContainsKey(key) || String.IsNullOrWhiteSpace(dictionary[key])))
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    dictionary[key] = value.Trim();
                }
            }
            else
            {
                dictionary = new Dictionary<string, string>();
                if (!String.IsNullOrWhiteSpace(value))
                {
                    dictionary[key] = value.Trim();
                }
            }
            typeof(DeathRecord).GetProperty(fhirFieldName).SetValue(this.record, dictionary);
        }

        /// <summary>Get a value on the DeathRecord whose property is a geographic type (and is contained in a dictionary).</summary>
        private string Dictionary_Geo_Get(string ijeFieldName, string fhirFieldName, string keyPrefix, string geoType, bool isCoded)
        {
            IJEField info = FieldInfo(ijeFieldName);
            Dictionary<string, string> dictionary = this.record == null ? null : (Dictionary<string, string>)typeof(DeathRecord).GetProperty(fhirFieldName).GetValue(this.record);
            string key = keyPrefix + char.ToUpper(geoType[0]) + geoType.Substring(1);
            if (dictionary == null || !dictionary.ContainsKey(key))
            {
                return new String(' ', info.Length);
            }
            string current = Convert.ToString(dictionary[key]);
            if (isCoded)
            {
                if (geoType == "place") //|| geoType == "city")
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
                else if (geoType == "state")
                {
                    //current = dataLookup.StateNameToStateCode(current);
                }
                else if (geoType == "country")
                {
                    //current = dataLookup.CountryNameToCountryCode(current);
                }
                else if (geoType == "insideCityLimits")
                {
                    if (String.IsNullOrWhiteSpace(current))
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
                return Truncate(current.Replace("-", string.Empty), info.Length).PadRight(info.Length, ' '); // Remove "-" for zip
            }
            else
            {
                return new String(' ', info.Length);
            }
        }

        /// <summary>Set a value on the DeathRecord whose property is a geographic type (and is contained in a dictionary).</summary>
        private void Dictionary_Geo_Set(string ijeFieldName, string fhirFieldName, string keyPrefix, string geoType, bool isCoded, string value)
        {
            IJEField info = FieldInfo(ijeFieldName);
            Dictionary<string, string> dictionary = (Dictionary<string, string>)typeof(DeathRecord).GetProperty(fhirFieldName).GetValue(this.record);
            string key = keyPrefix + char.ToUpper(geoType[0]) + geoType.Substring(1);
            if (dictionary != null && (!dictionary.ContainsKey(key) || String.IsNullOrWhiteSpace(dictionary[key])))
            {
                if (isCoded)
                {
                    // v1.3 removed lookups for city
                    if (geoType == "place") // This is a tricky case, we need to know about county and state!
                    {
                        string state = null;
                        string county = null;
                        dictionary.TryGetValue(keyPrefix + "State", out state);
                        dictionary.TryGetValue(keyPrefix + "County", out county);
                        if (!String.IsNullOrWhiteSpace(state) && !String.IsNullOrWhiteSpace(county))
                        {
                            string city = dataLookup.StateNameAndCountyNameAndPlaceCodeToPlaceName(state, county, value);
                            if (!String.IsNullOrWhiteSpace(city))
                            {
                                dictionary[key] = city;
                            }
                        }
                    }
                    else if (geoType == "state" || geoType == "country")
                    {
                        dictionary[key] = value;
                    }
                    else if (geoType == "insideCityLimits")
                    {
                        if (!String.IsNullOrWhiteSpace(value) && value == "N")
                        {
                            dictionary[key] = "False";
                        }
                    }
                    else
                    {
                        dictionary[key] = value.Trim();
                    }
                }
                else
                {
                    dictionary[key] = value.Trim();
                }
            }
            else
            {
                dictionary = new Dictionary<string, string>();
                dictionary[key] = value.Trim();
            }
            typeof(DeathRecord).GetProperty(fhirFieldName).SetValue(this.record, dictionary);
        }

        /// <summary>Checks if the given race exists in the record.</summary>
        private string Get_Race(string name)
        {
            Tuple<string, string>[] raceStatus = record.Race.ToArray();

            Tuple<string, string> raceTuple = Array.Find(raceStatus, element => element.Item1 == name);
            if (raceTuple != null)
            {
                return raceTuple.Item2;
            }
            return "";
        }

        /// <summary>Adds the given race to the record.</summary>
        private void Set_Race(string name, string value)
        {
            List<Tuple<string, string>> raceStatus = record.Race.ToList();
            raceStatus.Add(Tuple.Create(name, value));
            record.Race = raceStatus.Distinct().ToArray();
        }

        /// <summary>Checks if the state local identifier is in the record.</summary>
        private string Get_StateLocalIdentifier(string ijeFieldName, string extensionUrl)
        {
            IJEField info = FieldInfo(ijeFieldName);

            Tuple<string, string>[] stateIds = record.StateLocalIdentifier;
            Tuple<string, string> id = Array.Find(stateIds, element => element.Item1 == extensionUrl);
            if (id != null)
            {
                return Truncate(id.Item2, info.Length).PadLeft(info.Length, '0');
            }
            return new String('0', info.Length);
        }

        /// <summary>Adds the given state local identifier to the record.</summary>
        private void Set_StateLocalIdentifier(string ijeFieldName, string extensionUrl, string value)
        {
            IJEField info = FieldInfo(ijeFieldName);
            List<Tuple<string, string>> stateIds = record.StateLocalIdentifier.ToList();
            stateIds.Add(Tuple.Create(extensionUrl, value.TrimStart('0')));
            record.StateLocalIdentifier = stateIds.Distinct().ToArray();
        }

        // /// <summary>Gets a "Yes", "No", or "Unknown" value.</summary>
        // private string Get_YNU(string fhirFieldName)
        // {
        //     object status = typeof(DeathRecord).GetProperty(fhirFieldName).GetValue(this.record);
        //     if (status == null)
        //     {
        //         return "U";
        //     }
        //     else
        //     {
        //         return ((bool)status) ? "Y" : "N";
        //     }
        // }

        // /// <summary>Sets a "Yes", "No", or "Unkown" value.</summary>
        // private void Set_YNU(string fhirFieldName, string value)
        // {
        //     if (value != "U" && value == "Y")
        //     {
        //         typeof(DeathRecord).GetProperty(fhirFieldName).SetValue(this.record, true);
        //     }
        //     else if (value != "U" && value == "N")
        //     {
        //         typeof(DeathRecord).GetProperty(fhirFieldName).SetValue(this.record, false);
        //     }
        // }

        /// <summary>Given a Dictionary mapping FHIR codes to IJE strings and the relevant FHIR and IJE fields pull the value
        /// from the FHIR record object and provide the appropriate IJE string</summary>
        /// <param name="mapping">Dictionary for mapping the desired concept from FHIR to IJE; these dictionaries are defined in Mappings.cs</param>
        /// <param name="fhirField">Name of the FHIR field to get from the record; must have a related Helper property, e.g., EducationLevel must have EducationLevelHelper</param>
        /// <param name="ijeField">Name of the IJE field that the FHIR field content is being placed into</param>
        /// <returns>The IJE value of the field translated from the FHIR value on the record</returns>
        private string Get_MappingFHIRToIJE(Dictionary<string,string> mapping, string fhirField, string ijeField)
        {
            PropertyInfo helperProperty = typeof(DeathRecord).GetProperty($"{fhirField}Helper");
            if (helperProperty == null)
            {
                throw new NullReferenceException($"No helper method found called '{fhirField}Helper'");
            }
            string fhirCode = (string)helperProperty.GetValue(this.record);
            if (String.IsNullOrWhiteSpace(fhirCode))
            {
                return "";
            }
            try
            {
                return mapping[fhirCode];
            }
            catch (KeyNotFoundException)
            {
                validationErrors.Add($"Error: Unable to find IJE {ijeField} mapping for FHIR {fhirField} field value '{fhirCode}'");
                return "";
             }

        }

        /// <summary>Given a Dictionary mapping IJE codes to FHIR strings and the relevant IJE and FHIR fields translate the IJE
        /// string to the appropriate FHIR code and set the value on the FHIR record object</summary>
        /// <param name="mapping">Dictionary for mapping the desired concept from IJE to FHIR; these dictionaries are defined in Mappings.cs</param>
        /// <param name="ijeField">Name of the IJE field that the FHIR field content is being set from</param>
        /// <param name="fhirField">Name of the FHIR field to set on the record; must have a related Helper property, e.g., EducationLevel must have EducationLevelHelper</param>
        /// <param name="value">The value to translate from IJE to FHIR and set on the record</param>
        private void Set_MappingIJEToFHIR(Dictionary<string,string> mapping, string ijeField, string fhirField, string value)
        {
            if (!String.IsNullOrWhiteSpace(value))
            {
                try
                {
                    PropertyInfo helperProperty = typeof(DeathRecord).GetProperty($"{fhirField}Helper");
                    if (helperProperty == null)
                    {
                        throw new NullReferenceException($"No helper method found called '{fhirField}Helper'");
                    }
                    helperProperty.SetValue(this.record, mapping[value]);
                }
                catch (KeyNotFoundException)
                {
                    validationErrors.Add($"Error: Unable to find FHIR {fhirField} mapping for IJE {ijeField} field value '{value}'");
                }
            }
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

        /// <summary>Date of Death--Year</summary>
        [IJEField(1, 1, 4, "Date of Death--Year", "DOD_YR", 1)]
        public string DOD_YR
        {
            // REFER TO UPDATES IN DOB_XX FOR IGv1.3
            get
            {
                String yearPart = DeathDate_Part_Absent_Get("DOD_YR", "date-year", "year-absent-reason");
                if (!String.IsNullOrWhiteSpace(yearPart)){
                    return yearPart;
                }
                return DateTime_Get("DOD_YR", "yyyy", "DateOfDeath");

            }
            set
            {
                if (String.IsNullOrWhiteSpace(value) || String.Equals(value, "9999"))
                {
                    List<Tuple<string, string>> dateParts = record.DateOfDeathDatePartAbsent.ToList();
                    dateParts.Add(Tuple.Create("year-absent-reason", "unknown"));
                    dateParts.RemoveAll(x => x.Item1 == "date-year");
                    record.DateOfDeathDatePartAbsent = dateParts.ToList().ToArray();
                }
                DateTime_Set("DOD_YR", "yyyy", "DateOfDeath", value, false, true);
            }
        }

        /// <summary>State, U.S. Territory or Canadian Province of Death - code</summary>
        [IJEField(2, 5, 2, "State, U.S. Territory or Canadian Province of Death - code", "DSTATE", 1)]
        public string DSTATE
        {
            get
            {
                string value = LeftJustified_Get("DSTATE", "DeathLocationJurisdiction");
                if (dataLookup.JurisdictionNameToJurisdictionCode(value) == null)
                {
                    validationErrors.Add("DSTATE value of " + value + " is invalid.");
                }
                return value;
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    LeftJustified_Set("DSTATE", "DeathLocationJurisdiction",value);
                    // Dictionary_Set("STATEC", "DeathLocationAddress", "addressState", value); // WHY????... used to be STATEC
                }
            }
        }

        /// <summary>Certificate Number</summary>
        [IJEField(3, 7, 6, "Certificate Number", "FILENO", 1)]
        public string FILENO
        {
            get
            {
                if (String.IsNullOrWhiteSpace(record?.Identifier))
                {
                    return "".PadLeft(6, '0');
                }
                string id_str = record.Identifier;
                if (id_str.Length > 6)
                {
                    id_str = id_str.Substring(id_str.Length - 6);
                }
                return id_str.PadLeft(6, '0');
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    RightJustifiedZeroed_Set("FILENO", "Identifier", value);
                }
            }
        }

        /// <summary>Void flag</summary>
        [IJEField(4, 13, 1, "Void flag", "VOID", 1)]
        public string VOID
        {
            get
            {
                return "0";
            }
            set
            {
                // NOOP
            }
        }

        /// <summary>Auxiliary State file number</summary>
        [IJEField(5, 14, 12, "Auxiliary State file number", "AUXNO", 1)]
        public string AUXNO
        {
            get
            {
                return Get_StateLocalIdentifier("AUXNO", ExtensionURL.AuxiliaryStateIdentifier1);
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {

                    Set_StateLocalIdentifier("AUXNO", ExtensionURL.AuxiliaryStateIdentifier1, value);
                }
            }
        }

        /// <summary>Source flag: paper/electronic</summary>
        [IJEField(6, 26, 1, "Source flag: paper/electronic", "MFILED", 1)]
        public string MFILED
        {
            get
            {
                return Get_MappingFHIRToIJE(Mappings.FilingFormat.FHIRToIJE, "FilingFormat", "MFILED");
            }
            set
            {
                Set_MappingIJEToFHIR(Mappings.FilingFormat.IJEToFHIR, "MFILED", "FilingFormat", value);
            }
        }

        /// <summary>Decedent's Legal Name--Given</summary>
        [IJEField(7, 27, 50, "Decedent's Legal Name--Given", "GNAME", 1)]
        public string GNAME
        {
            get
            {
                string[] names = record.GivenNames;
                if (names.Length > 0)
                {
                    return names[0];
                }
                return "";
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    record.GivenNames = new string[] { value.Trim() };
                }
            }
        }

        /// <summary>Decedent's Legal Name--Middle</summary>
        [IJEField(8, 77, 1, "Decedent's Legal Name--Middle", "MNAME", 3)]
        public string MNAME
        {
            get
            {
                string[] names = record.GivenNames;
                if (names.Length > 1)
                {
                    return names[1];
                }
                return "";
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    if (String.IsNullOrWhiteSpace(DMIDDLE))
                    {
                        if (record.GivenNames != null)
                        {
                            List<string> names = record.GivenNames.ToList();
                            names.Add(value.Trim());
                            record.GivenNames = names.ToArray();
                        }
                    }
                }
            }
        }

        /// <summary>Decedent's Legal Name--Last</summary>
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

        /// <summary>Decedent's Legal Name--Suffix</summary>
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

        /// <summary>Decedent's Legal Name--Alias</summary>
        [IJEField(11, 138, 1, "Decedent's Legal Name--Alias", "ALIAS", 1)]
        public string ALIAS
        {
            get
            {
                return "0";
            }
            set
            {
                // NOOP
            }
        }

        /// <summary>Father's Surname</summary>
        [IJEField(12, 139, 50, "Father's Surname", "FLNAME", 1)]
        public string FLNAME
        {
            get
            {
                return LeftJustified_Get("FLNAME", "FatherFamilyName");
            }
            set
            {
                LeftJustified_Set("FLNAME", "FatherFamilyName", value);
            }
        }

        /// <summary>Sex</summary>
        [IJEField(13, 189, 1, "Sex", "SEX", 1)]
        public string SEX
        {
            get
            {
                return Get_MappingFHIRToIJE(Mappings.AdministrativeGender.FHIRToIJE, "SexAtDeath", "SEX");
            }
            set
            {
                Set_MappingIJEToFHIR(Mappings.AdministrativeGender.IJEToFHIR, "SEX", "SexAtDeath", value);
            }
        }

        /// <summary>Sex--Edit Flag</summary>
        [IJEField(14, 190, 1, "Sex--Edit Flag", "SEX_BYPASS", 1)]
        public string SEX_BYPASS
        {
            get
            {
                return ""; // Blank
            }
            set
            {
                // NOOP
            }
        }

        /// <summary>Social Security Number</summary>
        [IJEField(15, 191, 9, "Social Security Number", "SSN", 1)]
        public string SSN
        {
            get
            {
                string ssn = record.SSN;
                if (!String.IsNullOrWhiteSpace(ssn))
                {
                    return ssn.Replace("-", string.Empty);
                }
                else
                {
                    return "";
                }
            }
            set
            {
                LeftJustified_Set("SSN", "SSN", value);
            }
        }

        /// <summary>Decedent's Age--Type</summary>
        [IJEField(16, 200, 1, "Decedent's Age--Type", "AGETYPE", 1)]
        public string AGETYPE
        {
            get
            {
                if (record.AgeAtDeath != null && !String.IsNullOrWhiteSpace(record.AgeAtDeath["unit"]) && !record.AgeAtDeathDataAbsentBoolean)
                {
                    switch (record.AgeAtDeath["unit"].ToLower().Trim())
                    {
                        case "a":
                            return "1";
                        case "d":
                            return "4";
                        case "h":
                            return "5";
                        case "min":
                            return "6";
                        case "mo":
                            return "2";
                        case "unk":
                            return "9";
                        case "wk":
                            return "3";
                    }
                }
                return "9";
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Dictionary<string, string> ageAtDeath = new Dictionary<string, string>();
                    switch (value.Trim())
                    {
                        case "1":
                            ageAtDeath["unit"] = "a";
                            break;
                        case "4":
                            ageAtDeath["unit"] = "d";
                            break;
                        case "5":
                            ageAtDeath["unit"] = "h";
                            break;
                        case "6":
                            ageAtDeath["unit"] = "min";
                            break;
                        case "2":
                            ageAtDeath["unit"] = "mo";
                            break;
                        case "9":
                            ageAtDeath["unit"] = "unk";
                            break;
                        case "3":
                            ageAtDeath["unit"] = "wk";
                            break;
                    }
                    record.AgeAtDeath = ageAtDeath;
                }
            }
        }

        /// <summary>Decedent's Age--Units</summary>
        [IJEField(17, 201, 3, "Decedent's Age--Units", "AGE", 2)]
        public string AGE
        {
            get
            {
                if ((record.AgeAtDeath != null) && this.AGETYPE != "9")
                {
                    IJEField info = FieldInfo("AGE");
                    return Truncate(record.AgeAtDeath["value"], info.Length).PadLeft(info.Length, '0');
                }
                else
                {  // record.AgeAtDeath["value"] is not defined
                    return "999";
                }
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value) && value != "999")
                {
                    Dictionary<string, string> ageAtDeath = record.AgeAtDeath;
                    ageAtDeath["value"] = value.TrimStart('0');
                    record.AgeAtDeath = ageAtDeath;
                    ;
                }
                else
                {
                    record.AgeAtDeathDataAbsentBoolean = true;
                }
            }
        }

        /// <summary>Decedent's Age--Edit Flag</summary>
        [IJEField(18, 204, 1, "Decedent's Age--Edit Flag", "AGE_BYPASS", 1)]
        public string AGE_BYPASS
        {
            get
            {
                return ""; // Blank
            }
            set
            {
                // NOOP
            }
        }

                /// <summary>Date of Birth--Year</summary>
        [IJEField(19, 205, 4, "Date of Birth--Year", "DOB_YR", 1)]
        public string DOB_YR
        {
            get
            {
                String yearPart = BirthDate_Part_Absent_Get("DOB_YR", "date-year", "year-absent-reason");
                if (!String.IsNullOrWhiteSpace(yearPart))
                {
                    return yearPart;
                }
                return DateTime_Get("DOB_YR", "yyyy", "DateOfBirth");
            }
            set
            {
                // if unknown, set the date part absent
                if (String.IsNullOrWhiteSpace(value) || String.Equals(value, "9999"))
                {
                    List<Tuple<string, string>> dateParts = new List<Tuple<string, string>>();
                    if (record.DateOfBirthDatePartAbsent != null){
                        dateParts = record.DateOfBirthDatePartAbsent.ToList();
                    }
                    dateParts.Add(Tuple.Create("year-absent-reason", "unknown"));
                    record.DateOfBirthDatePartAbsent = dateParts.ToList().ToArray();
                } else
                {
                    // we will still set this for now so we can reference the value to
                    // populate the Date Part Absent field
                    // In FHIR we will just ignore this
                    DateTime_Set("DOB_YR", "yyyy", "DateOfBirth", value, true);
                }
            }
        }

        /// <summary>Date of Birth--Month</summary>
        [IJEField(20, 209, 2, "Date of Birth--Month", "DOB_MO", 1)]
        public string DOB_MO
        {
            get
            {
                String monthPart = BirthDate_Part_Absent_Get("DOB_MO", "date-month", "month-absent-reason");
                if (!String.IsNullOrWhiteSpace(monthPart))
                {
                    return monthPart;
                }
                return DateTime_Get("DOB_MO", "MM", "DateOfBirth");
            }
            set
            {
                // if unknown, set the date part absent
                if (String.IsNullOrWhiteSpace(value) || String.Equals(value, "99"))
                {
                    List<Tuple<string, string>> dateParts = new List<Tuple<string, string>>();
                    if (record.DateOfBirthDatePartAbsent != null){
                        dateParts = record.DateOfBirthDatePartAbsent.ToList();
                    }
                    dateParts.Add(Tuple.Create("month-absent-reason", "unknown"));
                    record.DateOfBirthDatePartAbsent = dateParts.ToList().ToArray();
                } else
                {
                    DateTime_Set("DOB_MO", "MM", "DateOfBirth", value, true);
                }
            }
        }

        /// <summary>Date of Birth--Day</summary>
        [IJEField(21, 211, 2, "Date of Birth--Day", "DOB_DY", 1)]
        public string DOB_DY
        {
            get
            {
                String dayPart = BirthDate_Part_Absent_Get("DOB_DY", "date-day", "day-absent-reason");
                if (!String.IsNullOrWhiteSpace(dayPart)){
                    return dayPart;
                }
                return DateTime_Get("DOB_DY", "dd", "DateOfBirth");

            }
            set
            {
                if (String.Equals(DOB_YR, "9999") || String.Equals(DOB_MO, "99") || String.Equals(value, "99") || String.IsNullOrWhiteSpace(value))
                {
                    List<Tuple<string, string>> dateParts = record.DateOfBirthDatePartAbsent.ToList();
                    switch (value)
                    {
                        case "99":
                            dateParts.Add(Tuple.Create("day-absent-reason", "unknown"));
                            dateParts.RemoveAll(x => x.Item1 == "date-day");
                            break;
                        default:
                            DateTime_Set("DOB_DY", "dd", "DateOfBirth", value, true);
                            dateParts.Add(Tuple.Create("date-day", value));
                            dateParts.RemoveAll(x => x.Item1 == "day-absent-reason");
                            break;
                    }
                    switch (DOB_MO)
                    {
                        case "99":
                            dateParts.RemoveAll(x => x.Item1 == "date-month");
                            break;
                        default:
                            dateParts.Add(Tuple.Create("date-month", DOB_MO));
                            dateParts.RemoveAll(x => x.Item1 == "month-absent-reason");
                            break;
                    }
                    switch (DOB_YR)
                    {
                        case "9999":
                            dateParts.RemoveAll(x => x.Item1 == "date-year");
                            break;
                        default:
                            dateParts.Add(Tuple.Create("date-year", DOB_YR));
                            dateParts.RemoveAll(x => x.Item1 == "year-absent-reason");
                            break;
                    }
                    record.DateOfBirthDatePartAbsent = dateParts.ToList().ToArray();
                    // TODO should we set DateOfBirth to null because it will have default values for the unknown date parts?
                    // record.DateOfBirth = "";
                }
                else
                {
                    DateTime_Set("DOB_DY", "dd", "DateOfBirth", value, true);
                }
            }
        }

        /// <summary>Birthplace--Country</summary>
        [IJEField(22, 213, 2, "Birthplace--Country", "BPLACE_CNT", 1)]
        public string BPLACE_CNT
        {
            get
            {
                return Dictionary_Geo_Get("BPLACE_CNT", "PlaceOfBirth", "address", "country", true);
               }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Dictionary_Set("BPLACE_CNT", "PlaceOfBirth", "addressCountry", value);
                }
            }
        }

        /// <summary>State, U.S. Territory or Canadian Province of Birth - code</summary>
        [IJEField(23, 215, 2, "State, U.S. Territory or Canadian Province of Birth - code", "BPLACE_ST", 1)]
        public string BPLACE_ST
        {
            get
            {
                return Dictionary_Geo_Get("BPLACE_ST", "PlaceOfBirth", "address", "state", true);
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Dictionary_Geo_Set("BPLACE_ST", "PlaceOfBirth", "address", "state", true, value);
                }
            }
        }

        /// <summary>Decedent's Residence--City</summary>
        [IJEField(24, 217, 5, "Decedent's Residence--City", "CITYC", 3)]
        public string CITYC
        {
            get
            {
                return Dictionary_Geo_Get("CITYC", "Residence", "address", "cityC", true);
            }
            set
            {
                // NOOP
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Dictionary_Geo_Set("CITYC", "Residence", "address", "cityC", true, value);
                }
            }
        }

        /// <summary>Decedent's Residence--County</summary>
        [IJEField(25, 222, 3, "Decedent's Residence--County", "COUNTYC", 2)]
        public string COUNTYC
        {
            get
            {
                return Dictionary_Geo_Get("COUNTYC", "Residence", "address", "countyC", true);
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Dictionary_Geo_Set("COUNTYC", "Residence", "address", "countyC", true, value);
                }
            }
        }

        /// <summary>State, U.S. Territory or Canadian Province of Decedent's residence - code</summary>
        [IJEField(26, 225, 2, "State, U.S. Territory or Canadian Province of Decedent's residence - code", "STATEC", 1)]
        public string STATEC
        {
            get
            {
                return Dictionary_Geo_Get("STATEC", "Residence", "address", "State", true);
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Dictionary_Set("STATEC", "Residence", "addressState", value);
                }
            }
        }

        /// <summary>Decedent's Residence--Country</summary>
        [IJEField(27, 227, 2, "Decedent's Residence--Country", "COUNTRYC", 1)]
        public string COUNTRYC
        {
            get
            {
                return Dictionary_Geo_Get("COUNTRYC", "Residence", "address", "country", true); // NVSS-234 -- use 2 letter encoding for country, so no translation.
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Dictionary_Geo_Set("COUNTRYC", "Residence", "address", "country", true, value); // NVSS-234 -- use 2 letter encoding for country, so no translation.
                }
            }
        }

        /// <summary>Decedent's Residence--Inside City Limits</summary>
        [IJEField(28, 229, 1, "Decedent's Residence--Inside City Limits", "LIMITS", 10)]
        public string LIMITS
        {
            get
            {
                return Get_MappingFHIRToIJE(Mappings.YesNoUnknown.FHIRToIJE, "ResidenceWithinCityLimits", "LIMITS");
            }
            set
            {
                Set_MappingIJEToFHIR(Mappings.YesNoUnknown.IJEToFHIR, "LIMITS", "ResidenceWithinCityLimits", value);
            }
        }

        /// <summary>Marital Status</summary>
        [IJEField(29, 230, 1, "Marital Status", "MARITAL", 1)]
        public string MARITAL
        {
            get
            {
                return Get_MappingFHIRToIJE(Mappings.MaritalStatus.FHIRToIJE, "MaritalStatus", "MARITAL");
            }
            set
            {
                Set_MappingIJEToFHIR(Mappings.MaritalStatus.IJEToFHIR, "MARITAL", "MaritalStatus", value);
            }
        }

        /// <summary>Marital Status--Edit Flag</summary>
        [IJEField(30, 231, 1, "Marital Status--Edit Flag", "MARITAL_BYPASS", 1)]
        public string MARITAL_BYPASS
        {
            get
            {
                return Get_MappingFHIRToIJE(Mappings.EditBypass0124.FHIRToIJE, "MaritalBypass", "MARITAL_BYPASS");
            }
            set
            {
                Set_MappingIJEToFHIR(Mappings.EditBypass0124.IJEToFHIR, "MARITAL_BYPASS", "MaritalBypass", value);
            }
        }

        /// <summary>Marital Status--Edit Flag</summary>
        [IJEField(31, 232, 1, "Place of Death", "DPLACE", 1)]
        public string DPLACE
        {
            get
            {
                return Get_MappingFHIRToIJE(Mappings.PlaceOfDeath.FHIRToIJE, "DeathLocationType", "DPLACE");
            }
            set
            {
                Set_MappingIJEToFHIR(Mappings.PlaceOfDeath.IJEToFHIR, "DPLACE", "DeathLocationType", value);
            }
        }

        /// <summary>County of Death Occurrence</summary>
        [IJEField(32, 233, 3, "County of Death Occurrence", "COD", 2)]
        public string COD
        {
            get
            {
                return Dictionary_Geo_Get("COD", "DeathLocationAddress", "address", "county", true);
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Dictionary_Geo_Set("COD", "DeathLocationAddress", "address", "county", true, value);
                }
            }
        }

        /// <summary>Method of Disposition</summary>
        [IJEField(33, 236, 1, "Method of Disposition", "DISP", 1)]
        public string DISP
        {
            get
            {
                return Get_MappingFHIRToIJE(Mappings.MethodOfDisposition.FHIRToIJE, "DecedentDispositionMethod", "DISP");
            }
            set
            {
                Set_MappingIJEToFHIR(Mappings.MethodOfDisposition.IJEToFHIR, "DISP", "DecedentDispositionMethod", value);
            }
        }

        /// <summary>Date of Death--Month</summary>
        [IJEField(34, 237, 2, "Date of Death--Month", "DOD_MO", 1)]
        public string DOD_MO
        {
            // REFER TO UPDATES IN DOB_XX FOR IGv1.3
            get
            {

                String monthPart = DeathDate_Part_Absent_Get("DOD_MO", "date-month", "month-absent-reason");
                if (!String.IsNullOrWhiteSpace(monthPart)){
                    return monthPart;
                }
                return DateTime_Get("DOD_MO", "MM", "DateOfDeath");

            }
            set
            {
                // if unknown, create a date part absent
                if (String.IsNullOrWhiteSpace(value) || String.Equals(value, "99"))
                {
                    List<Tuple<string, string>> dateParts = record.DateOfDeathDatePartAbsent.ToList();
                    dateParts.Add(Tuple.Create("month-absent-reason", "unknown"));
                    dateParts.RemoveAll(x => x.Item1 == "date-month");
                    record.DateOfDeathDatePartAbsent = dateParts.ToList().ToArray();
                }
                DateTime_Set("DOD_MO", "MM", "DateOfDeath", value, false, true);
            }
        }

        /// <summary>Date of Death--Day</summary>
        [IJEField(35, 239, 2, "Date of Death--Day", "DOD_DY", 1)]
        public string DOD_DY
        // REFER TO UPDATES IN DOB_XX FOR IGv1.3
        {
            get
            {
                String dayPart = DeathDate_Part_Absent_Get("DOD_DY", "date-day", "day-absent-reason");
                if (!String.IsNullOrWhiteSpace(dayPart)){
                    return dayPart;
                }
                return DateTime_Get("DOD_DY", "dd", "DateOfDeath");

            }
            set
            {
                // Populate the date absent parts if any of the date parts are unknown
                // Doing all three parts at once in the last date part field
                // because the other fields hadn't populated the Date Part field yet
                // and only one would ever get added to the record
                if (String.Equals(DOD_YR, "9999") || String.Equals(DOD_MO, "99") || String.Equals(value, "99") || String.IsNullOrWhiteSpace(value))
                {
                    List<Tuple<string, string>> dateParts = record.DateOfDeathDatePartAbsent.ToList();
                    switch (value)
                    {
                        case "99":
                            dateParts.Add(Tuple.Create("day-absent-reason", "unknown"));
                            dateParts.RemoveAll(x => x.Item1 == "date-day");
                            break;
                        default:
                            dateParts.Add(Tuple.Create("date-day", value));
                            dateParts.RemoveAll(x => x.Item1 == "day-absent-reason");
                            break;
                    }
                    switch (DOD_MO)
                    {
                        case "99":
                            dateParts.RemoveAll(x => x.Item1 == "date-month");
                            break;
                        default:
                            dateParts.Add(Tuple.Create("date-month", DOB_MO));
                            dateParts.RemoveAll(x => x.Item1 == "month-absent-reason");
                            break;
                    }
                    switch (DOD_YR)
                    {
                        case "9999":
                            dateParts.RemoveAll(x => x.Item1 == "date-year");
                            break;
                        default:
                            dateParts.Add(Tuple.Create("date-year", DOB_YR));
                            dateParts.RemoveAll(x => x.Item1 == "year-absent-reason");
                            break;
                    }
                    record.DateOfDeathDatePartAbsent = dateParts.ToList().ToArray();
                }
                else
                {
                    DateTime_Set("DOD_DY", "dd", "DateOfDeath", value, false, true);
                }
            }
        }

        /// <summary>Time of Death</summary>
        [IJEField(36, 241, 4, "Time of Death", "TOD", 1)]
        public string TOD
        {
            get
            {
                return DateTime_Get("TOD", "HHmm", "DateOfDeath");
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    DateTime_Set("TOD", "HHmm", "DateOfDeath", value, false, true);
                }
            }
        }

        /// <summary>Decedent's Education</summary>
        [IJEField(37, 245, 1, "Decedent's Education", "DEDUC", 1)]
        public string DEDUC
        {
            get
            {
                return Get_MappingFHIRToIJE(Mappings.EducationLevel.FHIRToIJE, "EducationLevel", "DEDUC");
            }
            set
            {
                Set_MappingIJEToFHIR(Mappings.EducationLevel.IJEToFHIR, "DEDUC", "EducationLevel", value);
            }
        }

        /// <summary>Decedent's Education--Edit Flag</summary>
        [IJEField(38, 246, 1, "Decedent's Education--Edit Flag", "DEDUC_BYPASS", 1)]
        public string DEDUC_BYPASS
        {
            get
            {
                return Get_MappingFHIRToIJE(Mappings.EditBypass01234.FHIRToIJE, "EducationLevelEditFlag", "DEDUC_BYPASS");
            }
            set
            {
                Set_MappingIJEToFHIR(Mappings.EditBypass01234.IJEToFHIR, "DEDUC_BYPASS", "EducationLevelEditFlag", value);
            }
        }

        // The DETHNIC functions handle unknown ethnicity as follows
        // All of the DETHNIC fields have to be unknown, U, for the ethnicity json data to be empty ex. UUUU
            // Individual "Unknown" DETHNIC fields cannot be preserved in a roundtrip, only UUUU will return UUUU
        // If at least one DETHNIC field is H, the json data should show Hispanic or Latino ex. NNHN will return NNHN
        // If at least one DETHNIC field is N and no fields are H, the json data should show Non-Hispanic or Latino ex. UUNU will return NNNN
        /// <summary>Decedent of Hispanic Origin?--Mexican</summary>
        [IJEField(39, 247, 1, "Decedent of Hispanic Origin?--Mexican", "DETHNIC1", 1)]
        public string DETHNIC1
        {
            get
            {
                string code = Get_MappingFHIRToIJE(Mappings.YesNoUnknown.FHIRToIJE, "Ethnicity1", "DETHNIC1");
                if (code == "Y")
                {
                    code = "H";
                }
                if (String.IsNullOrWhiteSpace(code))
                {
                    code = "U";
                }
                return code;
            }
            set
            {
                if (value == "H"){
                    value = "Y";
                }
                Set_MappingIJEToFHIR(Mappings.YesNoUnknown.IJEToFHIR, "DETHNIC1", "Ethnicity1", value);
            }
        }

        /// <summary>Decedent of Hispanic Origin?--Puerto Rican</summary>
        [IJEField(40, 248, 1, "Decedent of Hispanic Origin?--Puerto Rican", "DETHNIC2", 1)]
        public string DETHNIC2
        {
            get
            {
                string code = Get_MappingFHIRToIJE(Mappings.YesNoUnknown.FHIRToIJE, "Ethnicity2", "DETHNIC2");
                if (code == "Y")
                {
                    code = "H";
                }
                if (String.IsNullOrWhiteSpace(code))
                {
                    code = "U";
                }
                return code;
            }
            set
            {
                if (value == "H"){
                    value = "Y";
                }
                Set_MappingIJEToFHIR(Mappings.YesNoUnknown.IJEToFHIR, "DETHNIC2", "Ethnicity2", value);
            }
        }

        /// <summary>Decedent of Hispanic Origin?--Cuban</summary>
        [IJEField(41, 249, 1, "Decedent of Hispanic Origin?--Cuban", "DETHNIC3", 1)]
        public string DETHNIC3
        {
            get
            {
                string code = Get_MappingFHIRToIJE(Mappings.YesNoUnknown.FHIRToIJE, "Ethnicity3", "DETHNIC3");
                if (code == "Y")
                {
                    code = "H";
                }
                if (String.IsNullOrWhiteSpace(code))
                {
                    code = "U";
                }
                return code;
            }
            set
            {
                if (value == "H"){
                    value = "Y";
                }
                Set_MappingIJEToFHIR(Mappings.YesNoUnknown.IJEToFHIR, "DETHNIC3", "Ethnicity3", value);
            }
        }

        /// <summary>Decedent of Hispanic Origin?--Other</summary>
        [IJEField(42, 250, 1, "Decedent of Hispanic Origin?--Other", "DETHNIC4", 1)]
        public string DETHNIC4
        {
            get
            {
                string code = Get_MappingFHIRToIJE(Mappings.YesNoUnknown.FHIRToIJE, "Ethnicity4", "DETHNIC4");
                if (code == "Y")
                {
                    code = "H";
                }
                if (String.IsNullOrWhiteSpace(code))
                {
                    code = "U";
                }
                return code;
            }
            set
            {
                if (value == "H"){
                    value = "Y";
                }
                Set_MappingIJEToFHIR(Mappings.YesNoUnknown.IJEToFHIR, "DETHNIC4", "Ethnicity4", value);
            }
        }

        /// <summary>Decedent of Hispanic Origin?--Other, Literal</summary>
        [IJEField(43, 251, 20, "Decedent of Hispanic Origin?--Other, Literal", "DETHNIC5", 1)]
        public string DETHNIC5
        {
            get
            {
                var ethnicityLiteral = record.EthnicityLiteral;
                if (!String.IsNullOrWhiteSpace(ethnicityLiteral))
                {
                    return Truncate(ethnicityLiteral, 20).Trim();
                }
                else
                {
                    return "";
                }
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    record.EthnicityLiteral = value;
                }
            }
        }

        /// <summary>Decedent's Race--White</summary>
        [IJEField(44, 271, 1, "Decedent's Race--White", "RACE1", 1)]
        public string RACE1
        {
            get
            {
                return Get_Race(NvssRace.White);
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Set_Race(NvssRace.White, value);
                }
            }
        }

        /// <summary>Decedent's Race--Black or African American</summary>
        [IJEField(45, 272, 1, "Decedent's Race--Black or African American", "RACE2", 1)]
        public string RACE2
        {
            get
            {
                return Get_Race(NvssRace.BlackOrAfricanAmerican);
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Set_Race(NvssRace.BlackOrAfricanAmerican, value);
                }
            }
        }

        /// <summary>Decedent's Race--American Indian or Alaska Native</summary>
        [IJEField(46, 273, 1, "Decedent's Race--American Indian or Alaska Native", "RACE3", 1)]
        public string RACE3
        {
            get
            {
                return Get_Race(NvssRace.AmericanIndianOrAlaskaNative);
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Set_Race(NvssRace.AmericanIndianOrAlaskaNative, value);
                }
            }
        }

        /// <summary>Decedent's Race--Asian Indian</summary>
        [IJEField(47, 274, 1, "Decedent's Race--Asian Indian", "RACE4", 1)]
        public string RACE4
        {
            get
            {
                return Get_Race(NvssRace.AsianIndian);
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Set_Race(NvssRace.AsianIndian, value);
                }
            }
        }

        /// <summary>Decedent's Race--Chinese</summary>
        [IJEField(48, 275, 1, "Decedent's Race--Chinese", "RACE5", 1)]
        public string RACE5
        {
            get
            {
                return Get_Race(NvssRace.Chinese);
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Set_Race(NvssRace.Chinese, value);
                }
            }
        }

        /// <summary>Decedent's Race--Filipino</summary>
        [IJEField(49, 276, 1, "Decedent's Race--Filipino", "RACE6", 1)]
        public string RACE6
        {
            get
            {
                return Get_Race(NvssRace.Filipino);
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Set_Race(NvssRace.Filipino, value);
                }
            }
        }

        /// <summary>Decedent's Race--Japanese</summary>
        [IJEField(50, 277, 1, "Decedent's Race--Japanese", "RACE7", 1)]
        public string RACE7
        {
            get
            {
                return Get_Race(NvssRace.Japanese);
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Set_Race(NvssRace.Japanese, value);
                }
            }
        }

        /// <summary>Decedent's Race--Korean</summary>
        [IJEField(51, 278, 1, "Decedent's Race--Korean", "RACE8", 1)]
        public string RACE8
        {
            get
            {
                return Get_Race(NvssRace.Korean);
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Set_Race(NvssRace.Korean, value);
                }
            }
        }

        /// <summary>Decedent's Race--Vietnamese</summary>
        [IJEField(52, 279, 1, "Decedent's Race--Vietnamese", "RACE9", 1)]
        public string RACE9
        {
            get
            {
                return Get_Race(NvssRace.Vietnamese);
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Set_Race(NvssRace.Vietnamese, value);
                }
            }
        }

        /// <summary>Decedent's Race--Other Asian</summary>
        [IJEField(53, 280, 1, "Decedent's Race--Other Asian", "RACE10", 1)]
        public string RACE10
        {
            get
            {
                return Get_Race(NvssRace.OtherAsian);
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Set_Race(NvssRace.OtherAsian, value);
                }

            }
        }

        /// <summary>Decedent's Race--Native Hawaiian</summary>
        [IJEField(54, 281, 1, "Decedent's Race--Native Hawaiian", "RACE11", 1)]
        public string RACE11
        {
            get
            {
                return Get_Race(NvssRace.NativeHawaiian);
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Set_Race(NvssRace.NativeHawaiian, value);
                }
            }
        }

        /// <summary>Decedent's Race--Guamanian or Chamorro</summary>
        [IJEField(55, 282, 1, "Decedent's Race--Guamanian or Chamorro", "RACE12", 1)]
        public string RACE12
        {
            get
            {
                return Get_Race(NvssRace.GuamanianOrChamorro);
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Set_Race(NvssRace.GuamanianOrChamorro, value);
                }
            }
        }

        /// <summary>Decedent's Race--Samoan</summary>
        [IJEField(56, 283, 1, "Decedent's Race--Samoan", "RACE13", 1)]
        public string RACE13
        {
            get
            {
                return Get_Race(NvssRace.Samoan);
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Set_Race(NvssRace.Samoan, value);
                }
            }
        }

        /// <summary>Decedent's Race--Other Pacific Islander</summary>
        [IJEField(57, 284, 1, "Decedent's Race--Other Pacific Islander", "RACE14", 1)]
        public string RACE14
        {
            get
            {
                return Get_Race(NvssRace.OtherPacificIslander);
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Set_Race(NvssRace.OtherPacificIslander, value);
                }
            }
        }

        /// <summary>Decedent's Race--Other</summary>
        [IJEField(58, 285, 1, "Decedent's Race--Other", "RACE15", 1)]
        public string RACE15
        {
            get
            {
                return Get_Race(NvssRace.OtherRace);
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Set_Race(NvssRace.OtherRace, value);
                }
            }
        }

        /// <summary>Decedent's Race--First American Indian or Alaska Native Literal</summary>
        [IJEField(59, 286, 30, "Decedent's Race--First American Indian or Alaska Native Literal", "RACE16", 1)]
        public string RACE16
        {
            get
            {
                return Get_Race(NvssRace.AmericanIndianOrAlaskanNativeLiteral1);
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Set_Race(NvssRace.AmericanIndianOrAlaskanNativeLiteral1, value);
                }
            }
        }

        /// <summary>Decedent's Race--Second American Indian or Alaska Native Literal</summary>
        [IJEField(60, 316, 30, "Decedent's Race--Second American Indian or Alaska Native Literal", "RACE17", 1)]
        public string RACE17
        {
            get
            {
                return Get_Race(NvssRace.AmericanIndianOrAlaskanNativeLiteral2);
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Set_Race(NvssRace.AmericanIndianOrAlaskanNativeLiteral2, value);
                }
            }
        }

        /// <summary>Decedent's Race--First Other Asian Literal</summary>
        [IJEField(61, 346, 30, "Decedent's Race--First Other Asian Literal", "RACE18", 1)]
        public string RACE18
        {
            get
            {
                return Get_Race(NvssRace.OtherAsianLiteral1);
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Set_Race(NvssRace.OtherAsianLiteral1, value);
                }
            }
        }

        /// <summary>Decedent's Race--Second Other Asian Literal</summary>
        [IJEField(62, 376, 30, "Decedent's Race--Second Other Asian Literal", "RACE19", 1)]
        public string RACE19
        {
            get
            {
                return Get_Race(NvssRace.OtherAsianLiteral2);
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Set_Race(NvssRace.OtherAsianLiteral2, value);
                }
            }
        }

        /// <summary>Decedent's Race--First Other Pacific Islander Literal</summary>
        [IJEField(63, 406, 30, "Decedent's Race--First Other Pacific Islander Literal", "RACE20", 1)]
        public string RACE20
        {
            get
            {
                return Get_Race(NvssRace.OtherPacificIslandLiteral1);
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Set_Race(NvssRace.OtherPacificIslandLiteral1, value);
                }
            }
        }

        /// <summary>Decedent's Race--Second Other Pacific Islander Literalr</summary>
        [IJEField(64, 436, 30, "Decedent's Race--Second Other Pacific Islander Literal", "RACE21", 1)]
        public string RACE21
        {
            get
            {
                return Get_Race(NvssRace.OtherPacificIslandLiteral2);
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Set_Race(NvssRace.OtherPacificIslandLiteral2, value);
                }
            }
        }

        /// <summary>Decedent's Race--First Other Literal</summary>
        [IJEField(65, 466, 30, "Decedent's Race--First Other Literal", "RACE22", 1)]
        public string RACE22
        {
            get
            {
                return Get_Race(NvssRace.OtherRaceLiteral1);
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Set_Race(NvssRace.OtherRaceLiteral1, value);
                }
            }
        }

        /// <summary>Decedent's Race--Second Other Literal</summary>
        [IJEField(66, 496, 30, "Decedent's Race--Second Other Literal", "RACE23", 1)]
        public string RACE23
        {
            get
            {
                return Get_Race(NvssRace.OtherRaceLiteral2);
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Set_Race(NvssRace.OtherRaceLiteral2, value);
                }
            }
        }

        /// <summary>First Edited Code</summary>
        [IJEField(67, 526, 3, "First Edited Code", "RACE1E", 1)]
        public string RACE1E
        {
            get
            {
                // TODO: Implement mapping from FHIR record location: CodedRaceAndEthnicity
                return "";
            }
            set
            {
                // TODO: Implement mapping to FHIR record location: CodedRaceAndEthnicity
            }
        }

        /// <summary>Second Edited Code</summary>
        [IJEField(68, 529, 3, "Second Edited Code", "RACE2E", 1)]
        public string RACE2E
        {
            get
            {
                // TODO: Implement mapping from FHIR record location: CodedRaceAndEthnicity
                return "";
            }
            set
            {
                // TODO: Implement mapping to FHIR record location: CodedRaceAndEthnicity
            }
        }

        /// <summary>Third Edited Code</summary>
        [IJEField(69, 532, 3, "Third Edited Code", "RACE3E", 1)]
        public string RACE3E
        {
            get
            {
                // TODO: Implement mapping from FHIR record location: CodedRaceAndEthnicity
                return "";
            }
            set
            {
                // TODO: Implement mapping to FHIR record location: CodedRaceAndEthnicity
            }
        }

        /// <summary>Fourth Edited Code</summary>
        [IJEField(70, 535, 3, "Fourth Edited Code", "RACE4E", 1)]
        public string RACE4E
        {
            get
            {
                // TODO: Implement mapping from FHIR record location: CodedRaceAndEthnicity
                return "";
            }
            set
            {
                // TODO: Implement mapping to FHIR record location: CodedRaceAndEthnicity
            }
        }

        /// <summary>Fifth Edited Code</summary>
        [IJEField(71, 538, 3, "Fifth Edited Code", "RACE5E", 1)]
        public string RACE5E
        {
            get
            {
                // TODO: Implement mapping from FHIR record location: CodedRaceAndEthnicity
                return "";
            }
            set
            {
                // TODO: Implement mapping to FHIR record location: CodedRaceAndEthnicity
            }
        }

        /// <summary>Sixth Edited Code</summary>
        [IJEField(72, 541, 3, "Sixth Edited Code", "RACE6E", 1)]
        public string RACE6E
        {
            get
            {
                // TODO: Implement mapping from FHIR record location: CodedRaceAndEthnicity
                return "";
            }
            set
            {
                // TODO: Implement mapping to FHIR record location: CodedRaceAndEthnicity
            }
        }

        /// <summary>Seventh Edited Code</summary>
        [IJEField(73, 544, 3, "Seventh Edited Code", "RACE7E", 1)]
        public string RACE7E
        {
            get
            {
                // TODO: Implement mapping from FHIR record location: CodedRaceAndEthnicity
                return "";
            }
            set
            {
                // TODO: Implement mapping to FHIR record location: CodedRaceAndEthnicity
            }
        }

        /// <summary>Eighth Edited Code</summary>
        [IJEField(74, 547, 3, "Eighth Edited Code", "RACE8E", 1)]
        public string RACE8E
        {
            get
            {
                // TODO: Implement mapping from FHIR record location: CodedRaceAndEthnicity
                return "";
            }
            set
            {
                // TODO: Implement mapping to FHIR record location: CodedRaceAndEthnicity
            }
        }

        /// <summary>First American Indian Code</summary>
        [IJEField(75, 550, 3, "First American Indian Code", "RACE16C", 1)]
        public string RACE16C
        {
            get
            {
                // TODO: Implement mapping from FHIR record location: CodedRaceAndEthnicity
                return "";
            }
            set
            {
                // TODO: Implement mapping to FHIR record location: CodedRaceAndEthnicity
            }
        }

        /// <summary>Second American Indian Code</summary>
        [IJEField(76, 553, 3, "Second American Indian Code", "RACE17C", 1)]
        public string RACE17C
        {
            get
            {
                // TODO: Implement mapping from FHIR record location: CodedRaceAndEthnicity
                return "";
            }
            set
            {
                // TODO: Implement mapping to FHIR record location: CodedRaceAndEthnicity
            }
        }

        /// <summary>First Other Asian Code</summary>
        [IJEField(77, 556, 3, "First Other Asian Code", "RACE18C", 1)]
        public string RACE18C
        {
            get
            {
                // TODO: Implement mapping from FHIR record location: CodedRaceAndEthnicity
                return "";
            }
            set
            {
                // TODO: Implement mapping to FHIR record location: CodedRaceAndEthnicity
            }
        }

        /// <summary>Second Other Asian Code</summary>
        [IJEField(78, 559, 3, "Second Other Asian Code", "RACE19C", 1)]
        public string RACE19C
        {
            get
            {
                // TODO: Implement mapping from FHIR record location: CodedRaceAndEthnicity
                return "";
            }
            set
            {
                // TODO: Implement mapping to FHIR record location: CodedRaceAndEthnicity
            }
        }

        /// <summary>First Other Pacific Islander Code</summary>
        [IJEField(79, 562, 3, "First Other Pacific Islander Code", "RACE20C", 1)]
        public string RACE20C
        {
            get
            {
                // TODO: Implement mapping from FHIR record location: CodedRaceAndEthnicity
                return "";
            }
            set
            {
                // TODO: Implement mapping to FHIR record location: CodedRaceAndEthnicity
            }
        }

        /// <summary>Second Other Pacific Islander Code</summary>
        [IJEField(80, 565, 3, "Second Other Pacific Islander Code", "RACE21C", 1)]
        public string RACE21C
        {
            get
            {
                // TODO: Implement mapping from FHIR record location: CodedRaceAndEthnicity
                return "";
            }
            set
            {
                // TODO: Implement mapping to FHIR record location: CodedRaceAndEthnicity
            }
        }

        /// <summary>First Other Race Code</summary>
        [IJEField(81, 568, 3, "First Other Race Code", "RACE22C", 1)]
        public string RACE22C
        {
            get
            {
                // TODO: Implement mapping from FHIR record location: CodedRaceAndEthnicity
                return "";
            }
            set
            {
                // TODO: Implement mapping to FHIR record location: CodedRaceAndEthnicity
            }
        }

        /// <summary>Second Other Race Code</summary>
        [IJEField(82, 571, 3, "Second Other Race Code", "RACE23C", 1)]
        public string RACE23C
        {
            get
            {
                // TODO: Implement mapping from FHIR record location: CodedRaceAndEthnicity
                return "";
            }
            set
            {
                // TODO: Implement mapping to FHIR record location: CodedRaceAndEthnicity
            }
        }

        /// <summary>Decedent's Race--Missing</summary>
        [IJEField(83, 574, 1, "Decedent's Race--Missing", "RACE_MVR", 1)]
        public string RACE_MVR
        {
            get
            {
                return Get_MappingFHIRToIJE(Mappings.RaceMissingValueReason.FHIRToIJE, "RaceMissingValueReason", "RACE_MVR");
            }
            set
            {
                Set_MappingIJEToFHIR(Mappings.RaceMissingValueReason.IJEToFHIR, "RACE_MVR", "RaceMissingValueReason", value);
            }
        }

        /// <summary>Occupation -- Literal (OPTIONAL)</summary>
        [IJEField(84, 575, 40, "Occupation -- Literal (OPTIONAL)", "OCCUP", 1)]
        public string OCCUP
        {
            get
            {
                return LeftJustified_Get("OCCUP", "UsualOccupation");
            }
            set
            {
                LeftJustified_Set("OCCUP", "UsualOccupation", value);
            }
        }

        /// <summary>Occupation -- Code</summary>
        [IJEField(85, 615, 3, "Occupation -- Code", "OCCUPC", 1)]
        public string OCCUPC
        {
            get
            {
                // NOTE: This is a placeholder, the IJE field OCCUPC is not currently implemented in FHIR
                return "";
            }
            set
            {
                // NOTE: This is a placeholder, the IJE field OCCUPC is not currently implemented in FHIR
            }
        }

        /// <summary>Industry -- Literal (OPTIONAL)</summary>
        [IJEField(86, 618, 40, "Industry -- Literal (OPTIONAL)", "INDUST", 1)]
        public string INDUST
        {
            get
            {
                return LeftJustified_Get("INDUST", "UsualIndustry");
            }
            set
            {
                LeftJustified_Set("INDUST", "UsualIndustry", value);
            }
        }

        /// <summary>Industry -- Code</summary>
        [IJEField(87, 658, 3, "Industry -- Code", "INDUSTC", 1)]
        public string INDUSTC
        {
            get
            {
                // NOTE: This is a placeholder, the IJE field INDUSTC is not currently implemented in FHIR
                return "";
            }
            set
            {
                // NOTE: This is a placeholder, the IJE field INDUSTC is not currently implemented in FHIR
            }
        }

        /// <summary>Infant Death/Birth Linking - birth certificate number</summary>
        [IJEField(88, 661, 6, "Infant Death/Birth Linking - birth certificate number", "BCNO", 1)]
        public string BCNO
        {
            get
            {
                if (String.IsNullOrWhiteSpace(record.BirthRecordId))
                {
                    return "";
                }
                string id_str = record.BirthRecordId;
                if (id_str.Length > 6)
                {
                    id_str = id_str.Substring(id_str.Length - 6);
                }
                return id_str.PadLeft(6, '0');
            }
            set
            {
                // if value is null, the library will add the data absent reason

                RightJustifiedZeroed_Set("BCNO", "BirthRecordId", value);

            }
        }

        /// <summary>Infant Death/Birth Linking - year of birth</summary>
        [IJEField(89, 667, 4, "Infant Death/Birth Linking - year of birth", "IDOB_YR", 1)]
        public string IDOB_YR
        {
            get
            {
                if (!String.IsNullOrWhiteSpace(BCNO))
                {
                    string dob = DateTime_Get("IDOB_YR", "yyyy", "DateOfBirth");
                    if (String.IsNullOrWhiteSpace(dob))
                    {
                        return "9999"; // Unknown
                    }
                    else
                    {
                        return dob;
                    }
                }
                return ""; // Blank
            }
            set
            {
                // NOOP
            }
        }

        /// <summary>Infant Death/Birth Linking - Birth state</summary>
        [IJEField(90, 671, 2, "Infant Death/Birth Linking - State, U.S. Territory or Canadian Province of Birth - code", "BSTATE", 1)]
        public string BSTATE
        {
            get
            {
                if (!String.IsNullOrWhiteSpace(BCNO))
                {
                    String state = Dictionary_Get_Full("BSTATE", "BirthRecordState", "code");
                    String retState;
                    // If the country is US or CA, strip the prefix
                    if (state.StartsWith("US-") || state.StartsWith("CA-"))
                    {
                        retState = state.Substring(3);
                    }
                    else
                    {
                        retState = state;
                    }
                    return retState;
                }
                return ""; // Blank
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    String birthCountry = BPLACE_CNT;
                    String ISO31662code;
                    switch (value)
                    {
                        case "ZZ": // UNKNOWN OR BLANK U.S. STATE OR TERRITORY OR UNKNOWN CANADIAN PROVINCE OR UNKNOWN/ UNCLASSIFIABLE COUNTRY
                            return;  // do nothing
                        case "XX": // UNKNOWN STATE WHERE COUNTRY IS KNOWN, BUT NOT U.S. OR CANADA
                             ISO31662code = birthCountry;
                            break;
                        default:  // a 2 character state
                            if (birthCountry.Equals("US") || birthCountry.Equals("CA"))
                            {
                                ISO31662code = birthCountry + "-" + value;
                            }
                            else
                            {
                                ISO31662code = value;
                            }
                            break;
                    }
                    Dictionary_Set("BSTATE", "BirthRecordState", "code", ISO31662code);
                }
            }
        }

        /// <summary>Receipt date -- Year</summary>
        [IJEField(91, 673, 4, "Receipt date -- Year", "R_YR", 1)]
        public string R_YR
        {
            get
            {
                // TODO: Implement mapping from FHIR record location: CodingStatusValues
                return "";
            }
            set
            {
                // TODO: Implement mapping to FHIR record location: CodingStatusValues
            }
        }

        /// <summary>Receipt date -- Month</summary>
        [IJEField(92, 677, 2, "Receipt date -- Month", "R_MO", 1)]
        public string R_MO
        {
            get
            {
                // TODO: Implement mapping from FHIR record location: CodingStatusValues
                return "";
            }
            set
            {
                // TODO: Implement mapping to FHIR record location: CodingStatusValues
            }
        }

        /// <summary>Receipt date -- Day</summary>
        [IJEField(93, 679, 2, "Receipt date -- Day", "R_DY", 1)]
        public string R_DY
        {
            get
            {
                // TODO: Implement mapping from FHIR record location: CodingStatusValues
                return "";
            }
            set
            {
                // TODO: Implement mapping to FHIR record location: CodingStatusValues
            }
        }

        /// <summary>Occupation -- 4 digit Code (OPTIONAL)</summary>
        [IJEField(94, 681, 4, "Occupation -- 4 digit Code (OPTIONAL)", "OCCUPC4", 1)]
        public string OCCUPC4
        {
            get
            {
                // NOTE: This is a placeholder, the IJE field OCCUPC4 is not currently implemented in FHIR
                return "";
            }
            set
            {
                // NOTE: This is a placeholder, the IJE field OCCUPC4 is not currently implemented in FHIR
            }
        }

        /// <summary>Industry -- 4 digit Code (OPTIONAL)</summary>
        [IJEField(95, 685, 4, "Industry -- 4 digit Code (OPTIONAL)", "INDUSTC4", 1)]
        public string INDUSTC4
        {
            get
            {
                // NOTE: This is a placeholder, the IJE field INDUSTC4 is not currently implemented in FHIR
                return "";
            }
            set
            {
                // NOTE: This is a placeholder, the IJE field INDUSTC4 is not currently implemented in FHIR
            }
        }

        /// <summary>Date of Registration--Year</summary>
        [IJEField(96, 689, 4, "Date of Registration--Year", "DOR_YR", 1)]
        public string DOR_YR
        {
            get
            {
                return DateTime_Get("DOR_YR", "yyyy", "RegisteredTime");
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    DateTime_Set("DOR_YR", "yyyy", "RegisteredTime", value, true);
                }
            }
        }

        /// <summary>Date of Registration--Month</summary>
        [IJEField(97, 693, 2, "Date of Registration--Month", "DOR_MO", 1)]
        public string DOR_MO
        {
            get
            {
                return DateTime_Get("DOR_MO", "MM", "RegisteredTime");
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    DateTime_Set("DOR_MO", "MM", "RegisteredTime", value, true);
                }
            }
        }

        /// <summary>Date of Registration--Day</summary>
        [IJEField(98, 695, 2, "Date of Registration--Day", "DOR_DY", 1)]
        public string DOR_DY
        {
            get
            {
                return DateTime_Get("DOR_DY", "dd", "RegisteredTime");
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    DateTime_Set("DOR_DY", "dd", "RegisteredTime", value, true);
                }
            }
        }

        /// <summary>FILLER 2 for expansion</summary>
        [IJEField(99, 697, 4, "FILLER 2 for expansion", "FILLER2", 1)]
        public string FILLER2
        {
            get
            {
                // NOTE: This is a placeholder, the IJE field  is not currently implemented in FHIR
                return "";
            }
            set
            {
                // NOTE: This is a placeholder, the IJE field  is not currently implemented in FHIR
            }
        }

        /// <summary>Manner of Death</summary>
        [IJEField(100, 701, 1, "Manner of Death", "MANNER", 1)]
        public string MANNER
        {
            get
            {
                return Get_MappingFHIRToIJE(Mappings.MannerOfDeath.FHIRToIJE, "MannerOfDeathType", "MANNER");
            }
            set
            {
                Set_MappingIJEToFHIR(Mappings.MannerOfDeath.IJEToFHIR, "MANNER", "MannerOfDeathType", value);
            }
        }

        /// <summary>Intentional Reject</summary>
        [IJEField(101, 702, 1, "Intentional Reject", "INT_REJ", 1)]
        public string INT_REJ
        {
            get
            {
                // TODO: Implement mapping from FHIR record location: CodingStatusValues
                return "";
            }
            set
            {
                // TODO: Implement mapping to FHIR record location: CodingStatusValues
            }
        }

        /// <summary>Acme System Reject Codes</summary>
        [IJEField(102, 703, 1, "Acme System Reject Codes", "SYS_REJ", 1)]
        public string SYS_REJ
        {
            get
            {
                // TODO: Implement mapping from FHIR record location: CodingStatusValues
                return "";
            }
            set
            {
                // TODO: Implement mapping to FHIR record location: CodingStatusValues
            }
        }

        /// <summary>Place of Injury (computer generated)</summary>
        [IJEField(103, 704, 1, "Place of Injury (computer generated)", "INJPL", 1)]
        public string INJPL
        {
            get
            {
                // IJE options below, default to Blank.
                // A Home
                // B Farm
                // C Residential Institution
                // D Military Residence
                // E Hospital
                // F School, Other Institutions, Administrative Area
                // G Industrial and Construction
                // H Garage/Warehouse
                // I Trade and Service Area
                // J Mine/Quarry
                // K Street/Highway
                // L Public Recreation Area
                // M Institutional Recreation Area
                // N Sports and Recreation Area
                // O Other building
                // P Other specified Place
                // Q Unspecified Place
                // Blank
                return "";
            }
            set
            {
                // NOOP
            }
        }

        /// <summary>Manual Underlying Cause</summary>
        [IJEField(104, 705, 5, "Manual Underlying Cause", "MAN_UC", 1)]
        public string MAN_UC
        {
            get
            {
                // TODO: Implement mapping from FHIR record location: ManualUnderlyingCauseOfDeath
                return "";
            }
            set
            {
                // TODO: Implement mapping to FHIR record location: ManualUnderlyingCauseOfDeath
            }
        }

        /// <summary>ACME Underlying Cause</summary>
        [IJEField(105, 710, 5, "ACME Underlying Cause", "ACME_UC", 1)]
        public string ACME_UC
        {
            get
            {
                // TODO: Implement mapping from FHIR record location: AutomatedUnderlyingCauseOfDeath
                return "";
            }
            set
            {
                // TODO: Implement mapping to FHIR record location: AutomatedUnderlyingCauseOfDeath
            }
        }

        /// <summary>Entity-axis codes</summary>
        [IJEField(106, 715, 160, "Entity-axis codes", "EAC", 1)]
        public string EAC
        {
            get
            {
                // TODO: Implement mapping from FHIR record location: EntityAxisCauseOfDeath
                return "";
            }
            set
            {
                // TODO: Implement mapping to FHIR record location: EntityAxisCauseOfDeath
            }
        }

        /// <summary>Transax conversion flag: Computer Generated</summary>
        [IJEField(107, 875, 1, "Transax conversion flag: Computer Generated", "TRX_FLG", 1)]
        public string TRX_FLG
        {
            get
            {
                // TODO: Implement mapping from FHIR record location: CodingStatusValues
                return "";
            }
            set
            {
                // TODO: Implement mapping to FHIR record location: CodingStatusValues
            }
        }

        /// <summary>Record-axis codes</summary>
        [IJEField(108, 876, 100, "Record-axis codes", "RAC", 1)]
        public string RAC
        {
            get
            {
                // TODO: Implement mapping from FHIR record location: RecordAxisCauseOfDeath
                return "";
            }
            set
            {
                // TODO: Implement mapping to FHIR record location: RecordAxisCauseOfDeath
            }
        }

        /// <summary>Was Autopsy performed</summary>
        [IJEField(109, 976, 1, "Was Autopsy performed", "AUTOP", 1)]
        public string AUTOP
        {
            get
            {
                return Get_MappingFHIRToIJE(Mappings.YesNoUnknown.FHIRToIJE, "AutopsyPerformedIndicator", "AUTOP");
            }
            set
            {
                Set_MappingIJEToFHIR(Mappings.YesNoUnknown.IJEToFHIR, "AUTOP", "AutopsyPerformedIndicator", value);
            }
        }

        /// <summary>Were Autopsy Findings Available to Complete the Cause of Death?</summary>
        [IJEField(110, 977, 1, "Were Autopsy Findings Available to Complete the Cause of Death?", "AUTOPF", 1)]
        public string AUTOPF
        {
            get
            {
                return Get_MappingFHIRToIJE(Mappings.YesNoUnknownNotApplicable.FHIRToIJE, "AutopsyResultsAvailable", "AUTOPF");
            }
            set
            {
                Set_MappingIJEToFHIR(Mappings.YesNoUnknownNotApplicable.IJEToFHIR, "AUTOPF", "AutopsyResultsAvailable", value);
            }
        }

        /// <summary>Did Tobacco Use Contribute to Death?</summary>
        [IJEField(111, 978, 1, "Did Tobacco Use Contribute to Death?", "TOBAC", 1)]
        public string TOBAC
        {
            get
            {
                return Get_MappingFHIRToIJE(Mappings.ContributoryTobaccoUse.FHIRToIJE, "TobaccoUse", "TOBAC");
            }
            set
            {
                Set_MappingIJEToFHIR(Mappings.ContributoryTobaccoUse.IJEToFHIR, "TOBAC", "TobaccoUse", value);
            }
        }

        /// <summary>Pregnancy</summary>
        [IJEField(112, 979, 1, "Pregnancy", "PREG", 1)]
        public string PREG
        {
            get
            {
                return Get_MappingFHIRToIJE(Mappings.PregnancyStatus.FHIRToIJE, "PregnancyStatus", "PREG");
            }
            set
            {
                Set_MappingIJEToFHIR(Mappings.PregnancyStatus.IJEToFHIR, "PREG", "PregnancyStatus", value);
            }
        }

        /// <summary>If Female--Edit Flag: From EDR only</summary>
        [IJEField(113, 980, 1, "If Female--Edit Flag: From EDR only", "PREG_BYPASS", 1)]
        public string PREG_BYPASS
        {
            get
            {
                return Get_MappingFHIRToIJE(Mappings.EditBypass012.FHIRToIJE, "PregnancyStatusEditFlag", "PREG_BYPASS");
            }
            set
            {
                Set_MappingIJEToFHIR(Mappings.EditBypass012.IJEToFHIR, "PREG_BYPASS", "PregnancyStatusEditFlag", value);
            }
        }

        /// <summary>Date of injury--month</summary>
        [IJEField(114, 981, 2, "Date of injury--month", "DOI_MO", 1)]
        public string DOI_MO
        {
            get
            {
                return DateTime_Get("DOI_MO", "MM", "InjuryDate");
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    DateTime_Set("DOI_MO", "MM", "InjuryDate", value.Trim(), false, true);
                }
            }
        }

        /// <summary>Date of injury--day</summary>
        [IJEField(115, 983, 2, "Date of injury--day", "DOI_DY", 1)]
        public string DOI_DY
        {
            get
            {
                return DateTime_Get("DOI_DY", "dd", "InjuryDate");
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    DateTime_Set("DOI_DY", "dd", "InjuryDate", value.Trim(), false, true);
                }
            }
        }

        /// <summary>Date of injury--year</summary>
        [IJEField(116, 985, 4, "Date of injury--year", "DOI_YR", 1)]
        public string DOI_YR
        {
            get
            {
                return DateTime_Get("DOI_YR", "yyyy", "InjuryDate");
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    DateTime_Set("DOI_YR", "yyyy", "InjuryDate", value.Trim(), false);
                }
            }
        }

        /// <summary>Time of injury</summary>
        [IJEField(117, 989, 4, "Time of injury", "TOI_HR", 1)]
        public string TOI_HR
        {
            get
            {
                return DateTime_Get("TOI_HR", "HHmm", "InjuryDate");
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    DateTime_Set("TOI_HR", "HHmm", "InjuryDate", value.Trim(), false, true);
                }
            }
        }

        /// <summary>Time of injury</summary>
        [IJEField(118, 993, 1, "Injury at work", "WORKINJ", 1)]
        public string WORKINJ
        {
            get
            {
                return Get_MappingFHIRToIJE(Mappings.YesNoUnknownNotApplicable.FHIRToIJE, "InjuryAtWork", "WORKINJ");
            }
            set
            {
                Set_MappingIJEToFHIR(Mappings.YesNoUnknownNotApplicable.IJEToFHIR, "WORKINJ", "InjuryAtWork", value);
            }
        }

        /// <summary>Title of Certifier</summary>
        [IJEField(119, 994, 30, "Title of Certifier", "CERTL", 1)]
        public string CERTL
        {
            get
            {
                string code = Dictionary_Get_Full("CERTL", "CertificationRole", "code");
                switch (code)
                {
                    case "434641000124105": // Physician (Certifier)
                        return "D";
                    case "434651000124107": // Physician (Pronouncer and Certifier)
                        return "P";
                    case "310193003": // Coroner
                        return "M";
                    case "440051000124108": // Medical Examiner
                        return "M";
                }
                return "";
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    switch (value.Trim())
                    {
                        case "D":
                            Dictionary_Set("CERTL", "CertificationRole", "code", "434641000124105");
                            Dictionary_Set("CERTL", "CertificationRole", "system", CodeSystems.SCT);
                            Dictionary_Set("CERTL", "CertificationRole", "display", "Death certification and verification by physician");
                            break;
                        case "P":
                            Dictionary_Set("CERTL", "CertificationRole", "code", "434651000124107");
                            Dictionary_Set("CERTL", "CertificationRole", "system", CodeSystems.SCT);
                            Dictionary_Set("CERTL", "CertificationRole", "display", "Physician certified and pronounced death certificate");
                            break;
                        case "M":
                            Dictionary_Set("CERTL", "CertificationRole", "code", "440051000124108");
                            Dictionary_Set("CERTL", "CertificationRole", "system", CodeSystems.SCT);
                            Dictionary_Set("CERTL", "CertificationRole", "display", "Medical Examiner");
                            break;
                    }
                }
            }
        }

        /// <summary>Activity at time of death (computer generated)</summary>
        [IJEField(120, 1024, 1, "Activity at time of death (computer generated)", "INACT", 1)]
        public string INACT
        {
            get
            {
                // IJE options below, default to "9"
                // 0 While engaged in sports activity
                // 1 While engaged in leisure activities
                // 2 While working for income
                // 3 While engaged in other types of work
                // 4 While resting, sleeping, eating, or engaging in other vital activities
                // 8 While engaged in other specified activities
                // 9 During unspecified activity
                return "9";
            }
            set
            {
                // NOOP
            }
        }

        /// <summary>Auxiliary State file number</summary>
        [IJEField(121, 1025, 12, "Auxiliary State file number", "AUXNO2", 1)]
        public string AUXNO2
        {
            get
            {
                return Get_StateLocalIdentifier("AUXNO2", ExtensionURL.AuxiliaryStateIdentifier2);
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {

                    Set_StateLocalIdentifier("AUXNO2", ExtensionURL.AuxiliaryStateIdentifier2, value);
                }
            }
        }

        /// <summary>State Specific Data</summary>
        [IJEField(122, 1037, 30, "State Specific Data", "STATESP", 1)]
        public string STATESP
        {
            get
            {
                return LeftJustified_Get("STATESP", "StateSpecific");
            }
            set
            {
                LeftJustified_Set("STATESP","StateSpecific", value);
            }
        }

        /// <summary>Surgery Date--month</summary>
        [IJEField(123, 1067, 2, "Surgery Date--month", "SUR_MO", 1)]
        public string SUR_MO
        {
            get
            {
                // TODO: Implement mapping from FHIR record location: SurgeryDate
                return "";
            }
            set
            {
                // TODO: Implement mapping to FHIR record location: SurgeryDate
            }
        }

        /// <summary>Surgery Date--day</summary>
        [IJEField(124, 1069, 2, "Surgery Date--day", "SUR_DY", 1)]
        public string SUR_DY
        {
            get
            {
                // TODO: Implement mapping from FHIR record location: SurgeryDate
                return "";
            }
            set
            {
                // TODO: Implement mapping to FHIR record location: SurgeryDate
            }
        }

        /// <summary>Surgery Date--year</summary>
        [IJEField(125, 1071, 4, "Surgery Date--year", "SUR_YR", 1)]
        public string SUR_YR
        {
            get
            {
                // TODO: Implement mapping from FHIR record location: SurgeryDate
                return "";
            }
            set
            {
                // TODO: Implement mapping to FHIR record location: SurgeryDate
            }
        }

        /// <summary>Time of Injury Unit</summary>
        [IJEField(126, 1075, 1, "Time of Injury Unit", "TOI_UNIT", 1)]
        public string TOI_UNIT
        {
            get
            {
                return "M"; // Military time
            }
            set
            {
                // NOOP
            }
        }

        /// <summary>For possible future change in transax</summary>
        [IJEField(127, 1076, 5, "For possible future change in transax", "BLANK1", 1)]
        public string BLANK1
        {
            get
            {
                // NOTE: This is a placeholder, the IJE field BLANK1 is not currently implemented in FHIR
                return "";
            }
            set
            {
                // NOTE: This is a placeholder, the IJE field BLANK1 is not currently implemented in FHIR
            }
        }

        /// <summary>Decedent ever served in Armed Forces?</summary>
        [IJEField(128, 1081, 1, "Decedent ever served in Armed Forces?", "ARMEDF", 1)]
        public string ARMEDF
        {
            get
            {
                return Get_MappingFHIRToIJE(Mappings.YesNoUnknown.FHIRToIJE, "MilitaryService", "ARMEDF");
            }
            set
            {
                Set_MappingIJEToFHIR(Mappings.YesNoUnknown.IJEToFHIR, "ARMEDF", "MilitaryService", value);
            }

        }

        /// <summary>Death Institution name</summary>
        [IJEField(129, 1082, 30, "Death Institution name", "DINSTI", 1)]
        public string DINSTI
        {
            get
            {
                return LeftJustified_Get("DINSTI", "DeathLocationName");
            }
            set
            {
                LeftJustified_Set("DINSTI", "DeathLocationName", value);
            }
        }

        /// <summary>Long String address for place of death</summary>
        [IJEField(130, 1112, 50, "Long String address for place of death", "ADDRESS_D", 1)]
        public string ADDRESS_D
        {
            get
            {
                return Dictionary_Get("ADDRESS_D", "DeathLocationAddress", "addressLine1");
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Dictionary_Set("ADDRESS_D", "DeathLocationAddress", "addressLine1", value);
                }
            }
        }

        /// <summary>Place of death. Street number</summary>
        [IJEField(131, 1162, 10, "Place of death. Street number", "STNUM_D", 1)]
        public string STNUM_D
        {
            get
            {
                // TODO: Implement mapping from FHIR record location: DeathLocation
                return "";
            }
            set
            {
                // TODO: Implement mapping to FHIR record location: DeathLocation
            }
        }

        /// <summary>Place of death. Pre Directional</summary>
        [IJEField(132, 1172, 10, "Place of death. Pre Directional", "PREDIR_D", 1)]
        public string PREDIR_D
        {
            get
            {
                // TODO: Implement mapping from FHIR record location: DeathLocation
                return "";
            }
            set
            {
                // TODO: Implement mapping to FHIR record location: DeathLocation
            }
        }

        /// <summary>Place of death. Street name</summary>
        [IJEField(133, 1182, 50, "Place of death. Street name", "STNAME_D", 1)]
        public string STNAME_D
        {
            get
            {
                // TODO: Implement mapping from FHIR record location: DeathLocation
                return "";
            }
            set
            {
                // TODO: Implement mapping to FHIR record location: DeathLocation
            }
        }

        /// <summary>Place of death. Street designator</summary>
        [IJEField(134, 1232, 10, "Place of death. Street designator", "STDESIG_D", 1)]
        public string STDESIG_D
        {
            get
            {
                // TODO: Implement mapping from FHIR record location: DeathLocation
                return "";
            }
            set
            {
                // TODO: Implement mapping to FHIR record location: DeathLocation
            }
        }

        /// <summary>Place of death. Post Directional</summary>
        [IJEField(135, 1242, 10, "Place of death. Post Directional", "POSTDIR_D", 1)]
        public string POSTDIR_D
        {
            get
            {
                // TODO: Implement mapping from FHIR record location: DeathLocation
                return "";
            }
            set
            {
                // TODO: Implement mapping to FHIR record location: DeathLocation
            }
        }

        /// <summary>Place of death. City or Town name</summary>
        [IJEField(136, 1252, 28, "Place of death. City or Town name", "CITYTEXT_D", 1)]
        public string CITYTEXT_D
        {
            get
            {
                return Dictionary_Geo_Get("CITYTEXT_D", "DeathLocationAddress", "address", "city", false);
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Dictionary_Geo_Set("CITYTEXT_D", "DeathLocationAddress", "address", "city", false, value);
                }
            }
        }

        /// <summary>Place of death. State name literal</summary>
        [IJEField(137, 1280, 28, "Place of death. State name literal", "STATETEXT_D", 1)]
        public string STATETEXT_D
        {
            get
            {
                return Dictionary_Geo_Get("STATETEXT_D", "DeathLocationAddress", "address", "state", false);
            }
            set
            {
                // NOOP
            }
        }

        /// <summary>Place of death. Zip code</summary>
        [IJEField(138, 1308, 9, "Place of death. Zip code", "ZIP9_D", 1)]
        public string ZIP9_D
        {
            get
            {
                return Dictionary_Get("ZIP9_D", "DeathLocationAddress", "addressZip");
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Dictionary_Set("ZIP9_D", "DeathLocationAddress", "addressZip", value);
                }
            }
        }

        /// <summary>Place of death. County of Death</summary>
        [IJEField(139, 1317, 28, "Place of death. County of Death", "COUNTYTEXT_D", 2)]
        public string COUNTYTEXT_D
        {
            get
            {
                return Dictionary_Geo_Get("COUNTYTEXT_D", "DeathLocationAddress", "address", "county", false);
            }
            set
            {
                // NOOP
            }
        }

        /// <summary>Place of death. City FIPS code</summary>
        [IJEField(140, 1345, 5, "Place of death. City FIPS code", "CITYCODE_D", 1)]
        public string CITYCODE_D
        {
            get
            {
                return Dictionary_Geo_Get("CITYCODE_D", "DeathLocationAddress", "address", "city", true);
            }
            set
            {
                // NOOP
            }
        }

        /// <summary>Place of death. Longitude</summary>
        [IJEField(141, 1350, 17, "Place of death. Longitude", "LONG_D", 1)]
        public string LONG_D
        {
            get
            {
                // TODO: Implement mapping from FHIR record location: DeathLocation
                return "";
            }
            set
            {
                // TODO: Implement mapping to FHIR record location: DeathLocation
            }
        }

        /// <summary>Place of Death. Latitude</summary>
        [IJEField(142, 1367, 17, "Place of Death. Latitude", "LAT_D", 1)]
        public string LAT_D
        {
            get
            {
                // TODO: Implement mapping from FHIR record location: DeathLocation
                return "";
            }
            set
            {
                // TODO: Implement mapping to FHIR record location: DeathLocation
            }
        }

        /// <summary>Decedent's spouse living at decedent's DOD?</summary>
        [IJEField(143, 1384, 1, "Decedent's spouse living at decedent's DOD?", "SPOUSELV", 1)]
        public string SPOUSELV
        {
            get
            {
                return Get_MappingFHIRToIJE(Mappings.YesNoUnknownNotApplicable.FHIRToIJE, "SpouseAlive", "SPOUSELV");
            }
            set
            {
                Set_MappingIJEToFHIR(Mappings.YesNoUnknownNotApplicable.IJEToFHIR, "SPOUSELV", "SpouseAlive", value);
            }
        }

        /// <summary>Spouse's First Name</summary>
        [IJEField(144, 1385, 50, "Spouse's First Name", "SPOUSEF", 1)]
        public string SPOUSEF
        // TODO: Implement mapping from FHIR record location: DecedentSpouse
        {
            get
            {
                string[] names = record.SpouseGivenNames;
                if (names.Length > 0)
                {
                    return names[0];
                }
                return "";
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    record.SpouseGivenNames = new string[] { value.Trim() };
                }
            }
        }

        /// <summary>Husband's Surname/Wife's Maiden Last Name</summary>
        [IJEField(145, 1435, 50, "Husband's Surname/Wife's Maiden Last Name", "SPOUSEL", 1)]
        public string SPOUSEL
        { // TODO: Implement mapping from FHIR record location: DecedentSpouse
            get
            {
                return LeftJustified_Get("SPOUSEL", "SpouseMaidenName");
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    LeftJustified_Set("SPOUSEL", "SpouseMaidenName", value);
                }
            }
        }

        /// <summary>Decedent's Residence - City or Town name</summary>
        [IJEField(152, 1560, 28, "Decedent's Residence - City or Town name", "CITYTEXT_R", 3)]
        public string CITYTEXT_R
        {
            get
            {
                return Dictionary_Geo_Get("CITYTEXT_R", "Residence", "address", "city", false);
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Dictionary_Geo_Set("CITYTEXT_R", "Residence", "address", "city", false, value);
                }
            }
        }


        /// <summary>Decedent's Residence - ZIP code</summary>
        [IJEField(153, 1588, 9, "Decedent's Residence - ZIP code", "ZIP9_R", 1)]
        public string ZIP9_R
        {
            get
            {
                return Dictionary_Geo_Get("ZIP9_R", "Residence", "address", "zip", false);
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Dictionary_Geo_Set("ZIP9_R", "Residence", "address", "zip", false, value);
                }
            }
        }

        /// <summary>Decedent's Residence - County</summary>
        [IJEField(154, 1597, 28, "Decedent's Residence - County", "COUNTYTEXT_R", 1)]
        public string COUNTYTEXT_R
        {
            get
            {
                return Dictionary_Geo_Get("COUNTYTEXT_R", "Residence", "address", "county", false);
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Dictionary_Geo_Set("COUNTYTEXT_R", "Residence", "address", "county", false, value);
                }
            }
        }

        /// <summary>Decedent's Residence - State name</summary>
        [IJEField(155, 1625, 28, "Decedent's Residence - State name", "STATETEXT_R", 1)]
        public string STATETEXT_R
        {
            get
            {
                // expand STATEC 2 letter code to full name
                var stateCode = Dictionary_Geo_Get("STATEC", "Residence", "address", "state", false);
                var mortalityData = MortalityData.Instance;
                return mortalityData.StateCodeToStateName(stateCode);
            }
            set
            {
                // NOOP, this field does not exist in FHIR
            }
        }

        /// <summary>Decedent's Residence - COUNTRY name</summary>
        [IJEField(156, 1653, 28, "Decedent's Residence - COUNTRY name", "COUNTRYTEXT_R", 1)]
        public string COUNTRYTEXT_R
        {
            get
            {
                // This is Now just the two letter code.  Need to map it to country name
                var countryCode = Dictionary_Geo_Get("COUNTRYC", "Residence", "address", "country", false);
                var mortalityData = MortalityData.Instance;
                return mortalityData.CountryCodeToCountryName(countryCode);
            }
            set
            {
                // NOOP, field does not exist in FHIR
            }
        }

        /// <summary>Long string address for decedent's place of residence same as above but allows states to choose the way they capture information.</summary>
        [IJEField(157, 1681, 50, "Long string address for decedent's place of residence same as above but allows states to choose the way they capture information.", "ADDRESS_R", 1)]
        public string ADDRESS_R
        {
            get
            {
                return Dictionary_Get("ADDRESS_R", "Residence", "addressLine1");
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Dictionary_Set("ADDRESS_R", "Residence", "addressLine1", value);
                }
            }
        }

        /// <summary>Old NCHS residence state code</summary>
        [IJEField(158, 1731, 2, "Old NCHS residence state code", "RESSTATE", 1)]
        public string RESSTATE
        {
            get
            {
                // NOTE: This is a placeholder, the IJE field RESSTATE is not currently implemented in FHIR
                return "";
            }
            set
            {
                // NOTE: This is a placeholder, the IJE field RESSTATE is not currently implemented in FHIR
            }
        }

        /// <summary>Old NCHS residence city/county combo code</summary>
        [IJEField(159, 1733, 3, "Old NCHS residence city/county combo code", "RESCON", 1)]
        public string RESCON
        {
            get
            {
                // NOTE: This is a placeholder, the IJE field RESCON is not currently implemented in FHIR
                return "";
            }
            set
            {
                // NOTE: This is a placeholder, the IJE field RESCON is not currently implemented in FHIR
            }
        }


        /// <summary>Place of death. City FIPS code</summary>
        [IJEField(145, 1485, 10, "Place of death. Decedent's Residence - Street number", "STNUM_R", 1)]
        public string STNUM_R
        {
            get
            {
                return Dictionary_Geo_Get("STNUM_R", "Residence", "address", "stnum", true);
            }
            set
            {
                // NOOP
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Dictionary_Geo_Set("STNUM_R", "Residence", "address", "stnum", false, value);
                }
            }
        }

        /// <summary>Pre directional </summary>
        [IJEField(146, 1495, 10, "Place of death. Decedent's Residence - Pre Directional", "PREDIR_R", 2)]
        public string PREDIR_R
        {
            get
            {
                return Dictionary_Geo_Get("PREDIR_R", "Residence", "address", "predir", true);
            }
            set
            {
                // NOOP
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Dictionary_Geo_Set("PREDIR_R", "Residence", "address", "predir", false, value);
                }
            }
        }

        /// <summary>Street name</summary>
        [IJEField(147, 1505, 28, "Place of death. Decedent's Residence - Street Name", "STNAME_R", 3)]
        public string STNAME_R
        {
            get
            {
                return Dictionary_Geo_Get("STNAME_R", "Residence", "address", "stname", true);
            }
            set
            {
                // NOOP
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Dictionary_Geo_Set("STNAME_R", "Residence", "address", "stname", false, value);
                }
            }
        }

        /// <summary>Street designator</summary>
        [IJEField(148, 1533, 10, "Place of death. Decedent's Residence - Street Designator", "STDESIG_R", 4)]
        public string STDESIG_R
        {
            get
            {
                return Dictionary_Geo_Get("STDESIG_R", "Residence", "address", "stdesig", true);
            }
            set
            {
                // NOOP
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Dictionary_Geo_Set("STDESIG_R", "Residence", "address", "stdesig", false, value);
                }
            }
        }

        /// <summary>Post Directional</summary>
        [IJEField(149, 1543, 10, "Place of death. Decedent's Residence - Post directional", "POSTDIR_R", 5)]
        public string POSTDIR_R
        {
            get
            {
                return Dictionary_Geo_Get("POSTDIR_R", "Residence", "address", "postdir", true);
            }
            set
            {
                // NOOP
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Dictionary_Geo_Set("POSTDIR_R", "Residence", "address", "postdir", false, value);
                }
            }
        }

        /// <summary>Unit number</summary>
        [IJEField(150, 1553, 7, "Place of death. Decedent's Residence - Unit number", "UNITNUM_R", 6)]
        public string UNITNUM_R
        {
            get
            {
                return Dictionary_Geo_Get("UNITNUM_R", "Residence", "address", "unitnum", true);
            }
            set
            {
                // NOOP
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Dictionary_Geo_Set("UNITNUM_R", "Residence", "address", "unitnum", false, value);
                }
            }
        }

        /// <summary>Hispanic</summary>
        [IJEField(160, 1736, 3, "Hispanic", "DETHNICE", 1)]
        public string DETHNICE
        {
            get
            {
                // TODO: Implement mapping from FHIR record location: CodedRaceAndEthnicity
                return "";
            }
            set
            {
                // TODO: Implement mapping to FHIR record location: CodedRaceAndEthnicity
            }
        }

        /// <summary>Bridged Race</summary>
        [IJEField(161, 1739, 2, "Bridged Race", "NCHSBRIDGE", 1)]
        public string NCHSBRIDGE
        {
            get
            {
                // NOTE: This is a placeholder, the IJE field NCHSBRIDGE is not currently implemented in FHIR
                return "";
            }
            set
            {
                // NOTE: This is a placeholder, the IJE field NCHSBRIDGE is not currently implemented in FHIR
            }
        }

        /// <summary>Hispanic - old NCHS single ethnicity codes</summary>
        [IJEField(162, 1741, 1, "Hispanic - old NCHS single ethnicity codes", "HISPOLDC", 1)]
        public string HISPOLDC
        {
            get
            {
                // NOTE: This is a placeholder, the IJE field HISPOLDC is not currently implemented in FHIR
                return "";
            }
            set
            {

                // NOTE: This is a placeholder, the IJE field HISPOLDC is not currently implemented in FHIR
            }
        }

        /// <summary>Race - old NCHS single race codes</summary>
        [IJEField(163, 1742, 1, "Race - old NCHS single race codes", "RACEOLDC", 1)]
        public string RACEOLDC

        {
            get
            {
                // NOTE: This is a placeholder, the IJE field RACEOLDC is not currently implemented in FHIR
                return "";
            }
            set
            {
                // NOTE: This is a placeholder, the IJE field RACEOLDC is not currently implemented in FHIR
            }
        }

        /// <summary>Hispanic Origin - Specify</summary>
        [IJEField(164, 1743, 15, "Hispanic Origin - Specify", "HISPSTSP", 1)]
        public string HISPSTSP
        {
            get
            {
                // NOTE: This is a placeholder, the IJE field HISPSTSP is not currently implemented in FHIR
                return "";
            }
            set
            {
                // NOTE: This is a placeholder, the IJE field HISPSTSP is not currently implemented in FHIR
            }
        }

        /// <summary>Race - Specify</summary>
        [IJEField(165, 1758, 50, "Race - Specify", "RACESTSP", 1)]
        public string RACESTSP
        {
            get
            {
                // NOTE: This is a placeholder, the IJE field RACESTSP is not currently implemented in FHIR
                return "";
            }
            set
            {
                // NOTE: This is a placeholder, the IJE field RACESTSP is not currently implemented in FHIR
            }
        }

        /// <summary>Middle Name of Decedent</summary>
        [IJEField(166, 1808, 50, "Middle Name of Decedent", "DMIDDLE", 2)]
        public string DMIDDLE
        {
            get
            {
                string[] names = record.GivenNames;
                if (names.Length > 1)
                {
                    return names[1];
                }
                return "";
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    if (record.GivenNames != null)
                    {
                        List<string> names = record.GivenNames.ToList();
                        names.Add(value.Trim());
                        record.GivenNames = names.ToArray();
                    }
                }
            }
        }

        /// <summary>Father's First Name</summary>
        [IJEField(167, 1858, 50, "Father's First Name", "DDADF", 1)]
        public string DDADF
        {
            // TODO: Implement mapping from FHIR record location: DecedentFather
            get
            {
                string[] names = record.FatherGivenNames;
                if (names != null && names.Length > 0)
                {
                    return names[0];
                }
                return "";
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    record.FatherGivenNames = new string[] { value.Trim() };
                }
            }
        }

        /// <summary>Father's Middle Name</summary>
        [IJEField(168, 1908, 50, "Father's Middle Name", "DDADMID", 2)]
        public string DDADMID
        {// TODO: Implement mapping to FHIR record location: DecedentFather
            get
            {
                string[] names = record.FatherGivenNames;
                if (names != null && names.Length > 1)
                {
                    return names[1];
                }
                return "";
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    if (record.FatherGivenNames != null)
                    {
                        List<string> names = record.FatherGivenNames.ToList();
                        names.Add(value.Trim());
                        record.FatherGivenNames = names.ToArray();
                    }
                }
            }
        }

        /// <summary>Mother's First Name</summary>
        [IJEField(169, 1958, 50, "Mother's First Name", "DMOMF", 1)]
        public string DMOMF
        {
            get
            {
                string[] names = record.MotherGivenNames;
                if (names != null && names.Length > 0)
                {
                    return names[0];
                }
                return "";
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    record.MotherGivenNames = new string[] { value.Trim() };
                }
            }
        }

        /// <summary>Mother's Middle Name</summary>
        [IJEField(170, 2008, 50, "Mother's Middle Name", "DMOMMID", 2)]
        public string DMOMMID
        {// TODO: Implement mapping to FHIR record location: DecedentMother
            get
            {
                string[] names = record.MotherGivenNames;
                if (names != null && names.Length > 1)
                {
                    return names[1];
                }
                return "";
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    if (record.MotherGivenNames != null)
                    {
                        List<string> names = record.MotherGivenNames.ToList();
                        names.Add(value.Trim());
                        record.MotherGivenNames = names.ToArray();
                    }
                }
            }
        }

        /// <summary>Mother's Maiden Surname</summary>
        [IJEField(171, 2058, 50, "Mother's Maiden Surname", "DMOMMDN", 1)]
        public string DMOMMDN
        {
            get
            {
                return LeftJustified_Get("DMOMMDN", "MotherMaidenName");
            }
            set
            {
                LeftJustified_Set("DMOMMDN", "MotherMaidenName", value);
            }
        }

             /// <summary>Was case Referred to Medical Examiner/Coroner?</summary>
        [IJEField(172, 2108, 1, "Was case Referred to Medical Examiner/Coroner?", "REFERRED", 1)]
        public string REFERRED
        {
            get
            {
                return Get_MappingFHIRToIJE(Mappings.YesNoUnknown.FHIRToIJE, "ExaminerContacted", "REFERRED");
            }
            set
            {
                Set_MappingIJEToFHIR(Mappings.YesNoUnknown.IJEToFHIR, "REFERRED", "ExaminerContacted", value);
            }
        }

        /// <summary>Place of Injury- literal</summary>
        [IJEField(173, 2109, 50, "Place of Injury- literal", "POILITRL", 1)]
        public string POILITRL
        {
            get
            {
                return LeftJustified_Get("POILITRL", "InjuryPlaceDescription");
            }
            set
            {
                LeftJustified_Set("POILITRL", "InjuryPlaceDescription", value);
            }

        }

        /// <summary>Describe How Injury Occurred</summary>
        [IJEField(174, 2159, 250, "Describe How Injury Occurred", "HOWINJ", 1)]
        public string HOWINJ
        {
            get
            {
                return LeftJustified_Get("HOWINJ", "InjuryDescription");
            }
            set
            {
                LeftJustified_Set("HOWINJ", "InjuryDescription", value);
            }
        }

        /// <summary>If Transportation Accident, Specify</summary>
        [IJEField(175, 2409, 30, "If Transportation Accident, Specify", "TRANSPRT", 1)]
        public string TRANSPRT
        {
            get
            {
                return Get_MappingFHIRToIJE(Mappings.MannerOfDeath.FHIRToIJE, "TransportationRole", "TRANSPRT");
            }
            set
            {
                Set_MappingIJEToFHIR(Mappings.MannerOfDeath.IJEToFHIR, "TRANSPRT", "TransportationRole", value);
            }
        }

        /// <summary>County of Injury - literal</summary>
        [IJEField(176, 2439, 28, "County of Injury - literal", "COUNTYTEXT_I", 1)]
        public string COUNTYTEXT_I
        {
            get
            {
                return Dictionary_Geo_Get("COUNTYTEXT_I", "InjuryLocationAddress", "address", "county", false);
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Dictionary_Geo_Set("COUNTYTEXT_I", "InjuryLocationAddress", "address", "county", false, value);
                }
            }
        }

        /// <summary>County of Injury code</summary>
        [IJEField(177, 2467, 3, "County of Injury code", "COUNTYCODE_I", 2)]
        public string COUNTYCODE_I
        {
            get
            {
                return Dictionary_Geo_Get("COUNTYCODE_I", "InjuryLocationAddress", "address", "countyC", true);
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Dictionary_Geo_Set("COUNTYCODE_I", "InjuryLocationAddress", "address", "countyC", true, value);
                }
            }
        }

        /// <summary>Town/city of Injury - literal</summary>
        [IJEField(178, 2470, 28, "Town/city of Injury - literal", "CITYTEXT_I", 3)]
        public string CITYTEXT_I
        {
            get
            {
                return Dictionary_Geo_Get("CITYTEXT_I", "InjuryLocationAddress", "address", "city", false);
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Dictionary_Geo_Set("CITYTEXT_I", "InjuryLocationAddress", "address", "city", false, value);
                }
            }
        }

        /// <summary>Town/city of Injury code</summary>
        [IJEField(179, 2498, 5, "Town/city of Injury code", "CITYCODE_I", 3)]
        public string CITYCODE_I
        {
            get
            {
                return Dictionary_Geo_Get("CITYCODE_I", "InjuryLocationAddress", "address", "cityC", true);
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Dictionary_Geo_Set("CITYCODE_I", "InjuryLocationAddress", "address", "CityC", true, value);
                }
            }
        }

        /// <summary>State, U.S. Territory or Canadian Province of Injury - code</summary>
        [IJEField(180, 2503, 2, "State, U.S. Territory or Canadian Province of Injury - code", "STATECODE_I", 1)]
        public string STATECODE_I
        {
            get
            {
                return Dictionary_Geo_Get("STATECODE_I", "InjuryLocationAddress", "address", "stateC", true);
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Dictionary_Geo_Set("STATECODE_I", "InjuryLocationAddress", "address", "stateC", true, value);
                }
            }
        }

        /// <summary>Place of injury. Longitude</summary>
        [IJEField(181, 2505, 17, "Place of injury. Longitude", "LONG_I", 1)]
        public string LONG_I
        {
            get
            {
                // TODO: Implement mapping from FHIR record location: InjuryLocation
                return "";
            }
            set
            {
                // TODO: Implement mapping to FHIR record location: InjuryLocation
            }
        }

        /// <summary>Place of injury. Latitude</summary>
        [IJEField(182, 2522, 17, "Place of injury. Latitude", "LAT_I", 1)]
        public string LAT_I
        {
            get
            {
                // TODO: Implement mapping from FHIR record location: InjuryLocation
                return "";
            }
            set
            {
                // TODO: Implement mapping to FHIR record location: InjuryLocation
            }
        }

        /// <summary>Old NCHS education code if collected - receiving state will recode as they prefer</summary>
        [IJEField(183, 2539, 2, "Old NCHS education code if collected - receiving state will recode as they prefer", "OLDEDUC", 1)]
        public string OLDEDUC
        {
            get
            {
                // NOTE: This is a placeholder, the IJE field OLDEDUC is not currently implemented in FHIR
                return "";
            }
            set
            {
                // NOTE: This is a placeholder, the IJE field OLDEDUC is not currently implemented in FHIR
            }
        }

        /// <summary>Replacement Record -- suggested codes</summary>
        [IJEField(184, 2541, 1, "Replacement Record -- suggested codes", "REPLACE", 1)]
        public string REPLACE
        {
            get
            {
                return Get_MappingFHIRToIJE(Mappings.ReplaceStatus.FHIRToIJE, "ReplaceStatus", "REPLACE");
            }
            set
            {
                Set_MappingIJEToFHIR(Mappings.ReplaceStatus.IJEToFHIR, "REPLACE", "ReplaceStatus", value);
            }
        }

        /// <summary>Cause of Death Part I Line a</summary>
        [IJEField(185, 2542, 120, "Cause of Death Part I Line a", "COD1A", 1)]
        public string COD1A
        {
            get
            {
                if (!String.IsNullOrWhiteSpace(record.COD1A)) {
                    return record.COD1A.Trim();
                }
                return "";
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    record.COD1A = value.Trim();
                }
            }
        }

        /// <summary>Cause of Death Part I Interval, Line a</summary>
        [IJEField(186, 2662, 20, "Cause of Death Part I Interval, Line a", "INTERVAL1A", 2)]
        public string INTERVAL1A
        {
            get
            {
                if (!String.IsNullOrWhiteSpace(record.INTERVAL1A)) {
                    return record.INTERVAL1A.Trim();
                }
                return "";
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    record.INTERVAL1A = value.Trim();
                }
            }
        }

        /// <summary>Cause of Death Part I Line b</summary>
        [IJEField(187, 2682, 120, "Cause of Death Part I Line b", "COD1B", 3)]
        public string COD1B
        {
            get
            {
                if (!String.IsNullOrWhiteSpace(record.COD1B)) {
                    return record.COD1B.Trim();
                }
                return "";
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    record.COD1B = value.Trim();
                }
            }
        }

        /// <summary>Cause of Death Part I Interval, Line b</summary>
        [IJEField(188, 2802, 20, "Cause of Death Part I Interval, Line b", "INTERVAL1B", 4)]
        public string INTERVAL1B
        {
            get
            {
                if (!String.IsNullOrWhiteSpace(record.INTERVAL1B)) {
                    return record.INTERVAL1B.Trim();
                }
                return "";
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    record.INTERVAL1B = value.Trim();
                }
            }
        }

        /// <summary>Cause of Death Part I Line c</summary>
        [IJEField(189, 2822, 120, "Cause of Death Part I Line c", "COD1C", 5)]
        public string COD1C
        {
            get
            {
                if (!String.IsNullOrWhiteSpace(record.COD1C)) {
                    return record.COD1C.Trim();
                }
                return "";
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    record.COD1C = value.Trim();
                }
            }
        }

        /// <summary>Cause of Death Part I Interval, Line c</summary>
        [IJEField(190, 2942, 20, "Cause of Death Part I Interval, Line c", "INTERVAL1C", 6)]
        public string INTERVAL1C
        {
            get
            {
                if (!String.IsNullOrWhiteSpace(record.INTERVAL1C)) {
                    return record.INTERVAL1C.Trim();
                }
                return "";
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    record.INTERVAL1C = value.Trim();
                }
            }
        }

        /// <summary>Cause of Death Part I Line d</summary>
        [IJEField(191, 2962, 120, "Cause of Death Part I Line d", "COD1D", 7)]
        public string COD1D
        {
            get
            {
                if (!String.IsNullOrWhiteSpace(record.COD1D)) {
                    return record.COD1D.Trim();
                }
                return "";
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    record.COD1D = value.Trim();
                }
            }
        }

        /// <summary>Cause of Death Part I Interval, Line d</summary>
        [IJEField(192, 3082, 20, "Cause of Death Part I Interval, Line d", "INTERVAL1D", 8)]
        public string INTERVAL1D
        {
            get
            {
                if (!String.IsNullOrWhiteSpace(record.INTERVAL1D)) {
                    return record.INTERVAL1D.Trim();
                }
                return "";
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    record.INTERVAL1D = value.Trim();
                }
            }
        }

        /// <summary>Cause of Death Part II</summary>
        [IJEField(193, 3102, 240, "Cause of Death Part II", "OTHERCONDITION", 1)]
        public string OTHERCONDITION
        {
            get
            {
                if (record.ContributingConditions != null)
                {
                    return record.ContributingConditions.Trim();
                }
                return "";
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    record.ContributingConditions = value.Trim();
                }
            }
        }

        /// <summary>Decedent's Maiden Name</summary>
        [IJEField(194, 3342, 50, "Decedent's Maiden Name", "DMAIDEN", 1)]
        public string DMAIDEN
        {
            get
            {
                return LeftJustified_Get("DMAIDEN", "MaidenName");
            }
            set
            {
                LeftJustified_Set("DMAIDEN", "MaidenName", value);
            }
        }


        /// <summary>Decedent's Birth Place City - Code</summary>
        [IJEField(194, 3392, 5, "Decedent's Birth Place City - Code", "DBPLACECITYCODE", 3)]
        public string DBPLACECITYCODE
        {
            get
            {
                return Dictionary_Geo_Get("DBPLACECITYCODE", "PlaceOfBirth", "address", "cityC", false);
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Dictionary_Geo_Set("DBPLACECITYCODE", "PlaceOfBirth", "address", "cityC", false, value);
                }
            }
        }

        /// <summary>Decedent's Birth Place City - Literal</summary>
        [IJEField(196, 3397, 28, "Decedent's Birth Place City - Literal", "DBPLACECITY", 3)]
        public string DBPLACECITY
        {
            get
            {
                return Dictionary_Geo_Get("DBPLACECITY", "PlaceOfBirth", "address", "city", false);
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Dictionary_Geo_Set("DBPLACECITY", "PlaceOfBirth", "address", "city", false, value);
                }
            }
        }

        /// <summary>Informant's Relationship</summary>
        [IJEField(200, 3505, 30, "Informant's Relationship", "INFORMRELATE", 3)]
        public string INFORMRELATE
        {
            get
            {
                return Dictionary_Get("INFORMRELATE", "ContactRelationship", "text");
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Dictionary_Set("INFORMRELATE", "ContactRelationship", "text", value);
                }
            }
        }

        /// <summary>Spouse's Middle Name</summary>
        [IJEField(197, 3425, 50, "Spouse's Middle Name", "SPOUSEMIDNAME", 2)]
        public string SPOUSEMIDNAME
        {// TODO: Implement mapping to FHIR record location: DecedentSpouse
            get
            {
                string[] names = record.SpouseGivenNames;
                if (names != null && names.Length > 1)
                {
                    return names[1];
                }
                return "";
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    if (record.SpouseGivenNames != null)
                    {
                        List<string> names = record.SpouseGivenNames.ToList();
                        names.Add(value.Trim());
                        record.SpouseGivenNames = names.ToArray();
                    }
                }
            }
        }

        /// <summary>Spouse's Suffix</summary>
        [IJEField(198, 3475, 10, "Spouse's Suffix", "SPOUSESUFFIX", 1)]
        public string SPOUSESUFFIX
         {
            // TODO: Implement mapping from FHIR record location: DecedentSpouse
            get
            {
                return LeftJustified_Get("SPOUSESUFFIX", "Suffix");
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    LeftJustified_Set("SPOUSESUFFIX", "Suffix", value.Trim());
                }
            }
        }
        /// <summary>Father's Suffix</summary>
        [IJEField(199, 3485, 10, "Father's Suffix", "FATHERSUFFIX", 1)]
        public string FATHERSUFFIX
        {
            // TODO: Implement mapping from FHIR record location: DecedentFather
            get
            {
                return LeftJustified_Get("FATHERSUFFIX", "Suffix");
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    LeftJustified_Set("FATHERSUFFIX", "Suffix", value.Trim());
                }
            }
        }

        /// <summary>Mother's Suffix</summary>
        [IJEField(200, 3495, 10, "Mother's Suffix", "MOTHERSSUFFIX", 1)]
        public string MOTHERSSUFFIX
        {
            // TODO: Implement mapping from FHIR record location: DecedentMother
            get
            {
                return LeftJustified_Get("MOTHERSSUFFIX", "Suffix");
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    LeftJustified_Set("MOTHERSSUFFIX", "Suffix", value.Trim());
                }
            }
        }

        /// <summary>State, U.S. Territory or Canadian Province of Disposition - code</summary>
        [IJEField(202, 3535, 2, "State, U.S. Territory or Canadian Province of Disposition - code", "DISPSTATECD", 1)]
        public string DISPSTATECD
        {
            get
            {
                return Dictionary_Geo_Get("DISPSTATECD", "DispositionLocationAddress", "address", "state", true);
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Dictionary_Geo_Set("DISPSTATECD", "DispositionLocationAddress", "address", "state", true, value);
                }
            }
        }

        /// <summary>Disposition State or Territory - Literal</summary>
        [IJEField(203, 3537, 28, "Disposition State or Territory - Literal", "DISPSTATE", 1)]
        public string DISPSTATE
        {
            get
            {
                var stateCode = Dictionary_Geo_Get("DISPSTATECD", "InjuryLocationAddress", "address", "stateC", false);
                var mortalityData = MortalityData.Instance;
                return mortalityData.StateCodeToStateName(stateCode);
            }
            set
            {
                // NOOP
            }
        }

        /// <summary>Disposition City - Code</summary>
        [IJEField(204, 3565, 5, "Disposition City - Code", "DISPCITYCODE", 1)]
        public string DISPCITYCODE
        {
            get
            {
                return Dictionary_Geo_Get("DISPCITYCODE", "DispositionLocationAddress", "address", "cityC", false);
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Dictionary_Geo_Set("DISPCITYCODE", "DispositionLocationAddress", "address", "cityC", false, value);
                }
            }
        }

        /// <summary>Disposition City - Literal</summary>
        [IJEField(205, 3570, 28, "Disposition City - Literal", "DISPCITY", 3)]
        public string DISPCITY
        {
            get
            {
                return Dictionary_Geo_Get("DISPCITY", "DispositionLocationAddress", "address", "city", false);
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Dictionary_Geo_Set("DISPCITY", "DispositionLocationAddress", "address", "city", false, value);
                    // We've got city, and we probably also have state now - so attempt to find county while we're at it (IJE does NOT include this).
                    string state = Dictionary_Geo_Get("DISPSTATECD", "DispositionLocationAddress", "address", "state", true);
                    string county = dataLookup.StateCodeAndCityNameToCountyName(state, value);
                    if (!String.IsNullOrWhiteSpace(county))
                    {
                        Dictionary_Geo_Set("DISPCITY", "DispositionLocationAddress", "address", "county", false, county);
                        // If we found a county, we know the country.
                        Dictionary_Geo_Set("DISPCITY", "DispositionLocationAddress", "address", "country", false, "US");
                    }
                }
            }
        }

        /// <summary>Funeral Facility Name</summary>
        [IJEField(206, 3598, 100, "Funeral Facility Name", "FUNFACNAME", 1)]
        public string FUNFACNAME
        {
            get
            {
                return LeftJustified_Get("FUNFACNAME", "FuneralHomeName");
            }
            set
            {
                LeftJustified_Set("FUNFACNAME", "FuneralHomeName", value);
            }
        }

        /// <summary>Funeral Facility - Street number</summary>
        [IJEField(207, 3698, 10, "Funeral Facility - Street number", "FUNFACSTNUM", 1)]
        public string FUNFACSTNUM
        {
            get
            {
                return Dictionary_Geo_Get("FUNFACSTNUM", "CertifierAddress", "address", "stnum", true);
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Dictionary_Geo_Set("FUNFACSTNUM", "CertifierAddress", "address", "stnum", false, value);
                }
            }
        }

        /// <summary>Funeral Facility - Pre Directional</summary>
        [IJEField(208, 3708, 10, "Funeral Facility - Pre Directional", "FUNFACPREDIR", 1)]
        public string FUNFACPREDIR
        {
            get
            {
                // TODO: Implement mapping from FHIR record location: FuneralHome
                return "";
            }
            set
            {
                // TODO: Implement mapping to FHIR record location: FuneralHome
            }
        }

        /// <summary>Funeral Facility - Street name</summary>
        [IJEField(209, 3718, 28, "Funeral Facility - Street name", "FUNFACSTRNAME", 1)]
        public string FUNFACSTRNAME
        {
            get
            {
                // TODO: Implement mapping from FHIR record location: FuneralHome
                return "";
            }
            set
            {
                // TODO: Implement mapping to FHIR record location: FuneralHome
            }
        }

        /// <summary>Funeral Facility - Street designator</summary>
        [IJEField(210, 3746, 10, "Funeral Facility - Street designator", "FUNFACSTRDESIG", 1)]
        public string FUNFACSTRDESIG
        {
            get
            {
                // TODO: Implement mapping from FHIR record location: FuneralHome
                return "";
            }
            set
            {
                // TODO: Implement mapping to FHIR record location: FuneralHome
            }
        }

        /// <summary>Funeral Facility - Post Directional</summary>
        [IJEField(211, 3756, 10, "Funeral Facility - Post Directional", "FUNPOSTDIR", 1)]
        public string FUNPOSTDIR
        {
            get
            {
                // TODO: Implement mapping from FHIR record location: FuneralHome
                return "";
            }
            set
            {
                // TODO: Implement mapping to FHIR record location: FuneralHome
            }
        }

        /// <summary>Funeral Facility - Unit or apt number</summary>
        [IJEField(212, 3766, 7, "Funeral Facility - Unit or apt number", "FUNUNITNUM", 1)]
        public string FUNUNITNUM
        {
            get
            {
                // TODO: Implement mapping from FHIR record location: FuneralHome
                return "";
            }
            set
            {
                // TODO: Implement mapping to FHIR record location: FuneralHome
            }
        }

        /// <summary>Long string address for Funeral Facility same as above but allows states to choose the way they capture information.</summary>
        [IJEField(213, 3773, 50, "Long string address for Funeral Facility same as above but allows states to choose the way they capture information.", "FUNFACADDRESS", 1)]
        public string FUNFACADDRESS
        {
            get
            {
                return Dictionary_Get("FUNFACADDRESS", "FuneralHomeAddress", "addressLine1");
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Dictionary_Set("FUNFACADDRESS", "FuneralHomeAddress", "addressLine1", value);
                }
            }
        }

        /// <summary>Funeral Facility - City or Town name</summary>
        [IJEField(214, 3823, 28, "Funeral Facility - City or Town name", "FUNCITYTEXT", 3)]
        public string FUNCITYTEXT
        {
            get
            {
                return Dictionary_Get("FUNCITYTEXT", "FuneralHomeAddress", "addressCity");
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Dictionary_Set("FUNCITYTEXT", "FuneralHomeAddress", "addressCity", value);
                    // We've got city, and we probably also have state now - so attempt to find county while we're at it (IJE does NOT include this).
                    string state = dataLookup.StateNameToStateCode(Dictionary_Get("FUNSTATE", "FuneralHomeAddress", "addressState"));
                    string county = dataLookup.StateCodeAndCityNameToCountyName(state, value);
                    if (!String.IsNullOrWhiteSpace(county))
                    {
                        Dictionary_Set("FUNCITYTEXT", "FuneralHomeAddress", "addressCounty", county);
                        // If we found a county, we know the country.
                        Dictionary_Set("FUNCITYTEXT", "FuneralHomeAddress", "addressCountry", "US");
                    }
                }
            }
        }

        /// <summary>State, U.S. Territory or Canadian Province of Funeral Facility - code</summary>
        [IJEField(215, 3851, 2, "State, U.S. Territory or Canadian Province of Funeral Facility - code", "FUNSTATECD", 1)]
        public string FUNSTATECD
        {
            get
            {

                return Dictionary_Geo_Get("FUNSTATECD", "InjuryLocationAddress", "address", "stateC", true);
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Dictionary_Set("FUNSTATECD", "FuneralHomeAddress", "stateC", value);
                }
            }
        }

        /// <summary>State, U.S. Territory or Canadian Province of Funeral Facility - literal</summary>
        [IJEField(216, 3853, 28, "State, U.S. Territory or Canadian Province of Funeral Facility - literal", "FUNSTATE", 1)]
        public string FUNSTATE
        {
            get
            {
                var stateCode = Dictionary_Geo_Get("FUNSTATECD", "InjuryLocationAddress", "address", "stateC", false);
                var mortalityData = MortalityData.Instance;
                return mortalityData.StateCodeToStateName(stateCode);
            }
            set
            {
                // NOOP
            }
        }

        /// <summary>Funeral Facility - ZIP</summary>
        [IJEField(217, 3881, 9, "Funeral Facility - ZIP", "FUNZIP", 1)]
        public string FUNZIP
        {
            get
            {
                return Dictionary_Get("FUNZIP", "FuneralHomeAddress", "addressZip");
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Dictionary_Set("FUNZIP", "FuneralHomeAddress", "addressZip", value);
                }
            }
        }

        /// <summary>Person Pronouncing Date Signed</summary>
        [IJEField(218, 3890, 8, "Person Pronouncing Date Signed", "PPDATESIGNED", 1)]
        public string PPDATESIGNED
        {
            get
            {
                return DateTime_Get("PPDATESIGNED", "MMddyyyy", "DateOfDeathPronouncement");
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    DateTime_Set("PPDATESIGNED", "MMddyyyy", "DateOfDeathPronouncement", value, false, true);
                }
            }
        }

        /// <summary>Person Pronouncing Time Pronounced</summary>
        [IJEField(219, 3898, 4, "Person Pronouncing Time Pronounced", "PPTIME", 1)]
        public string PPTIME
        {
            get
            {
                return DateTime_Get("PPTIME", "HHmm", "DateOfDeathPronouncement");
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    DateTime_Set("PPTIME", "HHmm", "DateOfDeathPronouncement", value, false, true);
                }
            }
        }

        /// <summary>Certifier's First Name</summary>
        [IJEField(220, 3902, 50, "Certifier's First Name", "CERTFIRST", 1)]
        public string CERTFIRST
        {
            get
            {
                string[] names = record.CertifierGivenNames;
                if (names != null && names.Length > 0)
                {
                    return names[0];
                }
                return "";
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    record.CertifierGivenNames = new string[] { value.Trim() };
                }
            }
        }

        /// <summary>Certifier's Middle Name </summary>
        [IJEField(221, 3952, 50, "Certifier's Middle Name", "CERTMIDDLE", 2)]
        public string CERTMIDDLE
        {
            get
            {
                string[] names = record.CertifierGivenNames;
                if (names != null && names.Length > 1)
                {
                    return names[1];
                }
                return "";
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    if (record.GivenNames != null)
                    {
                        List<string> names = record.CertifierGivenNames.ToList();
                        names.Add(value.Trim());
                        record.CertifierGivenNames = names.ToArray();
                    }
                }
            }
        }

        /// <summary>Certifier's Last Name</summary>
        [IJEField(222, 4002, 50, "Certifier's Last Name", "CERTLAST", 3)]
        public string CERTLAST
        {
            get
            {
                return LeftJustified_Get("CERTLAST", "CertifierFamilyName");
            }
            set
            {
                LeftJustified_Set("CERTLAST", "CertifierFamilyName", value);
            }
        }

        /// <summary>Certifier's Suffix Name</summary>
        [IJEField(223, 4052, 10, "Certifier's Suffix Name", "CERTSUFFIX", 4)]
        public string CERTSUFFIX
        {
            get
            {
                return LeftJustified_Get("CERTSUFFIX", "CertifierSuffix");
            }
            set
            {
                LeftJustified_Set("CERTSUFFIX", "CertifierSuffix", value);
            }
        }

        /// <summary>Certifier - Street number</summary>
        [IJEField(224, 4062, 10, "Certifier - Street number", "CERTSTNUM", 1)]
        public string CERTSTNUM
        {
            get
            {
                return Dictionary_Geo_Get("CERTSTNUM", "CertifierAddress", "address", "stnum", true);
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Dictionary_Geo_Set("CERTSTNUM", "CertifierAddress", "address", "stnum", false, value);
                }
            }
        }

        /// <summary>Certifier - Pre Directional</summary>
        [IJEField(225, 4072, 10, "Certifier - Pre Directional", "CERTPREDIR", 1)]
        public string CERTPREDIR
        {
            get
            {
                return Dictionary_Geo_Get("CERTPREDIR", "CertifierAddress", "address", "predir", true);
            }
            set
            {
                // NOOP
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Dictionary_Geo_Set("CERTPREDIR", "CertifierAddress", "address", "predir", false, value);
                }
            }
        }

        /// <summary>Certifier - Street name</summary>
        [IJEField(226, 4082, 28, "Certifier - Street name", "CERTSTRNAME", 1)]
        public string CERTSTRNAME
        {
            get
            {
                return Dictionary_Geo_Get("CERTSTRNAME", "CertifierAddress", "address", "stname", true);
            }
            set
            {
                // NOOP
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Dictionary_Geo_Set("CERTSTRNAME", "CertifierAddress", "address", "stname", false, value);
                }
            }
        }

        /// <summary>Certifier - Street designator</summary>
        [IJEField(227, 4110, 10, "Certifier - Street designator", "CERTSTRDESIG", 1)]
        public string CERTSTRDESIG
        {
            get
            {
                return Dictionary_Geo_Get("CERTSTRDESIG", "CertifierAddress", "address", "stdesig", true);
            }
            set
            {
                // NOOP
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Dictionary_Geo_Set("CERTSTRDESIG", "CertifierAddress", "address", "stdesig", false, value);
                }
            }
        }

        /// <summary>Certifier - Post Directional</summary>
        [IJEField(228, 4120, 10, "Certifier - Post Directional", "CERTPOSTDIR", 1)]
        public string CERTPOSTDIR
        {
           get
            {
                return Dictionary_Geo_Get("CERTPOSTDIR", "CertifierAddress", "address", "postdir", true);
            }
            set
            {
                // NOOP
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Dictionary_Geo_Set("CERTPOSTDIR", "CertifierAddress", "address", "postdir", false, value);
                }
            }

        }

        /// <summary>Certifier - Unit or apt number</summary>
        [IJEField(229, 4130, 7, "Certifier - Unit or apt number", "CERTUNITNUM", 1)]
        public string CERTUNITNUM
        {
           get
            {
                return Dictionary_Geo_Get("CERTUNITNUM", "CertifierAddress", "address", "unitnum", true);
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Dictionary_Geo_Set("CERTUNITNUM", "CertifierAddress", "address", "unitnum", false, value);
                }
            }
        }

        /// <summary>Long string address for Certifier same as above but allows states to choose the way they capture information.</summary>
        [IJEField(230, 4137, 50, "Long string address for Certifier same as above but allows states to choose the way they capture information.", "CERTADDRESS", 1)]
        public string CERTADDRESS
        {
            get
            {
                return Dictionary_Get("CERTADDRESS", "CertifierAddress", "addressLine1");
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Dictionary_Set("CERTADDRESS", "CertifierAddress", "addressLine1", value);
                }
            }
        }

        /// <summary>Certifier - City or Town name</summary>
        [IJEField(231, 4187, 28, "Certifier - City or Town name", "CERTCITYTEXT", 2)]
        public string CERTCITYTEXT
        {
            get
            {
                return Dictionary_Get("CERTCITYTEXT", "CertifierAddress", "addressCity");
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Dictionary_Set("CERTCITYTEXT", "CertifierAddress", "addressCity", value);
                    // // We've got city, and we probably also have state now - so attempt to find county while we're at it (IJE does NOT include this).
                    // string county = dataLookup.StateCodeAndCityNameToCountyName(CERTSTATECD, value);
                    // if (!String.IsNullOrWhiteSpace(county))
                    // {
                    //     Dictionary_Geo_Set("CERTCITYTEXT", "CertifierAddress", "address", "county", false, county);
                    //     // If we found a county, we know the country.
                    //     Dictionary_Geo_Set("CERTCITYTEXT", "CertifierAddress", "address", "country", false, "US");
                    // }
                }
            }
        }

        /// <summary>State, U.S. Territory or Canadian Province of Certifier - code</summary>
        [IJEField(232, 4215, 2, "State, U.S. Territory or Canadian Province of Certifier - code", "CERTSTATECD", 1)]
        public string CERTSTATECD
        {
            get
            {
                return dataLookup.StateNameToStateCode(Dictionary_Get_Full("CERTSTATECD", "CertifierAddress", "addressState"));
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Dictionary_Set("CERTSTATECD", "CertifierAddress", "addressState", value);
                }
            }
        }

        /// <summary>State, U.S. Territory or Canadian Province of Certifier - literal</summary>
        [IJEField(233, 4217, 28, "State, U.S. Territory or Canadian Province of Certifier - literal", "CERTSTATE", 1)]
        public string CERTSTATE
        {
            get
            {
                return Dictionary_Get("CERTSTATE", "CertifierAddress", "addressState");
            }
            set
            {
                // NOOP
            }
        }

        /// <summary>Certifier - Zip</summary>
        [IJEField(234, 4245, 9, "Certifier - Zip", "CERTZIP", 1)]
        public string CERTZIP
        {
            get
            {
                return Dictionary_Get("CERTZIP", "CertifierAddress", "addressZip");
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Dictionary_Set("CERTZIP", "CertifierAddress", "addressZip", value);
                }
            }
        }

        /// <summary>Certifier Date Signed</summary>
        [IJEField(235, 4254, 8, "Certifier Date Signed", "CERTDATE", 1)]
        public string CERTDATE
        {
            get
            {
                return DateTime_Get("CERTDATE", "MMddyyyy", "CertifiedTime");
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    DateTime_Set("CERTDATE", "MMddyyyy", "CertifiedTime", value, true, false);
                }
            }
        }

        /// <summary>Date Filed</summary>
        [IJEField(236, 4262, 8, "Date Filed", "FILEDATE", 1)]
        public string FILEDATE
        {
            get
            {
                // NOTE: This is a placeholder, the IJE field FILEDATE is not currently implemented in FHIR
                return "";
            }
            set
            {
                // NOTE: This is a placeholder, the IJE field FILEDATE is not currently implemented in FHIR
            }
        }

        /// <summary>State, U.S. Territory or Canadian Province of Injury - literal</summary>
        [IJEField(237, 4270, 28, "State, U.S. Territory or Canadian Province of Injury - literal", "STINJURY", 1)]
        public string STINJURY
        {
            get
            {
                var stateCode = Dictionary_Geo_Get("STATECODE_I", "InjuryLocationAddress", "address", "stateC", false);
                var mortalityData = MortalityData.Instance;
                return mortalityData.StateCodeToStateName(stateCode);
            }
            set
            {
                // NOOP
            }
        }

        /// <summary>State, U.S. Territory or Canadian Province of Birth - literal</summary>
        [IJEField(238, 4298, 28, "State, U.S. Territory or Canadian Province of Birth - literal", "STATEBTH", 1)]
        public string STATEBTH
        {
            get
            {
                var stateCode = Dictionary_Geo_Get("BPLACE_ST", "PlaceOfBirth", "address", "state", false);
                var mortalityData = MortalityData.Instance;
                return mortalityData.StateCodeToStateName(stateCode);

            }
            set
            {
                // NOOP, field does not exist in FHIR
            }
        }

        /// <summary>Country of Death - Code</summary>
        [IJEField(239, 4326, 2, "Country of Death - Code", "DTHCOUNTRYCD", 1)]
        public string DTHCOUNTRYCD
        {
            get
            {
                return Dictionary_Geo_Get("DTHCOUNTRYCD", "DeathLocationAddress", "address", "country", true);
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Dictionary_Geo_Set("DTHCOUNTRYCD", "DeathLocationAddress", "address", "country", true, value);
                }
            }
        }

        /// <summary>Country of Death - Literal</summary>
        [IJEField(240, 4328, 28, "Country of Death - Literal", "DTHCOUNTRY", 1)]
        public string DTHCOUNTRY
        {
            get
            {
                return Dictionary_Geo_Get("DTHCOUNTRY", "DeathLocationAddress", "address", "country", false);
            }
            set
            {
                // NOOP
            }
        }

        /// <summary>SSA State Source of Death</summary>
        [IJEField(241, 4356, 3, "SSA State Source of Death", "SSADTHCODE", 1)]
        public string SSADTHCODE
        {
            get
            {
                // NOTE: This is a placeholder, the IJE field SSADTHCODE is not currently implemented in FHIR
                return "";
            }
            set
            {
                // NOTE: This is a placeholder, the IJE field SSADTHCODE is not currently implemented in FHIR
            }
        }

        /// <summary>SSA Foreign Country Indicator</summary>
        [IJEField(242, 4359, 1, "SSA Foreign Country Indicator", "SSAFOREIGN", 1)]
        public string SSAFOREIGN
        {
            get
            {
                // NOTE: This is a placeholder, the IJE field SSAFOREIGN is not currently implemented in FHIR
                return "";
            }
            set
            {
                // NOTE: This is a placeholder, the IJE field SSAFOREIGN is not currently implemented in FHIR
            }
        }

        /// <summary>SSA EDR Verify Code</summary>
        [IJEField(243, 4360, 1, "SSA EDR Verify Code", "SSAVERIFY", 1)]
        public string SSAVERIFY
        {
            get
            {
                // NOTE: This is a placeholder, the IJE field SSAVERIFY is not currently implemented in FHIR
                return "";
            }
            set
            {
                // NOTE: This is a placeholder, the IJE field SSAVERIFY is not currently implemented in FHIR
            }
        }

        /// <summary>SSA Date of SSN Verification</summary>
        [IJEField(244, 4361, 8, "SSA Date of SSN Verification", "SSADATEVER", 1)]
        public string SSADATEVER
        {
            get
            {
                // NOTE: This is a placeholder, the IJE field SSADATEVER is not currently implemented in FHIR
                return "";
            }
            set
            {
                // NOTE: This is a placeholder, the IJE field SSADATEVER is not currently implemented in FHIR
            }
        }

        /// <summary>SSA Date of State Transmission</summary>
        [IJEField(245, 4369, 8, "SSA Date of State Transmission", "SSADATETRANS", 1)]
        public string SSADATETRANS
        {
            get
            {
                // NOTE: This is a placeholder, the IJE field SSADATETRANS is not currently implemented in FHIR
                return "";
            }
            set
            {
                // NOTE: This is a placeholder, the IJE field SSADATETRANS is not currently implemented in FHIR
            }
        }

        /// <summary>Hispanic Code for Literal</summary>
        [IJEField(247, 4427, 3, "Hispanic Code for Literal", "DETHNIC5C", 1)]
        public string DETHNIC5C
        {
            get
            {
                // TODO: Implement mapping from FHIR record location: CodedRaceAndEthnicity
                return "";
            }
            set
            {
                // TODO: Implement mapping to FHIR record location: CodedRaceAndEthnicity
            }
        }

        /// <summary>Blank for One-Byte Field 1</summary>
        [IJEField(248, 4430, 1, "Blank for One-Byte Field 1", "PLACE1_1", 1)]
        public string PLACE1_1
        {
            get
            {
                return LeftJustified_Get("PLACE1_1", "EmergingIssue1_1");
            }
            set
            {
                LeftJustified_Set("PLACE1_1", "EmergingIssue1_1", value);
            }
        }

        /// <summary>Blank for One-Byte Field 2</summary>
        [IJEField(249, 4431, 1, "Blank for One-Byte Field 2", "PLACE1_2", 1)]
        public string PLACE1_2
        {
            get
            {
                return LeftJustified_Get("PLACE1_2", "EmergingIssue1_2");
            }
            set
            {
                LeftJustified_Set("PLACE1_2", "EmergingIssue1_2", value);
            }
        }

        /// <summary>Blank for One-Byte Field 3</summary>
        [IJEField(250, 4432, 1, "Blank for One-Byte Field 3", "PLACE1_3", 1)]
        public string PLACE1_3
        {
            get
            {
                return LeftJustified_Get("PLACE1_3", "EmergingIssue1_3");
            }
            set
            {
                LeftJustified_Set("PLACE1_3", "EmergingIssue1_3", value);
            }
        }

        /// <summary>Blank for One-Byte Field 4</summary>
        [IJEField(251, 4433, 1, "Blank for One-Byte Field 4", "PLACE1_4", 1)]
        public string PLACE1_4
        {
            get
            {
                return LeftJustified_Get("PLACE1_4", "EmergingIssue1_4");
            }
            set
            {
                LeftJustified_Set("PLACE1_4", "EmergingIssue1_4", value);
            }
        }

        /// <summary>Blank for One-Byte Field 5</summary>
        [IJEField(252, 4434, 1, "Blank for One-Byte Field 5", "PLACE1_5", 1)]
        public string PLACE1_5
        {
            get
            {
                return LeftJustified_Get("PLACE1_5", "EmergingIssue1_5");
            }
            set
            {
                LeftJustified_Set("PLACE1_5", "EmergingIssue1_5", value);
            }
        }

        /// <summary>Blank for One-Byte Field 6</summary>
        [IJEField(253, 4435, 1, "Blank for One-Byte Field 6", "PLACE1_6", 1)]
        public string PLACE1_6
        {
            get
            {
                return LeftJustified_Get("PLACE1_6", "EmergingIssue1_6");
            }
            set
            {
                LeftJustified_Set("PLACE1_6", "EmergingIssue1_6", value);
            }
        }

        /// <summary>Blank for Eight-Byte Field 1</summary>
        [IJEField(254, 4436, 8, "Blank for Eight-Byte Field 1", "PLACE8_1", 1)]
        public string PLACE8_1
        {
            get
            {
                return LeftJustified_Get("PLACE8_1", "EmergingIssue8_1");
            }
            set
            {
                LeftJustified_Set("PLACE8_1", "EmergingIssue8_1", value);
            }
        }

        /// <summary>Blank for Eight-Byte Field 2</summary>
        [IJEField(255, 4444, 8, "Blank for Eight-Byte Field 2", "PLACE8_2", 1)]
        public string PLACE8_2
        {
            get
            {
                return LeftJustified_Get("PLACE8_2", "EmergingIssue8_2");
            }
            set
            {
                LeftJustified_Set("PLACE8_2", "EmergingIssue8_2", value);
            }
        }

        /// <summary>Blank for Eight-Byte Field 3</summary>
        [IJEField(256, 4452, 8, "Blank for Eight-Byte Field 3", "PLACE8_3", 1)]
        public string PLACE8_3
        {
            get
            {
                return LeftJustified_Get("PLACE8_3", "EmergingIssue8_3");
            }
            set
            {
                LeftJustified_Set("PLACE8_3", "EmergingIssue8_3", value);
            }
        }

        /// <summary>Blank for Twenty-Byte Field</summary>
        [IJEField(257, 4460, 20, "Blank for Twenty-Byte Field", "PLACE20", 1)]
        public string PLACE20
        {
            get
            {
                return LeftJustified_Get("PLACE20", "EmergingIssue20");
            }
            set
            {
                LeftJustified_Set("PLACE20", "EmergingIssue20", value);
            }
        }

        /// <summary>Blank for future expansion</summary>
        [IJEField(258, 4480, 250, "Blank for future expansion", "BLANK2", 1)]
        public string BLANK2
        {
            get
            {
                // NOTE: This is a placeholder, the IJE field BLANK2 is not currently implemented in FHIR
                return "";
            }
            set
            {
                // NOTE: This is a placeholder, the IJE field BLANK2 is not currently implemented in FHIR
            }
        }

        /// <summary>Blank for Jurisdictional Use Only</summary>
        [IJEField(259, 4730, 271, "Blank for Jurisdictional Use Only", "BLANK3", 1)]
        public string BLANK3
        {
            get
            {
                // NOTE: This is a placeholder, the IJE field BLANK3 is not currently implemented in FHIR
                return "";
            }
            set
            {

            }
        }
        // NOTE: This is a placeholder, the IJE field BLANK3 is not currently implemented in FHIR
        /// <summary>Marital Descriptor</summary>
        [IJEField(246, 4377, 50, "Martial Descriptor", "MARITAL_DESCRIP", 1)]
        public string MARITAL_DESCRIP
        {
            get
            {
                return LeftJustified_Get("MARITAL_DESCRIP", "MaritalStatusLiteral");
            }
            set
            {
                LeftJustified_Set("MARITAL_DESCRIP", "MaritalStatusLiteral", value);
            }
        }
    }
}
