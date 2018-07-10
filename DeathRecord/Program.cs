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
                DeathRecord deathRecord = new DeathRecord(json);
                Console.WriteLine($"  Given Name: {deathRecord.GivenName}");
                Console.WriteLine($"  Last Name: {deathRecord.LastName}");
                Console.WriteLine($"  SSN: {deathRecord.SSN}");
                Console.WriteLine($"  Cause of Death 1: {deathRecord.COD(0)}");
                Console.WriteLine($"  Cause of Death Interval 1: {deathRecord.CODInterval(0)}");
                Console.WriteLine($"  Cause of Death 2: {deathRecord.COD(1)}");
                Console.WriteLine($"  Cause of Death Interval 2: {deathRecord.CODInterval(1)}");
                Console.WriteLine($"  Cause of Death 3: {deathRecord.COD(2)}");
                Console.WriteLine($"  Cause of Death Interval 3: {deathRecord.CODInterval(2)}");
                Console.WriteLine($"  Cause of Death 4: {deathRecord.COD(3)}");
                Console.WriteLine($"  Cause of Death Interval 4: {deathRecord.CODInterval(3)}");
                Console.WriteLine($"  Contributing Conditions: {deathRecord.ContributingConditions}");
                Console.WriteLine($"  Certifier Given Name: {deathRecord.CertifierGivenName}");
                Console.WriteLine($"  Certifier Last Name: {deathRecord.CertifierLastName}");
            }
            else
            {
                Console.WriteLine($"Error: File '{path}' does not exist");
            }
        }
    }
}
