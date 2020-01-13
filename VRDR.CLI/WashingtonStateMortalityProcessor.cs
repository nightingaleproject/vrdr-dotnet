using System;
using System.IO;
using System.Text;
using CsvHelper;
using System.Collections.Generic;
using Bogus;
using Bogus.DataSets;


namespace VRDR.CLI
{
    /** reads in Washington State's public mortality data from 2 input files
     *  and merges them into a single WashingtonStateMortalityRecord */
    public class WashingtonStateMortalityProcessor
    {
        /** the full set of records, merged from all sources
         *  in the dictionary, the key is the state file number
         *                     the value is the record
         */
        public Dictionary<string, WashingtonStateMortalityRecord> records;

        public WashingtonStateMortalityProcessor() {
            records = new Dictionary<string, WashingtonStateMortalityRecord>();
        }

        public void Read()
        {
            // read in csv status file
            Console.WriteLine("Reading CSV file 1...");
            TextReader reader1 = new StreamReader("./sample_data/wa/DeathStatF2016.csv");
            var csvReader1 = new CsvReader(reader1);
            var recs1 = csvReader1.GetRecords<WashingtonStateMortalityRecord>();

            // generate fake names
            var faker = new Faker("en");    // generator
            foreach (WashingtonStateMortalityRecord record in recs1)
            {
                // Console.WriteLine(record);
                var gender = (record.Sex == "M") ? Name.Gender.Male : Name.Gender.Female;
                record.FirstName = "Test-" + faker.Name.FirstName(gender);
                record.LastName = faker.Name.LastName() + "-Test-Faker";
                records.Add( record.StateFileNumber, record );
            }

            reader1.Close();

            // read in csv literal file
            Console.WriteLine("Reading CSV file 2...");
            TextReader reader2 = new StreamReader("./sample_data/wa/DeathLitF2016.csv");
            var csvReader2 = new CsvReader(reader2);
            var recs2 = csvReader2.GetRecords<WashingtonStateMortalityRecord_l>();

            // merge each record in recs2 into matching WashingtonStateMortalityRecord in records
            foreach (WashingtonStateMortalityRecord_l record in recs2)
            {
                var r = records[record.Id];
                r.CauseOfDeathLineA = record.CauseOfDeathLineA;
                r.CauseOfDeathLineB = record.CauseOfDeathLineB;
                r.CauseOfDeathLineC = record.CauseOfDeathLineC;
                r.CauseOfDeathLineD = record.CauseOfDeathLineD;
                // Console.WriteLine(r);
            }

            reader2.Close();

            /** prints out all the records */
            foreach( KeyValuePair<string, WashingtonStateMortalityRecord> record in records ) {
                Console.WriteLine( record );
            }
        }

    }
}