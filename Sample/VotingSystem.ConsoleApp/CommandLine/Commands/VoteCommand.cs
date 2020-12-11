using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using MediatR;
using VotingSystem.Application.Commands;

namespace VotingSystem.ConsoleApp.CommandLine.Commands
{
    internal class VoteCommand : IConsoleCommand
    {
        private readonly ConsoleState _consoleState;

        public VoteCommand(ConsoleState consoleState)
        {
            _consoleState = consoleState;
        }

        public Task Execute(IComponentContext services, IReadOnlyList<string> args)
        {
            if (string.IsNullOrEmpty(_consoleState.VoterPesel))
            {
                Console.WriteLine("Log in before using this command.");
                return Task.CompletedTask;
            }
            
            return DoVote(services);
        }

        private async Task DoVote(IComponentContext services)
        {
            var mediator = services.Resolve<IMediator>();
            
            var selectedQuestion = await CommandLineHelper.AskToSelectQuestion(mediator);
            if (selectedQuestion is null)
            {
                return;
            }
            
            var selectedAnswer = CommandLineHelper.AskToSelectAnswer(selectedQuestion);
            if (selectedAnswer is null)
            {
                return;
            }
            
            var voteFor = new VoteFor(
                _consoleState.VoterId, 
                selectedQuestion.Id, 
                selectedAnswer.Id);
            
            try
            {
                await mediator.Send(voteFor);
            }
            catch (Exception exception)
            {
                exception.WriteToConsole();
                return;
            }

            Console.WriteLine($"Voted for: {selectedAnswer.Text}");
        }

        public string GetDefinition() => "vote";
    }
}