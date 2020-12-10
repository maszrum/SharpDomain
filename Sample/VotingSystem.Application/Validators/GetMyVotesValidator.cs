using System;
using FluentValidation;
using VotingSystem.Application.Queries;

// ReSharper disable once UnusedType.Global

namespace VotingSystem.Application.Validators
{
    internal class GetMyVotesValidator : AbstractValidator<GetMyVotes>
    {
        public GetMyVotesValidator()
        {
            RuleFor(x => x.VoterId).Must(NotBeEmpty);
        }
        
        private static bool NotBeEmpty(Guid id) => id != Guid.Empty;
    }
}