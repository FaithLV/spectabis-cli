using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using spectabis_cmd.Model;

namespace spectabis_cmd.Domain
{
    public static class ConfigurationManager
    {
        public static bool Set<T> (string setting, T newValue)
        {
            ConfigrationModel config = GetConfig();

            try
            {
                typeof(ConfigrationModel).GetProperty(setting).SetValue(config, newValue);
            }
            catch(NullReferenceException)
            {
                return false;
            }
            

            if(File.Exists(PathManager.ConfigurationPath))
            {
                File.Delete(PathManager.ConfigurationPath);
            }

            using(StreamWriter writer = File.CreateText(PathManager.ConfigurationPath))
            {
                JsonSerializer serial = new JsonSerializer();
                serial.Formatting = Formatting.Indented;
                serial.Serialize(writer, config);
            }

            return true;
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
            ConfigrationModel config = GetConfig();

            foreach(PropertyInfo prop in _props)
            {
                var x = typeof(ConfigrationModel).GetProperty(prop.Name).GetValue(config);
                dict.Add(prop.Name, x.ToString());
            }
            
            return dict;
        }

        private static ConfigrationModel GetConfig()
        {
            return File.Exists(PathManager.ConfigurationPath) ? JsonConvert.DeserializeObject<ConfigrationModel>(File.ReadAllText(PathManager.ConfigurationPath)) : new ConfigrationModel();
        }
    }

    public class ConfigrationModel
    {
        public string EmulatorExectuablePath {get; set;} = "null";
        public string GameDatabaseFilePath {get; set;} = "null";
        public string ConfigsPath {get; set;} = "null";
        public string PluginsPath {get; set;} = "null";
    }
}