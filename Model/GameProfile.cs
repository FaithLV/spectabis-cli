using System;
using System.Text;
using spectabis_cli.Domain;

namespace spectabis_cli.Model
{
    public class GameProfile
    {
        public string ProfileID {get; set;}
        public string ProfileName {get; set;} = "N/A";
        public string GamePath {get; set;}
        public string GameSerial {get; set;}
        public string ImagePath{get; set;}
        public string CustomExecutablePath {get; set;}
        public GameProfile() { }

        public string GenerateHash()
        {
            string hash = string.Empty;
            var Hasher = new CRC32();
            
            byte[] dataBuffer = Encoding.UTF8.GetBytes($"{ProfileName}{GamePath}{GameSerial}{DateTime.Now}"); 
            string hashBuffer = Hasher.Get(dataBuffer).ToString("X2");
            
            return hashBuffer;
        }
    }
}