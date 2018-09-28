using System;

namespace spectabis_cli.Model
{
    public static class PathManager
    {
        private static readonly string BaseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        public static readonly string ProfileDirectory = $"{BaseDirectory}/Profiles/";
        public static readonly string ProfileCache = $"{ProfileDirectory}//profiles.json";
        public static readonly string ConfigurationPath = $"{BaseDirectory}/spectabis.json";
    }
}