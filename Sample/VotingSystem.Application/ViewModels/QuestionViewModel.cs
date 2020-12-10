using System;
using System.Collections.Generic;
using System.Text;
using VotingSystem.Core.Models;

namespace VotingSystem.Application.ViewModels
{
    public class QuestionViewModel
    {
        public QuestionViewModel(
            Guid id, 
            string questionText, 
            List<string> answers)
        {
            Id = id;
            QuestionText = questionText;
            Answers = answers;
        }

        public Guid Id { get; }

        public string QuestionText { get; }
        
        public List<string> Answers { get; }
        
        public override string ToString()
        {
            var sb = new StringBuilder()
                .AppendLine($"# {nameof(Question)}")
                .AppendLine($"{nameof(Id)}: {Id}")
                .AppendLine($"{nameof(QuestionText)}: {QuestionText}");

            var order = 0;
            foreach (var answer in Answers)
            {
                sb.AppendLine($"{nameof(Answers)}[{order}]: {answer}");
                order++;
            }
            
            return sb.ToString().TrimEnd();
        }
    }
}