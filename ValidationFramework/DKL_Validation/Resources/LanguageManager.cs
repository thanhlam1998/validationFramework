using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace DKL_Validation.Resources
{
    class LanguageManager : ILanguageManager
    {
        private readonly Dictionary<string, Language> _languages = new Dictionary<string, Language>();
        private Language _fallback;

        public LanguageManager()
        {
            var languages = new Language[] {
                new EnglishLanguage(),
                new VietnameseLanguage()
            };

            foreach (var language in languages)
            {
                _languages[language.Name] = language;
            }

            _fallback = _languages["en"];
        }

        public bool Enabled { get; set; } = true;
        public CultureInfo Culture { get; set; }

        public string GetString(string key, CultureInfo culture = null)
        {
            culture = culture ?? Culture ?? CultureInfo.CurrentUICulture;

            string code = culture.Name;

            var languageToUse = Enabled && _languages.ContainsKey(code) ? _languages[code] : _fallback;

            string value = languageToUse.GetTranslation(key);

            if(string.IsNullOrEmpty(value) && languageToUse != _fallback)
            {
                value = _fallback.GetTranslation(key);
            }

            return value ?? string.Empty;
        }
    }
}
