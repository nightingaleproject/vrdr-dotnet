# DeathRecord

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
