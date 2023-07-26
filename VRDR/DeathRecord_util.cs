// DeathRecord_util.cs
//    Contains utility methods used across the DeathRecords class.


using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.FhirPath;
using Newtonsoft.Json;
using Hl7.Fhir.Utility;

namespace VRDR
{
    /// <summary>Class <c>DeathRecord</c> models a FHIR Vital Records Death Reporting (VRDR) Death
    /// Record. This class was designed to help consume and produce death records that follow the
    /// HL7 FHIR Vital Records Death Reporting Implementation Guide, as described at:
    /// http://hl7.org/fhir/us/vrdr and https://github.com/hl7/vrdr.
    /// </summary>
    public partial class DeathRecord
    {
        /// <summary>Getter helper for anything that uses PartialDateTime, allowing a particular date field (year, month, or day) to be read
        /// from the extension. Returns either a numeric date part, or -1 meaning explicitly unknown, or null meaning not specified.</summary>
        private int? GetPartialDate(Extension partialDateTime, string partURL)
        {
            Extension part = partialDateTime?.Extension?.Find(ext => ext.Url == partURL);
            Extension dataAbsent = part?.Extension?.Find(ext => ext.Url == OtherExtensionURL.DataAbsentReason);
            // extension for absent date can be directly on the part as with year, month, day
            if (dataAbsent != null)
            {
                // The data absent reason is either a placeholder that a field hasen't been set yet (data absent reason of 'temp-unknown') or
                // a claim that there's no data (any other data absent reason, e.g., 'unknown'); return null for the former and "-1" for the latter
                string code = ((Code)dataAbsent.Value).Value;
                if (code == "temp-unknown")
                {
                    return null;
                }
                else
                {
                    return -1;
                }
            }
            // check if the part (e.g. "_valueUnsignedInt") has a data absent reason extension on the value
            Extension dataAbsentOnValue = part?.Value?.Extension?.Find(ext => ext.Url == OtherExtensionURL.DataAbsentReason);
            if (dataAbsentOnValue != null)
            {
                string code = ((Code)dataAbsentOnValue.Value).Value;
                if (code == "temp-unknown")
                {
                    return null;
                }
                else
                {
                    return -1;
                }
            }
            // If we have a value, return it
            if (part?.Value != null)
            {
                return (int?)((UnsignedInt)part.Value).Value; // Untangle a FHIR UnsignedInt in an extension into an int
            }
            // No data present at all, return null
            return null;
        }

        /// <summary>NewBlankPartialDateTimeExtension, Build a blank PartialDateTime extension (which means all the placeholder data absent
        /// reasons are present to note that the data is not in fact present). This method takes an optional flag to determine if this extension
        /// should include the time field, which is not always needed</summary>
        private Extension NewBlankPartialDateTimeExtension(bool includeTime = true)
        {
            Extension partialDateTime = new Extension(includeTime ? ExtensionURL.PartialDateTime : ExtensionURL.PartialDate, null);
            Extension year = new Extension(ExtensionURL.DateYear, null);
            year.Extension.Add(new Extension(OtherExtensionURL.DataAbsentReason, new Code("temp-unknown")));
            partialDateTime.Extension.Add(year);
            Extension month = new Extension(ExtensionURL.DateMonth, null);
            month.Extension.Add(new Extension(OtherExtensionURL.DataAbsentReason, new Code("temp-unknown")));
            partialDateTime.Extension.Add(month);
            Extension day = new Extension(ExtensionURL.DateDay, null);
            day.Extension.Add(new Extension(OtherExtensionURL.DataAbsentReason, new Code("temp-unknown")));
            partialDateTime.Extension.Add(day);
            if (includeTime)
            {
                Extension time = new Extension(ExtensionURL.DateTime, null);
                time.Extension.Add(new Extension(OtherExtensionURL.DataAbsentReason, new Code("temp-unknown")));
                partialDateTime.Extension.Add(time);
            }
            return partialDateTime;
        }
        /// <summary>Setter helper for anything that uses PartialDateTime, allowing a particular date field (year, month, or day) to be
        /// set in the extension. Arguments are the extension to poplulate, the part of the URL to populate, and the value to specify.
        /// The value can be a positive number for an actual value, a -1 meaning that the value is explicitly unknown, or null meaning
        /// the data has not been specified.</summary>
        private void SetPartialDate(Extension partialDateTime, string partURL, int? value)
        {
            Extension part = partialDateTime.Extension.Find(ext => ext.Url == partURL);
            part.Extension.RemoveAll(ext => ext.Url == OtherExtensionURL.DataAbsentReason);
            if (value != null && value != -1)
            {
                part.Value = new UnsignedInt((int)value);
            }
            else
            {
                part.Value = new UnsignedInt();
                // Determine which data absent reason to use based on whether the value is unknown or -1
                part.Value.Extension.Add(new Extension(OtherExtensionURL.DataAbsentReason, new Code(value == -1 ? "unknown" : "temp-unknown")));
            }
        }

        /// <summary>Getter helper for anything that uses PartialDateTime, allowing the time to be read from the extension</summary>
        private string GetPartialTime(Extension partialDateTime)
        {
            Extension part = partialDateTime?.Extension?.Find(ext => ext.Url == ExtensionURL.DateTime);
            Extension dataAbsent = part?.Extension?.Find(ext => ext.Url == OtherExtensionURL.DataAbsentReason);
            // extension for absent date can be directly on the part as with year, month, day
            if (dataAbsent != null)
            {
                // The data absent reason is either a placeholder that a field hasen't been set yet (data absent reason of 'temp-unknown') or
                // a claim that there's no data (any other data absent reason, e.g., 'unknown'); return null for the former and "-1" for the latter
                string code = ((Code)dataAbsent.Value).Value;
                if (code == "temp-unknown")
                {
                    return null;
                }
                else
                {
                    return "-1";
                }
            }
            // check if the part (e.g. "_valueTime") has a data absent reason extension on the value
            Extension dataAbsentOnValue = part?.Value?.Extension?.Find(ext => ext.Url == OtherExtensionURL.DataAbsentReason);
            if (dataAbsentOnValue != null)
            {
                string code = ((Code)dataAbsentOnValue.Value).Value;
                if (code == "temp-unknown")
                {
                    return null;
                }
                else
                {
                    return "-1";
                }
            }
            // If we have a value, return it
            if (part?.Value != null)
            {
                return part.Value.ToString();
            }
            // No data present at all, return null
            return null;
        }

