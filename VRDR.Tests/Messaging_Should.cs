using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using Xunit;
using Hl7.Fhir.Model;

namespace VRDR.Tests
{
    public class Messaging_Should
    {
        private ArrayList XMLRecords;

        private ArrayList JSONRecords;

        public Messaging_Should()
        {
            XMLRecords = new ArrayList();
            XMLRecords.Add(new DeathRecord(File.ReadAllText(FixturePath("fixtures/xml/DeathRecord1.xml"))));
            JSONRecords = new ArrayList();
            JSONRecords.Add(new DeathRecord(File.ReadAllText(FixturePath("fixtures/json/DeathRecord1.json"))));
            JSONRecords.Add(new DeathRecord(File.ReadAllText(FixturePath("fixtures/json/DeathRecordNoIdentifiers.json"))));
        }

        [Fact]
        public void CreateSubmission()
        {
            DeathRecordSubmission submission = new DeathRecordSubmission();
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission", submission.MessageType);
            Assert.Null(submission.DeathRecord);
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission", submission.MessageDestination);
            Assert.NotNull(submission.MessageTimestamp);
            Assert.Null(submission.MessageSource);
            Assert.NotNull(submission.MessageId);
            Assert.Null(submission.cert_no);
            Assert.Null(submission.state_auxiliary_id);
            Assert.Null(submission.NCHSIdentifier);
        }

        [Fact]
        public void CreateSubmissionFromDeathRecord()
        {
            DeathRecord record = (DeathRecord)XMLRecords[0];
            DeathRecordSubmission submission = new DeathRecordSubmission(record);
            Assert.NotNull(submission.DeathRecord);
            Assert.Equal("2018-02-20T16:48:06-05:00", submission.DeathRecord.DateOfDeathPronouncement);
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission", submission.MessageType);
            Assert.Equal(10,10);
            Assert.Equal((uint)1, submission.cert_no);
            Assert.Equal((uint)2018, submission.death_year);
            Assert.Equal("42", submission.state_auxiliary_id);
            Assert.Equal("2018MA000001", submission.NCHSIdentifier);

            record = (DeathRecord)JSONRecords[0];
            submission = new DeathRecordSubmission(record);
            Assert.NotNull(submission.DeathRecord);
            Assert.Equal("2018-02-20T16:48:06-05:00", submission.DeathRecord.DateOfDeathPronouncement);
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission", submission.MessageType);
            Assert.Equal((uint)1, submission.cert_no);
            Assert.Equal((uint)2018, submission.death_year);
            Assert.Equal("42", submission.state_auxiliary_id);
            Assert.Equal("2018MA000001", submission.NCHSIdentifier);

            record = null;
            submission = new DeathRecordSubmission(record);
            Assert.Null(submission.DeathRecord);
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission", submission.MessageType);
            Assert.Null(submission.cert_no);
            Assert.Null(submission.state_auxiliary_id);
            Assert.Null(submission.NCHSIdentifier);

            record = (DeathRecord)JSONRecords[1];
            submission = new DeathRecordSubmission(record);
            Assert.NotNull(submission.DeathRecord);
            Assert.Equal("2018-02-20T16:48:06-05:00", submission.DeathRecord.DateOfDeathPronouncement);
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission", submission.MessageType);
            Assert.Null(submission.cert_no);
            Assert.Null(submission.state_auxiliary_id);
            Assert.Null(submission.NCHSIdentifier);
        }

        [Fact]
        public void CreateSubmissionFromJSON()
        {
            DeathRecordSubmission submission = BaseMessage.Parse<DeathRecordSubmission>(FixtureStream("fixtures/json/DeathRecordSubmission.json"));
            Assert.Equal("2018-02-20T16:48:06-05:00", submission.DeathRecord.DateOfDeathPronouncement);
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission", submission.MessageType);
            Assert.Equal("2018MA000001", submission.NCHSIdentifier);
            Assert.Equal((uint)1, submission.cert_no);
            Assert.Equal((uint)2018, submission.death_year);
            Assert.Equal("42", submission.state_auxiliary_id);
            Assert.Equal(submission.jurisdiction_id, submission.DeathRecord.DeathLocationJurisdiction);

            submission = BaseMessage.Parse<DeathRecordSubmission>(FixtureStream("fixtures/json/DeathRecordSubmissionNoIdentifiers.json"));
            Assert.Equal("2018-02-20T16:48:06-05:00", submission.DeathRecord.DateOfDeathPronouncement);
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission", submission.MessageType);
            Assert.Null(submission.cert_no);
            Assert.Null(submission.state_auxiliary_id);
            Assert.Null(submission.NCHSIdentifier);

            MessageParseException ex = Assert.Throws<MessageParseException>(() => BaseMessage.Parse(FixtureStream("fixtures/json/EmptySubmission.json")));
            Assert.Equal("Error processing DeathRecord entry in the message: Failed to find a Bundle Entry containing a Resource of type Bundle", ex.Message);
            ExtractionErrorMessage errMsg = ex.CreateExtractionErrorMessage();
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission", errMsg.MessageSource);
            Assert.Equal("nightingale", errMsg.MessageDestination);
            Assert.Equal("a9d66d2e-2480-4e8d-bab3-4e4c761da1b7", errMsg.FailedMessageId);
            Assert.Equal("2018MA000001", errMsg.NCHSIdentifier);
            Assert.Equal((uint)1, errMsg.cert_no);
            Assert.Equal((uint)2018, errMsg.death_year);
            Assert.Equal("42", errMsg.state_auxiliary_id);

            ex = Assert.Throws<MessageParseException>(() => BaseMessage.Parse<AckMessage>(FixtureStream("fixtures/json/DeathRecordSubmission.json")));
            Assert.Equal("The supplied message was of type VRDR.DeathRecordSubmission, expected VRDR.AckMessage or a subclass", ex.Message);
        }

        [Fact]
        public void CreateSubmissionFromBundle()
        {
            DeathRecordSubmission submission = new DeathRecordSubmission();
            submission.DeathRecord = new DeathRecord();
            submission.cert_no = 42;
            submission.state_auxiliary_id = "identifier";
            Bundle submissionBundle = (Bundle)submission;

            DeathRecordSubmission parsed = BaseMessage.Parse<DeathRecordSubmission>(submissionBundle);
            Assert.Equal(submission.DeathRecord.ToJson(), parsed.DeathRecord.ToJson());
            Assert.Equal(submission.MessageType, parsed.MessageType);
            Assert.Equal(submission.cert_no, parsed.cert_no);
            Assert.Equal(submission.state_auxiliary_id, parsed.state_auxiliary_id);
            Assert.Equal(submission.NCHSIdentifier, parsed.NCHSIdentifier);
        }

        [Fact]
        public void CreateUpdate()
        {
            DeathRecordUpdate submission = new DeathRecordUpdate();
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission_update", submission.MessageType);
            Assert.Null(submission.DeathRecord);
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission", submission.MessageDestination);
            Assert.NotNull(submission.MessageTimestamp);
            Assert.Null(submission.MessageSource);
            Assert.NotNull(submission.MessageId);
            Assert.Null(submission.cert_no);
            Assert.Null(submission.state_auxiliary_id);
            Assert.Null(submission.NCHSIdentifier);
        }

