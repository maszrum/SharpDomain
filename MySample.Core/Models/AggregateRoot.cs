using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using MySample.Core.Events;

namespace MySample.Core.Models
{
    public class ModelModifier<T> : IAsyncDisposable where T : AggregateRoot<T>
    {
        private readonly T _model;
        private readonly List<string> _propertiesModified = new List<string>();

        public ModelModifier(T model)
        {
            _model = model;
        }
        
        public bool IsDisposed { get; private set; }

        public void AppendPropertyModified(string propertyName)
        {
            if (!_propertiesModified.Contains(propertyName))
            {
                _propertiesModified.Add(propertyName);
            }
        }
        
        public async ValueTask DisposeAsync()
        {
            IsDisposed = true;
            
            var @event = new ModelChanged<T>(_model, _propertiesModified);
            await DomainEvents.Publish(@event);
        }
    }

    public abstract class AggregateRoot<T> where T : AggregateRoot<T>
    {
        public Guid Id { get; protected set; }
        
        private ModelModifier<T>? _modifier;
        
        protected bool ModifyAllowed => !_modifier?.IsDisposed ?? false;
        
        public IAsyncDisposable StartModifying()
        {
            if (this is T objectTyped)
            {
                _modifier = new ModelModifier<T>(objectTyped);
                return _modifier;
            }
            
            throw new InvalidOperationException();
        }
        
        protected void SetPropertyValue<TValue>(Expression<Func<T, TValue>> propertySelector, ref TValue backingField, TValue propertyValue)
        {
            if (!ModifyAllowed)
            {
                throw new InvalidOperationException(
                    $"{nameof(StartModifying)} method must be called before using property setter");
            }
            
            var propertyName = GetPropertyName(propertySelector);
            _modifier?.AppendPropertyModified(propertyName);
            
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