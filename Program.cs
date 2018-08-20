using System;
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

        };

        static void Main(string[] args)
        {
            if(args.Length < 1)
            {
                Help();
            }

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
        static void Create(string[] args)
        {
            ProfileManager.CreateProfile(args);
        }

        static void Edit(string[] args)
        {

        }

        static void Delete(string[] args)
        {

        }
    }
}