        [Fact]
        public void CreateUpdateFromDeathRecord()
        {
            DeathRecordUpdate update = new DeathRecordUpdate((DeathRecord)XMLRecords[0]);
            Assert.NotNull(update.DeathRecord);
            Assert.Equal("2018-02-20T16:48:06-05:00", update.DeathRecord.DateOfDeathPronouncement);
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission_update", update.MessageType);
            Assert.Equal((uint)1, update.cert_no);
            Assert.Equal((uint)2018, update.DeathYear);
            Assert.Equal("42", update.state_auxiliary_id);
            Assert.Equal("2018MA000001", update.NCHSIdentifier);

            update = new DeathRecordUpdate((DeathRecord)JSONRecords[1]); // no ids in this death record (except jurisdiction id which is required)
            Assert.NotNull(update.DeathRecord);
            Assert.Equal("2018-02-20T16:48:06-05:00", update.DeathRecord.DateOfDeathPronouncement);
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission_update", update.MessageType);
            Assert.Null(update.cert_no);
            Assert.Null(update.state_auxiliary_id);
            Assert.Null(update.NCHSIdentifier);
        }

        [Fact]
        public void CreateUpdateFromJSON()
        {
            DeathRecordUpdate update = BaseMessage.Parse<DeathRecordUpdate>(FixtureStream("fixtures/json/DeathRecordUpdate.json"));
            Assert.Equal("2018-02-20T16:48:06-05:00", update.DeathRecord.DateOfDeathPronouncement);
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission_update", update.MessageType);
            Assert.Equal("2018MA000001", update.NCHSIdentifier);
            Assert.Equal((uint)1, update.cert_no);
            Assert.Equal((uint)2018, update.DeathYear);
            Assert.Equal("42", update.state_auxiliary_id);

            update = BaseMessage.Parse<DeathRecordUpdate>(FixtureStream("fixtures/json/DeathRecordUpdateNoIdentifiers.json"));
            Assert.Equal("2018-02-20T16:48:06-05:00", update.DeathRecord.DateOfDeathPronouncement);
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission_update", update.MessageType);
            Assert.Null(update.cert_no);
            Assert.Null(update.state_auxiliary_id);
            Assert.Null(update.NCHSIdentifier);
        }

        [Fact]
        public void CreatUpdateFromBundle()
        {
            DeathRecordUpdate update = new DeathRecordUpdate();
            update.DeathRecord = new DeathRecord();
            update.cert_no = 42;
            update.state_auxiliary_id = "identifier";
            Bundle updateBundle = (Bundle)update;

            DeathRecordUpdate parsed = BaseMessage.Parse<DeathRecordUpdate>(updateBundle);
            Assert.Equal(update.DeathRecord.ToJson(), parsed.DeathRecord.ToJson());
            Assert.Equal(update.MessageType, parsed.MessageType);
            Assert.Equal(update.cert_no, parsed.cert_no);
            Assert.Equal(update.state_auxiliary_id, parsed.state_auxiliary_id);
            Assert.Equal(update.NCHSIdentifier, parsed.NCHSIdentifier);
        }

        [Fact]
        public void CreateAckForMessage()
        {
            DeathRecordSubmission submission = BaseMessage.Parse<DeathRecordSubmission>(FixtureStream("fixtures/json/DeathRecordSubmission.json"));
            AckMessage ack = new AckMessage(submission);
            Assert.Equal("http://nchs.cdc.gov/vrdr_acknowledgement", ack.MessageType);
            Assert.Equal(submission.MessageId, ack.AckedMessageId);
            Assert.Equal(submission.MessageSource, ack.MessageDestination);
            Assert.Equal(submission.MessageDestination, ack.MessageSource);
            Assert.Equal(submission.state_auxiliary_id, ack.state_auxiliary_id);
            Assert.Equal(submission.cert_no, ack.cert_no);
            Assert.Equal(submission.NCHSIdentifier, ack.NCHSIdentifier);

            submission = null;
            ack = new AckMessage(submission);
            Assert.Equal("http://nchs.cdc.gov/vrdr_acknowledgement", ack.MessageType);
            Assert.Null(ack.AckedMessageId);
            Assert.Null(ack.MessageDestination);
            Assert.Null(ack.MessageSource);
            Assert.Null(ack.cert_no);
            Assert.Null(ack.state_auxiliary_id);
            Assert.Null(ack.NCHSIdentifier);

            submission = new DeathRecordSubmission();
            ack = new AckMessage(submission);
            Assert.Equal("http://nchs.cdc.gov/vrdr_acknowledgement", ack.MessageType);
            Assert.Equal(submission.MessageId, ack.AckedMessageId);
            Assert.Equal(submission.MessageSource, ack.MessageDestination);
            Assert.Equal(submission.MessageDestination, ack.MessageSource);
            Assert.Equal(submission.state_auxiliary_id, ack.state_auxiliary_id);
            Assert.Equal(submission.cert_no, ack.cert_no);
            Assert.Equal(submission.NCHSIdentifier, ack.NCHSIdentifier);
        }

        [Fact]
        public void CreateAckFromJSON()
        {
            AckMessage ack = BaseMessage.Parse<AckMessage>(FixtureStream("fixtures/json/AckMessage.json"));
            Assert.Equal("http://nchs.cdc.gov/vrdr_acknowledgement", ack.MessageType);
            Assert.Equal("a9d66d2e-2480-4e8d-bab3-4e4c761da1b7", ack.AckedMessageId);
            Assert.Equal("nightingale", ack.MessageDestination);
            Assert.Equal("2018MA000001", ack.NCHSIdentifier);
            Assert.Equal((uint)1, ack.cert_no);
            Assert.Equal((uint)2018, ack.DeathYear);
            Assert.Equal("42", ack.state_auxiliary_id);
        }

        [Fact]
        public void CreateAckFromXML()
        {
            AckMessage ack = BaseMessage.Parse<AckMessage>(FixtureStream("fixtures/xml/AckMessage.xml"));
            Assert.Equal("http://nchs.cdc.gov/vrdr_acknowledgement", ack.MessageType);
            Assert.Equal("a9d66d2e-2480-4e8d-bab3-4e4c761da1b7", ack.AckedMessageId);
            Assert.Equal("nightingale", ack.MessageDestination);
            Assert.Equal("2018MA000001", ack.NCHSIdentifier);
            Assert.Equal((uint)1, ack.cert_no);
            Assert.Equal((uint)2018, ack.DeathYear);
            Assert.Equal("42", ack.state_auxiliary_id);
        }

