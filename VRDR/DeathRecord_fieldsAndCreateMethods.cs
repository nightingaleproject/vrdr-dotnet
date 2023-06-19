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

// DeathRecord_fieldsAndCreateMethods.cs
//     Contains field definitions and associated createXXXX methods used to construct a field

namespace VRDR
{
    /// <summary>Class <c>DeathRecord</c> models a FHIR Vital Records Death Reporting (VRDR) Death
    /// Record. This class was designed to help consume and produce death records that follow the
    /// HL7 FHIR Vital Records Death Reporting Implementation Guide, as described at:
    /// http://hl7.org/fhir/us/vrdr and https://github.com/hl7/vrdr.
    /// </summary>
    public partial class DeathRecord
    {
        /// <summary>String to represent a blank value when an empty string is not allowed</summary>
        public static string BlankPlaceholder = "BLANK";

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

        /// <summary>The Decedent's Race and Ethnicity provided by Jurisdiction.</summary>
        private Observation InputRaceAndEthnicityObs;

        /// <summary> Create Input Race and Ethnicity </summary>

        private void CreateInputRaceEthnicityObs()
        {
            InputRaceAndEthnicityObs = new Observation();
            InputRaceAndEthnicityObs.Id = Guid.NewGuid().ToString();
            InputRaceAndEthnicityObs.Meta = new Meta();
            string[] raceethnicity_profile = { ProfileURL.InputRaceAndEthnicity };
            InputRaceAndEthnicityObs.Meta.Profile = raceethnicity_profile;
            InputRaceAndEthnicityObs.Status = ObservationStatus.Final;
            InputRaceAndEthnicityObs.Code = new CodeableConcept(CodeSystems.ObservationCode, "inputraceandethnicity", "Input Race and Ethnicity", null);
            InputRaceAndEthnicityObs.Subject = new ResourceReference("urn:uuid:" + Decedent.Id);
            AddReferenceToComposition(InputRaceAndEthnicityObs.Id, "DecedentDemographics");
            Bundle.AddResourceEntry(InputRaceAndEthnicityObs, "urn:uuid:" + InputRaceAndEthnicityObs.Id);
        }

        /// <summary>The Certifier.</summary>
        private Practitioner Certifier;

        /// <summary>Create Certifier.</summary>
        private void CreateCertifier()
        {
            Certifier = new Practitioner();
            Certifier.Id = Guid.NewGuid().ToString();
            Certifier.Meta = new Meta();
            string[] certifier_profile = { ProfileURL.Certifier };
            Certifier.Meta.Profile = certifier_profile;
            // Not linked to Composition or inserted in bundle, since this is run before the composition exists.
        }

        /// <summary>The Certification.</summary>
        private Procedure DeathCertification;
        /// <summary>Create Death Certification.</summary>
        private void CreateDeathCertification()
        {
            DeathCertification = new Procedure();
            DeathCertification.Id = Guid.NewGuid().ToString();
            DeathCertification.Subject = new ResourceReference("urn:uuid:" + Decedent.Id);
            DeathCertification.Meta = new Meta();
            string[] deathcertification_profile = { ProfileURL.DeathCertification };
            DeathCertification.Meta.Profile = deathcertification_profile;
            DeathCertification.Status = EventStatus.Completed;
            DeathCertification.Category = new CodeableConcept(CodeSystems.SCT, "103693007", "Diagnostic procedure", null);
            DeathCertification.Code = new CodeableConcept(CodeSystems.SCT, "308646001", "Death certification", null);
            // Not linked to Composition or inserted in bundle, since this is run before the composition exists.
        }

        /// <summary>The Manner of Death Observation.</summary>
        private Observation MannerOfDeath;

        /// <summary>Condition Contributing to Death.</summary>
        private Observation ConditionContributingToDeath;

        /// <summary>Cause Of Death Condition Line A (#1).</summary>
        private Observation CauseOfDeathConditionA;

        /// <summary>Cause Of Death Condition Line B (#2).</summary>
        private Observation CauseOfDeathConditionB;

        /// <summary>Cause Of Death Condition Line C (#3).</summary>
        private Observation CauseOfDeathConditionC;

