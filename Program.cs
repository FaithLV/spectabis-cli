using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using McMaster.Extensions.CommandLineUtils;
using Newtonsoft.Json;
using spectabis_cmd.Model;
using spectabis_cmd.Domain;

namespace spectabis_cmd
{
    class Program
    {
        public static bool IsInteractive { get; private set; }

        private static readonly Dictionary<string, ConsoleCommand> CommandTable = new Dictionary<string, ConsoleCommand>()
        {
            {"version", new ConsoleCommand(Version, "Show program version")},
            {"help", new ConsoleCommand(Help, "Show help message")},
            {"create", new ConsoleCommand(Create, "Create a new game profile", "create < path / name > [optional]<name>")},
            {"profiles", new ConsoleCommand(Profiles, "Show all game profiles or single by ID or exact name", "profiles [optional]<ID/Title>")},
            {"config", new ConsoleCommand(Configure, "Set or Get Spectabis global configuration settings", "configure [get/set] <Setting> <value>")},
            { "exit", new ConsoleCommand(Exit, "Exit spectabis interactive shell")}
        };

        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
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
            while (true)
            {
                string input = ReadLine.Read("spectabis > ");
                string[] args = input.ParseAsArguments().ToArray();

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
        static void Create(string[] args = null)
        {
            if (args == null)
            {
                if (IsInteractive)
                {
                    args = new string[2];

                    string arg1 = Prompt.GetString("Enter game file path or title: ");
                    args[0] = arg1;
                }
                else
                {
                    PrettyPrinter.Print("missing profile name or game path");
                    return;
                }
            }

            ProfileManager.CreateProfile(args);
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

        }

        static void Launch(string[] args)
        {
            
        }

        static void Configure(string[] args)
        {
            SmartParser.Configuration(args);
        }

        static void Exit(string[] args = null)
        {
            Environment.Exit(-1);
        }
    }
}
