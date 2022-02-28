using System;
using System.Collections.Generic;
using Hl7.Fhir.Model;

namespace VRDR
{
    /// <summary>Class <c>CodingResponseMessage</c> conveys the coded cause of death, race and ethnicity of a decedent.</summary>
    public class CodingResponseMessage : BaseMessage
    {
        /// <summary>Constructor that creates a response for the specified message.</summary>
        /// <param name="sourceMessage">the message to create a response for.</param>
        /// <param name="source">the endpoint identifier that the message will be sent from.</param>
        public CodingResponseMessage(BaseMessage sourceMessage, string  source = "http://nchs.cdc.gov/vrdr_submission" /* VRDR.URIs.Submission */ ) : this(sourceMessage.MessageSource, source)
        {
            this.CertificateNumber = sourceMessage?.CertificateNumber;
            this.StateAuxiliaryIdentifier = sourceMessage?.StateAuxiliaryIdentifier;
            this.DeathJurisdictionID = sourceMessage?.DeathJurisdictionID;
            this.DeathYear = sourceMessage?.DeathYear;
        }

        /// <summary>
        /// Construct a CodingResponseMessage from a FHIR Bundle.
        /// </summary>
        /// <param name="messageBundle">a FHIR Bundle that will be used to initialize the CodingResponseMessage</param>
        /// <returns></returns>
        internal CodingResponseMessage(Bundle messageBundle) : base(messageBundle)
        {
        }

        /// <summary>Constructor that creates a response for the specified message.</summary>
        /// <param name="destination">the endpoint identifier that the response message will be sent to.</param>
        /// <param name="source">the endpoint identifier that the response message will be sent from.</param>
        public CodingResponseMessage(string destination, string source = "http://nchs.cdc.gov/vrdr_submission") : base("http://nchs.cdc.gov/vrdr_coding")
        {
            Header.Source.Endpoint = source;
            MessageDestination = destination;
        }
        /// <summary>Record axis coding of cause of death</summary>
        public List<string> CauseOfDeathRecordAxis
        {
            get
            {
                var codes = new List<string>();
                IEnumerable<Parameters.ParameterComponent> axisEntries = Record.Get("record_cause_of_death");
                if (axisEntries != null)
                {
                    foreach (Parameters.ParameterComponent entry in axisEntries)
                    {
                        if(entry.Value != null){ // guards against a misformatted entry that has a 'coding' part, instead of a value
                            var code = ((FhirString)entry.Value).Value;
                            codes.Add((string)code);
                        }
                    }
                }
                return codes;
            }
            set
            {
                Record.Remove("record_cause_of_death");
                foreach (string code in value)
                {
                    Record.Add("record_cause_of_death", new FhirString(code));
                }
            }
        }

        /// <summary>Flat list of entity axis (line, position, codes)  </summary>
        public List<CauseOfDeathEntityAxisEntry> CauseOfDeathEntityAxis
        {
            get
            {
                var entityAxisEntries = new List<CauseOfDeathEntityAxisEntry>();
                foreach (Parameters.ParameterComponent part in Record.Get("entity_axis_code"))
                {
                    uint lineNumber = 0;
                    uint position = 0;
                    string code = null;
                    string e_code_indicator = null;
                    foreach (Parameters.ParameterComponent childPart in part.Part)
                    {

                        if (childPart.Name == "lineNumber")
                        {
                            lineNumber = (uint)((UnsignedInt)childPart.Value).Value;
                        }
                        else if (childPart.Name == "position")
                        {
                            position = (uint)((UnsignedInt)childPart.Value).Value;
                        }
                        else if (childPart.Name == "coding")
                        {
                            code = ((FhirString)childPart.Value).Value;
                        }
                        else if (childPart.Name == "e_code_indicator")
                        {
                            e_code_indicator = ((FhirString)childPart.Value).Value;
                        }
                    }
                    var entry = new CauseOfDeathEntityAxisEntry(lineNumber, position, code, e_code_indicator);
                    entityAxisEntries.Add(entry);
                    if(entityAxisEntries.Count > 20){
                        throw new System.ArgumentException($"The maximum number of entityAxisEntries is 20, found: {entityAxisEntries.Count}");
                    }
                    lineNumber = 0;
                    position = 0;
                    code = "";
                }
                return entityAxisEntries;
            }
            set
            {
                Record.Remove("entity_axis_code");
                foreach (CauseOfDeathEntityAxisEntry entry in value)
                {
                   var list = new List<Tuple<string, Base>>();
                   list.Add( Tuple.Create("lineNumber", (Base)(new UnsignedInt((int)entry.LineNumber))));
                   list.Add(Tuple.Create("position", (Base)(new UnsignedInt((int)entry.Position))));
                   list.Add(Tuple.Create("coding", (Base)(new FhirString(entry.Code))));
                   if(entry.E_code_indicator != null){
                        list.Add(Tuple.Create("e_code_indicator", (Base)(new FhirString(entry.E_code_indicator))));
                   }
                   Record.Add("entity_axis_code", list);
                }
            }
        }
    }


