using System;
using System.Collections.Generic;

namespace SharpDomain.Persistence.InMemory.Datastore
{
    internal class ModelDatastore<TModel> where TModel : class
    {
        private static class TypeLock<T>
        {
            // ReSharper disable once StaticMemberInGenericType
            public static readonly object Lock = new object();
        }
        
        private static readonly Dictionary<Guid, TModel> DataStore  =
            new Dictionary<Guid, TModel>();

        public ModelDatastore()
        {
            _models = new DictionaryWithHistory<TModel>(DataStore);
        }
        
        private DictionaryWithHistory<TModel> _models;
        
        public IDictionary<Guid, TModel> Models => _models;
        
        public void Commit()
        {
            lock (TypeLock<TModel>.Lock)
            {
                foreach (var action in _models.Actions)
                {
                    action(DataStore);
                }
            }
        }
        
        public void Rollback() => 
            _models = new DictionaryWithHistory<TModel>(DataStore);
    }
}