using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.FhirPath;

namespace FhirDeathRecord
{
    /// <summary>Class <c>DeathRecord</c> models a FHIR Standard Death Record (SDR).
    /// For consuming a FHIR SDR (in either XML or JSON format), there is a constructor that
    /// takes a string, which will parse the given FHIR SDR and build a new <c>DeathRecord</c> that
    /// represents it. You can then use the various property getters to infer details about the
    /// record.
    /// For producing a new FHIR SDR, there is a default no-param constructor that creates the bare
    /// bones necessary for a new record. You can then use the various property setters to
    /// populate details of the record. All <c>DeathRecord</c>s can be converted to FHIR XML or JSON
    /// suitable for transmission, by making use of the <c>ToXML()</c> and <c>ToJSON()</c> methods.
    /// </summary>
    public class DeathRecord
    {
        /// <summary>Useful for navigating around the FHIR Bundle using FHIRPaths.</summary>
        private PocoNavigator Navigator;

        /// <summary>Bundle that contains the death record.</summary>
        private Bundle Bundle;

        /// <summary>Composition that described what the Bundle is, as well as keeping references to its contents.</summary>
        private Composition Composition;

        /// <summary>The decedent.</summary>
        private Patient Patient;

        /// <summary>The Certifier.</summary>
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

            // Create a Navigator for this new death record.
            Navigator = new PocoNavigator(Bundle);
        }

        /// <summary>Constructor that takes a string that represents a FHIR SDR in either XML or JSON format.</summary>
        /// <param name="record">represents a FHIR SDR in either XML or JSON format.</param>
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
        /// <returns>a string representation of this Death Record in XML format</returns>
        public string ToXML()
        {
            var serializer = new FhirXmlSerializer();
            return serializer.SerializeToString(Bundle);
        }

        /// <summary>Helper method to return a JSON string representation of this Death Record.</summary>
        /// <returns>a string representation of this Death Record in JSON format</returns>
        public string ToJSON()
        {
            var serializer = new FhirJsonSerializer();
            return serializer.SerializeToString(Bundle);
        }


        /////////////////////////////////////////////////////////////////////////////////
        //
        // Class Properties related to the record itself (i.e. record id).
        //
        /////////////////////////////////////////////////////////////////////////////////

        /// <summary>Death Record ID.</summary>
        /// <value>the record identification</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.Id = "42";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Record identification: {ExampleDeathRecord.Id}");</para>
        /// </example>
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
        /// <value>when the record was created</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.CreationDate = "2018-07-11";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Record was created on: {ExampleDeathRecord.CreationDate}");</para>
        /// </example>
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


        /////////////////////////////////////////////////////////////////////////////////
        //
        // Class Properties related to the Decedent (Patient).
        //
        /////////////////////////////////////////////////////////////////////////////////

        /// <summary>Decedent's Given Name(s). Middle name should be the last entry.</summary>
        /// <value>the decedent's name (first, etc., middle)</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>string[] names = {"Example", "Something", "Middle"};</para>
        /// <para>ExampleDeathRecord.GivenNames = names;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Decedent Given Name(s): {string.Join(", ", ExampleDeathRecord.GivenNames)}");</para>
        /// </example>
        public string[] GivenNames
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

        /// <summary>Decedent's First Name. This is essentially the same as the first thing in
        /// <c>GivenNames</c>. Setting this value will prepend whatever is given to the start
        /// of <c>GivenNames</c> if <c>GivenNames</c> already exists.</summary>
        /// <value>the decedent's first name</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.FirstName = "Example";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Decedent First Name: {ExampleDeathRecord.FirstName}");</para>
        /// </example>
        public string FirstName
        {
            get
            {
                return GetFirstString("Bundle.entry.resource.where($this is Patient).name.given");
            }
            set
            {
                HumanName name = Patient.Name.FirstOrDefault(); // Check if there is already a HumanName on the Decedent.
                if (name != null)
                {
                    string[] firstName = new String[] {value};
                    if (name.Given.First() == "") // Looks like middle name was set first, replace fake first name.
                    {
                        name.Given = firstName.Concat(name.Given.Skip(1).ToArray()).ToArray();
                    }
                    else
                    {
                        name.Given = firstName.Concat(name.Given).ToArray();
                    }
                }
                else
                {
                    name = new HumanName();
                    name.Use = HumanName.NameUse.Official;
                    name.Given = new String[] {value, ""}; // Put an empty "fake" middle name at the end.
                    Patient.Name.Add(name);
                }
            }
        }

        /// <summary>Decedent's Middle Name. This is essentially the same as the last thing in
        /// <c>GivenNames</c>. Setting this value will append whatever is given to the end
        /// of <c>GivenNames</c> if <c>GivenNames</c> already exists.</summary>
        /// <value>the decedent's middle name</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.MiddleName = "Middle";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Decedent Middle Name: {ExampleDeathRecord.MiddleName}");</para>
        /// </example>
        public string MiddleName
        {
            get
            {
                return GetLastString("Bundle.entry.resource.where($this is Patient).name.given");
            }
            set
            {
                HumanName name = Patient.Name.FirstOrDefault(); // Check if there is already a HumanName on the Decedent.
                if (name != null)
                {
                    string[] middleName = new String[] {value};
                    if (name.Given.Last() == "") // Looks like first name was set first, replace fake middle name.
                    {
                        name.Given = name.Given.Take(name.Given.Count() - 1).ToArray().Concat(middleName).ToArray();
                    }
                    else
                    {
                        name.Given = name.Given.Concat(middleName).ToArray();
                    }
                }
                else
                {
                    name = new HumanName();
                    name.Use = HumanName.NameUse.Official;
                    name.Given = new String[] {"", value}; // Put an empty "fake" first name at the start.
                    Patient.Name.Add(name);
                }
            }
        }

