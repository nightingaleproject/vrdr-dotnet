using System;
using System.Linq;
using System.Collections.Generic;
using Hl7.Fhir.Model;

// DeathRecord_submissionProperties.cs
//    These fields are used primarily for submitting death records to NCHS.  Some are also used in response messages from NCHS to EDRS corresponding to TRX and MRE content.

namespace VRDR
{
    /// <summary>Class <c>DeathRecord</c> models a FHIR Vital Records Death Reporting (VRDR) Death
    /// Record. This class was designed to help consume and produce death records that follow the
    /// HL7 FHIR Vital Records Death Reporting Implementation Guide, as described at:
    /// http://hl7.org/fhir/us/vrdr and https://github.com/hl7/vrdr.
    /// </summary>
    public partial class DeathRecord
    {

        /////////////////////////////////////////////////////////////////////////////////
        //
        // Record Properties: Death Certification
        //
        /////////////////////////////////////////////////////////////////////////////////

        /// <summary>Death Record Identifier, Death Certificate Number.</summary>
        /// <value>a record identification string.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.Identifier = "42";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Death Certificate Number: {ExampleDeathRecord.Identifier}");</para>
        /// </example>
        [Property("Identifier", Property.Types.String, "Death Certification", "Death Certificate Number.", true, IGURL.DeathCertificate, true, 3)]
        [FHIRPath("Bundle", "identifier")]
        public string Identifier
        {
            get
            {
                if (Bundle?.Identifier?.Extension != null)
                {
                    Extension ext = Bundle.Identifier.Extension.Find(ex => ex.Url == ExtensionURL.CertificateNumber);
                    if (ext?.Value != null)
                    {
                        return Convert.ToString(ext.Value);
                    }
                }
                return null;
            }
            set
            {
                Bundle.Identifier.Extension.RemoveAll(ex => ex.Url == ExtensionURL.CertificateNumber);
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Extension ext = new Extension(ExtensionURL.CertificateNumber, new FhirString(value));
                    Bundle.Identifier.Extension.Add(ext);
                    UpdateDeathRecordIdentifier();
                }
            }
        }

        /// <summary>Update the bundle identifier from the component fields.</summary>
        private void UpdateDeathRecordIdentifier()
        {
            uint certificateNumber = 0;
            if (Identifier != null)
            {
                UInt32.TryParse(Identifier, out certificateNumber);
            }
            uint deathYear = 0;
            if (this.DeathYear != null)
            {
                deathYear = (uint)this.DeathYear;
            }
            String jurisdictionId = this.DeathLocationJurisdiction;
            if (jurisdictionId == null || jurisdictionId.Trim().Length < 2)
            {
                jurisdictionId = "XX";
            }
            else
            {
                jurisdictionId = jurisdictionId.Trim().Substring(0, 2).ToUpper();
            }
            this.DeathRecordIdentifier = $"{deathYear.ToString("D4")}{jurisdictionId}{certificateNumber.ToString("D6")}";

        }

