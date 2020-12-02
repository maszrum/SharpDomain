using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace MySample.Core.Shared
{
    public abstract class AggregateRoot<T> where T : AggregateRoot<T>
    {
        public Guid Id { get; protected set; }
        
        private ModelChangesPublisher<T>? _changesPublisher;
        
        protected bool ModifyAllowed => !_changesPublisher?.IsDisposed ?? false;
        
        public IAsyncDisposable PublishChanges()
        {
            if (this is T objectTyped)
            {
                _changesPublisher = new ModelChangesPublisher<T>(objectTyped);
                return _changesPublisher;
            }
            
            throw new InvalidOperationException();
        }
        
        protected Task PublishChanges(Action<T> action)
        {
            async Task StartModifyingAndDo(Action<T> a, T obj)
            {
                await using (PublishChanges())
                {
                    a(obj);
                }
            }
            
            if (this is T objectTyped)
            {
                if (ModifyAllowed)
                {
                    action(objectTyped);
                    return Task.CompletedTask;
                }

                return StartModifyingAndDo(action, objectTyped);
            }
            
            throw new InvalidOperationException();
        }
        
        protected void SetPropertyValue<TValue>(Expression<Func<T, TValue>> propertySelector, ref TValue backingField, TValue propertyValue)
        {
            if (!ModifyAllowed)
            {
                throw new InvalidOperationException(
                    $"{nameof(PublishChanges)} method must be called before using property setter");
            }
            
            if (!(_changesPublisher is null))
            {
                var propertyName = GetPropertyName(propertySelector);
                _changesPublisher.AppendPropertyModified(propertyName);
            }
            
            backingField = propertyValue;
        }
        
        private static string GetPropertyName<TValue>(Expression<Func<T, TValue>> property)
        {
            if (property.Body is MemberExpression memberExpression)
            {
                if (memberExpression.Member is PropertyInfo propertyInfo)
                {
                    return propertyInfo.Name;
                }
            }
            
            throw new ArgumentException(
                "lambda expression should point to a valid property");
        }
    }
}