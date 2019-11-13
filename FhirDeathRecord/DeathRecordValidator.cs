using System;
using System.Collections.Generic;
using System.IO;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Validation;

namespace FhirDeathRecord
{
    /// <summary>Class <c>DeathRecordValidator</c> can be used to validate <c>DeathRecord</c>s. Against the
    /// published Vital Records Death Reporting FHIR Implementation Guide.
    /// </summary>
    public class DeathRecordValidator
    {
        private Validator validator;

        /// <summary>Default constructor.</summary>
        public DeathRecordValidator()
        {
            var zipfileVRDR = Path.Combine("profiles", "VRDR.zip");
            var zipfileBase = Path.Combine("profiles", "definitions.zip");
            var zipfileUS = Path.Combine("profiles", "us-core.zip");
            var ctx = new ValidationSettings()
            {
                ResourceResolver = new CachedResolver( new MultiResolver(new ZipSource(zipfileVRDR, new DirectorySourceSettings() { IncludeSubDirectories = true }),
                                                                         new ZipSource(zipfileBase, new DirectorySourceSettings() { IncludeSubDirectories = true }),
                                                                         new ZipSource(zipfileUS, new DirectorySourceSettings() { IncludeSubDirectories = true }))),
                GenerateSnapshot = true,
            };
            validator = new Validator(ctx);
        }

        /// <summary>Validate the given record.</summary>
        public void Validate(DeathRecord record)
        {
            validator.Validate(record.Decedent);
        }
    }
}