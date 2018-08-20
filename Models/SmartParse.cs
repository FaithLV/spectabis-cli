using System.Collections.Generic;
using System.IO;
using System.Text;

namespace spectabis_cmd.Models
{
    public static class SmartParse
    {
        public static IEnumerable<string> ParseAsArguments(this string commandLine)
        {
            // https://stackoverflow.com/a/7774211/6651569
            
            if (string.IsNullOrWhiteSpace(commandLine))
                yield break;
            var sb = new StringBuilder();
            bool inQuote = false;
            foreach (char c in commandLine)
            {
                if (c == '"' && !inQuote)
                {
                    inQuote = true;
                    continue;
                }
                if (c != '"' && !(char.IsWhiteSpace(c) && !inQuote))
                {
                    sb.Append(c);
                    continue;
                }
                if (sb.Length > 0)
                {
                    var result = sb.ToString();
                    sb.Clear();
                    inQuote = false;
                    yield return result;
                }
            }
            if (sb.Length > 0)
                yield return sb.ToString();
        }

        public static GameProfile CreateGameProfile(string[] args)
        {
            bool isFilePath = File.Exists(args[0]);
            GameProfile profile = null;

            if (isFilePath)
            {
                profile = new GameProfile() { GamePath = args[0] };

                if (args.Length > 1)
                {
                    profile.ProfileName = args[1];
                }
            }
            else
            {
                profile = new GameProfile() { ProfileName = args[0] };
            }

            return profile;
        }
    }
}