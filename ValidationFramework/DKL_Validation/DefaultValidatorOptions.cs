using System;
using System.Collections.Generic;
using System.Text;
using DKL_Validation.Internal;
using DKL_Validation.Resources;

namespace DKL_Validation
{
    public static class DefaultValidatorOptions
    {
        /// <summary>
		/// Specifies a custom error message to use when validation fails. Only applies to the rule that directly precedes it.
		/// </summary>
		/// <param name="rule">The current rule</param>
		/// <param name="errorMessage">The error message to use</param>
		/// <returns></returns>
		public static IRuleBuilderOptions<T, TProperty> WithMessage<T, TProperty>(this IRuleBuilderOptions<T, TProperty> rule, string errorMessage)
        {
            errorMessage.Guard("A message must be specified when calling WithMessage.", nameof(errorMessage));
            return rule.Configure(config => {
                config.CurrentValidator.Options.ErrorMessageSource = new StaticStringSource(errorMessage);
            });
        }
    }
}
