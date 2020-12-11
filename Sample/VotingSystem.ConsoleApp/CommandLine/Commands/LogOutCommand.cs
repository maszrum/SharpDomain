using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        public Task Execute(IComponentContext services, IReadOnlyList<string> args)
        {
            if (string.IsNullOrEmpty(_consoleState.VoterPesel))
            {
                Console.WriteLine("You are not logged.");
                return Task.CompletedTask;
            }
            
            _consoleState.VoterPesel = default;
            _consoleState.VoterId = Guid.Empty;
            
            return Task.CompletedTask;
        }

        public string GetDefinition() => "logout";
    }
}