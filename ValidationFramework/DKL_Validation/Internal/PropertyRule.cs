using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using DKL_Validation.Resources;
using DKL_Validation.Results;
using DKL_Validation.Validators;

namespace DKL_Validation.Internal
{
    public class PropertyRule : IValidationRule
    {
        readonly List<IPropertyValidator> _validators = new List<IPropertyValidator>();
        Func<CascadeMode> _cascadeModeThunk = () => ValidatorOptions.CascadeMode;
        string _propertyDisplayName;
        string _propertyName;

        /// <summary>
		/// String source that can be used to retrieve the display name (if null, falls back to the property name)
		/// </summary>
		public IStringSource DisplayName { get; set; }

        public MemberInfo Member { get; }

        /// <summary>
		/// Function that can be invoked to retrieve the value of the property.
		/// </summary>
		public Func<object, object> PropertyFunc { get; }

        /// <summary>
        /// Expression that was used to create the rule.
        /// </summary>
        public LambdaExpression Expression { get; }

        /// <summary>
		/// The current validator being configured by this rule.
		/// </summary>
        public IPropertyValidator CurrentValidator => _validators.LastOrDefault();

        /// <summary>
		/// Type of the property being validated
		/// </summary>
		public Type TypeToValidate { get; }

        /// <summary>
		/// Display name for the property.
		/// </summary>
		public string GetDisplayName()
        {
            string result = null;

            if (DisplayName != null)
            {
                result = DisplayName.GetString(null);
            }

            if (result == null)
            {
                result = _propertyDisplayName;
            }

            return result;
        }

        /// <summary>
        /// Display name for the property.
        /// </summary>
        public string GetDisplayName(IValidationContext context)
        {
            string result = null;

            if (DisplayName != null)
            {
                result = DisplayName.GetString(context);
            }

            if (result == null)
            {
                result = _propertyDisplayName;
            }

            return result;
        }

        /// <summary>
		/// Cascade mode for this rule.
		/// </summary>
		public CascadeMode CascadeMode
        {
            get => _cascadeModeThunk();
            set => _cascadeModeThunk = () => value;
        }

        public IEnumerable<IPropertyValidator> Validators => _validators;

        public PropertyRule(MemberInfo member, Func<object, object> propertyFunc, LambdaExpression expression, Func<CascadeMode> cascadeModeThunk, Type typeToValidate)
        {
            Member = member;
            PropertyFunc = propertyFunc;
            Expression = expression;
            TypeToValidate = typeToValidate;
            this._cascadeModeThunk = cascadeModeThunk;
            PropertyName = member.Name.ToString();

        }

        /// <summary>
		/// Creates a new property rule from a lambda expression.
		/// </summary>
		public static PropertyRule Create<T, TProperty>(Expression<Func<T, TProperty>> expression)
        {
            return Create(expression, () => ValidatorOptions.CascadeMode);
        }

        public static PropertyRule Create<T, TProperty>(Expression<Func<T, TProperty>> expression, Func<CascadeMode> cascadeModeThunk, bool bypassCache = false)
        {
            var member = expression.GetMember();
            var compiled = AccessorCache<T>.GetCachedAccessor(member, expression, bypassCache);
            return new PropertyRule(member, compiled.CoerceToNonGeneric(), expression, cascadeModeThunk, typeof(TProperty));
        }

        /// <summary>
		/// Adds a validator to the rule.
		/// </summary>
		public void AddValidator(IPropertyValidator validator)
        {
            _validators.Add(validator);
        }

        /// <summary>
        /// Replaces a validator in this rule. Used to wrap validators.
        /// </summary>
        public void ReplaceValidator(IPropertyValidator original, IPropertyValidator newValidator)
        {
            var index = _validators.IndexOf(original);

            if (index > -1)
            {
                _validators[index] = newValidator;
            }
        }

        /// <summary>
        /// Remove a validator in this rule.
        /// </summary>
        public void RemoveValidator(IPropertyValidator original)
        {
            _validators.Remove(original);
        }

        /// <summary>
        /// Clear all validators from this rule.
        /// </summary>
        public void ClearValidators()
        {
            _validators.Clear();
        }

        public string PropertyName
        {
            get { return _propertyName; }
            set
            {
                _propertyName = value;
                _propertyDisplayName = _propertyName.SplitPascalCase();
            }
        }

        public Func<MessageBuilderContext, string> MessageBuilder { get; set; }

        /// <summary>
		/// Dependent rules
		/// </summary>
		

        public IEnumerable<ValidationFailure> Validate(ValidationContext context)
        {
            string displayName = GetDisplayName(context);
            if (PropertyName == null && displayName == null)
            {
                //No name has been specified. Assume this is a model-level rule, so we should use empty string instead.
                displayName = string.Empty;
            }
            // Construct the full name of the property, taking into account overriden property names and the chain (if we're in a nested validator)
            //string propertyName = context.PropertyChain.BuildPropertyName(PropertyName ?? displayName);
            var cascade = _cascadeModeThunk();
            // Invoke each validator and collect its results.
            foreach (var validator in _validators)
            {
                IEnumerable<ValidationFailure> results;
                results = InvokePropertyValidator(context, validator, PropertyName);
                foreach (var result in results)
                {
                    yield return result;
                }
            }
        }

        /// <summary>
		/// Invokes a property validator using the specified validation context.
		/// </summary>
		protected virtual IEnumerable<ValidationFailure> InvokePropertyValidator(ValidationContext context, IPropertyValidator validator, string propertyName)
        {
            var propertyContext = new PropertyValidatorContext(context, this, propertyName);
            return validator.Validate(propertyContext);
        }
    }
}
