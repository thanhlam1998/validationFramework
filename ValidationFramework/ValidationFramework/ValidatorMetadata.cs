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

        /// <summary>
		/// Condition associated with the validator. If the condition fails, the validator will not run.
		/// </summary>
		public Func<PropertyValidatorContext, bool> Condition { get; private set; }

        /// <summary>
		/// Function used to retrieve custom state for the validator
		/// </summary>
		public Func<PropertyValidatorContext, object> CustomStateProvider { get; set; }

        /// <summary>
		/// Severity of error.
		/// </summary>
		public Severity Severity { get; set; }

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
