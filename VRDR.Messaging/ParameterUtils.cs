using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;

// This file contains the utility functions for setting/getting scalar parameter values.
// Accessors for non-scalar parameter values (record an entity cause of death) do not use the methods.
// These functions are called by the machine-generated code in Parameters.cs which is generated directly from the paramter profiles in
// the vital records messaging IG.
// There are three sets of methods for each type of parameter:   Coding, UnsignedInt and string.  The methods follow the same template, but
// there are minor variations to support null/empty values and type conversions.

namespace VRDR
{
    /// <summary>Class <c>BaseMessage</c> is the base class of all messages.</summary>
    public partial class BaseMessage
    {
        // =================Coding=======================
        /// <summary>SetParameterCoding:   set a coding parameter associated with a top level parameter</summary>
        private void SetParameterCoding(Dictionary<string, string> dict, string field)
        {
            Record.Remove(field);
            if (!IsDictEmptyOrDefault(dict))
            {
                Coding c = DictToCoding(dict);
                Record.Add(field, c);
            }
        }

        /// <summary>GetParameterCoding:   set a coding parameter associated with a top level parameter </summary>
        private Dictionary<string, string> GetParameterCoding(string field)
        {
            Parameters.ParameterComponent f = Record.GetSingle(field);
            if (f == null)
            {
                return null;
            }
            else
            {
                return CodingToDict((Coding)(f.Value));
            }
        }

        // =================UnsignedInt=======================
        /// <summary>SetParameterUnsignedInt:   Set an UnsignedInt parameter associated with a top level parameter </summary>
        private void SetParameterUnsignedInt(uint? u, string field)
        {
            Record.Remove(field);
            if (u != null)
            {
                Record.Add(field, new UnsignedInt((int)u));
            }
        }

        /// <summary>GetParameterUnsignedInt:   Get an UnsignedInt parameter associated with a top level parameter </summary>
        private uint? GetParameterUnsignedInt(string field)
        {
            Parameters.ParameterComponent f = Record.GetSingle(field);
            if (f == null)
            {
                return null;
            }
            else
            {
                UnsignedInt uI = (UnsignedInt)f.Value;
                uint u = (uint)uI.Value;
                return u;
            }
        }

        // =================String=======================
        /// <summary>SetParameterString:   Get a String parameter associated with a top level parameter </summary>
        private void SetParameterString(string s, string field)
        {
            Record.Remove(field);
            // Fields with empty strings should be not be included
            if (!String.IsNullOrEmpty(s))
            {
                Record.Add(field, new FhirString(s));
            }
            return;
        }

        /// <summary>GetParameterString:   Get a String parameter associated with a top level parameter </summary>
        private string GetParameterString(string field)
        {
            Parameters.ParameterComponent f = Record.GetSingle(field);
            if (f == null)
            {
                return null;
            }
            else
            {
                FhirString fs = (FhirString)f.Value;
                string s = (string)fs.Value;
                return s;
            }
        }
        // These should be simply referenced from VRDR/Program.cs....Not sure why they aren't visible from there.
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
            }
            return coding;
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
        /// <summary>Check if a dictionary is empty or a default empty dictionary (all values are null or empty strings)</summary>
        /// <param name="dict">represents a code.</param>
        /// <returns>A boolean identifying whether the provided dictionary is empty or default.</returns>
        private bool IsDictEmptyOrDefault(Dictionary<string, string> dict)
        {
            return dict.Count == 0 || dict.Values.All(v => v == null || v == "");
        }

    }
}