        [Fact]
        public void CreateAckFromBundle()
        {
            AckMessage ackFixture = BaseMessage.Parse<AckMessage>(FixtureStream("fixtures/json/AckMessage.json"));
            Bundle ackBundle = (Bundle)ackFixture;
            AckMessage ack = BaseMessage.Parse<AckMessage>(ackBundle);
            Assert.Equal("a9d66d2e-2480-4e8d-bab3-4e4c761da1b7", ack.AckedMessageId);
            Assert.Equal("nightingale", ack.MessageDestination);
            Assert.Equal("2018MA000001", ack.NCHSIdentifier);
            Assert.Equal((uint)1, ack.cert_no);
            Assert.Equal((uint)2018, ack.DeathYear);
            Assert.Equal("42", ack.state_auxiliary_id);
        }
[Fact]
        public void CreateCauseOfDeathCodingResponseFromJSON()
        {
            CodingResponseMessage message = BaseMessage.Parse<CodingResponseMessage>(FixtureStream("fixtures/json/Bundle-Message-MA20323-TRX-000182-Example.json"));
            Assert.Equal("http://nchs.cdc.gov/vrdr_coding", message.MessageType);
            Assert.Equal("http://mitre.org/vrdr", message.MessageDestination);
            Assert.Equal((uint)182, message.cert_no);
            Assert.Equal((uint)2020, message.death_year);
            Assert.Null(message.state_auxiliary_id);
            Assert.Equal("2020MA000182", message.NCHSIdentifier);
            Assert.Equal("O159", message.acme_underlying_cause_of_death);
            Assert.Equal("O159", message.manual_underlying_cause_of_death);
            var recordAxisCodes = message.CauseOfDeathRecordAxis;
            Assert.Equal(2, recordAxisCodes.Count);
            Assert.Equal("0159", recordAxisCodes[0]);
            Assert.Equal("I469", recordAxisCodes[1]);
            var entityAxisEntries = message.CauseOfDeathEntityAxis;
            Assert.Equal(3, (int)entityAxisEntries.Count);
            Assert.Equal((uint)1, entityAxisEntries[0].LineNumber);
            Assert.Equal((uint)1, entityAxisEntries[0].Position);
            Assert.Equal("I469", entityAxisEntries[0].Code);
            Assert.Equal((uint)1, entityAxisEntries[1].LineNumber);
            Assert.Equal((uint)2, entityAxisEntries[1].Position);
            Assert.Equal("O159", entityAxisEntries[1].Code);
            Assert.Equal((uint)6, entityAxisEntries[2].LineNumber);
            Assert.Equal((uint)1, entityAxisEntries[2].Position);
            Assert.Equal("O95", entityAxisEntries[2].Code);
            Assert.Null(entityAxisEntries[2].E_code_indicator);
            Assert.Equal("Cardiopulmonary arrest", message.COD1A);
            Assert.Equal("Eclampsia", message.COD1B);
            Assert.Null(message.COD1C);
            Assert.Equal("A",message.manner);
            Assert.Equal("0",message.injpl);
            Assert.Equal("Y",message.autop);
            Assert.Equal("Y",message.autopf);
            Assert.Equal("N",message.tobac);
            Assert.Equal("1",message.preg);
            Assert.Equal((uint)11,message.doi_mo);
            Assert.Equal((uint)2,message.doi_dy);
            Assert.Equal((uint)2019,message.doi_yr);
            Assert.Equal("M",message.toi_unit);
            Assert.Equal((uint)1300,message.toi_hr);
            Assert.Equal("N",message.workinj);
            Assert.Equal("M",message.certl);
        }

[Fact]
public void CreateDemographicCodingResponseFromJSON()
        {
            CodingResponseMessage message = BaseMessage.Parse<CodingResponseMessage>(FixtureStream("fixtures/json/Bundle-Message-MA20323-MRE-000182-Example.json"));
            Assert.Equal("http://nchs.cdc.gov/vrdr_coding", message.MessageType);
            Assert.Equal("http://mitre.org/vrdr", message.MessageDestination);
            Assert.Equal((uint)182, message.cert_no);
            Assert.Equal((uint)2020, message.DeathYear);
            Assert.Null(message.state_auxiliary_id);
            Assert.Equal("2020MA000182", message.NCHSIdentifier);
            Assert.Equal("100", message.RACE1E["code"] );
            Assert.Equal("http://cdc.gov/nchs/nvss/fhir/vital-records-messaging/CodeSystem/VRDR-RaceCodeList-cs", message.RACE1E["system"] );
            Assert.Equal("300", message.RACE2E["code"] );
            Assert.Equal("http://cdc.gov/nchs/nvss/fhir/vital-records-messaging/CodeSystem/VRDR-RaceCodeList-cs", message.RACE2E["system"] );
            Assert.Null(message.RACE3E);
            Assert.Equal("999", message.DETHNICE["code"] );
            Assert.Equal("http://cdc.gov/nchs/nvss/fhir/vital-records-messaging/CodeSystem/VRDR-HispanicOrigin-cs", message.DETHNICE["system"] );
            Assert.Null( message.DETHNIC5C );
            Assert.Equal("Y", message.RACE1);
            Assert.Equal("Fort Sill Apache", message.RACE16);
            Assert.Equal("N", message.DETHNIC1);
            Assert.Equal("Y", message.RACE1);
            Assert.Equal("N", message.RACE2);
            Assert.Equal("Fort Sill Apache", message.RACE16);
            Assert.Equal("N", message.DETHNIC1);
            Assert.Equal("N", message.DETHNIC2);
        }
        [Fact]
        public void CreateCodingUpdateFromJSON()
        {
            CodingUpdateMessage message = BaseMessage.Parse<CodingUpdateMessage>(FixtureStream("fixtures/json/CodingUpdateMessage.json"));
            Assert.Equal("http://nchs.cdc.gov/vrdr_coding_update", message.MessageType);
            Assert.Equal("destination", message.MessageDestination);
            Assert.Equal((uint)1, message.cert_no);
            Assert.Equal((uint)2018, message.DeathYear);
            Assert.Equal("42", message.state_auxiliary_id);
            Assert.Equal("2018MA000001", message.NCHSIdentifier);
            var ethnicity = message.Ethnicity;
            Assert.Equal(2, ethnicity.Count);
            Assert.True(ethnicity.ContainsKey(CodingResponseMessage.HispanicOrigin.DETHNICE));
            Assert.Equal("123", ethnicity.GetValueOrDefault(CodingResponseMessage.HispanicOrigin.DETHNICE, "foo"));
            Assert.True(ethnicity.ContainsKey(CodingResponseMessage.HispanicOrigin.DETHNIC5C));
            Assert.Equal("456", ethnicity.GetValueOrDefault(CodingResponseMessage.HispanicOrigin.DETHNIC5C, "foo"));
            var race = message.Race;
            Assert.Equal(3, race.Count);
            Assert.True(race.ContainsKey(CodingResponseMessage.RaceCode.RACE1E));
            Assert.Equal("foo", race.GetValueOrDefault(CodingResponseMessage.RaceCode.RACE1E,"yyz"));
            Assert.True(race.ContainsKey(CodingResponseMessage.RaceCode.RACE17C));
            Assert.Equal("bar", race.GetValueOrDefault(CodingResponseMessage.RaceCode.RACE17C,"yyz"));
            Assert.True(race.ContainsKey(CodingResponseMessage.RaceCode.RACEBRG));
            Assert.Equal("baz", race.GetValueOrDefault(CodingResponseMessage.RaceCode.RACEBRG,"yyz"));
            Assert.Equal("A04.7", message.UnderlyingCauseOfDeath);
            var recordAxisCodes = message.CauseOfDeathRecordAxis;
            Assert.Equal(4, recordAxisCodes.Count);
            Assert.Equal("A04.7", recordAxisCodes[0]);
            Assert.Equal("A41.9", recordAxisCodes[1]);
            Assert.Equal("J18.9", recordAxisCodes[2]);
            Assert.Equal("J96.0", recordAxisCodes[3]);
            var entityAxisEntries = message.CauseOfDeathEntityAxis;

            // Assert.Equal(2, (int)entityAxisEntries.Count);
            // Assert.Equal("1", entityAxisEntries[0].LineNumber);
            // Assert.Equal(2, (int)entityAxisEntries[0].AssignedCodes.Count);
            // Assert.Equal("code1_1", entityAxisEntries[0].AssignedCodes[0]);
            // Assert.Equal("code1_2", entityAxisEntries[0].AssignedCodes[1]);
            // Assert.Equal("2", entityAxisEntries[1].LineNumber);
            // Assert.Equal(1, (int)entityAxisEntries[1].AssignedCodes.Count);
            // Assert.Equal("code2_1", entityAxisEntries[1].AssignedCodes[0]);
        }

