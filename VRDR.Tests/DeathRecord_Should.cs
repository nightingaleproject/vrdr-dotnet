using System;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Xunit;
using System.Linq;

namespace VRDR.Tests
{
    public class DeathRecord_Should
    {
        private DeathRecord DeathRecord1_XML;
        private DeathRecord DeathCertificateDocument2_XML;
        private DeathRecord DeathRecord1_JSON;
        private DeathRecord DeathRecord2_JSON;
        private DeathRecord DeathCertificateDocument2_JSON;
        private DeathRecord DeathCertificateDocument1_JSON;
        private DeathRecord CauseOfDeathCodedContentBundle1_JSON;
        private DeathRecord DemographicCodedContentBundle1_JSON;

        private DeathRecord SetterDeathRecord;

        public DeathRecord_Should()
        {
            DeathRecord1_XML = new DeathRecord(File.ReadAllText(FixturePath("fixtures/xml/DeathRecord1.xml")));
            DeathCertificateDocument2_XML = new DeathRecord(File.ReadAllText(FixturePath("fixtures/xml/Bundle-DeathCertificateDocument-Example2.xml")));
            DeathRecord1_JSON = new DeathRecord(File.ReadAllText(FixturePath("fixtures/json/DeathRecord1.json")));
            DeathRecord2_JSON = new DeathRecord(File.ReadAllText(FixturePath("fixtures/json/2022CT000008_record.json")));
            DeathCertificateDocument2_JSON = new DeathRecord(File.ReadAllText(FixturePath("fixtures/json/Bundle-DeathCertificateDocument-Example2.json")));
            DeathCertificateDocument1_JSON = new DeathRecord(File.ReadAllText(FixturePath("fixtures/json/Bundle-DeathCertificateDocument-Example1.json")));
            CauseOfDeathCodedContentBundle1_JSON = new DeathRecord(File.ReadAllText(FixturePath("fixtures/json/Bundle-CauseOfDeathCodedContentBundle-Example1.json")));
            DemographicCodedContentBundle1_JSON = new DeathRecord(File.ReadAllText(FixturePath("fixtures/json/Bundle-DemographicCodedContentBundle-Example1.json")));

            SetterDeathRecord = new DeathRecord();
        }

        [Fact]
        public void FailBadDplaceCode()
        {
            DeathRecord dr = new DeathRecord(File.ReadAllText(FixturePath("fixtures/json/DeathRecordBadDplaceCode.json")));
            Exception ex = Assert.Throws<System.ArgumentOutOfRangeException>(() => new IJEMortality(dr).DPLACE);
            Assert.Equal("Error: Unable to find IJE DPLACE mapping for FHIR DeathLocationType field value '440081000124100x'", ex.Message.Substring(96, 98));
        }

        [Fact]
        public void FailInvalidInput()
        {
            Exception ex = Assert.Throws<System.ArgumentException>(() => new DeathRecord("foobar"));
            Assert.Equal("The given input does not appear to be a valid XML or JSON FHIR record.", ex.Message);
        }

        [Fact]
        public void FailMissingValue()
        {
            string bundle = File.ReadAllText(FixturePath("fixtures/xml/MissingValue.xml"));
            Exception ex = Assert.Throws<System.ArgumentException>(() => new DeathRecord(bundle));
            Assert.Equal("Parser: The attribute 'value' in element 'status' has an empty value, which is not allowed. (at line 21, 17)", ex.Message);
        }


        [Fact]
        public void FailMissingObservationCode()
        {
            string bundle = File.ReadAllText(FixturePath("fixtures/json/MissingObservationCode.json"));
            Exception ex = Assert.Throws<System.ArgumentException>(() => new DeathRecord(bundle));
            Assert.Equal("Found an Observation resource that did not contain a code. All Observations must include a code to specify what the Observation is referring to.", ex.Message);
        }

        [Fact]
        public void FailMissingRelatedPersonRelationshipCode()
        {
            string bundle = File.ReadAllText(FixturePath("fixtures/json/MissingRelatedPersonRelationshipCode.json"));
            Exception ex = Assert.Throws<System.ArgumentException>(() => new DeathRecord(bundle));
            Assert.Equal("Found a RelatedPerson resource that did not contain a relationship code. All RelatedPersons must include a relationship code to specify how the RelatedPerson is related to the subject.", ex.Message);
        }

        [Fact]
        public void InvalidDeathLocationJurisdiction()
        {
            // In input file's http://hl7.org/fhir/us/vrdr/StructureDefinition/Location-Jurisdiction-Id extension uses an old format from version 3.0.0 RC5
            // if the input is invalid, return null, no error is thrown
            DeathRecord deathRecord = new DeathRecord(File.ReadAllText(FixturePath("fixtures/json/InvalidJurisdictionId.json")));
            Assert.Null(deathRecord.DeathLocationJurisdiction);
        }

        [Fact]
        public void EmptyRecordToDescription()
        {
            DeathRecord deathRecord = new DeathRecord();
            string description = deathRecord.ToDescription();
            Assert.NotNull(description);
        }

        [Fact]
        public void EmptyRecordToIJE()
        {
            DeathRecord deathRecord = new DeathRecord();
            string ije = new IJEMortality(deathRecord, false).ToString(); // Don't validate since empty record
            Assert.NotNull(ije);
        }

        [Fact]
        public void AllConnectathonRecords()
        {
            var records = VRDR.Connectathon.Records;
            Assert.NotNull(records);
            Assert.IsType<DeathRecord[]>(records);
            Assert.Equal(4, records.Count());
            Assert.Equal(VRDR.Connectathon.TwilaHilty().FamilyName, records[0].FamilyName);
            Assert.Equal(VRDR.Connectathon.FideliaAlsup().Identifier, records[1].Identifier);
            Assert.Equal(VRDR.Connectathon.SujaUnknown().InjuryTime, records[3].InjuryTime);
        }

        [Fact]
        public void TestInjuryFields()
        {
            var records = VRDR.Connectathon.Records;
            Assert.NotNull(records);
            Assert.IsType<DeathRecord[]>(records);
            Assert.Equal(4, records.Count());
            Assert.Equal(VRDR.Connectathon.SujaUnknown().InjuryTime, records[3].InjuryTime);
            Assert.Equal(VRDR.Connectathon.SujaUnknown().InjuryPlaceDescription, records[3].InjuryPlaceDescription);
            Assert.Equal(VRDR.Connectathon.SujaUnknown().InjuryDescription, records[3].InjuryDescription);
            Assert.Equal("description", records[3].InjuryDescription);
            Assert.Equal("place", records[3].InjuryPlaceDescription);
        }

        [Fact]
        // Check that two issues in NVSS-398 have been resolved
        public void ConnectathonRecordNVSS398()
        {
            DeathRecord first = DeathRecord2_JSON;
            IJEMortality firstije = new IJEMortality(first);
            Assert.Null(first.DateOfDeath);   // Record has an unknown death day, the DeathDate should be null
            Assert.Equal(-1, first.DeathDay); // Since it's explicitly unknown the DeathDay should be -1
            Assert.Equal("French", firstije.RACE22);
        }
        [Fact]
        public void ToFromDescription()
        {
            DeathRecord first = DeathRecord1_XML;
            string firstDescription = first.ToDescription();
            DeathRecord second = DeathRecord.FromDescription(firstDescription);
            Assert.Equal(first.Identifier, second.Identifier);
            Assert.Equal(first.GivenNames, second.GivenNames);
            Assert.Equal(first.AutopsyResultsAvailable, second.AutopsyResultsAvailable);
            Assert.Equal(first.Race, second.Race);
            Assert.Equal(first.COD1A, second.COD1A);
            Assert.Equal(first.INTERVAL1B, second.INTERVAL1B);
            // Assert.Equal(first.CODE1A, second.CODE1A);
            Assert.Equal(first.CertifierAddress, second.CertifierAddress);
            Assert.Equal(first.CausesOfDeath, second.CausesOfDeath);
        }

        [Fact]
        public void ToFromCodedBundleViaDescription()
        {
            DeathRecord record = DeathCertificateDocument2_JSON;
            DeathRecord codedCODRecord = new DeathRecord(record.GetCauseOfDeathCodedContentBundle());
            DeathRecord codedDemoRecord = new DeathRecord(record.GetDemographicCodedContentBundle());
            string CodedCODDescription = codedCODRecord.ToDescription();
            string CodedDemoDescription = codedCODRecord.ToDescription();
            DeathRecord record2 = new DeathRecord(codedCODRecord.ToJSON());
            DeathRecord record3 = new DeathRecord(codedDemoRecord.ToJSON());
        }

        [Fact]
        public void SetPatientAfterParse()
        {
            DeathRecord sample1 = new DeathRecord(File.ReadAllText(FixturePath("fixtures/xml/DeathRecord1.xml")));
            DeathRecord sample2 = new DeathRecord(File.ReadAllText(FixturePath("fixtures/json/DeathRecord1.json")));
            Assert.Equal("Pãtêl", sample1.FamilyName);
            Assert.Equal("Pãtêl", sample2.FamilyName);
            sample1.FamilyName = "1changed2abc";
            sample2.FamilyName = "2changed1xyz";
            Assert.Equal("1changed2abc", sample1.FamilyName);
            Assert.Equal("2changed1xyz", sample2.FamilyName);
        }

        [Fact]
        public void ParseDeathLocationJurisdictionIJEtoJson()
        {
            IJEMortality ijefromjson = new IJEMortality(DeathRecord1_XML);
            IJEMortality ijefromxml = new IJEMortality(DeathRecord1_JSON);
            DeathRecord fromijefromjson = ijefromjson.ToDeathRecord();
            DeathRecord fromijefromxml = ijefromjson.ToDeathRecord();

            Assert.Equal("YC", fromijefromjson.DeathLocationJurisdiction);
            Assert.Equal("YC", fromijefromxml.DeathLocationJurisdiction);
        }

        [Fact]
        public void NoDeathLocationJurisdictionJsontoIJE()
        {
            DeathRecord deathRecord = new DeathRecord();
            IJEMortality ije = new IJEMortality(deathRecord, false); // Don't raise validation errors on empty record
            Assert.Equal("  ", ije.DSTATE);
        }

        [Fact]
        public void ParseDeathLocationTypeIJEtoJson()
        {
            DeathRecord djson = new DeathRecord(File.ReadAllText(FixturePath("fixtures/json/DeathLocationType.json")));
            IJEMortality ijefromjson = new IJEMortality(djson);
            DeathRecord fromijefromjson = ijefromjson.ToDeathRecord();

            Assert.NotEqual(fromijefromjson.DeathLocationTypeHelper, VRDR.ValueSets.PlaceOfDeath.Death_In_Hospice);
            Assert.Equal(fromijefromjson.DeathLocationTypeHelper, VRDR.ValueSets.PlaceOfDeath.Death_In_Home);
            Assert.Equal("Death in home", fromijefromjson.DeathLocationType["display"]);
        }

        [Fact]
        public void ParseRaceEthnicityJsonToIJE()
        {
            // Hispanic or Latino
            DeathRecord d = new DeathRecord(File.ReadAllText(FixturePath("fixtures/json/RaceEthnicityCaseRecord.json")));
            IJEMortality ije1 = new IJEMortality(d);
            Assert.Equal("H", ije1.DETHNIC1);
            Assert.Equal("H", ije1.DETHNIC2);
            Assert.Equal("H", ije1.DETHNIC3);
            Assert.Equal("H", ije1.DETHNIC4);
            Assert.Equal("Y", ije1.RACE1);
            Assert.Equal("N", ije1.RACE2);
            Assert.Equal("N", ije1.RACE3);
            Assert.Equal("N", ije1.RACE4);
            Assert.Equal("N", ije1.RACE5);
            Assert.Equal("N", ije1.RACE6);
            Assert.Equal("N", ije1.RACE7);
            Assert.Equal("N", ije1.RACE8);
            Assert.Equal("N", ije1.RACE9);
            Assert.Equal("N", ije1.RACE10);
            Assert.Equal("N", ije1.RACE11);
            Assert.Equal("N", ije1.RACE12);
            Assert.Equal("N", ije1.RACE13);
            Assert.Equal("N", ije1.RACE14);
            Assert.Equal("N", ije1.RACE15);

            // Non Hispanic or Latino
            DeathRecord d2 = new DeathRecord(File.ReadAllText(FixturePath("fixtures/json/RaceEthnicityCaseRecord2.json")));
            IJEMortality ije2 = new IJEMortality(d2);
            Assert.Equal("N", ije2.DETHNIC1);
            Assert.Equal("N", ije2.DETHNIC2);
            Assert.Equal("N", ije2.DETHNIC3);
            Assert.Equal("N", ije2.DETHNIC4);
            Assert.Equal("Y", ije2.RACE10);
            Assert.Equal("Hmong", ije2.RACE18);
            Assert.Equal("Y", ije2.RACE3);

            // From VRDR IG
            DeathRecord d3 = (DeathRecord1_JSON);
            IJEMortality ije3 = new IJEMortality(d3);
            Assert.Equal("H", ije3.DETHNIC1);
            Assert.Equal("U", ije3.DETHNIC2);
            Assert.Equal("U", ije3.DETHNIC3);
            Assert.Equal("U", ije3.DETHNIC4);
            Assert.Equal("", ije3.RACE18);
            Assert.Equal("Y", ije3.RACE1);
            Assert.Equal("N", ije3.RACE2);
            Assert.Equal("N", ije3.RACE3);
            Assert.Equal("N", ije3.RACE4);
            Assert.Equal("N", ije3.RACE5);
            Assert.Equal("N", ije3.RACE6);
            Assert.Equal("N", ije3.RACE7);
            Assert.Equal("N", ije3.RACE8);
            Assert.Equal("N", ije3.RACE9);
            Assert.Equal("N", ije3.RACE10);
            Assert.Equal("N", ije3.RACE11);
            Assert.Equal("N", ije3.RACE12);
            Assert.Equal("N", ije3.RACE13);
            Assert.Equal("N", ije3.RACE14);
            Assert.Equal("N", ije3.RACE15);
        }

        [Fact]
        public void ParseRaceEthnicityIJEtoJson()
        {
            DeathRecord d = new DeathRecord(File.ReadAllText(FixturePath("fixtures/json/RaceEthnicityCaseRecord.json")));
            IJEMortality ije1 = new IJEMortality(d);
            IJEMortality ije2 = new IJEMortality(ije1.ToString(), true);
            DeathRecord d2 = ije2.ToDeathRecord();

            // Ethnicity tuple
            Assert.Equal("Y", d2.Ethnicity2Helper);

            // Race tuple
            foreach (var pair in d2.Race)
            {
                switch (pair.Item1)
                {
                    case NvssRace.White:
                        Assert.Equal("Y", pair.Item2);
                        break;
                    case NvssRace.AmericanIndianOrAlaskanNative:
                        Assert.Equal("N", pair.Item2);
                        break;
                    default:
                        break;
                }
            }
            Assert.Equal(15, d2.Race.Length);
        }

        [Fact]
        public void HandleNoEthnicityDataInJSON()
        {
            // if no ethnicity data is provided in FHIR, IJE should have unknowns and a blank literal
            DeathRecord d = new DeathRecord(File.ReadAllText(FixturePath("fixtures/json/MissingEthnicityData.json")));
            IJEMortality ije1 = new IJEMortality(d);
            Assert.Equal("U", ije1.DETHNIC1);
            Assert.Equal("U", ije1.DETHNIC2);
            Assert.Equal("U", ije1.DETHNIC3);
            Assert.Equal("U", ije1.DETHNIC4);
            Assert.Equal("", ije1.DETHNIC5);
        }

        [Fact]
        public void SetPractitionerAfterParse()
        {
            DeathRecord sample1 = new DeathRecord(File.ReadAllText(FixturePath("fixtures/xml/DeathRecord1.xml")));
            DeathRecord sample2 = new DeathRecord(File.ReadAllText(FixturePath("fixtures/json/DeathRecord1.json")));
            Assert.Equal("Last", sample1.CertifierFamilyName);
            Assert.Equal("Last", sample2.CertifierFamilyName);
            sample1.CertifierFamilyName = "1diff2abc";
            sample2.CertifierFamilyName = "2diff1xyz";
            Assert.Equal("1diff2abc", sample1.CertifierFamilyName);
            Assert.Equal("2diff1xyz", sample2.CertifierFamilyName);
        }

        [Fact]
        public void Set_DeathLocationTypeHelper()
        {
            SetterDeathRecord.DeathLocationTypeHelper = VRDR.ValueSets.PlaceOfDeath.Death_In_Nursing_Home_Or_Long_Term_Care_Facility;
            Assert.Equal(VRDR.ValueSets.PlaceOfDeath.Death_In_Nursing_Home_Or_Long_Term_Care_Facility, SetterDeathRecord.DeathLocationTypeHelper);
            Assert.Equal("Death in nursing home or long term care facility", SetterDeathRecord.DeathLocationType["display"]);
            Exception ex = Assert.Throws<System.ArgumentException>(() => SetterDeathRecord.DeathLocationTypeHelper = "NotAValidValue");
        }

        [Fact]
        public void Get_DeathLocationType()
        {
            Assert.Equal(ValueSets.PlaceOfDeath.Death_In_Home, DeathRecord1_JSON.DeathLocationTypeHelper);
            Assert.Equal(ValueSets.PlaceOfDeath.Death_In_Hospital, DeathCertificateDocument1_JSON.DeathLocationTypeHelper);
            Assert.Equal("", CauseOfDeathCodedContentBundle1_JSON.DeathLocationType["code"]);
            Assert.Equal(ValueSets.PlaceOfDeath.Death_In_Home, DeathRecord1_XML.DeathLocationTypeHelper);
        }
        [Fact]
        public void Set_Identifier()
        {
            SetterDeathRecord.Identifier = "1337";
            Assert.Equal("1337", SetterDeathRecord.Identifier);
        }

        [Fact]
        public void Get_Identifier()
        {
            Assert.Equal("000182", DeathRecord1_JSON.Identifier);
            Assert.Equal("000182", DeathCertificateDocument2_JSON.Identifier);
            Assert.Equal("000182", DeathRecord1_XML.Identifier);
        }

        [Fact]
        public void Set_DeathRecordIdentifier()
        {
            DeathRecord empty = new DeathRecord();
            Assert.Equal("0000XX000000", empty.DeathRecordIdentifier);
            empty.DateOfDeath = "2019-10-01";
            Assert.Equal("2019XX000000", empty.DeathRecordIdentifier);
            empty.Identifier = "101";
            Assert.Equal("2019XX000101", empty.DeathRecordIdentifier);
            empty.DeathLocationJurisdiction = "MA";  // 25 is the code for MA
            Assert.Equal("2019MA000101", empty.DeathRecordIdentifier);
        }

        [Fact]
        public void Get_DeathRecordIdentifier()
        {
            Assert.Equal("2019YC000182", DeathRecord1_JSON.DeathRecordIdentifier);
            Assert.Equal("2020NY000182", DeathCertificateDocument2_JSON.DeathRecordIdentifier);
            Assert.Equal("2020NY000182", DeathCertificateDocument1_JSON.DeathRecordIdentifier);
            Assert.Equal("2020NY000182", CauseOfDeathCodedContentBundle1_JSON.DeathRecordIdentifier);
            Assert.Equal("2020NY000182", DemographicCodedContentBundle1_JSON.DeathRecordIdentifier);
            Assert.Equal("2019YC000182", DeathRecord1_XML.DeathRecordIdentifier);
        }

        [Fact]
        public void Set_StateLocalIdentifier()
        {
            SetterDeathRecord.StateLocalIdentifier1 = "000000000042";
            SetterDeathRecord.StateLocalIdentifier2 = "100000000042";
            Assert.Equal("000000000042", SetterDeathRecord.StateLocalIdentifier1);
            Assert.Equal("100000000042", SetterDeathRecord.StateLocalIdentifier2);
        }

        [Fact]
        public void Get_StateLocalIdentifier()
        {
            Assert.Equal("000000000042", DeathRecord1_JSON.StateLocalIdentifier1);
            Assert.Equal("000000000042", DeathRecord1_XML.StateLocalIdentifier1);
            Assert.Equal("000000000001", DeathCertificateDocument2_JSON.StateLocalIdentifier1);
            Assert.Equal("100000000001", DeathCertificateDocument2_JSON.StateLocalIdentifier2);
        }

        [Fact]
        public void Set_CertifiedTime()
        {
            SetterDeathRecord.CertifiedTime = "2019-01-28T16:48:06-05:00";
            Assert.Equal("2019-01-28T16:48:06-05:00", SetterDeathRecord.CertifiedTime);
        }

        [Fact]
        public void Get_CertifiedTime()
        {
            Assert.Equal("2019-01-29T16:48:06-05:00", DeathRecord1_JSON.CertifiedTime);
            Assert.Equal("2020-11-14T16:39:40-05:00", DeathCertificateDocument2_JSON.CertifiedTime);
            Assert.Equal("2019-01-29T16:48:06-05:00", DeathRecord1_XML.CertifiedTime);
        }

        [Fact]
        public void Set_RegisteredTime()
        {
            SetterDeathRecord.RegisteredTime = "2019-01-29T16:48:06-05:00";
            Assert.Equal("2019-01-29T16:48:06-05:00", SetterDeathRecord.RegisteredTime);
        }

        [Fact]
        public void Get_RegisteredTime()
        {
            Assert.Equal("2019-02-01T16:47:04-05:00", DeathRecord1_JSON.RegisteredTime);
            Assert.Equal("2020-11-15T16:39:54-05:00", DeathCertificateDocument2_JSON.RegisteredTime);
            Assert.Equal("2019-02-01T16:47:04-05:00", DeathRecord1_XML.RegisteredTime);
        }

        [Fact]
        public void Get_RegisteredTime_ConvertIJE()
        {
            IJEMortality ije1 = new IJEMortality(DeathRecord1_JSON);
            Assert.Equal("2019", ije1.DOR_YR);
            Assert.Equal("02", ije1.DOR_MO);
            Assert.Equal("01", ije1.DOR_DY);
        }

        [Fact]
        public void Set_CertificationRole()
        {
            Dictionary<string, string> CertificationRole = new Dictionary<string, string>();
            CertificationRole.Add("code", "434641000124105");
            CertificationRole.Add("system", CodeSystems.SCT);
            CertificationRole.Add("display", "Death certification and verification by physician (procedure)");
            SetterDeathRecord.CertificationRole = CertificationRole;
            Assert.Equal("434641000124105", SetterDeathRecord.CertificationRole["code"]);
            Assert.Equal(CodeSystems.SCT, SetterDeathRecord.CertificationRole["system"]);
            Assert.Equal("Death certification and verification by physician (procedure)", SetterDeathRecord.CertificationRole["display"]);
            SetterDeathRecord.CertificationRoleHelper = VRDR.ValueSets.CertifierTypes.Death_Certification_And_Verification_By_Physician_Procedure;
            Assert.Equal("434641000124105", SetterDeathRecord.CertificationRole["code"]);
            Assert.Equal(CodeSystems.SCT, SetterDeathRecord.CertificationRole["system"]);
            Assert.Equal("Death certification and verification by physician (procedure)", SetterDeathRecord.CertificationRole["display"]);
            SetterDeathRecord.CertificationRoleHelper = "Barber";
            Assert.Equal("OTH", SetterDeathRecord.CertificationRole["code"]);
            Assert.Equal(CodeSystems.NullFlavor_HL7_V3, SetterDeathRecord.CertificationRole["system"]);
            Assert.Equal("Other", SetterDeathRecord.CertificationRole["display"]);
            Assert.Equal("Barber", SetterDeathRecord.CertificationRole["text"]);
        }

        [Fact]
        public void Get_CertificationRole()
        {
            Assert.Equal("Nurse Practitioner", DeathCertificateDocument2_JSON.CertificationRoleHelper);
            Assert.Equal("434641000124105", DeathRecord1_JSON.CertificationRole["code"]);
            Assert.Equal(CodeSystems.SCT, DeathRecord1_XML.CertificationRole["system"]);
            Assert.Equal("Physician certified and pronounced death certificate", DeathRecord1_JSON.CertificationRole["display"]);
            Assert.Equal("434641000124105", DeathRecord1_XML.CertificationRole["code"]);
            Assert.Equal(CodeSystems.SCT, DeathRecord1_JSON.CertificationRole["system"]);
            Assert.Equal("Physician certified and pronounced death certificate", DeathRecord1_XML.CertificationRole["display"]);
        }

        // [Fact]
        // public void Set_InterestedPartyIdentifier()
        // {
        //     var id = new Dictionary<string, string>();
        //     id["system"] = "foo";
        //     id["value"] = "0000000000";
        //     SetterDeathRecord.InterestedPartyIdentifier = id;
        //     Assert.Equal("foo", SetterDeathRecord.InterestedPartyIdentifier["system"]);
        //     Assert.Equal("0000000000", SetterDeathRecord.InterestedPartyIdentifier["value"]);
        // }

        // [Fact]
        // public void Get_InterestedPartyIdentifier()
        // {
        //     Assert.Equal("1010101", DeathRecord1_JSON.InterestedPartyIdentifier["value"]);
        //     Assert.Equal("1010101", DeathRecord1_XML.InterestedPartyIdentifier["value"]);
        // }

        // [Fact]
        // public void Set_InterestedPartyName()
        // {
        //     SetterDeathRecord.InterestedPartyName = "123abc123xyz";
        //     Assert.Equal("123abc123xyz", SetterDeathRecord.InterestedPartyName);
        // }

        // [Fact]
        // public void Get_InterestedPartyName()
        // {
        //     Assert.Equal("Example Hospital", DeathRecord1_JSON.InterestedPartyName);
        //     Assert.Equal("Example Hospital", DeathRecord1_XML.InterestedPartyName);
        // }

        // [Fact]
        // public void Set_InterestedPartyAddress()
        // {
        //     Dictionary<string, string> address = new Dictionary<string, string>();
        //     address.Add("addressLine1", "12 Example Street");
        //     address.Add("addressLine2", "Line 2");
        //     address.Add("addressCity", "Bedford");
        //     address.Add("addressCounty", "Middlesex");
        //     address.Add("addressState", "MA");
        //     address.Add("addressZip", "01730");
        //     address.Add("addressCountry", "US");
        //     SetterDeathRecord.InterestedPartyAddress = address;
        //     Assert.Equal("12 Example Street", SetterDeathRecord.InterestedPartyAddress["addressLine1"]);
        //     Assert.Equal("Line 2", SetterDeathRecord.InterestedPartyAddress["addressLine2"]);
        //     Assert.Equal("Bedford", SetterDeathRecord.InterestedPartyAddress["addressCity"]);
        //     Assert.Equal("Middlesex", SetterDeathRecord.InterestedPartyAddress["addressCounty"]);
        //     Assert.Equal("MA", SetterDeathRecord.InterestedPartyAddress["addressState"]);
        //     Assert.Equal("01730", SetterDeathRecord.InterestedPartyAddress["addressZip"]);
        //     Assert.Equal("US", SetterDeathRecord.InterestedPartyAddress["addressCountry"]);
        // }

        // [Fact]
        // public void Get_InterestedPartyAddress()
        // {
        //     Assert.Equal("10 Example Street", DeathRecord1_JSON.InterestedPartyAddress["addressLine1"]);
        //     Assert.Equal("Line 2", DeathRecord1_JSON.InterestedPartyAddress["addressLine2"]);
        //     Assert.Equal("Bedford", DeathRecord1_JSON.InterestedPartyAddress["addressCity"]);
        //     Assert.Equal("Middlesex", DeathRecord1_JSON.InterestedPartyAddress["addressCounty"]);
        //     Assert.Equal("MA", DeathRecord1_JSON.InterestedPartyAddress["addressState"]);
        //     Assert.Equal("01730", DeathRecord1_JSON.InterestedPartyAddress["addressZip"]);
        //     Assert.Equal("US", DeathRecord1_JSON.InterestedPartyAddress["addressCountry"]);
        //     Assert.Equal("10 Example Street", DeathRecord1_XML.InterestedPartyAddress["addressLine1"]);
        //     Assert.Equal("Line 2", DeathRecord1_XML.InterestedPartyAddress["addressLine2"]);
        //     Assert.Equal("Bedford", DeathRecord1_XML.InterestedPartyAddress["addressCity"]);
        //     Assert.Equal("Middlesex", DeathRecord1_XML.InterestedPartyAddress["addressCounty"]);
        //     Assert.Equal("MA", DeathRecord1_XML.InterestedPartyAddress["addressState"]);
        //     Assert.Equal("01730", DeathRecord1_XML.InterestedPartyAddress["addressZip"]);
        //     Assert.Equal("US", DeathRecord1_XML.InterestedPartyAddress["addressCountry"]);
        // }

        // [Fact]
        // public void Set_InterestedPartyType()
        // {
        //     Dictionary<string, string> type = new Dictionary<string, string>();
        //     type.Add("code", "prov");
        //     type.Add("system", CodeSystems.HL7_organization_type);
        //     type.Add("display", "Healthcare Provider");
        //     SetterDeathRecord.InterestedPartyType = type;
        //     Assert.Equal("prov", SetterDeathRecord.InterestedPartyType["code"]);
        //     Assert.Equal(CodeSystems.HL7_organization_type, SetterDeathRecord.InterestedPartyType["system"]);
        //     Assert.Equal("Healthcare Provider", SetterDeathRecord.InterestedPartyType["display"]);
        // }

