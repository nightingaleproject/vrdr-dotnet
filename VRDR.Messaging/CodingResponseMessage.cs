using System;
using System.Collections.Generic;
using System.Linq;
using Hl7.Fhir.Model;

namespace VRDR
{
    /// <summary>Class <c>CodingResponseMessage</c> conveys the coded cause of death, race and ethnicity of a decedent.</summary>
    public class CodingResponseMessage : BaseMessage
    {
        private Parameters Payload;

        /// <summary>Constructor that creates a response for the specified message.</summary>
        /// <param name="sourceMessage">the message to create a response for.</param>
        /// <param name="source">the endpoint identifier that the ack message will be sent from.</param>
        public CodingResponseMessage(BaseMessage sourceMessage, string source = "http://nchs.cdc.gov/vrdr_submission") : this(sourceMessage.MessageSource, source)
        {
        }

        /// <summary>Constructor that creates a response for the specified message.</summary>
        /// <param name="destination">the endpoint identifier that the response message will be sent to.</param>
        /// <param name="source">the endpoint identifier that the response message will be sent from.</param>
        public CodingResponseMessage(string destination, string source = "http://nchs.cdc.gov/vrdr_submission") : base("vrdr_coding")
        {
            Header.Source.Endpoint = source;
            this.MessageDestination = destination;
            this.Payload = new Parameters();
            this.Payload.Id = Guid.NewGuid().ToString();
            MessageBundle.AddResourceEntry(this.Payload, "urn:uuid:" + this.Payload.Id);
            Header.Focus.Add(new ResourceReference(this.Payload.Id));

        }

        /// <summary>Constructor that takes a string that represents a response message in either XML or JSON format.</summary>
        /// <param name="message">represents a response message in either XML or JSON format.</param>
        /// <param name="permissive">if the parser should be permissive when parsing the given string</param>
        /// <exception cref="ArgumentException">Message is neither valid XML nor JSON.</exception>
        public CodingResponseMessage(string message, bool permissive = false) : base(message, permissive)
        {
        }

        /// <summary>Restores class references from a newly parsed record.</summary>
        protected override void RestoreReferences()
        {
            base.RestoreReferences();

            // Grab Payload
            var payloadEntry = MessageBundle.Entry.FirstOrDefault( entry => entry.Resource.ResourceType == ResourceType.Parameters );
            if (payloadEntry != null)
            {
                Payload = (Parameters)payloadEntry.Resource;
            }
        }

        /// <summary>Jurisdiction-assigned death certificate number</summary>
        public string CertificateNumber
        {
            get
            {
                return Payload.GetSingleValue<FhirString>("cert_no")?.Value;
            }
            set
            {
                Payload.Remove("cert_no");
                Payload.Add("cert_no", new FhirString(value));
            }
        }

        /// <summary>Jurisdiction-assigned id</summary>
        public string StateIdentifier
        {
            get
            {
                return Payload.GetSingleValue<FhirString>("state_id")?.Value;
            }
            set
            {
                Payload.Remove("state_id");
                Payload.Add("state_id", new FhirString(value));
            }
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
                Parameters.ParameterComponent ethnicityEntry = Payload.GetSingle("ethnicity");
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
                Payload.Remove("ethnicity");
                var list = new List<Tuple<string, Base>>();
                foreach (KeyValuePair<HispanicOrigin, string> item in value)
                {
                    var part = Tuple.Create(item.Key.ToString(), 
                        (Base)(new Coding("https://www.cdc.gov/nchs/data/dvs/HispanicCodeTitles.pdf", item.Value)));
                    list.Add(part);
                }
                Payload.Add("ethnicity", list);
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
                Parameters.ParameterComponent raceEntry = Payload.GetSingle("race");
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
                Payload.Remove("race");
                var list = new List<Tuple<string, Base>>();
                foreach (KeyValuePair<RaceCode, string> item in value)
                {
                    var part = Tuple.Create(item.Key.ToString(), 
                        (Base)(new Coding("https://www.cdc.gov/nchs/data/dvs/RaceCodeList.pdf", item.Value)));
                    list.Add(part);
                }
                Payload.Add("race", list);
            }
        }

        /// <summary>Underlying cause of death code (ICD-10)</summary>
        public string UnderlyingCauseOfDeath
        {
            get
            {
                return Payload.GetSingleValue<Coding>("underlying_cause_of_death")?.Code;
            }
            set
            {
                Payload.Remove("underlying_cause_of_death");
                Payload.Add("underlying_cause_of_death", new Coding("http://hl7.org/fhir/ValueSet/icd-10", value));
            }
        }

        /// <summary>Record axis coding of cause of death</summary>
        public List<string> CauseOfDeathRecordAxis
        {
            get
            {
                var codes = new List<string>();
                Parameters.ParameterComponent axisEntry = Payload.GetSingle("record_cause_of_death");
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
                Payload.Remove("record_cause_of_death");
                var list = new List<Tuple<string, Base>>();
                foreach (string code in value)
                {
                    var part = Tuple.Create("coding", 
                        (Base)(new Coding("https://www.cdc.gov/nchs/data/dvs/RaceCodeList.pdf", code)));
                    list.Add(part);
                }
                Payload.Add("record_cause_of_death", list);
            }
        }
    }

    /// <summary>Class for structuring a cause of death entity axis entry</summary>
    public class CauseOfDeathEntityAxisEntry
    {
        private string deathCertificateText;
        private string causeOfDeathConditionId;
        private List<string> assignedCodes;

        /// <summary>Create a CauseOfDeathEntityAxisEntry with the specified death certificate text and cause of death
        /// condition id</summary>
        public CauseOfDeathEntityAxisEntry(string deathCertificateText, string causeOfDeathConditionId)
        {
            this.assignedCodes = new List<string>();
            this.deathCertificateText = deathCertificateText;
            this.causeOfDeathConditionId = causeOfDeathConditionId;
        }

        /// <summary>Add a Cause of death code</summary>
        public void AddCode(string code)
        {
            assignedCodes.Add(code);
        }

        /// <summary>A line of the original cause of death on the death certificate</summary>
        public string DeathCertificateText
        {
            get
            {
                return deathCertificateText;
            }
        }

        /// <summary>The identifier of the corresponding cause of death condition entry in the VRDR FHIR document</summary>
        public string CauseOfDeathConditionId
        {
            get
            {
                return causeOfDeathConditionId;
            }
        }

        /// <summary>The codes assigned for the DeathCertificateText</summary>
        public List<string> AssignedCodes
        {
            get
            {
                return assignedCodes;
            }
        }
    }
}
