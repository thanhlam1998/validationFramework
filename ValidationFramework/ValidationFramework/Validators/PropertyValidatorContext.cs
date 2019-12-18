using System;
using System.Collections.Generic;
using System.Text;
using ValidationFramework.Internal;

namespace ValidationFramework.Validators
{
    public class PropertyValidatorContext : IValidationContext
    {
        private MessageFormatter _messageFormatter;

        private readonly Lazy<object> _propertyValueContainer;

        public ValidationContext ParentContext { get; private set; }
        public PropertyRule Rule { get; private set; }
        public string PropertyName { get; private set; }
        object[] _additionalArguments = new object[0];

        public string DisplayName => Rule.GetDisplayName(ParentContext);

        public object Instance => ParentContext.InstanceToValidate;

        public PropertyValidatorContext(ValidationContext parentContext, PropertyRule rule, string propertyName)
        {
            ParentContext = parentContext;
            Rule = rule;
            PropertyName = propertyName;
            _propertyValueContainer = new Lazy<object>(() => {
                var value = rule.PropertyFunc(parentContext.InstanceToValidate);
                if (rule.Transformer != null) value = rule.Transformer(value);
                return value;
            });
        }

        public PropertyValidatorContext(ValidationContext parentContext, PropertyRule rule, string propertyName, object propertyValue)
        {
            ParentContext = parentContext;
            Rule = rule;
            PropertyName = propertyName;
            _propertyValueContainer = new Lazy<object>(() => propertyValue);
        }
        
        public object InstanceToValidate => ParentContext.InstanceToValidate;

        public MessageFormatter MessageFormatter => _messageFormatter ?? (_messageFormatter = ValidatorOptions.MessageFormatterFactory());

        public object PropertyValue => _propertyValueContainer.Value;
    }
}
