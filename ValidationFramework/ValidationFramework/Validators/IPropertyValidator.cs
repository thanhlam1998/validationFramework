using System;
using System.Collections.Generic;
using System.Text;
using ValidationFramework.Results;
using ValidationFramework.Validators;

namespace ValidationFramework
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
