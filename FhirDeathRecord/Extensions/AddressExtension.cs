using System.Collections.Generic;
using System.Linq;
using Hl7.Fhir.Model;

namespace FhirDeathRecord.Extensions
{
    /// <summary>
    /// Extension methos for HL7.Fhir.Model.Address
    /// </summary>
    public static class AddressExtension
    {
        /// <summary>Convert a FHIR Address to an "address" Dictionary.</summary>
        /// <param name="source">a FHIR Address.</param>
        /// <returns>the corresponding Dictionary representation of the FHIR Address.</returns>
        public static Dictionary<string, string> ToDictionary(this Address source)
        {
            var dictionary = new Dictionary<string, string>
            {
                { "addressLine1", string.Empty },
                { "addressLine2", string.Empty },
                { "addressCity", string.Empty },
                { "addressCounty", string.Empty },
                { "addressState", string.Empty },
                { "addressZip", string.Empty },
                { "addressCountry", string.Empty }
            };

            if (source == null)
                return dictionary;

            if (source.Line != null && source.Line.Any())
                dictionary["addressLine1"] = source.Line.First();

            if (source.Line != null && source.Line.Count() > 1)
                dictionary["addressLine2"] = source.Line.Last();

            dictionary["addressCity"] = source.City;
            dictionary["addressCounty"] = source.District;
            dictionary["addressState"] = source.State;
            dictionary["addressZip"] = source.PostalCode;
            dictionary["addressCountry"] = source.Country;

            return dictionary;
        }

        /// <summary>Convert an "address" dictionary to a FHIR Address.</summary>
        /// <param name="source">represents an address.</param>
        /// <param name="target">the corresponding FHIR Address representation of the address.</param>
        public static void FromDictionary(this Address target, Dictionary<string, string> source)
        {
            if (source == null)
                return;

            if (source.HasStringValue("addressLine1"))
                target.LineElement.Add(new FhirString(source["addressLine1"]));

            if (source.HasStringValue("addressLine2"))
                target.LineElement.Add(new FhirString(source["addressLine2"]));

            if (source.HasStringValue("addressCity"))
                target.City = source["addressCity"];

            if (source.HasStringValue("addressCounty"))
                target.District = source["addressCounty"];

            if (source.HasStringValue("addressState"))
                target.State = source["addressState"];

            if (source.HasStringValue("addressZip"))
                target.PostalCode = source["addressZip"];

            if (source.HasStringValue("addressCountry"))
                target.Country = source["addressCountry"];
        }
    }
}