using Hl7.Fhir.Model;

namespace VRDR
{
    /// <summary>Class <c>DeathRecordSubmission</c> supports the submission of VRDR records.</summary>
    public class DeathRecordSubmission : BaseMessage
    {
        /// <summary>Bundle that contains the message payload.</summary>
        private DeathRecord deathRecord;

        /// <summary>Default constructor that creates a new, empty DeathRecordSubmission.</summary>
        public DeathRecordSubmission() : base("http://nchs.cdc.gov/vrdr_submission")
        {
        }

        /// <summary>Constructor that takes a VRDR.DeathRecord and wraps it in a DeathRecordSubmission.</summary>
        /// <param name="record">the VRDR.DeathRecord to create a DeathRecordSubmission for.</param>
        public DeathRecordSubmission(DeathRecord record) : this()
        {
            DeathRecord = record;
        }

        /// <summary>
        /// Construct a DeathRecordSubmission from a FHIR Bundle.
        /// </summary>
        /// <param name="messageBundle">a FHIR Bundle that will be used to initialize the DeathRecordSubmission</param>
        /// <returns></returns>
        internal DeathRecordSubmission(Bundle messageBundle) : base(messageBundle)
        {
            deathRecord = new DeathRecord(findEntry<Bundle>(ResourceType.Bundle));
        }

        /// <summary>The DeathRecord conveyed by this message</summary>
        /// <value>the DeathRecord</value>
        public DeathRecord DeathRecord
        {
            get
            {
                return deathRecord;
            }
            set
            {
                deathRecord = value;
                MessageBundle.Entry.RemoveAll( entry => entry.Resource.ResourceType == ResourceType.Bundle );
                Header.Focus.Clear();
                if (deathRecord != null)
                {
                    MessageBundle.AddResourceEntry(deathRecord.GetBundle(), "urn:uuid:" + deathRecord.GetBundle().Id);
                    Header.Focus.Add(new ResourceReference(deathRecord.GetBundle().Id));
                }
            }
        }
    }

    /// <summary>Class <c>DeathRecordUpdate</c> supports the update of VRDR records.</summary>
    public class DeathRecordUpdate : DeathRecordSubmission
    {
        /// <summary>Default constructor that creates a new, empty DeathRecordUpdate.</summary>
        public DeathRecordUpdate() : base()
        {
            MessageType = "http://nchs.cdc.gov/vrdr_submission_update";
        }

        /// <summary>Constructor that takes a VRDR.DeathRecord and wraps it in a DeathRecordUpdate.</summary>
        /// <param name="record">the VRDR.DeathRecord to create a DeathRecordUpdate for.</param>
        public DeathRecordUpdate(DeathRecord record) : base(record)
        {
            MessageType = "http://nchs.cdc.gov/vrdr_submission_update";
        }

        /// <summary>
        /// Construct a DeathRecordUpdate from a FHIR Bundle.
        /// </summary>
        /// <param name="messageBundle">a FHIR Bundle that will be used to initialize the DeathRecordUpdate</param>
        /// <returns></returns>
        internal DeathRecordUpdate(Bundle messageBundle) : base(messageBundle)
        {
        }
    }
}
