﻿using System;
using System.Collections.Generic;
using System.Text;
using ValidationFramework.Results;
using ValidationFramework.Validators;

namespace ValidationFramework
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
