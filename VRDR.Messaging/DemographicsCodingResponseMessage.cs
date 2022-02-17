using System;
using System.Collections.Generic;
using Hl7.Fhir.Model;

namespace VRDR
{
    /// <summary>
    /// A <c>CodingResponseMessage</c> that conveys the coded demographic information of a decedent.
    /// </summary>
    public class DemographicsCodingResponseMessage : CodingResponseMessage
    {
        /// <summary>
        /// The event URI for DemographicCodingResponseMessage.
        /// </summary>
        public const String MESSAGE_TYPE = "http://nchs.cdc.gov/vrdr_demographics_coding";

        /// <summary>Constructor that creates a response for the specified message.</summary>
        /// <param name="sourceMessage">the message to create a response for.</param>
        /// <param name="source">the endpoint identifier that the message will be sent from.</param>
        public DemographicsCodingResponseMessage(BaseMessage sourceMessage, string source = "http://nchs.cdc.gov/vrdr_submission") : this(sourceMessage.MessageSource, source)
        {
            this.CertificateNumber = sourceMessage?.CertificateNumber;
            this.StateAuxiliaryIdentifier = sourceMessage?.StateAuxiliaryIdentifier;
            this.DeathJurisdictionID = sourceMessage?.DeathJurisdictionID;
            this.DeathYear = sourceMessage?.DeathYear;
        }

        /// <summary>
        /// Construct a DemographicCodingResponseMessage from a FHIR Bundle.
        /// </summary>
        /// <param name="messageBundle">a FHIR Bundle that will be used to initialize the DemographicCodingResponseMessage</param>
        /// <returns></returns>
        internal DemographicsCodingResponseMessage(Bundle messageBundle) : base(messageBundle)
        {
        }

        /// <summary>Constructor that creates a response for the specified message.</summary>
        /// <param name="destination">the endpoint identifier that the response message will be sent to.</param>
        /// <param name="source">the endpoint identifier that the response message will be sent from.</param>
        public DemographicsCodingResponseMessage(string destination, string source = "http://nchs.cdc.gov/vrdr_submission") : base(MESSAGE_TYPE, destination, source)
        {
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
                    if (item.Key == RaceCode.RACEBRG)
                    {
                        codeList = "https://www.cdc.gov/nchs/data/dvs/Multiple_race_documentation_5-10-04.pdf";
                    }
                    var part = Tuple.Create(item.Key.ToString(),
                        (Base)(new Coding(codeList, item.Value)));
                    list.Add(part);
                }
                Record.Add("race", list);
            }
        }
    }

    /// <summary>Class <c>DemographicsCodingUpdateMessage</c> conveys an updated coded race and ethnicity of a decedent.</summary>
    public class DemographicsCodingUpdateMessage : DemographicsCodingResponseMessage
    {
        /// <summary>
        /// The event URI for DemographicsCodingUpdateMessage.
        /// </summary>
        public const string MESSAGE_TYPE = "http://nchs.cdc.gov/vrdr_demographics_coding_update";

        /// <summary>Constructor that creates an update for the specified message.</summary>
        /// <param name="sourceMessage">the message to create a response for.</param>
        /// <param name="source">the endpoint identifier that the message will be sent from.</param>
        public DemographicsCodingUpdateMessage(BaseMessage sourceMessage, string source = "http://nchs.cdc.gov/vrdr_submission") : this(sourceMessage.MessageSource, source)
        {
        }

        /// <summary>
        /// Construct a DemographicsCodingUpdateMessage from a FHIR Bundle.
        /// </summary>
        /// <param name="messageBundle">a FHIR Bundle that will be used to initialize the DemographicsCodingUpdateMessage</param>
        /// <returns></returns>
        internal DemographicsCodingUpdateMessage(Bundle messageBundle) : base(messageBundle)
        {
        }

        /// <summary>Constructor that creates a response for the specified message.</summary>
        /// <param name="destination">the endpoint identifier that the response message will be sent to.</param>
        /// <param name="source">the endpoint identifier that the response message will be sent from.</param>
        public DemographicsCodingUpdateMessage(string destination, string source = "http://nchs.cdc.gov/vrdr_submission") : base(destination, source)
        {
            Header.Event = new FhirUri(MESSAGE_TYPE);
        }
    }
}
