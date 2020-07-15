using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;

namespace VRDR
{
    /// <summary>Class <c>BaseMessage</c> is the base class of all messages.</summary>
    public class BaseMessage
    {
        /// <summary>Bundle that contains the message.</summary>
        protected Bundle MessageBundle;
        
        /// <summary>
        /// A Parameters entry that contains business identifiers for all messages plus additional information for Coding messages.
        /// </summary>
        protected Parameters Record;


        /// <summary>MessageHeader that contains the message header.</summary>
        protected MessageHeader Header;

        /// <summary>
        /// Construct a BaseMessage from a FHIR Bundle. This constructor will also validate that the Bundle
        /// represents a FHIR message of the correct type.
        /// </summary>
        /// <param name="messageBundle">a FHIR Bundle that will be used to initialize the BaseMessage</param>
        /// <param name="ignoreMissingEntries">if true, then missing bundle entries will not result in an exception</param>
        /// <param name="ignoreBundleType">if true, then an incorrect bundle type will not result in an exception</param>
        protected BaseMessage(Bundle messageBundle, bool ignoreMissingEntries = false, bool ignoreBundleType = false)
        {
            MessageBundle = messageBundle;

            // Validate bundle type is message
            if (messageBundle?.Type != Bundle.BundleType.Message && !ignoreBundleType)
            {
                String actualType = messageBundle?.Type == null ? "null" : messageBundle?.Type.ToString();
                throw new MessageParseException($"The FHIR Bundle must be of type message, not {actualType}", new BaseMessage(messageBundle, true, true));
            }

            // Find Header
            Header = findEntry<MessageHeader>(ResourceType.MessageHeader, ignoreMissingEntries);

            // Find Parameters
            Record = findEntry<Parameters>(ResourceType.Parameters, ignoreMissingEntries);
        }

        /// <summary>
        /// Find the first Entry within the message Bundle that contains a Resource of the specified type and return that resource.
        /// </summary>
        /// <param name="type">the type of FHIR resource to look for</param>
        /// <param name="ignoreMissingEntries">if true, then missing entries will not result in an exception</param>
        /// <typeparam name="T">the class of the FHIR resource to return, must match with specified type:</typeparam>
        /// <returns>The first matching Bundle entry</returns>
        protected T findEntry<T>(ResourceType type, bool ignoreMissingEntries = false) where T : Resource
        {
            var typedEntry = MessageBundle.Entry.FirstOrDefault( entry => entry.Resource.ResourceType == type );
            if (typedEntry == null && !ignoreMissingEntries)
            {
                throw new System.ArgumentException($"Failed to find a Bundle Entry containing a Resource of type {type.ToString()}");
            }
            return (T)typedEntry?.Resource;
        }

        /// <summary>Constructor that creates a new, empty message for the specified message type.</summary>
        protected BaseMessage(String messageType)
        {
            // Start with a Bundle.
            MessageBundle = new Bundle();
            MessageBundle.Id = Guid.NewGuid().ToString();
            MessageBundle.Type = Bundle.BundleType.Message;
            MessageBundle.Timestamp = DateTime.Now;

            // Start with a MessageHeader.
            Header = new MessageHeader();
            Header.Id = Guid.NewGuid().ToString();
            Header.Event = new FhirUri(messageType);

            MessageHeader.MessageDestinationComponent dest = new MessageHeader.MessageDestinationComponent();
            dest.Endpoint = "http://nchs.cdc.gov/vrdr_submission";
            Header.Destination.Add(dest);
            MessageHeader.MessageSourceComponent src = new MessageHeader.MessageSourceComponent();
            Header.Source = src;
            MessageBundle.AddResourceEntry(Header, "urn:uuid:" + Header.Id);

            Record = new Parameters();
            Record.Id = Guid.NewGuid().ToString();
            MessageBundle.AddResourceEntry(this.Record, "urn:uuid:" + this.Record.Id);
            Header.Focus.Add(new ResourceReference("urn:uuid:" + this.Record.Id));
        }

        /// <summary>Helper method to return a XML string representation of this DeathRecordSubmission.</summary>
        /// <param name="prettyPrint">controls whether the returned string is formatted for human readability (true) or compact (false)</param>
        /// <returns>a string representation of this DeathRecordSubmission in XML format</returns>
        public string ToXML(bool prettyPrint = false)
        {
            return MessageBundle.ToXml(new FhirXmlSerializationSettings { Pretty = prettyPrint, AppendNewLine = prettyPrint, TrimWhitespaces = prettyPrint });
        }

