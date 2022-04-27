using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace VRDR
{
    /// <summary>
    /// Models a FHIR Vital Records Death Reporting (VRDR) DemographicCodedContentBundle.
    /// This class was designed to help consume and produce coding responses that follow the
    /// HL7 FHIR Vital Records Death Reporting Implementation Guide, as described at:
    /// http://hl7.org/fhir/us/vrdr and https://github.com/hl7/vrdr. 
    /// 
    /// Placeholder class; to be implemented to support VRDR Messaging IG.
    /// </summary>
    public class DemographicCodedContentBundle
    {
        private readonly Bundle Bundle;

        /// <summary>
        /// Initializes an empty DemographicCodedContentBundle.
        /// </summary>
        public DemographicCodedContentBundle()
        {
            Bundle = new Bundle();
        }

        /// <summary>
        /// Initializes a DemographicCodedContentBundle from a <see cref="Hl7.Fhir.Model.Bundle">FHIR Bundle</see>.
        /// </summary>
        public DemographicCodedContentBundle(Bundle bundle)
        {
            this.Bundle = bundle;
            // TODO: Verify correct type/valid contents?
        }

        /// <summary>
        /// Initializes a DemographicCodedContentBundle by parsing a JSON or XML representation of the bundle.
        /// </summary>
        /// <param name="record">Represents a FHIR DemographicCodedContentBundle in either XML or JSON format.</param>
        /// <param name="permissive">Indicates whether the parser should be permissive when parsing the given string.</param>
        /// <exception cref="ArgumentException">Record is neither valid XML nor JSON.</exception>
        public DemographicCodedContentBundle(string record, bool permissive = false)
        {
            // TODO: Parser implementation
            throw new NotImplementedException("Parsing this from text is not yet implemented.");
        }

        // TODO: Implement relevant properties

        /// <summary>
        /// Gets the FHIR IG type of the content.
        /// </summary>
        /// <returns>A string representation of the profile URL.</returns>
        public string GetContentType()
        {
            return ProfileURL.DemographicCodedContentBundle;
        }

        /// <summary>
        /// Gets the underlying <see cref="Hl7.Fhir.Model.Bundle">Bundle</see> containing the data.
        /// </summary>
        /// <returns>The FHIR Bundle.</returns>
        public Bundle GetBundle()
        {
            return Bundle;
        }

        /// <summary>
        /// Serializes the Bundle to XML.
        /// </summary>
        /// <returns>A string containing the XML representation of the Bundle.</returns>
        public string ToXml()
        {
            return Bundle.ToXml();
        }

        /// <summary>
        /// Serializes the Bundle to JSON.
        /// </summary>
        /// <returns>A string containing the JSON representation of the Bundle.</returns>
        public string ToJson()
        {
            return Bundle.ToJson();
        }
    }
}
