using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.IO;
using System.Collections;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.FhirPath;

namespace FhirDeathRecord
{
    /// <summary>Class <c>DeathRecord</c> models a Standard Death Record (SDR).</summary>
    public class DeathRecord
    {
        private PocoNavigator Navigator;

        public Bundle Bundle;

        private Composition Composition;

        private Patient Patient;

        private Practitioner Practitioner;

        /// <summary>Default constructor that creates a new, empty FHIR SDR.</summary>
        public DeathRecord()
        {
            // Start with an empty Bundle.
            Bundle = new Bundle();
            Bundle.Type = Bundle.BundleType.Document; // By default, Bundle type is "document".

            // Add Composition to bundle. As the record is filled out, new entries will be added to this element.
            Composition = new Composition();
            Composition.Id = "urn:uuid:" + Guid.NewGuid().ToString();
            Composition.Status = CompositionStatus.Final;
            Composition.Meta = new Meta();
            string[] composition_profile = {"http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-deathRecord-DeathRecordContents"};
            Composition.Meta.Profile = composition_profile;
            Composition.Type = new CodeableConcept("http://loinc.org", "64297-5");
            Composition.Title = "Record of Death";
            Composition.Date = DateTime.Now.ToString("yyyy-MM-dd"); // By default, set creation date to now.
            Composition.Section = new List<Composition.SectionComponent>();
            Composition.SectionComponent section = new Composition.SectionComponent();
            section.Code = new CodeableConcept("http://loinc.org", "69453-9");
            Composition.Section.Add(section);
            Bundle.AddResourceEntry(Composition, Composition.Id);

            // Start with empty Patient to represent Decedent.
            Patient = new Patient();
            Patient.Id = "urn:uuid:" + Guid.NewGuid().ToString();
            Composition.Subject = new ResourceReference(Patient.Id);
            section.Entry.Add(new ResourceReference(Patient.Id));
            Bundle.AddResourceEntry(Patient, Patient.Id);

            // Start with empty Practitioner to represent Certifier.
            Practitioner = new Practitioner();
            Practitioner.Id = "urn:uuid:" + Guid.NewGuid().ToString();
            ResourceReference[] authors = { new ResourceReference(Practitioner.Id) };
            Composition.Author = authors.ToList();
            section.Entry.Add(new ResourceReference(Practitioner.Id));
            Bundle.AddResourceEntry(Practitioner, Practitioner.Id);

            Navigator = new PocoNavigator(Bundle);
        }

        /// <summary>Constructor that takes a string that represents a FHIR SDR in either XML or JSON format.</summary>
        /// <param name="record">a string that represents a FHIR SDR in either XML or JSON format.</param>
        /// <exception cref="ArgumentException">Record is neither valid XML nor JSON.</exception>
        public DeathRecord(string record)
        {
            // Try JSON
            try
            {
                FhirJsonParser parser = new FhirJsonParser();
                Bundle = parser.Parse<Bundle>(record);
                Navigator = new PocoNavigator(Bundle);
            }
            catch {}

            // Try XML
            try
            {
                FhirXmlParser parser = new FhirXmlParser();
                Bundle = parser.Parse<Bundle>(record);
                Navigator = new PocoNavigator(Bundle);
            }
            catch {}

            // If the given record string was not JSON or XML, fail immediately.
            if (Navigator == null)
            {
                throw new System.ArgumentException("Record is neither valid XML nor JSON.");
            }
        }

        /// <summary>Helper method to return a XML string representation of this Death Record.</summary>
        public string ToXML()
        {
            var serializer = new FhirXmlSerializer();
            return serializer.SerializeToString(Bundle);
        }

        /// <summary>Helper method to return a JSON string representation of this Death Record.</summary>
        public string ToJSON()
        {
            var serializer = new FhirJsonSerializer();
            return serializer.SerializeToString(Bundle);
        }

        /// <summary>Death Record ID.</summary>
        public string Id
        {
            get
            {
                return GetFirstString("Bundle.id");
            }
            set
            {
                Bundle.Id = value;
            }
        }

        /// <summary>Death Record Creation Date.</summary>
        public string CreationDate
        {
            get
            {
                return GetFirstString("Bundle.entry.resource.where($this is Composition).date");
            }
            set
            {
                Composition.Date = value;
            }
        }