        // [Fact]
        // public void Get_InterestedPartyType()
        // {
        //     Assert.Equal("prov", DeathRecord1_JSON.InterestedPartyType["code"]);
        //     Assert.Equal(CodeSystems.HL7_organization_type, DeathRecord1_XML.InterestedPartyType["system"]);
        //     Assert.Equal("Healthcare Provider", DeathRecord1_JSON.InterestedPartyType["display"]);
        //     Assert.Equal("prov", DeathRecord1_XML.InterestedPartyType["code"]);
        //     Assert.Equal(CodeSystems.HL7_organization_type, DeathRecord1_JSON.InterestedPartyType["system"]);
        //     Assert.Equal("Healthcare Provider", DeathRecord1_XML.InterestedPartyType["display"]);
        // }

        [Fact]
        public void Set_MannerOfDeathType()
        {
            Dictionary<string, string> type = new Dictionary<string, string>();
            SetterDeathRecord.MannerOfDeathTypeHelper = ValueSets.MannerOfDeath.Accidental_Death;
            Assert.Equal(CodeSystems.SCT, SetterDeathRecord.MannerOfDeathType["system"]);
            Assert.Equal(ValueSets.MannerOfDeath.Accidental_Death, SetterDeathRecord.MannerOfDeathTypeHelper);
            Assert.Equal("Accidental death", SetterDeathRecord.MannerOfDeathType["display"]);
        }

        [Fact]
        public void Get_MannerOfDeathType()
        {
            Assert.Equal(CodeSystems.SCT, DeathRecord1_JSON.MannerOfDeathType["system"]);
            Assert.Equal(ValueSets.MannerOfDeath.Accidental_Death, DeathRecord1_JSON.MannerOfDeathType["code"]);
            Assert.Equal("Accidental death", DeathRecord1_JSON.MannerOfDeathType["display"]);
            Assert.Equal(CodeSystems.SCT, DeathRecord1_XML.MannerOfDeathType["system"]);
            Assert.Equal(ValueSets.MannerOfDeath.Accidental_Death, DeathRecord1_XML.MannerOfDeathType["code"]);
            Assert.Equal("Accidental death", DeathRecord1_XML.MannerOfDeathType["display"]);
            Assert.Equal(ValueSets.MannerOfDeath.Natural_Death, DeathCertificateDocument2_JSON.MannerOfDeathTypeHelper);
        }

        [Fact]
        public void Set_CertifierGivenNames()
        {
            string[] cnames = { "Doctor", "Middle" };
            SetterDeathRecord.CertifierGivenNames = cnames;
            Assert.Equal("Doctor", SetterDeathRecord.CertifierGivenNames[0]);
            Assert.Equal("Middle", SetterDeathRecord.CertifierGivenNames[1]);
        }

        [Fact]
        public void Get_CertifierGivenNames()
        {
            string[] cnamesjson1 = DeathCertificateDocument1_JSON.CertifierGivenNames;
            Assert.Equal("Jim", cnamesjson1[0]);
            Assert.Single(cnamesjson1);
            string[] cnamesjson = DeathRecord1_JSON.CertifierGivenNames;
            Assert.Equal("Doctor", cnamesjson[0]);
            Assert.Equal("Middle", cnamesjson[1]);
            string[] cnamesxml = DeathRecord1_XML.CertifierGivenNames;
            Assert.Equal("Doctor", cnamesxml[0]);
            Assert.Equal("Middle", cnamesxml[1]);
        }

        [Fact]
        public void Set_CertifierFamilyName()
        {
            SetterDeathRecord.CertifierFamilyName = "Last";
            Assert.Equal("Last", SetterDeathRecord.CertifierFamilyName);
        }

        [Fact]
        public void Get_CertifierFamilyName()
        {
            Assert.Equal("Last", DeathRecord1_XML.CertifierFamilyName);
            Assert.Equal("Last", DeathRecord1_JSON.CertifierFamilyName);
            Assert.Equal("Black", DeathCertificateDocument1_JSON.CertifierFamilyName);
        }

        [Fact]
        public void Set_CertifierSuffix()
        {
            SetterDeathRecord.CertifierSuffix = "Jr.";
            Assert.Equal("Jr.", SetterDeathRecord.CertifierSuffix);
        }

        [Fact]
        public void Get_CertifierSuffix()
        {
            Assert.Equal("Jr.", DeathRecord1_XML.CertifierSuffix);
            Assert.Equal("Jr.", DeathRecord1_XML.CertifierSuffix);
        }

        [Fact]
        public void Set_CertifierAddress()
        {
            Dictionary<string, string> caddress = new Dictionary<string, string>();
            caddress.Add("addressLine1", "11 Example Street");
            caddress.Add("addressLine2", "Line 2");
            caddress.Add("addressCity", "Bedford");
            caddress.Add("addressCounty", "Middlesex");
            caddress.Add("addressState", "MA");
            caddress.Add("addressZip", "01730");
            caddress.Add("addressCountry", "US");
            caddress.Add("addressPredir", "W");
            caddress.Add("addressPostdir", "E");
            caddress.Add("addressStname", "Example");
            caddress.Add("addressStnum", "11");
            caddress.Add("addressStdesig", "Street");
            caddress.Add("addressUnitnum", "3");
            SetterDeathRecord.CertifierAddress = caddress;
            Assert.Equal("11 Example Street", SetterDeathRecord.CertifierAddress["addressLine1"]);
            Assert.Equal("Line 2", SetterDeathRecord.CertifierAddress["addressLine2"]);
            Assert.Equal("Bedford", SetterDeathRecord.CertifierAddress["addressCity"]);
            Assert.Equal("Middlesex", SetterDeathRecord.CertifierAddress["addressCounty"]);
            Assert.Equal("MA", SetterDeathRecord.CertifierAddress["addressState"]);
            Assert.Equal("01730", SetterDeathRecord.CertifierAddress["addressZip"]);
            Assert.Equal("US", SetterDeathRecord.CertifierAddress["addressCountry"]);
            Assert.Equal("W", SetterDeathRecord.CertifierAddress["addressPredir"]);
            Assert.Equal("E", SetterDeathRecord.CertifierAddress["addressPostdir"]);
            Assert.Equal("Example", SetterDeathRecord.CertifierAddress["addressStname"]);
            Assert.Equal("11", SetterDeathRecord.CertifierAddress["addressStnum"]);
            Assert.Equal("Street", SetterDeathRecord.CertifierAddress["addressStdesig"]);
            Assert.Equal("3", SetterDeathRecord.CertifierAddress["addressUnitnum"]);
        }

        [Fact]
        public void Get_CertifierAddress()
        {
            Assert.Equal("11 Example Street", DeathRecord1_JSON.CertifierAddress["addressLine1"]);
            Assert.Equal("Line 2", DeathRecord1_JSON.CertifierAddress["addressLine2"]);
            Assert.Equal("Bedford", DeathRecord1_JSON.CertifierAddress["addressCity"]);
            Assert.Equal("Middlesex", DeathRecord1_JSON.CertifierAddress["addressCounty"]);
            Assert.Equal("MA", DeathRecord1_JSON.CertifierAddress["addressState"]);
            Assert.Equal("01730", DeathRecord1_JSON.CertifierAddress["addressZip"]);
            Assert.Equal("US", DeathRecord1_JSON.CertifierAddress["addressCountry"]);
            Assert.Equal("11 Example Street", DeathRecord1_XML.CertifierAddress["addressLine1"]);
            Assert.Equal("Line 2", DeathRecord1_XML.CertifierAddress["addressLine2"]);
            Assert.Equal("Bedford", DeathRecord1_XML.CertifierAddress["addressCity"]);
            Assert.Equal("Middlesex", DeathRecord1_XML.CertifierAddress["addressCounty"]);
            Assert.Equal("MA", DeathRecord1_XML.CertifierAddress["addressState"]);
            Assert.Equal("01730", DeathRecord1_XML.CertifierAddress["addressZip"]);
            Assert.Equal("US", DeathRecord1_XML.CertifierAddress["addressCountry"]);
            Assert.Equal("44 South Street", DeathCertificateDocument2_JSON.CertifierAddress["addressLine1"]);
            Assert.Equal("Bird in Hand", DeathCertificateDocument2_JSON.CertifierAddress["addressCity"]);
            Assert.Equal("PA", DeathCertificateDocument2_JSON.CertifierAddress["addressState"]);
            Assert.Equal("17505", DeathCertificateDocument2_JSON.CertifierAddress["addressZip"]);
            Assert.Equal("US", DeathCertificateDocument2_JSON.CertifierAddress["addressCountry"]);
        }

        // [Fact]
        // public void Set_CertifierQualification()
        // {
        //     Dictionary<string, string> qualification = new Dictionary<string, string>();
        //     qualification.Add("code", "3060");
        //     qualification.Add("system", "urn:oid:2.16.840.1.114222.4.11.7186");
        //     qualification.Add("display", "Physicians and surgeons");
        //     SetterDeathRecord.CertifierQualification = qualification;
        //     Assert.Equal("3060", SetterDeathRecord.CertifierQualification["code"]);
        //     Assert.Equal("urn:oid:2.16.840.1.114222.4.11.7186", SetterDeathRecord.CertifierQualification["system"]);
        //     Assert.Equal("Physicians and surgeons", SetterDeathRecord.CertifierQualification["display"]);
        // }

        // [Fact]
        // public void Get_CertifierQualification()
        // {
        //     Assert.Equal("434641000124105", DeathRecord1_JSON.CertifierQualification["code"]);
        //     Assert.Equal(CodeSystems.SCT, DeathRecord1_XML.CertifierQualification["system"]);
        //     Assert.Equal("Physician certified and pronounced death certificate", DeathRecord1_JSON.CertifierQualification["display"]);
        //     Assert.Equal("434641000124105", DeathRecord1_XML.CertifierQualification["code"]);
        //     Assert.Equal(CodeSystems.SCT, DeathRecord1_JSON.CertifierQualification["system"]);
        //     Assert.Equal("Physician certified and pronounced death certificate", DeathRecord1_XML.CertifierQualification["display"]);
        // }

        // [Fact]
        // public void Set_CertifierLicenseNumber()
        // {
        //     SetterDeathRecord.CertifierLicenseNumber = "789123456";
        //     Assert.Equal("789123456", SetterDeathRecord.CertifierLicenseNumber);
        // }

        // [Fact]
        // public void Get_CertifierLicenseNumber()
        // {
        //     Assert.Equal("789123456", DeathRecord1_JSON.CertifierLicenseNumber);
        //     Assert.Equal("789123456", DeathRecord1_XML.CertifierLicenseNumber);
        // }

        [Fact]
        public void Set_ContributingConditions()
        {
            SetterDeathRecord.ContributingConditions = "Example Contributing Condition";
            Assert.Equal("Example Contributing Condition", SetterDeathRecord.ContributingConditions);
        }

        [Fact]
        public void Get_ContributingConditions()
        {
            Assert.Equal("hypertensive heart disease", DeathCertificateDocument2_JSON.ContributingConditions);
            Assert.Equal("Example Contributing Conditions", DeathRecord1_XML.ContributingConditions);
        }

        [Fact]
        public void Set_COD1A()
        {
            SetterDeathRecord.COD1A = "Rupture of myocardium";
            Assert.Equal("Rupture of myocardium", SetterDeathRecord.COD1A);
        }

        [Fact]
        public void Get_COD1A()
        {
            Assert.Equal("Cardiopulmonary arrest", DeathCertificateDocument2_JSON.COD1A);
            Assert.Equal("Cardiopulmonary arrest", DeathCertificateDocument2_XML.COD1A);
        }

        [Fact]
        public void Set_INTERVAL1A()
        {
            SetterDeathRecord.INTERVAL1A = "minutes";
            Assert.Equal("minutes", SetterDeathRecord.INTERVAL1A);
        }

        [Fact]
        public void Get_INTERVAL1A()
        {
            Assert.Equal("4 hours", DeathCertificateDocument2_JSON.INTERVAL1A);
            Assert.Equal("4 hours", DeathCertificateDocument2_XML.INTERVAL1A);
        }

        // [Fact]
        // public void Set_CODE1A()
        // {
        //     Dictionary<string, string> code = new Dictionary<string, string>();
        //     code.Add("code", "I21.0");
        //     code.Add("system", "http://hl7.org/fhir/sid/icd-10");
        //     code.Add("display", "Acute transmural myocardial infarction of anterior wall");
        //     SetterDeathRecord.CODE1A = code;
        //     Assert.Equal("I21.0", SetterDeathRecord.CODE1A["code"]);
        //     Assert.Equal("http://hl7.org/fhir/sid/icd-10", SetterDeathRecord.CODE1A["system"]);
        //     Assert.Equal("Acute transmural myocardial infarction of anterior wall", SetterDeathRecord.CODE1A["display"]);
        // }

        // [Fact]
        // public void Get_CODE1A()
        // {
        //     Assert.Equal("I21.0", DeathRecord1_JSON.CODE1A["code"]);
        //     Assert.Equal("http://hl7.org/fhir/sid/icd-10", DeathRecord1_XML.CODE1A["system"]);
        //     Assert.Equal("Acute transmural myocardial infarction of anterior wall", DeathRecord1_JSON.CODE1A["display"]);
        //     Assert.Equal("I21.0", DeathRecord1_XML.CODE1A["code"]);
        //     Assert.Equal("http://hl7.org/fhir/sid/icd-10", DeathRecord1_JSON.CODE1A["system"]);
        //     Assert.Equal("Acute transmural myocardial infarction of anterior wall", DeathRecord1_XML.CODE1A["display"]);
        // }

        [Fact]
        public void Set_COD1B()
        {
            SetterDeathRecord.COD1B = "cause 2";
            Assert.Equal("cause 2", SetterDeathRecord.COD1B);
        }

        [Fact]
        public void Get_COD1B()
        {
            Assert.Equal("Eclampsia", DeathCertificateDocument2_JSON.COD1B);
            Assert.Equal("Eclampsia", DeathCertificateDocument2_XML.COD1B);
        }

        [Fact]
        public void Set_INTERVAL1B()
        {
            SetterDeathRecord.INTERVAL1B = "days";
            Assert.Equal("days", SetterDeathRecord.INTERVAL1B);
        }

        [Fact]
        public void Get_INTERVAL1B()
        {
            Assert.Equal("3 months", DeathCertificateDocument2_JSON.INTERVAL1B);
            Assert.Equal("3 months", DeathCertificateDocument2_XML.INTERVAL1B);
        }

        // [Fact]
        // public void Set_CODE1B()
        // {
        //     Dictionary<string, string> code = new Dictionary<string, string>();
        //     code.Add("code", "code 2");
        //     code.Add("system", "system 2");
        //     code.Add("display", "display 2");
        //     SetterDeathRecord.CODE1B = code;
        //     Assert.Equal("code 2", SetterDeathRecord.CODE1B["code"]);
        //     Assert.Equal("system 2", SetterDeathRecord.CODE1B["system"]);
        //     Assert.Equal("display 2", SetterDeathRecord.CODE1B["display"]);
        // }

        // [Fact]
        // public void Get_CODE1B()
        // {
        //     Assert.Equal("I21.9", DeathRecord1_JSON.CODE1B["code"]);
        //     Assert.Equal("http://hl7.org/fhir/sid/icd-10", DeathRecord1_XML.CODE1B["system"]);
        //     Assert.Equal("Acute myocardial infarction, unspecified", DeathRecord1_JSON.CODE1B["display"]);
        //     Assert.Equal("I21.9", DeathRecord1_XML.CODE1B["code"]);
        //     Assert.Equal("http://hl7.org/fhir/sid/icd-10", DeathRecord1_JSON.CODE1B["system"]);
        //     Assert.Equal("Acute myocardial infarction, unspecified", DeathRecord1_XML.CODE1B["display"]);
        // }

        [Fact]
        public void Set_COD1C()
        {
            SetterDeathRecord.COD1C = "cause 3";
            Assert.Equal("cause 3", SetterDeathRecord.COD1C);
        }

        [Fact]
        public void Get_COD1C()
        {
            Assert.Equal("Coronary artery thrombosis", DeathCertificateDocument2_XML.COD1C);
        }

        [Fact]
        public void Set_INTERVAL1C()
        {
            SetterDeathRecord.INTERVAL1C = "interval 3";
            Assert.Equal("interval 3", SetterDeathRecord.INTERVAL1C);
        }

        [Fact]
        public void Get_INTERVAL1C()
        {
            Assert.Equal("3 months", DeathCertificateDocument2_XML.INTERVAL1C);
        }

        // [Fact]
        // public void Set_CODE1C()
        // {
        //     Dictionary<string, string> code = new Dictionary<string, string>();
        //     code.Add("code", "code 3");
        //     code.Add("system", "system 3");
        //     code.Add("display", "display 3");
        //     SetterDeathRecord.CODE1C = code;
        //     Assert.Equal("code 3", SetterDeathRecord.CODE1C["code"]);
        //     Assert.Equal("system 3", SetterDeathRecord.CODE1C["system"]);
        //     Assert.Equal("display 3", SetterDeathRecord.CODE1C["display"]);
        // }

        [Fact]
        public void Set_COD1D()
        {
            SetterDeathRecord.COD1D = "cause 4";
            Assert.Equal("cause 4", SetterDeathRecord.COD1D);
        }

        [Fact]
        public void Get_COD1D()
        {
            Assert.Equal("Atherosclerotic coronary artery disease", DeathCertificateDocument2_XML.COD1D);
        }

        [Fact]
        public void Set_INTERVAL1D()
        {
            SetterDeathRecord.INTERVAL1D = "interval 4";
            Assert.Equal("interval 4", SetterDeathRecord.INTERVAL1D);
        }

        [Fact]
        public void Get_INTERVAL1D()
        {
            Assert.Equal("3 months", DeathCertificateDocument2_XML.INTERVAL1D);
        }

        // [Fact]
        // public void Set_CODE1D()
        // {
        //     Dictionary<string, string> code = new Dictionary<string, string>();
        //     code.Add("code", "code 4");
        //     code.Add("system", "system 4");
        //     code.Add("display", "display 4");
        //     SetterDeathRecord.CODE1D = code;
        //     Assert.Equal("code 4", SetterDeathRecord.CODE1D["code"]);
        //     Assert.Equal("system 4", SetterDeathRecord.CODE1D["system"]);
        //     Assert.Equal("display 4", SetterDeathRecord.CODE1D["display"]);
        // }

        [Fact]
        public void GetCompositionReferencesJson()
        {
            // Grab Composition
            ParserSettings parserSettings = new ParserSettings
            {
                AcceptUnknownMembers = true,
                AllowUnrecognizedEnums = true,
                PermissiveParsing = true
            };
            string bundle = File.ReadAllText(FixturePath("fixtures/json/DeathRecord1.json"));
            FhirJsonParser parser = new FhirJsonParser(parserSettings);
            Bundle b = parser.Parse<Bundle>(bundle);
            var compositionEntry = b.Entry.FirstOrDefault(entry => entry.Resource is Composition);
            var covered = false;
            if (compositionEntry != null)
            {
                Composition comp = (Composition)compositionEntry.Resource;
                Assert.Equal(4, comp.Section.Count);

                Composition.SectionComponent demographics = comp.Section.Where(s => s.Code.Coding.First().Code == "DecedentDemographics").First();
                Assert.Equal(11, demographics.Entry.Count);

                Composition.SectionComponent investigation = comp.Section.Where(s => s.Code.Coding.First().Code == "DeathInvestigation").First();
                Assert.Equal(8, investigation.Entry.Count);

                Composition.SectionComponent certification = comp.Section.Where(s => s.Code.Coding.First().Code == "DeathCertification").First();
                Assert.Equal(8, certification.Entry.Count);

                Composition.SectionComponent disposition = comp.Section.Where(s => s.Code.Coding.First().Code == "DecedentDisposition").First();
                Assert.Equal(4, disposition.Entry.Count);

                covered = true;
            }
            Assert.True(covered);
        }

        [Fact]
        public void GetCompositionReferencesXml()
        {
            // Grab Composition
            ParserSettings parserSettings = new ParserSettings
            {
                AcceptUnknownMembers = true,
                AllowUnrecognizedEnums = true,
                PermissiveParsing = true
            };
            string bundle = File.ReadAllText(FixturePath("fixtures/xml/DeathRecord1.xml"));
            FhirXmlParser parser = new FhirXmlParser(parserSettings);
            Bundle b = parser.Parse<Bundle>(bundle);
            var compositionEntry = b.Entry.FirstOrDefault(entry => entry.Resource is Composition);
            var covered = false;
            if (compositionEntry != null)
            {
                Composition comp = (Composition)compositionEntry.Resource;
                Assert.Equal(4, comp.Section.Count);

                Composition.SectionComponent demographics = comp.Section.Where(s => s.Code.Coding.First().Code == "DecedentDemographics").First();
                Assert.Equal(13, demographics.Entry.Count);

                Composition.SectionComponent investigation = comp.Section.Where(s => s.Code.Coding.First().Code == "DeathInvestigation").First();
                Assert.Equal(8, investigation.Entry.Count);

                Composition.SectionComponent certification = comp.Section.Where(s => s.Code.Coding.First().Code == "DeathCertification").First();
                Assert.Equal(7, certification.Entry.Count);

                Composition.SectionComponent disposition = comp.Section.Where(s => s.Code.Coding.First().Code == "DecedentDisposition").First();
                Assert.Equal(4, disposition.Entry.Count);

                covered = true;
            }
            Assert.True(covered);

        }

        [Fact]
        public void Set_StateSpecific()
        {
            SetterDeathRecord.StateSpecific = "State Specific Info Test";
            Assert.Equal("State Specific Info Test", SetterDeathRecord.StateSpecific);
        }

        [Fact]
        public void Get_StateSpecific()
        {
            Assert.Equal("State Specific Content", DeathRecord1_JSON.StateSpecific);
            Assert.Equal("State Specific Content", DeathCertificateDocument1_JSON.StateSpecific);
            Assert.Equal("State Specific Content", DeathRecord1_XML.StateSpecific);
        }

        [Fact]
        public void Set_FilingFormat()
        {
            SetterDeathRecord.FilingFormatHelper = ValueSets.FilingFormat.Electronic;
            Assert.Equal("electronic", SetterDeathRecord.FilingFormat["code"]);
            Assert.Equal("Electronic", SetterDeathRecord.FilingFormat["display"]);
            Assert.Equal(VRDR.CodeSystems.FilingFormat, SetterDeathRecord.FilingFormat["system"]);
        }

        [Fact]
        public void Get_FilingFormat()
        {
            Assert.Equal("electronic", DeathRecord1_JSON.FilingFormatHelper);
            Assert.Equal("electronic", DeathCertificateDocument1_JSON.FilingFormatHelper);
            Assert.Equal("electronic", DeathRecord1_XML.FilingFormatHelper);
        }

        [Fact]
        public void Set_ReplaceStatus()
        {
            SetterDeathRecord.ReplaceStatusHelper = ValueSets.ReplaceStatus.Original_Record;
            Assert.Equal("original", SetterDeathRecord.ReplaceStatus["code"]);
            Assert.Equal("original record", SetterDeathRecord.ReplaceStatus["display"]);
            Assert.Equal(VRDR.CodeSystems.ReplaceStatus, SetterDeathRecord.ReplaceStatus["system"]);
        }

        [Fact]
        public void Get_ReplaceStatus()
        {
            Assert.Equal("original", DeathRecord1_JSON.ReplaceStatusHelper);
            Assert.Equal("original", DeathCertificateDocument1_JSON.ReplaceStatusHelper);
            Assert.Equal("original", DeathRecord1_XML.ReplaceStatusHelper);
        }
        [Fact]
        public void Set_GivenNames()
        {
            SetterDeathRecord.GivenNames = new string[] { "Example", "Something", "Middle" };
            Assert.Equal("Example", SetterDeathRecord.GivenNames[0]);
            Assert.Equal("Something", SetterDeathRecord.GivenNames[1]);
            Assert.Equal("Middle", SetterDeathRecord.GivenNames[2]);
        }

        [Fact]
        public void Get_GivenNames()
        {
            Assert.Equal("Mædęlyñ", DeathRecord1_JSON.GivenNames[0]);
            Assert.Equal("Middle", DeathRecord1_JSON.GivenNames[1]);
            Assert.Equal("Mædęlyñ", DeathRecord1_XML.GivenNames[0]);
            Assert.Equal("Middle", DeathRecord1_XML.GivenNames[1]);
            Assert.Equal("Madelyn", DeathCertificateDocument1_JSON.GivenNames[0]);
            Assert.Single(DeathCertificateDocument1_JSON.GivenNames);
        }

        [Fact]
        public void Set_FamilyName()
        {
            SetterDeathRecord.FamilyName = "Last";
            Assert.Equal("Last", SetterDeathRecord.FamilyName);
        }

        [Fact]
        public void Get_FamilyName()
        {
            Assert.Equal("Pãtêl", DeathRecord1_JSON.FamilyName);
            Assert.Equal("Pãtêl", DeathRecord1_XML.FamilyName);
        }

        [Fact]
        public void Set_Suffix()
        {
            SetterDeathRecord.Suffix = "Jr.";
            Assert.Equal("Jr.", SetterDeathRecord.Suffix);
        }

        [Fact]
        public void Get_Suffix()
        {
            Assert.Equal("Jr.", DeathRecord1_JSON.Suffix);
            Assert.Equal("Jr.", DeathRecord1_XML.Suffix);
        }

        // v1.3 OBE tests
        [Fact]
        public void testGenderSetterGetter()
        {
            SetterDeathRecord.Gender = "male";
            Assert.Equal("male", SetterDeathRecord.Gender);
            SetterDeathRecord.Gender = "Male";
            Assert.Equal("male", SetterDeathRecord.Gender);
            SetterDeathRecord.Gender = "m";
            Assert.Equal("male", SetterDeathRecord.Gender);
            SetterDeathRecord.Gender = "M";
            Assert.Equal("male", SetterDeathRecord.Gender);
            SetterDeathRecord.Gender = "m";
            Assert.NotEqual("female", SetterDeathRecord.Gender);
            SetterDeathRecord.Gender = "female";
            Assert.Equal("female", SetterDeathRecord.Gender);
            SetterDeathRecord.Gender = "Female";
            Assert.Equal("female", SetterDeathRecord.Gender);
            SetterDeathRecord.Gender = "f";
            Assert.Equal("female", SetterDeathRecord.Gender);
            SetterDeathRecord.Gender = "F";
            Assert.Equal("female", SetterDeathRecord.Gender);
            SetterDeathRecord.Gender = "o";
            Assert.Equal("other", SetterDeathRecord.Gender);
            SetterDeathRecord.Gender = "O";
            Assert.Equal("other", SetterDeathRecord.Gender);
            SetterDeathRecord.Gender = "other";
            Assert.Equal("other", SetterDeathRecord.Gender);
            SetterDeathRecord.Gender = "Other";
            Assert.Equal("other", SetterDeathRecord.Gender);
            SetterDeathRecord.Gender = "u";
            Assert.Equal("unknown", SetterDeathRecord.Gender);
            SetterDeathRecord.Gender = "U";
            Assert.Equal("unknown", SetterDeathRecord.Gender);
            SetterDeathRecord.Gender = "unknown";
            Assert.Equal("unknown", SetterDeathRecord.Gender);
            SetterDeathRecord.Gender = "Unknown";
            Assert.Equal("unknown", SetterDeathRecord.Gender);
        }

        [Fact]
        public void testGenderGetterFromParsedFile()
        {
            Assert.Equal("female", DeathRecord1_JSON.Gender);
            Assert.Equal("female", DeathRecord1_XML.Gender);
        }

        [Fact]
        public void Set_SexAtDeath()
        {
            SetterDeathRecord.SexAtDeathHelper = "female";
            Assert.Equal("female", SetterDeathRecord.SexAtDeathHelper);
        }

        [Fact]
        public void Get_SexAtDeath()
        {
            Assert.Equal("unknown", DeathRecord1_JSON.SexAtDeathHelper);
            Assert.Equal("unknown", DeathRecord1_XML.SexAtDeathHelper);
        }

        [Fact]
        public void Set_DateOfBirth()
        {
            SetterDeathRecord.DateOfBirth = "1940-02-19";
            Assert.Equal("1940-02-19", SetterDeathRecord.DateOfBirth);
        }

        [Fact]
        public void Get_DateOfBirth()
        {
            Assert.Equal("1940-02-19", DeathRecord1_JSON.DateOfBirth);
            Assert.Equal("1940-02-19", DeathRecord1_XML.DateOfBirth);
        }

