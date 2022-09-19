using Hl7.Fhir.Model;

namespace VRDR
{
    /// <summary>Class <c>AcknowledgementMessage</c> supports the acknowledgment of other messages.</summary>
    public class AcknowledgementMessage : BaseMessage
    {
        /// <summary>
        /// The Event URI for AcknowledgementMessage
        /// </summary>
        public const string MESSAGE_TYPE = "http://nchs.cdc.gov/vrdr_acknowledgement";

        /// <summary>Constructor that creates an acknowledgement for the specified message.</summary>
        /// <param name="messageToAck">the message to create an acknowledgement for.</param>
        public AcknowledgementMessage(BaseMessage messageToAck) : this(messageToAck?.MessageId, messageToAck?.MessageSource, messageToAck?.MessageDestination)
        {
            this.CertNo = messageToAck?.CertNo;
            this.StateAuxiliaryId = messageToAck?.StateAuxiliaryId;
            this.JurisdictionId = messageToAck?.JurisdictionId;
            this.DeathYear = messageToAck?.DeathYear;

            if(typeof(DeathRecordVoidMessage).IsInstanceOfType(messageToAck))
            {
                DeathRecordVoidMessage voidMessageToAck = (DeathRecordVoidMessage) messageToAck;
                this.BlockCount = voidMessageToAck?.BlockCount;
            }
        }

        /// <summary>
        /// Construct an AcknowledgementMessage from a FHIR Bundle.
        /// </summary>
        /// <param name="messageBundle">a FHIR Bundle that will be used to initialize the AcknowledgementMessage</param>
        /// <returns></returns>
        internal AcknowledgementMessage(Bundle messageBundle) : base(messageBundle)
        {
            // no payload for Ack message
        }

        /// <summary>Constructor that creates an acknowledgement for the specified message.</summary>
        /// <param name="messageId">the id of the message to create an acknowledgement for.</param>
        /// <param name="destination">the endpoint identifier that the ack message will be sent to.</param>
        /// <param name="source">the endpoint identifier that the ack message will be sent from.</param>
        public AcknowledgementMessage(string messageId, string destination, string source = "http://nchs.cdc.gov/vrdr_submission") : base(MESSAGE_TYPE)
        {
            Header.Source.Endpoint = source;
            this.MessageDestination = destination;
            MessageHeader.ResponseComponent resp = new MessageHeader.ResponseComponent();
            resp.Identifier = messageId;
            resp.Code = MessageHeader.ResponseType.Ok;
            Header.Response = resp;
        }

        /// <summary>The id of the message that is being acknowledged by this message</summary>
        /// <value>the message id.</value>
        public string AckedMessageId
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

        /// <summary>The number of records to void starting at the certificate number specified by the `CertNo` parameter</summary>
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
