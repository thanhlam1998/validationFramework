using System;
using System.Collections.Generic;
using System.Text;

namespace DKL_Validation.Internal
{
    public class RuleBuilder<T, TProperty> : IRuleBuilderOptions<T, TProperty>, IRuleBuilderInitial<T, TProperty>
    {
        public PropertyRule Rule { get; }

        public IValidator<T> ParentValidator { get; }

        public RuleBuilder(PropertyRule rule, IValidator<T> parent)
        {
            Rule = rule;
            ParentValidator = parent;
        }

        public IRuleBuilderOptions<T, TProperty> Configure(Action<PropertyRule> configurator)
        {
            configurator(Rule);
            return this;
        }

        public IRuleBuilderOptions<T, TProperty> SetValidator(IPropertyValidator validator)
        {
            validator.Guard("Cannot pass a null validator to SetValidator.", nameof(validator));
            Rule.AddValidator(validator);
            return this;
        }

        IRuleBuilderInitial<T, TProperty> IConfigurable<PropertyRule, IRuleBuilderInitial<T, TProperty>>.Configure(Action<PropertyRule> configurator)
        {
            configurator(Rule);
            return this;
        }
    }
}
