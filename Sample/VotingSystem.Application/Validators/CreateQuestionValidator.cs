using FluentValidation;
using VotingSystem.Application.Commands;

// ReSharper disable once UnusedType.Global

namespace VotingSystem.Application.Validators
{
    internal class CreateQuestionValidator : AbstractValidator<CreateQuestion>
    {
        public CreateQuestionValidator()
        {
            RuleFor(x => x.QuestionText).NotEmpty();
            
            RuleFor(x => x.Answers).NotNull();
            RuleFor(x => x.Answers.Count).GreaterThan(1);
            RuleForEach(x => x.Answers).NotEmpty();
        }
    }
}