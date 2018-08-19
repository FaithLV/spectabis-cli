using System;
using System.Collections.Generic;
using System.Linq;
using spectabis_cmd.Models;

namespace spectabis_cmd
{
    class Program
    {
        private static readonly Dictionary<string, Action<string[]>> ArgumentTable = new Dictionary<string, Action<string[]>>
        {
            {"-version" , Version}
        }; 

        static void Main(string[] args)
        {
            //Strip command argument from args
            string[] arguments = args.Where(x => x != args[0]).ToArray();

            //Execute the argument
            ArgumentTable[args[0]](arguments);
        }

        static void Version(string[] args)
        {
            System.Console.WriteLine($"Version: {AssemblyVersion.Get()}");
        }

        static void Create(string[] args)
        {
            
        }


    }
}
