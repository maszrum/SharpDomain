using System;
using System.Collections.Generic;

namespace VotingSystem.Application.ViewModels
{
    public class QuestionResultViewModel
    {
        public QuestionResultViewModel(Guid questionId)
        {
            QuestionId = questionId;
        }

        public Guid QuestionId { get; }
        public List<AnswerResultViewModel> AnswerResults { get; } = new();
        
        public class AnswerResultViewModel
        {
            public AnswerResultViewModel(
                Guid answerId, 
                int votes)
            {
                AnswerId = answerId;
                Votes = votes;
            }
            
            public Guid AnswerId { get; }
            public int Votes { get; }
        }
    }
}