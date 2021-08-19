using ChessGameCore.Constants;
using System;
namespace ChessGameCore.pairs
{
    public class Pair
    {

        public Pair(String name, String color, int horizontal, int vertical)
        {
            Name = name;
            Color = color;
            Horizontal = horizontal;
            Vertical = vertical;
        }

        //Move information pair
        public Pair(int toHorizontal, int toVertical, MoveType moveType, MoveType dangerChecker)
        {
            ToHorizontal = toHorizontal;
            ToVertical = toVertical;
            MoveType = moveType;
            DangerChecker = dangerChecker;
        }
        public int ToHorizontal { get; set; }
        public int ToVertical { get; set; }
        public MoveType MoveType { get; set; }
        public MoveType DangerChecker { get; set; }


        //Piece information pair
        public Pair(PieceType name, PieceColor color, String emptySymbol)
        {
            PieceName = name;
            PieceColor = color;
            EmptySymbol = emptySymbol;
        }
        public PieceType PieceName { get; set; }
        public PieceColor PieceColor { get; set; }
        public String EmptySymbol { get; set; }


        //Curren game informatiuon pair
        public Pair(String whitePlayerId, String blackPlayerId, String gameId)
        {
            WhitePlayerId = whitePlayerId;
            BlackPlayerId = blackPlayerId;
            EmptySymbol = gameId;
        }
        public String WhitePlayerId { get; set; }
        public String BlackPlayerId { get; set; }
        public String GameId { get; set; }


        //One player game information pair
        public Pair(String playerId, PieceColor color, String gameId)
        {
            PlayerId = playerId;
            PieceColor = color;
            GameId = gameId;
        }
        public String PlayerId { get; set; }


        //public String Player1 { get; set; }
        //public String Player2 { get; set; }
        public String Name { get; set; }
        public String Color { get; set; }
        public int Horizontal { get; set; }
        public int Vertical { get; set; }
    }




    //Move information pairmaker
    public class MakeMovePair
    {
        public MakeMovePair()
        {
        }
        public Pair Create(int ToHorizontal, int ToVertical, MoveType MoveType, MoveType DangerChecker)
        {
            return new Pair(ToHorizontal, ToVertical, MoveType, DangerChecker);
        }
    }


    //Move information pairmaker
    public class MakePiecePair
    {
        public MakePiecePair()
        {
        }
        public Pair Create(PieceType Name, PieceColor Color, String EmptySymbol)
        {
            return new Pair(Name, Color, EmptySymbol);
        }
    }

    //Curren game informatiuon pairmaker
    public class MakeCurrentGameInfoPair
    {
        public MakeCurrentGameInfoPair()
        {
        }
        public Pair Create(String WhitePlayerId, String blackPlayerId, String gameId)
        {
            return new Pair(WhitePlayerId, blackPlayerId, gameId);
        }
    }


    //One player game information pairmaker
    public class MakeOnePlayerGameInformation
    {
        public MakeOnePlayerGameInformation()
        {
        }
        public Pair Create(String PlayerId, PieceColor Color, String GameId)
        {
            return new Pair(PlayerId, Color, GameId);
        }
    }


    public class MakeBoardPair
    {
        public MakeBoardPair()
        {
        }
        public Pair Create(string name, String color, int horizontal, int vertical)
        {
            return new Pair(name, color, horizontal, vertical);
        }
    }
}
