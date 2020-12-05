using System;
using System.Collections.Generic;
using System.Linq;

namespace SharpDomain.Persistence.InMemory.Datastore
{
    internal class EntityDatastore<TEntity> where TEntity : class
    {
        private static class TypeLock<T>
        {
            // ReSharper disable once StaticMemberInGenericType
            public static readonly object Lock = new object();
        }
        
        private static readonly Dictionary<Guid, TEntity> DataStore  =
            new Dictionary<Guid, TEntity>();

        public EntityDatastore()
        {
            _models = new DictionaryWithHistory<TEntity>(DataStore);
        }
        
        private DictionaryWithHistory<TEntity> _models;
        
        public IDictionary<Guid, TEntity> Models => _models;
        
        public void Commit()
        {
            lock (TypeLock<TEntity>.Lock)
            {
                foreach (var action in _models.Actions.Reverse())
                {
                    action(DataStore);
                }
            }
        }
        
        public void Rollback() => 
            _models = new DictionaryWithHistory<TEntity>(DataStore);
    }
}