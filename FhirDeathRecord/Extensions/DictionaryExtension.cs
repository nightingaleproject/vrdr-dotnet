using System.Collections.Generic;

namespace FhirDeathRecord.Extensions
{
    /// <summary>
    /// Extension methos for System.Collections.Generic.Dictionary
    /// </summary>
    public static class DictioanryExtension
    {
        /// <summary>
        /// </summary>
        public static bool HasStringValue(this Dictionary<string, string> source, string key)
        {
            if (source != null && source.ContainsKey(key) && !string.IsNullOrEmpty(source[key]))
                return true;

            return false;
        }
    }
}