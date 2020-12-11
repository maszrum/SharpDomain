using MediatR;
using VotingSystem.Core.Models;

namespace VotingSystem.Core.Events
{
    public class QuestionCreated : INotification
    {
        public QuestionCreated(Question question)
        {
            Question = question;
        }

        public Question Question { get; }
    }
}