        /// <summary>Cause Of Death Condition Line D (#4).</summary>
        private Observation CauseOfDeathConditionD;

        /// <summary>Create a Cause Of Death Condition.   Used to create CauseOfDeathConditionA-D </summary>
        private Observation CauseOfDeathCondition(int index)
        {
            Observation CodCondition;
            CodCondition = new Observation();
            CodCondition.Id = Guid.NewGuid().ToString();
            CodCondition.Meta = new Meta();
            string[] condition_profile = { ProfileURL.CauseOfDeathPart1 };
            CodCondition.Meta.Profile = condition_profile;
            CodCondition.Status = ObservationStatus.Final;
            CodCondition.Code = new CodeableConcept(CodeSystems.LOINC, "69453-9", "Cause of death [US Standard Certificate of Death]", null);
            CodCondition.Subject = new ResourceReference("urn:uuid:" + Decedent.Id);
            CodCondition.Performer.Add(new ResourceReference("urn:uuid:" + Certifier.Id));
            Observation.ComponentComponent component = new Observation.ComponentComponent();
            component.Code = new CodeableConcept(CodeSystems.ComponentCode, "lineNumber", "line number", null);
            component.Value = new Integer(index + 1); // index is 0-3, linenumbers are 1-4
            CodCondition.Component.Add(component);
            AddReferenceToComposition(CodCondition.Id, "DeathCertification");
            Bundle.AddResourceEntry(CodCondition, "urn:uuid:" + CodCondition.Id);
            return (CodCondition);
        }


        /// <summary>Decedent's Father.</summary>
        private RelatedPerson Father;
        /// <summary>Create Spouse.</summary>
        private void CreateFather()
        {
            Father = new RelatedPerson();
            Father.Id = Guid.NewGuid().ToString();
            Father.Meta = new Meta();
            string[] father_profile = { ProfileURL.DecedentFather };
            Father.Meta.Profile = father_profile;
            Father.Patient = new ResourceReference("urn:uuid:" + Decedent.Id);
            Father.Active = true; // USCore RelatedPerson requires active = true
            Father.Relationship.Add(new CodeableConcept(CodeSystems.RoleCode_HL7_V3, "FTH", "father", null));
            AddReferenceToComposition(Father.Id, "DecedentDemographics");
            Bundle.AddResourceEntry(Father, "urn:uuid:" + Father.Id);
        }

        /// <summary>Decedent's Mother.</summary>
        private RelatedPerson Mother;
        /// <summary>Create Mother.</summary>
        private void CreateMother()
        {
            Mother = new RelatedPerson();
            Mother.Id = Guid.NewGuid().ToString();
            Mother.Meta = new Meta();
            string[] mother_profile = { ProfileURL.DecedentMother };
            Mother.Meta.Profile = mother_profile;
            Mother.Patient = new ResourceReference("urn:uuid:" + Decedent.Id);
            Mother.Active = true; // USCore RelatedPerson requires active = true
            Mother.Relationship.Add(new CodeableConcept(CodeSystems.RoleCode_HL7_V3, "MTH", "mother", null));
            AddReferenceToComposition(Mother.Id, "DecedentDemographics");
            Bundle.AddResourceEntry(Mother, "urn:uuid:" + Mother.Id);
        }

        /// <summary>Decedent's Spouse.</summary>
        private RelatedPerson Spouse;
        /// <summary>Create Spouse.</summary>
        private void CreateSpouse()
        {
            Spouse = new RelatedPerson();
            Spouse.Id = Guid.NewGuid().ToString();
            Spouse.Meta = new Meta();
            string[] spouse_profile = { ProfileURL.DecedentSpouse };
            Spouse.Meta.Profile = spouse_profile;
            Spouse.Patient = new ResourceReference("urn:uuid:" + Decedent.Id);
            Spouse.Active = true; // USCore RelatedPerson requires active = true
            Spouse.Relationship.Add(new CodeableConcept(CodeSystems.RoleCode_HL7_V3, "SPS", "spouse", null));
            AddReferenceToComposition(Spouse.Id, "DecedentDemographics");
            Bundle.AddResourceEntry(Spouse, "urn:uuid:" + Spouse.Id);
        }

