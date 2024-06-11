using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Linq;
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
            Assert.Equal("2019-02-20T16:48:06", submission.DeathRecord.DateOfDeathPronouncement);
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission", submission.MessageType);
            Assert.Equal(10, 10);
            Assert.Equal((uint)182, submission.CertNo);
            Assert.Equal((uint)2019, submission.DeathYear);
            Assert.Equal("000000000042", submission.StateAuxiliaryId);
            Assert.Equal("2019YC000182", submission.NCHSIdentifier);

            record = (DeathRecord)JSONRecords[0];
            submission = new DeathRecordSubmissionMessage(record);
            Assert.NotNull(submission.DeathRecord);
            Assert.Equal("2018-02-20T16:48:06", submission.DeathRecord.DateOfDeathPronouncement);
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
            Assert.Equal("2019-02-20T16:48:06", submission.DeathRecord.DateOfDeathPronouncement);
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission", submission.MessageType);
            Assert.Null(submission.CertNo);
            Assert.Null(submission.StateAuxiliaryId);
            Assert.Null(submission.NCHSIdentifier);
        }

        [Fact]
        public void CreateMultipleDestinationsRecord()
        {
            BaseMessage multipleDestinations = BaseMessage.Parse(FixtureStream("fixtures/json/MultipleDestinationsMessage.json"));
            Assert.Equal("test_one,test_two", multipleDestinations.MessageDestination);
            Assert.Equal((new string[] {"test_one", "test_two"}).ToList(), multipleDestinations.MessageDestinations);
            multipleDestinations.MessageDestination = "test_three,test_four";
            Assert.Equal("test_three,test_four", multipleDestinations.MessageDestination);
            Assert.Equal((new string[] {"test_three", "test_four"}).ToList(), multipleDestinations.MessageDestinations);
            multipleDestinations.MessageDestinations = (new string[] {"test_five", "test_six"}).ToList();
            Assert.Equal("test_five,test_six", multipleDestinations.MessageDestination);
            Assert.Equal((new string[] {"test_five", "test_six"}).ToList(), multipleDestinations.MessageDestinations);
            multipleDestinations.MessageDestination = "test_seven";
            Assert.Equal("test_seven", multipleDestinations.MessageDestination);
            Assert.Equal((new string[] {"test_seven"}).ToList(), multipleDestinations.MessageDestinations);
        }

        [Fact]
        public void CreateSubmissionFromJSON()
        {
            DeathRecordSubmissionMessage submission = BaseMessage.Parse<DeathRecordSubmissionMessage>(FixtureStream("fixtures/json/DeathRecordSubmissionMessage.json"));
            Assert.Equal("2020-11-13T16:39:40", submission.DeathRecord.DateOfDeathPronouncement);
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission", submission.MessageType);
            Assert.Equal("2018NY123456", submission.NCHSIdentifier);
            Assert.Equal((uint)123456, submission.CertNo);
            Assert.Equal((uint)2018, submission.DeathYear);
            Assert.Equal("abcdef10", submission.StateAuxiliaryId);
            Assert.Equal(submission.JurisdictionId, submission.DeathRecord.DeathLocationJurisdiction);

            submission = BaseMessage.Parse<DeathRecordSubmissionMessage>(FixtureStream("fixtures/json/DeathRecordSubmissionNoIdentifiers.json"));
            Assert.Equal("2018-02-20T16:48:06", submission.DeathRecord.DateOfDeathPronouncement);
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission", submission.MessageType);
            Assert.Null(submission.CertNo);
            Assert.Null(submission.StateAuxiliaryId);
            Assert.Null(submission.NCHSIdentifier);

            MessageParseException ex = Assert.Throws<MessageParseException>(() => BaseMessage.Parse(FixtureStream("fixtures/json/EmptySubmission.json")));
            Assert.Equal("Error processing DeathRecord entry in the message: Failed to find a Bundle Entry containing a Resource of type Hl7.Fhir.Model.Bundle", ex.Message);
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
        public void ProtectedSetMessageBundle()
        {
            DeathRecordSubmissionMessage submission = new DeathRecordSubmissionMessage();
            Assert.NotNull(submission.MessageBundle);
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
            Assert.Equal("2019-02-20T16:48:06", update.DeathRecord.DateOfDeathPronouncement);
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission_update", update.MessageType);
            Assert.Equal((uint)182, update.CertNo);
            Assert.Equal((uint)2019, update.DeathYear);
            Assert.Equal("000000000042", update.StateAuxiliaryId);
            Assert.Equal("2019YC000182", update.NCHSIdentifier);

            update = new DeathRecordUpdateMessage((DeathRecord)JSONRecords[1]); // no ids in this death record (except jurisdiction id which is required)
            Assert.NotNull(update.DeathRecord);
            Assert.Equal("2019-02-20T16:48:06", update.DeathRecord.DateOfDeathPronouncement);
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission_update", update.MessageType);
            Assert.Null(update.CertNo);
            Assert.Null(update.StateAuxiliaryId);
            Assert.Null(update.NCHSIdentifier);
        }

        [Fact]
        public void CreateUpdateFromJSON()
        {
            DeathRecordUpdateMessage update = BaseMessage.Parse<DeathRecordUpdateMessage>(FixtureStream("fixtures/json/DeathRecordUpdateMessage.json"));
            Assert.Equal("2020-11-13T16:39:40", update.DeathRecord.DateOfDeathPronouncement);
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission_update", update.MessageType);
            Assert.Equal("2018NY123456", update.NCHSIdentifier);
            Assert.Equal((uint)123456, update.CertNo);
            Assert.Equal((uint)2018, update.DeathYear);
            Assert.Equal("abcdef10", update.StateAuxiliaryId);

            update = BaseMessage.Parse<DeathRecordUpdateMessage>(FixtureStream("fixtures/json/DeathRecordUpdateNoIdentifiers.json"));
            Assert.Equal("2018-02-20T16:48:06", update.DeathRecord.DateOfDeathPronouncement);
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
        public void CreateCODForMessage()
        {
            DeathRecordSubmissionMessage submission = BaseMessage.Parse<DeathRecordSubmissionMessage>(FixtureStream("fixtures/json/DeathRecordSubmissionMessage.json"));
            CauseOfDeathCodingMessage coding = new CauseOfDeathCodingMessage(submission);
            Assert.Equal("http://nchs.cdc.gov/vrdr_causeofdeath_coding", coding.MessageType);
            Assert.Equal(submission.MessageId, coding.CodedMessageId);
            Assert.Equal(submission.MessageSource, coding.MessageDestination);
            Assert.Equal(submission.MessageDestination, coding.MessageSource);
            Assert.Equal(submission.StateAuxiliaryId, coding.StateAuxiliaryId);
            Assert.Equal(submission.CertNo, coding.CertNo);
            Assert.Equal(submission.NCHSIdentifier, coding.NCHSIdentifier);

            submission = null;
            coding = new CauseOfDeathCodingMessage(submission);
            Assert.Equal("http://nchs.cdc.gov/vrdr_causeofdeath_coding", coding.MessageType);
            Assert.Null(coding.CodedMessageId);
            Assert.Null(coding.MessageDestination);
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission", coding.MessageSource);
            Assert.Null(coding.CertNo);
            Assert.Null(coding.StateAuxiliaryId);
            Assert.Null(coding.NCHSIdentifier);

            submission = new DeathRecordSubmissionMessage();
            coding = new CauseOfDeathCodingMessage(submission);
            Assert.Equal("http://nchs.cdc.gov/vrdr_causeofdeath_coding", coding.MessageType);
            Assert.Equal(submission.MessageId, coding.CodedMessageId);
            Assert.Equal(submission.MessageSource, coding.MessageDestination);
            Assert.Equal(submission.MessageDestination, coding.MessageSource);
            Assert.Equal(submission.StateAuxiliaryId, coding.StateAuxiliaryId);
            Assert.Equal(submission.CertNo, coding.CertNo);
            Assert.Equal(submission.NCHSIdentifier, coding.NCHSIdentifier);
        }

        [Fact]
        public void CreateDemographicForMessage()
        {
            DeathRecordSubmissionMessage submission = BaseMessage.Parse<DeathRecordSubmissionMessage>(FixtureStream("fixtures/json/DeathRecordSubmissionMessage.json"));
            DemographicsCodingMessage coding = new DemographicsCodingMessage(submission);
            Assert.Equal("http://nchs.cdc.gov/vrdr_demographics_coding", coding.MessageType);
            Assert.Equal(submission.MessageId, coding.CodedMessageId);
            Assert.Equal(submission.MessageSource, coding.MessageDestination);
            Assert.Equal(submission.MessageDestination, coding.MessageSource);
            Assert.Equal(submission.StateAuxiliaryId, coding.StateAuxiliaryId);
            Assert.Equal(submission.CertNo, coding.CertNo);
            Assert.Equal(submission.NCHSIdentifier, coding.NCHSIdentifier);

            submission = null;
            coding = new DemographicsCodingMessage(submission);
            Assert.Equal("http://nchs.cdc.gov/vrdr_demographics_coding", coding.MessageType);
            Assert.Null(coding.CodedMessageId);
            Assert.Null(coding.MessageDestination);
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission", coding.MessageSource);
            Assert.Null(coding.CertNo);
            Assert.Null(coding.StateAuxiliaryId);
            Assert.Null(coding.NCHSIdentifier);

            submission = new DeathRecordSubmissionMessage();
            coding = new DemographicsCodingMessage(submission);
            Assert.Equal("http://nchs.cdc.gov/vrdr_demographics_coding", coding.MessageType);
            Assert.Equal(submission.MessageId, coding.CodedMessageId);
            Assert.Equal(submission.MessageSource, coding.MessageDestination);
            Assert.Equal(submission.MessageDestination, coding.MessageSource);
            Assert.Equal(submission.StateAuxiliaryId, coding.StateAuxiliaryId);
            Assert.Equal(submission.CertNo, coding.CertNo);
            Assert.Equal(submission.NCHSIdentifier, coding.NCHSIdentifier);
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
        public void CreateCauseOfDeathCodingResponseFromJSON()
        {
            CauseOfDeathCodingMessage message = BaseMessage.Parse<CauseOfDeathCodingMessage>(FixtureStream("fixtures/json/CauseOfDeathCodingMessage.json"));

            Assert.Equal(CauseOfDeathCodingMessage.MESSAGE_TYPE, message.MessageType);
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission", message.MessageDestination);
            Assert.Equal((uint)100000, message.CertNo);
            Assert.Equal((uint)2019, message.DeathYear);
            Assert.Null(message.StateAuxiliaryId);
            Assert.Equal("2019AK100000", message.NCHSIdentifier);

            Assert.Equal("I25.1", message.DeathRecord.AutomatedUnderlyingCOD);
            var recordAxisCodes = message.DeathRecord.RecordAxisCauseOfDeath;
            Assert.Equal(3, recordAxisCodes.Count());
            Assert.Equal("I25.1", recordAxisCodes.ElementAt(0).Code);
            Assert.Equal("I25.0", recordAxisCodes.ElementAt(1).Code);
            Assert.Equal("I51.7", recordAxisCodes.ElementAt(2).Code);
            var entityAxisEntryList = message.DeathRecord.EntityAxisCauseOfDeath;
            Assert.Equal(4, entityAxisEntryList.Count());
            Assert.Equal(1, entityAxisEntryList.ElementAt(0).LineNumber);
            Assert.Equal(1, entityAxisEntryList.ElementAt(0).Position);
            Assert.Equal("I25.9", entityAxisEntryList.ElementAt(0).Code);
            Assert.False(entityAxisEntryList.ElementAt(0).ECode);
            Assert.Equal(2, entityAxisEntryList.ElementAt(1).LineNumber);
            Assert.Equal(1, entityAxisEntryList.ElementAt(1).Position);
            Assert.Equal("I25.1", entityAxisEntryList.ElementAt(1).Code);
            Assert.False(entityAxisEntryList.ElementAt(1).ECode);
            Assert.Equal(3, entityAxisEntryList.ElementAt(2).LineNumber);
            Assert.Equal(1, entityAxisEntryList.ElementAt(2).Position);
            Assert.Equal("I25.0", entityAxisEntryList.ElementAt(2).Code);
            Assert.False(entityAxisEntryList.ElementAt(2).ECode);
            Assert.Equal(3, entityAxisEntryList.ElementAt(3).LineNumber);
            Assert.Equal(2, entityAxisEntryList.ElementAt(3).Position);
            Assert.Equal("I51.7", entityAxisEntryList.ElementAt(3).Code);
            Assert.True(entityAxisEntryList.ElementAt(3).ECode);
            Assert.Equal(3, message.DeathRecord.CoderStatus);
            Assert.Equal("555", message.DeathRecord.ShipmentNumber);
            Assert.Equal(2021, (int)message.DeathRecord.ReceiptYear);
            Assert.Equal(6, (int)message.DeathRecord.ReceiptMonth);
            Assert.Equal(1, (int)message.DeathRecord.ReceiptDay);
            Assert.Equal(ValueSets.MannerOfDeath.Natural_Death, message.DeathRecord.MannerOfDeathTypeHelper);
        }

        [Fact]
        public void CreateCauseOfDeathCodingAcknowledgementFromJSON()
        {
            // parse the message generically, without assuming type
            BaseMessage message = BaseMessage.Parse<BaseMessage>(FixtureStream("fixtures/json/CauseOfDeathCodingMessage2.json"));

            switch (message)
            {
                // use C# pattern matching to cast message type
                case CauseOfDeathCodingMessage codCodeMsg:
                    Assert.NotNull(codCodeMsg);
                    Assert.Equal(CauseOfDeathCodingMessage.MESSAGE_TYPE, message.MessageType);
                    Assert.Equal("https://apigw.cdc.gov/OSELS/NCHS/NVSSFHIRAPI/KY/Bundles?_since=2023-03-21T16:18:29.7258392-05:00", message.MessageDestination);
                    Assert.Equal((uint)8928, message.CertNo);
                    Assert.Equal("KY", message.JurisdictionId);
                    Assert.Equal((uint)2023, message.DeathYear);
                    Assert.Equal("230410164027", message.StateAuxiliaryId);

                    // now create an acknowledgement message for the cause of death coding message
                    var ackMessage = new AcknowledgementMessage(codCodeMsg);
                    Assert.Equal("KY", ackMessage.JurisdictionId);

                    // test serialization of the message to JSON
                    var ackMessageStr = ackMessage.ToJSON();
                    Assert.Contains("\"resourceType\":\"Bundle\"", ackMessageStr);
                    Assert.Contains("\"name\":\"jurisdiction_id\",\"valueString\":\"KY\"", ackMessageStr);

                    // re-parse the message to make sure it's still valid
                    var ackMessageParsed = BaseMessage.Parse<AcknowledgementMessage>(ackMessageStr);
                    Assert.NotNull(ackMessageParsed);
                    Assert.Equal("KY", ackMessageParsed.JurisdictionId);

                    break;
                default:
                    Assert.Fail("Message was not a CauseOfDeathCodingMessage");
                    break;
            }
        }


        [Fact]
        public void CreateCauseOfDeathCodingResponseFromSubmissionBundle()
        {
            CauseOfDeathCodingMessage message = new CauseOfDeathCodingMessage((DeathRecord)JSONRecords[0]);
            // Add cause of death coding
            message.DeathRecord.AutomatedUnderlyingCOD = "A04.7";
            // Add record axis codes
            IList<(int, string, bool)> causeOfDeathRecordAxis = new List<(int, string, bool)>();
            causeOfDeathRecordAxis.Add((1, "A41.9", false));
            message.DeathRecord.RecordAxisCauseOfDeath = causeOfDeathRecordAxis;
            // Add entity axis codes
            IList<(int, int, string, bool)> causeOfDeathEntityAxis = new List<(int, int, string, bool)>();
            causeOfDeathEntityAxis.Add((1, 1, "J18.9", false));
            message.DeathRecord.EntityAxisCauseOfDeath = causeOfDeathEntityAxis;
            // Make sure the codes are actually in there
            string json = message.ToJson();
            Assert.True(json.IndexOf("A04.7") > -1);
            Assert.True(json.IndexOf("A41.9") > -1);
            Assert.True(json.IndexOf("J18.9") > -1);
        }

        [Fact]
        public void CreateCauseOfDeathCodingUpdateFromJSON()
        {
            CauseOfDeathCodingUpdateMessage message = BaseMessage.Parse<CauseOfDeathCodingUpdateMessage>(FixtureStream("fixtures/json/CauseOfDeathCodingUpdateMessage.json"));
            Assert.Equal(CauseOfDeathCodingUpdateMessage.MESSAGE_TYPE, message.MessageType);
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission", message.MessageDestination);
            Assert.Equal((uint)100000, message.CertNo);
            Assert.Equal((uint)2019, message.DeathYear);
            Assert.Null(message.StateAuxiliaryId);
            Assert.Equal("2019AK100000", message.NCHSIdentifier);

            Assert.Equal("I25.1", message.DeathRecord.AutomatedUnderlyingCOD);
            var recordAxisCodes = message.DeathRecord.RecordAxisCauseOfDeath;
            Assert.Equal(3, recordAxisCodes.Count());
            Assert.Equal("I25.1", recordAxisCodes.ElementAt(0).Code);
            Assert.Equal("I25.0", recordAxisCodes.ElementAt(1).Code);
            Assert.Equal("I51.7", recordAxisCodes.ElementAt(2).Code);
            var entityAxisEntryList = message.DeathRecord.EntityAxisCauseOfDeath;
            Assert.Equal(4, entityAxisEntryList.Count());
            Assert.Equal(1, entityAxisEntryList.ElementAt(0).LineNumber);
            Assert.Equal(1, entityAxisEntryList.ElementAt(0).Position);
            Assert.Equal("I25.9", entityAxisEntryList.ElementAt(0).Code);
            Assert.False(entityAxisEntryList.ElementAt(0).ECode);
            Assert.Equal(2, entityAxisEntryList.ElementAt(1).LineNumber);
            Assert.Equal(1, entityAxisEntryList.ElementAt(1).Position);
            Assert.Equal("I25.1", entityAxisEntryList.ElementAt(1).Code);
            Assert.False(entityAxisEntryList.ElementAt(1).ECode);
            Assert.Equal(3, entityAxisEntryList.ElementAt(2).LineNumber);
            Assert.Equal(1, entityAxisEntryList.ElementAt(2).Position);
            Assert.Equal("I25.0", entityAxisEntryList.ElementAt(2).Code);
            Assert.False(entityAxisEntryList.ElementAt(2).ECode);
            Assert.Equal(3, entityAxisEntryList.ElementAt(3).LineNumber);
            Assert.Equal(2, entityAxisEntryList.ElementAt(3).Position);
            Assert.Equal("I51.7", entityAxisEntryList.ElementAt(3).Code);
            Assert.True(entityAxisEntryList.ElementAt(3).ECode);
            Assert.Equal(3, message.DeathRecord.CoderStatus);
            Assert.Equal("555", message.DeathRecord.ShipmentNumber);
            Assert.Equal(2021, (int)message.DeathRecord.ReceiptYear);
            Assert.Equal(6, (int)message.DeathRecord.ReceiptMonth);
            Assert.Equal(1, (int)message.DeathRecord.ReceiptDay);
        }

        [Fact]
        public void CreateCauseOfDeathCodingResponse()
        {
            // This test creates a response using the approach NCHS will use via IJE setters
            IJEMortality ije = new IJEMortality();
            ije.DOD_YR = "2022";
            ije.DSTATE = "YC";
            ije.FILENO = "123";
            ije.AUXNO = "500";
            ije.ACME_UC = "T273";
            ije.RAC = "T273 T270 ";
            ije.EAC = "11T273  21T270 &";
            ije.R_YR = "2019";
            ije.R_MO = "11";
            ije.R_DY = "14";
            ije.trx.CS = "8";
            ije.trx.SHIP = "876";
            ije.INT_REJ = "4";
            ije.SYS_REJ = "3";
            ije.TRX_FLG = "5";
            ije.MANNER = "A";
            ije.INJPL = "7";
            ije.DOI_YR = "2022";
            ije.DOI_MO = "1";
            ije.DOI_DY = "15";
            ije.AUTOP = "Y";
            ije.AUTOPF = "Y";
            ije.TOBAC = "Y";
            ije.PREG = "1";
            CauseOfDeathCodingMessage message = new CauseOfDeathCodingMessage(ije.ToDeathRecord());
            message.MessageSource = "http://nchs.cdc.gov/vrdr_submission";
            message.MessageDestination = "https://example.org/jurisdiction/endpoint";
            message.CodedMessageId = "378888";
            Assert.Equal(CauseOfDeathCodingMessage.MESSAGE_TYPE, message.MessageType);
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission", message.MessageSource);
            Assert.Equal("https://example.org/jurisdiction/endpoint", message.MessageDestination);
            Assert.Equal((uint)123, message.CertNo);
            Assert.Equal((uint)2022, message.DeathYear);
            Assert.Equal("500", message.StateAuxiliaryId);
            Assert.Equal("2022YC000123", message.NCHSIdentifier);
            Assert.Equal("378888", message.CodedMessageId);
            Assert.Equal("T27.3", message.DeathRecord.AutomatedUnderlyingCOD);
            var recordAxisCodes = message.DeathRecord.RecordAxisCauseOfDeath;
            Assert.Equal(2, recordAxisCodes.Count());
            Assert.Equal("T27.3", recordAxisCodes.ElementAt(0).Code);
            Assert.Equal("T27.0", recordAxisCodes.ElementAt(1).Code);
            var entityAxisEntryList = message.DeathRecord.EntityAxisCauseOfDeath;
            Assert.Equal(2, entityAxisEntryList.Count());
            Assert.Equal(1, entityAxisEntryList.ElementAt(0).LineNumber);
            Assert.Equal(1, entityAxisEntryList.ElementAt(0).Position);
            Assert.Equal("T27.3", entityAxisEntryList.ElementAt(0).Code);
            Assert.False(entityAxisEntryList.ElementAt(0).ECode);
            Assert.Equal(2, entityAxisEntryList.ElementAt(1).LineNumber);
            Assert.Equal(1, entityAxisEntryList.ElementAt(1).Position);
            Assert.Equal("T27.0", entityAxisEntryList.ElementAt(1).Code);
            Assert.True(entityAxisEntryList.ElementAt(1).ECode);
            Assert.Equal(2019, (int)message.DeathRecord.ReceiptYear);
            Assert.Equal(11, (int)message.DeathRecord.ReceiptMonth);
            Assert.Equal(14, (int)message.DeathRecord.ReceiptDay);
            Assert.Equal(8, (int)message.DeathRecord.CoderStatus);
            Assert.Equal("876", message.DeathRecord.ShipmentNumber);
            Assert.Equal(ValueSets.IntentionalReject.Reject4, message.DeathRecord.IntentionalRejectHelper);
            Assert.Equal(ValueSets.AcmeSystemReject.Micar_Reject_Rule_Application, message.DeathRecord.AcmeSystemRejectHelper);
            Assert.Equal(ValueSets.TransaxConversion.Duplicate_Entity_Axis_Codes_Deleted_No_Other_Action_Involved, message.DeathRecord.TransaxConversionHelper);
            Assert.Equal(ValueSets.MannerOfDeath.Accidental_Death, message.DeathRecord.MannerOfDeathTypeHelper);
            Assert.Equal(ValueSets.PlaceOfInjury.Farm, message.DeathRecord.PlaceOfInjuryHelper);
            Assert.Equal(2022, (int)message.DeathRecord.InjuryYear);
            Assert.Equal(1, (int)message.DeathRecord.InjuryMonth);
            Assert.Equal(15, (int)message.DeathRecord.InjuryDay);
            Assert.Equal(ValueSets.YesNoUnknown.Yes, message.DeathRecord.AutopsyPerformedIndicatorHelper);
            Assert.Equal(ValueSets.YesNoUnknown.Yes, message.DeathRecord.AutopsyResultsAvailableHelper);
            Assert.Equal(ValueSets.ContributoryTobaccoUse.Yes, message.DeathRecord.TobaccoUseHelper);
            Assert.Equal(ValueSets.PregnancyStatus.Not_Pregnant_Within_Past_Year, message.DeathRecord.PregnancyStatusHelper);
        }

        [Fact]
        public void CreateCauseOfDeathCodingUpdate()
        {
            // This test creates a response using the approach NCHS will use via IJE setters
            IJEMortality ije = new IJEMortality();
            ije.DOD_YR = "2022";
            ije.DSTATE = "YC";
            ije.FILENO = "123";
            ije.AUXNO = "500";
            ije.ACME_UC = "T273";
            ije.RAC = "T273 T270 ";
            ije.EAC = "11T273  21T270 &";
            ije.R_YR = "2019";
            ije.R_MO = "11";
            ije.R_DY = "14";
            ije.trx.CS = "8";
            ije.trx.SHIP = "876";
            ije.INT_REJ = "4";
            ije.SYS_REJ = "3";
            ije.TRX_FLG = "5";
            ije.MANNER = "A";
            ije.INJPL = "7";
            ije.DOI_YR = "2022";
            ije.DOI_MO = "1";
            ije.DOI_DY = "15";
            ije.AUTOP = "Y";
            ije.AUTOPF = "Y";
            ije.TOBAC = "Y";
            ije.PREG = "1";
            CauseOfDeathCodingUpdateMessage message = new CauseOfDeathCodingUpdateMessage(ije.ToDeathRecord());
            message.MessageSource = "http://nchs.cdc.gov/vrdr_submission";
            message.MessageDestination = "https://example.org/jurisdiction/endpoint";
            Assert.Equal(CauseOfDeathCodingUpdateMessage.MESSAGE_TYPE, message.MessageType);
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission", message.MessageSource);
            Assert.Equal("https://example.org/jurisdiction/endpoint", message.MessageDestination);
            Assert.Equal((uint)123, message.CertNo);
            Assert.Equal((uint)2022, message.DeathYear);
            Assert.Equal("500", message.StateAuxiliaryId);
            Assert.Equal("2022YC000123", message.NCHSIdentifier);
            Assert.Equal("T27.3", message.DeathRecord.AutomatedUnderlyingCOD);
            var recordAxisCodes = message.DeathRecord.RecordAxisCauseOfDeath;
            Assert.Equal(2, recordAxisCodes.Count());
            Assert.Equal("T27.3", recordAxisCodes.ElementAt(0).Code);
            Assert.Equal("T27.0", recordAxisCodes.ElementAt(1).Code);
            var entityAxisEntryList = message.DeathRecord.EntityAxisCauseOfDeath;
            Assert.Equal(2, entityAxisEntryList.Count());
            Assert.Equal(1, entityAxisEntryList.ElementAt(0).LineNumber);
            Assert.Equal(1, entityAxisEntryList.ElementAt(0).Position);
            Assert.Equal("T27.3", entityAxisEntryList.ElementAt(0).Code);
            Assert.False(entityAxisEntryList.ElementAt(0).ECode);
            Assert.Equal(2, entityAxisEntryList.ElementAt(1).LineNumber);
            Assert.Equal(1, entityAxisEntryList.ElementAt(1).Position);
            Assert.Equal("T27.0", entityAxisEntryList.ElementAt(1).Code);
            Assert.True(entityAxisEntryList.ElementAt(1).ECode);
            Assert.Equal(2019, (int)message.DeathRecord.ReceiptYear);
            Assert.Equal(11, (int)message.DeathRecord.ReceiptMonth);
            Assert.Equal(14, (int)message.DeathRecord.ReceiptDay);
            Assert.Equal(8, (int)message.DeathRecord.CoderStatus);
            Assert.Equal("876", message.DeathRecord.ShipmentNumber);
            Assert.Equal(ValueSets.IntentionalReject.Reject4, message.DeathRecord.IntentionalRejectHelper);
            Assert.Equal(ValueSets.AcmeSystemReject.Micar_Reject_Rule_Application, message.DeathRecord.AcmeSystemRejectHelper);
            Assert.Equal(ValueSets.TransaxConversion.Duplicate_Entity_Axis_Codes_Deleted_No_Other_Action_Involved, message.DeathRecord.TransaxConversionHelper);
            Assert.Equal(ValueSets.MannerOfDeath.Accidental_Death, message.DeathRecord.MannerOfDeathTypeHelper);
            Assert.Equal(ValueSets.PlaceOfInjury.Farm, message.DeathRecord.PlaceOfInjuryHelper);
            Assert.Equal(2022, (int)message.DeathRecord.InjuryYear);
            Assert.Equal(1, (int)message.DeathRecord.InjuryMonth);
            Assert.Equal(15, (int)message.DeathRecord.InjuryDay);
            Assert.Equal(ValueSets.YesNoUnknown.Yes, message.DeathRecord.AutopsyPerformedIndicatorHelper);
            Assert.Equal(ValueSets.YesNoUnknown.Yes, message.DeathRecord.AutopsyResultsAvailableHelper);
            Assert.Equal(ValueSets.ContributoryTobaccoUse.Yes, message.DeathRecord.TobaccoUseHelper);
            Assert.Equal(ValueSets.PregnancyStatus.Not_Pregnant_Within_Past_Year, message.DeathRecord.PregnancyStatusHelper);
        }

        [Fact]
        public void CreateDemographicCodingResponseFromJSON()
        {
            DemographicsCodingMessage message = BaseMessage.Parse<DemographicsCodingMessage>(FixtureStream("fixtures/json/DemographicsCodingMessage.json"));
            Assert.Equal(DemographicsCodingMessage.MESSAGE_TYPE, message.MessageType);
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission", message.MessageDestination);
            Assert.Equal((uint)123, message.CertNo);
            Assert.Equal((uint)2022, message.DeathYear);
            Assert.Equal("500", message.StateAuxiliaryId);
            Assert.Equal("2022YC000123", message.NCHSIdentifier);
            Assert.Equal("199", message.DeathRecord.FirstEditedRaceCodeHelper);
            Assert.Equal("B40", message.DeathRecord.FirstAmericanIndianRaceCodeHelper);
        }

        [Fact]
        public void CreateDemographicsCodingUpdateFromJSON()
        {
            DemographicsCodingUpdateMessage message = BaseMessage.Parse<DemographicsCodingUpdateMessage>(FixtureStream("fixtures/json/DemographicsCodingUpdateMessage.json"));
            Assert.Equal(DemographicsCodingUpdateMessage.MESSAGE_TYPE, message.MessageType);
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission", message.MessageDestination);
            Assert.Equal((uint)123, message.CertNo);
            Assert.Equal((uint)2022, message.DeathYear);
            Assert.Equal("500", message.StateAuxiliaryId);
            Assert.Equal("2022YC000123", message.NCHSIdentifier);
            Assert.Equal("199", message.DeathRecord.FirstEditedRaceCodeHelper);
            Assert.Equal("B40", message.DeathRecord.FirstAmericanIndianRaceCodeHelper);
        }

        [Fact]
        public void CreateDemographicCodingResponse()
        {
            // This test creates a response using the approach NCHS will use via IJE setters
            IJEMortality ije = new IJEMortality();
            ije.DOD_YR = "2022";
            ije.DSTATE = "YC";
            ije.FILENO = "123";
            ije.AUXNO = "500";
            ije.DETHNIC1 = "Y";
            ije.DETHNIC2 = "N";
            ije.RACE1 = "Y";
            ije.RACE2 = "N";
            ije.RACE16 = "Cheyenne";
            ije.RACE1E = "199";
            ije.RACE16C = "B40";
            DemographicsCodingMessage message = new DemographicsCodingMessage(ije.ToDeathRecord());
            message.MessageSource = "http://nchs.cdc.gov/vrdr_submission";
            message.MessageDestination = "https://example.org/jurisdiction/endpoint";
            Assert.Equal(DemographicsCodingMessage.MESSAGE_TYPE, message.MessageType);
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission", message.MessageSource);
            Assert.Equal("https://example.org/jurisdiction/endpoint", message.MessageDestination);
            Assert.Equal((uint)123, message.CertNo);
            Assert.Equal((uint)2022, message.DeathYear);
            Assert.Equal("500", message.StateAuxiliaryId);
            Assert.Equal("2022YC000123", message.NCHSIdentifier);
            Assert.Equal(ValueSets.YesNoUnknown.Yes, message.DeathRecord.Ethnicity1Helper);
            Assert.Equal(ValueSets.YesNoUnknown.No, message.DeathRecord.Ethnicity2Helper);
            Assert.Equal(NvssRace.White, message.DeathRecord.Race[0].Item1);
            Assert.Equal("Y", message.DeathRecord.Race[0].Item2);
            Assert.Equal(NvssRace.BlackOrAfricanAmerican, message.DeathRecord.Race[1].Item1);
            Assert.Equal("N", message.DeathRecord.Race[1].Item2);
            Assert.Equal("199", message.DeathRecord.FirstEditedRaceCodeHelper);
            Assert.Equal("B40", message.DeathRecord.FirstAmericanIndianRaceCodeHelper);
        }

        [Fact]
        public void CreateDemographicCodingUpdate()
        {
            // This test creates a response using the approach NCHS will use via IJE setters
            IJEMortality ije = new IJEMortality();
            ije.DOD_YR = "2022";
            ije.DSTATE = "YC";
            ije.FILENO = "123";
            ije.AUXNO = "500";
            ije.DETHNIC1 = "Y";
            ije.DETHNIC2 = "N";
            ije.RACE1 = "Y";
            ije.RACE2 = "N";
            ije.RACE16 = "Cheyenne";
            ije.RACE1E = "199";
            ije.RACE16C = "B40";
            DemographicsCodingUpdateMessage message = new DemographicsCodingUpdateMessage(ije.ToDeathRecord());
            message.MessageSource = "http://nchs.cdc.gov/vrdr_submission";
            message.MessageDestination = "https://example.org/jurisdiction/endpoint";
            Assert.Equal(DemographicsCodingUpdateMessage.MESSAGE_TYPE, message.MessageType);
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission", message.MessageSource);
            Assert.Equal("https://example.org/jurisdiction/endpoint", message.MessageDestination);
            Assert.Equal((uint)123, message.CertNo);
            Assert.Equal((uint)2022, message.DeathYear);
            Assert.Equal("500", message.StateAuxiliaryId);
            Assert.Equal("2022YC000123", message.NCHSIdentifier);
            Assert.Equal(ValueSets.YesNoUnknown.Yes, message.DeathRecord.Ethnicity1Helper);
            Assert.Equal(ValueSets.YesNoUnknown.No, message.DeathRecord.Ethnicity2Helper);
            Assert.Equal(NvssRace.White, message.DeathRecord.Race[0].Item1);
            Assert.Equal("Y", message.DeathRecord.Race[0].Item2);
            Assert.Equal(NvssRace.BlackOrAfricanAmerican, message.DeathRecord.Race[1].Item1);
            Assert.Equal("N", message.DeathRecord.Race[1].Item2);
            Assert.Equal("199", message.DeathRecord.FirstEditedRaceCodeHelper);
            Assert.Equal("B40", message.DeathRecord.FirstAmericanIndianRaceCodeHelper);
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
            message.BlockCount = 0;
            Assert.Equal((uint)0, message.BlockCount);
        }
        [Fact]
        public void CreateStatusMessage()
        {
            DeathRecordSubmissionMessage submission = BaseMessage.Parse<DeathRecordSubmissionMessage>(FixtureStream("fixtures/json/DeathRecordSubmissionMessage.json"));
            StatusMessage status = new StatusMessage(submission, "manualCauseOfDeathCoding");
            Assert.Equal("http://nchs.cdc.gov/vrdr_status", status.MessageType);
            Assert.Equal("manualCauseOfDeathCoding", status.Status);
            Assert.Equal(submission.MessageId, status.StatusedMessageId);
            Assert.Equal(submission.MessageSource, status.MessageDestination);
            Assert.Equal(submission.MessageDestination, status.MessageSource);
            Assert.Equal(submission.StateAuxiliaryId, status.StateAuxiliaryId);
            Assert.Equal(submission.CertNo, status.CertNo);
            Assert.Equal(submission.NCHSIdentifier, status.NCHSIdentifier);


            submission = null;
            status = new StatusMessage(submission, "manualCauseOfDeathCoding");
            Assert.Equal("http://nchs.cdc.gov/vrdr_status", status.MessageType);
            Assert.Equal("manualCauseOfDeathCoding", status.Status);
            Assert.Null(status.StatusedMessageId);
            Assert.Null(status.MessageDestination);
            Assert.Null(status.MessageSource);
            Assert.Null(status.CertNo);
            Assert.Null(status.StateAuxiliaryId);
            Assert.Null(status.NCHSIdentifier);

            submission = new DeathRecordSubmissionMessage();
            status = new StatusMessage(submission, "manualCauseOfDeathCoding");
            Assert.Equal("http://nchs.cdc.gov/vrdr_status", status.MessageType);
            Assert.Equal("manualCauseOfDeathCoding", status.Status);
            Assert.Equal(submission.MessageId, status.StatusedMessageId);
            Assert.Equal(submission.MessageSource, status.MessageDestination);
            Assert.Equal(submission.MessageDestination, status.MessageSource);
            Assert.Equal(submission.StateAuxiliaryId, status.StateAuxiliaryId);
            Assert.Equal(submission.CertNo, status.CertNo);
            Assert.Equal(submission.NCHSIdentifier, status.NCHSIdentifier);
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
        public void CreateAckForStatusMessage()
        {
            StatusMessage statusMessage = BaseMessage.Parse<StatusMessage>(FixtureStream("fixtures/json/StatusMessage.json"));
            AcknowledgementMessage ack = new AcknowledgementMessage(statusMessage);
            Assert.Equal("http://nchs.cdc.gov/vrdr_acknowledgement", ack.MessageType);
            Assert.Equal(statusMessage.MessageId, ack.AckedMessageId);
            Assert.Equal(statusMessage.MessageSource, ack.MessageDestination);
            Assert.Equal(statusMessage.MessageDestination, ack.MessageSource);
            Assert.Equal(statusMessage.StateAuxiliaryId, ack.StateAuxiliaryId);
            Assert.Equal(statusMessage.CertNo, ack.CertNo);
            Assert.Equal(statusMessage.NCHSIdentifier, ack.NCHSIdentifier);

            statusMessage = null;
            ack = new AcknowledgementMessage(statusMessage);
            Assert.Equal("http://nchs.cdc.gov/vrdr_acknowledgement", ack.MessageType);
            Assert.Null(ack.AckedMessageId);
            Assert.Null(ack.MessageDestination);
            Assert.Null(ack.MessageSource);
            Assert.Null(ack.CertNo);
            Assert.Null(ack.StateAuxiliaryId);
            Assert.Null(ack.NCHSIdentifier);

            statusMessage = new StatusMessage();
            ack = new AcknowledgementMessage(statusMessage);
            Assert.Equal("http://nchs.cdc.gov/vrdr_acknowledgement", ack.MessageType);
            Assert.Equal(statusMessage.MessageId, ack.AckedMessageId);
            Assert.Equal(statusMessage.MessageSource, ack.MessageDestination);
            Assert.Equal(statusMessage.MessageDestination, ack.MessageSource);
            Assert.Equal(statusMessage.StateAuxiliaryId, ack.StateAuxiliaryId);
            Assert.Equal(statusMessage.CertNo, ack.CertNo);
            Assert.Equal(statusMessage.NCHSIdentifier, ack.NCHSIdentifier);
        }
        [Fact]
        public void CreateDeathRecordVoidMessageFromJson()
        {
            DeathRecordVoidMessage message = BaseMessage.Parse<DeathRecordVoidMessage>(FixtureStream("fixtures/json/DeathRecordVoidMessage.json"));
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission_void", message.MessageType);
            Assert.Equal((uint)123456, message.CertNo);
            Assert.Equal((uint)10, message.BlockCount);
            Assert.Equal("abcdef10", message.StateAuxiliaryId);
            Assert.Equal("2018NY123456", message.NCHSIdentifier);
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission", message.MessageDestination);
            Assert.Equal("https://sos.nh.gov/vitalrecords", message.MessageSource);

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
            Assert.Equal((uint)123456, message.CertNo);
            Assert.Equal("abcdef10", message.StateAuxiliaryId);
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission", message.MessageDestination);
            Assert.Equal("John", message.AliasDecedentFirstName);
            Assert.Equal("Schmidt", message.AliasDecedentLastName);
            Assert.Equal("Jacob", message.AliasDecedentMiddleName);
            Assert.Equal("III", message.AliasDecedentNameSuffix);
            Assert.Equal("Jingleheimer", message.AliasFatherSurname);
            Assert.Equal("123-45-6789", message.AliasSocialSecurityNumber);
        }

        [Fact]
        public void CreateAliasForDocument()
        {
            DeathRecordAliasMessage message = new DeathRecordAliasMessage((DeathRecord)XMLRecords[0]);
            Assert.Equal("http://nchs.cdc.gov/vrdr_alias", message.MessageType);
            Assert.Equal((uint)182, message.CertNo);
            Assert.Equal("000000000042", message.StateAuxiliaryId);
            Assert.Equal("2019YC000182", message.NCHSIdentifier);
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission", message.MessageDestination);
            Assert.Null(message.MessageSource);
        }

        [Fact]
        public void CreateAliasForRecordWithEmptyMiddleNameString()
        {
            // This is a regression test for a bug where an empty middle name field led to an empty string in the alias
            DeathRecord record = new DeathRecord();
            record.Identifier = "321";
            record.DeathLocationJurisdiction = "MA";
            record.DeathYear = 2022;
            record.GivenNames = new string[] { "Rocket", "" };
            DeathRecordAliasMessage message = new DeathRecordAliasMessage(record);
            Assert.Null(message.AliasDecedentMiddleName);
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
        public void ParseOldTRXVersionAsGenericMsg()
        {
            // verifies the Generic Message Parser allows for messages based on the old IG 1.2
            string trxMsg = FixtureStream("fixtures/json/old-trx-pre-IGv13.json").ReadToEnd();
            BaseMessage msg = BaseMessage.ParseGenericMessage(trxMsg, true);
            Assert.Equal("http://nchs.cdc.gov/vrdr_coding", msg.MessageType);
        }

        [Fact]
        public void ParseOldTRXVersionAsCodedMsgType()
        {
            // verifies the Type Specific Message Parser errors on messages based on the old IG 1.2
            MessageParseException ex = Assert.Throws<MessageParseException>(() => BaseMessage.Parse(FixtureStream("fixtures/json/old-trx-pre-IGv13.json")));
            Assert.Equal("Unsupported message type: http://nchs.cdc.gov/vrdr_coding", ex.Message);
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

        /*         [Fact]
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
                } */

        [Fact]
        public void CreateSubmissionFromJSONWithValueCodeableConceptText()
        {
            DeathRecordSubmissionMessage submission = BaseMessage.Parse<DeathRecordSubmissionMessage>(FixtureStream("fixtures/json/DeathRecordValueCodeableConceptText.json"));
            Assert.Equal("Drowning", submission.DeathRecord.COD1A);
            Assert.Equal("sad", submission.DeathRecord.ContributingConditions);
            Assert.Equal("DO", submission.DeathRecord.CertificationRoleHelper);
            Assert.Null(submission.DeathRecord.UsualOccupation);
            Assert.Null(submission.DeathRecord.UsualIndustry);
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
