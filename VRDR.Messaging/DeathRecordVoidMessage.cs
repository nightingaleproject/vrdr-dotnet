using System;
using Hl7.Fhir.Model;

namespace VRDR
{
    /// <summary>Class <c>DeathRecordVoidMessage</c> indicates that a previously submitted DeathRecordSubmissionMessage should be voided.</summary>
    public class DeathRecordVoidMessage : BaseMessage
    {
        /// <summary>
        /// The Event URI for DeathRecordVoidMessage
        /// </summary>
        public const string MESSAGE_TYPE = "http://nchs.cdc.gov/vrdr_submission_void";

        /// <summary>Default constructor that creates a new, empty DeathRecordVoidMessage.</summary>
        public DeathRecordVoidMessage() : base(MESSAGE_TYPE)
        {
        }

        /// <summary>
        /// Construct a DeathRecordVoidMessage from a FHIR Bundle.
        /// </summary>
        /// <param name="messageBundle">a FHIR Bundle that will be used to initialize the DeathRecordVoidMessage</param>
        /// <returns></returns>
        internal DeathRecordVoidMessage(Bundle messageBundle) : base(messageBundle)
        {
        }

        /// <summary>Constructor that takes a VRDR.DeathRecord and creates a message to void that record.</summary>
        /// <param name="record">the VRDR.DeathRecord to create a DeathRecordVoidMessage for.</param>
        public DeathRecordVoidMessage(DeathRecord record) : this()
        {
            ExtractBusinessIdentifiers(record);
        }

        /// <summary>The number of records to void starting at the certificate number specified by the `CertNo` parameter</summary>
        public uint? BlockCount
        {
            get
            {
                return (uint?)Record?.GetSingleValue<UnsignedInt>("block_count")?.Value;
            }
            set
            {
                Record.Remove("block_count");
                if (value != null && value >= 0)
                {
                    Record.Add("block_count", new UnsignedInt((int)value));
                }
            }
        }
    }
}
