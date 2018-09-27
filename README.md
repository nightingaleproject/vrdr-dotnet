[![Build Status](https://travis-ci.org/nightingaleproject/csharp-fhir-death-record.svg?branch=master)](https://travis-ci.org/nightingaleproject/csharp-fhir-death-record)

# csharp-fhir-death-record
This repository includes C# code for producing and consuming the preliminary version of the Standard Death Record (SDR) Health Level 7 (HL7) Fast Healthcare Interoperability Resources (FHIR). [Click here to view the generated FHIR IG](https://nightingaleproject.github.io/fhir-death-record).

## Project Organization

### FhirDeathRecord
This directory contains a FHIR Death Record library for consuming and producing Standard Death Records.

#### Usage
You can include the library by referencing it in your project configuration, for example (taken from FhirDeathRecord.CLI):
```
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
```
// Read in XML file as a string
string xml = File.ReadAllText("./example_sdr_fhir.xml");

// Construct a new DeathRecord object from the SDR XML string
DeathRecord deathRecord = new DeathRecord(xml);

// Print out some details from the record
Console.WriteLine($"Decedent's Given Name(s): {deathRecord.GivenNames}");
Console.WriteLine($"Decedent's Last Name: {deathRecord.FamilyName}");

Console.WriteLine($"Autopsy Performed: {deathRecord.AutopsyPerformed}");

Tuple<string, string, Dictionary<string, string>>[] causes = deathRecord.CausesOfDeath;
foreach (var cause in causes)
{
  Console.WriteLine($"Cause: {cause.Item1}, Onset: {cause.Item2}, Coding: {cause.Item3}");
}
```

#### Producing Example
A quick example of producing a from-scratch SDR FHIR document using this library, and then printing it out as a JSON string:
```
DeathRecord deathRecord = new DeathRecord();

// Set Death Record ID
deathRecord.Id = "42";

// Add Decedent Given Names
string[] givenNames = {"First", "Middle"};
deathRecord.GivenNames = givenNames;

// Add Decedent Last Name
deathRecord.FamilyName = "Last";

// Add Causes Of Death
Tuple<string, string, Dictionary<string, string>>[] causes =
{
  Tuple.Create("Example Immediate COD", "minutes", new Dictionary<string, string>(){ {"code", "1234"}, {"system", "example"} }), // Include a fake code
  Tuple.Create("Example Underlying COD 1", "2 hours", new Dictionary<string, string>()),
  Tuple.Create("Example Underlying COD 2", "5 weeks", new Dictionary<string, string>()),
  Tuple.Create("Example Underlying COD 3", "30 years", new Dictionary<string, string>())
};
deathRecord.CausesOfDeath = causes;

// Add TimingOfRecentPregnancyInRelationToDeath
Dictionary<string, string> code = new Dictionary<string, string>();
code.Add("code", "PHC1260");
code.Add("system", "http://github.com/nightingaleproject/fhirDeathRecord/sdr/causeOfDeath/vs/PregnancyStatusVS");
code.Add("display", "Not pregnant within past year");
deathRecord.TimingOfRecentPregnancyInRelationToDeath = code;

// Add MedicalExaminerContacted
deathRecord.MedicalExaminerContacted = false;

// Add DatePronouncedDead
deathRecord.DatePronouncedDead = "2018-09-01T00:00:00+04:00";

// Print record as a JSON string
Console.WriteLine(deathRecord.ToJSON());
```

### FhirDeathRecord.Tests
This directory contains unit and functional tests for the FhirDeathRecord library.

#### Usage
The tests are automatically run by this repositories Travis CI config, but can be run locally by executing the following command in the root project directory:
```
dotnet test FhirDeathRecord.Tests/DeathRecord.Tests.csproj
```

### FhirDeathRecord.CLI
This directory contains a sample app that uses the FhirDeathRecord library. The app is a simple command line utility that takes a single parameter (filepath to a SDR FHIR file, either json or xml) and parses it, then prints what it found to standard out.

#### Usage
Example usage (executed inside the FhirDeathRecord.CLI directory):
```
dotnet run 1.xml
```