        /// <summary>Decedent Education Level.</summary>
        private Observation DecedentEducationLevel;
        /// <summary>Create an empty EducationLevel Observation, to be populated in either EducationLevel or EducationLevelEditFlag.</summary>
        private void CreateEducationLevelObs()
        {
            DecedentEducationLevel = new Observation();
            DecedentEducationLevel.Id = Guid.NewGuid().ToString();
            DecedentEducationLevel.Meta = new Meta();
            string[] educationlevel_profile = { ProfileURL.DecedentEducationLevel };
            DecedentEducationLevel.Meta.Profile = educationlevel_profile;
            DecedentEducationLevel.Status = ObservationStatus.Final;
            DecedentEducationLevel.Code = new CodeableConcept(CodeSystems.LOINC, "80913-7", "Highest level of education [US Standard Certificate of Death]", null);
            DecedentEducationLevel.Subject = new ResourceReference("urn:uuid:" + Decedent.Id);
            AddReferenceToComposition(DecedentEducationLevel.Id, "DecedentDemographics");
            Bundle.AddResourceEntry(DecedentEducationLevel, "urn:uuid:" + DecedentEducationLevel.Id);
        }

        /// <summary>Birth Record Identifier.</summary>
        private Observation BirthRecordIdentifier;

        /// <summary>Create an empty BirthRecordIdentifier Observation.</summary>
        private void CreateBirthRecordIdentifier()
        {
            BirthRecordIdentifier = new Observation();
            BirthRecordIdentifier.Id = Guid.NewGuid().ToString();
            BirthRecordIdentifier.Meta = new Meta();
            string[] br_profile = { ProfileURL.BirthRecordIdentifier };
            BirthRecordIdentifier.Meta.Profile = br_profile;
            BirthRecordIdentifier.Status = ObservationStatus.Final;
            BirthRecordIdentifier.Code = new CodeableConcept(CodeSystems.HL7_identifier_type, "BR", "Birth registry number", null);
            BirthRecordIdentifier.Subject = new ResourceReference("urn:uuid:" + Decedent.Id);
            BirthRecordIdentifier.Value = (FhirString)null;
            BirthRecordIdentifier.DataAbsentReason = new CodeableConcept(CodeSystems.Data_Absent_Reason_HL7_V3, "unknown", "Unknown", null);

            AddReferenceToComposition(BirthRecordIdentifier.Id, "DecedentDemographics");
            Bundle.AddResourceEntry(BirthRecordIdentifier, "urn:uuid:" + BirthRecordIdentifier.Id);
        }


        /// <summary>Emerging Issues.</summary>
        protected Observation EmergingIssues;

        /// <summary> Coding Status </summary>
        private Parameters CodingStatusValues;

        /// <summary>Create an empty Coding Status Value Parameters.</summary>
        private void CreateCodingStatusValues()
        {
            CodingStatusValues = new Parameters();
            CodingStatusValues.Id = Guid.NewGuid().ToString();
            CodingStatusValues.Meta = new Meta();
            string[] profile = { ProfileURL.CodingStatusValues };
            CodingStatusValues.Meta.Profile = profile;
            Date date = new Date();
            date.Extension.Add(NewBlankPartialDateTimeExtension(false));
            CodingStatusValues.Add("receiptDate", date);
            AddReferenceToComposition(CodingStatusValues.Id, "CodedContent");
            Bundle.AddResourceEntry(CodingStatusValues, "urn:uuid:" + CodingStatusValues.Id);
        }

