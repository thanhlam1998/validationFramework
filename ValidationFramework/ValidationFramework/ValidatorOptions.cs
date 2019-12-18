﻿using System;
using System.Collections.Generic;
using System.Text;
using ValidationFramework.Internal;
using ValidationFramework.Resources;
using ValidationFramework.Validators;

namespace ValidationFramework
{
    public static class ValidatorOptions
    {
        /// <summary>
		/// Default cascade mode
		/// </summary>
		public static CascadeMode CascadeMode = CascadeMode.Continue;

        /// <summary>
		/// Disables the expression accessor cache. Not recommended.
		/// </summary>
		public static bool DisableAccessorCache { get; set; }

        /// <summary>
		/// Default property chain separator
		/// </summary>
		public static string PropertyChainSeparator = ".";

        public static ValidatorSelectorOptions ValidatorSelectors { get; } = new ValidatorSelectorOptions();

        private static Func<PropertyValidator, string> _errorCodeResolver = DefaultErrorCodeResolver;

        private static Func<MessageFormatter> _messageFormatterFactory = () => new MessageFormatter();
        /// <summary>
		/// Specifies a factory for creating MessageFormatter instances.
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

    public class ValidatorSelectorOptions
    {
        private Func<IValidatorSelector> _defaultValidatorSelector = () => new DefaultValidatorSelector();
        private Func<string[], IValidatorSelector> _memberNmaeValidatorSelector = properties => new MemberNameValidatorSelector(properties);
         /// <summary>
		/// Factory func for creating the default validator selector
		/// </summary>
		public Func<IValidatorSelector> DefaultValidatorSelectorFactory
        {
            get => _defaultValidatorSelector;
            set => _defaultValidatorSelector = value ?? (() => new DefaultValidatorSelector());
        }
    }

}
