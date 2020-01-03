using System;
using System.Collections.Generic;
using System.Text;

namespace DKL_Validation.Resources
{
    public class StaticStringSource : IStringSource
    {
        readonly string _message;

        public StaticStringSource(string message)
        {
            _message = message;
        }

        public string GetString(IValidationContext context)
        {
            return _message;
        }

        /// <summary>
		/// The name of the resource if localized.
		/// </summary>
		public string ResourceName => null;

        /// <summary>
        /// The type of the resource provider if localized.
        /// </summary>
        public Type ResourceType => null;
    }
}
