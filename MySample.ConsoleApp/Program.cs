using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Autofac;
using MediatR;
using MySample.Application;
using MySample.Application.Commands;

namespace MySample.ConsoleApp
{
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var containerBuilder = new ContainerBuilder()
                .RegisterApplicationLayer();
            
            await using var container = containerBuilder.Build();
            
            var mediator = container.Resolve<IMediator>();
            
            var createModel = new CreateMyModel();
            var createResult = await mediator.Send(createModel);

            Console.WriteLine(createResult);
        }
    }
}