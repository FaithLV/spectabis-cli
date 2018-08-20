using System;
using static System.Console;

namespace spectabis_cmd.Models
{
    public static class HelpPrettyPrints
    {
        public static void PrintASCIILogo()
        {
            string art = 
            @"
                ╔═╗╔═╗╔═╗╔═╗╔╦╗╔═╗╔╗ ╦╔═╗
                ╚═╗╠═╝║╣ ║   ║ ╠═╣╠╩╗║╚═╗
                ╚═╝╩  ╚═╝╚═╝ ╩ ╩ ╩╚═╝╩╚═╝
            ";

            Console.WriteLine(art);

        }

        public static void PrintCommand(string command, string description)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("{0, 12}", command);

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("{0, 12}", "      |       ");

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("{0, 12}", description);

        }
    }
}