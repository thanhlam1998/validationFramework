using System;
using System.Collections.Generic;
using System.Text;
using DKL_Validation.Resources;
using DKL_Validation.Validators;

namespace DKL_Validation.Internal
{
    public class MessageBuilderContext : IValidationContext
    {
        private PropertyValidatorContext _innerContext;

        public MessageBuilderContext(PropertyValidatorContext innerContext, IStringSource errorSource, IPropertyValidator propertyValidator)
        {
            _innerContext = innerContext;
            ErrorSource = errorSource;
            PropertyValidator = propertyValidator;
        }

        public IPropertyValidator PropertyValidator { get; }

        public IStringSource ErrorSource { get; }

        public ValidationContext ParentContext => _innerContext.ParentContext;

        public string PropertyName => _innerContext.PropertyName;

        public string DisplayName => _innerContext.DisplayName;

        public object Instance => _innerContext.Instance;

        public MessageFormatter MessageFormatter => _innerContext.MessageFormatter;

        public object InstanceToValidate => _innerContext.Instance;

        public object PropertyValue => _innerContext.PropertyValue;

        public string GetDefaultMessage()
        {
            return MessageFormatter.BuildMessage(ErrorSource.GetString(_innerContext));
        }
    }
}
