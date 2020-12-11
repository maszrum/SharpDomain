using System;
using Autofac;

namespace VotingSystem.ConsoleApp.CommandLine.Commands
{
    internal class AddQuestionCommand : IConsoleCommand
    {
        private readonly ConsoleState _consoleState;

        public AddQuestionCommand(ConsoleState consoleState)
        {
            _consoleState = consoleState;
        }

        public void Execute(IComponentContext services, string[] args)
        {
            // TODO
            Console.WriteLine("add question");
        }

        public string GetHelp() => "Some help."; // TODO

        public string GetDefinition() => "add-question [text]"; // TODO
    }
}