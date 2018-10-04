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
        public string GS_Plugin {get; set;}
        public string PAD_Plugin {get; set;}
        public string SPU2_Plugin {get; set;}
        public string CDVD_Plugin {get; set;}
        public string USB_Plugin {get; set;}
        public string FW_Plugin {get; set;}
        public string DEV9_Plugin {get; set;}
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