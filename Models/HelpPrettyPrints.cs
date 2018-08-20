using System;
using System.Collections.Generic;
using ConsoleTables;
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

        public static void PrintCommandTable(Dictionary<string, ConsoleCommand> cmdTable)
        {
            ConsoleTable table = new ConsoleTable("Command", "Description", "Usage");

            foreach(KeyValuePair<string, ConsoleCommand> cmd in cmdTable)
            {
                ConsoleCommand command = cmd.Value;
                table.AddRow(cmd.Key, command.Description, command.Usage);
            }

            table.Write();
        }
    }
}