using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Autofac;

namespace SharpDomain.IoC
{
    internal class ActionsContainer
    {
        private readonly Dictionary<int, Action<IComponentContext>> _syncActions = new();
        private readonly Dictionary<int, Func<IComponentContext, Task>> _asyncActions = new();
        
        private int _orderCounter = 0;
        
        public int ActionsCount => _orderCounter;
        
        public void Add(Func<IComponentContext, Task> action)
        {
            _asyncActions.Add(_orderCounter, action);
            _orderCounter++;
        }
        
        public void Add(Action<IComponentContext> action)
        {
            _syncActions.Add(_orderCounter, action);
            _orderCounter++;
        }
        
        public bool TryGetAsyncAction(int index, [NotNullWhen(true)]out Func<IComponentContext, Task>? action) => 
            _asyncActions.TryGetValue(index, out action);
        
        public bool TryGetSyncAction(int index, [NotNullWhen(true)]out Action<IComponentContext>? action) => 
            _syncActions.TryGetValue(index, out action);
    }
}