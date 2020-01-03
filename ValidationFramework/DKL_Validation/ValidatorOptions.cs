using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using DKL_Validation.Validators;
using DKL_Validation.Internal;
using DKL_Validation.Resources;

namespace DKL_Validation
{
    public static class ValidatorOptions
    {
        /// <summary>
		/// Default cascade mode
		/// </summary>
		public static CascadeMode CascadeMode = CascadeMode.Continue;

        private static Func<PropertyValidator, string> _errorCodeResolver = DefaultErrorCodeResolver;

        private static Func<MessageFormatter> _messageFormatterFactory = () => new MessageFormatter();
        /// <summary>
		/// Specifies a factory for creating MessageFormatter instances
		/// </summary>
		public static Func<MessageFormatter> MessageFormatterFactory
        {
            get => _messageFormatterFactory;
            set => _messageFormatterFactory = value ?? (() => new MessageFormatter());
        }
        private static ILanguageManager _languageManager = new LanguageManager();

        /// <summary>
		/// Default language manager 
		/// </summary>
		public static ILanguageManager LanguageManager
        {
            get => _languageManager;
            set => _languageManager = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
		/// Pluggable resolver for default error codes
		/// </summary>
		public static Func<PropertyValidator, string> ErrorCodeResolver
        {
            get => _errorCodeResolver;
            set => _errorCodeResolver = value ?? DefaultErrorCodeResolver;
        }

        static string DefaultErrorCodeResolver(PropertyValidator validator)
        {
            return validator.GetType().Name;
        }
    }

}
