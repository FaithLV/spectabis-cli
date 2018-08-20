using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ConsoleTables;
using static System.Console;

namespace spectabis_cmd.Models
{
    public static class HelpPrettyPrints
    {
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

        public static void PrintGameProfiles(List<GameProfile> profiles)
        {
            //Get all GameProfile public properties and set as table header
            PropertyInfo[] _props = typeof(GameProfile).GetProperties();
            string[] properties = _props.Select( x => x.Name.ToString()).ToArray(); //properties as strings
            ConsoleTable table = new ConsoleTable(properties);

            table.Write();
        }
    }
}