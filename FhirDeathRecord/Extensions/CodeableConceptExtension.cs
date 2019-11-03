using System.Collections.Generic;
using System.Linq;
using Hl7.Fhir.Model;

namespace FhirDeathRecord.Extensions
{
    public static class CodeableConceptExtension
    {
        /// <summary>Convert a FHIR CodableConcept to a "code" Dictionary</summary>
        /// <param name="source">a FHIR CodeableConcept.</param>
        /// <returns>the corresponding Dictionary representation of the code.</returns>
        public static Dictionary<string, string> ToDictionary(this CodeableConcept source)
        {

            var dictionary = new Dictionary<string, string>
            {
                { "code", string.Empty },
                { "system", string.Empty },
                { "display", string.Empty }
            };

            if (source?.Coding == null || !source.Coding.Any())
                return dictionary;

            Coding coding = source.Coding.First();

            if (!string.IsNullOrEmpty(coding.Code))
                dictionary["code"] = coding.Code;

            if (!string.IsNullOrEmpty(coding.System))
                dictionary["system"] = coding.System;

            if (!string.IsNullOrEmpty(coding.Display))
                dictionary["display"] = coding.Display;

            return dictionary;
        }

        /// <summary>Convert a "code" dictionary to a FHIR CodableConcept.</summary>
        /// <param name="target">the target codeable concept</param>
        /// <param name="source">represents a code.</param>
        /// <returns>the corresponding CodeableConcept representation of the code.</returns>
        public static void FromDictionary(this CodeableConcept target, Dictionary<string, string> source)
        {
            if (source == null)
                return;

            var coding = new Coding();
            if (source != null)
            {
                if (source.ContainsKey("code") && !string.IsNullOrEmpty(source["code"]))
                {
                    coding.Code = source["code"];
                }
                if (source.ContainsKey("system") && !string.IsNullOrEmpty(source["system"]))
                {
                    coding.System = source["system"];
                }
                if (source.ContainsKey("display") && !string.IsNullOrEmpty(source["display"]))
                {
                    coding.Display = source["display"];
                }
            }
            target.Coding.Add(coding);
        }
    }
}