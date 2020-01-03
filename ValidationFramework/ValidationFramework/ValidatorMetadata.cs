using System;
using System.Collections.Generic;
using System.Text;
using ValidationFramework.Resources;
using ValidationFramework.Validators;

namespace ValidationFramework
{
    public class PropertyValidatorOptions
    {
        private IStringSource _errorSource;
        private IStringSource _errorCodeSource;

        // <summary>
        /// Retrieves the unformatted error message template.
        /// </summary>
        public IStringSource ErrorMessageSource
        {
            get => _errorSource;
            set => _errorSource = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
		/// Retrieves the error code.
		/// </summary>
		public IStringSource ErrorCodeSource
        {
            get => _errorCodeSource;
            set => _errorCodeSource = value ?? throw new ArgumentNullException(nameof(value));
        }
    }
}
