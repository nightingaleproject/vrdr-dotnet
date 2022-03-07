using System;
using Hl7.Fhir.Model;

namespace VRDR
{
    /// <summary>Class <c>CodingResponseMessage</c> conveys the coded cause of death, race and ethnicity of a decedent.</summary>
    public abstract class CodingResponseMessage : BaseMessage
    {

        /// <summary>
        /// Construct a CodingResponseMessage from a FHIR Bundle.
        /// </summary>
        /// <param name="messageBundle">a FHIR Bundle that will be used to initialize the CodingResponseMessage</param>
        /// <returns></returns>
        protected CodingResponseMessage(Bundle messageBundle) : base(messageBundle)
        {
        }

        /// <summary>Constructor that creates a response for the specified message.</summary>
        /// <param name="messageType">The event URI identifying the specific type of CodingResponseMessage.</param>
        /// <param name="destination">the endpoint identifier that the response message will be sent to.</param>
        /// <param name="source">the endpoint identifier that the response message will be sent from.</param>
        protected CodingResponseMessage(string messageType, string destination, string source = "http://nchs.cdc.gov/vrdr_submission") : base(messageType)
        {
            Header.Source.Endpoint = source;
            MessageDestination = destination;
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
    }
}
