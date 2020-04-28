using System;
using Hl7.Fhir.Model;

namespace VRDR
{
    /// <summary>Class <c>VoidMessage</c> indicates that a previously submitted DeathRecordSubmission message should be voided.</summary>
    public class VoidMessage : BaseMessage
    {
        private Parameters parameters;

        /// <summary>Default constructor that creates a new, empty VoidMessage.</summary>
        public VoidMessage() : base("http://nchs.cdc.gov/vrdr_submission_void")
        {
            this.parameters = new Parameters();
            this.parameters.Id = Guid.NewGuid().ToString();
            MessageBundle.AddResourceEntry(this.parameters, "urn:uuid:" + this.parameters.Id);
            Header.Focus.Add(new ResourceReference(this.parameters.Id));
        }

        /// <summary>
        /// Construct a VoidMessage from a FHIR Bundle.
        /// </summary>
        /// <param name="messageBundle">a FHIR Bundle that will be used to initialize the VoidMessage</param>
        /// <returns></returns>
        internal VoidMessage(Bundle messageBundle) : base(messageBundle)
        {
            parameters = findEntry<Parameters>(ResourceType.Parameters);
        }

        /// <summary>Constructor that takes a VRDR.DeathRecord and creates a message to void that record.</summary>
        /// <param name="record">the VRDR.DeathRecord to create a VoidMessage for.</param>
        public VoidMessage(DeathRecord record) : this()
        {
            this.CertificateNumber = record?.Identifier;
            this.StateIdentifier = record?.BundleIdentifier;
        }

        /// <summary>Jurisdiction-assigned death certificate number</summary>
        public string CertificateNumber
        {
            get
            {
                return parameters.GetSingleValue<FhirString>("cert_no")?.Value;
            }
            set
            {
                parameters.Remove("cert_no");
                parameters.Add("cert_no", new FhirString(value));
            }
        }

        /// <summary>Jurisdiction-assigned id</summary>
        public string StateIdentifier
        {
            get
            {
                return parameters.GetSingleValue<FhirString>("state_id")?.Value;
            }
            set
            {
                parameters.Remove("state_id");
                parameters.Add("state_id", new FhirString(value));
            }
        }
    }
}
