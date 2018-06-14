using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.ElementModel;
using Hl7.FhirPath;

namespace csharp_fhir_death_record
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (var path in args)
            {
                ReadFile(path);
            }
        }

        private static void ReadFile(string path)
        {
            if (File.Exists(path))
            {
                Console.WriteLine($"Reading file '{path}'");
                string json = File.ReadAllText(path);
                FhirJsonParser parser = new FhirJsonParser();
                Bundle bundle = parser.Parse<Bundle>(json);
                // TODO: Need to gracefully handle edge conditins like no decedent present
                Patient decedent = (Patient)bundle.Entry.Find(e => e.Resource.ResourceType.ToString() == "Patient").Resource;
                Console.WriteLine($"  Decedent Name: {decedent.Name[0].ToString()}");
                var deceasedDateTime = DateTime.Parse(decedent.Deceased.ToString());
                Console.WriteLine($"  Time and Date of Death: {String.Format("{0:MMMM d, yyyy \\a\\t h:mmtt}", deceasedDateTime)}");
                Practitioner certifier = (Practitioner)bundle.Entry.Find(e => e.Resource.ResourceType.ToString() == "Practitioner").Resource;
                Console.WriteLine($"  Certifier Name: {certifier.Name[0].ToString()}");

                var conditions = bundle.Entry.FindAll(e => e.Resource.ResourceType.ToString() == "Condition").Select(e => (Condition)e.Resource);
                Regex htmlRegex = new Regex("<.*?>");
                foreach (var condition in conditions)
                {
                    if (condition.Onset != null)
                    {
                        Console.WriteLine($"  Cause of Death: {htmlRegex.Replace(condition.Text.Div, "")} ({condition.Onset})");
                    }
                    else
                    {
                        Console.WriteLine($"  Contributing Conditions: {htmlRegex.Replace(condition.Text.Div, "")}");
                    }
                }

                // TODO: The use of FHIR paths seems potentially valuable, but the paths below don't appear to work as expected
                //PocoNavigator navigator = new PocoNavigator(bundle);
                //var matches = navigator.Select("Bundle.entry.resource.where(resourceType='Patient').name.family");
                //var matches = navigator.Select("Bundle.entry.resource.where(meta.profile='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-Decedent')");
                //var matches = navigator.Select("Bundle.entry.resource");
                //Console.WriteLine($"Found {matches.Count()} matches");
                //foreach (var match in matches)
                //{
                //    Console.WriteLine(match.Value);
                //    if (match.Type == "Patient")
                //    {
                //        var names = match.Select("name.family");
                //        foreach (var name in names)
                //        {
                //            Console.WriteLine($"Name: {name.Value}");
                //        }
                //    }
                //}
            }
            else
            {
                Console.WriteLine($"Error: File '{path}' does not exist");
            }
        }
    }
}