        /// <summary>Helper method to return a XML string representation of this DeathRecordSubmission.</summary>
        /// <param name="prettyPrint">controls whether the returned string is formatted for human readability (true) or compact (false)</param>
        /// <returns>a string representation of this DeathRecordSubmission in XML format</returns>
        public string ToXml(bool prettyPrint = false)
        {
            return ToXML(prettyPrint);
        }

        /// <summary>Helper method to return a JSON string representation of this DeathRecordSubmission.</summary>
        /// <param name="prettyPrint">controls whether the returned string is formatted for human readability (true) or compact (false)</param>
        /// <returns>a string representation of this DeathRecordSubmission in JSON format</returns>
        public string ToJSON(bool prettyPrint = false)
        {
            return MessageBundle.ToJson(new FhirJsonSerializationSettings { Pretty = prettyPrint, AppendNewLine = prettyPrint });
        }

        /// <summary>Helper method to return a JSON string representation of this DeathRecordSubmission.</summary>
        /// <param name="prettyPrint">controls whether the returned string is formatted for human readability (true) or compact (false)</param>
        /// <returns>a string representation of this DeathRecordSubmission in JSON format</returns>
        public string ToJson(bool prettyPrint = false)
        {
            return ToJSON(prettyPrint);
        }

        /////////////////////////////////////////////////////////////////////////////////
        //
        // Message Properties
        //
        /////////////////////////////////////////////////////////////////////////////////

        /// <summary>Message timestamp</summary>
        /// <value>the message timestamp.</value>
        public DateTimeOffset? MessageTimestamp
        {
            get
            {
                return MessageBundle.Timestamp;
            }
            set
            {
                MessageBundle.Timestamp = value;
            }
        }

        /// <summary>Message Id</summary>
        /// <value>the message id.</value>
        public string MessageId
        {
            get
            {
                return Header?.Id;
            }
            set
            {
                Header.Id = value;
                MessageBundle.Entry.RemoveAll( entry => entry.Resource.ResourceType == ResourceType.MessageHeader );
                MessageBundle.AddResourceEntry(Header, "urn:uuid:" + Header.Id);
            }
        }

        /// <summary>Message Type</summary>
        /// <value>the message type.</value>
        public string MessageType
        {
            get
            {
                if (Header?.Event != null && Header.Event.TypeName == "uri")
                {
                    return ((FhirUri)Header.Event).Value;
                }
                else
                {
                    return null;
                }
                
            }
            set
            {
                Header.Event = new FhirUri(value);
            }
        }

        /// <summary>Message Source</summary>
        /// <value>the message source.</value>
        public string MessageSource
        {
            get
            {
                return Header?.Source.Endpoint;
            }
            set
            {
                Header.Source.Endpoint = value;
            }
        }

        /// <summary>Message Destination</summary>
        /// <value>the message destination.</value>
        public string MessageDestination
        {
            get
            {
                return Header?.Destination.ToArray()[0].Endpoint;
            }
            set
            {
                Header.Destination.Clear();
                MessageHeader.MessageDestinationComponent dest = new MessageHeader.MessageDestinationComponent();
                dest.Endpoint = value;
                Header.Destination.Add(dest);
            }
        }

        /// <summary>Jurisdiction-assigned death certificate number</summary>
        public string CertificateNumber
        {
            get
            {
                return Record?.GetSingleValue<FhirString>("cert_no")?.Value;
            }
            set
            {
                Record.Remove("cert_no");
                if (value != null)
                {
                    Record.Add("cert_no", new FhirString(value));
                }
            }
        }

        /// <summary>Jurisdiction-assigned auxiliary identifier</summary>
        public string StateAuxiliaryIdentifier
        {
            get
            {
                return Record?.GetSingleValue<FhirString>("state_auxiliary_id")?.Value;
            }
            set
            {
                Record.Remove("state_auxiliary_id");
                if (value != null)
                {
                    Record.Add("state_auxiliary_id", new FhirString(value));
                }
            }
        }

        /// <summary>NCHS identifier. Format is 4-digit year, two character jurisdiction id, six character/digit certificate id.</summary>
        public string NCHSIdentifier
        {
            get
            {
                return Record?.GetSingleValue<FhirString>("nchs_id")?.Value;
            }
            set
            {
                Record.Remove("nchs_id");
                if (value != null)
                {
                    Record.Add("nchs_id", new FhirString(value));
                }
            }
        }

