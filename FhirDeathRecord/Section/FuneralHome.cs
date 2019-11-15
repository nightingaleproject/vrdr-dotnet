using System;
using System.Collections.Generic;
using System.Linq;
using FhirDeathRecord.Extensions;
using Hl7.Fhir.Model;

namespace FhirDeathRecord.Section
{
    /// <summary>The Funeral Home.</summary>
    public class FuneralHome
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

        /// <summary>Funeral Home Address.</summary>
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

        /// <summary>
        /// Constructor
        /// </summary>
        public FuneralHome()
        {
            Resource = new Organization
            {
                Id = Guid.NewGuid().ToString(),
                Meta = new Meta
                {
                    Profile = new List<string> { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Funeral-Home" }
                },
                Type = new List<CodeableConcept>
                {
                    new CodeableConcept(null, "bus", "Non-Healthcare Business or Corporation", null)
                }
            };
        }

        /// <summary>
        /// Create Instance
        /// </summary>
        public static FuneralHome CreateInstance(DeathRecord record = null)
        {
            var source = new FuneralHome();

            if (record != null)
            {
                record.Bundle.AddResourceEntry(source.Resource, source.Url);
                record.Composition.Section.First().Entry.Add(new ResourceReference(source.Url));
            }

            return source;
        }
    }
}