using System;
using System.Collections.Generic;
using System.Linq;
using FhirDeathRecord.Extensions;
using Hl7.Fhir.Model;

namespace FhirDeathRecord.Section
{
    /// <summary>
    /// Class <c>DeathCertification</c> models the Interested Party used in FHIR Vital Records Death Reporting (VRDR) Death Record. 
    /// </summary>
    public class InterestedParty
    {
        private Organization _resource;
        internal Organization Resource
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

        /// <summary>Interested Party's Identifier.</summary>
        public string Identifier
        {
            get { return Resource.Identifier?.FirstOrDefault()?.Value; }
            set
            {
                Resource.Identifier = new List<Identifier>
                {
                    new Identifier { Value = value }
                };
            }
        }

        /// <summary>Interested Party's Name.</summary>
        public string Name
        {
            get { return Resource.Name; }
            set { Resource.Name = value; }
        }

        /// <summary>Interested Party's Address.</summary>
        public Dictionary<string, string> Address
        {
            get { return Resource.Address?.FirstOrDefault()?.ToDictionary(); }
            set
            {
                if (value == null)
                    return;

                var address = new Address();
                address.FromDictionary(value);

                Resource.Address = new List<Address> { address };
            }
        }

        /// <summary>Interested Party's Type.</summary>
        public Dictionary<string, string> Type
        {
            get { return Resource.Type?.FirstOrDefault()?.ToDictionary(); }
            set
            {
                if (value == null)
                    return;

                var type = new CodeableConcept();
                type.FromDictionary(value);

                Resource.Type = new List<CodeableConcept> { type };
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public InterestedParty()
        {
            Resource = new Organization
            {
                Id = Guid.NewGuid().ToString(),
                Meta = new Meta
                {
                    Profile = new List<string> { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Interested-Party" }
                },
                Active = true
            };
        }

        /// <summary>
        /// Create Instance
        /// </summary>
        public static InterestedParty CreateInstance(DeathRecord record = null)
        {
            var source = new InterestedParty();

            if (record != null)
            {
                record.Bundle.AddResourceEntry(source.Resource, source.Url);
                record.Composition.Section.First().Entry.Add(new ResourceReference(source.Url));
            }

            return source;
        }
    }
}