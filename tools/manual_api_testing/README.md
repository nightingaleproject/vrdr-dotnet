# Manual API testing

This directory contains tools for manually interacting with a VRDR FHIR API.

* push.rb - submit one or more messages to the NVSS API
    * Takes a flag --single that means use a single process rather than running multiple submitter processes
    * Takes a flag --sequential that means send individual messages rather than using the bulk upload feature

* pull_and_process.rb - pull messages from the NVSS API and process them

Both files require OAuth credentials to be in a local YAML file called config.yml with the following
format:

```yaml
---
client_id: <client-id>
client_secret: <client-secret>
username: <username>
password: <password>
```

Note: if any of these fields have special characters they can be enclosed in single quotes.

* status.rb - after running pull_and_process.rb this script analyzes the results

## Typical usage patterns

### Simple API test

Generate a few records:

```bash
dotnet run --project ~/git/vrdr-dotnet/VRDR.CLI generaterecords 118 3 TT . 2022
```

Wrap them in messages:

```bash
dotnet run --project ~/git/vrdr-dotnet/VRDR.CLI submit . *.json
```

Submit them:

```bash
ruby push.rb TT *_submission.json
```

Look for responses:

```bash
ruby pull_and_process.rb TT
```

### Volume testing

Follow the same steps as the simple API test but generate more records for the first step, e.g., generating 100 records:

```bash
dotnet run --project ~/git/vrdr-dotnet/VRDR.CLI generaterecords 2 100 TT . 2023
```