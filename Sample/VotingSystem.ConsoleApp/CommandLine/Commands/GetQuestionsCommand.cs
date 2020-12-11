using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Autofac;
using MediatR;
using VotingSystem.Application.Queries;
using VotingSystem.Application.ViewModels;

namespace VotingSystem.ConsoleApp.CommandLine.Commands
{
    internal class GetQuestionsCommand : IConsoleCommand
    {
        private readonly ConsoleState _consoleState;

        public GetQuestionsCommand(ConsoleState consoleState)
        {
            _consoleState = consoleState;
        }

        public async Task Execute(IComponentContext services, IReadOnlyList<string> args)
        {
            var mediator = services.Resolve<IMediator>();
            var getQuestions = new GetQuestions();
            
            QuestionsListViewModel getQuestionsResponse;
            try
            {
                getQuestionsResponse = await mediator.Send(getQuestions);
            }
            catch (Exception exception)
            {
                exception.WriteToConsole();
                return;
            }

            Console.WriteLine();
            Console.WriteLine(getQuestionsResponse.ToString());
            Console.WriteLine();
        }

        public string GetDefinition() => "get-questions";
    }
}