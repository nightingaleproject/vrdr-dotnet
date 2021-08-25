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
            Assert.Null(submission.CertificateNumber);
            Assert.Null(submission.StateAuxiliaryIdentifier);
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
            Assert.Equal((uint)1, submission.CertificateNumber);
            Assert.Equal((uint)2018, submission.DeathYear);
            Assert.Equal("42", submission.StateAuxiliaryIdentifier);
            Assert.Equal("2018MA000001", submission.NCHSIdentifier);

            record = (DeathRecord)JSONRecords[0];
            submission = new DeathRecordSubmission(record);
            Assert.NotNull(submission.DeathRecord);
            Assert.Equal("2018-02-20T16:48:06-05:00", submission.DeathRecord.DateOfDeathPronouncement);
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission", submission.MessageType);
            Assert.Equal(10,10);
            Assert.Equal((uint)1, submission.CertificateNumber);
            Assert.Equal((uint)2018, submission.DeathYear);
            Assert.Equal("42", submission.StateAuxiliaryIdentifier);
            Assert.Equal("2018MA000001", submission.NCHSIdentifier);

            record = null;
            submission = new DeathRecordSubmission(record);
            Assert.Null(submission.DeathRecord);
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission", submission.MessageType);
            Assert.Null(submission.CertificateNumber);
            Assert.Null(submission.StateAuxiliaryIdentifier);
            Assert.Null(submission.NCHSIdentifier);

            record = (DeathRecord)JSONRecords[1]; // no ids in this record
            submission = new DeathRecordSubmission(record);
            Assert.NotNull(submission.DeathRecord);
            Assert.Equal("2018-02-20T16:48:06-05:00", submission.DeathRecord.DateOfDeathPronouncement);
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission", submission.MessageType);
            Assert.Null(submission.CertificateNumber);
            Assert.Null(submission.StateAuxiliaryIdentifier);
            Assert.Null(submission.NCHSIdentifier);
        }

        [Fact]
        public void CreateSubmissionFromJSON()
        {
            DeathRecordSubmission submission = BaseMessage.Parse<DeathRecordSubmission>(FixtureStream("fixtures/json/DeathRecordSubmission.json"));
            Assert.Equal("2018-02-20T16:48:06-05:00", submission.DeathRecord.DateOfDeathPronouncement);
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission", submission.MessageType);
            Assert.Equal("2018MA000001", submission.NCHSIdentifier);
            Assert.Equal((uint)1, submission.CertificateNumber);
            Assert.Equal((uint)2018, submission.DeathYear);
            Assert.Equal("42", submission.StateAuxiliaryIdentifier);
            Assert.Equal(submission.DeathJurisdictionID, submission.DeathRecord.DeathLocationJurisdiction);

            submission = BaseMessage.Parse<DeathRecordSubmission>(FixtureStream("fixtures/json/DeathRecordSubmissionNoIdentifiers.json"));
            Assert.Equal("2018-02-20T16:48:06-05:00", submission.DeathRecord.DateOfDeathPronouncement);
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission", submission.MessageType);
            Assert.Null(submission.CertificateNumber);
            Assert.Null(submission.StateAuxiliaryIdentifier);
            Assert.Null(submission.NCHSIdentifier);

            MessageParseException ex = Assert.Throws<MessageParseException>(() => BaseMessage.Parse(FixtureStream("fixtures/json/EmptySubmission.json")));
            Assert.Equal("Error processing DeathRecord entry in the message: Failed to find a Bundle Entry containing a Resource of type Bundle", ex.Message);
            ExtractionErrorMessage errMsg = ex.CreateExtractionErrorMessage();
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission", errMsg.MessageSource);
            Assert.Equal("nightingale", errMsg.MessageDestination);
            Assert.Equal("a9d66d2e-2480-4e8d-bab3-4e4c761da1b7", errMsg.FailedMessageId);
            Assert.Equal("2018MA000001", errMsg.NCHSIdentifier);
            Assert.Equal((uint)1, errMsg.CertificateNumber);
            Assert.Equal((uint)2018, errMsg.DeathYear);
            Assert.Equal("42", errMsg.StateAuxiliaryIdentifier);

            ex = Assert.Throws<MessageParseException>(() => BaseMessage.Parse<AckMessage>(FixtureStream("fixtures/json/DeathRecordSubmission.json")));
            Assert.Equal("The supplied message was of type VRDR.DeathRecordSubmission, expected VRDR.AckMessage or a subclass", ex.Message);
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
            Assert.Null(submission.CertificateNumber);
            Assert.Null(submission.StateAuxiliaryIdentifier);
            Assert.Null(submission.NCHSIdentifier);
        }

        [Fact]
        public void CreateUpdateFromDeathRecord()
        {
            DeathRecordUpdate update = new DeathRecordUpdate((DeathRecord)XMLRecords[0]);
            Assert.NotNull(update.DeathRecord);
            Assert.Equal("2018-02-20T16:48:06-05:00", update.DeathRecord.DateOfDeathPronouncement);
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission_update", update.MessageType);
            Assert.Equal((uint)1, update.CertificateNumber);
            Assert.Equal((uint)2018, update.DeathYear);
            Assert.Equal("42", update.StateAuxiliaryIdentifier);
            Assert.Equal("2018MA000001", update.NCHSIdentifier);

            update = new DeathRecordUpdate((DeathRecord)JSONRecords[1]); // no ids in this death record
            Assert.NotNull(update.DeathRecord);
            Assert.Equal("2018-02-20T16:48:06-05:00", update.DeathRecord.DateOfDeathPronouncement);
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission_update", update.MessageType);
            Assert.Null(update.CertificateNumber);
            Assert.Null(update.StateAuxiliaryIdentifier);
            Assert.Null(update.NCHSIdentifier);
        }

        [Fact]
        public void CreateUpdateFromJSON()
        {
            DeathRecordUpdate update = BaseMessage.Parse<DeathRecordUpdate>(FixtureStream("fixtures/json/DeathRecordUpdate.json"));
            Assert.Equal("2018-02-20T16:48:06-05:00", update.DeathRecord.DateOfDeathPronouncement);
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission_update", update.MessageType);
            Assert.Equal("2018MA000001", update.NCHSIdentifier);
            Assert.Equal((uint)1, update.CertificateNumber);
            Assert.Equal((uint)2018, update.DeathYear);
            Assert.Equal("42", update.StateAuxiliaryIdentifier);

            update = BaseMessage.Parse<DeathRecordUpdate>(FixtureStream("fixtures/json/DeathRecordUpdateNoIdentifiers.json"));
            Assert.Equal("2018-02-20T16:48:06-05:00", update.DeathRecord.DateOfDeathPronouncement);
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission_update", update.MessageType);
            Assert.Null(update.CertificateNumber);
            Assert.Null(update.StateAuxiliaryIdentifier);
            Assert.Null(update.NCHSIdentifier);
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
            Assert.Equal(submission.StateAuxiliaryIdentifier, ack.StateAuxiliaryIdentifier);
            Assert.Equal(submission.CertificateNumber, ack.CertificateNumber);
            Assert.Equal(submission.NCHSIdentifier, ack.NCHSIdentifier);

            submission = null;
            ack = new AckMessage(submission);
            Assert.Equal("http://nchs.cdc.gov/vrdr_acknowledgement", ack.MessageType);
            Assert.Null(ack.AckedMessageId);
            Assert.Null(ack.MessageDestination);
            Assert.Null(ack.MessageSource);
            Assert.Null(ack.CertificateNumber);
            Assert.Null(ack.StateAuxiliaryIdentifier);
            Assert.Null(ack.NCHSIdentifier);

            submission = new DeathRecordSubmission();
            ack = new AckMessage(submission);
            Assert.Equal("http://nchs.cdc.gov/vrdr_acknowledgement", ack.MessageType);
            Assert.Equal(submission.MessageId, ack.AckedMessageId);
            Assert.Equal(submission.MessageSource, ack.MessageDestination);
            Assert.Equal(submission.MessageDestination, ack.MessageSource);
            Assert.Equal(submission.StateAuxiliaryIdentifier, ack.StateAuxiliaryIdentifier);
            Assert.Equal(submission.CertificateNumber, ack.CertificateNumber);
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
            Assert.Equal((uint)1, ack.CertificateNumber);
            Assert.Equal((uint)2018, ack.DeathYear);
            Assert.Equal("42", ack.StateAuxiliaryIdentifier);
        }

        [Fact]
        public void CreateAckFromXML()
        {
            AckMessage ack = BaseMessage.Parse<AckMessage>(FixtureStream("fixtures/xml/AckMessage.xml"));
            Assert.Equal("http://nchs.cdc.gov/vrdr_acknowledgement", ack.MessageType);
            Assert.Equal("a9d66d2e-2480-4e8d-bab3-4e4c761da1b7", ack.AckedMessageId);
            Assert.Equal("nightingale", ack.MessageDestination);
            Assert.Equal("2018MA000001", ack.NCHSIdentifier);
            Assert.Equal((uint)1, ack.CertificateNumber);
            Assert.Equal((uint)2018, ack.DeathYear);
            Assert.Equal("42", ack.StateAuxiliaryIdentifier);
        }

        [Fact]
        public void CreateCodingResponseFromJSON()
        {
            CodingResponseMessage message = BaseMessage.Parse<CodingResponseMessage>(FixtureStream("fixtures/json/CauseOfDeathCodingMessage.json"));
            Assert.Equal("http://nchs.cdc.gov/vrdr_coding", message.MessageType);
            Assert.Equal("destination", message.MessageDestination);
            Assert.Equal((uint)1, message.CertificateNumber);
            Assert.Equal((uint)2018, message.DeathYear);
            Assert.Equal("42", message.StateAuxiliaryIdentifier);
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
            Assert.Equal(CodingResponseMessage.MannerOfDeathEnum.Accident, message.MannerOfDeath);
            Assert.Equal("5", message.IntentionalReject);
            Assert.Equal(CodingResponseMessage.ACMESystemRejectEnum.ACMEReject, message.ACMESystemRejectCodes);
            Assert.Equal(CodingResponseMessage.PlaceOfInjuryEnum.Home, message.PlaceOfInjury);
        }

        [Fact]
        public void CreateCodingUpdateFromJSON()
        {
            CodingUpdateMessage message = BaseMessage.Parse<CodingUpdateMessage>(FixtureStream("fixtures/json/CodingUpdateMessage.json"));
            Assert.Equal("http://nchs.cdc.gov/vrdr_coding_update", message.MessageType);
            Assert.Equal("destination", message.MessageDestination);
            Assert.Equal((uint)1, message.CertificateNumber);
            Assert.Equal((uint)2018, message.DeathYear);
            Assert.Equal("42", message.StateAuxiliaryIdentifier);
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
        public void CreateCodingResponse()
        {
            CodingResponseMessage message = new CodingResponseMessage("destination", "http://nchs.cdc.gov/vrdr_submission");
            Assert.Equal("http://nchs.cdc.gov/vrdr_coding", message.MessageType);
            Assert.Equal("destination", message.MessageDestination);

            Assert.Null(message.CertificateNumber);
            message.CertificateNumber = 10;
            Assert.Equal((uint)10, message.CertificateNumber);

            Assert.Null(message.StateAuxiliaryIdentifier);
            message.StateAuxiliaryIdentifier = "id101010";
            Assert.Equal("id101010", message.StateAuxiliaryIdentifier);

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

            Assert.Null(message.PlaceOfInjury);
            message.PlaceOfInjury = CodingResponseMessage.PlaceOfInjuryEnum.Home;
            Assert.Equal(CodingResponseMessage.PlaceOfInjuryEnum.Home, message.PlaceOfInjury);

            Assert.Null(message.OtherSpecifiedPlace);
            message.OtherSpecifiedPlace = "Unique Location";
            Assert.Equal("Unique Location", message.OtherSpecifiedPlace);

            Assert.Null(message.DeathJurisdictionID);
            message.DeathJurisdictionID = "NH";
            Assert.Equal("NH", message.DeathJurisdictionID);
            Assert.Equal("2019NH000010", message.NCHSIdentifier);

            Assert.Null(message.IntentionalReject);
            message.IntentionalReject = "5";
            Assert.Equal("5", message.IntentionalReject);

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
        public void CreateCodingUpdate()
        {
            CodingUpdateMessage message = new CodingUpdateMessage("destination", "http://nchs.cdc.gov/vrdr_submission");
            Assert.Equal("http://nchs.cdc.gov/vrdr_coding_update", message.MessageType);
            Assert.Equal("destination", message.MessageDestination);

            Assert.Null(message.CertificateNumber);
            message.CertificateNumber = 10;
            Assert.Equal((uint)10, message.CertificateNumber);

            Assert.Null(message.StateAuxiliaryIdentifier);
            message.StateAuxiliaryIdentifier = "id101010";
            Assert.Equal("id101010", message.StateAuxiliaryIdentifier);

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
        public void CreateVoidMessage()
        {
            VoidMessage message = new VoidMessage();
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission_void", message.MessageType);
            Assert.Null(message.CertificateNumber);
            message.CertificateNumber = 11;
            Assert.Equal((uint)11, message.CertificateNumber);
            Assert.Null(message.StateAuxiliaryIdentifier);
            message.StateAuxiliaryIdentifier = "bar";
            Assert.Equal("bar", message.StateAuxiliaryIdentifier);
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
            Assert.Equal(voidMessage.StateAuxiliaryIdentifier, ack.StateAuxiliaryIdentifier);
            Assert.Equal(voidMessage.CertificateNumber, ack.CertificateNumber);
            Assert.Equal(voidMessage.NCHSIdentifier, ack.NCHSIdentifier);
            Assert.Equal(voidMessage.BlockCount, ack.BlockCount);

            voidMessage = null;
            ack = new AckMessage(voidMessage);
            Assert.Equal("http://nchs.cdc.gov/vrdr_acknowledgement", ack.MessageType);
            Assert.Null(ack.AckedMessageId);
            Assert.Null(ack.MessageDestination);
            Assert.Null(ack.MessageSource);
            Assert.Null(ack.CertificateNumber);
            Assert.Null(ack.StateAuxiliaryIdentifier);
            Assert.Null(ack.NCHSIdentifier);
            Assert.Null(ack.BlockCount);

            voidMessage = new VoidMessage();
            ack = new AckMessage(voidMessage);
            Assert.Equal("http://nchs.cdc.gov/vrdr_acknowledgement", ack.MessageType);
            Assert.Equal(voidMessage.MessageId, ack.AckedMessageId);
            Assert.Equal(voidMessage.MessageSource, ack.MessageDestination);
            Assert.Equal(voidMessage.MessageDestination, ack.MessageSource);
            Assert.Equal(voidMessage.StateAuxiliaryIdentifier, ack.StateAuxiliaryIdentifier);
            Assert.Equal(voidMessage.CertificateNumber, ack.CertificateNumber);
            Assert.Equal(voidMessage.NCHSIdentifier, ack.NCHSIdentifier);
            Assert.Equal(voidMessage.BlockCount, ack.BlockCount);
        }

        [Fact]
        public void CreateVoidMessageFromJson()
        {
            VoidMessage message = BaseMessage.Parse<VoidMessage>(FixtureStream("fixtures/json/VoidMessage.json"));
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission_void", message.MessageType);
            Assert.Equal((uint)1, message.CertificateNumber);
            Assert.Equal((uint)10, message.BlockCount);
            Assert.Equal("42", message.StateAuxiliaryIdentifier);
            Assert.Equal("2018MA000001", message.NCHSIdentifier);
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission", message.MessageDestination);
            Assert.Equal("nightingale", message.MessageSource);

            message = BaseMessage.Parse<VoidMessage>(FixtureStream("fixtures/json/VoidMessageNoIdentifiers.json"));
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission_void", message.MessageType);
            Assert.Null(message.CertificateNumber);
            Assert.Null(message.StateAuxiliaryIdentifier);
            Assert.Null(message.NCHSIdentifier);
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission", message.MessageDestination);
            Assert.Equal("nightingale", message.MessageSource);
        }

        [Fact]
        public void CreateVoidForDocument()
        {
            VoidMessage message = new VoidMessage((DeathRecord)XMLRecords[0]);
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission_void", message.MessageType);
            Assert.Equal((uint)1, message.CertificateNumber);
            Assert.Equal("42", message.StateAuxiliaryIdentifier);
            Assert.Equal("2018MA000001", message.NCHSIdentifier);
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission", message.MessageDestination);
            Assert.Null(message.MessageSource);

            message = new VoidMessage(null);
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission_void", message.MessageType);
            Assert.Null(message.CertificateNumber);
            Assert.Null(message.StateAuxiliaryIdentifier);
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
            Assert.Null(responseMsg.CertificateNumber);
            Assert.Null(responseMsg.NCHSIdentifier);
            Assert.Null(responseMsg.StateAuxiliaryIdentifier);

            ex = Assert.Throws<MessageParseException>(() => BaseMessage.Parse(FixtureStream("fixtures/json/MissingMessageType.json")));
            Assert.Equal("Message type was missing from MessageHeader", ex.Message);
            responseMsg = ex.CreateExtractionErrorMessage();
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission", responseMsg.MessageDestination);
            Assert.Equal("nightingale", responseMsg.MessageSource);
            Assert.Equal("761dca08-259b-4dcd-aeb7-bb3c73fa30f2", responseMsg.FailedMessageId);
            Assert.Null(responseMsg.CertificateNumber);
            Assert.Null(responseMsg.NCHSIdentifier);
            Assert.Null(responseMsg.StateAuxiliaryIdentifier);

            ex = Assert.Throws<MessageParseException>(() => BaseMessage.Parse(FixtureStream("fixtures/json/EmptyMessage.json")));
            Assert.Equal("Failed to find a Bundle Entry containing a Resource of type MessageHeader", ex.Message);
            responseMsg = ex.CreateExtractionErrorMessage();
            Assert.Null(responseMsg.MessageDestination);
            Assert.Null(responseMsg.MessageSource);
            Assert.Null(responseMsg.FailedMessageId);
            Assert.Null(responseMsg.CertificateNumber);
            Assert.Null(responseMsg.NCHSIdentifier);
            Assert.Null(responseMsg.StateAuxiliaryIdentifier);

            ex = Assert.Throws<MessageParseException>(() => BaseMessage.Parse(FixtureStream("fixtures/json/Empty.json")));
            Assert.Equal("The FHIR Bundle must be of type message, not null", ex.Message);
            responseMsg = ex.CreateExtractionErrorMessage();
            Assert.Null(responseMsg.MessageDestination);
            Assert.Null(responseMsg.MessageSource);
            Assert.Null(responseMsg.FailedMessageId);
            Assert.Null(responseMsg.CertificateNumber);
            Assert.Null(responseMsg.NCHSIdentifier);
            Assert.Null(responseMsg.StateAuxiliaryIdentifier);
        }

        [Fact]
        public void CreateExtractionErrorForMessage()
        {
            DeathRecordSubmission submission = BaseMessage.Parse<DeathRecordSubmission>(FixtureStream("fixtures/json/DeathRecordSubmission.json"));
            ExtractionErrorMessage err = new ExtractionErrorMessage(submission);
            Assert.Equal("http://nchs.cdc.gov/vrdr_extraction_error", err.MessageType);
            Assert.Equal(submission.MessageId, err.FailedMessageId);
            Assert.Equal(submission.MessageSource, err.MessageDestination);
            Assert.Equal(submission.StateAuxiliaryIdentifier, err.StateAuxiliaryIdentifier);
            Assert.Equal(submission.CertificateNumber, err.CertificateNumber);
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
            Assert.Null(err.CertificateNumber);
            Assert.Null(err.StateAuxiliaryIdentifier);
            Assert.Null(err.NCHSIdentifier);
            Assert.Empty(err.Issues);
        }

        [Fact]
        public void CreateExtractionErrorFromJson()
        {
            ExtractionErrorMessage err = BaseMessage.Parse<ExtractionErrorMessage>(FixtureStream("fixtures/json/ExtractionErrorMessage.json"));
            Assert.Equal("http://nchs.cdc.gov/vrdr_extraction_error", err.MessageType);
            Assert.Equal((uint)1, err.CertificateNumber);
            Assert.Equal("42", err.StateAuxiliaryIdentifier);
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
