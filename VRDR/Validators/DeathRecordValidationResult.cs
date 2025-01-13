using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VRDR.Validators
{
    internal class DeathRecordValidationResult : ValidationResult
    {

        string _AdditionalInfo = string.Empty;

        public string AdditionalInfo { get { return _AdditionalInfo; } set { _AdditionalInfo = value; } }
        public DeathRecordValidationResult(string message) : base(message)
        {
        }

        public DeathRecordValidationResult(string message, IEnumerable<string> memebers) : base
            (message, memebers)
        { }
        public DeathRecordValidationResult(ValidationResult validationResult) : base(validationResult) { }

        public DeathRecordValidationResult(string message, string additionalInfo) : base(message)
        {
            _AdditionalInfo = additionalInfo;
        }
    }
}
