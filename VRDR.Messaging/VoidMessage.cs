using System;
using Hl7.Fhir.Model;

namespace VRDR
{
    /// <summary>Class <c>VoidMessage</c> indicates that a previously submitted DeathRecordSubmission message should be voided.</summary>
    public class VoidMessage : BaseMessage
    {
        /// <summary>Default constructor that creates a new, empty VoidMessage.</summary>
        public VoidMessage() : base("http://nchs.cdc.gov/vrdr_submission_void")
        {
        }

        /// <summary>
        /// Construct a VoidMessage from a FHIR Bundle.
        /// </summary>
        /// <param name="messageBundle">a FHIR Bundle that will be used to initialize the VoidMessage</param>
        /// <returns></returns>
        internal VoidMessage(Bundle messageBundle) : base(messageBundle)
        {
        }

        /// <summary>Constructor that takes a VRDR.DeathRecord and creates a message to void that record.</summary>
        /// <param name="record">the VRDR.DeathRecord to create a VoidMessage for.</param>
        public VoidMessage(DeathRecord record) : this()
        {
            this.CertificateNumber = record?.Identifier;
            this.StateAuxiliaryIdentifier = record?.StateLocalIdentifier;
            this.NCHSIdentifier = record?.BundleIdentifier;
        }
    }
}
