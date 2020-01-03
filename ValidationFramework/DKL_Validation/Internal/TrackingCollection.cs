using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DKL_Validation.Internal
{
    public class TrackingCollection<T> : IEnumerable<T>
    {
        readonly List<T> _innerCollection = new List<T>();

        public void Add (T item)
        {
                _innerCollection.Add(item);
        }

        public int Count => _innerCollection.Count;

        public void Remove(T item)
        {
            _innerCollection.Remove(item);
        }

        public void AddRange(IEnumerable<T> collection)
        {
            _innerCollection.AddRange(collection);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _innerCollection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