        /// <summary>Usual Work.</summary>
        private Observation UsualWork;
        /// <summary>Create Usual Work.</summary>
        private void CreateUsualWork()
        {
            UsualWork = new Observation();
            UsualWork.Id = Guid.NewGuid().ToString();
            UsualWork.Meta = new Meta();
            string[] usualwork_profile = { ProfileURL.DecedentUsualWork };
            UsualWork.Meta.Profile = usualwork_profile;
            UsualWork.Status = ObservationStatus.Final;
            UsualWork.Code = new CodeableConcept(CodeSystems.LOINC, "21843-8", "History of Usual occupation", null);
            UsualWork.Category.Add(new CodeableConcept(CodeSystems.ObservationCategory, "social-history", null, null));
            UsualWork.Subject = new ResourceReference("urn:uuid:" + Decedent.Id);
            UsualWork.Effective = new Period();
            AddReferenceToComposition(UsualWork.Id, "DecedentDemographics");
            Bundle.AddResourceEntry(UsualWork, "urn:uuid:" + UsualWork.Id);
        }

        /// <summary>Whether the decedent served in the military</summary>
        private Observation MilitaryServiceObs;

        /// <summary>The Funeral Home.</summary>
        private Organization FuneralHome;

        /// <summary>Create Funeral Home.</summary>
        private void CreateFuneralHome()
        {
            FuneralHome = new Organization();
            FuneralHome.Id = Guid.NewGuid().ToString();
            FuneralHome.Meta = new Meta();
            string[] funeralhome_profile = { ProfileURL.FuneralHome };
            FuneralHome.Meta.Profile = funeralhome_profile;
            FuneralHome.Type.Add(new CodeableConcept(CodeSystems.OrganizationType, "funeralhome", "Funeral Home", null));
            FuneralHome.Active = true;
            AddReferenceToComposition(FuneralHome.Id, "DecedentDisposition");
            Bundle.AddResourceEntry(FuneralHome, "urn:uuid:" + FuneralHome.Id);
        }

        /// <summary>Disposition Location.</summary>
        private Location DispositionLocation;
        /// <summary>Create Disposition Location.</summary>
        private void CreateDispositionLocation()
        {
            DispositionLocation = new Location();
            DispositionLocation.Id = Guid.NewGuid().ToString();
            DispositionLocation.Meta = new Meta();
            string[] dispositionlocation_profile = { ProfileURL.DispositionLocation };
            DispositionLocation.Meta.Profile = dispositionlocation_profile;
            DispositionLocation.Name = DeathRecord.BlankPlaceholder; // We cannot have a blank string, but the field is required to be present
            Coding pt = new Coding(CodeSystems.HL7_location_physical_type, "si", "Site");
            DispositionLocation.PhysicalType = new CodeableConcept();
            DispositionLocation.PhysicalType.Coding.Add(pt);
            DispositionLocation.Type.Add(new CodeableConcept(CodeSystems.LocationType, "disposition", "disposition location", null));
            AddReferenceToComposition(DispositionLocation.Id, "DecedentDisposition");
            Bundle.AddResourceEntry(DispositionLocation, "urn:uuid:" + DispositionLocation.Id);
        }

        /// <summary>Disposition Method.</summary>
        private Observation DispositionMethod;

        /// <summary>Autopsy Performed.</summary>
        private Observation AutopsyPerformed;
        /// <summary>Create Autopsy Performed </summary>
        private void CreateAutopsyPerformed()
        {
            AutopsyPerformed = new Observation();
            AutopsyPerformed.Id = Guid.NewGuid().ToString();
            AutopsyPerformed.Meta = new Meta();
            string[] autopsyperformed_profile = { ProfileURL.AutopsyPerformedIndicator };
            AutopsyPerformed.Meta.Profile = autopsyperformed_profile;
            AutopsyPerformed.Status = ObservationStatus.Final;
            AutopsyPerformed.Code = new CodeableConcept(CodeSystems.LOINC, "85699-7", "Autopsy was performed", null);
            AutopsyPerformed.Subject = new ResourceReference("urn:uuid:" + Decedent.Id);
            AddReferenceToComposition(AutopsyPerformed.Id, "DeathInvestigation");
            Bundle.AddResourceEntry(AutopsyPerformed, "urn:uuid:" + AutopsyPerformed.Id);
        }