        /// <summary>Create an NCHS identifier for the supplied DeathRecord.</summary>
        /// <param name="record">the DeathRecord from which the year of death, jurisdiction of death and certificate id will be extracted.</param>
        /// <returns>the NCHS compound identifier for the supplied DeathRecord.</returns>
        protected static string CreateNCHSIdentifier(DeathRecord record)
        {
            // The following code may not be required in the future if the VRDR FHIR IG
            // specifies that the NCHS identifier is carried directly in a Death Certificate document.
            var ije = new IJEMortality(record);
            string year = ije.DOD_YR;
            string state = ije.DSTATE;
            string file = ije.FILENO;
            if (year.Trim().Length == 0 || state.Trim().Length == 0 || file.Equals("000000"))
            {
                return null;
            }
            return year+state+file;
        }

        /// <summary>
        /// Parse an XML or JSON serialization of a FHIR Bundle and construct the appropriate subclass of
        /// BaseMessage. The new object is checked to ensure it the same or a subtype of the type parameter.
        /// </summary>
        /// <typeparam name="T">the expected message type</typeparam>
        /// <param name="source">the XML or JSON serialization of a FHIR Bundle</param>
        /// <param name="permissive">if the parser should be permissive when parsing the given string</param>
        /// <returns>The deserialized message object</returns>
        /// <exception cref="MessageParseException">Thrown when source does not represent the same or a subtype of the type parameter.</exception>
        public static T Parse<T>(StreamReader source, bool permissive = false) where T: BaseMessage
        {
            BaseMessage typedMessage = Parse(source, permissive);
            if (!typeof(T).IsInstanceOfType(typedMessage))
            {
                throw new MessageParseException($"The supplied message was of type {typedMessage.GetType()}, expected {typeof(T)} or a subclass", typedMessage);
            }
            return (T)typedMessage;
        }

        /// <summary>
        /// Parse an XML or JSON serialization of a FHIR Bundle and construct the appropriate subclass of
        /// BaseMessage. The new object is checked to ensure it the same or a subtype of the type parameter.
        /// </summary>
        /// <typeparam name="T">the expected message type</typeparam>
        /// <param name="source">the XML or JSON serialization of a FHIR Bundle</param>
        /// <param name="permissive">if the parser should be permissive when parsing the given string</param>
        /// <returns>the deserialized message object</returns>
        /// <exception cref="MessageParseException">thrown when source does not represent the same or a subtype of the type parameter.</exception>
        public static T Parse<T>(string source, bool permissive = false) where T: BaseMessage
        {
            BaseMessage typedMessage = Parse(source, permissive);
            if (!typeof(T).IsInstanceOfType(typedMessage))
            {
                throw new MessageParseException($"The supplied message was of type {typedMessage.GetType()}, expected {typeof(T)} or a subclass", typedMessage);
            }
            return (T)typedMessage;
        }

        /// <summary>
        /// Parse an XML or JSON serialization of a FHIR Bundle and construct the appropriate subclass of
        /// BaseMessage. Clients can use the typeof operator to determine the type of message object returned.
        /// </summary>
        /// <param name="source">the XML or JSON serialization of a FHIR Bundle</param>
        /// <param name="permissive">if the parser should be permissive when parsing the given string</param>
        /// <returns>The deserialized message object</returns>
        public static BaseMessage Parse(string source, bool permissive = false)
        {
            Bundle bundle = null;
            if (!String.IsNullOrEmpty(source) && source.TrimStart().StartsWith("<"))
            {
                bundle = ParseXML(source, permissive);
            }
            else if (!String.IsNullOrEmpty(source) && source.TrimStart().StartsWith("{"))
            {
                bundle = ParseJSON(source, permissive);
            }
            else
            {
                throw new System.ArgumentException("The given input does not appear to be a valid XML or JSON FHIR message.");
            }

            BaseMessage message = new BaseMessage(bundle, true, false);
            switch (message.MessageType)
            {
                case "http://nchs.cdc.gov/vrdr_submission":
                    message = new DeathRecordSubmission(bundle, message);
                    break;
                case "http://nchs.cdc.gov/vrdr_submission_update":
                    message = new DeathRecordUpdate(bundle, message);
                    break;
                case "http://nchs.cdc.gov/vrdr_acknowledgement":
                    message = new AckMessage(bundle);
                    break;
                case "http://nchs.cdc.gov/vrdr_submission_void":
                    message = new VoidMessage(bundle);
                    break;
                case "http://nchs.cdc.gov/vrdr_coding":
                    message = new CodingResponseMessage(bundle);
                    break;
                case "http://nchs.cdc.gov/vrdr_coding_update":
                    message = new CodingUpdateMessage(bundle);
                    break;
                case "http://nchs.cdc.gov/vrdr_extraction_error":
                    message = new ExtractionErrorMessage(bundle, message);
                    break;
                default:
                    string errorText;
                    if (message.Header == null)
                    {
                        errorText = "Failed to find a Bundle Entry containing a Resource of type MessageHeader";
                    }
                    else if (message.MessageType == null)
                    {
                        errorText = "Message type was missing from MessageHeader";
                    }
                    else
                    {
                        errorText = $"Unsupported message type: {message.MessageType}";
                    }
                    throw new MessageParseException(errorText, message);
            }
            return message;
        }

