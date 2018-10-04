using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using spectabis_cli.Model;

namespace spectabis_cli.Domain
{
    public static class ProfileManager
    {

        public static void CreateProfile(GameProfile profile)
        {
            List<GameProfile> allProfiles = GetAllProfiles();
            GameProfile game = profile;
            allProfiles.Add(game);

            Directory.CreateDirectory($"{PathManager.ProfileDirectory}//{game.ProfileID}");

            WriteAllProfiles(allProfiles);
        }


        public static void DeleteProfile(GameProfile profile)
        {
            List<GameProfile> allProfiles = GetAllProfiles();

            try
            {
                Directory.Delete($"{PathManager.ProfileDirectory}//{profile.ProfileID}");
            }
            catch(Exception e)
            {
                PrettyPrinter.Print("Something went wrong deleting profile configuration files.");
                System.Console.WriteLine(e.Message);
            }
            

            allProfiles.RemoveAll(x => x.ProfileID == profile.ProfileID);
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

        public static GameProfile FindProfile(string query)
        {
            List<GameProfile> profiles = GetAllProfiles();

            bool isProfileID = profiles.Any(x => x.ProfileID.ToLower() == query.ToLower());
            bool isTitle = profiles.Any(x => x.ProfileName.ToLower().Contains(query.ToLower()));

            if(!isTitle && !isProfileID)
            {
                return null;
            }

            GameProfile profile;

            if(isProfileID)
            {
                profile = profiles.SingleOrDefault(x => x.ProfileID.ToLower() == query.ToLower());
            }
            else
            {
                profile = profiles.SingleOrDefault(x => x.ProfileName.ToLower().Contains(query.ToLower()));
            }

            return profile;
        }
    }
}