        /// <summary>Age At Death.</summary>
        private Observation AgeAtDeathObs;
        /// <summary>Create Age At Death Obs</summary>
        private void CreateAgeAtDeathObs()
        {
            AgeAtDeathObs = new Observation();
            AgeAtDeathObs.Id = Guid.NewGuid().ToString();
            AgeAtDeathObs.Meta = new Meta();
            string[] age_profile = { ProfileURL.DecedentAge };
            AgeAtDeathObs.Meta.Profile = age_profile;
            AgeAtDeathObs.Status = ObservationStatus.Final;
            AgeAtDeathObs.Code = new CodeableConcept(CodeSystems.LOINC, "39016-1", "Age at death", null);
            AgeAtDeathObs.Subject = new ResourceReference("urn:uuid:" + Decedent.Id);
            AgeAtDeathObs.Value = new Quantity();
            // AgeAtDeathObs.DataAbsentReason = new CodeableConcept(CodeSystems.Data_Absent_Reason_HL7_V3, "unknown", "Unknown", null); // set at birth
            AddReferenceToComposition(AgeAtDeathObs.Id, "DecedentDemographics");
            Bundle.AddResourceEntry(AgeAtDeathObs, "urn:uuid:" + AgeAtDeathObs.Id);
        }

        /// <summary>Decedent Pregnancy Status.</summary>
        private Observation PregnancyObs;
        /// <summary> Create Pregnancy Status. </summary>
        private void CreatePregnancyObs()
        {
            PregnancyObs = new Observation();
            PregnancyObs.Id = Guid.NewGuid().ToString();
            PregnancyObs.Meta = new Meta();
            string[] p_profile = { ProfileURL.DecedentPregnancyStatus };
            PregnancyObs.Meta.Profile = p_profile;
            PregnancyObs.Status = ObservationStatus.Final;
            PregnancyObs.Code = new CodeableConcept(CodeSystems.LOINC, "69442-2", "Timing of recent pregnancy in relation to death", null);
            PregnancyObs.Subject = new ResourceReference("urn:uuid:" + Decedent.Id);
            AddReferenceToComposition(PregnancyObs.Id, "DeathInvestigation");
            Bundle.AddResourceEntry(PregnancyObs, "urn:uuid:" + PregnancyObs.Id);
        }

        /// <summary>Examiner Contacted.</summary>
        private Observation ExaminerContactedObs;

        /// <summary>Tobacco Use Contributed To Death.</summary>
        private Observation TobaccoUseObs;

        /// <summary>Injury Location.</summary>
        private Location InjuryLocationLoc;
        /// <summary>Create Injury Location.</summary>
        private void CreateInjuryLocationLoc()
        {
            InjuryLocationLoc = new Location();
            InjuryLocationLoc.Id = Guid.NewGuid().ToString();
            InjuryLocationLoc.Meta = new Meta();
            string[] injurylocation_profile = { ProfileURL.InjuryLocation };
            InjuryLocationLoc.Meta.Profile = injurylocation_profile;
            InjuryLocationLoc.Name = DeathRecord.BlankPlaceholder; // We cannot have a blank string, but the field is required to be present
            InjuryLocationLoc.Address = DictToAddress(EmptyAddrDict());
            InjuryLocationLoc.Type.Add(new CodeableConcept(CodeSystems.LocationType, "injury", "injury location", null));
            AddReferenceToComposition(InjuryLocationLoc.Id, "DeathInvestigation");
            Bundle.AddResourceEntry(InjuryLocationLoc, "urn:uuid:" + InjuryLocationLoc.Id);
        }

        /// <summary>Injury Incident.</summary>
        private Observation InjuryIncidentObs;
        /// <summary>Create Injury Incident.</summary>
        private void CreateInjuryIncidentObs()
        {
            InjuryIncidentObs = new Observation();
            InjuryIncidentObs.Id = Guid.NewGuid().ToString();
            InjuryIncidentObs.Meta = new Meta();
            string[] iio_profile = { ProfileURL.InjuryIncident };
            InjuryIncidentObs.Meta.Profile = iio_profile;
            InjuryIncidentObs.Status = ObservationStatus.Final;
            InjuryIncidentObs.Code = new CodeableConcept(CodeSystems.LOINC, "11374-6", "Injury incident description Narrative", null);
            InjuryIncidentObs.Subject = new ResourceReference("urn:uuid:" + Decedent.Id);
            InjuryIncidentObs.Effective = new FhirDateTime();
            InjuryIncidentObs.Effective.Extension.Add(NewBlankPartialDateTimeExtension(true));
            AddReferenceToComposition(InjuryIncidentObs.Id, "DeathInvestigation");
            Bundle.AddResourceEntry(InjuryIncidentObs, "urn:uuid:" + InjuryIncidentObs.Id);
        }

