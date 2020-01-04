using System;
using System.Collections;
using System.Collections.Generic;
using DKL_Validation.Resources;

namespace DKL_Validation.Validators
{
    public class EmailValidator : PropertyValidator, IEmailValidator
    {
        private readonly object _defaultValueForType;
        public EmailValidator(object defaultValueForType) : base(new LanguageStringSource(nameof(EmailValidator)))
        {
            _defaultValueForType = defaultValueForType;
        }
        protected override bool IsValid(PropertyValidatorContext context)
        {
            if (context.PropertyValue is string email)
            {
                return email.Contains("@") && email.Length > 8 && email.Length < 255 && email.Contains(".");
            }
            return false;
        }
    }

    public interface IEmailValidator : IPropertyValidator
    {
    }
}