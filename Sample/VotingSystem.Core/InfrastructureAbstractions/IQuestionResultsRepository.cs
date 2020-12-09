using System;
using System.Threading.Tasks;
using VotingSystem.Core.Models;

namespace VotingSystem.Core.InfrastructureAbstractions
{
    public interface IQuestionResultsRepository
    {
        Task<QuestionResult?> GetQuestionResultByQuestionId(Guid questionId);
    }
}