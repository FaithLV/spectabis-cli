using System;
using System.Linq;
using spectabis_cli.Domain;

namespace spectabis_cli
{
    class AutoCompletionHandler : IAutoCompleteHandler
    {
        private string[] CommandKeys = Program.CommandTable.Keys.ToArray();
        public char[] Separators { get; set; } = new char[] { ' ', '.', '/', '\\', ':' };
        public string[] GetSuggestions (string text, int index)
        {
            //Base command hints
            if (string.IsNullOrWhiteSpace(text))
            {
                return Program.CommandTable.Keys.ToArray();
            }

            string[] arguments = SmartParser.ParseAsArguments(text).ToArray();
            string lastArg = arguments.Last();
            bool newArgument = text.Last() == ' ';

            if(arguments.Count() == 1 && !newArgument && CommandKeys.Any(x => x.StartsWith(lastArg)))
            {
                return QueryCommands(lastArg);
            }

            //Profile hints
            if(text.StartsWith("profiles "))
            {
                string query = text.Replace("profiles ", String.Empty).ToLower();
                return QueryProfiles(query);
            }



            return null;
        }
        private string[] QueryProfiles(string query)
        {
            var profiles = ProfileManager.GetAllProfiles().Where(x => x.ProfileID.ToLower().StartsWith(query));
            return profiles.Select(x => x.ProfileID).ToArray();
        }

        private string[] QueryCommands(string query)
        {
            var commands = CommandKeys.Where(x => x.StartsWith(query)).ToArray();
            return commands;
        }

    }

}