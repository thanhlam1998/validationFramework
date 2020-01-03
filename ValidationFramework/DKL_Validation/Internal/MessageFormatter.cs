using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DKL_Validation.Internal
{
    /// <summary>
    /// Assist in the construction of validation message
    /// </summary>
    public class MessageFormatter
    {
        private static readonly Regex _keyRegex = new Regex("{([^{}:]+)(?::([^{}]+))?}");
        readonly Dictionary<string, object> _placeholderValues = new Dictionary<string, object>(2);
        //object[] _additionalArguments = new object[0];

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

        // object[] AdditionalArguments => _additionalArguments;

        /// <summary>
		/// Additional placeholder values
		/// </summary>
		public Dictionary<string, object> PlaceholderValues => _placeholderValues;

        /// <summary>
		/// Constructs the final message from the specified template. 
		/// </summary>
		/// <param name="messageTemplate">Message template</param>
		/// <returns>The message with placeholders replaced with their appropriate values</returns>
		public virtual string BuildMessage(string messageTemplate)
        {

            string result = messageTemplate;
            result = ReplacePlaceholdersWithValues(result, _placeholderValues);
            return result;
        }

        protected virtual string ReplacePlaceholdersWithValues(string template, IDictionary<string, object> values)
        {
            return _keyRegex.Replace(template, m => {
                var key = m.Groups[1].Value;

                if (!values.ContainsKey(key))
                    return m.Value; // No placeholder / value

                var format = m.Groups[2].Success // Format specified?
                    ? $"{{0:{m.Groups[2].Value}}}"
                    : null;

                return format == null
                    ? values[key]?.ToString()
                    : string.Format(format, values[key]);
            });
        }
    }
}
