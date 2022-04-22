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

namespace VRDR
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

        /// <summary>DocumentReference that is used to specify state local death record identifier.</summary>
        private DocumentReference StateDocumentReference;

        /// <summary>The Decedent.</summary>
        private Patient Decedent;

        /// <summary>The Decedent's Demographics.</summary>
        private Observation InputRaceandEthnicity;

        /// <summary>The Pronouncer of death.</summary>
        private Practitioner Pronouncer;

        /// <summary>The Certifier.</summary>
        private Practitioner Certifier;

        /// <summary>The Mortician.</summary>
        private Practitioner Mortician;

        /// <summary>The Certification.</summary>
        private Procedure DeathCertification;

        /// <summary>Create Death Certification.</summary>
        private void CreateDeathCertification(){
            CreateEmptyDeathCertification();
            AddReferenceToComposition(DeathCertification.Id);
            Bundle.AddResourceEntry(DeathCertification, "urn:uuid:" + DeathCertification.Id);
        }

        /// <summary>Create Empty Death Certification.</summary>
        private void CreateEmptyDeathCertification(){
            DeathCertification = new Procedure();
            DeathCertification.Id = Guid.NewGuid().ToString();
            DeathCertification.Subject = new ResourceReference("urn:uuid:" + Decedent.Id);
            DeathCertification.Meta = new Meta();
            string[] deathcertification_profile = { IGURL.DeathCertification };
            DeathCertification.Meta.Profile = deathcertification_profile;
            DeathCertification.Status = EventStatus.Completed;
            DeathCertification.Category = new CodeableConcept(CodeSystems.SCT, "103693007", "Diagnostic procedure", null);
            DeathCertification.Code = new CodeableConcept(CodeSystems.SCT, "308646001", "Death certification", null);
        }

        /// <summary>The Manner of Death Observation.</summary>
        private Observation MannerOfDeath;

        /// <summary>Condition Contributing to Death.</summary>
        private Observation ConditionContributingToDeath;

        /// <summary>Create a Cause Of Death Condition </summary>
        private Observation CauseOfDeathCondition(int index){
                    Observation CodCondition;
                    CodCondition = new Observation();
                    CodCondition.Id = Guid.NewGuid().ToString();
                    CodCondition.Meta = new Meta();
                    string[] condition_profile = { ProfileURL.CauseOfDeathPart1 };
                    CodCondition.Meta.Profile = condition_profile;
                    CodCondition.Code = new CodeableConcept(CodeSystems.LOINC, "69453-9", "Cause of death [US Standard Certificate of Death]", null);
                    CodCondition.Subject = new ResourceReference("urn:uuid:" + Decedent.Id);
                    CodCondition.Performer.Add (new ResourceReference("urn:uuid:" + Certifier.Id));
                    AddReferenceToComposition(CodCondition.Id);
                    Bundle.AddResourceEntry(CodCondition, "urn:uuid:" + CodCondition.Id);
                    List.EntryComponent entry = new List.EntryComponent();
                    entry.Item = new ResourceReference("urn:uuid:" + CodCondition.Id);
                    if (CauseOfDeathConditionPathway.Entry.Count() != 4)
                    {
                        foreach (var i in Enumerable.Range(0, 4)) { CauseOfDeathConditionPathway.Entry.Add(null); }
                    }
                    CauseOfDeathConditionPathway.Entry[index] = entry;
                    return (CodCondition);
        }


        /// <summary>Cause Of Death Condition Line A (#1).</summary>
        private Observation CauseOfDeathConditionA;

        /// <summary>Cause Of Death Condition Line B (#2).</summary>
        private Observation CauseOfDeathConditionB;

        /// <summary>Cause Of Death Condition Line C (#3).</summary>
        private Observation CauseOfDeathConditionC;

        /// <summary>Cause Of Death Condition Line D (#4).</summary>
        private Observation CauseOfDeathConditionD;

        /// <summary>Cause Of Death Condition Pathway.</summary>
        private List CauseOfDeathConditionPathway;

        /// <summary>Decedent's Father.</summary>
        private RelatedPerson Father;

                /// <summary>Create Spouse.</summary>
        private void CreateFather(){
            Father = new RelatedPerson();
            Father.Id = Guid.NewGuid().ToString();
            Father.Meta = new Meta();
            string[] father_profile = { ProfileURL.DecedentFather };
            Father.Meta.Profile = father_profile;
            Father.Patient = new ResourceReference("urn:uuid:" + Decedent.Id);
            Father.Relationship.Add(new CodeableConcept(CodeSystems.RoleCode_HL7_V3, "FTH", "father", null));
            AddReferenceToComposition(Father.Id);
            Bundle.AddResourceEntry(Father, "urn:uuid:" + Father.Id);
        }

        /// <summary>Decedent's Mother.</summary>
        private RelatedPerson Mother;

        /// <summary>Create Mother.</summary>
        private void CreateMother(){
            Mother = new RelatedPerson();
            Mother.Id = Guid.NewGuid().ToString();
            Mother.Meta = new Meta();
            string[] mother_profile = { ProfileURL.DecedentMother };
            Mother.Meta.Profile = mother_profile;
            Mother.Patient = new ResourceReference("urn:uuid:" + Decedent.Id);
            Mother.Relationship.Add(new CodeableConcept(CodeSystems.RoleCode_HL7_V3, "MTH", "mother", null));
            AddReferenceToComposition(Mother.Id);
            Bundle.AddResourceEntry(Mother, "urn:uuid:" + Mother.Id);
        }

        /// <summary>Decedent's Spouse.</summary>
        private RelatedPerson Spouse;

        /// <summary>Create Spouse.</summary>
        private void CreateSpouse(){
            Spouse = new RelatedPerson();
            Spouse.Id = Guid.NewGuid().ToString();
            Spouse.Meta = new Meta();
            string[] spouse_profile = { ProfileURL.DecedentSpouse };
            Spouse.Meta.Profile = spouse_profile;
            Spouse.Patient = new ResourceReference("urn:uuid:" + Decedent.Id);
            Spouse.Relationship.Add(new CodeableConcept(CodeSystems.RoleCode_HL7_V3, "SPS", "spouse", null));
            AddReferenceToComposition(Spouse.Id);
            Bundle.AddResourceEntry(Spouse, "urn:uuid:" + Spouse.Id);
        }


        /// <summary>Decedent Education Level.</summary>
        private Observation DecedentEducationLevel;

        /// <summary>Birth Record Identifier.</summary>
        private Observation BirthRecordIdentifier;

        /// <summary>Emerging Issues.</summary>
        protected Observation EmergingIssues;


        private void CreateBirthRecordIdentifier(){
            BirthRecordIdentifier = new Observation();
            BirthRecordIdentifier.Id = Guid.NewGuid().ToString();
            BirthRecordIdentifier.Meta = new Meta();
            string[] br_profile = { ProfileURL.BirthRecordIdentifier };
            BirthRecordIdentifier.Meta.Profile = br_profile;
            BirthRecordIdentifier.Status = ObservationStatus.Final;
            BirthRecordIdentifier.Code = new CodeableConcept("http://terminology.hl7.org/CodeSystem/v2-0203", "BR", "Birth registry number", null);
            BirthRecordIdentifier.Subject = new ResourceReference("urn:uuid:" + Decedent.Id);
            BirthRecordIdentifier.Value = (FhirString) null;
            BirthRecordIdentifier.DataAbsentReason = new CodeableConcept(CodeSystems.Data_Absent_Reason_HL7_V3, "unknown", "Unknown", null);

            AddReferenceToComposition(BirthRecordIdentifier.Id);
            Bundle.AddResourceEntry(BirthRecordIdentifier, "urn:uuid:" + BirthRecordIdentifier.Id);
        }
        /// <summary>Usual Work.</summary>
        private Observation UsualWork;

        /// <summary>Create Usual Work.</summary>
        private void CreateUsualWork(){
            UsualWork = new Observation();
            UsualWork.Id = Guid.NewGuid().ToString();
            UsualWork.Meta = new Meta();
            string[] usualwork_profile = { ProfileURL.DecedentUsualWork };
            UsualWork.Meta.Profile = usualwork_profile;
            UsualWork.Status = ObservationStatus.Final;
            UsualWork.Code = new CodeableConcept(CodeSystems.LOINC, "21843-8", "History of Usual occupation", null);
            UsualWork.Category.Add(new CodeableConcept(CodeSystems.ObservationCategory,"social-history",null, null));
            UsualWork.Subject = new ResourceReference("urn:uuid:" + Decedent.Id);
            UsualWork.Effective = new Period();
            AddReferenceToComposition(UsualWork.Id);
            Bundle.AddResourceEntry(UsualWork, "urn:uuid:" + UsualWork.Id);
        }

        /// <summary>Whether the decedent served in the military</summary>
        private Observation MilitaryServiceObs;

        /// <summary>The Funeral Home.</summary>
        private Organization FuneralHome;


        // <summary> Create Funeral Home. </summary>
        private void CreateFuneralHome(){
            FuneralHome = new Organization();
            FuneralHome.Id = Guid.NewGuid().ToString();
            FuneralHome.Meta = new Meta();
            string[] funeralhome_profile = { ProfileURL.FuneralHome };
            FuneralHome.Meta.Profile = funeralhome_profile;
            FuneralHome.Type.Add(new CodeableConcept("http://hl7.org/fhir/us/vrdr/CodeSystem/vrdr-organization-type-cs", "funeralhome", "Funeral Home",null));
            FuneralHome.Active = true;
        }
        /// <summary>The Funeral Home Director.</summary>
        private PractitionerRole FuneralHomeDirector;


        /// <summary>Disposition Location.</summary>
        private Location DispositionLocation;

        /// <summary>Create Disposition Location.</summary>
        private void CreateDispositionLocation(){
            DispositionLocation = new Location();
            DispositionLocation.Id = Guid.NewGuid().ToString();
            DispositionLocation.Meta = new Meta();
            string[] dispositionlocation_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Disposition-Location" };
            DispositionLocation.Meta.Profile = dispositionlocation_profile;
            Coding pt = new Coding(CodeSystems.HL7_location_physical_type, "si", "Site");
            DispositionLocation.PhysicalType = new CodeableConcept();
            DispositionLocation.PhysicalType.Coding.Add(pt);
            DispositionLocation.Type.Add(new CodeableConcept("http://hl7.org/fhir/us/vrdr/CodeSystem/vrdr-location-type-cs", "disposition", "disposition location", null));
            LinkObservationToLocation(DispositionMethod, DispositionLocation);
        }

        /// <summary>Disposition Method.</summary>
        private Observation DispositionMethod;

        /// <summary>Autopsy Performed.</summary>
        private Observation AutopsyPerformed;

        /// <summary>Create Autopsy Performed </summary>
        private void CreateAutopsyPerformed(){
            AutopsyPerformed = new Observation();
            AutopsyPerformed.Id = Guid.NewGuid().ToString();
            AutopsyPerformed.Meta = new Meta();
            string[] autopsyperformed_profile = { ProfileURL.AutopsyPerformedIndicator };
            AutopsyPerformed.Meta.Profile = autopsyperformed_profile;
            AutopsyPerformed.Status = ObservationStatus.Final;
            AutopsyPerformed.Code = new CodeableConcept(CodeSystems.LOINC, "85699-7", "Autopsy was performed", null);
            AutopsyPerformed.Subject = new ResourceReference("urn:uuid:" + Decedent.Id);
            AddReferenceToComposition(AutopsyPerformed.Id);
            Bundle.AddResourceEntry(AutopsyPerformed, "urn:uuid:" + AutopsyPerformed.Id);
        }

        /// <summary>Age At Death.</summary>
        private Observation AgeAtDeathObs;

         /// <summary>Create Age At Death Obs</summary>
        private void CreateAgeAtDeathObs(){
            AgeAtDeathObs = new Observation();
            AgeAtDeathObs.Id = Guid.NewGuid().ToString();
            AgeAtDeathObs.Meta = new Meta();
            string[] age_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Decedent-Age" };
            AgeAtDeathObs.Meta.Profile = age_profile;
            AgeAtDeathObs.Status = ObservationStatus.Final;
            AgeAtDeathObs.Code = new CodeableConcept(CodeSystems.LOINC, "30525-0", "Age", null);
            AgeAtDeathObs.Subject = new ResourceReference("urn:uuid:" + Decedent.Id);
            AgeAtDeathObs.Effective = DeathDateObs?.Value;
            AgeAtDeathObs.Value = new Quantity();
            AgeAtDeathObs.DataAbsentReason = new CodeableConcept(CodeSystems.Data_Absent_Reason_HL7_V3, "unknown", "Unknown", null); // set at birth
            AddReferenceToComposition(AgeAtDeathObs.Id);
            Bundle.AddResourceEntry(AgeAtDeathObs, "urn:uuid:" + AgeAtDeathObs.Id);
        }

        private void CreateDeathDateObs() {
            DeathDateObs = new Observation();
            DeathDateObs.Id = Guid.NewGuid().ToString();
            DeathDateObs.Meta = new Meta();
            string[] deathdate_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Death-Date" };
            DeathDateObs.Meta.Profile = deathdate_profile;
            DeathDateObs.Status = ObservationStatus.Final;
            DeathDateObs.Code = new CodeableConcept(CodeSystems.LOINC, "81956-5", "Date and time of death", null);
            DeathDateObs.Subject = new ResourceReference("urn:uuid:" + Decedent.Id);
            AddReferenceToComposition(DeathDateObs.Id);
            Bundle.AddResourceEntry(DeathDateObs, "urn:uuid:" + DeathDateObs.Id);
        }

        private void CreateRaceEthnicityObs() {
            InputRaceandEthnicity = new Observation();
            InputRaceandEthnicity.Id = Guid.NewGuid().ToString();
            InputRaceandEthnicity.Meta = new Meta();
            string[] raceethnicity_profile = { ProfileURL.InputRaceAndEthnicity };
            InputRaceandEthnicity.Meta.Profile = raceethnicity_profile;
            InputRaceandEthnicity.Status = ObservationStatus.Final;
            Dictionary<string, string> coding = new Dictionary<string, string>();
            coding.Add("code", "inputraceandethnicity");
            coding.Add("system", "http://hl7.org/fhir/us/vrdr/CodeSystem/vrdr-observation-cs");
            InputRaceandEthnicity.Code = DictToCodeableConcept(coding);
            InputRaceandEthnicity.Subject = new ResourceReference("urn:uuid:" + InputRaceandEthnicity.Id);
            AddReferenceToComposition(InputRaceandEthnicity.Id);
            Bundle.AddResourceEntry(InputRaceandEthnicity, "urn:uuid:" + InputRaceandEthnicity.Id);
        }

        /// <summary>Decedent Pregnancy Status.</summary>
        private Observation PregnancyObs;

        /// <summary> Create Pregnancy Status. </summary>
        private void CreatePregnancyObs(){
            PregnancyObs = new Observation();
            PregnancyObs.Id = Guid.NewGuid().ToString();
            PregnancyObs.Meta = new Meta();
            string[] p_profile = { IGURL.DecedentPregnancyStatus };
            PregnancyObs.Meta.Profile = p_profile;
            PregnancyObs.Status = ObservationStatus.Final;
            PregnancyObs.Code = new CodeableConcept(CodeSystems.LOINC, "69442-2", "Timing of recent pregnancy in relation to death", null);
            PregnancyObs.Subject = new ResourceReference("urn:uuid:" + Decedent.Id);
            AddReferenceToComposition(PregnancyObs.Id);
            Bundle.AddResourceEntry(PregnancyObs, "urn:uuid:" + PregnancyObs.Id);
        }

        /// <summary>Examiner Contacted.</summary>
        private Observation ExaminerContactedObs;

        /// <summary>Tobacco Use Contributed To Death.</summary>
        private Observation TobaccoUseObs;

        /// <summary>Injury Location.</summary>
        private Location InjuryLocationLoc;

          /// <summary>Create Injury Location.</summary>
          private void CreateInjuryLocationLoc(){
            InjuryLocationLoc = new Location();
            InjuryLocationLoc.Id = Guid.NewGuid().ToString();
            InjuryLocationLoc.Meta = new Meta();
            string[] injurylocation_profile = {ProfileURL.InjuryLocation };
            InjuryLocationLoc.Meta.Profile = injurylocation_profile;
            InjuryLocationLoc.Address = DictToAddress(EmptyAddrDict());
            InjuryLocationLoc.Type.Add(new CodeableConcept("http://hl7.org/fhir/us/vrdr/CodeSystem/vrdr-location-type-cs", "injury", "injury location", null));
        }

        /// <summary>Injury Incident.</summary>
        private Observation InjuryIncidentObs;

       /// <summary>Create Injury Incident.</summary>
        private void CreateInjuryIncidentObs(){
                    InjuryIncidentObs = new Observation();
                    InjuryIncidentObs.Id = Guid.NewGuid().ToString();
                    InjuryIncidentObs.Meta = new Meta();
                    string[] iio_profile = { ProfileURL.InjuryIncident };
                    InjuryIncidentObs.Meta.Profile = iio_profile;
                    InjuryIncidentObs.Status = ObservationStatus.Final;
                    InjuryIncidentObs.Code = new CodeableConcept(CodeSystems.LOINC, "11374-6", "Injury incident description Narrative", null);
                    InjuryIncidentObs.Subject = new ResourceReference("urn:uuid:" + Decedent.Id);
                    LinkObservationToLocation(InjuryIncidentObs, InjuryLocationLoc);
                    AddReferenceToComposition(InjuryIncidentObs.Id);
                    Bundle.AddResourceEntry(InjuryIncidentObs, "urn:uuid:" + InjuryIncidentObs.Id);
        }
        /// <summary>Death Location.</summary>
        private Location DeathLocationLoc;
        /// <summary>Create Death Location </summary>
        private void CreateDeathLocation(){
            DeathLocationLoc = new Location();
            DeathLocationLoc.Id = Guid.NewGuid().ToString();
            DeathLocationLoc.Meta = new Meta();
            string[] deathlocation_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/vrdr-death-location" };
            DeathLocationLoc.Meta.Profile = deathlocation_profile;
            DeathLocationLoc.Type.Add(new CodeableConcept("http://hl7.org/fhir/us/vrdr/CodeSystem/vrdr-location-type-cs", "death", "death location", null));
            LinkObservationToLocation(DeathDateObs, DeathLocationLoc);
            AddReferenceToComposition(DeathLocationLoc.Id);
            Bundle.AddResourceEntry(DeathLocationLoc, "urn:uuid:" + DeathLocationLoc.Id);

        }

        /// <summary>Date Of Death.</summary>
        private Observation DeathDateObs;
        private const string  locationJurisdictionExtPath = "http://hl7.org/fhir/us/vrdr/StructureDefinition/Location-Jurisdiction-Id";

        /// <summary>Default constructor that creates a new, empty DeathRecord.</summary>
        public DeathRecord()
        {
            // Start with an empty Bundle.
            Bundle = new Bundle();
            Bundle.Id = Guid.NewGuid().ToString();
            Bundle.Type = Bundle.BundleType.Document; // By default, Bundle type is "document".
            Bundle.Meta = new Meta();
            string[] bundle_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Death-Certificate-Document" };
            Bundle.Timestamp = DateTime.Now;
            Bundle.Meta.Profile = bundle_profile;

            // Start with an empty decedent.
            Decedent = new Patient();
            Decedent.Id = Guid.NewGuid().ToString();
            Decedent.Meta = new Meta();
            string[] decedent_profile = { IGURL.Decedent  };
            Decedent.Meta.Profile = decedent_profile;

            // Start with an empty race and ethinicity observation
            InputRaceandEthnicity = new Observation();
            InputRaceandEthnicity.Id = Guid.NewGuid().ToString();
            InputRaceandEthnicity.Meta = new Meta();
            string[] raceethnicity_profile = { ProfileURL.InputRaceAndEthnicity };
            InputRaceandEthnicity.Meta.Profile = raceethnicity_profile;
            InputRaceandEthnicity.Status = ObservationStatus.Final;
            Dictionary<string, string> coding = new Dictionary<string, string>();
            coding.Add("code", "inputraceandethnicity");
            coding.Add("system", "Input Race and Ethnicity");
            InputRaceandEthnicity.Code = DictToCodeableConcept(coding);
            InputRaceandEthnicity.Subject = new ResourceReference("urn:uuid:" + InputRaceandEthnicity.Id);

            // Start with an empty certifier.
            Certifier = new Practitioner();
            Certifier.Id = Guid.NewGuid().ToString();
            Certifier.Meta = new Meta();
            string[] certifier_profile = { IGURL.Certifier  };
            Certifier.Meta.Profile = certifier_profile;

            // Start with an empty pronouncer.
            Pronouncer = new Practitioner();
            Pronouncer.Id = Guid.NewGuid().ToString();
            Pronouncer.Meta = new Meta();
            string[] pronouncer_profile = { OtherURL.USCorePractitioner };
            Pronouncer.Meta.Profile = pronouncer_profile;

            // Start with an empty mortician.
           // InitializeMorticianIfNull();

            // Start with an empty certification.
            CreateEmptyDeathCertification();

            // Start with an empty funeral home.
            CreateFuneralHome();

            // FuneralHomeLicensee Points to Mortician and FuneralHome
            // FuneralHomeDirector = new PractitionerRole();
            // FuneralHomeDirector.Id = Guid.NewGuid().ToString();
            // FuneralHomeDirector.Meta = new Meta();
            // string[] funeralhomedirector_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Funeral-Service-Licensee" };
            // FuneralHomeDirector.Meta.Profile = funeralhomedirector_profile;
            // FuneralHomeDirector.Practitioner = new ResourceReference("urn:uuid:" + Mortician.Id);
            // FuneralHomeDirector.Organization = new ResourceReference("urn:uuid:" + FuneralHome.Id);

            // Location of Disposition
            CreateDispositionLocation();
            // DispositionLocation = new Location();
            // DispositionLocation.Id = Guid.NewGuid().ToString();
            // DispositionLocation.Meta = new Meta();
            // string[] dispositionlocation_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Disposition-Location" };
            // DispositionLocation.Meta.Profile = dispositionlocation_profile;
            // Coding pt = new Coding(CodeSystems.HL7_location_physical_type, "si", "Site");
            // DispositionLocation.PhysicalType = new CodeableConcept();
            // DispositionLocation.PhysicalType.Coding.Add(pt);

            // Add Composition to bundle. As the record is filled out, new entries will be added to this element.
            Composition = new Composition();
            Composition.Id = Guid.NewGuid().ToString();
            Composition.Status = CompositionStatus.Final;
            Composition.Meta = new Meta();
            string[] composition_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Death-Certificate" };
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

            // Start with an empty Cause of Death Pathway
            CauseOfDeathConditionPathway = new List();
            CauseOfDeathConditionPathway.Id = Guid.NewGuid().ToString();
            CauseOfDeathConditionPathway.Meta = new Meta();
            string[] causeofdeathconditionpathway_profile = { ProfileURL.CauseOfDeathPathway };
            CauseOfDeathConditionPathway.Meta.Profile = causeofdeathconditionpathway_profile;
            CauseOfDeathConditionPathway.Status = List.ListStatus.Current;
            CauseOfDeathConditionPathway.Mode = Hl7.Fhir.Model.ListMode.Snapshot;
            CauseOfDeathConditionPathway.Source = new ResourceReference("urn:uuid:" + Certifier.Id);
            CauseOfDeathConditionPathway.OrderedBy = new CodeableConcept("http://terminology.hl7.org/CodeSystem/list-order", "priority", "Sorted by Priority", null);

            // Add references back to the Decedent, Certifier, Certification, etc.
            AddReferenceToComposition(Decedent.Id);
            AddReferenceToComposition(InputRaceandEthnicity.Id);
            AddReferenceToComposition(Certifier.Id);
            AddReferenceToComposition(Pronouncer.Id);
            //AddReferenceToComposition(Mortician.Id);
            AddReferenceToComposition(DeathCertification.Id);
            AddReferenceToComposition(FuneralHome.Id);
            //AddReferenceToComposition(FuneralHomeDirector.Id);
            AddReferenceToComposition(CauseOfDeathConditionPathway.Id);
            AddReferenceToComposition(DispositionLocation.Id);
            Bundle.AddResourceEntry(Decedent, "urn:uuid:" + Decedent.Id);
            Bundle.AddResourceEntry(InputRaceandEthnicity, "urn:uuid:" + InputRaceandEthnicity.Id);
            Bundle.AddResourceEntry(Certifier, "urn:uuid:" + Certifier.Id);
            Bundle.AddResourceEntry(Pronouncer, "urn:uuid:" + Pronouncer.Id);
            //Bundle.AddResourceEntry(Mortician, "urn:uuid:" + Mortician.Id);
            Bundle.AddResourceEntry(DeathCertification, "urn:uuid:" + DeathCertification.Id);
            Bundle.AddResourceEntry(FuneralHome, "urn:uuid:" + FuneralHome.Id);
            //Bundle.AddResourceEntry(FuneralHomeDirector, "urn:uuid:" + FuneralHomeDirector.Id);
            Bundle.AddResourceEntry(CauseOfDeathConditionPathway, "urn:uuid:" + CauseOfDeathConditionPathway.Id);
            Bundle.AddResourceEntry(DispositionLocation, "urn:uuid:" + DispositionLocation.Id);

            // Create a Navigator for this new death record.
            Navigator = Bundle.ToTypedElement();

            UpdateBundleIdentifier();
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
            Boolean maybeXML = record.TrimStart().StartsWith("<");
            Boolean maybeJSON = record.TrimStart().StartsWith("{");
            if (!String.IsNullOrEmpty(record) && (maybeXML || maybeJSON))
            {
                // Grab all errors found by visiting all nodes and report if not permissive
                if (!permissive)
                {
                    List<string> entries = new List<string>();
                    ISourceNode node = null;
                    if (maybeXML) {
                        node = FhirXmlNode.Parse(record, new FhirXmlParsingSettings { PermissiveParsing = permissive });
                    }
                    else {
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
                    if(maybeXML) {
                        FhirXmlParser parser = new FhirXmlParser(parserSettings);
                        Bundle = parser.Parse<Bundle>(record);
                    }
                    else {
                        FhirJsonParser parser = new FhirJsonParser(parserSettings);
                        Bundle = parser.Parse<Bundle>(record);
                    }
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
        [Property("Identifier", Property.Types.String, "Death Certificate Document", "Death Certificate Number.", true, ProfileURL.DeathCertificateDocument, true, 3)]
        [FHIRPath("Bundle.entry.resource.where($this is Identifier)", "identifier")]
        public string Identifier
        {
            get
            {
                Identifier id = DeathCertification?.Identifier?.FirstOrDefault(i => i.Value != null && i.Value.Length > 0);
                return id == null ? null : id.Value;
            }
            set
            {
                DeathCertification.Identifier.Clear();
                Identifier identifier = new Identifier();
                identifier.Value = value;
                DeathCertification.Identifier.Add(identifier);
                UpdateBundleIdentifier();
            }
        }

        /// <summary>Update the bundle identifier from the component fields.</summary>
        private void UpdateBundleIdentifier()
        {
            uint certificateNumber = 0;
            UInt32.TryParse(this.Identifier, out certificateNumber);
            uint deathYear = 0;
            if (this.DateOfDeath != null)
            {
                if (this.DateOfDeath.Length >= 4)
                {
                    UInt32.TryParse(this.DateOfDeath.Substring(0,4), out deathYear);
                }
            }

            String jurisdictionId = this.DeathLocationJurisdiction; // this.DeathLocationAddress?["addressState"];
            if (jurisdictionId == null || jurisdictionId.Trim().Length < 2)
            {
                jurisdictionId = "XX";
            }
            else
            {
                jurisdictionId = jurisdictionId.Trim().Substring(0, 2).ToUpper();
            }
            this.BundleIdentifier = $"{deathYear.ToString("D4")}{jurisdictionId}{certificateNumber.ToString("D6")}";

        }

        /// <summary>Death Record Bundle Identifier, NCHS identifier.</summary>
        /// <value>a record bundle identification string.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.BundleIdentifier = "42";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"NCHS identifier: {ExampleDeathRecord.BundleIdentifier}");</para>
        /// </example>
        [Property("Bundle Identifier", Property.Types.String, "Death Certification", "NCHS identifier.", true, "http://build.fhir.org/ig/HL7/vrdr/StructureDefinition-VRDR-Death-Certificate-Document.html", true, 4)]
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
            private set
            {
                Identifier identifier = new Identifier();
                identifier.Value = value;
                identifier.System = "http://nchs.cdc.gov/vrdr_id";
                Bundle.Identifier = identifier;
            }
        }

        /// <summary>State Local Identifier.</summary>
        /// <value> a tuple array of state local identifiers
        /// <para>"item1" - the extension url for the local identifier</para>
        /// <para>"item2" - the string representation of the local identifier</para>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Tuple<string, string>[] ids = new Tuple<string,string>[]{Tuple.Create(ExtensionURL.AuxiliaryStateIdentifier2, "100000000001")};</para>
        /// <para>ExampleDeathRecord.StateLocalIdentifier = ids;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"State local identifier: {ExampleDeathRecord.StateLocalIdentifier[0].Item2}");</para>
        /// </example>
        [Property("State Local Identifier", Property.Types.TupleArr, "Death Certificate Document", "State Local Identifier.", true, ProfileURL.DeathCertificateDocument, true, 5)]
        [FHIRPath("Bundle", "identifier")]
        public Tuple<string, string>[] StateLocalIdentifier
        {
            get
            {
                // return first non-null/empty identifier value or null if none found
                List<Tuple<string, string>> identifiers = new List<Tuple<string, string>>();
                if (Bundle.Identifier != null)
                {
                    List<Extension> extensions = Bundle.Identifier.Extension.AsEnumerable().ToList();
                    foreach(Extension ext in extensions)
                    {
                        Tuple<string, string> id = Tuple.Create(ext.Url, ext.Value.ToString());
                        identifiers.Add(id);
                    }
                }     
                return identifiers.ToArray();
            }
            set
            {
                Bundle?.Identifier?.Extension.Clear();
                foreach(Tuple<string, string> element in value)
                {
                    Extension ext = new Extension();
                    ext.Url = element.Item1;
                    ext.Value = new FhirString(element.Item2);
                    if (Bundle.Identifier == null)
                    {
                        Identifier identifier = new Identifier();
                        Bundle.Identifier = identifier;
                    }
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
        [Property("Certified Time", Property.Types.StringDateTime, "Death Certification", "Certified time (i.e. certifier date signed).", true, "http://build.fhir.org/ig/HL7/vrdr/StructureDefinition-VRDR-Death-Certification.html", false, 12)]
        [FHIRPath("Bundle.entry.resource.where($this is Procedure).where(code.coding.code='308646001')", "")]
        public string CertifiedTime
        {
            get
            {
                if (DeathCertification != null && DeathCertification.Performed != null)
                {
                    return Convert.ToString(DeathCertification.Performed);
                } else if (Composition.Attester != null && Composition.Attester.FirstOrDefault() != null && Composition.Attester.First().Time != null)
                {
                    return Composition.Attester.First().Time;
                }
                return null;
            }
            set
            {
                if (DeathCertification == null)
                {
                    CreateDeathCertification();
                }

                Composition.Attester.First().Time = value;
                DeathCertification.Performed = new FhirDateTime(value);
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
        [Property("Registered Date/Time", Property.Types.StringDateTime, "Death Certification", "Date/Time of record registration.", true, "http://build.fhir.org/ig/HL7/vrdr/StructureDefinition-VRDR-Death-Certificate.html", true, 13)]
        [FHIRPath("Bundle.entry.resource.where($this is Composition)", "date")]
        public string RegisteredTime
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
        [Property("Certification Role", Property.Types.Dictionary, "Death Certification", "Certification Role.", true, "http://build.fhir.org/ig/HL7/vrdr/StructureDefinition-VRDR-Death-Certification.html", true, 4)]
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
                    return EmptyCodeDict();
                }
                Hl7.Fhir.Model.Procedure.PerformerComponent performer = DeathCertification.Performer.FirstOrDefault();
                if (performer != null && performer.Function != null)
                {
                    return CodeableConceptToDict(performer.Function);
                }
                return EmptyCodeDict();
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
        [Property("Certification Role", Property.Types.String, "Death Certification", "Certification Role.", true, "http://build.fhir.org/ig/HL7/vrdr/StructureDefinition-VRDR-Death-Certification.html", true, 4)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [FHIRPath("Bundle.entry.resource.where($this is Procedure).where(code.coding.code='308646001')", "performer")]
        public string CertificationRoleHelper
        {
            get
            {
                if (CertificationRole.ContainsKey("code"))
                {
                    string code = CertificationRole["code"] ;
                    if (code == "OTH"){
                        if (CertificationRole.ContainsKey("text")){
                            return (CertificationRole["text"]);
                        }
                        return("Other");
                    }
                }
                return null;
            }
            set
            {
                if ((value != null) && !VRDR.Mappings.CertifierTypes.FHIRToIJE.ContainsKey(value)){ //other
                    if (DeathCertification == null)
                    {
                        CreateDeathCertification();
                    }
                    CertificationRole = CodeableConceptToDict(new CodeableConcept(CodeSystems.NullFlavor_HL7_V3, "OTH", "Other", value));
                }
                else
                { // normal path
                    SetCodeValue("CertificationRole", value, VRDR.ValueSets.CertificationRole.Codes);
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
        [Property("Manner Of Death Type", Property.Types.Dictionary, "Death Certification", "Manner of Death Type.", true, "http://build.fhir.org/ig/HL7/vrdr/StructureDefinition-VRDR-Manner-of-Death.html", true, 49)]
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
                return EmptyCodeDict();
            }
            set
            {
                if (MannerOfDeath == null)
                {
                    MannerOfDeath = new Observation();
                    MannerOfDeath.Id = Guid.NewGuid().ToString();
                    MannerOfDeath.Meta = new Meta();
                    string[] mannerofdeath_profile = { IGURL.MannerOfDeath };
                    MannerOfDeath.Meta.Profile = mannerofdeath_profile;
                    MannerOfDeath.Status = ObservationStatus.Final;
                    MannerOfDeath.Code = new CodeableConcept(CodeSystems.LOINC, "69449-7", "Manner of death", null);
                    MannerOfDeath.Subject = new ResourceReference("urn:uuid:" + Decedent.Id);
                    MannerOfDeath.Performer.Add(new ResourceReference("urn:uuid:" + Certifier.Id));
                    MannerOfDeath.Value = DictToCodeableConcept(value);
                    AddReferenceToComposition(MannerOfDeath.Id);
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
        /// <para>Dictionary&lt;string, string&gt; manner = new Dictionary&lt;string, string&gt;();</para>
        /// <para>manner.Add("code", "7878000");</para>
        /// <para>manner.Add("system", "");</para>
        /// <para>manner.Add("display", "Accidental death");</para>
        /// <para>ExampleDeathRecord.MannerOfDeathTypeHelper = MannerOfDeath.Natural;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Manner Of Death Type: {ExampleDeathRecord.MannerOfDeathTypeHelper}");</para>
        /// </example>
        [Property("Manner Of Death Type", Property.Types.String, "Death Certification", "Manner of Death Type.", true, "http://build.fhir.org/ig/HL7/vrdr/StructureDefinition-VRDR-Manner-of-Death.html", true, 49)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='69449-7')", "")]

        public string MannerOfDeathTypeHelper
        {
            get
            {
                if (MannerOfDeathType.ContainsKey("code"))
                {
                    return MannerOfDeathType["code"];
                }
                return null;
            }
            set
            {
                SetCodeValue("MannerOfDeathType", value, VRDR.ValueSets.MannerOfDeath.Codes);
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
        [Property("Certifier Family Name", Property.Types.String, "Death Certification", "Family name of certifier.", true, "http://build.fhir.org/ig/HL7/vrdr/StructureDefinition-VRDR-Certifier.html", true, 6)]
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
        [Property("Certifier Suffix", Property.Types.String, "Death Certification", "Certifier's Suffix.", true, "http://build.fhir.org/ig/HL7/vrdr/StructureDefinition-VRDR-Certifier.html", true, 7)]
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
                return AddressToDict(Certifier.Address.FirstOrDefault());
            }
            set
            {
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
        [Property("Certifier Identifier", Property.Types.Dictionary, "Death Certification", "Certifier Identifier.", true, "http://build.fhir.org/ig/HL7/vrdr/StructureDefinition-VRDR-Certifier.html", false, 10)]
        [PropertyParam("system", "The identifier system.")]
        [PropertyParam("value", "The identifier value.")]
        [FHIRPath("Bundle.entry.resource.where($this is Practitioner).where(meta.profile='http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Certifier')", "identifier")]
        public Dictionary<string, string> CertifierIdentifier
        {
            get
            {
                Identifier identifier = Certifier.Identifier.FirstOrDefault();
                var result = new Dictionary<string, string>();
                if (identifier != null)
                {
                    result["system"] = identifier.System;
                    result["value"] = identifier.Value;
                }
                return result;
            }
            set
            {
                if (Certifier.Identifier.Count > 0)
                {
                    Certifier.Identifier.Clear();
                }
                if(value.ContainsKey("system") && value.ContainsKey("value")) {
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
        // [FHIRPath("Bundle.entry.resource.where($this is Practitioner).where(meta.profile='http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Certifier')", "qualification")]
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
        [Property("Contributing Conditions", Property.Types.String, "Death Certification", "Significant conditions that contributed to death but did not result in the underlying cause.", true, "http://build.fhir.org/ig/HL7/vrdr/StructureDefinition-VRDR-Condition-Contributing-To-Death.html", true, 100)]
        [FHIRPath("Bundle.entry.resource.where($this is Condition).where(onset.empty())", "")]
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
                    ConditionContributingToDeath.Code = (new CodeableConcept(CodeSystems.LOINC, "69441-4", "Other significant causes or conditions of death", null));
                    ConditionContributingToDeath.Value= new CodeableConcept(null, null, null, value);
                    AddReferenceToComposition(ConditionContributingToDeath.Id);
                    Bundle.AddResourceEntry(ConditionContributingToDeath, "urn:uuid:" + ConditionContributingToDeath.Id);
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
        [Property("Causes Of Death", Property.Types.TupleCOD, "Death Certification", "Conditions that resulted in the cause of death.", true, IGURL.CauseOfDeathPathway, true, 50)]
        [FHIRPath("Bundle.entry.resource.where($this is Condition).where(onset.empty().not())", "")]
        public Tuple<string, string /*, Dictionary<string, string>*/>[] CausesOfDeath
        {
            get
            {
                List<Tuple<string, string/*, Dictionary<string, string>*/>> results = new List<Tuple<string, string /*, Dictionary<string, string>*/>>();
                if (!String.IsNullOrEmpty(COD1A) || !String.IsNullOrEmpty(INTERVAL1A) /*|| (CODE1A != null && !String.IsNullOrEmpty(CODE1A["code"]))*/ )
                {
                    results.Add(Tuple.Create(COD1A, INTERVAL1A /*, CODE1A)*/));
                }
                if (!String.IsNullOrEmpty(COD1B) || !String.IsNullOrEmpty(INTERVAL1B) /*|| (CODE1B != null && !String.IsNullOrEmpty(CODE1B["code"])) */)
                {
                    results.Add(Tuple.Create(COD1B, INTERVAL1B/* , CODE1B*/));
                }
                if (!String.IsNullOrEmpty(COD1C) || !String.IsNullOrEmpty(INTERVAL1C) /*|| (CODE1C != null && !String.IsNullOrEmpty(CODE1C["code"])) */)
                {
                    results.Add(Tuple.Create(COD1C, INTERVAL1C /*, CODE1C */));
                }
                if (!String.IsNullOrEmpty(COD1D) || !String.IsNullOrEmpty(INTERVAL1D) /*||  (CODE1D != null && !String.IsNullOrEmpty(CODE1D["code"])) */)
                {
                    results.Add(Tuple.Create(COD1D, INTERVAL1D  /*, CODE1D */));
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
                        // CODE1A = value[0].Item3;
                    }
                    if (value.Length > 1)
                    {
                        COD1B = value[1].Item1;
                        INTERVAL1B = value[1].Item2;
                        // CODE1B = value[1].Item3;
                    }
                    if (value.Length > 2)
                    {
                        COD1C = value[2].Item1;
                        INTERVAL1C = value[2].Item2;
                        // CODE1C = value[2].Item3;
                    }
                    if (value.Length > 3)
                    {
                        COD1D = value[3].Item1;
                        INTERVAL1D = value[3].Item2;
                        // CODE1D = value[3].Item3;
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
                if(CauseOfDeathConditionA == null)
                {
                    CauseOfDeathConditionA = CauseOfDeathCondition(0);
                }
                CauseOfDeathConditionA.Value= new CodeableConcept(null, null, null, value);
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
                    var intervalComp = CauseOfDeathConditionA.Component.FirstOrDefault( entry => ((Observation.ComponentComponent)entry).Code != null &&
                       ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault() != null && ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault().Code == "69440-6" );
                    if (intervalComp?.Value != null && intervalComp.Value as CodeableConcept != null)
                    {
                        return (CodeableConceptToDict((CodeableConcept)intervalComp.Value))["text"];
                    }
                }
                return "";
            }
            set
            {
                if(CauseOfDeathConditionA == null)
                {
                    CauseOfDeathConditionA = CauseOfDeathCondition(0);
                }
                // Find correct component; if doesn't exist add another
                var intervalComp = CauseOfDeathConditionA.Component.FirstOrDefault( entry => ((Observation.ComponentComponent)entry).Code != null &&
                       ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault() != null && ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault().Code == "69440-6" );
                    if (intervalComp != null)
                    {

                    ((Observation.ComponentComponent)intervalComp).Value = new CodeableConcept(null, null, null, value);
                    }
                    else
                    {
                    Observation.ComponentComponent component = new Observation.ComponentComponent();
                    component.Code = new CodeableConcept(CodeSystems.LOINC, "69440-6", "Disease onset to death interval", null);
                    component.Value = new CodeableConcept(null, null, null, value);
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
                if(CauseOfDeathConditionB == null)
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
                    var intervalComp = CauseOfDeathConditionB.Component.FirstOrDefault( entry => ((Observation.ComponentComponent)entry).Code != null &&
                       ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault() != null && ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault().Code == "69440-6" );
                    if (intervalComp?.Value != null && intervalComp.Value as CodeableConcept != null)
                    {
                        return (CodeableConceptToDict((CodeableConcept)intervalComp.Value))["text"];
                    }
                }
                return "";
            }
            set
            {
                if(CauseOfDeathConditionB == null)
                {
                    CauseOfDeathConditionB = CauseOfDeathCondition(1);
                }
                // Find correct component; if doesn't exist add another
                var intervalComp = CauseOfDeathConditionB.Component.FirstOrDefault( entry => ((Observation.ComponentComponent)entry).Code != null &&
                       ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault() != null && ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault().Code == "69440-6" );
                    if (intervalComp != null)
                    {

                    ((Observation.ComponentComponent)intervalComp).Value = new CodeableConcept(null, null, null, value);
                    }
                    else
                    {
                    Observation.ComponentComponent component = new Observation.ComponentComponent();
                    component.Code = new CodeableConcept(CodeSystems.LOINC, "69440-6", "Disease onset to death interval", null);
                    component.Value = new CodeableConcept(null, null, null, value);
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
                if(CauseOfDeathConditionC == null)
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
                    var intervalComp = CauseOfDeathConditionC.Component.FirstOrDefault( entry => ((Observation.ComponentComponent)entry).Code != null &&
                       ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault() != null && ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault().Code == "69440-6" );
                    if (intervalComp?.Value != null && intervalComp.Value as CodeableConcept != null)
                    {
                        return (CodeableConceptToDict((CodeableConcept)intervalComp.Value))["text"];
                    }
                }
                return "";
            }
            set
            {
                if(CauseOfDeathConditionC == null)
                {
                    CauseOfDeathConditionC = CauseOfDeathCondition(2);
                }
                // Find correct component; if doesn't exist add another
                var intervalComp = CauseOfDeathConditionC.Component.FirstOrDefault( entry => ((Observation.ComponentComponent)entry).Code != null &&
                       ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault() != null && ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault().Code == "69440-6" );
                    if (intervalComp != null)
                    {

                    ((Observation.ComponentComponent)intervalComp).Value = new CodeableConcept(null, null, null, value);
                    }
                    else
                    {
                    Observation.ComponentComponent component = new Observation.ComponentComponent();
                    component.Code = new CodeableConcept(CodeSystems.LOINC, "69440-6", "Disease onset to death interval", null);
                    component.Value = new CodeableConcept(null, null, null, value);
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
                    var intervalComp = CauseOfDeathConditionD.Component.FirstOrDefault( entry => ((Observation.ComponentComponent)entry).Code != null &&
                       ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault() != null && ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault().Code == "69440-6" );
                    if (intervalComp?.Value != null && intervalComp.Value as CodeableConcept != null)
                    {
                        return (CodeableConceptToDict((CodeableConcept)intervalComp.Value))["text"];
                    }
                }
                return "";
            }
            set
            {
                if(CauseOfDeathConditionD == null)
                {
                    CauseOfDeathConditionD = CauseOfDeathCondition(3);
                }
                // Find correct component; if doesn't exist add another
                var intervalComp = CauseOfDeathConditionD.Component.FirstOrDefault( entry => ((Observation.ComponentComponent)entry).Code != null &&
                       ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault() != null && ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault().Code == "69440-6" );
                    if (intervalComp != null)
                    {

                    ((Observation.ComponentComponent)intervalComp).Value = new CodeableConcept(null, null, null, value);
                    }
                    else
                    {
                    Observation.ComponentComponent component = new Observation.ComponentComponent();
                    component.Code = new CodeableConcept(CodeSystems.LOINC, "69440-6", "Disease onset to death interval", null);
                    component.Value = new CodeableConcept(null, null, null, value);
                    CauseOfDeathConditionD.Component.Add(component);
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
        [Property("Given Names", Property.Types.StringArr, "Decedent Demographics", "Decedent's Given Name(s).", true, ProfileURL.Decedent, true, 0)]
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
        [Property("Family Name", Property.Types.String, "Decedent Demographics", "Decedent's Family Name.", true, ProfileURL.Decedent, true, 5)]
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

        /// <summary>Decedent's Suffix.</summary>
        /// <value>the decedent's suffix</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.Suffix = "Jr.";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Decedent Suffix: {ExampleDeathRecord.Suffix}");</para>
        /// </example>
        [Property("Suffix", Property.Types.String, "Decedent Demographics", "Decedent's Suffix.", true, ProfileURL.Decedent, true, 6)]
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
        [Property("Maiden Name", Property.Types.String, "Decedent Demographics", "Decedent's Maiden Name.", true, ProfileURL.Decedent, true, 10)]
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
        [Property("Gender", Property.Types.String, "Decedent Demographics", "Decedent's Gender.", true, ProfileURL.Decedent, true, 11)]
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

        /// <summary>Decedent's Sex at Death.</summary>
        /// <value>the decedent's sex at time of death</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.SexAtDeath = "F";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Sex at Time of Death: {ExampleDeathRecord.SexAtDeath}");</para>
        /// </example>
        [Property("Sex At Death", Property.Types.Dictionary, "Decedent Demographics", "Decedent's Sex at Death.", true, ProfileURL.Decedent, true, 12)]
        [FHIRPath("Bundle.entry.resource.where($this is Patient).extension.where(url='http://hl7.org/fhir/us/vrdr/StructureDefinition/NVSS-SexAtDeath')", "")]
        public Dictionary<string, string> SexAtDeath
        {
            get
            {
                Extension sex = Decedent.Extension.Find(ext => ext.Url == "http://hl7.org/fhir/us/vrdr/StructureDefinition/NVSS-SexAtDeath");
                if (sex != null && sex.Value != null && sex.Value as CodeableConcept != null)
                {
                    return CodeableConceptToDict((CodeableConcept)sex.Value);
                }
                return EmptyCodeDict();
            }
            set
            {
                Decedent.Extension.RemoveAll(ext => ext.Url == "http://hl7.org/fhir/us/vrdr/StructureDefinition/NVSS-SexAtDeath");
                Extension sex = new Extension();
                sex.Url = "http://hl7.org/fhir/us/vrdr/StructureDefinition/NVSS-SexAtDeath";
                sex.Value = DictToCodeableConcept(value);
                Decedent.Extension.Add(sex);
            }
        }

        /// <summary>Decedent's Sex At Death Helper</summary>
        /// <value>Decedent's sex at death.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.SexAtDeathHelper = VRDR.ValueSets.AdministractiveGender.Male;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Decedent's SexAtDeathHelper: {ExampleDeathRecord.SexAtDeathHelper}");</para>
        /// </example>
        [Property("Sex At Death", Property.Types.String, "Decedent Demographics", "Decedent's Sex At Death.", true, ProfileURL.Decedent, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [FHIRPath("Bundle.entry.resource.where($this is Patient).extension.where(url='http://hl7.org/fhir/us/vrdr/StructureDefinition/NVSS-SexAtDeath')", "")]
        public string SexAtDeathHelper
        {
            get
            {
                if (SexAtDeath.ContainsKey("code"))
                {
                    return SexAtDeath["code"];
                }
                return null;
            }
            set
            {
                SetCodeValue("SexAtDeath", value, VRDR.ValueSets.AdministrativeGender.Codes);
            }
        }

        // Should this be removed for IG v1.3 updates?
        /// <summary>Decedent's Birth Sex.</summary>
        /// <value>the decedent's birth sex</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.BirthSex = "F";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Birth Sex: {ExampleDeathRecord.BirthSex}");</para>
        /// </example>
        [Property("Birth Sex", Property.Types.String, "Decedent Demographics", "Decedent's Birth Sex.", true, ProfileURL.Decedent, true, 12)]
        [FHIRPath("Bundle.entry.resource.where($this is Patient).extension.where(url='http://hl7.org/fhir/us/core/StructureDefinition/us-core-birthsex')", "")]
        public string BirthSex
        {
            get
            {
                Extension birthsex = Decedent.Extension.Find(ext => ext.Url == "http://hl7.org/fhir/us/core/StructureDefinition/us-core-birthsex");
                if (birthsex != null && birthsex.Value != null && birthsex.Value.GetType() == typeof(Code))
                {
                    return ((Code)birthsex.Value).Value;
                }
                return null;
            }
            set
            {
                Decedent.Extension.RemoveAll(ext => ext.Url == "http://hl7.org/fhir/us/core/StructureDefinition/us-core-birthsex");
                Extension birthsex = new Extension();
                birthsex.Url = "http://hl7.org/fhir/us/core/StructureDefinition/us-core-birthsex";
                birthsex.Value = new Code(value);
                Decedent.Extension.Add(birthsex);
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
        [Property("Date Of Birth", Property.Types.String, "Decedent Demographics", "Decedent's Date of Birth.", true, "http://build.fhir.org/ig/HL7/vrdr/StructureDefinition-VRDR-Decedent.html", true, 14)]
        [FHIRPath("Bundle.entry.resource.where($this is Patient)", "birthDate")]
        public string DateOfBirth
        {
            get
            {
                return GetFirstString("Bundle.entry.resource.where($this is Patient).birthDate");
            }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    return;
                }
                if (Decedent.BirthDateElement == null){
                    Decedent.BirthDateElement = new Date();
                }
                Decedent.BirthDateElement.Value = value.Trim();
            }
        }


        /// <summary>Decedent's Date of Birth Date Part Absent Extension.</summary>
        /// <value>the decedent's date of birth date part absent reason</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.DateOfBirthDatePartReason = "1940-02-19";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Decedent Date of Birth Date Part Reason: {ExampleDeathRecord.DateOfBirthDatePartAbsent}");</para>
        /// </example>
        [Property("Date Of Birth Date Part Absent", Property.Types.TupleArr, "Decedent Demographics", "Decedent's Date of Birth Date Part.", true, ProfileURL.Decedent, true, 14)]
        [FHIRPath("Bundle.entry.resource.where($this is Patient)", "_birthDate")]
        public Tuple<string,string>[] DateOfBirthDatePartAbsent
        {
            get
            {
                if (Decedent.BirthDateElement != null){
                    Extension datePartAbsent = Decedent.BirthDateElement.Extension.Where(ext => ext.Url == "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Partial-date-part-absent-reason").FirstOrDefault();
                    return DatePartsToArray(datePartAbsent);
                }
                return null;
            }
            set
            {
                if (value != null && value.Length > 0)
                {
                    if (Decedent.BirthDateElement != null){
                        // Clear the previous date part absent and rebuild with the new data
                        Decedent.BirthDateElement.Extension.RemoveAll(ext => ext.Url == "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Partial-date-part-absent-reason");
                    }

                    Extension datePart = new Extension();
                    datePart.Url = "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Partial-date-part-absent-reason";
                    foreach (Tuple<string, string> element in value)
                    {
                        if (element != null)
                        {
                            Extension datePartDetails = new Extension();
                            datePartDetails.Url = element.Item1;
                            datePartDetails.Value = DatePartToIntegerOrCode(element);
                            datePart.Extension.Add(datePartDetails);
                        }

                    }
                    if (Decedent.BirthDateElement == null){
                        Decedent.BirthDateElement = new Date();
                    }
                    Decedent.BirthDateElement.Extension.Add(datePart);

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
        [Property("Residence", Property.Types.Dictionary, "Decedent Demographics", "Decedent's residence.", true, ProfileURL.Decedent, true, 19)]
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
                if (Decedent.Address == null){
                    Decedent.Address = new List<Address>();
                }
                Decedent.Address.Clear();
                Decedent.Address.Add(DictToAddress(value));


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
        [Property("Residence Within City Limits", Property.Types.Dictionary, "Decedent Demographics", "Decedent's residence is/is not within city limits.", true, ProfileURL.Decedent, true, 20)]
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
        [Property("ResidenceWithinCityLimits Helper", Property.Types.String, "Decedent Demographics", "Decedent's ResidenceWithinCityLimits.", true, ProfileURL.Decedent, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [FHIRPath("Bundle.entry.resource.where($this is Patient)", "address")]
        public string ResidenceWithinCityLimitsHelper
        {
            get
            {
                if (ResidenceWithinCityLimits.ContainsKey("code"))
                {
                    return ResidenceWithinCityLimits["code"];
                }
                return null;
            }
            set
            {
                SetCodeValue("ResidenceWithinCityLimits", value, VRDR.ValueSets.YesNoUnknown.Codes);
            }
        }

        // Todo remove in V1.3 IG updates
        /// <summary>Decedent's residence is/is not within city limits. This is a convenience method, to access the coded value use ResidenceWithinCityLimits.</summary>
        /// <value>Decedent's residence is/is not within city limits. A null value means "not applicable".</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>SetterDeathRecord.ResidenceWithinCityLimitsBoolean = true;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Residence within city limits: {ExampleDeathRecord.ResidenceWithinCityLimitsBoolean}");</para>
        /// </example>
        [Property("Residence Within City Limits Boolean", Property.Types.Bool, "Decedent Demographics", "Decedent's residence is/is not within city limits.", true, ProfileURL.Decedent, true, 21)]
        [FHIRPath("Bundle.entry.resource.where($this is Patient)", "address")]
        public bool? ResidenceWithinCityLimitsBoolean
        {
            get
            {
                var code = this.ResidenceWithinCityLimits;
                switch (code["code"])
                {
                    case "Y": // Yes
                        return true;
                    case "N": // No
                        return false;
                    default: // Not applicable
                        return null;
                }
            }
            set
            {
                var code = EmptyCodeDict();
                switch(value)
                {
                    case true:
                        code["code"] = "Y";
                        code["display"] = "Yes";
                        code["system"] = CodeSystems.PH_YesNo_HL7_2x;
                        break;
                    case false:
                        code["code"] = "N";
                        code["display"] = "No";
                        code["system"] = CodeSystems.PH_YesNo_HL7_2x;
                        break;
                    default:
                        code["code"] = "NA";
                        code["display"] = "not applicable";
                        code["system"] = CodeSystems.PH_NullFlavor_HL7_V3;
                        break;
                }
                this.ResidenceWithinCityLimits = code;
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
        [Property("SSN", Property.Types.String, "Decedent Demographics", "Decedent's Social Security Number.", true, ProfileURL.Decedent, true, 13)]
        [FHIRPath("Bundle.entry.resource.where($this is Patient).identifier.where(system='http://hl7.org/fhir/sid/us-ssn')", "")]
        public string SSN
        {
            get
            {
                return GetFirstString("Bundle.entry.resource.where($this is Patient).identifier.where(system = 'http://hl7.org/fhir/sid/us-ssn').value");
            }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    return;
                }
                Decedent.Identifier.RemoveAll(iden => iden.System == CodeSystems.US_SSN);
                Identifier ssn = new Identifier();
                ssn.Type = new CodeableConcept(CodeSystems.HL7_identifier_type, "SB", "Social Beneficiary Identifier", null);
                ssn.System = CodeSystems.US_SSN;
                ssn.Value = value.Replace("-", string.Empty);
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
        [Property("Ethnicity1", Property.Types.Dictionary, "Decedent Demographics", "Decedent's Ethnicity Hispanic Mexican.", true, ProfileURL.InputRaceAndEthnicity, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation)", "")]
        public Dictionary<string, string> Ethnicity1
        {
            get
            {
                if (InputRaceandEthnicity != null)
                {
                    Observation.ComponentComponent ethnicity = InputRaceandEthnicity.Component.FirstOrDefault(c => c.Code.Coding[0].Code == NvssEthnicity.Mexican);
                    if (ethnicity != null && ethnicity.Value != null && ethnicity.Value as CodeableConcept != null)
                    {
                        return CodeableConceptToDict((CodeableConcept)ethnicity.Value);
                    }
                }
                return EmptyCodeableDict();
            }
            set
            {
                if (InputRaceandEthnicity == null)
                {
                    CreateRaceEthnicityObs();
                }
                InputRaceandEthnicity.Component.RemoveAll(c => c.Code.Coding[0].Code == NvssEthnicity.Mexican);
                var coding = EmptyRaceEthnicityCodeDict();
                coding["code"] = NvssEthnicity.Mexican;
                coding["display"] = NvssEthnicity.Mexican;
                Observation.ComponentComponent component = new Observation.ComponentComponent();
                component.Code = DictToCodeableConcept(coding);
                component.Value = DictToCodeableConcept(value);
                InputRaceandEthnicity.Component.Add(component);
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
        [Property("Ethnicity 1 Helper", Property.Types.String, "Decedent Demographics", "Decedent's Ethnicity 1.", true, ProfileURL.InputRaceAndEthnicity, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation)", "")]
        public string Ethnicity1Helper
        {
            get
            {
                if (Ethnicity1.ContainsKey("code"))
                {
                    return Ethnicity1["code"];
                }
                return null;
            }
            set
            {
                SetCodeValue("Ethnicity1", value, VRDR.ValueSets.YesNoUnknown.Codes);
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
        [Property("Ethnicity2", Property.Types.Dictionary, "Decedent Demographics", "Decedent's Ethnicity Hispanic Puerto Rican.", true, ProfileURL.InputRaceAndEthnicity, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation)", "")]
        public Dictionary<string, string> Ethnicity2
        {
            get
            {
                if (InputRaceandEthnicity != null)
                {
                    Observation.ComponentComponent ethnicity = InputRaceandEthnicity.Component.FirstOrDefault(c => c.Code.Coding[0].Code == NvssEthnicity.PuertoRican);
                    if (ethnicity != null && ethnicity.Value != null && ethnicity.Value as CodeableConcept != null)
                    {
                        return CodeableConceptToDict((CodeableConcept)ethnicity.Value);
                    }
                }
                return EmptyCodeableDict();
            }
            set
            {
                if (InputRaceandEthnicity == null)
                {
                    CreateRaceEthnicityObs();
                }
                InputRaceandEthnicity.Component.RemoveAll(c => c.Code.Coding[0].Code == NvssEthnicity.PuertoRican);
                var coding = EmptyRaceEthnicityCodeDict();
                coding["code"] = NvssEthnicity.PuertoRican;
                coding["display"] = NvssEthnicity.PuertoRican;
                Observation.ComponentComponent component = new Observation.ComponentComponent();
                component.Code = DictToCodeableConcept(coding);
                component.Value = DictToCodeableConcept(value);
                InputRaceandEthnicity.Component.Add(component);
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
        [Property("Ethnicity 2 Helper", Property.Types.String, "Decedent Demographics", "Decedent's Ethnicity 2.", true, ProfileURL.InputRaceAndEthnicity, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation)", "")]
        public string Ethnicity2Helper
        {
            get
            {
                if (Ethnicity2.ContainsKey("code"))
                {
                    return Ethnicity2["code"];
                }
                return null;
            }
            set
            {
                SetCodeValue("Ethnicity2", value, VRDR.ValueSets.YesNoUnknown.Codes);
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
        [Property("Ethnicity3", Property.Types.Dictionary, "Decedent Demographics", "Decedent's Ethnicity Hispanic Cuban.", true, ProfileURL.InputRaceAndEthnicity, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation)", "")]
        public Dictionary<string, string> Ethnicity3
        {
            get
            {
                if (InputRaceandEthnicity != null)
                {
                    Observation.ComponentComponent ethnicity = InputRaceandEthnicity.Component.FirstOrDefault(c => c.Code.Coding[0].Code == NvssEthnicity.Cuban);
                    if (ethnicity != null && ethnicity.Value != null && ethnicity.Value as CodeableConcept != null)
                    {
                        return CodeableConceptToDict((CodeableConcept)ethnicity.Value);
                    }
                }
                return EmptyCodeableDict();
            }
            set
            {
                if (InputRaceandEthnicity == null)
                {
                    CreateRaceEthnicityObs();
                }
                InputRaceandEthnicity.Component.RemoveAll(c => c.Code.Coding[0].Code == NvssEthnicity.Cuban);
                var coding = EmptyRaceEthnicityCodeDict();
                coding["code"] = NvssEthnicity.Cuban;
                coding["display"] = NvssEthnicity.Cuban;
                Observation.ComponentComponent component = new Observation.ComponentComponent();
                component.Code = DictToCodeableConcept(coding);
                component.Value = DictToCodeableConcept(value);
                InputRaceandEthnicity.Component.Add(component);
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
        [Property("Ethnicity 3 Helper", Property.Types.String, "Decedent Demographics", "Decedent's Ethnicity 3.", true, ProfileURL.InputRaceAndEthnicity, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation)", "")]
        public string Ethnicity3Helper
        {
            get
            {
                if (Ethnicity3.ContainsKey("code"))
                {
                    return Ethnicity3["code"];
                }
                return null;
            }
            set
            {
                SetCodeValue("Ethnicity3", value, VRDR.ValueSets.YesNoUnknown.Codes);
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
        [Property("Ethnicity4", Property.Types.Dictionary, "Decedent Demographics", "Decedent's Ethnicity Hispanic Other.", true, ProfileURL.InputRaceAndEthnicity, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation)", "")]
        public Dictionary<string, string> Ethnicity4
        {
            get
            {
                if (InputRaceandEthnicity != null)
                {
                    Observation.ComponentComponent ethnicity = InputRaceandEthnicity.Component.FirstOrDefault(c => c.Code.Coding[0].Code == NvssEthnicity.Other);
                    if (ethnicity != null && ethnicity.Value != null && ethnicity.Value as CodeableConcept != null)
                    {
                        return CodeableConceptToDict((CodeableConcept)ethnicity.Value);
                    }
                }
                return EmptyCodeableDict();
            }
            set
            {
                if (InputRaceandEthnicity == null)
                {
                    CreateRaceEthnicityObs();
                }
                InputRaceandEthnicity.Component.RemoveAll(c => c.Code.Coding[0].Code == NvssEthnicity.Other);
                var coding = EmptyRaceEthnicityCodeDict();
                coding["code"] = NvssEthnicity.Other;
                coding["display"] = NvssEthnicity.Other;
                Observation.ComponentComponent component = new Observation.ComponentComponent();
                component.Code = DictToCodeableConcept(coding);
                component.Value = DictToCodeableConcept(value);
                InputRaceandEthnicity.Component.Add(component);
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
        [Property("Ethnicity 4 Helper", Property.Types.String, "Decedent Demographics", "Decedent's Ethnicity 4.", true, ProfileURL.InputRaceAndEthnicity, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation)", "")]
        public string Ethnicity4Helper
        {
            get
            {
                if (Ethnicity4.ContainsKey("code"))
                {
                    return Ethnicity4["code"];
                }
                return null;
            }
            set
            {
                SetCodeValue("Ethnicity4", value, VRDR.ValueSets.YesNoUnknown.Codes);
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
        [Property("EthnicityLiteral", Property.Types.String, "Decedent Demographics", "Decedent's Ethnicity Literal.", true, ProfileURL.InputRaceAndEthnicity, false, 34)]
        [PropertyParam("ethnicity", "The literal string to describe ethnicity.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation)", "")]
        public string EthnicityLiteral
        {
            get
            {
                if (InputRaceandEthnicity != null)
                {
                    Observation.ComponentComponent ethnicity = InputRaceandEthnicity.Component.FirstOrDefault(c => c.Code.Coding[0].Code == NvssEthnicity.Literal);
                    if (ethnicity != null && ethnicity.Value != null)
                    {
                        return ethnicity.Value.ToString();
                    }
                }
                return "";
            }
            set
            {
                if (InputRaceandEthnicity == null)
                {
                    CreateRaceEthnicityObs();
                }
                InputRaceandEthnicity.Component.RemoveAll(c => c.Code.Coding[0].Code == NvssEthnicity.Literal);
                var coding = EmptyRaceEthnicityCodeDict();
                coding["code"] = NvssEthnicity.Literal;
                coding["display"] = NvssEthnicity.Literal;
                Observation.ComponentComponent component = new Observation.ComponentComponent();
                component.Code = DictToCodeableConcept(coding);
                component.Value = new FhirString(value);
                InputRaceandEthnicity.Component.Add(component);
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
        [Property("Race", Property.Types.TupleArr, "Decedent Demographics", "Decedent's Race", true, ProfileURL.InputRaceAndEthnicity, true, 38)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='inputraceandethnicity')", "")]
        public Tuple<string, string>[] Race
        {
            get
            {
                // filter the boolean race values
                var booleanRaceCodes = NvssRace.GetBooleanRaceCodes();
                List<string> raceCodes = booleanRaceCodes.Concat(NvssRace.GetLiteralRaceCodes()).ToList();

                var races = new List<Tuple<string, string>>(){};

                if (InputRaceandEthnicity == null)
                {
                    return races.ToArray();
                }
                foreach(string raceCode in raceCodes)
                {
                    Observation.ComponentComponent component = InputRaceandEthnicity.Component.Where(c => c.Code.Coding[0].Code == raceCode).FirstOrDefault();
                    if (component != null)
                    {
                        // convert boolean race codes to strings
                        if (booleanRaceCodes.Contains(raceCode))
                        {

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
                            var race = Tuple.Create(raceCode, component.Value.ToString());
                            races.Add(race);
                        }

                    }
                }

                return races.ToArray();
            }
            set
            {
                if (InputRaceandEthnicity == null)
                {
                    CreateRaceEthnicityObs();
                }
                var booleanRaceCodes = NvssRace.GetBooleanRaceCodes();
                foreach (Tuple<string, string> element in value)
                {
                    InputRaceandEthnicity.Component.RemoveAll(c => c.Code.Coding[0].Code == element.Item1);
                    var coding = EmptyRaceEthnicityCodeDict();
                    coding["code"] = element.Item1;
                    coding["display"] = element.Item1;
                    Observation.ComponentComponent component = new Observation.ComponentComponent();
                    component.Code = DictToCodeableConcept(coding);
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
                    else
                    {
                        component.Value = new FhirString(element.Item2);
                    }
                    InputRaceandEthnicity.Component.Add(component);
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
        [Property("RaceMissingValueReason", Property.Types.Dictionary, "Decedent Demographics", "Decedent's Race MissingValueReason.", true, ProfileURL.InputRaceAndEthnicity, true, 38)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='MissingValueReason')", "")]
        public Dictionary<string, string> RaceMissingValueReason
        {
            get
            {
                if (InputRaceandEthnicity != null)
                {
                    Observation.ComponentComponent raceMVR = InputRaceandEthnicity.Component.FirstOrDefault(c => c.Code.Coding[0].Code == NvssRace.MissingValueReason);
                    if (raceMVR != null && raceMVR.Value != null && raceMVR.Value as CodeableConcept != null)
                    {
                        return CodeableConceptToDict((CodeableConcept)raceMVR.Value);
                    }
                }
                return EmptyCodeableDict();
            }
            set
            {
                if (InputRaceandEthnicity == null)
                {
                    CreateRaceEthnicityObs();
                }
                InputRaceandEthnicity.Component.RemoveAll(c => c.Code.Coding[0].Code == NvssRace.MissingValueReason);
                var coding = EmptyRaceEthnicityCodeDict();
                coding["code"] = NvssRace.MissingValueReason;
                coding["display"] = NvssRace.MissingValueReason;
                Observation.ComponentComponent component = new Observation.ComponentComponent();
                component.Code = DictToCodeableConcept(coding);
                component.Value = DictToCodeableConcept(value);
                InputRaceandEthnicity.Component.Add(component);
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
        [Property("RaceMissingValueReason", Property.Types.TupleArr, "Decedent Demographics", "Decedent's Race MissingValueReason.", true, ProfileURL.InputRaceAndEthnicity, true, 38)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='MissingValueReason')", "")]
        public string RaceMissingValueReasonHelper
        {
            get
            {
                if (RaceMissingValueReason.ContainsKey("code"))
                {
                    return RaceMissingValueReason["code"];
                }
                return null;
            }
            set
            {
                SetCodeValue("RaceMissingValueReason", value, VRDR.ValueSets.RaceMissingValueReason.Codes);
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
        [Property("Place Of Birth", Property.Types.Dictionary, "Decedent Demographics", "Decedent's Place Of Birth.", true, ProfileURL.Decedent, true, 15)]
        [PropertyParam("addressLine1", "address, line one")]
        [PropertyParam("addressLine2", "address, line two")]
        [PropertyParam("addressCity", "address, city")]
        [PropertyParam("addressCounty", "address, county")]
        [PropertyParam("addressState", "address, state")]
        [PropertyParam("addressZip", "address, zip")]
        [PropertyParam("addressCountry", "address, country")]
        [FHIRPath("Bundle.entry.resource.where($this is Patient).extension.where(url='http://hl7.org/fhir/StructureDefinition/patient-birthPlace')", "")]
        public Dictionary<string, string> PlaceOfBirth
        {
            get
            {
                Extension addressExt = Decedent.Extension.FirstOrDefault( extension => extension.Url == "http://hl7.org/fhir/StructureDefinition/patient-birthPlace" );
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
                Decedent.Extension.RemoveAll(ext => ext.Url == "http://hl7.org/fhir/StructureDefinition/patient-birthPlace");
                Extension placeOfBirthExt = new Extension();
                placeOfBirthExt.Url = "http://hl7.org/fhir/StructureDefinition/patient-birthPlace";
                placeOfBirthExt.Value = DictToAddress(value);
                Decedent.Extension.Add(placeOfBirthExt);
            }
        }

        /// <summary>The informant of the decedent's death.</summary>
        /// <value>String representation of the informant's relationship to the decedent
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.Contact = "Friend of family";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Contact's Relationship: {ExampleDeathRecord.Contact}");</para>
        /// </example>
        [Property("Contact Relationship", Property.Types.Dictionary, "Decedent Demographics", "The informant's relationship to the decedent", true, ProfileURL.Decedent, true, 24)]
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
                return null;
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
        [Property("Marital Status", Property.Types.Dictionary, "Decedent Demographics", "The marital status of the decedent at the time of death.", true, ProfileURL.Decedent, true, 24)]
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
                return EmptyCodeDict();
            }
            set
            {
                if (Decedent.MaritalStatus == null)
                {
                    Decedent.MaritalStatus = DictToCodeableConcept(value);
                }
                else
                {
                    string text = Decedent.MaritalStatus.Text;
                    Extension bypass = Decedent.MaritalStatus.Extension.FirstOrDefault();
                    Decedent.MaritalStatus = DictToCodeableConcept(value);
                    Decedent.MaritalStatus.Extension.Add(bypass);
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
        [Property("Marital Status Bypass", Property.Types.Dictionary, "Decedent Demographics", "The marital status edit flag.", true, ProfileURL.Decedent, true, 24)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Patient)", "maritalStatus")]
        public Dictionary<string, string> MaritalBypass
        {
            get
            {
                if (Decedent.MaritalStatus != null && Decedent.MaritalStatus.Extension.FirstOrDefault() != null)
                {
                    Extension addressExt = Decedent.MaritalStatus.Extension.FirstOrDefault(extension => extension.Url == ExtensionURL.BypassEditFlag);
                    if (addressExt != null && addressExt.Value != null && addressExt.Value as CodeableConcept != null)
                    {
                        return CodeableConceptToDict((CodeableConcept)addressExt.Value);
                    }
                }
                return EmptyCodeDict();
            }
            set
            {
                Extension ext = new Extension();
                ext.Url = ExtensionURL.BypassEditFlag;
                ext.Value = DictToCodeableConcept(value);
                if (Decedent.MaritalStatus == null){
                    Decedent.MaritalStatus = new CodeableConcept();
                }
                Decedent.MaritalStatus.Extension.Add(ext);
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
        [Property("Marital Status", Property.Types.String, "Decedent Demographics", "The marital status of the decedent at the time of death.", true, ProfileURL.Decedent, true, 24)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [FHIRPath("Bundle.entry.resource.where($this is Patient)", "maritalStatus")]
        public string MaritalStatusHelper
        {
            get
            {
                if (MaritalStatus.ContainsKey("code"))
                {
                    return MaritalStatus["code"];
                }
                return null;
            }
            set
            {
                SetCodeValue("MaritalStatus", value, VRDR.ValueSets.MaritalStatus.Codes);
            }
        }

        /// <summary>The marital bypass helper method.</summary>
        /// <value>the marital pass.:
        /// <para>"code" - the code describing this finding</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.MaritalBypassHelper = ValueSets.EditBypass0124.0;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Marital status: {ExampleDeathRecord.MaritalBypassHelper}");</para>
        /// </example>
        [Property("Marital Bypass", Property.Types.String, "Decedent Demographics", "The marital bypass.", true, ProfileURL.Decedent, true, 24)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [FHIRPath("Bundle.entry.resource.where($this is Patient)", "maritalStatus")]
        public string MaritalBypassHelper
        {
            get
            {
                if (MaritalBypass.ContainsKey("code"))
                {
                    return MaritalBypass["code"];
                }
                return null;
            }
            set
            {
                SetCodeValue("MaritalBypass", value, VRDR.ValueSets.EditBypass0124.Codes);
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
        [Property("Marital Status Literal", Property.Types.String, "Decedent Demographics", "The marital status of the decedent at the time of death.", true, ProfileURL.Decedent, true, 24)]
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
                return "";
            }
            set
            {
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
        [Property("Father Given Names", Property.Types.StringArr, "Decedent Demographics", "Given name(s) of decedent's father.", true, IGURL.DecedentFather , false, 28)]
        [FHIRPath("Bundle.entry.resource.where($this is RelatedPerson).where(relationship.coding.code='FTH')", "name")]
        public string[] FatherGivenNames
        {
            get
            {
                if (Father != null && Father.Name != null ) {
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
                   HumanName name = Father.Name.SingleOrDefault(n => n.Use == HumanName.NameUse.Official);
                if (name != null && value != null)
                {
                    name.Given = value;
                }
                else if (value != null)
                {
                    name = new HumanName();
                    name.Use = HumanName.NameUse.Official;
                    name.Given = value;
                    Father.Name.Add(name);
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
        [Property("Father Family Name", Property.Types.String, "Decedent Demographics", "Family name of decedent's father.", true, IGURL.DecedentFather, false, 29)]
        [FHIRPath("Bundle.entry.resource.where($this is RelatedPerson).where(relationship.coding.code='FTH')", "name")]
        public string FatherFamilyName
        {
           get
            {
                if (Father != null && Father.Name != null ) {
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
                if (Father != null && Father.Name != null ) {
                    string[] suffixes = GetAllString("Bundle.entry.resource.where($this is RelatedPerson).where(relationship.coding.code='FTH').name.where(use='official').suffix");
                    return suffixes != null ? suffixes[0] : null;
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
                if (Mother != null && Mother.Name != null ) {
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
                   HumanName name = Mother.Name.SingleOrDefault(n => n.Use == HumanName.NameUse.Official);
                if (name != null && value != null)
                {
                    name.Given = value;
                }
                else if (value != null)
                {
                    name = new HumanName();
                    name.Use = HumanName.NameUse.Official;
                    name.Given = value;
                    Mother.Name.Add(name);
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
        [Property("Mother Maiden Name", Property.Types.String, "Decedent Demographics", "Maiden name of decedent's mother.", true, IGURL.DecedentMother, false, 32)]
        [FHIRPath("Bundle.entry.resource.where($this is RelatedPerson).where(relationship.coding.code='MTH')", "name")]
        public string MotherMaidenName
        {
            get
            {

                if (Mother != null && Mother.Name != null ) {
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
                if (Mother != null && Mother.Name != null ) {
                    string[] suffixes = GetAllString("Bundle.entry.resource.where($this is RelatedPerson).where(relationship.coding.code='MTH').name.where(use='official').suffix");
                    return suffixes != null ? suffixes[0] : null;
                }
                return null;
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
                if (Spouse != null && Spouse.Name != null ) {
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
                   HumanName name = Spouse.Name.SingleOrDefault(n => n.Use == HumanName.NameUse.Official);
                if (name != null && value != null)
                {
                    name.Given = value;
                }
                else if (value != null)
                {
                    name = new HumanName();
                    name.Use = HumanName.NameUse.Official;
                    name.Given = value;
                    Spouse.Name.Add(name);
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
        [Property("Spouse Family Name", Property.Types.String, "Decedent Demographics", "Family name of decedent's spouse.", true, IGURL.DecedentSpouse, false, 26)]
        [FHIRPath("Bundle.entry.resource.where($this is RelatedPerson).where(relationship.coding.code='SPS')", "name")]
        public string SpouseFamilyName
        {
            get
            {
                if (Spouse != null && Spouse.Name != null ) {
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
                if (Spouse != null && Spouse.Name != null ) {
                    string[] suffixes = GetAllString("Bundle.entry.resource.where($this is RelatedPerson).where(relationship.coding.code='SPS').name.where(use='official').suffix");
                    return suffixes != null ? suffixes[0] : null;
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

                if (Spouse != null && Spouse.Name != null ) {
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
        [Property("Spouse Alive", Property.Types.Dictionary, "Decedent Demographics", "Spouse Alive", true, ProfileURL.Decedent, false, 27)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Patient)", "")]
        public Dictionary<string, string> SpouseAlive
        {
            get
            {
                Extension spouseExt = Decedent.Extension.FirstOrDefault( extension => extension.Url == ExtensionURL.SpouseAlive);
                if (spouseExt != null && spouseExt.Value != null && spouseExt.Value as CodeableConcept != null) {

                    return CodeableConceptToDict((CodeableConcept)spouseExt.Value);
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
        /// <para>Console.WriteLine($"Decedent's RaceMissingValueReason: {ExampleDeathRecord.RaceMissingValueReasonHelper}");</para>
        /// </example>
        [Property("Spouse Alive", Property.Types.String, "Decedent Demographics", "Spouse Alive", true, ProfileURL.Decedent, false, 27)]
        [FHIRPath("Bundle.entry.resource.where($this is Patient)", "")]
        public string SpouseAliveHelper
        {
            get
            {
                if (SpouseAlive.ContainsKey("code"))
                {
                    return SpouseAlive["code"];
                }
                return null;
            }
            set
            {
                SetCodeValue("SpouseAlive", value, VRDR.ValueSets.YesNoUnknownNotApplicable.Codes);
            }
        }

        /// <summary>Create an empty EducationLevel Observation, to be populated in either EducationLevel or EducationLevelEditFlag.</summary>
        private void CreateEmptyEducationLevelObservation()
        {
            DecedentEducationLevel = new Observation();
            DecedentEducationLevel.Id = Guid.NewGuid().ToString();
            DecedentEducationLevel.Meta = new Meta();
            string[] educationlevel_profile = { ProfileURL.DecedentEducationLevel };
            DecedentEducationLevel.Meta.Profile = educationlevel_profile;
            DecedentEducationLevel.Status = ObservationStatus.Final;
            DecedentEducationLevel.Code = new CodeableConcept(CodeSystems.LOINC, "80913-7", "Highest level of education [US Standard Certificate of Death]", null);
            DecedentEducationLevel.Subject = new ResourceReference("urn:uuid:" + Decedent.Id);
            AddReferenceToComposition(DecedentEducationLevel.Id);
            Bundle.AddResourceEntry(DecedentEducationLevel, "urn:uuid:" + DecedentEducationLevel.Id);
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
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='80913-7')", "")]
        public Dictionary<string, string> EducationLevel
        {
            get
            {
                if (DecedentEducationLevel != null && DecedentEducationLevel.Value != null && DecedentEducationLevel.Value as CodeableConcept != null)
                {
                    return CodeableConceptToDict((CodeableConcept)DecedentEducationLevel.Value);
                }
                return EmptyCodeDict();
            }
            set
            {
                if (DecedentEducationLevel == null)
                {
                    CreateEmptyEducationLevelObservation();
                }
                DecedentEducationLevel.Value = DictToCodeableConcept(value);
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
        [Property("Education Level", Property.Types.String, "Decedent Demographics", "Decedent's Education Level.", true, IGURL.DecedentEducationLevel, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='80913-7')", "")]
        public string EducationLevelHelper
        {
            get
            {
                if (EducationLevel.ContainsKey("code"))
                {
                    return EducationLevel["code"];
                }
                return null;
            }
            set
            {
                SetCodeValue("EducationLevel", value, VRDR.ValueSets.EducationLevel.Codes);
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
        // TODO: This URL should be the html one, and those should be generated as well
        [Property("Education Level Edit Flag", Property.Types.Dictionary, "Decedent Demographics", "Decedent's Education Level Edit Flag.", true, IGURL.DecedentEducationLevel, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='80913-7')", "")]
        public Dictionary<string, string> EducationLevelEditFlag
        {
            get
            {
                if (DecedentEducationLevel != null && DecedentEducationLevel.Extension != null)
                {
                    Extension editFlag = DecedentEducationLevel.Value.Extension.FirstOrDefault(extension => extension.Url == ExtensionURL.BypassEditFlag);
                    if (editFlag != null)
                    {
                        return CodeableConceptToDict((CodeableConcept)editFlag.Value);
                    }
                }
                return EmptyCodeableDict();
            }
            set
            {
                if (DecedentEducationLevel == null)
                {
                    CreateEmptyEducationLevelObservation();
                }
                if (DecedentEducationLevel.Value != null && DecedentEducationLevel.Value.Extension != null){
                    DecedentEducationLevel.Value.Extension.RemoveAll(ext => ext.Url == ExtensionURL.BypassEditFlag);
                }
                Extension editFlag = new Extension();
                editFlag.Url = ExtensionURL.BypassEditFlag;
                if(DecedentEducationLevel.Value == null){
                    DecedentEducationLevel.Value = new CodeableConcept();
                }
                editFlag.Value = DictToCodeableConcept(value);
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
        [Property("Education Level Edit Flag", Property.Types.String, "Decedent Demographics", "Decedent's Education Level Edit Flag.", true, IGURL.DecedentEducationLevel, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='80913-7')", "")]
        public string EducationLevelEditFlagHelper
        {
            get
            {
                if (EducationLevelEditFlag.ContainsKey("code"))
                {
                    return EducationLevelEditFlag["code"];
                }
                return null;
            }
            set
            {
                SetCodeValue("EducationLevelEditFlag", value, VRDR.ValueSets.EditBypass01234.Codes);
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
                    BirthRecordIdentifierDataAbsentBoolean = false;
                } else {
                    BirthRecordIdentifier.Value = (FhirString) null;
                    BirthRecordIdentifierDataAbsentBoolean = true;
                }
             }
        }
        /// <summary>Birth Record Identifier Data Absent Reason (Code).</summary>
        /// <value>Data Absent Reason for the Birth Record Identifier. A Dictionary representing a code, containing the following key/value pairs:
        /// <para>"code" - the code</para>
        /// <para>"system" - the code system this code belongs to</para>
        /// <para>"display" - a human readable meaning of the code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; brs = new Dictionary&lt;string, string&gt;();</para>
        /// <para>brs.Add("code", "unknown");</para>
        /// <para>code.Add("system", CodeSystems.Data_Absent_Reason_HL7_V3);</para>
        /// <para>brs.Add("display", "unknown");</para>
        /// <para>ExampleDeathRecord.BirthRecordIdentifierDataAbsentReason = brs;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Birth Record Data Absent Reason: {ExampleDeathRecord.BirthRecordIdentifierDataAbsentReason}");</para>
        /// </example>
        [Property("Birth Record Identifier Data Absent Reason (Code)", Property.Types.Dictionary, "Decedent Demographics", "Birth Record Identifier Data Absent Reason.", true, "http://build.fhir.org/ig/HL7/vrdr/StructureDefinition-VRDR-BirthRecordIdentifier.html", false, 17)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='BR').dataAbsentReason", "")]
        public Dictionary<string, string> BirthRecordIdentifierDataAbsentReason
        {
            get
            {
                if (BirthRecordIdentifier?.DataAbsentReason != null && BirthRecordIdentifier.DataAbsentReason as CodeableConcept != null)
                {
                    return CodeableConceptToDict(BirthRecordIdentifier.DataAbsentReason);
                }
                return EmptyCodeableDict();
            }
            set
            {
               if (BirthRecordIdentifier == null)
                {
                    CreateBirthRecordIdentifier();

                }
                // If an empty dict is provided then this will reset the `BirthRecordIdentifier.Value`
                // Since `EmptyCodeableDict()` is returned via the getter, implementers trying to
                // copy one death record to another would get their BirthRecordId cleared when copying this field.
                if(!IsDictEmptyOrDefault(value)) {
                    BirthRecordIdentifier.DataAbsentReason = DictToCodeableConcept(value);
                    BirthRecordIdentifier.Value = (FhirString) null; // If reason is set the data must be null;
                }
            }
        }

        /// <summary>Birth Record Identifier Data Absent Boolean.</summary>
        /// <value>True if the data absent reason field is present</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.BirthRecordIdentifierDataAbsentBoolean = true;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"BirthRecordIdentifierDataAbsentBoolean: {ExampleDeathRecord.BirthRecordIdentifierDataAbsentReason}");</para>
        /// </example>
        [Property("Birth Record Identifier Data Absent (Boolean)", Property.Types.Bool, "Decedent Demographics", "Birth Record Identifier Data Absent Reason.", true, "http://build.fhir.org/ig/HL7/vrdr/StructureDefinition-VRDR-BirthRecordIdentifier.html", false, 17)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='30525-0').dataAbsentReason", "")]
        public bool BirthRecordIdentifierDataAbsentBoolean
        {
           get
            {
                return (BirthRecordIdentifier == null || BirthRecordIdentifier.DataAbsentReason != null);
            }
            set
            {
                if (value == false)
                {
                    if (BirthRecordIdentifier != null) {  // if it has been created, reset the DataAbsentReason, otherwise, nothing to do.
                        BirthRecordIdentifier.DataAbsentReason = (CodeableConcept)null;
                    }
                }
                else
                {
                    if (BirthRecordIdentifier == null)
                    {
                        CreateBirthRecordIdentifier();

                    }
                    // If there already is a data absent reason do not overwrite it with the default
                    if(BirthRecordIdentifier.DataAbsentReason == null) {
                        BirthRecordIdentifierDataAbsentReason = new Dictionary<string, string>() {
                            { "system", CodeSystems.Data_Absent_Reason_HL7_V3 },
                            { "code", "unknown" },
                            { "display", "unknown" },
                            { "text", null }
                        };
                    }
                    BirthRecordIdentifier.Value = (FhirString) null; // If reason is set the data must be null;
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
        /// <para>Dictionary&lt;string, string&gt; brs = new Dictionary&lt;string, string&gt;();</para>
        /// <para>brs.Add("code", "US-MA");</para>
        /// <para>brs.Add("system", "urn:iso:std:iso:3166:-2");</para>
        /// <para>brs.Add("display", "Massachusetts");</para>
        /// <para>ExampleDeathRecord.BirthRecordState = brs;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Birth Record identification: {ExampleDeathRecord.BirthRecordState}");</para>
        /// </example>
        [Property("Birth Record State", Property.Types.Dictionary, "Decedent Demographics", "Birth Record State.", true, "http://build.fhir.org/ig/HL7/vrdr/StructureDefinition-VRDR-BirthRecordIdentifier.html", true, 17)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='BR')", "")]
        public Dictionary<string, string> BirthRecordState
        {
            get
            {
                if (BirthRecordIdentifier != null && BirthRecordIdentifier.Component.Count > 0)
                {
                    // Find correct component
                    var stateComp = BirthRecordIdentifier.Component.FirstOrDefault( entry => ((Observation.ComponentComponent)entry).Code != null && ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault() != null && ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault().Code == "21842-0" );
                    if (stateComp != null && stateComp.Value != null && stateComp.Value as CodeableConcept != null)
                    {
                        return(CodeableConceptToDict((CodeableConcept)stateComp.Value));
                    }
                }
                return EmptyCodeDict();
            }
            set
            {
               CodeableConcept state = DictToCodeableConcept(value);
               if (BirthRecordIdentifier == null)
                {
                    CreateBirthRecordIdentifier();
                    Observation.ComponentComponent component = new Observation.ComponentComponent();
                    component.Code = new CodeableConcept(CodeSystems.LOINC, "21842-0", "Birthplace", null);
                    component.Value = state;
                    BirthRecordIdentifier.Component.Add(component);
                }
                else
                {
                    // Find correct component; if doesn't exist add another
                    var stateComp = BirthRecordIdentifier.Component.FirstOrDefault( entry => ((Observation.ComponentComponent)entry).Code != null && ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault() != null && ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault().Code == "21842-0" );
                    if (stateComp != null)
                    {
                        ((Observation.ComponentComponent)stateComp).Value = state;
                    }
                    else
                    {
                        Observation.ComponentComponent component = new Observation.ComponentComponent();
                        component.Code = new CodeableConcept(CodeSystems.LOINC, "21842-0", "Birthplace", null);
                        component.Value = state;
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
        [Property("Birth Record Year", Property.Types.String, "Decedent Demographics", "Birth Record Year.", true, "http://build.fhir.org/ig/HL7/vrdr/StructureDefinition-VRDR-BirthRecordIdentifier.html", true, 18)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='BR')", "")]
        public string BirthRecordYear
        {
            get
            {
                if (BirthRecordIdentifier != null && BirthRecordIdentifier.Component.Count > 0)
                {
                    // Find correct component
                    var stateComp = BirthRecordIdentifier.Component.FirstOrDefault( entry => ((Observation.ComponentComponent)entry).Code != null && ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault() != null && ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault().Code == "80904-6" );
                    if (stateComp != null)
                    {
                        return Convert.ToString(stateComp.Value);
                    }
                }
                return null;
            }
            set
            {
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
                    var stateComp = BirthRecordIdentifier.Component.FirstOrDefault( entry => ((Observation.ComponentComponent)entry).Code != null && ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault() != null && ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault().Code == "80904-6" );
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
        /// <summary>Decedent's Usual Occupation (Code).</summary>
        /// <value>the decedent's usual occupation. A Dictionary representing a code, containing the following key/value pairs:
        /// <para>"code" - the code</para>
        /// <para>"system" - the code system this code belongs to</para>
        /// <para>"display" - a human readable meaning of the code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; uocc = new Dictionary&lt;string, string&gt;();</para>
        /// <para>uocc.Add("code", "1340");</para>
        /// <para>uocc.Add("system", "urn:oid:2.16.840.1.114222.4.11.7186");</para>
        /// <para>uocc.Add("display", "Biomedical engineers");</para>
        /// <para>ExampleDeathRecord.UsualOccupationCode = uocc;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Usual Occupation: {ExampleDeathRecord.UsualOccupationCode['display']}");</para>
        /// </example>
        [Property("Usual Occupation (Code)", Property.Types.Dictionary, "Decedent Demographics", "Decedent's Usual Occupation.", false, IGURL.DecedentUsualWork, false, 39)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [PropertyParam("text", "Additional descriptive text.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='21843-8')", "")]
        public Dictionary<string, string> UsualOccupationCode
        {
            get
            {
                if (UsualWork != null && UsualWork.Value != null && UsualWork.Value as CodeableConcept != null)
                {
                    return CodeableConceptToDict((CodeableConcept)UsualWork.Value);
                }
                return EmptyCodeDict();
            }
            set
            {
                if (UsualWork == null)
                {
                    CreateUsualWork();
                }
                UsualWork.Value = DictToCodeableConcept(value);

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
                var usualOccupationCode = UsualOccupationCode;
                if (usualOccupationCode.ContainsKey("text"))
                {
                    return UsualOccupationCode["text"];
                }
                else
                {
                    return null;
                }
            }
            set
            {
                var uocc = new Dictionary<string, string>();
                uocc["text"] = value;
                UsualOccupationCode = uocc;
            }
        }

        /// <summary>Start Date of Usual Occupation.</summary>
        /// <value>the date usual occupation started</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.UsualOccupationStart = "2018-02-19";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Start of Usual Occupation: {ExampleDeathRecord.UsualOccupationStart}");</para>
        /// </example>
        [Property("Usual Occupation Start Date", Property.Types.StringDateTime, "Decedent Demographics", "Decedent's Usual Occupation.", true, IGURL.DecedentUsualWork, true, 41)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='21843-8')", "")]
        public string UsualOccupationStart
        {
            get
            {
                if (UsualWork?.Effective != null && UsualWork.Effective as Period != null && ((Period)UsualWork.Effective).Start != null)
                {
                    return Convert.ToString(((Period)UsualWork.Effective).Start);
                }
                return null;
            }
            set
            {
                if (UsualWork == null)
                {
                    CreateUsualWork();
                    UsualWork.Effective = new Period(new FhirDateTime(value), null);
                }
                if (UsualWork.Effective as Period != null)
                {
                    ((Period)UsualWork.Effective).Start = value;
                }
            }
        }

        /// <summary>End Date of Usual Occupation.</summary>
        /// <value>the date usual occupation ended</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.UsualOccupationEnd = "2018-02-19";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"End of Usual Occupation: {ExampleDeathRecord.UsualOccupationEnd}");</para>
        /// </example>
        [Property("Usual Occupation End Date", Property.Types.StringDateTime, "Decedent Demographics", "Decedent's Usual Occupation.", true, IGURL.DecedentUsualWork, true, 42)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='21843-8')", "")]
        public string UsualOccupationEnd
        {
            get
            {
                if (UsualWork?.Effective != null && UsualWork.Effective as Period != null && ((Period)UsualWork.Effective).End != null)
                {
                    return Convert.ToString(((Period)UsualWork.Effective).End);
                }
                return null;
            }
            set
            {
                if (UsualWork == null)
                {
                    CreateUsualWork();
                    UsualWork.Effective = new Period(null, new FhirDateTime(value));
                }
                if (UsualWork.Effective as Period != null)
                {
                    ((Period)UsualWork.Effective).End = value;
                }
            }
        }
        /// <summary>Decedent's Usual Industry (Code).</summary>
        /// <value>the decedent's usual industry. A Dictionary representing a code, containing the following key/value pairs:
        /// <para>"code" - the code</para>
        /// <para>"system" - the code system this code belongs to</para>
        /// <para>"display" - a human readable meaning of the code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; uind = new Dictionary&lt;string, string&gt;();</para>
        /// <para>uind.Add("code", "7280");</para>
        /// <para>uind.Add("system", "urn:oid:2.16.840.1.114222.4.11.7187");</para>
        /// <para>uind.Add("display", "Accounting, tax preparation, bookkeeping, and payroll services");</para>
        /// <para>ExampleDeathRecord.UsualIndustryCode = uind;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Usual Industry: {ExampleDeathRecord.UsualIndustryCode['display']}");</para>
        /// </example>
        [Property("Usual Industry (Code)", Property.Types.Dictionary, "Decedent Demographics", "Decedent's Usual Industry.", false, IGURL.DecedentUsualWork, false, 43)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [PropertyParam("text", "Additional descriptive text.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='21843-8')", "")]
        public Dictionary<string, string> UsualIndustryCode
        {
            get
            {
                if (UsualWork != null)
                {
                    Observation.ComponentComponent component = UsualWork.Component.FirstOrDefault( cmp => cmp.Code!= null && cmp.Code.Coding != null && cmp.Code.Coding.Count() > 0 && cmp.Code.Coding.First().Code == "21844-6" );
                    if (component != null && component.Value != null && component.Value as CodeableConcept != null)
                    {
                        return CodeableConceptToDict((CodeableConcept)component.Value);
                    }
                }
                return EmptyCodeableDict();
            }
            set
            {
                if (UsualWork == null)
                {
                    CreateUsualWork();
                }

                UsualWork.Component.RemoveAll( cmp => cmp.Code!= null && cmp.Code.Coding != null && cmp.Code.Coding.Count() > 0 && cmp.Code.Coding.First().Code == "21844-6" );
                Observation.ComponentComponent component = new Observation.ComponentComponent();
                component.Code = new CodeableConcept(CodeSystems.LOINC, "21844-6", "History of Usual industry", null);

                component.Value = DictToCodeableConcept(value);
                UsualWork.Component.Add(component);

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
                var usualIndustryCode = UsualIndustryCode;
                if (usualIndustryCode.ContainsKey("text"))
                {
                    return UsualIndustryCode["text"];
                }
                else
                {
                    return null;
                }
            }
            set
            {
                var uicc = new Dictionary<string, string>();
                uicc["text"] = value;
                UsualIndustryCode = uicc;
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
        [Property("Military Service", Property.Types.Dictionary, "Decedent Demographics", "Decedent's Military Service.", true, "http://build.fhir.org/ig/HL7/vrdr/StructureDefinition-VRDR-Decedent-Military-Service.html", false, 22)]
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
                return EmptyCodeDict();
            }
            set
            {
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
                    AddReferenceToComposition(MilitaryServiceObs.Id);
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
        /// <para>ExampleDeathRecord.MilitaryServiceHelper = Y;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Military Service: {ExampleDeathRecord.MilitaryServiceHelper}");</para>
        /// </example>
        [Property("Military Service Helper", Property.Types.String, "Decedent Demographics", "Decedent's Military Service.", true, "http://build.fhir.org/ig/HL7/vrdr/StructureDefinition-VRDR-Decedent-Military-Service.html", false, 23)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='55280-2')", "")]
         public string MilitaryServiceHelper
        {
            get
            {
                if (MilitaryService.ContainsKey("code"))
                {
                    return(MilitaryService["code"]);
                }
                return null;
            }
            set
            {
                SetCodeValue("MilitaryService", value, VRDR.ValueSets.YesNoUnknown.Codes);
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
        // [Property("Mortician Given Names", Property.Types.StringArr, "Decedent Disposition", "Given name(s) of mortician.", true, "http://build.fhir.org/ig/HL7/vrdr/StructureDefinition-VRDR-Mortician.html", false, 96)]
        // [FHIRPath("Bundle.entry.resource.where($this is Practitioner).where(meta.profile='http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Mortician')", "name")]
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
        // [FHIRPath("Bundle.entry.resource.where($this is Practitioner).where(meta.profile='http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Mortician')", "name")]
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
        // [FHIRPath("Bundle.entry.resource.where($this is Practitioner).where(meta.profile='http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Mortician')", "suffix")]
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
        // [FHIRPath("Bundle.entry.resource.where($this is Practitioner).where(meta.profile='http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Mortician')", "identifier")]
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
        //         string[] mortician_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Mortician" };
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
        [Property("Funeral Home Name", Property.Types.String, "Decedent Disposition", "Name of Funeral Home.", true, "http://build.fhir.org/ig/HL7/vrdr/StructureDefinition-VRDR-Funeral-Home.html", false, 94)]
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
        //             string[] funeralhomedirector_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Funeral-Service-Licensee" };
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
        [FHIRPath("Bundle.entry.resource.where($this is Location).where(meta.profile='http://hl7.org/fhir/us/vrdr/StructureDefinition/vrdr-disposition-location')", "address")]
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
                    CreateDispositionLocation();
                }

                DispositionLocation.Name = value;
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
        [Property("Decedent Disposition Method", Property.Types.Dictionary, "Decedent Disposition", "Decedent's Disposition Method.", true, IGURL.DispositionLocation, true, 1)]
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
                return EmptyCodeDict();
            }
            set
            {
                if (DispositionMethod == null)
                {
                    DispositionMethod = new Observation();
                    DispositionMethod.Id = Guid.NewGuid().ToString();
                    DispositionMethod.Meta = new Meta();
                    string[] dispositionmethod_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Decedent-Disposition-Method" };
                    DispositionMethod.Meta.Profile = dispositionmethod_profile;
                    DispositionMethod.Status = ObservationStatus.Final;
                    DispositionMethod.Code = new CodeableConcept(CodeSystems.LOINC, "80905-3", "Body disposition method", null);
                    DispositionMethod.Subject = new ResourceReference("urn:uuid:" + Decedent.Id);
//                    DispositionMethod.Performer.Add(new ResourceReference("urn:uuid:" + Mortician.Id));
                    DispositionMethod.Value = DictToCodeableConcept(value);
                    LinkObservationToLocation(DispositionMethod, DispositionLocation);
                    AddReferenceToComposition(DispositionMethod.Id);
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
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.DecedentDispositionMethodHelper = dmethod;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Decedent Disposition Method: {ExampleDeathRecord.DecedentDispositionMethodHelper}");</para>
        /// </example>
        /// </value>
        [Property("Decedent Disposition Method", Property.Types.String, "Decedent Disposition", "Decedent's Disposition Method.", true, IGURL.DispositionLocation, true, 1)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='80905-3')", "")]
        public string DecedentDispositionMethodHelper
        {
            get
            {
                if (DecedentDispositionMethod.ContainsKey("code"))
                {
                    return DecedentDispositionMethod["code"];
                }
                return null;
            }
            set
            {
                SetCodeValue("DecedentDispositionMethod", value, VRDR.ValueSets.MethodsOfDisposition.Codes);
            }
        }

        private void LinkObservationToLocation(Observation observation, Location location)
        {
            if (observation == null || location == null)
            {
                return;
            }

            Extension extension = null;
            foreach (Extension ext in observation.Extension)
            {
                if (ext.Url == "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Observation-Location")
                {
                    extension = ext;
                    break;
                }
            }
            if (extension == null)
            {
                extension = new Extension();
                extension.Url = "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Observation-Location";
                observation.Extension.Add(extension);
            }
            extension.Value = new ResourceReference("urn:uuid:" + location.Id);
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
        [Property("Autopsy Performed Indicator", Property.Types.Dictionary, "Death Investigation", "Autopsy Performed Indicator.", true, "http://build.fhir.org/ig/HL7/vrdr/StructureDefinition-VRDR-Autopsy-Performed-Indicator.html", true, 28)]
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
                return EmptyCodeDict();
            }
            set
            {
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
        /// <para>ExampleDeathRecord.AutopsyPerformedIndicatorBoolean = true;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Autopsy Performed Indicator: {ExampleDeathRecord.AutopsyPerformedIndicatorBoolean}");</para>
        /// </example>
        [Property("Autopsy Performed Indicator Helper", Property.Types.String, "Death Investigation", "Autopsy Performed Indicator.", true, IGURL.AutopsyPerformedIndicator, true, 29)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='85699-7')", "")]
        public string AutopsyPerformedIndicatorHelper
        {
            get
            {
                if (AutopsyPerformedIndicator.ContainsKey("code"))
                {
                    return AutopsyPerformedIndicator["code"];
                }
                return null;
            }
            set
            {
                SetCodeValue("AutopsyPerformedIndicator", value, VRDR.ValueSets.YesNoUnknown.Codes);
            }
        }

        /// <summary>Given name(s) of Pronouncer.</summary>
        /// <value>the Pronouncer's name (first, middle, etc.)</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>string[] names = { "FD", "Middle" };</para>
        /// <para>ExampleDeathRecord.PronouncerGivenNames = names;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Pronouncer Given Name(s): {string.Join(", ", ExampleDeathRecord.PronouncerGivenNames)}");</para>
        /// </example>
        [Property("Pronouncer Given Names", Property.Types.StringArr, "Death Investigation", "Given name(s) of Pronouncer.", true, "http://build.fhir.org/ig/HL7/vrdr/StructureDefinition-VRDR-Death-Pronouncement-Performer.html", false, 21)]
        [FHIRPath("Bundle.entry.resource.where($this is Practitioner).where(meta.profile='http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Death-Pronouncement-Performer')", "name")]
        public string[] PronouncerGivenNames
        {
            get
            {
                if (Pronouncer != null && Pronouncer.Name.Count() > 0)
                {
                    return Pronouncer.Name.First().Given.ToArray();
                }
                return new string[0];
            }
            set
            {
                HumanName name = Pronouncer.Name.SingleOrDefault(n => n.Use == HumanName.NameUse.Official);
                if (name != null)
                {
                    name.Given = value;
                }
                else
                {
                    name = new HumanName();
                    name.Use = HumanName.NameUse.Official;
                    name.Given = value;
                    Pronouncer.Name.Add(name);
                }
            }
        }

        /// <summary>Family name of Pronouncer.</summary>
        /// <value>the Pronouncer's family name (i.e. last name)</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.PronouncerFamilyName = "Last";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Pronouncer's Last Name: {ExampleDeathRecord.PronouncerFamilyName}");</para>
        /// </example>
        [Property("Pronouncer Family Name", Property.Types.String, "Death Investigation", "Family name of Pronouncer.", true, "http://build.fhir.org/ig/HL7/vrdr/StructureDefinition-VRDR-Death-Pronouncement-Performer.html", false, 22)]
        [FHIRPath("Bundle.entry.resource.where($this is Practitioner).where(meta.profile='http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Death-Pronouncement-Performer')", "name")]
        public string PronouncerFamilyName
        {
            get
            {
                if (Pronouncer != null && Pronouncer.Name.Count() > 0)
                {
                    return Pronouncer.Name.First().Family;
                }
                return null;
            }
            set
            {
                HumanName name = Pronouncer.Name.FirstOrDefault();
                if (name != null && !String.IsNullOrEmpty(value))
                {
                    name.Family = value;
                }
                else if (!String.IsNullOrEmpty(value))
                {
                    name = new HumanName();
                    name.Use = HumanName.NameUse.Official;
                    name.Family = value;
                    Pronouncer.Name.Add(name);
                }
            }
        }

        /// <summary>Pronouncer's Suffix.</summary>
        /// <value>the Pronouncer's suffix</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.PronouncerSuffix = "Jr.";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Pronouncer Suffix: {ExampleDeathRecord.PronouncerSuffix}");</para>
        /// </example>
        [Property("Pronouncer Suffix", Property.Types.String, "Death Investigation", "Pronouncer's Suffix.", true, "http://build.fhir.org/ig/HL7/vrdr/StructureDefinition-VRDR-Death-Pronouncement-Performer.html", false, 23)]
        [FHIRPath("Bundle.entry.resource.where($this is Practitioner).where(meta.profile='http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Death-Pronouncement-Performer')", "suffix")]
        public string PronouncerSuffix
        {
            get
            {
                if (Pronouncer != null && Pronouncer.Name.Count() > 0 && Pronouncer.Name.First().Suffix.Count() > 0)
                {
                    return Pronouncer.Name.First().Suffix.First();
                }
                return null;
            }
            set
            {
                HumanName name = Pronouncer.Name.FirstOrDefault();
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
                    Pronouncer.Name.Add(name);
                }
            }
        }

        /// <summary>Pronouncer Identifier.</summary>
        /// <value>the Pronouncer identification. A Dictionary representing a system (e.g. NPI) and a value, containing the following key/value pairs:
        /// <para>"system" - the identifier system, e.g. US NPI</para>
        /// <para>"value" - the identifier value, e.g. US NPI number</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; identifier = new Dictionary&lt;string, string&gt;();</para>
        /// <para>identifier.Add("system", "http://hl7.org/fhir/sid/us-npi");</para>
        /// <para>identifier.Add("value", "1234567890");</para>
        /// <para>ExampleDeathRecord.PronouncerIdentifier = identifier;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"\tPronouncer Identifier: {ExampleDeathRecord.PronouncerIdentifier['value']}");</para>
        /// </example>
        [Property("Pronouncer Identifier", Property.Types.Dictionary, "Death Investigation", "Pronouncer Identifier.", true, "http://build.fhir.org/ig/HL7/vrdr/StructureDefinition-VRDR-Death-Pronouncement-Performer.html", false, 24)]
        [PropertyParam("system", "The identifier system.")]
        [PropertyParam("value", "The identifier value.")]
        [FHIRPath("Bundle.entry.resource.where($this is Practitioner).where(meta.profile='http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Death-Pronouncement-Performer')", "identifier")]
        public Dictionary<string, string> PronouncerIdentifier
        {
            get
            {
                Identifier identifier = Pronouncer?.Identifier?.FirstOrDefault();
                var result = new Dictionary<string, string>();
                if (identifier != null)
                {
                    result["system"] = identifier.System;
                    result["value"] = identifier.Value;
                }
                return result;
            }
            set
            {
                if (Pronouncer.Identifier.Count > 0)
                {
                    Pronouncer.Identifier.Clear();
                }
                if(value.ContainsKey("system") && value.ContainsKey("value")) {
                    Identifier identifier = new Identifier();
                    identifier.System = value["system"];
                    identifier.Value = value["value"];
                    Pronouncer.Identifier.Add(identifier);
                }
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
        [Property("Date/Time Of Death", Property.Types.StringDateTime, "Death Investigation", "Decedent's Date and Time of Death.", true, "http://build.fhir.org/ig/HL7/vrdr/StructureDefinition-VRDR-Death-Date.html", true, 25)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='81956-5')", "")]
        public string DateOfDeath
        {
            get
            {
                if (DeathDateObs != null)
                {
                    return Convert.ToString(DeathDateObs.Value);
                }
                return null;
            }
            set
            {
                if (DeathDateObs == null)
                {
                    CreateDeathDateObs();
                    if (Pronouncer != null)
                    {
                        DeathDateObs.Performer.Add(new ResourceReference("urn:uuid:" + Pronouncer.Id));
                    }
                    LinkObservationToLocation(DeathDateObs, DeathLocationLoc);
                }

                DeathDateObs.Value = new FhirDateTime(value);
                DeathDateObs.Effective = new FhirDateTime(value);

                UpdateBundleIdentifier();
            }
        }

        /// <summary>Decedent's Date of Death Date Part Absent Extension.</summary>
        /// <value>the decedent's date of death date part absent reason</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.DateOfDeathDatePartReason = "2021-02-19";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Decedent Date of Death Date Part Reason: {ExampleDeathRecord.DateOfDeathDatePartAbsent}");</para>
        /// </example>
        [Property("Date Of Death Date Part Absent", Property.Types.TupleArr, "Death Investigation", "Decedent's Date of Birth Date Part.", true, "http://build.fhir.org/ig/HL7/vrdr/StructureDefinition-VRDR-Death-Date.html", true, 14)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='81956-5')", "_valueDateTime")]
        public Tuple<string,string>[] DateOfDeathDatePartAbsent
        {
            get
            {
                if (DeathDateObs != null && DeathDateObs.Value != null){
                    Extension datePartAbsent = DeathDateObs.Value.Extension.Where(ext => ext.Url == "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Partial-date-part-absent-reason").FirstOrDefault();
                    return DatePartsToArray(datePartAbsent);
                }
                return null;
            }
            set
            {
                if (DeathDateObs == null)
                {
                    CreateDeathDateObs();
                }
                if (value != null && value.Length > 0)
                {
                    if (DeathDateObs.Value != null)
                    {
                        DeathDateObs.Value.Extension.RemoveAll(ext => ext.Url == "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Partial-date-part-absent-reason");
                    }
                    Extension datePart = new Extension();
                    datePart.Url = "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Partial-date-part-absent-reason";
                    foreach (Tuple<string, string> element in value)
                    {
                        if (element != null)
                        {
                            Extension datePartDetails = new Extension();
                            datePartDetails.Url = element.Item1;
                            datePartDetails.Value = DatePartToIntegerOrCode(element);
                            datePart.Extension.Add(datePartDetails);
                        }

                    }
                    if (DeathDateObs.Value == null){
                        DeathDateObs.Value = new FhirDateTime();
                    }
                    DeathDateObs.Value.Extension.Add(datePart);
                    // set effective date to some arbitrary date since it is a required field
                    // TODO the IG should be updated to put the extension on the Effective Date instead of value
                    // since effective date is the required field
                    DeathDateObs.Effective = new FhirDateTime("2021-01-01T00:00:00-00:00");

                }

            }

        }

        /// <summary>Decedent's Date/Time of Death Pronouncement.</summary>
        /// <value>the decedent's date and time of death pronouncement</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.DateOfDeathPronouncement = "2018-02-20T16:48:06-05:00";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Decedent Date of Death Pronouncement: {ExampleDeathRecord.DateOfDeathPronouncement}");</para>
        /// </example>
        [Property("Date/Time Of Death Pronouncement", Property.Types.StringDateTime, "Death Investigation", "Decedent's Date/Time of Death Pronouncement.", true, "http://build.fhir.org/ig/HL7/vrdr/StructureDefinition-VRDR-Death-Date.html", false, 20)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='80616-6')", "")]
        public string DateOfDeathPronouncement
        {
         get
            {
                if (DeathDateObs != null && DeathDateObs.Component.Count > 0) // if there is a value for death location type, return it
                {
                    var pronComp = DeathDateObs.Component.FirstOrDefault( entry => ((Observation.ComponentComponent)entry).Code != null
                         && ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault() != null && ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault().Code == "80616-6" );
                    if (pronComp != null && pronComp.Value != null)
                    {
                        return Convert.ToString(pronComp.Value);
                    }
                }
                return null;
            }
            set
            {

                if (DeathDateObs == null)
                {
                    CreateDeathDateObs(); // Create it
                }

                // Find correct component; if doesn't exist add another
                var pronComp = DeathDateObs.Component.FirstOrDefault( entry => ((Observation.ComponentComponent)entry).Code != null
                         && ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault() != null && ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault().Code == "80616-6" );
                if (pronComp != null)
                {
                    ((Observation.ComponentComponent)pronComp).Value = new FhirDateTime(value);
                }
                else
                {
                    Observation.ComponentComponent component = new Observation.ComponentComponent();
                    component.Code = new CodeableConcept(CodeSystems.LOINC, "80616-6", "Date and time pronounced dead [US Standard Certificate of Death]", null);
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
        /// <para>code.Add("system", CodeSystems.PH_YesNo_HL7_2x);</para>
        /// <para>code.Add("display", "Yes");</para>
        /// <para>ExampleDeathRecord.AutopsyResultsAvailable = code;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Autopsy Results Available: {ExampleDeathRecord.AutopsyResultsAvailable['display']}");</para>
        /// </example>
        [Property("Autopsy Results Available", Property.Types.Dictionary, "Death Investigation", "Autopsy results available, used to complete cause of death.", true, "http://build.fhir.org/ig/HL7/vrdr/StructureDefinition-VRDR-Autopsy-Performed-Indicator.html", true, 30)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='85699-7')", "")]
        public Dictionary<string, string> AutopsyResultsAvailable
        {
            get
            {
                if (AutopsyPerformed != null && AutopsyPerformed.Component.Count() > 0 && AutopsyPerformed.Component.First().Value as CodeableConcept != null)
                {
                    return CodeableConceptToDict((CodeableConcept)AutopsyPerformed.Component.First().Value);
                }
                return EmptyCodeDict();
            }
            set
            {
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
        [Property("Autopsy Results Available Helper", Property.Types.String, "Death Investigation", "Autopsy results available, used to complete cause of death.", true, IGURL.AutopsyPerformedIndicator, true, 31)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='85699-7')", "")]
        public string AutopsyResultsAvailableHelper
        {
            get
            {
                if (AutopsyResultsAvailable.ContainsKey("code"))
                {
                    return AutopsyResultsAvailable["code"];
                }
                return null;
            }
            set
            {
                SetCodeValue("AutopsyResultsAvailable", value, VRDR.ValueSets.YesNoUnknownNotApplicable.Codes);
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
        [Property("Death Location Jurisdiction", Property.Types.String, "Death Investigation", "Vital Records Jurisdiction of Death Location (two character jurisdiction code, e.g. CA).", true, locationJurisdictionExtPath, false, 16)]
        [FHIRPath("Bundle.entry.resource.where($this is Location).where(meta.profile='http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Death-Location')", "extension")]
        public string DeathLocationJurisdiction
        {
            get
            {
                if (DeathLocationLoc != null && DeathLocationLoc.Address != null )
                {
                    string stateName = "";
                    if(DeathLocationLoc.Address.State != null){
                        stateName = DeathLocationLoc.Address.State;
                    }
                    if (DeathLocationLoc.Address.StateElement != null)
                    {
                        Extension jurisdiction = DeathLocationLoc.Address.StateElement.Extension.Find(ext => ext.Url == locationJurisdictionExtPath);
                        if(jurisdiction != null && jurisdiction.Value != null){
                            stateName = jurisdiction.Value.ToString();
                        }
                    }
                    if(!String.IsNullOrWhiteSpace(stateName)){
                        return stateName;
                    }
                }
                return null;
            }
            set
            {
                if (DeathLocationLoc == null)
                {
                    CreateDeathLocation();
                    DeathLocationLoc.Address = DictToAddress(EmptyAddrDict());
                    DeathLocationLoc.Address.StateElement = new FhirString();
                }
                else
                {
                    DeathLocationLoc.Address.StateElement.Extension.RemoveAll(ext => ext.Url == locationJurisdictionExtPath);
                }
                if (!String.IsNullOrWhiteSpace(value)) // If a jurisdiction is provided, create and add the extension
                {
                    if (value == "YC"){
                        DeathLocationLoc.Address.State = "NY";
                        Extension extension = new Extension();
                        extension.Url = locationJurisdictionExtPath;
                        extension.Value = new FhirString(value);
                        DeathLocationLoc.Address.StateElement.Extension.Add(extension);
                    }
                    else
                    {
                        DeathLocationLoc.Address.State = value;
                    }
                    UpdateBundleIdentifier();
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
        [PropertyParam("addressState", "address, state")]
        [PropertyParam("addressZip", "address, zip")]
        [PropertyParam("addressCountry", "address, country")]
        [FHIRPath("Bundle.entry.resource.where($this is Location).where(meta.profile='http://hl7.org/fhir/us/vrdr/StructureDefinition/vrdr-death-location')", "address")]
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

                UpdateBundleIdentifier();
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
        [Property("Death Location Name", Property.Types.String, "Death Investigation", "Name of Death Location.", true, "http://build.fhir.org/ig/HL7/vrdr/StructureDefinition-VRDR-Death-Location.html", false, 17)]
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
                    CreateDeathLocation();
                }

                DeathLocationLoc.Name = value;

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
        [Property("Death Location Description", Property.Types.String, "Death Investigation", "Description of Death Location.", true, "http://build.fhir.org/ig/HL7/vrdr/StructureDefinition-VRDR-Death-Location.html", false, 18)]
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
                    DeathLocationLoc.Id = Guid.NewGuid().ToString();
                    DeathLocationLoc.Meta = new Meta();
                    string[] deathlocation_profile = { locationJurisdictionExtPath };
                    DeathLocationLoc.Meta.Profile = deathlocation_profile;
                    DeathLocationLoc.Description = value;
                    DeathLocationLoc.Type.Add(new CodeableConcept("http://hl7.org/fhir/us/vrdr/CodeSystem/vrdr-location-type-cs", "death", "death location", null));
                    LinkObservationToLocation(DeathDateObs, DeathLocationLoc);
                    AddReferenceToComposition(DeathLocationLoc.Id);
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
        [Property("Death Location Type", Property.Types.Dictionary, "Death Investigation", "Type of Death Location.", true, "http://build.fhir.org/ig/HL7/vrdr/StructureDefinition-VRDR-Death-Location.html", false, 19)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [PropertyParam("text", "Additional descriptive text.")]
        [FHIRPath("Bundle.entry.resource.where($this is Location).where(meta.profile='http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Death-Location')", "type")]
        public Dictionary<string, string> DeathLocationType
        {
         get
            {
                if (DeathDateObs != null && DeathDateObs.Component.Count > 0) // if there is a value for death location type, return it
                {
                    var placeComp = DeathDateObs.Component.FirstOrDefault( entry => ((Observation.ComponentComponent)entry).Code != null
                         && ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault() != null && ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault().Code == "58332-8" );
                    if (placeComp != null && placeComp.Value != null && placeComp.Value as CodeableConcept != null)
                    {
                        return(CodeableConceptToDict((CodeableConcept)placeComp.Value));
                    }
                }
                return null;
            }
            set
            {

                if (DeathDateObs == null)
                {
                    CreateDeathDateObs(); // Create it
                }

                // Find correct component; if doesn't exist add another
                var placeComp = DeathDateObs.Component.FirstOrDefault( entry => ((Observation.ComponentComponent)entry).Code != null
                         && ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault() != null && ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault().Code == "58332-8" );
                if (placeComp != null)
                {
                    ((Observation.ComponentComponent)placeComp).Value = DictToCodeableConcept(value);
                }
                else
                {
                    Observation.ComponentComponent component = new Observation.ComponentComponent();
                    component.Code = new CodeableConcept(CodeSystems.LOINC, "58332-8", "Place of death", null);
                    component.Value = DictToCodeableConcept(value);
                    DeathDateObs.Component.Add(component);
                }
              }

        }

        /// <summary>Type of Death Location Helper</summary>
        /// <value>type of death location code.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.DeathLocationTypeHelper = "16983000";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Death Location Type: {ExampleDeathRecord.DeathLocationTypeHelper}");</para>
        /// </example>
        [Property("Death Location Type Helper", Property.Types.String, "Death Investigation", "Type of Death Location.", true, "http://build.fhir.org/ig/HL7/vrdr/StructureDefinition-VRDR-Death-Location.html", false, 19)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [FHIRPath("Bundle.entry.resource.where($this is Location).where(meta.profile='http://hl7.org/fhir/us/vrdr/StructureDefinition/vrdr-death-location')", "type")]
        public string DeathLocationTypeHelper
        {
            get
            {
                if (DeathLocationType != null && DeathLocationType.ContainsKey("code"))
                {
                    return DeathLocationType["code"];
                }
                return null;
            }
            set
            {
                SetCodeValue("DeathLocationType", value, VRDR.ValueSets.PlaceOfDeath.Codes);
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
        [Property("Age At Death", Property.Types.Dictionary, "Death Investigation", "Age At Death.", true, "http://build.fhir.org/ig/HL7/vrdr/StructureDefinition-vrdr-decedent-age.html", true, 2)]
        [PropertyParam("value", "The quantity value.")]
        [PropertyParam("unit", "The quantity unit.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='30525-0')", "")]
        public Dictionary<string, string> AgeAtDeath
        {
            get
            {
                if (AgeAtDeathObs?.Value != null && !AgeAtDeathDataAbsentBoolean) // if there is a value for age, return it
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
                    CreateAgeAtDeathObs(); // Create it
                }
                string extractedValue = GetValue(value, "value");
                // If the value or unit is null, put out a data absent reason
                if ( !String.IsNullOrWhiteSpace(extractedValue) ){  // if there is a value for age, set it
                    Quantity quantity = (Quantity)AgeAtDeathObs.Value;
                    quantity.Value = Convert.ToDecimal(extractedValue);
                    AgeAtDeathObs.Value = quantity;
                    AgeAtDeathDataAbsentBoolean = false; // if age is present, cancel the data absent reason
                }
                if( (!String.IsNullOrWhiteSpace(GetValue(value, "unit"))) ){ // if there is a value for unit, set it
                    Quantity quantity = (Quantity)AgeAtDeathObs.Value;
                    quantity.Unit = GetValue(value, "unit");
                    AgeAtDeathObs.Value = quantity;
                }
              }
        }

        /// <summary>Decedent's Age At Death Data Absent Reason (code).</summary>
        /// <value>Decedent's Age At Death Data Absent Reason. A Dictionary representing a code, containing the following key/value pairs:
        /// <para>"code" - the code</para>
        /// <para>"system" - the code system this code belongs to</para>
        /// <para>"display" - a human readable meaning of the code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; code = new Dictionary&lt;string, string&gt;();</para>
        /// <para>code.Add("code", "unknown");</para>
        /// <para>code.Add("system", CodeSystems.Data_Absent_Reason_HL7_V3);</para>
        /// <para>brs.Add("display", "unknown");</para>
        /// <para>ExampleDeathRecord.DeathLocationType = code;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"AgeAtDeathDataAbsentBoolean: {ExampleDeathRecord.AgeAtDeathDataAbsentReason}");</para>
        /// </example>
        [Property("Age At Death Data Absent Reason (Code)", Property.Types.Dictionary, "Death Investigation", "Age At Death Data Absent Reason.", true, "http://build.fhir.org/ig/HL7/vrdr/StructureDefinition-VRDR-Decedent-Age.html", false, 2)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [PropertyParam("text", "Additional descriptive text.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='30525-0').dataAbsentReason", "")]
        public Dictionary<string, string> AgeAtDeathDataAbsentReason
        {
           get
            {
                if (AgeAtDeathObs?.DataAbsentReason != null && AgeAtDeathObs.DataAbsentReason as CodeableConcept != null)
                {
                    return CodeableConceptToDict((CodeableConcept)AgeAtDeathObs.DataAbsentReason);
                }
                return EmptyCodeableDict();
            }
            set
            {
                if (AgeAtDeathObs == null) // if it hasn't been created, create it
                {
                    CreateAgeAtDeathObs();
                }
                // If an empty dict is provided then this will reset the `AgeAtDeathObs.Value`
                // Since `EmptyCodeableDict()` is returned via the getter, implementers trying to
                // copy one death record to another would get their AgeAtDeath cleared when copying this field.
                if(!IsDictEmptyOrDefault(value)) {
                    AgeAtDeathObs.DataAbsentReason = DictToCodeableConcept(value);
                    AgeAtDeathObs.Value = (Quantity)null;  // this is either or with the data absent reason
                }
            }
        }

        /// <summary>Decedent's Age At Death Data Absent Boolean.</summary>
        /// <value>True if the data absent reason field is present</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.AgeAtDeathAbsentBoolean = true;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"AgeAtDeathDataAbsentBoolean: {ExampleDeathRecord.AgeAtDeathDataAbsentReason}");</para>
        /// </example>
        [Property("Age At Death Data Absent (Boolean)", Property.Types.Bool, "Death Investigation", "Age At Death Data Absent Reason.", true, "http://build.fhir.org/ig/HL7/vrdr/StructureDefinition-VRDR-Decedent-Age.html", true, 2)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='30525-0').dataAbsentReason", "")]
        public bool AgeAtDeathDataAbsentBoolean
        {
           get
            {
                return (AgeAtDeathObs == null || AgeAtDeathObs.DataAbsentReason != null);
            }
            set
            {
                if (value == false)
                {
                    if (AgeAtDeathObs != null) {  // if it has been created, reset the DataAbsentReason, otherwise, nothing to do.
                        AgeAtDeathObs.DataAbsentReason = (CodeableConcept)null;
                    }
                }
                else
                {
                    if (AgeAtDeathObs == null)
                    {
                        CreateAgeAtDeathObs();
                    }
                    // If there already is a data absent reason do not overwrite it with the default
                    if(AgeAtDeathObs.DataAbsentReason == null) {
                        AgeAtDeathDataAbsentReason = new Dictionary<string, string>() {
                            { "system", CodeSystems.Data_Absent_Reason_HL7_V3 },
                            { "code", "unknown" },
                            { "display", "Unknown" },
                            { "text", null }
                        };
                    }
                    AgeAtDeathObs.Value = (Quantity)null;  // this is either or with the data absent reason
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
        [Property("Pregnancy Status", Property.Types.Dictionary, "Death Investigation", "Pregnancy Status At Death.", true, "http://build.fhir.org/ig/HL7/vrdr/StructureDefinition-VRDR-Decedent-Pregnancy.html", true, 33)]
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
                return EmptyCodeDict();
            }
            set
            {
                if (PregnancyObs == null)
                {
                    CreatePregnancyObs();
                }
                PregnancyObs.Value = DictToCodeableConcept(value);
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
        [Property("Pregnancy Status", Property.Types.String, "Death Investigation", "Pregnancy Status At Death.", true, "http://build.fhir.org/ig/HL7/vrdr/StructureDefinition-VRDR-Decedent-Pregnancy.html", true, 33)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='69442-2')", "")]
        public string PregnancyStatusHelper
        {
            get
            {
                if (PregnancyStatus.ContainsKey("code"))
                {
                    return PregnancyStatus["code"];
                }
                return null;
            }
            set
            {
                SetCodeValue("PregnancyStatus", value, VRDR.ValueSets.PregnancyStatus.Codes);
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
        /// <para>ExampleDeathRecord.EducationLevelEditFlag = elevel;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Education Level Edit Flag: {ExampleDeathRecord.EducationLevelEditFlag['display']}");</para>
        /// </example>
        // TODO: This URL should be the html one, and those should be generated as well
        [Property("Decedent's Pregnancy Status at Death Edit Flag", Property.Types.Dictionary, "Decedent Demographics", "Decedent's Decedent's Pregnancy Status at Death Edit Flag.", true, IGURL.DecedentPregnancyStatus, false, 34)]
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
                if (PregnancyObs == null)
                {
                    CreatePregnancyObs();
                }
                if (PregnancyObs.Value != null && PregnancyObs.Value.Extension != null){
                    PregnancyObs.Value.Extension.RemoveAll(ext => ext.Url == ExtensionURL.BypassEditFlag);
                }
                Extension editFlag = new Extension();
                editFlag.Url = ExtensionURL.BypassEditFlag;
                editFlag.Value = DictToCodeableConcept(value);
                if(PregnancyObs.Value == null){
                    PregnancyObs.Value = new CodeableConcept();
                }
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
        [Property("Pregnancy Status Edit Flag", Property.Types.String, "Decedent Demographics", "Decedent's Pregnancy Status Edit Flag.", true, IGURL.DecedentPregnancyStatus, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='69442-2')", "")]
        public string PregnancyStatusEditFlagHelper
        {
            get
            {
                if (PregnancyStatusEditFlag.ContainsKey("code"))
                {
                    return PregnancyStatusEditFlag["code"];
                }
                return null;
            }
            set
            {
                SetCodeValue("PregnancyStatusEditFlag", value, VRDR.ValueSets.EditBypass012.Codes);
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
                return EmptyCodeDict();
            }
            set
            {
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
                    AddReferenceToComposition(ExaminerContactedObs.Id);
                    Bundle.AddResourceEntry(ExaminerContactedObs, "urn:uuid:" + ExaminerContactedObs.Id);
                }
                else
                {
                    ExaminerContactedObs.Value = contactedCoding;
                }
            }
        }

        /// <summary>Examiner Contacted Boolean. This is a conenience method, to access the code use ExaminerContacted instead.</summary>
        /// <value>if a medical examiner was contacted. A null value indicates "unknown".</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.ExaminerContacted = false;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Examiner Contacted: {ExampleDeathRecord.ExaminerContacted}");</para>
        /// </example>
        [Property("Examiner Contacted Helper", Property.Types.String, "Death Investigation", "Examiner Contacted.", true, IGURL.ExaminerContacted, true, 27)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='74497-9')", "")]
        public string ExaminerContactedHelper
        {
            get
            {
                if (ExaminerContacted.ContainsKey("code"))
                {
                    return ExaminerContacted["code"];
                }
                return null;
            }
            set
            {
                SetCodeValue("ExaminerContacted", value, VRDR.ValueSets.YesNoUnknown.Codes);
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
        [FHIRPath("Bundle.entry.resource.where($this is Location).where(meta.profile='http://hl7.org/fhir/us/vrdr/StructureDefinition/vrdr-injury-location')", "address")]
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
                    CreateInjuryLocationLoc();
                    LinkObservationToLocation(InjuryIncidentObs, InjuryLocationLoc);
                    AddReferenceToComposition(InjuryLocationLoc.Id);
                    Bundle.AddResourceEntry(InjuryLocationLoc, "urn:uuid:" + InjuryLocationLoc.Id);
                }

                InjuryLocationLoc.Address = DictToAddress(value);
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
        [Property("Injury Location Name", Property.Types.String, "Death Investigation", "Name of Injury Location.", true, "http://build.fhir.org/ig/HL7/vrdr/StructureDefinition-VRDR-Injury-Location.html", true, 35)]
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
                    CreateInjuryLocationLoc();
                    LinkObservationToLocation(InjuryIncidentObs, InjuryLocationLoc);
                    AddReferenceToComposition(InjuryLocationLoc.Id);
                    Bundle.AddResourceEntry(InjuryLocationLoc, "urn:uuid:" + InjuryLocationLoc.Id);
                }
                InjuryLocationLoc.Name = value;
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
        [Property("Injury Location Description", Property.Types.String, "Death Investigation", "Description of Injury Location.", true, IGURL.InjuryLocation, true, 36)]
        [ FHIRPath("Bundle.entry.resource.where($this is Location).where(meta.profile=ProfileURL.InjuryLocation)", "description")]
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
                    CreateInjuryLocationLoc();
                    LinkObservationToLocation(InjuryIncidentObs, InjuryLocationLoc);
                    AddReferenceToComposition(InjuryLocationLoc.Id);
                    Bundle.AddResourceEntry(InjuryLocationLoc, "urn:uuid:" + InjuryLocationLoc.Id);
                }

                InjuryLocationLoc.Description = value;
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
                if (InjuryIncidentObs?.Effective != null)
                {
                    return Convert.ToString(InjuryIncidentObs.Effective);
                }
                return null;
            }
            set
            {
                if (InjuryIncidentObs == null)
                {
                    CreateInjuryIncidentObs();
                }
                InjuryIncidentObs.Effective = new FhirDateTime(value);
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
                if (InjuryIncidentObs?.Value != null)
                {
                    return Convert.ToString(InjuryIncidentObs.Value);
                }
                return null;
            }
            set
            {
                if (InjuryIncidentObs == null)
                {
                    CreateInjuryIncidentObs();
                }
                InjuryIncidentObs.Value = new FhirString(value);
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
                    var placeComp = InjuryIncidentObs.Component.FirstOrDefault( entry => ((Observation.ComponentComponent)entry).Code != null
                         && ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault() != null && ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault().Code == "69450-5" );
                    if (placeComp != null && placeComp.Value != null && placeComp.Value as CodeableConcept != null)
                    {
                        Dictionary<string, string> dict = CodeableConceptToDict((CodeableConcept)placeComp.Value);
                        if ( dict.ContainsKey("text")){
                                    return (dict["text"]);
                        }
                    }
                }
                return "";
            }
            set
            {
                if (InjuryIncidentObs == null){
                    CreateInjuryIncidentObs();
                }
                // Find correct component; if doesn't exist add another
                var placeComp = InjuryIncidentObs.Component.FirstOrDefault( entry => ((Observation.ComponentComponent)entry).Code != null
                && ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault() != null && ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault().Code == "69450-5" );
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
                    var placeComp = InjuryIncidentObs.Component.FirstOrDefault( entry => ((Observation.ComponentComponent)entry).Code != null
                    && ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault() != null && ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault().Code == "69444-8" );
                    if (placeComp != null && placeComp.Value != null && placeComp.Value as CodeableConcept != null)
                    {
                        return CodeableConceptToDict((CodeableConcept)placeComp.Value);
                    }
                }
                return EmptyCodeDict();
            }
            set
            {
                if (InjuryIncidentObs == null)
                {
                    CreateInjuryIncidentObs();
                }

                // Find correct component; if doesn't exist add another
                var placeComp = InjuryIncidentObs.Component.FirstOrDefault( entry => ((Observation.ComponentComponent)entry).Code != null && ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault() != null && ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault().Code == "69444-8" );
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
        /// <para>ExampleDeathRecord.InjuryAtWorkHelper = true;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Injury At Work? : {ExampleDeathRecord.InjuryAtWorkHelper}");</para>
        /// </example>
        [Property("Injury At Work Helper", Property.Types.String, "Death Investigation", "Did the injury occur at work?", true, IGURL.InjuryIncident, true, 42)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='11374-6')", "")]
        public string InjuryAtWorkHelper
        {
            get
            {
                if (InjuryAtWork.ContainsKey("code"))
                {
                    return InjuryAtWork["code"];
                }
                return null;
            }
            set
            {
                SetCodeValue("InjuryAtWork", value, VRDR.ValueSets.YesNoUnknownNotApplicable.Codes);
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
        [Property("Transportation Role", Property.Types.Dictionary, "Death Investigation", "Transportation Role in death.", true, "http://build.fhir.org/ig/HL7/vrdr/StructureDefinition-VRDR-Decedent-Transportation-Role.html", true, 45)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='69451-3')", "")]
        public Dictionary<string, string> TransportationRole
        {
            get
            {
                if (InjuryIncidentObs != null && InjuryIncidentObs.Component.Count > 0)
                {
                    // Find correct component
                    var transportComp = InjuryIncidentObs.Component.FirstOrDefault( entry => ((Observation.ComponentComponent)entry).Code != null &&
                    ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault() != null && ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault().Code == "69451-3" );
                    if (transportComp != null && transportComp.Value != null && transportComp.Value as CodeableConcept != null)
                    {
                        return CodeableConceptToDict((CodeableConcept)transportComp.Value);
                    }
                }
                return EmptyCodeableDict();
            }
            set
            {
                if (InjuryIncidentObs == null)
                {
                    CreateInjuryIncidentObs();
                }
                // Find correct component; if doesn't exist add another
                var transportComp = InjuryIncidentObs.Component.FirstOrDefault( entry => ((Observation.ComponentComponent)entry).Code != null &&
                ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault() != null && ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault().Code == "69451-3" );
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
        [Property("Transportation Role Helper", Property.Types.String, "Death Investigation", "Transportation Role in death.", true, "http://build.fhir.org/ig/HL7/vrdr/StructureDefinition-VRDR-Decedent-Transportation-Role.html", true, 45)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='69451-3')", "")]
        public string TransportationRoleHelper
        {
            get
            {
                if (TransportationRole.ContainsKey("code"))
                {
                    string code = TransportationRole["code"] ;
                    if (code == "OTH"){
                        if (TransportationRole.ContainsKey("text")){
                            return (TransportationRole["text"]);
                        }
                        return("Other");
                    }
                }
                return null;
            }
            set
            {
                if ((value != null) && !VRDR.Mappings.TransportationIncidentRole.FHIRToIJE.ContainsKey(value))
                { //other
                    //Find the component, or create it
                    var transportComp = InjuryIncidentObs.Component.FirstOrDefault( entry => ((Observation.ComponentComponent)entry).Code != null &&
                    ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault() != null && ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault().Code == "69451-3" );
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
                    SetCodeValue("TransportationRole", value, VRDR.ValueSets.TransportationRoles.Codes);
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
        [Property("Tobacco Use", Property.Types.Dictionary, "Death Investigation", "If Tobacco Use Contributed To Death.", true, IGURL.TobaccoUseContributedToDeath , true, 32)]
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
                return EmptyCodeDict();
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
                    AddReferenceToComposition(TobaccoUseObs.Id);
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
        [Property("Tobacco Use Helper", Property.Types.String, "Death Investigation", "Tobacco Use.", true, IGURL.TobaccoUseContributedToDeath, true, 27)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='69443-0')", "")]
        public string TobaccoUseHelper
        {
            get
            {
                if (TobaccoUse.ContainsKey("code"))
                {
                    return TobaccoUse["code"];
                }
                return null;
            }
            set
            {
                SetCodeValue("TobaccoUse", value, VRDR.ValueSets.ContributoryTobaccoUse.Codes);
            }
        }

        /// <summary>Set an emerging issue value, creating an empty EmergingIssues Observation as needed.</summary>
        private void SetEmergingIssue(string identifier, string value)
        {
                if (EmergingIssues == null)
                {
                    EmergingIssues = new Observation();
                    EmergingIssues.Id = Guid.NewGuid().ToString();
                    EmergingIssues.Meta = new Meta();
                    string[] tb_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/vrdr-emerging-issues" /* ProfileURL.EmergingIssues */ };
                    EmergingIssues.Meta.Profile = tb_profile;
                    EmergingIssues.Status = ObservationStatus.Final;
                    EmergingIssues.Code = new CodeableConcept("http://hl7.org/fhir/us/vrdr/CodeSystem/vrdr-observation-cs", "emergingissues", "Emerging Issues", null);
                    EmergingIssues.Subject = new ResourceReference("urn:uuid:" + Decedent.Id);
                    AddReferenceToComposition(EmergingIssues.Id);
                    Bundle.AddResourceEntry(EmergingIssues, "urn:uuid:" + EmergingIssues.Id);
                }
                // Remove existing component (if it exists) and add an appropriate component.
                EmergingIssues.Component.RemoveAll( cmp => cmp.Code!= null && cmp.Code.Coding != null && cmp.Code.Coding.Count() > 0 && cmp.Code.Coding.First().Code == identifier );
                Observation.ComponentComponent component = new Observation.ComponentComponent();
                component.Code = new CodeableConcept("http://hl7.org/fhir/us/vrdr/CodeSystem/vrdr-component-cs", identifier, null, null);
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
                if (issue != null){
                    return issue.Value.ToString() ;
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
        [Property("Emerging Issue Field Length 1 Number 1", Property.Types.String, "Decedent Demographics", "One-Byte Field 1", true, "http://hl7.org/fhir/us/vrdr/StructureDefinition/vrdr-emerging-issues", false, 50)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='emergingissues')", "")]
        public string EmergingIssue1_1
        {
            get
            {
                return GetEmergingIssue("EmergingIssue1_1");
            }
            set
            {
                SetEmergingIssue("EmergingIssue1_1", value);
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
        [Property("Emerging Issue Field Length 1 Number 2", Property.Types.String, "Decedent Demographics", "1-Byte Field 2", true, ProfileURL.ParametersForEmergingIssues, false, 50)]
        [FHIRPath("Bundle.entry.resource.where($this is Parameters).where(parameter.name='PLACE1_2')", "value")]
        public string EmergingIssue1_2
        {
            get
            {
                return GetEmergingIssue("EmergingIssue1_2");
            }
            set
            {
                SetEmergingIssue("EmergingIssue1_2", value);
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
        [Property("Emerging Issue Field Length 1 Number 3", Property.Types.String, "Decedent Demographics", "1-Byte Field 3", true, ProfileURL.ParametersForEmergingIssues, false, 50)]
        [FHIRPath("Bundle.entry.resource.where($this is Parameters).where(parameter.name='PLACE1_3')", "value")]
        public string EmergingIssue1_3
        {
            get
            {
                return GetEmergingIssue("EmergingIssue1_3");
            }
            set
            {
                SetEmergingIssue("EmergingIssue1_3", value);
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
        [Property("Emerging Issue Field Length 1 Number 4", Property.Types.String, "Decedent Demographics", "1-Byte Field 4", true, ProfileURL.ParametersForEmergingIssues, false, 50)]
        [FHIRPath("Bundle.entry.resource.where($this is Parameters).where(parameter.name='PLACE1_4')", "value")]
        public string EmergingIssue1_4
        {
            get
            {
                return GetEmergingIssue("EmergingIssue1_4");
            }
            set
            {
                SetEmergingIssue("EmergingIssue1_4", value);
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
        [Property("Emerging Issue Field Length 1 Number 5", Property.Types.String, "Decedent Demographics", "1-Byte Field 5", true, ProfileURL.ParametersForEmergingIssues, false, 50)]
        [FHIRPath("Bundle.entry.resource.where($this is Parameters).where(parameter.name='PLACE1_5')", "value")]
        public string EmergingIssue1_5
        {
            get
            {
                return GetEmergingIssue("EmergingIssue1_5");
            }
            set
            {
                SetEmergingIssue("EmergingIssue1_5", value);
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
        [Property("Emerging Issue Field Length 1 Number 6", Property.Types.String, "Decedent Demographics", "1-Byte Field 6", true, ProfileURL.ParametersForEmergingIssues, false, 50)]
        [FHIRPath("Bundle.entry.resource.where($this is Parameters).where(parameter.name='PLACE1_6')", "value")]
        public string EmergingIssue1_6
        {
            get
            {
                return GetEmergingIssue("EmergingIssue1_6");
            }
            set
            {
                SetEmergingIssue("EmergingIssue1_6", value);
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
        [Property("Emerging Issue Field Length 8 Number 1", Property.Types.String, "Decedent Demographics", "8-Byte Field 1", true, ProfileURL.ParametersForEmergingIssues, false, 50)]
        [FHIRPath("Bundle.entry.resource.where($this is Parameters).where(parameter.name='PLACE8_1')", "value")]
        public string EmergingIssue8_1
        {
            get
            {
                return GetEmergingIssue("EmergingIssue8_1");
            }
            set
            {
                SetEmergingIssue("EmergingIssue8_1", value);
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
        [Property("Emerging Issue Field Length 8 Number 2", Property.Types.String, "Decedent Demographics", "8-Byte Field 2", true, ProfileURL.ParametersForEmergingIssues, false, 50)]
        [FHIRPath("Bundle.entry.resource.where($this is Parameters).where(parameter.name='PLACE8_2')", "value")]
        public string EmergingIssue8_2
        {
            get
            {
                return GetEmergingIssue("EmergingIssue8_2");
            }
            set
            {
                SetEmergingIssue("EmergingIssue8_2", value);
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
        [Property("Emerging Issue Field Length 8 Number 3", Property.Types.String, "Decedent Demographics", "8-Byte Field 3", true, ProfileURL.ParametersForEmergingIssues, false, 50)]
        [FHIRPath("Bundle.entry.resource.where($this is Parameters).where(parameter.name='PLACE8_3')", "value")]
        public string EmergingIssue8_3
        {
            get
            {
                return GetEmergingIssue("EmergingIssue8_3");
            }
            set
            {
                SetEmergingIssue("EmergingIssue8_3", value);
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
        [Property("Emerging Issue Field Length 20", Property.Types.String, "Decedent Demographics", "20-Byte Field", true, ProfileURL.ParametersForEmergingIssues, false, 50)]
        [FHIRPath("Bundle.entry.resource.where($this is Parameters).where(parameter.name='PLACE20')", "value")]
        public string EmergingIssue20
        {
            get
            {
                return GetEmergingIssue("EmergingIssue20");
            }
            set
            {
                SetEmergingIssue("EmergingIssue20", value);
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
            Composition.Section.First().Entry.Add(new ResourceReference("urn:uuid:" + reference));
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

            // Grab Input Race and Ethinicity
            var inputRaceAndEthnicity = Bundle.Entry.FirstOrDefault( entry => entry.Resource.ResourceType == ResourceType.Observation && ((Observation)entry.Resource).Code.Coding.First().Code == "inputraceandethnicity" );
            if (inputRaceAndEthnicity != null)
            {
                InputRaceandEthnicity = (Observation)inputRaceAndEthnicity.Resource;
            }

            // Grab Certifier
            if (Composition.Attester == null || Composition.Attester.FirstOrDefault() == null || Composition.Attester.First().Party == null || String.IsNullOrWhiteSpace(Composition.Attester.First().Party.Reference))
            {
                throw new System.ArgumentException("The Composition is missing an attestor (a reference to the Certifier/Practitioner resource).");
            }
            var practitionerEntry = Bundle.Entry.FirstOrDefault( entry => entry.Resource.ResourceType == ResourceType.Practitioner && (entry.FullUrl == Composition.Attester.First().Party.Reference || (entry.Resource.Id != null && entry.Resource.Id == Composition.Attester.First().Party.Reference)));
            if (practitionerEntry != null)
            {
                Certifier = (Practitioner)practitionerEntry.Resource;
            }
            else
            {
                throw new System.ArgumentException("Failed to find a Certifier (Practitioner). The third entry in the FHIR Bundle is usually the Certifier (Practitioner). Either the Certifier is missing from the Bundle, or the attestor reference specified in the Composition is incorrect.");
            }

            // Grab Pronouncer
            // IMPROVEMENT: Move away from using meta profile to find this Practitioner.  Use performer reference from DeathDate
            var pronouncerEntry = Bundle.Entry.FirstOrDefault( entry => entry.Resource.ResourceType == ResourceType.Practitioner && entry.Resource.Meta.Profile.FirstOrDefault() != null && MatchesProfile("VRDR-Death-Pronouncement-Performer", entry.Resource.Meta.Profile.FirstOrDefault()));
            if (pronouncerEntry != null)
            {
                Pronouncer = (Practitioner)pronouncerEntry.Resource;
            }

            // Grab Mortician
            // IMPROVEMENT: Move away from using meta profile or id to find this Practitioner, use reference from disposition method performer instead or as well
            var morticianEntry = Bundle.Entry.FirstOrDefault( entry => entry.Resource.ResourceType == ResourceType.Practitioner && entry.Resource.Meta.Profile.FirstOrDefault() != null && MatchesProfile("VRDR-Mortician", entry.Resource.Meta.Profile.FirstOrDefault()));
            if (morticianEntry == null)
            {
                morticianEntry = Bundle.Entry.FirstOrDefault( entry => entry.Resource.ResourceType == ResourceType.Practitioner && ((Practitioner)entry.Resource).Id != Certifier.Id );
            }
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

            // Grab State Local Identifier
            var stateDocumentReferenceEntry = Bundle.Entry.FirstOrDefault( entry => entry.Resource.ResourceType == ResourceType.DocumentReference && ((DocumentReference)entry.Resource).Type.Coding.First().Code == "64297-5" );
            if (stateDocumentReferenceEntry != null)
            {
                StateDocumentReference = (DocumentReference)stateDocumentReferenceEntry.Resource;
            }

            // Grab Funeral Home  **** SHould be able to find this without resort to meta.profile.  May need a reference from somewhere.  Or perhaps Locations and Organizations should require a profile.
            var funeralHome = Bundle.Entry.FirstOrDefault( entry => entry.Resource.ResourceType == ResourceType.Organization &&
                    ((Organization)entry.Resource).Meta.Profile.FirstOrDefault() != null &&
                    ((Organization)entry.Resource).Type.FirstOrDefault() != null  && (CodeableConceptToDict(((Organization)entry.Resource).Type.First())["code"] == "funeralhome"));
            if (funeralHome != null)
            {
                FuneralHome = (Organization)funeralHome.Resource;
            }

            // // Grab Funeral Home Director
            // var funeralHomeDirector = Bundle.Entry.FirstOrDefault( entry => entry.Resource.ResourceType == ResourceType.PractitionerRole );
            // if (funeralHomeDirector != null)
            // {
            //     FuneralHomeDirector = (PractitionerRole)funeralHomeDirector.Resource;
            // }

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

            // Grab Cause Of Death Condition Pathway
            var causeOfDeathConditionPathway = Bundle.Entry.FirstOrDefault( entry => entry.Resource.ResourceType == ResourceType.List );
            if (causeOfDeathConditionPathway != null)
            {
                CauseOfDeathConditionPathway = (List)causeOfDeathConditionPathway.Resource;
            }

            // Grab Causes of Death using CauseOfDeathConditionPathway
            List<Observation> causeConditions = new List<Observation>();
            if (CauseOfDeathConditionPathway != null)
            {
                foreach (List.EntryComponent condition in CauseOfDeathConditionPathway.Entry)
                {
                    if (condition != null && condition.Item != null && condition.Item.Reference != null)
                    {
                        var codCond = Bundle.Entry.FirstOrDefault( entry => entry.Resource.ResourceType == ResourceType.Observation && (((Observation)entry.Resource).Code.Coding.First().Code == "69453-9") &&
                        (entry.FullUrl == condition.Item.Reference || (entry.Resource.Id != null && entry.Resource.Id == condition.Item.Reference)) );
                        if (codCond != null)
                        {
                            causeConditions.Add((Observation)codCond.Resource);
                        }
                    }
                }
                if (causeConditions.Count() > 0)
                {
                    CauseOfDeathConditionA = causeConditions[0];
                }
                if (causeConditions.Count() > 1)
                {
                    CauseOfDeathConditionB = causeConditions[1];
                }
                if (causeConditions.Count() > 2)
                {
                    CauseOfDeathConditionC = causeConditions[2];
                }
                if (causeConditions.Count() > 3)
                {
                    CauseOfDeathConditionD = causeConditions[3];
                }

            }

            // Grab Condition Contributing To Death
            List<Observation> remainingConditions = new List<Observation>();
            foreach (var condition in Bundle.Entry.Where( entry => entry.Resource.ResourceType == ResourceType.Observation && (((Observation)entry.Resource).Code.Coding.First().Code == "69441-4")))
            {
                if (condition != null)
                {
                    if (!causeConditions.Contains((Observation)condition.Resource)) // should never trigger, since now Part1 and Part2 are observations with different codes
                    {
                        remainingConditions.Add((Observation)condition.Resource);

                    }
                }
            }
            if (remainingConditions.Count() > 1)
            {
                throw new System.ArgumentException("There are multiple Condition Contributing to Death resources present. Condition Contributing to Death resources are identified by not being referenced in the Cause of Death Pathway resource, so please confirm that all Cause of Death Conditions are correctly referenced in the Cause of Death Pathway to ensure they are not mistaken for a Condition Contributing to Death resource.");
            }
            else if (remainingConditions.Count() == 1)
            {
                ConditionContributingToDeath = remainingConditions[0];
            }

            // Scan through all RelatedPerson to make sure they all have relationship codes!
            foreach (var rp in Bundle.Entry.Where( entry => entry.Resource.ResourceType == ResourceType.RelatedPerson ))
            {
                RelatedPerson rpn = (RelatedPerson)rp.Resource;
                if (rpn.Relationship == null || rpn.Relationship.FirstOrDefault() == null || rpn.Relationship.FirstOrDefault().Coding == null || rpn.Relationship.FirstOrDefault().Coding.FirstOrDefault() == null || rpn.Relationship.FirstOrDefault().Coding.First().Code == null)
                {
                    throw new System.ArgumentException("Found a RelatedPerson resource that did not contain a relationship code. All RelatedPersons must include a relationship code to specify how the RelatedPerson is related to the subject.");
                }
            }

            // Grab Father
            var father = Bundle.Entry.FirstOrDefault( entry => entry.Resource.ResourceType == ResourceType.RelatedPerson && ((RelatedPerson)entry.Resource).Relationship.First().Coding.First().Code == "FTH" );
            if (father != null)
            {
                Father = (RelatedPerson)father.Resource;
            }

            // Grab Mother
            var mother = Bundle.Entry.FirstOrDefault( entry => entry.Resource.ResourceType == ResourceType.RelatedPerson && ((RelatedPerson)entry.Resource).Relationship.First().Coding.First().Code == "MTH" );
            if (mother != null)
            {
                Mother = (RelatedPerson)mother.Resource;
            }

            // Grab Spouse
            var spouse = Bundle.Entry.FirstOrDefault( entry => entry.Resource.ResourceType == ResourceType.RelatedPerson && ((RelatedPerson)entry.Resource).Relationship.First().Coding.First().Code == "SPS" );
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
            var employmentHistory = Bundle.Entry.FirstOrDefault( entry => entry.Resource.ResourceType == ResourceType.Observation && ((Observation)entry.Resource).Code.Coding.First().Code == "21843-8" );
            if (employmentHistory != null)
            {
                UsualWork = (Observation)employmentHistory.Resource;
            }

            // Grab Employment History
            var militaryServiceEntry = Bundle.Entry.FirstOrDefault( entry => entry.Resource.ResourceType == ResourceType.Observation && ((Observation)entry.Resource).Code.Coding.First().Code == "55280-2" );
            if (militaryServiceEntry != null)
            {
                MilitaryServiceObs = (Observation)militaryServiceEntry.Resource;
            }

            // Grab Disposition Location
            var dispositionLocation = Bundle.Entry.FirstOrDefault( entry => (entry.Resource.ResourceType == ResourceType.Location && ((Location)entry.Resource).Type.FirstOrDefault() != null ) &&
               (CodeableConceptToDict(((Location)entry.Resource).Type.First())["code"] == "disposition"));
            if (dispositionLocation != null)
            {
                DispositionLocation = (Location)dispositionLocation.Resource;
            }

            // Grab Injury Location
            // IMPROVEMENT: Move away from using meta profile to find this exact Location.
            var injuryLocation = Bundle.Entry.FirstOrDefault( entry => (entry.Resource.ResourceType == ResourceType.Location && ((Location)entry.Resource).Type.FirstOrDefault() != null ) &&
               (CodeableConceptToDict(((Location)entry.Resource).Type.First())["code"] == "injury"));
             // Bundle.Entry.FirstOrDefault( entry => entry.Resource.ResourceType == ResourceType.Location && ((Location)entry.Resource).Meta.Profile.FirstOrDefault() != null && MatchesProfile("vrdr-injury-location", ((Location)entry.Resource).Meta.Profile.FirstOrDefault()));
            if (injuryLocation != null)
            {
                InjuryLocationLoc = (Location)injuryLocation.Resource;
            }

            // Grab Death Location
            var deathLocation = Bundle.Entry.FirstOrDefault( entry => (entry.Resource.ResourceType == ResourceType.Location && ((Location)entry.Resource).Type.FirstOrDefault() != null ) &&
               (CodeableConceptToDict(((Location)entry.Resource).Type.First())["code"] == "death"));
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
            var pregnancyStatus = Bundle.Entry.FirstOrDefault( entry => entry.Resource.ResourceType == ResourceType.Observation && ((Observation)entry.Resource).Code.Coding.First().Code == "69442-2" );
            if (pregnancyStatus != null)
            {
                PregnancyObs = (Observation)pregnancyStatus.Resource;
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

            // Grab Emerging Parameters
            var emergingIssues = Bundle.Entry.FirstOrDefault( entry => entry.Resource.ResourceType == ResourceType.Observation && ((Observation)entry.Resource).Code.Coding.First().Code == "emergingissues" );
            if (emergingIssues != null)
            {
                EmergingIssues = (Observation)emergingIssues.Resource;
            }
        }

        /// <summary>Helper function to set a codeable value based on a code and the set of allowed codes.</summary>
        // <param name="field">the field name to set.</param>
        // <param name="code">the code to set the field to.</param>
        // <param name="options">the list of valid options and related display strings and code systems</param>
        private void SetCodeValue(string field, string code, string[,] options)
        {
            // If string is empty don't bother to set the value
            if (code == null || code == "") {
                return;
            }
            // Iterate over the allowed options and see if the code supplies is one of them
            for (int i = 0; i < options.GetLength(0); i += 1)
            {
                if (options[i, 0] == code)
                {
                    // Found it, so call the supplied setter with the appropriate dictionary built based on the code
                    // using the supplied options and return
                    Dictionary<string, string> dict = new Dictionary<string, string>();
                    dict.Add("code", code);
                    dict.Add("display", options[i, 1]);
                    dict.Add("system", options[i, 2]);
                    typeof(DeathRecord).GetProperty(field).SetValue(this, dict);
                    return;
                }
            }
            // If we got here we didn't find the code, so it's not a valid option
            throw new System.ArgumentException($"Code '{code}' is not an allowed value for field {field}");
        }

        /// <summary>Convert a "code" dictionary to a FHIR Coding.</summary>
        /// <param name="dict">represents a code.</param>
        /// <returns>the corresponding Coding representation of the code.</returns>
        private Coding DictToCoding(Dictionary<string, string> dict)
        {
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
            return coding;
        }

        /// <summary>Convert a "code" dictionary to a FHIR CodableConcept.</summary>
        /// <param name="dict">represents a code.</param>
        /// <returns>the corresponding CodeableConcept representation of the code.</returns>
        private CodeableConcept DictToCodeableConcept(Dictionary<string, string> dict)
        {
            CodeableConcept codeableConcept = new CodeableConcept();
            Coding coding = DictToCoding(dict);
            codeableConcept.Coding.Add(coding);
            if (dict != null && dict.ContainsKey("text") && dict["text"] != null && dict["text"].Length > 0)
            {
                codeableConcept.Text = dict["text"];
            }
            return codeableConcept;
        }

        /// <summary>Check if a dictionary is empty or a default empty dictionary (all values are null or empty strings)</summary>
        /// <param name="dict">represents a code.</param>
        /// <returns>A boolean identifying whether the provided dictionary is empty or default.</returns>
        private bool IsDictEmptyOrDefault(Dictionary<string, string> dict)
        {
            return dict.Count == 0 || dict.Values.All(v => v == null || v == "");
        }

        /// <summary>Convert a FHIR Coding to a "code" Dictionary</summary>
        /// <param name="coding">a FHIR Coding.</param>
        /// <returns>the corresponding Dictionary representation of the code.</returns>
        private Dictionary<string, string> CodingToDict(Coding coding)
        {
            Dictionary<string, string> dictionary = EmptyCodeDict();
            if (coding != null)
            {
                if (!String.IsNullOrEmpty(coding.Code))
                {
                    dictionary["code"] = coding.Code;
                }
                if (!String.IsNullOrEmpty(coding.System))
                {
                    dictionary["system"] = coding.System;
                }
                if (!String.IsNullOrEmpty(coding.Display))
                {
                    dictionary["display"] = coding.Display;
                }
            }
            return dictionary;
        }

        /// <summary>Convert a FHIR CodableConcept to a "code" Dictionary</summary>
        /// <param name="codeableConcept">a FHIR CodeableConcept.</param>
        /// <returns>the corresponding Dictionary representation of the code.</returns>
        private Dictionary<string, string> CodeableConceptToDict(CodeableConcept codeableConcept)
        {
            if (codeableConcept != null && codeableConcept.Coding != null)
            {
                Coding coding = codeableConcept.Coding.FirstOrDefault();
                var codeDict = CodingToDict(coding);
                if (codeableConcept != null && codeableConcept.Text != null && codeableConcept.Text.Length > 0)
                {
                    codeDict["text"] = codeableConcept.Text;
                }
                else
                {
                    codeDict["text"] = "";
                }
                return codeDict;
            }
            else
            {
                return EmptyCodeableDict();
            }
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
                if (dict.ContainsKey("addressCityC") && !String.IsNullOrEmpty(dict["addressCityC"]))
                {
                    Extension cityCode = new Extension();
                    cityCode.Url = ExtensionURL.CityCode;
                    cityCode.Value = new FhirString(dict["addressCityC"]);
                    address.CityElement = new FhirString();
                    address.CityElement.Extension.Add(cityCode);
                }
                if (dict.ContainsKey("addressCity") && !String.IsNullOrEmpty(dict["addressCity"]))
                {
                    if (address.CityElement != null)
                    {
                        address.CityElement.Value = dict["addressCity"];
                    }
                    else
                    {
                        address.City = dict["addressCity"];
                    }

                }
                if (dict.ContainsKey("addressCountyC") && !String.IsNullOrEmpty(dict["addressCountyC"]))
                {
                    Extension countyCode = new Extension();
                    countyCode.Url = ExtensionURL.DistrictCode;
                    countyCode.Value = new FhirString(dict["addressCountyC"]);
                    address.DistrictElement = new FhirString();
                    address.DistrictElement.Extension.Add(countyCode);
                }
                if (dict.ContainsKey("addressCounty") && !String.IsNullOrEmpty(dict["addressCounty"]))
                {
                    if (address.DistrictElement != null)
                    {
                        address.DistrictElement.Value = dict["addressCounty"];
                    }
                    else
                    {
                        address.District = dict["addressCounty"];
                    }
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
                if (dict.ContainsKey("addressStnum") && !String.IsNullOrEmpty(dict["addressStnum"]))
                {
                    Extension stnum = new Extension();
                    stnum.Url = ExtensionURL.StreetNumber;
                    stnum.Value = new FhirString(dict["addressStnum"]);
                    address.Extension.Add(stnum);
                }
                if (dict.ContainsKey("addressPredir") && !String.IsNullOrEmpty(dict["addressPredir"]))
                {
                    Extension predir = new Extension();
                    predir.Url = ExtensionURL.PreDirectional;
                    predir.Value = new FhirString(dict["addressPredir"]);
                    address.Extension.Add(predir);
                }
                if (dict.ContainsKey("addressStname") && !String.IsNullOrEmpty(dict["addressStname"]))
                {
                    Extension stname = new Extension();
                    stname.Url = ExtensionURL.StreetName;
                    stname.Value = new FhirString(dict["addressStname"]);
                    address.Extension.Add(stname);
                }
                if (dict.ContainsKey("addressStdesig") && !String.IsNullOrEmpty(dict["addressStdesig"]))
                {
                    Extension stdesig = new Extension();
                    stdesig.Url = ExtensionURL.StreetDesignator;
                    stdesig.Value = new FhirString(dict["addressStdesig"]);
                    address.Extension.Add(stdesig);
                }
                if (dict.ContainsKey("addressPostdir") && !String.IsNullOrEmpty(dict["addressPostdir"]))
                {
                    Extension postdir = new Extension();
                    postdir.Url = ExtensionURL.PostDirectional;
                    postdir.Value = new FhirString(dict["addressPostdir"]);
                    address.Extension.Add(postdir);
                }
                if (dict.ContainsKey("addressUnitnum") && !String.IsNullOrEmpty(dict["addressUnitnum"]))
                {
                    Extension unitnum = new Extension();
                    unitnum.Url = ExtensionURL.UnitOrAptNumber;
                    unitnum.Value = new FhirString(dict["addressUnitnum"]);
                    address.Extension.Add(unitnum);
                }

            }
            return address;
        }


        /// <summary>Convert a Date Part Extension to an Array.</summary>
        /// <param name="datePartAbsent">a Date Part Extension.</param>
        /// <returns>the corresponding array representation of the date parts.</returns>
        private Tuple<string, string>[] DatePartsToArray(Extension datePartAbsent)
        {
            List<Tuple<string, string>> dateParts = new List<Tuple<string, string>>();
            if (datePartAbsent != null)
            {
                Extension yearAbsentPart = datePartAbsent.Extension.Where(ext => ext.Url == "year-absent-reason").FirstOrDefault();
                Extension monthAbsentPart = datePartAbsent.Extension.Where(ext => ext.Url == "month-absent-reason").FirstOrDefault();
                Extension dayAbsentPart = datePartAbsent.Extension.Where(ext => ext.Url == "day-absent-reason").FirstOrDefault();
                Extension yearPart = datePartAbsent.Extension.Where(ext => ext.Url == "date-year").FirstOrDefault();
                Extension monthPart = datePartAbsent.Extension.Where(ext => ext.Url == "date-month").FirstOrDefault();
                Extension dayPart = datePartAbsent.Extension.Where(ext => ext.Url == "date-day").FirstOrDefault();
                // Year part
                if (yearAbsentPart != null)
                {
                    dateParts.Add(Tuple.Create("year-absent-reason", yearAbsentPart.Value.ToString()));
                }
                if (yearPart != null)
                {
                    dateParts.Add(Tuple.Create("date-year", yearPart.Value.ToString()));
                }
                // Month part
                if (monthAbsentPart != null)
                {
                    dateParts.Add(Tuple.Create("month-absent-reason", monthAbsentPart.Value.ToString()));
                }
                if (monthPart != null)
                {
                    dateParts.Add(Tuple.Create("date-month", monthPart.Value.ToString()));
                }
                // Day Part
                if (dayAbsentPart != null)
                {
                    dateParts.Add(Tuple.Create("day-absent-reason", dayAbsentPart.Value.ToString()));
                }
                if (dayPart != null)
                {
                    dateParts.Add(Tuple.Create("date-day", dayPart.Value.ToString()));
                }
            }
            return dateParts.ToArray();
        }

        /// <summary>Convert an element to an integer or code depending on if the input element is a date part.</summary>
        /// <param name="pair">A key value pair, the key will be used to identify whether the element is a date part.</param>
        private Element DatePartToIntegerOrCode(Tuple<string, string> pair)
        {
            if (pair.Item1 == "date-year" || pair.Item1 == "date-month" || pair.Item1 == "date-day")
            {
                return new Integer(Int32.Parse(pair.Item2));
            }
            else
            {
                return new Code(pair.Item2);
            }
        }

        /// <summary>Convert a FHIR Address to an "address" Dictionary.</summary>
        /// <param name="addr">a FHIR Address.</param>
        /// <returns>the corresponding Dictionary representation of the FHIR Address.</returns>
        private Dictionary<string, string> AddressToDict(Address addr)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add("addressLine1", "");
            dictionary.Add("addressLine2", "");
            dictionary.Add("addressCity", "");
            dictionary.Add("addressCityC", "");
            dictionary.Add("addressCounty", "");
            dictionary.Add("addressCountyC", "");
            dictionary.Add("addressState", "");
            dictionary.Add("addressZip", "");
            dictionary.Add("addressCountry", "");
            dictionary.Add("addressStnum", "");
            dictionary.Add("addressPredir", "");
            dictionary.Add("addressStname", "");
            dictionary.Add("addressStdesig", "");
            dictionary.Add("addressPostdir", "");
            dictionary.Add("addressUnitnum", "");
            if (addr != null)
            {
                if (addr.Line != null && addr.Line.Count() > 0)
                {
                    dictionary["addressLine1"] = addr.Line.First();
                }

                if (addr.Line != null && addr.Line.Count() > 1)
                {
                    dictionary["addressLine2"] = addr.Line.Last();
                }

                if(addr.CityElement != null)
                {
                    Extension cityCode = addr.CityElement.Extension.Where(ext => ext.Url == ExtensionURL.CityCode).FirstOrDefault();
                    if (cityCode != null)
                    {
                        dictionary["addressCityC"] = cityCode.Value.ToString();
                    }
                }

                if(addr.DistrictElement != null)
                {
                    Extension districtCode = addr.DistrictElement.Extension.Where(ext => ext.Url == ExtensionURL.DistrictCode).FirstOrDefault();
                    if (districtCode != null){
                        dictionary["addressCountyC"] = districtCode.Value.ToString();
                    }
                }

                Extension stnum = addr.Extension.Where(ext => ext.Url == ExtensionURL.StreetNumber).FirstOrDefault();
                if (stnum != null)
                {
                    dictionary["addressStnum"] = stnum.Value.ToString();
                }

                Extension predir = addr.Extension.Where(ext => ext.Url == ExtensionURL.PreDirectional).FirstOrDefault();
                if (predir != null)
                {
                    dictionary["addressPredir"] = predir.Value.ToString();
                }

                Extension stname = addr.Extension.Where(ext => ext.Url == ExtensionURL.StreetName).FirstOrDefault();
                if (stname != null)
                {
                    dictionary["addressStname"] = stname.Value.ToString();
                }

                Extension stdesig = addr.Extension.Where(ext => ext.Url == ExtensionURL.StreetDesignator).FirstOrDefault();
                if (stdesig != null)
                {
                    dictionary["addressStdesig"] = stdesig.Value.ToString();
                }

                Extension postdir = addr.Extension.Where(ext => ext.Url == ExtensionURL.PostDirectional).FirstOrDefault();
                if (postdir != null)
                {
                    dictionary["addressPostdir"] = postdir.Value.ToString();
                }

                Extension unitnum = addr.Extension.Where(ext => ext.Url == ExtensionURL.UnitOrAptNumber).FirstOrDefault();
                if (unitnum != null)
                {
                    dictionary["addressUnitnum"] = unitnum.Value.ToString();
                }


                if (addr.State != null)
                {
                    dictionary["addressState"] = addr.State;
                }
                if (addr.StateElement != null)
                 {
                     dictionary["addressJurisdiction"] = addr.State; // by default.  If extension present, override
                     Extension stateExt = addr.StateElement.Extension.Where(ext => ext.Url == ExtensionURL.LocationJurisdictionId).FirstOrDefault();
                     if (stateExt != null)
                     {
                         dictionary["addressJurisdiction"] = stateExt.Value.ToString();
                     }
                 }
                if (addr.City != null)
                {
                    dictionary["addressCity"] = addr.City;
                }
                if (addr.District != null)
                {
                    dictionary["addressCounty"] = addr.District;
                }
                if (addr.PostalCode != null)
                {
                    dictionary["addressZip"] = addr.PostalCode;
                }
                if (addr.Country != null)
                {
                    dictionary["addressCountry"] = addr.Country;
                }
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
            dictionary.Add("addressCityC", "");
            dictionary.Add("addressCounty", "");
            dictionary.Add("addressState", "");
            dictionary.Add("addressZip", "");
            dictionary.Add("addressCountry", "");
            return dictionary;
        }

        /// <summary>Returns an empty "code" Dictionary.</summary>
        /// <returns>an empty "code" Dictionary.</returns>
        private Dictionary<string, string> EmptyCodeDict()
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add("code", "");
            dictionary.Add("system", "");
            dictionary.Add("display", "");
            return dictionary;
        }

        /// <summary>Returns an empty "code" Dictionary for race and ethnicity.</summary>
        /// <returns>an empty "code" Dictionary.</returns>
        private Dictionary<string, string> EmptyRaceEthnicityCodeDict()
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add("code", "");
            dictionary.Add("display", "");
            return dictionary;
        }

        /// <summary>Returns an empty "codeable" Dictionary.</summary>
        /// <returns>an empty "codeable" Dictionary.</returns>
        private Dictionary<string, string> EmptyCodeableDict()
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add("code", "");
            dictionary.Add("system", "");
            dictionary.Add("display", "");
            dictionary.Add("text", "");
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

        /// <summary>Check to make sure the given profile contains the given resource.</summary>
        private static bool MatchesProfile(string resource, string profile)
        {
            if (!String.IsNullOrWhiteSpace(profile) && profile.Contains(resource))
            {
                return true;
            }
            return false;
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
            // the priority values should order the categories as: Decedent Demographics, Decedent Disposition, Death Investigation, Death Certification
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
                        value = property.Value["Value"].ToObject<Tuple<string, string /*, Dictionary<string, string>*/>[]>();
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
        public Property(string name, Types type, string category, string description, bool serialize, string igurl, bool capturedInIJE, int priority = 4)
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
