# Using the VRDR.Filter Library

This document describes how to use the VRDR.Filter library.

## Instantiating the filter class

Before the filter class can be used, we need to instantiate an instance of the class with 3 arguments.

#### Argument 1:
`nchsIjeFilterFileLocation`: This is the location of the filter file. This file will contain an array of IJE fields that we want
in the filtered file.
```text
["DOD_YR","DSTATE","FILENO","VOID","AUXNO","MFILED"...]
```

#### Argument 2:
`ijeToFhirMappingFileLocation`: This is the location of the IJE to FHIR fields mapping file. This file will contain an object with
IJE keys and FHIR array objects. We need this mapping so know which FHIR fields to exclude when filtering out an IJE field.
In the future we will only have an array for FHIR fields to filter.
```text
{
  "DOD_YR": [
    "DeathRecordIdentifier",
    "DeathYear"
  ],
  "DSTATE": [
    "DeathRecordIdentifier",
    "DeathLocationJurisdiction",
    "DeathLocationAddress.addressState",
    "DeathLocationAddress.addressJurisdiction"
  ],
  ...
}
```

#### Argument 3:
`filterIsFile`: This field indicates if the first argument is a file location or a string representation of the filter array.
It is defaulted to true.