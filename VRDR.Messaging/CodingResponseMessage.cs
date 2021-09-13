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
                    string codeList = "https://www.cdc.gov/nchs/data/dvs/RaceCodeList.pdf";
                    // Special case for RACEBRG, it uses a different code document
                    if(item.Key == RaceCode.RACEBRG) {
                        codeList = "https://www.cdc.gov/nchs/data/dvs/Multiple_race_documentation_5-10-04.pdf";
                    }
                    var part = Tuple.Create(item.Key.ToString(),
                        (Base)(new Coding(codeList, item.Value)));
                    list.Add(part);
                }
                Record.Add("race", list);
            }
        }

        /// <summary>Coder Status (string, 0-9 or null)</summary>
        public string CoderStatus
        {
            get
            {
                return Record.GetSingleValue<Coding>("cs")?.Code;
            }
            set
            {
                Record.Remove("cs");
                if (value != null)
                {
                    Record.Add("cs", new Coding("https://ftp.cdc.gov/pub/Health_Statistics/NCHS/Software/MICAR/Data_Entry_Software/ACME_TRANSAX/Documentation/auser.pdf", value));
                }
            }
        }

        /// <summary>Shipment Number (Alpha Numeric)</summary>
        public string ShipmentNumber
        {
            get
            {
                return Record.GetSingleValue<FhirString>("ship")?.Value;
            }
            set
            {
                Record.Remove("ship");
                if (value != null)
                {
                    Record.Add("ship", new FhirString(value));
                }
            }
        }

        /// <summary>NCHS Receipt Date Month (Numeric, 01-12 or null)</summary>
        public uint? NCHSReceiptMonth
        {
            get
            {
                return (uint?)Record.GetSingleValue<UnsignedInt>("rec_mo")?.Value;
            }
            set
            {
                Record.Remove("rec_mo");
                if (value != null)
                {
                    if (value < 1 || value > 12) {
                        throw new ArgumentException("Valid values for NCHS Receipt Month are 01-12 or null");
                    }
                    Record.Add("rec_mo", new UnsignedInt((int)value));
                }
            }
        }

        /// <summary>NCHS Receipt Date Month (string, 01-12 or null)</summary>
        public string NCHSReceiptMonthString
        {
            get
            {
                uint? month = this.NCHSReceiptMonth;
                return (month != null) ? month.ToString().PadLeft(2, '0') : null;
            }
            set
            {
                this.NCHSReceiptMonth = (value != null) ? Convert.ToUInt32(value) : (uint?)null;
            }
        }

        /// <summary>NCHS Receipt Date Day (Numeric, 01-31 or blank)</summary>
        public uint? NCHSReceiptDay
        {
            get
            {
                return (uint?)Record.GetSingleValue<UnsignedInt>("rec_dy")?.Value;
            }
            set
            {
                Record.Remove("rec_dy");
                if (value != null)
                {
                    if (value < 1 || value > 31) {
                        throw new ArgumentException("Valid values for NCHS Receipt Day are 01-31 or null");
                    }
                    Record.Add("rec_dy", new UnsignedInt((int)value));
                }
            }
        }

        /// <summary>NCHS Receipt Date Day (string, 01-31 or null)</summary>
        public string NCHSReceiptDayString
        {
            get
            {
                uint? month = this.NCHSReceiptDay;
                return (month != null) ? month.ToString().PadLeft(2, '0') : null;
            }
            set
            {
                this.NCHSReceiptDay = (value != null) ? Convert.ToUInt32(value) : (uint?)null;
            }
        }

        /// <summary>NCHS Receipt Date Year (Numeric, Greater than or equal to year of death or blank)</summary>
        public uint? NCHSReceiptYear
        {
            get
            {
                return (uint?)Record.GetSingleValue<UnsignedInt>("rec_yr")?.Value;
            }
            set
            {
                Record.Remove("rec_yr");
                if (value != null)
                {
                    if (DeathYear != null && value < DeathYear) {
                        throw new ArgumentException("NCHS Receipt Year must be greater than or equal to Death Year, or null");
                    }
                    Record.Add("rec_yr", new UnsignedInt((int)value));
                }
            }
        }

        /// <summary>NCHS Receipt Date Year (string, Greater than year of death or null)</summary>
        public string NCHSReceiptYearString
        {
            get
            {
                uint? month = this.NCHSReceiptYear;
                return (month != null) ? month.ToString() : null;
            }
            set
            {
                this.NCHSReceiptYear = (value != null) ? Convert.ToUInt32(value) : (uint?)null;
            }
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
                if(value == null)
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
                if(value != null)
                {
                    Record.Add("manner", new FhirString(value.ToString()));
                }
            }
        }

        /// <summary>Intentional reject (1-5, 9 or null). See Coding one-character reject codes in code document for values.</summary>
        public string IntentionalReject
        {
            get
            {

                return Record.GetSingleValue<Coding>("int_rej")?.Code;
            }
            set
            {
                Record.Remove("int_rej");
                if (value != null)
                {
                    Record.Add("int_rej", new Coding("https://www.cdc.gov/nchs/data/dvs/2b_2017.pdf", value));
                }
            }
        }

        /// <summary>ACME System Reject Enum</summary>
        public enum ACMESystemRejectEnum
        {
            /// <summary>MICAR Reject - Dictionary Match</summary>
            MICARRejectDictionaryMatch,
            /// <summary>ACME Reject</summary>
            ACMEReject,
            /// <summary>MICAR Reject - Rule Application</summary>
            MICARRejectRuleApplication,
            /// <summary>Record Reviewed</summary>
            RecordReviewed,
            /// <summary>Not Rejected</summary>
            NotRejected,
        }

        /// <summary>ACME system reject codes (or null)</summary>
        public ACMESystemRejectEnum? ACMESystemRejectCodes
        {
            get
            {
                string value = Record.GetSingleValue<FhirString>("sys_rej")?.Value;
                if(value == null)
                {
                    return null;
                }
                else
                {
                    Enum.TryParse<ACMESystemRejectEnum>(value, out ACMESystemRejectEnum code);
                    return code;
                }
            }
            set
            {
                Record.Remove("sys_rej");
                if(value != null)
                {
                    Record.Add("sys_rej", new FhirString(value.ToString()));
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
                if(value == null)
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
                if(value != null)
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
