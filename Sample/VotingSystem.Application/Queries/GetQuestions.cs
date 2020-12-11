using MediatR;
using VotingSystem.Application.ViewModels;

namespace VotingSystem.Application.Queries
{
    public class GetQuestions : IRequest<QuestionsListViewModel>
    {
    }
}