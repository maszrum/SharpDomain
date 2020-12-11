using System.Collections.Generic;
using System.Threading.Tasks;
using VotingSystem.Core.Models;

namespace VotingSystem.Core.InfrastructureAbstractions
{
    public interface IQuestionsRepository
    {
        Task<IReadOnlyList<Question>> GetAll();
    }
}