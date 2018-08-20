using System;

namespace spectabis_cmd.Models
{
    public static class PathManager
    {
        public static readonly string BaseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        public static readonly string ProfileDirectory = $"{BaseDirectory}/Profiles/";
        public static readonly string ProfileCache = $"{ProfileDirectory}//profiles.json";
        public static readonly string ConfigrationPath = $"{BaseDirectory}/spectabis.json";
    }
}