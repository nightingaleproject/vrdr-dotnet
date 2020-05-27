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
        public CodingResponseMessage(BaseMessage sourceMessage, string source = "http://nchs.cdc.gov/vrdr_submission") : this(sourceMessage.MessageSource, source)
        {
            this.CertificateNumber = sourceMessage?.CertificateNumber;
            this.StateAuxiliaryIdentifier = sourceMessage?.StateAuxiliaryIdentifier;
            this.NCHSIdentifier = sourceMessage?.NCHSIdentifier;
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

        /// <summary>Ethnicity codes</summary>
        public enum HispanicOrigin
        {
            /// <summary>Edited Hispanic Origin Code</summary>
            DETHNICE,
            /// <summary>Hispanic Code for Literal</summary>
            DETHNIC5C
        }

        /// <summary>Decedent ethnicity coding</summary>
        public Dictionary<HispanicOrigin, string> Ethnicity
        {
            get
            {
                var ethnicity = new Dictionary<HispanicOrigin, string>();
                Parameters.ParameterComponent ethnicityEntry = Record.GetSingle("ethnicity");
                if (ethnicityEntry != null)
                {
                    foreach (Parameters.ParameterComponent entry in ethnicityEntry.Part)
                    {
                        Enum.TryParse(entry.Name, out HispanicOrigin code);
                        var coding = (Coding)entry.Value;
                        ethnicity.Add(code, coding.Code);
                    }
                }
                return ethnicity;
            }
            set
            {
                Record.Remove("ethnicity");
                var list = new List<Tuple<string, Base>>();
                foreach (KeyValuePair<HispanicOrigin, string> item in value)
                {
                    var part = Tuple.Create(item.Key.ToString(), 
                        (Base)(new Coding("https://www.cdc.gov/nchs/data/dvs/HispanicCodeTitles.pdf", item.Value)));
                    list.Add(part);
                }
                Record.Add("ethnicity", list);
            }
        }

        /// <summary>Race codes</summary>
        public enum RaceCode
        {
            /// <summary>First Edited Code</summary>
            RACE1E,
            /// <summary>Second Edited Code</summary>
            RACE2E,
            /// <summary>Third Edited Code</summary>
            RACE3E,
            /// <summary>Fourth Edited Code</summary>
            RACE4E,
            /// <summary>Fifth Edited Code</summary>
            RACE5E,
            /// <summary>Sixth Edited Code</summary>
            RACE6E,
            /// <summary>Seventh Edited Code</summary>
            RACE7E,
            /// <summary>Eighth Edited Code</summary>
            RACE8E,
            /// <summary>First Am. Ind Code</summary>
            RACE16C,
            /// <summary>Second Am. Ind Code</summary>
            RACE17C,
            /// <summary>First Other Asian Code</summary>
            RACE18C,
            /// <summary>Second Other Asian Code</summary>
            RACE19C,
            /// <summary>First Other PI Code</summary>
            RACE20C,
            /// <summary>Second Other Pi Code</summary>
            RACE21C,
            /// <summary>First Other Code</summary>
            RACE22C,
            /// <summary>Second Other Code</summary>
            RACE23C,
            /// <summary>Bridged Multiple Race Code</summary>
            RACEBRG
        }

        /// <summary>Decedent race coding</summary>
        public Dictionary<RaceCode, string> Race
        {
            get
            {
                var race = new Dictionary<RaceCode, string>();
                Parameters.ParameterComponent raceEntry = Record.GetSingle("race");
                if (raceEntry != null)
                {
                    foreach (Parameters.ParameterComponent entry in raceEntry.Part)
                    {
                        Enum.TryParse(entry.Name, out RaceCode code);
                        var coding = (Coding)entry.Value;
                        race.Add(code, coding.Code);
                    }
                }
                return race;
            }
            set
            {
                Record.Remove("race");
                var list = new List<Tuple<string, Base>>();
                foreach (KeyValuePair<RaceCode, string> item in value)
                {
                    var part = Tuple.Create(item.Key.ToString(), 
                        (Base)(new Coding("https://www.cdc.gov/nchs/data/dvs/RaceCodeList.pdf", item.Value)));
                    list.Add(part);
                }
                Record.Add("race", list);
            }
        }

        /// <summary>Underlying cause of death code (ICD-10)</summary>
        public string UnderlyingCauseOfDeath
        {
            get
            {
                return Record.GetSingleValue<Coding>("underlying_cause_of_death")?.Code;
            }
            set
            {
                Record.Remove("underlying_cause_of_death");
                Record.Add("underlying_cause_of_death", new Coding("http://hl7.org/fhir/ValueSet/icd-10", value));
            }
        }

        /// <summary>Record axis coding of cause of death</summary>
        public List<string> CauseOfDeathRecordAxis
        {
            get
            {
                var codes = new List<string>();
                Parameters.ParameterComponent axisEntry = Record.GetSingle("record_cause_of_death");
                if (axisEntry != null)
                {
                    foreach (Parameters.ParameterComponent entry in axisEntry.Part)
                    {
                        var coding = (Coding)entry.Value;
                        codes.Add(coding.Code);
                    }
                }
                return codes;
            }
            set
            {
                Record.Remove("record_cause_of_death");
                var list = new List<Tuple<string, Base>>();
                foreach (string code in value)
                {
                    var part = Tuple.Create("coding", 
                        (Base)(new Coding("http://hl7.org/fhir/ValueSet/icd-10", code)));
                    list.Add(part);
                }
                Record.Add("record_cause_of_death", list);
            }
        }

        /// <summary>Entity axis cause of death coding.</summary>
        public List<CauseOfDeathEntityAxisEntry> CauseOfDeathEntityAxis
        {
            get
            {
                var entityAxisEntries = new List<CauseOfDeathEntityAxisEntry>();
                foreach (Parameters.ParameterComponent part in Record.Get("entity_axis_code"))
                {
                    string lineNumber = "";
                    var codes = new List<string>();
                    foreach (Parameters.ParameterComponent childPart in part.Part)
                    {
                        if (childPart.Name == "lineNumber")
                        {
                            lineNumber = ((Id)childPart.Value).Value;
                        }
                        else if (childPart.Name == "coding")
                        {
                            var coding = (Coding)childPart.Value;
                            codes.Add(coding.Code);
                        }
                    }
                    var entry = new CauseOfDeathEntityAxisEntry(lineNumber, codes);
                    entityAxisEntries.Add(entry);
                }
                return entityAxisEntries;
            }
            set
            {
                Record.Remove("entity_axis_code");
                foreach (CauseOfDeathEntityAxisEntry entry in value)
                {
                    var entityAxisEntry = new List<Tuple<string, Base>>();
                    var part = Tuple.Create("lineNumber", (Base)(new Id(entry.LineNumber)));
                    entityAxisEntry.Add(part);
                    foreach (string code in entry.AssignedCodes)
                    {
                        part = Tuple.Create("coding", (Base)(new Coding("http://hl7.org/fhir/ValueSet/icd-10", code)));
                        entityAxisEntry.Add(part);
                    }
                    Record.Add("entity_axis_code", entityAxisEntry);
                }
            }
        }
    }

    /// <summary>Class for structuring a cause of death entity axis entry</summary>
    public class CauseOfDeathEntityAxisEntry
    {
        /// <summary>Identifies the line number (values "1" to "6") of the corresponding cause of death entered on the 
        /// death certificate. The following list showa the corresponding line in the death certificate for each value.</summary>
        /// <list type="number">
        /// <item>Part I. Line a</item>
        /// <item>Part I. Line b</item>
        /// <item>Part I. Line c</item>
        /// <item>Part I. Line d</item>
        /// <item>Part I. Line e</item>
        /// <item>Part II</item>
        /// </list>
        public readonly string LineNumber;

        /// <summary>The codes assigned for one of the cause of death entries in the death certificate.</summary>
        public readonly List<string> AssignedCodes;

        /// <summary>Create a CauseOfDeathEntityAxisEntry with the specified line identifier</summary>
        /// <param name="lineNumber"><see cref="LineNumber"/></param>
        public CauseOfDeathEntityAxisEntry(string lineNumber)
        {
            this.AssignedCodes = new List<string>();
            this.LineNumber = lineNumber;
        }

        /// <summary>Create a CauseOfDeathEntityAxisEntry with the specified line identifier and corresponding codes</summary>
        /// <param name="lineNumber"><see cref="LineNumber"/></param>
        /// <param name="codes">list of codes</param>
        public CauseOfDeathEntityAxisEntry(string lineNumber, List<string> codes)
        {
            this.AssignedCodes = codes;
            this.LineNumber = lineNumber;
        }
    }

    /// <summary>
    /// Helper class for building a List&lt;CauseOfDeathEntityAxis&gt; from a flat file. Groups codes
    /// into one <c>CauseOfDeathEntityAxisEntry</c> per line and sorts codes by position.
    /// </summary>
    public class CauseOfDeathEntityAxisBuilder
    {
        private SortedDictionary<int, SortedDictionary<int, string>> codes;

        /// <summary>
        /// Construct a new empty instance.
        /// </summary>
        public CauseOfDeathEntityAxisBuilder()
        {
            codes = new SortedDictionary<int, SortedDictionary<int, string>>();
        }

        /// <summary>
        /// Build a List&lt;CauseOfDeathEntityAxis&gt; from the currently contained set of codes.
        /// </summary>
        /// <returns>cause of death entity axis coding list</returns>
        public List<CauseOfDeathEntityAxisEntry> ToCauseOfDeathEntityAxis()
        {
            var list = new List<CauseOfDeathEntityAxisEntry>();
            foreach (KeyValuePair<int, SortedDictionary<int, string>> pair in codes)
            {
                string lineNumber = pair.Key.ToString();
                var entry = new CauseOfDeathEntityAxisEntry(lineNumber);
                foreach (var code in pair.Value.Values)
                {
                    entry.AssignedCodes.Add(code);
                }
                list.Add(entry);
            }
            return list;
        }

        /// <summary>
        /// Add a code to the list of codes that will be used to build a List&lt;CauseOfDeathEntityAxis&gt;.
        /// Order of code addition is not significant, codes will be ordered by <c>line</c> and <c>position</c>
        /// by the <c>ToCauseOfDeathEntityAxis</c> method.
        /// </summary>
        /// <param name="lineNumber"><see cref="CauseOfDeathEntityAxisEntry.LineNumber"/></param>
        /// <param name="position">Sequence within line</param>
        /// <param name="code">ICD code</param>
        /// <exception cref="System.ArgumentException">Thrown if <c>line</c> or <c>position</c> is not a number</exception>
        public void Add(string lineNumber, string position, string code)
        {
            if (!Int32.TryParse(lineNumber, out int lineNumberValue))
            {
                throw new System.ArgumentException($"The value of the line argument must be a number, found: {lineNumber}");
            }
            if (!Int32.TryParse(position, out int positionVal))
            {
                throw new System.ArgumentException($"The value of the position argument must be a number, found: {position}");
            }
            if (code != null && code.Trim().Length > 0) // skip blank codes
            {
                if (!codes.ContainsKey(lineNumberValue))
                {
                    codes[lineNumberValue] = new SortedDictionary<int, string>();
                }
                codes[lineNumberValue][positionVal] = code;
            }
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

        /// <summary>Constructor that creates a response for the specified message.</summary>
        /// <param name="destination">the endpoint identifier that the response message will be sent to.</param>
        /// <param name="source">the endpoint identifier that the response message will be sent from.</param>
        public CodingUpdateMessage(string destination, string source = "http://nchs.cdc.gov/vrdr_submission") : base(destination, source)
        {
            MessageType = "http://nchs.cdc.gov/vrdr_coding_update";
        }
    }
}