        /// <summary>Setter helper for anything that uses PartialDateTime, allowing the time to be set in the extension</summary>
        private void SetPartialTime(Extension partialDateTime, String value)
        {
            Extension part = partialDateTime.Extension.Find(ext => ext.Url == ExtensionURL.DateTime);
            part.Extension.RemoveAll(ext => ext.Url == OtherExtensionURL.DataAbsentReason);
            if (value != null && value != "-1")
            {
                // we need to force it to be 00:00:00 format to be compliant with the IG because the FHIR class doesn't
                if (value.Length < 8)
                {
                    value += ":";
                    value = value.PadRight(8, '0');
                }
                part.Value = new Time(value);
            }
            else
            {
                part.Value = new Time();
                // Determine which data absent reason to use based on whether the value is unknown or -1
                part.Value.Extension.Add(new Extension(OtherExtensionURL.DataAbsentReason, new Code(value == "-1" ? "unknown" : "temp-unknown")));
            }
        }

        /// <summary>Getter helper for anything that can have a regular FHIR date/time
        /// field (year, month, or day) to be read the value
        /// supports dates and date times but does NOT support extensions</summary>
        private int? GetDateFragment(Element value, string partURL)
        {
            if (value == null)
            {
                return null;
            }
            // If we have a basic value as a valueDateTime use that, otherwise pull from the PartialDateTime extension
            DateTimeOffset? dateTimeOffset = null;
            if (value is FhirDateTime && ((FhirDateTime)value).Value != null)
            {
                // Note: We can't just call ToDateTimeOffset() on the FhirDateTime because want the datetime in whatever local time zone was provided
                dateTimeOffset = DateTimeOffset.Parse(((FhirDateTime)value).Value);
            }
            else if (value is Date && ((Date)value).Value != null)
            {
                // Note: We can't just call ToDateTimeOffset() on the Date because want the date in whatever local time zone was provided
                dateTimeOffset = DateTimeOffset.Parse(((Date)value).Value);
            }
            if (dateTimeOffset != null)
            {
                switch (partURL)
                {
                    case ExtensionURL.DateYear:
                        return ((DateTimeOffset)dateTimeOffset).Year;
                    case ExtensionURL.DateMonth:
                        return ((DateTimeOffset)dateTimeOffset).Month;
                    case ExtensionURL.DateDay:
                        return ((DateTimeOffset)dateTimeOffset).Day;
                    default:
                        throw new ArgumentException("GetDateFragment called with unsupported PartialDateTime segment");
                }
            }
            return null;
        }

        /// <summary>Getter helper for anything that can have a regular FHIR date/time or a PartialDateTime extension, allowing a particular date
        /// field (year, month, or day) to be read from either the value or the extension</summary>
        private int? GetDateFragmentOrPartialDate(Element value, string partURL)
        {
            if (value == null) {
                return null;
            }
            var dateFragment = GetDateFragment(value, partURL);
            if (dateFragment != null)
            {
                return dateFragment;
            }
            // Look for either PartialDate or PartialDateTime
            Extension extension = value.Extension.Find(ext => ext.Url == ExtensionURL.PartialDateTime);
            if (extension == null)
            {
                extension = value.Extension.Find(ext => ext.Url == ExtensionURL.PartialDate);
            }
            return GetPartialDate(extension, partURL);
        }

        private FhirDateTime ConvertFhirTimeToFhirDateTime(Time value) {
            return new FhirDateTime(DateTimeOffset.MinValue.Year, DateTimeOffset.MinValue.Month, DateTimeOffset.MinValue.Day,
                FhirTimeHour(value), FhirTimeMin(value), FhirTimeSec(value), TimeSpan.Zero);
        }

        private int FhirTimeHour(Time value) {
            return int.Parse(value.ToString().Substring(0, 2));
        }

        private int FhirTimeMin(Time value) {
            return int.Parse(value.ToString().Substring(3, 2));
        }

        private int FhirTimeSec(Time value) {
            return int.Parse(value.ToString().Substring(6, 2));
        }

        /// <summary>Getter helper for anything that can have a regular FHIR date/time, allowing the time to be read from the value</summary>
        private string GetTimeFragment(Element value) {
            if (value is FhirDateTime && ((FhirDateTime)value).Value != null)
            {
                // Using FhirDateTime's ToDateTimeOffset doesn't keep the time in the original time zone, so we parse the string representation, first using the appropriate segment of
                // the Regex defined at http://hl7.org/fhir/R4/datatypes.html#dateTime to pull out everything except the time zone
                string dateRegex = "([0-9]([0-9]([0-9][1-9]|[1-9]0)|[1-9]00)|[1-9]000)(-(0[1-9]|1[0-2])(-(0[1-9]|[1-2][0-9]|3[0-1])(T([01][0-9]|2[0-3]):[0-5][0-9]:([0-5][0-9]|60)?)?)?)?";
                Match dateStringMatch = Regex.Match(((FhirDateTime)value).ToString(), dateRegex);
                DateTime dateTime;
                if (dateStringMatch != null && DateTime.TryParse(dateStringMatch.ToString(), out dateTime))
                {
                    TimeSpan timeSpan = new TimeSpan(0, dateTime.Hour, dateTime.Minute, dateTime.Second);
                    return timeSpan.ToString(@"hh\:mm\:ss");
                }
            }
            return null;
        }

        /// <summary>Getter helper for anything that can have a regular FHIR date/time or a PartialDateTime extension, allowing the time to be read
        /// from either the value or the extension</summary>
        private string GetTimeFragmentOrPartialTime(Element value)
        {
            // If we have a basic value as a valueDateTime use that, otherwise pull from the PartialDateTime extension
            string time = GetTimeFragment(value);
            if (time != null) {
                return time;
            }
            return GetPartialTime(value.Extension.Find(ext => ext.Url == ExtensionURL.PartialDateTime));
        }

