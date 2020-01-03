using System;
using System.Collections.Generic;
using System.Text;

namespace DKL_Validation.Resources
{
    public interface IStringSource
    {
        /// <summary>
		/// Construct the error message template
		/// </summary>
		/// <returns>Error message template</returns>
		string GetString(IValidationContext context);
    }
}
