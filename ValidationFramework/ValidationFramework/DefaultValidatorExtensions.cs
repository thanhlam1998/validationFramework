using System;
using System.Collections.Generic;
using System.Text;
using ValidationFramework.Validators;

namespace ValidationFramework
{
    public static class DefaultValidatorExtensions
    {
        /// <summary>
		/// Defines a 'not null' validator on the current rule builder.
		/// Validation will fail if the property is null.
		/// </summary>
		/// <typeparam name="T">Type of object being validated</typeparam>
		/// <typeparam name="TProperty">Type of property being validated</typeparam>
		/// <param name="ruleBuilder">The rule builder on which the validator should be defined</param>
		/// <returns></returns>
		public static IRuleBuilderOptions<T, TProperty> NotNull<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new NotNullValidator());
        }

        /// <summary>
        /// Defines a 'not empty' validator on the current rule builder.
        /// Validation will fail if the property is null, an empty string, whitespace, an empty collection or the default value for the type (for example, 0 for integers but null for nullable integers)
        /// </summary>
        /// <typeparam name="T">Type of object being validated</typeparam>
        /// <typeparam name="TProperty">Type of property being validated</typeparam>
        /// <param name="ruleBuilder">The rule builder on which the validator should be defined</param>
        /// <returns></returns>
        public static IRuleBuilderOptions<T, TProperty> NotEmpty<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new NotEmptyValidator(default(TProperty)));
        }
    }
}
