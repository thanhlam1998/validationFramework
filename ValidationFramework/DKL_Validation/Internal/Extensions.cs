using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace DKL_Validation.Internal
{
    public static class Extensions
    {
        internal static void Guard(this object obj, string message, string paramName)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(paramName, message);
            }
        }

        internal static void Guard(this string str, string message, string paramName)
        {
            if(str == null)
            {
                throw new ArgumentNullException(paramName, message); 
            }
            if (string.IsNullOrEmpty(str))
            {
                throw new ArgumentException(message, paramName);
            }
        }

        public static MemberInfo GetMember<T,TProperty>(this Expression<Func<T,TProperty>> expression)
        {
            var memberExp = RemoveUnary(expression.Body) as MemberExpression;

            if (memberExp == null)
            {
                return null;
            }

            Expression currentExpr = memberExp.Expression;

            // Unwind the expression to get the root object that the expression acts upon. 
            while (true)
            {
                currentExpr = RemoveUnary(currentExpr);

                if (currentExpr != null && currentExpr.NodeType == ExpressionType.MemberAccess)
                {
                    currentExpr = ((MemberExpression)currentExpr).Expression;
                }
                else
                {
                    break;
                }
            }

            if (currentExpr == null || currentExpr.NodeType != ExpressionType.Parameter)
            {
                return null; // We don't care if we're not acting upon the model instance. 
            }

            return memberExp.Member;
        }

        private static Expression RemoveUnary(Expression toUnwrap)
        {
            if (toUnwrap is UnaryExpression)
            {
                return ((UnaryExpression)toUnwrap).Operand;
            }

            return toUnwrap;
        }


        public static Func<object, object> CoerceToNonGeneric<T, TProperty>(this Func<T, TProperty> func)
        {
            return x => func((T)x);
        }

        public static Func<object, bool> CoerceToNonGeneric<T>(this Func<T, bool> func)
        {
            return x => func((T)x);
        }

        public static Func<object, int> CoerceToNonGeneric<T>(this Func<T, int> func)
        {
            return x => func((T)x);
        }

        public static Func<object, long> CoerceToNonGeneric<T>(this Func<T, long> func)
        {
            return x => func((T)x);
        }

        public static Func<object, string> CoerceToNonGeneric<T>(this Func<T, string> func)
        {
            return x => func((T)x);
        }

        public static Func<object, System.Text.RegularExpressions.Regex> CoerceToNonGeneric<T>(this Func<T, System.Text.RegularExpressions.Regex> func)
        {
            return x => func((T)x);
        }

        public static Action<object> CoerceToNonGeneric<T>(this Action<T> action)
        {
            return x => action((T)x);
        }



        internal static T GetOrAdd<T>(this IDictionary<string, object> dict, string key, Func<T> value)
        {
            if(dict.TryGetValue(key, out var tmp))
            {
                if(tmp is T result)
                {
                    return result;
                }
            }

            var val = value();
            dict[key] = val;
            return val;
        }

        /// <summary>
		/// Splits pascal case, so "FooBar" would become "Foo Bar"
		/// </summary>
		public static string SplitPascalCase(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            var retVal = new StringBuilder(input.Length + 5);

            for (int i = 0; i < input.Length; ++i)
            {
                var currentChar = input[i];
                if (char.IsUpper(currentChar))
                {
                    if ((i > 1 && !char.IsUpper(input[i - 1]))
                            || (i + 1 < input.Length && !char.IsUpper(input[i + 1])))
                        retVal.Append(' ');
                }

                retVal.Append(currentChar);
            }

            return retVal.ToString().Trim();
        }
    }
}
