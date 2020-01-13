using CsvHelper.Configuration.Attributes;
namespace VRDR.CLI
{
    /** record for Washington State Mortality data
     *  this is the combined data from all input files,
     *  but is also used for reading in the main input file */
    public class WashingtonStateMortalityRecord
    {
        [Name("State file number")]
        public string StateFileNumber { get; set; }

        [Name("Sex")]
        public string Sex { get; set; }

        [Name("Date of Birth")]
        public string DateOfBirth { get; set; }

        [Name("Date of Death")]
        public string DateOfDeath { get; set; }

        [Name("Marital")]
        public string Marital { get; set; }

        [Name("Residence City FIPS code")]
        public string ResidenceCityFips { get; set; }

        [Name("Residence County FIPS code")]
        public string ResidenceCountyFips { get; set; }

        [Name("Residence state FIPS code")]
        public string ResidenceStateFips { get; set; }

        [Name("Residence Zip")]
        public string ResidenceZip { get; set; }

        // [ignore] in the following because they are generated
        [Ignore]
        public string FirstName { get; set; }

        [Ignore]
        public string LastName { get; set; }

        // [ignore] in the following because they are merged in from another file, read separately
        [Ignore]
        public string CauseOfDeathLineA { get; set; }

        [Ignore]
        public string CauseOfDeathLineB { get; set; }

        [Ignore]
        public string CauseOfDeathLineC { get; set; }

        [Ignore]
        public string CauseOfDeathLineD { get; set; }


        public override string ToString()
        {
            return
                //base.ToString() + ": " +
                // this.StateFileNumber + ": " +
                this.FirstName + " " + this.LastName + " " +
                "(" + this.Sex + ") " +
                this.DateOfBirth + " - " +
                this.DateOfDeath +
                "(" + this.Marital + ") " +
                this.CauseOfDeathLineA + "|" +
                this.CauseOfDeathLineB + "|" +
                this.CauseOfDeathLineC + "|" +
                this.CauseOfDeathLineD + "|" +
                this.ResidenceCityFips + " " +
                this.ResidenceCountyFips + " " +
                this.ResidenceStateFips + " " +
                this.ResidenceZip + " " +
                "";
        }

    }

    /** helper record for reading in the secondary file
     *  Note that this is used to simplify merging into the main record */
    public class WashingtonStateMortalityRecord_l {
        [Name("State File Number")]
        public string Id { get; set; }

        [Name("Cause of Death - Line A")]
        public string CauseOfDeathLineA { get; set; }

        [Name("Cause of Death - Line B")]
        public string CauseOfDeathLineB { get; set; }

        [Name("Cause of Death - Line C")]
        public string CauseOfDeathLineC { get; set; }

        [Name("Cause of Death - Line D")]
        public string CauseOfDeathLineD { get; set; }

        public override string ToString()
        {
            return
                //base.ToString() + ": " +
                this.Id.ToString() + ": " +
                this.CauseOfDeathLineA +
                // "            " + this.CauseOfDeathLineB + "\n" +
                // "            " + this.CauseOfDeathLineC + "\n" +
                // "            " + this.CauseOfDeathLineD + "\n"
                "";
        }
    }

}