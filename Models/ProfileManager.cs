using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace spectabis_cmd.Models
{
    public static class ProfileManager
    {

        public static void CreateProfile(string[] args)
        {
            List<GameProfile> allProfiles = GetAllProfiles();

            GameProfile game = SmartParse.CreateGameProfile(args);
            allProfiles.Add(game);

            WriteAllProfiles(allProfiles);
        }
        
        public static void WriteAllProfiles(List<GameProfile> profiles)
        {   
            Directory.CreateDirectory(PathManager.ProfileDirectory);

            if(File.Exists(PathManager.ProfileCache))
            {
                File.Delete(PathManager.ProfileCache);
            }

            using(StreamWriter writer = File.CreateText(PathManager.ProfileCache))
            {
                JsonSerializer serial = new JsonSerializer();
                serial.Formatting = Formatting.Indented;
                serial.Serialize(writer, profiles);
            }

        }

        public static List<GameProfile> GetAllProfiles()
        {
            if(File.Exists(PathManager.ProfileCache))
            {
                return JsonConvert.DeserializeObject<List<GameProfile>>(File.ReadAllText(PathManager.ProfileCache));
            }
            else
            {
                return new List<GameProfile>();
            }
        }
    }
}