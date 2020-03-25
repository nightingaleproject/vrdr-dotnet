using System;
using Hl7.Fhir.Model;

namespace VRDR
{
    /// <summary>Class <c>AckMessage</c> supports the acknowledgment of other messages.</summary>
    public class AckMessage : BaseMessage
    {
        /// <summary>Constructor that creates an acknowledgement for the specified message.</summary>
        /// <param name="messageToAck">the message to create an acknowledgement for.</param>
        /// <param name="source">the endpoint identifier that the ack message will be sent from.</param>
        public AckMessage(BaseMessage messageToAck, string source = "http://nchs.cdc.gov/vrdr_submission") : this(messageToAck.MessageId, messageToAck.MessageSource, source)
        {
        }

        /// <summary>Constructor that creates an acknowledgement for the specified message.</summary>
        /// <param name="messageId">the id of the message to create an acknowledgement for.</param>
        /// <param name="destination">the endpoint identifier that the ack message will be sent to.</param>
        /// <param name="source">the endpoint identifier that the ack message will be sent from.</param>
        public AckMessage(string messageId, string destination, string source = "http://nchs.cdc.gov/vrdr_submission") : base("vrdr_acknowledgement")
        {
            Header.Source.Endpoint = source;
            this.MessageDestination = destination;
            MessageHeader.ResponseComponent resp = new MessageHeader.ResponseComponent();
            resp.Identifier = messageId;
            resp.Code = MessageHeader.ResponseType.Ok;
            Header.Response = resp;
        }

        /// <summary>Constructor that takes a string that represents an AckMessage in either XML or JSON format.</summary>
        /// <param name="message">represents an AckMessage in either XML or JSON format.</param>
        /// <param name="permissive">if the parser should be permissive when parsing the given string</param>
        /// <exception cref="ArgumentException">Message is neither valid XML nor JSON.</exception>
        public AckMessage(string message, bool permissive = false) : base(message, permissive)
        {
        }

        /// <summary>The id of the message that is being acknowledged by this message</summary>
        /// <value>the message id.</value>
        public string AckedMessageId
        {
            get
            {
                return Header.Response.Identifier;
            }
            set
            {
                Header.Response.Identifier = value;
            }
        }

    }
}
