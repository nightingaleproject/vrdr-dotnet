using System;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Globalization;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using ICSharpCode.SharpZipLib;

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
        /// <summary>Utility location to provide support for setting TRX-only fields that have no mapping in IJE when creating coding response records</summary>
        public TRXHelper trx;

        /// <summary>Utility location to provide support for setting MRE-only fields that have no mapping in IJE when creating coding response records</summary>
        public MREHelper mre;

        /// <summary>Field _void.</summary>
        private string _void;

        /// <summary>Field _alias.</summary>
        private string _alias;
        

        /// <summary>Helper class to contain properties for setting TRX-only fields that have no mapping in IJE when creating coding response records</summary>
        public class TRXHelper
        {
            private DeathRecord record;
            /// <summary>Constructor for class to contain properties for setting TRX-only fields that have no mapping in IJE when creating coding response records</summary>
            public TRXHelper(DeathRecord record)
            {
                this.record = record;
            }
            /// <summary>coder status - Property for setting the CodingStatus of a Cause of Death Coding Submission</summary>
            public string CS
            {
                get
                {
                    return record.CoderStatus.ToString();
                }
                set
                {
                    if (!String.IsNullOrWhiteSpace(value))
                    {
                        record.CoderStatus = Convert.ToInt32(value);
                    }
                }
            }
            /// <summary>shipment number - Property for setting the ShipmentNumber of a Cause of Death Coding Submission</summary>
            public string SHIP
            {
                get
                {
                    return record.ShipmentNumber;
                }
                set
                {
                    record.ShipmentNumber = value;
                }
            }
        }

        /// <summary>Helper class to contain properties for setting MRE-only fields that have no mapping in IJE when creating coding response records</summary>
        public class MREHelper
        {
            private DeathRecord record;
            /// <summary>Constructor for class to contain properties for setting MRE-only fields that have no mapping in IJE when creating coding response records</summary>
            public MREHelper(DeathRecord record)
            {
                this.record = record;
            }
            /// <summary>Property for setting the Race Recode 40 of a Demographic Coding Submission</summary>
            public string RECODE40
            {
                get
                {
                    return record.RaceRecode40Helper;
                }
                set
                {
                    record.RaceRecode40Helper = value;
                }
            }
        }

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
            this.trx = new TRXHelper(record);
            this.mre = new MREHelper(record);
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
        public IJEMortality(string ije, bool validate = true) : this()
        {
            if (ije == null)
            {
                throw new ArgumentException("IJE string cannot be null.");
            }
            if (ije.Length < 5000)
            {
                ije = ije.PadRight(5000, ' ');
            }
            // Loop over every property (these are the fields); Order by priority
            List<PropertyInfo> properties = typeof(IJEMortality).GetProperties().ToList().OrderBy(p => p.GetCustomAttribute<IJEField>().Priority).ToList();
            foreach (PropertyInfo property in properties)
            {
                // Grab the field attributes
                IJEField info = property.GetCustomAttribute<IJEField>();
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

        /// <summary>Constructor that creates an empty record for constructing records using the IJE properties.</summary>
        public IJEMortality()
        {
            this.record = new DeathRecord();
            this.trx = new TRXHelper(record);
            this.mre = new MREHelper(record);
        }

        /// <summary>Converts the internal <c>DeathRecord</c> into an IJE string.</summary>
        public override string ToString()
        {
            // Start with empty IJE Mortality record
            StringBuilder ije = new StringBuilder(new String(' ', 5000), 5000);

            // Loop over every property (these are the fields)
            foreach (PropertyInfo property in typeof(IJEMortality).GetProperties())
            {
                // Grab the field value
                string field = Convert.ToString(property.GetValue(this, null));
                // Grab the field attributes
                IJEField info = property.GetCustomAttribute<IJEField>();
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
            return typeof(IJEMortality).GetProperty(ijeFieldName).GetCustomAttribute<IJEField>();
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

        /// <summary>Get a value on the DeathRecord that is a numeric string with the option of being set to all 9s on the IJE side and -1 on the
        /// FHIR side to represent'unknown' and blank on the IJE side and null on the FHIR side to represent unspecified</summary>
        private string NumericAllowingUnknown_Get(string ijeFieldName, string fhirFieldName)
        {
            IJEField info = FieldInfo(ijeFieldName);
            int? value = (int?)typeof(DeathRecord).GetProperty(fhirFieldName).GetValue(this.record);
            if (value == null) return new String(' ', info.Length); // No value specified
            if (value == -1) return new String('9', info.Length); // Explicitly set to unknown
            string valueString = Convert.ToString(value);
            if (valueString.Length > info.Length)
            {
                validationErrors.Add($"Error: FHIR field {fhirFieldName} contains string '{valueString}' that's not the expected length for IJE field {ijeFieldName} of length {info.Length}");
            }
            return Truncate(valueString, info.Length).PadLeft(info.Length, '0');
        }

        /// <summary>Set a value on the DeathRecord that is a numeric string with the option of being set to all 9s on the IJE side and -1 on the
        /// FHIR side to represent'unknown' and blank on the IJE side and null on the FHIR side to represent unspecified</summary>
        private void NumericAllowingUnknown_Set(string ijeFieldName, string fhirFieldName, string value)
        {
            IJEField info = FieldInfo(ijeFieldName);
            if (value == new string(' ', info.Length))
            {
                typeof(DeathRecord).GetProperty(fhirFieldName).SetValue(this.record, null);
            }
            else if (value == new string('9', info.Length))
            {
                typeof(DeathRecord).GetProperty(fhirFieldName).SetValue(this.record, -1);
            }
            else
            {
                typeof(DeathRecord).GetProperty(fhirFieldName).SetValue(this.record, Convert.ToInt32(value));
            }
        }

        /// <summary>Get a value on the DeathRecord that is a time with the option of being set to all 9s on the IJE side and null on the FHIR side to represent null</summary>
        private string TimeAllowingUnknown_Get(string ijeFieldName, string fhirFieldName)
        {
            IJEField info = FieldInfo(ijeFieldName);
            string timeString = (string)typeof(DeathRecord).GetProperty(fhirFieldName).GetValue(this.record);
            if (timeString == null) return new String(' ', info.Length); // No value specified
            if (timeString == "-1") return new String('9', info.Length); // Explicitly set to unknown
            DateTimeOffset parsedTime;
            if (DateTimeOffset.TryParse(timeString, out parsedTime))
            {
                TimeSpan timeSpan = new TimeSpan(0, parsedTime.Hour, parsedTime.Minute, parsedTime.Second);
                return timeSpan.ToString(@"hhmm");
            }
            // No valid date found
            validationErrors.Add($"Error: FHIR field {fhirFieldName} contains value '{timeString}' that cannot be parsed into a time for IJE field {ijeFieldName}");
            return new String(' ', info.Length);
        }

        /// <summary>Set a value on the DeathRecord that is a time with the option of being set to all 9s on the IJE side and null on the FHIR side to represent null</summary>
        private void TimeAllowingUnknown_Set(string ijeFieldName, string fhirFieldName, string value)
        {
            IJEField info = FieldInfo(ijeFieldName);
            if (value == new string(' ', info.Length))
            {
                typeof(DeathRecord).GetProperty(fhirFieldName).SetValue(this.record, null);
            }
            else if (value == new string('9', info.Length))
            {
                typeof(DeathRecord).GetProperty(fhirFieldName).SetValue(this.record, "-1");
            }
            else
            {
                DateTimeOffset parsedTime;
                if (DateTimeOffset.TryParseExact(value, "HHmm", null, DateTimeStyles.None, out parsedTime))
                {
                    TimeSpan timeSpan = new TimeSpan(0, parsedTime.Hour, parsedTime.Minute, 0);
                    typeof(DeathRecord).GetProperty(fhirFieldName).SetValue(this.record, timeSpan.ToString(@"hh\:mm\:ss"));
                }
                else
                {
                    validationErrors.Add($"Error: FHIR field {fhirFieldName} value of '{value}' is invalid for IJE field {ijeFieldName}");
                }
            }
        }

        /// <summary>Get a value on the DeathRecord whose IJE type is a right justified, zero filled string.</summary>
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

        /// <summary>Set a value on the DeathRecord whose IJE type is a right justified, zero filled string.</summary>
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
                    validationErrors.Add($"Error: FHIR field {fhirFieldName} contains string '{current}' too long for IJE field {ijeFieldName} of length {info.Length}");
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
            if (dictionary == null)
            {
                dictionary = new Dictionary<string, string>();
            }
            if (!String.IsNullOrWhiteSpace(value))
            {
                dictionary[key] = value.Trim();
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
                if (geoType == "insideCityLimits")
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
                else if (geoType == "countyC" || geoType == "cityC")
                {
                    current = Truncate(current, info.Length).PadLeft(info.Length, '0');
                }
            }

            if (geoType == "zip")
            {  // Remove "-" for zip
                current.Replace("-", string.Empty);
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

        /// <summary>Set a value on the DeathRecord whose property is a geographic type (and is contained in a dictionary).</summary>
        private void Dictionary_Geo_Set(string ijeFieldName, string fhirFieldName, string keyPrefix, string geoType, bool isCoded, string value)
        {
            IJEField info = FieldInfo(ijeFieldName);
            Dictionary<string, string> dictionary = (Dictionary<string, string>)typeof(DeathRecord).GetProperty(fhirFieldName).GetValue(this.record);
            string key = keyPrefix + char.ToUpper(geoType[0]) + geoType.Substring(1);

            // if the value is null, and the dictionary does not exist, return
            if (dictionary == null && String.IsNullOrWhiteSpace(value))
            {
                return;
            }
            // initialize the dictionary if it does not exist
            if (dictionary == null)
            {
                dictionary = new Dictionary<string, string>();
            }

            if (!dictionary.ContainsKey(key) || String.IsNullOrWhiteSpace(dictionary[key]))
            {
                if (isCoded)
                {
                    if (geoType == "insideCityLimits")
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
                return (raceTuple.Item2).Trim();
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
        private string Get_MappingFHIRToIJE(Dictionary<string, string> mapping, string fhirField, string ijeField)
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
                switch (ijeField)
                {
                    case "COD":
                        ijeField = "County of Death";
                        break;
                    case "COD1A":
                        ijeField = "Cause of Death-1A";
                        break;
                    case "COD1B":
                        ijeField = "Cause of Death-1B";
                        break;
                    case "COD1C":
                        ijeField = "Cause of Death-1C";
                        break;
                    case "COD1D":
                        ijeField = "Cause of Death-1D";
                        break;
                    default:
                        break;
                }
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
        private void Set_MappingIJEToFHIR(Dictionary<string, string> mapping, string ijeField, string fhirField, string value)
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

        /// <summary>NCHS ICD10 to actual ICD10 </summary>
        private string NCHSICD10toActualICD10(string nchsicd10code)
        {
            string code = "";

            if (!String.IsNullOrEmpty(nchsicd10code))
            {
                if (ValidNCHSICD10(nchsicd10code.Trim()))
                {
                    code = nchsicd10code.Trim();
                }
                else
                {
                    throw new ArgumentException($"NCHS ICD10 code {nchsicd10code} is invalid.");
                }

            }

            if (code.Length >= 4)    // codes of length 4 or 5 need to have a decimal inserted
            {
                code = nchsicd10code.Insert(3, ".");
            }

            return (code);
        }
        /// <summary>Actual ICD10 to NCHS ICD10 </summary>
        private string ActualICD10toNCHSICD10(string icd10code)
        {
            if (!String.IsNullOrEmpty(icd10code))
            {
                return (icd10code.Replace(".", ""));
            }
            else
            {
                return "";
            }
        }

        /// <summary>Actual ICD10 to NCHS ICD10 </summary>
        public static bool ValidNCHSICD10(string nchsicd10code)
        {
            // ICD-10 diagnosis codes always begin with a letter followed by a digit.
            // The third character is usually a digit, but could be an A or B [1].
            // After the first three characters, there may be a decimal point, and up to three more alphanumeric characters.
            // Sometimes the decimal is left out.
            // NCHS ICD10 codes are the same as above for the first three characters.
            // The decimal point is always dropped.
            // Some codes have a fourth character that reflects an actual ICD10 code.
            // NCHS tacks on an extra character to some ICD10 codes, e.g., K7210 (K27.10)
            Regex NCHSICD10regex = new Regex(@"^[A-Z][0-9][0-9AB][0-9A-Z]{0,2}$");

            return (String.IsNullOrEmpty(nchsicd10code) ||
                 NCHSICD10regex.Match(nchsicd10code).Success);
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
                return NumericAllowingUnknown_Get("DOD_YR", "DeathYear");
            }
            set
            {
                NumericAllowingUnknown_Set("DOD_YR", "DeathYear", value);
            }
        }

        /// <summary>State, U.S. Territory or Canadian Province of Death - code</summary>
        [IJEField(2, 5, 2, "State, U.S. Territory or Canadian Province of Death - code", "DSTATE", 1)]
        public string DSTATE
        {
            get
            {
                string value = LeftJustified_Get("DSTATE", "DeathLocationJurisdiction");
                if (String.IsNullOrWhiteSpace(value))
                {
                    validationErrors.Add($"Error: FHIR field DeathLocationJurisdiction is blank, which is invalid for IJE field DSTATE.");
                }
                else if (dataLookup.JurisdictionNameToJurisdictionCode(value) == null)
                {
                    validationErrors.Add($"Error: FHIR field DeathLocationJurisdiction has value '{value}', which is invalid for IJE field DSTATE.");
                }
                return value;
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    LeftJustified_Set("DSTATE", "DeathLocationJurisdiction", value);
                    // We used to state the DeathLocationAddress here as well, but that's now handled in DeathRecord
                    // Dictionary_Set("STATEC", "DeathLocationAddress", "addressState", value);
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
				if(_void == null)
					_void = "0";
                return _void;
            }
            set
            {
				if(value.Trim() == "1")
				{
					_void = "1";
				}
				else
					_void = "0";
            }
        }

        /// <summary>Auxiliary State file number</summary>
        [IJEField(5, 14, 12, "Auxiliary State file number", "AUXNO", 1)]
        public string AUXNO
        {
            get
            {
                if (record.StateLocalIdentifier1 == null)
                {
                    return (new String(' ', 12));
                }
                return LeftJustified_Get("AUXNO", "StateLocalIdentifier1");
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    value = value.PadLeft(12 , '0');
                    LeftJustified_Set("AUXNO", "StateLocalIdentifier1", value);
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
                    return Truncate(names[0], 50).PadRight(50, ' ');
                }
                return new string(' ', 50);
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
        [IJEField(8, 77, 1, "Decedent's Legal Name--Middle", "MNAME", 2)]
        public string MNAME
        {
            get
            {
                string[] names = record.GivenNames;
                if (names.Length > 1)
                {
                    return Truncate(names[1], 1).PadRight(1, ' ');
                }
                return " ";
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    if (String.IsNullOrWhiteSpace(GNAME)) throw new ArgumentException("Middle name cannot be set before first name");
                    if (String.IsNullOrWhiteSpace(DMIDDLE))
                    {
                        if (record.GivenNames != null)
                        {
                            List<string> names = record.GivenNames.ToList();
                            if (names.Count() > 1) names[1] = value.Trim(); else names.Add(value.Trim());
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
                if (!String.IsNullOrWhiteSpace(record.FamilyName))
                {
                    return LeftJustified_Get("LNAME", "FamilyName");
                }
                else
                {
                    return "UNKNOWN";
                }
            }
            set
            {
                if (value.Equals("UNKNOWN"))
                {
                    Set_MappingIJEToFHIR(Mappings.AdministrativeGender.IJEToFHIR, "LNAME", "FamilyName", null);
                }
                else
		{
                    LeftJustified_Set("LNAME", "FamilyName", value);
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
                LeftJustified_Set("SUFF", "Suffix", value);
            }
        }

        /// <summary>Decedent's Legal Name--Alias</summary>
        [IJEField(11, 138, 1, "Decedent's Legal Name--Alias", "ALIAS", 1)]
        public string ALIAS
        {
            get
            {
                return _alias;
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    string valueTrim = value.Trim();
                    if(valueTrim == "0" || valueTrim == "1")
                    {
                        _alias = valueTrim;
                    }
                }
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
                string fhirFieldName = "SSN";
                string ijeFieldName = "SSN";
                int ssnLength = 9;
                string ssn = record.SSN;
                if (!String.IsNullOrWhiteSpace(ssn))
                {
                    string formattedSSN = ssn.Replace("-", string.Empty).Replace(" ", string.Empty);
                    if (formattedSSN.Length != ssnLength)
                    {
                        validationErrors.Add($"Error: FHIR field {fhirFieldName} contains string '{ssn}' which is not the expected length (without dashes or spaces) for IJE field {ijeFieldName} of length {ssnLength}");
                    }
                    return Truncate(formattedSSN, ssnLength).PadRight(ssnLength, ' ');
                }
                else
                {
                    return new String(' ', ssnLength);
                }
            }
            set
            {
                string fhirFieldName = "SSN";
                string ijeFieldName = "SSN";
                int ssnLength = 9;
                if (!String.IsNullOrWhiteSpace(value))
                {
                    string ssn = value.Trim();
                    if (ssn.Contains("-") || ssn.Contains(" "))
                    {
                        validationErrors.Add($"Error: IJE field {ijeFieldName} contains string '{value}' which cannot contain ` ` or `-` characters for FHIR field {fhirFieldName}.");
                    }
                    string formattedSSN = ssn.Replace("-", string.Empty).Replace(" ", string.Empty);
                    if (formattedSSN.Length != ssnLength)
                    {
                        validationErrors.Add($"Error: IJE field {ijeFieldName} contains string '{value}' which is not the expected length (without dashes or spaces) for FHIR field {fhirFieldName} of length {ssnLength}");
                    }
                }
                LeftJustified_Set(ijeFieldName, fhirFieldName, value);
            }
        }

        /// <summary>Decedent's Age--Type</summary>
        [IJEField(16, 200, 1, "Decedent's Age--Type", "AGETYPE", 1)]
        public string AGETYPE
        {
            get
            {
                // Pull code from coded unit.   "code" field is not required by VRDR IG
                string code = Dictionary_Get_Full("AGETYPE", "AgeAtDeath", "code") ?? "";
                Mappings.UnitsOfAge.FHIRToIJE.TryGetValue(code, out string ijeValue);
                return ijeValue ?? "9";
            }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    return;  // nothing to do
                }
                // If we have an IJE value map it to FHIR and set the unit, code and system appropriately, otherwise set to unknown
                if (!Mappings.UnitsOfAge.IJEToFHIR.TryGetValue(value, out string fhirValue))
                {
                    // We have an invalid code, map it to unknown
                    fhirValue = ValueSets.UnitsOfAge.Unknown;
                }
                // We have the code, now we need the corresponding unit and system
                // Iterate over the allowed options and see if the code supplies is one of them
                int length = ValueSets.UnitsOfAge.Codes.Length;
                for (int i = 0; i < length; i += 1)
                {
                    if (ValueSets.UnitsOfAge.Codes[i, 0] == fhirValue)
                    {
                        // Found it, so call the supplied setter with the appropriate dictionary built based on the code
                        // using the supplied options and return
                        Dictionary<string, string> dict = new Dictionary<string, string>();
                        dict.Add("code", fhirValue);
                        dict.Add("unit", ValueSets.UnitsOfAge.Codes[i, 1]);
                        dict.Add("system", ValueSets.UnitsOfAge.Codes[i, 2]);
                        typeof(DeathRecord).GetProperty("AgeAtDeath").SetValue(this.record, dict);
                        return;
                    }
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
                {
                    return "999";
                }
            }
            set
            {
                Dictionary_Set("AGE", "AgeAtDeath", "value", value.TrimStart('0'));
            }
        }

        /// <summary>Decedent's Age--Edit Flag</summary>
        [IJEField(18, 204, 1, "Decedent's Age--Edit Flag", "AGE_BYPASS", 1)]
        public string AGE_BYPASS
        {
            get
            {
                return Get_MappingFHIRToIJE(Mappings.EditBypass01.FHIRToIJE, "AgeAtDeathEditFlag", "AGE_BYPASS");
            }
            set
            {
                Set_MappingIJEToFHIR(Mappings.EditBypass01.IJEToFHIR, "AGE_BYPASS", "AgeAtDeathEditFlag", value);
            }
        }

        /// <summary>Date of Birth--Year</summary>
        [IJEField(19, 205, 4, "Date of Birth--Year", "DOB_YR", 1)]
        public string DOB_YR
        {
            get
            {
                return NumericAllowingUnknown_Get("DOB_YR", "BirthYear");
            }
            set
            {
                NumericAllowingUnknown_Set("DOB_YR", "BirthYear", value);
            }
        }

        /// <summary>Date of Birth--Month</summary>
        [IJEField(20, 209, 2, "Date of Birth--Month", "DOB_MO", 1)]
        public string DOB_MO
        {
            get
            {
                return NumericAllowingUnknown_Get("DOB_MO", "BirthMonth");
            }
            set
            {
                NumericAllowingUnknown_Set("DOB_MO", "BirthMonth", value);
            }
        }

        /// <summary>Date of Birth--Day</summary>
        [IJEField(21, 211, 2, "Date of Birth--Day", "DOB_DY", 1)]
        public string DOB_DY
        {
            get
            {
                return NumericAllowingUnknown_Get("DOB_DY", "BirthDay");
            }
            set
            {
                NumericAllowingUnknown_Set("DOB_DY", "BirthDay", value);
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
                if (!String.IsNullOrWhiteSpace(value)) // need to filter out countries that are excluded as residences because they are defunct, e.g., "UR"
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
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Set_MappingIJEToFHIR(Mappings.YesNoUnknown.IJEToFHIR, "LIMITS", "ResidenceWithinCityLimits", value);
                }
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
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Set_MappingIJEToFHIR(Mappings.MaritalStatus.IJEToFHIR, "MARITAL", "MaritalStatus", value);
                }
            }
        }

        /// <summary>Marital Status--Edit Flag</summary>
        [IJEField(30, 231, 1, "Marital Status--Edit Flag", "MARITAL_BYPASS", 1)]
        public string MARITAL_BYPASS
        {
            get
            {
                return Get_MappingFHIRToIJE(Mappings.EditBypass0124.FHIRToIJE, "MaritalStatusEditFlag", "MARITAL_BYPASS");
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Set_MappingIJEToFHIR(Mappings.EditBypass0124.IJEToFHIR, "MARITAL_BYPASS", "MaritalStatusEditFlag", value);
                }
            }
        }

        /// <summary>Place of Death</summary>
        [IJEField(31, 232, 1, "Place of Death", "DPLACE", 1)]
        public string DPLACE
        {
            get
            {
                return Get_MappingFHIRToIJE(Mappings.PlaceOfDeath.FHIRToIJE, "DeathLocationType", "DPLACE");
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Set_MappingIJEToFHIR(Mappings.PlaceOfDeath.IJEToFHIR, "DPLACE", "DeathLocationType", value);
                }
            }
        }

        /// <summary>County of Death Occurrence</summary>
        [IJEField(32, 233, 3, "County of Death Occurrence", "COD", 2)]
        public string COD
        {
            get
            {
                return Dictionary_Geo_Get("COD", "DeathLocationAddress", "address", "countyC", true);
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Dictionary_Geo_Set("COD", "DeathLocationAddress", "address", "countyC", true, value);
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
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Set_MappingIJEToFHIR(Mappings.MethodOfDisposition.IJEToFHIR, "DISP", "DecedentDispositionMethod", value);
                }
            }
        }

        /// <summary>Date of Death--Month</summary>
        [IJEField(34, 237, 2, "Date of Death--Month", "DOD_MO", 1)]
        public string DOD_MO
        {
            get
            {
                return NumericAllowingUnknown_Get("DOD_MO", "DeathMonth");
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    NumericAllowingUnknown_Set("DOD_MO", "DeathMonth", value);
                }
            }
        }

        /// <summary>Date of Death--Day</summary>
        [IJEField(35, 239, 2, "Date of Death--Day", "DOD_DY", 1)]
        public string DOD_DY
        {
            get
            {
                return NumericAllowingUnknown_Get("DOD_DY", "DeathDay");
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    NumericAllowingUnknown_Set("DOD_DY", "DeathDay", value);
                }
            }
        }

        /// <summary>Time of Death</summary>
        [IJEField(36, 241, 4, "Time of Death", "TOD", 1)]
        public string TOD
        {
            get
            {
                return TimeAllowingUnknown_Get("TOD", "DeathTime");
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                   TimeAllowingUnknown_Set("TOD", "DeathTime", value);
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
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Set_MappingIJEToFHIR(Mappings.EducationLevel.IJEToFHIR, "DEDUC", "EducationLevel", value);
                }
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
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Set_MappingIJEToFHIR(Mappings.EditBypass01234.IJEToFHIR, "DEDUC_BYPASS", "EducationLevelEditFlag", value);
                }
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
                if (!String.IsNullOrWhiteSpace(value))
                {
                    if (value == "H")
                    {
                        value = "Y";
                    }
                    Set_MappingIJEToFHIR(Mappings.YesNoUnknown.IJEToFHIR, "DETHNIC1", "Ethnicity1", value);
                }
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
                if (!String.IsNullOrWhiteSpace(value))
                {
                    if (value == "H")
                    {
                        value = "Y";
                    }
                    Set_MappingIJEToFHIR(Mappings.YesNoUnknown.IJEToFHIR, "DETHNIC2", "Ethnicity2", value);
                }
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
                if (!String.IsNullOrWhiteSpace(value))
                {
                    if (value == "H")
                    {
                        value = "Y";
                    }
                    Set_MappingIJEToFHIR(Mappings.YesNoUnknown.IJEToFHIR, "DETHNIC3", "Ethnicity3", value);
                }
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
                if (!String.IsNullOrWhiteSpace(value))
                {
                    if (value == "H")
                    {
                        value = "Y";
                    }
                    Set_MappingIJEToFHIR(Mappings.YesNoUnknown.IJEToFHIR, "DETHNIC4", "Ethnicity4", value);
                }
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
                return Get_Race(NvssRace.AmericanIndianOrAlaskanNative);
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Set_Race(NvssRace.AmericanIndianOrAlaskanNative, value);
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
                return Get_Race(NvssRace.FirstAmericanIndianOrAlaskanNativeLiteral);
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Set_Race(NvssRace.FirstAmericanIndianOrAlaskanNativeLiteral, value);
                }
            }
        }

        /// <summary>Decedent's Race--Second American Indian or Alaska Native Literal</summary>
        [IJEField(60, 316, 30, "Decedent's Race--Second American Indian or Alaska Native Literal", "RACE17", 1)]
        public string RACE17
        {
            get
            {
                return Get_Race(NvssRace.SecondAmericanIndianOrAlaskanNativeLiteral);
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Set_Race(NvssRace.SecondAmericanIndianOrAlaskanNativeLiteral, value);
                }
            }
        }

        /// <summary>Decedent's Race--First Other Asian Literal</summary>
        [IJEField(61, 346, 30, "Decedent's Race--First Other Asian Literal", "RACE18", 1)]
        public string RACE18
        {
            get
            {
                return Get_Race(NvssRace.FirstOtherAsianLiteral);
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Set_Race(NvssRace.FirstOtherAsianLiteral, value);
                }
            }
        }

        /// <summary>Decedent's Race--Second Other Asian Literal</summary>
        [IJEField(62, 376, 30, "Decedent's Race--Second Other Asian Literal", "RACE19", 1)]
        public string RACE19
        {
            get
            {
                return Get_Race(NvssRace.SecondOtherAsianLiteral);
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Set_Race(NvssRace.SecondOtherAsianLiteral, value);
                }
            }
        }

        /// <summary>Decedent's Race--First Other Pacific Islander Literal</summary>
        [IJEField(63, 406, 30, "Decedent's Race--First Other Pacific Islander Literal", "RACE20", 1)]
        public string RACE20
        {
            get
            {
                return Get_Race(NvssRace.FirstOtherPacificIslanderLiteral);
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Set_Race(NvssRace.FirstOtherPacificIslanderLiteral, value);
                }
            }
        }

        /// <summary>Decedent's Race--Second Other Pacific Islander Literalr</summary>
        [IJEField(64, 436, 30, "Decedent's Race--Second Other Pacific Islander Literal", "RACE21", 1)]
        public string RACE21
        {
            get
            {
                return Get_Race(NvssRace.SecondOtherPacificIslanderLiteral);
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Set_Race(NvssRace.SecondOtherPacificIslanderLiteral, value);
                }
            }
        }

        /// <summary>Decedent's Race--First Other Literal</summary>
        [IJEField(65, 466, 30, "Decedent's Race--First Other Literal", "RACE22", 1)]
        public string RACE22
        {
            get
            {
                return Get_Race(NvssRace.FirstOtherRaceLiteral);
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Set_Race(NvssRace.FirstOtherRaceLiteral, value);
                }
            }
        }

        /// <summary>Decedent's Race--Second Other Literal</summary>
        [IJEField(66, 496, 30, "Decedent's Race--Second Other Literal", "RACE23", 1)]
        public string RACE23
        {
            get
            {
                return Get_Race(NvssRace.SecondOtherRaceLiteral);
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Set_Race(NvssRace.SecondOtherRaceLiteral, value);
                }
            }
        }

        /// <summary>First Edited Code</summary>
        [IJEField(67, 526, 3, "First Edited Code", "RACE1E", 1)]
        public string RACE1E
        {
            get
            {
                return Get_MappingFHIRToIJE(Mappings.RaceCode.FHIRToIJE, "FirstEditedRaceCode", "RACE1E");
            }
            set
            {
                Set_MappingIJEToFHIR(Mappings.RaceCode.IJEToFHIR, "RACE1E", "FirstEditedRaceCode", value);
            }
        }

        /// <summary>Second Edited Code</summary>
        [IJEField(68, 529, 3, "Second Edited Code", "RACE2E", 1)]
        public string RACE2E
        {
            get
            {
                return Get_MappingFHIRToIJE(Mappings.RaceCode.FHIRToIJE, "SecondEditedRaceCode", "RACE2E");
            }
            set
            {
                Set_MappingIJEToFHIR(Mappings.RaceCode.IJEToFHIR, "RACE2E", "SecondEditedRaceCode", value);
            }
        }

        /// <summary>Third Edited Code</summary>
        [IJEField(69, 532, 3, "Third Edited Code", "RACE3E", 1)]
        public string RACE3E
        {
            get
            {
                return Get_MappingFHIRToIJE(Mappings.RaceCode.FHIRToIJE, "ThirdEditedRaceCode", "RACE3E");
            }
            set
            {
                Set_MappingIJEToFHIR(Mappings.RaceCode.IJEToFHIR, "RACE3E", "ThirdEditedRaceCode", value);
            }
        }

        /// <summary>Fourth Edited Code</summary>
        [IJEField(70, 535, 3, "Fourth Edited Code", "RACE4E", 1)]
        public string RACE4E
        {
            get
            {
                return Get_MappingFHIRToIJE(Mappings.RaceCode.FHIRToIJE, "FourthEditedRaceCode", "RACE4E");
            }
            set
            {
                Set_MappingIJEToFHIR(Mappings.RaceCode.IJEToFHIR, "RACE4E", "FourthEditedRaceCode", value);
            }
        }

        /// <summary>Fifth Edited Code</summary>
        [IJEField(71, 538, 3, "Fifth Edited Code", "RACE5E", 1)]
        public string RACE5E
        {
            get
            {
                return Get_MappingFHIRToIJE(Mappings.RaceCode.FHIRToIJE, "FifthEditedRaceCode", "RACE5E");
            }
            set
            {
                Set_MappingIJEToFHIR(Mappings.RaceCode.IJEToFHIR, "RACE5E", "FifthEditedRaceCode", value);
            }
        }

        /// <summary>Sixth Edited Code</summary>
        [IJEField(72, 541, 3, "Sixth Edited Code", "RACE6E", 1)]
        public string RACE6E
        {
            get
            {
                return Get_MappingFHIRToIJE(Mappings.RaceCode.FHIRToIJE, "SixthEditedRaceCode", "RACE6E");
            }
            set
            {
                Set_MappingIJEToFHIR(Mappings.RaceCode.IJEToFHIR, "RACE6E", "SixthEditedRaceCode", value);
            }
        }

        /// <summary>Seventh Edited Code</summary>
        [IJEField(73, 544, 3, "Seventh Edited Code", "RACE7E", 1)]
        public string RACE7E
        {
            get
            {
                return Get_MappingFHIRToIJE(Mappings.RaceCode.FHIRToIJE, "SeventhEditedRaceCode", "RACE7E");
            }
            set
            {
                Set_MappingIJEToFHIR(Mappings.RaceCode.IJEToFHIR, "RACE7E", "SeventhEditedRaceCode", value);
            }
        }

        /// <summary>Eighth Edited Code</summary>
        [IJEField(74, 547, 3, "Eighth Edited Code", "RACE8E", 1)]
        public string RACE8E
        {
            get
            {
                return Get_MappingFHIRToIJE(Mappings.RaceCode.FHIRToIJE, "EighthEditedRaceCode", "RACE8E");
            }
            set
            {
                Set_MappingIJEToFHIR(Mappings.RaceCode.IJEToFHIR, "RACE8E", "EighthEditedRaceCode", value);
            }
        }

        /// <summary>First American Indian Code</summary>
        [IJEField(75, 550, 3, "First American Indian Code", "RACE16C", 1)]
        public string RACE16C
        {
            get
            {
                return Get_MappingFHIRToIJE(Mappings.RaceCode.FHIRToIJE, "FirstAmericanIndianRaceCode", "RACE16C");
            }
            set
            {
                Set_MappingIJEToFHIR(Mappings.RaceCode.IJEToFHIR, "RACE16C", "FirstAmericanIndianRaceCode", value);
            }
        }

        /// <summary>Second American Indian Code</summary>
        [IJEField(76, 553, 3, "Second American Indian Code", "RACE17C", 1)]
        public string RACE17C
        {
            get
            {
                return Get_MappingFHIRToIJE(Mappings.RaceCode.FHIRToIJE, "SecondAmericanIndianRaceCode", "RACE17C");
            }
            set
            {
                Set_MappingIJEToFHIR(Mappings.RaceCode.IJEToFHIR, "RACE17C", "SecondAmericanIndianRaceCode", value);
            }
        }

        /// <summary>First Other Asian Code</summary>
        [IJEField(77, 556, 3, "First Other Asian Code", "RACE18C", 1)]
        public string RACE18C
        {
            get
            {
                return Get_MappingFHIRToIJE(Mappings.RaceCode.FHIRToIJE, "FirstOtherAsianRaceCode", "RACE18C");
            }
            set
            {
                Set_MappingIJEToFHIR(Mappings.RaceCode.IJEToFHIR, "RACE18C", "FirstOtherAsianRaceCode", value);
            }
        }

        /// <summary>Second Other Asian Code</summary>
        [IJEField(78, 559, 3, "Second Other Asian Code", "RACE19C", 1)]
        public string RACE19C
        {
            get
            {
                return Get_MappingFHIRToIJE(Mappings.RaceCode.FHIRToIJE, "SecondOtherAsianRaceCode", "RACE19C");
            }
            set
            {
                Set_MappingIJEToFHIR(Mappings.RaceCode.IJEToFHIR, "RACE19C", "SecondOtherAsianRaceCode", value);
            }
        }

        /// <summary>First Other Pacific Islander Code</summary>
        [IJEField(79, 562, 3, "First Other Pacific Islander Code", "RACE20C", 1)]
        public string RACE20C
        {
            get
            {
                return Get_MappingFHIRToIJE(Mappings.RaceCode.FHIRToIJE, "FirstOtherPacificIslanderRaceCode", "RACE20C");
            }
            set
            {
                Set_MappingIJEToFHIR(Mappings.RaceCode.IJEToFHIR, "RACE20C", "FirstOtherPacificIslanderRaceCode", value);
            }
        }

        /// <summary>Second Other Pacific Islander Code</summary>
        [IJEField(80, 565, 3, "Second Other Pacific Islander Code", "RACE21C", 1)]
        public string RACE21C
        {
            get
            {
                return Get_MappingFHIRToIJE(Mappings.RaceCode.FHIRToIJE, "SecondOtherPacificIslanderRaceCode", "RACE21C");
            }
            set
            {
                Set_MappingIJEToFHIR(Mappings.RaceCode.IJEToFHIR, "RACE21C", "SecondOtherPacificIslanderRaceCode", value);
            }
        }

        /// <summary>First Other Race Code</summary>
        [IJEField(81, 568, 3, "First Other Race Code", "RACE22C", 1)]
        public string RACE22C
        {
            get
            {
                return Get_MappingFHIRToIJE(Mappings.RaceCode.FHIRToIJE, "FirstOtherRaceCode", "RACE22C");
            }
            set
            {
                Set_MappingIJEToFHIR(Mappings.RaceCode.IJEToFHIR, "RACE22C", "FirstOtherRaceCode", value);
            }
        }

        /// <summary>Second Other Race Code</summary>
        [IJEField(82, 571, 3, "Second Other Race Code", "RACE23C", 1)]
        public string RACE23C
        {
            get
            {
                return Get_MappingFHIRToIJE(Mappings.RaceCode.FHIRToIJE, "SecondOtherRaceCode", "RACE23C");
            }
            set
            {
                Set_MappingIJEToFHIR(Mappings.RaceCode.IJEToFHIR, "RACE23C", "SecondOtherRaceCode", value);
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
                string bcno = record.BirthRecordId;
                if (bcno != null)
                {
                    return bcno;
                }
                return "";
            }
            set
            {
                // if value is null, the library will add the data absent reason

                record.BirthRecordId = value;
            }
        }

        /// <summary>Infant Death/Birth Linking - year of birth</summary>
        [IJEField(89, 667, 4, "Infant Death/Birth Linking - year of birth", "IDOB_YR", 1)]
        public string IDOB_YR
        {
            get
            {
                return LeftJustified_Get("IDOB_YR", "BirthRecordYear");
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    LeftJustified_Set("IDOB_YR", "BirthRecordYear", value);
                }
            }
        }

        /// <summary>Infant Death/Birth Linking - Birth state</summary>
        [IJEField(90, 671, 2, "Infant Death/Birth Linking - State, U.S. Territory or Canadian Province of Birth - code", "BSTATE", 1)]
        public string BSTATE
        {
            get
            {
                return LeftJustified_Get("BSTATE", "BirthRecordState");
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    LeftJustified_Set("BSTATE", "BirthRecordState", value);
                }
            }
        }

        /// <summary>Receipt date -- Year</summary>
        [IJEField(91, 673, 4, "Receipt date -- Year", "R_YR", 1)]
        public string R_YR
        {
            get
            {
                return NumericAllowingUnknown_Get("R_YR", "ReceiptYear");
            }
            set
            {
                NumericAllowingUnknown_Set("R_YR", "ReceiptYear", value);
            }
        }

        /// <summary>Receipt date -- Month</summary>
        [IJEField(92, 677, 2, "Receipt date -- Month", "R_MO", 1)]
        public string R_MO
        {
            get
            {
                return NumericAllowingUnknown_Get("R_MO", "ReceiptMonth");
            }
            set
            {
                NumericAllowingUnknown_Set("R_MO", "ReceiptMonth", value);
            }
        }

        /// <summary>Receipt date -- Day</summary>
        [IJEField(93, 679, 2, "Receipt date -- Day", "R_DY", 1)]
        public string R_DY
        {
            get
            {
                return NumericAllowingUnknown_Get("R_DY", "ReceiptDay");
            }
            set
            {
                NumericAllowingUnknown_Set("R_DY", "ReceiptDay", value);
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
                return Get_MappingFHIRToIJE(Mappings.IntentionalReject.FHIRToIJE, "IntentionalReject", "INT_REJ");
            }
            set
            {
                Set_MappingIJEToFHIR(Mappings.IntentionalReject.IJEToFHIR, "INT_REJ", "IntentionalReject", value);
            }
        }

        /// <summary>Acme System Reject Codes</summary>
        [IJEField(102, 703, 1, "Acme System Reject Codes", "SYS_REJ", 1)]
        public string SYS_REJ
        {
            get
            {
                return Get_MappingFHIRToIJE(Mappings.SystemReject.FHIRToIJE, "AcmeSystemReject", "SYS_REJ");
            }
            set
            {
                Set_MappingIJEToFHIR(Mappings.SystemReject.IJEToFHIR, "SYS_REJ", "AcmeSystemReject", value);
            }
        }

        /// <summary>Place of Injury (computer generated)</summary>
        [IJEField(103, 704, 1, "Place of Injury (computer generated)", "INJPL", 1)]
        public string INJPL
        {
            get
            {
                return Get_MappingFHIRToIJE(Mappings.PlaceOfInjury.FHIRToIJE, "PlaceOfInjury", "INJPL");
            }
            set
            {
                Set_MappingIJEToFHIR(Mappings.PlaceOfInjury.IJEToFHIR, "INJPL", "PlaceOfInjury", value);
            }
        }

        /// <summary>Manual Underlying Cause</summary>
        [IJEField(104, 705, 5, "Manual Underlying Cause", "MAN_UC", 1)]
        public string MAN_UC
        {
            get
            {
                return (ActualICD10toNCHSICD10(LeftJustified_Get("MAN_UC", "ManUnderlyingCOD")));
            }
            set
            {
                LeftJustified_Set("MAN_UC", "ManUnderlyingCOD", NCHSICD10toActualICD10(value));
            }
        }

        /// <summary>ACME Underlying Cause</summary>
        [IJEField(105, 710, 5, "ACME Underlying Cause", "ACME_UC", 1)]
        public string ACME_UC
        {
            get
            {
                return (ActualICD10toNCHSICD10(LeftJustified_Get("ACME_UC", "AutomatedUnderlyingCOD")));
            }
            set
            {
                LeftJustified_Set("ACME_UC", "AutomatedUnderlyingCOD", NCHSICD10toActualICD10(value));
            }
        }

        /// <summary>Entity-axis codes</summary>
        // 20 codes, each taking up 8 characters:
        // 1 char:   part/line number (1-6)
        // 1 char: sequence within the line. (1-8)
        // 4 char “ICD code” in NCHS format, without the ‘.’
        // 1 char reserved”.  (not represented in the FHIR specification)
        // 1 char ‘e code indicator’
        [IJEField(106, 715, 160, "Entity-axis codes", "EAC", 1)]
        public string EAC
        {
            get
            {
                string eacStr = "";
                foreach ((int LineNumber, int Position, string Code, bool ECode) entry in record.EntityAxisCauseOfDeath)
                {
                    string lineNumber = Truncate(entry.LineNumber.ToString(), 1).PadRight(1, ' ');
                    string position = Truncate(entry.Position.ToString(), 1).PadRight(1, ' ');
                    string icdCode = Truncate(ActualICD10toNCHSICD10(entry.Code), 5).PadRight(5, ' '); ;
                    string eCode = entry.ECode ? "&" : " ";
                    eacStr += lineNumber + position + icdCode + eCode;
                }
                string fmtEac = Truncate(eacStr, 160).PadRight(160, ' ');
                return fmtEac;
            }
            set
            {
                List<(int LineNumber, int Position, string Code, bool ECode)> eac = new List<(int LineNumber, int Position, string Code, bool ECode)>();
                string paddedValue = value.PadRight(160); // Accept input that's missing white space padding to the right
                IEnumerable<string> codes = Enumerable.Range(0, paddedValue.Length / 8).Select(i => paddedValue.Substring(i * 8, 8));
                foreach (string code in codes)
                {
                    if (!String.IsNullOrWhiteSpace(code))
                    {
                        if (int.TryParse(code.Substring(0, 1), out int lineNumber) && int.TryParse(code.Substring(1, 1), out int position))
                        {
                            string icdCode = NCHSICD10toActualICD10(code.Substring(2, 5));
                            string eCode = code.Substring(7, 1);
                            eac.Add((LineNumber: lineNumber, Position: position, Code: icdCode, ECode: eCode == "&"));
                        }
                    }
                }
                if (eac.Count > 0)
                {
                    record.EntityAxisCauseOfDeath = eac;
                }
            }
        }

        /// <summary>Transax conversion flag: Computer Generated</summary>
        [IJEField(107, 875, 1, "Transax conversion flag: Computer Generated", "TRX_FLG", 1)]
        public string TRX_FLG
        {
            get
            {
                return Get_MappingFHIRToIJE(Mappings.TransaxConversion.FHIRToIJE, "TransaxConversion", "TRX_FLG");
            }
            set
            {
                Set_MappingIJEToFHIR(Mappings.TransaxConversion.IJEToFHIR, "TRXFLG", "TransaxConversion", value);
            }
        }

        /// <summary>Record-axis codes</summary>
        // 20 codes, each taking up 5 characters:
        // 4 char “ICD code” in NCHS format, without the ‘.’
        // 1 char WouldBeUnderlyingCauseOfDeathWithoutPregnancy, only significant if position=2”
        [IJEField(108, 876, 100, "Record-axis codes", "RAC", 1)]
        public string RAC
        {
            get
            {
                string racStr = "";
                foreach ((int Position, string Code, bool Pregnancy) entry in record.RecordAxisCauseOfDeath)
                {
                    // Position doesn't appear in the IJE/TRX format it's just implicit
                    string icdCode = Truncate(ActualICD10toNCHSICD10(entry.Code), 4).PadRight(4, ' ');
                    string preg = entry.Pregnancy ? "1" : " ";
                    racStr += icdCode + preg;
                }
                string fmtRac = Truncate(racStr, 100).PadRight(100, ' ');
                return fmtRac;
            }
            set
            {
                List<(int Position, string Code, bool Pregnancy)> rac = new List<(int Position, string Code, bool Pregnancy)>();
                string paddedValue = value.PadRight(100); // Accept input that's missing white space padding to the right
                IEnumerable<string> codes = Enumerable.Range(0, paddedValue.Length / 5).Select(i => paddedValue.Substring(i * 5, 5));
                int position = 1;
                foreach (string code in codes)
                {
                    if (!String.IsNullOrWhiteSpace(code))
                    {
                        string icdCode = NCHSICD10toActualICD10(code.Substring(0, 4));
                        string preg = code.Substring(4, 1);
                        Tuple<string, string, string> entry = Tuple.Create(Convert.ToString(position), icdCode, preg);
                        rac.Add((Position: position, Code: icdCode, Pregnancy: preg == "1"));
                    }
                    position++;
                }
                if (rac.Count > 0)
                {
                    record.RecordAxisCauseOfDeath = rac;
                }
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
                return NumericAllowingUnknown_Get("DOI_MO", "InjuryMonth");
            }
            set
            {
                NumericAllowingUnknown_Set("DOI_MO", "InjuryMonth", value);
            }
        }

        /// <summary>Date of injury--day</summary>
        [IJEField(115, 983, 2, "Date of injury--day", "DOI_DY", 1)]
        public string DOI_DY
        {
            get
            {
                return NumericAllowingUnknown_Get("DOI_DY", "InjuryDay");
            }
            set
            {
                NumericAllowingUnknown_Set("DOI_DY", "InjuryDay", value);
            }
        }

        /// <summary>Date of injury--year</summary>
        [IJEField(116, 985, 4, "Date of injury--year", "DOI_YR", 1)]
        public string DOI_YR
        {
            get
            {
                return NumericAllowingUnknown_Get("DOI_YR", "InjuryYear");
            }
            set
            {
                NumericAllowingUnknown_Set("DOI_YR", "InjuryYear", value);
            }
        }

        /// <summary>Time of injury</summary>
        [IJEField(117, 989, 4, "Time of injury", "TOI_HR", 1)]
        public string TOI_HR
        {
            get
            {
                return TimeAllowingUnknown_Get("TOI_HR", "InjuryTime");
            }
            set
            {
                TimeAllowingUnknown_Set("TOI_HR", "InjuryTime", value);
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
                var ret = record.CertificationRoleHelper;
                if (ret != null && Mappings.CertifierTypes.FHIRToIJE.ContainsKey(ret))
                {
                    return Get_MappingFHIRToIJE(Mappings.CertifierTypes.FHIRToIJE, "CertificationRole", "CERTL");
                }
                else  // If the return value is not a code, it is just an arbitrary string, so return it.
                {
                    return ret;
                }
            }
            set
            {
                if (Mappings.CertifierTypes.IJEToFHIR.ContainsKey(value.Split(' ')[0]))
                {
                    Set_MappingIJEToFHIR(Mappings.CertifierTypes.IJEToFHIR, "CERTL", "CertificationRole", value.Trim());
                }
                else  // If the value is not a valid code, it is just an arbitrary string.  The helper will deal with it.
                {
                    record.CertificationRoleHelper = value;
                }
            }

        }

        /// <summary>Activity at time of death (computer generated)</summary>
        [IJEField(120, 1024, 1, "Activity at time of death (computer generated)", "INACT", 1)]
        public string INACT
        {
            get
            {
                return Get_MappingFHIRToIJE(Mappings.ActivityAtTimeOfDeath.FHIRToIJE, "ActivityAtDeath", "INACT");
            }
            set
            {
                Set_MappingIJEToFHIR(Mappings.ActivityAtTimeOfDeath.IJEToFHIR, "INACT", "ActivityAtDeath", value);
            }
        }

        /// <summary>Auxiliary State file number</summary>
        [IJEField(121, 1025, 12, "Auxiliary State file number", "AUXNO2", 1)]
        public string AUXNO2
        {
            get
            {
                if (record.StateLocalIdentifier2 == null)
                {
                    return (new String(' ', 12));
                }
                return LeftJustified_Get("AUXNO2", "StateLocalIdentifier2");
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    value = value.PadLeft(12 , '0');
                    LeftJustified_Set("AUXNO2", "StateLocalIdentifier2", value);
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
                if (!String.IsNullOrWhiteSpace(value))
                {
                    LeftJustified_Set("STATESP", "StateSpecific", value);
                }
            }
        }

        /// <summary>Surgery Date--month</summary>
        [IJEField(123, 1067, 2, "Surgery Date--month", "SUR_MO", 1)]
        public string SUR_MO
        {
            get
            {
                return NumericAllowingUnknown_Get("SUR_MO", "SurgeryMonth");
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    NumericAllowingUnknown_Set("SUR_MO", "SurgeryMonth", value);
                }
            }
        }

        /// <summary>Surgery Date--day</summary>
        [IJEField(124, 1069, 2, "Surgery Date--day", "SUR_DY", 1)]
        public string SUR_DY
        {
            get
            {
                return NumericAllowingUnknown_Get("SUR_DY", "SurgeryDay");
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    NumericAllowingUnknown_Set("SUR_DY", "SurgeryDay", value);
                }
            }
        }

        /// <summary>Surgery Date--year</summary>
        [IJEField(125, 1071, 4, "Surgery Date--year", "SUR_YR", 1)]
        public string SUR_YR
        {
            get
            {
                return NumericAllowingUnknown_Get("SUR_YR", "SurgeryYear");
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    NumericAllowingUnknown_Set("SUR_YR", "SurgeryYear", value);
                }
            }
        }

        /// <summary>Time of Injury Unit</summary>
        [IJEField(126, 1075, 1, "Time of Injury Unit", "TOI_UNIT", 1)]
        public string TOI_UNIT
        {
            get
            {
                if (DOI_YR != "9999" && DOI_YR != "    ")
                {
                    // Military time since that's the form the datetime object VRDR stores the time of injury as.
                    return "M";
                }
                else
                {
                    // Blank since there is no time of injury.
                    return " ";

                }
            }
            set
            { // The TOI is persisted as a datetime, so the A/P/M is meaningless.   This set is a NOOP, but generate a diagnostic for A and P
                if (value != "M" && value != " ")
                {
                    validationErrors.Add($"Error: IJE field TOI_UNIT contains string '{value}' but can only be set to M or blank");
                }
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
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Set_MappingIJEToFHIR(Mappings.YesNoUnknown.IJEToFHIR, "ARMEDF", "MilitaryService", value);
                }
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
                if (!String.IsNullOrWhiteSpace(value))
                {
                    LeftJustified_Set("DINSTI", "DeathLocationName", value);
                }
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
                return Dictionary_Geo_Get("STNUM_D", "DeathLocationAddress", "address", "stnum", true);
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Dictionary_Geo_Set("STNUM_D", "DeathLocationAddress", "address", "stnum", false, value);
                }
            }
        }

        /// <summary>Place of death. Pre Directional</summary>
        [IJEField(132, 1172, 10, "Place of death. Pre Directional", "PREDIR_D", 1)]
        public string PREDIR_D
        {
            get
            {
                return Dictionary_Geo_Get("PREDIR_D", "DeathLocationAddress", "address", "predir", true);
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Dictionary_Geo_Set("PREDIR_D", "DeathLocationAddress", "address", "predir", false, value);
                }
            }
        }

        /// <summary>Place of death. Street name</summary>
        [IJEField(133, 1182, 50, "Place of death. Street name", "STNAME_D", 1)]
        public string STNAME_D
        {
            get
            {
                return Dictionary_Geo_Get("STNAME_D", "DeathLocationAddress", "address", "stname", true);
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Dictionary_Geo_Set("STNAME_D", "DeathLocationAddress", "address", "stname", false, value);
                }
            }
        }

        /// <summary>Place of death. Street designator</summary>
        [IJEField(134, 1232, 10, "Place of death. Street designator", "STDESIG_D", 1)]
        public string STDESIG_D
        {
            get
            {
                return Dictionary_Geo_Get("STDESIG_D", "DeathLocationAddress", "address", "stdesig", true);
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Dictionary_Geo_Set("STDESIG_D", "DeathLocationAddress", "address", "stdesig", false, value);
                }
            }
        }

        /// <summary>Place of death. Post Directional</summary>
        [IJEField(135, 1242, 10, "Place of death. Post Directional", "POSTDIR_D", 1)]
        public string POSTDIR_D
        {
            get
            {
                return Dictionary_Geo_Get("POSTDIR_D", "DeathLocationAddress", "address", "postdir", true);
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Dictionary_Geo_Set("POSTDIR_D", "DeathLocationAddress", "address", "postdir", false, value);
                }
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
                var stateCode = Dictionary_Geo_Get("DSTATE", "DeathLocationAddress", "address", "state", false);
                //var mortalityData = MortalityData.Instance;
                string statetextd = dataLookup.StateCodeToStateName(stateCode);
                if (statetextd == null)
                {
                    statetextd = " ";
                }
                return (Truncate(statetextd, 28).PadRight(28, ' '));
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
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Dictionary_Geo_Set("COUNTYTEXT_D", "DeathLocationAddress", "address", "county", false, value);
                }
            }
        }

        /// <summary>Place of death. City FIPS code</summary>
        [IJEField(140, 1345, 5, "Place of death. City FIPS code", "CITYCODE_D", 1)]
        public string CITYCODE_D
        {
            get
            {
                return Dictionary_Geo_Get("CITYCODE_D", "DeathLocationAddress", "address", "cityC", true);
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Dictionary_Geo_Set("COUNTYTEXT_D", "DeathLocationAddress", "address", "cityC", false, value);
                }
            }
        }

        /// <summary>Place of death. Longitude</summary>
        [IJEField(141, 1350, 17, "Place of death. Longitude", "LONG_D", 1)]
        public string LONG_D
        {
            get
            {
                return LeftJustified_Get("LONG_D", "DeathLocationLongitude");
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    LeftJustified_Set("LONG_D", "DeathLocationLongitude", value);
                }
            }
        }

        /// <summary>Place of Death. Latitude</summary>
        [IJEField(142, 1367, 17, "Place of Death. Latitude", "LAT_D", 1)]
        public string LAT_D
        {
            get
            {
                return LeftJustified_Get("LAT_D", "DeathLocationLatitude");
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    LeftJustified_Set("LAT_D", "DeathLocationLatitude", value);
                }
            }
        }

        /// <summary>Decedent's spouse living at decedent's DOD?</summary>
        [IJEField(143, 1384, 1, "Decedent's spouse living at decedent's DOD?", "SPOUSELV", 1)]
        public string SPOUSELV
        {
            get
            {
                return Get_MappingFHIRToIJE(Mappings.SpouseAlive.FHIRToIJE, "SpouseAlive", "SPOUSELV");
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Set_MappingIJEToFHIR(Mappings.SpouseAlive.IJEToFHIR, "SPOUSELV", "SpouseAlive", value);
                }
            }
        }

        /// <summary>Spouse's First Name</summary>
        [IJEField(144, 1385, 50, "Spouse's First Name", "SPOUSEF", 1)]
        public string SPOUSEF
        {
            get
            {
                string[] names = record.SpouseGivenNames;
                if (names.Length > 0)
                {
                    return Truncate(names[0], 50).PadRight(50, ' ');
                }
                return new string(' ', 50);
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
        {
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
                //               var mortalityData = MortalityData.Instance;
                string statetextr = dataLookup.StateCodeToStateName(stateCode);
                if (statetextr == null)
                {
                    statetextr = " ";
                }
                return (Truncate(statetextr, 28).PadRight(28, ' '));
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
                //                var mortalityData = MortalityData.Instance;
                string countrytextr = dataLookup.CountryCodeToCountryName(countryCode);
                if (countrytextr == null)
                {
                    countrytextr = " ";
                }
                return (Truncate(countrytextr, 28).PadRight(28, ' '));
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
                return Get_MappingFHIRToIJE(Mappings.HispanicOrigin.FHIRToIJE, "HispanicCode", "DETHNICE");
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Set_MappingIJEToFHIR(Mappings.HispanicOrigin.IJEToFHIR, "DETHNICE", "HispanicCode", value);
                }
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
        [IJEField(166, 1808, 50, "Middle Name of Decedent", "DMIDDLE", 3)]
        public string DMIDDLE
        {
            get
            {
                string[] names = record.GivenNames;
                if (names.Length > 1)
                {
                    return Truncate(names[1], 50).PadRight(50, ' ');
                }
                return new string(' ', 50);
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    if (String.IsNullOrWhiteSpace(GNAME)) throw new ArgumentException("Middle name cannot be set before first name");
                    if (record.GivenNames != null)
                    {
                        List<string> names = record.GivenNames.ToList();
                        if (names.Count() > 1) names[1] = value.Trim(); else names.Add(value.Trim());
                        record.GivenNames = names.ToArray();
                    }
                }
            }
        }

        /// <summary>Father's First Name</summary>
        [IJEField(167, 1858, 50, "Father's First Name", "DDADF", 1)]
        public string DDADF
        {
            get
            {
                string[] names = record.FatherGivenNames;
                if (names != null && names.Length > 0)
                {
                    return Truncate(names[0], 50).PadRight(50, ' ');
                }
                return new string(' ', 50);
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
        {
            get
            {
                string[] names = record.FatherGivenNames;
                if (names != null && names.Length > 1)
                {
                    return Truncate(names[1], 50).PadRight(50, ' ');
                }
                return new string(' ', 50);
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    if (String.IsNullOrWhiteSpace(DDADF)) throw new ArgumentException("Middle name cannot be set before first name");
                    if (record.FatherGivenNames != null)
                    {
                        List<string> names = record.FatherGivenNames.ToList();
                        if (names.Count() > 1) names[1] = value.Trim(); else names.Add(value.Trim());
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
                    return Truncate(names[0], 50).PadRight(50, ' ');
                }
                return new string(' ', 50);
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
        {
            get
            {
                string[] names = record.MotherGivenNames;
                if (names != null && names.Length > 1)
                {
                    return Truncate(names[1], 50).PadRight(50, ' ');
                }
                return new string(' ', 50);
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    if (String.IsNullOrWhiteSpace(DMOMF)) throw new ArgumentException("Middle name cannot be set before first name");
                    if (record.MotherGivenNames != null)
                    {
                        List<string> names = record.MotherGivenNames.ToList();
                        if (names.Count() > 1) names[1] = value.Trim(); else names.Add(value.Trim());
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
                if (!String.IsNullOrWhiteSpace(value))
                {
                    LeftJustified_Set("DMOMMDN", "MotherMaidenName", value);
                }
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
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Set_MappingIJEToFHIR(Mappings.YesNoUnknown.IJEToFHIR, "REFERRED", "ExaminerContacted", value);
                }
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
                if (!String.IsNullOrWhiteSpace(value))
                {
                    LeftJustified_Set("POILITRL", "InjuryPlaceDescription", value);
                }
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
                if (!String.IsNullOrWhiteSpace(value))
                {
                    LeftJustified_Set("HOWINJ", "InjuryDescription", value);
                }
            }
        }
        /// <summary>If Transportation Accident, Specify</summary>
        [IJEField(175, 2409, 30, "If Transportation Accident, Specify", "TRANSPRT", 1)]
        public string TRANSPRT
        {
            get
            {
                var ret = record.TransportationRoleHelper;
                if (ret != null && Mappings.TransportationIncidentRole.FHIRToIJE.ContainsKey(ret))
                {
                    return Get_MappingFHIRToIJE(Mappings.TransportationIncidentRole.FHIRToIJE, "TransportationRole", "TRANSPRT");
                }
                else
                {
                    return ret;  // If the return value is not a code, it is just an arbitrary string, so return it.
                }
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    if (Mappings.TransportationIncidentRole.IJEToFHIR.ContainsKey(value.Split(' ')[0]))
                    {
                        Set_MappingIJEToFHIR(Mappings.TransportationIncidentRole.IJEToFHIR, "TRANSPRT", "TransportationRole", value.Trim());
                    }
                    else
                    {
                        record.TransportationRoleHelper = value;   // If the value is not a valid code, it is just an arbitrary string.  The helper will deal with it.
                    }
                }
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
                    Dictionary_Geo_Set("CITYCODE_I", "InjuryLocationAddress", "address", "cityC", true, value);
                }
            }
        }

        /// <summary>State, U.S. Territory or Canadian Province of Injury - code</summary>
        [IJEField(180, 2503, 2, "State, U.S. Territory or Canadian Province of Injury - code", "STATECODE_I", 1)]
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

        /// <summary>Place of injury. Longitude</summary>
        [IJEField(181, 2505, 17, "Place of injury. Longitude", "LONG_I", 1)]
        public string LONG_I
        {
            get
            {
                return LeftJustified_Get("LONG_I", "InjuryLocationLongitude");
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    LeftJustified_Set("LONG_I", "InjuryLocationLongitude", value);
                }
            }
        }

        /// <summary>Place of injury. Latitude</summary>
        [IJEField(182, 2522, 17, "Place of injury. Latitude", "LAT_I", 1)]
        public string LAT_I
        {
            get
            {
                return LeftJustified_Get("LAT_I", "InjuryLocationLatitude");
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    LeftJustified_Set("LAT_I", "InjuryLocationLatitude", value);
                }
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
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Set_MappingIJEToFHIR(Mappings.ReplaceStatus.IJEToFHIR, "REPLACE", "ReplaceStatus", value);
                }
            }
        }

        /// <summary>Cause of Death Part I Line a</summary>
        [IJEField(185, 2542, 120, "Cause of Death Part I Line a", "COD1A", 1)]
        public string COD1A
        {
            get
            {
                if (!String.IsNullOrWhiteSpace(record.COD1A))
                {
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
                if (!String.IsNullOrWhiteSpace(record.INTERVAL1A))
                {
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
                if (!String.IsNullOrWhiteSpace(record.COD1B))
                {
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
                if (!String.IsNullOrWhiteSpace(record.INTERVAL1B))
                {
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
                if (!String.IsNullOrWhiteSpace(record.COD1C))
                {
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
                if (!String.IsNullOrWhiteSpace(record.INTERVAL1C))
                {
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
                if (!String.IsNullOrWhiteSpace(record.COD1D))
                {
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
                if (!String.IsNullOrWhiteSpace(record.INTERVAL1D))
                {
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
        {
            get
            {
                string[] names = record.SpouseGivenNames;
                if (names != null && names.Length > 1)
                {
                    return Truncate(names[1], 50).PadRight(50, ' ');
                }
                return new string(' ', 50);
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    if (String.IsNullOrWhiteSpace(SPOUSEF)) throw new ArgumentException("Middle name cannot be set before first name");
                    if (record.SpouseGivenNames != null)
                    {
                        List<string> names = record.SpouseGivenNames.ToList();
                        if (names.Count() > 1) names[1] = value.Trim(); else names.Add(value.Trim());
                        record.SpouseGivenNames = names.ToArray();
                    }
                }
            }
        }

        /// <summary>Spouse's Suffix</summary>
        [IJEField(198, 3475, 10, "Spouse's Suffix", "SPOUSESUFFIX", 1)]
        public string SPOUSESUFFIX
        {
            get
            {
                return LeftJustified_Get("SPOUSESUFFIX", "SpouseSuffix");
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    LeftJustified_Set("SPOUSESUFFIX", "SpouseSuffix", value.Trim());
                }
            }
        }
        /// <summary>Father's Suffix</summary>
        [IJEField(199, 3485, 10, "Father's Suffix", "FATHERSUFFIX", 1)]
        public string FATHERSUFFIX
        {
            get
            {
                return LeftJustified_Get("FATHERSUFFIX", "FatherSuffix");
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    LeftJustified_Set("FATHERSUFFIX", "FatherSuffix", value.Trim());
                }
            }
        }

        /// <summary>Mother's Suffix</summary>
        [IJEField(200, 3495, 10, "Mother's Suffix", "MOTHERSSUFFIX", 1)]
        public string MOTHERSSUFFIX
        {
            get
            {
                return LeftJustified_Get("MOTHERSSUFFIX", "MotherSuffix");
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    LeftJustified_Set("MOTHERSSUFFIX", "MotherSuffix", value.Trim());
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
                var stateCode = Dictionary_Geo_Get("DISPSTATECD", "InjuryLocationAddress", "address", "state", false);
                //                var mortalityData = MortalityData.Instance;
                return dataLookup.StateCodeToStateName(stateCode);
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
                if (!String.IsNullOrWhiteSpace(value))
                {
                    LeftJustified_Set("FUNFACNAME", "FuneralHomeName", value);
                }
            }
        }

        /// <summary>Funeral Facility - Street number</summary>
        [IJEField(207, 3698, 10, "Funeral Facility - Street number", "FUNFACSTNUM", 1)]
        public string FUNFACSTNUM
        {
            get
            {
                return Dictionary_Geo_Get("FUNFACSTNUM", "FuneralHomeAddress", "address", "stnum", true);
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Dictionary_Geo_Set("FUNFACSTNUM", "FuneralHomeAddress", "address", "stnum", false, value);
                }
            }
        }

        /// <summary>Funeral Facility - Pre Directional</summary>
        [IJEField(208, 3708, 10, "Funeral Facility - Pre Directional", "FUNFACPREDIR", 1)]
        public string FUNFACPREDIR
        {
            get
            {
                return Dictionary_Geo_Get("FUNFACPREDIR", "FuneralHomeAddress", "address", "predir", true);
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Dictionary_Geo_Set("FUNFACPREDIR", "FuneralHomeAddress", "address", "predir", false, value);
                }
            }
        }

        /// <summary>Funeral Facility - Street name</summary>
        [IJEField(209, 3718, 28, "Funeral Facility - Street name", "FUNFACSTRNAME", 1)]
        public string FUNFACSTRNAME
        {
            get
            {
                return Dictionary_Geo_Get("FUNFACSTRNAME", "FuneralHomeAddress", "address", "stname", true);
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Dictionary_Geo_Set("FUNFACSTRNAME", "FuneralHomeAddress", "address", "stname", false, value);
                }
            }
        }

        /// <summary>Funeral Facility - Street designator</summary>
        [IJEField(210, 3746, 10, "Funeral Facility - Street designator", "FUNFACSTRDESIG", 1)]
        public string FUNFACSTRDESIG
        {
            get
            {
                return Dictionary_Geo_Get("FUNFACSTRDESIG", "FuneralHomeAddress", "address", "stdesig", true);
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Dictionary_Geo_Set("FUNFACSTRDESIG", "FuneralHomeAddress", "address", "stdesig", false, value);
                }
            }
        }

        /// <summary>Funeral Facility - Post Directional</summary>
        [IJEField(211, 3756, 10, "Funeral Facility - Post Directional", "FUNPOSTDIR", 1)]
        public string FUNPOSTDIR
        {
            get
            {
                return Dictionary_Geo_Get("FUNPOSTDIR", "FuneralHomeAddress", "address", "postdir", true);
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Dictionary_Geo_Set("FUNPOSTDIR", "FuneralHomeAddress", "address", "postdir", false, value);
                }
            }
        }

        /// <summary>Funeral Facility - Unit or apt number</summary>
        [IJEField(212, 3766, 7, "Funeral Facility - Unit or apt number", "FUNUNITNUM", 1)]
        public string FUNUNITNUM
        {
            get
            {
                return Dictionary_Geo_Get("FUNUNITNUM", "FuneralHomeAddress", "address", "unitnum", true);
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Dictionary_Geo_Set("FUNUNITNUM", "FuneralHomeAddress", "address", "unitnum", false, value);
                }
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
                }
            }
        }

        /// <summary>State, U.S. Territory or Canadian Province of Funeral Facility - code</summary>
        [IJEField(215, 3851, 2, "State, U.S. Territory or Canadian Province of Funeral Facility - code", "FUNSTATECD", 1)]
        public string FUNSTATECD
        {
            get
            {

                return Dictionary_Geo_Get("FUNSTATECD", "FuneralHomeAddress", "address", "state", true);
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Dictionary_Geo_Set("FUNSTATECD", "FuneralHomeAddress", "address", "state", true, value);
                }
            }
        }

        /// <summary>State, U.S. Territory or Canadian Province of Funeral Facility - literal</summary>
        [IJEField(216, 3853, 28, "State, U.S. Territory or Canadian Province of Funeral Facility - literal", "FUNSTATE", 1)]
        public string FUNSTATE
        {
            get
            {
                var stateCode = Dictionary_Geo_Get("FUNSTATE", "FuneralHomeAddress", "address", "state", false);
                //                var mortalityData = MortalityData.Instance;
                string funstate = dataLookup.StateCodeToStateName(stateCode);
                if (funstate == null)
                {
                    funstate = " ";
                }
                return (Truncate(funstate, 28).PadRight(28, ' '));
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
                var month = record.DateOfDeathPronouncementMonth;
                var day = record.DateOfDeathPronouncementDay;
                var year = record.DateOfDeathPronouncementYear;
                if (month == null || day == null || year == null)
                {
                    return new String(' ', 8);
                }
                else
                {
                    return String.Format("{0:00}{1:00}{2:0000}", month, day, year);
                }
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    var mm = value.Substring(0, 2);
                    var dd = value.Substring(2, 2);
                    var yyyy = value.Substring(4, 4);
                    record.DateOfDeathPronouncementMonth = int.Parse(mm);
                    record.DateOfDeathPronouncementDay = int.Parse(dd);
                    record.DateOfDeathPronouncementYear = int.Parse(yyyy);
                }
            }
        }

        /// <summary>Person Pronouncing Time Pronounced</summary>
        [IJEField(219, 3898, 4, "Person Pronouncing Time Pronounced", "PPTIME", 1)]
        public string PPTIME
        {
            get
            {
                var fhirTimeStr = record.DateOfDeathPronouncementTime;
                if (fhirTimeStr == null) {
                    return "    ";
                }
                else {
                    var HH = fhirTimeStr.Substring(0, 2);
                    var mm = fhirTimeStr.Substring(3, 2);
                    var ijeTime = HH + mm;
                    return ijeTime;
                }
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    var HH = value.Substring(0, 2);
                    var mm = value.Substring(2, 2);
                    var fhirTimeStr = HH + ":" + mm + ":00";
                    record.DateOfDeathPronouncementTime = fhirTimeStr;
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
                    return Truncate(names[0], 50).PadRight(50, ' ');
                }
                return new string(' ', 50);
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
                    return Truncate(names[1], 50).PadRight(50, ' ');
                }
                return new string(' ', 50);
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    if (String.IsNullOrWhiteSpace(CERTFIRST)) throw new ArgumentException("Middle name cannot be set before first name");
                    if (record.GivenNames != null)
                    {
                        List<string> names = record.CertifierGivenNames.ToList();
                        if (names.Count() > 1) names[1] = value.Trim(); else names.Add(value.Trim());
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
                if (!String.IsNullOrWhiteSpace(value))
                {
                    LeftJustified_Set("CERTLAST", "CertifierFamilyName", value);
                }
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
                if (!String.IsNullOrWhiteSpace(value))
                {
                    LeftJustified_Set("CERTSUFFIX", "CertifierSuffix", value);
                }
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
                var stateCode = Dictionary_Get("CERTSTATE", "CertifierAddress", "addressState");
                //                var mortalityData = MortalityData.Instance;
                string certstate = dataLookup.StateCodeToStateName(stateCode);
                if (certstate == null)
                {
                    certstate = " ";
                }
                return (Truncate(certstate, 28).PadRight(28, ' '));
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
                var stateCode = Dictionary_Geo_Get("STATECODE_I", "InjuryLocationAddress", "address", "state", false);
                //                var mortalityData = MortalityData.Instance;
                string stinjury = dataLookup.StateCodeToStateName(stateCode);
                if (stinjury == null)
                {
                    stinjury = " ";
                }
                return (Truncate(stinjury, 28).PadRight(28, ' '));
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
                //                var mortalityData = MortalityData.Instance;
                string statebth = dataLookup.StateCodeToStateName(stateCode);
                if (statebth == null)
                {
                    statebth = " ";
                }
                return (Truncate(statebth, 28).PadRight(28, ' '));

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
                var countryCode = Dictionary_Geo_Get("DTHCOUNTRYCD", "Residence", "address", "country", false);
                //                var mortalityData = MortalityData.Instance;
                string dthcountry = dataLookup.CountryCodeToCountryName(countryCode);
                if (dthcountry == null)
                {
                    dthcountry = " ";
                }
                return (Truncate(dthcountry, 28).PadRight(28, ' '));
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
                return Get_MappingFHIRToIJE(Mappings.HispanicOrigin.FHIRToIJE, "HispanicCodeForLiteral", "DETHNIC5C");
            }
            set
            {
                Set_MappingIJEToFHIR(Mappings.HispanicOrigin.IJEToFHIR, "DETHNIC5C", "HispanicCodeForLiteral", value);
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
