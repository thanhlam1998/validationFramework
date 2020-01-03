using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DKL_Validation.Resources;

namespace DKL_Validation.Validators
{
    public class NotEmptyValidator : PropertyValidator, INotEmptyValidator
    {
        private readonly object _defaultValueForType;
        public NotEmptyValidator(object defaultValueForType) : base(new LanguageStringSource(nameof(NotEmptyValidator)))
        {
            _defaultValueForType = defaultValueForType;
        }
        protected override bool IsValid(PropertyValidatorContext context)
        {
            switch (context.PropertyValue)
            {
                case null:
                case string s when string.IsNullOrWhiteSpace(s):
                case ICollection c when c.Count == 0:
                case Array a when a.Length == 0:
                case IEnumerable e when !e.Cast<object>().Any():
                    return false;
            }
            return true;
        }
    }
    public interface INotEmptyValidator : IPropertyValidator
    {
    }
}
