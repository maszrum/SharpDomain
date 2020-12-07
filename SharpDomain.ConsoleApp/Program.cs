using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using MediatR;
using SharpDomain.Application;
using SharpDomain.Core;
using SharpDomain.Persistence;
using SharpDomain.Persistence.InMemory;
using SharpDomain.Application.Commands;
using SharpDomain.Application.Queries;
using SharpDomain.Persistence.Entities;
using SharpDomain.Transactions;

namespace SharpDomain.ConsoleApp
{
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var containerBuilder = new ContainerBuilder()
                .RegisterDomainLayer()
                .RegisterApplicationLayer(config =>
                {
                    config.ForbidMediatorInHandlers = true;
                    
                    var persistenceAssembly = typeof(MyModelEntity).GetTypeInfo().Assembly;
                    config.PermitWriteRepositoriesInHandlersOnlyIn(persistenceAssembly);
                })
                .RegisterAutoTransaction()
                .RegisterPersistenceLayer()
                .RegisterInMemoryPersistence();
            
            await using var container = containerBuilder.Build();

            Guid id;
            await using (var scope = container.BeginLifetimeScope())
            {
                id = await AddModel(scope);
            }
            
            await using (var scope = container.BeginLifetimeScope())
            {
                await IncrementModel(scope, id);
            }
            
            await using (var scope = container.BeginLifetimeScope())
            {
                await ReadModel(scope, id);
            }
        }
        
        private static async Task<Guid> AddModel(IComponentContext context)
        {
            var mediator = context.Resolve<IMediator>();
            
            var createModel = new CreateMyModel()
            {
                IntProperty = 1,
                StringProperty = "sample string"
            };
            var createResult = await mediator.Send(createModel);
            
            return createResult.Id;
        }
        
        private static async Task IncrementModel(IComponentContext context, Guid id)
        {
            var mediator = context.Resolve<IMediator>();
            
            var increment = new IncrementMyModelValue(id);
            
            for (var i = 1; i <= 3; i++)
            {
                await mediator.Send(increment);
            }
        }
        
        private static async Task ReadModel(IComponentContext context, Guid id)
        {
            var mediator = context.Resolve<IMediator>();
            
            var getModel = new GetMyModel(id);
            var viewModel = await mediator.Send(getModel);

            Console.WriteLine(viewModel);
        }
    }
}