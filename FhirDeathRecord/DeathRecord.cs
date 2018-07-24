using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.IO;
using System.Collections;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.FhirPath;

namespace FhirDeathRecord
{
    /// <summary>Class <c>DeathRecord</c> models a Standard Death Record (SDR).</summary>
    public class DeathRecord
    {
        private PocoNavigator Navigator;

        private Bundle Bundle;

        /// <summary>Default constructor that creates a new, empty FHIR SDR.</summary>
        public DeathRecord()
        {
            Bundle = new Bundle();
        }

        /// <summary>Constructor that takes a string that represents a FHIR SDR in either XML or JSON format.</summary>
        /// <param name="record">a string that represents a FHIR SDR in either XML or JSON format.</param>
        /// <exception cref="ArgumentException">Record is neither valid XML nor JSON.</exception>
        public DeathRecord(string record)
        {
            // Try JSON
            try
            {
                FhirJsonParser parser = new FhirJsonParser();
                Bundle = parser.Parse<Bundle>(record);
                Navigator = new PocoNavigator(Bundle);
            }
            catch {}

            // Try XML
            try
            {
                FhirXmlParser parser = new FhirXmlParser();
                Bundle = parser.Parse<Bundle>(record);
                Navigator = new PocoNavigator(Bundle);
            }
            catch {}

            // If the given record string was not JSON or XML, fail immediately.
            if (Navigator == null)
            {
                throw new System.ArgumentException("Record is neither valid XML nor JSON.");
            }
        }

        /// <summary>Decedent's Given Name(s)</summary>
        public string GivenName
        {
            get
            {
                return GetFirst("Bundle.entry.resource.where($this is Patient).name[0].given");
            }
            set
            {
                // TODO
            }
        }

        /// <summary>Decedent's Last Name</summary>
        public string LastName
        {
            get
            {
                return GetFirst("Bundle.entry.resource.where($this is Patient).name[0].family");
            }
            set
            {
                // TODO
            }
        }

        /// <summary>Decedent's Social Security Number</summary>
        public string SSN
        {
            get
            {
                return GetFirst("Bundle.entry.resource.where($this is Patient).identifier.where(system = 'http://hl7.org/fhir/sid/us-ssn').value");
            }
            set
            {
                // TODO
            }
        }

        /// <summary>Given name(s) of certifier.</summary>
        public string CertifierGivenName
        {
            get
            {
                return GetFirst("Bundle.entry.resource.where($this is Practitioner).name[0].given");
            }
            set
            {
                // TODO
            }
        }

        /// <summary>Last name of certifier.</summary>
        public string CertifierLastName
        {
            get
            {
                return GetFirst("Bundle.entry.resource.where($this is Practitioner).name[0].family");
            }
            set
            {
                // TODO
            }
        }

        /// <summary>Conditions that resulted in the underlying cause of death. Corresponds to part 1 of item 32 of the U.S.
        /// Standard Certificate of Death.</summary>
        public Tuple<string, string>[] CausesOfDeath
        {
            /// <returns>an array of tuples each containing the cause of death literal and the approximate interval onset to death.</returns>
            get
            {
                string[] causes = GetAll($"Bundle.entry.resource.where($this is Condition).where(onset).text.div");
                string[] intervals = GetAll($"Bundle.entry.resource.where($this is Condition).where(onset).onset");
                Regex htmlRegex = new Regex("<.*?>");
                return causes.Zip(intervals, (a, b) => Tuple.Create(htmlRegex.Replace(a, ""), b)).ToArray();
            }
            set
            {
                // TODO
            }
        }

