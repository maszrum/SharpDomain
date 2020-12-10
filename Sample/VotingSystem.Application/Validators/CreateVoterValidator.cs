using System.Linq;
using FluentValidation;
using VotingSystem.Application.Commands;

// ReSharper disable once UnusedType.Global

namespace VotingSystem.Application.Validators
{
    internal class CreateVoterValidator : AbstractValidator<CreateVoter>
    {
        public CreateVoterValidator()
        {
            RuleFor(x => x.Pesel)
                .NotNull()
                .Length(11)
                .Must(pesel => pesel.All(char.IsDigit));
        }
    }
}