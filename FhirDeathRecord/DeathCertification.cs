using System;
using System.Collections.Generic;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;

namespace FhirDeathRecord
{
    /// <summary>Class <c>DeathCertification</c> models a Death Cerfication used in FHIR Vital Records Death Reporting (VRDR) Death
    /// Record. 
    /// </summary>
    // public static class DeathCertification
    // {
    //     public static Procedure CreateDeathCertification(this Procedure source)
    //     {
    //         source.Id = Guid.NewGuid().ToString();
    //         source.Meta = new Meta
    //         {
    //             Profile = new List<string>() { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Death-Certification" }
    //         };
    //         source.Status = EventStatus.Completed;
    //         source.Category = new CodeableConcept("http://snomed.info/sct", "103693007", "Diagnostic procedure", null);
    //         source.Code = new CodeableConcept("http://snomed.info/sct", "308646001", "Death certification", null);

    //         return source;
    //     }
    // }
    [FhirType("Procedure")]
    public class DeathCertification : Procedure
    {
        public DeathCertification()
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