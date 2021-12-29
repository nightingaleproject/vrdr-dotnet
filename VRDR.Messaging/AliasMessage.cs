using System;
using Hl7.Fhir.Model;

namespace VRDR
{
    /// <summary>Class <c>AliasMessage</c> indicates that Alias information is being provided for a previously submitted DeathRecordSubmission message.</summary>
    public class AliasMessage : BaseMessage
    {
        /// <summary>Default constructor that creates a new, empty VoidMessage.</summary>
        public AliasMessage() : base("http://nchs.cdc.gov/vrdr_alias")
        {
        }

        /// <summary>
        /// Construct a VoidMessage from a FHIR Bundle.
        /// </summary>
        /// <param name="messageBundle">a FHIR Bundle that will be used to initialize the VoidMessage</param>
        /// <returns></returns>
        internal AliasMessage(Bundle messageBundle) : base(messageBundle)
        {
        }

        /// <summary>Constructor that takes a VRDR.DeathRecord and creates a message to Alias that record.</summary>
        /// <param name="record">the VRDR.DeathRecord to create a VoidMessage for.</param>
        public AliasMessage(DeathRecord record) : this()
        {
            ExtractBusinessIdentifiers(record);
        }

        // All Alias parameters are included in Parameters.cs and are automatically generated from the Vital Records Messaging IG.

    }
}
