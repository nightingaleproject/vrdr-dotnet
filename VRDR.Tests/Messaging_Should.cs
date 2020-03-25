using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using Xunit;

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
            Assert.Equal("vrdr_submission", submission.MessageType);
        }

        [Fact]
        public void CreateSubmissionFromDeathRecord()
        {
            DeathRecordSubmission submission = new DeathRecordSubmission((DeathRecord)XMLRecords[0]);
            Assert.Equal("2018-02-20T16:48:06-05:00", submission.MessagePayload.DateOfDeathPronouncement);
            Assert.Equal("vrdr_submission", submission.MessageType);
        }

        [Fact]
        public void CreateSubmissionFromJSON()
        {
            DeathRecordSubmission submission = new DeathRecordSubmission(File.ReadAllText(FixturePath("fixtures/json/DeathRecordSubmission.json")));
            Assert.Equal("2018-02-20T16:48:06-05:00", submission.MessagePayload.DateOfDeathPronouncement);
            Assert.Equal("vrdr_submission", submission.MessageType);
        }

        [Fact]
        public void CreateUpdate()
        {
            DeathRecordUpdate submission = new DeathRecordUpdate();
            Assert.Equal("vrdr_submission_update", submission.MessageType);
        }

        [Fact]
        public void CreateUpdateFromDeathRecord()
        {
            DeathRecordUpdate submission = new DeathRecordUpdate((DeathRecord)XMLRecords[0]);
            Assert.Equal("2018-02-20T16:48:06-05:00", submission.MessagePayload.DateOfDeathPronouncement);
            Assert.Equal("vrdr_submission_update", submission.MessageType);
        }

        [Fact]
        public void CreateUpdateFromJSON()
        {
            DeathRecordUpdate submission = new DeathRecordUpdate(File.ReadAllText(FixturePath("fixtures/json/DeathRecordUpdate.json")));
            Assert.Equal("2018-02-20T16:48:06-05:00", submission.MessagePayload.DateOfDeathPronouncement);
            Assert.Equal("vrdr_submission_update", submission.MessageType);
        }

        [Fact]
        public void CreateAckForMessage()
        {
            DeathRecordSubmission submission = new DeathRecordSubmission(File.ReadAllText(FixturePath("fixtures/json/DeathRecordSubmission.json")));
            AckMessage ack = new AckMessage(submission);
            Assert.Equal("vrdr_acknowledgement", ack.MessageType);
            Assert.Equal(submission.MessageId, ack.AckedMessageId);
            Assert.Equal(submission.MessageSource, ack.MessageDestination);
        }

        [Fact]
        public void CreateAckFromJSON()
        {
            AckMessage ack = new AckMessage(File.ReadAllText(FixturePath("fixtures/json/AckMessage.json")));
            Assert.Equal("vrdr_acknowledgement", ack.MessageType);
            Assert.Equal("a9d66d2e-2480-4e8d-bab3-4e4c761da1b7", ack.AckedMessageId);
            Assert.Equal("nightingale", ack.MessageDestination);
        }

        [Fact]
        public void CreateAckFromXML()
        {
            AckMessage ack = new AckMessage(File.ReadAllText(FixturePath("fixtures/xml/AckMessage.xml")));
            Assert.Equal("vrdr_acknowledgement", ack.MessageType);
            Assert.Equal("a9d66d2e-2480-4e8d-bab3-4e4c761da1b7", ack.AckedMessageId);
            Assert.Equal("nightingale", ack.MessageDestination);
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
