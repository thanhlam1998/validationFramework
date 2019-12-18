using System;
using System.Collections.Generic;
using System.Text;

namespace ValidationFramework.Internal
{
    /// <summary>
    /// Determine or not the rule should execute
    /// </summary>
    public interface IValidatorSelector
    {
        bool CanExecute(IValidationRule rule, string propertyPath, ValidationContext context);
    }
}
