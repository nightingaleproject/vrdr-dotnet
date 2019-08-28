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
using FhirDeathRecord;

namespace FhirDeathRecord.HTTP
{
    /// <summary>Utility for translating between the Nightingale format and the FHIR VRDR format.</summary>
    public class Nightingale
    {
        /// <summary>Convert FHIR VRDR <c>DeathRecord</c> to Nightingale style JSON string.</summary>
        public static string ToNightingale(DeathRecord record)
        {
            Dictionary<string, string> values = new Dictionary<string, string>();

            SetYesNoValueDictionary(values, "armedForcesService.armedForcesService", record, "MilitaryService");
            SetYesNoValueDictionary(values, "autopsyPerformed.autopsyPerformed", record, "AutopsyPerformedIndicator");
            SetYesNoValueDictionary(values, "autopsyAvailableToCompleteCauseOfDeath.autopsyAvailableToCompleteCauseOfDeath", record, "AutopsyResultsAvailable");
            SetYesNoValueDictionary(values, "didTobaccoUseContributeToDeath.didTobaccoUseContributeToDeath", record, "TobaccoUse");
            SetYesNoValueDictionary(values, "meOrCoronerContacted.meOrCoronerContacted", record, "ExaminerContacted");

            return JsonConvert.SerializeObject(values, Formatting.Indented);
        }

