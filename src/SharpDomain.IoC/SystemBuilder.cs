using System;
using System.Threading.Tasks;
using Autofac;

namespace SharpDomain.IoC
{
    public abstract class SystemBuilder
    {
        private readonly ActionsContainer _onBuildActionsContainer = new();
            
        protected ContainerBuilder ContainerBuilder { get; private set; } = new();
        
        public SystemBuilder UseContainerBuilder(ContainerBuilder containerBuilder)
        {
            ContainerBuilder = containerBuilder;
            return this;
        }
        
        public abstract SystemBuilder WireUpApplication();
        
        public SystemBuilder With(Action<ContainerBuilder> action)
        {
            action(ContainerBuilder);
            return this;
        }
        
        public SystemBuilder OnBuild(Func<IComponentContext, Task> action)
        {
            _onBuildActionsContainer.Add(action);
            return this;
        }
        
        public SystemBuilder OnBuild(Action<IComponentContext> action)
        {
            _onBuildActionsContainer.Add(action);
            return this;
        }
        
        public Task<IContainer> Build()
        {
            var container = ContainerBuilder.Build();
            
            return _onBuildActionsContainer.ActionsCount > 0 
                ? InvokeBuildActions(container) 
                : Task.FromResult(container);
        }
        
        private async Task<IContainer> InvokeBuildActions(IContainer container)
        {
            for (var i = 0; i < _onBuildActionsContainer.ActionsCount; i++)
            {
                if (_onBuildActionsContainer.TryGetSyncAction(i, out var syncAction))
                {
                    await using var scope = container.BeginLifetimeScope();
                    syncAction(scope);
                }
                else if (_onBuildActionsContainer.TryGetAsyncAction(i, out var asyncAction))
                {
                    await using var scope = container.BeginLifetimeScope();
                    await asyncAction(scope);
                }
            }
            
            return container;
        }
    }
}