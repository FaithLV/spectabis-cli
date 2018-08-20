using System.IO;
using Newtonsoft.Json;

namespace spectabis_cmd.Models
{
    public static class SmartParse
    {
        public static void CreateGameProfile(string[] args)
        {
            bool isFilePath = File.Exists(args[0]);
            GameProfile profile = null;

            if(isFilePath)
            {
                profile = new GameProfile() { GamePath = args[0] };

                if(args.Length > 1)
                {
                    profile.ProfileName = args[1];
                }
            }
            else
            {
                profile = new GameProfile() { ProfileName = args[0] };
            }

            string profileJson = JsonConvert.SerializeObject(profile);
        }
    }
}