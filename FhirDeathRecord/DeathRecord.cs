using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Web;
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
        private ITypedElement Navigator;

        /// <summary>Bundle that contains the death record.</summary>
        private Bundle Bundle;

        /// <summary>Composition that described what the Bundle is, as well as keeping references to its contents.</summary>
        private Composition Composition;

        /// <summary>The decedent.</summary>
        private Patient Patient;

        /// <summary>The Certifier.</summary>
        private Practitioner Practitioner;

        /// <summary>Mortality data for code translations.</summary>
        private MortalityData MortalityData = MortalityData.Instance;

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
            string[] composition_profile = { "http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-deathRecord-DeathRecordContents" };
            Composition.Meta.Profile = composition_profile;
            Composition.Type = new CodeableConcept("http://loinc.org", "64297-5");
            Composition.Title = "Record of Death";
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
            Navigator = Bundle.ToTypedElement();
        }

        /// <summary>Constructor that takes a string that represents a FHIR SDR in either XML or JSON format.</summary>
        /// <param name="record">represents a FHIR SDR in either XML or JSON format.</param>
        /// <param name="permissive">if the parser should be permissive when parsing the given string</param>
        /// <exception cref="ArgumentException">Record is neither valid XML nor JSON.</exception>
        public DeathRecord(string record, bool permissive = false)
        {
            // Check if XML
            if (!string.IsNullOrEmpty(record) && record.TrimStart().StartsWith("<"))
            {
                FhirXmlParser parser = new FhirXmlParser(new ParserSettings { AcceptUnknownMembers = permissive, AllowUnrecognizedEnums = permissive });
                Bundle = parser.Parse<Bundle>(record);
                Navigator = Bundle.ToTypedElement();
            }
            else
            {
                // Assume JSON
                FhirJsonParser parser = new FhirJsonParser(new ParserSettings { AcceptUnknownMembers = permissive, AllowUnrecognizedEnums = permissive });
                Bundle = parser.Parse<Bundle>(record);
                // Need to un-escape "div"s in Causes!
                UnescapeCauses(Bundle);
                Navigator = Bundle.ToTypedElement();
            }

            // Grab references to Patient and Practitioner for class instance
            if (Navigator != null)
            {
                var patientEntry = Bundle.Entry.FirstOrDefault( entry => entry.Resource.ResourceType == ResourceType.Patient );
                Patient = (Patient)patientEntry.Resource;
                var practitionerEntry = Bundle.Entry.FirstOrDefault( entry => entry.Resource.ResourceType == ResourceType.Practitioner );
                Practitioner = (Practitioner)practitionerEntry.Resource;
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
            // Need to escape "div"s in Causes!
            // IMPROVEMENT: Look into more efficient ways of doing this.
            FhirXmlParser parser = new FhirXmlParser();
            Bundle clonedBundle = parser.Parse<Bundle>(ToXML());
            EscapeCauses(clonedBundle);
            return serializer.SerializeToString(clonedBundle);
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

        /// <summary>Death Record Registration Date.</summary>
        /// <value>when the record was registered</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.DateOfRegistration = "2018-07-11";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Record was registered on: {ExampleDeathRecord.DateOfRegistration}");</para>
        /// </example>
        public string DateOfRegistration
        {
            get
            {
                return GetFirstString("Bundle.entry.resource.where($this is Composition).date");
            }
            set
            {
                Composition.Date = value.Trim();
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
                return GetAllString("Bundle.entry.resource.where($this is Patient).name.where(use='official').given");
            }
            set
            {
                HumanName name = Patient.Name.SingleOrDefault(n => n.Use == HumanName.NameUse.Official);
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
                return GetFirstString("Bundle.entry.resource.where($this is Patient).name.where(use='official').family");
            }
            set
            {
                HumanName name = Patient.Name.SingleOrDefault(n => n.Use == HumanName.NameUse.Official);
                if (name != null && !String.IsNullOrEmpty(value))
                {
                    name.Family = value;
                }
                else if (!String.IsNullOrEmpty(value))
                {
                    name = new HumanName();
                    name.Use = HumanName.NameUse.Official;
                    name.Family = value;
                    Patient.Name.Add(name);
                }
            }
        }

        /// <summary>Decedent's Maiden Name.</summary>
        /// <value>the decedent's maiden name (i.e. last name before marriage)</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.MaidenName = "Last";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Decedent's Maiden Name: {ExampleDeathRecord.MaidenName}");</para>
        /// </example>
        public string MaidenName
        {
            get
            {
                return GetFirstString("Bundle.entry.resource.where($this is Patient).name.where(use='maiden').family");
            }
            set
            {
                HumanName name = Patient.Name.SingleOrDefault(n => n.Use == HumanName.NameUse.Maiden);
                if (name != null && !String.IsNullOrEmpty(value))
                {
                    name.Family = value;
                }
                else if (!String.IsNullOrEmpty(value))
                {
                    name = new HumanName();
                    name.Use = HumanName.NameUse.Maiden;
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
                HumanName name = Patient.Name.SingleOrDefault(n => n.Use == HumanName.NameUse.Official);
                if (name != null && !String.IsNullOrEmpty(value))
                {
                    string[] suffix = { value };
                    name.Suffix = suffix;
                }
                else if (!String.IsNullOrEmpty(value))
                {
                    name = new HumanName();
                    name.Use = HumanName.NameUse.Official;
                    string[] suffix = { value };
                    name.Suffix = suffix;
                    Patient.Name.Add(name);
                }
            }
        }

        /// <summary>Decedent's Father's Family Name.</summary>
        /// <value>the decedent's father's family name (i.e. last name)</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.FatherFamilyName = "Last";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Decedent's Father's Last Name: {ExampleDeathRecord.FatherFamilyName}");</para>
        /// </example>
        public string FatherFamilyName
        {
            get
            {
                return GetFirstString("Bundle.entry.resource.where($this is Patient).contact.where(relationship.coding.code='FTH').name.family");
            }
            set
            {
                Patient.ContactComponent contact = Patient.Contact.SingleOrDefault(c => c.Relationship.First().Coding.First().Code == "FTH");
                if (contact != null && !String.IsNullOrEmpty(value))
                {
                    HumanName name = contact.Name;
                    if (name != null)
                    {
                        name.Family = value;
                    }
                }
                else if (!String.IsNullOrEmpty(value))
                {
                    contact = new Patient.ContactComponent();
                    CodeableConcept relation = new CodeableConcept();
                    Coding code = new Coding();
                    code.System = "http://hl7.org/fhir/v3/RoleCode";
                    code.Code = "FTH";
                    relation.Coding.Add(code);
                    contact.Relationship.Add(relation);
                    HumanName name = new HumanName();
                    name.Family = value;
                    contact.Name = name;
                    Patient.Contact.Add(contact);
                }
            }
        }

        /// <summary>Decedent's Gender.</summary>
        /// <value>the decedent's gender</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.Gender = "female";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Decedent Suffix: {ExampleDeathRecord.Gender}");</para>
        /// </example>
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
                    case "Male":
                    case "m":
                    case "M":
                        Patient.Gender = AdministrativeGender.Male;
                        break;
                    case "female":
                    case "Female":
                    case "f":
                    case "F":
                        Patient.Gender = AdministrativeGender.Female;
                        break;
                    case "other":
                    case "Other":
                    case "o":
                    case "O":
                        Patient.Gender = AdministrativeGender.Other;
                        break;
                    case "unknown":
                    case "Unknown":
                    case "u":
                    case "U":
                        Patient.Gender = AdministrativeGender.Unknown;
                        break;
                }
            }
        }

        /// <summary>Decedent's Birth Sex.</summary>
        /// <value>the decedent's birth sex</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; code = new Dictionary&lt;string, string&gt;();</para>
        /// <para>code.Add("code", "M");</para>
        /// <para>code.Add("system", "http://hl7.org/fhir/us/core/ValueSet/us-core-birthsex");</para>
        /// <para>code.Add("display", "Male");</para>
        /// <para>ExampleDeathRecord.BirthSex = code;</para>
        /// <para>// Getter:</para>
        /// <para>foreach(var pair in ExampleDeathRecord.BirthSex)</para>
        /// <para>{</para>
        /// <para>      Console.WriteLine($"\tAddress key: {pair.Key}: value: {pair.Value}");</para>
        /// <para>};</para>
        /// </example>
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
                Patient.Extension.RemoveAll(ext => ext.Url == "http://hl7.org/fhir/us/core/StructureDefinition/us-core-birthsex");
                Extension birthsex = new Extension();
                birthsex.Url = "http://hl7.org/fhir/us/core/StructureDefinition/us-core-birthsex";
                birthsex.Value = DictToCodeableConcept(value);
                Patient.Extension.Add(birthsex);
            }
        }

        /// <summary>Decedent's Date of Birth.</summary>
        /// <value>the decedent's date of birth</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.DateOfBirth = "1970-04-24";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Decedent Suffix: {ExampleDeathRecord.DateOfBirth}");</para>
        /// </example>

        public string DateOfBirth
        {
            get
            {
                return GetFirstString("Bundle.entry.resource.where($this is Patient).birthDate");
            }
            set
            {
                Patient.BirthDate = value.Trim();
            }
        }

        /// <summary>Decedent's Date and Time of Death.</summary>
        /// <value>the decedent's date and time of death</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.DateOfDeath = "1970-04-24";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Decedent Suffix: {ExampleDeathRecord.DateOfDeath}");</para>
        /// </example>
        public string DateOfDeath
         {
            get
            {
                return GetFirstString("Bundle.entry.resource.where($this is Patient).deceased");
            }
            set
            {
                Patient.Deceased = new FhirDateTime(value.Trim());
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
        /// <para>"residenceInsideCityLimits" - residence, inside city limits</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; residence = new Dictionary&lt;string, string&gt;();</para>
        /// <para>residence.Add("residenceLine1", "9 Example Street");</para>
        /// <para>residence.Add("residenceLine2", "Line 2");</para>
        /// <para>residence.Add("residenceCity", "Bedford");</para>
        /// <para>residence.Add("residenceCounty", "Middlesex");</para>
        /// <para>residence.Add("residenceState", "Massachusetts");</para>
        /// <para>residence.Add("residenceZip", "01730");</para>
        /// <para>residence.Add("residenceCountry", "United States");</para>
        /// <para>residence.Add("residenceInsideCityLimits", "True");</para>
        /// <para>SetterDeathRecord.Residence = residence;</para>
        /// <para>// Getter:</para>
        /// <para>string state = ExampleDeathRecord.Residence["residenceState"];</para>
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
                dictionary.Add("residenceInsideCityLimits", GetFirstString("Bundle.entry.resource.where($this is Patient).address.extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/shr-core-InsideCityLimits-extension').value"));

                return dictionary;
            }
            set
            {
                Address address = new Address();
                address.LineElement.Add(new FhirString(GetValue(value, "residenceLine1")));
                address.LineElement.Add(new FhirString(GetValue(value, "residenceLine2")));
                address.City = GetValue(value, "residenceCity");
                address.District = GetValue(value, "residenceCounty");
                address.State = GetValue(value, "residenceState");
                address.PostalCodeElement = new FhirString(GetValue(value, "residenceZip"));
                address.Country = GetValue(value, "residenceCountry");
                if (value.ContainsKey("residenceInsideCityLimits") && GetValue(value, "residenceInsideCityLimits") != null)
                {
                    Extension insideCityLimits = new Extension();
                    insideCityLimits.Url = "http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/shr-core-InsideCityLimits-extension";
                    insideCityLimits.Value = new FhirBoolean(GetValue(value, "residenceInsideCityLimits") == "true" || GetValue(value, "residenceInsideCityLimits") == "True");
                    address.Extension.Add(insideCityLimits);
                }
                Patient.Address.Clear();
                Patient.Address.Add(address);
            }
        }

        /// <summary>Decedent's Social Security Number.</summary>
        /// <value>the decedent's social security number</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.SSN = "123-45-678";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Decedent Suffix: {ExampleDeathRecord.SSN}");</para>
        /// </example>
        public string SSN
        {
            get
            {
                return GetFirstString("Bundle.entry.resource.where($this is Patient).identifier.where(system = 'http://hl7.org/fhir/sid/us-ssn').value");
            }
            set
            {
                Patient.Identifier.RemoveAll(iden => iden.System == "http://hl7.org/fhir/sid/us-ssn");
                Identifier ssn = new Identifier();
                ssn.System = "http://hl7.org/fhir/sid/us-ssn";
                ssn.Value = value;
                Patient.Identifier.Add(ssn);
            }
        }

        /// <summary>Decedent's Ethnicity.</summary>
        /// <value>the decedent's ethnicity</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Tuple&lt;string, string&gt;[] ethnicity = { Tuple.Create("Non Hispanic or Latino", "2186-5"), Tuple.Create("Salvadoran", "2161-8") };</para>
        /// <para>ExampleDeathRecord.Ethnicity = ethnicity;</para>
        /// <para>// Getter:</para>
        /// <para>foreach(var pair in deathRecord.Ethnicity)</para>
        /// <para>{</para>
        /// <para>      Console.WriteLine($"\tEthnicity text: {pair.Key}: code: {pair.Value}");</para>
        /// <para>};</para>
        /// </example>
        public Tuple<string, string>[] Ethnicity
        {
            get
            {
                string[] ombCodes = new string[] { };
                string[] detailedCodes = new string[] { };
                ombCodes = GetAllString("Bundle.entry.resource.where($this is Patient).extension.where(url = 'http://hl7.org/fhir/us/core/StructureDefinition/us-core-ethnicity').extension.where(url = 'ombCategory').value.code");
                detailedCodes = GetAllString("Bundle.entry.resource.where($this is Patient).extension.where(url = 'http://hl7.org/fhir/us/core/StructureDefinition/us-core-ethnicity').extension.where(url = 'detailed').value.code");
                Tuple<string, string>[] ethnicityList = new Tuple<string, string>[ombCodes.Length + detailedCodes.Length];
                for (int i = 0; i < ombCodes.Length; i++)
                {
                    ethnicityList[i] = (Tuple.Create(MortalityData.EthnicityCodeToEthnicityName(ombCodes[i]), ombCodes[i]));
                }
                for (int i = 0; i < detailedCodes.Length; i++)
                {
                    ethnicityList[ombCodes.Length + i] = (Tuple.Create(MortalityData.EthnicityCodeToEthnicityName(detailedCodes[i]), detailedCodes[i]));
                }
                return ethnicityList;
            }
            set
            {
                Patient.Extension.RemoveAll(ext => ext.Url == "http://hl7.org/fhir/us/core/StructureDefinition/us-core-ethnicity");
                Extension ethnicities = new Extension();
                ethnicities.Url = "http://hl7.org/fhir/us/core/StructureDefinition/us-core-ethnicity";
                foreach (Tuple<string, string> element in value)
                {
                    string display = element.Item1;
                    string code = element.Item2;
                    Extension textEthnicity = new Extension();
                    Extension codeEthnicity = new Extension();
                    textEthnicity.Url = "text";
                    textEthnicity.Value = new FhirString(display);
                    if (code == "2135-2" || code == "2186-5")
                    {
                        codeEthnicity.Url = "ombCategory";
                    }
                    else
                    {
                        codeEthnicity.Url = "detailed";
                    }
                    codeEthnicity.Value = new Coding("urn:oid:2.16.840.1.113883.6.238", code, display);
                    ethnicities.Extension.Add(textEthnicity);
                    ethnicities.Extension.Add(codeEthnicity);
                }
                Patient.Extension.Add(ethnicities);
            }
        }

        /// <summary>Decedent's Race.</summary>
        /// <value>the decedent's race</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Tuple&lt;string, string&gt;[] race = { Tuple.Create("Non Hispanic or Latino", "2186-5"), Tuple.Create("Salvadoran", "2161-8") };</para>
        /// <para>ExampleDeathRecord.Race = race;</para>
        /// <para>// Getter:</para>
        /// <para>foreach(var pair in ExampleDeathRecord.race)</para>
        /// <para>{</para>
        /// <para>      Console.WriteLine($"\Race text: {pair.Key}: code: {pair.Value}");</para>
        /// <para>};</para>
        /// </example>
        public Tuple<string, string>[] Race
        {
            get
            {
                string[] ombCodes = new string[] { };
                string[] detailedCodes = new string[] { };
                ombCodes = GetAllString("Bundle.entry.resource.where($this is Patient).extension.where(url = 'http://hl7.org/fhir/us/core/StructureDefinition/us-core-race').extension.where(url = 'ombCategory').value.code");
                detailedCodes = GetAllString("Bundle.entry.resource.where($this is Patient).extension.where(url = 'http://hl7.org/fhir/us/core/StructureDefinition/us-core-race').extension.where(url = 'detailed').value.code");
                Tuple<string, string>[] raceList = new Tuple<string, string>[ombCodes.Length + detailedCodes.Length];
                for (int i = 0; i < ombCodes.Length; i++)
                {
                    raceList[i] = (Tuple.Create(MortalityData.RaceCodeToRaceName(ombCodes[i]), ombCodes[i]));
                }
                for (int i = 0; i < detailedCodes.Length; i++)
                {
                    raceList[ombCodes.Length + i] = (Tuple.Create(MortalityData.RaceCodeToRaceName(detailedCodes[i]), detailedCodes[i]));
                }
                return raceList;
            }
            set
            {
                Patient.Extension.RemoveAll(ext => ext.Url == "http://hl7.org/fhir/us/core/StructureDefinition/us-core-race");
                Extension races = new Extension();
                races.Url = "http://hl7.org/fhir/us/core/StructureDefinition/us-core-race";
                foreach(Tuple<string,string> element in value)
                {
                    string display = element.Item1;
                    string code = element.Item2;
                    Extension textRace = new Extension();
                    Extension codeRace = new Extension();
                    textRace.Url = "text";
                    textRace.Value = new FhirString(display);
                    if (code == "1002-5" || code == "2028-9" || code == "2054-5" || code == "2076-8" || code == "2106-3")
                    {
                        codeRace.Url = "ombCategory";
                    }
                    else
                    {
                        codeRace.Url = "detailed";
                    }
                    codeRace.Value = new Coding("urn:oid:2.16.840.1.113883.6.238", code, display);
                    races.Extension.Add(textRace);
                    races.Extension.Add(codeRace);
                }
                Patient.Extension.Add(races);
            }
        }

        /// <summary>Decedent's Place Of Birth.</summary>
        /// <value>Decedent's Place Of Birth. A Dictionary representing a place of birth address, containing the following key/value pairs:
        /// <para>"placeOfBirthLine1" - location of birth, line one</para>
        /// <para>"placeOfBirthLine2" - location of birth, line two</para>
        /// <para>"placeOfBirthCity" - location of birth, city</para>
        /// <para>"placeOfBirthCounty" - location of birth, county</para>
        /// <para>"placeOfBirthState" - location of birth, state</para>
        /// <para>"placeOfBirthZip" - location of birth, zip</para>
        /// <para>"placeOfBirthCountry" - location of birth, country</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; placeOfBirth = new Dictionary&lt;string, string&gt;();</para>
        /// <para>placeOfBirth.Add("placeOfBirthLine1", "9 Example Street");</para>
        /// <para>placeOfBirth.Add("placeOfBirthLine2", "Line 2");</para>
        /// <para>placeOfBirth.Add("placeOfBirthCity", "Bedford");</para>
        /// <para>placeOfBirth.Add("placeOfBirthCounty", "Middlesex");</para>
        /// <para>placeOfBirth.Add("placeOfBirthState", "Massachusetts");</para>
        /// <para>placeOfBirth.Add("placeOfBirthZip", "01730");</para>
        /// <para>placeOfBirth.Add("placeOfBirthCountry", "United States");</para>
        /// <para>SetterDeathRecord.PlaceOfBirth = placeOfBirth;</para>
        /// <para>// Getter:</para>
        /// <para>string state = ExampleDeathRecord.PlaceOfBirth["placeOfBirthState"];</para>
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
                dictionary.Add("placeOfBirthCounty", GetFirstString("Bundle.entry.resource.where($this is Patient).extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-Birthplace-extension').value.district"));
                dictionary.Add("placeOfBirthState", GetFirstString("Bundle.entry.resource.where($this is Patient).extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-Birthplace-extension').value.state"));
                dictionary.Add("placeOfBirthZip", GetFirstString("Bundle.entry.resource.where($this is Patient).extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-Birthplace-extension').value.postalCode"));
                dictionary.Add("placeOfBirthCountry", GetFirstString("Bundle.entry.resource.where($this is Patient).extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-Birthplace-extension').value.country"));

                return dictionary;
            }
            set
            {
                // Place Of Birth extension
                Patient.Extension.RemoveAll(ext => ext.Url == "http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-Birthplace-extension");
                Extension placeOfBirthExt = new Extension();
                placeOfBirthExt.Url = "http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-Birthplace-extension";

                // Place Of Birth Address
                Address placeOfBirthAddress = new Address();
                string[] lines = {GetValue(value, "placeOfBirthLine1"), GetValue(value, "placeOfBirthLine2")};
                placeOfBirthAddress.Line = lines.ToArray();
                placeOfBirthAddress.City = GetValue(value, "placeOfBirthCity");
                placeOfBirthAddress.District = GetValue(value, "placeOfBirthCounty");
                placeOfBirthAddress.State = GetValue(value, "placeOfBirthState");
                placeOfBirthAddress.PostalCode = GetValue(value, "placeOfBirthZip");
                placeOfBirthAddress.Country = GetValue(value, "placeOfBirthCountry");
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
        /// <para>"placeOfDeathCounty" - location of death, county</para>
        /// <para>"placeOfDeathState" - location of death, state</para>
        /// <para>"placeOfDeathZip" - location of death, zip</para>
        /// <para>"placeOfDeathCountry" - location of death, country</para>
        /// <para>"placeOfDeathInsideCityLimits" - location of death, whether the address is within city limits (true) or not (false)</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; placeOfDeath = new Dictionary&lt;string, string&gt;();</para>
        /// <para>placeOfDeath.Add("placeOfDeathTypeCode", "16983000");</para>
        /// <para>placeOfDeath.Add("placeOfDeathTypeSystem", "http://snomed.info/sct");</para>
        /// <para>placeOfDeath.Add("placeOfDeathTypeDisplay", "Death in hospital");</para>
        /// <para>placeOfDeath.Add("placeOfDeathFacilityName", "Example Hospital");</para>
        /// <para>placeOfDeath.Add("placeOfDeathLine1", "8 Example Street");</para>
        /// <para>placeOfDeath.Add("placeOfDeathLine2", "Line 2");</para>
        /// <para>placeOfDeath.Add("placeOfDeathCity", "Bedford");</para>
        /// <para>placeOfDeath.Add("placeOfDeathCounty", "Middlesex");</para>
        /// <para>placeOfDeath.Add("placeOfDeathState", "Massachusetts");</para>
        /// <para>placeOfDeath.Add("placeOfDeathZip", "01730");</para>
        /// <para>placeOfDeath.Add("placeOfDeathCountry", "United States");</para>
        /// <para>placeOfDeath.Add("placeOfDeathInsideCityLimits", "True");</para>
        /// <para>SetterDeathRecord.PlaceOfDeath = placeOfDeath;</para>
        /// <para>// Getter:</para>
        /// <para>string state = ExampleDeathRecord.PlaceOfDeath["placeOfDeathState"];</para>
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
                dictionary.Add("placeOfDeathCounty", GetFirstString("Bundle.entry.resource.where($this is Patient).extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-PlaceOfDeath-extension').extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/shr-core-PostalAddress-extension').value.district"));
                dictionary.Add("placeOfDeathState", GetFirstString("Bundle.entry.resource.where($this is Patient).extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-PlaceOfDeath-extension').extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/shr-core-PostalAddress-extension').value.state"));
                dictionary.Add("placeOfDeathZip", GetFirstString("Bundle.entry.resource.where($this is Patient).extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-PlaceOfDeath-extension').extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/shr-core-PostalAddress-extension').value.postalCode"));
                dictionary.Add("placeOfDeathCountry", GetFirstString("Bundle.entry.resource.where($this is Patient).extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-PlaceOfDeath-extension').extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/shr-core-PostalAddress-extension').value.country"));
                dictionary.Add("placeOfDeathInsideCityLimits", GetFirstString("Bundle.entry.resource.where($this is Patient).extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-PlaceOfDeath-extension').extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/shr-core-PostalAddress-extension').value.extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/shr-core-InsideCityLimits-extension').value"));

                return dictionary;
            }
            set
            {
                // Place Of Death extension
                Patient.Extension.RemoveAll(ext => ext.Url == "http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-PlaceOfDeath-extension");
                Extension placeOfDeathExt = new Extension();
                placeOfDeathExt.Url = "http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-PlaceOfDeath-extension";

                // Place Of Death Type extension
                Extension placeOfDeathTypeExt = new Extension();
                placeOfDeathTypeExt.Url = "http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-PlaceOfDeathType-extension";
                CodeableConcept codeableConcept = new CodeableConcept();
                Coding coding = new Coding();
                coding.Code = GetValue(value, "placeOfDeathTypeCode");
                coding.System = GetValue(value, "placeOfDeathTypeSystem");
                coding.Display = GetValue(value, "placeOfDeathTypeDisplay");
                codeableConcept.Coding.Add(coding);
                placeOfDeathTypeExt.Value = codeableConcept;
                placeOfDeathExt.Extension.Add(placeOfDeathTypeExt);

                // Place Of Death Facility Name extension
                Extension placeOfDeathFacilityNameExt = new Extension();
                placeOfDeathFacilityNameExt.Url = "http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-FacilityName-extension";
                placeOfDeathFacilityNameExt.Value = new FhirString(GetValue(value, "placeOfDeathFacilityName"));
                placeOfDeathExt.Extension.Add(placeOfDeathFacilityNameExt);

                // Place Of Death Address extension
                Extension placeOfDeathAddressExt = new Extension();
                placeOfDeathAddressExt.Url = "http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/shr-core-PostalAddress-extension";
                Address placeOfDeathAddress = new Address();
                string[] lines = {GetValue(value, "placeOfDeathLine1"), GetValue(value, "placeOfDeathLine2")};
                placeOfDeathAddress.Line = lines.ToArray();
                placeOfDeathAddress.City = GetValue(value, "placeOfDeathCity");
                placeOfDeathAddress.District = GetValue(value, "placeOfDeathCounty");
                placeOfDeathAddress.State = GetValue(value, "placeOfDeathState");
                placeOfDeathAddress.PostalCode = GetValue(value, "placeOfDeathZip");
                placeOfDeathAddress.Country = GetValue(value, "placeOfDeathCountry");
                placeOfDeathAddress.Type = Hl7.Fhir.Model.Address.AddressType.Postal;
                if (value.ContainsKey("placeOfDeathInsideCityLimits") && GetValue(value, "placeOfDeathInsideCityLimits") != null)
                {
                    Extension insideCityLimits = new Extension();
                    insideCityLimits.Url = "http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/shr-core-InsideCityLimits-extension";
                    insideCityLimits.Value = new FhirBoolean(GetValue(value, "placeOfDeathInsideCityLimits") == "true" || GetValue(value, "placeOfDeathInsideCityLimits") == "True");
                    placeOfDeathAddress.Extension.Add(insideCityLimits);
                }
                placeOfDeathAddressExt.Value = placeOfDeathAddress;
                placeOfDeathExt.Extension.Add(placeOfDeathAddressExt);

                Patient.Extension.Add(placeOfDeathExt);
            }
        }

        /// <summary>The marital status of the decedent at the time of death. Corresponds to item 9 of the U.S. Standard Certificate of Death.</summary>
        /// <value>The marital status of the decedent at the time of death. A Dictionary representing a code, containing the following key/value pairs:
        /// <para>"code" - the code describing this finding</para>
        /// <para>"system" - the system the given code belongs to</para>
        /// <para>"display" - the human readable display text that corresponds to the given code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; code = new Dictionary&lt;string, string&gt;();</para>
        /// <para>code.Add("code", "S");</para>
        /// <para>code.Add("system", "http://hl7.org/fhir/v3/MaritalStatus");</para>
        /// <para>code.Add("display", "Never Married");</para>
        /// <para>ExampleDeathRecord.MaritalStatus = code;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Marital status: {ExampleDeathRecord.MaritalStatus["display"]}");</para>
        /// </example>
        public Dictionary<string, string> MaritalStatus
        {
            get
            {
                string code = GetFirstString("Bundle.entry.resource.where($this is Patient).extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-MaritalStatusAtDeath-extension').value.coding.code");
                string system = GetFirstString("Bundle.entry.resource.where($this is Patient).extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-MaritalStatusAtDeath-extension').value.coding.system");
                string display = GetFirstString("Bundle.entry.resource.where($this is Patient).extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-MaritalStatusAtDeath-extension').value.coding.display");
                Dictionary<string, string> dictionary = new Dictionary<string, string>();
                dictionary.Add("code", code);
                dictionary.Add("system", system);
                dictionary.Add("display", display);
                return dictionary;
            }
            set
            {
                Patient.Extension.RemoveAll(ext => ext.Url == "http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-MaritalStatusAtDeath-extension");
                Extension maritalStatus = new Extension();
                maritalStatus.Url = "http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-MaritalStatusAtDeath-extension";
                maritalStatus.Value = DictToCodeableConcept(value);
                Patient.Extension.Add(maritalStatus);
            }
        }

        /// <summary>Disposition of the decedent’s body. Corresponds to items 18, 19, 20, 21, 22 and 23 of the U.S. Standard Certificate of Death.</summary>
        /// <value>Disposition of the decedent’s body. A Dictionary representing the disposition, containing the following key/value pairs:
        /// <para>"dispositionTypeCode" - the code describing the method of disposition of the decedent’s remains</para>
        /// <para>"dispositionTypeSystem" - the code system describing the method of disposition of the decedent’s remains</para>
        /// <para>"dispositionTypeDisplay" - the human readable code display text that describes the method of disposition of the decedent’s remains</para>
        /// <para>"dispositionPlaceName" - the name of the disposition place</para>
        /// <para>"dispositionPlaceLine1" - disposition place address, line one</para>
        /// <para>"dispositionPlaceLine2" - disposition place address, line two</para>
        /// <para>"dispositionPlaceCity" - disposition place address, city</para>
        /// <para>"dispositionPlaceCounty" - disposition place address, county</para>
        /// <para>"dispositionPlaceState" - disposition place address, state</para>
        /// <para>"dispositionPlaceZip" - disposition place address, zip</para>
        /// <para>"dispositionPlaceCountry" - disposition place address, country</para>
        /// <para>"dispositionPlaceInsideCityLimits" - disposition place address, whether the address is within city limits (true) or not (false)</para>
        /// <para>"funeralFacilityName" - the name of a funeral facility or institution</para>
        /// <para>"funeralFacilityLine1" - funeral facility address, line one</para>
        /// <para>"funeralFacilityLine2" - funeral facility address, line two</para>
        /// <para>"funeralFacilityCity" - funeral facility address, city</para>
        /// <para>"funeralFacilityCounty" - funeral facility address, county</para>
        /// <para>"funeralFacilityState" - funeral facility address, state</para>
        /// <para>"funeralFacilityZip" - funeral facility address, zip</para>
        /// <para>"funeralFacilityCountry" - funeral facility address, country</para>
        /// <para>"funeralFacilityInsideCityLimits" - funeral facility address, whether the address is within city limits (true) or not (false)</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; disposition = new Dictionary&lt;string, string&gt;();</para>
        /// <para>disposition.Add("dispositionTypeCode", "449971000124106");</para>
        /// <para>disposition.Add("dispositionTypeSystem", "http://snomed.info/sct");</para>
        /// <para>disposition.Add("dispositionTypeDisplay", "Burial");</para>
        /// <para>disposition.Add("dispositionPlaceName", "Example disposition place name");</para>
        /// <para>disposition.Add("dispositionPlaceLine1", "100 Example Street");</para>
        /// <para>disposition.Add("dispositionPlaceLine2", "Line 2");</para>
        /// <para>disposition.Add("dispositionPlaceCity", "Bedford");</para>
        /// <para>disposition.Add("dispositionPlaceCounty", "Middlesex");</para>
        /// <para>disposition.Add("dispositionPlaceState", "Massachusetts");</para>
        /// <para>disposition.Add("dispositionPlaceZip", "01730");</para>
        /// <para>disposition.Add("dispositionPlaceCountry", "United States");</para>
        /// <para>disposition.Add("dispositionPlaceInsideCityLimits", "True");</para>
        /// <para>disposition.Add("funeralFacilityName", "Example funeral facility name");</para>
        /// <para>disposition.Add("funeralFacilityLine1", "50 Example Street");</para>
        /// <para>disposition.Add("funeralFacilityLine2", "Line 2");</para>
        /// <para>disposition.Add("funeralFacilityCity", "Bedford");</para>
        /// <para>disposition.Add("funeralFacilityCounty", "Middlesex");</para>
        /// <para>disposition.Add("funeralFacilityState", "Massachusetts");</para>
        /// <para>disposition.Add("funeralFacilityZip", "01730");</para>
        /// <para>disposition.Add("funeralFacilityCountry", "United States");</para>
        /// <para>disposition.Add("funeralFacilityInsideCityLimits", "True");</para>
        /// <para>ExampleDeathRecord.Disposition = disposition;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Funeral Facility name: {ExampleDeathRecord.Disposition["funeralFacilityName"]}");</para>
        /// </example>
        public Dictionary<string, string> Disposition
        {
            get
            {
                Dictionary<string, string> dictionary = new Dictionary<string, string>();

                // Disposition Type - The method of disposition of the decedent’s remains.
                dictionary.Add("dispositionTypeCode", GetFirstString("Bundle.entry.resource.where($this is Patient).extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-Disposition-extension').extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-DispositionType-extension').value.coding.code"));
                dictionary.Add("dispositionTypeSystem", GetFirstString("Bundle.entry.resource.where($this is Patient).extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-Disposition-extension').extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-DispositionType-extension').value.coding.system"));
                dictionary.Add("dispositionTypeDisplay", GetFirstString("Bundle.entry.resource.where($this is Patient).extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-Disposition-extension').extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-DispositionType-extension').value.coding.display"));

                // Disposition Place name
                dictionary.Add("dispositionPlaceName", GetFirstString("Bundle.entry.resource.where($this is Patient).extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-Disposition-extension').extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-DispositionFacility-extension').extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-FacilityName-extension').value"));

                // Disposition Place address
                dictionary.Add("dispositionPlaceLine1", GetFirstString("Bundle.entry.resource.where($this is Patient).extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-Disposition-extension').extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-DispositionFacility-extension').extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/shr-core-PostalAddress-extension').value.line[0]"));
                dictionary.Add("dispositionPlaceLine2", GetFirstString("Bundle.entry.resource.where($this is Patient).extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-Disposition-extension').extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-DispositionFacility-extension').extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/shr-core-PostalAddress-extension').value.line[1]"));
                dictionary.Add("dispositionPlaceCity", GetFirstString("Bundle.entry.resource.where($this is Patient).extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-Disposition-extension').extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-DispositionFacility-extension').extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/shr-core-PostalAddress-extension').value.city"));
                dictionary.Add("dispositionPlaceCounty", GetFirstString("Bundle.entry.resource.where($this is Patient).extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-Disposition-extension').extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-DispositionFacility-extension').extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/shr-core-PostalAddress-extension').value.district"));
                dictionary.Add("dispositionPlaceState", GetFirstString("Bundle.entry.resource.where($this is Patient).extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-Disposition-extension').extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-DispositionFacility-extension').extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/shr-core-PostalAddress-extension').value.state"));
                dictionary.Add("dispositionPlaceZip", GetFirstString("Bundle.entry.resource.where($this is Patient).extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-Disposition-extension').extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-DispositionFacility-extension').extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/shr-core-PostalAddress-extension').value.postalCode"));
                dictionary.Add("dispositionPlaceCountry", GetFirstString("Bundle.entry.resource.where($this is Patient).extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-Disposition-extension').extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-DispositionFacility-extension').extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/shr-core-PostalAddress-extension').value.country"));
                dictionary.Add("dispositionPlaceInsideCityLimits", GetFirstString("Bundle.entry.resource.where($this is Patient).extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-Disposition-extension').extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-DispositionFacility-extension').extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/shr-core-PostalAddress-extension').value.extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/shr-core-InsideCityLimits-extension').value"));

                // Disposition Facility name
                dictionary.Add("funeralFacilityName", GetFirstString("Bundle.entry.resource.where($this is Patient).extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-Disposition-extension').extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-FuneralFacility-extension').extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-FacilityName-extension').value"));

                // Disposition Facility address
                dictionary.Add("funeralFacilityLine1", GetFirstString("Bundle.entry.resource.where($this is Patient).extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-Disposition-extension').extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-FuneralFacility-extension').extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/shr-core-PostalAddress-extension').value.line[0]"));
                dictionary.Add("funeralFacilityLine2", GetFirstString("Bundle.entry.resource.where($this is Patient).extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-Disposition-extension').extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-FuneralFacility-extension').extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/shr-core-PostalAddress-extension').value.line[1]"));
                dictionary.Add("funeralFacilityCity", GetFirstString("Bundle.entry.resource.where($this is Patient).extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-Disposition-extension').extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-FuneralFacility-extension').extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/shr-core-PostalAddress-extension').value.city"));
                dictionary.Add("funeralFacilityCounty", GetFirstString("Bundle.entry.resource.where($this is Patient).extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-Disposition-extension').extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-FuneralFacility-extension').extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/shr-core-PostalAddress-extension').value.district"));
                dictionary.Add("funeralFacilityState", GetFirstString("Bundle.entry.resource.where($this is Patient).extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-Disposition-extension').extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-FuneralFacility-extension').extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/shr-core-PostalAddress-extension').value.state"));
                dictionary.Add("funeralFacilityZip", GetFirstString("Bundle.entry.resource.where($this is Patient).extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-Disposition-extension').extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-FuneralFacility-extension').extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/shr-core-PostalAddress-extension').value.postalCode"));
                dictionary.Add("funeralFacilityCountry", GetFirstString("Bundle.entry.resource.where($this is Patient).extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-Disposition-extension').extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-FuneralFacility-extension').extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/shr-core-PostalAddress-extension').value.country"));
                dictionary.Add("funeralFacilityInsideCityLimits", GetFirstString("Bundle.entry.resource.where($this is Patient).extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-Disposition-extension').extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-FuneralFacility-extension').extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/shr-core-PostalAddress-extension').value.extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/shr-core-InsideCityLimits-extension').value"));

                return dictionary;
            }
            set
            {
                // Disposition extension
                Patient.Extension.RemoveAll(ext => ext.Url == "http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-Disposition-extension");
                Extension dispositionExt = new Extension();
                dispositionExt.Url = "http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-Disposition-extension";

                // Disposition Type extension - The method of disposition of the decedent’s remains.
                Extension dispositionTypeExt = new Extension();
                dispositionTypeExt.Url = "http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-DispositionType-extension";
                CodeableConcept codeableConcept = new CodeableConcept();
                Coding coding = new Coding();
                coding.Code = GetValue(value, "dispositionTypeCode");
                coding.System = GetValue(value, "dispositionTypeSystem");
                coding.Display = GetValue(value, "dispositionTypeDisplay");
                codeableConcept.Coding.Add(coding);
                dispositionTypeExt.Value = codeableConcept;

                // Disposition Place extension - The place of disposition of the decedent’s remains.
                Extension dispositionPlaceExt = new Extension();
                dispositionPlaceExt.Url = "http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-DispositionFacility-extension";

                // Disposition Place name extension
                Extension dispositionPlaceNameExt = new Extension();
                dispositionPlaceNameExt.Url = "http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-FacilityName-extension";
                dispositionPlaceNameExt.Value = new FhirString(GetValue(value, "dispositionPlaceName"));
                dispositionPlaceExt.Extension.Add(dispositionPlaceNameExt);

                // Disposition Place address extension
                Extension dispositionPlaceAddressExt = new Extension();
                dispositionPlaceAddressExt.Url = "http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/shr-core-PostalAddress-extension";
                Address dispositionPlaceAddress = new Address();
                string[] lines = {GetValue(value, "dispositionPlaceLine1"), GetValue(value, "dispositionPlaceLine2")};
                dispositionPlaceAddress.Line = lines.ToArray();
                dispositionPlaceAddress.City = GetValue(value, "dispositionPlaceCity");
                dispositionPlaceAddress.District = GetValue(value, "dispositionPlaceCounty");
                dispositionPlaceAddress.State = GetValue(value, "dispositionPlaceState");
                dispositionPlaceAddress.PostalCode = GetValue(value, "dispositionPlaceZip");
                dispositionPlaceAddress.Country = GetValue(value, "dispositionPlaceCountry");
                dispositionPlaceAddress.Type = Hl7.Fhir.Model.Address.AddressType.Postal;
                if (value.ContainsKey("dispositionPlaceInsideCityLimits") && GetValue(value, "dispositionPlaceInsideCityLimits") != null)
                {
                    Extension insideCityLimits = new Extension();
                    insideCityLimits.Url = "http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/shr-core-InsideCityLimits-extension";
                    insideCityLimits.Value = new FhirBoolean(GetValue(value, "dispositionPlaceInsideCityLimits") == "true" || GetValue(value, "dispositionPlaceInsideCityLimits") == "True");
                    dispositionPlaceAddress.Extension.Add(insideCityLimits);
                }
                dispositionPlaceAddressExt.Value = dispositionPlaceAddress;
                dispositionPlaceExt.Extension.Add(dispositionPlaceAddressExt);

                // Funeral Facility extension - Name and address of the funeral facility.
                Extension funeralFacilityExt = new Extension();
                funeralFacilityExt.Url = "http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-FuneralFacility-extension";

                // Disposition Facility name extension
                Extension funeralFacilityNameExt = new Extension();
                funeralFacilityNameExt.Url = "http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-FacilityName-extension";
                funeralFacilityNameExt.Value = new FhirString(GetValue(value, "funeralFacilityName"));
                funeralFacilityExt.Extension.Add(funeralFacilityNameExt);

                // Disposition Facility address extension
                Extension funeralFacilityAddressExt = new Extension();
                funeralFacilityAddressExt.Url = "http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/shr-core-PostalAddress-extension";
                Address funeralFacilityAddress = new Address();
                string[] fflines = {GetValue(value, "funeralFacilityLine1"), GetValue(value, "funeralFacilityLine2")};
                funeralFacilityAddress.Line = fflines.ToArray();
                funeralFacilityAddress.City = GetValue(value, "funeralFacilityCity");
                funeralFacilityAddress.District = GetValue(value, "funeralFacilityCounty");
                funeralFacilityAddress.State = GetValue(value, "funeralFacilityState");
                funeralFacilityAddress.PostalCode = GetValue(value, "funeralFacilityZip");
                funeralFacilityAddress.Country = GetValue(value, "funeralFacilityCountry");
                funeralFacilityAddress.Type = Hl7.Fhir.Model.Address.AddressType.Postal;
                if (value.ContainsKey("funeralFacilityInsideCityLimits") && GetValue(value, "funeralFacilityInsideCityLimits") != null)
                {
                    Extension insideCityLimits = new Extension();
                    insideCityLimits.Url = "http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/shr-core-InsideCityLimits-extension";
                    insideCityLimits.Value = new FhirBoolean(GetValue(value, "funeralFacilityInsideCityLimits") == "true" || GetValue(value, "funeralFacilityInsideCityLimits") == "True");
                    funeralFacilityAddress.Extension.Add(insideCityLimits);
                }
                funeralFacilityAddressExt.Value = funeralFacilityAddress;
                funeralFacilityExt.Extension.Add(funeralFacilityAddressExt);

                dispositionExt.Extension.Add(dispositionTypeExt);
                dispositionExt.Extension.Add(dispositionPlaceExt);
                dispositionExt.Extension.Add(funeralFacilityExt);
                Patient.Extension.Add(dispositionExt);
            }
        }

        /// <summary>Decedent’s level of education. Corresponds to item 51 of the U.S. Standard Certificate of Death.</summary>
        /// <value>Decedent’s level of education. A Dictionary representing a code, containing the following key/value pairs:
        /// <para>"code" - the code describing this finding</para>
        /// <para>"system" - the system the given code belongs to</para>
        /// <para>"display" - the human readable display text that corresponds to the given code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; code = new Dictionary&lt;string, string&gt;();</para>
        /// <para>code.Add("code", "PHC1453");</para>
        /// <para>code.Add("system", "http://github.com/nightingaleproject/fhirDeathRecord/sdr/decedent/cs/EducationCS	");</para>
        /// <para>code.Add("display", "Bachelor's Degree");</para>
        /// <para>ExampleDeathRecord.MaritalStatus = code;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Degree: {ExampleDeathRecord.Education["display"]}");</para>
        /// </example>
        public Dictionary<string, string> Education
        {
            get
            {
                string code = GetFirstString("Bundle.entry.resource.where($this is Patient).extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-Education-extension').value.coding.code");
                string system = GetFirstString("Bundle.entry.resource.where($this is Patient).extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-Education-extension').value.coding.system");
                string display = GetFirstString("Bundle.entry.resource.where($this is Patient).extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-Education-extension').value.coding.display");
                Dictionary<string, string> education = new Dictionary<string, string>();
                education.Add("code", code);
                education.Add("system", system);
                education.Add("display", display);
                return education;
            }
            set
            {
                Patient.Extension.RemoveAll(ext => ext.Url == "http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-Education-extension");
                Extension education = new Extension();
                education.Url = "http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-Education-extension";
                education.Value = DictToCodeableConcept(value);
                Patient.Extension.Add(education);
            }
        }

        /// <summary>The decedent’s age in years at last birthday. Corresponds to item 4a of the U.S. Standard Certificate of Death.</summary>
        /// <value>The decedent’s age in years at last birthday.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.Age = "100";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Age in years at last birthday.: {ExampleDeathRecord.Age}");</para>
        /// </example>
        public string Age
        {
            get
            {
                string age = GetFirstString("Bundle.entry.resource.where($this is Patient).extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-Age-extension').value");
                if (age != null)
                {
                    return Convert.ToString(Int32.Parse(age));
                }
                else
                {
                    return null;
                }
            }
            set
            {
                int n;
                bool isNumeric = int.TryParse(value, out n);
                if (isNumeric)
                {
                    Patient.Extension.RemoveAll(ext => ext.Url == "http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-Age-extension");
                    Extension age = new Extension();
                    age.Url = "http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-Age-extension";
                    age.Value = new FhirDecimal(n);
                    Patient.Extension.Add(age);
                }
            }
        }

        /// <summary>Whether the decedent ever served in the US armed forces. Corresponds to item 8 of the U.S. Standard Certificate of Death.</summary>
        /// <value>Whether the decedent ever served in the US armed forces.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.ServedInArmedForces = False;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Served In Armed Forces?: {ExampleDeathRecord.ServedInArmedForces}");</para>
        /// </example>
        public bool ServedInArmedForces
        {
            get
            {
                return GetFirstString("Bundle.entry.resource.where($this is Patient).extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-ServedInArmedForces-extension').value") == "True";
            }
            set
            {
                Patient.Extension.RemoveAll(ext => ext.Url == "http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-ServedInArmedForces-extension");
                Extension served = new Extension();
                served.Url = "http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-ServedInArmedForces-extension";
                served.Value = new FhirBoolean(value);
                Patient.Extension.Add(served);
            }
        }

        /// <summary>Decedent’s usual occupation and industry. Corresponds to items 54 and 55 of the U.S. Standard Certificate of Death.</summary>
        /// <value>Decedent’s usual occupation and industry.
        /// <para>"jobDescription" - Type of work done during most of decedent’s working life. Corresponds to item 54 of the U.S. Standard Certificate of Death.</para>
        /// <para>"industryDescription" - Kind of industry or business in which the decedent worked. Corresponds to item 55 of the U.S. Standard Certificate of Death.</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; occupation = new Dictionary&lt;string, string&gt;();</para>
        /// <para>occupation.Add("jobDescription", "Example occupation");</para>
        /// <para>occupation.Add("industryDescription", "Example industry");</para>
        /// <para>SetterDeathRecord.Occupation = occupation;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Job Description: {ExampleDeathRecord.Occupation["jobDescription"]}");</para>
        /// </example>
        public Dictionary<string, string> Occupation
        {
            get
            {
                Dictionary<string, string> dictionary = new Dictionary<string, string>();
                dictionary.Add("jobDescription", GetFirstString("Bundle.entry.resource.where($this is Patient).extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-Occupation-extension').extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-Job-extension').value"));
                dictionary.Add("industryDescription", GetFirstString("Bundle.entry.resource.where($this is Patient).extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-Occupation-extension').extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-Industry-extension').value"));
                return dictionary;
            }
            set
            {
                // Occupation extension
                Patient.Extension.RemoveAll(ext => ext.Url == "http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-Occupation-extension");
                Extension occupation = new Extension();
                occupation.Url = "http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-Occupation-extension";

                // Job extension
                Extension jobExt = new Extension();
                jobExt.Url = "http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-Job-extension";
                jobExt.Value = new FhirString(GetValue(value, "jobDescription"));
                occupation.Extension.Add(jobExt);

                // Industry extension
                Extension industryExt = new Extension();
                industryExt.Url = "http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-decedent-Industry-extension";
                industryExt.Value = new FhirString(GetValue(value, "industryDescription"));
                occupation.Extension.Add(industryExt);

                Patient.Extension.Add(occupation);
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
                return GetAllString("Bundle.entry.resource.where($this is Practitioner).name.where(use='official').given");
            }
            set
            {
                HumanName name = Practitioner.Name.SingleOrDefault(n => n.Use == HumanName.NameUse.Official);
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
                return GetFirstString("Bundle.entry.resource.where($this is Practitioner).name.where(use='official').family");
            }
            set
            {
                HumanName name = Practitioner.Name.FirstOrDefault(); // Check if there is already a HumanName on the Certifier.
                if (name != null && !String.IsNullOrEmpty(value))
                {
                    name.Family = value;
                }
                else if (!String.IsNullOrEmpty(value))
                {
                    name = new HumanName();
                    name.Use = HumanName.NameUse.Official;
                    name.Family = value;
                    Practitioner.Name.Add(name);
                }
            }
        }

        /// <summary>Certifier's Suffix.</summary>
        /// <value>the certifier's suffix</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.CertifierSuffix = "Jr.";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Certifier Suffix: {ExampleDeathRecord.CertifierSuffix}");</para>
        /// </example>
        public string CertifierSuffix
        {
            get
            {
                return GetFirstString("Bundle.entry.resource.where($this is Practitioner).name.suffix");
            }
            set
            {
                HumanName name = Practitioner.Name.FirstOrDefault(); // Check if there is already a HumanName on the Decedent.
                if (name != null && !String.IsNullOrEmpty(value))
                {
                    string[] suffix = { value };
                    name.Suffix = suffix;
                }
                else if (!String.IsNullOrEmpty(value))
                {
                    name = new HumanName();
                    name.Use = HumanName.NameUse.Official;
                    string[] suffix = { value };
                    name.Suffix = suffix;
                    Practitioner.Name.Add(name);
                }
            }
        }

        /// <summary>Address of certifier.</summary>
        /// <value>the certifier's address</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; address = new Dictionary&lt;string, string&gt;();</para>
        /// <para>address.Add("street", "123 Test Street");</para>
        /// <para>address.Add("city", "Boston");</para>
        /// <para>address.Add("state", "Massachusetts");</para>
        /// <para>address.Add("zip", "12345");</para>
        /// <para>ExampleDeathRecord.CertifierAddress = address;</para>
        /// <para>// Getter:</para>
        /// <para>foreach(var pair in ExampleDeathRecord.CertifierAddress)</para>
        /// <para>{</para>
        /// <para>      Console.WriteLine($"\tCertifierAddress key: {pair.Key}: value: {pair.Value}");</para>
        /// <para>};</para>
        /// </example>
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
                Address address = new Address();
                address.LineElement.Add(new FhirString(value["street"]));
                address.City = value["city"];
                address.State = value["state"];
                address.PostalCodeElement = new FhirString(value["zip"]);
                Practitioner.Address = new List<Hl7.Fhir.Model.Address>();
                Practitioner.Address.Add(address);
            }
        }

        /// <summary>Certifier Type.</summary>
        /// <value>the certifier type</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; type = new Dictionary&lt;string, string&gt;();</para>
        /// <para>code.Add("code", "434651000124107");</para>
        /// <para>code.Add("display", "Physician (Pronouncer and Certifier)");</para>
        /// <para>ExampleDeathRecord.CertifierType = type;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"\tCertifier Type: {ExampleDeathRecord.CertifierType['display']}");</para>
        /// </example>
        public Dictionary<string, string> CertifierType
        {
            get
            {
                string display = GetFirstString("Bundle.entry.resource.where($this is Practitioner).extension.where(url = 'http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-deathRecord-CertifierType-extension').value.coding.display");
                string code = GetFirstString("Bundle.entry.resource.where($this is Practitioner).extension.where(url = 'http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-deathRecord-CertifierType-extension').value.coding.code");
                string system = GetFirstString("Bundle.entry.resource.where($this is Practitioner).extension.where(url = 'http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-deathRecord-CertifierType-extension').value.coding.system");
                Dictionary<string, string> dictionary = new Dictionary<string, string>();
                dictionary.Add("display", display);
                dictionary.Add("code", code);
                dictionary.Add("system", system);
                return dictionary;
            }
            set
            {
                Practitioner.Extension.RemoveAll(ext => ext.Url == "http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-deathRecord-CertifierType-extension");
                Extension type = new Extension();
                type.Url = "http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-deathRecord-CertifierType-extension";
                type.Value = DictToCodeableConcept(value);
                Practitioner.Extension.Add(type);
            }
        }

        /// <summary>Certifier Qualification.</summary>
        /// <value>the certifier qualification</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; qualification = new Dictionary&lt;string, string&gt;();</para>
        /// <para>code.Add("code", "MD");</para>
        /// <para>code.Add("system", "http://hl7.org/fhir/v2/0360/2.7");</para>
        /// <para>code.Add("display", "Doctor of Medicine");</para>
        /// <para>ExampleDeathRecord.CertifierQualification = qualification;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"\tCertifier Qualification: {ExampleDeathRecord.CertifierQualification['display']}");</para>
        /// </example>
        public Dictionary<string, string> CertifierQualification
        {
            get
            {
                Practitioner.QualificationComponent qualification = Practitioner.Qualification.FirstOrDefault();
                Dictionary<string, string> dictionary = new Dictionary<string, string>();
                dictionary.Add("display", ((qualification != null && qualification.Code.Coding.FirstOrDefault() != null) ? qualification.Code.Coding.FirstOrDefault().Display : ""));
                dictionary.Add("code", ((qualification != null && qualification.Code.Coding.FirstOrDefault() != null) ? qualification.Code.Coding.FirstOrDefault().Code : ""));
                dictionary.Add("system", ((qualification != null && qualification.Code.Coding.FirstOrDefault() != null) ? qualification.Code.Coding.FirstOrDefault().System : ""));
                return dictionary;
            }
            set
            {
                Practitioner.QualificationComponent qualification = new Practitioner.QualificationComponent();
                qualification.Code = DictToCodeableConcept(value);
                Practitioner.Qualification.Clear();
                Practitioner.Qualification.Add(qualification);
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
        /// <para>    Tuple.Create("Example Immediate COD", "minutes", new Dictionary&lt;string, string&gt;(){ {"code", "1234"}, {"system", "example"} }),</para>
        /// <para>    Tuple.Create("Example Underlying COD 1", "2 hours", new Dictionary&lt;string, string&gt;()),</para>
        /// <para>    Tuple.Create("Example Underlying COD 2", "6 months", new Dictionary&lt;string, string&gt;()),</para>
        /// <para>    Tuple.Create("Example Underlying COD 3", "15 years", new Dictionary&lt;string, string&gt;())</para>
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
                        else
                        {
                            code.Add("code", "");
                            code.Add("system", "");
                            code.Add("display", "");
                        }
                    }
                    else
                    {
                        code.Add("code", "");
                        code.Add("system", "");
                        code.Add("display", "");
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

        /// <summary>An alias for the <c>CausesOfDeath</c> property - Cause of Death Part I, Line a.</summary>
        public string COD1A
        {
            get
            {
                Tuple<string, string, Dictionary<string, string>>[] causes = CausesOfDeath;
                if (causes.Length > 0)
                {
                    return string.IsNullOrWhiteSpace(causes[0].Item1) ? "" : causes[0].Item1;
                }
                return "";
            }
            set
            {
                Tuple<string, string, Dictionary<string, string>>[] causes = CausesOfDeath;
                if (causes.Length > 0)
                {
                    InsertCOD(Tuple.Create(value.Trim(), causes[0].Item2, causes[0].Item3), 0);
                }
                else
                {
                    InsertCOD(Tuple.Create(value.Trim(), (string)null, new Dictionary<string, string>()), 0);
                }
            }
        }

        /// <summary>An alias for the <c>CausesOfDeath</c> property - Cause of Death Part I Interval, Line a.</summary>
        public string INTERVAL1A
        {
            get
            {
                Tuple<string, string, Dictionary<string, string>>[] causes = CausesOfDeath;
                if (causes.Length > 0)
                {
                    return string.IsNullOrWhiteSpace(causes[0].Item2) ? "" : causes[0].Item2;
                }
                return "";
            }
            set
            {
                Tuple<string, string, Dictionary<string, string>>[] causes = CausesOfDeath;
                if (causes.Length > 0)
                {
                    InsertCOD(Tuple.Create(causes[0].Item1, value.Trim(), causes[0].Item3), 0);
                }
                else
                {
                    InsertCOD(Tuple.Create((string)null, value.Trim(), new Dictionary<string, string>()), 0);
                }
            }
        }

        /// <summary>An alias for the <c>CausesOfDeath</c> property - Cause of Death Part I Code, Line a.</summary>
        public Dictionary<string, string> CODE1A
        {
            get
            {
                Tuple<string, string, Dictionary<string, string>>[] causes = CausesOfDeath;
                if (causes.Length > 0)
                {
                    return causes[0].Item3;
                }
                return null;
            }
            set
            {
                Tuple<string, string, Dictionary<string, string>>[] causes = CausesOfDeath;
                if (causes.Length > 0)
                {
                    InsertCOD(Tuple.Create(causes[0].Item1, causes[0].Item2, value), 0);
                }
                else
                {
                    InsertCOD(Tuple.Create((string)null, (string)null, value), 0);
                }
            }
        }

        /// <summary>An alias for the <c>CausesOfDeath</c> property - Cause of Death Part I, Line b.</summary>
        public string COD1B
        {
            get
            {
                Tuple<string, string, Dictionary<string, string>>[] causes = CausesOfDeath;
                if (causes.Length > 1)
                {
                    return string.IsNullOrWhiteSpace(causes[1].Item1) ? "" : causes[1].Item1;
                }
                return "";
            }
            set
            {
                Tuple<string, string, Dictionary<string, string>>[] causes = CausesOfDeath;
                if (causes.Length > 1)
                {
                    InsertCOD(Tuple.Create(value.Trim(), causes[1].Item2, causes[1].Item3), 1);
                }
                else
                {
                    InsertCOD(Tuple.Create(value.Trim(), (string)null, new Dictionary<string, string>()), 1);
                }
            }
        }

        /// <summary>An alias for the <c>CausesOfDeath</c> property - Cause of Death Part I Interval, Line b.</summary>
        public string INTERVAL1B
        {
            get
            {
                Tuple<string, string, Dictionary<string, string>>[] causes = CausesOfDeath;
                if (causes.Length > 1)
                {
                    return string.IsNullOrWhiteSpace(causes[1].Item2) ? "" : causes[1].Item2;
                }
                return "";
            }
            set
            {
                Tuple<string, string, Dictionary<string, string>>[] causes = CausesOfDeath;
                if (causes.Length > 1)
                {
                    InsertCOD(Tuple.Create(causes[1].Item1, value.Trim(), causes[1].Item3), 1);
                }
                else
                {
                    InsertCOD(Tuple.Create((string)null, value.Trim(), new Dictionary<string, string>()), 1);
                }
            }
        }

        /// <summary>An alias for the <c>CausesOfDeath</c> property - Cause of Death Part I Code, Line b.</summary>
        public Dictionary<string, string> CODE1B
        {
            get
            {
                Tuple<string, string, Dictionary<string, string>>[] causes = CausesOfDeath;
                if (causes.Length > 1)
                {
                    return causes[1].Item3;
                }
                return null;
            }
            set
            {
                Tuple<string, string, Dictionary<string, string>>[] causes = CausesOfDeath;
                if (causes.Length > 1)
                {
                    InsertCOD(Tuple.Create(causes[1].Item1, causes[1].Item2, value), 1);
                }
                else
                {
                    InsertCOD(Tuple.Create((string)null, (string)null, value), 1);
                }
            }
        }

        /// <summary>An alias for the <c>CausesOfDeath</c> property - Cause of Death Part I, Line c.</summary>
        public string COD1C
        {
            get
            {
                Tuple<string, string, Dictionary<string, string>>[] causes = CausesOfDeath;
                if (causes.Length > 2)
                {
                    return string.IsNullOrWhiteSpace(causes[2].Item1) ? "" : causes[2].Item1;
                }
                return "";
            }
            set
            {
                Tuple<string, string, Dictionary<string, string>>[] causes = CausesOfDeath;
                if (causes.Length > 2)
                {
                    InsertCOD(Tuple.Create(value.Trim(), causes[2].Item2, causes[2].Item3), 2);
                }
                else
                {
                    InsertCOD(Tuple.Create(value.Trim(), (string)null, new Dictionary<string, string>()), 2);
                }
            }
        }

        /// <summary>An alias for the <c>CausesOfDeath</c> property - Cause of Death Part I Interval, Line c.</summary>
        public string INTERVAL1C
        {
            get
            {
                Tuple<string, string, Dictionary<string, string>>[] causes = CausesOfDeath;
                if (causes.Length > 2)
                {
                    return string.IsNullOrWhiteSpace(causes[2].Item2) ? "" : causes[2].Item2;
                }
                return "";
            }
            set
            {
                Tuple<string, string, Dictionary<string, string>>[] causes = CausesOfDeath;
                if (causes.Length > 2)
                {
                    InsertCOD(Tuple.Create(causes[2].Item1, value.Trim(), causes[2].Item3), 2);
                }
                else
                {
                    InsertCOD(Tuple.Create((string)null, value.Trim(), new Dictionary<string, string>()), 2);
                }
            }
        }

        /// <summary>An alias for the <c>CausesOfDeath</c> property - Cause of Death Part I Code, Line c.</summary>
        public Dictionary<string, string> CODE1C
        {
            get
            {
                Tuple<string, string, Dictionary<string, string>>[] causes = CausesOfDeath;
                if (causes.Length > 2)
                {
                    return causes[2].Item3;
                }
                return null;
            }
            set
            {
                Tuple<string, string, Dictionary<string, string>>[] causes = CausesOfDeath;
                if (causes.Length > 2)
                {
                    InsertCOD(Tuple.Create(causes[2].Item1, causes[2].Item2, value), 2);
                }
                else
                {
                    InsertCOD(Tuple.Create((string)null, (string)null, value), 2);
                }
            }
        }

        /// <summary>An alias for the <c>CausesOfDeath</c> property - Cause of Death Part I, Line d.</summary>
        public string COD1D
        {
            get
            {
                Tuple<string, string, Dictionary<string, string>>[] causes = CausesOfDeath;
                if (causes.Length > 3)
                {
                    return string.IsNullOrWhiteSpace(causes[3].Item1) ? "" : causes[3].Item1;
                }
                return "";
            }
            set
            {
                Tuple<string, string, Dictionary<string, string>>[] causes = CausesOfDeath;
                if (causes.Length > 3)
                {
                    InsertCOD(Tuple.Create(value.Trim(), causes[3].Item2, causes[3].Item3), 3);
                }
                else
                {
                    InsertCOD(Tuple.Create(value.Trim(), (string)null, new Dictionary<string, string>()), 3);
                }
            }
        }

        /// <summary>An alias for the <c>CausesOfDeath</c> property - Cause of Death Part I Interval, Line d.</summary>
        public string INTERVAL1D
        {
            get
            {
                Tuple<string, string, Dictionary<string, string>>[] causes = CausesOfDeath;
                if (causes.Length > 3)
                {
                    return string.IsNullOrWhiteSpace(causes[3].Item2) ? "" : causes[3].Item2;
                }
                return "";
            }
            set
            {
                Tuple<string, string, Dictionary<string, string>>[] causes = CausesOfDeath;
                if (causes.Length > 3)
                {
                    InsertCOD(Tuple.Create(causes[3].Item1, value.Trim(), causes[3].Item3), 3);
                }
                else
                {
                    InsertCOD(Tuple.Create((string)null, value.Trim(), new Dictionary<string, string>()), 3);
                }
            }
        }

        /// <summary>An alias for the <c>CausesOfDeath</c> property - Cause of Death Part I Code, Line d.</summary>
        public Dictionary<string, string> CODE1D
        {
            get
            {
                Tuple<string, string, Dictionary<string, string>>[] causes = CausesOfDeath;
                if (causes.Length > 3)
                {
                    return causes[3].Item3;
                }
                return null;
            }
            set
            {
                Tuple<string, string, Dictionary<string, string>>[] causes = CausesOfDeath;
                if (causes.Length > 3)
                {
                    InsertCOD(Tuple.Create(causes[3].Item1, causes[3].Item2, value), 3);
                }
                else
                {
                    InsertCOD(Tuple.Create((string)null, (string)null, value), 3);
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
        /// <para>"placeOfInjuryCounty" - location of injury, county</para>
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
        /// <para>detailsOfInjury.Add("placeOfInjuryCounty", "Middlesex");</para>
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
                dictionary.Add("placeOfInjuryCounty", GetFirstString("Bundle.entry.resource.where($this is Observation).where(code.coding.code='11374-6').extension.where(url='http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/shr-core-PostalAddress-extension').value.district"));
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
                observation.Value = new FhirString(GetValue(value, "description"));
                observation.Effective = new FhirDateTime(GetValue(value, "effectiveDateTime"));
                Extension placeOfInjury = new Extension();
                placeOfInjury.Url = "http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/sdr-causeOfDeath-PlaceOfInjury-extension";
                placeOfInjury.Value = new FhirString(GetValue(value, "placeOfInjuryDescription"));
                observation.Extension.Add(placeOfInjury);
                Extension placeOfInjuryLocation = new Extension();
                placeOfInjuryLocation.Url = "http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/shr-core-PostalAddress-extension";
                Address placeOfInjuryLocationAddress = new Address();
                string[] lines = {GetValue(value, "placeOfInjuryLine1"), GetValue(value, "placeOfInjuryLine2")};
                placeOfInjuryLocationAddress.Line = lines.ToArray();
                placeOfInjuryLocationAddress.City = GetValue(value, "placeOfInjuryCity");
                placeOfInjuryLocationAddress.District = GetValue(value, "placeOfInjuryCounty");
                placeOfInjuryLocationAddress.State = GetValue(value, "placeOfInjuryState");
                placeOfInjuryLocationAddress.PostalCode = GetValue(value, "placeOfInjuryZip");
                placeOfInjuryLocationAddress.Country = GetValue(value, "placeOfInjuryCountry");
                placeOfInjuryLocationAddress.Type = Hl7.Fhir.Model.Address.AddressType.Postal;
                if (!String.IsNullOrWhiteSpace(GetValue(value, "placeOfInjuryInsideCityLimits")))
                {
                    Extension insideCityLimits = new Extension();
                    insideCityLimits.Url = "http://nightingaleproject.github.io/fhirDeathRecord/StructureDefinition/shr-core-InsideCityLimits-extension";
                    insideCityLimits.Value = new FhirBoolean(GetValue(value, "placeOfInjuryInsideCityLimits") == "true" || GetValue(value, "placeOfInjuryInsideCityLimits") == "True");
                    placeOfInjuryLocationAddress.Extension.Add(insideCityLimits);
                }
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
        /// <param name="reference">a reference.</param>
        private void AddReferenceToComposition(string reference)
        {
            Composition.Section.First().Entry.Add(new ResourceReference(reference));
        }

        /// <summary>Remove a reference from the Death Record Composition.</summary>
        /// <param name="reference">a reference.</param>
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
            if (dict != null)
            {
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
            }
            codeableConcept.Coding.Add(coding);
            return codeableConcept;
        }

        /// <summary>Inserts a single COD line into the CausesOfDeath property.</summary>
        /// <param name="cod">cause of death to add.</param>
        /// <param name="index">what index the cause of death should appear.</param>
        private void InsertCOD(Tuple<string, string, Dictionary<string, string>> cod, int index)
        {
            List<Tuple<string, string, Dictionary<string, string>>> causes = CausesOfDeath.ToList();
            for (int i = 0; i < index + 1; i++)
            {
                if (causes.Count <= i)
                {
                    if (i == index)
                    {
                        causes.Add(cod);
                    }
                    else
                    {
                        causes.Add(Tuple.Create("", "", new Dictionary<string, string>()));
                    }
                }
                else if (i == index)
                {
                    causes[i] = cod;
                }
            }
            CausesOfDeath = causes.ToArray();
        }

        /// <summary>Given a FHIR path, return the elements that match the given path;
        /// returns an empty array if no matches are found.</summary>
        /// <param name="path">represents a FHIR path.</param>
        /// <returns>all elements that match the given path, or an empty array if no matches are found.</returns>
        public object[] GetAll(string path)
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
        public object GetFirst(string path)
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
        public object GetLast(string path)
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

        /// <summary>Get a value from a Dictionary, but return null if the key doesn't exist.</summary>
        private static string GetValue(Dictionary<string, string> dict, string key)
        {
            string value;
            dict.TryGetValue(key, out value);
            return value;
        }

        /// <summary>Combine the given dictionaries and return the combined result.</summary>
        private static Dictionary<string, string> UpdateDictionary(Dictionary<string, string> a, Dictionary<string, string> b)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            foreach(KeyValuePair<string, string> entry in a)
            {
                dictionary[entry.Key] = entry.Value;
            }
            foreach(KeyValuePair<string, string> entry in b)
            {
                dictionary[entry.Key] = entry.Value;
            }
            return dictionary;
        }

        /// <summary>HTML escape cause of death divs in the given Bundle.</summary>
        private static void EscapeCauses(Bundle bundle)
        {
            var causes = bundle.Entry.Where( entry => entry.Resource.ResourceType == ResourceType.Condition).ToList();
            foreach (var cause in causes)
            {
                Condition causeEntry = (Condition)cause.Resource;
                string causeNarrativeDiv = causeEntry.Text.Div;
                Narrative narrative = new Narrative();
                narrative.Div = HttpUtility.HtmlEncode(causeNarrativeDiv);
                narrative.Status = Narrative.NarrativeStatus.Additional;
                causeEntry.Text = narrative;
            }
        }

        /// <summary>HTML un-escape cause of death divs in the given Bundle.</summary>
        private static void UnescapeCauses(Bundle bundle)
        {
            var causes = bundle.Entry.Where( entry => entry.Resource.ResourceType == ResourceType.Condition).ToList();
            foreach (var cause in causes)
            {
                Condition causeEntry = (Condition)cause.Resource;
                string causeNarrativeDiv = causeEntry.Text.Div;
                Narrative narrative = new Narrative();
                narrative.Div = HttpUtility.HtmlDecode(causeNarrativeDiv);
                narrative.Status = Narrative.NarrativeStatus.Additional;
                causeEntry.Text = narrative;
            }
        }
    }
}


