using System;
using System.Collections.Generic;
using System.Text;
using ValidationFramework.Internal;

namespace ValidationFramework
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
        //public ValidationContext(T instanceToValidate) : this(instanceToValidate, new PropertyChain())
        //{
        //}

        public ValidationContext(T instanceToValidate) : base(instanceToValidate)
        {
            InstanceToValidate = instanceToValidate;
        }
        public new T InstanceToValidate { get; private set; }

    }

    public class ValidationContext : IValidationContext
    {
        private ValidationContext _parentContext;

        /// <summary>
        /// Additional data associated with the validation request.
        /// </summary>
        public IDictionary<string, object> RootContextData { get; private set; } = new Dictionary<string, object>();

        public ValidationContext(object instanceToValidate)
        {
            InstanceToValidate = instanceToValidate;
        }


        public object InstanceToValidate { get; private set; }

        public object PropertyValue => throw new NotImplementedException();

        public ValidationContext ParentContext => throw new NotImplementedException();

        /// <summary>
		/// Whether this is a child context
		/// </summary>
		public virtual bool IsChildContext { get; internal set; }

        /// <summary>
        /// Whether this is a child collection context.
        /// </summary>
        public virtual bool IsChildCollectionContext { get; internal set; }

        internal ValidationContext<T> ToGeneric<T>()
        {
            return new ValidationContext<T>((T)InstanceToValidate)
            {
                IsChildContext = IsChildContext,
                RootContextData = RootContextData,
                _parentContext = _parentContext
            };
        }
    }


}
