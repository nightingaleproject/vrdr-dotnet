using System.Collections.Generic;
using System.Linq;
using Hl7.Fhir.Model;

namespace FhirDeathRecord.Extensions
{
    /// <summary>
    /// Extension methos for HL7.Fhir.Model.CodeableConcept
    /// </summary>
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
        /// <param name="target">the corresponding CodeableConcept representation of the code</param>
        /// <param name="source">represents a code.</param>
        public static void FromDictionary(this CodeableConcept target, Dictionary<string, string> source)
        {
            if (source == null)
                return;

            var coding = new Coding();

            if (source.HasStringValue("code"))
                coding.Code = source["code"];

            if (source.HasStringValue("system"))
                coding.System = source["system"];

            if (source.HasStringValue("display"))
                coding.Display = source["display"];

            target.Coding.Add(coding);
        }
    }
}