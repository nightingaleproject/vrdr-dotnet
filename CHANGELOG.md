## Change Log

### v3.1.0-preview8 - 2020-05-27

* Add business identifier properties (`NCHSIdentifier`, `StateAuxiliaryIdentifier`, `CertificateNumber`) to every message type.
* Replace `DeathCertificateText` and `CauseOfDeathConditionId` properties of `CauseOfDeathEntityAxisEntry` with `LineNumber` property.
* Serialized messages use `urn:uuid:...` style resource references everywhere.
* `EntityAxisBuilder.Add` will skip adding an entry if the supplied code is blank.
* Add variant of `BaseMessage.Parse` that accepts a `string` argument.
* `AckMessage(sourceMessage)` constructor initializes the `MessageSource` property from `sourceMessage.MessageDestination`. Removed the defaulted `source` parameter from this constructor.
