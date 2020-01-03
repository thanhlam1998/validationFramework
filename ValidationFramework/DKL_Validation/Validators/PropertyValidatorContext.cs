using System;
using System.Collections.Generic;
using System.Text;
using DKL_Validation.Internal;

namespace DKL_Validation.Validators
{
    public class PropertyValidatorContext : IValidationContext
    {
        private MessageFormatter _messageFormatter;

        public ValidationContext ParentContext { get; private set; }
        public PropertyRule Rule { get; private set; }
        public string PropertyName { get; private set; }

        public string DisplayName => Rule.GetDisplayName(ParentContext);

        public object Instance => ParentContext.InstanceToValidate;

        public PropertyValidatorContext(ValidationContext parentContext, PropertyRule rule, string propertyName)
        {
            ParentContext = parentContext;
            Rule = rule;
            PropertyName = propertyName;
            PropertyValue = rule.PropertyFunc(parentContext.InstanceToValidate);     
        }

        public PropertyValidatorContext(ValidationContext parentContext, PropertyRule rule, string propertyName, object propertyValue)
        {
            ParentContext = parentContext;
            Rule = rule;
            PropertyName = propertyName;
            PropertyValue = propertyValue;
        }
        
        public object InstanceToValidate => ParentContext.InstanceToValidate;

        public MessageFormatter MessageFormatter => _messageFormatter ?? (_messageFormatter = ValidatorOptions.MessageFormatterFactory());

        public object PropertyValue { get; set; }
    }
}
