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

        /// <summary>The Certifier.</summary>
        private Practitioner Certifier;

        /// <summary>The Certification.</summary>
        private Procedure DeathCertification;

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

        /// <summary>Emerging Issues.</summary>
        protected Observation EmergingIssues;

        /// <summary> Coding Status </summary>
        private Parameters CodingStatusValues;

       /// <summary>Usual Work.</summary>
        private Observation UsualWork;

        /// <summary>Whether the decedent served in the military</summary>
        private Observation MilitaryServiceObs;

        /// <summary>The Funeral Home.</summary>
        private Organization FuneralHome;

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

        /// <summary>Date Of Surgery.</summary>
        private Observation SurgeryDateObs;

        // Coded Observations
        /// <summary> Activity at Time of Death </summary>
        private Observation ActivityAtTimeOfDeathObs;

        /// <summary> Automated Underlying Cause of Death </summary>
        private Observation AutomatedUnderlyingCauseOfDeathObs;

        /// <summary> Manual Underlying Cause of Death </summary>
        private Observation ManualUnderlyingCauseOfDeathObs;

        /// <summary> Place Of Injury </summary>
        private Observation PlaceOfInjuryObs;

        /// <summary>The Decedent's Race and Ethnicity provided by Jurisdiction.</summary>
        private Observation CodedRaceAndEthnicityObs;

        /// <summary>Entity Axis Cause of Death</summary>
        private List<Observation> EntityAxisCauseOfDeathObsList;

        /// <summary>Record Axis Cause of Death</summary>
        private List<Observation> RecordAxisCauseOfDeathObsList;

    }
}