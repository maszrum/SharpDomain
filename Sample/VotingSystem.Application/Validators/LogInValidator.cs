using System.Linq;
using FluentValidation;
using VotingSystem.Application.Queries;

// ReSharper disable once UnusedType.Global

namespace VotingSystem.Application.Validators
{
    internal class LogInValidator : AbstractValidator<LogIn>
    {
        public LogInValidator()
        {
            RuleFor(x => x.Pesel)
                .NotNull()
                .Length(11)
                .Must(pesel => pesel.All(char.IsDigit));
        }
    }
}