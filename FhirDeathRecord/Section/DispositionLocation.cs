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
    public class DispositionLocation
    {
        private Location _resource;
        internal Location Resource
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

        /// <summary>Disposition Location's Name.</summary>
        public string Name
        {
            get { return Resource.Name; }
            set { Resource.Name = value; }
        }

        /// <summary>Disposition Location's Address.</summary>
        public Dictionary<string, string> Address
        {
            get { return Resource.Address?.ToDictionary(); }
            set
            {
                if (value == null)
                    return;

                Resource.Address = new Hl7.Fhir.Model.Address();
                Resource.Address.FromDictionary(value);
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public DispositionLocation()
        {
            Resource = new Location
            {
                Id = Guid.NewGuid().ToString(),
                Meta = new Meta
                {
                    Profile = new List<string> { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Disposition-Location" }
                }
            };
        }

        /// <summary>
        /// Create Instance
        /// </summary>
        public static DispositionLocation CreateInstance(DeathRecord record = null)
        {
            var source = new DispositionLocation();

            if (record != null)
            {
                record.Bundle.AddResourceEntry(source.Resource, source.Url);
                record.Composition.Section.First().Entry.Add(new ResourceReference(source.Url));
            }

            return source;
        }
    }
}