using System;
using FluentValidation;
using VotingSystem.Application.Queries;

// ReSharper disable once UnusedType.Global

namespace VotingSystem.Application.Validators
{
    internal class GetQuestionResultValidator : AbstractValidator<GetQuestionResult>
    {
        public GetQuestionResultValidator()
        {
            RuleFor(x => x.QuestionId).Must(NotBeEmpty);
            RuleFor(x => x.VoterId).Must(NotBeEmpty);
        }
        
        private static bool NotBeEmpty(Guid id) => id != Guid.Empty; 
    }
}