using System;
using System.Collections.Generic;
using System.Text;

namespace ValidationFramework.Internal
{
    public class RuleBuilder<T, TProperty> : IRuleBuilderOptions<T, TProperty>, IRuleBuilderInitial<T, TProperty>, IExposesParentValidator<T>
    {
        public PropertyRule Rule { get; }

        public IValidator<T> ParentValidator { get; }

        public RuleBuilder(PropertyRule rule, IValidator<T> parent)
        {
            Rule = rule;
            ParentValidator = parent;
        }

        public IRuleBuilderOptions<T, TProperty> Configure(Action<PropertyRule> configuration)
        {
            throw new NotImplementedException();
        }

        public IRuleBuilderOptions<T, TProperty> SetValidator(IPropertyValidator validator)
        {
            validator.Guard("Cannot pass a null validator to SetValidator.", nameof(validator));
            Rule.AddValidator(validator);
            return this;
        }

        public IRuleBuilderOptions<T, TProperty> SetValidator(IValidator<TProperty> validator, params string[] ruleSets)
        {
            throw new NotImplementedException();
        }

        IRuleBuilderInitial<T, TProperty> IConfigurable<PropertyRule, IRuleBuilderInitial<T, TProperty>>.Configure(Action<PropertyRule> configurator)
        {
            configurator(Rule);
            return this;
        }
    }

    internal interface IExposesParentValidator<T>
    {
        IValidator<T> ParentValidator { get; }
    }
}
