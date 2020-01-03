using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace DKL_Validation.Internal
{
    public static class AccessorCache<T>
    {
        private static readonly ConcurrentDictionary<Key, Delegate> _cache = new ConcurrentDictionary<Key, Delegate>();

        public static Func<T, TProperty> GetCachedAccessor<TProperty>(MemberInfo member, Expression<Func<T, TProperty>> expression, bool bypassCache = false)
        {
            if (member == null || bypassCache)
            {
                return expression.Compile();
            }

            var key = new Key(member, expression);
            return (Func<T, TProperty>)_cache.GetOrAdd(key, k => expression.Compile());
        }

        public static void Clear()
        {
            _cache.Clear();
        }

        private class Key
        {
            private readonly MemberInfo _memberInfo;
            private readonly string _expressionDebugView;

            public Key(MemberInfo member, Expression expression)
            {
                _memberInfo = member;
                _expressionDebugView = expression.ToString();
            }

            protected bool Equals(Key other)
            {
                return Equals(_memberInfo, other._memberInfo) && string.Equals(_expressionDebugView, other._expressionDebugView);
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != this.GetType()) return false;
                return Equals((Key)obj);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    return ((_memberInfo != null ? _memberInfo.GetHashCode() : 0) * 397) ^ (_expressionDebugView != null ? _expressionDebugView.GetHashCode() : 0);
                }
            }

        }
    }
}
