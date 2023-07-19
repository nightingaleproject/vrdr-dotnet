[![Build Status](https://travis-ci.org/nightingaleproject/vrdr-dotnet.svg?branch=master)](https://travis-ci.org/nightingaleproject/vrdr-dotnet)
[![Nuget](https://img.shields.io/nuget/v/VRDR?label=VRDR%20%28nuget%29)](https://www.nuget.org/packages/VRDR)
[![Nuget](https://img.shields.io/nuget/v/VRDR.Messaging?label=VRDR.Messaging%20%28nuget%29)](https://www.nuget.org/packages/VRDR.Messaging)

# vrdr-dotnet

This repository includes .NET (C#) code for

- Producing and consuming the Vital Records Death Reporting (VRDR) Health Level 7 (HL7) Fast Healthcare Interoperability Resources (FHIR) standard. [Click here to view the FHIR Implementation Guide STU2.1](http://hl7.org/fhir/us/vrdr/STU2.1).
- Producing and consuming FHIR messages for the exchange of VRDR documents.
- Support for converting VRDR FHIR records to and from the Inter-Jurisdictional Exchange (IJE) Mortality format, as well as companion microservice for performing conversions.
- This codebase covers the subset of the IJE fields listed in this [spreadsheet](VRDRdotNETLibraryCoverage.csv).

## Documentation

[Doxygen Docs](https://nightingaleproject.github.io/vrdr-dotnet/)

## Versions
Interactions with NCHS are governed by the CI build version of the VRDR and Vital Records Messaging IGs, and should use the latest, supported releases of the VRDR .NET software and Canary.

<table class="versionTable" border="3">
<tbody>
<tr>
<td style="text-align: center;"><strong>VRDR IG</strong></td>
<td style="text-align: center;"><strong>Messaging IG</strong></td>
<td style="text-align: center;"><strong>FHIR</strong></td>
<td style="text-align: center;"><strong>Version</strong></td>
<td style="text-align: center;"><strong>VRDR</strong></td>
<td style="text-align: center;"><strong>VRDR.Messaging</strong></td>
</tr>
</tr>
<tr>
<td style="text-align: center;"><a href="http://build.fhir.org/ig/HL7/vrdr/">STU2.1 CI build version</a></td>
<td style="text-align: center;"><a href="http://build.fhir.org/ig/nightingaleproject/vital_records_fhir_messaging_ig/branches/main/index.html">v0.9.1</a></td>
<td style="text-align: center;">R4</td>
<td style="text-align: center;">V4.0.3</td>
<td style="text-align: center;"><a href="https://www.nuget.org/packages/VRDR/4.0.3">nuget</a> <a href="https://github.com/nightingaleproject/vrdr-dotnet/releases/tag/4.0.3"> github</a></td>
<td style="text-align: center;"><a href="https://www.nuget.org/packages/VRDR.Messaging/4.0.3">nuget</a> <a href="https://github.com/nightingaleproject/vrdr-dotnet/releases/tag/4.0.3"> github</a></td>
</tr>
<tr>
<td style="text-align: center;"><a href="http://hl7.org/fhir/us/vrdr/STU2.1/">STU2.1 Published</a></td>
<td style="text-align: center;"><a href="http://build.fhir.org/ig/nightingaleproject/vital_records_fhir_messaging_ig/branches/main/index.html">v0.9.1</a></td>
<td style="text-align: center;">R4</td>
<td style="text-align: center;">V4.0.3</td>
<td style="text-align: center;"><a href="https://www.nuget.org/packages/VRDR/4.0.3">nuget</a> <a href="https://github.com/nightingaleproject/vrdr-dotnet/releases/tag/4.0.3"> github</a></td>
<td style="text-align: center;"><a href="https://www.nuget.org/packages/VRDR.Messaging/4.0.3">nuget</a> <a href="https://github.com/nightingaleproject/vrdr-dotnet/releases/tag/4.0.3"> github</a></td>
</tr>
<tr>
<td style="text-align: center;"><a href="http://hl7.org/fhir/us/vrdr/STU2/">STU2 Published</a></td>
<td style="text-align: center;"><a href="http://build.fhir.org/ig/nightingaleproject/vital_records_fhir_messaging_ig/branches/main/index.html">v0.9.1</a></td>
<td style="text-align: center;">R4</td>
<td style="text-align: center;">V4.0.3</td>
<td style="text-align: center;"><a href="https://www.nuget.org/packages/VRDR/4.0.3">nuget</a> <a href="https://github.com/nightingaleproject/vrdr-dotnet/releases/tag/4.0.3"> github</a></td>
<td style="text-align: center;"><a href="https://www.nuget.org/packages/VRDR.Messaging/4.0.3">nuget</a> <a href="https://github.com/nightingaleproject/vrdr-dotnet/releases/tag/4.0.3"> github</a></td>
</tr>
<tr>
<td style="text-align: center;"><a href="http://build.fhir.org/ig/HL7/vrdr/">STU2 v1.3</a></td>
<td style="text-align: center;"><a href="http://build.fhir.org/ig/nightingaleproject/vital_records_fhir_messaging_ig/branches/main/index.html">v0.9</a></td>
<td style="text-align: center;">R4</td>
<td style="text-align: center;">V4.0.3</td>
<td style="text-align: center;"><a href="https://www.nuget.org/packages/VRDR/4.0.3">nuget</a> <a href="https://github.com/nightingaleproject/vrdr-dotnet/releases/tag/4.0.3"> github</a></td>
<td style="text-align: center;"><a href="https://www.nuget.org/packages/VRDR.Messaging/4.0.3">nuget</a> <a href="https://github.com/nightingaleproject/vrdr-dotnet/releases/tag/4.0.3"> github</a></td>
</tr>
<tr>
<td style="text-align: center;">STU1</td>
<td style="text-align: center;">N/A</td>
<td style="text-align: center;">R4</td>
<td style="text-align: center;">V3.1.1</td>
<td style="text-align: center;"><a href="https://www.nuget.org/packages/VRDR/3.1.1">nuget</a> <a href="https://github.com/nightingaleproject/vrdr-dotnet/releases/tag/v3.1.1"> github</a></td>
<td style="text-align: center;"><a href="https://www.nuget.org/packages/VRDR.Messaging/3.1.1">nuget</a> <a href="https://github.com/nightingaleproject/vital_records_fhir_messaging/releases/download/v3.1.0/fhir_messaging_for_nvss.pdf"> github</a></td>
</tr>

</tbody>
</table>

## Requirements

### Development & CLI Requirements
- This repository is built using .NET Core 6.0, download [here](https://dotnet.microsoft.com/download)
### Library Usage
- The VRDR and VRDR.Messaging libraries target .NET Standard 2.0
- The VRDR.Client library targets .NET Core 3.1 and .NET Framework 4.8
- To check whether your .NET version supports a release, refer to [the .NET matrix](https://docs.microsoft.com/en-us/dotnet/standard/net-standard#net-implementation-support).
  - Note whether you are using .NET Core or .NET Framework - see [here](https://docs.microsoft.com/en-us/archive/msdn-magazine/2017/september/net-standard-demystifying-net-core-and-net-standard) for distinctions between the .NET implementation options.
  - Once you’ve determined your .NET implementation type and version, for example you are using .NET Framework 4.6.1, refer to the matrix to verify whether your .NET implementation supports the targeted .NET Standard version.
    - Ex. If you are using .NET Framework 4.6.1, you can look at the matrix and see the .NET Framework 4.6.1 supports .NET Standard 2.0 so the tool would be supported.

## Project Organization

### VRDR
This directory contains a FHIR Death Record library for consuming and producing VRDR FHIR. This library also includes support for converting to and from the Inter-Jurisdictional Exchange (IJE) Mortality format.

For API documentation, [click here](VRDR/DeathRecord.md).

#### Usage

This package is published on NuGet, so including it is as easy as:
```xml
<ItemGroup>
  ...
  <PackageReference Include="VRDR" Version="4.0.3" />
  ...
</ItemGroup>
```

You can also include a locally downloaded copy of the library instead of the NuGet version by referencing `DeathRecord.csproj` in your project configuration, for example (taken from VRDR.CLI):
```xml
<Project Sdk="Microsoft.NET.Sdk">
  ...
  <ItemGroup>
    <ProjectReference Include="..\VRDR\DeathRecord.csproj" />
    ...
  </ItemGroup>
</Project>
```

#### Producing Example
The following snippet is a quick example of producing a from-scratch FHIR VRDR record using this library, and then printing it out as a JSON string. For a complete example, [click here](https://github.com/nightingaleproject/csharp-fhir-death-record/blob/master/VRDR.CLI/Program.cs#L23).
```cs
using VRDR;

DeathRecord deathRecord = new DeathRecord();

// Set Death Record ID
deathRecord.Identifier = "42";

// Add Decedent Given Names
string[] givenNames = { "First", "Middle" };
deathRecord.GivenNames = givenNames;

// Add Decedent Last Name
deathRecord.FamilyName = "Last";

// Cause of Death Part I, Line a
deathRecord.COD1A = "Rupture of myocardium";

// Cause of Death Part I Interval, Line a
deathRecord.INTERVAL1A = "Minutes";

// Add PregnancyStatus
Dictionary<string, string> code = new Dictionary<string, string>();
code.Add("code", "2");
code.Add("system", VRDR.CodeSystems.PregnancyStatus);
code.Add("display", "Pregnant at the time of death");
deathRecord.PregnancyStatus = code;

// Add ExaminerContacted
deathRecord.ExaminerContactedHelper = VRDR.ValueSets.YesNoUnkown.Yes ;

// Add DateOfDeath
deathRecord.DateOfDeath = "2018-07-10T10:04:00+00:00";

// Print record as a JSON string
Console.WriteLine(deathRecord.ToJSON());
```

#### Consuming Example
An example of consuming a VRDR FHIR document (in XML format) using this library, and printing some details from it:
```cs
using VRDR;

// Read in FHIR Death Record XML file as a string
string xml = File.ReadAllText("./example_vrdr_fhir_record.xml");

// Construct a new DeathRecord object from the FHIR VRDR XML string
DeathRecord deathRecord = new DeathRecord(xml);

// Print out some details from the record
Console.WriteLine($"Decedent's Last Name: {deathRecord.FamilyName}");

Console.WriteLine($"Date/Time of Death: {deathRecord.DateOfDeath}");

Console.WriteLine($"Cause of Death Part I, Line a: {deathRecord.COD1A}");
Console.WriteLine($"Cause of Death Part I Interval, Line a: {deathRecord.INTERVAL1A}");
```
#### Specifying that a date or time is explicitly unknown

When specifying a date or time it is important to be able to differentiate between "we explicitly
don't know the date, and we're telling you that we don't know it" and just not setting a date
property at all. For this reason the date and time properties on the DeathRecord class support the
special value of -1 (for properties that expect an integer) or "-1" (for properties that expect a
string) in order to specify that the data is explicitly unknown. This is equivalent to using a value
of "9999" in IJE.

Example:

```
DeathRecord deathRecord = new DeathRecord();
deathRecord.DeathYear = 2022;
deathRecord.DeathMonth = 2;
deathRecord.DeathDay = -1;
deathRecord.DeathTime = "-1";
```

#### Names in FHIR

FHIR manages names in a way that there is a fundamental incompatibility with IJE: in FHIR the
"middle name" is stored as the second element in an array of given names. That means that it's not
possible to set a middle name without first setting a first name. The library handles this by

1. Requiring the entire given name (first name and any middle names) to be set all at once when
using the DeathRecord class
2. Raising an exception if a middle name is set before a first name when using the IJEMortality
class
3. Resetting the middle name if the first name is set again when using the IJEMortality class;
setting the first name and then the middle name ensures no issues will occur.

For the decedent's last name, if the family name, denoted as FamilyName in FHIR, is missing or unknown,
its corresponding LNAME in IJE has value of "UNKNOWN". Vice versa, if its LNAME in IJE is "UNKNOWN",
its corresponding FamilyName in FHIR has value of NULL. All other values have 1-to-1 mappings between
FHIR's FamilyName and IJE's LNAME.

#### Helper Properties for Value Sets

For fields that contain coded values it can involve some extra effort to provide the code, the code
system, and the display text. The VRDR library includes some helper methods to make this easier. For
example, here's how to specify the pregnancy status using the long form specifying each of the three
necessary values:

```
// Add PregnancyStatus
Dictionary<string, string> code = new Dictionary<string, string>();
code.Add("code", "2");
code.Add("system", VRDR.CodeSystems.PregnancyStatus);
code.Add("display", "Pregnant at the time of death");
deathRecord.PregnancyStatus = code;
```

Here's a simpler way to accomplish the same thing by using the `PregnancyStatusHelper`:

```
deathRecord.PregnancyStatusHelper = VRDR.ValueSets.PregnancyStatus.Pregnant_At_Time_Of_Death;
```

The helper automatically looks up the correct code system and display string. The following helpers
are available to simplify setting coded values:

* FilingFormatHelper
* ReplaceStatusHelper
* CertificationRoleHelper
* MannerOfDeathTypeHelper
* SexAtDeathHelper
* ResidenceWithinCityLimitsHelper
* Ethnicity1Helper
* Ethnicity2Helper
* Ethnicity3Helper
* Ethnicity4Helper
* RaceMissingValueReasonHelper
* MaritalStatusHelper
* MaritalBypassHelper
* SpouseAliveHelper
* EducationLevelHelper
* EducationLevelEditFlagHelper
* MilitaryServiceHelper
* DecedentDispositionMethodHelper
* AutopsyPerformedIndicatorHelper
* AutopsyResultsAvailableHelper
* DeathLocationTypeHelper
* AgeAtDeathEditBypassFlagHelper
* PregnancyStatusHelper
* PregnancyStatusEditFlagHelper
* ExaminerContactedHelper
* InjuryAtWorkHelper
* TransportationRoleHelper
* TobaccoUseHelper
* ActivityAtDeathHelper
* PlaceOfInjuryHelper
* FirstEditedRaceCodeHelper
* SecondEditedRaceCodeHelper
* ThirdEditedRaceCodeHelper
* FourthEditedRaceCodeHelper
* FifthEditedRaceCodeHelper
* SixthEditedRaceCodeHelper
* SeventhEditedRaceCodeHelper
* EighthEditedRaceCodeHelper
* FirstAmericanIndianRaceCodeHelper
* SecondAmericanIndianRaceCodeHelper
* FirstOtherAsianRaceCodeHelper
* SecondOtherAsianRaceCodeHelper
* FirstOtherPacificIslanderRaceCodeHelper
* SecondOtherPacificIslanderRaceCodeHelper
* FirstOtherRaceCodeHelper
* SecondOtherRaceCodeHelper
* HispanicCodeHelper
* HispanicCodeForLiteralHelper
* RaceRecode40Helper
* IntentionalRejectHelper
* AcmeSystemRejectHelper
* TransaxConversionHelper

#### Helper Properties for Age at Death

In addition to the standard set of coded value fields there are also helper properties for setting
Age at Death. For example, here's how to specify Age at Death using the long form:

```
// Set AgeAtDeath
Dictionary<string, string> age = new Dictionary<string, string>();
age.Add("value", "100");
age.Add("code", "a");
deathRecord.AgeAtDeath = age;
```

Here's a simpler way to accomplish the same thing by using `AgeAtDeathYears`:

```
deathRecord.AgeAtDeathYears = 100;
```

This helper property automatically sets the correct code. The following helper properties are
available to simplify setting Age at Death:

* AgeAtDeathYears
* AgeAtDeathMonths
* AgeAtDeathDays
* AgeAtDeathHours
* AgeAtDeathMinutes

#### FHIR VRDR record to/from IJE Mortality format

An example of converting a VRDR FHIR Death Record to an IJE string:
```cs
using VRDR;

// Read in FHIR Death Record XML file as a string
string xml = File.ReadAllText("./example_vrdr_fhir_record.xml");

// Construct a new DeathRecord object from the string
DeathRecord deathRecord = new DeathRecord(xml);

// Create an IJEMortality instance from the DeathRecord
IJEMortality ije = new IJEMortality(deathRecord);

// Print out the corresponding IJE version of the DeathRecord
string ijeString = ije.ToString(); // Converts DeathRecord to IJE
Console.WriteLine(ijeString);
```

An example of converting an IJE string to a VRDR FHIR Death Record:
```cs
using VRDR;

// Construct a new IJEMortality instance from an IJE string
IJEMortality ije = new IJEMortality("..."); // This will convert the IJE string to a DeathRecord

// Grab the corresponding FHIR DeathRecord
DeathRecord deathRecord = ije.ToDeathRecord();

// Print out the converted FHIR DeathRecord as a JSON string
Console.WriteLine(deathRecord.ToJSON());
```

### VRDR.Messaging

This directory contains classes to create and parse FHIR messages used for Vital Records Death Reporting.

#### Usage

This package is published on NuGet, so including it is as easy as:
```xml
<ItemGroup>
  ...
  <PackageReference Include="VRDR.Messaging" Version="4.0.3" />
  ...
</ItemGroup>
```

Note that the VRDR.Messaging package automatically includes the VRDR package, a project file should not reference both.

You can also include a locally downloaded copy of the library instead of the NuGet version by referencing `VRDRMessaging.csproj` in your project configuration, for example:

```xml
<Project Sdk="Microsoft.NET.Sdk">
  ...
  <ItemGroup>
    <ProjectReference Include="..\VRDR.Messaging\VRDRMessaging.csproj" />
    ...
  </ItemGroup>
</Project>
```

Use of the VRDR.Messaging library to support various message exchange scenarios is described in [`doc/Messaging.md`](doc/Messaging.md).

#### Return Coding Example

An example of producing a `CauseOfDeathCodingResponseMessage` for handling the returned message from
NCHS containing coded causes. For convenience we can create the record either using IJE-style
properties on an IJEMortality record (demonstrated below) or using FHIR IG properties on a
DeathRecord record. For a complete example, [click here](doc/Messaging.md#return-coding).

```cs
using VRDR;

// Create an empty IJE Mortality record
IJEMortality ije = new IJEMortality();

// Populate the IJE fields
ije.DOD_YR = "2022";
ije.DSTATE = "YC";
ije.FILENO = "123";
ije.AUXNO = "500";
ije.ACME_UC = "T273";
ije.RAC = "T273 T270 ";
ije.EAC = "11T273  21T270 &";
ije.R_YR = "2019";
ije.R_MO = "11";
ije.R_DY = "14";
ije.INT_REJ = "4";
ije.SYS_REJ = "3";
ije.TRX_FLG = "5";
ije.MANNER = "A";
ije.INJPL = "7";
ije.DOI_YR = "2022";
ije.DOI_MO = "1";
ije.DOI_DY = "15";

// Populate the fields that only appear in TRX records and not IJE
ije.trx.CS = "8";
ije.trx.SHIP = "876";

// Create the message
CauseOfDeathCodingMessage message = new CauseOfDeathCodingMessage(ije.ToDeathRecord());

// Set the source and destination
message.MessageSource = "http://nchs.cdc.gov/vrdr_submission";
// Set a single destination using MessageDestination (not plural).
message.MessageDestination = "https://example.org/jurisdiction/endpoint";
// Set multiple destinations with a comma separated list where each endpoint in the string is seperated by a comma. Use MessageDestinations (plural).
// Messages without NCHS in this list of destinations will not be sent to NCHS.
message.MessageDestinations = "https://example.org/jurisdiction/endpoint,https://example.org/jurisdiction/endpoint";

// Create a JSON representation of the coding response message
string jsonMessage = message.ToJSON();
```

### VRDR.Tests

This directory contains unit and functional tests for the VRDR library as well as scripts for testing via the CLI and translation microservice.

#### Usage

The tests are automatically run by the repository GitHub workflows config, but can be run locally by executing the following command in the root project directory:

```bash
./VRDR.Tests/run_tests.sh
```

The C# tests can be run separately from the other tests by executing the following command:

```bash
dotnet test
```

#### Filtering tests
Tests have been added to test the filtering process as well as the parsing of filtered files to ensure a valid file is generated from filtering.
These tests will run with the above commands.

##### Filtering tests description

**ADDRESS_DShouldEqual**: Tests that the `DeathLocationAddress` field isn't filtered out.  
**LIMITSShouldEqual_1**: Tests that the `ResidenceWithinCityLimits` field isn't filtered out. [Related to ticket: https://ruvos.atlassian.net/browse/STEVESD-2582]  
**LIMITSShouldEqual_2**: Tests that the `ResidenceWithinCityLimits` field isn't filtered out. [Related to ticket: https://ruvos.atlassian.net/browse/STEVESD-2582]  
**PreFilteredFileEqualsFilteredFile**: Tests that filtering through all fields in a file results in all fields being the same before and after filtering.  
**FilteringNoFields**: Tests filtering no fields results in a valid Death Record.  
**FilteringPlusParsingTest_1**: Tests filtering results in a valid Death Record.  
**FilteringPlusParsingTest_2**: Tests filtering results in a valid Death Record.  
**FilteringPlusParsingTest_3**: Tests filtering results in a valid Death Record.  
**FilteringPlusParsingTest_4**: Tests filtering results in a valid Death Record.  
**FilteringPlusParsingTest_5**: Tests filtering results in a valid Death Record.  
**FilterAllFields_1**: Tests filtering all fields results in a valid Death Record.  
**FilterAllFields_2**: Tests filtering all fields results in a valid Death Record.  
**FilterAllFields_3**: Tests filtering all fields results in a valid Death Record.  
**FilterFilePerJurisdictionFilters**: Tests that each jurisdictions filter results in a valid Death Record.  

### VRDR.CLI
This directory contains a sample command line interface app that uses the VRDR library to do a few different things.

#### Example Usages

```bash
# Prints CLI Help
dotnet run -- project VRDR.CLI help

# Builds a fake death record and print out the record as FHIR XML and JSON
dotnet run --project VRDR.CLI

# Read in the FHIR XML or JSON death record and print out as IJE
dotnet run --project VRDR.CLI 2ije VRDR.CLI/1.xml

# Read in the IJE death record and print out as FHIR XML
dotnet run --project VRDR.CLI ije2xml VRDR.CLI/1.MOR

# Read in the IJE death record and print out as FHIR JSON
dotnet run --project VRDR.CLI ije2json VRDR.CLI/1.MOR

# Read in the FHIR XML death record and print out as FHIR JSON
dotnet run --project VRDR.CLI xml2json VRDR.CLI/1.xml

# Read in the FHIR JSON death record and print out as FHIR XML
dotnet run --project VRDR.CLI json2xml VRDR.CLI/1.json

# Read in the FHIR JSON death record, completely disassemble then reassemble, and print as FHIR JSON
dotnet run --project VRDR.CLI json2json VRDR.CLI/1.json

# Read in the FHIR XML death record, completely disassemble then reassemble, and print as FHIR XML
dotnet run --project VRDR.CLI xml2xml VRDR.CLI/1.xml

# Read in the given FHIR xml (being permissive) and print out the same; useful for doing validation diffs
dotnet run --project VRDR.CLI checkXml VRDR.CLI/1.xml

# Read in the given FHIR json (being permissive) and print out the same; useful for doing validation diffs
dotnet run --project VRDR.CLI checkJson VRDR.CLI/1.json

# Read in and parse an IJE death record and print out the values for every (supported) field
dotnet run --project VRDR.CLI ije VRDR.CLI/1.MOR

# Generate the first connectathon record with certifcate number 100 and jurisdiction MA
dotnet run --project VRDR.CLI connectathon 1 100 MA

# Generate records for bulk testing based on the 3 connectathon testing records.
# Parameters are:
#    - initial certificate number
#    - number of records to generate (each with cert_no one greater than its predecessor)
#	 - Submitting jurisdiction
#    - output directory (must exist)
#    
dotnet run --project VRDR.CLI generaterecords 23 100 CT ./generatedrecords 

# Generate a verbose JSON description of the record (in the format used to drive Canary)
dotnet run --project VRDR.CLI description VRDR.CLI/1.json

# Dump content of a death record in key/value IJE format
dotnet run --project VRDR.CLI 2ijecontent VRDR.CLI/1.json

# Convert a record to IJE and back to identify any conversion issues
dotnet run --project VRDR.CLI roundtrip-ije VRDR.CLI/1.json

# Convert a record to JSON and back to identify any conversion issues
dotnet run --project VRDR.CLI roundtrip-all VRDR.CLI/1.json

# Create a record using the provided IJE field name and value pairs
dotnet run --project VRDR.CLI ijebuilder GNAME=Lazarus AGE=990

# Compare an IJE record with a FHIR record by each IJE field
dotnet run --project VRDR.CLI compare VRDR.CLI/1.MOR VRDR.CLI/1.json

# Extract a FHIR record from a FHIR message
dotnet run --project VRDR.CLI extract VRDR.CLI/1submit.json

# Dump content of a submission message in key/value IJE format
dotnet run --project VRDR.CLI extract2ijecontent VRDR.CLI/1submit.json

# Create a submission FHIR message wrapping a FHIR record
dotnet run --project VRDR.CLI submit VRDR.CLI/1.json

# Create an update FHIR message wrapping a FHIR record
dotnet run --project VRDR.CLI resubmit VRDR.CLI/1.json

# Create an acknowledgement FHIR message for a submission FHIR message
dotnet run --project VRDR.CLI ack VRDR.CLI/1submit.json

# Create an alias FHIR message for a FHIR death record (1 argument: FHIR death record)
dotnet run --project VRDR.CLI alias VRDR.Tests/fixtures/json/DeathRecord1.json

# Creates a void message for a Death Record (1 argument: FHIR death record; one optional argument: number of records to void)
dotnet run --project VRDR.CLI void VRDR.Tests/fixtures/json/DeathRecord1.json

# Extract and show the codes in a coding response message
dotnet run --project VRDR.CLI showcodes VRDR.CLI/1coding.json

# Filter file and write output as `filteredFile.json`
dotnet run --project VRDR.CLI filter VRDR.CLI/1coding.json
```

### VRDR.Client

This directory contains classes and functions to connect to the [NVSS API server](https://github.com/nightingaleproject/Reference-NCHS-API), authenticate, manage authentication tokens, post records, and retrieve responses. For a full implementation of a client service that uses this library, see the [Reference Client Implementation](https://github.com/nightingaleproject/Reference-Client-API).

*This package is not yet published on NuGet.*

Note that the VRDR.Client package automatically includes the VRDR package and the VRDR Messaging package, a project file should not reference both.

You can include a locally downloaded copy of the library instead of the NuGet version by referencing `VRDRClient.csproj` in your project configuration, for example:

```xml
<Project Sdk="Microsoft.NET.Sdk">
  ...
  <ItemGroup>
    <ProjectReference Include="..\VRDR.Client\VRDRClient.csproj" />
    ...
  </ItemGroup>
</Project>
```

#### Example Usage

Using VOID and ALIAS fields of IJEMortality object. Use case: VOID is for voiding the Death Record, and should be sent using a DeathRecordVoidMessage. ALIAS is for denoting optional records submitted only for National Death Index purposes, and should be sent using a DeathRecordAliasMessage. Both are mainly sent by Vital Records Jurisdiction.
```
  // Example getting values of VOID and ALIAS fields, respectively
  IJEMortality ijeMortality = new IJEMortality(ijeFile);
  string voidFieldValue = ijeMortality.VOID;
  string aliasFieldValue = ijeMortality.ALIAS;

  // Example setting values of VOID and ALIAS fields, respectively
  IJEMortality ijeMortality = new IJEMortality(ijeFile);
  ijeMortality.VOID = "0"; // flag "OFF"
  ijeMortality.VOID = "1";  // flag "ON"
  ijeMortality.ALIAS = "0"; // flag "OFF"
  ijeMortality.ALIAS = "1"; // flag "ON"

Using MessageBundle of BaseMessage. Use case: as a FHIR Bundle, it is for initializing various message types
```
  // Example get MessageBundle after setting it (in BaseMessage) by constructor
  DeathRecordSubmissionMessage message = new DeathRecordSubmissionMessage();
  Bundle messageBundle = message.MessageBundle;
  or
  BaseMessage message = new DeathRecordUpdateMessage();
  Bundle messageBundle = message.MessageBundle;
  
Authenticate to the NVSS API Server
```
  // Example SAMS credentials
  string authUrl = "https://<authServerUrl>.gov/auth/oauth/v2/token";
  string clientId = "Client-id-from-sams";
  string clientSecret = "Client-secret-from-sams";
  string username = "your-sams-username";
  string pass = "your-sams-password";

  // ... Create the credentials and client intance
  Credentials credentials = new Credentials(authUrl, clientId, clientSecret, username, pass);
  string apiUrl = "https://<thenvssapiserverurl>.gov/OSELS/NCHS/NVSSFHIRAPI/<Jurisidction-Id>/Bundles";
  Boolean localDev = false; // false when testing against the NVSS API Server
  client = new Client(apiUrl, localDev, credentials);
```

POST a FHIR Message to the NVSS API Server with your authenticated client
```
  // ... Create a FHIR Message
  BaseMessage msg = new BaseMessage();
  HttpResponseMessage response = client.PostMessageAsync(msg);
  // ... handle success or failure
```

GET record responses from the NVSS API Server with your authenticated client
```
  // lastUpdated is a timestamp of the last GET request
  lastUpdatedStr = lastUpdated.ToString("yyyy-MM-ddTHH:mm:ss.fffffff");
  var content = client.GetMessageResponsesAsync(lastUpdatedStr);

  // ...parse the Bundle of Bundles in the content response
```

Generate a batch Bundle of FHIR Messages for upload to the NVSS API Server
```
  // ... Create FHIR Messages and put them into a List
  List<BaseMessage> messages = new List<BaseMessage>();
  for (int i = 0; i < 5; i++)
  {
    BaseMessage msg = new BaseMessage();
    messages.Add(msg);
  }
  // Create the batch upload JSON
  string batch = Client.CreateBulkUploadPayload(messages, "/Bundle", true);
```

POST a batch of FHIR Message to the NVSS API Server with your authenticated client
```
  // ... Create FHIR Messages and put them into a List
  List<BaseMessage> messages = new List<BaseMessage>();
  for (int i = 0; i < 50; i++)
  {
    BaseMessage msg = new BaseMessage();
    messages.Add(msg);
  }
  // POST messages in batches of 20
  List<HttpResponseMessage> responses = client.PostMessagesAsync(messages, 20);
  // ... handle success or failure
```

#### For Library Developers

Attributes (equivalent to Annotations in Java) are used in .NET to promote loose coupling via  “declarative” programming, and add Metadata to the target program entity, namely .NET assembly, module, for global scope, and class, interface, struct, enum, constructor, delegate, field, property, method, parameter, return value, and event, for non-global scope. They can be either built-in or custom, and denoted by pair or pair(s), for mutltiple attributes, of square brackets [...] surrounding the target entity. As shown in [VRDR/DeathRecord_submissionProperties.cs](../master/VRDR/DeathRecord_submissionProperties.cs), the custom attributes [Property(...)] and [FHIRPath(...)] for each of the DeathRecord's properties, and [PropertyParam(...)] for many of its properties, add relevant sets of Metadata to their targets, based on their definitions and orders of formal parameters given in [VRDR/DeathRecord_util.cs](../master/VRDR/DeathRecord_util.cs), where custom attribute [Property(...)], as in
```
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
                if (String.IsNullOrWhiteSpace(value))
                {
                    return;
                }
                if (Bundle.Identifier == null)
                {
                    Bundle.Identifier = new Identifier();
                }
                Bundle.Identifier.Value = value;
                Bundle.Identifier.System = "http://nchs.cdc.gov/vrdr_id";
            }
        }
```
for example, can be seen mapped to the following custom attribute class with the same name:

`public class Property : System.Attribute`

and with the following constructor:

`public Property(string name, Types type, string category, string description, bool serialize, string igurl, bool capturedInIJE, int priority = 4)`

Similarly, custom attribute [FHIRPath( ... )] is mapped to custom attribute class FHIRPath with public constructor `FHIRPath(string path, string element)`

and custom attribute [PropertyParam( ... )] is mapped to custom attribute class PropertyParam with public constructor `PropertyParam(string key, string description)`

Custom attribute classes are typically derived, either directly or indirectly, from built-in abstract class System.Attribute, just as illustrated here.

The property values of these Metadata/attributes for DeathRecord are set and retrieved via setters and getters, respectively, based on individual sets of rules also as shown in [VRDR/DeathRecord_submissionProperties.cs](../master/VRDR/DeathRecord_submissionProperties.cs)

Snippet from [VRDR.CLI/Program.cs](../master/VRDR.CLI/Program.cs#L479-L489) gives an example of how these custom attributes can be used:
```
DeathRecord d = new DeathRecord(File.ReadAllText(args[1]));
IJEMortality ije1 = new IJEMortality(d, false);
// Loop over every property (these are the fields); Order by priority
List<PropertyInfo> properties = typeof(IJEMortality).GetProperties().ToList().OrderBy(p => p.GetCustomAttribute<IJEField>().Location).ToList();
foreach (PropertyInfo property in properties)
{
    // Grab the field attributes
    IJEField info = property.GetCustomAttribute<IJEField>();
    // Grab the field value
    string field = Convert.ToString(property.GetValue(ije1, null));
}   
```
Custom attributes are also used extensively in IJEField's properties, one of which is shown below as an example.
```
        [IJEField(1, 1, 4, "Date of Death--Year", "DOD_YR", 1)]
        public string DOD_YR
        {
            get
            {
                return NumericAllowingUnknown_Get("DOD_YR", "DeathYear");
            }
            set
            {
                NumericAllowingUnknown_Set("DOD_YR", "DeathYear", value);
            }
        }
```
which is mapped to the following custom attribute class with the same name:

`public class IJEField : System.Attribute`

and with the following constructor:

`public IJEField(int field, int location, int length, string contents, string name, int priority)` in the same file [VRDR/IJEMortality.cs](../master/VRDR/IJEMortality.cs)


Official resources:<br/>
https://learn.microsoft.com/en-us/dotnet/csharp/advanced-topics/reflection-and-attributes/attribute-tutorial<br/>
https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/attributes<br/>
https://learn.microsoft.com/en-us/dotnet/csharp/advanced-topics/reflection-and-attributes/creating-custom-attributes<br/>
https://learn.microsoft.com/en-us/dotnet/standard/attributes/writing-custom-attributes<br/><br/>


### VRDR.HTTP
This directory contains a deployable microservice that exposes endpoints for conversion of IJE flat files to DeathRecord JSON or XML, and vice versa.

The current available endpoints to `POST` to are:
```
http://<server>:8080/xml   <- For requesting a record as FHIR in XML format
http://<server>:8080/json  <- For requesting a record as FHIR in JSON format
http://<server>:8080/ije   <- For requesting a record as IJE
```

Include a `Content-Type` header indicating what format the record is represented as in the body of the message (e.g. `application/fhir+json`, `application/fhir+xml`, or `application/ije`.).

#### Running

To build a Dockerized version from scratch (from source), you can do so by running (inside the project root directory):

```
dotnet publish
docker build -t vrdr-microservice -f ./VRDR.HTTP/Dockerfile .
docker run -p 8080:8080 vrdr-microservice
```

If you prefer not to use Docker, you can run it from the root project directory using [.NET Core](https://dotnet.microsoft.com/download):

```
dotnet run --project VRDR.HTTP
```

The service will be listening locally at `http://localhost:8080`.

## Contributing
This repository follows [Semantic Versioning](https://semver.org/). Bug fixes and feature enhancements should be merged into the `master` branch via Pull Requests (PR), instead of directly committing to the `master` branch. Once a PR is merged, a new version of the library will be automatically published to NuGet.

```
git pull origin master
git checkout -b <your-working-branch-name>
<commit-your-changes>
<test-with-changes-from-master>
git push -u origin <your-working-branch-name>
```
Once the working branch is pushed to the respository, follow these steps:

1. Create a new PR with the working branch as base, and master as head.
1. The PR title and description should follow the [Conventional Commit](https://www.conventionalcommits.org/en/v1.0.0/) format. The PR title should be structured as  `<type>[optional scope]: <short-message>`, where a type can be:
  - **feat:** introduces a new feature to the codebase (correlates with MINOR in Semantic Versioning).
  - **fix:** patches a bug in the codebase (correlates with PATCH in Semantic Versioning).
  - Other types such as `build`, `chore`, `ci`, `docs`, `style`, `refactor`, `perf`, `test`, and others are allowed as well (correlates with PATCH in Semantic Versioning).
  
  (The PR title needs to be concise and conform to the style guide for change tracking purposes. The PR description can include additional details about the changes associated with this PR.)
1. Assign one or more reviewers to review your changes. At least one approved review is required before the PR can be merged.
1. If the PR addresses an existing Issue, link the PR with the Issue to resolve it through the PR.
1. Once the PR is approved by a reviewer, with all discussions resolved and all checks passed, click the Squash and Merge button. Avoid using "Create a merge commit." The PR title and description will automatically fill in the commit message boxes.

#### Release Pull Request

With each commit to the default branch, a release pull request will be automatically created/updated. This PR increments the package version and updates the CHANGELOG using a special branch named `actions/release`. You don't need to wait for this special PR to be merged into the default branch before merging additional pull requests. It consolidates subsequent commits to the default branch; only one instance of release pull request will be active.


#### Publishing a Version

To create a new release of VRDR on NuGet:

1. Bump the version of the libraries listed in the [Directory.Build.props](Directory.Build.props) file. Whenever a commit is merged into the master branch that changes the Directory.Build.props file, [Github Actions](.github/workflows/publish.yml) will automatically build and publish a new version of the package based on the value specified.
1. Update the version numbers listed in this README
1. Update the CHANGELOG.md file with information on what is changing in the release
1. Merge the above changes to master, causing the GitHub publishing workflow to fire
1. Create a GitHub release
    1. Go to the [Releases page](https://github.com/nightingaleproject/vrdr-dotnet/releases)
    1. Click on "Draft a new release"
    1. Enter the release version on the tag and release; this should be the same as in the Directory.Build.props file (e.g., v3.2.0-preview3)
    1. Copy the information from the CHANGELOG.md file from this version into the release description
    1. Do not check the "pre-release" button, even for preview releases, since those don't show up on the main GitHub page

## License

Copyright 2018, 2019, 2020, 2021, 2022 The MITRE Corporation

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance with the License. You may obtain a copy of the License at

```
http://www.apache.org/licenses/LICENSE-2.0
```

Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the License for the specific language governing permissions and limitations under the License.

### Contact Information

For questions or comments about vrdr-dotnet, please send email to

    nvssmodernization@cdc.gov
