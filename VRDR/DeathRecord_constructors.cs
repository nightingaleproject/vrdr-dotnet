using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.FhirPath;
using Newtonsoft.Json;

// DeathRecord_constructors.cs
//     Contains constructors and associated methods for the DeathRecords class
namespace VRDR
{
    /// <summary>Class <c>DeathRecord</c> models a FHIR Vital Records Death Reporting (VRDR) Death
    /// Record. This class was designed to help consume and produce death records that follow the
    /// HL7 FHIR Vital Records Death Reporting Implementation Guide, as described at:
    /// http://hl7.org/fhir/us/vrdr and https://github.com/hl7/vrdr.
    /// </summary>
    public partial class DeathRecord
    {
        /// <summary>Default constructor that creates a new, empty DeathRecord.</summary>
        public DeathRecord()
        {
            // Start with an empty Bundle.
            Bundle = new Bundle();
            Bundle.Id = Guid.NewGuid().ToString();
            Bundle.Type = Bundle.BundleType.Document; // By default, Bundle type is "document".
            Bundle.Meta = new Meta();
            string[] bundle_profile = { ProfileURL.DeathCertificateDocument };
            Bundle.Timestamp = DateTime.Now;
            Bundle.Meta.Profile = bundle_profile;

            // Start with an empty decedent.  Need reference in Composition.
            Decedent = new Patient();
            Decedent.Id = Guid.NewGuid().ToString();
            Decedent.Meta = new Meta();
            string[] decedent_profile = { ProfileURL.Decedent };
            Decedent.Meta.Profile = decedent_profile;

            // Start with an empty certifier. - Need reference in Composition
            CreateCertifier();

            // Start with an empty certification. - need reference in Composition
            CreateDeathCertification();

            // Add Composition to bundle. As the record is filled out, new entries will be added to this element.
            // I think we need to add sections to the composition.
            Composition = new Composition();
            Composition.Id = Guid.NewGuid().ToString();
            Composition.Status = CompositionStatus.Final;
            Composition.Meta = new Meta();
            string[] composition_profile = { ProfileURL.DeathCertificate };
            Composition.Meta.Profile = composition_profile;
            Composition.Type = new CodeableConcept(CodeSystems.LOINC, "64297-5", "Death certificate", null);
            Composition.Section.Add(new Composition.SectionComponent());
            Composition.Subject = new ResourceReference("urn:uuid:" + Decedent.Id);
            Composition.Author.Add(new ResourceReference("urn:uuid:" + Certifier.Id));
            Composition.Title = "Death Certificate";
            Composition.Attester.Add(new Composition.AttesterComponent());
            Composition.Attester.First().Party = new ResourceReference("urn:uuid:" + Certifier.Id);
            Composition.Attester.First().ModeElement = new Code<Hl7.Fhir.Model.Composition.CompositionAttestationMode>(Hl7.Fhir.Model.Composition.CompositionAttestationMode.Legal);
            Hl7.Fhir.Model.Composition.EventComponent eventComponent = new Hl7.Fhir.Model.Composition.EventComponent();
            eventComponent.Code.Add(new CodeableConcept(CodeSystems.SCT, "103693007", "Diagnostic procedure (procedure)", null));
            eventComponent.Detail.Add(new ResourceReference("urn:uuid:" + DeathCertification.Id));
            Composition.Event.Add(eventComponent);
            Bundle.AddResourceEntry(Composition, "urn:uuid:" + Composition.Id);


            // Add references back to the Decedent, Certifier, Certification, etc.
            AddReferenceToComposition(Decedent.Id, "DecedentDemographics");
            Bundle.AddResourceEntry(Decedent, "urn:uuid:" + Decedent.Id);
            AddReferenceToComposition(Certifier.Id, "DeathCertification");
            Bundle.AddResourceEntry(Certifier, "urn:uuid:" + Certifier.Id);
            AddReferenceToComposition(DeathCertification.Id, "DeathCertification");
            Bundle.AddResourceEntry(DeathCertification, "urn:uuid:" + DeathCertification.Id);

            // AddReferenceToComposition(Pronouncer.Id, "OBE");
            // Bundle.AddResourceEntry(Pronouncer, "urn:uuid:" + Pronouncer.Id);
            //Bundle.AddResourceEntry(Mortician, "urn:uuid:" + Mortician.Id);
            //Bundle.AddResourceEntry(FuneralHomeDirector, "urn:uuid:" + FuneralHomeDirector.Id);

            // Create a Navigator for this new death record.
            Navigator = Bundle.ToTypedElement();

            UpdateDeathRecordIdentifier();
        }

