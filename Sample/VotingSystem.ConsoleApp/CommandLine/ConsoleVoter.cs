using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using VotingSystem.ConsoleApp.CommandLine.Commands;

namespace VotingSystem.ConsoleApp.CommandLine
{
    internal class ConsoleVoter
    {
        private readonly IContainer _container;
        private readonly ConsoleState _state = new();
        private readonly Dictionary<string, IConsoleCommand> _commands = new ();
        
        public ConsoleVoter(IContainer container)
        {
            _container = container;
            SetupCommands();
        }
        
        private void SetupCommands()
        {
            static string GetCommandText(IConsoleCommand command) => 
                command.GetDefinition().Split(' ')[0];

            var commands = new IConsoleCommand[]
            {
                new RegisterCommand(_state),
                new LogInCommand(_state),
                new LogOutCommand(_state),
                new GetQuestionsCommand(_state),
                new AddQuestionCommand(_state),
                new VoteCommand(_state),
                new GetResultCommand(_state)
            };

            foreach (var command in commands)
            {
                var commandText = GetCommandText(command);
                _commands.Add(commandText, command);
            }
        }
        
        public void RunBlocking()
        {
            ShowWelcomeMessage();
            
            var command = default(string);

            do
            {
                Console.Write(GetReadLinePrefix());
                if (!TryReadLine(out var line))
                {
                    return;
                }
                
                var parts = line.Split(' ');
                if (parts.Length > 0)
                {
                    command = parts[0].ToLower();
                    var args = parts.Skip(1).ToArray();
                    
                    if (command == "help")
                    {
                        ShowHelp();
                    }
                    else if (_commands.TryGetValue(command, out var commandHandler))
                    {
                        using var scope = _container.BeginLifetimeScope();
                        commandHandler.Execute(scope, args).GetAwaiter().GetResult();
                    }
                    else if (!IsQuitCommand(command))
                    {
                        Console.WriteLine("Invalid command. Enter valid command or 'help' / 'q'");
                    }
                }
            }
            while (!IsQuitCommand(command));
        }

        private void ShowWelcomeMessage()
        {
            Console.WriteLine();
            Console.WriteLine("Welcome to voting system.");
            Console.WriteLine("Enter 'q' to quit program.");
            Console.WriteLine();
            
            ShowHelp();
            Console.WriteLine();
        }

        private void ShowHelp()
        {
            Console.WriteLine("Available commands:");
            
            foreach (var command in _commands.Values)
            {
                Console.WriteLine($"  {command.GetDefinition()}");
            }
        }

        private string GetReadLinePrefix()
        {
            return string.IsNullOrEmpty(_state.VoterPesel) 
                ? "<not-logged>: " 
                : $"<{_state.VoterPesel}>: ";
        }
        
        private static bool TryReadLine(out string line)
        {
            var tries = 0;
            
            do
            {
                line = Console.ReadLine()?.Trim() ?? string.Empty;
                tries++;
                
                if (tries > 5 && string.IsNullOrEmpty(line))
                {
                    return false;
                }
            }
            while (string.IsNullOrEmpty(line));
            
            return true;
        }
        
        private static bool IsQuitCommand(string? command) => command == "q";
    }
}