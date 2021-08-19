using System.Collections.Generic;

namespace ChessGameCore.Players
{
    public class Players
    {
        public Players()
        {
            PlayerList = PlayerListMaker();
        }
        public List<Player> PlayerList { get; set; }
        public static List<Player> PlayerListMaker()
        {
            List<Player> PlayerList = new();
            PlayerList.Add(new Player("player1", "Mikheil Berisvhili", " 12, Decemeber, 2002", 18, " Georgia"));
            PlayerList.Add(new Player("player2", "Magnus Carlsen", " 30, November, 1990", 30, " Norway"));
            PlayerList.Add(new Player("player3", "Viswanathan Anand", " 11, December, 1969", 51, " India"));
            PlayerList.Add(new Player("player4", "Alireza Firouzja", " 18, June, 2003", 17, " Iran"));
            return PlayerList;
        }
    }

}
