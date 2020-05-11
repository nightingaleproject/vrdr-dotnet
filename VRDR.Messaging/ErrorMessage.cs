using System;
using System.Collections.Generic;
using Hl7.Fhir.Model;

namespace VRDR
{
    /// <summary>Class <c>ExtractionErrorMessage</c> is used to communicate that initial processing of a DeathRecordSubmission message failed.</summary>
    public class ExtractionErrorMessage : BaseMessage
    {
        private OperationOutcome details;

        /// <summary>Constructor that creates an extraction error for the specified message.</summary>
        /// <param name="sourceMessage">the message that could not be processed.</param>
        /// <param name="source">the endpoint identifier that the message will be sent from.</param>
        public ExtractionErrorMessage(BaseMessage sourceMessage, string source = "http://nchs.cdc.gov/vrdr_submission") : this(sourceMessage?.MessageId, sourceMessage?.MessageSource, source)
        {
        }

        /// <summary>
        /// Construct an ExtractionErrorMessage from a FHIR Bundle.
        /// </summary>
        /// <param name="messageBundle">a FHIR Bundle that will be used to initialize the ExtractionErrorMessage</param>
        /// <returns></returns>
        internal ExtractionErrorMessage(Bundle messageBundle) : base(messageBundle)
        {
            details = findEntry<OperationOutcome>(ResourceType.OperationOutcome);
        }

        /// <summary>Constructor that creates an extraction error message for the specified message.</summary>
        /// <param name="messageId">the id of the message to create an extraction error for.</param>
        /// <param name="destination">the endpoint identifier that the extraction error message will be sent to.</param>
        /// <param name="source">the endpoint identifier that the extraction error message will be sent from.</param>
        public ExtractionErrorMessage(string messageId, string destination, string source = "http://nchs.cdc.gov/vrdr_submission") : base("http://nchs.cdc.gov/vrdr_extraction_error")
        {
            Header.Source.Endpoint = source;
            this.MessageDestination = destination;
            MessageHeader.ResponseComponent resp = new MessageHeader.ResponseComponent();
            resp.Identifier = messageId;
            resp.Code = MessageHeader.ResponseType.FatalError;
            Header.Response = resp;

            this.details = new OperationOutcome();
            this.details.Id = Guid.NewGuid().ToString();
            MessageBundle.AddResourceEntry(this.details, "urn:uuid:" + this.details.Id);
            Header.Response.Details = new ResourceReference(this.details.Id);
        }

        /// <summary>The id of the message that could not be extracted</summary>
        /// <value>the message id.</value>
        public string FailedMessageId
        {
            get
            {
                return Header.Response.Identifier;
            }
            set
            {
                Header.Response.Identifier = value;
            }
        }

        /// <summary>
        /// List of issues found when attenpting to extract the message
        /// </summary>
        /// <value>list of issues</value>
        public List<Issue> Issues
        {
            get
            {
                var issues = new List<Issue>();
                foreach (var detailEntry in details.Issue)
                {
                    var issue = new Issue(detailEntry.Severity, detailEntry.Code, detailEntry.Diagnostics);
                    issues.Add(issue);
                }
                return issues;
            }
            set
            {
                details.Issue.Clear();
                foreach (var issue in value)
                {
                    var entry = new OperationOutcome.IssueComponent();
                    entry.Severity = issue.Severity;
                    entry.Code = issue.Type;
                    entry.Diagnostics = issue.Description;
                    details.Issue.Add(entry);
                }
            }
        }
    }

    /// <summary>Class <c>CodingErrorMessage</c> is used to communicate that cading of a DeathRecordSubmission message failed.</summary>
    public class CodingErrorMessage : ExtractionErrorMessage
    {
        private Parameters parameters;

        /// <summary>Constructor that creates a coding error for the specified message.</summary>
        /// <param name="sourceMessage">the message that could not be processed.</param>
        /// <param name="source">the endpoint identifier that the message will be sent from.</param>
        public CodingErrorMessage(DeathRecordSubmission sourceMessage, string source = "http://nchs.cdc.gov/vrdr_submission")
            : this(sourceMessage?.DeathRecord.Identifier, sourceMessage?.DeathRecord.BundleIdentifier, sourceMessage?.MessageSource, source)
        {
            FailedMessageId = sourceMessage?.MessageId;
        }

        /// <summary>
        /// Construct an CodingErrorMessage from a FHIR Bundle.
        /// </summary>
        /// <param name="messageBundle">a FHIR Bundle that will be used to initialize the CodingErrorMessage</param>
        /// <returns></returns>
        internal CodingErrorMessage(Bundle messageBundle) : base(messageBundle)
        {
            parameters = findEntry<Parameters>(ResourceType.Parameters);
        }

        /// <summary>Constructor that creates a coding error message for the specified message.</summary>
        /// <param name="certificateNumber">the jurisdiction-assigned death certificate number.</param>
        /// <param name="stateIdentifier">the jurisdiction-assigned id.</param>
        /// <param name="destination">the endpoint identifier that the coding error message will be sent to.</param>
        /// <param name="source">the endpoint identifier that the coding error message will be sent from.</param>
        public CodingErrorMessage(string certificateNumber, string stateIdentifier, string destination, string source = "http://nchs.cdc.gov/vrdr_submission")
            : base(null, destination, source)
        {
            MessageType = "http://nchs.cdc.gov/vrdr_coding_error";
            this.parameters = new Parameters();
            this.parameters.Id = Guid.NewGuid().ToString();
            MessageBundle.AddResourceEntry(this.parameters, "urn:uuid:" + this.parameters.Id);
            CertificateNumber = certificateNumber;
            StateIdentifier = stateIdentifier;
            Header.Focus.Add(new ResourceReference("urn:uuid:" + this.parameters.Id));
        }
    }

    /// <summary>
    /// Class representing an issue detected during message processing.
    /// </summary>
    public class Issue
    {
        /// <summary>
        /// Severity of the issue
        /// </summary>
        public readonly OperationOutcome.IssueSeverity? Severity;

        /// <summary>
        /// Type of the issue
        /// </summary>
        public readonly OperationOutcome.IssueType? Type;

        /// <summary>
        /// Human readable description of the issue
        /// </summary>
        public readonly string Description;

        /// <summary>
        /// Construct a new Issue
        /// </summary>
        /// <param name="severity">the severity of the issue</param>
        /// <param name="type">the type of issue</param>
        /// <param name="description">a human readable description of the issue</param>
        public Issue(OperationOutcome.IssueSeverity? severity, OperationOutcome.IssueType? type, string description)
        {
            Severity = severity;
            Type = type;
            Description = description;
        }
    }
}