        [Fact]
        public void Set_Residence()
        {
            Dictionary<string, string> raddress = new Dictionary<string, string>();
            raddress.Add("addressLine1", "101 Example Street");
            raddress.Add("addressLine2", "Line 2");
            raddress.Add("addressCity", "Bedford");
            raddress.Add("addressCounty", "Middlesex");
            raddress.Add("addressState", "MA");
            raddress.Add("addressZip", "01730");
            raddress.Add("addressCountry", "US");
            raddress.Add("addressCityC", "1234");
            raddress.Add("addressCountyC", "123");
            raddress.Add("addressStnum", "101");
            raddress.Add("addressPredir", "N");
            raddress.Add("addressStname", "St-Jean");
            raddress.Add("addressStdesig", "St");
            raddress.Add("addressPostdir", "W");
            raddress.Add("addressUnitnum", "A");

            SetterDeathRecord.Residence = raddress;

            Assert.Equal("101 Example Street", SetterDeathRecord.Residence["addressLine1"]);
            Assert.Equal("Line 2", SetterDeathRecord.Residence["addressLine2"]);
            Assert.Equal("Bedford", SetterDeathRecord.Residence["addressCity"]);
            Assert.Equal("Middlesex", SetterDeathRecord.Residence["addressCounty"]);
            Assert.Equal("MA", SetterDeathRecord.Residence["addressState"]);
            Assert.Equal("01730", SetterDeathRecord.Residence["addressZip"]);
            Assert.Equal("US", SetterDeathRecord.Residence["addressCountry"]);

            Assert.Equal("1234", SetterDeathRecord.Residence["addressCityC"]);
            Assert.Equal("123", SetterDeathRecord.Residence["addressCountyC"]);
            Assert.Equal("101", SetterDeathRecord.Residence["addressStnum"]);
            Assert.Equal("N", SetterDeathRecord.Residence["addressPredir"]);
            Assert.Equal("St-Jean", SetterDeathRecord.Residence["addressStname"]);
            Assert.Equal("St", SetterDeathRecord.Residence["addressStdesig"]);
            Assert.Equal("W", SetterDeathRecord.Residence["addressPostdir"]);
            Assert.Equal("A", SetterDeathRecord.Residence["addressUnitnum"]);
        }

        [Fact]
        public void Get_Residence()
        {
            Assert.Equal("5590 Lockwood Drive", DeathRecord1_JSON.Residence["addressLine1"]);
            Assert.Equal("", DeathRecord1_JSON.Residence["addressLine2"]);
            Assert.Equal("Danville", DeathRecord1_JSON.Residence["addressCity"]);
            Assert.Equal("Fairfax", DeathRecord1_JSON.Residence["addressCounty"]);
            Assert.Equal("VA", DeathRecord1_JSON.Residence["addressState"]);
            Assert.Equal("01730", DeathRecord1_JSON.Residence["addressZip"]);
            Assert.Equal("US", DeathRecord1_JSON.Residence["addressCountry"]);
            Assert.Equal("5590 Lockwood Drive", DeathRecord1_XML.Residence["addressLine1"]);
            Assert.Equal("", DeathRecord1_XML.Residence["addressLine2"]);
            Assert.Equal("Danville", DeathRecord1_XML.Residence["addressCity"]);
            Assert.Equal("Fairfax", DeathRecord1_XML.Residence["addressCounty"]);
            Assert.Equal("VA", DeathRecord1_XML.Residence["addressState"]);
            Assert.Equal("01730", DeathRecord1_XML.Residence["addressZip"]);
            Assert.Equal("US", DeathRecord1_XML.Residence["addressCountry"]);
        }

        [Fact]
        public void Get_ResidenceWithinCityLimits()
        {
            SetterDeathRecord.ResidenceWithinCityLimitsHelper = ValueSets.YesNoUnknown.No;
            Assert.Equal("N", SetterDeathRecord.ResidenceWithinCityLimits["code"]);
            SetterDeathRecord.ResidenceWithinCityLimitsHelper = ValueSets.YesNoUnknown.Yes;
            Assert.Equal("Y", SetterDeathRecord.ResidenceWithinCityLimits["code"]);
            SetterDeathRecord.ResidenceWithinCityLimitsHelper = ValueSets.YesNoUnknown.Unknown;
            Assert.Equal("UNK", SetterDeathRecord.ResidenceWithinCityLimits["code"]);
        }

        [Fact]
        public void Set_ResidenceWithinCityLimits()
        {
            Assert.Equal("Y", DeathRecord1_JSON.ResidenceWithinCityLimitsHelper);
            Assert.Equal("Y", DeathRecord1_XML.ResidenceWithinCityLimitsHelper);
        }

        [Fact]
        public void Set_SSN()
        {
            SetterDeathRecord.SSN = "123456789";
            Assert.Equal("123456789", SetterDeathRecord.SSN);
        }

        [Fact]
        public void Get_SSN()
        {
            Assert.Equal("987654321", DeathRecord1_JSON.SSN);
            Assert.Equal("987654321", DeathRecord1_XML.SSN);
        }

        [Fact]
        public void Set_Ethnicity()
        {
            SetterDeathRecord.EthnicityLiteral = "Hispanic or Latino, Puerto Rican";
            SetterDeathRecord.Ethnicity2Helper = "Y";
            Assert.Equal("Y", SetterDeathRecord.Ethnicity2Helper);
            Assert.Equal("Hispanic or Latino, Puerto Rican", SetterDeathRecord.EthnicityLiteral);
        }

        [Fact]
        public void Get_Ethnicity()
        {
            Assert.Equal("Y", DeathRecord1_JSON.Ethnicity1Helper);
            Assert.Equal("Y", DeathRecord1_XML.Ethnicity1Helper);
        }

        [Fact]
        public void Set_BirthDate_Partial_Date()
        {
            SetterDeathRecord.BirthYear = 1950;
            SetterDeathRecord.BirthMonth = null;
            SetterDeathRecord.BirthDay = null;
            Assert.Equal(1950, (int)SetterDeathRecord.BirthYear);
            Assert.Null(SetterDeathRecord.BirthMonth);
            Assert.Null(SetterDeathRecord.BirthDay);
            SetterDeathRecord.BirthMonth = -1;
            SetterDeathRecord.BirthDay = -1;
            Assert.Equal(1950, (int)SetterDeathRecord.BirthYear);
            Assert.Equal(-1, SetterDeathRecord.BirthMonth);
            Assert.Equal(-1, SetterDeathRecord.BirthDay);
        }

        [Fact]
        public void Get_BirthDate_Partial_Date()
        {
            DeathRecord dr = new DeathRecord(File.ReadAllText(FixturePath("fixtures/json/BirthAndDeathDateDataAbsent.json")));
            Assert.Equal(-1, dr.BirthYear);
            Assert.Equal(-1, dr.BirthMonth);
            Assert.Equal(24, (int)dr.BirthDay);
        }


        [Fact]
        public void Get_BirthDate_Partial_Date_Roundtrip()
        {
            DeathRecord dr = new DeathRecord(File.ReadAllText(FixturePath("fixtures/json/BirthAndDeathDateDataAbsent.json")));
            IJEMortality ije1 = new IJEMortality(dr);
            Assert.Equal("9999", ije1.DOB_YR);
            Assert.Equal("99", ije1.DOB_MO);
            Assert.Equal("24", ije1.DOB_DY);
            DeathRecord dr1 = ije1.ToDeathRecord();
            Assert.Equal(-1, dr1.BirthYear);
            Assert.Equal(-1, dr1.BirthMonth);
            Assert.Equal(24, (int)dr1.BirthDay);
            Assert.Null(dr1.DateOfBirth);
        }

        [Fact]
        public void Test_StateText_JSON_To_IJE()
        {
            DeathRecord dr = new DeathRecord(File.ReadAllText(FixturePath("fixtures/json/Test_StateText_JSON_To_IJE.json")));
            IJEMortality ije1 = new IJEMortality(dr);
            Assert.Equal("District of Columbia", ije1.STATETEXT_R.Trim());
        }

        [Fact]
        public void Set_Race()
        {
            Tuple<string, string>[] race = new Tuple<string, string>[] { Tuple.Create(NvssRace.White, "Y"), Tuple.Create(NvssRace.NativeHawaiian, "Y"), Tuple.Create(NvssRace.FirstOtherPacificIslanderLiteral, "White, Native Hawaiian or Other Pacific Islander") };
            SetterDeathRecord.Race = race;
            Assert.Equal(race[0], SetterDeathRecord.Race[0]);
            Assert.Equal(race[1], SetterDeathRecord.Race[1]);
            Assert.Equal(race[2], SetterDeathRecord.Race[2]);
        }

        [Fact]
        public void Get_Race()
        {
            Assert.Equal(Tuple.Create(NvssRace.White, "Y"), DeathRecord1_JSON.Race[0]);
            Assert.Equal(Tuple.Create(NvssRace.BlackOrAfricanAmerican, "N"), DeathRecord1_JSON.Race[1]);
            Assert.Equal(Tuple.Create(NvssRace.AmericanIndianOrAlaskanNative, "N"), DeathRecord1_JSON.Race[2]);
            Assert.Equal(Tuple.Create(NvssRace.AsianIndian, "N"), DeathRecord1_JSON.Race[3]);
            Assert.Equal(Tuple.Create(NvssRace.Chinese, "N"), DeathRecord1_JSON.Race[4]);
            Assert.Equal(Tuple.Create(NvssRace.Filipino, "N"), DeathRecord1_JSON.Race[5]);
            Assert.Equal(Tuple.Create(NvssRace.Japanese, "N"), DeathRecord1_JSON.Race[6]);
            Assert.Equal(Tuple.Create(NvssRace.Korean, "N"), DeathRecord1_JSON.Race[7]);
            Assert.Equal(Tuple.Create(NvssRace.Vietnamese, "N"), DeathRecord1_JSON.Race[8]);
            Assert.Equal(Tuple.Create(NvssRace.OtherAsian, "N"), DeathRecord1_JSON.Race[9]);
            Assert.Equal(Tuple.Create(NvssRace.NativeHawaiian, "N"), DeathRecord1_JSON.Race[10]);
            Assert.Equal(Tuple.Create(NvssRace.GuamanianOrChamorro, "N"), DeathRecord1_JSON.Race[11]);
            Assert.Equal(Tuple.Create(NvssRace.Samoan, "N"), DeathRecord1_JSON.Race[12]);
            Assert.Equal(Tuple.Create(NvssRace.OtherPacificIslander, "N"), DeathRecord1_JSON.Race[13]);
            Assert.Equal(Tuple.Create(NvssRace.OtherRace, "N"), DeathRecord1_JSON.Race[14]);

            Assert.Equal(Tuple.Create(NvssRace.White, "Y"), DeathRecord1_XML.Race[0]);
            Assert.Equal(Tuple.Create(NvssRace.BlackOrAfricanAmerican, "N"), DeathRecord1_XML.Race[1]);
            Assert.Equal(Tuple.Create(NvssRace.AmericanIndianOrAlaskanNative, "N"), DeathRecord1_XML.Race[2]);
            Assert.Equal(Tuple.Create(NvssRace.AsianIndian, "N"), DeathRecord1_XML.Race[3]);
            Assert.Equal(Tuple.Create(NvssRace.Chinese, "N"), DeathRecord1_XML.Race[4]);
            Assert.Equal(Tuple.Create(NvssRace.Filipino, "N"), DeathRecord1_XML.Race[5]);
            Assert.Equal(Tuple.Create(NvssRace.Japanese, "N"), DeathRecord1_XML.Race[6]);
            Assert.Equal(Tuple.Create(NvssRace.Korean, "N"), DeathRecord1_XML.Race[7]);
            Assert.Equal(Tuple.Create(NvssRace.Vietnamese, "N"), DeathRecord1_XML.Race[8]);
            Assert.Equal(Tuple.Create(NvssRace.OtherAsian, "N"), DeathRecord1_XML.Race[9]);
            Assert.Equal(Tuple.Create(NvssRace.NativeHawaiian, "N"), DeathRecord1_XML.Race[10]);
            Assert.Equal(Tuple.Create(NvssRace.GuamanianOrChamorro, "N"), DeathRecord1_XML.Race[11]);
            Assert.Equal(Tuple.Create(NvssRace.Samoan, "N"), DeathRecord1_XML.Race[12]);
            Assert.Equal(Tuple.Create(NvssRace.OtherPacificIslander, "N"), DeathRecord1_XML.Race[13]);
            Assert.Equal(Tuple.Create(NvssRace.OtherRace, "N"), DeathRecord1_XML.Race[14]);
        }

        [Fact]
        public void Set_PlaceOfBirth()
        {
            Dictionary<string, string> pobaddress = new Dictionary<string, string>();
            pobaddress.Add("addressLine1", "1011 Example Street");
            pobaddress.Add("addressLine2", "Line 2");
            pobaddress.Add("addressCity", "Bedford");
            pobaddress.Add("addressCounty", "Middlesex");
            pobaddress.Add("addressState", "MA");
            pobaddress.Add("addressZip", "01730");
            pobaddress.Add("addressCountry", "US");
            SetterDeathRecord.PlaceOfBirth = pobaddress;
            Assert.Equal("1011 Example Street", SetterDeathRecord.PlaceOfBirth["addressLine1"]);
            Assert.Equal("Line 2", SetterDeathRecord.PlaceOfBirth["addressLine2"]);
            Assert.Equal("Bedford", SetterDeathRecord.PlaceOfBirth["addressCity"]);
            Assert.Equal("Middlesex", SetterDeathRecord.PlaceOfBirth["addressCounty"]);
            Assert.Equal("MA", SetterDeathRecord.PlaceOfBirth["addressState"]);
            Assert.Equal("01730", SetterDeathRecord.PlaceOfBirth["addressZip"]);
            Assert.Equal("US", SetterDeathRecord.PlaceOfBirth["addressCountry"]);
        }

        [Fact]
        public void Get_PlaceOfBirth()
        {
            Assert.Equal("", DeathRecord1_JSON.PlaceOfBirth["addressLine1"]);
            Assert.Equal("", DeathRecord1_JSON.PlaceOfBirth["addressLine2"]);
            Assert.Equal("Roanoke", DeathRecord1_JSON.PlaceOfBirth["addressCity"]);
            Assert.Equal("", DeathRecord1_JSON.PlaceOfBirth["addressCounty"]);
            Assert.Equal("VA", DeathRecord1_JSON.PlaceOfBirth["addressState"]);
            Assert.Equal("", DeathRecord1_JSON.PlaceOfBirth["addressZip"]);
            Assert.Equal("US", DeathRecord1_JSON.PlaceOfBirth["addressCountry"]);
            Assert.Equal("", DeathRecord1_XML.PlaceOfBirth["addressLine1"]);
            Assert.Equal("", DeathRecord1_XML.PlaceOfBirth["addressLine2"]);
            Assert.Equal("Roanoke", DeathRecord1_XML.PlaceOfBirth["addressCity"]);
            Assert.Equal("", DeathRecord1_XML.PlaceOfBirth["addressCounty"]);
            Assert.Equal("VA", DeathRecord1_XML.PlaceOfBirth["addressState"]);
            Assert.Equal("", DeathRecord1_XML.PlaceOfBirth["addressZip"]);
            Assert.Equal("US", DeathRecord1_XML.PlaceOfBirth["addressCountry"]);
        }

        [Fact]
        public void Set_MaritalStatus()
        {
            SetterDeathRecord.MaritalStatusHelper = ValueSets.MaritalStatus.Never_Married;
            Assert.Equal(ValueSets.MaritalStatus.Never_Married, SetterDeathRecord.MaritalStatus["code"]);
            Assert.Equal(VRDR.CodeSystems.PH_MaritalStatus_HL7_2x, SetterDeathRecord.MaritalStatus["system"]);
            Assert.Equal("Never Married", SetterDeathRecord.MaritalStatus["display"]);
        }

        [Fact]
        public void Get_MaritalStatus()
        {
            Assert.Equal(ValueSets.MaritalStatus.Never_Married, DeathRecord1_JSON.MaritalStatus["code"]);
            Assert.Equal(VRDR.CodeSystems.PH_MaritalStatus_HL7_2x, DeathRecord1_JSON.MaritalStatus["system"]);
            Assert.Equal("Never Married", DeathRecord1_JSON.MaritalStatus["display"]);
            Assert.Equal(ValueSets.MaritalStatus.Never_Married, DeathRecord1_XML.MaritalStatus["code"]);
            Assert.Equal(VRDR.CodeSystems.PH_MaritalStatus_HL7_2x, DeathRecord1_XML.MaritalStatus["system"]);
            Assert.Equal("Never Married", DeathRecord1_XML.MaritalStatus["display"]);
        }

        [Fact]
        public void Set_MaritalStatusEditFlag()
        {
            SetterDeathRecord.MaritalStatusEditFlagHelper = ValueSets.EditBypass0124.Edit_Passed;
            Assert.Equal(ValueSets.EditBypass0124.Edit_Passed, SetterDeathRecord.MaritalStatusEditFlag["code"]);
            Assert.Equal(VRDR.CodeSystems.BypassEditFlag, SetterDeathRecord.MaritalStatusEditFlag["system"]);
            Assert.Equal("Edit Passed", SetterDeathRecord.MaritalStatusEditFlag["display"]);
        }

        [Fact]
        public void Get_MaritalStatusEditFlag()
        {
            Assert.Equal(ValueSets.EditBypass0124.Edit_Passed, DeathRecord1_JSON.MaritalStatusEditFlag["code"]);
            Assert.Equal(VRDR.CodeSystems.BypassEditFlag, DeathRecord1_JSON.MaritalStatusEditFlag["system"]);
            Assert.Equal("Edit Passed", DeathRecord1_JSON.MaritalStatusEditFlag["display"]);
            Assert.Equal(ValueSets.EditBypass0124.Edit_Passed, DeathRecord1_XML.MaritalStatusEditFlag["code"]);
            Assert.Equal(VRDR.CodeSystems.BypassEditFlag, DeathRecord1_XML.MaritalStatusEditFlag["system"]);
            Assert.Equal("Edit Passed", DeathRecord1_XML.MaritalStatusEditFlag["display"]);
        }

        [Fact]
        public void Get_MaritalStatusEditFlagHelper()
        {
            SetterDeathRecord.MaritalStatusEditFlagHelper = ValueSets.EditBypass0124.Edit_Passed;
            Assert.Equal(ValueSets.EditBypass0124.Edit_Passed, SetterDeathRecord.MaritalStatusEditFlagHelper);
        }

        [Fact]
        public void Get_MaritalStatusAndBypass()
        {
            SetterDeathRecord.MaritalStatusHelper = ValueSets.MaritalStatus.Never_Married;
            SetterDeathRecord.MaritalStatusEditFlagHelper = ValueSets.EditBypass0124.Edit_Passed;

            Assert.Equal(ValueSets.MaritalStatus.Never_Married, SetterDeathRecord.MaritalStatus["code"]);
            Assert.Equal(VRDR.CodeSystems.PH_MaritalStatus_HL7_2x, SetterDeathRecord.MaritalStatus["system"]);
            Assert.Equal("Never Married", SetterDeathRecord.MaritalStatus["display"]);
            Assert.Equal(ValueSets.EditBypass0124.Edit_Passed, SetterDeathRecord.MaritalStatusEditFlagHelper);
        }

        [Fact]
        public void Set_MaritalDescriptor()
        {
            SetterDeathRecord.MaritalStatusLiteral = "Single";
            Assert.Equal("Single", SetterDeathRecord.MaritalStatusLiteral);
        }

        [Fact]
        public void Get_MaritalDescriptor()
        {
            Assert.Equal("Single", DeathRecord1_JSON.MaritalStatusLiteral);
            Assert.Equal("Single", DeathRecord1_XML.MaritalStatusLiteral);
        }

        [Fact]
        public void Set_FatherGivenNames()
        {
            string[] fnames = { "Father", "Middle" };
            SetterDeathRecord.FatherGivenNames = fnames;
            Assert.Equal("Father", SetterDeathRecord.FatherGivenNames[0]);
            Assert.Equal("Middle", SetterDeathRecord.FatherGivenNames[1]);
        }

        [Fact]
        public void Get_FatherGivenNames()
        {
            Assert.Equal("Father", DeathRecord1_JSON.FatherGivenNames[0]);
            Assert.Equal("Middle", DeathRecord1_JSON.FatherGivenNames[1]);
            Assert.Equal("Father", DeathRecord1_XML.FatherGivenNames[0]);
            Assert.Equal("Middle", DeathRecord1_XML.FatherGivenNames[1]);
        }

        [Fact]
        public void Set_FatherFamilyName()
        {
            SetterDeathRecord.FatherFamilyName = "Last";
            Assert.Equal("Last", SetterDeathRecord.FatherFamilyName);
        }

        [Fact]
        public void Get_FatherFamilyName()
        {
            Assert.Equal("Last", DeathRecord1_JSON.FatherFamilyName);
            Assert.Equal("Last", DeathRecord1_XML.FatherFamilyName);
        }

        [Fact]
        public void Set_FatherSuffix()
        {
            SetterDeathRecord.FatherSuffix = "Sr.";
            Assert.Equal("Sr.", SetterDeathRecord.FatherSuffix);
        }

        [Fact]
        public void Get_FatherSuffix()
        {
            Assert.Equal("Sr.", DeathRecord1_JSON.FatherSuffix);
            Assert.Equal("Sr.", DeathRecord1_XML.FatherSuffix);
        }

        [Fact]
        public void Set_MotherGivenNames()
        {
            string[] mnames = { "Mother", "Middle" };
            SetterDeathRecord.MotherGivenNames = mnames;
            Assert.Equal("Mother", SetterDeathRecord.MotherGivenNames[0]);
            Assert.Equal("Middle", SetterDeathRecord.MotherGivenNames[1]);
        }

        [Fact]
        public void Get_MotherGivenNames()
        {
            Assert.Equal("Mother", DeathRecord1_JSON.MotherGivenNames[0]);
            Assert.Equal("Middle", DeathRecord1_JSON.MotherGivenNames[1]);
            Assert.Equal("Mother", DeathRecord1_XML.MotherGivenNames[0]);
            Assert.Equal("Middle", DeathRecord1_XML.MotherGivenNames[1]);
        }

        [Fact]
        public void Set_MotherMaidenName()
        {
            SetterDeathRecord.MotherMaidenName = "Maiden";
            Assert.Equal("Maiden", SetterDeathRecord.MotherMaidenName);
        }

        [Fact]
        public void Get_MotherMaidenName()
        {
            Assert.Equal("Maiden", DeathRecord1_JSON.MotherMaidenName);
            Assert.Equal("Maiden", DeathRecord1_XML.MotherMaidenName);
        }

        [Fact]
        public void Set_MotherSuffix()
        {
            SetterDeathRecord.MotherSuffix = "Dr.";
            Assert.Equal("Dr.", SetterDeathRecord.MotherSuffix);
        }

        [Fact]
        public void Get_MotherSuffix()
        {
            Assert.Equal("Dr.", DeathRecord1_JSON.MotherSuffix);
            Assert.Equal("Dr.", DeathRecord1_XML.MotherSuffix);
        }

        [Fact]
        public void Set_ContactRelationship()
        {
            Dictionary<string, string> relationship = new Dictionary<string, string>();
            relationship.Add("text", "sibling");
            SetterDeathRecord.ContactRelationship = relationship;
            Assert.Equal("sibling", SetterDeathRecord.ContactRelationship["text"]);
        }

        [Fact]
        public void Get_ContactRelationship()
        {
            Assert.Equal("Friend of family", DeathRecord1_JSON.ContactRelationship["text"]);
            Assert.Equal("Friend of family", DeathRecord1_XML.ContactRelationship["text"]);
        }

        [Fact]
        public void Set_SpouseLiving()
        {
            SetterDeathRecord.SpouseAliveHelper = ValueSets.YesNoUnknownNotApplicable.Yes;
            Assert.Equal(ValueSets.YesNoUnknownNotApplicable.Yes, SetterDeathRecord.SpouseAlive["code"]);
            Assert.Equal(VRDR.CodeSystems.YesNo, SetterDeathRecord.SpouseAlive["system"]);
            Assert.Equal("Yes", SetterDeathRecord.SpouseAlive["display"]);
        }

        [Fact]
        public void Get_SpouseLiving()
        {
            Assert.Equal(ValueSets.YesNoUnknownNotApplicable.Yes, DeathRecord1_JSON.SpouseAlive["code"]);
            Assert.Equal(VRDR.CodeSystems.YesNo, DeathRecord1_JSON.SpouseAlive["system"]);
            Assert.Equal("Yes", DeathRecord1_JSON.SpouseAlive["display"]);
            Assert.Equal(ValueSets.YesNoUnknownNotApplicable.Yes, DeathRecord1_XML.SpouseAlive["code"]);
            Assert.Equal(VRDR.CodeSystems.YesNo, DeathRecord1_XML.SpouseAlive["system"]);
            Assert.Equal("Yes", DeathRecord1_XML.SpouseAlive["display"]);
        }

        [Fact]
        public void Set_SpouseGivenNames()
        {
            string[] spnames = { "Spouse", "Middle" };
            SetterDeathRecord.SpouseGivenNames = spnames;
            Assert.Equal("Spouse", SetterDeathRecord.SpouseGivenNames[0]);
            Assert.Equal("Middle", SetterDeathRecord.SpouseGivenNames[1]);
        }

        [Fact]
        public void Get_SpouseGivenNames()
        {
            Assert.Equal("Spouse", DeathRecord1_JSON.SpouseGivenNames[0]);
            Assert.Equal("Middle", DeathRecord1_JSON.SpouseGivenNames[1]);
            Assert.Equal("Spouse", DeathRecord1_XML.SpouseGivenNames[0]);
            Assert.Equal("Middle", DeathRecord1_XML.SpouseGivenNames[1]);
        }

        [Fact]
        public void Set_SpouseFamilyName()
        {
            SetterDeathRecord.SpouseFamilyName = "Last";
            Assert.Equal("Last", SetterDeathRecord.SpouseFamilyName);
        }

        [Fact]
        public void Get_SpouseFamilyName()
        {
            Assert.Equal("Last", DeathRecord1_JSON.SpouseFamilyName);
            Assert.Equal("Last", DeathRecord1_XML.SpouseFamilyName);
        }

        [Fact]
        public void Set_SpouseMaidenName()
        {
            SetterDeathRecord.SpouseMaidenName = "Maiden";
            Assert.Equal("Maiden", SetterDeathRecord.SpouseMaidenName);
        }

        [Fact]
        public void Get_SpouseMaidenName()
        {
            Assert.Equal("Maiden", DeathRecord1_JSON.SpouseMaidenName);
            Assert.Equal("Maiden", DeathRecord1_XML.SpouseMaidenName);
        }
        [Fact]
        public void Set_SpouseSuffix()
        {
            SetterDeathRecord.SpouseSuffix = "Ph.D.";
            Assert.Equal("Ph.D.", SetterDeathRecord.SpouseSuffix);
        }

        [Fact]
        public void Get_SpouseSuffix()
        {
            Assert.Equal("Ph.D.", DeathRecord1_JSON.SpouseSuffix);
            Assert.Equal("Ph.D.", DeathRecord1_XML.SpouseSuffix);
        }

        [Fact]
        public void Set_EducationLevel()
        {
            SetterDeathRecord.EducationLevelHelper = VRDR.ValueSets.EducationLevel.Bachelors_Degree;
            Assert.Equal(VRDR.ValueSets.EducationLevel.Bachelors_Degree, SetterDeathRecord.EducationLevel["code"]);
            Assert.Equal(VRDR.CodeSystems.DegreeLicenceAndCertificate, SetterDeathRecord.EducationLevel["system"]);
            Assert.Equal("Bachelor's degree", SetterDeathRecord.EducationLevel["display"]);
            SetterDeathRecord.EducationLevelHelper = VRDR.ValueSets.EducationLevel.Associates_Or_Technical_Degree_Complete;
            Assert.Equal(VRDR.ValueSets.EducationLevel.Associates_Or_Technical_Degree_Complete, SetterDeathRecord.EducationLevelHelper);

        }

        [Fact]
        public void Get_EducationLevel()
        {
            Assert.Equal(VRDR.ValueSets.EducationLevel.Bachelors_Degree, DeathRecord1_JSON.EducationLevelHelper);
            Assert.Equal(VRDR.CodeSystems.DegreeLicenceAndCertificate, DeathRecord1_JSON.EducationLevel["system"]);
            Assert.Equal("Bachelor's Degree", DeathRecord1_JSON.EducationLevel["display"]);
            Assert.Equal(VRDR.ValueSets.EducationLevel.Bachelors_Degree, DeathRecord1_XML.EducationLevelHelper);
            Assert.Equal(VRDR.CodeSystems.DegreeLicenceAndCertificate, DeathRecord1_XML.EducationLevel["system"]);
            Assert.Equal("Bachelor's Degree", DeathRecord1_XML.EducationLevel["display"]);
        }

        [Fact]
        public void Set_EducationLevelEditFlag()
        {
            Dictionary<string, string> elef = new Dictionary<string, string>();
            elef.Add("code", VRDR.ValueSets.EditBypass01234.Edit_Failed_Data_Queried_And_Verified);
            elef.Add("system", VRDR.CodeSystems.BypassEditFlag);
            elef.Add("display", "Edit Failed, Data Queried, and Verified");
            SetterDeathRecord.EducationLevelEditFlag = elef;
            Assert.Equal(VRDR.ValueSets.EditBypass01234.Edit_Failed_Data_Queried_And_Verified, SetterDeathRecord.EducationLevelEditFlag["code"]);
            Assert.Equal(VRDR.CodeSystems.BypassEditFlag, SetterDeathRecord.EducationLevelEditFlag["system"]);
            Assert.Equal("Edit Failed, Data Queried, and Verified", SetterDeathRecord.EducationLevelEditFlag["display"]);
            SetterDeathRecord.EducationLevelEditFlagHelper = VRDR.ValueSets.EditBypass01234.Edit_Passed;
            Assert.Equal(VRDR.ValueSets.EditBypass01234.Edit_Passed, SetterDeathRecord.EducationLevelEditFlagHelper);
            Assert.Equal(VRDR.CodeSystems.BypassEditFlag, SetterDeathRecord.EducationLevelEditFlag["system"]);
            Assert.Equal("Edit Passed", SetterDeathRecord.EducationLevelEditFlag["display"]);
        }

