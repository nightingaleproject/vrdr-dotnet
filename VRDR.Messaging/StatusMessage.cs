using System;
using Hl7.Fhir.Model;

namespace VRDR
{
    /// <summary>Class <c>DeathRecordVoidMessage</c> indicates that a previously submitted DeathRecordSubmissionMessage should be voided.</summary>
    public class StatusMessage : BaseMessage
    {
        /// <summary>
        /// The Event URI for DeathRecordVoidMessage
        /// </summary>
        public const string MESSAGE_TYPE = "http://nchs.cdc.gov/vrdr_status";

        /// <summary>Default constructor that creates a new, empty DeathRecordVoidMessage.</summary>
        public StatusMessage() : base(MESSAGE_TYPE)
        {
        }

        /// <summary>
        /// Construct a DeathRecordVoidMessage from a FHIR Bundle.
        /// </summary>
        /// <param name="messageBundle">a FHIR Bundle that will be used to initialize the DeathRecordVoidMessage</param>
        /// <returns></returns>
        internal StatusMessage(Bundle messageBundle) : base(messageBundle)
        {
        }

        /// <summary>Constructor that takes a VRDR.DeathRecord and creates a message to void that record.</summary>
        /// <param name="record">the VRDR.DeathRecord to create a DeathRecordVoidMessage for.</param>
        public StatusMessage(DeathRecord record) : this()
        {
            ExtractBusinessIdentifiers(record);
        }

        /// <summary>ProcessingStatus</summary>
        public string Status
        {
            get
            {
                return Record?.GetSingleValue<FhirString>("status")?.Value;
            }
            set
            {
                // Allowed values are:   manualCauseOfDeathCoding, manualDemographicCoding
                SetSingleStringValue("status", value);
            }
        }
    }
}
