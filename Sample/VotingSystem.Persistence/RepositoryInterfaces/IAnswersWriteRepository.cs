using System.Threading.Tasks;
using VotingSystem.Persistence.Entities;

namespace VotingSystem.Persistence.RepositoryInterfaces
{
    public interface IAnswersWriteRepository
    {
        Task Update(AnswerResultEntity entity);
    }
}