using System;
using MediatR;
using VotingSystem.Application.ViewModels;

// ReSharper disable once UnusedType.Global

namespace VotingSystem.Application.Queries
{
    internal class GetMyAnswersHandler : RequestHandler<GetMyAnswers, MyAnswersViewModel>
    {
        protected override MyAnswersViewModel Handle(GetMyAnswers request)
        {
            // TODO
            throw new NotImplementedException();
        }
    }
}