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
            XMLRecords.Add(new DeathRecord(File.ReadAllText(FixturePath("fixtures/xml/1.xml"))));
            JSONRecords = new ArrayList();
            JSONRecords.Add(new DeathRecord(File.ReadAllText(FixturePath("fixtures/json/1.json"))));
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
            Assert.Null(submission.StateIdentifier);
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
            Assert.Equal("1", submission.CertificateNumber);
            Assert.Equal("42", submission.StateIdentifier);
            Assert.Equal("2018MA000001", submission.NCHSIdentifier);

            record = null;
            submission = new DeathRecordSubmission(record);
            Assert.Null(submission.DeathRecord);
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission", submission.MessageType);
            Assert.Null(submission.CertificateNumber);
            Assert.Null(submission.StateIdentifier);
            Assert.Null(submission.NCHSIdentifier);
        }

        [Fact]
        public void CreateSubmissionFromJSON()
        {
            DeathRecordSubmission submission = BaseMessage.Parse<DeathRecordSubmission>(FixtureStream("fixtures/json/DeathRecordSubmission.json"));
            Assert.Equal("2018-02-20T16:48:06-05:00", submission.DeathRecord.DateOfDeathPronouncement);
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission", submission.MessageType);
            Assert.Equal("2019MA101010", submission.NCHSIdentifier);
            Assert.Equal("101010", submission.CertificateNumber);
            Assert.Equal("12345", submission.StateIdentifier);

            Exception ex = Assert.Throws<System.ArgumentException>(() => BaseMessage.Parse(FixtureStream("fixtures/json/EmptySubmission.json")));
            Assert.Equal("Failed to find a Bundle Entry containing a Resource of type Bundle", ex.Message);

            ex = Assert.Throws<System.ArgumentException>(() => BaseMessage.Parse<AckMessage>(FixtureStream("fixtures/json/DeathRecordSubmission.json")));
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
            Assert.Null(submission.StateIdentifier);
            Assert.Null(submission.NCHSIdentifier);
        }

        [Fact]
        public void CreateUpdateFromDeathRecord()
        {
            DeathRecordUpdate submission = new DeathRecordUpdate((DeathRecord)XMLRecords[0]);
            Assert.NotNull(submission.DeathRecord);
            Assert.Equal("2018-02-20T16:48:06-05:00", submission.DeathRecord.DateOfDeathPronouncement);
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission_update", submission.MessageType);
            Assert.Equal("1", submission.CertificateNumber);
            Assert.Equal("42", submission.StateIdentifier);
            Assert.Equal("2018MA000001", submission.NCHSIdentifier);
        }

        [Fact]
        public void CreateUpdateFromJSON()
        {
            DeathRecordUpdate submission = BaseMessage.Parse<DeathRecordUpdate>(FixtureStream("fixtures/json/DeathRecordUpdate.json"));
            Assert.Equal("2018-02-20T16:48:06-05:00", submission.DeathRecord.DateOfDeathPronouncement);
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission_update", submission.MessageType);
            Assert.Equal("2019MA101010", submission.NCHSIdentifier);
            Assert.Equal("101010", submission.CertificateNumber);
            Assert.Equal("12345", submission.StateIdentifier);
        }

        [Fact]
        public void CreateAckForMessage()
        {
            DeathRecordSubmission submission = BaseMessage.Parse<DeathRecordSubmission>(FixtureStream("fixtures/json/DeathRecordSubmission.json"));
            AckMessage ack = new AckMessage(submission);
            Assert.Equal("http://nchs.cdc.gov/vrdr_acknowledgement", ack.MessageType);
            Assert.Equal(submission.MessageId, ack.AckedMessageId);
            Assert.Equal(submission.MessageSource, ack.MessageDestination);
            Assert.Equal(submission.StateIdentifier, ack.StateIdentifier);
            Assert.Equal(submission.CertificateNumber, ack.CertificateNumber);
            Assert.Equal(submission.NCHSIdentifier, ack.NCHSIdentifier);

            submission = null;
            ack = new AckMessage(submission);
            Assert.Equal("http://nchs.cdc.gov/vrdr_acknowledgement", ack.MessageType);
            Assert.Null(ack.AckedMessageId);
            Assert.Null(ack.MessageDestination);
            Assert.Null(ack.CertificateNumber);
            Assert.Null(ack.StateIdentifier);
            Assert.Null(ack.NCHSIdentifier);

            submission = new DeathRecordSubmission();
            ack = new AckMessage(submission);
            Assert.Equal("http://nchs.cdc.gov/vrdr_acknowledgement", ack.MessageType);
            Assert.Equal(submission.MessageId, ack.AckedMessageId);
            Assert.Equal(submission.MessageSource, ack.MessageDestination);
            Assert.Equal(submission.StateIdentifier, ack.StateIdentifier);
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
            Assert.Equal("2019MA101010", ack.NCHSIdentifier);
            Assert.Equal("101010", ack.CertificateNumber);
            Assert.Equal("12345", ack.StateIdentifier);
        }

        [Fact]
        public void CreateAckFromXML()
        {
            AckMessage ack = BaseMessage.Parse<AckMessage>(FixtureStream("fixtures/xml/AckMessage.xml"));
            Assert.Equal("http://nchs.cdc.gov/vrdr_acknowledgement", ack.MessageType);
            Assert.Equal("a9d66d2e-2480-4e8d-bab3-4e4c761da1b7", ack.AckedMessageId);
            Assert.Equal("nightingale", ack.MessageDestination);
            Assert.Equal("2019MA101010", ack.NCHSIdentifier);
            Assert.Equal("101010", ack.CertificateNumber);
            Assert.Equal("12345", ack.StateIdentifier);
        }

        [Fact]
        public void CreateCodingResponseFromJSON()
        {
            CodingResponseMessage message = BaseMessage.Parse<CodingResponseMessage>(FixtureStream("fixtures/json/CauseOfDeathCodingMessage.json"));
            Assert.Equal("http://nchs.cdc.gov/vrdr_coding", message.MessageType);
            Assert.Equal("destination", message.MessageDestination);
            Assert.Equal("cert101010", message.CertificateNumber);
            Assert.Equal("id101010", message.StateIdentifier);
            Assert.Equal("2019MA101010", message.NCHSIdentifier);
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
            Assert.Equal("DEATH CERT LINE 1 TEXT", entityAxisEntries[0].DeathCertificateText);
            Assert.Equal("abcde", entityAxisEntries[0].CauseOfDeathConditionId);
            Assert.Equal(2, (int)entityAxisEntries[0].AssignedCodes.Count);
            Assert.Equal("code1_1", entityAxisEntries[0].AssignedCodes[0]);
            Assert.Equal("code1_2", entityAxisEntries[0].AssignedCodes[1]);
            Assert.Equal("DEATH CERT LINE 2 TEXT", entityAxisEntries[1].DeathCertificateText);
            Assert.Equal("xyzzy", entityAxisEntries[1].CauseOfDeathConditionId);
            Assert.Equal(1, (int)entityAxisEntries[1].AssignedCodes.Count);
            Assert.Equal("code2_1", entityAxisEntries[1].AssignedCodes[0]);
        }

        [Fact]
        public void CreateCodingUpdateFromJSON()
        {
            CodingUpdateMessage message = BaseMessage.Parse<CodingUpdateMessage>(FixtureStream("fixtures/json/CodingUpdateMessage.json"));
            Assert.Equal("http://nchs.cdc.gov/vrdr_coding_update", message.MessageType);
            Assert.Equal("destination", message.MessageDestination);
            Assert.Equal("cert101010", message.CertificateNumber);
            Assert.Equal("id101010", message.StateIdentifier);
            Assert.Equal("2019MA101010", message.NCHSIdentifier);
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
            Assert.Equal("DEATH CERT LINE 1 TEXT", entityAxisEntries[0].DeathCertificateText);
            Assert.Equal("abcde", entityAxisEntries[0].CauseOfDeathConditionId);
            Assert.Equal(2, (int)entityAxisEntries[0].AssignedCodes.Count);
            Assert.Equal("code1_1", entityAxisEntries[0].AssignedCodes[0]);
            Assert.Equal("code1_2", entityAxisEntries[0].AssignedCodes[1]);
            Assert.Equal("DEATH CERT LINE 2 TEXT", entityAxisEntries[1].DeathCertificateText);
            Assert.Equal("xyzzy", entityAxisEntries[1].CauseOfDeathConditionId);
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
            message.CertificateNumber = "cert101010";
            Assert.Equal("cert101010", message.CertificateNumber);

            Assert.Null(message.StateIdentifier);
            message.StateIdentifier = "id101010";
            Assert.Equal("id101010", message.StateIdentifier);
            
            Assert.Null(message.NCHSIdentifier);
            message.NCHSIdentifier = "2019MA101010";
            Assert.Equal("2019MA101010", message.NCHSIdentifier);

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
            var entry1 = new CauseOfDeathEntityAxisEntry("DEATH CERT LINE 1 TEXT", "abcde");
            entry1.AssignedCodes.Add("code1_1");
            entry1.AssignedCodes.Add("code1_2");
            entityAxisEntries.Add(entry1);
            var entry2 = new CauseOfDeathEntityAxisEntry("DEATH CERT LINE 2 TEXT", "xyzzy");
            entry2.AssignedCodes.Add("code2_1");
            entityAxisEntries.Add(entry2);
            message.CauseOfDeathEntityAxis = entityAxisEntries;
            entityAxisEntries = message.CauseOfDeathEntityAxis;
            Assert.Equal(2, (int)entityAxisEntries.Count);
            Assert.Equal("DEATH CERT LINE 1 TEXT", entityAxisEntries[0].DeathCertificateText);
            Assert.Equal("abcde", entityAxisEntries[0].CauseOfDeathConditionId);
            Assert.Equal(2, (int)entityAxisEntries[0].AssignedCodes.Count);
            Assert.Equal("code1_1", entityAxisEntries[0].AssignedCodes[0]);
            Assert.Equal("code1_2", entityAxisEntries[0].AssignedCodes[1]);
            Assert.Equal("DEATH CERT LINE 2 TEXT", entityAxisEntries[1].DeathCertificateText);
            Assert.Equal("xyzzy", entityAxisEntries[1].CauseOfDeathConditionId);
            Assert.Equal(1, (int)entityAxisEntries[1].AssignedCodes.Count);
            Assert.Equal("code2_1", entityAxisEntries[1].AssignedCodes[0]);
        }

        [Fact]
        public void CreateCodingUpdate()
        {
            CodingUpdateMessage message = new CodingUpdateMessage("destination", "http://nchs.cdc.gov/vrdr_submission");
            Assert.Equal("http://nchs.cdc.gov/vrdr_coding_update", message.MessageType);
            Assert.Equal("destination", message.MessageDestination);
            
            Assert.Null(message.CertificateNumber);
            message.CertificateNumber = "cert101010";
            Assert.Equal("cert101010", message.CertificateNumber);

            Assert.Null(message.StateIdentifier);
            message.StateIdentifier = "id101010";
            Assert.Equal("id101010", message.StateIdentifier);
            
            Assert.Null(message.NCHSIdentifier);
            message.NCHSIdentifier = "2019MA101010";
            Assert.Equal("2019MA101010", message.NCHSIdentifier);

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
            var entry1 = new CauseOfDeathEntityAxisEntry("DEATH CERT LINE 1 TEXT", "abcde");
            entry1.AssignedCodes.Add("code1_1");
            entry1.AssignedCodes.Add("code1_2");
            entityAxisEntries.Add(entry1);
            var entry2 = new CauseOfDeathEntityAxisEntry("DEATH CERT LINE 2 TEXT", "xyzzy");
            entry2.AssignedCodes.Add("code2_1");
            entityAxisEntries.Add(entry2);
            message.CauseOfDeathEntityAxis = entityAxisEntries;
            entityAxisEntries = message.CauseOfDeathEntityAxis;
            Assert.Equal(2, (int)entityAxisEntries.Count);
            Assert.Equal("DEATH CERT LINE 1 TEXT", entityAxisEntries[0].DeathCertificateText);
            Assert.Equal("abcde", entityAxisEntries[0].CauseOfDeathConditionId);
            Assert.Equal(2, (int)entityAxisEntries[0].AssignedCodes.Count);
            Assert.Equal("code1_1", entityAxisEntries[0].AssignedCodes[0]);
            Assert.Equal("code1_2", entityAxisEntries[0].AssignedCodes[1]);
            Assert.Equal("DEATH CERT LINE 2 TEXT", entityAxisEntries[1].DeathCertificateText);
            Assert.Equal("xyzzy", entityAxisEntries[1].CauseOfDeathConditionId);
            Assert.Equal(1, (int)entityAxisEntries[1].AssignedCodes.Count);
            Assert.Equal("code2_1", entityAxisEntries[1].AssignedCodes[0]);
        }

        [Fact]
        public void TestCauseOfDeathEntityAxisEntry()
        {
            var entry = new CauseOfDeathEntityAxisEntry("FooBarBaz", "id101010");
            Assert.Equal("FooBarBaz", entry.DeathCertificateText);
            Assert.Equal("id101010", entry.CauseOfDeathConditionId);
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
            message.CertificateNumber = "foo";
            Assert.Equal("foo", message.CertificateNumber);
            Assert.Null(message.StateIdentifier);
            message.StateIdentifier = "bar";
            Assert.Equal("bar", message.StateIdentifier);
            Assert.Null(message.NCHSIdentifier);
            message.NCHSIdentifier = "2019MA101010";
            Assert.Equal("2019MA101010", message.NCHSIdentifier);
        }

        [Fact]
        public void CreateVoidMessageFromJson()
        {
            VoidMessage message = BaseMessage.Parse<VoidMessage>(FixtureStream("fixtures/json/VoidMessage.json"));
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission_void", message.MessageType);
            Assert.Equal("foo", message.CertificateNumber);
            Assert.Equal("bar", message.StateIdentifier);
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission", message.MessageDestination);
            Assert.Equal("nightingale", message.MessageSource);
        }

        [Fact]
        public void CreateVoidForDocument()
        {
            VoidMessage message = new VoidMessage((DeathRecord)XMLRecords[0]);
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission_void", message.MessageType);
            Assert.Equal("1", message.CertificateNumber);
            Assert.Equal("42", message.StateIdentifier);
            Assert.Equal("2018MA000001", message.NCHSIdentifier);
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission", message.MessageDestination);
            Assert.Null(message.MessageSource);

            message = new VoidMessage(null);
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission_void", message.MessageType);
            Assert.Null(message.CertificateNumber);
            Assert.Null(message.StateIdentifier);
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
            Exception ex = Assert.Throws<System.ArgumentException>(() => BaseMessage.Parse(FixtureStream("fixtures/json/InvalidMessageType.json")));
            Assert.Equal("Unsupported message type: http://nchs.cdc.gov/vrdr_invalid_type", ex.Message);
            ex = Assert.Throws<System.ArgumentException>(() => BaseMessage.Parse(FixtureStream("fixtures/json/EmptyMessage.json")));
            Assert.Equal("Failed to find a Bundle Entry containing a Resource of type MessageHeader", ex.Message);
        }

        [Fact]
        public void CreateExtractionErrorForMessage()
        {
            DeathRecordSubmission submission = BaseMessage.Parse<DeathRecordSubmission>(FixtureStream("fixtures/json/DeathRecordSubmission.json"));
            ExtractionErrorMessage err = new ExtractionErrorMessage(submission);
            Assert.Equal("http://nchs.cdc.gov/vrdr_extraction_error", err.MessageType);
            Assert.Equal(submission.MessageId, err.FailedMessageId);
            Assert.Equal(submission.MessageSource, err.MessageDestination);
            Assert.Equal(submission.StateIdentifier, err.StateIdentifier);
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
            Assert.Null(err.StateIdentifier);
            Assert.Null(err.NCHSIdentifier);
            Assert.Empty(err.Issues);
        }

        [Fact]
        public void CreateExtractionErrorFromJson()
        {
            ExtractionErrorMessage err = BaseMessage.Parse<ExtractionErrorMessage>(FixtureStream("fixtures/json/ExtractionErrorMessage.json"));
            Assert.Equal("http://nchs.cdc.gov/vrdr_extraction_error", err.MessageType);
            Assert.Equal("101010", err.CertificateNumber);
            Assert.Equal("12345", err.StateIdentifier);
            Assert.Equal("2019MA101010", err.NCHSIdentifier);
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
            list = builder.ToCauseOfDeathEntityAxis();
            Assert.Equal(5, list.Count);
            var entry = list[0];
            Assert.Equal("1", entry.CauseOfDeathConditionId);
            Assert.Equal(1, (int)entry.AssignedCodes.Count);
            Assert.Equal("R688", entry.AssignedCodes[0]);
            entry = list[1];
            Assert.Equal("2", entry.CauseOfDeathConditionId);
            Assert.Equal(3, (int)entry.AssignedCodes.Count);
            Assert.Equal("J960", entry.AssignedCodes[0]);
            Assert.Equal("R579", entry.AssignedCodes[1]);
            Assert.Equal("N19", entry.AssignedCodes[2]);
            entry = list[2];
            Assert.Equal("3", entry.CauseOfDeathConditionId);
            Assert.Equal(1, (int)entry.AssignedCodes.Count);
            Assert.Equal("A419", entry.AssignedCodes[0]);
            entry = list[3];
            Assert.Equal("4", entry.CauseOfDeathConditionId);
            Assert.Equal(1, (int)entry.AssignedCodes.Count);
            Assert.Equal("J189", entry.AssignedCodes[0]);
            entry = list[4];
            Assert.Equal("6", entry.CauseOfDeathConditionId);
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
