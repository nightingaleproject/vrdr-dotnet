using System;
using System.Net;
using System.Threading;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.ServiceProcess;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using VRDR;

namespace VRDR.HTTP
{
    /// <summary>Utility for translating between the Nightingale format and the FHIR VRDR format.</summary>
    public class Nightingale
    {
        /// <summary>Convert FHIR VRDR <c>DeathRecord</c> to Nightingale style JSON string.</summary>
        public static string ToNightingale(DeathRecord record)
        {
            Dictionary<string, string> values = new Dictionary<string, string>();

            SetStringValueDictionary(values, "certificateNumber", record.Identifier);
            SetStringValueDictionary(values, "deathLocationJurisdiction", record.DeathLocationJurisdiction);

            SetYesNoValueDictionary(values, "armedForcesService.armedForcesService", record, "MilitaryService");
            SetYesNoValueDictionary(values, "autopsyPerformed.autopsyPerformed", record, "AutopsyPerformedIndicator");
            SetYesNoValueDictionary(values, "autopsyAvailableToCompleteCauseOfDeath.autopsyAvailableToCompleteCauseOfDeath", record, "AutopsyResultsAvailable");
            SetYesNoValueDictionary(values, "didTobaccoUseContributeToDeath.didTobaccoUseContributeToDeath", record, "TobaccoUse");
            SetYesNoValueDictionary(values, "meOrCoronerContacted.meOrCoronerContacted", record, "ExaminerContacted");

            if (GetValueDict(record.CertificationRole, "code") == "434641000124105")
            {
                values["certifierType.certifierType"] = "Physician (Certifier)";
            }
            else if (GetValueDict(record.CertificationRole, "code") == "434651000124107")
            {
                values["certifierType.certifierType"] = "Physician (Pronouncer and Certifier)";
            }
            else if (GetValueDict(record.CertificationRole, "code") == "440051000124108")
            {
                values["certifierType.certifierType"] = "Medical Examiner";
            }

            SetStringValueDictionary(values, "cod.immediate", record.COD1A);
            SetStringValueDictionary(values, "cod.immediateInt", record.INTERVAL1A);
            SetStringValueDictionary(values, "cod.under1", record.COD1B);
            SetStringValueDictionary(values, "cod.under1Int", record.INTERVAL1B);
            SetStringValueDictionary(values, "cod.under2", record.COD1C);
            SetStringValueDictionary(values, "cod.under2Int", record.INTERVAL1C);
            SetStringValueDictionary(values, "cod.under3", record.COD1D);
            SetStringValueDictionary(values, "cod.under3Int", record.INTERVAL1D);

            SetStringValueDictionary(values, "contributingCauses.contributingCauses", record.ContributingConditions);

            SetDateValueDictionary(values, "dateOfBirth.dateOfBirth", record.DateOfBirth);
            SetDateValueDictionary(values, "dateOfDeath.dateOfDeath", record.DateOfDeath);
            SetDateValueDictionary(values, "datePronouncedDead.datePronouncedDead", record.DateOfDeathPronouncement);

            SetDictionaryValueDictionary(record.Residence, "addressLine1", values, "decedentAddress.street");
            SetDictionaryValueDictionary(record.Residence, "addressCity", values, "decedentAddress.city");
            SetDictionaryValueDictionary(record.Residence, "addressState", values, "decedentAddress.state");
            SetDictionaryValueDictionary(record.Residence, "addressZip", values, "decedentAddress.zip");

            SetStringValueDictionary(values, "detailsOfInjuryLocation.name", record.InjuryLocationName);
            SetDictionaryValueDictionary(record.InjuryLocationAddress, "addressLine1", values, "detailsOfInjuryLocation.street");
            SetDictionaryValueDictionary(record.InjuryLocationAddress, "addressCity", values, "detailsOfInjuryLocation.city");
            SetDictionaryValueDictionary(record.InjuryLocationAddress, "addressState", values, "detailsOfInjuryLocation.state");
            SetDictionaryValueDictionary(record.InjuryLocationAddress, "addressZip", values, "detailsOfInjuryLocation.zip");

            SetStringValueDictionary(values, "locationOfDeath.name", record.DeathLocationName);
            SetDictionaryValueDictionary(record.DeathLocationAddress, "addressLine1", values, "locationOfDeath.street");
            SetDictionaryValueDictionary(record.DeathLocationAddress, "addressCity", values, "locationOfDeath.city");
            SetDictionaryValueDictionary(record.DeathLocationAddress, "addressState", values, "locationOfDeath.state");
            SetDictionaryValueDictionary(record.DeathLocationAddress, "addressZip", values, "locationOfDeath.zip");

            SetStringValueDictionary(values, "funeralFacility.name", record.FuneralHomeName);
            SetDictionaryValueDictionary(record.FuneralHomeAddress, "addressLine1", values, "funeralFacility.street");
            SetDictionaryValueDictionary(record.FuneralHomeAddress, "addressCity", values, "funeralFacility.city");
            SetDictionaryValueDictionary(record.FuneralHomeAddress, "addressState", values, "funeralFacility.state");
            SetDictionaryValueDictionary(record.FuneralHomeAddress, "addressZip", values, "funeralFacility.zip");

            SetDictionaryValueDictionary(record.CertifierAddress, "addressLine1", values, "personCompletingCauseOfDeathAddress.street");
            SetDictionaryValueDictionary(record.CertifierAddress, "addressCity", values, "personCompletingCauseOfDeathAddress.city");
            SetDictionaryValueDictionary(record.CertifierAddress, "addressState", values, "personCompletingCauseOfDeathAddress.state");
            SetDictionaryValueDictionary(record.CertifierAddress, "addressZip", values, "personCompletingCauseOfDeathAddress.zip");

            SetDictionaryValueDictionary(record.PlaceOfBirth, "addressCity", values, "placeOfBirth.city");
            SetDictionaryValueDictionary(record.PlaceOfBirth, "addressState", values, "placeOfBirth.state");
            SetDictionaryValueDictionary(record.PlaceOfBirth, "addressCountry", values, "placeOfBirth.country");

            SetDictionaryValueDictionary(record.DeathLocationAddress, "addressLine1", values, "locationOfDeath.street");
            SetDictionaryValueDictionary(record.DeathLocationAddress, "addressCity", values, "locationOfDeath.city");
            SetDictionaryValueDictionary(record.DeathLocationAddress, "addressState", values, "locationOfDeath.state");
            SetDictionaryValueDictionary(record.DeathLocationAddress, "addressZip", values, "locationOfDeath.zip");

            SetStringValueDictionary(values, "placeOfDisposition.name", record.DispositionLocationName);
            SetDictionaryValueDictionary(record.DispositionLocationAddress, "addressCity", values, "placeOfDisposition.city");
            SetDictionaryValueDictionary(record.DispositionLocationAddress, "addressState", values, "placeOfDisposition.state");
            SetDictionaryValueDictionary(record.DispositionLocationAddress, "addressCountry", values, "placeOfDisposition.country");

            if (record.GivenNames != null)
            {
                if (record.GivenNames.Count() > 0)
                {
                    values["decedentName.firstName"] = record.GivenNames[0];
                }
                if (record.GivenNames.Count() > 1)
                {
                    values["decedentName.middleName"] = record.GivenNames[1];
                }
            }

            SetStringValueDictionary(values, "decedentName.lastName", record.FamilyName);

            if (record.CertifierGivenNames != null)
            {
                if (record.CertifierGivenNames.Count() > 0)
                {
                    values["personCompletingCauseOfDeathName.firstName"] = record.CertifierGivenNames[0];
                }
                if (record.CertifierGivenNames.Count() > 1)
                {
                    values["personCompletingCauseOfDeathName.middleName"] = record.CertifierGivenNames[1];
                }
            }

            SetStringValueDictionary(values, "personCompletingCauseOfDeathName.lastName", record.CertifierFamilyName);

            SetStringValueDictionary(values, "detailsOfInjury.detailsOfInjury", record.InjuryLocationDescription);

            if (record.EducationLevel != null && record.EducationLevel.ContainsKey("code"))
            {
                switch (record.EducationLevel["code"])
                {
                    case "PHC1448":
                        values["education.education"] = "8th grade or less";
                        break;
                    case "PHC1449":
                        values["education.education"] = "9th through 12th grade; no diploma";
                        break;
                    case "PHC1450":
                        values["education.education"] = "High School Graduate or GED Completed";
                        break;
                    case "PHC1451":
                        values["education.education"] = "Some college credit, but no degree";
                        break;
                    case "PHC1452":
                        values["education.education"] = "Associate Degree";
                        break;
                    case "PHC1453":
                        values["education.education"] = "Bachelor's Degree";
                        break;
                    case "PHC1454":
                        values["education.education"] = "Master's Degree";
                        break;
                    case "PHC1455":
                        values["education.education"] = "Doctorate Degree or Professional Degree";
                        break;
                }
            }

            SetStringValueDictionary(values, "motherName.lastName", record.MotherMaidenName);

            if (record.MannerOfDeathType != null && record.MannerOfDeathType.ContainsKey("code"))
            {
                switch (record.MannerOfDeathType["code"])
                {
                    case "38605008":
                        values["mannerOfDeath.mannerOfDeath"] = "Natural";
                        break;
                    case "7878000":
                        values["mannerOfDeath.mannerOfDeath"] = "Accident";
                        break;
                    case "44301001":
                        values["mannerOfDeath.mannerOfDeath"] = "Suicide";
                        break;
                    case "27935005":
                        values["mannerOfDeath.mannerOfDeath"] = "Homicide";
                        break;
                    case "185973002":
                        values["mannerOfDeath.mannerOfDeath"] = "Pending Investigation";
                        break;
                    case "65037004":
                        values["mannerOfDeath.mannerOfDeath"] = "Could not be determined";
                        break;
                }
            }

            if (record.MaritalStatus != null && record.MaritalStatus.ContainsKey("code"))
            {
                switch (record.MaritalStatus["code"])
                {
                    case "M":
                        values["maritalStatus.maritalStatus"] = "Married";
                        break;
                    case "L":
                        values["maritalStatus.maritalStatus"] = "Legally Separated";
                        break;
                    case "W":
                        values["maritalStatus.maritalStatus"] = "Widowed";
                        break;
                    case "D":
                        values["maritalStatus.maritalStatus"] = "Divorced";
                        break;
                    case "S":
                        values["maritalStatus.maritalStatus"] = "Never Married";
                        break;
                    case "UNK":
                        values["maritalStatus.maritalStatus"] = "unknown";
                        break;
                }
            }

            if (record.BirthSex != null)
            {
                if (record.BirthSex == "M")
                {
                    values["sex.sex"] = "Male";
                }
                else if (record.BirthSex == "F")
                {
                    values["sex.sex"] = "Female";
                }
            }

            if (record.SSN != null && record.SSN.Length == 9)
            {
                values["ssn.ssn1"] = record.SSN.Substring(0, 3);
                values["ssn.ssn2"] = record.SSN.Substring(3, 2);
                values["ssn.ssn3"] = record.SSN.Substring(5, 4);
            }

            values["race.race.option"] = "Known";
            List<string> names = new List<string>();
            foreach (Tuple<string, string> race in record.Race)
            {
                names.Add(race.Item1);
            }
            values["race.race.specify"] = JsonConvert.SerializeObject(names.ToArray());

            return JsonConvert.SerializeObject(values, Formatting.Indented);
        }

