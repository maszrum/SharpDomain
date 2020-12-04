using System;
using System.Collections;
using System.Collections.Generic;

namespace SharpDomain.Persistence.InMemory.Datastore
{
    internal class DictionaryWithHistory<TModel> : IDictionary<Guid, TModel>
    {
        public DictionaryWithHistory(IDictionary<Guid, TModel> dictionary)
        {
            _dictionary = new Dictionary<Guid, TModel>(dictionary);
        }
        
        private readonly Dictionary<Guid, TModel> _dictionary;
        private readonly List<Action<IDictionary<Guid, TModel>>> _actions = 
            new List<Action<IDictionary<Guid, TModel>>>();

        public IReadOnlyList<Action<IDictionary<Guid, TModel>>> Actions => _actions;
        
        /*
         * indexer
         */
        
        public TModel this[Guid key]
        {
            get => _dictionary[key];
            set
            {
                _dictionary[key] = value;
                _actions.Add(dict => dict[key] = value);
            }
        }

        /*
         * properties
         */
        
        public ICollection<Guid> Keys => _dictionary.Keys;

        public ICollection<TModel> Values => _dictionary.Values;
        
        public int Count => _dictionary.Count;
        
        public IEnumerator<KeyValuePair<Guid, TModel>> GetEnumerator() => _dictionary.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable) _dictionary).GetEnumerator();

        /*
         * methods
         */

        public bool ContainsKey(Guid key) => 
            _dictionary.ContainsKey(key);
        
        public bool TryGetValue(Guid key, out TModel value) => 
            _dictionary.TryGetValue(key, out value);
        
        public void Clear()
        {
            _dictionary.Clear();
            _actions.Add(dict => dict.Clear());
        }

        public void Add(Guid key, TModel value)
        {
            _dictionary.Add(key, value);
            _actions.Add(dict => dict.Add(key, value));
        }

        public bool Remove(Guid key)
        {
            var result = _dictionary.Remove(key);
            _actions.Add(dict => dict.Remove(key));
            return result;
        }

        /*
         * implicit methods & properties
         */
        
        bool ICollection<KeyValuePair<Guid, TModel>>.IsReadOnly => DictionaryAsCollection().IsReadOnly;
        
        bool ICollection<KeyValuePair<Guid, TModel>>.Contains(KeyValuePair<Guid, TModel> item) => 
            DictionaryAsCollection().Contains(item);
        
        void ICollection<KeyValuePair<Guid, TModel>>.CopyTo(KeyValuePair<Guid, TModel>[] array, int arrayIndex) => 
            DictionaryAsCollection().CopyTo(array, arrayIndex);

        bool ICollection<KeyValuePair<Guid, TModel>>.Remove(KeyValuePair<Guid, TModel> item)
        {
            var result = DictionaryAsCollection().Remove(item);
            _actions.Add(dict => dict.Remove(item.Key));
            return result;
        }

        void ICollection<KeyValuePair<Guid, TModel>>.Add(KeyValuePair<Guid, TModel> item)
        {
            DictionaryAsCollection().Add(item);
            _actions.Add(dict => dict.Add(item.Key, item.Value));
        }

        private ICollection<KeyValuePair<Guid, TModel>> DictionaryAsCollection() => _dictionary;
    }
}