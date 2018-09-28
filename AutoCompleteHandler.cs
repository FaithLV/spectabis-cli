using System;
using System.Linq;
using spectabis_cli.Domain;

namespace spectabis_cli
{
    class AutoCompletionHandler : IAutoCompleteHandler
    {
        public char[] Separators { get; set; } = new char[] { ' ', '.', '/', '\\', ':' };
        public string[] GetSuggestions (string text, int index)
        {
            //Base command hints
            if (string.IsNullOrWhiteSpace(text))
            {
                return Program.CommandTable.Keys.ToArray();
            }

            //Profile hints
            if(text.StartsWith("profiles "))
            {
                string query = text.Replace("profiles ", String.Empty).ToLower();
                var profiles = ProfileManager.GetAllProfiles().Where(x => x.ProfileID.ToLower().StartsWith(query));
                return profiles.Select(x => x.ProfileID).ToArray();
            }

            return null;
        }

    }

}