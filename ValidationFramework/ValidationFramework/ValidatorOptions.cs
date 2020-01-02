using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
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

        private static Func<PropertyValidator, string> _errorCodeResolver = DefaultErrorCodeResolver;

        private static Func<Type, MemberInfo, LambdaExpression, string> _propertyNameResolver = DefaultPropertyNameResolver;
		private static Func<Type, MemberInfo, LambdaExpression, string> _displayNameResolver = DefaultDisplayNameResolver;

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

        /// <summary>
        /// Pluggable logic for resolving property names
        /// </summary>
        public static Func<Type, MemberInfo, LambdaExpression, string> PropertyNameResolver
        {
            get => _propertyNameResolver;
            set => _propertyNameResolver = value ?? DefaultPropertyNameResolver;
        }

        /// <summary>
		/// Pluggable logic for resolving display names
		/// </summary>
		public static Func<Type, MemberInfo, LambdaExpression, string> DisplayNameResolver
        {
            get => _displayNameResolver;
            set => _displayNameResolver = value ?? DefaultDisplayNameResolver;
        }

        static string DefaultPropertyNameResolver(Type type, MemberInfo memberInfo, LambdaExpression expression)
        {
            if (expression != null)
            {
                var chain = PropertyChain.FromExpression(expression);
                if (chain.Count > 0) return chain.ToString();
            }

            return memberInfo?.Name;
        }

        static string DefaultDisplayNameResolver(Type type, MemberInfo memberInfo, LambdaExpression expression)
        {
            return memberInfo == null ? null : DisplayNameCache.GetCachedDisplayName(memberInfo);
        }


    }

}
