using System.Threading.Tasks;
using VotingSystem.Persistence.Entities;

namespace VotingSystem.Persistence.RepositoryInterfaces
{
    public interface IAnswerResultsWriteRepository
    {
        Task Update(AnswerResultEntity entity);
    }
}