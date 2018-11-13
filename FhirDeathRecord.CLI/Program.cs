using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Reflection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.ElementModel;
using Hl7.FhirPath;
using FhirDeathRecord;

namespace csharp_fhir_death_record
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("No filepath given; Constructing a fake record and printing its XML output...\n");
                DeathRecord d = new DeathRecord();

                // Death Record ID
                d.Id = "1";

                // Given Names
                string[] givens = {"First", "Middle"};
                d.GivenNames = givens;

                // Last Name
                d.FamilyName = "Last";

                // Certifier Given Names
                string[] cgivens = {"First", "Middle", "Doc"};
                d.GivenNames = cgivens;

                // Certifier Last Name
                d.FamilyName = "Doctor";

                // Add TimingOfRecentPregnancyInRelationToDeath
                Dictionary<string, string> code = new Dictionary<string, string>();
                code.Add("code", "PHC1260");
                code.Add("system", "http://github.com/nightingaleproject/fhirDeathRecord/sdr/causeOfDeath/vs/PregnancyStatusVS");
                code.Add("display", "Not pregnant within past year");
                d.TimingOfRecentPregnancyInRelationToDeath = code;

                // Add MedicalExaminerContacted
                d.MedicalExaminerContacted = false;

                // Add CausesOfDeath
                Tuple<string, string, Dictionary<string, string>>[] causes =
                {
                    Tuple.Create("Example Immediate COD", "minutes", new Dictionary<string, string>(){ {"code", "1234"}, {"system", "example"} }),
                    Tuple.Create("Example Underlying COD 1", "2 hours", new Dictionary<string, string>())
                };
                d.CausesOfDeath = causes;

                Console.WriteLine(XDocument.Parse(d.ToXML()).ToString() + "\n");
            }
            else if (args.Length == 2 && args[0] == "ije")
            {
                Console.WriteLine("Converting FHIR SDR to IJE...\n");
                DeathRecord d = new DeathRecord(File.ReadAllText(args[1]));
                //Console.WriteLine(XDocument.Parse(d.ToXML()).ToString() + "\n");
                IJEMortality ije1 = new IJEMortality(d);
                //Console.WriteLine(ije1.ToString() + "\n\n");
                IJEMortality ije2 = new IJEMortality(ije1.ToString());
                //Console.WriteLine(ije2.ToString() + "\n\n");
                //Console.WriteLine(XDocument.Parse(ije2.ToDeathRecord().ToXML()).ToString() + "\n");
                IJEMortality ije3 = new IJEMortality(new DeathRecord(ije2.ToDeathRecord().ToXML()));
                int issues = 0;
                int total = 0;
                foreach(PropertyInfo property in typeof(IJEMortality).GetProperties())
                {
                    string val1 = Convert.ToString(property.GetValue(ije1, null));
                    string val2 = Convert.ToString(property.GetValue(ije2, null));
                    string val3 = Convert.ToString(property.GetValue(ije3, null));

                    IJEField info = (IJEField)property.GetCustomAttributes().First();

                    if (val1.ToUpper() != val2.ToUpper() || val1.ToUpper() != val3.ToUpper() || val2.ToUpper() != val3.ToUpper())
                    {
                        issues++;
                        Console.WriteLine($"[MISMATCH]\t{info.Name}: {info.Contents} \t\t\"{val1}\" != \"{val2}\" != \"{val3}\"");
                    }
                    total++;
                }
                Console.WriteLine($"\n{issues} issues out of {total} total fields.");
            }
            else if (args.Length == 2 && args[0] == "json2xml")
            {
                Console.WriteLine("Converting FHIR JSON to FHIR XML...\n");
                DeathRecord d = new DeathRecord(File.ReadAllText(args[1]));
                Console.WriteLine(XDocument.Parse(d.ToXML()).ToString() + "\n");
            }
            else if (args.Length == 2 && args[0] == "xml2json")
            {
                Console.WriteLine("Converting FHIR XML to FHIR JSON...\n");
                DeathRecord d = new DeathRecord(File.ReadAllText(args[1]));
                Console.WriteLine(d.ToJSON());
            }
            else
            {
                foreach (var path in args)
                {
                    ReadFile(path);
                }
            }
        }

        private static void ReadFile(string path)
        {
            if (File.Exists(path))
            {
                Console.WriteLine($"Reading file '{path}'");
                string contents = File.ReadAllText(path);
                DeathRecord deathRecord = new DeathRecord(contents);

                // Record Information
                Console.WriteLine($"\tRecord ID: {deathRecord.Id}");

                // Decedent Information
                Console.WriteLine($"\tGiven Name: {string.Join(", ", deathRecord.GivenNames)}");
                Console.WriteLine($"\tLast Name: {deathRecord.FamilyName}");
                Console.WriteLine($"\tGender: {deathRecord.Gender}");
                Console.WriteLine($"\tSSN: {deathRecord.SSN}");
                Console.WriteLine($"\tEthnicity: {deathRecord.Ethnicity}");
                Console.WriteLine($"\tDate of Birth: {deathRecord.DateOfBirth}");
                Console.WriteLine($"\tDate of Death: {deathRecord.DateOfDeath}");

                // Certifier Information
                Console.WriteLine($"\tCertifier Given Name: {deathRecord.CertifierGivenNames}");
                Console.WriteLine($"\tCertifier Last Name: {deathRecord.CertifierFamilyName}");
                foreach(var pair in deathRecord.CertifierAddress)
                {
                    Console.WriteLine($"\tCertifierAddress key: {pair.Key}: value: {pair.Value}");
                }
                Console.WriteLine($"\tCertifier Type: {deathRecord.CertifierType}");

                // Conditions
                Tuple<string, string, Dictionary<string, string>>[] causes = deathRecord.CausesOfDeath;
                foreach (var cause in causes)
                {
                    Console.WriteLine($"\tCause: {cause.Item1}, Onset: {cause.Item2}, Code: {string.Join(", ", cause.Item3)}");
                }
                Console.WriteLine($"\tContributing Conditions: {deathRecord.ContributingConditions}");

                // Observations
                Console.WriteLine($"\tAutopsy Performed: {deathRecord.AutopsyPerformed}");
                Console.WriteLine($"\tAutopsy Results Available: {deathRecord.AutopsyResultsAvailable}");
                Console.WriteLine($"\tActual Or Presumed Date of Death: {deathRecord.ActualOrPresumedDateOfDeath}");
                Console.WriteLine($"\tDate Pronounced Dead: {deathRecord.DatePronouncedDead}");
                Console.WriteLine($"\tDeath Resulted from Injury at Work: {deathRecord.DeathFromWorkInjury}");
                Console.WriteLine($"\tDeath From Transport Injury: {string.Join(", ", deathRecord.DeathFromTransportInjury)}");
                Console.WriteLine($"\tDetails of Injury: {string.Join(", ", deathRecord.DetailsOfInjury)}");
                Console.WriteLine($"\tMedical Examiner Contacted: {deathRecord.MedicalExaminerContacted}");
                Console.WriteLine($"\tTiming of Recent Pregnancy In Relation to Death: {string.Join(", ", deathRecord.TimingOfRecentPregnancyInRelationToDeath)}");
                foreach(var pair in deathRecord.MannerOfDeath)
                {
                    Console.WriteLine($"\tManner of Death key: {pair.Key}: value: {pair.Value}");
                }

                foreach(var pair in deathRecord.TobaccoUseContributedToDeath)
                {
                    Console.WriteLine($"\tTobacco Use Contributed to Death key: {pair.Key}: value: {pair.Value}");
                }
            }
            else
            {
                Console.WriteLine($"Error: File '{path}' does not exist");
            }
        }
    }
}
