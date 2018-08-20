using System.Collections.Generic;
using System.IO;
using System.Linq;
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

            profile.ProfileID = profile.GenerateHash();
            return profile;
        }

        public static void PrintProfile(string[] args)
        {
            string arg = args[0];
            List<GameProfile> profiles = ProfileManager.GetAllProfiles();

            bool isProfileID = profiles.Any(x => x.ProfileID.ToLower() == arg.ToLower());
            bool isTitle = profiles.Any(x => x.ProfileName.ToLower() == arg.ToLower());

            if(!isTitle && !isProfileID)
            {
                System.Console.WriteLine("  spectabis: profile not found by id or title");
                return;
            }

            GameProfile profile = null;

            if(isProfileID)
            {
                profile = profiles.SingleOrDefault(x => x.ProfileID.ToLower() == arg.ToLower());
            }
            else
            {
                profile = profiles.SingleOrDefault(x => x.ProfileName.ToLower() == arg.ToLower());
            }

            HelpPrettyPrints.PrintGameProfile(profile);
        }
    }
}