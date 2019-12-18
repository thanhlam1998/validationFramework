using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValidationFramework.Internal
{
    public class DefaultValidatorSelector : IValidatorSelector
    {
        public bool CanExecute(IValidationRule rule, string propertyPath, ValidationContext context)
        {
            // By default we ignore any Rules part of ruleSets
            if(rule.RuleSets.Length > 0 && !rule.RuleSets.Contains("default", StringComparer.OrdinalIgnoreCase))
            {
                return false;
            }
            return true;
        }
    }
}