        /// <summary>Convert Nightingale style JSON string to FHIR VRDR <c>DeathRecord</c>,</summary>
        public static DeathRecord FromNightingale(string json)
        {
            // Load in Nightingale string and serialize to Dictionary<string, string>
            Dictionary<string, string> values = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

            // Start building the record
            DeathRecord deathRecord = new DeathRecord();

            SetStringValueDeathRecordString(deathRecord, "Identifier", GetValue(values, "certificateNumber"));
            SetStringValueDeathRecordString(deathRecord, "DeathLocationJurisdiction", GetValue(values, "deathLocationJurisdiction"));

            SetYesNoValueDeathRecordBoolean(deathRecord, "MilitaryServiceBoolean", GetValue(values, "armedForcesService.armedForcesService"));
            SetYesNoValueDeathRecordBoolean(deathRecord, "AutopsyPerformedIndicatorBoolean", GetValue(values, "autopsyPerformed.autopsyPerformed"));
            SetYesNoValueDeathRecordBoolean(deathRecord, "AutopsyResultsAvailableBoolean", GetValue(values, "autopsyAvailableToCompleteCauseOfDeath.autopsyAvailableToCompleteCauseOfDeath"));
            SetYesNoValueDeathRecordBoolean(deathRecord, "ExaminerContactedBoolean", GetValue(values, "meOrCoronerContacted.meOrCoronerContacted"));

            SetYesNoValueDeathRecordCode(deathRecord, "TobaccoUse", GetValue(values, "didTobaccoUseContributeToDeath.didTobaccoUseContributeToDeath"));

            if (GetValue(values, "certifierType.certifierType") == "Physician (Certifier)")
            {
                SetStringValueDeathRecordDictionary(deathRecord, "CertificationRole", "code", "434641000124105");
                SetStringValueDeathRecordDictionary(deathRecord, "CertificationRole", "display", "Physician");
                SetStringValueDeathRecordDictionary(deathRecord, "CertificationRole", "system", VRDR.CodeSystems.SCT);
            }
            else if (GetValue(values, "certifierType.certifierType") == "Physician (Pronouncer and Certifier)")
            {
                SetStringValueDeathRecordDictionary(deathRecord, "CertificationRole", "code", "434651000124107");
                SetStringValueDeathRecordDictionary(deathRecord, "CertificationRole", "display", "Physician (Pronouncer and Certifier)");
                SetStringValueDeathRecordDictionary(deathRecord, "CertificationRole", "system", VRDR.CodeSystems.SCT);
            }
            else if (GetValue(values, "certifierType.certifierType") == "Medical Examiner")
            {
                SetStringValueDeathRecordDictionary(deathRecord, "CertificationRole", "code", "440051000124108");
                SetStringValueDeathRecordDictionary(deathRecord, "CertificationRole", "display", "Medical Examiner");
                SetStringValueDeathRecordDictionary(deathRecord, "CertificationRole", "system", VRDR.CodeSystems.SCT);
            }

            SetStringValueDeathRecordString(deathRecord, "COD1A", GetValue(values, "cod.immediate"));
            SetStringValueDeathRecordString(deathRecord, "INTERVAL1A", GetValue(values, "cod.immediateInt"));
            SetStringValueDeathRecordString(deathRecord, "COD1B", GetValue(values, "cod.under1"));
            SetStringValueDeathRecordString(deathRecord, "INTERVAL1B", GetValue(values, "cod.under1Int"));
            SetStringValueDeathRecordString(deathRecord, "COD1C", GetValue(values, "cod.under2"));
            SetStringValueDeathRecordString(deathRecord, "INTERVAL1C", GetValue(values, "cod.under2Int"));
            SetStringValueDeathRecordString(deathRecord, "COD1D", GetValue(values, "cod.under3"));
            SetStringValueDeathRecordString(deathRecord, "INTERVAL1D", GetValue(values, "cod.under3Int"));

            SetStringValueDeathRecordString(deathRecord, "ContributingConditions", GetValue(values, "contributingCauses.contributingCauses"));

            SetStringValueDeathRecordString(deathRecord, "DateOfBirth", GetValue(values, "dateOfBirth.dateOfBirth"));
            SetStringValueDeathRecordString(deathRecord, "DateOfDeath", GetValue(values, "dateOfDeath.dateOfDeath"));
            SetStringValueDeathRecordString(deathRecord, "DateOfDeathPronouncement", GetValue(values, "datePronouncedDead.datePronouncedDead"));

            SetStringValueDeathRecordDictionary(deathRecord, "Residence", "addressLine1", GetValue(values, "decedentAddress.street"));
            SetStringValueDeathRecordDictionary(deathRecord, "Residence", "addressCity", GetValue(values, "decedentAddress.city"));
            SetStringValueDeathRecordDictionary(deathRecord, "Residence", "addressState", GetValue(values, "decedentAddress.state"));
            SetStringValueDeathRecordDictionary(deathRecord, "Residence", "addressZip", GetValue(values, "decedentAddress.zip"));
            SetStringValueDeathRecordDictionary(deathRecord, "Residence", "addressCountry", "US");

            SetStringValueDeathRecordString(deathRecord, "InjuryLocationName", GetValue(values, "detailsOfInjuryLocation.name"));
            SetStringValueDeathRecordDictionary(deathRecord, "InjuryLocationAddress", "addressLine1", GetValue(values, "detailsOfInjuryLocation.street"));
            SetStringValueDeathRecordDictionary(deathRecord, "InjuryLocationAddress", "addressCity", GetValue(values, "detailsOfInjuryLocation.city"));
            SetStringValueDeathRecordDictionary(deathRecord, "InjuryLocationAddress", "addressState", GetValue(values, "detailsOfInjuryLocation.state"));
            SetStringValueDeathRecordDictionary(deathRecord, "InjuryLocationAddress", "addressZip", GetValue(values, "detailsOfInjuryLocation.zip"));
            SetStringValueDeathRecordDictionary(deathRecord, "InjuryLocationAddress", "addressCountry", "US");

            SetStringValueDeathRecordString(deathRecord, "DeathLocationName", GetValue(values, "locationOfDeath.name"));
            SetStringValueDeathRecordDictionary(deathRecord, "DeathLocationAddress", "addressLine1", GetValue(values, "locationOfDeath.street"));
            SetStringValueDeathRecordDictionary(deathRecord, "DeathLocationAddress", "addressCity", GetValue(values, "locationOfDeath.city"));
            SetStringValueDeathRecordDictionary(deathRecord, "DeathLocationAddress", "addressState", GetValue(values, "locationOfDeath.state"));
            SetStringValueDeathRecordDictionary(deathRecord, "DeathLocationAddress", "addressZip", GetValue(values, "locationOfDeath.zip"));
            SetStringValueDeathRecordDictionary(deathRecord, "DeathLocationAddress", "addressCountry", "US");

            SetStringValueDeathRecordString(deathRecord, "FuneralHomeName", GetValue(values, "funeralFacility.name"));
            SetStringValueDeathRecordDictionary(deathRecord, "FuneralHomeAddress", "addressLine1", GetValue(values, "funeralFacility.street"));
            SetStringValueDeathRecordDictionary(deathRecord, "FuneralHomeAddress", "addressCity", GetValue(values, "funeralFacility.city"));
            SetStringValueDeathRecordDictionary(deathRecord, "FuneralHomeAddress", "addressState", GetValue(values, "funeralFacility.state"));
            SetStringValueDeathRecordDictionary(deathRecord, "FuneralHomeAddress", "addressZip", GetValue(values, "funeralFacility.zip"));
            SetStringValueDeathRecordDictionary(deathRecord, "FuneralHomeAddress", "addressCountry", "US");

            SetStringValueDeathRecordDictionary(deathRecord, "CertifierAddress", "addressLine1", GetValue(values, "personCompletingCauseOfDeathAddress.street"));
            SetStringValueDeathRecordDictionary(deathRecord, "CertifierAddress", "addressCity", GetValue(values, "personCompletingCauseOfDeathAddress.city"));
            SetStringValueDeathRecordDictionary(deathRecord, "CertifierAddress", "addressState", GetValue(values, "personCompletingCauseOfDeathAddress.state"));
            SetStringValueDeathRecordDictionary(deathRecord, "CertifierAddress", "addressZip", GetValue(values, "personCompletingCauseOfDeathAddress.zip"));
            SetStringValueDeathRecordDictionary(deathRecord, "CertifierAddress", "addressCountry", "US");

            SetStringValueDeathRecordDictionary(deathRecord, "PlaceOfBirth", "addressCity", GetValue(values, "placeOfBirth.city"));
            SetStringValueDeathRecordDictionary(deathRecord, "PlaceOfBirth", "addressState", GetValue(values, "placeOfBirth.state"));
            SetStringValueDeathRecordDictionary(deathRecord, "PlaceOfBirth", "addressZip", GetValue(values, "placeOfBirth.zip"));
            SetStringValueDeathRecordDictionary(deathRecord, "PlaceOfBirth", "addressCountry", "US");

            SetStringValueDeathRecordString(deathRecord, "DispositionLocationName", GetValue(values, "placeOfDisposition.name"));
            SetStringValueDeathRecordDictionary(deathRecord, "DispositionLocationAddress", "addressCity", GetValue(values, "placeOfDisposition.city"));
            SetStringValueDeathRecordDictionary(deathRecord, "DispositionLocationAddress", "addressState", GetValue(values, "placeOfDisposition.state"));
            SetStringValueDeathRecordDictionary(deathRecord, "DispositionLocationAddress", "addressCountry", GetValue(values, "placeOfDisposition.country"));

            List<string> names = new List<string>();
            if (!String.IsNullOrWhiteSpace(GetValue(values, "decedentName.firstName")))
            {
                names.Add(GetValue(values, "decedentName.firstName"));
            }
            if (!String.IsNullOrWhiteSpace(GetValue(values, "decedentName.middleName")))
            {
                names.Add(GetValue(values, "decedentName.middleName"));
            }
            deathRecord.GivenNames = names.ToArray();
            SetStringValueDeathRecordString(deathRecord, "FamilyName", GetValue(values, "decedentName.lastName"));

            List<string> cnames = new List<string>();
            if (!String.IsNullOrWhiteSpace(GetValue(values, "personCompletingCauseOfDeathName.firstName")))
            {
                cnames.Add(GetValue(values, "personCompletingCauseOfDeathName.firstName"));
            }
            if (!String.IsNullOrWhiteSpace(GetValue(values, "personCompletingCauseOfDeathName.middleName")))
            {
                cnames.Add(GetValue(values, "personCompletingCauseOfDeathName.middleName"));
            }
            deathRecord.CertifierGivenNames = cnames.ToArray();

            SetStringValueDeathRecordString(deathRecord, "CertifierFamilyName", GetValue(values, "personCompletingCauseOfDeathName.lastName"));

            SetStringValueDeathRecordString(deathRecord, "InjuryLocationDescription", GetValue(values, "detailsOfInjury.detailsOfInjury"));

            switch (GetValue(values, "education.education"))
            {
                case "8th grade or less":
                    Dictionary<string, string> edu = new Dictionary<string, string>();
                    edu.Add("code", "PHC1448");
                    edu.Add("system", VRDR.CodeSystems.PH_PHINVS_CDC);
                    edu.Add("display", "8th grade or less");
                    deathRecord.EducationLevel = edu;
                    break;
                case "9th through 12th grade; no diploma":
                    edu = new Dictionary<string, string>();
                    edu.Add("code", "PHC1449");
                    edu.Add("system", VRDR.CodeSystems.PH_PHINVS_CDC);
                    edu.Add("display", "9th through 12th grade; no diploma");
                    deathRecord.EducationLevel = edu;
                    break;
                case "High School Graduate or GED Completed":
                    edu = new Dictionary<string, string>();
                    edu.Add("code", "PHC1450");
                    edu.Add("system", VRDR.CodeSystems.PH_PHINVS_CDC);
                    edu.Add("display", "High School Graduate or GED Completed");
                    deathRecord.EducationLevel = edu;
                    break;
                case "Some college credit, but no degree":
                    edu = new Dictionary<string, string>();
                    edu.Add("code", "PHC1451");
                    edu.Add("system", VRDR.CodeSystems.PH_PHINVS_CDC);
                    edu.Add("display", "Some college credit, but no degree");
                    deathRecord.EducationLevel = edu;
                    break;
                case "Associate Degree":
                    edu = new Dictionary<string, string>();
                    edu.Add("code", "PHC1452");
                    edu.Add("system", VRDR.CodeSystems.PH_PHINVS_CDC);
                    edu.Add("display", "Associate Degree");
                    deathRecord.EducationLevel = edu;
                    break;
                case "Bachelor's Degree":
                    edu = new Dictionary<string, string>();
                    edu.Add("code", "PHC1453");
                    edu.Add("system", VRDR.CodeSystems.PH_PHINVS_CDC);
                    edu.Add("display", "Bachelor's Degree");
                    deathRecord.EducationLevel = edu;
                    break;
                case "Master's Degree":
                    edu = new Dictionary<string, string>();
                    edu.Add("code", "PHC1454");
                    edu.Add("system", VRDR.CodeSystems.PH_PHINVS_CDC);
                    edu.Add("display", "Master's Degree");
                    deathRecord.EducationLevel = edu;
                    break;
                case "Doctorate Degree or Professional Degree":
                    edu = new Dictionary<string, string>();
                    edu.Add("code", "PHC1455");
                    edu.Add("system", VRDR.CodeSystems.PH_PHINVS_CDC);
                    edu.Add("display", "Doctorate Degree or Professional Degree");
                    deathRecord.EducationLevel = edu;
                    break;
                default:
                    edu = new Dictionary<string, string>();
                    edu.Add("code", "UNK");
                    edu.Add("system", VRDR.CodeSystems.PH_NullFlavor_HL7_V3);
                    edu.Add("display", "Unknown");
                    deathRecord.EducationLevel = edu;
                    break;
            }

            SetStringValueDeathRecordString(deathRecord, "MotherMaidenName", GetValue(values, "motherName.lastName"));

            switch (GetValue(values, "mannerOfDeath.mannerOfDeath"))
            {
                case "Natural":
                    Dictionary<string, string> mod = new Dictionary<string, string>();
                    mod.Add("code", "38605008");
                    mod.Add("system", VRDR.CodeSystems.SCT);
                    mod.Add("display", "Natural");
                    deathRecord.MannerOfDeathType = mod;
                    break;
                case "Accident":
                    mod = new Dictionary<string, string>();
                    mod.Add("code", "7878000");
                    mod.Add("system", VRDR.CodeSystems.SCT);
                    mod.Add("display", "Accident");
                    deathRecord.MannerOfDeathType = mod;
                    break;
                case "Suicide":
                    mod = new Dictionary<string, string>();
                    mod.Add("code", "44301001");
                    mod.Add("system", VRDR.CodeSystems.SCT);
                    mod.Add("display", "Suicide");
                    deathRecord.MannerOfDeathType = mod;
                    break;
                case "Homicide":
                    mod = new Dictionary<string, string>();
                    mod.Add("code", "27935005");
                    mod.Add("system", VRDR.CodeSystems.SCT);
                    mod.Add("display", "Homicide");
                    deathRecord.MannerOfDeathType = mod;
                    break;
                case "Pending Investigation":
                    mod = new Dictionary<string, string>();
                    mod.Add("code", "185973002");
                    mod.Add("system", VRDR.CodeSystems.SCT);
                    mod.Add("display", "Pending Investigation");
                    deathRecord.MannerOfDeathType = mod;
                    break;
                case "Could not be determined":
                    mod = new Dictionary<string, string>();
                    mod.Add("code", "65037004");
                    mod.Add("system", VRDR.CodeSystems.SCT);
                    mod.Add("display", "Could not be determined");
                    deathRecord.MannerOfDeathType = mod;
                    break;
            }

            switch (GetValue(values, "maritalStatus.maritalStatus"))
            {
                case "Married":
                    Dictionary<string, string> mar = new Dictionary<string, string>();
                    mar.Add("code", "M");
                    mar.Add("system", VRDR.CodeSystems.PH_MaritalStatus_HL7_2x);
                    mar.Add("display", "Married");
                    deathRecord.MaritalStatus = mar;
                    break;
                case "Legally Separated":
                    mar = new Dictionary<string, string>();
                    mar.Add("code", "L");
                    mar.Add("system", VRDR.CodeSystems.PH_MaritalStatus_HL7_2x);
                    mar.Add("display", "Legally Separated");
                    deathRecord.MaritalStatus = mar;
                    break;
                case "Widowed":
                    mar = new Dictionary<string, string>();
                    mar.Add("code", "W");
                    mar.Add("system", VRDR.CodeSystems.PH_MaritalStatus_HL7_2x);
                    mar.Add("display", "Widowed");
                    deathRecord.MaritalStatus = mar;
                    break;
                case "Divorced":
                    mar = new Dictionary<string, string>();
                    mar.Add("code", "D");
                    mar.Add("system", VRDR.CodeSystems.PH_MaritalStatus_HL7_2x);
                    mar.Add("display", "Divorced");
                    deathRecord.MaritalStatus = mar;
                    break;
                case "Never Married":
                    mar = new Dictionary<string, string>();
                    mar.Add("code", "S");
                    mar.Add("system", VRDR.CodeSystems.PH_MaritalStatus_HL7_2x);
                    mar.Add("display", "Never Married");
                    deathRecord.MaritalStatus = mar;
                    break;
                case "unknown":
                    mar = new Dictionary<string, string>();
                    mar.Add("code", "UNK");
                    mar.Add("system", VRDR.CodeSystems.PH_NullFlavor_HL7_V3);
                    mar.Add("display", "unknown");
                    deathRecord.MaritalStatus = mar;
                    break;
            }

            if (GetValue(values, "sex.sex") == "Male")
            {
                deathRecord.BirthSex = "M";
            }
            else if (GetValue(values, "sex.sex") == "Female")
            {
                deathRecord.BirthSex = "F";
            }

            if (GetValue(values, "ssn.ssn1") != null && GetValue(values, "ssn.ssn2") != null && GetValue(values, "ssn.ssn3") != null)
            {
                SetStringValueDeathRecordString(deathRecord, "SSN", GetValue(values, "ssn.ssn1") + GetValue(values, "ssn.ssn2") + GetValue(values, "ssn.ssn3"));
            }

            if (!String.IsNullOrWhiteSpace(GetValue(values, "race.race.specify")))
            {
                MortalityData mdata = MortalityData.Instance;
                List<Tuple<string, string>> rtuples = new List<Tuple<string, string>>();
                string[] races = JsonConvert.DeserializeObject<string[]>(GetValue(values, "race.race.specify"));
                foreach (string r in races)
                {
                    string c = mdata.RaceNameToRaceCode(r);
                    if (!String.IsNullOrWhiteSpace(c))
                    {
                        rtuples.Add(Tuple.Create(r, c));
                    }
                }
                deathRecord.Race = rtuples.ToArray();
            }

            return deathRecord;
        }

