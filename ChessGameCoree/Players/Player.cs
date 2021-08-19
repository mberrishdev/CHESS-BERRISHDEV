using System;

namespace ChessGameCore.Players
{
    public class Player
    {
        public Player(String playerid, String name, String born, int age, String country)
        {
            PlayerId = playerid;
            Name = name;
            Born = born;
            Age = age;
            Country = country;
        }
        public String PlayerId { get; set; }
        public String Name { get; set; }
        public String Born { get; set; }
        public int Age { get; set; }
        public String Country { get; set; }
    }
}
