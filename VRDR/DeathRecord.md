# DeathRecord

## Contents

- [AcmeSystemReject](#T-VRDR-ValueSets-AcmeSystemReject 'VRDR.ValueSets.AcmeSystemReject')
  - [Acme_Reject](#F-VRDR-ValueSets-AcmeSystemReject-Acme_Reject 'VRDR.ValueSets.AcmeSystemReject.Acme_Reject')
  - [Codes](#F-VRDR-ValueSets-AcmeSystemReject-Codes 'VRDR.ValueSets.AcmeSystemReject.Codes')
  - [Micar_Reject_Dictionary_Match](#F-VRDR-ValueSets-AcmeSystemReject-Micar_Reject_Dictionary_Match 'VRDR.ValueSets.AcmeSystemReject.Micar_Reject_Dictionary_Match')
  - [Micar_Reject_Rule_Application](#F-VRDR-ValueSets-AcmeSystemReject-Micar_Reject_Rule_Application 'VRDR.ValueSets.AcmeSystemReject.Micar_Reject_Rule_Application')
  - [Not_Rejected](#F-VRDR-ValueSets-AcmeSystemReject-Not_Rejected 'VRDR.ValueSets.AcmeSystemReject.Not_Rejected')
  - [Record_Reviewed](#F-VRDR-ValueSets-AcmeSystemReject-Record_Reviewed 'VRDR.ValueSets.AcmeSystemReject.Record_Reviewed')
- [ActivityAtTimeOfDeath](#T-VRDR-Mappings-ActivityAtTimeOfDeath 'VRDR.Mappings.ActivityAtTimeOfDeath')
- [ActivityAtTimeOfDeath](#T-VRDR-ValueSets-ActivityAtTimeOfDeath 'VRDR.ValueSets.ActivityAtTimeOfDeath')
  - [FHIRToIJE](#F-VRDR-Mappings-ActivityAtTimeOfDeath-FHIRToIJE 'VRDR.Mappings.ActivityAtTimeOfDeath.FHIRToIJE')
  - [IJEToFHIR](#F-VRDR-Mappings-ActivityAtTimeOfDeath-IJEToFHIR 'VRDR.Mappings.ActivityAtTimeOfDeath.IJEToFHIR')
  - [Codes](#F-VRDR-ValueSets-ActivityAtTimeOfDeath-Codes 'VRDR.ValueSets.ActivityAtTimeOfDeath.Codes')
  - [During_Unspecified_Activity](#F-VRDR-ValueSets-ActivityAtTimeOfDeath-During_Unspecified_Activity 'VRDR.ValueSets.ActivityAtTimeOfDeath.During_Unspecified_Activity')
  - [Unknown](#F-VRDR-ValueSets-ActivityAtTimeOfDeath-Unknown 'VRDR.ValueSets.ActivityAtTimeOfDeath.Unknown')
  - [While_Engaged_In_Leisure_Activities](#F-VRDR-ValueSets-ActivityAtTimeOfDeath-While_Engaged_In_Leisure_Activities 'VRDR.ValueSets.ActivityAtTimeOfDeath.While_Engaged_In_Leisure_Activities')
  - [While_Engaged_In_Other_Specified_Activities](#F-VRDR-ValueSets-ActivityAtTimeOfDeath-While_Engaged_In_Other_Specified_Activities 'VRDR.ValueSets.ActivityAtTimeOfDeath.While_Engaged_In_Other_Specified_Activities')
  - [While_Engaged_In_Other_Types_Of_Work](#F-VRDR-ValueSets-ActivityAtTimeOfDeath-While_Engaged_In_Other_Types_Of_Work 'VRDR.ValueSets.ActivityAtTimeOfDeath.While_Engaged_In_Other_Types_Of_Work')
  - [While_Engaged_In_Sports_Activity](#F-VRDR-ValueSets-ActivityAtTimeOfDeath-While_Engaged_In_Sports_Activity 'VRDR.ValueSets.ActivityAtTimeOfDeath.While_Engaged_In_Sports_Activity')
  - [While_Resting_Sleeping_Eating_Or_Engaging_In_Other_Vital_Activities](#F-VRDR-ValueSets-ActivityAtTimeOfDeath-While_Resting_Sleeping_Eating_Or_Engaging_In_Other_Vital_Activities 'VRDR.ValueSets.ActivityAtTimeOfDeath.While_Resting_Sleeping_Eating_Or_Engaging_In_Other_Vital_Activities')
  - [While_Working_For_Income](#F-VRDR-ValueSets-ActivityAtTimeOfDeath-While_Working_For_Income 'VRDR.ValueSets.ActivityAtTimeOfDeath.While_Working_For_Income')
- [AdministrativeGender](#T-VRDR-Mappings-AdministrativeGender 'VRDR.Mappings.AdministrativeGender')
- [AdministrativeGender](#T-VRDR-ValueSets-AdministrativeGender 'VRDR.ValueSets.AdministrativeGender')
  - [FHIRToIJE](#F-VRDR-Mappings-AdministrativeGender-FHIRToIJE 'VRDR.Mappings.AdministrativeGender.FHIRToIJE')
  - [IJEToFHIR](#F-VRDR-Mappings-AdministrativeGender-IJEToFHIR 'VRDR.Mappings.AdministrativeGender.IJEToFHIR')
  - [Codes](#F-VRDR-ValueSets-AdministrativeGender-Codes 'VRDR.ValueSets.AdministrativeGender.Codes')
  - [Female](#F-VRDR-ValueSets-AdministrativeGender-Female 'VRDR.ValueSets.AdministrativeGender.Female')
  - [Male](#F-VRDR-ValueSets-AdministrativeGender-Male 'VRDR.ValueSets.AdministrativeGender.Male')
  - [Unknown](#F-VRDR-ValueSets-AdministrativeGender-Unknown 'VRDR.ValueSets.AdministrativeGender.Unknown')
- [CertifierTypes](#T-VRDR-Mappings-CertifierTypes 'VRDR.Mappings.CertifierTypes')
- [CertifierTypes](#T-VRDR-ValueSets-CertifierTypes 'VRDR.ValueSets.CertifierTypes')
  - [FHIRToIJE](#F-VRDR-Mappings-CertifierTypes-FHIRToIJE 'VRDR.Mappings.CertifierTypes.FHIRToIJE')
  - [IJEToFHIR](#F-VRDR-Mappings-CertifierTypes-IJEToFHIR 'VRDR.Mappings.CertifierTypes.IJEToFHIR')
  - [Certifying_Physician](#F-VRDR-ValueSets-CertifierTypes-Certifying_Physician 'VRDR.ValueSets.CertifierTypes.Certifying_Physician')
  - [Codes](#F-VRDR-ValueSets-CertifierTypes-Codes 'VRDR.ValueSets.CertifierTypes.Codes')
  - [Medical_Examiner_Coroner](#F-VRDR-ValueSets-CertifierTypes-Medical_Examiner_Coroner 'VRDR.ValueSets.CertifierTypes.Medical_Examiner_Coroner')
  - [Other_Specify](#F-VRDR-ValueSets-CertifierTypes-Other_Specify 'VRDR.ValueSets.CertifierTypes.Other_Specify')
  - [Pronouncing_Certifying_Physician](#F-VRDR-ValueSets-CertifierTypes-Pronouncing_Certifying_Physician 'VRDR.ValueSets.CertifierTypes.Pronouncing_Certifying_Physician')
- [CodeSystems](#T-VRDR-CodeSystems 'VRDR.CodeSystems')
  - [ActivityAtTimeOfDeath](#F-VRDR-CodeSystems-ActivityAtTimeOfDeath 'VRDR.CodeSystems.ActivityAtTimeOfDeath')
  - [AdministrativeGender](#F-VRDR-CodeSystems-AdministrativeGender 'VRDR.CodeSystems.AdministrativeGender')
  - [BypassEditFlag](#F-VRDR-CodeSystems-BypassEditFlag 'VRDR.CodeSystems.BypassEditFlag')
  - [Component](#F-VRDR-CodeSystems-Component 'VRDR.CodeSystems.Component')
  - [ComponentCode](#F-VRDR-CodeSystems-ComponentCode 'VRDR.CodeSystems.ComponentCode')
  - [Data_Absent_Reason_HL7_V3](#F-VRDR-CodeSystems-Data_Absent_Reason_HL7_V3 'VRDR.CodeSystems.Data_Absent_Reason_HL7_V3')
  - [DegreeLicenceAndCertificate](#F-VRDR-CodeSystems-DegreeLicenceAndCertificate 'VRDR.CodeSystems.DegreeLicenceAndCertificate')
  - [DocumentSections](#F-VRDR-CodeSystems-DocumentSections 'VRDR.CodeSystems.DocumentSections')
  - [EducationLevel](#F-VRDR-CodeSystems-EducationLevel 'VRDR.CodeSystems.EducationLevel')
  - [FilingFormat](#F-VRDR-CodeSystems-FilingFormat 'VRDR.CodeSystems.FilingFormat')
  - [HL7_identifier_type](#F-VRDR-CodeSystems-HL7_identifier_type 'VRDR.CodeSystems.HL7_identifier_type')
  - [HL7_location_physical_type](#F-VRDR-CodeSystems-HL7_location_physical_type 'VRDR.CodeSystems.HL7_location_physical_type')
  - [HispanicOrigin](#F-VRDR-CodeSystems-HispanicOrigin 'VRDR.CodeSystems.HispanicOrigin')
  - [ICD10](#F-VRDR-CodeSystems-ICD10 'VRDR.CodeSystems.ICD10')
  - [IntentionalReject](#F-VRDR-CodeSystems-IntentionalReject 'VRDR.CodeSystems.IntentionalReject')
  - [LOINC](#F-VRDR-CodeSystems-LOINC 'VRDR.CodeSystems.LOINC')
  - [LocationType](#F-VRDR-CodeSystems-LocationType 'VRDR.CodeSystems.LocationType')
  - [MissingValueReason](#F-VRDR-CodeSystems-MissingValueReason 'VRDR.CodeSystems.MissingValueReason')
  - [NullFlavor_HL7_V3](#F-VRDR-CodeSystems-NullFlavor_HL7_V3 'VRDR.CodeSystems.NullFlavor_HL7_V3')
  - [ObservationCategory](#F-VRDR-CodeSystems-ObservationCategory 'VRDR.CodeSystems.ObservationCategory')
  - [ObservationCode](#F-VRDR-CodeSystems-ObservationCode 'VRDR.CodeSystems.ObservationCode')
  - [OrganizationType](#F-VRDR-CodeSystems-OrganizationType 'VRDR.CodeSystems.OrganizationType')
  - [PH_MaritalStatus_HL7_2x](#F-VRDR-CodeSystems-PH_MaritalStatus_HL7_2x 'VRDR.CodeSystems.PH_MaritalStatus_HL7_2x')
  - [PregnancyStatus](#F-VRDR-CodeSystems-PregnancyStatus 'VRDR.CodeSystems.PregnancyStatus')
  - [RaceCode](#F-VRDR-CodeSystems-RaceCode 'VRDR.CodeSystems.RaceCode')
  - [RaceRecode40](#F-VRDR-CodeSystems-RaceRecode40 'VRDR.CodeSystems.RaceRecode40')
  - [ReplaceStatus](#F-VRDR-CodeSystems-ReplaceStatus 'VRDR.CodeSystems.ReplaceStatus')
  - [RoleCode_HL7_V3](#F-VRDR-CodeSystems-RoleCode_HL7_V3 'VRDR.CodeSystems.RoleCode_HL7_V3')
  - [SCT](#F-VRDR-CodeSystems-SCT 'VRDR.CodeSystems.SCT')
  - [SystemReject](#F-VRDR-CodeSystems-SystemReject 'VRDR.CodeSystems.SystemReject')
  - [TransaxConversion](#F-VRDR-CodeSystems-TransaxConversion 'VRDR.CodeSystems.TransaxConversion')
  - [US_NPI_HL7](#F-VRDR-CodeSystems-US_NPI_HL7 'VRDR.CodeSystems.US_NPI_HL7')
  - [US_SSN](#F-VRDR-CodeSystems-US_SSN 'VRDR.CodeSystems.US_SSN')
  - [UnitsOfMeasure](#F-VRDR-CodeSystems-UnitsOfMeasure 'VRDR.CodeSystems.UnitsOfMeasure')
  - [YesNo](#F-VRDR-CodeSystems-YesNo 'VRDR.CodeSystems.YesNo')
  - [YesNo_0136HL7_V2](#F-VRDR-CodeSystems-YesNo_0136HL7_V2 'VRDR.CodeSystems.YesNo_0136HL7_V2')
- [Connectathon](#T-VRDR-Connectathon 'VRDR.Connectathon')
  - [DavisLineberry()](#M-VRDR-Connectathon-DavisLineberry 'VRDR.Connectathon.DavisLineberry')
  - [FideliaAlsup()](#M-VRDR-Connectathon-FideliaAlsup 'VRDR.Connectathon.FideliaAlsup')
  - [FromId()](#M-VRDR-Connectathon-FromId-System-Int32,System-Nullable{System-Int32},System-String- 'VRDR.Connectathon.FromId(System.Int32,System.Nullable{System.Int32},System.String)')
  - [TwilaHilty()](#M-VRDR-Connectathon-TwilaHilty 'VRDR.Connectathon.TwilaHilty')
  - [WriteRecordAsXml()](#M-VRDR-Connectathon-WriteRecordAsXml-VRDR-DeathRecord,System-String- 'VRDR.Connectathon.WriteRecordAsXml(VRDR.DeathRecord,System.String)')
- [ContributoryTobaccoUse](#T-VRDR-Mappings-ContributoryTobaccoUse 'VRDR.Mappings.ContributoryTobaccoUse')
- [ContributoryTobaccoUse](#T-VRDR-ValueSets-ContributoryTobaccoUse 'VRDR.ValueSets.ContributoryTobaccoUse')
  - [FHIRToIJE](#F-VRDR-Mappings-ContributoryTobaccoUse-FHIRToIJE 'VRDR.Mappings.ContributoryTobaccoUse.FHIRToIJE')
  - [IJEToFHIR](#F-VRDR-Mappings-ContributoryTobaccoUse-IJEToFHIR 'VRDR.Mappings.ContributoryTobaccoUse.IJEToFHIR')
  - [Codes](#F-VRDR-ValueSets-ContributoryTobaccoUse-Codes 'VRDR.ValueSets.ContributoryTobaccoUse.Codes')
  - [No](#F-VRDR-ValueSets-ContributoryTobaccoUse-No 'VRDR.ValueSets.ContributoryTobaccoUse.No')
  - [No_Information](#F-VRDR-ValueSets-ContributoryTobaccoUse-No_Information 'VRDR.ValueSets.ContributoryTobaccoUse.No_Information')
  - [Probably](#F-VRDR-ValueSets-ContributoryTobaccoUse-Probably 'VRDR.ValueSets.ContributoryTobaccoUse.Probably')
  - [Unknown](#F-VRDR-ValueSets-ContributoryTobaccoUse-Unknown 'VRDR.ValueSets.ContributoryTobaccoUse.Unknown')
  - [Yes](#F-VRDR-ValueSets-ContributoryTobaccoUse-Yes 'VRDR.ValueSets.ContributoryTobaccoUse.Yes')
- [DeathRecord](#T-VRDR-DeathRecord 'VRDR.DeathRecord')
  - [#ctor()](#M-VRDR-DeathRecord-#ctor 'VRDR.DeathRecord.#ctor')
  - [#ctor(record,permissive)](#M-VRDR-DeathRecord-#ctor-System-String,System-Boolean- 'VRDR.DeathRecord.#ctor(System.String,System.Boolean)')
  - [#ctor(bundle)](#M-VRDR-DeathRecord-#ctor-Hl7-Fhir-Model-Bundle- 'VRDR.DeathRecord.#ctor(Hl7.Fhir.Model.Bundle)')
  - [ActivityAtTimeOfDeathObs](#F-VRDR-DeathRecord-ActivityAtTimeOfDeathObs 'VRDR.DeathRecord.ActivityAtTimeOfDeathObs')
  - [AgeAtDeathObs](#F-VRDR-DeathRecord-AgeAtDeathObs 'VRDR.DeathRecord.AgeAtDeathObs')
  - [AutomatedUnderlyingCauseOfDeathObs](#F-VRDR-DeathRecord-AutomatedUnderlyingCauseOfDeathObs 'VRDR.DeathRecord.AutomatedUnderlyingCauseOfDeathObs')
  - [AutopsyPerformed](#F-VRDR-DeathRecord-AutopsyPerformed 'VRDR.DeathRecord.AutopsyPerformed')
  - [BirthRecordIdentifier](#F-VRDR-DeathRecord-BirthRecordIdentifier 'VRDR.DeathRecord.BirthRecordIdentifier')
  - [BlankPlaceholder](#F-VRDR-DeathRecord-BlankPlaceholder 'VRDR.DeathRecord.BlankPlaceholder')
  - [Bundle](#F-VRDR-DeathRecord-Bundle 'VRDR.DeathRecord.Bundle')
  - [CauseOfDeathConditionA](#F-VRDR-DeathRecord-CauseOfDeathConditionA 'VRDR.DeathRecord.CauseOfDeathConditionA')
  - [CauseOfDeathConditionB](#F-VRDR-DeathRecord-CauseOfDeathConditionB 'VRDR.DeathRecord.CauseOfDeathConditionB')
  - [CauseOfDeathConditionC](#F-VRDR-DeathRecord-CauseOfDeathConditionC 'VRDR.DeathRecord.CauseOfDeathConditionC')
  - [CauseOfDeathConditionD](#F-VRDR-DeathRecord-CauseOfDeathConditionD 'VRDR.DeathRecord.CauseOfDeathConditionD')
  - [Certifier](#F-VRDR-DeathRecord-Certifier 'VRDR.DeathRecord.Certifier')
  - [CodedRaceAndEthnicityObs](#F-VRDR-DeathRecord-CodedRaceAndEthnicityObs 'VRDR.DeathRecord.CodedRaceAndEthnicityObs')
  - [CodingStatusValues](#F-VRDR-DeathRecord-CodingStatusValues 'VRDR.DeathRecord.CodingStatusValues')
  - [Composition](#F-VRDR-DeathRecord-Composition 'VRDR.DeathRecord.Composition')
  - [ConditionContributingToDeath](#F-VRDR-DeathRecord-ConditionContributingToDeath 'VRDR.DeathRecord.ConditionContributingToDeath')
  - [DeathCertification](#F-VRDR-DeathRecord-DeathCertification 'VRDR.DeathRecord.DeathCertification')
  - [DeathDateObs](#F-VRDR-DeathRecord-DeathDateObs 'VRDR.DeathRecord.DeathDateObs')
  - [DeathLocationLoc](#F-VRDR-DeathRecord-DeathLocationLoc 'VRDR.DeathRecord.DeathLocationLoc')
  - [Decedent](#F-VRDR-DeathRecord-Decedent 'VRDR.DeathRecord.Decedent')
  - [DecedentEducationLevel](#F-VRDR-DeathRecord-DecedentEducationLevel 'VRDR.DeathRecord.DecedentEducationLevel')
  - [DispositionLocation](#F-VRDR-DeathRecord-DispositionLocation 'VRDR.DeathRecord.DispositionLocation')
  - [DispositionMethod](#F-VRDR-DeathRecord-DispositionMethod 'VRDR.DeathRecord.DispositionMethod')
  - [EmergingIssues](#F-VRDR-DeathRecord-EmergingIssues 'VRDR.DeathRecord.EmergingIssues')
  - [EntityAxisCauseOfDeathObsList](#F-VRDR-DeathRecord-EntityAxisCauseOfDeathObsList 'VRDR.DeathRecord.EntityAxisCauseOfDeathObsList')
  - [ExaminerContactedObs](#F-VRDR-DeathRecord-ExaminerContactedObs 'VRDR.DeathRecord.ExaminerContactedObs')
  - [Father](#F-VRDR-DeathRecord-Father 'VRDR.DeathRecord.Father')
  - [FuneralHome](#F-VRDR-DeathRecord-FuneralHome 'VRDR.DeathRecord.FuneralHome')
  - [InjuryIncidentObs](#F-VRDR-DeathRecord-InjuryIncidentObs 'VRDR.DeathRecord.InjuryIncidentObs')
  - [InjuryLocationLoc](#F-VRDR-DeathRecord-InjuryLocationLoc 'VRDR.DeathRecord.InjuryLocationLoc')
  - [InputRaceAndEthnicityObs](#F-VRDR-DeathRecord-InputRaceAndEthnicityObs 'VRDR.DeathRecord.InputRaceAndEthnicityObs')
  - [MannerOfDeath](#F-VRDR-DeathRecord-MannerOfDeath 'VRDR.DeathRecord.MannerOfDeath')
  - [ManualUnderlyingCauseOfDeathObs](#F-VRDR-DeathRecord-ManualUnderlyingCauseOfDeathObs 'VRDR.DeathRecord.ManualUnderlyingCauseOfDeathObs')
  - [MilitaryServiceObs](#F-VRDR-DeathRecord-MilitaryServiceObs 'VRDR.DeathRecord.MilitaryServiceObs')
  - [MortalityData](#F-VRDR-DeathRecord-MortalityData 'VRDR.DeathRecord.MortalityData')
  - [Mother](#F-VRDR-DeathRecord-Mother 'VRDR.DeathRecord.Mother')
  - [Navigator](#F-VRDR-DeathRecord-Navigator 'VRDR.DeathRecord.Navigator')
  - [PlaceOfInjuryObs](#F-VRDR-DeathRecord-PlaceOfInjuryObs 'VRDR.DeathRecord.PlaceOfInjuryObs')
  - [PregnancyObs](#F-VRDR-DeathRecord-PregnancyObs 'VRDR.DeathRecord.PregnancyObs')
  - [RecordAxisCauseOfDeathObsList](#F-VRDR-DeathRecord-RecordAxisCauseOfDeathObsList 'VRDR.DeathRecord.RecordAxisCauseOfDeathObsList')
  - [Spouse](#F-VRDR-DeathRecord-Spouse 'VRDR.DeathRecord.Spouse')
  - [SurgeryDateObs](#F-VRDR-DeathRecord-SurgeryDateObs 'VRDR.DeathRecord.SurgeryDateObs')
  - [TobaccoUseObs](#F-VRDR-DeathRecord-TobaccoUseObs 'VRDR.DeathRecord.TobaccoUseObs')
  - [UsualWork](#F-VRDR-DeathRecord-UsualWork 'VRDR.DeathRecord.UsualWork')
  - [AcmeSystemReject](#P-VRDR-DeathRecord-AcmeSystemReject 'VRDR.DeathRecord.AcmeSystemReject')
  - [AcmeSystemRejectHelper](#P-VRDR-DeathRecord-AcmeSystemRejectHelper 'VRDR.DeathRecord.AcmeSystemRejectHelper')
  - [ActivityAtDeath](#P-VRDR-DeathRecord-ActivityAtDeath 'VRDR.DeathRecord.ActivityAtDeath')
  - [ActivityAtDeathHelper](#P-VRDR-DeathRecord-ActivityAtDeathHelper 'VRDR.DeathRecord.ActivityAtDeathHelper')
  - [AgeAtDeath](#P-VRDR-DeathRecord-AgeAtDeath 'VRDR.DeathRecord.AgeAtDeath')
  - [AgeAtDeathEditFlag](#P-VRDR-DeathRecord-AgeAtDeathEditFlag 'VRDR.DeathRecord.AgeAtDeathEditFlag')
  - [AgeAtDeathEditFlagHelper](#P-VRDR-DeathRecord-AgeAtDeathEditFlagHelper 'VRDR.DeathRecord.AgeAtDeathEditFlagHelper')
  - [AutomatedUnderlyingCOD](#P-VRDR-DeathRecord-AutomatedUnderlyingCOD 'VRDR.DeathRecord.AutomatedUnderlyingCOD')
  - [AutopsyPerformedIndicator](#P-VRDR-DeathRecord-AutopsyPerformedIndicator 'VRDR.DeathRecord.AutopsyPerformedIndicator')
  - [AutopsyPerformedIndicatorHelper](#P-VRDR-DeathRecord-AutopsyPerformedIndicatorHelper 'VRDR.DeathRecord.AutopsyPerformedIndicatorHelper')
  - [AutopsyResultsAvailable](#P-VRDR-DeathRecord-AutopsyResultsAvailable 'VRDR.DeathRecord.AutopsyResultsAvailable')
  - [AutopsyResultsAvailableHelper](#P-VRDR-DeathRecord-AutopsyResultsAvailableHelper 'VRDR.DeathRecord.AutopsyResultsAvailableHelper')
  - [BirthDay](#P-VRDR-DeathRecord-BirthDay 'VRDR.DeathRecord.BirthDay')
  - [BirthMonth](#P-VRDR-DeathRecord-BirthMonth 'VRDR.DeathRecord.BirthMonth')
  - [BirthRecordId](#P-VRDR-DeathRecord-BirthRecordId 'VRDR.DeathRecord.BirthRecordId')
  - [BirthRecordState](#P-VRDR-DeathRecord-BirthRecordState 'VRDR.DeathRecord.BirthRecordState')
  - [BirthRecordYear](#P-VRDR-DeathRecord-BirthRecordYear 'VRDR.DeathRecord.BirthRecordYear')
  - [BirthYear](#P-VRDR-DeathRecord-BirthYear 'VRDR.DeathRecord.BirthYear')
  - [COD1A](#P-VRDR-DeathRecord-COD1A 'VRDR.DeathRecord.COD1A')
  - [COD1B](#P-VRDR-DeathRecord-COD1B 'VRDR.DeathRecord.COD1B')
  - [COD1C](#P-VRDR-DeathRecord-COD1C 'VRDR.DeathRecord.COD1C')
  - [COD1D](#P-VRDR-DeathRecord-COD1D 'VRDR.DeathRecord.COD1D')
  - [CausesOfDeath](#P-VRDR-DeathRecord-CausesOfDeath 'VRDR.DeathRecord.CausesOfDeath')
  - [CertificationRole](#P-VRDR-DeathRecord-CertificationRole 'VRDR.DeathRecord.CertificationRole')
  - [CertificationRoleHelper](#P-VRDR-DeathRecord-CertificationRoleHelper 'VRDR.DeathRecord.CertificationRoleHelper')
  - [CertifiedTime](#P-VRDR-DeathRecord-CertifiedTime 'VRDR.DeathRecord.CertifiedTime')
  - [CertifierAddress](#P-VRDR-DeathRecord-CertifierAddress 'VRDR.DeathRecord.CertifierAddress')
  - [CertifierFamilyName](#P-VRDR-DeathRecord-CertifierFamilyName 'VRDR.DeathRecord.CertifierFamilyName')
  - [CertifierGivenNames](#P-VRDR-DeathRecord-CertifierGivenNames 'VRDR.DeathRecord.CertifierGivenNames')
  - [CertifierIdentifier](#P-VRDR-DeathRecord-CertifierIdentifier 'VRDR.DeathRecord.CertifierIdentifier')
  - [CertifierSuffix](#P-VRDR-DeathRecord-CertifierSuffix 'VRDR.DeathRecord.CertifierSuffix')
  - [CoderStatus](#P-VRDR-DeathRecord-CoderStatus 'VRDR.DeathRecord.CoderStatus')
  - [ContactRelationship](#P-VRDR-DeathRecord-ContactRelationship 'VRDR.DeathRecord.ContactRelationship')
  - [ContributingConditions](#P-VRDR-DeathRecord-ContributingConditions 'VRDR.DeathRecord.ContributingConditions')
  - [DateOfBirth](#P-VRDR-DeathRecord-DateOfBirth 'VRDR.DeathRecord.DateOfBirth')
  - [DateOfDeath](#P-VRDR-DeathRecord-DateOfDeath 'VRDR.DeathRecord.DateOfDeath')
  - [DateOfDeathPronouncement](#P-VRDR-DeathRecord-DateOfDeathPronouncement 'VRDR.DeathRecord.DateOfDeathPronouncement')
  - [DeathDay](#P-VRDR-DeathRecord-DeathDay 'VRDR.DeathRecord.DeathDay')
  - [DeathLocationAddress](#P-VRDR-DeathRecord-DeathLocationAddress 'VRDR.DeathRecord.DeathLocationAddress')
  - [DeathLocationDescription](#P-VRDR-DeathRecord-DeathLocationDescription 'VRDR.DeathRecord.DeathLocationDescription')
  - [DeathLocationJurisdiction](#P-VRDR-DeathRecord-DeathLocationJurisdiction 'VRDR.DeathRecord.DeathLocationJurisdiction')
  - [DeathLocationLatitude](#P-VRDR-DeathRecord-DeathLocationLatitude 'VRDR.DeathRecord.DeathLocationLatitude')
  - [DeathLocationLongitude](#P-VRDR-DeathRecord-DeathLocationLongitude 'VRDR.DeathRecord.DeathLocationLongitude')
  - [DeathLocationName](#P-VRDR-DeathRecord-DeathLocationName 'VRDR.DeathRecord.DeathLocationName')
  - [DeathLocationType](#P-VRDR-DeathRecord-DeathLocationType 'VRDR.DeathRecord.DeathLocationType')
  - [DeathLocationTypeHelper](#P-VRDR-DeathRecord-DeathLocationTypeHelper 'VRDR.DeathRecord.DeathLocationTypeHelper')
  - [DeathMonth](#P-VRDR-DeathRecord-DeathMonth 'VRDR.DeathRecord.DeathMonth')
  - [DeathRecordIdentifier](#P-VRDR-DeathRecord-DeathRecordIdentifier 'VRDR.DeathRecord.DeathRecordIdentifier')
  - [DeathTime](#P-VRDR-DeathRecord-DeathTime 'VRDR.DeathRecord.DeathTime')
  - [DeathYear](#P-VRDR-DeathRecord-DeathYear 'VRDR.DeathRecord.DeathYear')
  - [DecedentDispositionMethod](#P-VRDR-DeathRecord-DecedentDispositionMethod 'VRDR.DeathRecord.DecedentDispositionMethod')
  - [DecedentDispositionMethodHelper](#P-VRDR-DeathRecord-DecedentDispositionMethodHelper 'VRDR.DeathRecord.DecedentDispositionMethodHelper')
  - [DispositionLocationAddress](#P-VRDR-DeathRecord-DispositionLocationAddress 'VRDR.DeathRecord.DispositionLocationAddress')
  - [DispositionLocationName](#P-VRDR-DeathRecord-DispositionLocationName 'VRDR.DeathRecord.DispositionLocationName')
  - [EducationLevel](#P-VRDR-DeathRecord-EducationLevel 'VRDR.DeathRecord.EducationLevel')
  - [EducationLevelEditFlag](#P-VRDR-DeathRecord-EducationLevelEditFlag 'VRDR.DeathRecord.EducationLevelEditFlag')
  - [EducationLevelEditFlagHelper](#P-VRDR-DeathRecord-EducationLevelEditFlagHelper 'VRDR.DeathRecord.EducationLevelEditFlagHelper')
  - [EducationLevelHelper](#P-VRDR-DeathRecord-EducationLevelHelper 'VRDR.DeathRecord.EducationLevelHelper')
  - [EighthEditedRaceCode](#P-VRDR-DeathRecord-EighthEditedRaceCode 'VRDR.DeathRecord.EighthEditedRaceCode')
  - [EighthEditedRaceCodeHelper](#P-VRDR-DeathRecord-EighthEditedRaceCodeHelper 'VRDR.DeathRecord.EighthEditedRaceCodeHelper')
  - [EmergingIssue1_1](#P-VRDR-DeathRecord-EmergingIssue1_1 'VRDR.DeathRecord.EmergingIssue1_1')
  - [EmergingIssue1_2](#P-VRDR-DeathRecord-EmergingIssue1_2 'VRDR.DeathRecord.EmergingIssue1_2')
  - [EmergingIssue1_3](#P-VRDR-DeathRecord-EmergingIssue1_3 'VRDR.DeathRecord.EmergingIssue1_3')
  - [EmergingIssue1_4](#P-VRDR-DeathRecord-EmergingIssue1_4 'VRDR.DeathRecord.EmergingIssue1_4')
  - [EmergingIssue1_5](#P-VRDR-DeathRecord-EmergingIssue1_5 'VRDR.DeathRecord.EmergingIssue1_5')
  - [EmergingIssue1_6](#P-VRDR-DeathRecord-EmergingIssue1_6 'VRDR.DeathRecord.EmergingIssue1_6')
  - [EmergingIssue20](#P-VRDR-DeathRecord-EmergingIssue20 'VRDR.DeathRecord.EmergingIssue20')
  - [EmergingIssue8_1](#P-VRDR-DeathRecord-EmergingIssue8_1 'VRDR.DeathRecord.EmergingIssue8_1')
  - [EmergingIssue8_2](#P-VRDR-DeathRecord-EmergingIssue8_2 'VRDR.DeathRecord.EmergingIssue8_2')
  - [EmergingIssue8_3](#P-VRDR-DeathRecord-EmergingIssue8_3 'VRDR.DeathRecord.EmergingIssue8_3')
  - [EntityAxisCauseOfDeath](#P-VRDR-DeathRecord-EntityAxisCauseOfDeath 'VRDR.DeathRecord.EntityAxisCauseOfDeath')
  - [Ethnicity1](#P-VRDR-DeathRecord-Ethnicity1 'VRDR.DeathRecord.Ethnicity1')
  - [Ethnicity1Helper](#P-VRDR-DeathRecord-Ethnicity1Helper 'VRDR.DeathRecord.Ethnicity1Helper')
  - [Ethnicity2](#P-VRDR-DeathRecord-Ethnicity2 'VRDR.DeathRecord.Ethnicity2')
  - [Ethnicity2Helper](#P-VRDR-DeathRecord-Ethnicity2Helper 'VRDR.DeathRecord.Ethnicity2Helper')
  - [Ethnicity3](#P-VRDR-DeathRecord-Ethnicity3 'VRDR.DeathRecord.Ethnicity3')
  - [Ethnicity3Helper](#P-VRDR-DeathRecord-Ethnicity3Helper 'VRDR.DeathRecord.Ethnicity3Helper')
  - [Ethnicity4](#P-VRDR-DeathRecord-Ethnicity4 'VRDR.DeathRecord.Ethnicity4')
  - [Ethnicity4Helper](#P-VRDR-DeathRecord-Ethnicity4Helper 'VRDR.DeathRecord.Ethnicity4Helper')
  - [EthnicityLiteral](#P-VRDR-DeathRecord-EthnicityLiteral 'VRDR.DeathRecord.EthnicityLiteral')
  - [ExaminerContacted](#P-VRDR-DeathRecord-ExaminerContacted 'VRDR.DeathRecord.ExaminerContacted')
  - [ExaminerContactedHelper](#P-VRDR-DeathRecord-ExaminerContactedHelper 'VRDR.DeathRecord.ExaminerContactedHelper')
  - [FamilyName](#P-VRDR-DeathRecord-FamilyName 'VRDR.DeathRecord.FamilyName')
  - [FatherFamilyName](#P-VRDR-DeathRecord-FatherFamilyName 'VRDR.DeathRecord.FatherFamilyName')
  - [FatherGivenNames](#P-VRDR-DeathRecord-FatherGivenNames 'VRDR.DeathRecord.FatherGivenNames')
  - [FatherSuffix](#P-VRDR-DeathRecord-FatherSuffix 'VRDR.DeathRecord.FatherSuffix')
  - [FifthEditedRaceCode](#P-VRDR-DeathRecord-FifthEditedRaceCode 'VRDR.DeathRecord.FifthEditedRaceCode')
  - [FifthEditedRaceCodeHelper](#P-VRDR-DeathRecord-FifthEditedRaceCodeHelper 'VRDR.DeathRecord.FifthEditedRaceCodeHelper')
  - [FilingFormat](#P-VRDR-DeathRecord-FilingFormat 'VRDR.DeathRecord.FilingFormat')
  - [FilingFormatHelper](#P-VRDR-DeathRecord-FilingFormatHelper 'VRDR.DeathRecord.FilingFormatHelper')
  - [FirstAmericanIndianRaceCode](#P-VRDR-DeathRecord-FirstAmericanIndianRaceCode 'VRDR.DeathRecord.FirstAmericanIndianRaceCode')
  - [FirstAmericanIndianRaceCodeHelper](#P-VRDR-DeathRecord-FirstAmericanIndianRaceCodeHelper 'VRDR.DeathRecord.FirstAmericanIndianRaceCodeHelper')
  - [FirstEditedRaceCode](#P-VRDR-DeathRecord-FirstEditedRaceCode 'VRDR.DeathRecord.FirstEditedRaceCode')
  - [FirstEditedRaceCodeHelper](#P-VRDR-DeathRecord-FirstEditedRaceCodeHelper 'VRDR.DeathRecord.FirstEditedRaceCodeHelper')
  - [FirstOtherAsianRaceCode](#P-VRDR-DeathRecord-FirstOtherAsianRaceCode 'VRDR.DeathRecord.FirstOtherAsianRaceCode')
  - [FirstOtherAsianRaceCodeHelper](#P-VRDR-DeathRecord-FirstOtherAsianRaceCodeHelper 'VRDR.DeathRecord.FirstOtherAsianRaceCodeHelper')
  - [FirstOtherPacificIslanderRaceCode](#P-VRDR-DeathRecord-FirstOtherPacificIslanderRaceCode 'VRDR.DeathRecord.FirstOtherPacificIslanderRaceCode')
  - [FirstOtherPacificIslanderRaceCodeHelper](#P-VRDR-DeathRecord-FirstOtherPacificIslanderRaceCodeHelper 'VRDR.DeathRecord.FirstOtherPacificIslanderRaceCodeHelper')
  - [FirstOtherRaceCode](#P-VRDR-DeathRecord-FirstOtherRaceCode 'VRDR.DeathRecord.FirstOtherRaceCode')
  - [FirstOtherRaceCodeHelper](#P-VRDR-DeathRecord-FirstOtherRaceCodeHelper 'VRDR.DeathRecord.FirstOtherRaceCodeHelper')
  - [FourthEditedRaceCode](#P-VRDR-DeathRecord-FourthEditedRaceCode 'VRDR.DeathRecord.FourthEditedRaceCode')
  - [FourthEditedRaceCodeHelper](#P-VRDR-DeathRecord-FourthEditedRaceCodeHelper 'VRDR.DeathRecord.FourthEditedRaceCodeHelper')
  - [FuneralHomeAddress](#P-VRDR-DeathRecord-FuneralHomeAddress 'VRDR.DeathRecord.FuneralHomeAddress')
  - [FuneralHomeName](#P-VRDR-DeathRecord-FuneralHomeName 'VRDR.DeathRecord.FuneralHomeName')
  - [GivenNames](#P-VRDR-DeathRecord-GivenNames 'VRDR.DeathRecord.GivenNames')
  - [HispanicCode](#P-VRDR-DeathRecord-HispanicCode 'VRDR.DeathRecord.HispanicCode')
  - [HispanicCodeForLiteral](#P-VRDR-DeathRecord-HispanicCodeForLiteral 'VRDR.DeathRecord.HispanicCodeForLiteral')
  - [HispanicCodeForLiteralHelper](#P-VRDR-DeathRecord-HispanicCodeForLiteralHelper 'VRDR.DeathRecord.HispanicCodeForLiteralHelper')
  - [HispanicCodeHelper](#P-VRDR-DeathRecord-HispanicCodeHelper 'VRDR.DeathRecord.HispanicCodeHelper')
  - [INTERVAL1A](#P-VRDR-DeathRecord-INTERVAL1A 'VRDR.DeathRecord.INTERVAL1A')
  - [INTERVAL1B](#P-VRDR-DeathRecord-INTERVAL1B 'VRDR.DeathRecord.INTERVAL1B')
  - [INTERVAL1C](#P-VRDR-DeathRecord-INTERVAL1C 'VRDR.DeathRecord.INTERVAL1C')
  - [INTERVAL1D](#P-VRDR-DeathRecord-INTERVAL1D 'VRDR.DeathRecord.INTERVAL1D')
  - [Identifier](#P-VRDR-DeathRecord-Identifier 'VRDR.DeathRecord.Identifier')
  - [InjuryAtWork](#P-VRDR-DeathRecord-InjuryAtWork 'VRDR.DeathRecord.InjuryAtWork')
  - [InjuryAtWorkHelper](#P-VRDR-DeathRecord-InjuryAtWorkHelper 'VRDR.DeathRecord.InjuryAtWorkHelper')
  - [InjuryDate](#P-VRDR-DeathRecord-InjuryDate 'VRDR.DeathRecord.InjuryDate')
  - [InjuryDay](#P-VRDR-DeathRecord-InjuryDay 'VRDR.DeathRecord.InjuryDay')
  - [InjuryDescription](#P-VRDR-DeathRecord-InjuryDescription 'VRDR.DeathRecord.InjuryDescription')
  - [InjuryLocationAddress](#P-VRDR-DeathRecord-InjuryLocationAddress 'VRDR.DeathRecord.InjuryLocationAddress')
  - [InjuryLocationLatitude](#P-VRDR-DeathRecord-InjuryLocationLatitude 'VRDR.DeathRecord.InjuryLocationLatitude')
  - [InjuryLocationLongitude](#P-VRDR-DeathRecord-InjuryLocationLongitude 'VRDR.DeathRecord.InjuryLocationLongitude')
  - [InjuryLocationName](#P-VRDR-DeathRecord-InjuryLocationName 'VRDR.DeathRecord.InjuryLocationName')
  - [InjuryMonth](#P-VRDR-DeathRecord-InjuryMonth 'VRDR.DeathRecord.InjuryMonth')
  - [InjuryPlaceDescription](#P-VRDR-DeathRecord-InjuryPlaceDescription 'VRDR.DeathRecord.InjuryPlaceDescription')
  - [InjuryTime](#P-VRDR-DeathRecord-InjuryTime 'VRDR.DeathRecord.InjuryTime')
  - [InjuryYear](#P-VRDR-DeathRecord-InjuryYear 'VRDR.DeathRecord.InjuryYear')
  - [IntentionalReject](#P-VRDR-DeathRecord-IntentionalReject 'VRDR.DeathRecord.IntentionalReject')
  - [IntentionalRejectHelper](#P-VRDR-DeathRecord-IntentionalRejectHelper 'VRDR.DeathRecord.IntentionalRejectHelper')
  - [MaidenName](#P-VRDR-DeathRecord-MaidenName 'VRDR.DeathRecord.MaidenName')
  - [ManUnderlyingCOD](#P-VRDR-DeathRecord-ManUnderlyingCOD 'VRDR.DeathRecord.ManUnderlyingCOD')
  - [MannerOfDeathType](#P-VRDR-DeathRecord-MannerOfDeathType 'VRDR.DeathRecord.MannerOfDeathType')
  - [MannerOfDeathTypeHelper](#P-VRDR-DeathRecord-MannerOfDeathTypeHelper 'VRDR.DeathRecord.MannerOfDeathTypeHelper')
  - [MaritalStatus](#P-VRDR-DeathRecord-MaritalStatus 'VRDR.DeathRecord.MaritalStatus')
  - [MaritalStatusEditFlag](#P-VRDR-DeathRecord-MaritalStatusEditFlag 'VRDR.DeathRecord.MaritalStatusEditFlag')
  - [MaritalStatusEditFlagHelper](#P-VRDR-DeathRecord-MaritalStatusEditFlagHelper 'VRDR.DeathRecord.MaritalStatusEditFlagHelper')
  - [MaritalStatusHelper](#P-VRDR-DeathRecord-MaritalStatusHelper 'VRDR.DeathRecord.MaritalStatusHelper')
  - [MaritalStatusLiteral](#P-VRDR-DeathRecord-MaritalStatusLiteral 'VRDR.DeathRecord.MaritalStatusLiteral')
  - [MilitaryService](#P-VRDR-DeathRecord-MilitaryService 'VRDR.DeathRecord.MilitaryService')
  - [MilitaryServiceHelper](#P-VRDR-DeathRecord-MilitaryServiceHelper 'VRDR.DeathRecord.MilitaryServiceHelper')
  - [MotherGivenNames](#P-VRDR-DeathRecord-MotherGivenNames 'VRDR.DeathRecord.MotherGivenNames')
  - [MotherMaidenName](#P-VRDR-DeathRecord-MotherMaidenName 'VRDR.DeathRecord.MotherMaidenName')
  - [MotherSuffix](#P-VRDR-DeathRecord-MotherSuffix 'VRDR.DeathRecord.MotherSuffix')
  - [PlaceOfBirth](#P-VRDR-DeathRecord-PlaceOfBirth 'VRDR.DeathRecord.PlaceOfBirth')
  - [PlaceOfInjury](#P-VRDR-DeathRecord-PlaceOfInjury 'VRDR.DeathRecord.PlaceOfInjury')
  - [PlaceOfInjuryHelper](#P-VRDR-DeathRecord-PlaceOfInjuryHelper 'VRDR.DeathRecord.PlaceOfInjuryHelper')
  - [PregnancyStatus](#P-VRDR-DeathRecord-PregnancyStatus 'VRDR.DeathRecord.PregnancyStatus')
  - [PregnancyStatusEditFlag](#P-VRDR-DeathRecord-PregnancyStatusEditFlag 'VRDR.DeathRecord.PregnancyStatusEditFlag')
  - [PregnancyStatusEditFlagHelper](#P-VRDR-DeathRecord-PregnancyStatusEditFlagHelper 'VRDR.DeathRecord.PregnancyStatusEditFlagHelper')
  - [PregnancyStatusHelper](#P-VRDR-DeathRecord-PregnancyStatusHelper 'VRDR.DeathRecord.PregnancyStatusHelper')
  - [Race](#P-VRDR-DeathRecord-Race 'VRDR.DeathRecord.Race')
  - [RaceMissingValueReason](#P-VRDR-DeathRecord-RaceMissingValueReason 'VRDR.DeathRecord.RaceMissingValueReason')
  - [RaceMissingValueReasonHelper](#P-VRDR-DeathRecord-RaceMissingValueReasonHelper 'VRDR.DeathRecord.RaceMissingValueReasonHelper')
  - [RaceRecode40](#P-VRDR-DeathRecord-RaceRecode40 'VRDR.DeathRecord.RaceRecode40')
  - [RaceRecode40Helper](#P-VRDR-DeathRecord-RaceRecode40Helper 'VRDR.DeathRecord.RaceRecode40Helper')
  - [ReceiptDate](#P-VRDR-DeathRecord-ReceiptDate 'VRDR.DeathRecord.ReceiptDate')
  - [ReceiptDay](#P-VRDR-DeathRecord-ReceiptDay 'VRDR.DeathRecord.ReceiptDay')
  - [ReceiptMonth](#P-VRDR-DeathRecord-ReceiptMonth 'VRDR.DeathRecord.ReceiptMonth')
  - [ReceiptYear](#P-VRDR-DeathRecord-ReceiptYear 'VRDR.DeathRecord.ReceiptYear')
  - [RecordAxisCauseOfDeath](#P-VRDR-DeathRecord-RecordAxisCauseOfDeath 'VRDR.DeathRecord.RecordAxisCauseOfDeath')
  - [RegisteredTime](#P-VRDR-DeathRecord-RegisteredTime 'VRDR.DeathRecord.RegisteredTime')
  - [ReplaceStatus](#P-VRDR-DeathRecord-ReplaceStatus 'VRDR.DeathRecord.ReplaceStatus')
  - [ReplaceStatusHelper](#P-VRDR-DeathRecord-ReplaceStatusHelper 'VRDR.DeathRecord.ReplaceStatusHelper')
  - [Residence](#P-VRDR-DeathRecord-Residence 'VRDR.DeathRecord.Residence')
  - [ResidenceWithinCityLimits](#P-VRDR-DeathRecord-ResidenceWithinCityLimits 'VRDR.DeathRecord.ResidenceWithinCityLimits')
  - [ResidenceWithinCityLimitsHelper](#P-VRDR-DeathRecord-ResidenceWithinCityLimitsHelper 'VRDR.DeathRecord.ResidenceWithinCityLimitsHelper')
  - [SSN](#P-VRDR-DeathRecord-SSN 'VRDR.DeathRecord.SSN')
  - [SecondAmericanIndianRaceCode](#P-VRDR-DeathRecord-SecondAmericanIndianRaceCode 'VRDR.DeathRecord.SecondAmericanIndianRaceCode')
  - [SecondAmericanIndianRaceCodeHelper](#P-VRDR-DeathRecord-SecondAmericanIndianRaceCodeHelper 'VRDR.DeathRecord.SecondAmericanIndianRaceCodeHelper')
  - [SecondEditedRaceCode](#P-VRDR-DeathRecord-SecondEditedRaceCode 'VRDR.DeathRecord.SecondEditedRaceCode')
  - [SecondEditedRaceCodeHelper](#P-VRDR-DeathRecord-SecondEditedRaceCodeHelper 'VRDR.DeathRecord.SecondEditedRaceCodeHelper')
  - [SecondOtherAsianRaceCode](#P-VRDR-DeathRecord-SecondOtherAsianRaceCode 'VRDR.DeathRecord.SecondOtherAsianRaceCode')
  - [SecondOtherAsianRaceCodeHelper](#P-VRDR-DeathRecord-SecondOtherAsianRaceCodeHelper 'VRDR.DeathRecord.SecondOtherAsianRaceCodeHelper')
  - [SecondOtherPacificIslanderRaceCode](#P-VRDR-DeathRecord-SecondOtherPacificIslanderRaceCode 'VRDR.DeathRecord.SecondOtherPacificIslanderRaceCode')
  - [SecondOtherPacificIslanderRaceCodeHelper](#P-VRDR-DeathRecord-SecondOtherPacificIslanderRaceCodeHelper 'VRDR.DeathRecord.SecondOtherPacificIslanderRaceCodeHelper')
  - [SecondOtherRaceCode](#P-VRDR-DeathRecord-SecondOtherRaceCode 'VRDR.DeathRecord.SecondOtherRaceCode')
  - [SecondOtherRaceCodeHelper](#P-VRDR-DeathRecord-SecondOtherRaceCodeHelper 'VRDR.DeathRecord.SecondOtherRaceCodeHelper')
  - [SeventhEditedRaceCode](#P-VRDR-DeathRecord-SeventhEditedRaceCode 'VRDR.DeathRecord.SeventhEditedRaceCode')
  - [SeventhEditedRaceCodeHelper](#P-VRDR-DeathRecord-SeventhEditedRaceCodeHelper 'VRDR.DeathRecord.SeventhEditedRaceCodeHelper')
  - [SexAtDeath](#P-VRDR-DeathRecord-SexAtDeath 'VRDR.DeathRecord.SexAtDeath')
  - [SexAtDeathHelper](#P-VRDR-DeathRecord-SexAtDeathHelper 'VRDR.DeathRecord.SexAtDeathHelper')
  - [ShipmentNumber](#P-VRDR-DeathRecord-ShipmentNumber 'VRDR.DeathRecord.ShipmentNumber')
  - [SixthEditedRaceCode](#P-VRDR-DeathRecord-SixthEditedRaceCode 'VRDR.DeathRecord.SixthEditedRaceCode')
  - [SixthEditedRaceCodeHelper](#P-VRDR-DeathRecord-SixthEditedRaceCodeHelper 'VRDR.DeathRecord.SixthEditedRaceCodeHelper')
  - [SpouseAlive](#P-VRDR-DeathRecord-SpouseAlive 'VRDR.DeathRecord.SpouseAlive')
  - [SpouseAliveHelper](#P-VRDR-DeathRecord-SpouseAliveHelper 'VRDR.DeathRecord.SpouseAliveHelper')
  - [SpouseFamilyName](#P-VRDR-DeathRecord-SpouseFamilyName 'VRDR.DeathRecord.SpouseFamilyName')
  - [SpouseGivenNames](#P-VRDR-DeathRecord-SpouseGivenNames 'VRDR.DeathRecord.SpouseGivenNames')
  - [SpouseMaidenName](#P-VRDR-DeathRecord-SpouseMaidenName 'VRDR.DeathRecord.SpouseMaidenName')
  - [SpouseSuffix](#P-VRDR-DeathRecord-SpouseSuffix 'VRDR.DeathRecord.SpouseSuffix')
  - [StateLocalIdentifier1](#P-VRDR-DeathRecord-StateLocalIdentifier1 'VRDR.DeathRecord.StateLocalIdentifier1')
  - [StateLocalIdentifier2](#P-VRDR-DeathRecord-StateLocalIdentifier2 'VRDR.DeathRecord.StateLocalIdentifier2')
  - [StateSpecific](#P-VRDR-DeathRecord-StateSpecific 'VRDR.DeathRecord.StateSpecific')
  - [Suffix](#P-VRDR-DeathRecord-Suffix 'VRDR.DeathRecord.Suffix')
  - [SurgeryDate](#P-VRDR-DeathRecord-SurgeryDate 'VRDR.DeathRecord.SurgeryDate')
  - [SurgeryDay](#P-VRDR-DeathRecord-SurgeryDay 'VRDR.DeathRecord.SurgeryDay')
  - [SurgeryMonth](#P-VRDR-DeathRecord-SurgeryMonth 'VRDR.DeathRecord.SurgeryMonth')
  - [SurgeryYear](#P-VRDR-DeathRecord-SurgeryYear 'VRDR.DeathRecord.SurgeryYear')
  - [ThirdEditedRaceCode](#P-VRDR-DeathRecord-ThirdEditedRaceCode 'VRDR.DeathRecord.ThirdEditedRaceCode')
  - [ThirdEditedRaceCodeHelper](#P-VRDR-DeathRecord-ThirdEditedRaceCodeHelper 'VRDR.DeathRecord.ThirdEditedRaceCodeHelper')
  - [TobaccoUse](#P-VRDR-DeathRecord-TobaccoUse 'VRDR.DeathRecord.TobaccoUse')
  - [TobaccoUseHelper](#P-VRDR-DeathRecord-TobaccoUseHelper 'VRDR.DeathRecord.TobaccoUseHelper')
  - [TransaxConversion](#P-VRDR-DeathRecord-TransaxConversion 'VRDR.DeathRecord.TransaxConversion')
  - [TransaxConversionHelper](#P-VRDR-DeathRecord-TransaxConversionHelper 'VRDR.DeathRecord.TransaxConversionHelper')
  - [TransportationRole](#P-VRDR-DeathRecord-TransportationRole 'VRDR.DeathRecord.TransportationRole')
  - [TransportationRoleHelper](#P-VRDR-DeathRecord-TransportationRoleHelper 'VRDR.DeathRecord.TransportationRoleHelper')
  - [UsualIndustry](#P-VRDR-DeathRecord-UsualIndustry 'VRDR.DeathRecord.UsualIndustry')
  - [UsualOccupation](#P-VRDR-DeathRecord-UsualOccupation 'VRDR.DeathRecord.UsualOccupation')
  - [AddReferenceToComposition(reference,code)](#M-VRDR-DeathRecord-AddReferenceToComposition-System-String,System-String- 'VRDR.DeathRecord.AddReferenceToComposition(System.String,System.String)')
  - [AddressToDict(addr)](#M-VRDR-DeathRecord-AddressToDict-Hl7-Fhir-Model-Address- 'VRDR.DeathRecord.AddressToDict(Hl7.Fhir.Model.Address)')
  - [CauseOfDeathCondition()](#M-VRDR-DeathRecord-CauseOfDeathCondition-System-Int32- 'VRDR.DeathRecord.CauseOfDeathCondition(System.Int32)')
  - [CodeableConceptToDict(codeableConcept)](#M-VRDR-DeathRecord-CodeableConceptToDict-Hl7-Fhir-Model-CodeableConcept- 'VRDR.DeathRecord.CodeableConceptToDict(Hl7.Fhir.Model.CodeableConcept)')
  - [CodingToDict(coding)](#M-VRDR-DeathRecord-CodingToDict-Hl7-Fhir-Model-Coding- 'VRDR.DeathRecord.CodingToDict(Hl7.Fhir.Model.Coding)')
  - [CreateActivityAtTimeOfDeathObs()](#M-VRDR-DeathRecord-CreateActivityAtTimeOfDeathObs 'VRDR.DeathRecord.CreateActivityAtTimeOfDeathObs')
  - [CreateAgeAtDeathObs()](#M-VRDR-DeathRecord-CreateAgeAtDeathObs 'VRDR.DeathRecord.CreateAgeAtDeathObs')
  - [CreateAutomatedUnderlyingCauseOfDeathObs()](#M-VRDR-DeathRecord-CreateAutomatedUnderlyingCauseOfDeathObs 'VRDR.DeathRecord.CreateAutomatedUnderlyingCauseOfDeathObs')
  - [CreateAutopsyPerformed()](#M-VRDR-DeathRecord-CreateAutopsyPerformed 'VRDR.DeathRecord.CreateAutopsyPerformed')
  - [CreateCertifier()](#M-VRDR-DeathRecord-CreateCertifier 'VRDR.DeathRecord.CreateCertifier')
  - [CreateCodedRaceAndEthnicityObs()](#M-VRDR-DeathRecord-CreateCodedRaceAndEthnicityObs 'VRDR.DeathRecord.CreateCodedRaceAndEthnicityObs')
  - [CreateDeathCertification()](#M-VRDR-DeathRecord-CreateDeathCertification 'VRDR.DeathRecord.CreateDeathCertification')
  - [CreateDeathDateObs()](#M-VRDR-DeathRecord-CreateDeathDateObs 'VRDR.DeathRecord.CreateDeathDateObs')
  - [CreateDeathLocation()](#M-VRDR-DeathRecord-CreateDeathLocation 'VRDR.DeathRecord.CreateDeathLocation')
  - [CreateDispositionLocation()](#M-VRDR-DeathRecord-CreateDispositionLocation 'VRDR.DeathRecord.CreateDispositionLocation')
  - [CreateEducationLevelObs()](#M-VRDR-DeathRecord-CreateEducationLevelObs 'VRDR.DeathRecord.CreateEducationLevelObs')
  - [CreateFather()](#M-VRDR-DeathRecord-CreateFather 'VRDR.DeathRecord.CreateFather')
  - [CreateFuneralHome()](#M-VRDR-DeathRecord-CreateFuneralHome 'VRDR.DeathRecord.CreateFuneralHome')
  - [CreateInjuryIncidentObs()](#M-VRDR-DeathRecord-CreateInjuryIncidentObs 'VRDR.DeathRecord.CreateInjuryIncidentObs')
  - [CreateInjuryLocationLoc()](#M-VRDR-DeathRecord-CreateInjuryLocationLoc 'VRDR.DeathRecord.CreateInjuryLocationLoc')
  - [CreateInputRaceEthnicityObs()](#M-VRDR-DeathRecord-CreateInputRaceEthnicityObs 'VRDR.DeathRecord.CreateInputRaceEthnicityObs')
  - [CreateManualUnderlyingCauseOfDeathObs()](#M-VRDR-DeathRecord-CreateManualUnderlyingCauseOfDeathObs 'VRDR.DeathRecord.CreateManualUnderlyingCauseOfDeathObs')
  - [CreateMother()](#M-VRDR-DeathRecord-CreateMother 'VRDR.DeathRecord.CreateMother')
  - [CreatePlaceOfInjuryObs()](#M-VRDR-DeathRecord-CreatePlaceOfInjuryObs 'VRDR.DeathRecord.CreatePlaceOfInjuryObs')
  - [CreatePregnancyObs()](#M-VRDR-DeathRecord-CreatePregnancyObs 'VRDR.DeathRecord.CreatePregnancyObs')
  - [CreateSpouse()](#M-VRDR-DeathRecord-CreateSpouse 'VRDR.DeathRecord.CreateSpouse')
  - [CreateSurgeryDateObs()](#M-VRDR-DeathRecord-CreateSurgeryDateObs 'VRDR.DeathRecord.CreateSurgeryDateObs')
  - [CreateUsualWork()](#M-VRDR-DeathRecord-CreateUsualWork 'VRDR.DeathRecord.CreateUsualWork')
  - [DatePartToIntegerOrCode(pair)](#M-VRDR-DeathRecord-DatePartToIntegerOrCode-System-Tuple{System-String,System-String}- 'VRDR.DeathRecord.DatePartToIntegerOrCode(System.Tuple{System.String,System.String})')
  - [DatePartsToArray(datePartAbsent)](#M-VRDR-DeathRecord-DatePartsToArray-Hl7-Fhir-Model-Extension- 'VRDR.DeathRecord.DatePartsToArray(Hl7.Fhir.Model.Extension)')
  - [DictToAddress(dict)](#M-VRDR-DeathRecord-DictToAddress-System-Collections-Generic-Dictionary{System-String,System-String}- 'VRDR.DeathRecord.DictToAddress(System.Collections.Generic.Dictionary{System.String,System.String})')
  - [DictToCodeableConcept(dict)](#M-VRDR-DeathRecord-DictToCodeableConcept-System-Collections-Generic-Dictionary{System-String,System-String}- 'VRDR.DeathRecord.DictToCodeableConcept(System.Collections.Generic.Dictionary{System.String,System.String})')
  - [DictToCoding(dict)](#M-VRDR-DeathRecord-DictToCoding-System-Collections-Generic-Dictionary{System-String,System-String}- 'VRDR.DeathRecord.DictToCoding(System.Collections.Generic.Dictionary{System.String,System.String})')
  - [EmptyAddrDict()](#M-VRDR-DeathRecord-EmptyAddrDict 'VRDR.DeathRecord.EmptyAddrDict')
  - [EmptyCodeDict()](#M-VRDR-DeathRecord-EmptyCodeDict 'VRDR.DeathRecord.EmptyCodeDict')
  - [EmptyCodeableDict()](#M-VRDR-DeathRecord-EmptyCodeableDict 'VRDR.DeathRecord.EmptyCodeableDict')
  - [FromDescription(contents)](#M-VRDR-DeathRecord-FromDescription-System-String- 'VRDR.DeathRecord.FromDescription(System.String)')
  - [GetAll(path)](#M-VRDR-DeathRecord-GetAll-System-String- 'VRDR.DeathRecord.GetAll(System.String)')
  - [GetAllString(path)](#M-VRDR-DeathRecord-GetAllString-System-String- 'VRDR.DeathRecord.GetAllString(System.String)')
  - [GetBundle()](#M-VRDR-DeathRecord-GetBundle 'VRDR.DeathRecord.GetBundle')
  - [GetCauseOfDeathCodedContentBundle()](#M-VRDR-DeathRecord-GetCauseOfDeathCodedContentBundle 'VRDR.DeathRecord.GetCauseOfDeathCodedContentBundle')
  - [GetDateFragmentOrPartialDate()](#M-VRDR-DeathRecord-GetDateFragmentOrPartialDate-Hl7-Fhir-Model-Element,System-String- 'VRDR.DeathRecord.GetDateFragmentOrPartialDate(Hl7.Fhir.Model.Element,System.String)')
  - [GetDeathCertificateDocumentBundle()](#M-VRDR-DeathRecord-GetDeathCertificateDocumentBundle 'VRDR.DeathRecord.GetDeathCertificateDocumentBundle')
  - [GetDemographicCodedContentBundle()](#M-VRDR-DeathRecord-GetDemographicCodedContentBundle 'VRDR.DeathRecord.GetDemographicCodedContentBundle')
  - [GetEmergingIssue()](#M-VRDR-DeathRecord-GetEmergingIssue-System-String- 'VRDR.DeathRecord.GetEmergingIssue(System.String)')
  - [GetFirst(path)](#M-VRDR-DeathRecord-GetFirst-System-String- 'VRDR.DeathRecord.GetFirst(System.String)')
  - [GetFirstString(path)](#M-VRDR-DeathRecord-GetFirstString-System-String- 'VRDR.DeathRecord.GetFirstString(System.String)')
  - [GetITypedElement()](#M-VRDR-DeathRecord-GetITypedElement 'VRDR.DeathRecord.GetITypedElement')
  - [GetLast(path)](#M-VRDR-DeathRecord-GetLast-System-String- 'VRDR.DeathRecord.GetLast(System.String)')
  - [GetLastString(path)](#M-VRDR-DeathRecord-GetLastString-System-String- 'VRDR.DeathRecord.GetLastString(System.String)')
  - [GetPartialDate()](#M-VRDR-DeathRecord-GetPartialDate-Hl7-Fhir-Model-Extension,System-String- 'VRDR.DeathRecord.GetPartialDate(Hl7.Fhir.Model.Extension,System.String)')
  - [GetPartialTime()](#M-VRDR-DeathRecord-GetPartialTime-Hl7-Fhir-Model-Extension- 'VRDR.DeathRecord.GetPartialTime(Hl7.Fhir.Model.Extension)')
  - [GetTimeFragmentOrPartialTime()](#M-VRDR-DeathRecord-GetTimeFragmentOrPartialTime-Hl7-Fhir-Model-Element- 'VRDR.DeathRecord.GetTimeFragmentOrPartialTime(Hl7.Fhir.Model.Element)')
  - [GetValue()](#M-VRDR-DeathRecord-GetValue-System-Collections-Generic-Dictionary{System-String,System-String},System-String- 'VRDR.DeathRecord.GetValue(System.Collections.Generic.Dictionary{System.String,System.String},System.String)')
  - [IsDictEmptyOrDefault(dict)](#M-VRDR-DeathRecord-IsDictEmptyOrDefault-System-Collections-Generic-Dictionary{System-String,System-String}- 'VRDR.DeathRecord.IsDictEmptyOrDefault(System.Collections.Generic.Dictionary{System.String,System.String})')
  - [RemoveReferenceFromComposition(reference,code)](#M-VRDR-DeathRecord-RemoveReferenceFromComposition-System-String,System-String- 'VRDR.DeathRecord.RemoveReferenceFromComposition(System.String,System.String)')
  - [RestoreReferences()](#M-VRDR-DeathRecord-RestoreReferences 'VRDR.DeathRecord.RestoreReferences')
  - [SetCodeValue()](#M-VRDR-DeathRecord-SetCodeValue-System-String,System-String,System-String[0-,0-]- 'VRDR.DeathRecord.SetCodeValue(System.String,System.String,System.String[0:,0:])')
  - [SetEmergingIssue()](#M-VRDR-DeathRecord-SetEmergingIssue-System-String,System-String- 'VRDR.DeathRecord.SetEmergingIssue(System.String,System.String)')
  - [SetPartialDate()](#M-VRDR-DeathRecord-SetPartialDate-Hl7-Fhir-Model-Extension,System-String,System-Nullable{System-UInt32}- 'VRDR.DeathRecord.SetPartialDate(Hl7.Fhir.Model.Extension,System.String,System.Nullable{System.UInt32})')
  - [SetPartialTime()](#M-VRDR-DeathRecord-SetPartialTime-Hl7-Fhir-Model-Extension,System-String- 'VRDR.DeathRecord.SetPartialTime(Hl7.Fhir.Model.Extension,System.String)')
  - [ToDescription()](#M-VRDR-DeathRecord-ToDescription 'VRDR.DeathRecord.ToDescription')
  - [ToJSON()](#M-VRDR-DeathRecord-ToJSON 'VRDR.DeathRecord.ToJSON')
  - [ToJson()](#M-VRDR-DeathRecord-ToJson 'VRDR.DeathRecord.ToJson')
  - [ToXML()](#M-VRDR-DeathRecord-ToXML 'VRDR.DeathRecord.ToXML')
  - [ToXml()](#M-VRDR-DeathRecord-ToXml 'VRDR.DeathRecord.ToXml')
  - [UpdateDeathRecordIdentifier()](#M-VRDR-DeathRecord-UpdateDeathRecordIdentifier 'VRDR.DeathRecord.UpdateDeathRecordIdentifier')
  - [UpdateDictionary()](#M-VRDR-DeathRecord-UpdateDictionary-System-Collections-Generic-Dictionary{System-String,System-String},System-Collections-Generic-Dictionary{System-String,System-String}- 'VRDR.DeathRecord.UpdateDictionary(System.Collections.Generic.Dictionary{System.String,System.String},System.Collections.Generic.Dictionary{System.String,System.String})')
- [EditBypass01](#T-VRDR-Mappings-EditBypass01 'VRDR.Mappings.EditBypass01')
- [EditBypass01](#T-VRDR-ValueSets-EditBypass01 'VRDR.ValueSets.EditBypass01')
  - [FHIRToIJE](#F-VRDR-Mappings-EditBypass01-FHIRToIJE 'VRDR.Mappings.EditBypass01.FHIRToIJE')
  - [IJEToFHIR](#F-VRDR-Mappings-EditBypass01-IJEToFHIR 'VRDR.Mappings.EditBypass01.IJEToFHIR')
  - [Codes](#F-VRDR-ValueSets-EditBypass01-Codes 'VRDR.ValueSets.EditBypass01.Codes')
  - [Edit_Failed_Data_Queried_And_Verified](#F-VRDR-ValueSets-EditBypass01-Edit_Failed_Data_Queried_And_Verified 'VRDR.ValueSets.EditBypass01.Edit_Failed_Data_Queried_And_Verified')
  - [Edit_Passed](#F-VRDR-ValueSets-EditBypass01-Edit_Passed 'VRDR.ValueSets.EditBypass01.Edit_Passed')
- [EditBypass012](#T-VRDR-Mappings-EditBypass012 'VRDR.Mappings.EditBypass012')
- [EditBypass012](#T-VRDR-ValueSets-EditBypass012 'VRDR.ValueSets.EditBypass012')
  - [FHIRToIJE](#F-VRDR-Mappings-EditBypass012-FHIRToIJE 'VRDR.Mappings.EditBypass012.FHIRToIJE')
  - [IJEToFHIR](#F-VRDR-Mappings-EditBypass012-IJEToFHIR 'VRDR.Mappings.EditBypass012.IJEToFHIR')
  - [Codes](#F-VRDR-ValueSets-EditBypass012-Codes 'VRDR.ValueSets.EditBypass012.Codes')
  - [Edit_Failed_Data_Queried_And_Verified](#F-VRDR-ValueSets-EditBypass012-Edit_Failed_Data_Queried_And_Verified 'VRDR.ValueSets.EditBypass012.Edit_Failed_Data_Queried_And_Verified')
  - [Edit_Failed_Data_Queried_But_Not_Verified](#F-VRDR-ValueSets-EditBypass012-Edit_Failed_Data_Queried_But_Not_Verified 'VRDR.ValueSets.EditBypass012.Edit_Failed_Data_Queried_But_Not_Verified')
  - [Edit_Passed](#F-VRDR-ValueSets-EditBypass012-Edit_Passed 'VRDR.ValueSets.EditBypass012.Edit_Passed')
- [EditBypass01234](#T-VRDR-Mappings-EditBypass01234 'VRDR.Mappings.EditBypass01234')
- [EditBypass01234](#T-VRDR-ValueSets-EditBypass01234 'VRDR.ValueSets.EditBypass01234')
  - [FHIRToIJE](#F-VRDR-Mappings-EditBypass01234-FHIRToIJE 'VRDR.Mappings.EditBypass01234.FHIRToIJE')
  - [IJEToFHIR](#F-VRDR-Mappings-EditBypass01234-IJEToFHIR 'VRDR.Mappings.EditBypass01234.IJEToFHIR')
  - [Codes](#F-VRDR-ValueSets-EditBypass01234-Codes 'VRDR.ValueSets.EditBypass01234.Codes')
  - [Edit_Failed_Data_Queried_And_Verified](#F-VRDR-ValueSets-EditBypass01234-Edit_Failed_Data_Queried_And_Verified 'VRDR.ValueSets.EditBypass01234.Edit_Failed_Data_Queried_And_Verified')
  - [Edit_Failed_Data_Queried_But_Not_Verified](#F-VRDR-ValueSets-EditBypass01234-Edit_Failed_Data_Queried_But_Not_Verified 'VRDR.ValueSets.EditBypass01234.Edit_Failed_Data_Queried_But_Not_Verified')
  - [Edit_Failed_Query_Needed](#F-VRDR-ValueSets-EditBypass01234-Edit_Failed_Query_Needed 'VRDR.ValueSets.EditBypass01234.Edit_Failed_Query_Needed')
  - [Edit_Failed_Review_Needed](#F-VRDR-ValueSets-EditBypass01234-Edit_Failed_Review_Needed 'VRDR.ValueSets.EditBypass01234.Edit_Failed_Review_Needed')
  - [Edit_Passed](#F-VRDR-ValueSets-EditBypass01234-Edit_Passed 'VRDR.ValueSets.EditBypass01234.Edit_Passed')
- [EditBypass0124](#T-VRDR-Mappings-EditBypass0124 'VRDR.Mappings.EditBypass0124')
- [EditBypass0124](#T-VRDR-ValueSets-EditBypass0124 'VRDR.ValueSets.EditBypass0124')
  - [FHIRToIJE](#F-VRDR-Mappings-EditBypass0124-FHIRToIJE 'VRDR.Mappings.EditBypass0124.FHIRToIJE')
  - [IJEToFHIR](#F-VRDR-Mappings-EditBypass0124-IJEToFHIR 'VRDR.Mappings.EditBypass0124.IJEToFHIR')
  - [Codes](#F-VRDR-ValueSets-EditBypass0124-Codes 'VRDR.ValueSets.EditBypass0124.Codes')
  - [Edit_Failed_Data_Queried_And_Verified](#F-VRDR-ValueSets-EditBypass0124-Edit_Failed_Data_Queried_And_Verified 'VRDR.ValueSets.EditBypass0124.Edit_Failed_Data_Queried_And_Verified')
  - [Edit_Failed_Data_Queried_But_Not_Verified](#F-VRDR-ValueSets-EditBypass0124-Edit_Failed_Data_Queried_But_Not_Verified 'VRDR.ValueSets.EditBypass0124.Edit_Failed_Data_Queried_But_Not_Verified')
  - [Edit_Failed_Query_Needed](#F-VRDR-ValueSets-EditBypass0124-Edit_Failed_Query_Needed 'VRDR.ValueSets.EditBypass0124.Edit_Failed_Query_Needed')
  - [Edit_Passed](#F-VRDR-ValueSets-EditBypass0124-Edit_Passed 'VRDR.ValueSets.EditBypass0124.Edit_Passed')
- [EducationLevel](#T-VRDR-Mappings-EducationLevel 'VRDR.Mappings.EducationLevel')
- [EducationLevel](#T-VRDR-ValueSets-EducationLevel 'VRDR.ValueSets.EducationLevel')
  - [FHIRToIJE](#F-VRDR-Mappings-EducationLevel-FHIRToIJE 'VRDR.Mappings.EducationLevel.FHIRToIJE')
  - [IJEToFHIR](#F-VRDR-Mappings-EducationLevel-IJEToFHIR 'VRDR.Mappings.EducationLevel.IJEToFHIR')
  - [Associates_Or_Technical_Degree_Complete](#F-VRDR-ValueSets-EducationLevel-Associates_Or_Technical_Degree_Complete 'VRDR.ValueSets.EducationLevel.Associates_Or_Technical_Degree_Complete')
  - [Bachelors_Degree](#F-VRDR-ValueSets-EducationLevel-Bachelors_Degree 'VRDR.ValueSets.EducationLevel.Bachelors_Degree')
  - [Codes](#F-VRDR-ValueSets-EducationLevel-Codes 'VRDR.ValueSets.EducationLevel.Codes')
  - [Doctoral_Or_Post_Graduate_Education](#F-VRDR-ValueSets-EducationLevel-Doctoral_Or_Post_Graduate_Education 'VRDR.ValueSets.EducationLevel.Doctoral_Or_Post_Graduate_Education')
  - [Elementary_School](#F-VRDR-ValueSets-EducationLevel-Elementary_School 'VRDR.ValueSets.EducationLevel.Elementary_School')
  - [High_School_Or_Secondary_School_Degree_Complete](#F-VRDR-ValueSets-EducationLevel-High_School_Or_Secondary_School_Degree_Complete 'VRDR.ValueSets.EducationLevel.High_School_Or_Secondary_School_Degree_Complete')
  - [Masters_Degree](#F-VRDR-ValueSets-EducationLevel-Masters_Degree 'VRDR.ValueSets.EducationLevel.Masters_Degree')
  - [Some_College_Education](#F-VRDR-ValueSets-EducationLevel-Some_College_Education 'VRDR.ValueSets.EducationLevel.Some_College_Education')
  - [Some_Secondary_Or_High_School_Education](#F-VRDR-ValueSets-EducationLevel-Some_Secondary_Or_High_School_Education 'VRDR.ValueSets.EducationLevel.Some_Secondary_Or_High_School_Education')
  - [Unknown](#F-VRDR-ValueSets-EducationLevel-Unknown 'VRDR.ValueSets.EducationLevel.Unknown')
- [ExtensionURL](#T-VRDR-ExtensionURL 'VRDR.ExtensionURL')
  - [AuxiliaryStateIdentifier1](#F-VRDR-ExtensionURL-AuxiliaryStateIdentifier1 'VRDR.ExtensionURL.AuxiliaryStateIdentifier1')
  - [AuxiliaryStateIdentifier2](#F-VRDR-ExtensionURL-AuxiliaryStateIdentifier2 'VRDR.ExtensionURL.AuxiliaryStateIdentifier2')
  - [BypassEditFlag](#F-VRDR-ExtensionURL-BypassEditFlag 'VRDR.ExtensionURL.BypassEditFlag')
  - [CertificateNumber](#F-VRDR-ExtensionURL-CertificateNumber 'VRDR.ExtensionURL.CertificateNumber')
  - [CityCode](#F-VRDR-ExtensionURL-CityCode 'VRDR.ExtensionURL.CityCode')
  - [DateDay](#F-VRDR-ExtensionURL-DateDay 'VRDR.ExtensionURL.DateDay')
  - [DateMonth](#F-VRDR-ExtensionURL-DateMonth 'VRDR.ExtensionURL.DateMonth')
  - [DateTime](#F-VRDR-ExtensionURL-DateTime 'VRDR.ExtensionURL.DateTime')
  - [DateYear](#F-VRDR-ExtensionURL-DateYear 'VRDR.ExtensionURL.DateYear')
  - [DistrictCode](#F-VRDR-ExtensionURL-DistrictCode 'VRDR.ExtensionURL.DistrictCode')
  - [FilingFormat](#F-VRDR-ExtensionURL-FilingFormat 'VRDR.ExtensionURL.FilingFormat')
  - [LocationJurisdictionId](#F-VRDR-ExtensionURL-LocationJurisdictionId 'VRDR.ExtensionURL.LocationJurisdictionId')
  - [NVSSSexAtDeath](#F-VRDR-ExtensionURL-NVSSSexAtDeath 'VRDR.ExtensionURL.NVSSSexAtDeath')
  - [PartialDate](#F-VRDR-ExtensionURL-PartialDate 'VRDR.ExtensionURL.PartialDate')
  - [PartialDateTime](#F-VRDR-ExtensionURL-PartialDateTime 'VRDR.ExtensionURL.PartialDateTime')
  - [PostDirectional](#F-VRDR-ExtensionURL-PostDirectional 'VRDR.ExtensionURL.PostDirectional')
  - [PreDirectional](#F-VRDR-ExtensionURL-PreDirectional 'VRDR.ExtensionURL.PreDirectional')
  - [ReplaceStatus](#F-VRDR-ExtensionURL-ReplaceStatus 'VRDR.ExtensionURL.ReplaceStatus')
  - [SpouseAlive](#F-VRDR-ExtensionURL-SpouseAlive 'VRDR.ExtensionURL.SpouseAlive')
  - [StateSpecificField](#F-VRDR-ExtensionURL-StateSpecificField 'VRDR.ExtensionURL.StateSpecificField')
  - [StreetDesignator](#F-VRDR-ExtensionURL-StreetDesignator 'VRDR.ExtensionURL.StreetDesignator')
  - [StreetName](#F-VRDR-ExtensionURL-StreetName 'VRDR.ExtensionURL.StreetName')
  - [StreetNumber](#F-VRDR-ExtensionURL-StreetNumber 'VRDR.ExtensionURL.StreetNumber')
  - [UnitOrAptNumber](#F-VRDR-ExtensionURL-UnitOrAptNumber 'VRDR.ExtensionURL.UnitOrAptNumber')
  - [WithinCityLimitsIndicator](#F-VRDR-ExtensionURL-WithinCityLimitsIndicator 'VRDR.ExtensionURL.WithinCityLimitsIndicator')
- [FHIRPath](#T-VRDR-FHIRPath 'VRDR.FHIRPath')
  - [#ctor()](#M-VRDR-FHIRPath-#ctor-System-String,System-String- 'VRDR.FHIRPath.#ctor(System.String,System.String)')
  - [Element](#F-VRDR-FHIRPath-Element 'VRDR.FHIRPath.Element')
  - [Path](#F-VRDR-FHIRPath-Path 'VRDR.FHIRPath.Path')
- [FilingFormat](#T-VRDR-Mappings-FilingFormat 'VRDR.Mappings.FilingFormat')
- [FilingFormat](#T-VRDR-ValueSets-FilingFormat 'VRDR.ValueSets.FilingFormat')
  - [FHIRToIJE](#F-VRDR-Mappings-FilingFormat-FHIRToIJE 'VRDR.Mappings.FilingFormat.FHIRToIJE')
  - [IJEToFHIR](#F-VRDR-Mappings-FilingFormat-IJEToFHIR 'VRDR.Mappings.FilingFormat.IJEToFHIR')
  - [Codes](#F-VRDR-ValueSets-FilingFormat-Codes 'VRDR.ValueSets.FilingFormat.Codes')
  - [Electronic](#F-VRDR-ValueSets-FilingFormat-Electronic 'VRDR.ValueSets.FilingFormat.Electronic')
  - [Mixed](#F-VRDR-ValueSets-FilingFormat-Mixed 'VRDR.ValueSets.FilingFormat.Mixed')
  - [Paper](#F-VRDR-ValueSets-FilingFormat-Paper 'VRDR.ValueSets.FilingFormat.Paper')
- [HispanicNoUnknown](#T-VRDR-Mappings-HispanicNoUnknown 'VRDR.Mappings.HispanicNoUnknown')
- [HispanicNoUnknown](#T-VRDR-ValueSets-HispanicNoUnknown 'VRDR.ValueSets.HispanicNoUnknown')
  - [FHIRToIJE](#F-VRDR-Mappings-HispanicNoUnknown-FHIRToIJE 'VRDR.Mappings.HispanicNoUnknown.FHIRToIJE')
  - [IJEToFHIR](#F-VRDR-Mappings-HispanicNoUnknown-IJEToFHIR 'VRDR.Mappings.HispanicNoUnknown.IJEToFHIR')
  - [Codes](#F-VRDR-ValueSets-HispanicNoUnknown-Codes 'VRDR.ValueSets.HispanicNoUnknown.Codes')
  - [No](#F-VRDR-ValueSets-HispanicNoUnknown-No 'VRDR.ValueSets.HispanicNoUnknown.No')
  - [Unknown](#F-VRDR-ValueSets-HispanicNoUnknown-Unknown 'VRDR.ValueSets.HispanicNoUnknown.Unknown')
  - [Yes](#F-VRDR-ValueSets-HispanicNoUnknown-Yes 'VRDR.ValueSets.HispanicNoUnknown.Yes')
- [HispanicOrigin](#T-VRDR-Mappings-HispanicOrigin 'VRDR.Mappings.HispanicOrigin')
- [HispanicOrigin](#T-VRDR-ValueSets-HispanicOrigin 'VRDR.ValueSets.HispanicOrigin')
  - [FHIRToIJE](#F-VRDR-Mappings-HispanicOrigin-FHIRToIJE 'VRDR.Mappings.HispanicOrigin.FHIRToIJE')
  - [IJEToFHIR](#F-VRDR-Mappings-HispanicOrigin-IJEToFHIR 'VRDR.Mappings.HispanicOrigin.IJEToFHIR')
  - [Andalusian](#F-VRDR-ValueSets-HispanicOrigin-Andalusian 'VRDR.ValueSets.HispanicOrigin.Andalusian')
  - [Argentinean](#F-VRDR-ValueSets-HispanicOrigin-Argentinean 'VRDR.ValueSets.HispanicOrigin.Argentinean')
  - [Asturian](#F-VRDR-ValueSets-HispanicOrigin-Asturian 'VRDR.ValueSets.HispanicOrigin.Asturian')
  - [Balearic_Islander](#F-VRDR-ValueSets-HispanicOrigin-Balearic_Islander 'VRDR.ValueSets.HispanicOrigin.Balearic_Islander')
  - [Bolivian](#F-VRDR-ValueSets-HispanicOrigin-Bolivian 'VRDR.ValueSets.HispanicOrigin.Bolivian')
  - [Californio](#F-VRDR-ValueSets-HispanicOrigin-Californio 'VRDR.ValueSets.HispanicOrigin.Californio')
  - [Canal_Zone](#F-VRDR-ValueSets-HispanicOrigin-Canal_Zone 'VRDR.ValueSets.HispanicOrigin.Canal_Zone')
  - [Canarian](#F-VRDR-ValueSets-HispanicOrigin-Canarian 'VRDR.ValueSets.HispanicOrigin.Canarian')
  - [Caribbean](#F-VRDR-ValueSets-HispanicOrigin-Caribbean 'VRDR.ValueSets.HispanicOrigin.Caribbean')
  - [Castillian](#F-VRDR-ValueSets-HispanicOrigin-Castillian 'VRDR.ValueSets.HispanicOrigin.Castillian')
  - [Catalonian](#F-VRDR-ValueSets-HispanicOrigin-Catalonian 'VRDR.ValueSets.HispanicOrigin.Catalonian')
  - [Central_American](#F-VRDR-ValueSets-HispanicOrigin-Central_American 'VRDR.ValueSets.HispanicOrigin.Central_American')
  - [Central_American_Indian](#F-VRDR-ValueSets-HispanicOrigin-Central_American_Indian 'VRDR.ValueSets.HispanicOrigin.Central_American_Indian')
  - [Central_And_South_America](#F-VRDR-ValueSets-HispanicOrigin-Central_And_South_America 'VRDR.ValueSets.HispanicOrigin.Central_And_South_America')
  - [Chicano](#F-VRDR-ValueSets-HispanicOrigin-Chicano 'VRDR.ValueSets.HispanicOrigin.Chicano')
  - [Chilean](#F-VRDR-ValueSets-HispanicOrigin-Chilean 'VRDR.ValueSets.HispanicOrigin.Chilean')
  - [Codes](#F-VRDR-ValueSets-HispanicOrigin-Codes 'VRDR.ValueSets.HispanicOrigin.Codes')
  - [Colombian](#F-VRDR-ValueSets-HispanicOrigin-Colombian 'VRDR.ValueSets.HispanicOrigin.Colombian')
  - [Costa_Rican](#F-VRDR-ValueSets-HispanicOrigin-Costa_Rican 'VRDR.ValueSets.HispanicOrigin.Costa_Rican')
  - [Criollo](#F-VRDR-ValueSets-HispanicOrigin-Criollo 'VRDR.ValueSets.HispanicOrigin.Criollo')
  - [Cuban](#F-VRDR-ValueSets-HispanicOrigin-Cuban 'VRDR.ValueSets.HispanicOrigin.Cuban')
  - [Cuban_2](#F-VRDR-ValueSets-HispanicOrigin-Cuban_2 'VRDR.ValueSets.HispanicOrigin.Cuban_2')
  - [Deferred](#F-VRDR-ValueSets-HispanicOrigin-Deferred 'VRDR.ValueSets.HispanicOrigin.Deferred')
  - [Dominican](#F-VRDR-ValueSets-HispanicOrigin-Dominican 'VRDR.ValueSets.HispanicOrigin.Dominican')
  - [Ecuadorian](#F-VRDR-ValueSets-HispanicOrigin-Ecuadorian 'VRDR.ValueSets.HispanicOrigin.Ecuadorian')
  - [First_Pass_Reject](#F-VRDR-ValueSets-HispanicOrigin-First_Pass_Reject 'VRDR.ValueSets.HispanicOrigin.First_Pass_Reject')
  - [Gallego](#F-VRDR-ValueSets-HispanicOrigin-Gallego 'VRDR.ValueSets.HispanicOrigin.Gallego')
  - [Guatemalan](#F-VRDR-ValueSets-HispanicOrigin-Guatemalan 'VRDR.ValueSets.HispanicOrigin.Guatemalan')
  - [Hispanic](#F-VRDR-ValueSets-HispanicOrigin-Hispanic 'VRDR.ValueSets.HispanicOrigin.Hispanic')
  - [Honduran](#F-VRDR-ValueSets-HispanicOrigin-Honduran 'VRDR.ValueSets.HispanicOrigin.Honduran')
  - [La_Raza](#F-VRDR-ValueSets-HispanicOrigin-La_Raza 'VRDR.ValueSets.HispanicOrigin.La_Raza')
  - [Latin](#F-VRDR-ValueSets-HispanicOrigin-Latin 'VRDR.ValueSets.HispanicOrigin.Latin')
  - [Latin_American](#F-VRDR-ValueSets-HispanicOrigin-Latin_American 'VRDR.ValueSets.HispanicOrigin.Latin_American')
  - [Latino](#F-VRDR-ValueSets-HispanicOrigin-Latino 'VRDR.ValueSets.HispanicOrigin.Latino')
  - [Meso_American_Indian](#F-VRDR-ValueSets-HispanicOrigin-Meso_American_Indian 'VRDR.ValueSets.HispanicOrigin.Meso_American_Indian')
  - [Mestizo](#F-VRDR-ValueSets-HispanicOrigin-Mestizo 'VRDR.ValueSets.HispanicOrigin.Mestizo')
  - [Mexican](#F-VRDR-ValueSets-HispanicOrigin-Mexican 'VRDR.ValueSets.HispanicOrigin.Mexican')
  - [Mexican_2](#F-VRDR-ValueSets-HispanicOrigin-Mexican_2 'VRDR.ValueSets.HispanicOrigin.Mexican_2')
  - [Mexican_American](#F-VRDR-ValueSets-HispanicOrigin-Mexican_American 'VRDR.ValueSets.HispanicOrigin.Mexican_American')
  - [Mexican_American_Indian](#F-VRDR-ValueSets-HispanicOrigin-Mexican_American_Indian 'VRDR.ValueSets.HispanicOrigin.Mexican_American_Indian')
  - [Mexicano](#F-VRDR-ValueSets-HispanicOrigin-Mexicano 'VRDR.ValueSets.HispanicOrigin.Mexicano')
  - [Mexico](#F-VRDR-ValueSets-HispanicOrigin-Mexico 'VRDR.ValueSets.HispanicOrigin.Mexico')
  - [Multiple_Hispanic_Responses](#F-VRDR-ValueSets-HispanicOrigin-Multiple_Hispanic_Responses 'VRDR.ValueSets.HispanicOrigin.Multiple_Hispanic_Responses')
  - [Nicaraguan](#F-VRDR-ValueSets-HispanicOrigin-Nicaraguan 'VRDR.ValueSets.HispanicOrigin.Nicaraguan')
  - [Not_Hispanic](#F-VRDR-ValueSets-HispanicOrigin-Not_Hispanic 'VRDR.ValueSets.HispanicOrigin.Not_Hispanic')
  - [Nuevo_Mexicano](#F-VRDR-ValueSets-HispanicOrigin-Nuevo_Mexicano 'VRDR.ValueSets.HispanicOrigin.Nuevo_Mexicano')
  - [Other_Spanish](#F-VRDR-ValueSets-HispanicOrigin-Other_Spanish 'VRDR.ValueSets.HispanicOrigin.Other_Spanish')
  - [Other_Spanish_2](#F-VRDR-ValueSets-HispanicOrigin-Other_Spanish_2 'VRDR.ValueSets.HispanicOrigin.Other_Spanish_2')
  - [Panamanian](#F-VRDR-ValueSets-HispanicOrigin-Panamanian 'VRDR.ValueSets.HispanicOrigin.Panamanian')
  - [Paraguayan](#F-VRDR-ValueSets-HispanicOrigin-Paraguayan 'VRDR.ValueSets.HispanicOrigin.Paraguayan')
  - [Peruvian](#F-VRDR-ValueSets-HispanicOrigin-Peruvian 'VRDR.ValueSets.HispanicOrigin.Peruvian')
  - [Puerto_Rican](#F-VRDR-ValueSets-HispanicOrigin-Puerto_Rican 'VRDR.ValueSets.HispanicOrigin.Puerto_Rican')
  - [Puerto_Rican_2](#F-VRDR-ValueSets-HispanicOrigin-Puerto_Rican_2 'VRDR.ValueSets.HispanicOrigin.Puerto_Rican_2')
  - [Salvadoran](#F-VRDR-ValueSets-HispanicOrigin-Salvadoran 'VRDR.ValueSets.HispanicOrigin.Salvadoran')
  - [South_American](#F-VRDR-ValueSets-HispanicOrigin-South_American 'VRDR.ValueSets.HispanicOrigin.South_American')
  - [South_American_Indian](#F-VRDR-ValueSets-HispanicOrigin-South_American_Indian 'VRDR.ValueSets.HispanicOrigin.South_American_Indian')
  - [Spaniard](#F-VRDR-ValueSets-HispanicOrigin-Spaniard 'VRDR.ValueSets.HispanicOrigin.Spaniard')
  - [Spanish](#F-VRDR-ValueSets-HispanicOrigin-Spanish 'VRDR.ValueSets.HispanicOrigin.Spanish')
  - [Spanish_American](#F-VRDR-ValueSets-HispanicOrigin-Spanish_American 'VRDR.ValueSets.HispanicOrigin.Spanish_American')
  - [Spanish_American_Indian](#F-VRDR-ValueSets-HispanicOrigin-Spanish_American_Indian 'VRDR.ValueSets.HispanicOrigin.Spanish_American_Indian')
  - [Spanish_Basque](#F-VRDR-ValueSets-HispanicOrigin-Spanish_Basque 'VRDR.ValueSets.HispanicOrigin.Spanish_Basque')
  - [Tejano](#F-VRDR-ValueSets-HispanicOrigin-Tejano 'VRDR.ValueSets.HispanicOrigin.Tejano')
  - [Uncodable](#F-VRDR-ValueSets-HispanicOrigin-Uncodable 'VRDR.ValueSets.HispanicOrigin.Uncodable')
  - [Unknown](#F-VRDR-ValueSets-HispanicOrigin-Unknown 'VRDR.ValueSets.HispanicOrigin.Unknown')
  - [Uruguayan](#F-VRDR-ValueSets-HispanicOrigin-Uruguayan 'VRDR.ValueSets.HispanicOrigin.Uruguayan')
  - [Valencian](#F-VRDR-ValueSets-HispanicOrigin-Valencian 'VRDR.ValueSets.HispanicOrigin.Valencian')
  - [Venezuelan](#F-VRDR-ValueSets-HispanicOrigin-Venezuelan 'VRDR.ValueSets.HispanicOrigin.Venezuelan')
- [IGURL](#T-VRDR-IGURL 'VRDR.IGURL')
  - [ActivityAtTimeOfDeath](#F-VRDR-IGURL-ActivityAtTimeOfDeath 'VRDR.IGURL.ActivityAtTimeOfDeath')
  - [AutomatedUnderlyingCauseOfDeath](#F-VRDR-IGURL-AutomatedUnderlyingCauseOfDeath 'VRDR.IGURL.AutomatedUnderlyingCauseOfDeath')
  - [AutopsyPerformedIndicator](#F-VRDR-IGURL-AutopsyPerformedIndicator 'VRDR.IGURL.AutopsyPerformedIndicator')
  - [AuxiliaryStateIdentifier1](#F-VRDR-IGURL-AuxiliaryStateIdentifier1 'VRDR.IGURL.AuxiliaryStateIdentifier1')
  - [AuxiliaryStateIdentifier2](#F-VRDR-IGURL-AuxiliaryStateIdentifier2 'VRDR.IGURL.AuxiliaryStateIdentifier2')
  - [BirthRecordIdentifier](#F-VRDR-IGURL-BirthRecordIdentifier 'VRDR.IGURL.BirthRecordIdentifier')
  - [BypassEditFlag](#F-VRDR-IGURL-BypassEditFlag 'VRDR.IGURL.BypassEditFlag')
  - [CauseOfDeathCodedContentBundle](#F-VRDR-IGURL-CauseOfDeathCodedContentBundle 'VRDR.IGURL.CauseOfDeathCodedContentBundle')
  - [CauseOfDeathPart1](#F-VRDR-IGURL-CauseOfDeathPart1 'VRDR.IGURL.CauseOfDeathPart1')
  - [CauseOfDeathPart2](#F-VRDR-IGURL-CauseOfDeathPart2 'VRDR.IGURL.CauseOfDeathPart2')
  - [CauseOfDeathPathway](#F-VRDR-IGURL-CauseOfDeathPathway 'VRDR.IGURL.CauseOfDeathPathway')
  - [CertificateNumber](#F-VRDR-IGURL-CertificateNumber 'VRDR.IGURL.CertificateNumber')
  - [Certifier](#F-VRDR-IGURL-Certifier 'VRDR.IGURL.Certifier')
  - [CityCode](#F-VRDR-IGURL-CityCode 'VRDR.IGURL.CityCode')
  - [CodedRaceAndEthnicity](#F-VRDR-IGURL-CodedRaceAndEthnicity 'VRDR.IGURL.CodedRaceAndEthnicity')
  - [CodingStatusValues](#F-VRDR-IGURL-CodingStatusValues 'VRDR.IGURL.CodingStatusValues')
  - [DateDay](#F-VRDR-IGURL-DateDay 'VRDR.IGURL.DateDay')
  - [DateMonth](#F-VRDR-IGURL-DateMonth 'VRDR.IGURL.DateMonth')
  - [DateTime](#F-VRDR-IGURL-DateTime 'VRDR.IGURL.DateTime')
  - [DateYear](#F-VRDR-IGURL-DateYear 'VRDR.IGURL.DateYear')
  - [DeathCertificate](#F-VRDR-IGURL-DeathCertificate 'VRDR.IGURL.DeathCertificate')
  - [DeathCertificateDocument](#F-VRDR-IGURL-DeathCertificateDocument 'VRDR.IGURL.DeathCertificateDocument')
  - [DeathCertification](#F-VRDR-IGURL-DeathCertification 'VRDR.IGURL.DeathCertification')
  - [DeathDate](#F-VRDR-IGURL-DeathDate 'VRDR.IGURL.DeathDate')
  - [DeathLocation](#F-VRDR-IGURL-DeathLocation 'VRDR.IGURL.DeathLocation')
  - [Decedent](#F-VRDR-IGURL-Decedent 'VRDR.IGURL.Decedent')
  - [DecedentAge](#F-VRDR-IGURL-DecedentAge 'VRDR.IGURL.DecedentAge')
  - [DecedentDispositionMethod](#F-VRDR-IGURL-DecedentDispositionMethod 'VRDR.IGURL.DecedentDispositionMethod')
  - [DecedentEducationLevel](#F-VRDR-IGURL-DecedentEducationLevel 'VRDR.IGURL.DecedentEducationLevel')
  - [DecedentFather](#F-VRDR-IGURL-DecedentFather 'VRDR.IGURL.DecedentFather')
  - [DecedentMilitaryService](#F-VRDR-IGURL-DecedentMilitaryService 'VRDR.IGURL.DecedentMilitaryService')
  - [DecedentMother](#F-VRDR-IGURL-DecedentMother 'VRDR.IGURL.DecedentMother')
  - [DecedentPregnancyStatus](#F-VRDR-IGURL-DecedentPregnancyStatus 'VRDR.IGURL.DecedentPregnancyStatus')
  - [DecedentSpouse](#F-VRDR-IGURL-DecedentSpouse 'VRDR.IGURL.DecedentSpouse')
  - [DecedentUsualWork](#F-VRDR-IGURL-DecedentUsualWork 'VRDR.IGURL.DecedentUsualWork')
  - [DemographicCodedContentBundle](#F-VRDR-IGURL-DemographicCodedContentBundle 'VRDR.IGURL.DemographicCodedContentBundle')
  - [DispositionLocation](#F-VRDR-IGURL-DispositionLocation 'VRDR.IGURL.DispositionLocation')
  - [DistrictCode](#F-VRDR-IGURL-DistrictCode 'VRDR.IGURL.DistrictCode')
  - [EmergingIssues](#F-VRDR-IGURL-EmergingIssues 'VRDR.IGURL.EmergingIssues')
  - [EntityAxisCauseOfDeath](#F-VRDR-IGURL-EntityAxisCauseOfDeath 'VRDR.IGURL.EntityAxisCauseOfDeath')
  - [ExaminerContacted](#F-VRDR-IGURL-ExaminerContacted 'VRDR.IGURL.ExaminerContacted')
  - [FilingFormat](#F-VRDR-IGURL-FilingFormat 'VRDR.IGURL.FilingFormat')
  - [FuneralHome](#F-VRDR-IGURL-FuneralHome 'VRDR.IGURL.FuneralHome')
  - [InjuryIncident](#F-VRDR-IGURL-InjuryIncident 'VRDR.IGURL.InjuryIncident')
  - [InjuryLocation](#F-VRDR-IGURL-InjuryLocation 'VRDR.IGURL.InjuryLocation')
  - [InputRaceAndEthnicity](#F-VRDR-IGURL-InputRaceAndEthnicity 'VRDR.IGURL.InputRaceAndEthnicity')
  - [LocationJurisdictionId](#F-VRDR-IGURL-LocationJurisdictionId 'VRDR.IGURL.LocationJurisdictionId')
  - [MannerOfDeath](#F-VRDR-IGURL-MannerOfDeath 'VRDR.IGURL.MannerOfDeath')
  - [ManualUnderlyingCauseOfDeath](#F-VRDR-IGURL-ManualUnderlyingCauseOfDeath 'VRDR.IGURL.ManualUnderlyingCauseOfDeath')
  - [NVSSSexAtDeath](#F-VRDR-IGURL-NVSSSexAtDeath 'VRDR.IGURL.NVSSSexAtDeath')
  - [PartialDate](#F-VRDR-IGURL-PartialDate 'VRDR.IGURL.PartialDate')
  - [PartialDateTime](#F-VRDR-IGURL-PartialDateTime 'VRDR.IGURL.PartialDateTime')
  - [PlaceOfInjury](#F-VRDR-IGURL-PlaceOfInjury 'VRDR.IGURL.PlaceOfInjury')
  - [PostDirectional](#F-VRDR-IGURL-PostDirectional 'VRDR.IGURL.PostDirectional')
  - [PreDirectional](#F-VRDR-IGURL-PreDirectional 'VRDR.IGURL.PreDirectional')
  - [RecordAxisCauseOfDeath](#F-VRDR-IGURL-RecordAxisCauseOfDeath 'VRDR.IGURL.RecordAxisCauseOfDeath')
  - [ReplaceStatus](#F-VRDR-IGURL-ReplaceStatus 'VRDR.IGURL.ReplaceStatus')
  - [SpouseAlive](#F-VRDR-IGURL-SpouseAlive 'VRDR.IGURL.SpouseAlive')
  - [StateSpecificField](#F-VRDR-IGURL-StateSpecificField 'VRDR.IGURL.StateSpecificField')
  - [StreetDesignator](#F-VRDR-IGURL-StreetDesignator 'VRDR.IGURL.StreetDesignator')
  - [StreetName](#F-VRDR-IGURL-StreetName 'VRDR.IGURL.StreetName')
  - [StreetNumber](#F-VRDR-IGURL-StreetNumber 'VRDR.IGURL.StreetNumber')
  - [SurgeryDate](#F-VRDR-IGURL-SurgeryDate 'VRDR.IGURL.SurgeryDate')
  - [TobaccoUseContributedToDeath](#F-VRDR-IGURL-TobaccoUseContributedToDeath 'VRDR.IGURL.TobaccoUseContributedToDeath')
  - [UnitOrAptNumber](#F-VRDR-IGURL-UnitOrAptNumber 'VRDR.IGURL.UnitOrAptNumber')
  - [WithinCityLimitsIndicator](#F-VRDR-IGURL-WithinCityLimitsIndicator 'VRDR.IGURL.WithinCityLimitsIndicator')
- [IJEField](#T-VRDR-IJEField 'VRDR.IJEField')
  - [#ctor()](#M-VRDR-IJEField-#ctor-System-Int32,System-Int32,System-Int32,System-String,System-String,System-Int32- 'VRDR.IJEField.#ctor(System.Int32,System.Int32,System.Int32,System.String,System.String,System.Int32)')
  - [Contents](#F-VRDR-IJEField-Contents 'VRDR.IJEField.Contents')
  - [Field](#F-VRDR-IJEField-Field 'VRDR.IJEField.Field')
  - [Length](#F-VRDR-IJEField-Length 'VRDR.IJEField.Length')
  - [Location](#F-VRDR-IJEField-Location 'VRDR.IJEField.Location')
  - [Name](#F-VRDR-IJEField-Name 'VRDR.IJEField.Name')
  - [Priority](#F-VRDR-IJEField-Priority 'VRDR.IJEField.Priority')
- [IJEMortality](#T-VRDR-IJEMortality 'VRDR.IJEMortality')
  - [#ctor()](#M-VRDR-IJEMortality-#ctor-VRDR-DeathRecord,System-Boolean- 'VRDR.IJEMortality.#ctor(VRDR.DeathRecord,System.Boolean)')
  - [#ctor()](#M-VRDR-IJEMortality-#ctor-System-String,System-Boolean- 'VRDR.IJEMortality.#ctor(System.String,System.Boolean)')
  - [#ctor()](#M-VRDR-IJEMortality-#ctor 'VRDR.IJEMortality.#ctor')
  - [dataLookup](#F-VRDR-IJEMortality-dataLookup 'VRDR.IJEMortality.dataLookup')
  - [mre](#F-VRDR-IJEMortality-mre 'VRDR.IJEMortality.mre')
  - [record](#F-VRDR-IJEMortality-record 'VRDR.IJEMortality.record')
  - [trx](#F-VRDR-IJEMortality-trx 'VRDR.IJEMortality.trx')
  - [validationErrors](#F-VRDR-IJEMortality-validationErrors 'VRDR.IJEMortality.validationErrors')
  - [ACME_UC](#P-VRDR-IJEMortality-ACME_UC 'VRDR.IJEMortality.ACME_UC')
  - [ADDRESS_D](#P-VRDR-IJEMortality-ADDRESS_D 'VRDR.IJEMortality.ADDRESS_D')
  - [ADDRESS_R](#P-VRDR-IJEMortality-ADDRESS_R 'VRDR.IJEMortality.ADDRESS_R')
  - [AGE](#P-VRDR-IJEMortality-AGE 'VRDR.IJEMortality.AGE')
  - [AGETYPE](#P-VRDR-IJEMortality-AGETYPE 'VRDR.IJEMortality.AGETYPE')
  - [AGE_BYPASS](#P-VRDR-IJEMortality-AGE_BYPASS 'VRDR.IJEMortality.AGE_BYPASS')
  - [ALIAS](#P-VRDR-IJEMortality-ALIAS 'VRDR.IJEMortality.ALIAS')
  - [ARMEDF](#P-VRDR-IJEMortality-ARMEDF 'VRDR.IJEMortality.ARMEDF')
  - [AUTOP](#P-VRDR-IJEMortality-AUTOP 'VRDR.IJEMortality.AUTOP')
  - [AUTOPF](#P-VRDR-IJEMortality-AUTOPF 'VRDR.IJEMortality.AUTOPF')
  - [AUXNO](#P-VRDR-IJEMortality-AUXNO 'VRDR.IJEMortality.AUXNO')
  - [AUXNO2](#P-VRDR-IJEMortality-AUXNO2 'VRDR.IJEMortality.AUXNO2')
  - [BCNO](#P-VRDR-IJEMortality-BCNO 'VRDR.IJEMortality.BCNO')
  - [BLANK1](#P-VRDR-IJEMortality-BLANK1 'VRDR.IJEMortality.BLANK1')
  - [BLANK2](#P-VRDR-IJEMortality-BLANK2 'VRDR.IJEMortality.BLANK2')
  - [BLANK3](#P-VRDR-IJEMortality-BLANK3 'VRDR.IJEMortality.BLANK3')
  - [BPLACE_CNT](#P-VRDR-IJEMortality-BPLACE_CNT 'VRDR.IJEMortality.BPLACE_CNT')
  - [BPLACE_ST](#P-VRDR-IJEMortality-BPLACE_ST 'VRDR.IJEMortality.BPLACE_ST')
  - [BSTATE](#P-VRDR-IJEMortality-BSTATE 'VRDR.IJEMortality.BSTATE')
  - [CERTADDRESS](#P-VRDR-IJEMortality-CERTADDRESS 'VRDR.IJEMortality.CERTADDRESS')
  - [CERTCITYTEXT](#P-VRDR-IJEMortality-CERTCITYTEXT 'VRDR.IJEMortality.CERTCITYTEXT')
  - [CERTDATE](#P-VRDR-IJEMortality-CERTDATE 'VRDR.IJEMortality.CERTDATE')
  - [CERTFIRST](#P-VRDR-IJEMortality-CERTFIRST 'VRDR.IJEMortality.CERTFIRST')
  - [CERTL](#P-VRDR-IJEMortality-CERTL 'VRDR.IJEMortality.CERTL')
  - [CERTLAST](#P-VRDR-IJEMortality-CERTLAST 'VRDR.IJEMortality.CERTLAST')
  - [CERTMIDDLE](#P-VRDR-IJEMortality-CERTMIDDLE 'VRDR.IJEMortality.CERTMIDDLE')
  - [CERTPOSTDIR](#P-VRDR-IJEMortality-CERTPOSTDIR 'VRDR.IJEMortality.CERTPOSTDIR')
  - [CERTPREDIR](#P-VRDR-IJEMortality-CERTPREDIR 'VRDR.IJEMortality.CERTPREDIR')
  - [CERTSTATE](#P-VRDR-IJEMortality-CERTSTATE 'VRDR.IJEMortality.CERTSTATE')
  - [CERTSTATECD](#P-VRDR-IJEMortality-CERTSTATECD 'VRDR.IJEMortality.CERTSTATECD')
  - [CERTSTNUM](#P-VRDR-IJEMortality-CERTSTNUM 'VRDR.IJEMortality.CERTSTNUM')
  - [CERTSTRDESIG](#P-VRDR-IJEMortality-CERTSTRDESIG 'VRDR.IJEMortality.CERTSTRDESIG')
  - [CERTSTRNAME](#P-VRDR-IJEMortality-CERTSTRNAME 'VRDR.IJEMortality.CERTSTRNAME')
  - [CERTSUFFIX](#P-VRDR-IJEMortality-CERTSUFFIX 'VRDR.IJEMortality.CERTSUFFIX')
  - [CERTUNITNUM](#P-VRDR-IJEMortality-CERTUNITNUM 'VRDR.IJEMortality.CERTUNITNUM')
  - [CERTZIP](#P-VRDR-IJEMortality-CERTZIP 'VRDR.IJEMortality.CERTZIP')
  - [CITYC](#P-VRDR-IJEMortality-CITYC 'VRDR.IJEMortality.CITYC')
  - [CITYCODE_D](#P-VRDR-IJEMortality-CITYCODE_D 'VRDR.IJEMortality.CITYCODE_D')
  - [CITYCODE_I](#P-VRDR-IJEMortality-CITYCODE_I 'VRDR.IJEMortality.CITYCODE_I')
  - [CITYTEXT_D](#P-VRDR-IJEMortality-CITYTEXT_D 'VRDR.IJEMortality.CITYTEXT_D')
  - [CITYTEXT_I](#P-VRDR-IJEMortality-CITYTEXT_I 'VRDR.IJEMortality.CITYTEXT_I')
  - [CITYTEXT_R](#P-VRDR-IJEMortality-CITYTEXT_R 'VRDR.IJEMortality.CITYTEXT_R')
  - [COD](#P-VRDR-IJEMortality-COD 'VRDR.IJEMortality.COD')
  - [COD1A](#P-VRDR-IJEMortality-COD1A 'VRDR.IJEMortality.COD1A')
  - [COD1B](#P-VRDR-IJEMortality-COD1B 'VRDR.IJEMortality.COD1B')
  - [COD1C](#P-VRDR-IJEMortality-COD1C 'VRDR.IJEMortality.COD1C')
  - [COD1D](#P-VRDR-IJEMortality-COD1D 'VRDR.IJEMortality.COD1D')
  - [COUNTRYC](#P-VRDR-IJEMortality-COUNTRYC 'VRDR.IJEMortality.COUNTRYC')
  - [COUNTRYTEXT_R](#P-VRDR-IJEMortality-COUNTRYTEXT_R 'VRDR.IJEMortality.COUNTRYTEXT_R')
  - [COUNTYC](#P-VRDR-IJEMortality-COUNTYC 'VRDR.IJEMortality.COUNTYC')
  - [COUNTYCODE_I](#P-VRDR-IJEMortality-COUNTYCODE_I 'VRDR.IJEMortality.COUNTYCODE_I')
  - [COUNTYTEXT_D](#P-VRDR-IJEMortality-COUNTYTEXT_D 'VRDR.IJEMortality.COUNTYTEXT_D')
  - [COUNTYTEXT_I](#P-VRDR-IJEMortality-COUNTYTEXT_I 'VRDR.IJEMortality.COUNTYTEXT_I')
  - [COUNTYTEXT_R](#P-VRDR-IJEMortality-COUNTYTEXT_R 'VRDR.IJEMortality.COUNTYTEXT_R')
  - [DBPLACECITY](#P-VRDR-IJEMortality-DBPLACECITY 'VRDR.IJEMortality.DBPLACECITY')
  - [DBPLACECITYCODE](#P-VRDR-IJEMortality-DBPLACECITYCODE 'VRDR.IJEMortality.DBPLACECITYCODE')
  - [DDADF](#P-VRDR-IJEMortality-DDADF 'VRDR.IJEMortality.DDADF')
  - [DDADMID](#P-VRDR-IJEMortality-DDADMID 'VRDR.IJEMortality.DDADMID')
  - [DEDUC](#P-VRDR-IJEMortality-DEDUC 'VRDR.IJEMortality.DEDUC')
  - [DEDUC_BYPASS](#P-VRDR-IJEMortality-DEDUC_BYPASS 'VRDR.IJEMortality.DEDUC_BYPASS')
  - [DETHNIC1](#P-VRDR-IJEMortality-DETHNIC1 'VRDR.IJEMortality.DETHNIC1')
  - [DETHNIC2](#P-VRDR-IJEMortality-DETHNIC2 'VRDR.IJEMortality.DETHNIC2')
  - [DETHNIC3](#P-VRDR-IJEMortality-DETHNIC3 'VRDR.IJEMortality.DETHNIC3')
  - [DETHNIC4](#P-VRDR-IJEMortality-DETHNIC4 'VRDR.IJEMortality.DETHNIC4')
  - [DETHNIC5](#P-VRDR-IJEMortality-DETHNIC5 'VRDR.IJEMortality.DETHNIC5')
  - [DETHNIC5C](#P-VRDR-IJEMortality-DETHNIC5C 'VRDR.IJEMortality.DETHNIC5C')
  - [DETHNICE](#P-VRDR-IJEMortality-DETHNICE 'VRDR.IJEMortality.DETHNICE')
  - [DINSTI](#P-VRDR-IJEMortality-DINSTI 'VRDR.IJEMortality.DINSTI')
  - [DISP](#P-VRDR-IJEMortality-DISP 'VRDR.IJEMortality.DISP')
  - [DISPCITY](#P-VRDR-IJEMortality-DISPCITY 'VRDR.IJEMortality.DISPCITY')
  - [DISPCITYCODE](#P-VRDR-IJEMortality-DISPCITYCODE 'VRDR.IJEMortality.DISPCITYCODE')
  - [DISPSTATE](#P-VRDR-IJEMortality-DISPSTATE 'VRDR.IJEMortality.DISPSTATE')
  - [DISPSTATECD](#P-VRDR-IJEMortality-DISPSTATECD 'VRDR.IJEMortality.DISPSTATECD')
  - [DMAIDEN](#P-VRDR-IJEMortality-DMAIDEN 'VRDR.IJEMortality.DMAIDEN')
  - [DMIDDLE](#P-VRDR-IJEMortality-DMIDDLE 'VRDR.IJEMortality.DMIDDLE')
  - [DMOMF](#P-VRDR-IJEMortality-DMOMF 'VRDR.IJEMortality.DMOMF')
  - [DMOMMDN](#P-VRDR-IJEMortality-DMOMMDN 'VRDR.IJEMortality.DMOMMDN')
  - [DMOMMID](#P-VRDR-IJEMortality-DMOMMID 'VRDR.IJEMortality.DMOMMID')
  - [DOB_DY](#P-VRDR-IJEMortality-DOB_DY 'VRDR.IJEMortality.DOB_DY')
  - [DOB_MO](#P-VRDR-IJEMortality-DOB_MO 'VRDR.IJEMortality.DOB_MO')
  - [DOB_YR](#P-VRDR-IJEMortality-DOB_YR 'VRDR.IJEMortality.DOB_YR')
  - [DOD_DY](#P-VRDR-IJEMortality-DOD_DY 'VRDR.IJEMortality.DOD_DY')
  - [DOD_MO](#P-VRDR-IJEMortality-DOD_MO 'VRDR.IJEMortality.DOD_MO')
  - [DOD_YR](#P-VRDR-IJEMortality-DOD_YR 'VRDR.IJEMortality.DOD_YR')
  - [DOI_DY](#P-VRDR-IJEMortality-DOI_DY 'VRDR.IJEMortality.DOI_DY')
  - [DOI_MO](#P-VRDR-IJEMortality-DOI_MO 'VRDR.IJEMortality.DOI_MO')
  - [DOI_YR](#P-VRDR-IJEMortality-DOI_YR 'VRDR.IJEMortality.DOI_YR')
  - [DOR_DY](#P-VRDR-IJEMortality-DOR_DY 'VRDR.IJEMortality.DOR_DY')
  - [DOR_MO](#P-VRDR-IJEMortality-DOR_MO 'VRDR.IJEMortality.DOR_MO')
  - [DOR_YR](#P-VRDR-IJEMortality-DOR_YR 'VRDR.IJEMortality.DOR_YR')
  - [DPLACE](#P-VRDR-IJEMortality-DPLACE 'VRDR.IJEMortality.DPLACE')
  - [DSTATE](#P-VRDR-IJEMortality-DSTATE 'VRDR.IJEMortality.DSTATE')
  - [DTHCOUNTRY](#P-VRDR-IJEMortality-DTHCOUNTRY 'VRDR.IJEMortality.DTHCOUNTRY')
  - [DTHCOUNTRYCD](#P-VRDR-IJEMortality-DTHCOUNTRYCD 'VRDR.IJEMortality.DTHCOUNTRYCD')
  - [EAC](#P-VRDR-IJEMortality-EAC 'VRDR.IJEMortality.EAC')
  - [FATHERSUFFIX](#P-VRDR-IJEMortality-FATHERSUFFIX 'VRDR.IJEMortality.FATHERSUFFIX')
  - [FILEDATE](#P-VRDR-IJEMortality-FILEDATE 'VRDR.IJEMortality.FILEDATE')
  - [FILENO](#P-VRDR-IJEMortality-FILENO 'VRDR.IJEMortality.FILENO')
  - [FILLER2](#P-VRDR-IJEMortality-FILLER2 'VRDR.IJEMortality.FILLER2')
  - [FLNAME](#P-VRDR-IJEMortality-FLNAME 'VRDR.IJEMortality.FLNAME')
  - [FUNCITYTEXT](#P-VRDR-IJEMortality-FUNCITYTEXT 'VRDR.IJEMortality.FUNCITYTEXT')
  - [FUNFACADDRESS](#P-VRDR-IJEMortality-FUNFACADDRESS 'VRDR.IJEMortality.FUNFACADDRESS')
  - [FUNFACNAME](#P-VRDR-IJEMortality-FUNFACNAME 'VRDR.IJEMortality.FUNFACNAME')
  - [FUNFACPREDIR](#P-VRDR-IJEMortality-FUNFACPREDIR 'VRDR.IJEMortality.FUNFACPREDIR')
  - [FUNFACSTNUM](#P-VRDR-IJEMortality-FUNFACSTNUM 'VRDR.IJEMortality.FUNFACSTNUM')
  - [FUNFACSTRDESIG](#P-VRDR-IJEMortality-FUNFACSTRDESIG 'VRDR.IJEMortality.FUNFACSTRDESIG')
  - [FUNFACSTRNAME](#P-VRDR-IJEMortality-FUNFACSTRNAME 'VRDR.IJEMortality.FUNFACSTRNAME')
  - [FUNPOSTDIR](#P-VRDR-IJEMortality-FUNPOSTDIR 'VRDR.IJEMortality.FUNPOSTDIR')
  - [FUNSTATE](#P-VRDR-IJEMortality-FUNSTATE 'VRDR.IJEMortality.FUNSTATE')
  - [FUNSTATECD](#P-VRDR-IJEMortality-FUNSTATECD 'VRDR.IJEMortality.FUNSTATECD')
  - [FUNUNITNUM](#P-VRDR-IJEMortality-FUNUNITNUM 'VRDR.IJEMortality.FUNUNITNUM')
  - [FUNZIP](#P-VRDR-IJEMortality-FUNZIP 'VRDR.IJEMortality.FUNZIP')
  - [GNAME](#P-VRDR-IJEMortality-GNAME 'VRDR.IJEMortality.GNAME')
  - [HISPOLDC](#P-VRDR-IJEMortality-HISPOLDC 'VRDR.IJEMortality.HISPOLDC')
  - [HISPSTSP](#P-VRDR-IJEMortality-HISPSTSP 'VRDR.IJEMortality.HISPSTSP')
  - [HOWINJ](#P-VRDR-IJEMortality-HOWINJ 'VRDR.IJEMortality.HOWINJ')
  - [IDOB_YR](#P-VRDR-IJEMortality-IDOB_YR 'VRDR.IJEMortality.IDOB_YR')
  - [INACT](#P-VRDR-IJEMortality-INACT 'VRDR.IJEMortality.INACT')
  - [INDUST](#P-VRDR-IJEMortality-INDUST 'VRDR.IJEMortality.INDUST')
  - [INDUSTC](#P-VRDR-IJEMortality-INDUSTC 'VRDR.IJEMortality.INDUSTC')
  - [INDUSTC4](#P-VRDR-IJEMortality-INDUSTC4 'VRDR.IJEMortality.INDUSTC4')
  - [INFORMRELATE](#P-VRDR-IJEMortality-INFORMRELATE 'VRDR.IJEMortality.INFORMRELATE')
  - [INJPL](#P-VRDR-IJEMortality-INJPL 'VRDR.IJEMortality.INJPL')
  - [INTERVAL1A](#P-VRDR-IJEMortality-INTERVAL1A 'VRDR.IJEMortality.INTERVAL1A')
  - [INTERVAL1B](#P-VRDR-IJEMortality-INTERVAL1B 'VRDR.IJEMortality.INTERVAL1B')
  - [INTERVAL1C](#P-VRDR-IJEMortality-INTERVAL1C 'VRDR.IJEMortality.INTERVAL1C')
  - [INTERVAL1D](#P-VRDR-IJEMortality-INTERVAL1D 'VRDR.IJEMortality.INTERVAL1D')
  - [INT_REJ](#P-VRDR-IJEMortality-INT_REJ 'VRDR.IJEMortality.INT_REJ')
  - [LAT_D](#P-VRDR-IJEMortality-LAT_D 'VRDR.IJEMortality.LAT_D')
  - [LAT_I](#P-VRDR-IJEMortality-LAT_I 'VRDR.IJEMortality.LAT_I')
  - [LIMITS](#P-VRDR-IJEMortality-LIMITS 'VRDR.IJEMortality.LIMITS')
  - [LNAME](#P-VRDR-IJEMortality-LNAME 'VRDR.IJEMortality.LNAME')
  - [LONG_D](#P-VRDR-IJEMortality-LONG_D 'VRDR.IJEMortality.LONG_D')
  - [LONG_I](#P-VRDR-IJEMortality-LONG_I 'VRDR.IJEMortality.LONG_I')
  - [MANNER](#P-VRDR-IJEMortality-MANNER 'VRDR.IJEMortality.MANNER')
  - [MAN_UC](#P-VRDR-IJEMortality-MAN_UC 'VRDR.IJEMortality.MAN_UC')
  - [MARITAL](#P-VRDR-IJEMortality-MARITAL 'VRDR.IJEMortality.MARITAL')
  - [MARITAL_BYPASS](#P-VRDR-IJEMortality-MARITAL_BYPASS 'VRDR.IJEMortality.MARITAL_BYPASS')
  - [MARITAL_DESCRIP](#P-VRDR-IJEMortality-MARITAL_DESCRIP 'VRDR.IJEMortality.MARITAL_DESCRIP')
  - [MFILED](#P-VRDR-IJEMortality-MFILED 'VRDR.IJEMortality.MFILED')
  - [MNAME](#P-VRDR-IJEMortality-MNAME 'VRDR.IJEMortality.MNAME')
  - [MOTHERSSUFFIX](#P-VRDR-IJEMortality-MOTHERSSUFFIX 'VRDR.IJEMortality.MOTHERSSUFFIX')
  - [NCHSBRIDGE](#P-VRDR-IJEMortality-NCHSBRIDGE 'VRDR.IJEMortality.NCHSBRIDGE')
  - [OCCUP](#P-VRDR-IJEMortality-OCCUP 'VRDR.IJEMortality.OCCUP')
  - [OCCUPC](#P-VRDR-IJEMortality-OCCUPC 'VRDR.IJEMortality.OCCUPC')
  - [OCCUPC4](#P-VRDR-IJEMortality-OCCUPC4 'VRDR.IJEMortality.OCCUPC4')
  - [OLDEDUC](#P-VRDR-IJEMortality-OLDEDUC 'VRDR.IJEMortality.OLDEDUC')
  - [OTHERCONDITION](#P-VRDR-IJEMortality-OTHERCONDITION 'VRDR.IJEMortality.OTHERCONDITION')
  - [PLACE1_1](#P-VRDR-IJEMortality-PLACE1_1 'VRDR.IJEMortality.PLACE1_1')
  - [PLACE1_2](#P-VRDR-IJEMortality-PLACE1_2 'VRDR.IJEMortality.PLACE1_2')
  - [PLACE1_3](#P-VRDR-IJEMortality-PLACE1_3 'VRDR.IJEMortality.PLACE1_3')
  - [PLACE1_4](#P-VRDR-IJEMortality-PLACE1_4 'VRDR.IJEMortality.PLACE1_4')
  - [PLACE1_5](#P-VRDR-IJEMortality-PLACE1_5 'VRDR.IJEMortality.PLACE1_5')
  - [PLACE1_6](#P-VRDR-IJEMortality-PLACE1_6 'VRDR.IJEMortality.PLACE1_6')
  - [PLACE20](#P-VRDR-IJEMortality-PLACE20 'VRDR.IJEMortality.PLACE20')
  - [PLACE8_1](#P-VRDR-IJEMortality-PLACE8_1 'VRDR.IJEMortality.PLACE8_1')
  - [PLACE8_2](#P-VRDR-IJEMortality-PLACE8_2 'VRDR.IJEMortality.PLACE8_2')
  - [PLACE8_3](#P-VRDR-IJEMortality-PLACE8_3 'VRDR.IJEMortality.PLACE8_3')
  - [POILITRL](#P-VRDR-IJEMortality-POILITRL 'VRDR.IJEMortality.POILITRL')
  - [POSTDIR_D](#P-VRDR-IJEMortality-POSTDIR_D 'VRDR.IJEMortality.POSTDIR_D')
  - [POSTDIR_R](#P-VRDR-IJEMortality-POSTDIR_R 'VRDR.IJEMortality.POSTDIR_R')
  - [PPDATESIGNED](#P-VRDR-IJEMortality-PPDATESIGNED 'VRDR.IJEMortality.PPDATESIGNED')
  - [PPTIME](#P-VRDR-IJEMortality-PPTIME 'VRDR.IJEMortality.PPTIME')
  - [PREDIR_D](#P-VRDR-IJEMortality-PREDIR_D 'VRDR.IJEMortality.PREDIR_D')
  - [PREDIR_R](#P-VRDR-IJEMortality-PREDIR_R 'VRDR.IJEMortality.PREDIR_R')
  - [PREG](#P-VRDR-IJEMortality-PREG 'VRDR.IJEMortality.PREG')
  - [PREG_BYPASS](#P-VRDR-IJEMortality-PREG_BYPASS 'VRDR.IJEMortality.PREG_BYPASS')
  - [RAC](#P-VRDR-IJEMortality-RAC 'VRDR.IJEMortality.RAC')
  - [RACE1](#P-VRDR-IJEMortality-RACE1 'VRDR.IJEMortality.RACE1')
  - [RACE10](#P-VRDR-IJEMortality-RACE10 'VRDR.IJEMortality.RACE10')
  - [RACE11](#P-VRDR-IJEMortality-RACE11 'VRDR.IJEMortality.RACE11')
  - [RACE12](#P-VRDR-IJEMortality-RACE12 'VRDR.IJEMortality.RACE12')
  - [RACE13](#P-VRDR-IJEMortality-RACE13 'VRDR.IJEMortality.RACE13')
  - [RACE14](#P-VRDR-IJEMortality-RACE14 'VRDR.IJEMortality.RACE14')
  - [RACE15](#P-VRDR-IJEMortality-RACE15 'VRDR.IJEMortality.RACE15')
  - [RACE16](#P-VRDR-IJEMortality-RACE16 'VRDR.IJEMortality.RACE16')
  - [RACE16C](#P-VRDR-IJEMortality-RACE16C 'VRDR.IJEMortality.RACE16C')
  - [RACE17](#P-VRDR-IJEMortality-RACE17 'VRDR.IJEMortality.RACE17')
  - [RACE17C](#P-VRDR-IJEMortality-RACE17C 'VRDR.IJEMortality.RACE17C')
  - [RACE18](#P-VRDR-IJEMortality-RACE18 'VRDR.IJEMortality.RACE18')
  - [RACE18C](#P-VRDR-IJEMortality-RACE18C 'VRDR.IJEMortality.RACE18C')
  - [RACE19](#P-VRDR-IJEMortality-RACE19 'VRDR.IJEMortality.RACE19')
  - [RACE19C](#P-VRDR-IJEMortality-RACE19C 'VRDR.IJEMortality.RACE19C')
  - [RACE1E](#P-VRDR-IJEMortality-RACE1E 'VRDR.IJEMortality.RACE1E')
  - [RACE2](#P-VRDR-IJEMortality-RACE2 'VRDR.IJEMortality.RACE2')
  - [RACE20](#P-VRDR-IJEMortality-RACE20 'VRDR.IJEMortality.RACE20')
  - [RACE20C](#P-VRDR-IJEMortality-RACE20C 'VRDR.IJEMortality.RACE20C')
  - [RACE21](#P-VRDR-IJEMortality-RACE21 'VRDR.IJEMortality.RACE21')
  - [RACE21C](#P-VRDR-IJEMortality-RACE21C 'VRDR.IJEMortality.RACE21C')
  - [RACE22](#P-VRDR-IJEMortality-RACE22 'VRDR.IJEMortality.RACE22')
  - [RACE22C](#P-VRDR-IJEMortality-RACE22C 'VRDR.IJEMortality.RACE22C')
  - [RACE23](#P-VRDR-IJEMortality-RACE23 'VRDR.IJEMortality.RACE23')
  - [RACE23C](#P-VRDR-IJEMortality-RACE23C 'VRDR.IJEMortality.RACE23C')
  - [RACE2E](#P-VRDR-IJEMortality-RACE2E 'VRDR.IJEMortality.RACE2E')
  - [RACE3](#P-VRDR-IJEMortality-RACE3 'VRDR.IJEMortality.RACE3')
  - [RACE3E](#P-VRDR-IJEMortality-RACE3E 'VRDR.IJEMortality.RACE3E')
  - [RACE4](#P-VRDR-IJEMortality-RACE4 'VRDR.IJEMortality.RACE4')
  - [RACE4E](#P-VRDR-IJEMortality-RACE4E 'VRDR.IJEMortality.RACE4E')
  - [RACE5](#P-VRDR-IJEMortality-RACE5 'VRDR.IJEMortality.RACE5')
  - [RACE5E](#P-VRDR-IJEMortality-RACE5E 'VRDR.IJEMortality.RACE5E')
  - [RACE6](#P-VRDR-IJEMortality-RACE6 'VRDR.IJEMortality.RACE6')
  - [RACE6E](#P-VRDR-IJEMortality-RACE6E 'VRDR.IJEMortality.RACE6E')
  - [RACE7](#P-VRDR-IJEMortality-RACE7 'VRDR.IJEMortality.RACE7')
  - [RACE7E](#P-VRDR-IJEMortality-RACE7E 'VRDR.IJEMortality.RACE7E')
  - [RACE8](#P-VRDR-IJEMortality-RACE8 'VRDR.IJEMortality.RACE8')
  - [RACE8E](#P-VRDR-IJEMortality-RACE8E 'VRDR.IJEMortality.RACE8E')
  - [RACE9](#P-VRDR-IJEMortality-RACE9 'VRDR.IJEMortality.RACE9')
  - [RACEOLDC](#P-VRDR-IJEMortality-RACEOLDC 'VRDR.IJEMortality.RACEOLDC')
  - [RACESTSP](#P-VRDR-IJEMortality-RACESTSP 'VRDR.IJEMortality.RACESTSP')
  - [RACE_MVR](#P-VRDR-IJEMortality-RACE_MVR 'VRDR.IJEMortality.RACE_MVR')
  - [REFERRED](#P-VRDR-IJEMortality-REFERRED 'VRDR.IJEMortality.REFERRED')
  - [REPLACE](#P-VRDR-IJEMortality-REPLACE 'VRDR.IJEMortality.REPLACE')
  - [RESCON](#P-VRDR-IJEMortality-RESCON 'VRDR.IJEMortality.RESCON')
  - [RESSTATE](#P-VRDR-IJEMortality-RESSTATE 'VRDR.IJEMortality.RESSTATE')
  - [R_DY](#P-VRDR-IJEMortality-R_DY 'VRDR.IJEMortality.R_DY')
  - [R_MO](#P-VRDR-IJEMortality-R_MO 'VRDR.IJEMortality.R_MO')
  - [R_YR](#P-VRDR-IJEMortality-R_YR 'VRDR.IJEMortality.R_YR')
  - [SEX](#P-VRDR-IJEMortality-SEX 'VRDR.IJEMortality.SEX')
  - [SEX_BYPASS](#P-VRDR-IJEMortality-SEX_BYPASS 'VRDR.IJEMortality.SEX_BYPASS')
  - [SPOUSEF](#P-VRDR-IJEMortality-SPOUSEF 'VRDR.IJEMortality.SPOUSEF')
  - [SPOUSEL](#P-VRDR-IJEMortality-SPOUSEL 'VRDR.IJEMortality.SPOUSEL')
  - [SPOUSELV](#P-VRDR-IJEMortality-SPOUSELV 'VRDR.IJEMortality.SPOUSELV')
  - [SPOUSEMIDNAME](#P-VRDR-IJEMortality-SPOUSEMIDNAME 'VRDR.IJEMortality.SPOUSEMIDNAME')
  - [SPOUSESUFFIX](#P-VRDR-IJEMortality-SPOUSESUFFIX 'VRDR.IJEMortality.SPOUSESUFFIX')
  - [SSADATETRANS](#P-VRDR-IJEMortality-SSADATETRANS 'VRDR.IJEMortality.SSADATETRANS')
  - [SSADATEVER](#P-VRDR-IJEMortality-SSADATEVER 'VRDR.IJEMortality.SSADATEVER')
  - [SSADTHCODE](#P-VRDR-IJEMortality-SSADTHCODE 'VRDR.IJEMortality.SSADTHCODE')
  - [SSAFOREIGN](#P-VRDR-IJEMortality-SSAFOREIGN 'VRDR.IJEMortality.SSAFOREIGN')
  - [SSAVERIFY](#P-VRDR-IJEMortality-SSAVERIFY 'VRDR.IJEMortality.SSAVERIFY')
  - [SSN](#P-VRDR-IJEMortality-SSN 'VRDR.IJEMortality.SSN')
  - [STATEBTH](#P-VRDR-IJEMortality-STATEBTH 'VRDR.IJEMortality.STATEBTH')
  - [STATEC](#P-VRDR-IJEMortality-STATEC 'VRDR.IJEMortality.STATEC')
  - [STATECODE_I](#P-VRDR-IJEMortality-STATECODE_I 'VRDR.IJEMortality.STATECODE_I')
  - [STATESP](#P-VRDR-IJEMortality-STATESP 'VRDR.IJEMortality.STATESP')
  - [STATETEXT_D](#P-VRDR-IJEMortality-STATETEXT_D 'VRDR.IJEMortality.STATETEXT_D')
  - [STATETEXT_R](#P-VRDR-IJEMortality-STATETEXT_R 'VRDR.IJEMortality.STATETEXT_R')
  - [STDESIG_D](#P-VRDR-IJEMortality-STDESIG_D 'VRDR.IJEMortality.STDESIG_D')
  - [STDESIG_R](#P-VRDR-IJEMortality-STDESIG_R 'VRDR.IJEMortality.STDESIG_R')
  - [STINJURY](#P-VRDR-IJEMortality-STINJURY 'VRDR.IJEMortality.STINJURY')
  - [STNAME_D](#P-VRDR-IJEMortality-STNAME_D 'VRDR.IJEMortality.STNAME_D')
  - [STNAME_R](#P-VRDR-IJEMortality-STNAME_R 'VRDR.IJEMortality.STNAME_R')
  - [STNUM_D](#P-VRDR-IJEMortality-STNUM_D 'VRDR.IJEMortality.STNUM_D')
  - [STNUM_R](#P-VRDR-IJEMortality-STNUM_R 'VRDR.IJEMortality.STNUM_R')
  - [SUFF](#P-VRDR-IJEMortality-SUFF 'VRDR.IJEMortality.SUFF')
  - [SUR_DY](#P-VRDR-IJEMortality-SUR_DY 'VRDR.IJEMortality.SUR_DY')
  - [SUR_MO](#P-VRDR-IJEMortality-SUR_MO 'VRDR.IJEMortality.SUR_MO')
  - [SUR_YR](#P-VRDR-IJEMortality-SUR_YR 'VRDR.IJEMortality.SUR_YR')
  - [SYS_REJ](#P-VRDR-IJEMortality-SYS_REJ 'VRDR.IJEMortality.SYS_REJ')
  - [TOBAC](#P-VRDR-IJEMortality-TOBAC 'VRDR.IJEMortality.TOBAC')
  - [TOD](#P-VRDR-IJEMortality-TOD 'VRDR.IJEMortality.TOD')
  - [TOI_HR](#P-VRDR-IJEMortality-TOI_HR 'VRDR.IJEMortality.TOI_HR')
  - [TOI_UNIT](#P-VRDR-IJEMortality-TOI_UNIT 'VRDR.IJEMortality.TOI_UNIT')
  - [TRANSPRT](#P-VRDR-IJEMortality-TRANSPRT 'VRDR.IJEMortality.TRANSPRT')
  - [TRX_FLG](#P-VRDR-IJEMortality-TRX_FLG 'VRDR.IJEMortality.TRX_FLG')
  - [UNITNUM_R](#P-VRDR-IJEMortality-UNITNUM_R 'VRDR.IJEMortality.UNITNUM_R')
  - [VOID](#P-VRDR-IJEMortality-VOID 'VRDR.IJEMortality.VOID')
  - [WORKINJ](#P-VRDR-IJEMortality-WORKINJ 'VRDR.IJEMortality.WORKINJ')
  - [ZIP9_D](#P-VRDR-IJEMortality-ZIP9_D 'VRDR.IJEMortality.ZIP9_D')
  - [ZIP9_R](#P-VRDR-IJEMortality-ZIP9_R 'VRDR.IJEMortality.ZIP9_R')
  - [ActualICD10toNCHSICD10()](#M-VRDR-IJEMortality-ActualICD10toNCHSICD10-System-String- 'VRDR.IJEMortality.ActualICD10toNCHSICD10(System.String)')
  - [DateTimeStringHelper()](#M-VRDR-IJEMortality-DateTimeStringHelper-VRDR-IJEField,System-String,System-String,System-DateTimeOffset,System-Boolean,System-Boolean- 'VRDR.IJEMortality.DateTimeStringHelper(VRDR.IJEField,System.String,System.String,System.DateTimeOffset,System.Boolean,System.Boolean)')
  - [DateTime_Get()](#M-VRDR-IJEMortality-DateTime_Get-System-String,System-String,System-String- 'VRDR.IJEMortality.DateTime_Get(System.String,System.String,System.String)')
  - [DateTime_Set()](#M-VRDR-IJEMortality-DateTime_Set-System-String,System-String,System-String,System-String,System-Boolean,System-Boolean- 'VRDR.IJEMortality.DateTime_Set(System.String,System.String,System.String,System.String,System.Boolean,System.Boolean)')
  - [Dictionary_Geo_Get()](#M-VRDR-IJEMortality-Dictionary_Geo_Get-System-String,System-String,System-String,System-String,System-Boolean- 'VRDR.IJEMortality.Dictionary_Geo_Get(System.String,System.String,System.String,System.String,System.Boolean)')
  - [Dictionary_Geo_Set()](#M-VRDR-IJEMortality-Dictionary_Geo_Set-System-String,System-String,System-String,System-String,System-Boolean,System-String- 'VRDR.IJEMortality.Dictionary_Geo_Set(System.String,System.String,System.String,System.String,System.Boolean,System.String)')
  - [Dictionary_Get()](#M-VRDR-IJEMortality-Dictionary_Get-System-String,System-String,System-String- 'VRDR.IJEMortality.Dictionary_Get(System.String,System.String,System.String)')
  - [Dictionary_Get_Full()](#M-VRDR-IJEMortality-Dictionary_Get_Full-System-String,System-String,System-String- 'VRDR.IJEMortality.Dictionary_Get_Full(System.String,System.String,System.String)')
  - [Dictionary_Set()](#M-VRDR-IJEMortality-Dictionary_Set-System-String,System-String,System-String,System-String- 'VRDR.IJEMortality.Dictionary_Set(System.String,System.String,System.String,System.String)')
  - [FieldInfo()](#M-VRDR-IJEMortality-FieldInfo-System-String- 'VRDR.IJEMortality.FieldInfo(System.String)')
  - [Get_MappingFHIRToIJE(mapping,fhirField,ijeField)](#M-VRDR-IJEMortality-Get_MappingFHIRToIJE-System-Collections-Generic-Dictionary{System-String,System-String},System-String,System-String- 'VRDR.IJEMortality.Get_MappingFHIRToIJE(System.Collections.Generic.Dictionary{System.String,System.String},System.String,System.String)')
  - [Get_Race()](#M-VRDR-IJEMortality-Get_Race-System-String- 'VRDR.IJEMortality.Get_Race(System.String)')
  - [LeftJustified_Get()](#M-VRDR-IJEMortality-LeftJustified_Get-System-String,System-String- 'VRDR.IJEMortality.LeftJustified_Get(System.String,System.String)')
  - [LeftJustified_Set()](#M-VRDR-IJEMortality-LeftJustified_Set-System-String,System-String,System-String- 'VRDR.IJEMortality.LeftJustified_Set(System.String,System.String,System.String)')
  - [NCHSICD10toActualICD10()](#M-VRDR-IJEMortality-NCHSICD10toActualICD10-System-String- 'VRDR.IJEMortality.NCHSICD10toActualICD10(System.String)')
  - [NumericAllowingUnknown_Get()](#M-VRDR-IJEMortality-NumericAllowingUnknown_Get-System-String,System-String- 'VRDR.IJEMortality.NumericAllowingUnknown_Get(System.String,System.String)')
  - [NumericAllowingUnknown_Set()](#M-VRDR-IJEMortality-NumericAllowingUnknown_Set-System-String,System-String,System-String- 'VRDR.IJEMortality.NumericAllowingUnknown_Set(System.String,System.String,System.String)')
  - [RightJustifiedZeroed_Get()](#M-VRDR-IJEMortality-RightJustifiedZeroed_Get-System-String,System-String- 'VRDR.IJEMortality.RightJustifiedZeroed_Get(System.String,System.String)')
  - [RightJustifiedZeroed_Set()](#M-VRDR-IJEMortality-RightJustifiedZeroed_Set-System-String,System-String,System-String- 'VRDR.IJEMortality.RightJustifiedZeroed_Set(System.String,System.String,System.String)')
  - [Set_MappingIJEToFHIR(mapping,ijeField,fhirField,value)](#M-VRDR-IJEMortality-Set_MappingIJEToFHIR-System-Collections-Generic-Dictionary{System-String,System-String},System-String,System-String,System-String- 'VRDR.IJEMortality.Set_MappingIJEToFHIR(System.Collections.Generic.Dictionary{System.String,System.String},System.String,System.String,System.String)')
  - [Set_Race()](#M-VRDR-IJEMortality-Set_Race-System-String,System-String- 'VRDR.IJEMortality.Set_Race(System.String,System.String)')
  - [TimeAllowingUnknown_Get()](#M-VRDR-IJEMortality-TimeAllowingUnknown_Get-System-String,System-String- 'VRDR.IJEMortality.TimeAllowingUnknown_Get(System.String,System.String)')
  - [TimeAllowingUnknown_Set()](#M-VRDR-IJEMortality-TimeAllowingUnknown_Set-System-String,System-String,System-String- 'VRDR.IJEMortality.TimeAllowingUnknown_Set(System.String,System.String,System.String)')
  - [ToDeathRecord()](#M-VRDR-IJEMortality-ToDeathRecord 'VRDR.IJEMortality.ToDeathRecord')
  - [ToString()](#M-VRDR-IJEMortality-ToString 'VRDR.IJEMortality.ToString')
  - [Truncate()](#M-VRDR-IJEMortality-Truncate-System-String,System-Int32- 'VRDR.IJEMortality.Truncate(System.String,System.Int32)')
- [IntentionalReject](#T-VRDR-Mappings-IntentionalReject 'VRDR.Mappings.IntentionalReject')
- [IntentionalReject](#T-VRDR-ValueSets-IntentionalReject 'VRDR.ValueSets.IntentionalReject')
  - [FHIRToIJE](#F-VRDR-Mappings-IntentionalReject-FHIRToIJE 'VRDR.Mappings.IntentionalReject.FHIRToIJE')
  - [IJEToFHIR](#F-VRDR-Mappings-IntentionalReject-IJEToFHIR 'VRDR.Mappings.IntentionalReject.IJEToFHIR')
  - [Codes](#F-VRDR-ValueSets-IntentionalReject-Codes 'VRDR.ValueSets.IntentionalReject.Codes')
  - [Reject1](#F-VRDR-ValueSets-IntentionalReject-Reject1 'VRDR.ValueSets.IntentionalReject.Reject1')
  - [Reject2](#F-VRDR-ValueSets-IntentionalReject-Reject2 'VRDR.ValueSets.IntentionalReject.Reject2')
  - [Reject3](#F-VRDR-ValueSets-IntentionalReject-Reject3 'VRDR.ValueSets.IntentionalReject.Reject3')
  - [Reject4](#F-VRDR-ValueSets-IntentionalReject-Reject4 'VRDR.ValueSets.IntentionalReject.Reject4')
  - [Reject5](#F-VRDR-ValueSets-IntentionalReject-Reject5 'VRDR.ValueSets.IntentionalReject.Reject5')
  - [Reject9](#F-VRDR-ValueSets-IntentionalReject-Reject9 'VRDR.ValueSets.IntentionalReject.Reject9')
- [LinqHelper](#T-VRDR-LinqHelper 'VRDR.LinqHelper')
  - [EqualsInsensitive()](#M-VRDR-LinqHelper-EqualsInsensitive-System-String,System-String- 'VRDR.LinqHelper.EqualsInsensitive(System.String,System.String)')
- [MREHelper](#T-VRDR-IJEMortality-MREHelper 'VRDR.IJEMortality.MREHelper')
  - [#ctor()](#M-VRDR-IJEMortality-MREHelper-#ctor-VRDR-DeathRecord- 'VRDR.IJEMortality.MREHelper.#ctor(VRDR.DeathRecord)')
  - [RECODE40](#P-VRDR-IJEMortality-MREHelper-RECODE40 'VRDR.IJEMortality.MREHelper.RECODE40')
- [MannerOfDeath](#T-VRDR-Mappings-MannerOfDeath 'VRDR.Mappings.MannerOfDeath')
- [MannerOfDeath](#T-VRDR-ValueSets-MannerOfDeath 'VRDR.ValueSets.MannerOfDeath')
  - [FHIRToIJE](#F-VRDR-Mappings-MannerOfDeath-FHIRToIJE 'VRDR.Mappings.MannerOfDeath.FHIRToIJE')
  - [IJEToFHIR](#F-VRDR-Mappings-MannerOfDeath-IJEToFHIR 'VRDR.Mappings.MannerOfDeath.IJEToFHIR')
  - [Accidental_Death](#F-VRDR-ValueSets-MannerOfDeath-Accidental_Death 'VRDR.ValueSets.MannerOfDeath.Accidental_Death')
  - [Codes](#F-VRDR-ValueSets-MannerOfDeath-Codes 'VRDR.ValueSets.MannerOfDeath.Codes')
  - [Death_Manner_Undetermined](#F-VRDR-ValueSets-MannerOfDeath-Death_Manner_Undetermined 'VRDR.ValueSets.MannerOfDeath.Death_Manner_Undetermined')
  - [Homicide](#F-VRDR-ValueSets-MannerOfDeath-Homicide 'VRDR.ValueSets.MannerOfDeath.Homicide')
  - [Natural_Death](#F-VRDR-ValueSets-MannerOfDeath-Natural_Death 'VRDR.ValueSets.MannerOfDeath.Natural_Death')
  - [Patient_Awaiting_Investigation](#F-VRDR-ValueSets-MannerOfDeath-Patient_Awaiting_Investigation 'VRDR.ValueSets.MannerOfDeath.Patient_Awaiting_Investigation')
  - [Suicide](#F-VRDR-ValueSets-MannerOfDeath-Suicide 'VRDR.ValueSets.MannerOfDeath.Suicide')
- [Mappings](#T-VRDR-Mappings 'VRDR.Mappings')
- [MaritalStatus](#T-VRDR-Mappings-MaritalStatus 'VRDR.Mappings.MaritalStatus')
- [MaritalStatus](#T-VRDR-ValueSets-MaritalStatus 'VRDR.ValueSets.MaritalStatus')
  - [FHIRToIJE](#F-VRDR-Mappings-MaritalStatus-FHIRToIJE 'VRDR.Mappings.MaritalStatus.FHIRToIJE')
  - [IJEToFHIR](#F-VRDR-Mappings-MaritalStatus-IJEToFHIR 'VRDR.Mappings.MaritalStatus.IJEToFHIR')
  - [Codes](#F-VRDR-ValueSets-MaritalStatus-Codes 'VRDR.ValueSets.MaritalStatus.Codes')
  - [Divorced](#F-VRDR-ValueSets-MaritalStatus-Divorced 'VRDR.ValueSets.MaritalStatus.Divorced')
  - [Legally_Separated](#F-VRDR-ValueSets-MaritalStatus-Legally_Separated 'VRDR.ValueSets.MaritalStatus.Legally_Separated')
  - [Married](#F-VRDR-ValueSets-MaritalStatus-Married 'VRDR.ValueSets.MaritalStatus.Married')
  - [Never_Married](#F-VRDR-ValueSets-MaritalStatus-Never_Married 'VRDR.ValueSets.MaritalStatus.Never_Married')
  - [Unknown](#F-VRDR-ValueSets-MaritalStatus-Unknown 'VRDR.ValueSets.MaritalStatus.Unknown')
  - [Widowed](#F-VRDR-ValueSets-MaritalStatus-Widowed 'VRDR.ValueSets.MaritalStatus.Widowed')
- [MethodOfDisposition](#T-VRDR-Mappings-MethodOfDisposition 'VRDR.Mappings.MethodOfDisposition')
- [MethodOfDisposition](#T-VRDR-ValueSets-MethodOfDisposition 'VRDR.ValueSets.MethodOfDisposition')
  - [FHIRToIJE](#F-VRDR-Mappings-MethodOfDisposition-FHIRToIJE 'VRDR.Mappings.MethodOfDisposition.FHIRToIJE')
  - [IJEToFHIR](#F-VRDR-Mappings-MethodOfDisposition-IJEToFHIR 'VRDR.Mappings.MethodOfDisposition.IJEToFHIR')
  - [Burial](#F-VRDR-ValueSets-MethodOfDisposition-Burial 'VRDR.ValueSets.MethodOfDisposition.Burial')
  - [Codes](#F-VRDR-ValueSets-MethodOfDisposition-Codes 'VRDR.ValueSets.MethodOfDisposition.Codes')
  - [Cremation](#F-VRDR-ValueSets-MethodOfDisposition-Cremation 'VRDR.ValueSets.MethodOfDisposition.Cremation')
  - [Donation](#F-VRDR-ValueSets-MethodOfDisposition-Donation 'VRDR.ValueSets.MethodOfDisposition.Donation')
  - [Entombment](#F-VRDR-ValueSets-MethodOfDisposition-Entombment 'VRDR.ValueSets.MethodOfDisposition.Entombment')
  - [Other](#F-VRDR-ValueSets-MethodOfDisposition-Other 'VRDR.ValueSets.MethodOfDisposition.Other')
  - [Removal_From_State](#F-VRDR-ValueSets-MethodOfDisposition-Removal_From_State 'VRDR.ValueSets.MethodOfDisposition.Removal_From_State')
  - [Unknown](#F-VRDR-ValueSets-MethodOfDisposition-Unknown 'VRDR.ValueSets.MethodOfDisposition.Unknown')
- [MortalityData](#T-VRDR-MortalityData 'VRDR.MortalityData')
  - [CDCEthnicityCodes](#F-VRDR-MortalityData-CDCEthnicityCodes 'VRDR.MortalityData.CDCEthnicityCodes')
  - [CDCRaceACodes](#F-VRDR-MortalityData-CDCRaceACodes 'VRDR.MortalityData.CDCRaceACodes')
  - [CDCRaceAIANCodes](#F-VRDR-MortalityData-CDCRaceAIANCodes 'VRDR.MortalityData.CDCRaceAIANCodes')
  - [CDCRaceBAACodes](#F-VRDR-MortalityData-CDCRaceBAACodes 'VRDR.MortalityData.CDCRaceBAACodes')
  - [CDCRaceNHOPICodes](#F-VRDR-MortalityData-CDCRaceNHOPICodes 'VRDR.MortalityData.CDCRaceNHOPICodes')
  - [CDCRaceWCodes](#F-VRDR-MortalityData-CDCRaceWCodes 'VRDR.MortalityData.CDCRaceWCodes')
  - [CountryCodes](#F-VRDR-MortalityData-CountryCodes 'VRDR.MortalityData.CountryCodes')
  - [JurisdictionCodes](#F-VRDR-MortalityData-JurisdictionCodes 'VRDR.MortalityData.JurisdictionCodes')
  - [PlaceCodes](#F-VRDR-MortalityData-PlaceCodes 'VRDR.MortalityData.PlaceCodes')
  - [StateTerritoryProvinceCodes](#F-VRDR-MortalityData-StateTerritoryProvinceCodes 'VRDR.MortalityData.StateTerritoryProvinceCodes')
  - [Instance](#P-VRDR-MortalityData-Instance 'VRDR.MortalityData.Instance')
  - [AIANRaceCodeToRaceName()](#M-VRDR-MortalityData-AIANRaceCodeToRaceName-System-String- 'VRDR.MortalityData.AIANRaceCodeToRaceName(System.String)')
  - [AIANRaceNameToRaceCode()](#M-VRDR-MortalityData-AIANRaceNameToRaceCode-System-String- 'VRDR.MortalityData.AIANRaceNameToRaceCode(System.String)')
  - [ARaceCodeToRaceName()](#M-VRDR-MortalityData-ARaceCodeToRaceName-System-String- 'VRDR.MortalityData.ARaceCodeToRaceName(System.String)')
  - [ARaceNameToRaceCode()](#M-VRDR-MortalityData-ARaceNameToRaceCode-System-String- 'VRDR.MortalityData.ARaceNameToRaceCode(System.String)')
  - [BAARaceCodeToRaceName()](#M-VRDR-MortalityData-BAARaceCodeToRaceName-System-String- 'VRDR.MortalityData.BAARaceCodeToRaceName(System.String)')
  - [BAARaceNameToRaceCode()](#M-VRDR-MortalityData-BAARaceNameToRaceCode-System-String- 'VRDR.MortalityData.BAARaceNameToRaceCode(System.String)')
  - [CountryCodeToCountryName()](#M-VRDR-MortalityData-CountryCodeToCountryName-System-String- 'VRDR.MortalityData.CountryCodeToCountryName(System.String)')
  - [CountryNameToCountryCode()](#M-VRDR-MortalityData-CountryNameToCountryCode-System-String- 'VRDR.MortalityData.CountryNameToCountryCode(System.String)')
  - [DictKeyFinderHelper\`\`1()](#M-VRDR-MortalityData-DictKeyFinderHelper``1-``0,System-String- 'VRDR.MortalityData.DictKeyFinderHelper``1(``0,System.String)')
  - [DictValueFinderHelper\`\`1()](#M-VRDR-MortalityData-DictValueFinderHelper``1-``0,System-String- 'VRDR.MortalityData.DictValueFinderHelper``1(``0,System.String)')
  - [EthnicityCodeToEthnicityName()](#M-VRDR-MortalityData-EthnicityCodeToEthnicityName-System-String- 'VRDR.MortalityData.EthnicityCodeToEthnicityName(System.String)')
  - [EthnicityNameToEthnicityCode()](#M-VRDR-MortalityData-EthnicityNameToEthnicityCode-System-String- 'VRDR.MortalityData.EthnicityNameToEthnicityCode(System.String)')
  - [JurisdictionCodeToJurisdictionName()](#M-VRDR-MortalityData-JurisdictionCodeToJurisdictionName-System-String- 'VRDR.MortalityData.JurisdictionCodeToJurisdictionName(System.String)')
  - [JurisdictionNameToJurisdictionCode()](#M-VRDR-MortalityData-JurisdictionNameToJurisdictionCode-System-String- 'VRDR.MortalityData.JurisdictionNameToJurisdictionCode(System.String)')
  - [NHOPIRaceCodeToRaceName()](#M-VRDR-MortalityData-NHOPIRaceCodeToRaceName-System-String- 'VRDR.MortalityData.NHOPIRaceCodeToRaceName(System.String)')
  - [NHOPIRaceNameToRaceCode()](#M-VRDR-MortalityData-NHOPIRaceNameToRaceCode-System-String- 'VRDR.MortalityData.NHOPIRaceNameToRaceCode(System.String)')
  - [RaceCodeToRaceName()](#M-VRDR-MortalityData-RaceCodeToRaceName-System-String- 'VRDR.MortalityData.RaceCodeToRaceName(System.String)')
  - [RaceNameToRaceCode()](#M-VRDR-MortalityData-RaceNameToRaceCode-System-String- 'VRDR.MortalityData.RaceNameToRaceCode(System.String)')
  - [StateCodeAndCityNameToCountyName()](#M-VRDR-MortalityData-StateCodeAndCityNameToCountyName-System-String,System-String- 'VRDR.MortalityData.StateCodeAndCityNameToCountyName(System.String,System.String)')
  - [StateCodeToRandomPlace()](#M-VRDR-MortalityData-StateCodeToRandomPlace-System-String- 'VRDR.MortalityData.StateCodeToRandomPlace(System.String)')
  - [StateCodeToStateName()](#M-VRDR-MortalityData-StateCodeToStateName-System-String- 'VRDR.MortalityData.StateCodeToStateName(System.String)')
  - [StateNameAndCountyCodeToCountyName()](#M-VRDR-MortalityData-StateNameAndCountyCodeToCountyName-System-String,System-String- 'VRDR.MortalityData.StateNameAndCountyCodeToCountyName(System.String,System.String)')
  - [StateNameAndCountyNameAndPlaceCodeToPlaceName()](#M-VRDR-MortalityData-StateNameAndCountyNameAndPlaceCodeToPlaceName-System-String,System-String,System-String- 'VRDR.MortalityData.StateNameAndCountyNameAndPlaceCodeToPlaceName(System.String,System.String,System.String)')
  - [StateNameAndCountyNameAndPlaceNameToPlaceCode()](#M-VRDR-MortalityData-StateNameAndCountyNameAndPlaceNameToPlaceCode-System-String,System-String,System-String- 'VRDR.MortalityData.StateNameAndCountyNameAndPlaceNameToPlaceCode(System.String,System.String,System.String)')
  - [StateNameAndCountyNameToCountyCode()](#M-VRDR-MortalityData-StateNameAndCountyNameToCountyCode-System-String,System-String- 'VRDR.MortalityData.StateNameAndCountyNameToCountyCode(System.String,System.String)')
  - [StateNameToStateCode()](#M-VRDR-MortalityData-StateNameToStateCode-System-String- 'VRDR.MortalityData.StateNameToStateCode(System.String)')
  - [WRaceCodeToRaceName()](#M-VRDR-MortalityData-WRaceCodeToRaceName-System-String- 'VRDR.MortalityData.WRaceCodeToRaceName(System.String)')
  - [WRaceNameToRaceCode()](#M-VRDR-MortalityData-WRaceNameToRaceCode-System-String- 'VRDR.MortalityData.WRaceNameToRaceCode(System.String)')
- [NotApplicable](#T-VRDR-Mappings-NotApplicable 'VRDR.Mappings.NotApplicable')
  - [FHIRToIJE](#F-VRDR-Mappings-NotApplicable-FHIRToIJE 'VRDR.Mappings.NotApplicable.FHIRToIJE')
  - [IJEToFHIR](#F-VRDR-Mappings-NotApplicable-IJEToFHIR 'VRDR.Mappings.NotApplicable.IJEToFHIR')
- [NvssEthnicity](#T-VRDR-NvssEthnicity 'VRDR.NvssEthnicity')
  - [Cuban](#F-VRDR-NvssEthnicity-Cuban 'VRDR.NvssEthnicity.Cuban')
  - [Literal](#F-VRDR-NvssEthnicity-Literal 'VRDR.NvssEthnicity.Literal')
  - [Mexican](#F-VRDR-NvssEthnicity-Mexican 'VRDR.NvssEthnicity.Mexican')
  - [Other](#F-VRDR-NvssEthnicity-Other 'VRDR.NvssEthnicity.Other')
  - [PuertoRican](#F-VRDR-NvssEthnicity-PuertoRican 'VRDR.NvssEthnicity.PuertoRican')
- [NvssRace](#T-VRDR-NvssRace 'VRDR.NvssRace')
  - [AmericanIndianOrAlaskaNative](#F-VRDR-NvssRace-AmericanIndianOrAlaskaNative 'VRDR.NvssRace.AmericanIndianOrAlaskaNative')
  - [AmericanIndianOrAlaskanNativeLiteral1](#F-VRDR-NvssRace-AmericanIndianOrAlaskanNativeLiteral1 'VRDR.NvssRace.AmericanIndianOrAlaskanNativeLiteral1')
  - [AmericanIndianOrAlaskanNativeLiteral2](#F-VRDR-NvssRace-AmericanIndianOrAlaskanNativeLiteral2 'VRDR.NvssRace.AmericanIndianOrAlaskanNativeLiteral2')
  - [AsianIndian](#F-VRDR-NvssRace-AsianIndian 'VRDR.NvssRace.AsianIndian')
  - [BlackOrAfricanAmerican](#F-VRDR-NvssRace-BlackOrAfricanAmerican 'VRDR.NvssRace.BlackOrAfricanAmerican')
  - [Chinese](#F-VRDR-NvssRace-Chinese 'VRDR.NvssRace.Chinese')
  - [Filipino](#F-VRDR-NvssRace-Filipino 'VRDR.NvssRace.Filipino')
  - [GuamanianOrChamorro](#F-VRDR-NvssRace-GuamanianOrChamorro 'VRDR.NvssRace.GuamanianOrChamorro')
  - [Japanese](#F-VRDR-NvssRace-Japanese 'VRDR.NvssRace.Japanese')
  - [Korean](#F-VRDR-NvssRace-Korean 'VRDR.NvssRace.Korean')
  - [MissingValueReason](#F-VRDR-NvssRace-MissingValueReason 'VRDR.NvssRace.MissingValueReason')
  - [NativeHawaiian](#F-VRDR-NvssRace-NativeHawaiian 'VRDR.NvssRace.NativeHawaiian')
  - [OtherAsian](#F-VRDR-NvssRace-OtherAsian 'VRDR.NvssRace.OtherAsian')
  - [OtherAsianLiteral1](#F-VRDR-NvssRace-OtherAsianLiteral1 'VRDR.NvssRace.OtherAsianLiteral1')
  - [OtherAsianLiteral2](#F-VRDR-NvssRace-OtherAsianLiteral2 'VRDR.NvssRace.OtherAsianLiteral2')
  - [OtherPacificIslandLiteral1](#F-VRDR-NvssRace-OtherPacificIslandLiteral1 'VRDR.NvssRace.OtherPacificIslandLiteral1')
  - [OtherPacificIslandLiteral2](#F-VRDR-NvssRace-OtherPacificIslandLiteral2 'VRDR.NvssRace.OtherPacificIslandLiteral2')
  - [OtherPacificIslander](#F-VRDR-NvssRace-OtherPacificIslander 'VRDR.NvssRace.OtherPacificIslander')
  - [OtherRace](#F-VRDR-NvssRace-OtherRace 'VRDR.NvssRace.OtherRace')
  - [OtherRaceLiteral1](#F-VRDR-NvssRace-OtherRaceLiteral1 'VRDR.NvssRace.OtherRaceLiteral1')
  - [OtherRaceLiteral2](#F-VRDR-NvssRace-OtherRaceLiteral2 'VRDR.NvssRace.OtherRaceLiteral2')
  - [Samoan](#F-VRDR-NvssRace-Samoan 'VRDR.NvssRace.Samoan')
  - [Vietnamese](#F-VRDR-NvssRace-Vietnamese 'VRDR.NvssRace.Vietnamese')
  - [White](#F-VRDR-NvssRace-White 'VRDR.NvssRace.White')
  - [GetBooleanRaceCodes()](#M-VRDR-NvssRace-GetBooleanRaceCodes 'VRDR.NvssRace.GetBooleanRaceCodes')
  - [GetLiteralRaceCodes()](#M-VRDR-NvssRace-GetLiteralRaceCodes 'VRDR.NvssRace.GetLiteralRaceCodes')
- [OtherExtensionURL](#T-VRDR-OtherExtensionURL 'VRDR.OtherExtensionURL')
  - [BirthSex](#F-VRDR-OtherExtensionURL-BirthSex 'VRDR.OtherExtensionURL.BirthSex')
  - [DataAbsentReason](#F-VRDR-OtherExtensionURL-DataAbsentReason 'VRDR.OtherExtensionURL.DataAbsentReason')
  - [PatientBirthPlace](#F-VRDR-OtherExtensionURL-PatientBirthPlace 'VRDR.OtherExtensionURL.PatientBirthPlace')
- [OtherIGURL](#T-VRDR-OtherIGURL 'VRDR.OtherIGURL')
  - [USCorePractitioner](#F-VRDR-OtherIGURL-USCorePractitioner 'VRDR.OtherIGURL.USCorePractitioner')
- [OtherProfileURL](#T-VRDR-OtherProfileURL 'VRDR.OtherProfileURL')
  - [USCorePractitioner](#F-VRDR-OtherProfileURL-USCorePractitioner 'VRDR.OtherProfileURL.USCorePractitioner')
- [PlaceCode](#T-VRDR-PlaceCode 'VRDR.PlaceCode')
  - [#ctor()](#M-VRDR-PlaceCode-#ctor 'VRDR.PlaceCode.#ctor')
  - [#ctor()](#M-VRDR-PlaceCode-#ctor-System-String,System-String,System-String,System-String,System-String,System-String- 'VRDR.PlaceCode.#ctor(System.String,System.String,System.String,System.String,System.String,System.String)')
  - [City](#P-VRDR-PlaceCode-City 'VRDR.PlaceCode.City')
  - [Code](#P-VRDR-PlaceCode-Code 'VRDR.PlaceCode.Code')
  - [County](#P-VRDR-PlaceCode-County 'VRDR.PlaceCode.County')
  - [CountyCode](#P-VRDR-PlaceCode-CountyCode 'VRDR.PlaceCode.CountyCode')
  - [Description](#P-VRDR-PlaceCode-Description 'VRDR.PlaceCode.Description')
  - [State](#P-VRDR-PlaceCode-State 'VRDR.PlaceCode.State')
- [PlaceOfDeath](#T-VRDR-Mappings-PlaceOfDeath 'VRDR.Mappings.PlaceOfDeath')
- [PlaceOfDeath](#T-VRDR-ValueSets-PlaceOfDeath 'VRDR.ValueSets.PlaceOfDeath')
  - [FHIRToIJE](#F-VRDR-Mappings-PlaceOfDeath-FHIRToIJE 'VRDR.Mappings.PlaceOfDeath.FHIRToIJE')
  - [IJEToFHIR](#F-VRDR-Mappings-PlaceOfDeath-IJEToFHIR 'VRDR.Mappings.PlaceOfDeath.IJEToFHIR')
  - [Codes](#F-VRDR-ValueSets-PlaceOfDeath-Codes 'VRDR.ValueSets.PlaceOfDeath.Codes')
  - [Dead_On_Arrival_At_Hospital](#F-VRDR-ValueSets-PlaceOfDeath-Dead_On_Arrival_At_Hospital 'VRDR.ValueSets.PlaceOfDeath.Dead_On_Arrival_At_Hospital')
  - [Death_In_Home](#F-VRDR-ValueSets-PlaceOfDeath-Death_In_Home 'VRDR.ValueSets.PlaceOfDeath.Death_In_Home')
  - [Death_In_Hospice](#F-VRDR-ValueSets-PlaceOfDeath-Death_In_Hospice 'VRDR.ValueSets.PlaceOfDeath.Death_In_Hospice')
  - [Death_In_Hospital](#F-VRDR-ValueSets-PlaceOfDeath-Death_In_Hospital 'VRDR.ValueSets.PlaceOfDeath.Death_In_Hospital')
  - [Death_In_Hospital_Based_Emergency_Department_Or_Outpatient_Department](#F-VRDR-ValueSets-PlaceOfDeath-Death_In_Hospital_Based_Emergency_Department_Or_Outpatient_Department 'VRDR.ValueSets.PlaceOfDeath.Death_In_Hospital_Based_Emergency_Department_Or_Outpatient_Department')
  - [Death_In_Nursing_Home_Or_Long_Term_Care_Facility](#F-VRDR-ValueSets-PlaceOfDeath-Death_In_Nursing_Home_Or_Long_Term_Care_Facility 'VRDR.ValueSets.PlaceOfDeath.Death_In_Nursing_Home_Or_Long_Term_Care_Facility')
  - [Other](#F-VRDR-ValueSets-PlaceOfDeath-Other 'VRDR.ValueSets.PlaceOfDeath.Other')
  - [Unk](#F-VRDR-ValueSets-PlaceOfDeath-Unk 'VRDR.ValueSets.PlaceOfDeath.Unk')
- [PlaceOfInjury](#T-VRDR-Mappings-PlaceOfInjury 'VRDR.Mappings.PlaceOfInjury')
- [PlaceOfInjury](#T-VRDR-ValueSets-PlaceOfInjury 'VRDR.ValueSets.PlaceOfInjury')
  - [FHIRToIJE](#F-VRDR-Mappings-PlaceOfInjury-FHIRToIJE 'VRDR.Mappings.PlaceOfInjury.FHIRToIJE')
  - [IJEToFHIR](#F-VRDR-Mappings-PlaceOfInjury-IJEToFHIR 'VRDR.Mappings.PlaceOfInjury.IJEToFHIR')
  - [Codes](#F-VRDR-ValueSets-PlaceOfInjury-Codes 'VRDR.ValueSets.PlaceOfInjury.Codes')
  - [Farm](#F-VRDR-ValueSets-PlaceOfInjury-Farm 'VRDR.ValueSets.PlaceOfInjury.Farm')
  - [Home](#F-VRDR-ValueSets-PlaceOfInjury-Home 'VRDR.ValueSets.PlaceOfInjury.Home')
  - [Industrial_Or_Construction_Area](#F-VRDR-ValueSets-PlaceOfInjury-Industrial_Or_Construction_Area 'VRDR.ValueSets.PlaceOfInjury.Industrial_Or_Construction_Area')
  - [Other](#F-VRDR-ValueSets-PlaceOfInjury-Other 'VRDR.ValueSets.PlaceOfInjury.Other')
  - [Residential_Institution](#F-VRDR-ValueSets-PlaceOfInjury-Residential_Institution 'VRDR.ValueSets.PlaceOfInjury.Residential_Institution')
  - [School](#F-VRDR-ValueSets-PlaceOfInjury-School 'VRDR.ValueSets.PlaceOfInjury.School')
  - [Sports_Or_Recreational_Area](#F-VRDR-ValueSets-PlaceOfInjury-Sports_Or_Recreational_Area 'VRDR.ValueSets.PlaceOfInjury.Sports_Or_Recreational_Area')
  - [Street_Or_Highway](#F-VRDR-ValueSets-PlaceOfInjury-Street_Or_Highway 'VRDR.ValueSets.PlaceOfInjury.Street_Or_Highway')
  - [Trade_Or_Service_Area](#F-VRDR-ValueSets-PlaceOfInjury-Trade_Or_Service_Area 'VRDR.ValueSets.PlaceOfInjury.Trade_Or_Service_Area')
  - [Unspecified](#F-VRDR-ValueSets-PlaceOfInjury-Unspecified 'VRDR.ValueSets.PlaceOfInjury.Unspecified')
- [PregnancyStatus](#T-VRDR-Mappings-PregnancyStatus 'VRDR.Mappings.PregnancyStatus')
- [PregnancyStatus](#T-VRDR-ValueSets-PregnancyStatus 'VRDR.ValueSets.PregnancyStatus')
  - [FHIRToIJE](#F-VRDR-Mappings-PregnancyStatus-FHIRToIJE 'VRDR.Mappings.PregnancyStatus.FHIRToIJE')
  - [IJEToFHIR](#F-VRDR-Mappings-PregnancyStatus-IJEToFHIR 'VRDR.Mappings.PregnancyStatus.IJEToFHIR')
  - [Codes](#F-VRDR-ValueSets-PregnancyStatus-Codes 'VRDR.ValueSets.PregnancyStatus.Codes')
  - [Not_Applicable](#F-VRDR-ValueSets-PregnancyStatus-Not_Applicable 'VRDR.ValueSets.PregnancyStatus.Not_Applicable')
  - [Not_Pregnant_But_Pregnant_43_Days_To_1_Year_Before_Death](#F-VRDR-ValueSets-PregnancyStatus-Not_Pregnant_But_Pregnant_43_Days_To_1_Year_Before_Death 'VRDR.ValueSets.PregnancyStatus.Not_Pregnant_But_Pregnant_43_Days_To_1_Year_Before_Death')
  - [Not_Pregnant_But_Pregnant_Within_42_Days_Of_Death](#F-VRDR-ValueSets-PregnancyStatus-Not_Pregnant_But_Pregnant_Within_42_Days_Of_Death 'VRDR.ValueSets.PregnancyStatus.Not_Pregnant_But_Pregnant_Within_42_Days_Of_Death')
  - [Not_Pregnant_Within_Past_Year](#F-VRDR-ValueSets-PregnancyStatus-Not_Pregnant_Within_Past_Year 'VRDR.ValueSets.PregnancyStatus.Not_Pregnant_Within_Past_Year')
  - [Pregnant_At_Time_Of_Death](#F-VRDR-ValueSets-PregnancyStatus-Pregnant_At_Time_Of_Death 'VRDR.ValueSets.PregnancyStatus.Pregnant_At_Time_Of_Death')
  - [Unknown_If_Pregnant_Within_The_Past_Year](#F-VRDR-ValueSets-PregnancyStatus-Unknown_If_Pregnant_Within_The_Past_Year 'VRDR.ValueSets.PregnancyStatus.Unknown_If_Pregnant_Within_The_Past_Year')
- [ProfileURL](#T-VRDR-ProfileURL 'VRDR.ProfileURL')
  - [ActivityAtTimeOfDeath](#F-VRDR-ProfileURL-ActivityAtTimeOfDeath 'VRDR.ProfileURL.ActivityAtTimeOfDeath')
  - [AutomatedUnderlyingCauseOfDeath](#F-VRDR-ProfileURL-AutomatedUnderlyingCauseOfDeath 'VRDR.ProfileURL.AutomatedUnderlyingCauseOfDeath')
  - [AutopsyPerformedIndicator](#F-VRDR-ProfileURL-AutopsyPerformedIndicator 'VRDR.ProfileURL.AutopsyPerformedIndicator')
  - [BirthRecordIdentifier](#F-VRDR-ProfileURL-BirthRecordIdentifier 'VRDR.ProfileURL.BirthRecordIdentifier')
  - [CauseOfDeathCodedContentBundle](#F-VRDR-ProfileURL-CauseOfDeathCodedContentBundle 'VRDR.ProfileURL.CauseOfDeathCodedContentBundle')
  - [CauseOfDeathPart1](#F-VRDR-ProfileURL-CauseOfDeathPart1 'VRDR.ProfileURL.CauseOfDeathPart1')
  - [CauseOfDeathPart2](#F-VRDR-ProfileURL-CauseOfDeathPart2 'VRDR.ProfileURL.CauseOfDeathPart2')
  - [CauseOfDeathPathway](#F-VRDR-ProfileURL-CauseOfDeathPathway 'VRDR.ProfileURL.CauseOfDeathPathway')
  - [Certifier](#F-VRDR-ProfileURL-Certifier 'VRDR.ProfileURL.Certifier')
  - [CodedRaceAndEthnicity](#F-VRDR-ProfileURL-CodedRaceAndEthnicity 'VRDR.ProfileURL.CodedRaceAndEthnicity')
  - [CodingStatusValues](#F-VRDR-ProfileURL-CodingStatusValues 'VRDR.ProfileURL.CodingStatusValues')
  - [DeathCertificate](#F-VRDR-ProfileURL-DeathCertificate 'VRDR.ProfileURL.DeathCertificate')
  - [DeathCertificateDocument](#F-VRDR-ProfileURL-DeathCertificateDocument 'VRDR.ProfileURL.DeathCertificateDocument')
  - [DeathCertification](#F-VRDR-ProfileURL-DeathCertification 'VRDR.ProfileURL.DeathCertification')
  - [DeathDate](#F-VRDR-ProfileURL-DeathDate 'VRDR.ProfileURL.DeathDate')
  - [DeathLocation](#F-VRDR-ProfileURL-DeathLocation 'VRDR.ProfileURL.DeathLocation')
  - [Decedent](#F-VRDR-ProfileURL-Decedent 'VRDR.ProfileURL.Decedent')
  - [DecedentAge](#F-VRDR-ProfileURL-DecedentAge 'VRDR.ProfileURL.DecedentAge')
  - [DecedentDispositionMethod](#F-VRDR-ProfileURL-DecedentDispositionMethod 'VRDR.ProfileURL.DecedentDispositionMethod')
  - [DecedentEducationLevel](#F-VRDR-ProfileURL-DecedentEducationLevel 'VRDR.ProfileURL.DecedentEducationLevel')
  - [DecedentFather](#F-VRDR-ProfileURL-DecedentFather 'VRDR.ProfileURL.DecedentFather')
  - [DecedentMilitaryService](#F-VRDR-ProfileURL-DecedentMilitaryService 'VRDR.ProfileURL.DecedentMilitaryService')
  - [DecedentMother](#F-VRDR-ProfileURL-DecedentMother 'VRDR.ProfileURL.DecedentMother')
  - [DecedentPregnancyStatus](#F-VRDR-ProfileURL-DecedentPregnancyStatus 'VRDR.ProfileURL.DecedentPregnancyStatus')
  - [DecedentSpouse](#F-VRDR-ProfileURL-DecedentSpouse 'VRDR.ProfileURL.DecedentSpouse')
  - [DecedentUsualWork](#F-VRDR-ProfileURL-DecedentUsualWork 'VRDR.ProfileURL.DecedentUsualWork')
  - [DemographicCodedContentBundle](#F-VRDR-ProfileURL-DemographicCodedContentBundle 'VRDR.ProfileURL.DemographicCodedContentBundle')
  - [DispositionLocation](#F-VRDR-ProfileURL-DispositionLocation 'VRDR.ProfileURL.DispositionLocation')
  - [EmergingIssues](#F-VRDR-ProfileURL-EmergingIssues 'VRDR.ProfileURL.EmergingIssues')
  - [EntityAxisCauseOfDeath](#F-VRDR-ProfileURL-EntityAxisCauseOfDeath 'VRDR.ProfileURL.EntityAxisCauseOfDeath')
  - [ExaminerContacted](#F-VRDR-ProfileURL-ExaminerContacted 'VRDR.ProfileURL.ExaminerContacted')
  - [FuneralHome](#F-VRDR-ProfileURL-FuneralHome 'VRDR.ProfileURL.FuneralHome')
  - [InjuryIncident](#F-VRDR-ProfileURL-InjuryIncident 'VRDR.ProfileURL.InjuryIncident')
  - [InjuryLocation](#F-VRDR-ProfileURL-InjuryLocation 'VRDR.ProfileURL.InjuryLocation')
  - [InputRaceAndEthnicity](#F-VRDR-ProfileURL-InputRaceAndEthnicity 'VRDR.ProfileURL.InputRaceAndEthnicity')
  - [MannerOfDeath](#F-VRDR-ProfileURL-MannerOfDeath 'VRDR.ProfileURL.MannerOfDeath')
  - [ManualUnderlyingCauseOfDeath](#F-VRDR-ProfileURL-ManualUnderlyingCauseOfDeath 'VRDR.ProfileURL.ManualUnderlyingCauseOfDeath')
  - [PlaceOfInjury](#F-VRDR-ProfileURL-PlaceOfInjury 'VRDR.ProfileURL.PlaceOfInjury')
  - [RecordAxisCauseOfDeath](#F-VRDR-ProfileURL-RecordAxisCauseOfDeath 'VRDR.ProfileURL.RecordAxisCauseOfDeath')
  - [SurgeryDate](#F-VRDR-ProfileURL-SurgeryDate 'VRDR.ProfileURL.SurgeryDate')
  - [TobaccoUseContributedToDeath](#F-VRDR-ProfileURL-TobaccoUseContributedToDeath 'VRDR.ProfileURL.TobaccoUseContributedToDeath')
- [Property](#T-VRDR-Property 'VRDR.Property')
  - [#ctor()](#M-VRDR-Property-#ctor-System-String,VRDR-Property-Types,System-String,System-String,System-Boolean,System-String,System-Boolean,System-Int32- 'VRDR.Property.#ctor(System.String,VRDR.Property.Types,System.String,System.String,System.Boolean,System.String,System.Boolean,System.Int32)')
  - [CapturedInIJE](#F-VRDR-Property-CapturedInIJE 'VRDR.Property.CapturedInIJE')
  - [Category](#F-VRDR-Property-Category 'VRDR.Property.Category')
  - [Description](#F-VRDR-Property-Description 'VRDR.Property.Description')
  - [IGUrl](#F-VRDR-Property-IGUrl 'VRDR.Property.IGUrl')
  - [Name](#F-VRDR-Property-Name 'VRDR.Property.Name')
  - [Priority](#F-VRDR-Property-Priority 'VRDR.Property.Priority')
  - [Serialize](#F-VRDR-Property-Serialize 'VRDR.Property.Serialize')
  - [Type](#F-VRDR-Property-Type 'VRDR.Property.Type')
- [PropertyParam](#T-VRDR-PropertyParam 'VRDR.PropertyParam')
  - [#ctor()](#M-VRDR-PropertyParam-#ctor-System-String,System-String- 'VRDR.PropertyParam.#ctor(System.String,System.String)')
  - [Description](#F-VRDR-PropertyParam-Description 'VRDR.PropertyParam.Description')
  - [Key](#F-VRDR-PropertyParam-Key 'VRDR.PropertyParam.Key')
- [RaceCode](#T-VRDR-Mappings-RaceCode 'VRDR.Mappings.RaceCode')
- [RaceCode](#T-VRDR-ValueSets-RaceCode 'VRDR.ValueSets.RaceCode')
  - [FHIRToIJE](#F-VRDR-Mappings-RaceCode-FHIRToIJE 'VRDR.Mappings.RaceCode.FHIRToIJE')
  - [IJEToFHIR](#F-VRDR-Mappings-RaceCode-IJEToFHIR 'VRDR.Mappings.RaceCode.IJEToFHIR')
  - [Abenaki_Nation_Of_Missiquoi](#F-VRDR-ValueSets-RaceCode-Abenaki_Nation_Of_Missiquoi 'VRDR.ValueSets.RaceCode.Abenaki_Nation_Of_Missiquoi')
  - [Absentee_Shawnee_Tribe_Of_Indians_Of_Oklahoma](#F-VRDR-ValueSets-RaceCode-Absentee_Shawnee_Tribe_Of_Indians_Of_Oklahoma 'VRDR.ValueSets.RaceCode.Absentee_Shawnee_Tribe_Of_Indians_Of_Oklahoma')
  - [Acoma](#F-VRDR-ValueSets-RaceCode-Acoma 'VRDR.ValueSets.RaceCode.Acoma')
  - [Afghanistani](#F-VRDR-ValueSets-RaceCode-Afghanistani 'VRDR.ValueSets.RaceCode.Afghanistani')
  - [African](#F-VRDR-ValueSets-RaceCode-African 'VRDR.ValueSets.RaceCode.African')
  - [African_American](#F-VRDR-ValueSets-RaceCode-African_American 'VRDR.ValueSets.RaceCode.African_American')
  - [Afroamerican](#F-VRDR-ValueSets-RaceCode-Afroamerican 'VRDR.ValueSets.RaceCode.Afroamerican')
  - [Agdaagux_Tribe_Of_King_Cove](#F-VRDR-ValueSets-RaceCode-Agdaagux_Tribe_Of_King_Cove 'VRDR.ValueSets.RaceCode.Agdaagux_Tribe_Of_King_Cove')
  - [Agua_Caliente](#F-VRDR-ValueSets-RaceCode-Agua_Caliente 'VRDR.ValueSets.RaceCode.Agua_Caliente')
  - [Agua_Caliente_Band_Of_Cahuilla_Indians](#F-VRDR-ValueSets-RaceCode-Agua_Caliente_Band_Of_Cahuilla_Indians 'VRDR.ValueSets.RaceCode.Agua_Caliente_Band_Of_Cahuilla_Indians')
  - [Ahtna](#F-VRDR-ValueSets-RaceCode-Ahtna 'VRDR.ValueSets.RaceCode.Ahtna')
  - [Akchin](#F-VRDR-ValueSets-RaceCode-Akchin 'VRDR.ValueSets.RaceCode.Akchin')
  - [Akiachak_Native_Community](#F-VRDR-ValueSets-RaceCode-Akiachak_Native_Community 'VRDR.ValueSets.RaceCode.Akiachak_Native_Community')
  - [Akiak_Native_Community](#F-VRDR-ValueSets-RaceCode-Akiak_Native_Community 'VRDR.ValueSets.RaceCode.Akiak_Native_Community')
  - [Alabama_Coushatta_Tribes_Of_Texas](#F-VRDR-ValueSets-RaceCode-Alabama_Coushatta_Tribes_Of_Texas 'VRDR.ValueSets.RaceCode.Alabama_Coushatta_Tribes_Of_Texas')
  - [Alabama_Creek](#F-VRDR-ValueSets-RaceCode-Alabama_Creek 'VRDR.ValueSets.RaceCode.Alabama_Creek')
  - [Alabama_Quassarte_Tribal_Town](#F-VRDR-ValueSets-RaceCode-Alabama_Quassarte_Tribal_Town 'VRDR.ValueSets.RaceCode.Alabama_Quassarte_Tribal_Town')
  - [Alamo_Navajo](#F-VRDR-ValueSets-RaceCode-Alamo_Navajo 'VRDR.ValueSets.RaceCode.Alamo_Navajo')
  - [Alanvik](#F-VRDR-ValueSets-RaceCode-Alanvik 'VRDR.ValueSets.RaceCode.Alanvik')
  - [Alaska_Indian](#F-VRDR-ValueSets-RaceCode-Alaska_Indian 'VRDR.ValueSets.RaceCode.Alaska_Indian')
  - [Alaska_Native](#F-VRDR-ValueSets-RaceCode-Alaska_Native 'VRDR.ValueSets.RaceCode.Alaska_Native')
  - [Alaskan_Athabascan](#F-VRDR-ValueSets-RaceCode-Alaskan_Athabascan 'VRDR.ValueSets.RaceCode.Alaskan_Athabascan')
  - [Alatna_Village](#F-VRDR-ValueSets-RaceCode-Alatna_Village 'VRDR.ValueSets.RaceCode.Alatna_Village')
  - [Albanian](#F-VRDR-ValueSets-RaceCode-Albanian 'VRDR.ValueSets.RaceCode.Albanian')
  - [Aleut](#F-VRDR-ValueSets-RaceCode-Aleut 'VRDR.ValueSets.RaceCode.Aleut')
  - [Aleut_Corporation](#F-VRDR-ValueSets-RaceCode-Aleut_Corporation 'VRDR.ValueSets.RaceCode.Aleut_Corporation')
  - [Aleutian](#F-VRDR-ValueSets-RaceCode-Aleutian 'VRDR.ValueSets.RaceCode.Aleutian')
  - [Aleutian_Islander](#F-VRDR-ValueSets-RaceCode-Aleutian_Islander 'VRDR.ValueSets.RaceCode.Aleutian_Islander')
  - [Alexander](#F-VRDR-ValueSets-RaceCode-Alexander 'VRDR.ValueSets.RaceCode.Alexander')
  - [Algaaciq_Native_Village](#F-VRDR-ValueSets-RaceCode-Algaaciq_Native_Village 'VRDR.ValueSets.RaceCode.Algaaciq_Native_Village')
  - [Algonquian](#F-VRDR-ValueSets-RaceCode-Algonquian 'VRDR.ValueSets.RaceCode.Algonquian')
  - [Alien_Canyon](#F-VRDR-ValueSets-RaceCode-Alien_Canyon 'VRDR.ValueSets.RaceCode.Alien_Canyon')
  - [Allakaket_Village](#F-VRDR-ValueSets-RaceCode-Allakaket_Village 'VRDR.ValueSets.RaceCode.Allakaket_Village')
  - [Alpine](#F-VRDR-ValueSets-RaceCode-Alpine 'VRDR.ValueSets.RaceCode.Alpine')
  - [Alsea](#F-VRDR-ValueSets-RaceCode-Alsea 'VRDR.ValueSets.RaceCode.Alsea')
  - [Alturas_Indian_Rancheria](#F-VRDR-ValueSets-RaceCode-Alturas_Indian_Rancheria 'VRDR.ValueSets.RaceCode.Alturas_Indian_Rancheria')
  - [Alutiiq](#F-VRDR-ValueSets-RaceCode-Alutiiq 'VRDR.ValueSets.RaceCode.Alutiiq')
  - [Alutiiq_Aleut](#F-VRDR-ValueSets-RaceCode-Alutiiq_Aleut 'VRDR.ValueSets.RaceCode.Alutiiq_Aleut')
  - [Amerasian](#F-VRDR-ValueSets-RaceCode-Amerasian 'VRDR.ValueSets.RaceCode.Amerasian')
  - [American_Eskimo](#F-VRDR-ValueSets-RaceCode-American_Eskimo 'VRDR.ValueSets.RaceCode.American_Eskimo')
  - [American_Indian](#F-VRDR-ValueSets-RaceCode-American_Indian 'VRDR.ValueSets.RaceCode.American_Indian')
  - [American_Indian_Checkbox](#F-VRDR-ValueSets-RaceCode-American_Indian_Checkbox 'VRDR.ValueSets.RaceCode.American_Indian_Checkbox')
  - [Anaktuvuk](#F-VRDR-ValueSets-RaceCode-Anaktuvuk 'VRDR.ValueSets.RaceCode.Anaktuvuk')
  - [Angoon_Community_Association](#F-VRDR-ValueSets-RaceCode-Angoon_Community_Association 'VRDR.ValueSets.RaceCode.Angoon_Community_Association')
  - [Anstohini](#F-VRDR-ValueSets-RaceCode-Anstohini 'VRDR.ValueSets.RaceCode.Anstohini')
  - [Anvik_Village](#F-VRDR-ValueSets-RaceCode-Anvik_Village 'VRDR.ValueSets.RaceCode.Anvik_Village')
  - [Apache](#F-VRDR-ValueSets-RaceCode-Apache 'VRDR.ValueSets.RaceCode.Apache')
  - [Arab](#F-VRDR-ValueSets-RaceCode-Arab 'VRDR.ValueSets.RaceCode.Arab')
  - [Arapahoe](#F-VRDR-ValueSets-RaceCode-Arapahoe 'VRDR.ValueSets.RaceCode.Arapahoe')
  - [Arctic_Slope_Corporation](#F-VRDR-ValueSets-RaceCode-Arctic_Slope_Corporation 'VRDR.ValueSets.RaceCode.Arctic_Slope_Corporation')
  - [Arctic_Village](#F-VRDR-ValueSets-RaceCode-Arctic_Village 'VRDR.ValueSets.RaceCode.Arctic_Village')
  - [Argentinean](#F-VRDR-ValueSets-RaceCode-Argentinean 'VRDR.ValueSets.RaceCode.Argentinean')
  - [Arikara](#F-VRDR-ValueSets-RaceCode-Arikara 'VRDR.ValueSets.RaceCode.Arikara')
  - [Arizona_Tewa](#F-VRDR-ValueSets-RaceCode-Arizona_Tewa 'VRDR.ValueSets.RaceCode.Arizona_Tewa')
  - [Armenian](#F-VRDR-ValueSets-RaceCode-Armenian 'VRDR.ValueSets.RaceCode.Armenian')
  - [Aroostook_Band](#F-VRDR-ValueSets-RaceCode-Aroostook_Band 'VRDR.ValueSets.RaceCode.Aroostook_Band')
  - [Aruba_Islander](#F-VRDR-ValueSets-RaceCode-Aruba_Islander 'VRDR.ValueSets.RaceCode.Aruba_Islander')
  - [Aryan](#F-VRDR-ValueSets-RaceCode-Aryan 'VRDR.ValueSets.RaceCode.Aryan')
  - [Asacarsarmiut_Tribe](#F-VRDR-ValueSets-RaceCode-Asacarsarmiut_Tribe 'VRDR.ValueSets.RaceCode.Asacarsarmiut_Tribe')
  - [Asian](#F-VRDR-ValueSets-RaceCode-Asian 'VRDR.ValueSets.RaceCode.Asian')
  - [Asian2](#F-VRDR-ValueSets-RaceCode-Asian2 'VRDR.ValueSets.RaceCode.Asian2')
  - [Asian_Indian](#F-VRDR-ValueSets-RaceCode-Asian_Indian 'VRDR.ValueSets.RaceCode.Asian_Indian')
  - [Asiatic](#F-VRDR-ValueSets-RaceCode-Asiatic 'VRDR.ValueSets.RaceCode.Asiatic')
  - [Assiniboine](#F-VRDR-ValueSets-RaceCode-Assiniboine 'VRDR.ValueSets.RaceCode.Assiniboine')
  - [Assiniboine_Sioux](#F-VRDR-ValueSets-RaceCode-Assiniboine_Sioux 'VRDR.ValueSets.RaceCode.Assiniboine_Sioux')
  - [Assyrian](#F-VRDR-ValueSets-RaceCode-Assyrian 'VRDR.ValueSets.RaceCode.Assyrian')
  - [Atqasuk_Village](#F-VRDR-ValueSets-RaceCode-Atqasuk_Village 'VRDR.ValueSets.RaceCode.Atqasuk_Village')
  - [Atsina](#F-VRDR-ValueSets-RaceCode-Atsina 'VRDR.ValueSets.RaceCode.Atsina')
  - [Attacapa](#F-VRDR-ValueSets-RaceCode-Attacapa 'VRDR.ValueSets.RaceCode.Attacapa')
  - [Augustine](#F-VRDR-ValueSets-RaceCode-Augustine 'VRDR.ValueSets.RaceCode.Augustine')
  - [Azerbaijani](#F-VRDR-ValueSets-RaceCode-Azerbaijani 'VRDR.ValueSets.RaceCode.Azerbaijani')
  - [Bad_River_Band_Of_The_Lake_Superior_Tribe](#F-VRDR-ValueSets-RaceCode-Bad_River_Band_Of_The_Lake_Superior_Tribe 'VRDR.ValueSets.RaceCode.Bad_River_Band_Of_The_Lake_Superior_Tribe')
  - [Bahamian](#F-VRDR-ValueSets-RaceCode-Bahamian 'VRDR.ValueSets.RaceCode.Bahamian')
  - [Bangladeshi](#F-VRDR-ValueSets-RaceCode-Bangladeshi 'VRDR.ValueSets.RaceCode.Bangladeshi')
  - [Bannock](#F-VRDR-ValueSets-RaceCode-Bannock 'VRDR.ValueSets.RaceCode.Bannock')
  - [Barbadian](#F-VRDR-ValueSets-RaceCode-Barbadian 'VRDR.ValueSets.RaceCode.Barbadian')
  - [Barona_Group_Of_Capitan_Grande_Band](#F-VRDR-ValueSets-RaceCode-Barona_Group_Of_Capitan_Grande_Band 'VRDR.ValueSets.RaceCode.Barona_Group_Of_Capitan_Grande_Band')
  - [Barrio_Libre](#F-VRDR-ValueSets-RaceCode-Barrio_Libre 'VRDR.ValueSets.RaceCode.Barrio_Libre')
  - [Battle_Mountain](#F-VRDR-ValueSets-RaceCode-Battle_Mountain 'VRDR.ValueSets.RaceCode.Battle_Mountain')
  - [Bay_Mills_Indian_Community_Of_The_Sault_Ste_Marie_Band](#F-VRDR-ValueSets-RaceCode-Bay_Mills_Indian_Community_Of_The_Sault_Ste_Marie_Band 'VRDR.ValueSets.RaceCode.Bay_Mills_Indian_Community_Of_The_Sault_Ste_Marie_Band')
  - [Bear_River_Band_Of_Rohnerville_Rancheria](#F-VRDR-ValueSets-RaceCode-Bear_River_Band_Of_Rohnerville_Rancheria 'VRDR.ValueSets.RaceCode.Bear_River_Band_Of_Rohnerville_Rancheria')
  - [Beaver_Village](#F-VRDR-ValueSets-RaceCode-Beaver_Village 'VRDR.ValueSets.RaceCode.Beaver_Village')
  - [Belizean](#F-VRDR-ValueSets-RaceCode-Belizean 'VRDR.ValueSets.RaceCode.Belizean')
  - [Bering_Straits_Inupiat](#F-VRDR-ValueSets-RaceCode-Bering_Straits_Inupiat 'VRDR.ValueSets.RaceCode.Bering_Straits_Inupiat')
  - [Bermudan](#F-VRDR-ValueSets-RaceCode-Bermudan 'VRDR.ValueSets.RaceCode.Bermudan')
  - [Berry_Creek_Rancheria_Of_Maidu_Indians](#F-VRDR-ValueSets-RaceCode-Berry_Creek_Rancheria_Of_Maidu_Indians 'VRDR.ValueSets.RaceCode.Berry_Creek_Rancheria_Of_Maidu_Indians')
  - [Bhutanese](#F-VRDR-ValueSets-RaceCode-Bhutanese 'VRDR.ValueSets.RaceCode.Bhutanese')
  - [Big_Cypress](#F-VRDR-ValueSets-RaceCode-Big_Cypress 'VRDR.ValueSets.RaceCode.Big_Cypress')
  - [Big_Lagoon_Rancheria](#F-VRDR-ValueSets-RaceCode-Big_Lagoon_Rancheria 'VRDR.ValueSets.RaceCode.Big_Lagoon_Rancheria')
  - [Big_Pine_Band_Of_Owens_Valley_Paiuteshoshone](#F-VRDR-ValueSets-RaceCode-Big_Pine_Band_Of_Owens_Valley_Paiuteshoshone 'VRDR.ValueSets.RaceCode.Big_Pine_Band_Of_Owens_Valley_Paiuteshoshone')
  - [Big_Sandy_Rancheria](#F-VRDR-ValueSets-RaceCode-Big_Sandy_Rancheria 'VRDR.ValueSets.RaceCode.Big_Sandy_Rancheria')
  - [Big_Valley_Rancheria_Of_Pomo_And_Pit_River_Indians](#F-VRDR-ValueSets-RaceCode-Big_Valley_Rancheria_Of_Pomo_And_Pit_River_Indians 'VRDR.ValueSets.RaceCode.Big_Valley_Rancheria_Of_Pomo_And_Pit_River_Indians')
  - [Biloxi](#F-VRDR-ValueSets-RaceCode-Biloxi 'VRDR.ValueSets.RaceCode.Biloxi')
  - [Biracial](#F-VRDR-ValueSets-RaceCode-Biracial 'VRDR.ValueSets.RaceCode.Biracial')
  - [Birch_Crcek_Village](#F-VRDR-ValueSets-RaceCode-Birch_Crcek_Village 'VRDR.ValueSets.RaceCode.Birch_Crcek_Village')
  - [Bishop](#F-VRDR-ValueSets-RaceCode-Bishop 'VRDR.ValueSets.RaceCode.Bishop')
  - [Black](#F-VRDR-ValueSets-RaceCode-Black 'VRDR.ValueSets.RaceCode.Black')
  - [Black_Or_African_American](#F-VRDR-ValueSets-RaceCode-Black_Or_African_American 'VRDR.ValueSets.RaceCode.Black_Or_African_American')
  - [Blackfeet](#F-VRDR-ValueSets-RaceCode-Blackfeet 'VRDR.ValueSets.RaceCode.Blackfeet')
  - [Blackfoot_Sioux](#F-VRDR-ValueSets-RaceCode-Blackfoot_Sioux 'VRDR.ValueSets.RaceCode.Blackfoot_Sioux')
  - [Blue_Lake_Rancheria](#F-VRDR-ValueSets-RaceCode-Blue_Lake_Rancheria 'VRDR.ValueSets.RaceCode.Blue_Lake_Rancheria')
  - [Bois_Forte_Nett_Lake_Band_Of_Chippewa](#F-VRDR-ValueSets-RaceCode-Bois_Forte_Nett_Lake_Band_Of_Chippewa 'VRDR.ValueSets.RaceCode.Bois_Forte_Nett_Lake_Band_Of_Chippewa')
  - [Bolivian](#F-VRDR-ValueSets-RaceCode-Bolivian 'VRDR.ValueSets.RaceCode.Bolivian')
  - [Bosnian](#F-VRDR-ValueSets-RaceCode-Bosnian 'VRDR.ValueSets.RaceCode.Bosnian')
  - [Botswana](#F-VRDR-ValueSets-RaceCode-Botswana 'VRDR.ValueSets.RaceCode.Botswana')
  - [Brazilian](#F-VRDR-ValueSets-RaceCode-Brazilian 'VRDR.ValueSets.RaceCode.Brazilian')
  - [Bridgeport_Paiute_Indian_Colony](#F-VRDR-ValueSets-RaceCode-Bridgeport_Paiute_Indian_Colony 'VRDR.ValueSets.RaceCode.Bridgeport_Paiute_Indian_Colony')
  - [Brighton](#F-VRDR-ValueSets-RaceCode-Brighton 'VRDR.ValueSets.RaceCode.Brighton')
  - [Bristol_Bay](#F-VRDR-ValueSets-RaceCode-Bristol_Bay 'VRDR.ValueSets.RaceCode.Bristol_Bay')
  - [Bristol_Bay_Aleut](#F-VRDR-ValueSets-RaceCode-Bristol_Bay_Aleut 'VRDR.ValueSets.RaceCode.Bristol_Bay_Aleut')
  - [Brotherton](#F-VRDR-ValueSets-RaceCode-Brotherton 'VRDR.ValueSets.RaceCode.Brotherton')
  - [Brown](#F-VRDR-ValueSets-RaceCode-Brown 'VRDR.ValueSets.RaceCode.Brown')
  - [Brule_Sioux](#F-VRDR-ValueSets-RaceCode-Brule_Sioux 'VRDR.ValueSets.RaceCode.Brule_Sioux')
  - [Buena_Vista_Rancheria_Of_Mewuk_Indians_Of_California](#F-VRDR-ValueSets-RaceCode-Buena_Vista_Rancheria_Of_Mewuk_Indians_Of_California 'VRDR.ValueSets.RaceCode.Buena_Vista_Rancheria_Of_Mewuk_Indians_Of_California')
  - [Burmese](#F-VRDR-ValueSets-RaceCode-Burmese 'VRDR.ValueSets.RaceCode.Burmese')
  - [Burns_Paiute_Tribe](#F-VRDR-ValueSets-RaceCode-Burns_Paiute_Tribe 'VRDR.ValueSets.RaceCode.Burns_Paiute_Tribe')
  - [Burt_Lake_Band](#F-VRDR-ValueSets-RaceCode-Burt_Lake_Band 'VRDR.ValueSets.RaceCode.Burt_Lake_Band')
  - [Burt_Lake_Chippewa](#F-VRDR-ValueSets-RaceCode-Burt_Lake_Chippewa 'VRDR.ValueSets.RaceCode.Burt_Lake_Chippewa')
  - [Burt_Lake_Ottawa](#F-VRDR-ValueSets-RaceCode-Burt_Lake_Ottawa 'VRDR.ValueSets.RaceCode.Burt_Lake_Ottawa')
  - [Bushwacker](#F-VRDR-ValueSets-RaceCode-Bushwacker 'VRDR.ValueSets.RaceCode.Bushwacker')
  - [Cabazon_Band_Of_Cahuilla_Mission_Indians](#F-VRDR-ValueSets-RaceCode-Cabazon_Band_Of_Cahuilla_Mission_Indians 'VRDR.ValueSets.RaceCode.Cabazon_Band_Of_Cahuilla_Mission_Indians')
  - [Cachil_Dehe_Band_Of_Wintun_Indians_Of_The_Colusa_Rancheria](#F-VRDR-ValueSets-RaceCode-Cachil_Dehe_Band_Of_Wintun_Indians_Of_The_Colusa_Rancheria 'VRDR.ValueSets.RaceCode.Cachil_Dehe_Band_Of_Wintun_Indians_Of_The_Colusa_Rancheria')
  - [Caddo](#F-VRDR-ValueSets-RaceCode-Caddo 'VRDR.ValueSets.RaceCode.Caddo')
  - [Caddo_Adais_Indians](#F-VRDR-ValueSets-RaceCode-Caddo_Adais_Indians 'VRDR.ValueSets.RaceCode.Caddo_Adais_Indians')
  - [Caddo_Indian_Tribe_Of_Oklahoma](#F-VRDR-ValueSets-RaceCode-Caddo_Indian_Tribe_Of_Oklahoma 'VRDR.ValueSets.RaceCode.Caddo_Indian_Tribe_Of_Oklahoma')
  - [Cahto_Indian_Tribe_Of_The_Laytonville_Rancheria](#F-VRDR-ValueSets-RaceCode-Cahto_Indian_Tribe_Of_The_Laytonville_Rancheria 'VRDR.ValueSets.RaceCode.Cahto_Indian_Tribe_Of_The_Laytonville_Rancheria')
  - [Cahuilla](#F-VRDR-ValueSets-RaceCode-Cahuilla 'VRDR.ValueSets.RaceCode.Cahuilla')
  - [Cahuilla_Band_Of_Mission_Indians](#F-VRDR-ValueSets-RaceCode-Cahuilla_Band_Of_Mission_Indians 'VRDR.ValueSets.RaceCode.Cahuilla_Band_Of_Mission_Indians')
  - [Cajun](#F-VRDR-ValueSets-RaceCode-Cajun 'VRDR.ValueSets.RaceCode.Cajun')
  - [California_Tribes_N_E_C](#F-VRDR-ValueSets-RaceCode-California_Tribes_N_E_C 'VRDR.ValueSets.RaceCode.California_Tribes_N_E_C')
  - [Californio](#F-VRDR-ValueSets-RaceCode-Californio 'VRDR.ValueSets.RaceCode.Californio')
  - [Calista](#F-VRDR-ValueSets-RaceCode-Calista 'VRDR.ValueSets.RaceCode.Calista')
  - [Cambodian](#F-VRDR-ValueSets-RaceCode-Cambodian 'VRDR.ValueSets.RaceCode.Cambodian')
  - [Campo_Band_Of_Diegueno_Mission_Indians](#F-VRDR-ValueSets-RaceCode-Campo_Band_Of_Diegueno_Mission_Indians 'VRDR.ValueSets.RaceCode.Campo_Band_Of_Diegueno_Mission_Indians')
  - [Canadian](#F-VRDR-ValueSets-RaceCode-Canadian 'VRDR.ValueSets.RaceCode.Canadian')
  - [Canadian_Indian](#F-VRDR-ValueSets-RaceCode-Canadian_Indian 'VRDR.ValueSets.RaceCode.Canadian_Indian')
  - [Cape_Verdean](#F-VRDR-ValueSets-RaceCode-Cape_Verdean 'VRDR.ValueSets.RaceCode.Cape_Verdean')
  - [Capitan_Grande_Band_Of_Diegueno_Mission_Indians](#F-VRDR-ValueSets-RaceCode-Capitan_Grande_Band_Of_Diegueno_Mission_Indians 'VRDR.ValueSets.RaceCode.Capitan_Grande_Band_Of_Diegueno_Mission_Indians')
  - [Carolinian](#F-VRDR-ValueSets-RaceCode-Carolinian 'VRDR.ValueSets.RaceCode.Carolinian')
  - [Carson_Colony](#F-VRDR-ValueSets-RaceCode-Carson_Colony 'VRDR.ValueSets.RaceCode.Carson_Colony')
  - [Catawba_Indian_Nation](#F-VRDR-ValueSets-RaceCode-Catawba_Indian_Nation 'VRDR.ValueSets.RaceCode.Catawba_Indian_Nation')
  - [Cayenne](#F-VRDR-ValueSets-RaceCode-Cayenne 'VRDR.ValueSets.RaceCode.Cayenne')
  - [Cayman_Islander](#F-VRDR-ValueSets-RaceCode-Cayman_Islander 'VRDR.ValueSets.RaceCode.Cayman_Islander')
  - [Cayuga_Nation](#F-VRDR-ValueSets-RaceCode-Cayuga_Nation 'VRDR.ValueSets.RaceCode.Cayuga_Nation')
  - [Cayuse](#F-VRDR-ValueSets-RaceCode-Cayuse 'VRDR.ValueSets.RaceCode.Cayuse')
  - [Cedarville_Rancheria](#F-VRDR-ValueSets-RaceCode-Cedarville_Rancheria 'VRDR.ValueSets.RaceCode.Cedarville_Rancheria')
  - [Celilo](#F-VRDR-ValueSets-RaceCode-Celilo 'VRDR.ValueSets.RaceCode.Celilo')
  - [Central_American](#F-VRDR-ValueSets-RaceCode-Central_American 'VRDR.ValueSets.RaceCode.Central_American')
  - [Central_American_Indian](#F-VRDR-ValueSets-RaceCode-Central_American_Indian 'VRDR.ValueSets.RaceCode.Central_American_Indian')
  - [Central_Council_Of_The_Tlingit_And_Haida_Indian_Tribes](#F-VRDR-ValueSets-RaceCode-Central_Council_Of_The_Tlingit_And_Haida_Indian_Tribes 'VRDR.ValueSets.RaceCode.Central_Council_Of_The_Tlingit_And_Haida_Indian_Tribes')
  - [Central_Pomo](#F-VRDR-ValueSets-RaceCode-Central_Pomo 'VRDR.ValueSets.RaceCode.Central_Pomo')
  - [Chalkyitsik_Village](#F-VRDR-ValueSets-RaceCode-Chalkyitsik_Village 'VRDR.ValueSets.RaceCode.Chalkyitsik_Village')
  - [Chamorro](#F-VRDR-ValueSets-RaceCode-Chamorro 'VRDR.ValueSets.RaceCode.Chamorro')
  - [Chaubunagungameg_Nipmuc](#F-VRDR-ValueSets-RaceCode-Chaubunagungameg_Nipmuc 'VRDR.ValueSets.RaceCode.Chaubunagungameg_Nipmuc')
  - [Chehalis](#F-VRDR-ValueSets-RaceCode-Chehalis 'VRDR.ValueSets.RaceCode.Chehalis')
  - [Chemakuan](#F-VRDR-ValueSets-RaceCode-Chemakuan 'VRDR.ValueSets.RaceCode.Chemakuan')
  - [Chemehuevi](#F-VRDR-ValueSets-RaceCode-Chemehuevi 'VRDR.ValueSets.RaceCode.Chemehuevi')
  - [Cherae_Indian_Community_Of_Trinidad_Rancheria](#F-VRDR-ValueSets-RaceCode-Cherae_Indian_Community_Of_Trinidad_Rancheria 'VRDR.ValueSets.RaceCode.Cherae_Indian_Community_Of_Trinidad_Rancheria')
  - [Cherokee](#F-VRDR-ValueSets-RaceCode-Cherokee 'VRDR.ValueSets.RaceCode.Cherokee')
  - [Cherokee_Alabama](#F-VRDR-ValueSets-RaceCode-Cherokee_Alabama 'VRDR.ValueSets.RaceCode.Cherokee_Alabama')
  - [Cherokee_Of_Georgia](#F-VRDR-ValueSets-RaceCode-Cherokee_Of_Georgia 'VRDR.ValueSets.RaceCode.Cherokee_Of_Georgia')
  - [Cherokee_Shawnee](#F-VRDR-ValueSets-RaceCode-Cherokee_Shawnee 'VRDR.ValueSets.RaceCode.Cherokee_Shawnee')
  - [Cherokees_Of_Northeast_Alabama](#F-VRDR-ValueSets-RaceCode-Cherokees_Of_Northeast_Alabama 'VRDR.ValueSets.RaceCode.Cherokees_Of_Northeast_Alabama')
  - [Cherokees_Of_Southeast_Alabama](#F-VRDR-ValueSets-RaceCode-Cherokees_Of_Southeast_Alabama 'VRDR.ValueSets.RaceCode.Cherokees_Of_Southeast_Alabama')
  - [Chevak_Native_Village](#F-VRDR-ValueSets-RaceCode-Chevak_Native_Village 'VRDR.ValueSets.RaceCode.Chevak_Native_Village')
  - [Cheyenne](#F-VRDR-ValueSets-RaceCode-Cheyenne 'VRDR.ValueSets.RaceCode.Cheyenne')
  - [Cheyenne_Arapaho](#F-VRDR-ValueSets-RaceCode-Cheyenne_Arapaho 'VRDR.ValueSets.RaceCode.Cheyenne_Arapaho')
  - [Cheyenne_River_Sioux](#F-VRDR-ValueSets-RaceCode-Cheyenne_River_Sioux 'VRDR.ValueSets.RaceCode.Cheyenne_River_Sioux')
  - [Chicano](#F-VRDR-ValueSets-RaceCode-Chicano 'VRDR.ValueSets.RaceCode.Chicano')
  - [Chickahominy_Eastern_Band](#F-VRDR-ValueSets-RaceCode-Chickahominy_Eastern_Band 'VRDR.ValueSets.RaceCode.Chickahominy_Eastern_Band')
  - [Chickahominy_Indian_Tribe](#F-VRDR-ValueSets-RaceCode-Chickahominy_Indian_Tribe 'VRDR.ValueSets.RaceCode.Chickahominy_Indian_Tribe')
  - [Chickaloon_Native_Village](#F-VRDR-ValueSets-RaceCode-Chickaloon_Native_Village 'VRDR.ValueSets.RaceCode.Chickaloon_Native_Village')
  - [Chickasaw](#F-VRDR-ValueSets-RaceCode-Chickasaw 'VRDR.ValueSets.RaceCode.Chickasaw')
  - [Chicken_Ranch_Rancheria_Of_Mewuk_Indians](#F-VRDR-ValueSets-RaceCode-Chicken_Ranch_Rancheria_Of_Mewuk_Indians 'VRDR.ValueSets.RaceCode.Chicken_Ranch_Rancheria_Of_Mewuk_Indians')
  - [Chignik_Lake_Village](#F-VRDR-ValueSets-RaceCode-Chignik_Lake_Village 'VRDR.ValueSets.RaceCode.Chignik_Lake_Village')
  - [Chilean](#F-VRDR-ValueSets-RaceCode-Chilean 'VRDR.ValueSets.RaceCode.Chilean')
  - [Chilkat_Indian_Village](#F-VRDR-ValueSets-RaceCode-Chilkat_Indian_Village 'VRDR.ValueSets.RaceCode.Chilkat_Indian_Village')
  - [Chilkoot_Indian_Association](#F-VRDR-ValueSets-RaceCode-Chilkoot_Indian_Association 'VRDR.ValueSets.RaceCode.Chilkoot_Indian_Association')
  - [Chimariko](#F-VRDR-ValueSets-RaceCode-Chimariko 'VRDR.ValueSets.RaceCode.Chimariko')
  - [Chinese](#F-VRDR-ValueSets-RaceCode-Chinese 'VRDR.ValueSets.RaceCode.Chinese')
  - [Chinik_Eskimo_Community](#F-VRDR-ValueSets-RaceCode-Chinik_Eskimo_Community 'VRDR.ValueSets.RaceCode.Chinik_Eskimo_Community')
  - [Chinook](#F-VRDR-ValueSets-RaceCode-Chinook 'VRDR.ValueSets.RaceCode.Chinook')
  - [Chippewa](#F-VRDR-ValueSets-RaceCode-Chippewa 'VRDR.ValueSets.RaceCode.Chippewa')
  - [Chiricahua](#F-VRDR-ValueSets-RaceCode-Chiricahua 'VRDR.ValueSets.RaceCode.Chiricahua')
  - [Chitimacha_Tribe_Of_Louisiana](#F-VRDR-ValueSets-RaceCode-Chitimacha_Tribe_Of_Louisiana 'VRDR.ValueSets.RaceCode.Chitimacha_Tribe_Of_Louisiana')
  - [Chocolate](#F-VRDR-ValueSets-RaceCode-Chocolate 'VRDR.ValueSets.RaceCode.Chocolate')
  - [Choctaw](#F-VRDR-ValueSets-RaceCode-Choctaw 'VRDR.ValueSets.RaceCode.Choctaw')
  - [Choctaw_Apache_Community_Of_Ebarb](#F-VRDR-ValueSets-RaceCode-Choctaw_Apache_Community_Of_Ebarb 'VRDR.ValueSets.RaceCode.Choctaw_Apache_Community_Of_Ebarb')
  - [Chugach_Aleut](#F-VRDR-ValueSets-RaceCode-Chugach_Aleut 'VRDR.ValueSets.RaceCode.Chugach_Aleut')
  - [Chugach_Corporation](#F-VRDR-ValueSets-RaceCode-Chugach_Corporation 'VRDR.ValueSets.RaceCode.Chugach_Corporation')
  - [Chuloonawick_Native_Village](#F-VRDR-ValueSets-RaceCode-Chuloonawick_Native_Village 'VRDR.ValueSets.RaceCode.Chuloonawick_Native_Village')
  - [Chumash](#F-VRDR-ValueSets-RaceCode-Chumash 'VRDR.ValueSets.RaceCode.Chumash')
  - [Chuukese](#F-VRDR-ValueSets-RaceCode-Chuukese 'VRDR.ValueSets.RaceCode.Chuukese')
  - [Circle_Native_Community](#F-VRDR-ValueSets-RaceCode-Circle_Native_Community 'VRDR.ValueSets.RaceCode.Circle_Native_Community')
  - [Citizen_Potawatomi_Nation](#F-VRDR-ValueSets-RaceCode-Citizen_Potawatomi_Nation 'VRDR.ValueSets.RaceCode.Citizen_Potawatomi_Nation')
  - [Clatsop](#F-VRDR-ValueSets-RaceCode-Clatsop 'VRDR.ValueSets.RaceCode.Clatsop')
  - [Clear_Lake](#F-VRDR-ValueSets-RaceCode-Clear_Lake 'VRDR.ValueSets.RaceCode.Clear_Lake')
  - [Clifton_Choctaw](#F-VRDR-ValueSets-RaceCode-Clifton_Choctaw 'VRDR.ValueSets.RaceCode.Clifton_Choctaw')
  - [Cloverdale_Rancheria](#F-VRDR-ValueSets-RaceCode-Cloverdale_Rancheria 'VRDR.ValueSets.RaceCode.Cloverdale_Rancheria')
  - [Coast_Miwok](#F-VRDR-ValueSets-RaceCode-Coast_Miwok 'VRDR.ValueSets.RaceCode.Coast_Miwok')
  - [Coast_Yurok](#F-VRDR-ValueSets-RaceCode-Coast_Yurok 'VRDR.ValueSets.RaceCode.Coast_Yurok')
  - [Cochiti](#F-VRDR-ValueSets-RaceCode-Cochiti 'VRDR.ValueSets.RaceCode.Cochiti')
  - [Cocopah_Tribe_Of_Arizona](#F-VRDR-ValueSets-RaceCode-Cocopah_Tribe_Of_Arizona 'VRDR.ValueSets.RaceCode.Cocopah_Tribe_Of_Arizona')
  - [Codes](#F-VRDR-ValueSets-RaceCode-Codes 'VRDR.ValueSets.RaceCode.Codes')
  - [Coe_Clan](#F-VRDR-ValueSets-RaceCode-Coe_Clan 'VRDR.ValueSets.RaceCode.Coe_Clan')
  - [Coeur_Dalene](#F-VRDR-ValueSets-RaceCode-Coeur_Dalene 'VRDR.ValueSets.RaceCode.Coeur_Dalene')
  - [Coffee](#F-VRDR-ValueSets-RaceCode-Coffee 'VRDR.ValueSets.RaceCode.Coffee')
  - [Coharie](#F-VRDR-ValueSets-RaceCode-Coharie 'VRDR.ValueSets.RaceCode.Coharie')
  - [Cold_Springs_Rancheria](#F-VRDR-ValueSets-RaceCode-Cold_Springs_Rancheria 'VRDR.ValueSets.RaceCode.Cold_Springs_Rancheria')
  - [Colombian](#F-VRDR-ValueSets-RaceCode-Colombian 'VRDR.ValueSets.RaceCode.Colombian')
  - [Colorado_River](#F-VRDR-ValueSets-RaceCode-Colorado_River 'VRDR.ValueSets.RaceCode.Colorado_River')
  - [Columbia](#F-VRDR-ValueSets-RaceCode-Columbia 'VRDR.ValueSets.RaceCode.Columbia')
  - [Columbia_River_Chinook](#F-VRDR-ValueSets-RaceCode-Columbia_River_Chinook 'VRDR.ValueSets.RaceCode.Columbia_River_Chinook')
  - [Colville](#F-VRDR-ValueSets-RaceCode-Colville 'VRDR.ValueSets.RaceCode.Colville')
  - [Comanche](#F-VRDR-ValueSets-RaceCode-Comanche 'VRDR.ValueSets.RaceCode.Comanche')
  - [Confederated_Tribes_Of_The_Siletz_Reservation](#F-VRDR-ValueSets-RaceCode-Confederated_Tribes_Of_The_Siletz_Reservation 'VRDR.ValueSets.RaceCode.Confederated_Tribes_Of_The_Siletz_Reservation')
  - [Cook_Inlet](#F-VRDR-ValueSets-RaceCode-Cook_Inlet 'VRDR.ValueSets.RaceCode.Cook_Inlet')
  - [Coos](#F-VRDR-ValueSets-RaceCode-Coos 'VRDR.ValueSets.RaceCode.Coos')
  - [Coos_Lower_Umpqua_And_Siuslaw](#F-VRDR-ValueSets-RaceCode-Coos_Lower_Umpqua_And_Siuslaw 'VRDR.ValueSets.RaceCode.Coos_Lower_Umpqua_And_Siuslaw')
  - [Copper_Center](#F-VRDR-ValueSets-RaceCode-Copper_Center 'VRDR.ValueSets.RaceCode.Copper_Center')
  - [Copper_River](#F-VRDR-ValueSets-RaceCode-Copper_River 'VRDR.ValueSets.RaceCode.Copper_River')
  - [Coquille](#F-VRDR-ValueSets-RaceCode-Coquille 'VRDR.ValueSets.RaceCode.Coquille')
  - [Cortina_Indian_Rancheria_Of_Wintun_Indians](#F-VRDR-ValueSets-RaceCode-Cortina_Indian_Rancheria_Of_Wintun_Indians 'VRDR.ValueSets.RaceCode.Cortina_Indian_Rancheria_Of_Wintun_Indians')
  - [Cosmopolitan](#F-VRDR-ValueSets-RaceCode-Cosmopolitan 'VRDR.ValueSets.RaceCode.Cosmopolitan')
  - [Costa_Rican](#F-VRDR-ValueSets-RaceCode-Costa_Rican 'VRDR.ValueSets.RaceCode.Costa_Rican')
  - [Costanoan](#F-VRDR-ValueSets-RaceCode-Costanoan 'VRDR.ValueSets.RaceCode.Costanoan')
  - [Coushatta](#F-VRDR-ValueSets-RaceCode-Coushatta 'VRDR.ValueSets.RaceCode.Coushatta')
  - [Cow_Creek_Umpqua](#F-VRDR-ValueSets-RaceCode-Cow_Creek_Umpqua 'VRDR.ValueSets.RaceCode.Cow_Creek_Umpqua')
  - [Cowlitz](#F-VRDR-ValueSets-RaceCode-Cowlitz 'VRDR.ValueSets.RaceCode.Cowlitz')
  - [Coyote_Valley_Band](#F-VRDR-ValueSets-RaceCode-Coyote_Valley_Band 'VRDR.ValueSets.RaceCode.Coyote_Valley_Band')
  - [Craig_Community_Association](#F-VRDR-ValueSets-RaceCode-Craig_Community_Association 'VRDR.ValueSets.RaceCode.Craig_Community_Association')
  - [Cree](#F-VRDR-ValueSets-RaceCode-Cree 'VRDR.ValueSets.RaceCode.Cree')
  - [Creole](#F-VRDR-ValueSets-RaceCode-Creole 'VRDR.ValueSets.RaceCode.Creole')
  - [Croatan](#F-VRDR-ValueSets-RaceCode-Croatan 'VRDR.ValueSets.RaceCode.Croatan')
  - [Croatian](#F-VRDR-ValueSets-RaceCode-Croatian 'VRDR.ValueSets.RaceCode.Croatian')
  - [Crow](#F-VRDR-ValueSets-RaceCode-Crow 'VRDR.ValueSets.RaceCode.Crow')
  - [Crow_Creek_Sioux](#F-VRDR-ValueSets-RaceCode-Crow_Creek_Sioux 'VRDR.ValueSets.RaceCode.Crow_Creek_Sioux')
  - [Cuban](#F-VRDR-ValueSets-RaceCode-Cuban 'VRDR.ValueSets.RaceCode.Cuban')
  - [Cumberland_County_Association_For_Indian_People](#F-VRDR-ValueSets-RaceCode-Cumberland_County_Association_For_Indian_People 'VRDR.ValueSets.RaceCode.Cumberland_County_Association_For_Indian_People')
  - [Cupeno](#F-VRDR-ValueSets-RaceCode-Cupeno 'VRDR.ValueSets.RaceCode.Cupeno')
  - [Curyung_Tribal_Council](#F-VRDR-ValueSets-RaceCode-Curyung_Tribal_Council 'VRDR.ValueSets.RaceCode.Curyung_Tribal_Council')
  - [Cuyapaipe](#F-VRDR-ValueSets-RaceCode-Cuyapaipe 'VRDR.ValueSets.RaceCode.Cuyapaipe')
  - [Czech](#F-VRDR-ValueSets-RaceCode-Czech 'VRDR.ValueSets.RaceCode.Czech')
  - [Czechoslovakian](#F-VRDR-ValueSets-RaceCode-Czechoslovakian 'VRDR.ValueSets.RaceCode.Czechoslovakian')
  - [Dakota_Sioux](#F-VRDR-ValueSets-RaceCode-Dakota_Sioux 'VRDR.ValueSets.RaceCode.Dakota_Sioux')
  - [Dania_Seminole](#F-VRDR-ValueSets-RaceCode-Dania_Seminole 'VRDR.ValueSets.RaceCode.Dania_Seminole')
  - [Death_Valley_Timbisha_Shoshone](#F-VRDR-ValueSets-RaceCode-Death_Valley_Timbisha_Shoshone 'VRDR.ValueSets.RaceCode.Death_Valley_Timbisha_Shoshone')
  - [Deferred](#F-VRDR-ValueSets-RaceCode-Deferred 'VRDR.ValueSets.RaceCode.Deferred')
  - [Delaware](#F-VRDR-ValueSets-RaceCode-Delaware 'VRDR.ValueSets.RaceCode.Delaware')
  - [Delaware_Tribe_Of_Indians_Oklahoma](#F-VRDR-ValueSets-RaceCode-Delaware_Tribe_Of_Indians_Oklahoma 'VRDR.ValueSets.RaceCode.Delaware_Tribe_Of_Indians_Oklahoma')
  - [Delaware_Tribe_Of_Western_Oklahoma](#F-VRDR-ValueSets-RaceCode-Delaware_Tribe_Of_Western_Oklahoma 'VRDR.ValueSets.RaceCode.Delaware_Tribe_Of_Western_Oklahoma')
  - [Diegueno](#F-VRDR-ValueSets-RaceCode-Diegueno 'VRDR.ValueSets.RaceCode.Diegueno')
  - [Digger](#F-VRDR-ValueSets-RaceCode-Digger 'VRDR.ValueSets.RaceCode.Digger')
  - [Dominica_Islander](#F-VRDR-ValueSets-RaceCode-Dominica_Islander 'VRDR.ValueSets.RaceCode.Dominica_Islander')
  - [Dominican_Republic](#F-VRDR-ValueSets-RaceCode-Dominican_Republic 'VRDR.ValueSets.RaceCode.Dominican_Republic')
  - [Douglas_Indian_Association](#F-VRDR-ValueSets-RaceCode-Douglas_Indian_Association 'VRDR.ValueSets.RaceCode.Douglas_Indian_Association')
  - [Doyon](#F-VRDR-ValueSets-RaceCode-Doyon 'VRDR.ValueSets.RaceCode.Doyon')
  - [Dresslerville_Colony](#F-VRDR-ValueSets-RaceCode-Dresslerville_Colony 'VRDR.ValueSets.RaceCode.Dresslerville_Colony')
  - [Dry_Creek](#F-VRDR-ValueSets-RaceCode-Dry_Creek 'VRDR.ValueSets.RaceCode.Dry_Creek')
  - [Duck_Valley](#F-VRDR-ValueSets-RaceCode-Duck_Valley 'VRDR.ValueSets.RaceCode.Duck_Valley')
  - [Duckwater](#F-VRDR-ValueSets-RaceCode-Duckwater 'VRDR.ValueSets.RaceCode.Duckwater')
  - [Duwamish](#F-VRDR-ValueSets-RaceCode-Duwamish 'VRDR.ValueSets.RaceCode.Duwamish')
  - [Eastern_Creek](#F-VRDR-ValueSets-RaceCode-Eastern_Creek 'VRDR.ValueSets.RaceCode.Eastern_Creek')
  - [Eastern_Muscogee](#F-VRDR-ValueSets-RaceCode-Eastern_Muscogee 'VRDR.ValueSets.RaceCode.Eastern_Muscogee')
  - [Eastern_Pomo](#F-VRDR-ValueSets-RaceCode-Eastern_Pomo 'VRDR.ValueSets.RaceCode.Eastern_Pomo')
  - [Eastern_Shawnee](#F-VRDR-ValueSets-RaceCode-Eastern_Shawnee 'VRDR.ValueSets.RaceCode.Eastern_Shawnee')
  - [Echota_Cherokee](#F-VRDR-ValueSets-RaceCode-Echota_Cherokee 'VRDR.ValueSets.RaceCode.Echota_Cherokee')
  - [Ecuadorian](#F-VRDR-ValueSets-RaceCode-Ecuadorian 'VRDR.ValueSets.RaceCode.Ecuadorian')
  - [Egegik_Village](#F-VRDR-ValueSets-RaceCode-Egegik_Village 'VRDR.ValueSets.RaceCode.Egegik_Village')
  - [Egyptian](#F-VRDR-ValueSets-RaceCode-Egyptian 'VRDR.ValueSets.RaceCode.Egyptian')
  - [Ekiutna_Native_Village](#F-VRDR-ValueSets-RaceCode-Ekiutna_Native_Village 'VRDR.ValueSets.RaceCode.Ekiutna_Native_Village')
  - [Ekwok_Village](#F-VRDR-ValueSets-RaceCode-Ekwok_Village 'VRDR.ValueSets.RaceCode.Ekwok_Village')
  - [Elem_Indian_Colony_Of_The_Sulphur_Bank](#F-VRDR-ValueSets-RaceCode-Elem_Indian_Colony_Of_The_Sulphur_Bank 'VRDR.ValueSets.RaceCode.Elem_Indian_Colony_Of_The_Sulphur_Bank')
  - [Elk_Valley_Rancheria](#F-VRDR-ValueSets-RaceCode-Elk_Valley_Rancheria 'VRDR.ValueSets.RaceCode.Elk_Valley_Rancheria')
  - [Elko](#F-VRDR-ValueSets-RaceCode-Elko 'VRDR.ValueSets.RaceCode.Elko')
  - [Ely](#F-VRDR-ValueSets-RaceCode-Ely 'VRDR.ValueSets.RaceCode.Ely')
  - [Emmonak_Village](#F-VRDR-ValueSets-RaceCode-Emmonak_Village 'VRDR.ValueSets.RaceCode.Emmonak_Village')
  - [English](#F-VRDR-ValueSets-RaceCode-English 'VRDR.ValueSets.RaceCode.English')
  - [Enterprise_Rancheria](#F-VRDR-ValueSets-RaceCode-Enterprise_Rancheria 'VRDR.ValueSets.RaceCode.Enterprise_Rancheria')
  - [Eritrean](#F-VRDR-ValueSets-RaceCode-Eritrean 'VRDR.ValueSets.RaceCode.Eritrean')
  - [Eskimo](#F-VRDR-ValueSets-RaceCode-Eskimo 'VRDR.ValueSets.RaceCode.Eskimo')
  - [Esselen](#F-VRDR-ValueSets-RaceCode-Esselen 'VRDR.ValueSets.RaceCode.Esselen')
  - [Ethiopian](#F-VRDR-ValueSets-RaceCode-Ethiopian 'VRDR.ValueSets.RaceCode.Ethiopian')
  - [Etowah_Cherokee](#F-VRDR-ValueSets-RaceCode-Etowah_Cherokee 'VRDR.ValueSets.RaceCode.Etowah_Cherokee')
  - [Eurasian](#F-VRDR-ValueSets-RaceCode-Eurasian 'VRDR.ValueSets.RaceCode.Eurasian')
  - [European](#F-VRDR-ValueSets-RaceCode-European 'VRDR.ValueSets.RaceCode.European')
  - [Evansville_Village](#F-VRDR-ValueSets-RaceCode-Evansville_Village 'VRDR.ValueSets.RaceCode.Evansville_Village')
  - [Eyak](#F-VRDR-ValueSets-RaceCode-Eyak 'VRDR.ValueSets.RaceCode.Eyak')
  - [Fallen](#F-VRDR-ValueSets-RaceCode-Fallen 'VRDR.ValueSets.RaceCode.Fallen')
  - [Fijian](#F-VRDR-ValueSets-RaceCode-Fijian 'VRDR.ValueSets.RaceCode.Fijian')
  - [Filipino](#F-VRDR-ValueSets-RaceCode-Filipino 'VRDR.ValueSets.RaceCode.Filipino')
  - [First_Pass_Reject](#F-VRDR-ValueSets-RaceCode-First_Pass_Reject 'VRDR.ValueSets.RaceCode.First_Pass_Reject')
  - [Flandreau_Santee_Sioux](#F-VRDR-ValueSets-RaceCode-Flandreau_Santee_Sioux 'VRDR.ValueSets.RaceCode.Flandreau_Santee_Sioux')
  - [Florida_Seminole](#F-VRDR-ValueSets-RaceCode-Florida_Seminole 'VRDR.ValueSets.RaceCode.Florida_Seminole')
  - [Fond_Du_Lac](#F-VRDR-ValueSets-RaceCode-Fond_Du_Lac 'VRDR.ValueSets.RaceCode.Fond_Du_Lac')
  - [Forest_County](#F-VRDR-ValueSets-RaceCode-Forest_County 'VRDR.ValueSets.RaceCode.Forest_County')
  - [Fort_Belknap](#F-VRDR-ValueSets-RaceCode-Fort_Belknap 'VRDR.ValueSets.RaceCode.Fort_Belknap')
  - [Fort_Belknap_Assiniboine](#F-VRDR-ValueSets-RaceCode-Fort_Belknap_Assiniboine 'VRDR.ValueSets.RaceCode.Fort_Belknap_Assiniboine')
  - [Fort_Belknap_Gros_Ventres](#F-VRDR-ValueSets-RaceCode-Fort_Belknap_Gros_Ventres 'VRDR.ValueSets.RaceCode.Fort_Belknap_Gros_Ventres')
  - [Fort_Berthold](#F-VRDR-ValueSets-RaceCode-Fort_Berthold 'VRDR.ValueSets.RaceCode.Fort_Berthold')
  - [Fort_Bidwell](#F-VRDR-ValueSets-RaceCode-Fort_Bidwell 'VRDR.ValueSets.RaceCode.Fort_Bidwell')
  - [Fort_Independence](#F-VRDR-ValueSets-RaceCode-Fort_Independence 'VRDR.ValueSets.RaceCode.Fort_Independence')
  - [Fort_Mcdermitt_Paiute_And_Shoshone_Tribes](#F-VRDR-ValueSets-RaceCode-Fort_Mcdermitt_Paiute_And_Shoshone_Tribes 'VRDR.ValueSets.RaceCode.Fort_Mcdermitt_Paiute_And_Shoshone_Tribes')
  - [Fort_Mcdowell_Mohaveapache_Community](#F-VRDR-ValueSets-RaceCode-Fort_Mcdowell_Mohaveapache_Community 'VRDR.ValueSets.RaceCode.Fort_Mcdowell_Mohaveapache_Community')
  - [Fort_Mojave_Indian_Tribe_Of_Arizona](#F-VRDR-ValueSets-RaceCode-Fort_Mojave_Indian_Tribe_Of_Arizona 'VRDR.ValueSets.RaceCode.Fort_Mojave_Indian_Tribe_Of_Arizona')
  - [Fort_Peck_Assiniboine](#F-VRDR-ValueSets-RaceCode-Fort_Peck_Assiniboine 'VRDR.ValueSets.RaceCode.Fort_Peck_Assiniboine')
  - [Fort_Peck_Assiniboine_And_Sioux](#F-VRDR-ValueSets-RaceCode-Fort_Peck_Assiniboine_And_Sioux 'VRDR.ValueSets.RaceCode.Fort_Peck_Assiniboine_And_Sioux')
  - [Fort_Peck_Sioux](#F-VRDR-ValueSets-RaceCode-Fort_Peck_Sioux 'VRDR.ValueSets.RaceCode.Fort_Peck_Sioux')
  - [Fort_Sill_Apache](#F-VRDR-ValueSets-RaceCode-Fort_Sill_Apache 'VRDR.ValueSets.RaceCode.Fort_Sill_Apache')
  - [Four_Winds_Cherokee](#F-VRDR-ValueSets-RaceCode-Four_Winds_Cherokee 'VRDR.ValueSets.RaceCode.Four_Winds_Cherokee')
  - [French](#F-VRDR-ValueSets-RaceCode-French 'VRDR.ValueSets.RaceCode.French')
  - [French_American_Indian](#F-VRDR-ValueSets-RaceCode-French_American_Indian 'VRDR.ValueSets.RaceCode.French_American_Indian')
  - [French_Canadian](#F-VRDR-ValueSets-RaceCode-French_Canadian 'VRDR.ValueSets.RaceCode.French_Canadian')
  - [Gabrieleno](#F-VRDR-ValueSets-RaceCode-Gabrieleno 'VRDR.ValueSets.RaceCode.Gabrieleno')
  - [Galena_Village](#F-VRDR-ValueSets-RaceCode-Galena_Village 'VRDR.ValueSets.RaceCode.Galena_Village')
  - [Gay_Head_Wampanoag](#F-VRDR-ValueSets-RaceCode-Gay_Head_Wampanoag 'VRDR.ValueSets.RaceCode.Gay_Head_Wampanoag')
  - [Georgetown](#F-VRDR-ValueSets-RaceCode-Georgetown 'VRDR.ValueSets.RaceCode.Georgetown')
  - [Georgia_Eastern_Cherokee](#F-VRDR-ValueSets-RaceCode-Georgia_Eastern_Cherokee 'VRDR.ValueSets.RaceCode.Georgia_Eastern_Cherokee')
  - [German](#F-VRDR-ValueSets-RaceCode-German 'VRDR.ValueSets.RaceCode.German')
  - [Gila_Bend](#F-VRDR-ValueSets-RaceCode-Gila_Bend 'VRDR.ValueSets.RaceCode.Gila_Bend')
  - [Gila_River_Indian_Community](#F-VRDR-ValueSets-RaceCode-Gila_River_Indian_Community 'VRDR.ValueSets.RaceCode.Gila_River_Indian_Community')
  - [Golden_Hill_Paugussett](#F-VRDR-ValueSets-RaceCode-Golden_Hill_Paugussett 'VRDR.ValueSets.RaceCode.Golden_Hill_Paugussett')
  - [Golovin](#F-VRDR-ValueSets-RaceCode-Golovin 'VRDR.ValueSets.RaceCode.Golovin')
  - [Goshute](#F-VRDR-ValueSets-RaceCode-Goshute 'VRDR.ValueSets.RaceCode.Goshute')
  - [Grand_Portage](#F-VRDR-ValueSets-RaceCode-Grand_Portage 'VRDR.ValueSets.RaceCode.Grand_Portage')
  - [Grand_River_Band_Of_Ottawa_Indians](#F-VRDR-ValueSets-RaceCode-Grand_River_Band_Of_Ottawa_Indians 'VRDR.ValueSets.RaceCode.Grand_River_Band_Of_Ottawa_Indians')
  - [Grand_Ronde](#F-VRDR-ValueSets-RaceCode-Grand_Ronde 'VRDR.ValueSets.RaceCode.Grand_Ronde')
  - [Grand_Traverse_Band_Of_Ottawa_And_Chippewa_Indians](#F-VRDR-ValueSets-RaceCode-Grand_Traverse_Band_Of_Ottawa_And_Chippewa_Indians 'VRDR.ValueSets.RaceCode.Grand_Traverse_Band_Of_Ottawa_And_Chippewa_Indians')
  - [Greenland_Eskimo](#F-VRDR-ValueSets-RaceCode-Greenland_Eskimo 'VRDR.ValueSets.RaceCode.Greenland_Eskimo')
  - [Greenville_Rancheria](#F-VRDR-ValueSets-RaceCode-Greenville_Rancheria 'VRDR.ValueSets.RaceCode.Greenville_Rancheria')
  - [Grindstone_Indian_Rancheria_Of_Wintunwailaki_Indians](#F-VRDR-ValueSets-RaceCode-Grindstone_Indian_Rancheria_Of_Wintunwailaki_Indians 'VRDR.ValueSets.RaceCode.Grindstone_Indian_Rancheria_Of_Wintunwailaki_Indians')
  - [Gros_Ventres](#F-VRDR-ValueSets-RaceCode-Gros_Ventres 'VRDR.ValueSets.RaceCode.Gros_Ventres')
  - [Guamanian](#F-VRDR-ValueSets-RaceCode-Guamanian 'VRDR.ValueSets.RaceCode.Guamanian')
  - [Guatemalan](#F-VRDR-ValueSets-RaceCode-Guatemalan 'VRDR.ValueSets.RaceCode.Guatemalan')
  - [Guidiville_Rancheria_Of_California](#F-VRDR-ValueSets-RaceCode-Guidiville_Rancheria_Of_California 'VRDR.ValueSets.RaceCode.Guidiville_Rancheria_Of_California')
  - [Guilford_Native_American_Association](#F-VRDR-ValueSets-RaceCode-Guilford_Native_American_Association 'VRDR.ValueSets.RaceCode.Guilford_Native_American_Association')
  - [Gulkana_Village](#F-VRDR-ValueSets-RaceCode-Gulkana_Village 'VRDR.ValueSets.RaceCode.Gulkana_Village')
  - [Guyanese](#F-VRDR-ValueSets-RaceCode-Guyanese 'VRDR.ValueSets.RaceCode.Guyanese')
  - [Haida](#F-VRDR-ValueSets-RaceCode-Haida 'VRDR.ValueSets.RaceCode.Haida')
  - [Haitian](#F-VRDR-ValueSets-RaceCode-Haitian 'VRDR.ValueSets.RaceCode.Haitian')
  - [Half_Breed](#F-VRDR-ValueSets-RaceCode-Half_Breed 'VRDR.ValueSets.RaceCode.Half_Breed')
  - [Haliwasaponi](#F-VRDR-ValueSets-RaceCode-Haliwasaponi 'VRDR.ValueSets.RaceCode.Haliwasaponi')
  - [Hannahville_Indian_Community_Of_Wisconsin_Potawatomi](#F-VRDR-ValueSets-RaceCode-Hannahville_Indian_Community_Of_Wisconsin_Potawatomi 'VRDR.ValueSets.RaceCode.Hannahville_Indian_Community_Of_Wisconsin_Potawatomi')
  - [Hassanamisco_Band_Of_The_Nipmuc_Nation](#F-VRDR-ValueSets-RaceCode-Hassanamisco_Band_Of_The_Nipmuc_Nation 'VRDR.ValueSets.RaceCode.Hassanamisco_Band_Of_The_Nipmuc_Nation')
  - [Havasupai](#F-VRDR-ValueSets-RaceCode-Havasupai 'VRDR.ValueSets.RaceCode.Havasupai')
  - [Hawaiian](#F-VRDR-ValueSets-RaceCode-Hawaiian 'VRDR.ValueSets.RaceCode.Hawaiian')
  - [Healy_Lake_Village](#F-VRDR-ValueSets-RaceCode-Healy_Lake_Village 'VRDR.ValueSets.RaceCode.Healy_Lake_Village')
  - [Hidatsa](#F-VRDR-ValueSets-RaceCode-Hidatsa 'VRDR.ValueSets.RaceCode.Hidatsa')
  - [Hispanic](#F-VRDR-ValueSets-RaceCode-Hispanic 'VRDR.ValueSets.RaceCode.Hispanic')
  - [Hmong](#F-VRDR-ValueSets-RaceCode-Hmong 'VRDR.ValueSets.RaceCode.Hmong')
  - [Hochunk_Nation_Of_Wisconsin](#F-VRDR-ValueSets-RaceCode-Hochunk_Nation_Of_Wisconsin 'VRDR.ValueSets.RaceCode.Hochunk_Nation_Of_Wisconsin')
  - [Hoh_Indian_Tribe](#F-VRDR-ValueSets-RaceCode-Hoh_Indian_Tribe 'VRDR.ValueSets.RaceCode.Hoh_Indian_Tribe')
  - [Hollywood_Seminole](#F-VRDR-ValueSets-RaceCode-Hollywood_Seminole 'VRDR.ValueSets.RaceCode.Hollywood_Seminole')
  - [Holy_Cross_Village](#F-VRDR-ValueSets-RaceCode-Holy_Cross_Village 'VRDR.ValueSets.RaceCode.Holy_Cross_Village')
  - [Honduran](#F-VRDR-ValueSets-RaceCode-Honduran 'VRDR.ValueSets.RaceCode.Honduran')
  - [Hoonah_Indian_Association](#F-VRDR-ValueSets-RaceCode-Hoonah_Indian_Association 'VRDR.ValueSets.RaceCode.Hoonah_Indian_Association')
  - [Hoopa_Extension](#F-VRDR-ValueSets-RaceCode-Hoopa_Extension 'VRDR.ValueSets.RaceCode.Hoopa_Extension')
  - [Hoopa_Valley_Tribe](#F-VRDR-ValueSets-RaceCode-Hoopa_Valley_Tribe 'VRDR.ValueSets.RaceCode.Hoopa_Valley_Tribe')
  - [Hopi](#F-VRDR-ValueSets-RaceCode-Hopi 'VRDR.ValueSets.RaceCode.Hopi')
  - [Hopland_Band_Of_Pomo_Indians](#F-VRDR-ValueSets-RaceCode-Hopland_Band_Of_Pomo_Indians 'VRDR.ValueSets.RaceCode.Hopland_Band_Of_Pomo_Indians')
  - [Houlton_Band_Of_Maliseet_Indians](#F-VRDR-ValueSets-RaceCode-Houlton_Band_Of_Maliseet_Indians 'VRDR.ValueSets.RaceCode.Houlton_Band_Of_Maliseet_Indians')
  - [Hualapai](#F-VRDR-ValueSets-RaceCode-Hualapai 'VRDR.ValueSets.RaceCode.Hualapai')
  - [Hughes_Village](#F-VRDR-ValueSets-RaceCode-Hughes_Village 'VRDR.ValueSets.RaceCode.Hughes_Village')
  - [Huron_Potawatomi](#F-VRDR-ValueSets-RaceCode-Huron_Potawatomi 'VRDR.ValueSets.RaceCode.Huron_Potawatomi')
  - [Huslia_Village](#F-VRDR-ValueSets-RaceCode-Huslia_Village 'VRDR.ValueSets.RaceCode.Huslia_Village')
  - [Hydaburg_Cooperative_Association](#F-VRDR-ValueSets-RaceCode-Hydaburg_Cooperative_Association 'VRDR.ValueSets.RaceCode.Hydaburg_Cooperative_Association')
  - [Iberian](#F-VRDR-ValueSets-RaceCode-Iberian 'VRDR.ValueSets.RaceCode.Iberian')
  - [Igiugig_Village](#F-VRDR-ValueSets-RaceCode-Igiugig_Village 'VRDR.ValueSets.RaceCode.Igiugig_Village')
  - [Illinois_Miami](#F-VRDR-ValueSets-RaceCode-Illinois_Miami 'VRDR.ValueSets.RaceCode.Illinois_Miami')
  - [Inaja_Band_Of_Diegueno_Mission_Indians_Of_The_Inaja_And_Cosmit_Reservation](#F-VRDR-ValueSets-RaceCode-Inaja_Band_Of_Diegueno_Mission_Indians_Of_The_Inaja_And_Cosmit_Reservation 'VRDR.ValueSets.RaceCode.Inaja_Band_Of_Diegueno_Mission_Indians_Of_The_Inaja_And_Cosmit_Reservation')
  - [Indian](#F-VRDR-ValueSets-RaceCode-Indian 'VRDR.ValueSets.RaceCode.Indian')
  - [Indian_Township](#F-VRDR-ValueSets-RaceCode-Indian_Township 'VRDR.ValueSets.RaceCode.Indian_Township')
  - [Indiana_Miami](#F-VRDR-ValueSets-RaceCode-Indiana_Miami 'VRDR.ValueSets.RaceCode.Indiana_Miami')
  - [Indians_Of_Person_County](#F-VRDR-ValueSets-RaceCode-Indians_Of_Person_County 'VRDR.ValueSets.RaceCode.Indians_Of_Person_County')
  - [Indo_Chinese](#F-VRDR-ValueSets-RaceCode-Indo_Chinese 'VRDR.ValueSets.RaceCode.Indo_Chinese')
  - [Indonesian](#F-VRDR-ValueSets-RaceCode-Indonesian 'VRDR.ValueSets.RaceCode.Indonesian')
  - [Interracial](#F-VRDR-ValueSets-RaceCode-Interracial 'VRDR.ValueSets.RaceCode.Interracial')
  - [Inuit](#F-VRDR-ValueSets-RaceCode-Inuit 'VRDR.ValueSets.RaceCode.Inuit')
  - [Inupiaq](#F-VRDR-ValueSets-RaceCode-Inupiaq 'VRDR.ValueSets.RaceCode.Inupiaq')
  - [Inupiat](#F-VRDR-ValueSets-RaceCode-Inupiat 'VRDR.ValueSets.RaceCode.Inupiat')
  - [Inupiat_Community_Of_The_Arctic_Slope](#F-VRDR-ValueSets-RaceCode-Inupiat_Community_Of_The_Arctic_Slope 'VRDR.ValueSets.RaceCode.Inupiat_Community_Of_The_Arctic_Slope')
  - [Inupiat_Eskimo](#F-VRDR-ValueSets-RaceCode-Inupiat_Eskimo 'VRDR.ValueSets.RaceCode.Inupiat_Eskimo')
  - [Ione_Band_Of_Miwok_Indians](#F-VRDR-ValueSets-RaceCode-Ione_Band_Of_Miwok_Indians 'VRDR.ValueSets.RaceCode.Ione_Band_Of_Miwok_Indians')
  - [Iowa](#F-VRDR-ValueSets-RaceCode-Iowa 'VRDR.ValueSets.RaceCode.Iowa')
  - [Iowa_Of_Kansas_And_Nebraska](#F-VRDR-ValueSets-RaceCode-Iowa_Of_Kansas_And_Nebraska 'VRDR.ValueSets.RaceCode.Iowa_Of_Kansas_And_Nebraska')
  - [Iowa_Of_Oklahoma](#F-VRDR-ValueSets-RaceCode-Iowa_Of_Oklahoma 'VRDR.ValueSets.RaceCode.Iowa_Of_Oklahoma')
  - [Iqurmuit_Traditional_Council](#F-VRDR-ValueSets-RaceCode-Iqurmuit_Traditional_Council 'VRDR.ValueSets.RaceCode.Iqurmuit_Traditional_Council')
  - [Iranian](#F-VRDR-ValueSets-RaceCode-Iranian 'VRDR.ValueSets.RaceCode.Iranian')
  - [Iraqi](#F-VRDR-ValueSets-RaceCode-Iraqi 'VRDR.ValueSets.RaceCode.Iraqi')
  - [Irish](#F-VRDR-ValueSets-RaceCode-Irish 'VRDR.ValueSets.RaceCode.Irish')
  - [Iroquois](#F-VRDR-ValueSets-RaceCode-Iroquois 'VRDR.ValueSets.RaceCode.Iroquois')
  - [Isleta](#F-VRDR-ValueSets-RaceCode-Isleta 'VRDR.ValueSets.RaceCode.Isleta')
  - [Israeli](#F-VRDR-ValueSets-RaceCode-Israeli 'VRDR.ValueSets.RaceCode.Israeli')
  - [Issues](#F-VRDR-ValueSets-RaceCode-Issues 'VRDR.ValueSets.RaceCode.Issues')
  - [Italian](#F-VRDR-ValueSets-RaceCode-Italian 'VRDR.ValueSets.RaceCode.Italian')
  - [Ivanoff_Bay_Village](#F-VRDR-ValueSets-RaceCode-Ivanoff_Bay_Village 'VRDR.ValueSets.RaceCode.Ivanoff_Bay_Village')
  - [Iwo_Jiman](#F-VRDR-ValueSets-RaceCode-Iwo_Jiman 'VRDR.ValueSets.RaceCode.Iwo_Jiman')
  - [Jackson_Rancheria_Of_Mewuk_Indians_Of_California](#F-VRDR-ValueSets-RaceCode-Jackson_Rancheria_Of_Mewuk_Indians_Of_California 'VRDR.ValueSets.RaceCode.Jackson_Rancheria_Of_Mewuk_Indians_Of_California')
  - [Jackson_White](#F-VRDR-ValueSets-RaceCode-Jackson_White 'VRDR.ValueSets.RaceCode.Jackson_White')
  - [Jamaican](#F-VRDR-ValueSets-RaceCode-Jamaican 'VRDR.ValueSets.RaceCode.Jamaican')
  - [Jamestown_Sklallam](#F-VRDR-ValueSets-RaceCode-Jamestown_Sklallam 'VRDR.ValueSets.RaceCode.Jamestown_Sklallam')
  - [Japanese](#F-VRDR-ValueSets-RaceCode-Japanese 'VRDR.ValueSets.RaceCode.Japanese')
  - [Jarnul_Indian_Village](#F-VRDR-ValueSets-RaceCode-Jarnul_Indian_Village 'VRDR.ValueSets.RaceCode.Jarnul_Indian_Village')
  - [Jemez](#F-VRDR-ValueSets-RaceCode-Jemez 'VRDR.ValueSets.RaceCode.Jemez')
  - [Jena_Band_Of_Choctaw](#F-VRDR-ValueSets-RaceCode-Jena_Band_Of_Choctaw 'VRDR.ValueSets.RaceCode.Jena_Band_Of_Choctaw')
  - [Jewish](#F-VRDR-ValueSets-RaceCode-Jewish 'VRDR.ValueSets.RaceCode.Jewish')
  - [Jicarilla_Apache](#F-VRDR-ValueSets-RaceCode-Jicarilla_Apache 'VRDR.ValueSets.RaceCode.Jicarilla_Apache')
  - [Juaneno](#F-VRDR-ValueSets-RaceCode-Juaneno 'VRDR.ValueSets.RaceCode.Juaneno')
  - [Juneau](#F-VRDR-ValueSets-RaceCode-Juneau 'VRDR.ValueSets.RaceCode.Juneau')
  - [Kaguyak_Village](#F-VRDR-ValueSets-RaceCode-Kaguyak_Village 'VRDR.ValueSets.RaceCode.Kaguyak_Village')
  - [Kaibab_Band_Of_Paiute_Indians](#F-VRDR-ValueSets-RaceCode-Kaibab_Band_Of_Paiute_Indians 'VRDR.ValueSets.RaceCode.Kaibab_Band_Of_Paiute_Indians')
  - [Kaktovik_Village](#F-VRDR-ValueSets-RaceCode-Kaktovik_Village 'VRDR.ValueSets.RaceCode.Kaktovik_Village')
  - [Kalapuya](#F-VRDR-ValueSets-RaceCode-Kalapuya 'VRDR.ValueSets.RaceCode.Kalapuya')
  - [Kalispel_Indian_Community](#F-VRDR-ValueSets-RaceCode-Kalispel_Indian_Community 'VRDR.ValueSets.RaceCode.Kalispel_Indian_Community')
  - [Karuk_Tribe_Of_California](#F-VRDR-ValueSets-RaceCode-Karuk_Tribe_Of_California 'VRDR.ValueSets.RaceCode.Karuk_Tribe_Of_California')
  - [Kashia_Band_Of_Pomo_Indians_Of_The_Stewarts_Point_Rancheria](#F-VRDR-ValueSets-RaceCode-Kashia_Band_Of_Pomo_Indians_Of_The_Stewarts_Point_Rancheria 'VRDR.ValueSets.RaceCode.Kashia_Band_Of_Pomo_Indians_Of_The_Stewarts_Point_Rancheria')
  - [Kathlamet](#F-VRDR-ValueSets-RaceCode-Kathlamet 'VRDR.ValueSets.RaceCode.Kathlamet')
  - [Kaw](#F-VRDR-ValueSets-RaceCode-Kaw 'VRDR.ValueSets.RaceCode.Kaw')
  - [Kawaiisu](#F-VRDR-ValueSets-RaceCode-Kawaiisu 'VRDR.ValueSets.RaceCode.Kawaiisu')
  - [Kawerak](#F-VRDR-ValueSets-RaceCode-Kawerak 'VRDR.ValueSets.RaceCode.Kawerak')
  - [Keechi](#F-VRDR-ValueSets-RaceCode-Keechi 'VRDR.ValueSets.RaceCode.Keechi')
  - [Kem_River_Paiute_Council](#F-VRDR-ValueSets-RaceCode-Kem_River_Paiute_Council 'VRDR.ValueSets.RaceCode.Kem_River_Paiute_Council')
  - [Kenaitze_Indian_Tribe](#F-VRDR-ValueSets-RaceCode-Kenaitze_Indian_Tribe 'VRDR.ValueSets.RaceCode.Kenaitze_Indian_Tribe')
  - [Keres](#F-VRDR-ValueSets-RaceCode-Keres 'VRDR.ValueSets.RaceCode.Keres')
  - [Ketchikan_Indian_Corporation](#F-VRDR-ValueSets-RaceCode-Ketchikan_Indian_Corporation 'VRDR.ValueSets.RaceCode.Ketchikan_Indian_Corporation')
  - [Keweenaw_Bay_Indian_Community_Of_The_Lanse_And_Ontonagon_Bands](#F-VRDR-ValueSets-RaceCode-Keweenaw_Bay_Indian_Community_Of_The_Lanse_And_Ontonagon_Bands 'VRDR.ValueSets.RaceCode.Keweenaw_Bay_Indian_Community_Of_The_Lanse_And_Ontonagon_Bands')
  - [Kialegee_Tribal_Town](#F-VRDR-ValueSets-RaceCode-Kialegee_Tribal_Town 'VRDR.ValueSets.RaceCode.Kialegee_Tribal_Town')
  - [Kickapoo](#F-VRDR-ValueSets-RaceCode-Kickapoo 'VRDR.ValueSets.RaceCode.Kickapoo')
  - [Kikiallus](#F-VRDR-ValueSets-RaceCode-Kikiallus 'VRDR.ValueSets.RaceCode.Kikiallus')
  - [King_Cove](#F-VRDR-ValueSets-RaceCode-King_Cove 'VRDR.ValueSets.RaceCode.King_Cove')
  - [King_Island_Native_Community](#F-VRDR-ValueSets-RaceCode-King_Island_Native_Community 'VRDR.ValueSets.RaceCode.King_Island_Native_Community')
  - [King_Salmon](#F-VRDR-ValueSets-RaceCode-King_Salmon 'VRDR.ValueSets.RaceCode.King_Salmon')
  - [Kiowa](#F-VRDR-ValueSets-RaceCode-Kiowa 'VRDR.ValueSets.RaceCode.Kiowa')
  - [Kirabati](#F-VRDR-ValueSets-RaceCode-Kirabati 'VRDR.ValueSets.RaceCode.Kirabati')
  - [Klallam](#F-VRDR-ValueSets-RaceCode-Klallam 'VRDR.ValueSets.RaceCode.Klallam')
  - [Klamath](#F-VRDR-ValueSets-RaceCode-Klamath 'VRDR.ValueSets.RaceCode.Klamath')
  - [Klawock_Cooperative_Association](#F-VRDR-ValueSets-RaceCode-Klawock_Cooperative_Association 'VRDR.ValueSets.RaceCode.Klawock_Cooperative_Association')
  - [Knik_Tribe](#F-VRDR-ValueSets-RaceCode-Knik_Tribe 'VRDR.ValueSets.RaceCode.Knik_Tribe')
  - [Kodiak](#F-VRDR-ValueSets-RaceCode-Kodiak 'VRDR.ValueSets.RaceCode.Kodiak')
  - [Kokhanok_Village](#F-VRDR-ValueSets-RaceCode-Kokhanok_Village 'VRDR.ValueSets.RaceCode.Kokhanok_Village')
  - [Koniag_Aleut](#F-VRDR-ValueSets-RaceCode-Koniag_Aleut 'VRDR.ValueSets.RaceCode.Koniag_Aleut')
  - [Konkow](#F-VRDR-ValueSets-RaceCode-Konkow 'VRDR.ValueSets.RaceCode.Konkow')
  - [Kootenai](#F-VRDR-ValueSets-RaceCode-Kootenai 'VRDR.ValueSets.RaceCode.Kootenai')
  - [Korean](#F-VRDR-ValueSets-RaceCode-Korean 'VRDR.ValueSets.RaceCode.Korean')
  - [Kosovian](#F-VRDR-ValueSets-RaceCode-Kosovian 'VRDR.ValueSets.RaceCode.Kosovian')
  - [Kosraean](#F-VRDR-ValueSets-RaceCode-Kosraean 'VRDR.ValueSets.RaceCode.Kosraean')
  - [Koyukuk_Native_Village](#F-VRDR-ValueSets-RaceCode-Koyukuk_Native_Village 'VRDR.ValueSets.RaceCode.Koyukuk_Native_Village')
  - [Kutenai_Indian](#F-VRDR-ValueSets-RaceCode-Kutenai_Indian 'VRDR.ValueSets.RaceCode.Kutenai_Indian')
  - [Kwiguk](#F-VRDR-ValueSets-RaceCode-Kwiguk 'VRDR.ValueSets.RaceCode.Kwiguk')
  - [La_Jolla_Band_Of_Luiseno_Mission_Indians](#F-VRDR-ValueSets-RaceCode-La_Jolla_Band_Of_Luiseno_Mission_Indians 'VRDR.ValueSets.RaceCode.La_Jolla_Band_Of_Luiseno_Mission_Indians')
  - [La_Posta_Band_Of_Diegueno_Mission_Indians](#F-VRDR-ValueSets-RaceCode-La_Posta_Band_Of_Diegueno_Mission_Indians 'VRDR.ValueSets.RaceCode.La_Posta_Band_Of_Diegueno_Mission_Indians')
  - [Lac_Court_Oreilles_Band_Of_Lake_Superior_Chippewa](#F-VRDR-ValueSets-RaceCode-Lac_Court_Oreilles_Band_Of_Lake_Superior_Chippewa 'VRDR.ValueSets.RaceCode.Lac_Court_Oreilles_Band_Of_Lake_Superior_Chippewa')
  - [Lac_Du_Flambeau](#F-VRDR-ValueSets-RaceCode-Lac_Du_Flambeau 'VRDR.ValueSets.RaceCode.Lac_Du_Flambeau')
  - [Lac_Vieux_Desert_Band_Of_Lake_Superior_Chippewa](#F-VRDR-ValueSets-RaceCode-Lac_Vieux_Desert_Band_Of_Lake_Superior_Chippewa 'VRDR.ValueSets.RaceCode.Lac_Vieux_Desert_Band_Of_Lake_Superior_Chippewa')
  - [Laguna](#F-VRDR-ValueSets-RaceCode-Laguna 'VRDR.ValueSets.RaceCode.Laguna')
  - [Lake_Minchumina](#F-VRDR-ValueSets-RaceCode-Lake_Minchumina 'VRDR.ValueSets.RaceCode.Lake_Minchumina')
  - [Lake_Superior](#F-VRDR-ValueSets-RaceCode-Lake_Superior 'VRDR.ValueSets.RaceCode.Lake_Superior')
  - [Lake_Traverse_Sioux](#F-VRDR-ValueSets-RaceCode-Lake_Traverse_Sioux 'VRDR.ValueSets.RaceCode.Lake_Traverse_Sioux')
  - [Laotian](#F-VRDR-ValueSets-RaceCode-Laotian 'VRDR.ValueSets.RaceCode.Laotian')
  - [Las_Vegas_Tribe_Of_The_Las_Vegas_Indian_Colony](#F-VRDR-ValueSets-RaceCode-Las_Vegas_Tribe_Of_The_Las_Vegas_Indian_Colony 'VRDR.ValueSets.RaceCode.Las_Vegas_Tribe_Of_The_Las_Vegas_Indian_Colony')
  - [Lassik](#F-VRDR-ValueSets-RaceCode-Lassik 'VRDR.ValueSets.RaceCode.Lassik')
  - [Latin_American](#F-VRDR-ValueSets-RaceCode-Latin_American 'VRDR.ValueSets.RaceCode.Latin_American')
  - [Lebanese](#F-VRDR-ValueSets-RaceCode-Lebanese 'VRDR.ValueSets.RaceCode.Lebanese')
  - [Leech_Lake](#F-VRDR-ValueSets-RaceCode-Leech_Lake 'VRDR.ValueSets.RaceCode.Leech_Lake')
  - [Lenni_Lanape](#F-VRDR-ValueSets-RaceCode-Lenni_Lanape 'VRDR.ValueSets.RaceCode.Lenni_Lanape')
  - [Lesnoi_Village](#F-VRDR-ValueSets-RaceCode-Lesnoi_Village 'VRDR.ValueSets.RaceCode.Lesnoi_Village')
  - [Levelock_Village](#F-VRDR-ValueSets-RaceCode-Levelock_Village 'VRDR.ValueSets.RaceCode.Levelock_Village')
  - [Liberian](#F-VRDR-ValueSets-RaceCode-Liberian 'VRDR.ValueSets.RaceCode.Liberian')
  - [Lime_Village](#F-VRDR-ValueSets-RaceCode-Lime_Village 'VRDR.ValueSets.RaceCode.Lime_Village')
  - [Lipan_Apache](#F-VRDR-ValueSets-RaceCode-Lipan_Apache 'VRDR.ValueSets.RaceCode.Lipan_Apache')
  - [Little_River_Band_Of_Ottawa_Indians_Of_Michigan](#F-VRDR-ValueSets-RaceCode-Little_River_Band_Of_Ottawa_Indians_Of_Michigan 'VRDR.ValueSets.RaceCode.Little_River_Band_Of_Ottawa_Indians_Of_Michigan')
  - [Little_Shell_Chippewa](#F-VRDR-ValueSets-RaceCode-Little_Shell_Chippewa 'VRDR.ValueSets.RaceCode.Little_Shell_Chippewa')
  - [Little_Traverse_Bay_Bands_Of_Ottawa_Indians_Of_Michigan](#F-VRDR-ValueSets-RaceCode-Little_Traverse_Bay_Bands_Of_Ottawa_Indians_Of_Michigan 'VRDR.ValueSets.RaceCode.Little_Traverse_Bay_Bands_Of_Ottawa_Indians_Of_Michigan')
  - [Lone_Pine](#F-VRDR-ValueSets-RaceCode-Lone_Pine 'VRDR.ValueSets.RaceCode.Lone_Pine')
  - [Long_Island](#F-VRDR-ValueSets-RaceCode-Long_Island 'VRDR.ValueSets.RaceCode.Long_Island')
  - [Los_Coyotes_Band_Of_Cahuilla_Mission_Indians](#F-VRDR-ValueSets-RaceCode-Los_Coyotes_Band_Of_Cahuilla_Mission_Indians 'VRDR.ValueSets.RaceCode.Los_Coyotes_Band_Of_Cahuilla_Mission_Indians')
  - [Lovelock_Paiute_Tribe_Of_The_Lovelock_Indian_Colony](#F-VRDR-ValueSets-RaceCode-Lovelock_Paiute_Tribe_Of_The_Lovelock_Indian_Colony 'VRDR.ValueSets.RaceCode.Lovelock_Paiute_Tribe_Of_The_Lovelock_Indian_Colony')
  - [Lower_Brule_Sioux](#F-VRDR-ValueSets-RaceCode-Lower_Brule_Sioux 'VRDR.ValueSets.RaceCode.Lower_Brule_Sioux')
  - [Lower_Elwha_Tribal_Community](#F-VRDR-ValueSets-RaceCode-Lower_Elwha_Tribal_Community 'VRDR.ValueSets.RaceCode.Lower_Elwha_Tribal_Community')
  - [Lower_Muscogee_Creek_Tama_Tribal_Town](#F-VRDR-ValueSets-RaceCode-Lower_Muscogee_Creek_Tama_Tribal_Town 'VRDR.ValueSets.RaceCode.Lower_Muscogee_Creek_Tama_Tribal_Town')
  - [Lower_Sioux_Indian_Community_Of_Minnesota_Mdewakanton_Sioux](#F-VRDR-ValueSets-RaceCode-Lower_Sioux_Indian_Community_Of_Minnesota_Mdewakanton_Sioux 'VRDR.ValueSets.RaceCode.Lower_Sioux_Indian_Community_Of_Minnesota_Mdewakanton_Sioux')
  - [Lower_Skagit](#F-VRDR-ValueSets-RaceCode-Lower_Skagit 'VRDR.ValueSets.RaceCode.Lower_Skagit')
  - [Luiseno](#F-VRDR-ValueSets-RaceCode-Luiseno 'VRDR.ValueSets.RaceCode.Luiseno')
  - [Lumbee](#F-VRDR-ValueSets-RaceCode-Lumbee 'VRDR.ValueSets.RaceCode.Lumbee')
  - [Lummi](#F-VRDR-ValueSets-RaceCode-Lummi 'VRDR.ValueSets.RaceCode.Lummi')
  - [Lytton_Rancheria_Of_California](#F-VRDR-ValueSets-RaceCode-Lytton_Rancheria_Of_California 'VRDR.ValueSets.RaceCode.Lytton_Rancheria_Of_California')
  - [Machis_Lower_Creek_Indian](#F-VRDR-ValueSets-RaceCode-Machis_Lower_Creek_Indian 'VRDR.ValueSets.RaceCode.Machis_Lower_Creek_Indian')
  - [Madagascar](#F-VRDR-ValueSets-RaceCode-Madagascar 'VRDR.ValueSets.RaceCode.Madagascar')
  - [Maidu](#F-VRDR-ValueSets-RaceCode-Maidu 'VRDR.ValueSets.RaceCode.Maidu')
  - [Makah](#F-VRDR-ValueSets-RaceCode-Makah 'VRDR.ValueSets.RaceCode.Makah')
  - [Malada](#F-VRDR-ValueSets-RaceCode-Malada 'VRDR.ValueSets.RaceCode.Malada')
  - [Malaysian](#F-VRDR-ValueSets-RaceCode-Malaysian 'VRDR.ValueSets.RaceCode.Malaysian')
  - [Maldivian](#F-VRDR-ValueSets-RaceCode-Maldivian 'VRDR.ValueSets.RaceCode.Maldivian')
  - [Malheur_Paiute](#F-VRDR-ValueSets-RaceCode-Malheur_Paiute 'VRDR.ValueSets.RaceCode.Malheur_Paiute')
  - [Maliseet](#F-VRDR-ValueSets-RaceCode-Maliseet 'VRDR.ValueSets.RaceCode.Maliseet')
  - [Manchester_Band_Of_Pomo_Indians_Of_The_Manchesterpoint_Arena_Racheria](#F-VRDR-ValueSets-RaceCode-Manchester_Band_Of_Pomo_Indians_Of_The_Manchesterpoint_Arena_Racheria 'VRDR.ValueSets.RaceCode.Manchester_Band_Of_Pomo_Indians_Of_The_Manchesterpoint_Arena_Racheria')
  - [Mandan](#F-VRDR-ValueSets-RaceCode-Mandan 'VRDR.ValueSets.RaceCode.Mandan')
  - [Manley_Hot_Springs_Village](#F-VRDR-ValueSets-RaceCode-Manley_Hot_Springs_Village 'VRDR.ValueSets.RaceCode.Manley_Hot_Springs_Village')
  - [Manokotak_Village](#F-VRDR-ValueSets-RaceCode-Manokotak_Village 'VRDR.ValueSets.RaceCode.Manokotak_Village')
  - [Manzanita](#F-VRDR-ValueSets-RaceCode-Manzanita 'VRDR.ValueSets.RaceCode.Manzanita')
  - [Mariana_Islander](#F-VRDR-ValueSets-RaceCode-Mariana_Islander 'VRDR.ValueSets.RaceCode.Mariana_Islander')
  - [Maricopa](#F-VRDR-ValueSets-RaceCode-Maricopa 'VRDR.ValueSets.RaceCode.Maricopa')
  - [Marietta_Band_Of_Nooksack](#F-VRDR-ValueSets-RaceCode-Marietta_Band_Of_Nooksack 'VRDR.ValueSets.RaceCode.Marietta_Band_Of_Nooksack')
  - [Marshallese](#F-VRDR-ValueSets-RaceCode-Marshallese 'VRDR.ValueSets.RaceCode.Marshallese')
  - [Mashantucket_Pequot](#F-VRDR-ValueSets-RaceCode-Mashantucket_Pequot 'VRDR.ValueSets.RaceCode.Mashantucket_Pequot')
  - [Mashpee_Wampanoag](#F-VRDR-ValueSets-RaceCode-Mashpee_Wampanoag 'VRDR.ValueSets.RaceCode.Mashpee_Wampanoag')
  - [Matinecock](#F-VRDR-ValueSets-RaceCode-Matinecock 'VRDR.ValueSets.RaceCode.Matinecock')
  - [Mattaponi_Indian_Tribe](#F-VRDR-ValueSets-RaceCode-Mattaponi_Indian_Tribe 'VRDR.ValueSets.RaceCode.Mattaponi_Indian_Tribe')
  - [Mattole](#F-VRDR-ValueSets-RaceCode-Mattole 'VRDR.ValueSets.RaceCode.Mattole')
  - [Mauneluk_Inupiat](#F-VRDR-ValueSets-RaceCode-Mauneluk_Inupiat 'VRDR.ValueSets.RaceCode.Mauneluk_Inupiat')
  - [Mcgrath_Native_Village](#F-VRDR-ValueSets-RaceCode-Mcgrath_Native_Village 'VRDR.ValueSets.RaceCode.Mcgrath_Native_Village')
  - [Mdewakanton_Sioux](#F-VRDR-ValueSets-RaceCode-Mdewakanton_Sioux 'VRDR.ValueSets.RaceCode.Mdewakanton_Sioux')
  - [Mechoopda_Indian_Tribe_Of_Chico_Rancheria_California](#F-VRDR-ValueSets-RaceCode-Mechoopda_Indian_Tribe_Of_Chico_Rancheria_California 'VRDR.ValueSets.RaceCode.Mechoopda_Indian_Tribe_Of_Chico_Rancheria_California')
  - [Mehemn_Indian_Tribe](#F-VRDR-ValueSets-RaceCode-Mehemn_Indian_Tribe 'VRDR.ValueSets.RaceCode.Mehemn_Indian_Tribe')
  - [Melanesian](#F-VRDR-ValueSets-RaceCode-Melanesian 'VRDR.ValueSets.RaceCode.Melanesian')
  - [Melungeon](#F-VRDR-ValueSets-RaceCode-Melungeon 'VRDR.ValueSets.RaceCode.Melungeon')
  - [Menominee](#F-VRDR-ValueSets-RaceCode-Menominee 'VRDR.ValueSets.RaceCode.Menominee')
  - [Mentasta_Traditional_Council](#F-VRDR-ValueSets-RaceCode-Mentasta_Traditional_Council 'VRDR.ValueSets.RaceCode.Mentasta_Traditional_Council')
  - [Mesa_Grande_Band_Of_Diegueno_Mission_Indians](#F-VRDR-ValueSets-RaceCode-Mesa_Grande_Band_Of_Diegueno_Mission_Indians 'VRDR.ValueSets.RaceCode.Mesa_Grande_Band_Of_Diegueno_Mission_Indians')
  - [Mescalero_Apache](#F-VRDR-ValueSets-RaceCode-Mescalero_Apache 'VRDR.ValueSets.RaceCode.Mescalero_Apache')
  - [Mestizo](#F-VRDR-ValueSets-RaceCode-Mestizo 'VRDR.ValueSets.RaceCode.Mestizo')
  - [Metlakatia_Indian_Community_Annette_Island_Reserve](#F-VRDR-ValueSets-RaceCode-Metlakatia_Indian_Community_Annette_Island_Reserve 'VRDR.ValueSets.RaceCode.Metlakatia_Indian_Community_Annette_Island_Reserve')
  - [Metrolina_Nadve_American_Association](#F-VRDR-ValueSets-RaceCode-Metrolina_Nadve_American_Association 'VRDR.ValueSets.RaceCode.Metrolina_Nadve_American_Association')
  - [Mewuk](#F-VRDR-ValueSets-RaceCode-Mewuk 'VRDR.ValueSets.RaceCode.Mewuk')
  - [Mexican](#F-VRDR-ValueSets-RaceCode-Mexican 'VRDR.ValueSets.RaceCode.Mexican')
  - [Mexican_American_Indian](#F-VRDR-ValueSets-RaceCode-Mexican_American_Indian 'VRDR.ValueSets.RaceCode.Mexican_American_Indian')
  - [Miami](#F-VRDR-ValueSets-RaceCode-Miami 'VRDR.ValueSets.RaceCode.Miami')
  - [Miccosukee](#F-VRDR-ValueSets-RaceCode-Miccosukee 'VRDR.ValueSets.RaceCode.Miccosukee')
  - [Micmac](#F-VRDR-ValueSets-RaceCode-Micmac 'VRDR.ValueSets.RaceCode.Micmac')
  - [Micronesian](#F-VRDR-ValueSets-RaceCode-Micronesian 'VRDR.ValueSets.RaceCode.Micronesian')
  - [Middle_East](#F-VRDR-ValueSets-RaceCode-Middle_East 'VRDR.ValueSets.RaceCode.Middle_East')
  - [Middletown_Rancheria_Of_Pomo_Indians](#F-VRDR-ValueSets-RaceCode-Middletown_Rancheria_Of_Pomo_Indians 'VRDR.ValueSets.RaceCode.Middletown_Rancheria_Of_Pomo_Indians')
  - [Mien](#F-VRDR-ValueSets-RaceCode-Mien 'VRDR.ValueSets.RaceCode.Mien')
  - [Mille_Lacs](#F-VRDR-ValueSets-RaceCode-Mille_Lacs 'VRDR.ValueSets.RaceCode.Mille_Lacs')
  - [Miniconjou](#F-VRDR-ValueSets-RaceCode-Miniconjou 'VRDR.ValueSets.RaceCode.Miniconjou')
  - [Minnesota_Chippewa](#F-VRDR-ValueSets-RaceCode-Minnesota_Chippewa 'VRDR.ValueSets.RaceCode.Minnesota_Chippewa')
  - [Mission_Band](#F-VRDR-ValueSets-RaceCode-Mission_Band 'VRDR.ValueSets.RaceCode.Mission_Band')
  - [Mission_Indians](#F-VRDR-ValueSets-RaceCode-Mission_Indians 'VRDR.ValueSets.RaceCode.Mission_Indians')
  - [Mississippi_Band_Of_Choctaw](#F-VRDR-ValueSets-RaceCode-Mississippi_Band_Of_Choctaw 'VRDR.ValueSets.RaceCode.Mississippi_Band_Of_Choctaw')
  - [Mixed](#F-VRDR-ValueSets-RaceCode-Mixed 'VRDR.ValueSets.RaceCode.Mixed')
  - [Moapa_Band_Of_Paiute](#F-VRDR-ValueSets-RaceCode-Moapa_Band_Of_Paiute 'VRDR.ValueSets.RaceCode.Moapa_Band_Of_Paiute')
  - [Modoc](#F-VRDR-ValueSets-RaceCode-Modoc 'VRDR.ValueSets.RaceCode.Modoc')
  - [Mohawk](#F-VRDR-ValueSets-RaceCode-Mohawk 'VRDR.ValueSets.RaceCode.Mohawk')
  - [Mohegan](#F-VRDR-ValueSets-RaceCode-Mohegan 'VRDR.ValueSets.RaceCode.Mohegan')
  - [Molalla](#F-VRDR-ValueSets-RaceCode-Molalla 'VRDR.ValueSets.RaceCode.Molalla')
  - [Monacan_Indian_Nation](#F-VRDR-ValueSets-RaceCode-Monacan_Indian_Nation 'VRDR.ValueSets.RaceCode.Monacan_Indian_Nation')
  - [Mongolian](#F-VRDR-ValueSets-RaceCode-Mongolian 'VRDR.ValueSets.RaceCode.Mongolian')
  - [Mono](#F-VRDR-ValueSets-RaceCode-Mono 'VRDR.ValueSets.RaceCode.Mono')
  - [Montauk](#F-VRDR-ValueSets-RaceCode-Montauk 'VRDR.ValueSets.RaceCode.Montauk')
  - [Moor](#F-VRDR-ValueSets-RaceCode-Moor 'VRDR.ValueSets.RaceCode.Moor')
  - [Mooretown_Rancheria_Of_Maidu_Indians](#F-VRDR-ValueSets-RaceCode-Mooretown_Rancheria_Of_Maidu_Indians 'VRDR.ValueSets.RaceCode.Mooretown_Rancheria_Of_Maidu_Indians')
  - [Morena](#F-VRDR-ValueSets-RaceCode-Morena 'VRDR.ValueSets.RaceCode.Morena')
  - [Moroccan](#F-VRDR-ValueSets-RaceCode-Moroccan 'VRDR.ValueSets.RaceCode.Moroccan')
  - [Morongo_Band_Of_Cahuilla_Mission_Indians](#F-VRDR-ValueSets-RaceCode-Morongo_Band_Of_Cahuilla_Mission_Indians 'VRDR.ValueSets.RaceCode.Morongo_Band_Of_Cahuilla_Mission_Indians')
  - [Mountain_Maidu](#F-VRDR-ValueSets-RaceCode-Mountain_Maidu 'VRDR.ValueSets.RaceCode.Mountain_Maidu')
  - [Mountain_Village](#F-VRDR-ValueSets-RaceCode-Mountain_Village 'VRDR.ValueSets.RaceCode.Mountain_Village')
  - [Mowa_Band_Of_Choctaw](#F-VRDR-ValueSets-RaceCode-Mowa_Band_Of_Choctaw 'VRDR.ValueSets.RaceCode.Mowa_Band_Of_Choctaw')
  - [Muckleshoot](#F-VRDR-ValueSets-RaceCode-Muckleshoot 'VRDR.ValueSets.RaceCode.Muckleshoot')
  - [Mulatto](#F-VRDR-ValueSets-RaceCode-Mulatto 'VRDR.ValueSets.RaceCode.Mulatto')
  - [Multiethnic](#F-VRDR-ValueSets-RaceCode-Multiethnic 'VRDR.ValueSets.RaceCode.Multiethnic')
  - [Multinational](#F-VRDR-ValueSets-RaceCode-Multinational 'VRDR.ValueSets.RaceCode.Multinational')
  - [Multiple_Asian_Responses](#F-VRDR-ValueSets-RaceCode-Multiple_Asian_Responses 'VRDR.ValueSets.RaceCode.Multiple_Asian_Responses')
  - [Multiple_Black_Or_African_American_Responses](#F-VRDR-ValueSets-RaceCode-Multiple_Black_Or_African_American_Responses 'VRDR.ValueSets.RaceCode.Multiple_Black_Or_African_American_Responses')
  - [Multiple_Native_Hawaiian_And_Other_Pacific_Islander_Responses](#F-VRDR-ValueSets-RaceCode-Multiple_Native_Hawaiian_And_Other_Pacific_Islander_Responses 'VRDR.ValueSets.RaceCode.Multiple_Native_Hawaiian_And_Other_Pacific_Islander_Responses')
  - [Multiple_Some_Other_Race_Responses_995_American](#F-VRDR-ValueSets-RaceCode-Multiple_Some_Other_Race_Responses_995_American 'VRDR.ValueSets.RaceCode.Multiple_Some_Other_Race_Responses_995_American')
  - [Multiple_White_Responses](#F-VRDR-ValueSets-RaceCode-Multiple_White_Responses 'VRDR.ValueSets.RaceCode.Multiple_White_Responses')
  - [Multiracial](#F-VRDR-ValueSets-RaceCode-Multiracial 'VRDR.ValueSets.RaceCode.Multiracial')
  - [Munsee](#F-VRDR-ValueSets-RaceCode-Munsee 'VRDR.ValueSets.RaceCode.Munsee')
  - [Muscogee_Creek_Nation](#F-VRDR-ValueSets-RaceCode-Muscogee_Creek_Nation 'VRDR.ValueSets.RaceCode.Muscogee_Creek_Nation')
  - [Naknek_Native_Village](#F-VRDR-ValueSets-RaceCode-Naknek_Native_Village 'VRDR.ValueSets.RaceCode.Naknek_Native_Village')
  - [Nambe](#F-VRDR-ValueSets-RaceCode-Nambe 'VRDR.ValueSets.RaceCode.Nambe')
  - [Namibian](#F-VRDR-ValueSets-RaceCode-Namibian 'VRDR.ValueSets.RaceCode.Namibian')
  - [Nana_Inupiat](#F-VRDR-ValueSets-RaceCode-Nana_Inupiat 'VRDR.ValueSets.RaceCode.Nana_Inupiat')
  - [Nansemond_Indian_Tribe](#F-VRDR-ValueSets-RaceCode-Nansemond_Indian_Tribe 'VRDR.ValueSets.RaceCode.Nansemond_Indian_Tribe')
  - [Nanticoke](#F-VRDR-ValueSets-RaceCode-Nanticoke 'VRDR.ValueSets.RaceCode.Nanticoke')
  - [Nanticoke_Lennilenape](#F-VRDR-ValueSets-RaceCode-Nanticoke_Lennilenape 'VRDR.ValueSets.RaceCode.Nanticoke_Lennilenape')
  - [Narragansett](#F-VRDR-ValueSets-RaceCode-Narragansett 'VRDR.ValueSets.RaceCode.Narragansett')
  - [Natchez](#F-VRDR-ValueSets-RaceCode-Natchez 'VRDR.ValueSets.RaceCode.Natchez')
  - [Native_Hawaiian](#F-VRDR-ValueSets-RaceCode-Native_Hawaiian 'VRDR.ValueSets.RaceCode.Native_Hawaiian')
  - [Native_Hawaiian_Checkbox](#F-VRDR-ValueSets-RaceCode-Native_Hawaiian_Checkbox 'VRDR.ValueSets.RaceCode.Native_Hawaiian_Checkbox')
  - [Native_Of_Hamilton](#F-VRDR-ValueSets-RaceCode-Native_Of_Hamilton 'VRDR.ValueSets.RaceCode.Native_Of_Hamilton')
  - [Native_Village_Of_Akhiok](#F-VRDR-ValueSets-RaceCode-Native_Village_Of_Akhiok 'VRDR.ValueSets.RaceCode.Native_Village_Of_Akhiok')
  - [Native_Village_Of_Akutan](#F-VRDR-ValueSets-RaceCode-Native_Village_Of_Akutan 'VRDR.ValueSets.RaceCode.Native_Village_Of_Akutan')
  - [Native_Village_Of_Aleknagik](#F-VRDR-ValueSets-RaceCode-Native_Village_Of_Aleknagik 'VRDR.ValueSets.RaceCode.Native_Village_Of_Aleknagik')
  - [Native_Village_Of_Ambler](#F-VRDR-ValueSets-RaceCode-Native_Village_Of_Ambler 'VRDR.ValueSets.RaceCode.Native_Village_Of_Ambler')
  - [Native_Village_Of_Atka](#F-VRDR-ValueSets-RaceCode-Native_Village_Of_Atka 'VRDR.ValueSets.RaceCode.Native_Village_Of_Atka')
  - [Native_Village_Of_Barrow_Hilipiat_Traditional_Government](#F-VRDR-ValueSets-RaceCode-Native_Village_Of_Barrow_Hilipiat_Traditional_Government 'VRDR.ValueSets.RaceCode.Native_Village_Of_Barrow_Hilipiat_Traditional_Government')
  - [Native_Village_Of_Belkofski](#F-VRDR-ValueSets-RaceCode-Native_Village_Of_Belkofski 'VRDR.ValueSets.RaceCode.Native_Village_Of_Belkofski')
  - [Native_Village_Of_Brevig_Mission](#F-VRDR-ValueSets-RaceCode-Native_Village_Of_Brevig_Mission 'VRDR.ValueSets.RaceCode.Native_Village_Of_Brevig_Mission')
  - [Native_Village_Of_Cantwell](#F-VRDR-ValueSets-RaceCode-Native_Village_Of_Cantwell 'VRDR.ValueSets.RaceCode.Native_Village_Of_Cantwell')
  - [Native_Village_Of_Chanega](#F-VRDR-ValueSets-RaceCode-Native_Village_Of_Chanega 'VRDR.ValueSets.RaceCode.Native_Village_Of_Chanega')
  - [Native_Village_Of_Chignik](#F-VRDR-ValueSets-RaceCode-Native_Village_Of_Chignik 'VRDR.ValueSets.RaceCode.Native_Village_Of_Chignik')
  - [Native_Village_Of_Chignikn_Lagoon](#F-VRDR-ValueSets-RaceCode-Native_Village_Of_Chignikn_Lagoon 'VRDR.ValueSets.RaceCode.Native_Village_Of_Chignikn_Lagoon')
  - [Native_Village_Of_Chistochina](#F-VRDR-ValueSets-RaceCode-Native_Village_Of_Chistochina 'VRDR.ValueSets.RaceCode.Native_Village_Of_Chistochina')
  - [Native_Village_Of_Chitina](#F-VRDR-ValueSets-RaceCode-Native_Village_Of_Chitina 'VRDR.ValueSets.RaceCode.Native_Village_Of_Chitina')
  - [Native_Village_Of_Chuathbaluk](#F-VRDR-ValueSets-RaceCode-Native_Village_Of_Chuathbaluk 'VRDR.ValueSets.RaceCode.Native_Village_Of_Chuathbaluk')
  - [Native_Village_Of_Council](#F-VRDR-ValueSets-RaceCode-Native_Village_Of_Council 'VRDR.ValueSets.RaceCode.Native_Village_Of_Council')
  - [Native_Village_Of_Deering](#F-VRDR-ValueSets-RaceCode-Native_Village_Of_Deering 'VRDR.ValueSets.RaceCode.Native_Village_Of_Deering')
  - [Native_Village_Of_Diomede](#F-VRDR-ValueSets-RaceCode-Native_Village_Of_Diomede 'VRDR.ValueSets.RaceCode.Native_Village_Of_Diomede')
  - [Native_Village_Of_Eagle](#F-VRDR-ValueSets-RaceCode-Native_Village_Of_Eagle 'VRDR.ValueSets.RaceCode.Native_Village_Of_Eagle')
  - [Native_Village_Of_Eek](#F-VRDR-ValueSets-RaceCode-Native_Village_Of_Eek 'VRDR.ValueSets.RaceCode.Native_Village_Of_Eek')
  - [Native_Village_Of_Ekuk](#F-VRDR-ValueSets-RaceCode-Native_Village_Of_Ekuk 'VRDR.ValueSets.RaceCode.Native_Village_Of_Ekuk')
  - [Native_Village_Of_Elim](#F-VRDR-ValueSets-RaceCode-Native_Village_Of_Elim 'VRDR.ValueSets.RaceCode.Native_Village_Of_Elim')
  - [Native_Village_Of_False_Pass](#F-VRDR-ValueSets-RaceCode-Native_Village_Of_False_Pass 'VRDR.ValueSets.RaceCode.Native_Village_Of_False_Pass')
  - [Native_Village_Of_Fort_Yukon](#F-VRDR-ValueSets-RaceCode-Native_Village_Of_Fort_Yukon 'VRDR.ValueSets.RaceCode.Native_Village_Of_Fort_Yukon')
  - [Native_Village_Of_Gakona](#F-VRDR-ValueSets-RaceCode-Native_Village_Of_Gakona 'VRDR.ValueSets.RaceCode.Native_Village_Of_Gakona')
  - [Native_Village_Of_Gambell](#F-VRDR-ValueSets-RaceCode-Native_Village_Of_Gambell 'VRDR.ValueSets.RaceCode.Native_Village_Of_Gambell')
  - [Native_Village_Of_Georgetown](#F-VRDR-ValueSets-RaceCode-Native_Village_Of_Georgetown 'VRDR.ValueSets.RaceCode.Native_Village_Of_Georgetown')
  - [Native_Village_Of_Goodnews_Bay](#F-VRDR-ValueSets-RaceCode-Native_Village_Of_Goodnews_Bay 'VRDR.ValueSets.RaceCode.Native_Village_Of_Goodnews_Bay')
  - [Native_Village_Of_Hooper_Bay](#F-VRDR-ValueSets-RaceCode-Native_Village_Of_Hooper_Bay 'VRDR.ValueSets.RaceCode.Native_Village_Of_Hooper_Bay')
  - [Native_Village_Of_Kanatak](#F-VRDR-ValueSets-RaceCode-Native_Village_Of_Kanatak 'VRDR.ValueSets.RaceCode.Native_Village_Of_Kanatak')
  - [Native_Village_Of_Karluk](#F-VRDR-ValueSets-RaceCode-Native_Village_Of_Karluk 'VRDR.ValueSets.RaceCode.Native_Village_Of_Karluk')
  - [Native_Village_Of_Kasigluk](#F-VRDR-ValueSets-RaceCode-Native_Village_Of_Kasigluk 'VRDR.ValueSets.RaceCode.Native_Village_Of_Kasigluk')
  - [Native_Village_Of_Kiana](#F-VRDR-ValueSets-RaceCode-Native_Village_Of_Kiana 'VRDR.ValueSets.RaceCode.Native_Village_Of_Kiana')
  - [Native_Village_Of_Kipnuk](#F-VRDR-ValueSets-RaceCode-Native_Village_Of_Kipnuk 'VRDR.ValueSets.RaceCode.Native_Village_Of_Kipnuk')
  - [Native_Village_Of_Kivalina](#F-VRDR-ValueSets-RaceCode-Native_Village_Of_Kivalina 'VRDR.ValueSets.RaceCode.Native_Village_Of_Kivalina')
  - [Native_Village_Of_Kluti_Kaah](#F-VRDR-ValueSets-RaceCode-Native_Village_Of_Kluti_Kaah 'VRDR.ValueSets.RaceCode.Native_Village_Of_Kluti_Kaah')
  - [Native_Village_Of_Kobuk](#F-VRDR-ValueSets-RaceCode-Native_Village_Of_Kobuk 'VRDR.ValueSets.RaceCode.Native_Village_Of_Kobuk')
  - [Native_Village_Of_Kongiganak](#F-VRDR-ValueSets-RaceCode-Native_Village_Of_Kongiganak 'VRDR.ValueSets.RaceCode.Native_Village_Of_Kongiganak')
  - [Native_Village_Of_Kotzebue](#F-VRDR-ValueSets-RaceCode-Native_Village_Of_Kotzebue 'VRDR.ValueSets.RaceCode.Native_Village_Of_Kotzebue')
  - [Native_Village_Of_Koyuk](#F-VRDR-ValueSets-RaceCode-Native_Village_Of_Koyuk 'VRDR.ValueSets.RaceCode.Native_Village_Of_Koyuk')
  - [Native_Village_Of_Kwigillingok](#F-VRDR-ValueSets-RaceCode-Native_Village_Of_Kwigillingok 'VRDR.ValueSets.RaceCode.Native_Village_Of_Kwigillingok')
  - [Native_Village_Of_Kwinhagak](#F-VRDR-ValueSets-RaceCode-Native_Village_Of_Kwinhagak 'VRDR.ValueSets.RaceCode.Native_Village_Of_Kwinhagak')
  - [Native_Village_Of_Larsen_Bay](#F-VRDR-ValueSets-RaceCode-Native_Village_Of_Larsen_Bay 'VRDR.ValueSets.RaceCode.Native_Village_Of_Larsen_Bay')
  - [Native_Village_Of_Marshall](#F-VRDR-ValueSets-RaceCode-Native_Village_Of_Marshall 'VRDR.ValueSets.RaceCode.Native_Village_Of_Marshall')
  - [Native_Village_Of_Marys_Igloo](#F-VRDR-ValueSets-RaceCode-Native_Village_Of_Marys_Igloo 'VRDR.ValueSets.RaceCode.Native_Village_Of_Marys_Igloo')
  - [Native_Village_Of_Mekoryuk](#F-VRDR-ValueSets-RaceCode-Native_Village_Of_Mekoryuk 'VRDR.ValueSets.RaceCode.Native_Village_Of_Mekoryuk')
  - [Native_Village_Of_Minto](#F-VRDR-ValueSets-RaceCode-Native_Village_Of_Minto 'VRDR.ValueSets.RaceCode.Native_Village_Of_Minto')
  - [Native_Village_Of_Nanwaiek](#F-VRDR-ValueSets-RaceCode-Native_Village_Of_Nanwaiek 'VRDR.ValueSets.RaceCode.Native_Village_Of_Nanwaiek')
  - [Native_Village_Of_Napaimute](#F-VRDR-ValueSets-RaceCode-Native_Village_Of_Napaimute 'VRDR.ValueSets.RaceCode.Native_Village_Of_Napaimute')
  - [Native_Village_Of_Napakiak](#F-VRDR-ValueSets-RaceCode-Native_Village_Of_Napakiak 'VRDR.ValueSets.RaceCode.Native_Village_Of_Napakiak')
  - [Native_Village_Of_Napaskiak](#F-VRDR-ValueSets-RaceCode-Native_Village_Of_Napaskiak 'VRDR.ValueSets.RaceCode.Native_Village_Of_Napaskiak')
  - [Native_Village_Of_Nelson_Lagoon](#F-VRDR-ValueSets-RaceCode-Native_Village_Of_Nelson_Lagoon 'VRDR.ValueSets.RaceCode.Native_Village_Of_Nelson_Lagoon')
  - [Native_Village_Of_Nightmute](#F-VRDR-ValueSets-RaceCode-Native_Village_Of_Nightmute 'VRDR.ValueSets.RaceCode.Native_Village_Of_Nightmute')
  - [Native_Village_Of_Nikolski](#F-VRDR-ValueSets-RaceCode-Native_Village_Of_Nikolski 'VRDR.ValueSets.RaceCode.Native_Village_Of_Nikolski')
  - [Native_Village_Of_Noatak](#F-VRDR-ValueSets-RaceCode-Native_Village_Of_Noatak 'VRDR.ValueSets.RaceCode.Native_Village_Of_Noatak')
  - [Native_Village_Of_Nuiqsut](#F-VRDR-ValueSets-RaceCode-Native_Village_Of_Nuiqsut 'VRDR.ValueSets.RaceCode.Native_Village_Of_Nuiqsut')
  - [Native_Village_Of_Nunapitchuk](#F-VRDR-ValueSets-RaceCode-Native_Village_Of_Nunapitchuk 'VRDR.ValueSets.RaceCode.Native_Village_Of_Nunapitchuk')
  - [Native_Village_Of_Ouzinkie](#F-VRDR-ValueSets-RaceCode-Native_Village_Of_Ouzinkie 'VRDR.ValueSets.RaceCode.Native_Village_Of_Ouzinkie')
  - [Native_Village_Of_Perryville](#F-VRDR-ValueSets-RaceCode-Native_Village_Of_Perryville 'VRDR.ValueSets.RaceCode.Native_Village_Of_Perryville')
  - [Native_Village_Of_Pilot_Point](#F-VRDR-ValueSets-RaceCode-Native_Village_Of_Pilot_Point 'VRDR.ValueSets.RaceCode.Native_Village_Of_Pilot_Point')
  - [Native_Village_Of_Pitkas_Point](#F-VRDR-ValueSets-RaceCode-Native_Village_Of_Pitkas_Point 'VRDR.ValueSets.RaceCode.Native_Village_Of_Pitkas_Point')
  - [Native_Village_Of_Point_Hope](#F-VRDR-ValueSets-RaceCode-Native_Village_Of_Point_Hope 'VRDR.ValueSets.RaceCode.Native_Village_Of_Point_Hope')
  - [Native_Village_Of_Point_Lay](#F-VRDR-ValueSets-RaceCode-Native_Village_Of_Point_Lay 'VRDR.ValueSets.RaceCode.Native_Village_Of_Point_Lay')
  - [Native_Village_Of_Port_Graham](#F-VRDR-ValueSets-RaceCode-Native_Village_Of_Port_Graham 'VRDR.ValueSets.RaceCode.Native_Village_Of_Port_Graham')
  - [Native_Village_Of_Port_Heiden](#F-VRDR-ValueSets-RaceCode-Native_Village_Of_Port_Heiden 'VRDR.ValueSets.RaceCode.Native_Village_Of_Port_Heiden')
  - [Native_Village_Of_Port_Lions](#F-VRDR-ValueSets-RaceCode-Native_Village_Of_Port_Lions 'VRDR.ValueSets.RaceCode.Native_Village_Of_Port_Lions')
  - [Native_Village_Of_Ruby](#F-VRDR-ValueSets-RaceCode-Native_Village_Of_Ruby 'VRDR.ValueSets.RaceCode.Native_Village_Of_Ruby')
  - [Native_Village_Of_Saint_Michael](#F-VRDR-ValueSets-RaceCode-Native_Village_Of_Saint_Michael 'VRDR.ValueSets.RaceCode.Native_Village_Of_Saint_Michael')
  - [Native_Village_Of_Savoonga](#F-VRDR-ValueSets-RaceCode-Native_Village_Of_Savoonga 'VRDR.ValueSets.RaceCode.Native_Village_Of_Savoonga')
  - [Native_Village_Of_Scammon_Bay](#F-VRDR-ValueSets-RaceCode-Native_Village_Of_Scammon_Bay 'VRDR.ValueSets.RaceCode.Native_Village_Of_Scammon_Bay')
  - [Native_Village_Of_Selawik](#F-VRDR-ValueSets-RaceCode-Native_Village_Of_Selawik 'VRDR.ValueSets.RaceCode.Native_Village_Of_Selawik')
  - [Native_Village_Of_Shaktoolik](#F-VRDR-ValueSets-RaceCode-Native_Village_Of_Shaktoolik 'VRDR.ValueSets.RaceCode.Native_Village_Of_Shaktoolik')
  - [Native_Village_Of_Sheldons_Point](#F-VRDR-ValueSets-RaceCode-Native_Village_Of_Sheldons_Point 'VRDR.ValueSets.RaceCode.Native_Village_Of_Sheldons_Point')
  - [Native_Village_Of_Shishmaref](#F-VRDR-ValueSets-RaceCode-Native_Village_Of_Shishmaref 'VRDR.ValueSets.RaceCode.Native_Village_Of_Shishmaref')
  - [Native_Village_Of_Shungnak](#F-VRDR-ValueSets-RaceCode-Native_Village_Of_Shungnak 'VRDR.ValueSets.RaceCode.Native_Village_Of_Shungnak')
  - [Native_Village_Of_Stevens](#F-VRDR-ValueSets-RaceCode-Native_Village_Of_Stevens 'VRDR.ValueSets.RaceCode.Native_Village_Of_Stevens')
  - [Native_Village_Of_Tanacross](#F-VRDR-ValueSets-RaceCode-Native_Village_Of_Tanacross 'VRDR.ValueSets.RaceCode.Native_Village_Of_Tanacross')
  - [Native_Village_Of_Tanana](#F-VRDR-ValueSets-RaceCode-Native_Village_Of_Tanana 'VRDR.ValueSets.RaceCode.Native_Village_Of_Tanana')
  - [Native_Village_Of_Tatitlek](#F-VRDR-ValueSets-RaceCode-Native_Village_Of_Tatitlek 'VRDR.ValueSets.RaceCode.Native_Village_Of_Tatitlek')
  - [Native_Village_Of_Tazlina](#F-VRDR-ValueSets-RaceCode-Native_Village_Of_Tazlina 'VRDR.ValueSets.RaceCode.Native_Village_Of_Tazlina')
  - [Native_Village_Of_Teller](#F-VRDR-ValueSets-RaceCode-Native_Village_Of_Teller 'VRDR.ValueSets.RaceCode.Native_Village_Of_Teller')
  - [Native_Village_Of_Tetlin](#F-VRDR-ValueSets-RaceCode-Native_Village_Of_Tetlin 'VRDR.ValueSets.RaceCode.Native_Village_Of_Tetlin')
  - [Native_Village_Of_Toksook_Bay](#F-VRDR-ValueSets-RaceCode-Native_Village_Of_Toksook_Bay 'VRDR.ValueSets.RaceCode.Native_Village_Of_Toksook_Bay')
  - [Native_Village_Of_Tuntutuliak](#F-VRDR-ValueSets-RaceCode-Native_Village_Of_Tuntutuliak 'VRDR.ValueSets.RaceCode.Native_Village_Of_Tuntutuliak')
  - [Native_Village_Of_Tununak](#F-VRDR-ValueSets-RaceCode-Native_Village_Of_Tununak 'VRDR.ValueSets.RaceCode.Native_Village_Of_Tununak')
  - [Native_Village_Of_Tyonek](#F-VRDR-ValueSets-RaceCode-Native_Village_Of_Tyonek 'VRDR.ValueSets.RaceCode.Native_Village_Of_Tyonek')
  - [Native_Village_Of_Unalakleet](#F-VRDR-ValueSets-RaceCode-Native_Village_Of_Unalakleet 'VRDR.ValueSets.RaceCode.Native_Village_Of_Unalakleet')
  - [Native_Village_Of_Unga](#F-VRDR-ValueSets-RaceCode-Native_Village_Of_Unga 'VRDR.ValueSets.RaceCode.Native_Village_Of_Unga')
  - [Native_Village_Ofbuckland](#F-VRDR-ValueSets-RaceCode-Native_Village_Ofbuckland 'VRDR.ValueSets.RaceCode.Native_Village_Ofbuckland')
  - [Nausu_Waiwash](#F-VRDR-ValueSets-RaceCode-Nausu_Waiwash 'VRDR.ValueSets.RaceCode.Nausu_Waiwash')
  - [Navajo](#F-VRDR-ValueSets-RaceCode-Navajo 'VRDR.ValueSets.RaceCode.Navajo')
  - [Near_Easterner](#F-VRDR-ValueSets-RaceCode-Near_Easterner 'VRDR.ValueSets.RaceCode.Near_Easterner')
  - [Nebraska_Ponca](#F-VRDR-ValueSets-RaceCode-Nebraska_Ponca 'VRDR.ValueSets.RaceCode.Nebraska_Ponca')
  - [Nebraska_Winnebago](#F-VRDR-ValueSets-RaceCode-Nebraska_Winnebago 'VRDR.ValueSets.RaceCode.Nebraska_Winnebago')
  - [Negro](#F-VRDR-ValueSets-RaceCode-Negro 'VRDR.ValueSets.RaceCode.Negro')
  - [Nenana_Native_Association](#F-VRDR-ValueSets-RaceCode-Nenana_Native_Association 'VRDR.ValueSets.RaceCode.Nenana_Native_Association')
  - [Nepalese](#F-VRDR-ValueSets-RaceCode-Nepalese 'VRDR.ValueSets.RaceCode.Nepalese')
  - [New_Hebrides](#F-VRDR-ValueSets-RaceCode-New_Hebrides 'VRDR.ValueSets.RaceCode.New_Hebrides')
  - [New_Koliganek_Village_Council](#F-VRDR-ValueSets-RaceCode-New_Koliganek_Village_Council 'VRDR.ValueSets.RaceCode.New_Koliganek_Village_Council')
  - [New_Stuyahok_Village](#F-VRDR-ValueSets-RaceCode-New_Stuyahok_Village 'VRDR.ValueSets.RaceCode.New_Stuyahok_Village')
  - [Newhalen_Village](#F-VRDR-ValueSets-RaceCode-Newhalen_Village 'VRDR.ValueSets.RaceCode.Newhalen_Village')
  - [Newtek_Village](#F-VRDR-ValueSets-RaceCode-Newtek_Village 'VRDR.ValueSets.RaceCode.Newtek_Village')
  - [Nez_Perce](#F-VRDR-ValueSets-RaceCode-Nez_Perce 'VRDR.ValueSets.RaceCode.Nez_Perce')
  - [Nicaraguan](#F-VRDR-ValueSets-RaceCode-Nicaraguan 'VRDR.ValueSets.RaceCode.Nicaraguan')
  - [Nigerian](#F-VRDR-ValueSets-RaceCode-Nigerian 'VRDR.ValueSets.RaceCode.Nigerian')
  - [Nigritian](#F-VRDR-ValueSets-RaceCode-Nigritian 'VRDR.ValueSets.RaceCode.Nigritian')
  - [Nikolai_Village](#F-VRDR-ValueSets-RaceCode-Nikolai_Village 'VRDR.ValueSets.RaceCode.Nikolai_Village')
  - [Ninilchik_Village_Traditional_Council](#F-VRDR-ValueSets-RaceCode-Ninilchik_Village_Traditional_Council 'VRDR.ValueSets.RaceCode.Ninilchik_Village_Traditional_Council')
  - [Nipmuc](#F-VRDR-ValueSets-RaceCode-Nipmuc 'VRDR.ValueSets.RaceCode.Nipmuc')
  - [Nisenen](#F-VRDR-ValueSets-RaceCode-Nisenen 'VRDR.ValueSets.RaceCode.Nisenen')
  - [Nisqually](#F-VRDR-ValueSets-RaceCode-Nisqually 'VRDR.ValueSets.RaceCode.Nisqually')
  - [Nome_Eskimo_Community](#F-VRDR-ValueSets-RaceCode-Nome_Eskimo_Community 'VRDR.ValueSets.RaceCode.Nome_Eskimo_Community')
  - [Nomlaki](#F-VRDR-ValueSets-RaceCode-Nomlaki 'VRDR.ValueSets.RaceCode.Nomlaki')
  - [Nondalton_Village](#F-VRDR-ValueSets-RaceCode-Nondalton_Village 'VRDR.ValueSets.RaceCode.Nondalton_Village')
  - [Nooksack](#F-VRDR-ValueSets-RaceCode-Nooksack 'VRDR.ValueSets.RaceCode.Nooksack')
  - [Noorvik_Native_Community](#F-VRDR-ValueSets-RaceCode-Noorvik_Native_Community 'VRDR.ValueSets.RaceCode.Noorvik_Native_Community')
  - [North_African](#F-VRDR-ValueSets-RaceCode-North_African 'VRDR.ValueSets.RaceCode.North_African')
  - [North_Fork_Rancheria](#F-VRDR-ValueSets-RaceCode-North_Fork_Rancheria 'VRDR.ValueSets.RaceCode.North_Fork_Rancheria')
  - [Northern_Arapahoe](#F-VRDR-ValueSets-RaceCode-Northern_Arapahoe 'VRDR.ValueSets.RaceCode.Northern_Arapahoe')
  - [Northern_Cherokee_Nation_Of_Missouri_And_Arkansas](#F-VRDR-ValueSets-RaceCode-Northern_Cherokee_Nation_Of_Missouri_And_Arkansas 'VRDR.ValueSets.RaceCode.Northern_Cherokee_Nation_Of_Missouri_And_Arkansas')
  - [Northern_Cheyenne](#F-VRDR-ValueSets-RaceCode-Northern_Cheyenne 'VRDR.ValueSets.RaceCode.Northern_Cheyenne')
  - [Northern_Paiute](#F-VRDR-ValueSets-RaceCode-Northern_Paiute 'VRDR.ValueSets.RaceCode.Northern_Paiute')
  - [Northern_Pomo](#F-VRDR-ValueSets-RaceCode-Northern_Pomo 'VRDR.ValueSets.RaceCode.Northern_Pomo')
  - [Northway_Village](#F-VRDR-ValueSets-RaceCode-Northway_Village 'VRDR.ValueSets.RaceCode.Northway_Village')
  - [Northwestern_Band_Of_Shoshoni_Nation_Of_Utah](#F-VRDR-ValueSets-RaceCode-Northwestern_Band_Of_Shoshoni_Nation_Of_Utah 'VRDR.ValueSets.RaceCode.Northwestern_Band_Of_Shoshoni_Nation_Of_Utah')
  - [Nulato_Village](#F-VRDR-ValueSets-RaceCode-Nulato_Village 'VRDR.ValueSets.RaceCode.Nulato_Village')
  - [Octoroon](#F-VRDR-ValueSets-RaceCode-Octoroon 'VRDR.ValueSets.RaceCode.Octoroon')
  - [Odgers_Ranch](#F-VRDR-ValueSets-RaceCode-Odgers_Ranch 'VRDR.ValueSets.RaceCode.Odgers_Ranch')
  - [Oglala_Sioux](#F-VRDR-ValueSets-RaceCode-Oglala_Sioux 'VRDR.ValueSets.RaceCode.Oglala_Sioux')
  - [Okinawan](#F-VRDR-ValueSets-RaceCode-Okinawan 'VRDR.ValueSets.RaceCode.Okinawan')
  - [Oklahoma_Apache](#F-VRDR-ValueSets-RaceCode-Oklahoma_Apache 'VRDR.ValueSets.RaceCode.Oklahoma_Apache')
  - [Oklahoma_Choctaw](#F-VRDR-ValueSets-RaceCode-Oklahoma_Choctaw 'VRDR.ValueSets.RaceCode.Oklahoma_Choctaw')
  - [Oklahoma_Comanche](#F-VRDR-ValueSets-RaceCode-Oklahoma_Comanche 'VRDR.ValueSets.RaceCode.Oklahoma_Comanche')
  - [Oklahoma_Kickapoo](#F-VRDR-ValueSets-RaceCode-Oklahoma_Kickapoo 'VRDR.ValueSets.RaceCode.Oklahoma_Kickapoo')
  - [Oklahoma_Kiowa](#F-VRDR-ValueSets-RaceCode-Oklahoma_Kiowa 'VRDR.ValueSets.RaceCode.Oklahoma_Kiowa')
  - [Oklahoma_Miami](#F-VRDR-ValueSets-RaceCode-Oklahoma_Miami 'VRDR.ValueSets.RaceCode.Oklahoma_Miami')
  - [Oklahoma_Modoc](#F-VRDR-ValueSets-RaceCode-Oklahoma_Modoc 'VRDR.ValueSets.RaceCode.Oklahoma_Modoc')
  - [Oklahoma_Ottawa](#F-VRDR-ValueSets-RaceCode-Oklahoma_Ottawa 'VRDR.ValueSets.RaceCode.Oklahoma_Ottawa')
  - [Oklahoma_Pawnee](#F-VRDR-ValueSets-RaceCode-Oklahoma_Pawnee 'VRDR.ValueSets.RaceCode.Oklahoma_Pawnee')
  - [Oklahoma_Peoria](#F-VRDR-ValueSets-RaceCode-Oklahoma_Peoria 'VRDR.ValueSets.RaceCode.Oklahoma_Peoria')
  - [Oklahoma_Ponca](#F-VRDR-ValueSets-RaceCode-Oklahoma_Ponca 'VRDR.ValueSets.RaceCode.Oklahoma_Ponca')
  - [Oklahoma_Seminole](#F-VRDR-ValueSets-RaceCode-Oklahoma_Seminole 'VRDR.ValueSets.RaceCode.Oklahoma_Seminole')
  - [Omaha](#F-VRDR-ValueSets-RaceCode-Omaha 'VRDR.ValueSets.RaceCode.Omaha')
  - [Oneida_Nation_Of_New_York](#F-VRDR-ValueSets-RaceCode-Oneida_Nation_Of_New_York 'VRDR.ValueSets.RaceCode.Oneida_Nation_Of_New_York')
  - [Oneida_Tribe_Of_Wisconsin](#F-VRDR-ValueSets-RaceCode-Oneida_Tribe_Of_Wisconsin 'VRDR.ValueSets.RaceCode.Oneida_Tribe_Of_Wisconsin')
  - [Onondaga](#F-VRDR-ValueSets-RaceCode-Onondaga 'VRDR.ValueSets.RaceCode.Onondaga')
  - [Ontonagon](#F-VRDR-ValueSets-RaceCode-Ontonagon 'VRDR.ValueSets.RaceCode.Ontonagon')
  - [Oregon_Athabaskan](#F-VRDR-ValueSets-RaceCode-Oregon_Athabaskan 'VRDR.ValueSets.RaceCode.Oregon_Athabaskan')
  - [Organized_Village_Of_Grayling](#F-VRDR-ValueSets-RaceCode-Organized_Village_Of_Grayling 'VRDR.ValueSets.RaceCode.Organized_Village_Of_Grayling')
  - [Organized_Village_Of_Kake](#F-VRDR-ValueSets-RaceCode-Organized_Village_Of_Kake 'VRDR.ValueSets.RaceCode.Organized_Village_Of_Kake')
  - [Organized_Village_Of_Kasaan](#F-VRDR-ValueSets-RaceCode-Organized_Village_Of_Kasaan 'VRDR.ValueSets.RaceCode.Organized_Village_Of_Kasaan')
  - [Organized_Village_Of_Kwethluk](#F-VRDR-ValueSets-RaceCode-Organized_Village_Of_Kwethluk 'VRDR.ValueSets.RaceCode.Organized_Village_Of_Kwethluk')
  - [Organized_Village_Of_Saxman](#F-VRDR-ValueSets-RaceCode-Organized_Village_Of_Saxman 'VRDR.ValueSets.RaceCode.Organized_Village_Of_Saxman')
  - [Oriental](#F-VRDR-ValueSets-RaceCode-Oriental 'VRDR.ValueSets.RaceCode.Oriental')
  - [Orutsararmuit_Native_Village](#F-VRDR-ValueSets-RaceCode-Orutsararmuit_Native_Village 'VRDR.ValueSets.RaceCode.Orutsararmuit_Native_Village')
  - [Osage](#F-VRDR-ValueSets-RaceCode-Osage 'VRDR.ValueSets.RaceCode.Osage')
  - [Oscarville_Traditional_Village](#F-VRDR-ValueSets-RaceCode-Oscarville_Traditional_Village 'VRDR.ValueSets.RaceCode.Oscarville_Traditional_Village')
  - [Other_African](#F-VRDR-ValueSets-RaceCode-Other_African 'VRDR.ValueSets.RaceCode.Other_African')
  - [Other_Alaskan_Nec](#F-VRDR-ValueSets-RaceCode-Other_Alaskan_Nec 'VRDR.ValueSets.RaceCode.Other_Alaskan_Nec')
  - [Other_Arab](#F-VRDR-ValueSets-RaceCode-Other_Arab 'VRDR.ValueSets.RaceCode.Other_Arab')
  - [Other_Race_N_E_C](#F-VRDR-ValueSets-RaceCode-Other_Race_N_E_C 'VRDR.ValueSets.RaceCode.Other_Race_N_E_C')
  - [Other_Spanish](#F-VRDR-ValueSets-RaceCode-Other_Spanish 'VRDR.ValueSets.RaceCode.Other_Spanish')
  - [Otoemissouria](#F-VRDR-ValueSets-RaceCode-Otoemissouria 'VRDR.ValueSets.RaceCode.Otoemissouria')
  - [Ottawa](#F-VRDR-ValueSets-RaceCode-Ottawa 'VRDR.ValueSets.RaceCode.Ottawa')
  - [Pacific_Islander](#F-VRDR-ValueSets-RaceCode-Pacific_Islander 'VRDR.ValueSets.RaceCode.Pacific_Islander')
  - [Paiute](#F-VRDR-ValueSets-RaceCode-Paiute 'VRDR.ValueSets.RaceCode.Paiute')
  - [Pakistani](#F-VRDR-ValueSets-RaceCode-Pakistani 'VRDR.ValueSets.RaceCode.Pakistani')
  - [Pala_Band_Of_Luiseno_Mission_Indians](#F-VRDR-ValueSets-RaceCode-Pala_Band_Of_Luiseno_Mission_Indians 'VRDR.ValueSets.RaceCode.Pala_Band_Of_Luiseno_Mission_Indians')
  - [Palauan](#F-VRDR-ValueSets-RaceCode-Palauan 'VRDR.ValueSets.RaceCode.Palauan')
  - [Palestinian](#F-VRDR-ValueSets-RaceCode-Palestinian 'VRDR.ValueSets.RaceCode.Palestinian')
  - [Pamunkey_Indian_Tribe](#F-VRDR-ValueSets-RaceCode-Pamunkey_Indian_Tribe 'VRDR.ValueSets.RaceCode.Pamunkey_Indian_Tribe')
  - [Panamanian](#F-VRDR-ValueSets-RaceCode-Panamanian 'VRDR.ValueSets.RaceCode.Panamanian')
  - [Panamint](#F-VRDR-ValueSets-RaceCode-Panamint 'VRDR.ValueSets.RaceCode.Panamint')
  - [Papua_New_Guinean](#F-VRDR-ValueSets-RaceCode-Papua_New_Guinean 'VRDR.ValueSets.RaceCode.Papua_New_Guinean')
  - [Paraguayan](#F-VRDR-ValueSets-RaceCode-Paraguayan 'VRDR.ValueSets.RaceCode.Paraguayan')
  - [Part_Hawaiian](#F-VRDR-ValueSets-RaceCode-Part_Hawaiian 'VRDR.ValueSets.RaceCode.Part_Hawaiian')
  - [Pascua_Yaqui](#F-VRDR-ValueSets-RaceCode-Pascua_Yaqui 'VRDR.ValueSets.RaceCode.Pascua_Yaqui')
  - [Paskenta_Band_Of_Nomlaki_Indians](#F-VRDR-ValueSets-RaceCode-Paskenta_Band_Of_Nomlaki_Indians 'VRDR.ValueSets.RaceCode.Paskenta_Band_Of_Nomlaki_Indians')
  - [Passamaquoddy](#F-VRDR-ValueSets-RaceCode-Passamaquoddy 'VRDR.ValueSets.RaceCode.Passamaquoddy')
  - [Paucatuck_Eastern_Pequot](#F-VRDR-ValueSets-RaceCode-Paucatuck_Eastern_Pequot 'VRDR.ValueSets.RaceCode.Paucatuck_Eastern_Pequot')
  - [Pauloff_Harbor_Village](#F-VRDR-ValueSets-RaceCode-Pauloff_Harbor_Village 'VRDR.ValueSets.RaceCode.Pauloff_Harbor_Village')
  - [Pauma_Band_Of_Luiseno_Mission_Indians](#F-VRDR-ValueSets-RaceCode-Pauma_Band_Of_Luiseno_Mission_Indians 'VRDR.ValueSets.RaceCode.Pauma_Band_Of_Luiseno_Mission_Indians')
  - [Pawnee](#F-VRDR-ValueSets-RaceCode-Pawnee 'VRDR.ValueSets.RaceCode.Pawnee')
  - [Payson_Tonto_Apache](#F-VRDR-ValueSets-RaceCode-Payson_Tonto_Apache 'VRDR.ValueSets.RaceCode.Payson_Tonto_Apache')
  - [Pechanga_Band_Of_Luiseno_Mission_Indians](#F-VRDR-ValueSets-RaceCode-Pechanga_Band_Of_Luiseno_Mission_Indians 'VRDR.ValueSets.RaceCode.Pechanga_Band_Of_Luiseno_Mission_Indians')
  - [Pedro_Bay_Village](#F-VRDR-ValueSets-RaceCode-Pedro_Bay_Village 'VRDR.ValueSets.RaceCode.Pedro_Bay_Village')
  - [Pelican](#F-VRDR-ValueSets-RaceCode-Pelican 'VRDR.ValueSets.RaceCode.Pelican')
  - [Penobscot](#F-VRDR-ValueSets-RaceCode-Penobscot 'VRDR.ValueSets.RaceCode.Penobscot')
  - [Peoria](#F-VRDR-ValueSets-RaceCode-Peoria 'VRDR.ValueSets.RaceCode.Peoria')
  - [Pequot](#F-VRDR-ValueSets-RaceCode-Pequot 'VRDR.ValueSets.RaceCode.Pequot')
  - [Peruvian](#F-VRDR-ValueSets-RaceCode-Peruvian 'VRDR.ValueSets.RaceCode.Peruvian')
  - [Petersburg_Indian_Association](#F-VRDR-ValueSets-RaceCode-Petersburg_Indian_Association 'VRDR.ValueSets.RaceCode.Petersburg_Indian_Association')
  - [Picayune_Rancheria_Of_Chukchansi_Indians](#F-VRDR-ValueSets-RaceCode-Picayune_Rancheria_Of_Chukchansi_Indians 'VRDR.ValueSets.RaceCode.Picayune_Rancheria_Of_Chukchansi_Indians')
  - [Picuris](#F-VRDR-ValueSets-RaceCode-Picuris 'VRDR.ValueSets.RaceCode.Picuris')
  - [Pilot_Station_Traditional_Village](#F-VRDR-ValueSets-RaceCode-Pilot_Station_Traditional_Village 'VRDR.ValueSets.RaceCode.Pilot_Station_Traditional_Village')
  - [Pima](#F-VRDR-ValueSets-RaceCode-Pima 'VRDR.ValueSets.RaceCode.Pima')
  - [Pine_Ridge_Sioux](#F-VRDR-ValueSets-RaceCode-Pine_Ridge_Sioux 'VRDR.ValueSets.RaceCode.Pine_Ridge_Sioux')
  - [Pinoleville_Rancheria_Of_Pomo_Indians](#F-VRDR-ValueSets-RaceCode-Pinoleville_Rancheria_Of_Pomo_Indians 'VRDR.ValueSets.RaceCode.Pinoleville_Rancheria_Of_Pomo_Indians')
  - [Pipestone_Sioux](#F-VRDR-ValueSets-RaceCode-Pipestone_Sioux 'VRDR.ValueSets.RaceCode.Pipestone_Sioux')
  - [Piqua_Sept_Of_Ohio_Shawnee](#F-VRDR-ValueSets-RaceCode-Piqua_Sept_Of_Ohio_Shawnee 'VRDR.ValueSets.RaceCode.Piqua_Sept_Of_Ohio_Shawnee')
  - [Piro](#F-VRDR-ValueSets-RaceCode-Piro 'VRDR.ValueSets.RaceCode.Piro')
  - [Piscataway](#F-VRDR-ValueSets-RaceCode-Piscataway 'VRDR.ValueSets.RaceCode.Piscataway')
  - [Pit_River_Tribe_Of_California](#F-VRDR-ValueSets-RaceCode-Pit_River_Tribe_Of_California 'VRDR.ValueSets.RaceCode.Pit_River_Tribe_Of_California')
  - [Platinum_Traditional_Village](#F-VRDR-ValueSets-RaceCode-Platinum_Traditional_Village 'VRDR.ValueSets.RaceCode.Platinum_Traditional_Village')
  - [Pleasant_Point_Passamaquoddy](#F-VRDR-ValueSets-RaceCode-Pleasant_Point_Passamaquoddy 'VRDR.ValueSets.RaceCode.Pleasant_Point_Passamaquoddy')
  - [Poarch_Band](#F-VRDR-ValueSets-RaceCode-Poarch_Band 'VRDR.ValueSets.RaceCode.Poarch_Band')
  - [Pocasset_Wampanoag](#F-VRDR-ValueSets-RaceCode-Pocasset_Wampanoag 'VRDR.ValueSets.RaceCode.Pocasset_Wampanoag')
  - [Pocomoke_Acohonock](#F-VRDR-ValueSets-RaceCode-Pocomoke_Acohonock 'VRDR.ValueSets.RaceCode.Pocomoke_Acohonock')
  - [Pohnpeian](#F-VRDR-ValueSets-RaceCode-Pohnpeian 'VRDR.ValueSets.RaceCode.Pohnpeian')
  - [Pojoaque](#F-VRDR-ValueSets-RaceCode-Pojoaque 'VRDR.ValueSets.RaceCode.Pojoaque')
  - [Pokagon_Band_Of_Potawatomi_Indians](#F-VRDR-ValueSets-RaceCode-Pokagon_Band_Of_Potawatomi_Indians 'VRDR.ValueSets.RaceCode.Pokagon_Band_Of_Potawatomi_Indians')
  - [Polish](#F-VRDR-ValueSets-RaceCode-Polish 'VRDR.ValueSets.RaceCode.Polish')
  - [Polynesian](#F-VRDR-ValueSets-RaceCode-Polynesian 'VRDR.ValueSets.RaceCode.Polynesian')
  - [Pomo](#F-VRDR-ValueSets-RaceCode-Pomo 'VRDR.ValueSets.RaceCode.Pomo')
  - [Ponca](#F-VRDR-ValueSets-RaceCode-Ponca 'VRDR.ValueSets.RaceCode.Ponca')
  - [Pondre_Band_Of_Salish_And_Kootenai](#F-VRDR-ValueSets-RaceCode-Pondre_Band_Of_Salish_And_Kootenai 'VRDR.ValueSets.RaceCode.Pondre_Band_Of_Salish_And_Kootenai')
  - [Poospatuck](#F-VRDR-ValueSets-RaceCode-Poospatuck 'VRDR.ValueSets.RaceCode.Poospatuck')
  - [Port_Gamble_Klallam](#F-VRDR-ValueSets-RaceCode-Port_Gamble_Klallam 'VRDR.ValueSets.RaceCode.Port_Gamble_Klallam')
  - [Port_Madison](#F-VRDR-ValueSets-RaceCode-Port_Madison 'VRDR.ValueSets.RaceCode.Port_Madison')
  - [Portage_Creek_Village](#F-VRDR-ValueSets-RaceCode-Portage_Creek_Village 'VRDR.ValueSets.RaceCode.Portage_Creek_Village')
  - [Portuguese](#F-VRDR-ValueSets-RaceCode-Portuguese 'VRDR.ValueSets.RaceCode.Portuguese')
  - [Potawatomi](#F-VRDR-ValueSets-RaceCode-Potawatomi 'VRDR.ValueSets.RaceCode.Potawatomi')
  - [Potter_Valley_Rancheria_Of_Pomo_Indians](#F-VRDR-ValueSets-RaceCode-Potter_Valley_Rancheria_Of_Pomo_Indians 'VRDR.ValueSets.RaceCode.Potter_Valley_Rancheria_Of_Pomo_Indians')
  - [Powhatan](#F-VRDR-ValueSets-RaceCode-Powhatan 'VRDR.ValueSets.RaceCode.Powhatan')
  - [Prairie_Band_Of_Potawatomi_Indians](#F-VRDR-ValueSets-RaceCode-Prairie_Band_Of_Potawatomi_Indians 'VRDR.ValueSets.RaceCode.Prairie_Band_Of_Potawatomi_Indians')
  - [Prairie_Island_Sioux](#F-VRDR-ValueSets-RaceCode-Prairie_Island_Sioux 'VRDR.ValueSets.RaceCode.Prairie_Island_Sioux')
  - [Principal_Creek_Indian_Nation](#F-VRDR-ValueSets-RaceCode-Principal_Creek_Indian_Nation 'VRDR.ValueSets.RaceCode.Principal_Creek_Indian_Nation')
  - [Pueblo](#F-VRDR-ValueSets-RaceCode-Pueblo 'VRDR.ValueSets.RaceCode.Pueblo')
  - [Puerto_Rican](#F-VRDR-ValueSets-RaceCode-Puerto_Rican 'VRDR.ValueSets.RaceCode.Puerto_Rican')
  - [Puget_Sound_Salish](#F-VRDR-ValueSets-RaceCode-Puget_Sound_Salish 'VRDR.ValueSets.RaceCode.Puget_Sound_Salish')
  - [Puyaliup](#F-VRDR-ValueSets-RaceCode-Puyaliup 'VRDR.ValueSets.RaceCode.Puyaliup')
  - [Pyramid_Lake](#F-VRDR-ValueSets-RaceCode-Pyramid_Lake 'VRDR.ValueSets.RaceCode.Pyramid_Lake')
  - [Qagan_Toyagungin_Tribe_Of_Sand_Point_Village](#F-VRDR-ValueSets-RaceCode-Qagan_Toyagungin_Tribe_Of_Sand_Point_Village 'VRDR.ValueSets.RaceCode.Qagan_Toyagungin_Tribe_Of_Sand_Point_Village')
  - [Qawalangin_Tribe_Of_Unalaska](#F-VRDR-ValueSets-RaceCode-Qawalangin_Tribe_Of_Unalaska 'VRDR.ValueSets.RaceCode.Qawalangin_Tribe_Of_Unalaska')
  - [Quadroon](#F-VRDR-ValueSets-RaceCode-Quadroon 'VRDR.ValueSets.RaceCode.Quadroon')
  - [Quapaw](#F-VRDR-ValueSets-RaceCode-Quapaw 'VRDR.ValueSets.RaceCode.Quapaw')
  - [Quartz_Valley](#F-VRDR-ValueSets-RaceCode-Quartz_Valley 'VRDR.ValueSets.RaceCode.Quartz_Valley')
  - [Quechan](#F-VRDR-ValueSets-RaceCode-Quechan 'VRDR.ValueSets.RaceCode.Quechan')
  - [Quileute](#F-VRDR-ValueSets-RaceCode-Quileute 'VRDR.ValueSets.RaceCode.Quileute')
  - [Quinault](#F-VRDR-ValueSets-RaceCode-Quinault 'VRDR.ValueSets.RaceCode.Quinault')
  - [Rainbow](#F-VRDR-ValueSets-RaceCode-Rainbow 'VRDR.ValueSets.RaceCode.Rainbow')
  - [Ramah_Navajo](#F-VRDR-ValueSets-RaceCode-Ramah_Navajo 'VRDR.ValueSets.RaceCode.Ramah_Navajo')
  - [Ramapough_Mountain](#F-VRDR-ValueSets-RaceCode-Ramapough_Mountain 'VRDR.ValueSets.RaceCode.Ramapough_Mountain')
  - [Ramona_Band_Or_Village_Of_Cahuilla_Mission_Indians](#F-VRDR-ValueSets-RaceCode-Ramona_Band_Or_Village_Of_Cahuilla_Mission_Indians 'VRDR.ValueSets.RaceCode.Ramona_Band_Or_Village_Of_Cahuilla_Mission_Indians')
  - [Ramp](#F-VRDR-ValueSets-RaceCode-Ramp 'VRDR.ValueSets.RaceCode.Ramp')
  - [Rampart_Village](#F-VRDR-ValueSets-RaceCode-Rampart_Village 'VRDR.ValueSets.RaceCode.Rampart_Village')
  - [Rappahannock_Indian_Tribe](#F-VRDR-ValueSets-RaceCode-Rappahannock_Indian_Tribe 'VRDR.ValueSets.RaceCode.Rappahannock_Indian_Tribe')
  - [Red_Cliff_Band_Of_Lake_Superior_Chippewa](#F-VRDR-ValueSets-RaceCode-Red_Cliff_Band_Of_Lake_Superior_Chippewa 'VRDR.ValueSets.RaceCode.Red_Cliff_Band_Of_Lake_Superior_Chippewa')
  - [Red_Lake_Band_Of_Chippewa_Indians](#F-VRDR-ValueSets-RaceCode-Red_Lake_Band_Of_Chippewa_Indians 'VRDR.ValueSets.RaceCode.Red_Lake_Band_Of_Chippewa_Indians')
  - [Red_Wood](#F-VRDR-ValueSets-RaceCode-Red_Wood 'VRDR.ValueSets.RaceCode.Red_Wood')
  - [Redding_Rancheria](#F-VRDR-ValueSets-RaceCode-Redding_Rancheria 'VRDR.ValueSets.RaceCode.Redding_Rancheria')
  - [Redwood_Valley_Rancheria_Of_Pomo_Indians](#F-VRDR-ValueSets-RaceCode-Redwood_Valley_Rancheria_Of_Pomo_Indians 'VRDR.ValueSets.RaceCode.Redwood_Valley_Rancheria_Of_Pomo_Indians')
  - [Renosparks](#F-VRDR-ValueSets-RaceCode-Renosparks 'VRDR.ValueSets.RaceCode.Renosparks')
  - [Resighini_Rancheria](#F-VRDR-ValueSets-RaceCode-Resighini_Rancheria 'VRDR.ValueSets.RaceCode.Resighini_Rancheria')
  - [Rincon_Band_Of_Luiseno_Mission_Indians](#F-VRDR-ValueSets-RaceCode-Rincon_Band_Of_Luiseno_Mission_Indians 'VRDR.ValueSets.RaceCode.Rincon_Band_Of_Luiseno_Mission_Indians')
  - [Robinson_Rancheria_Of_Pomo_Indians](#F-VRDR-ValueSets-RaceCode-Robinson_Rancheria_Of_Pomo_Indians 'VRDR.ValueSets.RaceCode.Robinson_Rancheria_Of_Pomo_Indians')
  - [Rocky_Boys_Chippewa_Cree](#F-VRDR-ValueSets-RaceCode-Rocky_Boys_Chippewa_Cree 'VRDR.ValueSets.RaceCode.Rocky_Boys_Chippewa_Cree')
  - [Rosebud_Sioux](#F-VRDR-ValueSets-RaceCode-Rosebud_Sioux 'VRDR.ValueSets.RaceCode.Rosebud_Sioux')
  - [Round_Valley](#F-VRDR-ValueSets-RaceCode-Round_Valley 'VRDR.ValueSets.RaceCode.Round_Valley')
  - [Ruby_Valley](#F-VRDR-ValueSets-RaceCode-Ruby_Valley 'VRDR.ValueSets.RaceCode.Ruby_Valley')
  - [Rumsey_Indian_Rancheria_Of_Wintun_Indians](#F-VRDR-ValueSets-RaceCode-Rumsey_Indian_Rancheria_Of_Wintun_Indians 'VRDR.ValueSets.RaceCode.Rumsey_Indian_Rancheria_Of_Wintun_Indians')
  - [Russian](#F-VRDR-ValueSets-RaceCode-Russian 'VRDR.ValueSets.RaceCode.Russian')
  - [Sac_And_Fox](#F-VRDR-ValueSets-RaceCode-Sac_And_Fox 'VRDR.ValueSets.RaceCode.Sac_And_Fox')
  - [Sac_And_Fox_Nation_Of_Missouri_In_Kansas_And_Nebraska](#F-VRDR-ValueSets-RaceCode-Sac_And_Fox_Nation_Of_Missouri_In_Kansas_And_Nebraska 'VRDR.ValueSets.RaceCode.Sac_And_Fox_Nation_Of_Missouri_In_Kansas_And_Nebraska')
  - [Sac_And_Fox_Nation_Oklahoma](#F-VRDR-ValueSets-RaceCode-Sac_And_Fox_Nation_Oklahoma 'VRDR.ValueSets.RaceCode.Sac_And_Fox_Nation_Oklahoma')
  - [Sac_And_Fox_Tribe_Of_The_Mississippi_In_Iowa](#F-VRDR-ValueSets-RaceCode-Sac_And_Fox_Tribe_Of_The_Mississippi_In_Iowa 'VRDR.ValueSets.RaceCode.Sac_And_Fox_Tribe_Of_The_Mississippi_In_Iowa')
  - [Sac_River_Band_Of_The_Chickamauga_Cherokee](#F-VRDR-ValueSets-RaceCode-Sac_River_Band_Of_The_Chickamauga_Cherokee 'VRDR.ValueSets.RaceCode.Sac_River_Band_Of_The_Chickamauga_Cherokee')
  - [Saginaw_Chippewa](#F-VRDR-ValueSets-RaceCode-Saginaw_Chippewa 'VRDR.ValueSets.RaceCode.Saginaw_Chippewa')
  - [Saint_George](#F-VRDR-ValueSets-RaceCode-Saint_George 'VRDR.ValueSets.RaceCode.Saint_George')
  - [Saint_Paul](#F-VRDR-ValueSets-RaceCode-Saint_Paul 'VRDR.ValueSets.RaceCode.Saint_Paul')
  - [Saipanese](#F-VRDR-ValueSets-RaceCode-Saipanese 'VRDR.ValueSets.RaceCode.Saipanese')
  - [Salinan](#F-VRDR-ValueSets-RaceCode-Salinan 'VRDR.ValueSets.RaceCode.Salinan')
  - [Salish](#F-VRDR-ValueSets-RaceCode-Salish 'VRDR.ValueSets.RaceCode.Salish')
  - [Salish_And_Kootenai](#F-VRDR-ValueSets-RaceCode-Salish_And_Kootenai 'VRDR.ValueSets.RaceCode.Salish_And_Kootenai')
  - [Salt_River_Pimamaricopa](#F-VRDR-ValueSets-RaceCode-Salt_River_Pimamaricopa 'VRDR.ValueSets.RaceCode.Salt_River_Pimamaricopa')
  - [Salvadoran](#F-VRDR-ValueSets-RaceCode-Salvadoran 'VRDR.ValueSets.RaceCode.Salvadoran')
  - [Samish](#F-VRDR-ValueSets-RaceCode-Samish 'VRDR.ValueSets.RaceCode.Samish')
  - [Samoan](#F-VRDR-ValueSets-RaceCode-Samoan 'VRDR.ValueSets.RaceCode.Samoan')
  - [San_Carlos_Apache](#F-VRDR-ValueSets-RaceCode-San_Carlos_Apache 'VRDR.ValueSets.RaceCode.San_Carlos_Apache')
  - [San_Felipe](#F-VRDR-ValueSets-RaceCode-San_Felipe 'VRDR.ValueSets.RaceCode.San_Felipe')
  - [San_Ildefonso](#F-VRDR-ValueSets-RaceCode-San_Ildefonso 'VRDR.ValueSets.RaceCode.San_Ildefonso')
  - [San_Juan](#F-VRDR-ValueSets-RaceCode-San_Juan 'VRDR.ValueSets.RaceCode.San_Juan')
  - [San_Juan_De](#F-VRDR-ValueSets-RaceCode-San_Juan_De 'VRDR.ValueSets.RaceCode.San_Juan_De')
  - [San_Juan_Pueblo](#F-VRDR-ValueSets-RaceCode-San_Juan_Pueblo 'VRDR.ValueSets.RaceCode.San_Juan_Pueblo')
  - [San_Juan_Southern_Paiute](#F-VRDR-ValueSets-RaceCode-San_Juan_Southern_Paiute 'VRDR.ValueSets.RaceCode.San_Juan_Southern_Paiute')
  - [San_Luis_Rey_Mission_Indian](#F-VRDR-ValueSets-RaceCode-San_Luis_Rey_Mission_Indian 'VRDR.ValueSets.RaceCode.San_Luis_Rey_Mission_Indian')
  - [San_Manual_Band](#F-VRDR-ValueSets-RaceCode-San_Manual_Band 'VRDR.ValueSets.RaceCode.San_Manual_Band')
  - [San_Pasqual_Band_Of_Diegueno_Mission_Indians](#F-VRDR-ValueSets-RaceCode-San_Pasqual_Band_Of_Diegueno_Mission_Indians 'VRDR.ValueSets.RaceCode.San_Pasqual_Band_Of_Diegueno_Mission_Indians')
  - [San_Xavier](#F-VRDR-ValueSets-RaceCode-San_Xavier 'VRDR.ValueSets.RaceCode.San_Xavier')
  - [Sand_Hill_Band_Of_Delaware_Indians](#F-VRDR-ValueSets-RaceCode-Sand_Hill_Band_Of_Delaware_Indians 'VRDR.ValueSets.RaceCode.Sand_Hill_Band_Of_Delaware_Indians')
  - [Sand_Point](#F-VRDR-ValueSets-RaceCode-Sand_Point 'VRDR.ValueSets.RaceCode.Sand_Point')
  - [Sandia](#F-VRDR-ValueSets-RaceCode-Sandia 'VRDR.ValueSets.RaceCode.Sandia')
  - [Sans_Arc_Sioux](#F-VRDR-ValueSets-RaceCode-Sans_Arc_Sioux 'VRDR.ValueSets.RaceCode.Sans_Arc_Sioux')
  - [Santa_Ana](#F-VRDR-ValueSets-RaceCode-Santa_Ana 'VRDR.ValueSets.RaceCode.Santa_Ana')
  - [Santa_Clara](#F-VRDR-ValueSets-RaceCode-Santa_Clara 'VRDR.ValueSets.RaceCode.Santa_Clara')
  - [Santa_Rosa_Cahuilla](#F-VRDR-ValueSets-RaceCode-Santa_Rosa_Cahuilla 'VRDR.ValueSets.RaceCode.Santa_Rosa_Cahuilla')
  - [Santa_Rosa_Indian_Community](#F-VRDR-ValueSets-RaceCode-Santa_Rosa_Indian_Community 'VRDR.ValueSets.RaceCode.Santa_Rosa_Indian_Community')
  - [Santa_Ynez](#F-VRDR-ValueSets-RaceCode-Santa_Ynez 'VRDR.ValueSets.RaceCode.Santa_Ynez')
  - [Santa_Ysabel_Band_Of_Diegueno_Mission_Indians](#F-VRDR-ValueSets-RaceCode-Santa_Ysabel_Band_Of_Diegueno_Mission_Indians 'VRDR.ValueSets.RaceCode.Santa_Ysabel_Band_Of_Diegueno_Mission_Indians')
  - [Santee_Sioux_Of_Nebraska](#F-VRDR-ValueSets-RaceCode-Santee_Sioux_Of_Nebraska 'VRDR.ValueSets.RaceCode.Santee_Sioux_Of_Nebraska')
  - [Santo_Domingo](#F-VRDR-ValueSets-RaceCode-Santo_Domingo 'VRDR.ValueSets.RaceCode.Santo_Domingo')
  - [Sauksuiattle](#F-VRDR-ValueSets-RaceCode-Sauksuiattle 'VRDR.ValueSets.RaceCode.Sauksuiattle')
  - [Sault_Ste_Marie_Chippewa](#F-VRDR-ValueSets-RaceCode-Sault_Ste_Marie_Chippewa 'VRDR.ValueSets.RaceCode.Sault_Ste_Marie_Chippewa')
  - [Schaghticoke](#F-VRDR-ValueSets-RaceCode-Schaghticoke 'VRDR.ValueSets.RaceCode.Schaghticoke')
  - [Scottish](#F-VRDR-ValueSets-RaceCode-Scottish 'VRDR.ValueSets.RaceCode.Scottish')
  - [Scotts_Valley_Band](#F-VRDR-ValueSets-RaceCode-Scotts_Valley_Band 'VRDR.ValueSets.RaceCode.Scotts_Valley_Band')
  - [Seaconeke_Wampanoag](#F-VRDR-ValueSets-RaceCode-Seaconeke_Wampanoag 'VRDR.ValueSets.RaceCode.Seaconeke_Wampanoag')
  - [Sealaska](#F-VRDR-ValueSets-RaceCode-Sealaska 'VRDR.ValueSets.RaceCode.Sealaska')
  - [Sealaska_Corporation](#F-VRDR-ValueSets-RaceCode-Sealaska_Corporation 'VRDR.ValueSets.RaceCode.Sealaska_Corporation')
  - [Seldovia_Village_Tribe](#F-VRDR-ValueSets-RaceCode-Seldovia_Village_Tribe 'VRDR.ValueSets.RaceCode.Seldovia_Village_Tribe')
  - [Sells](#F-VRDR-ValueSets-RaceCode-Sells 'VRDR.ValueSets.RaceCode.Sells')
  - [Seminole](#F-VRDR-ValueSets-RaceCode-Seminole 'VRDR.ValueSets.RaceCode.Seminole')
  - [Seneca](#F-VRDR-ValueSets-RaceCode-Seneca 'VRDR.ValueSets.RaceCode.Seneca')
  - [Seneca_Nation](#F-VRDR-ValueSets-RaceCode-Seneca_Nation 'VRDR.ValueSets.RaceCode.Seneca_Nation')
  - [Senecacayuga](#F-VRDR-ValueSets-RaceCode-Senecacayuga 'VRDR.ValueSets.RaceCode.Senecacayuga')
  - [Serrano](#F-VRDR-ValueSets-RaceCode-Serrano 'VRDR.ValueSets.RaceCode.Serrano')
  - [Setauket](#F-VRDR-ValueSets-RaceCode-Setauket 'VRDR.ValueSets.RaceCode.Setauket')
  - [Shageluk_Native_Village](#F-VRDR-ValueSets-RaceCode-Shageluk_Native_Village 'VRDR.ValueSets.RaceCode.Shageluk_Native_Village')
  - [Shakopee_Mdewakanton_Sioux_Community](#F-VRDR-ValueSets-RaceCode-Shakopee_Mdewakanton_Sioux_Community 'VRDR.ValueSets.RaceCode.Shakopee_Mdewakanton_Sioux_Community')
  - [Shasta](#F-VRDR-ValueSets-RaceCode-Shasta 'VRDR.ValueSets.RaceCode.Shasta')
  - [Shawnee](#F-VRDR-ValueSets-RaceCode-Shawnee 'VRDR.ValueSets.RaceCode.Shawnee')
  - [Sheep_Ranch_Rancheria_Of_Mewuk_Indians](#F-VRDR-ValueSets-RaceCode-Sheep_Ranch_Rancheria_Of_Mewuk_Indians 'VRDR.ValueSets.RaceCode.Sheep_Ranch_Rancheria_Of_Mewuk_Indians')
  - [Sherwood_Valley_Rancheria_Of_Pomo_Indians_Of_California](#F-VRDR-ValueSets-RaceCode-Sherwood_Valley_Rancheria_Of_Pomo_Indians_Of_California 'VRDR.ValueSets.RaceCode.Sherwood_Valley_Rancheria_Of_Pomo_Indians_Of_California')
  - [Shingle_Springs_Band_Of_Miwok_Indians](#F-VRDR-ValueSets-RaceCode-Shingle_Springs_Band_Of_Miwok_Indians 'VRDR.ValueSets.RaceCode.Shingle_Springs_Band_Of_Miwok_Indians')
  - [Shinnecock](#F-VRDR-ValueSets-RaceCode-Shinnecock 'VRDR.ValueSets.RaceCode.Shinnecock')
  - [Shoalwater_Bay](#F-VRDR-ValueSets-RaceCode-Shoalwater_Bay 'VRDR.ValueSets.RaceCode.Shoalwater_Bay')
  - [Shoshone](#F-VRDR-ValueSets-RaceCode-Shoshone 'VRDR.ValueSets.RaceCode.Shoshone')
  - [Shoshone_Paiute](#F-VRDR-ValueSets-RaceCode-Shoshone_Paiute 'VRDR.ValueSets.RaceCode.Shoshone_Paiute')
  - [Shoshonebannock_Tribes_Of_The_Fort_Hall_Reservation](#F-VRDR-ValueSets-RaceCode-Shoshonebannock_Tribes_Of_The_Fort_Hall_Reservation 'VRDR.ValueSets.RaceCode.Shoshonebannock_Tribes_Of_The_Fort_Hall_Reservation')
  - [Siberian_Eskimo](#F-VRDR-ValueSets-RaceCode-Siberian_Eskimo 'VRDR.ValueSets.RaceCode.Siberian_Eskimo')
  - [Siberian_Yupik](#F-VRDR-ValueSets-RaceCode-Siberian_Yupik 'VRDR.ValueSets.RaceCode.Siberian_Yupik')
  - [Singaporean](#F-VRDR-ValueSets-RaceCode-Singaporean 'VRDR.ValueSets.RaceCode.Singaporean')
  - [Sioux](#F-VRDR-ValueSets-RaceCode-Sioux 'VRDR.ValueSets.RaceCode.Sioux')
  - [Sisseton_Sioux](#F-VRDR-ValueSets-RaceCode-Sisseton_Sioux 'VRDR.ValueSets.RaceCode.Sisseton_Sioux')
  - [Sissetonwahpeton](#F-VRDR-ValueSets-RaceCode-Sissetonwahpeton 'VRDR.ValueSets.RaceCode.Sissetonwahpeton')
  - [Sitka_Tribe_Of_Alaska](#F-VRDR-ValueSets-RaceCode-Sitka_Tribe_Of_Alaska 'VRDR.ValueSets.RaceCode.Sitka_Tribe_Of_Alaska')
  - [Siuslaw](#F-VRDR-ValueSets-RaceCode-Siuslaw 'VRDR.ValueSets.RaceCode.Siuslaw')
  - [Skagway_Village](#F-VRDR-ValueSets-RaceCode-Skagway_Village 'VRDR.ValueSets.RaceCode.Skagway_Village')
  - [Skokomish](#F-VRDR-ValueSets-RaceCode-Skokomish 'VRDR.ValueSets.RaceCode.Skokomish')
  - [Skull_Valley_Band_Of_Goshute_Indians](#F-VRDR-ValueSets-RaceCode-Skull_Valley_Band_Of_Goshute_Indians 'VRDR.ValueSets.RaceCode.Skull_Valley_Band_Of_Goshute_Indians')
  - [Skykomish](#F-VRDR-ValueSets-RaceCode-Skykomish 'VRDR.ValueSets.RaceCode.Skykomish')
  - [Slana](#F-VRDR-ValueSets-RaceCode-Slana 'VRDR.ValueSets.RaceCode.Slana')
  - [Smith_River_Rancheria](#F-VRDR-ValueSets-RaceCode-Smith_River_Rancheria 'VRDR.ValueSets.RaceCode.Smith_River_Rancheria')
  - [Snohomish](#F-VRDR-ValueSets-RaceCode-Snohomish 'VRDR.ValueSets.RaceCode.Snohomish')
  - [Snoqualmie](#F-VRDR-ValueSets-RaceCode-Snoqualmie 'VRDR.ValueSets.RaceCode.Snoqualmie')
  - [Soboba](#F-VRDR-ValueSets-RaceCode-Soboba 'VRDR.ValueSets.RaceCode.Soboba')
  - [Sokoagon_Chippewa](#F-VRDR-ValueSets-RaceCode-Sokoagon_Chippewa 'VRDR.ValueSets.RaceCode.Sokoagon_Chippewa')
  - [Solomon_Islander](#F-VRDR-ValueSets-RaceCode-Solomon_Islander 'VRDR.ValueSets.RaceCode.Solomon_Islander')
  - [Somalian](#F-VRDR-ValueSets-RaceCode-Somalian 'VRDR.ValueSets.RaceCode.Somalian')
  - [South_African](#F-VRDR-ValueSets-RaceCode-South_African 'VRDR.ValueSets.RaceCode.South_African')
  - [South_American](#F-VRDR-ValueSets-RaceCode-South_American 'VRDR.ValueSets.RaceCode.South_American')
  - [South_American_Indian](#F-VRDR-ValueSets-RaceCode-South_American_Indian 'VRDR.ValueSets.RaceCode.South_American_Indian')
  - [South_Fork](#F-VRDR-ValueSets-RaceCode-South_Fork 'VRDR.ValueSets.RaceCode.South_Fork')
  - [South_Naknek_Village](#F-VRDR-ValueSets-RaceCode-South_Naknek_Village 'VRDR.ValueSets.RaceCode.South_Naknek_Village')
  - [Southeast_Alaska](#F-VRDR-ValueSets-RaceCode-Southeast_Alaska 'VRDR.ValueSets.RaceCode.Southeast_Alaska')
  - [Southeastern_Cherokee_Council](#F-VRDR-ValueSets-RaceCode-Southeastern_Cherokee_Council 'VRDR.ValueSets.RaceCode.Southeastern_Cherokee_Council')
  - [Southeastern_Indians](#F-VRDR-ValueSets-RaceCode-Southeastern_Indians 'VRDR.ValueSets.RaceCode.Southeastern_Indians')
  - [Southern_Arapahoe](#F-VRDR-ValueSets-RaceCode-Southern_Arapahoe 'VRDR.ValueSets.RaceCode.Southern_Arapahoe')
  - [Southern_Cheyenne](#F-VRDR-ValueSets-RaceCode-Southern_Cheyenne 'VRDR.ValueSets.RaceCode.Southern_Cheyenne')
  - [Southern_Paiute](#F-VRDR-ValueSets-RaceCode-Southern_Paiute 'VRDR.ValueSets.RaceCode.Southern_Paiute')
  - [Southern_Ute](#F-VRDR-ValueSets-RaceCode-Southern_Ute 'VRDR.ValueSets.RaceCode.Southern_Ute')
  - [Spaniard](#F-VRDR-ValueSets-RaceCode-Spaniard 'VRDR.ValueSets.RaceCode.Spaniard')
  - [Spanish](#F-VRDR-ValueSets-RaceCode-Spanish 'VRDR.ValueSets.RaceCode.Spanish')
  - [Spanish_American](#F-VRDR-ValueSets-RaceCode-Spanish_American 'VRDR.ValueSets.RaceCode.Spanish_American')
  - [Spanish_American_Indian](#F-VRDR-ValueSets-RaceCode-Spanish_American_Indian 'VRDR.ValueSets.RaceCode.Spanish_American_Indian')
  - [Spirit_Lake_Sioux](#F-VRDR-ValueSets-RaceCode-Spirit_Lake_Sioux 'VRDR.ValueSets.RaceCode.Spirit_Lake_Sioux')
  - [Spokane](#F-VRDR-ValueSets-RaceCode-Spokane 'VRDR.ValueSets.RaceCode.Spokane')
  - [Squaxin_Island](#F-VRDR-ValueSets-RaceCode-Squaxin_Island 'VRDR.ValueSets.RaceCode.Squaxin_Island')
  - [Sri_Lankan](#F-VRDR-ValueSets-RaceCode-Sri_Lankan 'VRDR.ValueSets.RaceCode.Sri_Lankan')
  - [St_Croix_Chippewa](#F-VRDR-ValueSets-RaceCode-St_Croix_Chippewa 'VRDR.ValueSets.RaceCode.St_Croix_Chippewa')
  - [Standing_Rock_Sioux](#F-VRDR-ValueSets-RaceCode-Standing_Rock_Sioux 'VRDR.ValueSets.RaceCode.Standing_Rock_Sioux')
  - [Star_Clan_Of_Muskogee_Creeks](#F-VRDR-ValueSets-RaceCode-Star_Clan_Of_Muskogee_Creeks 'VRDR.ValueSets.RaceCode.Star_Clan_Of_Muskogee_Creeks')
  - [Stebbins_Community_Association](#F-VRDR-ValueSets-RaceCode-Stebbins_Community_Association 'VRDR.ValueSets.RaceCode.Stebbins_Community_Association')
  - [Steilacoom](#F-VRDR-ValueSets-RaceCode-Steilacoom 'VRDR.ValueSets.RaceCode.Steilacoom')
  - [Stewart_Community](#F-VRDR-ValueSets-RaceCode-Stewart_Community 'VRDR.ValueSets.RaceCode.Stewart_Community')
  - [Stillaguamish](#F-VRDR-ValueSets-RaceCode-Stillaguamish 'VRDR.ValueSets.RaceCode.Stillaguamish')
  - [Stockbridgemunsee_Community_Of_Mohican_Indians_Of_Wisconsin](#F-VRDR-ValueSets-RaceCode-Stockbridgemunsee_Community_Of_Mohican_Indians_Of_Wisconsin 'VRDR.ValueSets.RaceCode.Stockbridgemunsee_Community_Of_Mohican_Indians_Of_Wisconsin')
  - [Stonyford](#F-VRDR-ValueSets-RaceCode-Stonyford 'VRDR.ValueSets.RaceCode.Stonyford')
  - [Sudamericano](#F-VRDR-ValueSets-RaceCode-Sudamericano 'VRDR.ValueSets.RaceCode.Sudamericano')
  - [Sudanese](#F-VRDR-ValueSets-RaceCode-Sudanese 'VRDR.ValueSets.RaceCode.Sudanese')
  - [Sugpiaq](#F-VRDR-ValueSets-RaceCode-Sugpiaq 'VRDR.ValueSets.RaceCode.Sugpiaq')
  - [Summit_Lake](#F-VRDR-ValueSets-RaceCode-Summit_Lake 'VRDR.ValueSets.RaceCode.Summit_Lake')
  - [Suqpigaq](#F-VRDR-ValueSets-RaceCode-Suqpigaq 'VRDR.ValueSets.RaceCode.Suqpigaq')
  - [Suquamish](#F-VRDR-ValueSets-RaceCode-Suquamish 'VRDR.ValueSets.RaceCode.Suquamish')
  - [Surinam](#F-VRDR-ValueSets-RaceCode-Surinam 'VRDR.ValueSets.RaceCode.Surinam')
  - [Susanville](#F-VRDR-ValueSets-RaceCode-Susanville 'VRDR.ValueSets.RaceCode.Susanville')
  - [Susquehanock](#F-VRDR-ValueSets-RaceCode-Susquehanock 'VRDR.ValueSets.RaceCode.Susquehanock')
  - [Swan_Creek_Black_River_Confederate_Tribe](#F-VRDR-ValueSets-RaceCode-Swan_Creek_Black_River_Confederate_Tribe 'VRDR.ValueSets.RaceCode.Swan_Creek_Black_River_Confederate_Tribe')
  - [Swinomish](#F-VRDR-ValueSets-RaceCode-Swinomish 'VRDR.ValueSets.RaceCode.Swinomish')
  - [Sycuan_Band_Of_Diegueno_Mission_Indians](#F-VRDR-ValueSets-RaceCode-Sycuan_Band_Of_Diegueno_Mission_Indians 'VRDR.ValueSets.RaceCode.Sycuan_Band_Of_Diegueno_Mission_Indians')
  - [Syrian](#F-VRDR-ValueSets-RaceCode-Syrian 'VRDR.ValueSets.RaceCode.Syrian')
  - [Table_Bluff](#F-VRDR-ValueSets-RaceCode-Table_Bluff 'VRDR.ValueSets.RaceCode.Table_Bluff')
  - [Table_Mountain_Rancheria](#F-VRDR-ValueSets-RaceCode-Table_Mountain_Rancheria 'VRDR.ValueSets.RaceCode.Table_Mountain_Rancheria')
  - [Tachi](#F-VRDR-ValueSets-RaceCode-Tachi 'VRDR.ValueSets.RaceCode.Tachi')
  - [Tahitian](#F-VRDR-ValueSets-RaceCode-Tahitian 'VRDR.ValueSets.RaceCode.Tahitian')
  - [Taiwanese](#F-VRDR-ValueSets-RaceCode-Taiwanese 'VRDR.ValueSets.RaceCode.Taiwanese')
  - [Takelma](#F-VRDR-ValueSets-RaceCode-Takelma 'VRDR.ValueSets.RaceCode.Takelma')
  - [Takotna_Village](#F-VRDR-ValueSets-RaceCode-Takotna_Village 'VRDR.ValueSets.RaceCode.Takotna_Village')
  - [Talakamish](#F-VRDR-ValueSets-RaceCode-Talakamish 'VRDR.ValueSets.RaceCode.Talakamish')
  - [Tampa_Seminole](#F-VRDR-ValueSets-RaceCode-Tampa_Seminole 'VRDR.ValueSets.RaceCode.Tampa_Seminole')
  - [Tanaina](#F-VRDR-ValueSets-RaceCode-Tanaina 'VRDR.ValueSets.RaceCode.Tanaina')
  - [Tanana_Chiefs](#F-VRDR-ValueSets-RaceCode-Tanana_Chiefs 'VRDR.ValueSets.RaceCode.Tanana_Chiefs')
  - [Taos](#F-VRDR-ValueSets-RaceCode-Taos 'VRDR.ValueSets.RaceCode.Taos')
  - [Tawakonie](#F-VRDR-ValueSets-RaceCode-Tawakonie 'VRDR.ValueSets.RaceCode.Tawakonie')
  - [Tejano](#F-VRDR-ValueSets-RaceCode-Tejano 'VRDR.ValueSets.RaceCode.Tejano')
  - [Telida_Village](#F-VRDR-ValueSets-RaceCode-Telida_Village 'VRDR.ValueSets.RaceCode.Telida_Village')
  - [Temecula](#F-VRDR-ValueSets-RaceCode-Temecula 'VRDR.ValueSets.RaceCode.Temecula')
  - [Temoak_Tribes_Of_Western_Shoshone_Indians](#F-VRDR-ValueSets-RaceCode-Temoak_Tribes_Of_Western_Shoshone_Indians 'VRDR.ValueSets.RaceCode.Temoak_Tribes_Of_Western_Shoshone_Indians')
  - [Tenakee_Springs](#F-VRDR-ValueSets-RaceCode-Tenakee_Springs 'VRDR.ValueSets.RaceCode.Tenakee_Springs')
  - [Tenino](#F-VRDR-ValueSets-RaceCode-Tenino 'VRDR.ValueSets.RaceCode.Tenino')
  - [Tesuque](#F-VRDR-ValueSets-RaceCode-Tesuque 'VRDR.ValueSets.RaceCode.Tesuque')
  - [Teton_Sioux](#F-VRDR-ValueSets-RaceCode-Teton_Sioux 'VRDR.ValueSets.RaceCode.Teton_Sioux')
  - [Tewa](#F-VRDR-ValueSets-RaceCode-Tewa 'VRDR.ValueSets.RaceCode.Tewa')
  - [Texas_Kickapoo](#F-VRDR-ValueSets-RaceCode-Texas_Kickapoo 'VRDR.ValueSets.RaceCode.Texas_Kickapoo')
  - [Thai](#F-VRDR-ValueSets-RaceCode-Thai 'VRDR.ValueSets.RaceCode.Thai')
  - [Thiopthlocco_Tribal_Town](#F-VRDR-ValueSets-RaceCode-Thiopthlocco_Tribal_Town 'VRDR.ValueSets.RaceCode.Thiopthlocco_Tribal_Town')
  - [Three_Affiliated_Tribes_Of_North_Dakota](#F-VRDR-ValueSets-RaceCode-Three_Affiliated_Tribes_Of_North_Dakota 'VRDR.ValueSets.RaceCode.Three_Affiliated_Tribes_Of_North_Dakota')
  - [Tia](#F-VRDR-ValueSets-RaceCode-Tia 'VRDR.ValueSets.RaceCode.Tia')
  - [Tibetan](#F-VRDR-ValueSets-RaceCode-Tibetan 'VRDR.ValueSets.RaceCode.Tibetan')
  - [Tigua](#F-VRDR-ValueSets-RaceCode-Tigua 'VRDR.ValueSets.RaceCode.Tigua')
  - [Tillamook](#F-VRDR-ValueSets-RaceCode-Tillamook 'VRDR.ValueSets.RaceCode.Tillamook')
  - [Tlingit](#F-VRDR-ValueSets-RaceCode-Tlingit 'VRDR.ValueSets.RaceCode.Tlingit')
  - [Tlingit_Haida](#F-VRDR-ValueSets-RaceCode-Tlingit_Haida 'VRDR.ValueSets.RaceCode.Tlingit_Haida')
  - [Tobago](#F-VRDR-ValueSets-RaceCode-Tobago 'VRDR.ValueSets.RaceCode.Tobago')
  - [Togolese](#F-VRDR-ValueSets-RaceCode-Togolese 'VRDR.ValueSets.RaceCode.Togolese')
  - [Tohajiileehee_Navajo](#F-VRDR-ValueSets-RaceCode-Tohajiileehee_Navajo 'VRDR.ValueSets.RaceCode.Tohajiileehee_Navajo')
  - [Tohono_Oodham](#F-VRDR-ValueSets-RaceCode-Tohono_Oodham 'VRDR.ValueSets.RaceCode.Tohono_Oodham')
  - [Tok](#F-VRDR-ValueSets-RaceCode-Tok 'VRDR.ValueSets.RaceCode.Tok')
  - [Tokelauan](#F-VRDR-ValueSets-RaceCode-Tokelauan 'VRDR.ValueSets.RaceCode.Tokelauan')
  - [Tolowa](#F-VRDR-ValueSets-RaceCode-Tolowa 'VRDR.ValueSets.RaceCode.Tolowa')
  - [Tonawanda_Band_Of_Seneca](#F-VRDR-ValueSets-RaceCode-Tonawanda_Band_Of_Seneca 'VRDR.ValueSets.RaceCode.Tonawanda_Band_Of_Seneca')
  - [Tongan](#F-VRDR-ValueSets-RaceCode-Tongan 'VRDR.ValueSets.RaceCode.Tongan')
  - [Tonkawa](#F-VRDR-ValueSets-RaceCode-Tonkawa 'VRDR.ValueSets.RaceCode.Tonkawa')
  - [Torres_Martinez_Band_Of_Cahuilla_Mission_Indians](#F-VRDR-ValueSets-RaceCode-Torres_Martinez_Band_Of_Cahuilla_Mission_Indians 'VRDR.ValueSets.RaceCode.Torres_Martinez_Band_Of_Cahuilla_Mission_Indians')
  - [Traditional_Village_Oftogiak](#F-VRDR-ValueSets-RaceCode-Traditional_Village_Oftogiak 'VRDR.ValueSets.RaceCode.Traditional_Village_Oftogiak')
  - [Tribal_Response_Nec](#F-VRDR-ValueSets-RaceCode-Tribal_Response_Nec 'VRDR.ValueSets.RaceCode.Tribal_Response_Nec')
  - [Trigueno](#F-VRDR-ValueSets-RaceCode-Trigueno 'VRDR.ValueSets.RaceCode.Trigueno')
  - [Trinidad](#F-VRDR-ValueSets-RaceCode-Trinidad 'VRDR.ValueSets.RaceCode.Trinidad')
  - [Trinity](#F-VRDR-ValueSets-RaceCode-Trinity 'VRDR.ValueSets.RaceCode.Trinity')
  - [Tsimshian](#F-VRDR-ValueSets-RaceCode-Tsimshian 'VRDR.ValueSets.RaceCode.Tsimshian')
  - [Tuckabachee](#F-VRDR-ValueSets-RaceCode-Tuckabachee 'VRDR.ValueSets.RaceCode.Tuckabachee')
  - [Tulalip](#F-VRDR-ValueSets-RaceCode-Tulalip 'VRDR.ValueSets.RaceCode.Tulalip')
  - [Tule_River](#F-VRDR-ValueSets-RaceCode-Tule_River 'VRDR.ValueSets.RaceCode.Tule_River')
  - [Tuluksak_Native_Community](#F-VRDR-ValueSets-RaceCode-Tuluksak_Native_Community 'VRDR.ValueSets.RaceCode.Tuluksak_Native_Community')
  - [Tunica_Biloxi](#F-VRDR-ValueSets-RaceCode-Tunica_Biloxi 'VRDR.ValueSets.RaceCode.Tunica_Biloxi')
  - [Tuolumne_Band_Of_Mewuk_Indians_Of_California](#F-VRDR-ValueSets-RaceCode-Tuolumne_Band_Of_Mewuk_Indians_Of_California 'VRDR.ValueSets.RaceCode.Tuolumne_Band_Of_Mewuk_Indians_Of_California')
  - [Turk](#F-VRDR-ValueSets-RaceCode-Turk 'VRDR.ValueSets.RaceCode.Turk')
  - [Turtle_Mountain_Band](#F-VRDR-ValueSets-RaceCode-Turtle_Mountain_Band 'VRDR.ValueSets.RaceCode.Turtle_Mountain_Band')
  - [Tuscarora](#F-VRDR-ValueSets-RaceCode-Tuscarora 'VRDR.ValueSets.RaceCode.Tuscarora')
  - [Tuscola](#F-VRDR-ValueSets-RaceCode-Tuscola 'VRDR.ValueSets.RaceCode.Tuscola')
  - [Twentynine_Palms_Band_Of_Luiseno_Mission_Indians](#F-VRDR-ValueSets-RaceCode-Twentynine_Palms_Band_Of_Luiseno_Mission_Indians 'VRDR.ValueSets.RaceCode.Twentynine_Palms_Band_Of_Luiseno_Mission_Indians')
  - [Twin_Hills_Village](#F-VRDR-ValueSets-RaceCode-Twin_Hills_Village 'VRDR.ValueSets.RaceCode.Twin_Hills_Village')
  - [Two_Kettle_Sioux](#F-VRDR-ValueSets-RaceCode-Two_Kettle_Sioux 'VRDR.ValueSets.RaceCode.Two_Kettle_Sioux')
  - [Tygh](#F-VRDR-ValueSets-RaceCode-Tygh 'VRDR.ValueSets.RaceCode.Tygh')
  - [Ugashik_Village](#F-VRDR-ValueSets-RaceCode-Ugashik_Village 'VRDR.ValueSets.RaceCode.Ugashik_Village')
  - [Uintah_Ute](#F-VRDR-ValueSets-RaceCode-Uintah_Ute 'VRDR.ValueSets.RaceCode.Uintah_Ute')
  - [Ukranian](#F-VRDR-ValueSets-RaceCode-Ukranian 'VRDR.ValueSets.RaceCode.Ukranian')
  - [Umatilla](#F-VRDR-ValueSets-RaceCode-Umatilla 'VRDR.ValueSets.RaceCode.Umatilla')
  - [Umkumiute_Native_Village](#F-VRDR-ValueSets-RaceCode-Umkumiute_Native_Village 'VRDR.ValueSets.RaceCode.Umkumiute_Native_Village')
  - [Umpqua](#F-VRDR-ValueSets-RaceCode-Umpqua 'VRDR.ValueSets.RaceCode.Umpqua')
  - [Unalaska](#F-VRDR-ValueSets-RaceCode-Unalaska 'VRDR.ValueSets.RaceCode.Unalaska')
  - [Unangan](#F-VRDR-ValueSets-RaceCode-Unangan 'VRDR.ValueSets.RaceCode.Unangan')
  - [Unangan_Aleut](#F-VRDR-ValueSets-RaceCode-Unangan_Aleut 'VRDR.ValueSets.RaceCode.Unangan_Aleut')
  - [Uncodable](#F-VRDR-ValueSets-RaceCode-Uncodable 'VRDR.ValueSets.RaceCode.Uncodable')
  - [United_Arab_Emirates](#F-VRDR-ValueSets-RaceCode-United_Arab_Emirates 'VRDR.ValueSets.RaceCode.United_Arab_Emirates')
  - [United_Houma_Nation](#F-VRDR-ValueSets-RaceCode-United_Houma_Nation 'VRDR.ValueSets.RaceCode.United_Houma_Nation')
  - [United_Keetoowah_Band_Of_Cherokee](#F-VRDR-ValueSets-RaceCode-United_Keetoowah_Band_Of_Cherokee 'VRDR.ValueSets.RaceCode.United_Keetoowah_Band_Of_Cherokee')
  - [Unknown](#F-VRDR-ValueSets-RaceCode-Unknown 'VRDR.ValueSets.RaceCode.Unknown')
  - [Upper_Chinook](#F-VRDR-ValueSets-RaceCode-Upper_Chinook 'VRDR.ValueSets.RaceCode.Upper_Chinook')
  - [Upper_Lake_Band_Of_Pomo_Indians_Of_Upper_Lake_Rancheria](#F-VRDR-ValueSets-RaceCode-Upper_Lake_Band_Of_Pomo_Indians_Of_Upper_Lake_Rancheria 'VRDR.ValueSets.RaceCode.Upper_Lake_Band_Of_Pomo_Indians_Of_Upper_Lake_Rancheria')
  - [Upper_Mattaponi_Tribe](#F-VRDR-ValueSets-RaceCode-Upper_Mattaponi_Tribe 'VRDR.ValueSets.RaceCode.Upper_Mattaponi_Tribe')
  - [Upper_Sioux](#F-VRDR-ValueSets-RaceCode-Upper_Sioux 'VRDR.ValueSets.RaceCode.Upper_Sioux')
  - [Upper_Skagit](#F-VRDR-ValueSets-RaceCode-Upper_Skagit 'VRDR.ValueSets.RaceCode.Upper_Skagit')
  - [Uruguayan](#F-VRDR-ValueSets-RaceCode-Uruguayan 'VRDR.ValueSets.RaceCode.Uruguayan')
  - [Ute](#F-VRDR-ValueSets-RaceCode-Ute 'VRDR.ValueSets.RaceCode.Ute')
  - [Ute_Mountain](#F-VRDR-ValueSets-RaceCode-Ute_Mountain 'VRDR.ValueSets.RaceCode.Ute_Mountain')
  - [Utu_Utu_Gwaitu_Paiute](#F-VRDR-ValueSets-RaceCode-Utu_Utu_Gwaitu_Paiute 'VRDR.ValueSets.RaceCode.Utu_Utu_Gwaitu_Paiute')
  - [Venezuelan](#F-VRDR-ValueSets-RaceCode-Venezuelan 'VRDR.ValueSets.RaceCode.Venezuelan')
  - [Viejas_Group_Of_Capitan_Grande_Band](#F-VRDR-ValueSets-RaceCode-Viejas_Group_Of_Capitan_Grande_Band 'VRDR.ValueSets.RaceCode.Viejas_Group_Of_Capitan_Grande_Band')
  - [Vietnamese](#F-VRDR-ValueSets-RaceCode-Vietnamese 'VRDR.ValueSets.RaceCode.Vietnamese')
  - [Village_Of_Afognak](#F-VRDR-ValueSets-RaceCode-Village_Of_Afognak 'VRDR.ValueSets.RaceCode.Village_Of_Afognak')
  - [Village_Of_Alakanuk](#F-VRDR-ValueSets-RaceCode-Village_Of_Alakanuk 'VRDR.ValueSets.RaceCode.Village_Of_Alakanuk')
  - [Village_Of_Anaktuvuk_Pass](#F-VRDR-ValueSets-RaceCode-Village_Of_Anaktuvuk_Pass 'VRDR.ValueSets.RaceCode.Village_Of_Anaktuvuk_Pass')
  - [Village_Of_Aniak](#F-VRDR-ValueSets-RaceCode-Village_Of_Aniak 'VRDR.ValueSets.RaceCode.Village_Of_Aniak')
  - [Village_Of_Atmautluak](#F-VRDR-ValueSets-RaceCode-Village_Of_Atmautluak 'VRDR.ValueSets.RaceCode.Village_Of_Atmautluak')
  - [Village_Of_Bill_Moores_Slough_Bay](#F-VRDR-ValueSets-RaceCode-Village_Of_Bill_Moores_Slough_Bay 'VRDR.ValueSets.RaceCode.Village_Of_Bill_Moores_Slough_Bay')
  - [Village_Of_Chefomak](#F-VRDR-ValueSets-RaceCode-Village_Of_Chefomak 'VRDR.ValueSets.RaceCode.Village_Of_Chefomak')
  - [Village_Of_Clarks_Point](#F-VRDR-ValueSets-RaceCode-Village_Of_Clarks_Point 'VRDR.ValueSets.RaceCode.Village_Of_Clarks_Point')
  - [Village_Of_Crooked_Creek](#F-VRDR-ValueSets-RaceCode-Village_Of_Crooked_Creek 'VRDR.ValueSets.RaceCode.Village_Of_Crooked_Creek')
  - [Village_Of_Dot_Lake](#F-VRDR-ValueSets-RaceCode-Village_Of_Dot_Lake 'VRDR.ValueSets.RaceCode.Village_Of_Dot_Lake')
  - [Village_Of_Iliamna](#F-VRDR-ValueSets-RaceCode-Village_Of_Iliamna 'VRDR.ValueSets.RaceCode.Village_Of_Iliamna')
  - [Village_Of_Kalskag](#F-VRDR-ValueSets-RaceCode-Village_Of_Kalskag 'VRDR.ValueSets.RaceCode.Village_Of_Kalskag')
  - [Village_Of_Kotlik](#F-VRDR-ValueSets-RaceCode-Village_Of_Kotlik 'VRDR.ValueSets.RaceCode.Village_Of_Kotlik')
  - [Village_Of_Lower_Kalskag](#F-VRDR-ValueSets-RaceCode-Village_Of_Lower_Kalskag 'VRDR.ValueSets.RaceCode.Village_Of_Lower_Kalskag')
  - [Village_Of_Ohogamiut](#F-VRDR-ValueSets-RaceCode-Village_Of_Ohogamiut 'VRDR.ValueSets.RaceCode.Village_Of_Ohogamiut')
  - [Village_Of_Old_Harbor](#F-VRDR-ValueSets-RaceCode-Village_Of_Old_Harbor 'VRDR.ValueSets.RaceCode.Village_Of_Old_Harbor')
  - [Village_Of_Red_Devil](#F-VRDR-ValueSets-RaceCode-Village_Of_Red_Devil 'VRDR.ValueSets.RaceCode.Village_Of_Red_Devil')
  - [Village_Of_Salamatoff](#F-VRDR-ValueSets-RaceCode-Village_Of_Salamatoff 'VRDR.ValueSets.RaceCode.Village_Of_Salamatoff')
  - [Village_Of_Sleetmute](#F-VRDR-ValueSets-RaceCode-Village_Of_Sleetmute 'VRDR.ValueSets.RaceCode.Village_Of_Sleetmute')
  - [Village_Of_Solomon](#F-VRDR-ValueSets-RaceCode-Village_Of_Solomon 'VRDR.ValueSets.RaceCode.Village_Of_Solomon')
  - [Village_Of_Stony_River](#F-VRDR-ValueSets-RaceCode-Village_Of_Stony_River 'VRDR.ValueSets.RaceCode.Village_Of_Stony_River')
  - [Village_Of_Venetie](#F-VRDR-ValueSets-RaceCode-Village_Of_Venetie 'VRDR.ValueSets.RaceCode.Village_Of_Venetie')
  - [Village_Of_Wainwright](#F-VRDR-ValueSets-RaceCode-Village_Of_Wainwright 'VRDR.ValueSets.RaceCode.Village_Of_Wainwright')
  - [Village_Of_Wales](#F-VRDR-ValueSets-RaceCode-Village_Of_Wales 'VRDR.ValueSets.RaceCode.Village_Of_Wales')
  - [Village_Of_White_Mountain](#F-VRDR-ValueSets-RaceCode-Village_Of_White_Mountain 'VRDR.ValueSets.RaceCode.Village_Of_White_Mountain')
  - [Village_Ofkaltag](#F-VRDR-ValueSets-RaceCode-Village_Ofkaltag 'VRDR.ValueSets.RaceCode.Village_Ofkaltag')
  - [Waccamaw_Siouan](#F-VRDR-ValueSets-RaceCode-Waccamaw_Siouan 'VRDR.ValueSets.RaceCode.Waccamaw_Siouan')
  - [Waco](#F-VRDR-ValueSets-RaceCode-Waco 'VRDR.ValueSets.RaceCode.Waco')
  - [Wahpekute_Sioux](#F-VRDR-ValueSets-RaceCode-Wahpekute_Sioux 'VRDR.ValueSets.RaceCode.Wahpekute_Sioux')
  - [Wahpeton_Sioux](#F-VRDR-ValueSets-RaceCode-Wahpeton_Sioux 'VRDR.ValueSets.RaceCode.Wahpeton_Sioux')
  - [Wailaki](#F-VRDR-ValueSets-RaceCode-Wailaki 'VRDR.ValueSets.RaceCode.Wailaki')
  - [Wakiakum_Chinook](#F-VRDR-ValueSets-RaceCode-Wakiakum_Chinook 'VRDR.ValueSets.RaceCode.Wakiakum_Chinook')
  - [Walker_River](#F-VRDR-ValueSets-RaceCode-Walker_River 'VRDR.ValueSets.RaceCode.Walker_River')
  - [Wallawalla](#F-VRDR-ValueSets-RaceCode-Wallawalla 'VRDR.ValueSets.RaceCode.Wallawalla')
  - [Wampanoag](#F-VRDR-ValueSets-RaceCode-Wampanoag 'VRDR.ValueSets.RaceCode.Wampanoag')
  - [Wappo](#F-VRDR-ValueSets-RaceCode-Wappo 'VRDR.ValueSets.RaceCode.Wappo')
  - [Warm_Springs](#F-VRDR-ValueSets-RaceCode-Warm_Springs 'VRDR.ValueSets.RaceCode.Warm_Springs')
  - [Wascopum](#F-VRDR-ValueSets-RaceCode-Wascopum 'VRDR.ValueSets.RaceCode.Wascopum')
  - [Washoe](#F-VRDR-ValueSets-RaceCode-Washoe 'VRDR.ValueSets.RaceCode.Washoe')
  - [Wazhaza_Sioux](#F-VRDR-ValueSets-RaceCode-Wazhaza_Sioux 'VRDR.ValueSets.RaceCode.Wazhaza_Sioux')
  - [Wells_Band](#F-VRDR-ValueSets-RaceCode-Wells_Band 'VRDR.ValueSets.RaceCode.Wells_Band')
  - [Wenatchee](#F-VRDR-ValueSets-RaceCode-Wenatchee 'VRDR.ValueSets.RaceCode.Wenatchee')
  - [Wesort](#F-VRDR-ValueSets-RaceCode-Wesort 'VRDR.ValueSets.RaceCode.Wesort')
  - [West_Indies](#F-VRDR-ValueSets-RaceCode-West_Indies 'VRDR.ValueSets.RaceCode.West_Indies')
  - [Western_Cherokee](#F-VRDR-ValueSets-RaceCode-Western_Cherokee 'VRDR.ValueSets.RaceCode.Western_Cherokee')
  - [Western_Chickahominy](#F-VRDR-ValueSets-RaceCode-Western_Chickahominy 'VRDR.ValueSets.RaceCode.Western_Chickahominy')
  - [Whello](#F-VRDR-ValueSets-RaceCode-Whello 'VRDR.ValueSets.RaceCode.Whello')
  - [Whilkut](#F-VRDR-ValueSets-RaceCode-Whilkut 'VRDR.ValueSets.RaceCode.Whilkut')
  - [White](#F-VRDR-ValueSets-RaceCode-White 'VRDR.ValueSets.RaceCode.White')
  - [White_Checkbox](#F-VRDR-ValueSets-RaceCode-White_Checkbox 'VRDR.ValueSets.RaceCode.White_Checkbox')
  - [White_Earth](#F-VRDR-ValueSets-RaceCode-White_Earth 'VRDR.ValueSets.RaceCode.White_Earth')
  - [White_Mountain_Apache](#F-VRDR-ValueSets-RaceCode-White_Mountain_Apache 'VRDR.ValueSets.RaceCode.White_Mountain_Apache')
  - [White_Mountain_Inupiat](#F-VRDR-ValueSets-RaceCode-White_Mountain_Inupiat 'VRDR.ValueSets.RaceCode.White_Mountain_Inupiat')
  - [White_River_Band_Of_The_Chickamauga_Cherokee](#F-VRDR-ValueSets-RaceCode-White_River_Band_Of_The_Chickamauga_Cherokee 'VRDR.ValueSets.RaceCode.White_River_Band_Of_The_Chickamauga_Cherokee')
  - [Wichita](#F-VRDR-ValueSets-RaceCode-Wichita 'VRDR.ValueSets.RaceCode.Wichita')
  - [Wicomico](#F-VRDR-ValueSets-RaceCode-Wicomico 'VRDR.ValueSets.RaceCode.Wicomico')
  - [Wikchamni](#F-VRDR-ValueSets-RaceCode-Wikchamni 'VRDR.ValueSets.RaceCode.Wikchamni')
  - [Willapa_Chinook](#F-VRDR-ValueSets-RaceCode-Willapa_Chinook 'VRDR.ValueSets.RaceCode.Willapa_Chinook')
  - [Wilono](#F-VRDR-ValueSets-RaceCode-Wilono 'VRDR.ValueSets.RaceCode.Wilono')
  - [Wind_River](#F-VRDR-ValueSets-RaceCode-Wind_River 'VRDR.ValueSets.RaceCode.Wind_River')
  - [Wind_River_Arapahoe](#F-VRDR-ValueSets-RaceCode-Wind_River_Arapahoe 'VRDR.ValueSets.RaceCode.Wind_River_Arapahoe')
  - [Wind_River_Shoshone](#F-VRDR-ValueSets-RaceCode-Wind_River_Shoshone 'VRDR.ValueSets.RaceCode.Wind_River_Shoshone')
  - [Winnebago](#F-VRDR-ValueSets-RaceCode-Winnebago 'VRDR.ValueSets.RaceCode.Winnebago')
  - [Winnemucca](#F-VRDR-ValueSets-RaceCode-Winnemucca 'VRDR.ValueSets.RaceCode.Winnemucca')
  - [Wintun](#F-VRDR-ValueSets-RaceCode-Wintun 'VRDR.ValueSets.RaceCode.Wintun')
  - [Wisconsin_Potawatomi](#F-VRDR-ValueSets-RaceCode-Wisconsin_Potawatomi 'VRDR.ValueSets.RaceCode.Wisconsin_Potawatomi')
  - [Wiseman](#F-VRDR-ValueSets-RaceCode-Wiseman 'VRDR.ValueSets.RaceCode.Wiseman')
  - [Wishram](#F-VRDR-ValueSets-RaceCode-Wishram 'VRDR.ValueSets.RaceCode.Wishram')
  - [Wiyot](#F-VRDR-ValueSets-RaceCode-Wiyot 'VRDR.ValueSets.RaceCode.Wiyot')
  - [Woodsfords_Community](#F-VRDR-ValueSets-RaceCode-Woodsfords_Community 'VRDR.ValueSets.RaceCode.Woodsfords_Community')
  - [Wrangell_Cooperative_Association](#F-VRDR-ValueSets-RaceCode-Wrangell_Cooperative_Association 'VRDR.ValueSets.RaceCode.Wrangell_Cooperative_Association')
  - [Wyandotte](#F-VRDR-ValueSets-RaceCode-Wyandotte 'VRDR.ValueSets.RaceCode.Wyandotte')
  - [Yahooskin_Band_Of_Snake](#F-VRDR-ValueSets-RaceCode-Yahooskin_Band_Of_Snake 'VRDR.ValueSets.RaceCode.Yahooskin_Band_Of_Snake')
  - [Yakama](#F-VRDR-ValueSets-RaceCode-Yakama 'VRDR.ValueSets.RaceCode.Yakama')
  - [Yakama_Cowlitz](#F-VRDR-ValueSets-RaceCode-Yakama_Cowlitz 'VRDR.ValueSets.RaceCode.Yakama_Cowlitz')
  - [Yakutat_Tlingit_Tribe](#F-VRDR-ValueSets-RaceCode-Yakutat_Tlingit_Tribe 'VRDR.ValueSets.RaceCode.Yakutat_Tlingit_Tribe')
  - [Yana](#F-VRDR-ValueSets-RaceCode-Yana 'VRDR.ValueSets.RaceCode.Yana')
  - [Yankton_Sioux](#F-VRDR-ValueSets-RaceCode-Yankton_Sioux 'VRDR.ValueSets.RaceCode.Yankton_Sioux')
  - [Yanktonai_Sioux](#F-VRDR-ValueSets-RaceCode-Yanktonai_Sioux 'VRDR.ValueSets.RaceCode.Yanktonai_Sioux')
  - [Yapese](#F-VRDR-ValueSets-RaceCode-Yapese 'VRDR.ValueSets.RaceCode.Yapese')
  - [Yaqui](#F-VRDR-ValueSets-RaceCode-Yaqui 'VRDR.ValueSets.RaceCode.Yaqui')
  - [Yavapai_Apache](#F-VRDR-ValueSets-RaceCode-Yavapai_Apache 'VRDR.ValueSets.RaceCode.Yavapai_Apache')
  - [Yavapaiprescott_Tribe_Of_The_Yavapai_Reservation](#F-VRDR-ValueSets-RaceCode-Yavapaiprescott_Tribe_Of_The_Yavapai_Reservation 'VRDR.ValueSets.RaceCode.Yavapaiprescott_Tribe_Of_The_Yavapai_Reservation')
  - [Yello](#F-VRDR-ValueSets-RaceCode-Yello 'VRDR.ValueSets.RaceCode.Yello')
  - [Yerington_Paiute](#F-VRDR-ValueSets-RaceCode-Yerington_Paiute 'VRDR.ValueSets.RaceCode.Yerington_Paiute')
  - [Yokuts](#F-VRDR-ValueSets-RaceCode-Yokuts 'VRDR.ValueSets.RaceCode.Yokuts')
  - [Yomba](#F-VRDR-ValueSets-RaceCode-Yomba 'VRDR.ValueSets.RaceCode.Yomba')
  - [Ysleta_Del_Sur_Pueblo_Of_Texas](#F-VRDR-ValueSets-RaceCode-Ysleta_Del_Sur_Pueblo_Of_Texas 'VRDR.ValueSets.RaceCode.Ysleta_Del_Sur_Pueblo_Of_Texas')
  - [Yuchi](#F-VRDR-ValueSets-RaceCode-Yuchi 'VRDR.ValueSets.RaceCode.Yuchi')
  - [Yuki](#F-VRDR-ValueSets-RaceCode-Yuki 'VRDR.ValueSets.RaceCode.Yuki')
  - [Yuman](#F-VRDR-ValueSets-RaceCode-Yuman 'VRDR.ValueSets.RaceCode.Yuman')
  - [Yupiit_Of_Andreafski](#F-VRDR-ValueSets-RaceCode-Yupiit_Of_Andreafski 'VRDR.ValueSets.RaceCode.Yupiit_Of_Andreafski')
  - [Yupik](#F-VRDR-ValueSets-RaceCode-Yupik 'VRDR.ValueSets.RaceCode.Yupik')
  - [Yupik_Eskimo](#F-VRDR-ValueSets-RaceCode-Yupik_Eskimo 'VRDR.ValueSets.RaceCode.Yupik_Eskimo')
  - [Yurok](#F-VRDR-ValueSets-RaceCode-Yurok 'VRDR.ValueSets.RaceCode.Yurok')
  - [Zaire](#F-VRDR-ValueSets-RaceCode-Zaire 'VRDR.ValueSets.RaceCode.Zaire')
  - [Zia](#F-VRDR-ValueSets-RaceCode-Zia 'VRDR.ValueSets.RaceCode.Zia')
  - [Zuni](#F-VRDR-ValueSets-RaceCode-Zuni 'VRDR.ValueSets.RaceCode.Zuni')
- [RaceMissingValueReason](#T-VRDR-Mappings-RaceMissingValueReason 'VRDR.Mappings.RaceMissingValueReason')
- [RaceMissingValueReason](#T-VRDR-ValueSets-RaceMissingValueReason 'VRDR.ValueSets.RaceMissingValueReason')
  - [FHIRToIJE](#F-VRDR-Mappings-RaceMissingValueReason-FHIRToIJE 'VRDR.Mappings.RaceMissingValueReason.FHIRToIJE')
  - [IJEToFHIR](#F-VRDR-Mappings-RaceMissingValueReason-IJEToFHIR 'VRDR.Mappings.RaceMissingValueReason.IJEToFHIR')
  - [Codes](#F-VRDR-ValueSets-RaceMissingValueReason-Codes 'VRDR.ValueSets.RaceMissingValueReason.Codes')
  - [Not_Obtainable](#F-VRDR-ValueSets-RaceMissingValueReason-Not_Obtainable 'VRDR.ValueSets.RaceMissingValueReason.Not_Obtainable')
  - [Refused](#F-VRDR-ValueSets-RaceMissingValueReason-Refused 'VRDR.ValueSets.RaceMissingValueReason.Refused')
  - [Sought_But_Unknown](#F-VRDR-ValueSets-RaceMissingValueReason-Sought_But_Unknown 'VRDR.ValueSets.RaceMissingValueReason.Sought_But_Unknown')
- [RaceRecode40](#T-VRDR-Mappings-RaceRecode40 'VRDR.Mappings.RaceRecode40')
- [RaceRecode40](#T-VRDR-ValueSets-RaceRecode40 'VRDR.ValueSets.RaceRecode40')
  - [FHIRToIJE](#F-VRDR-Mappings-RaceRecode40-FHIRToIJE 'VRDR.Mappings.RaceRecode40.FHIRToIJE')
  - [IJEToFHIR](#F-VRDR-Mappings-RaceRecode40-IJEToFHIR 'VRDR.Mappings.RaceRecode40.IJEToFHIR')
  - [Aian_And_Asian](#F-VRDR-ValueSets-RaceRecode40-Aian_And_Asian 'VRDR.ValueSets.RaceRecode40.Aian_And_Asian')
  - [Aian_And_Nhopi](#F-VRDR-ValueSets-RaceRecode40-Aian_And_Nhopi 'VRDR.ValueSets.RaceRecode40.Aian_And_Nhopi')
  - [Aian_And_Nhopi_2](#F-VRDR-ValueSets-RaceRecode40-Aian_And_Nhopi_2 'VRDR.ValueSets.RaceRecode40.Aian_And_Nhopi_2')
  - [Aian_Asian_And_Nhopi](#F-VRDR-ValueSets-RaceRecode40-Aian_Asian_And_Nhopi 'VRDR.ValueSets.RaceRecode40.Aian_Asian_And_Nhopi')
  - [Aian_Asian_And_White](#F-VRDR-ValueSets-RaceRecode40-Aian_Asian_And_White 'VRDR.ValueSets.RaceRecode40.Aian_Asian_And_White')
  - [Aian_Asian_Nhopi_And_White](#F-VRDR-ValueSets-RaceRecode40-Aian_Asian_Nhopi_And_White 'VRDR.ValueSets.RaceRecode40.Aian_Asian_Nhopi_And_White')
  - [Aian_Nhopi_And_White](#F-VRDR-ValueSets-RaceRecode40-Aian_Nhopi_And_White 'VRDR.ValueSets.RaceRecode40.Aian_Nhopi_And_White')
  - [American_Indian_Or_Alaskan_Native_Aian](#F-VRDR-ValueSets-RaceRecode40-American_Indian_Or_Alaskan_Native_Aian 'VRDR.ValueSets.RaceRecode40.American_Indian_Or_Alaskan_Native_Aian')
  - [Asian_And_Nhopi](#F-VRDR-ValueSets-RaceRecode40-Asian_And_Nhopi 'VRDR.ValueSets.RaceRecode40.Asian_And_Nhopi')
  - [Asian_And_White](#F-VRDR-ValueSets-RaceRecode40-Asian_And_White 'VRDR.ValueSets.RaceRecode40.Asian_And_White')
  - [Asian_Indian](#F-VRDR-ValueSets-RaceRecode40-Asian_Indian 'VRDR.ValueSets.RaceRecode40.Asian_Indian')
  - [Asian_Nhopi_And_White](#F-VRDR-ValueSets-RaceRecode40-Asian_Nhopi_And_White 'VRDR.ValueSets.RaceRecode40.Asian_Nhopi_And_White')
  - [Black](#F-VRDR-ValueSets-RaceRecode40-Black 'VRDR.ValueSets.RaceRecode40.Black')
  - [Black_Aian_And_Asian](#F-VRDR-ValueSets-RaceRecode40-Black_Aian_And_Asian 'VRDR.ValueSets.RaceRecode40.Black_Aian_And_Asian')
  - [Black_Aian_And_Nhopi](#F-VRDR-ValueSets-RaceRecode40-Black_Aian_And_Nhopi 'VRDR.ValueSets.RaceRecode40.Black_Aian_And_Nhopi')
  - [Black_Aian_And_White](#F-VRDR-ValueSets-RaceRecode40-Black_Aian_And_White 'VRDR.ValueSets.RaceRecode40.Black_Aian_And_White')
  - [Black_Aian_Asian_And_Nhopi](#F-VRDR-ValueSets-RaceRecode40-Black_Aian_Asian_And_Nhopi 'VRDR.ValueSets.RaceRecode40.Black_Aian_Asian_And_Nhopi')
  - [Black_Aian_Asian_And_White](#F-VRDR-ValueSets-RaceRecode40-Black_Aian_Asian_And_White 'VRDR.ValueSets.RaceRecode40.Black_Aian_Asian_And_White')
  - [Black_Aian_Asian_Nhopi_And_White](#F-VRDR-ValueSets-RaceRecode40-Black_Aian_Asian_Nhopi_And_White 'VRDR.ValueSets.RaceRecode40.Black_Aian_Asian_Nhopi_And_White')
  - [Black_Aian_Nhopi_And_White](#F-VRDR-ValueSets-RaceRecode40-Black_Aian_Nhopi_And_White 'VRDR.ValueSets.RaceRecode40.Black_Aian_Nhopi_And_White')
  - [Black_And_Aian](#F-VRDR-ValueSets-RaceRecode40-Black_And_Aian 'VRDR.ValueSets.RaceRecode40.Black_And_Aian')
  - [Black_And_Asian](#F-VRDR-ValueSets-RaceRecode40-Black_And_Asian 'VRDR.ValueSets.RaceRecode40.Black_And_Asian')
  - [Black_And_White](#F-VRDR-ValueSets-RaceRecode40-Black_And_White 'VRDR.ValueSets.RaceRecode40.Black_And_White')
  - [Black_Asian_And_Nhopi](#F-VRDR-ValueSets-RaceRecode40-Black_Asian_And_Nhopi 'VRDR.ValueSets.RaceRecode40.Black_Asian_And_Nhopi')
  - [Black_Asian_And_White](#F-VRDR-ValueSets-RaceRecode40-Black_Asian_And_White 'VRDR.ValueSets.RaceRecode40.Black_Asian_And_White')
  - [Black_Asian_Nhopi_And_White](#F-VRDR-ValueSets-RaceRecode40-Black_Asian_Nhopi_And_White 'VRDR.ValueSets.RaceRecode40.Black_Asian_Nhopi_And_White')
  - [Black_Nhopi_And_White](#F-VRDR-ValueSets-RaceRecode40-Black_Nhopi_And_White 'VRDR.ValueSets.RaceRecode40.Black_Nhopi_And_White')
  - [Chinese](#F-VRDR-ValueSets-RaceRecode40-Chinese 'VRDR.ValueSets.RaceRecode40.Chinese')
  - [Codes](#F-VRDR-ValueSets-RaceRecode40-Codes 'VRDR.ValueSets.RaceRecode40.Codes')
  - [Filipino](#F-VRDR-ValueSets-RaceRecode40-Filipino 'VRDR.ValueSets.RaceRecode40.Filipino')
  - [Guamanian](#F-VRDR-ValueSets-RaceRecode40-Guamanian 'VRDR.ValueSets.RaceRecode40.Guamanian')
  - [Hawaiian](#F-VRDR-ValueSets-RaceRecode40-Hawaiian 'VRDR.ValueSets.RaceRecode40.Hawaiian')
  - [Japanese](#F-VRDR-ValueSets-RaceRecode40-Japanese 'VRDR.ValueSets.RaceRecode40.Japanese')
  - [Korean](#F-VRDR-ValueSets-RaceRecode40-Korean 'VRDR.ValueSets.RaceRecode40.Korean')
  - [Nhopi_And_White](#F-VRDR-ValueSets-RaceRecode40-Nhopi_And_White 'VRDR.ValueSets.RaceRecode40.Nhopi_And_White')
  - [Nhopi_And_White_2](#F-VRDR-ValueSets-RaceRecode40-Nhopi_And_White_2 'VRDR.ValueSets.RaceRecode40.Nhopi_And_White_2')
  - [Other_Or_Multiple_Asian](#F-VRDR-ValueSets-RaceRecode40-Other_Or_Multiple_Asian 'VRDR.ValueSets.RaceRecode40.Other_Or_Multiple_Asian')
  - [Other_Or_Multiple_Pacific_Islander](#F-VRDR-ValueSets-RaceRecode40-Other_Or_Multiple_Pacific_Islander 'VRDR.ValueSets.RaceRecode40.Other_Or_Multiple_Pacific_Islander')
  - [Samoan](#F-VRDR-ValueSets-RaceRecode40-Samoan 'VRDR.ValueSets.RaceRecode40.Samoan')
  - [Unknown_And_Other_Race](#F-VRDR-ValueSets-RaceRecode40-Unknown_And_Other_Race 'VRDR.ValueSets.RaceRecode40.Unknown_And_Other_Race')
  - [Vietnamese](#F-VRDR-ValueSets-RaceRecode40-Vietnamese 'VRDR.ValueSets.RaceRecode40.Vietnamese')
  - [White](#F-VRDR-ValueSets-RaceRecode40-White 'VRDR.ValueSets.RaceRecode40.White')
- [ReplaceStatus](#T-VRDR-Mappings-ReplaceStatus 'VRDR.Mappings.ReplaceStatus')
- [ReplaceStatus](#T-VRDR-ValueSets-ReplaceStatus 'VRDR.ValueSets.ReplaceStatus')
  - [FHIRToIJE](#F-VRDR-Mappings-ReplaceStatus-FHIRToIJE 'VRDR.Mappings.ReplaceStatus.FHIRToIJE')
  - [IJEToFHIR](#F-VRDR-Mappings-ReplaceStatus-IJEToFHIR 'VRDR.Mappings.ReplaceStatus.IJEToFHIR')
  - [Codes](#F-VRDR-ValueSets-ReplaceStatus-Codes 'VRDR.ValueSets.ReplaceStatus.Codes')
  - [Original_Record](#F-VRDR-ValueSets-ReplaceStatus-Original_Record 'VRDR.ValueSets.ReplaceStatus.Original_Record')
  - [Updated_Record](#F-VRDR-ValueSets-ReplaceStatus-Updated_Record 'VRDR.ValueSets.ReplaceStatus.Updated_Record')
  - [Updated_Record_Not_For_Nchs](#F-VRDR-ValueSets-ReplaceStatus-Updated_Record_Not_For_Nchs 'VRDR.ValueSets.ReplaceStatus.Updated_Record_Not_For_Nchs')
- [SpouseAlive](#T-VRDR-Mappings-SpouseAlive 'VRDR.Mappings.SpouseAlive')
- [SpouseAlive](#T-VRDR-ValueSets-SpouseAlive 'VRDR.ValueSets.SpouseAlive')
  - [FHIRToIJE](#F-VRDR-Mappings-SpouseAlive-FHIRToIJE 'VRDR.Mappings.SpouseAlive.FHIRToIJE')
  - [IJEToFHIR](#F-VRDR-Mappings-SpouseAlive-IJEToFHIR 'VRDR.Mappings.SpouseAlive.IJEToFHIR')
  - [Codes](#F-VRDR-ValueSets-SpouseAlive-Codes 'VRDR.ValueSets.SpouseAlive.Codes')
  - [No](#F-VRDR-ValueSets-SpouseAlive-No 'VRDR.ValueSets.SpouseAlive.No')
  - [Not_Applicable](#F-VRDR-ValueSets-SpouseAlive-Not_Applicable 'VRDR.ValueSets.SpouseAlive.Not_Applicable')
  - [Unknown](#F-VRDR-ValueSets-SpouseAlive-Unknown 'VRDR.ValueSets.SpouseAlive.Unknown')
  - [Yes](#F-VRDR-ValueSets-SpouseAlive-Yes 'VRDR.ValueSets.SpouseAlive.Yes')
- [SystemReject](#T-VRDR-Mappings-SystemReject 'VRDR.Mappings.SystemReject')
  - [FHIRToIJE](#F-VRDR-Mappings-SystemReject-FHIRToIJE 'VRDR.Mappings.SystemReject.FHIRToIJE')
  - [IJEToFHIR](#F-VRDR-Mappings-SystemReject-IJEToFHIR 'VRDR.Mappings.SystemReject.IJEToFHIR')
- [TRXHelper](#T-VRDR-IJEMortality-TRXHelper 'VRDR.IJEMortality.TRXHelper')
  - [#ctor()](#M-VRDR-IJEMortality-TRXHelper-#ctor-VRDR-DeathRecord- 'VRDR.IJEMortality.TRXHelper.#ctor(VRDR.DeathRecord)')
  - [CS](#P-VRDR-IJEMortality-TRXHelper-CS 'VRDR.IJEMortality.TRXHelper.CS')
  - [SHIP](#P-VRDR-IJEMortality-TRXHelper-SHIP 'VRDR.IJEMortality.TRXHelper.SHIP')
- [TransaxConversion](#T-VRDR-Mappings-TransaxConversion 'VRDR.Mappings.TransaxConversion')
- [TransaxConversion](#T-VRDR-ValueSets-TransaxConversion 'VRDR.ValueSets.TransaxConversion')
  - [FHIRToIJE](#F-VRDR-Mappings-TransaxConversion-FHIRToIJE 'VRDR.Mappings.TransaxConversion.FHIRToIJE')
  - [IJEToFHIR](#F-VRDR-Mappings-TransaxConversion-IJEToFHIR 'VRDR.Mappings.TransaxConversion.IJEToFHIR')
  - [Artificial_Code_Conversion_No_Other_Action](#F-VRDR-ValueSets-TransaxConversion-Artificial_Code_Conversion_No_Other_Action 'VRDR.ValueSets.TransaxConversion.Artificial_Code_Conversion_No_Other_Action')
  - [Codes](#F-VRDR-ValueSets-TransaxConversion-Codes 'VRDR.ValueSets.TransaxConversion.Codes')
  - [Conversion_Using_Ambivalent_Table_Entries](#F-VRDR-ValueSets-TransaxConversion-Conversion_Using_Ambivalent_Table_Entries 'VRDR.ValueSets.TransaxConversion.Conversion_Using_Ambivalent_Table_Entries')
  - [Conversion_Using_Non_Ambivalent_Table_Entries](#F-VRDR-ValueSets-TransaxConversion-Conversion_Using_Non_Ambivalent_Table_Entries 'VRDR.ValueSets.TransaxConversion.Conversion_Using_Non_Ambivalent_Table_Entries')
  - [Duplicate_Entity_Axis_Codes_Deleted_No_Other_Action_Involved](#F-VRDR-ValueSets-TransaxConversion-Duplicate_Entity_Axis_Codes_Deleted_No_Other_Action_Involved 'VRDR.ValueSets.TransaxConversion.Duplicate_Entity_Axis_Codes_Deleted_No_Other_Action_Involved')
- [TransportationIncidentRole](#T-VRDR-Mappings-TransportationIncidentRole 'VRDR.Mappings.TransportationIncidentRole')
- [TransportationIncidentRole](#T-VRDR-ValueSets-TransportationIncidentRole 'VRDR.ValueSets.TransportationIncidentRole')
  - [FHIRToIJE](#F-VRDR-Mappings-TransportationIncidentRole-FHIRToIJE 'VRDR.Mappings.TransportationIncidentRole.FHIRToIJE')
  - [IJEToFHIR](#F-VRDR-Mappings-TransportationIncidentRole-IJEToFHIR 'VRDR.Mappings.TransportationIncidentRole.IJEToFHIR')
  - [Codes](#F-VRDR-ValueSets-TransportationIncidentRole-Codes 'VRDR.ValueSets.TransportationIncidentRole.Codes')
  - [Not_Applicable](#F-VRDR-ValueSets-TransportationIncidentRole-Not_Applicable 'VRDR.ValueSets.TransportationIncidentRole.Not_Applicable')
  - [Other](#F-VRDR-ValueSets-TransportationIncidentRole-Other 'VRDR.ValueSets.TransportationIncidentRole.Other')
  - [Passenger](#F-VRDR-ValueSets-TransportationIncidentRole-Passenger 'VRDR.ValueSets.TransportationIncidentRole.Passenger')
  - [Pedestrian](#F-VRDR-ValueSets-TransportationIncidentRole-Pedestrian 'VRDR.ValueSets.TransportationIncidentRole.Pedestrian')
  - [Unknown](#F-VRDR-ValueSets-TransportationIncidentRole-Unknown 'VRDR.ValueSets.TransportationIncidentRole.Unknown')
  - [Vehicle_Driver](#F-VRDR-ValueSets-TransportationIncidentRole-Vehicle_Driver 'VRDR.ValueSets.TransportationIncidentRole.Vehicle_Driver')
- [Types](#T-VRDR-Property-Types 'VRDR.Property.Types')
  - [Bool](#F-VRDR-Property-Types-Bool 'VRDR.Property.Types.Bool')
  - [Dictionary](#F-VRDR-Property-Types-Dictionary 'VRDR.Property.Types.Dictionary')
  - [String](#F-VRDR-Property-Types-String 'VRDR.Property.Types.String')
  - [StringArr](#F-VRDR-Property-Types-StringArr 'VRDR.Property.Types.StringArr')
  - [StringDateTime](#F-VRDR-Property-Types-StringDateTime 'VRDR.Property.Types.StringDateTime')
  - [Tuple4Arr](#F-VRDR-Property-Types-Tuple4Arr 'VRDR.Property.Types.Tuple4Arr')
  - [TupleArr](#F-VRDR-Property-Types-TupleArr 'VRDR.Property.Types.TupleArr')
  - [TupleCOD](#F-VRDR-Property-Types-TupleCOD 'VRDR.Property.Types.TupleCOD')
  - [UInt32](#F-VRDR-Property-Types-UInt32 'VRDR.Property.Types.UInt32')
- [UnitsOfAge](#T-VRDR-Mappings-UnitsOfAge 'VRDR.Mappings.UnitsOfAge')
- [UnitsOfAge](#T-VRDR-ValueSets-UnitsOfAge 'VRDR.ValueSets.UnitsOfAge')
  - [FHIRToIJE](#F-VRDR-Mappings-UnitsOfAge-FHIRToIJE 'VRDR.Mappings.UnitsOfAge.FHIRToIJE')
  - [IJEToFHIR](#F-VRDR-Mappings-UnitsOfAge-IJEToFHIR 'VRDR.Mappings.UnitsOfAge.IJEToFHIR')
  - [Codes](#F-VRDR-ValueSets-UnitsOfAge-Codes 'VRDR.ValueSets.UnitsOfAge.Codes')
  - [Days](#F-VRDR-ValueSets-UnitsOfAge-Days 'VRDR.ValueSets.UnitsOfAge.Days')
  - [Hours](#F-VRDR-ValueSets-UnitsOfAge-Hours 'VRDR.ValueSets.UnitsOfAge.Hours')
  - [Minutes](#F-VRDR-ValueSets-UnitsOfAge-Minutes 'VRDR.ValueSets.UnitsOfAge.Minutes')
  - [Months](#F-VRDR-ValueSets-UnitsOfAge-Months 'VRDR.ValueSets.UnitsOfAge.Months')
  - [Unknown](#F-VRDR-ValueSets-UnitsOfAge-Unknown 'VRDR.ValueSets.UnitsOfAge.Unknown')
  - [Years](#F-VRDR-ValueSets-UnitsOfAge-Years 'VRDR.ValueSets.UnitsOfAge.Years')
- [ValueSets](#T-VRDR-ValueSets 'VRDR.ValueSets')
- [YesNoNotApplicable](#T-VRDR-Mappings-YesNoNotApplicable 'VRDR.Mappings.YesNoNotApplicable')
  - [FHIRToIJE](#F-VRDR-Mappings-YesNoNotApplicable-FHIRToIJE 'VRDR.Mappings.YesNoNotApplicable.FHIRToIJE')
  - [IJEToFHIR](#F-VRDR-Mappings-YesNoNotApplicable-IJEToFHIR 'VRDR.Mappings.YesNoNotApplicable.IJEToFHIR')
- [YesNoUnknown](#T-VRDR-Mappings-YesNoUnknown 'VRDR.Mappings.YesNoUnknown')
- [YesNoUnknown](#T-VRDR-ValueSets-YesNoUnknown 'VRDR.ValueSets.YesNoUnknown')
  - [FHIRToIJE](#F-VRDR-Mappings-YesNoUnknown-FHIRToIJE 'VRDR.Mappings.YesNoUnknown.FHIRToIJE')
  - [IJEToFHIR](#F-VRDR-Mappings-YesNoUnknown-IJEToFHIR 'VRDR.Mappings.YesNoUnknown.IJEToFHIR')
  - [Codes](#F-VRDR-ValueSets-YesNoUnknown-Codes 'VRDR.ValueSets.YesNoUnknown.Codes')
  - [No](#F-VRDR-ValueSets-YesNoUnknown-No 'VRDR.ValueSets.YesNoUnknown.No')
  - [Unknown](#F-VRDR-ValueSets-YesNoUnknown-Unknown 'VRDR.ValueSets.YesNoUnknown.Unknown')
  - [Yes](#F-VRDR-ValueSets-YesNoUnknown-Yes 'VRDR.ValueSets.YesNoUnknown.Yes')
- [YesNoUnknownNotApplicable](#T-VRDR-Mappings-YesNoUnknownNotApplicable 'VRDR.Mappings.YesNoUnknownNotApplicable')
- [YesNoUnknownNotApplicable](#T-VRDR-ValueSets-YesNoUnknownNotApplicable 'VRDR.ValueSets.YesNoUnknownNotApplicable')
  - [FHIRToIJE](#F-VRDR-Mappings-YesNoUnknownNotApplicable-FHIRToIJE 'VRDR.Mappings.YesNoUnknownNotApplicable.FHIRToIJE')
  - [IJEToFHIR](#F-VRDR-Mappings-YesNoUnknownNotApplicable-IJEToFHIR 'VRDR.Mappings.YesNoUnknownNotApplicable.IJEToFHIR')
  - [Codes](#F-VRDR-ValueSets-YesNoUnknownNotApplicable-Codes 'VRDR.ValueSets.YesNoUnknownNotApplicable.Codes')
  - [No](#F-VRDR-ValueSets-YesNoUnknownNotApplicable-No 'VRDR.ValueSets.YesNoUnknownNotApplicable.No')
  - [Not_Applicable](#F-VRDR-ValueSets-YesNoUnknownNotApplicable-Not_Applicable 'VRDR.ValueSets.YesNoUnknownNotApplicable.Not_Applicable')
  - [Unknown](#F-VRDR-ValueSets-YesNoUnknownNotApplicable-Unknown 'VRDR.ValueSets.YesNoUnknownNotApplicable.Unknown')
  - [Yes](#F-VRDR-ValueSets-YesNoUnknownNotApplicable-Yes 'VRDR.ValueSets.YesNoUnknownNotApplicable.Yes')

<a name='T-VRDR-ValueSets-AcmeSystemReject'></a>
## AcmeSystemReject `type`

##### Namespace

VRDR.ValueSets

##### Summary

AcmeSystemReject

<a name='F-VRDR-ValueSets-AcmeSystemReject-Acme_Reject'></a>
### Acme_Reject `constants`

##### Summary

Acme_Reject

<a name='F-VRDR-ValueSets-AcmeSystemReject-Codes'></a>
### Codes `constants`

##### Summary

Codes

<a name='F-VRDR-ValueSets-AcmeSystemReject-Micar_Reject_Dictionary_Match'></a>
### Micar_Reject_Dictionary_Match `constants`

##### Summary

Micar_Reject_Dictionary_Match

<a name='F-VRDR-ValueSets-AcmeSystemReject-Micar_Reject_Rule_Application'></a>
### Micar_Reject_Rule_Application `constants`

##### Summary

Micar_Reject_Rule_Application

<a name='F-VRDR-ValueSets-AcmeSystemReject-Not_Rejected'></a>
### Not_Rejected `constants`

##### Summary

Not_Rejected

<a name='F-VRDR-ValueSets-AcmeSystemReject-Record_Reviewed'></a>
### Record_Reviewed `constants`

##### Summary

Record_Reviewed

<a name='T-VRDR-Mappings-ActivityAtTimeOfDeath'></a>
## ActivityAtTimeOfDeath `type`

##### Namespace

VRDR.Mappings

##### Summary

Mappings for ActivityAtTimeOfDeath

<a name='T-VRDR-ValueSets-ActivityAtTimeOfDeath'></a>
## ActivityAtTimeOfDeath `type`

##### Namespace

VRDR.ValueSets

##### Summary

ActivityAtTimeOfDeath

<a name='F-VRDR-Mappings-ActivityAtTimeOfDeath-FHIRToIJE'></a>
### FHIRToIJE `constants`

##### Summary

FHIR -> IJE Mapping for ActivityAtTimeOfDeath

<a name='F-VRDR-Mappings-ActivityAtTimeOfDeath-IJEToFHIR'></a>
### IJEToFHIR `constants`

##### Summary

IJE -> FHIR Mapping for ActivityAtTimeOfDeath

<a name='F-VRDR-ValueSets-ActivityAtTimeOfDeath-Codes'></a>
### Codes `constants`

##### Summary

Codes

<a name='F-VRDR-ValueSets-ActivityAtTimeOfDeath-During_Unspecified_Activity'></a>
### During_Unspecified_Activity `constants`

##### Summary

During_Unspecified_Activity

<a name='F-VRDR-ValueSets-ActivityAtTimeOfDeath-Unknown'></a>
### Unknown `constants`

##### Summary

Unknown

<a name='F-VRDR-ValueSets-ActivityAtTimeOfDeath-While_Engaged_In_Leisure_Activities'></a>
### While_Engaged_In_Leisure_Activities `constants`

##### Summary

While_Engaged_In_Leisure_Activities

<a name='F-VRDR-ValueSets-ActivityAtTimeOfDeath-While_Engaged_In_Other_Specified_Activities'></a>
### While_Engaged_In_Other_Specified_Activities `constants`

##### Summary

While_Engaged_In_Other_Specified_Activities

<a name='F-VRDR-ValueSets-ActivityAtTimeOfDeath-While_Engaged_In_Other_Types_Of_Work'></a>
### While_Engaged_In_Other_Types_Of_Work `constants`

##### Summary

While_Engaged_In_Other_Types_Of_Work

<a name='F-VRDR-ValueSets-ActivityAtTimeOfDeath-While_Engaged_In_Sports_Activity'></a>
### While_Engaged_In_Sports_Activity `constants`

##### Summary

While_Engaged_In_Sports_Activity

<a name='F-VRDR-ValueSets-ActivityAtTimeOfDeath-While_Resting_Sleeping_Eating_Or_Engaging_In_Other_Vital_Activities'></a>
### While_Resting_Sleeping_Eating_Or_Engaging_In_Other_Vital_Activities `constants`

##### Summary

While_Resting_Sleeping_Eating_Or_Engaging_In_Other_Vital_Activities

<a name='F-VRDR-ValueSets-ActivityAtTimeOfDeath-While_Working_For_Income'></a>
### While_Working_For_Income `constants`

##### Summary

While_Working_For_Income

<a name='T-VRDR-Mappings-AdministrativeGender'></a>
## AdministrativeGender `type`

##### Namespace

VRDR.Mappings

##### Summary

Mappings for AdministrativeGender

<a name='T-VRDR-ValueSets-AdministrativeGender'></a>
## AdministrativeGender `type`

##### Namespace

VRDR.ValueSets

##### Summary

AdministrativeGender

<a name='F-VRDR-Mappings-AdministrativeGender-FHIRToIJE'></a>
### FHIRToIJE `constants`

##### Summary

FHIR -> IJE Mapping for AdministrativeGender

<a name='F-VRDR-Mappings-AdministrativeGender-IJEToFHIR'></a>
### IJEToFHIR `constants`

##### Summary

IJE -> FHIR Mapping for AdministrativeGender

<a name='F-VRDR-ValueSets-AdministrativeGender-Codes'></a>
### Codes `constants`

##### Summary

Codes

<a name='F-VRDR-ValueSets-AdministrativeGender-Female'></a>
### Female `constants`

##### Summary

Female

<a name='F-VRDR-ValueSets-AdministrativeGender-Male'></a>
### Male `constants`

##### Summary

Male

<a name='F-VRDR-ValueSets-AdministrativeGender-Unknown'></a>
### Unknown `constants`

##### Summary

Unknown

<a name='T-VRDR-Mappings-CertifierTypes'></a>
## CertifierTypes `type`

##### Namespace

VRDR.Mappings

##### Summary

Mappings for CertifierTypes

<a name='T-VRDR-ValueSets-CertifierTypes'></a>
## CertifierTypes `type`

##### Namespace

VRDR.ValueSets

##### Summary

CertifierTypes

<a name='F-VRDR-Mappings-CertifierTypes-FHIRToIJE'></a>
### FHIRToIJE `constants`

##### Summary

FHIR -> IJE Mapping for CertifierTypes

<a name='F-VRDR-Mappings-CertifierTypes-IJEToFHIR'></a>
### IJEToFHIR `constants`

##### Summary

IJE -> FHIR Mapping for CertifierTypes

<a name='F-VRDR-ValueSets-CertifierTypes-Certifying_Physician'></a>
### Certifying_Physician `constants`

##### Summary

Certifying_Physician

<a name='F-VRDR-ValueSets-CertifierTypes-Codes'></a>
### Codes `constants`

##### Summary

Codes

<a name='F-VRDR-ValueSets-CertifierTypes-Medical_Examiner_Coroner'></a>
### Medical_Examiner_Coroner `constants`

##### Summary

Medical_Examiner_Coroner

<a name='F-VRDR-ValueSets-CertifierTypes-Other_Specify'></a>
### Other_Specify `constants`

##### Summary

Other_Specify

<a name='F-VRDR-ValueSets-CertifierTypes-Pronouncing_Certifying_Physician'></a>
### Pronouncing_Certifying_Physician `constants`

##### Summary

Pronouncing_Certifying_Physician

<a name='T-VRDR-CodeSystems'></a>
## CodeSystems `type`

##### Namespace

VRDR

##### Summary

Single definitions for CodeSystem OIDs and URLs used throughout.

<a name='F-VRDR-CodeSystems-ActivityAtTimeOfDeath'></a>
### ActivityAtTimeOfDeath `constants`

##### Summary

Activity at Time of Death

<a name='F-VRDR-CodeSystems-AdministrativeGender'></a>
### AdministrativeGender `constants`

##### Summary

Administrative Gender

<a name='F-VRDR-CodeSystems-BypassEditFlag'></a>
### BypassEditFlag `constants`

##### Summary

Bypass Edit Flag

<a name='F-VRDR-CodeSystems-Component'></a>
### Component `constants`

##### Summary

Component

<a name='F-VRDR-CodeSystems-ComponentCode'></a>
### ComponentCode `constants`

##### Summary

Component Codes

<a name='F-VRDR-CodeSystems-Data_Absent_Reason_HL7_V3'></a>
### Data_Absent_Reason_HL7_V3 `constants`

##### Summary

HL7 Data Absent reason.

<a name='F-VRDR-CodeSystems-DegreeLicenceAndCertificate'></a>
### DegreeLicenceAndCertificate `constants`

##### Summary

Degree Licence and Certificate

<a name='F-VRDR-CodeSystems-DocumentSections'></a>
### DocumentSections `constants`

##### Summary

Composition document sections

<a name='F-VRDR-CodeSystems-EducationLevel'></a>
### EducationLevel `constants`

##### Summary

Education Level

<a name='F-VRDR-CodeSystems-FilingFormat'></a>
### FilingFormat `constants`

##### Summary

Filing Format

<a name='F-VRDR-CodeSystems-HL7_identifier_type'></a>
### HL7_identifier_type `constants`

##### Summary

HL7 Identifier Type.

<a name='F-VRDR-CodeSystems-HL7_location_physical_type'></a>
### HL7_location_physical_type `constants`

##### Summary

HL7 Location Physical Type.

<a name='F-VRDR-CodeSystems-HispanicOrigin'></a>
### HispanicOrigin `constants`

##### Summary

Hispanic Origin

<a name='F-VRDR-CodeSystems-ICD10'></a>
### ICD10 `constants`

##### Summary

ICD10

<a name='F-VRDR-CodeSystems-IntentionalReject'></a>
### IntentionalReject `constants`

##### Summary

Intentional Reject

<a name='F-VRDR-CodeSystems-LOINC'></a>
### LOINC `constants`

##### Summary

LOINC.

<a name='F-VRDR-CodeSystems-LocationType'></a>
### LocationType `constants`

##### Summary

Location Type

<a name='F-VRDR-CodeSystems-MissingValueReason'></a>
### MissingValueReason `constants`

##### Summary

Missing Value Reason

<a name='F-VRDR-CodeSystems-NullFlavor_HL7_V3'></a>
### NullFlavor_HL7_V3 `constants`

##### Summary

HL7 V3 Null Flavor.

<a name='F-VRDR-CodeSystems-ObservationCategory'></a>
### ObservationCategory `constants`

##### Summary

Observation Category.

<a name='F-VRDR-CodeSystems-ObservationCode'></a>
### ObservationCode `constants`

##### Summary

Observation Codes

<a name='F-VRDR-CodeSystems-OrganizationType'></a>
### OrganizationType `constants`

##### Summary

Organization Type

<a name='F-VRDR-CodeSystems-PH_MaritalStatus_HL7_2x'></a>
### PH_MaritalStatus_HL7_2x `constants`

##### Summary

PHINVADS Marital Status.

<a name='F-VRDR-CodeSystems-PregnancyStatus'></a>
### PregnancyStatus `constants`

##### Summary

Pregnancy Status

<a name='F-VRDR-CodeSystems-RaceCode'></a>
### RaceCode `constants`

##### Summary

Race Code

<a name='F-VRDR-CodeSystems-RaceRecode40'></a>
### RaceRecode40 `constants`

##### Summary

Race Recode40

<a name='F-VRDR-CodeSystems-ReplaceStatus'></a>
### ReplaceStatus `constants`

##### Summary

Replace Status

<a name='F-VRDR-CodeSystems-RoleCode_HL7_V3'></a>
### RoleCode_HL7_V3 `constants`

##### Summary

HL7 RoleCode.

<a name='F-VRDR-CodeSystems-SCT'></a>
### SCT `constants`

##### Summary

SNOMEDCT.

<a name='F-VRDR-CodeSystems-SystemReject'></a>
### SystemReject `constants`

##### Summary

System Reject

<a name='F-VRDR-CodeSystems-TransaxConversion'></a>
### TransaxConversion `constants`

##### Summary

Hispanic Origin

<a name='F-VRDR-CodeSystems-US_NPI_HL7'></a>
### US_NPI_HL7 `constants`

##### Summary

US NPI HL7

<a name='F-VRDR-CodeSystems-US_SSN'></a>
### US_SSN `constants`

##### Summary

Social Security Numbers.

<a name='F-VRDR-CodeSystems-UnitsOfMeasure'></a>
### UnitsOfMeasure `constants`

##### Summary

Units of Measure

<a name='F-VRDR-CodeSystems-YesNo'></a>
### YesNo `constants`

##### Summary

HL7 Yes No

<a name='F-VRDR-CodeSystems-YesNo_0136HL7_V2'></a>
### YesNo_0136HL7_V2 `constants`

##### Summary

HL7 Yes No.

<a name='T-VRDR-Connectathon'></a>
## Connectathon `type`

##### Namespace

VRDR

##### Summary

Class `Connectathon` provides static methods for generating records used in Connectathon testing, used in Canary and in the CLI tool

<a name='M-VRDR-Connectathon-DavisLineberry'></a>
### DavisLineberry() `method`

##### Summary

Generate the Davis Lineberry example record

##### Parameters

This method has no parameters.

<a name='M-VRDR-Connectathon-FideliaAlsup'></a>
### FideliaAlsup() `method`

##### Summary

Generate the Fidelia Alsup example record

##### Parameters

This method has no parameters.

<a name='M-VRDR-Connectathon-FromId-System-Int32,System-Nullable{System-Int32},System-String-'></a>
### FromId() `method`

##### Summary

Generate a DeathRecord from one of 5 pre-set records, providing an optional certificate number and state

##### Parameters

This method has no parameters.

<a name='M-VRDR-Connectathon-TwilaHilty'></a>
### TwilaHilty() `method`

##### Summary

Generate the Twila Hilty example record

##### Parameters

This method has no parameters.

<a name='M-VRDR-Connectathon-WriteRecordAsXml-VRDR-DeathRecord,System-String-'></a>
### WriteRecordAsXml() `method`

##### Summary

Of historical interest for writing a record to a file

##### Parameters

This method has no parameters.

<a name='T-VRDR-Mappings-ContributoryTobaccoUse'></a>
## ContributoryTobaccoUse `type`

##### Namespace

VRDR.Mappings

##### Summary

Mappings for ContributoryTobaccoUse

<a name='T-VRDR-ValueSets-ContributoryTobaccoUse'></a>
## ContributoryTobaccoUse `type`

##### Namespace

VRDR.ValueSets

##### Summary

ContributoryTobaccoUse

<a name='F-VRDR-Mappings-ContributoryTobaccoUse-FHIRToIJE'></a>
### FHIRToIJE `constants`

##### Summary

FHIR -> IJE Mapping for ContributoryTobaccoUse

<a name='F-VRDR-Mappings-ContributoryTobaccoUse-IJEToFHIR'></a>
### IJEToFHIR `constants`

##### Summary

IJE -> FHIR Mapping for ContributoryTobaccoUse

<a name='F-VRDR-ValueSets-ContributoryTobaccoUse-Codes'></a>
### Codes `constants`

##### Summary

Codes

<a name='F-VRDR-ValueSets-ContributoryTobaccoUse-No'></a>
### No `constants`

##### Summary

No

<a name='F-VRDR-ValueSets-ContributoryTobaccoUse-No_Information'></a>
### No_Information `constants`

##### Summary

No_Information

<a name='F-VRDR-ValueSets-ContributoryTobaccoUse-Probably'></a>
### Probably `constants`

##### Summary

Probably

<a name='F-VRDR-ValueSets-ContributoryTobaccoUse-Unknown'></a>
### Unknown `constants`

##### Summary

Unknown

<a name='F-VRDR-ValueSets-ContributoryTobaccoUse-Yes'></a>
### Yes `constants`

##### Summary

Yes

<a name='T-VRDR-DeathRecord'></a>
## DeathRecord `type`

##### Namespace

VRDR

##### Summary

Class `DeathRecord` models a FHIR Vital Records Death Reporting (VRDR) Death
Record. This class was designed to help consume and produce death records that follow the
HL7 FHIR Vital Records Death Reporting Implementation Guide, as described at:
http://hl7.org/fhir/us/vrdr and https://github.com/hl7/vrdr.

<a name='M-VRDR-DeathRecord-#ctor'></a>
### #ctor() `constructor`

##### Summary

Default constructor that creates a new, empty DeathRecord.

##### Parameters

This constructor has no parameters.

<a name='M-VRDR-DeathRecord-#ctor-System-String,System-Boolean-'></a>
### #ctor(record,permissive) `constructor`

##### Summary

Constructor that takes a string that represents a FHIR Death Record in either XML or JSON format.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| record | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | represents a FHIR Death Record in either XML or JSON format. |
| permissive | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | if the parser should be permissive when parsing the given string |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.ArgumentException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.ArgumentException 'System.ArgumentException') | Record is neither valid XML nor JSON. |

<a name='M-VRDR-DeathRecord-#ctor-Hl7-Fhir-Model-Bundle-'></a>
### #ctor(bundle) `constructor`

##### Summary

Constructor that takes a FHIR Bundle that represents a FHIR Death Record.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| bundle | [Hl7.Fhir.Model.Bundle](#T-Hl7-Fhir-Model-Bundle 'Hl7.Fhir.Model.Bundle') | represents a FHIR Bundle. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.ArgumentException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.ArgumentException 'System.ArgumentException') | Record is invalid. |

<a name='F-VRDR-DeathRecord-ActivityAtTimeOfDeathObs'></a>
### ActivityAtTimeOfDeathObs `constants`

##### Summary

Activity at Time of Death

<a name='F-VRDR-DeathRecord-AgeAtDeathObs'></a>
### AgeAtDeathObs `constants`

##### Summary

Age At Death.

<a name='F-VRDR-DeathRecord-AutomatedUnderlyingCauseOfDeathObs'></a>
### AutomatedUnderlyingCauseOfDeathObs `constants`

##### Summary

Automated Underlying Cause of Death

<a name='F-VRDR-DeathRecord-AutopsyPerformed'></a>
### AutopsyPerformed `constants`

##### Summary

Autopsy Performed.

<a name='F-VRDR-DeathRecord-BirthRecordIdentifier'></a>
### BirthRecordIdentifier `constants`

##### Summary

Birth Record Identifier.

<a name='F-VRDR-DeathRecord-BlankPlaceholder'></a>
### BlankPlaceholder `constants`

##### Summary

String to represent a blank value when an empty string is not allowed

<a name='F-VRDR-DeathRecord-Bundle'></a>
### Bundle `constants`

##### Summary

Bundle that contains the death record.

<a name='F-VRDR-DeathRecord-CauseOfDeathConditionA'></a>
### CauseOfDeathConditionA `constants`

##### Summary

Cause Of Death Condition Line A (#1).

<a name='F-VRDR-DeathRecord-CauseOfDeathConditionB'></a>
### CauseOfDeathConditionB `constants`

##### Summary

Cause Of Death Condition Line B (#2).

<a name='F-VRDR-DeathRecord-CauseOfDeathConditionC'></a>
### CauseOfDeathConditionC `constants`

##### Summary

Cause Of Death Condition Line C (#3).

<a name='F-VRDR-DeathRecord-CauseOfDeathConditionD'></a>
### CauseOfDeathConditionD `constants`

##### Summary

Cause Of Death Condition Line D (#4).

<a name='F-VRDR-DeathRecord-Certifier'></a>
### Certifier `constants`

##### Summary

The Certifier.

<a name='F-VRDR-DeathRecord-CodedRaceAndEthnicityObs'></a>
### CodedRaceAndEthnicityObs `constants`

##### Summary

The Decedent's Race and Ethnicity provided by Jurisdiction.

<a name='F-VRDR-DeathRecord-CodingStatusValues'></a>
### CodingStatusValues `constants`

##### Summary

Coding Status

<a name='F-VRDR-DeathRecord-Composition'></a>
### Composition `constants`

##### Summary

Composition that described what the Bundle is, as well as keeping references to its contents.

<a name='F-VRDR-DeathRecord-ConditionContributingToDeath'></a>
### ConditionContributingToDeath `constants`

##### Summary

Condition Contributing to Death.

<a name='F-VRDR-DeathRecord-DeathCertification'></a>
### DeathCertification `constants`

##### Summary

The Certification.

<a name='F-VRDR-DeathRecord-DeathDateObs'></a>
### DeathDateObs `constants`

##### Summary

Date Of Death.

<a name='F-VRDR-DeathRecord-DeathLocationLoc'></a>
### DeathLocationLoc `constants`

##### Summary

Death Location.

<a name='F-VRDR-DeathRecord-Decedent'></a>
### Decedent `constants`

##### Summary

The Decedent.

<a name='F-VRDR-DeathRecord-DecedentEducationLevel'></a>
### DecedentEducationLevel `constants`

##### Summary

Decedent Education Level.

<a name='F-VRDR-DeathRecord-DispositionLocation'></a>
### DispositionLocation `constants`

##### Summary

Disposition Location.

<a name='F-VRDR-DeathRecord-DispositionMethod'></a>
### DispositionMethod `constants`

##### Summary

Disposition Method.

<a name='F-VRDR-DeathRecord-EmergingIssues'></a>
### EmergingIssues `constants`

##### Summary

Emerging Issues.

<a name='F-VRDR-DeathRecord-EntityAxisCauseOfDeathObsList'></a>
### EntityAxisCauseOfDeathObsList `constants`

##### Summary

Entity Axis Cause of Death

<a name='F-VRDR-DeathRecord-ExaminerContactedObs'></a>
### ExaminerContactedObs `constants`

##### Summary

Examiner Contacted.

<a name='F-VRDR-DeathRecord-Father'></a>
### Father `constants`

##### Summary

Decedent's Father.

<a name='F-VRDR-DeathRecord-FuneralHome'></a>
### FuneralHome `constants`

##### Summary

The Funeral Home.

<a name='F-VRDR-DeathRecord-InjuryIncidentObs'></a>
### InjuryIncidentObs `constants`

##### Summary

Injury Incident.

<a name='F-VRDR-DeathRecord-InjuryLocationLoc'></a>
### InjuryLocationLoc `constants`

##### Summary

Injury Location.

<a name='F-VRDR-DeathRecord-InputRaceAndEthnicityObs'></a>
### InputRaceAndEthnicityObs `constants`

##### Summary

The Decedent's Race and Ethnicity provided by Jurisdiction.

<a name='F-VRDR-DeathRecord-MannerOfDeath'></a>
### MannerOfDeath `constants`

##### Summary

The Manner of Death Observation.

<a name='F-VRDR-DeathRecord-ManualUnderlyingCauseOfDeathObs'></a>
### ManualUnderlyingCauseOfDeathObs `constants`

##### Summary

Manual Underlying Cause of Death

<a name='F-VRDR-DeathRecord-MilitaryServiceObs'></a>
### MilitaryServiceObs `constants`

##### Summary

Whether the decedent served in the military

<a name='F-VRDR-DeathRecord-MortalityData'></a>
### MortalityData `constants`

##### Summary

Mortality data for code translations.

<a name='F-VRDR-DeathRecord-Mother'></a>
### Mother `constants`

##### Summary

Decedent's Mother.

<a name='F-VRDR-DeathRecord-Navigator'></a>
### Navigator `constants`

##### Summary

Useful for navigating around the FHIR Bundle using FHIRPaths.

<a name='F-VRDR-DeathRecord-PlaceOfInjuryObs'></a>
### PlaceOfInjuryObs `constants`

##### Summary

Place Of Injury

<a name='F-VRDR-DeathRecord-PregnancyObs'></a>
### PregnancyObs `constants`

##### Summary

Decedent Pregnancy Status.

<a name='F-VRDR-DeathRecord-RecordAxisCauseOfDeathObsList'></a>
### RecordAxisCauseOfDeathObsList `constants`

##### Summary

Record Axis Cause of Death

<a name='F-VRDR-DeathRecord-Spouse'></a>
### Spouse `constants`

##### Summary

Decedent's Spouse.

<a name='F-VRDR-DeathRecord-SurgeryDateObs'></a>
### SurgeryDateObs `constants`

##### Summary

Date Of Surgery.

<a name='F-VRDR-DeathRecord-TobaccoUseObs'></a>
### TobaccoUseObs `constants`

##### Summary

Tobacco Use Contributed To Death.

<a name='F-VRDR-DeathRecord-UsualWork'></a>
### UsualWork `constants`

##### Summary

Usual Work.

<a name='P-VRDR-DeathRecord-AcmeSystemReject'></a>
### AcmeSystemReject `property`

##### Summary

Acme System Reject.

##### Example

// Setter:

ExampleDeathRecord.AcmeSystemReject = 3;

// Getter:

Console.WriteLine($"Acme System Reject Code: {ExampleDeathRecord.AcmeSystemReject}");

<a name='P-VRDR-DeathRecord-AcmeSystemRejectHelper'></a>
### AcmeSystemRejectHelper `property`

##### Summary

Acme System Reject.

##### Example

// Setter:

ExampleDeathRecord.AcmeSystemReject = 3;

// Getter:

Console.WriteLine($"Acme System Reject Code: {ExampleDeathRecord.AcmeSystemReject}");

<a name='P-VRDR-DeathRecord-ActivityAtDeath'></a>
### ActivityAtDeath `property`

##### Summary

Activity at Time of Death.

##### Example

// Setter:

Dictionary<string, string> activity = new Dictionary<string, string>();

elevel.Add("code", "0");

elevel.Add("system", CodeSystems.ActivityAtTimeOfDeath);

elevel.Add("display", "While engaged in sports activity");

ExampleDeathRecord.ActivityAtDeath = activity;

// Getter:

Console.WriteLine($"Education Level: {ExampleDeathRecord.EducationLevel['display']}");

<a name='P-VRDR-DeathRecord-ActivityAtDeathHelper'></a>
### ActivityAtDeathHelper `property`

##### Summary

Decedent's Activity At Time of Death Helper

##### Example

// Setter:

ExampleDeathRecord.ActivityAtDeath = 0;

// Getter:

Console.WriteLine($"Decedent's Activity at Time of Death: {ExampleDeathRecord.ActivityAtDeath}");

<a name='P-VRDR-DeathRecord-AgeAtDeath'></a>
### AgeAtDeath `property`

##### Summary

Age At Death.

##### Example

// Setter:

Dictionary<string, string> age = new Dictionary<string, string>();

age.Add("value", "100");

age.Add("unit", "a"); // USE: http://hl7.org/fhir/us/vrdr/ValueSet/vrdr-units-of-age-vs

ExampleDeathRecord.AgeAtDeath = age;

// Getter:

Console.WriteLine($"Age At Death: {ExampleDeathRecord.AgeAtDeath['value']} years");

<a name='P-VRDR-DeathRecord-AgeAtDeathEditFlag'></a>
### AgeAtDeathEditFlag `property`

##### Summary

Decedent's Age At Death Edit Flag.

##### Example

// Setter:

Dictionary<string, string> ageEdit = new Dictionary<string, string>();

ageEdit.Add("code", "0");

ageEdit.Add("system", CodeSystems.BypassEditFlag);

ageEdit.Add("display", "Edit Passed");

ExampleDeathRecord.AgeAtDeathEditFlag = ageEdit;

// Getter:

Console.WriteLine($"Age At Death Edit Flag: {ExampleDeathRecord.AgeAtDeathEditFlag['display']}");

<a name='P-VRDR-DeathRecord-AgeAtDeathEditFlagHelper'></a>
### AgeAtDeathEditFlagHelper `property`

##### Summary

Age at Death Edit Bypass Flag Helper

<a name='P-VRDR-DeathRecord-AutomatedUnderlyingCOD'></a>
### AutomatedUnderlyingCOD `property`

##### Summary

Decedent's Automated Underlying Cause of Death

##### Example

// Setter:

ExampleDeathRecord.AutomatedUnderlyingCOD = "I13.1";

// Getter:

Console.WriteLine($"Decedent's Automated Underlying Cause of Death: {ExampleDeathRecord.AutomatedUnderlyingCOD}");

<a name='P-VRDR-DeathRecord-AutopsyPerformedIndicator'></a>
### AutopsyPerformedIndicator `property`

##### Summary

Autopsy Performed Indicator.

##### Example

// Setter:

Dictionary<string, string> code = new Dictionary<string, string>();

code.Add("code", "Y");

code.Add("system", CodeSystems.PH_YesNo_HL7_2x);

code.Add("display", "Yes");

ExampleDeathRecord.AutopsyPerformedIndicator = code;

// Getter:

Console.WriteLine($"Autopsy Performed Indicator: {ExampleDeathRecord.AutopsyPerformedIndicator['display']}");

<a name='P-VRDR-DeathRecord-AutopsyPerformedIndicatorHelper'></a>
### AutopsyPerformedIndicatorHelper `property`

##### Summary

Autopsy Performed Indicator Helper. This is a helper method, to access the code use the AutopsyPerformedIndicator property.

##### Example

// Setter:

ExampleDeathRecord.AutopsyPerformedIndicatorBoolean = true;

// Getter:

Console.WriteLine($"Autopsy Performed Indicator: {ExampleDeathRecord.AutopsyPerformedIndicatorBoolean}");

<a name='P-VRDR-DeathRecord-AutopsyResultsAvailable'></a>
### AutopsyResultsAvailable `property`

##### Summary

Autopsy Results Available.

##### Example

// Setter:

Dictionary<string, string> code = new Dictionary<string, string>();

code.Add("code", "Y");

code.Add("system", CodeSystems.PH_YesNo_HL7_2x);

code.Add("display", "Yes");

ExampleDeathRecord.AutopsyResultsAvailable = code;

// Getter:

Console.WriteLine($"Autopsy Results Available: {ExampleDeathRecord.AutopsyResultsAvailable['display']}");

<a name='P-VRDR-DeathRecord-AutopsyResultsAvailableHelper'></a>
### AutopsyResultsAvailableHelper `property`

##### Summary

Autopsy Results Available Helper. This is a convenience method, to access the coded value use AutopsyResultsAvailable.

##### Example

// Setter:

ExampleDeathRecord.AutopsyResultsAvailableHelper = "N";

// Getter:

Console.WriteLine($"Autopsy Results Available: {ExampleDeathRecord.AutopsyResultsAvailableHelper}");

<a name='P-VRDR-DeathRecord-BirthDay'></a>
### BirthDay `property`

##### Summary

Decedent's Day of Birth.

##### Example

// Setter:

ExampleDeathRecord.BirthDay = 11;

// Getter:

Console.WriteLine($"Decedent Day of Birth: {ExampleDeathRecord.BirthDay}");

<a name='P-VRDR-DeathRecord-BirthMonth'></a>
### BirthMonth `property`

##### Summary

Decedent's Month of Birth.

##### Example

// Setter:

ExampleDeathRecord.BirthMonth = 11;

// Getter:

Console.WriteLine($"Decedent Month of Birth: {ExampleDeathRecord.BirthMonth}");

<a name='P-VRDR-DeathRecord-BirthRecordId'></a>
### BirthRecordId `property`

##### Summary

Birth Record Identifier.

##### Example

// Setter:

ExampleDeathRecord.BirthRecordId = "4242123";

// Getter:

Console.WriteLine($"Birth Record identification: {ExampleDeathRecord.BirthRecordId}");

<a name='P-VRDR-DeathRecord-BirthRecordState'></a>
### BirthRecordState `property`

##### Summary

Birth Record State.

##### Example

// Setter:

Dictionary<string, string> brs = new Dictionary<string, string>();

brs.Add("code", "US-MA");

brs.Add("system", "urn:iso:std:iso:3166:-2");

brs.Add("display", "Massachusetts");

ExampleDeathRecord.BirthRecordState = brs;

// Getter:

Console.WriteLine($"Birth Record identification: {ExampleDeathRecord.BirthRecordState}");

<a name='P-VRDR-DeathRecord-BirthRecordYear'></a>
### BirthRecordYear `property`

##### Summary

Birth Record Year.

##### Example

// Setter:

ExampleDeathRecord.BirthRecordYear = "1940";

// Getter:

Console.WriteLine($"Birth Record year: {ExampleDeathRecord.BirthRecordYear}");

<a name='P-VRDR-DeathRecord-BirthYear'></a>
### BirthYear `property`

##### Summary

Decedent's Year of Birth.

##### Example

// Setter:

ExampleDeathRecord.BirthYear = 1928;

// Getter:

Console.WriteLine($"Decedent Year of Birth: {ExampleDeathRecord.BirthYear}");

<a name='P-VRDR-DeathRecord-COD1A'></a>
### COD1A `property`

##### Summary

Cause of Death Part I, Line a.

##### Example

// Setter:

ExampleDeathRecord.COD1A = "Rupture of myocardium";

// Getter:

Console.WriteLine($"Cause: {ExampleDeathRecord.COD1A}");

<a name='P-VRDR-DeathRecord-COD1B'></a>
### COD1B `property`

##### Summary

Cause of Death Part I, Line b.

##### Example

// Setter:

ExampleDeathRecord.COD1B = "Acute myocardial infarction";

// Getter:

Console.WriteLine($"Cause: {ExampleDeathRecord.COD1B}");

<a name='P-VRDR-DeathRecord-COD1C'></a>
### COD1C `property`

##### Summary

Cause of Death Part I, Line c.

##### Example

// Setter:

ExampleDeathRecord.COD1C = "Coronary artery thrombosis";

// Getter:

Console.WriteLine($"Cause: {ExampleDeathRecord.COD1C}");

<a name='P-VRDR-DeathRecord-COD1D'></a>
### COD1D `property`

##### Summary

Cause of Death Part I, Line d.

##### Example

// Setter:

ExampleDeathRecord.COD1D = "Atherosclerotic coronary artery disease";

// Getter:

Console.WriteLine($"Cause: {ExampleDeathRecord.COD1D}");

<a name='P-VRDR-DeathRecord-CausesOfDeath'></a>
### CausesOfDeath `property`

##### Summary

Conditions that resulted in the cause of death. Corresponds to part 1 of item 32 of the U.S.
Standard Certificate of Death.

##### Example

// Setter:

Tuple<string, string>[] causes =

{

Tuple.Create("Example Immediate COD", "minutes"),

Tuple.Create("Example Underlying COD 1", "2 hours"),

Tuple.Create("Example Underlying COD 2", "6 months"),

Tuple.Create("Example Underlying COD 3", "15 years")

};

ExampleDeathRecord.CausesOfDeath = causes;

// Getter:

Tuple<string, string>[] causes = ExampleDeathRecord.CausesOfDeath;

foreach (var cause in causes)

{

Console.WriteLine($"Cause: {cause.Item1}, Onset: {cause.Item2}");

}

<a name='P-VRDR-DeathRecord-CertificationRole'></a>
### CertificationRole `property`

##### Summary

Certification Role.

##### Example

// Setter:

Dictionary<string, string> role = new Dictionary<string, string>();

role.Add("code", "76899008");

role.Add("system", CodeSystems.SCT);

role.Add("display", "Infectious diseases physician");

ExampleDeathRecord.CertificationRole = role;

// Getter:

Console.WriteLine($"Certification Role: {ExampleDeathRecord.CertificationRole['display']}");

<a name='P-VRDR-DeathRecord-CertificationRoleHelper'></a>
### CertificationRoleHelper `property`

##### Summary

Certification Role Helper.

##### Example

// Setter:

ExampleDeathRecord.CertificationRoleHelper = ValueSets.CertificationRole.InfectiousDiseasesPhysician;

// Getter:

Console.WriteLine($"Certification Role: {ExampleDeathRecord.CertificationRoleHelper}");

<a name='P-VRDR-DeathRecord-CertifiedTime'></a>
### CertifiedTime `property`

##### Summary

Certified time.

##### Example

// Setter:

ExampleDeathRecord.CertifiedTime = "2019-01-29T16:48:06-05:00";

// Getter:

Console.WriteLine($"Certified at: {ExampleDeathRecord.CertifiedTime}");

<a name='P-VRDR-DeathRecord-CertifierAddress'></a>
### CertifierAddress `property`

##### Summary

Certifier's Address.

##### Example

// Setter:

Dictionary<string, string> address = new Dictionary<string, string>();

address.Add("addressLine1", "123 Test Street");

address.Add("addressLine2", "Unit 3");

address.Add("addressCity", "Boston");

address.Add("addressCounty", "Suffolk");

address.Add("addressState", "MA");

address.Add("addressZip", "12345");

address.Add("addressCountry", "US");

ExampleDeathRecord.CertifierAddress = address;

// Getter:

foreach(var pair in ExampleDeathRecord.CertifierAddress)

{

Console.WriteLine($"\tCertifierAddress key: {pair.Key}: value: {pair.Value}");

};

<a name='P-VRDR-DeathRecord-CertifierFamilyName'></a>
### CertifierFamilyName `property`

##### Summary

Family name of certifier.

##### Example

// Setter:

ExampleDeathRecord.CertifierFamilyName = "Last";

// Getter:

Console.WriteLine($"Certifier's Last Name: {ExampleDeathRecord.CertifierFamilyName}");

<a name='P-VRDR-DeathRecord-CertifierGivenNames'></a>
### CertifierGivenNames `property`

##### Summary

Given name(s) of certifier.

##### Example

// Setter:

string[] names = { "Doctor", "Middle" };

ExampleDeathRecord.CertifierGivenNames = names;

// Getter:

Console.WriteLine($"Certifier Given Name(s): {string.Join(", ", ExampleDeathRecord.CertifierGivenNames)}");

<a name='P-VRDR-DeathRecord-CertifierIdentifier'></a>
### CertifierIdentifier `property`

##### Summary

Certifier Identifier ** not mapped to IJE **.

##### Example

// Setter:

Dictionary<string, string> identifier = new Dictionary<string, string>();

identifier.Add("system", "http://hl7.org/fhir/sid/us-npi");

identifier.Add("value", "1234567890");

ExampleDeathRecord.CertifierIdentifier = identifier;

// Getter:

Console.WriteLine($"\tCertifier Identifier: {ExampleDeathRecord.CertifierIdentifier['value']}");

<a name='P-VRDR-DeathRecord-CertifierSuffix'></a>
### CertifierSuffix `property`

##### Summary

Certifier's Suffix.

##### Example

// Setter:

ExampleDeathRecord.CertifierSuffix = "Jr.";

// Getter:

Console.WriteLine($"Certifier Suffix: {ExampleDeathRecord.CertifierSuffix}");

<a name='P-VRDR-DeathRecord-CoderStatus'></a>
### CoderStatus `property`

##### Summary

Coder Status; TRX field with no IJE mapping

##### Example

// Setter:

ExampleDeathRecord.CoderStatus = 3;

// Getter:

Console.WriteLine($"Coder STatus {ExampleDeathRecord.CoderStatus}");

<a name='P-VRDR-DeathRecord-ContactRelationship'></a>
### ContactRelationship `property`

##### Summary

The informant of the decedent's death.

##### Example

// Setter:

ExampleDeathRecord.Contact = "Friend of family";

// Getter:

Console.WriteLine($"Contact's Relationship: {ExampleDeathRecord.Contact}");

<a name='P-VRDR-DeathRecord-ContributingConditions'></a>
### ContributingConditions `property`

##### Summary

Significant conditions that contributed to death but did not result in the underlying cause.
Corresponds to part 2 of item 32 of the U.S. Standard Certificate of Death.

##### Example

// Setter:

ExampleDeathRecord.ContributingConditions = "Example Contributing Condition";

// Getter:

Console.WriteLine($"Cause: {ExampleDeathRecord.ContributingConditions}");

<a name='P-VRDR-DeathRecord-DateOfBirth'></a>
### DateOfBirth `property`

##### Summary

Decedent's Date of Birth.

##### Example

// Setter:

ExampleDeathRecord.DateOfBirth = "1940-02-19";

// Getter:

Console.WriteLine($"Decedent Date of Birth: {ExampleDeathRecord.DateOfBirth}");

<a name='P-VRDR-DeathRecord-DateOfDeath'></a>
### DateOfDeath `property`

##### Summary

Decedent's Date/Time of Death.

##### Example

// Setter:

ExampleDeathRecord.DateOfDeath = "2018-02-19T16:48:06-05:00";

// Getter:

Console.WriteLine($"Decedent Date of Death: {ExampleDeathRecord.DateOfDeath}");

<a name='P-VRDR-DeathRecord-DateOfDeathPronouncement'></a>
### DateOfDeathPronouncement `property`

##### Summary

Decedent's Date/Time of Death Pronouncement.

##### Example

// Setter:

ExampleDeathRecord.DateOfDeathPronouncement = "2018-02-20T16:48:06-05:00";

// Getter:

Console.WriteLine($"Decedent Date of Death Pronouncement: {ExampleDeathRecord.DateOfDeathPronouncement}");

<a name='P-VRDR-DeathRecord-DeathDay'></a>
### DeathDay `property`

##### Summary

Decedent's Day of Death.

##### Example

// Setter:

ExampleDeathRecord.DeathDay = 16;

// Getter:

Console.WriteLine($"Decedent Day of Death: {ExampleDeathRecord.DeathDay}");

<a name='P-VRDR-DeathRecord-DeathLocationAddress'></a>
### DeathLocationAddress `property`

##### Summary

Location of Death.

##### Example

// Setter:

Dictionary<string, string> address = new Dictionary<string, string>();

address.Add("addressLine1", "123456789 Test Street");

address.Add("addressLine2", "Unit 3");

address.Add("addressCity", "Boston");

address.Add("addressCounty", "Suffolk");

address.Add("addressState", "MA");

address.Add("addressZip", "12345");

address.Add("addressCountry", "US");

ExampleDeathRecord.DeathLocationAddress = address;

// Getter:

foreach(var pair in ExampleDeathRecord.DeathLocationAddress)

{

Console.WriteLine($"\DeathLocationAddress key: {pair.Key}: value: {pair.Value}");

};

<a name='P-VRDR-DeathRecord-DeathLocationDescription'></a>
### DeathLocationDescription `property`

##### Summary

Description of Death Location.

##### Example

// Setter:

ExampleDeathRecord.DeathLocationDescription = "Bedford Cemetery";

// Getter:

Console.WriteLine($"Death Location Description: {ExampleDeathRecord.DeathLocationDescription}");

<a name='P-VRDR-DeathRecord-DeathLocationJurisdiction'></a>
### DeathLocationJurisdiction `property`

##### Summary

Death Location Jurisdiction.

##### Example

// Setter:

ExampleDeathRecord.DeathLocationJurisdiction = "MA";

// Getter:

Console.WriteLine($"Death Location Jurisdiction: {ExampleDeathRecord.DeathLocationJurisdiction}");

<a name='P-VRDR-DeathRecord-DeathLocationLatitude'></a>
### DeathLocationLatitude `property`

##### Summary

Lattitude of Death Location.

##### Example

// Setter:

ExampleDeathRecord.DeathLocationLattitude = "37.88888" ;

// Getter:

Console.WriteLine($"Death Location Lattitude: {ExampleDeathRecord.DeathLocationLattitude}");

<a name='P-VRDR-DeathRecord-DeathLocationLongitude'></a>
### DeathLocationLongitude `property`

##### Summary

Longitude of Death Location.

##### Example

// Setter:

ExampleDeathRecord.DeathLocationLongitude = "-50.000" ;

// Getter:

Console.WriteLine($"Death Location Longitude: {ExampleDeathRecord.DeathLocationLongitude}");

<a name='P-VRDR-DeathRecord-DeathLocationName'></a>
### DeathLocationName `property`

##### Summary

Name of Death Location.

##### Example

// Setter:

ExampleDeathRecord.DeathLocationName = "Example Death Location Name";

// Getter:

Console.WriteLine($"Death Location Name: {ExampleDeathRecord.DeathLocationName}");

<a name='P-VRDR-DeathRecord-DeathLocationType'></a>
### DeathLocationType `property`

##### Summary

Type of Death Location

##### Example

// Setter:

Dictionary<string, string> code = new Dictionary<string, string>();

code.Add("code", "16983000");

code.Add("system", CodeSystems.SCT);

code.Add("display", "Death in hospital");

ExampleDeathRecord.DeathLocationType = code;

// Getter:

Console.WriteLine($"Death Location Type: {ExampleDeathRecord.DeathLocationType['display']}");

<a name='P-VRDR-DeathRecord-DeathLocationTypeHelper'></a>
### DeathLocationTypeHelper `property`

##### Summary

Type of Death Location Helper

##### Example

// Setter:

ExampleDeathRecord.DeathLocationTypeHelper = "16983000";

// Getter:

Console.WriteLine($"Death Location Type: {ExampleDeathRecord.DeathLocationTypeHelper}");

<a name='P-VRDR-DeathRecord-DeathMonth'></a>
### DeathMonth `property`

##### Summary

Decedent's Month of Death.

##### Example

// Setter:

ExampleDeathRecord.DeathMonth = 6;

// Getter:

Console.WriteLine($"Decedent Month of Death: {ExampleDeathRecord.DeathMonth}");

<a name='P-VRDR-DeathRecord-DeathRecordIdentifier'></a>
### DeathRecordIdentifier `property`

##### Summary

Death Record Bundle Identifier, NCHS identifier.

##### Example

// Getter:

Console.WriteLine($"NCHS identifier: {ExampleDeathRecord.DeathRecordIdentifier}");

<a name='P-VRDR-DeathRecord-DeathTime'></a>
### DeathTime `property`

##### Summary

Decedent's Time of Death.

##### Example

// Setter:

ExampleDeathRecord.DeathTime = "07:15";

// Getter:

Console.WriteLine($"Decedent Time of Death: {ExampleDeathRecord.DeathTime}");

<a name='P-VRDR-DeathRecord-DeathYear'></a>
### DeathYear `property`

##### Summary

Decedent's Year of Death.

##### Example

// Setter:

ExampleDeathRecord.DeathYear = 2018;

// Getter:

Console.WriteLine($"Decedent Year of Death: {ExampleDeathRecord.DeathYear}");

<a name='P-VRDR-DeathRecord-DecedentDispositionMethod'></a>
### DecedentDispositionMethod `property`

##### Summary

Decedent's Disposition Method.

##### Example

// Setter:

Dictionary<string, string> dmethod = new Dictionary<string, string>();

dmethod.Add("code", "449971000124106");

dmethod.Add("system", CodeSystems.SCT);

dmethod.Add("display", "Burial");

ExampleDeathRecord.DecedentDispositionMethod = dmethod;

// Getter:

Console.WriteLine($"Decedent Disposition Method: {ExampleDeathRecord.DecedentDispositionMethod['display']}");

<a name='P-VRDR-DeathRecord-DecedentDispositionMethodHelper'></a>
### DecedentDispositionMethodHelper `property`

##### Summary

Decedent's Disposition Method Helper.

<a name='P-VRDR-DeathRecord-DispositionLocationAddress'></a>
### DispositionLocationAddress `property`

##### Summary

Disposition Location Address.

##### Example

// Setter:

Dictionary<string, string> address = new Dictionary<string, string>();

address.Add("addressLine1", "1234 Test Street");

address.Add("addressLine2", "Unit 3");

address.Add("addressCity", "Boston");

address.Add("addressCounty", "Suffolk");

address.Add("addressState", "MA");

address.Add("addressZip", "12345");

address.Add("addressCountry", "US");

ExampleDeathRecord.DispositionLocationAddress = address;

// Getter:

foreach(var pair in ExampleDeathRecord.DispositionLocationAddress)

{

Console.WriteLine($"\DispositionLocationAddress key: {pair.Key}: value: {pair.Value}");

};

<a name='P-VRDR-DeathRecord-DispositionLocationName'></a>
### DispositionLocationName `property`

##### Summary

Name of Disposition Location.

##### Example

// Setter:

ExampleDeathRecord.DispositionLocationName = "Bedford Cemetery";

// Getter:

Console.WriteLine($"Disposition Location Name: {ExampleDeathRecord.DispositionLocationName}");

<a name='P-VRDR-DeathRecord-EducationLevel'></a>
### EducationLevel `property`

##### Summary

Decedent's Education Level.

##### Example

// Setter:

Dictionary<string, string> elevel = new Dictionary<string, string>();

elevel.Add("code", "BA");

elevel.Add("system", CodeSystems.EducationLevel);

elevel.Add("display", "Bachelor’s Degree");

ExampleDeathRecord.EducationLevel = elevel;

// Getter:

Console.WriteLine($"Education Level: {ExampleDeathRecord.EducationLevel['display']}");

<a name='P-VRDR-DeathRecord-EducationLevelEditFlag'></a>
### EducationLevelEditFlag `property`

##### Summary

Decedent's Education Level Edit Flag.

##### Example

// Setter:

Dictionary<string, string> elevel = new Dictionary<string, string>();

elevel.Add("code", "0");

elevel.Add("system", CodeSystems.BypassEditFlag);

elevel.Add("display", "Edit Passed");

ExampleDeathRecord.EducationLevelEditFlag = elevel;

// Getter:

Console.WriteLine($"Education Level Edit Flag: {ExampleDeathRecord.EducationLevelEditFlag['display']}");

<a name='P-VRDR-DeathRecord-EducationLevelEditFlagHelper'></a>
### EducationLevelEditFlagHelper `property`

##### Summary

Decedent's Education Level Edit Flag Helper

##### Example

// Setter:

ExampleDeathRecord.DecedentEducationLevelEditFlag = VRDR.ValueSets.EditBypass01234.EditPassed;

// Getter:

Console.WriteLine($"Decedent's Education Level Edit Flag: {ExampleDeathRecord.EducationLevelHelperEditFlag}");

<a name='P-VRDR-DeathRecord-EducationLevelHelper'></a>
### EducationLevelHelper `property`

##### Summary

Decedent's Education Level Helper

##### Example

// Setter:

ExampleDeathRecord.DecedentEducationLevel = VRDR.ValueSets.EducationLevel.Bachelors_Degree;

// Getter:

Console.WriteLine($"Decedent's Education Level: {ExampleDeathRecord.EducationLevelHelper}");

<a name='P-VRDR-DeathRecord-EighthEditedRaceCode'></a>
### EighthEditedRaceCode `property`

##### Summary

Eighth Edited Race Code.

##### Example

// Setter:

Dictionary<string, string> racecode = new Dictionary<string, string>();

racecode.Add("code", "300");

racecode.Add("system", CodeSystems.RaceCode);

racecode.Add("display", "African");

ExampleDeathRecord.EighthEditedRaceCode = racecode;

// Getter:

Console.WriteLine($"Eighth Edited Race Code: {ExampleDeathRecord.EighthEditedRaceCode['display']}");

<a name='P-VRDR-DeathRecord-EighthEditedRaceCodeHelper'></a>
### EighthEditedRaceCodeHelper `property`

##### Summary

Eighth Edited Race Code  Helper

##### Example

// Setter:

ExampleDeathRecord.EighthEditedRaceCodeHelper = VRDR.ValueSets.RaceCode.African ;

// Getter:

Console.WriteLine($"Eighth Edited Race Code: {ExampleDeathRecord.EighthEditedRaceCodeHelper}");

<a name='P-VRDR-DeathRecord-EmergingIssue1_1'></a>
### EmergingIssue1_1 `property`

##### Summary

Emerging Issue Field Length 1 Number 1

##### Example

// Setter:

ExampleDeathRecord.EmergingIssue1_1 = "X";

// Getter:

Console.WriteLine($"Emerging Issue Value: {ExampleDeathRecord.EmergingIssue1_1}");

<a name='P-VRDR-DeathRecord-EmergingIssue1_2'></a>
### EmergingIssue1_2 `property`

##### Summary

Emerging Issue Field Length 1 Number 2

##### Example

// Setter:

ExampleDeathRecord.EmergingIssue1_2 = "X";

// Getter:

Console.WriteLine($"Emerging Issue Value: {ExampleDeathRecord.EmergingIssue1_2}");

<a name='P-VRDR-DeathRecord-EmergingIssue1_3'></a>
### EmergingIssue1_3 `property`

##### Summary

Emerging Issue Field Length 1 Number 3

##### Example

// Setter:

ExampleDeathRecord.EmergingIssue1_3 = "X";

// Getter:

Console.WriteLine($"Emerging Issue Value: {ExampleDeathRecord.EmergingIssue1_3}");

<a name='P-VRDR-DeathRecord-EmergingIssue1_4'></a>
### EmergingIssue1_4 `property`

##### Summary

Emerging Issue Field Length 1 Number 4

##### Example

// Setter:

ExampleDeathRecord.EmergingIssue1_4 = "X";

// Getter:

Console.WriteLine($"Emerging Issue Value: {ExampleDeathRecord.EmergingIssue1_4}");

<a name='P-VRDR-DeathRecord-EmergingIssue1_5'></a>
### EmergingIssue1_5 `property`

##### Summary

Emerging Issue Field Length 1 Number 5

##### Example

// Setter:

ExampleDeathRecord.EmergingIssue1_5 = "X";

// Getter:

Console.WriteLine($"Emerging Issue Value: {ExampleDeathRecord.EmergingIssue1_5}");

<a name='P-VRDR-DeathRecord-EmergingIssue1_6'></a>
### EmergingIssue1_6 `property`

##### Summary

Emerging Issue Field Length 1 Number 6

##### Example

// Setter:

ExampleDeathRecord.EmergingIssue1_6 = "X";

// Getter:

Console.WriteLine($"Emerging Issue Value: {ExampleDeathRecord.EmergingIssue1_6}");

<a name='P-VRDR-DeathRecord-EmergingIssue20'></a>
### EmergingIssue20 `property`

##### Summary

Emerging Issue Field Length 20

##### Example

// Setter:

ExampleDeathRecord.EmergingIssue20 = "XXXXXXXXXXXXXXXXXXXX";

// Getter:

Console.WriteLine($"Emerging Issue Value: {ExampleDeathRecord.EmergingIssue20}");

<a name='P-VRDR-DeathRecord-EmergingIssue8_1'></a>
### EmergingIssue8_1 `property`

##### Summary

Emerging Issue Field Length 8 Number 1

##### Example

// Setter:

ExampleDeathRecord.EmergingIssue8_1 = "XXXXXXXX";

// Getter:

Console.WriteLine($"Emerging Issue Value: {ExampleDeathRecord.EmergingIssue8_1}");

<a name='P-VRDR-DeathRecord-EmergingIssue8_2'></a>
### EmergingIssue8_2 `property`

##### Summary

Emerging Issue Field Length 8 Number 2

##### Example

// Setter:

ExampleDeathRecord.EmergingIssue8_2 = "XXXXXXXX";

// Getter:

Console.WriteLine($"Emerging Issue Value: {ExampleDeathRecord.EmergingIssue8_2}");

<a name='P-VRDR-DeathRecord-EmergingIssue8_3'></a>
### EmergingIssue8_3 `property`

##### Summary

Emerging Issue Field Length 8 Number 3

##### Example

// Setter:

ExampleDeathRecord.EmergingIssue8_3 = "XXXXXXXX";

// Getter:

Console.WriteLine($"Emerging Issue Value: {ExampleDeathRecord.EmergingIssue8_3}");

<a name='P-VRDR-DeathRecord-EntityAxisCauseOfDeath'></a>
### EntityAxisCauseOfDeath `property`

##### Summary

Entity Axis Cause Of Death

Note that record axis codes have an unusual and obscure handling of a Pregnancy flag, for more information see
http://build.fhir.org/ig/HL7/vrdr/branches/master/StructureDefinition-vrdr-record-axis-cause-of-death.html#usage>

##### Example

// Setter:

ExampleDeathRecord.EntityAxisCauseOfDeath = new [] {(LineNumber: 2, Position: 1, Code: "T27.3", ECode: true)};

// Getter:

Console.WriteLine($"First Entity Axis Code: {ExampleDeathRecord.EntityAxisCauseOfDeath.ElementAt(0).Code}");

<a name='P-VRDR-DeathRecord-Ethnicity1'></a>
### Ethnicity1 `property`

##### Summary

Decedent's Ethnicity Hispanic Mexican.

##### Example

// Setter:

Dictionary<string, string> ethnicity = new Dictionary<string, string>();

ethnicity.Add("code", "Y");

ethnicity.Add("system", CodeSystems.YesNo);

ethnicity.Add("display", "Yes");

ExampleDeathRecord.Ethnicity = ethnicity;

// Getter:

Console.WriteLine($"Ethnicity: {ExampleDeathRecord.Ethnicity1['display']}");

<a name='P-VRDR-DeathRecord-Ethnicity1Helper'></a>
### Ethnicity1Helper `property`

##### Summary

Decedent's Ethnicity 1 Helper

##### Example

// Setter:

ExampleDeathRecord.EthnicityLevel = VRDR.ValueSets.YesNoUnknown.Yes;

// Getter:

Console.WriteLine($"Decedent's Ethnicity: {ExampleDeathRecord.Ethnicity1Helper}");

<a name='P-VRDR-DeathRecord-Ethnicity2'></a>
### Ethnicity2 `property`

##### Summary

Decedent's Ethnicity Hispanic Puerto Rican.

##### Example

// Setter:

Dictionary<string, string> ethnicity = new Dictionary<string, string>();

ethnicity.Add("code", "Y");

ethnicity.Add("system", CodeSystems.YesNo);

ethnicity.Add("display", "Yes");

ExampleDeathRecord.Ethnicity2 = ethnicity;

// Getter:

Console.WriteLine($"Ethnicity: {ExampleDeathRecord.Ethnicity2['display']}");

<a name='P-VRDR-DeathRecord-Ethnicity2Helper'></a>
### Ethnicity2Helper `property`

##### Summary

Decedent's Ethnicity 2 Helper

##### Example

// Setter:

ExampleDeathRecord.Ethnicity2Helper = "Y";

// Getter:

Console.WriteLine($"Decedent's Ethnicity: {ExampleDeathRecord.Ethnicity1Helper}");

<a name='P-VRDR-DeathRecord-Ethnicity3'></a>
### Ethnicity3 `property`

##### Summary

Decedent's Ethnicity Hispanic Cuban.

##### Example

// Setter:

Dictionary<string, string> ethnicity = new Dictionary<string, string>();

ethnicity.Add("code", "Y");

ethnicity.Add("system", CodeSystems.YesNo);

ethnicity.Add("display", "Yes");

ExampleDeathRecord.Ethnicity3 = ethnicity;

// Getter:

Console.WriteLine($"Ethnicity: {ExampleDeathRecord.Ethnicity3['display']}");

<a name='P-VRDR-DeathRecord-Ethnicity3Helper'></a>
### Ethnicity3Helper `property`

##### Summary

Decedent's Ethnicity 3 Helper

##### Example

// Setter:

ExampleDeathRecord.Ethnicity3Helper = "Y";

// Getter:

Console.WriteLine($"Decedent's Ethnicity: {ExampleDeathRecord.Ethnicity3Helper}");

<a name='P-VRDR-DeathRecord-Ethnicity4'></a>
### Ethnicity4 `property`

##### Summary

Decedent's Ethnicity Hispanic Other.

##### Example

// Setter:

Dictionary<string, string> ethnicity = new Dictionary<string, string>();

ethnicity.Add("code", "Y");

ethnicity.Add("system", CodeSystems.YesNo);

ethnicity.Add("display", "Yes");

ExampleDeathRecord.Ethnicity4 = ethnicity;

// Getter:

Console.WriteLine($"Ethnicity: {ExampleDeathRecord.Ethnicity4['display']}");

<a name='P-VRDR-DeathRecord-Ethnicity4Helper'></a>
### Ethnicity4Helper `property`

##### Summary

Decedent's Ethnicity 4 Helper

##### Example

// Setter:

ExampleDeathRecord.Ethnicity4Helper = "Y";

// Getter:

Console.WriteLine($"Decedent's Ethnicity: {ExampleDeathRecord.Ethnicity4Helper}");

<a name='P-VRDR-DeathRecord-EthnicityLiteral'></a>
### EthnicityLiteral `property`

##### Summary

Decedent's Ethnicity Hispanic Literal.

##### Example

// Setter:

ExampleDeathRecord.EthnicityLiteral = ethnicity;

// Getter:

Console.WriteLine($"Ethnicity: {ExampleDeathRecord.Ethnicity4['display']}");

<a name='P-VRDR-DeathRecord-ExaminerContacted'></a>
### ExaminerContacted `property`

##### Summary

Examiner Contacted.

##### Example

// Setter:

Dictionary<string, string> ec = new Dictionary<string, string>();

within.Add("code", "Y");

within.Add("system", CodeSystems.PH_YesNo_HL7_2x);

within.Add("display", "Yes");

ExampleDeathRecord.ExaminerContacted = ec;

// Getter:

Console.WriteLine($"Examiner Contacted: {ExampleDeathRecord.ExaminerContacted['display']}");

<a name='P-VRDR-DeathRecord-ExaminerContactedHelper'></a>
### ExaminerContactedHelper `property`

##### Summary

Examiner Contacted Boolean. This is a conenience method, to access the code use ExaminerContacted instead.

##### Example

// Setter:

ExampleDeathRecord.ExaminerContacted = false;

// Getter:

Console.WriteLine($"Examiner Contacted: {ExampleDeathRecord.ExaminerContacted}");

<a name='P-VRDR-DeathRecord-FamilyName'></a>
### FamilyName `property`

##### Summary

Decedent's Family Name.

##### Example

// Setter:

ExampleDeathRecord.FamilyName = "Last";

// Getter:

Console.WriteLine($"Decedent's Last Name: {ExampleDeathRecord.FamilyName}");

<a name='P-VRDR-DeathRecord-FatherFamilyName'></a>
### FatherFamilyName `property`

##### Summary

Family name of decedent's father.

##### Example

// Setter:

ExampleDeathRecord.FatherFamilyName = "Last";

// Getter:

Console.WriteLine($"Father's Last Name: {ExampleDeathRecord.FatherFamilyName}");

<a name='P-VRDR-DeathRecord-FatherGivenNames'></a>
### FatherGivenNames `property`

##### Summary

Given name(s) of decedent's father.

##### Example

// Setter:

string[] names = { "Dad", "Middle" };

ExampleDeathRecord.FatherGivenNames = names;

// Getter:

Console.WriteLine($"Father Given Name(s): {string.Join(", ", ExampleDeathRecord.FatherGivenNames)}");

<a name='P-VRDR-DeathRecord-FatherSuffix'></a>
### FatherSuffix `property`

##### Summary

Father's Suffix.

##### Example

// Setter:

ExampleDeathRecord.FatherSuffix = "Jr.";

// Getter:

Console.WriteLine($"Father Suffix: {ExampleDeathRecord.FatherSuffix}");

<a name='P-VRDR-DeathRecord-FifthEditedRaceCode'></a>
### FifthEditedRaceCode `property`

##### Summary

Fifth Edited Race Code.

##### Example

// Setter:

Dictionary<string, string> racecode = new Dictionary<string, string>();

racecode.Add("code", "300");

racecode.Add("system", CodeSystems.RaceCode);

racecode.Add("display", "African");

ExampleDeathRecord.FifthEditedRaceCode = racecode;

// Getter:

Console.WriteLine($"Fifth Edited Race Code: {ExampleDeathRecord.FifthEditedRaceCode['display']}");

<a name='P-VRDR-DeathRecord-FifthEditedRaceCodeHelper'></a>
### FifthEditedRaceCodeHelper `property`

##### Summary

Fifth Edited Race Code  Helper

##### Example

// Setter:

ExampleDeathRecord.FifthEditedRaceCodeHelper = VRDR.ValueSets.RaceCode.African ;

// Getter:

Console.WriteLine($"Fifth Edited Race Code: {ExampleDeathRecord.FifthEditedRaceCodeHelper}");

<a name='P-VRDR-DeathRecord-FilingFormat'></a>
### FilingFormat `property`

##### Summary

Filing Format.

##### Example

// Setter:

ExampleDeathRecord.FilingFormat = ValueSets.FilingFormat.electronic;

// Getter:

Console.WriteLine($"Filed method: {ExampleDeathRecord.FilingFormat}");

<a name='P-VRDR-DeathRecord-FilingFormatHelper'></a>
### FilingFormatHelper `property`

##### Summary

Filing Format Helper.

##### Example

// Setter:

ExampleDeathRecord.FilingFormatHelper = ValueSets.FilingFormat.Electronic;

// Getter:

Console.WriteLine($"Filing Format: {ExampleDeathRecord.FilingFormatHelper}");

<a name='P-VRDR-DeathRecord-FirstAmericanIndianRaceCode'></a>
### FirstAmericanIndianRaceCode `property`

##### Summary

First American Indian Race Code.

##### Example

// Setter:

Dictionary<string, string> racecode = new Dictionary<string, string>();

racecode.Add("code", "300");

racecode.Add("system", CodeSystems.RaceCode);

racecode.Add("display", "African");

ExampleDeathRecord.FirstAmericanIndianRaceCode = racecode;

// Getter:

Console.WriteLine($"First American Indian Race Code: {ExampleDeathRecord.FirstAmericanIndianRaceCode['display']}");

<a name='P-VRDR-DeathRecord-FirstAmericanIndianRaceCodeHelper'></a>
### FirstAmericanIndianRaceCodeHelper `property`

##### Summary

First American Indian Race Code  Helper

##### Example

// Setter:

ExampleDeathRecord.FirstAmericanIndianRaceCodeHelper = VRDR.ValueSets.RaceCode.African ;

// Getter:

Console.WriteLine($"First American Indian Race Code: {ExampleDeathRecord.FirstAmericanIndianRaceCodeHelper}");

<a name='P-VRDR-DeathRecord-FirstEditedRaceCode'></a>
### FirstEditedRaceCode `property`

##### Summary

First Edited Race Code.

##### Example

// Setter:

Dictionary<string, string> racecode = new Dictionary<string, string>();

racecode.Add("code", "300");

racecode.Add("system", CodeSystems.RaceCode);

racecode.Add("display", "African");

ExampleDeathRecord.FirstEditedRaceCode = racecode;

// Getter:

Console.WriteLine($"First Edited Race Code: {ExampleDeathRecord.FirstEditedRaceCode['display']}");

<a name='P-VRDR-DeathRecord-FirstEditedRaceCodeHelper'></a>
### FirstEditedRaceCodeHelper `property`

##### Summary

First Edited Race Code  Helper

##### Example

// Setter:

ExampleDeathRecord.FirstEditedRaceCodeHelper = VRDR.ValueSets.RaceCode.African ;

// Getter:

Console.WriteLine($"First Edited Race Code: {ExampleDeathRecord.FirstEditedRaceCodeHelper}");

<a name='P-VRDR-DeathRecord-FirstOtherAsianRaceCode'></a>
### FirstOtherAsianRaceCode `property`

##### Summary

First Other Asian Race Code.

##### Example

// Setter:

Dictionary<string, string> racecode = new Dictionary<string, string>();

racecode.Add("code", "300");

racecode.Add("system", CodeSystems.RaceCode);

racecode.Add("display", "African");

ExampleDeathRecord.FirstOtherAsianRaceCode = racecode;

// Getter:

Console.WriteLine($"First Other Asian Race Code: {ExampleDeathRecord.FirstOtherAsianRaceCode['display']}");

<a name='P-VRDR-DeathRecord-FirstOtherAsianRaceCodeHelper'></a>
### FirstOtherAsianRaceCodeHelper `property`

##### Summary

First Other Asian Race Code  Helper

##### Example

// Setter:

ExampleDeathRecord.FirstOtherAsianRaceCodeHelper = VRDR.ValueSets.RaceCode.African ;

// Getter:

Console.WriteLine($"First Other Asian Race Code: {ExampleDeathRecord.FirstOtherAsianRaceCodeHelper}");

<a name='P-VRDR-DeathRecord-FirstOtherPacificIslanderRaceCode'></a>
### FirstOtherPacificIslanderRaceCode `property`

##### Summary

First Other Pacific Islander Race Code.

##### Example

// Setter:

Dictionary<string, string> racecode = new Dictionary<string, string>();

racecode.Add("code", "300");

racecode.Add("system", CodeSystems.RaceCode);

racecode.Add("display", "African");

ExampleDeathRecord.FirstOtherPacificIslanderRaceCode = racecode;

// Getter:

Console.WriteLine($"First Other Pacific Islander Race Code: {ExampleDeathRecord.FirstOtherPacificIslanderRaceCode['display']}");

<a name='P-VRDR-DeathRecord-FirstOtherPacificIslanderRaceCodeHelper'></a>
### FirstOtherPacificIslanderRaceCodeHelper `property`

##### Summary

First Other Pacific Islander Race Code  Helper

##### Example

// Setter:

ExampleDeathRecord.FirstOtherPacificIslanderRaceCodeHelper = VRDR.ValueSets.RaceCode.African ;

// Getter:

Console.WriteLine($"First Other Pacific Islander Race Code: {ExampleDeathRecord.FirstOtherPacificIslanderRaceCodeHelper}");

<a name='P-VRDR-DeathRecord-FirstOtherRaceCode'></a>
### FirstOtherRaceCode `property`

##### Summary

First Other Race Code.

##### Example

// Setter:

Dictionary<string, string> racecode = new Dictionary<string, string>();

racecode.Add("code", "300");

racecode.Add("system", CodeSystems.RaceCode);

racecode.Add("display", "African");

ExampleDeathRecord.FirstOtherRaceCode = racecode;

// Getter:

Console.WriteLine($"First Other Race Code: {ExampleDeathRecord.FirstOtherRaceCode['display']}");

<a name='P-VRDR-DeathRecord-FirstOtherRaceCodeHelper'></a>
### FirstOtherRaceCodeHelper `property`

##### Summary

First Other Race Code  Helper

##### Example

// Setter:

ExampleDeathRecord.FirstOtherRaceCodeHelper = VRDR.ValueSets.RaceCode.African ;

// Getter:

Console.WriteLine($"First Other Race Code: {ExampleDeathRecord.FirstOtherRaceCodeHelper}");

<a name='P-VRDR-DeathRecord-FourthEditedRaceCode'></a>
### FourthEditedRaceCode `property`

##### Summary

Fourth Edited Race Code.

##### Example

// Setter:

Dictionary<string, string> racecode = new Dictionary<string, string>();

racecode.Add("code", "300");

racecode.Add("system", CodeSystems.RaceCode);

racecode.Add("display", "African");

ExampleDeathRecord.FourthEditedRaceCode = racecode;

// Getter:

Console.WriteLine($"Fourth Edited Race Code: {ExampleDeathRecord.FourthEditedRaceCode['display']}");

<a name='P-VRDR-DeathRecord-FourthEditedRaceCodeHelper'></a>
### FourthEditedRaceCodeHelper `property`

##### Summary

Fourth Edited Race Code  Helper

##### Example

// Setter:

ExampleDeathRecord.FourthEditedRaceCodeHelper = VRDR.ValueSets.RaceCode.African ;

// Getter:

Console.WriteLine($"Fourth Edited Race Code: {ExampleDeathRecord.FourthEditedRaceCodeHelper}");

<a name='P-VRDR-DeathRecord-FuneralHomeAddress'></a>
### FuneralHomeAddress `property`

##### Summary

Funeral Home Address.

##### Example

// Setter:

Dictionary<string, string> address = new Dictionary<string, string>();

address.Add("addressLine1", "1234 Test Street");

address.Add("addressLine2", "Unit 3");

address.Add("addressCity", "Boston");

address.Add("addressCounty", "Suffolk");

address.Add("addressState", "MA");

address.Add("addressZip", "12345");

address.Add("addressCountry", "US");

ExampleDeathRecord.FuneralHomeAddress = address;

// Getter:

foreach(var pair in ExampleDeathRecord.FuneralHomeAddress)

{

Console.WriteLine($"\FuneralHomeAddress key: {pair.Key}: value: {pair.Value}");

};

<a name='P-VRDR-DeathRecord-FuneralHomeName'></a>
### FuneralHomeName `property`

##### Summary

Name of Funeral Home.

##### Example

// Setter:

ExampleDeathRecord.FuneralHomeName = "Smith Funeral Home";

// Getter:

Console.WriteLine($"Funeral Home Name: {ExampleDeathRecord.FuneralHomeName}");

<a name='P-VRDR-DeathRecord-GivenNames'></a>
### GivenNames `property`

##### Summary

Decedent's Legal Name - Given. Middle name should be the last entry.

##### Example

// Setter:

string[] names = { "Example", "Something", "Middle" };

ExampleDeathRecord.GivenNames = names;

// Getter:

Console.WriteLine($"Decedent Given Name(s): {string.Join(", ", ExampleDeathRecord.GivenNames)}");

<a name='P-VRDR-DeathRecord-HispanicCode'></a>
### HispanicCode `property`

##### Summary

Hispanic Code.

##### Example

// Setter:

Dictionary<string, string> racecode = new Dictionary<string, string>();

racecode.Add("code", "300");

racecode.Add("system", CodeSystems.RaceCode);

racecode.Add("display", "African");

ExampleDeathRecord.HispanicCode = racecode;

// Getter:

Console.WriteLine($"Hispanic Code: {ExampleDeathRecord.HispanicCode['display']}");

<a name='P-VRDR-DeathRecord-HispanicCodeForLiteral'></a>
### HispanicCodeForLiteral `property`

##### Summary

Hispanic Code For Literal.

##### Example

// Setter:

Dictionary<string, string> racecode = new Dictionary<string, string>();

racecode.Add("code", "300");

racecode.Add("system", CodeSystems.RaceCode);

racecode.Add("display", "African");

ExampleDeathRecord.HispanicCodeForLiteral = racecode;

// Getter:

Console.WriteLine($"Hispanic Code For Literal: {ExampleDeathRecord.HispanicCodeForLiteral['display']}");

<a name='P-VRDR-DeathRecord-HispanicCodeForLiteralHelper'></a>
### HispanicCodeForLiteralHelper `property`

##### Summary

Hispanic Code For Literal  Helper

##### Example

// Setter:

ExampleDeathRecord.HispanicCodeForLiteralHelper = VRDR.ValueSets.RaceCode.African ;

// Getter:

Console.WriteLine($"Hispanic Code For Literal: {ExampleDeathRecord.HispanicCodeForLiteralHelper}");

<a name='P-VRDR-DeathRecord-HispanicCodeHelper'></a>
### HispanicCodeHelper `property`

##### Summary

Hispanic Code  Helper

##### Example

// Setter:

ExampleDeathRecord.HispanicCodeHelper = VRDR.ValueSets.RaceCode.African ;

// Getter:

Console.WriteLine($"Hispanic Code: {ExampleDeathRecord.HispanicCodeHelper}");

<a name='P-VRDR-DeathRecord-INTERVAL1A'></a>
### INTERVAL1A `property`

##### Summary

Cause of Death Part I Interval, Line a.

##### Example

// Setter:

ExampleDeathRecord.INTERVAL1A = "Minutes";

// Getter:

Console.WriteLine($"Interval: {ExampleDeathRecord.INTERVAL1A}");

<a name='P-VRDR-DeathRecord-INTERVAL1B'></a>
### INTERVAL1B `property`

##### Summary

Cause of Death Part I Interval, Line b.

##### Example

// Setter:

ExampleDeathRecord.INTERVAL1B = "6 days";

// Getter:

Console.WriteLine($"Interval: {ExampleDeathRecord.INTERVAL1B}");

<a name='P-VRDR-DeathRecord-INTERVAL1C'></a>
### INTERVAL1C `property`

##### Summary

Cause of Death Part I Interval, Line c.

##### Example

// Setter:

ExampleDeathRecord.INTERVAL1C = "5 years";

// Getter:

Console.WriteLine($"Interval: {ExampleDeathRecord.INTERVAL1C}");

<a name='P-VRDR-DeathRecord-INTERVAL1D'></a>
### INTERVAL1D `property`

##### Summary

Cause of Death Part I Interval, Line d.

##### Example

// Setter:

ExampleDeathRecord.INTERVAL1D = "7 years";

// Getter:

Console.WriteLine($"Interval: {ExampleDeathRecord.INTERVAL1D}");

<a name='P-VRDR-DeathRecord-Identifier'></a>
### Identifier `property`

##### Summary

Death Record Identifier, Death Certificate Number.

##### Example

// Setter:

ExampleDeathRecord.Identifier = "42";

// Getter:

Console.WriteLine($"Death Certificate Number: {ExampleDeathRecord.Identifier}");

<a name='P-VRDR-DeathRecord-InjuryAtWork'></a>
### InjuryAtWork `property`

##### Summary

Injury At Work?

##### Example

// Setter:

Dictionary<string, string> code = new Dictionary<string, string>();

code.Add("code", "N");

code.Add("system", CodeSystems.YesNo);

code.Add("display", "No");

ExampleDeathRecord.InjuryAtWork = code;

// Getter:

Console.WriteLine($"Injury At Work?: {ExampleDeathRecord.InjuryAtWork['display']}");

<a name='P-VRDR-DeathRecord-InjuryAtWorkHelper'></a>
### InjuryAtWorkHelper `property`

##### Summary

Injury At Work Helper This is a convenience method, to access the code use the InjuryAtWork property instead.

##### Example

// Setter:

ExampleDeathRecord.InjuryAtWorkHelper = true;

// Getter:

Console.WriteLine($"Injury At Work? : {ExampleDeathRecord.InjuryAtWorkHelper}");

<a name='P-VRDR-DeathRecord-InjuryDate'></a>
### InjuryDate `property`

##### Summary

Date/Time of Injury.

##### Example

// Setter:

ExampleDeathRecord.InjuryDate = "2018-02-19T16:48:06-05:00";

// Getter:

Console.WriteLine($"Date of Injury: {ExampleDeathRecord.InjuryDate}");

<a name='P-VRDR-DeathRecord-InjuryDay'></a>
### InjuryDay `property`

##### Summary

Decedent's Day of Injury.

##### Example

// Setter:

ExampleDeathRecord.InjuryDay = 22;

// Getter:

Console.WriteLine($"Decedent Day of Injury: {ExampleDeathRecord.InjuryDay}");

<a name='P-VRDR-DeathRecord-InjuryDescription'></a>
### InjuryDescription `property`

##### Summary

Description of Injury.

##### Example

// Setter:

ExampleDeathRecord.InjuryDescription = "drug toxicity";

// Getter:

Console.WriteLine($"Injury Description: {ExampleDeathRecord.InjuryDescription}");

<a name='P-VRDR-DeathRecord-InjuryLocationAddress'></a>
### InjuryLocationAddress `property`

##### Summary

Location of Injury.

##### Example

// Setter:

Dictionary<string, string> address = new Dictionary<string, string>();

address.Add("addressLine1", "123456 Test Street");

address.Add("addressLine2", "Unit 3");

address.Add("addressCity", "Boston");

address.Add("addressCounty", "Suffolk");

address.Add("addressState", "MA");

address.Add("addressZip", "12345");

address.Add("addressCountry", "US");

ExampleDeathRecord.InjuryLocationAddress = address;

// Getter:

foreach(var pair in ExampleDeathRecord.InjuryLocationAddress)

{

Console.WriteLine($"\InjuryLocationAddress key: {pair.Key}: value: {pair.Value}");

};

<a name='P-VRDR-DeathRecord-InjuryLocationLatitude'></a>
### InjuryLocationLatitude `property`

##### Summary

Lattitude of Injury Location.

##### Example

// Setter:

ExampleDeathRecord.InjuryLocationLattitude = "37.88888" ;

// Getter:

Console.WriteLine($"Injury Location Lattitude: {ExampleDeathRecord.InjuryLocationLattitude}");

<a name='P-VRDR-DeathRecord-InjuryLocationLongitude'></a>
### InjuryLocationLongitude `property`

##### Summary

Longitude of Injury Location.

##### Example

// Setter:

ExampleDeathRecord.InjuryLocationLongitude = "-50.000" ;

// Getter:

Console.WriteLine($"Injury Location Longitude: {ExampleDeathRecord.InjuryLocationLongitude}");

<a name='P-VRDR-DeathRecord-InjuryLocationName'></a>
### InjuryLocationName `property`

##### Summary

Name of Injury Location.

##### Example

// Setter:

ExampleDeathRecord.InjuryLocationName = "Bedford Cemetery";

// Getter:

Console.WriteLine($"Injury Location Name: {ExampleDeathRecord.InjuryLocationName}");

<a name='P-VRDR-DeathRecord-InjuryMonth'></a>
### InjuryMonth `property`

##### Summary

Decedent's Month of Injury.

##### Example

// Setter:

ExampleDeathRecord.InjuryMonth = 7;

// Getter:

Console.WriteLine($"Decedent Month of Injury: {ExampleDeathRecord.InjuryMonth}");

<a name='P-VRDR-DeathRecord-InjuryPlaceDescription'></a>
### InjuryPlaceDescription `property`

##### Summary

Place of Injury Description.

##### Example

// Setter:

ExampleDeathRecord.InjuryPlaceDescription = "At home, in the kitchen";

// Getter:

Console.WriteLine($"Place of Injury Description: {ExampleDeathRecord.InjuryPlaceDescription}");

<a name='P-VRDR-DeathRecord-InjuryTime'></a>
### InjuryTime `property`

##### Summary

Decedent's Time of Injury.

##### Example

// Setter:

ExampleDeathRecord.InjuryTime = "07:15";

// Getter:

Console.WriteLine($"Decedent Time of Injury: {ExampleDeathRecord.InjuryTime}");

<a name='P-VRDR-DeathRecord-InjuryYear'></a>
### InjuryYear `property`

##### Summary

Decedent's Year of Injury.

##### Example

// Setter:

ExampleDeathRecord.InjuryYear = 2018;

// Getter:

Console.WriteLine($"Decedent Year of Injury: {ExampleDeathRecord.InjuryYear}");

<a name='P-VRDR-DeathRecord-IntentionalReject'></a>
### IntentionalReject `property`

##### Summary

Intentional Reject

##### Example

// Setter:

ExampleDeathRecord.IntentionalReject = "Reject1";

// Getter:

Console.WriteLine($"Intentional Reject {ExampleDeathRecord.IntentionalReject}");

<a name='P-VRDR-DeathRecord-IntentionalRejectHelper'></a>
### IntentionalRejectHelper `property`

##### Summary

Intentional Reject Helper.

##### Example

// Setter:

ExampleDeathRecord.IntentionalRejectHelper = ValueSets.IntentionalReject.Not_Rejected;

// Getter:

Console.WriteLine($"Intentional Reject Code: {ExampleDeathRecord.IntentionalRejectHelper}");

<a name='P-VRDR-DeathRecord-MaidenName'></a>
### MaidenName `property`

##### Summary

Decedent's Maiden Name.

##### Example

// Setter:

ExampleDeathRecord.MaidenName = "Last";

// Getter:

Console.WriteLine($"Decedent's Maiden Name: {ExampleDeathRecord.MaidenName}");

<a name='P-VRDR-DeathRecord-ManUnderlyingCOD'></a>
### ManUnderlyingCOD `property`

##### Summary

Decedent's Manual Underlying Cause of Death

##### Example

// Setter:

ExampleDeathRecord.ManUnderlyingCOD = "I13.1";

// Getter:

Console.WriteLine($"Decedent's Manual Underlying Cause of Death: {ExampleDeathRecord.ManUnderlyingCOD}");

<a name='P-VRDR-DeathRecord-MannerOfDeathType'></a>
### MannerOfDeathType `property`

##### Summary

Manner of Death Type.

##### Example

// Setter:

Dictionary<string, string> manner = new Dictionary<string, string>();

manner.Add("code", "7878000");

manner.Add("system", "");

manner.Add("display", "Accidental death");

ExampleDeathRecord.MannerOfDeathType = manner;

// Getter:

Console.WriteLine($"Manner Of Death Type: {ExampleDeathRecord.MannerOfDeathType['display']}");

<a name='P-VRDR-DeathRecord-MannerOfDeathTypeHelper'></a>
### MannerOfDeathTypeHelper `property`

##### Summary

Manner of Death Type Helper

##### Example

// Setter:

Dictionary<string, string> manner = new Dictionary<string, string>();

manner.Add("code", "7878000");

manner.Add("system", "");

manner.Add("display", "Accidental death");

ExampleDeathRecord.MannerOfDeathTypeHelper = MannerOfDeath.Natural;

// Getter:

Console.WriteLine($"Manner Of Death Type: {ExampleDeathRecord.MannerOfDeathTypeHelper}");

<a name='P-VRDR-DeathRecord-MaritalStatus'></a>
### MaritalStatus `property`

##### Summary

The marital status of the decedent at the time of death.

##### Example

// Setter:

Dictionary<string, string> code = new Dictionary<string, string>();

code.Add("code", "S");

code.Add("system", "http://terminology.hl7.org/CodeSystem/v3-MaritalStatus");

code.Add("display", "Never Married");

ExampleDeathRecord.MaritalStatus = code;

// Getter:

Console.WriteLine($"Marital status: {ExampleDeathRecord.MaritalStatus["display"]}");

<a name='P-VRDR-DeathRecord-MaritalStatusEditFlag'></a>
### MaritalStatusEditFlag `property`

##### Summary

The marital status edit flag.

##### Example

// Setter:

Dictionary<string, string> code = new Dictionary<string, string>();

code.Add("code", "S");

code.Add("system", "http://terminology.hl7.org/CodeSystem/v3-MaritalStatus");

code.Add("display", "Never Married");

ExampleDeathRecord.MaritalStatus = code;

// Getter:

Console.WriteLine($"Marital status: {ExampleDeathRecord.MaritalStatus["display"]}");

<a name='P-VRDR-DeathRecord-MaritalStatusEditFlagHelper'></a>
### MaritalStatusEditFlagHelper `property`

##### Summary

The marital status edit flag helper method.

##### Example

// Setter:

ExampleDeathRecord.MaritalStatusEditFlagHelper = ValueSets.EditBypass0124.0;

// Getter:

Console.WriteLine($"Marital status: {ExampleDeathRecord.MaritalStatusEditFlagHelper}");

<a name='P-VRDR-DeathRecord-MaritalStatusHelper'></a>
### MaritalStatusHelper `property`

##### Summary

The marital status of the decedent at the time of death helper method.

##### Example

// Setter:

ExampleDeathRecord.MaritalStatusHelper = ValueSets.MaritalStatus.NeverMarried;

// Getter:

Console.WriteLine($"Marital status: {ExampleDeathRecord.MaritalStatusHelper}");

<a name='P-VRDR-DeathRecord-MaritalStatusLiteral'></a>
### MaritalStatusLiteral `property`

##### Summary

The literal text string of the marital status of the decedent at the time of death.

##### Example

// Setter:

ExampleDeathRecord.MaritalStatusLiteral = "Single";

// Getter:

Console.WriteLine($"Marital status: {ExampleDeathRecord.MaritalStatusLiteral}");

<a name='P-VRDR-DeathRecord-MilitaryService'></a>
### MilitaryService `property`

##### Summary

Decedent's Military Service.

##### Example

// Setter:

Dictionary<string, string> mserv = new Dictionary<string, string>();

mserv.Add("code", "Y");

mserv.Add("system", CodeSystems.PH_YesNo_HL7_2x);

mserv.Add("display", "Yes");

ExampleDeathRecord.MilitaryService = uind;

// Getter:

Console.WriteLine($"Military Service: {ExampleDeathRecord.MilitaryService['display']}");

<a name='P-VRDR-DeathRecord-MilitaryServiceHelper'></a>
### MilitaryServiceHelper `property`

##### Summary

Decedent's Military Service. This is a helper method, to obtain the code use the MilitaryService property instead.

##### Example

// Setter:

ExampleDeathRecord.MilitaryServiceHelper = Y;

// Getter:

Console.WriteLine($"Military Service: {ExampleDeathRecord.MilitaryServiceHelper}");

<a name='P-VRDR-DeathRecord-MotherGivenNames'></a>
### MotherGivenNames `property`

##### Summary

Given name(s) of decedent's mother.

##### Example

// Setter:

string[] names = { "Mom", "Middle" };

ExampleDeathRecord.MotherGivenNames = names;

// Getter:

Console.WriteLine($"Mother Given Name(s): {string.Join(", ", ExampleDeathRecord.MotherGivenNames)}");

<a name='P-VRDR-DeathRecord-MotherMaidenName'></a>
### MotherMaidenName `property`

##### Summary

Maiden name of decedent's mother.

##### Example

// Setter:

ExampleDeathRecord.MotherMaidenName = "Last";

// Getter:

Console.WriteLine($"Mother's Maiden Name: {ExampleDeathRecord.MotherMaidenName}");

<a name='P-VRDR-DeathRecord-MotherSuffix'></a>
### MotherSuffix `property`

##### Summary

Mother's Suffix.

##### Example

// Setter:

ExampleDeathRecord.MotherSuffix = "Jr.";

// Getter:

Console.WriteLine($"Mother Suffix: {ExampleDeathRecord.MotherSuffix}");

<a name='P-VRDR-DeathRecord-PlaceOfBirth'></a>
### PlaceOfBirth `property`

##### Summary

Decedent's Place Of Birth.

##### Example

// Setter:

Dictionary<string, string> address = new Dictionary<string, string>();

address.Add("addressLine1", "123 Test Street");

address.Add("addressLine2", "Unit 3");

address.Add("addressCity", "Boston");

address.Add("addressCounty", "Suffolk");

address.Add("addressState", "MA");

address.Add("addressZip", "12345");

address.Add("addressCountry", "US");

SetterDeathRecord.PlaceOfBirth = address;

// Getter:

Console.WriteLine($"State where decedent was born: {ExampleDeathRecord.PlaceOfBirth["placeOfBirthState"]}");

<a name='P-VRDR-DeathRecord-PlaceOfInjury'></a>
### PlaceOfInjury `property`

##### Summary

Place of Injury.

##### Example

// Setter:

Dictionary<string, string> elevel = new Dictionary<string, string>();

elevel.Add("code", "LA14084-0");

elevel.Add("system", CodeSystems.LOINC);

elevel.Add("display", "Home");

ExampleDeathRecord.PlaceOfInjury = elevel;

// Getter:

Console.WriteLine($"PlaceOfInjury: {ExampleDeathRecord.PlaceOfInjury['display']}");

<a name='P-VRDR-DeathRecord-PlaceOfInjuryHelper'></a>
### PlaceOfInjuryHelper `property`

##### Summary

Decedent's Place of Injury Helper

##### Example

// Setter:

ExampleDeathRecord.PlaceOfInjuryHelper = ValueSets.PlaceOfInjury.Home;

// Getter:

Console.WriteLine($"Place of Injury: {ExampleDeathRecord.PlaceOfInjuryHelper}");

<a name='P-VRDR-DeathRecord-PregnancyStatus'></a>
### PregnancyStatus `property`

##### Summary

Pregnancy Status At Death.

##### Example

// Setter:

Dictionary<string, string> code = new Dictionary<string, string>();

code.Add("code", "1");

code.Add("system", VRDR.CodeSystems.PregnancyStatus);

code.Add("display", "Not pregnant within past year");

ExampleDeathRecord.PregnancyObs = code;

// Getter:

Console.WriteLine($"Pregnancy Status: {ExampleDeathRecord.PregnancyObs['display']}");

<a name='P-VRDR-DeathRecord-PregnancyStatusEditFlag'></a>
### PregnancyStatusEditFlag `property`

##### Summary

Decedent's Pregnancy Status at Death Edit Flag.

##### Example

// Setter:

Dictionary<string, string> elevel = new Dictionary<string, string>();

elevel.Add("code", "0");

elevel.Add("system", CodeSystems.BypassEditFlag);

elevel.Add("display", "Edit Passed");

ExampleDeathRecord.PregnancyStatusEditFlag = elevel;

// Getter:

Console.WriteLine($"Pregnancy Status Edit Flag: {ExampleDeathRecord.PregnancyStatusEditFlag['display']}");

<a name='P-VRDR-DeathRecord-PregnancyStatusEditFlagHelper'></a>
### PregnancyStatusEditFlagHelper `property`

##### Summary

Decedent's Pregnancy Status Edit Flag Helper

##### Example

// Setter:

ExampleDeathRecord.DecedentPregnancyStatusEditFlag = VRDR.ValueSets.EditBypass012.EditPassed;

// Getter:

Console.WriteLine($"Decedent's Pregnancy Status Edit Flag: {ExampleDeathRecord.PregnancyStatusHelperEditFlag}");

<a name='P-VRDR-DeathRecord-PregnancyStatusHelper'></a>
### PregnancyStatusHelper `property`

##### Summary

Pregnancy Status At Death Helper.

##### Example

// Setter:

ExampleDeathRecord.PregnancyStatusHelper = ValueSets.PregnancyStatus.Not_Pregnant_Within_Past_Year;

// Getter:

Console.WriteLine($"Pregnancy Status: {ExampleDeathRecord.PregnancyStatusHelper}");

<a name='P-VRDR-DeathRecord-Race'></a>
### Race `property`

##### Summary

Decedent's Race values.

##### Example

// Setter:

ExampleDeathRecord.Race = {NvssRace.BlackOrAfricanAmerican, "Y"};

// Getter:

string boaa = ExampleDeathRecord.RaceBlackOfAfricanAmerican;

<a name='P-VRDR-DeathRecord-RaceMissingValueReason'></a>
### RaceMissingValueReason `property`

##### Summary

Decedent's Race MissingValueReason.

##### Example

// Setter:

Dictionary<string, string> mvr = new Dictionary<string, string>();

mvr.Add("code", "R");

mvr.Add("system", CodeSystems.MissingValueReason);

mvr.Add("display", "Refused");

ExampleDeathRecord.RaceMissingValueReason = mvr;

// Getter:

Console.WriteLine($"Missing Race: {ExampleDeathRecord.RaceMissingValueReason['display']}");

<a name='P-VRDR-DeathRecord-RaceMissingValueReasonHelper'></a>
### RaceMissingValueReasonHelper `property`

##### Summary

Decedent's RaceMissingValueReason

##### Example

// Setter:

ExampleDeathRecord.RaceMissingValueReasonHelper = VRDR.ValueSets.RaceMissingValueReason.R;

// Getter:

Console.WriteLine($"Decedent's RaceMissingValueReason: {ExampleDeathRecord.RaceMissingValueReasonHelper}");

<a name='P-VRDR-DeathRecord-RaceRecode40'></a>
### RaceRecode40 `property`

##### Summary

Race Recode 40.

##### Example

// Setter:

Dictionary<string, string> racecode = new Dictionary<string, string>();

racecode.Add("code", "09");

racecode.Add("system", CodeSystems.RaceRecode40CS);

racecode.Add("display", "Vietnamiese");

ExampleDeathRecord.RaceRecode40 = racecode;

// Getter:

Console.WriteLine($"RaceRecode40: {ExampleDeathRecord.RaceRecode40['display']}");

<a name='P-VRDR-DeathRecord-RaceRecode40Helper'></a>
### RaceRecode40Helper `property`

##### Summary

Race Recode 40  Helper

##### Example

// Setter:

ExampleDeathRecord.RaceRecode40Helper = VRDR.ValueSets.RaceRecode40.AIAN ;

// Getter:

Console.WriteLine($"Race Recode 40: {ExampleDeathRecord.RaceRecode40Helper}");

<a name='P-VRDR-DeathRecord-ReceiptDate'></a>
### ReceiptDate `property`

##### Summary

Receipt Date.

##### Example

// Setter:

ExampleDeathRecord.ReceiptDate = "2018-02-19";

// Getter:

Console.WriteLine($"Receipt Date: {ExampleDeathRecord.ReceiptDate}");

<a name='P-VRDR-DeathRecord-ReceiptDay'></a>
### ReceiptDay `property`

##### Summary

The day NCHS received the death record.

##### Example

// Setter:

ExampleDeathRecord.ReceiptDay = 13

// Getter:

Console.WriteLine($"Receipt Day: {ExampleDeathRecord.ReceiptDay}");

<a name='P-VRDR-DeathRecord-ReceiptMonth'></a>
### ReceiptMonth `property`

##### Summary

The month NCHS received the death record.

##### Example

// Setter:

ExampleDeathRecord.ReceiptMonth = 11

// Getter:

Console.WriteLine($"Receipt Month: {ExampleDeathRecord.ReceiptMonth}");

<a name='P-VRDR-DeathRecord-ReceiptYear'></a>
### ReceiptYear `property`

##### Summary

The year NCHS received the death record.

##### Example

// Setter:

ExampleDeathRecord.ReceiptYear = 2022

// Getter:

Console.WriteLine($"Receipt Year: {ExampleDeathRecord.ReceiptYear}");

<a name='P-VRDR-DeathRecord-RecordAxisCauseOfDeath'></a>
### RecordAxisCauseOfDeath `property`

##### Summary

Record Axis Cause Of Death

##### Example

// Setter:

Tuple<string, string, string>[] eac = new Tuple<string, string, string>{Tuple.Create("position", "code", "pregnancy")}

ExampleDeathRecord.RecordAxisCauseOfDeath = new [] { (Position: 1, Code: "T27.3", Pregnancy: true) };

// Getter:

Console.WriteLine($"First Record Axis Code: {ExampleDeathRecord.RecordAxisCauseOfDeath.ElememtAt(0).Code}");

<a name='P-VRDR-DeathRecord-RegisteredTime'></a>
### RegisteredTime `property`

##### Summary

Registered time.

##### Example

// Setter:

ExampleDeathRecord.RegisteredTime = "2019-01-29T16:48:06-05:00";

// Getter:

Console.WriteLine($"Registered at: {ExampleDeathRecord.RegisteredTime}");

<a name='P-VRDR-DeathRecord-ReplaceStatus'></a>
### ReplaceStatus `property`

##### Summary

Replace Status.

##### Example

// Setter:

ExampleDeathRecord.ReplaceStatus = ValueSets.ReplaceStatus.Original_Record;

// Getter:

Console.WriteLine($"Filed method: {ExampleDeathRecord.ReplaceStatus}");

<a name='P-VRDR-DeathRecord-ReplaceStatusHelper'></a>
### ReplaceStatusHelper `property`

##### Summary

Replace Status Helper.

##### Example

// Setter:

ExampleDeathRecord.ReplaceStatusHelper = ValueSets.ReplaceStatus.Original_Record;

// Getter:

Console.WriteLine($"ReplaceStatus: {ExampleDeathRecord.ReplaceStatusHelper}");

<a name='P-VRDR-DeathRecord-Residence'></a>
### Residence `property`

##### Summary

Decedent's Residence.

##### Example

// Setter:

Dictionary<string, string> address = new Dictionary<string, string>();

address.Add("addressLine1", "123 Test Street");

address.Add("addressLine2", "Unit 3");

address.Add("addressCity", "Boston");

address.Add("addressCityC", "1234");

address.Add("addressCounty", "Suffolk");

address.Add("addressState", "MA");

address.Add("addressZip", "12345");

address.Add("addressCountry", "US");

SetterDeathRecord.Residence = address;

(addressStnum, 6)

// Getter:

Console.WriteLine($"State of residence: {ExampleDeathRecord.Residence["addressState"]}");

<a name='P-VRDR-DeathRecord-ResidenceWithinCityLimits'></a>
### ResidenceWithinCityLimits `property`

##### Summary

Decedent's residence is/is not within city limits.

##### Example

// Setter:

Dictionary<string, string> within = new Dictionary<string, string>();

within.Add("code", "Y");

within.Add("system", CodeSystems.PH_YesNo_HL7_2x);

within.Add("display", "Yes");

SetterDeathRecord.ResidenceWithinCityLimits = within;

// Getter:

Console.WriteLine($"Residence within city limits: {ExampleDeathRecord.ResidenceWithinCityLimits['display']}");

<a name='P-VRDR-DeathRecord-ResidenceWithinCityLimitsHelper'></a>
### ResidenceWithinCityLimitsHelper `property`

##### Summary

Residence Within City Limits Helper

##### Example

// Setter:

ExampleDeathRecord.ResidenceWithinCityLimitsHelper = VRDR.ValueSets.YesNoUnknown.Y;

// Getter:

Console.WriteLine($"Decedent's Residence within city limits: {ExampleDeathRecord.ResidenceWithinCityLimitsHelper}");

<a name='P-VRDR-DeathRecord-SSN'></a>
### SSN `property`

##### Summary

Decedent's Social Security Number.

##### Example

// Setter:

ExampleDeathRecord.SSN = "12345678";

// Getter:

Console.WriteLine($"Decedent Suffix: {ExampleDeathRecord.SSN}");

<a name='P-VRDR-DeathRecord-SecondAmericanIndianRaceCode'></a>
### SecondAmericanIndianRaceCode `property`

##### Summary

Second American Indian Race Code.

##### Example

// Setter:

Dictionary<string, string> racecode = new Dictionary<string, string>();

racecode.Add("code", "300");

racecode.Add("system", CodeSystems.RaceCode);

racecode.Add("display", "African");

ExampleDeathRecord.SecondAmericanIndianRaceCode = racecode;

// Getter:

Console.WriteLine($"Second American Indian Race Code: {ExampleDeathRecord.SecondAmericanIndianRaceCode['display']}");

<a name='P-VRDR-DeathRecord-SecondAmericanIndianRaceCodeHelper'></a>
### SecondAmericanIndianRaceCodeHelper `property`

##### Summary

Second American Indian Race Code  Helper

##### Example

// Setter:

ExampleDeathRecord.SecondAmericanIndianRaceCodeHelper = VRDR.ValueSets.RaceCode.African ;

// Getter:

Console.WriteLine($"Second American Indian Race Code: {ExampleDeathRecord.SecondAmericanIndianRaceCodeHelper}");

<a name='P-VRDR-DeathRecord-SecondEditedRaceCode'></a>
### SecondEditedRaceCode `property`

##### Summary

Second Edited Race Code.

##### Example

// Setter:

Dictionary<string, string> racecode = new Dictionary<string, string>();

racecode.Add("code", "300");

racecode.Add("system", CodeSystems.RaceCode);

racecode.Add("display", "African");

ExampleDeathRecord.SecondEditedRaceCode = racecode;

// Getter:

Console.WriteLine($"Second Edited Race Code: {ExampleDeathRecord.SecondEditedRaceCode['display']}");

<a name='P-VRDR-DeathRecord-SecondEditedRaceCodeHelper'></a>
### SecondEditedRaceCodeHelper `property`

##### Summary

Second Edited Race Code  Helper

##### Example

// Setter:

ExampleDeathRecord.SecondEditedRaceCodeHelper = VRDR.ValueSets.RaceCode.African ;

// Getter:

Console.WriteLine($"Second Edited Race Code: {ExampleDeathRecord.SecondEditedRaceCodeHelper}");

<a name='P-VRDR-DeathRecord-SecondOtherAsianRaceCode'></a>
### SecondOtherAsianRaceCode `property`

##### Summary

Second Other Asian Race Code.

##### Example

// Setter:

Dictionary<string, string> racecode = new Dictionary<string, string>();

racecode.Add("code", "300");

racecode.Add("system", CodeSystems.RaceCode);

racecode.Add("display", "African");

ExampleDeathRecord.SecondOtherAsianRaceCode = racecode;

// Getter:

Console.WriteLine($"Second Other Asian Race Code: {ExampleDeathRecord.SecondOtherAsianRaceCode['display']}");

<a name='P-VRDR-DeathRecord-SecondOtherAsianRaceCodeHelper'></a>
### SecondOtherAsianRaceCodeHelper `property`

##### Summary

Second Other Asian Race Code  Helper

##### Example

// Setter:

ExampleDeathRecord.SecondOtherAsianRaceCodeHelper = VRDR.ValueSets.RaceCode.African ;

// Getter:

Console.WriteLine($"Second Other Asian Race Code: {ExampleDeathRecord.SecondOtherAsianRaceCodeHelper}");

<a name='P-VRDR-DeathRecord-SecondOtherPacificIslanderRaceCode'></a>
### SecondOtherPacificIslanderRaceCode `property`

##### Summary

Second Other Pacific Islander Race Code.

##### Example

// Setter:

Dictionary<string, string> racecode = new Dictionary<string, string>();

racecode.Add("code", "300");

racecode.Add("system", CodeSystems.RaceCode);

racecode.Add("display", "African");

ExampleDeathRecord.SecondOtherPacificIslanderRaceCode = racecode;

// Getter:

Console.WriteLine($"Second Other Pacific Islander Race Code: {ExampleDeathRecord.SecondOtherPacificIslanderRaceCode['display']}");

<a name='P-VRDR-DeathRecord-SecondOtherPacificIslanderRaceCodeHelper'></a>
### SecondOtherPacificIslanderRaceCodeHelper `property`

##### Summary

Second Other Pacific Islander Race Code  Helper

##### Example

// Setter:

ExampleDeathRecord.SecondOtherPacificIslanderRaceCodeHelper = VRDR.ValueSets.RaceCode.African ;

// Getter:

Console.WriteLine($"Second Other Pacific Islander Race Code: {ExampleDeathRecord.SecondOtherPacificIslanderRaceCodeHelper}");

<a name='P-VRDR-DeathRecord-SecondOtherRaceCode'></a>
### SecondOtherRaceCode `property`

##### Summary

Second Other Race Code.

##### Example

// Setter:

Dictionary<string, string> racecode = new Dictionary<string, string>();

racecode.Add("code", "300");

racecode.Add("system", CodeSystems.RaceCode);

racecode.Add("display", "African");

ExampleDeathRecord.SecondOtherRaceCode = racecode;

// Getter:

Console.WriteLine($"Second Other Race Code: {ExampleDeathRecord.SecondOtherRaceCode['display']}");

<a name='P-VRDR-DeathRecord-SecondOtherRaceCodeHelper'></a>
### SecondOtherRaceCodeHelper `property`

##### Summary

Second Other Race Code  Helper

##### Example

// Setter:

ExampleDeathRecord.SecondOtherRaceCodeHelper = VRDR.ValueSets.RaceCode.African ;

// Getter:

Console.WriteLine($"Second Other Race Code: {ExampleDeathRecord.SecondOtherRaceCodeHelper}");

<a name='P-VRDR-DeathRecord-SeventhEditedRaceCode'></a>
### SeventhEditedRaceCode `property`

##### Summary

Seventh Edited Race Code.

##### Example

// Setter:

Dictionary<string, string> racecode = new Dictionary<string, string>();

racecode.Add("code", "300");

racecode.Add("system", CodeSystems.RaceCode);

racecode.Add("display", "African");

ExampleDeathRecord.SeventhEditedRaceCode = racecode;

// Getter:

Console.WriteLine($"Seventh Edited Race Code: {ExampleDeathRecord.SeventhEditedRaceCode['display']}");

<a name='P-VRDR-DeathRecord-SeventhEditedRaceCodeHelper'></a>
### SeventhEditedRaceCodeHelper `property`

##### Summary

Seventh Edited Race Code  Helper

##### Example

// Setter:

ExampleDeathRecord.SeventhEditedRaceCodeHelper = VRDR.ValueSets.RaceCode.African ;

// Getter:

Console.WriteLine($"Seventh Edited Race Code: {ExampleDeathRecord.SeventhEditedRaceCodeHelper}");

<a name='P-VRDR-DeathRecord-SexAtDeath'></a>
### SexAtDeath `property`

##### Summary

Decedent's Sex at Death.

##### Example

// Setter:

ExampleDeathRecord.SexAtDeath = "F";

// Getter:

Console.WriteLine($"Sex at Time of Death: {ExampleDeathRecord.SexAtDeath}");

<a name='P-VRDR-DeathRecord-SexAtDeathHelper'></a>
### SexAtDeathHelper `property`

##### Summary

Decedent's Sex At Death Helper

##### Example

// Setter:

ExampleDeathRecord.SexAtDeathHelper = VRDR.ValueSets.AdministractiveGender.Male;

// Getter:

Console.WriteLine($"Decedent's SexAtDeathHelper: {ExampleDeathRecord.SexAtDeathHelper}");

<a name='P-VRDR-DeathRecord-ShipmentNumber'></a>
### ShipmentNumber `property`

##### Summary

Shipment Number; TRX field with no IJE mapping

##### Example

// Setter:

ExampleDeathRecord.ShipmentNumber = "abc123"";

// Getter:

Console.WriteLine($"Shipment Number{ExampleDeathRecord.ShipmentNumber}");

<a name='P-VRDR-DeathRecord-SixthEditedRaceCode'></a>
### SixthEditedRaceCode `property`

##### Summary

Sixth Edited Race Code.

##### Example

// Setter:

Dictionary<string, string> racecode = new Dictionary<string, string>();

racecode.Add("code", "300");

racecode.Add("system", CodeSystems.RaceCode);

racecode.Add("display", "African");

ExampleDeathRecord.SixthEditedRaceCode = racecode;

// Getter:

Console.WriteLine($"Sixth Edited Race Code: {ExampleDeathRecord.SixthEditedRaceCode['display']}");

<a name='P-VRDR-DeathRecord-SixthEditedRaceCodeHelper'></a>
### SixthEditedRaceCodeHelper `property`

##### Summary

Sixth Edited Race Code  Helper

##### Example

// Setter:

ExampleDeathRecord.SixthEditedRaceCodeHelper = VRDR.ValueSets.RaceCode.African ;

// Getter:

Console.WriteLine($"Sixth Edited Race Code: {ExampleDeathRecord.SixthEditedRaceCodeHelper}");

<a name='P-VRDR-DeathRecord-SpouseAlive'></a>
### SpouseAlive `property`

##### Summary

Spouse Alive.

##### Example

// Setter:

Dictionary<string, string> code = new Dictionary<string, string>();

code.Add("code", "Y");

code.Add("system", "http://terminology.hl7.org/CodeSystem/v2-0136");

code.Add("display", "Yes");

ExampleDeathRecord.SpouseAlive = code;

// Getter:

Console.WriteLine($"Marital status: {ExampleDeathRecord.SpouseAlive["display"]}");

<a name='P-VRDR-DeathRecord-SpouseAliveHelper'></a>
### SpouseAliveHelper `property`

##### Summary

Decedent's SpouseAlive

##### Example

// Setter:

ExampleDeathRecord.SpouseAliveHelper = VRDR.ValueSets.YesNoUnknownNotApplicable.Y;

// Getter:

Console.WriteLine($"Decedent's Spouse Alive: {ExampleDeathRecord.SpouseAliveHelper}");

<a name='P-VRDR-DeathRecord-SpouseFamilyName'></a>
### SpouseFamilyName `property`

##### Summary

Family name of decedent's spouse.

##### Example

// Setter:

ExampleDeathRecord.SpouseFamilyName = "Last";

// Getter:

Console.WriteLine($"Spouse's Last Name: {ExampleDeathRecord.SpouseFamilyName}");

<a name='P-VRDR-DeathRecord-SpouseGivenNames'></a>
### SpouseGivenNames `property`

##### Summary

Given name(s) of decedent's spouse.

##### Example

// Setter:

string[] names = { "Spouse", "Middle" };

ExampleDeathRecord.SpouseGivenNames = names;

// Getter:

Console.WriteLine($"Spouse Given Name(s): {string.Join(", ", ExampleDeathRecord.SpouseGivenNames)}");

<a name='P-VRDR-DeathRecord-SpouseMaidenName'></a>
### SpouseMaidenName `property`

##### Summary

Spouse's Maiden Name.

##### Example

// Setter:

ExampleDeathRecord.SpouseSuffix = "Jr.";

// Getter:

Console.WriteLine($"Spouse Suffix: {ExampleDeathRecord.SpouseSuffix}");

<a name='P-VRDR-DeathRecord-SpouseSuffix'></a>
### SpouseSuffix `property`

##### Summary

Spouse's Suffix.

##### Example

// Setter:

ExampleDeathRecord.SpouseSuffix = "Jr.";

// Getter:

Console.WriteLine($"Spouse Suffix: {ExampleDeathRecord.SpouseSuffix}");

<a name='P-VRDR-DeathRecord-StateLocalIdentifier1'></a>
### StateLocalIdentifier1 `property`

##### Summary

State Local Identifier1.

##### Example

// Setter:

ExampleDeathRecord.StateLocalIdentifier1 = "MA";

// Getter:

Console.WriteLine($"State local identifier: {ExampleDeathRecord.StateLocalIdentifier1}");

<a name='P-VRDR-DeathRecord-StateLocalIdentifier2'></a>
### StateLocalIdentifier2 `property`

##### Summary

State Local Identifier2.

##### Example

// Setter:

ExampleDeathRecord.StateLocalIdentifier2 = "YC";

// Getter:

Console.WriteLine($"State local identifier: {ExampleDeathRecord.StateLocalIdentifier1}");

<a name='P-VRDR-DeathRecord-StateSpecific'></a>
### StateSpecific `property`

##### Summary

State Specific Data.

##### Example

// Setter:

ExampleDeathRecord.StateSpecific = "Data";

// Getter:

Console.WriteLine($"State Specific Data: {ExampleDeathRecord.StateSpecific}");

<a name='P-VRDR-DeathRecord-Suffix'></a>
### Suffix `property`

##### Summary

Decedent's Suffix.

##### Example

// Setter:

ExampleDeathRecord.Suffix = "Jr.";

// Getter:

Console.WriteLine($"Decedent Suffix: {ExampleDeathRecord.Suffix}");

<a name='P-VRDR-DeathRecord-SurgeryDate'></a>
### SurgeryDate `property`

##### Summary

Decedent's Surgery Date.

##### Example

// Setter:

ExampleDeathRecord.SurgeryDate = "2018-02-19";

// Getter:

Console.WriteLine($"Decedent Surgery Date: {ExampleDeathRecord.SurgeryDate}");

<a name='P-VRDR-DeathRecord-SurgeryDay'></a>
### SurgeryDay `property`

##### Summary

Decedent's Day of Surgery.

##### Example

// Setter:

ExampleDeathRecord.SurgeryDay = 16;

// Getter:

Console.WriteLine($"Decedent Day of Surgery: {ExampleDeathRecord.SurgeryDay}");

<a name='P-VRDR-DeathRecord-SurgeryMonth'></a>
### SurgeryMonth `property`

##### Summary

Decedent's Month of Surgery.

##### Example

// Setter:

ExampleDeathRecord.SurgeryMonth = 6;

// Getter:

Console.WriteLine($"Decedent Month of Surgery: {ExampleDeathRecord.SurgeryMonth}");

<a name='P-VRDR-DeathRecord-SurgeryYear'></a>
### SurgeryYear `property`

##### Summary

Decedent's Year of Surgery.

##### Example

// Setter:

ExampleDeathRecord.SurgeryYear = 2018;

// Getter:

Console.WriteLine($"Decedent Year of Surgery: {ExampleDeathRecord.SurgeryYear}");

<a name='P-VRDR-DeathRecord-ThirdEditedRaceCode'></a>
### ThirdEditedRaceCode `property`

##### Summary

Third Edited Race Code.

##### Example

// Setter:

Dictionary<string, string> racecode = new Dictionary<string, string>();

racecode.Add("code", "300");

racecode.Add("system", CodeSystems.RaceCode);

racecode.Add("display", "African");

ExampleDeathRecord.ThirdEditedRaceCode = racecode;

// Getter:

Console.WriteLine($"Third Edited Race Code: {ExampleDeathRecord.ThirdEditedRaceCode['display']}");

<a name='P-VRDR-DeathRecord-ThirdEditedRaceCodeHelper'></a>
### ThirdEditedRaceCodeHelper `property`

##### Summary

Third Edited Race Code  Helper

##### Example

// Setter:

ExampleDeathRecord.ThirdEditedRaceCodeHelper = VRDR.ValueSets.RaceCode.African ;

// Getter:

Console.WriteLine($"Third Edited Race Code: {ExampleDeathRecord.ThirdEditedRaceCodeHelper}");

<a name='P-VRDR-DeathRecord-TobaccoUse'></a>
### TobaccoUse `property`

##### Summary

Tobacco Use Contributed To Death.

##### Example

// Setter:

Dictionary<string, string> code = new Dictionary<string, string>();

code.Add("code", "373066001");

code.Add("system", CodeSystems.SCT);

code.Add("display", "Yes");

ExampleDeathRecord.TobaccoUse = code;

// Getter:

Console.WriteLine($"Tobacco Use: {ExampleDeathRecord.TobaccoUse['display']}");

<a name='P-VRDR-DeathRecord-TobaccoUseHelper'></a>
### TobaccoUseHelper `property`

##### Summary

Tobacco Use Helper. This is a convenience method, to access the code use TobaccoUse instead.

##### Example

// Setter:

ExampleDeathRecord.TobaccoUseHelper = "N";

// Getter:

Console.WriteLine($"Tobacco Use: {ExampleDeathRecord.TobaccoUseHelper}");

<a name='P-VRDR-DeathRecord-TransaxConversion'></a>
### TransaxConversion `property`

##### Summary

Transax Conversion Flag

##### Example

// Setter:

ExampleDeathRecord.TransaxConversion = 3;

// Getter:

Console.WriteLine($"Transax Conversion Code: {ExampleDeathRecord.TransaxConversion}");

<a name='P-VRDR-DeathRecord-TransaxConversionHelper'></a>
### TransaxConversionHelper `property`

##### Summary

TransaxConversion Helper.

##### Example

// Setter:

ExampleDeathRecord.TransaxConversionHelper = ValueSets.TransaxConversion.Conversion_Using_Non_Ambivalent_Table_Entries;

// Getter:

Console.WriteLine($"Filing Format: {ExampleDeathRecord.TransaxConversionHelper}");

<a name='P-VRDR-DeathRecord-TransportationRole'></a>
### TransportationRole `property`

##### Summary

Transportation Role in death.

##### Example

// Setter:

Dictionary<string, string> code = new Dictionary<string, string>();

code.Add("code", "257500003");

code.Add("system", CodeSystems.SCT);

code.Add("display", "Passenger");

ExampleDeathRecord.TransportationRole = code;

// Getter:

Console.WriteLine($"Transportation Role: {ExampleDeathRecord.TransportationRole['display']}");

<a name='P-VRDR-DeathRecord-TransportationRoleHelper'></a>
### TransportationRoleHelper `property`

##### Summary

Transportation Role in death helper.

##### Example

// Setter:

ExampleDeathRecord.TransportationRoleHelper = VRDR.TransportationRoles.Passenger;

// Getter:

Console.WriteLine($"Transportation Role: {ExampleDeathRecord.TransportationRoleHelper");

<a name='P-VRDR-DeathRecord-UsualIndustry'></a>
### UsualIndustry `property`

##### Summary

Decedent's Usual Industry (Text).

##### Example

// Setter:

ExampleDeathRecord.UsualIndustry = "Accounting";

// Getter:

Console.WriteLine($"Usual Industry: {ExampleDeathRecord.UsualIndustry}");

<a name='P-VRDR-DeathRecord-UsualOccupation'></a>
### UsualOccupation `property`

##### Summary

Decedent's Usual Occupation.

##### Example

// Setter:

ExampleDeathRecord.UsualOccupation = "Biomedical engineering";

// Getter:

Console.WriteLine($"Usual Occupation: {ExampleDeathRecord.UsualOccupation}");

<a name='M-VRDR-DeathRecord-AddReferenceToComposition-System-String,System-String-'></a>
### AddReferenceToComposition(reference,code) `method`

##### Summary

Add a reference to the Death Record Composition.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| reference | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | a reference. |
| code | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | the code for the section to add to. |

<a name='M-VRDR-DeathRecord-AddressToDict-Hl7-Fhir-Model-Address-'></a>
### AddressToDict(addr) `method`

##### Summary

Convert a FHIR Address to an "address" Dictionary.

##### Returns

the corresponding Dictionary representation of the FHIR Address.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| addr | [Hl7.Fhir.Model.Address](#T-Hl7-Fhir-Model-Address 'Hl7.Fhir.Model.Address') | a FHIR Address. |

<a name='M-VRDR-DeathRecord-CauseOfDeathCondition-System-Int32-'></a>
### CauseOfDeathCondition() `method`

##### Summary

Create a Cause Of Death Condition

##### Parameters

This method has no parameters.

<a name='M-VRDR-DeathRecord-CodeableConceptToDict-Hl7-Fhir-Model-CodeableConcept-'></a>
### CodeableConceptToDict(codeableConcept) `method`

##### Summary

Convert a FHIR CodableConcept to a "code" Dictionary

##### Returns

the corresponding Dictionary representation of the code.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| codeableConcept | [Hl7.Fhir.Model.CodeableConcept](#T-Hl7-Fhir-Model-CodeableConcept 'Hl7.Fhir.Model.CodeableConcept') | a FHIR CodeableConcept. |

<a name='M-VRDR-DeathRecord-CodingToDict-Hl7-Fhir-Model-Coding-'></a>
### CodingToDict(coding) `method`

##### Summary

Convert a FHIR Coding to a "code" Dictionary

##### Returns

the corresponding Dictionary representation of the code.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| coding | [Hl7.Fhir.Model.Coding](#T-Hl7-Fhir-Model-Coding 'Hl7.Fhir.Model.Coding') | a FHIR Coding. |

<a name='M-VRDR-DeathRecord-CreateActivityAtTimeOfDeathObs'></a>
### CreateActivityAtTimeOfDeathObs() `method`

##### Summary

Create an empty ActivityAtTimeOfDeathObs, to be populated in ActivityAtDeath.

##### Parameters

This method has no parameters.

<a name='M-VRDR-DeathRecord-CreateAgeAtDeathObs'></a>
### CreateAgeAtDeathObs() `method`

##### Summary

Create Age At Death Obs

##### Parameters

This method has no parameters.

<a name='M-VRDR-DeathRecord-CreateAutomatedUnderlyingCauseOfDeathObs'></a>
### CreateAutomatedUnderlyingCauseOfDeathObs() `method`

##### Summary

Create an empty AutomatedUnderlyingCODObs, to be populated in AutomatedUnderlyingCOD.

##### Parameters

This method has no parameters.

<a name='M-VRDR-DeathRecord-CreateAutopsyPerformed'></a>
### CreateAutopsyPerformed() `method`

##### Summary

Create Autopsy Performed

##### Parameters

This method has no parameters.

<a name='M-VRDR-DeathRecord-CreateCertifier'></a>
### CreateCertifier() `method`

##### Summary

Create Certifier.

##### Parameters

This method has no parameters.

<a name='M-VRDR-DeathRecord-CreateCodedRaceAndEthnicityObs'></a>
### CreateCodedRaceAndEthnicityObs() `method`

##### Summary

Create an empty CodedRaceAndEthnicityObs, to be populated in Various Methods.

##### Parameters

This method has no parameters.

<a name='M-VRDR-DeathRecord-CreateDeathCertification'></a>
### CreateDeathCertification() `method`

##### Summary

Create Death Certification.

##### Parameters

This method has no parameters.

<a name='M-VRDR-DeathRecord-CreateDeathDateObs'></a>
### CreateDeathDateObs() `method`

##### Summary

Create Death Date Observation.

##### Parameters

This method has no parameters.

<a name='M-VRDR-DeathRecord-CreateDeathLocation'></a>
### CreateDeathLocation() `method`

##### Summary

Create Death Location

##### Parameters

This method has no parameters.

<a name='M-VRDR-DeathRecord-CreateDispositionLocation'></a>
### CreateDispositionLocation() `method`

##### Summary

Create Disposition Location.

##### Parameters

This method has no parameters.

<a name='M-VRDR-DeathRecord-CreateEducationLevelObs'></a>
### CreateEducationLevelObs() `method`

##### Summary

Create an empty EducationLevel Observation, to be populated in either EducationLevel or EducationLevelEditFlag.

##### Parameters

This method has no parameters.

<a name='M-VRDR-DeathRecord-CreateFather'></a>
### CreateFather() `method`

##### Summary

Create Spouse.

##### Parameters

This method has no parameters.

<a name='M-VRDR-DeathRecord-CreateFuneralHome'></a>
### CreateFuneralHome() `method`

##### Summary

Create Funeral Home.

##### Parameters

This method has no parameters.

<a name='M-VRDR-DeathRecord-CreateInjuryIncidentObs'></a>
### CreateInjuryIncidentObs() `method`

##### Summary

Create Injury Incident.

##### Parameters

This method has no parameters.

<a name='M-VRDR-DeathRecord-CreateInjuryLocationLoc'></a>
### CreateInjuryLocationLoc() `method`

##### Summary

Create Injury Location.

##### Parameters

This method has no parameters.

<a name='M-VRDR-DeathRecord-CreateInputRaceEthnicityObs'></a>
### CreateInputRaceEthnicityObs() `method`

##### Summary

Create Input Race and Ethnicity

##### Parameters

This method has no parameters.

<a name='M-VRDR-DeathRecord-CreateManualUnderlyingCauseOfDeathObs'></a>
### CreateManualUnderlyingCauseOfDeathObs() `method`

##### Summary

Create an empty AutomatedUnderlyingCODObs, to be populated in AutomatedUnderlyingCOD.

##### Parameters

This method has no parameters.

<a name='M-VRDR-DeathRecord-CreateMother'></a>
### CreateMother() `method`

##### Summary

Create Mother.

##### Parameters

This method has no parameters.

<a name='M-VRDR-DeathRecord-CreatePlaceOfInjuryObs'></a>
### CreatePlaceOfInjuryObs() `method`

##### Summary

Create an empty PlaceOfInjuryObs, to be populated in PlaceOfInjury.

##### Parameters

This method has no parameters.

<a name='M-VRDR-DeathRecord-CreatePregnancyObs'></a>
### CreatePregnancyObs() `method`

##### Summary

Create Pregnancy Status.

##### Parameters

This method has no parameters.

<a name='M-VRDR-DeathRecord-CreateSpouse'></a>
### CreateSpouse() `method`

##### Summary

Create Spouse.

##### Parameters

This method has no parameters.

<a name='M-VRDR-DeathRecord-CreateSurgeryDateObs'></a>
### CreateSurgeryDateObs() `method`

##### Summary

Create Surgery Date Observation.

##### Parameters

This method has no parameters.

<a name='M-VRDR-DeathRecord-CreateUsualWork'></a>
### CreateUsualWork() `method`

##### Summary

Create Usual Work.

##### Parameters

This method has no parameters.

<a name='M-VRDR-DeathRecord-DatePartToIntegerOrCode-System-Tuple{System-String,System-String}-'></a>
### DatePartToIntegerOrCode(pair) `method`

##### Summary

Convert an element to an integer or code depending on if the input element is a date part.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| pair | [System.Tuple{System.String,System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Tuple 'System.Tuple{System.String,System.String}') | A key value pair, the key will be used to identify whether the element is a date part. |

<a name='M-VRDR-DeathRecord-DatePartsToArray-Hl7-Fhir-Model-Extension-'></a>
### DatePartsToArray(datePartAbsent) `method`

##### Summary

Convert a Date Part Extension to an Array.

##### Returns

the corresponding array representation of the date parts.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| datePartAbsent | [Hl7.Fhir.Model.Extension](#T-Hl7-Fhir-Model-Extension 'Hl7.Fhir.Model.Extension') | a Date Part Extension. |

<a name='M-VRDR-DeathRecord-DictToAddress-System-Collections-Generic-Dictionary{System-String,System-String}-'></a>
### DictToAddress(dict) `method`

##### Summary

Convert an "address" dictionary to a FHIR Address.

##### Returns

the corresponding FHIR Address representation of the address.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| dict | [System.Collections.Generic.Dictionary{System.String,System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.Dictionary 'System.Collections.Generic.Dictionary{System.String,System.String}') | represents an address. |

<a name='M-VRDR-DeathRecord-DictToCodeableConcept-System-Collections-Generic-Dictionary{System-String,System-String}-'></a>
### DictToCodeableConcept(dict) `method`

##### Summary

Convert a "code" dictionary to a FHIR CodableConcept.

##### Returns

the corresponding CodeableConcept representation of the code.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| dict | [System.Collections.Generic.Dictionary{System.String,System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.Dictionary 'System.Collections.Generic.Dictionary{System.String,System.String}') | represents a code. |

<a name='M-VRDR-DeathRecord-DictToCoding-System-Collections-Generic-Dictionary{System-String,System-String}-'></a>
### DictToCoding(dict) `method`

##### Summary

Convert a "code" dictionary to a FHIR Coding.

##### Returns

the corresponding Coding representation of the code.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| dict | [System.Collections.Generic.Dictionary{System.String,System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.Dictionary 'System.Collections.Generic.Dictionary{System.String,System.String}') | represents a code. |

<a name='M-VRDR-DeathRecord-EmptyAddrDict'></a>
### EmptyAddrDict() `method`

##### Summary

Returns an empty "address" Dictionary.

##### Returns

an empty "address" Dictionary.

##### Parameters

This method has no parameters.

<a name='M-VRDR-DeathRecord-EmptyCodeDict'></a>
### EmptyCodeDict() `method`

##### Summary

Returns an empty "code" Dictionary.

##### Returns

an empty "code" Dictionary.

##### Parameters

This method has no parameters.

<a name='M-VRDR-DeathRecord-EmptyCodeableDict'></a>
### EmptyCodeableDict() `method`

##### Summary

Returns an empty "codeable" Dictionary.

##### Returns

an empty "codeable" Dictionary.

##### Parameters

This method has no parameters.

<a name='M-VRDR-DeathRecord-FromDescription-System-String-'></a>
### FromDescription(contents) `method`

##### Summary

Helper method to return a JSON string representation of this Death Record.

##### Returns

a new DeathRecord that corresponds to the given descriptive format

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| contents | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | string that represents |

<a name='M-VRDR-DeathRecord-GetAll-System-String-'></a>
### GetAll(path) `method`

##### Summary

Given a FHIR path, return the elements that match the given path;
returns an empty array if no matches are found.

##### Returns

all elements that match the given path, or an empty array if no matches are found.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| path | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | represents a FHIR path. |

<a name='M-VRDR-DeathRecord-GetAllString-System-String-'></a>
### GetAllString(path) `method`

##### Summary

Given a FHIR path, return the elements that match the given path as a string;
returns an empty array if no matches are found.

##### Returns

all elements that match the given path as a string, or an empty array if no matches are found.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| path | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | represents a FHIR path. |

<a name='M-VRDR-DeathRecord-GetBundle'></a>
### GetBundle() `method`

##### Summary

Helper method to return a the bundle.

##### Returns

a FHIR Bundle

##### Parameters

This method has no parameters.

<a name='M-VRDR-DeathRecord-GetCauseOfDeathCodedContentBundle'></a>
### GetCauseOfDeathCodedContentBundle() `method`

##### Summary

Helper method to return the subset of this record that makes up a CauseOfDeathCodedContent bundle.

##### Returns

a new FHIR Bundle

##### Parameters

This method has no parameters.

<a name='M-VRDR-DeathRecord-GetDateFragmentOrPartialDate-Hl7-Fhir-Model-Element,System-String-'></a>
### GetDateFragmentOrPartialDate() `method`

##### Summary

Getter helper for anything that can have a regular FHIR date/time or a PartialDateTime extension, allowing a particular date
field (year, month, or day) to be read from either the value or the extension

##### Parameters

This method has no parameters.

<a name='M-VRDR-DeathRecord-GetDeathCertificateDocumentBundle'></a>
### GetDeathCertificateDocumentBundle() `method`

##### Summary

Helper method to return the bundle that makes up a CauseOfDeathCodedContent bundle. This is actually
the complete DeathRecord Bundle accessible via a method name that aligns with the other specific
GetBundle methods (GetCauseOfDeathCodedContentBundle and GetDemographicCodedContentBundle).

##### Returns

a FHIR Bundle

##### Parameters

This method has no parameters.

<a name='M-VRDR-DeathRecord-GetDemographicCodedContentBundle'></a>
### GetDemographicCodedContentBundle() `method`

##### Summary

Helper method to return the subset of this record that makes up a DemographicCodedContent bundle.

##### Returns

a new FHIR Bundle

##### Parameters

This method has no parameters.

<a name='M-VRDR-DeathRecord-GetEmergingIssue-System-String-'></a>
### GetEmergingIssue() `method`

##### Summary

Get an emerging issue value.

##### Parameters

This method has no parameters.

<a name='M-VRDR-DeathRecord-GetFirst-System-String-'></a>
### GetFirst(path) `method`

##### Summary

Given a FHIR path, return the first element that matches the given path.

##### Returns

the first element that matches the given path, or null if no match is found.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| path | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | represents a FHIR path. |

<a name='M-VRDR-DeathRecord-GetFirstString-System-String-'></a>
### GetFirstString(path) `method`

##### Summary

Given a FHIR path, return the first element that matches the given path as a string;
returns null if no match is found.

##### Returns

the first element that matches the given path as a string, or null if no match is found.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| path | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | represents a FHIR path. |

<a name='M-VRDR-DeathRecord-GetITypedElement'></a>
### GetITypedElement() `method`

##### Summary

Helper method to return an ITypedElement of the record bundle.

##### Returns

an ITypedElement of the record bundle

##### Parameters

This method has no parameters.

<a name='M-VRDR-DeathRecord-GetLast-System-String-'></a>
### GetLast(path) `method`

##### Summary

Given a FHIR path, return the last element that matches the given path.

##### Returns

the last element that matches the given path, or null if no match is found.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| path | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | represents a FHIR path. |

<a name='M-VRDR-DeathRecord-GetLastString-System-String-'></a>
### GetLastString(path) `method`

##### Summary

Given a FHIR path, return the last element that matches the given path as a string;
returns an empty string if no match is found.

##### Returns

the last element that matches the given path as a string, or null if no match is found.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| path | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | represents a FHIR path. |

<a name='M-VRDR-DeathRecord-GetPartialDate-Hl7-Fhir-Model-Extension,System-String-'></a>
### GetPartialDate() `method`

##### Summary

Getter helper for anything that uses PartialDateTime, allowing a particular date field (year, month, or day) to be read from the extension

##### Parameters

This method has no parameters.

<a name='M-VRDR-DeathRecord-GetPartialTime-Hl7-Fhir-Model-Extension-'></a>
### GetPartialTime() `method`

##### Summary

Getter helper for anything that uses PartialDateTime, allowing the time to be read from the extension

##### Parameters

This method has no parameters.

<a name='M-VRDR-DeathRecord-GetTimeFragmentOrPartialTime-Hl7-Fhir-Model-Element-'></a>
### GetTimeFragmentOrPartialTime() `method`

##### Summary

Getter helper for anything that can have a regular FHIR date/time or a PartialDateTime extension, allowing the time to be read
from either the value or the extension

##### Parameters

This method has no parameters.

<a name='M-VRDR-DeathRecord-GetValue-System-Collections-Generic-Dictionary{System-String,System-String},System-String-'></a>
### GetValue() `method`

##### Summary

Get a value from a Dictionary, but return null if the key doesn't exist or the value is an empty string.

##### Parameters

This method has no parameters.

<a name='M-VRDR-DeathRecord-IsDictEmptyOrDefault-System-Collections-Generic-Dictionary{System-String,System-String}-'></a>
### IsDictEmptyOrDefault(dict) `method`

##### Summary

Check if a dictionary is empty or a default empty dictionary (all values are null or empty strings)

##### Returns

A boolean identifying whether the provided dictionary is empty or default.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| dict | [System.Collections.Generic.Dictionary{System.String,System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.Dictionary 'System.Collections.Generic.Dictionary{System.String,System.String}') | represents a code. |

<a name='M-VRDR-DeathRecord-RemoveReferenceFromComposition-System-String,System-String-'></a>
### RemoveReferenceFromComposition(reference,code) `method`

##### Summary

Remove a reference from the Death Record Composition.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| reference | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | a reference. |
| code | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | a code for the section to modify. |

<a name='M-VRDR-DeathRecord-RestoreReferences'></a>
### RestoreReferences() `method`

##### Summary

Restores class references from a newly parsed record.

##### Parameters

This method has no parameters.

<a name='M-VRDR-DeathRecord-SetCodeValue-System-String,System-String,System-String[0-,0-]-'></a>
### SetCodeValue() `method`

##### Summary

Helper function to set a codeable value based on a code and the set of allowed codes.

##### Parameters

This method has no parameters.

<a name='M-VRDR-DeathRecord-SetEmergingIssue-System-String,System-String-'></a>
### SetEmergingIssue() `method`

##### Summary

Set an emerging issue value, creating an empty EmergingIssues Observation as needed.

##### Parameters

This method has no parameters.

<a name='M-VRDR-DeathRecord-SetPartialDate-Hl7-Fhir-Model-Extension,System-String,System-Nullable{System-UInt32}-'></a>
### SetPartialDate() `method`

##### Summary

Setter helper for anything that uses PartialDateTime, allowing a particular date field (year, month, or day) to be set in the extension

##### Parameters

This method has no parameters.

<a name='M-VRDR-DeathRecord-SetPartialTime-Hl7-Fhir-Model-Extension,System-String-'></a>
### SetPartialTime() `method`

##### Summary

Setter helper for anything that uses PartialDateTime, allowing the time to be set in the extension

##### Parameters

This method has no parameters.

<a name='M-VRDR-DeathRecord-ToDescription'></a>
### ToDescription() `method`

##### Summary

Returns a JSON encoded structure that maps to the various property
annotations found in the DeathRecord class. This is useful for scenarios
where you may want to display the data in user interfaces.

##### Returns

a string representation of this Death Record in a descriptive format.

##### Parameters

This method has no parameters.

<a name='M-VRDR-DeathRecord-ToJSON'></a>
### ToJSON() `method`

##### Summary

Helper method to return a JSON string representation of this Death Record.

##### Returns

a string representation of this Death Record in JSON format

##### Parameters

This method has no parameters.

<a name='M-VRDR-DeathRecord-ToJson'></a>
### ToJson() `method`

##### Summary

Helper method to return a JSON string representation of this Death Record.

##### Returns

a string representation of this Death Record in JSON format

##### Parameters

This method has no parameters.

<a name='M-VRDR-DeathRecord-ToXML'></a>
### ToXML() `method`

##### Summary

Helper method to return a XML string representation of this Death Record.

##### Returns

a string representation of this Death Record in XML format

##### Parameters

This method has no parameters.

<a name='M-VRDR-DeathRecord-ToXml'></a>
### ToXml() `method`

##### Summary

Helper method to return a XML string representation of this Death Record.

##### Returns

a string representation of this Death Record in XML format

##### Parameters

This method has no parameters.

<a name='M-VRDR-DeathRecord-UpdateDeathRecordIdentifier'></a>
### UpdateDeathRecordIdentifier() `method`

##### Summary

Update the bundle identifier from the component fields.

##### Parameters

This method has no parameters.

<a name='M-VRDR-DeathRecord-UpdateDictionary-System-Collections-Generic-Dictionary{System-String,System-String},System-Collections-Generic-Dictionary{System-String,System-String}-'></a>
### UpdateDictionary() `method`

##### Summary

Combine the given dictionaries and return the combined result.

##### Parameters

This method has no parameters.

<a name='T-VRDR-Mappings-EditBypass01'></a>
## EditBypass01 `type`

##### Namespace

VRDR.Mappings

##### Summary

Mappings for EditBypass01

<a name='T-VRDR-ValueSets-EditBypass01'></a>
## EditBypass01 `type`

##### Namespace

VRDR.ValueSets

##### Summary

EditBypass01

<a name='F-VRDR-Mappings-EditBypass01-FHIRToIJE'></a>
### FHIRToIJE `constants`

##### Summary

FHIR -> IJE Mapping for EditBypass01

<a name='F-VRDR-Mappings-EditBypass01-IJEToFHIR'></a>
### IJEToFHIR `constants`

##### Summary

IJE -> FHIR Mapping for EditBypass01

<a name='F-VRDR-ValueSets-EditBypass01-Codes'></a>
### Codes `constants`

##### Summary

Codes

<a name='F-VRDR-ValueSets-EditBypass01-Edit_Failed_Data_Queried_And_Verified'></a>
### Edit_Failed_Data_Queried_And_Verified `constants`

##### Summary

Edit_Failed_Data_Queried_And_Verified

<a name='F-VRDR-ValueSets-EditBypass01-Edit_Passed'></a>
### Edit_Passed `constants`

##### Summary

Edit_Passed

<a name='T-VRDR-Mappings-EditBypass012'></a>
## EditBypass012 `type`

##### Namespace

VRDR.Mappings

##### Summary

Mappings for EditBypass012

<a name='T-VRDR-ValueSets-EditBypass012'></a>
## EditBypass012 `type`

##### Namespace

VRDR.ValueSets

##### Summary

EditBypass012

<a name='F-VRDR-Mappings-EditBypass012-FHIRToIJE'></a>
### FHIRToIJE `constants`

##### Summary

FHIR -> IJE Mapping for EditBypass012

<a name='F-VRDR-Mappings-EditBypass012-IJEToFHIR'></a>
### IJEToFHIR `constants`

##### Summary

IJE -> FHIR Mapping for EditBypass012

<a name='F-VRDR-ValueSets-EditBypass012-Codes'></a>
### Codes `constants`

##### Summary

Codes

<a name='F-VRDR-ValueSets-EditBypass012-Edit_Failed_Data_Queried_And_Verified'></a>
### Edit_Failed_Data_Queried_And_Verified `constants`

##### Summary

Edit_Failed_Data_Queried_And_Verified

<a name='F-VRDR-ValueSets-EditBypass012-Edit_Failed_Data_Queried_But_Not_Verified'></a>
### Edit_Failed_Data_Queried_But_Not_Verified `constants`

##### Summary

Edit_Failed_Data_Queried_But_Not_Verified

<a name='F-VRDR-ValueSets-EditBypass012-Edit_Passed'></a>
### Edit_Passed `constants`

##### Summary

Edit_Passed

<a name='T-VRDR-Mappings-EditBypass01234'></a>
## EditBypass01234 `type`

##### Namespace

VRDR.Mappings

##### Summary

Mappings for EditBypass01234

<a name='T-VRDR-ValueSets-EditBypass01234'></a>
## EditBypass01234 `type`

##### Namespace

VRDR.ValueSets

##### Summary

EditBypass01234

<a name='F-VRDR-Mappings-EditBypass01234-FHIRToIJE'></a>
### FHIRToIJE `constants`

##### Summary

FHIR -> IJE Mapping for EditBypass01234

<a name='F-VRDR-Mappings-EditBypass01234-IJEToFHIR'></a>
### IJEToFHIR `constants`

##### Summary

IJE -> FHIR Mapping for EditBypass01234

<a name='F-VRDR-ValueSets-EditBypass01234-Codes'></a>
### Codes `constants`

##### Summary

Codes

<a name='F-VRDR-ValueSets-EditBypass01234-Edit_Failed_Data_Queried_And_Verified'></a>
### Edit_Failed_Data_Queried_And_Verified `constants`

##### Summary

Edit_Failed_Data_Queried_And_Verified

<a name='F-VRDR-ValueSets-EditBypass01234-Edit_Failed_Data_Queried_But_Not_Verified'></a>
### Edit_Failed_Data_Queried_But_Not_Verified `constants`

##### Summary

Edit_Failed_Data_Queried_But_Not_Verified

<a name='F-VRDR-ValueSets-EditBypass01234-Edit_Failed_Query_Needed'></a>
### Edit_Failed_Query_Needed `constants`

##### Summary

Edit_Failed_Query_Needed

<a name='F-VRDR-ValueSets-EditBypass01234-Edit_Failed_Review_Needed'></a>
### Edit_Failed_Review_Needed `constants`

##### Summary

Edit_Failed_Review_Needed

<a name='F-VRDR-ValueSets-EditBypass01234-Edit_Passed'></a>
### Edit_Passed `constants`

##### Summary

Edit_Passed

<a name='T-VRDR-Mappings-EditBypass0124'></a>
## EditBypass0124 `type`

##### Namespace

VRDR.Mappings

##### Summary

Mappings for EditBypass0124

<a name='T-VRDR-ValueSets-EditBypass0124'></a>
## EditBypass0124 `type`

##### Namespace

VRDR.ValueSets

##### Summary

EditBypass0124

<a name='F-VRDR-Mappings-EditBypass0124-FHIRToIJE'></a>
### FHIRToIJE `constants`

##### Summary

FHIR -> IJE Mapping for EditBypass0124

<a name='F-VRDR-Mappings-EditBypass0124-IJEToFHIR'></a>
### IJEToFHIR `constants`

##### Summary

IJE -> FHIR Mapping for EditBypass0124

<a name='F-VRDR-ValueSets-EditBypass0124-Codes'></a>
### Codes `constants`

##### Summary

Codes

<a name='F-VRDR-ValueSets-EditBypass0124-Edit_Failed_Data_Queried_And_Verified'></a>
### Edit_Failed_Data_Queried_And_Verified `constants`

##### Summary

Edit_Failed_Data_Queried_And_Verified

<a name='F-VRDR-ValueSets-EditBypass0124-Edit_Failed_Data_Queried_But_Not_Verified'></a>
### Edit_Failed_Data_Queried_But_Not_Verified `constants`

##### Summary

Edit_Failed_Data_Queried_But_Not_Verified

<a name='F-VRDR-ValueSets-EditBypass0124-Edit_Failed_Query_Needed'></a>
### Edit_Failed_Query_Needed `constants`

##### Summary

Edit_Failed_Query_Needed

<a name='F-VRDR-ValueSets-EditBypass0124-Edit_Passed'></a>
### Edit_Passed `constants`

##### Summary

Edit_Passed

<a name='T-VRDR-Mappings-EducationLevel'></a>
## EducationLevel `type`

##### Namespace

VRDR.Mappings

##### Summary

Mappings for EducationLevel

<a name='T-VRDR-ValueSets-EducationLevel'></a>
## EducationLevel `type`

##### Namespace

VRDR.ValueSets

##### Summary

EducationLevel

<a name='F-VRDR-Mappings-EducationLevel-FHIRToIJE'></a>
### FHIRToIJE `constants`

##### Summary

FHIR -> IJE Mapping for EducationLevel

<a name='F-VRDR-Mappings-EducationLevel-IJEToFHIR'></a>
### IJEToFHIR `constants`

##### Summary

IJE -> FHIR Mapping for EducationLevel

<a name='F-VRDR-ValueSets-EducationLevel-Associates_Or_Technical_Degree_Complete'></a>
### Associates_Or_Technical_Degree_Complete `constants`

##### Summary

Associates_Or_Technical_Degree_Complete

<a name='F-VRDR-ValueSets-EducationLevel-Bachelors_Degree'></a>
### Bachelors_Degree `constants`

##### Summary

Bachelors_Degree

<a name='F-VRDR-ValueSets-EducationLevel-Codes'></a>
### Codes `constants`

##### Summary

Codes

<a name='F-VRDR-ValueSets-EducationLevel-Doctoral_Or_Post_Graduate_Education'></a>
### Doctoral_Or_Post_Graduate_Education `constants`

##### Summary

Doctoral_Or_Post_Graduate_Education

<a name='F-VRDR-ValueSets-EducationLevel-Elementary_School'></a>
### Elementary_School `constants`

##### Summary

Elementary_School

<a name='F-VRDR-ValueSets-EducationLevel-High_School_Or_Secondary_School_Degree_Complete'></a>
### High_School_Or_Secondary_School_Degree_Complete `constants`

##### Summary

High_School_Or_Secondary_School_Degree_Complete

<a name='F-VRDR-ValueSets-EducationLevel-Masters_Degree'></a>
### Masters_Degree `constants`

##### Summary

Masters_Degree

<a name='F-VRDR-ValueSets-EducationLevel-Some_College_Education'></a>
### Some_College_Education `constants`

##### Summary

Some_College_Education

<a name='F-VRDR-ValueSets-EducationLevel-Some_Secondary_Or_High_School_Education'></a>
### Some_Secondary_Or_High_School_Education `constants`

##### Summary

Some_Secondary_Or_High_School_Education

<a name='F-VRDR-ValueSets-EducationLevel-Unknown'></a>
### Unknown `constants`

##### Summary

Unknown

<a name='T-VRDR-ExtensionURL'></a>
## ExtensionURL `type`

##### Namespace

VRDR

##### Summary

Extension URLs

<a name='F-VRDR-ExtensionURL-AuxiliaryStateIdentifier1'></a>
### AuxiliaryStateIdentifier1 `constants`

##### Summary

URL for AuxiliaryStateIdentifier1

<a name='F-VRDR-ExtensionURL-AuxiliaryStateIdentifier2'></a>
### AuxiliaryStateIdentifier2 `constants`

##### Summary

URL for AuxiliaryStateIdentifier2

<a name='F-VRDR-ExtensionURL-BypassEditFlag'></a>
### BypassEditFlag `constants`

##### Summary

URL for BypassEditFlag

<a name='F-VRDR-ExtensionURL-CertificateNumber'></a>
### CertificateNumber `constants`

##### Summary

URL for CertificateNumber

<a name='F-VRDR-ExtensionURL-CityCode'></a>
### CityCode `constants`

##### Summary

URL for CityCode

<a name='F-VRDR-ExtensionURL-DateDay'></a>
### DateDay `constants`

##### Summary

URL for DateDay

<a name='F-VRDR-ExtensionURL-DateMonth'></a>
### DateMonth `constants`

##### Summary

URL for DateMonth

<a name='F-VRDR-ExtensionURL-DateTime'></a>
### DateTime `constants`

##### Summary

URL for DateTime

<a name='F-VRDR-ExtensionURL-DateYear'></a>
### DateYear `constants`

##### Summary

URL for DateYear

<a name='F-VRDR-ExtensionURL-DistrictCode'></a>
### DistrictCode `constants`

##### Summary

URL for DistrictCode

<a name='F-VRDR-ExtensionURL-FilingFormat'></a>
### FilingFormat `constants`

##### Summary

URL for FilingFormat

<a name='F-VRDR-ExtensionURL-LocationJurisdictionId'></a>
### LocationJurisdictionId `constants`

##### Summary

URL for LocationJurisdictionId

<a name='F-VRDR-ExtensionURL-NVSSSexAtDeath'></a>
### NVSSSexAtDeath `constants`

##### Summary

URL for NVSSSexAtDeath

<a name='F-VRDR-ExtensionURL-PartialDate'></a>
### PartialDate `constants`

##### Summary

URL for PartialDate

<a name='F-VRDR-ExtensionURL-PartialDateTime'></a>
### PartialDateTime `constants`

##### Summary

URL for PartialDateTime

<a name='F-VRDR-ExtensionURL-PostDirectional'></a>
### PostDirectional `constants`

##### Summary

URL for PostDirectional

<a name='F-VRDR-ExtensionURL-PreDirectional'></a>
### PreDirectional `constants`

##### Summary

URL for PreDirectional

<a name='F-VRDR-ExtensionURL-ReplaceStatus'></a>
### ReplaceStatus `constants`

##### Summary

URL for ReplaceStatus

<a name='F-VRDR-ExtensionURL-SpouseAlive'></a>
### SpouseAlive `constants`

##### Summary

URL for SpouseAlive

<a name='F-VRDR-ExtensionURL-StateSpecificField'></a>
### StateSpecificField `constants`

##### Summary

URL for StateSpecificField

<a name='F-VRDR-ExtensionURL-StreetDesignator'></a>
### StreetDesignator `constants`

##### Summary

URL for StreetDesignator

<a name='F-VRDR-ExtensionURL-StreetName'></a>
### StreetName `constants`

##### Summary

URL for StreetName

<a name='F-VRDR-ExtensionURL-StreetNumber'></a>
### StreetNumber `constants`

##### Summary

URL for StreetNumber

<a name='F-VRDR-ExtensionURL-UnitOrAptNumber'></a>
### UnitOrAptNumber `constants`

##### Summary

URL for UnitOrAptNumber

<a name='F-VRDR-ExtensionURL-WithinCityLimitsIndicator'></a>
### WithinCityLimitsIndicator `constants`

##### Summary

URL for WithinCityLimitsIndicator

<a name='T-VRDR-FHIRPath'></a>
## FHIRPath `type`

##### Namespace

VRDR

##### Summary

Describes a FHIR path that can be used to get to the element.

<a name='M-VRDR-FHIRPath-#ctor-System-String,System-String-'></a>
### #ctor() `constructor`

##### Summary

Constructor.

##### Parameters

This constructor has no parameters.

<a name='F-VRDR-FHIRPath-Element'></a>
### Element `constants`

##### Summary

The relevant element.

<a name='F-VRDR-FHIRPath-Path'></a>
### Path `constants`

##### Summary

The relevant FHIR path.

<a name='T-VRDR-Mappings-FilingFormat'></a>
## FilingFormat `type`

##### Namespace

VRDR.Mappings

##### Summary

Mappings for FilingFormat

<a name='T-VRDR-ValueSets-FilingFormat'></a>
## FilingFormat `type`

##### Namespace

VRDR.ValueSets

##### Summary

FilingFormat

<a name='F-VRDR-Mappings-FilingFormat-FHIRToIJE'></a>
### FHIRToIJE `constants`

##### Summary

FHIR -> IJE Mapping for FilingFormat

<a name='F-VRDR-Mappings-FilingFormat-IJEToFHIR'></a>
### IJEToFHIR `constants`

##### Summary

IJE -> FHIR Mapping for FilingFormat

<a name='F-VRDR-ValueSets-FilingFormat-Codes'></a>
### Codes `constants`

##### Summary

Codes

<a name='F-VRDR-ValueSets-FilingFormat-Electronic'></a>
### Electronic `constants`

##### Summary

Electronic

<a name='F-VRDR-ValueSets-FilingFormat-Mixed'></a>
### Mixed `constants`

##### Summary

Mixed

<a name='F-VRDR-ValueSets-FilingFormat-Paper'></a>
### Paper `constants`

##### Summary

Paper

<a name='T-VRDR-Mappings-HispanicNoUnknown'></a>
## HispanicNoUnknown `type`

##### Namespace

VRDR.Mappings

##### Summary

Mappings for HispanicNoUnknown

<a name='T-VRDR-ValueSets-HispanicNoUnknown'></a>
## HispanicNoUnknown `type`

##### Namespace

VRDR.ValueSets

##### Summary

HispanicNoUnknown

<a name='F-VRDR-Mappings-HispanicNoUnknown-FHIRToIJE'></a>
### FHIRToIJE `constants`

##### Summary

FHIR -> IJE Mapping for HispanicNoUnknown

<a name='F-VRDR-Mappings-HispanicNoUnknown-IJEToFHIR'></a>
### IJEToFHIR `constants`

##### Summary

IJE -> FHIR Mapping for HispanicNoUnknown

<a name='F-VRDR-ValueSets-HispanicNoUnknown-Codes'></a>
### Codes `constants`

##### Summary

Codes

<a name='F-VRDR-ValueSets-HispanicNoUnknown-No'></a>
### No `constants`

##### Summary

No

<a name='F-VRDR-ValueSets-HispanicNoUnknown-Unknown'></a>
### Unknown `constants`

##### Summary

Unknown

<a name='F-VRDR-ValueSets-HispanicNoUnknown-Yes'></a>
### Yes `constants`

##### Summary

Yes

<a name='T-VRDR-Mappings-HispanicOrigin'></a>
## HispanicOrigin `type`

##### Namespace

VRDR.Mappings

##### Summary

Mappings for HispanicOrigin

<a name='T-VRDR-ValueSets-HispanicOrigin'></a>
## HispanicOrigin `type`

##### Namespace

VRDR.ValueSets

##### Summary

HispanicOrigin

<a name='F-VRDR-Mappings-HispanicOrigin-FHIRToIJE'></a>
### FHIRToIJE `constants`

##### Summary

FHIR -> IJE Mapping for HispanicOrigin

<a name='F-VRDR-Mappings-HispanicOrigin-IJEToFHIR'></a>
### IJEToFHIR `constants`

##### Summary

IJE -> FHIR Mapping for HispanicOrigin

<a name='F-VRDR-ValueSets-HispanicOrigin-Andalusian'></a>
### Andalusian `constants`

##### Summary

Andalusian

<a name='F-VRDR-ValueSets-HispanicOrigin-Argentinean'></a>
### Argentinean `constants`

##### Summary

Argentinean

<a name='F-VRDR-ValueSets-HispanicOrigin-Asturian'></a>
### Asturian `constants`

##### Summary

Asturian

<a name='F-VRDR-ValueSets-HispanicOrigin-Balearic_Islander'></a>
### Balearic_Islander `constants`

##### Summary

Balearic_Islander

<a name='F-VRDR-ValueSets-HispanicOrigin-Bolivian'></a>
### Bolivian `constants`

##### Summary

Bolivian

<a name='F-VRDR-ValueSets-HispanicOrigin-Californio'></a>
### Californio `constants`

##### Summary

Californio

<a name='F-VRDR-ValueSets-HispanicOrigin-Canal_Zone'></a>
### Canal_Zone `constants`

##### Summary

Canal_Zone

<a name='F-VRDR-ValueSets-HispanicOrigin-Canarian'></a>
### Canarian `constants`

##### Summary

Canarian

<a name='F-VRDR-ValueSets-HispanicOrigin-Caribbean'></a>
### Caribbean `constants`

##### Summary

Caribbean

<a name='F-VRDR-ValueSets-HispanicOrigin-Castillian'></a>
### Castillian `constants`

##### Summary

Castillian

<a name='F-VRDR-ValueSets-HispanicOrigin-Catalonian'></a>
### Catalonian `constants`

##### Summary

Catalonian

<a name='F-VRDR-ValueSets-HispanicOrigin-Central_American'></a>
### Central_American `constants`

##### Summary

Central_American

<a name='F-VRDR-ValueSets-HispanicOrigin-Central_American_Indian'></a>
### Central_American_Indian `constants`

##### Summary

Central_American_Indian

<a name='F-VRDR-ValueSets-HispanicOrigin-Central_And_South_America'></a>
### Central_And_South_America `constants`

##### Summary

Central_And_South_America

<a name='F-VRDR-ValueSets-HispanicOrigin-Chicano'></a>
### Chicano `constants`

##### Summary

Chicano

<a name='F-VRDR-ValueSets-HispanicOrigin-Chilean'></a>
### Chilean `constants`

##### Summary

Chilean

<a name='F-VRDR-ValueSets-HispanicOrigin-Codes'></a>
### Codes `constants`

##### Summary

Codes

<a name='F-VRDR-ValueSets-HispanicOrigin-Colombian'></a>
### Colombian `constants`

##### Summary

Colombian

<a name='F-VRDR-ValueSets-HispanicOrigin-Costa_Rican'></a>
### Costa_Rican `constants`

##### Summary

Costa_Rican

<a name='F-VRDR-ValueSets-HispanicOrigin-Criollo'></a>
### Criollo `constants`

##### Summary

Criollo

<a name='F-VRDR-ValueSets-HispanicOrigin-Cuban'></a>
### Cuban `constants`

##### Summary

Cuban

<a name='F-VRDR-ValueSets-HispanicOrigin-Cuban_2'></a>
### Cuban_2 `constants`

##### Summary

Cuban_2

<a name='F-VRDR-ValueSets-HispanicOrigin-Deferred'></a>
### Deferred `constants`

##### Summary

Deferred

<a name='F-VRDR-ValueSets-HispanicOrigin-Dominican'></a>
### Dominican `constants`

##### Summary

Dominican

<a name='F-VRDR-ValueSets-HispanicOrigin-Ecuadorian'></a>
### Ecuadorian `constants`

##### Summary

Ecuadorian

<a name='F-VRDR-ValueSets-HispanicOrigin-First_Pass_Reject'></a>
### First_Pass_Reject `constants`

##### Summary

First_Pass_Reject

<a name='F-VRDR-ValueSets-HispanicOrigin-Gallego'></a>
### Gallego `constants`

##### Summary

Gallego

<a name='F-VRDR-ValueSets-HispanicOrigin-Guatemalan'></a>
### Guatemalan `constants`

##### Summary

Guatemalan

<a name='F-VRDR-ValueSets-HispanicOrigin-Hispanic'></a>
### Hispanic `constants`

##### Summary

Hispanic

<a name='F-VRDR-ValueSets-HispanicOrigin-Honduran'></a>
### Honduran `constants`

##### Summary

Honduran

<a name='F-VRDR-ValueSets-HispanicOrigin-La_Raza'></a>
### La_Raza `constants`

##### Summary

La_Raza

<a name='F-VRDR-ValueSets-HispanicOrigin-Latin'></a>
### Latin `constants`

##### Summary

Latin

<a name='F-VRDR-ValueSets-HispanicOrigin-Latin_American'></a>
### Latin_American `constants`

##### Summary

Latin_American

<a name='F-VRDR-ValueSets-HispanicOrigin-Latino'></a>
### Latino `constants`

##### Summary

Latino

<a name='F-VRDR-ValueSets-HispanicOrigin-Meso_American_Indian'></a>
### Meso_American_Indian `constants`

##### Summary

Meso_American_Indian

<a name='F-VRDR-ValueSets-HispanicOrigin-Mestizo'></a>
### Mestizo `constants`

##### Summary

Mestizo

<a name='F-VRDR-ValueSets-HispanicOrigin-Mexican'></a>
### Mexican `constants`

##### Summary

Mexican

<a name='F-VRDR-ValueSets-HispanicOrigin-Mexican_2'></a>
### Mexican_2 `constants`

##### Summary

Mexican_2

<a name='F-VRDR-ValueSets-HispanicOrigin-Mexican_American'></a>
### Mexican_American `constants`

##### Summary

Mexican_American

<a name='F-VRDR-ValueSets-HispanicOrigin-Mexican_American_Indian'></a>
### Mexican_American_Indian `constants`

##### Summary

Mexican_American_Indian

<a name='F-VRDR-ValueSets-HispanicOrigin-Mexicano'></a>
### Mexicano `constants`

##### Summary

Mexicano

<a name='F-VRDR-ValueSets-HispanicOrigin-Mexico'></a>
### Mexico `constants`

##### Summary

Mexico

<a name='F-VRDR-ValueSets-HispanicOrigin-Multiple_Hispanic_Responses'></a>
### Multiple_Hispanic_Responses `constants`

##### Summary

Multiple_Hispanic_Responses

<a name='F-VRDR-ValueSets-HispanicOrigin-Nicaraguan'></a>
### Nicaraguan `constants`

##### Summary

Nicaraguan

<a name='F-VRDR-ValueSets-HispanicOrigin-Not_Hispanic'></a>
### Not_Hispanic `constants`

##### Summary

Not_Hispanic

<a name='F-VRDR-ValueSets-HispanicOrigin-Nuevo_Mexicano'></a>
### Nuevo_Mexicano `constants`

##### Summary

Nuevo_Mexicano

<a name='F-VRDR-ValueSets-HispanicOrigin-Other_Spanish'></a>
### Other_Spanish `constants`

##### Summary

Other_Spanish

<a name='F-VRDR-ValueSets-HispanicOrigin-Other_Spanish_2'></a>
### Other_Spanish_2 `constants`

##### Summary

Other_Spanish_2

<a name='F-VRDR-ValueSets-HispanicOrigin-Panamanian'></a>
### Panamanian `constants`

##### Summary

Panamanian

<a name='F-VRDR-ValueSets-HispanicOrigin-Paraguayan'></a>
### Paraguayan `constants`

##### Summary

Paraguayan

<a name='F-VRDR-ValueSets-HispanicOrigin-Peruvian'></a>
### Peruvian `constants`

##### Summary

Peruvian

<a name='F-VRDR-ValueSets-HispanicOrigin-Puerto_Rican'></a>
### Puerto_Rican `constants`

##### Summary

Puerto_Rican

<a name='F-VRDR-ValueSets-HispanicOrigin-Puerto_Rican_2'></a>
### Puerto_Rican_2 `constants`

##### Summary

Puerto_Rican_2

<a name='F-VRDR-ValueSets-HispanicOrigin-Salvadoran'></a>
### Salvadoran `constants`

##### Summary

Salvadoran

<a name='F-VRDR-ValueSets-HispanicOrigin-South_American'></a>
### South_American `constants`

##### Summary

South_American

<a name='F-VRDR-ValueSets-HispanicOrigin-South_American_Indian'></a>
### South_American_Indian `constants`

##### Summary

South_American_Indian

<a name='F-VRDR-ValueSets-HispanicOrigin-Spaniard'></a>
### Spaniard `constants`

##### Summary

Spaniard

<a name='F-VRDR-ValueSets-HispanicOrigin-Spanish'></a>
### Spanish `constants`

##### Summary

Spanish

<a name='F-VRDR-ValueSets-HispanicOrigin-Spanish_American'></a>
### Spanish_American `constants`

##### Summary

Spanish_American

<a name='F-VRDR-ValueSets-HispanicOrigin-Spanish_American_Indian'></a>
### Spanish_American_Indian `constants`

##### Summary

Spanish_American_Indian

<a name='F-VRDR-ValueSets-HispanicOrigin-Spanish_Basque'></a>
### Spanish_Basque `constants`

##### Summary

Spanish_Basque

<a name='F-VRDR-ValueSets-HispanicOrigin-Tejano'></a>
### Tejano `constants`

##### Summary

Tejano

<a name='F-VRDR-ValueSets-HispanicOrigin-Uncodable'></a>
### Uncodable `constants`

##### Summary

Uncodable

<a name='F-VRDR-ValueSets-HispanicOrigin-Unknown'></a>
### Unknown `constants`

##### Summary

Unknown

<a name='F-VRDR-ValueSets-HispanicOrigin-Uruguayan'></a>
### Uruguayan `constants`

##### Summary

Uruguayan

<a name='F-VRDR-ValueSets-HispanicOrigin-Valencian'></a>
### Valencian `constants`

##### Summary

Valencian

<a name='F-VRDR-ValueSets-HispanicOrigin-Venezuelan'></a>
### Venezuelan `constants`

##### Summary

Venezuelan

<a name='T-VRDR-IGURL'></a>
## IGURL `type`

##### Namespace

VRDR

##### Summary

IG URLs

<a name='F-VRDR-IGURL-ActivityAtTimeOfDeath'></a>
### ActivityAtTimeOfDeath `constants`

##### Summary

URL for ActivityAtTimeOfDeath

<a name='F-VRDR-IGURL-AutomatedUnderlyingCauseOfDeath'></a>
### AutomatedUnderlyingCauseOfDeath `constants`

##### Summary

URL for AutomatedUnderlyingCauseOfDeath

<a name='F-VRDR-IGURL-AutopsyPerformedIndicator'></a>
### AutopsyPerformedIndicator `constants`

##### Summary

URL for AutopsyPerformedIndicator

<a name='F-VRDR-IGURL-AuxiliaryStateIdentifier1'></a>
### AuxiliaryStateIdentifier1 `constants`

##### Summary

URL for AuxiliaryStateIdentifier1

<a name='F-VRDR-IGURL-AuxiliaryStateIdentifier2'></a>
### AuxiliaryStateIdentifier2 `constants`

##### Summary

URL for AuxiliaryStateIdentifier2

<a name='F-VRDR-IGURL-BirthRecordIdentifier'></a>
### BirthRecordIdentifier `constants`

##### Summary

URL for BirthRecordIdentifier

<a name='F-VRDR-IGURL-BypassEditFlag'></a>
### BypassEditFlag `constants`

##### Summary

URL for BypassEditFlag

<a name='F-VRDR-IGURL-CauseOfDeathCodedContentBundle'></a>
### CauseOfDeathCodedContentBundle `constants`

##### Summary

URL for CauseOfDeathCodedContentBundle

<a name='F-VRDR-IGURL-CauseOfDeathPart1'></a>
### CauseOfDeathPart1 `constants`

##### Summary

URL for CauseOfDeathPart1

<a name='F-VRDR-IGURL-CauseOfDeathPart2'></a>
### CauseOfDeathPart2 `constants`

##### Summary

URL for CauseOfDeathPart2

<a name='F-VRDR-IGURL-CauseOfDeathPathway'></a>
### CauseOfDeathPathway `constants`

##### Summary

URL for CauseOfDeathPathway

<a name='F-VRDR-IGURL-CertificateNumber'></a>
### CertificateNumber `constants`

##### Summary

URL for CertificateNumber

<a name='F-VRDR-IGURL-Certifier'></a>
### Certifier `constants`

##### Summary

URL for Certifier

<a name='F-VRDR-IGURL-CityCode'></a>
### CityCode `constants`

##### Summary

URL for CityCode

<a name='F-VRDR-IGURL-CodedRaceAndEthnicity'></a>
### CodedRaceAndEthnicity `constants`

##### Summary

URL for CodedRaceAndEthnicity

<a name='F-VRDR-IGURL-CodingStatusValues'></a>
### CodingStatusValues `constants`

##### Summary

URL for CodingStatusValues

<a name='F-VRDR-IGURL-DateDay'></a>
### DateDay `constants`

##### Summary

URL for DateDay

<a name='F-VRDR-IGURL-DateMonth'></a>
### DateMonth `constants`

##### Summary

URL for DateMonth

<a name='F-VRDR-IGURL-DateTime'></a>
### DateTime `constants`

##### Summary

URL for DateTime

<a name='F-VRDR-IGURL-DateYear'></a>
### DateYear `constants`

##### Summary

URL for DateYear

<a name='F-VRDR-IGURL-DeathCertificate'></a>
### DeathCertificate `constants`

##### Summary

URL for DeathCertificate

<a name='F-VRDR-IGURL-DeathCertificateDocument'></a>
### DeathCertificateDocument `constants`

##### Summary

URL for DeathCertificateDocument

<a name='F-VRDR-IGURL-DeathCertification'></a>
### DeathCertification `constants`

##### Summary

URL for DeathCertification

<a name='F-VRDR-IGURL-DeathDate'></a>
### DeathDate `constants`

##### Summary

URL for DeathDate

<a name='F-VRDR-IGURL-DeathLocation'></a>
### DeathLocation `constants`

##### Summary

URL for DeathLocation

<a name='F-VRDR-IGURL-Decedent'></a>
### Decedent `constants`

##### Summary

URL for Decedent

<a name='F-VRDR-IGURL-DecedentAge'></a>
### DecedentAge `constants`

##### Summary

URL for DecedentAge

<a name='F-VRDR-IGURL-DecedentDispositionMethod'></a>
### DecedentDispositionMethod `constants`

##### Summary

URL for DecedentDispositionMethod

<a name='F-VRDR-IGURL-DecedentEducationLevel'></a>
### DecedentEducationLevel `constants`

##### Summary

URL for DecedentEducationLevel

<a name='F-VRDR-IGURL-DecedentFather'></a>
### DecedentFather `constants`

##### Summary

URL for DecedentFather

<a name='F-VRDR-IGURL-DecedentMilitaryService'></a>
### DecedentMilitaryService `constants`

##### Summary

URL for DecedentMilitaryService

<a name='F-VRDR-IGURL-DecedentMother'></a>
### DecedentMother `constants`

##### Summary

URL for DecedentMother

<a name='F-VRDR-IGURL-DecedentPregnancyStatus'></a>
### DecedentPregnancyStatus `constants`

##### Summary

URL for DecedentPregnancyStatus

<a name='F-VRDR-IGURL-DecedentSpouse'></a>
### DecedentSpouse `constants`

##### Summary

URL for DecedentSpouse

<a name='F-VRDR-IGURL-DecedentUsualWork'></a>
### DecedentUsualWork `constants`

##### Summary

URL for DecedentUsualWork

<a name='F-VRDR-IGURL-DemographicCodedContentBundle'></a>
### DemographicCodedContentBundle `constants`

##### Summary

URL for DemographicCodedContentBundle

<a name='F-VRDR-IGURL-DispositionLocation'></a>
### DispositionLocation `constants`

##### Summary

URL for DispositionLocation

<a name='F-VRDR-IGURL-DistrictCode'></a>
### DistrictCode `constants`

##### Summary

URL for DistrictCode

<a name='F-VRDR-IGURL-EmergingIssues'></a>
### EmergingIssues `constants`

##### Summary

URL for EmergingIssues

<a name='F-VRDR-IGURL-EntityAxisCauseOfDeath'></a>
### EntityAxisCauseOfDeath `constants`

##### Summary

URL for EntityAxisCauseOfDeath

<a name='F-VRDR-IGURL-ExaminerContacted'></a>
### ExaminerContacted `constants`

##### Summary

URL for ExaminerContacted

<a name='F-VRDR-IGURL-FilingFormat'></a>
### FilingFormat `constants`

##### Summary

URL for FilingFormat

<a name='F-VRDR-IGURL-FuneralHome'></a>
### FuneralHome `constants`

##### Summary

URL for FuneralHome

<a name='F-VRDR-IGURL-InjuryIncident'></a>
### InjuryIncident `constants`

##### Summary

URL for InjuryIncident

<a name='F-VRDR-IGURL-InjuryLocation'></a>
### InjuryLocation `constants`

##### Summary

URL for InjuryLocation

<a name='F-VRDR-IGURL-InputRaceAndEthnicity'></a>
### InputRaceAndEthnicity `constants`

##### Summary

URL for InputRaceAndEthnicity

<a name='F-VRDR-IGURL-LocationJurisdictionId'></a>
### LocationJurisdictionId `constants`

##### Summary

URL for LocationJurisdictionId

<a name='F-VRDR-IGURL-MannerOfDeath'></a>
### MannerOfDeath `constants`

##### Summary

URL for MannerOfDeath

<a name='F-VRDR-IGURL-ManualUnderlyingCauseOfDeath'></a>
### ManualUnderlyingCauseOfDeath `constants`

##### Summary

URL for ManualUnderlyingCauseOfDeath

<a name='F-VRDR-IGURL-NVSSSexAtDeath'></a>
### NVSSSexAtDeath `constants`

##### Summary

URL for NVSSSexAtDeath

<a name='F-VRDR-IGURL-PartialDate'></a>
### PartialDate `constants`

##### Summary

URL for PartialDate

<a name='F-VRDR-IGURL-PartialDateTime'></a>
### PartialDateTime `constants`

##### Summary

URL for PartialDateTime

<a name='F-VRDR-IGURL-PlaceOfInjury'></a>
### PlaceOfInjury `constants`

##### Summary

URL for PlaceOfInjury

<a name='F-VRDR-IGURL-PostDirectional'></a>
### PostDirectional `constants`

##### Summary

URL for PostDirectional

<a name='F-VRDR-IGURL-PreDirectional'></a>
### PreDirectional `constants`

##### Summary

URL for PreDirectional

<a name='F-VRDR-IGURL-RecordAxisCauseOfDeath'></a>
### RecordAxisCauseOfDeath `constants`

##### Summary

URL for RecordAxisCauseOfDeath

<a name='F-VRDR-IGURL-ReplaceStatus'></a>
### ReplaceStatus `constants`

##### Summary

URL for ReplaceStatus

<a name='F-VRDR-IGURL-SpouseAlive'></a>
### SpouseAlive `constants`

##### Summary

URL for SpouseAlive

<a name='F-VRDR-IGURL-StateSpecificField'></a>
### StateSpecificField `constants`

##### Summary

URL for StateSpecificField

<a name='F-VRDR-IGURL-StreetDesignator'></a>
### StreetDesignator `constants`

##### Summary

URL for StreetDesignator

<a name='F-VRDR-IGURL-StreetName'></a>
### StreetName `constants`

##### Summary

URL for StreetName

<a name='F-VRDR-IGURL-StreetNumber'></a>
### StreetNumber `constants`

##### Summary

URL for StreetNumber

<a name='F-VRDR-IGURL-SurgeryDate'></a>
### SurgeryDate `constants`

##### Summary

URL for SurgeryDate

<a name='F-VRDR-IGURL-TobaccoUseContributedToDeath'></a>
### TobaccoUseContributedToDeath `constants`

##### Summary

URL for TobaccoUseContributedToDeath

<a name='F-VRDR-IGURL-UnitOrAptNumber'></a>
### UnitOrAptNumber `constants`

##### Summary

URL for UnitOrAptNumber

<a name='F-VRDR-IGURL-WithinCityLimitsIndicator'></a>
### WithinCityLimitsIndicator `constants`

##### Summary

URL for WithinCityLimitsIndicator

<a name='T-VRDR-IJEField'></a>
## IJEField `type`

##### Namespace

VRDR

##### Summary

Property attribute used to describe a field in the IJE Mortality format.

<a name='M-VRDR-IJEField-#ctor-System-Int32,System-Int32,System-Int32,System-String,System-String,System-Int32-'></a>
### #ctor() `constructor`

##### Summary

Constructor.

##### Parameters

This constructor has no parameters.

<a name='F-VRDR-IJEField-Contents'></a>
### Contents `constants`

##### Summary

Description of what the field contains.

<a name='F-VRDR-IJEField-Field'></a>
### Field `constants`

##### Summary

Field number.

<a name='F-VRDR-IJEField-Length'></a>
### Length `constants`

##### Summary

Field length.

<a name='F-VRDR-IJEField-Location'></a>
### Location `constants`

##### Summary

Beginning location.

<a name='F-VRDR-IJEField-Name'></a>
### Name `constants`

##### Summary

Field name.

<a name='F-VRDR-IJEField-Priority'></a>
### Priority `constants`

##### Summary

Priority - lower will be "GET" and "SET" earlier.

<a name='T-VRDR-IJEMortality'></a>
## IJEMortality `type`

##### Namespace

VRDR

##### Summary

A "wrapper" class to convert between a FHIR based `DeathRecord` and
a record in IJE Mortality format. Each property of this class corresponds exactly
with a field in the IJE Mortality format. The getters convert from the embedded
FHIR based `DeathRecord` to the IJE format for a specific field, and
the setters convert from IJE format for a specific field and set that value
on the embedded FHIR based `DeathRecord`.

<a name='M-VRDR-IJEMortality-#ctor-VRDR-DeathRecord,System-Boolean-'></a>
### #ctor() `constructor`

##### Summary

Constructor that takes a `DeathRecord`.

##### Parameters

This constructor has no parameters.

<a name='M-VRDR-IJEMortality-#ctor-System-String,System-Boolean-'></a>
### #ctor() `constructor`

##### Summary

Constructor that takes an IJE string and builds a corresponding internal `DeathRecord`.

##### Parameters

This constructor has no parameters.

<a name='M-VRDR-IJEMortality-#ctor'></a>
### #ctor() `constructor`

##### Summary

Constructor that creates a completely empty record for constructing records using the IJE properties.

##### Parameters

This constructor has no parameters.

<a name='F-VRDR-IJEMortality-dataLookup'></a>
### dataLookup `constants`

##### Summary

IJE data lookup helper. Thread-safe singleton!

<a name='F-VRDR-IJEMortality-mre'></a>
### mre `constants`

##### Summary

Utility location to provide support for setting MRE-only fields that have no mapping in IJE when creating coding response records

<a name='F-VRDR-IJEMortality-record'></a>
### record `constants`

##### Summary

FHIR based death record.

<a name='F-VRDR-IJEMortality-trx'></a>
### trx `constants`

##### Summary

Utility location to provide support for setting TRX-only fields that have no mapping in IJE when creating coding response records

<a name='F-VRDR-IJEMortality-validationErrors'></a>
### validationErrors `constants`

##### Summary

Validation errors encountered while converting a record

<a name='P-VRDR-IJEMortality-ACME_UC'></a>
### ACME_UC `property`

##### Summary

ACME Underlying Cause

<a name='P-VRDR-IJEMortality-ADDRESS_D'></a>
### ADDRESS_D `property`

##### Summary

Long String address for place of death

<a name='P-VRDR-IJEMortality-ADDRESS_R'></a>
### ADDRESS_R `property`

##### Summary

Long string address for decedent's place of residence same as above but allows states to choose the way they capture information.

<a name='P-VRDR-IJEMortality-AGE'></a>
### AGE `property`

##### Summary

Decedent's Age--Units

<a name='P-VRDR-IJEMortality-AGETYPE'></a>
### AGETYPE `property`

##### Summary

Decedent's Age--Type

<a name='P-VRDR-IJEMortality-AGE_BYPASS'></a>
### AGE_BYPASS `property`

##### Summary

Decedent's Age--Edit Flag

<a name='P-VRDR-IJEMortality-ALIAS'></a>
### ALIAS `property`

##### Summary

Decedent's Legal Name--Alias

<a name='P-VRDR-IJEMortality-ARMEDF'></a>
### ARMEDF `property`

##### Summary

Decedent ever served in Armed Forces?

<a name='P-VRDR-IJEMortality-AUTOP'></a>
### AUTOP `property`

##### Summary

Was Autopsy performed

<a name='P-VRDR-IJEMortality-AUTOPF'></a>
### AUTOPF `property`

##### Summary

Were Autopsy Findings Available to Complete the Cause of Death?

<a name='P-VRDR-IJEMortality-AUXNO'></a>
### AUXNO `property`

##### Summary

Auxiliary State file number

<a name='P-VRDR-IJEMortality-AUXNO2'></a>
### AUXNO2 `property`

##### Summary

Auxiliary State file number

<a name='P-VRDR-IJEMortality-BCNO'></a>
### BCNO `property`

##### Summary

Infant Death/Birth Linking - birth certificate number

<a name='P-VRDR-IJEMortality-BLANK1'></a>
### BLANK1 `property`

##### Summary

For possible future change in transax

<a name='P-VRDR-IJEMortality-BLANK2'></a>
### BLANK2 `property`

##### Summary

Blank for future expansion

<a name='P-VRDR-IJEMortality-BLANK3'></a>
### BLANK3 `property`

##### Summary

Blank for Jurisdictional Use Only

<a name='P-VRDR-IJEMortality-BPLACE_CNT'></a>
### BPLACE_CNT `property`

##### Summary

Birthplace--Country

<a name='P-VRDR-IJEMortality-BPLACE_ST'></a>
### BPLACE_ST `property`

##### Summary

State, U.S. Territory or Canadian Province of Birth - code

<a name='P-VRDR-IJEMortality-BSTATE'></a>
### BSTATE `property`

##### Summary

Infant Death/Birth Linking - Birth state

<a name='P-VRDR-IJEMortality-CERTADDRESS'></a>
### CERTADDRESS `property`

##### Summary

Long string address for Certifier same as above but allows states to choose the way they capture information.

<a name='P-VRDR-IJEMortality-CERTCITYTEXT'></a>
### CERTCITYTEXT `property`

##### Summary

Certifier - City or Town name

<a name='P-VRDR-IJEMortality-CERTDATE'></a>
### CERTDATE `property`

##### Summary

Certifier Date Signed

<a name='P-VRDR-IJEMortality-CERTFIRST'></a>
### CERTFIRST `property`

##### Summary

Certifier's First Name

<a name='P-VRDR-IJEMortality-CERTL'></a>
### CERTL `property`

##### Summary

Title of Certifier

<a name='P-VRDR-IJEMortality-CERTLAST'></a>
### CERTLAST `property`

##### Summary

Certifier's Last Name

<a name='P-VRDR-IJEMortality-CERTMIDDLE'></a>
### CERTMIDDLE `property`

##### Summary

Certifier's Middle Name

<a name='P-VRDR-IJEMortality-CERTPOSTDIR'></a>
### CERTPOSTDIR `property`

##### Summary

Certifier - Post Directional

<a name='P-VRDR-IJEMortality-CERTPREDIR'></a>
### CERTPREDIR `property`

##### Summary

Certifier - Pre Directional

<a name='P-VRDR-IJEMortality-CERTSTATE'></a>
### CERTSTATE `property`

##### Summary

State, U.S. Territory or Canadian Province of Certifier - literal

<a name='P-VRDR-IJEMortality-CERTSTATECD'></a>
### CERTSTATECD `property`

##### Summary

State, U.S. Territory or Canadian Province of Certifier - code

<a name='P-VRDR-IJEMortality-CERTSTNUM'></a>
### CERTSTNUM `property`

##### Summary

Certifier - Street number

<a name='P-VRDR-IJEMortality-CERTSTRDESIG'></a>
### CERTSTRDESIG `property`

##### Summary

Certifier - Street designator

<a name='P-VRDR-IJEMortality-CERTSTRNAME'></a>
### CERTSTRNAME `property`

##### Summary

Certifier - Street name

<a name='P-VRDR-IJEMortality-CERTSUFFIX'></a>
### CERTSUFFIX `property`

##### Summary

Certifier's Suffix Name

<a name='P-VRDR-IJEMortality-CERTUNITNUM'></a>
### CERTUNITNUM `property`

##### Summary

Certifier - Unit or apt number

<a name='P-VRDR-IJEMortality-CERTZIP'></a>
### CERTZIP `property`

##### Summary

Certifier - Zip

<a name='P-VRDR-IJEMortality-CITYC'></a>
### CITYC `property`

##### Summary

Decedent's Residence--City

<a name='P-VRDR-IJEMortality-CITYCODE_D'></a>
### CITYCODE_D `property`

##### Summary

Place of death. City FIPS code

<a name='P-VRDR-IJEMortality-CITYCODE_I'></a>
### CITYCODE_I `property`

##### Summary

Town/city of Injury code

<a name='P-VRDR-IJEMortality-CITYTEXT_D'></a>
### CITYTEXT_D `property`

##### Summary

Place of death. City or Town name

<a name='P-VRDR-IJEMortality-CITYTEXT_I'></a>
### CITYTEXT_I `property`

##### Summary

Town/city of Injury - literal

<a name='P-VRDR-IJEMortality-CITYTEXT_R'></a>
### CITYTEXT_R `property`

##### Summary

Decedent's Residence - City or Town name

<a name='P-VRDR-IJEMortality-COD'></a>
### COD `property`

##### Summary

County of Death Occurrence

<a name='P-VRDR-IJEMortality-COD1A'></a>
### COD1A `property`

##### Summary

Cause of Death Part I Line a

<a name='P-VRDR-IJEMortality-COD1B'></a>
### COD1B `property`

##### Summary

Cause of Death Part I Line b

<a name='P-VRDR-IJEMortality-COD1C'></a>
### COD1C `property`

##### Summary

Cause of Death Part I Line c

<a name='P-VRDR-IJEMortality-COD1D'></a>
### COD1D `property`

##### Summary

Cause of Death Part I Line d

<a name='P-VRDR-IJEMortality-COUNTRYC'></a>
### COUNTRYC `property`

##### Summary

Decedent's Residence--Country

<a name='P-VRDR-IJEMortality-COUNTRYTEXT_R'></a>
### COUNTRYTEXT_R `property`

##### Summary

Decedent's Residence - COUNTRY name

<a name='P-VRDR-IJEMortality-COUNTYC'></a>
### COUNTYC `property`

##### Summary

Decedent's Residence--County

<a name='P-VRDR-IJEMortality-COUNTYCODE_I'></a>
### COUNTYCODE_I `property`

##### Summary

County of Injury code

<a name='P-VRDR-IJEMortality-COUNTYTEXT_D'></a>
### COUNTYTEXT_D `property`

##### Summary

Place of death. County of Death

<a name='P-VRDR-IJEMortality-COUNTYTEXT_I'></a>
### COUNTYTEXT_I `property`

##### Summary

County of Injury - literal

<a name='P-VRDR-IJEMortality-COUNTYTEXT_R'></a>
### COUNTYTEXT_R `property`

##### Summary

Decedent's Residence - County

<a name='P-VRDR-IJEMortality-DBPLACECITY'></a>
### DBPLACECITY `property`

##### Summary

Decedent's Birth Place City - Literal

<a name='P-VRDR-IJEMortality-DBPLACECITYCODE'></a>
### DBPLACECITYCODE `property`

##### Summary

Decedent's Birth Place City - Code

<a name='P-VRDR-IJEMortality-DDADF'></a>
### DDADF `property`

##### Summary

Father's First Name

<a name='P-VRDR-IJEMortality-DDADMID'></a>
### DDADMID `property`

##### Summary

Father's Middle Name

<a name='P-VRDR-IJEMortality-DEDUC'></a>
### DEDUC `property`

##### Summary

Decedent's Education

<a name='P-VRDR-IJEMortality-DEDUC_BYPASS'></a>
### DEDUC_BYPASS `property`

##### Summary

Decedent's Education--Edit Flag

<a name='P-VRDR-IJEMortality-DETHNIC1'></a>
### DETHNIC1 `property`

##### Summary

Decedent of Hispanic Origin?--Mexican

<a name='P-VRDR-IJEMortality-DETHNIC2'></a>
### DETHNIC2 `property`

##### Summary

Decedent of Hispanic Origin?--Puerto Rican

<a name='P-VRDR-IJEMortality-DETHNIC3'></a>
### DETHNIC3 `property`

##### Summary

Decedent of Hispanic Origin?--Cuban

<a name='P-VRDR-IJEMortality-DETHNIC4'></a>
### DETHNIC4 `property`

##### Summary

Decedent of Hispanic Origin?--Other

<a name='P-VRDR-IJEMortality-DETHNIC5'></a>
### DETHNIC5 `property`

##### Summary

Decedent of Hispanic Origin?--Other, Literal

<a name='P-VRDR-IJEMortality-DETHNIC5C'></a>
### DETHNIC5C `property`

##### Summary

Hispanic Code for Literal

<a name='P-VRDR-IJEMortality-DETHNICE'></a>
### DETHNICE `property`

##### Summary

Hispanic

<a name='P-VRDR-IJEMortality-DINSTI'></a>
### DINSTI `property`

##### Summary

Death Institution name

<a name='P-VRDR-IJEMortality-DISP'></a>
### DISP `property`

##### Summary

Method of Disposition

<a name='P-VRDR-IJEMortality-DISPCITY'></a>
### DISPCITY `property`

##### Summary

Disposition City - Literal

<a name='P-VRDR-IJEMortality-DISPCITYCODE'></a>
### DISPCITYCODE `property`

##### Summary

Disposition City - Code

<a name='P-VRDR-IJEMortality-DISPSTATE'></a>
### DISPSTATE `property`

##### Summary

Disposition State or Territory - Literal

<a name='P-VRDR-IJEMortality-DISPSTATECD'></a>
### DISPSTATECD `property`

##### Summary

State, U.S. Territory or Canadian Province of Disposition - code

<a name='P-VRDR-IJEMortality-DMAIDEN'></a>
### DMAIDEN `property`

##### Summary

Decedent's Maiden Name

<a name='P-VRDR-IJEMortality-DMIDDLE'></a>
### DMIDDLE `property`

##### Summary

Middle Name of Decedent

<a name='P-VRDR-IJEMortality-DMOMF'></a>
### DMOMF `property`

##### Summary

Mother's First Name

<a name='P-VRDR-IJEMortality-DMOMMDN'></a>
### DMOMMDN `property`

##### Summary

Mother's Maiden Surname

<a name='P-VRDR-IJEMortality-DMOMMID'></a>
### DMOMMID `property`

##### Summary

Mother's Middle Name

<a name='P-VRDR-IJEMortality-DOB_DY'></a>
### DOB_DY `property`

##### Summary

Date of Birth--Day

<a name='P-VRDR-IJEMortality-DOB_MO'></a>
### DOB_MO `property`

##### Summary

Date of Birth--Month

<a name='P-VRDR-IJEMortality-DOB_YR'></a>
### DOB_YR `property`

##### Summary

Date of Birth--Year

<a name='P-VRDR-IJEMortality-DOD_DY'></a>
### DOD_DY `property`

##### Summary

Date of Death--Day

<a name='P-VRDR-IJEMortality-DOD_MO'></a>
### DOD_MO `property`

##### Summary

Date of Death--Month

<a name='P-VRDR-IJEMortality-DOD_YR'></a>
### DOD_YR `property`

##### Summary

Date of Death--Year

<a name='P-VRDR-IJEMortality-DOI_DY'></a>
### DOI_DY `property`

##### Summary

Date of injury--day

<a name='P-VRDR-IJEMortality-DOI_MO'></a>
### DOI_MO `property`

##### Summary

Date of injury--month

<a name='P-VRDR-IJEMortality-DOI_YR'></a>
### DOI_YR `property`

##### Summary

Date of injury--year

<a name='P-VRDR-IJEMortality-DOR_DY'></a>
### DOR_DY `property`

##### Summary

Date of Registration--Day

<a name='P-VRDR-IJEMortality-DOR_MO'></a>
### DOR_MO `property`

##### Summary

Date of Registration--Month

<a name='P-VRDR-IJEMortality-DOR_YR'></a>
### DOR_YR `property`

##### Summary

Date of Registration--Year

<a name='P-VRDR-IJEMortality-DPLACE'></a>
### DPLACE `property`

##### Summary

Place of Death

<a name='P-VRDR-IJEMortality-DSTATE'></a>
### DSTATE `property`

##### Summary

State, U.S. Territory or Canadian Province of Death - code

<a name='P-VRDR-IJEMortality-DTHCOUNTRY'></a>
### DTHCOUNTRY `property`

##### Summary

Country of Death - Literal

<a name='P-VRDR-IJEMortality-DTHCOUNTRYCD'></a>
### DTHCOUNTRYCD `property`

##### Summary

Country of Death - Code

<a name='P-VRDR-IJEMortality-EAC'></a>
### EAC `property`

##### Summary

Entity-axis codes

<a name='P-VRDR-IJEMortality-FATHERSUFFIX'></a>
### FATHERSUFFIX `property`

##### Summary

Father's Suffix

<a name='P-VRDR-IJEMortality-FILEDATE'></a>
### FILEDATE `property`

##### Summary

Date Filed

<a name='P-VRDR-IJEMortality-FILENO'></a>
### FILENO `property`

##### Summary

Certificate Number

<a name='P-VRDR-IJEMortality-FILLER2'></a>
### FILLER2 `property`

##### Summary

FILLER 2 for expansion

<a name='P-VRDR-IJEMortality-FLNAME'></a>
### FLNAME `property`

##### Summary

Father's Surname

<a name='P-VRDR-IJEMortality-FUNCITYTEXT'></a>
### FUNCITYTEXT `property`

##### Summary

Funeral Facility - City or Town name

<a name='P-VRDR-IJEMortality-FUNFACADDRESS'></a>
### FUNFACADDRESS `property`

##### Summary

Long string address for Funeral Facility same as above but allows states to choose the way they capture information.

<a name='P-VRDR-IJEMortality-FUNFACNAME'></a>
### FUNFACNAME `property`

##### Summary

Funeral Facility Name

<a name='P-VRDR-IJEMortality-FUNFACPREDIR'></a>
### FUNFACPREDIR `property`

##### Summary

Funeral Facility - Pre Directional

<a name='P-VRDR-IJEMortality-FUNFACSTNUM'></a>
### FUNFACSTNUM `property`

##### Summary

Funeral Facility - Street number

<a name='P-VRDR-IJEMortality-FUNFACSTRDESIG'></a>
### FUNFACSTRDESIG `property`

##### Summary

Funeral Facility - Street designator

<a name='P-VRDR-IJEMortality-FUNFACSTRNAME'></a>
### FUNFACSTRNAME `property`

##### Summary

Funeral Facility - Street name

<a name='P-VRDR-IJEMortality-FUNPOSTDIR'></a>
### FUNPOSTDIR `property`

##### Summary

Funeral Facility - Post Directional

<a name='P-VRDR-IJEMortality-FUNSTATE'></a>
### FUNSTATE `property`

##### Summary

State, U.S. Territory or Canadian Province of Funeral Facility - literal

<a name='P-VRDR-IJEMortality-FUNSTATECD'></a>
### FUNSTATECD `property`

##### Summary

State, U.S. Territory or Canadian Province of Funeral Facility - code

<a name='P-VRDR-IJEMortality-FUNUNITNUM'></a>
### FUNUNITNUM `property`

##### Summary

Funeral Facility - Unit or apt number

<a name='P-VRDR-IJEMortality-FUNZIP'></a>
### FUNZIP `property`

##### Summary

Funeral Facility - ZIP

<a name='P-VRDR-IJEMortality-GNAME'></a>
### GNAME `property`

##### Summary

Decedent's Legal Name--Given

<a name='P-VRDR-IJEMortality-HISPOLDC'></a>
### HISPOLDC `property`

##### Summary

Hispanic - old NCHS single ethnicity codes

<a name='P-VRDR-IJEMortality-HISPSTSP'></a>
### HISPSTSP `property`

##### Summary

Hispanic Origin - Specify

<a name='P-VRDR-IJEMortality-HOWINJ'></a>
### HOWINJ `property`

##### Summary

Describe How Injury Occurred

<a name='P-VRDR-IJEMortality-IDOB_YR'></a>
### IDOB_YR `property`

##### Summary

Infant Death/Birth Linking - year of birth

<a name='P-VRDR-IJEMortality-INACT'></a>
### INACT `property`

##### Summary

Activity at time of death (computer generated)

<a name='P-VRDR-IJEMortality-INDUST'></a>
### INDUST `property`

##### Summary

Industry -- Literal (OPTIONAL)

<a name='P-VRDR-IJEMortality-INDUSTC'></a>
### INDUSTC `property`

##### Summary

Industry -- Code

<a name='P-VRDR-IJEMortality-INDUSTC4'></a>
### INDUSTC4 `property`

##### Summary

Industry -- 4 digit Code (OPTIONAL)

<a name='P-VRDR-IJEMortality-INFORMRELATE'></a>
### INFORMRELATE `property`

##### Summary

Informant's Relationship

<a name='P-VRDR-IJEMortality-INJPL'></a>
### INJPL `property`

##### Summary

Place of Injury (computer generated)

<a name='P-VRDR-IJEMortality-INTERVAL1A'></a>
### INTERVAL1A `property`

##### Summary

Cause of Death Part I Interval, Line a

<a name='P-VRDR-IJEMortality-INTERVAL1B'></a>
### INTERVAL1B `property`

##### Summary

Cause of Death Part I Interval, Line b

<a name='P-VRDR-IJEMortality-INTERVAL1C'></a>
### INTERVAL1C `property`

##### Summary

Cause of Death Part I Interval, Line c

<a name='P-VRDR-IJEMortality-INTERVAL1D'></a>
### INTERVAL1D `property`

##### Summary

Cause of Death Part I Interval, Line d

<a name='P-VRDR-IJEMortality-INT_REJ'></a>
### INT_REJ `property`

##### Summary

Intentional Reject

<a name='P-VRDR-IJEMortality-LAT_D'></a>
### LAT_D `property`

##### Summary

Place of Death. Latitude

<a name='P-VRDR-IJEMortality-LAT_I'></a>
### LAT_I `property`

##### Summary

Place of injury. Latitude

<a name='P-VRDR-IJEMortality-LIMITS'></a>
### LIMITS `property`

##### Summary

Decedent's Residence--Inside City Limits

<a name='P-VRDR-IJEMortality-LNAME'></a>
### LNAME `property`

##### Summary

Decedent's Legal Name--Last

<a name='P-VRDR-IJEMortality-LONG_D'></a>
### LONG_D `property`

##### Summary

Place of death. Longitude

<a name='P-VRDR-IJEMortality-LONG_I'></a>
### LONG_I `property`

##### Summary

Place of injury. Longitude

<a name='P-VRDR-IJEMortality-MANNER'></a>
### MANNER `property`

##### Summary

Manner of Death

<a name='P-VRDR-IJEMortality-MAN_UC'></a>
### MAN_UC `property`

##### Summary

Manual Underlying Cause

<a name='P-VRDR-IJEMortality-MARITAL'></a>
### MARITAL `property`

##### Summary

Marital Status

<a name='P-VRDR-IJEMortality-MARITAL_BYPASS'></a>
### MARITAL_BYPASS `property`

##### Summary

Marital Status--Edit Flag

<a name='P-VRDR-IJEMortality-MARITAL_DESCRIP'></a>
### MARITAL_DESCRIP `property`

##### Summary

Marital Descriptor

<a name='P-VRDR-IJEMortality-MFILED'></a>
### MFILED `property`

##### Summary

Source flag: paper/electronic

<a name='P-VRDR-IJEMortality-MNAME'></a>
### MNAME `property`

##### Summary

Decedent's Legal Name--Middle

<a name='P-VRDR-IJEMortality-MOTHERSSUFFIX'></a>
### MOTHERSSUFFIX `property`

##### Summary

Mother's Suffix

<a name='P-VRDR-IJEMortality-NCHSBRIDGE'></a>
### NCHSBRIDGE `property`

##### Summary

Bridged Race

<a name='P-VRDR-IJEMortality-OCCUP'></a>
### OCCUP `property`

##### Summary

Occupation -- Literal (OPTIONAL)

<a name='P-VRDR-IJEMortality-OCCUPC'></a>
### OCCUPC `property`

##### Summary

Occupation -- Code

<a name='P-VRDR-IJEMortality-OCCUPC4'></a>
### OCCUPC4 `property`

##### Summary

Occupation -- 4 digit Code (OPTIONAL)

<a name='P-VRDR-IJEMortality-OLDEDUC'></a>
### OLDEDUC `property`

##### Summary

Old NCHS education code if collected - receiving state will recode as they prefer

<a name='P-VRDR-IJEMortality-OTHERCONDITION'></a>
### OTHERCONDITION `property`

##### Summary

Cause of Death Part II

<a name='P-VRDR-IJEMortality-PLACE1_1'></a>
### PLACE1_1 `property`

##### Summary

Blank for One-Byte Field 1

<a name='P-VRDR-IJEMortality-PLACE1_2'></a>
### PLACE1_2 `property`

##### Summary

Blank for One-Byte Field 2

<a name='P-VRDR-IJEMortality-PLACE1_3'></a>
### PLACE1_3 `property`

##### Summary

Blank for One-Byte Field 3

<a name='P-VRDR-IJEMortality-PLACE1_4'></a>
### PLACE1_4 `property`

##### Summary

Blank for One-Byte Field 4

<a name='P-VRDR-IJEMortality-PLACE1_5'></a>
### PLACE1_5 `property`

##### Summary

Blank for One-Byte Field 5

<a name='P-VRDR-IJEMortality-PLACE1_6'></a>
### PLACE1_6 `property`

##### Summary

Blank for One-Byte Field 6

<a name='P-VRDR-IJEMortality-PLACE20'></a>
### PLACE20 `property`

##### Summary

Blank for Twenty-Byte Field

<a name='P-VRDR-IJEMortality-PLACE8_1'></a>
### PLACE8_1 `property`

##### Summary

Blank for Eight-Byte Field 1

<a name='P-VRDR-IJEMortality-PLACE8_2'></a>
### PLACE8_2 `property`

##### Summary

Blank for Eight-Byte Field 2

<a name='P-VRDR-IJEMortality-PLACE8_3'></a>
### PLACE8_3 `property`

##### Summary

Blank for Eight-Byte Field 3

<a name='P-VRDR-IJEMortality-POILITRL'></a>
### POILITRL `property`

##### Summary

Place of Injury- literal

<a name='P-VRDR-IJEMortality-POSTDIR_D'></a>
### POSTDIR_D `property`

##### Summary

Place of death. Post Directional

<a name='P-VRDR-IJEMortality-POSTDIR_R'></a>
### POSTDIR_R `property`

##### Summary

Post Directional

<a name='P-VRDR-IJEMortality-PPDATESIGNED'></a>
### PPDATESIGNED `property`

##### Summary

Person Pronouncing Date Signed

<a name='P-VRDR-IJEMortality-PPTIME'></a>
### PPTIME `property`

##### Summary

Person Pronouncing Time Pronounced

<a name='P-VRDR-IJEMortality-PREDIR_D'></a>
### PREDIR_D `property`

##### Summary

Place of death. Pre Directional

<a name='P-VRDR-IJEMortality-PREDIR_R'></a>
### PREDIR_R `property`

##### Summary

Pre directional

<a name='P-VRDR-IJEMortality-PREG'></a>
### PREG `property`

##### Summary

Pregnancy

<a name='P-VRDR-IJEMortality-PREG_BYPASS'></a>
### PREG_BYPASS `property`

##### Summary

If Female--Edit Flag: From EDR only

<a name='P-VRDR-IJEMortality-RAC'></a>
### RAC `property`

##### Summary

Record-axis codes

<a name='P-VRDR-IJEMortality-RACE1'></a>
### RACE1 `property`

##### Summary

Decedent's Race--White

<a name='P-VRDR-IJEMortality-RACE10'></a>
### RACE10 `property`

##### Summary

Decedent's Race--Other Asian

<a name='P-VRDR-IJEMortality-RACE11'></a>
### RACE11 `property`

##### Summary

Decedent's Race--Native Hawaiian

<a name='P-VRDR-IJEMortality-RACE12'></a>
### RACE12 `property`

##### Summary

Decedent's Race--Guamanian or Chamorro

<a name='P-VRDR-IJEMortality-RACE13'></a>
### RACE13 `property`

##### Summary

Decedent's Race--Samoan

<a name='P-VRDR-IJEMortality-RACE14'></a>
### RACE14 `property`

##### Summary

Decedent's Race--Other Pacific Islander

<a name='P-VRDR-IJEMortality-RACE15'></a>
### RACE15 `property`

##### Summary

Decedent's Race--Other

<a name='P-VRDR-IJEMortality-RACE16'></a>
### RACE16 `property`

##### Summary

Decedent's Race--First American Indian or Alaska Native Literal

<a name='P-VRDR-IJEMortality-RACE16C'></a>
### RACE16C `property`

##### Summary

First American Indian Code

<a name='P-VRDR-IJEMortality-RACE17'></a>
### RACE17 `property`

##### Summary

Decedent's Race--Second American Indian or Alaska Native Literal

<a name='P-VRDR-IJEMortality-RACE17C'></a>
### RACE17C `property`

##### Summary

Second American Indian Code

<a name='P-VRDR-IJEMortality-RACE18'></a>
### RACE18 `property`

##### Summary

Decedent's Race--First Other Asian Literal

<a name='P-VRDR-IJEMortality-RACE18C'></a>
### RACE18C `property`

##### Summary

First Other Asian Code

<a name='P-VRDR-IJEMortality-RACE19'></a>
### RACE19 `property`

##### Summary

Decedent's Race--Second Other Asian Literal

<a name='P-VRDR-IJEMortality-RACE19C'></a>
### RACE19C `property`

##### Summary

Second Other Asian Code

<a name='P-VRDR-IJEMortality-RACE1E'></a>
### RACE1E `property`

##### Summary

First Edited Code

<a name='P-VRDR-IJEMortality-RACE2'></a>
### RACE2 `property`

##### Summary

Decedent's Race--Black or African American

<a name='P-VRDR-IJEMortality-RACE20'></a>
### RACE20 `property`

##### Summary

Decedent's Race--First Other Pacific Islander Literal

<a name='P-VRDR-IJEMortality-RACE20C'></a>
### RACE20C `property`

##### Summary

First Other Pacific Islander Code

<a name='P-VRDR-IJEMortality-RACE21'></a>
### RACE21 `property`

##### Summary

Decedent's Race--Second Other Pacific Islander Literalr

<a name='P-VRDR-IJEMortality-RACE21C'></a>
### RACE21C `property`

##### Summary

Second Other Pacific Islander Code

<a name='P-VRDR-IJEMortality-RACE22'></a>
### RACE22 `property`

##### Summary

Decedent's Race--First Other Literal

<a name='P-VRDR-IJEMortality-RACE22C'></a>
### RACE22C `property`

##### Summary

First Other Race Code

<a name='P-VRDR-IJEMortality-RACE23'></a>
### RACE23 `property`

##### Summary

Decedent's Race--Second Other Literal

<a name='P-VRDR-IJEMortality-RACE23C'></a>
### RACE23C `property`

##### Summary

Second Other Race Code

<a name='P-VRDR-IJEMortality-RACE2E'></a>
### RACE2E `property`

##### Summary

Second Edited Code

<a name='P-VRDR-IJEMortality-RACE3'></a>
### RACE3 `property`

##### Summary

Decedent's Race--American Indian or Alaska Native

<a name='P-VRDR-IJEMortality-RACE3E'></a>
### RACE3E `property`

##### Summary

Third Edited Code

<a name='P-VRDR-IJEMortality-RACE4'></a>
### RACE4 `property`

##### Summary

Decedent's Race--Asian Indian

<a name='P-VRDR-IJEMortality-RACE4E'></a>
### RACE4E `property`

##### Summary

Fourth Edited Code

<a name='P-VRDR-IJEMortality-RACE5'></a>
### RACE5 `property`

##### Summary

Decedent's Race--Chinese

<a name='P-VRDR-IJEMortality-RACE5E'></a>
### RACE5E `property`

##### Summary

Fifth Edited Code

<a name='P-VRDR-IJEMortality-RACE6'></a>
### RACE6 `property`

##### Summary

Decedent's Race--Filipino

<a name='P-VRDR-IJEMortality-RACE6E'></a>
### RACE6E `property`

##### Summary

Sixth Edited Code

<a name='P-VRDR-IJEMortality-RACE7'></a>
### RACE7 `property`

##### Summary

Decedent's Race--Japanese

<a name='P-VRDR-IJEMortality-RACE7E'></a>
### RACE7E `property`

##### Summary

Seventh Edited Code

<a name='P-VRDR-IJEMortality-RACE8'></a>
### RACE8 `property`

##### Summary

Decedent's Race--Korean

<a name='P-VRDR-IJEMortality-RACE8E'></a>
### RACE8E `property`

##### Summary

Eighth Edited Code

<a name='P-VRDR-IJEMortality-RACE9'></a>
### RACE9 `property`

##### Summary

Decedent's Race--Vietnamese

<a name='P-VRDR-IJEMortality-RACEOLDC'></a>
### RACEOLDC `property`

##### Summary

Race - old NCHS single race codes

<a name='P-VRDR-IJEMortality-RACESTSP'></a>
### RACESTSP `property`

##### Summary

Race - Specify

<a name='P-VRDR-IJEMortality-RACE_MVR'></a>
### RACE_MVR `property`

##### Summary

Decedent's Race--Missing

<a name='P-VRDR-IJEMortality-REFERRED'></a>
### REFERRED `property`

##### Summary

Was case Referred to Medical Examiner/Coroner?

<a name='P-VRDR-IJEMortality-REPLACE'></a>
### REPLACE `property`

##### Summary

Replacement Record -- suggested codes

<a name='P-VRDR-IJEMortality-RESCON'></a>
### RESCON `property`

##### Summary

Old NCHS residence city/county combo code

<a name='P-VRDR-IJEMortality-RESSTATE'></a>
### RESSTATE `property`

##### Summary

Old NCHS residence state code

<a name='P-VRDR-IJEMortality-R_DY'></a>
### R_DY `property`

##### Summary

Receipt date -- Day

<a name='P-VRDR-IJEMortality-R_MO'></a>
### R_MO `property`

##### Summary

Receipt date -- Month

<a name='P-VRDR-IJEMortality-R_YR'></a>
### R_YR `property`

##### Summary

Receipt date -- Year

<a name='P-VRDR-IJEMortality-SEX'></a>
### SEX `property`

##### Summary

Sex

<a name='P-VRDR-IJEMortality-SEX_BYPASS'></a>
### SEX_BYPASS `property`

##### Summary

Sex--Edit Flag

<a name='P-VRDR-IJEMortality-SPOUSEF'></a>
### SPOUSEF `property`

##### Summary

Spouse's First Name

<a name='P-VRDR-IJEMortality-SPOUSEL'></a>
### SPOUSEL `property`

##### Summary

Husband's Surname/Wife's Maiden Last Name

<a name='P-VRDR-IJEMortality-SPOUSELV'></a>
### SPOUSELV `property`

##### Summary

Decedent's spouse living at decedent's DOD?

<a name='P-VRDR-IJEMortality-SPOUSEMIDNAME'></a>
### SPOUSEMIDNAME `property`

##### Summary

Spouse's Middle Name

<a name='P-VRDR-IJEMortality-SPOUSESUFFIX'></a>
### SPOUSESUFFIX `property`

##### Summary

Spouse's Suffix

<a name='P-VRDR-IJEMortality-SSADATETRANS'></a>
### SSADATETRANS `property`

##### Summary

SSA Date of State Transmission

<a name='P-VRDR-IJEMortality-SSADATEVER'></a>
### SSADATEVER `property`

##### Summary

SSA Date of SSN Verification

<a name='P-VRDR-IJEMortality-SSADTHCODE'></a>
### SSADTHCODE `property`

##### Summary

SSA State Source of Death

<a name='P-VRDR-IJEMortality-SSAFOREIGN'></a>
### SSAFOREIGN `property`

##### Summary

SSA Foreign Country Indicator

<a name='P-VRDR-IJEMortality-SSAVERIFY'></a>
### SSAVERIFY `property`

##### Summary

SSA EDR Verify Code

<a name='P-VRDR-IJEMortality-SSN'></a>
### SSN `property`

##### Summary

Social Security Number

<a name='P-VRDR-IJEMortality-STATEBTH'></a>
### STATEBTH `property`

##### Summary

State, U.S. Territory or Canadian Province of Birth - literal

<a name='P-VRDR-IJEMortality-STATEC'></a>
### STATEC `property`

##### Summary

State, U.S. Territory or Canadian Province of Decedent's residence - code

<a name='P-VRDR-IJEMortality-STATECODE_I'></a>
### STATECODE_I `property`

##### Summary

State, U.S. Territory or Canadian Province of Injury - code

<a name='P-VRDR-IJEMortality-STATESP'></a>
### STATESP `property`

##### Summary

State Specific Data

<a name='P-VRDR-IJEMortality-STATETEXT_D'></a>
### STATETEXT_D `property`

##### Summary

Place of death. State name literal

<a name='P-VRDR-IJEMortality-STATETEXT_R'></a>
### STATETEXT_R `property`

##### Summary

Decedent's Residence - State name

<a name='P-VRDR-IJEMortality-STDESIG_D'></a>
### STDESIG_D `property`

##### Summary

Place of death. Street designator

<a name='P-VRDR-IJEMortality-STDESIG_R'></a>
### STDESIG_R `property`

##### Summary

Street designator

<a name='P-VRDR-IJEMortality-STINJURY'></a>
### STINJURY `property`

##### Summary

State, U.S. Territory or Canadian Province of Injury - literal

<a name='P-VRDR-IJEMortality-STNAME_D'></a>
### STNAME_D `property`

##### Summary

Place of death. Street name

<a name='P-VRDR-IJEMortality-STNAME_R'></a>
### STNAME_R `property`

##### Summary

Street name

<a name='P-VRDR-IJEMortality-STNUM_D'></a>
### STNUM_D `property`

##### Summary

Place of death. Street number

<a name='P-VRDR-IJEMortality-STNUM_R'></a>
### STNUM_R `property`

##### Summary

Place of death. City FIPS code

<a name='P-VRDR-IJEMortality-SUFF'></a>
### SUFF `property`

##### Summary

Decedent's Legal Name--Suffix

<a name='P-VRDR-IJEMortality-SUR_DY'></a>
### SUR_DY `property`

##### Summary

Surgery Date--day

<a name='P-VRDR-IJEMortality-SUR_MO'></a>
### SUR_MO `property`

##### Summary

Surgery Date--month

<a name='P-VRDR-IJEMortality-SUR_YR'></a>
### SUR_YR `property`

##### Summary

Surgery Date--year

<a name='P-VRDR-IJEMortality-SYS_REJ'></a>
### SYS_REJ `property`

##### Summary

Acme System Reject Codes

<a name='P-VRDR-IJEMortality-TOBAC'></a>
### TOBAC `property`

##### Summary

Did Tobacco Use Contribute to Death?

<a name='P-VRDR-IJEMortality-TOD'></a>
### TOD `property`

##### Summary

Time of Death

<a name='P-VRDR-IJEMortality-TOI_HR'></a>
### TOI_HR `property`

##### Summary

Time of injury

<a name='P-VRDR-IJEMortality-TOI_UNIT'></a>
### TOI_UNIT `property`

##### Summary

Time of Injury Unit

<a name='P-VRDR-IJEMortality-TRANSPRT'></a>
### TRANSPRT `property`

##### Summary

If Transportation Accident, Specify

<a name='P-VRDR-IJEMortality-TRX_FLG'></a>
### TRX_FLG `property`

##### Summary

Transax conversion flag: Computer Generated

<a name='P-VRDR-IJEMortality-UNITNUM_R'></a>
### UNITNUM_R `property`

##### Summary

Unit number

<a name='P-VRDR-IJEMortality-VOID'></a>
### VOID `property`

##### Summary

Void flag

<a name='P-VRDR-IJEMortality-WORKINJ'></a>
### WORKINJ `property`

##### Summary

Time of injury

<a name='P-VRDR-IJEMortality-ZIP9_D'></a>
### ZIP9_D `property`

##### Summary

Place of death. Zip code

<a name='P-VRDR-IJEMortality-ZIP9_R'></a>
### ZIP9_R `property`

##### Summary

Decedent's Residence - ZIP code

<a name='M-VRDR-IJEMortality-ActualICD10toNCHSICD10-System-String-'></a>
### ActualICD10toNCHSICD10() `method`

##### Summary

Actual ICD10 to NCHS ICD10

##### Parameters

This method has no parameters.

<a name='M-VRDR-IJEMortality-DateTimeStringHelper-VRDR-IJEField,System-String,System-String,System-DateTimeOffset,System-Boolean,System-Boolean-'></a>
### DateTimeStringHelper() `method`

##### Summary

Helps decompose a DateTime into individual parts (year, month, day, time).

##### Parameters

This method has no parameters.

<a name='M-VRDR-IJEMortality-DateTime_Get-System-String,System-String,System-String-'></a>
### DateTime_Get() `method`

##### Summary

Get a value on the DeathRecord whose type is some part of a DateTime.

##### Parameters

This method has no parameters.

<a name='M-VRDR-IJEMortality-DateTime_Set-System-String,System-String,System-String,System-String,System-Boolean,System-Boolean-'></a>
### DateTime_Set() `method`

##### Summary

Set a value on the DeathRecord whose type is some part of a DateTime.

##### Parameters

This method has no parameters.

<a name='M-VRDR-IJEMortality-Dictionary_Geo_Get-System-String,System-String,System-String,System-String,System-Boolean-'></a>
### Dictionary_Geo_Get() `method`

##### Summary

Get a value on the DeathRecord whose property is a geographic type (and is contained in a dictionary).

##### Parameters

This method has no parameters.

<a name='M-VRDR-IJEMortality-Dictionary_Geo_Set-System-String,System-String,System-String,System-String,System-Boolean,System-String-'></a>
### Dictionary_Geo_Set() `method`

##### Summary

Set a value on the DeathRecord whose property is a geographic type (and is contained in a dictionary).

##### Parameters

This method has no parameters.

<a name='M-VRDR-IJEMortality-Dictionary_Get-System-String,System-String,System-String-'></a>
### Dictionary_Get() `method`

##### Summary

Get a value on the DeathRecord whose property is a Dictionary type.

##### Parameters

This method has no parameters.

<a name='M-VRDR-IJEMortality-Dictionary_Get_Full-System-String,System-String,System-String-'></a>
### Dictionary_Get_Full() `method`

##### Summary

Get a value on the DeathRecord whose property is a Dictionary type, with NO truncating.

##### Parameters

This method has no parameters.

<a name='M-VRDR-IJEMortality-Dictionary_Set-System-String,System-String,System-String,System-String-'></a>
### Dictionary_Set() `method`

##### Summary

Set a value on the DeathRecord whose property is a Dictionary type.

##### Parameters

This method has no parameters.

<a name='M-VRDR-IJEMortality-FieldInfo-System-String-'></a>
### FieldInfo() `method`

##### Summary

Grabs the IJEInfo for a specific IJE field name.

##### Parameters

This method has no parameters.

<a name='M-VRDR-IJEMortality-Get_MappingFHIRToIJE-System-Collections-Generic-Dictionary{System-String,System-String},System-String,System-String-'></a>
### Get_MappingFHIRToIJE(mapping,fhirField,ijeField) `method`

##### Summary

Given a Dictionary mapping FHIR codes to IJE strings and the relevant FHIR and IJE fields pull the value
from the FHIR record object and provide the appropriate IJE string

##### Returns

The IJE value of the field translated from the FHIR value on the record

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| mapping | [System.Collections.Generic.Dictionary{System.String,System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.Dictionary 'System.Collections.Generic.Dictionary{System.String,System.String}') | Dictionary for mapping the desired concept from FHIR to IJE; these dictionaries are defined in Mappings.cs |
| fhirField | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Name of the FHIR field to get from the record; must have a related Helper property, e.g., EducationLevel must have EducationLevelHelper |
| ijeField | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Name of the IJE field that the FHIR field content is being placed into |

<a name='M-VRDR-IJEMortality-Get_Race-System-String-'></a>
### Get_Race() `method`

##### Summary

Checks if the given race exists in the record.

##### Parameters

This method has no parameters.

<a name='M-VRDR-IJEMortality-LeftJustified_Get-System-String,System-String-'></a>
### LeftJustified_Get() `method`

##### Summary

Get a value on the DeathRecord whose IJE type is a left justified string.

##### Parameters

This method has no parameters.

<a name='M-VRDR-IJEMortality-LeftJustified_Set-System-String,System-String,System-String-'></a>
### LeftJustified_Set() `method`

##### Summary

Set a value on the DeathRecord whose IJE type is a left justified string.

##### Parameters

This method has no parameters.

<a name='M-VRDR-IJEMortality-NCHSICD10toActualICD10-System-String-'></a>
### NCHSICD10toActualICD10() `method`

##### Summary

NCHS ICD10 to actual ICD10

##### Parameters

This method has no parameters.

<a name='M-VRDR-IJEMortality-NumericAllowingUnknown_Get-System-String,System-String-'></a>
### NumericAllowingUnknown_Get() `method`

##### Summary

Get a value on the DeathRecord that is a numeric string with the option of being set to all 9s on the IJE side and null on the FHIR side to represent null

##### Parameters

This method has no parameters.

<a name='M-VRDR-IJEMortality-NumericAllowingUnknown_Set-System-String,System-String,System-String-'></a>
### NumericAllowingUnknown_Set() `method`

##### Summary

Set a value on the DeathRecord that is a numeric string with the option of being set to all 9s on the IJE side and null on the FHIR side to represent null

##### Parameters

This method has no parameters.

<a name='M-VRDR-IJEMortality-RightJustifiedZeroed_Get-System-String,System-String-'></a>
### RightJustifiedZeroed_Get() `method`

##### Summary

Get a value on the DeathRecord whose IJE type is a right justified, zero filled string.

##### Parameters

This method has no parameters.

<a name='M-VRDR-IJEMortality-RightJustifiedZeroed_Set-System-String,System-String,System-String-'></a>
### RightJustifiedZeroed_Set() `method`

##### Summary

Set a value on the DeathRecord whose IJE type is a right justified, zero filled string.

##### Parameters

This method has no parameters.

<a name='M-VRDR-IJEMortality-Set_MappingIJEToFHIR-System-Collections-Generic-Dictionary{System-String,System-String},System-String,System-String,System-String-'></a>
### Set_MappingIJEToFHIR(mapping,ijeField,fhirField,value) `method`

##### Summary

Given a Dictionary mapping IJE codes to FHIR strings and the relevant IJE and FHIR fields translate the IJE
string to the appropriate FHIR code and set the value on the FHIR record object

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| mapping | [System.Collections.Generic.Dictionary{System.String,System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.Dictionary 'System.Collections.Generic.Dictionary{System.String,System.String}') | Dictionary for mapping the desired concept from IJE to FHIR; these dictionaries are defined in Mappings.cs |
| ijeField | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Name of the IJE field that the FHIR field content is being set from |
| fhirField | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Name of the FHIR field to set on the record; must have a related Helper property, e.g., EducationLevel must have EducationLevelHelper |
| value | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The value to translate from IJE to FHIR and set on the record |

<a name='M-VRDR-IJEMortality-Set_Race-System-String,System-String-'></a>
### Set_Race() `method`

##### Summary

Adds the given race to the record.

##### Parameters

This method has no parameters.

<a name='M-VRDR-IJEMortality-TimeAllowingUnknown_Get-System-String,System-String-'></a>
### TimeAllowingUnknown_Get() `method`

##### Summary

Get a value on the DeathRecord that is a time with the option of being set to all 9s on the IJE side and null on the FHIR side to represent null

##### Parameters

This method has no parameters.

<a name='M-VRDR-IJEMortality-TimeAllowingUnknown_Set-System-String,System-String,System-String-'></a>
### TimeAllowingUnknown_Set() `method`

##### Summary

Set a value on the DeathRecord that is a time with the option of being set to all 9s on the IJE side and null on the FHIR side to represent null

##### Parameters

This method has no parameters.

<a name='M-VRDR-IJEMortality-ToDeathRecord'></a>
### ToDeathRecord() `method`

##### Summary

Returns the corresponding `DeathRecord` for this IJE string.

##### Parameters

This method has no parameters.

<a name='M-VRDR-IJEMortality-ToString'></a>
### ToString() `method`

##### Summary

Converts the internal `DeathRecord` into an IJE string.

##### Parameters

This method has no parameters.

<a name='M-VRDR-IJEMortality-Truncate-System-String,System-Int32-'></a>
### Truncate() `method`

##### Summary

Truncates the given string to the given length.

##### Parameters

This method has no parameters.

<a name='T-VRDR-Mappings-IntentionalReject'></a>
## IntentionalReject `type`

##### Namespace

VRDR.Mappings

##### Summary

Mappings for IntentionalReject

<a name='T-VRDR-ValueSets-IntentionalReject'></a>
## IntentionalReject `type`

##### Namespace

VRDR.ValueSets

##### Summary

IntentionalReject

<a name='F-VRDR-Mappings-IntentionalReject-FHIRToIJE'></a>
### FHIRToIJE `constants`

##### Summary

FHIR -> IJE Mapping for IntentionalReject

<a name='F-VRDR-Mappings-IntentionalReject-IJEToFHIR'></a>
### IJEToFHIR `constants`

##### Summary

IJE -> FHIR Mapping for IntentionalReject

<a name='F-VRDR-ValueSets-IntentionalReject-Codes'></a>
### Codes `constants`

##### Summary

Codes

<a name='F-VRDR-ValueSets-IntentionalReject-Reject1'></a>
### Reject1 `constants`

##### Summary

Reject1

<a name='F-VRDR-ValueSets-IntentionalReject-Reject2'></a>
### Reject2 `constants`

##### Summary

Reject2

<a name='F-VRDR-ValueSets-IntentionalReject-Reject3'></a>
### Reject3 `constants`

##### Summary

Reject3

<a name='F-VRDR-ValueSets-IntentionalReject-Reject4'></a>
### Reject4 `constants`

##### Summary

Reject4

<a name='F-VRDR-ValueSets-IntentionalReject-Reject5'></a>
### Reject5 `constants`

##### Summary

Reject5

<a name='F-VRDR-ValueSets-IntentionalReject-Reject9'></a>
### Reject9 `constants`

##### Summary

Reject9

<a name='T-VRDR-LinqHelper'></a>
## LinqHelper `type`

##### Namespace

VRDR

##### Summary

Internal Helper class which provides Trimming and Case-Insensitive comparison of LINQ Queries.

<a name='M-VRDR-LinqHelper-EqualsInsensitive-System-String,System-String-'></a>
### EqualsInsensitive() `method`

##### Summary

Adds a extension to handle case insensitive comparisons, always Trims second parameter.

##### Parameters

This method has no parameters.

<a name='T-VRDR-IJEMortality-MREHelper'></a>
## MREHelper `type`

##### Namespace

VRDR.IJEMortality

##### Summary

Helper class to contain properties for setting MRE-only fields that have no mapping in IJE when creating coding response records

<a name='M-VRDR-IJEMortality-MREHelper-#ctor-VRDR-DeathRecord-'></a>
### #ctor() `constructor`

##### Summary

Constructor for class to contain properties for setting MRE-only fields that have no mapping in IJE when creating coding response records

##### Parameters

This constructor has no parameters.

<a name='P-VRDR-IJEMortality-MREHelper-RECODE40'></a>
### RECODE40 `property`

##### Summary

Property for setting the Race Recode 40 of a Demographic Coding Submission

<a name='T-VRDR-Mappings-MannerOfDeath'></a>
## MannerOfDeath `type`

##### Namespace

VRDR.Mappings

##### Summary

Mappings for MannerOfDeath

<a name='T-VRDR-ValueSets-MannerOfDeath'></a>
## MannerOfDeath `type`

##### Namespace

VRDR.ValueSets

##### Summary

MannerOfDeath

<a name='F-VRDR-Mappings-MannerOfDeath-FHIRToIJE'></a>
### FHIRToIJE `constants`

##### Summary

FHIR -> IJE Mapping for MannerOfDeath

<a name='F-VRDR-Mappings-MannerOfDeath-IJEToFHIR'></a>
### IJEToFHIR `constants`

##### Summary

IJE -> FHIR Mapping for MannerOfDeath

<a name='F-VRDR-ValueSets-MannerOfDeath-Accidental_Death'></a>
### Accidental_Death `constants`

##### Summary

Accidental_Death

<a name='F-VRDR-ValueSets-MannerOfDeath-Codes'></a>
### Codes `constants`

##### Summary

Codes

<a name='F-VRDR-ValueSets-MannerOfDeath-Death_Manner_Undetermined'></a>
### Death_Manner_Undetermined `constants`

##### Summary

Death_Manner_Undetermined

<a name='F-VRDR-ValueSets-MannerOfDeath-Homicide'></a>
### Homicide `constants`

##### Summary

Homicide

<a name='F-VRDR-ValueSets-MannerOfDeath-Natural_Death'></a>
### Natural_Death `constants`

##### Summary

Natural_Death

<a name='F-VRDR-ValueSets-MannerOfDeath-Patient_Awaiting_Investigation'></a>
### Patient_Awaiting_Investigation `constants`

##### Summary

Patient_Awaiting_Investigation

<a name='F-VRDR-ValueSets-MannerOfDeath-Suicide'></a>
### Suicide `constants`

##### Summary

Suicide

<a name='T-VRDR-Mappings'></a>
## Mappings `type`

##### Namespace

VRDR

##### Summary

Mappings between IJE and FHIR value sets

<a name='T-VRDR-Mappings-MaritalStatus'></a>
## MaritalStatus `type`

##### Namespace

VRDR.Mappings

##### Summary

Mappings for MaritalStatus

<a name='T-VRDR-ValueSets-MaritalStatus'></a>
## MaritalStatus `type`

##### Namespace

VRDR.ValueSets

##### Summary

MaritalStatus

<a name='F-VRDR-Mappings-MaritalStatus-FHIRToIJE'></a>
### FHIRToIJE `constants`

##### Summary

FHIR -> IJE Mapping for MaritalStatus

<a name='F-VRDR-Mappings-MaritalStatus-IJEToFHIR'></a>
### IJEToFHIR `constants`

##### Summary

IJE -> FHIR Mapping for MaritalStatus

<a name='F-VRDR-ValueSets-MaritalStatus-Codes'></a>
### Codes `constants`

##### Summary

Codes

<a name='F-VRDR-ValueSets-MaritalStatus-Divorced'></a>
### Divorced `constants`

##### Summary

Divorced

<a name='F-VRDR-ValueSets-MaritalStatus-Legally_Separated'></a>
### Legally_Separated `constants`

##### Summary

Legally_Separated

<a name='F-VRDR-ValueSets-MaritalStatus-Married'></a>
### Married `constants`

##### Summary

Married

<a name='F-VRDR-ValueSets-MaritalStatus-Never_Married'></a>
### Never_Married `constants`

##### Summary

Never_Married

<a name='F-VRDR-ValueSets-MaritalStatus-Unknown'></a>
### Unknown `constants`

##### Summary

Unknown

<a name='F-VRDR-ValueSets-MaritalStatus-Widowed'></a>
### Widowed `constants`

##### Summary

Widowed

<a name='T-VRDR-Mappings-MethodOfDisposition'></a>
## MethodOfDisposition `type`

##### Namespace

VRDR.Mappings

##### Summary

Mappings for MethodOfDisposition

<a name='T-VRDR-ValueSets-MethodOfDisposition'></a>
## MethodOfDisposition `type`

##### Namespace

VRDR.ValueSets

##### Summary

MethodOfDisposition

<a name='F-VRDR-Mappings-MethodOfDisposition-FHIRToIJE'></a>
### FHIRToIJE `constants`

##### Summary

FHIR -> IJE Mapping for MethodOfDisposition

<a name='F-VRDR-Mappings-MethodOfDisposition-IJEToFHIR'></a>
### IJEToFHIR `constants`

##### Summary

IJE -> FHIR Mapping for MethodOfDisposition

<a name='F-VRDR-ValueSets-MethodOfDisposition-Burial'></a>
### Burial `constants`

##### Summary

Burial

<a name='F-VRDR-ValueSets-MethodOfDisposition-Codes'></a>
### Codes `constants`

##### Summary

Codes

<a name='F-VRDR-ValueSets-MethodOfDisposition-Cremation'></a>
### Cremation `constants`

##### Summary

Cremation

<a name='F-VRDR-ValueSets-MethodOfDisposition-Donation'></a>
### Donation `constants`

##### Summary

Donation

<a name='F-VRDR-ValueSets-MethodOfDisposition-Entombment'></a>
### Entombment `constants`

##### Summary

Entombment

<a name='F-VRDR-ValueSets-MethodOfDisposition-Other'></a>
### Other `constants`

##### Summary

Other

<a name='F-VRDR-ValueSets-MethodOfDisposition-Removal_From_State'></a>
### Removal_From_State `constants`

##### Summary

Removal_From_State

<a name='F-VRDR-ValueSets-MethodOfDisposition-Unknown'></a>
### Unknown `constants`

##### Summary

Unknown

<a name='T-VRDR-MortalityData'></a>
## MortalityData `type`

##### Namespace

VRDR

##### Summary

Data helper class for dealing with IJE mortality data. Follows Singleton-esque pattern!

<a name='F-VRDR-MortalityData-CDCEthnicityCodes'></a>
### CDCEthnicityCodes `constants`

##### Summary

CDC Ethnicity Codes

<a name='F-VRDR-MortalityData-CDCRaceACodes'></a>
### CDCRaceACodes `constants`

##### Summary

CDC Race Asian Codes

<a name='F-VRDR-MortalityData-CDCRaceAIANCodes'></a>
### CDCRaceAIANCodes `constants`

##### Summary

CDC Race American Indian or Alaska Native Codes

<a name='F-VRDR-MortalityData-CDCRaceBAACodes'></a>
### CDCRaceBAACodes `constants`

##### Summary

CDC Race Black or African American Codes

<a name='F-VRDR-MortalityData-CDCRaceNHOPICodes'></a>
### CDCRaceNHOPICodes `constants`

##### Summary

CDC Race Native Hawaiian or Other Pacific Islander Codes

<a name='F-VRDR-MortalityData-CDCRaceWCodes'></a>
### CDCRaceWCodes `constants`

##### Summary

CDC Race White Codes

<a name='F-VRDR-MortalityData-CountryCodes'></a>
### CountryCodes `constants`

##### Summary

Country Codes based on PH_Country_GEC = 2.16.840.1.113883.13.250

<a name='F-VRDR-MortalityData-JurisdictionCodes'></a>
### JurisdictionCodes `constants`

##### Summary

Jurisdiction Codes

<a name='F-VRDR-MortalityData-PlaceCodes'></a>
### PlaceCodes `constants`

##### Summary

Place Codes

<a name='F-VRDR-MortalityData-StateTerritoryProvinceCodes'></a>
### StateTerritoryProvinceCodes `constants`

##### Summary

State and Territory Province Codes

<a name='P-VRDR-MortalityData-Instance'></a>
### Instance `property`

##### Summary

Instance get method for singleton.

<a name='M-VRDR-MortalityData-AIANRaceCodeToRaceName-System-String-'></a>
### AIANRaceCodeToRaceName() `method`

##### Summary

Given an American Indian or Alaska Native Race code - return the representative Race name.

##### Parameters

This method has no parameters.

<a name='M-VRDR-MortalityData-AIANRaceNameToRaceCode-System-String-'></a>
### AIANRaceNameToRaceCode() `method`

##### Summary

Given an American Indian or Alaska Native Race name - return the representative Race code.

##### Parameters

This method has no parameters.

<a name='M-VRDR-MortalityData-ARaceCodeToRaceName-System-String-'></a>
### ARaceCodeToRaceName() `method`

##### Summary

Given an Asian Race code - return the representative Race name.

##### Parameters

This method has no parameters.

<a name='M-VRDR-MortalityData-ARaceNameToRaceCode-System-String-'></a>
### ARaceNameToRaceCode() `method`

##### Summary

Given an Asian Race name - return the representative Race code.

##### Parameters

This method has no parameters.

<a name='M-VRDR-MortalityData-BAARaceCodeToRaceName-System-String-'></a>
### BAARaceCodeToRaceName() `method`

##### Summary

Given a Black or African American Race code - return the representative Race name.

##### Parameters

This method has no parameters.

<a name='M-VRDR-MortalityData-BAARaceNameToRaceCode-System-String-'></a>
### BAARaceNameToRaceCode() `method`

##### Summary

Given a Black or African American Race name - return the representative Race code.

##### Parameters

This method has no parameters.

<a name='M-VRDR-MortalityData-CountryCodeToCountryName-System-String-'></a>
### CountryCodeToCountryName() `method`

##### Summary

Given a Country code - return the representative Country name.

##### Parameters

This method has no parameters.

<a name='M-VRDR-MortalityData-CountryNameToCountryCode-System-String-'></a>
### CountryNameToCountryCode() `method`

##### Summary

Given a Country name - return the representative Country code.

##### Parameters

This method has no parameters.

<a name='M-VRDR-MortalityData-DictKeyFinderHelper``1-``0,System-String-'></a>
### DictKeyFinderHelper\`\`1() `method`

##### Summary

Given a value in a <string, string> object, return the first matching key.

##### Parameters

This method has no parameters.

<a name='M-VRDR-MortalityData-DictValueFinderHelper``1-``0,System-String-'></a>
### DictValueFinderHelper\`\`1() `method`

##### Summary

Given a key in a (string, string) object, return the first matching value.

##### Parameters

This method has no parameters.

<a name='M-VRDR-MortalityData-EthnicityCodeToEthnicityName-System-String-'></a>
### EthnicityCodeToEthnicityName() `method`

##### Summary

Given an Ethnicity code - return the representative Ethnicity name.

##### Parameters

This method has no parameters.

<a name='M-VRDR-MortalityData-EthnicityNameToEthnicityCode-System-String-'></a>
### EthnicityNameToEthnicityCode() `method`

##### Summary

Given an Ethnicity name - return the representative Ethnicity code.

##### Parameters

This method has no parameters.

<a name='M-VRDR-MortalityData-JurisdictionCodeToJurisdictionName-System-String-'></a>
### JurisdictionCodeToJurisdictionName() `method`

##### Summary

Given a Jurisdiction name - return the representative Jurisdiction code.

##### Parameters

This method has no parameters.

<a name='M-VRDR-MortalityData-JurisdictionNameToJurisdictionCode-System-String-'></a>
### JurisdictionNameToJurisdictionCode() `method`

##### Summary

Given a Jurisdiction code - return the Jurisdiction name.

##### Parameters

This method has no parameters.

<a name='M-VRDR-MortalityData-NHOPIRaceCodeToRaceName-System-String-'></a>
### NHOPIRaceCodeToRaceName() `method`

##### Summary

Given a Native Hawaiian or Other Pacific Islander Race code - return the representative Race name.

##### Parameters

This method has no parameters.

<a name='M-VRDR-MortalityData-NHOPIRaceNameToRaceCode-System-String-'></a>
### NHOPIRaceNameToRaceCode() `method`

##### Summary

Given a Native Hawaiian or Other Pacific Islander Race name - return the representative Race code.

##### Parameters

This method has no parameters.

<a name='M-VRDR-MortalityData-RaceCodeToRaceName-System-String-'></a>
### RaceCodeToRaceName() `method`

##### Summary

Given a Race code - return the representative Race name.

##### Parameters

This method has no parameters.

<a name='M-VRDR-MortalityData-RaceNameToRaceCode-System-String-'></a>
### RaceNameToRaceCode() `method`

##### Summary

Given a Race name - return the representative Race code.

##### Parameters

This method has no parameters.

<a name='M-VRDR-MortalityData-StateCodeAndCityNameToCountyName-System-String,System-String-'></a>
### StateCodeAndCityNameToCountyName() `method`

##### Summary

Given a State and County name, and a Place code - return the representative Place name.

##### Parameters

This method has no parameters.

<a name='M-VRDR-MortalityData-StateCodeToRandomPlace-System-String-'></a>
### StateCodeToRandomPlace() `method`

##### Summary

Given a State code, return a random PlaceCode.

##### Parameters

This method has no parameters.

<a name='M-VRDR-MortalityData-StateCodeToStateName-System-String-'></a>
### StateCodeToStateName() `method`

##### Summary

Given a State, Territory, or Province code - return the representative State, Territory, or Province name.

##### Parameters

This method has no parameters.

<a name='M-VRDR-MortalityData-StateNameAndCountyCodeToCountyName-System-String,System-String-'></a>
### StateNameAndCountyCodeToCountyName() `method`

##### Summary

Given a County code and a State name - return the representative County name.

##### Parameters

This method has no parameters.

<a name='M-VRDR-MortalityData-StateNameAndCountyNameAndPlaceCodeToPlaceName-System-String,System-String,System-String-'></a>
### StateNameAndCountyNameAndPlaceCodeToPlaceName() `method`

##### Summary

Given a State and County name, and a Place code - return the representative Place name.

##### Parameters

This method has no parameters.

<a name='M-VRDR-MortalityData-StateNameAndCountyNameAndPlaceNameToPlaceCode-System-String,System-String,System-String-'></a>
### StateNameAndCountyNameAndPlaceNameToPlaceCode() `method`

##### Summary

Given a State, County, and Place name - return the representative Place code.

##### Parameters

This method has no parameters.

<a name='M-VRDR-MortalityData-StateNameAndCountyNameToCountyCode-System-String,System-String-'></a>
### StateNameAndCountyNameToCountyCode() `method`

##### Summary

Given a State and County name - return the representative County code.

##### Parameters

This method has no parameters.

<a name='M-VRDR-MortalityData-StateNameToStateCode-System-String-'></a>
### StateNameToStateCode() `method`

##### Summary

Given a State, Territory, or Province name - return the representative State code.

##### Parameters

This method has no parameters.

<a name='M-VRDR-MortalityData-WRaceCodeToRaceName-System-String-'></a>
### WRaceCodeToRaceName() `method`

##### Summary

Given a White Race code - return the representative Race name.

##### Parameters

This method has no parameters.

<a name='M-VRDR-MortalityData-WRaceNameToRaceCode-System-String-'></a>
### WRaceNameToRaceCode() `method`

##### Summary

Given a White Race name - return the representative Race code.

##### Parameters

This method has no parameters.

<a name='T-VRDR-Mappings-NotApplicable'></a>
## NotApplicable `type`

##### Namespace

VRDR.Mappings

##### Summary

Mappings for NotApplicable

<a name='F-VRDR-Mappings-NotApplicable-FHIRToIJE'></a>
### FHIRToIJE `constants`

##### Summary

FHIR -> IJE Mapping for NotApplicable

<a name='F-VRDR-Mappings-NotApplicable-IJEToFHIR'></a>
### IJEToFHIR `constants`

##### Summary

IJE -> FHIR Mapping for NotApplicable

<a name='T-VRDR-NvssEthnicity'></a>
## NvssEthnicity `type`

##### Namespace

VRDR

##### Summary

String representations of IJE Ethnicity fields

<a name='F-VRDR-NvssEthnicity-Cuban'></a>
### Cuban `constants`

##### Summary

Cuban

<a name='F-VRDR-NvssEthnicity-Literal'></a>
### Literal `constants`

##### Summary

Literal

<a name='F-VRDR-NvssEthnicity-Mexican'></a>
### Mexican `constants`

##### Summary

Mexican

<a name='F-VRDR-NvssEthnicity-Other'></a>
### Other `constants`

##### Summary

Other

<a name='F-VRDR-NvssEthnicity-PuertoRican'></a>
### PuertoRican `constants`

##### Summary

PuertoRican

<a name='T-VRDR-NvssRace'></a>
## NvssRace `type`

##### Namespace

VRDR

##### Summary

String representations of IJE Race fields

<a name='F-VRDR-NvssRace-AmericanIndianOrAlaskaNative'></a>
### AmericanIndianOrAlaskaNative `constants`

##### Summary

AmericanIndianOrAlaskaNative

<a name='F-VRDR-NvssRace-AmericanIndianOrAlaskanNativeLiteral1'></a>
### AmericanIndianOrAlaskanNativeLiteral1 `constants`

##### Summary

AmericanIndianOrAlaskanNativeLiteralFirst

<a name='F-VRDR-NvssRace-AmericanIndianOrAlaskanNativeLiteral2'></a>
### AmericanIndianOrAlaskanNativeLiteral2 `constants`

##### Summary

AmericanIndianOrAlaskanNativeLiteralSecond

<a name='F-VRDR-NvssRace-AsianIndian'></a>
### AsianIndian `constants`

##### Summary

AsianIndian

<a name='F-VRDR-NvssRace-BlackOrAfricanAmerican'></a>
### BlackOrAfricanAmerican `constants`

##### Summary

BlackOrAfricanAmerican

<a name='F-VRDR-NvssRace-Chinese'></a>
### Chinese `constants`

##### Summary

Chinese

<a name='F-VRDR-NvssRace-Filipino'></a>
### Filipino `constants`

##### Summary

Filipino

<a name='F-VRDR-NvssRace-GuamanianOrChamorro'></a>
### GuamanianOrChamorro `constants`

##### Summary

GuamanianOrChamorro

<a name='F-VRDR-NvssRace-Japanese'></a>
### Japanese `constants`

##### Summary

Japanese

<a name='F-VRDR-NvssRace-Korean'></a>
### Korean `constants`

##### Summary

Korean

<a name='F-VRDR-NvssRace-MissingValueReason'></a>
### MissingValueReason `constants`

##### Summary

MissingValueReason

<a name='F-VRDR-NvssRace-NativeHawaiian'></a>
### NativeHawaiian `constants`

##### Summary

NativeHawaiian

<a name='F-VRDR-NvssRace-OtherAsian'></a>
### OtherAsian `constants`

##### Summary

OtherAsian

<a name='F-VRDR-NvssRace-OtherAsianLiteral1'></a>
### OtherAsianLiteral1 `constants`

##### Summary

OtherAsianLiteralFirst

<a name='F-VRDR-NvssRace-OtherAsianLiteral2'></a>
### OtherAsianLiteral2 `constants`

##### Summary

OtherAsianLiteralFirstSecond

<a name='F-VRDR-NvssRace-OtherPacificIslandLiteral1'></a>
### OtherPacificIslandLiteral1 `constants`

##### Summary

OtherPacificIslandLiteralFirst

<a name='F-VRDR-NvssRace-OtherPacificIslandLiteral2'></a>
### OtherPacificIslandLiteral2 `constants`

##### Summary

OtherPacificIslandLiteralSecond

<a name='F-VRDR-NvssRace-OtherPacificIslander'></a>
### OtherPacificIslander `constants`

##### Summary

OtherPacificIslander

<a name='F-VRDR-NvssRace-OtherRace'></a>
### OtherRace `constants`

##### Summary

OtherRace

<a name='F-VRDR-NvssRace-OtherRaceLiteral1'></a>
### OtherRaceLiteral1 `constants`

##### Summary

OtherRaceLiteralFirst

<a name='F-VRDR-NvssRace-OtherRaceLiteral2'></a>
### OtherRaceLiteral2 `constants`

##### Summary

OtherRaceLiteral2

<a name='F-VRDR-NvssRace-Samoan'></a>
### Samoan `constants`

##### Summary

Samoan

<a name='F-VRDR-NvssRace-Vietnamese'></a>
### Vietnamese `constants`

##### Summary

Vietnamese

<a name='F-VRDR-NvssRace-White'></a>
### White `constants`

##### Summary

White

<a name='M-VRDR-NvssRace-GetBooleanRaceCodes'></a>
### GetBooleanRaceCodes() `method`

##### Summary

GetBooleanRaceCodes Returns a list of the Boolean Race Codes, Y or N values

##### Parameters

This method has no parameters.

<a name='M-VRDR-NvssRace-GetLiteralRaceCodes'></a>
### GetLiteralRaceCodes() `method`

##### Summary

GetLiteralRaceCodes Returns a list of the literal Race Codes

##### Parameters

This method has no parameters.

<a name='T-VRDR-OtherExtensionURL'></a>
## OtherExtensionURL `type`

##### Namespace

VRDR

##### Summary

Extension URLs for non-VRDR Profiles

<a name='F-VRDR-OtherExtensionURL-BirthSex'></a>
### BirthSex `constants`

##### Summary

URL for US Core Birthsex

<a name='F-VRDR-OtherExtensionURL-DataAbsentReason'></a>
### DataAbsentReason `constants`

##### Summary

URL for DataAbsentReason

<a name='F-VRDR-OtherExtensionURL-PatientBirthPlace'></a>
### PatientBirthPlace `constants`

##### Summary

URL for PatientBirthPlace

<a name='T-VRDR-OtherIGURL'></a>
## OtherIGURL `type`

##### Namespace

VRDR

##### Summary

IG URLs for non-VRDR Profiles

<a name='F-VRDR-OtherIGURL-USCorePractitioner'></a>
### USCorePractitioner `constants`

##### Summary

URL for USCorePractitioner

<a name='T-VRDR-OtherProfileURL'></a>
## OtherProfileURL `type`

##### Namespace

VRDR

##### Summary

Profile URLs for non-VRDR Profiles

<a name='F-VRDR-OtherProfileURL-USCorePractitioner'></a>
### USCorePractitioner `constants`

##### Summary

URL for USCorePractitioner

<a name='T-VRDR-PlaceCode'></a>
## PlaceCode `type`

##### Namespace

VRDR

##### Summary

Helper class providing more descriptive definitions for each PlaceCode field

<a name='M-VRDR-PlaceCode-#ctor'></a>
### #ctor() `constructor`

##### Summary

The empty constructor, used by the Default case when performing LINQ queries and there is no match

##### Parameters

This constructor has no parameters.

<a name='M-VRDR-PlaceCode-#ctor-System-String,System-String,System-String,System-String,System-String,System-String-'></a>
### #ctor() `constructor`

##### Summary

The complete constructor, normally used when declaring a PlaceCode

##### Parameters

This constructor has no parameters.

<a name='P-VRDR-PlaceCode-City'></a>
### City `property`

##### Summary

Unabbreviated city name

<a name='P-VRDR-PlaceCode-Code'></a>
### Code `property`

##### Summary

The representative PlaceCode corresponding to all other fields

<a name='P-VRDR-PlaceCode-County'></a>
### County `property`

##### Summary

Unabbreviated county name

<a name='P-VRDR-PlaceCode-CountyCode'></a>
### CountyCode `property`

##### Summary

Three digit county code

<a name='P-VRDR-PlaceCode-Description'></a>
### Description `property`

##### Summary

Description, normally either blank or "City of", "Township of", etc

<a name='P-VRDR-PlaceCode-State'></a>
### State `property`

##### Summary

Two letter state abbreviation

<a name='T-VRDR-Mappings-PlaceOfDeath'></a>
## PlaceOfDeath `type`

##### Namespace

VRDR.Mappings

##### Summary

Mappings for PlaceOfDeath

<a name='T-VRDR-ValueSets-PlaceOfDeath'></a>
## PlaceOfDeath `type`

##### Namespace

VRDR.ValueSets

##### Summary

PlaceOfDeath

<a name='F-VRDR-Mappings-PlaceOfDeath-FHIRToIJE'></a>
### FHIRToIJE `constants`

##### Summary

FHIR -> IJE Mapping for PlaceOfDeath

<a name='F-VRDR-Mappings-PlaceOfDeath-IJEToFHIR'></a>
### IJEToFHIR `constants`

##### Summary

IJE -> FHIR Mapping for PlaceOfDeath

<a name='F-VRDR-ValueSets-PlaceOfDeath-Codes'></a>
### Codes `constants`

##### Summary

Codes

<a name='F-VRDR-ValueSets-PlaceOfDeath-Dead_On_Arrival_At_Hospital'></a>
### Dead_On_Arrival_At_Hospital `constants`

##### Summary

Dead_On_Arrival_At_Hospital

<a name='F-VRDR-ValueSets-PlaceOfDeath-Death_In_Home'></a>
### Death_In_Home `constants`

##### Summary

Death_In_Home

<a name='F-VRDR-ValueSets-PlaceOfDeath-Death_In_Hospice'></a>
### Death_In_Hospice `constants`

##### Summary

Death_In_Hospice

<a name='F-VRDR-ValueSets-PlaceOfDeath-Death_In_Hospital'></a>
### Death_In_Hospital `constants`

##### Summary

Death_In_Hospital

<a name='F-VRDR-ValueSets-PlaceOfDeath-Death_In_Hospital_Based_Emergency_Department_Or_Outpatient_Department'></a>
### Death_In_Hospital_Based_Emergency_Department_Or_Outpatient_Department `constants`

##### Summary

Death_In_Hospital_Based_Emergency_Department_Or_Outpatient_Department

<a name='F-VRDR-ValueSets-PlaceOfDeath-Death_In_Nursing_Home_Or_Long_Term_Care_Facility'></a>
### Death_In_Nursing_Home_Or_Long_Term_Care_Facility `constants`

##### Summary

Death_In_Nursing_Home_Or_Long_Term_Care_Facility

<a name='F-VRDR-ValueSets-PlaceOfDeath-Other'></a>
### Other `constants`

##### Summary

Other

<a name='F-VRDR-ValueSets-PlaceOfDeath-Unk'></a>
### Unk `constants`

##### Summary

Unk

<a name='T-VRDR-Mappings-PlaceOfInjury'></a>
## PlaceOfInjury `type`

##### Namespace

VRDR.Mappings

##### Summary

Mappings for PlaceOfInjury

<a name='T-VRDR-ValueSets-PlaceOfInjury'></a>
## PlaceOfInjury `type`

##### Namespace

VRDR.ValueSets

##### Summary

PlaceOfInjury

<a name='F-VRDR-Mappings-PlaceOfInjury-FHIRToIJE'></a>
### FHIRToIJE `constants`

##### Summary

FHIR -> IJE Mapping for PlaceOfInjury

<a name='F-VRDR-Mappings-PlaceOfInjury-IJEToFHIR'></a>
### IJEToFHIR `constants`

##### Summary

IJE -> FHIR Mapping for PlaceOfInjury

<a name='F-VRDR-ValueSets-PlaceOfInjury-Codes'></a>
### Codes `constants`

##### Summary

Codes

<a name='F-VRDR-ValueSets-PlaceOfInjury-Farm'></a>
### Farm `constants`

##### Summary

Farm

<a name='F-VRDR-ValueSets-PlaceOfInjury-Home'></a>
### Home `constants`

##### Summary

Home

<a name='F-VRDR-ValueSets-PlaceOfInjury-Industrial_Or_Construction_Area'></a>
### Industrial_Or_Construction_Area `constants`

##### Summary

Industrial_Or_Construction_Area

<a name='F-VRDR-ValueSets-PlaceOfInjury-Other'></a>
### Other `constants`

##### Summary

Other

<a name='F-VRDR-ValueSets-PlaceOfInjury-Residential_Institution'></a>
### Residential_Institution `constants`

##### Summary

Residential_Institution

<a name='F-VRDR-ValueSets-PlaceOfInjury-School'></a>
### School `constants`

##### Summary

School

<a name='F-VRDR-ValueSets-PlaceOfInjury-Sports_Or_Recreational_Area'></a>
### Sports_Or_Recreational_Area `constants`

##### Summary

Sports_Or_Recreational_Area

<a name='F-VRDR-ValueSets-PlaceOfInjury-Street_Or_Highway'></a>
### Street_Or_Highway `constants`

##### Summary

Street_Or_Highway

<a name='F-VRDR-ValueSets-PlaceOfInjury-Trade_Or_Service_Area'></a>
### Trade_Or_Service_Area `constants`

##### Summary

Trade_Or_Service_Area

<a name='F-VRDR-ValueSets-PlaceOfInjury-Unspecified'></a>
### Unspecified `constants`

##### Summary

Unspecified

<a name='T-VRDR-Mappings-PregnancyStatus'></a>
## PregnancyStatus `type`

##### Namespace

VRDR.Mappings

##### Summary

Mappings for PregnancyStatus

<a name='T-VRDR-ValueSets-PregnancyStatus'></a>
## PregnancyStatus `type`

##### Namespace

VRDR.ValueSets

##### Summary

PregnancyStatus

<a name='F-VRDR-Mappings-PregnancyStatus-FHIRToIJE'></a>
### FHIRToIJE `constants`

##### Summary

FHIR -> IJE Mapping for PregnancyStatus

<a name='F-VRDR-Mappings-PregnancyStatus-IJEToFHIR'></a>
### IJEToFHIR `constants`

##### Summary

IJE -> FHIR Mapping for PregnancyStatus

<a name='F-VRDR-ValueSets-PregnancyStatus-Codes'></a>
### Codes `constants`

##### Summary

Codes

<a name='F-VRDR-ValueSets-PregnancyStatus-Not_Applicable'></a>
### Not_Applicable `constants`

##### Summary

Not_Applicable

<a name='F-VRDR-ValueSets-PregnancyStatus-Not_Pregnant_But_Pregnant_43_Days_To_1_Year_Before_Death'></a>
### Not_Pregnant_But_Pregnant_43_Days_To_1_Year_Before_Death `constants`

##### Summary

Not_Pregnant_But_Pregnant_43_Days_To_1_Year_Before_Death

<a name='F-VRDR-ValueSets-PregnancyStatus-Not_Pregnant_But_Pregnant_Within_42_Days_Of_Death'></a>
### Not_Pregnant_But_Pregnant_Within_42_Days_Of_Death `constants`

##### Summary

Not_Pregnant_But_Pregnant_Within_42_Days_Of_Death

<a name='F-VRDR-ValueSets-PregnancyStatus-Not_Pregnant_Within_Past_Year'></a>
### Not_Pregnant_Within_Past_Year `constants`

##### Summary

Not_Pregnant_Within_Past_Year

<a name='F-VRDR-ValueSets-PregnancyStatus-Pregnant_At_Time_Of_Death'></a>
### Pregnant_At_Time_Of_Death `constants`

##### Summary

Pregnant_At_Time_Of_Death

<a name='F-VRDR-ValueSets-PregnancyStatus-Unknown_If_Pregnant_Within_The_Past_Year'></a>
### Unknown_If_Pregnant_Within_The_Past_Year `constants`

##### Summary

Unknown_If_Pregnant_Within_The_Past_Year

<a name='T-VRDR-ProfileURL'></a>
## ProfileURL `type`

##### Namespace

VRDR

##### Summary

Profile URLs

<a name='F-VRDR-ProfileURL-ActivityAtTimeOfDeath'></a>
### ActivityAtTimeOfDeath `constants`

##### Summary

URL for ActivityAtTimeOfDeath

<a name='F-VRDR-ProfileURL-AutomatedUnderlyingCauseOfDeath'></a>
### AutomatedUnderlyingCauseOfDeath `constants`

##### Summary

URL for AutomatedUnderlyingCauseOfDeath

<a name='F-VRDR-ProfileURL-AutopsyPerformedIndicator'></a>
### AutopsyPerformedIndicator `constants`

##### Summary

URL for AutopsyPerformedIndicator

<a name='F-VRDR-ProfileURL-BirthRecordIdentifier'></a>
### BirthRecordIdentifier `constants`

##### Summary

URL for BirthRecordIdentifier

<a name='F-VRDR-ProfileURL-CauseOfDeathCodedContentBundle'></a>
### CauseOfDeathCodedContentBundle `constants`

##### Summary

URL for CauseOfDeathCodedContentBundle

<a name='F-VRDR-ProfileURL-CauseOfDeathPart1'></a>
### CauseOfDeathPart1 `constants`

##### Summary

URL for CauseOfDeathPart1

<a name='F-VRDR-ProfileURL-CauseOfDeathPart2'></a>
### CauseOfDeathPart2 `constants`

##### Summary

URL for CauseOfDeathPart2

<a name='F-VRDR-ProfileURL-CauseOfDeathPathway'></a>
### CauseOfDeathPathway `constants`

##### Summary

URL for CauseOfDeathPathway

<a name='F-VRDR-ProfileURL-Certifier'></a>
### Certifier `constants`

##### Summary

URL for Certifier

<a name='F-VRDR-ProfileURL-CodedRaceAndEthnicity'></a>
### CodedRaceAndEthnicity `constants`

##### Summary

URL for CodedRaceAndEthnicity

<a name='F-VRDR-ProfileURL-CodingStatusValues'></a>
### CodingStatusValues `constants`

##### Summary

URL for CodingStatusValues

<a name='F-VRDR-ProfileURL-DeathCertificate'></a>
### DeathCertificate `constants`

##### Summary

URL for DeathCertificate

<a name='F-VRDR-ProfileURL-DeathCertificateDocument'></a>
### DeathCertificateDocument `constants`

##### Summary

URL for DeathCertificateDocument

<a name='F-VRDR-ProfileURL-DeathCertification'></a>
### DeathCertification `constants`

##### Summary

URL for DeathCertification

<a name='F-VRDR-ProfileURL-DeathDate'></a>
### DeathDate `constants`

##### Summary

URL for DeathDate

<a name='F-VRDR-ProfileURL-DeathLocation'></a>
### DeathLocation `constants`

##### Summary

URL for DeathLocation

<a name='F-VRDR-ProfileURL-Decedent'></a>
### Decedent `constants`

##### Summary

URL for Decedent

<a name='F-VRDR-ProfileURL-DecedentAge'></a>
### DecedentAge `constants`

##### Summary

URL for DecedentAge

<a name='F-VRDR-ProfileURL-DecedentDispositionMethod'></a>
### DecedentDispositionMethod `constants`

##### Summary

URL for DecedentDispositionMethod

<a name='F-VRDR-ProfileURL-DecedentEducationLevel'></a>
### DecedentEducationLevel `constants`

##### Summary

URL for DecedentEducationLevel

<a name='F-VRDR-ProfileURL-DecedentFather'></a>
### DecedentFather `constants`

##### Summary

URL for DecedentFather

<a name='F-VRDR-ProfileURL-DecedentMilitaryService'></a>
### DecedentMilitaryService `constants`

##### Summary

URL for DecedentMilitaryService

<a name='F-VRDR-ProfileURL-DecedentMother'></a>
### DecedentMother `constants`

##### Summary

URL for DecedentMother

<a name='F-VRDR-ProfileURL-DecedentPregnancyStatus'></a>
### DecedentPregnancyStatus `constants`

##### Summary

URL for DecedentPregnancyStatus

<a name='F-VRDR-ProfileURL-DecedentSpouse'></a>
### DecedentSpouse `constants`

##### Summary

URL for DecedentSpouse

<a name='F-VRDR-ProfileURL-DecedentUsualWork'></a>
### DecedentUsualWork `constants`

##### Summary

URL for DecedentUsualWork

<a name='F-VRDR-ProfileURL-DemographicCodedContentBundle'></a>
### DemographicCodedContentBundle `constants`

##### Summary

URL for DemographicCodedContentBundle

<a name='F-VRDR-ProfileURL-DispositionLocation'></a>
### DispositionLocation `constants`

##### Summary

URL for DispositionLocation

<a name='F-VRDR-ProfileURL-EmergingIssues'></a>
### EmergingIssues `constants`

##### Summary

URL for EmergingIssues

<a name='F-VRDR-ProfileURL-EntityAxisCauseOfDeath'></a>
### EntityAxisCauseOfDeath `constants`

##### Summary

URL for EntityAxisCauseOfDeath

<a name='F-VRDR-ProfileURL-ExaminerContacted'></a>
### ExaminerContacted `constants`

##### Summary

URL for ExaminerContacted

<a name='F-VRDR-ProfileURL-FuneralHome'></a>
### FuneralHome `constants`

##### Summary

URL for FuneralHome

<a name='F-VRDR-ProfileURL-InjuryIncident'></a>
### InjuryIncident `constants`

##### Summary

URL for InjuryIncident

<a name='F-VRDR-ProfileURL-InjuryLocation'></a>
### InjuryLocation `constants`

##### Summary

URL for InjuryLocation

<a name='F-VRDR-ProfileURL-InputRaceAndEthnicity'></a>
### InputRaceAndEthnicity `constants`

##### Summary

URL for InputRaceAndEthnicity

<a name='F-VRDR-ProfileURL-MannerOfDeath'></a>
### MannerOfDeath `constants`

##### Summary

URL for MannerOfDeath

<a name='F-VRDR-ProfileURL-ManualUnderlyingCauseOfDeath'></a>
### ManualUnderlyingCauseOfDeath `constants`

##### Summary

URL for ManualUnderlyingCauseOfDeath

<a name='F-VRDR-ProfileURL-PlaceOfInjury'></a>
### PlaceOfInjury `constants`

##### Summary

URL for PlaceOfInjury

<a name='F-VRDR-ProfileURL-RecordAxisCauseOfDeath'></a>
### RecordAxisCauseOfDeath `constants`

##### Summary

URL for RecordAxisCauseOfDeath

<a name='F-VRDR-ProfileURL-SurgeryDate'></a>
### SurgeryDate `constants`

##### Summary

URL for SurgeryDate

<a name='F-VRDR-ProfileURL-TobaccoUseContributedToDeath'></a>
### TobaccoUseContributedToDeath `constants`

##### Summary

URL for TobaccoUseContributedToDeath

<a name='T-VRDR-Property'></a>
## Property `type`

##### Namespace

VRDR

##### Summary

Property attribute used to describe a DeathRecord property.

<a name='M-VRDR-Property-#ctor-System-String,VRDR-Property-Types,System-String,System-String,System-Boolean,System-String,System-Boolean,System-Int32-'></a>
### #ctor() `constructor`

##### Summary

Constructor.

##### Parameters

This constructor has no parameters.

<a name='F-VRDR-Property-CapturedInIJE'></a>
### CapturedInIJE `constants`

##### Summary

If this field has an equivalent in IJE.

<a name='F-VRDR-Property-Category'></a>
### Category `constants`

##### Summary

Category of this property.

<a name='F-VRDR-Property-Description'></a>
### Description `constants`

##### Summary

Description of this property.

<a name='F-VRDR-Property-IGUrl'></a>
### IGUrl `constants`

##### Summary

URL that links to the IG description for this property.

<a name='F-VRDR-Property-Name'></a>
### Name `constants`

##### Summary

Name of this property.

<a name='F-VRDR-Property-Priority'></a>
### Priority `constants`

##### Summary

Priority that this should show up in generated lists. Lower numbers come first.

<a name='F-VRDR-Property-Serialize'></a>
### Serialize `constants`

##### Summary

If this field should be kept when serialzing.

<a name='F-VRDR-Property-Type'></a>
### Type `constants`

##### Summary

The property type (e.g. string, bool, Dictionary).

<a name='T-VRDR-PropertyParam'></a>
## PropertyParam `type`

##### Namespace

VRDR

##### Summary

Property attribute used to describe a DeathRecord property parameter,
specifically if the property is a dictionary that has keys.

<a name='M-VRDR-PropertyParam-#ctor-System-String,System-String-'></a>
### #ctor() `constructor`

##### Summary

Constructor.

##### Parameters

This constructor has no parameters.

<a name='F-VRDR-PropertyParam-Description'></a>
### Description `constants`

##### Summary

Description of this parameter.

<a name='F-VRDR-PropertyParam-Key'></a>
### Key `constants`

##### Summary

If the related property is a Dictionary, the key name.

<a name='T-VRDR-Mappings-RaceCode'></a>
## RaceCode `type`

##### Namespace

VRDR.Mappings

##### Summary

Mappings for RaceCode

<a name='T-VRDR-ValueSets-RaceCode'></a>
## RaceCode `type`

##### Namespace

VRDR.ValueSets

##### Summary

RaceCode

<a name='F-VRDR-Mappings-RaceCode-FHIRToIJE'></a>
### FHIRToIJE `constants`

##### Summary

FHIR -> IJE Mapping for RaceCode

<a name='F-VRDR-Mappings-RaceCode-IJEToFHIR'></a>
### IJEToFHIR `constants`

##### Summary

IJE -> FHIR Mapping for RaceCode

<a name='F-VRDR-ValueSets-RaceCode-Abenaki_Nation_Of_Missiquoi'></a>
### Abenaki_Nation_Of_Missiquoi `constants`

##### Summary

Abenaki_Nation_Of_Missiquoi

<a name='F-VRDR-ValueSets-RaceCode-Absentee_Shawnee_Tribe_Of_Indians_Of_Oklahoma'></a>
### Absentee_Shawnee_Tribe_Of_Indians_Of_Oklahoma `constants`

##### Summary

Absentee_Shawnee_Tribe_Of_Indians_Of_Oklahoma

<a name='F-VRDR-ValueSets-RaceCode-Acoma'></a>
### Acoma `constants`

##### Summary

Acoma

<a name='F-VRDR-ValueSets-RaceCode-Afghanistani'></a>
### Afghanistani `constants`

##### Summary

Afghanistani

<a name='F-VRDR-ValueSets-RaceCode-African'></a>
### African `constants`

##### Summary

African

<a name='F-VRDR-ValueSets-RaceCode-African_American'></a>
### African_American `constants`

##### Summary

African_American

<a name='F-VRDR-ValueSets-RaceCode-Afroamerican'></a>
### Afroamerican `constants`

##### Summary

Afroamerican

<a name='F-VRDR-ValueSets-RaceCode-Agdaagux_Tribe_Of_King_Cove'></a>
### Agdaagux_Tribe_Of_King_Cove `constants`

##### Summary

Agdaagux_Tribe_Of_King_Cove

<a name='F-VRDR-ValueSets-RaceCode-Agua_Caliente'></a>
### Agua_Caliente `constants`

##### Summary

Agua_Caliente

<a name='F-VRDR-ValueSets-RaceCode-Agua_Caliente_Band_Of_Cahuilla_Indians'></a>
### Agua_Caliente_Band_Of_Cahuilla_Indians `constants`

##### Summary

Agua_Caliente_Band_Of_Cahuilla_Indians

<a name='F-VRDR-ValueSets-RaceCode-Ahtna'></a>
### Ahtna `constants`

##### Summary

Ahtna

<a name='F-VRDR-ValueSets-RaceCode-Akchin'></a>
### Akchin `constants`

##### Summary

Akchin

<a name='F-VRDR-ValueSets-RaceCode-Akiachak_Native_Community'></a>
### Akiachak_Native_Community `constants`

##### Summary

Akiachak_Native_Community

<a name='F-VRDR-ValueSets-RaceCode-Akiak_Native_Community'></a>
### Akiak_Native_Community `constants`

##### Summary

Akiak_Native_Community

<a name='F-VRDR-ValueSets-RaceCode-Alabama_Coushatta_Tribes_Of_Texas'></a>
### Alabama_Coushatta_Tribes_Of_Texas `constants`

##### Summary

Alabama_Coushatta_Tribes_Of_Texas

<a name='F-VRDR-ValueSets-RaceCode-Alabama_Creek'></a>
### Alabama_Creek `constants`

##### Summary

Alabama_Creek

<a name='F-VRDR-ValueSets-RaceCode-Alabama_Quassarte_Tribal_Town'></a>
### Alabama_Quassarte_Tribal_Town `constants`

##### Summary

Alabama_Quassarte_Tribal_Town

<a name='F-VRDR-ValueSets-RaceCode-Alamo_Navajo'></a>
### Alamo_Navajo `constants`

##### Summary

Alamo_Navajo

<a name='F-VRDR-ValueSets-RaceCode-Alanvik'></a>
### Alanvik `constants`

##### Summary

Alanvik

<a name='F-VRDR-ValueSets-RaceCode-Alaska_Indian'></a>
### Alaska_Indian `constants`

##### Summary

Alaska_Indian

<a name='F-VRDR-ValueSets-RaceCode-Alaska_Native'></a>
### Alaska_Native `constants`

##### Summary

Alaska_Native

<a name='F-VRDR-ValueSets-RaceCode-Alaskan_Athabascan'></a>
### Alaskan_Athabascan `constants`

##### Summary

Alaskan_Athabascan

<a name='F-VRDR-ValueSets-RaceCode-Alatna_Village'></a>
### Alatna_Village `constants`

##### Summary

Alatna_Village

<a name='F-VRDR-ValueSets-RaceCode-Albanian'></a>
### Albanian `constants`

##### Summary

Albanian

<a name='F-VRDR-ValueSets-RaceCode-Aleut'></a>
### Aleut `constants`

##### Summary

Aleut

<a name='F-VRDR-ValueSets-RaceCode-Aleut_Corporation'></a>
### Aleut_Corporation `constants`

##### Summary

Aleut_Corporation

<a name='F-VRDR-ValueSets-RaceCode-Aleutian'></a>
### Aleutian `constants`

##### Summary

Aleutian

<a name='F-VRDR-ValueSets-RaceCode-Aleutian_Islander'></a>
### Aleutian_Islander `constants`

##### Summary

Aleutian_Islander

<a name='F-VRDR-ValueSets-RaceCode-Alexander'></a>
### Alexander `constants`

##### Summary

Alexander

<a name='F-VRDR-ValueSets-RaceCode-Algaaciq_Native_Village'></a>
### Algaaciq_Native_Village `constants`

##### Summary

Algaaciq_Native_Village

<a name='F-VRDR-ValueSets-RaceCode-Algonquian'></a>
### Algonquian `constants`

##### Summary

Algonquian

<a name='F-VRDR-ValueSets-RaceCode-Alien_Canyon'></a>
### Alien_Canyon `constants`

##### Summary

Alien_Canyon

<a name='F-VRDR-ValueSets-RaceCode-Allakaket_Village'></a>
### Allakaket_Village `constants`

##### Summary

Allakaket_Village

<a name='F-VRDR-ValueSets-RaceCode-Alpine'></a>
### Alpine `constants`

##### Summary

Alpine

<a name='F-VRDR-ValueSets-RaceCode-Alsea'></a>
### Alsea `constants`

##### Summary

Alsea

<a name='F-VRDR-ValueSets-RaceCode-Alturas_Indian_Rancheria'></a>
### Alturas_Indian_Rancheria `constants`

##### Summary

Alturas_Indian_Rancheria

<a name='F-VRDR-ValueSets-RaceCode-Alutiiq'></a>
### Alutiiq `constants`

##### Summary

Alutiiq

<a name='F-VRDR-ValueSets-RaceCode-Alutiiq_Aleut'></a>
### Alutiiq_Aleut `constants`

##### Summary

Alutiiq_Aleut

<a name='F-VRDR-ValueSets-RaceCode-Amerasian'></a>
### Amerasian `constants`

##### Summary

Amerasian

<a name='F-VRDR-ValueSets-RaceCode-American_Eskimo'></a>
### American_Eskimo `constants`

##### Summary

American_Eskimo

<a name='F-VRDR-ValueSets-RaceCode-American_Indian'></a>
### American_Indian `constants`

##### Summary

American_Indian

<a name='F-VRDR-ValueSets-RaceCode-American_Indian_Checkbox'></a>
### American_Indian_Checkbox `constants`

##### Summary

American_Indian_Checkbox

<a name='F-VRDR-ValueSets-RaceCode-Anaktuvuk'></a>
### Anaktuvuk `constants`

##### Summary

Anaktuvuk

<a name='F-VRDR-ValueSets-RaceCode-Angoon_Community_Association'></a>
### Angoon_Community_Association `constants`

##### Summary

Angoon_Community_Association

<a name='F-VRDR-ValueSets-RaceCode-Anstohini'></a>
### Anstohini `constants`

##### Summary

Anstohini

<a name='F-VRDR-ValueSets-RaceCode-Anvik_Village'></a>
### Anvik_Village `constants`

##### Summary

Anvik_Village

<a name='F-VRDR-ValueSets-RaceCode-Apache'></a>
### Apache `constants`

##### Summary

Apache

<a name='F-VRDR-ValueSets-RaceCode-Arab'></a>
### Arab `constants`

##### Summary

Arab

<a name='F-VRDR-ValueSets-RaceCode-Arapahoe'></a>
### Arapahoe `constants`

##### Summary

Arapahoe

<a name='F-VRDR-ValueSets-RaceCode-Arctic_Slope_Corporation'></a>
### Arctic_Slope_Corporation `constants`

##### Summary

Arctic_Slope_Corporation

<a name='F-VRDR-ValueSets-RaceCode-Arctic_Village'></a>
### Arctic_Village `constants`

##### Summary

Arctic_Village

<a name='F-VRDR-ValueSets-RaceCode-Argentinean'></a>
### Argentinean `constants`

##### Summary

Argentinean

<a name='F-VRDR-ValueSets-RaceCode-Arikara'></a>
### Arikara `constants`

##### Summary

Arikara

<a name='F-VRDR-ValueSets-RaceCode-Arizona_Tewa'></a>
### Arizona_Tewa `constants`

##### Summary

Arizona_Tewa

<a name='F-VRDR-ValueSets-RaceCode-Armenian'></a>
### Armenian `constants`

##### Summary

Armenian

<a name='F-VRDR-ValueSets-RaceCode-Aroostook_Band'></a>
### Aroostook_Band `constants`

##### Summary

Aroostook_Band

<a name='F-VRDR-ValueSets-RaceCode-Aruba_Islander'></a>
### Aruba_Islander `constants`

##### Summary

Aruba_Islander

<a name='F-VRDR-ValueSets-RaceCode-Aryan'></a>
### Aryan `constants`

##### Summary

Aryan

<a name='F-VRDR-ValueSets-RaceCode-Asacarsarmiut_Tribe'></a>
### Asacarsarmiut_Tribe `constants`

##### Summary

Asacarsarmiut_Tribe

<a name='F-VRDR-ValueSets-RaceCode-Asian'></a>
### Asian `constants`

##### Summary

Asian

<a name='F-VRDR-ValueSets-RaceCode-Asian2'></a>
### Asian2 `constants`

##### Summary

Asian2

<a name='F-VRDR-ValueSets-RaceCode-Asian_Indian'></a>
### Asian_Indian `constants`

##### Summary

Asian_Indian

<a name='F-VRDR-ValueSets-RaceCode-Asiatic'></a>
### Asiatic `constants`

##### Summary

Asiatic

<a name='F-VRDR-ValueSets-RaceCode-Assiniboine'></a>
### Assiniboine `constants`

##### Summary

Assiniboine

<a name='F-VRDR-ValueSets-RaceCode-Assiniboine_Sioux'></a>
### Assiniboine_Sioux `constants`

##### Summary

Assiniboine_Sioux

<a name='F-VRDR-ValueSets-RaceCode-Assyrian'></a>
### Assyrian `constants`

##### Summary

Assyrian

<a name='F-VRDR-ValueSets-RaceCode-Atqasuk_Village'></a>
### Atqasuk_Village `constants`

##### Summary

Atqasuk_Village

<a name='F-VRDR-ValueSets-RaceCode-Atsina'></a>
### Atsina `constants`

##### Summary

Atsina

<a name='F-VRDR-ValueSets-RaceCode-Attacapa'></a>
### Attacapa `constants`

##### Summary

Attacapa

<a name='F-VRDR-ValueSets-RaceCode-Augustine'></a>
### Augustine `constants`

##### Summary

Augustine

<a name='F-VRDR-ValueSets-RaceCode-Azerbaijani'></a>
### Azerbaijani `constants`

##### Summary

Azerbaijani

<a name='F-VRDR-ValueSets-RaceCode-Bad_River_Band_Of_The_Lake_Superior_Tribe'></a>
### Bad_River_Band_Of_The_Lake_Superior_Tribe `constants`

##### Summary

Bad_River_Band_Of_The_Lake_Superior_Tribe

<a name='F-VRDR-ValueSets-RaceCode-Bahamian'></a>
### Bahamian `constants`

##### Summary

Bahamian

<a name='F-VRDR-ValueSets-RaceCode-Bangladeshi'></a>
### Bangladeshi `constants`

##### Summary

Bangladeshi

<a name='F-VRDR-ValueSets-RaceCode-Bannock'></a>
### Bannock `constants`

##### Summary

Bannock

<a name='F-VRDR-ValueSets-RaceCode-Barbadian'></a>
### Barbadian `constants`

##### Summary

Barbadian

<a name='F-VRDR-ValueSets-RaceCode-Barona_Group_Of_Capitan_Grande_Band'></a>
### Barona_Group_Of_Capitan_Grande_Band `constants`

##### Summary

Barona_Group_Of_Capitan_Grande_Band

<a name='F-VRDR-ValueSets-RaceCode-Barrio_Libre'></a>
### Barrio_Libre `constants`

##### Summary

Barrio_Libre

<a name='F-VRDR-ValueSets-RaceCode-Battle_Mountain'></a>
### Battle_Mountain `constants`

##### Summary

Battle_Mountain

<a name='F-VRDR-ValueSets-RaceCode-Bay_Mills_Indian_Community_Of_The_Sault_Ste_Marie_Band'></a>
### Bay_Mills_Indian_Community_Of_The_Sault_Ste_Marie_Band `constants`

##### Summary

Bay_Mills_Indian_Community_Of_The_Sault_Ste_Marie_Band

<a name='F-VRDR-ValueSets-RaceCode-Bear_River_Band_Of_Rohnerville_Rancheria'></a>
### Bear_River_Band_Of_Rohnerville_Rancheria `constants`

##### Summary

Bear_River_Band_Of_Rohnerville_Rancheria

<a name='F-VRDR-ValueSets-RaceCode-Beaver_Village'></a>
### Beaver_Village `constants`

##### Summary

Beaver_Village

<a name='F-VRDR-ValueSets-RaceCode-Belizean'></a>
### Belizean `constants`

##### Summary

Belizean

<a name='F-VRDR-ValueSets-RaceCode-Bering_Straits_Inupiat'></a>
### Bering_Straits_Inupiat `constants`

##### Summary

Bering_Straits_Inupiat

<a name='F-VRDR-ValueSets-RaceCode-Bermudan'></a>
### Bermudan `constants`

##### Summary

Bermudan

<a name='F-VRDR-ValueSets-RaceCode-Berry_Creek_Rancheria_Of_Maidu_Indians'></a>
### Berry_Creek_Rancheria_Of_Maidu_Indians `constants`

##### Summary

Berry_Creek_Rancheria_Of_Maidu_Indians

<a name='F-VRDR-ValueSets-RaceCode-Bhutanese'></a>
### Bhutanese `constants`

##### Summary

Bhutanese

<a name='F-VRDR-ValueSets-RaceCode-Big_Cypress'></a>
### Big_Cypress `constants`

##### Summary

Big_Cypress

<a name='F-VRDR-ValueSets-RaceCode-Big_Lagoon_Rancheria'></a>
### Big_Lagoon_Rancheria `constants`

##### Summary

Big_Lagoon_Rancheria

<a name='F-VRDR-ValueSets-RaceCode-Big_Pine_Band_Of_Owens_Valley_Paiuteshoshone'></a>
### Big_Pine_Band_Of_Owens_Valley_Paiuteshoshone `constants`

##### Summary

Big_Pine_Band_Of_Owens_Valley_Paiuteshoshone

<a name='F-VRDR-ValueSets-RaceCode-Big_Sandy_Rancheria'></a>
### Big_Sandy_Rancheria `constants`

##### Summary

Big_Sandy_Rancheria

<a name='F-VRDR-ValueSets-RaceCode-Big_Valley_Rancheria_Of_Pomo_And_Pit_River_Indians'></a>
### Big_Valley_Rancheria_Of_Pomo_And_Pit_River_Indians `constants`

##### Summary

Big_Valley_Rancheria_Of_Pomo_And_Pit_River_Indians

<a name='F-VRDR-ValueSets-RaceCode-Biloxi'></a>
### Biloxi `constants`

##### Summary

Biloxi

<a name='F-VRDR-ValueSets-RaceCode-Biracial'></a>
### Biracial `constants`

##### Summary

Biracial

<a name='F-VRDR-ValueSets-RaceCode-Birch_Crcek_Village'></a>
### Birch_Crcek_Village `constants`

##### Summary

Birch_Crcek_Village

<a name='F-VRDR-ValueSets-RaceCode-Bishop'></a>
### Bishop `constants`

##### Summary

Bishop

<a name='F-VRDR-ValueSets-RaceCode-Black'></a>
### Black `constants`

##### Summary

Black

<a name='F-VRDR-ValueSets-RaceCode-Black_Or_African_American'></a>
### Black_Or_African_American `constants`

##### Summary

Black_Or_African_American

<a name='F-VRDR-ValueSets-RaceCode-Blackfeet'></a>
### Blackfeet `constants`

##### Summary

Blackfeet

<a name='F-VRDR-ValueSets-RaceCode-Blackfoot_Sioux'></a>
### Blackfoot_Sioux `constants`

##### Summary

Blackfoot_Sioux

<a name='F-VRDR-ValueSets-RaceCode-Blue_Lake_Rancheria'></a>
### Blue_Lake_Rancheria `constants`

##### Summary

Blue_Lake_Rancheria

<a name='F-VRDR-ValueSets-RaceCode-Bois_Forte_Nett_Lake_Band_Of_Chippewa'></a>
### Bois_Forte_Nett_Lake_Band_Of_Chippewa `constants`

##### Summary

Bois_Forte_Nett_Lake_Band_Of_Chippewa

<a name='F-VRDR-ValueSets-RaceCode-Bolivian'></a>
### Bolivian `constants`

##### Summary

Bolivian

<a name='F-VRDR-ValueSets-RaceCode-Bosnian'></a>
### Bosnian `constants`

##### Summary

Bosnian

<a name='F-VRDR-ValueSets-RaceCode-Botswana'></a>
### Botswana `constants`

##### Summary

Botswana

<a name='F-VRDR-ValueSets-RaceCode-Brazilian'></a>
### Brazilian `constants`

##### Summary

Brazilian

<a name='F-VRDR-ValueSets-RaceCode-Bridgeport_Paiute_Indian_Colony'></a>
### Bridgeport_Paiute_Indian_Colony `constants`

##### Summary

Bridgeport_Paiute_Indian_Colony

<a name='F-VRDR-ValueSets-RaceCode-Brighton'></a>
### Brighton `constants`

##### Summary

Brighton

<a name='F-VRDR-ValueSets-RaceCode-Bristol_Bay'></a>
### Bristol_Bay `constants`

##### Summary

Bristol_Bay

<a name='F-VRDR-ValueSets-RaceCode-Bristol_Bay_Aleut'></a>
### Bristol_Bay_Aleut `constants`

##### Summary

Bristol_Bay_Aleut

<a name='F-VRDR-ValueSets-RaceCode-Brotherton'></a>
### Brotherton `constants`

##### Summary

Brotherton

<a name='F-VRDR-ValueSets-RaceCode-Brown'></a>
### Brown `constants`

##### Summary

Brown

<a name='F-VRDR-ValueSets-RaceCode-Brule_Sioux'></a>
### Brule_Sioux `constants`

##### Summary

Brule_Sioux

<a name='F-VRDR-ValueSets-RaceCode-Buena_Vista_Rancheria_Of_Mewuk_Indians_Of_California'></a>
### Buena_Vista_Rancheria_Of_Mewuk_Indians_Of_California `constants`

##### Summary

Buena_Vista_Rancheria_Of_Mewuk_Indians_Of_California

<a name='F-VRDR-ValueSets-RaceCode-Burmese'></a>
### Burmese `constants`

##### Summary

Burmese

<a name='F-VRDR-ValueSets-RaceCode-Burns_Paiute_Tribe'></a>
### Burns_Paiute_Tribe `constants`

##### Summary

Burns_Paiute_Tribe

<a name='F-VRDR-ValueSets-RaceCode-Burt_Lake_Band'></a>
### Burt_Lake_Band `constants`

##### Summary

Burt_Lake_Band

<a name='F-VRDR-ValueSets-RaceCode-Burt_Lake_Chippewa'></a>
### Burt_Lake_Chippewa `constants`

##### Summary

Burt_Lake_Chippewa

<a name='F-VRDR-ValueSets-RaceCode-Burt_Lake_Ottawa'></a>
### Burt_Lake_Ottawa `constants`

##### Summary

Burt_Lake_Ottawa

<a name='F-VRDR-ValueSets-RaceCode-Bushwacker'></a>
### Bushwacker `constants`

##### Summary

Bushwacker

<a name='F-VRDR-ValueSets-RaceCode-Cabazon_Band_Of_Cahuilla_Mission_Indians'></a>
### Cabazon_Band_Of_Cahuilla_Mission_Indians `constants`

##### Summary

Cabazon_Band_Of_Cahuilla_Mission_Indians

<a name='F-VRDR-ValueSets-RaceCode-Cachil_Dehe_Band_Of_Wintun_Indians_Of_The_Colusa_Rancheria'></a>
### Cachil_Dehe_Band_Of_Wintun_Indians_Of_The_Colusa_Rancheria `constants`

##### Summary

Cachil_Dehe_Band_Of_Wintun_Indians_Of_The_Colusa_Rancheria

<a name='F-VRDR-ValueSets-RaceCode-Caddo'></a>
### Caddo `constants`

##### Summary

Caddo

<a name='F-VRDR-ValueSets-RaceCode-Caddo_Adais_Indians'></a>
### Caddo_Adais_Indians `constants`

##### Summary

Caddo_Adais_Indians

<a name='F-VRDR-ValueSets-RaceCode-Caddo_Indian_Tribe_Of_Oklahoma'></a>
### Caddo_Indian_Tribe_Of_Oklahoma `constants`

##### Summary

Caddo_Indian_Tribe_Of_Oklahoma

<a name='F-VRDR-ValueSets-RaceCode-Cahto_Indian_Tribe_Of_The_Laytonville_Rancheria'></a>
### Cahto_Indian_Tribe_Of_The_Laytonville_Rancheria `constants`

##### Summary

Cahto_Indian_Tribe_Of_The_Laytonville_Rancheria

<a name='F-VRDR-ValueSets-RaceCode-Cahuilla'></a>
### Cahuilla `constants`

##### Summary

Cahuilla

<a name='F-VRDR-ValueSets-RaceCode-Cahuilla_Band_Of_Mission_Indians'></a>
### Cahuilla_Band_Of_Mission_Indians `constants`

##### Summary

Cahuilla_Band_Of_Mission_Indians

<a name='F-VRDR-ValueSets-RaceCode-Cajun'></a>
### Cajun `constants`

##### Summary

Cajun

<a name='F-VRDR-ValueSets-RaceCode-California_Tribes_N_E_C'></a>
### California_Tribes_N_E_C `constants`

##### Summary

California_Tribes_N_E_C

<a name='F-VRDR-ValueSets-RaceCode-Californio'></a>
### Californio `constants`

##### Summary

Californio

<a name='F-VRDR-ValueSets-RaceCode-Calista'></a>
### Calista `constants`

##### Summary

Calista

<a name='F-VRDR-ValueSets-RaceCode-Cambodian'></a>
### Cambodian `constants`

##### Summary

Cambodian

<a name='F-VRDR-ValueSets-RaceCode-Campo_Band_Of_Diegueno_Mission_Indians'></a>
### Campo_Band_Of_Diegueno_Mission_Indians `constants`

##### Summary

Campo_Band_Of_Diegueno_Mission_Indians

<a name='F-VRDR-ValueSets-RaceCode-Canadian'></a>
### Canadian `constants`

##### Summary

Canadian

<a name='F-VRDR-ValueSets-RaceCode-Canadian_Indian'></a>
### Canadian_Indian `constants`

##### Summary

Canadian_Indian

<a name='F-VRDR-ValueSets-RaceCode-Cape_Verdean'></a>
### Cape_Verdean `constants`

##### Summary

Cape_Verdean

<a name='F-VRDR-ValueSets-RaceCode-Capitan_Grande_Band_Of_Diegueno_Mission_Indians'></a>
### Capitan_Grande_Band_Of_Diegueno_Mission_Indians `constants`

##### Summary

Capitan_Grande_Band_Of_Diegueno_Mission_Indians

<a name='F-VRDR-ValueSets-RaceCode-Carolinian'></a>
### Carolinian `constants`

##### Summary

Carolinian

<a name='F-VRDR-ValueSets-RaceCode-Carson_Colony'></a>
### Carson_Colony `constants`

##### Summary

Carson_Colony

<a name='F-VRDR-ValueSets-RaceCode-Catawba_Indian_Nation'></a>
### Catawba_Indian_Nation `constants`

##### Summary

Catawba_Indian_Nation

<a name='F-VRDR-ValueSets-RaceCode-Cayenne'></a>
### Cayenne `constants`

##### Summary

Cayenne

<a name='F-VRDR-ValueSets-RaceCode-Cayman_Islander'></a>
### Cayman_Islander `constants`

##### Summary

Cayman_Islander

<a name='F-VRDR-ValueSets-RaceCode-Cayuga_Nation'></a>
### Cayuga_Nation `constants`

##### Summary

Cayuga_Nation

<a name='F-VRDR-ValueSets-RaceCode-Cayuse'></a>
### Cayuse `constants`

##### Summary

Cayuse

<a name='F-VRDR-ValueSets-RaceCode-Cedarville_Rancheria'></a>
### Cedarville_Rancheria `constants`

##### Summary

Cedarville_Rancheria

<a name='F-VRDR-ValueSets-RaceCode-Celilo'></a>
### Celilo `constants`

##### Summary

Celilo

<a name='F-VRDR-ValueSets-RaceCode-Central_American'></a>
### Central_American `constants`

##### Summary

Central_American

<a name='F-VRDR-ValueSets-RaceCode-Central_American_Indian'></a>
### Central_American_Indian `constants`

##### Summary

Central_American_Indian

<a name='F-VRDR-ValueSets-RaceCode-Central_Council_Of_The_Tlingit_And_Haida_Indian_Tribes'></a>
### Central_Council_Of_The_Tlingit_And_Haida_Indian_Tribes `constants`

##### Summary

Central_Council_Of_The_Tlingit_And_Haida_Indian_Tribes

<a name='F-VRDR-ValueSets-RaceCode-Central_Pomo'></a>
### Central_Pomo `constants`

##### Summary

Central_Pomo

<a name='F-VRDR-ValueSets-RaceCode-Chalkyitsik_Village'></a>
### Chalkyitsik_Village `constants`

##### Summary

Chalkyitsik_Village

<a name='F-VRDR-ValueSets-RaceCode-Chamorro'></a>
### Chamorro `constants`

##### Summary

Chamorro

<a name='F-VRDR-ValueSets-RaceCode-Chaubunagungameg_Nipmuc'></a>
### Chaubunagungameg_Nipmuc `constants`

##### Summary

Chaubunagungameg_Nipmuc

<a name='F-VRDR-ValueSets-RaceCode-Chehalis'></a>
### Chehalis `constants`

##### Summary

Chehalis

<a name='F-VRDR-ValueSets-RaceCode-Chemakuan'></a>
### Chemakuan `constants`

##### Summary

Chemakuan

<a name='F-VRDR-ValueSets-RaceCode-Chemehuevi'></a>
### Chemehuevi `constants`

##### Summary

Chemehuevi

<a name='F-VRDR-ValueSets-RaceCode-Cherae_Indian_Community_Of_Trinidad_Rancheria'></a>
### Cherae_Indian_Community_Of_Trinidad_Rancheria `constants`

##### Summary

Cherae_Indian_Community_Of_Trinidad_Rancheria

<a name='F-VRDR-ValueSets-RaceCode-Cherokee'></a>
### Cherokee `constants`

##### Summary

Cherokee

<a name='F-VRDR-ValueSets-RaceCode-Cherokee_Alabama'></a>
### Cherokee_Alabama `constants`

##### Summary

Cherokee_Alabama

<a name='F-VRDR-ValueSets-RaceCode-Cherokee_Of_Georgia'></a>
### Cherokee_Of_Georgia `constants`

##### Summary

Cherokee_Of_Georgia

<a name='F-VRDR-ValueSets-RaceCode-Cherokee_Shawnee'></a>
### Cherokee_Shawnee `constants`

##### Summary

Cherokee_Shawnee

<a name='F-VRDR-ValueSets-RaceCode-Cherokees_Of_Northeast_Alabama'></a>
### Cherokees_Of_Northeast_Alabama `constants`

##### Summary

Cherokees_Of_Northeast_Alabama

<a name='F-VRDR-ValueSets-RaceCode-Cherokees_Of_Southeast_Alabama'></a>
### Cherokees_Of_Southeast_Alabama `constants`

##### Summary

Cherokees_Of_Southeast_Alabama

<a name='F-VRDR-ValueSets-RaceCode-Chevak_Native_Village'></a>
### Chevak_Native_Village `constants`

##### Summary

Chevak_Native_Village

<a name='F-VRDR-ValueSets-RaceCode-Cheyenne'></a>
### Cheyenne `constants`

##### Summary

Cheyenne

<a name='F-VRDR-ValueSets-RaceCode-Cheyenne_Arapaho'></a>
### Cheyenne_Arapaho `constants`

##### Summary

Cheyenne_Arapaho

<a name='F-VRDR-ValueSets-RaceCode-Cheyenne_River_Sioux'></a>
### Cheyenne_River_Sioux `constants`

##### Summary

Cheyenne_River_Sioux

<a name='F-VRDR-ValueSets-RaceCode-Chicano'></a>
### Chicano `constants`

##### Summary

Chicano

<a name='F-VRDR-ValueSets-RaceCode-Chickahominy_Eastern_Band'></a>
### Chickahominy_Eastern_Band `constants`

##### Summary

Chickahominy_Eastern_Band

<a name='F-VRDR-ValueSets-RaceCode-Chickahominy_Indian_Tribe'></a>
### Chickahominy_Indian_Tribe `constants`

##### Summary

Chickahominy_Indian_Tribe

<a name='F-VRDR-ValueSets-RaceCode-Chickaloon_Native_Village'></a>
### Chickaloon_Native_Village `constants`

##### Summary

Chickaloon_Native_Village

<a name='F-VRDR-ValueSets-RaceCode-Chickasaw'></a>
### Chickasaw `constants`

##### Summary

Chickasaw

<a name='F-VRDR-ValueSets-RaceCode-Chicken_Ranch_Rancheria_Of_Mewuk_Indians'></a>
### Chicken_Ranch_Rancheria_Of_Mewuk_Indians `constants`

##### Summary

Chicken_Ranch_Rancheria_Of_Mewuk_Indians

<a name='F-VRDR-ValueSets-RaceCode-Chignik_Lake_Village'></a>
### Chignik_Lake_Village `constants`

##### Summary

Chignik_Lake_Village

<a name='F-VRDR-ValueSets-RaceCode-Chilean'></a>
### Chilean `constants`

##### Summary

Chilean

<a name='F-VRDR-ValueSets-RaceCode-Chilkat_Indian_Village'></a>
### Chilkat_Indian_Village `constants`

##### Summary

Chilkat_Indian_Village

<a name='F-VRDR-ValueSets-RaceCode-Chilkoot_Indian_Association'></a>
### Chilkoot_Indian_Association `constants`

##### Summary

Chilkoot_Indian_Association

<a name='F-VRDR-ValueSets-RaceCode-Chimariko'></a>
### Chimariko `constants`

##### Summary

Chimariko

<a name='F-VRDR-ValueSets-RaceCode-Chinese'></a>
### Chinese `constants`

##### Summary

Chinese

<a name='F-VRDR-ValueSets-RaceCode-Chinik_Eskimo_Community'></a>
### Chinik_Eskimo_Community `constants`

##### Summary

Chinik_Eskimo_Community

<a name='F-VRDR-ValueSets-RaceCode-Chinook'></a>
### Chinook `constants`

##### Summary

Chinook

<a name='F-VRDR-ValueSets-RaceCode-Chippewa'></a>
### Chippewa `constants`

##### Summary

Chippewa

<a name='F-VRDR-ValueSets-RaceCode-Chiricahua'></a>
### Chiricahua `constants`

##### Summary

Chiricahua

<a name='F-VRDR-ValueSets-RaceCode-Chitimacha_Tribe_Of_Louisiana'></a>
### Chitimacha_Tribe_Of_Louisiana `constants`

##### Summary

Chitimacha_Tribe_Of_Louisiana

<a name='F-VRDR-ValueSets-RaceCode-Chocolate'></a>
### Chocolate `constants`

##### Summary

Chocolate

<a name='F-VRDR-ValueSets-RaceCode-Choctaw'></a>
### Choctaw `constants`

##### Summary

Choctaw

<a name='F-VRDR-ValueSets-RaceCode-Choctaw_Apache_Community_Of_Ebarb'></a>
### Choctaw_Apache_Community_Of_Ebarb `constants`

##### Summary

Choctaw_Apache_Community_Of_Ebarb

<a name='F-VRDR-ValueSets-RaceCode-Chugach_Aleut'></a>
### Chugach_Aleut `constants`

##### Summary

Chugach_Aleut

<a name='F-VRDR-ValueSets-RaceCode-Chugach_Corporation'></a>
### Chugach_Corporation `constants`

##### Summary

Chugach_Corporation

<a name='F-VRDR-ValueSets-RaceCode-Chuloonawick_Native_Village'></a>
### Chuloonawick_Native_Village `constants`

##### Summary

Chuloonawick_Native_Village

<a name='F-VRDR-ValueSets-RaceCode-Chumash'></a>
### Chumash `constants`

##### Summary

Chumash

<a name='F-VRDR-ValueSets-RaceCode-Chuukese'></a>
### Chuukese `constants`

##### Summary

Chuukese

<a name='F-VRDR-ValueSets-RaceCode-Circle_Native_Community'></a>
### Circle_Native_Community `constants`

##### Summary

Circle_Native_Community

<a name='F-VRDR-ValueSets-RaceCode-Citizen_Potawatomi_Nation'></a>
### Citizen_Potawatomi_Nation `constants`

##### Summary

Citizen_Potawatomi_Nation

<a name='F-VRDR-ValueSets-RaceCode-Clatsop'></a>
### Clatsop `constants`

##### Summary

Clatsop

<a name='F-VRDR-ValueSets-RaceCode-Clear_Lake'></a>
### Clear_Lake `constants`

##### Summary

Clear_Lake

<a name='F-VRDR-ValueSets-RaceCode-Clifton_Choctaw'></a>
### Clifton_Choctaw `constants`

##### Summary

Clifton_Choctaw

<a name='F-VRDR-ValueSets-RaceCode-Cloverdale_Rancheria'></a>
### Cloverdale_Rancheria `constants`

##### Summary

Cloverdale_Rancheria

<a name='F-VRDR-ValueSets-RaceCode-Coast_Miwok'></a>
### Coast_Miwok `constants`

##### Summary

Coast_Miwok

<a name='F-VRDR-ValueSets-RaceCode-Coast_Yurok'></a>
### Coast_Yurok `constants`

##### Summary

Coast_Yurok

<a name='F-VRDR-ValueSets-RaceCode-Cochiti'></a>
### Cochiti `constants`

##### Summary

Cochiti

<a name='F-VRDR-ValueSets-RaceCode-Cocopah_Tribe_Of_Arizona'></a>
### Cocopah_Tribe_Of_Arizona `constants`

##### Summary

Cocopah_Tribe_Of_Arizona

<a name='F-VRDR-ValueSets-RaceCode-Codes'></a>
### Codes `constants`

##### Summary

Codes

<a name='F-VRDR-ValueSets-RaceCode-Coe_Clan'></a>
### Coe_Clan `constants`

##### Summary

Coe_Clan

<a name='F-VRDR-ValueSets-RaceCode-Coeur_Dalene'></a>
### Coeur_Dalene `constants`

##### Summary

Coeur_Dalene

<a name='F-VRDR-ValueSets-RaceCode-Coffee'></a>
### Coffee `constants`

##### Summary

Coffee

<a name='F-VRDR-ValueSets-RaceCode-Coharie'></a>
### Coharie `constants`

##### Summary

Coharie

<a name='F-VRDR-ValueSets-RaceCode-Cold_Springs_Rancheria'></a>
### Cold_Springs_Rancheria `constants`

##### Summary

Cold_Springs_Rancheria

<a name='F-VRDR-ValueSets-RaceCode-Colombian'></a>
### Colombian `constants`

##### Summary

Colombian

<a name='F-VRDR-ValueSets-RaceCode-Colorado_River'></a>
### Colorado_River `constants`

##### Summary

Colorado_River

<a name='F-VRDR-ValueSets-RaceCode-Columbia'></a>
### Columbia `constants`

##### Summary

Columbia

<a name='F-VRDR-ValueSets-RaceCode-Columbia_River_Chinook'></a>
### Columbia_River_Chinook `constants`

##### Summary

Columbia_River_Chinook

<a name='F-VRDR-ValueSets-RaceCode-Colville'></a>
### Colville `constants`

##### Summary

Colville

<a name='F-VRDR-ValueSets-RaceCode-Comanche'></a>
### Comanche `constants`

##### Summary

Comanche

<a name='F-VRDR-ValueSets-RaceCode-Confederated_Tribes_Of_The_Siletz_Reservation'></a>
### Confederated_Tribes_Of_The_Siletz_Reservation `constants`

##### Summary

Confederated_Tribes_Of_The_Siletz_Reservation

<a name='F-VRDR-ValueSets-RaceCode-Cook_Inlet'></a>
### Cook_Inlet `constants`

##### Summary

Cook_Inlet

<a name='F-VRDR-ValueSets-RaceCode-Coos'></a>
### Coos `constants`

##### Summary

Coos

<a name='F-VRDR-ValueSets-RaceCode-Coos_Lower_Umpqua_And_Siuslaw'></a>
### Coos_Lower_Umpqua_And_Siuslaw `constants`

##### Summary

Coos_Lower_Umpqua_And_Siuslaw

<a name='F-VRDR-ValueSets-RaceCode-Copper_Center'></a>
### Copper_Center `constants`

##### Summary

Copper_Center

<a name='F-VRDR-ValueSets-RaceCode-Copper_River'></a>
### Copper_River `constants`

##### Summary

Copper_River

<a name='F-VRDR-ValueSets-RaceCode-Coquille'></a>
### Coquille `constants`

##### Summary

Coquille

<a name='F-VRDR-ValueSets-RaceCode-Cortina_Indian_Rancheria_Of_Wintun_Indians'></a>
### Cortina_Indian_Rancheria_Of_Wintun_Indians `constants`

##### Summary

Cortina_Indian_Rancheria_Of_Wintun_Indians

<a name='F-VRDR-ValueSets-RaceCode-Cosmopolitan'></a>
### Cosmopolitan `constants`

##### Summary

Cosmopolitan

<a name='F-VRDR-ValueSets-RaceCode-Costa_Rican'></a>
### Costa_Rican `constants`

##### Summary

Costa_Rican

<a name='F-VRDR-ValueSets-RaceCode-Costanoan'></a>
### Costanoan `constants`

##### Summary

Costanoan

<a name='F-VRDR-ValueSets-RaceCode-Coushatta'></a>
### Coushatta `constants`

##### Summary

Coushatta

<a name='F-VRDR-ValueSets-RaceCode-Cow_Creek_Umpqua'></a>
### Cow_Creek_Umpqua `constants`

##### Summary

Cow_Creek_Umpqua

<a name='F-VRDR-ValueSets-RaceCode-Cowlitz'></a>
### Cowlitz `constants`

##### Summary

Cowlitz

<a name='F-VRDR-ValueSets-RaceCode-Coyote_Valley_Band'></a>
### Coyote_Valley_Band `constants`

##### Summary

Coyote_Valley_Band

<a name='F-VRDR-ValueSets-RaceCode-Craig_Community_Association'></a>
### Craig_Community_Association `constants`

##### Summary

Craig_Community_Association

<a name='F-VRDR-ValueSets-RaceCode-Cree'></a>
### Cree `constants`

##### Summary

Cree

<a name='F-VRDR-ValueSets-RaceCode-Creole'></a>
### Creole `constants`

##### Summary

Creole

<a name='F-VRDR-ValueSets-RaceCode-Croatan'></a>
### Croatan `constants`

##### Summary

Croatan

<a name='F-VRDR-ValueSets-RaceCode-Croatian'></a>
### Croatian `constants`

##### Summary

Croatian

<a name='F-VRDR-ValueSets-RaceCode-Crow'></a>
### Crow `constants`

##### Summary

Crow

<a name='F-VRDR-ValueSets-RaceCode-Crow_Creek_Sioux'></a>
### Crow_Creek_Sioux `constants`

##### Summary

Crow_Creek_Sioux

<a name='F-VRDR-ValueSets-RaceCode-Cuban'></a>
### Cuban `constants`

##### Summary

Cuban

<a name='F-VRDR-ValueSets-RaceCode-Cumberland_County_Association_For_Indian_People'></a>
### Cumberland_County_Association_For_Indian_People `constants`

##### Summary

Cumberland_County_Association_For_Indian_People

<a name='F-VRDR-ValueSets-RaceCode-Cupeno'></a>
### Cupeno `constants`

##### Summary

Cupeno

<a name='F-VRDR-ValueSets-RaceCode-Curyung_Tribal_Council'></a>
### Curyung_Tribal_Council `constants`

##### Summary

Curyung_Tribal_Council

<a name='F-VRDR-ValueSets-RaceCode-Cuyapaipe'></a>
### Cuyapaipe `constants`

##### Summary

Cuyapaipe

<a name='F-VRDR-ValueSets-RaceCode-Czech'></a>
### Czech `constants`

##### Summary

Czech

<a name='F-VRDR-ValueSets-RaceCode-Czechoslovakian'></a>
### Czechoslovakian `constants`

##### Summary

Czechoslovakian

<a name='F-VRDR-ValueSets-RaceCode-Dakota_Sioux'></a>
### Dakota_Sioux `constants`

##### Summary

Dakota_Sioux

<a name='F-VRDR-ValueSets-RaceCode-Dania_Seminole'></a>
### Dania_Seminole `constants`

##### Summary

Dania_Seminole

<a name='F-VRDR-ValueSets-RaceCode-Death_Valley_Timbisha_Shoshone'></a>
### Death_Valley_Timbisha_Shoshone `constants`

##### Summary

Death_Valley_Timbisha_Shoshone

<a name='F-VRDR-ValueSets-RaceCode-Deferred'></a>
### Deferred `constants`

##### Summary

Deferred

<a name='F-VRDR-ValueSets-RaceCode-Delaware'></a>
### Delaware `constants`

##### Summary

Delaware

<a name='F-VRDR-ValueSets-RaceCode-Delaware_Tribe_Of_Indians_Oklahoma'></a>
### Delaware_Tribe_Of_Indians_Oklahoma `constants`

##### Summary

Delaware_Tribe_Of_Indians_Oklahoma

<a name='F-VRDR-ValueSets-RaceCode-Delaware_Tribe_Of_Western_Oklahoma'></a>
### Delaware_Tribe_Of_Western_Oklahoma `constants`

##### Summary

Delaware_Tribe_Of_Western_Oklahoma

<a name='F-VRDR-ValueSets-RaceCode-Diegueno'></a>
### Diegueno `constants`

##### Summary

Diegueno

<a name='F-VRDR-ValueSets-RaceCode-Digger'></a>
### Digger `constants`

##### Summary

Digger

<a name='F-VRDR-ValueSets-RaceCode-Dominica_Islander'></a>
### Dominica_Islander `constants`

##### Summary

Dominica_Islander

<a name='F-VRDR-ValueSets-RaceCode-Dominican_Republic'></a>
### Dominican_Republic `constants`

##### Summary

Dominican_Republic

<a name='F-VRDR-ValueSets-RaceCode-Douglas_Indian_Association'></a>
### Douglas_Indian_Association `constants`

##### Summary

Douglas_Indian_Association

<a name='F-VRDR-ValueSets-RaceCode-Doyon'></a>
### Doyon `constants`

##### Summary

Doyon

<a name='F-VRDR-ValueSets-RaceCode-Dresslerville_Colony'></a>
### Dresslerville_Colony `constants`

##### Summary

Dresslerville_Colony

<a name='F-VRDR-ValueSets-RaceCode-Dry_Creek'></a>
### Dry_Creek `constants`

##### Summary

Dry_Creek

<a name='F-VRDR-ValueSets-RaceCode-Duck_Valley'></a>
### Duck_Valley `constants`

##### Summary

Duck_Valley

<a name='F-VRDR-ValueSets-RaceCode-Duckwater'></a>
### Duckwater `constants`

##### Summary

Duckwater

<a name='F-VRDR-ValueSets-RaceCode-Duwamish'></a>
### Duwamish `constants`

##### Summary

Duwamish

<a name='F-VRDR-ValueSets-RaceCode-Eastern_Creek'></a>
### Eastern_Creek `constants`

##### Summary

Eastern_Creek

<a name='F-VRDR-ValueSets-RaceCode-Eastern_Muscogee'></a>
### Eastern_Muscogee `constants`

##### Summary

Eastern_Muscogee

<a name='F-VRDR-ValueSets-RaceCode-Eastern_Pomo'></a>
### Eastern_Pomo `constants`

##### Summary

Eastern_Pomo

<a name='F-VRDR-ValueSets-RaceCode-Eastern_Shawnee'></a>
### Eastern_Shawnee `constants`

##### Summary

Eastern_Shawnee

<a name='F-VRDR-ValueSets-RaceCode-Echota_Cherokee'></a>
### Echota_Cherokee `constants`

##### Summary

Echota_Cherokee

<a name='F-VRDR-ValueSets-RaceCode-Ecuadorian'></a>
### Ecuadorian `constants`

##### Summary

Ecuadorian

<a name='F-VRDR-ValueSets-RaceCode-Egegik_Village'></a>
### Egegik_Village `constants`

##### Summary

Egegik_Village

<a name='F-VRDR-ValueSets-RaceCode-Egyptian'></a>
### Egyptian `constants`

##### Summary

Egyptian

<a name='F-VRDR-ValueSets-RaceCode-Ekiutna_Native_Village'></a>
### Ekiutna_Native_Village `constants`

##### Summary

Ekiutna_Native_Village

<a name='F-VRDR-ValueSets-RaceCode-Ekwok_Village'></a>
### Ekwok_Village `constants`

##### Summary

Ekwok_Village

<a name='F-VRDR-ValueSets-RaceCode-Elem_Indian_Colony_Of_The_Sulphur_Bank'></a>
### Elem_Indian_Colony_Of_The_Sulphur_Bank `constants`

##### Summary

Elem_Indian_Colony_Of_The_Sulphur_Bank

<a name='F-VRDR-ValueSets-RaceCode-Elk_Valley_Rancheria'></a>
### Elk_Valley_Rancheria `constants`

##### Summary

Elk_Valley_Rancheria

<a name='F-VRDR-ValueSets-RaceCode-Elko'></a>
### Elko `constants`

##### Summary

Elko

<a name='F-VRDR-ValueSets-RaceCode-Ely'></a>
### Ely `constants`

##### Summary

Ely

<a name='F-VRDR-ValueSets-RaceCode-Emmonak_Village'></a>
### Emmonak_Village `constants`

##### Summary

Emmonak_Village

<a name='F-VRDR-ValueSets-RaceCode-English'></a>
### English `constants`

##### Summary

English

<a name='F-VRDR-ValueSets-RaceCode-Enterprise_Rancheria'></a>
### Enterprise_Rancheria `constants`

##### Summary

Enterprise_Rancheria

<a name='F-VRDR-ValueSets-RaceCode-Eritrean'></a>
### Eritrean `constants`

##### Summary

Eritrean

<a name='F-VRDR-ValueSets-RaceCode-Eskimo'></a>
### Eskimo `constants`

##### Summary

Eskimo

<a name='F-VRDR-ValueSets-RaceCode-Esselen'></a>
### Esselen `constants`

##### Summary

Esselen

<a name='F-VRDR-ValueSets-RaceCode-Ethiopian'></a>
### Ethiopian `constants`

##### Summary

Ethiopian

<a name='F-VRDR-ValueSets-RaceCode-Etowah_Cherokee'></a>
### Etowah_Cherokee `constants`

##### Summary

Etowah_Cherokee

<a name='F-VRDR-ValueSets-RaceCode-Eurasian'></a>
### Eurasian `constants`

##### Summary

Eurasian

<a name='F-VRDR-ValueSets-RaceCode-European'></a>
### European `constants`

##### Summary

European

<a name='F-VRDR-ValueSets-RaceCode-Evansville_Village'></a>
### Evansville_Village `constants`

##### Summary

Evansville_Village

<a name='F-VRDR-ValueSets-RaceCode-Eyak'></a>
### Eyak `constants`

##### Summary

Eyak

<a name='F-VRDR-ValueSets-RaceCode-Fallen'></a>
### Fallen `constants`

##### Summary

Fallen

<a name='F-VRDR-ValueSets-RaceCode-Fijian'></a>
### Fijian `constants`

##### Summary

Fijian

<a name='F-VRDR-ValueSets-RaceCode-Filipino'></a>
### Filipino `constants`

##### Summary

Filipino

<a name='F-VRDR-ValueSets-RaceCode-First_Pass_Reject'></a>
### First_Pass_Reject `constants`

##### Summary

First_Pass_Reject

<a name='F-VRDR-ValueSets-RaceCode-Flandreau_Santee_Sioux'></a>
### Flandreau_Santee_Sioux `constants`

##### Summary

Flandreau_Santee_Sioux

<a name='F-VRDR-ValueSets-RaceCode-Florida_Seminole'></a>
### Florida_Seminole `constants`

##### Summary

Florida_Seminole

<a name='F-VRDR-ValueSets-RaceCode-Fond_Du_Lac'></a>
### Fond_Du_Lac `constants`

##### Summary

Fond_Du_Lac

<a name='F-VRDR-ValueSets-RaceCode-Forest_County'></a>
### Forest_County `constants`

##### Summary

Forest_County

<a name='F-VRDR-ValueSets-RaceCode-Fort_Belknap'></a>
### Fort_Belknap `constants`

##### Summary

Fort_Belknap

<a name='F-VRDR-ValueSets-RaceCode-Fort_Belknap_Assiniboine'></a>
### Fort_Belknap_Assiniboine `constants`

##### Summary

Fort_Belknap_Assiniboine

<a name='F-VRDR-ValueSets-RaceCode-Fort_Belknap_Gros_Ventres'></a>
### Fort_Belknap_Gros_Ventres `constants`

##### Summary

Fort_Belknap_Gros_Ventres

<a name='F-VRDR-ValueSets-RaceCode-Fort_Berthold'></a>
### Fort_Berthold `constants`

##### Summary

Fort_Berthold

<a name='F-VRDR-ValueSets-RaceCode-Fort_Bidwell'></a>
### Fort_Bidwell `constants`

##### Summary

Fort_Bidwell

<a name='F-VRDR-ValueSets-RaceCode-Fort_Independence'></a>
### Fort_Independence `constants`

##### Summary

Fort_Independence

<a name='F-VRDR-ValueSets-RaceCode-Fort_Mcdermitt_Paiute_And_Shoshone_Tribes'></a>
### Fort_Mcdermitt_Paiute_And_Shoshone_Tribes `constants`

##### Summary

Fort_Mcdermitt_Paiute_And_Shoshone_Tribes

<a name='F-VRDR-ValueSets-RaceCode-Fort_Mcdowell_Mohaveapache_Community'></a>
### Fort_Mcdowell_Mohaveapache_Community `constants`

##### Summary

Fort_Mcdowell_Mohaveapache_Community

<a name='F-VRDR-ValueSets-RaceCode-Fort_Mojave_Indian_Tribe_Of_Arizona'></a>
### Fort_Mojave_Indian_Tribe_Of_Arizona `constants`

##### Summary

Fort_Mojave_Indian_Tribe_Of_Arizona

<a name='F-VRDR-ValueSets-RaceCode-Fort_Peck_Assiniboine'></a>
### Fort_Peck_Assiniboine `constants`

##### Summary

Fort_Peck_Assiniboine

<a name='F-VRDR-ValueSets-RaceCode-Fort_Peck_Assiniboine_And_Sioux'></a>
### Fort_Peck_Assiniboine_And_Sioux `constants`

##### Summary

Fort_Peck_Assiniboine_And_Sioux

<a name='F-VRDR-ValueSets-RaceCode-Fort_Peck_Sioux'></a>
### Fort_Peck_Sioux `constants`

##### Summary

Fort_Peck_Sioux

<a name='F-VRDR-ValueSets-RaceCode-Fort_Sill_Apache'></a>
### Fort_Sill_Apache `constants`

##### Summary

Fort_Sill_Apache

<a name='F-VRDR-ValueSets-RaceCode-Four_Winds_Cherokee'></a>
### Four_Winds_Cherokee `constants`

##### Summary

Four_Winds_Cherokee

<a name='F-VRDR-ValueSets-RaceCode-French'></a>
### French `constants`

##### Summary

French

<a name='F-VRDR-ValueSets-RaceCode-French_American_Indian'></a>
### French_American_Indian `constants`

##### Summary

French_American_Indian

<a name='F-VRDR-ValueSets-RaceCode-French_Canadian'></a>
### French_Canadian `constants`

##### Summary

French_Canadian

<a name='F-VRDR-ValueSets-RaceCode-Gabrieleno'></a>
### Gabrieleno `constants`

##### Summary

Gabrieleno

<a name='F-VRDR-ValueSets-RaceCode-Galena_Village'></a>
### Galena_Village `constants`

##### Summary

Galena_Village

<a name='F-VRDR-ValueSets-RaceCode-Gay_Head_Wampanoag'></a>
### Gay_Head_Wampanoag `constants`

##### Summary

Gay_Head_Wampanoag

<a name='F-VRDR-ValueSets-RaceCode-Georgetown'></a>
### Georgetown `constants`

##### Summary

Georgetown

<a name='F-VRDR-ValueSets-RaceCode-Georgia_Eastern_Cherokee'></a>
### Georgia_Eastern_Cherokee `constants`

##### Summary

Georgia_Eastern_Cherokee

<a name='F-VRDR-ValueSets-RaceCode-German'></a>
### German `constants`

##### Summary

German

<a name='F-VRDR-ValueSets-RaceCode-Gila_Bend'></a>
### Gila_Bend `constants`

##### Summary

Gila_Bend

<a name='F-VRDR-ValueSets-RaceCode-Gila_River_Indian_Community'></a>
### Gila_River_Indian_Community `constants`

##### Summary

Gila_River_Indian_Community

<a name='F-VRDR-ValueSets-RaceCode-Golden_Hill_Paugussett'></a>
### Golden_Hill_Paugussett `constants`

##### Summary

Golden_Hill_Paugussett

<a name='F-VRDR-ValueSets-RaceCode-Golovin'></a>
### Golovin `constants`

##### Summary

Golovin

<a name='F-VRDR-ValueSets-RaceCode-Goshute'></a>
### Goshute `constants`

##### Summary

Goshute

<a name='F-VRDR-ValueSets-RaceCode-Grand_Portage'></a>
### Grand_Portage `constants`

##### Summary

Grand_Portage

<a name='F-VRDR-ValueSets-RaceCode-Grand_River_Band_Of_Ottawa_Indians'></a>
### Grand_River_Band_Of_Ottawa_Indians `constants`

##### Summary

Grand_River_Band_Of_Ottawa_Indians

<a name='F-VRDR-ValueSets-RaceCode-Grand_Ronde'></a>
### Grand_Ronde `constants`

##### Summary

Grand_Ronde

<a name='F-VRDR-ValueSets-RaceCode-Grand_Traverse_Band_Of_Ottawa_And_Chippewa_Indians'></a>
### Grand_Traverse_Band_Of_Ottawa_And_Chippewa_Indians `constants`

##### Summary

Grand_Traverse_Band_Of_Ottawa_And_Chippewa_Indians

<a name='F-VRDR-ValueSets-RaceCode-Greenland_Eskimo'></a>
### Greenland_Eskimo `constants`

##### Summary

Greenland_Eskimo

<a name='F-VRDR-ValueSets-RaceCode-Greenville_Rancheria'></a>
### Greenville_Rancheria `constants`

##### Summary

Greenville_Rancheria

<a name='F-VRDR-ValueSets-RaceCode-Grindstone_Indian_Rancheria_Of_Wintunwailaki_Indians'></a>
### Grindstone_Indian_Rancheria_Of_Wintunwailaki_Indians `constants`

##### Summary

Grindstone_Indian_Rancheria_Of_Wintunwailaki_Indians

<a name='F-VRDR-ValueSets-RaceCode-Gros_Ventres'></a>
### Gros_Ventres `constants`

##### Summary

Gros_Ventres

<a name='F-VRDR-ValueSets-RaceCode-Guamanian'></a>
### Guamanian `constants`

##### Summary

Guamanian

<a name='F-VRDR-ValueSets-RaceCode-Guatemalan'></a>
### Guatemalan `constants`

##### Summary

Guatemalan

<a name='F-VRDR-ValueSets-RaceCode-Guidiville_Rancheria_Of_California'></a>
### Guidiville_Rancheria_Of_California `constants`

##### Summary

Guidiville_Rancheria_Of_California

<a name='F-VRDR-ValueSets-RaceCode-Guilford_Native_American_Association'></a>
### Guilford_Native_American_Association `constants`

##### Summary

Guilford_Native_American_Association

<a name='F-VRDR-ValueSets-RaceCode-Gulkana_Village'></a>
### Gulkana_Village `constants`

##### Summary

Gulkana_Village

<a name='F-VRDR-ValueSets-RaceCode-Guyanese'></a>
### Guyanese `constants`

##### Summary

Guyanese

<a name='F-VRDR-ValueSets-RaceCode-Haida'></a>
### Haida `constants`

##### Summary

Haida

<a name='F-VRDR-ValueSets-RaceCode-Haitian'></a>
### Haitian `constants`

##### Summary

Haitian

<a name='F-VRDR-ValueSets-RaceCode-Half_Breed'></a>
### Half_Breed `constants`

##### Summary

Half_Breed

<a name='F-VRDR-ValueSets-RaceCode-Haliwasaponi'></a>
### Haliwasaponi `constants`

##### Summary

Haliwasaponi

<a name='F-VRDR-ValueSets-RaceCode-Hannahville_Indian_Community_Of_Wisconsin_Potawatomi'></a>
### Hannahville_Indian_Community_Of_Wisconsin_Potawatomi `constants`

##### Summary

Hannahville_Indian_Community_Of_Wisconsin_Potawatomi

<a name='F-VRDR-ValueSets-RaceCode-Hassanamisco_Band_Of_The_Nipmuc_Nation'></a>
### Hassanamisco_Band_Of_The_Nipmuc_Nation `constants`

##### Summary

Hassanamisco_Band_Of_The_Nipmuc_Nation

<a name='F-VRDR-ValueSets-RaceCode-Havasupai'></a>
### Havasupai `constants`

##### Summary

Havasupai

<a name='F-VRDR-ValueSets-RaceCode-Hawaiian'></a>
### Hawaiian `constants`

##### Summary

Hawaiian

<a name='F-VRDR-ValueSets-RaceCode-Healy_Lake_Village'></a>
### Healy_Lake_Village `constants`

##### Summary

Healy_Lake_Village

<a name='F-VRDR-ValueSets-RaceCode-Hidatsa'></a>
### Hidatsa `constants`

##### Summary

Hidatsa

<a name='F-VRDR-ValueSets-RaceCode-Hispanic'></a>
### Hispanic `constants`

##### Summary

Hispanic

<a name='F-VRDR-ValueSets-RaceCode-Hmong'></a>
### Hmong `constants`

##### Summary

Hmong

<a name='F-VRDR-ValueSets-RaceCode-Hochunk_Nation_Of_Wisconsin'></a>
### Hochunk_Nation_Of_Wisconsin `constants`

##### Summary

Hochunk_Nation_Of_Wisconsin

<a name='F-VRDR-ValueSets-RaceCode-Hoh_Indian_Tribe'></a>
### Hoh_Indian_Tribe `constants`

##### Summary

Hoh_Indian_Tribe

<a name='F-VRDR-ValueSets-RaceCode-Hollywood_Seminole'></a>
### Hollywood_Seminole `constants`

##### Summary

Hollywood_Seminole

<a name='F-VRDR-ValueSets-RaceCode-Holy_Cross_Village'></a>
### Holy_Cross_Village `constants`

##### Summary

Holy_Cross_Village

<a name='F-VRDR-ValueSets-RaceCode-Honduran'></a>
### Honduran `constants`

##### Summary

Honduran

<a name='F-VRDR-ValueSets-RaceCode-Hoonah_Indian_Association'></a>
### Hoonah_Indian_Association `constants`

##### Summary

Hoonah_Indian_Association

<a name='F-VRDR-ValueSets-RaceCode-Hoopa_Extension'></a>
### Hoopa_Extension `constants`

##### Summary

Hoopa_Extension

<a name='F-VRDR-ValueSets-RaceCode-Hoopa_Valley_Tribe'></a>
### Hoopa_Valley_Tribe `constants`

##### Summary

Hoopa_Valley_Tribe

<a name='F-VRDR-ValueSets-RaceCode-Hopi'></a>
### Hopi `constants`

##### Summary

Hopi

<a name='F-VRDR-ValueSets-RaceCode-Hopland_Band_Of_Pomo_Indians'></a>
### Hopland_Band_Of_Pomo_Indians `constants`

##### Summary

Hopland_Band_Of_Pomo_Indians

<a name='F-VRDR-ValueSets-RaceCode-Houlton_Band_Of_Maliseet_Indians'></a>
### Houlton_Band_Of_Maliseet_Indians `constants`

##### Summary

Houlton_Band_Of_Maliseet_Indians

<a name='F-VRDR-ValueSets-RaceCode-Hualapai'></a>
### Hualapai `constants`

##### Summary

Hualapai

<a name='F-VRDR-ValueSets-RaceCode-Hughes_Village'></a>
### Hughes_Village `constants`

##### Summary

Hughes_Village

<a name='F-VRDR-ValueSets-RaceCode-Huron_Potawatomi'></a>
### Huron_Potawatomi `constants`

##### Summary

Huron_Potawatomi

<a name='F-VRDR-ValueSets-RaceCode-Huslia_Village'></a>
### Huslia_Village `constants`

##### Summary

Huslia_Village

<a name='F-VRDR-ValueSets-RaceCode-Hydaburg_Cooperative_Association'></a>
### Hydaburg_Cooperative_Association `constants`

##### Summary

Hydaburg_Cooperative_Association

<a name='F-VRDR-ValueSets-RaceCode-Iberian'></a>
### Iberian `constants`

##### Summary

Iberian

<a name='F-VRDR-ValueSets-RaceCode-Igiugig_Village'></a>
### Igiugig_Village `constants`

##### Summary

Igiugig_Village

<a name='F-VRDR-ValueSets-RaceCode-Illinois_Miami'></a>
### Illinois_Miami `constants`

##### Summary

Illinois_Miami

<a name='F-VRDR-ValueSets-RaceCode-Inaja_Band_Of_Diegueno_Mission_Indians_Of_The_Inaja_And_Cosmit_Reservation'></a>
### Inaja_Band_Of_Diegueno_Mission_Indians_Of_The_Inaja_And_Cosmit_Reservation `constants`

##### Summary

Inaja_Band_Of_Diegueno_Mission_Indians_Of_The_Inaja_And_Cosmit_Reservation

<a name='F-VRDR-ValueSets-RaceCode-Indian'></a>
### Indian `constants`

##### Summary

Indian

<a name='F-VRDR-ValueSets-RaceCode-Indian_Township'></a>
### Indian_Township `constants`

##### Summary

Indian_Township

<a name='F-VRDR-ValueSets-RaceCode-Indiana_Miami'></a>
### Indiana_Miami `constants`

##### Summary

Indiana_Miami

<a name='F-VRDR-ValueSets-RaceCode-Indians_Of_Person_County'></a>
### Indians_Of_Person_County `constants`

##### Summary

Indians_Of_Person_County

<a name='F-VRDR-ValueSets-RaceCode-Indo_Chinese'></a>
### Indo_Chinese `constants`

##### Summary

Indo_Chinese

<a name='F-VRDR-ValueSets-RaceCode-Indonesian'></a>
### Indonesian `constants`

##### Summary

Indonesian

<a name='F-VRDR-ValueSets-RaceCode-Interracial'></a>
### Interracial `constants`

##### Summary

Interracial

<a name='F-VRDR-ValueSets-RaceCode-Inuit'></a>
### Inuit `constants`

##### Summary

Inuit

<a name='F-VRDR-ValueSets-RaceCode-Inupiaq'></a>
### Inupiaq `constants`

##### Summary

Inupiaq

<a name='F-VRDR-ValueSets-RaceCode-Inupiat'></a>
### Inupiat `constants`

##### Summary

Inupiat

<a name='F-VRDR-ValueSets-RaceCode-Inupiat_Community_Of_The_Arctic_Slope'></a>
### Inupiat_Community_Of_The_Arctic_Slope `constants`

##### Summary

Inupiat_Community_Of_The_Arctic_Slope

<a name='F-VRDR-ValueSets-RaceCode-Inupiat_Eskimo'></a>
### Inupiat_Eskimo `constants`

##### Summary

Inupiat_Eskimo

<a name='F-VRDR-ValueSets-RaceCode-Ione_Band_Of_Miwok_Indians'></a>
### Ione_Band_Of_Miwok_Indians `constants`

##### Summary

Ione_Band_Of_Miwok_Indians

<a name='F-VRDR-ValueSets-RaceCode-Iowa'></a>
### Iowa `constants`

##### Summary

Iowa

<a name='F-VRDR-ValueSets-RaceCode-Iowa_Of_Kansas_And_Nebraska'></a>
### Iowa_Of_Kansas_And_Nebraska `constants`

##### Summary

Iowa_Of_Kansas_And_Nebraska

<a name='F-VRDR-ValueSets-RaceCode-Iowa_Of_Oklahoma'></a>
### Iowa_Of_Oklahoma `constants`

##### Summary

Iowa_Of_Oklahoma

<a name='F-VRDR-ValueSets-RaceCode-Iqurmuit_Traditional_Council'></a>
### Iqurmuit_Traditional_Council `constants`

##### Summary

Iqurmuit_Traditional_Council

<a name='F-VRDR-ValueSets-RaceCode-Iranian'></a>
### Iranian `constants`

##### Summary

Iranian

<a name='F-VRDR-ValueSets-RaceCode-Iraqi'></a>
### Iraqi `constants`

##### Summary

Iraqi

<a name='F-VRDR-ValueSets-RaceCode-Irish'></a>
### Irish `constants`

##### Summary

Irish

<a name='F-VRDR-ValueSets-RaceCode-Iroquois'></a>
### Iroquois `constants`

##### Summary

Iroquois

<a name='F-VRDR-ValueSets-RaceCode-Isleta'></a>
### Isleta `constants`

##### Summary

Isleta

<a name='F-VRDR-ValueSets-RaceCode-Israeli'></a>
### Israeli `constants`

##### Summary

Israeli

<a name='F-VRDR-ValueSets-RaceCode-Issues'></a>
### Issues `constants`

##### Summary

Issues

<a name='F-VRDR-ValueSets-RaceCode-Italian'></a>
### Italian `constants`

##### Summary

Italian

<a name='F-VRDR-ValueSets-RaceCode-Ivanoff_Bay_Village'></a>
### Ivanoff_Bay_Village `constants`

##### Summary

Ivanoff_Bay_Village

<a name='F-VRDR-ValueSets-RaceCode-Iwo_Jiman'></a>
### Iwo_Jiman `constants`

##### Summary

Iwo_Jiman

<a name='F-VRDR-ValueSets-RaceCode-Jackson_Rancheria_Of_Mewuk_Indians_Of_California'></a>
### Jackson_Rancheria_Of_Mewuk_Indians_Of_California `constants`

##### Summary

Jackson_Rancheria_Of_Mewuk_Indians_Of_California

<a name='F-VRDR-ValueSets-RaceCode-Jackson_White'></a>
### Jackson_White `constants`

##### Summary

Jackson_White

<a name='F-VRDR-ValueSets-RaceCode-Jamaican'></a>
### Jamaican `constants`

##### Summary

Jamaican

<a name='F-VRDR-ValueSets-RaceCode-Jamestown_Sklallam'></a>
### Jamestown_Sklallam `constants`

##### Summary

Jamestown_Sklallam

<a name='F-VRDR-ValueSets-RaceCode-Japanese'></a>
### Japanese `constants`

##### Summary

Japanese

<a name='F-VRDR-ValueSets-RaceCode-Jarnul_Indian_Village'></a>
### Jarnul_Indian_Village `constants`

##### Summary

Jarnul_Indian_Village

<a name='F-VRDR-ValueSets-RaceCode-Jemez'></a>
### Jemez `constants`

##### Summary

Jemez

<a name='F-VRDR-ValueSets-RaceCode-Jena_Band_Of_Choctaw'></a>
### Jena_Band_Of_Choctaw `constants`

##### Summary

Jena_Band_Of_Choctaw

<a name='F-VRDR-ValueSets-RaceCode-Jewish'></a>
### Jewish `constants`

##### Summary

Jewish

<a name='F-VRDR-ValueSets-RaceCode-Jicarilla_Apache'></a>
### Jicarilla_Apache `constants`

##### Summary

Jicarilla_Apache

<a name='F-VRDR-ValueSets-RaceCode-Juaneno'></a>
### Juaneno `constants`

##### Summary

Juaneno

<a name='F-VRDR-ValueSets-RaceCode-Juneau'></a>
### Juneau `constants`

##### Summary

Juneau

<a name='F-VRDR-ValueSets-RaceCode-Kaguyak_Village'></a>
### Kaguyak_Village `constants`

##### Summary

Kaguyak_Village

<a name='F-VRDR-ValueSets-RaceCode-Kaibab_Band_Of_Paiute_Indians'></a>
### Kaibab_Band_Of_Paiute_Indians `constants`

##### Summary

Kaibab_Band_Of_Paiute_Indians

<a name='F-VRDR-ValueSets-RaceCode-Kaktovik_Village'></a>
### Kaktovik_Village `constants`

##### Summary

Kaktovik_Village

<a name='F-VRDR-ValueSets-RaceCode-Kalapuya'></a>
### Kalapuya `constants`

##### Summary

Kalapuya

<a name='F-VRDR-ValueSets-RaceCode-Kalispel_Indian_Community'></a>
### Kalispel_Indian_Community `constants`

##### Summary

Kalispel_Indian_Community

<a name='F-VRDR-ValueSets-RaceCode-Karuk_Tribe_Of_California'></a>
### Karuk_Tribe_Of_California `constants`

##### Summary

Karuk_Tribe_Of_California

<a name='F-VRDR-ValueSets-RaceCode-Kashia_Band_Of_Pomo_Indians_Of_The_Stewarts_Point_Rancheria'></a>
### Kashia_Band_Of_Pomo_Indians_Of_The_Stewarts_Point_Rancheria `constants`

##### Summary

Kashia_Band_Of_Pomo_Indians_Of_The_Stewarts_Point_Rancheria

<a name='F-VRDR-ValueSets-RaceCode-Kathlamet'></a>
### Kathlamet `constants`

##### Summary

Kathlamet

<a name='F-VRDR-ValueSets-RaceCode-Kaw'></a>
### Kaw `constants`

##### Summary

Kaw

<a name='F-VRDR-ValueSets-RaceCode-Kawaiisu'></a>
### Kawaiisu `constants`

##### Summary

Kawaiisu

<a name='F-VRDR-ValueSets-RaceCode-Kawerak'></a>
### Kawerak `constants`

##### Summary

Kawerak

<a name='F-VRDR-ValueSets-RaceCode-Keechi'></a>
### Keechi `constants`

##### Summary

Keechi

<a name='F-VRDR-ValueSets-RaceCode-Kem_River_Paiute_Council'></a>
### Kem_River_Paiute_Council `constants`

##### Summary

Kem_River_Paiute_Council

<a name='F-VRDR-ValueSets-RaceCode-Kenaitze_Indian_Tribe'></a>
### Kenaitze_Indian_Tribe `constants`

##### Summary

Kenaitze_Indian_Tribe

<a name='F-VRDR-ValueSets-RaceCode-Keres'></a>
### Keres `constants`

##### Summary

Keres

<a name='F-VRDR-ValueSets-RaceCode-Ketchikan_Indian_Corporation'></a>
### Ketchikan_Indian_Corporation `constants`

##### Summary

Ketchikan_Indian_Corporation

<a name='F-VRDR-ValueSets-RaceCode-Keweenaw_Bay_Indian_Community_Of_The_Lanse_And_Ontonagon_Bands'></a>
### Keweenaw_Bay_Indian_Community_Of_The_Lanse_And_Ontonagon_Bands `constants`

##### Summary

Keweenaw_Bay_Indian_Community_Of_The_Lanse_And_Ontonagon_Bands

<a name='F-VRDR-ValueSets-RaceCode-Kialegee_Tribal_Town'></a>
### Kialegee_Tribal_Town `constants`

##### Summary

Kialegee_Tribal_Town

<a name='F-VRDR-ValueSets-RaceCode-Kickapoo'></a>
### Kickapoo `constants`

##### Summary

Kickapoo

<a name='F-VRDR-ValueSets-RaceCode-Kikiallus'></a>
### Kikiallus `constants`

##### Summary

Kikiallus

<a name='F-VRDR-ValueSets-RaceCode-King_Cove'></a>
### King_Cove `constants`

##### Summary

King_Cove

<a name='F-VRDR-ValueSets-RaceCode-King_Island_Native_Community'></a>
### King_Island_Native_Community `constants`

##### Summary

King_Island_Native_Community

<a name='F-VRDR-ValueSets-RaceCode-King_Salmon'></a>
### King_Salmon `constants`

##### Summary

King_Salmon

<a name='F-VRDR-ValueSets-RaceCode-Kiowa'></a>
### Kiowa `constants`

##### Summary

Kiowa

<a name='F-VRDR-ValueSets-RaceCode-Kirabati'></a>
### Kirabati `constants`

##### Summary

Kirabati

<a name='F-VRDR-ValueSets-RaceCode-Klallam'></a>
### Klallam `constants`

##### Summary

Klallam

<a name='F-VRDR-ValueSets-RaceCode-Klamath'></a>
### Klamath `constants`

##### Summary

Klamath

<a name='F-VRDR-ValueSets-RaceCode-Klawock_Cooperative_Association'></a>
### Klawock_Cooperative_Association `constants`

##### Summary

Klawock_Cooperative_Association

<a name='F-VRDR-ValueSets-RaceCode-Knik_Tribe'></a>
### Knik_Tribe `constants`

##### Summary

Knik_Tribe

<a name='F-VRDR-ValueSets-RaceCode-Kodiak'></a>
### Kodiak `constants`

##### Summary

Kodiak

<a name='F-VRDR-ValueSets-RaceCode-Kokhanok_Village'></a>
### Kokhanok_Village `constants`

##### Summary

Kokhanok_Village

<a name='F-VRDR-ValueSets-RaceCode-Koniag_Aleut'></a>
### Koniag_Aleut `constants`

##### Summary

Koniag_Aleut

<a name='F-VRDR-ValueSets-RaceCode-Konkow'></a>
### Konkow `constants`

##### Summary

Konkow

<a name='F-VRDR-ValueSets-RaceCode-Kootenai'></a>
### Kootenai `constants`

##### Summary

Kootenai

<a name='F-VRDR-ValueSets-RaceCode-Korean'></a>
### Korean `constants`

##### Summary

Korean

<a name='F-VRDR-ValueSets-RaceCode-Kosovian'></a>
### Kosovian `constants`

##### Summary

Kosovian

<a name='F-VRDR-ValueSets-RaceCode-Kosraean'></a>
### Kosraean `constants`

##### Summary

Kosraean

<a name='F-VRDR-ValueSets-RaceCode-Koyukuk_Native_Village'></a>
### Koyukuk_Native_Village `constants`

##### Summary

Koyukuk_Native_Village

<a name='F-VRDR-ValueSets-RaceCode-Kutenai_Indian'></a>
### Kutenai_Indian `constants`

##### Summary

Kutenai_Indian

<a name='F-VRDR-ValueSets-RaceCode-Kwiguk'></a>
### Kwiguk `constants`

##### Summary

Kwiguk

<a name='F-VRDR-ValueSets-RaceCode-La_Jolla_Band_Of_Luiseno_Mission_Indians'></a>
### La_Jolla_Band_Of_Luiseno_Mission_Indians `constants`

##### Summary

La_Jolla_Band_Of_Luiseno_Mission_Indians

<a name='F-VRDR-ValueSets-RaceCode-La_Posta_Band_Of_Diegueno_Mission_Indians'></a>
### La_Posta_Band_Of_Diegueno_Mission_Indians `constants`

##### Summary

La_Posta_Band_Of_Diegueno_Mission_Indians

<a name='F-VRDR-ValueSets-RaceCode-Lac_Court_Oreilles_Band_Of_Lake_Superior_Chippewa'></a>
### Lac_Court_Oreilles_Band_Of_Lake_Superior_Chippewa `constants`

##### Summary

Lac_Court_Oreilles_Band_Of_Lake_Superior_Chippewa

<a name='F-VRDR-ValueSets-RaceCode-Lac_Du_Flambeau'></a>
### Lac_Du_Flambeau `constants`

##### Summary

Lac_Du_Flambeau

<a name='F-VRDR-ValueSets-RaceCode-Lac_Vieux_Desert_Band_Of_Lake_Superior_Chippewa'></a>
### Lac_Vieux_Desert_Band_Of_Lake_Superior_Chippewa `constants`

##### Summary

Lac_Vieux_Desert_Band_Of_Lake_Superior_Chippewa

<a name='F-VRDR-ValueSets-RaceCode-Laguna'></a>
### Laguna `constants`

##### Summary

Laguna

<a name='F-VRDR-ValueSets-RaceCode-Lake_Minchumina'></a>
### Lake_Minchumina `constants`

##### Summary

Lake_Minchumina

<a name='F-VRDR-ValueSets-RaceCode-Lake_Superior'></a>
### Lake_Superior `constants`

##### Summary

Lake_Superior

<a name='F-VRDR-ValueSets-RaceCode-Lake_Traverse_Sioux'></a>
### Lake_Traverse_Sioux `constants`

##### Summary

Lake_Traverse_Sioux

<a name='F-VRDR-ValueSets-RaceCode-Laotian'></a>
### Laotian `constants`

##### Summary

Laotian

<a name='F-VRDR-ValueSets-RaceCode-Las_Vegas_Tribe_Of_The_Las_Vegas_Indian_Colony'></a>
### Las_Vegas_Tribe_Of_The_Las_Vegas_Indian_Colony `constants`

##### Summary

Las_Vegas_Tribe_Of_The_Las_Vegas_Indian_Colony

<a name='F-VRDR-ValueSets-RaceCode-Lassik'></a>
### Lassik `constants`

##### Summary

Lassik

<a name='F-VRDR-ValueSets-RaceCode-Latin_American'></a>
### Latin_American `constants`

##### Summary

Latin_American

<a name='F-VRDR-ValueSets-RaceCode-Lebanese'></a>
### Lebanese `constants`

##### Summary

Lebanese

<a name='F-VRDR-ValueSets-RaceCode-Leech_Lake'></a>
### Leech_Lake `constants`

##### Summary

Leech_Lake

<a name='F-VRDR-ValueSets-RaceCode-Lenni_Lanape'></a>
### Lenni_Lanape `constants`

##### Summary

Lenni_Lanape

<a name='F-VRDR-ValueSets-RaceCode-Lesnoi_Village'></a>
### Lesnoi_Village `constants`

##### Summary

Lesnoi_Village

<a name='F-VRDR-ValueSets-RaceCode-Levelock_Village'></a>
### Levelock_Village `constants`

##### Summary

Levelock_Village

<a name='F-VRDR-ValueSets-RaceCode-Liberian'></a>
### Liberian `constants`

##### Summary

Liberian

<a name='F-VRDR-ValueSets-RaceCode-Lime_Village'></a>
### Lime_Village `constants`

##### Summary

Lime_Village

<a name='F-VRDR-ValueSets-RaceCode-Lipan_Apache'></a>
### Lipan_Apache `constants`

##### Summary

Lipan_Apache

<a name='F-VRDR-ValueSets-RaceCode-Little_River_Band_Of_Ottawa_Indians_Of_Michigan'></a>
### Little_River_Band_Of_Ottawa_Indians_Of_Michigan `constants`

##### Summary

Little_River_Band_Of_Ottawa_Indians_Of_Michigan

<a name='F-VRDR-ValueSets-RaceCode-Little_Shell_Chippewa'></a>
### Little_Shell_Chippewa `constants`

##### Summary

Little_Shell_Chippewa

<a name='F-VRDR-ValueSets-RaceCode-Little_Traverse_Bay_Bands_Of_Ottawa_Indians_Of_Michigan'></a>
### Little_Traverse_Bay_Bands_Of_Ottawa_Indians_Of_Michigan `constants`

##### Summary

Little_Traverse_Bay_Bands_Of_Ottawa_Indians_Of_Michigan

<a name='F-VRDR-ValueSets-RaceCode-Lone_Pine'></a>
### Lone_Pine `constants`

##### Summary

Lone_Pine

<a name='F-VRDR-ValueSets-RaceCode-Long_Island'></a>
### Long_Island `constants`

##### Summary

Long_Island

<a name='F-VRDR-ValueSets-RaceCode-Los_Coyotes_Band_Of_Cahuilla_Mission_Indians'></a>
### Los_Coyotes_Band_Of_Cahuilla_Mission_Indians `constants`

##### Summary

Los_Coyotes_Band_Of_Cahuilla_Mission_Indians

<a name='F-VRDR-ValueSets-RaceCode-Lovelock_Paiute_Tribe_Of_The_Lovelock_Indian_Colony'></a>
### Lovelock_Paiute_Tribe_Of_The_Lovelock_Indian_Colony `constants`

##### Summary

Lovelock_Paiute_Tribe_Of_The_Lovelock_Indian_Colony

<a name='F-VRDR-ValueSets-RaceCode-Lower_Brule_Sioux'></a>
### Lower_Brule_Sioux `constants`

##### Summary

Lower_Brule_Sioux

<a name='F-VRDR-ValueSets-RaceCode-Lower_Elwha_Tribal_Community'></a>
### Lower_Elwha_Tribal_Community `constants`

##### Summary

Lower_Elwha_Tribal_Community

<a name='F-VRDR-ValueSets-RaceCode-Lower_Muscogee_Creek_Tama_Tribal_Town'></a>
### Lower_Muscogee_Creek_Tama_Tribal_Town `constants`

##### Summary

Lower_Muscogee_Creek_Tama_Tribal_Town

<a name='F-VRDR-ValueSets-RaceCode-Lower_Sioux_Indian_Community_Of_Minnesota_Mdewakanton_Sioux'></a>
### Lower_Sioux_Indian_Community_Of_Minnesota_Mdewakanton_Sioux `constants`

##### Summary

Lower_Sioux_Indian_Community_Of_Minnesota_Mdewakanton_Sioux

<a name='F-VRDR-ValueSets-RaceCode-Lower_Skagit'></a>
### Lower_Skagit `constants`

##### Summary

Lower_Skagit

<a name='F-VRDR-ValueSets-RaceCode-Luiseno'></a>
### Luiseno `constants`

##### Summary

Luiseno

<a name='F-VRDR-ValueSets-RaceCode-Lumbee'></a>
### Lumbee `constants`

##### Summary

Lumbee

<a name='F-VRDR-ValueSets-RaceCode-Lummi'></a>
### Lummi `constants`

##### Summary

Lummi

<a name='F-VRDR-ValueSets-RaceCode-Lytton_Rancheria_Of_California'></a>
### Lytton_Rancheria_Of_California `constants`

##### Summary

Lytton_Rancheria_Of_California

<a name='F-VRDR-ValueSets-RaceCode-Machis_Lower_Creek_Indian'></a>
### Machis_Lower_Creek_Indian `constants`

##### Summary

Machis_Lower_Creek_Indian

<a name='F-VRDR-ValueSets-RaceCode-Madagascar'></a>
### Madagascar `constants`

##### Summary

Madagascar

<a name='F-VRDR-ValueSets-RaceCode-Maidu'></a>
### Maidu `constants`

##### Summary

Maidu

<a name='F-VRDR-ValueSets-RaceCode-Makah'></a>
### Makah `constants`

##### Summary

Makah

<a name='F-VRDR-ValueSets-RaceCode-Malada'></a>
### Malada `constants`

##### Summary

Malada

<a name='F-VRDR-ValueSets-RaceCode-Malaysian'></a>
### Malaysian `constants`

##### Summary

Malaysian

<a name='F-VRDR-ValueSets-RaceCode-Maldivian'></a>
### Maldivian `constants`

##### Summary

Maldivian

<a name='F-VRDR-ValueSets-RaceCode-Malheur_Paiute'></a>
### Malheur_Paiute `constants`

##### Summary

Malheur_Paiute

<a name='F-VRDR-ValueSets-RaceCode-Maliseet'></a>
### Maliseet `constants`

##### Summary

Maliseet

<a name='F-VRDR-ValueSets-RaceCode-Manchester_Band_Of_Pomo_Indians_Of_The_Manchesterpoint_Arena_Racheria'></a>
### Manchester_Band_Of_Pomo_Indians_Of_The_Manchesterpoint_Arena_Racheria `constants`

##### Summary

Manchester_Band_Of_Pomo_Indians_Of_The_Manchesterpoint_Arena_Racheria

<a name='F-VRDR-ValueSets-RaceCode-Mandan'></a>
### Mandan `constants`

##### Summary

Mandan

<a name='F-VRDR-ValueSets-RaceCode-Manley_Hot_Springs_Village'></a>
### Manley_Hot_Springs_Village `constants`

##### Summary

Manley_Hot_Springs_Village

<a name='F-VRDR-ValueSets-RaceCode-Manokotak_Village'></a>
### Manokotak_Village `constants`

##### Summary

Manokotak_Village

<a name='F-VRDR-ValueSets-RaceCode-Manzanita'></a>
### Manzanita `constants`

##### Summary

Manzanita

<a name='F-VRDR-ValueSets-RaceCode-Mariana_Islander'></a>
### Mariana_Islander `constants`

##### Summary

Mariana_Islander

<a name='F-VRDR-ValueSets-RaceCode-Maricopa'></a>
### Maricopa `constants`

##### Summary

Maricopa

<a name='F-VRDR-ValueSets-RaceCode-Marietta_Band_Of_Nooksack'></a>
### Marietta_Band_Of_Nooksack `constants`

##### Summary

Marietta_Band_Of_Nooksack

<a name='F-VRDR-ValueSets-RaceCode-Marshallese'></a>
### Marshallese `constants`

##### Summary

Marshallese

<a name='F-VRDR-ValueSets-RaceCode-Mashantucket_Pequot'></a>
### Mashantucket_Pequot `constants`

##### Summary

Mashantucket_Pequot

<a name='F-VRDR-ValueSets-RaceCode-Mashpee_Wampanoag'></a>
### Mashpee_Wampanoag `constants`

##### Summary

Mashpee_Wampanoag

<a name='F-VRDR-ValueSets-RaceCode-Matinecock'></a>
### Matinecock `constants`

##### Summary

Matinecock

<a name='F-VRDR-ValueSets-RaceCode-Mattaponi_Indian_Tribe'></a>
### Mattaponi_Indian_Tribe `constants`

##### Summary

Mattaponi_Indian_Tribe

<a name='F-VRDR-ValueSets-RaceCode-Mattole'></a>
### Mattole `constants`

##### Summary

Mattole

<a name='F-VRDR-ValueSets-RaceCode-Mauneluk_Inupiat'></a>
### Mauneluk_Inupiat `constants`

##### Summary

Mauneluk_Inupiat

<a name='F-VRDR-ValueSets-RaceCode-Mcgrath_Native_Village'></a>
### Mcgrath_Native_Village `constants`

##### Summary

Mcgrath_Native_Village

<a name='F-VRDR-ValueSets-RaceCode-Mdewakanton_Sioux'></a>
### Mdewakanton_Sioux `constants`

##### Summary

Mdewakanton_Sioux

<a name='F-VRDR-ValueSets-RaceCode-Mechoopda_Indian_Tribe_Of_Chico_Rancheria_California'></a>
### Mechoopda_Indian_Tribe_Of_Chico_Rancheria_California `constants`

##### Summary

Mechoopda_Indian_Tribe_Of_Chico_Rancheria_California

<a name='F-VRDR-ValueSets-RaceCode-Mehemn_Indian_Tribe'></a>
### Mehemn_Indian_Tribe `constants`

##### Summary

Mehemn_Indian_Tribe

<a name='F-VRDR-ValueSets-RaceCode-Melanesian'></a>
### Melanesian `constants`

##### Summary

Melanesian

<a name='F-VRDR-ValueSets-RaceCode-Melungeon'></a>
### Melungeon `constants`

##### Summary

Melungeon

<a name='F-VRDR-ValueSets-RaceCode-Menominee'></a>
### Menominee `constants`

##### Summary

Menominee

<a name='F-VRDR-ValueSets-RaceCode-Mentasta_Traditional_Council'></a>
### Mentasta_Traditional_Council `constants`

##### Summary

Mentasta_Traditional_Council

<a name='F-VRDR-ValueSets-RaceCode-Mesa_Grande_Band_Of_Diegueno_Mission_Indians'></a>
### Mesa_Grande_Band_Of_Diegueno_Mission_Indians `constants`

##### Summary

Mesa_Grande_Band_Of_Diegueno_Mission_Indians

<a name='F-VRDR-ValueSets-RaceCode-Mescalero_Apache'></a>
### Mescalero_Apache `constants`

##### Summary

Mescalero_Apache

<a name='F-VRDR-ValueSets-RaceCode-Mestizo'></a>
### Mestizo `constants`

##### Summary

Mestizo

<a name='F-VRDR-ValueSets-RaceCode-Metlakatia_Indian_Community_Annette_Island_Reserve'></a>
### Metlakatia_Indian_Community_Annette_Island_Reserve `constants`

##### Summary

Metlakatia_Indian_Community_Annette_Island_Reserve

<a name='F-VRDR-ValueSets-RaceCode-Metrolina_Nadve_American_Association'></a>
### Metrolina_Nadve_American_Association `constants`

##### Summary

Metrolina_Nadve_American_Association

<a name='F-VRDR-ValueSets-RaceCode-Mewuk'></a>
### Mewuk `constants`

##### Summary

Mewuk

<a name='F-VRDR-ValueSets-RaceCode-Mexican'></a>
### Mexican `constants`

##### Summary

Mexican

<a name='F-VRDR-ValueSets-RaceCode-Mexican_American_Indian'></a>
### Mexican_American_Indian `constants`

##### Summary

Mexican_American_Indian

<a name='F-VRDR-ValueSets-RaceCode-Miami'></a>
### Miami `constants`

##### Summary

Miami

<a name='F-VRDR-ValueSets-RaceCode-Miccosukee'></a>
### Miccosukee `constants`

##### Summary

Miccosukee

<a name='F-VRDR-ValueSets-RaceCode-Micmac'></a>
### Micmac `constants`

##### Summary

Micmac

<a name='F-VRDR-ValueSets-RaceCode-Micronesian'></a>
### Micronesian `constants`

##### Summary

Micronesian

<a name='F-VRDR-ValueSets-RaceCode-Middle_East'></a>
### Middle_East `constants`

##### Summary

Middle_East

<a name='F-VRDR-ValueSets-RaceCode-Middletown_Rancheria_Of_Pomo_Indians'></a>
### Middletown_Rancheria_Of_Pomo_Indians `constants`

##### Summary

Middletown_Rancheria_Of_Pomo_Indians

<a name='F-VRDR-ValueSets-RaceCode-Mien'></a>
### Mien `constants`

##### Summary

Mien

<a name='F-VRDR-ValueSets-RaceCode-Mille_Lacs'></a>
### Mille_Lacs `constants`

##### Summary

Mille_Lacs

<a name='F-VRDR-ValueSets-RaceCode-Miniconjou'></a>
### Miniconjou `constants`

##### Summary

Miniconjou

<a name='F-VRDR-ValueSets-RaceCode-Minnesota_Chippewa'></a>
### Minnesota_Chippewa `constants`

##### Summary

Minnesota_Chippewa

<a name='F-VRDR-ValueSets-RaceCode-Mission_Band'></a>
### Mission_Band `constants`

##### Summary

Mission_Band

<a name='F-VRDR-ValueSets-RaceCode-Mission_Indians'></a>
### Mission_Indians `constants`

##### Summary

Mission_Indians

<a name='F-VRDR-ValueSets-RaceCode-Mississippi_Band_Of_Choctaw'></a>
### Mississippi_Band_Of_Choctaw `constants`

##### Summary

Mississippi_Band_Of_Choctaw

<a name='F-VRDR-ValueSets-RaceCode-Mixed'></a>
### Mixed `constants`

##### Summary

Mixed

<a name='F-VRDR-ValueSets-RaceCode-Moapa_Band_Of_Paiute'></a>
### Moapa_Band_Of_Paiute `constants`

##### Summary

Moapa_Band_Of_Paiute

<a name='F-VRDR-ValueSets-RaceCode-Modoc'></a>
### Modoc `constants`

##### Summary

Modoc

<a name='F-VRDR-ValueSets-RaceCode-Mohawk'></a>
### Mohawk `constants`

##### Summary

Mohawk

<a name='F-VRDR-ValueSets-RaceCode-Mohegan'></a>
### Mohegan `constants`

##### Summary

Mohegan

<a name='F-VRDR-ValueSets-RaceCode-Molalla'></a>
### Molalla `constants`

##### Summary

Molalla

<a name='F-VRDR-ValueSets-RaceCode-Monacan_Indian_Nation'></a>
### Monacan_Indian_Nation `constants`

##### Summary

Monacan_Indian_Nation

<a name='F-VRDR-ValueSets-RaceCode-Mongolian'></a>
### Mongolian `constants`

##### Summary

Mongolian

<a name='F-VRDR-ValueSets-RaceCode-Mono'></a>
### Mono `constants`

##### Summary

Mono

<a name='F-VRDR-ValueSets-RaceCode-Montauk'></a>
### Montauk `constants`

##### Summary

Montauk

<a name='F-VRDR-ValueSets-RaceCode-Moor'></a>
### Moor `constants`

##### Summary

Moor

<a name='F-VRDR-ValueSets-RaceCode-Mooretown_Rancheria_Of_Maidu_Indians'></a>
### Mooretown_Rancheria_Of_Maidu_Indians `constants`

##### Summary

Mooretown_Rancheria_Of_Maidu_Indians

<a name='F-VRDR-ValueSets-RaceCode-Morena'></a>
### Morena `constants`

##### Summary

Morena

<a name='F-VRDR-ValueSets-RaceCode-Moroccan'></a>
### Moroccan `constants`

##### Summary

Moroccan

<a name='F-VRDR-ValueSets-RaceCode-Morongo_Band_Of_Cahuilla_Mission_Indians'></a>
### Morongo_Band_Of_Cahuilla_Mission_Indians `constants`

##### Summary

Morongo_Band_Of_Cahuilla_Mission_Indians

<a name='F-VRDR-ValueSets-RaceCode-Mountain_Maidu'></a>
### Mountain_Maidu `constants`

##### Summary

Mountain_Maidu

<a name='F-VRDR-ValueSets-RaceCode-Mountain_Village'></a>
### Mountain_Village `constants`

##### Summary

Mountain_Village

<a name='F-VRDR-ValueSets-RaceCode-Mowa_Band_Of_Choctaw'></a>
### Mowa_Band_Of_Choctaw `constants`

##### Summary

Mowa_Band_Of_Choctaw

<a name='F-VRDR-ValueSets-RaceCode-Muckleshoot'></a>
### Muckleshoot `constants`

##### Summary

Muckleshoot

<a name='F-VRDR-ValueSets-RaceCode-Mulatto'></a>
### Mulatto `constants`

##### Summary

Mulatto

<a name='F-VRDR-ValueSets-RaceCode-Multiethnic'></a>
### Multiethnic `constants`

##### Summary

Multiethnic

<a name='F-VRDR-ValueSets-RaceCode-Multinational'></a>
### Multinational `constants`

##### Summary

Multinational

<a name='F-VRDR-ValueSets-RaceCode-Multiple_Asian_Responses'></a>
### Multiple_Asian_Responses `constants`

##### Summary

Multiple_Asian_Responses

<a name='F-VRDR-ValueSets-RaceCode-Multiple_Black_Or_African_American_Responses'></a>
### Multiple_Black_Or_African_American_Responses `constants`

##### Summary

Multiple_Black_Or_African_American_Responses

<a name='F-VRDR-ValueSets-RaceCode-Multiple_Native_Hawaiian_And_Other_Pacific_Islander_Responses'></a>
### Multiple_Native_Hawaiian_And_Other_Pacific_Islander_Responses `constants`

##### Summary

Multiple_Native_Hawaiian_And_Other_Pacific_Islander_Responses

<a name='F-VRDR-ValueSets-RaceCode-Multiple_Some_Other_Race_Responses_995_American'></a>
### Multiple_Some_Other_Race_Responses_995_American `constants`

##### Summary

Multiple_Some_Other_Race_Responses_995_American

<a name='F-VRDR-ValueSets-RaceCode-Multiple_White_Responses'></a>
### Multiple_White_Responses `constants`

##### Summary

Multiple_White_Responses

<a name='F-VRDR-ValueSets-RaceCode-Multiracial'></a>
### Multiracial `constants`

##### Summary

Multiracial

<a name='F-VRDR-ValueSets-RaceCode-Munsee'></a>
### Munsee `constants`

##### Summary

Munsee

<a name='F-VRDR-ValueSets-RaceCode-Muscogee_Creek_Nation'></a>
### Muscogee_Creek_Nation `constants`

##### Summary

Muscogee_Creek_Nation

<a name='F-VRDR-ValueSets-RaceCode-Naknek_Native_Village'></a>
### Naknek_Native_Village `constants`

##### Summary

Naknek_Native_Village

<a name='F-VRDR-ValueSets-RaceCode-Nambe'></a>
### Nambe `constants`

##### Summary

Nambe

<a name='F-VRDR-ValueSets-RaceCode-Namibian'></a>
### Namibian `constants`

##### Summary

Namibian

<a name='F-VRDR-ValueSets-RaceCode-Nana_Inupiat'></a>
### Nana_Inupiat `constants`

##### Summary

Nana_Inupiat

<a name='F-VRDR-ValueSets-RaceCode-Nansemond_Indian_Tribe'></a>
### Nansemond_Indian_Tribe `constants`

##### Summary

Nansemond_Indian_Tribe

<a name='F-VRDR-ValueSets-RaceCode-Nanticoke'></a>
### Nanticoke `constants`

##### Summary

Nanticoke

<a name='F-VRDR-ValueSets-RaceCode-Nanticoke_Lennilenape'></a>
### Nanticoke_Lennilenape `constants`

##### Summary

Nanticoke_Lennilenape

<a name='F-VRDR-ValueSets-RaceCode-Narragansett'></a>
### Narragansett `constants`

##### Summary

Narragansett

<a name='F-VRDR-ValueSets-RaceCode-Natchez'></a>
### Natchez `constants`

##### Summary

Natchez

<a name='F-VRDR-ValueSets-RaceCode-Native_Hawaiian'></a>
### Native_Hawaiian `constants`

##### Summary

Native_Hawaiian

<a name='F-VRDR-ValueSets-RaceCode-Native_Hawaiian_Checkbox'></a>
### Native_Hawaiian_Checkbox `constants`

##### Summary

Native_Hawaiian_Checkbox

<a name='F-VRDR-ValueSets-RaceCode-Native_Of_Hamilton'></a>
### Native_Of_Hamilton `constants`

##### Summary

Native_Of_Hamilton

<a name='F-VRDR-ValueSets-RaceCode-Native_Village_Of_Akhiok'></a>
### Native_Village_Of_Akhiok `constants`

##### Summary

Native_Village_Of_Akhiok

<a name='F-VRDR-ValueSets-RaceCode-Native_Village_Of_Akutan'></a>
### Native_Village_Of_Akutan `constants`

##### Summary

Native_Village_Of_Akutan

<a name='F-VRDR-ValueSets-RaceCode-Native_Village_Of_Aleknagik'></a>
### Native_Village_Of_Aleknagik `constants`

##### Summary

Native_Village_Of_Aleknagik

<a name='F-VRDR-ValueSets-RaceCode-Native_Village_Of_Ambler'></a>
### Native_Village_Of_Ambler `constants`

##### Summary

Native_Village_Of_Ambler

<a name='F-VRDR-ValueSets-RaceCode-Native_Village_Of_Atka'></a>
### Native_Village_Of_Atka `constants`

##### Summary

Native_Village_Of_Atka

<a name='F-VRDR-ValueSets-RaceCode-Native_Village_Of_Barrow_Hilipiat_Traditional_Government'></a>
### Native_Village_Of_Barrow_Hilipiat_Traditional_Government `constants`

##### Summary

Native_Village_Of_Barrow_Hilipiat_Traditional_Government

<a name='F-VRDR-ValueSets-RaceCode-Native_Village_Of_Belkofski'></a>
### Native_Village_Of_Belkofski `constants`

##### Summary

Native_Village_Of_Belkofski

<a name='F-VRDR-ValueSets-RaceCode-Native_Village_Of_Brevig_Mission'></a>
### Native_Village_Of_Brevig_Mission `constants`

##### Summary

Native_Village_Of_Brevig_Mission

<a name='F-VRDR-ValueSets-RaceCode-Native_Village_Of_Cantwell'></a>
### Native_Village_Of_Cantwell `constants`

##### Summary

Native_Village_Of_Cantwell

<a name='F-VRDR-ValueSets-RaceCode-Native_Village_Of_Chanega'></a>
### Native_Village_Of_Chanega `constants`

##### Summary

Native_Village_Of_Chanega

<a name='F-VRDR-ValueSets-RaceCode-Native_Village_Of_Chignik'></a>
### Native_Village_Of_Chignik `constants`

##### Summary

Native_Village_Of_Chignik

<a name='F-VRDR-ValueSets-RaceCode-Native_Village_Of_Chignikn_Lagoon'></a>
### Native_Village_Of_Chignikn_Lagoon `constants`

##### Summary

Native_Village_Of_Chignikn_Lagoon

<a name='F-VRDR-ValueSets-RaceCode-Native_Village_Of_Chistochina'></a>
### Native_Village_Of_Chistochina `constants`

##### Summary

Native_Village_Of_Chistochina

<a name='F-VRDR-ValueSets-RaceCode-Native_Village_Of_Chitina'></a>
### Native_Village_Of_Chitina `constants`

##### Summary

Native_Village_Of_Chitina

<a name='F-VRDR-ValueSets-RaceCode-Native_Village_Of_Chuathbaluk'></a>
### Native_Village_Of_Chuathbaluk `constants`

##### Summary

Native_Village_Of_Chuathbaluk

<a name='F-VRDR-ValueSets-RaceCode-Native_Village_Of_Council'></a>
### Native_Village_Of_Council `constants`

##### Summary

Native_Village_Of_Council

<a name='F-VRDR-ValueSets-RaceCode-Native_Village_Of_Deering'></a>
### Native_Village_Of_Deering `constants`

##### Summary

Native_Village_Of_Deering

<a name='F-VRDR-ValueSets-RaceCode-Native_Village_Of_Diomede'></a>
### Native_Village_Of_Diomede `constants`

##### Summary

Native_Village_Of_Diomede

<a name='F-VRDR-ValueSets-RaceCode-Native_Village_Of_Eagle'></a>
### Native_Village_Of_Eagle `constants`

##### Summary

Native_Village_Of_Eagle

<a name='F-VRDR-ValueSets-RaceCode-Native_Village_Of_Eek'></a>
### Native_Village_Of_Eek `constants`

##### Summary

Native_Village_Of_Eek

<a name='F-VRDR-ValueSets-RaceCode-Native_Village_Of_Ekuk'></a>
### Native_Village_Of_Ekuk `constants`

##### Summary

Native_Village_Of_Ekuk

<a name='F-VRDR-ValueSets-RaceCode-Native_Village_Of_Elim'></a>
### Native_Village_Of_Elim `constants`

##### Summary

Native_Village_Of_Elim

<a name='F-VRDR-ValueSets-RaceCode-Native_Village_Of_False_Pass'></a>
### Native_Village_Of_False_Pass `constants`

##### Summary

Native_Village_Of_False_Pass

<a name='F-VRDR-ValueSets-RaceCode-Native_Village_Of_Fort_Yukon'></a>
### Native_Village_Of_Fort_Yukon `constants`

##### Summary

Native_Village_Of_Fort_Yukon

<a name='F-VRDR-ValueSets-RaceCode-Native_Village_Of_Gakona'></a>
### Native_Village_Of_Gakona `constants`

##### Summary

Native_Village_Of_Gakona

<a name='F-VRDR-ValueSets-RaceCode-Native_Village_Of_Gambell'></a>
### Native_Village_Of_Gambell `constants`

##### Summary

Native_Village_Of_Gambell

<a name='F-VRDR-ValueSets-RaceCode-Native_Village_Of_Georgetown'></a>
### Native_Village_Of_Georgetown `constants`

##### Summary

Native_Village_Of_Georgetown

<a name='F-VRDR-ValueSets-RaceCode-Native_Village_Of_Goodnews_Bay'></a>
### Native_Village_Of_Goodnews_Bay `constants`

##### Summary

Native_Village_Of_Goodnews_Bay

<a name='F-VRDR-ValueSets-RaceCode-Native_Village_Of_Hooper_Bay'></a>
### Native_Village_Of_Hooper_Bay `constants`

##### Summary

Native_Village_Of_Hooper_Bay

<a name='F-VRDR-ValueSets-RaceCode-Native_Village_Of_Kanatak'></a>
### Native_Village_Of_Kanatak `constants`

##### Summary

Native_Village_Of_Kanatak

<a name='F-VRDR-ValueSets-RaceCode-Native_Village_Of_Karluk'></a>
### Native_Village_Of_Karluk `constants`

##### Summary

Native_Village_Of_Karluk

<a name='F-VRDR-ValueSets-RaceCode-Native_Village_Of_Kasigluk'></a>
### Native_Village_Of_Kasigluk `constants`

##### Summary

Native_Village_Of_Kasigluk

<a name='F-VRDR-ValueSets-RaceCode-Native_Village_Of_Kiana'></a>
### Native_Village_Of_Kiana `constants`

##### Summary

Native_Village_Of_Kiana

<a name='F-VRDR-ValueSets-RaceCode-Native_Village_Of_Kipnuk'></a>
### Native_Village_Of_Kipnuk `constants`

##### Summary

Native_Village_Of_Kipnuk

<a name='F-VRDR-ValueSets-RaceCode-Native_Village_Of_Kivalina'></a>
### Native_Village_Of_Kivalina `constants`

##### Summary

Native_Village_Of_Kivalina

<a name='F-VRDR-ValueSets-RaceCode-Native_Village_Of_Kluti_Kaah'></a>
### Native_Village_Of_Kluti_Kaah `constants`

##### Summary

Native_Village_Of_Kluti_Kaah

<a name='F-VRDR-ValueSets-RaceCode-Native_Village_Of_Kobuk'></a>
### Native_Village_Of_Kobuk `constants`

##### Summary

Native_Village_Of_Kobuk

<a name='F-VRDR-ValueSets-RaceCode-Native_Village_Of_Kongiganak'></a>
### Native_Village_Of_Kongiganak `constants`

##### Summary

Native_Village_Of_Kongiganak

<a name='F-VRDR-ValueSets-RaceCode-Native_Village_Of_Kotzebue'></a>
### Native_Village_Of_Kotzebue `constants`

##### Summary

Native_Village_Of_Kotzebue

<a name='F-VRDR-ValueSets-RaceCode-Native_Village_Of_Koyuk'></a>
### Native_Village_Of_Koyuk `constants`

##### Summary

Native_Village_Of_Koyuk

<a name='F-VRDR-ValueSets-RaceCode-Native_Village_Of_Kwigillingok'></a>
### Native_Village_Of_Kwigillingok `constants`

##### Summary

Native_Village_Of_Kwigillingok

<a name='F-VRDR-ValueSets-RaceCode-Native_Village_Of_Kwinhagak'></a>
### Native_Village_Of_Kwinhagak `constants`

##### Summary

Native_Village_Of_Kwinhagak

<a name='F-VRDR-ValueSets-RaceCode-Native_Village_Of_Larsen_Bay'></a>
### Native_Village_Of_Larsen_Bay `constants`

##### Summary

Native_Village_Of_Larsen_Bay

<a name='F-VRDR-ValueSets-RaceCode-Native_Village_Of_Marshall'></a>
### Native_Village_Of_Marshall `constants`

##### Summary

Native_Village_Of_Marshall

<a name='F-VRDR-ValueSets-RaceCode-Native_Village_Of_Marys_Igloo'></a>
### Native_Village_Of_Marys_Igloo `constants`

##### Summary

Native_Village_Of_Marys_Igloo

<a name='F-VRDR-ValueSets-RaceCode-Native_Village_Of_Mekoryuk'></a>
### Native_Village_Of_Mekoryuk `constants`

##### Summary

Native_Village_Of_Mekoryuk

<a name='F-VRDR-ValueSets-RaceCode-Native_Village_Of_Minto'></a>
### Native_Village_Of_Minto `constants`

##### Summary

Native_Village_Of_Minto

<a name='F-VRDR-ValueSets-RaceCode-Native_Village_Of_Nanwaiek'></a>
### Native_Village_Of_Nanwaiek `constants`

##### Summary

Native_Village_Of_Nanwaiek

<a name='F-VRDR-ValueSets-RaceCode-Native_Village_Of_Napaimute'></a>
### Native_Village_Of_Napaimute `constants`

##### Summary

Native_Village_Of_Napaimute

<a name='F-VRDR-ValueSets-RaceCode-Native_Village_Of_Napakiak'></a>
### Native_Village_Of_Napakiak `constants`

##### Summary

Native_Village_Of_Napakiak

<a name='F-VRDR-ValueSets-RaceCode-Native_Village_Of_Napaskiak'></a>
### Native_Village_Of_Napaskiak `constants`

##### Summary

Native_Village_Of_Napaskiak

<a name='F-VRDR-ValueSets-RaceCode-Native_Village_Of_Nelson_Lagoon'></a>
### Native_Village_Of_Nelson_Lagoon `constants`

##### Summary

Native_Village_Of_Nelson_Lagoon

<a name='F-VRDR-ValueSets-RaceCode-Native_Village_Of_Nightmute'></a>
### Native_Village_Of_Nightmute `constants`

##### Summary

Native_Village_Of_Nightmute

<a name='F-VRDR-ValueSets-RaceCode-Native_Village_Of_Nikolski'></a>
### Native_Village_Of_Nikolski `constants`

##### Summary

Native_Village_Of_Nikolski

<a name='F-VRDR-ValueSets-RaceCode-Native_Village_Of_Noatak'></a>
### Native_Village_Of_Noatak `constants`

##### Summary

Native_Village_Of_Noatak

<a name='F-VRDR-ValueSets-RaceCode-Native_Village_Of_Nuiqsut'></a>
### Native_Village_Of_Nuiqsut `constants`

##### Summary

Native_Village_Of_Nuiqsut

<a name='F-VRDR-ValueSets-RaceCode-Native_Village_Of_Nunapitchuk'></a>
### Native_Village_Of_Nunapitchuk `constants`

##### Summary

Native_Village_Of_Nunapitchuk

<a name='F-VRDR-ValueSets-RaceCode-Native_Village_Of_Ouzinkie'></a>
### Native_Village_Of_Ouzinkie `constants`

##### Summary

Native_Village_Of_Ouzinkie

<a name='F-VRDR-ValueSets-RaceCode-Native_Village_Of_Perryville'></a>
### Native_Village_Of_Perryville `constants`

##### Summary

Native_Village_Of_Perryville

<a name='F-VRDR-ValueSets-RaceCode-Native_Village_Of_Pilot_Point'></a>
### Native_Village_Of_Pilot_Point `constants`

##### Summary

Native_Village_Of_Pilot_Point

<a name='F-VRDR-ValueSets-RaceCode-Native_Village_Of_Pitkas_Point'></a>
### Native_Village_Of_Pitkas_Point `constants`

##### Summary

Native_Village_Of_Pitkas_Point

<a name='F-VRDR-ValueSets-RaceCode-Native_Village_Of_Point_Hope'></a>
### Native_Village_Of_Point_Hope `constants`

##### Summary

Native_Village_Of_Point_Hope

<a name='F-VRDR-ValueSets-RaceCode-Native_Village_Of_Point_Lay'></a>
### Native_Village_Of_Point_Lay `constants`

##### Summary

Native_Village_Of_Point_Lay

<a name='F-VRDR-ValueSets-RaceCode-Native_Village_Of_Port_Graham'></a>
### Native_Village_Of_Port_Graham `constants`

##### Summary

Native_Village_Of_Port_Graham

<a name='F-VRDR-ValueSets-RaceCode-Native_Village_Of_Port_Heiden'></a>
### Native_Village_Of_Port_Heiden `constants`

##### Summary

Native_Village_Of_Port_Heiden

<a name='F-VRDR-ValueSets-RaceCode-Native_Village_Of_Port_Lions'></a>
### Native_Village_Of_Port_Lions `constants`

##### Summary

Native_Village_Of_Port_Lions

<a name='F-VRDR-ValueSets-RaceCode-Native_Village_Of_Ruby'></a>
### Native_Village_Of_Ruby `constants`

##### Summary

Native_Village_Of_Ruby

<a name='F-VRDR-ValueSets-RaceCode-Native_Village_Of_Saint_Michael'></a>
### Native_Village_Of_Saint_Michael `constants`

##### Summary

Native_Village_Of_Saint_Michael

<a name='F-VRDR-ValueSets-RaceCode-Native_Village_Of_Savoonga'></a>
### Native_Village_Of_Savoonga `constants`

##### Summary

Native_Village_Of_Savoonga

<a name='F-VRDR-ValueSets-RaceCode-Native_Village_Of_Scammon_Bay'></a>
### Native_Village_Of_Scammon_Bay `constants`

##### Summary

Native_Village_Of_Scammon_Bay

<a name='F-VRDR-ValueSets-RaceCode-Native_Village_Of_Selawik'></a>
### Native_Village_Of_Selawik `constants`

##### Summary

Native_Village_Of_Selawik

<a name='F-VRDR-ValueSets-RaceCode-Native_Village_Of_Shaktoolik'></a>
### Native_Village_Of_Shaktoolik `constants`

##### Summary

Native_Village_Of_Shaktoolik

<a name='F-VRDR-ValueSets-RaceCode-Native_Village_Of_Sheldons_Point'></a>
### Native_Village_Of_Sheldons_Point `constants`

##### Summary

Native_Village_Of_Sheldons_Point

<a name='F-VRDR-ValueSets-RaceCode-Native_Village_Of_Shishmaref'></a>
### Native_Village_Of_Shishmaref `constants`

##### Summary

Native_Village_Of_Shishmaref

<a name='F-VRDR-ValueSets-RaceCode-Native_Village_Of_Shungnak'></a>
### Native_Village_Of_Shungnak `constants`

##### Summary

Native_Village_Of_Shungnak

<a name='F-VRDR-ValueSets-RaceCode-Native_Village_Of_Stevens'></a>
### Native_Village_Of_Stevens `constants`

##### Summary

Native_Village_Of_Stevens

<a name='F-VRDR-ValueSets-RaceCode-Native_Village_Of_Tanacross'></a>
### Native_Village_Of_Tanacross `constants`

##### Summary

Native_Village_Of_Tanacross

<a name='F-VRDR-ValueSets-RaceCode-Native_Village_Of_Tanana'></a>
### Native_Village_Of_Tanana `constants`

##### Summary

Native_Village_Of_Tanana

<a name='F-VRDR-ValueSets-RaceCode-Native_Village_Of_Tatitlek'></a>
### Native_Village_Of_Tatitlek `constants`

##### Summary

Native_Village_Of_Tatitlek

<a name='F-VRDR-ValueSets-RaceCode-Native_Village_Of_Tazlina'></a>
### Native_Village_Of_Tazlina `constants`

##### Summary

Native_Village_Of_Tazlina

<a name='F-VRDR-ValueSets-RaceCode-Native_Village_Of_Teller'></a>
### Native_Village_Of_Teller `constants`

##### Summary

Native_Village_Of_Teller

<a name='F-VRDR-ValueSets-RaceCode-Native_Village_Of_Tetlin'></a>
### Native_Village_Of_Tetlin `constants`

##### Summary

Native_Village_Of_Tetlin

<a name='F-VRDR-ValueSets-RaceCode-Native_Village_Of_Toksook_Bay'></a>
### Native_Village_Of_Toksook_Bay `constants`

##### Summary

Native_Village_Of_Toksook_Bay

<a name='F-VRDR-ValueSets-RaceCode-Native_Village_Of_Tuntutuliak'></a>
### Native_Village_Of_Tuntutuliak `constants`

##### Summary

Native_Village_Of_Tuntutuliak

<a name='F-VRDR-ValueSets-RaceCode-Native_Village_Of_Tununak'></a>
### Native_Village_Of_Tununak `constants`

##### Summary

Native_Village_Of_Tununak

<a name='F-VRDR-ValueSets-RaceCode-Native_Village_Of_Tyonek'></a>
### Native_Village_Of_Tyonek `constants`

##### Summary

Native_Village_Of_Tyonek

<a name='F-VRDR-ValueSets-RaceCode-Native_Village_Of_Unalakleet'></a>
### Native_Village_Of_Unalakleet `constants`

##### Summary

Native_Village_Of_Unalakleet

<a name='F-VRDR-ValueSets-RaceCode-Native_Village_Of_Unga'></a>
### Native_Village_Of_Unga `constants`

##### Summary

Native_Village_Of_Unga

<a name='F-VRDR-ValueSets-RaceCode-Native_Village_Ofbuckland'></a>
### Native_Village_Ofbuckland `constants`

##### Summary

Native_Village_Ofbuckland

<a name='F-VRDR-ValueSets-RaceCode-Nausu_Waiwash'></a>
### Nausu_Waiwash `constants`

##### Summary

Nausu_Waiwash

<a name='F-VRDR-ValueSets-RaceCode-Navajo'></a>
### Navajo `constants`

##### Summary

Navajo

<a name='F-VRDR-ValueSets-RaceCode-Near_Easterner'></a>
### Near_Easterner `constants`

##### Summary

Near_Easterner

<a name='F-VRDR-ValueSets-RaceCode-Nebraska_Ponca'></a>
### Nebraska_Ponca `constants`

##### Summary

Nebraska_Ponca

<a name='F-VRDR-ValueSets-RaceCode-Nebraska_Winnebago'></a>
### Nebraska_Winnebago `constants`

##### Summary

Nebraska_Winnebago

<a name='F-VRDR-ValueSets-RaceCode-Negro'></a>
### Negro `constants`

##### Summary

Negro

<a name='F-VRDR-ValueSets-RaceCode-Nenana_Native_Association'></a>
### Nenana_Native_Association `constants`

##### Summary

Nenana_Native_Association

<a name='F-VRDR-ValueSets-RaceCode-Nepalese'></a>
### Nepalese `constants`

##### Summary

Nepalese

<a name='F-VRDR-ValueSets-RaceCode-New_Hebrides'></a>
### New_Hebrides `constants`

##### Summary

New_Hebrides

<a name='F-VRDR-ValueSets-RaceCode-New_Koliganek_Village_Council'></a>
### New_Koliganek_Village_Council `constants`

##### Summary

New_Koliganek_Village_Council

<a name='F-VRDR-ValueSets-RaceCode-New_Stuyahok_Village'></a>
### New_Stuyahok_Village `constants`

##### Summary

New_Stuyahok_Village

<a name='F-VRDR-ValueSets-RaceCode-Newhalen_Village'></a>
### Newhalen_Village `constants`

##### Summary

Newhalen_Village

<a name='F-VRDR-ValueSets-RaceCode-Newtek_Village'></a>
### Newtek_Village `constants`

##### Summary

Newtek_Village

<a name='F-VRDR-ValueSets-RaceCode-Nez_Perce'></a>
### Nez_Perce `constants`

##### Summary

Nez_Perce

<a name='F-VRDR-ValueSets-RaceCode-Nicaraguan'></a>
### Nicaraguan `constants`

##### Summary

Nicaraguan

<a name='F-VRDR-ValueSets-RaceCode-Nigerian'></a>
### Nigerian `constants`

##### Summary

Nigerian

<a name='F-VRDR-ValueSets-RaceCode-Nigritian'></a>
### Nigritian `constants`

##### Summary

Nigritian

<a name='F-VRDR-ValueSets-RaceCode-Nikolai_Village'></a>
### Nikolai_Village `constants`

##### Summary

Nikolai_Village

<a name='F-VRDR-ValueSets-RaceCode-Ninilchik_Village_Traditional_Council'></a>
### Ninilchik_Village_Traditional_Council `constants`

##### Summary

Ninilchik_Village_Traditional_Council

<a name='F-VRDR-ValueSets-RaceCode-Nipmuc'></a>
### Nipmuc `constants`

##### Summary

Nipmuc

<a name='F-VRDR-ValueSets-RaceCode-Nisenen'></a>
### Nisenen `constants`

##### Summary

Nisenen

<a name='F-VRDR-ValueSets-RaceCode-Nisqually'></a>
### Nisqually `constants`

##### Summary

Nisqually

<a name='F-VRDR-ValueSets-RaceCode-Nome_Eskimo_Community'></a>
### Nome_Eskimo_Community `constants`

##### Summary

Nome_Eskimo_Community

<a name='F-VRDR-ValueSets-RaceCode-Nomlaki'></a>
### Nomlaki `constants`

##### Summary

Nomlaki

<a name='F-VRDR-ValueSets-RaceCode-Nondalton_Village'></a>
### Nondalton_Village `constants`

##### Summary

Nondalton_Village

<a name='F-VRDR-ValueSets-RaceCode-Nooksack'></a>
### Nooksack `constants`

##### Summary

Nooksack

<a name='F-VRDR-ValueSets-RaceCode-Noorvik_Native_Community'></a>
### Noorvik_Native_Community `constants`

##### Summary

Noorvik_Native_Community

<a name='F-VRDR-ValueSets-RaceCode-North_African'></a>
### North_African `constants`

##### Summary

North_African

<a name='F-VRDR-ValueSets-RaceCode-North_Fork_Rancheria'></a>
### North_Fork_Rancheria `constants`

##### Summary

North_Fork_Rancheria

<a name='F-VRDR-ValueSets-RaceCode-Northern_Arapahoe'></a>
### Northern_Arapahoe `constants`

##### Summary

Northern_Arapahoe

<a name='F-VRDR-ValueSets-RaceCode-Northern_Cherokee_Nation_Of_Missouri_And_Arkansas'></a>
### Northern_Cherokee_Nation_Of_Missouri_And_Arkansas `constants`

##### Summary

Northern_Cherokee_Nation_Of_Missouri_And_Arkansas

<a name='F-VRDR-ValueSets-RaceCode-Northern_Cheyenne'></a>
### Northern_Cheyenne `constants`

##### Summary

Northern_Cheyenne

<a name='F-VRDR-ValueSets-RaceCode-Northern_Paiute'></a>
### Northern_Paiute `constants`

##### Summary

Northern_Paiute

<a name='F-VRDR-ValueSets-RaceCode-Northern_Pomo'></a>
### Northern_Pomo `constants`

##### Summary

Northern_Pomo

<a name='F-VRDR-ValueSets-RaceCode-Northway_Village'></a>
### Northway_Village `constants`

##### Summary

Northway_Village

<a name='F-VRDR-ValueSets-RaceCode-Northwestern_Band_Of_Shoshoni_Nation_Of_Utah'></a>
### Northwestern_Band_Of_Shoshoni_Nation_Of_Utah `constants`

##### Summary

Northwestern_Band_Of_Shoshoni_Nation_Of_Utah

<a name='F-VRDR-ValueSets-RaceCode-Nulato_Village'></a>
### Nulato_Village `constants`

##### Summary

Nulato_Village

<a name='F-VRDR-ValueSets-RaceCode-Octoroon'></a>
### Octoroon `constants`

##### Summary

Octoroon

<a name='F-VRDR-ValueSets-RaceCode-Odgers_Ranch'></a>
### Odgers_Ranch `constants`

##### Summary

Odgers_Ranch

<a name='F-VRDR-ValueSets-RaceCode-Oglala_Sioux'></a>
### Oglala_Sioux `constants`

##### Summary

Oglala_Sioux

<a name='F-VRDR-ValueSets-RaceCode-Okinawan'></a>
### Okinawan `constants`

##### Summary

Okinawan

<a name='F-VRDR-ValueSets-RaceCode-Oklahoma_Apache'></a>
### Oklahoma_Apache `constants`

##### Summary

Oklahoma_Apache

<a name='F-VRDR-ValueSets-RaceCode-Oklahoma_Choctaw'></a>
### Oklahoma_Choctaw `constants`

##### Summary

Oklahoma_Choctaw

<a name='F-VRDR-ValueSets-RaceCode-Oklahoma_Comanche'></a>
### Oklahoma_Comanche `constants`

##### Summary

Oklahoma_Comanche

<a name='F-VRDR-ValueSets-RaceCode-Oklahoma_Kickapoo'></a>
### Oklahoma_Kickapoo `constants`

##### Summary

Oklahoma_Kickapoo

<a name='F-VRDR-ValueSets-RaceCode-Oklahoma_Kiowa'></a>
### Oklahoma_Kiowa `constants`

##### Summary

Oklahoma_Kiowa

<a name='F-VRDR-ValueSets-RaceCode-Oklahoma_Miami'></a>
### Oklahoma_Miami `constants`

##### Summary

Oklahoma_Miami

<a name='F-VRDR-ValueSets-RaceCode-Oklahoma_Modoc'></a>
### Oklahoma_Modoc `constants`

##### Summary

Oklahoma_Modoc

<a name='F-VRDR-ValueSets-RaceCode-Oklahoma_Ottawa'></a>
### Oklahoma_Ottawa `constants`

##### Summary

Oklahoma_Ottawa

<a name='F-VRDR-ValueSets-RaceCode-Oklahoma_Pawnee'></a>
### Oklahoma_Pawnee `constants`

##### Summary

Oklahoma_Pawnee

<a name='F-VRDR-ValueSets-RaceCode-Oklahoma_Peoria'></a>
### Oklahoma_Peoria `constants`

##### Summary

Oklahoma_Peoria

<a name='F-VRDR-ValueSets-RaceCode-Oklahoma_Ponca'></a>
### Oklahoma_Ponca `constants`

##### Summary

Oklahoma_Ponca

<a name='F-VRDR-ValueSets-RaceCode-Oklahoma_Seminole'></a>
### Oklahoma_Seminole `constants`

##### Summary

Oklahoma_Seminole

<a name='F-VRDR-ValueSets-RaceCode-Omaha'></a>
### Omaha `constants`

##### Summary

Omaha

<a name='F-VRDR-ValueSets-RaceCode-Oneida_Nation_Of_New_York'></a>
### Oneida_Nation_Of_New_York `constants`

##### Summary

Oneida_Nation_Of_New_York

<a name='F-VRDR-ValueSets-RaceCode-Oneida_Tribe_Of_Wisconsin'></a>
### Oneida_Tribe_Of_Wisconsin `constants`

##### Summary

Oneida_Tribe_Of_Wisconsin

<a name='F-VRDR-ValueSets-RaceCode-Onondaga'></a>
### Onondaga `constants`

##### Summary

Onondaga

<a name='F-VRDR-ValueSets-RaceCode-Ontonagon'></a>
### Ontonagon `constants`

##### Summary

Ontonagon

<a name='F-VRDR-ValueSets-RaceCode-Oregon_Athabaskan'></a>
### Oregon_Athabaskan `constants`

##### Summary

Oregon_Athabaskan

<a name='F-VRDR-ValueSets-RaceCode-Organized_Village_Of_Grayling'></a>
### Organized_Village_Of_Grayling `constants`

##### Summary

Organized_Village_Of_Grayling

<a name='F-VRDR-ValueSets-RaceCode-Organized_Village_Of_Kake'></a>
### Organized_Village_Of_Kake `constants`

##### Summary

Organized_Village_Of_Kake

<a name='F-VRDR-ValueSets-RaceCode-Organized_Village_Of_Kasaan'></a>
### Organized_Village_Of_Kasaan `constants`

##### Summary

Organized_Village_Of_Kasaan

<a name='F-VRDR-ValueSets-RaceCode-Organized_Village_Of_Kwethluk'></a>
### Organized_Village_Of_Kwethluk `constants`

##### Summary

Organized_Village_Of_Kwethluk

<a name='F-VRDR-ValueSets-RaceCode-Organized_Village_Of_Saxman'></a>
### Organized_Village_Of_Saxman `constants`

##### Summary

Organized_Village_Of_Saxman

<a name='F-VRDR-ValueSets-RaceCode-Oriental'></a>
### Oriental `constants`

##### Summary

Oriental

<a name='F-VRDR-ValueSets-RaceCode-Orutsararmuit_Native_Village'></a>
### Orutsararmuit_Native_Village `constants`

##### Summary

Orutsararmuit_Native_Village

<a name='F-VRDR-ValueSets-RaceCode-Osage'></a>
### Osage `constants`

##### Summary

Osage

<a name='F-VRDR-ValueSets-RaceCode-Oscarville_Traditional_Village'></a>
### Oscarville_Traditional_Village `constants`

##### Summary

Oscarville_Traditional_Village

<a name='F-VRDR-ValueSets-RaceCode-Other_African'></a>
### Other_African `constants`

##### Summary

Other_African

<a name='F-VRDR-ValueSets-RaceCode-Other_Alaskan_Nec'></a>
### Other_Alaskan_Nec `constants`

##### Summary

Other_Alaskan_Nec

<a name='F-VRDR-ValueSets-RaceCode-Other_Arab'></a>
### Other_Arab `constants`

##### Summary

Other_Arab

<a name='F-VRDR-ValueSets-RaceCode-Other_Race_N_E_C'></a>
### Other_Race_N_E_C `constants`

##### Summary

Other_Race_N_E_C

<a name='F-VRDR-ValueSets-RaceCode-Other_Spanish'></a>
### Other_Spanish `constants`

##### Summary

Other_Spanish

<a name='F-VRDR-ValueSets-RaceCode-Otoemissouria'></a>
### Otoemissouria `constants`

##### Summary

Otoemissouria

<a name='F-VRDR-ValueSets-RaceCode-Ottawa'></a>
### Ottawa `constants`

##### Summary

Ottawa

<a name='F-VRDR-ValueSets-RaceCode-Pacific_Islander'></a>
### Pacific_Islander `constants`

##### Summary

Pacific_Islander

<a name='F-VRDR-ValueSets-RaceCode-Paiute'></a>
### Paiute `constants`

##### Summary

Paiute

<a name='F-VRDR-ValueSets-RaceCode-Pakistani'></a>
### Pakistani `constants`

##### Summary

Pakistani

<a name='F-VRDR-ValueSets-RaceCode-Pala_Band_Of_Luiseno_Mission_Indians'></a>
### Pala_Band_Of_Luiseno_Mission_Indians `constants`

##### Summary

Pala_Band_Of_Luiseno_Mission_Indians

<a name='F-VRDR-ValueSets-RaceCode-Palauan'></a>
### Palauan `constants`

##### Summary

Palauan

<a name='F-VRDR-ValueSets-RaceCode-Palestinian'></a>
### Palestinian `constants`

##### Summary

Palestinian

<a name='F-VRDR-ValueSets-RaceCode-Pamunkey_Indian_Tribe'></a>
### Pamunkey_Indian_Tribe `constants`

##### Summary

Pamunkey_Indian_Tribe

<a name='F-VRDR-ValueSets-RaceCode-Panamanian'></a>
### Panamanian `constants`

##### Summary

Panamanian

<a name='F-VRDR-ValueSets-RaceCode-Panamint'></a>
### Panamint `constants`

##### Summary

Panamint

<a name='F-VRDR-ValueSets-RaceCode-Papua_New_Guinean'></a>
### Papua_New_Guinean `constants`

##### Summary

Papua_New_Guinean

<a name='F-VRDR-ValueSets-RaceCode-Paraguayan'></a>
### Paraguayan `constants`

##### Summary

Paraguayan

<a name='F-VRDR-ValueSets-RaceCode-Part_Hawaiian'></a>
### Part_Hawaiian `constants`

##### Summary

Part_Hawaiian

<a name='F-VRDR-ValueSets-RaceCode-Pascua_Yaqui'></a>
### Pascua_Yaqui `constants`

##### Summary

Pascua_Yaqui

<a name='F-VRDR-ValueSets-RaceCode-Paskenta_Band_Of_Nomlaki_Indians'></a>
### Paskenta_Band_Of_Nomlaki_Indians `constants`

##### Summary

Paskenta_Band_Of_Nomlaki_Indians

<a name='F-VRDR-ValueSets-RaceCode-Passamaquoddy'></a>
### Passamaquoddy `constants`

##### Summary

Passamaquoddy

<a name='F-VRDR-ValueSets-RaceCode-Paucatuck_Eastern_Pequot'></a>
### Paucatuck_Eastern_Pequot `constants`

##### Summary

Paucatuck_Eastern_Pequot

<a name='F-VRDR-ValueSets-RaceCode-Pauloff_Harbor_Village'></a>
### Pauloff_Harbor_Village `constants`

##### Summary

Pauloff_Harbor_Village

<a name='F-VRDR-ValueSets-RaceCode-Pauma_Band_Of_Luiseno_Mission_Indians'></a>
### Pauma_Band_Of_Luiseno_Mission_Indians `constants`

##### Summary

Pauma_Band_Of_Luiseno_Mission_Indians

<a name='F-VRDR-ValueSets-RaceCode-Pawnee'></a>
### Pawnee `constants`

##### Summary

Pawnee

<a name='F-VRDR-ValueSets-RaceCode-Payson_Tonto_Apache'></a>
### Payson_Tonto_Apache `constants`

##### Summary

Payson_Tonto_Apache

<a name='F-VRDR-ValueSets-RaceCode-Pechanga_Band_Of_Luiseno_Mission_Indians'></a>
### Pechanga_Band_Of_Luiseno_Mission_Indians `constants`

##### Summary

Pechanga_Band_Of_Luiseno_Mission_Indians

<a name='F-VRDR-ValueSets-RaceCode-Pedro_Bay_Village'></a>
### Pedro_Bay_Village `constants`

##### Summary

Pedro_Bay_Village

<a name='F-VRDR-ValueSets-RaceCode-Pelican'></a>
### Pelican `constants`

##### Summary

Pelican

<a name='F-VRDR-ValueSets-RaceCode-Penobscot'></a>
### Penobscot `constants`

##### Summary

Penobscot

<a name='F-VRDR-ValueSets-RaceCode-Peoria'></a>
### Peoria `constants`

##### Summary

Peoria

<a name='F-VRDR-ValueSets-RaceCode-Pequot'></a>
### Pequot `constants`

##### Summary

Pequot

<a name='F-VRDR-ValueSets-RaceCode-Peruvian'></a>
### Peruvian `constants`

##### Summary

Peruvian

<a name='F-VRDR-ValueSets-RaceCode-Petersburg_Indian_Association'></a>
### Petersburg_Indian_Association `constants`

##### Summary

Petersburg_Indian_Association

<a name='F-VRDR-ValueSets-RaceCode-Picayune_Rancheria_Of_Chukchansi_Indians'></a>
### Picayune_Rancheria_Of_Chukchansi_Indians `constants`

##### Summary

Picayune_Rancheria_Of_Chukchansi_Indians

<a name='F-VRDR-ValueSets-RaceCode-Picuris'></a>
### Picuris `constants`

##### Summary

Picuris

<a name='F-VRDR-ValueSets-RaceCode-Pilot_Station_Traditional_Village'></a>
### Pilot_Station_Traditional_Village `constants`

##### Summary

Pilot_Station_Traditional_Village

<a name='F-VRDR-ValueSets-RaceCode-Pima'></a>
### Pima `constants`

##### Summary

Pima

<a name='F-VRDR-ValueSets-RaceCode-Pine_Ridge_Sioux'></a>
### Pine_Ridge_Sioux `constants`

##### Summary

Pine_Ridge_Sioux

<a name='F-VRDR-ValueSets-RaceCode-Pinoleville_Rancheria_Of_Pomo_Indians'></a>
### Pinoleville_Rancheria_Of_Pomo_Indians `constants`

##### Summary

Pinoleville_Rancheria_Of_Pomo_Indians

<a name='F-VRDR-ValueSets-RaceCode-Pipestone_Sioux'></a>
### Pipestone_Sioux `constants`

##### Summary

Pipestone_Sioux

<a name='F-VRDR-ValueSets-RaceCode-Piqua_Sept_Of_Ohio_Shawnee'></a>
### Piqua_Sept_Of_Ohio_Shawnee `constants`

##### Summary

Piqua_Sept_Of_Ohio_Shawnee

<a name='F-VRDR-ValueSets-RaceCode-Piro'></a>
### Piro `constants`

##### Summary

Piro

<a name='F-VRDR-ValueSets-RaceCode-Piscataway'></a>
### Piscataway `constants`

##### Summary

Piscataway

<a name='F-VRDR-ValueSets-RaceCode-Pit_River_Tribe_Of_California'></a>
### Pit_River_Tribe_Of_California `constants`

##### Summary

Pit_River_Tribe_Of_California

<a name='F-VRDR-ValueSets-RaceCode-Platinum_Traditional_Village'></a>
### Platinum_Traditional_Village `constants`

##### Summary

Platinum_Traditional_Village

<a name='F-VRDR-ValueSets-RaceCode-Pleasant_Point_Passamaquoddy'></a>
### Pleasant_Point_Passamaquoddy `constants`

##### Summary

Pleasant_Point_Passamaquoddy

<a name='F-VRDR-ValueSets-RaceCode-Poarch_Band'></a>
### Poarch_Band `constants`

##### Summary

Poarch_Band

<a name='F-VRDR-ValueSets-RaceCode-Pocasset_Wampanoag'></a>
### Pocasset_Wampanoag `constants`

##### Summary

Pocasset_Wampanoag

<a name='F-VRDR-ValueSets-RaceCode-Pocomoke_Acohonock'></a>
### Pocomoke_Acohonock `constants`

##### Summary

Pocomoke_Acohonock

<a name='F-VRDR-ValueSets-RaceCode-Pohnpeian'></a>
### Pohnpeian `constants`

##### Summary

Pohnpeian

<a name='F-VRDR-ValueSets-RaceCode-Pojoaque'></a>
### Pojoaque `constants`

##### Summary

Pojoaque

<a name='F-VRDR-ValueSets-RaceCode-Pokagon_Band_Of_Potawatomi_Indians'></a>
### Pokagon_Band_Of_Potawatomi_Indians `constants`

##### Summary

Pokagon_Band_Of_Potawatomi_Indians

<a name='F-VRDR-ValueSets-RaceCode-Polish'></a>
### Polish `constants`

##### Summary

Polish

<a name='F-VRDR-ValueSets-RaceCode-Polynesian'></a>
### Polynesian `constants`

##### Summary

Polynesian

<a name='F-VRDR-ValueSets-RaceCode-Pomo'></a>
### Pomo `constants`

##### Summary

Pomo

<a name='F-VRDR-ValueSets-RaceCode-Ponca'></a>
### Ponca `constants`

##### Summary

Ponca

<a name='F-VRDR-ValueSets-RaceCode-Pondre_Band_Of_Salish_And_Kootenai'></a>
### Pondre_Band_Of_Salish_And_Kootenai `constants`

##### Summary

Pondre_Band_Of_Salish_And_Kootenai

<a name='F-VRDR-ValueSets-RaceCode-Poospatuck'></a>
### Poospatuck `constants`

##### Summary

Poospatuck

<a name='F-VRDR-ValueSets-RaceCode-Port_Gamble_Klallam'></a>
### Port_Gamble_Klallam `constants`

##### Summary

Port_Gamble_Klallam

<a name='F-VRDR-ValueSets-RaceCode-Port_Madison'></a>
### Port_Madison `constants`

##### Summary

Port_Madison

<a name='F-VRDR-ValueSets-RaceCode-Portage_Creek_Village'></a>
### Portage_Creek_Village `constants`

##### Summary

Portage_Creek_Village

<a name='F-VRDR-ValueSets-RaceCode-Portuguese'></a>
### Portuguese `constants`

##### Summary

Portuguese

<a name='F-VRDR-ValueSets-RaceCode-Potawatomi'></a>
### Potawatomi `constants`

##### Summary

Potawatomi

<a name='F-VRDR-ValueSets-RaceCode-Potter_Valley_Rancheria_Of_Pomo_Indians'></a>
### Potter_Valley_Rancheria_Of_Pomo_Indians `constants`

##### Summary

Potter_Valley_Rancheria_Of_Pomo_Indians

<a name='F-VRDR-ValueSets-RaceCode-Powhatan'></a>
### Powhatan `constants`

##### Summary

Powhatan

<a name='F-VRDR-ValueSets-RaceCode-Prairie_Band_Of_Potawatomi_Indians'></a>
### Prairie_Band_Of_Potawatomi_Indians `constants`

##### Summary

Prairie_Band_Of_Potawatomi_Indians

<a name='F-VRDR-ValueSets-RaceCode-Prairie_Island_Sioux'></a>
### Prairie_Island_Sioux `constants`

##### Summary

Prairie_Island_Sioux

<a name='F-VRDR-ValueSets-RaceCode-Principal_Creek_Indian_Nation'></a>
### Principal_Creek_Indian_Nation `constants`

##### Summary

Principal_Creek_Indian_Nation

<a name='F-VRDR-ValueSets-RaceCode-Pueblo'></a>
### Pueblo `constants`

##### Summary

Pueblo

<a name='F-VRDR-ValueSets-RaceCode-Puerto_Rican'></a>
### Puerto_Rican `constants`

##### Summary

Puerto_Rican

<a name='F-VRDR-ValueSets-RaceCode-Puget_Sound_Salish'></a>
### Puget_Sound_Salish `constants`

##### Summary

Puget_Sound_Salish

<a name='F-VRDR-ValueSets-RaceCode-Puyaliup'></a>
### Puyaliup `constants`

##### Summary

Puyaliup

<a name='F-VRDR-ValueSets-RaceCode-Pyramid_Lake'></a>
### Pyramid_Lake `constants`

##### Summary

Pyramid_Lake

<a name='F-VRDR-ValueSets-RaceCode-Qagan_Toyagungin_Tribe_Of_Sand_Point_Village'></a>
### Qagan_Toyagungin_Tribe_Of_Sand_Point_Village `constants`

##### Summary

Qagan_Toyagungin_Tribe_Of_Sand_Point_Village

<a name='F-VRDR-ValueSets-RaceCode-Qawalangin_Tribe_Of_Unalaska'></a>
### Qawalangin_Tribe_Of_Unalaska `constants`

##### Summary

Qawalangin_Tribe_Of_Unalaska

<a name='F-VRDR-ValueSets-RaceCode-Quadroon'></a>
### Quadroon `constants`

##### Summary

Quadroon

<a name='F-VRDR-ValueSets-RaceCode-Quapaw'></a>
### Quapaw `constants`

##### Summary

Quapaw

<a name='F-VRDR-ValueSets-RaceCode-Quartz_Valley'></a>
### Quartz_Valley `constants`

##### Summary

Quartz_Valley

<a name='F-VRDR-ValueSets-RaceCode-Quechan'></a>
### Quechan `constants`

##### Summary

Quechan

<a name='F-VRDR-ValueSets-RaceCode-Quileute'></a>
### Quileute `constants`

##### Summary

Quileute

<a name='F-VRDR-ValueSets-RaceCode-Quinault'></a>
### Quinault `constants`

##### Summary

Quinault

<a name='F-VRDR-ValueSets-RaceCode-Rainbow'></a>
### Rainbow `constants`

##### Summary

Rainbow

<a name='F-VRDR-ValueSets-RaceCode-Ramah_Navajo'></a>
### Ramah_Navajo `constants`

##### Summary

Ramah_Navajo

<a name='F-VRDR-ValueSets-RaceCode-Ramapough_Mountain'></a>
### Ramapough_Mountain `constants`

##### Summary

Ramapough_Mountain

<a name='F-VRDR-ValueSets-RaceCode-Ramona_Band_Or_Village_Of_Cahuilla_Mission_Indians'></a>
### Ramona_Band_Or_Village_Of_Cahuilla_Mission_Indians `constants`

##### Summary

Ramona_Band_Or_Village_Of_Cahuilla_Mission_Indians

<a name='F-VRDR-ValueSets-RaceCode-Ramp'></a>
### Ramp `constants`

##### Summary

Ramp

<a name='F-VRDR-ValueSets-RaceCode-Rampart_Village'></a>
### Rampart_Village `constants`

##### Summary

Rampart_Village

<a name='F-VRDR-ValueSets-RaceCode-Rappahannock_Indian_Tribe'></a>
### Rappahannock_Indian_Tribe `constants`

##### Summary

Rappahannock_Indian_Tribe

<a name='F-VRDR-ValueSets-RaceCode-Red_Cliff_Band_Of_Lake_Superior_Chippewa'></a>
### Red_Cliff_Band_Of_Lake_Superior_Chippewa `constants`

##### Summary

Red_Cliff_Band_Of_Lake_Superior_Chippewa

<a name='F-VRDR-ValueSets-RaceCode-Red_Lake_Band_Of_Chippewa_Indians'></a>
### Red_Lake_Band_Of_Chippewa_Indians `constants`

##### Summary

Red_Lake_Band_Of_Chippewa_Indians

<a name='F-VRDR-ValueSets-RaceCode-Red_Wood'></a>
### Red_Wood `constants`

##### Summary

Red_Wood

<a name='F-VRDR-ValueSets-RaceCode-Redding_Rancheria'></a>
### Redding_Rancheria `constants`

##### Summary

Redding_Rancheria

<a name='F-VRDR-ValueSets-RaceCode-Redwood_Valley_Rancheria_Of_Pomo_Indians'></a>
### Redwood_Valley_Rancheria_Of_Pomo_Indians `constants`

##### Summary

Redwood_Valley_Rancheria_Of_Pomo_Indians

<a name='F-VRDR-ValueSets-RaceCode-Renosparks'></a>
### Renosparks `constants`

##### Summary

Renosparks

<a name='F-VRDR-ValueSets-RaceCode-Resighini_Rancheria'></a>
### Resighini_Rancheria `constants`

##### Summary

Resighini_Rancheria

<a name='F-VRDR-ValueSets-RaceCode-Rincon_Band_Of_Luiseno_Mission_Indians'></a>
### Rincon_Band_Of_Luiseno_Mission_Indians `constants`

##### Summary

Rincon_Band_Of_Luiseno_Mission_Indians

<a name='F-VRDR-ValueSets-RaceCode-Robinson_Rancheria_Of_Pomo_Indians'></a>
### Robinson_Rancheria_Of_Pomo_Indians `constants`

##### Summary

Robinson_Rancheria_Of_Pomo_Indians

<a name='F-VRDR-ValueSets-RaceCode-Rocky_Boys_Chippewa_Cree'></a>
### Rocky_Boys_Chippewa_Cree `constants`

##### Summary

Rocky_Boys_Chippewa_Cree

<a name='F-VRDR-ValueSets-RaceCode-Rosebud_Sioux'></a>
### Rosebud_Sioux `constants`

##### Summary

Rosebud_Sioux

<a name='F-VRDR-ValueSets-RaceCode-Round_Valley'></a>
### Round_Valley `constants`

##### Summary

Round_Valley

<a name='F-VRDR-ValueSets-RaceCode-Ruby_Valley'></a>
### Ruby_Valley `constants`

##### Summary

Ruby_Valley

<a name='F-VRDR-ValueSets-RaceCode-Rumsey_Indian_Rancheria_Of_Wintun_Indians'></a>
### Rumsey_Indian_Rancheria_Of_Wintun_Indians `constants`

##### Summary

Rumsey_Indian_Rancheria_Of_Wintun_Indians

<a name='F-VRDR-ValueSets-RaceCode-Russian'></a>
### Russian `constants`

##### Summary

Russian

<a name='F-VRDR-ValueSets-RaceCode-Sac_And_Fox'></a>
### Sac_And_Fox `constants`

##### Summary

Sac_And_Fox

<a name='F-VRDR-ValueSets-RaceCode-Sac_And_Fox_Nation_Of_Missouri_In_Kansas_And_Nebraska'></a>
### Sac_And_Fox_Nation_Of_Missouri_In_Kansas_And_Nebraska `constants`

##### Summary

Sac_And_Fox_Nation_Of_Missouri_In_Kansas_And_Nebraska

<a name='F-VRDR-ValueSets-RaceCode-Sac_And_Fox_Nation_Oklahoma'></a>
### Sac_And_Fox_Nation_Oklahoma `constants`

##### Summary

Sac_And_Fox_Nation_Oklahoma

<a name='F-VRDR-ValueSets-RaceCode-Sac_And_Fox_Tribe_Of_The_Mississippi_In_Iowa'></a>
### Sac_And_Fox_Tribe_Of_The_Mississippi_In_Iowa `constants`

##### Summary

Sac_And_Fox_Tribe_Of_The_Mississippi_In_Iowa

<a name='F-VRDR-ValueSets-RaceCode-Sac_River_Band_Of_The_Chickamauga_Cherokee'></a>
### Sac_River_Band_Of_The_Chickamauga_Cherokee `constants`

##### Summary

Sac_River_Band_Of_The_Chickamauga_Cherokee

<a name='F-VRDR-ValueSets-RaceCode-Saginaw_Chippewa'></a>
### Saginaw_Chippewa `constants`

##### Summary

Saginaw_Chippewa

<a name='F-VRDR-ValueSets-RaceCode-Saint_George'></a>
### Saint_George `constants`

##### Summary

Saint_George

<a name='F-VRDR-ValueSets-RaceCode-Saint_Paul'></a>
### Saint_Paul `constants`

##### Summary

Saint_Paul

<a name='F-VRDR-ValueSets-RaceCode-Saipanese'></a>
### Saipanese `constants`

##### Summary

Saipanese

<a name='F-VRDR-ValueSets-RaceCode-Salinan'></a>
### Salinan `constants`

##### Summary

Salinan

<a name='F-VRDR-ValueSets-RaceCode-Salish'></a>
### Salish `constants`

##### Summary

Salish

<a name='F-VRDR-ValueSets-RaceCode-Salish_And_Kootenai'></a>
### Salish_And_Kootenai `constants`

##### Summary

Salish_And_Kootenai

<a name='F-VRDR-ValueSets-RaceCode-Salt_River_Pimamaricopa'></a>
### Salt_River_Pimamaricopa `constants`

##### Summary

Salt_River_Pimamaricopa

<a name='F-VRDR-ValueSets-RaceCode-Salvadoran'></a>
### Salvadoran `constants`

##### Summary

Salvadoran

<a name='F-VRDR-ValueSets-RaceCode-Samish'></a>
### Samish `constants`

##### Summary

Samish

<a name='F-VRDR-ValueSets-RaceCode-Samoan'></a>
### Samoan `constants`

##### Summary

Samoan

<a name='F-VRDR-ValueSets-RaceCode-San_Carlos_Apache'></a>
### San_Carlos_Apache `constants`

##### Summary

San_Carlos_Apache

<a name='F-VRDR-ValueSets-RaceCode-San_Felipe'></a>
### San_Felipe `constants`

##### Summary

San_Felipe

<a name='F-VRDR-ValueSets-RaceCode-San_Ildefonso'></a>
### San_Ildefonso `constants`

##### Summary

San_Ildefonso

<a name='F-VRDR-ValueSets-RaceCode-San_Juan'></a>
### San_Juan `constants`

##### Summary

San_Juan

<a name='F-VRDR-ValueSets-RaceCode-San_Juan_De'></a>
### San_Juan_De `constants`

##### Summary

San_Juan_De

<a name='F-VRDR-ValueSets-RaceCode-San_Juan_Pueblo'></a>
### San_Juan_Pueblo `constants`

##### Summary

San_Juan_Pueblo

<a name='F-VRDR-ValueSets-RaceCode-San_Juan_Southern_Paiute'></a>
### San_Juan_Southern_Paiute `constants`

##### Summary

San_Juan_Southern_Paiute

<a name='F-VRDR-ValueSets-RaceCode-San_Luis_Rey_Mission_Indian'></a>
### San_Luis_Rey_Mission_Indian `constants`

##### Summary

San_Luis_Rey_Mission_Indian

<a name='F-VRDR-ValueSets-RaceCode-San_Manual_Band'></a>
### San_Manual_Band `constants`

##### Summary

San_Manual_Band

<a name='F-VRDR-ValueSets-RaceCode-San_Pasqual_Band_Of_Diegueno_Mission_Indians'></a>
### San_Pasqual_Band_Of_Diegueno_Mission_Indians `constants`

##### Summary

San_Pasqual_Band_Of_Diegueno_Mission_Indians

<a name='F-VRDR-ValueSets-RaceCode-San_Xavier'></a>
### San_Xavier `constants`

##### Summary

San_Xavier

<a name='F-VRDR-ValueSets-RaceCode-Sand_Hill_Band_Of_Delaware_Indians'></a>
### Sand_Hill_Band_Of_Delaware_Indians `constants`

##### Summary

Sand_Hill_Band_Of_Delaware_Indians

<a name='F-VRDR-ValueSets-RaceCode-Sand_Point'></a>
### Sand_Point `constants`

##### Summary

Sand_Point

<a name='F-VRDR-ValueSets-RaceCode-Sandia'></a>
### Sandia `constants`

##### Summary

Sandia

<a name='F-VRDR-ValueSets-RaceCode-Sans_Arc_Sioux'></a>
### Sans_Arc_Sioux `constants`

##### Summary

Sans_Arc_Sioux

<a name='F-VRDR-ValueSets-RaceCode-Santa_Ana'></a>
### Santa_Ana `constants`

##### Summary

Santa_Ana

<a name='F-VRDR-ValueSets-RaceCode-Santa_Clara'></a>
### Santa_Clara `constants`

##### Summary

Santa_Clara

<a name='F-VRDR-ValueSets-RaceCode-Santa_Rosa_Cahuilla'></a>
### Santa_Rosa_Cahuilla `constants`

##### Summary

Santa_Rosa_Cahuilla

<a name='F-VRDR-ValueSets-RaceCode-Santa_Rosa_Indian_Community'></a>
### Santa_Rosa_Indian_Community `constants`

##### Summary

Santa_Rosa_Indian_Community

<a name='F-VRDR-ValueSets-RaceCode-Santa_Ynez'></a>
### Santa_Ynez `constants`

##### Summary

Santa_Ynez

<a name='F-VRDR-ValueSets-RaceCode-Santa_Ysabel_Band_Of_Diegueno_Mission_Indians'></a>
### Santa_Ysabel_Band_Of_Diegueno_Mission_Indians `constants`

##### Summary

Santa_Ysabel_Band_Of_Diegueno_Mission_Indians

<a name='F-VRDR-ValueSets-RaceCode-Santee_Sioux_Of_Nebraska'></a>
### Santee_Sioux_Of_Nebraska `constants`

##### Summary

Santee_Sioux_Of_Nebraska

<a name='F-VRDR-ValueSets-RaceCode-Santo_Domingo'></a>
### Santo_Domingo `constants`

##### Summary

Santo_Domingo

<a name='F-VRDR-ValueSets-RaceCode-Sauksuiattle'></a>
### Sauksuiattle `constants`

##### Summary

Sauksuiattle

<a name='F-VRDR-ValueSets-RaceCode-Sault_Ste_Marie_Chippewa'></a>
### Sault_Ste_Marie_Chippewa `constants`

##### Summary

Sault_Ste_Marie_Chippewa

<a name='F-VRDR-ValueSets-RaceCode-Schaghticoke'></a>
### Schaghticoke `constants`

##### Summary

Schaghticoke

<a name='F-VRDR-ValueSets-RaceCode-Scottish'></a>
### Scottish `constants`

##### Summary

Scottish

<a name='F-VRDR-ValueSets-RaceCode-Scotts_Valley_Band'></a>
### Scotts_Valley_Band `constants`

##### Summary

Scotts_Valley_Band

<a name='F-VRDR-ValueSets-RaceCode-Seaconeke_Wampanoag'></a>
### Seaconeke_Wampanoag `constants`

##### Summary

Seaconeke_Wampanoag

<a name='F-VRDR-ValueSets-RaceCode-Sealaska'></a>
### Sealaska `constants`

##### Summary

Sealaska

<a name='F-VRDR-ValueSets-RaceCode-Sealaska_Corporation'></a>
### Sealaska_Corporation `constants`

##### Summary

Sealaska_Corporation

<a name='F-VRDR-ValueSets-RaceCode-Seldovia_Village_Tribe'></a>
### Seldovia_Village_Tribe `constants`

##### Summary

Seldovia_Village_Tribe

<a name='F-VRDR-ValueSets-RaceCode-Sells'></a>
### Sells `constants`

##### Summary

Sells

<a name='F-VRDR-ValueSets-RaceCode-Seminole'></a>
### Seminole `constants`

##### Summary

Seminole

<a name='F-VRDR-ValueSets-RaceCode-Seneca'></a>
### Seneca `constants`

##### Summary

Seneca

<a name='F-VRDR-ValueSets-RaceCode-Seneca_Nation'></a>
### Seneca_Nation `constants`

##### Summary

Seneca_Nation

<a name='F-VRDR-ValueSets-RaceCode-Senecacayuga'></a>
### Senecacayuga `constants`

##### Summary

Senecacayuga

<a name='F-VRDR-ValueSets-RaceCode-Serrano'></a>
### Serrano `constants`

##### Summary

Serrano

<a name='F-VRDR-ValueSets-RaceCode-Setauket'></a>
### Setauket `constants`

##### Summary

Setauket

<a name='F-VRDR-ValueSets-RaceCode-Shageluk_Native_Village'></a>
### Shageluk_Native_Village `constants`

##### Summary

Shageluk_Native_Village

<a name='F-VRDR-ValueSets-RaceCode-Shakopee_Mdewakanton_Sioux_Community'></a>
### Shakopee_Mdewakanton_Sioux_Community `constants`

##### Summary

Shakopee_Mdewakanton_Sioux_Community

<a name='F-VRDR-ValueSets-RaceCode-Shasta'></a>
### Shasta `constants`

##### Summary

Shasta

<a name='F-VRDR-ValueSets-RaceCode-Shawnee'></a>
### Shawnee `constants`

##### Summary

Shawnee

<a name='F-VRDR-ValueSets-RaceCode-Sheep_Ranch_Rancheria_Of_Mewuk_Indians'></a>
### Sheep_Ranch_Rancheria_Of_Mewuk_Indians `constants`

##### Summary

Sheep_Ranch_Rancheria_Of_Mewuk_Indians

<a name='F-VRDR-ValueSets-RaceCode-Sherwood_Valley_Rancheria_Of_Pomo_Indians_Of_California'></a>
### Sherwood_Valley_Rancheria_Of_Pomo_Indians_Of_California `constants`

##### Summary

Sherwood_Valley_Rancheria_Of_Pomo_Indians_Of_California

<a name='F-VRDR-ValueSets-RaceCode-Shingle_Springs_Band_Of_Miwok_Indians'></a>
### Shingle_Springs_Band_Of_Miwok_Indians `constants`

##### Summary

Shingle_Springs_Band_Of_Miwok_Indians

<a name='F-VRDR-ValueSets-RaceCode-Shinnecock'></a>
### Shinnecock `constants`

##### Summary

Shinnecock

<a name='F-VRDR-ValueSets-RaceCode-Shoalwater_Bay'></a>
### Shoalwater_Bay `constants`

##### Summary

Shoalwater_Bay

<a name='F-VRDR-ValueSets-RaceCode-Shoshone'></a>
### Shoshone `constants`

##### Summary

Shoshone

<a name='F-VRDR-ValueSets-RaceCode-Shoshone_Paiute'></a>
### Shoshone_Paiute `constants`

##### Summary

Shoshone_Paiute

<a name='F-VRDR-ValueSets-RaceCode-Shoshonebannock_Tribes_Of_The_Fort_Hall_Reservation'></a>
### Shoshonebannock_Tribes_Of_The_Fort_Hall_Reservation `constants`

##### Summary

Shoshonebannock_Tribes_Of_The_Fort_Hall_Reservation

<a name='F-VRDR-ValueSets-RaceCode-Siberian_Eskimo'></a>
### Siberian_Eskimo `constants`

##### Summary

Siberian_Eskimo

<a name='F-VRDR-ValueSets-RaceCode-Siberian_Yupik'></a>
### Siberian_Yupik `constants`

##### Summary

Siberian_Yupik

<a name='F-VRDR-ValueSets-RaceCode-Singaporean'></a>
### Singaporean `constants`

##### Summary

Singaporean

<a name='F-VRDR-ValueSets-RaceCode-Sioux'></a>
### Sioux `constants`

##### Summary

Sioux

<a name='F-VRDR-ValueSets-RaceCode-Sisseton_Sioux'></a>
### Sisseton_Sioux `constants`

##### Summary

Sisseton_Sioux

<a name='F-VRDR-ValueSets-RaceCode-Sissetonwahpeton'></a>
### Sissetonwahpeton `constants`

##### Summary

Sissetonwahpeton

<a name='F-VRDR-ValueSets-RaceCode-Sitka_Tribe_Of_Alaska'></a>
### Sitka_Tribe_Of_Alaska `constants`

##### Summary

Sitka_Tribe_Of_Alaska

<a name='F-VRDR-ValueSets-RaceCode-Siuslaw'></a>
### Siuslaw `constants`

##### Summary

Siuslaw

<a name='F-VRDR-ValueSets-RaceCode-Skagway_Village'></a>
### Skagway_Village `constants`

##### Summary

Skagway_Village

<a name='F-VRDR-ValueSets-RaceCode-Skokomish'></a>
### Skokomish `constants`

##### Summary

Skokomish

<a name='F-VRDR-ValueSets-RaceCode-Skull_Valley_Band_Of_Goshute_Indians'></a>
### Skull_Valley_Band_Of_Goshute_Indians `constants`

##### Summary

Skull_Valley_Band_Of_Goshute_Indians

<a name='F-VRDR-ValueSets-RaceCode-Skykomish'></a>
### Skykomish `constants`

##### Summary

Skykomish

<a name='F-VRDR-ValueSets-RaceCode-Slana'></a>
### Slana `constants`

##### Summary

Slana

<a name='F-VRDR-ValueSets-RaceCode-Smith_River_Rancheria'></a>
### Smith_River_Rancheria `constants`

##### Summary

Smith_River_Rancheria

<a name='F-VRDR-ValueSets-RaceCode-Snohomish'></a>
### Snohomish `constants`

##### Summary

Snohomish

<a name='F-VRDR-ValueSets-RaceCode-Snoqualmie'></a>
### Snoqualmie `constants`

##### Summary

Snoqualmie

<a name='F-VRDR-ValueSets-RaceCode-Soboba'></a>
### Soboba `constants`

##### Summary

Soboba

<a name='F-VRDR-ValueSets-RaceCode-Sokoagon_Chippewa'></a>
### Sokoagon_Chippewa `constants`

##### Summary

Sokoagon_Chippewa

<a name='F-VRDR-ValueSets-RaceCode-Solomon_Islander'></a>
### Solomon_Islander `constants`

##### Summary

Solomon_Islander

<a name='F-VRDR-ValueSets-RaceCode-Somalian'></a>
### Somalian `constants`

##### Summary

Somalian

<a name='F-VRDR-ValueSets-RaceCode-South_African'></a>
### South_African `constants`

##### Summary

South_African

<a name='F-VRDR-ValueSets-RaceCode-South_American'></a>
### South_American `constants`

##### Summary

South_American

<a name='F-VRDR-ValueSets-RaceCode-South_American_Indian'></a>
### South_American_Indian `constants`

##### Summary

South_American_Indian

<a name='F-VRDR-ValueSets-RaceCode-South_Fork'></a>
### South_Fork `constants`

##### Summary

South_Fork

<a name='F-VRDR-ValueSets-RaceCode-South_Naknek_Village'></a>
### South_Naknek_Village `constants`

##### Summary

South_Naknek_Village

<a name='F-VRDR-ValueSets-RaceCode-Southeast_Alaska'></a>
### Southeast_Alaska `constants`

##### Summary

Southeast_Alaska

<a name='F-VRDR-ValueSets-RaceCode-Southeastern_Cherokee_Council'></a>
### Southeastern_Cherokee_Council `constants`

##### Summary

Southeastern_Cherokee_Council

<a name='F-VRDR-ValueSets-RaceCode-Southeastern_Indians'></a>
### Southeastern_Indians `constants`

##### Summary

Southeastern_Indians

<a name='F-VRDR-ValueSets-RaceCode-Southern_Arapahoe'></a>
### Southern_Arapahoe `constants`

##### Summary

Southern_Arapahoe

<a name='F-VRDR-ValueSets-RaceCode-Southern_Cheyenne'></a>
### Southern_Cheyenne `constants`

##### Summary

Southern_Cheyenne

<a name='F-VRDR-ValueSets-RaceCode-Southern_Paiute'></a>
### Southern_Paiute `constants`

##### Summary

Southern_Paiute

<a name='F-VRDR-ValueSets-RaceCode-Southern_Ute'></a>
### Southern_Ute `constants`

##### Summary

Southern_Ute

<a name='F-VRDR-ValueSets-RaceCode-Spaniard'></a>
### Spaniard `constants`

##### Summary

Spaniard

<a name='F-VRDR-ValueSets-RaceCode-Spanish'></a>
### Spanish `constants`

##### Summary

Spanish

<a name='F-VRDR-ValueSets-RaceCode-Spanish_American'></a>
### Spanish_American `constants`

##### Summary

Spanish_American

<a name='F-VRDR-ValueSets-RaceCode-Spanish_American_Indian'></a>
### Spanish_American_Indian `constants`

##### Summary

Spanish_American_Indian

<a name='F-VRDR-ValueSets-RaceCode-Spirit_Lake_Sioux'></a>
### Spirit_Lake_Sioux `constants`

##### Summary

Spirit_Lake_Sioux

<a name='F-VRDR-ValueSets-RaceCode-Spokane'></a>
### Spokane `constants`

##### Summary

Spokane

<a name='F-VRDR-ValueSets-RaceCode-Squaxin_Island'></a>
### Squaxin_Island `constants`

##### Summary

Squaxin_Island

<a name='F-VRDR-ValueSets-RaceCode-Sri_Lankan'></a>
### Sri_Lankan `constants`

##### Summary

Sri_Lankan

<a name='F-VRDR-ValueSets-RaceCode-St_Croix_Chippewa'></a>
### St_Croix_Chippewa `constants`

##### Summary

St_Croix_Chippewa

<a name='F-VRDR-ValueSets-RaceCode-Standing_Rock_Sioux'></a>
### Standing_Rock_Sioux `constants`

##### Summary

Standing_Rock_Sioux

<a name='F-VRDR-ValueSets-RaceCode-Star_Clan_Of_Muskogee_Creeks'></a>
### Star_Clan_Of_Muskogee_Creeks `constants`

##### Summary

Star_Clan_Of_Muskogee_Creeks

<a name='F-VRDR-ValueSets-RaceCode-Stebbins_Community_Association'></a>
### Stebbins_Community_Association `constants`

##### Summary

Stebbins_Community_Association

<a name='F-VRDR-ValueSets-RaceCode-Steilacoom'></a>
### Steilacoom `constants`

##### Summary

Steilacoom

<a name='F-VRDR-ValueSets-RaceCode-Stewart_Community'></a>
### Stewart_Community `constants`

##### Summary

Stewart_Community

<a name='F-VRDR-ValueSets-RaceCode-Stillaguamish'></a>
### Stillaguamish `constants`

##### Summary

Stillaguamish

<a name='F-VRDR-ValueSets-RaceCode-Stockbridgemunsee_Community_Of_Mohican_Indians_Of_Wisconsin'></a>
### Stockbridgemunsee_Community_Of_Mohican_Indians_Of_Wisconsin `constants`

##### Summary

Stockbridgemunsee_Community_Of_Mohican_Indians_Of_Wisconsin

<a name='F-VRDR-ValueSets-RaceCode-Stonyford'></a>
### Stonyford `constants`

##### Summary

Stonyford

<a name='F-VRDR-ValueSets-RaceCode-Sudamericano'></a>
### Sudamericano `constants`

##### Summary

Sudamericano

<a name='F-VRDR-ValueSets-RaceCode-Sudanese'></a>
### Sudanese `constants`

##### Summary

Sudanese

<a name='F-VRDR-ValueSets-RaceCode-Sugpiaq'></a>
### Sugpiaq `constants`

##### Summary

Sugpiaq

<a name='F-VRDR-ValueSets-RaceCode-Summit_Lake'></a>
### Summit_Lake `constants`

##### Summary

Summit_Lake

<a name='F-VRDR-ValueSets-RaceCode-Suqpigaq'></a>
### Suqpigaq `constants`

##### Summary

Suqpigaq

<a name='F-VRDR-ValueSets-RaceCode-Suquamish'></a>
### Suquamish `constants`

##### Summary

Suquamish

<a name='F-VRDR-ValueSets-RaceCode-Surinam'></a>
### Surinam `constants`

##### Summary

Surinam

<a name='F-VRDR-ValueSets-RaceCode-Susanville'></a>
### Susanville `constants`

##### Summary

Susanville

<a name='F-VRDR-ValueSets-RaceCode-Susquehanock'></a>
### Susquehanock `constants`

##### Summary

Susquehanock

<a name='F-VRDR-ValueSets-RaceCode-Swan_Creek_Black_River_Confederate_Tribe'></a>
### Swan_Creek_Black_River_Confederate_Tribe `constants`

##### Summary

Swan_Creek_Black_River_Confederate_Tribe

<a name='F-VRDR-ValueSets-RaceCode-Swinomish'></a>
### Swinomish `constants`

##### Summary

Swinomish

<a name='F-VRDR-ValueSets-RaceCode-Sycuan_Band_Of_Diegueno_Mission_Indians'></a>
### Sycuan_Band_Of_Diegueno_Mission_Indians `constants`

##### Summary

Sycuan_Band_Of_Diegueno_Mission_Indians

<a name='F-VRDR-ValueSets-RaceCode-Syrian'></a>
### Syrian `constants`

##### Summary

Syrian

<a name='F-VRDR-ValueSets-RaceCode-Table_Bluff'></a>
### Table_Bluff `constants`

##### Summary

Table_Bluff

<a name='F-VRDR-ValueSets-RaceCode-Table_Mountain_Rancheria'></a>
### Table_Mountain_Rancheria `constants`

##### Summary

Table_Mountain_Rancheria

<a name='F-VRDR-ValueSets-RaceCode-Tachi'></a>
### Tachi `constants`

##### Summary

Tachi

<a name='F-VRDR-ValueSets-RaceCode-Tahitian'></a>
### Tahitian `constants`

##### Summary

Tahitian

<a name='F-VRDR-ValueSets-RaceCode-Taiwanese'></a>
### Taiwanese `constants`

##### Summary

Taiwanese

<a name='F-VRDR-ValueSets-RaceCode-Takelma'></a>
### Takelma `constants`

##### Summary

Takelma

<a name='F-VRDR-ValueSets-RaceCode-Takotna_Village'></a>
### Takotna_Village `constants`

##### Summary

Takotna_Village

<a name='F-VRDR-ValueSets-RaceCode-Talakamish'></a>
### Talakamish `constants`

##### Summary

Talakamish

<a name='F-VRDR-ValueSets-RaceCode-Tampa_Seminole'></a>
### Tampa_Seminole `constants`

##### Summary

Tampa_Seminole

<a name='F-VRDR-ValueSets-RaceCode-Tanaina'></a>
### Tanaina `constants`

##### Summary

Tanaina

<a name='F-VRDR-ValueSets-RaceCode-Tanana_Chiefs'></a>
### Tanana_Chiefs `constants`

##### Summary

Tanana_Chiefs

<a name='F-VRDR-ValueSets-RaceCode-Taos'></a>
### Taos `constants`

##### Summary

Taos

<a name='F-VRDR-ValueSets-RaceCode-Tawakonie'></a>
### Tawakonie `constants`

##### Summary

Tawakonie

<a name='F-VRDR-ValueSets-RaceCode-Tejano'></a>
### Tejano `constants`

##### Summary

Tejano

<a name='F-VRDR-ValueSets-RaceCode-Telida_Village'></a>
### Telida_Village `constants`

##### Summary

Telida_Village

<a name='F-VRDR-ValueSets-RaceCode-Temecula'></a>
### Temecula `constants`

##### Summary

Temecula

<a name='F-VRDR-ValueSets-RaceCode-Temoak_Tribes_Of_Western_Shoshone_Indians'></a>
### Temoak_Tribes_Of_Western_Shoshone_Indians `constants`

##### Summary

Temoak_Tribes_Of_Western_Shoshone_Indians

<a name='F-VRDR-ValueSets-RaceCode-Tenakee_Springs'></a>
### Tenakee_Springs `constants`

##### Summary

Tenakee_Springs

<a name='F-VRDR-ValueSets-RaceCode-Tenino'></a>
### Tenino `constants`

##### Summary

Tenino

<a name='F-VRDR-ValueSets-RaceCode-Tesuque'></a>
### Tesuque `constants`

##### Summary

Tesuque

<a name='F-VRDR-ValueSets-RaceCode-Teton_Sioux'></a>
### Teton_Sioux `constants`

##### Summary

Teton_Sioux

<a name='F-VRDR-ValueSets-RaceCode-Tewa'></a>
### Tewa `constants`

##### Summary

Tewa

<a name='F-VRDR-ValueSets-RaceCode-Texas_Kickapoo'></a>
### Texas_Kickapoo `constants`

##### Summary

Texas_Kickapoo

<a name='F-VRDR-ValueSets-RaceCode-Thai'></a>
### Thai `constants`

##### Summary

Thai

<a name='F-VRDR-ValueSets-RaceCode-Thiopthlocco_Tribal_Town'></a>
### Thiopthlocco_Tribal_Town `constants`

##### Summary

Thiopthlocco_Tribal_Town

<a name='F-VRDR-ValueSets-RaceCode-Three_Affiliated_Tribes_Of_North_Dakota'></a>
### Three_Affiliated_Tribes_Of_North_Dakota `constants`

##### Summary

Three_Affiliated_Tribes_Of_North_Dakota

<a name='F-VRDR-ValueSets-RaceCode-Tia'></a>
### Tia `constants`

##### Summary

Tia

<a name='F-VRDR-ValueSets-RaceCode-Tibetan'></a>
### Tibetan `constants`

##### Summary

Tibetan

<a name='F-VRDR-ValueSets-RaceCode-Tigua'></a>
### Tigua `constants`

##### Summary

Tigua

<a name='F-VRDR-ValueSets-RaceCode-Tillamook'></a>
### Tillamook `constants`

##### Summary

Tillamook

<a name='F-VRDR-ValueSets-RaceCode-Tlingit'></a>
### Tlingit `constants`

##### Summary

Tlingit

<a name='F-VRDR-ValueSets-RaceCode-Tlingit_Haida'></a>
### Tlingit_Haida `constants`

##### Summary

Tlingit_Haida

<a name='F-VRDR-ValueSets-RaceCode-Tobago'></a>
### Tobago `constants`

##### Summary

Tobago

<a name='F-VRDR-ValueSets-RaceCode-Togolese'></a>
### Togolese `constants`

##### Summary

Togolese

<a name='F-VRDR-ValueSets-RaceCode-Tohajiileehee_Navajo'></a>
### Tohajiileehee_Navajo `constants`

##### Summary

Tohajiileehee_Navajo

<a name='F-VRDR-ValueSets-RaceCode-Tohono_Oodham'></a>
### Tohono_Oodham `constants`

##### Summary

Tohono_Oodham

<a name='F-VRDR-ValueSets-RaceCode-Tok'></a>
### Tok `constants`

##### Summary

Tok

<a name='F-VRDR-ValueSets-RaceCode-Tokelauan'></a>
### Tokelauan `constants`

##### Summary

Tokelauan

<a name='F-VRDR-ValueSets-RaceCode-Tolowa'></a>
### Tolowa `constants`

##### Summary

Tolowa

<a name='F-VRDR-ValueSets-RaceCode-Tonawanda_Band_Of_Seneca'></a>
### Tonawanda_Band_Of_Seneca `constants`

##### Summary

Tonawanda_Band_Of_Seneca

<a name='F-VRDR-ValueSets-RaceCode-Tongan'></a>
### Tongan `constants`

##### Summary

Tongan

<a name='F-VRDR-ValueSets-RaceCode-Tonkawa'></a>
### Tonkawa `constants`

##### Summary

Tonkawa

<a name='F-VRDR-ValueSets-RaceCode-Torres_Martinez_Band_Of_Cahuilla_Mission_Indians'></a>
### Torres_Martinez_Band_Of_Cahuilla_Mission_Indians `constants`

##### Summary

Torres_Martinez_Band_Of_Cahuilla_Mission_Indians

<a name='F-VRDR-ValueSets-RaceCode-Traditional_Village_Oftogiak'></a>
### Traditional_Village_Oftogiak `constants`

##### Summary

Traditional_Village_Oftogiak

<a name='F-VRDR-ValueSets-RaceCode-Tribal_Response_Nec'></a>
### Tribal_Response_Nec `constants`

##### Summary

Tribal_Response_Nec

<a name='F-VRDR-ValueSets-RaceCode-Trigueno'></a>
### Trigueno `constants`

##### Summary

Trigueno

<a name='F-VRDR-ValueSets-RaceCode-Trinidad'></a>
### Trinidad `constants`

##### Summary

Trinidad

<a name='F-VRDR-ValueSets-RaceCode-Trinity'></a>
### Trinity `constants`

##### Summary

Trinity

<a name='F-VRDR-ValueSets-RaceCode-Tsimshian'></a>
### Tsimshian `constants`

##### Summary

Tsimshian

<a name='F-VRDR-ValueSets-RaceCode-Tuckabachee'></a>
### Tuckabachee `constants`

##### Summary

Tuckabachee

<a name='F-VRDR-ValueSets-RaceCode-Tulalip'></a>
### Tulalip `constants`

##### Summary

Tulalip

<a name='F-VRDR-ValueSets-RaceCode-Tule_River'></a>
### Tule_River `constants`

##### Summary

Tule_River

<a name='F-VRDR-ValueSets-RaceCode-Tuluksak_Native_Community'></a>
### Tuluksak_Native_Community `constants`

##### Summary

Tuluksak_Native_Community

<a name='F-VRDR-ValueSets-RaceCode-Tunica_Biloxi'></a>
### Tunica_Biloxi `constants`

##### Summary

Tunica_Biloxi

<a name='F-VRDR-ValueSets-RaceCode-Tuolumne_Band_Of_Mewuk_Indians_Of_California'></a>
### Tuolumne_Band_Of_Mewuk_Indians_Of_California `constants`

##### Summary

Tuolumne_Band_Of_Mewuk_Indians_Of_California

<a name='F-VRDR-ValueSets-RaceCode-Turk'></a>
### Turk `constants`

##### Summary

Turk

<a name='F-VRDR-ValueSets-RaceCode-Turtle_Mountain_Band'></a>
### Turtle_Mountain_Band `constants`

##### Summary

Turtle_Mountain_Band

<a name='F-VRDR-ValueSets-RaceCode-Tuscarora'></a>
### Tuscarora `constants`

##### Summary

Tuscarora

<a name='F-VRDR-ValueSets-RaceCode-Tuscola'></a>
### Tuscola `constants`

##### Summary

Tuscola

<a name='F-VRDR-ValueSets-RaceCode-Twentynine_Palms_Band_Of_Luiseno_Mission_Indians'></a>
### Twentynine_Palms_Band_Of_Luiseno_Mission_Indians `constants`

##### Summary

Twentynine_Palms_Band_Of_Luiseno_Mission_Indians

<a name='F-VRDR-ValueSets-RaceCode-Twin_Hills_Village'></a>
### Twin_Hills_Village `constants`

##### Summary

Twin_Hills_Village

<a name='F-VRDR-ValueSets-RaceCode-Two_Kettle_Sioux'></a>
### Two_Kettle_Sioux `constants`

##### Summary

Two_Kettle_Sioux

<a name='F-VRDR-ValueSets-RaceCode-Tygh'></a>
### Tygh `constants`

##### Summary

Tygh

<a name='F-VRDR-ValueSets-RaceCode-Ugashik_Village'></a>
### Ugashik_Village `constants`

##### Summary

Ugashik_Village

<a name='F-VRDR-ValueSets-RaceCode-Uintah_Ute'></a>
### Uintah_Ute `constants`

##### Summary

Uintah_Ute

<a name='F-VRDR-ValueSets-RaceCode-Ukranian'></a>
### Ukranian `constants`

##### Summary

Ukranian

<a name='F-VRDR-ValueSets-RaceCode-Umatilla'></a>
### Umatilla `constants`

##### Summary

Umatilla

<a name='F-VRDR-ValueSets-RaceCode-Umkumiute_Native_Village'></a>
### Umkumiute_Native_Village `constants`

##### Summary

Umkumiute_Native_Village

<a name='F-VRDR-ValueSets-RaceCode-Umpqua'></a>
### Umpqua `constants`

##### Summary

Umpqua

<a name='F-VRDR-ValueSets-RaceCode-Unalaska'></a>
### Unalaska `constants`

##### Summary

Unalaska

<a name='F-VRDR-ValueSets-RaceCode-Unangan'></a>
### Unangan `constants`

##### Summary

Unangan

<a name='F-VRDR-ValueSets-RaceCode-Unangan_Aleut'></a>
### Unangan_Aleut `constants`

##### Summary

Unangan_Aleut

<a name='F-VRDR-ValueSets-RaceCode-Uncodable'></a>
### Uncodable `constants`

##### Summary

Uncodable

<a name='F-VRDR-ValueSets-RaceCode-United_Arab_Emirates'></a>
### United_Arab_Emirates `constants`

##### Summary

United_Arab_Emirates

<a name='F-VRDR-ValueSets-RaceCode-United_Houma_Nation'></a>
### United_Houma_Nation `constants`

##### Summary

United_Houma_Nation

<a name='F-VRDR-ValueSets-RaceCode-United_Keetoowah_Band_Of_Cherokee'></a>
### United_Keetoowah_Band_Of_Cherokee `constants`

##### Summary

United_Keetoowah_Band_Of_Cherokee

<a name='F-VRDR-ValueSets-RaceCode-Unknown'></a>
### Unknown `constants`

##### Summary

Unknown

<a name='F-VRDR-ValueSets-RaceCode-Upper_Chinook'></a>
### Upper_Chinook `constants`

##### Summary

Upper_Chinook

<a name='F-VRDR-ValueSets-RaceCode-Upper_Lake_Band_Of_Pomo_Indians_Of_Upper_Lake_Rancheria'></a>
### Upper_Lake_Band_Of_Pomo_Indians_Of_Upper_Lake_Rancheria `constants`

##### Summary

Upper_Lake_Band_Of_Pomo_Indians_Of_Upper_Lake_Rancheria

<a name='F-VRDR-ValueSets-RaceCode-Upper_Mattaponi_Tribe'></a>
### Upper_Mattaponi_Tribe `constants`

##### Summary

Upper_Mattaponi_Tribe

<a name='F-VRDR-ValueSets-RaceCode-Upper_Sioux'></a>
### Upper_Sioux `constants`

##### Summary

Upper_Sioux

<a name='F-VRDR-ValueSets-RaceCode-Upper_Skagit'></a>
### Upper_Skagit `constants`

##### Summary

Upper_Skagit

<a name='F-VRDR-ValueSets-RaceCode-Uruguayan'></a>
### Uruguayan `constants`

##### Summary

Uruguayan

<a name='F-VRDR-ValueSets-RaceCode-Ute'></a>
### Ute `constants`

##### Summary

Ute

<a name='F-VRDR-ValueSets-RaceCode-Ute_Mountain'></a>
### Ute_Mountain `constants`

##### Summary

Ute_Mountain

<a name='F-VRDR-ValueSets-RaceCode-Utu_Utu_Gwaitu_Paiute'></a>
### Utu_Utu_Gwaitu_Paiute `constants`

##### Summary

Utu_Utu_Gwaitu_Paiute

<a name='F-VRDR-ValueSets-RaceCode-Venezuelan'></a>
### Venezuelan `constants`

##### Summary

Venezuelan

<a name='F-VRDR-ValueSets-RaceCode-Viejas_Group_Of_Capitan_Grande_Band'></a>
### Viejas_Group_Of_Capitan_Grande_Band `constants`

##### Summary

Viejas_Group_Of_Capitan_Grande_Band

<a name='F-VRDR-ValueSets-RaceCode-Vietnamese'></a>
### Vietnamese `constants`

##### Summary

Vietnamese

<a name='F-VRDR-ValueSets-RaceCode-Village_Of_Afognak'></a>
### Village_Of_Afognak `constants`

##### Summary

Village_Of_Afognak

<a name='F-VRDR-ValueSets-RaceCode-Village_Of_Alakanuk'></a>
### Village_Of_Alakanuk `constants`

##### Summary

Village_Of_Alakanuk

<a name='F-VRDR-ValueSets-RaceCode-Village_Of_Anaktuvuk_Pass'></a>
### Village_Of_Anaktuvuk_Pass `constants`

##### Summary

Village_Of_Anaktuvuk_Pass

<a name='F-VRDR-ValueSets-RaceCode-Village_Of_Aniak'></a>
### Village_Of_Aniak `constants`

##### Summary

Village_Of_Aniak

<a name='F-VRDR-ValueSets-RaceCode-Village_Of_Atmautluak'></a>
### Village_Of_Atmautluak `constants`

##### Summary

Village_Of_Atmautluak

<a name='F-VRDR-ValueSets-RaceCode-Village_Of_Bill_Moores_Slough_Bay'></a>
### Village_Of_Bill_Moores_Slough_Bay `constants`

##### Summary

Village_Of_Bill_Moores_Slough_Bay

<a name='F-VRDR-ValueSets-RaceCode-Village_Of_Chefomak'></a>
### Village_Of_Chefomak `constants`

##### Summary

Village_Of_Chefomak

<a name='F-VRDR-ValueSets-RaceCode-Village_Of_Clarks_Point'></a>
### Village_Of_Clarks_Point `constants`

##### Summary

Village_Of_Clarks_Point

<a name='F-VRDR-ValueSets-RaceCode-Village_Of_Crooked_Creek'></a>
### Village_Of_Crooked_Creek `constants`

##### Summary

Village_Of_Crooked_Creek

<a name='F-VRDR-ValueSets-RaceCode-Village_Of_Dot_Lake'></a>
### Village_Of_Dot_Lake `constants`

##### Summary

Village_Of_Dot_Lake

<a name='F-VRDR-ValueSets-RaceCode-Village_Of_Iliamna'></a>
### Village_Of_Iliamna `constants`

##### Summary

Village_Of_Iliamna

<a name='F-VRDR-ValueSets-RaceCode-Village_Of_Kalskag'></a>
### Village_Of_Kalskag `constants`

##### Summary

Village_Of_Kalskag

<a name='F-VRDR-ValueSets-RaceCode-Village_Of_Kotlik'></a>
### Village_Of_Kotlik `constants`

##### Summary

Village_Of_Kotlik

<a name='F-VRDR-ValueSets-RaceCode-Village_Of_Lower_Kalskag'></a>
### Village_Of_Lower_Kalskag `constants`

##### Summary

Village_Of_Lower_Kalskag

<a name='F-VRDR-ValueSets-RaceCode-Village_Of_Ohogamiut'></a>
### Village_Of_Ohogamiut `constants`

##### Summary

Village_Of_Ohogamiut

<a name='F-VRDR-ValueSets-RaceCode-Village_Of_Old_Harbor'></a>
### Village_Of_Old_Harbor `constants`

##### Summary

Village_Of_Old_Harbor

<a name='F-VRDR-ValueSets-RaceCode-Village_Of_Red_Devil'></a>
### Village_Of_Red_Devil `constants`

##### Summary

Village_Of_Red_Devil

<a name='F-VRDR-ValueSets-RaceCode-Village_Of_Salamatoff'></a>
### Village_Of_Salamatoff `constants`

##### Summary

Village_Of_Salamatoff

<a name='F-VRDR-ValueSets-RaceCode-Village_Of_Sleetmute'></a>
### Village_Of_Sleetmute `constants`

##### Summary

Village_Of_Sleetmute

<a name='F-VRDR-ValueSets-RaceCode-Village_Of_Solomon'></a>
### Village_Of_Solomon `constants`

##### Summary

Village_Of_Solomon

<a name='F-VRDR-ValueSets-RaceCode-Village_Of_Stony_River'></a>
### Village_Of_Stony_River `constants`

##### Summary

Village_Of_Stony_River

<a name='F-VRDR-ValueSets-RaceCode-Village_Of_Venetie'></a>
### Village_Of_Venetie `constants`

##### Summary

Village_Of_Venetie

<a name='F-VRDR-ValueSets-RaceCode-Village_Of_Wainwright'></a>
### Village_Of_Wainwright `constants`

##### Summary

Village_Of_Wainwright

<a name='F-VRDR-ValueSets-RaceCode-Village_Of_Wales'></a>
### Village_Of_Wales `constants`

##### Summary

Village_Of_Wales

<a name='F-VRDR-ValueSets-RaceCode-Village_Of_White_Mountain'></a>
### Village_Of_White_Mountain `constants`

##### Summary

Village_Of_White_Mountain

<a name='F-VRDR-ValueSets-RaceCode-Village_Ofkaltag'></a>
### Village_Ofkaltag `constants`

##### Summary

Village_Ofkaltag

<a name='F-VRDR-ValueSets-RaceCode-Waccamaw_Siouan'></a>
### Waccamaw_Siouan `constants`

##### Summary

Waccamaw_Siouan

<a name='F-VRDR-ValueSets-RaceCode-Waco'></a>
### Waco `constants`

##### Summary

Waco

<a name='F-VRDR-ValueSets-RaceCode-Wahpekute_Sioux'></a>
### Wahpekute_Sioux `constants`

##### Summary

Wahpekute_Sioux

<a name='F-VRDR-ValueSets-RaceCode-Wahpeton_Sioux'></a>
### Wahpeton_Sioux `constants`

##### Summary

Wahpeton_Sioux

<a name='F-VRDR-ValueSets-RaceCode-Wailaki'></a>
### Wailaki `constants`

##### Summary

Wailaki

<a name='F-VRDR-ValueSets-RaceCode-Wakiakum_Chinook'></a>
### Wakiakum_Chinook `constants`

##### Summary

Wakiakum_Chinook

<a name='F-VRDR-ValueSets-RaceCode-Walker_River'></a>
### Walker_River `constants`

##### Summary

Walker_River

<a name='F-VRDR-ValueSets-RaceCode-Wallawalla'></a>
### Wallawalla `constants`

##### Summary

Wallawalla

<a name='F-VRDR-ValueSets-RaceCode-Wampanoag'></a>
### Wampanoag `constants`

##### Summary

Wampanoag

<a name='F-VRDR-ValueSets-RaceCode-Wappo'></a>
### Wappo `constants`

##### Summary

Wappo

<a name='F-VRDR-ValueSets-RaceCode-Warm_Springs'></a>
### Warm_Springs `constants`

##### Summary

Warm_Springs

<a name='F-VRDR-ValueSets-RaceCode-Wascopum'></a>
### Wascopum `constants`

##### Summary

Wascopum

<a name='F-VRDR-ValueSets-RaceCode-Washoe'></a>
### Washoe `constants`

##### Summary

Washoe

<a name='F-VRDR-ValueSets-RaceCode-Wazhaza_Sioux'></a>
### Wazhaza_Sioux `constants`

##### Summary

Wazhaza_Sioux

<a name='F-VRDR-ValueSets-RaceCode-Wells_Band'></a>
### Wells_Band `constants`

##### Summary

Wells_Band

<a name='F-VRDR-ValueSets-RaceCode-Wenatchee'></a>
### Wenatchee `constants`

##### Summary

Wenatchee

<a name='F-VRDR-ValueSets-RaceCode-Wesort'></a>
### Wesort `constants`

##### Summary

Wesort

<a name='F-VRDR-ValueSets-RaceCode-West_Indies'></a>
### West_Indies `constants`

##### Summary

West_Indies

<a name='F-VRDR-ValueSets-RaceCode-Western_Cherokee'></a>
### Western_Cherokee `constants`

##### Summary

Western_Cherokee

<a name='F-VRDR-ValueSets-RaceCode-Western_Chickahominy'></a>
### Western_Chickahominy `constants`

##### Summary

Western_Chickahominy

<a name='F-VRDR-ValueSets-RaceCode-Whello'></a>
### Whello `constants`

##### Summary

Whello

<a name='F-VRDR-ValueSets-RaceCode-Whilkut'></a>
### Whilkut `constants`

##### Summary

Whilkut

<a name='F-VRDR-ValueSets-RaceCode-White'></a>
### White `constants`

##### Summary

White

<a name='F-VRDR-ValueSets-RaceCode-White_Checkbox'></a>
### White_Checkbox `constants`

##### Summary

White_Checkbox

<a name='F-VRDR-ValueSets-RaceCode-White_Earth'></a>
### White_Earth `constants`

##### Summary

White_Earth

<a name='F-VRDR-ValueSets-RaceCode-White_Mountain_Apache'></a>
### White_Mountain_Apache `constants`

##### Summary

White_Mountain_Apache

<a name='F-VRDR-ValueSets-RaceCode-White_Mountain_Inupiat'></a>
### White_Mountain_Inupiat `constants`

##### Summary

White_Mountain_Inupiat

<a name='F-VRDR-ValueSets-RaceCode-White_River_Band_Of_The_Chickamauga_Cherokee'></a>
### White_River_Band_Of_The_Chickamauga_Cherokee `constants`

##### Summary

White_River_Band_Of_The_Chickamauga_Cherokee

<a name='F-VRDR-ValueSets-RaceCode-Wichita'></a>
### Wichita `constants`

##### Summary

Wichita

<a name='F-VRDR-ValueSets-RaceCode-Wicomico'></a>
### Wicomico `constants`

##### Summary

Wicomico

<a name='F-VRDR-ValueSets-RaceCode-Wikchamni'></a>
### Wikchamni `constants`

##### Summary

Wikchamni

<a name='F-VRDR-ValueSets-RaceCode-Willapa_Chinook'></a>
### Willapa_Chinook `constants`

##### Summary

Willapa_Chinook

<a name='F-VRDR-ValueSets-RaceCode-Wilono'></a>
### Wilono `constants`

##### Summary

Wilono

<a name='F-VRDR-ValueSets-RaceCode-Wind_River'></a>
### Wind_River `constants`

##### Summary

Wind_River

<a name='F-VRDR-ValueSets-RaceCode-Wind_River_Arapahoe'></a>
### Wind_River_Arapahoe `constants`

##### Summary

Wind_River_Arapahoe

<a name='F-VRDR-ValueSets-RaceCode-Wind_River_Shoshone'></a>
### Wind_River_Shoshone `constants`

##### Summary

Wind_River_Shoshone

<a name='F-VRDR-ValueSets-RaceCode-Winnebago'></a>
### Winnebago `constants`

##### Summary

Winnebago

<a name='F-VRDR-ValueSets-RaceCode-Winnemucca'></a>
### Winnemucca `constants`

##### Summary

Winnemucca

<a name='F-VRDR-ValueSets-RaceCode-Wintun'></a>
### Wintun `constants`

##### Summary

Wintun

<a name='F-VRDR-ValueSets-RaceCode-Wisconsin_Potawatomi'></a>
### Wisconsin_Potawatomi `constants`

##### Summary

Wisconsin_Potawatomi

<a name='F-VRDR-ValueSets-RaceCode-Wiseman'></a>
### Wiseman `constants`

##### Summary

Wiseman

<a name='F-VRDR-ValueSets-RaceCode-Wishram'></a>
### Wishram `constants`

##### Summary

Wishram

<a name='F-VRDR-ValueSets-RaceCode-Wiyot'></a>
### Wiyot `constants`

##### Summary

Wiyot

<a name='F-VRDR-ValueSets-RaceCode-Woodsfords_Community'></a>
### Woodsfords_Community `constants`

##### Summary

Woodsfords_Community

<a name='F-VRDR-ValueSets-RaceCode-Wrangell_Cooperative_Association'></a>
### Wrangell_Cooperative_Association `constants`

##### Summary

Wrangell_Cooperative_Association

<a name='F-VRDR-ValueSets-RaceCode-Wyandotte'></a>
### Wyandotte `constants`

##### Summary

Wyandotte

<a name='F-VRDR-ValueSets-RaceCode-Yahooskin_Band_Of_Snake'></a>
### Yahooskin_Band_Of_Snake `constants`

##### Summary

Yahooskin_Band_Of_Snake

<a name='F-VRDR-ValueSets-RaceCode-Yakama'></a>
### Yakama `constants`

##### Summary

Yakama

<a name='F-VRDR-ValueSets-RaceCode-Yakama_Cowlitz'></a>
### Yakama_Cowlitz `constants`

##### Summary

Yakama_Cowlitz

<a name='F-VRDR-ValueSets-RaceCode-Yakutat_Tlingit_Tribe'></a>
### Yakutat_Tlingit_Tribe `constants`

##### Summary

Yakutat_Tlingit_Tribe

<a name='F-VRDR-ValueSets-RaceCode-Yana'></a>
### Yana `constants`

##### Summary

Yana

<a name='F-VRDR-ValueSets-RaceCode-Yankton_Sioux'></a>
### Yankton_Sioux `constants`

##### Summary

Yankton_Sioux

<a name='F-VRDR-ValueSets-RaceCode-Yanktonai_Sioux'></a>
### Yanktonai_Sioux `constants`

##### Summary

Yanktonai_Sioux

<a name='F-VRDR-ValueSets-RaceCode-Yapese'></a>
### Yapese `constants`

##### Summary

Yapese

<a name='F-VRDR-ValueSets-RaceCode-Yaqui'></a>
### Yaqui `constants`

##### Summary

Yaqui

<a name='F-VRDR-ValueSets-RaceCode-Yavapai_Apache'></a>
### Yavapai_Apache `constants`

##### Summary

Yavapai_Apache

<a name='F-VRDR-ValueSets-RaceCode-Yavapaiprescott_Tribe_Of_The_Yavapai_Reservation'></a>
### Yavapaiprescott_Tribe_Of_The_Yavapai_Reservation `constants`

##### Summary

Yavapaiprescott_Tribe_Of_The_Yavapai_Reservation

<a name='F-VRDR-ValueSets-RaceCode-Yello'></a>
### Yello `constants`

##### Summary

Yello

<a name='F-VRDR-ValueSets-RaceCode-Yerington_Paiute'></a>
### Yerington_Paiute `constants`

##### Summary

Yerington_Paiute

<a name='F-VRDR-ValueSets-RaceCode-Yokuts'></a>
### Yokuts `constants`

##### Summary

Yokuts

<a name='F-VRDR-ValueSets-RaceCode-Yomba'></a>
### Yomba `constants`

##### Summary

Yomba

<a name='F-VRDR-ValueSets-RaceCode-Ysleta_Del_Sur_Pueblo_Of_Texas'></a>
### Ysleta_Del_Sur_Pueblo_Of_Texas `constants`

##### Summary

Ysleta_Del_Sur_Pueblo_Of_Texas

<a name='F-VRDR-ValueSets-RaceCode-Yuchi'></a>
### Yuchi `constants`

##### Summary

Yuchi

<a name='F-VRDR-ValueSets-RaceCode-Yuki'></a>
### Yuki `constants`

##### Summary

Yuki

<a name='F-VRDR-ValueSets-RaceCode-Yuman'></a>
### Yuman `constants`

##### Summary

Yuman

<a name='F-VRDR-ValueSets-RaceCode-Yupiit_Of_Andreafski'></a>
### Yupiit_Of_Andreafski `constants`

##### Summary

Yupiit_Of_Andreafski

<a name='F-VRDR-ValueSets-RaceCode-Yupik'></a>
### Yupik `constants`

##### Summary

Yupik

<a name='F-VRDR-ValueSets-RaceCode-Yupik_Eskimo'></a>
### Yupik_Eskimo `constants`

##### Summary

Yupik_Eskimo

<a name='F-VRDR-ValueSets-RaceCode-Yurok'></a>
### Yurok `constants`

##### Summary

Yurok

<a name='F-VRDR-ValueSets-RaceCode-Zaire'></a>
### Zaire `constants`

##### Summary

Zaire

<a name='F-VRDR-ValueSets-RaceCode-Zia'></a>
### Zia `constants`

##### Summary

Zia

<a name='F-VRDR-ValueSets-RaceCode-Zuni'></a>
### Zuni `constants`

##### Summary

Zuni

<a name='T-VRDR-Mappings-RaceMissingValueReason'></a>
## RaceMissingValueReason `type`

##### Namespace

VRDR.Mappings

##### Summary

Mappings for RaceMissingValueReason

<a name='T-VRDR-ValueSets-RaceMissingValueReason'></a>
## RaceMissingValueReason `type`

##### Namespace

VRDR.ValueSets

##### Summary

RaceMissingValueReason

<a name='F-VRDR-Mappings-RaceMissingValueReason-FHIRToIJE'></a>
### FHIRToIJE `constants`

##### Summary

FHIR -> IJE Mapping for RaceMissingValueReason

<a name='F-VRDR-Mappings-RaceMissingValueReason-IJEToFHIR'></a>
### IJEToFHIR `constants`

##### Summary

IJE -> FHIR Mapping for RaceMissingValueReason

<a name='F-VRDR-ValueSets-RaceMissingValueReason-Codes'></a>
### Codes `constants`

##### Summary

Codes

<a name='F-VRDR-ValueSets-RaceMissingValueReason-Not_Obtainable'></a>
### Not_Obtainable `constants`

##### Summary

Not_Obtainable

<a name='F-VRDR-ValueSets-RaceMissingValueReason-Refused'></a>
### Refused `constants`

##### Summary

Refused

<a name='F-VRDR-ValueSets-RaceMissingValueReason-Sought_But_Unknown'></a>
### Sought_But_Unknown `constants`

##### Summary

Sought_But_Unknown

<a name='T-VRDR-Mappings-RaceRecode40'></a>
## RaceRecode40 `type`

##### Namespace

VRDR.Mappings

##### Summary

Mappings for RaceRecode40

<a name='T-VRDR-ValueSets-RaceRecode40'></a>
## RaceRecode40 `type`

##### Namespace

VRDR.ValueSets

##### Summary

RaceRecode40

<a name='F-VRDR-Mappings-RaceRecode40-FHIRToIJE'></a>
### FHIRToIJE `constants`

##### Summary

FHIR -> IJE Mapping for RaceRecode40

<a name='F-VRDR-Mappings-RaceRecode40-IJEToFHIR'></a>
### IJEToFHIR `constants`

##### Summary

IJE -> FHIR Mapping for RaceRecode40

<a name='F-VRDR-ValueSets-RaceRecode40-Aian_And_Asian'></a>
### Aian_And_Asian `constants`

##### Summary

Aian_And_Asian

<a name='F-VRDR-ValueSets-RaceRecode40-Aian_And_Nhopi'></a>
### Aian_And_Nhopi `constants`

##### Summary

Aian_And_Nhopi

<a name='F-VRDR-ValueSets-RaceRecode40-Aian_And_Nhopi_2'></a>
### Aian_And_Nhopi_2 `constants`

##### Summary

Aian_And_Nhopi_2

<a name='F-VRDR-ValueSets-RaceRecode40-Aian_Asian_And_Nhopi'></a>
### Aian_Asian_And_Nhopi `constants`

##### Summary

Aian_Asian_And_Nhopi

<a name='F-VRDR-ValueSets-RaceRecode40-Aian_Asian_And_White'></a>
### Aian_Asian_And_White `constants`

##### Summary

Aian_Asian_And_White

<a name='F-VRDR-ValueSets-RaceRecode40-Aian_Asian_Nhopi_And_White'></a>
### Aian_Asian_Nhopi_And_White `constants`

##### Summary

Aian_Asian_Nhopi_And_White

<a name='F-VRDR-ValueSets-RaceRecode40-Aian_Nhopi_And_White'></a>
### Aian_Nhopi_And_White `constants`

##### Summary

Aian_Nhopi_And_White

<a name='F-VRDR-ValueSets-RaceRecode40-American_Indian_Or_Alaskan_Native_Aian'></a>
### American_Indian_Or_Alaskan_Native_Aian `constants`

##### Summary

American_Indian_Or_Alaskan_Native_Aian

<a name='F-VRDR-ValueSets-RaceRecode40-Asian_And_Nhopi'></a>
### Asian_And_Nhopi `constants`

##### Summary

Asian_And_Nhopi

<a name='F-VRDR-ValueSets-RaceRecode40-Asian_And_White'></a>
### Asian_And_White `constants`

##### Summary

Asian_And_White

<a name='F-VRDR-ValueSets-RaceRecode40-Asian_Indian'></a>
### Asian_Indian `constants`

##### Summary

Asian_Indian

<a name='F-VRDR-ValueSets-RaceRecode40-Asian_Nhopi_And_White'></a>
### Asian_Nhopi_And_White `constants`

##### Summary

Asian_Nhopi_And_White

<a name='F-VRDR-ValueSets-RaceRecode40-Black'></a>
### Black `constants`

##### Summary

Black

<a name='F-VRDR-ValueSets-RaceRecode40-Black_Aian_And_Asian'></a>
### Black_Aian_And_Asian `constants`

##### Summary

Black_Aian_And_Asian

<a name='F-VRDR-ValueSets-RaceRecode40-Black_Aian_And_Nhopi'></a>
### Black_Aian_And_Nhopi `constants`

##### Summary

Black_Aian_And_Nhopi

<a name='F-VRDR-ValueSets-RaceRecode40-Black_Aian_And_White'></a>
### Black_Aian_And_White `constants`

##### Summary

Black_Aian_And_White

<a name='F-VRDR-ValueSets-RaceRecode40-Black_Aian_Asian_And_Nhopi'></a>
### Black_Aian_Asian_And_Nhopi `constants`

##### Summary

Black_Aian_Asian_And_Nhopi

<a name='F-VRDR-ValueSets-RaceRecode40-Black_Aian_Asian_And_White'></a>
### Black_Aian_Asian_And_White `constants`

##### Summary

Black_Aian_Asian_And_White

<a name='F-VRDR-ValueSets-RaceRecode40-Black_Aian_Asian_Nhopi_And_White'></a>
### Black_Aian_Asian_Nhopi_And_White `constants`

##### Summary

Black_Aian_Asian_Nhopi_And_White

<a name='F-VRDR-ValueSets-RaceRecode40-Black_Aian_Nhopi_And_White'></a>
### Black_Aian_Nhopi_And_White `constants`

##### Summary

Black_Aian_Nhopi_And_White

<a name='F-VRDR-ValueSets-RaceRecode40-Black_And_Aian'></a>
### Black_And_Aian `constants`

##### Summary

Black_And_Aian

<a name='F-VRDR-ValueSets-RaceRecode40-Black_And_Asian'></a>
### Black_And_Asian `constants`

##### Summary

Black_And_Asian

<a name='F-VRDR-ValueSets-RaceRecode40-Black_And_White'></a>
### Black_And_White `constants`

##### Summary

Black_And_White

<a name='F-VRDR-ValueSets-RaceRecode40-Black_Asian_And_Nhopi'></a>
### Black_Asian_And_Nhopi `constants`

##### Summary

Black_Asian_And_Nhopi

<a name='F-VRDR-ValueSets-RaceRecode40-Black_Asian_And_White'></a>
### Black_Asian_And_White `constants`

##### Summary

Black_Asian_And_White

<a name='F-VRDR-ValueSets-RaceRecode40-Black_Asian_Nhopi_And_White'></a>
### Black_Asian_Nhopi_And_White `constants`

##### Summary

Black_Asian_Nhopi_And_White

<a name='F-VRDR-ValueSets-RaceRecode40-Black_Nhopi_And_White'></a>
### Black_Nhopi_And_White `constants`

##### Summary

Black_Nhopi_And_White

<a name='F-VRDR-ValueSets-RaceRecode40-Chinese'></a>
### Chinese `constants`

##### Summary

Chinese

<a name='F-VRDR-ValueSets-RaceRecode40-Codes'></a>
### Codes `constants`

##### Summary

Codes

<a name='F-VRDR-ValueSets-RaceRecode40-Filipino'></a>
### Filipino `constants`

##### Summary

Filipino

<a name='F-VRDR-ValueSets-RaceRecode40-Guamanian'></a>
### Guamanian `constants`

##### Summary

Guamanian

<a name='F-VRDR-ValueSets-RaceRecode40-Hawaiian'></a>
### Hawaiian `constants`

##### Summary

Hawaiian

<a name='F-VRDR-ValueSets-RaceRecode40-Japanese'></a>
### Japanese `constants`

##### Summary

Japanese

<a name='F-VRDR-ValueSets-RaceRecode40-Korean'></a>
### Korean `constants`

##### Summary

Korean

<a name='F-VRDR-ValueSets-RaceRecode40-Nhopi_And_White'></a>
### Nhopi_And_White `constants`

##### Summary

Nhopi_And_White

<a name='F-VRDR-ValueSets-RaceRecode40-Nhopi_And_White_2'></a>
### Nhopi_And_White_2 `constants`

##### Summary

Nhopi_And_White_2

<a name='F-VRDR-ValueSets-RaceRecode40-Other_Or_Multiple_Asian'></a>
### Other_Or_Multiple_Asian `constants`

##### Summary

Other_Or_Multiple_Asian

<a name='F-VRDR-ValueSets-RaceRecode40-Other_Or_Multiple_Pacific_Islander'></a>
### Other_Or_Multiple_Pacific_Islander `constants`

##### Summary

Other_Or_Multiple_Pacific_Islander

<a name='F-VRDR-ValueSets-RaceRecode40-Samoan'></a>
### Samoan `constants`

##### Summary

Samoan

<a name='F-VRDR-ValueSets-RaceRecode40-Unknown_And_Other_Race'></a>
### Unknown_And_Other_Race `constants`

##### Summary

Unknown_And_Other_Race

<a name='F-VRDR-ValueSets-RaceRecode40-Vietnamese'></a>
### Vietnamese `constants`

##### Summary

Vietnamese

<a name='F-VRDR-ValueSets-RaceRecode40-White'></a>
### White `constants`

##### Summary

White

<a name='T-VRDR-Mappings-ReplaceStatus'></a>
## ReplaceStatus `type`

##### Namespace

VRDR.Mappings

##### Summary

Mappings for ReplaceStatus

<a name='T-VRDR-ValueSets-ReplaceStatus'></a>
## ReplaceStatus `type`

##### Namespace

VRDR.ValueSets

##### Summary

ReplaceStatus

<a name='F-VRDR-Mappings-ReplaceStatus-FHIRToIJE'></a>
### FHIRToIJE `constants`

##### Summary

FHIR -> IJE Mapping for ReplaceStatus

<a name='F-VRDR-Mappings-ReplaceStatus-IJEToFHIR'></a>
### IJEToFHIR `constants`

##### Summary

IJE -> FHIR Mapping for ReplaceStatus

<a name='F-VRDR-ValueSets-ReplaceStatus-Codes'></a>
### Codes `constants`

##### Summary

Codes

<a name='F-VRDR-ValueSets-ReplaceStatus-Original_Record'></a>
### Original_Record `constants`

##### Summary

Original_Record

<a name='F-VRDR-ValueSets-ReplaceStatus-Updated_Record'></a>
### Updated_Record `constants`

##### Summary

Updated_Record

<a name='F-VRDR-ValueSets-ReplaceStatus-Updated_Record_Not_For_Nchs'></a>
### Updated_Record_Not_For_Nchs `constants`

##### Summary

Updated_Record_Not_For_Nchs

<a name='T-VRDR-Mappings-SpouseAlive'></a>
## SpouseAlive `type`

##### Namespace

VRDR.Mappings

##### Summary

Mappings for SpouseAlive

<a name='T-VRDR-ValueSets-SpouseAlive'></a>
## SpouseAlive `type`

##### Namespace

VRDR.ValueSets

##### Summary

SpouseAlive

<a name='F-VRDR-Mappings-SpouseAlive-FHIRToIJE'></a>
### FHIRToIJE `constants`

##### Summary

FHIR -> IJE Mapping for SpouseAlive

<a name='F-VRDR-Mappings-SpouseAlive-IJEToFHIR'></a>
### IJEToFHIR `constants`

##### Summary

IJE -> FHIR Mapping for SpouseAlive

<a name='F-VRDR-ValueSets-SpouseAlive-Codes'></a>
### Codes `constants`

##### Summary

Codes

<a name='F-VRDR-ValueSets-SpouseAlive-No'></a>
### No `constants`

##### Summary

No

<a name='F-VRDR-ValueSets-SpouseAlive-Not_Applicable'></a>
### Not_Applicable `constants`

##### Summary

Not_Applicable

<a name='F-VRDR-ValueSets-SpouseAlive-Unknown'></a>
### Unknown `constants`

##### Summary

Unknown

<a name='F-VRDR-ValueSets-SpouseAlive-Yes'></a>
### Yes `constants`

##### Summary

Yes

<a name='T-VRDR-Mappings-SystemReject'></a>
## SystemReject `type`

##### Namespace

VRDR.Mappings

##### Summary

Mappings for SystemReject

<a name='F-VRDR-Mappings-SystemReject-FHIRToIJE'></a>
### FHIRToIJE `constants`

##### Summary

FHIR -> IJE Mapping for SystemReject

<a name='F-VRDR-Mappings-SystemReject-IJEToFHIR'></a>
### IJEToFHIR `constants`

##### Summary

IJE -> FHIR Mapping for SystemReject

<a name='T-VRDR-IJEMortality-TRXHelper'></a>
## TRXHelper `type`

##### Namespace

VRDR.IJEMortality

##### Summary

Helper class to contain properties for setting TRX-only fields that have no mapping in IJE when creating coding response records

<a name='M-VRDR-IJEMortality-TRXHelper-#ctor-VRDR-DeathRecord-'></a>
### #ctor() `constructor`

##### Summary

Constructor for class to contain properties for setting TRX-only fields that have no mapping in IJE when creating coding response records

##### Parameters

This constructor has no parameters.

<a name='P-VRDR-IJEMortality-TRXHelper-CS'></a>
### CS `property`

##### Summary

coder status - Property for setting the CodingStatus of a Cause of Death Coding Submission

<a name='P-VRDR-IJEMortality-TRXHelper-SHIP'></a>
### SHIP `property`

##### Summary

shipment number - Property for setting the ShipmentNumber of a Cause of Death Coding Submission

<a name='T-VRDR-Mappings-TransaxConversion'></a>
## TransaxConversion `type`

##### Namespace

VRDR.Mappings

##### Summary

Mappings for TransaxConversion

<a name='T-VRDR-ValueSets-TransaxConversion'></a>
## TransaxConversion `type`

##### Namespace

VRDR.ValueSets

##### Summary

TransaxConversion

<a name='F-VRDR-Mappings-TransaxConversion-FHIRToIJE'></a>
### FHIRToIJE `constants`

##### Summary

FHIR -> IJE Mapping for TransaxConversion

<a name='F-VRDR-Mappings-TransaxConversion-IJEToFHIR'></a>
### IJEToFHIR `constants`

##### Summary

IJE -> FHIR Mapping for TransaxConversion

<a name='F-VRDR-ValueSets-TransaxConversion-Artificial_Code_Conversion_No_Other_Action'></a>
### Artificial_Code_Conversion_No_Other_Action `constants`

##### Summary

Artificial_Code_Conversion_No_Other_Action

<a name='F-VRDR-ValueSets-TransaxConversion-Codes'></a>
### Codes `constants`

##### Summary

Codes

<a name='F-VRDR-ValueSets-TransaxConversion-Conversion_Using_Ambivalent_Table_Entries'></a>
### Conversion_Using_Ambivalent_Table_Entries `constants`

##### Summary

Conversion_Using_Ambivalent_Table_Entries

<a name='F-VRDR-ValueSets-TransaxConversion-Conversion_Using_Non_Ambivalent_Table_Entries'></a>
### Conversion_Using_Non_Ambivalent_Table_Entries `constants`

##### Summary

Conversion_Using_Non_Ambivalent_Table_Entries

<a name='F-VRDR-ValueSets-TransaxConversion-Duplicate_Entity_Axis_Codes_Deleted_No_Other_Action_Involved'></a>
### Duplicate_Entity_Axis_Codes_Deleted_No_Other_Action_Involved `constants`

##### Summary

Duplicate_Entity_Axis_Codes_Deleted_No_Other_Action_Involved

<a name='T-VRDR-Mappings-TransportationIncidentRole'></a>
## TransportationIncidentRole `type`

##### Namespace

VRDR.Mappings

##### Summary

Mappings for TransportationIncidentRole

<a name='T-VRDR-ValueSets-TransportationIncidentRole'></a>
## TransportationIncidentRole `type`

##### Namespace

VRDR.ValueSets

##### Summary

TransportationIncidentRole

<a name='F-VRDR-Mappings-TransportationIncidentRole-FHIRToIJE'></a>
### FHIRToIJE `constants`

##### Summary

FHIR -> IJE Mapping for TransportationIncidentRole

<a name='F-VRDR-Mappings-TransportationIncidentRole-IJEToFHIR'></a>
### IJEToFHIR `constants`

##### Summary

IJE -> FHIR Mapping for TransportationIncidentRole

<a name='F-VRDR-ValueSets-TransportationIncidentRole-Codes'></a>
### Codes `constants`

##### Summary

Codes

<a name='F-VRDR-ValueSets-TransportationIncidentRole-Not_Applicable'></a>
### Not_Applicable `constants`

##### Summary

Not_Applicable

<a name='F-VRDR-ValueSets-TransportationIncidentRole-Other'></a>
### Other `constants`

##### Summary

Other

<a name='F-VRDR-ValueSets-TransportationIncidentRole-Passenger'></a>
### Passenger `constants`

##### Summary

Passenger

<a name='F-VRDR-ValueSets-TransportationIncidentRole-Pedestrian'></a>
### Pedestrian `constants`

##### Summary

Pedestrian

<a name='F-VRDR-ValueSets-TransportationIncidentRole-Unknown'></a>
### Unknown `constants`

##### Summary

Unknown

<a name='F-VRDR-ValueSets-TransportationIncidentRole-Vehicle_Driver'></a>
### Vehicle_Driver `constants`

##### Summary

Vehicle_Driver

<a name='T-VRDR-Property-Types'></a>
## Types `type`

##### Namespace

VRDR.Property

##### Summary

Enum for describing the property type.

<a name='F-VRDR-Property-Types-Bool'></a>
### Bool `constants`

##### Summary

Parameter is a bool.

<a name='F-VRDR-Property-Types-Dictionary'></a>
### Dictionary `constants`

##### Summary

Parameter is a Dictionary.

<a name='F-VRDR-Property-Types-String'></a>
### String `constants`

##### Summary

Parameter is a string.

<a name='F-VRDR-Property-Types-StringArr'></a>
### StringArr `constants`

##### Summary

Parameter is an array of strings.

<a name='F-VRDR-Property-Types-StringDateTime'></a>
### StringDateTime `constants`

##### Summary

Parameter is like a string, but should be treated as a date and time.

<a name='F-VRDR-Property-Types-Tuple4Arr'></a>
### Tuple4Arr `constants`

##### Summary

Parameter is an array of 4-Tuples, specifically for entity axis codes.

<a name='F-VRDR-Property-Types-TupleArr'></a>
### TupleArr `constants`

##### Summary

Parameter is an array of Tuples.

<a name='F-VRDR-Property-Types-TupleCOD'></a>
### TupleCOD `constants`

##### Summary

Parameter is an array of Tuples, specifically for CausesOfDeath.

<a name='F-VRDR-Property-Types-UInt32'></a>
### UInt32 `constants`

##### Summary

Parameter is an unsigned integer.

<a name='T-VRDR-Mappings-UnitsOfAge'></a>
## UnitsOfAge `type`

##### Namespace

VRDR.Mappings

##### Summary

Mappings for UnitsOfAge

<a name='T-VRDR-ValueSets-UnitsOfAge'></a>
## UnitsOfAge `type`

##### Namespace

VRDR.ValueSets

##### Summary

UnitsOfAge

<a name='F-VRDR-Mappings-UnitsOfAge-FHIRToIJE'></a>
### FHIRToIJE `constants`

##### Summary

FHIR -> IJE Mapping for UnitsOfAge

<a name='F-VRDR-Mappings-UnitsOfAge-IJEToFHIR'></a>
### IJEToFHIR `constants`

##### Summary

IJE -> FHIR Mapping for UnitsOfAge

<a name='F-VRDR-ValueSets-UnitsOfAge-Codes'></a>
### Codes `constants`

##### Summary

Codes

<a name='F-VRDR-ValueSets-UnitsOfAge-Days'></a>
### Days `constants`

##### Summary

Days

<a name='F-VRDR-ValueSets-UnitsOfAge-Hours'></a>
### Hours `constants`

##### Summary

Hours

<a name='F-VRDR-ValueSets-UnitsOfAge-Minutes'></a>
### Minutes `constants`

##### Summary

Minutes

<a name='F-VRDR-ValueSets-UnitsOfAge-Months'></a>
### Months `constants`

##### Summary

Months

<a name='F-VRDR-ValueSets-UnitsOfAge-Unknown'></a>
### Unknown `constants`

##### Summary

Unknown

<a name='F-VRDR-ValueSets-UnitsOfAge-Years'></a>
### Years `constants`

##### Summary

Years

<a name='T-VRDR-ValueSets'></a>
## ValueSets `type`

##### Namespace

VRDR

##### Summary

ValueSet Helpers

<a name='T-VRDR-Mappings-YesNoNotApplicable'></a>
## YesNoNotApplicable `type`

##### Namespace

VRDR.Mappings

##### Summary

Mappings for YesNoNotApplicable

<a name='F-VRDR-Mappings-YesNoNotApplicable-FHIRToIJE'></a>
### FHIRToIJE `constants`

##### Summary

FHIR -> IJE Mapping for YesNoNotApplicable

<a name='F-VRDR-Mappings-YesNoNotApplicable-IJEToFHIR'></a>
### IJEToFHIR `constants`

##### Summary

IJE -> FHIR Mapping for YesNoNotApplicable

<a name='T-VRDR-Mappings-YesNoUnknown'></a>
## YesNoUnknown `type`

##### Namespace

VRDR.Mappings

##### Summary

Mappings for YesNoUnknown

<a name='T-VRDR-ValueSets-YesNoUnknown'></a>
## YesNoUnknown `type`

##### Namespace

VRDR.ValueSets

##### Summary

YesNoUnknown

<a name='F-VRDR-Mappings-YesNoUnknown-FHIRToIJE'></a>
### FHIRToIJE `constants`

##### Summary

FHIR -> IJE Mapping for YesNoUnknown

<a name='F-VRDR-Mappings-YesNoUnknown-IJEToFHIR'></a>
### IJEToFHIR `constants`

##### Summary

IJE -> FHIR Mapping for YesNoUnknown

<a name='F-VRDR-ValueSets-YesNoUnknown-Codes'></a>
### Codes `constants`

##### Summary

Codes

<a name='F-VRDR-ValueSets-YesNoUnknown-No'></a>
### No `constants`

##### Summary

No

<a name='F-VRDR-ValueSets-YesNoUnknown-Unknown'></a>
### Unknown `constants`

##### Summary

Unknown

<a name='F-VRDR-ValueSets-YesNoUnknown-Yes'></a>
### Yes `constants`

##### Summary

Yes

<a name='T-VRDR-Mappings-YesNoUnknownNotApplicable'></a>
## YesNoUnknownNotApplicable `type`

##### Namespace

VRDR.Mappings

##### Summary

Mappings for YesNoUnknownNotApplicable

<a name='T-VRDR-ValueSets-YesNoUnknownNotApplicable'></a>
## YesNoUnknownNotApplicable `type`

##### Namespace

VRDR.ValueSets

##### Summary

YesNoUnknownNotApplicable

<a name='F-VRDR-Mappings-YesNoUnknownNotApplicable-FHIRToIJE'></a>
### FHIRToIJE `constants`

##### Summary

FHIR -> IJE Mapping for YesNoUnknownNotApplicable

<a name='F-VRDR-Mappings-YesNoUnknownNotApplicable-IJEToFHIR'></a>
### IJEToFHIR `constants`

##### Summary

IJE -> FHIR Mapping for YesNoUnknownNotApplicable

<a name='F-VRDR-ValueSets-YesNoUnknownNotApplicable-Codes'></a>
### Codes `constants`

##### Summary

Codes

<a name='F-VRDR-ValueSets-YesNoUnknownNotApplicable-No'></a>
### No `constants`

##### Summary

No

<a name='F-VRDR-ValueSets-YesNoUnknownNotApplicable-Not_Applicable'></a>
### Not_Applicable `constants`

##### Summary

Not_Applicable

<a name='F-VRDR-ValueSets-YesNoUnknownNotApplicable-Unknown'></a>
### Unknown `constants`

##### Summary

Unknown

<a name='F-VRDR-ValueSets-YesNoUnknownNotApplicable-Yes'></a>
### Yes `constants`

##### Summary

Yes
