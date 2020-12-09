using System;
using System.Threading.Tasks;
using VotingSystem.Core.Models;

namespace VotingSystem.Core.InfrastructureAbstractions
{
    public interface IVotingResultsRepository
    {
        Task<AnswerResult?> GetAnswerResultByAnswerId(Guid answerId);
        Task<QuestionResult?> GetQuestionResultByQuestionId(Guid questionId);
    }
}