        /// <summary>Decedent's Given Name.</summary>
        public string[] GivenName
        {
            get
            {
                return GetAllString("Bundle.entry.resource.where($this is Patient).name.given");
            }
            set
            {
                HumanName name = Patient.Name.FirstOrDefault(); // Check if there is already a HumanName on the Decedent.
                if (name != null)
                {
                    name.Given = value;
                }
                else
                {
                    name = new HumanName();
                    name.Use = HumanName.NameUse.Official;
                    name.Given = value;
                    Patient.Name.Add(name);
                }
            }
        }

        /// <summary>Decedent's Last Name.</summary>
        public string LastName
        {
            get
            {
                return GetFirstString("Bundle.entry.resource.where($this is Patient).name[0].family");
            }
            set
            {
                HumanName name = Patient.Name.FirstOrDefault(); // Check if there is already a HumanName on the Decedent.
                if (name != null)
                {
                    name.Family = value;
                }
                else
                {
                    name = new HumanName();
                    name.Use = HumanName.NameUse.Official;
                    name.Family = value;
                    Patient.Name.Add(name);
                }
            }
        }

        /// <summary>Decedent's Gender.</summary>
        public string Gender
        {
            get
            {
                return GetFirstString("Bundle.entry.resource.where($this is Patient).gender");
            }
            set
            {
                // TODO
            }
        }

        /// <summary>Decedent's Date of Birth.</summary>
        public string DateOfBirth
        {
            get
            {
                return GetFirstString("Bundle.entry.resource.where($this is Patient).birthDate");
            }
            set
            {
                // TODO
            }
        }

        /// <summary>Decedent's Date and Time of Death.</summary>
        public string DateOfDeath
         {
            get
            {
                return GetFirstString("Bundle.entry.resource.where($this is Patient).deceased");
            }
            set
            {
                // TODO
            }
        }

        /// <summary>Decedent's address.</summary>
        public Dictionary<string, string> Address
        {
            get
            {
                string street = GetFirstString("Bundle.entry.resource.where($this is Patient).address.line[0]");
                string city = GetFirstString("Bundle.entry.resource.where($this is Patient).address.city");
                string state = GetFirstString("Bundle.entry.resource.where($this is Patient).address.state");
                string zip = GetFirstString("Bundle.entry.resource.where($this is Patient).address.postalCode");
                Dictionary<string, string> dictionary = new Dictionary<string, string>();
                dictionary.Add("street", street);
                dictionary.Add("city", city);
                dictionary.Add("state", state);
                dictionary.Add("zip", zip);
                return dictionary;
            }
            set
            {
                // TODO
            }
        }

        /// <summary>Decedent's Social Security Number.</summary>
        public string SSN
        {
            get
            {
                return GetFirstString("Bundle.entry.resource.where($this is Patient).identifier.where(system = 'http://hl7.org/fhir/sid/us-ssn').value");
            }
            set
            {
                // TODO
            }
        }

        /// <summary>Decedent's Ethnicity.</summary>
        public string Ethnicity
        {
            get
            {
                return "TODO";
                //return GetFirstString("Bundle.entry.resource.where($this is Patient).extension.where(url = 'http://hl7.org/fhir/us/core/StructureDefinition/us-core-ethnicity').value.coding.display");
            }
            set
            {
                // TODO
            }
        }

        /// <summary>Decedent's Race.</summary>
        public string Race
        {
            get
            {
                return "TODO";
                //return GetFirstString("Bundle.entry.resource.where($this is Patient).extension.where(url = 'http://hl7.org/fhir/us/core/StructureDefinition/us-core-ethnicity').value.coding.display");
            }
            set
            {
                // TODO
            }
        }

        /// <summary>Given name(s) of certifier.</summary>
        public string[] CertifierGivenName
        {
            get
            {
                return GetAllString("Bundle.entry.resource.where($this is Practitioner).name.given");
            }
            set
            {
                HumanName name = Practitioner.Name.FirstOrDefault(); // Check if there is already a HumanName on the Decedent.
                if (name != null)
                {
                    name.Given = value;
                }
                else
                {
                    name = new HumanName();
                    name.Use = HumanName.NameUse.Official;
                    name.Given = value;
                    Practitioner.Name.Add(name);
                }
            }
        }

