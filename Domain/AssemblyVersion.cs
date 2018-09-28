using System.Reflection;

namespace spectabis_cli.Domain
{
    public static class AssemblyVersion
    {
        public static string Get()
        {
            return Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;
        }
    }
}
