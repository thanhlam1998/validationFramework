using System;
using System.Collections.Generic;
using System.Text;
using DKL_Validation.Validators;
using DKL_Validation.Results;

namespace DKL_Validation
{
    public interface IPropertyValidator
    {
        /// <summary>
        /// Perform validation
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        IEnumerable<ValidationFailure> Validate(PropertyValidatorContext context);
        PropertyValidatorOptions Options { get; }
    }
}
