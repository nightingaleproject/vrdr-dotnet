using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.Linq;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.FhirPath;
using Newtonsoft.Json;

namespace FhirDeathRecord
{
    /// <summary>Class <c>DeathRecord</c> models a FHIR Vital Records Death Reporting (VRDR) Death
    /// Record. This class was designed to help consume and produce death records that follow the
    /// HL7 FHIR Vital Records Death Reporting Implementation Guide, as described at:
    /// http://hl7.org/fhir/us/vrdr and https://github.com/hl7/vrdr.
    /// </summary>
    public class DeathRecord
    {
        /// <summary>Mortality data for code translations.</summary>
        private MortalityData MortalityData = MortalityData.Instance;

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

        /// <summary>The Mortician.</summary>
        private Practitioner Mortician;

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

        /// <summary>Decedent's Father.</summary>
        private RelatedPerson Father;

        /// <summary>Decedent's Mother.</summary>
        private RelatedPerson Mother;

        /// <summary>Decedent's Spouse.</summary>
        private RelatedPerson Spouse;

        /// <summary>Decedent Education Level.</summary>
        private Observation DecedentEducationLevel;

        /// <summary>Birth Record Identifier.</summary>
        private Observation BirthRecordIdentifier;

        /// <summary>Employment History.</summary>
        private Observation EmploymentHistory;

        /// <summary>The Funeral Home.</summary>
        private Organization FuneralHome;

        /// <summary>The Funeral Home Director.</summary>
        private PractitionerRole FuneralHomeDirector;

        /// <summary>Disposition Location.</summary>
        private Location DispositionLocation;

        /// <summary>Disposition Method.</summary>
        private Observation DispositionMethod;

        /// <summary>Autopsy Performed.</summary>
        private Observation AutopsyPerformed;

        /// <summary>Age At Death.</summary>
        private Observation AgeAtDeathObs;

        /// <summary>Decedent Pregnancy Status.</summary>
        private Observation PregnancyObs;

        /// <summary>Transportation Role (in death).</summary>
        private Observation TransportationRoleObs;

        /// <summary>Examiner Contacted.</summary>
        private Observation ExaminerContactedObs;

        /// <summary>Tobacco Use Contributed To Death.</summary>
        private Observation TobaccoUseObs;

        /// <summary>Injury Location.</summary>
        private Location InjuryLocationLoc;

        /// <summary>Injury Incident.</summary>
        private Observation InjuryIncidentObs;

        /// <summary>Death Location.</summary>
        private Location DeathLocationLoc;

        /// <summary>Date Of Death.</summary>
        private Observation DeathDateObs;

        /// <summary>Default constructor that creates a new, empty DeathRecord.</summary>
        public DeathRecord()
        {
            // Start with an empty Bundle.
            Bundle = new Bundle();
            Bundle.Id = "urn:uuid:" + Guid.NewGuid().ToString();
            Bundle.Type = Bundle.BundleType.Document; // By default, Bundle type is "document".
            Bundle.Meta = new Meta();
            string[] bundle_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Death-Certificate-Document" };
            Bundle.Meta.Profile = bundle_profile;

            // Start with an empty decedent.
            Decedent = new Patient();
            Decedent.Id = "urn:uuid:" + Guid.NewGuid().ToString();
            Decedent.Meta = new Meta();
            string[] decedent_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Decedent" };
            Decedent.Meta.Profile = decedent_profile;

            // Start with an empty certifier.
            Certifier = new Practitioner();
            Certifier.Id = "urn:uuid:" + Guid.NewGuid().ToString();
            Certifier.Meta = new Meta();
            string[] certifier_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Certifier" };
            Certifier.Meta.Profile = certifier_profile;

            // Start with an empty mortician.
            Mortician = new Practitioner();
            Mortician.Id = "urn:uuid:" + Guid.NewGuid().ToString();
            Mortician.Meta = new Meta();
            string[] mortician_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Mortician" };
            Mortician.Meta.Profile = mortician_profile;

            // Start with an empty certification.
            DeathCertification = new Procedure();
            DeathCertification.Id = "urn:uuid:" + Guid.NewGuid().ToString();
            DeathCertification.Meta = new Meta();
            string[] deathcertification_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Death-Certification" };
            DeathCertification.Meta.Profile = deathcertification_profile;
            DeathCertification.Status = EventStatus.Completed;
            DeathCertification.Category = new CodeableConcept("http://snomed.info/sct", "103693007", "Diagnostic procedure", null);
            DeathCertification.Code = new CodeableConcept("http://snomed.info/sct", "308646001", "Death certification", null);

            // Start with an empty interested party.
            InterestedParty = new Organization();
            InterestedParty.Id = "urn:uuid:" + Guid.NewGuid().ToString();
            InterestedParty.Meta = new Meta();
            string[] interestedparty_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Interested-Party" };
            InterestedParty.Meta.Profile = interestedparty_profile;
            InterestedParty.Active = true;

            // Start with an empty funeral home.
            FuneralHome = new Organization();
            FuneralHome.Id = "urn:uuid:" + Guid.NewGuid().ToString();
            FuneralHome.Meta = new Meta();
            string[] funeralhome_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Funeral-Home" };
            FuneralHome.Meta.Profile = funeralhome_profile;
            FuneralHome.Type.Add(new CodeableConcept(null, "bus", "Non-Healthcare Business or Corporation", null));

            // FuneralHomeDirector Points to Mortician and FuneralHome
            FuneralHomeDirector = new PractitionerRole();
            FuneralHomeDirector.Id = "urn:uuid:" + Guid.NewGuid().ToString();
            FuneralHomeDirector.Meta = new Meta();
            string[] funeralhomedirector_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Funeral-Home-Director" };
            FuneralHomeDirector.Meta.Profile = funeralhomedirector_profile;
            FuneralHomeDirector.Practitioner = new ResourceReference(Mortician.Id);
            FuneralHomeDirector.Organization = new ResourceReference(FuneralHome.Id);

            // Location of Disposition
            DispositionLocation = new Location();
            DispositionLocation.Id = "urn:uuid:" + Guid.NewGuid().ToString();
            DispositionLocation.Meta = new Meta();
            string[] dispositionlocation_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Disposition-Location" };
            DispositionLocation.Meta.Profile = dispositionlocation_profile;

            // Add Composition to bundle. As the record is filled out, new entries will be added to this element.
            Composition = new Composition();
            Composition.Id = "urn:uuid:" + Guid.NewGuid().ToString();
            Composition.Status = CompositionStatus.Final;
            Composition.Meta = new Meta();
            string[] composition_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Death-Certificate" };
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
            string[] causeofdeathconditionpathway_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Cause-of-Death-Pathway" };
            CauseOfDeathConditionPathway.Meta.Profile = causeofdeathconditionpathway_profile;
            CauseOfDeathConditionPathway.Status = List.ListStatus.Current;
            CauseOfDeathConditionPathway.Mode = Hl7.Fhir.Model.ListMode.Snapshot;
            CauseOfDeathConditionPathway.Source = new ResourceReference(Certifier.Id);
            CauseOfDeathConditionPathway.OrderedBy = new CodeableConcept(null, "priority", null, null);

            // Add references back to the Decedent, Certifier, Certification, etc.
            AddReferenceToComposition(Decedent.Id);
            AddReferenceToComposition(Certifier.Id);
            AddReferenceToComposition(Mortician.Id);
            AddReferenceToComposition(DeathCertification.Id);
            AddReferenceToComposition(InterestedParty.Id);
            AddReferenceToComposition(FuneralHome.Id);
            AddReferenceToComposition(FuneralHomeDirector.Id);
            AddReferenceToComposition(CauseOfDeathConditionPathway.Id);
            AddReferenceToComposition(DispositionLocation.Id);
            Bundle.AddResourceEntry(Decedent, Decedent.Id);
            Bundle.AddResourceEntry(Certifier, Certifier.Id);
            Bundle.AddResourceEntry(Mortician, Mortician.Id);
            Bundle.AddResourceEntry(DeathCertification, DeathCertification.Id);
            Bundle.AddResourceEntry(InterestedParty, InterestedParty.Id);
            Bundle.AddResourceEntry(FuneralHome, FuneralHome.Id);
            Bundle.AddResourceEntry(FuneralHomeDirector, FuneralHomeDirector.Id);
            Bundle.AddResourceEntry(CauseOfDeathConditionPathway, CauseOfDeathConditionPathway.Id);
            Bundle.AddResourceEntry(DispositionLocation, DispositionLocation.Id);

            // Create a Navigator for this new death record.
            Navigator = Bundle.ToTypedElement();
        }