        /// <summary>A significant condition that contributed to death but did not result in the underlying cause
        /// captured by a CauseOfDeathCondition. Corresponds to part 2 of item 32 of the U.S. Standard Certificate of Death.</summary>
        public string ContributingConditions
        {
            get
            {
                string cc = GetFirst("Bundle.entry.resource.where($this is Condition).where(onset.empty()).text.div");
                if (cc != null)
                {
                    Regex htmlRegex = new Regex("<.*?>");
                    return htmlRegex.Replace(cc, "");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                // TODO
            }
        }

        /// <summary>Whether an autopsy was performed (true) or not (false). Corresponds to item 33 of the U.S. Standard
        /// Certificate of Death.</summary>
        public bool AutopsyPerformed
        {
            get
            {
                return GetFirst("Bundle.entry.resource.where($this is Observation).where(code.coding.code='85699-7').value") == "True";
            }
            set
            {
                // TODO
            }
        }

        /// <summary>Were autopsy findings available to complete the cause of death? Corresponds to item 34 of the U.S.
        /// Standard Certificate of Death.</summary>
        public bool AutopsyResultsAvailable
        {
            get
            {
                return GetFirst("Bundle.entry.resource.where($this is Observation).where(code.coding.code='69436-4').value") == "True";
            }
            set
            {
                // TODO
            }
        }

        /// <summary>The manner of the decendents demise. Corresponds to item 37 of the U.S. Standard Certificate of Death.</summary>
        public Dictionary<string, string> MannerOfDeath
        {
            /// <returns>a tuple containing a code, code system, and display</returns>
            get
            {
                string code = GetFirst("Bundle.entry.resource.where($this is Observation).where(code.coding.code='69449-7').value.coding.code");
                string system = GetFirst("Bundle.entry.resource.where($this is Observation).where(code.coding.code='69449-7').value.coding.system");
                string display = GetFirst("Bundle.entry.resource.where($this is Observation).where(code.coding.code='69449-7').value.coding.display");
                Dictionary<string, string> dictionary = new Dictionary<string, string>();
                dictionary.Add("code", code);
                dictionary.Add("system", system);
                dictionary.Add("display", display);
                return dictionary;
            }
            set
            {
                // TODO
            }
        }

        /// <summary>Did tobacco use contribute to death. Corresponds to item 35 of the U.S. Standard Certificate of Death.</summary>
        public Dictionary<string, string> TobaccoUseContributedToDeath
        {
            /// <returns>a tuple containing a code, code system, and display</returns>
            get
            {
                string code = GetFirst("Bundle.entry.resource.where($this is Observation).where(code.coding.code='69443-0').value.coding.code");
                string system = GetFirst("Bundle.entry.resource.where($this is Observation).where(code.coding.code='69443-0').value.coding.system");
                string display = GetFirst("Bundle.entry.resource.where($this is Observation).where(code.coding.code='69443-0').value.coding.display");
                Dictionary<string, string> dictionary = new Dictionary<string, string>();
                dictionary.Add("code", code);
                dictionary.Add("system", system);
                dictionary.Add("display", display);
                return dictionary;
            }
            set
            {
                // TODO
            }
        }

        /// <summary>Given a FHIR path, return the elements that match the given path as a string;
        /// returns an empty array if no matches are found.</summary>
        /// <param name="path">a string representing a FHIR path.</param>
        /// <returns>all elements that match the given path as a string, or an empty array if no matches are found.</returns>
        private string[] GetAll(string path)
        {
            var matches = Navigator.Select(path);
            ArrayList list = new ArrayList();
            foreach (var match in matches)
            {
                list.Add(Convert.ToString(match.Value));
            }
            return list.ToArray(typeof(string)) as string[];
        }

        /// <summary>Given a FHIR path, return the first element that matches the given path as a string;
        /// returns an empty string if no match is found.</summary>
        /// <param name="path">a string representing a FHIR path.</param>
        /// <returns>the first element that matches the given path as a string, or null if no match is found.</returns>
        private string GetFirst(string path)
        {
            var matches = Navigator.Select(path);
            if (matches.Count() > 0)
            {
                return Convert.ToString(matches.First().Value);
            }
            else
            {
                return null;
            }
        }

        /// <summary>Given a FHIR path, return the last element that matches the given path as a string;
        /// returns an empty string if no match is found.</summary>
        /// <param name="path">a string representing a FHIR path.</param>
        /// <returns>the last element that matches the given path as a string, or null if no match is found.</returns>
        private string GetLast(string path)
        {
            var matches = Navigator.Select(path);
            if (matches.Count() > 0)
            {
                return Convert.ToString(matches.Last().Value);
            }
            else
            {
                return null;
            }
        }
    }

}