        [Fact]
        public void Get_EducationLevelEditFlag()
        {
            Assert.Equal(VRDR.ValueSets.EditBypass01234.Edit_Passed, DeathRecord1_JSON.EducationLevelEditFlagHelper);
            Assert.Equal(VRDR.CodeSystems.BypassEditFlag, DeathRecord1_JSON.EducationLevelEditFlag["system"]);
            Assert.Equal("Edit Passed", DeathRecord1_JSON.EducationLevelEditFlag["display"]);
            Assert.Equal(VRDR.ValueSets.EditBypass01234.Edit_Passed, DeathRecord1_XML.EducationLevelEditFlagHelper);
            Assert.Equal(VRDR.CodeSystems.BypassEditFlag, DeathRecord1_XML.EducationLevelEditFlag["system"]);
            Assert.Equal("Edit Passed", DeathRecord1_XML.EducationLevelEditFlag["display"]);
        }

        [Fact]
        public void Set_ActivityAtTimeOfDeath()
        {
            SetterDeathRecord.ActivityAtDeathHelper = VRDR.ValueSets.ActivityAtTimeOfDeath.While_Resting_Sleeping_Eating_Or_Engaging_In_Other_Vital_Activities;
            Assert.Equal(VRDR.ValueSets.ActivityAtTimeOfDeath.While_Resting_Sleeping_Eating_Or_Engaging_In_Other_Vital_Activities, SetterDeathRecord.ActivityAtDeath["code"]);
            Assert.Equal(VRDR.CodeSystems.ActivityAtTimeOfDeath, SetterDeathRecord.ActivityAtDeath["system"]);
            Assert.Equal("While resting, sleeping, eating, or engaging in other vital activities", SetterDeathRecord.ActivityAtDeath["display"]);
            SetterDeathRecord.ActivityAtDeathHelper = VRDR.ValueSets.ActivityAtTimeOfDeath.While_Resting_Sleeping_Eating_Or_Engaging_In_Other_Vital_Activities;
            Assert.Equal(VRDR.ValueSets.ActivityAtTimeOfDeath.While_Resting_Sleeping_Eating_Or_Engaging_In_Other_Vital_Activities, SetterDeathRecord.ActivityAtDeathHelper);

        }
        [Fact]
        public void Get_ActivityAtTimeOfDeath()
        {
            Assert.Equal(VRDR.ValueSets.ActivityAtTimeOfDeath.While_Engaged_In_Leisure_Activities, DeathCertificateDocument2_JSON.ActivityAtDeathHelper);
        }

        [Fact]
        public void Set_AutomatedUnderlyingCOD()
        {
            SetterDeathRecord.AutomatedUnderlyingCOD = "I131";
            Assert.Equal("I131", SetterDeathRecord.AutomatedUnderlyingCOD);
            SetterDeathRecord.AutomatedUnderlyingCOD = "I13.1";
            Assert.Equal("I13.1", SetterDeathRecord.AutomatedUnderlyingCOD);
            SetterDeathRecord.AutomatedUnderlyingCOD = "I13.";
            Assert.Equal("I13.", SetterDeathRecord.AutomatedUnderlyingCOD);
            SetterDeathRecord.AutomatedUnderlyingCOD = "I13";
            Assert.Equal("I13", SetterDeathRecord.AutomatedUnderlyingCOD);
        }

        [Fact]
        public void Get_AutomatedUnderlyingCOD()
        {
            Assert.Equal("J96.0", DeathCertificateDocument2_JSON.AutomatedUnderlyingCOD);
            Assert.Equal("J96.0", CauseOfDeathCodedContentBundle1_JSON.AutomatedUnderlyingCOD);
        }

        [Fact]
        public void Set_ManUnderlyingCOD()
        {
            SetterDeathRecord.ManUnderlyingCOD = "I131";
            Assert.Equal("I131", SetterDeathRecord.ManUnderlyingCOD);
            SetterDeathRecord.ManUnderlyingCOD = "I13.1";
            Assert.Equal("I13.1", SetterDeathRecord.ManUnderlyingCOD);
            SetterDeathRecord.ManUnderlyingCOD = "I13.";
            Assert.Equal("I13.", SetterDeathRecord.ManUnderlyingCOD);
            SetterDeathRecord.ManUnderlyingCOD = "I13";
            Assert.Equal("I13", SetterDeathRecord.ManUnderlyingCOD);
        }
        [Fact]
        public void Get_ManUnderlyingCOD()
        {
            Assert.Equal("J96.0", DeathCertificateDocument2_JSON.ManUnderlyingCOD);
            Assert.Equal("J96.0", CauseOfDeathCodedContentBundle1_JSON.ManUnderlyingCOD);
        }

        [Fact]
        public void Set_PlaceOfInjury()
        {
            SetterDeathRecord.PlaceOfInjuryHelper = ValueSets.PlaceOfInjury.Home;
            Assert.Equal(ValueSets.PlaceOfInjury.Home, SetterDeathRecord.PlaceOfInjuryHelper);
        }
        [Fact]
        public void Get_PlaceOfInjury()
        {
            Assert.Equal(ValueSets.PlaceOfInjury.Home, DeathCertificateDocument2_JSON.PlaceOfInjuryHelper);
            Assert.Equal(ValueSets.PlaceOfInjury.Home, CauseOfDeathCodedContentBundle1_JSON.PlaceOfInjuryHelper);
        }
        [Fact]
        public void Set_EditedRaceCodes()
        {
            SetterDeathRecord.FirstEditedRaceCodeHelper = ValueSets.RaceCode.African;
            SetterDeathRecord.SecondEditedRaceCodeHelper = ValueSets.RaceCode.Asian;
            SetterDeathRecord.ThirdEditedRaceCodeHelper = ValueSets.RaceCode.Blackfeet;
            SetterDeathRecord.FourthEditedRaceCodeHelper = ValueSets.RaceCode.Jamestown_Sklallam;
            SetterDeathRecord.FifthEditedRaceCodeHelper = ValueSets.RaceCode.Kaw;
            SetterDeathRecord.SixthEditedRaceCodeHelper = ValueSets.RaceCode.Madagascar;
            SetterDeathRecord.SeventhEditedRaceCodeHelper = ValueSets.RaceCode.Okinawan;
            SetterDeathRecord.EighthEditedRaceCodeHelper = ValueSets.RaceCode.Zaire;
            SetterDeathRecord.FirstAmericanIndianRaceCodeHelper = ValueSets.RaceCode.Navajo;
            SetterDeathRecord.SecondAmericanIndianRaceCodeHelper = ValueSets.RaceCode.Stockbridgemunsee_Community_Of_Mohican_Indians_Of_Wisconsin;
            SetterDeathRecord.FirstOtherAsianRaceCodeHelper = ValueSets.RaceCode.Malaysian;
            SetterDeathRecord.SecondOtherAsianRaceCodeHelper = ValueSets.RaceCode.Burmese;
            SetterDeathRecord.FirstOtherPacificIslanderRaceCodeHelper = ValueSets.RaceCode.Taiwanese;
            SetterDeathRecord.SecondOtherPacificIslanderRaceCodeHelper = ValueSets.RaceCode.New_Hebrides;
            SetterDeathRecord.FirstOtherRaceCodeHelper = ValueSets.RaceCode.Lebanese;
            SetterDeathRecord.SecondOtherRaceCodeHelper = ValueSets.RaceCode.Palestinian;
            SetterDeathRecord.HispanicCodeForLiteralHelper = ValueSets.HispanicOrigin.Canal_Zone;
            SetterDeathRecord.HispanicCodeHelper = ValueSets.HispanicOrigin.Non_Hispanic; // test code 100...
            SetterDeathRecord.HispanicCodeHelper = ValueSets.HispanicOrigin.Cuban;
            SetterDeathRecord.RaceRecode40Helper = ValueSets.RaceRecode40.Aian_And_Asian;
            Assert.Equal(ValueSets.RaceCode.African, SetterDeathRecord.FirstEditedRaceCodeHelper);
            Assert.Equal(ValueSets.RaceCode.Asian, SetterDeathRecord.SecondEditedRaceCodeHelper);
            Assert.Equal(ValueSets.RaceCode.Blackfeet, SetterDeathRecord.ThirdEditedRaceCodeHelper);
            Assert.Equal(ValueSets.RaceCode.Jamestown_Sklallam, SetterDeathRecord.FourthEditedRaceCodeHelper);
            Assert.Equal(ValueSets.RaceCode.Kaw, SetterDeathRecord.FifthEditedRaceCodeHelper);
            Assert.Equal(ValueSets.RaceCode.Madagascar, SetterDeathRecord.SixthEditedRaceCodeHelper);
            Assert.Equal(ValueSets.RaceCode.Okinawan, SetterDeathRecord.SeventhEditedRaceCodeHelper);
            Assert.Equal(ValueSets.RaceCode.Zaire, SetterDeathRecord.EighthEditedRaceCodeHelper);
            Assert.Equal(ValueSets.RaceCode.Navajo, SetterDeathRecord.FirstAmericanIndianRaceCodeHelper);
            Assert.Equal(ValueSets.RaceCode.Stockbridgemunsee_Community_Of_Mohican_Indians_Of_Wisconsin, SetterDeathRecord.SecondAmericanIndianRaceCodeHelper);
            Assert.Equal(ValueSets.RaceCode.Malaysian, SetterDeathRecord.FirstOtherAsianRaceCodeHelper);
            Assert.Equal(ValueSets.RaceCode.Burmese, SetterDeathRecord.SecondOtherAsianRaceCodeHelper);
            Assert.Equal(ValueSets.RaceCode.Taiwanese, SetterDeathRecord.FirstOtherPacificIslanderRaceCodeHelper);
            Assert.Equal(ValueSets.RaceCode.New_Hebrides, SetterDeathRecord.SecondOtherPacificIslanderRaceCodeHelper);
            Assert.Equal(ValueSets.RaceCode.Lebanese, SetterDeathRecord.FirstOtherRaceCodeHelper);
            Assert.Equal(ValueSets.RaceCode.Palestinian, SetterDeathRecord.SecondOtherRaceCodeHelper);
            Assert.Equal(ValueSets.HispanicOrigin.Canal_Zone, SetterDeathRecord.HispanicCodeForLiteralHelper);
            Assert.Equal(ValueSets.HispanicOrigin.Cuban, SetterDeathRecord.HispanicCodeHelper);
            Assert.Equal(ValueSets.RaceRecode40.Aian_And_Asian, SetterDeathRecord.RaceRecode40Helper);
        }
        [Fact]
        public void Get_EditedRaceCodes()
        {
            Assert.Equal(ValueSets.RaceCode.White, DeathCertificateDocument2_JSON.FirstEditedRaceCodeHelper);
            Assert.Equal(ValueSets.RaceCode.Israeli, DeathCertificateDocument2_JSON.SecondEditedRaceCodeHelper);
            Assert.Equal(ValueSets.HispanicOrigin.Chilean, DeathCertificateDocument2_JSON.HispanicCodeHelper);
            Assert.Equal(ValueSets.RaceRecode40.Aian_And_Asian, DeathCertificateDocument2_JSON.RaceRecode40Helper);
        }



        [Fact]
        public void Set_BirthRecordId()
        {
            SetterDeathRecord.BirthRecordId = "242123";
            Assert.Equal("242123", SetterDeathRecord.BirthRecordId);
        }

        [Fact]
        public void Get_BirthRecordId()
        {
            Assert.Equal("242123", DeathRecord1_JSON.BirthRecordId);
            Assert.Equal("242123", DeathRecord1_XML.BirthRecordId);
        }

        [Fact]
        public void Set_BirthRecord_Absent()
        {
            SetterDeathRecord.BirthRecordId = "";
            Assert.Null(SetterDeathRecord.BirthRecordId);
        }

        [Fact]
        public void Get_BirthRecord_Absent()
        {
            DeathRecord dr = new DeathRecord(File.ReadAllText(FixturePath("fixtures/json/DeathRecordBirthRecordDataAbsent.json")));
            Assert.Null(dr.BirthRecordId);
        }

        [Fact]
        public void Get_BirthRecord_Roundtrip()
        {
            DeathRecord dr = DeathRecord1_JSON;
            Assert.Equal("242123", dr.BirthRecordId);
            IJEMortality ije1 = new IJEMortality(dr);
            Assert.Equal("242123", ije1.BCNO);
            DeathRecord dr2 = ije1.ToDeathRecord();
            Assert.Equal("242123", dr2.BirthRecordId);
        }
        [Fact]
        public void BirthRecord_Relic()
        {
            DeathRecord dr = new DeathRecord(File.ReadAllText(FixturePath("fixtures/json/RecordVRDRv1.2.json")));
            Exception ex = Assert.Throws<System.ArgumentOutOfRangeException>(() => new IJEMortality(dr));
        }
        [Fact]
        public void BirthRecord_Absent_Roundtrip()
        {
            DeathRecord dr = new DeathRecord(File.ReadAllText(FixturePath("fixtures/json/DeathRecordBirthRecordDataAbsent.json")));
            IJEMortality ije1 = new IJEMortality(dr);
            Assert.Equal("", ije1.BCNO);
            DeathRecord dr2 = ije1.ToDeathRecord();
            Assert.Null(dr.BirthRecordId);
        }
        [Theory]
        [InlineData("MA")]
        [InlineData("YC")]
        public void Set_BirthRecordState(string state)
        {
            SetterDeathRecord.BirthRecordState = state;
            Assert.Equal(state, SetterDeathRecord.BirthRecordState);
        }

        [Fact]
        public void Get_BirthRecordState()
        {
            Assert.Equal("MA", DeathRecord1_JSON.BirthRecordState);
            Assert.Equal("MA", DeathRecord1_XML.BirthRecordState);
            Assert.Equal("YC", DeathCertificateDocument1_JSON.BirthRecordState);

        }

        [Fact]
        public void Set_BirthRecordYear()
        {
            SetterDeathRecord.BirthRecordYear = "1940";
            Assert.Equal("1940", SetterDeathRecord.BirthRecordYear);
        }

        [Fact]
        public void Get_BirthRecordYear()
        {
            Assert.Equal("1940", DeathRecord1_JSON.BirthRecordYear);
            Assert.Equal("1940", DeathRecord1_XML.BirthRecordYear);
        }

        [Fact]
        public void Set_UsualOccupation()
        {
            SetterDeathRecord.UsualOccupation = "secretary";
            Assert.Equal("secretary", SetterDeathRecord.UsualOccupation);
        }

        [Fact]
        public void Get_UsualOccupation()
        {
            Assert.Equal("Executive secretary", DeathRecord1_JSON.UsualOccupation);
            Assert.Equal("Executive secretary", DeathRecord1_XML.UsualOccupation);

        }

        [Fact]
        public void Set_UsualIndustry()
        {
            SetterDeathRecord.UsualIndustry = "State agency";
            Assert.Equal("State agency", SetterDeathRecord.UsualIndustry);
        }

        [Fact]
        public void Get_UsualIndustry()
        {
            Assert.Equal("State department of agriculture", DeathRecord1_JSON.UsualIndustry);
            Assert.Equal("State department of agriculture", DeathRecord1_XML.UsualIndustry);
        }

        [Fact]
        public void Set_MilitaryService()
        {
            SetterDeathRecord.MilitaryServiceHelper = VRDR.ValueSets.YesNoUnknown.Yes;
            Assert.Equal(VRDR.ValueSets.YesNoUnknown.Yes, SetterDeathRecord.MilitaryServiceHelper);
        }

        [Fact]
        public void Get_MilitaryService()
        {
            Assert.Equal(VRDR.ValueSets.YesNoUnknown.Yes, DeathRecord1_JSON.MilitaryServiceHelper);
            Assert.Equal(VRDR.ValueSets.YesNoUnknown.Yes, DeathCertificateDocument2_JSON.MilitaryServiceHelper);
            Assert.Equal(VRDR.ValueSets.YesNoUnknown.Yes, DeathRecord1_XML.MilitaryServiceHelper);
        }

        // [Fact]
        // public void Set_MorticianGivenNames()
        // {
        //     string[] fdnames = { "FD", "Middle" };
        //     SetterDeathRecord.MorticianGivenNames = fdnames;
        //     Assert.Equal("FD", SetterDeathRecord.MorticianGivenNames[0]);
        //     Assert.Equal("Middle", SetterDeathRecord.MorticianGivenNames[1]);
        // }

        // [Fact]
        // public void Get_MorticianGivenNames()
        // {
        //     Assert.Equal("FD", DeathRecord1_JSON.MorticianGivenNames[0]);
        //     Assert.Equal("Middle", DeathRecord1_JSON.MorticianGivenNames[1]);
        //     Assert.Equal("FD", DeathRecord1_XML.MorticianGivenNames[0]);
        //     Assert.Equal("Middle", DeathRecord1_XML.MorticianGivenNames[1]);
        // }

        // [Fact]
        // public void Set_MorticianFamilyName()
        // {
        //     SetterDeathRecord.MorticianFamilyName = "Last";
        //     Assert.Equal("Last", SetterDeathRecord.MorticianFamilyName);
        // }

        // [Fact]
        // public void Get_MorticianFamilyName()
        // {
        //     Assert.Equal("Last", DeathRecord1_JSON.MorticianFamilyName);
        //     Assert.Equal("Last", DeathRecord1_XML.MorticianFamilyName);
        // }

        // [Fact]
        // public void Set_MorticianSuffix()
        // {
        //     SetterDeathRecord.MorticianSuffix = "Sr.";
        //     Assert.Equal("Sr.", SetterDeathRecord.MorticianSuffix);
        // }

        // [Fact]
        // public void Get_MorticianSuffix()
        // {
        //     Assert.Equal("Jr.", DeathRecord1_JSON.MorticianSuffix);
        //     Assert.Equal("Jr.", DeathRecord1_XML.MorticianSuffix);
        // }

        // [Fact]
        // public void Set_MorticianIdentifier()
        // {
        //     var id = new Dictionary<string, string>();
        //     id["system"] = "foo";
        //     id["value"] = "9876543210";
        //     SetterDeathRecord.MorticianIdentifier = id;
        //     Assert.Equal("foo", SetterDeathRecord.MorticianIdentifier["system"]);
        //     Assert.Equal("9876543210", SetterDeathRecord.MorticianIdentifier["value"]);
        // }

        // [Fact]
        // public void Get_MorticianIdentifier()
        // {
        //     Assert.Equal("9876543210", DeathRecord1_JSON.MorticianIdentifier["value"]);
        //     Assert.Equal("9876543210", DeathRecord1_XML.MorticianIdentifier["value"]);
        // }

        // [Fact]
        // public void Set_PronouncerGivenNames()
        // {
        //     string[] fdnames = { "FD", "Middle" };
        //     SetterDeathRecord.PronouncerGivenNames = fdnames;
        //     Assert.Equal("FD", SetterDeathRecord.PronouncerGivenNames[0]);
        //     Assert.Equal("Middle", SetterDeathRecord.PronouncerGivenNames[1]);
        // }

        // [Fact]
        // public void Get_PronouncerGivenNames()
        // {
        //     Assert.Equal("FD", DeathRecord1_JSON.PronouncerGivenNames[0]);
        //     Assert.Equal("Middle", DeathRecord1_JSON.PronouncerGivenNames[1]);
        //     Assert.Equal("FD", DeathRecord1_XML.PronouncerGivenNames[0]);
        //     Assert.Equal("Middle", DeathRecord1_XML.PronouncerGivenNames[1]);
        // }

        // [Fact]
        // public void Set_PronouncerFamilyName()
        // {
        //     SetterDeathRecord.PronouncerFamilyName = "Last";
        //     Assert.Equal("Last", SetterDeathRecord.PronouncerFamilyName);
        // }

        // [Fact]
        // public void Get_PronouncerFamilyName()
        // {
        //     Assert.Equal("Last", DeathRecord1_JSON.PronouncerFamilyName);
        //     Assert.Equal("Last", DeathRecord1_XML.PronouncerFamilyName);
        // }

        // [Fact]
        // public void Set_PronouncerSuffix()
        // {
        //     SetterDeathRecord.PronouncerSuffix = "Sr.";
        //     Assert.Equal("Sr.", SetterDeathRecord.PronouncerSuffix);
        // }

        // [Fact]
        // public void Get_PronouncerSuffix()
        // {
        //     Assert.Equal("Jr.", DeathRecord1_JSON.PronouncerSuffix);
        //     Assert.Equal("Jr.", DeathRecord1_XML.PronouncerSuffix);
        // }

        // [Fact]
        // public void Set_PronouncerIdentifier()
        // {
        //     var id = new Dictionary<string, string>();
        //     id["system"] = "foo";
        //     id["value"] = "0000000000";
        //     SetterDeathRecord.PronouncerIdentifier = id;
        //     Assert.Equal("foo", SetterDeathRecord.PronouncerIdentifier["system"]);
        //     Assert.Equal("0000000000", SetterDeathRecord.PronouncerIdentifier["value"]);
        // }

        // [Fact]
        // public void Get_PronouncerIdentifier()
        // {
        //     Assert.Equal("0000000000", DeathRecord1_JSON.PronouncerIdentifier["value"]);
        //     Assert.Equal("0000000000", DeathRecord1_XML.PronouncerIdentifier["value"]);
        // }

        [Fact]
        public void Set_FuneralHomeAddress()
        {
            Dictionary<string, string> fdaddress = new Dictionary<string, string>();
            fdaddress.Add("addressLine1", "1011010 Example Street");
            fdaddress.Add("addressLine2", "Line 2");
            fdaddress.Add("addressCity", "Bedford");
            fdaddress.Add("addressCounty", "Middlesex");
            fdaddress.Add("addressState", "MA");
            fdaddress.Add("addressZip", "01730");
            fdaddress.Add("addressCountry", "US");
            fdaddress.Add("addressPredir", "W");
            fdaddress.Add("addressPostdir", "E");
            fdaddress.Add("addressStname", "Example");
            fdaddress.Add("addressStnum", "11");
            fdaddress.Add("addressStdesig", "Street");
            fdaddress.Add("addressUnitnum", "3");
            SetterDeathRecord.FuneralHomeAddress = fdaddress;
            Assert.Equal("1011010 Example Street", SetterDeathRecord.FuneralHomeAddress["addressLine1"]);
            Assert.Equal("Line 2", SetterDeathRecord.FuneralHomeAddress["addressLine2"]);
            Assert.Equal("Bedford", SetterDeathRecord.FuneralHomeAddress["addressCity"]);
            Assert.Equal("Middlesex", SetterDeathRecord.FuneralHomeAddress["addressCounty"]);
            Assert.Equal("MA", SetterDeathRecord.FuneralHomeAddress["addressState"]);
            Assert.Equal("01730", SetterDeathRecord.FuneralHomeAddress["addressZip"]);
            Assert.Equal("US", SetterDeathRecord.FuneralHomeAddress["addressCountry"]);
            Assert.Equal("W", SetterDeathRecord.FuneralHomeAddress["addressPredir"]);
            Assert.Equal("E", SetterDeathRecord.FuneralHomeAddress["addressPostdir"]);
            Assert.Equal("Example", SetterDeathRecord.FuneralHomeAddress["addressStname"]);
            Assert.Equal("11", SetterDeathRecord.FuneralHomeAddress["addressStnum"]);
            Assert.Equal("Street", SetterDeathRecord.FuneralHomeAddress["addressStdesig"]);
            Assert.Equal("3", SetterDeathRecord.FuneralHomeAddress["addressUnitnum"]);
        }

        [Fact]
        public void Get_FuneralHomeAddress()
        {
            Assert.Equal("1011010 Example Street", DeathRecord1_XML.FuneralHomeAddress["addressLine1"]);
            Assert.Equal("1011010 Example Street", DeathRecord1_JSON.FuneralHomeAddress["addressLine1"]);
            Assert.Equal("Line 2", DeathRecord1_JSON.FuneralHomeAddress["addressLine2"]);
            Assert.Equal("Bedford", DeathRecord1_JSON.FuneralHomeAddress["addressCity"]);
            Assert.Equal("Middlesex", DeathRecord1_JSON.FuneralHomeAddress["addressCounty"]);
            Assert.Equal("MA", DeathRecord1_JSON.FuneralHomeAddress["addressState"]);
            Assert.Equal("01730", DeathRecord1_JSON.FuneralHomeAddress["addressZip"]);
            Assert.Equal("US", DeathRecord1_JSON.FuneralHomeAddress["addressCountry"]);

            Assert.Equal("Line 2", DeathRecord1_XML.FuneralHomeAddress["addressLine2"]);
            Assert.Equal("Bedford", DeathRecord1_XML.FuneralHomeAddress["addressCity"]);
            Assert.Equal("Middlesex", DeathRecord1_XML.FuneralHomeAddress["addressCounty"]);
            Assert.Equal("MA", DeathRecord1_XML.FuneralHomeAddress["addressState"]);
            Assert.Equal("01730", DeathRecord1_XML.FuneralHomeAddress["addressZip"]);
            Assert.Equal("US", DeathRecord1_XML.FuneralHomeAddress["addressCountry"]);
        }

        [Fact]
        public void Set_FuneralHomeName()
        {
            SetterDeathRecord.FuneralHomeName = "Smith Funeral Home";
            Assert.Equal("Smith Funeral Home", SetterDeathRecord.FuneralHomeName);
        }

        [Fact]
        public void Get_FuneralHomeName()
        {
            Assert.Equal("Smith Funeral Home", DeathRecord1_JSON.FuneralHomeName);
            Assert.Equal("Smith Funeral Home", DeathRecord1_XML.FuneralHomeName);
        }

        [Fact]
        public void Set_DispositionLocationAddress()
        {
            Dictionary<string, string> dladdress = new Dictionary<string, string>();
            dladdress.Add("addressLine1", "999 Example Street");
            dladdress.Add("addressLine2", "Line 2");
            dladdress.Add("addressCity", "Bedford");
            dladdress.Add("addressCounty", "Middlesex");
            dladdress.Add("addressState", "MA");
            dladdress.Add("addressZip", "01730");
            dladdress.Add("addressCountry", "US");
            SetterDeathRecord.DispositionLocationAddress = dladdress;
            Assert.Equal("999 Example Street", SetterDeathRecord.DispositionLocationAddress["addressLine1"]);
            Assert.Equal("Line 2", SetterDeathRecord.DispositionLocationAddress["addressLine2"]);
            Assert.Equal("Bedford", SetterDeathRecord.DispositionLocationAddress["addressCity"]);
            Assert.Equal("Middlesex", SetterDeathRecord.DispositionLocationAddress["addressCounty"]);
            Assert.Equal("MA", SetterDeathRecord.DispositionLocationAddress["addressState"]);
            Assert.Equal("01730", SetterDeathRecord.DispositionLocationAddress["addressZip"]);
            Assert.Equal("US", SetterDeathRecord.DispositionLocationAddress["addressCountry"]);
        }

        [Fact]
        public void Get_DispositionLocationAddress()
        {
            Assert.Equal("603 Example Street", DeathRecord1_JSON.DispositionLocationAddress["addressLine1"]);
            Assert.Equal("Line 2", DeathRecord1_JSON.DispositionLocationAddress["addressLine2"]);
            Assert.Equal("Bedford", DeathRecord1_JSON.DispositionLocationAddress["addressCity"]);
            Assert.Equal("Middlesex", DeathRecord1_JSON.DispositionLocationAddress["addressCounty"]);
            Assert.Equal("MA", DeathRecord1_JSON.DispositionLocationAddress["addressState"]);
            Assert.Equal("01730", DeathRecord1_JSON.DispositionLocationAddress["addressZip"]);
            Assert.Equal("US", DeathRecord1_JSON.DispositionLocationAddress["addressCountry"]);
            Assert.Equal("603 Example Street", DeathRecord1_XML.DispositionLocationAddress["addressLine1"]);
            Assert.Equal("Line 2", DeathRecord1_XML.DispositionLocationAddress["addressLine2"]);
            Assert.Equal("Bedford", DeathRecord1_XML.DispositionLocationAddress["addressCity"]);
            Assert.Equal("Middlesex", DeathRecord1_XML.DispositionLocationAddress["addressCounty"]);
            Assert.Equal("MA", DeathRecord1_XML.DispositionLocationAddress["addressState"]);
            Assert.Equal("01730", DeathRecord1_XML.DispositionLocationAddress["addressZip"]);
            Assert.Equal("US", DeathRecord1_XML.DispositionLocationAddress["addressCountry"]);
        }

        [Fact]
        public void Set_DispositionLocationName()
        {
            SetterDeathRecord.DispositionLocationName = "Bedford Cemetery";
            Assert.Equal("Bedford Cemetery", SetterDeathRecord.DispositionLocationName);
            SetterDeathRecord.DispositionLocationName = "";
            Assert.Null(SetterDeathRecord.DispositionLocationName);
            SetterDeathRecord.DispositionLocationName = null;
            Assert.Null(SetterDeathRecord.DispositionLocationName);
        }

        [Fact]
        public void Get_DispositionLocationName()
        {
            Assert.Equal("Bedford Cemetery", DeathRecord1_JSON.DispositionLocationName);
            Assert.Equal("Rosewood Cemetary", DeathCertificateDocument2_JSON.DispositionLocationName);
            Assert.Equal("Bedford Cemetery", DeathRecord1_XML.DispositionLocationName);
        }

