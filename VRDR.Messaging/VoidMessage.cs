using System;
using System.Collections.Generic;
using System.Linq;
using Hl7.Fhir.Model;

namespace VRDR
{
    /// <summary>Class <c>VoidMessage</c> indicates that a previously submitted DeathRecordSubmission message should be voided.</summary>
    public class VoidMessage : BaseMessage
    {
        private Parameters Payload;

        /// <summary>Default constructor that creates a new, empty VoidMessage.</summary>
        public VoidMessage() : base("vrdr_submission_void")
        {
            this.Payload = new Parameters();
            this.Payload.Id = Guid.NewGuid().ToString();
            MessageBundle.AddResourceEntry(this.Payload, "urn:uuid:" + this.Payload.Id);
            Header.Focus.Add(new ResourceReference(this.Payload.Id));
        }

        /// <summary>Constructor that takes a VRDR.DeathRecord and creates a message to void that record.</summary>
        /// <param name="record">the VRDR.DeathRecord to create a VoidMessage for.</param>
        public VoidMessage(DeathRecord record) : this()
        {
            this.CertificateNumber = record.Identifier;
            this.StateIdentifier = record.BundleIdentifier;
        }

        /// <summary>Constructor that takes a string that represents a void message in either XML or JSON format.</summary>
        /// <param name="message">represents a void message in either XML or JSON format.</param>
        /// <param name="permissive">if the parser should be permissive when parsing the given string</param>
        /// <exception cref="ArgumentException">Message is neither valid XML nor JSON.</exception>
        public VoidMessage(string message, bool permissive = false) : base(message, permissive)
        {
        }

        /// <summary>Restores class references from a newly parsed record.</summary>
        protected override void RestoreReferences()
        {
            base.RestoreReferences();

            // Grab Payload
            var payloadEntry = MessageBundle.Entry.FirstOrDefault( entry => entry.Resource.ResourceType == ResourceType.Parameters );
            if (payloadEntry != null)
            {
                Payload = (Parameters)payloadEntry.Resource;
            }
        }

        /// <summary>Jurisdiction-assigned death certificate number</summary>
        public string CertificateNumber
        {
            get
            {
                return Payload.GetSingleValue<FhirString>("cert_no")?.Value;
            }
            set
            {
                Payload.Remove("cert_no");
                Payload.Add("cert_no", new FhirString(value));
            }
        }

        /// <summary>Jurisdiction-assigned id</summary>
        public string StateIdentifier
        {
            get
            {
                return Payload.GetSingleValue<FhirString>("state_id")?.Value;
            }
            set
            {
                Payload.Remove("state_id");
                Payload.Add("state_id", new FhirString(value));
            }
        }
    }
}
