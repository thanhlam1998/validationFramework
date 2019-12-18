using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace ValidationFramework.Internal
{
    public class PropertyChain
    {
        readonly List<string> _memberNames = new List<string>(2);

        /// <summary>
        /// Constructor
        /// </summary>
        public PropertyChain()
        {
        }

        /// <summary>
        /// Create a new Property Chain based on another
        /// </summary>
        /// <param name="parent"></param>
        public PropertyChain(PropertyChain parent)
        {
            if(parent != null && parent._memberNames.Count > 0)
            {
                _memberNames.AddRange(parent._memberNames);
            }
        }

        /// <summary>
		/// Creates a new PropertyChain
		/// </summary>
		/// <param name="memberNames"></param>
		public PropertyChain(IEnumerable<string> memberNames)
        {
            this._memberNames.AddRange(memberNames);
        }

        /// <summary>
		/// Adds a MemberInfo instance to the chain
		/// </summary>
		/// <param name="member">Member to add</param>
		public void Add(MemberInfo member)
        {
            if (member != null)
                _memberNames.Add(member.Name);
        }

        /// <summary>
        /// Adds a property name to the chain
        /// </summary>
        /// <param name="propertyName">Name of the property to add</param>
        public void Add(string propertyName)
        {
            if (!string.IsNullOrEmpty(propertyName))
                _memberNames.Add(propertyName);
        }

        public static PropertyChain FromExpression<T>(Expression<Func<T, object>> expression)
        {
            var memberNames = new Stack<string>();

            var getMemberExp = new Func<Expression, MemberExpression>(toUnwrap => {
                if (toUnwrap is UnaryExpression)
                {
                    return ((UnaryExpression)toUnwrap).Operand as MemberExpression;
                }

                return toUnwrap as MemberExpression;
            });

            var memberExp = getMemberExp(expression.Body);

            while (memberExp != null)
            {
                memberNames.Push(memberExp.Member.Name);
                memberExp = getMemberExp(memberExp.Expression);
            }

            return new PropertyChain(memberNames);
        }

        /// <summary>
        /// Creates a string representation of a property chain.
        /// </summary>
        //public override string ToString()
        //      {
        //          // Performance: Calling string.Join causes much overhead when it's not needed.
        //          switch (_memberNames.Count)
        //          {
        //              case 0:
        //                  return string.Empty;
        //              case 1:
        //                  return _memberNames[0];
        //              default:
        //                  return string.Join(ValidatorOptions.PropertyChainSeparator, _memberNames);
        //          }
        //      }

        /// <summary>
        /// Builds a property path.
        /// </summary>
        public string BuildPropertyName(string propertyName)
        {
            if (_memberNames.Count == 0)
            {
                return propertyName;
            }

            var chain = new PropertyChain(this);
            chain.Add(propertyName);
            return chain.ToString();
        }
        public int Count => _memberNames.Count;
    }
}