        /// <summary>Set a Yes/No (coded) value on a Dictionary.</summary>
        private static void SetYesNoValueDictionary(Dictionary<string, string> dict, string key, DeathRecord deathRecord, string property)
        {
            Type type = deathRecord.GetType();
            PropertyInfo prop = type.GetProperty(property);
            Dictionary<string, string> value = (Dictionary<string, string>)prop.GetValue(deathRecord);
            if (value.ContainsKey("display"))
            {
                dict[key] = value["display"];
            }
        }

        /// <summary>Set a string value on a Dictionary.</summary>
        private static void SetStringValueDictionary(Dictionary<string, string> dict, string key, string value)
        {
            if (!String.IsNullOrWhiteSpace(value))
            {
                dict[key] = value;
            }
        }

        /// <summary>Set a date value on a Dictionary.</summary>
        private static void SetDateValueDictionary(Dictionary<string, string> dict, string key, string value)
        {
            if (!String.IsNullOrWhiteSpace(value))
            {
                dict[key] = value.Substring(0, 10);
            }
        }

        /// <summary>Set a Dictionary value on a Dictionary.</summary>
        private static void SetDictionaryValueDictionary(Dictionary<string, string> dict1, string key1, Dictionary<string, string> dict2, string key2)
        {
            if (dict1 != null && dict1.ContainsKey(key1) && dict2 != null && key2 != null)
            {
                if (!String.IsNullOrWhiteSpace(dict1[key1]))
                {
                    dict2[key2] = dict1[key1];
                }
            }
        }

