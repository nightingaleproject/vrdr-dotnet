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
        /// <summary>SetParameterCoding:   set a coding parameter associated with a group (top level) and field (part) parameter </summary>
        private void SetParameterCoding(Dictionary<string, string> dict, string group, string field)
        {
            if (group == null)
            { // not part of a group
                Record.Remove(field);
                if (dict != null){
                    Coding c = DictToCoding(dict);
                    Record.Add(field, c);
                }
                return;
            }
            // Get the group
            var g = Record.GetSingle(group);
            var list = new List<Tuple<string, Base>>();
            if (g != null)
            {

                // if the group exists, convert to list.  If list includes field, set the value
                foreach (Parameters.ParameterComponent item in g.Part)
                {
                    var value = (Base)item.Value;
                    if (item.Name == field)
                    {
                        if(dict != null){ // replace item if dict is not null, otherwise, don't add it to list
                            list.Add(Tuple.Create(item.Name, (Base)DictToCoding(dict)));
                        }
                    }else {
                        list.Add(Tuple.Create(item.Name, (Base)item.Value));
                    }
                }
            }
            // If list doesn't include field, and value is not null, add it with the value
            var element = list.Find(elem => elem.Item1 == field);
            if (element == null && dict != null)
            {
                var part = Tuple.Create(field, (Base)DictToCoding(dict));
                list.Add(part);
            }
            // If the group exists, remove it.
            if (g != null)
            {
                Record.Remove(group);
            }
            // Insert the list as the new group
            if(list.Count > 0){
                Record.Add(group, list);
            }
        }
        /// <summary>SetParameterCoding:   set a coding parameter associated with a top level parameter (no group)</summary>
        private void SetParameterCoding(Dictionary<string, string> dict, string field)
        {
            SetParameterCoding(dict, null, field);
        }
       /// <summary>GetParameterCoding:   get a coding parameter associated with a group (top level) and field (part) parameter </summary>
        private Dictionary<string, string> GetParameterCoding(string group, string field)
        {
            if (group != null)
            {
                var g = Record.GetSingle(group);
                if (g == null){
                    return null;
                }
                var element = g.Part.Find(elem => elem.Name == field);
                if (element != null && element.Value != null)
                {
                    return CodingToDict((Coding)(element.Value));
                }
                else
                {
                    return null; // field in group doesn't exist
                }
            }
            else
            {  // group doesn't exist
                return null;
            }
        }
       /// <summary>GetParameterCoding:   set a coding parameter associated with a top level parameter </summary>
        private Dictionary<string, string> GetParameterCoding(string field)
        {
            Parameters.ParameterComponent f = Record.GetSingle(field);
            if( f == null){
                return null;
            } else {
                return CodingToDict((Coding)(f.Value));
            }
        }

        // =================UnsignedInt=======================
       /// <summary>SetParameterUnsignedInt:   Set an UnsignedInt parameter associated with a group (top level) and field (part) parameter </summary>
        private void SetParameterUnsignedInt(uint? u, string group, string field)
        {
            if (group == null)
            { // not part of a group
                Record.Remove(field);
                if(u != null){
                    Record.Add(field, new UnsignedInt((int)u));
                }
                return;
            }
            // Get the group
            var g = Record.GetSingle(group);
            var list = new List<Tuple<string, Base>>();
            if (g != null)
            {

                // if the group exists, convert to list.  If list includes field, set the value
                foreach (Parameters.ParameterComponent item in g.Part)
                {
                    if (item.Name == field)
                    {
                        if(u != null){ // replace item if u is not null, otherwise, don't add it to list
                            list.Add(Tuple.Create(item.Name, (Base)new UnsignedInt((int)u)));
                        }
                    }else {
                        list.Add(Tuple.Create(item.Name, (Base)item.Value));
                    }
                }
            }
            // If list doesn't include field, add it with the value
            var element = list.Find(elem => elem.Item1 == field);
            if (element == null)
            {
                if(u != null){
                    list.Add(Tuple.Create(field, (Base)new UnsignedInt((int)u)));
                }
            }
            // If the group exists, remove it.
            if (g != null)
            {
                Record.Remove(group);
            }
            // Insert the list as the new group
            if(list.Count > 0){
                Record.Add(group, list);
            }
        }
        /// <summary>SetParameterUnsignedInt:   Set an UnsignedInt parameter associated with a top level parameter </summary>
        private void SetParameterUnsignedInt(uint? u, string field)
        {
            SetParameterUnsignedInt(u, null, field);
        }
       /// <summary>GetParameterUnsignedInt:   Get an UnsignedInt parameter associated with a group (top level) and field (part) parameter </summary>
        private uint? GetParameterUnsignedInt(string group, string field)
        {
            if (group != null)
            {
                var g = Record.GetSingle(group);
                if(g == null){
                    return null;
                }
                var element = g.Part.Find(elem => elem.Name == field);
                if (element != null && element.Value != null)
                {
                    UnsignedInt uI = (UnsignedInt)element.Value;
                    uint u = (uint)uI.Value;
                    return u;
                }
                else
                {
                    return null; // field in group doesn't exist
                }
            }
            else
            {  // group doesn't exist
                return null;
            }
        }
       /// <summary>GetParameterUnsignedInt:   Get an UnsignedInt parameter associated with a top level parameter </summary>
        private uint? GetParameterUnsignedInt(string field)
        {
            Parameters.ParameterComponent f = Record.GetSingle(field);
            if (f == null){
                return null;
            } else {
                UnsignedInt uI = (UnsignedInt)f.Value;
                uint u = (uint)uI.Value;
                return u;
            }
        }

        // =================String=======================
        /// <summary>SetParameterString:   Get a String parameter associated with a group (top level) and field (part) parameter </summary>
       private void SetParameterString(string s, string group, string field)
        {
            if (group == null)
            { // not part of a group
            }
            // Get the group
            var g = Record.GetSingle(group);
            var list = new List<Tuple<string, Base>>();
            if (g != null)
            {

                // if the group exists, convert to list.  If list includes field, set the value
                foreach (Parameters.ParameterComponent item in g.Part)
                {
                    var value = item.Value;
                    if (item.Name == field)
                    {
                        if(s != null && s.Length > 0){ // null values or empty strings should not be added
                            list.Add(Tuple.Create(item.Name,(Base) new FhirString(s)));
                        }
                    }else{
                            list.Add(Tuple.Create(item.Name,(Base) value));
                    }
                }
            }
            // If list doesn't include field, add it with the value
            var element = list.Find(elem => elem.Item1 == field);
            if (element == null)
            {
                var part = Tuple.Create(field, (Base)new FhirString(s));
                list.Add(part);
            }
            // If the group exists, remove it.
            if (g != null)
            {
                Record.Remove(group);
            }
            // Insert the list as the new group
            if(list.Count > 0){
                Record.Add(group, list);
            }
        }
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
        /// <summary>GetParameterString:   Get a String parameter associated with a group (top level) and field (part) parameter </summary>

        /// <summary>GetParameterString:   Get a String parameter associated with a top level parameter </summary>
        private string GetParameterString(string field)
        {
            Parameters.ParameterComponent f = Record.GetSingle(field);
            if (f == null){
                return null;
            } else {
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
