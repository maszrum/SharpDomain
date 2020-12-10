using System;
using System.Collections.Generic;
using System.Text;
using VotingSystem.Core.Models;

namespace VotingSystem.Application.ViewModels
{
    public class QuestionResultViewModel
    {
        public QuestionResultViewModel(Guid questionId)
        {
            QuestionId = questionId;
        }

        public Guid QuestionId { get; }
        public List<AnswerResultViewModel> AnswerResults { get; } = new();

        public override string ToString() =>
            new StringBuilder()
                .AppendLine($"# {nameof(QuestionResult)}")
                .AppendLine($"{nameof(QuestionId)}: {QuestionId}")
                .Append(string.Join(Environment.NewLine, AnswerResults))
                .ToString();

        public class AnswerResultViewModel
        {
            public AnswerResultViewModel(
                Guid answerId, 
                int votes)
            {
                AnswerId = answerId;
                Votes = votes;
            }
            
            public Guid AnswerId { get; }
            public int Votes { get; }

            public override string ToString() =>
                new StringBuilder()
                    .AppendLine($"  {nameof(AnswerId)}: {AnswerId}")
                    .Append($"  {nameof(Votes)}: {Votes}")
                    .ToString();
        }
    }
}