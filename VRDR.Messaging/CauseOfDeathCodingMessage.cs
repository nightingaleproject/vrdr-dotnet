using System;
using System.Collections.Generic;
using Hl7.Fhir.Model;

namespace VRDR
{
    /// <summary>
    /// A <c>CodingResponseMessage</c> that conveys the coded cause of death information of a decedent.
    /// </summary>
    public class CauseOfDeathCodingMessage : BaseMessage
    {
        /// <summary>
        /// The event URI for CauseOfDeathCodingMessage.
        /// </summary>
        public const String MESSAGE_TYPE = "http://nchs.cdc.gov/vrdr_causeofdeath_coding";

        ///// <summary>Constructor that creates a response for the specified message.</summary>
        ///// <param name="sourceMessage">the message to create a response for.</param>
        ///// <param name="source">the endpoint identifier that the message will be sent from.</param>
        //public CauseOfDeathCodingMessage(BaseMessage sourceMessage, string source = "http://nchs.cdc.gov/vrdr_submission") : this(sourceMessage.MessageSource, source)
        //{
        //    this.CertNo = sourceMessage?.CertNo;
        //    this.StateAuxiliaryId = sourceMessage?.StateAuxiliaryId;
        //    this.JurisdictionId = sourceMessage?.JurisdictionId;
        //    this.DeathYear = sourceMessage?.DeathYear;
        //}

        /// <summary>
        /// Construct a CauseOfDeathCodingMessage from a FHIR Bundle.
        /// </summary>
        /// <param name="messageBundle">a FHIR Bundle that will be used to initialize the CauseOfDeathCodingMessage</param>
        /// <returns></returns>
        internal CauseOfDeathCodingMessage(Bundle messageBundle) : base(messageBundle)
        {
        }

        ///// <summary>Constructor that creates a response for the specified message.</summary>
        ///// <param name="destination">the endpoint identifier that the response message will be sent to.</param>
        ///// <param name="source">the endpoint identifier that the response message will be sent from.</param>
        //public CauseOfDeathCodingMessage(string destination, string source = "http://nchs.cdc.gov/vrdr_submission") : base(MESSAGE_TYPE, destination, source)
        //{
        //}
    }

    /// <summary>Class <c>CauseOfDeathCodingUpdateMessage</c> conveys an updated coded cause of death of a decedent.</summary>
    public class CauseOfDeathCodingUpdateMessage : CauseOfDeathCodingMessage
    {
        /// <summary>
        /// The event URI for CauseOfDeathCodingUpdateMessage.
        /// </summary>
        public new const string MESSAGE_TYPE = "http://nchs.cdc.gov/vrdr_causeofdeath_coding_update";

        ///// <summary>Constructor that creates an update for the specified message.</summary>
        ///// <param name="sourceMessage">the message to create a response for.</param>
        ///// <param name="source">the endpoint identifier that the message will be sent from.</param>
        //public CauseOfDeathCodingUpdateMessage(BaseMessage sourceMessage, string source = "http://nchs.cdc.gov/vrdr_submission") : this(sourceMessage.MessageSource, source)
        //{
        //}

        /// <summary>
        /// Construct a CauseOfDeathCodingUpdateMessage from a FHIR Bundle.
        /// </summary>
        /// <param name="messageBundle">a FHIR Bundle that will be used to initialize the CauseOfDeathCodingUpdateMessage</param>
        /// <returns></returns>
        internal CauseOfDeathCodingUpdateMessage(Bundle messageBundle) : base(messageBundle)
        {
        }

        ///// <summary>Constructor that creates a response for the specified message.</summary>
        ///// <param name="destination">the endpoint identifier that the response message will be sent to.</param>
        ///// <param name="source">the endpoint identifier that the response message will be sent from.</param>
        //public CauseOfDeathCodingUpdateMessage(string destination, string source = "http://nchs.cdc.gov/vrdr_submission") : base(destination, source)
        //{
        //    Header.Event = new FhirUri(MESSAGE_TYPE);
        //}
    }
}
