using System;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using Hl7.Fhir.Model;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FhirDeathRecord
{
    // TODO: Consider abstrancting out concept of fixed length formats from this and IJE,
    // including attrubutes and helper functions

    /// <summary>Property attribute used to describe a field in the NAACCR format.</summary>
    [System.AttributeUsage(System.AttributeTargets.Property)]
    public class NAACCRField : System.Attribute
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
        public NAACCRField(int field, int location, int length, string contents, string name, int priority)
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
    /// a record in NAACCR format. Each property of this class corresponds exactly
    /// with a field in the NAACCR format. The getters convert from the embedded
    /// FHIR based <c>DeathRecord</c> to the NAACCR format for a specific field, and
    /// the setters convert from NAACCR format for a specific field and set that value
    /// on the embedded FHIR based <c>DeathRecord</c>.</summary>
    public class NAACCRRecord
    {

        private const int NAACCRRecordLength = 24194;

        /// <summary>FHIR based death record.</summary>
        private DeathRecord record;

        /// <summary>NAACCR data lookup helper. Thread-safe singleton!</summary>
        private MortalityData dataLookup = MortalityData.Instance;

        /// <summary>Constructor that takes a <c>DeathRecord</c>.</summary>
        public NAACCRRecord(DeathRecord record)
        {
            this.record = record;
        }

        /// <summary>Constructor that takes a NAACCR string and builds a corresponding internal <c>DeathRecord</c>.</summary>
        public NAACCRRecord(string naaccr)
        {
            if (naaccr == null)
            {
                throw new ArgumentException("NAACCR string cannot be null.");
            }
            if (naaccr.Length < NAACCRRecordLength)
            {
                naaccr = naaccr.PadRight(NAACCRRecordLength, ' ');
            }
            this.record = new DeathRecord();
            // Loop over every property (these are the fields); Order by priority
            List<PropertyInfo> properties = typeof(NAACCRRecord).GetProperties().ToList().OrderBy(p => ((NAACCRField)p.GetCustomAttributes().First()).Priority).ToList();
            foreach(PropertyInfo property in properties)
            {
                // Grab the field attributes
                NAACCRField info = (NAACCRField)property.GetCustomAttributes().First();
                // Grab the field value
                string field = naaccr.Substring(info.Location - 1, info.Length);
                // Set the value on this NAACCRRecord (and the embedded record)
                property.SetValue(this, field);
            }
        }

        /// <summary>Converts the internal <c>DeathRecord</c> into a NAACCR string.</summary>
        public override string ToString()
        {
            // Start with empty NAACCRRecord record
            StringBuilder naaccr = new StringBuilder(new String(' ', NAACCRRecordLength), NAACCRRecordLength);
            // Loop over every property (these are the fields)
            foreach(PropertyInfo property in typeof(NAACCRRecord).GetProperties())
            {
                // Grab the field value
                string field = Convert.ToString(property.GetValue(this, null));
                // Grab the field attributes
                NAACCRField info = (NAACCRField)property.GetCustomAttributes().First();
                // Be mindful about lengths
                if (field.Length > info.Length)
                {
                    field = field.Substring(0, info.Length);
                }
                // Insert the field value into the record
                naaccr.Insert(info.Location - 1, field);
            }
            return naaccr.ToString();
        }

        /// <summary>Returns the corresponding <c>DeathRecord</c> for this NAACCR string.</summary>
        public DeathRecord ToDeathRecord()
        {
            return this.record;
        }

        ///<summary>Populates additional fields by calling an external NLP service.</summary>
        // TODO: Very quick proof of concept of calling an external service, clearly not the right place for this
        public void ConsultNLPService()
        {
            // TODO: Update this to use HTTPClient()
            WebClient client = new WebClient();
            string proxy = Environment.GetEnvironmentVariable("HTTP_PROXY");
            if (proxy != null) {
                WebProxy wp = new WebProxy(proxy);
                client.Proxy = wp;
            }

            List<string> causes = new List<string>();
            foreach(Tuple<string, string, Dictionary<string, string>> cause in record.CausesOfDeath)
            {
                causes.Add(cause.Item1);
            }
            Dictionary<string,List<string>> reports = new Dictionary<string,List<string>>();
            reports.Add("reports", causes);
            string json = JsonConvert.SerializeObject(reports);
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            string result = client.UploadString("http://localhost:5000/", json);
            Console.WriteLine(result);
        }

        /////////////////////////////////////////////////////////////////////////////////
        //
        // Class helper methods for getting and settings NAACCR fields.
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

        /// <summary>Grabs the NAACCRInfo for a specific NAACCR field name.</summary>
        private static NAACCRField FieldInfo(string naaccrFieldName)
        {
            return (NAACCRField)typeof(NAACCRRecord).GetProperty(naaccrFieldName).GetCustomAttributes().First();
        }

        /// <summary>Get a value on the DeathRecord whose NAACCR type is a left justified string.</summary>
        private string LeftJustified_Get(string naaccrFieldName, string fhirFieldName)
        {
            NAACCRField info = FieldInfo(naaccrFieldName);
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

        /// <summary>Set a value on the DeathRecord whose NAACCR type is a left justified string.</summary>
        private void LeftJustified_Set(string naaccrFieldName, string fhirFieldName, string value)
        {
            NAACCRField info = FieldInfo(naaccrFieldName);
            typeof(DeathRecord).GetProperty(fhirFieldName).SetValue(this.record, value.Trim());
        }

        /// <summary>Get a value on the DeathRecord whose property is a Dictionary type.</summary>
        private string Dictionary_Get(string naaccrFieldName, string fhirFieldName, string key)
        {
            NAACCRField info = FieldInfo(naaccrFieldName);
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

        /// <summary>Set a value on the DeathRecord whose property is a Dictionary type.</summary>
        private void Dictionary_Set(string naaccrFieldName, string fhirFieldName, string key, string value)
        {
            NAACCRField info = FieldInfo(naaccrFieldName);
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
        private string Dictionary_Geo_Get(string naaccrFieldName, string fhirFieldName, string keyPrefix, string geoType, bool isCoded)
        {
            NAACCRField info = FieldInfo(naaccrFieldName);
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
        private void Dictionary_Geo_Set(string naaccrFieldName, string fhirFieldName, string keyPrefix, string geoType, bool isCoded, string value)
        {
            NAACCRField info = FieldInfo(naaccrFieldName);
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
                    else if (geoType == "state")
                    {
                        string state = dataLookup.StateCodeToStateName(value);
                        if (!String.IsNullOrWhiteSpace(state))
                        {
                            dictionary[key] = state;
                        }
                    }
                    else if (geoType == "country")
                    {
                        string country = dataLookup.CountryCodeToCountryName(value);
                        if (!String.IsNullOrWhiteSpace(country))
                        {
                            dictionary[key] = country;
                        }
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

        /////////////////////////////////////////////////////////////////////////////////
        //
        // Class Properties that provide getters and setters for each of the NAACCR
        // fields.
        //
        // Getters look at the embedded DeathRecord and convert values to NAACCR style.
        // Setters convert and store NAACCR style values to the embedded DeathRecord.
        //
        /////////////////////////////////////////////////////////////////////////////////

        /// <summary>Name--First</summary>
        [NAACCRField(2240, 4089, 40, "Name--First", "nameFirst", 1)]
        public string nameFirst
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

        /// <summary>Name--Middle</summary>
        [NAACCRField(2250, 4129, 40, "Name--Middle", "nameMiddle", 2)]
        public string nameMiddle
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

        /// <summary>Name--Last</summary>
        [NAACCRField(2230, 4049, 40, "Name--Last", "nameLast", 1)]
        public string nameLast
        {
            get
            {
                return LeftJustified_Get("nameLast", "FamilyName");
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    LeftJustified_Set("nameLast", "FamilyName", value.Trim());
                }
            }
        }

        /// <summary>Autopsy</summary>
        [NAACCRField(1930, 2947, 1, "Autopsy", "autopsy", 1)]
        public string autopsy
        {
            get
            {
                // TODO: This null check returns a warning (The result of the expression is always 'false'
                // since a value of type 'bool' is never equal to 'null') which suggests that the abstraction
                // in IJEMortality.cs may have the same issue (just never warned about because of introspection)
                if (record.AutopsyPerformed == null)
                {
                    return "9";
                }
                else if (record.AutopsyPerformed)
                {
                    return "1";
                }
                else
                {
                    return "2";
                }
            }
            set
            {
                if (value == "1")
                {
                    record.AutopsyPerformed = true;
                }
                else if (value == "2")
                {
                    record.AutopsyPerformed = false;
                }
            }
        }

        /// <summary>Date of birth</summary>
        [NAACCRField(240, 226, 8, "Date of birth", "dateOfBirth", 1)]
        public string dateOfBirth
        {
            get
            {
                DateTimeOffset date;
                string dateString = record.DateOfBirth;
                if (DateTimeOffset.TryParse(dateString, out date))
                {
                    return date.ToString("yyyyMMdd");
                }
                else
                {
                    return new String(' ', 8);
                }
            }
            set
            {
                // TODO: Complete this
            }
        }

        /// <summary>Type of reporting source</summary>
        [NAACCRField(500, 577, 1, "Type of reporting source", "typeOfReportingSource", 1)]
        public string typeOfReportingSource
        {
            get
            {
                // Always 7 (death certificate)
                return "7";
            }
            set
            {
                // NOOP
            }
        }

        /// <summary>Vital status</summary>
        [NAACCRField(1760, 2785, 1, "Vital status", "vitalStatus", 1)]
        public string vitalStatus
        {
            get
            {
                // Always 0 (dead)
                return "0";
            }
            set
            {
                // NOOP
            }
        }

        /// <summary>Casefinding source</summary>
        [NAACCRField(501, 578, 2, "Casefinding source", "casefindingSource", 1)]
        public string casefindingSource
        {
            get
            {
                // Always 80 (death certificate)
                return "80";
            }
            set
            {
                // NOOP
            }
        }

        /// <summary>Social security number</summary>
        [NAACCRField(2320, 4328, 9, "Social security number", "socialSecurityNumber", 1)]
        public string socialSecurityNumber
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
                if (!String.IsNullOrWhiteSpace(value) && value != "999999999")
                {
                    LeftJustified_Set("socialSecurityNumber", "SSN", value);
                }
            }
        }

        /// <summary>TEXT--USUAL OCCUPATION</summary>
        [NAACCRField(310, 247, 100, "TEXT--USUAL OCCUPATION", "textUsualOccupation", 1)]
        public string textUsualOccupation
        {
            get
            {
                return Dictionary_Get("textUsualOccupation", "Occupation", "jobDescription");
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Dictionary_Set("textUsualOccupation", "Occupation", "jobDescription", value);
                }
            }
        }

        /// <summary>TEXT--USUAL INDUSTRY</summary>
        [NAACCRField(320, 347, 100, "TEXT--USUAL INDUSTRY", "textUsualIndustry", 1)]
        public string textUsualIndustry
        {
            get
            {
                return Dictionary_Get("textUsualIndustry", "Occupation", "industryDescription");
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Dictionary_Set("textUsualIndustry", "Occupation", "industryDescription", value);
                }
            }
        }

        /// <summary>ADDR AT DX--CITY</summary>
        [NAACCRField(70, 74, 50, "ADDR AT DX--CITY", "addrAtDxCity", 1)]
        public string addrAtDxCity
        {
            get
            {
                return Dictionary_Geo_Get("addrAtDxCity", "Residence", "residence", "city", false);
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Dictionary_Geo_Set("addrAtDxCity", "Residence", "residence", "city", false, value);
                }
            }
        }

        /// <summary>ADDR AT DX--NO &amp; STREET</summary>
        [NAACCRField(2330, 4348, 60, "ADDR AT DX--NO & STREET", "addrAtDxNoStreet", 1)]
        public string addrAtDxNoStreet
        {
            get
            {
                return Dictionary_Get("addrAtDxNoStreet", "Residence", "residenceLine1");
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Dictionary_Set("addrAtDxNoStreet", "Residence", "residenceLine1", value);
                }
            }
        }

        /// <summary>ADDR AT DX--POSTAL CODE</summary>
        [NAACCRField(100, 126, 9, "ADDR AT DX--POSTAL CODE", "addrAtDxPostalCode", 1)]
        public string addrAtDxPostalCode
        {
            get
            {
                return Dictionary_Geo_Get("addrAtDxPostalCode", "Residence", "residence", "zip", false);
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Dictionary_Geo_Set("addrAtDxPostalCode", "Residence", "residence", "zip", false, value);
                }
            }
        }

        /// <summary>ADDR AT DX--STATE</summary>
        [NAACCRField(80, 124, 2, "ADDR AT DX--STATE", "addrAtDxState", 1)]
        public string addrAtDxState
        {
            get
            {
                return Dictionary_Geo_Get("addrAtDxState", "Residence", "residence", "state", false);
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Dictionary_Geo_Set("addrAtDxState", "Residence", "residence", "state", false, value);
                }
            }
        }


    }
}
