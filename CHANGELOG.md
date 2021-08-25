## Change Log

### v3.2.0-preview2 - 2021-08-25

* Fetch DeathLocationJurisdiction from DeathLocationJurisdiction instead of DeathLocationAddress?["addressState"] in VRDR Messaging

### v3.2.0-preview1 - 2021-08-24

* Updated to [STU2 version](http://hl7.org/fhir/us/vrdr/2021Sep/) of the VRDR IG
* DeathLocationJurisdiction must now be specified in order to generate an IJE file
* Country information is now universally represented in the library as a 2 character code (e.g. US instead of United States)
* State information is now univerally represented in the library as a 2 character code (e.g. FL instead of Florida)
* BirthRecordIdentifier.component:birthState is now represented as ISO-3166-2 (e.g. US-MA instead of MA)
* Added support for missing data as per the STU2 IG
* Full list of supported IJE fields [is now available](VRDRdotNETLibraryCoverage.csv).

### v3.1.1 - 2021-06-10

* Reorder death record properties to reflect order in standard death certificate

### v3.1.0 - 2021-02-17

* Acknowledgement messages support block_count field for acknowledging bulk void messages
* Added additional required fields to CodingResponseMessage
* Documentation updated to reflect completed migration from CauseCode to CodingResponseMessage

### v3.1.0-RC5 - 2020-09-16

* Blank identifiers are ignored in a Death Certificate Reference
* Default is to have no certificate number rather than an OID
* Added IJE vs. IJE-from-FHIR comparison utility function to VRDR.CLI
* Fixed a bug that allowed blank values in IJE age fields

### v3.1.0-RC4 - 2020-09-10

* Added message related utility functions to VRDR.CLI
* Assorted bug fixes
    - Fixed a bug where an identifier was being incorrectly initialized with a GUID

### v3.1.0-RC3 - 2020-09-10

* Assorted bug fixes
    - Fixed a bug that would cause an NPE if source was missing from a parsed message header

### v3.1.0-RC2 - 2020-09-08

* Assorted bug fixes
    - Fixed a bug that would cause an NPE if a mortician was not present in a death record file
    - Fixed a bug where mortician entry was not being correctly identified
    - Fixed bug where text description of usual occupation and industry was being put in the code description instead of the codeable concept text

### v3.1.0-RC1 - 2020-08-25

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

### v3.1.0-preview10 - 2020-06-12

* Improve error handling during message parsing.
* Add `MessageParseException` class.
* Added `CauseOfDeathEntityAxisList` property to `CodingResponseMessage` and `CodingUpdateMessage`.

### v3.1.0-preview9 - 2020-05-29

* Add ability to format output for human readability via `prettyPrint` parameter of `BaseMessage.ToXML` and `BaseMessage.ToJSON`.
* Move VRDR.Messaging API documentation to new, [task-oriented, documentation page](doc/Messaging.md).
* `ExtractionErrorMessage(sourceMessage)` constructor initializes the `MessageSource` property from `sourceMessage.MessageDestination`. Removed the defaulted `source` parameter from this constructor.

### v3.1.0-preview8 - 2020-05-27

* Add business identifier properties (`NCHSIdentifier`, `StateAuxiliaryIdentifier`, `CertificateNumber`) to every message type.
* Replace `DeathCertificateText` and `CauseOfDeathConditionId` properties of `CauseOfDeathEntityAxisEntry` with `LineNumber` property.
* Serialized messages use `urn:uuid:...` style resource references everywhere.
* `EntityAxisBuilder.Add` will skip adding an entry if the supplied code is blank.
* Add variant of `BaseMessage.Parse` that accepts a `string` argument.
* `AckMessage(sourceMessage)` constructor initializes the `MessageSource` property from `sourceMessage.MessageDestination`. Removed the defaulted `source` parameter from this constructor.
