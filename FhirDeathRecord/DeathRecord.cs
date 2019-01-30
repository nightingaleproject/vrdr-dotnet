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

        /// <summary>The certifier.</summary>
        private Practitioner Practitioner;

        /// <summary>The death date observation.</summary>
        private Observation DeathDate;

        /// <summary>The death location.</summary>
        private Location DeathLocation;

        /// <summary>The disposition method observation.</summary>
        private Observation DispositionMethod;

        /// <summary>The disposition location.</summary>
        private Location DispositionLocation;

        /// <summary>The mortician.</summary>
        private Practitioner Mortician;

        /// <summary>The funeral home.</summary>
        private Organization FuneralHome;

        /// <summary>The funeral home director.</summary>
        private PractitionerRole FuneralHomeDirector;

        /// <summary>The injury incident.</summary>
        private Observation InjuryIncident;

        /// <summary>The injury location.</summary>
        private Location InjuryLocation;

        /// <summary>If medical examiner contacted observation.</summary>
        private Observation MEContacted;

        /// <summary>Interested Party.</summary>
        private Organization InterestedParty;

        /// <summary>Death Certificate Reference.</summary>
        private DocumentReference DeathCertificateReference;

        /// <summary>Decedent's Education Level.</summary>
        private Observation EducationLevel;

        /// <summary>Decedent's Employment History.</summary>
        private Observation EmploymentHistory;

        /// <summary>The ordering of causes of death.</summary>
        private Hl7.Fhir.Model.List CauseOfDeathPathway;

        /// <summary>Mortality data for code translations.</summary>
        private MortalityData MortalityData = MortalityData.Instance;

        /// <summary>Default constructor that creates a new, empty FHIR SDR.</summary>
        public DeathRecord()
        {
            Bundle = new Bundle();
            Bundle.Type = Bundle.BundleType.Document; // By default, Bundle type is "document".
            Bundle.Identifier = new Identifier();
            Bundle.Identifier.Value = "1234567890";
            Bundle.Meta = new Meta();
            string[] bundle_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Death-Certificate-Document" };
            Bundle.Meta.Profile = bundle_profile;

            // Add Composition to bundle. As the record is filled out, new entries will be added to this element.
            Composition = new Composition();
            Composition.Id = "urn:uuid:" + Guid.NewGuid().ToString();
            Composition.Status = CompositionStatus.Final;
            Composition.Meta = new Meta();
            string[] composition_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Death-Certificate" };
            Composition.Meta.Profile = composition_profile;
            Composition.Type = new CodeableConcept("http://loinc.org", "64297-5");
            Composition.Title = "Record of Death";
            Composition.Date = DateTime.Now.ToString("o"); // By default, set creation date to now.
            Composition.Identifier = new Identifier();
            Composition.Identifier.Value = "123454"; // TODO
            Hl7.Fhir.Model.Composition.EventComponent eventComponent = new Hl7.Fhir.Model.Composition.EventComponent();
            eventComponent.Code.Add(new CodeableConcept(null, "103693007"));
            Composition.Event.Add(eventComponent);
            Hl7.Fhir.Model.Composition.AttesterComponent attesterComponent = new Hl7.Fhir.Model.Composition.AttesterComponent();
            attesterComponent.Time = "time";
            List<Hl7.Fhir.Model.Composition.CompositionAttestationMode> modes = new List<Hl7.Fhir.Model.Composition.CompositionAttestationMode>();
            attesterComponent.ModeElement.Add(new Code<Hl7.Fhir.Model.Composition.CompositionAttestationMode>(Hl7.Fhir.Model.Composition.CompositionAttestationMode.Legal));
            Composition.Attester.Add(attesterComponent);
            Bundle.AddResourceEntry(Composition, Composition.Id);

            InterestedParty = new Organization();
            InterestedParty.Id = "urn:uuid:" + Guid.NewGuid().ToString();
            InterestedParty.Meta = new Meta();
            string[] ip_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Interested-Party" };
            InterestedParty.Meta.Profile = ip_profile;
            Identifier ipid = new Identifier();
            ipid.Value = "1234567890";
            InterestedParty.Identifier.Add(ipid);
            InterestedParty.Active = true;
            InterestedParty.Type.Add(new CodeableConcept("http://hl7.org/fhir/ValueSet/organization-type", "dept", "Hospital Department"));
            InterestedParty.Name = "Example Interested Party";
            Address address = new Address();
            address.City = "Worcester";
            address.State = "Massachusetts";
            InterestedParty.Address.Add(address);
            Bundle.AddResourceEntry(InterestedParty, InterestedParty.Id);




            // Start with empty Patient to represent Decedent.
            Patient = new Patient();
            Patient.Id = "example-decedent"; //"urn:uuid:" + Guid.NewGuid().ToString(); // TODO
            Composition.Subject = new ResourceReference(Patient.Id);
            //section.Entry.Add(new ResourceReference(Patient.Id));
            //Bundle.AddResourceEntry(Patient, Patient.Id);


            DeathCertificateReference = new DocumentReference();
            DeathCertificateReference.Id = "urn:uuid:" + Guid.NewGuid().ToString();
            DeathCertificateReference.Meta = new Meta();
            string[] dcr_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Death-Certificate-Reference" };
            DeathCertificateReference.Meta.Profile = dcr_profile;
            Identifier dcrid = new Identifier();
            dcrid.Value = "1234567890";
            DeathCertificateReference.Identifier.Add(dcrid);
            DeathCertificateReference.Status = DocumentReferenceStatus.Current;
            DeathCertificateReference.Type = new CodeableConcept("http://loinc.org", "64297-5");
            DeathCertificateReference.Subject = new ResourceReference(Patient.Id);
            DeathCertificateReference.Indexed = new DateTimeOffset();
            DeathCertificateReference.Author.Add(new ResourceReference(InterestedParty.Id));
            Bundle.AddResourceEntry(DeathCertificateReference, DeathCertificateReference.Id);


            // Start with empty Practitioner to represent Certifier.
            Practitioner = new Practitioner();
            Practitioner.Id = "example-practitioner"; //"urn:uuid:" + Guid.NewGuid().ToString(); // TODO
            Practitioner.Meta = new Meta();
            string[] practProf = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Certifier" };
            Practitioner.Meta.Profile = practProf;
            //ResourceReference[] authors = { new ResourceReference(Practitioner.Id) };
            //Composition.Author = authors.ToList();
            //section.Entry.Add(new ResourceReference(Practitioner.Id));
            attesterComponent.Party = new ResourceReference(Practitioner.Id);


            // VRDR Death Certification
            Procedure deathCertification = new Procedure();
            deathCertification.Id = "urn:uuid:" + Guid.NewGuid().ToString();
            deathCertification.Meta = new Meta();
            string[] deathCertification_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Death-Certification" };
            deathCertification.Meta.Profile = deathCertification_profile;
            deathCertification.Status = EventStatus.Completed;
            deathCertification.Code = new CodeableConcept(null, "308646001");
            deathCertification.Category = new CodeableConcept(null, "103693007");
            Procedure.PerformerComponent performer = new Procedure.PerformerComponent();
            performer.Actor = new ResourceReference(Practitioner.Id);
            performer.Role = new CodeableConcept("http://hl7.org/fhir/ValueSet/performer-role", "59058001 ", "General physician");
            deathCertification.Performer.Add(performer);
            eventComponent.Detail.Add(new ResourceReference(deathCertification.Id));
            //Bundle.AddResourceEntry(deathCertification, deathCertification.Id);

            // Death Date Observation
            DeathDate = AddObservation("http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Death-Date", "81956-5", "urn:oid:2.16.840.1.113883.6.1", "Date and time of death");
            ResourceReference[] performers = { new ResourceReference(Practitioner.Id) };
            DeathDate.Performer = performers.ToList();

            RelatedPerson spouse = new RelatedPerson();
            spouse.Id = "urn:uuid:" + Guid.NewGuid().ToString();
            spouse.Meta = new Meta();
            string[] spouse_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Decedent-Spouse" };
            spouse.Meta.Profile = spouse_profile;
            HumanName spouse_name = new HumanName();
            spouse_name.Family = "spouse";
            spouse.Name.Add(spouse_name);
            spouse.Relationship = new CodeableConcept(null, "SPS");
            spouse.Patient = new ResourceReference(Patient.Id);
            Bundle.AddResourceEntry(spouse, spouse.Id);

            RelatedPerson mother = new RelatedPerson();
            mother.Id = "urn:uuid:" + Guid.NewGuid().ToString();
            mother.Meta = new Meta();
            string[] mother_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Decedent-Mother" };
            mother.Meta.Profile = mother_profile;
            HumanName mother_name = new HumanName();
            mother_name.Family = "mother";
            mother.Name.Add(mother_name);
            mother.Relationship = new CodeableConcept(null, "MTH");
            mother.Patient = new ResourceReference(Patient.Id);
            Bundle.AddResourceEntry(mother, mother.Id);

            RelatedPerson father = new RelatedPerson();
            father.Id = "urn:uuid:" + Guid.NewGuid().ToString();
            father.Meta = new Meta();
            string[] father_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Decedent-Father" };
            father.Meta.Profile = father_profile;
            HumanName father_name = new HumanName();
            father_name.Family = "father";
            father.Name.Add(father_name);
            father.Relationship = new CodeableConcept(null, "FTH");
            father.Patient = new ResourceReference(Patient.Id);
            Bundle.AddResourceEntry(father, father.Id);

            // Create a Navigator for this new death record.
            Navigator = Bundle.ToTypedElement();
        }

        /// <summary>Constructor that takes a string that represents a FHIR SDR in either XML or JSON format.</summary>
        /// <param name="record">represents a FHIR SDR in either XML or JSON format.</param>
        /// <exception cref="ArgumentException">Record is neither valid XML nor JSON.</exception>
        public DeathRecord(string record)
        {
            // Check if XML
            if (!string.IsNullOrEmpty(record) && record.TrimStart().StartsWith("<"))
            {
                FhirXmlParser parser = new FhirXmlParser();
                Bundle = parser.Parse<Bundle>(record);
                Navigator = Bundle.ToTypedElement();
            }
            else
            {
                // Assume JSON
                FhirJsonParser parser = new FhirJsonParser();
                Bundle = parser.Parse<Bundle>(record);
                Navigator = Bundle.ToTypedElement();
            }

            // TODO update class refs to use parsed info
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
                return GetFirstString("Bundle.entry.resource.where($this is Patient).name.where(use='official').given");
            }
            set
            {
                HumanName name = Patient.Name.SingleOrDefault(n => n.Use == HumanName.NameUse.Official);
                if (name != null && !String.IsNullOrEmpty(value))
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
                else if (!String.IsNullOrEmpty(value))
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
                return GetLastString("Bundle.entry.resource.where($this is Patient).name.where(use='official').given");
            }
            set
            {
                HumanName name = Patient.Name.SingleOrDefault(n => n.Use == HumanName.NameUse.Official);
                if (name != null && !String.IsNullOrEmpty(value))
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
                else if (!String.IsNullOrEmpty(value))
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

        /// <summary>Decedent's Nickname.</summary>
        /// <value>the decedent's nickname</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.Nickname = "Example Nickname";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Decedent Nickname: {ExampleDeathRecord.Nickname}");</para>
        /// </example>
        public string Nickname
        {
            get
            {
                return GetFirstString("Bundle.entry.resource.where($this is Patient).name.where(use='nickname').text");
            }
            set
            {
                HumanName name = Patient.Name.SingleOrDefault(n => n.Use == HumanName.NameUse.Nickname);
                if (name != null && !String.IsNullOrEmpty(value))
                {
                    name.Text = value;
                }
                else if (!String.IsNullOrEmpty(value))
                {
                    name = new HumanName();
                    name.Use = HumanName.NameUse.Nickname;
                    name.Text = value;
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
                Patient.BirthDate = value;
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
                dictionary.Add("residenceInsideCityLimits", GetFirstString("Bundle.entry.resource.where($this is Patient).address.extension.where(url='http://hl7.org/fhir/us/vrdr/StructureDefinition/Within-City-Limits-Indicator').value"));

                return dictionary;
            }
            set
            {
                Address address = new Address();
                address.Use = Address.AddressUse.Home;
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
                    insideCityLimits.Url = "http://hl7.org/fhir/us/vrdr/StructureDefinition/Within-City-Limits-Indicator";
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
                return GetFirstString("Bundle.entry.resource.where($this is Patient).identifier.where(system = 'urn:oid:2.16.840.1.113883.4.1').value");
            }
            set
            {
                Patient.Identifier.RemoveAll(iden => iden.System == "urn:oid:2.16.840.1.113883.4.1");
                Identifier ssn = new Identifier();
                ssn.System = "urn:oid:2.16.840.1.113883.4.1";
                ssn.Value = value;
                ssn.Type = new CodeableConcept();
                ssn.Type.Coding = new List<Coding>();
                ssn.Type.Coding.Add(new Coding(null, "BR", "Social Beneficiary Identifier"));
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
                dictionary.Add("placeOfBirthLine1", GetFirstString("Bundle.entry.resource.where($this is Patient).extension.where(url='http://hl7.org/fhir/StructureDefinition/birthPlace').value.line[0]"));
                dictionary.Add("placeOfBirthLine2", GetFirstString("Bundle.entry.resource.where($this is Patient).extension.where(url='http://hl7.org/fhir/StructureDefinition/birthPlace').value.line[1]"));
                dictionary.Add("placeOfBirthCity", GetFirstString("Bundle.entry.resource.where($this is Patient).extension.where(url='http://hl7.org/fhir/StructureDefinition/birthPlace').value.city"));
                dictionary.Add("placeOfBirthCounty", GetFirstString("Bundle.entry.resource.where($this is Patient).extension.where(url='http://hl7.org/fhir/StructureDefinition/birthPlace').value.district"));
                dictionary.Add("placeOfBirthState", GetFirstString("Bundle.entry.resource.where($this is Patient).extension.where(url='http://hl7.org/fhir/StructureDefinition/birthPlace').value.state"));
                dictionary.Add("placeOfBirthZip", GetFirstString("Bundle.entry.resource.where($this is Patient).extension.where(url='http://hl7.org/fhir/StructureDefinition/birthPlace').value.postalCode"));
                dictionary.Add("placeOfBirthCountry", GetFirstString("Bundle.entry.resource.where($this is Patient).extension.where(url='http://hl7.org/fhir/StructureDefinition/birthPlace').value.country"));

                return dictionary;
            }
            set
            {
                // Place Of Birth extension
                Patient.Extension.RemoveAll(ext => ext.Url == "http://hl7.org/fhir/StructureDefinition/birthPlace");
                Extension placeOfBirthExt = new Extension();
                placeOfBirthExt.Url = "http://hl7.org/fhir/StructureDefinition/birthPlace";

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
        /// <para>"placeOfDeathPhysicalTypeCode" - place of death type (physical), code</para>
        /// <para>"placeOfDeathPhysicalTypeSystem" - place of death type (physical), code system</para>
        /// <para>"placeOfDeathPhysicalTypeDisplay" - place of death type (physical), code display</para>
        /// <para>"placeOfDeathName" - place of death name</para>
        /// <para>"placeOfDeathDescription" - place of death description</para>
        /// <para>"placeOfDeathLine1" - location of death, line one</para>
        /// <para>"placeOfDeathLine2" - location of death, line two</para>
        /// <para>"placeOfDeathCity" - location of death, city</para>
        /// <para>"placeOfDeathCounty" - location of death, county</para>
        /// <para>"placeOfDeathState" - location of death, state</para>
        /// <para>"placeOfDeathZip" - location of death, zip</para>
        /// <para>"placeOfDeathCountry" - location of death, country</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; placeOfDeath = new Dictionary&lt;string, string&gt;();</para>
        /// <para>placeOfDeath.Add("placeOfDeathTypeCode", "HOSP");</para>
        /// <para>placeOfDeath.Add("placeOfDeathTypeSystem", "http://hl7.org/fhir/ValueSet/v3-ServiceDeliveryLocationRoleType");</para>
        /// <para>placeOfDeath.Add("placeOfDeathTypeDisplay", "Hospital");</para>
        /// <para>placeOfDeath.Add("placeOfDeathPhysicalTypeCode", "wa");</para>
        /// <para>placeOfDeath.Add("placeOfDeathPhysicalTypeSystem", "http://hl7.org/fhir/ValueSet/location-physical-type");</para>
        /// <para>placeOfDeath.Add("placeOfDeathPhysicalTypeDisplay", "Ward");</para>
        /// <para>placeOfDeath.Add("placeOfDeathName", "Example Hospital");</para>
        /// <para>placeOfDeath.Add("placeOfDeathDescription", "Example Hospital Wing B");</para>
        /// <para>placeOfDeath.Add("placeOfDeathLine1", "8 Example Street");</para>
        /// <para>placeOfDeath.Add("placeOfDeathLine2", "Line 2");</para>
        /// <para>placeOfDeath.Add("placeOfDeathCity", "Bedford");</para>
        /// <para>placeOfDeath.Add("placeOfDeathCounty", "Middlesex");</para>
        /// <para>placeOfDeath.Add("placeOfDeathState", "Massachusetts");</para>
        /// <para>placeOfDeath.Add("placeOfDeathZip", "01730");</para>
        /// <para>placeOfDeath.Add("placeOfDeathCountry", "United States");</para>
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

                if (DeathLocation != null)
                {
                    // Place Of Death Type
                    CodeableConcept typeCC = DeathLocation.Type;
                    if (typeCC != null)
                    {
                        Coding typeC = typeCC.Coding.FirstOrDefault();
                        if (typeC != null)
                        {
                            dictionary.Add("placeOfDeathTypeCode", typeC.Code);
                            dictionary.Add("placeOfDeathTypeSystem", typeC.System);
                            dictionary.Add("placeOfDeathTypeDisplay", typeC.Display);
                        }
                    }

                    // Place Of Death Type (Physical)
                    CodeableConcept ptypeCC = DeathLocation.PhysicalType;
                    if (ptypeCC != null)
                    {
                        Coding ptypeC = ptypeCC.Coding.FirstOrDefault();
                        if (ptypeC != null)
                        {
                            dictionary.Add("placeOfDeathPhysicalTypeCode", ptypeC.Code);
                            dictionary.Add("placeOfDeathPhysicalTypeSystem", ptypeC.System);
                            dictionary.Add("placeOfDeathPhysicalTypeDisplay", ptypeC.Display);
                        }
                    }

                    // Place Of Death Name and Description
                    dictionary.Add("placeOfDeathName", DeathLocation.Name);
                    dictionary.Add("placeOfDeathDescription", DeathLocation.Description);

                    // Place Of Death Address
                    if (DeathLocation.Address.Line.FirstOrDefault() != null)
                    {
                        dictionary.Add("placeOfDeathLine1", DeathLocation.Address.Line.FirstOrDefault());
                    }
                    if (DeathLocation.Address.Line.Count() > 1)
                    {
                        dictionary.Add("placeOfDeathLine2", DeathLocation.Address.Line.LastOrDefault());
                    }
                    dictionary.Add("placeOfDeathCity", DeathLocation.Address.City);
                    dictionary.Add("placeOfDeathCounty", DeathLocation.Address.District);
                    dictionary.Add("placeOfDeathState", DeathLocation.Address.State);
                    dictionary.Add("placeOfDeathZip", DeathLocation.Address.PostalCode);
                    dictionary.Add("placeOfDeathCountry", DeathLocation.Address.Country);
                }

                return dictionary;
            }
            set
            {
                if (DeathLocation == null)
                {
                    DeathLocation = new Location();
                    DeathLocation.Id = "urn:uuid:" + Guid.NewGuid().ToString();

                    // Add Location to Bundle
                    AddReferenceToComposition(DeathLocation.Id);
                    Bundle.AddResourceEntry(DeathLocation, DeathLocation.Id);

                    // Add reference in DeathDate to Location
                    Extension ext = new Extension();
                    ext.Url = "http://hl7.org/fhir/us/vrdr/StructureDefinition/Patient-Location";
                    ext.Value = new ResourceReference(DeathLocation.Id);
                    DeathDate.Extension.Add(ext);
                }

                // Add type to Location
                CodeableConcept typeCC = new CodeableConcept();
                Coding typeC = new Coding();
                typeC.Code = GetValue(value, "placeOfDeathTypeCode");
                typeC.System = GetValue(value, "placeOfDeathTypeSystem");
                typeC.Display = GetValue(value, "placeOfDeathTypeDisplay");
                typeCC.Coding.Add(typeC);
                DeathLocation.Type = typeCC;

                // Add type (physical) to Location
                CodeableConcept ptypeCC = new CodeableConcept();
                Coding ptypeC = new Coding();
                ptypeC.Code = GetValue(value, "placeOfDeathPhysicalTypeCode");
                ptypeC.System = GetValue(value, "placeOfDeathPhysicalTypeSystem");
                ptypeC.Display = GetValue(value, "placeOfDeathPhysicalTypeDisplay");
                ptypeCC.Coding.Add(ptypeC);
                DeathLocation.PhysicalType = ptypeCC;

                // Add Address to Location
                Address address = new Address();
                string[] lines = {GetValue(value, "placeOfDeathLine1"), GetValue(value, "placeOfDeathLine2")};
                address.Line = lines.ToArray();
                address.City = GetValue(value, "placeOfDeathCity");
                address.District = GetValue(value, "placeOfDeathCounty");
                address.State = GetValue(value, "placeOfDeathState");
                address.PostalCode = GetValue(value, "placeOfDeathZip");
                address.Country = GetValue(value, "placeOfDeathCountry");
                DeathLocation.Address = address;

                // Add Name and Description
                DeathLocation.Name = GetValue(value, "placeOfDeathName");
                DeathLocation.Description = GetValue(value, "placeOfDeathDescription");
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
        /// <para>code.Add("system", "urn:oid:2.16.840.1.113883.4.642.3.28");</para>
        /// <para>code.Add("display", "Never Married");</para>
        /// <para>ExampleDeathRecord.MaritalStatus = code;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Marital status: {ExampleDeathRecord.MaritalStatus["display"]}");</para>
        /// </example>
        public Dictionary<string, string> MaritalStatus
        {
            get
            {
                return CodeableConceptToDict(Patient.MaritalStatus);
            }
            set
            {
                Patient.MaritalStatus = DictToCodeableConcept(value);
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
        /// <para>"funeralFacilityName" - the name of a funeral facility or institution</para>
        /// <para>"funeralFacilityLine1" - funeral facility address, line one</para>
        /// <para>"funeralFacilityLine2" - funeral facility address, line two</para>
        /// <para>"funeralFacilityCity" - funeral facility address, city</para>
        /// <para>"funeralFacilityCounty" - funeral facility address, county</para>
        /// <para>"funeralFacilityState" - funeral facility address, state</para>
        /// <para>"funeralFacilityZip" - funeral facility address, zip</para>
        /// <para>"funeralFacilityCountry" - funeral facility address, country</para>
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
        /// <para>disposition.Add("funeralFacilityName", "Example funeral facility name");</para>
        /// <para>disposition.Add("funeralFacilityLine1", "50 Example Street");</para>
        /// <para>disposition.Add("funeralFacilityLine2", "Line 2");</para>
        /// <para>disposition.Add("funeralFacilityCity", "Bedford");</para>
        /// <para>disposition.Add("funeralFacilityCounty", "Middlesex");</para>
        /// <para>disposition.Add("funeralFacilityState", "Massachusetts");</para>
        /// <para>disposition.Add("funeralFacilityZip", "01730");</para>
        /// <para>disposition.Add("funeralFacilityCountry", "United States");</para>
        /// <para>ExampleDeathRecord.Disposition = disposition;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Funeral Facility name: {ExampleDeathRecord.Disposition["funeralFacilityName"]}");</para>
        /// </example>
        public Dictionary<string, string> Disposition
        {
            get
            {
                Dictionary<string, string> dictionary = new Dictionary<string, string>();

                // Disposition Type
                if (DispositionMethod != null)
                {
                    CodeableConcept typeCC = (CodeableConcept)DispositionMethod.Value;
                    if (typeCC != null)
                    {
                        Coding typeC = typeCC.Coding.FirstOrDefault();
                        if (typeC != null)
                        {
                            dictionary.Add("dispositionTypeCode", typeC.Code);
                            dictionary.Add("dispositionTypeSystem", typeC.System);
                            dictionary.Add("dispositionTypeDisplay", typeC.Display);
                        }
                    }
                }

                // Disposition Location
                if (DispositionLocation != null)
                {
                    dictionary.Add("dispositionPlaceName", DispositionLocation.Name);
                    if (DispositionLocation.Address != null)
                    {
                        if (DispositionLocation.Address.Line.FirstOrDefault() != null)
                        {
                            dictionary.Add("dispositionPlaceLine1", DispositionLocation.Address.Line.FirstOrDefault());
                        }
                        if (DispositionLocation.Address.Line.Count() > 1)
                        {
                            dictionary.Add("dispositionPlaceLine2", DispositionLocation.Address.Line.LastOrDefault());
                        }
                        dictionary.Add("dispositionPlaceCity", DispositionLocation.Address.City);
                        dictionary.Add("dispositionPlaceCounty", DispositionLocation.Address.District);
                        dictionary.Add("dispositionPlaceState", DispositionLocation.Address.State);
                        dictionary.Add("dispositionPlaceZip", DispositionLocation.Address.PostalCode);
                        dictionary.Add("dispositionPlaceCountry", DispositionLocation.Address.Country);
                    }
                }

                // Funeral Facility
                if (FuneralHome != null)
                {
                    dictionary.Add("funeralFacilityName", FuneralHome.Name);
                    Address funeralHomeAddress = FuneralHome.Address.FirstOrDefault();
                    if (funeralHomeAddress != null)
                    {
                        if (funeralHomeAddress.Line.FirstOrDefault() != null)
                        {
                            dictionary.Add("funeralFacilityLine1", funeralHomeAddress.Line.FirstOrDefault());
                        }
                        if (funeralHomeAddress.Line.Count() > 1)
                        {
                            dictionary.Add("funeralFacilityLine2", funeralHomeAddress.Line.LastOrDefault());
                        }
                        dictionary.Add("funeralFacilityCity", funeralHomeAddress.City);
                        dictionary.Add("funeralFacilityCounty", funeralHomeAddress.District);
                        dictionary.Add("funeralFacilityState", funeralHomeAddress.State);
                        dictionary.Add("funeralFacilityZip", funeralHomeAddress.PostalCode);
                        dictionary.Add("funeralFacilityCountry", funeralHomeAddress.Country);
                    }
                }

                return dictionary;
            }
            set
            {
                // Disposition Method

                if (DispositionMethod == null)
                {
                    DispositionMethod = AddObservation("http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Decedent-Disposition-Method", "80905-3", "urn:oid:2.16.840.1.113883.6.1", "Body disposition method");
                }

                // Add type
                CodeableConcept dispositionMethodTypeCodeableConcept = new CodeableConcept();
                Coding dispositionMethodTypeCoding = new Coding();
                dispositionMethodTypeCoding.Code = GetValue(value, "dispositionTypeCode");
                dispositionMethodTypeCoding.System = GetValue(value, "dispositionTypeSystem");
                dispositionMethodTypeCoding.Display = GetValue(value, "dispositionTypeDisplay");
                dispositionMethodTypeCodeableConcept.Coding.Add(dispositionMethodTypeCoding);
                DispositionMethod.Value = dispositionMethodTypeCodeableConcept;

                // Disposition Location

                if (DispositionLocation == null)
                {
                    DispositionLocation = new Location();
                    DispositionLocation.Id = "urn:uuid:" + Guid.NewGuid().ToString();

                    // Add profile to DispositionLocation
                    DispositionLocation.Meta = new Meta();
                    string[] disposition_location_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Disposition-Location" };
                    DispositionLocation.Meta.Profile = disposition_location_profile;

                    // Add Location to Bundle
                    AddReferenceToComposition(DispositionLocation.Id);
                    Bundle.AddResourceEntry(DispositionLocation, DispositionLocation.Id);
                }

                // Add name
                DispositionLocation.Name = GetValue(value, "dispositionPlaceName");

                // Add address
                Address dispositionPlaceAddress = new Address();
                string[] lines = {GetValue(value, "dispositionPlaceLine1"), GetValue(value, "dispositionPlaceLine2")};
                dispositionPlaceAddress.Line = lines.ToArray();
                dispositionPlaceAddress.City = GetValue(value, "dispositionPlaceCity");
                dispositionPlaceAddress.District = GetValue(value, "dispositionPlaceCounty");
                dispositionPlaceAddress.State = GetValue(value, "dispositionPlaceState");
                dispositionPlaceAddress.PostalCode = GetValue(value, "dispositionPlaceZip");
                dispositionPlaceAddress.Country = GetValue(value, "dispositionPlaceCountry");
                dispositionPlaceAddress.Type = Hl7.Fhir.Model.Address.AddressType.Postal;
                DispositionLocation.Address = dispositionPlaceAddress;

                // Add reference in DispositionMethod to DispositionLocation
                Extension ext = new Extension();
                ext.Url = "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Disposition-Location";
                ext.Value = new ResourceReference(DispositionLocation.Id);
                DispositionMethod.Extension.Clear();
                DispositionMethod.Extension.Add(ext);

                // Mortician

                if (Mortician == null)
                {
                    Mortician = new Practitioner();
                    Mortician.Id = "urn:uuid:" + Guid.NewGuid().ToString();

                    // Add profile to Mortician
                    Mortician.Meta = new Meta();
                    string[] mortician_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Mortician" };
                    Mortician.Meta.Profile = mortician_profile;

                    // Add Mortician to Bundle
                    AddReferenceToComposition(Mortician.Id);
                    Bundle.AddResourceEntry(Mortician, Mortician.Id);
                }
                ResourceReference[] mperformer = { new ResourceReference(Mortician.Id) };
                DispositionMethod.Performer = mperformer.ToList();

                // Funeral Home

                if (FuneralHome == null)
                {
                    FuneralHome = new Organization();
                    FuneralHome.Id = "urn:uuid:" + Guid.NewGuid().ToString();

                    // Add profile to Funeral Home
                    FuneralHome.Meta = new Meta();
                    string[] funeral_home_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Funeral-Home" };
                    FuneralHome.Meta.Profile = funeral_home_profile;

                    // Add Location to Bundle
                    AddReferenceToComposition(FuneralHome.Id);
                    Bundle.AddResourceEntry(FuneralHome, FuneralHome.Id);
                }

                // Add name
                FuneralHome.Name = GetValue(value, "funeralFacilityName");

                // Add type
                CodeableConcept funeralHomeTypeCodeableConcept = new CodeableConcept();
                Coding funeralHomeTypeCoding = new Coding();
                funeralHomeTypeCoding.Code = "bus";
                funeralHomeTypeCoding.Display = "Non-Healthcare Business or Corporation";
                funeralHomeTypeCodeableConcept.Coding.Add(funeralHomeTypeCoding);
                FuneralHome.Type.Clear();
                FuneralHome.Type.Add(funeralHomeTypeCodeableConcept);

                // Add address
                Address funeralFacilityAddress = new Address();
                string[] fflines = {GetValue(value, "funeralFacilityLine1"), GetValue(value, "funeralFacilityLine2")};
                funeralFacilityAddress.Line = fflines.ToArray();
                funeralFacilityAddress.City = GetValue(value, "funeralFacilityCity");
                funeralFacilityAddress.District = GetValue(value, "funeralFacilityCounty");
                funeralFacilityAddress.State = GetValue(value, "funeralFacilityState");
                funeralFacilityAddress.PostalCode = GetValue(value, "funeralFacilityZip");
                funeralFacilityAddress.Country = GetValue(value, "funeralFacilityCountry");
                funeralFacilityAddress.Type = Hl7.Fhir.Model.Address.AddressType.Postal;
                FuneralHome.Address.Clear();
                FuneralHome.Address.Add(funeralFacilityAddress);

                // Funeral Home Director

                if (FuneralHomeDirector == null)
                {
                    FuneralHomeDirector = new PractitionerRole();
                    FuneralHomeDirector.Id = "urn:uuid:" + Guid.NewGuid().ToString();

                    // Add profile to Funeral Home Director
                    FuneralHomeDirector.Meta = new Meta();
                    string[] funeral_home_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Funeral-Home-Director" };
                    FuneralHomeDirector.Meta.Profile = funeral_home_profile;

                    // Add Funeral Home Director to Bundle
                    AddReferenceToComposition(FuneralHomeDirector.Id);
                    Bundle.AddResourceEntry(FuneralHomeDirector, FuneralHomeDirector.Id);
                }

                // Add mortician and funeral home references
                FuneralHomeDirector.Practitioner = new ResourceReference(Mortician.Id);
                FuneralHomeDirector.Organization = new ResourceReference(FuneralHome.Id);
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
        /// <para>code.Add("system", "http://github.com/nightingaleproject/fhirDeathRecord/sdr/decedent/cs/EducationCS");</para>
        /// <para>code.Add("display", "Bachelor's Degree");</para>
        /// <para>ExampleDeathRecord.MaritalStatus = code;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Degree: {ExampleDeathRecord.Education["display"]}");</para>
        /// </example>
        public Dictionary<string, string> Education
        {
            get
            {
                if (EducationLevel != null)
                {
                    return CodeableConceptToDict((CodeableConcept)EducationLevel.Value);
                }
                else
                {
                    return new Dictionary<string, string>();
                }
            }
            set
            {
                EducationLevel = AddObservation("http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Decedent-Education-Level",
                                                "80913-7",
                                                null,
                                                null);
                EducationLevel.Value = DictToCodeableConcept(value);
            }
        }

        /// <summary>The decedent’s age in years at last birthday. Corresponds to item 4a of the U.S. Standard Certificate of Death.</summary>
        /// <value>The decedent’s age in years at last birthday.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; age = new Dictionary&lt;string, string&gt;();</para>
        /// <para>code.Add("value", "70");</para>
        /// <para>code.Add("unit", "a"); // See: http://hl7.org/fhir/STU3/valueset-age-units.html</para>
        /// <para>ExampleDeathRecord.Age = age;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Age: {ExampleDeathRecord.Age}");</para>
        /// </example>
        public Dictionary<string, string> Age
        {
            get
            {
                string value = GetFirstString("Bundle.entry.resource.where($this is Observation).where(code.coding.code='30525-0').value.value");
                string unit = GetFirstString("Bundle.entry.resource.where($this is Observation).where(code.coding.code='30525-0').value.unit");
                Dictionary<string, string> age = new Dictionary<string, string>();
                age.Add("value", value);
                age.Add("unit", unit);
                return age;
            }
            set
            {
                int n;
                bool isNumeric = int.TryParse(GetValue(value, "value"), out n);
                if (isNumeric)
                {
                    Observation age = AddObservation("http://hl7.org/fhir/us/vrdr/decedentAge", "30525-0", "urn:oid:2.16.840.1.113883.6.1", "AGE");
                    Quantity quant = new Quantity();
                    quant.Value = n;
                    quant.Unit = GetValue(value, "unit");
                    age.Value = quant;

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
        /// TODO
        public Dictionary<string, string> Occupation
        {
            get
            {
                if (EmploymentHistory != null)
                {
                    return new Dictionary<string, string>();//CodeableConceptToDict((CodeableConcept)EmploymentHistory.Value);
                }
                else
                {
                    return new Dictionary<string, string>();
                }
            }
            set
            {
                EmploymentHistory = AddObservation(" http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Decedent-Employment-History",
                                                "74165-2",
                                                "http://loinc.org",
                                                null);
                Observation.ComponentComponent component1 = new Observation.ComponentComponent();
                component1.Code = new CodeableConcept("http://loinc.org", "55280-2");
                component1.Value = new CodeableConcept("http://hl7.org/fhir/ValueSet/v2-0532", "Y", "Yes");
                EmploymentHistory.Component.Add(component1);
                Observation.ComponentComponent component2 = new Observation.ComponentComponent();
                component2.Code = new CodeableConcept("http://loinc.org", "21844-6");
                component2.Value = new CodeableConcept("http://hl7.org/fhir/ValueSet/industry-cdc-census-2010", "1320", "Aerospace engineers");
                EmploymentHistory.Component.Add(component2);
                Observation.ComponentComponent component3 = new Observation.ComponentComponent();
                component3.Code = new CodeableConcept("http://loinc.org", "21847-9");
                component3.Value = new CodeableConcept("http://hl7.org/fhir/ValueSet/Usual-occupation", "7280", "Accounting, tax preparation, bookkeeping, and payroll services");
                EmploymentHistory.Component.Add(component3);
                Bundle.AddResourceEntry(EmploymentHistory, EmploymentHistory.Id);
            }
        }

        /////////////////////////////////////////////////////////////////////////////////
        //
        // Class Properties related to the Mortician (Practitioner).
        //
        /////////////////////////////////////////////////////////////////////////////////

        /// <summary>Given name(s) of Mortician.</summary>
        /// <value>the Mortician's name (first, middle, etc.)</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>string[] names = {"Mortician", "Middle"};</para>
        /// <para>ExampleDeathRecord.MorticianGivenNames = names;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Mortician Given Name(s): {string.Join(", ", ExampleDeathRecord.MorticianGivenNames)}");</para>
        /// </example>
        public string[] MorticianGivenNames
        {
            get
            {
                if (Mortician != null && Mortician.Name.FirstOrDefault() != null)
                {
                    HumanName name = Mortician.Name.FirstOrDefault();
                    return name.Given.ToArray();
                }
                return new string[] {};
            }
            set
            {
                if (Mortician == null)
                {
                    Mortician = new Practitioner();
                    Mortician.Id = "urn:uuid:" + Guid.NewGuid().ToString();

                    // Add profile to Mortician
                    Mortician.Meta = new Meta();
                    string[] mortician_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Mortician" };
                    Mortician.Meta.Profile = mortician_profile;

                    // Add Mortician to Bundle
                    AddReferenceToComposition(Mortician.Id);
                    Bundle.AddResourceEntry(Mortician, Mortician.Id);
                }
                HumanName name = Mortician.Name.FirstOrDefault();
                if (name != null)
                {
                    name.Given = value;
                }
                else
                {
                    name = new HumanName();
                    name.Use = HumanName.NameUse.Official;
                    name.Given = value;
                    Mortician.Name.Add(name);
                }
            }
        }

        /// <summary>Mortician's First Name. This is essentially the same as the first thing in
        /// <c>MorticianGivenNames</c>. Setting this value will prepend whatever is given to the start
        /// of <c>MorticianGivenNames</c> if <c>MorticianGivenNames</c> already exists.</summary>
        /// <value>the Mortician's first name</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.MorticianFirstName = "Example";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Mortician First Name: {ExampleDeathRecord.MorticianFirstName}");</para>
        /// </example>
        public string MorticianFirstName
        {
            get
            {
                if (Mortician != null && Mortician.Name.FirstOrDefault() != null)
                {
                    HumanName name = Mortician.Name.FirstOrDefault();
                    return name.Given.FirstOrDefault();
                }
                return "";
            }
            set
            {
                if (Mortician == null)
                {
                    Mortician = new Practitioner();
                    Mortician.Id = "urn:uuid:" + Guid.NewGuid().ToString();

                    // Add profile to Mortician
                    Mortician.Meta = new Meta();
                    string[] mortician_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Mortician" };
                    Mortician.Meta.Profile = mortician_profile;

                    // Add Mortician to Bundle
                    AddReferenceToComposition(Mortician.Id);
                    Bundle.AddResourceEntry(Mortician, Mortician.Id);
                }
                HumanName name = Mortician.Name.FirstOrDefault(); // Check if there is already a HumanName on the Mortician.
                if (name != null && !String.IsNullOrEmpty(value))
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
                else if (!String.IsNullOrEmpty(value))
                {
                    name = new HumanName();
                    name.Use = HumanName.NameUse.Official;
                    name.Given = new String[] {value, ""}; // Put an empty "fake" middle name at the end.
                    Mortician.Name.Add(name);
                }
            }
        }

        /// <summary>Mortician's Middle Name. This is essentially the same as the last thing in
        /// <c>MorticianGivenNames</c>. Setting this value will append whatever is given to the end
        /// of <c>MorticianGivenNames</c> if <c>MorticianGivenNames</c> already exists.</summary>
        /// <value>the certifier's middle name</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.MorticianMiddleName = "Middle";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Mortician Middle Name: {ExampleDeathRecord.MorticianMiddleName}");</para>
        /// </example>
        public string MorticianMiddleName
        {
            get
            {
                if (Mortician != null && Mortician.Name.LastOrDefault() != null)
                {
                    HumanName name = Mortician.Name.LastOrDefault();
                    return name.Given.LastOrDefault();
                }
                return "";
            }
            set
            {
                if (Mortician == null)
                {
                    Mortician = new Practitioner();
                    Mortician.Id = "urn:uuid:" + Guid.NewGuid().ToString();

                    // Add profile to Mortician
                    Mortician.Meta = new Meta();
                    string[] mortician_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Mortician" };
                    Mortician.Meta.Profile = mortician_profile;

                    // Add Mortician to Bundle
                    AddReferenceToComposition(Mortician.Id);
                    Bundle.AddResourceEntry(Mortician, Mortician.Id);
                }
                HumanName name = Mortician.Name.FirstOrDefault(); // Check if there is already a HumanName on the Certifier.
                if (name != null && !String.IsNullOrEmpty(value))
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
                else if (!String.IsNullOrEmpty(value))
                {
                    name = new HumanName();
                    name.Use = HumanName.NameUse.Official;
                    name.Given = new String[] {"", value}; // Put an empty "fake" first name at the start.
                    Mortician.Name.Add(name);
                }
            }
        }

        /// <summary>Family name of Mortician.</summary>
        /// <value>the Mortician's family name (i.e. last name)</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.MorticianFamilyName = "Last";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Mortician's Last Name: {string.Join(", ", ExampleDeathRecord.MorticianFamilyName)}");</para>
        /// </example>
        public string MorticianFamilyName
        {
            get
            {
                if (Mortician != null && Mortician.Name.FirstOrDefault() != null)
                {
                    HumanName name = Mortician.Name.FirstOrDefault();
                    return name.Family;
                }
                return "";
            }
            set
            {
                if (Mortician == null)
                {
                    Mortician = new Practitioner();
                    Mortician.Id = "urn:uuid:" + Guid.NewGuid().ToString();

                    // Add profile to Mortician
                    Mortician.Meta = new Meta();
                    string[] mortician_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Mortician" };
                    Mortician.Meta.Profile = mortician_profile;

                    // Add Mortician to Bundle
                    AddReferenceToComposition(Mortician.Id);
                    Bundle.AddResourceEntry(Mortician, Mortician.Id);
                }
                HumanName name = Mortician.Name.FirstOrDefault();
                if (name != null)
                {
                    name.Family = value;
                }
                else
                {
                    name = new HumanName();
                    name.Use = HumanName.NameUse.Official;
                    name.Family = value;
                    Mortician.Name.Add(name);
                }
            }
        }

        /// <summary>Mortician's Suffix.</summary>
        /// <value>the Mortician's suffix</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.MorticianSuffix = "Jr.";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Mortician Suffix: {ExampleDeathRecord.MorticianSuffix}");</para>
        /// </example>
        public string MorticianSuffix
        {
            get
            {
                if (Mortician != null && Mortician.Name.FirstOrDefault() != null)
                {
                    HumanName name = Mortician.Name.FirstOrDefault();
                    return name.Suffix.FirstOrDefault();
                }
                return "";
            }
            set
            {
                if (Mortician == null)
                {
                    Mortician = new Practitioner();
                    Mortician.Id = "urn:uuid:" + Guid.NewGuid().ToString();

                    // Add profile to Mortician
                    Mortician.Meta = new Meta();
                    string[] mortician_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Mortician" };
                    Mortician.Meta.Profile = mortician_profile;

                    // Add Mortician to Bundle
                    AddReferenceToComposition(Mortician.Id);
                    Bundle.AddResourceEntry(Mortician, Mortician.Id);
                }
                HumanName name = Mortician.Name.FirstOrDefault();
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
                    Mortician.Name.Add(name);
                }
            }
        }

        /// <summary>Mortician Identification.</summary>
        /// <value>the Mortician identification</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.MorticianId = "123456789";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"\Mortician Id: {ExampleDeathRecord.MorticianId}");</para>
        /// </example>
        public string MorticianId
        {
            get
            {
                if (Mortician != null && Mortician.Qualification.FirstOrDefault() != null)
                {
                    Practitioner.QualificationComponent qual = Mortician.Qualification.FirstOrDefault();
                    if (qual == null)
                    {
                        return null;
                    }
                    Identifier ident = qual.Identifier.FirstOrDefault();
                    if (ident == null)
                    {
                        return null;
                    }
                    return ident.Value;
                }
                return "";
            }
            set
            {
                if (Mortician == null)
                {
                    Mortician = new Practitioner();
                    Mortician.Id = "urn:uuid:" + Guid.NewGuid().ToString();

                    // Add profile to Mortician
                    Mortician.Meta = new Meta();
                    string[] mortician_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Mortician" };
                    Mortician.Meta.Profile = mortician_profile;

                    // Add Mortician to Bundle
                    AddReferenceToComposition(Mortician.Id);
                    Bundle.AddResourceEntry(Mortician, Mortician.Id);
                }
                Practitioner.QualificationComponent qualification = new Practitioner.QualificationComponent();
                Identifier ident = new Identifier();
                ident.Value = value;
                Identifier[] idents = {ident};
                qualification.Identifier = idents.ToList();
                Practitioner.QualificationComponent[] quals = {qualification};
                Mortician.Qualification = quals.ToList();
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

        /// <summary>Certifier's First Name. This is essentially the same as the first thing in
        /// <c>CertifierGivenNames</c>. Setting this value will prepend whatever is given to the start
        /// of <c>CertifierGivenNames</c> if <c>CertifierGivenNames</c> already exists.</summary>
        /// <value>the certifier's first name</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.CertifierFirstName = "Example";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Certifier First Name: {ExampleDeathRecord.CertifierFirstName}");</para>
        /// </example>
        public string CertifierFirstName
        {
            get
            {
                return GetFirstString("Bundle.entry.resource.where($this is Practitioner).name.given");
            }
            set
            {
                HumanName name = Practitioner.Name.FirstOrDefault(); // Check if there is already a HumanName on the Certifier.
                if (name != null && !String.IsNullOrEmpty(value))
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
                else if (!String.IsNullOrEmpty(value))
                {
                    name = new HumanName();
                    name.Use = HumanName.NameUse.Official;
                    name.Given = new String[] {value, ""}; // Put an empty "fake" middle name at the end.
                    Practitioner.Name.Add(name);
                }
            }
        }

        /// <summary>Certifier's Middle Name. This is essentially the same as the last thing in
        /// <c>CertifierGivenNames</c>. Setting this value will append whatever is given to the end
        /// of <c>CertifierGivenNames</c> if <c>CertifierGivenNames</c> already exists.</summary>
        /// <value>the certifier's middle name</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.CertifierMiddleName = "Middle";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Certifier Middle Name: {ExampleDeathRecord.CertifierMiddleName}");</para>
        /// </example>
        public string CertifierMiddleName
        {
            get
            {
                return GetLastString("Bundle.entry.resource.where($this is Practitioner).name.given");
            }
            set
            {
                HumanName name = Practitioner.Name.FirstOrDefault(); // Check if there is already a HumanName on the Certifier.
                if (name != null && !String.IsNullOrEmpty(value))
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
                else if (!String.IsNullOrEmpty(value))
                {
                    name = new HumanName();
                    name.Use = HumanName.NameUse.Official;
                    name.Given = new String[] {"", value}; // Put an empty "fake" first name at the start.
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
        /// <para>address.Add("certifierAddressStreet", "123 Test Street");</para>
        /// <para>address.Add("certifierAddressCity", "Boston");</para>
        /// <para>address.Add("certifierAddressCounty", "Suffolk");</para>
        /// <para>address.Add("certifierAddressState", "Massachusetts");</para>
        /// <para>address.Add("certifierAddressZip", "12345");</para>
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
                string county = GetFirstString("Bundle.entry.resource.where($this is Practitioner).address.district");
                string state = GetFirstString("Bundle.entry.resource.where($this is Practitioner).address.state");
                string zip = GetFirstString("Bundle.entry.resource.where($this is Practitioner).address.postalCode");
                Dictionary<string, string> dictionary = new Dictionary<string, string>();
                dictionary.Add("certifierAddressStreet", street);
                dictionary.Add("certifierAddressCity", city);
                dictionary.Add("certifierAddressCounty", county);
                dictionary.Add("certifierAddressState", state);
                dictionary.Add("certifierAddressZip", zip);
                return dictionary;
            }
            set
            {
                Address address = new Address();
                address.LineElement.Add(new FhirString(GetValue(value, "certifierAddressStreet")));
                address.City = GetValue(value, "certifierAddressCity");
                address.District = GetValue(value, "certifierAddressCounty");
                address.State = GetValue(value, "certifierAddressState");
                address.PostalCodeElement = new FhirString(GetValue(value, "certifierAddressZip"));
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
        /// <para>Console.WriteLine($"\tCertifier Type: {ExampleDeathRecord.CertifierType["display"]}");</para>
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

        /// <summary>Certifier Identification.</summary>
        /// <value>the certifier identification</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.CertifierId = "123456789";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"\tCertifier Id: {ExampleDeathRecord.CertifierId}");</para>
        /// </example>
        public string CertifierId
        {
            get
            {
                Practitioner.QualificationComponent qual = Practitioner.Qualification.FirstOrDefault();
                if (qual == null)
                {
                    return "";
                }
                Identifier ident = qual.Identifier.FirstOrDefault();
                if (ident == null)
                {
                    return "";
                }
                return ident.Value;
            }
            set
            {
                Practitioner.QualificationComponent qualification = Practitioner.Qualification.FirstOrDefault();
                if (qualification == null)
                {
                    qualification = new Practitioner.QualificationComponent();
                }
                Identifier ident = new Identifier();
                ident.Value = value;
                Identifier[] idents = {ident};
                qualification.Identifier = idents.ToList();
                Practitioner.QualificationComponent[] quals = {qualification};
                Practitioner.Qualification = quals.ToList();
            }
        }

        /// <summary>Certifier Qualification.</summary>
        /// <value>the certifier qualification</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; qualification = new Dictionary&lt;string, string&gt;();</para>
        /// <para>code.Add("code", "434651000124107");</para>
        /// <para>code.Add("system", "434651000124107");</para>
        /// <para>code.Add("display", "Physician (Pronouncer and Certifier)");</para>
        /// <para>ExampleDeathRecord.CertifierQualification = qualification;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"\tCertifier Type: {ExampleDeathRecord.CertifierQualification["display"]}");</para>
        /// </example>
        public Dictionary<string, string> CertifierQualification
        {
            get
            {
                Practitioner.QualificationComponent qual = Practitioner.Qualification.FirstOrDefault();
                if (qual == null)
                {
                    return null;
                }
                return CodeableConceptToDict(qual.Code);
            }
            set
            {

                Practitioner.QualificationComponent qualification = Practitioner.Qualification.FirstOrDefault();
                if (qualification == null)
                {
                    qualification = new Practitioner.QualificationComponent();
                }
                qualification.Code = DictToCodeableConcept(value);
                Practitioner.QualificationComponent[] quals = {qualification};
                Practitioner.Qualification = quals.ToList();
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
                    }
                    results.Add(Tuple.Create(literal, onset, code));
                }

                return results.ToArray();
            }
            set
            {
                // Remove all existing Causes
                RemoveCauseConditions();

                // Create List that will describe the ordering of the Cause Conditions
                if (CauseOfDeathPathway != null)
                {
                    RemoveReferenceFromComposition(CauseOfDeathPathway.Id);
                }
                CauseOfDeathPathway = new Hl7.Fhir.Model.List();
                CauseOfDeathPathway.Id = "urn:uuid:" + Guid.NewGuid().ToString();
                CauseOfDeathPathway.Meta = new Meta();
                string[] pathway_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Cause-of-Death-Pathway" };
                CauseOfDeathPathway.Meta.Profile = pathway_profile;
                CauseOfDeathPathway.Status = Hl7.Fhir.Model.List.ListStatus.Current;
                CauseOfDeathPathway.Mode = Hl7.Fhir.Model.ListMode.Snapshot;
                CauseOfDeathPathway.Source = new ResourceReference(Practitioner.Id);
                CodeableConcept priorities = new CodeableConcept();
                Coding priority = new Coding();
                priority.Code = "priority";
                priorities.Coding.Add(priority);
                CauseOfDeathPathway.OrderedBy = priorities;

                // Add CauseOfDeathPathway to Composition and Bundle
                AddReferenceToComposition(CauseOfDeathPathway.Id);
                Bundle.AddResourceEntry(CauseOfDeathPathway, CauseOfDeathPathway.Id);

                // Add new Conditions for causes
                foreach (Tuple<string, string, Dictionary<string, string>> cause in value)
                {
                    // Create a new Condition and populate it with the given details
                    Condition condition = new Condition();
                    condition.Id = "urn:uuid:" + Guid.NewGuid().ToString();
                    condition.Subject = new ResourceReference(Patient.Id);
                    condition.Meta = new Meta();
                    string[] condition_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/-VRDR-Cause-Of-Death-Condition" };
                    condition.Meta.Profile = condition_profile;
                    condition.Asserter = new ResourceReference(Practitioner.Id);

                    Narrative narrative = new Narrative();
                    narrative.Div = HttpUtility.HtmlEncode($"<div xmlns='http://www.w3.org/1999/xhtml'>{cause.Item1}</div>");
                    narrative.Status = Narrative.NarrativeStatus.Additional;
                    condition.Text = narrative;

                    condition.Onset = new FhirString(cause.Item2);
                    condition.Code = DictToCodeableConcept(cause.Item3); // Optional, literal might not be coded yet.

                    // Add Condition to CauseOfDeathPathway
                    List.EntryComponent entry = new List.EntryComponent();
                    entry.Item = new ResourceReference(condition.Id);
                    CauseOfDeathPathway.Entry.Add(entry);

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
                    InsertCOD(Tuple.Create(value.Trim(), "", new Dictionary<string, string>()), 0);
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
                    InsertCOD(Tuple.Create("", value.Trim(), new Dictionary<string, string>()), 0);
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
                    InsertCOD(Tuple.Create("", "", value), 0);
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
                    InsertCOD(Tuple.Create(value.Trim(), "", new Dictionary<string, string>()), 1);
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
                    InsertCOD(Tuple.Create("", value.Trim(), new Dictionary<string, string>()), 1);
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
                    InsertCOD(Tuple.Create("", "", value), 1);
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
                    InsertCOD(Tuple.Create(value.Trim(), "", new Dictionary<string, string>()), 2);
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
                    InsertCOD(Tuple.Create("", value.Trim(), new Dictionary<string, string>()), 2);
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
                    InsertCOD(Tuple.Create("", "", value), 2);
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
                    InsertCOD(Tuple.Create(value.Trim(), "", new Dictionary<string, string>()), 3);
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
                    InsertCOD(Tuple.Create("", value.Trim(), new Dictionary<string, string>()), 3);
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
                    InsertCOD(Tuple.Create("", "", value), 3);
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
                string[] condition_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Condition-Contributintg-To-Death" };
                condition.Meta.Profile = condition_profile;
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
        /// <para>Dictionary&lt;string, string&gt; aperformed = new Dictionary&lt;string, string&gt;();</para>
        /// <para>code.Add("performedCode", "N");</para>
        /// <para>code.Add("performedSystem", "http://terminology.hl7.org/CodeSystem/v2-0136");</para>
        /// <para>code.Add("performedDisplay", "No");</para>
        /// <para>code.Add("resultsAvailableCode", "N");</para>
        /// <para>code.Add("resultsAvailableSystem", "http://terminology.hl7.org/CodeSystem/v2-0136");</para>
        /// <para>code.Add("resultsAvailableDisplay", "No");</para>
        /// <para>ExampleDeathRecord.AutopsyPerformedAndResultsAvailable = aperformed;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"\tWas autopsy performed?: {ExampleDeathRecord.AutopsyPerformedAndResultsAvailable["display"]}");</para>
        /// </example>
        public Dictionary<string, string> AutopsyPerformedAndResultsAvailable
        {
            get
            {
                string performedCode = GetFirstString("Bundle.entry.resource.where($this is Observation).where(code.coding.code='85699-7').value.coding.code");
                string performedSystem = GetFirstString("Bundle.entry.resource.where($this is Observation).where(code.coding.code='85699-7').value.coding.system");
                string performedDisplay = GetFirstString("Bundle.entry.resource.where($this is Observation).where(code.coding.code='85699-7').value.coding.display");
                Dictionary<string, string> dictionary = new Dictionary<string, string>(); // TODO! Need to grab embedded component here, or else keep a reference above
                dictionary.Add("performedCode", performedCode);
                dictionary.Add("performedSystem", performedSystem);
                dictionary.Add("performedDisplay", performedDisplay);
                return dictionary;
            }
            set
            {
                Observation observation = AddObservation("http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Autopsy-Performed-Indicator",
                                                         "85699-7",
                                                         "http://loinc.org",
                                                         "Autopsy was performed");
                Coding performedCode = new Coding();
                performedCode.Code = GetValue(value, "performedCode");
                performedCode.System = GetValue(value, "performedSystem");
                performedCode.Display = GetValue(value, "performedDisplay");
                CodeableConcept performedCodes = new CodeableConcept();
                performedCodes.Coding.Add(performedCode);
                observation.Value = performedCodes;
                Coding availableCode = new Coding();
                availableCode.Code = GetValue(value, "resultsAvailableCode");
                availableCode.System = GetValue(value, "resultsAvailableSystem");
                availableCode.Display = GetValue(value, "resultsAvailableDisplay");
                CodeableConcept availableCodes = new CodeableConcept();
                availableCodes.Coding.Add(availableCode);
                Observation.ComponentComponent component = new Observation.ComponentComponent();
                component.Value = availableCodes;
                Coding coding = new Coding();
                coding.Code = "69436-4";
                coding.System = "http://loinc.org";
                coding.Display = "Autopsy results available";
                CodeableConcept codings = new CodeableConcept();
                codings.Coding.Add(coding);
                component.Code = codings;
                observation.Component.Add(component);
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
                Observation observation = AddObservation("http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Manner-of-Death",
                                                         "69449-7",
                                                         "http://loinc.org",
                                                         "Manner of death");
                observation.Performer.Add(new ResourceReference(Practitioner.Id));
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
                Observation observation = AddObservation("http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Tobacco-Use-Contributed-To-Death",
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
                return DeathDate.Effective.ToString();
            }
            set
            {
                DeathDate.Effective = new FhirDateTime(value);
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
                Observation.ComponentComponent component = DeathDate.Component.FirstOrDefault();
                if (component != null)
                {
                    return component.Value.ToString();
                }
                return null;
            }
            set
            {
                Observation.ComponentComponent component = new Observation.ComponentComponent();
                component.Code = new CodeableConcept();
                Coding coding = new Coding();
                coding.Code = "80616-6";
                coding.System = "urn:oid:2.16.840.1.113883.6.1";
                coding.Display = "Date and time pronounced dead";
                component.Code.Coding.Add(coding);
                component.Value = new FhirDateTime(value);
                Observation.ComponentComponent[] components = { component };
                DeathDate.Component = components.ToList();
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
                if (InjuryIncident != null)
                {
                    Observation.ComponentComponent workInjury = InjuryIncident.Component.Find(cmp => cmp.Code != null && cmp.Code.Coding != null && cmp.Code.Coding.FirstOrDefault() != null && cmp.Code.Coding.First().Code == "69444-8");
                    if (workInjury != null)
                    {
                        CodeableConcept vCodes = (CodeableConcept)workInjury.Value;
                        if (vCodes.Coding.FirstOrDefault() != null && vCodes.Coding.First().Code == "Y")
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
            set
            {
                // Injury Incident

                if (InjuryIncident == null)
                {
                    InjuryIncident = AddObservation("http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Injury-Incident", "11374-6", "urn:oid:2.16.840.1.113883.3.290.2.1.19", "Injury incident description");
                }

                // Work Injury Indicator

                Observation.ComponentComponent workInjury = new Observation.ComponentComponent();
                CodeableConcept wiCodes = new CodeableConcept();
                Coding wiCode = new Coding();
                wiCode.Code = "69444-8";
                wiCode.System = "urn:oid:2.16.840.1.113883.6.1";
                wiCode.Display = "Did death result from injury at work";
                wiCodes.Coding.Add(wiCode);
                workInjury.Code = wiCodes;

                // Add value
                CodeableConcept vCodes = new CodeableConcept();
                Coding vCode = new Coding();
                vCode.System = "http://hl7.org/fhir/v2/0136";
                if (value)
                {
                    vCode.Code = "Y";
                    vCode.Display = "Yes";
                }
                else
                {
                    vCode.Code = "N";
                    vCode.Display = "No";
                }
                vCodes.Coding.Add(vCode);
                workInjury.Value = vCodes;

                // Add to Injury Incident
                InjuryIncident.Component.RemoveAll(cmp => cmp.Code != null && cmp.Code.Coding != null && cmp.Code.Coding.FirstOrDefault() != null && cmp.Code.Coding.First().Code == "69444-8");
                InjuryIncident.Component.Add(workInjury);
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
                if (InjuryIncident != null)
                {
                    Observation.ComponentComponent tInjury = InjuryIncident.Component.Find(cmp => cmp.Code != null && cmp.Code.Coding != null && cmp.Code.Coding.FirstOrDefault() != null && cmp.Code.Coding.First().Code == "69448-9");
                    if (tInjury != null)
                    {
                        return CodeableConceptToDict((CodeableConcept)tInjury.Value);
                    }
                }
                return null;
            }
            set
            {
                // Injury Incident

                if (InjuryIncident == null)
                {
                    InjuryIncident = AddObservation("http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Injury-Incident", "11374-6", "urn:oid:2.16.840.1.113883.3.290.2.1.19", "Injury incident description");
                }

                // Transportation Event Indicator

                Observation.ComponentComponent tInjury = new Observation.ComponentComponent();
                CodeableConcept tCodes = new CodeableConcept();
                Coding tCode = new Coding();
                tCode.Code = "69448-9";
                tCode.System = "urn:oid:2.16.840.1.113883.6.1";
                tCode.Display = "Injury leading to death associated with transportation event";
                tCodes.Coding.Add(tCode);
                tInjury.Code = tCodes;

                // Add value
                tInjury.Value = DictToCodeableConcept(value);

                // Add to Injury Incident
                InjuryIncident.Component.RemoveAll(cmp => cmp.Code != null && cmp.Code.Coding != null && cmp.Code.Coding.FirstOrDefault() != null && cmp.Code.Coding.First().Code == "69448-9");
                InjuryIncident.Component.Add(tInjury);
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
                if (MEContacted != null)
                {
                    return Convert.ToBoolean(MEContacted.Value);
                }
                return false;
            }
            set
            {
                if (MEContacted == null)
                {
                    MEContacted = AddObservation("http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Examiner-Contacted",
                                                 "74497-9",
                                                 "urn:oid:2.16.840.1.113883.6.1",
                                                 "Medical examiner or coroner was contacted");
                }
                MEContacted.Value = new FhirBoolean(value);
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
                Observation observation = AddObservation("http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Decedent-Pregnancy",
                                                         "69442-2",
                                                         "http://loinc.org",
                                                         "Timing of recent pregnancy in relation to death");
                observation.Value = DictToCodeableConcept(value);
            }
        }

        /// <summary>Injury incident description.</summary>
        /// <value>Injury incident description. A Dictionary representing a description of an injury incident, containing the following key/value pairs:
        /// <para>"injuryDescription" - description of injury</para>
        /// <para>"injuryEffectiveDateTime" - effective date and time of injury</para>
        /// <para>"placeOfInjuryName" - description of the place of injury, e.g. decedent’s home, restaurant, wooded area</para>
        /// <para>"placeOfInjuryLine1" - location of injury, line one</para>
        /// <para>"placeOfInjuryLine2" - location of injury, line two</para>
        /// <para>"placeOfInjuryCity" - location of injury, city</para>
        /// <para>"placeOfInjuryCounty" - location of injury, county</para>
        /// <para>"placeOfInjuryState" - location of injury, state</para>
        /// <para>"placeOfInjuryZip" - location of injury, zip</para>
        /// <para>"placeOfInjuryCountry" - location of injury, country</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; detailsOfInjury = new Dictionary&lt;string, string&gt;();</para>
        /// <para>detailsOfInjury.Add("injuryDescription", "Example details of injury");</para>
        /// <para>detailsOfInjury.Add("injuryEffectiveDateTime", "2018-04-19T15:43:00+00:00");</para>
        /// <para>detailsOfInjury.Add("placeOfInjuryName", "Decedent's Home");</para>
        /// <para>detailsOfInjury.Add("placeOfInjuryLine1", "7 Example Street");</para>
        /// <para>detailsOfInjury.Add("placeOfInjuryLine2", "Unit 1234");</para>
        /// <para>detailsOfInjury.Add("placeOfInjuryCity", "Bedford");</para>
        /// <para>detailsOfInjury.Add("placeOfInjuryCounty", "Middlesex");</para>
        /// <para>detailsOfInjury.Add("placeOfInjuryState", "Massachusetts");</para>
        /// <para>detailsOfInjury.Add("placeOfInjuryZip", "01730");</para>
        /// <para>detailsOfInjury.Add("placeOfInjuryCountry", "United States");</para>
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

                if (InjuryIncident != null)
                {
                    dictionary.Add("injuryDescription", Convert.ToString(InjuryIncident.Value));
                    dictionary.Add("injuryEffectiveDateTime", Convert.ToString(InjuryIncident.Effective));
                }

                if (InjuryLocation != null)
                {
                    Observation.ComponentComponent placeOfInjury = InjuryIncident.Component.FirstOrDefault();
                    if (placeOfInjury != null)
                    {
                        dictionary.Add("placeOfInjuryName", Convert.ToString(placeOfInjury.Value));
                    }
                    else
                    {
                        dictionary.Add("placeOfInjuryName", InjuryLocation.Name);
                    }
                    Address injuryAddress = InjuryLocation.Address;
                    if (injuryAddress != null)
                    {
                        if (injuryAddress.Line.FirstOrDefault() != null)
                        {
                            dictionary.Add("placeOfInjuryLine1", injuryAddress.Line.FirstOrDefault());
                        }
                        if (injuryAddress.Line.Count() > 1)
                        {
                            dictionary.Add("placeOfInjuryLine2", injuryAddress.Line.LastOrDefault());
                        }
                        dictionary.Add("placeOfInjuryCity", injuryAddress.City);
                        dictionary.Add("placeOfInjuryCounty", injuryAddress.District);
                        dictionary.Add("placeOfInjuryState", injuryAddress.State);
                        dictionary.Add("placeOfInjuryZip", injuryAddress.PostalCode);
                        dictionary.Add("placeOfInjuryCountry", injuryAddress.Country);
                    }
                }

                return dictionary;
            }
            set
            {
                // Injury Incident

                if (InjuryIncident == null)
                {
                    InjuryIncident = AddObservation("http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Injury-Incident", "11374-6", "urn:oid:2.16.840.1.113883.3.290.2.1.19", "Injury incident description");
                }

                // Set value
                InjuryIncident.Value = new FhirString(GetValue(value, "injuryDescription"));

                // Set effective datetime
                InjuryIncident.Effective = new FhirDateTime(GetValue(value, "injuryEffectiveDateTime"));

                // Injury Location

                if (InjuryLocation == null)
                {
                    InjuryLocation = new Location();
                    InjuryLocation.Id = "urn:uuid:" + Guid.NewGuid().ToString();

                    // Add profile to DispositionLocation
                    InjuryLocation.Meta = new Meta();
                    string[] injury_location_profile = { "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Injury-Location" };
                    InjuryLocation.Meta.Profile = injury_location_profile;

                    // Add Location to Bundle
                    AddReferenceToComposition(InjuryLocation.Id);
                    Bundle.AddResourceEntry(InjuryLocation, InjuryLocation.Id);
                }

                Address injuryAddress = new Address();
                string[] ialines = {GetValue(value, "placeOfInjuryLine1"), GetValue(value, "placeOfInjuryLine2")};
                injuryAddress.Line = ialines.ToArray();
                injuryAddress.City = GetValue(value, "placeOfInjuryCity");
                injuryAddress.District = GetValue(value, "placeOfInjuryCounty");
                injuryAddress.State = GetValue(value, "placeOfInjuryState");
                injuryAddress.PostalCode = GetValue(value, "placeOfInjuryZip");
                injuryAddress.Country = GetValue(value, "placeOfInjuryCountry");
                injuryAddress.Type = Hl7.Fhir.Model.Address.AddressType.Postal;
                InjuryLocation.Address = injuryAddress;

                // Add name
                InjuryLocation.Name = GetValue(value, "placeOfInjuryName");

                // Add Injury Location reference to Injury Incident
                Extension ext = new Extension();
                ext.Url = "http://hl7.org/fhir/us/vrdr/StructureDefinition/VRDR-Injury-Location";
                ext.Value = new ResourceReference(InjuryLocation.Id);
                InjuryIncident.Extension.Clear();
                InjuryIncident.Extension.Add(ext);

                // Place of Injury Component in Injury Incident
                Observation.ComponentComponent placeOfInjury = new Observation.ComponentComponent();
                CodeableConcept poiCodes = new CodeableConcept();
                Coding poiCode = new Coding();
                poiCode.Code = "69450-5";
                poiCode.System = "urn:oid:2.16.840.1.113883.6.1";
                poiCode.Display = "Place of injury";
                poiCodes.Coding.Add(poiCode);
                placeOfInjury.Code = poiCodes;
                placeOfInjury.Value = new FhirString(GetValue(value, "placeOfInjuryName"));
                InjuryIncident.Component.RemoveAll(cmp => cmp.Code != null && cmp.Code.Coding != null && cmp.Code.Coding.FirstOrDefault() != null && cmp.Code.Coding.First().Code == "69450-5");
                InjuryIncident.Component.Add(placeOfInjury);
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
            //Composition.Section.First().Entry.Add(new ResourceReference(reference));
        }

        /// <summary>Remove a reference from the Death Record Composition.</summary>
        /// <param name="reference">a reference.</param>
        private bool RemoveReferenceFromComposition(string reference)
        {
            return true;//Composition.Section.First().Entry.RemoveAll(entry => entry.Reference == reference) > 0;
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

        /// <summary>Convert a FHIR CodableConcepot to a "code" dictionary.</summary>
        /// <param name="codeableConcept">represents a CodeableConcept.</param>
        /// <returns>the corresponding dictionary representation of the code.</returns>
        private Dictionary<string, string> CodeableConceptToDict(CodeableConcept codeableConcept)
        {
            if (codeableConcept == null)
            {
                return null;
            }
            Dictionary<string, string> dict = new Dictionary<string, string>();
            Coding code = codeableConcept.Coding.First();
            if (code != null)
            {
                dict.Add("code", code.Code);
                dict.Add("system", code.System);
                dict.Add("display", code.Display);
            }
            else
            {
                return null;
            }
            return dict;
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
    }
}


