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
            IJEField info = FieldInfo(ijeFieldName);
            typeof(DeathRecord).GetProperty(fhirFieldName).SetValue(this.record, value.Trim());
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
                    // check for unknown or other values
                    string county = null;
                    dictionary.TryGetValue(keyPrefix + "County", out county);
                    if (county == "UNK")
                    {
                        current = "999";
                    }
                    else if (county == "OTH")
                    {
                        current = "000";
                    }
                    else
                    {
                        string state = null;
                        dictionary.TryGetValue(keyPrefix + "State", out state);

                        if (state != null)
                        {
                            current = dataLookup.StateNameAndCountyNameToCountyCode(state, current);
                        }
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
                    if (geoType == "place" || geoType == "city") // This is a tricky case, we need to know about county and state!
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
                    else if (geoType == "county") // This is a tricky case, we need to know about state!
                    {
                        // Handle unknown
                        if (value == "999")
                        {
                            dictionary[key] = "UNK";
                        }
                        else if (value == "000")
                        {
                            dictionary[key] = "OTH";
                        }
                        else
                        {
                            string state = null;
                            dictionary.TryGetValue(keyPrefix + "State", out state);
                            if (!String.IsNullOrWhiteSpace(state))
                            {
                                string county = dataLookup.StateNameAndCountyCodeToCountyName(state, value);
                                if (!String.IsNullOrWhiteSpace(county))
                                {
                                    dictionary[key] = county;
                                }
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

        /// <summary>If the decedent was of hispanic origin, returns a list of ethnicities.</summary>
        private string[] HispanicOrigin()
        {
            Tuple<string, string>[] ethnicityStatus = record.Ethnicity;
            List<string> ethnicities = new List<string>();
            // Check if hispanic origin
            if (Array.Exists(ethnicityStatus, element => element.Item1 == "Hispanic or Latino" || element.Item2 == "2135-2"))
            {
                foreach(Tuple<string, string> tuple in ethnicityStatus)
                {
                    ethnicities.Add(tuple.Item1);
                    ethnicities.Add(tuple.Item2);
                }
            }
            return ethnicities.ToArray();
        }

        /// <summary>If the decedent was of hispanic origin, returns a list of OTHER ethnicities (not Mexican, Cuban, or Puerto Rican).</summary>
        private Tuple<string, string>[] HispanicOriginOther()
        {
            Tuple<string, string>[] ethnicityStatus = record.Ethnicity;
            List<Tuple<string, string>> ethnicities = new List<Tuple<string, string>>();
            foreach(Tuple<string, string> tuple in ethnicityStatus)
            {
                if (tuple.Item1.ToUpper() != "Non Hispanic or Latino".ToUpper() &&
                    tuple.Item1.ToUpper() != "Hispanic or Latino".ToUpper() &&
                    tuple.Item1.ToUpper() != "Mexican".ToUpper() &&
                    tuple.Item1.ToUpper() != "Puerto Rican".ToUpper() &&
                    tuple.Item1.ToUpper() != "Cuban".ToUpper())
                {
                    if (tuple.Item2.ToUpper() != "2186-5".ToUpper() &&
                        tuple.Item2.ToUpper() != "2135-2".ToUpper() &&
                        tuple.Item2.ToUpper() != "2148-5".ToUpper() &&
                        tuple.Item2.ToUpper() != "2180-8".ToUpper() &&
                        tuple.Item2.ToUpper() != "2182-4".ToUpper())
                    {
                        ethnicities.Add(tuple);
                    }
                }
            }
            return ethnicities.ToArray();
        }

        /// <summary>Checks if the given race exists in the record.</summary>
        private bool Get_Race(string code, string display)
        {
            return Array.Exists(record.Race, element => element.Item1 == display || element.Item2 == code);
        }

        /// <summary>Retrieves American Indian or Alaska Native Race literals on the record.</summary>
        private string[] Get_Race_AIAN_Literals()
        {
            KeyValuePair<string, string>[] literals = record.Race.Select(race => new KeyValuePair<string, string>(race.Item2, race.Item1)).Intersect(dataLookup.CDCRaceAIANCodes).ToArray();
            string[] filterCodes = { "1002-5" };
            return literals.Where(race => !filterCodes.Contains(race.Key)).Select(race => race.Value).ToArray();
        }

        /// <summary>Retrieves Asian Race literals (not including ones captured by distinct fields).</summary>
        private string[] Get_Race_A_Literals()
        {
            KeyValuePair<string, string>[] literals = record.Race.Select(race => new KeyValuePair<string, string>(race.Item2, race.Item1)).Intersect(dataLookup.CDCRaceACodes).ToArray();
            string[] filterCodes = { "2028-9", "2039-6", "2040-4", "2047-9", "2036-2", "2034-7", "2029-7" };
            return literals.Where(race => !filterCodes.Contains(race.Key)).Select(race => race.Value).ToArray();
        }

        /// <summary>Retrieves Black or African American Race literals on the record.</summary>
        private string[] Get_Race_BAA_Literals()
        {
            KeyValuePair<string, string>[] literals = record.Race.Select(race => new KeyValuePair<string, string>(race.Item2, race.Item1)).Intersect(dataLookup.CDCRaceBAACodes).ToArray();
            string[] filterCodes = { "2054-5" };
            return literals.Where(race => !filterCodes.Contains(race.Key)).Select(race => race.Value).ToArray();
        }

        /// <summary>Retrieves Native Hawaiian or Other Pacific Islander Race literals on the record.</summary>
        private string[] Get_Race_NHOPI_Literals()
        {
            KeyValuePair<string, string>[] literals = record.Race.Select(race => new KeyValuePair<string, string>(race.Item2, race.Item1)).Intersect(dataLookup.CDCRaceNHOPICodes).ToArray();
            string[] filterCodes = { "2076-8", "2086-7", "2080-0", "2079-2" };
            return literals.Where(race => !filterCodes.Contains(race.Key)).Select(race => race.Value).ToArray();
        }

        /// <summary>Retrieves White Race literals on the record.</summary>
        private string[] Get_Race_W_Literals()
        {
            KeyValuePair<string, string>[] literals = record.Race.Select(race => new KeyValuePair<string, string>(race.Item2, race.Item1)).Intersect(dataLookup.CDCRaceWCodes).ToArray();
            string[] filterCodes = { "2106-3" };
            return literals.Where(race => !filterCodes.Contains(race.Key)).Select(race => race.Value).ToArray();
        }

        /// <summary>Retrieves OTHER Race literals on the record.</summary>
        private string[] Get_Race_OTHER_Literals()
        {
            return Get_Race_W_Literals().ToList().Concat(Get_Race_BAA_Literals().ToList()).ToArray();
        }

        /// <summary>Adds the given race to the record.</summary>
        private void Set_Race(string code, string display)
        {
            List<Tuple<string, string>> raceStatus = record.Race.ToList();
            raceStatus.Add(Tuple.Create(display, code));
            record.Race = raceStatus.Distinct().ToList().ToArray();
        }

        /// <summary>Gets a "Yes", "No", or "Unknown" value.</summary>
        private string Get_YNU(string fhirFieldName)
        {
            object status = typeof(DeathRecord).GetProperty(fhirFieldName).GetValue(this.record);
            if (status == null)
            {
                return "U";
            }
            else
            {
                return ((bool)status) ? "Y" : "N";
            }
        }

        /// <summary>Sets a "Yes", "No", or "Unkown" value.</summary>
        private void Set_YNU(string fhirFieldName, string value)
        {
            if (value != "U" && value == "Y")
            {
                typeof(DeathRecord).GetProperty(fhirFieldName).SetValue(this.record, true);
            }
            else if (value != "U" && value == "N")
            {
                typeof(DeathRecord).GetProperty(fhirFieldName).SetValue(this.record, false);
            }
        }

        /// <summary>Given a Dictionary mapping FHIR codes to IJE strings and the relevant FHIR and IJE fields pull the value
        /// from the FHIR record object and provide the appropriate IJE string</summary>
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
                     Dictionary_Set("STATEC", "DeathLocationAddress", "addressState", value);
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
                return RightJustifiedZeroed_Get("AUXNO", "StateLocalIdentifier");
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    RightJustifiedZeroed_Set("AUXNO", "StateLocalIdentifier", value);
                }
            }
        }

        /// <summary>Source flag: paper/electronic</summary>
        [IJEField(6, 26, 1, "Source flag: paper/electronic", "MFILED", 1)]
        public string MFILED
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
                if (!String.IsNullOrWhiteSpace(value))
                {
                    LeftJustified_Set("LNAME", "FamilyName", value.Trim());
                }
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
                if (!String.IsNullOrWhiteSpace(value))
                {
                    LeftJustified_Set("SUFF", "Suffix", value.Trim());
                }
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
                if (!String.IsNullOrWhiteSpace(value))
                {
                    LeftJustified_Set("FLNAME", "FatherFamilyName", value.Trim());
                }
            }
        }

        /// <summary>Sex</summary>
        [IJEField(13, 189, 1, "Sex", "SEX", 1)]
        public string SEX
        {
            get
            {
                return record.BirthSex;
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    record.BirthSex = value.Trim();
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
                    LeftJustified_Set("SEX", "Gender", display);
                }
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
                if (!String.IsNullOrWhiteSpace(value))
                {
                    LeftJustified_Set("SSN", "SSN", value);
                }
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
                // Populate the date absent parts if any of the date parts are unknown
                // Doing all three parts at once in the last date part field
                // because the other fields hadn't populated the Date Part field yet
                // and only one would ever get added to the record
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
                } else 
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
                    Dictionary_Set("STATEC", "PlaceOfBirth", "addressCountry", value);
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
                return Dictionary_Geo_Get("CITYC", "Residence", "address", "city", true);
            }
            set
            {
                // NOOP
            }
        }

        /// <summary>Decedent's Residence--County</summary>
        [IJEField(25, 222, 3, "Decedent's Residence--County", "COUNTYC", 2)]
        public string COUNTYC
        {
            get
            {
                return Dictionary_Geo_Get("COUNTYC", "Residence", "address", "county", true);
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Dictionary_Geo_Set("COUNTYC", "Residence", "address", "county", true, value);
                }
            }
        }

        /// <summary>State, U.S. Territory or Canadian Province of Decedent's residence - code</summary>
        [IJEField(26, 225, 2, "State, U.S. Territory or Canadian Province of Decedent's residence - code", "STATEC", 1)]
        public string STATEC
        {
            get
            {
                return Dictionary_Geo_Get("STATEC", "Residence", "address", "state", true);
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
                switch (record.ResidenceWithinCityLimitsBoolean)
                {
                    case true: // Yes
                        return "Y";
                    case false: // No
                        return "N";
                    default: // Unknown
                        return "U";
                }
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    switch (value)
                    {
                        case "Y":
                            record.ResidenceWithinCityLimitsBoolean = true;
                            break;
                        case "N":
                            record.ResidenceWithinCityLimitsBoolean = false;
                            break;
                        default:
                            record.ResidenceWithinCityLimitsBoolean = null;
                            break;
                    }
                }
            }
        }

        /// <summary>Marital Status</summary>
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
                if (!String.IsNullOrWhiteSpace(value))
                {
                    switch (value)
                    {
                        case "M":
                            Dictionary_Set("MARITAL", "MaritalStatus", "code", value);
                            Dictionary_Set("MARITAL", "MaritalStatus", "system", CodeSystems.PH_MaritalStatus_HL7_2x);
                            Dictionary_Set("MARITAL", "MaritalStatus", "display", "Married");
                            break;
                        case "A":
                            Dictionary_Set("MARITAL", "MaritalStatus", "code", value);
                            Dictionary_Set("MARITAL", "MaritalStatus", "system", CodeSystems.PH_MaritalStatus_HL7_2x);
                            Dictionary_Set("MARITAL", "MaritalStatus", "display", "Annulled");
                            break;
                        case "W":
                            Dictionary_Set("MARITAL", "MaritalStatus", "code", value);
                            Dictionary_Set("MARITAL", "MaritalStatus", "system", CodeSystems.PH_MaritalStatus_HL7_2x);
                            Dictionary_Set("MARITAL", "MaritalStatus", "display", "Widowed");
                            break;
                        case "D":
                            Dictionary_Set("MARITAL", "MaritalStatus", "code", value);
                            Dictionary_Set("MARITAL", "MaritalStatus", "system", CodeSystems.PH_MaritalStatus_HL7_2x);
                            Dictionary_Set("MARITAL", "MaritalStatus", "display", "Divorced");
                            break;
                        case "S":
                            Dictionary_Set("MARITAL", "MaritalStatus", "code", value);
                            Dictionary_Set("MARITAL", "MaritalStatus", "system", CodeSystems.PH_MaritalStatus_HL7_2x);
                            Dictionary_Set("MARITAL", "MaritalStatus", "display", "Never Married");
                            break;
                        case "U":
                            Dictionary_Set("MARITAL", "MaritalStatus", "code", "value");
                            Dictionary_Set("MARITAL", "MaritalStatus", "system", CodeSystems.PH_MaritalStatus_HL7_2x);
                            Dictionary_Set("MARITAL", "MaritalStatus", "display", "Unknown");
                            break;
                    }
                }
            }
        }

        /// <summary>Marital Status--Edit Flag</summary>
        [IJEField(30, 231, 1, "Marital Status--Edit Flag", "MARITAL_BYPASS", 1)]
        public string MARITAL_BYPASS
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

        /// <summary>Marital Status--Edit Flag</summary>
        [IJEField(31, 232, 1, "Place of Death", "DPLACE", 1)]
        public string DPLACE
        {
            get
            {
                string code = Dictionary_Get_Full("DPLACE", "DeathLocationType", "code");
                switch (code)
                {
                    case "16983000":
                        return "1";
                    case "450391000124102":
                        return "2";
                    case "63238001":
                        return "3";
                    case "440081000124100":
                        return "4";
                    case "440071000124103":
                        return "5";
                    case "450381000124100":
                        return "6";
                    case "OTH":
                        return "7";
                    case "UNK":
                        return "9";
                }
                return "";
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    switch (value)
                    {
                        case "1":
                            Dictionary_Set("DPLACE", "DeathLocationType", "code", "16983000");
                            Dictionary_Set("DPLACE", "DeathLocationType", "system", CodeSystems.PH_SNOMED_CT);
                            Dictionary_Set("DPLACE", "DeathLocationType", "display", "Hospital Inpatient");
                            break;
                        case "2":
                            Dictionary_Set("DPLACE", "DeathLocationType", "code", "450391000124102");
                            Dictionary_Set("DPLACE", "DeathLocationType", "system", CodeSystems.PH_SNOMED_CT);
                            Dictionary_Set("DPLACE", "DeathLocationType", "display", "Death in emergency Room/Outpatient");
                            break;
                        case "3":
                            Dictionary_Set("DPLACE", "DeathLocationType", "code", "63238001");
                            Dictionary_Set("DPLACE", "DeathLocationType", "system", CodeSystems.PH_SNOMED_CT);
                            Dictionary_Set("DPLACE", "DeathLocationType", "display", "Hospital Dead on Arrival");
                            break;
                        case "4":
                            Dictionary_Set("DPLACE", "DeathLocationType", "code", "440081000124100");
                            Dictionary_Set("DPLACE", "DeathLocationType", "system", CodeSystems.PH_SNOMED_CT);
                            Dictionary_Set("DPLACE", "DeathLocationType", "display", "Decendent's Home");
                            break;
                        case "5":
                            Dictionary_Set("DPLACE", "DeathLocationType", "code", "440071000124103");
                            Dictionary_Set("DPLACE", "DeathLocationType", "system", CodeSystems.PH_SNOMED_CT);
                            Dictionary_Set("DPLACE", "DeathLocationType", "display", "Hospice");
                            break;
                        case "6":
                            Dictionary_Set("DPLACE", "DeathLocationType", "code", "450381000124100");
                            Dictionary_Set("DPLACE", "DeathLocationType", "system", CodeSystems.PH_SNOMED_CT);
                            Dictionary_Set("DPLACE", "DeathLocationType", "display", "Death in nursing home/Long term care facility");
                            break;
                        case "7":
                            Dictionary_Set("DPLACE", "DeathLocationType", "code", "OTH");
                            Dictionary_Set("DPLACE", "DeathLocationType", "system", CodeSystems.PH_SNOMED_CT);
                            Dictionary_Set("DPLACE", "DeathLocationType", "display", "Other(Specify)");
                            break;
                        case "9":
                            Dictionary_Set("DPLACE", "DeathLocationType", "code", "Unknown");
                            Dictionary_Set("DPLACE", "DeathLocationType", "system", CodeSystems.PH_SNOMED_CT);
                            Dictionary_Set("DPLACE", "DeathLocationType", "display", "Unknown");
                            break;
                    }
                }
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
                string code = Dictionary_Get_Full("DISP", "DecedentDispositionMethod", "code");
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
                if (!String.IsNullOrWhiteSpace(value))
                {
                    switch (value)
                    {
                        case "D":
                            Dictionary_Set("DISP", "DecedentDispositionMethod", "code", "449951000124101");
                            Dictionary_Set("DISP", "DecedentDispositionMethod", "system", CodeSystems.SCT);
                            Dictionary_Set("DISP", "DecedentDispositionMethod", "display", "Patient status determination, deceased and body donated");
                            break;
                        case "B":
                            Dictionary_Set("DISP", "DecedentDispositionMethod", "code", "449971000124106");
                            Dictionary_Set("DISP", "DecedentDispositionMethod", "system", CodeSystems.SCT);
                            Dictionary_Set("DISP", "DecedentDispositionMethod", "display", "Patient status determination, deceased and buried");
                            break;
                        case "C":
                            Dictionary_Set("DISP", "DecedentDispositionMethod", "code", "449961000124104");
                            Dictionary_Set("DISP", "DecedentDispositionMethod", "system", CodeSystems.SCT);
                            Dictionary_Set("DISP", "DecedentDispositionMethod", "display", "Patient status determination, deceased and cremated");
                            break;
                        case "E":
                            Dictionary_Set("DISP", "DecedentDispositionMethod", "code", "449931000124108");
                            Dictionary_Set("DISP", "DecedentDispositionMethod", "system", CodeSystems.SCT);
                            Dictionary_Set("DISP", "DecedentDispositionMethod", "display", "Patient status determination, deceased and entombed");
                            break;
                        case "R":
                            Dictionary_Set("DISP", "DecedentDispositionMethod", "code", "449941000124103");
                            Dictionary_Set("DISP", "DecedentDispositionMethod", "system", CodeSystems.SCT);
                            Dictionary_Set("DISP", "DecedentDispositionMethod", "display", "Patient status determination, deceased and removed from state");
                            break;
                        case "U":
                            Dictionary_Set("DISP", "DecedentDispositionMethod", "code", "UNK");
                            Dictionary_Set("DISP", "DecedentDispositionMethod", "system", CodeSystems.PH_NullFlavor_HL7_V3 );
                            Dictionary_Set("DISP", "DecedentDispositionMethod", "display", "Unknown");
                            break;
                        case "O":
                            Dictionary_Set("DISP", "DecedentDispositionMethod", "code", "OTH");
                            Dictionary_Set("DISP", "DecedentDispositionMethod", "system", CodeSystems.PH_NullFlavor_HL7_V3);
                            Dictionary_Set("DISP", "DecedentDispositionMethod", "display", "Other");
                            break;
                    }
                }
            }
        }

        /// <summary>Date of Death--Month</summary>
        [IJEField(34, 237, 2, "Date of Death--Month", "DOD_MO", 1)]
        public string DOD_MO
        {
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
                Tuple<string, string>[] ethnicityStatus = record.Ethnicity;
                // if there is no ethnicity data, then the ethnicity is unknown
                if (ethnicityStatus.Length == 0){
                    return "U";
                }
                string[] ethnicities = HispanicOrigin();
                if (ethnicities.Length == 0)
                {
                    return "N";
                }
                if (Array.Exists(ethnicities, element => element == "Mexican" || element == "2148-5"))
                {
                    return "H";
                }
                return "N";
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    List<Tuple<string, string>> ethnicities = record.Ethnicity.ToList();
                    if (value == "H")
                    {
                        ethnicities.Add(Tuple.Create("Mexican", "2148-5"));
                        ethnicities.Add(Tuple.Create("Hispanic or Latino", "2135-2"));
                        ethnicities.RemoveAll(x => x.Item1 == "Non Hispanic or Latino" || x.Item2 == "2186-5");
                        record.Ethnicity = ethnicities.Distinct().ToList().ToArray();
                    }
                    else if (ethnicities.Count == 0 && value == "N")
                    {
                        ethnicities.Add(Tuple.Create("Non Hispanic or Latino", "2186-5"));
                        record.Ethnicity = ethnicities.Distinct().ToList().ToArray();
                    }
                }
            }
        }

        /// <summary>Decedent of Hispanic Origin?--Puerto Rican</summary>
        [IJEField(40, 248, 1, "Decedent of Hispanic Origin?--Puerto Rican", "DETHNIC2", 1)]
        public string DETHNIC2
        {
            get
            {

                Tuple<string, string>[] ethnicityStatus = record.Ethnicity;
                // if there is no ethnicity data, then the ethnicity is unknown
                if (ethnicityStatus.Length == 0){
                    return "U";
                }
                string[] ethnicities = HispanicOrigin();
                if (ethnicities.Length == 0)
                {
                    return "N";
                }
                if (Array.Exists(ethnicities, element => element == "Puerto Rican" || element == "2180-8"))
                {
                    return "H";
                }
                return "N";
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    List<Tuple<string, string>> ethnicities = record.Ethnicity.ToList();
                    if (value == "H")
                    {
                        ethnicities.Add(Tuple.Create("Puerto Rican", "2180-8"));
                        ethnicities.Add(Tuple.Create("Hispanic or Latino", "2135-2"));
                        ethnicities.RemoveAll(x => x.Item1 == "Non Hispanic or Latino" || x.Item2 == "2186-5");
                        record.Ethnicity = ethnicities.Distinct().ToList().ToArray();
                    }
                    else if (ethnicities.Count == 0 && value == "N")
                    {
                        ethnicities.Add(Tuple.Create("Non Hispanic or Latino", "2186-5"));
                        record.Ethnicity = ethnicities.Distinct().ToList().ToArray();
                    }
                }
            }
        }

        /// <summary>Decedent of Hispanic Origin?--Cuban</summary>
        [IJEField(41, 249, 1, "Decedent of Hispanic Origin?--Cuban", "DETHNIC3", 1)]
        public string DETHNIC3
        {
            get
            {
                Tuple<string, string>[] ethnicityStatus = record.Ethnicity;
                // if there is no ethnicity data, then the ethnicity is unknown
                if (ethnicityStatus.Length == 0){
                    return "U";
                }

                string[] ethnicities = HispanicOrigin();
                if (ethnicities.Length == 0)
                {
                    return "N";
                }
                if (Array.Exists(ethnicities, element => element == "Cuban" || element == "2182-4"))
                {
                    return "H";
                }
                return "N";
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    List<Tuple<string, string>> ethnicities = record.Ethnicity.ToList();
                    if (value == "H")
                    {
                        ethnicities.Add(Tuple.Create("Cuban", "2182-4"));
                        ethnicities.Add(Tuple.Create("Hispanic or Latino", "2135-2"));
                        ethnicities.RemoveAll(x => x.Item1 == "Non Hispanic or Latino" || x.Item2 == "2186-5");
                        record.Ethnicity = ethnicities.Distinct().ToList().ToArray();
                    }
                    else if (ethnicities.Count == 0 && value == "N")
                    {
                        ethnicities.Add(Tuple.Create("Non Hispanic or Latino", "2186-5"));
                        record.Ethnicity = ethnicities.Distinct().ToList().ToArray();
                    }
                }
            }
        }

        /// <summary>Decedent of Hispanic Origin?--Other</summary>
        [IJEField(42, 250, 1, "Decedent of Hispanic Origin?--Other", "DETHNIC4", 1)]
        public string DETHNIC4
        {
            get
            {
                Tuple<string, string>[] ethnicityStatus = record.Ethnicity;
                // if there is no ethnicity data, then the ethnicity is unknown
                if (ethnicityStatus.Length == 0){
                    return "U";
                }

                string[] hispanicOrigin = HispanicOrigin();
                Tuple<string, string>[] hispanicOriginOther = HispanicOriginOther();
                string[] validHispanicOrigins = { "Cuban", "2182-4", "Puerto Rican", "2180-8", "Mexican", "2148-5" };

                // This logic will handle cases where hispanic origin is other with or without write-in
                // It also maintains that hispanic origin and other are not mutually exclusive
                var ethnicityText = record.EthnicityText;
                if (!String.IsNullOrWhiteSpace(ethnicityText) && hispanicOrigin.Length > 0) // there was a write in and they are Hispanic or Latino
                {
                    return "H";
                }
                else if (hispanicOriginOther.Length > 0) // there was a code for an "other" hispanic or latino ethnicity
                {
                    return "H";
                }
                else if (hispanicOrigin.Length > 0 && !hispanicOrigin.Any(element => validHispanicOrigins.Contains(element))) // they are hispanic or latino and DETHNIC 1,2,3 ="N" so DETHIC4 must be "H"
                {
                    return "H";
                }
                else {
                    return "N"; // not hispanic or latino
                }
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    List<Tuple<string, string>> ethnicities = record.Ethnicity.ToList();
                    if (value == "H")
                    {
                        ethnicities.Add(Tuple.Create("Hispanic or Latino", "2135-2"));
                        ethnicities.RemoveAll(x => x.Item1 == "Non Hispanic or Latino" || x.Item2 == "2186-5");
                        record.Ethnicity = ethnicities.Distinct().ToList().ToArray();
                    }
                    else if (ethnicities.Count == 0 && value == "N")
                    {
                        ethnicities.Add(Tuple.Create("Non Hispanic or Latino", "2186-5"));
                        record.Ethnicity = ethnicities.Distinct().ToList().ToArray();
                    }
                }
            }
        }

        /// <summary>Decedent of Hispanic Origin?--Other, Literal</summary>
        [IJEField(43, 251, 20, "Decedent of Hispanic Origin?--Other, Literal", "DETHNIC5", 1)]
        public string DETHNIC5
        {
            get
            {
                var ethnicityText = record.EthnicityText;
                if (!String.IsNullOrWhiteSpace(ethnicityText))
                {
                    return Truncate(ethnicityText, 20).Trim();
                }
                Tuple<string, string> other = HispanicOriginOther().FirstOrDefault();
                if (other != null)
                {
                    return other.Item1;
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
                    record.EthnicityText = value;
                }
            }
        }

        /// <summary>Decedent's Race--White</summary>
        [IJEField(44, 271, 1, "Decedent's Race--White", "RACE1", 1)]
        public string RACE1
        {
            get
            {
                return Get_Race("2106-3", "White") ? "Y" : "N";
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    if (value == "Y")
                    {
                        Set_Race("2106-3", "White");
                    }
                }
            }
        }

        /// <summary>Decedent's Race--Black or African American</summary>
        [IJEField(45, 272, 1, "Decedent's Race--Black or African American", "RACE2", 1)]
        public string RACE2
        {
            get
            {
                return Get_Race("2054-5", "Black or African American") ? "Y" : "N";
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    if (value == "Y")
                    {
                        Set_Race("2054-5", "Black or African American");
                    }
                }
            }
        }

        /// <summary>Decedent's Race--American Indian or Alaska Native</summary>
        [IJEField(46, 273, 1, "Decedent's Race--American Indian or Alaska Native", "RACE3", 1)]
        public string RACE3
        {
            get
            {
                return Get_Race("1002-5", "American Indian or Alaska Native") ? "Y" : "N";
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    if (value == "Y")
                    {
                        Set_Race("1002-5", "American Indian or Alaska Native");
                    }
                }
            }
        }

        /// <summary>Decedent's Race--Asian Indian</summary>
        [IJEField(47, 274, 1, "Decedent's Race--Asian Indian", "RACE4", 1)]
        public string RACE4
        {
            get
            {
                return Get_Race("2029-7", "Asian Indian") ? "Y" : "N";
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    if (value == "Y")
                    {
                        Set_Race("2029-7", "Asian Indian");
                    }
                }
            }
        }

        /// <summary>Decedent's Race--Chinese</summary>
        [IJEField(48, 275, 1, "Decedent's Race--Chinese", "RACE5", 1)]
        public string RACE5
        {
            get
            {
                return Get_Race("2034-7", "Chinese") ? "Y" : "N";
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    if (value == "Y")
                    {
                        Set_Race("2034-7", "Chinese");
                    }
                }
            }
        }

        /// <summary>Decedent's Race--Filipino</summary>
        [IJEField(49, 276, 1, "Decedent's Race--Filipino", "RACE6", 1)]
        public string RACE6
        {
            get
            {
                return Get_Race("2036-2", "Filipino") ? "Y" : "N";
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    if (value == "Y")
                    {
                        Set_Race("2036-2", "Filipino");
                    }
                }
            }
        }

        /// <summary>Decedent's Race--Japanese</summary>
        [IJEField(50, 277, 1, "Decedent's Race--Japanese", "RACE7", 1)]
        public string RACE7
        {
            get
            {
                return Get_Race("2039-6", "Japanese") ? "Y" : "N";
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    if (value == "Y")
                    {
                        Set_Race("2039-6", "Japanese");
                    }
                }
            }
        }

        /// <summary>Decedent's Race--Korean</summary>
        [IJEField(51, 278, 1, "Decedent's Race--Korean", "RACE8", 1)]
        public string RACE8
        {
            get
            {
                return Get_Race("2040-4", "Korean") ? "Y" : "N";
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    if (value == "Y")
                    {
                        Set_Race("2040-4", "Korean");
                    }
                }
            }
        }

        /// <summary>Decedent's Race--Vietnamese</summary>
        [IJEField(52, 279, 1, "Decedent's Race--Vietnamese", "RACE9", 1)]
        public string RACE9
        {
            get
            {
                return Get_Race("2047-9", "Vietnamese") ? "Y" : "N";
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    if (value == "Y")
                    {
                        Set_Race("2047-9", "Vietnamese");
                    }
                }
            }
        }

        /// <summary>Decedent's Race--Other Asian</summary>
        [IJEField(53, 280, 1, "Decedent's Race--Other Asian", "RACE10", 1)]
        public string RACE10
        {
            get
            {
                return Get_Race_A_Literals().Length > 0 ? "Y" : "N";
            }
            set
            {
                // NOOP
            }
        }

        /// <summary>Decedent's Race--Native Hawaiian</summary>
        [IJEField(54, 281, 1, "Decedent's Race--Native Hawaiian", "RACE11", 1)]
        public string RACE11
        {
            get
            {
                return Get_Race("2079-2", "Native Hawaiian") ? "Y" : "N";
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    if (value == "Y")
                    {
                        Set_Race("2079-2", "Native Hawaiian");
                    }
                }
            }
        }

        /// <summary>Decedent's Race--Guamanian or Chamorro</summary>
        [IJEField(55, 282, 1, "Decedent's Race--Guamanian or Chamorro", "RACE12", 1)]
        public string RACE12
        {
            get
            {
                return Get_Race("2086-7", "Guamanian or Chamorro") ? "Y" : "N";
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    if (value == "Y")
                    {
                        Set_Race("2086-7", "Guamanian or Chamorro");
                    }
                }
            }
        }

        /// <summary>Decedent's Race--Samoan</summary>
        [IJEField(56, 283, 1, "Decedent's Race--Samoan", "RACE13", 1)]
        public string RACE13
        {
            get
            {
                return Get_Race("2080-0", "Samoan") ? "Y" : "N";
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    if (value == "Y")
                    {
                        Set_Race("2080-0", "Samoan");
                    }
                }
            }
        }

        /// <summary>Decedent's Race--Other Pacific Islander</summary>
        [IJEField(57, 284, 1, "Decedent's Race--Other Pacific Islander", "RACE14", 1)]
        public string RACE14
        {
            get
            {
                return Get_Race_NHOPI_Literals().Length > 0 ? "Y" : "N";
            }
            set
            {
                // NOOP
            }
        }

        /// <summary>Decedent's Race--Other</summary>
        [IJEField(58, 285, 1, "Decedent's Race--Other", "RACE15", 1)]
        public string RACE15
        {
            get
            {
                return Get_Race_OTHER_Literals().Length > 0 ? "Y" : "N";
            }
            set
            {
                // NOOP
            }
        }

        /// <summary>Decedent's Race--First American Indian or Alaska Native Literal</summary>
        [IJEField(59, 286, 30, "Decedent's Race--First American Indian or Alaska Native Literal", "RACE16", 1)]
        public string RACE16
        {
            get
            {
                string[] literals = Get_Race_AIAN_Literals();
                if (literals.Length > 0)
                {
                    return literals[0];
                }
                return "";
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    string name = value.Trim();
                    string code = dataLookup.AIANRaceNameToRaceCode(name);
                    if (!String.IsNullOrWhiteSpace(code) && !String.IsNullOrWhiteSpace(name))
                    {
                        Set_Race(code, name);
                    }
                }
            }
        }

        /// <summary>Decedent's Race--Second American Indian or Alaska Native Literal</summary>
        [IJEField(60, 316, 30, "Decedent's Race--Second American Indian or Alaska Native Literal", "RACE17", 1)]
        public string RACE17
        {
            get
            {
                string[] literals = Get_Race_AIAN_Literals();
                if (literals.Length > 1)
                {
                    return literals[1];
                }
                return "";
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    string name = value.Trim();
                    string code = dataLookup.AIANRaceNameToRaceCode(name);
                    if (!String.IsNullOrWhiteSpace(code) && !String.IsNullOrWhiteSpace(name))
                    {
                        Set_Race(code, name);
                    }
                }
            }
        }

        /// <summary>Decedent's Race--First Other Asian Literal</summary>
        [IJEField(61, 346, 30, "Decedent's Race--First Other Asian Literal", "RACE18", 1)]
        public string RACE18
        {
            get
            {
                string[] literals = Get_Race_A_Literals();
                if (literals.Length > 0)
                {
                    return literals[0];
                }
                return "";
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    string name = value.Trim();
                    string code = dataLookup.ARaceNameToRaceCode(name);
                    if (!String.IsNullOrWhiteSpace(code) && !String.IsNullOrWhiteSpace(name))
                    {
                        Set_Race(code, name);
                    }
                }
            }
        }

        /// <summary>Decedent's Race--Second Other Asian Literal</summary>
        [IJEField(62, 376, 30, "Decedent's Race--Second Other Asian Literal", "RACE19", 1)]
        public string RACE19
        {
            get
            {
                string[] literals = Get_Race_A_Literals();
                if (literals.Length > 1)
                {
                    return literals[1];
                }
                return "";
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    string name = value.Trim();
                    string code = dataLookup.ARaceNameToRaceCode(name);
                    if (!String.IsNullOrWhiteSpace(code) && !String.IsNullOrWhiteSpace(name))
                    {
                        Set_Race(code, name);
                    }
                }
            }
        }

        /// <summary>Decedent's Race--First Other Pacific Islander Literal</summary>
        [IJEField(63, 406, 30, "Decedent's Race--First Other Pacific Islander Literal", "RACE20", 1)]
        public string RACE20
        {
            get
            {
                string[] literals = Get_Race_NHOPI_Literals();
                if (literals.Length > 0)
                {
                    return literals[0];
                }
                return "";
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    string name = value.Trim();
                    string code = dataLookup.NHOPIRaceNameToRaceCode(name);
                    if (!String.IsNullOrWhiteSpace(code) && !String.IsNullOrWhiteSpace(name))
                    {
                        Set_Race(code, name);
                    }
                }
            }
        }

        /// <summary>Decedent's Race--Second Other Pacific Islander Literalr</summary>
        [IJEField(64, 436, 30, "Decedent's Race--Second Other Pacific Islander Literal", "RACE21", 1)]
        public string RACE21
        {
            get
            {
                string[] literals = Get_Race_NHOPI_Literals();
                if (literals.Length > 1)
                {
                    return literals[1];
                }
                return "";
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    string name = value.Trim();
                    string code = dataLookup.NHOPIRaceNameToRaceCode(name);
                    if (!String.IsNullOrWhiteSpace(code) && !String.IsNullOrWhiteSpace(name))
                    {
                        Set_Race(code, name);
                    }
                }
            }
        }

        /// <summary>Decedent's Race--First Other Literal</summary>
        [IJEField(65, 466, 30, "Decedent's Race--First Other Literal", "RACE22", 1)]
        public string RACE22
        {
            get
            {
                string[] literals = Get_Race_OTHER_Literals();
                if (literals.Length > 0)
                {
                    return literals[0];
                }
                return "";
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    string name = value.Trim();
                    string code = dataLookup.WRaceNameToRaceCode(name);
                    if (String.IsNullOrWhiteSpace(code))
                    {
                        code = dataLookup.BAARaceNameToRaceCode(name);
                    }
                    if (!String.IsNullOrWhiteSpace(code) && !String.IsNullOrWhiteSpace(name))
                    {
                        Set_Race(code, name);
                    }
                }
            }
        }

        /// <summary>Decedent's Race--Second Other Literal</summary>
        [IJEField(66, 496, 30, "Decedent's Race--Second Other Literal", "RACE23", 1)]
        public string RACE23
        {
            get
            {
                string[] literals = Get_Race_OTHER_Literals();
                if (literals.Length > 1)
                {
                    return literals[1];
                }
                return "";
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    string name = value.Trim();
                    string code = dataLookup.WRaceNameToRaceCode(name);
                    if (String.IsNullOrWhiteSpace(code))
                    {
                        code = dataLookup.BAARaceNameToRaceCode(name);
                    }
                    if (!String.IsNullOrWhiteSpace(code) && !String.IsNullOrWhiteSpace(name))
                    {
                        Set_Race(code, name);
                    }
                }
            }
        }

        /// <summary>Decedent's Race--Missing</summary>
        [IJEField(83, 574, 1, "Decedent's Race--Missing", "RACE_MVR", 1)]
        public string RACE_MVR
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
                if (!String.IsNullOrWhiteSpace(value))
                {
                    LeftJustified_Set("OCCUP", "UsualOccupation", value.Trim());
                }
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
                if (!String.IsNullOrWhiteSpace(value))
                {
                    LeftJustified_Set("INDUST", "UsualIndustry", value.Trim());
                }
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

        /// <summary>Date of Registration--Year</summary>
        [IJEField(95, 689, 4, "Date of Registration--Year", "DOR_YR", 1)]
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
        [IJEField(96, 693, 2, "Date of Registration--Month", "DOR_MO", 1)]
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
        [IJEField(97, 695, 2, "Date of Registration--Day", "DOR_DY", 1)]
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

        /// <summary>Manner of Death</summary>
        [IJEField(99, 701, 1, "Manner of Death", "MANNER", 1)]
        public string MANNER
        {
            get
            {
                string code = Dictionary_Get_Full("MANNER", "MannerOfDeathType", "code");
                switch (code)
                {
                    case "38605008": // Natural
                        return "N";
                    case "7878000": // Accident
                        return "A";
                    case "44301001": // Suicide
                        return "S";
                    case "27935005": // Homicide
                        return "H";
                    case "185973002": // Pending Investigation
                        return "P";
                    case "65037004": // Could not be determined
                        return "C";
                }
                return "";
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    switch (value)
                    {
                        case "N":
                            Dictionary_Set("MANNER", "MannerOfDeathType", "code", "38605008");
                            Dictionary_Set("MANNER", "MannerOfDeathType", "system", CodeSystems.SCT);
                            Dictionary_Set("MANNER", "MannerOfDeathType", "display", "Natural death");
                            break;
                        case "A":
                            Dictionary_Set("MANNER", "MannerOfDeathType", "code", "7878000");
                            Dictionary_Set("MANNER", "MannerOfDeathType", "system", CodeSystems.SCT);
                            Dictionary_Set("MANNER", "MannerOfDeathType", "display", "Accidental death");
                            break;
                        case "S":
                            Dictionary_Set("MANNER", "MannerOfDeathType", "code", "44301001");
                            Dictionary_Set("MANNER", "MannerOfDeathType", "system", CodeSystems.SCT);
                            Dictionary_Set("MANNER", "MannerOfDeathType", "display", "Suicide");
                            break;
                        case "H":
                            Dictionary_Set("MANNER", "MannerOfDeathType", "code", "27935005");
                            Dictionary_Set("MANNER", "MannerOfDeathType", "system", CodeSystems.SCT);
                            Dictionary_Set("MANNER", "MannerOfDeathType", "display", "Homicide");
                            break;
                        case "P":
                            Dictionary_Set("MANNER", "MannerOfDeathType", "code", "185973002");
                            Dictionary_Set("MANNER", "MannerOfDeathType", "system", CodeSystems.SCT);
                            Dictionary_Set("MANNER", "MannerOfDeathType", "display", "Patient awaiting investigation");
                            break;
                        case "C":
                            Dictionary_Set("MANNER", "MannerOfDeathType", "code", "65037004");
                            Dictionary_Set("MANNER", "MannerOfDeathType", "system", CodeSystems.SCT);
                            Dictionary_Set("MANNER", "MannerOfDeathType", "display", "Death, manner undetermined");
                            break;
                    }
                }
            }
        }

        /// <summary>Place of Injury (computer generated)</summary>
        [IJEField(102, 704, 1, "Place of Injury (computer generated)", "INJPL", 1)]
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

        /// <summary>Was Autopsy performed</summary>
        [IJEField(108, 976, 1, "Was Autopsy performed", "AUTOP", 1)]
        public string AUTOP
        {
            get
            {
                string code = Dictionary_Get_Full("AUTOP", "AutopsyPerformedIndicator", "code");
                switch (code)
                {
                    case "Y": // Yes
                        return "Y";
                    case "N": // No
                        return "N";
                    case "UNK": // Unknown
                        return "U";
                }
                return "";
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    switch (value)
                    {
                        case "Y":
                            Dictionary_Set("AUTOP", "AutopsyPerformedIndicator", "code", "Y");
                            Dictionary_Set("AUTOP", "AutopsyPerformedIndicator", "system", CodeSystems.PH_YesNo_HL7_2x);
                            Dictionary_Set("AUTOP", "AutopsyPerformedIndicator", "display", "Yes");
                            break;
                        case "N":
                            Dictionary_Set("AUTOP", "AutopsyPerformedIndicator", "code", "N");
                            Dictionary_Set("AUTOP", "AutopsyPerformedIndicator", "system", CodeSystems.PH_YesNo_HL7_2x);
                            Dictionary_Set("AUTOP", "AutopsyPerformedIndicator", "display", "No");
                            break;
                        case "U":
                            Dictionary_Set("AUTOP", "AutopsyPerformedIndicator", "code", "UNK");
                            Dictionary_Set("AUTOP", "AutopsyPerformedIndicator", "system", CodeSystems.PH_NullFlavor_HL7_V3);
                            Dictionary_Set("AUTOP", "AutopsyPerformedIndicator", "display", "Unknown");
                            break;
                    }
                }
            }
        }

        /// <summary>Were Autopsy Findings Available to Complete the Cause of Death?</summary>
        [IJEField(109, 977, 1, "Were Autopsy Findings Available to Complete the Cause of Death?", "AUTOPF", 1)]
        public string AUTOPF
        {
            get
            {
                string code = Dictionary_Get_Full("AUTOPF", "AutopsyResultsAvailable", "code");
                switch (code)
                {
                    case "Y": // Yes
                        return "Y";
                    case "N": // No
                        return "N";
                    case "UNK": // Unknown
                        return "U";
                }
                return "";
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    switch (value)
                    {
                        case "Y":
                            Dictionary_Set("AUTOPF", "AutopsyResultsAvailable", "code", "Y");
                            Dictionary_Set("AUTOPF", "AutopsyResultsAvailable", "system", CodeSystems.PH_YesNo_HL7_2x);
                            Dictionary_Set("AUTOPF", "AutopsyResultsAvailable", "display", "Yes");
                            break;
                        case "N":
                            Dictionary_Set("AUTOPF", "AutopsyResultsAvailable", "code", "N");
                            Dictionary_Set("AUTOPF", "AutopsyResultsAvailable", "system", CodeSystems.PH_YesNo_HL7_2x);
                            Dictionary_Set("AUTOPF", "AutopsyResultsAvailable", "display", "No");
                            break;
                        case "U":
                            Dictionary_Set("AUTOPF", "AutopsyResultsAvailable", "code", "UNK");
                            Dictionary_Set("AUTOPF", "AutopsyResultsAvailable", "system", CodeSystems.PH_NullFlavor_HL7_V3);
                            Dictionary_Set("AUTOPF", "AutopsyResultsAvailable", "display", "Unknown");
                            break;
                    }
                }
            }
        }

        /// <summary>Did Tobacco Use Contribute to Death?</summary>
        /// Value set contains 5 values (SCT/No, SCT/Yes, SCT/Probably, NullFlavor/UNK,  NullFlavor/NASK - C)
        [IJEField(110, 978, 1, "Did Tobacco Use Contribute to Death?", "TOBAC", 1)]
        public string TOBAC
        {
            // Value set contains 5 values (SCT/No, SCT/Yes, SCT/Probably, NullFlavor/UNK,  NullFlavor/NASK - C)
            get
            {
                string code = Dictionary_Get_Full("TOBAC", "TobaccoUse", "code");
                switch (code)
                {
                    case "373066001": // Yes
                        return "Y";
                    case "373067005": // No
                        return "N";
                    case "2931005": // Probably
                        return "P";
                    case "UNK": // Unknown
                        return "U";
                    case "NASK": // Not asked
                        return "C"; // maps to not on certificate
                }
                return "";
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    switch (value)
                    {
                        case "Y":
                            Dictionary_Set("TOBAC", "TobaccoUse", "code", "373066001");
                            Dictionary_Set("TOBAC", "TobaccoUse", "system", CodeSystems.SCT);
                            Dictionary_Set("TOBAC", "TobaccoUse", "display", "Yes");
                            break;
                        case "N":
                            Dictionary_Set("TOBAC", "TobaccoUse", "code", "373067005");
                            Dictionary_Set("TOBAC", "TobaccoUse", "system", CodeSystems.SCT);
                            Dictionary_Set("TOBAC", "TobaccoUse", "display", "No");
                            break;
                        case "P":
                            Dictionary_Set("TOBAC", "TobaccoUse", "code", "2931005");
                            Dictionary_Set("TOBAC", "TobaccoUse", "system", CodeSystems.SCT);
                            Dictionary_Set("TOBAC", "TobaccoUse", "display", "Probably");
                            break;
                        case "U":
                            Dictionary_Set("TOBAC", "TobaccoUse", "code", "UNK");
                            Dictionary_Set("TOBAC", "TobaccoUse", "system", CodeSystems.PH_NullFlavor_HL7_V3);
                            Dictionary_Set("TOBAC", "TobaccoUse", "display", "Unknown");
                            break;
                        case "C":
                            Dictionary_Set("TOBAC", "TobaccoUse", "code", "NASK");
                            Dictionary_Set("TOBAC", "TobaccoUse", "system", CodeSystems.PH_NullFlavor_HL7_V3);
                            Dictionary_Set("TOBAC", "TobaccoUse", "display", "Not asked");
                            break;
                    }
                }
            }
        }

        /// <summary>Pregnancy</summary>
        [IJEField(111, 979, 1, "Pregnancy", "PREG", 1)]
        public string PREG
        {
            get
            {
                string code = Dictionary_Get_Full("PREG", "PregnancyStatus", "code");
                switch (code)
                {
                    case "PHC1260": // Not pregnant within past year
                        return "1";
                    case "PHC1261": // Pregnant at time of death
                        return "2";
                    case "PHC1262": // Not pregnant, but pregnant within 42 days of death
                        return "3";
                    case "PHC1263": // Not pregnant, but pregnant 43 days to 1 year before death
                        return "4";
                    case "PHC1264": // Unknown if pregnant within the past year
                        return "9";
                    case "NA": // Not applicable
                        return "8";
                }
                return "";
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    switch (value)
                    {
                        case "1":
                            Dictionary_Set("PREG", "PregnancyStatus", "code", "PHC1260");
                            Dictionary_Set("PREG", "PregnancyStatus", "system", CodeSystems.PH_PHINVS_CDC);
                            Dictionary_Set("PREG", "PregnancyStatus", "display", "Not pregnant within past year");
                            break;
                        case "2":
                            Dictionary_Set("PREG", "PregnancyStatus", "code", "PHC1261");
                            Dictionary_Set("PREG", "PregnancyStatus", "system", CodeSystems.PH_PHINVS_CDC);
                            Dictionary_Set("PREG", "PregnancyStatus", "display", "Pregnant at time of death");
                            break;
                        case "3":
                            Dictionary_Set("PREG", "PregnancyStatus", "code", "PHC1262");
                            Dictionary_Set("PREG", "PregnancyStatus", "system", CodeSystems.PH_PHINVS_CDC);
                            Dictionary_Set("PREG", "PregnancyStatus", "display", "Not pregnant, but pregnant within 42 days of death");
                            break;
                        case "4":
                            Dictionary_Set("PREG", "PregnancyStatus", "code", "PHC1263");
                            Dictionary_Set("PREG", "PregnancyStatus", "system", CodeSystems.PH_PHINVS_CDC);
                            Dictionary_Set("PREG", "PregnancyStatus", "display", "Not pregnant, but pregnant 43 days to 1 year before death");
                            break;
                        case "9":
                            Dictionary_Set("PREG", "PregnancyStatus", "code", "PHC1264");
                            Dictionary_Set("PREG", "PregnancyStatus", "system", CodeSystems.PH_PHINVS_CDC);
                            Dictionary_Set("PREG", "PregnancyStatus", "display", "Unknown if pregnant within the past year");
                            break;
                        case "8":
                            Dictionary_Set("PREG", "PregnancyStatus", "code", "NA");
                            Dictionary_Set("PREG", "PregnancyStatus", "system", CodeSystems.PH_NullFlavor_HL7_V3);
                            Dictionary_Set("PREG", "PregnancyStatus", "display", "Not applicable");
                            break;
                    }
                }
            }
        }

        /// <summary>If Female--Edit Flag: From EDR only</summary>
        [IJEField(112, 980, 1, "If Female--Edit Flag: From EDR only", "PREG_BYPASS", 1)]
        public string PREG_BYPASS
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

        /// <summary>Date of injury--month</summary>
        [IJEField(113, 981, 2, "Date of injury--month", "DOI_MO", 1)]
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
        [IJEField(114, 983, 2, "Date of injury--day", "DOI_DY", 1)]
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
        [IJEField(115, 985, 4, "Date of injury--year", "DOI_YR", 1)]
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
        [IJEField(116, 989, 4, "Time of injury", "TOI_HR", 1)]
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
        [IJEField(117, 993, 1, "Injury at work", "WORKINJ", 1)]
        public string WORKINJ
        {
            get
            {
                string code = Dictionary_Get_Full("WORKINJ", "InjuryAtWork", "code");
                switch (code)
                {
                    case "Y": // Yes
                        return "Y";
                    case "N": // No
                        return "N";
                    case "UNK": // Unknown
                        return "U";
                }
                return "";
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    switch (value)
                    {
                        case "Y":
                            Dictionary_Set("WORKINJ", "InjuryAtWork", "code", "Y");
                            Dictionary_Set("WORKINJ", "InjuryAtWork", "system", CodeSystems.PH_YesNo_HL7_2x);
                            Dictionary_Set("WORKINJ", "InjuryAtWork", "display", "Yes");
                            break;
                        case "N":
                            Dictionary_Set("WORKINJ", "InjuryAtWork", "code", "N");
                            Dictionary_Set("WORKINJ", "InjuryAtWork", "system", CodeSystems.PH_YesNo_HL7_2x);
                            Dictionary_Set("WORKINJ", "InjuryAtWork", "display", "No");
                            break;
                        case "U":
                            Dictionary_Set("WORKINJ", "InjuryAtWork", "code", "UNK");
                            Dictionary_Set("WORKINJ", "InjuryAtWork", "system", CodeSystems.PH_NullFlavor_HL7_V3);
                            Dictionary_Set("WORKINJ", "InjuryAtWork", "display", "Unknown");
                            break;
                    }
                }
            }
        }

        /// <summary>Title of Certifier</summary>
        [IJEField(118, 994, 30, "Title of Certifier", "CERTL", 1)]
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
        [IJEField(119, 1024, 1, "Activity at time of death (computer generated)", "INACT", 1)]
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

        /// <summary>Time of Injury Unit</summary>
        [IJEField(125, 1075, 1, "Time of Injury Unit", "TOI_UNIT", 1)]
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

        /// <summary>Decedent ever served in Armed Forces?</summary>
        [IJEField(127, 1081, 1, "Decedent ever served in Armed Forces?", "ARMEDF", 1)]
        public string ARMEDF
        {
            get
            {
                string code = Dictionary_Get_Full("ARMEDF", "MilitaryService", "code");
                switch (code)
                {
                    case "Y": // Yes
                        return "Y";
                    case "N": // No
                        return "N";
                    case "UNK": // Unknown
                        return "U";
                }
                return "";
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    switch (value)
                    {
                        case "Y":
                            Dictionary_Set("ARMEDF", "MilitaryService", "code", "Y");
                            Dictionary_Set("ARMEDF", "MilitaryService", "system", CodeSystems.PH_YesNo_HL7_2x);
                            Dictionary_Set("ARMEDF", "MilitaryService", "display", "Yes");
                            break;
                        case "N":
                            Dictionary_Set("ARMEDF", "MilitaryService", "code", "N");
                            Dictionary_Set("ARMEDF", "MilitaryService", "system", CodeSystems.PH_YesNo_HL7_2x);
                            Dictionary_Set("ARMEDF", "MilitaryService", "display", "No");
                            break;
                        case "U":
                            Dictionary_Set("ARMEDF", "MilitaryService", "code", "UNK");
                            Dictionary_Set("ARMEDF", "MilitaryService", "system", CodeSystems.PH_NullFlavor_HL7_V3);
                            Dictionary_Set("ARMEDF", "MilitaryService", "display", "Unknown");
                            break;
                    }
                }
            }
        }

        /// <summary>Death Institution name</summary>
        [IJEField(128, 1082, 30, "Death Institution name", "DINSTI", 1)]
        public string DINSTI
        {
            get
            {
                return LeftJustified_Get("DINSTI", "DeathLocationName");
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    LeftJustified_Set("DINSTI", "DeathLocationName", value.Trim());
                }
            }
        }

        /// <summary>Long String address for place of death</summary>
        [IJEField(129, 1112, 50, "Long String address for place of death", "ADDRESS_D", 1)]
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

        /// <summary>Place of death. City or Town name</summary>
        [IJEField(135, 1252, 28, "Place of death. City or Town name", "CITYTEXT_D", 1)]
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
        [IJEField(136, 1280, 28, "Place of death. State name literal", "STATETEXT_D", 1)]
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
        [IJEField(137, 1308, 9, "Place of death. Zip code", "ZIP9_D", 1)]
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
        [IJEField(138, 1317, 28, "Place of death. County of Death", "COUNTYTEXT_D", 2)]
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
        [IJEField(139, 1345, 5, "Place of death. City FIPS code", "CITYCODE_D", 1)]
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

        /// <summary>Decedent's Residence - City or Town name</summary>
        [IJEField(151, 1560, 28, "Decedent's Residence - City or Town name", "CITYTEXT_R", 3)]
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
        [IJEField(152, 1588, 9, "Decedent's Residence - ZIP code", "ZIP9_R", 1)]
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
        [IJEField(153, 1597, 28, "Decedent's Residence - County", "COUNTYTEXT_R", 1)]
        public string COUNTYTEXT_R
        {
            get
            {
                return Dictionary_Geo_Get("COUNTYTEXT_R", "Residence", "address", "county", false);
            }
            set
            {
                // NOOP
            }
        }

        /// <summary>Decedent's Residence - State name</summary>
        [IJEField(154, 1625, 28, "Decedent's Residence - State name", "STATETEXT_R", 1)]
        public string STATETEXT_R
        {
            get
            {
                return Dictionary_Geo_Get("STATETEXT_R", "Residence", "address", "state", false);
            }
            set
            {
                // NOOP
            }
        }

        /// <summary>Decedent's Residence - COUNTRY name</summary>
        [IJEField(155, 1653, 28, "Decedent's Residence - COUNTRY name", "COUNTRYTEXT_R", 1)]
        public string COUNTRYTEXT_R
        {
            get
            {
                return Dictionary_Geo_Get("COUNTRYTEXT_R", "Residence", "address", "country", false); // This is Now just the two letter code.  Need to map it to country name
            }
            set
            {
                // NOOP
            }
        }

        /// <summary>Long string address for decedent's place of residence same as above but allows states to choose the way they capture information.</summary>
        [IJEField(156, 1681, 50, "Long string address for decedent's place of residence same as above but allows states to choose the way they capture information.", "ADDRESS_R", 1)]
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

        /// <summary>Middle Name of Decedent</summary>
        [IJEField(165, 1808, 50, "Middle Name of Decedent", "DMIDDLE", 2)]
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
        /// <summary>Mother's First Name</summary>
        [IJEField(168, 1958, 50, "Mother's First Name", "DMOMF", 1)]
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
        /// <summary>Mother's Maiden Surname</summary>
        [IJEField(170, 2058, 50, "Mother's Maiden Surname", "DMOMMDN", 1)]
        public string DMOMMDN
        {
            get
            {
                return LeftJustified_Get("DMOMMDN", "MotherMaidenName");
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    LeftJustified_Set("DMOMMDN", "MotherMaidenName", value.Trim());
                }
            }
        }

        /// <summary>Was case Referred to Medical Examiner/Coroner?</summary>
        [IJEField(171, 2108, 1, "Was case Referred to Medical Examiner/Coroner?", "REFERRED", 1)]
        public string REFERRED
        {
            get
            {
                return Get_YNU("ExaminerContactedBoolean");
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Set_YNU("ExaminerContactedBoolean", value);
                }
            }
        }

        /// <summary>Place of Injury- literal</summary>
        [IJEField(172, 2109, 50, "Place of Injury- literal", "POILITRL", 1)]
        public string POILITRL
        {
            get
            {
                string code = Dictionary_Get_Full("POILITRL", "InjuryPlace", "code");
                switch (code)
                {
                    case "0":
                        return "Home";
                    case "1":
                        return "Residential Institution";
                    case "2":
                        return "School, Other Institutions, Public Administrative Area";
                    case "3":
                        return "Sports and Atheletics Area";
                    case "4":
                        return "Street/Highway";
                    case "5":
                        return "Trade and Service Area";
                    case "6":
                        return "Industrial and Construction Area";
                    case "7":
                        return "Farm";
                    case "8":
                        return "Other Specified Place";
                    case "9":
                        return "Unspecified Place";
                    case "NI":
                        return "NoInformation";
                }
                return "";
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    switch (value.Trim())
                    {
                        case "Home":
                            Dictionary_Set("POILITRL", "InjuryPlace", "code", "0");
                            Dictionary_Set("POILITRL", "InjuryPlace", "system", CodeSystems.PH_PlaceOfOccurrence_ICD_10_WHO);
                            Dictionary_Set("POILITRL", "InjuryPlace", "display", value.Trim());
                            break;
                        case "Residential Institution":
                            Dictionary_Set("POILITRL", "InjuryPlace", "code", "1");
                            Dictionary_Set("POILITRL", "InjuryPlace", "system", CodeSystems.PH_PlaceOfOccurrence_ICD_10_WHO);
                            Dictionary_Set("POILITRL", "InjuryPlace", "display", value.Trim());
                            break;
                        case "School, Other Institutions, Public Administrative Area":
                            Dictionary_Set("POILITRL", "InjuryPlace", "code", "2");
                            Dictionary_Set("POILITRL", "InjuryPlace", "system", CodeSystems.PH_PlaceOfOccurrence_ICD_10_WHO);
                            Dictionary_Set("POILITRL", "InjuryPlace", "display", value.Trim());
                            break;
                        case "Sports and Atheletics Area":
                            Dictionary_Set("POILITRL", "InjuryPlace", "code", "3");
                            Dictionary_Set("POILITRL", "InjuryPlace", "system", CodeSystems.PH_PlaceOfOccurrence_ICD_10_WHO);
                            Dictionary_Set("POILITRL", "InjuryPlace", "display", value.Trim());
                            break;
                        case "Street/Highway":
                            Dictionary_Set("POILITRL", "InjuryPlace", "code", "4");
                            Dictionary_Set("POILITRL", "InjuryPlace", "system", CodeSystems.PH_PlaceOfOccurrence_ICD_10_WHO);
                            Dictionary_Set("POILITRL", "InjuryPlace", "display", value.Trim());
                            break;
                        case "Trade and Service Area":
                            Dictionary_Set("POILITRL", "InjuryPlace", "code", "5");
                            Dictionary_Set("POILITRL", "InjuryPlace", "system", CodeSystems.PH_PlaceOfOccurrence_ICD_10_WHO);
                            Dictionary_Set("POILITRL", "InjuryPlace", "display", value.Trim());
                            break;
                        case "Industrial and Construction Area":
                            Dictionary_Set("POILITRL", "InjuryPlace", "code", "6");
                            Dictionary_Set("POILITRL", "InjuryPlace", "system", CodeSystems.PH_PlaceOfOccurrence_ICD_10_WHO);
                            Dictionary_Set("POILITRL", "InjuryPlace", "display", value.Trim());
                            break;
                        case "Farm":
                            Dictionary_Set("POILITRL", "InjuryPlace", "code", "7");
                            Dictionary_Set("POILITRL", "InjuryPlace", "system", CodeSystems.PH_PlaceOfOccurrence_ICD_10_WHO);
                            Dictionary_Set("POILITRL", "InjuryPlace", "display", value.Trim());
                            break;
                        case "Other Specified Place":
                        default:    // if the value is non-null, but not one of the expected string, it is 'other specified place'
                            Dictionary_Set("POILITRL", "InjuryPlace", "code", "8");
                            Dictionary_Set("POILITRL", "InjuryPlace", "system", CodeSystems.PH_PlaceOfOccurrence_ICD_10_WHO);
                            Dictionary_Set("POILITRL", "InjuryPlace", "display", value.Trim());
                            break;
                        case "Unspecified Place":
                            Dictionary_Set("POILITRL", "InjuryPlace", "code", "9");
                            Dictionary_Set("POILITRL", "InjuryPlace", "system", CodeSystems.PH_PlaceOfOccurrence_ICD_10_WHO);
                            Dictionary_Set("POILITRL", "InjuryPlace", "display", value.Trim());
                            break;
                    }
                }
            }
        }

        /// <summary>Describe How Injury Occurred</summary>
        [IJEField(173, 2159, 250, "Describe How Injury Occurred", "HOWINJ", 1)]
        public string HOWINJ
        {
            get
            {
                return LeftJustified_Get("HOWINJ", "InjuryDescription");
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    LeftJustified_Set("HOWINJ", "InjuryDescription", value.Trim());
                }
            }
        }

        /// <summary>If Transportation Accident, Specify</summary>
        [IJEField(174, 2409, 30, "If Transportation Accident, Specify", "TRANSPRT", 1)]
        public string TRANSPRT
        {
            get
            {
                string code = Dictionary_Get_Full("TRANSPRT", "TransportationRole", "code");
                switch (code)
                {
                    case "236320001": // Vehicle driver
                        return "DR";
                    case "257500003": // Passenger
                        return "PA";
                    case "257518000": // Pedestrian
                        return "PE";
                }
                return "";
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    switch (value.Trim())
                    {
                        case "DR":
                            Dictionary_Set("TRANSPRT", "TransportationRole", "code", "236320001");
                            Dictionary_Set("TRANSPRT", "TransportationRole", "system", CodeSystems.SCT);
                            Dictionary_Set("TRANSPRT", "TransportationRole", "display", "Vehicle driver");
                            break;
                        case "PA":
                            Dictionary_Set("TRANSPRT", "TransportationRole", "code", "257500003");
                            Dictionary_Set("TRANSPRT", "TransportationRole", "system", CodeSystems.SCT);
                            Dictionary_Set("TRANSPRT", "TransportationRole", "display", "Passenger");
                            break;
                        case "PE":
                            Dictionary_Set("TRANSPRT", "TransportationRole", "code", "257518000");
                            Dictionary_Set("TRANSPRT", "TransportationRole", "system", CodeSystems.SCT);
                            Dictionary_Set("TRANSPRT", "TransportationRole", "display", "Pedestrian");
                            break;
                    }
                }
            }
        }

        /// <summary>County of Injury - literal</summary>
        [IJEField(175, 2439, 28, "County of Injury - literal", "COUNTYTEXT_I", 1)]
        public string COUNTYTEXT_I
        {
            get
            {
                return Dictionary_Geo_Get("COUNTYTEXT_I", "InjuryLocationAddress", "address", "county", false);
            }
            set
            {
                // NOOP
            }
        }

        /// <summary>County of Injury code</summary>
        [IJEField(176, 2467, 3, "County of Injury code", "COUNTYCODE_I", 2)]
        public string COUNTYCODE_I
        {
            get
            {
                return Dictionary_Geo_Get("COUNTYCODE_I", "InjuryLocationAddress", "address", "county", true);
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Dictionary_Geo_Set("COUNTYCODE_I", "InjuryLocationAddress", "address", "county", true, value);
                }
            }
        }

        /// <summary>Town/city of Injury - literal</summary>
        [IJEField(177, 2470, 28, "Town/city of Injury - literal", "CITYTEXT_I", 3)]
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
        [IJEField(178, 2498, 5, "Town/city of Injury code", "CITYCODE_I", 3)]
        public string CITYCODE_I
        {
            get
            {
                return Dictionary_Geo_Get("CITYCODE_I", "InjuryLocationAddress", "address", "city", true);
            }
            set
            {
                // NOOP
            }
        }

        /// <summary>State, U.S. Territory or Canadian Province of Injury - code</summary>
        [IJEField(179, 2503, 2, "State, U.S. Territory or Canadian Province of Injury - code", "STATECODE_I", 1)]
        public string STATECODE_I
        {
            get
            {
                return Dictionary_Geo_Get("STATECODE_I", "InjuryLocationAddress", "address", "state", true);
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Dictionary_Geo_Set("STATECODE_I", "InjuryLocationAddress", "address", "state", true, value);
                }
            }
        }

        /// <summary>Cause of Death Part I Line a</summary>
        [IJEField(184, 2542, 120, "Cause of Death Part I Line a", "COD1A", 1)]
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
        [IJEField(185, 2662, 20, "Cause of Death Part I Interval, Line a", "INTERVAL1A", 2)]
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
        [IJEField(186, 2682, 120, "Cause of Death Part I Line b", "COD1B", 3)]
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
        [IJEField(187, 2802, 20, "Cause of Death Part I Interval, Line b", "INTERVAL1B", 4)]
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
        [IJEField(188, 2822, 120, "Cause of Death Part I Line c", "COD1C", 5)]
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
        [IJEField(189, 2942, 20, "Cause of Death Part I Interval, Line c", "INTERVAL1C", 6)]
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
        [IJEField(190, 2962, 120, "Cause of Death Part I Line d", "COD1D", 7)]
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
        [IJEField(191, 3082, 20, "Cause of Death Part I Interval, Line d", "INTERVAL1D", 8)]
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
        [IJEField(192, 3102, 240, "Cause of Death Part II", "OTHERCONDITION", 1)]
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
        [IJEField(193, 3342, 50, "Decedent's Maiden Name", "DMAIDEN", 1)]
        public string DMAIDEN
        {
            get
            {
                return LeftJustified_Get("DMAIDEN", "MaidenName");
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    LeftJustified_Set("DMAIDEN", "MaidenName", value);
                }
            }
        }

        /// <summary>Decedent's Birth Place City - Literal</summary>
        [IJEField(195, 3397, 28, "Decedent's Birth Place City - Literal", "DBPLACECITY", 3)]
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
                    // We've got city, and we probably also have state now - so attempt to find county while we're at it (IJE does NOT include this).
                    string state = Dictionary_Geo_Get("BSTATE", "PlaceOfBirth", "address", "state", true);
                    string county = dataLookup.StateCodeAndCityNameToCountyName(state, value);
                    if (!String.IsNullOrWhiteSpace(county))
                    {
                        Dictionary_Geo_Set("DBPLACECITY", "PlaceOfBirth", "address", "county", false, county);
                    }
                }
            }
        }

        /// <summary>State, U.S. Territory or Canadian Province of Disposition - code</summary>
        [IJEField(201, 3535, 2, "State, U.S. Territory or Canadian Province of Disposition - code", "DISPSTATECD", 1)]
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
        [IJEField(202, 3537, 28, "Disposition State or Territory - Literal", "DISPSTATE", 1)]
        public string DISPSTATE
        {
            get
            {
                return Dictionary_Geo_Get("DISPSTATE", "DispositionLocationAddress", "address", "state", false);
            }
            set
            {
                // NOOP
            }
        }

        /// <summary>Disposition City - Literal</summary>
        [IJEField(204, 3570, 28, "Disposition City - Literal", "DISPCITY", 3)]
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
        [IJEField(205, 3598, 100, "Funeral Facility Name", "FUNFACNAME", 1)]
        public string FUNFACNAME
        {
            get
            {
                return LeftJustified_Get("FUNFACNAME", "FuneralHomeName");
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    LeftJustified_Set("FUNFACNAME", "FuneralHomeName", value.Trim());
                }
            }
        }

        /// <summary>Long string address for Funeral Facility same as above but allows states to choose the way they capture information.</summary>
        [IJEField(212, 3773, 50, "Long string address for Funeral Facility same as above but allows states to choose the way they capture information.", "FUNFACADDRESS", 1)]
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
        [IJEField(213, 3823, 28, "Funeral Facility - City or Town name", "FUNCITYTEXT", 3)]
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
        [IJEField(214, 3851, 2, "State, U.S. Territory or Canadian Province of Funeral Facility - code", "FUNSTATECD", 1)]
        public string FUNSTATECD
        {
            get
            {
                return dataLookup.StateNameToStateCode(Dictionary_Get_Full("FUNSTATECD", "FuneralHomeAddress", "addressState"));
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Dictionary_Set("FUNSTATECD", "FuneralHomeAddress", "addressState", value);
                }
            }
        }

        /// <summary>State, U.S. Territory or Canadian Province of Funeral Facility - literal</summary>
        [IJEField(215, 3853, 28, "State, U.S. Territory or Canadian Province of Funeral Facility - literal", "FUNSTATE", 1)]
        public string FUNSTATE
        {
            get
            {
                return Dictionary_Get("FUNSTATE", "FuneralHomeAddress", "addressState");
            }
            set
            {
                // NOOP
            }
        }

        /// <summary>Funeral Facility - ZIP</summary>
        [IJEField(216, 3881, 9, "Funeral Facility - ZIP", "FUNZIP", 1)]
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
        [IJEField(217, 3890, 8, "Person Pronouncing Date Signed", "PPDATESIGNED", 1)]
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
        [IJEField(218, 3898, 4, "Person Pronouncing Time Pronounced", "PPTIME", 1)]
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
        [IJEField(219, 3902, 50, "Certifier's First Name", "CERTFIRST", 1)]
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

        /// <summary>Certifier's Middle Name</summary>
        [IJEField(220, 3952, 50, "Certifier's Middle Name", "CERTMIDDLE", 2)]
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
        [IJEField(221, 4002, 50, "Certifier's Last Name", "CERTLAST", 3)]
        public string CERTLAST
        {
            get
            {
                return LeftJustified_Get("CERTLAST", "CertifierFamilyName");
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    LeftJustified_Set("CERTLAST", "CertifierFamilyName", value.Trim());
                }
            }
        }

        /// <summary>Certifier's Suffix Name</summary>
        [IJEField(222, 4052, 10, "Certifier's Suffix Name", "CERTSUFFIX", 4)]
        public string CERTSUFFIX
        {
            get
            {
                return LeftJustified_Get("CERTSUFFIX", "CertifierSuffix");
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    LeftJustified_Set("CERTSUFFIX", "CertifierSuffix", value.Trim());
                }
            }
        }

        /// <summary>Long string address for Certifier same as above but allows states to choose the way they capture information.</summary>
        [IJEField(229, 4137, 50, "Long string address for Certifier same as above but allows states to choose the way they capture information.", "CERTADDRESS", 1)]
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
        [IJEField(230, 4187, 28, "Certifier - City or Town name", "CERTCITYTEXT", 2)]
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
                    // We've got city, and we probably also have state now - so attempt to find county while we're at it (IJE does NOT include this).
                    string county = dataLookup.StateCodeAndCityNameToCountyName(CERTSTATECD, value);
                    if (!String.IsNullOrWhiteSpace(county))
                    {
                        Dictionary_Geo_Set("CERTCITYTEXT", "CertifierAddress", "address", "county", false, county);
                        // If we found a county, we know the country.
                        Dictionary_Geo_Set("CERTCITYTEXT", "CertifierAddress", "address", "country", false, "US");
                    }
                }
            }
        }

        /// <summary>State, U.S. Territory or Canadian Province of Certifier - code</summary>
        [IJEField(231, 4215, 2, "State, U.S. Territory or Canadian Province of Certifier - code", "CERTSTATECD", 1)]
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
        [IJEField(232, 4217, 28, "State, U.S. Territory or Canadian Province of Certifier - literal", "CERTSTATE", 1)]
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
        [IJEField(233, 4245, 9, "Certifier - Zip", "CERTZIP", 1)]
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
        [IJEField(234, 4254, 8, "Certifier Date Signed", "CERTDATE", 1)]
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

        /// <summary>State, U.S. Territory or Canadian Province of Injury - literal</summary>
        [IJEField(236, 4270, 28, "State, U.S. Territory or Canadian Province of Injury - literal", "STINJURY", 1)]
        public string STINJURY
        {
            get
            {
                return Dictionary_Geo_Get("STINJURY", "InjuryLocationAddress", "address", "state", false);
            }
            set
            {
                // NOOP
            }
        }

        /// <summary>State, U.S. Territory or Canadian Province of Birth - literal</summary>
        [IJEField(237, 4298, 28, "State, U.S. Territory or Canadian Province of Birth - literal", "STATEBTH", 1)]
        public string STATEBTH
        {
            get
            {
                return Dictionary_Geo_Get("STATEBTH", "PlaceOfBirth", "address", "state", false);
            }
            set
            {
                // NOOP
            }
        }

        /// <summary>Country of Death - Code</summary>
        [IJEField(238, 4326, 2, "Country of Death - Code", "DTHCOUNTRYCD", 1)]
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
        [IJEField(239, 4328, 28, "Country of Death - Literal", "DTHCOUNTRY", 1)]
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
    }
}