        /// <summary>Helper function to set a codeable value based on a code and the set of allowed codes.</summary>
        // <param name="field">the field name to set.</param>
        // <param name="code">the code to set the field to.</param>
        // <param name="options">the list of valid options and related display strings and code systems</param>
        private void SetCodeValue(string field, string code, string[,] options)
        {
            // If string is empty don't bother to set the value
            if (code == null || code == "")
            {
                return;
            }
            // Iterate over the allowed options and see if the code supplies is one of them
            for (int i = 0; i < options.GetLength(0); i += 1)
            {
                if (options[i, 0] == code)
                {
                    // Found it, so call the supplied setter with the appropriate dictionary built based on the code
                    // using the supplied options and return
                    Dictionary<string, string> dict = new Dictionary<string, string>();
                    dict.Add("code", code);
                    dict.Add("display", options[i, 1]);
                    dict.Add("system", options[i, 2]);
                    typeof(DeathRecord).GetProperty(field).SetValue(this, dict);
                    return;
                }
            }
            // If we got here we didn't find the code, so it's not a valid option
            throw new System.ArgumentException($"Code '{code}' is not an allowed value for field {field}");
        }

        /// <summary>Convert a "code" dictionary to a FHIR Coding.</summary>
        /// <param name="dict">represents a code.</param>
        /// <returns>the corresponding Coding representation of the code.</returns>
        private Coding DictToCoding(Dictionary<string, string> dict)
        {
            Coding coding = new Coding();
            if (dict != null)
            {
                if (dict.ContainsKey("code") && !String.IsNullOrEmpty(dict["code"]))
                {
                    coding.Code = dict["code"];
                }
                if (dict.ContainsKey("system") && !String.IsNullOrEmpty(dict["system"]))
                {
                    coding.System = dict["system"];
                }
                if (dict.ContainsKey("display") && !String.IsNullOrEmpty(dict["display"]))
                {
                    coding.Display = dict["display"];
                }
                return coding;
            }
            return null;
        }

        /// <summary>Convert a "code" dictionary to a FHIR CodableConcept.</summary>
        /// <param name="dict">represents a code.</param>
        /// <returns>the corresponding CodeableConcept representation of the code.</returns>
        private CodeableConcept DictToCodeableConcept(Dictionary<string, string> dict)
        {
            CodeableConcept codeableConcept = new CodeableConcept();
            Coding coding = DictToCoding(dict);
            codeableConcept.Coding.Add(coding);
            if (dict != null && dict.ContainsKey("text") && !String.IsNullOrEmpty(dict["text"]))
            {
                codeableConcept.Text = dict["text"];
            }
            return codeableConcept;
        }

        /// <summary>Check if a dictionary is empty or a default empty dictionary (all values are null or empty strings)</summary>
        /// <param name="dict">represents a code.</param>
        /// <returns>A boolean identifying whether the provided dictionary is empty or default.</returns>
        private bool IsDictEmptyOrDefault(Dictionary<string, string> dict)
        {
            return dict.Count == 0 || dict.Values.All(v => v == null || v == "");
        }

        /// <summary>Convert a FHIR Coding to a "code" Dictionary</summary>
        /// <param name="coding">a FHIR Coding.</param>
        /// <returns>the corresponding Dictionary representation of the code.</returns>
        private Dictionary<string, string> CodingToDict(Coding coding)
        {
            Dictionary<string, string> dictionary = EmptyCodeDict();
            if (coding != null)
            {
                if (!String.IsNullOrEmpty(coding.Code))
                {
                    dictionary["code"] = coding.Code;
                }
                if (!String.IsNullOrEmpty(coding.System))
                {
                    dictionary["system"] = coding.System;
                }
                if (!String.IsNullOrEmpty(coding.Display))
                {
                    dictionary["display"] = coding.Display;
                }
            }
            return dictionary;
        }

        /// <summary>Convert a FHIR CodableConcept to a "code" Dictionary</summary>
        /// <param name="codeableConcept">a FHIR CodeableConcept.</param>
        /// <returns>the corresponding Dictionary representation of the code.</returns>
        private Dictionary<string, string> CodeableConceptToDict(CodeableConcept codeableConcept)
        {
            if (codeableConcept != null && codeableConcept.Coding != null)
            {
                Coding coding = codeableConcept.Coding.FirstOrDefault();
                var codeDict = CodingToDict(coding);
                if (codeableConcept != null && !String.IsNullOrEmpty(codeableConcept.Text))
                {
                    codeDict["text"] = codeableConcept.Text;
                }
                return codeDict;
            }
            else
            {
                return EmptyCodeableDict();
            }
        }

