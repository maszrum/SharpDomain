using MediatR;
using VotingSystem.Core.Models;

namespace VotingSystem.Core.Events
{
    public class AnswerResultIncremented : INotification
    {
        public AnswerResultIncremented(AnswerResult answerResult)
        {
            AnswerResult = answerResult;
        }

        public AnswerResult AnswerResult { get; }
    }
}