        [Fact]
        public void Set_DecedentDispositionMethod()
        {
            Dictionary<string, string> ddm = new Dictionary<string, string>();
            SetterDeathRecord.DecedentDispositionMethodHelper = VRDR.ValueSets.MethodOfDisposition.Burial;
            Assert.Equal(VRDR.ValueSets.MethodOfDisposition.Burial, SetterDeathRecord.DecedentDispositionMethodHelper);
            Assert.Equal(VRDR.CodeSystems.SCT, SetterDeathRecord.DecedentDispositionMethod["system"]);
            Assert.Equal("Burial", SetterDeathRecord.DecedentDispositionMethod["display"]);
        }

        [Fact]
        public void Get_DecedentDispositionMethod()
        {
            Assert.Equal("449971000124106", DeathRecord1_JSON.DecedentDispositionMethod["code"]);
            Assert.Equal(CodeSystems.SCT, DeathRecord1_JSON.DecedentDispositionMethod["system"]);
            Assert.Equal("Burial", DeathRecord1_JSON.DecedentDispositionMethod["display"]);
            Assert.Equal("449971000124106", DeathRecord1_XML.DecedentDispositionMethod["code"]);
            Assert.Equal(CodeSystems.SCT, DeathRecord1_XML.DecedentDispositionMethod["system"]);
            Assert.Equal("Burial", DeathRecord1_XML.DecedentDispositionMethod["display"]);
        }

        [Fact]
        public void Set_AutopsyPerformedIndicator()
        {
            Dictionary<string, string> api = new Dictionary<string, string>();
            api.Add("code", "Y");
            api.Add("system", VRDR.CodeSystems.YesNo);
            api.Add("display", "Yes");
            SetterDeathRecord.AutopsyPerformedIndicator = api;
            Assert.Equal("Y", SetterDeathRecord.AutopsyPerformedIndicator["code"]);
            Assert.Equal(VRDR.CodeSystems.YesNo, SetterDeathRecord.AutopsyPerformedIndicator["system"]);
            Assert.Equal("Yes", SetterDeathRecord.AutopsyPerformedIndicator["display"]);
            Assert.Equal("Y", SetterDeathRecord.AutopsyPerformedIndicatorHelper);
            SetterDeathRecord.AutopsyPerformedIndicatorHelper = "N";
            Assert.Equal("N", SetterDeathRecord.AutopsyPerformedIndicator["code"]);
            Assert.Equal(VRDR.CodeSystems.YesNo, SetterDeathRecord.AutopsyPerformedIndicator["system"]);
            Assert.Equal("No", SetterDeathRecord.AutopsyPerformedIndicator["display"]);
            Assert.Equal("N", SetterDeathRecord.AutopsyPerformedIndicatorHelper);
            SetterDeathRecord.AutopsyPerformedIndicatorHelper = "UNK";
            Assert.Equal("UNK", SetterDeathRecord.AutopsyPerformedIndicator["code"]);
            Assert.Equal(VRDR.CodeSystems.NullFlavor_HL7_V3, SetterDeathRecord.AutopsyPerformedIndicator["system"]);
            Assert.Equal("unknown", SetterDeathRecord.AutopsyPerformedIndicator["display"]);
            Assert.Equal("UNK", SetterDeathRecord.AutopsyPerformedIndicatorHelper);
        }

        [Fact]
        public void Get_AutopsyPerformedIndicator()
        {
            Assert.Equal(ValueSets.YesNoUnknown.Yes, DeathCertificateDocument2_JSON.AutopsyPerformedIndicatorHelper);
            Assert.Equal("Y", DeathRecord1_JSON.AutopsyPerformedIndicator["code"]);
            Assert.Equal(VRDR.CodeSystems.YesNo, DeathRecord1_JSON.AutopsyPerformedIndicator["system"]);
            Assert.Equal("Yes", DeathRecord1_JSON.AutopsyPerformedIndicator["display"]);
            Assert.Equal("Y", DeathRecord1_JSON.AutopsyPerformedIndicatorHelper);
            Assert.Equal("Y", DeathRecord1_XML.AutopsyPerformedIndicator["code"]);
            Assert.Equal(VRDR.CodeSystems.YesNo, DeathRecord1_XML.AutopsyPerformedIndicator["system"]);
            Assert.Equal("Yes", DeathRecord1_XML.AutopsyPerformedIndicator["display"]);
            Assert.Equal("Y", DeathRecord1_XML.AutopsyPerformedIndicatorHelper);
        }

        [Fact]
        public void Set_AutopsyResultsAvailable()
        {
            SetterDeathRecord.AutopsyResultsAvailableHelper = VRDR.ValueSets.YesNoUnknown.Yes;
            Assert.Equal("Y", SetterDeathRecord.AutopsyResultsAvailableHelper);
            SetterDeathRecord.AutopsyResultsAvailableHelper = "N";
            Assert.Equal("N", SetterDeathRecord.AutopsyResultsAvailableHelper);
            SetterDeathRecord.AutopsyResultsAvailableHelper = "NA";
            Assert.Equal("NA", SetterDeathRecord.AutopsyResultsAvailable["code"]);
            Assert.Equal(VRDR.CodeSystems.NullFlavor_HL7_V3, SetterDeathRecord.AutopsyResultsAvailable["system"]);
            Assert.Equal("not applicable", SetterDeathRecord.AutopsyResultsAvailable["display"]);
            Assert.Equal("NA", SetterDeathRecord.AutopsyResultsAvailableHelper);
        }

        [Fact]
        public void Get_AutopsyResultsAvailable()
        {
            Assert.Equal(ValueSets.YesNoUnknown.Yes, DeathCertificateDocument2_JSON.AutopsyResultsAvailableHelper);
            Assert.Equal("Y", DeathRecord1_JSON.AutopsyResultsAvailable["code"]);
            Assert.Equal(VRDR.CodeSystems.YesNo, DeathRecord1_JSON.AutopsyResultsAvailable["system"]);
            Assert.Equal("Yes", DeathRecord1_JSON.AutopsyResultsAvailable["display"]);
            Assert.Equal("Y", DeathRecord1_JSON.AutopsyResultsAvailableHelper);
            Assert.Equal("Y", DeathRecord1_XML.AutopsyResultsAvailable["code"]);
            Assert.Equal(VRDR.CodeSystems.YesNo, DeathRecord1_XML.AutopsyResultsAvailable["system"]);
            Assert.Equal("Yes", DeathRecord1_XML.AutopsyResultsAvailable["display"]);
            Assert.Equal("Y", DeathRecord1_XML.AutopsyResultsAvailableHelper);
        }

        [Fact]
        public void Set_AgeAtDeath()
        {
            Dictionary<string, string> aad = new Dictionary<string, string>();
            aad.Add("code", "mo");
            aad.Add("value", "11");
            SetterDeathRecord.AgeAtDeath = aad;
            Assert.Equal("mo", SetterDeathRecord.AgeAtDeath["code"]);
            Assert.Equal("11", SetterDeathRecord.AgeAtDeath["value"]);
            // optional fields
            aad.Add("system", "http://unitsofmeasure.org");
            aad.Add("unit", "years");
            SetterDeathRecord.AgeAtDeath = aad;
            Assert.Equal("http://unitsofmeasure.org", (SetterDeathRecord.AgeAtDeath["system"]));
            Assert.Equal("years", (SetterDeathRecord.AgeAtDeath["unit"]));
        }

        [Fact]
        public void Get_AgeAtDeath()
        {
            Assert.Equal("a", DeathCertificateDocument2_JSON.AgeAtDeath["code"]);
            Assert.Equal("42", DeathCertificateDocument2_JSON.AgeAtDeath["value"]);
            DeathRecord dr1 = DeathCertificateDocument2_JSON;
            DeathRecord dr2 = new DeathRecord(dr1.ToJSON());
            Assert.Equal("a", (dr2.AgeAtDeath["code"]));
            Assert.Equal("42", (dr2.AgeAtDeath["value"]));
            Dictionary<string, string> aad = new Dictionary<string, string>();
            aad.Add("code", "mo");
            aad.Add("value", "11");
            dr2.AgeAtDeath = aad;
            Assert.Equal("mo", (dr2.AgeAtDeath["code"]));
            Assert.Equal("11", (dr2.AgeAtDeath["value"]));
            Assert.Equal("a", DeathRecord1_XML.AgeAtDeath["code"]);
            Assert.Equal("79", DeathRecord1_XML.AgeAtDeath["value"]);
            // optional fields
            Assert.Equal("http://unitsofmeasure.org", DeathCertificateDocument2_JSON.AgeAtDeath["system"]);
            Assert.Equal("years", DeathCertificateDocument2_JSON.AgeAtDeath["unit"]);
            Assert.Equal("http://unitsofmeasure.org", dr2.AgeAtDeath["system"]);
            Assert.Equal("years", dr2.AgeAtDeath["unit"]);
        }

        [Fact]
        public void Set_AgeAtDeathYears()
        {
            SetterDeathRecord.AgeAtDeathYears = 11;
            Assert.Equal("a", SetterDeathRecord.AgeAtDeath["code"]);
            Assert.Equal("11", SetterDeathRecord.AgeAtDeath["value"]);
        }

        [Fact]
        public void Get_AgeAtDeathYears()
        {
            Assert.Equal(42, DeathCertificateDocument2_JSON.AgeAtDeathYears);
            Assert.Equal(79, DeathRecord1_XML.AgeAtDeathYears);
            DeathRecord dr2 = new DeathRecord(DeathCertificateDocument2_JSON.ToJSON());
            dr2.AgeAtDeath = new Dictionary<string, string>() {
                {"code", "a"},
                {"value", "11"}
            };
            Assert.Equal(11, dr2.AgeAtDeathYears);
        }

        [Fact]
        public void Set_AgeAtDeathMonths()
        {
            SetterDeathRecord.AgeAtDeathMonths = 11;
            Assert.Equal("mo", SetterDeathRecord.AgeAtDeath["code"]);
            Assert.Equal("11", SetterDeathRecord.AgeAtDeath["value"]);
        }

        [Fact]
        public void Get_AgeAtDeathMonths()
        {
            DeathRecord dr2 = new DeathRecord(DeathCertificateDocument2_JSON.ToJSON());
            dr2.AgeAtDeath = new Dictionary<string, string>() {
                {"code", "mo"},
                {"value", "11"}
            };
            Assert.Equal(11, dr2.AgeAtDeathMonths);
        }

        [Fact]
        public void Set_AgeAtDeathDays()
        {
            SetterDeathRecord.AgeAtDeathDays = 11;
            Assert.Equal("d", SetterDeathRecord.AgeAtDeath["code"]);
            Assert.Equal("11", SetterDeathRecord.AgeAtDeath["value"]);
        }

        [Fact]
        public void Get_AgeAtDeathDays()
        {
            DeathRecord dr2 = new DeathRecord(DeathCertificateDocument2_JSON.ToJSON());
            dr2.AgeAtDeath = new Dictionary<string, string>() {
                {"code", "d"},
                {"value", "11"}
            };
            Assert.Equal(11, dr2.AgeAtDeathDays);
        }

        [Fact]
        public void Set_AgeAtDeathHours()
        {
            SetterDeathRecord.AgeAtDeathHours = 11;
            Assert.Equal("h", SetterDeathRecord.AgeAtDeath["code"]);
            Assert.Equal("11", SetterDeathRecord.AgeAtDeath["value"]);
        }

        [Fact]
        public void Get_AgeAtDeathHours()
        {
            DeathRecord dr2 = new DeathRecord(DeathCertificateDocument2_JSON.ToJSON());
            dr2.AgeAtDeath = new Dictionary<string, string>() {
                {"code", "h"},
                {"value", "11"}
            };
            Assert.Equal(11, dr2.AgeAtDeathHours);
        }

        [Fact]
        public void Set_AgeAtDeathMinutes()
        {
            SetterDeathRecord.AgeAtDeathMinutes = 11;
            Assert.Equal("min", SetterDeathRecord.AgeAtDeath["code"]);
            Assert.Equal("11", SetterDeathRecord.AgeAtDeath["value"]);
        }

        [Fact]
        public void Get_AgeAtDeathMinutes()
        {
            DeathRecord dr2 = new DeathRecord(DeathCertificateDocument2_JSON.ToJSON());
            dr2.AgeAtDeath = new Dictionary<string, string>() {
                {"code", "min"},
                {"value", "11"}
            };
            Assert.Equal(11, dr2.AgeAtDeathMinutes);
        }

        [Fact]
        public void Set_AgeAtDeath_EditBypassFlag()
        {
            SetterDeathRecord.AgeAtDeathEditFlagHelper = ValueSets.EditBypass01.Edit_Passed;
            Assert.Equal(ValueSets.EditBypass01.Edit_Passed, SetterDeathRecord.AgeAtDeathEditFlagHelper);

            SetterDeathRecord.AgeAtDeathEditFlagHelper = ValueSets.EditBypass01.Edit_Failed_Data_Queried_And_Verified;
            Assert.Equal(ValueSets.EditBypass01.Edit_Failed_Data_Queried_And_Verified, SetterDeathRecord.AgeAtDeathEditFlagHelper);
        }

        [Fact]
        public void AgeAtDeath_RoundTrip()
        {
            DeathRecord dr = new DeathRecord(File.ReadAllText(FixturePath("fixtures/json/MissingAge.json")));
            IJEMortality ije = new IJEMortality(dr);
            Assert.Equal("999", ije.AGE);
            Assert.Equal("9", ije.AGETYPE);
            DeathRecord dr2 = ije.ToDeathRecord();
            Assert.Equal("", dr2.AgeAtDeath["code"]);
            Assert.Equal("", dr2.AgeAtDeath["value"]);
        }

        [Fact]
        public void AgeAtDeath_EditFlag()
        {
            Dictionary<string, string> flag = new Dictionary<string, string>();
            flag.Add("system", CodeSystems.BypassEditFlag);
            flag.Add("code", "0");
            flag.Add("display", "Edit Passed");
            SetterDeathRecord.AgeAtDeathEditFlag = flag;
            Assert.Equal(CodeSystems.BypassEditFlag, SetterDeathRecord.AgeAtDeathEditFlag["system"]);
            Assert.Equal("0", SetterDeathRecord.AgeAtDeathEditFlag["code"]);
            Assert.Equal("Edit Passed", SetterDeathRecord.AgeAtDeathEditFlag["display"]);

            flag = new Dictionary<string, string>();
            flag.Add("system", CodeSystems.BypassEditFlag);
            flag.Add("code", "1");
            flag.Add("display", "Edit Failed, Data Queried, and Verified");
            SetterDeathRecord.AgeAtDeathEditFlag = flag;
            Assert.Equal(CodeSystems.BypassEditFlag, SetterDeathRecord.AgeAtDeathEditFlag["system"]);
            Assert.Equal("1", SetterDeathRecord.AgeAtDeathEditFlag["code"]);
            Assert.Equal("Edit Failed, Data Queried, and Verified", SetterDeathRecord.AgeAtDeathEditFlag["display"]);
        }

        [Fact]
        public void AgeAtDeath_EditFlag_RoundTrip()
        {
            DeathRecord dr = new DeathRecord(File.ReadAllText(FixturePath("fixtures/json/MissingAge.json")));
            Dictionary<string, string> flag = new Dictionary<string, string>();
            flag.Add("system", CodeSystems.BypassEditFlag);
            flag.Add("code", "0");
            flag.Add("display", "Edit Passed");
            dr.AgeAtDeathEditFlag = flag;

            IJEMortality ije = new IJEMortality(dr);
            Assert.Equal("999", ije.AGE);
            Assert.Equal("9", ije.AGETYPE);
            Assert.Equal("0", ije.AGE_BYPASS);
            DeathRecord dr2 = ije.ToDeathRecord();
            Assert.Equal(CodeSystems.BypassEditFlag, dr2.AgeAtDeathEditFlag["system"]);
            Assert.Equal("0", dr2.AgeAtDeathEditFlag["code"]);
            Assert.Equal("Edit Passed", dr2.AgeAtDeathEditFlag["display"]);
        }

        [Fact]
        public void Set_PregnancyStatus()
        {
            Dictionary<string, string> ps = new Dictionary<string, string>();
            ps.Add("code", "1");
            ps.Add("system", VRDR.CodeSystems.PregnancyStatus);
            ps.Add("display", "Not pregnant within past year");
            SetterDeathRecord.PregnancyStatus = ps;
            Assert.Equal("1", SetterDeathRecord.PregnancyStatus["code"]);
            Assert.Equal(VRDR.CodeSystems.PregnancyStatus, SetterDeathRecord.PregnancyStatus["system"]);
            Assert.Equal("Not pregnant within past year", SetterDeathRecord.PregnancyStatus["display"]);
        }

        [Fact]
        public void Get_PregnancyStatus()
        {
            Assert.Equal("1", DeathRecord1_JSON.PregnancyStatus["code"]);
            Assert.Equal(VRDR.CodeSystems.PregnancyStatus, DeathRecord1_JSON.PregnancyStatus["system"]);
            Assert.Equal("Not pregnant within past year", DeathRecord1_JSON.PregnancyStatus["display"]);
            Assert.Equal(ValueSets.PregnancyStatus.Pregnant_At_Time_Of_Death, DeathCertificateDocument2_JSON.PregnancyStatusHelper);
            Assert.Equal("1", DeathRecord1_XML.PregnancyStatus["code"]);
            Assert.Equal(VRDR.CodeSystems.PregnancyStatus, DeathRecord1_XML.PregnancyStatus["system"]);
            Assert.Equal("Not pregnant within past year", DeathRecord1_XML.PregnancyStatus["display"]);
        }

        [Fact]
        public void Set_PregnancyStatusEditFlag()
        {
            Dictionary<string, string> elef = new Dictionary<string, string>();
            elef.Add("code", VRDR.ValueSets.EditBypass012.Edit_Failed_Data_Queried_And_Verified);
            elef.Add("system", VRDR.CodeSystems.BypassEditFlag);
            elef.Add("display", "Edit Failed Data Queried And Verified");
            SetterDeathRecord.PregnancyStatusEditFlag = elef;
            Assert.Equal(VRDR.ValueSets.EditBypass012.Edit_Failed_Data_Queried_And_Verified, SetterDeathRecord.PregnancyStatusEditFlag["code"]);
            Assert.Equal(VRDR.CodeSystems.BypassEditFlag, SetterDeathRecord.PregnancyStatusEditFlag["system"]);
            Assert.Equal("Edit Failed Data Queried And Verified", SetterDeathRecord.PregnancyStatusEditFlag["display"]);
            SetterDeathRecord.PregnancyStatusEditFlagHelper = VRDR.ValueSets.EditBypass012.Edit_Passed;
            Assert.Equal(VRDR.ValueSets.EditBypass012.Edit_Passed, SetterDeathRecord.PregnancyStatusEditFlagHelper);
            Assert.Equal(VRDR.CodeSystems.BypassEditFlag, SetterDeathRecord.PregnancyStatusEditFlag["system"]);
            Assert.Equal("Edit Passed", SetterDeathRecord.PregnancyStatusEditFlag["display"]);
        }

        [Fact]
        public void Get_PregnancyStatusEditFlag()
        {
            Assert.Equal(VRDR.ValueSets.EditBypass012.Edit_Failed_Data_Queried_But_Not_Verified, DeathCertificateDocument2_JSON.PregnancyStatusEditFlagHelper);
            Assert.Equal(VRDR.ValueSets.EditBypass012.Edit_Passed, DeathRecord1_JSON.PregnancyStatusEditFlagHelper);
            Assert.Equal(VRDR.CodeSystems.BypassEditFlag, DeathRecord1_JSON.PregnancyStatusEditFlag["system"]);
            Assert.Equal("Edit Passed", DeathRecord1_JSON.PregnancyStatusEditFlag["display"]);
            Assert.Equal(VRDR.ValueSets.EditBypass012.Edit_Passed, DeathRecord1_XML.PregnancyStatusEditFlagHelper);
            Assert.Equal(VRDR.CodeSystems.BypassEditFlag, DeathRecord1_XML.PregnancyStatusEditFlag["system"]);
            Assert.Equal("Edit Passed", DeathRecord1_XML.PregnancyStatusEditFlag["display"]);
        }

        [Fact]
        public void Set_InjuryPlace()
        {
            Dictionary<string, string> code = new Dictionary<string, string>();

            // no description
            SetterDeathRecord.InjuryPlaceDescription = "At home, in the kitchen";
            Assert.Equal("At home, in the kitchen", SetterDeathRecord.InjuryPlaceDescription);
        }

        [Fact]
        public void Get_InjuryPlace()
        {
            Assert.Equal("At home, in the kitchen", DeathRecord1_JSON.InjuryPlaceDescription);
            Assert.Equal("Home", DeathCertificateDocument2_JSON.InjuryPlaceDescription);
            Assert.Equal("At home, in the kitchen", DeathRecord1_XML.InjuryPlaceDescription);
        }

        [Fact]
        public void Set_TransportationRole()
        {
            SetterDeathRecord.TransportationRoleHelper = ValueSets.TransportationIncidentRole.Passenger;
            Assert.Equal(ValueSets.TransportationIncidentRole.Passenger, SetterDeathRecord.TransportationRole["code"]);
            Assert.Equal(CodeSystems.SCT, SetterDeathRecord.TransportationRole["system"]);
            Assert.Equal("Passenger", SetterDeathRecord.TransportationRole["display"]);
            SetterDeathRecord.DeathLocationJurisdiction = "MA";
            IJEMortality ije1 = new IJEMortality(SetterDeathRecord);
            Assert.Equal("PA", ije1.TRANSPRT);
            ije1.TRANSPRT = "PAP";
            Assert.Equal("PAP", ije1.TRANSPRT);
            DeathRecord d = ije1.ToDeathRecord();
            IJEMortality ije2 = new IJEMortality(d);
            Assert.Equal("PAP", ije2.TRANSPRT);
            SetterDeathRecord.TransportationRoleHelper = "Hover Board Rider";
            Assert.Equal("Hover Board Rider", SetterDeathRecord.TransportationRoleHelper);
            Assert.Equal("Hover Board Rider", SetterDeathRecord.TransportationRole["text"]);
            Assert.Equal("OTH", SetterDeathRecord.TransportationRole["code"]);
            Assert.Equal(CodeSystems.NullFlavor_HL7_V3, SetterDeathRecord.TransportationRole["system"]);
            Assert.Equal("Other", SetterDeathRecord.TransportationRole["display"]);
        }

        [Fact]
        public void Get_TransportationRole()
        {
            Assert.Equal(ValueSets.TransportationIncidentRole.Passenger, DeathRecord1_JSON.TransportationRole["code"]);
            Assert.Equal(ValueSets.TransportationIncidentRole.Pedestrian, DeathCertificateDocument2_JSON.TransportationRoleHelper);
            Assert.Equal(CodeSystems.SCT, DeathRecord1_JSON.TransportationRole["system"]);
            Assert.Equal("Passenger", DeathRecord1_JSON.TransportationRole["display"]);
            Assert.Equal(ValueSets.TransportationIncidentRole.Passenger, DeathRecord1_XML.TransportationRole["code"]);
            Assert.Equal(CodeSystems.SCT, DeathRecord1_XML.TransportationRole["system"]);
            Assert.Equal("Passenger", DeathRecord1_XML.TransportationRole["display"]);
        }

        [Fact]
        public void Set_ExaminerContacted()
        {
            Dictionary<string, string> ec = new Dictionary<string, string>();
            ec.Add("code", "Y");
            ec.Add("system", VRDR.CodeSystems.YesNo);
            ec.Add("display", "Yes");
            SetterDeathRecord.ExaminerContacted = ec;
            Assert.Equal("Y", SetterDeathRecord.ExaminerContacted["code"]);
            Assert.Equal(VRDR.CodeSystems.YesNo, SetterDeathRecord.ExaminerContacted["system"]);
            Assert.Equal("Yes", SetterDeathRecord.ExaminerContacted["display"]);
            Assert.Equal("Y", SetterDeathRecord.ExaminerContactedHelper);
            SetterDeathRecord.ExaminerContactedHelper = "N";
            Assert.Equal("N", SetterDeathRecord.ExaminerContacted["code"]);
            Assert.Equal(VRDR.CodeSystems.YesNo, SetterDeathRecord.ExaminerContacted["system"]);
            Assert.Equal("No", SetterDeathRecord.ExaminerContacted["display"]);
            Assert.Equal("N", SetterDeathRecord.ExaminerContactedHelper);
            SetterDeathRecord.ExaminerContactedHelper = "UNK";
            Assert.Equal("UNK", SetterDeathRecord.ExaminerContacted["code"]);
            Assert.Equal(VRDR.CodeSystems.NullFlavor_HL7_V3, SetterDeathRecord.ExaminerContacted["system"]);
            Assert.Equal("unknown", SetterDeathRecord.ExaminerContacted["display"]);
            Assert.Equal("UNK", SetterDeathRecord.ExaminerContactedHelper);
        }

        [Fact]
        public void Get_ExaminerContacted()
        {
            Assert.Equal("N", DeathRecord1_JSON.ExaminerContacted["code"]);
            Assert.Equal(VRDR.CodeSystems.YesNo_0136HL7_V2, DeathRecord1_JSON.ExaminerContacted["system"]);
            Assert.Equal("No", DeathRecord1_JSON.ExaminerContacted["display"]);
            Assert.Equal("N", DeathRecord1_JSON.ExaminerContactedHelper);
            Assert.Equal("Y", DeathCertificateDocument2_JSON.ExaminerContactedHelper);
            Assert.Equal("N", DeathRecord1_XML.ExaminerContacted["code"]);
            Assert.Equal(VRDR.CodeSystems.YesNo_0136HL7_V2, DeathRecord1_XML.ExaminerContacted["system"]);
            Assert.Equal("No", DeathRecord1_XML.ExaminerContacted["display"]);
            Assert.Equal("N", DeathRecord1_XML.ExaminerContactedHelper);
        }

        [Fact]
        public void Set_TobaccoUse()
        {
            Dictionary<string, string> tbu = new Dictionary<string, string>();
            tbu.Add("code", "373066001");
            tbu.Add("system", CodeSystems.SCT);
            tbu.Add("display", "Yes");
            SetterDeathRecord.TobaccoUse = tbu;
            Assert.Equal("373066001", SetterDeathRecord.TobaccoUse["code"]);
            Assert.Equal(CodeSystems.SCT, SetterDeathRecord.TobaccoUse["system"]);
            Assert.Equal("Yes", SetterDeathRecord.TobaccoUse["display"]);
        }

        [Fact]
        public void Get_TobaccoUse()
        {
            Assert.Equal("373066001", DeathRecord1_JSON.TobaccoUse["code"]);
            Assert.Equal(CodeSystems.SCT, DeathRecord1_JSON.TobaccoUse["system"]);
            Assert.Equal("Yes", DeathRecord1_JSON.TobaccoUse["display"]);
            Assert.Equal(ValueSets.ContributoryTobaccoUse.Yes, DeathRecord1_JSON.TobaccoUseHelper);
            Assert.Equal("373066001", DeathRecord1_XML.TobaccoUse["code"]);
            Assert.Equal(CodeSystems.SCT, DeathRecord1_XML.TobaccoUse["system"]);
            Assert.Equal("Yes", DeathRecord1_XML.TobaccoUse["display"]);
        }

        [Fact]
        public void Set_InjuryLocationAddress()
        {
            Dictionary<string, string> iladdress = new Dictionary<string, string>();
            iladdress.Add("addressLine1", "99912 Example Street");
            iladdress.Add("addressLine2", "Line 2");
            iladdress.Add("addressCity", "Bedford");
            iladdress.Add("addressCounty", "Middlesex");
            iladdress.Add("addressCityC", "12345");
            iladdress.Add("addressCountyC", "123");
            iladdress.Add("addressState", "MA");
            iladdress.Add("addressZip", "01730");
            iladdress.Add("addressCountry", "US");
            iladdress.Add("addressPredir", "W");
            iladdress.Add("addressPostdir", "E");
            iladdress.Add("addressStname", "Example");
            iladdress.Add("addressStnum", "11");
            iladdress.Add("addressStdesig", "Street");
            iladdress.Add("addressUnitnum", "3");
            SetterDeathRecord.InjuryLocationAddress = iladdress;
            Assert.Equal("99912 Example Street", SetterDeathRecord.InjuryLocationAddress["addressLine1"]);
            Assert.Equal("Line 2", SetterDeathRecord.InjuryLocationAddress["addressLine2"]);
            Assert.Equal("Bedford", SetterDeathRecord.InjuryLocationAddress["addressCity"]);
            Assert.Equal("Middlesex", SetterDeathRecord.InjuryLocationAddress["addressCounty"]);
            Assert.Equal("12345", SetterDeathRecord.InjuryLocationAddress["addressCityC"]);
            Assert.Equal("123", SetterDeathRecord.InjuryLocationAddress["addressCountyC"]);
            Assert.Equal("MA", SetterDeathRecord.InjuryLocationAddress["addressState"]);
            Assert.Equal("01730", SetterDeathRecord.InjuryLocationAddress["addressZip"]);
            Assert.Equal("US", SetterDeathRecord.InjuryLocationAddress["addressCountry"]);
            Assert.Equal("W", SetterDeathRecord.InjuryLocationAddress["addressPredir"]);
            Assert.Equal("E", SetterDeathRecord.InjuryLocationAddress["addressPostdir"]);
            Assert.Equal("Example", SetterDeathRecord.InjuryLocationAddress["addressStname"]);
            Assert.Equal("11", SetterDeathRecord.InjuryLocationAddress["addressStnum"]);
            Assert.Equal("Street", SetterDeathRecord.InjuryLocationAddress["addressStdesig"]);
            Assert.Equal("3", SetterDeathRecord.InjuryLocationAddress["addressUnitnum"]);

        }

