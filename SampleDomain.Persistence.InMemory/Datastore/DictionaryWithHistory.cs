using System;
using System.Collections;
using System.Collections.Generic;

namespace SampleDomain.Persistence.InMemory.Datastore
{
    internal class DictionaryWithHistory<TEntity> : IDictionary<Guid, TEntity>
    {
        public DictionaryWithHistory(IDictionary<Guid, TEntity> dictionary)
        {
            _dictionary = new Dictionary<Guid, TEntity>(dictionary);
        }
        
        private readonly Dictionary<Guid, TEntity> _dictionary;
        private readonly List<Action<IDictionary<Guid, TEntity>>> _actions = 
            new List<Action<IDictionary<Guid, TEntity>>>();

        public IReadOnlyList<Action<IDictionary<Guid, TEntity>>> Actions => _actions;
        
        /*
         * indexer
         */
        
        public TEntity this[Guid key]
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

        public ICollection<TEntity> Values => _dictionary.Values;
        
        public int Count => _dictionary.Count;
        
        public IEnumerator<KeyValuePair<Guid, TEntity>> GetEnumerator() => _dictionary.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable) _dictionary).GetEnumerator();

        /*
         * methods
         */

        public bool ContainsKey(Guid key) => 
            _dictionary.ContainsKey(key);
        
        public bool TryGetValue(Guid key, out TEntity value) => 
            _dictionary.TryGetValue(key, out value);
        
        public void Clear()
        {
            _dictionary.Clear();
            _actions.Add(dict => dict.Clear());
        }

        public void Add(Guid key, TEntity value)
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
        
        bool ICollection<KeyValuePair<Guid, TEntity>>.IsReadOnly => DictionaryAsCollection().IsReadOnly;
        
        bool ICollection<KeyValuePair<Guid, TEntity>>.Contains(KeyValuePair<Guid, TEntity> item) => 
            DictionaryAsCollection().Contains(item);
        
        void ICollection<KeyValuePair<Guid, TEntity>>.CopyTo(KeyValuePair<Guid, TEntity>[] array, int arrayIndex) => 
            DictionaryAsCollection().CopyTo(array, arrayIndex);

        bool ICollection<KeyValuePair<Guid, TEntity>>.Remove(KeyValuePair<Guid, TEntity> item)
        {
            var result = DictionaryAsCollection().Remove(item);
            _actions.Add(dict => dict.Remove(item.Key));
            return result;
        }

        void ICollection<KeyValuePair<Guid, TEntity>>.Add(KeyValuePair<Guid, TEntity> item)
        {
            DictionaryAsCollection().Add(item);
            _actions.Add(dict => dict.Add(item.Key, item.Value));
        }

        private ICollection<KeyValuePair<Guid, TEntity>> DictionaryAsCollection() => _dictionary;
    }
}