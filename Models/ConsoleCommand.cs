using System;

namespace spectabis_cmd.Models
{
    public class ConsoleCommand
    {
        public ConsoleCommand(Action<string[]> commandDelegate, string description)
        {
            CommandDelegate = commandDelegate;
            Description = description;
        }
        public Action<string[]> CommandDelegate {get; set;}
        public string Description {get; set;}
    }
}