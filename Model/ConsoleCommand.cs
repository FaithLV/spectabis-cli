using System;

namespace spectabis_cli.Model
{
    public class ConsoleCommand
    {
        public ConsoleCommand(Action<string[]> commandDelegate, string description, string usage = null)
        {
            CommandDelegate = commandDelegate;
            Description = description;
            Usage = usage;
        }
        public Action<string[]> CommandDelegate {get; set;}
        public string Description {get; set;}
        public string Usage {get; set;}
    }
}