using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ValidationFramework.Internal
{
    class MemberNameValidatorSelector : IValidatorSelector
    {
        readonly IEnumerable<string> _memberNames;

        public MemberNameValidatorSelector(IEnumerable<string> memberNames)
        {
            _memberNames = memberNames;
        }

        /// <summary>
        /// Member names that are validated.
        /// </summary>
        public IEnumerable<string> MemberNames => _memberNames;

        public bool CanExecute(IValidationRule rule, string propertyPath, ValidationContext context)
        {
            throw new NotImplementedException();
        }

        ///<summary>
		/// Creates a MemberNameValidatorSelector from a collection of expressions.
		///</summary>
		public static MemberNameValidatorSelector FromExpressions<T>(params Expression<Func<T, object>>[] propertyExpressions)
        {
            var members = propertyExpressions.Select(MemberFromExpression).ToList();
            return new MemberNameValidatorSelector(members);
        }

        /// <summary>
        /// Gets member names from expressions
        /// </summary>
        /// <param name="propertyExpressions"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string[] MemberNamesFromExpressions<T>(params Expression<Func<T, object>>[] propertyExpressions)
        {
            var members = propertyExpressions.Select(MemberFromExpression).ToArray();
            return members;
        }


        private static string MemberFromExpression<T>(Expression<Func<T, object>> expression)
        {
            var chain = PropertyChain.FromExpression(expression);

            if ( chain.Count == 0)
            {
                throw new ArgumentException($"Expression '{expression}' does not specify a valid property or field.");
            }

            return chain.ToString();
        }
    }
}
