using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using MediatR;
using MySample.Application;
using MySample.Application.Commands;
using MySample.Application.Queries;
using MySample.Core;
using MySample.Persistence;
using MySample.Persistence.Entities;
using MySample.Persistence.InMemory;

namespace MySample.ConsoleApp
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
                .RegisterPersistenceLayer()
                .RegisterInMemoryPersistence();
            
            await using var container = containerBuilder.Build();
            
            await using (var scope = container.BeginLifetimeScope())
            {
                await DoSomething(scope);
            }
            
            await using (var scope = container.BeginLifetimeScope())
            {
                await DoSomething(scope);
            }
        }
        
        private static async Task DoSomething(IComponentContext context)
        {
            var mediator = context.Resolve<IMediator>();
            
            var createModel = new CreateMyModel()
            {
                IntProperty = 1,
                StringProperty = "sample string"
            };
            var createResult = await mediator.Send(createModel);
            
            var increment = new IncrementMyModelValue(createResult.Id);
            
            for (var i = 1; i <= 3; i++)
            {
                var incrementResult = await mediator.Send(increment);
                Console.WriteLine(incrementResult);
            }
            
            var getModel = new GetMyModel(createResult.Id);
            var viewModel = await mediator.Send(getModel);
            
            Console.WriteLine(viewModel);
        }
    }
}