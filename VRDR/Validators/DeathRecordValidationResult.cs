using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VRDR.Validators
{
    public struct DeathRecordValidationResult
    {
        public string PropertyName { get; set; }

        public string ErrorMessage { get; set; }

        public object AssignedValue { get; set; }

        public object ValidatorType { get; set; }
    }
}
