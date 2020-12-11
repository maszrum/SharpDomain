using System.Threading.Tasks;
using VotingSystem.Persistence.Entities;

namespace VotingSystem.Persistence.RepositoryInterfaces
{
    public interface IQuestionResultsWriteRepository
    {
        Task Create(QuestionResultEntity questionResult);
    }
}