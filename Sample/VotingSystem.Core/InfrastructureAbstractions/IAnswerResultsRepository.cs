using System;
using System.Threading.Tasks;
using VotingSystem.Core.Models;

namespace VotingSystem.Core.InfrastructureAbstractions
{
    public interface IAnswerResultsRepository
    {
        Task<AnswerResult?> GetAnswerResultByAnswerId(Guid answerId);
    }
}