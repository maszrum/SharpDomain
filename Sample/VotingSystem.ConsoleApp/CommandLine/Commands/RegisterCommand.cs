using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Autofac;
using MediatR;
using VotingSystem.Application.Commands;
using VotingSystem.Application.ViewModels;

namespace VotingSystem.ConsoleApp.CommandLine.Commands
{
    internal class RegisterCommand : IConsoleCommand
    {
        private readonly ConsoleState _consoleState;

        public RegisterCommand(ConsoleState consoleState)
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
            
            return Register(services, pesel);
        }

        private static async Task Register(IComponentContext services, string pesel)
        {
            var mediator = services.Resolve<IMediator>();
            var createVoter = new CreateVoter(pesel);
            
            VoterViewModel createVoterResponse;
            try
            {
                createVoterResponse = await mediator.Send(createVoter);
            }
            catch (Exception exception)
            {
                exception.WriteToConsole();
                return;
            }

            Console.WriteLine();
            Console.WriteLine(createVoterResponse.ToString());
            Console.WriteLine();
            Console.WriteLine("Now you can login.");
        }

        public string GetDefinition() => "register [pesel]";
        
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