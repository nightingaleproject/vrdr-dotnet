using System;
using System.Collections.Generic;
using System.Linq;
using FhirDeathRecord.Extensions;
using Hl7.Fhir.Model;

namespace FhirDeathRecord.Section
{
        /// <summary>The Funeral Home Director.</summary>    
        public class FuneralHomeDirector
    {
        private PractitionerRole _resource;
        internal PractitionerRole Resource
        {
            get { return _resource; }
            set
            {
                if (value != null)
                    _resource = value;
            }
        }

        /// <summary>
        /// Reference used in Bundle/Composition
        /// </summary>
        public string Url
        {
            get { return "urn:uuid:" + Resource.Id; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public FuneralHomeDirector()
        {
            Resource = new PractitionerRole
            {
                Id = Guid.NewGuid().ToString(),
                Meta = new Meta
                {
                    Profile = new List<string> { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Funeral-Home-Director" }
                }
            };
        }

        /// <summary>
        /// Create Instance
        /// </summary>
        public static FuneralHomeDirector CreateInstance(DeathRecord record = null, 
        string morticianReference = null, 
        string funeralHomeReference = null)
        {
            var source = new FuneralHomeDirector();

            if (morticianReference != null)
                source.Resource.Practitioner = new ResourceReference(morticianReference);
            
            if (funeralHomeReference != null)
                source.Resource.Organization = new ResourceReference(funeralHomeReference);
    
            if (record != null)
            {
                record.Bundle.AddResourceEntry(source.Resource, source.Url);
                record.Composition.Section.First().Entry.Add(new ResourceReference(source.Url));
            }

            return source;
        }
    }
}