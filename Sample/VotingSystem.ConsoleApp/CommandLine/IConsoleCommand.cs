using Autofac;

namespace VotingSystem.ConsoleApp.CommandLine
{
    internal interface IConsoleCommand
    {
        void Execute(IComponentContext services, string[] args);
        string GetHelp();
        string GetDefinition();
    }
}