using System;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using VotingSystem.Application.Queries;
using VotingSystem.Application.ViewModels;

namespace VotingSystem.ConsoleApp.CommandLine.Commands
{
    internal static class CommandLineHelper
    {
        public static async Task<QuestionViewModel?> AskToSelectQuestion(IMediator mediator)
        {
            var getQuestions = new GetQuestions();
            QuestionsListViewModel getQuestionsResponse;
            try
            {
                getQuestionsResponse = await mediator.Send(getQuestions);
            }
            catch (Exception exception)
            {
                exception.WriteToConsole();
                return default;
            }
            
            if (getQuestionsResponse.Questions.Count == 0)
            {
                Console.WriteLine("The question list is empty.");
                return default;
            }
            
            var questionsList = getQuestionsResponse.Questions
                .Select((q, i) => $"[{i+1}]: {q.QuestionText}")
                .ToArray();

            Console.WriteLine();
            Console.WriteLine(string.Join(Environment.NewLine, questionsList));
            
            Console.Write("Select question: ");
            if (!int.TryParse(Console.ReadLine()?.Trim(), out var selectedIndex) || 
                selectedIndex < 1 || selectedIndex > questionsList.Length)
            {
                Console.WriteLine("Invalid number.");
                return default;
            }
            
            return getQuestionsResponse.Questions[selectedIndex-1];
        }
        
        public static QuestionViewModel.AnswerViewModel? AskToSelectAnswer(QuestionViewModel question)
        {
            Console.WriteLine();
            Console.WriteLine(question.QuestionText);
            
            var answerTexts = question.Answers.Select((a, i) => $"[{i + 1}]: {a.Text}");
            Console.WriteLine(string.Join(Environment.NewLine, answerTexts));

            Console.WriteLine();
            
            Console.Write("Select answer: ");
            
            if (!int.TryParse(Console.ReadLine()?.Trim(), out var selectedIndex) ||
                selectedIndex < 1 || selectedIndex > question.Answers.Count)
            {
                Console.WriteLine("Invalid number.");
                return default;
            }
            
            return question.Answers[selectedIndex - 1];
        }
    }
}