        [Fact]
        public void CreateCodingResponse()
        {
            CodingResponseMessage message = new CodingResponseMessage("destination", "http://nchs.cdc.gov/vrdr_submission");
            Assert.Equal("http://nchs.cdc.gov/vrdr_coding", message.MessageType);
            Assert.Equal("destination", message.MessageDestination);

            Assert.Null(message.cert_no);
            message.cert_no = 10;
            Assert.Equal((uint)10, message.cert_no);

            Assert.Null(message.state_auxiliary_id);
            message.state_auxiliary_id = "id101010";
            Assert.Equal("id101010", message.state_auxiliary_id);

            Assert.Null(message.death_year);
            message.death_year = 2019;
            Assert.Equal((uint)2019, message.death_year);

            Assert.Null(message.rec_mo);
            message.rec_mo = 1;
            Assert.Equal((uint)1, message.rec_mo);
            message.rec_mo = null;
            Assert.Null(message.rec_mo);

            Assert.Null(message.rec_dy);
            message.rec_dy = 8;
            Assert.Equal((uint)8, message.rec_dy);

            Assert.Null(message.rec_yr);
            message.rec_yr = (uint)2021;
            Assert.Equal((uint)2021, message.rec_yr);

            Assert.Null(message.cs);
            message.cs = 8;
            Assert.Equal((uint)8, message.cs);

            Assert.Null(message.ship);
            message.ship = "B202101";
            Assert.Equal("B202101", message.ship);

            Assert.Null(message.sys_rej);
            message.sys_rej = 0;
            Assert.Equal((uint)0, message.sys_rej);

            Assert.Null(message.jurisdiction_id);
            message.jurisdiction_id = "NH";
            Assert.Equal("NH", message.jurisdiction_id);
            Assert.Equal("2019NH000010", message.NCHSIdentifier);

            Assert.Null(message.int_rej);
            message.int_rej = 5;
            Assert.Equal((uint)5, message.int_rej);

            Assert.Null(message.DETHNICE); // this tests a coding value when the group doesn't exist
            Assert.Null(message.DETHNIC5C);
            Dictionary<string, string> ethnic_dict  = new Dictionary<string, string>();
            ethnic_dict["code"] = "456";
            ethnic_dict["system"] = CodeSystems.HispanicOriginCodes;
            ethnic_dict["display"] = "Some Ethnicity";
            message.DETHNIC5C = ethnic_dict;
            ethnic_dict["code"] = "123";
            ethnic_dict["system"] = CodeSystems.HispanicOriginCodes;
            ethnic_dict["display"] = "Some Ethnicity";
            message.DETHNICE = ethnic_dict;
            Assert.Equal("123", message.DETHNICE["code"]);
            Assert.Equal(CodeSystems.HispanicOriginCodes, message.DETHNICE["system"]);
            Dictionary<string, string> race_dict  = new Dictionary<string, string>();
            race_dict["code"] = "300";
            race_dict["system"] = CodeSystems.RaceCodes;
            race_dict["display"] = "Some Race";
            Assert.Null(message.RACE1E);
            message.RACE1E = race_dict;
            Assert.Equal("300", message.RACE1E["code"]);
            Assert.Equal(CodeSystems.RaceCodes, message.RACE1E["system"]);

            Assert.Null(message.manual_underlying_cause_of_death);
            message.manual_underlying_cause_of_death = "A047";
            Assert.Equal("A047", message.manual_underlying_cause_of_death);

            Assert.Empty(message.CauseOfDeathRecordAxis);
            var recordAxisCodes = new List<string>();
            recordAxisCodes.Add("A047");
            recordAxisCodes.Add("A419");
            recordAxisCodes.Add("J189");
            recordAxisCodes.Add("J960");
            message.CauseOfDeathRecordAxis = recordAxisCodes;
            recordAxisCodes = message.CauseOfDeathRecordAxis;
            Assert.Equal(4, recordAxisCodes.Count);
            Assert.Equal("A047", recordAxisCodes[0]);
            Assert.Equal("A419", recordAxisCodes[1]);
            Assert.Equal("J189", recordAxisCodes[2]);
            Assert.Equal("J960", recordAxisCodes[3]);
            List<CauseOfDeathEntityAxisEntry> entityAxisEntries = new List<CauseOfDeathEntityAxisEntry>();
            entityAxisEntries.Add ( new CauseOfDeathEntityAxisEntry(1,1,"code1_1"));
            entityAxisEntries.Add ( new CauseOfDeathEntityAxisEntry(1,2,"code1_2"));
            entityAxisEntries.Add ( new CauseOfDeathEntityAxisEntry(2,1,"code2_1"));
            entityAxisEntries.Add ( new CauseOfDeathEntityAxisEntry(2,2,"code2_2"));
            entityAxisEntries.Add ( new CauseOfDeathEntityAxisEntry(3,1,"code3_1","&"));
            Assert.Empty(message.CauseOfDeathEntityAxis);
            message.CauseOfDeathEntityAxis = entityAxisEntries;
            entityAxisEntries = message.CauseOfDeathEntityAxis;
            Assert.Equal(5, (int)entityAxisEntries.Count);
            Assert.Equal((uint)1, entityAxisEntries[0].LineNumber);
            Assert.Equal((uint)1, entityAxisEntries[0].Position);
            Assert.Equal("code1_1", entityAxisEntries[0].Code);
            Assert.Equal("code1_2", entityAxisEntries[1].Code);
            Assert.Equal((uint)2, entityAxisEntries[3].LineNumber);
            Assert.Equal((uint)2, entityAxisEntries[3].Position);
            Assert.Equal("code2_2", entityAxisEntries[3].Code);
            Assert.Null(entityAxisEntries[3].E_code_indicator);
            Assert.Equal("&",entityAxisEntries[4].E_code_indicator);

            // 'Regurgigated Values' in input_causes_of_death group
            Assert.Null(message.COD1A);  // this tests a string when the group doesn't exist
            Assert.Null(message.INTERVAL1A);
            Assert.Null(message.OTHERCONDITION);
            Assert.Null(message.manner);
            Assert.Null(message.injpl);
            Assert.Null(message.autop);
            Assert.Null(message.doi_mo); // this tests an unsigned when the group doesn't exist
            message.COD1A = "Hangnail";
            Assert.Equal("Hangnail", message.COD1A);
            message.INTERVAL1A = "3 months";
            Assert.Equal("3 months", message.INTERVAL1A);
            message.OTHERCONDITION = "COVID19";
            // Regurgitated Values' in input_misc_fields group
            Assert.Null(message.manner);
            message.manner = "A";
            Assert.Equal("A", message.manner);
            Assert.Null(message.injpl);
            message.injpl = "3";
            Assert.Equal("3", message.injpl);
            message.autop = "Y";
            Assert.Equal("Y", message.autop);
            message.autopf = "Y";
            Assert.Equal("Y", message.autopf);
            message.tobac = "Y";
            Assert.Equal("Y", message.tobac);
            message.preg = "Y";
            Assert.Equal("Y", message.preg);
            message.doi_mo = 12;
            Assert.Equal((uint)12, message.doi_mo);
            message.doi_dy = 13;
            Assert.Equal((uint)13, message.doi_dy);
            message.doi_yr = 2019;
            Assert.Equal((uint)2019, message.doi_yr);
            message.toi_hr = 1200;
            Assert.Equal((uint)1200, message.toi_hr);
            message.toi_unit = "M";
            Assert.Equal("M", message.toi_unit);
            message.workinj = "Y";
            Assert.Equal("Y", message.workinj);
            message.certl = "M";
            Assert.Equal("M", message.certl);
            message.inact = 7;
            Assert.Equal((uint)7, message.inact);
            message.auxno2 = 123456;
            Assert.Equal((uint)123456, message.auxno2);
            message.statesp = "State specific stuff";
            Assert.Equal("State specific stuff", message.statesp);
            message.sur_mo = 12;
            Assert.Equal((uint)12, message.sur_mo);
            message.sur_dy = 13;
            Assert.Equal((uint)13, message.sur_dy);
            message.sur_yr = 2019;
            Assert.Equal((uint)2019, message.sur_yr);

        }
[Fact]
        public void CreateCodingResponseNew()
        {
            CodingResponseMessage message = new CodingResponseMessage("destination", "http://nchs.cdc.gov/vrdr_submission");
            Assert.Equal("http://nchs.cdc.gov/vrdr_coding", message.MessageType);
            Assert.Equal("destination", message.MessageDestination);

            Assert.Null(message.cert_no);
            message.cert_no = 10;
            Assert.Equal((uint)10, message.cert_no);

            Assert.Null(message.state_auxiliary_id);
            message.state_auxiliary_id = "id101010";
            Assert.Equal("id101010", message.state_auxiliary_id);

            Assert.Null(message.DeathYear);
            message.DeathYear = 2019;
            Assert.Equal((uint)2019, message.DeathYear);

            Assert.Null(message.NCHSReceiptMonthString);
            message.NCHSReceiptMonthString = "1";
            Assert.Equal("01", message.NCHSReceiptMonthString);
            Assert.Equal((uint)1, message.NCHSReceiptMonth);
            message.NCHSReceiptMonthString = null;
            Assert.Null(message.NCHSReceiptMonthString);

            Assert.Null(message.NCHSReceiptMonth);
            message.NCHSReceiptMonth = (uint)1;
            Assert.Equal((uint)1, message.NCHSReceiptMonth);

            Assert.Null(message.NCHSReceiptDayString);
            message.NCHSReceiptDayString = "9";
            Assert.Equal("09", message.NCHSReceiptDayString);
            Assert.Equal((uint)9, message.NCHSReceiptDay);
            message.NCHSReceiptDayString = null;

            Assert.Null(message.NCHSReceiptDay);
            message.NCHSReceiptDay = (uint)8;
            Assert.Equal((uint)8, message.NCHSReceiptDay);

            Assert.Null(message.NCHSReceiptYearString);
            message.NCHSReceiptYearString = "2020";
            Assert.Equal("2020", message.NCHSReceiptYearString);
            Assert.Equal((uint)2020, message.NCHSReceiptYear);
            message.NCHSReceiptYearString = null;
            Assert.Null(message.NCHSReceiptYearString);

            Assert.Null(message.NCHSReceiptYear);
            message.NCHSReceiptYear = (uint)2021;
            Assert.Equal((uint)2021, message.NCHSReceiptYear);

            Assert.Null(message.MannerOfDeath);
            message.MannerOfDeath = CodingResponseMessage.MannerOfDeathEnum.Accident;
            Assert.Equal(CodingResponseMessage.MannerOfDeathEnum.Accident, message.MannerOfDeath);

            Assert.Null(message.CoderStatus);
            message.CoderStatus = "8";
            Assert.Equal("8", message.CoderStatus);

            Assert.Null(message.ShipmentNumber);
            message.ShipmentNumber = "B202101";
            Assert.Equal("B202101", message.ShipmentNumber);

            Assert.Null(message.ACMESystemRejectCodes);
            message.ACMESystemRejectCodes = CodingResponseMessage.ACMESystemRejectEnum.ACMEReject;
            Assert.Equal(CodingResponseMessage.ACMESystemRejectEnum.ACMEReject, message.ACMESystemRejectCodes);

            Assert.Null(message.injpl);
            message.injpl = "Back Porch";
            Assert.Equal("Back Porch", message.injpl);

            Assert.Null(message.OtherSpecifiedPlace);
            message.OtherSpecifiedPlace = "Unique Location";
            Assert.Equal("Unique Location", message.OtherSpecifiedPlace);

            Assert.Null(message.DeathJurisdictionID);
            message.DeathJurisdictionID = "NH";
            Assert.Equal("NH", message.DeathJurisdictionID);
            Assert.Equal("2019NH000010", message.NCHSIdentifier);

            // Assert.Null(message.IntentionalReject);
            // message.IntentionalReject = "5";
            // Assert.Equal("5", message.IntentionalReject);

            Assert.Null(message.int_rej);
            message.int_rej = 5;
            Assert.Equal(5, (int)message.int_rej);

            Assert.Null(message.DETHNIC5C);

            Dictionary<string, string> ethnic_dict = new Dictionary<string, string>();
            ethnic_dict["code"] = "456";
            ethnic_dict["system"] = CodeSystems.SCT;
            ethnic_dict["display"] = "Some Ethnicity";
            message.DETHNIC5C = ethnic_dict;
            Assert.Equal("456",message.DETHNIC5C["code"]);
            Assert.Equal(CodeSystems.SCT,message.DETHNIC5C["system"]);
            Assert.Equal("Some Ethnicity",message.DETHNIC5C["display"]);
            message.manner = "Kicked the bucket";
            Assert.Equal("Kicked the bucket", message.manner);





            Assert.Empty(message.Race);
            var race = new Dictionary<CodingResponseMessage.RaceCode, string>();
            race.Add(CodingResponseMessage.RaceCode.RACE1E, "foo");
            race.Add(CodingResponseMessage.RaceCode.RACE17C, "bar");
            race.Add(CodingResponseMessage.RaceCode.RACEBRG, "baz");
            message.Race = race;
            race = message.Race;
            Assert.Equal(3, race.Count);
            Assert.True(race.ContainsKey(CodingResponseMessage.RaceCode.RACE1E));
            Assert.Equal("foo", race.GetValueOrDefault(CodingResponseMessage.RaceCode.RACE1E,"yyz"));
            Assert.True(race.ContainsKey(CodingResponseMessage.RaceCode.RACE17C));
            Assert.Equal("bar", race.GetValueOrDefault(CodingResponseMessage.RaceCode.RACE17C,"yyz"));
            Assert.True(race.ContainsKey(CodingResponseMessage.RaceCode.RACEBRG));
            Assert.Equal("baz", race.GetValueOrDefault(CodingResponseMessage.RaceCode.RACEBRG,"yyz"));

            Assert.Null(message.UnderlyingCauseOfDeath);
            message.UnderlyingCauseOfDeath = "A04.7";
            Assert.Equal("A04.7", message.UnderlyingCauseOfDeath);

            Assert.Empty(message.CauseOfDeathRecordAxis);
            var recordAxisCodes = new List<string>();
            recordAxisCodes.Add("A04.7");
            recordAxisCodes.Add("A41.9");
            recordAxisCodes.Add("J18.9");
            recordAxisCodes.Add("J96.0");
            message.CauseOfDeathRecordAxis = recordAxisCodes;
            recordAxisCodes = message.CauseOfDeathRecordAxis;
            Assert.Equal(4, recordAxisCodes.Count);
            Assert.Equal("A04.7", recordAxisCodes[0]);
            Assert.Equal("A41.9", recordAxisCodes[1]);
            Assert.Equal("J18.9", recordAxisCodes[2]);
            Assert.Equal("J96.0", recordAxisCodes[3]);

            Assert.Empty(message.CauseOfDeathEntityAxis);
            // var entityAxisEntries = new List<CauseOfDeathEntityAxisEntry>();
            // var entry1 = new CauseOfDeathEntityAxisEntry("1");
            // entry1.AssignedCodes.Add("code1_1");
            // entry1.AssignedCodes.Add("code1_2");
            // entityAxisEntries.Add(entry1);
            // var entry2 = new CauseOfDeathEntityAxisEntry("2");
            // entry2.AssignedCodes.Add("code2_1");
            // entityAxisEntries.Add(entry2);
            // message.CauseOfDeathEntityAxis = entityAxisEntries;
            // entityAxisEntries = message.CauseOfDeathEntityAxis;
            // Assert.Equal(2, (int)entityAxisEntries.Count);
            // Assert.Equal("1", entityAxisEntries[0].LineNumber);
            // Assert.Equal(2, (int)entityAxisEntries[0].AssignedCodes.Count);
            // Assert.Equal("code1_1", entityAxisEntries[0].AssignedCodes[0]);
            // Assert.Equal("code1_2", entityAxisEntries[0].AssignedCodes[1]);
            // Assert.Equal("2", entityAxisEntries[1].LineNumber);
            // Assert.Equal(1, (int)entityAxisEntries[1].AssignedCodes.Count);
            // Assert.Equal("code2_1", entityAxisEntries[1].AssignedCodes[0]);
            // var entityAxisEntryList = message.CauseOfDeathEntityAxisList;
            // Assert.Equal(3, (int)entityAxisEntryList.Count);
            // (string line, string position, string code) = entityAxisEntryList[0];
            // Assert.Equal("1", line);
            // Assert.Equal("1", position);
            // Assert.Equal("code1_1", code);
            // (line, position, code) = entityAxisEntryList[1];
            // Assert.Equal("1", line);
            // Assert.Equal("2", position);
            // Assert.Equal("code1_2", code);
            // (line, position, code) = entityAxisEntryList[2];
            // Assert.Equal("2", line);
            // Assert.Equal("1", position);
            // Assert.Equal("code2_1", code);
        }
        [Theory]
        [InlineData("2021", 2021)]
        [InlineData("2022", 2021)]
        [InlineData(null, 2021)]
        public void SuccessfullySetNCHSReceiptYear(string receiptYear, uint deathYear)
        {
            CodingResponseMessage message = new CodingResponseMessage("destination", "http://nchs.cdc.gov/vrdr_submission");
            message.DeathYear = deathYear;
            message.NCHSReceiptYearString = receiptYear;
            Assert.Equal(receiptYear, message.NCHSReceiptYearString);
        }

