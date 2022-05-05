using System;
using System.Collections.Generic;
using Hl7.Fhir.Model;

namespace VRDR
{
    /// <summary>
    /// A <c>CodingResponseMessage</c> that conveys the coded cause of death information of a decedent.
    /// </summary>
    public class CauseOfDeathCodingMessage : CodingResponseMessage
    {
        /// <summary>
        /// The event URI for CauseOfDeathCodingMessage.
        /// </summary>
        public const String MESSAGE_TYPE = "http://nchs.cdc.gov/vrdr_causeofdeath_coding";

        /// <summary>Constructor that creates a response for the specified message.</summary>
        /// <param name="sourceMessage">the message to create a response for.</param>
        /// <param name="source">the endpoint identifier that the message will be sent from.</param>
        public CauseOfDeathCodingMessage(BaseMessage sourceMessage, string source = "http://nchs.cdc.gov/vrdr_submission") : this(sourceMessage.MessageSource, source)
        {
            this.CertificateNumber = sourceMessage?.CertificateNumber;
            this.StateAuxiliaryIdentifier = sourceMessage?.StateAuxiliaryIdentifier;
            this.DeathJurisdictionID = sourceMessage?.DeathJurisdictionID;
            this.DeathYear = sourceMessage?.DeathYear;
        }

        /// <summary>
        /// Construct a CauseOfDeathCodingMessage from a FHIR Bundle.
        /// </summary>
        /// <param name="messageBundle">a FHIR Bundle that will be used to initialize the CauseOfDeathCodingMessage</param>
        /// <returns></returns>
        internal CauseOfDeathCodingMessage(Bundle messageBundle) : base(messageBundle)
        {
        }

        /// <summary>Constructor that creates a response for the specified message.</summary>
        /// <param name="destination">the endpoint identifier that the response message will be sent to.</param>
        /// <param name="source">the endpoint identifier that the response message will be sent from.</param>
        public CauseOfDeathCodingMessage(string destination, string source = "http://nchs.cdc.gov/vrdr_submission") : base(MESSAGE_TYPE, destination, source)
        {
        }

        /// <summary>Manner of Death Enum</summary>
        public enum MannerOfDeathEnum
        {
            /// <summary>Natural cause of death</summary>
            Natural,
            /// <summary>Accident cause of death</summary>
            Accident,
            /// <summary>Suicide cause of death</summary>
            Suicide,
            /// <summary>Cause of death is pending investigation</summary>
            PendingInvestigation,
            /// <summary>Cause of death could not be determined</summary>
            CouldNotBeDetermined
        }

        /// <summary>Manner of Death (or null)</summary>
        public MannerOfDeathEnum? MannerOfDeath
        {
            get
            {
                string value = Record.GetSingleValue<FhirString>("manner")?.Value;
                if (value == null)
                {
                    return null;
                }
                else
                {
                    Enum.TryParse<MannerOfDeathEnum>(value, out MannerOfDeathEnum code);
                    return code;
                }
            }
            set
            {
                Record.Remove("manner");
                if (value != null)
                {
                    Record.Add("manner", new FhirString(value.ToString()));
                }
            }
        }

        /// <summary>Place of Injury Enum</summary>
        public enum PlaceOfInjuryEnum
        {
            /// <summary>Home</summary>
            Home,
            /// <summary>Residential Institution</summary>
            ResidentialInstution,
            /// <summary>School, Other Institutions, Public Administrative Area</summary>
            SchoolOtherInstutionOrPublicAdministrativeArea,
            /// <summary>Sports and Atheletics Area</summary>
            SportsAndAthleticsArea,
            /// <summary>Street/Highway</summary>
            StreetOrHighway,
            /// <summary>Trade and Service Area</summary>
            TradeAndServiceArea,
            /// <summary>Industrial and Construction Area</summary>
            IndustrialAndConstructionArea,
            /// <summary>Farm</summary>
            Farm,
            /// <summary>Other Specified Place</summary>
            OtherSpecifiedPlace,
            /// <summary>Unspecified Place</summary>
            UnspecifiedPlace
        }

