using System.Collections.Generic;
using System.Linq;
using Hl7.Fhir.Model;

namespace FhirDeathRecord.Extensions
{
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
            target = null;
            // Address address = new Address();
            // if (dict != null)
            // {
            //     List<string> lines = new List<string>();
            //     if (dict.ContainsKey("addressLine1") && !String.IsNullOrEmpty(dict["addressLine1"]))
            //     {
            //         lines.Add(dict["addressLine1"]);
            //     }
            //     if (dict.ContainsKey("addressLine2") && !String.IsNullOrEmpty(dict["addressLine2"]))
            //     {
            //         lines.Add(dict["addressLine2"]);
            //     }
            //     if (lines.Count() > 0)
            //     {
            //         address.Line = lines.ToArray();
            //     }
            //     if (dict.ContainsKey("addressCity") && !String.IsNullOrEmpty(dict["addressCity"]))
            //     {
            //         address.City = dict["addressCity"];
            //     }
            //     if (dict.ContainsKey("addressCounty") && !String.IsNullOrEmpty(dict["addressCounty"]))
            //     {
            //         address.District = dict["addressCounty"];
            //     }
            //     if (dict.ContainsKey("addressState") && !String.IsNullOrEmpty(dict["addressState"]))
            //     {
            //         address.State = dict["addressState"];
            //     }
            //     if (dict.ContainsKey("addressZip") && !String.IsNullOrEmpty(dict["addressZip"]))
            //     {
            //         address.PostalCode = dict["addressZip"];
            //     }
            //     if (dict.ContainsKey("addressCountry") && !String.IsNullOrEmpty(dict["addressCountry"]))
            //     {
            //         address.Country = dict["addressCountry"];
            //     }
            // }
            // return address;
        }

    }
}