        [Theory]
        [InlineData("2019", 2021)]
        [InlineData("2020", 2021)]
        public void NCHSReceiptYearMustBeGreaterThanOrEqualToDeathYear(string receiptYear, uint deathYear)
        {
            CodingResponseMessage message = new CodingResponseMessage("destination", "http://nchs.cdc.gov/vrdr_submission");
            message.DeathYear = deathYear;
            System.ArgumentException ex = Assert.Throws<System.ArgumentException>(() => message.NCHSReceiptYearString = receiptYear);
            Assert.Equal("NCHS Receipt Year must be greater than or equal to Death Year, or null", ex.Message);
        }

        [Fact]
        public void CreateCodingUpdate()
        {
            CodingUpdateMessage message = new CodingUpdateMessage("destination", "http://nchs.cdc.gov/vrdr_submission");
            Assert.Equal("http://nchs.cdc.gov/vrdr_coding_update", message.MessageType);
            Assert.Equal("destination", message.MessageDestination);

            Assert.Null(message.cert_no);
            message.cert_no = 10;
            Assert.Equal((uint)10, message.cert_no);

            Assert.Null(message.state_auxiliary_id);
            message.state_auxiliary_id = "id101010";
            Assert.Equal("id101010", message.state_auxiliary_id);

            Assert.Null(message.DeathYear);
            message.DeathYear = 2019;
            Assert.Equal((uint)2019, message.DeathYear);

            Assert.Null(message.DeathJurisdictionID);
            message.DeathJurisdictionID = "NH";
            Assert.Equal("NH", message.DeathJurisdictionID);
            Assert.Equal("2019NH000010", message.NCHSIdentifier);

            Assert.Empty(message.Ethnicity);
            var ethnicity = new Dictionary<CodingResponseMessage.HispanicOrigin, string>();
            ethnicity.Add(CodingResponseMessage.HispanicOrigin.DETHNICE, "123");
            ethnicity.Add(CodingResponseMessage.HispanicOrigin.DETHNIC5C, "456");
            message.Ethnicity = ethnicity;
            ethnicity = message.Ethnicity;
            Assert.Equal(2, ethnicity.Count);
            Assert.True(ethnicity.ContainsKey(CodingResponseMessage.HispanicOrigin.DETHNICE));
            Assert.Equal("123", ethnicity.GetValueOrDefault(CodingResponseMessage.HispanicOrigin.DETHNICE, "foo"));
            Assert.True(ethnicity.ContainsKey(CodingResponseMessage.HispanicOrigin.DETHNIC5C));
            Assert.Equal("456", ethnicity.GetValueOrDefault(CodingResponseMessage.HispanicOrigin.DETHNIC5C, "foo"));

            Assert.Empty(message.Race);
            var race = new Dictionary<CodingResponseMessage.RaceCode, string>();
            race.Add(CodingResponseMessage.RaceCode.RACE1E, "foo");
            race.Add(CodingResponseMessage.RaceCode.RACE17C, "bar");
            race.Add(CodingResponseMessage.RaceCode.RACEBRG, "baz");
            message.Race = race;
            race = message.Race;
            Assert.Equal(3, race.Count);
            Assert.True(race.ContainsKey(CodingResponseMessage.RaceCode.RACE1E));
            Assert.Equal("foo", race.GetValueOrDefault(CodingResponseMessage.RaceCode.RACE1E,"yyz"));
            Assert.True(race.ContainsKey(CodingResponseMessage.RaceCode.RACE17C));
            Assert.Equal("bar", race.GetValueOrDefault(CodingResponseMessage.RaceCode.RACE17C,"yyz"));
            Assert.True(race.ContainsKey(CodingResponseMessage.RaceCode.RACEBRG));
            Assert.Equal("baz", race.GetValueOrDefault(CodingResponseMessage.RaceCode.RACEBRG,"yyz"));

            Assert.Null(message.UnderlyingCauseOfDeath);
            message.UnderlyingCauseOfDeath = "A04.7";
            Assert.Equal("A04.7", message.UnderlyingCauseOfDeath);

            Assert.Empty(message.CauseOfDeathRecordAxis);
            var recordAxisCodes = new List<string>();
            recordAxisCodes.Add("A04.7");
            recordAxisCodes.Add("A41.9");
            recordAxisCodes.Add("J18.9");
            recordAxisCodes.Add("J96.0");
            message.CauseOfDeathRecordAxis = recordAxisCodes;
            recordAxisCodes = message.CauseOfDeathRecordAxis;
            Assert.Equal(4, recordAxisCodes.Count);
            Assert.Equal("A04.7", recordAxisCodes[0]);
            Assert.Equal("A41.9", recordAxisCodes[1]);
            Assert.Equal("J18.9", recordAxisCodes[2]);
            Assert.Equal("J96.0", recordAxisCodes[3]);

        //     Assert.Empty(message.CauseOfDeathEntityAxis);
        //     var entityAxisEntries = new List<CauseOfDeathEntityAxisEntry>();
        //     var entry1 = new CauseOfDeathEntityAxisEntry("1");
        //     entry1.AssignedCodes.Add("code1_1");
        //     entry1.AssignedCodes.Add("code1_2");
        //     entityAxisEntries.Add(entry1);
        //     var entry2 = new CauseOfDeathEntityAxisEntry("2");
        //     entry2.AssignedCodes.Add("code2_1");
        //     entityAxisEntries.Add(entry2);
        //     message.CauseOfDeathEntityAxis = entityAxisEntries;
        //     entityAxisEntries = message.CauseOfDeathEntityAxis;
        //     Assert.Equal(2, (int)entityAxisEntries.Count);
        //     Assert.Equal("1", entityAxisEntries[0].LineNumber);
        //     Assert.Equal(2, (int)entityAxisEntries[0].AssignedCodes.Count);
        //     Assert.Equal("code1_1", entityAxisEntries[0].AssignedCodes[0]);
        //     Assert.Equal("code1_2", entityAxisEntries[0].AssignedCodes[1]);
        //     Assert.Equal("2", entityAxisEntries[1].LineNumber);
        //     Assert.Equal(1, (int)entityAxisEntries[1].AssignedCodes.Count);
        //     Assert.Equal("code2_1", entityAxisEntries[1].AssignedCodes[0]);
     }

