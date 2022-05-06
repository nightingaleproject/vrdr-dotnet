using System;
using System.Collections.Generic;
using Hl7.Fhir.Model;

namespace VRDR
{
    /// <summary>
    /// A <c>CodingResponseMessage</c> that conveys the coded demographic information of a decedent.
    /// </summary>
    public class DemographicsCodingMessage : BaseMessage
    {
        /// <summary>
        /// The event URI for DemographicCodingResponseMessage.
        /// </summary>
        public const String MESSAGE_TYPE = "http://nchs.cdc.gov/vrdr_demographics_coding";

        ///// <summary>Constructor that creates a response for the specified message.</summary>
        ///// <param name="sourceMessage">the message to create a response for.</param>
        ///// <param name="source">the endpoint identifier that the message will be sent from.</param>
        //public DemographicsCodingMessage(BaseMessage sourceMessage, string source = "http://nchs.cdc.gov/vrdr_submission") : this(sourceMessage.MessageSource, source)
        //{
        //    this.CertNo = sourceMessage?.CertNo;
        //    this.StateAuxiliaryId = sourceMessage?.StateAuxiliaryId;
        //    this.JurisdictionId = sourceMessage?.JurisdictionId;
        //    this.DeathYear = sourceMessage?.DeathYear;
        //}

        /// <summary>
        /// Construct a DemographicCodingResponseMessage from a FHIR Bundle.
        /// </summary>
        /// <param name="messageBundle">a FHIR Bundle that will be used to initialize the DemographicCodingResponseMessage</param>
        /// <returns></returns>
        internal DemographicsCodingMessage(Bundle messageBundle) : base(messageBundle)
        {
        }

        ///// <summary>Constructor that creates a response for the specified message.</summary>
        ///// <param name="destination">the endpoint identifier that the response message will be sent to.</param>
        ///// <param name="source">the endpoint identifier that the response message will be sent from.</param>
        //public DemographicsCodingMessage(string destination, string source = "http://nchs.cdc.gov/vrdr_submission") : base(MESSAGE_TYPE, destination, source)
        //{
        //}
    }

    /// <summary>Class <c>DemographicsCodingUpdateMessage</c> conveys an updated coded race and ethnicity of a decedent.</summary>
    public class DemographicsCodingUpdateMessage : DemographicsCodingMessage
    {
        /// <summary>
        /// The event URI for DemographicsCodingUpdateMessage.
        /// </summary>
        public new const string MESSAGE_TYPE = "http://nchs.cdc.gov/vrdr_demographics_coding_update";

        ///// <summary>Constructor that creates an update for the specified message.</summary>
        ///// <param name="sourceMessage">the message to create a response for.</param>
        ///// <param name="source">the endpoint identifier that the message will be sent from.</param>
        //public DemographicsCodingUpdateMessage(BaseMessage sourceMessage, string source = "http://nchs.cdc.gov/vrdr_submission") : this(sourceMessage.MessageSource, source)
        //{
        //}

        /// <summary>
        /// Construct a DemographicsCodingUpdateMessage from a FHIR Bundle.
        /// </summary>
        /// <param name="messageBundle">a FHIR Bundle that will be used to initialize the DemographicsCodingUpdateMessage</param>
        /// <returns></returns>
        internal DemographicsCodingUpdateMessage(Bundle messageBundle) : base(messageBundle)
        {
        }

        ///// <summary>Constructor that creates a response for the specified message.</summary>
        ///// <param name="destination">the endpoint identifier that the response message will be sent to.</param>
        ///// <param name="source">the endpoint identifier that the response message will be sent from.</param>
        //public DemographicsCodingUpdateMessage(string destination, string source = "http://nchs.cdc.gov/vrdr_submission") : base(destination, source)
        //{
        //    Header.Event = new FhirUri(MESSAGE_TYPE);
        //}
    }
}
