using System;
using System.Threading.Tasks;
using Autofac;
using MediatR;
using SharpDomain.Application;
using SharpDomain.Core;
using SharpDomain.AutoTransaction;
using SharpDomain.Persistence;
using VotingSystem.Application.Commands;
using VotingSystem.Application.Queries;
using VotingSystem.Persistence.Entities;
using VotingSystem.Persistence.InMemory;

// ReSharper disable once ClassNeverInstantiated.Global

namespace VotingSystem.ConsoleApp
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var applicationAssembly = typeof(CreateMyModel).Assembly;
            var persistenceAssembly = typeof(MyModelEntity).Assembly;
            var inMemoryPersistenceAssembly = typeof(Persistence.InMemory.AutofacExtensions).Assembly;
            
            var containerBuilder = new ContainerBuilder()
                .RegisterDomainLayer()
                .RegisterApplicationLayer(
                    assembly: applicationAssembly, 
                    configurationAction: config =>
                    {
                        config.ForbidMediatorInHandlers = true;
                        config.ForbidWriteRepositoriesInHandlersExceptIn(persistenceAssembly);
                    })
                .RegisterAutoTransaction(inMemoryPersistenceAssembly)
                .RegisterPersistenceLayer(persistenceAssembly)
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

            var createModel = new CreateMyModel(
                intProperty: 1, 
                stringProperty: "sample string");
            
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