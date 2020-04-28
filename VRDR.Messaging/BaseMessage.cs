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

        /// <summary>MessageHeader that contains the message header.</summary>
        protected MessageHeader Header;

        /// <summary>
        /// Construct a BaseMessage from a FHIR Bundle. This constructor will also validate that the Bundle
        /// represents a FHIR message of the correct type.
        /// </summary>
        /// <param name="messageBundle">a FHIR Bundle that will be used to initialize the BaseMessage</param>
        protected BaseMessage(Bundle messageBundle)
        {
            MessageBundle = messageBundle;

            // Validate bundle type is message
            if (messageBundle?.Type != Bundle.BundleType.Message)
            {
                throw new System.ArgumentException($"The FHIR Bundle must be of type message, not {messageBundle?.Type}");
            }

            // Find Header
            Header = findEntry<MessageHeader>(ResourceType.MessageHeader);
        }

        /// <summary>
        /// Find the first Entry within the message Bundle that contains a Resource of the specified type and return that resource.
        /// </summary>
        /// <param name="type">the type of FHIR resource to look for</param>
        /// <typeparam name="T">the class of the FHIR resource to return, must match with specified type:</typeparam>
        /// <returns></returns>
        protected T findEntry<T>(ResourceType type) where T : Resource
        {
            var typedEntry = MessageBundle.Entry.FirstOrDefault( entry => entry.Resource.ResourceType == type );
            if (typedEntry == null)
            {
                throw new System.ArgumentException($"Failed to find a Bundle Entry containing a Resource of type {type.ToString()}");
            }
            return (T)typedEntry.Resource;
        }

        /// <summary>Constructor that creates a new, empty message for the specified message type.</summary>
        public BaseMessage(String messageType)
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
            src.Endpoint = "nightingale";
            Header.Source = src;

            MessageBundle.AddResourceEntry(Header, "urn:uuid:" + Header.Id);
        }

        /// <summary>Helper method to return a XML string representation of this DeathRecordSubmission.</summary>
        /// <returns>a string representation of this DeathRecordSubmission in XML format</returns>
        public string ToXML()
        {
            return MessageBundle.ToXml();
        }

        /// <summary>Helper method to return a XML string representation of this DeathRecordSubmission.</summary>
        /// <returns>a string representation of this DeathRecordSubmission in XML format</returns>
        public string ToXml()
        {
            return MessageBundle.ToXml();
        }

        /// <summary>Helper method to return a JSON string representation of this DeathRecordSubmission.</summary>
        /// <returns>a string representation of this DeathRecordSubmission in JSON format</returns>
        public string ToJSON()
        {
            return MessageBundle.ToJson();
        }

        /// <summary>Helper method to return a JSON string representation of this DeathRecordSubmission.</summary>
        /// <returns>a string representation of this DeathRecordSubmission in JSON format</returns>
        public string ToJson()
        {
            return MessageBundle.ToJson();
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
                return Header.Id;
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
                if (Header.Event != null && Header.Event.TypeName == "uri")
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
                return Header.Source.Endpoint;
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
                return Header.Destination.ToArray()[0].Endpoint;
            }
            set
            {
                Header.Destination.Clear();
                MessageHeader.MessageDestinationComponent dest = new MessageHeader.MessageDestinationComponent();
                dest.Endpoint = value;
                Header.Destination.Add(dest);
            }
        }

        /// <summary>
        /// Parse an XML or JSON serialization of a FHIR Bundle and construct the appropriate subclass of
        /// BaseMessage. The new object is checked to ensure it the same or a subtype of the type parameter.
        /// </summary>
        /// <param name="source">the XML or JSON serialization of a FHIR Bundle</param>
        /// <param name="permissive">if the parser should be permissive when parsing the given string</param>
        /// <returns>The deserialized message object</returns>
        /// <exception cref="System.ArgumentException">Thrown when source does not represent the same or a subtype of the type parameter.</exception>
        public static T Parse<T>(StreamReader source, bool permissive = false) where T: BaseMessage
        {
            BaseMessage typedMessage = Parse(source, permissive);
            if (!typeof(T).IsInstanceOfType(typedMessage))
            {
                throw new System.ArgumentException($"The supplied message was of type {typedMessage.GetType()}, expected {typeof(T)} or a subclass");
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
        public static BaseMessage Parse(StreamReader source, bool permissive = false)
        {
            string content = source.ReadToEnd();
            Bundle bundle = null;
            if (!String.IsNullOrEmpty(content) && content.TrimStart().StartsWith("<"))
            {
                bundle = ParseXML(content, permissive);
            }
            else if (!String.IsNullOrEmpty(content) && content.TrimStart().StartsWith("{"))
            {
                bundle = ParseJSON(content, permissive);
            }
            else
            {
                throw new System.ArgumentException("The given input does not appear to be a valid XML or JSON FHIR message.");
            }

            BaseMessage typedMessage = null;
            switch (GetMessageType(bundle))
            {
                case "http://nchs.cdc.gov/vrdr_submission":
                    typedMessage = new DeathRecordSubmission(bundle);
                    break;
                case "http://nchs.cdc.gov/vrdr_submission_update":
                    typedMessage = new DeathRecordUpdate(bundle);
                    break;
                case "http://nchs.cdc.gov/vrdr_acknowledgement":
                    typedMessage = new AckMessage(bundle);
                    break;
                case "http://nchs.cdc.gov/vrdr_submission_void":
                    typedMessage = new VoidMessage(bundle);
                    break;
                case "http://nchs.cdc.gov/vrdr_coding":
                    typedMessage = new CodingResponseMessage(bundle);
                    break;
                case "http://nchs.cdc.gov/vrdr_coding_update":
                    typedMessage = new CodingUpdateMessage(bundle);
                    break;
                case "http://nchs.cdc.gov/vrdr_extraction_error":
                    typedMessage = new ExtractionErrorMessage(bundle);
                    break;
                case "http://nchs.cdc.gov/vrdr_coding_error":
                    typedMessage = new CodingErrorMessage(bundle);
                    break;
                default:
                    throw new System.ArgumentException($"Unsupported message type: {GetMessageType(bundle)}");
            }
            return typedMessage;
        }

        private static string GetMessageType(Bundle bundle)
        {
            var baseMessage = new BaseMessage(bundle);
            return baseMessage.MessageType;
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
}
