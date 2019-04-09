using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.FhirPath;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace FhirDeathRecord
{
    /// <summary>Class <c>DeathRecord</c> models a FHIR Vital Records Death Reporting (VRDR) Death
    /// Record. This class was designed to help consume and produce death records that follow the
    /// HL7 FHIR Vital Records Death Reporting Implementation Guide, as described at:
    /// http://hl7.org/fhir/us/vrdr and https://github.com/hl7/vrdr.
    /// </summary>
    public class DeathRecord
    {
        /// <summary>Useful for navigating around the FHIR Bundle using FHIRPaths.</summary>
        private ITypedElement Navigator;

        /// <summary>Bundle that contains the death record.</summary>
        private Bundle Bundle;

        /// <summary>Composition that described what the Bundle is, as well as keeping references to its contents.</summary>
        private Composition Composition;

        /// <summary>The Decedent.</summary>
        private Patient Decedent;

        /// <summary>The Certifier.</summary>
        private Practitioner Certifier;

        /// <summary>The Certification.</summary>
        private Procedure DeathCertification;

        /// <summary>The Interested Party.</summary>
        private Organization InterestedParty;

        /// <summary>The Manner of Death Observation.</summary>
        private Observation MannerOfDeath;

        /// <summary>Condition Contributing to Death.</summary>
        private Condition ConditionContributingToDeath;

        /// <summary>Cause Of Death Condition Line A.</summary>
        private Condition CauseOfDeathConditionA;

        /// <summary>Cause Of Death Condition Line B.</summary>
        private Condition CauseOfDeathConditionB;

        /// <summary>Cause Of Death Condition Line C.</summary>
        private Condition CauseOfDeathConditionC;

        /// <summary>Cause Of Death Condition Line D.</summary>
        private Condition CauseOfDeathConditionD;

        /// <summary>Cause Of Death Condition Pathway.</summary>
        private List CauseOfDeathConditionPathway;

        /// <summary>Default constructor that creates a new, empty FHIR SDR.</summary>
        public DeathRecord()
        {
            // Start with an empty Bundle.
            Bundle = new Bundle();
            Bundle.Id = "urn:uuid:" + Guid.NewGuid().ToString();
            Bundle.Type = Bundle.BundleType.Document; // By default, Bundle type is "document".
            Bundle.Meta = new Meta();
            string[] bundle_profile = { "http://www.hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Death-Certificate-Document" };
            Bundle.Meta.Profile = bundle_profile;

            // Start with an empty decedent.
            Decedent = new Patient();
            Decedent.Id = "urn:uuid:" + Guid.NewGuid().ToString();
            Decedent.Meta = new Meta();
            string[] decedent_profile = { "http://www.hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Decedent" };
            Decedent.Meta.Profile = decedent_profile;

            // Start with an empty certifier.
            Certifier = new Practitioner();
            Certifier.Id = "urn:uuid:" + Guid.NewGuid().ToString();
            Certifier.Meta = new Meta();
            string[] certifier_profile = { "http://www.hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Certifier" };
            Certifier.Meta.Profile = certifier_profile;

            // Start with an empty certification.
            DeathCertification = new Procedure();
            DeathCertification.Id = "urn:uuid:" + Guid.NewGuid().ToString();
            DeathCertification.Meta = new Meta();
            string[] deathcertification_profile = { "http://www.hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Death-Certification" };
            DeathCertification.Meta.Profile = deathcertification_profile;
            DeathCertification.Status = EventStatus.Completed;
            DeathCertification.Category = new CodeableConcept("http://snomed.info/sct", "103693007", "Diagnostic procedure", null);
            DeathCertification.Code = new CodeableConcept("http://snomed.info/sct", "308646001", "Death certification", null);

            // Start with an empty interested party.
            InterestedParty = new Organization();
            InterestedParty.Id = "urn:uuid:" + Guid.NewGuid().ToString();
            InterestedParty.Meta = new Meta();
            string[] interestedparty_profile = { "http://www.hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Interested-Party" };
            InterestedParty.Meta.Profile = interestedparty_profile;
            InterestedParty.Active = true;

            // Add Composition to bundle. As the record is filled out, new entries will be added to this element.
            Composition = new Composition();
            Composition.Id = "urn:uuid:" + Guid.NewGuid().ToString();
            Composition.Status = CompositionStatus.Final;
            Composition.Meta = new Meta();
            string[] composition_profile = { "http://www.hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Death-Certificate" };
            Composition.Meta.Profile = composition_profile;
            Composition.Type = new CodeableConcept("http://loinc.org", "64297-5", "Death certificate", null);
            Composition.Section.Add(new Composition.SectionComponent());
            Composition.Subject = new ResourceReference(Decedent.Id);
            Composition.Attester.Add(new Composition.AttesterComponent());
            Composition.Attester.First().Party = new ResourceReference(Certifier.Id);
            Composition.Attester.First().ModeElement.Add(new Code<Hl7.Fhir.Model.Composition.CompositionAttestationMode>(Hl7.Fhir.Model.Composition.CompositionAttestationMode.Legal));
            Hl7.Fhir.Model.Composition.EventComponent eventComponent = new Hl7.Fhir.Model.Composition.EventComponent();
            eventComponent.Code.Add(new CodeableConcept("http://snomed.info/sct", "103693007", "Diagnostic procedure", null));
            eventComponent.Detail.Add(new ResourceReference(DeathCertification.Id));
            Composition.Event.Add(eventComponent);
            Bundle.AddResourceEntry(Composition, Composition.Id);

            // Start with an empty Cause of Death Pathway
            CauseOfDeathConditionPathway = new List();
            CauseOfDeathConditionPathway.Id = "urn:uuid:" + Guid.NewGuid().ToString();
            CauseOfDeathConditionPathway.Meta = new Meta();
            string[] causeofdeathconditionpathway_profile = { "http://www.hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Cause-of-Death-Pathway" };
            CauseOfDeathConditionPathway.Meta.Profile = causeofdeathconditionpathway_profile;
            CauseOfDeathConditionPathway.Status = List.ListStatus.Current;
            CauseOfDeathConditionPathway.Mode = Hl7.Fhir.Model.ListMode.Snapshot;
            CauseOfDeathConditionPathway.Source = new ResourceReference(Certifier.Id);
            CauseOfDeathConditionPathway.OrderedBy = new CodeableConcept(null, "priority", null, null);

            // Add references back to the Decedent, Certifier, Certification, etc.
            AddReferenceToComposition(Decedent.Id);
            AddReferenceToComposition(Certifier.Id);
            AddReferenceToComposition(DeathCertification.Id);
            AddReferenceToComposition(InterestedParty.Id);
            AddReferenceToComposition(CauseOfDeathConditionPathway.Id);
            Bundle.AddResourceEntry(Decedent, Decedent.Id);
            Bundle.AddResourceEntry(Certifier, Certifier.Id);
            Bundle.AddResourceEntry(DeathCertification, DeathCertification.Id);
            Bundle.AddResourceEntry(InterestedParty, InterestedParty.Id);
            Bundle.AddResourceEntry(CauseOfDeathConditionPathway, CauseOfDeathConditionPathway.Id);

            // Create a Navigator for this new death record.
            Navigator = Bundle.ToTypedElement();
        }

        /// <summary>Constructor that takes a string that represents a FHIR SDR in either XML or JSON format.</summary>
        /// <param name="record">represents a FHIR SDR in either XML or JSON format.</param>
        /// <param name="permissive">if the parser should be permissive when parsing the given string</param>
        /// <exception cref="ArgumentException">Record is neither valid XML nor JSON.</exception>
        public DeathRecord(string record, bool permissive = false)
        {
            ParserSettings parserSettings = new ParserSettings { AcceptUnknownMembers = permissive,
                                                                 AllowUnrecognizedEnums = permissive,
                                                                 PermissiveParsing = permissive };
            // Check if XML
            if (!String.IsNullOrEmpty(record) && record.TrimStart().StartsWith("<"))
            {
                FhirXmlParser parser = new FhirXmlParser(parserSettings);
                Bundle = parser.Parse<Bundle>(record);
                Navigator = Bundle.ToTypedElement();
            }
            else
            {
                // Assume JSON
                FhirJsonParser parser = new FhirJsonParser(parserSettings);
                Bundle = parser.Parse<Bundle>(record);
                Navigator = Bundle.ToTypedElement();
            }
            // Fill out class instance references
            if (Navigator != null)
            {
                var compositionEntry = Bundle.Entry.FirstOrDefault( entry => entry.Resource.ResourceType == ResourceType.Composition );
                if (compositionEntry != null)
                {
                    Composition = (Composition)compositionEntry.Resource;
                }
                var patientEntry = Bundle.Entry.FirstOrDefault( entry => entry.Resource.ResourceType == ResourceType.Patient );
                if (patientEntry != null)
                {
                    Decedent = (Patient)patientEntry.Resource;
                }
                var practitionerEntry = Bundle.Entry.FirstOrDefault( entry => entry.Resource.ResourceType == ResourceType.Practitioner );
                if (practitionerEntry != null)
                {
                    Certifier = (Practitioner)practitionerEntry.Resource;
                }
                var procedureEntry = Bundle.Entry.FirstOrDefault( entry => entry.Resource.ResourceType == ResourceType.Procedure );
                if (procedureEntry != null)
                {
                    DeathCertification = (Procedure)procedureEntry.Resource;
                }
                var interestedParty = Bundle.Entry.FirstOrDefault( entry => entry.Resource.ResourceType == ResourceType.Organization && ((Organization)entry.Resource).Active != null );
                if (interestedParty != null)
                {
                    InterestedParty = (Organization)interestedParty.Resource;
                }
                var mannerOfDeath = Bundle.Entry.FirstOrDefault( entry => entry.Resource.ResourceType == ResourceType.Observation && ((Observation)entry.Resource).Code.Coding.First().Code == "69449-7" );
                if (mannerOfDeath != null)
                {
                    MannerOfDeath = (Observation)mannerOfDeath.Resource;
                }
                var conditionContributingToDeath = Bundle.Entry.FirstOrDefault( entry => entry.Resource.ResourceType == ResourceType.Condition && ((Condition)entry.Resource).Asserter == null );
                if (conditionContributingToDeath != null)
                {
                    ConditionContributingToDeath = (Condition)conditionContributingToDeath.Resource;
                }
            }
            else
            {
                throw new System.ArgumentException("Record is neither valid XML nor JSON.", "record");
            }
        }

        /// <summary>Helper method to return a XML string representation of this Death Record.</summary>
        /// <returns>a string representation of this Death Record in XML format</returns>
        public string ToXML()
        {
            return Bundle.ToXml();
        }

        /// <summary>Helper method to return a XML string representation of this Death Record.</summary>
        /// <returns>a string representation of this Death Record in XML format</returns>
        public string ToXml()
        {
            return Bundle.ToXml();
        }

        /// <summary>Helper method to return a JSON string representation of this Death Record.</summary>
        /// <returns>a string representation of this Death Record in JSON format</returns>
        public string ToJSON()
        {
            return Bundle.ToJson();
        }

        /// <summary>Helper method to return a JSON string representation of this Death Record.</summary>
        /// <returns>a string representation of this Death Record in JSON format</returns>
        public string ToJson()
        {
            return Bundle.ToJson();
        }

        /// <summary>Helper method to return an ITypedElement of the record bundle.</summary>
        /// <returns>an ITypedElement of the record bundle</returns>
        public ITypedElement GetITypedElement()
        {
            return Navigator;
        }


        /////////////////////////////////////////////////////////////////////////////////
        //
        // Record Properties: Death Certification
        //
        /////////////////////////////////////////////////////////////////////////////////

        /// <summary>Death Record Identifier.</summary>
        /// <value>a record identification string.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.Identifier = "42";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Record identification: {ExampleDeathRecord.Identifier}");</para>
        /// </example>
        public string Identifier
        {
            get
            {
                if (Composition != null && Composition.Identifier != null)
                {
                    return Composition.Identifier.Value;
                }
                return null;
            }
            set
            {
                Identifier identifier = new Identifier();
                identifier.Value = value;
                Composition.Identifier = identifier;
            }
        }

        /// <summary>Death Record Bundle Identifier.</summary>
        /// <value>a record bundle identification string.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.BundleIdentifier = "42";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Record bundle identification: {ExampleDeathRecord.BundleIdentifier}");</para>
        /// </example>
        public string BundleIdentifier
        {
            get
            {
                if (Bundle != null && Bundle.Identifier != null)
                {
                    return Bundle.Identifier.Value;
                }
                return null;
            }
            set
            {
                Identifier identifier = new Identifier();
                identifier.Value = value;
                Bundle.Identifier = identifier;
            }
        }

        /// <summary>Certified time.</summary>
        /// <value>time when the record was certified.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.CertifiedTime = "2019-01-29T16:48:06.4988220-05:00";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Certified at: {ExampleDeathRecord.CertifiedTime}");</para>
        /// </example>
        public string CertifiedTime
        {
            get
            {
                return Composition.Attester.First().Time != null ? Composition.Attester.First().Time : Convert.ToString(DeathCertification.Performed);
            }
            set
            {
                Composition.Attester.First().Time = value;
                DeathCertification.Performed = new FhirString(value);
            }
        }

        /// <summary>Created time.</summary>
        /// <value>time when the record was created.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.CreatedTime = "2019-01-29T16:48:06.4988220-05:00";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Created at: {ExampleDeathRecord.CreatedTime}");</para>
        /// </example>
        public string CreatedTime
        {
            get
            {
                return Composition.Date;
            }
            set
            {
                Composition.Date = value;
            }
        }

        /// <summary>Certifier Role (see http://www.hl7.org/fhir/stu3/valueset-Performer-role.html).</summary>
        /// <value>the certifier role</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; role = new Dictionary&lt;string, string&gt;();</para>
        /// <para>role.Add("code", "309343006");</para>
        /// <para>role.Add("system", "http://snomed.info/sct");</para>
        /// <para>role.Add("display", "Physician");</para>
        /// <para>ExampleDeathRecord.CertifierRole = role;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Certifier Role: {ExampleDeathRecord.CertifierRole['display']}");</para>
        /// </example>
        public Dictionary<string, string> CertifierRole
        {
            get
            {
                Hl7.Fhir.Model.Procedure.PerformerComponent performer = DeathCertification.Performer.FirstOrDefault();
                if (performer != null && performer.Role != null)
                {
                    return CodeableConceptToDict(performer.Role);
                }
                return null;
            }
            set
            {
                Hl7.Fhir.Model.Procedure.PerformerComponent performer = new Hl7.Fhir.Model.Procedure.PerformerComponent();
                performer.Role = DictToCodeableConcept(value);
                performer.Actor = new ResourceReference(Certifier.Id);
                DeathCertification.Performer.Clear();
                DeathCertification.Performer.Add(performer);
            }
        }

        /// <summary>Interested Party Identifier.</summary>
        /// <value>an interested party identification string.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.InterestedPartyIdentifier = "42";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Interested Party identification: {ExampleDeathRecord.InterestedPartyIdentifier}");</para>
        /// </example>
        public string InterestedPartyIdentifier
        {
            get
            {
                if (InterestedParty != null && InterestedParty.Identifier != null && InterestedParty.Identifier.Count() > 0)
                {
                    return InterestedParty.Identifier.FirstOrDefault().Value;
                }
                return null;
            }
            set
            {
                Identifier identifier = new Identifier();
                identifier.Value = value;
                InterestedParty.Identifier.Clear();
                InterestedParty.Identifier.Add(identifier);
            }
        }

        /// <summary>Interested Party Name.</summary>
        /// <value>an interested party name string.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.InterestedPartyName = "Fourty Two";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Interested Party name: {ExampleDeathRecord.InterestedPartyName}");</para>
        /// </example>
        public string InterestedPartyName
        {
            get
            {
                if (InterestedParty != null && InterestedParty.Name != null)
                {
                    return InterestedParty.Name;
                }
                return null;
            }
            set
            {
                InterestedParty.Name = value;
            }
        }

        /// <summary>Interested Party's address.</summary>
        /// <value>Interested Party's address. A Dictionary representing an address, containing the following key/value pairs:
        /// <para>"addressLine1" - address, line one</para>
        /// <para>"addressLine2" - address, line two</para>
        /// <para>"addressCity" - address, city</para>
        /// <para>"addressCounty" - address, county</para>
        /// <para>"addressState" - address, state</para>
        /// <para>"addressZip" - address, zip</para>
        /// <para>"addressCountry" - address, country</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; address = new Dictionary&lt;string, string&gt;();</para>
        /// <para>address.Add("addressLine1", "9 Example Street");</para>
        /// <para>address.Add("addressLine2", "Line 2");</para>
        /// <para>address.Add("addressCity", "Bedford");</para>
        /// <para>address.Add("addressCounty", "Middlesex");</para>
        /// <para>address.Add("addressState", "Massachusetts");</para>
        /// <para>address.Add("addressZip", "01730");</para>
        /// <para>address.Add("addressCountry", "United States");</para>
        /// <para>ExampleDeathRecord.InterestedPartyAddress = address;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Interested Party state: {ExampleDeathRecord.InterestedPartyAddress["addressState"]}");</para>
        /// </example>
        public Dictionary<string, string> InterestedPartyAddress
        {
            get
            {
                if (InterestedParty != null && InterestedParty.Address != null && InterestedParty.Address.Count() > 0)
                {
                    return AddressToDict(InterestedParty.Address.First());
                }
                return null;
            }
            set
            {
                InterestedParty.Address.Clear();
                InterestedParty.Address.Add(DictToAddress(value));
            }
        }

        /// <summary>Interested Party Type (see https://www.hl7.org/fhir/valueset-organization-type.html).</summary>
        /// <value>the interested party type</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; type = new Dictionary&lt;string, string&gt;();</para>
        /// <para>type.Add("code", "prov");</para>
        /// <para>type.Add("system", "http://terminology.hl7.org/CodeSystem/organization-type");</para>
        /// <para>type.Add("display", "Healthcare Provider");</para>
        /// <para>ExampleDeathRecord.InterestedPartyType = type;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Interested Party Type: {ExampleDeathRecord.InterestedPartyType['display']}");</para>
        /// </example>
        public Dictionary<string, string> InterestedPartyType
        {
            get
            {
                if (InterestedParty != null && InterestedParty.Type != null && InterestedParty.Type.Count() > 0)
                {
                    return CodeableConceptToDict(InterestedParty.Type.First());
                }
                return null;
            }
            set
            {
                InterestedParty.Type.Clear();
                InterestedParty.Type.Add(DictToCodeableConcept(value));
            }
        }

        /// <summary>Manner of Death Type.</summary>
        /// <value>the manner of death type</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; manner = new Dictionary&lt;string, string&gt;();</para>
        /// <para>manner.Add("code", "7878000");</para>
        /// <para>manner.Add("system", "");</para>
        /// <para>manner.Add("display", "Accident");</para>
        /// <para>ExampleDeathRecord.MannerOfDeathType = manner;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Manner Of Death Type: {ExampleDeathRecord.MannerOfDeathType['display']}");</para>
        /// </example>
        public Dictionary<string, string> MannerOfDeathType
        {
            get
            {
                if (MannerOfDeath != null && MannerOfDeath.Value != null)
                {
                    return CodeableConceptToDict((CodeableConcept)MannerOfDeath.Value);
                }
                return null;
            }
            set
            {
                if (MannerOfDeath == null)
                {
                    MannerOfDeath = new Observation();
                    MannerOfDeath.Id = "urn:uuid:" + Guid.NewGuid().ToString();
                    MannerOfDeath.Meta = new Meta();
                    string[] mannerofdeath_profile = { "http://www.hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Manner-of-Death" };
                    MannerOfDeath.Meta.Profile = mannerofdeath_profile;
                    MannerOfDeath.Status = ObservationStatus.Final;
                    MannerOfDeath.Code = new CodeableConcept("http://loinc.org", "69449-7", "Manner of death", null);
                    MannerOfDeath.Subject = new ResourceReference(Decedent.Id);
                    MannerOfDeath.Performer.Add(new ResourceReference(Certifier.Id));
                    MannerOfDeath.Value = DictToCodeableConcept(value);
                    AddReferenceToComposition(MannerOfDeath.Id);
                    Bundle.AddResourceEntry(MannerOfDeath, MannerOfDeath.Id);
                }
            }
        }

        /// <summary>Given name(s) of certifier.</summary>
        /// <value>the certifier's name (first, middle, etc.)</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>string[] names = { "Doctor", "Middle" };</para>
        /// <para>ExampleDeathRecord.CertifierGivenNames = names;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Certifier Given Name(s): {string.Join(", ", ExampleDeathRecord.CertifierGivenNames)}");</para>
        /// </example>
        public string[] CertifierGivenNames
        {
            get
            {
                return GetAllString("Bundle.entry.resource.where($this is Practitioner).name.where(use='official').given");
            }
            set
            {
                HumanName name = Certifier.Name.SingleOrDefault(n => n.Use == HumanName.NameUse.Official);
                if (name != null)
                {
                    name.Given = value;
                }
                else
                {
                    name = new HumanName();
                    name.Use = HumanName.NameUse.Official;
                    name.Given = value;
                    Certifier.Name.Add(name);
                }
            }
        }

        /// <summary>Family name of certifier.</summary>
        /// <value>the certifier's family name (i.e. last name)</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.CertifierFamilyName = "Last";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Certifier's Last Name: {string.Join(", ", ExampleDeathRecord.CertifierFamilyName)}");</para>
        /// </example>
        public string CertifierFamilyName
        {
            get
            {
                return GetFirstString("Bundle.entry.resource.where($this is Practitioner).name.where(use='official').family");
            }
            set
            {
                HumanName name = Certifier.Name.FirstOrDefault();
                if (name != null && !String.IsNullOrEmpty(value))
                {
                    name.Family = value;
                }
                else if (!String.IsNullOrEmpty(value))
                {
                    name = new HumanName();
                    name.Use = HumanName.NameUse.Official;
                    name.Family = value;
                    Certifier.Name.Add(name);
                }
            }
        }

        /// <summary>Certifier's Suffix.</summary>
        /// <value>the certifier's suffix</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.CertifierSuffix = "Jr.";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Certifier Suffix: {ExampleDeathRecord.CertifierSuffix}");</para>
        /// </example>
        public string CertifierSuffix
        {
            get
            {
                return GetFirstString("Bundle.entry.resource.where($this is Practitioner).name.suffix");
            }
            set
            {
                HumanName name = Certifier.Name.FirstOrDefault();
                if (name != null && !String.IsNullOrEmpty(value))
                {
                    string[] suffix = { value };
                    name.Suffix = suffix;
                }
                else if (!String.IsNullOrEmpty(value))
                {
                    name = new HumanName();
                    name.Use = HumanName.NameUse.Official;
                    string[] suffix = { value };
                    name.Suffix = suffix;
                    Certifier.Name.Add(name);
                }
            }
        }

        /// <summary>Certifier's Address.</summary>
        /// <value>the certifier's address. A Dictionary representing an address, containing the following key/value pairs:
        /// <para>"addressLine1" - address, line one</para>
        /// <para>"addressLine2" - address, line two</para>
        /// <para>"addressCity" - address, city</para>
        /// <para>"addressCounty" - address, county</para>
        /// <para>"addressState" - address, state</para>
        /// <para>"addressZip" - address, zip</para>
        /// <para>"addressCountry" - address, country</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; address = new Dictionary&lt;string, string&gt;();</para>
        /// <para>address.Add("addressLine1", "123 Test Street");</para>
        /// <para>address.Add("addressLine2", "Unit 3");</para>
        /// <para>address.Add("addressCity", "Boston");</para>
        /// <para>address.Add("addressCounty", "Suffolk");</para>
        /// <para>address.Add("addressState", "Massachusetts");</para>
        /// <para>address.Add("addressCountry", "United States");</para>
        /// <para>address.Add("addressZip", "12345");</para>
        /// <para>ExampleDeathRecord.CertifierAddress = address;</para>
        /// <para>// Getter:</para>
        /// <para>foreach(var pair in ExampleDeathRecord.CertifierAddress)</para>
        /// <para>{</para>
        /// <para>      Console.WriteLine($"\tCertifierAddress key: {pair.Key}: value: {pair.Value}");</para>
        /// <para>};</para>
        /// </example>
        public Dictionary<string, string> CertifierAddress
        {
            get
            {
                return AddressToDict(Certifier.Address.FirstOrDefault());
            }
            set
            {
                Certifier.Address.Clear();
                Certifier.Address.Add(DictToAddress(value));
            }
        }

        /// <summary>Certifier Qualification.</summary>
        /// <value>the certifier qualification</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; qualification = new Dictionary&lt;string, string&gt;();</para>
        /// <para>qualification.Add("code", "MD");</para>
        /// <para>qualification.Add("system", "http://hl7.org/fhir/v2/0360/2.7");</para>
        /// <para>qualification.Add("display", "Doctor of Medicine");</para>
        /// <para>ExampleDeathRecord.CertifierQualification = qualification;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"\tCertifier Qualification: {ExampleDeathRecord.CertifierQualification['display']}");</para>
        /// </example>
        public Dictionary<string, string> CertifierQualification
        {
            get
            {
                Practitioner.QualificationComponent qualification = Certifier.Qualification.FirstOrDefault();
                if (qualification != null)
                {
                    Dictionary<string, string> dictionary = new Dictionary<string, string>();
                    dictionary.Add("display", ((qualification != null && qualification.Code.Coding.FirstOrDefault() != null) ? qualification.Code.Coding.FirstOrDefault().Display : ""));
                    dictionary.Add("code", ((qualification != null && qualification.Code.Coding.FirstOrDefault() != null) ? qualification.Code.Coding.FirstOrDefault().Code : ""));
                    dictionary.Add("system", ((qualification != null && qualification.Code.Coding.FirstOrDefault() != null) ? qualification.Code.Coding.FirstOrDefault().System : ""));
                    return dictionary;
                }
                return null;
            }
            set
            {
                Practitioner.QualificationComponent qualification = new Practitioner.QualificationComponent();
                qualification.Code = DictToCodeableConcept(value);
                Certifier.Qualification.Clear();
                Certifier.Qualification.Add(qualification);
            }
        }

        /// <summary>A significant condition that contributed to death but did not result in the underlying cause
        /// captured by a CauseOfDeathCondition. Corresponds to part 2 of item 32 of the U.S. Standard Certificate of Death.</summary>
        /// <value>A significant condition that contributed to death but did not result in the underlying cause
        /// captured by a CauseOfDeathCondition.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.ContributingConditions = "Example Contributing Condition";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Cause: {ExampleDeathRecord.ContributingConditions}");</para>
        /// </example>
        public string ContributingConditions
        {
            get
            {
                if (ConditionContributingToDeath != null && ConditionContributingToDeath.Code != null && ConditionContributingToDeath.Code.Text != null)
                {
                    return ConditionContributingToDeath.Code.Text;
                }
                return null;
            }
            set
            {
                if (ConditionContributingToDeath != null)
                {
                    ConditionContributingToDeath.Code.Text = value;
                }
                else
                {
                    ConditionContributingToDeath = new Condition();
                    ConditionContributingToDeath.Id = "urn:uuid:" + Guid.NewGuid().ToString();
                    ConditionContributingToDeath.Subject = new ResourceReference(Decedent.Id);
                    ConditionContributingToDeath.Meta = new Meta();
                    string[] condition_profile = { "http://www.hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Condition-Contributing-To-Death" };
                    ConditionContributingToDeath.Meta.Profile = condition_profile;
                    ConditionContributingToDeath.Code = new CodeableConcept();
                    ConditionContributingToDeath.Code.Text = value;
                    AddReferenceToComposition(ConditionContributingToDeath.Id);
                    Bundle.AddResourceEntry(ConditionContributingToDeath, ConditionContributingToDeath.Id);
                }
            }
        }

        /// <summary>Cause of Death Part I, Line a.</summary>
        public string COD1A
        {
            get
            {
                if (CauseOfDeathConditionA != null && CauseOfDeathConditionA.Code != null)
                {
                    return CauseOfDeathConditionA.Code.Text;
                }
                return null;
            }
            set
            {
                if (CauseOfDeathConditionA != null && CauseOfDeathConditionA.Code != null)
                {
                    CauseOfDeathConditionA.Code.Text = value;
                }
                else
                {
                    CauseOfDeathConditionA = new Condition();
                    CauseOfDeathConditionA.Id = "urn:uuid:" + Guid.NewGuid().ToString();
                    CauseOfDeathConditionA.Meta = new Meta();
                    string[] condition_profile = { "http://www.hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Cause-Of-Death-Condition" };
                    CauseOfDeathConditionA.Meta.Profile = condition_profile;
                    CauseOfDeathConditionA.Code = new CodeableConcept();
                    CauseOfDeathConditionA.Code.Text = value;
                    CauseOfDeathConditionA.Subject = new ResourceReference(Decedent.Id);
                    CauseOfDeathConditionA.Asserter = new ResourceReference(Certifier.Id);
                    AddReferenceToComposition(CauseOfDeathConditionA.Id);
                    Bundle.AddResourceEntry(CauseOfDeathConditionA, CauseOfDeathConditionA.Id);
                    CauseOfDeathConditionPathway.Entry.Add(new List.EntryComponent(new ResourceReference()));
                }
            }
        }

        /// <summary>Cause of Death Part I Interval, Line a.</summary>
        public string INTERVAL1A
        {
            get
            {
                return null;
            }
            set
            {

            }
        }

        /// <summary>Cause of Death Part I Code, Line a.</summary>
        public Dictionary<string, string> CODE1A
        {
            get
            {
                return null;
            }
            set
            {

            }
        }


        /////////////////////////////////////////////////////////////////////////////////
        //
        // Record Properties: Decedent Demographics
        //
        /////////////////////////////////////////////////////////////////////////////////




        /////////////////////////////////////////////////////////////////////////////////
        //
        // Record Properties: Decedent Disposition
        //
        /////////////////////////////////////////////////////////////////////////////////

        // TODO



        /////////////////////////////////////////////////////////////////////////////////
        //
        // Record Properties: Death Investigation
        //
        /////////////////////////////////////////////////////////////////////////////////

        // TODO


        /////////////////////////////////////////////////////////////////////////////////
        //
        // Class helper methods useful for building, searching through records.
        //
        /////////////////////////////////////////////////////////////////////////////////

        /// <summary>Add a reference to the Death Record Composition.</summary>
        /// <param name="reference">a reference.</param>
        private void AddReferenceToComposition(string reference)
        {
            Composition.Section.First().Entry.Add(new ResourceReference(reference));
        }

        /// <summary>Remove a reference from the Death Record Composition.</summary>
        /// <param name="reference">a reference.</param>
        private bool RemoveReferenceFromComposition(string reference)
        {
            return Composition.Section.First().Entry.RemoveAll(entry => entry.Reference == reference) > 0;
        }

        /// <summary>Convert a "code" dictionary to a FHIR CodableConcept.</summary>
        /// <param name="dict">represents a code.</param>
        /// <returns>the corresponding CodeableConcept representation of the code.</returns>
        private CodeableConcept DictToCodeableConcept(Dictionary<string, string> dict)
        {
            CodeableConcept codeableConcept = new CodeableConcept();
            Coding coding = new Coding();
            if (dict != null)
            {
                if (dict.ContainsKey("code") && !String.IsNullOrEmpty(dict["code"]))
                {
                    coding.Code = dict["code"];
                }
                if (dict.ContainsKey("system") && !String.IsNullOrEmpty(dict["system"]))
                {
                    coding.System = dict["system"];
                }
                if (dict.ContainsKey("display") && !String.IsNullOrEmpty(dict["display"]))
                {
                    coding.Display = dict["display"];
                }
            }
            codeableConcept.Coding.Add(coding);
            return codeableConcept;
        }

        /// <summary>Convert a FHIR CodableConcept to a "code" Dictionary</summary>
        /// <param name="codeableConcept">a FHIR CodeableConcept.</param>
        /// <returns>the corresponding Dictionary representation of the code.</returns>
        private Dictionary<string, string> CodeableConceptToDict(CodeableConcept codeableConcept)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            Coding coding = codeableConcept.Coding.First();
            if (coding != null)
            {
                if (!String.IsNullOrEmpty(coding.Code))
                {
                    dictionary.Add("code", coding.Code);
                }
                if (!String.IsNullOrEmpty(coding.System))
                {
                    dictionary.Add("system", coding.System);
                }
                if (!String.IsNullOrEmpty(coding.Display))
                {
                    dictionary.Add("display", coding.Display);
                }
            }
            return dictionary;
        }

        /// <summary>Convert an "address" dictionary to a FHIR Address.</summary>
        /// <param name="dict">represents an address.</param>
        /// <returns>the corresponding FHIR Address representation of the address.</returns>
        private Address DictToAddress(Dictionary<string, string> dict)
        {
            Address address = new Address();
            if (dict != null)
            {
                List<string> lines = new List<string>();
                if (dict.ContainsKey("addressLine1") && !String.IsNullOrEmpty(dict["addressLine1"]))
                {
                    lines.Add(dict["addressLine1"]);
                }
                if (dict.ContainsKey("addressLine2") && !String.IsNullOrEmpty(dict["addressLine2"]))
                {
                    lines.Add(dict["addressLine2"]);
                }
                if (lines.Count() > 0)
                {
                    address.Line = lines.ToArray();
                }
                if (dict.ContainsKey("addressCity") && !String.IsNullOrEmpty(dict["addressCity"]))
                {
                    address.City = dict["addressCity"];
                }
                if (dict.ContainsKey("addressCounty") && !String.IsNullOrEmpty(dict["addressCounty"]))
                {
                    address.District = dict["addressCounty"];
                }
                if (dict.ContainsKey("addressState") && !String.IsNullOrEmpty(dict["addressState"]))
                {
                    address.State = dict["addressState"];
                }
                if (dict.ContainsKey("addressZip") && !String.IsNullOrEmpty(dict["addressZip"]))
                {
                    address.PostalCode = dict["addressZip"];
                }
                if (dict.ContainsKey("addressCountry") && !String.IsNullOrEmpty(dict["addressCountry"]))
                {
                    address.Country = dict["addressCountry"];
                }
            }
            return address;
        }

        /// <summary>Convert a FHIR Address to an "address" Dictionary</summary>
        /// <param name="addr">a FHIR Address.</param>
        /// <returns>the corresponding Dictionary representation of the FHIR Address.</returns>
        private Dictionary<string, string> AddressToDict(Address addr)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            if (addr != null)
            {
                if (addr.Line != null && addr.Line.Count() > 0)
                {
                    dictionary.Add("addressLine1", addr.Line.First());
                }
                if (addr.Line != null && addr.Line.Count() > 1)
                {
                    dictionary.Add("addressLine2", addr.Line.Last());
                }
                if (!String.IsNullOrEmpty(addr.City))
                {
                    dictionary.Add("addressCity", addr.City);
                }
                if (!String.IsNullOrEmpty(addr.District))
                {
                    dictionary.Add("addressCounty", addr.District);
                }
                if (!String.IsNullOrEmpty(addr.State))
                {
                    dictionary.Add("addressState", addr.State);
                }
                if (!String.IsNullOrEmpty(addr.PostalCode))
                {
                    dictionary.Add("addressZip", addr.PostalCode);
                }
                if (!String.IsNullOrEmpty(addr.Country))
                {
                    dictionary.Add("addressCountry", addr.Country);
                }
            }
            return dictionary;
        }

        /// <summary>Given a FHIR path, return the elements that match the given path;
        /// returns an empty array if no matches are found.</summary>
        /// <param name="path">represents a FHIR path.</param>
        /// <returns>all elements that match the given path, or an empty array if no matches are found.</returns>
        public object[] GetAll(string path)
        {
            var matches = Navigator.Select(path);
            ArrayList list = new ArrayList();
            foreach (var match in matches)
            {
                list.Add(match.Value);
            }
            return list.ToArray();
        }

        /// <summary>Given a FHIR path, return the first element that matches the given path.</summary>
        /// <param name="path">represents a FHIR path.</param>
        /// <returns>the first element that matches the given path, or null if no match is found.</returns>
        public object GetFirst(string path)
        {
            var matches = Navigator.Select(path);
            if (matches.Count() > 0)
            {
                return matches.First().Value;
            }
            else
            {
                return null; // Nothing found
            }
        }

        /// <summary>Given a FHIR path, return the last element that matches the given path.</summary>
        /// <param name="path">represents a FHIR path.</param>
        /// <returns>the last element that matches the given path, or null if no match is found.</returns>
        public object GetLast(string path)
        {
            var matches = Navigator.Select(path);
            if (matches.Count() > 0)
            {
                return matches.Last().Value;
            }
            else
            {
                return null; // Nothing found
            }
        }

        /// <summary>Given a FHIR path, return the elements that match the given path as a string;
        /// returns an empty array if no matches are found.</summary>
        /// <param name="path">represents a FHIR path.</param>
        /// <returns>all elements that match the given path as a string, or an empty array if no matches are found.</returns>
        private string[] GetAllString(string path)
        {
            ArrayList list = new ArrayList();
            foreach (var match in GetAll(path))
            {
                list.Add(Convert.ToString(match));
            }
            return list.ToArray(typeof(string)) as string[];
        }

        /// <summary>Given a FHIR path, return the first element that matches the given path as a string;
        /// returns an empty string if no match is found.</summary>
        /// <param name="path">represents a FHIR path.</param>
        /// <returns>the first element that matches the given path as a string, or null if no match is found.</returns>
        private string GetFirstString(string path)
        {
            var first = GetFirst(path);
            if (first != null)
            {
                return Convert.ToString(first);
            }
            else
            {
                return null; // Nothing found
            }
        }

        /// <summary>Given a FHIR path, return the last element that matches the given path as a string;
        /// returns an empty string if no match is found.</summary>
        /// <param name="path">represents a FHIR path.</param>
        /// <returns>the last element that matches the given path as a string, or null if no match is found.</returns>
        private string GetLastString(string path)
        {
            var last = GetLast(path);
            if (last != null)
            {
                return Convert.ToString(last);
            }
            else
            {
                return null; // Nothing found
            }
        }

        /// <summary>Get a value from a Dictionary, but return null if the key doesn't exist.</summary>
        private static string GetValue(Dictionary<string, string> dict, string key)
        {
            string value;
            dict.TryGetValue(key, out value);
            return value;
        }

        /// <summary>Combine the given dictionaries and return the combined result.</summary>
        private static Dictionary<string, string> UpdateDictionary(Dictionary<string, string> a, Dictionary<string, string> b)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            foreach(KeyValuePair<string, string> entry in a)
            {
                dictionary[entry.Key] = entry.Value;
            }
            foreach(KeyValuePair<string, string> entry in b)
            {
                dictionary[entry.Key] = entry.Value;
            }
            return dictionary;
        }

        /// <summary>Returns a JSON encoded structure that maps to the various property
        /// annotations found in the DeathRecord class. This is useful for scenarios
        /// where you may want to display the data in user interfaces.</summary>
        /// <returns>a string representation of this Death Record in a descriptive format.</returns>
        public string ToDescription()
        {
            Dictionary<string, Dictionary<string, dynamic>> description = new Dictionary<string, Dictionary<string, dynamic>>();
            foreach(PropertyInfo property in typeof(DeathRecord).GetProperties().OrderBy(p => ((Property)p.GetCustomAttributes().First()).Priority))
            {
                // Grab property annotation for this property
                Property info = (Property)property.GetCustomAttributes().First();

                // Skip properties that shouldn't be serialized.
                if (!info.Serialize)
                {
                    continue;
                }

                // Add category if it doesn't yet exist
                if (!description.ContainsKey(info.Category))
                {
                    description.Add(info.Category, new Dictionary<string, dynamic>());
                }

                // Add the new property to the category
                Dictionary<string, dynamic> category = description[info.Category];
                category[property.Name] = new Dictionary<string, dynamic>();

                // Add the attributes of the property
                category[property.Name]["Name"] = info.Name;
                category[property.Name]["Type"] = info.Type.ToString();
                category[property.Name]["Description"] = info.Description;

                // Add snippets
                FHIRPath path = (FHIRPath)property.GetCustomAttributes().Last();
                var matches = Navigator.Select(path.Path);
                if (matches.Count() > 0)
                {
                    if (info.Type == Property.Types.TupleCOD)
                    {
                        // Make sure to grab all of the Conditions for COD
                        string xml = "";
                        string json = "";
                        foreach(var match in matches)
                        {
                            xml += match.ToXml();
                            json += match.ToJson() + ",";
                        }
                        category[property.Name]["SnippetXML"] = xml;
                        category[property.Name]["SnippetJSON"] = "[" + json + "]";
                    }
                    else if (!String.IsNullOrWhiteSpace(path.Element))
                    {
                        // Since there is an "Element" for this path, we need to be more
                        // specific about what is included in the snippets.
                        XElement root = XElement.Parse(matches.First().ToXml());
                        XElement node = root.DescendantsAndSelf("{http://hl7.org/fhir}" + path.Element).FirstOrDefault();
                        if (node != null)
                        {
                            node.Name = node.Name.LocalName;
                            category[property.Name]["SnippetXML"] = node.ToString();
                        }
                        else
                        {
                            category[property.Name]["SnippetXML"] = "";
                        }
                         Dictionary<string, dynamic> jsonRoot =
                            JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(matches.First().ToJson(),
                                new JsonSerializerSettings() { DateParseHandling = DateParseHandling.None });
                        if (jsonRoot != null && jsonRoot.Keys.Contains(path.Element))
                        {
                            category[property.Name]["SnippetJSON"] = "{" + $"\"{path.Element}\": \"{jsonRoot[path.Element]}\"" + "}";
                        }
                        else
                        {
                            category[property.Name]["SnippetJSON"] = "";
                        }
                    }
                    else
                    {
                        category[property.Name]["SnippetXML"] = matches.First().ToXml();
                        category[property.Name]["SnippetJSON"] = matches.First().ToJson();
                    }

                }
                else
                {
                    category[property.Name]["SnippetXML"] = "";
                    category[property.Name]["SnippetJSON"] = "";
                }

                // Add the current value of the property
                if (info.Type == Property.Types.Dictionary)
                {
                    // Special case for Dictionary; we want to be able to describe what each key means
                    Dictionary<string, string> value = (Dictionary<string, string>)property.GetValue(this);
                    Dictionary<string, Dictionary<string, string>> moreInfo = new Dictionary<string, Dictionary<string, string>>();
                    foreach (PropertyParam parameter in property.GetCustomAttributes().Reverse().Skip(1).Reverse().Skip(1))
                    {
                        moreInfo[parameter.Key] = new Dictionary<string, string>();
                        moreInfo[parameter.Key]["Description"] = parameter.Description;
                        if (value.ContainsKey(parameter.Key))
                        {
                            moreInfo[parameter.Key]["Value"] = value[parameter.Key];
                        }
                        else
                        {
                            moreInfo[parameter.Key]["Value"] = null;
                        }

                    }
                    category[property.Name]["Value"] = moreInfo;
                }
                else
                {
                    category[property.Name]["Value"] = property.GetValue(this);
                }
            }
            return JsonConvert.SerializeObject(description);
        }

        /// <summary>Helper method to return a JSON string representation of this Death Record.</summary>
        /// <param name="contents">string that represents </param>
        /// <returns>a new DeathRecord that corresponds to the given descriptive format</returns>
        public static DeathRecord FromDescription(string contents)
        {
            DeathRecord record = new DeathRecord();
            Dictionary<string, Dictionary<string, dynamic>> description =
                JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, dynamic>>>(contents,
                    new JsonSerializerSettings() { DateParseHandling = DateParseHandling.None });
            // Loop over each category
            foreach(KeyValuePair<string, Dictionary<string, dynamic>> category in description)
            {
                // Loop over each property
                foreach(KeyValuePair<string, dynamic> property in category.Value)
                {
                    // Set the property on the new DeathRecord based on its type
                    string propertyName = property.Key;
                    Object value = null;
                    if (property.Value["Type"] == Property.Types.String || property.Value["Type"] == Property.Types.StringDateTime)
                    {
                        value = property.Value["Value"].ToString();
                        if (String.IsNullOrWhiteSpace((string)value))
                        {
                            value = null;
                        }
                    }
                    else if (property.Value["Type"] == Property.Types.StringArr)
                    {
                        value = property.Value["Value"].ToObject<String[]>();
                    }
                    else if (property.Value["Type"] == Property.Types.Bool)
                    {
                        value = property.Value["Value"].ToObject<bool>();
                    }
                    else if (property.Value["Type"] == Property.Types.TupleArr)
                    {
                        value = property.Value["Value"].ToObject<Tuple<string, string>[]>();
                    }
                    else if (property.Value["Type"] == Property.Types.TupleCOD)
                    {
                        value = property.Value["Value"].ToObject<Tuple<string, string, Dictionary<string, string>>[]>();
                    }
                    else if (property.Value["Type"] == Property.Types.Dictionary)
                    {
                        Dictionary<string, Dictionary<string, string>> moreInfo =
                            property.Value["Value"].ToObject<Dictionary<string, Dictionary<string, string>>>();
                        Dictionary<string, string> result = new Dictionary<string, string>();
                        foreach(KeyValuePair<string, Dictionary<string, string>> entry in moreInfo)
                        {
                            result[entry.Key] = entry.Value["Value"];
                        }
                        value = result;
                    }
                    if (value != null)
                    {
                        typeof(DeathRecord).GetProperty(propertyName).SetValue(record, value);
                    }
                }
            }
            return record;
        }
    }

    /// <summary>Property attribute used to describe a DeathRecord property.</summary>
    [System.AttributeUsage(System.AttributeTargets.Property)]
    public class Property : System.Attribute
    {
        /// <summary>Enum for describing the property type.</summary>
        public enum Types
        {
            /// <summary>Parameter is a string.</summary>
            String,
            /// <summary>Parameter is an array of strings.</summary>
            StringArr,
            /// <summary>Parameter is like a string, but should be treated as a date and time.</summary>
            StringDateTime,
            /// <summary>Parameter is a bool.</summary>
            Bool,
            /// <summary>Parameter is a Dictionary.</summary>
            Dictionary,
            /// <summary>Parameter is an array of Tuples.</summary>
            TupleArr,
            /// <summary>Parameter is an array of Tuples, specifically for CausesOfDeath.</summary>
            TupleCOD
        };

        /// <summary>Name of this property.</summary>
        public string Name;

        /// <summary>The property type (e.g. string, bool, Dictionary).</summary>
        public Types Type;

        /// <summary>Category of this property.</summary>
        public string Category;

        /// <summary>Description of this property.</summary>
        public string Description;

        /// <summary>If this field should be kept when serialzing.</summary>
        public bool Serialize;

        /// <summary>URL that links to the IG description for this property.</summary>
        public string IGUrl;

        /// <summary>Priority that this should show up in generated lists. Lower numbers come first.</summary>
        public int Priority;

        /// <summary>Constructor.</summary>
        public Property(string name, Types type, string category, string description, bool serialize, string igurl, int priority = 1)
        {
            this.Name = name;
            this.Type = type;
            this.Category = category;
            this.Description = description;
            this.Serialize = serialize;
            this.IGUrl = igurl;
            this.Priority = priority;
        }
    }

    /// <summary>Property attribute used to describe a DeathRecord property parameter,
    /// specifically if the property is a dictionary that has keys.</summary>
    [System.AttributeUsage(System.AttributeTargets.Property, AllowMultiple = true)]
    public class PropertyParam : System.Attribute
    {
        /// <summary>If the related property is a Dictionary, the key name.</summary>
        public string Key;

        /// <summary>Description of this parameter.</summary>
        public string Description;

        /// <summary>Constructor.</summary>
        public PropertyParam(string key, string description)
        {
            this.Key = key;
            this.Description = description;
        }
    }

    /// <summary>Describes a FHIR path that can be used to get to the element.</summary>
    [System.AttributeUsage(System.AttributeTargets.Property)]
    public class FHIRPath : System.Attribute
    {
        /// <summary>The relevant FHIR path.</summary>
        public string Path;

        /// <summary>The relevant element.</summary>
        public string Element;

        /// <summary>Constructor.</summary>
        public FHIRPath(string path, string element)
        {
            this.Path = path;
            this.Element = element;
        }
    }
}
