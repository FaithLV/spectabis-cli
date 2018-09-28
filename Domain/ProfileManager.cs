using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using spectabis_cli.Model;

namespace spectabis_cli.Domain
{
    public static class ProfileManager
    {

        public static void CreateProfile(string[] args)
        {
            List<GameProfile> allProfiles = GetAllProfiles();
            GameProfile game = SmartParser.CreateGameProfile(args);
            allProfiles.Add(game);

            Directory.CreateDirectory($"{PathManager.ProfileDirectory}//{game.ProfileID}");

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
            return File.Exists(PathManager.ProfileCache) ? JsonConvert.DeserializeObject<List<GameProfile>>(File.ReadAllText(PathManager.ProfileCache)) : new List<GameProfile>();
        }
    }
}