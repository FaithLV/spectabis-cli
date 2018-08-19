using System;
using spectabis_cmd.Models;

namespace spectabis_cmd
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine($"spectabis-cmd {AssemblyVersion.Get()}");
            Console.WriteLine("Hello World!");
        }
    }
}