        [Fact]
        public void Get_InjuryLocationAddress()
        {
            Assert.Equal("781 Example Street", DeathRecord1_JSON.InjuryLocationAddress["addressLine1"]);
            Assert.Equal("Line 2", DeathRecord1_JSON.InjuryLocationAddress["addressLine2"]);
            Assert.Equal("Bedford", DeathRecord1_JSON.InjuryLocationAddress["addressCity"]);
            Assert.Equal("Middlesex", DeathRecord1_JSON.InjuryLocationAddress["addressCounty"]);
            Assert.Equal("MA", DeathRecord1_JSON.InjuryLocationAddress["addressState"]);
            Assert.Equal("01730", DeathRecord1_JSON.InjuryLocationAddress["addressZip"]);
            Assert.Equal("US", DeathRecord1_JSON.InjuryLocationAddress["addressCountry"]);
            Assert.Equal("781 Example Street", DeathRecord1_XML.InjuryLocationAddress["addressLine1"]);
            Assert.Equal("Line 2", DeathRecord1_XML.InjuryLocationAddress["addressLine2"]);
            Assert.Equal("Bedford", DeathRecord1_XML.InjuryLocationAddress["addressCity"]);
            Assert.Equal("Middlesex", DeathRecord1_XML.InjuryLocationAddress["addressCounty"]);
            Assert.Equal("MA", DeathRecord1_XML.InjuryLocationAddress["addressState"]);
            Assert.Equal("01730", DeathRecord1_XML.InjuryLocationAddress["addressZip"]);
            Assert.Equal("US", DeathRecord1_XML.InjuryLocationAddress["addressCountry"]);
        }

        [Fact]
        public void Set_InjuryLocationName()
        {
            SetterDeathRecord.InjuryLocationName = "Example Injury Location Name";
            Assert.Equal("Example Injury Location Name", SetterDeathRecord.InjuryLocationName);
            SetterDeathRecord.InjuryLocationName = "";
            Assert.Null(SetterDeathRecord.InjuryLocationName);
            SetterDeathRecord.InjuryLocationName = null;
            Assert.Null(SetterDeathRecord.InjuryLocationName);
        }

        [Fact]
        public void Set_InjuryLocationLatLong()
        {
            SetterDeathRecord.InjuryLocationLatitude = "38.889248";
            SetterDeathRecord.InjuryLocationLongitude = "-77.050636";
            Assert.Equal("-77.050636", SetterDeathRecord.InjuryLocationLongitude);
            Assert.Equal("38.889248", SetterDeathRecord.InjuryLocationLatitude);
        }
        [Fact]
        public void Get_InjuryLocationLatLong()
        {
            Assert.Equal("-77.050636", DeathRecord1_JSON.InjuryLocationLongitude);
            Assert.Equal("38.889248", DeathRecord1_JSON.InjuryLocationLatitude);
            Assert.Equal("-77.050636", DeathRecord1_XML.InjuryLocationLongitude);
            Assert.Equal("38.889248", DeathRecord1_XML.InjuryLocationLatitude);
        }

        [Fact]
        public void Get_InjuryLocationName()
        {
            Assert.Equal("Example Injury Location Name", DeathRecord1_JSON.InjuryLocationName);
            Assert.Equal("Example Injury Location Name", DeathRecord1_XML.InjuryLocationName);
        }

        // [Fact]
        // public void Set_InjuryLocationDescription()
        // {
        //     SetterDeathRecord.InjuryLocationDescription = "Example Injury Location Description";
        //     Assert.Equal("Example Injury Location Description", SetterDeathRecord.InjuryLocationDescription);
        // }

        // [Fact]
        // public void Get_InjuryLocationDescription()
        // {
        //     Assert.Equal("Example Injury Location Description", DeathRecord1_JSON.InjuryLocationDescription);
        //     Assert.Equal("Example Injury Location Description", DeathRecord1_XML.InjuryLocationDescription);
        // }

        [Fact]
        public void Set_InjuryDate()
        {
            SetterDeathRecord.InjuryDate = "2018-02-19T16:48:00";
            Assert.Equal("2018-02-19T16:48:00", SetterDeathRecord.InjuryDate);
            Assert.Equal(2018, (int)SetterDeathRecord.InjuryYear);
            Assert.Equal(2, (int)SetterDeathRecord.InjuryMonth);
            Assert.Equal(19, (int)SetterDeathRecord.InjuryDay);
            Assert.Equal("16:48:00", SetterDeathRecord.InjuryTime);
        }

        [Fact]
        public void Get_InjuryDate()
        {
            Assert.Equal("2018-02-19T16:48:06", DeathRecord1_JSON.InjuryDate);
            Assert.Equal(2018, (int)DeathRecord1_JSON.InjuryYear);
            Assert.Equal(2, (int)DeathRecord1_JSON.InjuryMonth);
            Assert.Equal(19, (int)DeathRecord1_JSON.InjuryDay);
            Assert.Equal("16:48:06", DeathRecord1_JSON.InjuryTime);
            Assert.Equal("13:00:00", DeathCertificateDocument2_JSON.InjuryTime);
            Assert.Equal("2018-02-19T16:48:06", DeathRecord1_XML.InjuryDate);
            Assert.Equal(2018, (int)DeathRecord1_XML.InjuryYear);
            Assert.Equal(2, (int)DeathRecord1_XML.InjuryMonth);
            Assert.Equal(19, (int)DeathRecord1_XML.InjuryDay);
            Assert.Equal("16:48:06", DeathRecord1_XML.InjuryTime);
        }

        [Fact]
        public void Get_InjuryDate_Roundtrip()
        {
            IJEMortality ije1 = new IJEMortality(DeathRecord1_JSON);
            Assert.Equal("2018", ije1.DOI_YR);
            Assert.Equal("02", ije1.DOI_MO);
            Assert.Equal("19", ije1.DOI_DY);
            DeathRecord dr2 = ije1.ToDeathRecord();
            Assert.Equal("2018-02-19T16:48:06", dr2.InjuryDate);
            Assert.Equal(2018, (int)dr2.InjuryYear);
            Assert.Equal(02, (int)dr2.InjuryMonth);
            Assert.Equal(19, (int)dr2.InjuryDay);
        }

        [Fact]
        public void Set_InjuryAtWork()
        {
            Dictionary<string, string> iaw = new Dictionary<string, string>();
            iaw.Add("code", "N");
            iaw.Add("system", VRDR.CodeSystems.YesNo);
            iaw.Add("display", "No");
            SetterDeathRecord.InjuryAtWork = iaw;
            Assert.Equal("N", SetterDeathRecord.InjuryAtWork["code"]);
            Assert.Equal(VRDR.CodeSystems.YesNo, SetterDeathRecord.InjuryAtWork["system"]);
            Assert.Equal("No", SetterDeathRecord.InjuryAtWork["display"]);
            Assert.Equal("N", SetterDeathRecord.InjuryAtWorkHelper);
            SetterDeathRecord.InjuryAtWorkHelper = "Y";
            Assert.Equal("Y", SetterDeathRecord.InjuryAtWork["code"]);
            Assert.Equal(VRDR.CodeSystems.YesNo, SetterDeathRecord.InjuryAtWork["system"]);
            Assert.Equal("Yes", SetterDeathRecord.InjuryAtWork["display"]);
            Assert.Equal("Y", SetterDeathRecord.InjuryAtWorkHelper);
            SetterDeathRecord.InjuryAtWorkHelper = "NA";
            Assert.Equal("NA", SetterDeathRecord.InjuryAtWork["code"]);
            Assert.Equal(VRDR.CodeSystems.NullFlavor_HL7_V3, SetterDeathRecord.InjuryAtWork["system"]);
            Assert.Equal("not applicable", SetterDeathRecord.InjuryAtWork["display"]);
            Assert.Equal("NA", SetterDeathRecord.InjuryAtWorkHelper);
        }

        [Fact]
        public void Get_InjuryAtWork()
        {
            Assert.Equal("N", DeathRecord1_JSON.InjuryAtWork["code"]);
            Assert.Equal(VRDR.CodeSystems.YesNo_0136HL7_V2, DeathRecord1_JSON.InjuryAtWork["system"]);
            Assert.Equal("No", DeathRecord1_JSON.InjuryAtWork["display"]);
            Assert.Equal("N", DeathRecord1_JSON.InjuryAtWorkHelper);
            Assert.Equal(VRDR.ValueSets.YesNoUnknown.No, DeathCertificateDocument2_JSON.InjuryAtWorkHelper);
            Assert.Equal("N", DeathRecord1_XML.InjuryAtWork["code"]);
            Assert.Equal(VRDR.CodeSystems.YesNo_0136HL7_V2, DeathRecord1_XML.InjuryAtWork["system"]);
            Assert.Equal("No", DeathRecord1_XML.InjuryAtWork["display"]);
            Assert.Equal("N", DeathRecord1_XML.InjuryAtWorkHelper);
        }



        [Fact]
        public void Set_DeathLocationLatLong()
        {
            SetterDeathRecord.DeathLocationLatitude = "38.889248";
            SetterDeathRecord.DeathLocationLongitude = "-77.050636";
            Assert.Equal("-77.050636", SetterDeathRecord.DeathLocationLongitude);
            Assert.Equal("38.889248", SetterDeathRecord.DeathLocationLatitude);
        }
        [Fact]
        public void Get_DeathLocationLatLong()
        {
            Assert.Equal("-77.050636", DeathRecord1_JSON.DeathLocationLongitude);
            Assert.Equal("38.889248", DeathRecord1_JSON.DeathLocationLatitude);
            Assert.Equal("38.889248", DeathCertificateDocument2_JSON.DeathLocationLatitude);
            Assert.Equal("-77.050636", DeathRecord1_XML.DeathLocationLongitude);
            Assert.Equal("38.889248", DeathRecord1_XML.DeathLocationLatitude);
        }

        [Fact]
        public void Set_DeathLocationAddress()
        {
            Dictionary<string, string> dtladdress = new Dictionary<string, string>();

            dtladdress.Add("addressLine1", "671 Example Street");
            dtladdress.Add("addressLine2", "Line 2");
            dtladdress.Add("addressCity", "Bedford");
            dtladdress.Add("addressCounty", "Middlesex");
            dtladdress.Add("addressCountyC", "123");
            dtladdress.Add("addressCityC", "12345");
            dtladdress.Add("addressState", "MA");
            dtladdress.Add("addressZip", "01730");
            dtladdress.Add("addressCountry", "US");
            dtladdress.Add("addressPredir", "W");
            dtladdress.Add("addressPostdir", "E");
            dtladdress.Add("addressStname", "Example");
            dtladdress.Add("addressStnum", "11");
            dtladdress.Add("addressStdesig", "Street");
            dtladdress.Add("addressUnitnum", "3");
            SetterDeathRecord.DeathLocationAddress = dtladdress;
            Assert.Equal("671 Example Street", SetterDeathRecord.DeathLocationAddress["addressLine1"]);
            Assert.Equal("Line 2", SetterDeathRecord.DeathLocationAddress["addressLine2"]);
            Assert.Equal("Bedford", SetterDeathRecord.DeathLocationAddress["addressCity"]);
            Assert.Equal("Middlesex", SetterDeathRecord.DeathLocationAddress["addressCounty"]);
            Assert.Equal("12345", SetterDeathRecord.DeathLocationAddress["addressCityC"]);
            Assert.Equal("123", SetterDeathRecord.DeathLocationAddress["addressCountyC"]);
            Assert.Equal("MA", SetterDeathRecord.DeathLocationAddress["addressState"]);
            Assert.Equal("01730", SetterDeathRecord.DeathLocationAddress["addressZip"]);
            Assert.Equal("US", SetterDeathRecord.DeathLocationAddress["addressCountry"]);
            Assert.Equal("W", SetterDeathRecord.DeathLocationAddress["addressPredir"]);
            Assert.Equal("E", SetterDeathRecord.DeathLocationAddress["addressPostdir"]);
            Assert.Equal("Example", SetterDeathRecord.DeathLocationAddress["addressStname"]);
            Assert.Equal("11", SetterDeathRecord.DeathLocationAddress["addressStnum"]);
            Assert.Equal("Street", SetterDeathRecord.DeathLocationAddress["addressStdesig"]);
            Assert.Equal("3", SetterDeathRecord.DeathLocationAddress["addressUnitnum"]);
        }

        [Fact]
        public void Get_DeathLocationAddress()
        {
            Assert.Equal("671 Example Street", DeathRecord1_JSON.DeathLocationAddress["addressLine1"]);
            Assert.Equal("Line 2", DeathRecord1_JSON.DeathLocationAddress["addressLine2"]);
            Assert.Equal("Bedford", DeathRecord1_JSON.DeathLocationAddress["addressCity"]);
            Assert.Equal("Middlesex", DeathRecord1_JSON.DeathLocationAddress["addressCounty"]);
            Assert.Equal("NY", DeathRecord1_JSON.DeathLocationAddress["addressState"]);
            Assert.Equal("YC", DeathRecord1_JSON.DeathLocationAddress["addressJurisdiction"]);
            Assert.Equal("01730", DeathRecord1_JSON.DeathLocationAddress["addressZip"]);
            Assert.Equal("US", DeathRecord1_JSON.DeathLocationAddress["addressCountry"]);
            Assert.Equal("671 Example Street", DeathRecord1_XML.DeathLocationAddress["addressLine1"]);
            Assert.Equal("Line 2", DeathRecord1_XML.DeathLocationAddress["addressLine2"]);
            Assert.Equal("Bedford", DeathRecord1_XML.DeathLocationAddress["addressCity"]);
            Assert.Equal("Middlesex", DeathRecord1_XML.DeathLocationAddress["addressCounty"]);
            Assert.Equal("NY", DeathRecord1_XML.DeathLocationAddress["addressState"]);
            Assert.Equal("01730", DeathRecord1_XML.DeathLocationAddress["addressZip"]);
            Assert.Equal("US", DeathRecord1_XML.DeathLocationAddress["addressCountry"]);
            Assert.Equal("12345", DeathRecord1_XML.DeathLocationAddress["addressCityC"]);
            Assert.Equal("123", DeathRecord1_JSON.DeathLocationAddress["addressCountyC"]);
            Assert.Equal("12345", DeathRecord1_XML.DeathLocationAddress["addressCityC"]);
            Assert.Equal("123", DeathRecord1_JSON.DeathLocationAddress["addressCountyC"]);
            Assert.Equal("W", DeathRecord1_JSON.DeathLocationAddress["addressPredir"]);
            Assert.Equal("E", DeathRecord1_JSON.DeathLocationAddress["addressPostdir"]);
            Assert.Equal("Example", DeathRecord1_JSON.DeathLocationAddress["addressStname"]);
            Assert.Equal("671", DeathRecord1_JSON.DeathLocationAddress["addressStnum"]);
            Assert.Equal("Street", DeathRecord1_JSON.DeathLocationAddress["addressStdesig"]);
            Assert.Equal("3", DeathRecord1_JSON.DeathLocationAddress["addressUnitnum"]);
            Assert.Equal("W", DeathRecord1_XML.DeathLocationAddress["addressPredir"]);
            Assert.Equal("E", DeathRecord1_XML.DeathLocationAddress["addressPostdir"]);
            Assert.Equal("Example", DeathRecord1_XML.DeathLocationAddress["addressStname"]);
            Assert.Equal("671", DeathRecord1_XML.DeathLocationAddress["addressStnum"]);
            Assert.Equal("Street", DeathRecord1_XML.DeathLocationAddress["addressStdesig"]);
            Assert.Equal("3", DeathRecord1_XML.DeathLocationAddress["addressUnitnum"]);
        }

        [Fact]
        public void Set_DeathLocationName()
        {
            SetterDeathRecord.DeathLocationName = "Example Death Location Name";
            Assert.Equal("Example Death Location Name", SetterDeathRecord.DeathLocationName);
            SetterDeathRecord.DeathLocationName = "";
            Assert.Null(SetterDeathRecord.DeathLocationName);
            SetterDeathRecord.DeathLocationName = null;
            Assert.Null(SetterDeathRecord.DeathLocationName);
        }

        [Fact]
        public void Set_DeathLocationJurisdiction()
        {
            SetterDeathRecord.DeathLocationJurisdiction = "MA";
            Assert.Equal("MA", SetterDeathRecord.DeathLocationJurisdiction);
            SetterDeathRecord.DeathLocationJurisdiction = "YC";
            Assert.Equal("YC", SetterDeathRecord.DeathLocationJurisdiction);
        }

        [Fact]
        public void Get_DeathLocationName()
        {
            Assert.Equal("Example Death Location Name", DeathRecord1_JSON.DeathLocationName);
            Assert.Equal("Example Death Location Name", DeathRecord1_XML.DeathLocationName);
        }

        [Fact]
        public void Get_DeathLocationJurisdiction()
        {
            Assert.Equal("YC", DeathRecord1_JSON.DeathLocationJurisdiction);
            Assert.Equal("NY", DeathCertificateDocument2_JSON.DeathLocationJurisdiction);
            Assert.Equal("YC", DeathRecord1_XML.DeathLocationJurisdiction);
        }

        [Fact]
        public void Set_DeathLocationDescription()
        {
            SetterDeathRecord.DeathLocationDescription = "Example Death Location Description";
            Assert.Equal("Example Death Location Description", SetterDeathRecord.DeathLocationDescription);
        }

        [Fact]
        public void Get_DeathLocationDescription()
        {
            Assert.Equal("Example Death Location Description", DeathRecord1_JSON.DeathLocationDescription);
            Assert.Equal("nursing home", DeathCertificateDocument2_JSON.DeathLocationDescription);
            Assert.Equal("Example Death Location Description", DeathRecord1_XML.DeathLocationDescription);
        }

        [Fact]
        public void Set_DateOfDeath()
        {
            SetterDeathRecord.DateOfDeath = "2018-02-19T16:48:00";
            Dictionary<string, string> code = new Dictionary<string, string>();
            code.Add("code", "exact");
            code.Add("system", CodeSystems.DateOfDeathDeterminationMethods);
            code.Add("display", "exact");
            SetterDeathRecord.DateOfDeathDeterminationMethod = code;
            Assert.Equal("2018-02-19T16:48:00", SetterDeathRecord.DateOfDeath);
            Assert.Equal("exact", SetterDeathRecord.DateOfDeathDeterminationMethod["code"]);
        }

        [Fact]
        public void Get_DateOfDeath()
        {
            Assert.Null(DeathCertificateDocument2_JSON.DateOfDeath);
            Assert.Equal(-1, DeathCertificateDocument2_JSON.DeathDay); // partial unknowns return as -1
            Assert.Equal(2020, (DeathCertificateDocument2_JSON.DeathYear));
            Assert.Equal("2020-11-12T00:00:00", DeathCertificateDocument1_JSON.DateOfDeath);
            Assert.Equal(2020, (DeathCertificateDocument1_JSON.DeathYear));
            Assert.Equal("-1", DeathCertificateDocument1_JSON.DeathTime); // absent unknown "_valueTime"
            Assert.Equal("2019-02-19T16:48:06", DeathRecord1_XML.DateOfDeath);
            Assert.Equal(2019, (DeathRecord1_JSON.DeathYear));
        }

        [Fact]
        public void Get_DateOfDeath_Roundtrip()
        {
            IJEMortality ije1 = new IJEMortality(DeathRecord1_JSON);
            Assert.Equal("2019", ije1.DOD_YR);
            Assert.Equal("02", ije1.DOD_MO);
            Assert.Equal("19", ije1.DOD_DY);
            DeathRecord dr2 = ije1.ToDeathRecord();
            Assert.Equal("2019-02-19T16:48:06", dr2.DateOfDeath);
            Assert.Equal(2019, (int)dr2.DeathYear);
            Assert.Equal(02, (int)dr2.DeathMonth);
            Assert.Equal(19, (int)dr2.DeathDay);
        }

        [Fact]
        public void Set_DateOfDeath_Partial_Date()
        {
            //Tuple<string, string>[] datePart = { Tuple.Create("date-year", "2021"), Tuple.Create("date-month", "5"), Tuple.Create("day-absent-reason", "asked-unknown")};
            SetterDeathRecord.DeathYear = 2021;
            SetterDeathRecord.DeathMonth = 5;
            SetterDeathRecord.DeathDay = -1;
            SetterDeathRecord.DeathTime = "10:00:00";
            IJEMortality ije1 = new IJEMortality(SetterDeathRecord, false);
            Assert.Equal("2021", ije1.DOD_YR);
            Assert.Equal("05", ije1.DOD_MO);
            Assert.Equal("99", ije1.DOD_DY);
            Assert.Equal("1000", ije1.TOD);
            DeathRecord dr2 = ije1.ToDeathRecord();
            Assert.Equal(2021, dr2.DeathYear);
            Assert.Equal(5, dr2.DeathMonth);
            Assert.Equal(-1, dr2.DeathDay);
            Assert.Equal("10:00:00", dr2.DeathTime);
        }

        [Fact]
        public void Set_DateOfDeath_Unknown_Partial_Date()
        {
            // Test ability to set dates and times diferentiating between explicitly unknown and unspecified
            DeathRecord d = new DeathRecord();
            Assert.Null(d.DeathYear);
            Assert.Null(d.DeathMonth);
            Assert.Null(d.DeathDay);
            Assert.Null(d.DeathTime);
            d.DeathYear = 2022;
            Assert.Equal(2022, d.DeathYear);
            Assert.Null(d.DeathMonth);
            Assert.Null(d.DeathDay);
            Assert.Null(d.DeathTime);
            d.DeathMonth = -1;
            d.DeathTime = "-1";
            Assert.Equal(2022, d.DeathYear);
            Assert.Equal(-1, d.DeathMonth);
            Assert.Null(d.DeathDay);
            Assert.Equal("-1", d.DeathTime);
            IJEMortality ije = new IJEMortality(d, false);
            Assert.Equal("2022", ije.DOD_YR);
            Assert.Equal("99", ije.DOD_MO);
            Assert.Equal("  ", ije.DOD_DY);
            Assert.Equal("9999", ije.TOD);
        }

        [Fact]
        public void Get_DateOfDeath_Partial_Date()
        {
            DeathRecord dr = new DeathRecord(File.ReadAllText(FixturePath("fixtures/json/BirthAndDeathDateDataAbsent.json")));
            Assert.Equal(2021, (int)dr.DeathYear);
            Assert.Equal(2, (int)dr.DeathMonth);
            Assert.Equal(-1, dr.DeathDay);
        }

        [Fact]
        public void Get_DateOfDeath_Partial_Date_Roundtrip()
        {
            DeathRecord dr = new DeathRecord(File.ReadAllText(FixturePath("fixtures/json/BirthAndDeathDateDataAbsent.json")));
            IJEMortality ije1 = new IJEMortality(dr);
            Assert.Equal("2021", ije1.DOD_YR);
            Assert.Equal("02", ije1.DOD_MO);
            Assert.Equal("99", ije1.DOD_DY);
        }

        [Fact]
        public void Get_DateOfDeath_Timezone()
        {
            // The timezone of the death datetime should not impact the date due to timezone conversion
            DeathRecord record = new DeathRecord(File.ReadAllText(FixturePath("fixtures/json/DeathTimeZone.json")));
            Assert.Equal(17, record.DeathDay);
        }

        /* START DATE OF DEATH PRONOUNCEMENT */
        [Fact]
        public void Set_DateOfDeathPronouncement()
        {
            SetterDeathRecord.DateOfDeathPronouncement = "2019-01-31T17:48:07.498822-05:00"; // check that we are ignoring the timezone
            Assert.Equal("2019-01-31T17:48:07", SetterDeathRecord.DateOfDeathPronouncement);
        }

        [Fact]
        public void Get_DateOfDeathPronouncement()
        {
            Assert.Equal("2018-02-20T16:48:06", DeathRecord1_JSON.DateOfDeathPronouncement);
            Assert.Equal("2020-11-13T16:39:40", DeathCertificateDocument2_JSON.DateOfDeathPronouncement);
            Assert.Equal("2019-02-20T16:48:06", DeathRecord1_XML.DateOfDeathPronouncement);
        }

        [Fact]
        public void Get_DateOfDeathPronouncement_Parts()
        {
            SetterDeathRecord.DateOfDeathPronouncement = "2019-01-31T17:48:07.498822-05:00"; // check that we are ignoring the timezone
            Assert.Equal(2019, (int)SetterDeathRecord.DateOfDeathPronouncementYear);
            Assert.Equal(01, (int)SetterDeathRecord.DateOfDeathPronouncementMonth);
            Assert.Equal(31, (int)SetterDeathRecord.DateOfDeathPronouncementDay);
            Assert.Equal("17:48:07", SetterDeathRecord.DateOfDeathPronouncementTime);

            Assert.Equal(2018, (int)DeathRecord1_JSON.DateOfDeathPronouncementYear);
            Assert.Equal(02, (int)DeathRecord1_JSON.DateOfDeathPronouncementMonth);
            Assert.Equal(20, (int)DeathRecord1_JSON.DateOfDeathPronouncementDay);
            Assert.Equal("16:48:06", DeathRecord1_JSON.DateOfDeathPronouncementTime);
        }

        [Fact]
        public void Get_DateOfDeathPronouncement_PPDATESIGNED_and_PPTIME_Roundtrip()
        {
            IJEMortality ije1 = new IJEMortality(DeathRecord1_JSON);
            Assert.Equal("02202018", ije1.PPDATESIGNED);
            Assert.Equal("1648", ije1.PPTIME);
            DeathRecord dr2 = ije1.ToDeathRecord();
            Assert.Equal("2018-02-20T16:48:06", dr2.DateOfDeathPronouncement);
            Assert.Equal(2018, (int)dr2.DateOfDeathPronouncementYear);
            Assert.Equal(02, (int)dr2.DateOfDeathPronouncementMonth);
            Assert.Equal(20, (int)dr2.DateOfDeathPronouncementDay);
            Assert.Equal("16:48:06", dr2.DateOfDeathPronouncementTime);
        }

        [Fact]
        public void Get_DateOfDeathPronouncement_PPTIME_then_PPDATESIGNED_Roundtrip()
        {
            DeathRecord dr = new DeathRecord(File.ReadAllText(FixturePath("fixtures/json/BirthAndDeathDateNoDatePronounced.json")));
            IJEMortality ije1 = new IJEMortality(dr);
            Assert.Equal("        ", ije1.PPDATESIGNED);
            Assert.Equal("    ", ije1.PPTIME);
            ije1.PPTIME = "1648";
            DeathRecord dr2 = ije1.ToDeathRecord();
            Assert.Equal("16:48:00", dr2.DateOfDeathPronouncement);
            Assert.Null(dr2.DateOfDeathPronouncementYear);
            Assert.Null(dr2.DateOfDeathPronouncementMonth);
            Assert.Null(dr2.DateOfDeathPronouncementDay);
            Assert.Equal("16:48:00", dr2.DateOfDeathPronouncementTime);
            ije1.PPDATESIGNED = "02202018";
            DeathRecord dr3 = ije1.ToDeathRecord();
            Assert.Equal("2018-02-20T16:48:00", dr3.DateOfDeathPronouncement);
            Assert.Equal(2018, (int)dr3.DateOfDeathPronouncementYear);
            Assert.Equal(02, (int)dr3.DateOfDeathPronouncementMonth);
            Assert.Equal(20, (int)dr3.DateOfDeathPronouncementDay);
            Assert.Equal("16:48:00", dr3.DateOfDeathPronouncementTime);
        }

        [Fact]
        public void Set_DateOfDeathPronouncement_PPDATESIGNED_Only_Roundtrip()
        {
            SetterDeathRecord.DateOfDeathPronouncementYear = 2021;
            SetterDeathRecord.DateOfDeathPronouncementMonth = 5;
            SetterDeathRecord.DateOfDeathPronouncementDay = 10;
            SetterDeathRecord.DateOfDeathPronouncementTime = null;
            IJEMortality ije1 = new IJEMortality(SetterDeathRecord, false);
            Assert.Equal("05102021", ije1.PPDATESIGNED);
            Assert.Equal("0000", ije1.PPTIME); // null is converted to time 0000 as a fhir time
            DeathRecord dr2 = ije1.ToDeathRecord();
            Assert.Equal(2021, dr2.DateOfDeathPronouncementYear);
            Assert.Equal(5, dr2.DateOfDeathPronouncementMonth);
            Assert.Equal(10, dr2.DateOfDeathPronouncementDay);
            Assert.Equal("00:00:00", dr2.DateOfDeathPronouncementTime);
        }

        [Fact]
        public void Set_DateOfDeathPronouncement_PPTIME_Only_Roundtrip()
        {
            SetterDeathRecord.DateOfDeathPronouncementTime = "10:00:00";
            IJEMortality ije1 = new IJEMortality(SetterDeathRecord, false);
            Assert.Equal("        ", ije1.PPDATESIGNED);
            Assert.Equal("1000", ije1.PPTIME);
            DeathRecord dr2 = ije1.ToDeathRecord();
            Assert.Null(dr2.DateOfDeathPronouncementYear);
            Assert.Null(dr2.DateOfDeathPronouncementMonth);
            Assert.Null(dr2.DateOfDeathPronouncementDay);
            Assert.Equal("10:00:00", dr2.DateOfDeathPronouncementTime);
        }

