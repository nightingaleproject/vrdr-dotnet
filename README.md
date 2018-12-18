[![Build Status](https://travis-ci.org/nightingaleproject/csharp-fhir-death-record.svg?branch=master)](https://travis-ci.org/nightingaleproject/csharp-fhir-death-record)

# csharp-fhir-death-record
This repository includes C# code for producing and consuming the preliminary version of the Standard Death Record (SDR) Health Level 7 (HL7) Fast Healthcare Interoperability Resources (FHIR). [Click here to view the generated FHIR IG](https://nightingaleproject.github.io/fhir-death-record). This code also includes support for converting FHIR SDRs to and from the Inter-Jurisdictional Exchange (IJE) Mortality format, as well as companion microservice for performing conversions.

## Project Organization

### FhirDeathRecord
This directory contains a FHIR Death Record library for consuming and producing Standard Death Records. This library also includes support for converting to and from the Inter-Jurisdictional Exchange (IJE) Mortality format.

#### Usage
You can include the library by referencing it in your project configuration, for example (taken from FhirDeathRecord.CLI):
```xml
<Project Sdk="Microsoft.NET.Sdk">
  ...
  <ItemGroup>
    <ProjectReference Include="..\FhirDeathRecord\DeathRecord.csproj" />
    ...
  </ItemGroup>
</Project>
```

#### Consuming Example
A quick example of consuming a SDR FHIR document (in XML format) using this library, and printing some details from it:
```cs
// Read in FHIR Death Record XML file as a string
string xml = File.ReadAllText("./example_sdr_fhir.xml");

// Construct a new DeathRecord object from the SDR XML string
DeathRecord deathRecord = new DeathRecord(xml);

// Print out some details from the record
Console.WriteLine($"Decedent's First Name: {deathRecord.FirstName}");
Console.WriteLine($"Decedent's Last Name: {deathRecord.FamilyName}");

Console.WriteLine($"Was an Autopsy Performed?: {deathRecord.AutopsyPerformed}");

Console.WriteLine($"Cause of Death Part I, Line a: {deathRecord.COD1A}");
Console.WriteLine($"Cause of Death Part I Interval, Line a: {deathRecord.INTERVAL1A}");
Console.WriteLine($"Cause of Death Part I Code, Line a: {String.Join(", ", deathRecord.CODE1A.Select(x => x.Key + "=" + x.Value).ToArray())}");
```

#### Producing Example
A quick example of producing a from-scratch SDR FHIR document using this library, and then printing it out as a JSON string:
```cs
DeathRecord deathRecord = new DeathRecord();

// Set Death Record ID
deathRecord.Id = "42";

// Add Decedent Given Names
string[] givenNames = {"First", "Middle"};
deathRecord.GivenNames = givenNames;

// Add Decedent Last Name
deathRecord.FamilyName = "Last";

// Cause of Death Part I, Line a
deathRecord.COD1A = "Rupture of myocardium";

// Cause of Death Part I Interval, Line a
deathRecord.INTERVAL1A = "Minutes";

// Cause of Death Part I Code, Line a
Dictionary<string, string> exampleCode = new Dictionary<string, string>();
code.Add("code", "I21.0");
code.Add("system", "ICD-10");
code.Add("display", "Acute transmural myocardial infarction of anterior wall");
deathRecord.CODE1A = exampleCode;

// Add TimingOfRecentPregnancyInRelationToDeath
Dictionary<string, string> code = new Dictionary<string, string>();
code.Add("code", "PHC1260");
code.Add("system", "urn:oid:2.16.840.1.114222.4.11.6003");
code.Add("display", "Not pregnant within past year");
deathRecord.TimingOfRecentPregnancyInRelationToDeath = code;

// Add MedicalExaminerContacted
deathRecord.MedicalExaminerContacted = false;

// Add DatePronouncedDead
deathRecord.DatePronouncedDead = "2018-07-10T10:04:00.0000000+00:00";

// Print record as a JSON string
Console.WriteLine(deathRecord.ToJSON());
```

#### FHIR SDR to/from IJE Mortality format
A quick example of converting a FHIR Death Record to an IJE string:
```cs
// Read in FHIR Death Record XML file as a string
string xml = File.ReadAllText("./example_sdr_fhir.xml");

// Construct a new DeathRecord object from the string
DeathRecord deathRecord = new DeathRecord(xml);

// Create an IJEMortality instance from the DeathRecord
IJEMortality ije = new IJEMortality(deathRecord);

// Print out the corresponding IJE version of the DeathRecord
string ijeString = ije.ToString(); // Converts DeathRecord to IJE
Console.WriteLine(ijeString);
```

A quick example of converting an IJE string to a FHIR Death Record:
```cs
// Construct a new IJEMortality instance from an IJE string
IJEMortality ije = new IJEMortality("..."); // This will convert the IJE string to a DeathRecord

// Grab the corresponding FHIR DeathRecord
DeathRecord deathRecord = ije.ToDeathRecord();

// Print out the converted FHIR DeathRecord as a JSON string
Console.WriteLine(deathRecord.ToJSON());
```

### FhirDeathRecord.Tests
This directory contains unit and functional tests for the FhirDeathRecord library.

#### Usage
The tests are automatically run by this repositories Travis CI config, but can be run locally by executing the following command in the root project directory:
```bash
dotnet test FhirDeathRecord.Tests/DeathRecord.Tests.csproj
```

### FhirDeathRecord.CLI
This directory contains a sample command line interface app that uses the FhirDeathRecord library to do a few different things.

#### Example Usages
```bash
# Builds a fake death record and print out the record as FHIR XML and JSON
dotnet run

# Read in the FHIR XML or JSON death record and print out as IJE
dotnet run 2ije 1.xml

# Read in the IJE death record and print out as FHIR XML
dotnet run ije2xml 1.MOR

# Read in the IJE death record and print out as FHIR JSON
dotnet run ije2json 1.MOR

# Read in the FHIR XML death record and print out as FHIR JSON
dotnet run xml2json 1.xml

# Read in the FHIR JSON death record and print out as FHIR XML
dotnet run json2xml 1.json

# Read in the FHIR JSON death record, completely disassemble then reassemble, and print as FHIR JSON
dotnet run json2json 1.json

# Read in the FHIR XML death record, completely disassemble then reassemble, and print as FHIR XML
dotnet run xml2xml 1.xml

# Read in and parse the FHIR XML or JSON death record and print out some general details about it
dotnet run 1.xml

# Read in and parse an IJE death record and print out the values for every (supported) field
dotnet run ije 1.MOR
```

### FhirDeathRecord.HTTP
This directory contains a deployable microservice that exposes endpoints for conversion of IJE flat files to DeathRecord JSON or XML, and vice versa.

The current available endpoints to `POST` to are:
```
http://<server>:8080/xml   <- For requesting a record as FHIR in XML format
http://<server>:8080/json  <- For requesting a record as FHIR in JSON format
http://<server>:8080/ije   <- For requesting a record as IJE
```

Include a `Content-Type` header indicating what format the record is represented as in the body of the message (e.g. `application/fhir+json`, `application/fhir+xml`, or `application/ije`.).

#### Usage (Local)
Example usage (executed inside the FhirDeathRecord.HTTP directory):
```bash
dotnet run
```

This will launch the microservice locally, which will be listening on port 8080.

#### Usage (docker)
Note, this requires a locally running instance of docker on the machine for the service to be deployed on.

```bash
dotnet publish
docker build -t fhirdeath .
docker run -p 8080:8080 fhirdeath
```
