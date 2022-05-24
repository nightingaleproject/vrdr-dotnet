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
                DeathRecord = new DeathRecord(findEntry<Bundle>(ResourceType.Bundle));
            }
            catch (System.ArgumentException ex)
            {
                throw new MessageParseException($"Error processing DeathRecord entry in the message: {ex.Message}", baseMessage);
            }
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
