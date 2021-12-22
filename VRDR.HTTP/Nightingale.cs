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

            if (record.CertificationRoleHelper == "434651000124107")
            {
                values["certifierType.certifierType"] = "Physician (Certifier)";
            }
            else if (record.CertificationRoleHelper == "434641000124105")
            {
                values["certifierType.certifierType"] = "Physician (Pronouncer and Certifier)";
            }
            else if (record.CertificationRoleHelper == "455381000124109")
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

            if (record.SpouseGivenNames != null)
            {
                if (record.SpouseGivenNames.Count() > 0)
                {
                    values["spouseName.firstName"] = record.SpouseGivenNames[0];
                }
                if (record.SpouseGivenNames.Count() > 1)
                {
                    values["spouseName.middleName"] = record.SpouseGivenNames[1];
                }
            }
            SetStringValueDictionary(values, "spouseName.lastName", record.SpouseFamilyName);

            if (record.FatherGivenNames != null)
            {
                if (record.FatherGivenNames.Count() > 0)
                {
                    values["fatherName.firstName"] = record.FatherGivenNames[0];
                }
                if (record.FatherGivenNames.Count() > 1)
                {
                    values["fatherName.middleName"] = record.FatherGivenNames[1];
                }
            }
            SetStringValueDictionary(values, "fatherName.lastName", record.FatherFamilyName);

            if (record.MotherGivenNames != null)
            {
                if (record.MotherGivenNames.Count() > 0)
                {
                    values["motherName.firstName"] = record.MotherGivenNames[0];
                }
                if (record.MotherGivenNames.Count() > 1)
                {
                    values["motherName.middleName"] = record.MotherGivenNames[1];
                }
            }
            SetStringValueDictionary(values, "motherName.lastName", record.MotherMaidenName);

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

            if (record.EducationLevelHelper != null)
            {
                switch (record.EducationLevelHelper)
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

            if (record.MannerOfDeathTypeHelper != null)
            {
                switch (record.MannerOfDeathTypeHelper)
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

            if (record.MaritalStatusHelper != null)
            {
                switch (record.MaritalStatusHelper)
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

            SetStringValueDictionary(values, "usualOccupation.usualOccupation", record.UsualOccupation);

            if (record.DeathLocationTypeHelper != null)
            {
                if (record.DeathLocationTypeHelper == VRDR.ValueSets.PlaceOfDeath.Hospital_Dead_On_Arrival)
                {
                    values["placeOfDeath.placeOfDeath.option"] = "Dead on arrival at hospital";
                }
                else if (record.DeathLocationTypeHelper == VRDR.ValueSets.PlaceOfDeath.Decedents_Home)
                {
                    values["placeOfDeath.placeOfDeath.option"] = "Death in home";
                }
                else if (record.DeathLocationTypeHelper == VRDR.ValueSets.PlaceOfDeath.Hospice)
                {
                    values["placeOfDeath.placeOfDeath.option"] = "Death in hospice";
                }
                else if (record.DeathLocationTypeHelper == VRDR.ValueSets.PlaceOfDeath.Hospital_Inpatient)
                {
                    values["placeOfDeath.placeOfDeath.option"] = "Death in hospital";
                }
                else if (record.DeathLocationTypeHelper == VRDR.ValueSets.PlaceOfDeath.Death_In_Emergency_Room_Outpatient)
                {
                    values["placeOfDeath.placeOfDeath.option"] = "Death in hospital-based emergency department or outpatient department";
                }
                else if (record.DeathLocationTypeHelper == VRDR.ValueSets.PlaceOfDeath.Death_In_Nursing_Home_Long_Term_Care_Facility)
                {
                    values["placeOfDeath.placeOfDeath.option"] = "Death in nursing home or long term care facility";
                }
                else if (record.DeathLocationTypeHelper == VRDR.ValueSets.PlaceOfDeath.Unknown)
                {
                    values["placeOfDeath.placeOfDeath.option"] = "Unknown";
                }
                else
                {
                    values["placeOfDeath.placeOfDeath.option"] = "Other";
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
                deathRecord.CertificationRoleHelper = "434651000124107";
            }
            else if (GetValue(values, "certifierType.certifierType") == "Physician (Pronouncer and Certifier)")
            {
                deathRecord.CertificationRoleHelper = "434641000124105";
            }
            else if (GetValue(values, "certifierType.certifierType") == "Medical Examiner")
            {
                deathRecord.CertificationRoleHelper = "455381000124109";
            }
            else
            {
                deathRecord.CertificationRoleHelper = "OTH";
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

            SetNameValuesDeathRecordName(deathRecord, "GivenNames", "FamilyName",
                                         GetValue(values, "decedentName.firstName"),
                                         GetValue(values, "decedentName.middleName"),
                                         GetValue(values, "decedentName.lastName"));

            SetNameValuesDeathRecordName(deathRecord, "SpouseGivenNames", "SpouseFamilyName",
                                         GetValue(values, "spouseName.firstName"),
                                         GetValue(values, "spouseName.middleName"),
                                         GetValue(values, "spouseName.lastName"));

            SetNameValuesDeathRecordName(deathRecord, "FatherGivenNames", "FatherFamilyName",
                                         GetValue(values, "fatherName.firstName"),
                                         GetValue(values, "fatherName.middleName"),
                                         GetValue(values, "fatherName.lastName"));

            SetNameValuesDeathRecordName(deathRecord, "MotherGivenNames", "MotherMaidenName",
                                         GetValue(values, "motherName.firstName"),
                                         GetValue(values, "motherName.middleName"),
                                         GetValue(values, "motherName.lastName"));

            SetNameValuesDeathRecordName(deathRecord, "CertifierGivenNames", "CertifierFamilyName",
                                         GetValue(values, "personCompletingCauseOfDeathName.firstName"),
                                         GetValue(values, "personCompletingCauseOfDeathName.middleName"),
                                         GetValue(values, "personCompletingCauseOfDeathName.lastName"));

            SetStringValueDeathRecordString(deathRecord, "InjuryLocationDescription", GetValue(values, "detailsOfInjury.detailsOfInjury"));

            switch (GetValue(values, "education.education"))
            {
                case "8th grade or less":
                    deathRecord.EducationLevelHelper = "PHC1448";
                    break;
                case "9th through 12th grade; no diploma":
                    deathRecord.EducationLevelHelper = "PHC1449";
                    break;
                case "High School Graduate or GED Completed":
                    deathRecord.EducationLevelHelper = "PHC1450";
                    break;
                case "Some college credit, but no degree":
                    deathRecord.EducationLevelHelper = "PHC1451";
                    break;
                case "Associate Degree":
                    deathRecord.EducationLevelHelper = "PHC1452";
                    break;
                case "Bachelor's Degree":
                    deathRecord.EducationLevelHelper = "PHC1453";
                    break;
                case "Master's Degree":
                    deathRecord.EducationLevelHelper = "PHC1454";
                    break;
                case "Doctorate Degree or Professional Degree":
                    deathRecord.EducationLevelHelper = "PHC1455";
                    break;
                default:
                    deathRecord.EducationLevelHelper = "UNK";
                    break;
            }

            switch (GetValue(values, "mannerOfDeath.mannerOfDeath"))
            {
                case "Natural":
                    deathRecord.MannerOfDeathTypeHelper = "38605008";
                    break;
                case "Accident":
                    deathRecord.MannerOfDeathTypeHelper = "7878000";
                    break;
                case "Suicide":
                    deathRecord.MannerOfDeathTypeHelper = "44301001";
                    break;
                case "Homicide":
                    deathRecord.MannerOfDeathTypeHelper = "27935005";
                    break;
                case "Pending Investigation":
                    deathRecord.MannerOfDeathTypeHelper = "185973002";
                    break;
                case "Could not be determined":
                    deathRecord.MannerOfDeathTypeHelper = "65037004";
                    break;
            }

            switch (GetValue(values, "maritalStatus.maritalStatus"))
            {
                case "Married":
                    deathRecord.MaritalStatusHelper = "M";
                    break;
                case "Legally Separated":
                    deathRecord.MaritalStatusHelper = "L";
                    break;
                case "Widowed":
                    deathRecord.MaritalStatusHelper = "W";
                    break;
                case "Divorced":
                    deathRecord.MaritalStatusHelper = "D";
                    break;
                case "Never Married":
                    deathRecord.MaritalStatusHelper = "S";
                    break;
                case "unknown":
                    deathRecord.MaritalStatusHelper = "UNK";
                    break;
            }

            SetStringValueDeathRecordString(deathRecord, "UsualOccupation", GetValue(values, "usualOccupation.usualOccupation"));

            switch (GetValue(values, "placeOfDeath.placeOfDeath.option"))
            {
                case "Dead on arrival at hospital":
                    deathRecord.DeathLocationTypeHelper = VRDR.ValueSets.PlaceOfDeath.Hospital_Dead_On_Arrival;
                    break;
                case "Death in home":
                    deathRecord.DeathLocationTypeHelper = VRDR.ValueSets.PlaceOfDeath.Decedents_Home;
                    break;
                case "Death in hospice":
                    deathRecord.DeathLocationTypeHelper = VRDR.ValueSets.PlaceOfDeath.Hospice;
                    break;
                case "Death in hospital":
                    deathRecord.DeathLocationTypeHelper = VRDR.ValueSets.PlaceOfDeath.Hospital_Inpatient;
                    break;
                case "Death in hospital-based emergency department or outpatient department":
                    deathRecord.DeathLocationTypeHelper = VRDR.ValueSets.PlaceOfDeath.Death_In_Emergency_Room_Outpatient;
                    break;
                case "Death in nursing home or long term care facility":
                    deathRecord.DeathLocationTypeHelper = VRDR.ValueSets.PlaceOfDeath.Death_In_Nursing_Home_Long_Term_Care_Facility;
                    break;
                case "Unknown":
                    deathRecord.DeathLocationTypeHelper = VRDR.ValueSets.PlaceOfDeath.Unknown;
                    break;
                default:
                    deathRecord.DeathLocationTypeHelper = VRDR.ValueSets.PlaceOfDeath.Other;
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

        /// <summary>Set a String value on a DeathRecord within a dictionary.</summary>
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

        /// <summary>Set a name value on a DeathRecord.</summary>
        private static void SetNameValuesDeathRecordName(DeathRecord deathRecord, string givenNamesField, string familyNameField, string firstName, string middleName, string lastName)
        {
            List<string> names = new List<string>();
            if (!String.IsNullOrWhiteSpace(firstName))
            {
                names.Add(firstName);
            }
            if (!String.IsNullOrWhiteSpace(middleName))
            {
                names.Add(middleName);
            }
            Type type = deathRecord.GetType();
            PropertyInfo givenNames = type.GetProperty(givenNamesField);
            givenNames.SetValue(deathRecord, names.ToArray());
            SetStringValueDeathRecordString(deathRecord, familyNameField, lastName);
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