        /// <summary>Death Location.</summary>
        private Location DeathLocationLoc;
        /// <summary>Create Death Location </summary>
        private void CreateDeathLocation()
        {
            DeathLocationLoc = new Location();
            DeathLocationLoc.Id = Guid.NewGuid().ToString();
            DeathLocationLoc.Meta = new Meta();
            string[] deathlocation_profile = { ProfileURL.DeathLocation };
            DeathLocationLoc.Meta.Profile = deathlocation_profile;
            DeathLocationLoc.Type.Add(new CodeableConcept(CodeSystems.LocationType, "death", "death location", null));
            DeathLocationLoc.Name = DeathRecord.BlankPlaceholder; // We cannot have a blank string, but the field is required to be present
            AddReferenceToComposition(DeathLocationLoc.Id, "DeathInvestigation");
            Bundle.AddResourceEntry(DeathLocationLoc, "urn:uuid:" + DeathLocationLoc.Id);
        }



        /// <summary>Date Of Death.</summary>
        private Observation DeathDateObs;
        /// <summary>Create Death Date Observation.</summary>
        private void CreateDeathDateObs()
        {
            DeathDateObs = new Observation();
            DeathDateObs.Id = Guid.NewGuid().ToString();
            DeathDateObs.Meta = new Meta();
            string[] deathdate_profile = { ProfileURL.DeathDate };
            DeathDateObs.Meta.Profile = deathdate_profile;
            DeathDateObs.Status = ObservationStatus.Final;
            DeathDateObs.Code = new CodeableConcept(CodeSystems.LOINC, "81956-5", "Date+time of death", null);

            // Decedent is present in DeathCertificateDocuments, and absent in all other bundles.
            if (Decedent != null)
            {
                DeathDateObs.Subject = new ResourceReference("urn:uuid:" + Decedent.Id);
            }

            // A DeathDate can be represented either using the PartialDateTime or the valueDateTime; we always prefer
            // the PartialDateTime representation (though we'll correctly read records using valueDateTime) and so we
            // by default set up all the PartialDate extensions with a default state of "data absent"
            DeathDateObs.Value = new FhirDateTime();
            DeathDateObs.Value.Extension.Add(NewBlankPartialDateTimeExtension(true));
            DeathDateObs.Method = null;

            AddReferenceToComposition(DeathDateObs.Id, "DeathInvestigation");
            Bundle.AddResourceEntry(DeathDateObs, "urn:uuid:" + DeathDateObs.Id);
        }

        /// <summary>Create Death Date Pronouncement Observation Component Component.</summary>
        private Observation.ComponentComponent CreateDateOfDeathPronouncementObs() {
            if (DeathDateObs == null)
            {
                CreateDeathDateObs(); // Create it
            }
            var datetimePronouncedDeadComponent = new Observation.ComponentComponent();
            var pronComp = DeathDateObs.Component.FirstOrDefault(entry => ((Observation.ComponentComponent)entry).Code != null
                        && ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault() != null && ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault().Code == "80616-6");
            if (pronComp != null)
            {
                datetimePronouncedDeadComponent = pronComp;
            }
            else
            {
                datetimePronouncedDeadComponent = new Observation.ComponentComponent();
                datetimePronouncedDeadComponent.Code = new CodeableConcept(CodeSystems.LOINC, "80616-6", "Date and time pronounced dead [US Standard Certificate of Death]", null);
                DeathDateObs.Component.Add(datetimePronouncedDeadComponent);
            }
            datetimePronouncedDeadComponent.Value = null; // will be set to FhirDateTime for full datetime or a Time if only time is present
            return datetimePronouncedDeadComponent;
        }

