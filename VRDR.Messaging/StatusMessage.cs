using System;
using Hl7.Fhir.Model;

namespace VRDR
{
    /// <summary>Class <c>StatusMessage</c> provides a status update to a jurisdiction about a previously submitted message.</summary>
    public class StatusMessage : BaseMessage
    {
        /// <summary>
        /// The Event URI for StatusMessage
        /// </summary>
        public const string MESSAGE_TYPE = "http://nchs.cdc.gov/vrdr_status";

        /// <summary>Default constructor that creates a new, empty StatusMessage.</summary>
        public StatusMessage() : base(MESSAGE_TYPE)
        {
        }

        /// <summary>
        /// Construct a StatusMessage from a FHIR Bundle.
        /// </summary>
        /// <param name="messageBundle">a FHIR Bundle that will be used to initialize the StatusMessage</param>
        /// <returns></returns>
        internal StatusMessage(Bundle messageBundle) : base(messageBundle)
        {
        }
        /// <summary>Constructor that creates a status message for the specified message.</summary>
        /// <param name="messageToStatus">the message to create a status for.</param>
        /// <param name="status"> status value </param>
        public StatusMessage(BaseMessage messageToStatus, string status) : this(messageToStatus?.MessageId, messageToStatus?.MessageSource, status, messageToStatus?.MessageDestination)
        {
            this.CertNo = messageToStatus?.CertNo;
            this.StateAuxiliaryId = messageToStatus?.StateAuxiliaryId;
            this.JurisdictionId = messageToStatus?.JurisdictionId;
            this.DeathYear = messageToStatus?.DeathYear;
        }

        /// <summary>Constructor that creates a status message for the specified message.</summary>
        /// <param name="messageId">the id of the message to create status message for.</param>
        /// <param name="destination">the endpoint identifier that the ack message will be sent to.</param>
        /// <param name="status">the status being sent, from http://build.fhir.org/ig/nightingaleproject/vital_records_fhir_messaging_ig/branches/main/ValueSet-VRM-Status-vs.html</param>
        /// <param name="source">the endpoint identifier that the ack message will be sent from.</param>

        public StatusMessage(string messageId, string destination, string status, string source = "http://nchs.cdc.gov/vrdr_submission") : base(MESSAGE_TYPE)
        {
            Header.Source.Endpoint = source;
            this.MessageDestination = destination;
            MessageHeader.ResponseComponent resp = new MessageHeader.ResponseComponent();
            resp.Identifier = messageId;
            resp.Code = MessageHeader.ResponseType.Ok;
            Header.Response = resp;
            Status = status; // This should be a value from http://build.fhir.org/ig/nightingaleproject/vital_records_fhir_messaging_ig/branches/main/ValueSet-VRM-Status-vs.html
        }
        /// <summary>The id of the message whose status is being reported by this message</summary>
        /// <value>the message id.</value>
        public string StatusedMessageId
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
        /// <summary>ProcessingStatus</summary>
        public string Status
        {
            get
            {
                return Record?.GetSingleValue<FhirString>("status")?.Value;
            }
            set
            {
                // Allowed values are:   manualCauseOfDeathCoding, manualDemographicCoding.  need to add CauseOfDeathCodingCanceled, and DemographicCodingCanceled.
                SetSingleStringValue("status", value);
            }
        }
    }
}
