using Autofac;

namespace VotingSystem.ConsoleApp
{
    internal interface IConsoleCommand
    {
        void Execute(IComponentContext services, string[] args);
        string GetHelp();
        string GetDefinition();
    }
}