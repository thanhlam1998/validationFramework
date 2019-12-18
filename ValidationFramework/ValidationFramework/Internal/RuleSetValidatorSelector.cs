using System;
using System.Collections.Generic;
using System.Text;

namespace ValidationFramework.Internal
{
    public class RuleSetValidatorSelector : IValidatorSelector
    {
        readonly string[] _rulesetsToExecute;

        /// <summary>
		/// Creates a new instance of the RulesetValidatorSelector.
		/// </summary>
		public RuleSetValidatorSelector(params string[] rulesetsToExecute)
        {
            this._rulesetsToExecute = rulesetsToExecute;
        }

        public virtual bool CanExecute(IValidationRule rule, string propertyPath, ValidationContext context)
        {
            return false;
        }
    }
}