        /// <summary>Last name of certifier.</summary>
        public string CertifierLastName
        {
            get
            {
                return GetFirstString("Bundle.entry.resource.where($this is Practitioner).name[0].family");
            }
            set
            {
                HumanName name = Practitioner.Name.FirstOrDefault(); // Check if there is already a HumanName on the Decedent.
                if (name != null)
                {
                    string[] last = {value};
                    name.Given = last;
                }
                else
                {
                    name = new HumanName();
                    name.Use = HumanName.NameUse.Official;
                    string[] last = {value};
                    name.Given = last;
                    Practitioner.Name.Add(name);
                }
            }
        }

        /// <summary>Address of certifier.</summary>
        public Dictionary<string, string> CertifierAddress
        {
            get
            {
                string street = GetFirstString("Bundle.entry.resource.where($this is Practitioner).address.line[0]");
                string city = GetFirstString("Bundle.entry.resource.where($this is Practitioner).address.city");
                string state = GetFirstString("Bundle.entry.resource.where($this is Practitioner).address.state");
                string zip = GetFirstString("Bundle.entry.resource.where($this is Practitioner).address.postalCode");
                Dictionary<string, string> dictionary = new Dictionary<string, string>();
                dictionary.Add("street", street);
                dictionary.Add("city", city);
                dictionary.Add("state", state);
                dictionary.Add("zip", zip);
                return dictionary;
            }
            set
            {
                // TODO
            }
        }

        /// <summary>Type of certifier.</summary>
        public string CertifierType
        {
            get
            {
                return GetFirstString("Bundle.entry.resource.where($this is Practitioner).extension.where(url = 'http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-deathRecord-CertifierType-extension').value.coding.display");
            }
            set
            {
                //TODO
            }
        }

        /// <summary>Conditions that resulted in the underlying cause of death. Corresponds to part 1 of item 32 of the U.S.
        /// Standard Certificate of Death.</summary>
        public Tuple<string, string>[] CausesOfDeath
        {
            /// <returns>an array of tuples each containing the cause of death literal and the approximate interval onset to death.</returns>
            get
            {
                string[] causes = GetAllString($"Bundle.entry.resource.where($this is Condition).where(onset).text.div");
                string[] intervals = GetAllString($"Bundle.entry.resource.where($this is Condition).where(onset).onset");
                Regex htmlRegex = new Regex("<.*?>");
                return causes.Zip(intervals, (a, b) => Tuple.Create(htmlRegex.Replace(a, ""), b)).ToArray();
            }
            set
            {
                // TODO
            }
        }