        /// <summary>Death Record Bundle Identifier, NCHS identifier.</summary>
        /// <value>a record bundle identification string, e.g., 2022MA000100, derived from year of death, jurisdiction of death, and certificate number</value>
        /// <example>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"NCHS identifier: {ExampleDeathRecord.DeathRecordIdentifier}");</para>
        /// </example>
        [Property("Death Record Identifier", Property.Types.String, "Death Certification", "Death Record identifier.", true, IGURL.DeathCertificate, true, 4)]
        [FHIRPath("Bundle", "identifier")]
        public string DeathRecordIdentifier
        {
            get
            {
                if (Bundle != null && Bundle.Identifier != null)
                {
                    return Bundle.Identifier.Value;
                }
                return null;
            }
            // The setter is private because the value is derived so should never be set directly
            private set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    return;
                }
                if (Bundle.Identifier == null)
                {
                    Bundle.Identifier = new Identifier();
                }
                Bundle.Identifier.Value = value;
                Bundle.Identifier.System = "http://nchs.cdc.gov/vrdr_id";
            }
        }

        /// <summary>State Local Identifier1.</summary>
        /// <para>"value" the string representation of the local identifier</para>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.StateLocalIdentifier1 = "MA";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"State local identifier: {ExampleDeathRecord.StateLocalIdentifier1}");</para>
        /// </example>
        [Property("State Local Identifier1", Property.Types.String, "Death Certification", "State Local Identifier.", true, ProfileURL.DeathCertificate, true, 5)]
        [FHIRPath("Bundle", "identifier")]
        public string StateLocalIdentifier1
        {
            get
            {
                if (Bundle?.Identifier?.Extension != null)
                {
                    Extension ext = Bundle.Identifier.Extension.Find(ex => ex.Url == ExtensionURL.AuxiliaryStateIdentifier1);
                    if (ext?.Value != null)
                    {
                        return Convert.ToString(ext.Value);
                    }
                }
                return null;
            }
            set
            {
                Bundle.Identifier.Extension.RemoveAll(ex => ex.Url == ExtensionURL.AuxiliaryStateIdentifier1);
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Extension ext = new Extension(ExtensionURL.AuxiliaryStateIdentifier1, new FhirString(value));
                    Bundle.Identifier.Extension.Add(ext);
                }
            }
        }

        /// <summary>State Local Identifier2.</summary>
        /// <para>"value" the string representation of the local identifier</para>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.StateLocalIdentifier2 = "YC";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"State local identifier: {ExampleDeathRecord.StateLocalIdentifier1}");</para>
        /// </example>
        [Property("State Local Identifier2", Property.Types.String, "Death Certification", "State Local Identifier.", true, ProfileURL.DeathCertificate, true, 5)]
        [FHIRPath("Bundle", "identifier")]
        public string StateLocalIdentifier2
        {
            get
            {
                if (Bundle?.Identifier?.Extension != null)
                {
                    Extension ext = Bundle.Identifier.Extension.Find(ex => ex.Url == ExtensionURL.AuxiliaryStateIdentifier2);
                    if (ext?.Value != null)
                    {
                        return Convert.ToString(ext.Value);
                    }
                }
                return null;
            }
            set
            {
                Bundle.Identifier.Extension.RemoveAll(ex => ex.Url == ExtensionURL.AuxiliaryStateIdentifier2);
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Extension ext = new Extension(ExtensionURL.AuxiliaryStateIdentifier2, new FhirString(value));
                    Bundle.Identifier.Extension.Add(ext);
                }
            }
        }

        /// <summary>Certified time.</summary>
        /// <value>time when the record was certified.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.CertifiedTime = "2019-01-29T16:48:06-05:00";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Certified at: {ExampleDeathRecord.CertifiedTime}");</para>
        /// </example>
        [Property("Certified Time", Property.Types.StringDateTime, "Death Certification", "Certified time (i.e. certifier date signed).", true, IGURL.DeathCertification, false, 12)]
        [FHIRPath("Bundle.entry.resource.where($this is Procedure).where(code.coding.code='308646001')", "")]
        public string CertifiedTime
        {
            get
            {
                if (DeathCertification != null && DeathCertification.Performed != null)
                {
                    return Convert.ToString(DeathCertification.Performed);
                }
                else if (Composition != null && Composition.Attester != null && Composition.Attester.FirstOrDefault() != null && Composition.Attester.First().Time != null)
                {
                    return Composition.Attester.First().Time;
                }
                return null;
            }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    return;
                }
                if (DeathCertification == null)
                {
                    CreateDeathCertification();
                }
                Composition.Attester.First().Time = value;
                DeathCertification.Performed = new FhirDateTime(value);
            }
        }

        /// <summary>Filing Format.</summary>
        /// <value>Source flag: paper/electronic.
        /// <para>"code" - the code</para>
        /// <para>"system" - the code system this code belongs to</para>
        /// <para>"display" - a human readable meaning of the code</para>
        /// </value>        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; format = new Dictionary&lt;string, string&gt;();</para>
        /// <para>format.Add("code", ValueSets.FilingFormat.electronic);</para>
        /// <para>format.Add("system", CodeSystems.FilingFormat);</para>
        /// <para>format.Add("display", "Electronic");</para>
        /// <para>ExampleDeathRecord.FilingFormat = format;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Filed method: {ExampleDeathRecord.FilingFormat}");</para>
        /// </example>
        [Property("Filing Format", Property.Types.Dictionary, "Death Certification", "Filing format.", true, IGURL.DeathCertificate, true, 13)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Composition).extension.where(url='http://hl7.org/fhir/us/vrdr/StructureDefinition/FilingFormat')", "")]
        public Dictionary<string, string> FilingFormat
        {
            get
            {
                if (Composition != null)
                {
                    Extension filingFormat = Composition.Extension.Find(ext => ext.Url == ExtensionURL.FilingFormat);

                    if (filingFormat != null && filingFormat.Value != null && filingFormat.Value as CodeableConcept != null)
                    {
                        return CodeableConceptToDict((CodeableConcept)filingFormat.Value);
                    }
                }
                return EmptyCodeableDict();
            }
            set
            {
                // TODO: Handle case where Composition == null (either create it or throw exception)
                Composition.Extension.RemoveAll(ext => ext.Url == ExtensionURL.FilingFormat);
                Extension filingFormat = new Extension();
                filingFormat.Url = ExtensionURL.FilingFormat;
                filingFormat.Value = DictToCodeableConcept(value);
                Composition.Extension.Add(filingFormat);
            }
        }

        /// <summary>Filing Format Helper.</summary>
        /// <value>filing format.
        /// <para>"code" - the code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.FilingFormatHelper = ValueSets.FilingFormat.Electronic;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Filing Format: {ExampleDeathRecord.FilingFormatHelper}");</para>
        /// </example>
        [Property("Filing Format Helper", Property.Types.String, "Death Certification", "Filing Format.", false, IGURL.DeathCertificate, true, 13)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [FHIRPath("Bundle.entry.resource.where($this is Composition).extension.where(url='http://hl7.org/fhir/us/vrdr/StructureDefinition/FilingFormat')", "")]
        public string FilingFormatHelper
        {
            get
            {
                if (FilingFormat.ContainsKey("code") && !String.IsNullOrWhiteSpace(FilingFormat["code"]))
                {
                    return FilingFormat["code"];
                }
                return null;
            }
            set
            {
                if(!String.IsNullOrWhiteSpace(value))
                {
                    SetCodeValue("FilingFormat", value, VRDR.ValueSets.FilingFormat.Codes);
                }
            }
        }

        /// <summary>Registered time.</summary>
        /// <value>time when the record was registered.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.RegisteredTime = "2019-01-29T16:48:06-05:00";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Registered at: {ExampleDeathRecord.RegisteredTime}");</para>
        /// </example>
        [Property("Registered Date/Time", Property.Types.StringDateTime, "Death Certification", "Date/Time of record registration.", true, IGURL.DeathCertificate, true, 13)]
        [FHIRPath("Bundle.entry.resource.where($this is Composition)", "date")]
        public string RegisteredTime
        {
            get
            {
                if (Composition != null)
                {
                    return Composition.Date;
                }
                return null;
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Composition.Date = value;
                }
            }
        }

        /// <summary>State Specific Data.</summary>
        /// <value>Possible use for future filler unless two neighboring states wish to use for some specific information that they both collect. This would be a non-standard field</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.StateSpecific = "Data";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"State Specific Data: {ExampleDeathRecord.StateSpecific}");</para>
        /// </example>
        [Property("State Specific Data", Property.Types.String, "Death Certification", "State Specific Content.", true, IGURL.DeathCertificate, true, 13)]
        [FHIRPath("Bundle.entry.resource.where($this is Composition).extension.where(url='http://hl7.org/fhir/us/vrdr/StructureDefinition/StateSpecificField')", "date")]
        public string StateSpecific
        {
            get
            {
                if (Composition != null)
                {
                    Extension stateSpecificData = Composition.Extension.Where(ext => ext.Url == ExtensionURL.StateSpecificField).FirstOrDefault();
                    if (stateSpecificData != null && stateSpecificData.Value as FhirString != null)
                    {
                        return stateSpecificData.Value.ToString();
                    }
                }
                return null;
            }
            set
            {
                // TODO: Handle case where Composition == null (either create it or throw exception)
                Composition.Extension.RemoveAll(ext => ext.Url == ExtensionURL.StateSpecificField);
                if (String.IsNullOrWhiteSpace(value))
                {
                    return;
                }
                Extension stateSpecificData = new Extension();
                stateSpecificData.Url = ExtensionURL.StateSpecificField;
                stateSpecificData.Value = new FhirString(value);
                Composition.Extension.Add(stateSpecificData);
            }
        }

        /// <summary>Replace Status.</summary>
        /// <value>Replacement Record – suggested codes.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; replace = new Dictionary&lt;string, string&gt;();</para>
        /// <para>replace.Add("code", "original");</para>
        /// <para>replace.Add("system", CodeSystems.ReplaceStatus);</para>
        /// <para>replace.Add("display", "original");</para>
        /// <para>ExampleDeathRecord.ReplaceStatus = replace;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Filed method: {ExampleDeathRecord.ReplaceStatus}");</para>
        /// </example>
        [Property("Replace Status", Property.Types.Dictionary, "Death Certification", "Replace status.", true, IGURL.DeathCertificate, true, 13)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Composition).extension.where(url='http://hl7.org/fhir/us/vrdr/StructureDefinition/ReplaceStatus')", "")]
        public Dictionary<string, string> ReplaceStatus
        {
            get
            {
                if (Composition != null)
                {
                    Extension replaceStatus = Composition.Extension.Find(ext => ext.Url == ExtensionURL.ReplaceStatus);

                    if (replaceStatus != null && replaceStatus.Value != null && replaceStatus.Value as CodeableConcept != null)
                    {
                        return CodeableConceptToDict((CodeableConcept)replaceStatus.Value);
                    }
                }
                return EmptyCodeableDict();
            }
            set
            {
                // TODO: Handle case where Composition == null (either create it or throw exception)
                Composition.Extension.RemoveAll(ext => ext.Url == ExtensionURL.ReplaceStatus);
                Extension replaceStatus = new Extension();
                replaceStatus.Url = ExtensionURL.ReplaceStatus;
                replaceStatus.Value = DictToCodeableConcept(value);
                Composition.Extension.Add(replaceStatus);
            }
        }

        /// <summary>Replace Status Helper.</summary>
        /// <value>replace status.
        /// <para>"code" - the code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.ReplaceStatusHelper = ValueSets.ReplaceStatus.Original_Record;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"ReplaceStatus: {ExampleDeathRecord.ReplaceStatusHelper}");</para>
        /// </example>
        [Property("Replace Status Helper", Property.Types.String, "Death Certification", "Replace Status.", false, IGURL.DeathCertificate, true, 4)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [FHIRPath("Bundle.entry.resource.where($this is Composition).extension.where(url='http://hl7.org/fhir/us/vrdr/StructureDefinition/ReplaceStatus')", "")]
        public string ReplaceStatusHelper
        {
            get
            {
                if (ReplaceStatus.ContainsKey("code") && !String.IsNullOrWhiteSpace(ReplaceStatus["code"]))
                {
                    return ReplaceStatus["code"];
                }
                return null;
            }
            set
            {
                if(!String.IsNullOrWhiteSpace(value))
                {
                    SetCodeValue("ReplaceStatus", value, VRDR.ValueSets.ReplaceStatus.Codes);
                }
            }
        }


        /// <summary>Certification Role.</summary>
        /// <value>the role/qualification of the person who certified the death. A Dictionary representing a code, containing the following key/value pairs:
        /// <para>"code" - the code</para>
        /// <para>"system" - the code system this code belongs to</para>
        /// <para>"display" - a human readable meaning of the code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; role = new Dictionary&lt;string, string&gt;();</para>
        /// <para>role.Add("code", "76899008");</para>
        /// <para>role.Add("system", CodeSystems.SCT);</para>
        /// <para>role.Add("display", "Infectious diseases physician");</para>
        /// <para>ExampleDeathRecord.CertificationRole = role;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Certification Role: {ExampleDeathRecord.CertificationRole['display']}");</para>
        /// </example>
        [Property("Certification Role", Property.Types.Dictionary, "Death Certification", "Certification Role.", true, IGURL.DeathCertification, true, 4)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Procedure).where(code.coding.code='308646001')", "performer")]
        public Dictionary<string, string> CertificationRole
        {
            get
            {
                if (DeathCertification == null)
                {
                    return EmptyCodeableDict();
                }
                Hl7.Fhir.Model.Procedure.PerformerComponent performer = DeathCertification.Performer.FirstOrDefault();
                if (performer != null && performer.Function != null)
                {
                    return CodeableConceptToDict(performer.Function);
                }
                return EmptyCodeableDict();
            }
            set
            {
                if (DeathCertification == null)
                {
                    CreateDeathCertification();
                }
                Hl7.Fhir.Model.Procedure.PerformerComponent performer = new Hl7.Fhir.Model.Procedure.PerformerComponent();
                performer.Function = DictToCodeableConcept(value);
                performer.Actor = new ResourceReference("urn:uuid:" + Certifier.Id);
                DeathCertification.Performer.Clear();
                DeathCertification.Performer.Add(performer);
            }
        }

        /// <summary>Certification Role Helper.</summary>
        /// <value>the role/qualification of the person who certified the death.
        /// <para>"code" - the code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.CertificationRoleHelper = ValueSets.CertificationRole.InfectiousDiseasesPhysician;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Certification Role: {ExampleDeathRecord.CertificationRoleHelper}");</para>
        /// </example>
        [Property("Certification Role Helper", Property.Types.String, "Death Certification", "Certification Role.", false, IGURL.DeathCertification, true, 4)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [FHIRPath("Bundle.entry.resource.where($this is Procedure).where(code.coding.code='308646001')", "performer")]
        public string CertificationRoleHelper
        {
            get
            {
                if (CertificationRole.ContainsKey("code"))
                {
                    string code = CertificationRole["code"];
                    if (code == "OTH")
                    {
                        if (CertificationRole.ContainsKey("text") && !String.IsNullOrWhiteSpace(CertificationRole["text"]))
                        {
                            return (CertificationRole["text"]);
                        }
                        return ("Other");
                    }
                    else if (!String.IsNullOrWhiteSpace(code))
                    {
                        return code;
                    }
                }
                return null;
            }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    // do nothing
                    return;
                }
                if (!VRDR.Mappings.CertifierTypes.FHIRToIJE.ContainsKey(value))
                { //other
                    CertificationRole = CodeableConceptToDict(new CodeableConcept(CodeSystems.NullFlavor_HL7_V3, "OTH", "Other", value));
                }
                else
                { // normal path
                    SetCodeValue("CertificationRole", value, VRDR.ValueSets.CertifierTypes.Codes);
                }
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
        /// <para>manner.Add("display", "Accidental death");</para>
        /// <para>ExampleDeathRecord.MannerOfDeathType = manner;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Manner Of Death Type: {ExampleDeathRecord.MannerOfDeathType['display']}");</para>
        /// </example>
        [Property("Manner Of Death Type", Property.Types.Dictionary, "Death Certification", "Manner of Death Type.", true, IGURL.MannerOfDeath, true, 49)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='69449-7')", "")]
        public Dictionary<string, string> MannerOfDeathType
        {
            get
            {
                if (MannerOfDeath != null && MannerOfDeath.Value != null && MannerOfDeath.Value as CodeableConcept != null)
                {
                    return CodeableConceptToDict((CodeableConcept)MannerOfDeath.Value);
                }
                return EmptyCodeableDict();
            }
            set
            {
                if (IsDictEmptyOrDefault(value) && MannerOfDeath == null)
                {
                    return;
                }
                if (MannerOfDeath == null)
                {
                    MannerOfDeath = new Observation();
                    MannerOfDeath.Id = Guid.NewGuid().ToString();
                    MannerOfDeath.Meta = new Meta();
                    string[] mannerofdeath_profile = { ProfileURL.MannerOfDeath };
                    MannerOfDeath.Meta.Profile = mannerofdeath_profile;
                    MannerOfDeath.Status = ObservationStatus.Final;
                    MannerOfDeath.Code = new CodeableConcept(CodeSystems.LOINC, "69449-7", "Manner of death", null);
                    MannerOfDeath.Subject = new ResourceReference("urn:uuid:" + Decedent.Id);
                    MannerOfDeath.Performer.Add(new ResourceReference("urn:uuid:" + Certifier.Id));
                    MannerOfDeath.Value = DictToCodeableConcept(value);
                    AddReferenceToComposition(MannerOfDeath.Id, "DeathCertification");
                    Bundle.AddResourceEntry(MannerOfDeath, "urn:uuid:" + MannerOfDeath.Id);
                }
                else
                {
                    MannerOfDeath.Value = DictToCodeableConcept(value);
                }
            }
        }

        /// <summary>Manner of Death Type Helper</summary>
        /// <value>the manner of death type
        /// <para>"code" - the code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.MannerOfDeathTypeHelper = MannerOfDeath.Natural;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Manner Of Death Type: {ExampleDeathRecord.MannerOfDeathTypeHelper}");</para>
        /// </example>
        [Property("Manner Of Death Type Helper", Property.Types.String, "Death Certification", "Manner of Death Type.", false, IGURL.MannerOfDeath, true, 49)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='69449-7')", "")]

        public string MannerOfDeathTypeHelper
        {
            get
            {
                if (MannerOfDeathType.ContainsKey("code") && !String.IsNullOrWhiteSpace(MannerOfDeathType["code"]))
                {
                    return MannerOfDeathType["code"];
                }
                return null;
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    SetCodeValue("MannerOfDeathType", value, VRDR.ValueSets.MannerOfDeath.Codes);
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
        [Property("Certifier Given Names", Property.Types.StringArr, "Death Certification", "Given name(s) of certifier.", true, IGURL.Certifier, true, 5)]
        [FHIRPath("Bundle.entry.resource.where($this is Practitioner).where(meta.profile='http://hl7.org/fhir/us/vrdr/StructureDefinition/vrdr-certifier')", "name")]
        public string[] CertifierGivenNames
        {
            get
            {
                if (Certifier != null && Certifier.Name.Count() > 0)
                {
                    return Certifier.Name.First().Given.ToArray();
                }
                return new string[0];
            }
            set
            {
                if (Certifier == null)
                {
                    CreateCertifier();
                }
                updateGivenHumanName(value, Certifier.Name);
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
        [Property("Certifier Family Name", Property.Types.String, "Death Certification", "Family name of certifier.", true, IGURL.Certifier, true, 6)]
        [FHIRPath("Bundle.entry.resource.where($this is Practitioner).where(meta.profile='http://hl7.org/fhir/us/vrdr/StructureDefinition/vrdr-certifier')", "name")]
        public string CertifierFamilyName
        {
            get
            {
                if (Certifier != null && Certifier.Name.Count() > 0)
                {
                    return Certifier.Name.First().Family;
                }
                return null;
            }
            set
            {
                if (Certifier == null)
                {
                    CreateCertifier();
                }
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
        [Property("Certifier Suffix", Property.Types.String, "Death Certification", "Certifier's Suffix.", true, IGURL.Certifier, true, 7)]
        [FHIRPath("Bundle.entry.resource.where($this is Practitioner).where(meta.profile='http://hl7.org/fhir/us/vrdr/StructureDefinition/vrdr-certifier')", "name")]
        public string CertifierSuffix
        {
            get
            {
                if (Certifier != null && Certifier.Name.Count() > 0 && Certifier.Name.First().Suffix.Count() > 0)
                {
                    return Certifier.Name.First().Suffix.First();
                }
                return null;
            }
            set
            {
                if (Certifier == null)
                {
                    CreateCertifier();
                }
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
        /// <para>address.Add("addressState", "MA");</para>
        /// <para>address.Add("addressZip", "12345");</para>
        /// <para>address.Add("addressCountry", "US");</para>
        /// <para>ExampleDeathRecord.CertifierAddress = address;</para>
        /// <para>// Getter:</para>
        /// <para>foreach(var pair in ExampleDeathRecord.CertifierAddress)</para>
        /// <para>{</para>
        /// <para>  Console.WriteLine($"\tCertifierAddress key: {pair.Key}: value: {pair.Value}");</para>
        /// <para>};</para>
        /// </example>
        [Property("Certifier Address", Property.Types.Dictionary, "Death Certification", "Certifier's Address.", true, IGURL.Certifier, true, 8)]
        [PropertyParam("addressLine1", "address, line one")]
        [PropertyParam("addressLine2", "address, line two")]
        [PropertyParam("addressCity", "address, city")]
        [PropertyParam("addressCounty", "address, county")]
        [PropertyParam("addressState", "address, state")]
        [PropertyParam("addressStnum", "address, stnum")]
        [PropertyParam("addressPredir", "address, predir")]
        [PropertyParam("addressPostdir", "address, postdir")]
        [PropertyParam("addressStname", "address, stname")]
        [PropertyParam("addressStrdesig", "address, strdesig")]
        [PropertyParam("addressUnitnum", "address, unitnum")]
        [PropertyParam("addressZip", "address, zip")]
        [PropertyParam("addressCountry", "address, country")]
        [FHIRPath("Bundle.entry.resource.where($this is Practitioner).where(meta.profile='http://hl7.org/fhir/us/vrdr/StructureDefinition/vrdr-certifier')", "address")]
        public Dictionary<string, string> CertifierAddress
        {
            get
            {
                if (Certifier == null)
                {
                    return (new Dictionary<string, string>());
                }
                return AddressToDict(Certifier.Address.FirstOrDefault());
            }
            set
            {
                if (Certifier == null)
                {
                    CreateCertifier();
                }
                Certifier.Address.Clear();
                Certifier.Address.Add(DictToAddress(value));
            }
        }

        /// <summary>Certifier Identifier ** not mapped to IJE **.</summary>
        /// <value>the certifier identification. A Dictionary representing a system (e.g. NPI) and a value, containing the following key/value pairs:
        /// <para>"system" - the identifier system, e.g. US NPI</para>
        /// <para>"value" - the idetifier value, e.g. US NPI number</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; identifier = new Dictionary&lt;string, string&gt;();</para>
        /// <para>identifier.Add("system", "http://hl7.org/fhir/sid/us-npi");</para>
        /// <para>identifier.Add("value", "1234567890");</para>
        /// <para>ExampleDeathRecord.CertifierIdentifier = identifier;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"\tCertifier Identifier: {ExampleDeathRecord.CertifierIdentifier['value']}");</para>
        /// </example>
        [Property("Certifier Identifier", Property.Types.Dictionary, "Death Certification", "Certifier Identifier.", true, IGURL.Certifier, false, 10)]
        [PropertyParam("system", "The identifier system.")]
        [PropertyParam("value", "The identifier value.")]
        [FHIRPath("Bundle.entry.resource.where($this is Practitioner).where(meta.profile='http://hl7.org/fhir/us/vrdr/StructureDefinition/vrdr-certifier')", "identifier")]
        public Dictionary<string, string> CertifierIdentifier
        {
            get
            {

                if (Certifier == null || Certifier.Identifier.FirstOrDefault() == null)
                {
                    return new Dictionary<string, string>();
                }
                Identifier identifier = Certifier.Identifier.FirstOrDefault();
                var result = new Dictionary<string, string>();
                result["system"] = identifier.System;
                result["value"] = identifier.Value;
                return result;
            }
            set
            {
                if (Certifier == null)
                {
                    CreateCertifier();
                }
                if (Certifier.Identifier.Count > 0)
                {
                    Certifier.Identifier.Clear();
                }
                if (value.ContainsKey("system") && value.ContainsKey("value"))
                {
                    Identifier identifier = new Identifier();
                    identifier.System = value["system"];
                    identifier.Value = value["value"];
                    Certifier.Identifier.Add(identifier);
                }
            }
        }

        // /// <summary>Certifier License Number. ** NOT MAPPED TO IJE ** </summary>
        // /// <value>A string containing the certifier license number.</value>
        // /// <example>
        // /// <para>// Setter:</para>
        // /// <para>ExampleDeathRecord.CertifierQualification = qualification;</para>
        // /// <para>// Getter:</para>
        // /// <para>Console.WriteLine($"\tCertifier Qualification: {ExampleDeathRecord.CertifierQualification['display']}");</para>
        // /// </example>
        // [Property("Certifier License Number", Property.Types.String, "Death Certification", "Certifier License Number.", true, "http://build.fhir.org/ig/HL7/vrdr/StructureDefinition-VRDR-Certifier.html", false, 11)]
        // [FHIRPath("Bundle.entry.resource.where($this is Practitioner).where(meta.profile='http://hl7.org/fhir/us/vrdr/StructureDefinition/vrdr-certifier')", "qualification")]
        // public string CertifierLicenseNumber
        // {
        //     get
        //     {
        //         Practitioner.QualificationComponent qualification = Certifier.Qualification.FirstOrDefault();
        //         if (qualification != null && qualification.Identifier.FirstOrDefault() != null)
        //         {
        //             if (!String.IsNullOrWhiteSpace(qualification.Identifier.First().Value))
        //             {
        //                 return qualification.Identifier.First().Value;
        //             }
        //             return null;
        //         }
        //         return null;
        //     }
        //     set
        //     {
        //         if (Certifier.Qualification.FirstOrDefault() == null)
        //         {
        //             Practitioner.QualificationComponent qualification = new Practitioner.QualificationComponent();
        //             Identifier identifier = new Identifier();
        //             identifier.Value = value;
        //             qualification.Identifier.Add(identifier);
        //             Certifier.Qualification.Add(qualification);
        //         }
        //         else
        //         {
        //             Certifier.Qualification.First().Identifier.Clear();
        //             Identifier identifier = new Identifier();
        //             identifier.Value = value;
        //             Certifier.Qualification.First().Identifier.Add(identifier);
        //         }
        //     }
        // }

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
        [Property("Contributing Conditions", Property.Types.String, "Death Certification", "Significant conditions that contributed to death but did not result in the underlying cause.", true, IGURL.CauseOfDeathPart2, true, 100)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='69441-4')", "")]
        public string ContributingConditions
        {
            get
            {
                if (ConditionContributingToDeath != null && ConditionContributingToDeath.Value != null)
                {
                    return (CodeableConceptToDict((CodeableConcept)ConditionContributingToDeath.Value))["text"];
                }
                return null;
            }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    return;
                }
                if (ConditionContributingToDeath != null)
                {
                    ConditionContributingToDeath.Value = new CodeableConcept(null, null, null, value);
                }
                else
                {
                    ConditionContributingToDeath = new Observation();
                    ConditionContributingToDeath.Id = Guid.NewGuid().ToString();
                    ConditionContributingToDeath.Subject = new ResourceReference("urn:uuid:" + Decedent.Id);
                    ConditionContributingToDeath.Performer.Add(new ResourceReference("urn:uuid:" + Certifier.Id));
                    ConditionContributingToDeath.Meta = new Meta();
                    string[] condition_profile = { ProfileURL.CauseOfDeathPart2 };
                    ConditionContributingToDeath.Meta.Profile = condition_profile;
                    ConditionContributingToDeath.Status = ObservationStatus.Final;
                    ConditionContributingToDeath.Code = (new CodeableConcept(CodeSystems.LOINC, "69441-4", "Other significant causes or conditions of death", null));
                    ConditionContributingToDeath.Value = new CodeableConcept(null, null, null, value);
                    AddReferenceToComposition(ConditionContributingToDeath.Id, "DeathCertification");
                    Bundle.AddResourceEntry(ConditionContributingToDeath, "urn:uuid:" + ConditionContributingToDeath.Id);
                }
            }
        }

        /// <summary>Conditions that resulted in the cause of death. Corresponds to part 1 of item 32 of the U.S.
        /// Standard Certificate of Death.</summary>
        /// <value>Conditions that resulted in the underlying cause of death. An array of tuples (in the order they would
        /// appear on a death certificate, from top to bottom), each containing the cause of death literal (Tuple "Item1")
        /// and the approximate onset to death (Tuple "Item2").</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Tuple&lt;string, string&gt;[] causes =</para>
        /// <para>{</para>
        /// <para>    Tuple.Create("Example Immediate COD", "minutes"),</para>
        /// <para>    Tuple.Create("Example Underlying COD 1", "2 hours"),</para>
        /// <para>    Tuple.Create("Example Underlying COD 2", "6 months"),</para>
        /// <para>    Tuple.Create("Example Underlying COD 3", "15 years")</para>
        /// <para>};</para>
        /// <para>ExampleDeathRecord.CausesOfDeath = causes;</para>
        /// <para>// Getter:</para>
        /// <para>Tuple&lt;string, string&gt;[] causes = ExampleDeathRecord.CausesOfDeath;</para>
        /// <para>foreach (var cause in causes)</para>
        /// <para>{</para>
        /// <para>    Console.WriteLine($"Cause: {cause.Item1}, Onset: {cause.Item2}");</para>
        /// <para>}</para>
        /// </example>
        [Property("Causes Of Death", Property.Types.TupleArr, "Death Certification", "Conditions that resulted in the cause of death.", true, IGURL.CauseOfDeathPart1, true, 50)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='69453-9')", "")]
        public Tuple<string, string>[] CausesOfDeath
        {
            get
            {
                List<Tuple<string, string>> results = new List<Tuple<string, string>>();
                if (!String.IsNullOrEmpty(COD1A) || !String.IsNullOrEmpty(INTERVAL1A))
                {
                    results.Add(Tuple.Create(COD1A, INTERVAL1A));
                }
                if (!String.IsNullOrEmpty(COD1B) || !String.IsNullOrEmpty(INTERVAL1B))
                {
                    results.Add(Tuple.Create(COD1B, INTERVAL1B));
                }
                if (!String.IsNullOrEmpty(COD1C) || !String.IsNullOrEmpty(INTERVAL1C))
                {
                    results.Add(Tuple.Create(COD1C, INTERVAL1C));
                }
                if (!String.IsNullOrEmpty(COD1D) || !String.IsNullOrEmpty(INTERVAL1D))
                {
                    results.Add(Tuple.Create(COD1D, INTERVAL1D));
                }

                return results.ToArray();
            }
            set
            {
                if (value != null)
                {
                    if (value.Length > 0)
                    {
                        if (!String.IsNullOrWhiteSpace(value[0].Item1))
                        {
                            COD1A = value[0].Item1;
                        }
                        if (!String.IsNullOrWhiteSpace(value[0].Item2))
                        {
                            INTERVAL1A = value[0].Item2;
                        }
                    }
                    if (value.Length > 1)
                    {
                        if (!String.IsNullOrWhiteSpace(value[1].Item1))
                        {
                            COD1B = value[1].Item1;
                        }
                        if (!String.IsNullOrWhiteSpace(value[1].Item2))
                        {
                          INTERVAL1B = value[1].Item2;
                        }
                    }
                    if (value.Length > 2)
                    {
                        if (!String.IsNullOrWhiteSpace(value[2].Item1))
                        {
                            COD1C = value[2].Item1;

                        }
                        if (!String.IsNullOrWhiteSpace(value[2].Item2))
                        {
                            INTERVAL1C = value[2].Item2;
                        }
                    }
                    if (value.Length > 3)
                    {
                        if (!String.IsNullOrWhiteSpace(value[3].Item1))
                        {
                            COD1D = value[3].Item1;
                        }
                        if (!String.IsNullOrWhiteSpace(value[3].Item2))
                        {
                            INTERVAL1D = value[3].Item2;
                        }
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
        [Property("COD1A", Property.Types.String, "Death Certification", "Cause of Death Part I, Line a.", false, IGURL.CauseOfDeathPart1, false, 100)]
        public string COD1A
        {
            get
            {
                if (CauseOfDeathConditionA != null && CauseOfDeathConditionA.Value != null)
                {
                    return (CodeableConceptToDict((CodeableConcept)CauseOfDeathConditionA.Value))["text"];
                }
                return null;
            }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    return;
                }
                if (CauseOfDeathConditionA == null)
                {
                    CauseOfDeathConditionA = CauseOfDeathCondition(0);
                }
                CauseOfDeathConditionA.Value = new CodeableConcept(null, null, null, value);
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
        [Property("INTERVAL1A", Property.Types.String, "Death Certification", "Cause of Death Part I Interval, Line a.", false, IGURL.CauseOfDeathPart1, false, 100)]
        public string INTERVAL1A
        {
            get
            {
                if (CauseOfDeathConditionA != null && CauseOfDeathConditionA.Component != null)
                {
                    var intervalComp = CauseOfDeathConditionA.Component.FirstOrDefault(entry => ((Observation.ComponentComponent)entry).Code != null &&
                       ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault() != null && ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault().Code == "69440-6");
                    if (intervalComp?.Value != null && intervalComp.Value as FhirString != null)
                    {
                        return intervalComp.Value.ToString();
                    }
                }
                return null;
            }
            set
            {
                if (String.IsNullOrEmpty(value)) {
                  return;
                }
                if (CauseOfDeathConditionA == null)
                {
                    CauseOfDeathConditionA = CauseOfDeathCondition(0);
                }
                // Find correct component; if doesn't exist add another
                var intervalComp = CauseOfDeathConditionA.Component.FirstOrDefault(entry => ((Observation.ComponentComponent)entry).Code != null &&
                       ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault() != null && ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault().Code == "69440-6");
                if (intervalComp != null)
                {
                    ((Observation.ComponentComponent)intervalComp).Value = new FhirString(value);
                }
                else 
                {
                    Observation.ComponentComponent component = new Observation.ComponentComponent();
                    component.Code = new CodeableConcept(CodeSystems.LOINC, "69440-6", "Disease onset to death interval", null);
                    component.Value = new FhirString(value);
                    CauseOfDeathConditionA.Component.Add(component);
                }
            }

        }

        // /// <summary>Cause of Death Part I Code, Line a.</summary>
        // /// <value>the immediate cause of death coding. A Dictionary representing a code, containing the following key/value pairs:
        // /// <para>"code" - the code</para>
        // /// <para>"system" - the code system this code belongs to</para>
        // /// <para>"display" - a human readable meaning of the code</para>
        // /// </value>
        // /// <example>
        // /// <para>// Setter:</para>
        // /// <para>Dictionary&lt;string, string&gt; code = new Dictionary&lt;string, string&gt;();</para>
        // /// <para>code.Add("code", "I21.0");</para>
        // /// <para>code.Add("system", "http://hl7.org/fhir/sid/icd-10");</para>
        // /// <para>code.Add("display", "Acute transmural myocardial infarction of anterior wall");</para>
        // /// <para>ExampleDeathRecord.CODE1A = code;</para>
        // /// <para>// Getter:</para>
        // /// <para>Console.WriteLine($"\tCause of Death: {ExampleDeathRecord.CODE1A['display']}");</para>
        // /// </example>
        // [Property("CODE1A", Property.Types.Dictionary, "Death Certification", "Cause of Death Part I Code, Line a.", false, IGURL.CauseOfDeathPart1, false, 100)]
        // [PropertyParam("code", "The code used to describe this concept.")]
        // [PropertyParam("system", "The relevant code system.")]
        // [PropertyParam("display", "The human readable version of this code.")]
        // public Dictionary<string, string> CODE1A
        // {
        //     get
        //     {
        //         if (CauseOfDeathConditionA != null && CauseOfDeathConditionA.Code != null)
        //         {
        //             return CodeableConceptToDict(CauseOfDeathConditionA.Code);
        //         }
        //         return EmptyCodeDict();
        //     }
        //     set
        //     {
        //         if(CauseOfDeathConditionA == null)
        //         {
        //             CauseOfDeathConditionA = CauseOfDeathCondition(0);
        //         }
        //         if (CauseOfDeathConditionA.Code != null)
        //         {
        //             CodeableConcept code = DictToCodeableConcept(value);
        //             code.Text = CauseOfDeathConditionA.Code.Text;
        //             CauseOfDeathConditionA.Code = code;
        //         }
        //         else
        //         {
        //         CauseOfDeathConditionA.Code = DictToCodeableConcept(value);
        //         }
        //     }
        // }

        /// <summary>Cause of Death Part I, Line b.</summary>
        /// <value>the first underlying cause of death literal.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.COD1B = "Acute myocardial infarction";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Cause: {ExampleDeathRecord.COD1B}");</para>
        /// </example>
        [Property("COD1B", Property.Types.String, "Death Certification", "Cause of Death Part I, Line b.", false, IGURL.CauseOfDeathPart1, false, 100)]
        public string COD1B
        {
            get
            {
                if (CauseOfDeathConditionB != null && CauseOfDeathConditionB.Value != null)
                {
                    return (CodeableConceptToDict((CodeableConcept)CauseOfDeathConditionB.Value))["text"];
                }
                return null;
            }
            set
            {
                if (String.IsNullOrWhiteSpace(value)) {
                    return;
                }
                if (CauseOfDeathConditionB == null)
                {
                    CauseOfDeathConditionB = CauseOfDeathCondition(1);
                }
                CauseOfDeathConditionB.Value = new CodeableConcept(null, null, null, value);
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
        [Property("INTERVAL1B", Property.Types.String, "Death Certification", "Cause of Death Part I Interval, Line b.", false, IGURL.CauseOfDeathPart1, false, 100)]
        public string INTERVAL1B
        {
            get
            {
                if (CauseOfDeathConditionB != null && CauseOfDeathConditionB.Component != null)
                {
                    var intervalComp = CauseOfDeathConditionB.Component.FirstOrDefault(entry => ((Observation.ComponentComponent)entry).Code != null &&
                       ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault() != null && ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault().Code == "69440-6");
                    if (intervalComp?.Value != null && intervalComp.Value as FhirString != null)
                    {
                        return intervalComp.Value.ToString();
                    }
                }
                return null;
            }
            set
            {
                if (String.IsNullOrWhiteSpace(value)) {
                    return;
                }
                if (CauseOfDeathConditionB == null)
                {
                    CauseOfDeathConditionB = CauseOfDeathCondition(1);
                }
                // Find correct component; if doesn't exist add another
                var intervalComp = CauseOfDeathConditionB.Component.FirstOrDefault(entry => ((Observation.ComponentComponent)entry).Code != null &&
                       ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault() != null && ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault().Code == "69440-6");
                if (intervalComp != null)
                {
                    ((Observation.ComponentComponent)intervalComp).Value = new FhirString(value);
                }
                else
                {
                    Observation.ComponentComponent component = new Observation.ComponentComponent();
                    component.Code = new CodeableConcept(CodeSystems.LOINC, "69440-6", "Disease onset to death interval", null);
                    component.Value = new FhirString(value);
                    CauseOfDeathConditionB.Component.Add(component);
                }
            }

        }
        // /// <summary>Cause of Death Part I Code, Line b.</summary>
        // /// <value>the first underlying cause of death coding. A Dictionary representing a code, containing the following key/value pairs:
        // /// <para>"code" - the code</para>
        // /// <para>"system" - the code system this code belongs to</para>
        // /// <para>"display" - a human readable meaning of the code</para>
        // /// </value>
        // /// <example>
        // /// <para>// Setter:</para>
        // /// <para>Dictionary&lt;string, string&gt; code = new Dictionary&lt;string, string&gt;();</para>
        // /// <para>code.Add("code", "I21.9");</para>
        // /// <para>code.Add("system", "http://hl7.org/fhir/sid/icd-10");</para>
        // /// <para>code.Add("display", "Acute myocardial infarction, unspecified");</para>
        // /// <para>ExampleDeathRecord.CODE1B = code;</para>
        // /// <para>// Getter:</para>
        // /// <para>Console.WriteLine($"\tCause of Death: {ExampleDeathRecord.CODE1B['display']}");</para>
        // /// </example>
        // [Property("CODE1B", Property.Types.Dictionary, "Death Certification", "Cause of Death Part I Code, Line b.", false, IGURL.CauseOfDeathPart1, false, 100)]
        // [PropertyParam("code", "The code used to describe this concept.")]
        // [PropertyParam("system", "The relevant code system.")]
        // [PropertyParam("display", "The human readable version of this code.")]
        // public Dictionary<string, string> CODE1B
        // {
        //     get
        //     {
        //         if (CauseOfDeathConditionB != null && CauseOfDeathConditionB.Code != null)
        //         {
        //             return CodeableConceptToDict(CauseOfDeathConditionB.Code);
        //         }
        //         return EmptyCodeDict();
        //     }
        //     set
        //     {
        //         if(CauseOfDeathConditionB == null)
        //         {
        //             CauseOfDeathConditionB = CauseOfDeathCondition(1);
        //         }
        //         if (CauseOfDeathConditionB.Code != null)
        //         {
        //             CodeableConcept code = DictToCodeableConcept(value);
        //             code.Text = CauseOfDeathConditionB.Code.Text;
        //             CauseOfDeathConditionB.Code = code;
        //         }
        //         else
        //         {
        //             CauseOfDeathConditionB.Code = DictToCodeableConcept(value);
        //         }
        //                     }
        // }

        /// <summary>Cause of Death Part I, Line c.</summary>
        /// <value>the second underlying cause of death literal.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.COD1C = "Coronary artery thrombosis";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Cause: {ExampleDeathRecord.COD1C}");</para>
        /// </example>
        [Property("COD1C", Property.Types.String, "Death Certification", "Cause of Death Part I, Line c.", false, IGURL.CauseOfDeathPart1, false, 100)]
        public string COD1C
        {
            get
            {
                if (CauseOfDeathConditionC != null && CauseOfDeathConditionC.Value != null)
                {
                    return (CodeableConceptToDict((CodeableConcept)CauseOfDeathConditionC.Value))["text"];
                }
                return null;
            }
            set
            {
                if (String.IsNullOrWhiteSpace(value)) {
                    return;
                }
                if (CauseOfDeathConditionC == null)
                {
                    CauseOfDeathConditionC = CauseOfDeathCondition(2);
                }
                CauseOfDeathConditionC.Value = new CodeableConcept(null, null, null, value);
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
        [Property("INTERVAL1C", Property.Types.String, "Death Certification", "Cause of Death Part I Interval, Line c.", false, IGURL.CauseOfDeathPart1, false, 100)]
        public string INTERVAL1C
        {
            get
            {
                if (CauseOfDeathConditionC != null && CauseOfDeathConditionC.Component != null)
                {
                    var intervalComp = CauseOfDeathConditionC.Component.FirstOrDefault(entry => ((Observation.ComponentComponent)entry).Code != null &&
                       ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault() != null && ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault().Code == "69440-6");
                    if (intervalComp?.Value != null && intervalComp.Value as FhirString != null)
                    {
                        return intervalComp.Value.ToString();
                    }
                }
                return null;
            }
            set
            {
                if (String.IsNullOrWhiteSpace(value)) {
                    return;
                }
                if (CauseOfDeathConditionC == null)
                {
                    CauseOfDeathConditionC = CauseOfDeathCondition(2);
                }
                // Find correct component; if doesn't exist add another
                var intervalComp = CauseOfDeathConditionC.Component.FirstOrDefault(entry => ((Observation.ComponentComponent)entry).Code != null &&
                       ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault() != null && ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault().Code == "69440-6");
                if (intervalComp != null)
                {
                    ((Observation.ComponentComponent)intervalComp).Value = new FhirString(value);
                }
                else
                {
                    Observation.ComponentComponent component = new Observation.ComponentComponent();
                    component.Code = new CodeableConcept(CodeSystems.LOINC, "69440-6", "Disease onset to death interval", null);
                    component.Value = new FhirString(value);
                    CauseOfDeathConditionC.Component.Add(component);
                }
            }
        }

        // /// <summary>Cause of Death Part I Code, Line c.</summary>
        // /// <value>the second underlying cause of death coding. A Dictionary representing a code, containing the following key/value pairs:
        // /// <para>"code" - the code</para>
        // /// <para>"system" - the code system this code belongs to</para>
        // /// <para>"display" - a human readable meaning of the code</para>
        // /// </value>
        // /// <example>
        // /// <para>// Setter:</para>
        // /// <para>Dictionary&lt;string, string&gt; code = new Dictionary&lt;string, string&gt;();</para>
        // /// <para>code.Add("code", "I21.9");</para>
        // /// <para>code.Add("system", "http://hl7.org/fhir/sid/icd-10");</para>
        // /// <para>code.Add("display", "Acute myocardial infarction, unspecified");</para>
        // /// <para>ExampleDeathRecord.CODE1C = code;</para>
        // /// <para>// Getter:</para>
        // /// <para>Console.WriteLine($"\tCause of Death: {ExampleDeathRecord.CODE1C['display']}");</para>
        // /// </example>
        // [Property("CODE1C", Property.Types.Dictionary, "Death Certification", "Cause of Death Part I Code, Line c.", false, IGURL.CauseOfDeathPart1, false, 100)]
        // [PropertyParam("code", "The code used to describe this concept.")]
        // [PropertyParam("system", "The relevant code system.")]
        // [PropertyParam("display", "The human readable version of this code.")]
        // public Dictionary<string, string> CODE1C
        // {
        //     get
        //     {
        //         if (CauseOfDeathConditionC != null && CauseOfDeathConditionC.Code != null)
        //         {
        //             return CodeableConceptToDict(CauseOfDeathConditionC.Code);
        //         }
        //         return EmptyCodeDict();
        //     }
        //     set
        //     {
        //         if(CauseOfDeathConditionC == null)
        //         {
        //             CauseOfDeathConditionC = CauseOfDeathCondition(2);
        //         }
        //        if (CauseOfDeathConditionC.Code != null)
        //         {
        //             CodeableConcept code = DictToCodeableConcept(value);
        //             code.Text = CauseOfDeathConditionC.Code.Text;
        //             CauseOfDeathConditionC.Code = code;
        //         }
        //         else
        //         {
        //             CauseOfDeathConditionC.Code = DictToCodeableConcept(value);
        //         }
        //     }
        // }

        /// <summary>Cause of Death Part I, Line d.</summary>
        /// <value>the third underlying cause of death literal.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.COD1D = "Atherosclerotic coronary artery disease";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Cause: {ExampleDeathRecord.COD1D}");</para>
        /// </example>
        [Property("COD1D", Property.Types.String, "Death Certification", "Cause of Death Part I, Line d.", false, IGURL.CauseOfDeathPart1, false, 100)]
        public string COD1D
        {
            get
            {
                if (CauseOfDeathConditionD != null && CauseOfDeathConditionD.Value != null)
                {
                    return (CodeableConceptToDict((CodeableConcept)CauseOfDeathConditionD.Value))["text"];
                }
                return null;
            }
            set
            {
                if (String.IsNullOrWhiteSpace(value)) {
                    return;
                }
                if (CauseOfDeathConditionD == null)
                {
                    CauseOfDeathConditionD = CauseOfDeathCondition(3);
                }
                CauseOfDeathConditionD.Value = new CodeableConcept(null, null, null, value);
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
        [Property("INTERVAL1D", Property.Types.String, "Death Certification", "Cause of Death Part I Interval, Line d.", false, IGURL.CauseOfDeathPart1, false, 100)]
        public string INTERVAL1D
        {
            get
            {
                if (CauseOfDeathConditionD != null && CauseOfDeathConditionD.Component != null)
                {
                    var intervalComp = CauseOfDeathConditionD.Component.FirstOrDefault(entry => ((Observation.ComponentComponent)entry).Code != null &&
                       ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault() != null && ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault().Code == "69440-6");
                    if (intervalComp?.Value != null && intervalComp.Value as FhirString != null)
                    {
                        return intervalComp.Value.ToString();
                    }
                }
                return null;
            }
            set
            {
                if (String.IsNullOrWhiteSpace(value)) {
                    return;
                }
                if (CauseOfDeathConditionD == null)
                {
                    CauseOfDeathConditionD = CauseOfDeathCondition(3);
                }
                // Find correct component; if doesn't exist add another
                var intervalComp = CauseOfDeathConditionD.Component.FirstOrDefault(entry => ((Observation.ComponentComponent)entry).Code != null &&
                       ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault() != null && ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault().Code == "69440-6");
                if (intervalComp != null)
                {
                    ((Observation.ComponentComponent)intervalComp).Value = new FhirString(value);
                }
                else
                {
                    Observation.ComponentComponent component = new Observation.ComponentComponent();
                    component.Code = new CodeableConcept(CodeSystems.LOINC, "69440-6", "Disease onset to death interval", null);
                    component.Value = new FhirString(value);
                    CauseOfDeathConditionD.Component.Add(component);
                }
            }
        }

        // /// <summary>Cause of Death Part I Code, Line d.</summary>
        // /// <value>the third underlying cause of death coding. A Dictionary representing a code, containing the following key/value pairs:
        // /// <para>"code" - the code</para>
        // /// <para>"system" - the code system this code belongs to</para>
        // /// <para>"display" - a human readable meaning of the code</para>
        // /// </value>
        // /// <example>
        // /// <para>// Setter:</para>
        // /// <para>Dictionary&lt;string, string&gt; code = new Dictionary&lt;string, string&gt;();</para>
        // /// <para>code.Add("code", "I21.9");</para>
        // /// <para>code.Add("system", "http://hl7.org/fhir/sid/icd-10");</para>
        // /// <para>code.Add("display", "Acute myocardial infarction, unspecified");</para>
        // /// <para>ExampleDeathRecord.CODE1D = code;</para>
        // /// <para>// Getter:</para>
        // /// <para>Console.WriteLine($"\tCause of Death: {ExampleDeathRecord.CODE1D['display']}");</para>
        // /// </example>
        // [Property("CODE1D", Property.Types.Dictionary, "Death Certification", "Cause of Death Part I Code, Line d.", false, IGURL.CauseOfDeathPart1, false, 100)]
        // [PropertyParam("code", "The code used to describe this concept.")]
        // [PropertyParam("system", "The relevant code system.")]
        // [PropertyParam("display", "The human readable version of this code.")]
        // public Dictionary<string, string> CODE1D
        // {
        //     get
        //     {
        //         if (CauseOfDeathConditionD != null && CauseOfDeathConditionD.Code != null)
        //         {
        //             return CodeableConceptToDict(CauseOfDeathConditionD.Code);
        //         }
        //         return EmptyCodeDict();
        //     }
        //     set
        //     {
        //         if (CauseOfDeathConditionD == null)
        //         {
        //             CauseOfDeathConditionD = CauseOfDeathCondition(3);
        //         }
        //        if (CauseOfDeathConditionD.Code != null)
        //         {
        //             CodeableConcept code = DictToCodeableConcept(value);
        //             code.Text = CauseOfDeathConditionD.Code.Text;
        //             CauseOfDeathConditionD.Code = code;
        //         }
        //         else
        //         {
        //             CauseOfDeathConditionD.Code = DictToCodeableConcept(value);
        //         }
        //     }
        // }



        /////////////////////////////////////////////////////////////////////////////////
        //
        // Record Properties: Decedent Demographics
        //
        /////////////////////////////////////////////////////////////////////////////////

        /// <summary>Decedent's Legal Name - Given. Middle name should be the last entry.</summary>
        /// <value>the decedent's name (first, etc., middle)</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>string[] names = { "Example", "Something", "Middle" };</para>
        /// <para>ExampleDeathRecord.GivenNames = names;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Decedent Given Name(s): {string.Join(", ", ExampleDeathRecord.GivenNames)}");</para>
        /// </example>
        [Property("Given Names", Property.Types.StringArr, "Decedent Demographics", "Decedent's Given Name(s).", true, IGURL.Decedent, true, 0)]
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
                updateGivenHumanName(value, Decedent.Name);
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
        [Property("Family Name", Property.Types.String, "Decedent Demographics", "Decedent's Family Name.", true, IGURL.Decedent, true, 5)]
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
                    if (value.Equals("UNKNOWN"))
                        name.Family = null;
                    else
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

        /// <summary>Decedent's Suffix.</summary>
        /// <value>the decedent's suffix</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.Suffix = "Jr.";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Decedent Suffix: {ExampleDeathRecord.Suffix}");</para>
        /// </example>
        [Property("Suffix", Property.Types.String, "Decedent Demographics", "Decedent's Suffix.", true, IGURL.Decedent, true, 6)]
        [FHIRPath("Bundle.entry.resource.where($this is Patient)", "name")]
        public string Suffix
        {
            get
            {
                return GetFirstString("Bundle.entry.resource.where($this is Patient).name.where(use='official').suffix");
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

        /// <summary>Decedent's Maiden Name.</summary>
        /// <value>the decedent's maiden name (i.e. last name before marriage)</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.MaidenName = "Last";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Decedent's Maiden Name: {ExampleDeathRecord.MaidenName}");</para>
        /// </example>
        [Property("Maiden Name", Property.Types.String, "Decedent Demographics", "Decedent's Maiden Name.", true, IGURL.Decedent, true, 10)]
        [FHIRPath("Bundle.entry.resource.where($this is Patient).name.where(use='maiden')", "family")]
        public string MaidenName
        {
            get
            {
                return GetFirstString("Bundle.entry.resource.where($this is Patient).name.where(use='maiden').text");
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
                    name.Text = value;
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
        [Property("Gender", Property.Types.String, "Decedent Demographics", "Decedent's Gender.", true, IGURL.Decedent, true, 11)]
        [FHIRPath("Bundle.entry.resource.where($this is Patient)", "gender")]
        public string Gender
        {
            get
            {
                return GetFirstString("Bundle.entry.resource.where($this is Patient).gender");
            }
            set
            {
                switch (value)
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

        /// <summary>Decedent's Sex at Death.</summary>
        /// <value>the decedent's sex at time of death</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; sex = new Dictionary&lt;string, string&gt;();</para>
        /// <para>sex.Add("code", "female");</para>
        /// <para>sex.Add("system", "http://hl7.org/fhir/administrative-gender");</para>
        /// <para>sex.Add("display", "female");</para>
        /// <para>ExampleDeathRecord.SexAtDeath = sex;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Sex at Time of Death: {ExampleDeathRecord.SexAtDeath}");</para>
        /// </example>
        [Property("Sex At Death", Property.Types.Dictionary, "Decedent Demographics", "Decedent's Sex at Death.", true, IGURL.Decedent, true, 12)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Patient).extension.where(url='http://hl7.org/fhir/us/vrdr/StructureDefinition/NVSS-SexAtDeath')", "")]
        public Dictionary<string, string> SexAtDeath
        {
            get
            {
                if (Decedent != null)
                {
                    Extension sex = Decedent.Extension.Find(ext => ext.Url == ExtensionURL.NVSSSexAtDeath);
                    if (sex != null && sex.Value != null && sex.Value as CodeableConcept != null)
                    {
                        return CodeableConceptToDict((CodeableConcept)sex.Value);
                    }
                }
                return EmptyCodeableDict();
            }
            set
            {
                Decedent.Extension.RemoveAll(ext => ext.Url == ExtensionURL.NVSSSexAtDeath);
                if (IsDictEmptyOrDefault(value) && Decedent.Extension == null)
                {
                    return;
                }
                Extension sex = new Extension();
                sex.Url = ExtensionURL.NVSSSexAtDeath;
                sex.Value = DictToCodeableConcept(value);
                Decedent.Extension.Add(sex);
            }
        }

        /// <summary>Decedent's Sex At Death Helper</summary>
        /// <value>Decedent's sex at death.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.SexAtDeathHelper = VRDR.ValueSets.AdministrativeGender.Male;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Decedent's SexAtDeathHelper: {ExampleDeathRecord.SexAtDeathHelper}");</para>
        /// </example>
        [Property("Sex At Death Helper", Property.Types.String, "Decedent Demographics", "Decedent's Sex At Death.", false, IGURL.Decedent, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [FHIRPath("Bundle.entry.resource.where($this is Patient).extension.where(url='http://hl7.org/fhir/us/vrdr/StructureDefinition/NVSS-SexAtDeath')", "")]
        public string SexAtDeathHelper
        {
            get
            {
                if (SexAtDeath.ContainsKey("code") && !String.IsNullOrWhiteSpace(SexAtDeath["code"]))
                {
                    return SexAtDeath["code"];
                }
                return null;
            }
            set
            {
                if(!String.IsNullOrWhiteSpace(value))
                {
                    SetCodeValue("SexAtDeath", value, VRDR.ValueSets.AdministrativeGender.Codes);
                }
            }
        }

        // // Should this be removed for IG v1.3 updates?
        // /// <summary>Decedent's Birth Sex.</summary>
        // /// <value>the decedent's birth sex</value>
        // /// <example>
        // /// <para>// Setter:</para>
        // /// <para>ExampleDeathRecord.BirthSex = "F";</para>
        // /// <para>// Getter:</para>
        // /// <para>Console.WriteLine($"Birth Sex: {ExampleDeathRecord.BirthSex}");</para>
        // /// </example>
        // [Property("Birth Sex", Property.Types.String, "Decedent Demographics", "Decedent's Birth Sex.", true, IGURL.Decedent, true, 12)]
        // [FHIRPath("Bundle.entry.resource.where($this is Patient).extension.where(url='http://hl7.org/fhir/us/core/StructureDefinition/us-core-birthsex')", "")]
        // public string BirthSex
        // {
        //     get
        //     {
        //         Extension birthsex = Decedent.Extension.Find(ext => ext.Url == OtherExtensionURL.BirthSex);
        //         if (birthsex != null && birthsex.Value != null && birthsex.Value.GetType() == typeof(Code))
        //         {
        //             return ((Code)birthsex.Value).Value;
        //         }
        //         return null;
        //     }
        //     set
        //     {
        //         Decedent.Extension.RemoveAll(ext => ext.Url == OtherExtensionURL.BirthSex);
        //         Extension birthsex = new Extension();
        //         birthsex.Url = OtherExtensionURL.BirthSex;
        //         birthsex.Value = new Code(value);
        //         Decedent.Extension.Add(birthsex);
        //     }
        // }




        private void AddBirthDateToDecedent()
        {
            Decedent.BirthDateElement = new Date();
            Decedent.BirthDateElement.Extension.Add(NewBlankPartialDateTimeExtension(false));
        }

        /// <summary>Decedent's Year of Birth.</summary>
        /// <value>the decedent's year of birth, or -1 if explicitly unknown, or null if never specified</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.BirthYear = 1928;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Decedent Year of Birth: {ExampleDeathRecord.BirthYear}");</para>
        /// </example>
        [Property("BirthYear", Property.Types.Int32, "Decedent Demographics", "Decedent's Year of Birth.", true, IGURL.Decedent, true, 14)]
        [FHIRPath("Bundle.entry.resource.where($this is Patient).birthDate", "")]
        public int? BirthYear
        {
            get
            {
                if (Decedent != null && Decedent.BirthDateElement != null)
                {
                    return GetDateFragmentOrPartialDate(Decedent.BirthDateElement, ExtensionURL.DateYear);
                }
                return null;
            }
            set
            {
                if (Decedent.BirthDateElement == null)
                {
                    AddBirthDateToDecedent();
                }
                SetPartialDate(Decedent.BirthDateElement.Extension.Find(ext => ext.Url == ExtensionURL.PartialDate), ExtensionURL.DateYear, value);
            }
        }

        /// <summary>Decedent's Month of Birth.</summary>
        /// <value>the decedent's month of birth, or -1 if explicitly unknown, or null if never specified</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.BirthMonth = 11;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Decedent Month of Birth: {ExampleDeathRecord.BirthMonth}");</para>
        /// </example>
        [Property("BirthMonth", Property.Types.Int32, "Decedent Demographics", "Decedent's Month of Birth.", true, IGURL.Decedent, true, 14)]
        [FHIRPath("Bundle.entry.resource.where($this is Patient).birthDate", "")]
        public int? BirthMonth
        {
            get
            {
                if (Decedent != null && Decedent.BirthDateElement != null)
                {
                    return GetDateFragmentOrPartialDate(Decedent.BirthDateElement, ExtensionURL.DateMonth);
                }
                return null;
            }
            set
            {
                if (Decedent.BirthDateElement == null)
                {
                    AddBirthDateToDecedent();
                }
                SetPartialDate(Decedent.BirthDateElement.Extension.Find(ext => ext.Url == ExtensionURL.PartialDate), ExtensionURL.DateMonth, value);
            }
        }

        /// <summary>Decedent's Day of Birth.</summary>
        /// <value>the decedent's day of birth, or -1 if explicitly unknown, or null if never specified</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.BirthDay = 11;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Decedent Day of Birth: {ExampleDeathRecord.BirthDay}");</para>
        /// </example>
        [Property("BirthDay", Property.Types.Int32, "Decedent Demographics", "Decedent's Day of Birth.", true, IGURL.Decedent, true, 14)]
        [FHIRPath("Bundle.entry.resource.where($this is Patient).birthDate", "")]
        public int? BirthDay
        {
            get
            {
                if (Decedent != null && Decedent.BirthDateElement != null)
                {
                    return GetDateFragmentOrPartialDate(Decedent.BirthDateElement, ExtensionURL.DateDay);
                }
                return null;
            }
            set
            {
                if (Decedent.BirthDateElement == null)
                {
                    AddBirthDateToDecedent();
                }
                SetPartialDate(Decedent.BirthDateElement.Extension.Find(ext => ext.Url == ExtensionURL.PartialDate), ExtensionURL.DateDay, value);
            }
        }

        /// <summary>Decedent's Date of Birth.</summary>
        /// <value>the decedent's date of birth</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.DateOfBirth = "1940-02-19";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Decedent Date of Birth: {ExampleDeathRecord.DateOfBirth}");</para>
        /// </example>
        [Property("Date Of Birth", Property.Types.String, "Decedent Demographics", "Decedent's Date of Birth.", true, IGURL.Decedent, true, 14)]
        [FHIRPath("Bundle.entry.resource.where($this is Patient).birthDate", "")]
        public string DateOfBirth
        {
            get
            {
                // We support this legacy API entrypoint via the new partial date entrypoints
                if (BirthYear != null && BirthYear != -1 && BirthMonth != null && BirthMonth != -1 && BirthDay != null && BirthDay != -1)
                {
                    Date result = new Date((int)BirthYear, (int)BirthMonth, (int)BirthDay);
                    return result.ToString();
                }
                return null;
            }
            set
            {
                // We support this legacy API entrypoint via the new partial date entrypoints
                DateTimeOffset parsedDate;
                if (DateTimeOffset.TryParse(value, out parsedDate))
                {
                    BirthYear = parsedDate.Year;
                    BirthMonth = parsedDate.Month;
                    BirthDay = parsedDate.Day;
                }
            }
        }

        /// <summary>Decedent's Residence.</summary>
        /// <value>Decedent's Residence. A Dictionary representing residence address, containing the following key/value pairs:
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
        /// <para>address.Add("addressCityC", "1234");</para>
        /// <para>address.Add("addressCounty", "Suffolk");</para>
        /// <para>address.Add("addressState", "MA");</para>
        /// <para>address.Add("addressZip", "12345");</para>
        /// <para>address.Add("addressCountry", "US");</para>
        /// <para>SetterDeathRecord.Residence = address;</para> (addressStnum, 6)
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"State of residence: {ExampleDeathRecord.Residence["addressState"]}");</para>
        /// </example>
        [Property("Residence", Property.Types.Dictionary, "Decedent Demographics", "Decedent's residence.", true, IGURL.Decedent, true, 19)]
        [PropertyParam("addressLine1", "address, line one")]
        [PropertyParam("addressLine2", "address, line two")]
        [PropertyParam("addressCity", "address, city")]
        [PropertyParam("addressCityC", "address, _city")]
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
                    Dictionary<string, string> address = AddressToDict(Decedent.Address.First());
                    return address;
                }
                return EmptyAddrDict();
            }
            set
            {
                if (Decedent.Address == null)
                {
                    Decedent.Address = new List<Address>();
                }
                // Clear out the address since we're replacing it completely, except we need to keep the "WithinCityLimits" extension if present
                Extension withinCityLimits = Decedent.Address?.FirstOrDefault()?.Extension?.Where(ext => ext.Url == ExtensionURL.WithinCityLimitsIndicator)?.FirstOrDefault();
                Decedent.Address.Clear();
                Decedent.Address.Add(DictToAddress(value));
                if (withinCityLimits != null) Decedent.Address.FirstOrDefault().Extension.Add(withinCityLimits);

                // Now encode -
                //        Address.Country as PH_Country_GEC
                //        Adress.County as PHVS_DivisionVitalStatistics__County
                //        Address.City as 5 digit code as per FIPS 55-3, which are included as the preferred alternate code in https://phinvads.cdc.gov/vads/ViewValueSet.action?id=D06EE94C-4D4C-440A-AD2A-1C3CB35E6D08#
                //Address a = Decedent.Address.FirstOrDefault();
            }
        }

        /// <summary>Decedent's residence is/is not within city limits.</summary>
        /// <value>Decedent's residence is/is not within city limits. A Dictionary representing a code, containing the following key/value pairs:
        /// <para>"code" - the code</para>
        /// <para>"system" - the code system this code belongs to</para>
        /// <para>"display" - a human readable meaning of the code</para></value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; within = new Dictionary&lt;string, string&gt;();</para>
        /// <para>within.Add("code", "Y");</para>
        /// <para>within.Add("system", CodeSystems.PH_YesNo_HL7_2x);</para>
        /// <para>within.Add("display", "Yes");</para>
        /// <para>SetterDeathRecord.ResidenceWithinCityLimits = within;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Residence within city limits: {ExampleDeathRecord.ResidenceWithinCityLimits['display']}");</para>
        /// </example>
        [Property("Residence Within City Limits", Property.Types.Dictionary, "Decedent Demographics", "Decedent's residence is/is not within city limits.", true, IGURL.Decedent, true, 20)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Patient)", "address")]
        public Dictionary<string, string> ResidenceWithinCityLimits
        {
            get
            {
                if (Decedent != null && Decedent.Address.FirstOrDefault() != null)
                {
                    Extension cityLimits = Decedent.Address.FirstOrDefault().Extension.Where(ext => ext.Url == ExtensionURL.WithinCityLimitsIndicator).FirstOrDefault();
                    if (cityLimits != null && cityLimits.Value != null && cityLimits.Value as Coding != null)
                    {
                        return CodingToDict((Coding)cityLimits.Value);
                    }
                }
                return EmptyCodeDict();
            }
            set
            {
                if (Decedent != null)
                {
                    if (Decedent.Address.FirstOrDefault() == null)
                    {
                        Decedent.Address.Add(new Address());
                    }
                    Decedent.Address.FirstOrDefault().Extension.RemoveAll(ext => ext.Url == ExtensionURL.WithinCityLimitsIndicator);
                    Extension withinCityLimits = new Extension();
                    withinCityLimits.Url = ExtensionURL.WithinCityLimitsIndicator;
                    withinCityLimits.Value = DictToCoding(value);
                    Decedent.Address.FirstOrDefault().Extension.Add(withinCityLimits);
                }
            }
        }

        /// <summary>Residence Within City Limits Helper</summary>
        /// <value>Residence Within City Limits.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.ResidenceWithinCityLimitsHelper = VRDR.ValueSets.YesNoUnknown.Y;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Decedent's Residence within city limits: {ExampleDeathRecord.ResidenceWithinCityLimitsHelper}");</para>
        /// </example>
        [Property("ResidenceWithinCityLimits Helper", Property.Types.String, "Decedent Demographics", "Decedent's ResidenceWithinCityLimits.", false, IGURL.Decedent, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [FHIRPath("Bundle.entry.resource.where($this is Patient)", "address")]
        public string ResidenceWithinCityLimitsHelper
        {
            get
            {
                if (ResidenceWithinCityLimits.ContainsKey("code") && !String.IsNullOrWhiteSpace(ResidenceWithinCityLimits["code"]))
                {
                    return ResidenceWithinCityLimits["code"];
                }
                return null;
            }
            set
            {
                if(!String.IsNullOrWhiteSpace(value))
                {
                    SetCodeValue("ResidenceWithinCityLimits", value, VRDR.ValueSets.YesNoUnknown.Codes);
                }
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
        [Property("SSN", Property.Types.String, "Decedent Demographics", "Decedent's Social Security Number.", true, IGURL.Decedent, true, 13)]
        [FHIRPath("Bundle.entry.resource.where($this is Patient).identifier.where(system='http://hl7.org/fhir/sid/us-ssn')", "")]
        public string SSN
        {
            get
            {
                return GetFirstString("Bundle.entry.resource.where($this is Patient).identifier.where(system = 'http://hl7.org/fhir/sid/us-ssn').value");
            }
            set
            {
                Decedent.Identifier.RemoveAll(iden => iden.System == CodeSystems.US_SSN);
                if (String.IsNullOrWhiteSpace(value))
                {
                    return;
                }
                Identifier ssn = new Identifier();
                ssn.Type = new CodeableConcept(CodeSystems.HL7_identifier_type, "SB", "Social Beneficiary Identifier", null);
                ssn.System = CodeSystems.US_SSN;
                ssn.Value = value.Replace("-", string.Empty).Replace(" ", string.Empty);
                Decedent.Identifier.Add(ssn);
            }
        }

        /// <summary>Decedent's Ethnicity Hispanic Mexican.</summary>
        /// <value>the decedent's ethnicity. A Dictionary representing a code, containing the following key/value pairs:
        /// <para>"code" - the code</para>
        /// <para>"system" - the code system this code belongs to</para>
        /// <para>"display" - a human readable meaning of the code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; ethnicity = new Dictionary&lt;string, string&gt;();</para>
        /// <para>ethnicity.Add("code", "Y");</para>
        /// <para>ethnicity.Add("system", CodeSystems.YesNo);</para>
        /// <para>ethnicity.Add("display", "Yes");</para>
        /// <para>ExampleDeathRecord.Ethnicity = ethnicity;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Ethnicity: {ExampleDeathRecord.Ethnicity1['display']}");</para>
        /// </example>
        [Property("Ethnicity1", Property.Types.Dictionary, "Decedent Demographics", "Decedent's Ethnicity Hispanic Mexican.", true, IGURL.InputRaceAndEthnicity, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='inputraceandethnicity')", "")]
        public Dictionary<string, string> Ethnicity1
        {
            get
            {
                if (InputRaceAndEthnicityObs != null)
                {
                    Observation.ComponentComponent ethnicity = InputRaceAndEthnicityObs.Component.FirstOrDefault(c => c.Code.Coding[0].Code == NvssEthnicity.Mexican);
                    if (ethnicity != null && ethnicity.Value != null && ethnicity.Value as CodeableConcept != null)
                    {
                        return CodeableConceptToDict((CodeableConcept)ethnicity.Value);
                    }
                }
                return EmptyCodeableDict();
            }
            set
            {
                if (InputRaceAndEthnicityObs == null)
                {
                    CreateInputRaceEthnicityObs();
                }
                InputRaceAndEthnicityObs.Component.RemoveAll(c => c.Code.Coding[0].Code == NvssEthnicity.Mexican);
                Observation.ComponentComponent component = new Observation.ComponentComponent();
                component.Code = new CodeableConcept(CodeSystems.ComponentCode, NvssEthnicity.Mexican, NvssEthnicity.MexicanDisplay, null);
                component.Value = DictToCodeableConcept(value);
                InputRaceAndEthnicityObs.Component.Add(component);
            }
        }

        /// <summary>Decedent's Ethnicity 1 Helper</summary>
        /// <value>Decedent's Ethnicity 1.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.EthnicityLevel = VRDR.ValueSets.YesNoUnknown.Yes;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Decedent's Ethnicity: {ExampleDeathRecord.Ethnicity1Helper}");</para>
        /// </example>
        [Property("Ethnicity 1 Helper", Property.Types.String, "Decedent Demographics", "Decedent's Ethnicity 1.", false, IGURL.InputRaceAndEthnicity, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='inputraceandethnicity')", "")]
        public string Ethnicity1Helper
        {
            get
            {
                if (Ethnicity1.ContainsKey("code") && !String.IsNullOrWhiteSpace(Ethnicity1["code"]))
                {
                    return Ethnicity1["code"];
                }
                return null;
            }
            set
            {
                if(!String.IsNullOrWhiteSpace(value))
                {
                    SetCodeValue("Ethnicity1", value, VRDR.ValueSets.HispanicNoUnknown.Codes);
                }
            }
        }

        /// <summary>Decedent's Ethnicity Hispanic Puerto Rican.</summary>
        /// <value>the decedent's ethnicity. A Dictionary representing a code, containing the following key/value pairs:
        /// <para>"code" - the code</para>
        /// <para>"system" - the code system this code belongs to</para>
        /// <para>"display" - a human readable meaning of the code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; ethnicity = new Dictionary&lt;string, string&gt;();</para>
        /// <para>ethnicity.Add("code", "Y");</para>
        /// <para>ethnicity.Add("system", CodeSystems.YesNo);</para>
        /// <para>ethnicity.Add("display", "Yes");</para>
        /// <para>ExampleDeathRecord.Ethnicity2 = ethnicity;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Ethnicity: {ExampleDeathRecord.Ethnicity2['display']}");</para>
        /// </example>
        [Property("Ethnicity2", Property.Types.Dictionary, "Decedent Demographics", "Decedent's Ethnicity Hispanic Puerto Rican.", true, IGURL.InputRaceAndEthnicity, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='inputraceandethnicity')", "")]
        public Dictionary<string, string> Ethnicity2
        {
            get
            {
                if (InputRaceAndEthnicityObs != null)
                {
                    Observation.ComponentComponent ethnicity = InputRaceAndEthnicityObs.Component.FirstOrDefault(c => c.Code.Coding[0].Code == NvssEthnicity.PuertoRican);
                    if (ethnicity != null && ethnicity.Value != null && ethnicity.Value as CodeableConcept != null)
                    {
                        return CodeableConceptToDict((CodeableConcept)ethnicity.Value);
                    }
                }
                return EmptyCodeableDict();
            }
            set
            {
                if (InputRaceAndEthnicityObs == null)
                {
                    CreateInputRaceEthnicityObs();
                }
                InputRaceAndEthnicityObs.Component.RemoveAll(c => c.Code.Coding[0].Code == NvssEthnicity.PuertoRican);
                Observation.ComponentComponent component = new Observation.ComponentComponent();
                component.Code = new CodeableConcept(CodeSystems.ComponentCode, NvssEthnicity.PuertoRican, NvssEthnicity.PuertoRicanDisplay, null);
                component.Value = DictToCodeableConcept(value);
                InputRaceAndEthnicityObs.Component.Add(component);
            }
        }

        /// <summary>Decedent's Ethnicity 2 Helper</summary>
        /// <value>Decedent's Ethnicity 2.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.Ethnicity2Helper = "Y";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Decedent's Ethnicity: {ExampleDeathRecord.Ethnicity1Helper}");</para>
        /// </example>
        [Property("Ethnicity 2 Helper", Property.Types.String, "Decedent Demographics", "Decedent's Ethnicity 2.", false, IGURL.InputRaceAndEthnicity, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='inputraceandethnicity')", "")]
        public string Ethnicity2Helper
        {
            get
            {
                if (Ethnicity2.ContainsKey("code") && !String.IsNullOrWhiteSpace(Ethnicity2["code"]))
                {
                    return Ethnicity2["code"];
                }
                return null;
            }
            set
            {
                if(!String.IsNullOrWhiteSpace(value))
                {
                    SetCodeValue("Ethnicity2", value, VRDR.ValueSets.HispanicNoUnknown.Codes);
                }
            }
        }

        /// <summary>Decedent's Ethnicity Hispanic Cuban.</summary>
        /// <value>the decedent's ethnicity. A Dictionary representing a code, containing the following key/value pairs:
        /// <para>"code" - the code</para>
        /// <para>"system" - the code system this code belongs to</para>
        /// <para>"display" - a human readable meaning of the code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; ethnicity = new Dictionary&lt;string, string&gt;();</para>
        /// <para>ethnicity.Add("code", "Y");</para>
        /// <para>ethnicity.Add("system", CodeSystems.YesNo);</para>
        /// <para>ethnicity.Add("display", "Yes");</para>
        /// <para>ExampleDeathRecord.Ethnicity3 = ethnicity;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Ethnicity: {ExampleDeathRecord.Ethnicity3['display']}");</para>
        /// </example>
        [Property("Ethnicity3", Property.Types.Dictionary, "Decedent Demographics", "Decedent's Ethnicity Hispanic Cuban.", true, IGURL.InputRaceAndEthnicity, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='inputraceandethnicity')", "")]
        public Dictionary<string, string> Ethnicity3
        {
            get
            {
                if (InputRaceAndEthnicityObs != null)
                {
                    Observation.ComponentComponent ethnicity = InputRaceAndEthnicityObs.Component.FirstOrDefault(c => c.Code.Coding[0].Code == NvssEthnicity.Cuban);
                    if (ethnicity != null && ethnicity.Value != null && ethnicity.Value as CodeableConcept != null)
                    {
                        return CodeableConceptToDict((CodeableConcept)ethnicity.Value);
                    }
                }
                return EmptyCodeableDict();
            }
            set
            {
                if (InputRaceAndEthnicityObs == null)
                {
                    CreateInputRaceEthnicityObs();
                }
                InputRaceAndEthnicityObs.Component.RemoveAll(c => c.Code.Coding[0].Code == NvssEthnicity.Cuban);
                Observation.ComponentComponent component = new Observation.ComponentComponent();
                component.Code = new CodeableConcept(CodeSystems.ComponentCode, NvssEthnicity.Cuban, NvssEthnicity.CubanDisplay, null);
                component.Value = DictToCodeableConcept(value);
                InputRaceAndEthnicityObs.Component.Add(component);
            }
        }

        /// <summary>Decedent's Ethnicity 3 Helper</summary>
        /// <value>Decedent's Ethnicity 3.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.Ethnicity3Helper = "Y";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Decedent's Ethnicity: {ExampleDeathRecord.Ethnicity3Helper}");</para>
        /// </example>
        [Property("Ethnicity 3 Helper", Property.Types.String, "Decedent Demographics", "Decedent's Ethnicity 3.", false, IGURL.InputRaceAndEthnicity, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='inputraceandethnicity')", "")]
        public string Ethnicity3Helper
        {
            get
            {
                if (Ethnicity3.ContainsKey("code") && !String.IsNullOrWhiteSpace(Ethnicity3["code"]))
                {
                    return Ethnicity3["code"];
                }
                return null;
            }
            set
            {
                if(!String.IsNullOrWhiteSpace(value))
                {
                    SetCodeValue("Ethnicity3", value, VRDR.ValueSets.HispanicNoUnknown.Codes);
                }
            }
        }

        /// <summary>Decedent's Ethnicity Hispanic Other.</summary>
        /// <value>the decedent's ethnicity. A Dictionary representing a code, containing the following key/value pairs:
        /// <para>"code" - the code</para>
        /// <para>"system" - the code system this code belongs to</para>
        /// <para>"display" - a human readable meaning of the code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; ethnicity = new Dictionary&lt;string, string&gt;();</para>
        /// <para>ethnicity.Add("code", "Y");</para>
        /// <para>ethnicity.Add("system", CodeSystems.YesNo);</para>
        /// <para>ethnicity.Add("display", "Yes");</para>
        /// <para>ExampleDeathRecord.Ethnicity4 = ethnicity;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Ethnicity: {ExampleDeathRecord.Ethnicity4['display']}");</para>
        /// </example>
        [Property("Ethnicity4", Property.Types.Dictionary, "Decedent Demographics", "Decedent's Ethnicity Hispanic Other.", true, IGURL.InputRaceAndEthnicity, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='inputraceandethnicity')", "")]
        public Dictionary<string, string> Ethnicity4
        {
            get
            {
                if (InputRaceAndEthnicityObs != null)
                {
                    Observation.ComponentComponent ethnicity = InputRaceAndEthnicityObs.Component.FirstOrDefault(c => c.Code.Coding[0].Code == NvssEthnicity.Other);
                    if (ethnicity != null && ethnicity.Value != null && ethnicity.Value as CodeableConcept != null)
                    {
                        return CodeableConceptToDict((CodeableConcept)ethnicity.Value);
                    }
                }
                return EmptyCodeableDict();
            }
            set
            {
                if (InputRaceAndEthnicityObs == null)
                {
                    CreateInputRaceEthnicityObs();
                }
                InputRaceAndEthnicityObs.Component.RemoveAll(c => c.Code.Coding[0].Code == NvssEthnicity.Other); ;
                Observation.ComponentComponent component = new Observation.ComponentComponent();
                component.Code = new CodeableConcept(CodeSystems.ComponentCode, NvssEthnicity.Other, NvssEthnicity.OtherDisplay, null);
                component.Value = DictToCodeableConcept(value);
                InputRaceAndEthnicityObs.Component.Add(component);
            }
        }

        /// <summary>Decedent's Ethnicity 4 Helper</summary>
        /// <value>Decedent's Ethnicity 4.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.Ethnicity4Helper = "Y";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Decedent's Ethnicity: {ExampleDeathRecord.Ethnicity4Helper}");</para>
        /// </example>
        [Property("Ethnicity 4 Helper", Property.Types.String, "Decedent Demographics", "Decedent's Ethnicity 4.", false, IGURL.InputRaceAndEthnicity, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='inputraceandethnicity')", "")]
        public string Ethnicity4Helper
        {
            get
            {
                if (Ethnicity4.ContainsKey("code") && !String.IsNullOrWhiteSpace(Ethnicity4["code"]))
                {
                    return Ethnicity4["code"];
                }
                return null;
            }
            set
            {
                if(!String.IsNullOrWhiteSpace(value))
                {
                    SetCodeValue("Ethnicity4", value, VRDR.ValueSets.HispanicNoUnknown.Codes);
                }
            }
        }

        /// <summary>Decedent's Ethnicity Hispanic Literal.</summary>
        /// <value>the decedent's ethnicity. A Dictionary representing a code, containing the following key/value pairs:
        /// <para>"code" - the code</para>
        /// <para>"system" - the code system this code belongs to</para>
        /// <para>"display" - a human readable meaning of the code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.EthnicityLiteral = ethnicity;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Ethnicity: {ExampleDeathRecord.Ethnicity4['display']}");</para>
        /// </example>
        [Property("EthnicityLiteral", Property.Types.String, "Decedent Demographics", "Decedent's Ethnicity Literal.", true, IGURL.InputRaceAndEthnicity, false, 34)]
        [PropertyParam("ethnicity", "The literal string to describe ethnicity.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='inputraceandethnicity')", "")]
        public string EthnicityLiteral
        {
            get
            {
                if (InputRaceAndEthnicityObs != null)
                {
                    Observation.ComponentComponent ethnicity = InputRaceAndEthnicityObs.Component.FirstOrDefault(c => c.Code.Coding[0].Code == NvssEthnicity.Literal);
                    if (ethnicity != null && ethnicity.Value != null && ethnicity.Value as FhirString != null)
                    {
                        return ethnicity.Value.ToString();
                    }
                }
                return null;
            }
            set
            {
                if (InputRaceAndEthnicityObs == null)
                {
                    CreateInputRaceEthnicityObs();
                }
                InputRaceAndEthnicityObs.Component.RemoveAll(c => c.Code.Coding[0].Code == NvssEthnicity.Literal);
                if (String.IsNullOrWhiteSpace(value)) {
                    return;
                }
                Observation.ComponentComponent component = new Observation.ComponentComponent();
                component.Code = new CodeableConcept(CodeSystems.ComponentCode, NvssEthnicity.Literal, NvssEthnicity.LiteralDisplay, null);
                component.Value = new FhirString(value);
                InputRaceAndEthnicityObs.Component.Add(component);
            }
        }

        /// <summary>Decedent's Race values.</summary>
        /// <value>the decedent's race. A tuple, where the first value of the tuple is the display value, and the second is
        /// the IJE code Y or N.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.Race = {NvssRace.BlackOrAfricanAmerican, "Y"};</para>
        /// <para>// Getter:</para>
        /// <para>string boaa = ExampleDeathRecord.RaceBlackOfAfricanAmerican;</para>
        /// </example>
        [Property("Race", Property.Types.TupleArr, "Decedent Demographics", "Decedent's Race", true, IGURL.InputRaceAndEthnicity, true, 38)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='inputraceandethnicity')", "")]
        public Tuple<string, string>[] Race
        {
            get
            {
                // filter the boolean race values
                var booleanRaceCodes = NvssRace.GetBooleanRaceCodes();
                List<string> raceCodes = booleanRaceCodes.Concat(NvssRace.GetLiteralRaceCodes()).ToList();

                var races = new List<Tuple<string, string>>() { };

                if (InputRaceAndEthnicityObs == null)
                {
                    return races.ToArray();
                }
                foreach (string raceCode in raceCodes)
                {
                    Observation.ComponentComponent component = InputRaceAndEthnicityObs.Component.Where(c => c.Code.Coding[0].Code == raceCode).FirstOrDefault();
                    if (component != null)
                    {
                        // convert boolean race codes to strings
                        if (booleanRaceCodes.Contains(raceCode))
                        {
                            if (component.Value == null) {
                              // If there is no value given, set the race to blank.
                              var race = Tuple.Create(raceCode, "");
                              races.Add(race);
                              continue;
                            }

                            // Todo Find conversion from FhirBoolean to bool
                            string raceBool = ((FhirBoolean)component.Value).ToString();

                            if (Convert.ToBoolean(raceBool))
                            {
                                var race = Tuple.Create(raceCode, "Y");
                                races.Add(race);
                            }
                            else
                            {
                                var race = Tuple.Create(raceCode, "N");
                                races.Add(race);
                            }
                        }
                        else
                        {
                            // Ignore unless there's a value present
                            if (component.Value != null)
                            {
                                var race = Tuple.Create(raceCode, component.Value.ToString());
                                races.Add(race);
                            }
                        }

                    }
                }

                return races.ToArray();
            }
            set
            {
                if (InputRaceAndEthnicityObs == null)
                {
                    CreateInputRaceEthnicityObs();
                }
                var booleanRaceCodes = NvssRace.GetBooleanRaceCodes();
                var literalRaceCodes = NvssRace.GetLiteralRaceCodes();
                foreach (Tuple<string, string> element in value)
                {
                    InputRaceAndEthnicityObs.Component.RemoveAll(c => c.Code.Coding[0].Code == element.Item1);
                    Observation.ComponentComponent component = new Observation.ComponentComponent();
                    String displayValue = NvssRace.GetDisplayValueForCode(element.Item1);
                    component.Code = new CodeableConcept(CodeSystems.ComponentCode, element.Item1, displayValue, null);
                    if (booleanRaceCodes.Contains(element.Item1))
                    {
                        if (element.Item2 == "Y")
                        {
                            component.Value = new FhirBoolean(true);
                        }
                        else
                        {
                            component.Value = new FhirBoolean(false);
                        }
                    }
                    else if (literalRaceCodes.Contains(element.Item1))
                    {
                        component.Value = new FhirString(element.Item2);
                    }
                    else
                    {
                        throw new ArgumentException("Invalid race literal code found: " + element.Item1 + " with value: " + element.Item2);
                    }
                    InputRaceAndEthnicityObs.Component.Add(component);
                }

            }
        }

        /// <summary>Decedent's Race MissingValueReason.</summary>
        /// <value>why the decedent's race is missing. A Dictionary representing a code, containing the following key/value pairs:
        /// <para>"code" - the code</para>
        /// <para>"system" - the code system this code belongs to</para>
        /// <para>"display" - a human readable meaning of the code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; mvr = new Dictionary&lt;string, string&gt;();</para>
        /// <para>mvr.Add("code", "R");</para>
        /// <para>mvr.Add("system", CodeSystems.MissingValueReason);</para>
        /// <para>mvr.Add("display", "Refused");</para>
        /// <para>ExampleDeathRecord.RaceMissingValueReason = mvr;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Missing Race: {ExampleDeathRecord.RaceMissingValueReason['display']}");</para>
        /// </example>
        [Property("RaceMissingValueReason", Property.Types.Dictionary, "Decedent Demographics", "Decedent's Race MissingValueReason.", true, IGURL.InputRaceAndEthnicity, true, 38)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='MissingValueReason')", "")]
        public Dictionary<string, string> RaceMissingValueReason
        {
            get
            {
                if (InputRaceAndEthnicityObs != null)
                {
                    Observation.ComponentComponent raceMVR = InputRaceAndEthnicityObs.Component.FirstOrDefault(c => c.Code.Coding[0].Code == NvssRace.MissingValueReason);
                    if (raceMVR != null && raceMVR.Value != null && raceMVR.Value as CodeableConcept != null)
                    {
                        return CodeableConceptToDict((CodeableConcept)raceMVR.Value);
                    }
                }
                return EmptyCodeableDict();
            }
            set
            {
                if (InputRaceAndEthnicityObs == null)
                {
                    CreateInputRaceEthnicityObs();
                }
                InputRaceAndEthnicityObs.Component.RemoveAll(c => c.Code.Coding[0].Code == NvssRace.MissingValueReason);
                Observation.ComponentComponent component = new Observation.ComponentComponent();
                component.Code = new CodeableConcept(CodeSystems.ComponentCode, NvssRace.MissingValueReason, NvssRace.MissingValueReason, null);
                component.Value = DictToCodeableConcept(value);
                InputRaceAndEthnicityObs.Component.Add(component);
            }
        }


        /// <summary>Decedent's RaceMissingValueReason</summary>
        /// <value>Decedent's RaceMissingValueReason.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.RaceMissingValueReasonHelper = VRDR.ValueSets.RaceMissingValueReason.R;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Decedent's RaceMissingValueReason: {ExampleDeathRecord.RaceMissingValueReasonHelper}");</para>
        /// </example>
        [Property("RaceMissingValueReasonHelper", Property.Types.String, "Decedent Demographics", "Decedent's Race MissingValueReason.", false, IGURL.InputRaceAndEthnicity, true, 38)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='MissingValueReason')", "")]
        public string RaceMissingValueReasonHelper
        {
            get
            {
                if (RaceMissingValueReason.ContainsKey("code") && !String.IsNullOrWhiteSpace(RaceMissingValueReason["code"]))
                {
                    return RaceMissingValueReason["code"];
                }
                return null;
            }
            set
            {
                if(!String.IsNullOrWhiteSpace(value))
                {
                    SetCodeValue("RaceMissingValueReason", value, VRDR.ValueSets.RaceMissingValueReason.Codes);
                }
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
        /// <para>address.Add("addressState", "MA");</para>
        /// <para>address.Add("addressZip", "12345");</para>
        /// <para>address.Add("addressCountry", "US");</para>
        /// <para>SetterDeathRecord.PlaceOfBirth = address;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"State where decedent was born: {ExampleDeathRecord.PlaceOfBirth["placeOfBirthState"]}");</para>
        /// </example>
        [Property("Place Of Birth", Property.Types.Dictionary, "Decedent Demographics", "Decedent's Place Of Birth.", true, IGURL.Decedent, true, 15)]
        [PropertyParam("addressLine1", "address, line one")]
        [PropertyParam("addressLine2", "address, line two")]
        [PropertyParam("addressCity", "address, city")]
        [PropertyParam("addressCounty", "address, county")]
        [PropertyParam("addressState", "address, state")]
        [PropertyParam("addressZip", "address, zip")]
        [PropertyParam("addressCountry", "address, country")]
        [FHIRPath("Bundle.entry.resource.where($this is Patient).extension.where(url='" + OtherExtensionURL.PatientBirthPlace + "')", "")]
        public Dictionary<string, string> PlaceOfBirth
        {
            get
            {
                if (Decedent != null)
                {
                    Extension addressExt = Decedent.Extension.FirstOrDefault(extension => extension.Url == OtherExtensionURL.PatientBirthPlace);
                    if (addressExt != null)
                    {
                        Address address = (Address)addressExt.Value;
                        if (address != null)
                        {
                            return AddressToDict((Address)address);
                        }
                        return EmptyAddrDict();
                    }
                }
                return EmptyAddrDict();
            }
            set
            {
                Decedent.Extension.RemoveAll(ext => ext.Url == OtherExtensionURL.PatientBirthPlace);
                if (!IsDictEmptyOrDefault(value))
                {
                    Extension placeOfBirthExt = new Extension();
                    placeOfBirthExt.Url = OtherExtensionURL.PatientBirthPlace;
                    placeOfBirthExt.Value = DictToAddress(value);
                    Decedent.Extension.Add(placeOfBirthExt);
                }
            }
        }

        /// <summary>The informant of the decedent's death.</summary>
        /// <value>String representation of the informant's relationship to the decedent
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; relationship = new Dictionary&lt;string, string&gt;();</para>
        /// <para>relationship.Add("text", "sibling");</para>
        /// <para>SetterDeathRecord.ContactRelationship = relationship;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Contact's Relationship: {ExampleDeathRecord.ContactRelationship["text"]}");</para>
        /// </example>
        [Property("Contact Relationship", Property.Types.Dictionary, "Decedent Demographics", "The informant's relationship to the decedent", true, IGURL.Decedent, true, 24)]
        [PropertyParam("relationship", "The relationship to the decedent.")]
        [FHIRPath("Bundle.entry.resource.where($this is Patient)", "contact")]
        public Dictionary<string, string> ContactRelationship
        {
            get
            {
                if (Decedent != null && Decedent.Contact != null)
                {
                    var contact = Decedent.Contact.FirstOrDefault();
                    if (contact != null && contact.Relationship != null)
                    {
                        return CodeableConceptToDict(contact.Relationship.FirstOrDefault());
                    }
                }
                return EmptyCodeableDict();
            }
            set
            {
                Patient.ContactComponent component = new Patient.ContactComponent();
                component.Relationship.Add(DictToCodeableConcept(value));
                Decedent.Contact.Add(component);
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
        /// <para>code.Add("system", "http://terminology.hl7.org/CodeSystem/v3-MaritalStatus");</para>
        /// <para>code.Add("display", "Never Married");</para>
        /// <para>ExampleDeathRecord.MaritalStatus = code;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Marital status: {ExampleDeathRecord.MaritalStatus["display"]}");</para>
        /// </example>
        [Property("Marital Status", Property.Types.Dictionary, "Decedent Demographics", "The marital status of the decedent at the time of death.", true, IGURL.Decedent, true, 24)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Patient)", "maritalStatus")]
        public Dictionary<string, string> MaritalStatus
        {
            get
            {
                if (Decedent != null && Decedent.MaritalStatus != null && Decedent.MaritalStatus as CodeableConcept != null)
                {
                    return CodeableConceptToDict((CodeableConcept)Decedent.MaritalStatus);
                }
                return EmptyCodeableDict();
            }
            set
            {
                if (Decedent.MaritalStatus == null)
                {
                    Decedent.MaritalStatus = DictToCodeableConcept(value);
                }
                else
                {
                    // Need to keep any existing text or extension that could be there
                    string text = Decedent.MaritalStatus.Text;
                    List<Extension> extensions = Decedent.MaritalStatus.Extension.FindAll(e => true);
                    Decedent.MaritalStatus = DictToCodeableConcept(value);
                    Decedent.MaritalStatus.Extension.AddRange(extensions);
                    Decedent.MaritalStatus.Text = text;
                }
            }
        }

        /// <summary>The marital status edit flag.</summary>
        /// <value>the marital status edit flag
        /// <para>"code" - the code describing this finding</para>
        /// <para>"system" - the system the given code belongs to</para>
        /// <para>"display" - the human readable display text that corresponds to the given code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; code = new Dictionary&lt;string, string&gt;();</para>
        /// <para>code.Add("code", "S");</para>
        /// <para>code.Add("system", "http://terminology.hl7.org/CodeSystem/v3-MaritalStatus");</para>
        /// <para>code.Add("display", "Never Married");</para>
        /// <para>ExampleDeathRecord.MaritalStatus = code;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Marital status: {ExampleDeathRecord.MaritalStatus["display"]}");</para>
        /// </example>
        [Property("Marital Status Edit Flag", Property.Types.Dictionary, "Decedent Demographics", "The marital status edit flag.", true, IGURL.Decedent, true, 24)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Patient)", "maritalStatus")]
        public Dictionary<string, string> MaritalStatusEditFlag
        {
            get
            {
                if (Decedent != null && Decedent.MaritalStatus != null && Decedent.MaritalStatus.Extension.FirstOrDefault() != null)
                {
                    Extension addressExt = Decedent.MaritalStatus.Extension.FirstOrDefault(extension => extension.Url == ExtensionURL.BypassEditFlag);
                    if (addressExt != null && addressExt.Value != null && addressExt.Value as CodeableConcept != null)
                    {
                        return CodeableConceptToDict((CodeableConcept)addressExt.Value);
                    }
                }
                return EmptyCodeableDict();
            }
            set
            {
                if (Decedent.MaritalStatus == null)
                {
                    Decedent.MaritalStatus = new CodeableConcept();
                }
                Decedent.MaritalStatus.Extension.RemoveAll(ext => ext.Url == ExtensionURL.BypassEditFlag);
                Decedent.MaritalStatus.Extension.Add(new Extension(ExtensionURL.BypassEditFlag, DictToCodeableConcept(value)));
            }
        }

        /// <summary>The marital status of the decedent at the time of death helper method.</summary>
        /// <value>the marital status of the decedent at the time of death.:
        /// <para>"code" - the code describing this finding</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.MaritalStatusHelper = ValueSets.MaritalStatus.NeverMarried;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Marital status: {ExampleDeathRecord.MaritalStatusHelper}");</para>
        /// </example>
        [Property("Marital Status Helper", Property.Types.String, "Decedent Demographics", "The marital status of the decedent at the time of death.", false, IGURL.Decedent, true, 24)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [FHIRPath("Bundle.entry.resource.where($this is Patient)", "maritalStatus")]
        public string MaritalStatusHelper
        {
            get
            {
                if (MaritalStatus.ContainsKey("code") && !String.IsNullOrWhiteSpace(MaritalStatus["code"]))
                {
                    return MaritalStatus["code"];
                }
                return null;
            }
            set
            {
                if(!String.IsNullOrWhiteSpace(value))
                {
                    SetCodeValue("MaritalStatus", value, VRDR.ValueSets.MaritalStatus.Codes);
                }
            }
        }

        /// <summary>The marital status edit flag helper method.</summary>
        /// <value>the marital status edit flag value.:
        /// <para>"code" - the code describing this finding</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.MaritalStatusEditFlagHelper = ValueSets.EditBypass0124.0;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Marital status: {ExampleDeathRecord.MaritalStatusEditFlagHelper}");</para>
        /// </example>
        [Property("Marital Status Edit Flag Helper", Property.Types.String, "Decedent Demographics", "Marital Status Edit Flag Helper", false, IGURL.Decedent, true, 24)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [FHIRPath("Bundle.entry.resource.where($this is Patient)", "maritalStatus")]
        public string MaritalStatusEditFlagHelper
        {
            get
            {
                if (MaritalStatusEditFlag.ContainsKey("code") && !String.IsNullOrWhiteSpace(MaritalStatusEditFlag["code"]))
                {
                    return MaritalStatusEditFlag["code"];
                }
                return null;
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    SetCodeValue("MaritalStatusEditFlag", value, VRDR.ValueSets.EditBypass0124.Codes);
                }
            }
        }

        /// <summary>The literal text string of the marital status of the decedent at the time of death.</summary>
        /// <value>the marital status of the decedent at the time of death. A Dictionary representing a code, containing the following key/value pairs:
        /// <para>"text" - the code describing this finding</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.MaritalStatusLiteral = "Single";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Marital status: {ExampleDeathRecord.MaritalStatusLiteral}");</para>
        /// </example>
        [Property("Marital Status Literal", Property.Types.String, "Decedent Demographics", "The marital status of the decedent at the time of death.", true, IGURL.Decedent, true, 24)]
        [PropertyParam("text", "The literal string")]
        [FHIRPath("Bundle.entry.resource.where($this is Patient)", "maritalStatus")]
        public string MaritalStatusLiteral
        {
            get
            {
                if (Decedent != null && Decedent.MaritalStatus != null && Decedent.MaritalStatus.Text != null)
                {
                    return Decedent.MaritalStatus.Text;
                }
                return null;
            }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    return;
                }
                if (Decedent.MaritalStatus == null)
                {
                    Decedent.MaritalStatus = new CodeableConcept();
                }
                Decedent.MaritalStatus.Text = value;
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
        [Property("Father Given Names", Property.Types.StringArr, "Decedent Demographics", "Given name(s) of decedent's father.", true, IGURL.DecedentFather, false, 28)]
        [FHIRPath("Bundle.entry.resource.where($this is RelatedPerson).where(relationship.coding.code='FTH')", "name")]
        public string[] FatherGivenNames
        {
            get
            {
                if (Father != null && Father.Name != null)
                {
                    // Evaluation of method System.Linq.Enumerable.SingleOrDefault requires calling method System.Reflection.TypeInfo.get_DeclaredFields, which cannot be called in this context.
                    //HumanName name = Father.Name.SingleOrDefault(n => n.Use == HumanName.NameUse.Official);
                    string[] names = GetAllString("Bundle.entry.resource.where($this is RelatedPerson).where(relationship.coding.code='FTH').name.where(use='official').given");
                    return names != null ? names : new string[0];
                }
                return new string[0];
            }
            set
            {
                if (Father == null)
                {
                    CreateFather();
                }
                updateGivenHumanName(value, Father.Name);
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
        [Property("Father Family Name", Property.Types.String, "Decedent Demographics", "Family name of decedent's father.", true, IGURL.DecedentFather, false, 29)]
        [FHIRPath("Bundle.entry.resource.where($this is RelatedPerson).where(relationship.coding.code='FTH')", "name")]
        public string FatherFamilyName
        {
            get
            {
                if (Father != null && Father.Name != null)
                {
                    return GetFirstString("Bundle.entry.resource.where($this is RelatedPerson).where(relationship.coding.code='FTH').name.where(use='official').family");
                }
                return null;
            }
            set
            {
                if (Father == null)
                {
                    CreateFather();
                }
                HumanName name = Father.Name.SingleOrDefault(n => n.Use == HumanName.NameUse.Official);
                if (name != null && !String.IsNullOrEmpty(value))
                {
                    name.Family = value;
                }
                else if (!String.IsNullOrEmpty(value))
                {
                    name = new HumanName();
                    name.Use = HumanName.NameUse.Official;
                    name.Family = value;
                    Father.Name.Add(name);
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
        [Property("Father Suffix", Property.Types.String, "Decedent Demographics", "Father's Suffix.", true, IGURL.DecedentFather, false, 30)]
        [FHIRPath("Bundle.entry.resource.where($this is RelatedPerson).where(relationship.coding.code='FTH')", "name")]
        public string FatherSuffix
        {
            get
            {
                return GetFirstString("Bundle.entry.resource.where($this is RelatedPerson).where(relationship.coding.code='FTH').name.where(use='official').suffix");
            }
            set
            {
                if (Father == null)
                {
                    CreateFather();
                }
                HumanName name = Father.Name.SingleOrDefault(n => n.Use == HumanName.NameUse.Official);
                string[] suffix = { value };
                if (name != null && !String.IsNullOrEmpty(value))
                {
                    name.Suffix = suffix;
                }
                else if (!String.IsNullOrEmpty(value))
                {
                    name = new HumanName();
                    name.Use = HumanName.NameUse.Official;
                    name.Suffix = suffix;
                    Father.Name.Add(name);
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
        [Property("Mother Given Names", Property.Types.StringArr, "Decedent Demographics", "Given name(s) of decedent's mother.", true, IGURL.DecedentMother, false, 31)]
        [FHIRPath("Bundle.entry.resource.where($this is RelatedPerson).where(relationship.coding.code='MTH')", "name")]
        public string[] MotherGivenNames
        {
            get
            {
                if (Mother != null && Mother.Name != null)
                {
                    // Evaluation of method System.Linq.Enumerable.SingleOrDefault requires calling method System.Reflection.TypeInfo.get_DeclaredFields, which cannot be called in this context.
                    //HumanName name = Mother.Name.SingleOrDefault(n => n.Use == HumanName.NameUse.Official);
                    string[] names = GetAllString("Bundle.entry.resource.where($this is RelatedPerson).where(relationship.coding.code='MTH').name.where(use='official').given");
                    return names != null ? names : new string[0];
                }
                return new string[0];
            }
            set
            {
                if (Mother == null)
                {
                    CreateMother();
                }
                updateGivenHumanName(value, Mother.Name);
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
        [Property("Mother Maiden Name", Property.Types.String, "Decedent Demographics", "Maiden name of decedent's mother.", true, IGURL.DecedentMother, false, 32)]
        [FHIRPath("Bundle.entry.resource.where($this is RelatedPerson).where(relationship.coding.code='MTH')", "name")]
        public string MotherMaidenName
        {
            get
            {

                if (Mother != null && Mother.Name != null)
                {
                    return GetFirstString("Bundle.entry.resource.where($this is RelatedPerson).where(relationship.coding.code='MTH').name.where(use='maiden').family");
                }
                return null;
            }
            set
            {
                if (Mother == null)
                {
                    CreateMother();
                }
                HumanName name = Mother.Name.SingleOrDefault(n => n.Use == HumanName.NameUse.Maiden);
                if (name != null && !String.IsNullOrEmpty(value))
                {
                    name.Family = value;
                }
                else if (!String.IsNullOrEmpty(value))
                {
                    name = new HumanName();
                    name.Use = HumanName.NameUse.Maiden;
                    name.Family = value;
                    Mother.Name.Add(name);
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
        [Property("Mother Suffix", Property.Types.String, "Decedent Demographics", "Mother's Suffix.", true, IGURL.DecedentMother, false, 33)]
        [FHIRPath("Bundle.entry.resource.where($this is RelatedPerson).where(relationship.coding.code='MTH')", "name")]
        public string MotherSuffix
        {
            get
            {
                return GetFirstString("Bundle.entry.resource.where($this is RelatedPerson).where(relationship.coding.code='MTH').name.where(use='official').suffix");
            }
            set
            {
                if (Mother == null)
                {
                    CreateMother();
                }
                HumanName name = Mother.Name.SingleOrDefault(n => n.Use == HumanName.NameUse.Official);
                string[] suffix = { value };
                if (name != null && !String.IsNullOrEmpty(value))
                {
                    name.Suffix = suffix;
                }
                else if (!String.IsNullOrEmpty(value))
                {
                    name = new HumanName();
                    name.Use = HumanName.NameUse.Official;
                    name.Suffix = suffix;
                    Mother.Name.Add(name);
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
        [Property("Spouse Given Names", Property.Types.StringArr, "Decedent Demographics", "Given name(s) of decedent's spouse.", true, IGURL.DecedentSpouse, false, 25)]
        [FHIRPath("Bundle.entry.resource.where($this is RelatedPerson).where(relationship.coding.code='SPS')", "name")]
        public string[] SpouseGivenNames
        {
            get
            {
                if (Spouse != null && Spouse.Name != null)
                {
                    // Evaluation of method System.Linq.Enumerable.SingleOrDefault requires calling method System.Reflection.TypeInfo.get_DeclaredFields, which cannot be called in this context.
                    //HumanName name = Spouse.Name.SingleOrDefault(n => n.Use == HumanName.NameUse.Official);
                    string[] names = GetAllString("Bundle.entry.resource.where($this is RelatedPerson).where(relationship.coding.code='SPS').name.where(use='official').given");
                    return names != null ? names : new string[0];
                }
                return new string[0];
            }
            set
            {
                if (Spouse == null)
                {
                    CreateSpouse();
                }
                updateGivenHumanName(value, Spouse.Name);
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
        [Property("Spouse Family Name", Property.Types.String, "Decedent Demographics", "Family name of decedent's spouse.", true, IGURL.DecedentSpouse, false, 26)]
        [FHIRPath("Bundle.entry.resource.where($this is RelatedPerson).where(relationship.coding.code='SPS')", "name")]
        public string SpouseFamilyName
        {
            get
            {
                if (Spouse != null && Spouse.Name != null)
                {
                    return GetFirstString("Bundle.entry.resource.where($this is RelatedPerson).where(relationship.coding.code='SPS').name.where(use='official').family");
                }
                return null;
            }
            set
            {
                if (Spouse == null)
                {
                    CreateSpouse();
                }
                HumanName name = Spouse.Name.SingleOrDefault(n => n.Use == HumanName.NameUse.Official);
                if (name != null && !String.IsNullOrEmpty(value))
                {
                    name.Family = value;
                }
                else if (!String.IsNullOrEmpty(value))
                {
                    name = new HumanName();
                    name.Use = HumanName.NameUse.Official;
                    name.Family = value;
                    Spouse.Name.Add(name);
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
        [Property("Spouse Suffix", Property.Types.String, "Decedent Demographics", "Spouse's Suffix.", true, IGURL.DecedentSpouse, false, 27)]
        [FHIRPath("Bundle.entry.resource.where($this is RelatedPerson).where(relationship.coding.code='SPS')", "name")]
        public string SpouseSuffix
        {
            get
            {
                return GetFirstString("Bundle.entry.resource.where($this is RelatedPerson).where(relationship.coding.code='SPS').name.where(use='official').suffix");
            }
            set
            {
                if (Spouse == null)
                {
                    CreateSpouse();
                }
                HumanName name = Spouse.Name.SingleOrDefault(n => n.Use == HumanName.NameUse.Official);
                string[] suffix = { value };
                if (name != null && !String.IsNullOrEmpty(value))
                {
                    name.Suffix = suffix;
                }
                else if (!String.IsNullOrEmpty(value))
                {
                    name = new HumanName();
                    name.Use = HumanName.NameUse.Official;
                    name.Suffix = suffix;
                    Spouse.Name.Add(name);
                }

            }
        }

        /// <summary>Spouse's Maiden Name.</summary>
        /// <value>the decedent's spouse's maiden name</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.SpouseSuffix = "Jr.";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Spouse Suffix: {ExampleDeathRecord.SpouseSuffix}");</para>
        /// </example>
        [Property("Spouse Maiden Name", Property.Types.String, "Decedent Demographics", "Spouse's Maiden Name.", true, IGURL.DecedentSpouse, false, 27)]
        [FHIRPath("Bundle.entry.resource.where($this is RelatedPerson).where(relationship.coding.code='SPS').name.where(use='maiden')", "family")]
        public string SpouseMaidenName
        {
            get
            {

                if (Spouse != null && Spouse.Name != null)
                {
                    return GetFirstString("Bundle.entry.resource.where($this is RelatedPerson).where(relationship.coding.code='SPS').name.where(use='maiden').family");
                }
                return null;
            }
            set
            {
                if (Spouse == null)
                {
                    CreateSpouse();
                }
                HumanName name = Spouse.Name.SingleOrDefault(n => n.Use == HumanName.NameUse.Maiden);
                if (name != null && !String.IsNullOrEmpty(value))
                {
                    name.Family = value;
                }
                else if (!String.IsNullOrEmpty(value))
                {
                    name = new HumanName();
                    name.Use = HumanName.NameUse.Maiden;
                    name.Family = value;
                    Spouse.Name.Add(name);
                }
            }
        }
        /// <summary>Spouse Alive.</summary>
        /// <value>whether the decedent's spouse is alive
        /// <para>"code" - the code describing this finding</para>
        /// <para>"system" - the system the given code belongs to</para>
        /// <para>"display" - the human readable display text that corresponds to the given code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; code = new Dictionary&lt;string, string&gt;();</para>
        /// <para>code.Add("code", "Y");</para>
        /// <para>code.Add("system", "http://terminology.hl7.org/CodeSystem/v2-0136");</para>
        /// <para>code.Add("display", "Yes");</para>
        /// <para>ExampleDeathRecord.SpouseAlive = code;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Marital status: {ExampleDeathRecord.SpouseAlive["display"]}");</para>
        /// </example>
        [Property("Spouse Alive", Property.Types.Dictionary, "Decedent Demographics", "Spouse Alive", true, IGURL.Decedent, false, 27)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Patient)", "")]
        public Dictionary<string, string> SpouseAlive
        {
            get
            {
                if (Decedent != null)
                {
                    Extension spouseExt = Decedent.Extension.FirstOrDefault(extension => extension.Url == ExtensionURL.SpouseAlive);
                    if (spouseExt != null && spouseExt.Value != null && spouseExt.Value as CodeableConcept != null)
                    {

                        return CodeableConceptToDict((CodeableConcept)spouseExt.Value);
                    }
                }
                return EmptyCodeableDict();
            }
            set
            {

                Extension ext = new Extension();
                ext.Url = ExtensionURL.SpouseAlive;
                ext.Value = DictToCodeableConcept(value);
                Decedent.Extension.Add(ext);
            }
        }

        /// <summary>Decedent's SpouseAlive</summary>
        /// <value>Decedent's SpouseAlive.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.SpouseAliveHelper = VRDR.ValueSets.YesNoUnknownNotApplicable.Y;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Decedent's Spouse Alive: {ExampleDeathRecord.SpouseAliveHelper}");</para>
        /// </example>
        [Property("Spouse Alive Helper", Property.Types.String, "Decedent Demographics", "Spouse Alive", false, IGURL.Decedent, false, 27)]
        [FHIRPath("Bundle.entry.resource.where($this is Patient)", "")]
        public string SpouseAliveHelper
        {
            get
            {
                if (SpouseAlive.ContainsKey("code") && !String.IsNullOrWhiteSpace(SpouseAlive["code"]))
                {
                    return SpouseAlive["code"];
                }
                return null;
            }
            set
            {
                if(!String.IsNullOrWhiteSpace(value))
                {
                    SetCodeValue("SpouseAlive", value, VRDR.ValueSets.SpouseAlive.Codes);
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
        /// <para>elevel.Add("code", "BA");</para>
        /// <para>elevel.Add("system", CodeSystems.EducationLevel);</para>
        /// <para>elevel.Add("display", "Bachelor’s Degree");</para>
        /// <para>ExampleDeathRecord.EducationLevel = elevel;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Education Level: {ExampleDeathRecord.EducationLevel['display']}");</para>
        /// </example>
        [Property("Education Level", Property.Types.Dictionary, "Decedent Demographics", "Decedent's Education Level.", true, IGURL.DecedentEducationLevel, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='80913-7').value.coding", "")]
        public Dictionary<string, string> EducationLevel
        {
            get
            {
                if (DecedentEducationLevel != null && DecedentEducationLevel.Value != null && DecedentEducationLevel.Value as CodeableConcept != null)
                {
                    return CodeableConceptToDict((CodeableConcept)DecedentEducationLevel.Value);
                }
                return EmptyCodeableDict();
            }
            set
            {
                if (IsDictEmptyOrDefault(value) && DecedentEducationLevel == null)
                {
                    return;
                }
                if (DecedentEducationLevel == null)
                {
                    CreateEducationLevelObs();
                    DecedentEducationLevel.Value = DictToCodeableConcept(value);
                }
                else
                {
                    // Need to keep any existing extension that could be there
                    List<Extension> extensions = DecedentEducationLevel.Value.Extension.FindAll(e => true);
                    DecedentEducationLevel.Value = DictToCodeableConcept(value);
                    DecedentEducationLevel.Value.Extension.AddRange(extensions);
                }
            }
        }

        /// <summary>Decedent's Education Level Helper</summary>
        /// <value>Decedent's Education Level.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.DecedentEducationLevel = VRDR.ValueSets.EducationLevel.Bachelors_Degree;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Decedent's Education Level: {ExampleDeathRecord.EducationLevelHelper}");</para>
        /// </example>
        [Property("Education Level Helper", Property.Types.String, "Decedent Demographics", "Decedent's Education Level.", false, IGURL.DecedentEducationLevel, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='80913-7').value.coding", "")]
        public string EducationLevelHelper
        {
            get
            {
                if (EducationLevel.ContainsKey("code") && !String.IsNullOrWhiteSpace(EducationLevel["code"]))
                {
                    return EducationLevel["code"];
                }
                return null;
            }
            set
            {
                if(!String.IsNullOrWhiteSpace(value))
                {
                    SetCodeValue("EducationLevel", value, VRDR.ValueSets.EducationLevel.Codes);
                }
            }
        }

        /// <summary>Decedent's Education Level Edit Flag.</summary>
        /// <value>the decedent's education level edit flag. A Dictionary representing a code, containing the following key/value pairs:
        /// <para>"code" - the code</para>
        /// <para>"system" - the code system this code belongs to</para>
        /// <para>"display" - a human readable meaning of the code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; elevel = new Dictionary&lt;string, string&gt;();</para>
        /// <para>elevel.Add("code", "0");</para>
        /// <para>elevel.Add("system", CodeSystems.BypassEditFlag);</para>
        /// <para>elevel.Add("display", "Edit Passed");</para>
        /// <para>ExampleDeathRecord.EducationLevelEditFlag = elevel;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Education Level Edit Flag: {ExampleDeathRecord.EducationLevelEditFlag['display']}");</para>
        /// </example>
        [Property("Education Level Edit Flag", Property.Types.Dictionary, "Decedent Demographics", "Decedent's Education Level Edit Flag.", true, IGURL.DecedentEducationLevel, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='80913-7')", "")]
        public Dictionary<string, string> EducationLevelEditFlag
        {
            get
            {
                Extension editFlag = DecedentEducationLevel?.Value?.Extension.FirstOrDefault(ext => ext.Url == ExtensionURL.BypassEditFlag);
                if (editFlag != null && editFlag.Value != null && editFlag.Value.GetType() == typeof(CodeableConcept))
                {
                    return CodeableConceptToDict((CodeableConcept)editFlag.Value);
                }
                return EmptyCodeableDict();
            }
            set
            {
                if (IsDictEmptyOrDefault(value) && DecedentEducationLevel == null)
                {
                    return;
                }
                if (DecedentEducationLevel == null)
                {
                    CreateEducationLevelObs();
                }
                if (DecedentEducationLevel.Value != null && DecedentEducationLevel.Value.Extension != null)
                {
                    DecedentEducationLevel.Value.Extension.RemoveAll(ext => ext.Url == ExtensionURL.BypassEditFlag);
                }
                if (DecedentEducationLevel.Value == null)
                {
                    DecedentEducationLevel.Value = new CodeableConcept();
                }
                Extension editFlag = new Extension(ExtensionURL.BypassEditFlag, DictToCodeableConcept(value));
                DecedentEducationLevel.Value.Extension.Add(editFlag);
            }
        }

        /// <summary>Decedent's Education Level Edit Flag Helper</summary>
        /// <value>Decedent's Education Level Edit Flag.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.DecedentEducationLevelEditFlag = VRDR.ValueSets.EditBypass01234.EditPassed;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Decedent's Education Level Edit Flag: {ExampleDeathRecord.EducationLevelHelperEditFlag}");</para>
        /// </example>
        [Property("Education Level Edit Flag Helper", Property.Types.String, "Decedent Demographics", "Decedent's Education Level Edit Flag Helper.", false, IGURL.DecedentEducationLevel, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='80913-7')", "")]
        public string EducationLevelEditFlagHelper
        {
            get
            {
                if (EducationLevelEditFlag.ContainsKey("code") && !String.IsNullOrWhiteSpace(EducationLevelEditFlag["code"]))
                {
                    return EducationLevelEditFlag["code"];
                }
                return null;
            }
            set
            {
                if(!String.IsNullOrWhiteSpace(value))
                {
                   SetCodeValue("EducationLevelEditFlag", value, VRDR.ValueSets.EditBypass01234.Codes);
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
        [Property("Birth Record Id", Property.Types.String, "Decedent Demographics", "Birth Record Identifier (i.e. Certificate Number).", true, IGURL.BirthRecordIdentifier, true, 16)]
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
                    CreateBirthRecordIdentifier();
                }
                if (!String.IsNullOrWhiteSpace(value))
                {
                    BirthRecordIdentifier.Value = new FhirString(value);
                }
                else
                {
                    BirthRecordIdentifier.Value = (FhirString)null;
                }
            }
        }

        /// <summary>Birth Record State.</summary>
        /// <value>the state of the decedent's birth certificate. A Dictionary representing a code, containing the following key/value pairs:
        /// <para>"code" - the code</para>
        /// <para>"system" - the code system this code belongs to</para>
        /// <para>"display" - a human readable meaning of the code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.BirthRecordState = "MA";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Birth Record identification: {ExampleDeathRecord.BirthRecordState}");</para>
        /// </example>
        [Property("Birth Record State", Property.Types.String, "Decedent Demographics", "Birth Record State.", true, IGURL.BirthRecordIdentifier, true, 17)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='BR')", "")]
        public string BirthRecordState
        {
            get
            {
                if (BirthRecordIdentifier != null && BirthRecordIdentifier.Component.Count > 0)
                {
                    // Find correct component
                    var stateComp = BirthRecordIdentifier.Component.FirstOrDefault(entry => ((Observation.ComponentComponent)entry).Code != null &&
                    ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault() != null && ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault().Code == "21842-0");
                    if (stateComp != null && stateComp.Value != null && stateComp.Value as FhirString != null)
                    {
                        return (Convert.ToString(stateComp.Value));
                    }
                }
                return null;
            }
            set
            {
                if (String.IsNullOrWhiteSpace(value)) {
                    return;
                }
                //    CodeableConcept state = DictToCodeableConcept(value);
                if (BirthRecordIdentifier == null)
                {
                    CreateBirthRecordIdentifier();
                    Observation.ComponentComponent component = new Observation.ComponentComponent();
                    component.Code = new CodeableConcept(CodeSystems.LOINC, "21842-0", "Birthplace", null);
                    component.Value = new FhirString(value);
                    BirthRecordIdentifier.Component.Add(component);
                }
                else
                {
                    // Find correct component; if doesn't exist add another
                    var stateComp = BirthRecordIdentifier.Component.FirstOrDefault(entry => ((Observation.ComponentComponent)entry).Code != null && ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault() != null && ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault().Code == "21842-0");
                    if (stateComp != null)
                    {
                        ((Observation.ComponentComponent)stateComp).Value = new FhirString(value);
                    }
                    else
                    {
                        Observation.ComponentComponent component = new Observation.ComponentComponent();
                        component.Code = new CodeableConcept(CodeSystems.LOINC, "21842-0", "Birthplace", null);
                        component.Value = new FhirString(value);
                        BirthRecordIdentifier.Component.Add(component);
                    }
                }
            }
        }

        /// <summary>Birth Record Year.</summary>
        /// <value>the year found on the decedent's birth certificate.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.BirthRecordYear = "1940";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Birth Record year: {ExampleDeathRecord.BirthRecordYear}");</para>
        /// </example>
        [Property("Birth Record Year", Property.Types.String, "Decedent Demographics", "Birth Record Year.", true, IGURL.BirthRecordIdentifier, true, 18)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='BR')", "")]
        public string BirthRecordYear
        {
            get
            {
                if (BirthRecordIdentifier != null && BirthRecordIdentifier.Component.Count > 0)
                {
                    // Find correct component
                    var stateComp = BirthRecordIdentifier.Component.FirstOrDefault(entry => ((Observation.ComponentComponent)entry).Code != null && ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault() != null && ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault().Code == "80904-6");
                    if (stateComp != null)
                    {
                        return Convert.ToString(stateComp.Value);
                    }
                }
                return null;
            }
            set
            {
                if (String.IsNullOrWhiteSpace(value)) {
                    return;
                }
                if (BirthRecordIdentifier == null)
                {
                    CreateBirthRecordIdentifier();
                    Observation.ComponentComponent component = new Observation.ComponentComponent();
                    component.Code = new CodeableConcept(CodeSystems.LOINC, "80904-6", "Birth year", null);
                    component.Value = new FhirDateTime(value);
                    BirthRecordIdentifier.Component.Add(component);
                }
                else
                {
                    // Find correct component; if doesn't exist add another
                    var stateComp = BirthRecordIdentifier.Component.FirstOrDefault(entry => ((Observation.ComponentComponent)entry).Code != null && ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault() != null && ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault().Code == "80904-6");
                    if (stateComp != null)
                    {
                        ((Observation.ComponentComponent)stateComp).Value = new FhirDateTime(value);
                    }
                    else
                    {
                        Observation.ComponentComponent component = new Observation.ComponentComponent();
                        component.Code = new CodeableConcept(CodeSystems.LOINC, "80904-6", "Birth year", null);
                        component.Value = new FhirDateTime(value);
                        BirthRecordIdentifier.Component.Add(component);
                    }
                }
            }
        }

        /// <summary>Decedent's Usual Occupation.</summary>
        /// <value>the decedent's usual occupation.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.UsualOccupation = "Biomedical engineering";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Usual Occupation: {ExampleDeathRecord.UsualOccupation}");</para>
        /// </example>
        [Property("Usual Occupation (Text)", Property.Types.String, "Decedent Demographics", "Decedent's Usual Occupation.", true, IGURL.DecedentUsualWork, true, 40)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='21843-8')", "")]
        public string UsualOccupation
        {
            get
            {
                if (UsualWork != null && UsualWork.Value != null && UsualWork.Value as CodeableConcept != null)
                {
                    Dictionary<string, string> dict = CodeableConceptToDict((CodeableConcept)UsualWork.Value);
                    if (dict.ContainsKey("text"))
                    {
                        return dict["text"];
                    }
                }
                return null;
            }
            set
            {
                if ((String.IsNullOrWhiteSpace(value)))
                {
                    return;
                }
                if (UsualWork == null)
                {
                    CreateUsualWork();
                }
                UsualWork.Value = new CodeableConcept(CodeSystems.NullFlavor_HL7_V3, "UNK", "unknown", value);     // code is required

            }
        }

        /// <summary>Decedent's Usual Industry (Text).</summary>
        /// <value>the decedent's usual industry.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.UsualIndustry = "Accounting";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Usual Industry: {ExampleDeathRecord.UsualIndustry}");</para>
        /// </example>
        [Property("Usual Industry (Text)", Property.Types.String, "Decedent Demographics", "Decedent's Usual Industry.", true, IGURL.DecedentUsualWork, true, 44)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='21843-8')", "")]
        public string UsualIndustry
        {
            get
            {
                if (UsualWork != null)
                {
                    Observation.ComponentComponent component = UsualWork.Component.FirstOrDefault(cmp => cmp.Code != null && cmp.Code.Coding != null && cmp.Code.Coding.Count() > 0 && cmp.Code.Coding.First().Code == "21844-6");
                    if (component != null && component.Value != null && component.Value as CodeableConcept != null
                        && CodeableConceptToDict((CodeableConcept)component.Value).ContainsKey("text"))
                    {
                        return CodeableConceptToDict((CodeableConcept)component.Value)["text"];
                    }
                }
                return null;
            }
            set
            {
                if (UsualWork == null)
                {
                    CreateUsualWork();
                }
                UsualWork.Component.RemoveAll(cmp => cmp.Code != null && cmp.Code.Coding != null && cmp.Code.Coding.Count() > 0 && cmp.Code.Coding.First().Code == "21844-6");
                if ((String.IsNullOrWhiteSpace(value)))
                {
                    return;
                }
                Observation.ComponentComponent component = new Observation.ComponentComponent();
                component.Code = new CodeableConcept(CodeSystems.LOINC, "21844-6", "History of Usual industry", null);
                component.Value = new CodeableConcept(CodeSystems.NullFlavor_HL7_V3, "UNK", "unknown", value);     // code is required
                UsualWork.Component.Add(component);

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
        /// <para>mserv.Add("system", CodeSystems.PH_YesNo_HL7_2x);</para>
        /// <para>mserv.Add("display", "Yes");</para>
        /// <para>ExampleDeathRecord.MilitaryService = uind;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Military Service: {ExampleDeathRecord.MilitaryService['display']}");</para>
        /// </example>
        [Property("Military Service", Property.Types.Dictionary, "Decedent Demographics", "Decedent's Military Service.", true, IGURL.DecedentMilitaryService, false, 22)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='55280-2')", "")]
        public Dictionary<string, string> MilitaryService
        {
            get
            {
                if (MilitaryServiceObs != null && MilitaryServiceObs.Value != null && MilitaryServiceObs.Value as CodeableConcept != null)
                {
                    return CodeableConceptToDict((CodeableConcept)MilitaryServiceObs.Value);
                }
                return EmptyCodeableDict();
            }
            set
            {
                if (IsDictEmptyOrDefault(value) && MilitaryServiceObs == null)
                {
                    return;
                }
                if (MilitaryServiceObs == null)
                {
                    MilitaryServiceObs = new Observation();
                    MilitaryServiceObs.Id = Guid.NewGuid().ToString();
                    MilitaryServiceObs.Meta = new Meta();
                    string[] militaryhistory_profile = { ProfileURL.DecedentMilitaryService };
                    MilitaryServiceObs.Meta.Profile = militaryhistory_profile;
                    MilitaryServiceObs.Status = ObservationStatus.Final;
                    MilitaryServiceObs.Code = new CodeableConcept(CodeSystems.LOINC, "55280-2", "Military service Narrative", null);
                    MilitaryServiceObs.Subject = new ResourceReference("urn:uuid:" + Decedent.Id);
                    MilitaryServiceObs.Value = DictToCodeableConcept(value);
                    AddReferenceToComposition(MilitaryServiceObs.Id, "DecedentDemographics");
                    Bundle.AddResourceEntry(MilitaryServiceObs, "urn:uuid:" + MilitaryServiceObs.Id);
                }
                else
                {
                    MilitaryServiceObs.Value = DictToCodeableConcept(value);
                }
            }
        }

        /// <summary>Decedent's Military Service. This is a helper method, to obtain the code use the MilitaryService property instead.</summary>
        /// <value>the decedent's military service. Whether the decedent served in the military, a null value means "unknown".</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.MilitaryServiceHelper = "Y";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Military Service: {ExampleDeathRecord.MilitaryServiceHelper}");</para>
        /// </example>
        [Property("Military Service Helper", Property.Types.String, "Decedent Demographics", "Decedent's Military Service.", false, IGURL.DecedentMilitaryService, false, 23)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='55280-2')", "")]
        public string MilitaryServiceHelper
        {
            get
            {
                if (MilitaryService.ContainsKey("code") && !String.IsNullOrWhiteSpace(MilitaryService["code"]))
                {
                    return (MilitaryService["code"]);
                }
                return null;
            }
            set
            {
                if(!String.IsNullOrWhiteSpace(value))
                {
                    SetCodeValue("MilitaryService", value, VRDR.ValueSets.YesNoUnknown.Codes);
                }
            }
        }

        /////////////////////////////////////////////////////////////////////////////////
        //
        // Record Properties: Decedent Disposition
        //
        /////////////////////////////////////////////////////////////////////////////////

        // /// <summary>Given name(s) of mortician.</summary>
        // /// <value>the mortician's name (first, middle, etc.)</value>
        // /// <example>
        // /// <para>// Setter:</para>
        // /// <para>string[] names = { "FD", "Middle" };</para>
        // /// <para>ExampleDeathRecord.MorticianGivenNames = names;</para>
        // /// <para>// Getter:</para>
        // /// <para>Console.WriteLine($"Mortician Given Name(s): {string.Join(", ", ExampleDeathRecord.MorticianGivenNames)}");</para>
        // /// </example>
        // [Property("Mortician Given Names", Property.Types.StringArr, "Decedent Disposition", "Given name(s) of mortician.", true, "http://build.fhir.org/ig/HL7/vrdr/StructureDefinition-VRDR-Mortician.html", false, 96)]
        // [FHIRPath("Bundle.entry.resource.where($this is Practitioner).where(meta.profile='http://hl7.org/fhir/us/core/StructureDefinition/us-core-practitioner')", "name")]
        // public string[] MorticianGivenNames
        // {
        //     get
        //     {
        //         if (Mortician != null && Mortician.Name.Count() > 0)
        //         {
        //             return Mortician.Name.First().Given.ToArray();
        //         }
        //         return new string[0];
        //     }
        //     set
        //     {
        //         InitializeMorticianIfNull();
        //         HumanName name = Mortician.Name.SingleOrDefault(n => n.Use == HumanName.NameUse.Official);
        //         if (name != null)
        //         {
        //             name.Given = value;
        //         }
        //         else
        //         {
        //             name = new HumanName();
        //             name.Use = HumanName.NameUse.Official;
        //             name.Given = value;
        //             Mortician.Name.Add(name);
        //         }
        //     }
        // }

        // /// <summary>Family name of mortician.</summary>
        // /// <value>the mortician's family name (i.e. last name)</value>
        // /// <example>
        // /// <para>// Setter:</para>
        // /// <para>ExampleDeathRecord.MorticianFamilyName = "Last";</para>
        // /// <para>// Getter:</para>
        // /// <para>Console.WriteLine($"Mortician's Last Name: {ExampleDeathRecord.MorticianFamilyName}");</para>
        // /// </example>
        // [Property("Mortician Family Name", Property.Types.String, "Decedent Disposition", "Family name of mortician.", true, "http://build.fhir.org/ig/HL7/vrdr/StructureDefinition-VRDR-Mortician.html", false, 97)]
        // [FHIRPath("Bundle.entry.resource.where($this is Practitioner).where(meta.profile='http://hl7.org/fhir/us/core/StructureDefinition/us-core-practitioner')", "name")]
        // public string MorticianFamilyName
        // {
        //     get
        //     {
        //         if (Mortician != null && Mortician.Name.Count() > 0)
        //         {
        //             return Mortician.Name.First().Family;
        //         }
        //         return null;
        //     }
        //     set
        //     {
        //         InitializeMorticianIfNull();
        //         HumanName name = Mortician.Name.FirstOrDefault();
        //         if (name != null && !String.IsNullOrEmpty(value))
        //         {
        //             name.Family = value;
        //         }
        //         else if (!String.IsNullOrEmpty(value))
        //         {
        //             name = new HumanName();
        //             name.Use = HumanName.NameUse.Official;
        //             name.Family = value;
        //             Mortician.Name.Add(name);
        //         }
        //     }
        // }

        // /// <summary>Mortician's Suffix.</summary>
        // /// <value>the mortician's suffix</value>
        // /// <example>
        // /// <para>// Setter:</para>
        // /// <para>ExampleDeathRecord.MorticianSuffix = "Jr.";</para>
        // /// <para>// Getter:</para>
        // /// <para>Console.WriteLine($"Mortician Suffix: {ExampleDeathRecord.MorticianSuffix}");</para>
        // /// </example>
        // [Property("Mortician Suffix", Property.Types.String, "Decedent Disposition", "Mortician's Suffix.", true, "http://build.fhir.org/ig/HL7/vrdr/StructureDefinition-VRDR-Mortician.html", false, 98)]
        // [FHIRPath("Bundle.entry.resource.where($this is Practitioner).where(meta.profile='http://hl7.org/fhir/us/core/StructureDefinition/us-core-practitioner')", "suffix")]
        // public string MorticianSuffix
        // {
        //     get
        //     {
        //         if (Mortician != null && Mortician.Name.Count() > 0 && Mortician.Name.First().Suffix.Count() > 0)
        //         {
        //             return Mortician.Name.First().Suffix.First();
        //         }
        //         return null;
        //     }
        //     set
        //     {
        //         InitializeMorticianIfNull();
        //         HumanName name = Mortician.Name.FirstOrDefault();
        //         if (name != null && !String.IsNullOrEmpty(value))
        //         {
        //             string[] suffix = { value };
        //             name.Suffix = suffix;
        //         }
        //         else if (!String.IsNullOrEmpty(value))
        //         {
        //             name = new HumanName();
        //             name.Use = HumanName.NameUse.Official;
        //             string[] suffix = { value };
        //             name.Suffix = suffix;
        //             Mortician.Name.Add(name);
        //         }
        //     }
        // }

        // /// <summary>Mortician Identifier.</summary>
        // /// <value>the mortician identification. A Dictionary representing a system (e.g. NPI) and a value, containing the following key/value pairs:
        // /// <para>"system" - the identifier system, e.g. US NPI</para>
        // /// <para>"value" - the idetifier value, e.g. US NPI number</para>
        // /// </value>
        // /// <example>
        // /// <para>// Setter:</para>
        // /// <para>Dictionary&lt;string, string&gt; identifier = new Dictionary&lt;string, string&gt;();</para>
        // /// <para>identifier.Add("system", "http://hl7.org/fhir/sid/us-npi");</para>
        // /// <para>identifier.Add("value", "1234567890");</para>
        // /// <para>ExampleDeathRecord.MorticianIdentifier = identifier;</para>
        // /// <para>// Getter:</para>
        // /// <para>Console.WriteLine($"\tMortician Identifier: {ExampleDeathRecord.MorticianIdentifier['value']}");</para>
        // /// </example>
        // [Property("Mortician Identifier", Property.Types.Dictionary, "Decedent Disposition", "Mortician Identifier.", true, "http://build.fhir.org/ig/HL7/vrdr/StructureDefinition-VRDR-Mortician.html", false, 99)]
        // [PropertyParam("system", "The identifier system.")]
        // [PropertyParam("value", "The identifier value.")]
        // [FHIRPath("Bundle.entry.resource.where($this is Practitioner).where(meta.profile='http://hl7.org/fhir/us/core/StructureDefinition/us-core-practitioner')", "identifier")]
        // public Dictionary<string, string> MorticianIdentifier
        // {
        //     get
        //     {
        //         Identifier identifier = Mortician?.Identifier?.FirstOrDefault();
        //         var result = new Dictionary<string, string>();
        //         if (identifier != null)
        //         {
        //             result["system"] = identifier.System;
        //             result["value"] = identifier.Value;
        //         }
        //         return result;
        //     }
        //     set
        //     {
        //         InitializeMorticianIfNull();
        //         if (Mortician.Identifier.Count > 0)
        //         {
        //             Mortician.Identifier.Clear();
        //         }
        //         if(value.ContainsKey("system") && value.ContainsKey("value")) {
        //             Identifier identifier = new Identifier();
        //             identifier.System = value["system"];
        //             identifier.Value = value["value"];
        //             Mortician.Identifier.Add(identifier);
        //         }
        //     }
        // }

        // private void InitializeMorticianIfNull()
        // {
        //     if (Mortician == null)
        //     {
        //         Mortician = new Practitioner();
        //         Mortician.Id = Guid.NewGuid().ToString();
        //         Mortician.Meta = new Meta();
        //         string[] mortician_profile = { "http://hl7.org/fhir/us/core/StructureDefinition/us-core-practitioner" };
        //         Mortician.Meta.Profile = mortician_profile;
        //     }
        // }

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
        /// <para>address.Add("addressState", "MA");</para>
        /// <para>address.Add("addressZip", "12345");</para>
        /// <para>address.Add("addressCountry", "US");</para>
        /// <para>ExampleDeathRecord.FuneralHomeAddress = address;</para>
        /// <para>// Getter:</para>
        /// <para>foreach(var pair in ExampleDeathRecord.FuneralHomeAddress)</para>
        /// <para>{</para>
        /// <para>  Console.WriteLine($"\FuneralHomeAddress key: {pair.Key}: value: {pair.Value}");</para>
        /// <para>};</para>
        /// </example>
        [Property("Funeral Home Address", Property.Types.Dictionary, "Decedent Disposition", "Funeral Home Address.", true, IGURL.FuneralHome, false, 93)]
        [PropertyParam("addressLine1", "address, line one")]
        [PropertyParam("addressLine2", "address, line two")]
        [PropertyParam("addressCity", "address, city")]
        [PropertyParam("addressCounty", "address, county")]
        [PropertyParam("addressState", "address, state")]
        [PropertyParam("addressStnum", "address, stnum")]
        [PropertyParam("addressStdesig", "address, stdesig")]
        [PropertyParam("addressPredir", "address, predir")]
        [PropertyParam("addressPostDir", "address, postdir")]
        [PropertyParam("addressStname", "address, stname")]
        [PropertyParam("addressUnitnum", "address, unitnum")]
        [PropertyParam("addressZip", "address, zip")]
        [PropertyParam("addressCountry", "address, country")]
        [FHIRPath("Bundle.entry.resource.where($this is Organization).where(type.coding.code='funeralhome')", "address")]
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
                    return EmptyAddrDict();
                }
            }
            set
            {
                if (FuneralHome == null)
                {
                    CreateFuneralHome();
                }

                FuneralHome.Address.Clear();
                FuneralHome.Address.Add(DictToAddress(value));

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
        [Property("Funeral Home Name", Property.Types.String, "Decedent Disposition", "Name of Funeral Home.", true, IGURL.FuneralHome, false, 94)]
        [FHIRPath("Bundle.entry.resource.where($this is Organization).where(type.coding.code='funeralhome')", "name")]
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
                if (String.IsNullOrWhiteSpace(value)) {
                    return;
                }
                if (FuneralHome == null)
                {
                    CreateFuneralHome();
                }
                FuneralHome.Name = value;
            }
        }

        // /// <summary>Funeral Director Phone.</summary>
        // /// <value>the funeral director phone number.</value>
        // /// <example>
        // /// <para>// Setter:</para>
        // /// <para>ExampleDeathRecord.FuneralDirectorPhone = "000-000-0000";</para>
        // /// <para>// Getter:</para>
        // /// <para>Console.WriteLine($"Funeral Director Phone: {ExampleDeathRecord.FuneralDirectorPhone}");</para>
        // /// </example>
        // [Property("Funeral Director Phone", Property.Types.String, "Decedent Disposition", "Funeral Director Phone.", true, "http://build.fhir.org/ig/HL7/vrdr/StructureDefinition-VRDR-Funeral-Service-Licensee.html", false, 95)]
        // [FHIRPath("Bundle.entry.resource.where($this is PractitionerRole)", "telecom")]
        // public string FuneralDirectorPhone
        // {
        //     get
        //     {
        //         string value = null;
        //         if (FuneralHomeDirector != null)
        //         {
        //             ContactPoint cp = FuneralHomeDirector.Telecom.FirstOrDefault(entry => entry.System == ContactPoint.ContactPointSystem.Phone);
        //             if (cp != null)
        //             {
        //                 value = cp.Value;
        //             }
        //         }
        //         return value;
        //     }
        //     set
        //     {
        //         if (FuneralHomeDirector == null)
        //         {
        //             FuneralHomeDirector = new PractitionerRole();
        //             FuneralHomeDirector.Id = Guid.NewGuid().ToString();
        //             FuneralHomeDirector.Meta = new Meta();
        //             string[] funeralhomedirector_profile = { "http://hl7.org/fhir/us/core/StructureDefinition/us-core-practitioner" };
        //             FuneralHomeDirector.Meta.Profile = funeralhomedirector_profile;
        //             AddReferenceToComposition(FuneralHomeDirector.Id);
        //             Bundle.AddResourceEntry(FuneralHomeDirector, "urn:uuid:" + FuneralHomeDirector.Id);
        //         }
        //         ContactPoint cp = FuneralHomeDirector.Telecom.FirstOrDefault(entry => entry.System == ContactPoint.ContactPointSystem.Phone);
        //         if (cp != null)
        //         {
        //             cp.Value = value;
        //         }
        //         else
        //         {
        //             cp = new ContactPoint();
        //             cp.System = ContactPoint.ContactPointSystem.Phone;
        //             cp.Value = value;
        //             FuneralHomeDirector.Telecom.Add(cp);
        //         }
        //     }
        // }

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
        /// <para>address.Add("addressState", "MA");</para>
        /// <para>address.Add("addressZip", "12345");</para>
        /// <para>address.Add("addressCountry", "US");</para>
        /// <para>ExampleDeathRecord.DispositionLocationAddress = address;</para>
        /// <para>// Getter:</para>
        /// <para>foreach(var pair in ExampleDeathRecord.DispositionLocationAddress)</para>
        /// <para>{</para>
        /// <para>  Console.WriteLine($"\DispositionLocationAddress key: {pair.Key}: value: {pair.Value}");</para>
        /// <para>};</para>
        /// </example>
        [Property("Disposition Location Address", Property.Types.Dictionary, "Decedent Disposition", "Disposition Location Address.", true, IGURL.DispositionLocation, true, 91)]
        [PropertyParam("addressLine1", "address, line one")]
        [PropertyParam("addressLine2", "address, line two")]
        [PropertyParam("addressCity", "address, city")]
        [PropertyParam("addressCounty", "address, county")]
        [PropertyParam("addressState", "address, state")]
        [PropertyParam("addressZip", "address, zip")]
        [PropertyParam("addressCountry", "address, country")]
        [FHIRPath("Bundle.entry.resource.where($this is Location).where(type.coding.code='disposition')", "address")]
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
                    CreateDispositionLocation();
                }

                DispositionLocation.Address = DictToAddress(value);
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
        [Property("Disposition Location Name", Property.Types.String, "Decedent Disposition", "Name of Disposition Location.", true, IGURL.DispositionLocation, false, 92)]
        [FHIRPath("Bundle.entry.resource.where($this is Location).where(type.coding.code='disposition')", "name")]
        public string DispositionLocationName
        {
            get
            {
                if (DispositionLocation != null && DispositionLocation.Name != null && DispositionLocation.Name != DeathRecord.BlankPlaceholder)
                {
                    return DispositionLocation.Name;
                }
                return null;
            }
            set
            {
                if (DispositionLocation == null)
                {
                    CreateDispositionLocation();
                }
                if (!String.IsNullOrWhiteSpace(value))
                {
                    DispositionLocation.Name = value;
                }
                else
                {
                    DispositionLocation.Name = DeathRecord.BlankPlaceholder; // We cannot have a blank string, but the field is required to be present
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
        /// <para>dmethod.Add("system", CodeSystems.SCT);</para>
        /// <para>dmethod.Add("display", "Burial");</para>
        /// <para>ExampleDeathRecord.DecedentDispositionMethod = dmethod;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Decedent Disposition Method: {ExampleDeathRecord.DecedentDispositionMethod['display']}");</para>
        /// </example>
        [Property("Decedent Disposition Method", Property.Types.Dictionary, "Decedent Disposition", "Decedent's Disposition Method.", true, IGURL.DecedentDispositionMethod, true, 1)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='80905-3')", "")]
        public Dictionary<string, string> DecedentDispositionMethod
        {
            get
            {
                if (DispositionMethod != null && DispositionMethod.Value != null && DispositionMethod.Value as CodeableConcept != null)
                {
                    return CodeableConceptToDict((CodeableConcept)DispositionMethod.Value);
                }
                return EmptyCodeableDict();
            }
            set
            {
                if (DispositionMethod == null)
                {
                    DispositionMethod = new Observation();
                    DispositionMethod.Id = Guid.NewGuid().ToString();
                    DispositionMethod.Meta = new Meta();
                    string[] dispositionmethod_profile = { ProfileURL.DecedentDispositionMethod };
                    DispositionMethod.Meta.Profile = dispositionmethod_profile;
                    DispositionMethod.Status = ObservationStatus.Final;
                    DispositionMethod.Code = new CodeableConcept(CodeSystems.LOINC, "80905-3", "Body disposition method", null);
                    DispositionMethod.Subject = new ResourceReference("urn:uuid:" + Decedent.Id);
                    //                    DispositionMethod.Performer.Add(new ResourceReference("urn:uuid:" + Mortician.Id));
                    DispositionMethod.Value = DictToCodeableConcept(value);
                    AddReferenceToComposition(DispositionMethod.Id, "DecedentDisposition");
                    Bundle.AddResourceEntry(DispositionMethod, "urn:uuid:" + DispositionMethod.Id);
                }
                else
                {
                    DispositionMethod.Value = DictToCodeableConcept(value);
                }
            }
        }
        /// <summary>Decedent's Disposition Method Helper.</summary>
        /// <value>the decedent's disposition method. A Dictionary representing a code, containing the following key/value pairs:
        /// <para>"code" - the code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.DecedentDispositionMethodHelper = VRDR.ValueSets.MethodOfDisposition.Burial;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Decedent Disposition Method: {ExampleDeathRecord.DecedentDispositionMethodHelper}");</para>
        /// </example>

        [Property("Decedent Disposition Method Helper", Property.Types.String, "Decedent Disposition", "Decedent's Disposition Method.", false, IGURL.DecedentDispositionMethod, true, 1)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='80905-3')", "")]
        public string DecedentDispositionMethodHelper
        {
            get
            {
                if (DecedentDispositionMethod.ContainsKey("code") && !String.IsNullOrWhiteSpace(DecedentDispositionMethod["code"]))
                {
                    return DecedentDispositionMethod["code"];
                }
                return null;
            }
            set
            {
                if(!String.IsNullOrWhiteSpace(value))
                {
                    SetCodeValue("DecedentDispositionMethod", value, VRDR.ValueSets.MethodOfDisposition.Codes);
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
        /// <para>code.Add("system", CodeSystems.PH_YesNo_HL7_2x);</para>
        /// <para>code.Add("display", "Yes");</para>
        /// <para>ExampleDeathRecord.AutopsyPerformedIndicator = code;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Autopsy Performed Indicator: {ExampleDeathRecord.AutopsyPerformedIndicator['display']}");</para>
        /// </example>
        [Property("Autopsy Performed Indicator", Property.Types.Dictionary, "Death Investigation", "Autopsy Performed Indicator.", true, IGURL.AutopsyPerformedIndicator, true, 28)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='85699-7')", "")]
        public Dictionary<string, string> AutopsyPerformedIndicator
        {
            get
            {
                if (AutopsyPerformed != null && AutopsyPerformed.Value != null && AutopsyPerformed.Value as CodeableConcept != null)
                {
                    return CodeableConceptToDict((CodeableConcept)AutopsyPerformed.Value);
                }
                return EmptyCodeableDict();
            }
            set
            {
                if (IsDictEmptyOrDefault(value) && AutopsyPerformed == null)
                {
                    return;
                }
                if (AutopsyPerformed == null)
                {
                    CreateAutopsyPerformed();
                }

                AutopsyPerformed.Value = DictToCodeableConcept(value);
            }
        }

        /// <summary>Autopsy Performed Indicator Helper. This is a helper method, to access the code use the AutopsyPerformedIndicator property.</summary>
        /// <value>autopsy performed indicator. A null value indicates "not applicable".</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.AutopsyPerformedIndicatorHelper = "Y"";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Autopsy Performed Indicator: {ExampleDeathRecord.AutopsyPerformedIndicatorBoolean}");</para>
        /// </example>
        [Property("Autopsy Performed Indicator Helper", Property.Types.String, "Death Investigation", "Autopsy Performed Indicator.", false, IGURL.AutopsyPerformedIndicator, true, 29)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='85699-7')", "")]
        public string AutopsyPerformedIndicatorHelper
        {
            get
            {
                if (AutopsyPerformedIndicator.ContainsKey("code") && !String.IsNullOrWhiteSpace(AutopsyPerformedIndicator["code"]))
                {
                    return AutopsyPerformedIndicator["code"];
                }
                return null;
            }
            set
            {
                if(!String.IsNullOrWhiteSpace(value))
                {
                    SetCodeValue("AutopsyPerformedIndicator", value, VRDR.ValueSets.YesNoUnknown.Codes);
                }
            }
        }
        // The idea here is that we have getters and setters for each of the parts of the death datetime, which get used in IJEMortality.cs
        // These getters and setters 1) use the DeathDateObs Observation 2) get and set values on the PartialDateTime extension using helpers that
        // can be reused across year, month, etc. 3) interpret -1 and null as data being absent (intentionally and unintentially, respectively),
        // and so set the data absent reason if value is -1 or null 4) when getting, look also in the valueDateTime and return the year from there
        // if it happens to be set (but never bother to set it ourselves)

        /// <summary>Decedent's Year of Death.</summary>
        /// <value>the decedent's year of death, or -1 if explicitly unknown, or null if never specified</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.DeathYear = 2018;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Decedent Year of Death: {ExampleDeathRecord.DeathYear}");</para>
        /// </example>
        [Property("DeathYear", Property.Types.Int32, "Death Investigation", "Decedent's Year of Death.", true, IGURL.DeathDate, true, 25)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='81956-5')", "")]
        public int? DeathYear
        {
            get
            {
                if (DeathDateObs != null && DeathDateObs.Value != null)
                {
                    return GetDateFragmentOrPartialDate(DeathDateObs.Value, ExtensionURL.DateYear);
                }
                return null;
            }
            set
            {
                if (DeathDateObs == null)
                {
                    CreateDeathDateObs();
                }
                SetPartialDate(DeathDateObs.Value.Extension.Find(ext => ext.Url == ExtensionURL.PartialDateTime), ExtensionURL.DateYear, value);
                UpdateDeathRecordIdentifier();
            }
        }

        /// <summary>Decedent's Month of Death.</summary>
        /// <value>the decedent's month of death, or -1 if explicitly unknown, or null if never specified</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.DeathMonth = 6;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Decedent Month of Death: {ExampleDeathRecord.DeathMonth}");</para>
        /// </example>
        [Property("DeathMonth", Property.Types.Int32, "Death Investigation", "Decedent's Month of Death.", true, IGURL.DeathDate, true, 25)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='81956-5')", "")]
        public int? DeathMonth
        {
            get
            {
                if (DeathDateObs != null && DeathDateObs.Value != null)
                {
                    return GetDateFragmentOrPartialDate(DeathDateObs.Value, ExtensionURL.DateMonth);
                }
                return null;
            }
            set
            {
                if (DeathDateObs == null)
                {
                    CreateDeathDateObs();
                }
                SetPartialDate(DeathDateObs.Value.Extension.Find(ext => ext.Url == ExtensionURL.PartialDateTime), ExtensionURL.DateMonth, value);
            }
        }

        /// <summary>Decedent's Day of Death.</summary>
        /// <value>the decedent's day of death, or -1 if explicitly unknown, or null if never specified</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.DeathDay = 16;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Decedent Day of Death: {ExampleDeathRecord.DeathDay}");</para>
        /// </example>
        [Property("DeathDay", Property.Types.Int32, "Death Investigation", "Decedent's Day of Death.", true, IGURL.DeathDate, true, 25)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='81956-5')", "")]
        public int? DeathDay
        {
            get
            {
                if (DeathDateObs != null && DeathDateObs.Value != null)
                {
                    return GetDateFragmentOrPartialDate(DeathDateObs.Value, ExtensionURL.DateDay);
                }
                return null;
            }
            set
            {
                if (DeathDateObs == null)
                {
                    CreateDeathDateObs();
                }
                SetPartialDate(DeathDateObs.Value.Extension.Find(ext => ext.Url == ExtensionURL.PartialDateTime), ExtensionURL.DateDay, value);
            }
        }
        /// <summary>Decedent's Time of Death.</summary>
        /// <value>the decedent's time of death, or "-1" if explicitly unknown, or null if never specified</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.DeathTime = "07:15:00";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Decedent Time of Death: {ExampleDeathRecord.DeathTime}");</para>
        /// </example>
        [Property("DeathTime", Property.Types.String, "Death Investigation", "Decedent's Time of Death.", true, IGURL.DeathDate, true, 25)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='81956-5')", "")]
        public string DeathTime
        {
            get
            {
                if (DeathDateObs != null && DeathDateObs.Value != null)
                {
                    return GetTimeFragmentOrPartialTime(DeathDateObs.Value);
                }
                return null;
            }
            set
            {
                if (String.IsNullOrWhiteSpace(value)) {
                    return;
                }
                if (DeathDateObs == null)
                {
                    CreateDeathDateObs();
                }
                SetPartialTime(DeathDateObs.Value.Extension.Find(ext => ext.Url == ExtensionURL.PartialDateTime), value);
            }
        }

        /* START datetimePronouncedDead */
        /// <summary>Decedent's Pronouncement Year of Death.</summary>
        /// <value>the decedent's pronouncement year of death, or null if never specified</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.DeathPronouncementYear = 2018;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Decedent Pronouncement Year of Death: {ExampleDeathRecord.DateOfDeathPronouncementYear}");</para>
        /// </example>
        [Property("DateOfDeathPronouncementYear", Property.Types.Int32, "Death Investigation", "Decedent's Pronouncement Year of Death.", true, IGURL.DeathDate, true, 25)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='81956-5')", "")]
        public int? DateOfDeathPronouncementYear
        {
            get
            {
                Observation.ComponentComponent pronouncementDateObs = GetDateOfDeathPronouncementObs();
                if (pronouncementDateObs != null && pronouncementDateObs.Value != null && pronouncementDateObs.Value is FhirDateTime)
                {
                    return GetDateFragment(pronouncementDateObs.Value, ExtensionURL.DateYear);
                }
                return null;
            }
            set
            {
                if (value == null || !value.HasValue) {
                    return;
                }
                Observation.ComponentComponent pronouncementDateObs = GetOrCreateDateOfDeathPronouncementObs();
                if (pronouncementDateObs != null && pronouncementDateObs.Value != null && pronouncementDateObs.Value is Time)
                {
                    // we need to convert to a FhirDateTime
                    pronouncementDateObs.Value = ConvertFhirTimeToFhirDateTime((Time)pronouncementDateObs.Value);
                }
                if (pronouncementDateObs != null && pronouncementDateObs.Value == null) {
                    pronouncementDateObs.Value = new FhirDateTime(); // initialize date object
                }
                if (pronouncementDateObs != null && pronouncementDateObs.Value != null && pronouncementDateObs.Value is FhirDateTime)
                {
                    // If we have a basic value as a valueDateTime use that, otherwise pull from the PartialDateTime extension
                    DateTimeOffset? dateTimeOffset = null;
                    if (pronouncementDateObs.Value is FhirDateTime && ((FhirDateTime)pronouncementDateObs.Value).Value != null)
                    {
                        // Note: We can't just call ToDateTimeOffset() on the FhirDateTime because want the datetime in whatever local time zone was provided
                        dateTimeOffset = DateTimeOffset.Parse(((FhirDateTime)pronouncementDateObs.Value).Value);
                    }
                    var dt = dateTimeOffset ?? DateTimeOffset.MinValue;
                    var newFdt = new FhirDateTime(value.Value, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second, TimeSpan.Zero);
                    pronouncementDateObs.Value = newFdt;
                }
            }
        }

        /// <summary>Decedent's Pronouncement Month of Death.</summary>
        /// <value>the decedent's pronouncement month of death, or -1 if explicitly unknown, or null if never specified</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.DateOfDeathPronouncementMonth = 6;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Decedent Pronouncement Month of Death: {ExampleDeathRecord.DateOfDeathPronouncementMonth}");</para>
        /// </example>
        [Property("DeathMonth", Property.Types.Int32, "Death Investigation", "Decedent's Pronouncement Month of Death.", true, IGURL.DeathDate, true, 25)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='81956-5')", "")]
        public int? DateOfDeathPronouncementMonth
        {
            get
            {
                Observation.ComponentComponent pronouncementDateObs = GetDateOfDeathPronouncementObs();
                if (pronouncementDateObs != null && pronouncementDateObs.Value != null && pronouncementDateObs.Value is FhirDateTime)
                {
                    return GetDateFragment(pronouncementDateObs.Value, ExtensionURL.DateMonth);
                }
                return null;
            }
            set
            {
                if (value == null || !value.HasValue) {
                    return;
                }
                Observation.ComponentComponent pronouncementDateObs = GetOrCreateDateOfDeathPronouncementObs();
                if (pronouncementDateObs != null && pronouncementDateObs.Value != null && pronouncementDateObs.Value is Time)
                {
                    // we need to convert to a FhirDateTime
                    pronouncementDateObs.Value = ConvertFhirTimeToFhirDateTime((Time)pronouncementDateObs.Value);
                }
                if (pronouncementDateObs != null && pronouncementDateObs.Value == null) {
                    pronouncementDateObs.Value = new FhirDateTime(); // initialize date object
                }
                if (pronouncementDateObs != null && pronouncementDateObs.Value != null && pronouncementDateObs.Value is FhirDateTime)
                {
                    // If we have a basic value as a valueDateTime use that, otherwise pull from the PartialDateTime extension
                    DateTimeOffset? dateTimeOffset = null;
                    if (pronouncementDateObs.Value is FhirDateTime && ((FhirDateTime)pronouncementDateObs.Value).Value != null)
                    {
                        // Note: We can't just call ToDateTimeOffset() on the FhirDateTime because want the datetime in whatever local time zone was provided
                        dateTimeOffset = DateTimeOffset.Parse(((FhirDateTime)pronouncementDateObs.Value).Value);
                    }
                    var dt = dateTimeOffset ?? DateTimeOffset.MinValue;
                    var newFdt = new FhirDateTime(dt.Year, value.Value, dt.Day, dt.Hour, dt.Minute, dt.Second, TimeSpan.Zero);
                    pronouncementDateObs.Value = newFdt;
                }
            }

        }

        /// <summary>Decedent's Pronouncement Day of Death.</summary>
        /// <value>the decedent's pronouncement day of death, or -1 if explicitly unknown, or null if never specified</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.DateOfDeathPronouncementDay = 16;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Decedent Prounecement Day of Death: {ExampleDeathRecord.DateOfDeathPronouncementDay}");</para>
        /// </example>
        [Property("DeathDay", Property.Types.Int32, "Death Investigation", "Decedent's Pronouncement Day of Death.", true, IGURL.DeathDate, true, 25)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='81956-5')", "")]
        public int? DateOfDeathPronouncementDay
        {
            get
            {
                Observation.ComponentComponent pronouncementDateObs = GetDateOfDeathPronouncementObs();
                if (pronouncementDateObs != null && pronouncementDateObs.Value != null && pronouncementDateObs.Value is FhirDateTime)
                {
                    return GetDateFragment(pronouncementDateObs.Value, ExtensionURL.DateDay);
                }
                return null;

            }
            set
            {
                if (value == null || !value.HasValue) {
                    return;
                }
                Observation.ComponentComponent pronouncementDateObs = GetOrCreateDateOfDeathPronouncementObs();
                if (pronouncementDateObs != null && pronouncementDateObs.Value != null && pronouncementDateObs.Value is Time)
                {
                    // we need to convert to a FhirDateTime
                    pronouncementDateObs.Value = ConvertFhirTimeToFhirDateTime((Time)pronouncementDateObs.Value);
                }
                if (pronouncementDateObs != null && pronouncementDateObs.Value == null) {
                    pronouncementDateObs.Value = new FhirDateTime(); // initialize date object
                }
                if (pronouncementDateObs != null && pronouncementDateObs.Value != null && pronouncementDateObs.Value is FhirDateTime)
                {
                    // If we have a basic value as a valueDateTime use that, otherwise pull from the PartialDateTime extension
                    DateTimeOffset? dateTimeOffset = null;
                    if (pronouncementDateObs.Value is FhirDateTime && ((FhirDateTime)pronouncementDateObs.Value).Value != null)
                    {
                        // Note: We can't just call ToDateTimeOffset() on the FhirDateTime because want the datetime in whatever local time zone was provided
                        dateTimeOffset = DateTimeOffset.Parse(((FhirDateTime)pronouncementDateObs.Value).Value);
                    }
                    var dt = dateTimeOffset ?? DateTimeOffset.MinValue;
                    var newFdt = new FhirDateTime(dt.Year, dt.Month, value.Value, dt.Hour, dt.Minute, dt.Second, TimeSpan.Zero);
                    pronouncementDateObs.Value = newFdt;
                }
            }
        }

        /// <summary>Decedent's Pronouncement Time of Death.</summary>
        /// <value>the decedent's pronouncement time of death, or "-1" if explicitly unknown, or null if never specified</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.DateOfDeathPronouncementTime = "07:15:00";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Decedent Pronouncement Time of Death: {ExampleDeathRecord.DateOfDeathPronouncementTime}");</para>
        /// </example>
        [Property("DeathTime", Property.Types.String, "Death Investigation", "Decedent's Pronoucement Time of Death.", true, IGURL.DeathDate, true, 25)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='81956-5')", "")]
        public string DateOfDeathPronouncementTime
        {
            get
            {
                Observation.ComponentComponent pronouncementDateObs = GetDateOfDeathPronouncementObs();
                if (pronouncementDateObs != null && pronouncementDateObs.Value != null && pronouncementDateObs.Value is FhirDateTime)
                {
                    return GetTimeFragment(pronouncementDateObs.Value);
                }
                 if (pronouncementDateObs != null && pronouncementDateObs.Value != null && pronouncementDateObs.Value is Time)
                {
                    var time = (Time)pronouncementDateObs.Value;
                    return time.Value;
                }
                return null;
            }
            set
            {
                if (String.IsNullOrEmpty(value)) {
                    return;
                }

                // we need to force it to be 00:00:00 format to be compliant with the IG because the FHIR class doesn't
                if (value.Length < 8)
                {
                    value += ":";
                    value = value.PadRight(8, '0');
                }
                Observation.ComponentComponent pronouncementDateObs = GetOrCreateDateOfDeathPronouncementObs();
                // if we are only storing time, set just the valueTime
                if (pronouncementDateObs != null && (pronouncementDateObs.Value == null || pronouncementDateObs.Value is Time)) {
                    pronouncementDateObs.Value = new Time(value); // set to FhirTime
                    return;
                }

                // otherwise we need to set the time portion of the valueDateTime
                if (pronouncementDateObs.Value != null && pronouncementDateObs.Value is FhirDateTime) {
                    // set time part of FhirDateTime
                    var ft = new Time(value);
                    var fdt = (FhirDateTime)pronouncementDateObs.Value;
                    DateTimeOffset? dateTimeOffset = null;
                    if (pronouncementDateObs.Value is FhirDateTime && ((FhirDateTime)pronouncementDateObs.Value).Value != null)
                    {
                        // Note: We can't just call ToDateTimeOffset() on the FhirDateTime because want the datetime in whatever local time zone was provided
                        dateTimeOffset = DateTimeOffset.Parse(((FhirDateTime)pronouncementDateObs.Value).Value);
                    }
                    var dto = dateTimeOffset ?? DateTimeOffset.MinValue;
                    var newFdt = new FhirDateTime(dto.Year, dto.Month, dto.Day, FhirTimeHour(ft), FhirTimeMin(ft), FhirTimeSec(ft), TimeSpan.Zero);
                    pronouncementDateObs.Value = newFdt;
                }
            }
        }
        /* END datetimePronouncedDead */

        /// <summary>DateOfDeathDeterminationMethod.</summary>
        /// <value>method. A Dictionary representing a code, containing the following key/value pairs:
        /// <para>"code" - the code</para>
        /// <para>"system" - the code system this code belongs to</para>
        /// <para>"display" - a human readable meaning of the code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; code = new Dictionary&lt;string, string&gt;();</para>
        /// <para>code.Add("code", "exact");</para>
        /// <para>code.Add("system", CodeSystems.DateOfDeathDeterminationMethods);</para>
        /// <para>code.Add("display", "exact");</para>
        /// <para>ExampleDeathRecord.DateOfDeathDeterminationMethod = code;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Date of Death Determination Method: {ExampleDeathRecord.DateOfDeathDeterminationMethod['display']}");</para>
        /// </example>

        [Property("DateOfDeathDeterminationMethod", Property.Types.Dictionary, "Death Investigation", "Date of Death Determination Method.", true, IGURL.DeathDate, true, 25)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='81956-5').method", "")]
        public Dictionary<string, string> DateOfDeathDeterminationMethod
        {
            get
            {
                if (DeathDateObs != null && (DeathDateObs.Method as CodeableConcept) != null)
                {
                    return CodeableConceptToDict(DeathDateObs.Method);
                }
                return EmptyCodeableDict();
            }
            set
            {
                if (DeathDateObs == null)
                {
                    CreateDeathDateObs();
                }
                DeathDateObs.Method = DictToCodeableConcept(value);
            }

        }

        /// <summary>Decedent's Date/Time of Death.</summary>
        /// <value>the decedent's date and time of death</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.DateOfDeath = "2018-02-19T16:48:06-05:00";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Decedent Date of Death: {ExampleDeathRecord.DateOfDeath}");</para>
        /// </example>
        [Property("Date/Time Of Death", Property.Types.StringDateTime, "Death Investigation", "Decedent's Date+Time of Death.", true, IGURL.DeathDate, true, 25)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='81956-5')", "")]
        public string DateOfDeath
        {
            get
            {
                // We support this legacy API entrypoint via the new partial date and time entrypoints
                if (DeathYear != null && DeathYear != -1 && DeathMonth != null && DeathMonth != -1 && DeathDay != null && DeathDay != -1 && DeathTime != null && DeathTime != "-1")
                {
                    DateTimeOffset parsedTime;
                    if (DateTimeOffset.TryParse(DeathTime, out parsedTime))
                    {
                        DateTimeOffset result = new DateTimeOffset((int)DeathYear, (int)DeathMonth, (int)DeathDay, parsedTime.Hour, parsedTime.Minute, parsedTime.Second, TimeSpan.Zero);
                        return result.ToString("s");
                    }
                }
                else if (DeathYear != null && DeathYear != -1 && DeathMonth != null && DeathMonth != -1 && DeathDay != null && DeathDay != -1)
                {
                    DateTime result = new DateTime((int)DeathYear, (int)DeathMonth, (int)DeathDay);
                    return result.ToString("s");
                }
                return null;
            }
            set
            {
                // We support this legacy API entrypoint via the new partial date and time entrypoints
                DateTimeOffset parsedTime;
                if (DateTimeOffset.TryParse(value, out parsedTime))
                {
                    DeathYear = parsedTime.Year;
                    DeathMonth = parsedTime.Month;
                    DeathDay = parsedTime.Day;
                    TimeSpan timeSpan = new TimeSpan(0, parsedTime.Hour, parsedTime.Minute, parsedTime.Second);
                    DeathTime = timeSpan.ToString(@"hh\:mm\:ss");
                }
            }
        }

        /// <summary>Decedent's Date/Time of Death Pronouncement as a component.</summary>
        /// <value>the decedent's date and time of death pronouncement observation component</value>
         public Observation.ComponentComponent GetDateOfDeathPronouncementObs() {
            if (DeathDateObs != null && DeathDateObs.Component.Count > 0) // if there is a value for death pronouncement type, return it
            {
                var pronComp = DeathDateObs.Component.FirstOrDefault(entry => ((Observation.ComponentComponent)entry).Code != null
                        && ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault() != null && ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault().Code == "80616-6");
                if (pronComp != null && pronComp.Value != null)
                {
                    return pronComp;
                }
            }
            return null;
        }

        /// <summary>Get or Create Decedent's Date/Time of Death Pronouncement as a component.</summary>
        /// <value>the decedent's date and time of death pronouncement observation component, if not present create it, return it in either case</value>
        public Observation.ComponentComponent GetOrCreateDateOfDeathPronouncementObs() {
            Observation.ComponentComponent pronouncementDateObs = GetDateOfDeathPronouncementObs();
            if (pronouncementDateObs == null)
            {
                pronouncementDateObs = CreateDateOfDeathPronouncementObs();
            }
            return pronouncementDateObs;
        }

        /// <summary>Decedent's Date/Time of Death Pronouncement.</summary>
        /// <value>the decedent's date and time of death pronouncement</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.DateOfDeathPronouncement = "2018-02-20T16:48:06-05:00";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Decedent Date of Death Pronouncement: {ExampleDeathRecord.DateOfDeathPronouncement}");</para>
        /// </example>
        [Property("Date/Time Of Death Pronouncement", Property.Types.StringDateTime, "Death Investigation", "Decedent's Date/Time of Death Pronouncement.", true, IGURL.DeathDate, false, 20)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='81956-5').component.where(code.coding.code='80616-6')", "")]
        public string DateOfDeathPronouncement
        {
            get {
                if (DateOfDeathPronouncementYear != null && DateOfDeathPronouncementYear != -1 && DateOfDeathPronouncementMonth != null && DateOfDeathPronouncementMonth != -1 && DateOfDeathPronouncementDay != null && DateOfDeathPronouncementDay != -1 && DateOfDeathPronouncementTime != null && DateOfDeathPronouncementTime != "-1")
                {
                    DateTimeOffset parsedTime;
                    if (DateTimeOffset.TryParse(DateOfDeathPronouncementTime, out parsedTime))
                    {
                        DateTimeOffset result = new DateTimeOffset((int)DateOfDeathPronouncementYear, (int)DateOfDeathPronouncementMonth, (int)DateOfDeathPronouncementDay, parsedTime.Hour, parsedTime.Minute, parsedTime.Second, TimeSpan.Zero);
                        return result.ToString("s");
                    }
                }
                else if (DateOfDeathPronouncementYear != null && DateOfDeathPronouncementYear != -1 && DateOfDeathPronouncementMonth != null && DateOfDeathPronouncementMonth != -1 && DateOfDeathPronouncementDay != null && DateOfDeathPronouncementDay != -1)
                {
                    DateTime result = new DateTime((int)DateOfDeathPronouncementYear, (int)DateOfDeathPronouncementMonth, (int)DateOfDeathPronouncementDay);
                    return result.ToString("s");
                }
                else if (DateOfDeathPronouncementYear == null && DateOfDeathPronouncementMonth == null && DateOfDeathPronouncementDay == null && DateOfDeathPronouncementTime != null)
                {
                    return DateOfDeathPronouncementTime;
                }
                return null;
            }
            set
            {
                if (String.IsNullOrEmpty(value)) {
                    return;
                }
                // We support this legacy API entrypoint via the new partial date and time entrypoints
                DateTimeOffset parsedTime;
                if (DateTimeOffset.TryParse(value, out parsedTime))
                {
                    DateOfDeathPronouncementYear = parsedTime.Year;
                    DateOfDeathPronouncementMonth = parsedTime.Month;
                    DateOfDeathPronouncementDay = parsedTime.Day;
                    TimeSpan timeSpan = new TimeSpan(0, parsedTime.Hour, parsedTime.Minute, parsedTime.Second);
                    DateOfDeathPronouncementTime = timeSpan.ToString(@"hh\:mm\:ss");
                }
            }
        }

        /// <summary>Decedent's Year of Surgery.</summary>
        /// <value>the decedent's year of surgery, or -1 if explicitly unknown, or null if never specified</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.SurgeryYear = 2018;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Decedent Year of Surgery: {ExampleDeathRecord.SurgeryYear}");</para>
        /// </example>
        [Property("SurgeryYear", Property.Types.Int32, "Death Investigation", "Decedent's Year of Surgery.", true, IGURL.SurgeryDate, true, 25)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='80992-1')", "")]
        public int? SurgeryYear
        {
            get
            {
                if (SurgeryDateObs != null && SurgeryDateObs.Value != null)
                {
                    return GetDateFragmentOrPartialDate(SurgeryDateObs.Value, ExtensionURL.DateYear);
                }
                return null;
            }
            set
            {
                if (SurgeryDateObs == null)
                {
                    CreateSurgeryDateObs();
                }
                SetPartialDate(SurgeryDateObs.Value.Extension.Find(ext => ext.Url == ExtensionURL.PartialDate), ExtensionURL.DateYear, value);
            }
        }

        /// <summary>Decedent's Month of Surgery.</summary>
        /// <value>the decedent's month of surgery, or -1 if explicitly unknown, or null if never specified</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.SurgeryMonth = 6;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Decedent Month of Surgery: {ExampleDeathRecord.SurgeryMonth}");</para>
        /// </example>
        [Property("SurgeryMonth", Property.Types.Int32, "Death Investigation", "Decedent's Month of Surgery.", true, IGURL.SurgeryDate, true, 25)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='80992-1')", "")]
        public int? SurgeryMonth
        {
            get
            {
                if (SurgeryDateObs != null && SurgeryDateObs.Value != null)
                {
                    return GetDateFragmentOrPartialDate(SurgeryDateObs.Value, ExtensionURL.DateMonth);
                }
                return null;
            }
            set
            {
                if (SurgeryDateObs == null)
                {
                    CreateSurgeryDateObs();
                }
                SetPartialDate(SurgeryDateObs.Value.Extension.Find(ext => ext.Url == ExtensionURL.PartialDate), ExtensionURL.DateMonth, value);
            }
        }

        /// <summary>Decedent's Day of Surgery.</summary>
        /// <value>the decedent's day of surgery, or -1 if explicitly unknown, or null if never specified</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.SurgeryDay = 16;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Decedent Day of Surgery: {ExampleDeathRecord.SurgeryDay}");</para>
        /// </example>
        [Property("SurgeryDay", Property.Types.Int32, "Death Investigation", "Decedent's Day of Surgery.", true, IGURL.SurgeryDate, true, 25)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='80992-1')", "")]
        public int? SurgeryDay
        {
            get
            {
                if (SurgeryDateObs != null && SurgeryDateObs.Value != null)
                {
                    return GetDateFragmentOrPartialDate(SurgeryDateObs.Value, ExtensionURL.DateDay);
                }
                return null;
            }
            set
            {
                if (SurgeryDateObs == null)
                {
                    CreateSurgeryDateObs();
                }
                SetPartialDate(SurgeryDateObs.Value.Extension.Find(ext => ext.Url == ExtensionURL.PartialDate), ExtensionURL.DateDay, value);
            }
        }

        /// <summary>Decedent's Surgery Date.</summary>
        /// <value>the decedent's date of surgery</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.SurgeryDate = "2018-02-19";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Decedent Surgery Date: {ExampleDeathRecord.SurgeryDate}");</para>
        /// </example>
        [Property("Surgery Date", Property.Types.StringDateTime, "Death Investigation", "Decedent's Date and Time of Surgery.", true, IGURL.SurgeryDate, true, 25)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='80992-1')", "")]
        public string SurgeryDate
        {
            get
            {
                // We support this legacy-style API entrypoint via the new partial date and time entrypoints
                if (SurgeryYear != null && SurgeryYear != -1 && SurgeryMonth != null && SurgeryMonth != -1 && SurgeryDay != null && SurgeryDay != -1)
                {
                    Date result = new Date((int)SurgeryYear, (int)SurgeryMonth, (int)SurgeryDay);
                    return result.ToString();
                }
                return null;
            }
            set
            {
                // We support this legacy-style API entrypoint via the new partial date and time entrypoints
                DateTimeOffset parsedDate;
                if (DateTimeOffset.TryParse(value, out parsedDate))
                {
                    SurgeryYear = parsedDate.Year;
                    SurgeryMonth = parsedDate.Month;
                    SurgeryDay = parsedDate.Day;
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
        /// <para>code.Add("system", CodeSystems.PH_YesNo_HL7_2x);</para>
        /// <para>code.Add("display", "Yes");</para>
        /// <para>ExampleDeathRecord.AutopsyResultsAvailable = code;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Autopsy Results Available: {ExampleDeathRecord.AutopsyResultsAvailable['display']}");</para>
        /// </example>
        [Property("Autopsy Results Available", Property.Types.Dictionary, "Death Investigation", "Autopsy results available, used to complete cause of death.", true, IGURL.AutopsyPerformedIndicator, true, 30)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='85699-7')", "")]
        public Dictionary<string, string> AutopsyResultsAvailable
        {
            get
            {
                if (AutopsyPerformed == null || AutopsyPerformed.Component == null)
                {
                    return EmptyCodeableDict();
                }
                var performed = AutopsyPerformed.Component.FirstOrDefault(entry => ((Observation.ComponentComponent)entry).Code != null
                    && ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault() != null && ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault().Code == "69436-4");
                if (performed != null)
                {
                    return CodeableConceptToDict((CodeableConcept)performed.Value);
                }
                return EmptyCodeableDict();
            }
            set
            {
                if (IsDictEmptyOrDefault(value) && AutopsyPerformed == null)
                {
                    return;
                }
                if (AutopsyPerformed == null)
                {
                    CreateAutopsyPerformed();
                }
                Observation.ComponentComponent component = new Observation.ComponentComponent();
                component.Code = new CodeableConcept(CodeSystems.LOINC, "69436-4", "Autopsy results available", null);
                component.Value = DictToCodeableConcept(value);
                AutopsyPerformed.Component.Clear();
                AutopsyPerformed.Component.Add(component);
            }
        }

        /// <summary>Autopsy Results Available Helper. This is a convenience method, to access the coded value use AutopsyResultsAvailable.</summary>
        /// <value>autopsy results available. A null value indicates "not applicable".</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.AutopsyResultsAvailableHelper = "N";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Autopsy Results Available: {ExampleDeathRecord.AutopsyResultsAvailableHelper}");</para>
        /// </example>
        [Property("Autopsy Results Available Helper", Property.Types.String, "Death Investigation", "Autopsy results available, used to complete cause of death.", false, IGURL.AutopsyPerformedIndicator, true, 31)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='85699-7')", "")]
        public string AutopsyResultsAvailableHelper
        {
            get
            {
                if (AutopsyResultsAvailable.ContainsKey("code") && !String.IsNullOrWhiteSpace(AutopsyResultsAvailable["code"]))
                {
                    return AutopsyResultsAvailable["code"];
                }
                return null;
            }
            set
            {
                if(!String.IsNullOrWhiteSpace(value))
                {
                    SetCodeValue("AutopsyResultsAvailable", value, VRDR.ValueSets.YesNoUnknownNotApplicable.Codes);
                }
            }
        }

        /// <summary>Death Location Jurisdiction.</summary>
        /// <value>the vital record jurisdiction identifier.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.DeathLocationJurisdiction = "MA";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Death Location Jurisdiction: {ExampleDeathRecord.DeathLocationJurisdiction}");</para>
        /// </example>
        [Property("Death Location Jurisdiction", Property.Types.String, "Death Investigation", "Vital Records Jurisdiction of Death Location (two character jurisdiction code, e.g. CA).", true, IGURL.DeathLocation, false, 16)]
        [FHIRPath("Bundle.entry.resource.where($this is Location).where(type.coding.code='death')", "")]
        public string DeathLocationJurisdiction
        {
            get
            {
                // If addressJurisdiction is present use it, otherwise return the addressState
                if (DeathLocationAddress.ContainsKey("addressJurisdiction") && !String.IsNullOrWhiteSpace(DeathLocationAddress["addressJurisdiction"]))
                {
                    return DeathLocationAddress["addressJurisdiction"];
                }
                if (DeathLocationAddress.ContainsKey("addressState") && !String.IsNullOrWhiteSpace(DeathLocationAddress["addressState"]))
                {
                    return DeathLocationAddress["addressState"];
                }
                return null;
            }
            set
            {
                // If the jurisdiction is YC (New York City) set the addressJurisdiction to YC and the addressState to NY, otherwise set both to the same;
                // setting the addressJurisdiction is technically optional but the way we use DeathLocationAddress to constantly read the existing values
                // when adding new values means that having both set correctly is important for consistency
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Dictionary<string, string> currentAddress = DeathLocationAddress;
                    if (value == "YC")
                    {
                        currentAddress["addressJurisdiction"] = value;
                        currentAddress["addressState"] = "NY";
                    }
                    else
                    {
                        currentAddress["addressJurisdiction"] = value;
                        currentAddress["addressState"] = value;
                    }
                    DeathLocationAddress = currentAddress;
                    UpdateDeathRecordIdentifier();
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
        /// <para>address.Add("addressState", "MA");</para>
        /// <para>address.Add("addressZip", "12345");</para>
        /// <para>address.Add("addressCountry", "US");</para>
        /// <para>ExampleDeathRecord.DeathLocationAddress = address;</para>
        /// <para>// Getter:</para>
        /// <para>foreach(var pair in ExampleDeathRecord.DeathLocationAddress)</para>
        /// <para>{</para>
        /// <para>  Console.WriteLine($"\DeathLocationAddress key: {pair.Key}: value: {pair.Value}");</para>
        /// <para>};</para>
        /// </example>
        [Property("Death Location Address", Property.Types.Dictionary, "Death Investigation", "Location of Death.", true, IGURL.DeathLocation, true, 15)]
        [PropertyParam("addressLine1", "address, line one")]
        [PropertyParam("addressLine2", "address, line two")]
        [PropertyParam("addressCity", "address, city")]
        [PropertyParam("addressCounty", "address, county")]
        [PropertyParam("addressState", "address, state literal")]
        [PropertyParam("addressZip", "address, zip")]
        [PropertyParam("addressCountry", "address, country literal")]
        [FHIRPath("Bundle.entry.resource.where($this is Location).where(type.coding.code='death')", "address")]
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
                    CreateDeathLocation();
                }
                DeathLocationLoc.Address = DictToAddress(value);
                UpdateDeathRecordIdentifier();
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
        [Property("Death Location Name", Property.Types.String, "Death Investigation", "Name of Death Location.", true, IGURL.DeathLocation, false, 17)]
        [FHIRPath("Bundle.entry.resource.where($this is Location).where(type.coding.code='death')", "name")]
        public string DeathLocationName
        {
            get
            {
                if (DeathLocationLoc != null && DeathLocationLoc.Name != null && DeathLocationLoc.Name != DeathRecord.BlankPlaceholder)
                {
                    return DeathLocationLoc.Name;
                }
                return null;
            }
            set
            {
                if (DeathLocationLoc == null)
                {
                    CreateDeathLocation();
                }
                if (!String.IsNullOrWhiteSpace(value))
                {
                    DeathLocationLoc.Name = value;
                }
                else
                {
                    DeathLocationLoc.Name = DeathRecord.BlankPlaceholder; // We cannot have a blank string, but the field is required to be present
                }
            }
        }

        /// <summary>Lattitude of Death Location.</summary>
        /// <value>tLattitude of Death Location.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.DeathLocationLattitude = "37.88888" ;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Death Location Lattitude: {ExampleDeathRecord.DeathLocationLattitude}");</para>
        /// </example>
        [Property("Death Location Latitude", Property.Types.String, "Death Investigation", "Death Location Lattitude.", true, IGURL.DeathLocation, false, 17)]
        [FHIRPath("Bundle.entry.resource.where($this is Location).where(type.coding.code='death')", "position")]
        public string DeathLocationLatitude
        {
            get
            {
                if (DeathLocationLoc != null && DeathLocationLoc.Position != null)
                {
                    return (DeathLocationLoc.Position.Latitude).ToString();
                }
                return null;
            }
            set
            {
                if (String.IsNullOrWhiteSpace(value)) {
                    return;
                }
                if (DeathLocationLoc == null)
                {
                    CreateDeathLocation();
                }
                if (DeathLocationLoc.Position == null)
                {
                    DeathLocationLoc.Position = new Location.PositionComponent();
                }
                DeathLocationLoc.Position.Latitude = Convert.ToDecimal(value);
            }
        }

        /// <summary>Longitude of Death Location.</summary>
        /// <value>Longitude of Death Location.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.DeathLocationLongitude = "-50.000" ;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Death Location Longitude: {ExampleDeathRecord.DeathLocationLongitude}");</para>
        /// </example>
        [Property("Death Location Longitude", Property.Types.String, "Death Investigation", "Death Location Lattitude.", true, IGURL.DeathLocation, false, 17)]
        [FHIRPath("Bundle.entry.resource.where($this is Location).where(type.coding.code='death')", "position")]
        public string DeathLocationLongitude
        {
            get
            {
                if (DeathLocationLoc != null && DeathLocationLoc.Position != null)
                {
                    return (DeathLocationLoc.Position.Longitude).ToString();
                }
                return null;
            }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    return;
                }
                if (DeathLocationLoc == null)
                {
                    CreateDeathLocation();
                }
                if (DeathLocationLoc.Position == null)
                {
                    DeathLocationLoc.Position = new Location.PositionComponent();
                }
                DeathLocationLoc.Position.Longitude = Convert.ToDecimal(value);
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
        [Property("Death Location Description", Property.Types.String, "Death Investigation", "Description of Death Location.", true, IGURL.DeathLocation, false, 18)]
        [FHIRPath("Bundle.entry.resource.where($this is Location).where(type.coding.code='death')", "description")]
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
                if (String.IsNullOrWhiteSpace(value)) {
                    return;
                }
                if (DeathLocationLoc == null)
                {
                    DeathLocationLoc = new Location();
                    DeathLocationLoc.Id = Guid.NewGuid().ToString();
                    DeathLocationLoc.Meta = new Meta();
                    string[] deathlocation_profile = { ProfileURL.DeathLocation };
                    DeathLocationLoc.Meta.Profile = deathlocation_profile;
                    DeathLocationLoc.Description = value;
                    DeathLocationLoc.Type.Add(new CodeableConcept(CodeSystems.LocationType, "death", "death location", null));
                    // LinkObservationToLocation(DeathDateObs, DeathLocationLoc);
                    AddReferenceToComposition(DeathLocationLoc.Id, "DeathInvestigation");
                    Bundle.AddResourceEntry(DeathLocationLoc, "urn:uuid:" + DeathLocationLoc.Id);
                }
                else
                {
                    DeathLocationLoc.Description = value;
                }
            }
        }

        /// <summary>Type of Death Location</summary>
        /// <value>type of death location. A Dictionary representing a code, containing the following key/value pairs:
        /// <para>"code" - the code</para>
        /// <para>"system" - the code system this code belongs to</para>
        /// <para>"display" - a human readable meaning of the code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; code = new Dictionary&lt;string, string&gt;();</para>
        /// <para>code.Add("code", "16983000");</para>
        /// <para>code.Add("system", CodeSystems.SCT);</para>
        /// <para>code.Add("display", "Death in hospital");</para>
        /// <para>ExampleDeathRecord.DeathLocationType = code;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Death Location Type: {ExampleDeathRecord.DeathLocationType['display']}");</para>
        /// </example>
        [Property("Death Location Type", Property.Types.Dictionary, "Death Investigation", "Type of Death Location.", true, IGURL.DeathDate, false, 19)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [PropertyParam("text", "Additional descriptive text.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='81956-5')", "component")]
        public Dictionary<string, string> DeathLocationType
        {
            get
            {
                if (DeathDateObs != null && DeathDateObs.Component.Count > 0) // if there is a value for death location type, return it
                {
                    var placeComp = DeathDateObs.Component.FirstOrDefault(entry => ((Observation.ComponentComponent)entry).Code != null
                         && ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault() != null && ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault().Code == "58332-8");
                    if (placeComp != null && placeComp.Value != null && placeComp.Value as CodeableConcept != null)
                    {
                        return (CodeableConceptToDict((CodeableConcept)placeComp.Value));
                    }
                }
                return EmptyCodeableDict();
            }
            set
            {
                if (IsDictEmptyOrDefault(value) && DeathDateObs == null)
                {
                    return;
                }
                if (DeathDateObs == null)
                {
                    CreateDeathDateObs(); // Create it
                }

                // Find correct component; if doesn't exist add another
                var placeComp = DeathDateObs.Component.FirstOrDefault(entry => ((Observation.ComponentComponent)entry).Code != null
                         && ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault() != null && ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault().Code == "58332-8");
                if (placeComp != null)
                {
                    ((Observation.ComponentComponent)placeComp).Value = DictToCodeableConcept(value);
                }
                else
                {
                    Observation.ComponentComponent component = new Observation.ComponentComponent();
                    component.Code = new CodeableConcept(CodeSystems.LOINC, "58332-8", "Location of death", null);
                    component.Value = DictToCodeableConcept(value);
                    DeathDateObs.Component.Add(component);
                }
            }

        }

        /// <summary>Type of Death Location Helper</summary>
        /// <value>type of death location code.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.DeathLocationTypeHelper = VRDR.ValueSets.PlaceOfDeath.Death_In_Home;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Death Location Type: {ExampleDeathRecord.DeathLocationTypeHelper}");</para>
        /// </example>
        [Property("Death Location Type Helper", Property.Types.String, "Death Investigation", "Type of Death Location.", false, IGURL.DeathDate, false, 19)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='81956-5')", "component")]
        public string DeathLocationTypeHelper
        {
            get
            {
                if (DeathLocationType != null && DeathLocationType.ContainsKey("code") && !String.IsNullOrWhiteSpace(DeathLocationType["code"]))
                {
                    return DeathLocationType["code"];
                }
                return null;
            }
            set
            {
                if(!String.IsNullOrWhiteSpace(value))
                {
                    SetCodeValue("DeathLocationType", value, VRDR.ValueSets.PlaceOfDeath.Codes);
                }
            }
        }

        /// <summary>Age At Death.</summary>
        /// <value>decedent's age at time of death. A Dictionary representing a length of time,
        /// containing the following key/value pairs: </value>
        /// <para>"value" - the quantity value, structured as valueQuantity.value</para>
        /// <para>"code" - the unit a PHIN VADS code set UnitsOfAge, structed as valueQuantity.code
        ///   USE: http://hl7.org/fhir/us/vrdr/STU2/StructureDefinition-vrdr-decedent-age.html </para>
        /// <para>"system" - OPTIONAL: from the example page http://hl7.org/fhir/us/vrdr/Observation-DecedentAge-Example1.json.html</para>
        /// <para>"unit" - OPTIONAL: from the example page http://hl7.org/fhir/us/vrdr/Observation-DecedentAge-Example1.json.html</para>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; age = new Dictionary&lt;string, string&gt;();</para>
        /// <para>age.Add("value", "100");</para>
        /// <para>age.Add("code", "a"); // e.g. "min" = minutes, "d" = days, "h" = hours, "mo" = months, "a" = years, "UNK" = unknown</para>
        /// <para>age.Add("system", "http://unitsofmeasure.org");</para>
        /// <para>age.Add("unit", "years");</para>
        /// <para>ExampleDeathRecord.AgeAtDeath = age;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Age At Death: {ExampleDeathRecord.AgeAtDeath['value']} years");</para>
        /// </example>
        [Property("Age At Death", Property.Types.Dictionary, "Decedent Demographics", "Age At Death.", true, IGURL.DecedentAge, true, 2)]
        [PropertyParam("value", "The quantity value.")]
        [PropertyParam("code", "The unit type, from UnitsOfAge ValueSet.")]
        [PropertyParam("system", "OPTIONAL: The coding system.")]
        [PropertyParam("unit", "OPTIONAL: The unit description.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='39016-1')", "")]
        public Dictionary<string, string> AgeAtDeath
        {
            get
            {
                if (AgeAtDeathObs?.Value != null)
                {
                    Dictionary<string, string> age = new Dictionary<string, string>();
                    Quantity quantity = (Quantity)AgeAtDeathObs.Value;
                    age.Add("value", quantity.Value == null ? "" : Convert.ToString(quantity.Value));
                    age.Add("code", quantity.Code == null ? "" : quantity.Code);
                    age.Add("system", quantity.System == null ? "" : quantity.System);
                    age.Add("unit", quantity.Unit== null ? "" : quantity.Unit);
                    return age;
                }
                return new Dictionary<string, string>() { { "value", "" }, { "code", "" }, { "system", null }, { "unit", null} };
            }
            set
            {
                string extractedValue = GetValue(value, "value");
                string extractedCode = GetValue(value, "code"); ;
                string extractedSystem = GetValue(value, "system");
                string extractedUnit = GetValue(value, "unit");
                if ((extractedValue == null && extractedCode == null && extractedUnit == null && extractedSystem == null)) // if there is nothing to do, do nothing.
                {
                    return;
                }
                if (AgeAtDeathObs == null)
                {
                    CreateAgeAtDeathObs();
                }
                Quantity quantity = (Quantity)AgeAtDeathObs.Value;

                if (extractedValue != null)
                {
                    quantity.Value = Convert.ToDecimal(extractedValue);
                }
                if (extractedCode != null)
                {
                    quantity.Code = extractedCode;
                }
                if (extractedSystem != null)
                {
                    quantity.System = extractedSystem;
                }
                if (extractedUnit != null)
                {
                    quantity.Unit = extractedUnit;
                }
                AgeAtDeathObs.Value = (Quantity)quantity;
            }
        }

        /// <summary>Get Age At Death For Code</summary>
        /// <value>Private helper method to get the age at death for a given code.</value>
        private int? _getAgeAtDeathForCode(string code)
        {
            if (AgeAtDeath["code"] == code && AgeAtDeath["value"] != null)
            {
                return Convert.ToInt32(AgeAtDeath["value"]);
            }
            else
            {
                return null;
            }
        }

        /// <summary>Set Age At Death For Code</summary>
        /// <value>Private helper method to set the age at death for a given code and value.</value>
        private void _setAgeAtDeathForCode(string code, int? value)
        {
            if (value != null)
            {
                AgeAtDeath = new Dictionary<string, string>() {
                    { "value", Convert.ToString(value) },
                    { "code", code }
                };
            }
        }

        /// <summary>Age At Death Years Helper</summary>
        /// <value>Set decedent's age at time of death in years.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.AgeAtDeathYears = 100;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Age At Death: {ExampleDeathRecord.AgeAtDeathYears} years");</para>
        /// </example>
        [Property("Age At Death Years Helper", Property.Types.Int32, "Decedent Demographics", "Age At Death in Years.", false, IGURL.DecedentAge, true, 2)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='39016-1')", "")]
        public int? AgeAtDeathYears
        {
            get => _getAgeAtDeathForCode("a");
            set => _setAgeAtDeathForCode("a", value);
        }

        /// <summary>Age At Death Months Helper</summary>
        /// <value>Set decedent's age at time of death in months.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.AgeAtDeathMonths = 11;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Age At Death: {ExampleDeathRecord.AgeAtDeathMonths} months");</para>
        /// </example>
        [Property("Age At Death Months Helper", Property.Types.Int32, "Decedent Demographics", "Age At Death in Months.", false, IGURL.DecedentAge, true, 2)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='39016-1')", "")]
        public int? AgeAtDeathMonths
        {
            get => _getAgeAtDeathForCode("mo");
            set => _setAgeAtDeathForCode("mo", value);
        }

        /// <summary>Age At Death Days Helper</summary>
        /// <value>Set decedent's age at time of death in days.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.AgeAtDeathDays = 11;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Age At Death: {ExampleDeathRecord.AgeAtDeathDays} days");</para>
        /// </example>
        [Property("Age At Death Days Helper", Property.Types.Int32, "Decedent Demographics", "Age At Death in Days.", false, IGURL.DecedentAge, true, 2)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='39016-1')", "")]
        public int? AgeAtDeathDays
        {
            get => _getAgeAtDeathForCode("d");
            set => _setAgeAtDeathForCode("d", value);
        }

        /// <summary>Age At Death Hours Helper</summary>
        /// <value>Set decedent's age at time of death in hours.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.AgeAtDeathHours = 11;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Age At Death: {ExampleDeathRecord.AgeAtDeathHours} hours");</para>
        /// </example>
        [Property("Age At Death Hours Helper", Property.Types.Int32, "Decedent Demographics", "Age At Death in Hours.", false, IGURL.DecedentAge, true, 2)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='39016-1')", "")]
        public int? AgeAtDeathHours
        {
            get => _getAgeAtDeathForCode("h");
            set => _setAgeAtDeathForCode("h", value);
        }

        /// <summary>Age At Death Minutes Helper</summary>
        /// <value>Set decedent's age at time of death in minutes.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.AgeAtDeathMinutes = 11;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Age At Death: {ExampleDeathRecord.AgeAtDeathMinutes} minutes");</para>
        /// </example>
        [Property("Age At Death Minutes Helper", Property.Types.Int32, "Decedent Demographics", "Age At Death in Minutes.", false, IGURL.DecedentAge, true, 2)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='39016-1')", "")]
        public int? AgeAtDeathMinutes
        {
            get => _getAgeAtDeathForCode("min");
            set => _setAgeAtDeathForCode("min", value);
        }

        /// <summary>Decedent's Age At Death Edit Flag.</summary>
        /// <value>the decedent's age at death edit flag. A Dictionary representing a code, containing the following key/value pairs:
        /// <para>"code" - the code</para>
        /// <para>"system" - the code system this code belongs to</para>
        /// <para>"display" - a human readable meaning of the code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; ageEdit = new Dictionary&lt;string, string&gt;();</para>
        /// <para>ageEdit.Add("code", "0");</para>
        /// <para>ageEdit.Add("system", CodeSystems.BypassEditFlag);</para>
        /// <para>ageEdit.Add("display", "Edit Passed");</para>
        /// <para>ExampleDeathRecord.AgeAtDeathEditFlag = ageEdit;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Age At Death Edit Flag: {ExampleDeathRecord.AgeAtDeathEditFlag['display']}");</para>
        /// </example>
        [Property("Age At Death Edit Flag", Property.Types.Dictionary, "Decedent Demographics", "Age At Death Edit Flag.", true, IGURL.DecedentAge, true, 2)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [PropertyParam("text", "Additional descriptive text.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='39016-1').value.extension", "")]
        public Dictionary<string, string> AgeAtDeathEditFlag
        {
            get
            {
                Extension editFlag = AgeAtDeathObs?.Value?.Extension.FirstOrDefault(ext => ext.Url == ExtensionURL.BypassEditFlag);
                if (editFlag != null && editFlag.Value != null && editFlag.Value.GetType() == typeof(CodeableConcept))
                {
                    return CodeableConceptToDict((CodeableConcept)editFlag.Value);
                }
                return EmptyCodeableDict();
            }
            set
            {
                if (IsDictEmptyOrDefault(value) && AgeAtDeathObs == null)
                {
                    return;
                }
                if (AgeAtDeathObs == null)
                {
                    CreateAgeAtDeathObs();
                }
                AgeAtDeathObs.Value.Extension.RemoveAll(ext => ext.Url == ExtensionURL.BypassEditFlag);
                if (IsDictEmptyOrDefault(value))
                {
                    return;
                }
                Extension editFlag = new Extension(ExtensionURL.BypassEditFlag, DictToCodeableConcept(value));
                AgeAtDeathObs.Value.Extension.Add(editFlag);
            }
        }

        /// <summary>
        /// Age at Death Edit Bypass Flag Helper
        /// </summary>
        [Property("Age At Death Edit Flag Helper", Property.Types.String, "Decedent Demographics", "Age At Death Edit Flag Helper.", false, IGURL.DecedentAge, true, 2)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='39016-1').value.extension", "")]
        public String AgeAtDeathEditFlagHelper
        {
            get
            {
                return AgeAtDeathEditFlag.ContainsKey("code") && !String.IsNullOrWhiteSpace(AgeAtDeathEditFlag["code"]) ? AgeAtDeathEditFlag["code"] : null;
            }
            set
            {
                if(!String.IsNullOrWhiteSpace(value))
                {
                    SetCodeValue("AgeAtDeathEditFlag", value, VRDR.ValueSets.EditBypass01.Codes);
                }
            }
        }


        /// <summary>Pregnancy Status At Death.</summary>
        /// <value>pregnancy status at death. A Dictionary representing a code, containing the following key/value pairs:
        /// <para>"code" - the code</para>
        /// <para>"system" - the code system this code belongs to</para>
        /// <para>"display" - a human readable meaning of the code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; code = new Dictionary&lt;string, string&gt;();</para>
        /// <para>code.Add("code", "1");</para>
        /// <para>code.Add("system", VRDR.CodeSystems.PregnancyStatus);</para>
        /// <para>code.Add("display", "Not pregnant within past year");</para>
        /// <para>ExampleDeathRecord.PregnancyObs = code;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Pregnancy Status: {ExampleDeathRecord.PregnancyObs['display']}");</para>
        /// </example>
        [Property("Pregnancy Status", Property.Types.Dictionary, "Death Investigation", "Pregnancy Status At Death.", true, IGURL.DecedentPregnancyStatus, true, 33)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='69442-2')", "")]
        public Dictionary<string, string> PregnancyStatus
        {
            get
            {
                if (PregnancyObs != null && PregnancyObs.Value != null && PregnancyObs.Value as CodeableConcept != null)
                {
                    return CodeableConceptToDict((CodeableConcept)PregnancyObs.Value);
                }
                return EmptyCodeableDict();
            }
            set
            {
                if (IsDictEmptyOrDefault(value) && PregnancyObs == null)
                {
                    return;
                }
                if (PregnancyObs == null)
                {
                    CreatePregnancyObs();
                    PregnancyObs.Value = DictToCodeableConcept(value);
                }
                else
                {
                    // Need to keep any existing extension that could be there
                    List<Extension> extensions = PregnancyObs.Value.Extension.FindAll(e => true);
                    PregnancyObs.Value = DictToCodeableConcept(value);
                    PregnancyObs.Value.Extension.AddRange(extensions);
                }
            }
        }

        /// <summary>Pregnancy Status At Death Helper.</summary>
        /// <value>pregnancy status at death.
        /// <para>"code" - the code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.PregnancyStatusHelper = ValueSets.PregnancyStatus.Not_Pregnant_Within_Past_Year;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Pregnancy Status: {ExampleDeathRecord.PregnancyStatusHelper}");</para>
        /// </example>
        [Property("Pregnancy Status Helper", Property.Types.String, "Death Investigation", "Pregnancy Status At Death.", false, IGURL.DecedentPregnancyStatus, true, 33)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='69442-2')", "")]
        public string PregnancyStatusHelper
        {
            get
            {
                if (PregnancyStatus.ContainsKey("code") && !String.IsNullOrWhiteSpace(PregnancyStatus["code"]))
                {
                    return PregnancyStatus["code"];
                }
                return null;
            }
            set
            {
                if(!String.IsNullOrWhiteSpace(value))
                {
                    SetCodeValue("PregnancyStatus", value, VRDR.ValueSets.PregnancyStatus.Codes);
                }
            }
        }
        /// <summary>Decedent's Pregnancy Status at Death Edit Flag.</summary>
        /// <value>the decedent's pregnancy status at death edit flag. A Dictionary representing a code, containing the following key/value pairs:
        /// <para>"code" - the code</para>
        /// <para>"system" - the code system this code belongs to</para>
        /// <para>"display" - a human readable meaning of the code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; elevel = new Dictionary&lt;string, string&gt;();</para>
        /// <para>elevel.Add("code", "0");</para>
        /// <para>elevel.Add("system", CodeSystems.BypassEditFlag);</para>
        /// <para>elevel.Add("display", "Edit Passed");</para>
        /// <para>ExampleDeathRecord.PregnancyStatusEditFlag = elevel;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Pregnancy Status Edit Flag: {ExampleDeathRecord.PregnancyStatusEditFlag['display']}");</para>
        /// </example>
        [Property("Pregnancy Status Edit Flag", Property.Types.Dictionary, "Decedent Demographics", "Decedent's Pregnancy Status at Death Edit Flag.", true, IGURL.DecedentPregnancyStatus, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='69442-2')", "")]
        public Dictionary<string, string> PregnancyStatusEditFlag
        {
            get
            {
                if (PregnancyObs != null && PregnancyObs.Value != null && PregnancyObs.Value.Extension != null)
                {
                    Extension editFlag = PregnancyObs.Value.Extension.FirstOrDefault(extension => extension.Url == ExtensionURL.BypassEditFlag);
                    if (editFlag != null)
                    {
                        return CodeableConceptToDict((CodeableConcept)editFlag.Value);
                    }
                }
                return EmptyCodeableDict();
            }
            set
            {
                if (IsDictEmptyOrDefault(value) && PregnancyObs == null)
                {
                    return;
                }
                if (PregnancyObs == null)
                {
                    CreatePregnancyObs();
                }
                if (PregnancyObs.Value != null && PregnancyObs.Value.Extension != null)
                {
                    PregnancyObs.Value.Extension.RemoveAll(ext => ext.Url == ExtensionURL.BypassEditFlag);
                }
                if (PregnancyObs.Value == null)
                {
                    PregnancyObs.Value = new CodeableConcept();
                }
                Extension editFlag = new Extension(ExtensionURL.BypassEditFlag, DictToCodeableConcept(value));
                PregnancyObs.Value.Extension.Add(editFlag);
            }
        }

        /// <summary>Decedent's Pregnancy Status Edit Flag Helper</summary>
        /// <value>Decedent's Pregnancy Status Edit Flag.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.DecedentPregnancyStatusEditFlag = VRDR.ValueSets.EditBypass012.EditPassed;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Decedent's Pregnancy Status Edit Flag: {ExampleDeathRecord.PregnancyStatusHelperEditFlag}");</para>
        /// </example>
        [Property("Pregnancy Status Edit Flag Helper", Property.Types.String, "Decedent Demographics", "Decedent's Pregnancy Status Edit Flag Helper.", false, IGURL.DecedentPregnancyStatus, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='69442-2')", "")]
        public string PregnancyStatusEditFlagHelper
        {
            get
            {
                if (PregnancyStatusEditFlag.ContainsKey("code") && !String.IsNullOrWhiteSpace(PregnancyStatusEditFlag["code"]))
                {
                    return PregnancyStatusEditFlag["code"];
                }
                return null;
            }
            set
            {
                if(!String.IsNullOrWhiteSpace(value))
                {
                    SetCodeValue("PregnancyStatusEditFlag", value, VRDR.ValueSets.EditBypass012.Codes);
                }
            }
        }


        /// <summary>Examiner Contacted.</summary>
        /// <value>if a medical examiner was contacted.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; ec = new Dictionary&lt;string, string&gt;();</para>
        /// <para>within.Add("code", "Y");</para>
        /// <para>within.Add("system", CodeSystems.PH_YesNo_HL7_2x);</para>
        /// <para>within.Add("display", "Yes");</para>
        /// <para>ExampleDeathRecord.ExaminerContacted = ec;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Examiner Contacted: {ExampleDeathRecord.ExaminerContacted['display']}");</para>
        /// </example>
        [Property("Examiner Contacted", Property.Types.Dictionary, "Death Investigation", "Examiner Contacted.", true, IGURL.ExaminerContacted, true, 26)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='74497-9')", "")]
        public Dictionary<string, string> ExaminerContacted
        {
            get
            {
                if (ExaminerContactedObs != null && ExaminerContactedObs.Value != null && ExaminerContactedObs.Value as CodeableConcept != null)
                {
                    CodeableConcept cc = (CodeableConcept)ExaminerContactedObs.Value;
                    return CodeableConceptToDict(cc);
                }
                return EmptyCodeableDict();
            }
            set
            {
                if (IsDictEmptyOrDefault(value) && ExaminerContactedObs == null)
                {
                    return;
                }
                var contactedCoding = DictToCodeableConcept(value);
                if (ExaminerContactedObs == null)
                {
                    ExaminerContactedObs = new Observation();
                    ExaminerContactedObs.Id = Guid.NewGuid().ToString();
                    ExaminerContactedObs.Meta = new Meta();
                    string[] ec_profile = { ProfileURL.ExaminerContacted };
                    ExaminerContactedObs.Meta.Profile = ec_profile;
                    ExaminerContactedObs.Status = ObservationStatus.Final;
                    ExaminerContactedObs.Code = new CodeableConcept(CodeSystems.LOINC, "74497-9", "Medical examiner or coroner was contacted [US Standard Certificate of Death]", null);
                    ExaminerContactedObs.Subject = new ResourceReference("urn:uuid:" + Decedent.Id);
                    ExaminerContactedObs.Value = contactedCoding;
                    AddReferenceToComposition(ExaminerContactedObs.Id, "DeathInvestigation");
                    Bundle.AddResourceEntry(ExaminerContactedObs, "urn:uuid:" + ExaminerContactedObs.Id);
                }
                else
                {
                    ExaminerContactedObs.Value = contactedCoding;
                }
            }
        }

        /// <summary>Examiner Contacted Helper. This is a convenience method, to access the code use ExaminerContacted instead.</summary>
        /// <value>if a medical examiner was contacted. A null value indicates "unknown".</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.ExaminerContactedHelper = "N"</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Examiner Contacted: {ExampleDeathRecord.ExaminerContactedHelper}");</para>
        /// </example>
        [Property("Examiner Contacted Helper", Property.Types.String, "Death Investigation", "Examiner Contacted.", false, IGURL.ExaminerContacted, true, 27)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='74497-9')", "")]
        public string ExaminerContactedHelper
        {
            get
            {
                if (ExaminerContacted.ContainsKey("code") && !String.IsNullOrWhiteSpace(ExaminerContacted["code"]))
                {
                    return ExaminerContacted["code"];
                }
                return null;
            }
            set
            {
                if(!String.IsNullOrWhiteSpace(value))
                {
                    SetCodeValue("ExaminerContacted", value, VRDR.ValueSets.YesNoUnknown.Codes);
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
        /// <para>address.Add("addressState", "MA");</para>
        /// <para>address.Add("addressZip", "12345");</para>
        /// <para>address.Add("addressCountry", "US");</para>
        /// <para>ExampleDeathRecord.InjuryLocationAddress = address;</para>
        /// <para>// Getter:</para>
        /// <para>foreach(var pair in ExampleDeathRecord.InjuryLocationAddress)</para>
        /// <para>{</para>
        /// <para>  Console.WriteLine($"\InjuryLocationAddress key: {pair.Key}: value: {pair.Value}");</para>
        /// <para>};</para>
        /// </example>
        [Property("Injury Location Address", Property.Types.Dictionary, "Death Investigation", "Location of Injury.", true, IGURL.InjuryLocation, true, 34)]
        [PropertyParam("addressLine1", "address, line one")]
        [PropertyParam("addressLine2", "address, line two")]
        [PropertyParam("addressCity", "address, city")]
        [PropertyParam("addressCityC", "address, city code")]
        [PropertyParam("addressCounty", "address, county")]
        [PropertyParam("addressCountyC", "address, county code")]
        [PropertyParam("addressState", "address, state")]
        [PropertyParam("addressZip", "address, zip")]
        [PropertyParam("addressCountry", "address, country")]
        [FHIRPath("Bundle.entry.resource.where($this is Location).where(type.coding.code='injury')", "address")]
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
                if (value == null && InjuryLocationLoc == null)
                {
                    return;
                }
                if (InjuryLocationLoc == null)
                {
                    CreateInjuryLocationLoc();
                    //LinkObservationToLocation(InjuryIncidentObs, InjuryLocationLoc);
                }

                InjuryLocationLoc.Address = DictToAddress(value);
            }
        }

        /// <summary>Lattitude of Injury Location.</summary>
        /// <value>tLattitude of Injury Location.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.InjuryLocationLattitude = "37.88888" ;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Injury Location Lattitude: {ExampleDeathRecord.InjuryLocationLattitude}");</para>
        /// </example>
        [Property("Injury Location Latitude", Property.Types.String, "Death Investigation", "Injury Location Lattitude.", true, IGURL.InjuryLocation, false, 17)]
        [FHIRPath("Bundle.entry.resource.where($this is Location).where(type.coding.code='injury')", "position")]
        public string InjuryLocationLatitude
        {
            get
            {
                if (InjuryLocationLoc != null && InjuryLocationLoc.Position != null)
                {
                    return (InjuryLocationLoc.Position.Latitude).ToString();
                }
                return null;
            }
            set
            {
                if (String.IsNullOrWhiteSpace(value)) {
                    return;
                }
                if (value == null && InjuryLocationLoc == null)
                {
                    return;
                }
                if (InjuryLocationLoc == null)
                {
                    CreateInjuryLocationLoc();
                }
                if (InjuryLocationLoc.Position == null)
                {
                    InjuryLocationLoc.Position = new Location.PositionComponent();
                }
                InjuryLocationLoc.Position.Latitude = Convert.ToDecimal(value);
            }
        }

        /// <summary>Longitude of Injury Location.</summary>
        /// <value>Longitude of Injury Location.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.InjuryLocationLongitude = "-50.000" ;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Injury Location Longitude: {ExampleDeathRecord.InjuryLocationLongitude}");</para>
        /// </example>
        [Property("Injury Location Longitude", Property.Types.String, "Death Investigation", "Injury Location Lattitude.", true, IGURL.DeathLocation, false, 17)]
        [FHIRPath("Bundle.entry.resource.where($this is Location).where(type.coding.code='injury')", "position")]
        public string InjuryLocationLongitude
        {
            get
            {
                if (InjuryLocationLoc != null && InjuryLocationLoc.Position != null)
                {
                    return (InjuryLocationLoc.Position.Longitude).ToString();
                }
                return null;
            }
            set
            {
                if (String.IsNullOrWhiteSpace(value)) {
                    return;
                }
                if (value == null && InjuryLocationLoc == null)
                {
                    return;
                }
                if (InjuryLocationLoc == null)
                {
                    CreateInjuryLocationLoc();
                }
                if (InjuryLocationLoc.Position == null)
                {
                    InjuryLocationLoc.Position = new Location.PositionComponent();
                }
                InjuryLocationLoc.Position.Longitude = Convert.ToDecimal(value);
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
        [Property("Injury Location Name", Property.Types.String, "Death Investigation", "Name of Injury Location.", true, IGURL.InjuryLocation, true, 35)]
        [FHIRPath("Bundle.entry.resource.where($this is Location).where(type.coding.code='injury')", "name")]
        public string InjuryLocationName
        {
            get
            {
                if (InjuryLocationLoc != null && InjuryLocationLoc.Name != null && InjuryLocationLoc.Name != DeathRecord.BlankPlaceholder)
                {
                    return InjuryLocationLoc.Name;
                }
                return null;
            }
            set
            {
                if (String.IsNullOrWhiteSpace(value) && InjuryLocationLoc == null)
                {
                    return;
                }
                if (InjuryLocationLoc == null)
                {
                    CreateInjuryLocationLoc();
                    // LinkObservationToLocation(InjuryIncidentObs, InjuryLocationLoc);
                }
                if (!String.IsNullOrWhiteSpace(value))
                {
                    InjuryLocationLoc.Name = value;
                }
                else
                {
                    InjuryLocationLoc.Name = DeathRecord.BlankPlaceholder; // We cannot have a blank string, but the field is required to be present
                }
            }
        }


        /// <summary>Set an emerging issue value, creating an empty EmergingIssues Observation as needed.</summary>
        private void SetEmergingIssue(string identifier, string value)
        {
            if (String.IsNullOrEmpty(value) && EmergingIssues == null)
            {
                return;
            }
            if (EmergingIssues == null)
            {
                EmergingIssues = new Observation();
                EmergingIssues.Id = Guid.NewGuid().ToString();
                EmergingIssues.Meta = new Meta();
                string[] tb_profile = { ProfileURL.EmergingIssues };
                EmergingIssues.Meta.Profile = tb_profile;
                EmergingIssues.Status = ObservationStatus.Final;
                EmergingIssues.Code = new CodeableConcept(CodeSystems.ObservationCode, "emergingissues", "NCHS-required Parameter Slots for Emerging Issues", null);
                EmergingIssues.Subject = new ResourceReference("urn:uuid:" + Decedent.Id);
                AddReferenceToComposition(EmergingIssues.Id, "DecedentDemographics");
                Bundle.AddResourceEntry(EmergingIssues, "urn:uuid:" + EmergingIssues.Id);
            }
            // Remove existing component (if it exists) and add an appropriate component.
            EmergingIssues.Component.RemoveAll(cmp => cmp.Code != null && cmp.Code.Coding != null && cmp.Code.Coding.Count() > 0 && cmp.Code.Coding.First().Code == identifier);
            Observation.ComponentComponent component = new Observation.ComponentComponent();
            component.Code = new CodeableConcept(CodeSystems.ComponentCode, identifier, null, null);
            component.Value = new FhirString(value);
            EmergingIssues.Component.Add(component);
        }

        /// <summary>Get an emerging issue value.</summary>
        private string GetEmergingIssue(string identifier)
        {
            if (EmergingIssues == null)
            {
                return null;
            }
            // Remove existing component (if it exists) and add an appropriate component.
            Observation.ComponentComponent issue = EmergingIssues.Component.FirstOrDefault(c => c.Code.Coding[0].Code == identifier);
            if (issue != null && issue.Value != null && issue.Value as FhirString != null)
            {
                return issue.Value.ToString();
            }
            return null;
        }


        /// <summary>Emerging Issue Field Length 1 Number 1</summary>
        /// <value>the emerging issue value</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.EmergingIssue1_1 = "X";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Emerging Issue Value: {ExampleDeathRecord.EmergingIssue1_1}");</para>
        /// </example>
        [Property("Emerging Issue Field Length 1 Number 1", Property.Types.String, "Decedent Demographics", "One-Byte Field 1", true, IGURL.EmergingIssues, false, 50)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='emergingissues')", "")]
        public string EmergingIssue1_1
        {
            get
            {
                return GetEmergingIssue("EmergingIssue1_1");
            }
            set
            {
                if(!String.IsNullOrWhiteSpace(value))
                {
                    SetEmergingIssue("EmergingIssue1_1", value);
                }
            }
        }

        /// <summary>Emerging Issue Field Length 1 Number 2</summary>
        /// <value>the emerging issue value</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.EmergingIssue1_2 = "X";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Emerging Issue Value: {ExampleDeathRecord.EmergingIssue1_2}");</para>
        /// </example>
        [Property("Emerging Issue Field Length 1 Number 2", Property.Types.String, "Decedent Demographics", "1-Byte Field 2", true, IGURL.EmergingIssues, false, 50)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='emergingissues')", "")]
        public string EmergingIssue1_2
        {
            get
            {
                return GetEmergingIssue("EmergingIssue1_2");
            }
            set
            {
                if(!String.IsNullOrWhiteSpace(value))
                {
                    SetEmergingIssue("EmergingIssue1_2", value);
                }
            }
        }

        /// <summary>Emerging Issue Field Length 1 Number 3</summary>
        /// <value>the emerging issue value</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.EmergingIssue1_3 = "X";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Emerging Issue Value: {ExampleDeathRecord.EmergingIssue1_3}");</para>
        /// </example>
        [Property("Emerging Issue Field Length 1 Number 3", Property.Types.String, "Decedent Demographics", "1-Byte Field 3", true, IGURL.EmergingIssues, false, 50)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='emergingissues')", "")]

        public string EmergingIssue1_3
        {
            get
            {
                return GetEmergingIssue("EmergingIssue1_3");
            }
            set
            {
                if(!String.IsNullOrWhiteSpace(value))
                {
                    SetEmergingIssue("EmergingIssue1_3", value);
                }
            }
        }

        /// <summary>Emerging Issue Field Length 1 Number 4</summary>
        /// <value>the emerging issue value</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.EmergingIssue1_4 = "X";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Emerging Issue Value: {ExampleDeathRecord.EmergingIssue1_4}");</para>
        /// </example>
        [Property("Emerging Issue Field Length 1 Number 4", Property.Types.String, "Decedent Demographics", "1-Byte Field 4", true, IGURL.EmergingIssues, false, 50)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='emergingissues')", "")]

        public string EmergingIssue1_4
        {
            get
            {
                return GetEmergingIssue("EmergingIssue1_4");
            }
            set
            {
                if(!String.IsNullOrWhiteSpace(value))
                {
                    SetEmergingIssue("EmergingIssue1_4", value);
                }
            }
        }

        /// <summary>Emerging Issue Field Length 1 Number 5</summary>
        /// <value>the emerging issue value</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.EmergingIssue1_5 = "X";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Emerging Issue Value: {ExampleDeathRecord.EmergingIssue1_5}");</para>
        /// </example>
        [Property("Emerging Issue Field Length 1 Number 5", Property.Types.String, "Decedent Demographics", "1-Byte Field 5", true, IGURL.EmergingIssues, false, 50)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='emergingissues')", "")]

        public string EmergingIssue1_5
        {
            get
            {
                return GetEmergingIssue("EmergingIssue1_5");
            }
            set
            {
                if(!String.IsNullOrWhiteSpace(value))
                {
                    SetEmergingIssue("EmergingIssue1_5", value);
                }
            }
        }

        /// <summary>Emerging Issue Field Length 1 Number 6</summary>
        /// <value>the emerging issue value</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.EmergingIssue1_6 = "X";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Emerging Issue Value: {ExampleDeathRecord.EmergingIssue1_6}");</para>
        /// </example>
        [Property("Emerging Issue Field Length 1 Number 6", Property.Types.String, "Decedent Demographics", "1-Byte Field 6", true, IGURL.EmergingIssues, false, 50)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='emergingissues')", "")]

        public string EmergingIssue1_6
        {
            get
            {
                return GetEmergingIssue("EmergingIssue1_6");
            }
            set
            {
                if(!String.IsNullOrWhiteSpace(value))
                {
                    SetEmergingIssue("EmergingIssue1_6", value);
                }
            }
        }

        /// <summary>Emerging Issue Field Length 8 Number 1</summary>
        /// <value>the emerging issue value</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.EmergingIssue8_1 = "XXXXXXXX";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Emerging Issue Value: {ExampleDeathRecord.EmergingIssue8_1}");</para>
        /// </example>
        [Property("Emerging Issue Field Length 8 Number 1", Property.Types.String, "Decedent Demographics", "8-Byte Field 1", true, IGURL.EmergingIssues, false, 50)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='emergingissues')", "")]

        public string EmergingIssue8_1
        {
            get
            {
                return GetEmergingIssue("EmergingIssue8_1");
            }
            set
            {
                if(!String.IsNullOrWhiteSpace(value))
                {
                    SetEmergingIssue("EmergingIssue8_1", value);
                }
            }
        }

        /// <summary>Emerging Issue Field Length 8 Number 2</summary>
        /// <value>the emerging issue value</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.EmergingIssue8_2 = "XXXXXXXX";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Emerging Issue Value: {ExampleDeathRecord.EmergingIssue8_2}");</para>
        /// </example>
        [Property("Emerging Issue Field Length 8 Number 2", Property.Types.String, "Decedent Demographics", "8-Byte Field 2", true, IGURL.EmergingIssues, false, 50)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='emergingissues')", "")]

        public string EmergingIssue8_2
        {
            get
            {
                return GetEmergingIssue("EmergingIssue8_2");
            }
            set
            {
                if(!String.IsNullOrWhiteSpace(value))
                {
                    SetEmergingIssue("EmergingIssue8_2", value);
                }
            }
        }

        /// <summary>Emerging Issue Field Length 8 Number 3</summary>
        /// <value>the emerging issue value</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.EmergingIssue8_3 = "XXXXXXXX";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Emerging Issue Value: {ExampleDeathRecord.EmergingIssue8_3}");</para>
        /// </example>
        [Property("Emerging Issue Field Length 8 Number 3", Property.Types.String, "Decedent Demographics", "8-Byte Field 3", true, IGURL.EmergingIssues, false, 50)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='emergingissues')", "")]

        public string EmergingIssue8_3
        {
            get
            {
                return GetEmergingIssue("EmergingIssue8_3");
            }
            set
            {
                if(!String.IsNullOrWhiteSpace(value))
                {
                    SetEmergingIssue("EmergingIssue8_3", value);
                }
            }
        }

        /// <summary>Emerging Issue Field Length 20</summary>
        /// <value>the emerging issue value</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.EmergingIssue20 = "XXXXXXXXXXXXXXXXXXXX";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Emerging Issue Value: {ExampleDeathRecord.EmergingIssue20}");</para>
        /// </example>
        [Property("Emerging Issue Field Length 20", Property.Types.String, "Decedent Demographics", "20-Byte Field", true, IGURL.EmergingIssues, false, 50)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='emergingissues')", "")]

        public string EmergingIssue20
        {
            get
            {
                return GetEmergingIssue("EmergingIssue20");
            }
            set
            {
                if(!String.IsNullOrWhiteSpace(value))
                {
                    SetEmergingIssue("EmergingIssue20", value);
                }
            }
        }


        /// <summary>Decedent's Year of Injury.</summary>
        /// <value>the decedent's year of injury, or -1 if explicitly unknown, or null if never specified</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.InjuryYear = 2018;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Decedent Year of Injury: {ExampleDeathRecord.InjuryYear}");</para>
        /// </example>
        [Property("InjuryYear", Property.Types.Int32, "Death Investigation", "Decedent's Year of Injury.", true, IGURL.InjuryIncident, true, 25)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='11374-6')", "")]
        public int? InjuryYear
        {
            get
            {
                if (InjuryIncidentObs != null && InjuryIncidentObs.Effective != null)
                {
                    return GetDateFragmentOrPartialDate(InjuryIncidentObs.Effective, ExtensionURL.DateYear);
                }
                return null;
            }
            set
            {
                if (value == null && InjuryIncidentObs == null)
                {
                    return;
                }
                if (InjuryIncidentObs == null)
                {
                    CreateInjuryIncidentObs();
                }
                SetPartialDate(InjuryIncidentObs.Effective.Extension.Find(ext => ext.Url == ExtensionURL.PartialDateTime), ExtensionURL.DateYear, value);
            }
        }

        /// <summary>Decedent's Month of Injury.</summary>
        /// <value>the decedent's month of injury, or -1 if explicitly unknown, or null if never specified</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.InjuryMonth = 7;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Decedent Month of Injury: {ExampleDeathRecord.InjuryMonth}");</para>
        /// </example>
        [Property("InjuryMonth", Property.Types.Int32, "Death Investigation", "Decedent's Month of Injury.", true, IGURL.InjuryIncident, true, 25)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='11374-6')", "")]
        public int? InjuryMonth
        {
            get
            {
                if (InjuryIncidentObs != null && InjuryIncidentObs.Effective != null)
                {
                    return GetDateFragmentOrPartialDate(InjuryIncidentObs.Effective, ExtensionURL.DateMonth);
                }
                return null;
            }
            set
            {
                if (value == null && InjuryIncidentObs == null)
                {
                    return;
                }
                if (InjuryIncidentObs == null)
                {
                    CreateInjuryIncidentObs();
                }
                SetPartialDate(InjuryIncidentObs.Effective.Extension.Find(ext => ext.Url == ExtensionURL.PartialDateTime), ExtensionURL.DateMonth, value);
            }
        }

        /// <summary>Decedent's Day of Injury.</summary>
        /// <value>the decedent's day of injury, or -1 if explicitly unknown, or null if never specified</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.InjuryDay = 22;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Decedent Day of Injury: {ExampleDeathRecord.InjuryDay}");</para>
        /// </example>
        [Property("InjuryDay", Property.Types.Int32, "Death Investigation", "Decedent's Day of Injury.", true, IGURL.InjuryIncident, true, 25)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='11374-6')", "")]
        public int? InjuryDay
        {
            get
            {
                if (InjuryIncidentObs != null && InjuryIncidentObs.Effective != null)
                {
                    return GetDateFragmentOrPartialDate(InjuryIncidentObs.Effective, ExtensionURL.DateDay);
                }
                return null;
            }
            set
            {
                if (value == null && InjuryIncidentObs == null)
                {
                    return;
                }
                if (InjuryIncidentObs == null)
                {
                    CreateInjuryIncidentObs();
                }
                SetPartialDate(InjuryIncidentObs.Effective.Extension.Find(ext => ext.Url == ExtensionURL.PartialDateTime), ExtensionURL.DateDay, value);
            }
        }

        /// <summary>Decedent's Time of Injury.</summary>
        /// <value>the decedent's time of injury, or "-1" if explicitly unknown, or null if never specified</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.InjuryTime = "07:15";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Decedent Time of Injury: {ExampleDeathRecord.InjuryTime}");</para>
        /// </example>
        [Property("InjuryTime", Property.Types.String, "Death Investigation", "Decedent's Time of Injury.", true, IGURL.InjuryIncident, true, 25)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='11374-6')", "")]
        public string InjuryTime
        {
            get
            {
                if (InjuryIncidentObs != null && InjuryIncidentObs.Effective != null)
                {
                    return GetTimeFragmentOrPartialTime(InjuryIncidentObs.Effective);
                }
                return null;
            }
            set
            {
                if (String.IsNullOrWhiteSpace(value) && InjuryIncidentObs == null)
                {
                    return;
                }
                if (InjuryIncidentObs == null)
                {
                    CreateInjuryIncidentObs();
                }
                SetPartialTime(InjuryIncidentObs.Effective.Extension.Find(ext => ext.Url == ExtensionURL.PartialDateTime), value);
            }
        }

        /// <summary>Date/Time of Injury.</summary>
        /// <value>the date and time of injury</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.InjuryDate = "2018-02-19T16:48:06-05:00";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Date of Injury: {ExampleDeathRecord.InjuryDate}");</para>
        /// </example>
        [Property("Injury Date/Time", Property.Types.StringDateTime, "Death Investigation", "Date/Time of Injury.", true, IGURL.InjuryIncident, true, 37)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='11374-6')", "")]
        public string InjuryDate
        {
            get
            {
                // We support this legacy API entrypoint via the new partial date and time entrypoints
                if (InjuryYear != null && InjuryYear != -1 && InjuryMonth != null && InjuryMonth != -1 && InjuryDay != null && InjuryDay != -1 && InjuryTime != null && InjuryTime != "-1")
                {
                    DateTimeOffset parsedTime;
                    if (DateTimeOffset.TryParse(InjuryTime, out parsedTime))
                    {
                        DateTimeOffset result = new DateTimeOffset((int)InjuryYear, (int)InjuryMonth, (int)InjuryDay, parsedTime.Hour, parsedTime.Minute, parsedTime.Second, TimeSpan.Zero);
                        return result.ToString("s");
                    }
                }
                else if (InjuryYear != null && InjuryYear != -1 && InjuryMonth != null && InjuryMonth != -1 && InjuryDay != null && InjuryDay != -1)
                {
                    DateTime result = new DateTime((int)InjuryYear, (int)InjuryMonth, (int)InjuryDay);
                    return result.ToString("s");
                }
                return null;
            }
            set
            {
                // We support this legacy API entrypoint via the new partial date and time entrypoints
                DateTimeOffset parsedTime;
                if (DateTimeOffset.TryParse(value, out parsedTime))
                {
                    InjuryYear = parsedTime.Year;
                    InjuryMonth = parsedTime.Month;
                    InjuryDay = parsedTime.Day;
                    TimeSpan timeSpan = new TimeSpan(0, parsedTime.Hour, parsedTime.Minute, parsedTime.Second);
                    InjuryTime = timeSpan.ToString(@"hh\:mm\:ss");
                }
            }
        }

        /// <summary>Description of Injury.</summary>
        /// <value>the description of the injury</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.InjuryDescription = "drug toxicity";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Injury Description: {ExampleDeathRecord.InjuryDescription}");</para>
        /// </example>
        [Property("Injury Description", Property.Types.String, "Death Investigation", "Description of Injury.", true, IGURL.InjuryIncident, true, 38)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='11374-6')", "")]
        public string InjuryDescription
        {
            get
            {
                CodeableConcept concept = InjuryIncidentObs?.Value as CodeableConcept;
                if (concept != null)
                {
                    return concept.Text;
                }
                return null;
            }
            set
            {
                if (String.IsNullOrWhiteSpace(value) && InjuryIncidentObs == null)
                {
                    return;
                }
                if (InjuryIncidentObs == null)
                {
                    CreateInjuryIncidentObs();
                }
                InjuryIncidentObs.Value = new CodeableConcept(null, null, null, value);
            }
        }

        /// <summary>Place of Injury Description.</summary>
        /// <value>the place of injury.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.InjuryPlaceDescription = "At home, in the kitchen";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Place of Injury Description: {ExampleDeathRecord.InjuryPlaceDescription}");</para>
        /// </example>
        [Property("Injury Place Description", Property.Types.String, "Death Investigation", "Place of Injury.", true, IGURL.InjuryIncident, true, 40)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='11374-6')", "")]
        public string InjuryPlaceDescription
        {
            get
            {
                // Find the component
                if (InjuryIncidentObs != null && InjuryIncidentObs.Component.Count > 0)
                {
                    // Find correct component
                    var placeComp = InjuryIncidentObs.Component.FirstOrDefault(entry => ((Observation.ComponentComponent)entry).Code != null
                         && ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault() != null && ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault().Code == "69450-5");
                    if (placeComp != null && placeComp.Value != null && placeComp.Value as CodeableConcept != null)
                    {
                        Dictionary<string, string> dict = CodeableConceptToDict((CodeableConcept)placeComp.Value);
                        if (dict.ContainsKey("text"))
                        {
                            return (dict["text"]);
                        }
                    }
                }
                return null;
            }
            set
            {
                if (String.IsNullOrWhiteSpace(value) && InjuryIncidentObs == null)
                {
                    return;
                }
                if (InjuryIncidentObs == null)
                {
                    CreateInjuryIncidentObs();
                }
                // Find correct component; if doesn't exist add another
                var placeComp = InjuryIncidentObs.Component.FirstOrDefault(entry => ((Observation.ComponentComponent)entry).Code != null
                && ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault() != null && ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault().Code == "69450-5");
                if (placeComp != null)
                {
                    ((Observation.ComponentComponent)placeComp).Value = new CodeableConcept(null, null, null, value);
                }
                else
                {
                    Observation.ComponentComponent component = new Observation.ComponentComponent();
                    component.Code = new CodeableConcept(CodeSystems.LOINC, "69450-5", "Place of injury Facility", null);
                    component.Value = new CodeableConcept(null, null, null, value);
                    InjuryIncidentObs.Component.Add(component);
                }
            }
        }

        /// <summary>Injury At Work?</summary>
        /// <value>did the injury occur at work? A Dictionary representing a code, containing the following key/value pairs:
        /// <para>"code" - the code</para>
        /// <para>"system" - the code system this code belongs to</para>
        /// <para>"display" - a human readable meaning of the code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; code = new Dictionary&lt;string, string&gt;();</para>
        /// <para>code.Add("code", "N");</para>
        /// <para>code.Add("system", CodeSystems.YesNo);</para>
        /// <para>code.Add("display", "No");</para>
        /// <para>ExampleDeathRecord.InjuryAtWork = code;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Injury At Work?: {ExampleDeathRecord.InjuryAtWork['display']}");</para>
        /// </example>
        [Property("Injury At Work?", Property.Types.Dictionary, "Death Investigation", "Did the injury occur at work?", true, IGURL.InjuryIncident, true, 41)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='11374-6')", "")]
        public Dictionary<string, string> InjuryAtWork
        {
            get
            {
                if (InjuryIncidentObs != null && InjuryIncidentObs.Component.Count > 0)
                {
                    // Find correct component
                    var placeComp = InjuryIncidentObs.Component.FirstOrDefault(entry => ((Observation.ComponentComponent)entry).Code != null
                    && ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault() != null && ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault().Code == "69444-8");
                    if (placeComp != null && placeComp.Value != null && placeComp.Value as CodeableConcept != null)
                    {
                        return CodeableConceptToDict((CodeableConcept)placeComp.Value);
                    }
                }
                return EmptyCodeableDict();
            }
            set
            {
                if (IsDictEmptyOrDefault(value) && InjuryIncidentObs == null)
                {
                    return;
                }
                if (InjuryIncidentObs == null)
                {
                    CreateInjuryIncidentObs();
                }

                // Find correct component; if doesn't exist add another
                var placeComp = InjuryIncidentObs.Component.FirstOrDefault(entry => ((Observation.ComponentComponent)entry).Code != null && ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault() != null && ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault().Code == "69444-8");
                if (placeComp != null)
                {
                    ((Observation.ComponentComponent)placeComp).Value = DictToCodeableConcept(value);
                }
                else
                {
                    Observation.ComponentComponent component = new Observation.ComponentComponent();
                    component.Code = new CodeableConcept(CodeSystems.LOINC, "69444-8", "Did death result from injury at work", null);
                    component.Value = DictToCodeableConcept(value);
                    InjuryIncidentObs.Component.Add(component);
                }

            }
        }

        /// <summary>Injury At Work Helper This is a convenience method, to access the code use the InjuryAtWork property instead.</summary>
        /// <value>did the injury occur at work? A null value indicates "not applicable".</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.InjuryAtWorkHelper = "Y"";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Injury At Work? : {ExampleDeathRecord.InjuryAtWorkHelper}");</para>
        /// </example>
        [Property("Injury At Work Helper", Property.Types.String, "Death Investigation", "Did the injury occur at work?", false, IGURL.InjuryIncident, true, 42)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='11374-6')", "")]
        public string InjuryAtWorkHelper
        {
            get
            {
                if (InjuryAtWork.ContainsKey("code") && !String.IsNullOrWhiteSpace(InjuryAtWork["code"]))
                {
                    return InjuryAtWork["code"];
                }
                return null;
            }
            set
            {
                if(!String.IsNullOrWhiteSpace(value))
                {
                    SetCodeValue("InjuryAtWork", value, VRDR.ValueSets.YesNoUnknownNotApplicable.Codes);
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
        /// <para>code.Add("code", "257500003");</para>
        /// <para>code.Add("system", CodeSystems.SCT);</para>
        /// <para>code.Add("display", "Passenger");</para>
        /// <para>ExampleDeathRecord.TransportationRole = code;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Transportation Role: {ExampleDeathRecord.TransportationRole['display']}");</para>
        /// </example>
        [Property("Transportation Role", Property.Types.Dictionary, "Death Investigation", "Transportation Role in death.", true, IGURL.InjuryIncident, true, 45)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='11374-6')", "")]  // The component  code is '69451-3'
        public Dictionary<string, string> TransportationRole
        {
            get
            {
                if (InjuryIncidentObs != null && InjuryIncidentObs.Component.Count > 0)
                {
                    // Find correct component
                    var transportComp = InjuryIncidentObs.Component.FirstOrDefault(entry => ((Observation.ComponentComponent)entry).Code != null &&
                    ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault() != null && ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault().Code == "69451-3");
                    if (transportComp != null && transportComp.Value != null && transportComp.Value as CodeableConcept != null)
                    {
                        return CodeableConceptToDict((CodeableConcept)transportComp.Value);
                    }
                }
                return EmptyCodeableDict();
            }
            set
            {
                if (IsDictEmptyOrDefault(value) && InjuryIncidentObs == null)
                {
                    return;
                }
                if (InjuryIncidentObs == null)
                {
                    CreateInjuryIncidentObs();
                }
                // Find correct component; if doesn't exist add another
                var transportComp = InjuryIncidentObs.Component.FirstOrDefault(entry => ((Observation.ComponentComponent)entry).Code != null &&
                ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault() != null && ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault().Code == "69451-3");
                if (transportComp != null)
                {
                    ((Observation.ComponentComponent)transportComp).Value = DictToCodeableConcept(value);
                }
                else
                {
                    Observation.ComponentComponent component = new Observation.ComponentComponent();
                    component.Code = new CodeableConcept(CodeSystems.LOINC, "69451-3", "Transportation role of decedent", null);
                    component.Value = DictToCodeableConcept(value);
                    InjuryIncidentObs.Component.Add(component);
                }
            }
        }
        /// <summary>Transportation Role in death helper.</summary>
        /// <value>transportation code for role in death.
        /// <para>"code" - the code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.TransportationRoleHelper = VRDR.TransportationRoles.Passenger;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Transportation Role: {ExampleDeathRecord.TransportationRoleHelper");</para>
        /// </example>
        [Property("Transportation Role Helper", Property.Types.String, "Death Investigation", "Transportation Role in death.", false, IGURL.InjuryIncident, true, 45)]
	[PropertyParam("code", "The code used to describe this concept.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='11374-6')", "")]  // The component  code is '69451-3'
        public string TransportationRoleHelper
        {
            get
            {
                if (TransportationRole.ContainsKey("code"))
                {
                    string code = TransportationRole["code"];
                    if (code == "OTH")
                    {
                        if (TransportationRole.ContainsKey("text"))
                        {
                            return (TransportationRole["text"]);
                        }
                        return ("Other");
                    }
                    else if (!String.IsNullOrWhiteSpace(code))
                    {
                        return code;
                    }
                }
                return null;
            }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    // do nothing
                    return;
                }
                if (InjuryIncidentObs == null)
                {
                    CreateInjuryIncidentObs();
                }
                if (!VRDR.Mappings.TransportationIncidentRole.FHIRToIJE.ContainsKey(value))
                { //other
                    //Find the component, or create it
                    var transportComp = InjuryIncidentObs.Component.FirstOrDefault(entry => ((Observation.ComponentComponent)entry).Code != null &&
                    ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault() != null && ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault().Code == "69451-3");
                    if (transportComp == null)
                    {
                        transportComp = new Observation.ComponentComponent();
                        transportComp.Code = new CodeableConcept(CodeSystems.LOINC, "69451-3", "Transportation role of decedent", null);
                        InjuryIncidentObs.Component.Add(transportComp);
                    }
                    transportComp.Value = new CodeableConcept(CodeSystems.NullFlavor_HL7_V3, "OTH", "Other", value);
                }
                else
                { // normal path
                    SetCodeValue("TransportationRole", value, VRDR.ValueSets.TransportationIncidentRole.Codes);
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
        /// <para>code.Add("code", "373066001");</para>
        /// <para>code.Add("system", CodeSystems.SCT);</para>
        /// <para>code.Add("display", "Yes");</para>
        /// <para>ExampleDeathRecord.TobaccoUse = code;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Tobacco Use: {ExampleDeathRecord.TobaccoUse['display']}");</para>
        /// </example>
        [Property("Tobacco Use", Property.Types.Dictionary, "Death Investigation", "If Tobacco Use Contributed To Death.", true, IGURL.TobaccoUseContributedToDeath, true, 32)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='69443-0')", "")]
        public Dictionary<string, string> TobaccoUse
        {
            get
            {
                if (TobaccoUseObs != null && TobaccoUseObs.Value != null && TobaccoUseObs.Value as CodeableConcept != null)
                {
                    return CodeableConceptToDict((CodeableConcept)TobaccoUseObs.Value);
                }
                return EmptyCodeableDict();
            }
            set
            {
                if (TobaccoUseObs == null)
                {
                    TobaccoUseObs = new Observation();
                    TobaccoUseObs.Id = Guid.NewGuid().ToString();
                    TobaccoUseObs.Meta = new Meta();
                    string[] tb_profile = { ProfileURL.TobaccoUseContributedToDeath };
                    TobaccoUseObs.Meta.Profile = tb_profile;
                    TobaccoUseObs.Status = ObservationStatus.Final;
                    TobaccoUseObs.Code = new CodeableConcept(CodeSystems.LOINC, "69443-0", "Did tobacco use contribute to death", null);
                    TobaccoUseObs.Subject = new ResourceReference("urn:uuid:" + Decedent.Id);
                    TobaccoUseObs.Value = DictToCodeableConcept(value);
                    AddReferenceToComposition(TobaccoUseObs.Id, "DeathInvestigation");
                    Bundle.AddResourceEntry(TobaccoUseObs, "urn:uuid:" + TobaccoUseObs.Id);
                }
                else
                {
                    TobaccoUseObs.Value = DictToCodeableConcept(value);
                }
            }
        }

        /// <summary>Tobacco Use Helper. This is a convenience method, to access the code use TobaccoUse instead.</summary>
        /// <value>From a value set..</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.TobaccoUseHelper = "N";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Tobacco Use: {ExampleDeathRecord.TobaccoUseHelper}");</para>
        /// </example>
        [Property("Tobacco Use Helper", Property.Types.String, "Death Investigation", "Tobacco Use.", false, IGURL.TobaccoUseContributedToDeath, true, 27)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='69443-0')", "")]
        public string TobaccoUseHelper
        {
            get
            {
                if (TobaccoUse.ContainsKey("code") && !String.IsNullOrWhiteSpace(TobaccoUse["code"]))
                {
                    return TobaccoUse["code"];
                }
                return null;
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    SetCodeValue("TobaccoUse", value, VRDR.ValueSets.ContributoryTobaccoUse.Codes);
                }
            }
        }


    }
}
