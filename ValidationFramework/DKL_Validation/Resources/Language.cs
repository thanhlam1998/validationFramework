using System;
using System.Collections.Generic;
using System.Text;

namespace DKL_Validation.Resources
{
    public abstract class Language
    {
        /// <summary>
		/// Name of language (culture code)
		/// </summary>
		public abstract string Name { get; }

        private readonly Dictionary<string, string> _translations = new Dictionary<string, string>();

        /// <summary>
		/// Adds a translation
		/// </summary>
		/// <param name="key"></param>
		/// <param name="message"></param>
		public virtual void Translate(string key, string message)
        {
            _translations[key] = message;
        }


        /// <summary>
		/// Adds a translation for a type
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="message"></param>
		public void Translate<T>(string message)
        {
            Translate(typeof(T).Name, message);
        }

        /// <summary>
		/// Gets the localized version of a string with a specific key.
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public virtual string GetTranslation(string key)
        {
            string value;

            if (_translations.TryGetValue(key, out value))
            {
                return value;
            }

            return null;
        }

    }
}
