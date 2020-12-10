using System;
using Autofac;

namespace VotingSystem.ConsoleApp.ConsoleCommands
{
    internal class LogInCommand : IConsoleCommand
    {
        private readonly ConsoleState _consoleState;

        public LogInCommand(ConsoleState consoleState)
        {
            _consoleState = consoleState;
        }

        public void Execute(IComponentContext services, string[] args)
        {
            if (!string.IsNullOrEmpty(_consoleState.VoterPesel))
            {
                Console.WriteLine("Logout before using this command.");
                return;
            }
            
            _consoleState.VoterPesel = args[0];
            _consoleState.VoterId = Guid.NewGuid();
        }

        public string GetHelp() => "Some help."; // TODO

        public string GetDefinition() => "login [pesel]";
    }
}