        [Fact]
        public void Get_DateOfDeathPronouncement_DateTime()
        {
            DeathRecord dr = new DeathRecord(File.ReadAllText(FixturePath("fixtures/json/BirthAndDeathDateDataAbsent.json")));
            Assert.Equal(2021, (int)dr.DateOfDeathPronouncementYear);
            Assert.Equal(3, (int)dr.DateOfDeathPronouncementMonth);
            Assert.Equal(10, (int)dr.DateOfDeathPronouncementDay);
            Assert.Equal("16:48:06", dr.DateOfDeathPronouncementTime);
        }

        [Fact]
        public void Get_DateOfDeathPronouncement_DateTime_Date_Only()
        {
            DeathRecord dr = new DeathRecord(File.ReadAllText(FixturePath("fixtures/json/BirthAndDeathDateDateOnly.json")));
            Assert.Equal(2021, (int)dr.DateOfDeathPronouncementYear);
            Assert.Equal(3, (int)dr.DateOfDeathPronouncementMonth);
            Assert.Equal(10, (int)dr.DateOfDeathPronouncementDay);
            Assert.Equal("00:00:00", dr.DateOfDeathPronouncementTime);
        }

        [Fact]
        public void Get_DateOfDeathPronouncement_Time_Only()
        {
            DeathRecord dr = new DeathRecord(File.ReadAllText(FixturePath("fixtures/json/BirthAndDeathDateTimeOnly.json")));
            Assert.Null(dr.DateOfDeathPronouncementYear);
            Assert.Null(dr.DateOfDeathPronouncementMonth);
            Assert.Null(dr.DateOfDeathPronouncementDay);
            Assert.Equal("16:48:06", dr.DateOfDeathPronouncementTime);
        }

        [Fact]
        public void Get_DateOfDeathPronouncement_Timezone()
        {
            // The timezone of the death datetime should not impact the date due to timezone conversion
            DeathRecord record = new DeathRecord(File.ReadAllText(FixturePath("fixtures/json/DeathTimeZone.json")));
            Assert.Equal(18, record.DateOfDeathPronouncementDay);
        }

        /* END DATE OF DEATH PRONOUCMENT */

        [Fact]
        public void Set_SurgeryDate()
        {
            SetterDeathRecord.SurgeryDate = "2017-03-18";
            Assert.Equal("2017-03-18", SetterDeathRecord.SurgeryDate);
        }

        [Fact]
        public void Get_SurgeryDate()
        {
            Assert.Equal("2017-03-18", DeathRecord1_JSON.SurgeryDate);
            Assert.Equal("2017-03-18", DeathRecord1_XML.SurgeryDate);
        }

        [Fact]
        public void Get_SurgeryDate_Roundtrip()
        {
            IJEMortality ije1 = new IJEMortality(DeathRecord1_JSON);
            Assert.Equal("2017", ije1.SUR_YR);
            Assert.Equal("03", ije1.SUR_MO);
            Assert.Equal("18", ije1.SUR_DY);
            DeathRecord dr2 = ije1.ToDeathRecord();
            Assert.Equal("2017-03-18", dr2.SurgeryDate);
            Assert.Equal(2017, (int)dr2.SurgeryYear);
            Assert.Equal(03, (int)dr2.SurgeryMonth);
            Assert.Equal(18, (int)dr2.SurgeryDay);
        }

        [Fact]
        public void Set_SurgeryDate_Partial_Date()
        {
            SetterDeathRecord.SurgeryYear = 2017;
            SetterDeathRecord.SurgeryMonth = 3;
            SetterDeathRecord.SurgeryDay = null;
            Assert.Equal(2017, (int)SetterDeathRecord.SurgeryYear);
            Assert.Equal(3, (int)SetterDeathRecord.SurgeryMonth);
            Assert.Null(SetterDeathRecord.SurgeryDay);
        }
        [Fact]
        public void Set_ReceiptDate_Partial_Date()
        {
            SetterDeathRecord.ReceiptYear = 2017;
            SetterDeathRecord.ReceiptMonth = 3;
            SetterDeathRecord.ReceiptDay = null;
            Assert.Equal(2017, (int)SetterDeathRecord.ReceiptYear);
            Assert.Equal(3, (int)SetterDeathRecord.ReceiptMonth);
            Assert.Null(SetterDeathRecord.ReceiptDay);
        }

        [Fact]
        public void Get_ReceiptDate_Roundtrip()
        {
            DeathRecord dr = new DeathRecord(File.ReadAllText(FixturePath("fixtures/json/Bundle-DeathCertificateDocument-Example2.json")));
            IJEMortality ije1 = new IJEMortality(dr, false);
            Assert.Equal("2021", ije1.R_YR);
            Assert.Equal("12", ije1.R_MO);
            Assert.Equal("12", ije1.R_DY);
            DeathRecord dr2 = ije1.ToDeathRecord();
            Assert.Equal("2021-12-12", dr2.ReceiptDate);
            Assert.Equal(2021, (int)dr2.ReceiptYear);
            Assert.Equal(12, (int)dr2.ReceiptMonth);
            Assert.Equal(12, (int)dr2.ReceiptDay);
        }
        [Fact]
        public void Get_SurgeryDate_Partial_Date()
        {
            DeathRecord dr = new DeathRecord(File.ReadAllText(FixturePath("fixtures/json/BirthAndDeathDateDataAbsent.json")));
            IJEMortality ije1 = new IJEMortality(dr);
            Assert.Equal("2017", ije1.SUR_YR);
            Assert.Equal("03", ije1.SUR_MO);
            Assert.Equal("99", ije1.SUR_DY);
        }

        [Fact]
        public void Set_ReceiptDate()
        {
            SetterDeathRecord.ReceiptDate = "2017-03-18";
            Assert.Equal("2017-03-18", SetterDeathRecord.ReceiptDate);
        }

        [Fact]
        public void Get_ReceiptDate()
        {
            Assert.Equal("2021-12-12", DeathCertificateDocument2_JSON.ReceiptDate);
        }

        [Fact]
        public void Set_TransaxConversion()
        {
            SetterDeathRecord.TransaxConversionHelper = ValueSets.TransaxConversion.Artificial_Code_Conversion_No_Other_Action;
            Assert.Equal(ValueSets.TransaxConversion.Artificial_Code_Conversion_No_Other_Action, SetterDeathRecord.TransaxConversionHelper);
        }

        [Fact]
        public void Get_TransaxConversion()
        {
            Assert.Equal(VRDR.ValueSets.TransaxConversion.Conversion_Using_Non_Ambivalent_Table_Entries, DeathCertificateDocument2_JSON.TransaxConversionHelper);
        }

        [Fact]
        public void Set_AcmeSystemReject()
        {
            SetterDeathRecord.AcmeSystemRejectHelper = ValueSets.AcmeSystemReject.Not_Rejected;
            Assert.Equal(ValueSets.AcmeSystemReject.Not_Rejected, SetterDeathRecord.AcmeSystemRejectHelper);
        }

        [Fact]
        public void Get_AcmeSystemReject()
        {
            Assert.Equal(VRDR.ValueSets.AcmeSystemReject.Not_Rejected, DeathCertificateDocument2_JSON.AcmeSystemRejectHelper);
        }

        [Fact]
        public void Set_IntentionalReject()
        {
            SetterDeathRecord.IntentionalRejectHelper = ValueSets.IntentionalReject.Reject1;
            Assert.Equal(ValueSets.IntentionalReject.Reject1, SetterDeathRecord.IntentionalRejectHelper);
        }

        [Fact]
        public void Get_IntentionalReject()
        {
            Assert.Equal(VRDR.ValueSets.IntentionalReject.Reject1, DeathCertificateDocument2_JSON.IntentionalRejectHelper);
        }

        [Fact]
        public void Set_ShipmentNumber()
        {
            SetterDeathRecord.ShipmentNumber = "3";
            Assert.Equal("3", SetterDeathRecord.ShipmentNumber);
        }

        [Fact]
        public void Get_ShipmentNumber()
        {
            Assert.Equal("A2B2", DeathCertificateDocument2_JSON.ShipmentNumber);
        }


        [Fact]
        public void Set_EmergingIssues()
        {
            SetterDeathRecord.EmergingIssue1_1 = "A";
            SetterDeathRecord.EmergingIssue1_2 = "B";
            SetterDeathRecord.EmergingIssue1_3 = "C";
            SetterDeathRecord.EmergingIssue1_4 = "D";
            SetterDeathRecord.EmergingIssue1_5 = "E";
            SetterDeathRecord.EmergingIssue1_6 = "F";
            SetterDeathRecord.EmergingIssue8_1 = "AAAAAAAA";
            SetterDeathRecord.EmergingIssue8_2 = "BBBBBBBB";
            SetterDeathRecord.EmergingIssue8_3 = "CCCCCCCC";
            SetterDeathRecord.EmergingIssue20 = "AAAAAAAAAAAAAAAAAAAA";
            Assert.Equal("A", SetterDeathRecord.EmergingIssue1_1);
            Assert.Equal("B", SetterDeathRecord.EmergingIssue1_2);
            Assert.Equal("C", SetterDeathRecord.EmergingIssue1_3);
            Assert.Equal("D", SetterDeathRecord.EmergingIssue1_4);
            Assert.Equal("E", SetterDeathRecord.EmergingIssue1_5);
            Assert.Equal("F", SetterDeathRecord.EmergingIssue1_6);
            Assert.Equal("AAAAAAAA", SetterDeathRecord.EmergingIssue8_1);
            Assert.Equal("BBBBBBBB", SetterDeathRecord.EmergingIssue8_2);
            Assert.Equal("CCCCCCCC", SetterDeathRecord.EmergingIssue8_3);
            Assert.Equal("AAAAAAAAAAAAAAAAAAAA", SetterDeathRecord.EmergingIssue20);
            IJEMortality ije = new IJEMortality(SetterDeathRecord, false); // Don't validate since we don't care about most fields
            Assert.Equal("A", ije.PLACE1_1);
            Assert.Equal("B", ije.PLACE1_2);
            Assert.Equal("C", ije.PLACE1_3);
            Assert.Equal("D", ije.PLACE1_4);
            Assert.Equal("E", ije.PLACE1_5);
            Assert.Equal("F", ije.PLACE1_6);
            Assert.Equal("AAAAAAAA", ije.PLACE8_1);
            Assert.Equal("BBBBBBBB", ije.PLACE8_2);
            Assert.Equal("CCCCCCCC", ije.PLACE8_3);
            Assert.Equal("AAAAAAAAAAAAAAAAAAAA", ije.PLACE20);
        }

        [Fact]
        public void Get_EmergingIssues()
        {
            Assert.Equal("A", DeathRecord1_JSON.EmergingIssue1_1);
            Assert.Equal("B", DeathRecord1_JSON.EmergingIssue1_2);
            Assert.Equal("C", DeathRecord1_JSON.EmergingIssue1_3);
            Assert.Equal("D", DeathRecord1_JSON.EmergingIssue1_4);
            Assert.Equal("E", DeathRecord1_JSON.EmergingIssue1_5);
            Assert.Equal("F", DeathRecord1_JSON.EmergingIssue1_6);
            Assert.Equal("AAAAAAAA", DeathRecord1_JSON.EmergingIssue8_1);
            Assert.Equal("BBBBBBBB", DeathRecord1_JSON.EmergingIssue8_2);
            Assert.Equal("CCCCCCCC", DeathRecord1_JSON.EmergingIssue8_3);
            Assert.Equal("AAAAAAAAAAAAAAAAAAAA", DeathRecord1_JSON.EmergingIssue20);
            Assert.Equal("A", DeathRecord1_XML.EmergingIssue1_1);
            Assert.Equal("B", DeathRecord1_XML.EmergingIssue1_2);
            Assert.Equal("C", DeathRecord1_XML.EmergingIssue1_3);
            Assert.Equal("D", DeathRecord1_XML.EmergingIssue1_4);
            Assert.Equal("E", DeathRecord1_XML.EmergingIssue1_5);
            Assert.Equal("F", DeathRecord1_XML.EmergingIssue1_6);
            Assert.Equal("AAAAAAAA", DeathRecord1_XML.EmergingIssue8_1);
            Assert.Equal("BBBBBBBB", DeathRecord1_XML.EmergingIssue8_2);
            Assert.Equal("CCCCCCCC", DeathRecord1_XML.EmergingIssue8_3);
            Assert.Equal("AAAAAAAAAAAAAAAAAAAA", DeathRecord1_XML.EmergingIssue20);
        }

        [Fact]
        public void Set_EntityAxisCodes()
        {
            SetterDeathRecord.EntityAxisCauseOfDeath = new[] { (LineNumber: 2, Position: 1, Code: "T27.3", ECode: true),
                                                               (LineNumber: 2, Position: 3, Code: "K27.10", ECode: false) };
            var eacGet = SetterDeathRecord.EntityAxisCauseOfDeath;
            Assert.Equal(2, eacGet.Count());
            Assert.Equal(2, eacGet.ElementAt(0).LineNumber);
            Assert.Equal(1, eacGet.ElementAt(0).Position);
            Assert.Equal("T27.3", eacGet.ElementAt(0).Code);
            Assert.True(eacGet.ElementAt(0).ECode);
            Assert.Equal(2, eacGet.ElementAt(1).LineNumber);
            Assert.Equal(3, eacGet.ElementAt(1).Position);
            Assert.Equal("K27.10", eacGet.ElementAt(1).Code);
            Assert.False(eacGet.ElementAt(1).ECode);

            IJEMortality ije = new IJEMortality(SetterDeathRecord, false); // Don't validate since we don't care about most fields
            string fmtEac = "21T273 &23K2710".PadRight(160, ' ');
            Assert.Equal(fmtEac, ije.EAC);
        }

        [Fact]
        public void Get_EntityAxisCodes()
        {
            var eacGet = DeathCertificateDocument2_JSON.EntityAxisCauseOfDeath;
            Assert.Single(eacGet);
            Assert.Equal(1, eacGet.ElementAt(0).LineNumber);
            Assert.Equal(1, eacGet.ElementAt(0).Position);
            Assert.Equal("J96.0", eacGet.ElementAt(0).Code);
            Assert.False(eacGet.ElementAt(0).ECode);
            // Add more items to IG example
            eacGet = CauseOfDeathCodedContentBundle1_JSON.EntityAxisCauseOfDeath;
            Assert.Equal(4, eacGet.LongCount());
            Assert.Equal(1, eacGet.ElementAt(0).LineNumber);
            Assert.Equal(1, eacGet.ElementAt(0).Position);
            Assert.Equal("J96.0", eacGet.ElementAt(0).Code);
            Assert.False(eacGet.ElementAt(0).ECode);
            Assert.Equal(2, eacGet.ElementAt(1).LineNumber);
            Assert.Equal(1, eacGet.ElementAt(1).Position);
            Assert.Equal("T27.3", eacGet.ElementAt(1).Code);
            Assert.False(eacGet.ElementAt(1).ECode);
            Assert.Equal(2, eacGet.ElementAt(2).LineNumber);
            Assert.Equal(2, eacGet.ElementAt(2).Position);
            Assert.Equal("X00", eacGet.ElementAt(2).Code);
            Assert.True(eacGet.ElementAt(2).ECode);
            Assert.Equal(2, eacGet.ElementAt(3).LineNumber);
            Assert.Equal(3, eacGet.ElementAt(3).Position);
            Assert.Equal("T27.2", eacGet.ElementAt(3).Code);
            Assert.False(eacGet.ElementAt(3).ECode);
        }

        [Fact]
        public void Set_RecordAxisCodes()
        {
            SetterDeathRecord.RecordAxisCauseOfDeath = new[] { (Position: 1, Code: "T27.3", Pregnancy: true), (Position: 2, Code: "T27.5", Pregnancy: true) };
            var racGet = SetterDeathRecord.RecordAxisCauseOfDeath;
            Assert.Equal(2, racGet.Count());
            Assert.Equal(1, racGet.ElementAt(0).Position);
            Assert.Equal("T27.3", racGet.ElementAt(0).Code);
            Assert.True(racGet.ElementAt(0).Pregnancy); // Pregnancy flag is not enforced as only allowed in position 2
            Assert.Equal(2, racGet.ElementAt(1).Position);
            Assert.Equal("T27.5", racGet.ElementAt(1).Code);
            Assert.True(racGet.ElementAt(1).Pregnancy);

            IJEMortality ije = new IJEMortality(SetterDeathRecord, false); // Don't validate since we don't care about most fields
            string fmtRac = "T2731T2751".PadRight(100, ' ');
            Assert.Equal(fmtRac, ije.RAC);
        }

        [Fact]
        public void Get_RecordAxisCodes()
        {
            var racGet = DeathCertificateDocument2_JSON.RecordAxisCauseOfDeath;
            Assert.Single(racGet);
            Assert.Equal(1, racGet.ElementAt(0).Position);
            Assert.Equal("J96.0", racGet.ElementAt(0).Code);
            Assert.False(racGet.ElementAt(0).Pregnancy);
            racGet = CauseOfDeathCodedContentBundle1_JSON.RecordAxisCauseOfDeath;
            Assert.Single(racGet);
            Assert.Equal(1, racGet.ElementAt(0).Position);
            Assert.Equal("J96.0", racGet.ElementAt(0).Code);
            Assert.False(racGet.ElementAt(0).Pregnancy);
        }

        [Fact]
        public void CheckConnectathonRecord1()
        {
            DeathRecord dr1 = VRDR.Connectathon.TwilaHilty();
            Assert.Equal("21", dr1.AgeAtDeath["value"]);
            Assert.Equal("female", dr1.SexAtDeath["code"]);
            Assert.NotNull(dr1.ToDescription()); // This endpoint is used by Canary
            IJEMortality ije = new IJEMortality(dr1, false); // Don't validate since we don't care about most fields
            Assert.Equal("021", ije.AGE);
            Assert.Equal("F", ije.SEX);
            Assert.Equal("531869507", ije.SSN);
            Assert.Equal("Hypoxemia", ije.COD1A.Trim());
            Assert.Equal("N", ije.DETHNIC1);
        }

        [Fact]
        public void CheckConnectathonRecord1DeathDatePartialTime()
        {
            DeathRecord dr1 = VRDR.Connectathon.TwilaHilty();
            Assert.Equal("10:00:00", dr1.DeathTime);
        }
        [Fact]
        public void CheckConnectathonRecordLocationOfDeath()
        {
            DeathRecord dr1 = VRDR.Connectathon.TwilaHilty();
            String dr1Str = dr1.ToJson();
            Assert.Contains("Location of death", dr1Str);
            Assert.False(dr1Str.Contains("Place of death"), dr1Str);
        }

        [Fact]
        public void CheckConnectathonRecordHispanicDisplayValue()
        {
            DeathRecord dr1 = VRDR.Connectathon.TwilaHilty();
            String dr1Str = dr1.ToJson();
            Assert.Contains("Hispanic Mexican", dr1Str);
            Assert.Contains("Hispanic Puerto Rican", dr1Str);
            Assert.Contains("Hispanic Cuban", dr1Str);
            Assert.Contains("Hispanic Other", dr1Str);
        }

        [Fact]
        public void CheckConnectathonRecordRaceDisplayValue()
        {
            DeathRecord dr1 = VRDR.Connectathon.TwilaHilty();
            String dr1Str = dr1.ToJson();
            Assert.Contains("Black Or African American", dr1Str);
            Assert.Contains("American Indian Or Alaskan Native", dr1Str);
            Assert.Contains("Asian Indian", dr1Str);
            Assert.Contains("Other Asian", dr1Str);
            Assert.Contains("Native Hawaiian", dr1Str);
            Assert.Contains("Guamanian Or Chamorro", dr1Str);
            Assert.Contains("Other Pacific Islander", dr1Str);
            Assert.Contains("Other Race", dr1Str);
        }

        [Fact]
        public void CheckConnectathonRecord2()
        {
            DeathRecord dr1 = VRDR.Connectathon.FideliaAlsup();
            Assert.Equal("63", dr1.AgeAtDeath["value"]);
            Assert.NotNull(dr1.ToDescription()); // This endpoint is used by Canary
            IJEMortality ije = new IJEMortality(dr1, false); // Don't validate since we don't care about most fields
            Assert.Equal("063", ije.AGE);
            Assert.Equal("478151044", ije.SSN);
            Assert.Equal("", ije.HOWINJ.Trim());
            Assert.Equal("H", ije.DETHNIC2);
        }

        [Fact]
        public void CheckConnectathonRecord3()
        {
            DeathRecord dr1 = VRDR.Connectathon.DavisLineberry();
            Assert.Equal("3", dr1.AgeAtDeath["value"]);
            Assert.Equal("male", dr1.SexAtDeath["code"]);
            Assert.NotNull(dr1.ToDescription()); // This endpoint is used by Canary
            IJEMortality ije = new IJEMortality(dr1, false); // Don't validate since we don't care about most fields
            Assert.Equal("003", ije.AGE);
            Assert.Equal("M", ije.SEX);
            Assert.Equal("429471420", ije.SSN);
            Assert.Equal("Pending", ije.COD1A.Trim());
            Assert.Equal("N", ije.DETHNIC1);
        }

        [Fact]
        public void Test_GetCauseOfDeathCodedContentBundle()
        {
            Bundle bundle = DeathRecord1_JSON.GetCauseOfDeathCodedContentBundle();
            DeathRecord codedcontentbundle = new DeathRecord(bundle);
            Assert.NotNull(bundle);
            Assert.Equal("2019YC000182", codedcontentbundle.DeathRecordIdentifier);
            Assert.Equal("000182", codedcontentbundle.Identifier);
            Assert.Equal("000000000042", codedcontentbundle.StateLocalIdentifier1);
            Assert.Equal("100000000001", codedcontentbundle.StateLocalIdentifier2);
            Assert.Equal("", codedcontentbundle.DeathLocationType["code"]); // should be empty
            Assert.Null(codedcontentbundle.BirthDay); // should be missing
            // TODO: Fill out tests
        }

        [Fact]
        public void Test_GetDemographicCodedContentBundle()
        {
            Bundle bundle = DeathRecord1_JSON.GetDemographicCodedContentBundle();
            Assert.NotNull(bundle);
            DeathRecord codedcontentbundle = new DeathRecord(bundle);
            Assert.Equal("2019YC000182", codedcontentbundle.DeathRecordIdentifier);
            Assert.Equal("000182", codedcontentbundle.Identifier);
            Assert.Equal("000000000042", codedcontentbundle.StateLocalIdentifier1);
            Assert.Equal("100000000001", codedcontentbundle.StateLocalIdentifier2);
            Assert.Equal("", codedcontentbundle.DeathLocationType["code"]); // should be empty
            Assert.Null(codedcontentbundle.BirthDay); // should be missing
            // TODO: Fill out tests
        }
        [Fact]
        public void Test_MortalityRosterBundle()
        {
            Bundle bundle = DeathRecord1_JSON.GetMortalityRosterBundle(false);
            DeathRecord mortalityrosterbundle = new DeathRecord(bundle);
            Assert.NotNull(bundle);
            var numExtensions = bundle.Meta.Extension.Count();
            Assert.Equal(2, numExtensions); // alias and replace
            Assert.Equal("2019YC000182", mortalityrosterbundle.DeathRecordIdentifier);
            Assert.Equal("000182", mortalityrosterbundle.Identifier);
            Assert.Equal("000000000042", mortalityrosterbundle.StateLocalIdentifier1);
            Assert.Equal("100000000001", mortalityrosterbundle.StateLocalIdentifier2);
            Assert.Equal("", mortalityrosterbundle.CertificationRole["code"]); // should be empty
            Assert.Null(mortalityrosterbundle.PregnancyStatusHelper); // should be missing
            // TODO: Fill out tests
        }
        [Fact]
        public void TestTRXRoundTrip()
        {
            IJEMortality ije = new IJEMortality();
            ije.DOD_YR = "2021";
            ije.DSTATE = "MA";
            ije.FILENO = "578660";
            ije.MAN_UC = "I219";
            ije.EAC = "21I219  31I251  61E119  62F179  63I10   64E780  ";
            ije.RAC = "I219 E119 E780 F179 I10  I251 ";
            Assert.Equal("I219 E119 E780 F179 I10  I251 ".PadRight(100), ije.RAC);
            ije.AUXNO = "579927".PadRight(12, ' ');
            ije.MFILED = "0";
            ije.MANNER = "N";
            ije.trx.CS = "1";
            ije.trx.SHIP = "497";
            ije.AUTOP = "Y";
            ije.AUTOPF = "Y";
            ije.TOBAC = "Y";
            ije.PREG = "8";
            ije.CERTL = "D";
            Assert.Equal("D", ije.CERTL);
            ije.CERTL = "DDDD";
            Assert.Equal("DDDD", ije.CERTL);
            ije.TRANSPRT = "Hover Board Rider";
            ije.INACT = "9";
            DeathRecord record = ije.ToDeathRecord();
            DeathRecord record1 = new DeathRecord(record.GetCauseOfDeathCodedContentBundle());
            IJEMortality ije2 = new IJEMortality(record);
            Assert.Equal("2021", ije2.DOD_YR);
            Assert.Equal("MA", ije2.DSTATE);
            Assert.Equal("578660", ije2.FILENO);
            Assert.Equal("I219", ije2.MAN_UC);
            Assert.Equal("21I219  31I251  61E119  62F179  63I10   64E780".PadRight(160), ije2.EAC);
            Assert.Equal("I219 E119 E780 F179 I10  I251  ".PadRight(100), ije2.RAC);
            Assert.Equal("579927".PadRight(12, ' '), ije2.AUXNO);
            Assert.Equal("0", ije2.MFILED);
            Assert.Equal("N", ije2.MANNER);
            Assert.Equal("1", ije2.trx.CS);
            Assert.Equal("497", ije2.trx.SHIP);
            Assert.Equal("Y", ije2.AUTOP);
            Assert.Equal("Y", ije2.AUTOPF);
            Assert.Equal("Y", ije2.TOBAC);
            Assert.Equal("8", ije2.PREG);
            Assert.Equal("DDDD", ije2.CERTL);
            Assert.Equal("Hover Board Rider", ije2.TRANSPRT);
            Assert.Equal("9", ije2.INACT);
        }
        [Fact]
        public void TestRaceLiteralRoundTrip()
        {
            // setup race literals in an IJE record
            var ije = new IJEMortality();
            ije.DOD_YR = "2021";
            ije.DSTATE = "MA";
            ije.FILENO = "578660";
            ije.RACE16 = "Apache";
            ije.RACE17 = "Lipan Apache";
            ije.RACE18 = "Taiwanese";
            ije.RACE19 = "Gaoshan";
            ije.RACE20 = "Maori";
            ije.RACE21 = "Waikato";
            ije.RACE22 = "Vulcan";
            ije.RACE23 = "Hgrtcha";

            // convert to a DeathRecord and check race literals
            var record = ije.ToDeathRecord();
            var race = record.Race.ToList().ToDictionary(x => x.Item1, x => x.Item2);
            Assert.Equal(2021, record.DeathYear);
            Assert.Equal("MA", record.DeathLocationJurisdiction);
            Assert.Equal("578660", record.Identifier);
            Assert.Equal("Apache", race.GetValueOrDefault("FirstAmericanIndianOrAlaskanNativeLiteral"));
            Assert.Equal("Lipan Apache", race.GetValueOrDefault("SecondAmericanIndianOrAlaskanNativeLiteral"));
            Assert.Equal("Taiwanese", race.GetValueOrDefault("FirstOtherAsianLiteral"));
            Assert.Equal("Gaoshan", race.GetValueOrDefault("SecondOtherAsianLiteral"));
            Assert.Equal("Maori", race.GetValueOrDefault("FirstOtherPacificIslanderLiteral"));
            Assert.Equal("Waikato", race.GetValueOrDefault("SecondOtherPacificIslanderLiteral"));
            Assert.Equal("Vulcan", race.GetValueOrDefault("FirstOtherRaceLiteral"));
            Assert.Equal("Hgrtcha", race.GetValueOrDefault("SecondOtherRaceLiteral"));

            // convert back to an IJE record and check race literals for roundtrip
            var ije2 = new IJEMortality(record);
            Assert.Equal("2021", ije2.DOD_YR);
            Assert.Equal("MA", ije2.DSTATE);
            Assert.Equal("578660", ije2.FILENO);
            Assert.Equal("Apache", ije.RACE16);
            Assert.Equal("Lipan Apache", ije.RACE17);
            Assert.Equal("Taiwanese", ije.RACE18);
            Assert.Equal("Gaoshan", ije.RACE19);
            Assert.Equal("Maori", ije.RACE20);
            Assert.Equal("Waikato", ije.RACE21);
            Assert.Equal("Vulcan", ije.RACE22);
            Assert.Equal("Hgrtcha", ije.RACE23);
        }

        [Fact]
        public void TestLoadRaceEthnicityLiteralsFromFHIRJSON()
        {
            // confirm that we can read a death record from JSON and each of the race literal records
            DeathRecord dr = new DeathRecord(File.ReadAllText(FixturePath("fixtures/json/AllRaceLiterals.json")));
            var race = dr.Race.ToList().ToDictionary(x => x.Item1, x => x.Item2);
            Assert.Equal(2022, dr.DeathYear);
            Assert.Equal("ID", dr.DeathLocationJurisdiction);
            Assert.Equal("000182", dr.Identifier);
            Assert.Equal("Apache", race.GetValueOrDefault("FirstAmericanIndianOrAlaskanNativeLiteral"));
            Assert.Equal("Lipan Apache", race.GetValueOrDefault("SecondAmericanIndianOrAlaskanNativeLiteral"));
            Assert.Equal("Taiwanese", race.GetValueOrDefault("FirstOtherAsianLiteral"));
            Assert.Equal("Gaoshan", race.GetValueOrDefault("SecondOtherAsianLiteral"));
            Assert.Equal("Maori", race.GetValueOrDefault("FirstOtherPacificIslanderLiteral"));
            Assert.Equal("Waikato", race.GetValueOrDefault("SecondOtherPacificIslanderLiteral"));
            Assert.Equal("Vulcan", race.GetValueOrDefault("FirstOtherRaceLiteral"));
            Assert.Equal("Hgrtcha", race.GetValueOrDefault("SecondOtherRaceLiteral"));
            Assert.Equal("Panamanian", dr.EthnicityLiteral); // HispanicLiteral
        }

