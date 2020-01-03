using System;
using System.Collections.Generic;
using System.Text;
using DKL_Validation.Results;
using DKL_Validation.Validators;

namespace DKL_Validation
{
    public interface IValidationRule
    {
        /// <summary>
        /// The validators are grouped under this rule
        /// </summary>
        IEnumerable<IPropertyValidator> Validators { get; }
        IEnumerable<ValidationFailure> Validate(ValidationContext context);
    }
}
