using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;

namespace VRDR
{
    /// <summary>Class <c>BaseMessage</c> is the base class of all messages.</summary>
    public partial class BaseMessage
    {
        private void SetParameterCoding(Dictionary<string, string> dict, string group, string field)
        {
            if (group == null)
            { // not part of a group
                Record.Remove(field);
                Coding c = DictToCoding(dict);
                Record.Add(field, c);
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
                        value = (Base)DictToCoding(dict);
                    }
                    var part = Tuple.Create(item.Name, (Base)value);
                    list.Add(part);
                }
            }
            // If list doesn't include field, add it with the value
            var element = list.Find(elem => elem.Item1 == field);
            if (element == null)
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
            Record.Add(group, list);
        }
        private void SetParameterCoding(Dictionary<string, string> dict, string field)
        {
            SetParameterCoding(dict, null, field);
        }
        private Dictionary<string, string> GetParameterCoding(string group, string field)
        {
            if (group != null)
            {
                var g = Record.GetSingle(group);
                if (g == null){
                    return null;
                }
                var element = g.Part.Find(elem => elem.Name == field);
                if (element != null)
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
        private Dictionary<string, string> GetParameterCoding(string field)
        {
            Parameters.ParameterComponent f = Record.GetSingle(field);
            if( f == null){
                return null;
            } else {
                return CodingToDict((Coding)(f.Value));
            }
        }

        // =================
        private void SetParameterUnsignedInt(uint u, string group, string field)
        {
            if (group == null)
            { // not part of a group
                Record.Remove(field);
                Record.Add(field, new UnsignedInt((int)u));
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
                        value = new UnsignedInt((int)u);
                    }
                    var part = Tuple.Create(item.Name, (Base)value);
                    list.Add(part);
                }
            }
            // If list doesn't include field, add it with the value
            var element = list.Find(elem => elem.Item1 == field);
            if (element == null)
            {
                var part = Tuple.Create(field, (Base)new UnsignedInt((int)u));
                list.Add(part);
            }
            // If the group exists, remove it.
            if (g != null)
            {
                Record.Remove(group);
            }
            // Insert the list as the new group
            Record.Add(group, list);
        }
        private void SetParameterUnsignedInt(uint u, string field)
        {
            SetParameterUnsignedInt(u, null, field);
        }
        private uint? GetParameterUnsignedInt(string group, string field)
        {
            if (group != null)
            {
                var g = Record.GetSingle(group);
                var element = g.Part.Find(elem => elem.Name == field);
                if (element != null)
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

        private void SetParameterString(string s, string group, string field)
        {
            if (group == null)
            { // not part of a group
                Record.Remove(field);
                Record.Add(field, new FhirString(s));
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
                    var value = item.Value;
                    if (item.Name == field)
                    {
                        value = new FhirString(s);
                    }
                    var part = Tuple.Create(item.Name, (Base)value);
                    list.Add(part);
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
            Record.Add(group, list);
        }
        private void SetParameterString(string s, string field)
        {
            SetParameterString(s, null, field);
        }
        private string GetParameterString(string group, string field)
        {
            if (group != null)
            {
                var g = Record.GetSingle(group);
                if (g == null){ // group doesn't exist
                    return null;
                }
                var element = g.Part.Find(elem => elem.Name == field);
                if (element != null)
                {
                    return ((FhirString)element.Value).Value;
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
        private string GetParameterString(string field)
        {
            return (Record.GetSingleValue<FhirString>(field)).Value;
        }
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
