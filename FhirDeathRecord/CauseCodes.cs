using System;
using System.Linq;
using System.Collections.Generic;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;

namespace FhirDeathRecord
{
    /// <summary>Class <c>CauseCodes</c> models a FHIR Vital Records Death Reporting (VRDR) Death
    /// Record return record. This can be used to consume or produce coded records that are typically
    /// returned from NCHS to a jurisdiction.
    /// </summary>
    public class CauseCodes
    {
        /// <summary>Useful for navigating around the FHIR Bundle using FHIRPaths.</summary>
        private ITypedElement Navigator;

        /// <summary>Bundle that contains the death record.</summary>
        private Bundle Bundle;

        /// <summary>The Certification.</summary>
        private Procedure DeathCertification;

        /// <summary>Composition that described what the Bundle is, as well as keeping references to its contents.</summary>
        private Composition Composition;

        /// <summary>Cause Of Death Condition Pathway.</summary>
        private List CauseOfDeathConditionPathway;

        /// <summary>Cause Of Death Conditions.</summary>
        private List<Condition> CauseOfDeathConditions;

        /// <summary>Default constructor that creates a new, empty CauseCodes.</summary>
        public CauseCodes()
        {
            // Start with an empty Bundle.
            Bundle = new Bundle();
            Bundle.Id = "urn:uuid:" + Guid.NewGuid().ToString();
            Bundle.Type = Bundle.BundleType.Document; // By default, Bundle type is "document".
            Bundle.Meta = new Meta();
            string[] bundle_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Death-Certificate-Document" };
            Bundle.Meta.Profile = bundle_profile;

            // Start with an empty certification.
            DeathCertification = new Procedure();
            DeathCertification.Id = "urn:uuid:" + Guid.NewGuid().ToString();
            DeathCertification.Meta = new Meta();
            string[] deathcertification_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Death-Certification" };
            DeathCertification.Meta.Profile = deathcertification_profile;
            DeathCertification.Status = EventStatus.Completed;
            DeathCertification.Category = new CodeableConcept("http://snomed.info/sct", "103693007", "Diagnostic procedure", null);
            DeathCertification.Code = new CodeableConcept("http://snomed.info/sct", "308646001", "Death certification", null);

            // Add Composition to bundle. As the record is filled out, new entries will be added to this element.
            Composition = new Composition();
            Composition.Id = "urn:uuid:" + Guid.NewGuid().ToString();
            Composition.Status = CompositionStatus.Final;
            Composition.Meta = new Meta();
            string[] composition_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Death-Certificate" };
            Composition.Meta.Profile = composition_profile;
            Composition.Type = new CodeableConcept("http://loinc.org", "64297-5", "Death certificate", null);
            Composition.Section.Add(new Composition.SectionComponent());
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
            CauseOfDeathConditionPathway.OrderedBy = new CodeableConcept(null, "priority", null, null);

            AddReferenceToComposition(DeathCertification.Id);
            AddReferenceToComposition(CauseOfDeathConditionPathway.Id);
            Bundle.AddResourceEntry(DeathCertification, DeathCertification.Id);
            Bundle.AddResourceEntry(CauseOfDeathConditionPathway, CauseOfDeathConditionPathway.Id);

            CauseOfDeathConditions = new List<Condition>();
        }

        /// <summary>Constructor that takes a string that represents a CauseCodes record in either XML or JSON format.</summary>
        /// <param name="record">represents a CauseCodes record in either XML or JSON format.</param>
        /// <param name="permissive">if the parser should be permissive when parsing the given string</param>
        /// <exception cref="ArgumentException">Record is neither valid XML nor JSON.</exception>
        public CauseCodes(string record, bool permissive = false)
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
        // Record Properties
        //
        /////////////////////////////////////////////////////////////////////////////////

        /// <summary>Death Record Identifier, Death Certificate Number.</summary>
        /// <value>a record identification string.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.Identifier = "42";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Record identification: {ExampleDeathRecord.Identifier}");</para>
        /// </example>
        [Property("Identifier", Property.Types.String, "Death Certification", "Death Certificate Number.", true, "http://hl7.org/fhir/us/vrdr/2019May/DeathCertificate.html", true, 1)]
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

