using System.Collections.Generic;
using System.Threading.Tasks;
using Autofac;

namespace VotingSystem.ConsoleApp.CommandLine
{
    internal interface IConsoleCommand
    {
        Task Execute(IComponentContext services, IReadOnlyList<string> args);
        string GetDefinition();
    }
}