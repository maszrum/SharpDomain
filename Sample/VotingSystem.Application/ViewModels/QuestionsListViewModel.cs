using System.Collections.Generic;

namespace VotingSystem.Application.ViewModels
{
    public class QuestionsListViewModel
    {
        public List<QuestionViewModel> Questions { get; } = new ();
    }
}