        /// <summary>Constructor that takes a string that represents a FHIR Death Record in either XML or JSON format.</summary>
        /// <param name="record">represents a FHIR Death Record in either XML or JSON format.</param>
        /// <param name="permissive">if the parser should be permissive when parsing the given string</param>
        /// <exception cref="ArgumentException">Record is neither valid XML nor JSON.</exception>
        public DeathRecord(string record, bool permissive = false)
        {
            ParserSettings parserSettings = new ParserSettings { AcceptUnknownMembers = permissive,
                                                                 AllowUnrecognizedEnums = permissive,
                                                                 PermissiveParsing = permissive };
            // XML?
            if (!String.IsNullOrEmpty(record) && record.TrimStart().StartsWith("<"))
            {
                // Grab all errors found by visiting all nodes and report if not permissive
                if (!permissive)
                {
                    List<string> entries = new List<string>();
                    ISourceNode node = FhirXmlNode.Parse(record, new FhirXmlParsingSettings { PermissiveParsing = permissive });
                    foreach (Hl7.Fhir.Utility.ExceptionNotification problem in node.VisitAndCatch())
                    {
                        entries.Add(problem.Message);
                    }
                    if (entries.Count > 0)
                    {
                        throw new System.ArgumentException(String.Join("; ", entries).TrimEnd());
                    }
                }
                // Try Parse
                try
                {
                    FhirXmlParser parser = new FhirXmlParser(parserSettings);
                    Bundle = parser.Parse<Bundle>(record);
                    Navigator = Bundle.ToTypedElement();
                }
                catch (Exception e)
                {
                    throw new System.ArgumentException(e.Message);
                }
            }
            // JSON?
            if (!String.IsNullOrEmpty(record) && record.TrimStart().StartsWith("{"))
            {
                // Grab all errors found by visiting all nodes and report if not permissive
                if (!permissive)
                {
                    List<string> entries = new List<string>();
                    ISourceNode node = FhirJsonNode.Parse(record, "Bundle", new FhirJsonParsingSettings { PermissiveParsing = permissive });
                    foreach (Hl7.Fhir.Utility.ExceptionNotification problem in node.VisitAndCatch())
                    {
                        entries.Add(problem.Message);
                    }
                    if (entries.Count > 0)
                    {
                        throw new System.ArgumentException(String.Join("; ", entries).TrimEnd());
                    }
                }
                // Try Parse
                try
                {
                    FhirJsonParser parser = new FhirJsonParser(parserSettings);
                    Bundle = parser.Parse<Bundle>(record);
                    Navigator = Bundle.ToTypedElement();
                }
                catch (Exception e)
                {
                    throw new System.ArgumentException(e.Message);
                }
            }
            // Fill out class instance references
            if (Navigator != null)
            {
                RestoreReferences();
            }
            else
            {
                throw new System.ArgumentException("The given input does not appear to be a valid XML or JSON FHIR record.");
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
        [Property("Identifier", Property.Types.String, "Death Certification", "Death Record Identifier.", true, "http://hl7.org/fhir/us/vrdr/2019May/DeathCertificate.html", true, 1)]
        [FHIRPath("Bundle.entry.resource.where($this is Composition)", "identifier")]
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
        [Property("Bundle Identifier", Property.Types.String, "Death Certification", "Death Record Bundle Identifier.", true, "http://hl7.org/fhir/us/vrdr/2019May/DeathCertificateDocument.html", true, 1)]
        [FHIRPath("Bundle", "identifier")]
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
        [Property("Certified Time", Property.Types.StringDateTime, "Death Certification", "Certified time.", true, "http://hl7.org/fhir/us/vrdr/2019May/DeathCertificate.html", false, 2)]
        [FHIRPath("Bundle.entry.resource.where($this is Composition).attester", "time")]
        public string CertifiedTime
        {
            get
            {
                return Composition.Attester.First().Time != null ? Composition.Attester.First().Time : Convert.ToString(DeathCertification.Performed);
            }
            set
            {
                if (DeathCertification == null)
                {
                    DeathCertification = new Procedure();
                    DeathCertification.Id = "urn:uuid:" + Guid.NewGuid().ToString();
                    DeathCertification.Meta = new Meta();
                    string[] deathcertification_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Death-Certification" };
                    DeathCertification.Meta.Profile = deathcertification_profile;
                    DeathCertification.Status = EventStatus.Completed;
                    DeathCertification.Category = new CodeableConcept("http://snomed.info/sct", "103693007", "Diagnostic procedure", null);
                    DeathCertification.Code = new CodeableConcept("http://snomed.info/sct", "308646001", "Death certification", null);
                    AddReferenceToComposition(DeathCertification.Id);
                    Bundle.AddResourceEntry(DeathCertification, DeathCertification.Id);
                    Composition.Attester.First().Time = value;
                    DeathCertification.Performed = new FhirDateTime(value);
                }
                else
                {
                    Composition.Attester.First().Time = value;
                    DeathCertification.Performed = new FhirDateTime(value);
                }
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
        [Property("Created Time", Property.Types.StringDateTime, "Death Certification", "Created time.", true, "http://hl7.org/fhir/us/vrdr/2019May/DeathCertificate.html", true, 2)]
        [FHIRPath("Bundle.entry.resource.where($this is Composition)", "date")]
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

        /// <summary>Certifier Role.</summary>
        /// <value>the certifier role. A Dictionary representing a code, containing the following key/value pairs:
        /// <para>"code" - the code</para>
        /// <para>"system" - the code system this code belongs to</para>
        /// <para>"display" - a human readable meaning of the code</para>
        /// </value>
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
        [Property("Certifier Role", Property.Types.Dictionary, "Death Certification", "Certifier Role.", false, "http://hl7.org/fhir/us/vrdr/2019May/DeathCertificate.html", false, 3)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Procedure).where(code.coding.code='308646001')", "performer")]
        public Dictionary<string, string> CertifierRole
        {
            get
            {
                Hl7.Fhir.Model.Procedure.PerformerComponent performer = DeathCertification.Performer.FirstOrDefault();
                if (performer != null && performer.Role != null)
                {
                    return CodeableConceptToDict(performer.Role);
                }
                return new Dictionary<string, string>() { { "code", "" }, { "system", "" }, { "display", "" } };
            }
            set
            {
                if (DeathCertification == null)
                {
                    DeathCertification = new Procedure();
                    DeathCertification.Id = "urn:uuid:" + Guid.NewGuid().ToString();
                    DeathCertification.Meta = new Meta();
                    string[] deathcertification_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Death-Certification" };
                    DeathCertification.Meta.Profile = deathcertification_profile;
                    DeathCertification.Status = EventStatus.Completed;
                    DeathCertification.Category = new CodeableConcept("http://snomed.info/sct", "103693007", "Diagnostic procedure", null);
                    DeathCertification.Code = new CodeableConcept("http://snomed.info/sct", "308646001", "Death certification", null);
                    AddReferenceToComposition(DeathCertification.Id);
                    Bundle.AddResourceEntry(DeathCertification, DeathCertification.Id);
                    Hl7.Fhir.Model.Procedure.PerformerComponent performer = new Hl7.Fhir.Model.Procedure.PerformerComponent();
                    performer.Role = DictToCodeableConcept(value);
                    performer.Actor = new ResourceReference(Certifier.Id);
                    DeathCertification.Performer.Clear();
                    DeathCertification.Performer.Add(performer);
                }
                else
                {
                    Hl7.Fhir.Model.Procedure.PerformerComponent performer = new Hl7.Fhir.Model.Procedure.PerformerComponent();
                    performer.Role = DictToCodeableConcept(value);
                    performer.Actor = new ResourceReference(Certifier.Id);
                    DeathCertification.Performer.Clear();
                    DeathCertification.Performer.Add(performer);
                }
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
        [Property("Interested Party Identifier", Property.Types.String, "Death Certification", "Interested Party Identifier.", true, "http://hl7.org/fhir/us/vrdr/2019May/InterestedParty.html", false, 4)]
        [FHIRPath("Bundle.entry.resource.where($this is Organization).where(meta.profile='http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Interested-Party')", "identifier")]
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
                if (InterestedParty == null)
                {
                    InterestedParty = new Organization();
                    InterestedParty.Id = "urn:uuid:" + Guid.NewGuid().ToString();
                    InterestedParty.Meta = new Meta();
                    string[] interestedparty_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Interested-Party" };
                    InterestedParty.Meta.Profile = interestedparty_profile;
                    InterestedParty.Active = true;
                    Identifier identifier = new Identifier();
                    identifier.Value = value;
                    InterestedParty.Identifier.Add(identifier);
                    AddReferenceToComposition(InterestedParty.Id);
                    Bundle.AddResourceEntry(InterestedParty, InterestedParty.Id);
                }
                else
                {
                    Identifier identifier = new Identifier();
                    identifier.Value = value;
                    InterestedParty.Identifier.Clear();
                    InterestedParty.Identifier.Add(identifier);
                }
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
        [Property("Interested Party Name", Property.Types.String, "Death Certification", "Interested Party Name.", true, "http://hl7.org/fhir/us/vrdr/2019May/InterestedParty.html", false, 4)]
        [FHIRPath("Bundle.entry.resource.where($this is Organization).where(meta.profile='http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Interested-Party')", "name")]
        public string InterestedPartyName
        {
            get
            {
                if (InterestedParty != null)
                {
                    return InterestedParty.Name;
                }
                return null;
            }
            set
            {
                if (InterestedParty == null)
                {
                    InterestedParty = new Organization();
                    InterestedParty.Id = "urn:uuid:" + Guid.NewGuid().ToString();
                    InterestedParty.Meta = new Meta();
                    string[] interestedparty_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Interested-Party" };
                    InterestedParty.Meta.Profile = interestedparty_profile;
                    InterestedParty.Active = true;
                    InterestedParty.Name = value;
                    AddReferenceToComposition(InterestedParty.Id);
                    Bundle.AddResourceEntry(InterestedParty, InterestedParty.Id);
                }
                else
                {
                    InterestedParty.Name = value;
                }
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
        /// <para>address.Add("addressZip", "12345");</para>
        /// <para>address.Add("addressCountry", "United States");</para>
        /// <para>ExampleDeathRecord.InterestedPartyAddress = address;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Interested Party state: {ExampleDeathRecord.InterestedPartyAddress["addressState"]}");</para>
        /// </example>
        [Property("Interested Party Address", Property.Types.Dictionary, "Death Certification", "Interested Party's address.", true, "http://hl7.org/fhir/us/vrdr/2019May/InterestedParty.html", false, 4)]
        [PropertyParam("addressLine1", "address, line one")]
        [PropertyParam("addressLine2", "address, line two")]
        [PropertyParam("addressCity", "address, city")]
        [PropertyParam("addressCounty", "address, county")]
        [PropertyParam("addressState", "address, state")]
        [PropertyParam("addressZip", "address, zip")]
        [PropertyParam("addressCountry", "address, country")]
        [FHIRPath("Bundle.entry.resource.where($this is Organization).where(meta.profile='http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Interested-Party')", "address")]
        public Dictionary<string, string> InterestedPartyAddress
        {
            get
            {
                if (InterestedParty != null && InterestedParty.Address != null && InterestedParty.Address.Count() > 0)
                {
                    return AddressToDict(InterestedParty.Address.First());
                }
                return EmptyAddrDict();
            }
            set
            {
                InterestedParty.Address.Clear();
                InterestedParty.Address.Add(DictToAddress(value));
            }
        }

        /// <summary>Interested Party Type.</summary>
        /// <value>the interested party type. A Dictionary representing a code, containing the following key/value pairs:
        /// <para>"code" - the code</para>
        /// <para>"system" - the code system this code belongs to</para>
        /// <para>"display" - a human readable meaning of the code</para>
        /// </value>
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
        [Property("Interested Party Type", Property.Types.Dictionary, "Death Certification", "Interested Party Type.", true, "http://hl7.org/fhir/us/vrdr/2019May/InterestedParty.html", false, 4)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Organization).where(meta.profile='http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Interested-Party')", "type")]
        public Dictionary<string, string> InterestedPartyType
        {
            get
            {
                if (InterestedParty != null && InterestedParty.Type != null && InterestedParty.Type.Count() > 0)
                {
                    return CodeableConceptToDict(InterestedParty.Type.First());
                }
                return new Dictionary<string, string>() { { "code", "" }, { "system", "" }, { "display", "" } };
            }
            set
            {
                InterestedParty.Type.Clear();
                InterestedParty.Type.Add(DictToCodeableConcept(value));
            }
        }

        /// <summary>Manner of Death Type.</summary>
        /// <value>the manner of death type. A Dictionary representing a code, containing the following key/value pairs:
        /// <para>"code" - the code</para>
        /// <para>"system" - the code system this code belongs to</para>
        /// <para>"display" - a human readable meaning of the code</para>
        /// </value>
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
        [Property("Manner Of Death Type", Property.Types.Dictionary, "Death Certification", "Manner of Death Type.", true, "http://hl7.org/fhir/us/vrdr/2019May/MannerofDeath.html", true, 4)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='69449-7')", "")]
        public Dictionary<string, string> MannerOfDeathType
        {
            get
            {
                if (MannerOfDeath != null && MannerOfDeath.Value != null)
                {
                    return CodeableConceptToDict((CodeableConcept)MannerOfDeath.Value);
                }
                return new Dictionary<string, string>() { { "code", "" }, { "system", "" }, { "display", "" } };
            }
            set
            {
                if (MannerOfDeath == null)
                {
                    MannerOfDeath = new Observation();
                    MannerOfDeath.Id = "urn:uuid:" + Guid.NewGuid().ToString();
                    MannerOfDeath.Meta = new Meta();
                    string[] mannerofdeath_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Manner-of-Death" };
                    MannerOfDeath.Meta.Profile = mannerofdeath_profile;
                    MannerOfDeath.Status = ObservationStatus.Final;
                    MannerOfDeath.Code = new CodeableConcept("http://loinc.org", "69449-7", "Manner of death", null);
                    MannerOfDeath.Subject = new ResourceReference(Decedent.Id);
                    MannerOfDeath.Performer.Add(new ResourceReference(Certifier.Id));
                    MannerOfDeath.Value = DictToCodeableConcept(value);
                    AddReferenceToComposition(MannerOfDeath.Id);
                    Bundle.AddResourceEntry(MannerOfDeath, MannerOfDeath.Id);
                }
                else
                {
                    MannerOfDeath.Value = DictToCodeableConcept(value);
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
        [Property("Certifier Given Names", Property.Types.StringArr, "Death Certification", "Given name(s) of certifier.", true, "http://hl7.org/fhir/us/vrdr/2019May/Certifier.html", true, 4)]
        [FHIRPath("Bundle.entry.resource.where($this is Practitioner).where(meta.profile='http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Certifier')", "name")]
        public string[] CertifierGivenNames
        {
            get
            {
                if (Certifier.Name.Count() > 0)
                {
                    return Certifier.Name.First().Given.ToArray();
                }
                return new string[0];
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
        /// <para>Console.WriteLine($"Certifier's Last Name: {ExampleDeathRecord.CertifierFamilyName}");</para>
        /// </example>
        [Property("Certifier Family Name", Property.Types.String, "Death Certification", "Family name of certifier.", true, "http://hl7.org/fhir/us/vrdr/2019May/Certifier.html", true, 4)]
        [FHIRPath("Bundle.entry.resource.where($this is Practitioner).where(meta.profile='http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Certifier')", "name")]
        public string CertifierFamilyName
        {
            get
            {
                if (Certifier.Name.Count() > 0)
                {
                    return Certifier.Name.First().Family;
                }
                return null;
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
        [Property("Certifier Suffix", Property.Types.String, "Death Certification", "Certifier's Suffix.", true, "http://hl7.org/fhir/us/vrdr/2019May/Certifier.html", true, 4)]
        [FHIRPath("Bundle.entry.resource.where($this is Practitioner).where(meta.profile='http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Certifier')", "name")]
        public string CertifierSuffix
        {
            get
            {
                if (Certifier.Name.Count() > 0 && Certifier.Name.First().Suffix.Count() > 0)
                {
                    return Certifier.Name.First().Suffix.First();
                }
                return null;
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
        /// <para>address.Add("addressZip", "12345");</para>
        /// <para>address.Add("addressCountry", "United States");</para>
        /// <para>ExampleDeathRecord.CertifierAddress = address;</para>
        /// <para>// Getter:</para>
        /// <para>foreach(var pair in ExampleDeathRecord.CertifierAddress)</para>
        /// <para>{</para>
        /// <para>  Console.WriteLine($"\tCertifierAddress key: {pair.Key}: value: {pair.Value}");</para>
        /// <para>};</para>
        /// </example>
        [Property("Certifier Address", Property.Types.Dictionary, "Death Certification", "Certifier's Address.", true, "http://hl7.org/fhir/us/vrdr/2019May/Certifier.html", true, 4)]
        [PropertyParam("addressLine1", "address, line one")]
        [PropertyParam("addressLine2", "address, line two")]
        [PropertyParam("addressCity", "address, city")]
        [PropertyParam("addressCounty", "address, county")]
        [PropertyParam("addressState", "address, state")]
        [PropertyParam("addressZip", "address, zip")]
        [PropertyParam("addressCountry", "address, country")]
        [FHIRPath("Bundle.entry.resource.where($this is Practitioner).where(meta.profile='http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Certifier')", "address")]
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
        /// <value>the certifier qualification. A Dictionary representing a code, containing the following key/value pairs:
        /// <para>"code" - the code</para>
        /// <para>"system" - the code system this code belongs to</para>
        /// <para>"display" - a human readable meaning of the code</para>
        /// </value>
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
        [Property("Certifier Qualification", Property.Types.Dictionary, "Death Certification", "Certifier Qualification.", true, "http://hl7.org/fhir/us/vrdr/2019May/Certifier.html", false, 4)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Practitioner).where(meta.profile='http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Certifier')", "qualification")]
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
                return new Dictionary<string, string>() { { "code", "" }, { "system", "" }, { "display", "" } };
            }
            set
            {
                Practitioner.QualificationComponent qualification = new Practitioner.QualificationComponent();
                qualification.Code = DictToCodeableConcept(value);
                Certifier.Qualification.Clear();
                Certifier.Qualification.Add(qualification);
            }
        }

        /// <summary>Significant conditions that contributed to death but did not result in the underlying cause.
        /// Corresponds to part 2 of item 32 of the U.S. Standard Certificate of Death.</summary>
        /// <value>A string containing the significant conditions that contributed to death but did not result in
        /// the underlying cause captured by a CauseOfDeathCondition.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.ContributingConditions = "Example Contributing Condition";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Cause: {ExampleDeathRecord.ContributingConditions}");</para>
        /// </example>
        [Property("Contributing Conditions", Property.Types.String, "Death Certification", "Significant conditions that contributed to death but did not result in the underlying cause.", true, "http://hl7.org/fhir/us/vrdr/2019May/ConditionContributingtoDeath.html", true, 4)]
        [FHIRPath("Bundle.entry.resource.where($this is Condition).where(onset.empty())", "")]
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
                    string[] condition_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Condition-Contributing-To-Death" };
                    ConditionContributingToDeath.Meta.Profile = condition_profile;
                    ConditionContributingToDeath.Code = new CodeableConcept();
                    ConditionContributingToDeath.Code.Text = value;
                    AddReferenceToComposition(ConditionContributingToDeath.Id);
                    Bundle.AddResourceEntry(ConditionContributingToDeath, ConditionContributingToDeath.Id);
                }
            }
        }

        /// <summary>Conditions that resulted in the cause of death. Corresponds to part 1 of item 32 of the U.S.
        /// Standard Certificate of Death.</summary>
        /// <value>Conditions that resulted in the underlying cause of death. An array of tuples (in the order they would
        /// appear on a death certificate, from top to bottom), each containing the cause of death literal (Tuple "Item1") the
        /// approximate onset to death (Tuple "Item2"), and an (optional) Dictionary representing a code, containing the following key/value pairs:
        /// <para>"code" - the code describing this finding</para>
        /// <para>"system" - the system the given code belongs to</para>
        /// <para>"display" - the human readable display text that corresponds to the given code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Tuple&lt;string, string, Dictionary&lt;string, string&gt;&gt;[] causes =</para>
        /// <para>{</para>
        /// <para>    Tuple.Create("Example Immediate COD", "minutes", new Dictionary&lt;string, string&gt;(){ {"code", "1234"}, {"system", "example"} }),</para>
        /// <para>    Tuple.Create("Example Underlying COD 1", "2 hours", new Dictionary&lt;string, string&gt;()),</para>
        /// <para>    Tuple.Create("Example Underlying COD 2", "6 months", new Dictionary&lt;string, string&gt;()),</para>
        /// <para>    Tuple.Create("Example Underlying COD 3", "15 years", new Dictionary&lt;string, string&gt;())</para>
        /// <para>};</para>
        /// <para>ExampleDeathRecord.CausesOfDeath = causes;</para>
        /// <para>// Getter:</para>
        /// <para>Tuple&lt;string, string&gt;[] causes = ExampleDeathRecord.CausesOfDeath;</para>
        /// <para>foreach (var cause in causes)</para>
        /// <para>{</para>
        /// <para>    Console.WriteLine($"Cause: {cause.Item1}, Onset: {cause.Item2}, Code: {cause.Item3}");</para>
        /// <para>}</para>
        /// </example>
        [Property("Causes Of Death", Property.Types.TupleCOD, "Death Certification", "Conditions that resulted in the cause of death.", true, "http://hl7.org/fhir/us/vrdr/2019May/CauseofDeathPathway.html", true, 4)]
        [FHIRPath("Bundle.entry.resource.where($this is Condition).where(onset.empty().not())", "")]
        public Tuple<string, string, Dictionary<string, string>>[] CausesOfDeath
        {
            get
            {
                List<Tuple<string, string, Dictionary<string, string>>> results = new List<Tuple<string, string, Dictionary<string, string>>>();
                if (!String.IsNullOrEmpty(COD1A) || !String.IsNullOrEmpty(INTERVAL1A) || CODE1A != null)
                {
                    results.Add(Tuple.Create(COD1A, INTERVAL1A, CODE1A));
                }
                if (!String.IsNullOrEmpty(COD1B) || !String.IsNullOrEmpty(INTERVAL1B) || CODE1B != null)
                {
                    results.Add(Tuple.Create(COD1B, INTERVAL1B, CODE1B));
                }
                if (!String.IsNullOrEmpty(COD1C) || !String.IsNullOrEmpty(INTERVAL1C) || CODE1C != null)
                {
                    results.Add(Tuple.Create(COD1C, INTERVAL1C, CODE1C));
                }
                if (!String.IsNullOrEmpty(COD1D) || !String.IsNullOrEmpty(INTERVAL1D) || CODE1D != null)
                {
                    results.Add(Tuple.Create(COD1D, INTERVAL1A, CODE1D));
                }
                return results.ToArray();
            }
            set
            {
                if (value != null)
                {
                    if (value.Length > 0)
                    {
                        COD1A = value[0].Item1;
                        INTERVAL1A = value[0].Item2;
                        CODE1A = value[0].Item3;
                    }
                    if (value.Length > 1)
                    {
                        COD1B = value[1].Item1;
                        INTERVAL1B = value[1].Item2;
                        CODE1B = value[1].Item3;
                    }
                    if (value.Length > 2)
                    {
                        COD1C = value[2].Item1;
                        INTERVAL1C = value[2].Item2;
                        CODE1C = value[2].Item3;
                    }
                    if (value.Length > 3)
                    {
                        COD1D = value[3].Item1;
                        INTERVAL1D = value[3].Item2;
                        CODE1D = value[3].Item3;
                    }
                }
            }
        }

        /// <summary>Cause of Death Part I, Line a.</summary>
        /// <value>the immediate cause of death literal.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.COD1A = "Rupture of myocardium";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Cause: {ExampleDeathRecord.COD1A}");</para>
        /// </example>
        [Property("COD1A", Property.Types.String, "Death Certification", "Cause of Death Part I, Line a.", false, "http://hl7.org/fhir/us/vrdr/2019May/CauseOfDeathCondition.html", false, 4)]
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
                else if (CauseOfDeathConditionA != null)
                {
                    CauseOfDeathConditionA.Code = new CodeableConcept();
                    CauseOfDeathConditionA.Code.Text = value;
                }
                else
                {
                    CauseOfDeathConditionA = new Condition();
                    CauseOfDeathConditionA.Id = "urn:uuid:" + Guid.NewGuid().ToString();
                    CauseOfDeathConditionA.Meta = new Meta();
                    string[] condition_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Cause-Of-Death-Condition" };
                    CauseOfDeathConditionA.Meta.Profile = condition_profile;
                    CauseOfDeathConditionA.Code = new CodeableConcept();
                    CauseOfDeathConditionA.Code.Text = value;
                    CauseOfDeathConditionA.Subject = new ResourceReference(Decedent.Id);
                    CauseOfDeathConditionA.Asserter = new ResourceReference(Certifier.Id);
                    AddReferenceToComposition(CauseOfDeathConditionA.Id);
                    Bundle.AddResourceEntry(CauseOfDeathConditionA, CauseOfDeathConditionA.Id);
                    List.EntryComponent entry = new List.EntryComponent();
                    entry.Item = new ResourceReference(CauseOfDeathConditionA.Id);
                    if (CauseOfDeathConditionPathway.Entry.Count() != 4)
                    {
                        CauseOfDeathConditionPathway.Entry.Add(null);
                        CauseOfDeathConditionPathway.Entry.Add(null);
                        CauseOfDeathConditionPathway.Entry.Add(null);
                        CauseOfDeathConditionPathway.Entry.Add(null);
                    }
                    CauseOfDeathConditionPathway.Entry[0] = entry;
                }
            }
        }

        /// <summary>Cause of Death Part I Interval, Line a.</summary>
        /// <value>the immediate cause of death approximate interval: onset to death.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.INTERVAL1A = "Minutes";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Interval: {ExampleDeathRecord.INTERVAL1A}");</para>
        /// </example>
        [Property("INTERVAL1A", Property.Types.String, "Death Certification", "Cause of Death Part I Interval, Line a.", false, "http://hl7.org/fhir/us/vrdr/2019May/CauseOfDeathCondition.html", false, 4)]
        public string INTERVAL1A
        {
            get
            {
                if (CauseOfDeathConditionA != null && CauseOfDeathConditionA.Onset != null)
                {
                    return CauseOfDeathConditionA.Onset.ToString();
                }
                return null;
            }
            set
            {
                if (CauseOfDeathConditionA != null)
                {
                    CauseOfDeathConditionA.Onset = new FhirString(value);
                }
                else
                {
                    CauseOfDeathConditionA = new Condition();
                    CauseOfDeathConditionA.Id = "urn:uuid:" + Guid.NewGuid().ToString();
                    CauseOfDeathConditionA.Meta = new Meta();
                    string[] condition_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Cause-Of-Death-Condition" };
                    CauseOfDeathConditionA.Meta.Profile = condition_profile;
                    CauseOfDeathConditionA.Onset = new FhirString(value);
                    CauseOfDeathConditionA.Subject = new ResourceReference(Decedent.Id);
                    CauseOfDeathConditionA.Asserter = new ResourceReference(Certifier.Id);
                    AddReferenceToComposition(CauseOfDeathConditionA.Id);
                    Bundle.AddResourceEntry(CauseOfDeathConditionA, CauseOfDeathConditionA.Id);
                    List.EntryComponent entry = new List.EntryComponent();
                    entry.Item = new ResourceReference(CauseOfDeathConditionA.Id);
                    if (CauseOfDeathConditionPathway.Entry.Count() != 4)
                    {
                        CauseOfDeathConditionPathway.Entry.Add(null);
                        CauseOfDeathConditionPathway.Entry.Add(null);
                        CauseOfDeathConditionPathway.Entry.Add(null);
                        CauseOfDeathConditionPathway.Entry.Add(null);
                    }
                    CauseOfDeathConditionPathway.Entry[0] = entry;
                }
            }
        }

        /// <summary>Cause of Death Part I Code, Line a.</summary>
        /// <value>the immediate cause of death coding. A Dictionary representing a code, containing the following key/value pairs:
        /// <para>"code" - the code</para>
        /// <para>"system" - the code system this code belongs to</para>
        /// <para>"display" - a human readable meaning of the code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; code = new Dictionary&lt;string, string&gt;();</para>
        /// <para>code.Add("code", "I21.0");</para>
        /// <para>code.Add("system", "http://hl7.org/fhir/sid/icd-10");</para>
        /// <para>code.Add("display", "Acute transmural myocardial infarction of anterior wall");</para>
        /// <para>ExampleDeathRecord.CODE1A = code;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"\tCause of Death: {ExampleDeathRecord.CODE1A['display']}");</para>
        /// </example>
        [Property("CODE1A", Property.Types.Dictionary, "Death Certification", "Cause of Death Part I Code, Line a.", false, "http://hl7.org/fhir/us/vrdr/2019May/CauseOfDeathCondition.html", false, 4)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        public Dictionary<string, string> CODE1A
        {
            get
            {
                if (CauseOfDeathConditionA != null && CauseOfDeathConditionA.Code != null)
                {
                    return CodeableConceptToDict(CauseOfDeathConditionA.Code);
                }
                return null;
            }
            set
            {
                if (CauseOfDeathConditionA != null && CauseOfDeathConditionA.Code != null)
                {
                    CodeableConcept code = DictToCodeableConcept(value);
                    code.Text = CauseOfDeathConditionA.Code.Text;
                    CauseOfDeathConditionA.Code = code;
                }
                else if (CauseOfDeathConditionA != null)
                {
                    CauseOfDeathConditionA.Code = DictToCodeableConcept(value);
                }
                else
                {
                    CauseOfDeathConditionA = new Condition();
                    CauseOfDeathConditionA.Id = "urn:uuid:" + Guid.NewGuid().ToString();
                    CauseOfDeathConditionA.Meta = new Meta();
                    string[] condition_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Cause-Of-Death-Condition" };
                    CauseOfDeathConditionA.Meta.Profile = condition_profile;
                    CauseOfDeathConditionA.Code = DictToCodeableConcept(value);
                    CauseOfDeathConditionA.Subject = new ResourceReference(Decedent.Id);
                    CauseOfDeathConditionA.Asserter = new ResourceReference(Certifier.Id);
                    AddReferenceToComposition(CauseOfDeathConditionA.Id);
                    Bundle.AddResourceEntry(CauseOfDeathConditionA, CauseOfDeathConditionA.Id);
                    List.EntryComponent entry = new List.EntryComponent();
                    entry.Item = new ResourceReference(CauseOfDeathConditionA.Id);
                    if (CauseOfDeathConditionPathway.Entry.Count() != 4)
                    {
                        CauseOfDeathConditionPathway.Entry.Add(null);
                        CauseOfDeathConditionPathway.Entry.Add(null);
                        CauseOfDeathConditionPathway.Entry.Add(null);
                        CauseOfDeathConditionPathway.Entry.Add(null);
                    }
                    CauseOfDeathConditionPathway.Entry[0] = entry;
                }
            }
        }

        /// <summary>Cause of Death Part I, Line b.</summary>
        /// <value>the first underlying cause of death literal.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.COD1B = "Acute myocardial infarction";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Cause: {ExampleDeathRecord.COD1B}");</para>
        /// </example>
        [Property("COD1B", Property.Types.String, "Death Certification", "Cause of Death Part I, Line b.", false, "http://hl7.org/fhir/us/vrdr/2019May/CauseOfDeathCondition.html", false, 4)]
        public string COD1B
        {
            get
            {
                if (CauseOfDeathConditionB != null && CauseOfDeathConditionB.Code != null)
                {
                    return CauseOfDeathConditionB.Code.Text;
                }
                return null;
            }
            set
            {
                if (CauseOfDeathConditionB != null && CauseOfDeathConditionB.Code != null)
                {
                    CauseOfDeathConditionB.Code.Text = value;
                }
                else if (CauseOfDeathConditionB != null)
                {
                    CauseOfDeathConditionB.Code = new CodeableConcept();
                    CauseOfDeathConditionB.Code.Text = value;
                }
                else
                {
                    CauseOfDeathConditionB = new Condition();
                    CauseOfDeathConditionB.Id = "urn:uuid:" + Guid.NewGuid().ToString();
                    CauseOfDeathConditionB.Meta = new Meta();
                    string[] condition_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Cause-Of-Death-Condition" };
                    CauseOfDeathConditionB.Meta.Profile = condition_profile;
                    CauseOfDeathConditionB.Code = new CodeableConcept();
                    CauseOfDeathConditionB.Code.Text = value;
                    CauseOfDeathConditionB.Subject = new ResourceReference(Decedent.Id);
                    CauseOfDeathConditionB.Asserter = new ResourceReference(Certifier.Id);
                    AddReferenceToComposition(CauseOfDeathConditionB.Id);
                    Bundle.AddResourceEntry(CauseOfDeathConditionB, CauseOfDeathConditionB.Id);
                    List.EntryComponent entry = new List.EntryComponent();
                    entry.Item = new ResourceReference(CauseOfDeathConditionB.Id);
                    if (CauseOfDeathConditionPathway.Entry.Count() != 4)
                    {
                        CauseOfDeathConditionPathway.Entry.Add(null);
                        CauseOfDeathConditionPathway.Entry.Add(null);
                        CauseOfDeathConditionPathway.Entry.Add(null);
                        CauseOfDeathConditionPathway.Entry.Add(null);
                    }
                    CauseOfDeathConditionPathway.Entry[1] = entry;
                }
            }
        }

        /// <summary>Cause of Death Part I Interval, Line b.</summary>
        /// <value>the first underlying cause of death approximate interval: onset to death.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.INTERVAL1B = "6 days";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Interval: {ExampleDeathRecord.INTERVAL1B}");</para>
        /// </example>
        [Property("INTERVAL1B", Property.Types.String, "Death Certification", "Cause of Death Part I Interval, Line b.", false, "http://hl7.org/fhir/us/vrdr/2019May/CauseOfDeathCondition.html", false, 4)]
        public string INTERVAL1B
        {
            get
            {
                if (CauseOfDeathConditionB != null && CauseOfDeathConditionB.Onset != null)
                {
                    return CauseOfDeathConditionB.Onset.ToString();
                }
                return null;
            }
            set
            {
                if (CauseOfDeathConditionB != null)
                {
                    CauseOfDeathConditionB.Onset = new FhirString(value);
                }
                else
                {
                    CauseOfDeathConditionB = new Condition();
                    CauseOfDeathConditionB.Id = "urn:uuid:" + Guid.NewGuid().ToString();
                    CauseOfDeathConditionB.Meta = new Meta();
                    string[] condition_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Cause-Of-Death-Condition" };
                    CauseOfDeathConditionB.Meta.Profile = condition_profile;
                    CauseOfDeathConditionB.Onset = new FhirString(value);
                    CauseOfDeathConditionB.Subject = new ResourceReference(Decedent.Id);
                    CauseOfDeathConditionB.Asserter = new ResourceReference(Certifier.Id);
                    AddReferenceToComposition(CauseOfDeathConditionB.Id);
                    Bundle.AddResourceEntry(CauseOfDeathConditionB, CauseOfDeathConditionB.Id);
                    List.EntryComponent entry = new List.EntryComponent();
                    entry.Item = new ResourceReference(CauseOfDeathConditionB.Id);
                    if (CauseOfDeathConditionPathway.Entry.Count() != 4)
                    {
                        CauseOfDeathConditionPathway.Entry.Add(null);
                        CauseOfDeathConditionPathway.Entry.Add(null);
                        CauseOfDeathConditionPathway.Entry.Add(null);
                        CauseOfDeathConditionPathway.Entry.Add(null);
                    }
                    CauseOfDeathConditionPathway.Entry[1] = entry;
                }
            }
        }

        /// <summary>Cause of Death Part I Code, Line b.</summary>
        /// <value>the first underlying cause of death coding. A Dictionary representing a code, containing the following key/value pairs:
        /// <para>"code" - the code</para>
        /// <para>"system" - the code system this code belongs to</para>
        /// <para>"display" - a human readable meaning of the code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; code = new Dictionary&lt;string, string&gt;();</para>
        /// <para>code.Add("code", "I21.9");</para>
        /// <para>code.Add("system", "http://hl7.org/fhir/sid/icd-10");</para>
        /// <para>code.Add("display", "Acute myocardial infarction, unspecified");</para>
        /// <para>ExampleDeathRecord.CODE1B = code;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"\tCause of Death: {ExampleDeathRecord.CODE1B['display']}");</para>
        /// </example>
        [Property("CODE1B", Property.Types.Dictionary, "Death Certification", "Cause of Death Part I Code, Line b.", false, "http://hl7.org/fhir/us/vrdr/2019May/CauseOfDeathCondition.html", false, 4)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        public Dictionary<string, string> CODE1B
        {
            get
            {
                if (CauseOfDeathConditionB != null && CauseOfDeathConditionB.Code != null)
                {
                    return CodeableConceptToDict(CauseOfDeathConditionB.Code);
                }
                return null;
            }
            set
            {
                if (CauseOfDeathConditionB != null && CauseOfDeathConditionB.Code != null)
                {
                    CodeableConcept code = DictToCodeableConcept(value);
                    code.Text = CauseOfDeathConditionB.Code.Text;
                    CauseOfDeathConditionB.Code = code;
                }
                else if (CauseOfDeathConditionB != null)
                {
                    CauseOfDeathConditionB.Code = DictToCodeableConcept(value);
                }
                else
                {
                    CauseOfDeathConditionB = new Condition();
                    CauseOfDeathConditionB.Id = "urn:uuid:" + Guid.NewGuid().ToString();
                    CauseOfDeathConditionB.Meta = new Meta();
                    string[] condition_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Cause-Of-Death-Condition" };
                    CauseOfDeathConditionB.Meta.Profile = condition_profile;
                    CauseOfDeathConditionB.Code = DictToCodeableConcept(value);
                    CauseOfDeathConditionB.Subject = new ResourceReference(Decedent.Id);
                    CauseOfDeathConditionB.Asserter = new ResourceReference(Certifier.Id);
                    AddReferenceToComposition(CauseOfDeathConditionB.Id);
                    Bundle.AddResourceEntry(CauseOfDeathConditionB, CauseOfDeathConditionB.Id);
                    List.EntryComponent entry = new List.EntryComponent();
                    entry.Item = new ResourceReference(CauseOfDeathConditionB.Id);
                    if (CauseOfDeathConditionPathway.Entry.Count() != 4)
                    {
                        CauseOfDeathConditionPathway.Entry.Add(null);
                        CauseOfDeathConditionPathway.Entry.Add(null);
                        CauseOfDeathConditionPathway.Entry.Add(null);
                        CauseOfDeathConditionPathway.Entry.Add(null);
                    }
                    CauseOfDeathConditionPathway.Entry[1] = entry;
                }
            }
        }

        /// <summary>Cause of Death Part I, Line c.</summary>
        /// <value>the second underlying cause of death literal.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.COD1C = "Coronary artery thrombosis";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Cause: {ExampleDeathRecord.COD1C}");</para>
        /// </example>
        [Property("COD1C", Property.Types.String, "Death Certification", "Cause of Death Part I, Line c.", false, "http://hl7.org/fhir/us/vrdr/2019May/CauseOfDeathCondition.html", false, 4)]
        public string COD1C
        {
            get
            {
                if (CauseOfDeathConditionC != null && CauseOfDeathConditionC.Code != null)
                {
                    return CauseOfDeathConditionC.Code.Text;
                }
                return null;
            }
            set
            {
                if (CauseOfDeathConditionC != null && CauseOfDeathConditionC.Code != null)
                {
                    CauseOfDeathConditionC.Code.Text = value;
                }
                else if (CauseOfDeathConditionC != null)
                {
                    CauseOfDeathConditionC.Code = new CodeableConcept();
                    CauseOfDeathConditionC.Code.Text = value;
                }
                else
                {
                    CauseOfDeathConditionC = new Condition();
                    CauseOfDeathConditionC.Id = "urn:uuid:" + Guid.NewGuid().ToString();
                    CauseOfDeathConditionC.Meta = new Meta();
                    string[] condition_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Cause-Of-Death-Condition" };
                    CauseOfDeathConditionC.Meta.Profile = condition_profile;
                    CauseOfDeathConditionC.Code = new CodeableConcept();
                    CauseOfDeathConditionC.Code.Text = value;
                    CauseOfDeathConditionC.Subject = new ResourceReference(Decedent.Id);
                    CauseOfDeathConditionC.Asserter = new ResourceReference(Certifier.Id);
                    AddReferenceToComposition(CauseOfDeathConditionC.Id);
                    Bundle.AddResourceEntry(CauseOfDeathConditionC, CauseOfDeathConditionC.Id);
                    List.EntryComponent entry = new List.EntryComponent();
                    entry.Item = new ResourceReference(CauseOfDeathConditionC.Id);
                    if (CauseOfDeathConditionPathway.Entry.Count() != 4)
                    {
                        CauseOfDeathConditionPathway.Entry.Add(null);
                        CauseOfDeathConditionPathway.Entry.Add(null);
                        CauseOfDeathConditionPathway.Entry.Add(null);
                        CauseOfDeathConditionPathway.Entry.Add(null);
                    }
                    CauseOfDeathConditionPathway.Entry[2] = entry;
                }
            }
        }

        /// <summary>Cause of Death Part I Interval, Line c.</summary>
        /// <value>the second underlying cause of death approximate interval: onset to death.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.INTERVAL1C = "5 years";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Interval: {ExampleDeathRecord.INTERVAL1C}");</para>
        /// </example>
        [Property("INTERVAL1C", Property.Types.String, "Death Certification", "Cause of Death Part I Interval, Line c.", false, "http://hl7.org/fhir/us/vrdr/2019May/CauseOfDeathCondition.html", false, 4)]
        public string INTERVAL1C
        {
            get
            {
                if (CauseOfDeathConditionC != null && CauseOfDeathConditionC.Onset != null)
                {
                    return CauseOfDeathConditionC.Onset.ToString();
                }
                return null;
            }
            set
            {
                if (CauseOfDeathConditionC != null)
                {
                    CauseOfDeathConditionC.Onset = new FhirString(value);
                }
                else
                {
                    CauseOfDeathConditionC = new Condition();
                    CauseOfDeathConditionC.Id = "urn:uuid:" + Guid.NewGuid().ToString();
                    CauseOfDeathConditionC.Meta = new Meta();
                    string[] condition_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Cause-Of-Death-Condition" };
                    CauseOfDeathConditionC.Meta.Profile = condition_profile;
                    CauseOfDeathConditionC.Onset = new FhirString(value);
                    CauseOfDeathConditionC.Subject = new ResourceReference(Decedent.Id);
                    CauseOfDeathConditionC.Asserter = new ResourceReference(Certifier.Id);
                    AddReferenceToComposition(CauseOfDeathConditionC.Id);
                    Bundle.AddResourceEntry(CauseOfDeathConditionC, CauseOfDeathConditionC.Id);
                    List.EntryComponent entry = new List.EntryComponent();
                    entry.Item = new ResourceReference(CauseOfDeathConditionC.Id);
                    if (CauseOfDeathConditionPathway.Entry.Count() != 4)
                    {
                        CauseOfDeathConditionPathway.Entry.Add(null);
                        CauseOfDeathConditionPathway.Entry.Add(null);
                        CauseOfDeathConditionPathway.Entry.Add(null);
                        CauseOfDeathConditionPathway.Entry.Add(null);
                    }
                    CauseOfDeathConditionPathway.Entry[2] = entry;
                }
            }
        }

        /// <summary>Cause of Death Part I Code, Line c.</summary>
        /// <value>the second underlying cause of death coding. A Dictionary representing a code, containing the following key/value pairs:
        /// <para>"code" - the code</para>
        /// <para>"system" - the code system this code belongs to</para>
        /// <para>"display" - a human readable meaning of the code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; code = new Dictionary&lt;string, string&gt;();</para>
        /// <para>code.Add("code", "I21.9");</para>
        /// <para>code.Add("system", "http://hl7.org/fhir/sid/icd-10");</para>
        /// <para>code.Add("display", "Acute myocardial infarction, unspecified");</para>
        /// <para>ExampleDeathRecord.CODE1C = code;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"\tCause of Death: {ExampleDeathRecord.CODE1C['display']}");</para>
        /// </example>
        [Property("CODE1C", Property.Types.Dictionary, "Death Certification", "Cause of Death Part I Code, Line c.", false, "http://hl7.org/fhir/us/vrdr/2019May/CauseOfDeathCondition.html", false, 4)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        public Dictionary<string, string> CODE1C
        {
            get
            {
                if (CauseOfDeathConditionC != null && CauseOfDeathConditionC.Code != null)
                {
                    return CodeableConceptToDict(CauseOfDeathConditionC.Code);
                }
                return null;
            }
            set
            {
                if (CauseOfDeathConditionC != null && CauseOfDeathConditionC.Code != null)
                {
                    CodeableConcept code = DictToCodeableConcept(value);
                    code.Text = CauseOfDeathConditionC.Code.Text;
                    CauseOfDeathConditionC.Code = code;
                }
                else if (CauseOfDeathConditionC != null)
                {
                    CauseOfDeathConditionC.Code = DictToCodeableConcept(value);
                }
                else
                {
                    CauseOfDeathConditionC = new Condition();
                    CauseOfDeathConditionC.Id = "urn:uuid:" + Guid.NewGuid().ToString();
                    CauseOfDeathConditionC.Meta = new Meta();
                    string[] condition_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Cause-Of-Death-Condition" };
                    CauseOfDeathConditionC.Meta.Profile = condition_profile;
                    CauseOfDeathConditionC.Code = DictToCodeableConcept(value);
                    CauseOfDeathConditionC.Subject = new ResourceReference(Decedent.Id);
                    CauseOfDeathConditionC.Asserter = new ResourceReference(Certifier.Id);
                    AddReferenceToComposition(CauseOfDeathConditionC.Id);
                    Bundle.AddResourceEntry(CauseOfDeathConditionC, CauseOfDeathConditionC.Id);
                    List.EntryComponent entry = new List.EntryComponent();
                    entry.Item = new ResourceReference(CauseOfDeathConditionC.Id);
                    if (CauseOfDeathConditionPathway.Entry.Count() != 4)
                    {
                        CauseOfDeathConditionPathway.Entry.Add(null);
                        CauseOfDeathConditionPathway.Entry.Add(null);
                        CauseOfDeathConditionPathway.Entry.Add(null);
                        CauseOfDeathConditionPathway.Entry.Add(null);
                    }
                    CauseOfDeathConditionPathway.Entry[2] = entry;
                }
            }
        }

        /// <summary>Cause of Death Part I, Line d.</summary>
        /// <value>the third underlying cause of death literal.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.COD1D = "Atherosclerotic coronary artery disease";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Cause: {ExampleDeathRecord.COD1D}");</para>
        /// </example>
        [Property("COD1D", Property.Types.String, "Death Certification", "Cause of Death Part I, Line d.", false, "http://hl7.org/fhir/us/vrdr/2019May/CauseOfDeathCondition.html", false, 4)]
        public string COD1D
        {
            get
            {
                if (CauseOfDeathConditionD != null && CauseOfDeathConditionD.Code != null)
                {
                    return CauseOfDeathConditionD.Code.Text;
                }
                return null;
            }
            set
            {
                if (CauseOfDeathConditionD != null && CauseOfDeathConditionD.Code != null)
                {
                    CauseOfDeathConditionD.Code.Text = value;
                }
                else if (CauseOfDeathConditionD != null)
                {
                    CauseOfDeathConditionD.Code = new CodeableConcept();
                    CauseOfDeathConditionD.Code.Text = value;
                }
                else
                {
                    CauseOfDeathConditionD = new Condition();
                    CauseOfDeathConditionD.Id = "urn:uuid:" + Guid.NewGuid().ToString();
                    CauseOfDeathConditionD.Meta = new Meta();
                    string[] condition_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Cause-Of-Death-Condition" };
                    CauseOfDeathConditionD.Meta.Profile = condition_profile;
                    CauseOfDeathConditionD.Code = new CodeableConcept();
                    CauseOfDeathConditionD.Code.Text = value;
                    CauseOfDeathConditionD.Subject = new ResourceReference(Decedent.Id);
                    CauseOfDeathConditionD.Asserter = new ResourceReference(Certifier.Id);
                    AddReferenceToComposition(CauseOfDeathConditionD.Id);
                    Bundle.AddResourceEntry(CauseOfDeathConditionD, CauseOfDeathConditionD.Id);
                    List.EntryComponent entry = new List.EntryComponent();
                    entry.Item = new ResourceReference(CauseOfDeathConditionD.Id);
                    if (CauseOfDeathConditionPathway.Entry.Count() != 4)
                    {
                        CauseOfDeathConditionPathway.Entry.Add(null);
                        CauseOfDeathConditionPathway.Entry.Add(null);
                        CauseOfDeathConditionPathway.Entry.Add(null);
                        CauseOfDeathConditionPathway.Entry.Add(null);
                    }
                    CauseOfDeathConditionPathway.Entry[3] = entry;
                }
            }
        }

        /// <summary>Cause of Death Part I Interval, Line d.</summary>
        /// <value>the third underlying cause of death approximate interval: onset to death.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.INTERVAL1D = "7 years";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Interval: {ExampleDeathRecord.INTERVAL1D}");</para>
        /// </example>
        [Property("INTERVAL1D", Property.Types.String, "Death Certification", "Cause of Death Part I Interval, Line d.", false, "http://hl7.org/fhir/us/vrdr/2019May/CauseOfDeathCondition.html", false, 4)]
        public string INTERVAL1D
        {
            get
            {
                if (CauseOfDeathConditionD != null && CauseOfDeathConditionD.Onset != null)
                {
                    return CauseOfDeathConditionD.Onset.ToString();
                }
                return null;
            }
            set
            {
                if (CauseOfDeathConditionD != null)
                {
                    CauseOfDeathConditionD.Onset = new FhirString(value);
                }
                else
                {
                    CauseOfDeathConditionD = new Condition();
                    CauseOfDeathConditionD.Id = "urn:uuid:" + Guid.NewGuid().ToString();
                    CauseOfDeathConditionD.Meta = new Meta();
                    string[] condition_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Cause-Of-Death-Condition" };
                    CauseOfDeathConditionD.Meta.Profile = condition_profile;
                    CauseOfDeathConditionD.Onset = new FhirString(value);
                    CauseOfDeathConditionD.Subject = new ResourceReference(Decedent.Id);
                    CauseOfDeathConditionD.Asserter = new ResourceReference(Certifier.Id);
                    AddReferenceToComposition(CauseOfDeathConditionD.Id);
                    Bundle.AddResourceEntry(CauseOfDeathConditionD, CauseOfDeathConditionD.Id);
                    List.EntryComponent entry = new List.EntryComponent();
                    entry.Item = new ResourceReference(CauseOfDeathConditionD.Id);
                    if (CauseOfDeathConditionPathway.Entry.Count() != 4)
                    {
                        CauseOfDeathConditionPathway.Entry.Add(null);
                        CauseOfDeathConditionPathway.Entry.Add(null);
                        CauseOfDeathConditionPathway.Entry.Add(null);
                        CauseOfDeathConditionPathway.Entry.Add(null);
                    }
                    CauseOfDeathConditionPathway.Entry[3] = entry;
                }
            }
        }

        /// <summary>Cause of Death Part I Code, Line d.</summary>
        /// <value>the third underlying cause of death coding. A Dictionary representing a code, containing the following key/value pairs:
        /// <para>"code" - the code</para>
        /// <para>"system" - the code system this code belongs to</para>
        /// <para>"display" - a human readable meaning of the code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; code = new Dictionary&lt;string, string&gt;();</para>
        /// <para>code.Add("code", "I21.9");</para>
        /// <para>code.Add("system", "http://hl7.org/fhir/sid/icd-10");</para>
        /// <para>code.Add("display", "Acute myocardial infarction, unspecified");</para>
        /// <para>ExampleDeathRecord.CODE1D = code;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"\tCause of Death: {ExampleDeathRecord.CODE1D['display']}");</para>
        /// </example>
        [Property("CODE1D", Property.Types.Dictionary, "Death Certification", "Cause of Death Part I Code, Line d.", false, "http://hl7.org/fhir/us/vrdr/2019May/CauseOfDeathCondition.html", false, 4)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        public Dictionary<string, string> CODE1D
        {
            get
            {
                if (CauseOfDeathConditionD != null && CauseOfDeathConditionD.Code != null)
                {
                    return CodeableConceptToDict(CauseOfDeathConditionD.Code);
                }
                return null;
            }
            set
            {
                if (CauseOfDeathConditionD != null && CauseOfDeathConditionD.Code != null)
                {
                    CodeableConcept code = DictToCodeableConcept(value);
                    code.Text = CauseOfDeathConditionD.Code.Text;
                    CauseOfDeathConditionD.Code = code;
                }
                else if (CauseOfDeathConditionD != null)
                {
                    CauseOfDeathConditionD.Code = DictToCodeableConcept(value);
                }
                else
                {
                    CauseOfDeathConditionD = new Condition();
                    CauseOfDeathConditionD.Id = "urn:uuid:" + Guid.NewGuid().ToString();
                    CauseOfDeathConditionD.Meta = new Meta();
                    string[] condition_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Cause-Of-Death-Condition" };
                    CauseOfDeathConditionD.Meta.Profile = condition_profile;
                    CauseOfDeathConditionD.Code = DictToCodeableConcept(value);
                    CauseOfDeathConditionD.Subject = new ResourceReference(Decedent.Id);
                    CauseOfDeathConditionD.Asserter = new ResourceReference(Certifier.Id);
                    AddReferenceToComposition(CauseOfDeathConditionD.Id);
                    Bundle.AddResourceEntry(CauseOfDeathConditionD, CauseOfDeathConditionD.Id);
                    List.EntryComponent entry = new List.EntryComponent();
                    entry.Item = new ResourceReference(CauseOfDeathConditionD.Id);
                    if (CauseOfDeathConditionPathway.Entry.Count() != 4)
                    {
                        CauseOfDeathConditionPathway.Entry.Add(null);
                        CauseOfDeathConditionPathway.Entry.Add(null);
                        CauseOfDeathConditionPathway.Entry.Add(null);
                        CauseOfDeathConditionPathway.Entry.Add(null);
                    }
                    CauseOfDeathConditionPathway.Entry[3] = entry;
                }
            }
        }


        /////////////////////////////////////////////////////////////////////////////////
        //
        // Record Properties: Decedent Demographics
        //
        /////////////////////////////////////////////////////////////////////////////////

        /// <summary>Decedent's Given Name(s). Middle name should be the last entry.</summary>
        /// <value>the decedent's name (first, etc., middle)</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>string[] names = { "Example", "Something", "Middle" };</para>
        /// <para>ExampleDeathRecord.GivenNames = names;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Decedent Given Name(s): {string.Join(", ", ExampleDeathRecord.GivenNames)}");</para>
        /// </example>
        [Property("Given Names", Property.Types.StringArr, "Decedent Demographics", "Decedent's Given Name(s).", true, "http://hl7.org/fhir/us/vrdr/2019May/Decedent.html", true, 2)]
        [FHIRPath("Bundle.entry.resource.where($this is Patient)", "name")]
        public string[] GivenNames
        {
            get
            {
                string[] names = GetAllString("Bundle.entry.resource.where($this is Patient).name.where(use='official').given");
                return names != null ? names : new string[0];
            }
            set
            {
                HumanName name = Decedent.Name.SingleOrDefault(n => n.Use == HumanName.NameUse.Official);
                if (name != null)
                {
                    name.Given = value;
                }
                else
                {
                    name = new HumanName();
                    name.Use = HumanName.NameUse.Official;
                    name.Given = value;
                    Decedent.Name.Add(name);
                }
            }
        }

        /// <summary>Decedent's Family Name.</summary>
        /// <value>the decedent's family name (i.e. last name)</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.FamilyName = "Last";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Decedent's Last Name: {ExampleDeathRecord.FamilyName}");</para>
        /// </example>
        [Property("Family Name", Property.Types.String, "Decedent Demographics", "Decedent's Family Name.", true, "http://hl7.org/fhir/us/vrdr/2019May/Decedent.html", true, 2)]
        [FHIRPath("Bundle.entry.resource.where($this is Patient)", "name")]
        public string FamilyName
        {
            get
            {
                return GetFirstString("Bundle.entry.resource.where($this is Patient).name.where(use='official').family");
            }
            set
            {
                HumanName name = Decedent.Name.SingleOrDefault(n => n.Use == HumanName.NameUse.Official);
                if (name != null && !String.IsNullOrEmpty(value))
                {
                    name.Family = value;
                }
                else if (!String.IsNullOrEmpty(value))
                {
                    name = new HumanName();
                    name.Use = HumanName.NameUse.Official;
                    name.Family = value;
                    Decedent.Name.Add(name);
                }
            }
        }

        /// <summary>Decedent's Family Name.</summary>
        /// <value>the decedent's maiden name (i.e. last name before marriage)</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.MaidenName = "Last";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Decedent's Maiden Name: {ExampleDeathRecord.MaidenName}");</para>
        /// </example>
        [Property("Maiden Name", Property.Types.String, "Decedent Demographics", "Decedent's Maiden Name.", true, "http://hl7.org/fhir/us/vrdr/2019May/Decedent.html", true, 2)]
        [FHIRPath("Bundle.entry.resource.where($this is Patient).name.where(use='maiden')", "family")]
        public string MaidenName
        {
            get
            {
                return GetFirstString("Bundle.entry.resource.where($this is Patient).name.where(use='maiden').family");
            }
            set
            {
                HumanName name = Decedent.Name.SingleOrDefault(n => n.Use == HumanName.NameUse.Maiden);
                if (name != null && !String.IsNullOrEmpty(value))
                {
                    name.Family = value;
                }
                else if (!String.IsNullOrEmpty(value))
                {
                    name = new HumanName();
                    name.Use = HumanName.NameUse.Maiden;
                    name.Family = value;
                    Decedent.Name.Add(name);
                }
            }
        }

        /// <summary>Decedent's Suffix.</summary>
        /// <value>the decedent's suffix</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.Suffix = "Jr.";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Decedent Suffix: {ExampleDeathRecord.Suffix}");</para>
        /// </example>
        [Property("Suffix", Property.Types.String, "Decedent Demographics", "Decedent's Suffix.", true, "http://hl7.org/fhir/us/vrdr/2019May/Decedent.html", true, 2)]
        [FHIRPath("Bundle.entry.resource.where($this is Patient)", "name")]
        public string Suffix
        {
            get
            {
                return GetFirstString("Bundle.entry.resource.where($this is Patient).name.suffix");
            }
            set
            {
                HumanName name = Decedent.Name.SingleOrDefault(n => n.Use == HumanName.NameUse.Official);
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
                    Decedent.Name.Add(name);
                }
            }
        }

        /// <summary>Decedent's Gender.</summary>
        /// <value>the decedent's gender</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.Gender = "female";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Gender: {ExampleDeathRecord.Gender}");</para>
        /// </example>
        [Property("Gender", Property.Types.String, "Decedent Demographics", "Decedent's Gender.", true, "http://hl7.org/fhir/us/vrdr/2019May/Decedent.html", true, 2)]
        [FHIRPath("Bundle.entry.resource.where($this is Patient)", "gender")]
        public string Gender
        {
            get
            {
                return GetFirstString("Bundle.entry.resource.where($this is Patient).gender");
            }
            set
            {
                switch(value)
                {
                    case "male":
                    case "Male":
                    case "m":
                    case "M":
                        Decedent.Gender = AdministrativeGender.Male;
                        break;
                    case "female":
                    case "Female":
                    case "f":
                    case "F":
                        Decedent.Gender = AdministrativeGender.Female;
                        break;
                    case "other":
                    case "Other":
                    case "o":
                    case "O":
                        Decedent.Gender = AdministrativeGender.Other;
                        break;
                    case "unknown":
                    case "Unknown":
                    case "u":
                    case "U":
                        Decedent.Gender = AdministrativeGender.Unknown;
                        break;
                }
            }
        }

        /// <summary>Decedent's Birth Sex.</summary>
        /// <value>the decedent's birth sex A Dictionary representing a code, containing the following key/value pairs:
        /// <para>"code" - the code</para>
        /// <para>"system" - the code system this code belongs to</para>
        /// <para>"display" - a human readable meaning of the code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; code = new Dictionary&lt;string, string&gt;();</para>
        /// <para>code.Add("code", "M");</para>
        /// <para>code.Add("system", "http://hl7.org/fhir/us/core/ValueSet/us-core-birthsex");</para>
        /// <para>code.Add("display", "Male");</para>
        /// <para>ExampleDeathRecord.BirthSex = code;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Birth Sex: {ExampleDeathRecord.BirthSex}");</para>
        /// </example>
        [Property("Birth Sex", Property.Types.Dictionary, "Decedent Demographics", "Decedent's Birth Sex.", true, "http://hl7.org/fhir/us/vrdr/2019May/Decedent.html", true, 2)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Patient).extension.where(url='http://hl7.org/fhir/us/core/StructureDefinition/us-core-birthsex')", "")]
        public Dictionary<string, string> BirthSex
        {
            get
            {
                Dictionary<string, string> birthsex = new Dictionary<string, string>();
                birthsex.Add("code", GetFirstString("Bundle.entry.resource.where($this is Patient).extension.where(url='http://hl7.org/fhir/us/core/StructureDefinition/us-core-birthsex').value.coding.code"));
                birthsex.Add("system", GetFirstString("Bundle.entry.resource.where($this is Patient).extension.where(url='http://hl7.org/fhir/us/core/StructureDefinition/us-core-birthsex').value.coding.system"));
                birthsex.Add("display", GetFirstString("Bundle.entry.resource.where($this is Patient).extension.where(url='http://hl7.org/fhir/us/core/StructureDefinition/us-core-birthsex').value.coding.display"));
                return birthsex;
            }
            set
            {
                Decedent.Extension.RemoveAll(ext => ext.Url == "http://hl7.org/fhir/us/core/StructureDefinition/us-core-birthsex");
                Extension birthsex = new Extension();
                birthsex.Url = "http://hl7.org/fhir/us/core/StructureDefinition/us-core-birthsex";
                birthsex.Value = DictToCodeableConcept(value);
                Decedent.Extension.Add(birthsex);
            }
        }

        /// <summary>Decedent's Date of Birth.</summary>
        /// <value>the decedent's date of birth</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.DateOfBirth = "1940-02-19T16:48:06.4988220-05:00";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Decedent Date of Birth: {ExampleDeathRecord.DateOfBirth}");</para>
        /// </example>
        [Property("Date Of Birth", Property.Types.String, "Decedent Demographics", "Decedent's Date of Birth.", true, "http://hl7.org/fhir/us/vrdr/2019May/Decedent.html", true, 2)]
        [FHIRPath("Bundle.entry.resource.where($this is Patient)", "birthDate")]
        public string DateOfBirth
        {
            get
            {
                return GetFirstString("Bundle.entry.resource.where($this is Patient).birthDate");
            }
            set
            {
                Decedent.BirthDate = value.Trim();
            }
        }

        /// <summary>Decedent's residence.</summary>
        /// <value>Decedent's residence. A Dictionary representing residence address, containing the following key/value pairs:
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
        /// <para>address.Add("addressZip", "12345");</para>
        /// <para>address.Add("addressCountry", "United States");</para>
        /// <para>SetterDeathRecord.Residence = address;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"State of residence: {ExampleDeathRecord.Residence["addressState"]}");</para>
        /// </example>
        [Property("Residence", Property.Types.Dictionary, "Decedent Demographics", "Decedent's residence.", true, "http://hl7.org/fhir/us/vrdr/2019May/Decedent.html", true, 2)]
        [PropertyParam("addressLine1", "address, line one")]
        [PropertyParam("addressLine2", "address, line two")]
        [PropertyParam("addressCity", "address, city")]
        [PropertyParam("addressCounty", "address, county")]
        [PropertyParam("addressState", "address, state")]
        [PropertyParam("addressZip", "address, zip")]
        [PropertyParam("addressCountry", "address, country")]
        [FHIRPath("Bundle.entry.resource.where($this is Patient)", "address")]
        public Dictionary<string, string> Residence
        {
            get
            {
                if (Decedent != null && Decedent.Address != null && Decedent.Address.Count() > 0)
                {
                    return AddressToDict(Decedent.Address.First());
                }
                return EmptyAddrDict();
            }
            set
            {
                Decedent.Address.Clear();
                Decedent.Address.Add(DictToAddress(value));
            }
        }

        /// <summary>Decedent's Social Security Number.</summary>
        /// <value>the decedent's social security number, without dashes.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.SSN = "12345678";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Decedent Suffix: {ExampleDeathRecord.SSN}");</para>
        /// </example>
        [Property("SSN", Property.Types.String, "Decedent Demographics", "Decedent's Social Security Number.", true, "http://hl7.org/fhir/us/vrdr/2019May/Decedent.html", true, 2)]
        [FHIRPath("Bundle.entry.resource.where($this is Patient).identifier.where(system='http://hl7.org/fhir/sid/us-ssn')", "")]
        public string SSN
        {
            get
            {
                return GetFirstString("Bundle.entry.resource.where($this is Patient).identifier.where(system = 'http://hl7.org/fhir/sid/us-ssn').value");
            }
            set
            {
                Decedent.Identifier.RemoveAll(iden => iden.System == "http://hl7.org/fhir/sid/us-ssn");
                Identifier ssn = new Identifier();
                ssn.Type = new CodeableConcept(null, "BR", "Social Beneficiary Identifier", null);
                ssn.System = "http://hl7.org/fhir/sid/us-ssn";
                ssn.Value = value.Replace("-", string.Empty);
                Decedent.Identifier.Add(ssn);
            }
        }

        /// <summary>Decedent's Ethnicity.</summary>
        /// <value>the decedent's ethnicity. An array of tuples, where the first value of each tuple is the display value, and the second is
        /// the code.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Tuple&lt;string, string&gt;[] ethnicity = { Tuple.Create("Non Hispanic or Latino", "2186-5"), Tuple.Create("Salvadoran", "2161-8") };</para>
        /// <para>ExampleDeathRecord.Ethnicity = ethnicity;</para>
        /// <para>// Getter:</para>
        /// <para>foreach(var pair in deathRecord.Ethnicity)</para>
        /// <para>{</para>
        /// <para>  Console.WriteLine($"\tEthnicity text: {pair.Key}: code: {pair.Value}");</para>
        /// <para>};</para>
        /// </example>
        [Property("Ethnicity", Property.Types.TupleArr, "Decedent Demographics", "Decedent's Ethnicity.", true, "http://hl7.org/fhir/us/vrdr/2019May/Decedent.html", true, 2)]
        [FHIRPath("Bundle.entry.resource.where($this is Patient).extension.where(url='http://hl7.org/fhir/us/core/StructureDefinition/us-core-ethnicity')", "")]
        public Tuple<string, string>[] Ethnicity
        {
            get
            {
                string[] ombCodes = new string[] { };
                string[] detailedCodes = new string[] { };
                ombCodes = GetAllString("Bundle.entry.resource.where($this is Patient).extension.where(url = 'http://hl7.org/fhir/us/core/StructureDefinition/us-core-ethnicity').extension.where(url = 'ombCategory').value.code");
                detailedCodes = GetAllString("Bundle.entry.resource.where($this is Patient).extension.where(url = 'http://hl7.org/fhir/us/core/StructureDefinition/us-core-ethnicity').extension.where(url = 'detailed').value.code");
                Tuple<string, string>[] ethnicityList = new Tuple<string, string>[ombCodes.Length + detailedCodes.Length];
                for (int i = 0; i < ombCodes.Length; i++)
                {
                    ethnicityList[i] = (Tuple.Create(MortalityData.EthnicityCodeToEthnicityName(ombCodes[i]), ombCodes[i]));
                }
                for (int i = 0; i < detailedCodes.Length; i++)
                {
                    ethnicityList[ombCodes.Length + i] = (Tuple.Create(MortalityData.EthnicityCodeToEthnicityName(detailedCodes[i]), detailedCodes[i]));
                }
                return ethnicityList;
            }
            set
            {
                Decedent.Extension.RemoveAll(ext => ext.Url == "http://hl7.org/fhir/us/core/StructureDefinition/us-core-ethnicity");
                Extension ethnicities = new Extension();
                ethnicities.Url = "http://hl7.org/fhir/us/core/StructureDefinition/us-core-ethnicity";
                List<string> textEthStrs = new List<string>();
                foreach (Tuple<string, string> element in value)
                {
                    string display = element.Item1;
                    string code = element.Item2;
                    textEthStrs.Add(display);
                    Extension codeEthnicity = new Extension();
                    if (code == "2135-2" || code == "2186-5")
                    {
                        codeEthnicity.Url = "ombCategory";
                    }
                    else
                    {
                        codeEthnicity.Url = "detailed";
                    }
                    codeEthnicity.Value = new Coding("urn:oid:2.16.840.1.113883.6.238", code, display);
                    ethnicities.Extension.Add(codeEthnicity);
                }
                Extension textEth = new Extension();
                textEth.Url = "text";
                textEth.Value = new FhirString(String.Join(", ", textEthStrs));
                ethnicities.Extension.Add(textEth);
                Decedent.Extension.Add(ethnicities);
            }
        }

        /// <summary>Decedent's Race.</summary>
        /// <value>the decedent's race. An array of tuples, where the first value of each tuple is the display value, and the second is
        /// the code.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Tuple&lt;string, string&gt;[] race = { Tuple.Create("Non Hispanic or Latino", "2186-5"), Tuple.Create("Salvadoran", "2161-8") };</para>
        /// <para>ExampleDeathRecord.Race = race;</para>
        /// <para>// Getter:</para>
        /// <para>foreach(var pair in ExampleDeathRecord.race)</para>
        /// <para>{</para>
        /// <para>   Console.WriteLine($"\Race text: {pair.Key}: code: {pair.Value}");</para>
        /// <para>};</para>
        /// </example>
        [Property("Race", Property.Types.TupleArr, "Decedent Demographics", "Decedent's Race.", true, "http://hl7.org/fhir/us/vrdr/2019May/Decedent.html", true, 2)]
        [FHIRPath("Bundle.entry.resource.where($this is Patient).extension.where(url='http://hl7.org/fhir/us/core/StructureDefinition/us-core-race')", "")]
        public Tuple<string, string>[] Race
        {
            get
            {
                string[] ombCodes = new string[] { };
                string[] detailedCodes = new string[] { };
                ombCodes = GetAllString("Bundle.entry.resource.where($this is Patient).extension.where(url = 'http://hl7.org/fhir/us/core/StructureDefinition/us-core-race').extension.where(url = 'ombCategory').value.code");
                detailedCodes = GetAllString("Bundle.entry.resource.where($this is Patient).extension.where(url = 'http://hl7.org/fhir/us/core/StructureDefinition/us-core-race').extension.where(url = 'detailed').value.code");
                Tuple<string, string>[] raceList = new Tuple<string, string>[ombCodes.Length + detailedCodes.Length];
                for (int i = 0; i < ombCodes.Length; i++)
                {
                    raceList[i] = (Tuple.Create(MortalityData.RaceCodeToRaceName(ombCodes[i]), ombCodes[i]));
                }
                for (int i = 0; i < detailedCodes.Length; i++)
                {
                    raceList[ombCodes.Length + i] = (Tuple.Create(MortalityData.RaceCodeToRaceName(detailedCodes[i]), detailedCodes[i]));
                }
                return raceList;
            }
            set
            {
                Decedent.Extension.RemoveAll(ext => ext.Url == "http://hl7.org/fhir/us/core/StructureDefinition/us-core-race");
                Extension races = new Extension();
                races.Url = "http://hl7.org/fhir/us/core/StructureDefinition/us-core-race";
                List<string> textRaceStrs = new List<string>();
                foreach(Tuple<string,string> element in value)
                {
                    string display = element.Item1;
                    string code = element.Item2;
                    textRaceStrs.Add(display);
                    Extension codeRace = new Extension();
                    if (code == "1002-5" || code == "2028-9" || code == "2054-5" || code == "2076-8" || code == "2106-3")
                    {
                        codeRace.Url = "ombCategory";
                    }
                    else
                    {
                        codeRace.Url = "detailed";
                    }
                    codeRace.Value = new Coding("urn:oid:2.16.840.1.113883.6.238", code, display);
                    races.Extension.Add(codeRace);
                }
                Extension textRace = new Extension();
                textRace.Url = "text";
                textRace.Value = new FhirString(String.Join(", ", textRaceStrs));
                races.Extension.Add(textRace);
                Decedent.Extension.Add(races);
            }
        }

        /// <summary>Decedent's Place Of Birth.</summary>
        /// <value>decedent's Place Of Birth. A Dictionary representing residence address, containing the following key/value pairs:
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
        /// <para>address.Add("addressZip", "12345");</para>
        /// <para>address.Add("addressCountry", "United States");</para>
        /// <para>SetterDeathRecord.PlaceOfBirth = address;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"State where decedent was born: {ExampleDeathRecord.PlaceOfBirth["placeOfBirthState"]}");</para>
        /// </example>
        [Property("Place Of Birth", Property.Types.Dictionary, "Decedent Demographics", "Decedent's Place Of Birth.", true, "http://hl7.org/fhir/us/vrdr/2019May/Decedent.html", true, 2)]
        [PropertyParam("addressLine1", "address, line one")]
        [PropertyParam("addressLine2", "address, line two")]
        [PropertyParam("addressCity", "address, city")]
        [PropertyParam("addressCounty", "address, county")]
        [PropertyParam("addressState", "address, state")]
        [PropertyParam("addressZip", "address, zip")]
        [PropertyParam("addressCountry", "address, country")]
        [FHIRPath("Bundle.entry.resource.where($this is Patient).extension.where(url='http://hl7.org/fhir/StructureDefinition/birthPlace')", "")]
        public Dictionary<string, string> PlaceOfBirth
        {
            get
            {
                Extension addressExt = Decedent.Extension.FirstOrDefault( extension => extension.Url == "http://hl7.org/fhir/StructureDefinition/birthPlace" );
                if (addressExt != null)
                {
                    Address address = (Address)addressExt.Value;
                    if (address != null)
                    {
                        return AddressToDict((Address)address);
                    }
                    return EmptyAddrDict();
                }
                return EmptyAddrDict();
            }
            set
            {
                Decedent.Extension.RemoveAll(ext => ext.Url == "http://hl7.org/fhir/StructureDefinition/birthPlace");
                Extension placeOfBirthExt = new Extension();
                placeOfBirthExt.Url = "http://hl7.org/fhir/StructureDefinition/birthPlace";
                placeOfBirthExt.Value = DictToAddress(value);
                Decedent.Extension.Add(placeOfBirthExt);
            }
        }

        /// <summary>The marital status of the decedent at the time of death.</summary>
        /// <value>the marital status of the decedent at the time of death. A Dictionary representing a code, containing the following key/value pairs:
        /// <para>"code" - the code describing this finding</para>
        /// <para>"system" - the system the given code belongs to</para>
        /// <para>"display" - the human readable display text that corresponds to the given code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; code = new Dictionary&lt;string, string&gt;();</para>
        /// <para>code.Add("code", "S");</para>
        /// <para>code.Add("system", "http://hl7.org/fhir/v3/MaritalStatus");</para>
        /// <para>code.Add("display", "Never Married");</para>
        /// <para>ExampleDeathRecord.MaritalStatus = code;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Marital status: {ExampleDeathRecord.MaritalStatus["display"]}");</para>
        /// </example>
        [Property("Marital Status", Property.Types.Dictionary, "Decedent Demographics", "The marital status of the decedent at the time of death.", true, "http://hl7.org/fhir/us/vrdr/2019May/Decedent.html", true, 2)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Patient)", "maritalStatus")]
        public Dictionary<string, string> MaritalStatus
        {
            get
            {
                if (Decedent != null && Decedent.MaritalStatus != null)
                {
                    return CodeableConceptToDict(Decedent.MaritalStatus);
                }
                return new Dictionary<string, string>() { { "code", "" }, { "system", "" }, { "display", "" } };
            }
            set
            {
                Decedent.MaritalStatus = DictToCodeableConcept(value);
            }
        }

        /// <summary>Given name(s) of decedent's father.</summary>
        /// <value>the decedent's father's name (first, middle, etc.)</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>string[] names = { "Dad", "Middle" };</para>
        /// <para>ExampleDeathRecord.FatherGivenNames = names;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Father Given Name(s): {string.Join(", ", ExampleDeathRecord.FatherGivenNames)}");</para>
        /// </example>
        [Property("Father Given Names", Property.Types.StringArr, "Decedent Demographics", "Given name(s) of decedent's father.", true, "http://hl7.org/fhir/us/vrdr/2019May/DecedentFather.html", false, 2)]
        [FHIRPath("Bundle.entry.resource.where($this is RelatedPerson).where(relationship.coding.code='FTH')", "name")]
        public string[] FatherGivenNames
        {
            get
            {
                if (Father != null && Father.Name != null && Father.Name.Count() > 0 && Father.Name.First().Given != null) {
                    return Father.Name.First().Given.ToArray();
                }
                return new string[0];
            }
            set
            {
                if (Father == null)
                {
                    Father = new RelatedPerson();
                    Father.Id = "urn:uuid:" + Guid.NewGuid().ToString();
                    Father.Meta = new Meta();
                    string[] father_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Decedent-Father" };
                    Father.Meta.Profile = father_profile;
                    Father.Patient = new ResourceReference(Decedent.Id);
                    Father.Relationship = new CodeableConcept(null, "FTH", null, null);
                    HumanName name = new HumanName();
                    name.Given = value;
                    Father.Name.Add(name);
                    AddReferenceToComposition(Father.Id);
                    Bundle.AddResourceEntry(Father, Father.Id);
                }
                else
                {
                    Father.Name.First().Given = value;
                }

            }
        }

        /// <summary>Family name of decedent's father.</summary>
        /// <value>the decedent's father's family name (i.e. last name)</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.FatherFamilyName = "Last";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Father's Last Name: {ExampleDeathRecord.FatherFamilyName}");</para>
        /// </example>
        [Property("Father Family Name", Property.Types.String, "Decedent Demographics", "Family name of decedent's father.", true, "http://hl7.org/fhir/us/vrdr/2019May/DecedentFather.html", false, 2)]
        [FHIRPath("Bundle.entry.resource.where($this is RelatedPerson).where(relationship.coding.code='FTH')", "name")]
        public string FatherFamilyName
        {
            get
            {
                if (Father != null && Father.Name != null && Father.Name.Count() > 0 && Father.Name.First().Family != null) {
                    return Father.Name.First().Family;
                }
                return null;
            }
            set
            {
                if (Father == null)
                {
                    Father = new RelatedPerson();
                    Father.Id = "urn:uuid:" + Guid.NewGuid().ToString();
                    Father.Meta = new Meta();
                    string[] father_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Decedent-Father" };
                    Father.Meta.Profile = father_profile;
                    Father.Patient = new ResourceReference(Decedent.Id);
                    Father.Relationship = new CodeableConcept(null, "FTH", null, null);
                    HumanName name = new HumanName();
                    name.Family = value;
                    Father.Name.Add(name);
                    AddReferenceToComposition(Father.Id);
                    Bundle.AddResourceEntry(Father, Father.Id);
                }
                else
                {
                    Father.Name.First().Family = value;
                }

            }
        }

        /// <summary>Father's Suffix.</summary>
        /// <value>the decedent's father's suffix</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.FatherSuffix = "Jr.";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Father Suffix: {ExampleDeathRecord.FatherSuffix}");</para>
        /// </example>
        [Property("Father Suffix", Property.Types.String, "Decedent Demographics", "Father's Suffix.", true, "http://hl7.org/fhir/us/vrdr/2019May/DecedentFather.html", false, 2)]
        [FHIRPath("Bundle.entry.resource.where($this is RelatedPerson).where(relationship.coding.code='FTH')", "name")]
        public string FatherSuffix
        {
            get
            {
                if (Father != null && Father.Name != null && Father.Name.Count() > 0 && Father.Name.First().Suffix != null) {

                    return Father.Name.First().Suffix.FirstOrDefault();
                }
                return null;
            }
            set
            {
                if (Father == null)
                {
                    Father = new RelatedPerson();
                    Father.Id = "urn:uuid:" + Guid.NewGuid().ToString();
                    Father.Meta = new Meta();
                    string[] father_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Decedent-Father" };
                    Father.Meta.Profile = father_profile;
                    Father.Patient = new ResourceReference(Decedent.Id);
                    Father.Relationship = new CodeableConcept(null, "FTH", null, null);
                    HumanName name = new HumanName();
                    string[] suffix = { value };
                    name.Suffix = suffix;
                    Father.Name.Add(name);
                    AddReferenceToComposition(Father.Id);
                    Bundle.AddResourceEntry(Father, Father.Id);
                }
                else
                {
                    string[] suffix = { value };
                    Father.Name.First().Suffix = suffix;
                }

            }
        }

        /// <summary>Given name(s) of decedent's mother.</summary>
        /// <value>the decedent's mother's name (first, middle, etc.)</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>string[] names = { "Mom", "Middle" };</para>
        /// <para>ExampleDeathRecord.MotherGivenNames = names;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Mother Given Name(s): {string.Join(", ", ExampleDeathRecord.MotherGivenNames)}");</para>
        /// </example>
        [Property("Mother Given Names", Property.Types.StringArr, "Decedent Demographics", "Given name(s) of decedent's mother.", true, "http://hl7.org/fhir/us/vrdr/2019May/DecedentMother.html", false, 2)]
        [FHIRPath("Bundle.entry.resource.where($this is RelatedPerson).where(relationship.coding.code='MTH')", "name")]
        public string[] MotherGivenNames
        {
            get
            {
                if (Mother != null && Mother.Name != null && Mother.Name.Count() > 0 && Mother.Name.First().Given != null) {
                    return Mother.Name.First().Given.ToArray();
                }
                return new string[0];
            }
            set
            {
                if (Mother == null)
                {
                    Mother = new RelatedPerson();
                    Mother.Id = "urn:uuid:" + Guid.NewGuid().ToString();
                    Mother.Meta = new Meta();
                    string[] mother_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Decedent-Mother" };
                    Mother.Meta.Profile = mother_profile;
                    Mother.Patient = new ResourceReference(Decedent.Id);
                    Mother.Relationship = new CodeableConcept(null, "MTH", null, null);
                    HumanName name = new HumanName();
                    name.Given = value;
                    Mother.Name.Add(name);
                    AddReferenceToComposition(Mother.Id);
                    Bundle.AddResourceEntry(Mother, Mother.Id);
                }
                else
                {
                    Mother.Name.First().Given = value;
                }

            }
        }

        /// <summary>Maiden name of decedent's mother.</summary>
        /// <value>the decedent's mother's maiden name (i.e. last name before marriage)</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.MotherMaidenName = "Last";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Mother's Maiden Name: {ExampleDeathRecord.MotherMaidenName}");</para>
        /// </example>
        [Property("Mother Maiden Name", Property.Types.String, "Decedent Demographics", "Maiden name of decedent's mother.", true, "http://hl7.org/fhir/us/vrdr/2019May/DecedentMother.html", false, 2)]
        [FHIRPath("Bundle.entry.resource.where($this is RelatedPerson).where(relationship.coding.code='MTH')", "name")]
        public string MotherMaidenName
        {
            get
            {
                if (Mother != null && Mother.Name != null && Mother.Name.Count() > 0 && Mother.Name.First().Family != null) {
                    return Mother.Name.First().Family;
                }
                return null;
            }
            set
            {
                if (Mother == null)
                {
                    Mother = new RelatedPerson();
                    Mother.Id = "urn:uuid:" + Guid.NewGuid().ToString();
                    Mother.Meta = new Meta();
                    string[] mother_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Decedent-Mother" };
                    Mother.Meta.Profile = mother_profile;
                    Mother.Patient = new ResourceReference(Decedent.Id);
                    Mother.Relationship = new CodeableConcept(null, "MTH", null, null);
                    HumanName name = new HumanName();
                    name.Family = value;
                    Mother.Name.Add(name);
                    AddReferenceToComposition(Mother.Id);
                    Bundle.AddResourceEntry(Mother, Mother.Id);
                }
                else
                {
                    Mother.Name.First().Family = value;
                }

            }
        }

        /// <summary>Mother's Suffix.</summary>
        /// <value>the decedent's mother's suffix</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.MotherSuffix = "Jr.";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Mother Suffix: {ExampleDeathRecord.MotherSuffix}");</para>
        /// </example>
        [Property("Mother Suffix", Property.Types.String, "Decedent Demographics", "Mother's Suffix.", true, "http://hl7.org/fhir/us/vrdr/2019May/DecedentMother.html", false, 2)]
        [FHIRPath("Bundle.entry.resource.where($this is RelatedPerson).where(relationship.coding.code='MTH')", "name")]
        public string MotherSuffix
        {
            get
            {
                if (Mother != null && Mother.Name != null && Mother.Name.Count() > 0 && Mother.Name.First().Suffix != null) {

                    return Mother.Name.First().Suffix.FirstOrDefault();
                }
                return null;
            }
            set
            {
                if (Mother == null)
                {
                    Mother = new RelatedPerson();
                    Mother.Id = "urn:uuid:" + Guid.NewGuid().ToString();
                    Mother.Meta = new Meta();
                    string[] mother_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Decedent-Mother" };
                    Mother.Meta.Profile = mother_profile;
                    Mother.Patient = new ResourceReference(Decedent.Id);
                    Mother.Relationship = new CodeableConcept(null, "MTH", null, null);
                    HumanName name = new HumanName();
                    string[] suffix = { value };
                    name.Suffix = suffix;
                    Mother.Name.Add(name);
                    AddReferenceToComposition(Mother.Id);
                    Bundle.AddResourceEntry(Mother, Mother.Id);
                }
                else
                {
                    string[] suffix = { value };
                    Mother.Name.First().Suffix = suffix;
                }

            }
        }

        /// <summary>Given name(s) of decedent's spouse.</summary>
        /// <value>the decedent's spouse's name (first, middle, etc.)</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>string[] names = { "Spouse", "Middle" };</para>
        /// <para>ExampleDeathRecord.SpouseGivenNames = names;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Spouse Given Name(s): {string.Join(", ", ExampleDeathRecord.SpouseGivenNames)}");</para>
        /// </example>
        [Property("Spouse Given Names", Property.Types.StringArr, "Decedent Demographics", "Given name(s) of decedent's spouse.", true, "http://hl7.org/fhir/us/vrdr/2019May/DecedentSpouse.html", false, 2)]
        [FHIRPath("Bundle.entry.resource.where($this is RelatedPerson).where(relationship.coding.code='SPS')", "name")]
        public string[] SpouseGivenNames
        {
            get
            {
                if (Spouse != null && Spouse.Name != null && Spouse.Name.Count() > 0 && Spouse.Name.First().Given != null) {
                    return Spouse.Name.First().Given.ToArray();
                }
                return new string[0];
            }
            set
            {
                if (Spouse == null)
                {
                    Spouse = new RelatedPerson();
                    Spouse.Id = "urn:uuid:" + Guid.NewGuid().ToString();
                    Spouse.Meta = new Meta();
                    string[] spouse_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Decedent-Spouse" };
                    Spouse.Meta.Profile = spouse_profile;
                    Spouse.Patient = new ResourceReference(Decedent.Id);
                    Spouse.Relationship = new CodeableConcept(null, "SPS", null, null);
                    HumanName name = new HumanName();
                    name.Given = value;
                    Spouse.Name.Add(name);
                    AddReferenceToComposition(Spouse.Id);
                    Bundle.AddResourceEntry(Spouse, Spouse.Id);
                }
                else
                {
                    Spouse.Name.First().Given = value;
                }

            }
        }

        /// <summary>Family name of decedent's spouse.</summary>
        /// <value>the decedent's spouse's family name (i.e. last name)</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.SpouseFamilyName = "Last";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Spouse's Last Name: {ExampleDeathRecord.SpouseFamilyName}");</para>
        /// </example>
        [Property("Spouse Family Name", Property.Types.String, "Decedent Demographics", "Family name of decedent's spouse.", true, "http://hl7.org/fhir/us/vrdr/2019May/DecedentSpouse.html", false, 2)]
        [FHIRPath("Bundle.entry.resource.where($this is RelatedPerson).where(relationship.coding.code='SPS')", "name")]
        public string SpouseFamilyName
        {
            get
            {
                if (Spouse != null && Spouse.Name != null && Spouse.Name.Count() > 0 && Spouse.Name.First().Family != null) {
                    return Spouse.Name.First().Family;
                }
                return null;
            }
            set
            {
                if (Spouse == null)
                {
                    Spouse = new RelatedPerson();
                    Spouse.Id = "urn:uuid:" + Guid.NewGuid().ToString();
                    Spouse.Meta = new Meta();
                    string[] spouse_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Decedent-Spouse" };
                    Spouse.Meta.Profile = spouse_profile;
                    Spouse.Patient = new ResourceReference(Decedent.Id);
                    Spouse.Relationship = new CodeableConcept(null, "SPS", null, null);
                    HumanName name = new HumanName();
                    name.Family = value;
                    Spouse.Name.Add(name);
                    AddReferenceToComposition(Spouse.Id);
                    Bundle.AddResourceEntry(Spouse, Spouse.Id);
                }
                else
                {
                    Spouse.Name.First().Family = value;
                }

            }
        }

        /// <summary>Spouse's Suffix.</summary>
        /// <value>the decedent's spouse's suffix</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.SpouseSuffix = "Jr.";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Spouse Suffix: {ExampleDeathRecord.SpouseSuffix}");</para>
        /// </example>
        [Property("Spouse Suffix", Property.Types.String, "Decedent Demographics", "Spouse's Suffix.", true, "http://hl7.org/fhir/us/vrdr/2019May/DecedentSpouse.html", false, 2)]
        [FHIRPath("Bundle.entry.resource.where($this is RelatedPerson).where(relationship.coding.code='SPS')", "name")]
        public string SpouseSuffix
        {
            get
            {
                if (Spouse != null && Spouse.Name != null && Spouse.Name.Count() > 0 && Spouse.Name.First().Suffix != null) {

                    return Spouse.Name.First().Suffix.FirstOrDefault();
                }
                return null;
            }
            set
            {
                if (Spouse == null)
                {
                    Spouse = new RelatedPerson();
                    Spouse.Id = "urn:uuid:" + Guid.NewGuid().ToString();
                    Spouse.Meta = new Meta();
                    string[] spouse_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Decedent-Spouse" };
                    Spouse.Meta.Profile = spouse_profile;
                    Spouse.Patient = new ResourceReference(Decedent.Id);
                    Spouse.Relationship = new CodeableConcept(null, "SPS", null, null);
                    HumanName name = new HumanName();
                    string[] suffix = { value };
                    name.Suffix = suffix;
                    Spouse.Name.Add(name);
                    AddReferenceToComposition(Spouse.Id);
                    Bundle.AddResourceEntry(Spouse, Spouse.Id);
                }
                else
                {
                    string[] suffix = { value };
                    Spouse.Name.First().Suffix = suffix;
                }

            }
        }

        /// <summary>Decedent's Education Level.</summary>
        /// <value>the decedent's education level. A Dictionary representing a code, containing the following key/value pairs:
        /// <para>"code" - the code</para>
        /// <para>"system" - the code system this code belongs to</para>
        /// <para>"display" - a human readable meaning of the code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; elevel = new Dictionary&lt;string, string&gt;();</para>
        /// <para>elevel.Add("code", "BD");</para>
        /// <para>elevel.Add("system", "http://hl7.org/fhir/v3/EducationLevel");</para>
        /// <para>elevel.Add("display", "College or baccalaureate degree complete");</para>
        /// <para>ExampleDeathRecord.EducationLevel = elevel;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Education Level: {ExampleDeathRecord.EducationLevel['display']}");</para>
        /// </example>
        [Property("Education Level", Property.Types.Dictionary, "Decedent Demographics", "Decedent's Education Level.", true, "http://hl7.org/fhir/us/vrdr/2019May/DecedentEducationLevel.html", false, 2)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='80913-7')", "")]
        public Dictionary<string, string> EducationLevel
        {
            get
            {
                if (DecedentEducationLevel != null && DecedentEducationLevel.Value != null)
                {
                    return CodeableConceptToDict((CodeableConcept)DecedentEducationLevel.Value);
                }
                return new Dictionary<string, string>() { { "code", "" }, { "system", "" }, { "display", "" } };
            }
            set
            {
                if (DecedentEducationLevel == null)
                {
                    DecedentEducationLevel = new Observation();
                    DecedentEducationLevel.Id = "urn:uuid:" + Guid.NewGuid().ToString();
                    DecedentEducationLevel.Meta = new Meta();
                    string[] educationlevel_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Decedent-Education-Level" };
                    DecedentEducationLevel.Meta.Profile = educationlevel_profile;
                    DecedentEducationLevel.Status = ObservationStatus.Final;
                    DecedentEducationLevel.Code = new CodeableConcept("http://loinc.org", "80913-7", "Highest level of education", null);
                    DecedentEducationLevel.Subject = new ResourceReference(Decedent.Id);
                    DecedentEducationLevel.Value = DictToCodeableConcept(value);
                    AddReferenceToComposition(DecedentEducationLevel.Id);
                    Bundle.AddResourceEntry(DecedentEducationLevel, DecedentEducationLevel.Id);
                }
                else
                {
                    DecedentEducationLevel.Value = DictToCodeableConcept(value);
                }
            }
        }

        /// <summary>Birth Record Identifier.</summary>
        /// <value>a birth record identification string.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.BirthRecordId = "4242123";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Birth Record identification: {ExampleDeathRecord.BirthRecordId}");</para>
        /// </example>
        [Property("Birth Record Id", Property.Types.String, "Decedent Demographics", "Birth Record Identifier.", true, "http://hl7.org/fhir/us/vrdr/2019May/BirthRecordIdentifier.html", true, 2)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='BR')", "")]
        public string BirthRecordId
        {
            get
            {
                if (BirthRecordIdentifier != null && BirthRecordIdentifier.Value != null)
                {
                    return Convert.ToString(BirthRecordIdentifier.Value);
                }
                return null;
            }
            set
            {
                if (BirthRecordIdentifier == null)
                {
                    BirthRecordIdentifier = new Observation();
                    BirthRecordIdentifier.Id = "urn:uuid:" + Guid.NewGuid().ToString();
                    BirthRecordIdentifier.Meta = new Meta();
                    string[] br_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Birth-Record-Identifier" };
                    BirthRecordIdentifier.Meta.Profile = br_profile;
                    BirthRecordIdentifier.Status = ObservationStatus.Final;
                    BirthRecordIdentifier.Code = new CodeableConcept(null, "BR", null, null);
                    BirthRecordIdentifier.Subject = new ResourceReference(Decedent.Id);
                    BirthRecordIdentifier.Value = new FhirString(value);
                    AddReferenceToComposition(BirthRecordIdentifier.Id);
                    Bundle.AddResourceEntry(BirthRecordIdentifier, BirthRecordIdentifier.Id);
                }
                else
                {
                    BirthRecordIdentifier.Value = new FhirString(value);
                }
            }
        }

        /// <summary>Decedent's Usual Occupation.</summary>
        /// <value>the decedent's usual occupation. A Dictionary representing a code, containing the following key/value pairs:
        /// <para>"code" - the code</para>
        /// <para>"system" - the code system this code belongs to</para>
        /// <para>"display" - a human readable meaning of the code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; uocc = new Dictionary&lt;string, string&gt;();</para>
        /// <para>uocc.Add("code", "1340");</para>
        /// <para>uocc.Add("system", "http://hl7.org/fhir/ValueSet/occupation-cdc-census-2010");</para>
        /// <para>uocc.Add("display", "Biomedical engineers");</para>
        /// <para>ExampleDeathRecord.UsualOccupation = uocc;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Usual Occupation: {ExampleDeathRecord.UsualOccupation['display']}");</para>
        /// </example>
        [Property("Usual Occupation", Property.Types.Dictionary, "Decedent Demographics", "Decedent's Usual Occupation.", true, "http://hl7.org/fhir/us/vrdr/2019May/DecedentEmploymentHistory.html", false, 2)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='74165-2')", "")]
        public Dictionary<string, string> UsualOccupation
        {
            get
            {
                if (EmploymentHistory != null)
                {
                    Observation.ComponentComponent component = EmploymentHistory.Component.FirstOrDefault( cmp => cmp.Code!= null && cmp.Code.Coding != null && cmp.Code.Coding.Count() > 0 && cmp.Code.Coding.First().Code == "21847-9" );
                    if (component != null)
                    {
                        return CodeableConceptToDict((CodeableConcept)component.Value);
                    }
                    return new Dictionary<string, string>() { { "code", "" }, { "system", "" }, { "display", "" } };
                }
                return new Dictionary<string, string>() { { "code", "" }, { "system", "" }, { "display", "" } };
            }
            set
            {
                if (EmploymentHistory == null)
                {
                    EmploymentHistory = new Observation();
                    EmploymentHistory.Id = "urn:uuid:" + Guid.NewGuid().ToString();
                    EmploymentHistory.Meta = new Meta();
                    string[] employmenthistory_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Decedent-Employment-History" };
                    EmploymentHistory.Meta.Profile = employmenthistory_profile;
                    EmploymentHistory.Status = ObservationStatus.Final;
                    EmploymentHistory.Code = new CodeableConcept("http://loinc.org", "74165-2", "History of employment status", null);
                    EmploymentHistory.Subject = new ResourceReference(Decedent.Id);
                    Observation.ComponentComponent component = new Observation.ComponentComponent();
                    component.Code = new CodeableConcept("http://loinc.org", "21847-9", "Usual occupation", null);
                    component.Value = DictToCodeableConcept(value);
                    EmploymentHistory.Component.Add(component);
                    AddReferenceToComposition(EmploymentHistory.Id);
                    Bundle.AddResourceEntry(EmploymentHistory, EmploymentHistory.Id);
                }
                else
                {
                    EmploymentHistory.Component.RemoveAll( cmp => cmp.Code!= null && cmp.Code.Coding != null && cmp.Code.Coding.Count() > 0 && cmp.Code.Coding.First().Code == "21847-9" );
                    Observation.ComponentComponent component = new Observation.ComponentComponent();
                    component.Code = new CodeableConcept("http://loinc.org", "21847-9", "Usual occupation", null);
                    component.Value = DictToCodeableConcept(value);
                    EmploymentHistory.Component.Add(component);
                }
            }
        }

        /// <summary>Decedent's Usual Industry.</summary>
        /// <value>the decedent's usual industry. A Dictionary representing a code, containing the following key/value pairs:
        /// <para>"code" - the code</para>
        /// <para>"system" - the code system this code belongs to</para>
        /// <para>"display" - a human readable meaning of the code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; uind = new Dictionary&lt;string, string&gt;();</para>
        /// <para>uind.Add("code", "7280");</para>
        /// <para>uind.Add("system", "http://hl7.org/fhir/ValueSet/industry-cdc-census-2010");</para>
        /// <para>uind.Add("display", "Accounting, tax preparation, bookkeeping, and payroll services");</para>
        /// <para>ExampleDeathRecord.UsualIndustry = uind;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Usual Industry: {ExampleDeathRecord.UsualIndustry['display']}");</para>
        /// </example>
        [Property("Usual Industry", Property.Types.Dictionary, "Decedent Demographics", "Decedent's Usual Industry.", true, "http://hl7.org/fhir/us/vrdr/2019May/DecedentEmploymentHistory.html", false, 2)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='74165-2')", "")]
        public Dictionary<string, string> UsualIndustry
        {
            get
            {
                if (EmploymentHistory != null)
                {
                    Observation.ComponentComponent component = EmploymentHistory.Component.FirstOrDefault( cmp => cmp.Code!= null && cmp.Code.Coding != null && cmp.Code.Coding.Count() > 0 && cmp.Code.Coding.First().Code == "21844-6" );
                    if (component != null)
                    {
                        return CodeableConceptToDict((CodeableConcept)component.Value);
                    }
                    return new Dictionary<string, string>() { { "code", "" }, { "system", "" }, { "display", "" } };
                }
                return new Dictionary<string, string>() { { "code", "" }, { "system", "" }, { "display", "" } };
            }
            set
            {
                if (EmploymentHistory == null)
                {
                    EmploymentHistory = new Observation();
                    EmploymentHistory.Id = "urn:uuid:" + Guid.NewGuid().ToString();
                    EmploymentHistory.Meta = new Meta();
                    string[] employmenthistory_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Decedent-Employment-History" };
                    EmploymentHistory.Meta.Profile = employmenthistory_profile;
                    EmploymentHistory.Status = ObservationStatus.Final;
                    EmploymentHistory.Code = new CodeableConcept("http://loinc.org", "74165-2", "History of employment status", null);
                    EmploymentHistory.Subject = new ResourceReference(Decedent.Id);
                    Observation.ComponentComponent component = new Observation.ComponentComponent();
                    component.Code = new CodeableConcept("http://loinc.org", "21844-6", "Usual industry", null);
                    component.Value = DictToCodeableConcept(value);
                    EmploymentHistory.Component.Add(component);
                    AddReferenceToComposition(EmploymentHistory.Id);
                    Bundle.AddResourceEntry(EmploymentHistory, EmploymentHistory.Id);
                }
                else
                {
                    EmploymentHistory.Component.RemoveAll( cmp => cmp.Code!= null && cmp.Code.Coding != null && cmp.Code.Coding.Count() > 0 && cmp.Code.Coding.First().Code == "21844-6" );
                    Observation.ComponentComponent component = new Observation.ComponentComponent();
                    component.Code = new CodeableConcept("http://loinc.org", "21844-6", "Usual industry", null);
                    component.Value = DictToCodeableConcept(value);
                    EmploymentHistory.Component.Add(component);
                }
            }
        }

        /// <summary>Decedent's Military Service.</summary>
        /// <value>the decedent's military service. A Dictionary representing a code, containing the following key/value pairs:
        /// <para>"code" - the code</para>
        /// <para>"system" - the code system this code belongs to</para>
        /// <para>"display" - a human readable meaning of the code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; mserv = new Dictionary&lt;string, string&gt;();</para>
        /// <para>mserv.Add("code", "Y");</para>
        /// <para>mserv.Add("system", "http://hl7.org/fhir/ValueSet/v2-0532");</para>
        /// <para>mserv.Add("display", "Yes");</para>
        /// <para>ExampleDeathRecord.MilitaryService = uind;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Military Service: {ExampleDeathRecord.MilitaryService['display']}");</para>
        /// </example>
        [Property("Military Service", Property.Types.Dictionary, "Decedent Demographics", "Decedent's Military Service.", true, "http://hl7.org/fhir/us/vrdr/2019May/DecedentEmploymentHistory.html", false, 2)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='74165-2')", "")]
        public Dictionary<string, string> MilitaryService
        {
            get
            {
                if (EmploymentHistory != null)
                {
                    Observation.ComponentComponent component = EmploymentHistory.Component.FirstOrDefault( cmp => cmp.Code!= null && cmp.Code.Coding != null && cmp.Code.Coding.Count() > 0 && cmp.Code.Coding.First().Code == "55280-2" );
                    if (component != null)
                    {
                        return CodeableConceptToDict((CodeableConcept)component.Value);
                    }
                    return new Dictionary<string, string>() { { "code", "" }, { "system", "" }, { "display", "" } };
                }
                return new Dictionary<string, string>() { { "code", "" }, { "system", "" }, { "display", "" } };
            }
            set
            {
                if (EmploymentHistory == null)
                {
                    EmploymentHistory = new Observation();
                    EmploymentHistory.Id = "urn:uuid:" + Guid.NewGuid().ToString();
                    EmploymentHistory.Meta = new Meta();
                    string[] employmenthistory_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Decedent-Employment-History" };
                    EmploymentHistory.Meta.Profile = employmenthistory_profile;
                    EmploymentHistory.Status = ObservationStatus.Final;
                    EmploymentHistory.Code = new CodeableConcept("http://loinc.org", "74165-2", "History of employment status", null);
                    EmploymentHistory.Subject = new ResourceReference(Decedent.Id);
                    Observation.ComponentComponent component = new Observation.ComponentComponent();
                    component.Code = new CodeableConcept("http://loinc.org", "55280-2", "Military service", null);
                    component.Value = DictToCodeableConcept(value);
                    EmploymentHistory.Component.Add(component);
                    AddReferenceToComposition(EmploymentHistory.Id);
                    Bundle.AddResourceEntry(EmploymentHistory, EmploymentHistory.Id);
                }
                else
                {
                    EmploymentHistory.Component.RemoveAll( cmp => cmp.Code!= null && cmp.Code.Coding != null && cmp.Code.Coding.Count() > 0 && cmp.Code.Coding.First().Code == "55280-2" );
                    Observation.ComponentComponent component = new Observation.ComponentComponent();
                    component.Code = new CodeableConcept("http://loinc.org", "55280-2", "Military service", null);
                    component.Value = DictToCodeableConcept(value);
                    EmploymentHistory.Component.Add(component);
                }
            }
        }


        /////////////////////////////////////////////////////////////////////////////////
        //
        // Record Properties: Decedent Disposition
        //
        /////////////////////////////////////////////////////////////////////////////////

        /// <summary>Given name(s) of mortician.</summary>
        /// <value>the mortician's name (first, middle, etc.)</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>string[] names = { "FD", "Middle" };</para>
        /// <para>ExampleDeathRecord.MorticianGivenNames = names;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Mortician Given Name(s): {string.Join(", ", ExampleDeathRecord.MorticianGivenNames)}");</para>
        /// </example>
        [Property("Mortician Given Names", Property.Types.StringArr, "Decedent Disposition", "Given name(s) of mortician.", true, "http://hl7.org/fhir/us/vrdr/2019May/Mortician.html", false, 3)]
        [FHIRPath("Bundle.entry.resource.where($this is Practitioner).where(meta.profile='http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Mortician')", "name")]
        public string[] MorticianGivenNames
        {
            get
            {
                if (Mortician.Name.Count() > 0)
                {
                    return Mortician.Name.First().Given.ToArray();
                }
                return new string[0];
            }
            set
            {
                HumanName name = Mortician.Name.SingleOrDefault(n => n.Use == HumanName.NameUse.Official);
                if (name != null)
                {
                    name.Given = value;
                }
                else
                {
                    name = new HumanName();
                    name.Use = HumanName.NameUse.Official;
                    name.Given = value;
                    Mortician.Name.Add(name);
                }
            }
        }

        /// <summary>Family name of mortician.</summary>
        /// <value>the mortician's family name (i.e. last name)</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.MorticianFamilyName = "Last";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Mortician's Last Name: {ExampleDeathRecord.MorticianFamilyName}");</para>
        /// </example>
        [Property("Mortician Family Name", Property.Types.String, "Decedent Disposition", "Family name of mortician.", true, "http://hl7.org/fhir/us/vrdr/2019May/Mortician.html", false, 3)]
        [FHIRPath("Bundle.entry.resource.where($this is Practitioner).where(meta.profile='http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Mortician')", "name")]
        public string MorticianFamilyName
        {
            get
            {
                if (Mortician.Name.Count() > 0)
                {
                    return Mortician.Name.First().Family;
                }
                return null;
            }
            set
            {
                HumanName name = Mortician.Name.FirstOrDefault();
                if (name != null && !String.IsNullOrEmpty(value))
                {
                    name.Family = value;
                }
                else if (!String.IsNullOrEmpty(value))
                {
                    name = new HumanName();
                    name.Use = HumanName.NameUse.Official;
                    name.Family = value;
                    Mortician.Name.Add(name);
                }
            }
        }

        /// <summary>Mortician's Suffix.</summary>
        /// <value>the mortician's suffix</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.MorticianSuffix = "Jr.";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Mortician Suffix: {ExampleDeathRecord.MorticianSuffix}");</para>
        /// </example>
        [Property("Mortician Suffix", Property.Types.String, "Decedent Disposition", "Mortician's Suffix.", true, "http://hl7.org/fhir/us/vrdr/2019May/Mortician.html", false, 3)]
        [FHIRPath("Bundle.entry.resource.where($this is Practitioner).where(meta.profile='http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Mortician')", "suffix")]
        public string MorticianSuffix
        {
            get
            {
                if (Mortician.Name.Count() > 0 && Mortician.Name.First().Suffix.Count() > 0)
                {
                    return Mortician.Name.First().Suffix.First();
                }
                return null;
            }
            set
            {
                HumanName name = Mortician.Name.FirstOrDefault();
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
                    Mortician.Name.Add(name);
                }
            }
        }

        /// <summary>Mortician Identifier.</summary>
        /// <value>the mortician's identifier</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.MorticianIdentifier = "9876543210";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Mortician Identifier: {ExampleDeathRecord.MorticianIdentifier}");</para>
        /// </example>
        [Property("Mortician Identifier", Property.Types.String, "Decedent Disposition", "Mortician Identifier.", true, "http://hl7.org/fhir/us/vrdr/2019May/Mortician.html", false, 3)]
        [FHIRPath("Bundle.entry.resource.where($this is Practitioner).where(meta.profile='http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Mortician')", "identifier")]
        public string MorticianIdentifier
        {
            get
            {
                if (Mortician != null)
                {
                    Practitioner.QualificationComponent qualification = Mortician.Qualification.FirstOrDefault();
                    if (qualification != null && qualification.Identifier.Count() > 0)
                    {
                        return qualification.Identifier.First().Value;
                    }
                }
                return null;
            }
            set
            {
                Practitioner.QualificationComponent qualification = Mortician.Qualification.FirstOrDefault();
                if (qualification != null)
                {
                    qualification.Identifier.Clear();
                    Identifier identifier = new Identifier();
                    identifier.Value = value;
                    qualification.Identifier.Add(identifier);
                }
                else
                {
                    qualification = new Practitioner.QualificationComponent();
                    Identifier identifier = new Identifier();
                    identifier.Value = value;
                    qualification.Identifier.Add(identifier);
                    Mortician.Qualification.Add(qualification);
                }
            }
        }

        /// <summary>Funeral Home Address.</summary>
        /// <value>the funeral home address. A Dictionary representing an address, containing the following key/value pairs:
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
        /// <para>address.Add("addressLine1", "1234 Test Street");</para>
        /// <para>address.Add("addressLine2", "Unit 3");</para>
        /// <para>address.Add("addressCity", "Boston");</para>
        /// <para>address.Add("addressCounty", "Suffolk");</para>
        /// <para>address.Add("addressState", "Massachusetts");</para>
        /// <para>address.Add("addressZip", "12345");</para>
        /// <para>address.Add("addressCountry", "United States");</para>
        /// <para>ExampleDeathRecord.FuneralHomeAddress = address;</para>
        /// <para>// Getter:</para>
        /// <para>foreach(var pair in ExampleDeathRecord.FuneralHomeAddress)</para>
        /// <para>{</para>
        /// <para>  Console.WriteLine($"\FuneralHomeAddress key: {pair.Key}: value: {pair.Value}");</para>
        /// <para>};</para>
        /// </example>
        [Property("Funeral Home Address", Property.Types.Dictionary, "Decedent Disposition", "Funeral Home Address.", true, "http://hl7.org/fhir/us/vrdr/2019May/FuneralHome.html", false, 3)]
        [PropertyParam("addressLine1", "address, line one")]
        [PropertyParam("addressLine2", "address, line two")]
        [PropertyParam("addressCity", "address, city")]
        [PropertyParam("addressCounty", "address, county")]
        [PropertyParam("addressState", "address, state")]
        [PropertyParam("addressZip", "address, zip")]
        [PropertyParam("addressCountry", "address, country")]
        [FHIRPath("Bundle.entry.resource.where($this is Organization).where(type.coding.code='bus')", "address")]
        public Dictionary<string, string> FuneralHomeAddress
        {
            get
            {
                if (FuneralHome != null)
                {
                    return AddressToDict(FuneralHome.Address.FirstOrDefault());
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (FuneralHome == null)
                {
                    FuneralHome = new Organization();
                    FuneralHome.Id = "urn:uuid:" + Guid.NewGuid().ToString();
                    FuneralHome.Meta = new Meta();
                    string[] funeralhome_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Funeral-Home" };
                    FuneralHome.Meta.Profile = funeralhome_profile;
                    FuneralHome.Type.Add(new CodeableConcept(null, "bus", "Non-Healthcare Business or Corporation", null));
                    FuneralHome.Address.Add(DictToAddress(value));
                }
                else
                {
                    FuneralHome.Address.Clear();
                    FuneralHome.Address.Add(DictToAddress(value));
                }
            }
        }

        /// <summary>Name of Funeral Home.</summary>
        /// <value>the funeral home name.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.FuneralHomeName = "Smith Funeral Home";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Funeral Home Name: {ExampleDeathRecord.FuneralHomeName}");</para>
        /// </example>
        [Property("Funeral Home Name", Property.Types.String, "Decedent Disposition", "Name of Funeral Home.", true, "http://hl7.org/fhir/us/vrdr/2019May/FuneralHome.html", false, 3)]
        [FHIRPath("Bundle.entry.resource.where($this is Organization).where(type.coding.code='bus')", "name")]
        public string FuneralHomeName
        {
            get
            {
                if (FuneralHome != null)
                {
                    return FuneralHome.Name;
                }
                return null;
            }
            set
            {
                if (FuneralHome == null)
                {
                    FuneralHome = new Organization();
                    FuneralHome.Id = "urn:uuid:" + Guid.NewGuid().ToString();
                    FuneralHome.Meta = new Meta();
                    string[] funeralhome_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Funeral-Home" };
                    FuneralHome.Meta.Profile = funeralhome_profile;
                    FuneralHome.Type.Add(new CodeableConcept(null, "bus", "Non-Healthcare Business or Corporation", null));
                    FuneralHome.Name = value;
                }
                else
                {
                    FuneralHome.Name = value;
                }
            }
        }

        /// <summary>Disposition Location Address.</summary>
        /// <value>the disposition location address. A Dictionary representing an address, containing the following key/value pairs:
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
        /// <para>address.Add("addressLine1", "1234 Test Street");</para>
        /// <para>address.Add("addressLine2", "Unit 3");</para>
        /// <para>address.Add("addressCity", "Boston");</para>
        /// <para>address.Add("addressCounty", "Suffolk");</para>
        /// <para>address.Add("addressState", "Massachusetts");</para>
        /// <para>address.Add("addressZip", "12345");</para>
        /// <para>address.Add("addressCountry", "United States");</para>
        /// <para>ExampleDeathRecord.DispositionLocationAddress = address;</para>
        /// <para>// Getter:</para>
        /// <para>foreach(var pair in ExampleDeathRecord.DispositionLocationAddress)</para>
        /// <para>{</para>
        /// <para>  Console.WriteLine($"\DispositionLocationAddress key: {pair.Key}: value: {pair.Value}");</para>
        /// <para>};</para>
        /// </example>
        [Property("Disposition Location Address", Property.Types.Dictionary, "Decedent Disposition", "Disposition Location Address.", true, "http://hl7.org/fhir/us/vrdr/2019May/DispositionLocation.html", true, 3)]
        [PropertyParam("addressLine1", "address, line one")]
        [PropertyParam("addressLine2", "address, line two")]
        [PropertyParam("addressCity", "address, city")]
        [PropertyParam("addressCounty", "address, county")]
        [PropertyParam("addressState", "address, state")]
        [PropertyParam("addressZip", "address, zip")]
        [PropertyParam("addressCountry", "address, country")]
        [FHIRPath("Bundle.entry.resource.where($this is Location).where(meta.profile='http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Disposition-Location')", "address")]
        public Dictionary<string, string> DispositionLocationAddress
        {
            get
            {
                if (DispositionLocation != null)
                {
                    return AddressToDict(DispositionLocation.Address);
                }
                return EmptyAddrDict();
            }
            set
            {
                if (DispositionLocation == null)
                {
                    DispositionLocation = new Location();
                    DispositionLocation.Id = "urn:uuid:" + Guid.NewGuid().ToString();
                    DispositionLocation.Meta = new Meta();
                    string[] dispositionlocation_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Disposition-Location" };
                    DispositionLocation.Meta.Profile = dispositionlocation_profile;
                    DispositionLocation.Address = DictToAddress(value);
                }
                else
                {
                    DispositionLocation.Address = DictToAddress(value);
                }
            }
        }

        /// <summary>Name of Disposition Location.</summary>
        /// <value>the displosition location name.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.DispositionLocationName = "Bedford Cemetery";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Disposition Location Name: {ExampleDeathRecord.DispositionLocationName}");</para>
        /// </example>
        [Property("Disposition Location Name", Property.Types.String, "Decedent Disposition", "Name of Disposition Location.", true, "http://hl7.org/fhir/us/vrdr/2019May/DispositionLocation.html", false, 3)]
        [FHIRPath("Bundle.entry.resource.where($this is Location).where(meta.profile='http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Disposition-Location')", "name")]
        public string DispositionLocationName
        {
            get
            {
                if (DispositionLocation != null)
                {
                    return DispositionLocation.Name;
                }
                return null;
            }
            set
            {
                if (DispositionLocation == null)
                {
                    DispositionLocation = new Location();
                    DispositionLocation.Id = "urn:uuid:" + Guid.NewGuid().ToString();
                    DispositionLocation.Meta = new Meta();
                    string[] dispositionlocation_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Disposition-Location" };
                    DispositionLocation.Meta.Profile = dispositionlocation_profile;
                    DispositionLocation.Name = value;
                }
                else
                {
                    DispositionLocation.Name = value;
                }
            }
        }

        /// <summary>Decedent's Disposition Method.</summary>
        /// <value>the decedent's disposition method. A Dictionary representing a code, containing the following key/value pairs:
        /// <para>"code" - the code</para>
        /// <para>"system" - the code system this code belongs to</para>
        /// <para>"display" - a human readable meaning of the code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; dmethod = new Dictionary&lt;string, string&gt;();</para>
        /// <para>dmethod.Add("code", "449971000124106");</para>
        /// <para>dmethod.Add("system", "http://snomed.info/sct");</para>
        /// <para>dmethod.Add("display", "Burial");</para>
        /// <para>ExampleDeathRecord.DecedentDispositionMethod = dmethod;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Decedent Disposition Method: {ExampleDeathRecord.DecedentDispositionMethod['display']}");</para>
        /// </example>
        [Property("Decedent Disposition Method", Property.Types.Dictionary, "Decedent Disposition", "Decedent's Disposition Method.", true, "http://hl7.org/fhir/us/vrdr/2019May/DispositionLocation.html", true, 3)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='80905-3')", "")]
        public Dictionary<string, string> DecedentDispositionMethod
        {
            get
            {
                if (DispositionMethod != null && DispositionMethod.Value != null)
                {
                    return CodeableConceptToDict((CodeableConcept)DispositionMethod.Value);
                }
                return new Dictionary<string, string>() { { "code", "" }, { "system", "" }, { "display", "" } };
            }
            set
            {
                if (DispositionMethod == null)
                {
                    DispositionMethod = new Observation();
                    DispositionMethod.Id = "urn:uuid:" + Guid.NewGuid().ToString();
                    DispositionMethod.Meta = new Meta();
                    string[] dispositionmethod_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Decedent-Disposition-Method" };
                    DispositionMethod.Meta.Profile = dispositionmethod_profile;
                    DispositionMethod.Status = ObservationStatus.Final;
                    DispositionMethod.Code = new CodeableConcept("http://loinc.org", "80905-3", "Body disposition method", null);
                    DispositionMethod.Subject = new ResourceReference(Decedent.Id);
                    DispositionMethod.Performer.Add(new ResourceReference(Mortician.Id));
                    Extension extension = new Extension();
                    extension.Url = "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Disposition-Location";
                    extension.Value = new ResourceReference(DispositionLocation.Id);
                    DispositionMethod.Extension.Add(extension);
                    DispositionMethod.Value = DictToCodeableConcept(value);
                    AddReferenceToComposition(DispositionMethod.Id);
                    Bundle.AddResourceEntry(DispositionMethod, DispositionMethod.Id);
                }
                else
                {
                    DispositionMethod.Value = DictToCodeableConcept(value);
                }
            }
        }


        /////////////////////////////////////////////////////////////////////////////////
        //
        // Record Properties: Death Investigation
        //
        /////////////////////////////////////////////////////////////////////////////////

        /// <summary>Autopsy Performed Indicator.</summary>
        /// <value>autopsy performed indicator. A Dictionary representing a code, containing the following key/value pairs:
        /// <para>"code" - the code</para>
        /// <para>"system" - the code system this code belongs to</para>
        /// <para>"display" - a human readable meaning of the code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; code = new Dictionary&lt;string, string&gt;();</para>
        /// <para>code.Add("code", "Y");</para>
        /// <para>code.Add("system", "http://hl7.org/fhir/ValueSet/v2-0532");</para>
        /// <para>code.Add("display", "Yes");</para>
        /// <para>ExampleDeathRecord.AutopsyPerformedIndicator = code;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Autopsy Performed Indicator: {ExampleDeathRecord.AutopsyPerformedIndicator['display']}");</para>
        /// </example>
        [Property("Autopsy Performed Indicator", Property.Types.Dictionary, "Death Investigation", "Autopsy Performed Indicator.", true, "http://hl7.org/fhir/us/vrdr/2019May/AutopsyPerformedIndicator.html", true, 4)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='85699-7')", "")]
        public Dictionary<string, string> AutopsyPerformedIndicator
        {
            get
            {
                if (AutopsyPerformed != null && AutopsyPerformed.Value != null)
                {
                    return CodeableConceptToDict((CodeableConcept)AutopsyPerformed.Value);
                }
                return new Dictionary<string, string>() { { "code", "" }, { "system", "" }, { "display", "" } };
            }
            set
            {
                if (AutopsyPerformed == null)
                {
                    AutopsyPerformed = new Observation();
                    AutopsyPerformed.Id = "urn:uuid:" + Guid.NewGuid().ToString();
                    AutopsyPerformed.Meta = new Meta();
                    string[] autopsyperformed_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Autopsy-Performed-Indicator" };
                    AutopsyPerformed.Meta.Profile = autopsyperformed_profile;
                    AutopsyPerformed.Status = ObservationStatus.Final;
                    AutopsyPerformed.Code = new CodeableConcept("http://loinc.org", "85699-7", "Autopsy was performed", null);
                    AutopsyPerformed.Subject = new ResourceReference(Decedent.Id);
                    AutopsyPerformed.Value = DictToCodeableConcept(value);
                    AddReferenceToComposition(AutopsyPerformed.Id);
                    Bundle.AddResourceEntry(AutopsyPerformed, AutopsyPerformed.Id);
                }
                else
                {
                    AutopsyPerformed.Value = DictToCodeableConcept(value);
                }
            }
        }

        /// <summary>Decedent's Date/Time of Death.</summary>
        /// <value>the decedent's date and time of death</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.DateOfDeath = "2018-02-19T16:48:06.4988220-05:00";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Decedent Date of Death: {ExampleDeathRecord.DateOfDeath}");</para>
        /// </example>
        [Property("Date Of Death", Property.Types.StringDateTime, "Death Investigation", "Decedent's Date/Time of Death.", true, "http://hl7.org/fhir/us/vrdr/2019May/DeathDate.html", true, 4)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='81956-5')", "")]
        public string DateOfDeath
        {
            get
            {
                if (DeathDateObs != null)
                {
                    return Convert.ToString(DeathDateObs.Effective);
                }
                return null;
            }
            set
            {
                if (DeathDateObs == null)
                {
                    DeathDateObs = new Observation();
                    DeathDateObs.Id = "urn:uuid:" + Guid.NewGuid().ToString();
                    DeathDateObs.Meta = new Meta();
                    string[] deathdate_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Death-Date" };
                    DeathDateObs.Meta.Profile = deathdate_profile;
                    DeathDateObs.Status = ObservationStatus.Final;
                    DeathDateObs.Code = new CodeableConcept("http://loinc.org", "81956-5", "Date and time of death", null);
                    DeathDateObs.Subject = new ResourceReference(Decedent.Id);
                    DeathDateObs.Performer.Add(new ResourceReference(Certifier.Id));
                    DeathDateObs.Effective = new FhirDateTime(value);
                    AddReferenceToComposition(DeathDateObs.Id);
                    Bundle.AddResourceEntry(DeathDateObs, DeathDateObs.Id);
                }
                else
                {
                    DeathDateObs.Effective = new FhirDateTime(value);
                }
            }
        }

        /// <summary>Decedent's Date/Time of Death Pronouncement.</summary>
        /// <value>the decedent's date and time of death pronouncement</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.DateOfDeathPronouncement = "2018-02-20T16:48:06.4988220-05:00";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Decedent Date of Death Pronouncement: {ExampleDeathRecord.DateOfDeathPronouncement}");</para>
        /// </example>
        [Property("Date Of Death Pronouncement", Property.Types.StringDateTime, "Death Investigation", "Decedent's Date/Time of Death Pronouncement.", true, "http://hl7.org/fhir/us/vrdr/2019May/DeathDate.html", false, 4)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='80616-6')", "")]
        public string DateOfDeathPronouncement
        {
            get
            {
                if (DeathDateObs != null)
                {
                    Observation.ComponentComponent component = DeathDateObs.Component.FirstOrDefault();
                    if (component != null)
                    {
                        return Convert.ToString(component.Value);
                    }
                }
                return null;
            }
            set
            {
                if (DeathDateObs == null)
                {
                    DeathDateObs = new Observation();
                    DeathDateObs.Id = "urn:uuid:" + Guid.NewGuid().ToString();
                    DeathDateObs.Meta = new Meta();
                    string[] deathdate_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Death-Date" };
                    DeathDateObs.Meta.Profile = deathdate_profile;
                    DeathDateObs.Status = ObservationStatus.Final;
                    DeathDateObs.Code = new CodeableConcept("http://loinc.org", "81956-5", "Date and time of death", null);
                    DeathDateObs.Subject = new ResourceReference(Decedent.Id);
                    DeathDateObs.Performer.Add(new ResourceReference(Certifier.Id));
                    Observation.ComponentComponent component = new Observation.ComponentComponent();
                    component.Code = new CodeableConcept("http://loinc.org", "80616-6", "Date and time pronounced dead", null);
                    component.Value = new FhirDateTime(value);
                    DeathDateObs.Component.Add(component);
                    AddReferenceToComposition(DeathDateObs.Id);
                    Bundle.AddResourceEntry(DeathDateObs, DeathDateObs.Id);
                }
                else
                {
                    DeathDateObs.Component.Clear();
                    Observation.ComponentComponent component = new Observation.ComponentComponent();
                    component.Code = new CodeableConcept("http://loinc.org", "80616-6", "Date and time pronounced dead", null);
                    component.Value = new FhirDateTime(value);
                    DeathDateObs.Component.Add(component);
                }
            }
        }

        /// <summary>Autopsy Results Available.</summary>
        /// <value>autopsy results available. A Dictionary representing a code, containing the following key/value pairs:
        /// <para>"code" - the code</para>
        /// <para>"system" - the code system this code belongs to</para>
        /// <para>"display" - a human readable meaning of the code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; code = new Dictionary&lt;string, string&gt;();</para>
        /// <para>code.Add("code", "Y");</para>
        /// <para>code.Add("system", "http://hl7.org/fhir/ValueSet/v2-0532");</para>
        /// <para>code.Add("display", "Yes");</para>
        /// <para>ExampleDeathRecord.AutopsyResultsAvailable = code;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Autopsy Results Available: {ExampleDeathRecord.AutopsyResultsAvailable['display']}");</para>
        /// </example>
        [Property("Autopsy Results Available", Property.Types.Dictionary, "Death Investigation", "Autopsy Results Available.", true, "http://hl7.org/fhir/us/vrdr/2019May/AutopsyPerformedIndicator.html", true, 4)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='85699-7')", "")]
        public Dictionary<string, string> AutopsyResultsAvailable
        {
            get
            {
                if (AutopsyPerformed != null && AutopsyPerformed.Component.Count() > 0)
                {
                    return CodeableConceptToDict((CodeableConcept)AutopsyPerformed.Component.First().Value);
                }
                return new Dictionary<string, string>() { { "code", "" }, { "system", "" }, { "display", "" } };
            }
            set
            {
                if (AutopsyPerformed == null)
                {
                    AutopsyPerformed = new Observation();
                    AutopsyPerformed.Id = "urn:uuid:" + Guid.NewGuid().ToString();
                    AutopsyPerformed.Meta = new Meta();
                    string[] autopsyperformed_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Autopsy-Performed-Indicator" };
                    AutopsyPerformed.Meta.Profile = autopsyperformed_profile;
                    AutopsyPerformed.Status = ObservationStatus.Final;
                    AutopsyPerformed.Code = new CodeableConcept("http://loinc.org", "85699-7", "Autopsy was performed", null);
                    AutopsyPerformed.Subject = new ResourceReference(Decedent.Id);
                    Observation.ComponentComponent component = new Observation.ComponentComponent();
                    component.Code = new CodeableConcept("http://loinc.org", "69436-4", "Autopsy results available", null);
                    component.Value = DictToCodeableConcept(value);
                    AutopsyPerformed.Component.Clear();
                    AutopsyPerformed.Component.Add(component);
                    AddReferenceToComposition(AutopsyPerformed.Id);
                    Bundle.AddResourceEntry(AutopsyPerformed, AutopsyPerformed.Id);
                }
                else
                {
                    Observation.ComponentComponent component = new Observation.ComponentComponent();
                    component.Code = new CodeableConcept("http://loinc.org", "69436-4", "Autopsy results available", null);
                    component.Value = DictToCodeableConcept(value);
                    AutopsyPerformed.Component.Clear();
                    AutopsyPerformed.Component.Add(component);
                }
            }
        }

        /// <summary>Location of Death.</summary>
        /// <value>location of death. A Dictionary representing an address, containing the following key/value pairs:
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
        /// <para>address.Add("addressLine1", "123456789 Test Street");</para>
        /// <para>address.Add("addressLine2", "Unit 3");</para>
        /// <para>address.Add("addressCity", "Boston");</para>
        /// <para>address.Add("addressCounty", "Suffolk");</para>
        /// <para>address.Add("addressState", "Massachusetts");</para>
        /// <para>address.Add("addressZip", "12345");</para>
        /// <para>address.Add("addressCountry", "United States");</para>
        /// <para>ExampleDeathRecord.DeathLocationAddress = address;</para>
        /// <para>// Getter:</para>
        /// <para>foreach(var pair in ExampleDeathRecord.DeathLocationAddress)</para>
        /// <para>{</para>
        /// <para>  Console.WriteLine($"\DeathLocationAddress key: {pair.Key}: value: {pair.Value}");</para>
        /// <para>};</para>
        /// </example>
        [Property("Death Location Address", Property.Types.Dictionary, "Death Investigation", "Location of Death.", true, "http://hl7.org/fhir/us/vrdr/2019May/DeathLocation.html", true, 4)]
        [PropertyParam("addressLine1", "address, line one")]
        [PropertyParam("addressLine2", "address, line two")]
        [PropertyParam("addressCity", "address, city")]
        [PropertyParam("addressCounty", "address, county")]
        [PropertyParam("addressState", "address, state")]
        [PropertyParam("addressZip", "address, zip")]
        [PropertyParam("addressCountry", "address, country")]
        [FHIRPath("Bundle.entry.resource.where($this is Location).where(meta.profile='http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Death-Location')", "address")]
        public Dictionary<string, string> DeathLocationAddress
        {
            get
            {
                if (DeathLocationLoc != null)
                {
                    return AddressToDict(DeathLocationLoc.Address);
                }
                return EmptyAddrDict();
            }
            set
            {
                if (DeathLocationLoc == null)
                {
                    DeathLocationLoc = new Location();
                    DeathLocationLoc.Id = "urn:uuid:" + Guid.NewGuid().ToString();
                    DeathLocationLoc.Meta = new Meta();
                    string[] deathlocation_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Death-Location" };
                    DeathLocationLoc.Meta.Profile = deathlocation_profile;
                    DeathLocationLoc.Address = DictToAddress(value);
                    AddReferenceToComposition(DeathLocationLoc.Id);
                    Bundle.AddResourceEntry(DeathLocationLoc, DeathLocationLoc.Id);
                }
                else
                {
                    DeathLocationLoc.Address = DictToAddress(value);
                }
            }
        }

        /// <summary>Name of Death Location.</summary>
        /// <value>the death location name.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.DeathLocationName = "Example Death Location Name";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Death Location Name: {ExampleDeathRecord.DeathLocationName}");</para>
        /// </example>
        [Property("Death Location Name", Property.Types.String, "Death Investigation", "Name of Death Location.", true, "http://hl7.org/fhir/us/vrdr/2019May/DeathLocation.html", false, 4)]
        [FHIRPath("Bundle.entry.resource.where($this is Location).where(meta.profile='http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Death-Location')", "name")]
        public string DeathLocationName
        {
            get
            {
                if (DeathLocationLoc != null)
                {
                    return DeathLocationLoc.Name;
                }
                return null;
            }
            set
            {
                if (DeathLocationLoc == null)
                {
                    DeathLocationLoc = new Location();
                    DeathLocationLoc.Id = "urn:uuid:" + Guid.NewGuid().ToString();
                    DeathLocationLoc.Meta = new Meta();
                    string[] deathlocation_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Death-Location" };
                    DeathLocationLoc.Meta.Profile = deathlocation_profile;
                    DeathLocationLoc.Name = value;
                    AddReferenceToComposition(DeathLocationLoc.Id);
                    Bundle.AddResourceEntry(DeathLocationLoc, DeathLocationLoc.Id);
                }
                else
                {
                    DeathLocationLoc.Name = value;
                }
            }
        }

        /// <summary>Description of Death Location.</summary>
        /// <value>the death location description.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.DeathLocationDescription = "Bedford Cemetery";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Death Location Description: {ExampleDeathRecord.DeathLocationDescription}");</para>
        /// </example>
        [Property("Death Location Description", Property.Types.String, "Death Investigation", "Description of Death Location.", true, "http://hl7.org/fhir/us/vrdr/2019May/DeathLocation.html", false, 4)]
        [FHIRPath("Bundle.entry.resource.where($this is Location).where(meta.profile='http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Death-Location')", "description")]
        public string DeathLocationDescription
        {
            get
            {
                if (DeathLocationLoc != null)
                {
                    return DeathLocationLoc.Description;
                }
                return null;
            }
            set
            {
                if (DeathLocationLoc == null)
                {
                    DeathLocationLoc = new Location();
                    DeathLocationLoc.Id = "urn:uuid:" + Guid.NewGuid().ToString();
                    DeathLocationLoc.Meta = new Meta();
                    string[] deathlocation_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Death-Location" };
                    DeathLocationLoc.Meta.Profile = deathlocation_profile;
                    DeathLocationLoc.Description = value;
                    AddReferenceToComposition(DeathLocationLoc.Id);
                    Bundle.AddResourceEntry(DeathLocationLoc, DeathLocationLoc.Id);
                }
                else
                {
                    DeathLocationLoc.Description = value;
                }
            }
        }

        /// <summary>Age At Death.</summary>
        /// <value>decedent's age at time of death. A Dictionary representing a length of time, containing the following key/value pairs:
        /// <para>"value" - the quantity value</para>
        /// <para>"system" - the quantity unit</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; age = new Dictionary&lt;string, string&gt;();</para>
        /// <para>age.Add("value", "100");</para>
        /// <para>age.Add("unit", "a"); // USE: http://hl7.org/fhir/stu3/valueset-age-units.html</para>
        /// <para>ExampleDeathRecord.AgeAtDeath = age;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Age At Death: {ExampleDeathRecord.AgeAtDeath['unit']} years");</para>
        /// </example>
        [Property("Age At Death", Property.Types.Dictionary, "Death Investigation", "Age At Death.", true, "http://hl7.org/fhir/us/vrdr/2019May/DecedentAge.html", true, 4)]
        [PropertyParam("value", "The quantity value.")]
        [PropertyParam("unit", "The quantity unit.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='30525-0')", "")]
        public Dictionary<string, string> AgeAtDeath
        {
            get
            {
                if (AgeAtDeathObs != null && AgeAtDeathObs.Value != null)
                {
                    Dictionary<string, string> age = new Dictionary<string, string>();
                    age.Add("value", Convert.ToString(((Quantity)AgeAtDeathObs.Value).Value));
                    age.Add("unit", ((Quantity)AgeAtDeathObs.Value).Unit);
                    return age;
                }
                return new Dictionary<string, string>() { { "value", "" }, { "unit", "" } };
            }
            set
            {
                if (AgeAtDeathObs == null)
                {
                    AgeAtDeathObs = new Observation();
                    AgeAtDeathObs.Id = "urn:uuid:" + Guid.NewGuid().ToString();
                    AgeAtDeathObs.Meta = new Meta();
                    string[] age_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Decedent-Age" };
                    AgeAtDeathObs.Meta.Profile = age_profile;
                    AgeAtDeathObs.Status = ObservationStatus.Final;
                    AgeAtDeathObs.Code = new CodeableConcept("http://loinc.org", "30525-0", "Age", null);
                    AgeAtDeathObs.Subject = new ResourceReference(Decedent.Id);
                    Quantity quant = new Quantity();
                    quant.Value = Convert.ToDecimal(GetValue(value, "value"));
                    quant.Unit = GetValue(value, "unit");
                    AgeAtDeathObs.Value = quant;
                    AddReferenceToComposition(AgeAtDeathObs.Id);
                    Bundle.AddResourceEntry(AgeAtDeathObs, AgeAtDeathObs.Id);
                }
                else
                {
                    Quantity quant = new Quantity();
                    quant.Value = Convert.ToDecimal(GetValue(value, "value"));
                    quant.Unit = GetValue(value, "unit");
                    AgeAtDeathObs.Value = quant;
                }
            }
        }

        /// <summary>Pregnanacy Status At Death.</summary>
        /// <value>pregnanacy status at death. A Dictionary representing a code, containing the following key/value pairs:
        /// <para>"code" - the code</para>
        /// <para>"system" - the code system this code belongs to</para>
        /// <para>"display" - a human readable meaning of the code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; code = new Dictionary&lt;string, string&gt;();</para>
        /// <para>code.Add("code", "PHC1260");</para>
        /// <para>code.Add("system", "http://hl7.org/fhir/stu3/valueset-PregnancyStatusVS");</para>
        /// <para>code.Add("display", "Not pregnant within past year");</para>
        /// <para>ExampleDeathRecord.PregnanacyStatus = code;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Pregnanacy Status: {ExampleDeathRecord.PregnanacyStatus['display']}");</para>
        /// </example>
        [Property("Pregnanacy Status", Property.Types.Dictionary, "Death Investigation", "Pregnanacy Status At Death.", true, "http://hl7.org/fhir/us/vrdr/2019May/DecedentPregnancy.html", true, 4)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='69442-2')", "")]
        public Dictionary<string, string> PregnanacyStatus
        {
            get
            {
                if (PregnancyObs != null && PregnancyObs.Value != null)
                {
                    return CodeableConceptToDict((CodeableConcept)PregnancyObs.Value);
                }
                return new Dictionary<string, string>() { { "code", "" }, { "system", "" }, { "display", "" } };
            }
            set
            {
                if (PregnancyObs == null)
                {
                    PregnancyObs = new Observation();
                    PregnancyObs.Id = "urn:uuid:" + Guid.NewGuid().ToString();
                    PregnancyObs.Meta = new Meta();
                    string[] p_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Decedent-Pregnancy" };
                    PregnancyObs.Meta.Profile = p_profile;
                    PregnancyObs.Status = ObservationStatus.Final;
                    PregnancyObs.Code = new CodeableConcept("http://loinc.org", "69442-2", "Timing of recent pregnancy in relation to death", null);
                    PregnancyObs.Subject = new ResourceReference(Decedent.Id);
                    PregnancyObs.Value = DictToCodeableConcept(value);
                    AddReferenceToComposition(PregnancyObs.Id);
                    Bundle.AddResourceEntry(PregnancyObs, PregnancyObs.Id);
                }
                else
                {
                    PregnancyObs.Value = DictToCodeableConcept(value);
                }
            }
        }

        /// <summary>Transportation Role in death.</summary>
        /// <value>transportation role in death. A Dictionary representing a code, containing the following key/value pairs:
        /// <para>"code" - the code</para>
        /// <para>"system" - the code system this code belongs to</para>
        /// <para>"display" - a human readable meaning of the code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; code = new Dictionary&lt;string, string&gt;();</para>
        /// <para>code.Add("code", "example-code");</para>
        /// <para>code.Add("system", "http://hl7.org/fhir/stu3/valueset-TransportationRelationships");</para>
        /// <para>code.Add("display", "Example Code");</para>
        /// <para>ExampleDeathRecord.TransportationRole = code;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Transportation Role: {ExampleDeathRecord.TransportationRole['display']}");</para>
        /// </example>
        [Property("Transportation Role", Property.Types.Dictionary, "Death Investigation", "Transportation Role in death.", true, "http://hl7.org/fhir/us/vrdr/2019May/DecedentTransportationRole.html", true, 4)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='69451-3')", "")]
        public Dictionary<string, string> TransportationRole
        {
            get
            {
                if (TransportationRoleObs != null && TransportationRoleObs.Value != null)
                {
                    return CodeableConceptToDict((CodeableConcept)TransportationRoleObs.Value);
                }
                return new Dictionary<string, string>() { { "code", "" }, { "system", "" }, { "display", "" } };
            }
            set
            {
                if (TransportationRoleObs == null)
                {
                    TransportationRoleObs = new Observation();
                    TransportationRoleObs.Id = "urn:uuid:" + Guid.NewGuid().ToString();
                    TransportationRoleObs.Meta = new Meta();
                    string[] t_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Decedent-Transportation-Role" };
                    TransportationRoleObs.Meta.Profile = t_profile;
                    TransportationRoleObs.Status = ObservationStatus.Final;
                    TransportationRoleObs.Code = new CodeableConcept("http://loinc.org", "69451-3", "Transportation role of decedent", null);
                    TransportationRoleObs.Subject = new ResourceReference(Decedent.Id);
                    TransportationRoleObs.Value = DictToCodeableConcept(value);
                    AddReferenceToComposition(TransportationRoleObs.Id);
                    Bundle.AddResourceEntry(TransportationRoleObs, TransportationRoleObs.Id);
                }
                else
                {
                    TransportationRoleObs.Value = DictToCodeableConcept(value);
                }
            }
        }

        /// <summary>Examiner Contacted.</summary>
        /// <value>if a medical examiner was contacted.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.ExaminerContacted = false;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Examiner Contacted: {ExampleDeathRecord.ExaminerContacted}");</para>
        /// </example>
        [Property("Examiner Contacted", Property.Types.Bool, "Death Investigation", "Examiner Contacted.", true, "http://hl7.org/fhir/us/vrdr/2019May/ExaminerContacted.html", true, 4)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='74497-9')", "")]
        public bool ExaminerContacted
        {
            get
            {
                if (ExaminerContactedObs != null && ExaminerContactedObs.Value != null)
                {
                    return ((FhirBoolean)ExaminerContactedObs.Value).Value == true;
                }
                return false;
            }
            set
            {
                if (ExaminerContactedObs == null)
                {
                    ExaminerContactedObs = new Observation();
                    ExaminerContactedObs.Id = "urn:uuid:" + Guid.NewGuid().ToString();
                    ExaminerContactedObs.Meta = new Meta();
                    string[] ec_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Examiner-Contacted" };
                    ExaminerContactedObs.Meta.Profile = ec_profile;
                    ExaminerContactedObs.Status = ObservationStatus.Final;
                    ExaminerContactedObs.Code = new CodeableConcept("http://loinc.org", "74497-9", "Medical examiner or coroner was contacted", null);
                    ExaminerContactedObs.Subject = new ResourceReference(Decedent.Id);
                    ExaminerContactedObs.Value = new FhirBoolean(value);
                    AddReferenceToComposition(ExaminerContactedObs.Id);
                    Bundle.AddResourceEntry(ExaminerContactedObs, ExaminerContactedObs.Id);
                }
                else
                {
                    ExaminerContactedObs.Value = new FhirBoolean(value);
                }
            }
        }

        /// <summary>Location of Injury.</summary>
        /// <value>location of injury. A Dictionary representing an address, containing the following key/value pairs:
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
        /// <para>address.Add("addressLine1", "123456 Test Street");</para>
        /// <para>address.Add("addressLine2", "Unit 3");</para>
        /// <para>address.Add("addressCity", "Boston");</para>
        /// <para>address.Add("addressCounty", "Suffolk");</para>
        /// <para>address.Add("addressState", "Massachusetts");</para>
        /// <para>address.Add("addressZip", "12345");</para>
        /// <para>address.Add("addressCountry", "United States");</para>
        /// <para>ExampleDeathRecord.InjuryLocationAddress = address;</para>
        /// <para>// Getter:</para>
        /// <para>foreach(var pair in ExampleDeathRecord.InjuryLocationAddress)</para>
        /// <para>{</para>
        /// <para>  Console.WriteLine($"\InjuryLocationAddress key: {pair.Key}: value: {pair.Value}");</para>
        /// <para>};</para>
        /// </example>
        [Property("Injury Location Address", Property.Types.Dictionary, "Death Investigation", "Location of Injury.", true, "http://hl7.org/fhir/us/vrdr/2019May/InjuryLocation.html", true, 4)]
        [PropertyParam("addressLine1", "address, line one")]
        [PropertyParam("addressLine2", "address, line two")]
        [PropertyParam("addressCity", "address, city")]
        [PropertyParam("addressCounty", "address, county")]
        [PropertyParam("addressState", "address, state")]
        [PropertyParam("addressZip", "address, zip")]
        [PropertyParam("addressCountry", "address, country")]
        [FHIRPath("Bundle.entry.resource.where($this is Location).where(meta.profile='http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Injury-Location')", "address")]
        public Dictionary<string, string> InjuryLocationAddress
        {
            get
            {
                if (InjuryLocationLoc != null)
                {
                    return AddressToDict(InjuryLocationLoc.Address);
                }
                return EmptyAddrDict();
            }
            set
            {
                if (InjuryLocationLoc == null)
                {
                    InjuryLocationLoc = new Location();
                    InjuryLocationLoc.Id = "urn:uuid:" + Guid.NewGuid().ToString();
                    InjuryLocationLoc.Meta = new Meta();
                    string[] injurylocation_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Injury-Location" };
                    InjuryLocationLoc.Meta.Profile = injurylocation_profile;
                    InjuryLocationLoc.Address = DictToAddress(value);
                    AddReferenceToComposition(InjuryLocationLoc.Id);
                    Bundle.AddResourceEntry(InjuryLocationLoc, InjuryLocationLoc.Id);
                }
                else
                {
                    InjuryLocationLoc.Address = DictToAddress(value);
                }
            }
        }

        /// <summary>Name of Injury Location.</summary>
        /// <value>the injury location name.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.InjuryLocationName = "Bedford Cemetery";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Injury Location Name: {ExampleDeathRecord.InjuryLocationName}");</para>
        /// </example>
        [Property("Injury Location Name", Property.Types.String, "Death Investigation", "Name of Injury Location.", true, "http://hl7.org/fhir/us/vrdr/2019May/InjuryLocation.html", true, 4)]
        [FHIRPath("Bundle.entry.resource.where($this is Location).where(meta.profile='http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Injury-Location')", "name")]
        public string InjuryLocationName
        {
            get
            {
                if (InjuryLocationLoc != null)
                {
                    return InjuryLocationLoc.Name;
                }
                return null;
            }
            set
            {
                if (InjuryLocationLoc == null)
                {
                    InjuryLocationLoc = new Location();
                    InjuryLocationLoc.Id = "urn:uuid:" + Guid.NewGuid().ToString();
                    InjuryLocationLoc.Meta = new Meta();
                    string[] injurylocation_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Injury-Location" };
                    InjuryLocationLoc.Meta.Profile = injurylocation_profile;
                    InjuryLocationLoc.Name = value;
                    AddReferenceToComposition(InjuryLocationLoc.Id);
                    Bundle.AddResourceEntry(InjuryLocationLoc, InjuryLocationLoc.Id);
                }
                else
                {
                    InjuryLocationLoc.Name = value;
                }
            }
        }

        /// <summary>Description of Injury Location.</summary>
        /// <value>the injury location description.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.InjuryLocationDescription = "Bedford Cemetery";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Injury Location Description: {ExampleDeathRecord.InjuryLocationDescription}");</para>
        /// </example>
        [Property("Injury Location Description", Property.Types.String, "Death Investigation", "Description of Injury Location.", true, "http://hl7.org/fhir/us/vrdr/2019May/InjuryLocation.html", true, 4)]
        [FHIRPath("Bundle.entry.resource.where($this is Location).where(meta.profile='http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Injury-Location')", "description")]
        public string InjuryLocationDescription
        {
            get
            {
                if (InjuryLocationLoc != null)
                {
                    return InjuryLocationLoc.Description;
                }
                return null;
            }
            set
            {
                if (InjuryLocationLoc == null)
                {
                    InjuryLocationLoc = new Location();
                    InjuryLocationLoc.Id = "urn:uuid:" + Guid.NewGuid().ToString();
                    InjuryLocationLoc.Meta = new Meta();
                    string[] injurylocation_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Injury-Location" };
                    InjuryLocationLoc.Meta.Profile = injurylocation_profile;
                    InjuryLocationLoc.Description = value;
                    AddReferenceToComposition(InjuryLocationLoc.Id);
                    Bundle.AddResourceEntry(InjuryLocationLoc, InjuryLocationLoc.Id);
                }
                else
                {
                    InjuryLocationLoc.Description = value;
                }
            }
        }

        /// <summary>Date/Time of Injury.</summary>
        /// <value>the date and time of injury</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.InjuryDate = "2018-02-19T16:48:06.4988220-05:00";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Date of Injury: {ExampleDeathRecord.InjuryDate}");</para>
        /// </example>
        [Property("Injury Date", Property.Types.StringDateTime, "Death Investigation", "Date/Time of Injury.", true, "http://hl7.org/fhir/us/vrdr/2019May/InjuryIncident.html", true, 4)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='11374-6')", "")]
        public string InjuryDate
        {
            get
            {
                if (InjuryIncidentObs != null && InjuryIncidentObs.Effective != null)
                {
                    return Convert.ToString(InjuryIncidentObs.Effective);
                }
                return null;
            }
            set
            {
                if (InjuryIncidentObs == null)
                {
                    InjuryIncidentObs = new Observation();
                    InjuryIncidentObs.Id = "urn:uuid:" + Guid.NewGuid().ToString();
                    InjuryIncidentObs.Meta = new Meta();
                    string[] iio_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Injury-Incident" };
                    InjuryIncidentObs.Meta.Profile = iio_profile;
                    InjuryIncidentObs.Status = ObservationStatus.Final;
                    InjuryIncidentObs.Code = new CodeableConcept("http://loinc.org", "11374-6", "Injury incident description", null);
                    InjuryIncidentObs.Subject = new ResourceReference(Decedent.Id);
                    InjuryIncidentObs.Effective = new FhirDateTime(value);
                    AddReferenceToComposition(InjuryIncidentObs.Id);
                    Bundle.AddResourceEntry(InjuryIncidentObs, InjuryIncidentObs.Id);
                }
                else
                {
                    InjuryIncidentObs.Effective = new FhirDateTime(value);
                }
            }
        }

        /// <summary>Tobacco Use Contributed To Death.</summary>
        /// <value>if tobacco use contributed to death. A Dictionary representing a code, containing the following key/value pairs:
        /// <para>"code" - the code</para>
        /// <para>"system" - the code system this code belongs to</para>
        /// <para>"display" - a human readable meaning of the code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; code = new Dictionary&lt;string, string&gt;();</para>
        /// <para>code.Add("code", "Y");</para>
        /// <para>code.Add("system", "http://hl7.org/fhir/ValueSet/v2-0532");</para>
        /// <para>code.Add("display", "Yes");</para>
        /// <para>ExampleDeathRecord.TobaccoUse = code;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Tobacco Use: {ExampleDeathRecord.TobaccoUse['display']}");</para>
        /// </example>
        [Property("Tobacco Use", Property.Types.Dictionary, "Death Investigation", "If Tobacco Use Contributed To Death.", true, "http://hl7.org/fhir/us/vrdr/2019May/TobaccoUseContributedToDeath.html", true, 4)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='69443-0')", "")]
        public Dictionary<string, string> TobaccoUse
        {
            get
            {
                if (TobaccoUseObs != null && TobaccoUseObs.Value != null)
                {
                    return CodeableConceptToDict((CodeableConcept)TobaccoUseObs.Value);
                }
                return new Dictionary<string, string>() { { "code", "" }, { "system", "" }, { "display", "" } };
            }
            set
            {
                if (TobaccoUseObs == null)
                {
                    TobaccoUseObs = new Observation();
                    TobaccoUseObs.Id = "urn:uuid:" + Guid.NewGuid().ToString();
                    TobaccoUseObs.Meta = new Meta();
                    string[] tb_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Tobacco-Use-Contributed-To-Death" };
                    TobaccoUseObs.Meta.Profile = tb_profile;
                    TobaccoUseObs.Status = ObservationStatus.Final;
                    TobaccoUseObs.Code = new CodeableConcept("http://loinc.org", "69443-0", "Did tobacco use contribute to death", null);
                    TobaccoUseObs.Subject = new ResourceReference(Decedent.Id);
                    TobaccoUseObs.Value = DictToCodeableConcept(value);
                    AddReferenceToComposition(TobaccoUseObs.Id);
                    Bundle.AddResourceEntry(TobaccoUseObs, TobaccoUseObs.Id);
                }
                else
                {
                    TobaccoUseObs.Value = DictToCodeableConcept(value);
                }
            }
        }


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

        /// <summary>Restores class references from a newly parsed record.</summary>
        private void RestoreReferences()
        {
            // Grab Composition
            var compositionEntry = Bundle.Entry.FirstOrDefault( entry => entry.Resource.ResourceType == ResourceType.Composition );
            if (compositionEntry != null)
            {
                Composition = (Composition)compositionEntry.Resource;
            }
            else
            {
                throw new System.ArgumentException("Failed to find a Composition. The first entry in the FHIR Bundle should be a Composition.");
            }

            // Grab Patient
            if (Composition.Subject == null || String.IsNullOrWhiteSpace(Composition.Subject.Reference))
            {
                throw new System.ArgumentException("The Composition is missing a subject (a reference to the Decedent resource).");
            }
            var patientEntry = Bundle.Entry.FirstOrDefault( entry => entry.Resource.ResourceType == ResourceType.Patient );
            if (patientEntry != null)
            {
                Decedent = (Patient)patientEntry.Resource;
            }
            else
            {
                throw new System.ArgumentException("Failed to find a Decedent (Patient). The second entry in the FHIR Bundle is usually the Decedent (Patient).");
            }

            // Grab Certifier
            if (Composition.Attester == null || Composition.Attester.FirstOrDefault() == null || Composition.Attester.First().Party == null || String.IsNullOrWhiteSpace(Composition.Attester.First().Party.Reference))
            {
                throw new System.ArgumentException("The Composition is missing an attestor (a reference to the Certifier/Practitioner resource).");
            }
            var practitionerEntry = Bundle.Entry.FirstOrDefault( entry => entry.Resource.ResourceType == ResourceType.Practitioner && entry.FullUrl == Composition.Attester.First().Party.Reference);
            if (practitionerEntry != null)
            {
                Certifier = (Practitioner)practitionerEntry.Resource;
            }
            else
            {
                throw new System.ArgumentException("Failed to find a Certifier (Practitioner). The third entry in the FHIR Bundle is usually the Certifier (Practitioner). Either the Certifier is missing from the Bundle, or the attestor reference specified in the Composition is incorrect.");
            }

            // Grab Mortician
            var morticianEntry = Bundle.Entry.FirstOrDefault( entry => entry.Resource.ResourceType == ResourceType.Practitioner && ((Practitioner)entry.Resource).Id != Certifier.Id );
            if (morticianEntry != null)
            {
                Mortician = (Practitioner)morticianEntry.Resource;
            }

            // Grab Death Certification
            var procedureEntry = Bundle.Entry.FirstOrDefault( entry => entry.Resource.ResourceType == ResourceType.Procedure );
            if (procedureEntry != null)
            {
                DeathCertification = (Procedure)procedureEntry.Resource;
            }

            // Grab Interested Party
            var interestedParty = Bundle.Entry.FirstOrDefault( entry => entry.Resource.ResourceType == ResourceType.Organization && ((Organization)entry.Resource).Active != null );
            if (interestedParty != null)
            {
                InterestedParty = (Organization)interestedParty.Resource;
            }

            // Grab Funeral Home
            var funeralHome = Bundle.Entry.FirstOrDefault( entry => entry.Resource.ResourceType == ResourceType.Organization && (InterestedParty == null || ((Organization)entry.Resource).Id != InterestedParty.Id) );
            if (funeralHome != null)
            {
                FuneralHome = (Organization)funeralHome.Resource;
            }

            // Grab Funeral Home Director
            var funeralHomeDirector = Bundle.Entry.FirstOrDefault( entry => entry.Resource.ResourceType == ResourceType.PractitionerRole );
            if (funeralHomeDirector != null)
            {
                FuneralHomeDirector = (PractitionerRole)funeralHomeDirector.Resource;
            }

            // Scan through all Observations to make sure they all have codes!
            foreach (var ob in Bundle.Entry.Where( entry => entry.Resource.ResourceType == ResourceType.Observation ))
            {
                Observation obs = (Observation)ob.Resource;
                if (obs.Code == null || obs.Code.Coding == null || obs.Code.Coding.FirstOrDefault() == null || obs.Code.Coding.First().Code == null)
                {
                    throw new System.ArgumentException("Found an Observation resource that did not contain a code. All Observations must include a code to specify what the Observation is referring to.");
                }
            }

            // Grab Manner of Death
            var mannerOfDeath = Bundle.Entry.FirstOrDefault( entry => entry.Resource.ResourceType == ResourceType.Observation && ((Observation)entry.Resource).Code.Coding.First().Code == "69449-7" );
            if (mannerOfDeath != null)
            {
                MannerOfDeath = (Observation)mannerOfDeath.Resource;
            }

            // Grab Disposition Method
            var dispositionMethod = Bundle.Entry.FirstOrDefault( entry => entry.Resource.ResourceType == ResourceType.Observation && ((Observation)entry.Resource).Code.Coding.First().Code == "80905-3" );
            if (dispositionMethod != null)
            {
                DispositionMethod = (Observation)dispositionMethod.Resource;
            }

            // Grab Condition Contributing To Death
            var conditionContributingToDeath = Bundle.Entry.FirstOrDefault( entry => entry.Resource.ResourceType == ResourceType.Condition && ((Condition)entry.Resource).Asserter == null );
            if (conditionContributingToDeath != null)
            {
                ConditionContributingToDeath = (Condition)conditionContributingToDeath.Resource;
            }

            // Grab Cause Of Death Condition Pathway
            var causeOfDeathConditionPathway = Bundle.Entry.FirstOrDefault( entry => entry.Resource.ResourceType == ResourceType.List );
            if (causeOfDeathConditionPathway != null)
            {
                CauseOfDeathConditionPathway = (List)causeOfDeathConditionPathway.Resource;
            }

            // Grab Causes of Death
            // IMPROVEMENT: Look at using cause pathway to handle circumstances where input has un-ordered cause entries.
            // IMPROVEMENT: Support more than four causes.
            List<Bundle.EntryComponent> causes = Bundle.Entry.Where( entry => entry.Resource.ResourceType == ResourceType.Condition && ((Condition)entry.Resource).Asserter != null ).ToList();
            if (causes != null && causes.Count() > 0)
            {
                CauseOfDeathConditionA = (Condition)causes[0].Resource;
            }
            if (causes != null && causes.Count() > 1)
            {
                CauseOfDeathConditionB = (Condition)causes[1].Resource;
            }
            if (causes != null && causes.Count() > 2)
            {
                CauseOfDeathConditionC = (Condition)causes[2].Resource;
            }
            if (causes != null && causes.Count() > 3)
            {
                CauseOfDeathConditionD = (Condition)causes[3].Resource;
            }

            // Scan through all RelatedPerson to make sure they all have relationship codes!
            foreach (var rp in Bundle.Entry.Where( entry => entry.Resource.ResourceType == ResourceType.RelatedPerson ))
            {
                RelatedPerson rpn = (RelatedPerson)rp.Resource;
                if (rpn.Relationship == null || rpn.Relationship.Coding == null || rpn.Relationship.Coding.FirstOrDefault() == null || rpn.Relationship.Coding.First().Code == null)
                {
                    throw new System.ArgumentException("Found a RelatedPerson resource that did not contain a relationship code. All RelatedPersons must include a relationship code to specify how the RelatedPerson is related to the subject.");
                }
            }

            // Grab Father
            var father = Bundle.Entry.FirstOrDefault( entry => entry.Resource.ResourceType == ResourceType.RelatedPerson && ((RelatedPerson)entry.Resource).Relationship.Coding.First().Code == "FTH" );
            if (father != null)
            {
                Father = (RelatedPerson)father.Resource;
            }

            // Grab Mother
            var mother = Bundle.Entry.FirstOrDefault( entry => entry.Resource.ResourceType == ResourceType.RelatedPerson && ((RelatedPerson)entry.Resource).Relationship.Coding.First().Code == "MTH" );
            if (mother != null)
            {
                Mother = (RelatedPerson)mother.Resource;
            }

            // Grab Spouse
            var spouse = Bundle.Entry.FirstOrDefault( entry => entry.Resource.ResourceType == ResourceType.RelatedPerson && ((RelatedPerson)entry.Resource).Relationship.Coding.First().Code == "SPS" );
            if (spouse != null)
            {
                Spouse = (RelatedPerson)spouse.Resource;
            }

            // Grab Decedent Education Level
            var decedentEducationLevel = Bundle.Entry.FirstOrDefault( entry => entry.Resource.ResourceType == ResourceType.Observation && ((Observation)entry.Resource).Code.Coding.First().Code == "80913-7" );
            if (decedentEducationLevel != null)
            {
                DecedentEducationLevel = (Observation)decedentEducationLevel.Resource;
            }

            // Grab Birth Record Identifier
            var birthRecordIdentifier = Bundle.Entry.FirstOrDefault( entry => entry.Resource.ResourceType == ResourceType.Observation && ((Observation)entry.Resource).Code.Coding.First().Code == "BR" );
            if (birthRecordIdentifier != null)
            {
                BirthRecordIdentifier = (Observation)birthRecordIdentifier.Resource;
            }

            // Grab Employment History
            var employmentHistory = Bundle.Entry.FirstOrDefault( entry => entry.Resource.ResourceType == ResourceType.Observation && ((Observation)entry.Resource).Code.Coding.First().Code == "74165-2" );
            if (employmentHistory != null)
            {
                EmploymentHistory = (Observation)employmentHistory.Resource;
            }

            // Grab Disposition Location
            // IMPROVEMENT: Move away from using meta profile to find this exact Location.
            var dispositionLocation = Bundle.Entry.FirstOrDefault( entry => entry.Resource.ResourceType == ResourceType.Location && ((Location)entry.Resource).Meta.Profile.FirstOrDefault() != null && ((Location)entry.Resource).Meta.Profile.FirstOrDefault() == "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Disposition-Location" );
            if (dispositionLocation != null)
            {
                DispositionLocation = (Location)dispositionLocation.Resource;
            }

            // Grab Injury Location
            // IMPROVEMENT: Move away from using meta profile to find this exact Location.
            var injuryLocation = Bundle.Entry.FirstOrDefault( entry => entry.Resource.ResourceType == ResourceType.Location && ((Location)entry.Resource).Meta.Profile.FirstOrDefault() != null && ((Location)entry.Resource).Meta.Profile.FirstOrDefault() == "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Injury-Location" );
            if (injuryLocation != null)
            {
                InjuryLocationLoc = (Location)injuryLocation.Resource;
            }

            // Grab Death Location
            // IMPROVEMENT: Move away from using meta profile to find this exact Location.
            var deathLocation = Bundle.Entry.FirstOrDefault( entry => entry.Resource.ResourceType == ResourceType.Location && ((Location)entry.Resource).Meta.Profile.FirstOrDefault() != null && ((Location)entry.Resource).Meta.Profile.FirstOrDefault() == "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Death-Location" );
            if (deathLocation != null)
            {
                DeathLocationLoc = (Location)deathLocation.Resource;
            }

            // Grab Autopsy Performed
            var autopsyPerformed = Bundle.Entry.FirstOrDefault( entry => entry.Resource.ResourceType == ResourceType.Observation && ((Observation)entry.Resource).Code.Coding.First().Code == "85699-7" );
            if (autopsyPerformed != null)
            {
                AutopsyPerformed = (Observation)autopsyPerformed.Resource;
            }

            // Grab Age At Death
            var ageAtDeath = Bundle.Entry.FirstOrDefault( entry => entry.Resource.ResourceType == ResourceType.Observation && ((Observation)entry.Resource).Code.Coding.First().Code == "30525-0" );
            if (ageAtDeath != null)
            {
                AgeAtDeathObs = (Observation)ageAtDeath.Resource;
            }

            // Grab Pregnancy
            var pregnanacyStatus = Bundle.Entry.FirstOrDefault( entry => entry.Resource.ResourceType == ResourceType.Observation && ((Observation)entry.Resource).Code.Coding.First().Code == "69442-2" );
            if (pregnanacyStatus != null)
            {
                PregnancyObs = (Observation)pregnanacyStatus.Resource;
            }

            // Grab Transportation Role
            var transportationRole = Bundle.Entry.FirstOrDefault( entry => entry.Resource.ResourceType == ResourceType.Observation && ((Observation)entry.Resource).Code.Coding.First().Code == "69451-3" );
            if (transportationRole != null)
            {
                TransportationRoleObs = (Observation)transportationRole.Resource;
            }

            // Grab Examiner Contacted
            var examinerContacted = Bundle.Entry.FirstOrDefault( entry => entry.Resource.ResourceType == ResourceType.Observation && ((Observation)entry.Resource).Code.Coding.First().Code == "74497-9" );
            if (examinerContacted != null)
            {
                ExaminerContactedObs = (Observation)examinerContacted.Resource;
            }

            // Grab Tobacco Use
            var tobaccoUse = Bundle.Entry.FirstOrDefault( entry => entry.Resource.ResourceType == ResourceType.Observation && ((Observation)entry.Resource).Code.Coding.First().Code == "69443-0" );
            if (tobaccoUse != null)
            {
                TobaccoUseObs = (Observation)tobaccoUse.Resource;
            }

            // Grab Injury Incident
            var injuryIncident = Bundle.Entry.FirstOrDefault( entry => entry.Resource.ResourceType == ResourceType.Observation && ((Observation)entry.Resource).Code.Coding.First().Code == "11374-6" );
            if (injuryIncident != null)
            {
                InjuryIncidentObs = (Observation)injuryIncident.Resource;
            }

            // Grab Death Date
            var dateOfDeath = Bundle.Entry.FirstOrDefault( entry => entry.Resource.ResourceType == ResourceType.Observation && ((Observation)entry.Resource).Code.Coding.First().Code == "81956-5" );
            if (dateOfDeath != null)
            {
                DeathDateObs = (Observation)dateOfDeath.Resource;
            }
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
            if (codeableConcept != null && codeableConcept.Coding != null)
            {
                Coding coding = codeableConcept.Coding.FirstOrDefault();
                if (coding != null)
                {
                    if (!String.IsNullOrEmpty(coding.Code))
                    {
                        dictionary.Add("code", coding.Code);
                    }
                    else
                    {
                        dictionary.Add("code", "");
                    }
                    if (!String.IsNullOrEmpty(coding.System))
                    {
                        dictionary.Add("system", coding.System);
                    }
                    else
                    {
                        dictionary.Add("system", "");
                    }
                    if (!String.IsNullOrEmpty(coding.Display))
                    {
                        dictionary.Add("display", coding.Display);
                    }
                    else
                    {
                        dictionary.Add("display", "");
                    }
                }
                else
                {
                    return new Dictionary<string, string>() { { "code", "" }, { "system", "" }, { "display", "" } };
                }
            }
            else
            {
                return new Dictionary<string, string>() { { "code", "" }, { "system", "" }, { "display", "" } };
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

        /// <summary>Convert a FHIR Address to an "address" Dictionary.</summary>
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
                else
                {
                    dictionary.Add("addressLine1", "");
                }
                if (addr.Line != null && addr.Line.Count() > 1)
                {
                    dictionary.Add("addressLine2", addr.Line.Last());
                }
                else
                {
                    dictionary.Add("addressLine2", "");
                }
                dictionary.Add("addressCity", addr.City);
                dictionary.Add("addressCounty", addr.District);
                dictionary.Add("addressState", addr.State);
                dictionary.Add("addressZip", addr.PostalCode);
                dictionary.Add("addressCountry", addr.Country);
            }
            else
            {
                dictionary.Add("addressLine1", "");
                dictionary.Add("addressLine2", "");
                dictionary.Add("addressCity", "");
                dictionary.Add("addressCounty", "");
                dictionary.Add("addressState", "");
                dictionary.Add("addressZip", "");
                dictionary.Add("addressCountry", "");
            }
            return dictionary;
        }

        /// <summary>Returns an empty "address" Dictionary.</summary>
        /// <returns>an empty "address" Dictionary.</returns>
        private Dictionary<string, string> EmptyAddrDict()
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add("addressLine1", "");
            dictionary.Add("addressLine2", "");
            dictionary.Add("addressCity", "");
            dictionary.Add("addressCounty", "");
            dictionary.Add("addressState", "");
            dictionary.Add("addressZip", "");
            dictionary.Add("addressCountry", "");
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
                category[property.Name]["IGUrl"] = info.IGUrl;
                category[property.Name]["CapturedInIJE"] = info.CapturedInIJE;

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
                    if (value == null)
                    {
                        continue;
                    }
                    Dictionary<string, Dictionary<string, string>> moreInfo = new Dictionary<string, Dictionary<string, string>>();
                    foreach (PropertyParam propParameter in property.GetCustomAttributes().Reverse().Skip(1).Reverse().Skip(1))
                    {
                        moreInfo[propParameter.Key] = new Dictionary<string, string>();
                        moreInfo[propParameter.Key]["Description"] = propParameter.Description;
                        if (value.ContainsKey(propParameter.Key))
                        {
                            moreInfo[propParameter.Key]["Value"] = value[propParameter.Key];
                        }
                        else
                        {
                            moreInfo[propParameter.Key]["Value"] = null;
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
                    if (!property.Value.ContainsKey("Value") || property.Value["Value"] == null)
                    {
                        continue;
                    }
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

        /// <summary>If this field has an equivalent in IJE.</summary>
        public bool CapturedInIJE;

        /// <summary>Priority that this should show up in generated lists. Lower numbers come first.</summary>
        public int Priority;

        /// <summary>Constructor.</summary>
        public Property(string name, Types type, string category, string description, bool serialize, string igurl, bool capturedInIJE, int priority = 1)
        {
            this.Name = name;
            this.Type = type;
            this.Category = category;
            this.Description = description;
            this.Serialize = serialize;
            this.IGUrl = igurl;
            this.CapturedInIJE = capturedInIJE;
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
