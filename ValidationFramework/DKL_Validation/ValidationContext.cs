using System;
using System.Collections.Generic;
using System.Text;
using DKL_Validation.Internal;

namespace DKL_Validation
{
    public interface IValidationContext
    {
        /// <summary>
        /// The object currently being validate
        /// </summary>
        object InstanceToValidate { get; }

        /// <summary>
        /// The value of perperty being validated
        /// </summary>
        object PropertyValue { get; }

        /// <summary>
        /// Parent validation context
        /// </summary>
        ValidationContext ParentContext { get; }
    }

    public class ValidationContext<T> : ValidationContext
    {

        public ValidationContext(T instanceToValidate) : base(instanceToValidate)
        {
            InstanceToValidate = instanceToValidate;   
        }
        public new T InstanceToValidate { get; private set; }

    }

    public class ValidationContext : IValidationContext
    {
        private ValidationContext _parentContext;

        public ValidationContext(object instanceToValidate)
        {
            InstanceToValidate = instanceToValidate;
        }


        public object InstanceToValidate { get; private set; }

        public object PropertyValue => null;

        public ValidationContext ParentContext => _parentContext;

        internal ValidationContext<T> ToGeneric<T>()
        {
            return new ValidationContext<T>((T)InstanceToValidate)
            {
                _parentContext = _parentContext
            };
        }
    }


}
