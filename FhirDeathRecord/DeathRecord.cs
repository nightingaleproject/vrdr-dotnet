using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.IO;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.FhirPath;

namespace FhirDeathRecord
{
    public class DeathRecord
    {
        private PocoNavigator Navigator;

        public DeathRecord(string record)
        {
            // Try JSON
            try
            {
                FhirJsonParser parser = new FhirJsonParser();
                Bundle bundle = parser.Parse<Bundle>(record);
                Navigator = new PocoNavigator(bundle);
            }
            catch {}

            // Try XML
            try
            {
                FhirXmlParser parser = new FhirXmlParser();
                Bundle bundle = parser.Parse<Bundle>(record);
                Navigator = new PocoNavigator(bundle);
            }
            catch {}

            // If the given record string was not JSON or XML, fail immediately.
            if (Navigator == null)
            {
                throw new System.ArgumentException("Record is neither valid XML nor JSON.");
            }
        }

        // Given a FHIR path, return the first element that matches the path as a string;
        // returns an empty string if no match is found
        private string GetFirst(string path)
        {
            var matches = Navigator.Select(path);
            if (matches.Count() > 0)
            {
                return Convert.ToString(matches.First().Value);
            }
            else
            {
                // TODO: Figure out appropriate way to handle element not found
                Console.WriteLine("element not found");
                return "";
            }
        }

        public string GivenName { get => GetFirst("Bundle.entry.resource.where($this is Patient).name[0].given"); }
        public string LastName { get => GetFirst("Bundle.entry.resource.where($this is Patient).name[0].family"); }
        public string SSN { get => GetFirst("Bundle.entry.resource.where($this is Patient).identifier.where(system = 'http://hl7.org/fhir/sid/us-ssn').value"); }

        public string CertifierGivenName { get => GetFirst("Bundle.entry.resource.where($this is Practitioner).name[0].given"); }
        public string CertifierLastName { get => GetFirst("Bundle.entry.resource.where($this is Practitioner).name[0].family"); }

        public string COD(int index)
        {
            // Differentiate cause of death from contributing conditions based on presense of onset field
            // TODO: This doesn't seem ideal, should we use something else like meta (which isn't actually required)?
            string cod = GetFirst($"Bundle.entry.resource.where($this is Condition).where(onset).text.div[{index}]");
            Regex htmlRegex = new Regex("<.*?>");
            return htmlRegex.Replace(cod, "");
        }

        public string CODInterval(int index)
        {
            return GetFirst($"Bundle.entry.resource.where($this is Condition).where(onset).onset[{index}]");
        }

        public string ContributingConditions
        {
            get
            {
                string cc = GetFirst("Bundle.entry.resource.where($this is Condition).where(onset.empty()).text.div");
                Regex htmlRegex = new Regex("<.*?>");
                return htmlRegex.Replace(cc, "");
            }
        }

        public string AutopsyPerformed
        {
            get
            {
                string ap = GetFirst("Bundle.entry.resource.where($this is Observation).where(code.coding.code='85699-7').value");
                return ap;
            }
        }

        public string AutopsyResultsAvailable
        {
            get
            {
                string ara = GetFirst("Bundle.entry.resource.where($this is Observation).where(code.coding.code='69436-4').value");
                return ara;
            }
        }

        public string MannerOfDeath
        {
            get
            {
                string mod = GetFirst("Bundle.entry.resource.where($this is Observation).where(code.coding.code='69449-7').value.coding.display");
                return mod;
            }
        }

        public string TobaccoUseContributedToDeath
        {
            get
            {
                string tc = GetFirst("Bundle.entry.resource.where($this is Observation).where(code.coding.code='69443-0').value.coding.display");
                return tc;
            }
        }
    }

}