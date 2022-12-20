<a name='assembly'></a>
# Documentation of Public Methods of DeathRecord and IJEMortality
## Creating this File
Creation of this file is determined by the DocumentationFile tag in the .csproj file, and the package reference to Vsxmd.
Normally, have the .xml file generated and the Vsxmd package reference uncommented.
For releases, have the md file generated, and the vsxmd package reference commented.
Run 'dotnet build'.
Some editing is required.

## Contents

- [DeathRecord](#T-VRDR-DeathRecord 'VRDR.DeathRecord')
  - [#ctor()](#M-VRDR-DeathRecord-#ctor 'VRDR.DeathRecord.#ctor')
  - [#ctor(record,permissive)](#M-VRDR-DeathRecord-#ctor-System-String,System-Boolean- 'VRDR.DeathRecord.#ctor(System.String,System.Boolean)')
  - [#ctor(bundle)](#M-VRDR-DeathRecord-#ctor-Hl7-Fhir-Model-Bundle- 'VRDR.DeathRecord.#ctor(Hl7.Fhir.Model.Bundle)')
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
  - [ToDescription()](#M-VRDR-DeathRecord-ToDescription 'VRDR.DeathRecord.ToDescription')
  - [ToJSON()](#M-VRDR-DeathRecord-ToJSON 'VRDR.DeathRecord.ToJSON')
  - [ToJson()](#M-VRDR-DeathRecord-ToJson 'VRDR.DeathRecord.ToJson')
  - [ToXML()](#M-VRDR-DeathRecord-ToXML 'VRDR.DeathRecord.ToXML')
  - [ToXml()](#M-VRDR-DeathRecord-ToXml 'VRDR.DeathRecord.ToXml')
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


<a name='P-VRDR-DeathRecord-AcmeSystemReject'></a>
### AcmeSystemReject `property`

##### Summary

Acme System Reject.

##### Example

// Setter:

Dictionary<string, string> reject = new Dictionary<string, string>();

format.Add("code", ValueSets.FilingFormat.electronic);

format.Add("system", CodeSystems.SystemReject);

format.Add("display", "3");

ExampleDeathRecord.AcmeSystemReject = reject;

// Getter:

Console.WriteLine($"Acme System Reject Code: {ExampleDeathRecord.AcmeSystemReject}");

<a name='P-VRDR-DeathRecord-AcmeSystemRejectHelper'></a>
### AcmeSystemRejectHelper `property`

##### Summary

Acme System Reject.

##### Example

// Setter:

ExampleDeathRecord.AcmeSystemReject = "3";

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

age.Add("code", "a"); // USE: http://hl7.org/fhir/us/vrdr/ValueSet/vrdr-units-of-age-vs

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

ExampleDeathRecord.AutopsyPerformedIndicatorHelper = "Y"";

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

ExampleDeathRecord.BirthRecordState = "MA";

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

Dictionary<string, string> relationship = new Dictionary<string, string>();

relationship.Add("text", "sibling");

SetterDeathRecord.ContactRelationship = relationship;

// Getter:

Console.WriteLine($"Contact's Relationship: {ExampleDeathRecord.ContactRelationship["text"]}");

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

ExampleDeathRecord.DeathLocationTypeHelper = VRDR.ValueSets.PlaceOfDeath.Death_In_Home;

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

##### Example

// Setter:

ExampleDeathRecord.DecedentDispositionMethodHelper = VRDR.ValueSets.MethodOfDisposition.Burial;

// Getter:

Console.WriteLine($"Decedent Disposition Method: {ExampleDeathRecord.DecedentDispositionMethodHelper}");

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

Examiner Contacted Helper. This is a conenience method, to access the code use ExaminerContacted instead.

##### Example

// Setter:

ExampleDeathRecord.ExaminerContactedHelper = "N"

// Getter:

Console.WriteLine($"Examiner Contacted: {ExampleDeathRecord.ExaminerContactedHelper}");

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

Dictionary<string, string> format = new Dictionary<string, string>();

format.Add("code", ValueSets.FilingFormat.electronic);

format.Add("system", CodeSystems.FilingFormat);

format.Add("display", "Electronic");

ExampleDeathRecord.FilingFormat = format;

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

ExampleDeathRecord.InjuryAtWorkHelper = "Y"";

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

Dictionary<string, string> reject = new Dictionary<string, string>();

format.Add("code", ValueSets.FilingFormat.electronic);

format.Add("system", CodeSystems.IntentionalReject);

format.Add("display", "Reject1");

ExampleDeathRecord.IntentionalReject = "reject";

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

ExampleDeathRecord.MilitaryServiceHelper = "Y";

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

Dictionary<string, string> replace = new Dictionary<string, string>();

replace.Add("code", "original");

replace.Add("system", CodeSystems.ReplaceStatus);

replace.Add("display", "original");

ExampleDeathRecord.ReplaceStatus = replace;

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

Dictionary<string, string> sex = new Dictionary<string, string>();

sex.Add("code", "female");

sex.Add("system", "http://hl7.org/fhir/administrative-gender");

sex.Add("display", "female");

ExampleDeathRecord.SexAtDeath = sex;

// Getter:

Console.WriteLine($"Sex at Time of Death: {ExampleDeathRecord.SexAtDeath}");

<a name='P-VRDR-DeathRecord-SexAtDeathHelper'></a>
### SexAtDeathHelper `property`

##### Summary

Decedent's Sex At Death Helper

##### Example

// Setter:

ExampleDeathRecord.SexAtDeathHelper = VRDR.ValueSets.AdministrativeGender.Male;

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

Dictionary<string, string> tcf = new Dictionary<string, string>();

tcf.Add("code", "3");

tcf.Add("system", CodeSystems.TransaxConversion);

tcf.Add("display", "Conversion using non-ambivalent table entries");

ExampleDeathRecord.TransaxConversion = tcf;

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
