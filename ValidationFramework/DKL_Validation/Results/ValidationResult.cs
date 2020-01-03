using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DKL_Validation.Results
{
    public class ValidationResult
    {
        private readonly IList<ValidationFailure> errors;
        public virtual bool IsValid => Errors.Count == 0;

        /// <summary>
        /// A Collection of Errors
        /// </summary>
        public IList<ValidationFailure> Errors => errors;

        public ValidationResult()
        {
            this.errors = new List<ValidationFailure>();
        }

        public ValidationResult(IEnumerable<ValidationFailure> failures)
        {
            errors = failures.Where(failure => failure != null).ToList();
        }

        public override string ToString()
        {
            return ToString(Environment.NewLine);
        }

        public string ToString(string separator)
        {
            return string.Join(separator, errors.Select(failure => failure.ErrorMessage));
        }
    }
}
