using System;
using Hl7.Fhir.Model;

namespace VRDR
{
    /// <summary>Class <c>DeathRecordSubmission</c> supports the submission of VRDR records.</summary>
    public class DeathRecordSubmissionMessage : BaseMessage
    {
        /// <summary>
        /// The event URI for DeathRecordSubmission.
        /// </summary>
        public const String MESSAGE_TYPE = "http://nchs.cdc.gov/vrdr_submission";

        /// <summary>Bundle that contains the message payload.</summary>
        private DeathRecord deathRecord;

        /// <summary>Default constructor that creates a new, empty DeathRecordSubmission.</summary>
        public DeathRecordSubmissionMessage() : base(MESSAGE_TYPE)
        {
        }

        /// <summary>Constructor that takes a VRDR.DeathRecord and wraps it in a DeathRecordSubmission.</summary>
        /// <param name="record">the VRDR.DeathRecord to create a DeathRecordSubmission for.</param>
        public DeathRecordSubmissionMessage(DeathRecord record) : this()
        {
            this.DeathRecord = record;
            ExtractBusinessIdentifiers(record);
        }

        /// <summary>
        /// Construct a DeathRecordSubmission from a FHIR Bundle.
        /// </summary>
        /// <param name="messageBundle">a FHIR Bundle that will be used to initialize the DeathRecordSubmission</param>
        /// <param name="baseMessage">the BaseMessage instance that was constructed during parsing that can be used in a MessageParseException if needed</param>
        internal DeathRecordSubmissionMessage(Bundle messageBundle, BaseMessage baseMessage) : base(messageBundle)
        {
            try
            {
                DeathRecord = new DeathRecord(findEntry<Bundle>());
            }
            catch (System.ArgumentException ex)
            {
                throw new MessageParseException($"Error processing DeathRecord entry in the message: {ex.Message}", baseMessage);
            }
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
                UpdateMessageBundleRecord();
            }
        }

        /// <summary>The record bundle that should go into the message bundle for this message</summary>
        /// <value>the MessageBundleRecord</value>
        protected override Bundle MessageBundleRecord
        {
            get
            {
                return deathRecord?.GetBundle();
            }
        }
    }

    /// <summary>Class <c>DeathRecordUpdateMessage</c> supports the update of VRDR records.</summary>
    public class DeathRecordUpdateMessage : DeathRecordSubmissionMessage
    {
        /// <summary>
        /// The event URI for DeathRecordUpdateMessage.
        /// </summary>
        public new const String MESSAGE_TYPE = "http://nchs.cdc.gov/vrdr_submission_update";

        /// <summary>Default constructor that creates a new, empty DeathRecordUpdateMessage.</summary>
        public DeathRecordUpdateMessage() : base()
        {
            MessageType = MESSAGE_TYPE;
        }

        /// <summary>Constructor that takes a VRDR.DeathRecord and wraps it in a DeathRecordUpdateMessage.</summary>
        /// <param name="record">the VRDR.DeathRecord to create a DeathRecordUpdateMessage for.</param>
        public DeathRecordUpdateMessage(DeathRecord record) : base(record)
        {
            MessageType = MESSAGE_TYPE;
        }

        /// <summary>
        /// Construct a DeathRecordUpdateMessage from a FHIR Bundle.
        /// </summary>
        /// <param name="messageBundle">a FHIR Bundle that will be used to initialize the DeathRecordUpdateMessage</param>
        /// <param name="baseMessage">the BaseMessage instance that was constructed during parsing that can be used in a MessageParseException if needed</param>
        internal DeathRecordUpdateMessage(Bundle messageBundle, BaseMessage baseMessage) : base(messageBundle, baseMessage)
        {
        }
    }
}
