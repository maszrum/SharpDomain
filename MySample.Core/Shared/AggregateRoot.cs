﻿using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace MySample.Core.Shared
{
    public abstract class AggregateRoot<T> where T : AggregateRoot<T>
    {
        public Guid Id { get; protected set; }
        
        private ModelPropertiesModifier<T>? _modifier;
        
        protected bool ModifyAllowed => !_modifier?.IsDisposed ?? false;
        
        public IAsyncDisposable ModifyProperties()
        {
            if (this is T objectTyped)
            {
                _modifier = new ModelPropertiesModifier<T>(objectTyped);
                return _modifier;
            }
            
            throw new InvalidOperationException();
        }
        
        protected Task ModifyProperties(Action<T> action)
        {
            async Task StartModifyingAndDo(Action<T> a, T obj)
            {
                await using (ModifyProperties())
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
                    $"{nameof(ModifyProperties)} method must be called before using property setter");
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