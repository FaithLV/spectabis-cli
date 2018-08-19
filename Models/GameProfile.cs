namespace spectabis_cmd.Models
{
    public class GameProfile
    {
        public static int GameCount = 0;

        public int ProfileID{get; set;}
        public string ProfileName {get; set;}
        public string GamePath {get; set;}
        public string GameSerial {get; set;}

        public GameProfile()
        {
            ProfileID = GameCount++;
        }
    }
}