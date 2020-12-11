using System;
using System.Linq;

namespace VotingSystem.ConsoleApp.CommandLine
{
    internal static class ExceptionFormatterExtension
    {
        public static void WriteToConsole(this Exception exception)
        {
            if (exception is AggregateException ae && ae.InnerException is not null)
            {
                exception = ae.InnerException;
            }
            
            Console.WriteLine("An error occurred while executing the command:");
            
            var exceptionType = exception.GetType().FullName;
            var exceptionMessage = !string.IsNullOrEmpty(exception.Message) 
                ? exception.Message 
                : "(no exception message)";
            
            var exceptionMessageLines = exceptionMessage
                .Split(Environment.NewLine)
                .Select(l => $" > {l}");
            
            Console.WriteLine($" > [{exceptionType}]");
            foreach (var line in exceptionMessageLines)
            {
                Console.WriteLine(line);
            }
        }
    }
}