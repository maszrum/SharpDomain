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
        private readonly Dictionary<string, IConsoleCommand> _commands = new Dictionary<string, IConsoleCommand>();
        
        public ConsoleVoter(IContainer container)
        {
            _container = container;
            SetupCommands();
        }
        
        private void SetupCommands()
        {
            _commands.Add("login", new LogInCommand(_state));
            _commands.Add("add-question", new AddQuestionCommand(_state));
            _commands.Add("logout", new LogOutCommand(_state));
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
                        ShowHelp(args);
                    }
                    else if (_commands.TryGetValue(command, out var commandHandler))
                    {
                        using var scope = _container.BeginLifetimeScope();
                        commandHandler.Execute(scope, args);
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
            Console.WriteLine();
            
            ShowHelp(Array.Empty<string>());
            Console.WriteLine();
        }

        private void ShowHelp(IReadOnlyList<string> args)
        {
            Console.WriteLine("# Available commands:");
            
            if (args.Count > 0 && _commands.TryGetValue(args[0], out var commandHandler))
            {
                Console.WriteLine(commandHandler.GetHelp());
            }
            else
            {
                foreach (var command in _commands.Values)
                {
                    Console.WriteLine(command.GetDefinition());
                }

                Console.WriteLine("Enter help [command] to show command help.");
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