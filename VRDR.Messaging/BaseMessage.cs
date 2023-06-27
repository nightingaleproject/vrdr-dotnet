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
    public partial class BaseMessage
    {
        /// <summary>Bundle that contains the message.</summary>
        public Bundle MessageBundle { get; protected set; }

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
            Header = findEntry<MessageHeader>(ignoreMissingEntries);

            // Find Parameters
            Record = findEntry<Parameters>(ignoreMissingEntries);
        }

        /// <summary>
        /// Find the first Entry within the message Bundle that contains a Resource of the specified type and return that resource.
        /// </summary>
        /// <param name="ignoreMissingEntries">if true, then missing entries will not result in an exception</param>
        /// <typeparam name="T">the class of the FHIR resource to return, must match with specified type:</typeparam>
        /// <returns>The first matching Bundle entry</returns>
        protected T findEntry<T>(bool ignoreMissingEntries = false) where T : Resource
        {
            var typedEntry = MessageBundle.Entry.FirstOrDefault( entry => entry.Resource is T);
            if (typedEntry == null && !ignoreMissingEntries)
            {
                throw new System.ArgumentException($"Failed to find a Bundle Entry containing a Resource of type {typeof(T).FullName}");
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
            dest.Endpoint = DeathRecordSubmissionMessage.MESSAGE_TYPE;
            Header.Destination.Add(dest);
            MessageHeader.MessageSourceComponent src = new MessageHeader.MessageSourceComponent();
            Header.Source = src;
            MessageBundle.AddResourceEntry(Header, "urn:uuid:" + Header.Id);

            Record = new Parameters();
            Record.Id = Guid.NewGuid().ToString();
            MessageBundle.AddResourceEntry(this.Record, "urn:uuid:" + this.Record.Id);
            Header.Focus.Add(new ResourceReference("urn:uuid:" + this.Record.Id));
        }

        /// <summary>
        /// Extract the business identifiers for the message from the supplied death record.
        /// </summary>
        /// <param name="from">the death record to extract the identifiers from</param>
        protected void ExtractBusinessIdentifiers(DeathRecord from)
        {
            uint certificateNumber;
            if (UInt32.TryParse(from?.Identifier, out certificateNumber))
            {
                this.CertNo = certificateNumber;
            }
            // take the first state local identifier if it exists
            if (from?.StateLocalIdentifier1 != null)
            {
                this.StateAuxiliaryId = from.StateLocalIdentifier1;
            }
            else
            {
                this.StateAuxiliaryId = null;
            }
            if (from?.DeathYear != null)
            {
                this.DeathYear = (uint)from.DeathYear;
            }
            this.JurisdictionId = from?.DeathLocationJurisdiction;
        }

        /// <summary>
        /// Update the record bundle in this message based on the MessageBundleRecord property (for whichever subclass we're instantiated as).
        /// Important if we're managing a death record that might have changed.
        /// </summary>
        protected void UpdateMessageBundleRecord()
        {
            MessageBundle.Entry.RemoveAll( entry => entry.Resource is Bundle);
            Header.Focus.Clear();
            Bundle newBundle = MessageBundleRecord;
            if (newBundle != null)
            {
                MessageBundle.AddResourceEntry(newBundle, "urn:uuid:" + newBundle.Id);
                Header.Focus.Add(new ResourceReference("urn:uuid:" + newBundle.Id));
            }
        }

        /// <summary>
        /// Allow explicit casting of a message into a bundle
        /// </summary>
        /// <param name="message">the death record to extract the bundle from</param>
        public static explicit operator Bundle(BaseMessage message) => message.MessageBundle;

        /// <summary>Helper method to return a XML string representation of this DeathRecordSubmissionMessage.</summary>
        /// <param name="prettyPrint">controls whether the returned string is formatted for human readability (true) or compact (false)</param>
        /// <returns>a string representation of this DeathRecordSubmissionMessage in XML format</returns>
        public string ToXML(bool prettyPrint = false)
        {
            UpdateMessageBundleRecord(); // Update the record in the message bundle in case the DeathRecord (if present) has changed
            return MessageBundle.ToXml(new FhirXmlSerializationSettings { Pretty = prettyPrint, AppendNewLine = prettyPrint, TrimWhitespaces = prettyPrint });
        }

        /// <summary>Helper method to return a XML string representation of this DeathRecordSubmissionMessage.</summary>
        /// <param name="prettyPrint">controls whether the returned string is formatted for human readability (true) or compact (false)</param>
        /// <returns>a string representation of this DeathRecordSubmissionMessage in XML format</returns>
        public string ToXml(bool prettyPrint = false)
        {
            return ToXML(prettyPrint);
        }

        /// <summary>Helper method to return a JSON string representation of this DeathRecordSubmissionMessage.</summary>
        /// <param name="prettyPrint">controls whether the returned string is formatted for human readability (true) or compact (false)</param>
        /// <returns>a string representation of this DeathRecordSubmissionMessage in JSON format</returns>
        public string ToJSON(bool prettyPrint = false)
        {
            UpdateMessageBundleRecord(); // Update the record in the message bundle in case the DeathRecord (if present) has changed
            return MessageBundle.ToJson(new FhirJsonSerializationSettings { Pretty = prettyPrint, AppendNewLine = prettyPrint });
        }

        /// <summary>Helper method to return a JSON string representation of this DeathRecordSubmissionMessage.</summary>
        /// <param name="prettyPrint">controls whether the returned string is formatted for human readability (true) or compact (false)</param>
        /// <returns>a string representation of this DeathRecordSubmissionMessage in JSON format</returns>
        public string ToJson(bool prettyPrint = false)
        {
            return ToJSON(prettyPrint);
        }

        /////////////////////////////////////////////////////////////////////////////////
        //
        // Message Properties
        //
        /////////////////////////////////////////////////////////////////////////////////

        /// <summary>The record bundle that should go into the message bundle for this message</summary>
        /// <value>the MessageBundleRecord</value>
        protected virtual Bundle MessageBundleRecord
        {
            get
            {
                // The base message class and some subclasses do not have a record
                return null;
            }
        }

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
                MessageBundle.Entry.RemoveAll( entry => entry.Resource is MessageHeader );
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
                return Header?.Source?.Endpoint;
            }
            set
            {
                if (Header.Source == null)
                {
                    Header.Source = new MessageHeader.MessageSourceComponent();
                }
                Header.Source.Endpoint = value;
            }
        }

        /// <summary>Message Destination</summary>
        /// <value>the message destinations, in csv format to support multiple endpoints. Acts as a wrapper for MessageDestinations while still maintaining backwards compatibility.</value>
        public string MessageDestination
        {
            get
            {
                List<string> destinations = this.MessageDestinations;
                if (destinations == null || (destinations.Count() == 1 && destinations[0] == null) || destinations.Count() < 1) {
                    return null;
                }
                return String.Join(",", this.MessageDestinations);
            }
            set
            {
                this.MessageDestinations = value != null ? value.Split(',').ToList() : null;
            }
        }

        /// <summary>Message Destinations</summary>
        /// <value>the message destinations in list-based format.</value>
        public List<string> MessageDestinations
        {
            get
            {
                return Header?.Destination?.Select(dest => dest.Endpoint).ToList();
            }
            set
            {
                Header.Destination.Clear();
                if (value == null)
                {
                    MessageHeader.MessageDestinationComponent dest = new MessageHeader.MessageDestinationComponent();
                    dest.Endpoint = null;
                    Header.Destination.Add(dest);
                    return;
                }
                foreach (string endpoint in value) {
                    MessageHeader.MessageDestinationComponent dest = new MessageHeader.MessageDestinationComponent();
                    dest.Endpoint = endpoint;
                    Header.Destination.Add(dest);
                }
            }
        }

        /// <summary>Helper method to set a single string value on the Record portion of the Message</summary>
        protected void SetSingleStringValue(string key, string value)
        {
            Record.Remove(key);
            if (!String.IsNullOrWhiteSpace(value))
            {
                Record.Add(key, new FhirString(value));
            }
        }

        /// <summary>Jurisdiction-assigned death certificate number</summary>
        public uint? CertNo
        {
            get
            {
                return (uint?)Record?.GetSingleValue<UnsignedInt>("cert_no")?.Value;
            }
            set
            {
                Record.Remove("cert_no");
                if (value != null)
                {
                    if (value > 999999)
                    {
                        throw new ArgumentException("Certificate number must be a maximum of six digits");
                    }
                    Record.Add("cert_no", new UnsignedInt((int)value));
                }
            }
        }

        /// <summary>Jurisdiction-assigned auxiliary identifier</summary>
        public string StateAuxiliaryId
        {
            get
            {
                return Record?.GetSingleValue<FhirString>("state_auxiliary_id")?.Value;
            }
            set
            {
                SetSingleStringValue("state_auxiliary_id", value);
            }
        }

        /// <summary>The year in which the death occurred</summary>
        public uint? DeathYear
        {
            get
            {
                return (uint?)Record?.GetSingleValue<UnsignedInt>("death_year")?.Value;
            }
            set
            {
                Record.Remove("death_year");
                if (value != null)
                {
                    if (value < 1000 || value > 9999) {
                        throw new ArgumentException("Year of death must be specified using four digits");
                    }
                    Record.Add("death_year", new UnsignedInt((int)value));
                }
            }
        }

        /// <summary>Two character identifier of the jurisdiction in which the death occurred</summary>
        public string JurisdictionId
        {
            get
            {
                return Record?.GetSingleValue<FhirString>("jurisdiction_id")?.Value;
            }
            set
            {
                Record.Remove("jurisdiction_id");
                if (value != null)
                {
                    if (value.Length != 2)
                    {
                        throw new ArgumentException("Jurisdiction ID must be a two character string");
                    }
                    Record.Add("jurisdiction_id", new FhirString(value));
                }
            }
        }

        /// <summary>NCHS identifier. Format is 4-digit year, two character jurisdiction id, six character/digit certificate id.</summary>
        public string NCHSIdentifier
        {
            get
            {
                if (DeathYear == null || JurisdictionId == null || CertNo == null)
                {
                    return null;
                }
                return DeathYear.Value.ToString("D4") + JurisdictionId + CertNo.Value.ToString("D6");
            }
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
        /// Construct the appropriate subclass of BaseMessage based on a FHIR Bundle.
        /// The new object is checked to ensure it the same or a subtype of the type parameter.
        /// </summary>
        /// <typeparam name="T">the expected message type</typeparam>
        /// <param name="bundle">A FHIR Bundle</param>
        /// <returns>The message object of the appropriate message type</returns>
        /// <exception cref="MessageParseException">Thrown when source does not represent the same or a subtype of the type parameter.</exception>
        public static T Parse<T>(Bundle bundle) where T: BaseMessage
        {
            BaseMessage typedMessage = Parse(bundle);
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
            Bundle bundle = ParseGenericBundle(source, permissive);

            return Parse(bundle);
        }

        /// <summary>
        /// Parse an XML or JSON serialization of a FHIR Bundle and construct a generic BaseMessage.
        /// </summary>
        /// <param name="source">the XML or JSON serialization of a FHIR Bundle</param>
        /// <param name="permissive">if the parser should be permissive when parsing the given string</param>
        /// <returns>The deserialized base message object</returns>
        public static BaseMessage ParseGenericMessage(string source, bool permissive = false)
        {
            Bundle bundle = ParseGenericBundle(source, permissive);
            BaseMessage message = new BaseMessage(bundle, true, false);
            return message;
        }

        /// <summary>
        /// Parse an XML or JSON serialization of a FHIR Bundle.
        /// </summary>
        /// <param name="source">the XML or JSON serialization of a FHIR Bundle</param>
        /// <param name="permissive">if the parser should be permissive when parsing the given string</param>
        /// <returns>The deserialized bundle object</returns>
        public static Bundle ParseGenericBundle(string source, bool permissive = false)
        {
            if (!String.IsNullOrEmpty(source) && source.TrimStart().StartsWith("<"))
            {
                return ParseXML(source, permissive);
            }
            else if (!String.IsNullOrEmpty(source) && source.TrimStart().StartsWith("{"))
            {
                return ParseJSON(source, permissive);
            }
            else
            {
                throw new System.ArgumentException("The given input does not appear to be a valid XML or JSON FHIR message.");
            }
        }

        /// <summary>
        /// Construct the appropriate subclass of BaseMessage based on a FHIR Bundle.
        /// Clients can use the typeof operator to determine the type of message object returned.
        /// </summary>
        /// <param name="bundle">A FHIR Bundle</param>
        /// <returns>The message object of the appropriate message type</returns>
        public static BaseMessage Parse(Bundle bundle)
        {
            BaseMessage message = new BaseMessage(bundle, true, false);
            switch (message.MessageType)
            {
                case DeathRecordSubmissionMessage.MESSAGE_TYPE:
                    message = new DeathRecordSubmissionMessage(bundle, message);
                    break;
                case DeathRecordUpdateMessage.MESSAGE_TYPE:
                    message = new DeathRecordUpdateMessage(bundle, message);
                    break;
                case AcknowledgementMessage.MESSAGE_TYPE:
                    message = new AcknowledgementMessage(bundle);
                    break;
                case DeathRecordVoidMessage.MESSAGE_TYPE:
                    message = new DeathRecordVoidMessage(bundle);
                    break;
                case DeathRecordAliasMessage.MESSAGE_TYPE:
                    message = new DeathRecordAliasMessage(bundle);
                    break;
                case CauseOfDeathCodingMessage.MESSAGE_TYPE:
                    message = new CauseOfDeathCodingMessage(bundle, message);
                    break;
                case DemographicsCodingMessage.MESSAGE_TYPE:
                    message = new DemographicsCodingMessage(bundle, message);
                    break;
                case CauseOfDeathCodingUpdateMessage.MESSAGE_TYPE:
                    message = new CauseOfDeathCodingUpdateMessage(bundle, message);
                    break;
                case DemographicsCodingUpdateMessage.MESSAGE_TYPE:
                    message = new DemographicsCodingUpdateMessage(bundle, message);
                    break;
                case ExtractionErrorMessage.MESSAGE_TYPE:
                    message = new ExtractionErrorMessage(bundle, message);
                    break;
                case StatusMessage.MESSAGE_TYPE:
                    message = new StatusMessage(bundle);
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

        /// <summary>
        /// Convert message to message type and extract the death record
        /// </summary>
        /// <param name="message">base message</param>
        /// <returns>The death record inside the base message</returns>
        public static DeathRecord GetDeathRecordFromMessage(BaseMessage message)
        {
                
            Type messageType = message.GetType();

            DeathRecord dr = null;

            switch (messageType.Name)
            {
                case "DeathRecordSubmissionMessage":
                {
                    var drsm = message as DeathRecordSubmissionMessage;
                    dr = drsm?.DeathRecord;
                    break;
                }
                case "DeathRecordUpdateMessage":
                {
                    var drsm = message as DeathRecordUpdateMessage;
                    dr = drsm?.DeathRecord;
                    break;
                }
                case "CauseOfDeathCodingMessage":
                {
                    var drsm = message as CauseOfDeathCodingMessage;
                    dr = drsm?.DeathRecord;
                    break;
                }
                case "CauseOfDeathCodingUpdateMessage":
                {
                    var drsm = message as CauseOfDeathCodingUpdateMessage;
                    dr = drsm?.DeathRecord;
                    break;
                }
                case "DemographicsCodingMessage":
                {
                    var drsm = message as DemographicsCodingMessage;
                    dr = drsm?.DeathRecord;
                    break;
                }
                case "DemographicsCodingUpdateMessage":
                {
                    var drsm = message as DemographicsCodingUpdateMessage;
                    dr = drsm?.DeathRecord;
                    break;
                }
            }

            return dr;
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
        /// <param name="sourceMessage">The message that caused the problem</param>
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
