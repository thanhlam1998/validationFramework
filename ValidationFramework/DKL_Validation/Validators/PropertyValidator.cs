using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DKL_Validation.Internal;
using DKL_Validation.Resources;
using DKL_Validation.Results;

namespace DKL_Validation.Validators
{
    public abstract class PropertyValidator : IPropertyValidator
    {
        public PropertyValidatorOptions Options { get; } = new PropertyValidatorOptions();

        protected PropertyValidator(IStringSource errorMessageSource)
        {
            if (errorMessageSource == null) errorMessageSource = new StaticStringSource("No default error message has been specified.");
            else if (errorMessageSource is LanguageStringSource l && l.ErrorCodeFunc == null)
                l.ErrorCodeFunc = ctx => Options.ErrorCodeSource?.GetString(ctx);

            Options.ErrorMessageSource = errorMessageSource;
        }

        protected PropertyValidator(string errorMessage)
        {
            Options.ErrorMessageSource = new StaticStringSource(errorMessage);
        }

        protected abstract bool IsValid(PropertyValidatorContext context);

        public IEnumerable<ValidationFailure> Validate(PropertyValidatorContext context)
        {
            if (IsValid(context)) return Enumerable.Empty<ValidationFailure>();

            PrepareMessageFormatterForValidationError(context);
            return new[] { CreateValidationError(context) };
        }

        /// <summary>
		/// Prepares the <see cref="MessageFormatter"/> of <paramref name="context"/> for an upcoming <see cref="ValidationFailure"/>.
		/// </summary>
		/// <param name="context">The validator context</param>
		protected virtual void PrepareMessageFormatterForValidationError(PropertyValidatorContext context)
        {
            context.MessageFormatter.AppendPropertyName(context.DisplayName);
            context.MessageFormatter.AppendPropertyValue(context.PropertyValue);
        }

        /// <summary>
		/// Creates an error validation result for this validator.
		/// </summary>
		/// <param name="context">The validator context</param>
		/// <returns>Returns an error validation result.</returns>
		protected virtual ValidationFailure CreateValidationError(PropertyValidatorContext context)
        {
            var messageBuilderContext = new MessageBuilderContext(context, Options.ErrorMessageSource, this);

            var error = context.Rule.MessageBuilder != null
                ? context.Rule.MessageBuilder(messageBuilderContext)
                : messageBuilderContext.GetDefaultMessage();

            var failure = new ValidationFailure(context.PropertyName, error, context.PropertyValue);
            //failure.FormattedMessageArguments = context.MessageFormatter.AdditionalArguments;
            failure.FormattedMessagePlaceholderValues = context.MessageFormatter.PlaceholderValues;
            failure.ErrorCode = (Options.ErrorCodeSource != null)
                ? Options.ErrorCodeSource.GetString(context)
                : ValidatorOptions.ErrorCodeResolver(this);
            return failure;
        }
    }   
}
