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
            DeathRecordSubmissionMessage submission = new DeathRecordSubmissionMessage();
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission", submission.MessageType);
            Assert.Null(submission.DeathRecord);
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission", submission.MessageDestination);
            Assert.NotNull(submission.MessageTimestamp);
            Assert.Null(submission.MessageSource);
            Assert.NotNull(submission.MessageId);
            Assert.Null(submission.CertNo);
            Assert.Null(submission.StateAuxiliaryId);
            Assert.Null(submission.NCHSIdentifier);
        }

        [Fact]
        public void CreateSubmissionFromDeathRecord()
        {
            DeathRecord record = (DeathRecord)XMLRecords[0];
            DeathRecordSubmissionMessage submission = new DeathRecordSubmissionMessage(record);
            Assert.NotNull(submission.DeathRecord);
            Assert.Equal("2019-02-20T16:48:06-05:00", submission.DeathRecord.DateOfDeathPronouncement);
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission", submission.MessageType);
            Assert.Equal(10,10);
            Assert.Equal((uint)182, submission.CertNo);
            Assert.Equal((uint)2019, submission.DeathYear);
            Assert.Equal("000000000042", submission.StateAuxiliaryId);
            Assert.Equal("2019YC000182", submission.NCHSIdentifier);

            record = (DeathRecord)JSONRecords[0];
            submission = new DeathRecordSubmissionMessage(record);
            Assert.NotNull(submission.DeathRecord);
            Assert.Equal("2018-02-20T16:48:06-05:00", submission.DeathRecord.DateOfDeathPronouncement);
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission", submission.MessageType);
            Assert.Equal((uint)182, submission.CertNo);
            Assert.Equal((uint)2019, submission.DeathYear);
            Assert.Equal("000000000042", submission.StateAuxiliaryId);
            Assert.Equal("2019YC000182", submission.NCHSIdentifier);

            record = null;
            submission = new DeathRecordSubmissionMessage(record);
            Assert.Null(submission.DeathRecord);
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission", submission.MessageType);
            Assert.Null(submission.CertNo);
            Assert.Null(submission.StateAuxiliaryId);
            Assert.Null(submission.NCHSIdentifier);

            record = (DeathRecord)JSONRecords[1];
            submission = new DeathRecordSubmissionMessage(record);
            Assert.NotNull(submission.DeathRecord);
            Assert.Equal("2019-02-20T16:48:06-05:00", submission.DeathRecord.DateOfDeathPronouncement);
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission", submission.MessageType);
            Assert.Null(submission.CertNo);
            Assert.Null(submission.StateAuxiliaryId);
            Assert.Null(submission.NCHSIdentifier);
        }

        [Fact]
        public void CreateSubmissionFromJSON()
        {
            DeathRecordSubmissionMessage submission = BaseMessage.Parse<DeathRecordSubmissionMessage>(FixtureStream("fixtures/json/DeathRecordSubmissionMessage.json"));
            Assert.Equal("2018-02-20T16:48:06-05:00", submission.DeathRecord.DateOfDeathPronouncement);
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission", submission.MessageType);
            Assert.Equal("2018MA000001", submission.NCHSIdentifier);
            Assert.Equal((uint)1, submission.CertNo);
            Assert.Equal((uint)2018, submission.DeathYear);
            Assert.Equal("42", submission.StateAuxiliaryId);
            Assert.Equal(submission.JurisdictionId, submission.DeathRecord.DeathLocationJurisdiction);

            submission = BaseMessage.Parse<DeathRecordSubmissionMessage>(FixtureStream("fixtures/json/DeathRecordSubmissionNoIdentifiers.json"));
            Assert.Equal("2018-02-20T16:48:06-05:00", submission.DeathRecord.DateOfDeathPronouncement);
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission", submission.MessageType);
            Assert.Null(submission.CertNo);
            Assert.Null(submission.StateAuxiliaryId);
            Assert.Null(submission.NCHSIdentifier);

            MessageParseException ex = Assert.Throws<MessageParseException>(() => BaseMessage.Parse(FixtureStream("fixtures/json/EmptySubmission.json")));
            Assert.Equal("Error processing DeathRecord entry in the message: Failed to find a Bundle Entry containing a Resource of type Bundle", ex.Message);
            ExtractionErrorMessage errMsg = ex.CreateExtractionErrorMessage();
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission", errMsg.MessageSource);
            Assert.Equal("nightingale", errMsg.MessageDestination);
            Assert.Equal("a9d66d2e-2480-4e8d-bab3-4e4c761da1b7", errMsg.FailedMessageId);
            Assert.Equal("2018MA000001", errMsg.NCHSIdentifier);
            Assert.Equal((uint)1, errMsg.CertNo);
            Assert.Equal((uint)2018, errMsg.DeathYear);
            Assert.Equal("42", errMsg.StateAuxiliaryId);

            ex = Assert.Throws<MessageParseException>(() => BaseMessage.Parse<AcknowledgementMessage>(FixtureStream("fixtures/json/DeathRecordSubmissionMessage.json")));
            Assert.Equal("The supplied message was of type VRDR.DeathRecordSubmissionMessage, expected VRDR.AcknowledgementMessage or a subclass", ex.Message);
        }

        [Fact]
        public void CreateSubmissionFromBundle()
        {
            DeathRecordSubmissionMessage submission = new DeathRecordSubmissionMessage();
            submission.DeathRecord = new DeathRecord();
            submission.CertNo = 42;
            submission.StateAuxiliaryId = "identifier";
            Bundle submissionBundle = (Bundle)submission;

            DeathRecordSubmissionMessage parsed = BaseMessage.Parse<DeathRecordSubmissionMessage>(submissionBundle);
            Assert.Equal(submission.DeathRecord.ToJson(), parsed.DeathRecord.ToJson());
            Assert.Equal(submission.MessageType, parsed.MessageType);
            Assert.Equal(submission.CertNo, parsed.CertNo);
            Assert.Equal(submission.StateAuxiliaryId, parsed.StateAuxiliaryId);
            Assert.Equal(submission.NCHSIdentifier, parsed.NCHSIdentifier);
        }

        [Fact]
        public void CreateUpdate()
        {
            DeathRecordUpdateMessage submission = new DeathRecordUpdateMessage();
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission_update", submission.MessageType);
            Assert.Null(submission.DeathRecord);
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission", submission.MessageDestination);
            Assert.NotNull(submission.MessageTimestamp);
            Assert.Null(submission.MessageSource);
            Assert.NotNull(submission.MessageId);
            Assert.Null(submission.CertNo);
            Assert.Null(submission.StateAuxiliaryId);
            Assert.Null(submission.NCHSIdentifier);
        }

        [Fact]
        public void CreateUpdateFromDeathRecord()
        {
            DeathRecordUpdateMessage update = new DeathRecordUpdateMessage((DeathRecord)XMLRecords[0]);
            Assert.NotNull(update.DeathRecord);
            Assert.Equal("2019-02-20T16:48:06-05:00", update.DeathRecord.DateOfDeathPronouncement);
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission_update", update.MessageType);
            Assert.Equal((uint)182, update.CertNo);
            Assert.Equal((uint)2019, update.DeathYear);
            Assert.Equal("000000000042", update.StateAuxiliaryId);
            Assert.Equal("2019YC000182", update.NCHSIdentifier);

            update = new DeathRecordUpdateMessage((DeathRecord)JSONRecords[1]); // no ids in this death record (except jurisdiction id which is required)
            Assert.NotNull(update.DeathRecord);
            Assert.Equal("2019-02-20T16:48:06-05:00", update.DeathRecord.DateOfDeathPronouncement);
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission_update", update.MessageType);
            Assert.Null(update.CertNo);
            Assert.Null(update.StateAuxiliaryId);
            Assert.Null(update.NCHSIdentifier);
        }

        [Fact]
        public void CreateUpdateFromJSON()
        {
            DeathRecordUpdateMessage update = BaseMessage.Parse<DeathRecordUpdateMessage>(FixtureStream("fixtures/json/DeathRecordUpdateMessage.json"));
            Assert.Equal("2018-02-20T16:48:06-05:00", update.DeathRecord.DateOfDeathPronouncement);
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission_update", update.MessageType);
            Assert.Equal("2018MA000001", update.NCHSIdentifier);
            Assert.Equal((uint)1, update.CertNo);
            Assert.Equal((uint)2018, update.DeathYear);
            Assert.Equal("42", update.StateAuxiliaryId);

            update = BaseMessage.Parse<DeathRecordUpdateMessage>(FixtureStream("fixtures/json/DeathRecordUpdateNoIdentifiers.json"));
            Assert.Equal("2018-02-20T16:48:06-05:00", update.DeathRecord.DateOfDeathPronouncement);
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission_update", update.MessageType);
            Assert.Null(update.CertNo);
            Assert.Null(update.StateAuxiliaryId);
            Assert.Null(update.NCHSIdentifier);
        }

        [Fact]
        public void CreatUpdateFromBundle()
        {
            DeathRecordUpdateMessage update = new DeathRecordUpdateMessage();
            update.DeathRecord = new DeathRecord();
            update.CertNo = 42;
            update.StateAuxiliaryId = "identifier";
            Bundle updateBundle = (Bundle)update;

            DeathRecordUpdateMessage parsed = BaseMessage.Parse<DeathRecordUpdateMessage>(updateBundle);
            Assert.Equal(update.DeathRecord.ToJson(), parsed.DeathRecord.ToJson());
            Assert.Equal(update.MessageType, parsed.MessageType);
            Assert.Equal(update.CertNo, parsed.CertNo);
            Assert.Equal(update.StateAuxiliaryId, parsed.StateAuxiliaryId);
            Assert.Equal(update.NCHSIdentifier, parsed.NCHSIdentifier);
        }

        [Fact]
        public void CreateAckForMessage()
        {
            DeathRecordSubmissionMessage submission = BaseMessage.Parse<DeathRecordSubmissionMessage>(FixtureStream("fixtures/json/DeathRecordSubmissionMessage.json"));
            AcknowledgementMessage ack = new AcknowledgementMessage(submission);
            Assert.Equal("http://nchs.cdc.gov/vrdr_acknowledgement", ack.MessageType);
            Assert.Equal(submission.MessageId, ack.AckedMessageId);
            Assert.Equal(submission.MessageSource, ack.MessageDestination);
            Assert.Equal(submission.MessageDestination, ack.MessageSource);
            Assert.Equal(submission.StateAuxiliaryId, ack.StateAuxiliaryId);
            Assert.Equal(submission.CertNo, ack.CertNo);
            Assert.Equal(submission.NCHSIdentifier, ack.NCHSIdentifier);

            submission = null;
            ack = new AcknowledgementMessage(submission);
            Assert.Equal("http://nchs.cdc.gov/vrdr_acknowledgement", ack.MessageType);
            Assert.Null(ack.AckedMessageId);
            Assert.Null(ack.MessageDestination);
            Assert.Null(ack.MessageSource);
            Assert.Null(ack.CertNo);
            Assert.Null(ack.StateAuxiliaryId);
            Assert.Null(ack.NCHSIdentifier);

            submission = new DeathRecordSubmissionMessage();
            ack = new AcknowledgementMessage(submission);
            Assert.Equal("http://nchs.cdc.gov/vrdr_acknowledgement", ack.MessageType);
            Assert.Equal(submission.MessageId, ack.AckedMessageId);
            Assert.Equal(submission.MessageSource, ack.MessageDestination);
            Assert.Equal(submission.MessageDestination, ack.MessageSource);
            Assert.Equal(submission.StateAuxiliaryId, ack.StateAuxiliaryId);
            Assert.Equal(submission.CertNo, ack.CertNo);
            Assert.Equal(submission.NCHSIdentifier, ack.NCHSIdentifier);
        }

        [Fact]
        public void CreateAckFromJSON()
        {
            AcknowledgementMessage ack = BaseMessage.Parse<AcknowledgementMessage>(FixtureStream("fixtures/json/AcknowledgementMessage.json"));
            Assert.Equal("http://nchs.cdc.gov/vrdr_acknowledgement", ack.MessageType);
            Assert.Equal("a9d66d2e-2480-4e8d-bab3-4e4c761da1b7", ack.AckedMessageId);
            Assert.Equal("nightingale", ack.MessageDestination);
            Assert.Equal("2018MA000001", ack.NCHSIdentifier);
            Assert.Equal((uint)1, ack.CertNo);
            Assert.Equal((uint)2018, ack.DeathYear);
            Assert.Equal("42", ack.StateAuxiliaryId);
        }

        [Fact]
        public void CreateAckFromXML()
        {
            AcknowledgementMessage ack = BaseMessage.Parse<AcknowledgementMessage>(FixtureStream("fixtures/xml/AcknowledgementMessage.xml"));
            Assert.Equal("http://nchs.cdc.gov/vrdr_acknowledgement", ack.MessageType);
            Assert.Equal("a9d66d2e-2480-4e8d-bab3-4e4c761da1b7", ack.AckedMessageId);
            Assert.Equal("nightingale", ack.MessageDestination);
            Assert.Equal("2018MA000001", ack.NCHSIdentifier);
            Assert.Equal((uint)1, ack.CertNo);
            Assert.Equal((uint)2018, ack.DeathYear);
            Assert.Equal("42", ack.StateAuxiliaryId);
        }

        [Fact]
        public void CreateAckFromBundle()
        {
            AcknowledgementMessage ackFixture = BaseMessage.Parse<AcknowledgementMessage>(FixtureStream("fixtures/json/AcknowledgementMessage.json"));
            Bundle ackBundle = (Bundle)ackFixture;
            AcknowledgementMessage ack = BaseMessage.Parse<AcknowledgementMessage>(ackBundle);
            Assert.Equal("a9d66d2e-2480-4e8d-bab3-4e4c761da1b7", ack.AckedMessageId);
            Assert.Equal("nightingale", ack.MessageDestination);
            Assert.Equal("2018MA000001", ack.NCHSIdentifier);
            Assert.Equal((uint)1, ack.CertNo);
            Assert.Equal((uint)2018, ack.DeathYear);
            Assert.Equal("42", ack.StateAuxiliaryId);
        }

        [Fact]
        public void GenericCodingResponseMessageNoLongerSupported()
        {
            Assert.Throws<MessageParseException>(() => BaseMessage.Parse<CodingResponseMessage>(FixtureStream("fixtures/json/CodingResponseMessage.json")));
        }

        [Fact]
        public void CreateCauseOfDeathCodingResponseFromJSON()
        {
            CauseOfDeathCodingMessage message = BaseMessage.Parse<CauseOfDeathCodingMessage>(FixtureStream("fixtures/json/CauseOfDeathCodingMessage.json"));

            Assert.Equal(CauseOfDeathCodingMessage.MESSAGE_TYPE, message.MessageType);
            Assert.Equal("destination", message.MessageDestination);
            Assert.Equal((uint)1, message.CertNo);
            Assert.Equal((uint)2018, message.DeathYear);
            Assert.Equal("42", message.StateAuxiliaryId);
            Assert.Equal("2018MA000001", message.NCHSIdentifier);
            Assert.Equal("A04.7", message.UnderlyingCauseOfDeath);
            var recordAxisCodes = message.CauseOfDeathRecordAxis;
            Assert.Equal(4, recordAxisCodes.Count);
            Assert.Equal("A04.7", recordAxisCodes[0]);
            Assert.Equal("A41.9", recordAxisCodes[1]);
            Assert.Equal("J18.9", recordAxisCodes[2]);
            Assert.Equal("J96.0", recordAxisCodes[3]);
            var entityAxisEntries = message.CauseOfDeathEntityAxis;
            Assert.Equal(2, (int)entityAxisEntries.Count);
            Assert.Equal("abcde", entityAxisEntries[0].LineNumber);
            Assert.Equal(2, (int)entityAxisEntries[0].AssignedCodes.Count);
            Assert.Equal("code1_1", entityAxisEntries[0].AssignedCodes[0]);
            Assert.Equal("code1_2", entityAxisEntries[0].AssignedCodes[1]);
            Assert.Equal("xyzzy", entityAxisEntries[1].LineNumber);
            Assert.Equal(1, (int)entityAxisEntries[1].AssignedCodes.Count);
            Assert.Equal("code2_1", entityAxisEntries[1].AssignedCodes[0]);
            var entityAxisEntryList = message.CauseOfDeathEntityAxisList;
            Assert.Equal(3, (int)entityAxisEntryList.Count);
            (string line, string position, string code) = entityAxisEntryList[0];
            Assert.Equal("abcde", line);
            Assert.Equal("1", position);
            Assert.Equal("code1_1", code);
            (line, position, code) = entityAxisEntryList[1];
            Assert.Equal("abcde", line);
            Assert.Equal("2", position);
            Assert.Equal("code1_2", code);
            (line, position, code) = entityAxisEntryList[2];
            Assert.Equal("xyzzy", line);
            Assert.Equal("1", position);
            Assert.Equal("code2_1", code);

            Assert.Equal("8", message.CoderStatus);
            Assert.Equal("B202101", message.ShipmentNumber);
            Assert.Equal((uint)8, message.NCHSReceiptDay);
            Assert.Equal("08", message.NCHSReceiptDayString);
            Assert.Equal((uint)1, message.NCHSReceiptMonth);
            Assert.Equal("01", message.NCHSReceiptMonthString);
            Assert.Equal((uint)2021, message.NCHSReceiptYear);
            Assert.Equal("2021", message.NCHSReceiptYearString);
            Assert.Equal(CauseOfDeathCodingMessage.MannerOfDeathEnum.Accident, message.MannerOfDeath);
            Assert.Equal("5", message.IntentionalReject);
            Assert.Equal(CodingResponseMessage.ACMESystemRejectEnum.ACMEReject, message.ACMESystemRejectCodes);
            Assert.Equal(CauseOfDeathCodingMessage.PlaceOfInjuryEnum.Home, message.PlaceOfInjury);
        }

        [Fact]
        public void CreateDemographicCodingResponseFromJSON()
        {
            DemographicsCodingMessage message = BaseMessage.Parse<DemographicsCodingMessage>(FixtureStream("fixtures/json/DemographicsCodingMessage.json"));

            Assert.Equal(DemographicsCodingMessage.MESSAGE_TYPE, message.MessageType);
            Assert.Equal("destination", message.MessageDestination);
            Assert.Equal((uint)1, message.CertNo);
            Assert.Equal((uint)2018, message.DeathYear);
            Assert.Equal("42", message.StateAuxiliaryId);
            Assert.Equal("2018MA000001", message.NCHSIdentifier);
            var ethnicity = message.Ethnicity;
            Assert.Equal(2, ethnicity.Count);
            Assert.True(ethnicity.ContainsKey(DemographicsCodingMessage.HispanicOrigin.DETHNICE));
            Assert.Equal("123", ethnicity.GetValueOrDefault(DemographicsCodingMessage.HispanicOrigin.DETHNICE, "foo"));
            Assert.True(ethnicity.ContainsKey(DemographicsCodingMessage.HispanicOrigin.DETHNIC5C));
            Assert.Equal("456", ethnicity.GetValueOrDefault(DemographicsCodingMessage.HispanicOrigin.DETHNIC5C, "foo"));
            var race = message.Race;
            Assert.Equal(3, race.Count);
            Assert.True(race.ContainsKey(DemographicsCodingMessage.RaceCode.RACE1E));
            Assert.Equal("foo", race.GetValueOrDefault(DemographicsCodingMessage.RaceCode.RACE1E, "yyz"));
            Assert.True(race.ContainsKey(DemographicsCodingMessage.RaceCode.RACE17C));
            Assert.Equal("bar", race.GetValueOrDefault(DemographicsCodingMessage.RaceCode.RACE17C, "yyz"));
            Assert.True(race.ContainsKey(DemographicsCodingMessage.RaceCode.RACEBRG));
            Assert.Equal("baz", race.GetValueOrDefault(DemographicsCodingMessage.RaceCode.RACEBRG, "yyz"));
        }

        [Fact]
        public void GenericCodingUpdateNoLongerSupported()
        {
            Assert.Throws<MessageParseException>(() => BaseMessage.Parse<CauseOfDeathCodingUpdateMessage>(FixtureStream("fixtures/json/CodingUpdateMessage.json")));
            Assert.Throws<MessageParseException>(() => BaseMessage.Parse<DemographicsCodingUpdateMessage>(FixtureStream("fixtures/json/CodingUpdateMessage.json")));
        }

        [Fact]
        public void CreateCauseOfDeathCodingUpdateFromJSON()
        {
            CauseOfDeathCodingUpdateMessage message = BaseMessage.Parse<CauseOfDeathCodingUpdateMessage>(FixtureStream("fixtures/json/CauseOfDeathCodingUpdateMessage.json"));
            Assert.Equal(CauseOfDeathCodingUpdateMessage.MESSAGE_TYPE, message.MessageType);
            Assert.Equal("destination", message.MessageDestination);
            Assert.Equal((uint)1, message.CertNo);
            Assert.Equal((uint)2018, message.DeathYear);
            Assert.Equal("42", message.StateAuxiliaryId);
            Assert.Equal("2018MA000001", message.NCHSIdentifier);
            Assert.Equal("A04.7", message.UnderlyingCauseOfDeath);
            var recordAxisCodes = message.CauseOfDeathRecordAxis;
            Assert.Equal(4, recordAxisCodes.Count);
            Assert.Equal("A04.7", recordAxisCodes[0]);
            Assert.Equal("A41.9", recordAxisCodes[1]);
            Assert.Equal("J18.9", recordAxisCodes[2]);
            Assert.Equal("J96.0", recordAxisCodes[3]);
            var entityAxisEntries = message.CauseOfDeathEntityAxis;
            Assert.Equal(2, (int)entityAxisEntries.Count);
            Assert.Equal("1", entityAxisEntries[0].LineNumber);
            Assert.Equal(2, (int)entityAxisEntries[0].AssignedCodes.Count);
            Assert.Equal("code1_1", entityAxisEntries[0].AssignedCodes[0]);
            Assert.Equal("code1_2", entityAxisEntries[0].AssignedCodes[1]);
            Assert.Equal("2", entityAxisEntries[1].LineNumber);
            Assert.Equal(1, (int)entityAxisEntries[1].AssignedCodes.Count);
            Assert.Equal("code2_1", entityAxisEntries[1].AssignedCodes[0]);
        }

        [Fact]
        public void CreateDemographicsCodingUpdateFromJSON()
        {
            DemographicsCodingUpdateMessage message = BaseMessage.Parse<DemographicsCodingUpdateMessage>(FixtureStream("fixtures/json/DemographicsCodingUpdateMessage.json"));
            Assert.Equal(DemographicsCodingUpdateMessage.MESSAGE_TYPE, message.MessageType);
            Assert.Equal("destination", message.MessageDestination);
            Assert.Equal((uint)1, message.CertNo);
            Assert.Equal((uint)2018, message.DeathYear);
            Assert.Equal("42", message.StateAuxiliaryId);
            Assert.Equal("2018MA000001", message.NCHSIdentifier);
            var ethnicity = message.Ethnicity;
            Assert.Equal(2, ethnicity.Count);
            Assert.True(ethnicity.ContainsKey(DemographicsCodingMessage.HispanicOrigin.DETHNICE));
            Assert.Equal("123", ethnicity.GetValueOrDefault(DemographicsCodingMessage.HispanicOrigin.DETHNICE, "foo"));
            Assert.True(ethnicity.ContainsKey(DemographicsCodingMessage.HispanicOrigin.DETHNIC5C));
            Assert.Equal("456", ethnicity.GetValueOrDefault(DemographicsCodingMessage.HispanicOrigin.DETHNIC5C, "foo"));
            var race = message.Race;
            Assert.Equal(3, race.Count);
            Assert.True(race.ContainsKey(DemographicsCodingMessage.RaceCode.RACE1E));
            Assert.Equal("foo", race.GetValueOrDefault(DemographicsCodingMessage.RaceCode.RACE1E, "yyz"));
            Assert.True(race.ContainsKey(DemographicsCodingMessage.RaceCode.RACE17C));
            Assert.Equal("bar", race.GetValueOrDefault(DemographicsCodingMessage.RaceCode.RACE17C, "yyz"));
            Assert.True(race.ContainsKey(DemographicsCodingMessage.RaceCode.RACEBRG));
            Assert.Equal("baz", race.GetValueOrDefault(DemographicsCodingMessage.RaceCode.RACEBRG, "yyz"));
        }

        [Fact]
        public void CreateCauseOfDeathCodingResponse()
        {
            CauseOfDeathCodingMessage message = new CauseOfDeathCodingMessage("destination", "http://nchs.cdc.gov/vrdr_submission");
            Assert.Equal(CauseOfDeathCodingMessage.MESSAGE_TYPE, message.MessageType);
            Assert.Equal("destination", message.MessageDestination);

            Assert.Null(message.CertNo);
            message.CertNo = 10;
            Assert.Equal((uint)10, message.CertNo);

            Assert.Null(message.StateAuxiliaryId);
            message.StateAuxiliaryId = "id101010";
            Assert.Equal("id101010", message.StateAuxiliaryId);

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
            message.MannerOfDeath = CauseOfDeathCodingMessage.MannerOfDeathEnum.Accident;
            Assert.Equal(CauseOfDeathCodingMessage.MannerOfDeathEnum.Accident, message.MannerOfDeath);

            Assert.Null(message.CoderStatus);
            message.CoderStatus = "8";
            Assert.Equal("8", message.CoderStatus);

            Assert.Null(message.ShipmentNumber);
            message.ShipmentNumber = "B202101";
            Assert.Equal("B202101", message.ShipmentNumber);

            Assert.Null(message.ACMESystemRejectCodes);
            message.ACMESystemRejectCodes = CodingResponseMessage.ACMESystemRejectEnum.ACMEReject;
            Assert.Equal(CodingResponseMessage.ACMESystemRejectEnum.ACMEReject, message.ACMESystemRejectCodes);

            Assert.Null(message.PlaceOfInjury);
            message.PlaceOfInjury = CauseOfDeathCodingMessage.PlaceOfInjuryEnum.Home;
            Assert.Equal(CauseOfDeathCodingMessage.PlaceOfInjuryEnum.Home, message.PlaceOfInjury);

            Assert.Null(message.OtherSpecifiedPlace);
            message.OtherSpecifiedPlace = "Unique Location";
            Assert.Equal("Unique Location", message.OtherSpecifiedPlace);

            Assert.Null(message.JurisdictionId);
            message.JurisdictionId = "NH";
            Assert.Equal("NH", message.JurisdictionId);
            Assert.Equal("2019NH000010", message.NCHSIdentifier);

            Assert.Null(message.IntentionalReject);
            message.IntentionalReject = "5";
            Assert.Equal("5", message.IntentionalReject);

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
            var entityAxisEntries = new List<CauseOfDeathEntityAxisEntry>();
            var entry1 = new CauseOfDeathEntityAxisEntry("1");
            entry1.AssignedCodes.Add("code1_1");
            entry1.AssignedCodes.Add("code1_2");
            entityAxisEntries.Add(entry1);
            var entry2 = new CauseOfDeathEntityAxisEntry("2");
            entry2.AssignedCodes.Add("code2_1");
            entityAxisEntries.Add(entry2);
            message.CauseOfDeathEntityAxis = entityAxisEntries;
            entityAxisEntries = message.CauseOfDeathEntityAxis;
            Assert.Equal(2, (int)entityAxisEntries.Count);
            Assert.Equal("1", entityAxisEntries[0].LineNumber);
            Assert.Equal(2, (int)entityAxisEntries[0].AssignedCodes.Count);
            Assert.Equal("code1_1", entityAxisEntries[0].AssignedCodes[0]);
            Assert.Equal("code1_2", entityAxisEntries[0].AssignedCodes[1]);
            Assert.Equal("2", entityAxisEntries[1].LineNumber);
            Assert.Equal(1, (int)entityAxisEntries[1].AssignedCodes.Count);
            Assert.Equal("code2_1", entityAxisEntries[1].AssignedCodes[0]);
            var entityAxisEntryList = message.CauseOfDeathEntityAxisList;
            Assert.Equal(3, (int)entityAxisEntryList.Count);
            (string line, string position, string code) = entityAxisEntryList[0];
            Assert.Equal("1", line);
            Assert.Equal("1", position);
            Assert.Equal("code1_1", code);
            (line, position, code) = entityAxisEntryList[1];
            Assert.Equal("1", line);
            Assert.Equal("2", position);
            Assert.Equal("code1_2", code);
            (line, position, code) = entityAxisEntryList[2];
            Assert.Equal("2", line);
            Assert.Equal("1", position);
            Assert.Equal("code2_1", code);
        }

        [Fact]
        public void CreateDemographicCodingResponse()
        {
            DemographicsCodingMessage message = new DemographicsCodingMessage("destination", "http://nchs.cdc.gov/vrdr_submission");
            Assert.Equal(DemographicsCodingMessage.MESSAGE_TYPE, message.MessageType);
            Assert.Equal("destination", message.MessageDestination);

            Assert.Null(message.CertNo);
            message.CertNo = 10;
            Assert.Equal((uint)10, message.CertNo);

            Assert.Null(message.StateAuxiliaryId);
            message.StateAuxiliaryId = "id101010";
            Assert.Equal("id101010", message.StateAuxiliaryId);

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

            Assert.Null(message.CoderStatus);
            message.CoderStatus = "8";
            Assert.Equal("8", message.CoderStatus);

            Assert.Null(message.ShipmentNumber);
            message.ShipmentNumber = "B202101";
            Assert.Equal("B202101", message.ShipmentNumber);

            Assert.Null(message.ACMESystemRejectCodes);
            message.ACMESystemRejectCodes = CodingResponseMessage.ACMESystemRejectEnum.ACMEReject;
            Assert.Equal(CodingResponseMessage.ACMESystemRejectEnum.ACMEReject, message.ACMESystemRejectCodes);

            Assert.Null(message.JurisdictionId);
            message.JurisdictionId = "NH";
            Assert.Equal("NH", message.JurisdictionId);
            Assert.Equal("2019NH000010", message.NCHSIdentifier);

            Assert.Null(message.IntentionalReject);
            message.IntentionalReject = "5";
            Assert.Equal("5", message.IntentionalReject);

            Assert.Empty(message.Ethnicity);
            var ethnicity = new Dictionary<DemographicsCodingMessage.HispanicOrigin, string>();
            ethnicity.Add(DemographicsCodingMessage.HispanicOrigin.DETHNICE, "123");
            ethnicity.Add(DemographicsCodingMessage.HispanicOrigin.DETHNIC5C, "456");
            message.Ethnicity = ethnicity;
            ethnicity = message.Ethnicity;
            Assert.Equal(2, ethnicity.Count);
            Assert.True(ethnicity.ContainsKey(DemographicsCodingMessage.HispanicOrigin.DETHNICE));
            Assert.Equal("123", ethnicity.GetValueOrDefault(DemographicsCodingMessage.HispanicOrigin.DETHNICE, "foo"));
            Assert.True(ethnicity.ContainsKey(DemographicsCodingMessage.HispanicOrigin.DETHNIC5C));
            Assert.Equal("456", ethnicity.GetValueOrDefault(DemographicsCodingMessage.HispanicOrigin.DETHNIC5C, "foo"));

            Assert.Empty(message.Race);
            var race = new Dictionary<DemographicsCodingMessage.RaceCode, string>();
            race.Add(DemographicsCodingMessage.RaceCode.RACE1E, "foo");
            race.Add(DemographicsCodingMessage.RaceCode.RACE17C, "bar");
            race.Add(DemographicsCodingMessage.RaceCode.RACEBRG, "baz");
            message.Race = race;
            race = message.Race;
            Assert.Equal(3, race.Count);
            Assert.True(race.ContainsKey(DemographicsCodingMessage.RaceCode.RACE1E));
            Assert.Equal("foo", race.GetValueOrDefault(DemographicsCodingMessage.RaceCode.RACE1E, "yyz"));
            Assert.True(race.ContainsKey(DemographicsCodingMessage.RaceCode.RACE17C));
            Assert.Equal("bar", race.GetValueOrDefault(DemographicsCodingMessage.RaceCode.RACE17C, "yyz"));
            Assert.True(race.ContainsKey(DemographicsCodingMessage.RaceCode.RACEBRG));
            Assert.Equal("baz", race.GetValueOrDefault(DemographicsCodingMessage.RaceCode.RACEBRG, "yyz"));
        }

        [Theory]
        [InlineData("2021", 2021)]
        [InlineData("2022", 2021)]
        [InlineData(null, 2021)]
        public void SuccessfullySetNCHSReceiptYear(string receiptYear, uint deathYear)
        {
            CodingResponseMessage message = new CauseOfDeathCodingMessage("destination", "http://nchs.cdc.gov/vrdr_submission");
            message.DeathYear = deathYear;
            message.NCHSReceiptYearString = receiptYear;
            Assert.Equal(receiptYear, message.NCHSReceiptYearString);
        }

        [Theory]
        [InlineData("2019", 2021)]
        [InlineData("2020", 2021)]
        public void NCHSReceiptYearMustBeGreaterThanOrEqualToDeathYear(string receiptYear, uint deathYear)
        {
            CodingResponseMessage message = new CauseOfDeathCodingMessage("destination", "http://nchs.cdc.gov/vrdr_submission");
            message.DeathYear = deathYear;
            System.ArgumentException ex = Assert.Throws<System.ArgumentException>(() => message.NCHSReceiptYearString = receiptYear);
            Assert.Equal("NCHS Receipt Year must be greater than or equal to Death Year, or null", ex.Message);
        }

        [Fact]
        public void CreateCauseOfDeathCodingUpdate()
        {
            CauseOfDeathCodingUpdateMessage message = new CauseOfDeathCodingUpdateMessage("destination", "http://nchs.cdc.gov/vrdr_submission");
            Assert.Equal(CauseOfDeathCodingUpdateMessage.MESSAGE_TYPE, message.MessageType);
            Assert.Equal("destination", message.MessageDestination);

            Assert.Null(message.CertNo);
            message.CertNo = 10;
            Assert.Equal((uint)10, message.CertNo);

            Assert.Null(message.StateAuxiliaryId);
            message.StateAuxiliaryId = "id101010";
            Assert.Equal("id101010", message.StateAuxiliaryId);

            Assert.Null(message.DeathYear);
            message.DeathYear = 2019;
            Assert.Equal((uint)2019, message.DeathYear);

            Assert.Null(message.JurisdictionId);
            message.JurisdictionId = "NH";
            Assert.Equal("NH", message.JurisdictionId);
            Assert.Equal("2019NH000010", message.NCHSIdentifier);

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
            var entityAxisEntries = new List<CauseOfDeathEntityAxisEntry>();
            var entry1 = new CauseOfDeathEntityAxisEntry("1");
            entry1.AssignedCodes.Add("code1_1");
            entry1.AssignedCodes.Add("code1_2");
            entityAxisEntries.Add(entry1);
            var entry2 = new CauseOfDeathEntityAxisEntry("2");
            entry2.AssignedCodes.Add("code2_1");
            entityAxisEntries.Add(entry2);
            message.CauseOfDeathEntityAxis = entityAxisEntries;
            entityAxisEntries = message.CauseOfDeathEntityAxis;
            Assert.Equal(2, (int)entityAxisEntries.Count);
            Assert.Equal("1", entityAxisEntries[0].LineNumber);
            Assert.Equal(2, (int)entityAxisEntries[0].AssignedCodes.Count);
            Assert.Equal("code1_1", entityAxisEntries[0].AssignedCodes[0]);
            Assert.Equal("code1_2", entityAxisEntries[0].AssignedCodes[1]);
            Assert.Equal("2", entityAxisEntries[1].LineNumber);
            Assert.Equal(1, (int)entityAxisEntries[1].AssignedCodes.Count);
            Assert.Equal("code2_1", entityAxisEntries[1].AssignedCodes[0]);
        }

        [Fact]
        public void CreateCodingUpdate()
        {
            DemographicsCodingUpdateMessage message = new DemographicsCodingUpdateMessage("destination", "http://nchs.cdc.gov/vrdr_submission");
            Assert.Equal(DemographicsCodingUpdateMessage.MESSAGE_TYPE, message.MessageType);
            Assert.Equal("destination", message.MessageDestination);

            Assert.Null(message.CertNo);
            message.CertNo = 10;
            Assert.Equal((uint)10, message.CertNo);

            Assert.Null(message.StateAuxiliaryId);
            message.StateAuxiliaryId = "id101010";
            Assert.Equal("id101010", message.StateAuxiliaryId);

            Assert.Null(message.DeathYear);
            message.DeathYear = 2019;
            Assert.Equal((uint)2019, message.DeathYear);

            Assert.Null(message.JurisdictionId);
            message.JurisdictionId = "NH";
            Assert.Equal("NH", message.JurisdictionId);
            Assert.Equal("2019NH000010", message.NCHSIdentifier);

            Assert.Empty(message.Ethnicity);
            var ethnicity = new Dictionary<DemographicsCodingMessage.HispanicOrigin, string>();
            ethnicity.Add(DemographicsCodingMessage.HispanicOrigin.DETHNICE, "123");
            ethnicity.Add(DemographicsCodingMessage.HispanicOrigin.DETHNIC5C, "456");
            message.Ethnicity = ethnicity;
            ethnicity = message.Ethnicity;
            Assert.Equal(2, ethnicity.Count);
            Assert.True(ethnicity.ContainsKey(DemographicsCodingMessage.HispanicOrigin.DETHNICE));
            Assert.Equal("123", ethnicity.GetValueOrDefault(DemographicsCodingMessage.HispanicOrigin.DETHNICE, "foo"));
            Assert.True(ethnicity.ContainsKey(DemographicsCodingMessage.HispanicOrigin.DETHNIC5C));
            Assert.Equal("456", ethnicity.GetValueOrDefault(DemographicsCodingMessage.HispanicOrigin.DETHNIC5C, "foo"));

            Assert.Empty(message.Race);
            var race = new Dictionary<DemographicsCodingMessage.RaceCode, string>();
            race.Add(DemographicsCodingMessage.RaceCode.RACE1E, "foo");
            race.Add(DemographicsCodingMessage.RaceCode.RACE17C, "bar");
            race.Add(DemographicsCodingMessage.RaceCode.RACEBRG, "baz");
            message.Race = race;
            race = message.Race;
            Assert.Equal(3, race.Count);
            Assert.True(race.ContainsKey(DemographicsCodingMessage.RaceCode.RACE1E));
            Assert.Equal("foo", race.GetValueOrDefault(DemographicsCodingMessage.RaceCode.RACE1E, "yyz"));
            Assert.True(race.ContainsKey(DemographicsCodingMessage.RaceCode.RACE17C));
            Assert.Equal("bar", race.GetValueOrDefault(DemographicsCodingMessage.RaceCode.RACE17C, "yyz"));
            Assert.True(race.ContainsKey(DemographicsCodingMessage.RaceCode.RACEBRG));
            Assert.Equal("baz", race.GetValueOrDefault(DemographicsCodingMessage.RaceCode.RACEBRG, "yyz"));
        }

        [Fact]
        public void TestCauseOfDeathEntityAxisEntry()
        {
            var entry = new CauseOfDeathEntityAxisEntry("1");
            Assert.Equal("1", entry.LineNumber);
            Assert.Empty(entry.AssignedCodes);
            entry.AssignedCodes.Add("A10.4");
            entry.AssignedCodes.Add("J01.5");
            Assert.Equal(2, entry.AssignedCodes.Count);
            Assert.Equal("A10.4", entry.AssignedCodes[0]);
            Assert.Equal("J01.5", entry.AssignedCodes[1]);
        }

        [Fact]
        public void CreateDeathRecordVoidMessage()
        {
            DeathRecordVoidMessage message = new DeathRecordVoidMessage();
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission_void", message.MessageType);
            Assert.Null(message.CertNo);
            message.CertNo = 11;
            Assert.Equal((uint)11, message.CertNo);
            Assert.Null(message.StateAuxiliaryId);
            message.StateAuxiliaryId = "bar";
            Assert.Equal("bar", message.StateAuxiliaryId);
            Assert.Null(message.NCHSIdentifier);
            Assert.Null(message.BlockCount);
            message.BlockCount = 100;
            Assert.Equal((uint)100, message.BlockCount);
        }

        [Fact]
        public void CreateAckForDeathRecordVoidMessage()
        {
            DeathRecordVoidMessage voidMessage = BaseMessage.Parse<DeathRecordVoidMessage>(FixtureStream("fixtures/json/DeathRecordVoidMessage.json"));
            AcknowledgementMessage ack = new AcknowledgementMessage(voidMessage);
            Assert.Equal("http://nchs.cdc.gov/vrdr_acknowledgement", ack.MessageType);
            Assert.Equal(voidMessage.MessageId, ack.AckedMessageId);
            Assert.Equal(voidMessage.MessageSource, ack.MessageDestination);
            Assert.Equal(voidMessage.MessageDestination, ack.MessageSource);
            Assert.Equal(voidMessage.StateAuxiliaryId, ack.StateAuxiliaryId);
            Assert.Equal(voidMessage.CertNo, ack.CertNo);
            Assert.Equal(voidMessage.NCHSIdentifier, ack.NCHSIdentifier);
            Assert.Equal(voidMessage.BlockCount, ack.BlockCount);

            voidMessage = null;
            ack = new AcknowledgementMessage(voidMessage);
            Assert.Equal("http://nchs.cdc.gov/vrdr_acknowledgement", ack.MessageType);
            Assert.Null(ack.AckedMessageId);
            Assert.Null(ack.MessageDestination);
            Assert.Null(ack.MessageSource);
            Assert.Null(ack.CertNo);
            Assert.Null(ack.StateAuxiliaryId);
            Assert.Null(ack.NCHSIdentifier);
            Assert.Null(ack.BlockCount);

            voidMessage = new DeathRecordVoidMessage();
            ack = new AcknowledgementMessage(voidMessage);
            Assert.Equal("http://nchs.cdc.gov/vrdr_acknowledgement", ack.MessageType);
            Assert.Equal(voidMessage.MessageId, ack.AckedMessageId);
            Assert.Equal(voidMessage.MessageSource, ack.MessageDestination);
            Assert.Equal(voidMessage.MessageDestination, ack.MessageSource);
            Assert.Equal(voidMessage.StateAuxiliaryId, ack.StateAuxiliaryId);
            Assert.Equal(voidMessage.CertNo, ack.CertNo);
            Assert.Equal(voidMessage.NCHSIdentifier, ack.NCHSIdentifier);
            Assert.Equal(voidMessage.BlockCount, ack.BlockCount);
        }

        [Fact]
        public void CreateDeathRecordVoidMessageFromJson()
        {
            DeathRecordVoidMessage message = BaseMessage.Parse<DeathRecordVoidMessage>(FixtureStream("fixtures/json/DeathRecordVoidMessage.json"));
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission_void", message.MessageType);
            Assert.Equal((uint)1, message.CertNo);
            Assert.Equal((uint)10, message.BlockCount);
            Assert.Equal("42", message.StateAuxiliaryId);
            Assert.Equal("2018MA000001", message.NCHSIdentifier);
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission", message.MessageDestination);
            Assert.Equal("nightingale", message.MessageSource);

            message = BaseMessage.Parse<DeathRecordVoidMessage>(FixtureStream("fixtures/json/DeathRecordVoidMessageNoIdentifiers.json"));
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission_void", message.MessageType);
            Assert.Null(message.CertNo);
            Assert.Null(message.StateAuxiliaryId);
            Assert.Null(message.NCHSIdentifier);
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission", message.MessageDestination);
            Assert.Equal("nightingale", message.MessageSource);
        }

        [Fact]
        public void CreateVoidForDocument()
        {
            DeathRecordVoidMessage message = new DeathRecordVoidMessage((DeathRecord)XMLRecords[0]);
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission_void", message.MessageType);
            Assert.Equal((uint)182, message.CertNo);
            Assert.Equal("000000000042", message.StateAuxiliaryId);
            Assert.Equal("2019YC000182", message.NCHSIdentifier);
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission", message.MessageDestination);
            Assert.Null(message.MessageSource);

            message = new DeathRecordVoidMessage(null);
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission_void", message.MessageType);
            Assert.Null(message.CertNo);
            Assert.Null(message.StateAuxiliaryId);
            Assert.Null(message.NCHSIdentifier);
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission", message.MessageDestination);
            Assert.Null(message.MessageSource);
        }

        [Fact]
        public void CreateDeathRecordAliasMessage()
        {
            DeathRecordAliasMessage message = new DeathRecordAliasMessage();
            Assert.Equal("http://nchs.cdc.gov/vrdr_alias", message.MessageType);
            Assert.Null(message.CertNo);
            message.CertNo = 12;
            Assert.Equal((uint)12, message.CertNo);
            Assert.Null(message.StateAuxiliaryId);
            message.StateAuxiliaryId = "SAI";
            Assert.Equal("SAI", message.StateAuxiliaryId);
            Assert.Null(message.NCHSIdentifier);
            Assert.Null(message.AliasDecedentFirstName);
            message.AliasDecedentFirstName = "DecedentFirstName";
            Assert.Equal("DecedentFirstName", message.AliasDecedentFirstName);
            Assert.Null(message.AliasDecedentLastName);
            message.AliasDecedentLastName = "DecedentLastName";
            Assert.Equal("DecedentLastName", message.AliasDecedentLastName);
            Assert.Null(message.AliasDecedentMiddleName);
            message.AliasDecedentMiddleName = "DecedentMiddleName";
            Assert.Equal("DecedentMiddleName", message.AliasDecedentMiddleName);
            Assert.Null(message.AliasDecedentNameSuffix);
            message.AliasDecedentNameSuffix = "DecedentNameSuffix";
            Assert.Equal("DecedentNameSuffix", message.AliasDecedentNameSuffix);
            Assert.Null(message.AliasFatherSurname);
            message.AliasFatherSurname = "FatherSurname";
            Assert.Equal("FatherSurname", message.AliasFatherSurname);
            Assert.Null(message.AliasSocialSecurityNumber);
            message.AliasSocialSecurityNumber = "SocialSecurityNumber";
            Assert.Equal("SocialSecurityNumber", message.AliasSocialSecurityNumber);
        }

        [Fact]
        public void CreateAckForDeathRecordAliasMessage()
        {
            DeathRecordAliasMessage message = BaseMessage.Parse<DeathRecordAliasMessage>(FixtureStream("fixtures/json/DeathRecordAliasMessage.json"));
            AcknowledgementMessage ack = new AcknowledgementMessage(message);
            Assert.Equal("http://nchs.cdc.gov/vrdr_acknowledgement", ack.MessageType);
            Assert.Equal(message.MessageId, ack.AckedMessageId);
            Assert.Equal(message.MessageSource, ack.MessageDestination);
            Assert.Equal(message.MessageDestination, ack.MessageSource);
            Assert.Equal(message.StateAuxiliaryId, ack.StateAuxiliaryId);
            Assert.Equal(message.CertNo, ack.CertNo);
            Assert.Equal(message.NCHSIdentifier, ack.NCHSIdentifier);
        }

        [Fact]
        public void CreateDeathRecordAliasMessageFromJson()
        {
            DeathRecordAliasMessage message = BaseMessage.Parse<DeathRecordAliasMessage>(FixtureStream("fixtures/json/DeathRecordAliasMessage.json"));
            Assert.Equal("http://nchs.cdc.gov/vrdr_alias", message.MessageType);
            Assert.Equal((uint)12, message.CertNo);
            Assert.Equal("SAI", message.StateAuxiliaryId);
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission", message.MessageDestination);
            Assert.Equal("DecedentFirstName", message.AliasDecedentFirstName);
            Assert.Equal("DecedentLastName", message.AliasDecedentLastName);
            Assert.Equal("DecedentMiddleName", message.AliasDecedentMiddleName);
            Assert.Equal("DecedentNameSuffix", message.AliasDecedentNameSuffix);
            Assert.Equal("FatherSurname", message.AliasFatherSurname);
            Assert.Equal("SocialSecurityNumber", message.AliasSocialSecurityNumber);
        }

        [Fact]
        public void CreateAliasForDocument()
        {
            DeathRecordAliasMessage message = new DeathRecordAliasMessage((DeathRecord)XMLRecords[0]);
            Assert.Equal("http://nchs.cdc.gov/vrdr_alias", message.MessageType);
            Assert.Equal((uint)1, message.CertNo);
            Assert.Equal("000000000042", message.StateAuxiliaryId);
            Assert.Equal("2019YC000001", message.NCHSIdentifier);
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission", message.MessageDestination);
            Assert.Null(message.MessageSource);
        }

        [Fact]
        public void SelectMessageType()
        {
            var msg = BaseMessage.Parse(FixtureStream("fixtures/json/AcknowledgementMessage.json"), false);
            Assert.IsType<AcknowledgementMessage>(msg);
            msg = BaseMessage.Parse(FixtureStream("fixtures/json/DeathRecordVoidMessage.json"), false);
            Assert.IsType<DeathRecordVoidMessage>(msg);
            msg = BaseMessage.Parse(FixtureStream("fixtures/json/CauseOfDeathCodingMessage.json"), false);
            Assert.IsType<CauseOfDeathCodingMessage>(msg);
            msg = BaseMessage.Parse(FixtureStream("fixtures/json/DemographicsCodingMessage.json"), false);
            Assert.IsType<DemographicsCodingMessage>(msg);
            msg = BaseMessage.Parse(FixtureStream("fixtures/json/CauseOfDeathCodingUpdateMessage.json"), false);
            Assert.IsType<CauseOfDeathCodingUpdateMessage>(msg);
            msg = BaseMessage.Parse(FixtureStream("fixtures/json/DemographicsCodingUpdateMessage.json"), false);
            Assert.IsType<DemographicsCodingUpdateMessage>(msg);
            msg = BaseMessage.Parse(FixtureStream("fixtures/json/DeathRecordSubmissionMessage.json"), false);
            Assert.IsType<DeathRecordSubmissionMessage>(msg);
            msg = BaseMessage.Parse(FixtureStream("fixtures/json/DeathRecordUpdateMessage.json"), false);
            Assert.IsType<DeathRecordUpdateMessage>(msg);

            MessageParseException ex = Assert.Throws<MessageParseException>(() => BaseMessage.Parse(FixtureStream("fixtures/json/InvalidMessageType.json")));
            Assert.Equal("Unsupported message type: http://nchs.cdc.gov/vrdr_invalid_type", ex.Message);
            var responseMsg = ex.CreateExtractionErrorMessage();
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission", responseMsg.MessageDestination);
            Assert.Equal("nightingale", responseMsg.MessageSource);
            Assert.Equal("761dca08-259b-4dcd-aeb7-bb3c73fa30f2", responseMsg.FailedMessageId);
            Assert.Null(responseMsg.CertNo);
            Assert.Null(responseMsg.NCHSIdentifier);
            Assert.Null(responseMsg.StateAuxiliaryId);

            ex = Assert.Throws<MessageParseException>(() => BaseMessage.Parse(FixtureStream("fixtures/json/MissingMessageType.json")));
            Assert.Equal("Message type was missing from MessageHeader", ex.Message);
            responseMsg = ex.CreateExtractionErrorMessage();
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission", responseMsg.MessageDestination);
            Assert.Equal("nightingale", responseMsg.MessageSource);
            Assert.Equal("761dca08-259b-4dcd-aeb7-bb3c73fa30f2", responseMsg.FailedMessageId);
            Assert.Null(responseMsg.CertNo);
            Assert.Null(responseMsg.NCHSIdentifier);
            Assert.Null(responseMsg.StateAuxiliaryId);

            ex = Assert.Throws<MessageParseException>(() => BaseMessage.Parse(FixtureStream("fixtures/json/EmptyMessage.json")));
            Assert.Equal("Failed to find a Bundle Entry containing a Resource of type MessageHeader", ex.Message);
            responseMsg = ex.CreateExtractionErrorMessage();
            Assert.Null(responseMsg.MessageDestination);
            Assert.Null(responseMsg.MessageSource);
            Assert.Null(responseMsg.FailedMessageId);
            Assert.Null(responseMsg.CertNo);
            Assert.Null(responseMsg.NCHSIdentifier);
            Assert.Null(responseMsg.StateAuxiliaryId);

            ex = Assert.Throws<MessageParseException>(() => BaseMessage.Parse(FixtureStream("fixtures/json/Empty.json")));
            Assert.Equal("The FHIR Bundle must be of type message, not null", ex.Message);
            responseMsg = ex.CreateExtractionErrorMessage();
            Assert.Null(responseMsg.MessageDestination);
            Assert.Null(responseMsg.MessageSource);
            Assert.Null(responseMsg.FailedMessageId);
            Assert.Null(responseMsg.CertNo);
            Assert.Null(responseMsg.NCHSIdentifier);
            Assert.Null(responseMsg.StateAuxiliaryId);
        }

        [Fact]
        public void CreateExtractionErrorForMessage()
        {
            DeathRecordSubmissionMessage submission = BaseMessage.Parse<DeathRecordSubmissionMessage>(FixtureStream("fixtures/json/DeathRecordSubmissionMessage.json"));
            ExtractionErrorMessage err = new ExtractionErrorMessage(submission);
            Assert.Equal("http://nchs.cdc.gov/vrdr_extraction_error", err.MessageType);
            Assert.Equal(submission.MessageId, err.FailedMessageId);
            Assert.Equal(submission.MessageSource, err.MessageDestination);
            Assert.Equal(submission.StateAuxiliaryId, err.StateAuxiliaryId);
            Assert.Equal(submission.CertNo, err.CertNo);
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
            Assert.Null(err.CertNo);
            Assert.Null(err.StateAuxiliaryId);
            Assert.Null(err.NCHSIdentifier);
            Assert.Empty(err.Issues);
        }

        [Fact]
        public void CreateExtractionErrorFromJson()
        {
            ExtractionErrorMessage err = BaseMessage.Parse<ExtractionErrorMessage>(FixtureStream("fixtures/json/ExtractionErrorMessage.json"));
            Assert.Equal("http://nchs.cdc.gov/vrdr_extraction_error", err.MessageType);
            Assert.Equal((uint)1, err.CertNo);
            Assert.Equal("42", err.StateAuxiliaryId);
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
            var builder = new CauseOfDeathEntityAxisBuilder();
            var list = builder.ToCauseOfDeathEntityAxis();
            Assert.Empty(list);
            Exception ex = Assert.Throws<System.ArgumentException>(() => builder.Add("foo", "1", "bar"));
            Assert.Equal("The value of the line argument must be a number, found: foo", ex.Message);
            ex = Assert.Throws<System.ArgumentException>(() => builder.Add("1", "baz", "bar"));
            Assert.Equal("The value of the position argument must be a number, found: baz", ex.Message);
            Assert.Empty(list);
            builder.Add("6", "1", "A047");
            builder.Add("4", "1", "J189");
            builder.Add("3", "1", "A419");
            builder.Add("2", "3", "N19");
            builder.Add("2", "2", "R579");
            builder.Add("2", "1", "J960");
            builder.Add("1", "1", "R688");
            builder.Add("1", "2", "   "); // should be skipped
            builder.Add("1", "3", ""); // should be skipped
            builder.Add("1", "4", null); // should be skipped
            list = builder.ToCauseOfDeathEntityAxis();
            Assert.Equal(5, list.Count);
            var entry = list[0];
            Assert.Equal("1", entry.LineNumber);
            Assert.Equal(1, (int)entry.AssignedCodes.Count);
            Assert.Equal("R688", entry.AssignedCodes[0]);
            entry = list[1];
            Assert.Equal("2", entry.LineNumber);
            Assert.Equal(3, (int)entry.AssignedCodes.Count);
            Assert.Equal("J960", entry.AssignedCodes[0]);
            Assert.Equal("R579", entry.AssignedCodes[1]);
            Assert.Equal("N19", entry.AssignedCodes[2]);
            entry = list[2];
            Assert.Equal("3", entry.LineNumber);
            Assert.Equal(1, (int)entry.AssignedCodes.Count);
            Assert.Equal("A419", entry.AssignedCodes[0]);
            entry = list[3];
            Assert.Equal("4", entry.LineNumber);
            Assert.Equal(1, (int)entry.AssignedCodes.Count);
            Assert.Equal("J189", entry.AssignedCodes[0]);
            entry = list[4];
            Assert.Equal("6", entry.LineNumber);
            Assert.Equal(1, (int)entry.AssignedCodes.Count);
            Assert.Equal("A047", entry.AssignedCodes[0]);
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
