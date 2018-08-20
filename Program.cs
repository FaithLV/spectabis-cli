﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using spectabis_cmd.Models;

namespace spectabis_cmd
{
    class Program
    {
        private static readonly Dictionary<string, ConsoleCommand> CommandTable = new Dictionary<string, ConsoleCommand>()
        {
            {"version", new ConsoleCommand(Version, "Show program version")},
            {"help", new ConsoleCommand(Help, "Show help message")},
            {"create", new ConsoleCommand(Create, "Create a new game profile", "dotnet create < path / name > <[optional] name>")},
            { "exit", new ConsoleCommand(Exit, "Exit spectabis command line tool")}

        };

        static void Main(string[] args)
        {
            if(args.Length < 1)
            {
                LaunchInteractive();
            }
            else
            {
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
            catch(KeyNotFoundException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine();
                System.Console.WriteLine($"		Unknown command '{commandArg}'!");
                Console.ForegroundColor = ConsoleColor.White;
                Help();
            }
        }

        static void LaunchInteractive()
        {
            while(true)
            {
                System.Console.ForegroundColor = ConsoleColor.Gray;
                System.Console.Write("spectabis > ");
                System.Console.ForegroundColor = ConsoleColor.White;

                string args = System.Console.ReadLine().ToLower();
                string commandArg = args;

                try
                {
                    ConsoleCommand command = CommandTable[commandArg];
                    command.CommandDelegate(null);
                }
                catch(KeyNotFoundException)
                {
                    System.Console.WriteLine($"spectabis: '{commandArg}' command not found");
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
            HelpPrettyPrints.PrintASCIILogo();
            HelpPrettyPrints.PrintCommandTable(CommandTable);
        }

        //Create game profile
        static void Create(string[] args = null)
        {
            if(args == null)
            {
                System.Console.WriteLine("spectabis: missing profile name or game path");
                return;
            }

            ProfileManager.CreateProfile(args);
        }

        static void Edit(string[] args)
        {

        }

        static void Profiles(string[] args)
        {

        }

        static void Delete(string[] args)
        {

        }

        static void Exit(string[] args = null)
        {
            Environment.Exit(-1);
        }
    }
}
