using System;
using System.Linq;

namespace spectabis_cli
{
    class AutoCompletionHandler : IAutoCompleteHandler
    {
        public char[] Separators { get; set; } = new char[] { ' ', '.', '/', '\\', ':' };
        public string[] GetSuggestions (string text, int index)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return Program.CommandTable.Keys.ToArray();
            }
            return null;
        }

    }

}