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

namespace VRDR
{
    /// <summary>Class <c>DeathRecord</c> models a FHIR Vital Records Death Reporting (VRDR) Death
    /// Record. This class was designed to help consume and produce death records that follow the
    /// HL7 FHIR Vital Records Death Reporting Implementation Guide, as described at:
    /// http://hl7.org/fhir/us/vrdr and https://github.com/hl7/vrdr.
    /// </summary>
    public class DeathRecord
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
        // ///<summary>The Pronouncer of death.</summary>
        //private Practitioner Pronouncer;

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
        // ///<summary>The Mortician.</summary>
        //private Practitioner Mortician;

        /// <summary>The Certification.</summary>
        private Procedure DeathCertification;

        // /// <summary>Create Death Certification.</summary>
        // private void CreateDeathCertification(){
        //     CreateDeathCertification();

        // }

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

        /// <summary>Create a Cause Of Death Condition </summary>
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


        /// <summary>Cause Of Death Condition Line A (#1).</summary>
        private Observation CauseOfDeathConditionA;

        /// <summary>Cause Of Death Condition Line B (#2).</summary>
        private Observation CauseOfDeathConditionB;

        /// <summary>Cause Of Death Condition Line C (#3).</summary>
        private Observation CauseOfDeathConditionC;

        /// <summary>Cause Of Death Condition Line D (#4).</summary>
        private Observation CauseOfDeathConditionD;

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
            AddReferenceToComposition(DecedentEducationLevel.Id, "DecedentDemographic");
            Bundle.AddResourceEntry(DecedentEducationLevel, "urn:uuid:" + DecedentEducationLevel.Id);
        }

        /// <summary>Birth Record Identifier.</summary>
        private Observation BirthRecordIdentifier;

        /// <summary>Emerging Issues.</summary>
        protected Observation EmergingIssues;

        /// <summary>
        /// Coding Status
        /// </summary>
        private Parameters CodingStatusValues;

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
        // /// <summary>The Funeral Home Director.</summary>
        // private PractitionerRole FuneralHomeDirector;


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

        // Build a blank PartialDateTime extension (which means all the data absent reasons are present to note that the data is not in fact
        // present); takes an optional flag to determine if this extension should include the time field, which is not always needed
        private Extension NewBlankPartialDateTimeExtension(bool includeTime = true)
        {
            Extension partialDateTime = new Extension(includeTime ? ExtensionURL.PartialDateTime : ExtensionURL.PartialDate, null);
            Extension year = new Extension(ExtensionURL.DateYear, null);
            year.Extension.Add(new Extension(OtherExtensionURL.DataAbsentReason, new Code("unknown")));
            partialDateTime.Extension.Add(year);
            Extension month = new Extension(ExtensionURL.DateMonth, null);
            month.Extension.Add(new Extension(OtherExtensionURL.DataAbsentReason, new Code("unknown")));
            partialDateTime.Extension.Add(month);
            Extension day = new Extension(ExtensionURL.DateDay, null);
            day.Extension.Add(new Extension(OtherExtensionURL.DataAbsentReason, new Code("unknown")));
            partialDateTime.Extension.Add(day);
            if (includeTime)
            {
                Extension time = new Extension(ExtensionURL.DateTime, null);
                time.Extension.Add(new Extension(OtherExtensionURL.DataAbsentReason, new Code("unknown")));
                partialDateTime.Extension.Add(time);
            }
            return partialDateTime;
        }
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
            if(Decedent != null)
            {
                DeathDateObs.Subject = new ResourceReference("urn:uuid:" + Decedent.Id);
            }
            // A DeathDate can be represented either using the PartialDateTime or the valueDateTime; we always prefer
            // the PartialDateTime representation (though we'll correctly read records using valueDateTime) and so we
            // by default set up all the PartialDate extensions with a default state of "data absent"
            DeathDateObs.Value = new FhirDateTime();
            DeathDateObs.Value.Extension.Add(NewBlankPartialDateTimeExtension(true));
            AddReferenceToComposition(DeathDateObs.Id, "DeathInvestigation");
            Bundle.AddResourceEntry(DeathDateObs, "urn:uuid:" + DeathDateObs.Id);
        }
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

        // /// <summary>Transportation Role.</summary>
        // private Observation TransportationRoleObs;

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

        /// <summary>Date Of Surgery.</summary>
        private Observation SurgeryDateObs;

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

            // // Start with an empty pronouncer.
            // Pronouncer = new Practitioner();
            // Pronouncer.Id = Guid.NewGuid().ToString();
            // Pronouncer.Meta = new Meta();
            // string[] pronouncer_profile = { OtherProfileURL.USCorePractitioner };
            // Pronouncer.Meta.Profile = pronouncer_profile;

            // Start with an empty mortician.
            // InitializeMorticianIfNull();

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
                    Extension replaceExt = new Extension(ExtensionURL.ReplaceStatus , DictToCodeableConcept(ReplaceStatus) );
                    mortRosterBundle.Meta.Extension.Add(replaceExt);
            }
            Extension aliasExt = new Extension(ExtensionURL.AliasStatus, new FhirBoolean(alias));
            mortRosterBundle.Meta.Extension.Add(aliasExt);
            return mortRosterBundle;
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
                }
                UpdateDeathRecordIdentifier();
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
                if (FilingFormat.ContainsKey("code"))
                {
                    return FilingFormat["code"];
                }
                return null;
            }
            set
            {
                SetCodeValue("FilingFormat", value, VRDR.ValueSets.FilingFormat.Codes);
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
                return "";
            }
            set
            {
                Composition.Date = value;
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
                return "";
            }
            set
            {
                // TODO: Handle case where Composition == null (either create it or throw exception)
                Composition.Extension.RemoveAll(ext => ext.Url == ExtensionURL.StateSpecificField);
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
                if (ReplaceStatus.ContainsKey("code"))
                {
                    return ReplaceStatus["code"];
                }
                return null;
            }
            set
            {
                SetCodeValue("ReplaceStatus", value, VRDR.ValueSets.ReplaceStatus.Codes);
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
                        if (CertificationRole.ContainsKey("text"))
                        {
                            return (CertificationRole["text"]);
                        }
                        return ("Other");
                    }
                    else
                    {
                        return code;
                    }
                }
                return null;
            }
            set
            {
                if (String.IsNullOrWhiteSpace(value)){
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
                if (IsDictEmptyOrDefault(value) && MannerOfDeath == null){
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
                        COD1A = value[0].Item1;
                        INTERVAL1A = value[0].Item2;
                    }
                    if (value.Length > 1)
                    {
                        COD1B = value[1].Item1;
                        INTERVAL1B = value[1].Item2;
                    }
                    if (value.Length > 2)
                    {
                        COD1C = value[2].Item1;
                        INTERVAL1C = value[2].Item2;
                    }
                    if (value.Length > 3)
                    {
                        COD1D = value[3].Item1;
                        INTERVAL1D = value[3].Item2;
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
                return "";
            }
            set
            {
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
                return "";
            }
            set
            {
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
                return "";
            }
            set
            {
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
                return "";
            }
            set
            {
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

        // /// <summary>Decedent's Gender.</summary>
        // /// <value>the decedent's gender</value>
        // /// <example>
        // /// <para>// Setter:</para>
        // /// <para>ExampleDeathRecord.Gender = "female";</para>
        // /// <para>// Getter:</para>
        // /// <para>Console.WriteLine($"Gender: {ExampleDeathRecord.Gender}");</para>
        // /// </example>
        // [Property("Gender", Property.Types.String, "Decedent Demographics", "Decedent's Gender.", true, IGURL.Decedent, true, 11)]
        // [FHIRPath("Bundle.entry.resource.where($this is Patient)", "gender")]
        // public string Gender
        // {
        //     get
        //     {
        //         return GetFirstString("Bundle.entry.resource.where($this is Patient).gender");
        //     }
        //     set
        //     {
        //         switch (value)
        //         {
        //             case "male":
        //             case "Male":
        //             case "m":
        //             case "M":
        //                 Decedent.Gender = AdministrativeGender.Male;
        //                 break;
        //             case "female":
        //             case "Female":
        //             case "f":
        //             case "F":
        //                 Decedent.Gender = AdministrativeGender.Female;
        //                 break;
        //             case "other":
        //             case "Other":
        //             case "o":
        //             case "O":
        //                 Decedent.Gender = AdministrativeGender.Other;
        //                 break;
        //             case "unknown":
        //             case "Unknown":
        //             case "u":
        //             case "U":
        //                 Decedent.Gender = AdministrativeGender.Unknown;
        //                 break;
        //         }
        //     }
        // }

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
                if (IsDictEmptyOrDefault(value) && Decedent.Extension == null){
                    return;
                }
                Decedent.Extension.RemoveAll(ext => ext.Url == ExtensionURL.NVSSSexAtDeath);
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
        /// <value>the decedent's year of birth</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.BirthYear = 1928;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Decedent Year of Birth: {ExampleDeathRecord.BirthYear}");</para>
        /// </example>
        [Property("BirthYear", Property.Types.UInt32, "Decedent Demographics", "Decedent's Year of Birth.", true, IGURL.Decedent, true, 14)]
        [FHIRPath("Bundle.entry.resource.where($this is Patient).birthDate", "")]
        public uint? BirthYear
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
        /// <value>the decedent's month of birth</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.BirthMonth = 11;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Decedent Month of Birth: {ExampleDeathRecord.BirthMonth}");</para>
        /// </example>
        [Property("BirthMonth", Property.Types.UInt32, "Decedent Demographics", "Decedent's Month of Birth.", true, IGURL.Decedent, true, 14)]
        [FHIRPath("Bundle.entry.resource.where($this is Patient).birthDate", "")]
        public uint? BirthMonth
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
        /// <value>the decedent's dau of birth</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.BirthDay = 11;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Decedent Day of Birth: {ExampleDeathRecord.BirthDay}");</para>
        /// </example>
        [Property("BirthDay", Property.Types.UInt32, "Decedent Demographics", "Decedent's Day of Birth.", true, IGURL.Decedent, true, 14)]
        [FHIRPath("Bundle.entry.resource.where($this is Patient).birthDate", "")]
        public uint? BirthDay
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
                if (BirthYear != null && BirthMonth != null && BirthDay != null)
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
                    BirthYear = (uint?)parsedDate.Year;
                    BirthMonth = (uint?)parsedDate.Month;
                    BirthDay = (uint?)parsedDate.Day;
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
                component.Code = new CodeableConcept(CodeSystems.ComponentCode, NvssEthnicity.Mexican, NvssEthnicity.Mexican, null);
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
                if (Ethnicity1.ContainsKey("code"))
                {
                    return Ethnicity1["code"];
                }
                return null;
            }
            set
            {
                SetCodeValue("Ethnicity1", value, VRDR.ValueSets.HispanicNoUnknown.Codes);
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
                component.Code = new CodeableConcept(CodeSystems.ComponentCode, NvssEthnicity.PuertoRican, NvssEthnicity.PuertoRican, null);
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
                if (Ethnicity2.ContainsKey("code"))
                {
                    return Ethnicity2["code"];
                }
                return null;
            }
            set
            {
                SetCodeValue("Ethnicity2", value, VRDR.ValueSets.HispanicNoUnknown.Codes);
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
                component.Code = new CodeableConcept(CodeSystems.ComponentCode, NvssEthnicity.Cuban, NvssEthnicity.Cuban, null);
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
                if (Ethnicity3.ContainsKey("code"))
                {
                    return Ethnicity3["code"];
                }
                return null;
            }
            set
            {
                SetCodeValue("Ethnicity3", value, VRDR.ValueSets.HispanicNoUnknown.Codes);
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
                component.Code = new CodeableConcept(CodeSystems.ComponentCode, NvssEthnicity.Other, NvssEthnicity.Other, null);
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
                if (Ethnicity4.ContainsKey("code"))
                {
                    return Ethnicity4["code"];
                }
                return null;
            }
            set
            {
                SetCodeValue("Ethnicity4", value, VRDR.ValueSets.HispanicNoUnknown.Codes);
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
                return "";
            }
            set
            {
                if (InputRaceAndEthnicityObs == null)
                {
                    CreateInputRaceEthnicityObs();
                }
                InputRaceAndEthnicityObs.Component.RemoveAll(c => c.Code.Coding[0].Code == NvssEthnicity.Literal);
                Observation.ComponentComponent component = new Observation.ComponentComponent();
                component.Code = new CodeableConcept(CodeSystems.ComponentCode, NvssEthnicity.Literal, NvssEthnicity.Literal, null);
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
                if (InputRaceAndEthnicityObs == null)
                {
                    CreateInputRaceEthnicityObs();
                }
                var booleanRaceCodes = NvssRace.GetBooleanRaceCodes();
                foreach (Tuple<string, string> element in value)
                {
                    InputRaceAndEthnicityObs.Component.RemoveAll(c => c.Code.Coding[0].Code == element.Item1);
                    Observation.ComponentComponent component = new Observation.ComponentComponent();
                    component.Code = new CodeableConcept(CodeSystems.ComponentCode, element.Item1, element.Item1, null);
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
                if(!IsDictEmptyOrDefault(value))
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
                if (MaritalStatusEditFlag.ContainsKey("code"))
                {
                    return MaritalStatusEditFlag["code"];
                }
                return null;
            }
            set
            {
                SetCodeValue("MaritalStatusEditFlag", value, VRDR.ValueSets.EditBypass0124.Codes);
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
                if (SpouseAlive.ContainsKey("code"))
                {
                    return SpouseAlive["code"];
                }
                return null;
            }
            set
            {
                SetCodeValue("SpouseAlive", value, VRDR.ValueSets.SpouseAlive.Codes);
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
                if( IsDictEmptyOrDefault(value) && DecedentEducationLevel == null)
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
                if( IsDictEmptyOrDefault(value) && DecedentEducationLevel == null)
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
                return "";
            }
            set
            {
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
                    return CodeableConceptToDict((CodeableConcept)UsualWork.Value)["text"];
                }
                return "";
            }
            set
            {
                if( (String.IsNullOrEmpty(value)))
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
                    if (component != null && component.Value != null && component.Value as CodeableConcept != null)
                    {
                        return CodeableConceptToDict((CodeableConcept)component.Value)["text"];
                    }
                }
                return "";
            }
            set
            {
                if( (String.IsNullOrEmpty(value)))
                {
                    return;
                }
                if (UsualWork == null)
                {
                    CreateUsualWork();
                }

                UsualWork.Component.RemoveAll(cmp => cmp.Code != null && cmp.Code.Coding != null && cmp.Code.Coding.Count() > 0 && cmp.Code.Coding.First().Code == "21844-6");
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
                if (IsDictEmptyOrDefault(value) && MilitaryServiceObs == null){
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
                if (MilitaryService.ContainsKey("code"))
                {
                    return (MilitaryService["code"]);
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
                if (value != null && !String.IsNullOrWhiteSpace(value))
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
                if (DecedentDispositionMethod.ContainsKey("code"))
                {
                    return DecedentDispositionMethod["code"];
                }
                return null;
            }
            set
            {
                SetCodeValue("DecedentDispositionMethod", value, VRDR.ValueSets.MethodOfDisposition.Codes);
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
                if (IsDictEmptyOrDefault(value) && AutopsyPerformed == null){
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
        // ** Pronouncer not curently supported **/
        // /// <summary>Given name(s) of Pronouncer.</summary>
        // /// <value>the Pronouncer's name (first, middle, etc.)</value>
        // /// <example>
        // /// <para>// Setter:</para>
        // /// <para>string[] names = { "FD", "Middle" };</para>
        // /// <para>ExampleDeathRecord.PronouncerGivenNames = names;</para>
        // /// <para>// Getter:</para>
        // /// <para>Console.WriteLine($"Pronouncer Given Name(s): {string.Join(", ", ExampleDeathRecord.PronouncerGivenNames)}");</para>
        // /// </example>
        // [Property("Pronouncer Given Names", Property.Types.StringArr, "Death Investigation", "Given name(s) of Pronouncer.", true, "http://build.fhir.org/ig/HL7/vrdr/StructureDefinition-VRDR-Death-Pronouncement-Performer.html", false, 21)]
        // [FHIRPath("Bundle.entry.resource.where($this is Practitioner).where(meta.profile='http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Death-Pronouncement-Performer')", "name")]
        // public string[] PronouncerGivenNames
        // {
        //     get
        //     {
        //         if (Pronouncer != null && Pronouncer.Name.Count() > 0)
        //         {
        //             return Pronouncer.Name.First().Given.ToArray();
        //         }
        //         return new string[0];
        //     }
        //     set
        //     {
        //         HumanName name = Pronouncer.Name.SingleOrDefault(n => n.Use == HumanName.NameUse.Official);
        //         if (name != null)
        //         {
        //             name.Given = value;
        //         }
        //         else
        //         {
        //             name = new HumanName();
        //             name.Use = HumanName.NameUse.Official;
        //             name.Given = value;
        //             Pronouncer.Name.Add(name);
        //         }
        //     }
        // }

        // /// <summary>Family name of Pronouncer.</summary>
        // /// <value>the Pronouncer's family name (i.e. last name)</value>
        // /// <example>
        // /// <para>// Setter:</para>
        // /// <para>ExampleDeathRecord.PronouncerFamilyName = "Last";</para>
        // /// <para>// Getter:</para>
        // /// <para>Console.WriteLine($"Pronouncer's Last Name: {ExampleDeathRecord.PronouncerFamilyName}");</para>
        // /// </example>
        // [Property("Pronouncer Family Name", Property.Types.String, "Death Investigation", "Family name of Pronouncer.", true, "http://build.fhir.org/ig/HL7/vrdr/StructureDefinition-VRDR-Death-Pronouncement-Performer.html", false, 22)]
        // [FHIRPath("Bundle.entry.resource.where($this is Practitioner).where(meta.profile='http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Death-Pronouncement-Performer')", "name")]
        // public string PronouncerFamilyName
        // {
        //     get
        //     {
        //         if (Pronouncer != null && Pronouncer.Name.Count() > 0)
        //         {
        //             return Pronouncer.Name.First().Family;
        //         }
        //         return null;
        //     }
        //     set
        //     {
        //         HumanName name = Pronouncer.Name.FirstOrDefault();
        //         if (name != null && !String.IsNullOrEmpty(value))
        //         {
        //             name.Family = value;
        //         }
        //         else if (!String.IsNullOrEmpty(value))
        //         {
        //             name = new HumanName();
        //             name.Use = HumanName.NameUse.Official;
        //             name.Family = value;
        //             Pronouncer.Name.Add(name);
        //         }
        //     }
        // }

        // /// <summary>Pronouncer's Suffix.</summary>
        // /// <value>the Pronouncer's suffix</value>
        // /// <example>
        // /// <para>// Setter:</para>
        // /// <para>ExampleDeathRecord.PronouncerSuffix = "Jr.";</para>
        // /// <para>// Getter:</para>
        // /// <para>Console.WriteLine($"Pronouncer Suffix: {ExampleDeathRecord.PronouncerSuffix}");</para>
        // /// </example>
        // [Property("Pronouncer Suffix", Property.Types.String, "Death Investigation", "Pronouncer's Suffix.", true, "http://build.fhir.org/ig/HL7/vrdr/StructureDefinition-VRDR-Death-Pronouncement-Performer.html", false, 23)]
        // [FHIRPath("Bundle.entry.resource.where($this is Practitioner).where(meta.profile='http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Death-Pronouncement-Performer')", "suffix")]
        // public string PronouncerSuffix
        // {
        //     get
        //     {
        //         if (Pronouncer != null && Pronouncer.Name.Count() > 0 && Pronouncer.Name.First().Suffix.Count() > 0)
        //         {
        //             return Pronouncer.Name.First().Suffix.First();
        //         }
        //         return null;
        //     }
        //     set
        //     {
        //         HumanName name = Pronouncer.Name.FirstOrDefault();
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
        //             Pronouncer.Name.Add(name);
        //         }
        //     }
        // }

        // /// <summary>Pronouncer Identifier.</summary>
        // /// <value>the Pronouncer identification. A Dictionary representing a system (e.g. NPI) and a value, containing the following key/value pairs:
        // /// <para>"system" - the identifier system, e.g. US NPI</para>
        // /// <para>"value" - the identifier value, e.g. US NPI number</para>
        // /// </value>
        // /// <example>
        // /// <para>// Setter:</para>
        // /// <para>Dictionary&lt;string, string&gt; identifier = new Dictionary&lt;string, string&gt;();</para>
        // /// <para>identifier.Add("system", "http://hl7.org/fhir/sid/us-npi");</para>
        // /// <para>identifier.Add("value", "1234567890");</para>
        // /// <para>ExampleDeathRecord.PronouncerIdentifier = identifier;</para>
        // /// <para>// Getter:</para>
        // /// <para>Console.WriteLine($"\tPronouncer Identifier: {ExampleDeathRecord.PronouncerIdentifier['value']}");</para>
        // /// </example>
        // [Property("Pronouncer Identifier", Property.Types.Dictionary, "Death Investigation", "Pronouncer Identifier.", true, "http://build.fhir.org/ig/HL7/vrdr/StructureDefinition-VRDR-Death-Pronouncement-Performer.html", false, 24)]
        // [PropertyParam("system", "The identifier system.")]
        // [PropertyParam("value", "The identifier value.")]
        // [FHIRPath("Bundle.entry.resource.where($this is Practitioner).where(meta.profile='http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Death-Pronouncement-Performer')", "identifier")]
        // public Dictionary<string, string> PronouncerIdentifier
        // {
        //     get
        //     {
        //         Identifier identifier = Pronouncer?.Identifier?.FirstOrDefault();
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
        //         if (Pronouncer.Identifier.Count > 0)
        //         {
        //             Pronouncer.Identifier.Clear();
        //         }
        //         if(value.ContainsKey("system") && value.ContainsKey("value")) {
        //             Identifier identifier = new Identifier();
        //             identifier.System = value["system"];
        //             identifier.Value = value["value"];
        //             Pronouncer.Identifier.Add(identifier);
        //         }
        //     }
        // }

        /// <summary>Getter helper for anything that uses PartialDateTime, allowing a particular date field (year, month, or day) to be read from the extension</summary>
        private uint? GetPartialDate(Extension partialDateTime, string partURL)
        {
            if (partialDateTime != null)
            {
                Extension part = partialDateTime.Extension.Find(ext => ext.Url == partURL);
                if (part != null)
                {
                    Extension dataAbsent = part.Extension.Find(ext => ext.Url == OtherExtensionURL.DataAbsentReason);
                    if (dataAbsent != null || part.Value == null)
                    {
                        // There's either a specific claim that there's no data or actually no data, so return null
                        return null;
                    }
                    return (uint?)((UnsignedInt)part.Value).Value; // Untangle a FHIR UnsignedInt in an extension into a uint
                }
            }
            return null;
        }

        /// <summary>Setter helper for anything that uses PartialDateTime, allowing a particular date field (year, month, or day) to be set in the extension</summary>
        private void SetPartialDate(Extension partialDateTime, string partURL, uint? value)
        {
            Extension part = partialDateTime.Extension.Find(ext => ext.Url == partURL);
            part.Extension.RemoveAll(ext => ext.Url == OtherExtensionURL.DataAbsentReason);
            if (value != null)
            {
                part.Value = new UnsignedInt((int)value);
            }
            else
            {
                part.Value = null;
                part.Extension.Add(new Extension(OtherExtensionURL.DataAbsentReason, new Code("unknown")));
            }
        }

        /// <summary>Getter helper for anything that uses PartialDateTime, allowing the time to be read from the extension</summary>
        private string GetPartialTime(Extension partialDateTime)
        {
            if (partialDateTime != null)
            {
                Extension part = partialDateTime.Extension.Find(ext => ext.Url == ExtensionURL.DateTime);
                if (part != null)
                {
                    Extension dataAbsent = part.Extension.Find(ext => ext.Url == OtherExtensionURL.DataAbsentReason);
                    if (dataAbsent != null || part.Value == null )
                    {
                        // There's either a specific claim that there's no data or actually no data, so return null
                        return null;
                    }
                    return part.Value.ToString();
                }
            }
            return null;
        }

        /// <summary>Setter helper for anything that uses PartialDateTime, allowing the time to be set in the extension</summary>
        private void SetPartialTime(Extension partialDateTime, String value)
        {
            Extension part = partialDateTime.Extension.Find(ext => ext.Url == ExtensionURL.DateTime);
            part.Extension.RemoveAll(ext => ext.Url == OtherExtensionURL.DataAbsentReason);
            if (value != null)
            {
                part.Value = new Time(value);
            }
            else
            {
                part.Value = null;
                part.Extension.Add(new Extension(OtherExtensionURL.DataAbsentReason, new Code("unknown")));
            }
        }

        /// <summary>Getter helper for anything that can have a regular FHIR date/time or a PartialDateTime extension, allowing a particular date
        /// field (year, month, or day) to be read from either the value or the extension</summary>
        private uint? GetDateFragmentOrPartialDate(Element value, string partURL)
        {
            if (value == null)
            {
                return null;
            }
            // If we have a basic value as a valueDateTime use that, otherwise pull from the PartialDateTime extension
            DateTimeOffset? dateTimeOffset = null;
            if (value is FhirDateTime && ((FhirDateTime)value).Value != null)
            {
                dateTimeOffset = ((FhirDateTime)value).ToDateTimeOffset(TimeSpan.Zero);
            }
            else if (value is Date && ((Date)value).Value != null)
            {
                dateTimeOffset = ((Date)value).ToDateTimeOffset();
            }
            if (dateTimeOffset != null)
            {
                switch (partURL)
                {
                    case ExtensionURL.DateYear:
                        return (uint?)((DateTimeOffset)dateTimeOffset).Year;
                    case ExtensionURL.DateMonth:
                        return (uint?)((DateTimeOffset)dateTimeOffset).Month;
                    case ExtensionURL.DateDay:
                        return (uint?)((DateTimeOffset)dateTimeOffset).Day;
                    default:
                        throw new ArgumentException("GetDateFragmentOrPartialDate called with unsupported PartialDateTime segment");
                }
            }
            // Look for either PartialDate or PartialDateTime
            Extension extension = value.Extension.Find(ext => ext.Url == ExtensionURL.PartialDateTime);
            if (extension == null)
            {
                extension = value.Extension.Find(ext => ext.Url == ExtensionURL.PartialDate);
            }
            return GetPartialDate(extension, partURL);
        }

        /// <summary>Getter helper for anything that can have a regular FHIR date/time or a PartialDateTime extension, allowing the time to be read
        /// from either the value or the extension</summary>
        private string GetTimeFragmentOrPartialTime(Element value)
        {
            // If we have a basic value as a valueDateTime use that, otherwise pull from the PartialDateTime extension
            if (value is FhirDateTime && ((FhirDateTime)value).Value != null)
            {
                // Using FhirDateTime's ToDateTimeOffset doesn't keep the time in the original time zone, so we parse the string representation, first using the appropriate segment of
                // the Regex defined at http://hl7.org/fhir/R4/datatypes.html#dateTime to pull out everything except the time zone
                string dateRegex = "([0-9]([0-9]([0-9][1-9]|[1-9]0)|[1-9]00)|[1-9]000)(-(0[1-9]|1[0-2])(-(0[1-9]|[1-2][0-9]|3[0-1])(T([01][0-9]|2[0-3]):[0-5][0-9]:([0-5][0-9]|60)?)?)?)?";
                Match dateStringMatch = Regex.Match(((FhirDateTime)value).ToString(), dateRegex);
                DateTime dateTime;
                if (dateStringMatch != null && DateTime.TryParse(dateStringMatch.ToString(), out dateTime))
                {
                    TimeSpan timeSpan = new TimeSpan(0, dateTime.Hour, dateTime.Minute, dateTime.Second);
                    return timeSpan.ToString(@"hh\:mm\:ss");
                }
                return null;
            }
            return GetPartialTime(value.Extension.Find(ext => ext.Url == ExtensionURL.PartialDateTime));
        }

        // The idea here is that we have getters and setters for each of the parts of the death datetime, which get used in IJEMortality.cs
        // These getters and setters 1) use the DeathDateObs Observation 2) get and set values on the PartialDateTime extension using helpers that
        // can be reused across year, month, etc. 3) interpret null as data being absent, and so set the data absent reason if value is null 4) when
        // getting, look also in the valueDateTime and return the year from there if it happens to be set (but never bother to set it ourselves)

        /// <summary>Decedent's Year of Death.</summary>
        /// <value>the decedent's year of death</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.DeathYear = 2018;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Decedent Year of Death: {ExampleDeathRecord.DeathYear}");</para>
        /// </example>
        [Property("DeathYear", Property.Types.UInt32, "Death Investigation", "Decedent's Year of Death.", true, IGURL.DeathDate, true, 25)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='81956-5')", "")]
        public uint? DeathYear
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
        /// <value>the decedent's month of death</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.DeathMonth = 6;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Decedent Month of Death: {ExampleDeathRecord.DeathMonth}");</para>
        /// </example>
        [Property("DeathMonth", Property.Types.UInt32, "Death Investigation", "Decedent's Month of Death.", true, IGURL.DeathDate, true, 25)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='81956-5')", "")]
        public uint? DeathMonth
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
        /// <value>the decedent's day of death</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.DeathDay = 16;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Decedent Day of Death: {ExampleDeathRecord.DeathDay}");</para>
        /// </example>
        [Property("DeathDay", Property.Types.UInt32, "Death Investigation", "Decedent's Day of Death.", true, IGURL.DeathDate, true, 25)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='81956-5')", "")]
        public uint? DeathDay
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
        /// <value>the decedent's time of death</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.DeathTime = "07:15";</para>
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
                if (DeathDateObs == null)
                {
                    CreateDeathDateObs();
                }
                SetPartialTime(DeathDateObs.Value.Extension.Find(ext => ext.Url == ExtensionURL.PartialDateTime), value);
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
                if (DeathYear != null && DeathMonth != null && DeathDay != null && DeathTime != null)
                {
                    DateTimeOffset parsedTime;
                    if (DateTimeOffset.TryParse(DeathTime, out parsedTime))
                    {
                        DateTimeOffset result = new DateTimeOffset((int)DeathYear, (int)DeathMonth, (int)DeathDay, parsedTime.Hour, parsedTime.Minute, parsedTime.Second, TimeSpan.Zero);
                        return result.ToString("s");
                    }
                }
                else if (DeathYear != null && DeathMonth != null && DeathDay != null)
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
                    DeathYear = (uint?)parsedTime.Year;
                    DeathMonth = (uint?)parsedTime.Month;
                    DeathDay = (uint?)parsedTime.Day;
                    TimeSpan timeSpan = new TimeSpan(0, parsedTime.Hour, parsedTime.Minute, parsedTime.Second);
                    DeathTime = timeSpan.ToString(@"hh\:mm\:ss");
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
        [Property("Date/Time Of Death Pronouncement", Property.Types.StringDateTime, "Death Investigation", "Decedent's Date/Time of Death Pronouncement.", true, IGURL.DeathDate, false, 20)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='81956-5').component.where(code.coding.code='80616-6')", "")]
        public string DateOfDeathPronouncement
        {
            get
            {
                if (DeathDateObs != null && DeathDateObs.Component.Count > 0) // if there is a value for death location type, return it
                {
                    var pronComp = DeathDateObs.Component.FirstOrDefault(entry => ((Observation.ComponentComponent)entry).Code != null
                         && ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault() != null && ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault().Code == "80616-6");
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
                var pronComp = DeathDateObs.Component.FirstOrDefault(entry => ((Observation.ComponentComponent)entry).Code != null
                         && ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault() != null && ((Observation.ComponentComponent)entry).Code.Coding.FirstOrDefault().Code == "80616-6");
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

        /// <summary>Decedent's Year of Surgery.</summary>
        /// <value>the decedent's year of surgery</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.SurgeryYear = 2018;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Decedent Year of Surgery: {ExampleDeathRecord.SurgeryYear}");</para>
        /// </example>
        [Property("SurgeryYear", Property.Types.UInt32, "Death Investigation", "Decedent's Year of Surgery.", true, IGURL.SurgeryDate, true, 25)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='80992-1')", "")]
        public uint? SurgeryYear
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
        /// <value>the decedent's month of surgery</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.SurgeryMonth = 6;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Decedent Month of Surgery: {ExampleDeathRecord.SurgeryMonth}");</para>
        /// </example>
        [Property("SurgeryMonth", Property.Types.UInt32, "Death Investigation", "Decedent's Month of Surgery.", true, IGURL.SurgeryDate, true, 25)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='80992-1')", "")]
        public uint? SurgeryMonth
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
        /// <value>the decedent's day of surgery</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.SurgeryDay = 16;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Decedent Day of Surgery: {ExampleDeathRecord.SurgeryDay}");</para>
        /// </example>
        [Property("SurgeryDay", Property.Types.UInt32, "Death Investigation", "Decedent's Day of Surgery.", true, IGURL.SurgeryDate, true, 25)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='80992-1')", "")]
        public uint? SurgeryDay
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
                if (SurgeryYear != null && SurgeryMonth != null && SurgeryDay != null)
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
                    SurgeryYear = (uint?)parsedDate.Year;
                    SurgeryMonth = (uint?)parsedDate.Month;
                    SurgeryDay = (uint?)parsedDate.Day;
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
                if (IsDictEmptyOrDefault(value) && AutopsyPerformed == null){
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
                if (value != null && !String.IsNullOrWhiteSpace(value))
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
                if (DeathLocationLoc == null)
                {
                    CreateDeathLocation();
                }
                if (DeathLocationLoc.Position == null)
                {
                    DeathLocationLoc.Position = new Location.PositionComponent();
                }
                if (value != null)
                {
                    DeathLocationLoc.Position.Latitude = Convert.ToDecimal(value);
                }

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
                if (DeathLocationLoc == null)
                {
                    CreateDeathLocation();
                }
                if (DeathLocationLoc.Position == null)
                {
                    DeathLocationLoc.Position = new Location.PositionComponent();
                }
                if (value != null)
                {
                    DeathLocationLoc.Position.Longitude = Convert.ToDecimal(value);
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
                if (IsDictEmptyOrDefault(value) && DeathDateObs == null){
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
        /// <para>age.Add("unit", "a"); // USE: http://hl7.org/fhir/us/vrdr/ValueSet/vrdr-units-of-age-vs </para>
        /// <para>ExampleDeathRecord.AgeAtDeath = age;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Age At Death: {ExampleDeathRecord.AgeAtDeath['value']} years");</para>
        /// </example>
        [Property("Age At Death", Property.Types.Dictionary, "Decedent Demographics", "Age At Death.", true, IGURL.DecedentAge, true, 2)]
        [PropertyParam("value", "The unit type, from UnitsOfAge ValueSet.")]
        [PropertyParam("unit", "The quantity value.")]
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
                    age.Add("unit", quantity.Unit == null ? "" : quantity.Unit);
                    age.Add("code", quantity.Code == null ? "" : quantity.Code);
                    age.Add("system", quantity.System == null ? "" : quantity.System);
                    return age;
                }
                return new Dictionary<string, string>() { { "value", "" }, { "unit", "" }, { "code", "" }, { "system", "" } };
            }
            set
            {
                string extractedValue = GetValue(value, "value");
                string extractedCode = GetValue(value, "code");;
                string extractedUnit = GetValue(value, "unit");
                string extractedSystem = GetValue(value, "system");
                if((extractedValue == null &&  extractedCode == null && extractedUnit == null && extractedSystem == null)) // if there is nothing to do, do nothing.
                {
                    return;
                }
                if ( AgeAtDeathObs == null)
                {
                    CreateAgeAtDeathObs();
                }
                Quantity quantity = (Quantity)AgeAtDeathObs.Value;

                if (extractedValue != null)
                {
                    quantity.Value = Convert.ToDecimal(extractedValue);
                }
                if (extractedUnit != null)
                {
                    quantity.Unit = extractedUnit;
                }
                if (extractedCode != null)
                {
                    quantity.Code = extractedCode;
                }
                if (extractedSystem != null){
                    quantity.System = extractedSystem;
                }
                AgeAtDeathObs.Value = (Quantity)quantity;
            }
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
                if (IsDictEmptyOrDefault(value) && AgeAtDeathObs == null){
                    return;
                }
                if (AgeAtDeathObs == null)
                {
                    CreateAgeAtDeathObs();
                }
                AgeAtDeathObs.Value.Extension.RemoveAll(ext => ext.Url == ExtensionURL.BypassEditFlag);
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
                return AgeAtDeathEditFlag.ContainsKey("code") ? AgeAtDeathEditFlag["code"] : null;
            }
            set
            {
                SetCodeValue("AgeAtDeathEditFlag", value, VRDR.ValueSets.EditBypass01.Codes);
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
                if (IsDictEmptyOrDefault(value) && PregnancyObs == null){
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
                 if (IsDictEmptyOrDefault(value) && PregnancyObs == null){
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
                return EmptyCodeableDict();
            }
            set
            {
                if (IsDictEmptyOrDefault(value) && ExaminerContactedObs == null){
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
            if (value == null && InjuryLocationLoc == null){
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
                if (value == null && InjuryLocationLoc == null){
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
                if (value != null)
                {
                    InjuryLocationLoc.Position.Latitude = Convert.ToDecimal(value);
                }

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
                if (value == null && InjuryLocationLoc == null){
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
                if (value != null)
                {
                    InjuryLocationLoc.Position.Longitude = Convert.ToDecimal(value);
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
                if (value == null && InjuryLocationLoc == null){
                    return;
                }
                if (InjuryLocationLoc == null)
                {
                    CreateInjuryLocationLoc();
                    // LinkObservationToLocation(InjuryIncidentObs, InjuryLocationLoc);
                }
                if (value != null && !String.IsNullOrWhiteSpace(value))
                {
                    InjuryLocationLoc.Name = value;
                }
                else
                {
                    InjuryLocationLoc.Name = DeathRecord.BlankPlaceholder; // We cannot have a blank string, but the field is required to be present
                }
            }
        }

        // /// <summary>Description of Injury Location.</summary>
        // /// <value>the injury location description.</value>
        // /// <example>
        // /// <para>// Setter:</para>
        // /// <para>ExampleDeathRecord.InjuryLocationDescription = "Bedford Cemetery";</para>
        // /// <para>// Getter:</para>
        // /// <para>Console.WriteLine($"Injury Location Description: {ExampleDeathRecord.InjuryLocationDescription}");</para>
        // /// </example>
        // [Property("Injury Location Description", Property.Types.String, "Death Investigation", "Description of Injury Location.", true, IGURL.InjuryLocation, true, 36)]
        // [FHIRPath("Bundle.entry.resource.where($this is Location).where(type.coding.code='injury')", "description")]
        // public string InjuryLocationDescription
        // {
        //     get
        //     {
        //         if (InjuryLocationLoc != null)
        //         {
        //             return InjuryLocationLoc.Description;
        //         }
        //         return null;
        //     }
        //     set
        //     {
        //         if (InjuryLocationLoc == null)
        //         {
        //             CreateInjuryLocationLoc();
        //             // LinkObservationToLocation(InjuryIncidentObs, InjuryLocationLoc);
        //             AddReferenceToComposition(InjuryLocationLoc.Id, "DeathInvestigation");
        //             Bundle.AddResourceEntry(InjuryLocationLoc, "urn:uuid:" + InjuryLocationLoc.Id);
        //         }

        //         InjuryLocationLoc.Description = value;
        //     }
        // }

        /// <summary>Decedent's Year of Injury.</summary>
        /// <value>the decedent's year of injury</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.InjuryYear = 2018;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Decedent Year of Injury: {ExampleDeathRecord.InjuryYear}");</para>
        /// </example>
        [Property("InjuryYear", Property.Types.UInt32, "Death Investigation", "Decedent's Year of Injury.", true, IGURL.InjuryIncident, true, 25)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='11374-6')", "")]
        public uint? InjuryYear
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
                if (value == null && InjuryIncidentObs == null){
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
        /// <value>the decedent's month of injury</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.InjuryMonth = 7;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Decedent Month of Injury: {ExampleDeathRecord.InjuryMonth}");</para>
        /// </example>
        [Property("InjuryMonth", Property.Types.UInt32, "Death Investigation", "Decedent's Month of Injury.", true, IGURL.InjuryIncident, true, 25)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='11374-6')", "")]
        public uint? InjuryMonth
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
                if (value == null && InjuryIncidentObs == null){
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
        /// <value>the decedent's day of injury</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.InjuryDay = 22;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Decedent Day of Injury: {ExampleDeathRecord.InjuryDay}");</para>
        /// </example>
        [Property("InjuryDay", Property.Types.UInt32, "Death Investigation", "Decedent's Day of Injury.", true, IGURL.InjuryIncident, true, 25)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='11374-6')", "")]
        public uint? InjuryDay
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
                if (value == null && InjuryIncidentObs == null){
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
        /// <value>the decedent's time of injury</value>
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
                if (String.IsNullOrWhiteSpace(value) && InjuryIncidentObs == null){
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
                if (InjuryYear != null && InjuryMonth != null && InjuryDay != null && InjuryTime != null)
                {
                    DateTimeOffset parsedTime;
                    if (DateTimeOffset.TryParse(InjuryTime, out parsedTime))
                    {
                        DateTimeOffset result = new DateTimeOffset((int)InjuryYear, (int)InjuryMonth, (int)InjuryDay, parsedTime.Hour, parsedTime.Minute, parsedTime.Second, TimeSpan.Zero);
                        return result.ToString("s");
                    }
                }
                else if (InjuryYear != null && InjuryMonth != null && InjuryDay != null)
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
                    InjuryYear = (uint?)parsedTime.Year;
                    InjuryMonth = (uint?)parsedTime.Month;
                    InjuryDay = (uint?)parsedTime.Day;
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
                if (InjuryIncidentObs?.Value != null)
                {
                    return Convert.ToString(InjuryIncidentObs.Value);
                }
                return null;
            }
            set
            {
                if (String.IsNullOrWhiteSpace(value) && InjuryIncidentObs == null){
                    return;
                }
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
                return "";
            }
            set
            {
                if (String.IsNullOrWhiteSpace(value) && InjuryIncidentObs == null){
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
                if (IsDictEmptyOrDefault(value) && InjuryIncidentObs == null){
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
                if (IsDictEmptyOrDefault(value) && InjuryIncidentObs == null){
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
        [Property("Transportation Role Helper", Property.Types.String, "Death Investigation", "Transportation Role in death.", false, "http://build.fhir.org/ig/HL7/vrdr/StructureDefinition-VRDR-Decedent-Transportation-Role.html", true, 45)]
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
                    else
                    {
                        return code;
                    }
                }
                return null;
            }
            set
            {
                if (String.IsNullOrWhiteSpace(value)){
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
            if (value == null && EmergingIssues == null){
                return;
            }
            if (EmergingIssues == null )
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
                SetEmergingIssue("EmergingIssue20", value);
            }
        }

        /// <summary>Activity at Time of Death.</summary>
        /// <value>the decedent's activity at time of death. A Dictionary representing a code, containing the following key/value pairs:
        /// <para>"code" - the code</para>
        /// <para>"system" - the code system this code belongs to</para>
        /// <para>"display" - a human readable meaning of the code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; activity = new Dictionary&lt;string, string&gt;();</para>
        /// <para>elevel.Add("code", "0");</para>
        /// <para>elevel.Add("system", CodeSystems.ActivityAtTimeOfDeath);</para>
        /// <para>elevel.Add("display", "While engaged in sports activity");</para>
        /// <para>ExampleDeathRecord.ActivityAtDeath = activity;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Education Level: {ExampleDeathRecord.EducationLevel['display']}");</para>
        /// </example>
        [Property("Activity at Time of Death", Property.Types.Dictionary, "Coded Content", "Decedent's Activity at Time of Death.", true, IGURL.ActivityAtTimeOfDeath, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='80626-5')", "")]
        public Dictionary<string, string> ActivityAtDeath
        {
            get
            {
                if (ActivityAtTimeOfDeathObs != null && ActivityAtTimeOfDeathObs.Value != null && ActivityAtTimeOfDeathObs.Value as CodeableConcept != null)
                {
                    return CodeableConceptToDict((CodeableConcept)ActivityAtTimeOfDeathObs.Value);
                }
                return EmptyCodeableDict();
            }
            set
            {
                if (ActivityAtTimeOfDeathObs == null)
                {
                    CreateActivityAtTimeOfDeathObs();
                }
                ActivityAtTimeOfDeathObs.Value = DictToCodeableConcept(value);
            }
        }

        /// <summary>Decedent's Activity At Time of Death Helper</summary>
        /// <value>Decedent's Activity at Time of Death.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.ActivityAtDeath = 0;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Decedent's Activity at Time of Death: {ExampleDeathRecord.ActivityAtDeath}");</para>
        /// </example>
        [Property("Activity at Time of Death Helper", Property.Types.String, "Coded Content", "Decedent's Activity at Time of Death.", false, IGURL.ActivityAtTimeOfDeath, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='80626-5')", "")]
        public string ActivityAtDeathHelper
        {
            get
            {
                if (ActivityAtDeath.ContainsKey("code"))
                {
                    return ActivityAtDeath["code"];
                }
                return null;
            }
            set
            {
                SetCodeValue("ActivityAtDeath", value, VRDR.ValueSets.ActivityAtTimeOfDeath.Codes);
            }
        }

        /// <summary>Decedent's Automated Underlying Cause of Death</summary>
        /// <value>Decedent's Automated Underlying Cause of Death.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.AutomatedUnderlyingCOD = "I13.1";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Decedent's Automated Underlying Cause of Death: {ExampleDeathRecord.AutomatedUnderlyingCOD}");</para>
        /// </example>
        [Property("Automated Underlying Cause of Death", Property.Types.String, "Coded Content", "Automated Underlying Cause of Death.", true, IGURL.AutomatedUnderlyingCauseOfDeath, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='80358-5')", "")]
        public string AutomatedUnderlyingCOD
        {
            get
            {
                if (AutomatedUnderlyingCauseOfDeathObs != null && AutomatedUnderlyingCauseOfDeathObs.Value != null && AutomatedUnderlyingCauseOfDeathObs.Value as CodeableConcept != null)
                {

                    return CodeableConceptToDict((CodeableConcept)AutomatedUnderlyingCauseOfDeathObs.Value)["code"];
                }
                return "";
            }
            set
            {
                if (AutomatedUnderlyingCauseOfDeathObs == null)
                {
                    CreateAutomatedUnderlyingCauseOfDeathObs();
                }
                AutomatedUnderlyingCauseOfDeathObs.Value = new CodeableConcept(CodeSystems.ICD10, value, null, null);
            }
        }

        /// <summary>Decedent's Manual Underlying Cause of Death</summary>
        /// <value>Decedent's Manual Underlying Cause of Death.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.ManUnderlyingCOD = "I13.1";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Decedent's Manual Underlying Cause of Death: {ExampleDeathRecord.ManUnderlyingCOD}");</para>
        /// </example>
        [Property("Manual Underlying Cause of Death", Property.Types.String, "Coded Content", "Manual Underlying Cause of Death.", true, IGURL.ManualUnderlyingCauseOfDeath, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='80358-580359-3')", "")]
        public string ManUnderlyingCOD
        {
            get
            {
                if (ManualUnderlyingCauseOfDeathObs != null && ManualUnderlyingCauseOfDeathObs.Value != null && ManualUnderlyingCauseOfDeathObs.Value as CodeableConcept != null)
                {

                    return CodeableConceptToDict((CodeableConcept)ManualUnderlyingCauseOfDeathObs.Value)["code"];
                }
                return "";
            }
            set
            {
                if (ManualUnderlyingCauseOfDeathObs == null)
                {
                    CreateManualUnderlyingCauseOfDeathObs();
                }
                ManualUnderlyingCauseOfDeathObs.Value = new CodeableConcept(CodeSystems.ICD10, value, null, null);
            }
        }

        /// <summary>Place of Injury.</summary>
        /// <value>Place of Injury. A Dictionary representing a code, containing the following key/value pairs:
        /// <para>"code" - the code</para>
        /// <para>"system" - the code system this code belongs to</para>
        /// <para>"display" - a human readable meaning of the code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; elevel = new Dictionary&lt;string, string&gt;();</para>
        /// <para>elevel.Add("code", "LA14084-0");</para>
        /// <para>elevel.Add("system", CodeSystems.LOINC);</para>
        /// <para>elevel.Add("display", "Home");</para>
        /// <para>ExampleDeathRecord.PlaceOfInjury = elevel;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"PlaceOfInjury: {ExampleDeathRecord.PlaceOfInjury['display']}");</para>
        /// </example>
        [Property("Place of Injury", Property.Types.Dictionary, "Coded Content", "Place of Injury.", true, IGURL.PlaceOfInjury, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='11376-1')", "")]
        public Dictionary<string, string> PlaceOfInjury
        {
            get
            {
                if (PlaceOfInjuryObs != null && PlaceOfInjuryObs.Value != null && PlaceOfInjuryObs.Value as CodeableConcept != null)
                {
                    return CodeableConceptToDict((CodeableConcept)PlaceOfInjuryObs.Value);
                }
                return EmptyCodeableDict();
            }
            set
            {
                if (PlaceOfInjuryObs == null)
                {
                    CreatePlaceOfInjuryObs();
                }
                PlaceOfInjuryObs.Value = DictToCodeableConcept(value);
            }
        }

        /// <summary>Decedent's Place of Injury Helper</summary>
        /// <value>Place of Injury.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.PlaceOfInjuryHelper = ValueSets.PlaceOfInjury.Home;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Place of Injury: {ExampleDeathRecord.PlaceOfInjuryHelper}");</para>
        /// </example>
        [Property("Place of Injury Helper", Property.Types.String, "Coded Content", "Place of Injury.", false, IGURL.PlaceOfInjury, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='11376-1')", "")]
        public string PlaceOfInjuryHelper
        {
            get
            {
                if (PlaceOfInjury.ContainsKey("code"))
                {
                    return PlaceOfInjury["code"];
                }
                return null;
            }
            set
            {
                SetCodeValue("PlaceOfInjury", value, VRDR.ValueSets.PlaceOfInjury.Codes);
            }
        }

        /// <summary>First Edited Race Code.</summary>
        /// <value>First Edited Race Code. A Dictionary representing a code, containing the following key/value pairs:
        /// <para>"code" - the code</para>
        /// <para>"system" - the code system this code belongs to</para>
        /// <para>"display" - a human readable meaning of the code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; racecode = new Dictionary&lt;string, string&gt;();</para>
        /// <para>racecode.Add("code", "300");</para>
        /// <para>racecode.Add("system", CodeSystems.RaceCode);</para>
        /// <para>racecode.Add("display", "African");</para>
        /// <para>ExampleDeathRecord.FirstEditedRaceCode = racecode;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"First Edited Race Code: {ExampleDeathRecord.FirstEditedRaceCode['display']}");</para>
        /// </example>
        [Property("FirstEditedRaceCode", Property.Types.Dictionary, "Coded Content", "First Edited Race Code.", true, IGURL.CodedRaceAndEthnicity, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='codedraceandethnicity')", "")]
        public Dictionary<string, string> FirstEditedRaceCode
        {
            get
            {
                if (CodedRaceAndEthnicityObs != null)
                {
                    Observation.ComponentComponent racecode = CodedRaceAndEthnicityObs.Component.FirstOrDefault(c => c.Code.Coding[0].Code == "FirstEditedCode");
                    if (racecode != null && racecode.Value != null && racecode.Value as CodeableConcept != null)
                    {
                        return CodeableConceptToDict((CodeableConcept)racecode.Value);
                    }
                }
                return EmptyCodeableDict();
            }
            set
            {
                if (CodedRaceAndEthnicityObs == null)
                {
                    CreateCodedRaceAndEthnicityObs();
                }
                CodedRaceAndEthnicityObs.Component.RemoveAll(c => c.Code.Coding[0].Code == "FirstEditedCode");
                Observation.ComponentComponent component = new Observation.ComponentComponent();
                component.Code = new CodeableConcept(CodeSystems.ComponentCode, "FirstEditedCode", "First Edited Race", null);
                component.Value = DictToCodeableConcept(value);
                CodedRaceAndEthnicityObs.Component.Add(component);
            }
        }

        /// <summary>First Edited Race Code  Helper</summary>
        /// <value>First Edited Race Code Helper.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.FirstEditedRaceCodeHelper = VRDR.ValueSets.RaceCode.African ;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"First Edited Race Code: {ExampleDeathRecord.FirstEditedRaceCodeHelper}");</para>
        /// </example>
        [Property("First Edited Race Code Helper", Property.Types.String, "Coded Content", "First Edited Race Code.", false, IGURL.CodedRaceAndEthnicity, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='codedraceandethnicity')", "")]
        public string FirstEditedRaceCodeHelper
        {
            get
            {
                if (FirstEditedRaceCode.ContainsKey("code"))
                {
                    return FirstEditedRaceCode["code"];
                }
                return null;
            }
            set
            {
                SetCodeValue("FirstEditedRaceCode", value, VRDR.ValueSets.RaceCode.Codes);
            }
        }

        /// <summary>Second Edited Race Code.</summary>
        /// <value>Second Edited Race Code. A Dictionary representing a code, containing the following key/value pairs:
        /// <para>"code" - the code</para>
        /// <para>"system" - the code system this code belongs to</para>
        /// <para>"display" - a human readable meaning of the code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; racecode = new Dictionary&lt;string, string&gt;();</para>
        /// <para>racecode.Add("code", "300");</para>
        /// <para>racecode.Add("system", CodeSystems.RaceCode);</para>
        /// <para>racecode.Add("display", "African");</para>
        /// <para>ExampleDeathRecord.SecondEditedRaceCode = racecode;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Second Edited Race Code: {ExampleDeathRecord.SecondEditedRaceCode['display']}");</para>
        /// </example>
        [Property("SecondEditedRaceCode", Property.Types.Dictionary, "Coded Content", "Second Edited Race Code.", true, IGURL.CodedRaceAndEthnicity, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='codedraceandethnicity')", "")]
        public Dictionary<string, string> SecondEditedRaceCode
        {
            get
            {
                if (CodedRaceAndEthnicityObs != null)
                {
                    Observation.ComponentComponent racecode = CodedRaceAndEthnicityObs.Component.FirstOrDefault(c => c.Code.Coding[0].Code == "SecondEditedCode");
                    if (racecode != null && racecode.Value != null && racecode.Value as CodeableConcept != null)
                    {
                        return CodeableConceptToDict((CodeableConcept)racecode.Value);
                    }
                }
                return EmptyCodeableDict();
            }
            set
            {
                if (CodedRaceAndEthnicityObs == null)
                {
                    CreateCodedRaceAndEthnicityObs();
                }
                CodedRaceAndEthnicityObs.Component.RemoveAll(c => c.Code.Coding[0].Code == "SecondEditedCode");
                Observation.ComponentComponent component = new Observation.ComponentComponent();
                component.Code = new CodeableConcept(CodeSystems.ComponentCode, "SecondEditedCode", "Second Edited Race", null);
                component.Value = DictToCodeableConcept(value);
                CodedRaceAndEthnicityObs.Component.Add(component);
            }
        }

        /// <summary>Second Edited Race Code  Helper</summary>
        /// <value>Second Edited Race Code Helper.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.SecondEditedRaceCodeHelper = VRDR.ValueSets.RaceCode.African ;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Second Edited Race Code: {ExampleDeathRecord.SecondEditedRaceCodeHelper}");</para>
        /// </example>
        [Property("Second Edited Race Code Helper", Property.Types.String, "Coded Content", "Second Edited Race Code.", false, IGURL.CodedRaceAndEthnicity, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='codedraceandethnicity')", "")]
        public string SecondEditedRaceCodeHelper
        {
            get
            {
                if (SecondEditedRaceCode.ContainsKey("code"))
                {
                    return SecondEditedRaceCode["code"];
                }
                return null;
            }
            set
            {
                SetCodeValue("SecondEditedRaceCode", value, VRDR.ValueSets.RaceCode.Codes);
            }
        }

        /// <summary>Third Edited Race Code.</summary>
        /// <value>Third Edited Race Code. A Dictionary representing a code, containing the following key/value pairs:
        /// <para>"code" - the code</para>
        /// <para>"system" - the code system this code belongs to</para>
        /// <para>"display" - a human readable meaning of the code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; racecode = new Dictionary&lt;string, string&gt;();</para>
        /// <para>racecode.Add("code", "300");</para>
        /// <para>racecode.Add("system", CodeSystems.RaceCode);</para>
        /// <para>racecode.Add("display", "African");</para>
        /// <para>ExampleDeathRecord.ThirdEditedRaceCode = racecode;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Third Edited Race Code: {ExampleDeathRecord.ThirdEditedRaceCode['display']}");</para>
        /// </example>
        [Property("ThirdEditedRaceCode", Property.Types.Dictionary, "Coded Content", "Third Edited Race Code.", true, IGURL.CodedRaceAndEthnicity, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='codedraceandethnicity')", "")]
        public Dictionary<string, string> ThirdEditedRaceCode
        {
            get
            {
                if (CodedRaceAndEthnicityObs != null)
                {
                    Observation.ComponentComponent racecode = CodedRaceAndEthnicityObs.Component.FirstOrDefault(c => c.Code.Coding[0].Code == "ThirdEditedCode");
                    if (racecode != null && racecode.Value != null && racecode.Value as CodeableConcept != null)
                    {
                        return CodeableConceptToDict((CodeableConcept)racecode.Value);
                    }
                }
                return EmptyCodeableDict();
            }
            set
            {
                if (CodedRaceAndEthnicityObs == null)
                {
                    CreateCodedRaceAndEthnicityObs();
                }
                CodedRaceAndEthnicityObs.Component.RemoveAll(c => c.Code.Coding[0].Code == "ThirdEditedCode");
                Observation.ComponentComponent component = new Observation.ComponentComponent();
                component.Code = new CodeableConcept(CodeSystems.ComponentCode, "ThirdEditedCode", "Third Edited Race", null);
                component.Value = DictToCodeableConcept(value);
                CodedRaceAndEthnicityObs.Component.Add(component);
            }
        }

        /// <summary>Third Edited Race Code  Helper</summary>
        /// <value>Third Edited Race Code Helper.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.ThirdEditedRaceCodeHelper = VRDR.ValueSets.RaceCode.African ;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Third Edited Race Code: {ExampleDeathRecord.ThirdEditedRaceCodeHelper}");</para>
        /// </example>
        [Property("Third Edited Race Code Helper", Property.Types.String, "Coded Content", "Third Edited Race Code.", false, IGURL.CodedRaceAndEthnicity, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='codedraceandethnicity')", "")]
        public string ThirdEditedRaceCodeHelper
        {
            get
            {
                if (ThirdEditedRaceCode.ContainsKey("code"))
                {
                    return ThirdEditedRaceCode["code"];
                }
                return null;
            }
            set
            {
                SetCodeValue("ThirdEditedRaceCode", value, VRDR.ValueSets.RaceCode.Codes);
            }
        }

        /// <summary>Fourth Edited Race Code.</summary>
        /// <value>Fourth Edited Race Code. A Dictionary representing a code, containing the following key/value pairs:
        /// <para>"code" - the code</para>
        /// <para>"system" - the code system this code belongs to</para>
        /// <para>"display" - a human readable meaning of the code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; racecode = new Dictionary&lt;string, string&gt;();</para>
        /// <para>racecode.Add("code", "300");</para>
        /// <para>racecode.Add("system", CodeSystems.RaceCode);</para>
        /// <para>racecode.Add("display", "African");</para>
        /// <para>ExampleDeathRecord.FourthEditedRaceCode = racecode;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Fourth Edited Race Code: {ExampleDeathRecord.FourthEditedRaceCode['display']}");</para>
        /// </example>
        [Property("FourthEditedRaceCode", Property.Types.Dictionary, "Coded Content", "Fourth Edited Race Code.", true, IGURL.CodedRaceAndEthnicity, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='codedraceandethnicity')", "")]
        public Dictionary<string, string> FourthEditedRaceCode
        {
            get
            {
                if (CodedRaceAndEthnicityObs != null)
                {
                    Observation.ComponentComponent racecode = CodedRaceAndEthnicityObs.Component.FirstOrDefault(c => c.Code.Coding[0].Code == "FourthEditedCode");
                    if (racecode != null && racecode.Value != null && racecode.Value as CodeableConcept != null)
                    {
                        return CodeableConceptToDict((CodeableConcept)racecode.Value);
                    }
                }
                return EmptyCodeableDict();
            }
            set
            {
                if (CodedRaceAndEthnicityObs == null)
                {
                    CreateCodedRaceAndEthnicityObs();
                }
                CodedRaceAndEthnicityObs.Component.RemoveAll(c => c.Code.Coding[0].Code == "FourthEditedCode");
                Observation.ComponentComponent component = new Observation.ComponentComponent();
                component.Code = new CodeableConcept(CodeSystems.ComponentCode, "FourthEditedCode", "Fourth Edited Race", null);
                component.Value = DictToCodeableConcept(value);
                CodedRaceAndEthnicityObs.Component.Add(component);
            }
        }

        /// <summary>Fourth Edited Race Code  Helper</summary>
        /// <value>Fourth Edited Race Code Helper.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.FourthEditedRaceCodeHelper = VRDR.ValueSets.RaceCode.African ;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Fourth Edited Race Code: {ExampleDeathRecord.FourthEditedRaceCodeHelper}");</para>
        /// </example>
        [Property("Fourth Edited Race Code Helper", Property.Types.String, "Coded Content", "Fourth Edited Race Code.", false, IGURL.CodedRaceAndEthnicity, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='codedraceandethnicity')", "")]
        public string FourthEditedRaceCodeHelper
        {
            get
            {
                if (FourthEditedRaceCode.ContainsKey("code"))
                {
                    return FourthEditedRaceCode["code"];
                }
                return null;
            }
            set
            {
                SetCodeValue("FourthEditedRaceCode", value, VRDR.ValueSets.RaceCode.Codes);
            }
        }

        /// <summary>Fifth Edited Race Code.</summary>
        /// <value>Fifth Edited Race Code. A Dictionary representing a code, containing the following key/value pairs:
        /// <para>"code" - the code</para>
        /// <para>"system" - the code system this code belongs to</para>
        /// <para>"display" - a human readable meaning of the code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; racecode = new Dictionary&lt;string, string&gt;();</para>
        /// <para>racecode.Add("code", "300");</para>
        /// <para>racecode.Add("system", CodeSystems.RaceCode);</para>
        /// <para>racecode.Add("display", "African");</para>
        /// <para>ExampleDeathRecord.FifthEditedRaceCode = racecode;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Fifth Edited Race Code: {ExampleDeathRecord.FifthEditedRaceCode['display']}");</para>
        /// </example>
        [Property("FifthEditedRaceCode", Property.Types.Dictionary, "Coded Content", "Fifth Edited Race Code.", true, IGURL.CodedRaceAndEthnicity, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='codedraceandethnicity')", "")]
        public Dictionary<string, string> FifthEditedRaceCode
        {
            get
            {
                if (CodedRaceAndEthnicityObs != null)
                {
                    Observation.ComponentComponent racecode = CodedRaceAndEthnicityObs.Component.FirstOrDefault(c => c.Code.Coding[0].Code == "FifthEditedCode");
                    if (racecode != null && racecode.Value != null && racecode.Value as CodeableConcept != null)
                    {
                        return CodeableConceptToDict((CodeableConcept)racecode.Value);
                    }
                }
                return EmptyCodeableDict();
            }
            set
            {
                if (CodedRaceAndEthnicityObs == null)
                {
                    CreateCodedRaceAndEthnicityObs();
                }
                CodedRaceAndEthnicityObs.Component.RemoveAll(c => c.Code.Coding[0].Code == "FifthEditedCode");
                Observation.ComponentComponent component = new Observation.ComponentComponent();
                component.Code = new CodeableConcept(CodeSystems.ComponentCode, "FifthEditedCode", "Fifth Edited Race", null);
                component.Value = DictToCodeableConcept(value);
                CodedRaceAndEthnicityObs.Component.Add(component);
            }
        }

        /// <summary>Fifth Edited Race Code  Helper</summary>
        /// <value>Fifth Edited Race Code Helper.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.FifthEditedRaceCodeHelper = VRDR.ValueSets.RaceCode.African ;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Fifth Edited Race Code: {ExampleDeathRecord.FifthEditedRaceCodeHelper}");</para>
        /// </example>
        [Property("Fifth Edited Race Code Helper", Property.Types.String, "Coded Content", "Fifth Edited Race Code.", false, IGURL.CodedRaceAndEthnicity, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='codedraceandethnicity')", "")]
        public string FifthEditedRaceCodeHelper
        {
            get
            {
                if (FifthEditedRaceCode.ContainsKey("code"))
                {
                    return FifthEditedRaceCode["code"];
                }
                return null;
            }
            set
            {
                SetCodeValue("FifthEditedRaceCode", value, VRDR.ValueSets.RaceCode.Codes);
            }
        }

        /// <summary>Sixth Edited Race Code.</summary>
        /// <value>Sixth Edited Race Code. A Dictionary representing a code, containing the following key/value pairs:
        /// <para>"code" - the code</para>
        /// <para>"system" - the code system this code belongs to</para>
        /// <para>"display" - a human readable meaning of the code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; racecode = new Dictionary&lt;string, string&gt;();</para>
        /// <para>racecode.Add("code", "300");</para>
        /// <para>racecode.Add("system", CodeSystems.RaceCode);</para>
        /// <para>racecode.Add("display", "African");</para>
        /// <para>ExampleDeathRecord.SixthEditedRaceCode = racecode;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Sixth Edited Race Code: {ExampleDeathRecord.SixthEditedRaceCode['display']}");</para>
        /// </example>
        [Property("SixthEditedRaceCode", Property.Types.Dictionary, "Coded Content", "Sixth Edited Race Code.", true, IGURL.CodedRaceAndEthnicity, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='codedraceandethnicity')", "")]
        public Dictionary<string, string> SixthEditedRaceCode
        {
            get
            {
                if (CodedRaceAndEthnicityObs != null)
                {
                    Observation.ComponentComponent racecode = CodedRaceAndEthnicityObs.Component.FirstOrDefault(c => c.Code.Coding[0].Code == "SixthEditedCode");
                    if (racecode != null && racecode.Value != null && racecode.Value as CodeableConcept != null)
                    {
                        return CodeableConceptToDict((CodeableConcept)racecode.Value);
                    }
                }
                return EmptyCodeableDict();
            }
            set
            {
                if (CodedRaceAndEthnicityObs == null)
                {
                    CreateCodedRaceAndEthnicityObs();
                }
                CodedRaceAndEthnicityObs.Component.RemoveAll(c => c.Code.Coding[0].Code == "SixthEditedCode");
                Observation.ComponentComponent component = new Observation.ComponentComponent();
                component.Code = new CodeableConcept(CodeSystems.ComponentCode, "SixthEditedCode", "Sixth Edited Race", null);
                component.Value = DictToCodeableConcept(value);
                CodedRaceAndEthnicityObs.Component.Add(component);
            }
        }

        /// <summary>Sixth Edited Race Code  Helper</summary>
        /// <value>Sixth Edited Race Code Helper.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.SixthEditedRaceCodeHelper = VRDR.ValueSets.RaceCode.African ;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Sixth Edited Race Code: {ExampleDeathRecord.SixthEditedRaceCodeHelper}");</para>
        /// </example>
        [Property("Sixth Edited Race Code Helper", Property.Types.String, "Coded Content", "Sixth Edited Race Code.", false, IGURL.CodedRaceAndEthnicity, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='codedraceandethnicity')", "")]
        public string SixthEditedRaceCodeHelper
        {
            get
            {
                if (SixthEditedRaceCode.ContainsKey("code"))
                {
                    return SixthEditedRaceCode["code"];
                }
                return null;
            }
            set
            {
                SetCodeValue("SixthEditedRaceCode", value, VRDR.ValueSets.RaceCode.Codes);
            }
        }

        /// <summary>Seventh Edited Race Code.</summary>
        /// <value>Seventh Edited Race Code. A Dictionary representing a code, containing the following key/value pairs:
        /// <para>"code" - the code</para>
        /// <para>"system" - the code system this code belongs to</para>
        /// <para>"display" - a human readable meaning of the code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; racecode = new Dictionary&lt;string, string&gt;();</para>
        /// <para>racecode.Add("code", "300");</para>
        /// <para>racecode.Add("system", CodeSystems.RaceCode);</para>
        /// <para>racecode.Add("display", "African");</para>
        /// <para>ExampleDeathRecord.SeventhEditedRaceCode = racecode;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Seventh Edited Race Code: {ExampleDeathRecord.SeventhEditedRaceCode['display']}");</para>
        /// </example>
        [Property("SeventhEditedRaceCode", Property.Types.Dictionary, "Coded Content", "Seventh Edited Race Code.", true, IGURL.CodedRaceAndEthnicity, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='codedraceandethnicity')", "")]
        public Dictionary<string, string> SeventhEditedRaceCode
        {
            get
            {
                if (CodedRaceAndEthnicityObs != null)
                {
                    Observation.ComponentComponent racecode = CodedRaceAndEthnicityObs.Component.FirstOrDefault(c => c.Code.Coding[0].Code == "SeventhEditedCode");
                    if (racecode != null && racecode.Value != null && racecode.Value as CodeableConcept != null)
                    {
                        return CodeableConceptToDict((CodeableConcept)racecode.Value);
                    }
                }
                return EmptyCodeableDict();
            }
            set
            {
                if (CodedRaceAndEthnicityObs == null)
                {
                    CreateCodedRaceAndEthnicityObs();
                }
                CodedRaceAndEthnicityObs.Component.RemoveAll(c => c.Code.Coding[0].Code == "SeventhEditedCode");
                Observation.ComponentComponent component = new Observation.ComponentComponent();
                component.Code = new CodeableConcept(CodeSystems.ComponentCode, "SeventhEditedCode", "Seventh Edited Race", null);
                component.Value = DictToCodeableConcept(value);
                CodedRaceAndEthnicityObs.Component.Add(component);
            }
        }

        /// <summary>Seventh Edited Race Code  Helper</summary>
        /// <value>Seventh Edited Race Code Helper.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.SeventhEditedRaceCodeHelper = VRDR.ValueSets.RaceCode.African ;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Seventh Edited Race Code: {ExampleDeathRecord.SeventhEditedRaceCodeHelper}");</para>
        /// </example>
        [Property("Seventh Edited Race Code Helper", Property.Types.String, "Coded Content", "Seventh Edited Race Code.", false, IGURL.CodedRaceAndEthnicity, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='codedraceandethnicity')", "")]
        public string SeventhEditedRaceCodeHelper
        {
            get
            {
                if (SeventhEditedRaceCode.ContainsKey("code"))
                {
                    return SeventhEditedRaceCode["code"];
                }
                return null;
            }
            set
            {
                SetCodeValue("SeventhEditedRaceCode", value, VRDR.ValueSets.RaceCode.Codes);
            }
        }

        /// <summary>Eighth Edited Race Code.</summary>
        /// <value>Eighth Edited Race Code. A Dictionary representing a code, containing the following key/value pairs:
        /// <para>"code" - the code</para>
        /// <para>"system" - the code system this code belongs to</para>
        /// <para>"display" - a human readable meaning of the code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; racecode = new Dictionary&lt;string, string&gt;();</para>
        /// <para>racecode.Add("code", "300");</para>
        /// <para>racecode.Add("system", CodeSystems.RaceCode);</para>
        /// <para>racecode.Add("display", "African");</para>
        /// <para>ExampleDeathRecord.EighthEditedRaceCode = racecode;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Eighth Edited Race Code: {ExampleDeathRecord.EighthEditedRaceCode['display']}");</para>
        /// </example>
        [Property("EighthEditedRaceCode", Property.Types.Dictionary, "Coded Content", "Eighth Edited Race Code.", true, IGURL.CodedRaceAndEthnicity, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='codedraceandethnicity')", "")]
        public Dictionary<string, string> EighthEditedRaceCode
        {
            get
            {
                if (CodedRaceAndEthnicityObs != null)
                {
                    Observation.ComponentComponent racecode = CodedRaceAndEthnicityObs.Component.FirstOrDefault(c => c.Code.Coding[0].Code == "EighthEditedCode");
                    if (racecode != null && racecode.Value != null && racecode.Value as CodeableConcept != null)
                    {
                        return CodeableConceptToDict((CodeableConcept)racecode.Value);
                    }
                }
                return EmptyCodeableDict();
            }
            set
            {
                if (CodedRaceAndEthnicityObs == null)
                {
                    CreateCodedRaceAndEthnicityObs();
                }
                CodedRaceAndEthnicityObs.Component.RemoveAll(c => c.Code.Coding[0].Code == "EighthEditedCode");
                Observation.ComponentComponent component = new Observation.ComponentComponent();
                component.Code = new CodeableConcept(CodeSystems.ComponentCode, "EighthEditedCode", "Eighth Edited Race", null);
                component.Value = DictToCodeableConcept(value);
                CodedRaceAndEthnicityObs.Component.Add(component);
            }
        }

        /// <summary>Eighth Edited Race Code  Helper</summary>
        /// <value>Eighth Edited Race Code Helper.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.EighthEditedRaceCodeHelper = VRDR.ValueSets.RaceCode.African ;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Eighth Edited Race Code: {ExampleDeathRecord.EighthEditedRaceCodeHelper}");</para>
        /// </example>
        [Property("Eighth Edited Race Code Helper", Property.Types.String, "Coded Content", "Eighth Edited Race Code.", false, IGURL.CodedRaceAndEthnicity, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='codedraceandethnicity')", "")]
        public string EighthEditedRaceCodeHelper
        {
            get
            {
                if (EighthEditedRaceCode.ContainsKey("code"))
                {
                    return EighthEditedRaceCode["code"];
                }
                return null;
            }
            set
            {
                SetCodeValue("EighthEditedRaceCode", value, VRDR.ValueSets.RaceCode.Codes);
            }
        }

        /// <summary>First American Indian Race Code.</summary>
        /// <value>First American Indian Race Code. A Dictionary representing a code, containing the following key/value pairs:
        /// <para>"code" - the code</para>
        /// <para>"system" - the code system this code belongs to</para>
        /// <para>"display" - a human readable meaning of the code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; racecode = new Dictionary&lt;string, string&gt;();</para>
        /// <para>racecode.Add("code", "300");</para>
        /// <para>racecode.Add("system", CodeSystems.RaceCode);</para>
        /// <para>racecode.Add("display", "African");</para>
        /// <para>ExampleDeathRecord.FirstAmericanIndianRaceCode = racecode;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"First American Indian Race Code: {ExampleDeathRecord.FirstAmericanIndianRaceCode['display']}");</para>
        /// </example>
        [Property("FirstAmericanIndianRaceCode", Property.Types.Dictionary, "Coded Content", "First American Indian Race Code.", true, IGURL.CodedRaceAndEthnicity, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='codedraceandethnicity')", "")]
        public Dictionary<string, string> FirstAmericanIndianRaceCode
        {
            get
            {
                if (CodedRaceAndEthnicityObs != null)
                {
                    Observation.ComponentComponent racecode = CodedRaceAndEthnicityObs.Component.FirstOrDefault(c => c.Code.Coding[0].Code == "FirstAmericanIndianRace");
                    if (racecode != null && racecode.Value != null && racecode.Value as CodeableConcept != null)
                    {
                        return CodeableConceptToDict((CodeableConcept)racecode.Value);
                    }
                }
                return EmptyCodeableDict();
            }
            set
            {
                if (CodedRaceAndEthnicityObs == null)
                {
                    CreateCodedRaceAndEthnicityObs();
                }
                CodedRaceAndEthnicityObs.Component.RemoveAll(c => c.Code.Coding[0].Code == "FirstAmericanIndianRace");
                Observation.ComponentComponent component = new Observation.ComponentComponent();
                component.Code = new CodeableConcept(CodeSystems.ComponentCode, "FirstAmericanIndianRace", "First American Indian Race", null);
                component.Value = DictToCodeableConcept(value);
                CodedRaceAndEthnicityObs.Component.Add(component);
            }
        }

        /// <summary>First American Indian Race Code  Helper</summary>
        /// <value>First American Indian Race Code Helper.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.FirstAmericanIndianRaceCodeHelper = VRDR.ValueSets.RaceCode.African ;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"First American Indian Race Code: {ExampleDeathRecord.FirstAmericanIndianRaceCodeHelper}");</para>
        /// </example>
        [Property("First American Indian Race Code Helper", Property.Types.String, "Coded Content", "First American Indian Race Code.", false, IGURL.CodedRaceAndEthnicity, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='codedraceandethnicity')", "")]
        public string FirstAmericanIndianRaceCodeHelper
        {
            get
            {
                if (FirstAmericanIndianRaceCode.ContainsKey("code"))
                {
                    return FirstAmericanIndianRaceCode["code"];
                }
                return null;
            }
            set
            {
                SetCodeValue("FirstAmericanIndianRaceCode", value, VRDR.ValueSets.RaceCode.Codes);
            }
        }

        /// <summary>Second American Indian Race Code.</summary>
        /// <value>Second American Indian Race Code. A Dictionary representing a code, containing the following key/value pairs:
        /// <para>"code" - the code</para>
        /// <para>"system" - the code system this code belongs to</para>
        /// <para>"display" - a human readable meaning of the code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; racecode = new Dictionary&lt;string, string&gt;();</para>
        /// <para>racecode.Add("code", "300");</para>
        /// <para>racecode.Add("system", CodeSystems.RaceCode);</para>
        /// <para>racecode.Add("display", "African");</para>
        /// <para>ExampleDeathRecord.SecondAmericanIndianRaceCode = racecode;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Second American Indian Race Code: {ExampleDeathRecord.SecondAmericanIndianRaceCode['display']}");</para>
        /// </example>
        [Property("SecondAmericanIndianRaceCode", Property.Types.Dictionary, "Coded Content", "Second American Indian Race Code.", true, IGURL.CodedRaceAndEthnicity, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='codedraceandethnicity')", "")]
        public Dictionary<string, string> SecondAmericanIndianRaceCode
        {
            get
            {
                if (CodedRaceAndEthnicityObs != null)
                {
                    Observation.ComponentComponent racecode = CodedRaceAndEthnicityObs.Component.FirstOrDefault(c => c.Code.Coding[0].Code == "SecondAmericanIndianRace");
                    if (racecode != null && racecode.Value != null && racecode.Value as CodeableConcept != null)
                    {
                        return CodeableConceptToDict((CodeableConcept)racecode.Value);
                    }
                }
                return EmptyCodeableDict();
            }
            set
            {
                if (CodedRaceAndEthnicityObs == null)
                {
                    CreateCodedRaceAndEthnicityObs();
                }
                CodedRaceAndEthnicityObs.Component.RemoveAll(c => c.Code.Coding[0].Code == "SecondAmericanIndianRace");
                Observation.ComponentComponent component = new Observation.ComponentComponent();
                component.Code = new CodeableConcept(CodeSystems.ComponentCode, "SecondAmericanIndianRace", "Second American Indian Race", null);
                component.Value = DictToCodeableConcept(value);
                CodedRaceAndEthnicityObs.Component.Add(component);
            }
        }

        /// <summary>Second American Indian Race Code  Helper</summary>
        /// <value>Second American Indian Race Code Helper.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.SecondAmericanIndianRaceCodeHelper = VRDR.ValueSets.RaceCode.African ;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Second American Indian Race Code: {ExampleDeathRecord.SecondAmericanIndianRaceCodeHelper}");</para>
        /// </example>
        [Property("Second American Indian Race Code Helper", Property.Types.String, "Coded Content", "Second American Indian Race Code.", false, IGURL.CodedRaceAndEthnicity, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='codedraceandethnicity')", "")]
        public string SecondAmericanIndianRaceCodeHelper
        {
            get
            {
                if (SecondAmericanIndianRaceCode.ContainsKey("code"))
                {
                    return SecondAmericanIndianRaceCode["code"];
                }
                return null;
            }
            set
            {
                SetCodeValue("SecondAmericanIndianRaceCode", value, VRDR.ValueSets.RaceCode.Codes);
            }
        }

        /// <summary>First Other Asian Race Code.</summary>
        /// <value>First Other Asian Race Code. A Dictionary representing a code, containing the following key/value pairs:
        /// <para>"code" - the code</para>
        /// <para>"system" - the code system this code belongs to</para>
        /// <para>"display" - a human readable meaning of the code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; racecode = new Dictionary&lt;string, string&gt;();</para>
        /// <para>racecode.Add("code", "300");</para>
        /// <para>racecode.Add("system", CodeSystems.RaceCode);</para>
        /// <para>racecode.Add("display", "African");</para>
        /// <para>ExampleDeathRecord.FirstOtherAsianRaceCode = racecode;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"First Other Asian Race Code: {ExampleDeathRecord.FirstOtherAsianRaceCode['display']}");</para>
        /// </example>
        [Property("FirstOtherAsianRaceCode", Property.Types.Dictionary, "Coded Content", "First Other Asian Race Code.", true, IGURL.CodedRaceAndEthnicity, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='codedraceandethnicity')", "")]
        public Dictionary<string, string> FirstOtherAsianRaceCode
        {
            get
            {
                if (CodedRaceAndEthnicityObs != null)
                {
                    Observation.ComponentComponent racecode = CodedRaceAndEthnicityObs.Component.FirstOrDefault(c => c.Code.Coding[0].Code == "FirstOtherAsianRace");
                    if (racecode != null && racecode.Value != null && racecode.Value as CodeableConcept != null)
                    {
                        return CodeableConceptToDict((CodeableConcept)racecode.Value);
                    }
                }
                return EmptyCodeableDict();
            }
            set
            {
                if (CodedRaceAndEthnicityObs == null)
                {
                    CreateCodedRaceAndEthnicityObs();
                }
                CodedRaceAndEthnicityObs.Component.RemoveAll(c => c.Code.Coding[0].Code == "FirstOtherAsianRace");
                Observation.ComponentComponent component = new Observation.ComponentComponent();
                component.Code = new CodeableConcept(CodeSystems.ComponentCode, "FirstOtherAsianRace", "First Other Asian Race", null);
                component.Value = DictToCodeableConcept(value);
                CodedRaceAndEthnicityObs.Component.Add(component);
            }
        }

        /// <summary>First Other Asian Race Code  Helper</summary>
        /// <value>First Other Asian Race Code Helper.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.FirstOtherAsianRaceCodeHelper = VRDR.ValueSets.RaceCode.African ;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"First Other Asian Race Code: {ExampleDeathRecord.FirstOtherAsianRaceCodeHelper}");</para>
        /// </example>
        [Property("First Other Asian Race Code Helper", Property.Types.String, "Coded Content", "First Other Asian Race Code.", false, IGURL.CodedRaceAndEthnicity, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='codedraceandethnicity')", "")]
        public string FirstOtherAsianRaceCodeHelper
        {
            get
            {
                if (FirstOtherAsianRaceCode.ContainsKey("code"))
                {
                    return FirstOtherAsianRaceCode["code"];
                }
                return null;
            }
            set
            {
                SetCodeValue("FirstOtherAsianRaceCode", value, VRDR.ValueSets.RaceCode.Codes);
            }
        }

        /// <summary>Second Other Asian Race Code.</summary>
        /// <value>Second Other Asian Race Code. A Dictionary representing a code, containing the following key/value pairs:
        /// <para>"code" - the code</para>
        /// <para>"system" - the code system this code belongs to</para>
        /// <para>"display" - a human readable meaning of the code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; racecode = new Dictionary&lt;string, string&gt;();</para>
        /// <para>racecode.Add("code", "300");</para>
        /// <para>racecode.Add("system", CodeSystems.RaceCode);</para>
        /// <para>racecode.Add("display", "African");</para>
        /// <para>ExampleDeathRecord.SecondOtherAsianRaceCode = racecode;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Second Other Asian Race Code: {ExampleDeathRecord.SecondOtherAsianRaceCode['display']}");</para>
        /// </example>
        [Property("SecondOtherAsianRaceCode", Property.Types.Dictionary, "Coded Content", "Second Other Asian Race Code.", true, IGURL.CodedRaceAndEthnicity, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='codedraceandethnicity')", "")]
        public Dictionary<string, string> SecondOtherAsianRaceCode
        {
            get
            {
                if (CodedRaceAndEthnicityObs != null)
                {
                    Observation.ComponentComponent racecode = CodedRaceAndEthnicityObs.Component.FirstOrDefault(c => c.Code.Coding[0].Code == "SecondOtherAsianRace");
                    if (racecode != null && racecode.Value != null && racecode.Value as CodeableConcept != null)
                    {
                        return CodeableConceptToDict((CodeableConcept)racecode.Value);
                    }
                }
                return EmptyCodeableDict();
            }
            set
            {
                if (CodedRaceAndEthnicityObs == null)
                {
                    CreateCodedRaceAndEthnicityObs();
                }
                CodedRaceAndEthnicityObs.Component.RemoveAll(c => c.Code.Coding[0].Code == "SecondOtherAsianRace");
                Observation.ComponentComponent component = new Observation.ComponentComponent();
                component.Code = new CodeableConcept(CodeSystems.ComponentCode, "SecondOtherAsianRace", "Second Other Asian Race", null);
                component.Value = DictToCodeableConcept(value);
                CodedRaceAndEthnicityObs.Component.Add(component);
            }
        }

        /// <summary>Second Other Asian Race Code  Helper</summary>
        /// <value>Second Other Asian Race Code Helper.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.SecondOtherAsianRaceCodeHelper = VRDR.ValueSets.RaceCode.African ;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Second Other Asian Race Code: {ExampleDeathRecord.SecondOtherAsianRaceCodeHelper}");</para>
        /// </example>
        [Property("Second Other Asian Race Code Helper", Property.Types.String, "Coded Content", "Second Other Asian Race Code.", false, IGURL.CodedRaceAndEthnicity, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='codedraceandethnicity')", "")]
        public string SecondOtherAsianRaceCodeHelper
        {
            get
            {
                if (SecondOtherAsianRaceCode.ContainsKey("code"))
                {
                    return SecondOtherAsianRaceCode["code"];
                }
                return null;
            }
            set
            {
                SetCodeValue("SecondOtherAsianRaceCode", value, VRDR.ValueSets.RaceCode.Codes);
            }
        }

        /// <summary>First Other Pacific Islander Race Code.</summary>
        /// <value>First Other Pacific Islander Race Code. A Dictionary representing a code, containing the following key/value pairs:
        /// <para>"code" - the code</para>
        /// <para>"system" - the code system this code belongs to</para>
        /// <para>"display" - a human readable meaning of the code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; racecode = new Dictionary&lt;string, string&gt;();</para>
        /// <para>racecode.Add("code", "300");</para>
        /// <para>racecode.Add("system", CodeSystems.RaceCode);</para>
        /// <para>racecode.Add("display", "African");</para>
        /// <para>ExampleDeathRecord.FirstOtherPacificIslanderRaceCode = racecode;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"First Other Pacific Islander Race Code: {ExampleDeathRecord.FirstOtherPacificIslanderRaceCode['display']}");</para>
        /// </example>
        [Property("FirstOtherPacificIslanderRaceCode", Property.Types.Dictionary, "Coded Content", "First Other Pacific Islander Race Code.", true, IGURL.CodedRaceAndEthnicity, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='codedraceandethnicity')", "")]
        public Dictionary<string, string> FirstOtherPacificIslanderRaceCode
        {
            get
            {
                if (CodedRaceAndEthnicityObs != null)
                {
                    Observation.ComponentComponent racecode = CodedRaceAndEthnicityObs.Component.FirstOrDefault(c => c.Code.Coding[0].Code == "FirstOtherPacificIslanderRace");
                    if (racecode != null && racecode.Value != null && racecode.Value as CodeableConcept != null)
                    {
                        return CodeableConceptToDict((CodeableConcept)racecode.Value);
                    }
                }
                return EmptyCodeableDict();
            }
            set
            {
                if (CodedRaceAndEthnicityObs == null)
                {
                    CreateCodedRaceAndEthnicityObs();
                }
                CodedRaceAndEthnicityObs.Component.RemoveAll(c => c.Code.Coding[0].Code == "FirstOtherPacificIslanderRace");
                Observation.ComponentComponent component = new Observation.ComponentComponent();
                component.Code = new CodeableConcept(CodeSystems.ComponentCode, "FirstOtherPacificIslanderRace", "First Other Pacific Islander Race", null);
                component.Value = DictToCodeableConcept(value);
                CodedRaceAndEthnicityObs.Component.Add(component);
            }
        }

        /// <summary>First Other Pacific Islander Race Code  Helper</summary>
        /// <value>First Other Pacific Islander Race Code Helper.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.FirstOtherPacificIslanderRaceCodeHelper = VRDR.ValueSets.RaceCode.African ;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"First Other Pacific Islander Race Code: {ExampleDeathRecord.FirstOtherPacificIslanderRaceCodeHelper}");</para>
        /// </example>
        [Property("First Other Pacific Islander Race Code Helper", Property.Types.String, "Coded Content", "First Other Pacific Islander Race Code.", false, IGURL.CodedRaceAndEthnicity, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='codedraceandethnicity')", "")]
        public string FirstOtherPacificIslanderRaceCodeHelper
        {
            get
            {
                if (FirstOtherPacificIslanderRaceCode.ContainsKey("code"))
                {
                    return FirstOtherPacificIslanderRaceCode["code"];
                }
                return null;
            }
            set
            {
                SetCodeValue("FirstOtherPacificIslanderRaceCode", value, VRDR.ValueSets.RaceCode.Codes);
            }
        }

        /// <summary>Second Other Pacific Islander Race Code.</summary>
        /// <value>Second Other Pacific Islander Race Code. A Dictionary representing a code, containing the following key/value pairs:
        /// <para>"code" - the code</para>
        /// <para>"system" - the code system this code belongs to</para>
        /// <para>"display" - a human readable meaning of the code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; racecode = new Dictionary&lt;string, string&gt;();</para>
        /// <para>racecode.Add("code", "300");</para>
        /// <para>racecode.Add("system", CodeSystems.RaceCode);</para>
        /// <para>racecode.Add("display", "African");</para>
        /// <para>ExampleDeathRecord.SecondOtherPacificIslanderRaceCode = racecode;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Second Other Pacific Islander Race Code: {ExampleDeathRecord.SecondOtherPacificIslanderRaceCode['display']}");</para>
        /// </example>
        [Property("SecondOtherPacificIslanderRaceCode", Property.Types.Dictionary, "Coded Content", "Second Other Pacific Islander Race Code.", true, IGURL.CodedRaceAndEthnicity, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='codedraceandethnicity')", "")]
        public Dictionary<string, string> SecondOtherPacificIslanderRaceCode
        {
            get
            {
                if (CodedRaceAndEthnicityObs != null)
                {
                    Observation.ComponentComponent racecode = CodedRaceAndEthnicityObs.Component.FirstOrDefault(c => c.Code.Coding[0].Code == "SecondOtherPacificIslanderRace");
                    if (racecode != null && racecode.Value != null && racecode.Value as CodeableConcept != null)
                    {
                        return CodeableConceptToDict((CodeableConcept)racecode.Value);
                    }
                }
                return EmptyCodeableDict();
            }
            set
            {
                if (CodedRaceAndEthnicityObs == null)
                {
                    CreateCodedRaceAndEthnicityObs();
                }
                CodedRaceAndEthnicityObs.Component.RemoveAll(c => c.Code.Coding[0].Code == "SecondOtherPacificIslanderRace");
                Observation.ComponentComponent component = new Observation.ComponentComponent();
                component.Code = new CodeableConcept(CodeSystems.ComponentCode, "SecondOtherPacificIslanderRace", "Second Other Pacific Islander Race", null);
                component.Value = DictToCodeableConcept(value);
                CodedRaceAndEthnicityObs.Component.Add(component);
            }
        }

        /// <summary>Second Other Pacific Islander Race Code  Helper</summary>
        /// <value>Second Other Pacific Islander Race Code Helper.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.SecondOtherPacificIslanderRaceCodeHelper = VRDR.ValueSets.RaceCode.African ;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Second Other Pacific Islander Race Code: {ExampleDeathRecord.SecondOtherPacificIslanderRaceCodeHelper}");</para>
        /// </example>
        [Property("Second Other Pacific Islander Race Code Helper", Property.Types.String, "Coded Content", "Second Other Pacific Islander Race Code.", false, IGURL.CodedRaceAndEthnicity, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='codedraceandethnicity')", "")]
        public string SecondOtherPacificIslanderRaceCodeHelper
        {
            get
            {
                if (SecondOtherPacificIslanderRaceCode.ContainsKey("code"))
                {
                    return SecondOtherPacificIslanderRaceCode["code"];
                }
                return null;
            }
            set
            {
                SetCodeValue("SecondOtherPacificIslanderRaceCode", value, VRDR.ValueSets.RaceCode.Codes);
            }
        }

        /// <summary>First Other Race Code.</summary>
        /// <value>First Other Race Code. A Dictionary representing a code, containing the following key/value pairs:
        /// <para>"code" - the code</para>
        /// <para>"system" - the code system this code belongs to</para>
        /// <para>"display" - a human readable meaning of the code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; racecode = new Dictionary&lt;string, string&gt;();</para>
        /// <para>racecode.Add("code", "300");</para>
        /// <para>racecode.Add("system", CodeSystems.RaceCode);</para>
        /// <para>racecode.Add("display", "African");</para>
        /// <para>ExampleDeathRecord.FirstOtherRaceCode = racecode;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"First Other Race Code: {ExampleDeathRecord.FirstOtherRaceCode['display']}");</para>
        /// </example>
        [Property("FirstOtherRaceCode", Property.Types.Dictionary, "Coded Content", "First Other Race Code.", true, IGURL.CodedRaceAndEthnicity, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='codedraceandethnicity')", "")]
        public Dictionary<string, string> FirstOtherRaceCode
        {
            get
            {
                if (CodedRaceAndEthnicityObs != null)
                {
                    Observation.ComponentComponent racecode = CodedRaceAndEthnicityObs.Component.FirstOrDefault(c => c.Code.Coding[0].Code == "FirstOtherRace");
                    if (racecode != null && racecode.Value != null && racecode.Value as CodeableConcept != null)
                    {
                        return CodeableConceptToDict((CodeableConcept)racecode.Value);
                    }
                }
                return EmptyCodeableDict();
            }
            set
            {
                if (CodedRaceAndEthnicityObs == null)
                {
                    CreateCodedRaceAndEthnicityObs();
                }
                CodedRaceAndEthnicityObs.Component.RemoveAll(c => c.Code.Coding[0].Code == "FirstOtherRace");
                Observation.ComponentComponent component = new Observation.ComponentComponent();
                component.Code = new CodeableConcept(CodeSystems.ComponentCode, "FirstOtherRace", "First Other Race", null);
                component.Value = DictToCodeableConcept(value);
                CodedRaceAndEthnicityObs.Component.Add(component);
            }
        }

        /// <summary>First Other Race Code  Helper</summary>
        /// <value>First Other Race Code Helper.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.FirstOtherRaceCodeHelper = VRDR.ValueSets.RaceCode.African ;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"First Other Race Code: {ExampleDeathRecord.FirstOtherRaceCodeHelper}");</para>
        /// </example>
        [Property("First Other Race Code Helper", Property.Types.String, "Coded Content", "First Other Race Code.", false, IGURL.CodedRaceAndEthnicity, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='codedraceandethnicity')", "")]
        public string FirstOtherRaceCodeHelper
        {
            get
            {
                if (FirstOtherRaceCode.ContainsKey("code"))
                {
                    return FirstOtherRaceCode["code"];
                }
                return null;
            }
            set
            {
                SetCodeValue("FirstOtherRaceCode", value, VRDR.ValueSets.RaceCode.Codes);
            }
        }

        /// <summary>Second Other Race Code.</summary>
        /// <value>Second Other Race Code. A Dictionary representing a code, containing the following key/value pairs:
        /// <para>"code" - the code</para>
        /// <para>"system" - the code system this code belongs to</para>
        /// <para>"display" - a human readable meaning of the code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; racecode = new Dictionary&lt;string, string&gt;();</para>
        /// <para>racecode.Add("code", "300");</para>
        /// <para>racecode.Add("system", CodeSystems.RaceCode);</para>
        /// <para>racecode.Add("display", "African");</para>
        /// <para>ExampleDeathRecord.SecondOtherRaceCode = racecode;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Second Other Race Code: {ExampleDeathRecord.SecondOtherRaceCode['display']}");</para>
        /// </example>
        [Property("SecondOtherRaceCode", Property.Types.Dictionary, "Coded Content", "Second Other Race Code.", true, IGURL.CodedRaceAndEthnicity, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='codedraceandethnicity')", "")]
        public Dictionary<string, string> SecondOtherRaceCode
        {
            get
            {
                if (CodedRaceAndEthnicityObs != null)
                {
                    Observation.ComponentComponent racecode = CodedRaceAndEthnicityObs.Component.FirstOrDefault(c => c.Code.Coding[0].Code == "SecondOtherRace");
                    if (racecode != null && racecode.Value != null && racecode.Value as CodeableConcept != null)
                    {
                        return CodeableConceptToDict((CodeableConcept)racecode.Value);
                    }
                }
                return EmptyCodeableDict();
            }
            set
            {
                if (CodedRaceAndEthnicityObs == null)
                {
                    CreateCodedRaceAndEthnicityObs();
                }
                CodedRaceAndEthnicityObs.Component.RemoveAll(c => c.Code.Coding[0].Code == "SecondOtherRace");
                Observation.ComponentComponent component = new Observation.ComponentComponent();
                component.Code = new CodeableConcept(CodeSystems.ComponentCode, "SecondOtherRace", "Second Other Race", null);
                component.Value = DictToCodeableConcept(value);
                CodedRaceAndEthnicityObs.Component.Add(component);
            }
        }

        /// <summary>Second Other Race Code  Helper</summary>
        /// <value>Second Other Race Code Helper.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.SecondOtherRaceCodeHelper = VRDR.ValueSets.RaceCode.African ;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Second Other Race Code: {ExampleDeathRecord.SecondOtherRaceCodeHelper}");</para>
        /// </example>
        [Property("Second Other Race Code Helper", Property.Types.String, "Coded Content", "Second Other Race Code.", false, IGURL.CodedRaceAndEthnicity, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='codedraceandethnicity')", "")]
        public string SecondOtherRaceCodeHelper
        {
            get
            {
                if (SecondOtherRaceCode.ContainsKey("code"))
                {
                    return SecondOtherRaceCode["code"];
                }
                return null;
            }
            set
            {
                SetCodeValue("SecondOtherRaceCode", value, VRDR.ValueSets.RaceCode.Codes);
            }
        }

        /// <summary>Hispanic Code.</summary>
        /// <value>Hispanic Code. A Dictionary representing a code, containing the following key/value pairs:
        /// <para>"code" - the code</para>
        /// <para>"system" - the code system this code belongs to</para>
        /// <para>"display" - a human readable meaning of the code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; racecode = new Dictionary&lt;string, string&gt;();</para>
        /// <para>racecode.Add("code", "300");</para>
        /// <para>racecode.Add("system", CodeSystems.RaceCode);</para>
        /// <para>racecode.Add("display", "African");</para>
        /// <para>ExampleDeathRecord.HispanicCode = racecode;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Hispanic Code: {ExampleDeathRecord.HispanicCode['display']}");</para>
        /// </example>
        [Property("HispanicCode", Property.Types.Dictionary, "Coded Content", "Hispanic Code.", true, IGURL.CodedRaceAndEthnicity, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='codedraceandethnicity')", "")]
        public Dictionary<string, string> HispanicCode
        {
            get
            {
                if (CodedRaceAndEthnicityObs != null)
                {
                    Observation.ComponentComponent racecode = CodedRaceAndEthnicityObs.Component.FirstOrDefault(c => c.Code.Coding[0].Code == "HispanicCode");
                    if (racecode != null && racecode.Value != null && racecode.Value as CodeableConcept != null)
                    {
                        return CodeableConceptToDict((CodeableConcept)racecode.Value);
                    }
                }
                return EmptyCodeableDict();
            }
            set
            {
                if (CodedRaceAndEthnicityObs == null)
                {
                    CreateCodedRaceAndEthnicityObs();
                }
                CodedRaceAndEthnicityObs.Component.RemoveAll(c => c.Code.Coding[0].Code == "HispanicCode");
                Observation.ComponentComponent component = new Observation.ComponentComponent();
                component.Code = new CodeableConcept(CodeSystems.ComponentCode, "HispanicCode", "Hispanic Code", null);
                component.Value = DictToCodeableConcept(value);
                CodedRaceAndEthnicityObs.Component.Add(component);
            }
        }

        /// <summary>Hispanic Code  Helper</summary>
        /// <value>Hispanic Code Helper.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.HispanicCodeHelper = VRDR.ValueSets.RaceCode.African ;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Hispanic Code: {ExampleDeathRecord.HispanicCodeHelper}");</para>
        /// </example>
        [Property("Hispanic Code Helper", Property.Types.String, "Coded Content", "Hispanic Code.", false, IGURL.CodedRaceAndEthnicity, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='codedraceandethnicity')", "")]
        public string HispanicCodeHelper
        {
            get
            {
                if (HispanicCode.ContainsKey("code"))
                {
                    return HispanicCode["code"];
                }
                return null;
            }
            set
            {
                SetCodeValue("HispanicCode", value, VRDR.ValueSets.HispanicOrigin.Codes);
            }
        }

        /// <summary>Hispanic Code For Literal.</summary>
        /// <value>Hispanic Code For Literal. A Dictionary representing a code, containing the following key/value pairs:
        /// <para>"code" - the code</para>
        /// <para>"system" - the code system this code belongs to</para>
        /// <para>"display" - a human readable meaning of the code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; racecode = new Dictionary&lt;string, string&gt;();</para>
        /// <para>racecode.Add("code", "300");</para>
        /// <para>racecode.Add("system", CodeSystems.RaceCode);</para>
        /// <para>racecode.Add("display", "African");</para>
        /// <para>ExampleDeathRecord.HispanicCodeForLiteral = racecode;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Hispanic Code For Literal: {ExampleDeathRecord.HispanicCodeForLiteral['display']}");</para>
        /// </example>
        [Property("HispanicCodeForLiteral", Property.Types.Dictionary, "Coded Content", "Hispanic Code For Literal.", true, IGURL.CodedRaceAndEthnicity, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='codedraceandethnicity')", "")]
        public Dictionary<string, string> HispanicCodeForLiteral
        {
            get
            {
                if (CodedRaceAndEthnicityObs != null)
                {
                    Observation.ComponentComponent racecode = CodedRaceAndEthnicityObs.Component.FirstOrDefault(c => c.Code.Coding[0].Code == "HispanicCodeForLiteral");
                    if (racecode != null && racecode.Value != null && racecode.Value as CodeableConcept != null)
                    {
                        return CodeableConceptToDict((CodeableConcept)racecode.Value);
                    }
                }
                return EmptyCodeableDict();
            }
            set
            {
                if (CodedRaceAndEthnicityObs == null)
                {
                    CreateCodedRaceAndEthnicityObs();
                }
                CodedRaceAndEthnicityObs.Component.RemoveAll(c => c.Code.Coding[0].Code == "HispanicCodeForLiteral");
                Observation.ComponentComponent component = new Observation.ComponentComponent();
                component.Code = new CodeableConcept(CodeSystems.ComponentCode, "HispanicCodeForLiteral", "Hispanic Code For Literal", null);
                component.Value = DictToCodeableConcept(value);
                CodedRaceAndEthnicityObs.Component.Add(component);
            }
        }

        /// <summary>Hispanic Code For Literal  Helper</summary>
        /// <value>Hispanic Code For Literal Helper.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.HispanicCodeForLiteralHelper = VRDR.ValueSets.RaceCode.African ;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Hispanic Code For Literal: {ExampleDeathRecord.HispanicCodeForLiteralHelper}");</para>
        /// </example>
        [Property("Hispanic Code For Literal Helper", Property.Types.String, "Coded Content", "Hispanic Code For Literal.", false, IGURL.CodedRaceAndEthnicity, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='codedraceandethnicity')", "")]
        public string HispanicCodeForLiteralHelper
        {
            get
            {
                if (HispanicCodeForLiteral.ContainsKey("code"))
                {
                    return HispanicCodeForLiteral["code"];
                }
                return null;
            }
            set
            {
                SetCodeValue("HispanicCodeForLiteral", value, VRDR.ValueSets.HispanicOrigin.Codes);
            }
        }

        /// <summary>Race Recode 40.</summary>
        /// <value>Race Recode 40. A Dictionary representing a code, containing the following key/value pairs:
        /// <para>"code" - the code</para>
        /// <para>"system" - the code system this code belongs to</para>
        /// <para>"display" - a human readable meaning of the code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; racecode = new Dictionary&lt;string, string&gt;();</para>
        /// <para>racecode.Add("code", "09");</para>
        /// <para>racecode.Add("system", CodeSystems.RaceRecode40CS);</para>
        /// <para>racecode.Add("display", "Vietnamiese");</para>
        /// <para>ExampleDeathRecord.RaceRecode40 = racecode;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"RaceRecode40: {ExampleDeathRecord.RaceRecode40['display']}");</para>
        /// </example>
        [Property("RaceRecode40", Property.Types.Dictionary, "Coded Content", "RaceRecode40.", true, IGURL.CodedRaceAndEthnicity, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='codedraceandethnicity')", "")]
        public Dictionary<string, string> RaceRecode40
        {
            get
            {
                if (CodedRaceAndEthnicityObs != null)
                {
                    Observation.ComponentComponent racecode = CodedRaceAndEthnicityObs.Component.FirstOrDefault(c => c.Code.Coding[0].Code == "RaceRecode40");
                    if (racecode != null && racecode.Value != null && racecode.Value as CodeableConcept != null)
                    {
                        return CodeableConceptToDict((CodeableConcept)racecode.Value);
                    }
                }
                return EmptyCodeableDict();
            }
            set
            {
                if (CodedRaceAndEthnicityObs == null)
                {
                    CreateCodedRaceAndEthnicityObs();
                }
                CodedRaceAndEthnicityObs.Component.RemoveAll(c => c.Code.Coding[0].Code == "RaceRecode40");
                Observation.ComponentComponent component = new Observation.ComponentComponent();
                component.Code = new CodeableConcept(CodeSystems.ComponentCode, "RaceRecode40", null, null);
                component.Value = DictToCodeableConcept(value);
                CodedRaceAndEthnicityObs.Component.Add(component);
            }
        }

        /// <summary>Race Recode 40  Helper</summary>
        /// <value>Race Recode 40 Helper.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.RaceRecode40Helper = VRDR.ValueSets.RaceRecode40.AIAN ;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Race Recode 40: {ExampleDeathRecord.RaceRecode40Helper}");</para>
        /// </example>
        [Property("Race Recode 40 Helper", Property.Types.String, "Coded Content", "Race Recode 40.", false, IGURL.CodedRaceAndEthnicity, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='codedraceandethnicity')", "")]
        public string RaceRecode40Helper
        {
            get
            {
                if (RaceRecode40.ContainsKey("code"))
                {
                    return RaceRecode40["code"];
                }
                return null;
            }
            set
            {
                SetCodeValue("RaceRecode40", value, VRDR.ValueSets.RaceRecode40.Codes);
            }
        }

        /// <summary>Entity Axis Cause Of Death
        /// <para>Note that record axis codes have an unusual and obscure handling of a Pregnancy flag, for more information see
        /// http://build.fhir.org/ig/HL7/vrdr/branches/master/StructureDefinition-vrdr-record-axis-cause-of-death.html#usage></para>
        /// </summary>
        /// <value>Entity-axis codes</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para> ExampleDeathRecord.EntityAxisCauseOfDeath = new [] {(LineNumber: 2, Position: 1, Code: "T27.3", ECode: true)};</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"First Entity Axis Code: {ExampleDeathRecord.EntityAxisCauseOfDeath.ElementAt(0).Code}");</para>
        /// </example>
        [Property("Entity Axis Cause of Death", Property.Types.Tuple4Arr, "Coded Content", "", true, IGURL.EntityAxisCauseOfDeath, false, 50)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code=80356-9)", "")]
        public IEnumerable<(int LineNumber, int Position, string Code, bool ECode)> EntityAxisCauseOfDeath
        {
            get
            {
                List<(int LineNumber, int Position, string Code, bool ECode)> eac = new List<(int LineNumber, int Position, string Code, bool ECode)>();
                if (EntityAxisCauseOfDeathObsList != null)
                {
                    foreach (Observation ob in EntityAxisCauseOfDeathObsList)
                    {
                        int? lineNumber = null;
                        int? position = null;
                        string icd10code = null;
                        bool ecode = false;
                        Observation.ComponentComponent positionComp = ob.Component.Where(c => c.Code.Coding[0].Code == "position").FirstOrDefault();
                        if (positionComp != null && positionComp.Value != null)
                        {
                            position = ((Integer)positionComp.Value).Value;
                        }
                        Observation.ComponentComponent lineNumComp = ob.Component.Where(c => c.Code.Coding[0].Code == "lineNumber").FirstOrDefault();
                        if (lineNumComp != null && lineNumComp.Value != null)
                        {
                            lineNumber = ((Integer)lineNumComp.Value).Value;
                        }
                        CodeableConcept valueCC = (CodeableConcept)ob.Value;
                        if (valueCC != null && valueCC.Coding != null && valueCC.Coding.Count() > 0)
                        {
                            icd10code = valueCC.Coding[0].Code;
                        }
                        Observation.ComponentComponent ecodeComp = ob.Component.Where(c => c.Code.Coding[0].Code == "eCodeIndicator").FirstOrDefault();
                        if (ecodeComp != null && ecodeComp.Value != null)
                        {
                            ecode = (bool)((FhirBoolean)ecodeComp.Value).Value;
                        }
                        if (lineNumber != null && position != null && icd10code != null)
                        {
                            eac.Add((LineNumber: (int)lineNumber, Position: (int)position, Code: icd10code, ECode: ecode));
                        }
                    }
                }
                return eac.OrderBy(element => element.LineNumber).ThenBy(element => element.Position);
            }
            set
            {
                // clear all existing eac
                Bundle.Entry.RemoveAll(entry => entry.Resource.ResourceType == ResourceType.Observation && (((Observation)entry.Resource).Code.Coding.First().Code == "80356-9"));
                if (EntityAxisCauseOfDeathObsList != null)
                {
                    EntityAxisCauseOfDeathObsList.Clear();
                }
                else
                {
                    EntityAxisCauseOfDeathObsList = new List<Observation>();
                }
                // Rebuild the list of observations
                foreach ((int LineNumber, int Position, string Code, bool ECode) eac in value)
                {
                    Observation ob = new Observation();
                    ob.Id = Guid.NewGuid().ToString();
                    ob.Meta = new Meta();
                    string[] entityAxis_profile = { ProfileURL.EntityAxisCauseOfDeath };
                    ob.Meta.Profile = entityAxis_profile;
                    ob.Status = ObservationStatus.Final;
                    ob.Code = new CodeableConcept(CodeSystems.LOINC, "80356-9", "Cause of death entity axis code [Automated]", null);
                    ob.Subject = new ResourceReference("urn:uuid:" + Decedent.Id);
                    AddReferenceToComposition(ob.Id, "CodedContent");

                    ob.Effective = new FhirDateTime();
                    ob.Value = new CodeableConcept(CodeSystems.ICD10, eac.Code, null, null);

                    Observation.ComponentComponent lineNumComp = new Observation.ComponentComponent();
                    lineNumComp.Value = new Integer(eac.LineNumber);
                    lineNumComp.Code = new CodeableConcept(CodeSystems.Component, "lineNumber", "lineNumber", null);
                    ob.Component.Add(lineNumComp);

                    Observation.ComponentComponent positionComp = new Observation.ComponentComponent();
                    positionComp.Value = new Integer(eac.Position);
                    positionComp.Code = new CodeableConcept(CodeSystems.Component, "position", "Position", null);
                    ob.Component.Add(positionComp);

                    Observation.ComponentComponent eCodeComp = new Observation.ComponentComponent();
                    eCodeComp.Value = new FhirBoolean(eac.ECode);
                    eCodeComp.Code = new CodeableConcept(CodeSystems.Component, "eCodeIndicator", "eCodeIndicator", null);
                    ob.Component.Add(eCodeComp);

                    Bundle.AddResourceEntry(ob, "urn:uuid:" + ob.Id);
                    EntityAxisCauseOfDeathObsList.Add(ob);
                }
            }
        }

        /// <summary>Record Axis Cause Of Death</summary>
        /// <value>record-axis codes</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Tuple&lt;string, string, string&gt;[] eac = new Tuple&lt;string, string, string&gt;{Tuple.Create("position", "code", "pregnancy")}</para>
        /// <para>ExampleDeathRecord.RecordAxisCauseOfDeath = new [] { (Position: 1, Code: "T27.3", Pregnancy: true) };</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"First Record Axis Code: {ExampleDeathRecord.RecordAxisCauseOfDeath.ElememtAt(0).Code}");</para>
        /// </example>
        [Property("Record Axis Cause Of Death", Property.Types.Tuple4Arr, "Coded Content", "", true, IGURL.RecordAxisCauseOfDeath, false, 50)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code=80357-7)", "")]
        public IEnumerable<(int Position, string Code, bool Pregnancy)> RecordAxisCauseOfDeath
        {
            get
            {
                List<(int Position, string Code, bool Pregnancy)> rac = new List<(int Position, string Code, bool Pregnancy)>();
                if (RecordAxisCauseOfDeathObsList != null)
                {
                    foreach (Observation ob in RecordAxisCauseOfDeathObsList)
                    {
                        int? position = null;
                        string icd10code = null;
                        bool pregnancy = false;
                        Observation.ComponentComponent positionComp = ob.Component.Where(c => c.Code.Coding[0].Code == "position").FirstOrDefault();
                        if (positionComp != null && positionComp.Value != null)
                        {
                            position = ((Integer)positionComp.Value).Value;
                        }
                        CodeableConcept valueCC = (CodeableConcept)ob.Value;
                        if (valueCC != null && valueCC.Coding != null && valueCC.Coding.Count() > 0)
                        {
                            icd10code = valueCC.Coding[0].Code;
                        }
                        Observation.ComponentComponent pregComp = ob.Component.Where(c => c.Code.Coding[0].Code == "wouldBeUnderlyingCauseOfDeathWithoutPregnancy").FirstOrDefault();
                        if (pregComp != null && pregComp.Value != null)
                        {
                            pregnancy = (bool)((FhirBoolean)pregComp.Value).Value;
                        }
                        if (position != null && icd10code != null)
                        {
                            rac.Add((Position: (int)position, Code: icd10code, Pregnancy: pregnancy));
                        }
                    }
                }
                return rac.OrderBy(entry => entry.Position);
            }
            set
            {
                // clear all existing eac
                Bundle.Entry.RemoveAll(entry => entry.Resource.ResourceType == ResourceType.Observation && (((Observation)entry.Resource).Code.Coding.First().Code == "80357-7"));
                if (RecordAxisCauseOfDeathObsList != null)
                {
                    RecordAxisCauseOfDeathObsList.Clear();
                }
                else
                {
                    RecordAxisCauseOfDeathObsList = new List<Observation>();
                }
                // Rebuild the list of observations
                foreach ((int Position, string Code, bool Pregnancy) rac in value)
                {
                    Observation ob = new Observation();
                    ob.Id = Guid.NewGuid().ToString();
                    ob.Meta = new Meta();
                    string[] recordAxis_profile = { ProfileURL.RecordAxisCauseOfDeath };
                    ob.Meta.Profile = recordAxis_profile;
                    ob.Status = ObservationStatus.Final;
                    ob.Code = new CodeableConcept(CodeSystems.LOINC, "80357-7", "Cause of death record axis code [Automated]", null);
                    ob.Subject = new ResourceReference("urn:uuid:" + Decedent.Id);
                    AddReferenceToComposition(ob.Id, "CodedContent");

                    ob.Effective = new FhirDateTime();
                    ob.Value = new CodeableConcept(CodeSystems.ICD10, rac.Code, null, null);

                    Observation.ComponentComponent positionComp = new Observation.ComponentComponent();
                    positionComp.Value = new Integer(rac.Position);
                    positionComp.Code = new CodeableConcept(CodeSystems.Component, "position", "Position", null);
                    ob.Component.Add(positionComp);

                    // Record axis codes have an unusual and obscure handling of a Pregnancy flag, for more information see
                    // http://build.fhir.org/ig/HL7/vrdr/branches/master/StructureDefinition-vrdr-record-axis-cause-of-death.html#usage
                    if (rac.Pregnancy)
                    {
                        Observation.ComponentComponent pregComp = new Observation.ComponentComponent();
                        pregComp.Value = new FhirBoolean(true);
                        pregComp.Code = new CodeableConcept(CodeSystems.Component, "wouldBeUnderlyingCauseOfDeathWithoutPregnancy", "Would be underlying cause of death without pregnancy, if true");
                        ob.Component.Add(pregComp);
                    }

                    Bundle.AddResourceEntry(ob, "urn:uuid:" + ob.Id);
                    RecordAxisCauseOfDeathObsList.Add(ob);
                }
            }
        }


        /// <summary>The year NCHS received the death record.</summary>
        /// <value>year</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.ReceiptYear = 2022 </para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Receipt Year: {ExampleDeathRecord.ReceiptYear}");</para>
        /// </example>
        [Property("ReceiptYear", Property.Types.UInt32, "Coded Content", "Coding Status", true, IGURL.CodingStatusValues, true)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code=codingstatus)", "")]
        public uint? ReceiptYear
        {
            get
            {
                Date date = CodingStatusValues?.GetSingleValue<Date>("receiptDate");
                return GetDateFragmentOrPartialDate(date, ExtensionURL.DateYear);
            }
            set
            {
                if (CodingStatusValues == null)
                {
                    CreateCodingStatusValues();
                }
                Date date = CodingStatusValues?.GetSingleValue<Date>("receiptDate");
                SetPartialDate(date.Extension.Find(ext => ext.Url == ExtensionURL.PartialDate), ExtensionURL.DateYear, value);
            }
        }

        /// <summary>
        /// The month NCHS received the death record.
        /// </summary>
        /// <summary>The month NCHS received the death record.</summary>
        /// <value>month</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.ReceiptMonth = 11 </para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Receipt Month: {ExampleDeathRecord.ReceiptMonth}");</para>
        /// </example>
        [Property("ReceiptMonth", Property.Types.UInt32, "Coded Content", "Coding Status", true, IGURL.CodingStatusValues, true)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code=codingstatus)", "")]
        public uint? ReceiptMonth
        {
            get
            {
                Date date = CodingStatusValues?.GetSingleValue<Date>("receiptDate");
                return GetDateFragmentOrPartialDate(date, ExtensionURL.DateMonth);
            }
            set
            {
                if (CodingStatusValues == null)
                {
                    CreateCodingStatusValues();
                }
                Date date = CodingStatusValues?.GetSingleValue<Date>("receiptDate");
                SetPartialDate(date.Extension.Find(ext => ext.Url == ExtensionURL.PartialDate), ExtensionURL.DateMonth, value);
            }
        }

        /// <summary>The day NCHS received the death record.</summary>
        /// <value>month</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.ReceiptDay = 13 </para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Receipt Day: {ExampleDeathRecord.ReceiptDay}");</para>
        /// </example>
        [Property("ReceiptDay", Property.Types.UInt32, "Coded Content", "Coding Status", true, IGURL.CodingStatusValues, true)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code=codingstatus)", "")]
        public uint? ReceiptDay
        {
            get
            {
                Date date = CodingStatusValues?.GetSingleValue<Date>("receiptDate");
                return GetDateFragmentOrPartialDate(date, ExtensionURL.DateDay);
            }
            set
            {
                if (CodingStatusValues == null)
                {
                    CreateCodingStatusValues();
                }
                Date date = CodingStatusValues?.GetSingleValue<Date>("receiptDate");
                SetPartialDate(date.Extension.Find(ext => ext.Url == ExtensionURL.PartialDate), ExtensionURL.DateDay, value);
            }
        }

        /// <summary>Receipt Date.</summary>
        /// <value>receipt date</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.ReceiptDate = "2018-02-19";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Receipt Date: {ExampleDeathRecord.ReceiptDate}");</para>
        /// </example>
        [Property("Receipt Date", Property.Types.StringDateTime, "Coded Content", "Receipt Date.", true, IGURL.CodingStatusValues, true, 25)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code=codingstatus)", "")]
        public string ReceiptDate
        {
            get
            {
                // We support this legacy-style API entrypoint via the new partial date and time entrypoints
                if (ReceiptYear != null && ReceiptMonth != null && ReceiptDay != null)
                {
                    Date result = new Date((int)ReceiptYear, (int)ReceiptMonth, (int)ReceiptDay);
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
                    ReceiptYear = (uint?)parsedDate.Year;
                    ReceiptMonth = (uint?)parsedDate.Month;
                    ReceiptDay = (uint?)parsedDate.Day;
                }
            }
        }

        /// <summary>
        /// Coder Status; TRX field with no IJE mapping
        /// </summary>
        /// <summary>Coder Status; TRX field with no IJE mapping</summary>
        /// <value>integer code</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.CoderStatus = 3;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Coder STatus {ExampleDeathRecord.CoderStatus}");</para>
        /// </example>
        [Property("CoderStatus", Property.Types.UInt32, "Coded Content", "Coding Status", true, IGURL.CodingStatusValues, false)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code=codingstatus)", "")]
        public int? CoderStatus
        {
            get
            {
                return this.CodingStatusValues?.GetSingleValue<Integer>("coderStatus")?.Value;
            }
            set
            {
                if (CodingStatusValues == null)
                {
                    CreateCodingStatusValues();
                }
                CodingStatusValues.Remove("coderStatus");
                if (value != null)
                {
                    CodingStatusValues.Add("coderStatus", new Integer(value));
                }
            }
        }

        /// <summary>
        /// Shipment Number; TRX field with no IJE mapping
        /// </summary>
        /// <summary>Coder Status; TRX field with no IJE mapping</summary>
        /// <value>string</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.ShipmentNumber = "abc123"";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Shipment Number{ExampleDeathRecord.ShipmentNumber}");</para>
        /// </example>
        [Property("ShipmentNumber", Property.Types.String, "Coded Content", "Coding Status", true, IGURL.CodingStatusValues, false)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code=codingstatus)", "")]
        public string ShipmentNumber
        {
            get
            {
                return this.CodingStatusValues?.GetSingleValue<FhirString>("shipmentNumber")?.Value;
            }
            set
            {
                if (CodingStatusValues == null)
                {
                    CreateCodingStatusValues();
                }
                CodingStatusValues.Remove("shipmentNumber");
                if (value != null)
                {
                    CodingStatusValues.Add("shipmentNumber", new FhirString(value));
                }
            }
        }
        /// <summary>
        /// Intentional Reject
        /// </summary>
        /// <summary>Intentional Reject</summary>
        /// <value>string</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; reject = new Dictionary&lt;string, string&gt;();</para>
        /// <para>format.Add("code", ValueSets.FilingFormat.electronic);</para>
        /// <para>format.Add("system", CodeSystems.IntentionalReject);</para>
        /// <para>format.Add("display", "Reject1");</para>
        /// <para>ExampleDeathRecord.IntentionalReject = "reject";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Intentional Reject {ExampleDeathRecord.IntentionalReject}");</para>
        /// </example>
        [Property("IntentionalReject", Property.Types.Dictionary, "Coded Content", "Coding Status", true, IGURL.CodingStatusValues, true)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code=codingstatus)", "")]
        public Dictionary<string, string> IntentionalReject
        {
            get
            {
                CodeableConcept intentionalReject = this.CodingStatusValues?.GetSingleValue<CodeableConcept>("intentionalReject");
                if (intentionalReject != null)
                {
                    return CodeableConceptToDict(intentionalReject);
                }
                return EmptyCodeableDict();
            }
            set
            {
                if (CodingStatusValues == null)
                {
                    CreateCodingStatusValues();
                }
                CodingStatusValues.Remove("intentionalReject");
                if (value != null)
                {
                    CodingStatusValues.Add("intentionalReject", DictToCodeableConcept(value));
                }
            }
        }

        /// <summary>Intentional Reject Helper.</summary>
        /// <value>Intentional Reject
        /// <para>"code" - the code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.IntentionalRejectHelper = ValueSets.IntentionalReject.Not_Rejected;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Intentional Reject Code: {ExampleDeathRecord.IntentionalRejectHelper}");</para>
        /// </example>
        [Property("IntentionalRejectHelper", Property.Types.String, "Intentional Reject Codes", "IntentionalRejectCodes.", false, IGURL.CodingStatusValues, true, 4)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code=codingstatus)", "")]
        public string IntentionalRejectHelper
        {
            get
            {
                if (IntentionalReject.ContainsKey("code"))
                {
                    return IntentionalReject["code"];
                }
                return null;
            }
            set
            {
                SetCodeValue("IntentionalReject", value, VRDR.ValueSets.IntentionalReject.Codes);
            }
        }

        /// <summary>Acme System Reject.</summary>
        /// <value>
        /// <para>"code" - the code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; reject = new Dictionary&lt;string, string&gt;();</para>
        /// <para>format.Add("code", ValueSets.FilingFormat.electronic);</para>
        /// <para>format.Add("system", CodeSystems.SystemReject);</para>
        /// <para>format.Add("display", "3");</para>
        /// <para>ExampleDeathRecord.AcmeSystemReject = reject;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Acme System Reject Code: {ExampleDeathRecord.AcmeSystemReject}");</para>
        /// </example>

        [Property("AcmeSystemReject", Property.Types.Dictionary, "Coded Content", "Coding Status", true, IGURL.CodingStatusValues, true)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code=codingstatus)", "")]
        public Dictionary<string, string> AcmeSystemReject
        {
            get
            {
                CodeableConcept acmeSystemReject = this.CodingStatusValues?.GetSingleValue<CodeableConcept>("acmeSystemReject");
                if (acmeSystemReject != null)
                {
                    return CodeableConceptToDict(acmeSystemReject);
                }
                return EmptyCodeableDict();
            }
            set
            {
                if (CodingStatusValues == null)
                {
                    CreateCodingStatusValues();
                }
                CodingStatusValues.Remove("acmeSystemReject");
                if (value != null)
                {
                    CodingStatusValues.Add("acmeSystemReject", DictToCodeableConcept(value));
                }
            }
        }

        /// <summary>Acme System Reject.</summary>
        /// <value>
        /// <para>"code" - the code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.AcmeSystemRejectHelper = "3";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Acme System Reject Code: {ExampleDeathRecord.AcmeSystemReject}");</para>
        /// </example>
        [Property("AcmeSystemRejectHelper", Property.Types.String, "Acme System Reject Codes", "AcmeSystemRejectCodes.", false, IGURL.CodingStatusValues, true, 4)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code=codingstatus)", "")]
        public string AcmeSystemRejectHelper
        {
            get
            {
                if (AcmeSystemReject.ContainsKey("code"))
                {
                    return AcmeSystemReject["code"];
                }
                return null;
            }
            set
            {
                SetCodeValue("AcmeSystemReject", value, VRDR.ValueSets.AcmeSystemReject.Codes);
            }
        }


        /// <summary>Transax Conversion Flag</summary>
        /// <value>
        /// <para>"code" - the code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; tcf = new Dictionary&lt;string, string&gt;();</para>
        /// <para>tcf.Add("code", "3");</para>
        /// <para>tcf.Add("system", CodeSystems.TransaxConversion);</para>
        /// <para>tcf.Add("display", "Conversion using non-ambivalent table entries");</para>
        /// <para>ExampleDeathRecord.TransaxConversion = tcf;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Transax Conversion Code: {ExampleDeathRecord.TransaxConversion}");</para>
        /// </example>
        [Property("TransaxConversion", Property.Types.Dictionary, "Coded Content", "Coding Status", true, IGURL.CodingStatusValues, true)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code=codingstatus)", "")]
        public Dictionary<string, string> TransaxConversion
        {
            get
            {
                CodeableConcept transaxConversion = this.CodingStatusValues?.GetSingleValue<CodeableConcept>("transaxConversion");
                if (transaxConversion != null)
                {
                    return CodeableConceptToDict(transaxConversion);
                }
                return EmptyCodeableDict();
            }
            set
            {
                if (CodingStatusValues == null)
                {
                    CreateCodingStatusValues();
                }
                CodingStatusValues.Remove("transaxConversion");
                if (value != null)
                {
                    CodingStatusValues.Add("transaxConversion", DictToCodeableConcept(value));
                }
            }
        }

        /// <summary>TransaxConversion Helper.</summary>
        /// <value>transax conversion code
        /// <para>"code" - the code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.TransaxConversionHelper = ValueSets.TransaxConversion.Conversion_Using_Non_Ambivalent_Table_Entries;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Filing Format: {ExampleDeathRecord.TransaxConversionHelper}");</para>
        /// </example>
        [Property("TransaxConversionFlag Helper", Property.Types.String, "Transax Conversion", "TransaxConversion Flag.", false, IGURL.CodingStatusValues, true, 4)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code=codingstatus)", "")]
        public string TransaxConversionHelper
        {
            get
            {
                if (TransaxConversion.ContainsKey("code"))
                {
                    return TransaxConversion["code"];
                }
                return null;
            }
            set
            {
                SetCodeValue("TransaxConversion", value, VRDR.ValueSets.TransaxConversion.Codes);
            }
        }


        /////////////////////////////////////////////////////////////////////////////////
        //
        // Class helper methods useful for building, searching through records.
        //
        /////////////////////////////////////////////////////////////////////////////////

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
            if(Composition == null)
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
            var compositionEntry = Bundle.Entry.FirstOrDefault(entry => entry.Resource.ResourceType == ResourceType.Composition);
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
            var patientEntry = Bundle.Entry.FirstOrDefault(entry => entry.Resource.ResourceType == ResourceType.Patient);
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
                var practitionerEntry = Bundle.Entry.FirstOrDefault(entry => entry.Resource.ResourceType == ResourceType.Practitioner && (entry.FullUrl == Composition.Attester.First().Party.Reference ||
                (entry.Resource.Id != null && entry.Resource.Id == attesterID)));
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
            var procedureEntry = Bundle.Entry.FirstOrDefault(entry => entry.Resource.ResourceType == ResourceType.Procedure);
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
            var funeralHome = Bundle.Entry.FirstOrDefault(entry => entry.Resource.ResourceType == ResourceType.Organization &&
                    ((Organization)entry.Resource).Type.FirstOrDefault() != null && (CodeableConceptToDict(((Organization)entry.Resource).Type.First())["code"] == "funeralhome"));
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
            // // Grab Mortician
            // // IMPROVEMENT: Move away from using meta profile or id to find this Practitioner, use reference from disposition method performer instead or as well
            // var morticianEntry = Bundle.Entry.FirstOrDefault( entry => entry.Resource.ResourceType == ResourceType.Practitioner && entry.Resource.Meta.Profile.FirstOrDefault() != null && MatchesProfile("VRDR-Mortician", entry.Resource.Meta.Profile.FirstOrDefault()));
            // if (morticianEntry == null)
            // {
            //     morticianEntry = Bundle.Entry.FirstOrDefault( entry => entry.Resource.ResourceType == ResourceType.Practitioner && ((Practitioner)entry.Resource).Id != Certifier.Id );
            // }
            // if (morticianEntry != null)
            // {
            //     Mortician = (Practitioner)morticianEntry.Resource;
            // }


            // Grab Coding Status
            var parameterEntry = Bundle.Entry.FirstOrDefault(entry => entry.Resource.ResourceType == ResourceType.Parameters);
            if (parameterEntry != null)
            {
                CodingStatusValues = (Parameters)parameterEntry.Resource;
            }
            // Scan through all Observations to make sure they all have codes!
            foreach (var ob in Bundle.Entry.Where(entry => entry.Resource.ResourceType == ResourceType.Observation))
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
            foreach (var rp in Bundle.Entry.Where(entry => entry.Resource.ResourceType == ResourceType.RelatedPerson))
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
            foreach (var rp in Bundle.Entry.Where(entry => entry.Resource.ResourceType == ResourceType.Location))
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

        /// <summary>Helper function to set a codeable value based on a code and the set of allowed codes.</summary>
        // <param name="field">the field name to set.</param>
        // <param name="code">the code to set the field to.</param>
        // <param name="options">the list of valid options and related display strings and code systems</param>
        private void SetCodeValue(string field, string code, string[,] options)
        {
            // If string is empty don't bother to set the value
            if (code == null || code == "")
            {
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
                    cityCode.Value = new PositiveInt(Int32.Parse(dict["addressCityC"]));
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
                    countyCode.Value = new PositiveInt(Int32.Parse(dict["addressCountyC"]));
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
                // Special address field to support the jurisdiction extension custom to VRDR to support YC (New York City)
                // as used in the DeathLocationLoc
                if (dict.ContainsKey("addressJurisdiction") && !String.IsNullOrEmpty(dict["addressJurisdiction"]))
                {
                    if (address.StateElement == null)
                    {
                        address.StateElement = new FhirString();
                    }
                    address.StateElement.Extension.RemoveAll(ext => ext.Url == ExtensionURL.LocationJurisdictionId);
                    Extension extension = new Extension(ExtensionURL.LocationJurisdictionId, new FhirString(dict["addressJurisdiction"]));
                    address.StateElement.Extension.Add(extension);
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
            Dictionary<string, string> dictionary = EmptyAddrDict();
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

                if (addr.CityElement != null)
                {
                    Extension cityCode = addr.CityElement.Extension.Where(ext => ext.Url == ExtensionURL.CityCode).FirstOrDefault();
                    if (cityCode != null)
                    {
                        dictionary["addressCityC"] = cityCode.Value.ToString();
                    }
                }

                if (addr.DistrictElement != null)
                {
                    Extension districtCode = addr.DistrictElement.Extension.Where(ext => ext.Url == ExtensionURL.DistrictCode).FirstOrDefault();
                    if (districtCode != null)
                    {
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
            dictionary.Add("addressCountyC", "");
            dictionary.Add("addressState", "");
            dictionary.Add("addressJurisdiction", "");
            dictionary.Add("addressZip", "");
            dictionary.Add("addressCountry", "");
            dictionary.Add("addressStnum", "");
            dictionary.Add("addressPredir", "");
            dictionary.Add("addressStname", "");
            dictionary.Add("addressStdesig", "");
            dictionary.Add("addressPostdir", "");
            dictionary.Add("addressUnitnum", "");
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
        /// returns null if no match is found.</summary>
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

        /// <summary>Get a value from a Dictionary, but return null if the key doesn't exist or the value is an empty string.</summary>
        private static string GetValue(Dictionary<string, string> dict, string key)
        {
            if (dict != null && dict.ContainsKey(key) && !String.IsNullOrWhiteSpace(dict[key]))
            {
                return dict[key];
            }
            return null;
        }

        // /// <summary>Check to make sure the given profile contains the given resource.</summary>
        // private static bool MatchesProfile(string resource, string profile)
        // {
        //     if (!String.IsNullOrWhiteSpace(profile) && profile.Contains(resource))
        //     {
        //         return true;
        //     }
        //     return false;
        // }

        /// <summary>Combine the given dictionaries and return the combined result.</summary>
        private static Dictionary<string, string> UpdateDictionary(Dictionary<string, string> a, Dictionary<string, string> b)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            foreach (KeyValuePair<string, string> entry in a)
            {
                dictionary[entry.Key] = entry.Value;
            }
            foreach (KeyValuePair<string, string> entry in b)
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
            foreach (PropertyInfo property in typeof(DeathRecord).GetProperties().OrderBy(p => p.GetCustomAttribute<Property>().Priority))
            {
                // Grab property annotation for this property
                Property info = property.GetCustomAttribute<Property>();

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
                FHIRPath path = property.GetCustomAttribute<FHIRPath>();
                var matches = Navigator.Select(path.Path);
                if (matches.Count() > 0)
                {
                    if (info.Type == Property.Types.TupleCOD || info.Type == Property.Types.TupleArr || info.Type == Property.Types.Tuple4Arr)
                    {
                        // Make sure to grab all of the Conditions for COD
                        string xml = "";
                        string json = "";
                        foreach (var match in matches)
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
                    foreach (PropertyParam propParameter in property.GetCustomAttributes<PropertyParam>())
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
            foreach (KeyValuePair<string, Dictionary<string, dynamic>> category in description)
            {
                // Loop over each property
                foreach (KeyValuePair<string, dynamic> property in category.Value)
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
                        foreach (KeyValuePair<string, Dictionary<string, string>> entry in moreInfo)
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
            TupleCOD,
            /// <summary>Parameter is an unsigned integer.</summary>
            UInt32,
            /// <summary>Parameter is an array of 4-Tuples, specifically for entity axis codes.</summary>
            Tuple4Arr
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