        /// <summary>Date Of Surgery.</summary>
        private Observation SurgeryDateObs;
        /// <summary>Create Surgery Date Observation.</summary>
        private void CreateSurgeryDateObs()
        {
            SurgeryDateObs = new Observation();
            SurgeryDateObs.Id = Guid.NewGuid().ToString();
            SurgeryDateObs.Meta = new Meta();
            string[] profile = { ProfileURL.SurgeryDate };
            SurgeryDateObs.Meta.Profile = profile;
            SurgeryDateObs.Status = ObservationStatus.Final;
            SurgeryDateObs.Code = new CodeableConcept(CodeSystems.LOINC, "80992-1", "Date and time of surgery", null);
            SurgeryDateObs.Subject = new ResourceReference("urn:uuid:" + Decedent.Id);
            // A SurgeryDate can be represented either using the PartialDateTime or the valueDateTime as with DeathDate above
            SurgeryDateObs.Value = new FhirDateTime();
            SurgeryDateObs.Value.Extension.Add(NewBlankPartialDateTimeExtension(false));
            AddReferenceToComposition(SurgeryDateObs.Id, "DeathInvestigation");
            Bundle.AddResourceEntry(SurgeryDateObs, "urn:uuid:" + SurgeryDateObs.Id);
        }
        // Coded Observations
        /// <summary> Activity at Time of Death </summary>
        private Observation ActivityAtTimeOfDeathObs;
        /// <summary>Create an empty ActivityAtTimeOfDeathObs, to be populated in ActivityAtDeath.</summary>
        private void CreateActivityAtTimeOfDeathObs()
        {
            ActivityAtTimeOfDeathObs = new Observation();
            ActivityAtTimeOfDeathObs.Id = Guid.NewGuid().ToString();
            ActivityAtTimeOfDeathObs.Meta = new Meta();
            string[] profile = { ProfileURL.ActivityAtTimeOfDeath };
            ActivityAtTimeOfDeathObs.Meta.Profile = profile;
            ActivityAtTimeOfDeathObs.Status = ObservationStatus.Final;
            ActivityAtTimeOfDeathObs.Code = new CodeableConcept(CodeSystems.LOINC, "80626-5", "Activity at time of death [CDC]", null);
            ActivityAtTimeOfDeathObs.Subject = new ResourceReference("urn:uuid:" + Decedent.Id);
            AddReferenceToComposition(ActivityAtTimeOfDeathObs.Id, "CodedContent");
            Bundle.AddResourceEntry(ActivityAtTimeOfDeathObs, "urn:uuid:" + ActivityAtTimeOfDeathObs.Id);
        }
        /// <summary> Automated Underlying Cause of Death </summary>
        private Observation AutomatedUnderlyingCauseOfDeathObs;
        /// <summary>Create an empty AutomatedUnderlyingCODObs, to be populated in AutomatedUnderlyingCOD.</summary>
        private void CreateAutomatedUnderlyingCauseOfDeathObs()
        {
            AutomatedUnderlyingCauseOfDeathObs = new Observation();
            AutomatedUnderlyingCauseOfDeathObs.Id = Guid.NewGuid().ToString();
            AutomatedUnderlyingCauseOfDeathObs.Meta = new Meta();
            string[] profile = { ProfileURL.AutomatedUnderlyingCauseOfDeath };
            AutomatedUnderlyingCauseOfDeathObs.Meta.Profile = profile;
            AutomatedUnderlyingCauseOfDeathObs.Status = ObservationStatus.Final;
            AutomatedUnderlyingCauseOfDeathObs.Code = new CodeableConcept(CodeSystems.LOINC, "80358-5", "Cause of death.underlying [Automated]", null);
            AutomatedUnderlyingCauseOfDeathObs.Subject = new ResourceReference("urn:uuid:" + Decedent.Id);
            AddReferenceToComposition(AutomatedUnderlyingCauseOfDeathObs.Id, "CodedContent");
            Bundle.AddResourceEntry(AutomatedUnderlyingCauseOfDeathObs, "urn:uuid:" + AutomatedUnderlyingCauseOfDeathObs.Id);
        }
        /// <summary> Manual Underlying Cause of Death </summary>
        private Observation ManualUnderlyingCauseOfDeathObs;
        /// <summary>Create an empty AutomatedUnderlyingCODObs, to be populated in AutomatedUnderlyingCOD.</summary>
        private void CreateManualUnderlyingCauseOfDeathObs()
        {
            ManualUnderlyingCauseOfDeathObs = new Observation();
            ManualUnderlyingCauseOfDeathObs.Id = Guid.NewGuid().ToString();
            ManualUnderlyingCauseOfDeathObs.Meta = new Meta();
            string[] profile = { ProfileURL.AutomatedUnderlyingCauseOfDeath };
            ManualUnderlyingCauseOfDeathObs.Meta.Profile = profile;
            ManualUnderlyingCauseOfDeathObs.Status = ObservationStatus.Final;
            ManualUnderlyingCauseOfDeathObs.Code = new CodeableConcept(CodeSystems.LOINC, "80359-3", "Cause of death.underlying [Manual]", null);
            ManualUnderlyingCauseOfDeathObs.Subject = new ResourceReference("urn:uuid:" + Decedent.Id);
            AddReferenceToComposition(ManualUnderlyingCauseOfDeathObs.Id, "CodedContent");
            Bundle.AddResourceEntry(ManualUnderlyingCauseOfDeathObs, "urn:uuid:" + ManualUnderlyingCauseOfDeathObs.Id);
        }

