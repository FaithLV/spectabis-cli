using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using spectabis_cmd.Model;

namespace spectabis_cmd.Domain
{
    public static class SmartParser
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

            PrettyPrinter.PrintGameProfile(profile);
        }

        public static void Configuration(string[] args)
        {
            if(args.Length < 1 || args[0] == null)
            {
                System.Console.WriteLine("  spectabis: must specify get/set action");
            }
            else if(args[0].ToLower() == "set")
            {
                ConfigurationManager.Set(args[1], args[2]);
                System.Console.WriteLine($"  spectabis: {args[1]} = {args[2]}");
            }
            else if(args[0].ToLower() == "get" )
            {
                if(args.Length < 2)
                {
                    System.Console.WriteLine("  spectabis: must specify (case-sensitive) property name");
                    return;
                }

                string value = ConfigurationManager.Get(args[1]);

                if(args[1] == "*")
                {
                    PrettyPrinter.PrintAllConfiguration();
                    return;
                }
                else if(args[1] == null || value == null)
                {
                    System.Console.WriteLine("  spectabis: must specify proper property name (case-sensitive)");
                    return;
                }
                
                System.Console.WriteLine("  {0,10} = {1, 10}", args[1], value);
            }
            else
            {
                System.Console.WriteLine($"  spectabis: no such action {args[0]}");
            }
        }

        private static void PrintConfiguration(string setting)
        {
            if(setting == null)
            {
                System.Console.WriteLine("  spectabis: must specify setting");
                return;
            }
        }
    }
}