        /// <summary>Place of Injury (or null)</summary>
        public PlaceOfInjuryEnum? PlaceOfInjury
        {
            get
            {
                string value = Record.GetSingleValue<FhirString>("injpl")?.Value;
                if (value == null)
                {
                    return null;
                }
                else
                {
                    Enum.TryParse<PlaceOfInjuryEnum>(value, out PlaceOfInjuryEnum code);
                    return code;
                }
            }
            set
            {
                Record.Remove("injpl");
                if (value != null)
                {
                    Record.Add("injpl", new FhirString(value.ToString()));
                }
            }
        }

        /// <summary>Other Specified Place of Injury</summary>
        public string OtherSpecifiedPlace
        {
            get
            {
                return Record.GetSingleValue<FhirString>("other_specified_place")?.Value;
            }
            set
            {
                Record.Remove("other_specified_place");
                if (value != null)
                {
                    Record.Add("other_specified_place", new FhirString(value));
                }
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

        /// <summary>Entity axis cause of death coding grouped by line. An alternate flat list of entity axis codes
        /// is available via the CauseOfDeathEntityAxisList property, <see cref="CauseOfDeathEntityAxisList"/></summary>
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

        /// <summary>
        /// Entity axis cause of death coding as a flat list. Provided as an alternative to the
        /// CauseOfDeathEntityAxis property which groups cause codes by line, <see cref="CauseOfDeathEntityAxis"/>.
        /// </summary>
        /// <para>Each entry in the list is a tuple with three values:</para>
        /// <list type="bullet">
        /// <item>Line: <see cref="CauseOfDeathEntityAxisEntry.LineNumber"/></item>
        /// <item>Position: Sequence of code within the line</item>
        /// <item>Code>: ICD code</item>
        /// </list>
        public List<(string Line, string Position, string Code)> CauseOfDeathEntityAxisList
        {
            get
            {
                var entityAxisList = new List<(string Line, string Position, string Code)>();
                foreach (CauseOfDeathEntityAxisEntry entry in CauseOfDeathEntityAxis)
                {
                    int position = 1;
                    foreach (string code in entry.AssignedCodes)
                    {
                        entityAxisList.Add((Line: entry.LineNumber, Position: position.ToString(), Code: code));
                        position++;
                    }
                }
                return entityAxisList;
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

    /// <summary>Class <c>CauseOfDeathCodingUpdateMessage</c> conveys an updated coded cause of death of a decedent.</summary>
    public class CauseOfDeathCodingUpdateMessage : CauseOfDeathCodingMessage
    {
        /// <summary>
        /// The event URI for CauseOfDeathCodingUpdateMessage.
        /// </summary>
        public new const string MESSAGE_TYPE = "http://nchs.cdc.gov/vrdr_causeofdeath_coding_update";

        /// <summary>Constructor that creates an update for the specified message.</summary>
        /// <param name="sourceMessage">the message to create a response for.</param>
        /// <param name="source">the endpoint identifier that the message will be sent from.</param>
        public CauseOfDeathCodingUpdateMessage(BaseMessage sourceMessage, string source = "http://nchs.cdc.gov/vrdr_submission") : this(sourceMessage.MessageSource, source)
        {
        }

        /// <summary>
        /// Construct a CauseOfDeathCodingUpdateMessage from a FHIR Bundle.
        /// </summary>
        /// <param name="messageBundle">a FHIR Bundle that will be used to initialize the CauseOfDeathCodingUpdateMessage</param>
        /// <returns></returns>
        internal CauseOfDeathCodingUpdateMessage(Bundle messageBundle) : base(messageBundle)
        {
        }

        /// <summary>Constructor that creates a response for the specified message.</summary>
        /// <param name="destination">the endpoint identifier that the response message will be sent to.</param>
        /// <param name="source">the endpoint identifier that the response message will be sent from.</param>
        public CauseOfDeathCodingUpdateMessage(string destination, string source = "http://nchs.cdc.gov/vrdr_submission") : base(destination, source)
        {
            Header.Event = new FhirUri(MESSAGE_TYPE);
        }
    }
}
