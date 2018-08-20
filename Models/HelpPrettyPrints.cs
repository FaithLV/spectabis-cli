using System;
using System.Collections.Generic;
using System.ComponentModel;
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

            //Create arrays for Console Table and populate it
            for(int i = 0; i < profiles.Count; i++)
            {
                //object[] tableBuffer = new object[properties.Length];
                List<object> tableBuffer = new List<object>();

                foreach(PropertyDescriptor prop in TypeDescriptor.GetProperties(profiles[i]))
                {
                    Type t = profiles[0].GetType();
                    PropertyInfo p = t.GetProperty(prop.Name);
                    string value = (string)p.GetValue(profiles[i], null);

                    /* System.Console.WriteLine($"{prop.Name} | {profiles[0].ProfileName} {profiles[0].GamePath} {profiles[0].GameSerial}");
                    object obj = prop.GetType().GetProperty(prop.Name).GetValue(profiles[0].GetType(), null); */

                    tableBuffer.Add(value);
                }

                table.AddRow(tableBuffer.ToArray());
            }

            table.Write();
        }
    }
}