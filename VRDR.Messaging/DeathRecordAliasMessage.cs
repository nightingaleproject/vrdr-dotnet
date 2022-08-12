using System;
using Hl7.Fhir.Model;

namespace VRDR
{
    /// <summary>Class <c>DeathRecordAliasMessage</c> indicates that a previously submitted DeathRecordSubmissionMessage has alias information.</summary>
    public class DeathRecordAliasMessage : BaseMessage
    {
        /// <summary>
        /// The Event URI for DeathRecordAliasMessage
        /// </summary>
        public const string MESSAGE_TYPE = "http://nchs.cdc.gov/vrdr_alias";

        /// <summary>Default constructor that creates a new, empty DeathRecordAliasMessage.</summary>
        public DeathRecordAliasMessage() : base(MESSAGE_TYPE)
        {
        }

        /// <summary>
        /// Construct a DeathRecordAliasMessage from a FHIR Bundle.
        /// </summary>
        /// <param name="messageBundle">a FHIR Bundle that will be used to initialize the DeathRecordAliasMessage</param>
        /// <returns></returns>
        internal DeathRecordAliasMessage(Bundle messageBundle) : base(messageBundle)
        {
        }

        /// <summary>Constructor that takes a VRDR.DeathRecord and creates a message to submit an alias for that record.</summary>
        /// <param name="record">the VRDR.DeathRecord to create a DeathRecordAliasMessage for.</param>
        public DeathRecordAliasMessage(DeathRecord record) : this()
        {
            ExtractBusinessIdentifiers(record);
            if (record.GivenNames.Length > 0)
            {
                AliasDecedentFirstName = record.GivenNames[0];
            }
            if (record.FamilyName != null)
            {
                AliasDecedentLastName = record.FamilyName;
            }
            if (record.GivenNames.Length > 1)
            {
                AliasDecedentMiddleName = record.GivenNames[1];
            }
            if (record.Suffix != null)
            {
                AliasDecedentNameSuffix = record.Suffix;
            }
            if (record.FatherFamilyName != null)
            {
                AliasFatherSurname = record.FatherFamilyName;
            }
            if (record.SSN != null)
            {
                AliasSocialSecurityNumber = record.SSN;
            }
        }

        /// <summary>Alias for the decedent's first name</summary>
        public string AliasDecedentFirstName
        {
            get
            {
                return Record?.GetSingleValue<FhirString>("alias_decedent_first_name")?.Value;
            }
            set
            {
                SetSingleStringValue("alias_decedent_first_name", value);
            }
        }

        /// <summary>Alias for the decedent's last name</summary>
        public string AliasDecedentLastName
        {
            get
            {
                return Record?.GetSingleValue<FhirString>("alias_decedent_last_name")?.Value;
            }
            set
            {
                SetSingleStringValue("alias_decedent_last_name", value);
            }
        }

        /// <summary>Alias for the decedent's middle name</summary>
        public string AliasDecedentMiddleName
        {
            get
            {
                return Record?.GetSingleValue<FhirString>("alias_decedent_middle_name")?.Value;
            }
            set
            {
                SetSingleStringValue("alias_decedent_middle_name", value);
            }
        }

        /// <summary>Alias for the decedent's name suffix</summary>
        public string AliasDecedentNameSuffix
        {
            get
            {
                return Record?.GetSingleValue<FhirString>("alias_decedent_name_suffix")?.Value;
            }
            set
            {
                SetSingleStringValue("alias_decedent_name_suffix", value);
            }
        }

        /// <summary>Alias for the decedent's father's surname</summary>
        public string AliasFatherSurname
        {
            get
            {
                return Record?.GetSingleValue<FhirString>("alias_father_surname")?.Value;
            }
            set
            {
                SetSingleStringValue("alias_father_surname", value);
            }
        }

        /// <summary>Alias for the decedent's social security number</summary>
        public string AliasSocialSecurityNumber
        {
            get
            {
                return Record?.GetSingleValue<FhirString>("alias_social_security_number")?.Value;
            }
            set
            {
                SetSingleStringValue("alias_social_security_number", value);
            }
        }
    }
}
