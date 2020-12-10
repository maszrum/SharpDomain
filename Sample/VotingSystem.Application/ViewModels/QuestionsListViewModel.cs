using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VotingSystem.Core.Models;

namespace VotingSystem.Application.ViewModels
{
    public class QuestionsListViewModel
    {
        public List<QuestionViewModel> Questions { get; } = new ();

        public override string ToString()
        {
            var questionsFormatted = Questions
                .Select(q => q.ToString())
                .Select((q, i) => $"  [{i}]: {q}");
            var questionsString = string.Join(Environment.NewLine, questionsFormatted)
                .Replace(Environment.NewLine, $"{Environment.NewLine}  ");
            
            return new StringBuilder()
                .AppendLine($"# {nameof(Question)}[]")
                .AppendLine(questionsString)
                .ToString();
        }
    }
}