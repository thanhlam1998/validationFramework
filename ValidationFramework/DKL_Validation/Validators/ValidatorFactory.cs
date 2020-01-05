using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace DKL_Validation.Validators
{
    public class ValidatorFactory
    {
        public IPropertyValidator NotNullValidator()
        {
            return new NotNullValidator();
        }
        
        public IPropertyValidator NullValidator()
        {
            return new NullValidator();
        }

        public IPropertyValidator NotEmptyValidator(object defaultValue)
        {
            return new NotEmptyValidator(defaultValue);
        }

        public IPropertyValidator ValidEmailValidator(object defaultValue)
        {
            return new EmailValidator(defaultValue);
        }

        public IPropertyValidator EqualValidator(object defaultValue)
        {
            return new EqualValidator(defaultValue);
        }

        public IPropertyValidator EqualValidator(Func<object, object> comparisonProperty, MemberInfo member)
        {
            return new EqualValidator(comparisonProperty, member);
        }

        public IPropertyValidator NotEqualValidator(object defaultValue)
        {
            return new NotEqualValidator(defaultValue);
        }

        public IPropertyValidator NotEqualValidator(Func<object, object> comparisonProperty, MemberInfo member)
        {
            return new NotEqualValidator(comparisonProperty, member);
        }
    }
}
