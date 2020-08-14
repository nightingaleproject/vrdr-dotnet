## Change Log

### v3.1.0-preview10 - 2020-08-12

* Improve error handling during message parsing.
* Add `MessageParseException` class.
* Added `CauseOfDeathEntityAxisList` property to `CodingResponseMessage` and `CodingUpdateMessage`.
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
* `DeathRecord.ExaminerContacted` is now a `Dictionary` instead of a `bool`.
* Added `DeathRecord.ExaminerContactedBoolean` property as a convenience method.
* `DeathRecord.InjuryPlace` is now a `Dictionary` instead of a `string`.
* Added `DeathRecord.InjuryAtWorkBoolean` property as a convenience method.
* `DeathRecord.TransportEvent` is now a `Dictionary` instead of a `bool`.
* Added `DeathRecord.TransportationEventBoolean` property as a convenience method.

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