        /// <summary>Convert an "address" dictionary to a FHIR Address.</summary>
        /// <param name="dict">represents an address.</param>
        /// <returns>the corresponding FHIR Address representation of the address.</returns>
        private Address DictToAddress(Dictionary<string, string> dict)
        {
            Address address = new Address();

            if (dict != null)
            {
                List<string> lines = new List<string>();
                if (dict.ContainsKey("addressLine1") && !String.IsNullOrEmpty(dict["addressLine1"]))
                {
                    lines.Add(dict["addressLine1"]);
                }
                if (dict.ContainsKey("addressLine2") && !String.IsNullOrEmpty(dict["addressLine2"]))
                {
                    lines.Add(dict["addressLine2"]);
                }
                if (lines.Count() > 0)
                {
                    address.Line = lines.ToArray();
                }
                if (dict.ContainsKey("addressCityC") && !String.IsNullOrEmpty(dict["addressCityC"]))
                {
                    Extension cityCode = new Extension();
                    cityCode.Url = ExtensionURL.CityCode;
                    cityCode.Value = new PositiveInt(Int32.Parse(dict["addressCityC"]));
                    address.CityElement = new FhirString();
                    address.CityElement.Extension.Add(cityCode);
                }
                if (dict.ContainsKey("addressCity") && !String.IsNullOrEmpty(dict["addressCity"]))
                {
                    if (address.CityElement != null)
                    {
                        address.CityElement.Value = dict["addressCity"];
                    }
                    else
                    {
                        address.City = dict["addressCity"];
                    }

                }
                if (dict.ContainsKey("addressCountyC") && !String.IsNullOrEmpty(dict["addressCountyC"]))
                {
                    Extension countyCode = new Extension();
                    countyCode.Url = ExtensionURL.DistrictCode;
                    countyCode.Value = new PositiveInt(Int32.Parse(dict["addressCountyC"]));
                    address.DistrictElement = new FhirString();
                    address.DistrictElement.Extension.Add(countyCode);
                }
                if (dict.ContainsKey("addressCounty") && !String.IsNullOrEmpty(dict["addressCounty"]))
                {
                    if (address.DistrictElement != null)
                    {
                        address.DistrictElement.Value = dict["addressCounty"];
                    }
                    else
                    {
                        address.District = dict["addressCounty"];
                    }
                }
                if (dict.ContainsKey("addressState") && !String.IsNullOrEmpty(dict["addressState"]))
                {
                    address.State = dict["addressState"];
                }
                // Special address field to support the jurisdiction extension custom to VRDR to support YC (New York City)
                // as used in the DeathLocationLoc
                if (dict.ContainsKey("addressJurisdiction") && !String.IsNullOrEmpty(dict["addressJurisdiction"]))
                {
                    if (address.StateElement == null)
                    {
                        address.StateElement = new FhirString();
                    }
                    address.StateElement.Extension.RemoveAll(ext => ext.Url == ExtensionURL.LocationJurisdictionId);
                    Extension extension = new Extension(ExtensionURL.LocationJurisdictionId, new FhirString(dict["addressJurisdiction"]));
                    address.StateElement.Extension.Add(extension);
                }
                if (dict.ContainsKey("addressZip") && !String.IsNullOrEmpty(dict["addressZip"]))
                {
                    address.PostalCode = dict["addressZip"];
                }
                if (dict.ContainsKey("addressCountry") && !String.IsNullOrEmpty(dict["addressCountry"]))
                {
                    address.Country = dict["addressCountry"];
                }
                if (dict.ContainsKey("addressStnum") && !String.IsNullOrEmpty(dict["addressStnum"]))
                {
                    Extension stnum = new Extension();
                    stnum.Url = ExtensionURL.StreetNumber;
                    stnum.Value = new FhirString(dict["addressStnum"]);
                    address.Extension.Add(stnum);
                }
                if (dict.ContainsKey("addressPredir") && !String.IsNullOrEmpty(dict["addressPredir"]))
                {
                    Extension predir = new Extension();
                    predir.Url = ExtensionURL.PreDirectional;
                    predir.Value = new FhirString(dict["addressPredir"]);
                    address.Extension.Add(predir);
                }
                if (dict.ContainsKey("addressStname") && !String.IsNullOrEmpty(dict["addressStname"]))
                {
                    Extension stname = new Extension();
                    stname.Url = ExtensionURL.StreetName;
                    stname.Value = new FhirString(dict["addressStname"]);
                    address.Extension.Add(stname);
                }
                if (dict.ContainsKey("addressStdesig") && !String.IsNullOrEmpty(dict["addressStdesig"]))
                {
                    Extension stdesig = new Extension();
                    stdesig.Url = ExtensionURL.StreetDesignator;
                    stdesig.Value = new FhirString(dict["addressStdesig"]);
                    address.Extension.Add(stdesig);
                }
                if (dict.ContainsKey("addressPostdir") && !String.IsNullOrEmpty(dict["addressPostdir"]))
                {
                    Extension postdir = new Extension();
                    postdir.Url = ExtensionURL.PostDirectional;
                    postdir.Value = new FhirString(dict["addressPostdir"]);
                    address.Extension.Add(postdir);
                }
                if (dict.ContainsKey("addressUnitnum") && !String.IsNullOrEmpty(dict["addressUnitnum"]))
                {
                    Extension unitnum = new Extension();
                    unitnum.Url = ExtensionURL.UnitOrAptNumber;
                    unitnum.Value = new FhirString(dict["addressUnitnum"]);
                    address.Extension.Add(unitnum);
                }

            }
            return address;
        }


        /// <summary>Convert a Date Part Extension to an Array.</summary>
        /// <param name="datePartAbsent">a Date Part Extension.</param>
        /// <returns>the corresponding array representation of the date parts.</returns>
        private Tuple<string, string>[] DatePartsToArray(Extension datePartAbsent)
        {
            List<Tuple<string, string>> dateParts = new List<Tuple<string, string>>();
            if (datePartAbsent != null)
            {
                Extension yearAbsentPart = datePartAbsent.Extension.Where(ext => ext.Url == "year-absent-reason").FirstOrDefault();
                Extension monthAbsentPart = datePartAbsent.Extension.Where(ext => ext.Url == "month-absent-reason").FirstOrDefault();
                Extension dayAbsentPart = datePartAbsent.Extension.Where(ext => ext.Url == "day-absent-reason").FirstOrDefault();
                Extension yearPart = datePartAbsent.Extension.Where(ext => ext.Url == "date-year").FirstOrDefault();
                Extension monthPart = datePartAbsent.Extension.Where(ext => ext.Url == "date-month").FirstOrDefault();
                Extension dayPart = datePartAbsent.Extension.Where(ext => ext.Url == "date-day").FirstOrDefault();
                // Year part
                if (yearAbsentPart != null)
                {
                    dateParts.Add(Tuple.Create("year-absent-reason", yearAbsentPart.Value.ToString()));
                }
                if (yearPart != null)
                {
                    dateParts.Add(Tuple.Create("date-year", yearPart.Value.ToString()));
                }
                // Month part
                if (monthAbsentPart != null)
                {
                    dateParts.Add(Tuple.Create("month-absent-reason", monthAbsentPart.Value.ToString()));
                }
                if (monthPart != null)
                {
                    dateParts.Add(Tuple.Create("date-month", monthPart.Value.ToString()));
                }
                // Day Part
                if (dayAbsentPart != null)
                {
                    dateParts.Add(Tuple.Create("day-absent-reason", dayAbsentPart.Value.ToString()));
                }
                if (dayPart != null)
                {
                    dateParts.Add(Tuple.Create("date-day", dayPart.Value.ToString()));
                }
            }
            return dateParts.ToArray();
        }