        /// <summary>Set a String value on a DeathRecord.</summary>
        private static void SetStringValueDeathRecordString(DeathRecord deathRecord, string property, string value)
        {
            if (String.IsNullOrWhiteSpace(value))
            {
                return;
            }
            Type type = deathRecord.GetType();
            PropertyInfo prop = type.GetProperty(property);
            prop.SetValue(deathRecord, value, null);
        }

        /// <summary>Set a String value on a DeathRecord.</summary>
        private static void SetStringValueDeathRecordDictionary(DeathRecord deathRecord, string property, string key, string value)
        {
            if (String.IsNullOrWhiteSpace(value))
            {
                return;
            }
            Type type = deathRecord.GetType();
            PropertyInfo prop = type.GetProperty(property);
            Dictionary<string, string> dict = (Dictionary<string, string>)prop.GetValue(deathRecord);
            if (dict == null)
            {
                dict = new Dictionary<string, string>();
            }
            dict[key] = value;
            prop.SetValue(deathRecord, dict, null);
        }

        /// <summary>Set a Yes/No (coded) value on a DeathRecord using Boolean helpers.</summary>
        private static void SetYesNoValueDeathRecordBoolean(DeathRecord deathRecord, string property, string value)
        {
            Type type = deathRecord.GetType();
            PropertyInfo prop = type.GetProperty(property);
            if (String.IsNullOrWhiteSpace(value))
            {
                prop.SetValue(deathRecord, null);
            }
            else if (value.Trim().ToLower() == "yes")
            {
                prop.SetValue(deathRecord, true);
            }
            else if (value.Trim().ToLower() == "no")
            {
                prop.SetValue(deathRecord, false);
            }
            else
            {
                prop.SetValue(deathRecord, null);
            }
        }