        /// <summary> Place Of Injury </summary>
        private Observation PlaceOfInjuryObs;
        /// <summary>Create an empty PlaceOfInjuryObs, to be populated in PlaceOfInjury.</summary>
        private void CreatePlaceOfInjuryObs()
        {
            PlaceOfInjuryObs = new Observation();
            PlaceOfInjuryObs.Id = Guid.NewGuid().ToString();
            PlaceOfInjuryObs.Meta = new Meta();
            string[] profile = { ProfileURL.AutomatedUnderlyingCauseOfDeath };
            PlaceOfInjuryObs.Meta.Profile = profile;
            PlaceOfInjuryObs.Status = ObservationStatus.Final;
            PlaceOfInjuryObs.Code = new CodeableConcept(CodeSystems.LOINC, "11376-1", "Injury location", null);
            PlaceOfInjuryObs.Subject = new ResourceReference("urn:uuid:" + Decedent.Id);
            AddReferenceToComposition(PlaceOfInjuryObs.Id, "CodedContent");
            Bundle.AddResourceEntry(PlaceOfInjuryObs, "urn:uuid:" + PlaceOfInjuryObs.Id);
        }

        /// <summary>The Decedent's Race and Ethnicity provided by Jurisdiction.</summary>
        private Observation CodedRaceAndEthnicityObs;

        /// <summary>Create an empty CodedRaceAndEthnicityObs, to be populated in Various Methods.</summary>
        private void CreateCodedRaceAndEthnicityObs()
        {
            CodedRaceAndEthnicityObs = new Observation();
            CodedRaceAndEthnicityObs.Id = Guid.NewGuid().ToString();
            CodedRaceAndEthnicityObs.Meta = new Meta();
            string[] profile = { ProfileURL.CodedRaceAndEthnicity };
            CodedRaceAndEthnicityObs.Meta.Profile = profile;
            CodedRaceAndEthnicityObs.Status = ObservationStatus.Final;
            CodedRaceAndEthnicityObs.Code = new CodeableConcept(CodeSystems.ObservationCode, "codedraceandethnicity", "Coded Race and Ethnicity", null);
            CodedRaceAndEthnicityObs.Subject = new ResourceReference("urn:uuid:" + Decedent.Id);
            AddReferenceToComposition(CodedRaceAndEthnicityObs.Id, "CodedContent");
            Bundle.AddResourceEntry(CodedRaceAndEthnicityObs, "urn:uuid:" + CodedRaceAndEthnicityObs.Id);
        }
        /// <summary>Entity Axis Cause of Death</summary>
        private List<Observation> EntityAxisCauseOfDeathObsList;

        /// <summary>Record Axis Cause of Death</summary>
        private List<Observation> RecordAxisCauseOfDeathObsList;

    }
}