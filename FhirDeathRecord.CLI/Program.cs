using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
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
                DeathRecord deathRecord = new DeathRecord(json);

                // Decedent Information
                Console.WriteLine($"\tGiven Name: {deathRecord.GivenName}");
                Console.WriteLine($"\tLast Name: {deathRecord.LastName}");
                Console.WriteLine($"\tGender: {deathRecord.Gender}");
                Console.WriteLine($"\tSSN: {deathRecord.SSN}");
                Console.WriteLine($"\tDate of Birth: {deathRecord.DateOfBirth}");
                Console.WriteLine($"\tDate of Death: {deathRecord.DateOfDeath}");

                foreach(var pair in deathRecord.Address)
                {
                    Console.WriteLine($"\tAddressh key: {pair.Key}: value: {pair.Value}");
                }


                // Certifier Information
                Console.WriteLine($"\tCertifier Given Name: {deathRecord.CertifierGivenName}");
                Console.WriteLine($"\tCertifier Last Name: {deathRecord.CertifierLastName}");

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
                Console.WriteLine($"\tDeath Resulted from Injury at Work: {deathRecord.DeathResultedFromInjuryAtWork}");
                Console.WriteLine($"\tDeath From Transport Injury: {deathRecord.DeathFromTransportInjury}");
                Console.WriteLine($"\tDetails of Injury: {deathRecord.DetailsOfInjury}");
                Console.WriteLine($"\tMedical Examiner Contacted: {deathRecord.MedicalExaminerContacted}");
                Console.WriteLine($"\tTiming of Recent Pregnancy In Relation to Death: {deathRecord.TimingOfRecentPregnancyInRelationToDeath}");
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
