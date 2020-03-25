using System;
using System.Linq;
using Hl7.Fhir.Model;

namespace VRDR
{
    /// <summary>Class <c>DeathRecordSubmission</c> supports the submission of VRDR records.</summary>
    public class DeathRecordSubmission : BaseMessage
    {
        /// <summary>Bundle that contains the message payload.</summary>
        private DeathRecord Payload;

        /// <summary>Default constructor that creates a new, empty DeathRecordSubmission.</summary>
        public DeathRecordSubmission() : base("vrdr_submission")
        {
        }

        /// <summary>Constructor that takes a VRDR.DeathRecord and wraps it in a DeathRecordSubmission.</summary>
        /// <param name="record">the VRDR.DeathRecord to create a DeathRecordSubmission for.</param>
        public DeathRecordSubmission(DeathRecord record) : this()
        {
            MessagePayload = record;
        }

        /// <summary>Constructor that takes a string that represents a DeathRecordSubmission message in either XML or JSON format.</summary>
        /// <param name="message">represents a DeathRecordSubmission message in either XML or JSON format.</param>
        /// <param name="permissive">if the parser should be permissive when parsing the given string</param>
        /// <exception cref="ArgumentException">Message is neither valid XML nor JSON.</exception>
        public DeathRecordSubmission(string message, bool permissive = false) : base(message, permissive)
        {
        }

        /// <summary>Message payload</summary>
        /// <value>the message payload as a FHIR Bundle.</value>
        public DeathRecord MessagePayload
        {
            get
            {
                return Payload;
            }
            set
            {
                Payload = value;
                MessageBundle.Entry.RemoveAll( entry => entry.Resource.ResourceType == ResourceType.Bundle );
                MessageBundle.AddResourceEntry(Payload.GetBundle(), "urn:uuid:" + Payload.GetBundle().Id);
                Header.Focus.Clear();
                Header.Focus.Add(new ResourceReference(Payload.GetBundle().Id));
            }
        }

        /// <summary>Restores class references from a newly parsed record.</summary>
        protected override void RestoreReferences()
        {
            base.RestoreReferences();

            // Grab Payload
            var payloadEntry = MessageBundle.Entry.FirstOrDefault( entry => entry.Resource.ResourceType == ResourceType.Bundle );
            if (payloadEntry != null)
            {
                Payload = new DeathRecord((Bundle)payloadEntry.Resource);
            }
        }
    }

    /// <summary>Class <c>DeathRecordUpdate</c> supports the update of VRDR records.</summary>
    public class DeathRecordUpdate : DeathRecordSubmission
    {
        /// <summary>Default constructor that creates a new, empty DeathRecordUpdate.</summary>
        public DeathRecordUpdate() : base()
        {
            MessageType = "vrdr_submission_update";
        }

        /// <summary>Constructor that takes a VRDR.DeathRecord and wraps it in a DeathRecordUpdate.</summary>
        /// <param name="record">the VRDR.DeathRecord to create a DeathRecordUpdate for.</param>
        public DeathRecordUpdate(DeathRecord record) : base(record)
        {
            MessageType = "vrdr_submission_update";
        }

        /// <summary>Constructor that takes a string that represents a DeathRecordUpdate message in either XML or JSON format.</summary>
        /// <param name="message">represents a DeathRecordUpdate message in either XML or JSON format.</param>
        /// <param name="permissive">if the parser should be permissive when parsing the given string</param>
        /// <exception cref="ArgumentException">Message is neither valid XML nor JSON.</exception>
        public DeathRecordUpdate(string message, bool permissive = false) : base(message, permissive)
        {
        }
    }
}
