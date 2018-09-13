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
