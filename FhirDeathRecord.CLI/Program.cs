using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
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
            if (args.Length == 0) // NOTE (Adam 9/17): Temporarily adding this use case in order to help implement setters.
            {
                Console.WriteLine("No filepath given; Constructing fake record and printing its XML output...\n");
                DeathRecord d = new DeathRecord();

                // Death Record ID
                d.Id = "1";

                // Given Names
                string[] givens = {"First", "Middle"};
                d.GivenName = givens;

                // Last Name
                d.LastName = "Last";

                // Certifier Given Names
                string[] cgivens = {"First", "Middle", "Doc"};
                d.GivenName = cgivens;

                // Certifier Last Name
                d.LastName = "Doctor";

                // Add TimingOfRecentPregnancyInRelationToDeath
                Dictionary<string, string> code = new Dictionary<string, string>();
                code.Add("code", "PHC1260");
                code.Add("system", "http://github.com/nightingaleproject/fhirDeathRecord/sdr/causeOfDeath/vs/PregnancyStatusVS");
                code.Add("display", "Not pregnant within past year");
                d.TimingOfRecentPregnancyInRelationToDeath = code;

                // Add MedicalExaminerContacted
                d.MedicalExaminerContacted = false;

                Console.WriteLine(XDocument.Parse(d.ToXML()).ToString() + "\n");
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
                string json = File.ReadAllText(path);
                DeathRecord deathRecord = new DeathRecord(json);

                // Record Information
                Console.WriteLine($"\tRecord ID: {deathRecord.Id}");
                Console.WriteLine($"\tRecord Creation Date: {deathRecord.CreationDate}");

                // Decedent Information
                Console.WriteLine($"\tGiven Name: {deathRecord.GivenName}");
                Console.WriteLine($"\tLast Name: {deathRecord.LastName}");
                Console.WriteLine($"\tGender: {deathRecord.Gender}");
                Console.WriteLine($"\tSSN: {deathRecord.SSN}");
                Console.WriteLine($"\tEthnicity: {deathRecord.Ethnicity}");
                Console.WriteLine($"\tDate of Birth: {deathRecord.DateOfBirth}");
                Console.WriteLine($"\tDate of Death: {deathRecord.DateOfDeath}");

                foreach(var pair in deathRecord.Address)
                {
                    Console.WriteLine($"\tAddress key: {pair.Key}: value: {pair.Value}");
                }


                // Certifier Information
                Console.WriteLine($"\tCertifier Given Name: {deathRecord.CertifierGivenName}");
                Console.WriteLine($"\tCertifier Last Name: {deathRecord.CertifierLastName}");
                foreach(var pair in deathRecord.CertifierAddress)
                {
                    Console.WriteLine($"\tCertifierAddress key: {pair.Key}: value: {pair.Value}");
                }
                Console.WriteLine($"\tCertifier Type: {deathRecord.CertifierType}");

                // Conditions
                Tuple<string, string>[] causes = deathRecord.CausesOfDeath;
                foreach (var cause in causes)
                {
                    Console.WriteLine($"\tCause: {cause.Item1}, Onset: {cause.Item2}");
                }
                Console.WriteLine($"\tContributing Conditions: {deathRecord.ContributingConditions}");

                // Observations
                Console.WriteLine($"\tAutopsy Performed: {deathRecord.AutopsyPerformed}");
                Console.WriteLine($"\tAutopsy Results Available: {deathRecord.AutopsyResultsAvailable}");
                Console.WriteLine($"\tActual Or Presumed Date of Death: {deathRecord.ActualOrPresumedDateOfDeath}");
                Console.WriteLine($"\tDate Pronounced Dead: {deathRecord.DatePronouncedDead}");
                Console.WriteLine($"\tDeath Resulted from Injury at Work: {deathRecord.DeathFromWorkInjury}");
                Console.WriteLine($"\tDeath From Transport Injury: {deathRecord.DeathFromTransportInjury}");
                Console.WriteLine($"\tDetails of Injury: {deathRecord.DetailsOfInjury}");
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