    /// <summary>Class for structuring a cause of death entity axis entry</summary>
    public class CauseOfDeathEntityAxisEntry
    {
        /// <summary>Identifies the line number (values "1" to "6") of the corresponding cause of death entered on the
        /// death certificate. The following list shows the corresponding line in the death certificate for each value.</summary>
        /// <list type="number">
        /// <item>Part I. Line a</item>
        /// <item>Part I. Line b</item>
        /// <item>Part I. Line c</item>
        /// <item>Part I. Line d</item>
        /// <item>Part I. Line e</item>
        /// <item>Part II</item>
        /// </list>
        /// There can be up to 20 total entries, with up to 5 entries per line.
        /// <summary>A line number from the death certificate.</summary>
        public readonly uint LineNumber;
        /// <summary>The position of the code within the line.</summary>
        public readonly uint Position;

        /// <summary>The code assigned for one of the cause of death entries in the death certificate.</summary>
        public readonly string Code;

        /// <summary>A holdover from ICD-9 days.  Normally absent.  When present should be an ampersand.</summary>
        public readonly string E_code_indicator;

        /// <summary>Create a CauseOfDeathEntityAxisEntry with the specified line identifier</summary>
        /// <param name="lineNumber"><see cref="LineNumber"/></param>
        /// <param name="position"><see cref="Position"/></param>
        /// <param name="code"><see cref="Code"/></param>
        /// <param name="e_code_indicator"><see cref="E_code_indicator"/></param>
        public CauseOfDeathEntityAxisEntry(uint lineNumber, uint position, string code, string e_code_indicator = null)
        {
            if (lineNumber < 1 || lineNumber > 6 || position < 1 || position > 20 || code.Length < 3)
            {
                throw new System.ArgumentException($"Invalid arguments lineNumber({lineNumber}), position({position}, code{code})");
            }
            if (e_code_indicator != null && e_code_indicator != "&")
            {
                throw new System.ArgumentException($"The value of the e-code-indicator argument must be \"&\", found: {e_code_indicator}");
            }
            this.LineNumber = lineNumber;
            this.Position = position;
            this.Code = code;
            this.E_code_indicator = e_code_indicator;
        }
    }

    /// <summary>Class <c>CodingUpdateMessage</c> conveys an updated coded cause of death, race and ethnicity of a decedent.</summary>
    public class CodingUpdateMessage : CodingResponseMessage
    {
        /// <summary>Constructor that creates an update for the specified message.</summary>
        /// <param name="sourceMessage">the message to create a response for.</param>
        /// <param name="source">the endpoint identifier that the message will be sent from.</param>
        public CodingUpdateMessage(BaseMessage sourceMessage, string source = "http://nchs.cdc.gov/vrdr_submission") : this(sourceMessage.MessageSource, source)
        {
        }

        /// <summary>
        /// Construct a CodingResponseMessage from a FHIR Bundle.
        /// </summary>
        /// <param name="messageBundle">a FHIR Bundle that will be used to initialize the CodingResponseMessage</param>
        /// <returns></returns>
        internal CodingUpdateMessage(Bundle messageBundle) : base(messageBundle)
        {
        }
// SPLIT THIS INTO DEMOGRAPHIC AND CAUSE OF DEATH CODING
        /// <summary>Constructor that creates a response for the specified message.</summary>
        /// <param name="destination">the endpoint identifier that the response message will be sent to.</param>
        /// <param name="source">the endpoint identifier that the response message will be sent from.</param>
        public CodingUpdateMessage(string destination, string source = "http://nchs.cdc.gov/vrdr_submission") : base(destination, source)
        {
            MessageType = VRDR.URIs.CodingUpdate;
        }
    }
}