        /// <summary>Death Record Bundle Identifier, Auxiliary State File Number.</summary>
        /// <value>a record bundle identification string.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.BundleIdentifier = "42";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Record bundle identification: {ExampleDeathRecord.BundleIdentifier}");</para>
        /// </example>
        [Property("Bundle Identifier", Property.Types.String, "Death Certification", "Auxiliary State File Number.", true, "http://hl7.org/fhir/us/vrdr/2019May/DeathCertificateDocument.html", true, 1)]
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

        /// <summary>Cause Codes - an ordered array of ICD-10 codes for cause of death.</summary>
        /// <value>an ordered array of ICD-10 codes for cause of death.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.BundleIdentifier = "42";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Record bundle identification: {ExampleDeathRecord.BundleIdentifier}");</para>
        /// </example>
        [Property("Cause Codes", Property.Types.StringArr, "Death Certification", "An ordered array of ICD-10 codes for cause of death.", true, "http://hl7.org/fhir/us/vrdr/2019May/CauseOfDeathCondition.html", true, 1)]
        [FHIRPath("Bundle", "identifier")]
        public string[] Codes
        {
            get
            {
                List<string> codes = new List<string>();
                if (CauseOfDeathConditions.Count() > 0)
                {
                    foreach (var cause in CauseOfDeathConditions)
                    {
                        if (cause.Code != null && cause.Code.Coding.FirstOrDefault() != null && !String.IsNullOrWhiteSpace(cause.Code.Coding.First().Code))
                        {
                            codes.Add(cause.Code.Coding.First().Code);
                        }
                    }
                }
                return codes.ToArray();
            }
            set
            {
                CauseOfDeathConditionPathway.Entry.Clear();
                CauseOfDeathConditions.Clear();
                foreach (string code in value)
                {
                    Condition condition = new Condition();
                    condition.Id = "urn:uuid:" + Guid.NewGuid().ToString();
                    condition.Meta = new Meta();
                    string[] condition_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Cause-Of-Death-Condition" };
                    condition.Meta.Profile = condition_profile;
                    Dictionary<string, string> dictionary = new Dictionary<string, string>();
                    dictionary.Add("system", "http://hl7.org/fhir/sid/icd-10");
                    dictionary.Add("code", code);
                    condition.Code = DictToCodeableConcept(dictionary);
                    AddReferenceToComposition(condition.Id);
                    Bundle.AddResourceEntry(condition, condition.Id);
                    List.EntryComponent entry = new List.EntryComponent();
                    entry.Item = new ResourceReference(condition.Id);
                    CauseOfDeathConditionPathway.Entry.Add(entry);
                    CauseOfDeathConditions.Add(condition);
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

            // Grab Cause Of Death Condition Pathway
            var causeOfDeathConditionPathway = Bundle.Entry.FirstOrDefault( entry => entry.Resource.ResourceType == ResourceType.List );
            if (causeOfDeathConditionPathway != null)
            {
                CauseOfDeathConditionPathway = (List)causeOfDeathConditionPathway.Resource;
            }

            // Grab Death Certification
            var procedureEntry = Bundle.Entry.FirstOrDefault( entry => entry.Resource.ResourceType == ResourceType.Procedure );
            if (procedureEntry != null)
            {
                DeathCertification = (Procedure)procedureEntry.Resource;
            }

            // Grab Causes of Death using CauseOfDeathConditionPathway
            List<Condition> causeConditions = new List<Condition>();
            if (CauseOfDeathConditionPathway != null)
            {
                foreach (List.EntryComponent condition in CauseOfDeathConditionPathway.Entry)
                {
                    if (condition != null && condition.Item != null && condition.Item.Reference != null)
                    {
                        var codCond = Bundle.Entry.FirstOrDefault( entry => entry.Resource.ResourceType == ResourceType.Condition && entry.FullUrl == condition.Item.Reference );
                        if (codCond != null)
                        {
                            causeConditions.Add((Condition)codCond.Resource);
                        }
                    }
                }
                CauseOfDeathConditions = causeConditions;
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
}