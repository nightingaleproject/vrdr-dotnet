using Hl7.Fhir.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace VRDR
{
    /// <summary>
    /// A VRDR-augmented wrapper for a FHIR <see cref="Hl7.Fhir.Model.Bundle">Bundle</see>.
    /// </summary>
    public interface VRDRBundle
    {
        /// <summary>
        /// Gets the FHIR IG type of the content.
        /// </summary>
        /// <returns>A string representation of the profile URL.</returns>
        string GetContentType();


        /// <summary>
        /// Gets the underlying <see cref="Hl7.Fhir.Model.Bundle">Bundle</see> containing the data.
        /// </summary>
        /// <returns>The FHIR Bundle.</returns>
        Bundle GetBundle();


        /// <summary>
        /// Serializes the Bundle to XML.
        /// </summary>
        /// <returns>A string containing the XML representation of the Bundle.</returns>
        string ToXml();

        /// <summary>
        /// Serializes the Bundle to JSON.
        /// </summary>
        /// <returns>A string containing the JSON representation of the Bundle.</returns>
        string ToJson();
    }
}
