using System.Collections.Generic;

namespace VotingSystem.Application.ViewModels
{
    public class QuestionsListViewModel
    {
        public QuestionsListViewModel(List<QuestionViewModel> questions)
        {
            Questions = questions;
        }

        public List<QuestionViewModel> Questions { get; }
    }
}