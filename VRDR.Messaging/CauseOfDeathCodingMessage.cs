using System;
using System.Collections.Generic;
using Hl7.Fhir.Model;

namespace VRDR
{
    /// <summary>
    /// A <c>CauseOfDeathCodingMessage</c> that conveys the coded cause of death information of a decedent.
    /// </summary>
    public class CauseOfDeathCodingMessage : BaseMessage
    {
        /// <summary>
        /// The event URI for CauseOfDeathCodingMessage.
        /// </summary>
        public const String MESSAGE_TYPE = "http://nchs.cdc.gov/vrdr_causeofdeath_coding";

        /// <summary>Bundle that contains the message payload.</summary>
        private DeathRecord deathRecord;

        /// <summary>
        /// Construct a CauseOfDeathCodingMessage from a record containing cause of death coded content.
        /// </summary>
        /// <param name="record">a record containing cause of death coded content</param>
        /// <returns></returns>
        public CauseOfDeathCodingMessage(DeathRecord record) : base(MESSAGE_TYPE)
        {
            this.DeathRecord = record;
            ExtractBusinessIdentifiers(record);
        }

        /// <summary>
        /// Construct a CauseOfDeathCodingMessage from a FHIR Bundle.
        /// </summary>
        /// <param name="messageBundle">a FHIR Bundle that will be used to initialize the CauseOfDeathCodingMessage</param>
        /// <param name="baseMessage">the BaseMessage instance that was constructed during parsing that can be used in a MessageParseException if needed</param>
        /// <returns></returns>
        internal CauseOfDeathCodingMessage(Bundle messageBundle, BaseMessage baseMessage) : base(messageBundle)
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

        /// <summary>Constructor that creates an CauseOfDeathCodingMessage for the specified submitted death record message.</summary>
        /// <param name="messageToCode">the message to create coding response for.</param>
        public CauseOfDeathCodingMessage(BaseMessage messageToCode) : this(messageToCode?.MessageId, messageToCode?.MessageSource, messageToCode?.MessageDestination)
        {
            this.CertNo = messageToCode?.CertNo;
            this.StateAuxiliaryId = messageToCode?.StateAuxiliaryId;
            this.JurisdictionId = messageToCode?.JurisdictionId;
            this.DeathYear = messageToCode?.DeathYear;
        }

        /// <summary>Constructor that creates a CauseOfDeathCoding message for the specified message.</summary>
        /// <param name="messageId">the id of the message to code.</param>
        /// <param name="destination">the endpoint identifier that the ack message will be sent to.</param>
        /// <param name="status">the status being sent, from http://build.fhir.org/ig/nightingaleproject/vital_records_fhir_messaging_ig/branches/main/ValueSet-VRM-Status-vs.html</param>
        /// <param name="source">the endpoint identifier that the ack message will be sent from.</param>

        public CauseOfDeathCodingMessage(string messageId, string destination, string status, string source = "http://nchs.cdc.gov/vrdr_submission") : base(MESSAGE_TYPE)
        {
            Header.Source.Endpoint = source;
            this.MessageDestination = destination;
            MessageHeader.ResponseComponent resp = new MessageHeader.ResponseComponent();
            resp.Identifier = messageId;
            resp.Code = MessageHeader.ResponseType.Ok;
            Header.Response = resp;
        }

        /// <summary>The DeathRecord containing cause of death coded content conveyed by this message</summary>
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
                return deathRecord?.GetCauseOfDeathCodedContentBundle();
            }
        }

        /// <summary>The id of the death record submission/update message that was coded to produce the content of this message</summary>
        /// <value>the message id.</value>
        public string CodedMessageId
        {
            get
            {
                return Header?.Response?.Identifier;
            }
            set
            {
                if (Header.Response == null)
                {
                    Header.Response = new MessageHeader.ResponseComponent();
                    Header.Response.Code = MessageHeader.ResponseType.Ok;
                }
                Header.Response.Identifier = value;
            }
        }

    }

    /// <summary>Class <c>CauseOfDeathCodingUpdateMessage</c> conveys an updated coded cause of death of a decedent.</summary>
    public class CauseOfDeathCodingUpdateMessage : CauseOfDeathCodingMessage
    {
        /// <summary>
        /// The event URI for CauseOfDeathCodingUpdateMessage.
        /// </summary>
        public new const string MESSAGE_TYPE = "http://nchs.cdc.gov/vrdr_causeofdeath_coding_update";

        /// <summary>
        /// Construct a CauseOfDeathCodingUpdateMessage from a record that containing cause of death coded content.
        /// </summary>
        /// <param name="record">a record that containing cause of death coded content for initializing the CauseOfDeathCodingUpdateMessage</param>
        /// <returns></returns>
        public CauseOfDeathCodingUpdateMessage(DeathRecord record) : base(record)
        {
            MessageType = MESSAGE_TYPE;
        }

        /// <summary>
        /// Construct a CauseOfDeathCodingUpdateMessage from a FHIR Bundle.
        /// </summary>
        /// <param name="messageBundle">a FHIR Bundle that will be used to initialize the CauseOfDeathCodingUpdateMessage</param>
        /// <param name="baseMessage">the BaseMessage instance that was constructed during parsing that can be used in a MessageParseException if needed</param>
        /// <returns></returns>
        internal CauseOfDeathCodingUpdateMessage(Bundle messageBundle, BaseMessage baseMessage) : base(messageBundle, baseMessage) { }
    }
}