        [Fact]
        public void TestCauseOfDeathEntityAxisEntry()
        {
            // var entry = new CauseOfDeathEntityAxisEntry("1");
            // Assert.Equal("1", entry.LineNumber);
            // Assert.Empty(entry.AssignedCodes);
            // entry.AssignedCodes.Add("A10.4");
            // entry.AssignedCodes.Add("J01.5");
            // Assert.Equal(2, entry.AssignedCodes.Count);
            // Assert.Equal("A10.4", entry.AssignedCodes[0]);
            // Assert.Equal("J01.5", entry.AssignedCodes[1]);
        }

        [Fact]
        public void CreateVoidMessage()
        {
            VoidMessage message = new VoidMessage();
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission_void", message.MessageType);
            Assert.Null(message.cert_no);
            message.cert_no = 11;
            Assert.Equal((uint)11, message.cert_no);
            Assert.Null(message.state_auxiliary_id);
            message.state_auxiliary_id = "bar";
            Assert.Equal("bar", message.state_auxiliary_id);
            Assert.Null(message.NCHSIdentifier);
            Assert.Null(message.BlockCount);
            message.BlockCount = 100;
            Assert.Equal((uint)100, message.BlockCount);
        }

        [Fact]
        public void CreateAckForVoidMessage()
        {
            VoidMessage voidMessage = BaseMessage.Parse<VoidMessage>(FixtureStream("fixtures/json/VoidMessage.json"));
            AckMessage ack = new AckMessage(voidMessage);
            Assert.Equal("http://nchs.cdc.gov/vrdr_acknowledgement", ack.MessageType);
            Assert.Equal(voidMessage.MessageId, ack.AckedMessageId);
            Assert.Equal(voidMessage.MessageSource, ack.MessageDestination);
            Assert.Equal(voidMessage.MessageDestination, ack.MessageSource);
            Assert.Equal(voidMessage.state_auxiliary_id, ack.state_auxiliary_id);
            Assert.Equal(voidMessage.cert_no, ack.cert_no);
            Assert.Equal(voidMessage.NCHSIdentifier, ack.NCHSIdentifier);
            Assert.Equal(voidMessage.BlockCount, ack.BlockCount);

            voidMessage = null;
            ack = new AckMessage(voidMessage);
            Assert.Equal("http://nchs.cdc.gov/vrdr_acknowledgement", ack.MessageType);
            Assert.Null(ack.AckedMessageId);
            Assert.Null(ack.MessageDestination);
            Assert.Null(ack.MessageSource);
            Assert.Null(ack.cert_no);
            Assert.Null(ack.state_auxiliary_id);
            Assert.Null(ack.NCHSIdentifier);
            Assert.Null(ack.BlockCount);

            voidMessage = new VoidMessage();
            ack = new AckMessage(voidMessage);
            Assert.Equal("http://nchs.cdc.gov/vrdr_acknowledgement", ack.MessageType);
            Assert.Equal(voidMessage.MessageId, ack.AckedMessageId);
            Assert.Equal(voidMessage.MessageSource, ack.MessageDestination);
            Assert.Equal(voidMessage.MessageDestination, ack.MessageSource);
            Assert.Equal(voidMessage.state_auxiliary_id, ack.state_auxiliary_id);
            Assert.Equal(voidMessage.cert_no, ack.cert_no);
            Assert.Equal(voidMessage.NCHSIdentifier, ack.NCHSIdentifier);
            Assert.Equal(voidMessage.BlockCount, ack.BlockCount);
        }

