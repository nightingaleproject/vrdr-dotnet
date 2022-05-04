using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace VRDR
{
    /// <summary>
    /// Interface for a FHIR Vital Records Death Reporting (VRDR) CauseOfDeathCodedContentBundle.
    /// This interface was designed to help consume and produce coding responses that follow the
    /// HL7 FHIR Vital Records Death Reporting Implementation Guide, as described at:
    /// http://hl7.org/fhir/us/vrdr and https://github.com/hl7/vrdr.
    /// </summary>
    interface ICauseOfDeathCodedContent
    {
        /// <summary>
        /// Return a CauseOfDeathCodedContentBundle containing the appropriate FHIR Resources.
        /// </summary>
        Bundle GetCauseOfDeathCodedContentBundle();
    }
}