        /// <summary>Set a Yes/No (coded) value on a DeathRecord.</summary>
        private static void SetYesNoValueDeathRecordCode(DeathRecord deathRecord, string property, string value)
        {
            Dictionary<string, string> coding = new Dictionary<string, string>();
            if (String.IsNullOrWhiteSpace(value))
            {
                coding.Add("code", "UNK");
                coding.Add("system", VRDR.CodeSystems.PH_NullFlavor_HL7_V3);
                coding.Add("display", "Unknown");
            }
            else if (value.Trim().ToLower() == "yes")
            {
                coding.Add("code", "373066001");
                coding.Add("system", CodeSystems.SCT);
                coding.Add("display", "Yes");
            }
            else if (value.Trim().ToLower() == "no")
            {
                coding.Add("code", "373067005");
                coding.Add("system", CodeSystems.SCT);
                coding.Add("display", "No");
            }
            else
            {
                coding.Add("code", "UNK");
                coding.Add("system", VRDR.CodeSystems.PH_NullFlavor_HL7_V3);
                coding.Add("display", "Unknown");
            }
            Type type = deathRecord.GetType();
            PropertyInfo prop = type.GetProperty(property);
            prop.SetValue(deathRecord, coding);
        }

        /// <summary>Set a value in a Dictionary, but only if that value is not null or whitespace.</summary>
        private static void SetValueDict(Dictionary<string, string> dict, string key, string value)
        {
            if (dict != null && !String.IsNullOrWhiteSpace(value))
            {
                dict[key] = value.Trim();
            }
        }

        /// <summary>Get a value from a Dictionary, but return null if the key doesn't exist.</summary>
        private static string GetValueDict(Dictionary<string, string> dict, string key)
        {
            if (dict == null || key == null)
            {
                return null;
            }
            string value;
            dict.TryGetValue(key, out value);
            return value;
        }

        /// <summary>Get a value from a Dictionary, but return null if the key doesn't exist.</summary>
        private static string GetValue(Dictionary<string, string> dict, string key)
        {
            string value;
            dict.TryGetValue(key, out value);
            return value;
        }
    }
}