        /// <summary>A significant condition that contributed to death but did not result in the underlying cause
        /// captured by a CauseOfDeathCondition. Corresponds to part 2 of item 32 of the U.S. Standard Certificate of Death.</summary>
        public string ContributingConditions
        {
            get
            {
                string cc = GetFirstString("Bundle.entry.resource.where($this is Condition).where(onset.empty()).text.div");
                if (cc != null)
                {
                    Regex htmlRegex = new Regex("<.*?>");
                    return htmlRegex.Replace(cc, "");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                // TODO
            }
        }

        /// <summary>Whether an autopsy was performed (true) or not (false). Corresponds to item 33 of the U.S. Standard
        /// Certificate of Death.</summary>
        /// <value>a Boolean describing this finding</value>
        public bool AutopsyPerformed
        {
            get
            {
                return GetFirstString("Bundle.entry.resource.where($this is Observation).where(code.coding.code='85699-7').value") == "True";
            }
            set
            {
                Observation observation = AddObservation("http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-causeOfDeath-AutopsyPerformed",
                                                         "85699-7",
                                                         "http://loinc.org",
                                                         "Autopsy was performed");
                observation.Value = new FhirBoolean(value);
            }
        }

        /// <summary>Were autopsy findings available to complete the cause of death? Corresponds to item 34 of the U.S.
        /// Standard Certificate of Death.</summary>
        /// <value>a Boolean describing this finding</value>
        public bool AutopsyResultsAvailable
        {
            get
            {
                return GetFirstString("Bundle.entry.resource.where($this is Observation).where(code.coding.code='69436-4').value") == "True";
            }
            set
            {
                Observation observation = AddObservation("http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-causeOfDeath-AutopsyResultsAvailable",
                                                         "69436-4",
                                                         "http://loinc.org",
                                                         "Autopsy results available");
                observation.Value = new FhirBoolean(value);
            }
        }

        /// <summary>The manner of the decendents demise. Corresponds to item 37 of the U.S. Standard Certificate of Death.</summary>
        /// <value>a Dictionary representing a code, containing the following key/value pairs:
        /// "code" - the code describing this finding
        /// "system" - the system the given code belongs to
        /// "display" - the human readable display text that corresponds to the given code
        ///</value>
        public Dictionary<string, string> MannerOfDeath
        {
            /// <returns>a tuple containing a code, code system, and display</returns>
            get
            {
                string code = GetFirstString("Bundle.entry.resource.where($this is Observation).where(code.coding.code='69449-7').value.coding.code");
                string system = GetFirstString("Bundle.entry.resource.where($this is Observation).where(code.coding.code='69449-7').value.coding.system");
                string display = GetFirstString("Bundle.entry.resource.where($this is Observation).where(code.coding.code='69449-7').value.coding.display");
                Dictionary<string, string> dictionary = new Dictionary<string, string>();
                dictionary.Add("code", code);
                dictionary.Add("system", system);
                dictionary.Add("display", display);
                return dictionary;
            }
            set
            {
                Observation observation = AddObservation("http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-causeOfDeath-MannerOfDeath",
                                                         "69449-7",
                                                         "http://loinc.org",
                                                         "Manner of death");
                observation.Value = new CodeableConcept(value["system"], value["code"], value["display"], null);
            }
        }

        /// <summary>Did tobacco use contribute to death. Corresponds to item 35 of the U.S. Standard Certificate of Death.</summary>
        /// <value>a Dictionary representing a code, containing the following key/value pairs:
        /// "code" - the code describing this finding
        /// "system" - the system the given code belongs to
        /// "display" - the human readable display text that corresponds to the given code
        ///</value>
        public Dictionary<string, string> TobaccoUseContributedToDeath
        {
            /// <returns>a tuple containing a code, code system, and display</returns>
            get
            {
                string code = GetFirstString("Bundle.entry.resource.where($this is Observation).where(code.coding.code='69443-0').value.coding.code");
                string system = GetFirstString("Bundle.entry.resource.where($this is Observation).where(code.coding.code='69443-0').value.coding.system");
                string display = GetFirstString("Bundle.entry.resource.where($this is Observation).where(code.coding.code='69443-0').value.coding.display");
                Dictionary<string, string> dictionary = new Dictionary<string, string>();
                dictionary.Add("code", code);
                dictionary.Add("system", system);
                dictionary.Add("display", display);
                return dictionary;
            }
            set
            {
                Observation observation = AddObservation("http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-causeOfDeath-TobaccoUseContributedToDeath",
                                                         "69443-0",
                                                         "http://loinc.org",
                                                         "Did tobacco use contribute to death");
                observation.Value = new CodeableConcept(value["system"], value["code"], value["display"], null);
            }
        }
        /// <summary>Actual or presumed date of death. Corresponds to item 29 of the U.S. Standard Certificate of Death.</summary>
        /// <value>a String describing the date/time of this finding</value>
        public string ActualOrPresumedDateOfDeath
        {
            get
            {
                return GetFirstString("Bundle.entry.resource.where($this is Observation).where(code.coding.code='81956-5').value");
            }
            set
            {
                Observation observation = AddObservation("http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-causeOfDeath-ActualOrPresumedDateOfDeath",
                                                         "81956-5",
                                                         "http://loinc.org",
                                                         "Date and time of death");
                observation.Value = new FhirDateTime(value);
            }
        }

        /// <summary>Date pronounced dead. Corresponds to items 24 and 25 of the U.S. Standard Certificate of Death.</summary>
        /// <value>a String describing the date/time of this finding</value>
        public string DatePronouncedDead
        {
            get
            {
                return GetFirstString("Bundle.entry.resource.where($this is Observation).where(code.coding.code='80616-6').value");
            }
            set
            {
                Observation observation = AddObservation("http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-causeOfDeath-DatePronouncedDead",
                                                         "80616-6",
                                                         "http://loinc.org",
                                                         "Date and time pronounced dead");
                observation.Value = new FhirDateTime(value);
            }
        }

        /// <summary>Did death result from injury at work? Corresponds to item 41 of the U.S. Standard Certificate of Death.</summary>
        /// <value>a Boolean describing this finding</value>
        public bool DeathFromWorkInjury
        {
            get
            {
                return GetFirstString("Bundle.entry.resource.where($this is Observation).where(code.coding.code='69444-8').value") == "True";
            }
            set
            {
                Observation observation = AddObservation("http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-causeOfDeath-DeathFromWorkInjury",
                                                         "69444-8",
                                                         "http://loinc.org",
                                                         "Did death result from injury at work");
                observation.Value = new FhirBoolean(value);
            }
        }

        /// <summary>Injury leading to death associated with transportation event. Corresponds to item 44 of the U.S. Standard Certificate of Death.</summary>
        /// <value>a Dictionary representing a code, containing the following key/value pairs:
        /// "code" - the code describing this finding
        /// "system" - the system the given code belongs to
        /// "display" - the human readable display text that corresponds to the given code
        ///</value>
        public Dictionary<string, string> DeathFromTransportInjury
        {
            get
            {
                string code = GetFirstString("Bundle.entry.resource.where($this is Observation).where(code.coding.code='69448-9').value.coding.code");
                string system = GetFirstString("Bundle.entry.resource.where($this is Observation).where(code.coding.code='69448-9').value.coding.system");
                string display = GetFirstString("Bundle.entry.resource.where($this is Observation).where(code.coding.code='69448-9').value.coding.display");
                Dictionary<string, string> dictionary = new Dictionary<string, string>();
                dictionary.Add("code", code);
                dictionary.Add("system", system);
                dictionary.Add("display", display);
                return dictionary;
            }
            set
            {
                Observation observation = AddObservation("http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-causeOfDeath-DeathFromTransportInjury",
                                                         "69448-9",
                                                         "http://loinc.org",
                                                         "Injury leading to death associated with transportation event");
                observation.Value = new CodeableConcept(value["system"], value["code"], value["display"], null);
            }
        }

        /// <summary>Whether a medical examiner or coroner was contacted. Corresponds to item 31 of the U.S. Standard Certificate of Death.</summary>
        /// <value>a Boolean describing this finding</value>
        public bool MedicalExaminerContacted
        {
            get
            {
                return GetFirstString("Bundle.entry.resource.where($this is Observation).where(code.coding.code='74497-9').value") == "True";
            }
            set
            {
                Observation observation = AddObservation("http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-causeOfDeath-MedicalExaminerContacted",
                                                         "74497-9",
                                                         "http://loinc.org",
                                                         "Medical examiner or coroner was contacted");
                observation.Value = new FhirBoolean(value);
            }
        }

        /// <summary>Timing Of Recent Pregnancy In Relation To Death. Corresponds to item 36 of the U.S. Standard Certificate of Death.</summary>
        /// <value>a Dictionary representing a code, containing the following key/value pairs:
        /// "code" - the code describing this finding
        /// "system" - the system the given code belongs to
        /// "display" - the human readable display text that corresponds to the given code
        ///</value>
        public Dictionary<string, string> TimingOfRecentPregnancyInRelationToDeath
        {
            get
            {
                string code = GetFirstString("Bundle.entry.resource.where($this is Observation).where(code.coding.code='69442-2').value.coding.code");
                string system = GetFirstString("Bundle.entry.resource.where($this is Observation).where(code.coding.code='69442-2').value.coding.system");
                string display = GetFirstString("Bundle.entry.resource.where($this is Observation).where(code.coding.code='69442-2').value.coding.display");
                Dictionary<string, string> dictionary = new Dictionary<string, string>();
                dictionary.Add("code", code);
                dictionary.Add("system", system);
                dictionary.Add("display", display);
                return dictionary;
            }
            set
            {
                Observation observation = AddObservation("http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-causeOfDeath-TimingOfRecentPregnancyInRelationToDeath",
                                                         "69442-2",
                                                         "http://loinc.org",
                                                         "Timing of recent pregnancy in relation to death");
                observation.Value = new CodeableConcept(value["system"], value["code"], value["display"], null);
            }
        }

        /// <summary>Injury incident description.</summary>
        /// <value>a Dictionary representing a description of an injury incident, containing the following key/value pairs:
        /// "placeOfInjuryDescription" - description of the place of injury, e.g. decedent’s home, restaurant, wooded area
        /// "effectiveDateTime" - effective date and time of injury
        /// "description" - description of injury
        /// "placeOfInjuryLine1" - location of injury, line one
        /// "placeOfInjuryLine2" - location of injury, line two
        /// "placeOfInjuryCity" - location of injury, city
        /// "placeOfInjuryState" - location of injury, state
        /// "placeOfInjuryZip" - location of injury, zip
        /// "placeOfInjuryCountry" - location of injury, country
        /// "placeOfInjuryInsideCityLimits" - location of injury, whether the address is within city limits (true) or not (false)
        ///</value>
        public Dictionary<string, string> DetailsOfInjury
        {
            get
            {
                Dictionary<string, string> dictionary = new Dictionary<string, string>();

                // Place of injury - Description of the place of injury, e.g. decedent’s home, restaurant, wooded area
                dictionary.Add("placeOfInjuryDescription", GetFirstString("Bundle.entry.resource.where($this is Observation).where(code.coding.code='11374-6').extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-causeOfDeath-PlaceOfInjury-extension').value"));

                // Effective date and time of injury
                dictionary.Add("effectiveDateTime", GetFirstString("Bundle.entry.resource.where($this is Observation).where(code.coding.code='11374-6').effective"));

                // Description of injury
                dictionary.Add("description", GetFirstString("Bundle.entry.resource.where($this is Observation).where(code.coding.code='11374-6').value"));

                // Location of injury
                dictionary.Add("placeOfInjuryLine1", GetFirstString("Bundle.entry.resource.where($this is Observation).where(code.coding.code='11374-6').extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/shr-core-PostalAddress-extension').value.line[0]"));
                dictionary.Add("placeOfInjuryLine2", GetFirstString("Bundle.entry.resource.where($this is Observation).where(code.coding.code='11374-6').extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/shr-core-PostalAddress-extension').value.line[1]"));
                dictionary.Add("placeOfInjuryCity", GetFirstString("Bundle.entry.resource.where($this is Observation).where(code.coding.code='11374-6').extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/shr-core-PostalAddress-extension').value.city"));
                dictionary.Add("placeOfInjuryState", GetFirstString("Bundle.entry.resource.where($this is Observation).where(code.coding.code='11374-6').extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/shr-core-PostalAddress-extension').value.state"));
                dictionary.Add("placeOfInjuryZip", GetFirstString("Bundle.entry.resource.where($this is Observation).where(code.coding.code='11374-6').extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/shr-core-PostalAddress-extension').value.postalCode"));
                dictionary.Add("placeOfInjuryCountry", GetFirstString("Bundle.entry.resource.where($this is Observation).where(code.coding.code='11374-6').extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/shr-core-PostalAddress-extension').value.country"));
                dictionary.Add("placeOfInjuryInsideCityLimits", GetFirstString("Bundle.entry.resource.where($this is Observation).where(code.coding.code='11374-6').extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/shr-core-PostalAddress-extension').value.extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/shr-core-InsideCityLimits-extension').value"));

                return dictionary;
            }
            set
            {
                Observation observation = AddObservation("http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-causeOfDeath-DetailsOfInjury",
                                                         "11374-6",
                                                         "http://loinc.org",
                                                         "Injury incident description");
                observation.Value = new FhirString(value["description"]);
                observation.Effective = new FhirDateTime(value["effectiveDateTime"]);
                Extension placeOfInjury = new Extension();
                placeOfInjury.Url = "http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-causeOfDeath-PlaceOfInjury-extension";
                placeOfInjury.Value = new FhirString(value["placeOfInjuryDescription"]);
                observation.Extension.Add(placeOfInjury);
                Extension placeOfInjuryLocation = new Extension();
                placeOfInjuryLocation.Url = "http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/shr-core-PostalAddress-extension";
                Address placeOfInjuryLocationAddress = new Address();
                string[] lines = {value["placeOfInjuryLine1"], value["placeOfInjuryLine2"]};
                placeOfInjuryLocationAddress.Line = lines.ToArray();
                placeOfInjuryLocationAddress.City = value["placeOfInjuryCity"];
                placeOfInjuryLocationAddress.State = value["placeOfInjuryState"];
                placeOfInjuryLocationAddress.PostalCode = value["placeOfInjuryZip"];
                placeOfInjuryLocationAddress.Country = value["placeOfInjuryCountry"];
                Extension insideCityLimits = new Extension();
                insideCityLimits.Url = "http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/shr-core-InsideCityLimits-extension";
                insideCityLimits.Value = new FhirBoolean(value["placeOfInjuryInsideCityLimits"] == "true");
                placeOfInjuryLocationAddress.Extension.Add(insideCityLimits);
                placeOfInjuryLocation.Value = placeOfInjuryLocationAddress;
                observation.Extension.Add(placeOfInjuryLocation);
            }
        }

        /// <summary>Add a new observation to the Death Record.</summary>
        /// <param name="profile">the observation profile.</param>
        /// <param name="code">the observation code.</param>
        /// <param name="system">the observation code system.</param>
        /// <param name="display">the observation code display.</param>
        private Observation AddObservation(string profile, string code, string system, string display)
        {
            Observation observation = new Observation();
            observation.Id = "urn:uuid:" + Guid.NewGuid().ToString();
            observation.Subject = new ResourceReference(Patient.Id);
            observation.Meta = new Meta();
            string[] observation_profile = {profile};
            observation.Meta.Profile = observation_profile;
            observation.Status = ObservationStatus.Final;
            observation.Code = new CodeableConcept(system, code, display, null);
            AddReferenceToComposition(observation.Id);
            Bundle.AddResourceEntry(observation, observation.Id);
            return observation;
        }

        /// <summary>Add a reference to the Death Record Composition.</summary>
        /// <param name="path">a reference.</param>
        private void AddReferenceToComposition(string reference)
        {
            Composition.Section.First().Entry.Add(new ResourceReference(reference));
        }

        /// <summary>Given a FHIR path, return the elements that match the given path;
        /// returns an empty array if no matches are found.</summary>
        /// <param name="path">a string representing a FHIR path.</param>
        /// <returns>all elements that match the given path, or an empty array if no matches are found.</returns>
        private object[] GetAll(string path)
        {
            var matches = Navigator.Select(path);
            ArrayList list = new ArrayList();
            foreach (var match in matches)
            {
                list.Add(match.Value);
            }
            return list.ToArray();
        }

        /// <summary>Given a FHIR path, return the first element that matches the given path.</summary>
        /// <param name="path">a string representing a FHIR path.</param>
        /// <returns>the first element that matches the given path, or null if no match is found.</returns>
        private object GetFirst(string path)
        {
            var matches = Navigator.Select(path);
            if (matches.Count() > 0)
            {
                return matches.First().Value;
            }
            else
            {
                return null;
            }
        }

        /// <summary>Given a FHIR path, return the last element that matches the given path.</summary>
        /// <param name="path">a string representing a FHIR path.</param>
        /// <returns>the last element that matches the given path, or null if no match is found.</returns>
        private object GetLast(string path)
        {
            var matches = Navigator.Select(path);
            if (matches.Count() > 0)
            {
                return matches.Last().Value;
            }
            else
            {
                return null;
            }
        }

        /// <summary>Given a FHIR path, return the elements that match the given path as a string;
        /// returns an empty array if no matches are found.</summary>
        /// <param name="path">a string representing a FHIR path.</param>
        /// <returns>all elements that match the given path as a string, or an empty array if no matches are found.</returns>
        private string[] GetAllString(string path)
        {
            ArrayList list = new ArrayList();
            foreach (var match in GetAll(path))
            {
                list.Add(Convert.ToString(match));
            }
            return list.ToArray(typeof(string)) as string[];
        }

        /// <summary>Given a FHIR path, return the first element that matches the given path as a string;
        /// returns an empty string if no match is found.</summary>
        /// <param name="path">a string representing a FHIR path.</param>
        /// <returns>the first element that matches the given path as a string, or null if no match is found.</returns>
        private string GetFirstString(string path)
        {
            var first = GetFirst(path);
            if (first != null)
            {
                return Convert.ToString(first);
            }
            else
            {
                return null;
            }
        }

        /// <summary>Given a FHIR path, return the last element that matches the given path as a string;
        /// returns an empty string if no match is found.</summary>
        /// <param name="path">a string representing a FHIR path.</param>
        /// <returns>the last element that matches the given path as a string, or null if no match is found.</returns>
        private string GetLastString(string path)
        {
            var last = GetFirst(path);
            if (last != null)
            {
                return Convert.ToString(last);
            }
            else
            {
                return null;
            }
        }
    }

}