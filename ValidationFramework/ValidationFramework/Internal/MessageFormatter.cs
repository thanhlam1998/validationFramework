using System;
using System.Collections.Generic;
using System.Text;

namespace ValidationFramework.Internal
{

    /// <summary>
    /// Assist in the construction of validation message
    /// </summary>
    public class MessageFormatter
    {
        readonly Dictionary<string, object> _placeholderValues = new Dictionary<string, object>(2);
        object[] _additionalArguments = new object[0];

        /// <summary>
		/// Default Property Name placeholder.
		/// </summary>
		public const string PropertyName = "PropertyName";

        /// <summary>
        /// Default Property Value placeholder.
        /// </summary>
        public const string PropertyValue = "PropertyValue";

        /// <summary>
		/// Adds a value for a validation message placeholder.
		/// </summary>
		/// <param name="name"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public MessageFormatter AppendArgument(string name, object value)
        {
            _placeholderValues[name] = value;
            return this;
        }

        /// <summary>
        /// Appends a property name to the message.
        /// </summary>
        /// <param name="name">The name of the property</param>
        /// <returns></returns>
        public MessageFormatter AppendPropertyName(string name)
        {
            return AppendArgument(PropertyName, name);
        }

        /// <summary>
        /// Appends a property value to the message.
        /// </summary>
        /// <param name="value">The value of the property</param>
        /// <returns></returns>
        public MessageFormatter AppendPropertyValue(object value)
        {
            return AppendArgument(PropertyValue, value);
        }

        public object[] AdditionalArguments => _additionalArguments;

        /// <summary>
		/// Additional placeholder values
		/// </summary>
		public Dictionary<string, object> PlaceholderValues => _placeholderValues;
    }
}
