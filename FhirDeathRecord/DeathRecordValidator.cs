using System;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Validation;
using Hl7.Fhir.Serialization;
using System.Collections.Generic;
using System.IO;

namespace FhirDeathRecord
{
    /// <summary>Validator utility for a <c>DeathRecord</c>.</summary>
    public class DeathRecordValidator
    {
        /// <summary>Validate the given <c>DeathRecord</c>.</summary>
        public void Validate(DeathRecord record)
        {
            Validator validator = new Validator();

            FhirJsonParser parser = new FhirJsonParser();
            FhirXmlParser xmlparser = new FhirXmlParser();

            List<StructureDefinition> structureDefinitions = new List<StructureDefinition>();

            structureDefinitions.Add(xmlparser.Parse<StructureDefinition>(File.ReadAllText("definitions/VRDR_Decedent.json")));
            structureDefinitions.Add(parser.Parse<StructureDefinition>(File.ReadAllText("definitions/Patient.json")));


            var blah = validator.Validate(record.Decedent, structureDefinitions);


        }



    }
}