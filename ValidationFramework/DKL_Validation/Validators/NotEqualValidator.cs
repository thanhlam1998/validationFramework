using DKL_Validation.Resources;
using DKL_Validation.Validators;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace DKL_Validation.Validators
{
    class NotEqualValidator : PropertyValidator
    {
        public MemberInfo MemberToCompare { get; private set; }
        public object ValueToCompare { get; private set; }
        readonly Func<object, object> _func;

        public NotEqualValidator(object valueToCompare) : base(new LanguageStringSource(nameof(EqualValidator)))
        {
            this.ValueToCompare = valueToCompare;
        }

        public NotEqualValidator(Func<object, object> comparisonProperty, MemberInfo member) : base(new LanguageStringSource(nameof(EqualValidator)))
        {
            _func = comparisonProperty;
            MemberToCompare = member;
        }
        protected override bool IsValid(PropertyValidatorContext context)
        {
            var comparisonValue = GetComparisonValue(context);
            bool success = Compare(comparisonValue, context.PropertyValue);

            if (success)
            {
                context.MessageFormatter.AppendArgument("ComparisonValue", comparisonValue);
                return false;
            }
            return true;
        }
        private object GetComparisonValue(PropertyValidatorContext context)
        {
            if (_func != null)
            {
                return _func(context.Instance);
            }

            return ValueToCompare;
        }

        protected bool Compare(object comparisonValue, object propertyValue)
        {
            if (comparisonValue is IComparable comparable && propertyValue is IComparable comparable1)
            {
                return GetEqualsResult(comparable, comparable1);
            }

            return Equals(comparisonValue, propertyValue);
        }

        public static bool TryCompare(IComparable value, IComparable valueToCompare, out int result)
        {
            try
            {
                Compare(value, valueToCompare, out result);
                return true;
            }
            catch
            {
                result = 0;
            }

            return false;
        }

        static void Compare(IComparable value, IComparable valueToCompare, out int result)
        {
            try
            {
                // try default (will work on same types)
                result = value.CompareTo(valueToCompare);
            }
            catch (ArgumentException)
            {
                // attempt to to value type comparison
                if (value is decimal || valueToCompare is decimal ||
                    value is double || valueToCompare is double ||
                    value is float || valueToCompare is float)
                {
                    // we are comparing a decimal/double/float, then compare using doubles
                    result = Convert.ToDouble(value).CompareTo(Convert.ToDouble(valueToCompare));
                }
                else
                {
                    // use long integer
                    result = ((long)value).CompareTo((long)valueToCompare);
                }
            }
        }

        public static int GetComparisonResult(IComparable value, IComparable valueToCompare)
        {
            int result;
            if (TryCompare(value, valueToCompare, out result))
            {
                return result;
            }

            return value.CompareTo(valueToCompare);
        }

        public static bool GetEqualsResult(IComparable value, IComparable valueToCompare)
        {
            int result;
            if (TryCompare(value, valueToCompare, out result))
            {
                return result == 0;
            }

            return value.Equals(valueToCompare);
        }
    }
}
