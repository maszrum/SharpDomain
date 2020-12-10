using System;
using FluentValidation;
using VotingSystem.Application.Commands;

// ReSharper disable once UnusedType.Global

namespace VotingSystem.Application.Validators
{
    internal class VoteForValidator : AbstractValidator<VoteFor>
    {
        public VoteForValidator()
        {
            RuleFor(x => x.AnswerId).Must(NotBeEmpty);
            RuleFor(x => x.QuestionId).Must(NotBeEmpty);
            RuleFor(x => x.VoterId).Must(NotBeEmpty);
        }
        
        private static bool NotBeEmpty(Guid guid) => guid != Guid.Empty;
    }
}