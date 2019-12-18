using System;
using System.Collections.Generic;
using System.Text;
using ValidationFramework.Resources;

namespace ValidationFramework.Validators
{
    public class NotNullValidator : PropertyValidator, INotNullValidator
    {
        public NotNullValidator() : base(new LanguageStringSource(nameof(NotNullValidator)))
        {
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            if (context.PropertyValue == null)
            {
                return false;
            }
            return true;
        }
    }
    public interface INotNullValidator : IPropertyValidator
    {
    }
}
