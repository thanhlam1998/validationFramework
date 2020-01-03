using System;
using System.Collections.Generic;
using System.Text;
using DKL_Validation.Results;
using DKL_Validation.Internal;
using System.Collections;
using System.Reflection;
using System.Linq;
using System.Linq.Expressions;

namespace DKL_Validation
{
    public abstract class AbstractValidation<T> : IValidator<T>, IEnumerable<IValidationRule>
    {
        internal TrackingCollection<IValidationRule> Rules { get; } = new TrackingCollection<IValidationRule>();
        private Func<CascadeMode> _cascadeMode = () => ValidatorOptions.CascadeMode;
        public CascadeMode CascadeMode { get => _cascadeMode(); set => _cascadeMode = () => value; }

        public bool CanValidateInstancesOfType(Type type)
        {
            return typeof(T).GetTypeInfo().IsAssignableFrom(type.GetTypeInfo());   
        }

        public ValidationResult Validate(ValidationContext context)
        {
            context.Guard("Can not pass null to Validate", nameof(context));
            return Validate(context.ToGeneric<T>());
        }   


        public ValidationResult Validate(object instance)
        {
            instance.Guard("Can not pass null to Validate.", nameof(instance));
            if (!((IValidator)this).CanValidateInstancesOfType(instance.GetType()))
            {
                throw new InvalidOperationException($"Can not validate instances of type '{instance.GetType().Name}'. This validator can only validate instances of type '{typeof(T).Name}'.");
            }
            return Validate((T) instance);
        }



        public ValidationResult Validate(T instance)
        {
            return Validate(new ValidationContext<T>(instance));
        }

        public ValidationResult Validate(ValidationContext<T> context)
        {
            context.Guard("Can not pass null to Validate", nameof(context));
            var result = new ValidationResult();

            EnsureInstanceNotNull(context.InstanceToValidate);
            var failures = Rules.SelectMany(x => x.Validate(context));

            foreach(var validationFailure in failures.Where(failure => failure != null))
            {
                result.Errors.Add(validationFailure);
            }
            return result;  
        }

        protected void AddRule(IValidationRule rule)
        {
            Rules.Add(rule);
        }

        public IRuleBuilderInitial<T, TProperty> RuleFor<TProperty>(Expression<Func<T,TProperty>> expression)
        {
            expression.Guard("Can not pass null to RuleFor", nameof(expression));
            var rule = PropertyRule.Create(expression, () => CascadeMode);
            AddRule(rule);
            var ruleBuilder = new RuleBuilder<T, TProperty>(rule, this);
            return ruleBuilder; 
        }

        /// <summary>
		/// Throws an exception if the instance being validated is null.
		/// </summary>
		/// <param name="instanceToValidate"></param>
		protected virtual void EnsureInstanceNotNull(object instanceToValidate)
        {
            instanceToValidate.Guard("Cannot pass null model to Validate.", nameof(instanceToValidate));
        }

        public IEnumerator<IValidationRule> GetEnumerator()
        {
            return Rules.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
