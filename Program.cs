using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using McMaster.Extensions.CommandLineUtils;
using Newtonsoft.Json;
using spectabis_cli.Domain;
using spectabis_cli.Model;

namespace spectabis_cli
{
    class Program
    {
        public static bool IsInteractive { get; private set; }

        public static readonly Dictionary<string, ConsoleCommand> CommandTable = new Dictionary<string, ConsoleCommand>()
        { 
        { "launch", new ConsoleCommand(Launch, "Launch emulator") }, 
        { "version", new ConsoleCommand(Version, "Show program version") }, 
        { "help", new ConsoleCommand(Help, "Show help message") }, 
        { "create", new ConsoleCommand(Create, "Create a new game profile", "create < path / name > [optional]<name>") }, 
        { "delete", new ConsoleCommand(Delete, "Delete game profile", "delete < id / title >") }, 
        { "profiles", new ConsoleCommand(Profiles, "Show all game profiles or single by ID or exact name", "profiles [optional]<ID/Title>") },
        { "options", new ConsoleCommand(Options, "Access to spectabis settings", "configure [get/set] <Setting> <value>") }, 
        { "exit", new ConsoleCommand(Exit, "Exit spectabis interactive shell") }
        };

        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                PrettyPrinter.Print("Use `help` to list all available commands");
                IsInteractive = true;
                LaunchInteractive();
            }        
            else
            {
                IsInteractive = false;
                ExecuteCommandArgument(args);
            }
        }

        static void ExecuteCommandArgument(string[] args)
        {
            //Strip command argument from args
            string commandArg = args[0].ToLower();
            string[] arguments = args.Where(x => x != args[0]).ToArray();

            try
            {
                //Execute the argument and pass the remaining arguments
                ConsoleCommand command = CommandTable[commandArg];
                command.CommandDelegate(arguments);
            }
            catch (KeyNotFoundException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine();
                PrettyPrinter.Print($"Unknown command '{commandArg}'!");
                Console.ForegroundColor = ConsoleColor.White;
                Help();
            }
        }

        static void LaunchInteractive()
        {
            ReadLine.HistoryEnabled = true;
            ReadLine.AutoCompletionHandler = new AutoCompletionHandler();

            while (true)
            {
                string input = ReadLine.Read("spectabis > ");
                string[] args = input.ParseAsArguments().ToArray();

                if (!string.IsNullOrEmpty(input) && !string.IsNullOrWhiteSpace(input) && args.Count() != 0)
                {
                    Console.WriteLine(String.Empty);

                    string commandArg = args[0].ToLower();
                    string[] arguments = args.Where(x => x != args[0]).ToArray();

                    try
                    {
                        ConsoleCommand command = CommandTable[commandArg];
                        command.CommandDelegate(arguments);
                    }
                    catch (KeyNotFoundException)
                    {
                        PrettyPrinter.Print($"'{commandArg}' command not found");
                    }
                }
            }
        }

        //Return build version
        static void Version(string[] args)
        {
            System.Console.WriteLine($"Version: {AssemblyVersion.Get()}");
        }

        //Help command without extra arguments
        static void Help(string[] args = null)
        {
            PrettyPrinter.PrintCommandTable(CommandTable);
        }

        //Create game profile
        static void Create(string[] args)
        {
            SmartParser.CreateGameProfile(args);
        }

        static void Edit(string[] args)
        {

        }

        static void Profiles(string[] args = null)
        {
            if (args.Length < 1)
            {
                PrettyPrinter.PrintGameProfiles(ProfileManager.GetAllProfiles());
            }
            else
            {
                SmartParser.PrintProfile(args);
            }
        }

        static void Delete(string[] args)
        {
            SmartParser.DeleteGameProfile(args);
        }

        static void Launch(string[] args)
        {
            SmartParser.LaunchGame(args);
        }

        static void Options(string[] args)
        {
            SmartParser.Configuration(args);
        }

        static void Exit(string[] args = null)
        {
            Environment.Exit(-1);
        }
    }
}