        [Fact]
        public void CreateVoidMessageFromJson()
        {
            VoidMessage message = BaseMessage.Parse<VoidMessage>(FixtureStream("fixtures/json/VoidMessage.json"));
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission_void", message.MessageType);
            Assert.Equal((uint)1, message.cert_no);
            Assert.Equal((uint)10, message.BlockCount);
            Assert.Equal("42", message.state_auxiliary_id);
            Assert.Equal("2018MA000001", message.NCHSIdentifier);
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission", message.MessageDestination);
            Assert.Equal("nightingale", message.MessageSource);

            message = BaseMessage.Parse<VoidMessage>(FixtureStream("fixtures/json/VoidMessageNoIdentifiers.json"));
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission_void", message.MessageType);
            Assert.Null(message.cert_no);
            Assert.Null(message.state_auxiliary_id);
            Assert.Null(message.NCHSIdentifier);
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission", message.MessageDestination);
            Assert.Equal("nightingale", message.MessageSource);
        }

        [Fact]
        public void CreateVoidForDocument()
        {
            VoidMessage message = new VoidMessage((DeathRecord)XMLRecords[0]);
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission_void", message.MessageType);
            Assert.Equal((uint)1, message.cert_no);
            Assert.Equal("42", message.state_auxiliary_id);
            Assert.Equal("2018MA000001", message.NCHSIdentifier);
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission", message.MessageDestination);
            Assert.Null(message.MessageSource);

            message = new VoidMessage(null);
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission_void", message.MessageType);
            Assert.Null(message.cert_no);
            Assert.Null(message.state_auxiliary_id);
            Assert.Null(message.NCHSIdentifier);
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission", message.MessageDestination);
            Assert.Null(message.MessageSource);
        }

        [Fact]
        public void SelectMessageType()
        {
            var msg = BaseMessage.Parse(FixtureStream("fixtures/json/AckMessage.json"), false);
            Assert.IsType<AckMessage>(msg);
            msg = BaseMessage.Parse(FixtureStream("fixtures/json/VoidMessage.json"), false);
            Assert.IsType<VoidMessage>(msg);
            msg = BaseMessage.Parse(FixtureStream("fixtures/json/CauseOfDeathCodingMessage.json"), false);
            Assert.IsType<CodingResponseMessage>(msg);
            msg = BaseMessage.Parse(FixtureStream("fixtures/json/CodingUpdateMessage.json"), false);
            Assert.IsType<CodingUpdateMessage>(msg);
            msg = BaseMessage.Parse(FixtureStream("fixtures/json/DeathRecordSubmission.json"), false);
            Assert.IsType<DeathRecordSubmission>(msg);
            msg = BaseMessage.Parse(FixtureStream("fixtures/json/DeathRecordUpdate.json"), false);
            Assert.IsType<DeathRecordUpdate>(msg);

            MessageParseException ex = Assert.Throws<MessageParseException>(() => BaseMessage.Parse(FixtureStream("fixtures/json/InvalidMessageType.json")));
            Assert.Equal("Unsupported message type: http://nchs.cdc.gov/vrdr_invalid_type", ex.Message);
            var responseMsg = ex.CreateExtractionErrorMessage();
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission", responseMsg.MessageDestination);
            Assert.Equal("nightingale", responseMsg.MessageSource);
            Assert.Equal("761dca08-259b-4dcd-aeb7-bb3c73fa30f2", responseMsg.FailedMessageId);
            Assert.Null(responseMsg.cert_no);
            Assert.Null(responseMsg.NCHSIdentifier);
            Assert.Null(responseMsg.state_auxiliary_id);

            ex = Assert.Throws<MessageParseException>(() => BaseMessage.Parse(FixtureStream("fixtures/json/MissingMessageType.json")));
            Assert.Equal("Message type was missing from MessageHeader", ex.Message);
            responseMsg = ex.CreateExtractionErrorMessage();
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission", responseMsg.MessageDestination);
            Assert.Equal("nightingale", responseMsg.MessageSource);
            Assert.Equal("761dca08-259b-4dcd-aeb7-bb3c73fa30f2", responseMsg.FailedMessageId);
            Assert.Null(responseMsg.cert_no);
            Assert.Null(responseMsg.NCHSIdentifier);
            Assert.Null(responseMsg.state_auxiliary_id);

            ex = Assert.Throws<MessageParseException>(() => BaseMessage.Parse(FixtureStream("fixtures/json/EmptyMessage.json")));
            Assert.Equal("Failed to find a Bundle Entry containing a Resource of type MessageHeader", ex.Message);
            responseMsg = ex.CreateExtractionErrorMessage();
            Assert.Null(responseMsg.MessageDestination);
            Assert.Null(responseMsg.MessageSource);
            Assert.Null(responseMsg.FailedMessageId);
            Assert.Null(responseMsg.cert_no);
            Assert.Null(responseMsg.NCHSIdentifier);
            Assert.Null(responseMsg.state_auxiliary_id);

            ex = Assert.Throws<MessageParseException>(() => BaseMessage.Parse(FixtureStream("fixtures/json/Empty.json")));
            Assert.Equal("The FHIR Bundle must be of type message, not null", ex.Message);
            responseMsg = ex.CreateExtractionErrorMessage();
            Assert.Null(responseMsg.MessageDestination);
            Assert.Null(responseMsg.MessageSource);
            Assert.Null(responseMsg.FailedMessageId);
            Assert.Null(responseMsg.cert_no);
            Assert.Null(responseMsg.NCHSIdentifier);
            Assert.Null(responseMsg.state_auxiliary_id);
        }

