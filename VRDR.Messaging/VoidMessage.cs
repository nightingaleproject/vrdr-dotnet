using System;
using Hl7.Fhir.Model;

namespace VRDR
{
    /// <summary>Class <c>VoidMessage</c> indicates that a previously submitted DeathRecordSubmission message should be voided.</summary>
    public class VoidMessage : BaseMessage
    {
        /// <summary>
        /// The Event URI for VoidMessage
        /// </summary>
        public const string MESSAGE_TYPE = "http://nchs.cdc.gov/vrdr_submission_void";

        /// <summary>Default constructor that creates a new, empty VoidMessage.</summary>
        public VoidMessage() : base(MESSAGE_TYPE)
        {
        }

        /// <summary>
        /// Construct a VoidMessage from a FHIR Bundle.
        /// </summary>
        /// <param name="messageBundle">a FHIR Bundle that will be used to initialize the VoidMessage</param>
        /// <returns></returns>
        internal VoidMessage(Bundle messageBundle) : base(messageBundle)
        {
        }

        /// <summary>Constructor that takes a VRDR.DeathRecord and creates a message to void that record.</summary>
        /// <param name="record">the VRDR.DeathRecord to create a VoidMessage for.</param>
        public VoidMessage(DeathRecord record) : this()
        {
            ExtractBusinessIdentifiers(record);
        }

        /// <summary>The number of records to void starting at the certificate number specified by the `CertificateNumber` parameter</summary>
        public uint? BlockCount
        {
            get
            {
                return (uint?)Record?.GetSingleValue<PositiveInt>("block_count")?.Value;
            }
            set
            {
                Record.Remove("block_count");
                if (value != null && value > 1)
                {
                    Record.Add("block_count", new PositiveInt((int)value));
                }
            }
        }

    }
}
