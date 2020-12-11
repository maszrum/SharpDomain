using System;
using Autofac;

namespace VotingSystem.ConsoleApp.CommandLine.Commands
{
    internal class LogOutCommand : IConsoleCommand
    {
        private readonly ConsoleState _consoleState;

        public LogOutCommand(ConsoleState consoleState)
        {
            _consoleState = consoleState;
        }

        public void Execute(IComponentContext services, string[] args)
        {
            if (string.IsNullOrEmpty(_consoleState.VoterPesel))
            {
                Console.WriteLine("You are not logged.");
                return;
            }
            
            _consoleState.VoterPesel = default;
            _consoleState.VoterId = Guid.Empty;
        }

        public string GetHelp() => "Some help."; // TODO

        public string GetDefinition() => "logout";
    }
}