        /// <summary>Constructor that takes a string that represents a FHIR Death Record in either XML or JSON format.</summary>
        /// <param name="record">represents a FHIR Death Record in either XML or JSON format.</param>
        /// <param name="permissive">if the parser should be permissive when parsing the given string</param>
        /// <exception cref="ArgumentException">Record is neither valid XML nor JSON.</exception>
        public DeathRecord(string record, bool permissive = false)
        {
            ParserSettings parserSettings = new ParserSettings
            {
                AcceptUnknownMembers = permissive,
                AllowUnrecognizedEnums = permissive,
                PermissiveParsing = permissive
            };
            // XML?
            Boolean maybeXML = record.TrimStart().StartsWith("<");
            Boolean maybeJSON = record.TrimStart().StartsWith("{");
            if (!String.IsNullOrEmpty(record) && (maybeXML || maybeJSON))
            {
                // Grab all errors found by visiting all nodes and report if not permissive
                if (!permissive)
                {
                    List<string> entries = new List<string>();
                    ISourceNode node = null;
                    if (maybeXML)
                    {
                        node = FhirXmlNode.Parse(record, new FhirXmlParsingSettings { PermissiveParsing = permissive });
                    }
                    else
                    {
                        node = FhirJsonNode.Parse(record, "Bundle", new FhirJsonParsingSettings { PermissiveParsing = permissive });
                    }
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
                    if (maybeXML)
                    {
                        FhirXmlParser parser = new FhirXmlParser(parserSettings);
                        Bundle = parser.Parse<Bundle>(record);
                    }
                    else
                    {
                        FhirJsonParser parser = new FhirJsonParser(parserSettings);
                        Bundle = parser.Parse<Bundle>(record);
                    }

                    // Validate the partial dates.
                    DeathRecord.ValidatePartialDates(Bundle);

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

        /// <summary>Constructor that takes a FHIR Bundle that represents a FHIR Death Record.</summary>
        /// <param name="bundle">represents a FHIR Bundle.</param>
        /// <exception cref="ArgumentException">Record is invalid.</exception>
        public DeathRecord(Bundle bundle)
        {
            Bundle = bundle;
            Navigator = Bundle.ToTypedElement();
            RestoreReferences();
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

        /// <summary>Helper method to return a the bundle.</summary>
        /// <returns>a FHIR Bundle</returns>
        public Bundle GetBundle()
        {
            return Bundle;
        }

        /// <summary>
        /// Helper method to return the bundle that makes up a CauseOfDeathCodedContent bundle. This is actually
        /// the complete DeathRecord Bundle accessible via a method name that aligns with the other specific
        /// GetBundle methods (GetCauseOfDeathCodedContentBundle and GetDemographicCodedContentBundle).
        /// </summary>
        /// <returns>a FHIR Bundle</returns>
        public Bundle GetDeathCertificateDocumentBundle()
        {
            return Bundle;
        }

        private void AddResourceToBundleIfPresent(Resource resource, Bundle bundle)
        {
            if (resource != null)
            {
                bundle.AddResourceEntry(resource, "urn:uuid:" + resource.Id);
            }
        }

        /// <summary>Helper method to return the subset of this record that makes up a CauseOfDeathCodedContent bundle.</summary>
        /// <returns>a new FHIR Bundle</returns>
        public Bundle GetCauseOfDeathCodedContentBundle()
        {
            Bundle codccBundle = new Bundle();
            codccBundle.Id = Guid.NewGuid().ToString();
            codccBundle.Type = Bundle.BundleType.Collection;
            codccBundle.Meta = new Meta();
            string[] profile = { ProfileURL.CauseOfDeathCodedContentBundle };
            codccBundle.Meta.Profile = profile;
            codccBundle.Timestamp = DateTime.Now;
            // Make sure to include the base identifiers, including certificate number and auxiliary state IDs
            codccBundle.Identifier = Bundle.Identifier;
            AddResourceToBundleIfPresent(ActivityAtTimeOfDeathObs, codccBundle);
            AddResourceToBundleIfPresent(AutomatedUnderlyingCauseOfDeathObs, codccBundle);
            AddResourceToBundleIfPresent(ManualUnderlyingCauseOfDeathObs, codccBundle);
            if (EntityAxisCauseOfDeathObsList != null)
            {
                foreach (Observation observation in EntityAxisCauseOfDeathObsList)
                {
                    AddResourceToBundleIfPresent(observation, codccBundle);
                }
            }
            if (RecordAxisCauseOfDeathObsList != null)
            {
                foreach (Observation observation in RecordAxisCauseOfDeathObsList)
                {
                    AddResourceToBundleIfPresent(observation, codccBundle);
                }
            }
            AddResourceToBundleIfPresent(PlaceOfInjuryObs, codccBundle);
            AddResourceToBundleIfPresent(CodingStatusValues, codccBundle);
            AddResourceToBundleIfPresent(CauseOfDeathConditionA, codccBundle);
            AddResourceToBundleIfPresent(CauseOfDeathConditionB, codccBundle);
            AddResourceToBundleIfPresent(CauseOfDeathConditionC, codccBundle);
            AddResourceToBundleIfPresent(CauseOfDeathConditionD, codccBundle);
            AddResourceToBundleIfPresent(ConditionContributingToDeath, codccBundle);
            AddResourceToBundleIfPresent(MannerOfDeath, codccBundle);
            AddResourceToBundleIfPresent(AutopsyPerformed, codccBundle);
            AddResourceToBundleIfPresent(DeathCertification, codccBundle);
            AddResourceToBundleIfPresent(InjuryIncidentObs, codccBundle);
            AddResourceToBundleIfPresent(TobaccoUseObs, codccBundle);
            AddResourceToBundleIfPresent(PregnancyObs, codccBundle);
            AddResourceToBundleIfPresent(SurgeryDateObs, codccBundle);
            return codccBundle;
        }

        /// <summary>Helper method to return the subset of this record that makes up a DemographicCodedContent bundle.</summary>
        /// <returns>a new FHIR Bundle</returns>
        public Bundle GetDemographicCodedContentBundle()
        {
            Bundle dccBundle = new Bundle();
            dccBundle.Id = Guid.NewGuid().ToString();
            dccBundle.Type = Bundle.BundleType.Collection;
            dccBundle.Meta = new Meta();
            string[] profile = { ProfileURL.DemographicCodedContentBundle };
            dccBundle.Meta.Profile = profile;
            dccBundle.Timestamp = DateTime.Now;
            // Make sure to include the base identifiers, including certificate number and auxiliary state IDs
            dccBundle.Identifier = Bundle.Identifier;
            AddResourceToBundleIfPresent(CodedRaceAndEthnicityObs, dccBundle);
            AddResourceToBundleIfPresent(InputRaceAndEthnicityObs, dccBundle);
            return dccBundle;
        }
        /// <summary>Helper method to return the subset of this record that makes up a Mortality Roster bundle.</summary>
        /// <returns>a new FHIR Bundle</returns>
        public Bundle GetMortalityRosterBundle(Boolean alias)
        {
            Bundle mortRosterBundle = new Bundle();
            mortRosterBundle.Id = Guid.NewGuid().ToString();
            mortRosterBundle.Type = Bundle.BundleType.Collection;
            mortRosterBundle.Meta = new Meta();
            string[] profile = { ProfileURL.MortalityRosterBundle };
            mortRosterBundle.Meta.Profile = profile;
            mortRosterBundle.Timestamp = DateTime.Now;
            mortRosterBundle.Identifier = Bundle.Identifier; // includes the certificate number, and aux state IDs
            AddResourceToBundleIfPresent(Decedent, mortRosterBundle);
            AddResourceToBundleIfPresent(DeathLocationLoc, mortRosterBundle);
            AddResourceToBundleIfPresent(DeathDateObs, mortRosterBundle);
            AddResourceToBundleIfPresent(Father, mortRosterBundle);
            AddResourceToBundleIfPresent(Mother, mortRosterBundle);

            // Stick Replace and Alias into bundle header as extensions
            // Copy replace from Composition header
            // Use value of alias from argument
            if (!String.IsNullOrWhiteSpace(ReplaceStatusHelper))
            {
                Extension replaceExt = new Extension(ExtensionURL.ReplaceStatus, DictToCodeableConcept(ReplaceStatus));
                mortRosterBundle.Meta.Extension.Add(replaceExt);
            }
            Extension aliasExt = new Extension(ExtensionURL.AliasStatus, new FhirBoolean(alias));
            mortRosterBundle.Meta.Extension.Add(aliasExt);
            return mortRosterBundle;
        }


        /////////////////////////////////////////////////////////////////////////////////
        //
        // Class helper methods useful for building, searching through records.
        //
        /////////////////////////////////////////////////////////////////////////////////

        /// <summary>Return a reference to the Composition object for unit testing.</summary>
        public Composition GetComposition()
        {
            return Composition;
        }

        /// <summary>Add a reference to the Death Record Composition.</summary>
        /// <param name="reference">a reference.</param>
        /// <param name="code">the code for the section to add to.</param>
        /// The sections are from : CodeSystems.DocumentSections
        ///               DecedentDemographics
        ///               DeathInvestigation
        ///               DeathCertification
        ///               DecedentDisposition
        ///               CodedContent
        private void AddReferenceToComposition(string reference, string code)
        {
            // In many of the createXXXXXX methods this gets called as a last step to add a reference to the new instance to the composition.
            // The Composition is present only in the DeathCertificateDocument, and is absent in all of the other bundles.
            // In lieu of putting conditional logic in all of the calling methods, added it here.
            if (Composition == null)
            {
                return;
            }

            //Composition.Section.First().Entry.Add(new ResourceReference("urn:uuid:" + reference));
            Composition.SectionComponent section = new Composition.SectionComponent();
            string[] sections = new string[] { "DecedentDemographics", "DeathInvestigation", "DeathCertification", "DecedentDisposition", "CodedContent" };
            if (sections.Any(code.Contains))
            {
                // Find the right section
                foreach (var s in Composition.Section)
                {
                    if (s.Code != null && s.Code.Coding.Count > 0 && s.Code.Coding.First().Code == code)
                    {
                        section = s;
                    }
                }
                if (section.Code == null)
                {
                    Dictionary<string, string> coding = new Dictionary<string, string>();
                    coding["system"] = VRDR.CodeSystems.DocumentSections;
                    coding["code"] = code;
                    section.Code = DictToCodeableConcept(coding);
                    Composition.Section.Add(section);
                }
                section.Entry.Add(new ResourceReference("urn:uuid:" + reference));
            }
        }

        /// <summary>Remove a reference from the Death Record Composition.</summary>
        /// <param name="reference">a reference.</param>
        /// <param name="code">a code for the section to modify.</param>
        private bool RemoveReferenceFromComposition(string reference, string code)
        {
            Composition.SectionComponent section = Composition.Section.Where(s => s.Code.Coding.First().Code == code).First();
            return section.Entry.RemoveAll(entry => entry.Reference == reference) > 0;
        }

        /// <summary>Restores class references from a newly parsed record.</summary>
        private void RestoreReferences()
        {
            // Depending on the type of bundle, some of this information may not be present, so check it in a null-safe way
            string profile = Bundle.Meta?.Profile?.FirstOrDefault();
            bool fullRecord = VRDR.ProfileURL.DeathCertificateDocument.Equals(profile);
            // Grab Composition
            var compositionEntry = Bundle.Entry.FirstOrDefault(entry => entry.Resource is Composition);
            if (compositionEntry != null)
            {
                Composition = (Composition)compositionEntry.Resource;
            }
            else if (fullRecord)
            {
                throw new System.ArgumentException("Failed to find a Composition. The first entry in the FHIR Bundle should be a Composition.");
            }

            // Grab Patient
            if (fullRecord && (Composition.Subject == null || String.IsNullOrWhiteSpace(Composition.Subject.Reference)))
            {
                throw new System.ArgumentException("The Composition is missing a subject (a reference to the Decedent resource).");
            }
            var patientEntry = Bundle.Entry.FirstOrDefault(entry => entry.Resource is Patient);
            if (patientEntry != null)
            {
                Decedent = (Patient)patientEntry.Resource;
            }
            else if (fullRecord)
            {
                throw new System.ArgumentException("Failed to find a Decedent (Patient).");
            }

            // Grab Certifier
            if (Composition == null || (Composition.Attester == null || Composition.Attester.FirstOrDefault() == null || Composition.Attester.First().Party == null || String.IsNullOrWhiteSpace(Composition.Attester.First().Party.Reference)))
            {
                if (fullRecord)
                {
                    throw new System.ArgumentException("The Composition is missing an attestor (a reference to the Certifier/Practitioner resource).");
                }
            }
            else
            {  // There is an attester
                var attesterID = (Composition.Attester.First().Party.Reference).Split('/').Last(); // Practititioner/Certifier-Example1 --> Certifier-Example1.  Trims the type off of the path
                var practitionerEntry = Bundle.Entry.FirstOrDefault(entry => entry.Resource is Practitioner && (entry.FullUrl == Composition.Attester.First().Party.Reference || (entry.Resource.Id != null && entry.Resource.Id == attesterID)));
                if (practitionerEntry != null)
                {
                    Certifier = (Practitioner)practitionerEntry.Resource;
                }
            }
            // else
            // {
            //     throw new System.ArgumentException("Failed to find a Certifier (Practitioner). The third entry in the FHIR Bundle is usually the Certifier (Practitioner). Either the Certifier is missing from the Bundle, or the attestor reference specified in the Composition is incorrect.");
            // }
            // *** Pronouncer and Mortician are not supported by IJE. ***
            // They can be included in DeathCertificateDocument and linked from DeathCertificate.  THe only sure way to find them is to look for the reference from DeathDate and DispositionMethod, respectively.
            // For now, we comment them out.
            // // Grab Pronouncer
            // // IMPROVEMENT: Move away from using meta profile to find this Practitioner.  Use performer reference from DeathDate
            // var pronouncerEntry = Bundle.Entry.FirstOrDefault( entry => entry.Resource.ResourceType == ResourceType.Practitioner && entry.Resource.Meta.Profile.FirstOrDefault() != null && MatchesProfile("VRDR-Death-Pronouncement-Performer", entry.Resource.Meta.Profile.FirstOrDefault()));
            // if (pronouncerEntry != null)
            // {
            //     Pronouncer = (Practitioner)pronouncerEntry.Resource;
            // }

            // Grab Death Certification
            var procedureEntry = Bundle.Entry.FirstOrDefault(entry => entry.Resource is Procedure);
            if (procedureEntry != null)
            {
                DeathCertification = (Procedure)procedureEntry.Resource;
            }

            // // Grab State Local Identifier
            // var stateDocumentReferenceEntry = Bundle.Entry.FirstOrDefault( entry => entry.Resource.ResourceType == ResourceType.DocumentReference && ((DocumentReference)entry.Resource).Type.Coding.First().Code == "64297-5" );
            // if (stateDocumentReferenceEntry != null)
            // {
            //     StateDocumentReference = (DocumentReference)stateDocumentReferenceEntry.Resource;
            // }

            // Grab Funeral Home  - Organization with type="funeral"
            var funeralHome = Bundle.Entry.FirstOrDefault(entry => entry.Resource is Organization &&
                    ((Organization)entry.Resource).Type.FirstOrDefault() != null && (CodeableConceptToDict(((Organization)entry.Resource).Type.First())["code"] == "funeralhome"));
            if (funeralHome != null)
            {
                FuneralHome = (Organization)funeralHome.Resource;
            }
            // Grab Coding Status
            var parameterEntry = Bundle.Entry.FirstOrDefault(entry => entry.Resource is Parameters);
            if (parameterEntry != null)
            {
                CodingStatusValues = (Parameters)parameterEntry.Resource;
            }
            // Scan through all Observations to make sure they all have codes!
            foreach (var ob in Bundle.Entry.Where(entry => entry.Resource is Observation))
            {
                Observation obs = (Observation)ob.Resource;
                if (obs.Code == null || obs.Code.Coding == null || obs.Code.Coding.FirstOrDefault() == null || obs.Code.Coding.First().Code == null)
                {
                    throw new System.ArgumentException("Found an Observation resource that did not contain a code. All Observations must include a code to specify what the Observation is referring to.");
                }
                switch (obs.Code.Coding.First().Code)
                {
                    case "69449-7":
                        MannerOfDeath = (Observation)obs;
                        break;
                    case "80905-3":
                        DispositionMethod = (Observation)obs;
                        // Link the Mortician based on the performer of this observation
                        break;
                    case "69441-4":
                        ConditionContributingToDeath = (Observation)obs;
                        break;
                    case "69453-9":
                        var lineNumber = 0;
                        Observation.ComponentComponent lineNumComp = obs.Component.Where(c => c.Code.Coding[0].Code == "lineNumber").FirstOrDefault();
                        if (lineNumComp != null && lineNumComp.Value != null)
                        {
                            lineNumber = Int32.Parse(lineNumComp.Value.ToString());
                        }
                        switch (lineNumber)
                        {
                            case 1:
                                CauseOfDeathConditionA = obs;
                                break;

                            case 2:
                                CauseOfDeathConditionB = obs;
                                break;

                            case 3:
                                CauseOfDeathConditionC = obs;
                                break;

                            case 4:
                                CauseOfDeathConditionD = obs;
                                break;

                            default: // invalid position, should we go kaboom?
                                // throw new System.ArgumentException("Found a Cause of Death Part1 Observation with a linenumber other than 1-4.");
                                break;
                        }
                        break;
                    case "80913-7":
                        DecedentEducationLevel = (Observation)obs;
                        break;
                    case "21843-8":
                        UsualWork = (Observation)obs;
                        break;
                    case "55280-2":
                        MilitaryServiceObs = (Observation)obs;
                        break;
                    case "BR":
                        BirthRecordIdentifier = (Observation)obs;
                        break;
                    case "emergingissues":
                        EmergingIssues = (Observation)obs;
                        break;
                    case "codedraceandethnicity":
                        CodedRaceAndEthnicityObs = (Observation)obs;
                        break;
                    case "inputraceandethnicity":
                        InputRaceAndEthnicityObs = (Observation)obs;
                        break;
                    case "11376-1":
                        PlaceOfInjuryObs = (Observation)obs;
                        break;
                    case "80358-5":
                        AutomatedUnderlyingCauseOfDeathObs = (Observation)obs;
                        break;
                    case "80359-3":
                        ManualUnderlyingCauseOfDeathObs = (Observation)obs;
                        break;
                    case "80626-5":
                        ActivityAtTimeOfDeathObs = (Observation)obs;
                        break;
                    case "80992-1":
                        SurgeryDateObs = (Observation)obs;
                        break;
                    case "81956-5":
                        DeathDateObs = (Observation)obs;
                        break;
                    case "11374-6":
                        InjuryIncidentObs = (Observation)obs;
                        break;
                    case "69443-0":
                        TobaccoUseObs = (Observation)obs;
                        break;
                    case "74497-9":
                        ExaminerContactedObs = (Observation)obs;
                        break;
                    case "69442-2":
                        PregnancyObs = (Observation)obs;
                        break;
                    case "39016-1":
                        AgeAtDeathObs = (Observation)obs;
                        break;
                    case "85699-7":
                        AutopsyPerformed = (Observation)obs;
                        break;
                    case "80356-9":
                        if (EntityAxisCauseOfDeathObsList == null)
                        {
                            EntityAxisCauseOfDeathObsList = new List<Observation>();
                        }
                        EntityAxisCauseOfDeathObsList.Add((Observation)obs);
                        break;
                    case "80357-7":
                        if (RecordAxisCauseOfDeathObsList == null)
                        {
                            RecordAxisCauseOfDeathObsList = new List<Observation>();
                        }
                        RecordAxisCauseOfDeathObsList.Add((Observation)obs);
                        break;
                    default:
                        // skip
                        break;
                }
            }

            // Scan through all RelatedPerson to make sure they all have relationship codes!
            foreach (var rp in Bundle.Entry.Where(entry => entry.Resource is RelatedPerson))
            {
                RelatedPerson rpn = (RelatedPerson)rp.Resource;
                if (rpn.Relationship == null || rpn.Relationship.FirstOrDefault() == null || rpn.Relationship.FirstOrDefault().Coding == null || rpn.Relationship.FirstOrDefault().Coding.FirstOrDefault() == null ||
                      rpn.Relationship.FirstOrDefault().Coding.First().Code == null)
                {
                    throw new System.ArgumentException("Found a RelatedPerson resource that did not contain a relationship code. All RelatedPersons must include a relationship code to specify how the RelatedPerson is related to the subject.");
                }
                switch (rpn.Relationship.FirstOrDefault().Coding.First().Code)
                {
                    case "FTH":
                        Father = (RelatedPerson)rpn;
                        break;
                    case "MTH":
                        Mother = (RelatedPerson)rpn;
                        break;
                    case "SPS":
                        Spouse = (RelatedPerson)rpn;
                        break;
                    default:
                        // skip
                        break;
                }
            }
            foreach (var rp in Bundle.Entry.Where(entry => entry.Resource is Location))
            {
                Location lcn = (Location)rp.Resource;
                if ((lcn.Type.FirstOrDefault() == null) || lcn.Type.FirstOrDefault().Coding == null || lcn.Type.FirstOrDefault().Coding.First().Code == null)
                {
                    // throw new System.ArgumentException("Found a Location resource that did not contain a type code. All Locations must include a type code to specify the role of the location.");
                }
                else
                {
                    switch (lcn.Type.FirstOrDefault().Coding.First().Code)
                    {
                        case "death":
                            DeathLocationLoc = lcn;
                            break;
                        case "disposition":
                            DispositionLocation = lcn;
                            break;
                        case "injury":
                            InjuryLocationLoc = lcn;
                            break;
                        default:
                            // skip
                            break;
                    }
                }
            }
            if (fullRecord)
            {
                UpdateDeathRecordIdentifier();
            }
        }
    }
}