        /// <summary>Decedent's Family Name.</summary>
        /// <value>the decedent's family name (i.e. last name)</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.FamilyName = "Last";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Decedent's Last Name: {ExampleDeathRecord.FamilyName}");</para>
        /// </example>
        public string FamilyName
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

        /// <summary>Decedent's Suffix.</summary>
        /// <value>the decedent's suffix</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.Suffix = "Jr.";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Decedent Suffix: {ExampleDeathRecord.Suffix}");</para>
        /// </example>
        public string Suffix
        {
            get
            {
                return GetFirstString("Bundle.entry.resource.where($this is Patient).name.suffix");
            }
            set
            {
                HumanName name = Patient.Name.FirstOrDefault(); // Check if there is already a HumanName on the Decedent.
                if (name != null)
                {
                    string[] suffix = { value };
                    name.Suffix = suffix;
                }
                else
                {
                    name = new HumanName();
                    name.Use = HumanName.NameUse.Official;
                    string[] suffix = { value };
                    name.Suffix = suffix;
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
                switch(value)
                {
                    case "male":
                        Patient.Gender = AdministrativeGender.Male;
                        break;
                    case "female":
                        Patient.Gender = AdministrativeGender.Female;
                        break;
                    case "other":
                        Patient.Gender = AdministrativeGender.Other;
                        break;
                    case "unknown":
                        Patient.Gender = AdministrativeGender.Unknown;
                        break;
                }
            }
        }

        /// <summary>Decedent's Birth Sex.</summary>
        public Dictionary<string, string> BirthSex
        {
            get
            {
                string code = GetFirstString("Bundle.entry.resource.where($this is Patient).extension.where(url='http://hl7.org/fhir/us/core/StructureDefinition/us-core-birthsex').value.coding.code");
                string system = GetFirstString("Bundle.entry.resource.where($this is Patient).extension.where(url='http://hl7.org/fhir/us/core/StructureDefinition/us-core-birthsex').value.coding.system");
                string display = GetFirstString("Bundle.entry.resource.where($this is Patient).extension.where(url='http://hl7.org/fhir/us/core/StructureDefinition/us-core-birthsex').value.coding.display");
                Dictionary<string, string> birthsex = new Dictionary<string, string>();
                birthsex.Add("code", code);
                birthsex.Add("system", system);
                birthsex.Add("display", display);
                return birthsex;
            }
            set
            {
                Extension birthsex = new Extension();
                birthsex.Url = "http://hl7.org/fhir/us/core/StructureDefinition/us-core-birthsex";
                birthsex.Value = DictToCodeableConcept(value);
                Patient.Extension.Add(birthsex);
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
                Patient.BirthDate = value;
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
                Patient.Deceased = new FhirDateTime(value);
            }
        }

        /// <summary>Decedent's residence.</summary>
        /// <value>Decedent's residence. A Dictionary representing residence address, containing the following key/value pairs:
        /// <para>"residenceLine1" - residence, line one</para>
        /// <para>"residenceLine2" - residence, line two</para>
        /// <para>"residenceCity" - residence, city</para>
        /// <para>"residenceCounty" - residence, county</para>
        /// <para>"residenceState" - residence, state</para>
        /// <para>"residenceZip" - residence, zip</para>
        /// <para>"residenceCountry" - residence, country</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; residence = new Dictionary&lt;string, string&gt;();</para>
        /// <para>residence.Add("placeOfBirthLine1", "9 Example Street");
        /// <para>residence.Add("placeOfBirthLine2", "Line 2");
        /// <para>residence.Add("placeOfBirthCity", "Bedford");
        /// <para>residence.Add("placeOfBirthCounty", "Middlesex");
        /// <para>residence.Add("placeOfBirthState", "Massachusetts");
        /// <para>residence.Add("placeOfBirthZip", "01730");
        /// <para>residence.Add("placeOfBirthCountry", "United States");
        /// <para>SetterDeathRecord.Residence = residence;
        /// <para>// Getter:</para>
        /// <para>string state = ExampleDeathRecord.<para>["residenceState"];</para>
        /// <para>Console.WriteLine($"State of residence: {state}");</para>
        /// </example>
        public Dictionary<string, string> Residence
        {
            get
            {
                Dictionary<string, string> dictionary = new Dictionary<string, string>();

                // Place Of Birth Address
                dictionary.Add("residenceLine1", GetFirstString("Bundle.entry.resource.where($this is Patient).address.line[0]"));
                dictionary.Add("residenceLine2", GetFirstString("Bundle.entry.resource.where($this is Patient).address.line[1]"));
                dictionary.Add("residenceCity", GetFirstString("Bundle.entry.resource.where($this is Patient).address.city"));
                dictionary.Add("residenceCounty", GetFirstString("Bundle.entry.resource.where($this is Patient).address.district"));
                dictionary.Add("residenceState", GetFirstString("Bundle.entry.resource.where($this is Patient).address.state"));
                dictionary.Add("residenceZip", GetFirstString("Bundle.entry.resource.where($this is Patient).address.postalCode"));
                dictionary.Add("residenceCountry", GetFirstString("Bundle.entry.resource.where($this is Patient).address.country"));

                return dictionary;
            }
            set
            {
                Address address = new Address();
                address.LineElement.Add(new FhirString(value["residenceLine1"]));
                address.LineElement.Add(new FhirString(value["residenceLine2"]));
                address.City = value["residenceCity"];
                address.District = value["residenceCounty"];
                address.State = value["residenceState"];
                address.PostalCodeElement = new FhirString(value["residenceZip"]);
                address.Country = value["residenceCountry"];
                Patient.Address.Add(address);
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
                Identifier ssn = new Identifier();
                ssn.System = "http://hl7.org/fhir/sid/us-ssn";
                ssn.Value = value;
                Patient.Identifier.Add(ssn);
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

        /// <summary>Decedent's Place Of Birth.</summary>
        /// <value>Decedent's Place Of Birth. A Dictionary representing a place of birth address, containing the following key/value pairs:
        /// <para>"placeOfBirthLine1" - location of birth, line one</para>
        /// <para>"placeOfBirthLine2" - location of birth, line two</para>
        /// <para>"placeOfBirthCity" - location of birth, city</para>
        /// <para>"placeOfBirthState" - location of birth, state</para>
        /// <para>"placeOfBirthZip" - location of birth, zip</para>
        /// <para>"placeOfBirthCountry" - location of birth, country</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; placeOfBirth = new Dictionary&lt;string, string&gt;();</para>
        /// <para>placeOfBirth.Add("placeOfBirthLine1", "9 Example Street");
        /// <para>placeOfBirth.Add("placeOfBirthLine2", "Line 2");
        /// <para>placeOfBirth.Add("placeOfBirthCity", "Bedford");
        /// <para>placeOfBirth.Add("placeOfBirthState", "Massachusetts");
        /// <para>placeOfBirth.Add("placeOfBirthZip", "01730");
        /// <para>placeOfBirth.Add("placeOfBirthCountry", "United States");
        /// <para>SetterDeathRecord.PlaceOfBirth = placeOfBirth;
        /// <para>// Getter:</para>
        /// <para>string state = ExampleDeathRecord.<para>["placeOfBirthState"];</para>
        /// <para>Console.WriteLine($"State where decedent was born: {state}");</para>
        /// </example>
        public Dictionary<string, string> PlaceOfBirth
        {
            get
            {
                Dictionary<string, string> dictionary = new Dictionary<string, string>();

                // Place Of Birth Address
                dictionary.Add("placeOfBirthLine1", GetFirstString("Bundle.entry.resource.where($this is Patient).extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-Birthplace-extension').value.line[0]"));
                dictionary.Add("placeOfBirthLine2", GetFirstString("Bundle.entry.resource.where($this is Patient).extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-Birthplace-extension').value.line[1]"));
                dictionary.Add("placeOfBirthCity", GetFirstString("Bundle.entry.resource.where($this is Patient).extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-Birthplace-extension').value.city"));
                dictionary.Add("placeOfBirthState", GetFirstString("Bundle.entry.resource.where($this is Patient).extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-Birthplace-extension').value.state"));
                dictionary.Add("placeOfBirthZip", GetFirstString("Bundle.entry.resource.where($this is Patient).extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-Birthplace-extension').value.postalCode"));
                dictionary.Add("placeOfBirthCountry", GetFirstString("Bundle.entry.resource.where($this is Patient).extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-Birthplace-extension').value.country"));

                return dictionary;
            }
            set
            {
                // Place Of Birth extension
                Extension placeOfBirthExt = new Extension();
                placeOfBirthExt.Url = "http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-Birthplace-extension";

                // Place Of Birth Address
                Address placeOfBirthAddress = new Address();
                string[] lines = {value["placeOfBirthLine1"], value["placeOfBirthLine2"]};
                placeOfBirthAddress.Line = lines.ToArray();
                placeOfBirthAddress.City = value["placeOfBirthCity"];
                placeOfBirthAddress.State = value["placeOfBirthState"];
                placeOfBirthAddress.PostalCode = value["placeOfBirthZip"];
                placeOfBirthAddress.Country = value["placeOfBirthCountry"];
                placeOfBirthAddress.Type = Hl7.Fhir.Model.Address.AddressType.Postal;
                placeOfBirthExt.Value = placeOfBirthAddress;

                Patient.Extension.Add(placeOfBirthExt);
            }
        }

        /// <summary>Decedent's Place Of Death.</summary>
        /// <value>Decedent's Place Of Death. A Dictionary representing a place of death, containing the following key/value pairs:
        /// <para>"placeOfDeathTypeCode" - place of death type, code</para>
        /// <para>"placeOfDeathTypeSystem" - place of death type, code system</para>
        /// <para>"placeOfDeathTypeDisplay" - place of death type, code display</para>
        /// <para>"placeOfDeathFacilityName" - place of death facility name</para>
        /// <para>"placeOfDeathLine1" - location of death, line one</para>
        /// <para>"placeOfDeathLine2" - location of death, line two</para>
        /// <para>"placeOfDeathCity" - location of death, city</para>
        /// <para>"placeOfDeathState" - location of death, state</para>
        /// <para>"placeOfDeathZip" - location of death, zip</para>
        /// <para>"placeOfDeathCountry" - location of death, country</para>
        /// <para>"placeOfDeathInsideCityLimits" - location of death, whether the address is within city limits (true) or not (false)</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; placeOfDeath = new Dictionary&lt;string, string&gt;();</para>
        /// <para>placeOfDeath.Add("placeOfDeathTypeCode", "16983000");
        /// <para>placeOfDeath.Add("placeOfDeathTypeSystem", "http://snomed.info/sct");
        /// <para>placeOfDeath.Add("placeOfDeathTypeDisplay", "Death in hospital");
        /// <para>placeOfDeath.Add("placeOfDeathFacilityName", "Example Hospital");
        /// <para>placeOfDeath.Add("placeOfDeathLine1", "8 Example Street");
        /// <para>placeOfDeath.Add("placeOfDeathLine2", "Line 2");
        /// <para>placeOfDeath.Add("placeOfDeathCity", "Bedford");
        /// <para>placeOfDeath.Add("placeOfDeathState", "Massachusetts");
        /// <para>placeOfDeath.Add("placeOfDeathZip", "01730");
        /// <para>placeOfDeath.Add("placeOfDeathCountry", "United States");
        /// <para>placeOfDeath.Add("placeOfDeathInsideCityLimits", "True");
        /// <para>SetterDeathRecord.PlaceOfDeath = placeOfDeath;
        /// <para>// Getter:</para>
        /// <para>string state = ExampleDeathRecord.<para>["placeOfDeathState"];</para>
        /// <para>Console.WriteLine($"State where death occurred: {state}");</para>
        /// </example>
        public Dictionary<string, string> PlaceOfDeath
        {
            get
            {
                Dictionary<string, string> dictionary = new Dictionary<string, string>();

                // Place Of Death Type
                dictionary.Add("placeOfDeathTypeCode", GetFirstString("Bundle.entry.resource.where($this is Patient).extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-PlaceOfDeath-extension').extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-PlaceOfDeathType-extension').value.coding.code"));
                dictionary.Add("placeOfDeathTypeSystem", GetFirstString("Bundle.entry.resource.where($this is Patient).extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-PlaceOfDeath-extension').extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-PlaceOfDeathType-extension').value.coding.system"));
                dictionary.Add("placeOfDeathTypeDisplay", GetFirstString("Bundle.entry.resource.where($this is Patient).extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-PlaceOfDeath-extension').extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-PlaceOfDeathType-extension').value.coding.display"));

                // Place Of Death Facility Name
                dictionary.Add("placeOfDeathFacilityName", GetFirstString("Bundle.entry.resource.where($this is Patient).extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-PlaceOfDeath-extension').extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-FacilityName-extension').value"));

                // Place Of Death Address
                dictionary.Add("placeOfDeathLine1", GetFirstString("Bundle.entry.resource.where($this is Patient).extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-PlaceOfDeath-extension').extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/shr-core-PostalAddress-extension').value.line[0]"));
                dictionary.Add("placeOfDeathLine2", GetFirstString("Bundle.entry.resource.where($this is Patient).extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-PlaceOfDeath-extension').extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/shr-core-PostalAddress-extension').value.line[1]"));
                dictionary.Add("placeOfDeathCity", GetFirstString("Bundle.entry.resource.where($this is Patient).extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-PlaceOfDeath-extension').extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/shr-core-PostalAddress-extension').value.city"));
                dictionary.Add("placeOfDeathState", GetFirstString("Bundle.entry.resource.where($this is Patient).extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-PlaceOfDeath-extension').extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/shr-core-PostalAddress-extension').value.state"));
                dictionary.Add("placeOfDeathZip", GetFirstString("Bundle.entry.resource.where($this is Patient).extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-PlaceOfDeath-extension').extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/shr-core-PostalAddress-extension').value.postalCode"));
                dictionary.Add("placeOfDeathCountry", GetFirstString("Bundle.entry.resource.where($this is Patient).extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-PlaceOfDeath-extension').extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/shr-core-PostalAddress-extension').value.country"));
                dictionary.Add("placeOfDeathInsideCityLimits", GetFirstString("Bundle.entry.resource.where($this is Patient).extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-PlaceOfDeath-extension').extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/shr-core-PostalAddress-extension').value.extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/shr-core-InsideCityLimits-extension').value"));

                return dictionary;
            }
            set
            {
                // Place Of Death extension
                Extension placeOfDeathExt = new Extension();
                placeOfDeathExt.Url = "http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-PlaceOfDeath-extension";

                // Place Of Death Type extension
                Extension placeOfDeathTypeExt = new Extension();
                placeOfDeathTypeExt.Url = "http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-PlaceOfDeathType-extension";
                CodeableConcept codeableConcept = new CodeableConcept();
                Coding coding = new Coding();
                coding.Code = value["placeOfDeathTypeCode"];
                coding.System = value["placeOfDeathTypeSystem"];
                coding.Display = value["placeOfDeathTypeDisplay"];
                codeableConcept.Coding.Add(coding);
                placeOfDeathTypeExt.Value = codeableConcept;
                placeOfDeathExt.Extension.Add(placeOfDeathTypeExt);

                // Place Of Death Facility Name extension
                Extension placeOfDeathFacilityNameExt = new Extension();
                placeOfDeathFacilityNameExt.Url = "http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-FacilityName-extension";
                placeOfDeathFacilityNameExt.Value = new FhirString(value["placeOfDeathFacilityName"]);
                placeOfDeathExt.Extension.Add(placeOfDeathFacilityNameExt);

                // Place Of Death Address extension
                Extension placeOfDeathAddressExt = new Extension();
                placeOfDeathAddressExt.Url = "http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/shr-core-PostalAddress-extension";
                Address placeOfDeathAddress = new Address();
                string[] lines = {value["placeOfDeathLine1"], value["placeOfDeathLine2"]};
                placeOfDeathAddress.Line = lines.ToArray();
                placeOfDeathAddress.City = value["placeOfDeathCity"];
                placeOfDeathAddress.State = value["placeOfDeathState"];
                placeOfDeathAddress.PostalCode = value["placeOfDeathZip"];
                placeOfDeathAddress.Country = value["placeOfDeathCountry"];
                placeOfDeathAddress.Type = Hl7.Fhir.Model.Address.AddressType.Postal;
                Extension insideCityLimits = new Extension();
                insideCityLimits.Url = "http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/shr-core-InsideCityLimits-extension";
                insideCityLimits.Value = new FhirBoolean(value["placeOfDeathInsideCityLimits"] == "true" || value["placeOfDeathInsideCityLimits"] == "True");
                placeOfDeathAddress.Extension.Add(insideCityLimits);
                placeOfDeathAddressExt.Value = placeOfDeathAddress;
                placeOfDeathExt.Extension.Add(placeOfDeathAddressExt);

                Patient.Extension.Add(placeOfDeathExt);
            }
        }


        /////////////////////////////////////////////////////////////////////////////////
        //
        // Class Properties related to the Certifier (Practitioner).
        //
        /////////////////////////////////////////////////////////////////////////////////

        /// <summary>Given name(s) of certifier.</summary>
        /// <value>the certifier's name (first, middle, etc.)</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>string[] names = {"Doctor", "Middle"};</para>
        /// <para>ExampleDeathRecord.CertifierGivenNames = names;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Certifier Given Name(s): {string.Join(", ", ExampleDeathRecord.CertifierGivenNames)}");</para>
        /// </example>
        public string[] CertifierGivenNames
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

        /// <summary>Family name of certifier.</summary>
        /// <value>the certifier's family name (i.e. last name)</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.CertifierFamilyName = "Last";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Certifier's Last Name: {string.Join(", ", ExampleDeathRecord.CertifierFamilyName)}");</para>
        /// </example>
        public string CertifierFamilyName
        {
            get
            {
                return GetFirstString("Bundle.entry.resource.where($this is Practitioner).name[0].family");
            }
            set
            {
                HumanName name = Practitioner.Name.FirstOrDefault(); // Check if there is already a HumanName on the Certifier.
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
                // TODO
            }
        }


        /////////////////////////////////////////////////////////////////////////////////
        //
        // Class Properties related to the Cause of Death (Conditions).
        //
        /////////////////////////////////////////////////////////////////////////////////

        /// <summary>Conditions that resulted in the underlying cause of death. Corresponds to part 1 of item 32 of the U.S.
        /// Standard Certificate of Death.</summary>
        /// <value>Conditions that resulted in the underlying cause of death. An array of tuples (in the order they would
        /// appear on a death certificate, from top to bottom), each containing the cause of death literal (Tuple "Item1") the
        /// approximate onset to death (Tuple "Item2"), and an (optional) Dictionary representing a code, containing the following key/value pairs:
        /// <para>"code" - the code describing this finding</para>
        /// <para>"system" - the system the given code belongs to</para>
        /// <para>"display" - the human readable display text that corresponds to the given code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Tuple&lt;string, string, Dictionary&lt;string, string&gt;&gt;[] causes =</para>
        /// <para>{</para>
        /// <para>    Tuple.Create("Example Immediate COD", "minutes", new Dictionary<string, string>(){ {"code", "1234"}, {"system", "example"} }),</para>
        /// <para>    Tuple.Create("Example Underlying COD 1", "2 hours", new Dictionary<string, string>()),</para>
        /// <para>    Tuple.Create("Example Underlying COD 2", "6 months", new Dictionary<string, string>()),</para>
        /// <para>    Tuple.Create("Example Underlying COD 3", "15 years", new Dictionary<string, string>())</para>
        /// <para>};</para>
        /// <para>ExampleDeathRecord.CausesOfDeath = causes;</para>
        /// <para>// Getter:</para>
        /// <para>Tuple&lt;string, string&gt;[] causes = ExampleDeathRecord.CausesOfDeath;</para>
        /// <para>foreach (var cause in causes)</para>
        /// <para>{</para>
        /// <para>    Console.WriteLine($"Cause: {cause.Item1}, Onset: {cause.Item2}, Code: {cause.Item3}");</para>
        /// <para>}</para>
        /// </example>
        public Tuple<string, string, Dictionary<string, string>>[] CausesOfDeath
        {
            /// <returns>an array of tuples each containing the cause of death literal and the approximate interval onset to death.</returns>
            get
            {
                List<Tuple<string, string, Dictionary<string, string>>> results = new List<Tuple<string, string, Dictionary<string, string>>>();
                Regex htmlRegex = new Regex("<.*?>");

                // Grab the cause of death conditions on the record
                var causes = Bundle.Entry.Where( entry => entry.Resource.ResourceType == ResourceType.Condition && ((Condition)entry.Resource).Onset != null).ToList();
                foreach (var cause in causes)
                {
                    // Grab literal and onset
                    string literal = htmlRegex.Replace(Convert.ToString(((Condition)cause.Resource).Text.Div), "");
                    string onset = Convert.ToString(((Condition)cause.Resource).Onset);

                    // Grab code (if it is there)
                    CodeableConcept codeableConcept = ((Condition)cause.Resource).Code;
                    Dictionary<string, string> code = new Dictionary<string, string>();
                    if (codeableConcept != null)
                    {
                        Coding coding = codeableConcept.Coding.First();
                        if (coding != null)
                        {
                            code.Add("code", codeableConcept.Coding.First().Code);
                            code.Add("system", codeableConcept.Coding.First().System);
                            code.Add("display", codeableConcept.Coding.First().Display);
                        }
                    }
                    results.Add(Tuple.Create(literal, onset, code));
                }

                return results.ToArray();
            }
            set
            {
                // Remove all existing Causes
                RemoveCauseConditions();

                // Add new Conditions for causes
                foreach (Tuple<string, string, Dictionary<string, string>> cause in value)
                {
                    // Create a new Condition and populate it with the given details
                    Condition condition = new Condition();
                    condition.Id = "urn:uuid:" + Guid.NewGuid().ToString();
                    condition.Subject = new ResourceReference(Patient.Id);
                    condition.Meta = new Meta();
                    string[] condition_profile = {"http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-causeOfDeath-CauseOfDeathCondition"};
                    condition.Meta.Profile = condition_profile;
                    condition.ClinicalStatus = Condition.ConditionClinicalStatusCodes.Active;
                    Narrative narrative = new Narrative();
                    narrative.Div = $"<div xmlns='http://www.w3.org/1999/xhtml'>{cause.Item1}</div>";
                    narrative.Status = Narrative.NarrativeStatus.Additional;
                    condition.Text = narrative;
                    condition.Onset = new FhirString(cause.Item2);
                    condition.Code = DictToCodeableConcept(cause.Item3); // Optional, literal might not be coded yet.

                    // Add Condition to Composition and Bundle
                    AddReferenceToComposition(condition.Id);
                    Bundle.AddResourceEntry(condition, condition.Id);
                }
            }
        }

        /// <summary>A significant condition that contributed to death but did not result in the underlying cause
        /// captured by a CauseOfDeathCondition. Corresponds to part 2 of item 32 of the U.S. Standard Certificate of Death.</summary>
        /// <value>A significant condition that contributed to death but did not result in the underlying cause
        /// captured by a CauseOfDeathCondition.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.ContributingConditions = "Example Contributing Condition";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Cause: {ExampleDeathRecord.ContributingConditions}");</para>
        /// </example>
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
                // Create a new Condition and populate it with the given details
                Condition condition = new Condition();
                condition.Id = "urn:uuid:" + Guid.NewGuid().ToString();
                condition.Subject = new ResourceReference(Patient.Id);
                condition.Meta = new Meta();
                string[] condition_profile = {"http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-causeOfDeath-ContributedToDeathCondition"};
                condition.Meta.Profile = condition_profile;
                condition.ClinicalStatus = Condition.ConditionClinicalStatusCodes.Active;
                Narrative narrative = new Narrative();
                narrative.Div = $"<div xmlns='http://www.w3.org/1999/xhtml'>{value}</div>";
                narrative.Status = Narrative.NarrativeStatus.Additional;
                condition.Text = narrative;

                // Add Condition to Composition and Bundle
                AddReferenceToComposition(condition.Id);
                Bundle.AddResourceEntry(condition, condition.Id);
            }
        }


        /////////////////////////////////////////////////////////////////////////////////
        //
        // Class Properties related to other record information (Observations).
        //
        /////////////////////////////////////////////////////////////////////////////////

        /// <summary>Whether an autopsy was performed (true) or not (false). Corresponds to item 33 of the U.S. Standard
        /// Certificate of Death.</summary>
        /// <value>Whether an autopsy was performed (true) or not (false)</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.AutopsyPerformed = true;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Was autopsy was performed?: {ExampleDeathRecord.AutopsyPerformed}");</para>
        /// </example>
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
        /// <value>Were autopsy findings available to complete the cause of death?</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.AutopsyResultsAvailable = true;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Were autopsy findings available to complete the cause of death?: {ExampleDeathRecord.AutopsyResultsAvailable}");</para>
        /// </example>
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
        /// <value>The manner of the decendents demise. A Dictionary representing a code, containing the following key/value pairs:
        /// <para>"code" - the code describing this finding</para>
        /// <para>"system" - the system the given code belongs to</para>
        /// <para>"display" - the human readable display text that corresponds to the given code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; code = new Dictionary&lt;string, string&gt;();</para>
        /// <para>code.Add("code", "7878000");</para>
        /// <para>code.Add("system", "http://github.com/nightingaleproject/fhirDeathRecord/sdr/causeOfDeath/vs/MannerOfDeathVS");</para>
        /// <para>code.Add("display", "Accident");</para>
        /// <para>ExampleDeathRecord.MannerOfDeath = code;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Manner of the decendents demise: {ExampleDeathRecord.MannerOfDeath["display"]}");</para>
        /// </example>
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
                observation.Value = DictToCodeableConcept(value);
            }
        }

        /// <summary>Did tobacco use contribute to death. Corresponds to item 35 of the U.S. Standard Certificate of Death.</summary>
        /// <value>Did tobacco use contribute to death. A Dictionary representing a code, containing the following key/value pairs:
        /// <para>"code" - the code describing this finding</para>
        /// <para>"system" - the system the given code belongs to</para>
        /// <para>"display" - the human readable display text that corresponds to the given code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; code = new Dictionary&lt;string, string&gt;();</para>
        /// <para>code.Add("code", "373066001");</para>
        /// <para>code.Add("system", "http://github.com/nightingaleproject/fhirDeathRecord/sdr/causeOfDeath/vs/ContributoryTobaccoUseVS");</para>
        /// <para>code.Add("display", "Yes");</para>
        /// <para>ExampleDeathRecord.TobaccoUseContributedToDeath = code;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Did tobacco use contribute to death: {ExampleDeathRecord.TobaccoUseContributedToDeath["display"]}");</para>
        /// </example>
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
                observation.Value = DictToCodeableConcept(value);
            }
        }

        /// <summary>Actual or presumed date of death. Corresponds to item 29 of the U.S. Standard Certificate of Death.</summary>
        /// <value>Actual or presumed date of death</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.ActualOrPresumedDateOfDeath = "2018-04-24T00:00:00+00:00";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Actual or presumed date of death: {ExampleDeathRecord.ActualOrPresumedDateOfDeath}");</para>
        /// </example>
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
        /// <value>Date pronounced dead</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.DatePronouncedDead = "2018-04-24T00:00:00+00:00";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Date pronounced dead: {ExampleDeathRecord.DatePronouncedDead}");</para>
        /// </example>
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
        /// <value>Did death result from injury at work?</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.DeathFromWorkInjury = true;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Did death result from injury at work?: {ExampleDeathRecord.DeathFromWorkInjury}");</para>
        /// </example>
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
        /// <value>Injury leading to death associated with transportation event. A Dictionary representing a code, containing the following key/value pairs:
        /// <para>"code" - the code describing this finding</para>
        /// <para>"system" - the system the given code belongs to</para>
        /// <para>"display" - the human readable display text that corresponds to the given code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; code = new Dictionary&lt;string, string&gt;();</para>
        /// <para>code.Add("code", "236320001");</para>
        /// <para>code.Add("system", "http://github.com/nightingaleproject/fhirDeathRecord/sdr/causeOfDeath/vs/TransportRelationshipsVS");</para>
        /// <para>code.Add("display", "Vehicle driver");</para>
        /// <para>ExampleDeathRecord.DeathFromTransportInjury = code;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Injury leading to death associated with transportation event: {ExampleDeathRecord.DeathFromTransportInjury["display"]}");</para>
        /// </example>
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
                observation.Value = DictToCodeableConcept(value);
            }
        }

        /// <summary>Whether a medical examiner or coroner was contacted. Corresponds to item 31 of the U.S. Standard Certificate of Death.</summary>
        /// <value>Whether a medical examiner or coroner was contacted</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.MedicalExaminerContacted = true;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Medical examiner or coroner was contacted?: {ExampleDeathRecord.MedicalExaminerContacted}");</para>
        /// </example>
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
        /// <value>Timing Of Recent Pregnancy In Relation To Death. A Dictionary representing a code, containing the following key/value pairs:
        /// <para>"code" - the code describing this finding</para>
        /// <para>"system" - the system the given code belongs to</para>
        /// <para>"display" - the human readable display text that corresponds to the given code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; code = new Dictionary&lt;string, string&gt;();</para>
        /// <para>code.Add("code", "PHC1260");</para>
        /// <para>code.Add("system", "http://github.com/nightingaleproject/fhirDeathRecord/sdr/causeOfDeath/vs/PregnancyStatusVS");</para>
        /// <para>code.Add("display", "Not pregnant within past year");</para>
        /// <para>ExampleDeathRecord.TimingOfRecentPregnancyInRelationToDeath = code;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Timing Of Recent Pregnancy In Relation To Death: {ExampleDeathRecord.TimingOfRecentPregnancyInRelationToDeath["display"]}");</para>
        /// </example>
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
                observation.Value = DictToCodeableConcept(value);
            }
        }

        /// <summary>Injury incident description.</summary>
        /// <value>Injury incident description. A Dictionary representing a description of an injury incident, containing the following key/value pairs:
        /// <para>"placeOfInjuryDescription" - description of the place of injury, e.g. decedent’s home, restaurant, wooded area</para>
        /// <para>"effectiveDateTime" - effective date and time of injury</para>
        /// <para>"description" - description of injury</para>
        /// <para>"placeOfInjuryLine1" - location of injury, line one</para>
        /// <para>"placeOfInjuryLine2" - location of injury, line two</para>
        /// <para>"placeOfInjuryCity" - location of injury, city</para>
        /// <para>"placeOfInjuryState" - location of injury, state</para>
        /// <para>"placeOfInjuryZip" - location of injury, zip</para>
        /// <para>"placeOfInjuryCountry" - location of injury, country</para>
        /// <para>"placeOfInjuryInsideCityLimits" - location of injury, whether the address is within city limits (true) or not (false)</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; detailsOfInjury = new Dictionary&lt;string, string&gt;();</para>
        /// <para>detailsOfInjury.Add("placeOfInjuryDescription", "Home");</para>
        /// <para>detailsOfInjury.Add("effectiveDateTime", "2018-04-19T15:43:00+00:00");</para>
        /// <para>detailsOfInjury.Add("description", "Example details of injury");</para>
        /// <para>detailsOfInjury.Add("placeOfInjuryLine1", "7 Example Street");</para>
        /// <para>detailsOfInjury.Add("placeOfInjuryLine2", "Unit 1234");</para>
        /// <para>detailsOfInjury.Add("placeOfInjuryCity", "Bedford");</para>
        /// <para>detailsOfInjury.Add("placeOfInjuryState", "Massachusetts");</para>
        /// <para>detailsOfInjury.Add("placeOfInjuryZip", "01730");</para>
        /// <para>detailsOfInjury.Add("placeOfInjuryCountry", "United States");</para>
        /// <para>detailsOfInjury.Add("placeOfInjuryInsideCityLimits", "true");</para>
        /// <para>ExampleDeathRecord.DetailsOfInjury = detailsOfInjury;</para>
        /// <para>// Getter:</para>
        /// <para>string state = ExampleDeathRecord.DetailsOfInjury["placeOfInjuryState"];</para>
        /// <para>Console.WriteLine($"State where injury occurred: {state}");</para>
        /// </example>
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
                placeOfInjuryLocationAddress.Type = Hl7.Fhir.Model.Address.AddressType.Postal;
                Extension insideCityLimits = new Extension();
                insideCityLimits.Url = "http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/shr-core-InsideCityLimits-extension";
                insideCityLimits.Value = new FhirBoolean(value["placeOfInjuryInsideCityLimits"] == "true" || value["placeOfInjuryInsideCityLimits"] == "True");
                placeOfInjuryLocationAddress.Extension.Add(insideCityLimits);
                placeOfInjuryLocation.Value = placeOfInjuryLocationAddress;
                observation.Extension.Add(placeOfInjuryLocation);
            }
        }


        /////////////////////////////////////////////////////////////////////////////////
        //
        // Class helper methods useful for building, searching through records.
        //
        /////////////////////////////////////////////////////////////////////////////////

        /// <summary>Add a new (or replaces a) observation on the Death Record.</summary>
        /// <param name="profile">the observation profile.</param>
        /// <param name="code">the observation code.</param>
        /// <param name="system">the observation code system.</param>
        /// <param name="display">the observation code display.</param>
        private Observation AddObservation(string profile, string code, string system, string display)
        {
            // If this type of Observation already exists, remove it.
            RemoveObservation(code);

            // Create a new Observation, add references to it where approriate, and return it
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

        /// <summary>Remove a reference from the Death Record Composition.</summary>
        /// <param name="path">a reference.</param>
        private bool RemoveReferenceFromComposition(string reference)
        {
            return Composition.Section.First().Entry.RemoveAll(entry => entry.Reference == reference) > 0;
        }

        /// <summary>Remove an observation entry from the bundle, and remove the reference to it in the Composition.</summary>
        /// <param name="code">the code that describes the type of observation.</param>
        private void RemoveObservation(string code)
        {
            // Below RemoveAll predicate logic (makes use of short-circuit evaluation):
            // 1. Continue if an Observation
            // 2. Continue if Observation is of the correct type (code matches)
            // 3. At this point, we have the Observation we want to get rid of, so:
            //    Remove the reference to it in the composition (will return true if something was removed)
            // If all 3 points above were true, remove the entry
            Bundle.Entry.RemoveAll(entry => entry.Resource.ResourceType == ResourceType.Observation &&
                                            ((Observation)entry.Resource).Code.Coding.First().Code == code &&
                                            RemoveReferenceFromComposition(entry.Resource.Id));
        }

        /// <summary>Remove Cause of Death Condition entries from the bundle, and remove their references in the Composition.</summary>
        private void RemoveCauseConditions()
        {
            // Below RemoveAll predicate logic (makes use of short-circuit evaluation):
            // 1. Continue if a Condition
            // 2. Continue if Condition has an onset (not a contributing cause)
            // 3. At this point, we have a cause Condition, which we want to get rid of, so:
            //    Remove the reference to it in the composition (will return true if something was removed)
            // If all 3 points above were true, remove the entry
            Bundle.Entry.RemoveAll(entry => entry.Resource.ResourceType == ResourceType.Condition &&
                                            ((Condition)entry.Resource).Onset != null &&
                                            RemoveReferenceFromComposition(entry.Resource.Id));
        }

        /// <summary>Convert a "code" dictionary to a FHIR CodableConcept.</summary>
        /// <param name="dict">represents a code.</param>
        /// <returns>the corresponding CodeableConcept representation of the code.</returns>
        private CodeableConcept DictToCodeableConcept(Dictionary<string, string> dict)
        {
            CodeableConcept codeableConcept = new CodeableConcept();
            Coding coding = new Coding();
            if (dict.ContainsKey("code"))
            {
                coding.Code = dict["code"];
            }
            if (dict.ContainsKey("system"))
            {
                coding.System = dict["system"];
            }
            if (dict.ContainsKey("display"))
            {
                coding.Display = dict["display"];
            }
            codeableConcept.Coding.Add(coding);
            return codeableConcept;
        }

        /// <summary>Given a FHIR path, return the elements that match the given path;
        /// returns an empty array if no matches are found.</summary>
        /// <param name="path">represents a FHIR path.</param>
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
        /// <param name="path">represents a FHIR path.</param>
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
                return null; // Nothing found
            }
        }

        /// <summary>Given a FHIR path, return the last element that matches the given path.</summary>
        /// <param name="path">represents a FHIR path.</param>
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
                return null; // Nothing found
            }
        }

        /// <summary>Given a FHIR path, return the elements that match the given path as a string;
        /// returns an empty array if no matches are found.</summary>
        /// <param name="path">represents a FHIR path.</param>
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
        /// <param name="path">represents a FHIR path.</param>
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
                return null; // Nothing found
            }
        }

        /// <summary>Given a FHIR path, return the last element that matches the given path as a string;
        /// returns an empty string if no match is found.</summary>
        /// <param name="path">represents a FHIR path.</param>
        /// <returns>the last element that matches the given path as a string, or null if no match is found.</returns>
        private string GetLastString(string path)
        {
            var last = GetLast(path);
            if (last != null)
            {
                return Convert.ToString(last);
            }
            else
            {
                return null; // Nothing found
            }
        }
    }

}