        /// <summary>Convert an element to an integer or code depending on if the input element is a date part.</summary>
        /// <param name="pair">A key value pair, the key will be used to identify whether the element is a date part.</param>
        private Element DatePartToIntegerOrCode(Tuple<string, string> pair)
        {
            if (pair.Item1 == "date-year" || pair.Item1 == "date-month" || pair.Item1 == "date-day")
            {
                return new Integer(Int32.Parse(pair.Item2));
            }
            else
            {
                return new Code(pair.Item2);
            }
        }

        /// <summary>Convert a FHIR Address to an "address" Dictionary.</summary>
        /// <param name="addr">a FHIR Address.</param>
        /// <returns>the corresponding Dictionary representation of the FHIR Address.</returns>
        private Dictionary<string, string> AddressToDict(Address addr)
        {
            Dictionary<string, string> dictionary = EmptyAddrDict();
            if (addr != null)
            {
                if (addr.Line != null && addr.Line.Count() > 0)
                {
                    dictionary["addressLine1"] = addr.Line.First();
                }

                if (addr.Line != null && addr.Line.Count() > 1)
                {
                    dictionary["addressLine2"] = addr.Line.Last();
                }

                if (addr.CityElement != null)
                {
                    Extension cityCode = addr.CityElement.Extension.Where(ext => ext.Url == ExtensionURL.CityCode).FirstOrDefault();
                    if (cityCode != null)
                    {
                        dictionary["addressCityC"] = cityCode.Value.ToString();
                    }
                }

                if (addr.DistrictElement != null)
                {
                    Extension districtCode = addr.DistrictElement.Extension.Where(ext => ext.Url == ExtensionURL.DistrictCode).FirstOrDefault();
                    if (districtCode != null)
                    {
                        dictionary["addressCountyC"] = districtCode.Value.ToString();
                    }
                }

                Extension stnum = addr.Extension.Where(ext => ext.Url == ExtensionURL.StreetNumber).FirstOrDefault();
                if (stnum != null)
                {
                    dictionary["addressStnum"] = stnum.Value.ToString();
                }

                Extension predir = addr.Extension.Where(ext => ext.Url == ExtensionURL.PreDirectional).FirstOrDefault();
                if (predir != null)
                {
                    dictionary["addressPredir"] = predir.Value.ToString();
                }

                Extension stname = addr.Extension.Where(ext => ext.Url == ExtensionURL.StreetName).FirstOrDefault();
                if (stname != null)
                {
                    dictionary["addressStname"] = stname.Value.ToString();
                }

                Extension stdesig = addr.Extension.Where(ext => ext.Url == ExtensionURL.StreetDesignator).FirstOrDefault();
                if (stdesig != null)
                {
                    dictionary["addressStdesig"] = stdesig.Value.ToString();
                }

                Extension postdir = addr.Extension.Where(ext => ext.Url == ExtensionURL.PostDirectional).FirstOrDefault();
                if (postdir != null)
                {
                    dictionary["addressPostdir"] = postdir.Value.ToString();
                }

                Extension unitnum = addr.Extension.Where(ext => ext.Url == ExtensionURL.UnitOrAptNumber).FirstOrDefault();
                if (unitnum != null)
                {
                    dictionary["addressUnitnum"] = unitnum.Value.ToString();
                }


                if (addr.State != null)
                {
                    dictionary["addressState"] = addr.State;
                }
                if (addr.StateElement != null)
                {
                    dictionary["addressJurisdiction"] = addr.State; // by default.  If extension present, override
                    Extension stateExt = addr.StateElement.Extension.Where(ext => ext.Url == ExtensionURL.LocationJurisdictionId).FirstOrDefault();
                    if (stateExt != null)
                    {
                        dictionary["addressJurisdiction"] = stateExt.Value.ToString();
                    }
                }
                if (addr.City != null)
                {
                    dictionary["addressCity"] = addr.City;
                }
                if (addr.District != null)
                {
                    dictionary["addressCounty"] = addr.District;
                }
                if (addr.PostalCode != null)
                {
                    dictionary["addressZip"] = addr.PostalCode;
                }
                if (addr.Country != null)
                {
                    dictionary["addressCountry"] = addr.Country;
                }
            }
            return dictionary;
        }

        /// <summary>Returns an empty "address" Dictionary.</summary>
        /// <returns>an empty "address" Dictionary.</returns>
        private Dictionary<string, string> EmptyAddrDict()
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add("addressLine1", "");
            dictionary.Add("addressLine2", "");
            dictionary.Add("addressCity", "");
            dictionary.Add("addressCityC", "");
            dictionary.Add("addressCounty", "");
            dictionary.Add("addressCountyC", "");
            dictionary.Add("addressState", "");
            dictionary.Add("addressJurisdiction", "");
            dictionary.Add("addressZip", "");
            dictionary.Add("addressCountry", "");
            dictionary.Add("addressStnum", "");
            dictionary.Add("addressPredir", "");
            dictionary.Add("addressStname", "");
            dictionary.Add("addressStdesig", "");
            dictionary.Add("addressPostdir", "");
            dictionary.Add("addressUnitnum", "");
            return dictionary;
        }

