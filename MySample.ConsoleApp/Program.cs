using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Autofac;
using MediatR;
using MySample.Application;
using MySample.Application.Commands;
using MySample.Application.Queries;
using MySample.Persistence.InMemory;

namespace MySample.ConsoleApp
{
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var containerBuilder = new ContainerBuilder()
                .RegisterApplicationLayer()
                .RegisterInMemoryRepositories();
            
            await using var container = containerBuilder.Build();
            
            var mediator = container.Resolve<IMediator>();
            
            var createModel = new CreateMyModel();
            var createResult = await mediator.Send(createModel);

            var increment = new IncrementMyModelValue()
            {
                Id = createResult.Id
            };
            
            for (var i = 1; i <= 10; i++)
            {
                _ = await mediator.Send(increment);
            }
            
            var getModel = new GetMyModel()
            {
                Id = createResult.Id
            };
            var viewModel = await mediator.Send(getModel);
            
            Console.WriteLine(viewModel);
        }
    }
}