        [Fact]
        public void TestInvalidRaceLiteralThrowsException()
        {
            var record = new DeathRecord();
            var race = record.Race.ToList();
            race.Add(Tuple.Create("InvalidRaceLiteral", "Foo"));
            var raceArray = race.Distinct().ToArray();
            Assert.Throws<System.ArgumentException>(() => record.Race = raceArray);
        }

        [Fact]
        public void Get_BlankLocationNames()
        {
            DeathRecord dr = new DeathRecord(File.ReadAllText(FixturePath("fixtures/json/BlankLocationNames.json")));
            Assert.Null(dr.DeathLocationName);
            Assert.Null(dr.InjuryLocationName);
            Assert.Null(dr.DispositionLocationName);
        }

        [Fact]
        public void Get_EmptyLiteralRaceFields()
        {
            // A record with an a literal race field (e.g., FirstAmericanIndianOrAlaskanNativeLiteral) with no content should parse successfully
            DeathRecord dr = new DeathRecord(File.ReadAllText(FixturePath("fixtures/json/EmptyRaceLiteral.json")));
            Assert.DoesNotContain(dr.Race, (t => t.Item1 == "FirstAmericanIndianOrAlaskanNativeLiteral"));
        }

        [Fact]
        public void TestForOverwrites()
        {
            // This test makes sure that there are no fields that, when writing them, accidentally change another field;
            // we test this by going through each field, setting it to a value, and then setting all other fields to a value,
            // and then checking to make sure the original field still has the same value

            // Make a list of all the fields we'll test and a valid value for each
            Dictionary<string, string> fields = new Dictionary<string, string>
            {
                // This list of fields is fairly comprehensive, though some have been intentionally left out:
                // STATETEXT_D, STATEBTH, and FUNSTATE (setting these are a no-ops)
                // MNAME, DMIDDLE, DDADMID, DMOMMID, SPOUSEMIDNAME, CERTMIDDLE (middle names behave oddly due to how FHIR represents names)
                { "DOD_YR", "2023"}, //2022" },
                { "DSTATE", "CT" },
                { "FILENO", "000001" },
                { "AUXNO", "000000000001" },
                { "MFILED", "0" },
                { "GNAME", "Twila" },
                { "LNAME", "Hilty" },
                { "SUFF", "Jr." },
                { "FLNAME", "Brown" },
                { "SEX", "F" },
                { "SSN", "531869507" },
                { "AGETYPE", "1" },
                { "AGE", "021" },
                { "AGE_BYPASS", "0" },
                { "DOB_YR", "2002" },
                { "DOB_MO", "01" },
                { "DOB_DY", "01" },
                { "BPLACE_CNT", "US" },
                { "BPLACE_ST", "CT" },
                { "CITYC", "37000" },
                { "COUNTYC", "003" },
                { "STATEC", "CT" },
                { "COUNTRYC", "US" },
                { "LIMITS", "Y" },
                { "MARITAL", "S" },
                { "MARITAL_BYPASS", "0" },
                { "DPLACE", "1" },
                { "COD", "001" },
                { "DISP", "B" },
                { "DOD_MO", "01" },
                { "DOD_DY", "10" },
                { "TOD", "1000" },
                { "DEDUC", "8" },
                { "DEDUC_BYPASS", "0" },
                { "DETHNIC1", "N" },
                { "DETHNIC2", "N" },
                { "DETHNIC3", "N" },
                { "DETHNIC4", "N" },
                { "RACE1", "Y" },
                { "RACE2", "N" },
                { "RACE3", "N" },
                { "RACE4", "N" },
                { "RACE5", "N" },
                { "RACE6", "N" },
                { "RACE7", "N" },
                { "RACE8", "N" },
                { "RACE9", "N" },
                { "RACE10", "N" },
                { "RACE11", "N" },
                { "RACE12", "N" },
                { "RACE13", "N" },
                { "RACE14", "N" },
                { "RACE15", "N" },
                { "OCCUP", "Teacher" },
                { "INDUST", "Education" },
                { "BCNO", "717171" },
                { "IDOB_YR", "1961" },
                { "BSTATE", "YC" },
                { "R_YR", "2021" },
                { "R_MO", "12" },
                { "R_DY", "12" },
                { "DOR_YR", "2020" },
                { "DOR_MO", "11" },
                { "DOR_DY", "15" },
                { "MANNER", "N" },
                { "INT_REJ", "1" },
                { "SYS_REJ", "0" },
                { "INJPL", "0" },
                { "MAN_UC", "J960" },
                { "ACME_UC", "J960" },
                { "EAC", "11J960" },
                { "TRX_FLG", "3" },
                { "RAC", "J960" },
                { "AUTOP", "N" },
                { "AUTOPF", "X" },
                { "TOBAC", "U" },
                { "PREG", "8"}, //2" },
                { "PREG_BYPASS", "0" },
                { "DOI_MO", "01"}, //11" },
                { "DOI_DY", "10"}, //02" },
                { "DOI_YR", "2022"}, //2019" },
                { "TOI_HR", "1300" },
                { "WORKINJ", "N" },
                { "CERTL", "D" },
                { "INACT", "1" },
                { "AUXNO2", "100000000001" },
                { "STATESP", "20220101" },
                { "SUR_MO", "01" },
                { "SUR_DY", "10" },
                { "SUR_YR", "2022" },
                { "ARMEDF", "Y" },
                { "DINSTI", "Pecan Grove Nursing Home" },
                { "CITYTEXT_D", "Albany" },
                { "CITYCODE_D", "00000" },
                { "LONG_D", "-77.050636" },
                { "LAT_D", "38.889248" },
                { "SPOUSELV", "1" },
                { "SPOUSEL", "Gazette" },
                { "STNUM_R", "1829" },
                { "PREDIR_R", "North" },
                { "STNAME_R", "Charles" },
                { "STDESIG_R", "Avenue" },
                { "POSTDIR_R", "Southeast" },
                { "UNITNUM_R", "Apt 2B" },
                { "CITYTEXT_R", "Hartford" },
                { "ZIP9_R", "06107" },
                { "COUNTYTEXT_R", "Hartford" },
                { "STATETEXT_R", "Connecticut" },
                { "COUNTRYTEXT_R", "United States" },
                { "ADDRESS_R", "4437 North Charles Avenue Southeast Apt 2B" },
                { "DETHNICE", "233" },
                { "DDADF", "John" },
                { "DMOMF", "Momfirst" },
                { "DMOMMDN", "Suzette" },
                { "REFERRED", "Y" },
                { "POILITRL", "Home" },
                { "HOWINJ", "drug toxicity" },
                { "TRANSPRT", "PE" },
                { "COUNTYCODE_I", "000" },
                { "CITYCODE_I", "00000" },
                { "REPLACE", "0" },
                { "COD1A", "Hypoxemia"}, //Cardiopulmonary arrest" },
                { "INTERVAL1A", "4 Days"}, //4 Hours" },
                { "COD1B", "MRSA Pneumonia"}, //Eclampsia" },
                { "INTERVAL1B", "4 Days"}, //3 Months" },
                { "OTHERCONDITION", "hypertensive heart disease" },
                { "DBPLACECITY", "Roanoke" },
                { "SPOUSESUFFIX", "Ss" },
                { "FATHERSUFFIX", "Sr" },
                { "MOTHERSSUFFIX", "Ms" },
                { "INFORMRELATE", "Friend of family" },
                { "DISPSTATECD", "VA" },
                { "DISPCITY", "Danville" },
                { "FUNFACNAME", "Lancaster Funeral Home and Crematory" },
                { "FUNFACADDRESS", "211 High Street" },
                { "FUNCITYTEXT", "Lancaster" },
                { "FUNZIP", "17573" },
                { "PPDATESIGNED", "11132020" },
                { "PPTIME", "2139" },
                { "CERTFIRST", "Jim" },
                { "CERTLAST", "Black" },
                { "CERTSUFFIX", "Cr" },
                { "CERTADDRESS", "44 South Street" },
                { "CERTCITYTEXT", "Bird in Hand" },
                { "CERTSTATECD", "PA" },
                { "CERTSTATE", "Pennsylvania" },
                { "CERTZIP", "17505" },
                { "CERTDATE", "11142020" },
                { "DTHCOUNTRYCD", "US" },
                { "DTHCOUNTRY", "United States" },
                { "PLACE1_1", "H" },
                { "PLACE1_2", "I" },
                { "PLACE8_1", "Hi 8_1" },
                { "PLACE20", "Hi 20_1"}
            };
            // For each field, create a record, set that field, set all the other fields, and make sure the first field still has the same value
            foreach (var (field, value) in fields)
            {
                IJEMortality ije = new IJEMortality();
                PropertyInfo property = typeof(IJEMortality).GetProperty(field);
                property.SetValue(ije, value);
                foreach (var (overwriteField, overwriteValue) in fields)
                {
                    if (overwriteField == field) continue; // Don't rewrite the field we're testing
                    PropertyInfo overwriteProperty = typeof(IJEMortality).GetProperty(overwriteField);
                    overwriteProperty.SetValue(ije, overwriteValue);
                }
                // Console.WriteLine($"Testing {field} with value {value}");
                Assert.Equal(value, ((string)property.GetValue(ije)).Trim());
            }
        }

        [Fact]
        public void GetShouldNotReturnEmptyString()
        {
            // An empty string field should never return an empty string to mean no value, should return null
            DeathRecord blank = new DeathRecord();
            List<PropertyInfo> properties = typeof(DeathRecord).GetProperties().ToList();
            foreach (PropertyInfo property in properties)
            {
                if (property.PropertyType.ToString() == "System.String")
                {
                    object value = property.GetValue(blank);
                    if (property.Name == "DeathRecordIdentifier")
                    {
                        Assert.Equal(value, "0000XX000000");
                    }
                    else if (property.Name == "Gender")
                    {
                        Assert.Equal(value, "unknown");
                    }
                    else
                    {
                        Assert.Null(value);
                    }
                }
            }
        }

        [Fact]
        public void EmptyRecordRoundTrip()
        {
            // If we have an empty record and copy it over to a new empty record we shouldn't wind up with any blank strings
            DeathRecord blank = new DeathRecord();
            DeathRecord copy = new DeathRecord();
            List<PropertyInfo> properties = typeof(DeathRecord).GetProperties().ToList();
            foreach (PropertyInfo property in properties)
            {
                property.SetValue(copy, property.GetValue(blank));
            }
            Assert.DoesNotContain("\"\"", blank.ToJson());
            Assert.DoesNotContain("\"\"", copy.ToJson());
        }

        [Fact]
        public void SetWithEmptyStrings()
        {
            // Starting with an empty record and setting all fields with the "empty" version of the field (e.g., for strings use "")
            // should not actually set any fields to empty strings in the resulting JSON
            DeathRecord blank = new DeathRecord();
            List<PropertyInfo> properties = typeof(DeathRecord).GetProperties().ToList();
            foreach (PropertyInfo property in properties)
            {
                switch (property.PropertyType.ToString())
                {
                    case "System.String":
                        property.SetValue(blank, "");
                        break;
                    case "System.String[]":
                        property.SetValue(blank, new string[] { "", "" });
                        break;
                    case "System.Collections.Generic.Dictionary`2[System.String,System.String]":
                        property.SetValue(blank, new Dictionary<string, string> { { "code", "" }, { "system", "" }, { "display", "" } });
                        break;
                    case "System.Collections.Generic.IEnumerable`1[System.ValueTuple`4[System.Int32,System.Int32,System.String,System.Boolean]]":
                        property.SetValue(blank, new[] { (LineNumber: 1, Position: 1, Code: "", ECode: false) });
                        break;
                    case "System.Collections.Generic.IEnumerable`1[System.ValueTuple`3[System.Int32,System.String,System.Boolean]]":
                        property.SetValue(blank, new[] { (Position: 1, Code: "", Pregnancy: false) });
                        break;
                    case "System.Tuple`2[System.String,System.String][]":
                        property.SetValue(blank, new Tuple<string, string>[] { Tuple.Create(NvssRace.White, "") });
                        break;
                    default:
                        // Make sure we're not missing any string types in the test
                        if (property.Name.ToString() == "CausesOfDeath") continue; // This is a convenience method that we don't need to test
                        if (property.PropertyType.ToString().Contains("String")) Console.WriteLine($"Missing string type {property.PropertyType} for property {property.Name}");
                        Assert.DoesNotContain("String", property.PropertyType.ToString()); // Make sure we're not missing any string types
                        break;
                }
            }
            Assert.DoesNotContain("\"\"", blank.ToJson());
        }

        [Fact]
        public void TestPregnancyCodes()
        {
            DeathRecord testRecord = new DeathRecord();
            string currentStatus = testRecord.PregnancyStatusHelper;
            Assert.Null(currentStatus);
            testRecord.PregnancyStatusHelper = "3";
            Assert.Equal("3", testRecord.PregnancyStatusHelper);

            Exception ex = Assert.Throws<System.ArgumentException>(() => testRecord.PregnancyStatusHelper = "8");
            Exception ex2 = Assert.Throws<System.ArgumentException>(() => testRecord.PregnancyStatusHelper = "10");
        }

        [Fact]
        public void TestBadPartialDate()
        {
            Exception ex = Assert.Throws<System.ArgumentException>(() => new DeathRecord(File.ReadAllText(FixturePath("fixtures/json/DeathRecordBadPartialDate.json"))));
            System.Text.StringBuilder errorMsg = new System.Text.StringBuilder();
            errorMsg.Append("Missing 'Date-Month' of [http://hl7.org/fhir/us/vrdr/StructureDefinition/PartialDate] for resource [f384e3f6-2438-4e07-9df2-44e27e3aa72d].");
            errorMsg.AppendLine();
            errorMsg.Append("[http://hl7.org/fhir/us/vrdr/StructureDefinition/PartialDate] component contains extra invalid fields [http://hl7.org/fhir/us/vrdr/StructureDefinition/Date-Monh] for resource [f384e3f6-2438-4e07-9df2-44e27e3aa72d].");
            errorMsg.AppendLine();
            Assert.Equal(errorMsg.ToString(), ex.Message);
        }

        [Fact]
        public void TestBadPartialDateTime()
        {
            Exception ex = Assert.Throws<System.ArgumentException>(() => new DeathRecord(File.ReadAllText(FixturePath("fixtures/json/DeathRecordBadPartialDateTime.json"))));
            System.Text.StringBuilder errorMsg = new System.Text.StringBuilder();
            errorMsg.Append("Missing 'Date-Time' of [http://hl7.org/fhir/us/vrdr/StructureDefinition/PartialDateTime] for resource [81899bd9-0441-45f0-9b89-9d91daa08983].");
            errorMsg.AppendLine();
            errorMsg.Append("[http://hl7.org/fhir/us/vrdr/StructureDefinition/PartialDateTime] component contains extra invalid fields [http://hl7.org/fhir/us/vrdr/StructureDefinition/Invalid, http://hl7.org/fhir/us/vrdr/StructureDefinition/Date-Tme] for resource [81899bd9-0441-45f0-9b89-9d91daa08983].");
            errorMsg.AppendLine();
            Assert.Equal(errorMsg.ToString(), ex.Message);
        }

        [Fact]
        public void TestEducationLevelObs()
        {
            var testRecord = new DeathRecord();
            Composition composition = testRecord.GetComposition();

            int beforeCounts = 0;
            int afterCounts = 0;

            foreach (var s in composition.Section)
            {
                beforeCounts += s.Entry.Count;
            }

            testRecord.EducationLevelHelper = "HS";

            foreach (var s in composition.Section)
            {
                afterCounts += s.Entry.Count;
            }
            Assert.Equal(beforeCounts + 1, afterCounts);
        }

        [Fact]
        public void TestAutopsyAvailableCodes()
        {
            IJEMortality ije = new IJEMortality();
            ije.DOD_YR = "2021";
            ije.DSTATE = "MA";
            ije.FILENO = "578660";
            ije.MAN_UC = "I219";
            ije.EAC = "21I219  31I251  61E119  62F179  63I10   64E780  ";
            ije.RAC = "I219 E119 E780 F179 I10  I251 ";
            ije.AUXNO = "579927".PadRight(12, ' ');
            ije.MFILED = "0";
            ije.MANNER = "N";
            ije.trx.CS = "1";
            ije.trx.SHIP = "497";
            ije.AUTOP = "Y";
            ije.AUTOPF = "Y";
            ije.TOBAC = "Y";
            ije.PREG = "8";
            ije.CERTL = "D";
            ije.CERTL = "DDDD";
            ije.TRANSPRT = "Hover Board Rider";
            ije.INACT = "9";
            DeathRecord record = ije.ToDeathRecord();
            IJEMortality ije2 = new IJEMortality(record);
            Assert.Equal("Y", record.AutopsyResultsAvailableHelper);
            Assert.Equal("Y", ije2.AUTOPF);

            ije.AUTOPF = "N";
            record = ije.ToDeathRecord();
            ije2 = new IJEMortality(record);
            Assert.Equal("N", record.AutopsyResultsAvailableHelper);
            Assert.Equal("N", ije2.AUTOPF);

            ije.AUTOPF = "U";
            record = ije.ToDeathRecord();
            ije2 = new IJEMortality(record);
            Assert.Equal("UNK", record.AutopsyResultsAvailableHelper);
            Assert.Equal("U", ije2.AUTOPF);

            ije.AUTOPF = "X";
            record = ije.ToDeathRecord();
            ije2 = new IJEMortality(record);
            Assert.Equal("NA", record.AutopsyResultsAvailableHelper);
            Assert.Equal("X", ije2.AUTOPF);
        }

        [Fact]
        public void TestIJEParsing()
        {
            string initIJE = "2016NE0001151            0Jennifer                                          LJones                                                       0Adams                                             F05432112341047019680912USNE40780109NEUSYD04109C0107040530NNNN                    YNNNNNNNNNNNNNN                                                                                                                                                                                                                                                100                                              Tenneco                                    Factory                                    0172281968NE                20160112    A        R99  11R99                                                                                                                                                            R99                                                                                                 YYU1 010720169999NLancaster County Attorney                                                              N                              6100 W. Smith St.                                                                                                                           Raymond                     Nebraska                    68428    Lancaster                                                                                                                                                                                                                                          Raymond                     68428    Lancaster                   Nebraska                    United States               6100 W. South St.                                      10001                                                                   Loe                                               John                                                                                                Ann                                               L                                                 Doe                                               YHome                                              Decedeht was found unconscious and not breathing on the enclosed porchof the residence.                                                                                                                                                                                                                                Raymond                          NE                                    1Combined Ethanol, Cyclobenzaprine, Hydroxychloroquine And Gabapentin Toxicity                                           Unknown                                                                                                                                                                                                                                                                                                                                                                                                                                                 Diagnosed With Lupus; Depression History                                                                                                                                                                                                                                                               Lincoln                                                                                                     Son                             Nebraska                         Lincoln                     Lincoln Memorial Funeral Home                                                                                                                                                  6800 S. 14th Street                               Lincoln                       Nebraska                    68512    010720160405                                                                                                                                                                                                                                           575 South St                                      Lincoln                       Nebraska                    68508    0111201601122016Nebraska                    Nebraska                    USUnited States               280 Y01112016                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        ";
            IJEMortality ije = new IJEMortality(initIJE);
            Assert.Equal("Nebraska", ije.STATETEXT_R.Trim());
            Assert.Equal("L", ije.MNAME);
            Assert.Equal("", ije.FILEDATE); // blanks out since not used in FHIR Assert.Equal("01122026", ije.FILEDATE);
            // NOTE: below may change on subsequent reviews of IJE->FHIR mapping
            Assert.Equal("1", ije.VOID);
            Assert.Equal("0", ije.ALIAS); // zeroed out as with VOID Assert.Equal("1", ije.ALIAS);
        }

        [Fact]
        public void TestUnknownDateOfDeath()
        {
            DeathRecord dr = new DeathRecord(File.ReadAllText(FixturePath("fixtures/json/UnknownDateOfDeath.json")));
            Assert.Null(dr.DateOfDeath);
            Assert.Equal(-1, dr.DeathYear);
            Assert.Equal(-1, dr.DeathMonth);
            Assert.Equal(-1, dr.DeathDay);
            Assert.Equal("-1", dr.DeathTime);

            IJEMortality ije = new IJEMortality(dr);
            Assert.Equal("9999", ije.DOD_YR);
            Assert.Equal("99", ije.DOD_MO);
            Assert.Equal("99", ije.DOD_DY);
            Assert.Equal("9999", ije.TOD);
        }

        [Fact]
        public void TestUnknownDateOfInjury()
        {
            DeathRecord dr = new DeathRecord(File.ReadAllText(FixturePath("fixtures/json/UnknownDateOfInjury.json")));
            Assert.Null(dr.InjuryDate);
            Assert.Equal(-1, dr.InjuryYear);
            Assert.Equal(-1, dr.InjuryMonth);
            Assert.Equal(-1, dr.InjuryDay);
            Assert.Equal("-1", dr.InjuryTime);

            IJEMortality ije = new IJEMortality(dr);
            Assert.Equal("9999", ije.DOI_YR);
            Assert.Equal("99", ije.DOI_MO);
            Assert.Equal("99", ije.DOI_DY);
            Assert.Equal("9999", ije.TOI_HR);
        }

        [Fact]
        public void TestTempUnknownDateOfDeathAndDateOfInjury()
        {
            DeathRecord dr = new DeathRecord(File.ReadAllText(FixturePath("fixtures/json/TempUnknownDateOfDeath.json")));
            Assert.Null(dr.DateOfDeath);
            Assert.Null(dr.DeathYear);
            Assert.Null(dr.DeathMonth);
            Assert.Null(dr.DeathDay);
            Assert.Null(dr.DeathTime);

            Assert.Null(dr.InjuryDate);
            Assert.Null(dr.InjuryYear);
            Assert.Null(dr.InjuryMonth);
            Assert.Null(dr.InjuryDay);
            Assert.Null(dr.InjuryTime);

            IJEMortality ije = new IJEMortality(dr);
            Assert.Equal("    ", ije.DOD_YR);
            Assert.Equal("  ", ije.DOD_MO);
            Assert.Equal("  ", ije.DOD_DY);
            Assert.Equal("    ", ije.TOD);

            Assert.Equal("    ", ije.DOI_YR);
            Assert.Equal("  ", ije.DOI_MO);
            Assert.Equal("  ", ije.DOI_DY);
            Assert.Equal("    ", ije.TOI_HR);
            Assert.Equal("    ", ije.DOD_YR);
            /* // depends on outcome of temp-unknown discussion with Saul
            Assert.Equal("99", ije.DOD_MO);
            Assert.Equal("99", ije.DOD_DY);
            Assert.Equal("9999", ije.TOD);

            Assert.Equal("9999", ije.DOI_YR);
            Assert.Equal("99", ije.DOI_MO);
            Assert.Equal("99", ije.DOI_DY);
            Assert.Equal("9999", ije.TOI_HR);
            */
        }

        [Fact]
        public void TestUknownTimeOfDeathForFHIR()
        {
            var dr = new DeathRecord();
            dr.DeathDay = -1;
            dr.DeathMonth = -1;
            dr.DeathYear = -1;
            dr.DeathTime = "-1";
            var fhir = dr.ToJson();
            Assert.Null(dr.DateOfDeath);
            Assert.Matches(@"\{\s*""url"":\s*""http://hl7.org/fhir/us/vrdr/StructureDefinition/Date-Year"",\s*""_valueUnsignedInt"":\s*\{\s*""extension"":\s*\[\s*\{\s*""url"":\s*""http://hl7.org/fhir/StructureDefinition/data-absent-reason"",\s*""valueCode"":\s*""unknown""\s*\}\s*\]\s*\}\s*\}", fhir);
            Assert.Matches(@"\{\s*""url"":\s*""http://hl7.org/fhir/us/vrdr/StructureDefinition/Date-Month"",\s*""_valueUnsignedInt"":\s*\{\s*""extension"":\s*\[\s*\{\s*""url"":\s*""http://hl7.org/fhir/StructureDefinition/data-absent-reason"",\s*""valueCode"":\s*""unknown""\s*\}\s*\]\s*\}\s*\}", fhir);
            Assert.Matches(@"\{\s*""url"":\s*""http://hl7.org/fhir/us/vrdr/StructureDefinition/Date-Day"",\s*""_valueUnsignedInt"":\s*\{\s*""extension"":\s*\[\s*\{\s*""url"":\s*""http://hl7.org/fhir/StructureDefinition/data-absent-reason"",\s*""valueCode"":\s*""unknown""\s*\}\s*\]\s*\}\s*\}", fhir);
            Assert.Matches(@"\{\s*""url"":\s*""http://hl7.org/fhir/us/vrdr/StructureDefinition/Date-Time"",\s*""_valueTime"":\s*\{\s*""extension"":\s*\[\s*\{\s*""url"":\s*""http://hl7.org/fhir/StructureDefinition/data-absent-reason"",\s*""valueCode"":\s*""unknown""\s*\}\s*\]\s*\}\s*\}", fhir);
        }

        [Fact]
        public void TestUknownTimeOfInjuryForFHIR()
        {
            var dr = new DeathRecord();
            dr.InjuryDay = -1;
            dr.InjuryMonth = -1;
            dr.InjuryYear = -1;
            dr.InjuryTime = "-1";
            var fhir = dr.ToJson();
            Assert.Null(dr.InjuryDate);
            Assert.Matches(@"\{\s*""url"":\s*""http://hl7.org/fhir/us/vrdr/StructureDefinition/Date-Year"",\s*""_valueUnsignedInt"":\s*\{\s*""extension"":\s*\[\s*\{\s*""url"":\s*""http://hl7.org/fhir/StructureDefinition/data-absent-reason"",\s*""valueCode"":\s*""unknown""\s*\}\s*\]\s*\}\s*\}", fhir);
            Assert.Matches(@"\{\s*""url"":\s*""http://hl7.org/fhir/us/vrdr/StructureDefinition/Date-Month"",\s*""_valueUnsignedInt"":\s*\{\s*""extension"":\s*\[\s*\{\s*""url"":\s*""http://hl7.org/fhir/StructureDefinition/data-absent-reason"",\s*""valueCode"":\s*""unknown""\s*\}\s*\]\s*\}\s*\}", fhir);
            Assert.Matches(@"\{\s*""url"":\s*""http://hl7.org/fhir/us/vrdr/StructureDefinition/Date-Day"",\s*""_valueUnsignedInt"":\s*\{\s*""extension"":\s*\[\s*\{\s*""url"":\s*""http://hl7.org/fhir/StructureDefinition/data-absent-reason"",\s*""valueCode"":\s*""unknown""\s*\}\s*\]\s*\}\s*\}", fhir);
            Assert.Matches(@"\{\s*""url"":\s*""http://hl7.org/fhir/us/vrdr/StructureDefinition/Date-Time"",\s*""_valueTime"":\s*\{\s*""extension"":\s*\[\s*\{\s*""url"":\s*""http://hl7.org/fhir/StructureDefinition/data-absent-reason"",\s*""valueCode"":\s*""unknown""\s*\}\s*\]\s*\}\s*\}", fhir);
        }

        [Fact]
        public void testMissingFamilyNameInFHIR()
        {
            // test setter
            IJEMortality ije = new IJEMortality();
            ije.LNAME = "UNKNOWN";
            DeathRecord record = ije.ToDeathRecord();
            Assert.Equal("UNKNOWN", record.FamilyName);
            IJEMortality ije1 = new IJEMortality();
            ije1.LNAME = "Smith";
            DeathRecord record1 = ije1.ToDeathRecord();
            Assert.Equal("Smith", record1.FamilyName);

            // test getter
            Assert.Equal("UNKNOWN", ije.LNAME.Trim());
            Assert.Equal("Smith", ije1.LNAME.Trim());

            // test roundtrip from ije, with first half implemented above
            ije = new IJEMortality(record, false);
            ije1 = new IJEMortality(record1, false);
            Assert.Equal("UNKNOWN", ije.LNAME.Trim());
            Assert.Equal("Smith", ije1.LNAME.Trim());

            // test roundtrip from FHIR/record, with first half implemented above
            record.FamilyName = "Williams";
            ije = new IJEMortality(record, false);
            Assert.Equal("Williams", ije.LNAME.Trim());

            // test roundtrip from FHIR/record for special case FamilyName = "UNKNOWN"
            record.FamilyName = "UNKNOWN";
            ije = new IJEMortality(record, false);
            record = ije.ToDeathRecord();
            Assert.Equal("UNKNOWN", record.FamilyName);
        }

        private string FixturePath(string filePath)
        {
            if (Path.IsPathRooted(filePath))
            {
                return filePath;
            }
            else
            {
                return Path.GetRelativePath(Directory.GetCurrentDirectory(), filePath);
            }
        }
    }
}
