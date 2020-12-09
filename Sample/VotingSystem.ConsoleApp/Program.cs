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
            var applicationAssembly = typeof(CreateQuestion).Assembly;
            var persistenceAssembly = typeof(QuestionEntity).Assembly;
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

            await using var scope = container.BeginLifetimeScope();
                
            await Run(scope);
        }
        
        private static Task Run(IComponentContext context)
        {
            var mediator = context.Resolve<IMediator>();
            
            throw new NotImplementedException();
        }
    }
}