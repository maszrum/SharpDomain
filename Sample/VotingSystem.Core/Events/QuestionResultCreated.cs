using MediatR;
using VotingSystem.Core.Models;

namespace VotingSystem.Core.Events
{
    public class QuestionResultCreated : INotification
    {
        public QuestionResultCreated(QuestionResult questionResult)
        {
            QuestionResult = questionResult;
        }

        public QuestionResult QuestionResult { get; }
    }
}