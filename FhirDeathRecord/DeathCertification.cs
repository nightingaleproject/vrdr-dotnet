using System;
using System.Collections.Generic;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;

namespace FhirDeathRecord
{
    /// <summary>Class <c>DeathCertification</c> models a Death Cerfication used in FHIR Vital Records Death Reporting (VRDR) Death
    /// Record. 
    /// </summary>
    [FhirType("Procedure")]
    public class DeathCertification : Procedure
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public DeathCertification() : base()
        {
            Id = Guid.NewGuid().ToString();
            Meta = new Meta
            {
                Profile = new List<string>() { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Death-Certification" }
            };
            Status = EventStatus.Completed;
            Category = new CodeableConcept("http://snomed.info/sct", "103693007", "Diagnostic procedure", null);
            Code = new CodeableConcept("http://snomed.info/sct", "308646001", "Death certification", null);
        }
    }
}