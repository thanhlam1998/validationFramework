using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using DKL_Validation.Resources;

namespace DKL_Validation.Validators
{
    public class EmailValidator : PropertyValidator, IEmailValidator
    {
        private readonly object _defaultValueForType;
        string emailPatern = @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*@((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))\z";
        public EmailValidator(object defaultValueForType) : base(new LanguageStringSource(nameof(EmailValidator)))
        {
            _defaultValueForType = defaultValueForType;
        }
        protected override bool IsValid(PropertyValidatorContext context)
        {
            if (context.PropertyValue is string email)
            {
                return Regex.IsMatch(email, emailPatern);
            }
            return false;
        }
    }

    public interface IEmailValidator : IPropertyValidator
    {
    }
}