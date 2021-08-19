using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessGameCore.Constants;

namespace ChessGameCore.Games
{
    public class PlayerClass
    {
        public PlayerClass(string playerName, PieceColor playerColor)
        {
            PlayerName = playerName;
            PlayerColor = playerColor;
        }
        
        public string PlayerName { get; set; }
        public PieceColor PlayerColor { get; set; }
        public long LastPing { get; set; }
        public long Ping { get; set; } = 600;

    }
}