        [Fact]
        public void CreateExtractionErrorForMessage()
        {
            DeathRecordSubmission submission = BaseMessage.Parse<DeathRecordSubmission>(FixtureStream("fixtures/json/DeathRecordSubmission.json"));
            ExtractionErrorMessage err = new ExtractionErrorMessage(submission);
            Assert.Equal("http://nchs.cdc.gov/vrdr_extraction_error", err.MessageType);
            Assert.Equal(submission.MessageId, err.FailedMessageId);
            Assert.Equal(submission.MessageSource, err.MessageDestination);
            Assert.Equal(submission.state_auxiliary_id, err.state_auxiliary_id);
            Assert.Equal(submission.cert_no, err.cert_no);
            Assert.Equal(submission.NCHSIdentifier, err.NCHSIdentifier);
            Assert.Empty(err.Issues);
            var issues = new List<Issue>();
            var issue = new Issue(OperationOutcome.IssueSeverity.Fatal, OperationOutcome.IssueType.Invalid, "The message was invalid");
            issues.Add(issue);
            issue = new Issue(OperationOutcome.IssueSeverity.Warning, OperationOutcome.IssueType.Expired, "The message was very old");
            issues.Add(issue);
            err.Issues = issues;
            issues = err.Issues;
            Assert.Equal(2, (int)issues.Count);
            Assert.Equal(OperationOutcome.IssueSeverity.Fatal, issues[0].Severity);
            Assert.Equal(OperationOutcome.IssueType.Invalid, issues[0].Type);
            Assert.Equal("The message was invalid", issues[0].Description);
            Assert.Equal(OperationOutcome.IssueSeverity.Warning, issues[1].Severity);
            Assert.Equal(OperationOutcome.IssueType.Expired, issues[1].Type);
            Assert.Equal("The message was very old", issues[1].Description);

            submission = null;
            err = new ExtractionErrorMessage(submission);
            Assert.Equal("http://nchs.cdc.gov/vrdr_extraction_error", err.MessageType);
            Assert.Null(err.FailedMessageId);
            Assert.Null(err.MessageDestination);
            Assert.Null(err.cert_no);
            Assert.Null(err.state_auxiliary_id);
            Assert.Null(err.NCHSIdentifier);
            Assert.Empty(err.Issues);
        }

        [Fact]
        public void CreateExtractionErrorFromJson()
        {
            ExtractionErrorMessage err = BaseMessage.Parse<ExtractionErrorMessage>(FixtureStream("fixtures/json/ExtractionErrorMessage.json"));
            Assert.Equal("http://nchs.cdc.gov/vrdr_extraction_error", err.MessageType);
            Assert.Equal((uint)1, err.cert_no);
            Assert.Equal("42", err.state_auxiliary_id);
            Assert.Equal("2018MA000001", err.NCHSIdentifier);
            var issues = err.Issues;
            Assert.Equal(2, (int)issues.Count);
            Assert.Equal(OperationOutcome.IssueSeverity.Fatal, issues[0].Severity);
            Assert.Equal(OperationOutcome.IssueType.Invalid, issues[0].Type);
            Assert.Equal("The message was invalid", issues[0].Description);
            Assert.Equal(OperationOutcome.IssueSeverity.Warning, issues[1].Severity);
            Assert.Equal(OperationOutcome.IssueType.Expired, issues[1].Type);
            Assert.Equal("The message was very old", issues[1].Description);
        }

        [Fact]
        public void BuildEntityAxis()
        {
            // var builder = new CauseOfDeathEntityAxisBuilder();
            // var list = builder.ToCauseOfDeathEntityAxis();
            // Assert.Empty(list);
            // Exception ex = Assert.Throws<System.ArgumentException>(() => builder.Add("foo", "1", "bar"));
            // Assert.Equal("The value of the line argument must be a number, found: foo", ex.Message);
            // ex = Assert.Throws<System.ArgumentException>(() => builder.Add("1", "baz", "bar"));
            // Assert.Equal("The value of the position argument must be a number, found: baz", ex.Message);
            // Assert.Empty(list);
            // builder.Add("6", "1", "A047");
            // builder.Add("4", "1", "J189");
            // builder.Add("3", "1", "A419");
            // builder.Add("2", "3", "N19");
            // builder.Add("2", "2", "R579");
            // builder.Add("2", "1", "J960");
            // builder.Add("1", "1", "R688");
            // builder.Add("1", "2", "   "); // should be skipped
            // builder.Add("1", "3", ""); // should be skipped
            // builder.Add("1", "4", null); // should be skipped
            // list = builder.ToCauseOfDeathEntityAxis();
            // Assert.Equal(5, list.Count);
            // var entry = list[0];
            // Assert.Equal("1", entry.LineNumber);
            // Assert.Equal(1, (int)entry.AssignedCodes.Count);
            // Assert.Equal("R688", entry.AssignedCodes[0]);
            // entry = list[1];
            // Assert.Equal("2", entry.LineNumber);
            // Assert.Equal(3, (int)entry.AssignedCodes.Count);
            // Assert.Equal("J960", entry.AssignedCodes[0]);
            // Assert.Equal("R579", entry.AssignedCodes[1]);
            // Assert.Equal("N19", entry.AssignedCodes[2]);
            // entry = list[2];
            // Assert.Equal("3", entry.LineNumber);
            // Assert.Equal(1, (int)entry.AssignedCodes.Count);
            // Assert.Equal("A419", entry.AssignedCodes[0]);
            // entry = list[3];
            // Assert.Equal("4", entry.LineNumber);
            // Assert.Equal(1, (int)entry.AssignedCodes.Count);
            // Assert.Equal("J189", entry.AssignedCodes[0]);
            // entry = list[4];
            // Assert.Equal("6", entry.LineNumber);
            // Assert.Equal(1, (int)entry.AssignedCodes.Count);
            // Assert.Equal("A047", entry.AssignedCodes[0]);
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

        private StreamReader FixtureStream(string filePath)
        {
            if (!Path.IsPathRooted(filePath))
            {
                filePath = Path.GetRelativePath(Directory.GetCurrentDirectory(), filePath);
            }
            return File.OpenText(filePath);
        }
    }
}