        /// <summary>
        /// Parse an XML or JSON serialization of a FHIR Bundle and construct the appropriate subclass of
        /// BaseMessage. Clients can use the typeof operator to determine the type of message object returned.
        /// </summary>
        /// <param name="source">the XML or JSON serialization of a FHIR Bundle</param>
        /// <param name="permissive">if the parser should be permissive when parsing the given string</param>
        /// <returns>The deserialized message object</returns>
        public static BaseMessage Parse(StreamReader source, bool permissive = false)
        {
            string content = source.ReadToEnd();
            return Parse(content, permissive);
        }

        private static ParserSettings GetParserSettings(bool permissive)
        {
            return new ParserSettings { AcceptUnknownMembers = permissive,
                                        AllowUnrecognizedEnums = permissive,
                                        PermissiveParsing = permissive };
        }

        private static Bundle ParseXML(string content, bool permissive)
        {
            Bundle bundle = null;

            // Grab all errors found by visiting all nodes and report if not permissive
            if (!permissive)
            {
                List<string> entries = new List<string>();
                ISourceNode node = FhirXmlNode.Parse(content, new FhirXmlParsingSettings { PermissiveParsing = permissive });
                foreach (Hl7.Fhir.Utility.ExceptionNotification problem in node.VisitAndCatch())
                {
                    entries.Add(problem.Message);
                }
                if (entries.Count > 0)
                {
                    throw new System.ArgumentException(String.Join("; ", entries).TrimEnd());
                }
            }
            // Try Parse
            try
            {
                FhirXmlParser parser = new FhirXmlParser(GetParserSettings(permissive));
                bundle = parser.Parse<Bundle>(content);
            }
            catch (Exception e)
            {
                throw new System.ArgumentException(e.Message);
            }
            
            return bundle;
        }

        private static Bundle ParseJSON(string content, bool permissive)
        {
            Bundle bundle = null;

            // Grab all errors found by visiting all nodes and report if not permissive
            if (!permissive)
            {
                List<string> entries = new List<string>();
                ISourceNode node = FhirJsonNode.Parse(content, "Bundle", new FhirJsonParsingSettings { PermissiveParsing = permissive });
                foreach (Hl7.Fhir.Utility.ExceptionNotification problem in node.VisitAndCatch())
                {
                    entries.Add(problem.Message);
                }
                if (entries.Count > 0)
                {
                    throw new System.ArgumentException(String.Join("; ", entries).TrimEnd());
                }
            }
            // Try Parse
            try
            {
                FhirJsonParser parser = new FhirJsonParser(GetParserSettings(permissive));
                bundle = parser.Parse<Bundle>(content);
            }
            catch (Exception e)
            {
                throw new System.ArgumentException(e.Message);
            }

            return bundle;
        }
    }

    /// <summary>
    /// An exception that may be thrown during message parsing
    /// </summary>
    public class MessageParseException : System.ArgumentException
    {
        private BaseMessage sourceMessage;

        /// <summary>
        /// Construct a new instance.
        /// </summary>
        /// <param name="errorMessage">A text error message describing the problem</param>
        /// <param name="sourceMessage">The message that cuased the problem</param>
        public MessageParseException(string errorMessage, BaseMessage sourceMessage) : base(errorMessage)
        {
            this.sourceMessage = sourceMessage;
        }

        /// <summary>
        /// Build an ExtractionErrorMessage that conveys the issues reported in this exception.
        /// </summary>
        public ExtractionErrorMessage CreateExtractionErrorMessage()
        {
            var message = new ExtractionErrorMessage(sourceMessage);
            message.Issues.Add(new Issue(OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Exception, this.Message));
            return message;
        }
    }
}