        /// <summary>Convert Nightingale style JSON string to FHIR VRDR <c>DeathRecord</c>,</summary>
        public static DeathRecord FromNightingale(string json)
        {
            // Load in Nightingale string and serialize to Dictionary<string, string>
            Dictionary<string, string> values = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

            // Start building the record
            DeathRecord deathRecord = new DeathRecord();

            SetYesNoValueDeathRecordCode(deathRecord, "MilitaryService", GetValue(values, "armedForcesService.armedForcesService"));
            SetYesNoValueDeathRecordCode(deathRecord, "AutopsyPerformedIndicator", GetValue(values, "autopsyPerformed.autopsyPerformed"));
            SetYesNoValueDeathRecordCode(deathRecord, "AutopsyResultsAvailable", GetValue(values, "autopsyAvailableToCompleteCauseOfDeath.autopsyAvailableToCompleteCauseOfDeath"));
            SetYesNoValueDeathRecordCode(deathRecord, "TobaccoUse", GetValue(values, "didTobaccoUseContributeToDeath.didTobaccoUseContributeToDeath"));
            SetYesNoValueDeathRecordBool(deathRecord, "ExaminerContacted", GetValue(values, "meOrCoronerContacted.meOrCoronerContacted"));

            if (GetValue(values, "certifierType.certifierType") == "Physician (Certifier)")
            {
                SetStringValueDeathRecordDictionary(deathRecord, "CertifierRole", "code", "434641000124105");
                SetStringValueDeathRecordDictionary(deathRecord, "CertifierRole", "display", "Physician (Certifier)");
                SetStringValueDeathRecordDictionary(deathRecord, "CertifierRole", "system", "http://snomed.info/sct");
            }
            else if (GetValue(values, "certifierType.certifierType") == "Physician (Pronouncer and Certifier)")
            {
                SetStringValueDeathRecordDictionary(deathRecord, "CertifierRole", "code", "434651000124107");
                SetStringValueDeathRecordDictionary(deathRecord, "CertifierRole", "display", "Physician (Pronouncer and Certifier)");
                SetStringValueDeathRecordDictionary(deathRecord, "CertifierRole", "system", "http://snomed.info/sct");
            }
            else if (GetValue(values, "certifierType.certifierType") == "Medical Examiner")
            {
                SetStringValueDeathRecordDictionary(deathRecord, "CertifierRole", "code", "440051000124108");
                SetStringValueDeathRecordDictionary(deathRecord, "CertifierRole", "display", "Medical Examiner");
                SetStringValueDeathRecordDictionary(deathRecord, "CertifierRole", "system", "http://snomed.info/sct");
            }

            SetStringValueDeathRecordString(deathRecord, "COD1A", GetValue(values, "cod.immediate"));
            SetStringValueDeathRecordString(deathRecord, "INTERVAL1A", GetValue(values, "cod.immediateInt"));
            SetStringValueDeathRecordString(deathRecord, "COD1B", GetValue(values, "cod.under1"));
            SetStringValueDeathRecordString(deathRecord, "INTERVAL1B", GetValue(values, "cod.under1Int"));
            SetStringValueDeathRecordString(deathRecord, "COD1C", GetValue(values, "cod.under2"));
            SetStringValueDeathRecordString(deathRecord, "INTERVAL1C", GetValue(values, "cod.under2Int"));
            SetStringValueDeathRecordString(deathRecord, "COD1D", GetValue(values, "cod.under3"));
            SetStringValueDeathRecordString(deathRecord, "INTERVAL1D", GetValue(values, "cod.under3Int"));

            SetStringValueDeathRecordString(deathRecord, "DateOfBirth", GetValue(values, "dateOfBirth.dateOfBirth"));
            SetStringValueDeathRecordString(deathRecord, "DateOfDeath", GetValue(values, "dateOfDeath.dateOfDeath"));
            SetStringValueDeathRecordString(deathRecord, "DateOfDeathPronouncement", GetValue(values, "datePronouncedDead.datePronouncedDead"));

            SetStringValueDeathRecordDictionary(deathRecord, "Residence", "addressLine1", GetValue(values, "decedentAddress.street"));
            SetStringValueDeathRecordDictionary(deathRecord, "Residence", "addressCity", GetValue(values, "decedentAddress.city"));
            SetStringValueDeathRecordDictionary(deathRecord, "Residence", "addressState", GetValue(values, "decedentAddress.state"));
            SetStringValueDeathRecordDictionary(deathRecord, "Residence", "addressZip", GetValue(values, "decedentAddress.zip"));

            SetStringValueDeathRecordString(deathRecord, "InjuryLocationName", GetValue(values, "detailsOfInjuryLocation.name"));
            SetStringValueDeathRecordDictionary(deathRecord, "InjuryLocationAddress", "addressLine1", GetValue(values, "detailsOfInjuryLocation.street"));
            SetStringValueDeathRecordDictionary(deathRecord, "InjuryLocationAddress", "addressCity", GetValue(values, "detailsOfInjuryLocation.city"));
            SetStringValueDeathRecordDictionary(deathRecord, "InjuryLocationAddress", "addressState", GetValue(values, "detailsOfInjuryLocation.state"));
            SetStringValueDeathRecordDictionary(deathRecord, "InjuryLocationAddress", "addressZip", GetValue(values, "detailsOfInjuryLocation.zip"));

            SetStringValueDeathRecordString(deathRecord, "DeathLocationName", GetValue(values, "locationOfDeath.name"));
            SetStringValueDeathRecordDictionary(deathRecord, "DeathLocationAddress", "addressLine1", GetValue(values, "locationOfDeath.street"));
            SetStringValueDeathRecordDictionary(deathRecord, "DeathLocationAddress", "addressCity", GetValue(values, "locationOfDeath.city"));
            SetStringValueDeathRecordDictionary(deathRecord, "DeathLocationAddress", "addressState", GetValue(values, "locationOfDeath.state"));
            SetStringValueDeathRecordDictionary(deathRecord, "DeathLocationAddress", "addressZip", GetValue(values, "locationOfDeath.zip"));

            SetStringValueDeathRecordString(deathRecord, "FuneralHomeName", GetValue(values, "funeralFacility.name"));
            SetStringValueDeathRecordDictionary(deathRecord, "FuneralHomeAddress", "addressLine1", GetValue(values, "funeralFacility.street"));
            SetStringValueDeathRecordDictionary(deathRecord, "FuneralHomeAddress", "addressCity", GetValue(values, "funeralFacility.city"));
            SetStringValueDeathRecordDictionary(deathRecord, "FuneralHomeAddress", "addressState", GetValue(values, "funeralFacility.state"));
            SetStringValueDeathRecordDictionary(deathRecord, "FuneralHomeAddress", "addressZip", GetValue(values, "funeralFacility.zip"));

            SetStringValueDeathRecordDictionary(deathRecord, "CertifierAddress", "addressLine1", GetValue(values, "personCompletingCauseOfDeathAddress.street"));
            SetStringValueDeathRecordDictionary(deathRecord, "CertifierAddress", "addressCity", GetValue(values, "personCompletingCauseOfDeathAddress.city"));
            SetStringValueDeathRecordDictionary(deathRecord, "CertifierAddress", "addressState", GetValue(values, "personCompletingCauseOfDeathAddress.state"));
            SetStringValueDeathRecordDictionary(deathRecord, "CertifierAddress", "addressZip", GetValue(values, "personCompletingCauseOfDeathAddress.zip"));

            SetStringValueDeathRecordDictionary(deathRecord, "PlaceOfBirth", "addressCity", GetValue(values, "placeOfBirth.city"));
            SetStringValueDeathRecordDictionary(deathRecord, "PlaceOfBirth", "addressState", GetValue(values, "placeOfBirth.state"));
            SetStringValueDeathRecordDictionary(deathRecord, "PlaceOfBirth", "addressZip", GetValue(values, "placeOfBirth.country"));

            SetStringValueDeathRecordString(deathRecord, "DispositionLocationName", GetValue(values, "placeOfDisposition.name"));
            SetStringValueDeathRecordDictionary(deathRecord, "DispositionLocationAddress", "addressLine1", GetValue(values, "placeOfDisposition.city"));
            SetStringValueDeathRecordDictionary(deathRecord, "DispositionLocationAddress", "addressCity", GetValue(values, "placeOfDisposition.state"));
            SetStringValueDeathRecordDictionary(deathRecord, "DispositionLocationAddress", "addressState", GetValue(values, "placeOfDisposition.country"));

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

            SetStringValueDeathRecordString(deathRecord, "InjuryLocationDescription", GetValue(values, "detailsOfInjury.detailsOfInjury"));

            switch (GetValue(values, "education.education"))
            {
                case "8th grade or less":
                case "9th through 12th grade; no diploma":
                    Dictionary<string, string> edu = new Dictionary<string, string>();
                    edu.Add("code", "SEC");
                    edu.Add("system", "http://hl7.org/fhir/v3/EducationLevel");
                    edu.Add("display", "Some secondary or high school education");
                    deathRecord.EducationLevel = edu;
                    break;
                case "High School Graduate or GED Completed":
                    edu = new Dictionary<string, string>();
                    edu.Add("code", "HS");
                    edu.Add("system", "http://hl7.org/fhir/v3/EducationLevel");
                    edu.Add("display", "High School or secondary school degree complete");
                    deathRecord.EducationLevel = edu;
                    break;
                case "Some college credit, but no degree":
                    edu = new Dictionary<string, string>();
                    edu.Add("code", "PB");
                    edu.Add("system", "http://hl7.org/fhir/v3/EducationLevel");
                    edu.Add("display", "Some post-baccalaureate education");
                    deathRecord.EducationLevel = edu;
                    break;
                case "Associate Degree":
                    edu = new Dictionary<string, string>();
                    edu.Add("code", "ASSOC");
                    edu.Add("system", "http://hl7.org/fhir/v3/EducationLevel");
                    edu.Add("display", "Associate's or technical degree complete");
                    deathRecord.EducationLevel = edu;
                    break;
                case "Bachelor's Degree":
                    edu = new Dictionary<string, string>();
                    edu.Add("code", "BD");
                    edu.Add("system", "http://hl7.org/fhir/v3/EducationLevel");
                    edu.Add("display", "College or baccalaureate degree complete");
                    deathRecord.EducationLevel = edu;
                    break;
                case "Master's Degree":
                    edu = new Dictionary<string, string>();
                    edu.Add("code", "GD");
                    edu.Add("system", "http://hl7.org/fhir/v3/EducationLevel");
                    edu.Add("display", "Graduate or professional Degree complete");
                    deathRecord.EducationLevel = edu;
                    break;
                case "Doctorate Degree or Professional Degree":
                    edu = new Dictionary<string, string>();
                    edu.Add("code", "POSTG");
                    edu.Add("system", "http://hl7.org/fhir/v3/EducationLevel");
                    edu.Add("display", "Doctoral or post graduate education");
                    deathRecord.EducationLevel = edu;
                    break;
            }

            SetStringValueDeathRecordString(deathRecord, "MotherMaidenName", GetValue(values, "motherName.lastName"));

            switch (GetValue(values, "mannerOfDeath.mannerOfDeath"))
            {
                case "Natural":
                    Dictionary<string, string> mod = new Dictionary<string, string>();
                    mod.Add("code", "38605008");
                    mod.Add("display", "Doctoral or post graduate education");
                    deathRecord.MannerOfDeathType = mod;
                    break;
                case "Accident":
                    mod = new Dictionary<string, string>();
                    mod.Add("code", "7878000");
                    mod.Add("display", "Accident");
                    deathRecord.MannerOfDeathType = mod;
                    break;
                case "Suicide":
                    mod = new Dictionary<string, string>();
                    mod.Add("code", "44301001");
                    mod.Add("display", "Suicide");
                    deathRecord.MannerOfDeathType = mod;
                    break;
                case "Homicide":
                    mod = new Dictionary<string, string>();
                    mod.Add("code", "27935005");
                    mod.Add("display", "Homicide");
                    deathRecord.MannerOfDeathType = mod;
                    break;
                case "Pending Investigation":
                    mod = new Dictionary<string, string>();
                    mod.Add("code", "185973002");
                    mod.Add("display", "Pending Investigation");
                    deathRecord.MannerOfDeathType = mod;
                    break;
                case "Could not be determined":
                    mod = new Dictionary<string, string>();
                    mod.Add("code", "65037004");
                    mod.Add("display", "Could not be determined");
                    deathRecord.MannerOfDeathType = mod;
                    break;
            }

            switch (GetValue(values, "maritalStatus.maritalStatus"))
            {
                case "Married":
                    Dictionary<string, string> mar = new Dictionary<string, string>();
                    mar.Add("code", "M");
                    mar.Add("system", "http://hl7.org/fhir/v3/MaritalStatus");
                    mar.Add("display", "Married");
                    deathRecord.MaritalStatus = mar;
                    break;
                case "Married but seperated":
                    mar = new Dictionary<string, string>();
                    mar.Add("code", "L");
                    mar.Add("system", "http://hl7.org/fhir/v3/MaritalStatus");
                    mar.Add("display", "Legally Separated");
                    deathRecord.MaritalStatus = mar;
                    break;
                case "Widowed":
                case "Widowed (but not remarried)":
                    mar = new Dictionary<string, string>();
                    mar.Add("code", "W");
                    mar.Add("system", "http://hl7.org/fhir/v3/MaritalStatus");
                    mar.Add("display", "Widowed");
                    deathRecord.MaritalStatus = mar;
                    break;
                case "Divorced (but not remarried)":
                    mar = new Dictionary<string, string>();
                    mar.Add("code", "D");
                    mar.Add("system", "http://hl7.org/fhir/v3/MaritalStatus");
                    mar.Add("display", "Divorced");
                    deathRecord.MaritalStatus = mar;
                    break;
                case "Never married":
                    mar = new Dictionary<string, string>();
                    mar.Add("code", "S");
                    mar.Add("system", "http://hl7.org/fhir/v3/MaritalStatus");
                    mar.Add("display", "Never Married");
                    deathRecord.MaritalStatus = mar;
                    break;
                case "Unknown":
                    mar = new Dictionary<string, string>();
                    mar.Add("code", "UNK");
                    mar.Add("system", "http://hl7.org/fhir/v3/NullFlavor");
                    mar.Add("display", "unknown");
                    deathRecord.MaritalStatus = mar;
                    break;
            }

            if (GetValue(values, "sex.sex") == "Male")
            {
                Dictionary<string, string> bs = new Dictionary<string, string>();
                bs.Add("code", "M");
                bs.Add("system", "http://hl7.org/fhir/us/core/ValueSet/us-core-birthsex");
                bs.Add("display", "Male");
                deathRecord.BirthSex = bs;
            }
            else if (GetValue(values, "sex.sex") == "Female")
            {
                Dictionary<string, string> bs = new Dictionary<string, string>();
                bs.Add("code", "F");
                bs.Add("system", "http://hl7.org/fhir/us/core/ValueSet/us-core-birthsex");
                bs.Add("display", "Female");
                deathRecord.BirthSex = bs;
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

        /// <summary>Set a a Yes/No (coded) value on a Dictionary.</summary>
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

        /// <summary>Set a Yes/No (coded) value on a DeathRecord.</summary>
        private static void SetYesNoValueDeathRecordCode(DeathRecord deathRecord, string property, string value)
        {
            if (String.IsNullOrWhiteSpace(value))
            {
                return;
            }
            if (value.Trim().ToLower() == "yes")
            {
                Dictionary<string, string> mserv = new Dictionary<string, string>();
                mserv.Add("code", "Y");
                mserv.Add("system", "http://hl7.org/fhir/ValueSet/v2-0532");
                mserv.Add("display", "Yes");
                Type type = deathRecord.GetType();
                PropertyInfo prop = type.GetProperty(property);
                prop.SetValue(deathRecord, mserv, null);
            }
            else if (value.Trim().ToLower() == "no")
            {
                Dictionary<string, string> mserv = new Dictionary<string, string>();
                mserv.Add("code", "N");
                mserv.Add("system", "http://hl7.org/fhir/ValueSet/v2-0532");
                mserv.Add("display", "No");
                Type type = deathRecord.GetType();
                PropertyInfo prop = type.GetProperty(property);
                prop.SetValue(deathRecord, mserv, null);
            }
        }

        /// <summary>Set a Yes/No (coded) value on a DeathRecord.</summary>
        private static void SetYesNoValueDeathRecordBool(DeathRecord deathRecord, string property, string value)
        {
            if (String.IsNullOrWhiteSpace(value))
            {
                return;
            }
            if (value.Trim().ToLower() == "yes")
            {
                Type type = deathRecord.GetType();
                PropertyInfo prop = type.GetProperty(property);
                prop.SetValue(deathRecord, true, null);
            }
            else if (value.Trim().ToLower() == "no")
            {
                Type type = deathRecord.GetType();
                PropertyInfo prop = type.GetProperty(property);
                prop.SetValue(deathRecord, false, null);
            }
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