        /// <summary>Returns an empty "code" Dictionary.</summary>
        /// <returns>an empty "code" Dictionary.</returns>
        private Dictionary<string, string> EmptyCodeDict()
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add("code", "");
            dictionary.Add("system", "");
            dictionary.Add("display", "");
            return dictionary;
        }

        /// <summary>Returns an empty "codeable" Dictionary.</summary>
        /// <returns>an empty "codeable" Dictionary.</returns>
        private Dictionary<string, string> EmptyCodeableDict()
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add("code", "");
            dictionary.Add("system", "");
            dictionary.Add("display", "");
            dictionary.Add("text", "");
            return dictionary;
        }

        /// <summary>Given a FHIR path, return the elements that match the given path;
        /// returns an empty array if no matches are found.</summary>
        /// <param name="path">represents a FHIR path.</param>
        /// <returns>all elements that match the given path, or an empty array if no matches are found.</returns>
        public object[] GetAll(string path)
        {
            var matches = Navigator.Select(path);
            ArrayList list = new ArrayList();
            foreach (var match in matches)
            {
                list.Add(match.Value);
            }
            return list.ToArray();
        }

        /// <summary>Given a FHIR path, return the first element that matches the given path.</summary>
        /// <param name="path">represents a FHIR path.</param>
        /// <returns>the first element that matches the given path, or null if no match is found.</returns>
        public object GetFirst(string path)
        {
            var matches = Navigator.Select(path);
            if (matches.Count() > 0)
            {
                return matches.First().Value;
            }
            else
            {
                return null; // Nothing found
            }
        }

        /// <summary>Given a FHIR path, return the last element that matches the given path.</summary>
        /// <param name="path">represents a FHIR path.</param>
        /// <returns>the last element that matches the given path, or null if no match is found.</returns>
        public object GetLast(string path)
        {
            var matches = Navigator.Select(path);
            if (matches.Count() > 0)
            {
                return matches.Last().Value;
            }
            else
            {
                return null; // Nothing found
            }
        }

        /// <summary>Given a FHIR path, return the elements that match the given path as a string;
        /// returns an empty array if no matches are found.</summary>
        /// <param name="path">represents a FHIR path.</param>
        /// <returns>all elements that match the given path as a string, or an empty array if no matches are found.</returns>
        private string[] GetAllString(string path)
        {
            ArrayList list = new ArrayList();
            foreach (var match in GetAll(path))
            {
                list.Add(Convert.ToString(match));
            }
            return list.ToArray(typeof(string)) as string[];
        }

        /// <summary>Given a FHIR path, return the first element that matches the given path as a string;
        /// returns null if no match is found.</summary>
        /// <param name="path">represents a FHIR path.</param>
        /// <returns>the first element that matches the given path as a string, or null if no match is found.</returns>
        private string GetFirstString(string path)
        {
            var first = GetFirst(path);
            if (first != null)
            {
                return Convert.ToString(first);
            }
            else
            {
                return null; // Nothing found
            }
        }

        /// <summary>Given a FHIR path, return the last element that matches the given path as a string;
        /// returns an empty string if no match is found.</summary>
        /// <param name="path">represents a FHIR path.</param>
        /// <returns>the last element that matches the given path as a string, or null if no match is found.</returns>
        private string GetLastString(string path)
        {
            var last = GetLast(path);
            if (last != null)
            {
                return Convert.ToString(last);
            }
            else
            {
                return null; // Nothing found
            }
        }

        /// <summary>Get a value from a Dictionary, but return null if the key doesn't exist or the value is an empty string.</summary>
        private static string GetValue(Dictionary<string, string> dict, string key)
        {
            if (dict != null && dict.ContainsKey(key) && !String.IsNullOrWhiteSpace(dict[key]))
            {
                return dict[key];
            }
            return null;
        }

        // /// <summary>Check to make sure the given profile contains the given resource.</summary>
        // private static bool MatchesProfile(string resource, string profile)
        // {
        //     if (!String.IsNullOrWhiteSpace(profile) && profile.Contains(resource))
        //     {
        //         return true;
        //     }
        //     return false;
        // }

        /// <summary>Combine the given dictionaries and return the combined result.</summary>
        private static Dictionary<string, string> UpdateDictionary(Dictionary<string, string> a, Dictionary<string, string> b)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            foreach (KeyValuePair<string, string> entry in a)
            {
                dictionary[entry.Key] = entry.Value;
            }
            foreach (KeyValuePair<string, string> entry in b)
            {
                dictionary[entry.Key] = entry.Value;
            }
            return dictionary;
        }

        /// <summary>Returns a JSON encoded structure that maps to the various property
        /// annotations found in the DeathRecord class. This is useful for scenarios
        /// where you may want to display the data in user interfaces.</summary>
        /// <returns>a string representation of this Death Record in a descriptive format.</returns>
        public string ToDescription()
        {
            Dictionary<string, Dictionary<string, dynamic>> description = new Dictionary<string, Dictionary<string, dynamic>>();
            // the priority values should order the categories as: Decedent Demographics, Decedent Disposition, Death Investigation, Death Certification
            foreach (PropertyInfo property in typeof(DeathRecord).GetProperties().OrderBy(p => p.GetCustomAttribute<Property>().Priority))
            {
                // Grab property annotation for this property
                Property info = property.GetCustomAttribute<Property>();

                // Skip properties that shouldn't be serialized.
                if (!info.Serialize)
                {
                    continue;
                }

                // Add category if it doesn't yet exist
                if (!description.ContainsKey(info.Category))
                {
                    description.Add(info.Category, new Dictionary<string, dynamic>());
                }

                // Add the new property to the category
                Dictionary<string, dynamic> category = description[info.Category];
                category[property.Name] = new Dictionary<string, dynamic>();

                // Add the attributes of the property
                category[property.Name]["Name"] = info.Name;
                category[property.Name]["Type"] = info.Type.ToString();
                category[property.Name]["Description"] = info.Description;
                category[property.Name]["IGUrl"] = info.IGUrl;
                category[property.Name]["CapturedInIJE"] = info.CapturedInIJE;

                // Add snippets
                FHIRPath path = property.GetCustomAttribute<FHIRPath>();
                var matches = Navigator.Select(path.Path);
                if (matches.Count() > 0)
                {
                    if (info.Type == Property.Types.TupleCOD || info.Type == Property.Types.TupleArr || info.Type == Property.Types.Tuple4Arr)
                    {
                        // Make sure to grab all of the Conditions for COD
                        string xml = "";
                        string json = "";
                        foreach (var match in matches)
                        {
                            xml += match.ToXml();
                            json += match.ToJson() + ",";
                        }
                        category[property.Name]["SnippetXML"] = xml;
                        category[property.Name]["SnippetJSON"] = "[" + json + "]";
                    }
                    else if (!String.IsNullOrWhiteSpace(path.Element))
                    {
                        // Since there is an "Element" for this path, we need to be more
                        // specific about what is included in the snippets.
                        XElement root = XElement.Parse(matches.First().ToXml());
                        XElement node = root.DescendantsAndSelf("{http://hl7.org/fhir}" + path.Element).FirstOrDefault();
                        if (node != null)
                        {
                            node.Name = node.Name.LocalName;
                            category[property.Name]["SnippetXML"] = node.ToString();
                        }
                        else
                        {
                            category[property.Name]["SnippetXML"] = "";
                        }
                        Dictionary<string, dynamic> jsonRoot =
                           JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(matches.First().ToJson(),
                               new JsonSerializerSettings() { DateParseHandling = DateParseHandling.None });
                        if (jsonRoot != null && jsonRoot.Keys.Contains(path.Element))
                        {
                            category[property.Name]["SnippetJSON"] = "{" + $"\"{path.Element}\": \"{jsonRoot[path.Element]}\"" + "}";
                        }
                        else
                        {
                            category[property.Name]["SnippetJSON"] = "";
                        }
                    }
                    else
                    {
                        category[property.Name]["SnippetXML"] = matches.First().ToXml();
                        category[property.Name]["SnippetJSON"] = matches.First().ToJson();
                    }

                }
                else
                {
                    category[property.Name]["SnippetXML"] = "";
                    category[property.Name]["SnippetJSON"] = "";
                }

                // Add the current value of the property
                if (info.Type == Property.Types.Dictionary)
                {
                    // Special case for Dictionary; we want to be able to describe what each key means
                    Dictionary<string, string> value = (Dictionary<string, string>)property.GetValue(this);
                    if (value == null)
                    {
                        continue;
                    }
                    Dictionary<string, Dictionary<string, string>> moreInfo = new Dictionary<string, Dictionary<string, string>>();
                    foreach (PropertyParam propParameter in property.GetCustomAttributes<PropertyParam>())
                    {
                        moreInfo[propParameter.Key] = new Dictionary<string, string>();
                        moreInfo[propParameter.Key]["Description"] = propParameter.Description;
                        if (value.ContainsKey(propParameter.Key))
                        {
                            moreInfo[propParameter.Key]["Value"] = value[propParameter.Key];
                        }
                        else
                        {
                            moreInfo[propParameter.Key]["Value"] = null;
                        }
                    }
                    category[property.Name]["Value"] = moreInfo;
                }
                else
                {
                    category[property.Name]["Value"] = property.GetValue(this);
                }
            }
            return JsonConvert.SerializeObject(description);
        }

        /// <summary>Helper method to return a JSON string representation of this Death Record.</summary>
        /// <param name="contents">string that represents </param>
        /// <returns>a new DeathRecord that corresponds to the given descriptive format</returns>
        public static DeathRecord FromDescription(string contents)
        {
            DeathRecord record = new DeathRecord();
            Dictionary<string, Dictionary<string, dynamic>> description =
                JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, dynamic>>>(contents,
                    new JsonSerializerSettings() { DateParseHandling = DateParseHandling.None });
            // Loop over each category
            foreach (KeyValuePair<string, Dictionary<string, dynamic>> category in description)
            {
                // Loop over each property
                foreach (KeyValuePair<string, dynamic> property in category.Value)
                {
                    if (!property.Value.ContainsKey("Value") || property.Value["Value"] == null)
                    {
                        continue;
                    }
                    // Set the property on the new DeathRecord based on its type
                    string propertyName = property.Key;
                    Object value = null;
                    if (property.Value["Type"] == Property.Types.String || property.Value["Type"] == Property.Types.StringDateTime)
                    {
                        value = property.Value["Value"].ToString();
                        if (String.IsNullOrWhiteSpace((string)value))
                        {
                            value = null;
                        }
                    }
                    else if (property.Value["Type"] == Property.Types.StringArr)
                    {
                        value = property.Value["Value"].ToObject<String[]>();
                    }
                    else if (property.Value["Type"] == Property.Types.Bool)
                    {
                        value = property.Value["Value"].ToObject<bool>();
                    }
                    else if (property.Value["Type"] == Property.Types.TupleArr)
                    {
                        value = property.Value["Value"].ToObject<Tuple<string, string>[]>();
                    }
                    else if (property.Value["Type"] == Property.Types.TupleCOD)
                    {
                        value = property.Value["Value"].ToObject<Tuple<string, string /*, Dictionary<string, string>*/>[]>();
                    }
                    else if (property.Value["Type"] == Property.Types.Dictionary)
                    {
                        Dictionary<string, Dictionary<string, string>> moreInfo =
                            property.Value["Value"].ToObject<Dictionary<string, Dictionary<string, string>>>();
                        Dictionary<string, string> result = new Dictionary<string, string>();
                        foreach (KeyValuePair<string, Dictionary<string, string>> entry in moreInfo)
                        {
                            result[entry.Key] = entry.Value["Value"];
                        }
                        value = result;
                    }
                    if (value != null)
                    {
                        typeof(DeathRecord).GetProperty(propertyName).SetValue(record, value);
                    }
                }
            }
            return record;
        }

        /// <summary>Helper method to create a HumanName from a list of strings.</summary>
        /// <param name="value">A list of strings to be converted into a name.</param>
        /// <param name="names">The current list of HumanName attributes for the person.</param>
        public static void updateGivenHumanName(string[] value, List<HumanName> names)
        {
            // Remove any blank or null values.
            value = value.Where(v => !String.IsNullOrEmpty(v)).ToArray();
            // Set names only if there are non-blank values.
            if (value.Length < 1)
            {
              return;
            }
            HumanName name = names.SingleOrDefault(n => n.Use == HumanName.NameUse.Official);
            if (name != null)
            {
                name.Given = value;
            }
            else
            {
                name = new HumanName();
                name.Use = HumanName.NameUse.Official;
                name.Given = value;
                names.Add(name);
            }
        }

        /// <summary>Helper method to validate that all PartialDate and PartialDateTime exensions are valid and have the valid required sub-extensions.</summary>
        /// <param name="bundle">The bundle in which to validate the PartialDate/Time extensions.</param>
        private static void ValidatePartialDates(Bundle bundle)
        {
            System.Text.StringBuilder errors = new System.Text.StringBuilder();
            List<Resource> resources = bundle.Entry.Select(entry => entry.Resource).ToList();

            foreach (Hl7.Fhir.Model.Resource resource in resources)
            {
                foreach (DataType child in resource.Children.Where(child => child.GetType().IsSubclassOf(typeof(DataType))))
                {
                    // Extract PartialDates and PartialDateTimes.
                    List<Extension> partialDateExtensions = child.Extension.Where(ext => ext.Url.Equals(ExtensionURL.PartialDate) || ext.Url.Equals(ExtensionURL.PartialDateTime)).ToList();
                    foreach (Extension partialDateExtension in partialDateExtensions)
                    {
                        // Validate that the required sub-extensions are in the PartialDate/Time component.
                        List<String> partialDateSubExtensions = partialDateExtension.Extension.Select(ext => ext.Url).ToList();
                        if (!partialDateSubExtensions.Contains(ExtensionURL.DateDay))
                        {
                            errors.Append("Missing 'Date-Day' of [" + partialDateExtension.Url + "] for resource [" + resource.Id + "].").AppendLine();
                        }
                        if (!partialDateSubExtensions.Contains(ExtensionURL.DateMonth))
                        {
                            errors.Append("Missing 'Date-Month' of [" + partialDateExtension.Url + "] for resource [" + resource.Id + "].").AppendLine();
                        }
                        if (!partialDateSubExtensions.Contains(ExtensionURL.DateYear))
                        {
                            errors.Append("Missing 'Date-Year' of [" + partialDateExtension.Url + "] for resource [" + resource.Id + "].").AppendLine();
                        }
                        if (partialDateExtension.Url.Equals(ExtensionURL.PartialDateTime) && !partialDateSubExtensions.Contains(ExtensionURL.DateTime))
                        {
                            errors.Append("Missing 'Date-Time' of [" + partialDateExtension.Url + "] for resource [" + resource.Id + "].").AppendLine();
                        }
                        // Validate that there are no extraneous invalid sub-extensions of the PartialDate/Time component.
                        partialDateSubExtensions.Remove(ExtensionURL.DateDay);
                        partialDateSubExtensions.Remove(ExtensionURL.DateMonth);
                        partialDateSubExtensions.Remove(ExtensionURL.DateYear);
                        partialDateSubExtensions.Remove(ExtensionURL.DateTime);
                        if (partialDateSubExtensions.Count() > 0) {
                            errors.Append("[" + partialDateExtension.Url + "] component contains extra invalid fields [" + string.Join(", ", partialDateSubExtensions) + "] for resource [" + resource.Id + "].").AppendLine();
                        }
                    }
                }
            }
            if (errors.Length > 0)
            {
                throw new ArgumentException(errors.ToString());
            }
        }
    }

    /// <summary>Property attribute used to describe a DeathRecord property.</summary>
    [System.AttributeUsage(System.AttributeTargets.Property)]
    public class Property : System.Attribute
    {
        /// <summary>Enum for describing the property type.</summary>
        public enum Types
        {
            /// <summary>Parameter is a string.</summary>
            String,
            /// <summary>Parameter is an array of strings.</summary>
            StringArr,
            /// <summary>Parameter is like a string, but should be treated as a date and time.</summary>
            StringDateTime,
            /// <summary>Parameter is a bool.</summary>
            Bool,
            /// <summary>Parameter is a Dictionary.</summary>
            Dictionary,
            /// <summary>Parameter is an array of Tuples.</summary>
            TupleArr,
            /// <summary>Parameter is an array of Tuples, specifically for CausesOfDeath.</summary>
            TupleCOD,
            /// <summary>Parameter is an unsigned integer.</summary>
            Int32,
            /// <summary>Parameter is an array of 4-Tuples, specifically for entity axis codes.</summary>
            Tuple4Arr
        };

        /// <summary>Name of this property.</summary>
        public string Name;

        /// <summary>The property type (e.g. string, bool, Dictionary).</summary>
        public Types Type;

        /// <summary>Category of this property.</summary>
        public string Category;

        /// <summary>Description of this property.</summary>
        public string Description;

        /// <summary>If this field should be kept when serialzing.</summary>
        public bool Serialize;

        /// <summary>URL that links to the IG description for this property.</summary>
        public string IGUrl;

        /// <summary>If this field has an equivalent in IJE.</summary>
        public bool CapturedInIJE;

        /// <summary>Priority that this should show up in generated lists. Lower numbers come first.</summary>
        public int Priority;

        /// <summary>Constructor.</summary>
        public Property(string name, Types type, string category, string description, bool serialize, string igurl, bool capturedInIJE, int priority = 4)
        {
            this.Name = name;
            this.Type = type;
            this.Category = category;
            this.Description = description;
            this.Serialize = serialize;
            this.IGUrl = igurl;
            this.CapturedInIJE = capturedInIJE;
            this.Priority = priority;
        }
    }

    /// <summary>Property attribute used to describe a DeathRecord property parameter,
    /// specifically if the property is a dictionary that has keys.</summary>
    [System.AttributeUsage(System.AttributeTargets.Property, AllowMultiple = true)]
    public class PropertyParam : System.Attribute
    {
        /// <summary>If the related property is a Dictionary, the key name.</summary>
        public string Key;

        /// <summary>Description of this parameter.</summary>
        public string Description;

        /// <summary>Constructor.</summary>
        public PropertyParam(string key, string description)
        {
            this.Key = key;
            this.Description = description;
        }
    }

    /// <summary>Describes a FHIR path that can be used to get to the element.</summary>
    [System.AttributeUsage(System.AttributeTargets.Property)]
    public class FHIRPath : System.Attribute
    {
        /// <summary>The relevant FHIR path.</summary>
        public string Path;

        /// <summary>The relevant element.</summary>
        public string Element;

        /// <summary>Constructor.</summary>
        public FHIRPath(string path, string element)
        {
            this.Path = path;
            this.Element = element;
        }
    }
}
