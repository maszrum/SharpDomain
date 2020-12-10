﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Autofac;
using MediatR;
using SharpDomain.Application;
using SharpDomain.Core;
using SharpDomain.AutoTransaction;
using SharpDomain.Persistence;
using VotingSystem.Application.Commands;
using VotingSystem.Application.Queries;
using VotingSystem.Core.Models;
using VotingSystem.Persistence.Entities;
using VotingSystem.Persistence.InMemory;

// ReSharper disable once ClassNeverInstantiated.Global

namespace VotingSystem.ConsoleApp
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var domainAssembly = typeof(Question).Assembly;
            var applicationAssembly = typeof(CreateQuestion).Assembly;
            var persistenceAssembly = typeof(QuestionEntity).Assembly;
            var inMemoryPersistenceAssembly = typeof(Persistence.InMemory.AutofacExtensions).Assembly;
            
            var containerBuilder = new ContainerBuilder()
                .RegisterDomainLayer(domainAssembly)
                .RegisterApplicationLayer(
                    assembly: applicationAssembly, 
                    configurationAction: config =>
                    {
                        config.ForbidMediatorInHandlers = true;
                        config.ForbidWriteRepositoriesInHandlersExceptIn(persistenceAssembly);
                    })
                .RegisterPersistenceLayer(persistenceAssembly)
                .RegisterInMemoryPersistence()
                .RegisterAutoTransaction(inMemoryPersistenceAssembly);
            
            await using var container = containerBuilder.Build();

            await using var scope = container.BeginLifetimeScope();
                
            await Run(scope);
        }
        
        private static async Task Run(IComponentContext context)
        {
            var mediator = context.Resolve<IMediator>();
            
            var createVoter = new CreateVoter("94040236185");
            var createVoterResponse = await mediator.Send(createVoter);

            var createQuestion = new CreateQuestion(
                questionText: "Sample question?", 
                answers: new List<string>
                {
                    "First answer", 
                    "Second answer", 
                    "Third answer"
                });
            var createQuestionResponse = await mediator.Send(createQuestion);
            
            var voteFor = new VoteFor(
                voterId: createVoterResponse.Id, 
                questionId: createQuestionResponse.Id,
                answerId: createQuestionResponse.Answers[1].Id);
            await mediator.Send(voteFor);
            
            var getQuestionResult = new GetQuestionResult(
                questionId: createQuestionResponse.Id, 
                voterId: createVoterResponse.Id);
            var getQuestionResultResponse = await mediator.Send(getQuestionResult);
            
            var getQuestions = new GetQuestions();
            var getQuestionsResponse = await mediator.Send(getQuestions);
            
            Console.WriteLine(getQuestionsResponse);
        }
    }
}