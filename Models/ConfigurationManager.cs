using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

namespace spectabis_cmd.Models
{
    public static class ConfigurationManager
    {
        public static void Set(string setting, dynamic newValue)
        {
            ConfigrationModel config = GetConfig();
            typeof(ConfigrationModel).GetProperty(setting).SetValue(config, newValue);

            if(File.Exists(PathManager.ConfigrationPath))
            {
                File.Delete(PathManager.ConfigrationPath);
            }

            using(StreamWriter writer = File.CreateText(PathManager.ConfigrationPath))
            {
                JsonSerializer serial = new JsonSerializer();
                serial.Formatting = Formatting.Indented;
                serial.Serialize(writer, config);
            }
        }

        public static string Get(string setting)
        {
            try
            {
                ConfigrationModel config = GetConfig();
                object value = typeof(ConfigrationModel).GetProperty(setting).GetValue(config);
                return value.ToString();   
            }
            catch
            {
                return null;
            }
        }

        public static Dictionary<string, string> GetAllConfiguration()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            PropertyInfo[] _props = typeof(ConfigrationModel).GetProperties();

            foreach(PropertyInfo prop in _props)
            {
                dict.Add(prop.Name, prop.GetValue(prop.Name).ToString());
            }
            
            return dict;
        }

        private static ConfigrationModel GetConfig()
        {
            if(File.Exists(PathManager.ConfigrationPath))
            {
                return JsonConvert.DeserializeObject<ConfigrationModel>(File.ReadAllText(PathManager.ConfigrationPath));
            }

            return new ConfigrationModel();
        }
    }

    public class ConfigrationModel
    {
        public string PCSX2 {get; set;} = "null";
        public string DefaultConfigs {get; set;} = "null";
        public string GameDatabasePath {get; set;} = "null";
    }
}