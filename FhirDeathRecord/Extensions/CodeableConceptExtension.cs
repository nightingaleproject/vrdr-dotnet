using System.Collections.Generic;
using System.Linq;
using Hl7.Fhir.Model;

namespace FhirDeathRecord.Extensions
{
    public static class CodeableConceptExtension
    {
        /// <summary>Convert a FHIR CodableConcept to a "code" Dictionary</summary>
        /// <param name="codeableConcept">a FHIR CodeableConcept.</param>
        /// <returns>the corresponding Dictionary representation of the code.</returns>
        public static Dictionary<string, string> ToDictionary(this CodeableConcept codeableConcept)
        {

            var dictionary = EmptyCodeDict();

            if (codeableConcept?.Coding == null || !codeableConcept.Coding.Any())
                return dictionary;

            Coding coding = codeableConcept.Coding.First();

            if (!string.IsNullOrEmpty(coding.Code))
            {
                dictionary["code"] = coding.Code;
            }

            if (!string.IsNullOrEmpty(coding.System))
            {
                dictionary["system"] = coding.System;
            }

            if (!string.IsNullOrEmpty(coding.Display))
            {
                dictionary["display"] = coding.Display;
            }

            return dictionary;
        }

        /// <summary>Convert a "code" dictionary to a FHIR CodableConcept.</summary>
        /// <param name="dict">represents a code.</param>
        /// <returns>the corresponding CodeableConcept representation of the code.</returns>
        public static void FromDictionary(this CodeableConcept codeableConcept, Dictionary<string, string> dict)
        {
            if (dict == null)
                return;

            var coding = new Coding();
            if (dict != null)
            {
                if (dict.ContainsKey("code") && !string.IsNullOrEmpty(dict["code"]))
                {
                    coding.Code = dict["code"];
                }
                if (dict.ContainsKey("system") && !string.IsNullOrEmpty(dict["system"]))
                {
                    coding.System = dict["system"];
                }
                if (dict.ContainsKey("display") && !string.IsNullOrEmpty(dict["display"]))
                {
                    coding.Display = dict["display"];
                }
            }
            codeableConcept.Coding.Add(coding);
        }


        /// <summary>Returns an empty "code" Dictionary.</summary>
        /// <returns>an empty "code" Dictionary.</returns>
        private static Dictionary<string, string> EmptyCodeDict()
        {
            var dictionary = new Dictionary<string, string>();
            dictionary.Add("code", "");
            dictionary.Add("system", "");
            dictionary.Add("display", "");
            return dictionary;
        }

    }
}