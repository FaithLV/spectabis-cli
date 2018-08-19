using System.Reflection;

namespace spectabis_cmd.Models
{
    public static class AssemblyVersion
    {
        public static string Get()
        {
            return Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;
        }
    }
}
