using System;
using System.Collections.Generic;
using System.Text;

namespace ValidationFramework.Resources
{
    public class LanguageStringSource : IStringSource
    {
        private readonly string _key;
        internal Func<IValidationContext, string> ErrorCodeFunc { get; set; }

        public LanguageStringSource(string key)
        {
            _key = key;
        }

        public string GetString(IValidationContext context)
        {
            var errorCode = ErrorCodeFunc?.Invoke(context);

            if (errorCode != null)
            {
                string result = ValidatorOptions.LanguageManager.GetString(errorCode);

                if (!string.IsNullOrEmpty(result))
                {
                    return result;
                }
            }

            return ValidatorOptions.LanguageManager.GetString(_key);
        }

        public string ResourceName => _key;
        public Type ResourceType => typeof(LanguageManager);
    }
}
