using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Autofac;
using MediatR;
using VotingSystem.Application.Queries;
using VotingSystem.Application.ViewModels;

namespace VotingSystem.ConsoleApp.CommandLine.Commands
{
    internal class LogInCommand : IConsoleCommand
    {
        private readonly ConsoleState _consoleState;

        public LogInCommand(ConsoleState consoleState)
        {
            _consoleState = consoleState;
        }

        public Task Execute(IComponentContext services, IReadOnlyList<string> args)
        {
            if (!string.IsNullOrEmpty(_consoleState.VoterPesel))
            {
                Console.WriteLine("Log out before using this command.");
                return Task.CompletedTask;
            }
            
            if (!TryParseArgs(args, out string pesel))
            {
                Console.WriteLine($"Invalid args, use: {GetDefinition()}");
                return Task.CompletedTask;
            }
            
            return LogIn(services, pesel);
        }
        
        private async Task LogIn(IComponentContext services, string pesel)
        {
            var logIn = new LogIn(pesel);
            var mediator = services.Resolve<IMediator>();
            
            VoterViewModel logInResult;
            try
            {
                logInResult = await mediator.Send(logIn);
            }
            catch (Exception exception)
            {
                exception.WriteToConsole();
                return;
            }
            
            _consoleState.VoterId = logInResult.Id;
            _consoleState.VoterPesel = logInResult.Pesel;
        }

        public string GetDefinition() => "login [pesel]";
        
        private static bool TryParseArgs(IReadOnlyList<string> args, out string pesel)
        {
            pesel = string.Empty;
            
            if (args.Count != 1)
            {
                return false;
            }
            
            pesel = args[0];
            return true;
        }
    }
}