using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using spectabis_cli.Model;

namespace spectabis_cli.Domain
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

                if (sb.Length <= 0) continue;
                var result = sb.ToString();
                sb.Clear();
                inQuote = false;
                yield return result;
            }
            if (sb.Length > 0)
                yield return sb.ToString();
        }

        public static void CreateGameProfile(string[] args)
        {
            if(args.Count() < 1)
            {
                PrettyPrinter.Print("enter valid game profile title or file path");
                return;
            }

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

            ProfileManager.CreateProfile(profile);
        }

        public static void DeleteGameProfile(string[] args)
        {
            string arg = args[0];
            GameProfile profile = ProfileManager.FindProfile(arg);

            if(profile == null)
            {
                PrettyPrinter.Print("profile not found by id or title");
                return;
            }

            PrettyPrinter.Print($"Deleted profile ({profile.ProfileID} | {profile.ProfileName})");
            ProfileManager.DeleteProfile(profile);
        }

        public static void PrintProfile(string[] args)
        {
            string arg = args[0];
            GameProfile profile = ProfileManager.FindProfile(arg);

            if(profile == null)
            {
                PrettyPrinter.Print("profile not found by id or title");
            }

            PrettyPrinter.PrintGameProfile(profile);
        }

        public static void Configuration(string[] args)
        {
            if(args.Length < 1 || args[0] == null)
            {
                PrettyPrinter.Print("must specify get/set action");
            }
            else switch (args[0].ToLower())
            {
                case "set" when args.Length < 3:
                    PrettyPrinter.Print("spectabis: must specify value to set");
                    return;
                case "set":
                    if(ConfigurationManager.Set(args[1], args[2]))
                    {
                        PrettyPrinter.Print($"{args[1]} = {args[2]}");
                    }
                    else
                    {
                        PrettyPrinter.Print("key not found (hint: they are case sensitive)");    
                    }
                    break;
                case "get" when args.Length < 2:
                    PrettyPrinter.Print("must specify (case-sensitive) property name");
                    PrettyPrinter.Print("you can use `*` wilcard to see all options");
                    return;
                case "get":
                {
                    string value = ConfigurationManager.Get(args[1]);

                    if(args[1] == "*")
                    {
                        PrettyPrinter.PrintAllConfiguration();
                        return;
                    }
                    else if(args[1] == null || value == null)
                    {
                        PrettyPrinter.Print("must specify proper property name (case-sensitive)");
                        return;
                    }
                
                    PrettyPrinter.Print($"{args[1]} = {value}");
                    break;
                }
                default:
                    PrettyPrinter.Print($"no such action {args[0]}");
                    break;
            }
        }
    }
}