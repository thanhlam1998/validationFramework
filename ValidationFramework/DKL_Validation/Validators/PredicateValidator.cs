using System;
using System.Collections.Generic;
using System.Text;
using DKL_Validation.Internal;
using DKL_Validation.Resources;

namespace DKL_Validation.Validators
{
    class PredicateValidator : PropertyValidator, IPredicateValidator
    {
        public delegate bool Predicate(object instanceToValidate, object propertyValue, PropertyValidatorContext propertyValidatorContext);

        private readonly Predicate _predicate;

        public PredicateValidator(Predicate predicate) : base(new LanguageStringSource(nameof(PredicateValidator)))
        {
            predicate.Guard("A predicate must be specified.", nameof(predicate));
            this._predicate = predicate;
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            if(!_predicate(context.Instance, context.PropertyValue, context))
            {
                return false;
            }
            return true;
        }
    }
    public interface IPredicateValidator : IPropertyValidator { }
}
