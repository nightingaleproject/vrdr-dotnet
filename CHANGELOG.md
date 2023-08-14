# Changelog

<a name="4.1.4"></a>
## [4.1.4](https://www.github.com/nightingaleproject/vrdr-dotnet/releases/tag/v4.1.4) (2023-8-14)

### Bug Fixes

* alias flag on fhir message to 0 (or 1) from blank or null (#513) ([0407051](https://www.github.com/nightingaleproject/vrdr-dotnet/commit/040705121b126b945c1e5659928e9c4174a135b5))

### Other

* bump Microsoft.NET.Test.Sdk from 17.6.3 to 17.7.0 (#510) ([0b1319e](https://www.github.com/nightingaleproject/vrdr-dotnet/commit/0b1319edc99847385a5ece085bc9c822a2444599))

<a name="4.1.3"></a>
## [4.1.3](https://www.github.com/nightingaleproject/vrdr-dotnet/releases/tag/v4.1.3) (2023-8-4)

### Bug Fixes

* void flag on fhir message to 0 (or 1) from blank or null (#508) ([33f3efb](https://www.github.com/nightingaleproject/vrdr-dotnet/commit/33f3efbbcdb59d76ad90fd8baa34c4b47d421fcd))

### Other

* add Fact decorator; remove warning with negative assertion (#509) ([1c07be6](https://www.github.com/nightingaleproject/vrdr-dotnet/commit/1c07be624f0533622922cb1bed75c8465b225b60))
* use GITHUB_TOKEN to auto-merge; close/reopen PR to trigger workflow (#503) ([fa27f7d](https://www.github.com/nightingaleproject/vrdr-dotnet/commit/fa27f7dd9502c2bd0e233560cd91772260338039))
* use machine account PAT to create release PR ([61dd64b](https://www.github.com/nightingaleproject/vrdr-dotnet/commit/61dd64b16a387e8ddd81359ba481f4d6de9cc5ec))

<a name="4.1.2"></a>
## [4.1.2](https://www.github.com/nightingaleproject/vrdr-dotnet/releases/tag/v4.1.2) (2023-7-26)

### Other

* **release:** default to auto-merge; use deploy key to chain workflows (#501) ([517db68](https://www.github.com/nightingaleproject/vrdr-dotnet/commit/517db68b809fc43a9b2894ba2015d2130547caf5))

<a name="4.1.1"></a>
## [4.1.1](https://www.github.com/nightingaleproject/vrdr-dotnet/releases/tag/v4.1.1) (2023-7-26)

### Bug Fixes

* partialDate and partialDateTime validation (#492) ([5e7e932](https://www.github.com/nightingaleproject/vrdr-dotnet/commit/5e7e9326cd97d0c5c4beab751ba0ea3030e1c8e9))

### Other

* add #496 to change log (#499) ([8498226](https://www.github.com/nightingaleproject/vrdr-dotnet/commit/849822650f5ad57f90f281645a66527914b9e284))

<a name="4.1.0"></a>
## [4.1.0](https://www.github.com/nightingaleproject/vrdr-dotnet/releases/tag/v4.1.0) (2023-7-25)

### Features

* add setter/getter for Decedents gender (#482) ([68c8e03](https://www.github.com/nightingaleproject/vrdr-dotnet/commit/68c8e033d690de05be162091bc6d2af0c22abcee))

### Bug Fixes

* partial date time handling of day, month and year (#496) ([3e5b1d3](https://www.github.com/nightingaleproject/vrdr-dotnet/commit/3e5b1d3fb8fc9bbafd406e7bf6fd4dc63a9af31a))

### Other

* wrap commit message in double quotes to allow the use of apostrophe (') (#497) ([ebb1bda](https://www.github.com/nightingaleproject/vrdr-dotnet/commit/ebb1bdabdb1a28bd4bec9bfdab475ab222a9c0c0))

<a name="4.0.10"></a>
## [4.0.10](https://www.github.com/nightingaleproject/vrdr-dotnet/releases/tag/v4.0.10) (2023-7-19)

### Bug Fixes

* handling of missing decedents last name LNAME (#494) ([b5d0c30](https://www.github.com/nightingaleproject/vrdr-dotnet/commit/b5d0c309e159fc3b76b00e4e2c4d22fb4839eb04))

### Other

* use latest checkout and setup-dotnet action (#489) ([569a850](https://www.github.com/nightingaleproject/vrdr-dotnet/commit/569a8506c3a18ded00f03484c0cf7a742ab27d51))

<a name="4.0.9"></a>
## [4.0.9](https://www.github.com/nightingaleproject/vrdr-dotnet/releases/tag/v4.0.9) (2023-7-13)

### Bug Fixes

* ensure correct FHIR date of death and injury handling (#484) ([991ae30](https://www.github.com/nightingaleproject/vrdr-dotnet/commit/991ae30a9e852b23bbfd85f8b04f8d0f4f5f740e))

<a name="4.0.8"></a>
## [4.0.8](https://www.github.com/nightingaleproject/vrdr-dotnet/releases/tag/v4.0.8) (2023-7-7)

### Other

* add DeathRecord.cs annotations and library developers section to README (#477) ([3743ba0](https://www.github.com/nightingaleproject/vrdr-dotnet/commit/3743ba0ee6c9b375cf6f3ca0ab6f7664a249fee4))
* bump xunit from 2.4.2 to 2.5.0 (#481) ([568ec91](https://www.github.com/nightingaleproject/vrdr-dotnet/commit/568ec9152650a8d76d180472509a98ec1b33d785))
* bump xunit.runner.visualstudio from 2.4.5 to 2.5.0 (#480) ([0276b75](https://www.github.com/nightingaleproject/vrdr-dotnet/commit/0276b7596ac9dd2bb350a27c59d3c7bcde817427))
* handle dependabot pull requests (#478) ([2b7f1f8](https://www.github.com/nightingaleproject/vrdr-dotnet/commit/2b7f1f892930eeec91ce96bf73c473925c16f68c))

<a name="4.0.7"></a>
## [4.0.7](https://www.github.com/nightingaleproject/vrdr-dotnet/releases/tag/v4.0.7) (2023-6-29)

### Bug Fixes

* correct CLI argument position error between jurisidiction and output directory (#473) ([ccbc98d](https://www.github.com/nightingaleproject/vrdr-dotnet/commit/ccbc98d118233e1dfe2de61efe962e73810b188e))

### Other

* bump Microsoft.NET.Test.Sdk from 17.6.2 to 17.6.3 (#475) ([c59b91e](https://www.github.com/nightingaleproject/vrdr-dotnet/commit/c59b91e4184ab22970e241f5445ec0ef64b85ad4))

<a name="4.0.6"></a>
## [4.0.6](https://www.github.com/nightingaleproject/vrdr-dotnet/releases/tag/v4.0.6) (2023-6-27)

### Bug Fixes

* change BaseMessage.MessageBundle accessibility to public get, protected set ([72ae122](https://www.github.com/nightingaleproject/vrdr-dotnet/commit/72ae122430a6465699fd59006475b2306ddf978c))

<a name="4.0.5"></a>
## [4.0.5](https://www.github.com/nightingaleproject/vrdr-dotnet/releases/tag/v4.0.5) (2023-6-26)

### Features

* add VOID and ALIAS handling in IJEMortality class ([8a2c10b](https://www.github.com/nightingaleproject/vrdr-dotnet/commit/8a2c10be680358042595fcd045857367074c0e47))

<a name="4.0.4"></a>
## [4.0.4](https://www.github.com/nightingaleproject/vrdr-dotnet/releases/tag/v4.0.4) (2023-6-23)

### Features

* allow retrieval of all Connectathon records ([a5d5f07](https://www.github.com/nightingaleproject/vrdr-dotnet/commit/a5d5f07a601cf33fe31872e16a2d250e5e727a76))
* sync pregnancy status vealue set with IG ([61d84c6](https://www.github.com/nightingaleproject/vrdr-dotnet/commit/61d84c6361b1760d847bcf97d263e5fdba6c0771))

### Bug Fixes

* expected age at time of death for test record #3 ([e3fb04b](https://www.github.com/nightingaleproject/vrdr-dotnet/commit/e3fb04b330e97b909cc034d9f42e11c48d489fd1))
* sync VOID message block_count property with messaging IG ([731db5b](https://www.github.com/nightingaleproject/vrdr-dotnet/commit/731db5b004c8091031e979cda1d23e834e499f2d))
* yes-no value set url (#456) ([bf042dc](https://www.github.com/nightingaleproject/vrdr-dotnet/commit/bf042dcedfa3b883582741e8bb71c8b93155a0b9))


<a name="4.0.3"></a>
## [4.0.3](https://www.github.com/nightingaleproject/vrdr-dotnet/releases/tag/v4.0.3) (2023-6-7)

* Update birth month for test record #3 in Connectathon.cs
* Revert change to remove transportation role component on injury incident

## v4.0.2 - 2023-06-05

* Update ages in Connectathon.cs
* Update ages and add test method CheckConnectathonRecord3() in DeathRecord_Should.cs
* Update and sync examples files (json and xml) with IG as follows:
    * Remove Transport Role and its references
    * Remove Death Certificate Reference and its references
    * Remove Death Pronouncement Performer and its references
    * Remove Decedent Employment History and its references
    * Remove Cause of Death Pathway and its references
    * Change Mortician, Funeral Home Director, Funeral Service Licensee to us-core-practitioner
    * Change Interested Party to us-core-organization
    * Change Decendent Pregnancy to decedent-pregnancy-status
    * Change Cause of Death Condition to Cause of Death Part 1
    * Change Cause of Death Condition Contributing Death to Cause of Death Part 2

## v4.0.1 - 2023-05-16

* Syncs Connectathon test record generation code and example JSON and XML files with NCHS test plan changes
* Syncs Race and Ethnicity Literal fields with latest VRDR IG build version and adds additional test cases
* Add test cases for COD Coding Acknowledegement use case

## v4.0.0.preview21 - 2023-04-20

* Adds support for multiple message destinations. It implemented this with a comma separated string of endpoints in the MessageDestination attribute. 

*## v4.0.0.preview20 - 2023-04-20

* Corrected race literals in code, tests and example json, added component name check on setting race literals to be in sync with IG
* Updated test case to include missing usual occupation text, also made usual industry return null on unknown just as does usual occupation
* Fixed bug where calling UsualIndustry would crash if no text key was provided such as happens in the unknown case
* Incorporates preview19-with-filtering changes

## v4.0.0.preview19-with-filtering - 2023-03-16

* Added filtering logic
* Added CLI to allow filtering of a single file
* Added multiple tests to test issues we've experienced and testing different filters
* Added documentation for CLI and brief descriptions for each test case

## v4.0.0.preview19 - 2023-02-17

* Additional tests for autopsy performed and results cases
* Fixed decedent education level handling for demographics
* Added handling for pregnancy code 8 - computer generated

## v4.0.0.preview18 - 2023-02-10

* Connectathon test record fixes
* DateOfDeath valueTime formatting correction
* LOINC code display value corrected to "Location of Death"
* NVSS Race and Ethnicity display values corrected
* Test methods added to verify each of the above
* Corrected time values
* Fixed IJE classes to post values to time in format of 00:00:00.
* Updated ICD-10 code verification to support codes such as U071

## v4.0.0.preview17 - 2023-01-30

* Updated display text for the death record certifier types to align with IG

## v4.0.0.preview16 - 2022-12-23

* Updated AgeAtDeath property to expect a dictionary with "value" and "code" rather than "value" and "unit" to match the FHIR IG
* Added helper properties for AgeAtDeath to make it easier to set: AgeAtDeathYears, AgeAtDeathMonths, AgeAtDeathDays, AgeAtDeathHours, and AgeAtDeathMinutes
* Added a tabular data to FHIR Death Record Converter for testing
* Improved errors returned for DSTATE missing vs incorrect
* Fixed Auxno and Auxno2 formatting during conversion
* Fixed IJEMortality handling of NCHS-formatted ICD10 codes 
* Return null instead of empty strings for a blank fields in the death record

## v4.0.0-preview15 - 2022-12-06

* Fixed an issue where timezone conversion was causing an incorrect day of death to be returned
* Expanded TargetFrameworks for the VRDR.Client library to include .NET Core 3.1 and .NET Framework 4.8

## v4.0.0-preview14 - 2022-11-22

* Fixed issue with WithinCityLimits field getting overwritten when setting address properties
* Update date and time fields to differentiate between no data provided and explicitly unknown (see https://github.com/nightingaleproject/vrdr-dotnet#specifying-that-a-date-or-time-is-explicitly-unknown)
* Fixed an issue in handling blank race value fields
* Updated handling of middle name properties in IJEMortality class to account for mismatches between the FHIR standard and IJE (see https://github.com/nightingaleproject/vrdr-dotnet#names-in-fhir)
    - The library raises an exception if a middle name is set before a first name when using IJEMortality
    - The IJEMortality class returns middle name fields of the correct length

## v4.0.0-preview13 - 2022-11-03

* Added missing value for Non-Hispanic to HispanicOrigin value set

## v4.0.0-preview12 - 2022-10-31

* Added support for creating and submitting batch messages to the API
* Fixed SSN length validation
* Updated value mappings after corrections to the VRDR IG
* Fixed CLI MRE/TRX conversion bug
* Fixed issue with parsing empty literal race fields
* Fixed issue that allowed empty strings in alias message fields
* Fixed incorrect error for AgeAtDeathEditFlag

## v4.0.0-preview11 - 2022-09-19

* Fixed null object reference issue when retrieving message ID

## v4.0.0-preview10 - 2022-09-19

* Fixed date handling to correctly distinguish between unknown and unset
* Fixed issue in ICD code handling to correctly support 5 character codes
* Updated Client implementation to increase robustness of token refresh
* Improved support for Status messages
* Added support for linking coding response messages to the submission messages that they code

## v4.0.0-preview9 - 2022-08-19

* Added TS as a jurisdiction code for testing purposes

## v4.0.0-preview8 - 2022-08-18

* Fixed bug where injury incident value was encoded as String instead of CodeableConcept text
* Improved support for Alias messages to allow data to be pulled from a source record
* Improved error handling in microservice to return JSON errors
* Added tools in the VRDR.CLI to convert from Coding Content Bundles to and from MRE and TRX formats
* Added tools in the VRDR.CLI to compare JSON and TRX/MRE formatted data via translation to IJE
* Added support for DateOfDeathDetermination
* Updated the Client to stop using the now-unneeded _since parameter

## v4.0.0-preview7 - 2022-08-05

* Fixed credential handling in Client implementation
* Updated handling of RelatedPerson fields to correctly set Active=True

## v4.0.0-preview6 - 2022-07-22

* Fixed BSTATE issue with connectathon test record
* Fixed an issue with death age unit handling
* Fixed an issue with time handling to correctly include seconds
* Fixed an inconsistent race designator
* Updated FHIR generation code so that empty observations are not produced
* Updated pregnancy indicator handling for Record Axis Codes
* Added TT as a jurisdiction code for testing purposes
* Added initial approach for mortality roster bundle
* Added initial support for status messages
* Improved usability of the command line tool

## v4.0.0.preview5 - 2022-06-22

* Addressed incorrect conversion of non-string values into strings
* Updated documentation to better align with recent library changes
* Added new 2ijecontent and extract2ijecontent services to the command line tool
* Removed erroneous second injury location object from output bundle
* Fixed issue where VRDR DeathRecord properties that return a dictionary were returning null

## v4.0.0.preview4 - 2022-05-31

* Added parsing of generic non-VRDR message bundles to support more flexibility in message-handling APIs built using the VRDR.Messaging library

## v4.0.0.preview3 - 2022-05-25

* Aligned with IG updates
    - Eliminated CauseOfDeathPathway and added lineNumber component to Part1CauseOfDeath
    - Eliminated fields not in VRDR IG: BirthSex, BirthRecordDataAbsentReason, Occupation, Work
    - Updated approach for Location names to support field being required by setting to "BLANK" when not specified
    - Made small changes to field types to pass FHIR validation
    - Updated examples to match new IG
* Made updates to improve consistency and usability
    - Refactored RecordAxis and EntityAxis to use named tuples
    - Changed EditFlag properties to have consistent names
    - Removed Boolean methods in favor of more consistent Helper methods
    - Updated AgeAtDeath property to expect a dictionary with "value" and "unit" rather than "units" and "type" to match the FHIR IG
* Made updates to address bugs
    - Fixed errors in CertificationRole and TransportationRole
    - Fixed value set mapping for SpouseAlive
    - Fixed issue with Hispanic/No/Unknown mapping
    - Fixed issue with edit flag properties overwriting other content
    - Fixed issue with DeathLocationJurisdiction property setting values incorrectly
    - Fixed issue with PartialDateTime extensions being used where PartialDate should be used
    - Fixed issue with Messaging library to correctly support changing a Record within a Message
* Made updates to address issues in Canary
    - Updated functionality used by Canary to exclude Helper properties from Canary tests
    - Fixed FHIR paths to support better test results in Canary
* Added release of VRDR.Client NuGet package

## v4.0.0.preview2 - 2022-05-09

* Addressed inconsistencies in how identifiers were being handled

## v4.0.0.preview1 - 2022-05-07

* Aligned VRDR library to latest VRDR IG at http://build.fhir.org/ig/HL7/vrdr/artifacts.html
    - Updated 137 fields to the latest FHIR representation
    - Added support for 109 additional fields in the IJE specification that were not previously supported by the IG
    - Added helper methods for all coded fields
    - Provide constants for all Value Sets, Profile URLs, and Extension URLs
    - Moved Connectathon records to VRDR library and updated them for June 2022 testing event
    - Updated Canary mappings to prepare for Canary update to align with the new IG
* Aligned VRDR.Messaging library to latest VRDR Messaging IG at http://build.fhir.org/ig/nightingaleproject/vital_records_fhir_messaging_ig/branches/main/index.html
    - Renamed classes and properties to align with IG
        - Changed DeathRecordSubmission to DeathRecordSubmissionMessage
        - Changed DeathRecordUpdate to DeathRecordUpdateMessage
        - Changed AckMessage to AcknowledgementMessage
        - Changed VoidMessage to DeathRecordVoidMessage
        - Changed CauseOfDeathCodingResponseMessage to CauseOfDeathCodingMessage
        - Changed DemographicsCodingResponseMessage to DemographicsCodingMessage
        - Changed CertificateNumber property to CertNo
        - Changed StateAuxiliaryIdentifier property to StateAuxiliaryId
        - Changed DeathJurisdictionID property to JurisdictionId
    - Added support for new DeathRecordAliasMessage
    - New common model of message bundles to support submission messages, coding response messages, and full interjurisdictional exchange messages

## v3.3.1 - 2022-04-07

* Added the CertificationRoleHelper method
* Documented the new helper methods in the README
* Copied the Connectathon record generation code into VRDR from Canary (will eventually be removed from the Canary code base)
* Updated code for converting from the Nightingale data format to FHIR
* Added functionality to the VRDR.CLI application for generating Connectathon records from the command line
* Added a ruby script for generating a large quantity of FHIR records using the Connectathon records
* Added a ruby script for converting an Excel file with records in pseudo-IJE format into a set of FHIR records for testing
* Removed support for the now-obsolete netcoreapp2.1

## v3.3.0 - 2022-04-05

* Added a client library (VDRD.Client) for interacting with the [NVSS FHIR API](https://github.com/nightingaleproject/Reference-NCHS-API)
* Added helper methods for setting field values that use value sets
* Fixed bug in unknown DOB IJE to FHIR conversion

## v3.2.1 - 2021-10-05

* Return null rather than an error when jurisdiction id is missing from VRDR record

## v3.2.0 - 2021-10-01

* Fixed FHIRPath and data types to support date absent extension
* Allow casting a message to a Bundle to support the NCHS API
* Return descriptive error when jurisdiction id is missing from VRDR record
* Add support for passing Bundles directly to BaseMessage

## v3.2.0-preview5 - 2021-09-13

* Fixed bug in how nulls are interpreted when loading description files that caused segments of records to be dropped in Canary
* Removed incorrect extra spaces from some race strings
* Fixed bug that caused an error if the receipt year was set to the death year when creating coded response messages
* Improved text that describes expected values for the Death Location Jurisdiction field
* Fixed bug that caused incorrect data to be shown in Canary for Death Location Jurisdiction

## v3.2.0-preview4 - 2021-09-02

* Fixed bug that causes error when calling data absent boolean setter methods

## v3.2.0-preview3 - 2021-09-01

* Add methods for getting and setting AgeAtDeathAbsentReason and BirthRecordIdentifierAbsentReason. A boolean getter and setter returning whether a reason has been set has also been provided.

## v3.2.0-preview2 - 2021-08-25

* Fetch DeathLocationJurisdiction from DeathLocationJurisdiction instead of DeathLocationAddress?["addressState"] in VRDR Messaging

## v3.2.0-preview1 - 2021-08-24

* Updated to [STU2 version](http://hl7.org/fhir/us/vrdr/2021Sep/) of the VRDR IG
* DeathLocationJurisdiction must now be specified in order to generate an IJE file
* Country information is now universally represented in the library as a 2 character code (e.g. US instead of United States)
* State information is now univerally represented in the library as a 2 character code (e.g. FL instead of Florida)
* BirthRecordIdentifier.component:birthState is now represented as ISO-3166-2 (e.g. US-MA instead of MA)
* Added support for missing data as per the STU2 IG
* Full list of supported IJE fields [is now available](VRDRdotNETLibraryCoverage.csv).

## v3.1.1 - 2021-06-10

* Reorder death record properties to reflect order in standard death certificate

## v3.1.0 - 2021-02-17

* Acknowledgement messages support block_count field for acknowledging bulk void messages
* Added additional required fields to CodingResponseMessage
* Documentation updated to reflect completed migration from CauseCode to CodingResponseMessage

## v3.1.0-RC5 - 2020-09-16

* Blank identifiers are ignored in a Death Certificate Reference
* Default is to have no certificate number rather than an OID
* Added IJE vs. IJE-from-FHIR comparison utility function to VRDR.CLI
* Fixed a bug that allowed blank values in IJE age fields

## v3.1.0-RC4 - 2020-09-10

* Added message related utility functions to VRDR.CLI
* Assorted bug fixes
    - Fixed a bug where an identifier was being incorrectly initialized with a GUID

## v3.1.0-RC3 - 2020-09-10

* Assorted bug fixes
    - Fixed a bug that would cause an NPE if source was missing from a parsed message header

## v3.1.0-RC2 - 2020-09-08

* Assorted bug fixes
    - Fixed a bug that would cause an NPE if a mortician was not present in a death record file
    - Fixed a bug where mortician entry was not being correctly identified
    - Fixed bug where text description of usual occupation and industry was being put in the code description instead of the codeable concept text

## v3.1.0-RC1 - 2020-08-25

* Updated `DeathRecord` class to match latest VRDR IG changes.
* Numerous under-the-covers changes to address IG changes from FHIR STU3 -> R4 and fix errors and warnings from the FHIR validator including:
    - Switch location of business identifiers: certificate number from Death Certificate (Composition) to Death Certification (Procedure), state auxiliary identifier from Death Certificate Document to Document Reference, NCHS identifier to Bundle identifier
    - Clinical code changes, e.g. 74165-2 "History of employment status" to 21843-8 "History of usual occupation"
    - Add required code systems to codings
    - Add required (by US Core IG foundation profiles) identifiers to several resources
    - Fix display text for codes
    - Full state name to two letter state code
    - Boolean observations to coded observations with 'Y', 'N', unknown or not applicable values
    - Extract Observation component to full Observation, e.g. for Military Service
    - Add support for LocationJurisdictionId extension
* Properties that map to a FHIR CodeableConcept now support an addition `text` dictionary key that maps to the `CodeableConcept.text` element.
* Added `DeathRecord.StateLocalIdentifier` property.
* `DeathRecord.InterestedPartyIdentifier` is now a `Dictionary` instead of a `string`.
* Added `DeathRecord.CertifierIdentifier` property.
* `DeathRecord.ResidenceWithinCityLimits` is now a `Dictionary` instead of a `bool`.
* Added `DeathRecord.ResidenceWithinCityLimitsBoolean` property as a convenience method.
* Added `DeathRecord.UsualOccupationEnd` property.
* Added `DeathRecord.MilitaryServiceBoolean` property as a convenience method.
* `DeathRecord.MorticianIdentifier` is now a `Dictionary` instead of a `string`.
* Added `DeathRecord.FuneralDirectorPhone` property.
* Added `DeathRecord.AutopsyPerformedBoolean` property as a convenience method.
* Added death pronouncer: `PronouncerGivenNames`, `PronouncerFamilyName`, `PronouncerSuffix`, `PronouncerIdentifier`.
* Added `DeathRecord.DateOfDeath` property.
* Added `DeathRecord.AutopsyResultsAvailableBoolean` property as a convenience method.
* Added `DeathRecord.DeathLocationType` property.
* Added `DeathRecord.DeathLocationJurisdiction` property.
* `DeathRecord.ExaminerContacted` is now a `Dictionary` instead of a `bool`.
* Added `DeathRecord.ExaminerContactedBoolean` property as a convenience method.
* `DeathRecord.InjuryPlace` is now a `Dictionary` instead of a `string`.
* Added `DeathRecord.InjuryPlaceDescription` property as a convenience method.
* Added `DeathRecord.InjuryAtWorkBoolean` property as a convenience method.
* `DeathRecord.TransportEvent` is now a `Dictionary` instead of a `bool`.
* Added `DeathRecord.TransportationEventBoolean` property as a convenience method.
* Added `DeathRecord.EthnicityText` property for uncoded ethnicity
* Added `DeathRecord.RaceText` property for uncoded race

## v3.1.0-preview10 - 2020-06-12

* Improve error handling during message parsing.
* Add `MessageParseException` class.
* Added `CauseOfDeathEntityAxisList` property to `CodingResponseMessage` and `CodingUpdateMessage`.

## v3.1.0-preview9 - 2020-05-29

* Add ability to format output for human readability via `prettyPrint` parameter of `BaseMessage.ToXML` and `BaseMessage.ToJSON`.
* Move VRDR.Messaging API documentation to new, [task-oriented, documentation page](doc/Messaging.md).
* `ExtractionErrorMessage(sourceMessage)` constructor initializes the `MessageSource` property from `sourceMessage.MessageDestination`. Removed the defaulted `source` parameter from this constructor.

## v3.1.0-preview8 - 2020-05-27

* Add business identifier properties (`NCHSIdentifier`, `StateAuxiliaryIdentifier`, `CertificateNumber`) to every message type.
* Replace `DeathCertificateText` and `CauseOfDeathConditionId` properties of `CauseOfDeathEntityAxisEntry` with `LineNumber` property.
* Serialized messages use `urn:uuid:...` style resource references everywhere.
* `EntityAxisBuilder.Add` will skip adding an entry if the supplied code is blank.
* Add variant of `BaseMessage.Parse` that accepts a `string` argument.
* `AckMessage(sourceMessage)` constructor initializes the `MessageSource` property from `sourceMessage.MessageDestination`. Removed the defaulted `source` parameter from this constructor.