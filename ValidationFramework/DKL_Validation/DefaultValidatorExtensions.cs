using System;
using System.Collections.Generic;
using System.Text;
using DKL_Validation.Validators;
using DKL_Validation.Internal;

namespace DKL_Validation
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

        //Email validator extensions
        public static IRuleBuilderOptions<T, TProperty> ValidEmail<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new EmailValidator(default(TProperty)));
        }

        /// <summary>
		/// Defines a predicate validator on the current rule builder using a lambda expression to specify the predicate.
		/// Validation will fail if the specified lambda returns false.
		/// Validation will succeed if the specified lambda returns true.
		/// </summary>
		/// <typeparam name="T">Type of object being validated</typeparam>
		/// <typeparam name="TProperty">Type of property being validated</typeparam>
		/// <param name="ruleBuilder">The rule builder on which the validator should be defined</param>
		/// <param name="predicate">A lambda expression specifying the predicate</param>
		/// <returns></returns>
		public static IRuleBuilderOptions<T, TProperty> Must<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, Func<TProperty, bool> predicate)
        {
            predicate.Guard("Cannot pass a null predicate to Must.", nameof(predicate));

            return ruleBuilder.Must((x, val) => predicate(val));
        }

        /// <summary>
        /// Defines a predicate validator on the current rule builder using a lambda expression to specify the predicate.
        /// Validation will fail if the specified lambda returns false.
        /// Validation will succeed if the specified lambda returns true.
        /// This overload accepts the object being validated in addition to the property being validated.
        /// </summary>
        /// <typeparam name="T">Type of object being validated</typeparam>
        /// <typeparam name="TProperty">Type of property being validated</typeparam>
        /// <param name="ruleBuilder">The rule builder on which the validator should be defined</param>
        /// <param name="predicate">A lambda expression specifying the predicate</param>
        /// <returns></returns>
        public static IRuleBuilderOptions<T, TProperty> Must<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, Func<T, TProperty, bool> predicate)
        {
            predicate.Guard("Cannot pass a null predicate to Must.", nameof(predicate));
            return ruleBuilder.Must((x, val, propertyValidatorContext) => predicate(x, val));
        }

        /// <summary>
        /// Defines a predicate validator on the current rule builder using a lambda expression to specify the predicate.
        /// Validation will fail if the specified lambda returns false.
        /// Validation will succeed if the specified lambda returns true.
        /// This overload accepts the object being validated in addition to the property being validated.
        /// </summary>
        /// <typeparam name="T">Type of object being validated</typeparam>
        /// <typeparam name="TProperty">Type of property being validated</typeparam>
        /// <param name="ruleBuilder">The rule builder on which the validator should be defined</param>
        /// <param name="predicate">A lambda expression specifying the predicate</param>
        /// <returns></returns>
        public static IRuleBuilderOptions<T, TProperty> Must<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, Func<T, TProperty, PropertyValidatorContext, bool> predicate)
        {
            predicate.Guard("Cannot pass a null predicate to Must.", nameof(predicate));
            return ruleBuilder.SetValidator(new PredicateValidator((instance, property, propertyValidatorContext) => predicate((T)instance, (TProperty)property, propertyValidatorContext)));
        }

    }
}
