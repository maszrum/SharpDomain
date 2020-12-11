using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using MediatR;
using VotingSystem.Application.Queries;
using VotingSystem.Application.ViewModels;

namespace VotingSystem.ConsoleApp.CommandLine.Commands
{
    internal class GetResultCommand : IConsoleCommand
    {
        private readonly ConsoleState _consoleState;

        public GetResultCommand(ConsoleState consoleState)
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
            
            return GetResult(services);
        }

        private async Task GetResult(IComponentContext services)
        {
            var mediator = services.Resolve<IMediator>();
            
            var selectedQuestion = await CommandLineHelper.AskToSelectQuestion(mediator);
            if (selectedQuestion is null)
            {
                return;
            }
            
            var getQuestionResult = new GetQuestionResult(selectedQuestion.Id, _consoleState.VoterId);
            QuestionResultViewModel getQuestionResultResponse;
            try
            {
                getQuestionResultResponse = await mediator.Send(getQuestionResult);
            }
            catch (Exception exception)
            {
                exception.WriteToConsole();
                return;
            }

            Console.WriteLine();
            Console.WriteLine($"{selectedQuestion.QuestionText}");
            var index = 1;
            foreach (var answerResult in getQuestionResultResponse.AnswerResults)
            {
                var answerText = selectedQuestion.Answers
                    .Single(a => a.Id == answerResult.AnswerId)
                    .Text;

                Console.WriteLine($"[{index}]: {answerText} - {answerResult.Votes} votes");
                
                index++;
            }
            Console.WriteLine();
        }

        public string GetDefinition() => "get-result";
    }
}