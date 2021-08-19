using ChessGameCore.Constants;
using ChessGameCore.Pieces;
using System;


namespace ChessGameCore.Games
{
    public class Game
    {
        public Game(string playerWhoMadeGame, PieceColor choosenColor)
        {
            WhitePlayerId = null;
            BlackPlayerId = null;
            DeterminePlayerId(playerWhoMadeGame, choosenColor);
            GameId = GenerateId();
            HorizontalMax = 8;
            VerticalMax = 8;
            IsStarted = false;
            GameBoard = DefaultStartingPosition();
            PlayerWhoMadeGame = MakeNewPlayer(playerWhoMadeGame, choosenColor);
        }


        public PlayerClass PlayerWhoMadeGame { get; set; }
        public PlayerClass PlayerWhoJoined { get; set; }

        public double GameStartTime { get; set; } = 0;

        public string WhitePlayerId { get; set; }
        public string BlackPlayerId { get; set; }

        public int HorizontalMax { get; set; }
        public int VerticalMax { get; set; }

        public string GameId { set; get; }

        public int VersionBoard { get; set; } = 0;
        public PieceColor WhoseTurnIsIt { get; set; } = PieceColor.White;
        public ChessBoard GameBoard { set; get; }

        public bool IsWhiteKingOrRookMoved { get; set; } = false;
        public bool IsBlackKingOrRookMoved { get; set; } = false;

        public bool OfferofDraw { get; set; } = false;
        public bool AcceptofDraw { get; set; } = false;
        public string WhoOfferofDraw { get; set; }

        public bool IsStarted { get; set; }
        public bool IsFinished { get; set; }




        public static PlayerClass MakeNewPlayer(string playerWhoMadeGame, PieceColor choosenColor)
        {
            return new PlayerClass(playerWhoMadeGame, choosenColor);
        }

        public string GenerateId()
        {
            return Guid.NewGuid().ToString();
        }

        public void JoinPlayer(string playerWhoJoined)
        {
            IsStarted = true;

            if (PlayerWhoMadeGame.PlayerColor == PieceColor.White)
            {
                PlayerWhoJoined = new PlayerClass(playerWhoJoined, PieceColor.Black);
            }
            if (PlayerWhoMadeGame.PlayerColor == PieceColor.Black)
            {
                PlayerWhoJoined = new PlayerClass(playerWhoJoined, PieceColor.White);
            }

            if (BlackPlayerId == null)
            {
                BlackPlayerId = playerWhoJoined;
            }
            if (WhitePlayerId == null)
            {
                WhitePlayerId = playerWhoJoined;
            }
        }

        public void DeterminePlayerId(string playerWhomadeGame, PieceColor choosenColor)
        {
            if (choosenColor == PieceColor.Black)
            {
                WhitePlayerId = null;
                BlackPlayerId = playerWhomadeGame.ToString();
            }
            if (choosenColor == PieceColor.White)
            {
                WhitePlayerId = playerWhomadeGame.ToString();
                BlackPlayerId = null;
            }
        }

        public ChessBoard DefaultStartingPosition()
        {
            ChessBoard game = new(HorizontalMax, VerticalMax);

            //white figuress
            game.BoardArray[0, 0] = new Rook(game, PieceColor.White, 1, 1);
            game.BoardArray[0, 1] = new Knight(game, PieceColor.White, 2, 1);
            game.BoardArray[0, 2] = new Bishop(game, PieceColor.White, 3, 1);
            game.BoardArray[0, 3] = new Queen(game, PieceColor.White, 4, 1);
            game.BoardArray[0, 4] = new King(game, PieceColor.White, 5, 1);
            game.WhiteKing = new King(game, PieceColor.White, 5, 1);
            game.BoardArray[0, 5] = new Bishop(game, PieceColor.White, 6, 1);
            game.BoardArray[0, 6] = new Knight(game, PieceColor.White, 7, 1);
            game.BoardArray[0, 7] = new Rook(game, PieceColor.White, 8, 1);

            for (var index = 0; index < 8; index++)
            {
                game.BoardArray[1, index] = new Pawn(game, PieceColor.White, index + 1, 2);
            }

            //black figures
            game.BoardArray[7, 0] = new Rook(game, PieceColor.Black, 1, 8);
            game.BoardArray[7, 1] = new Knight(game, PieceColor.Black, 2, 8);
            game.BoardArray[7, 2] = new Bishop(game, PieceColor.Black, 3, 8);
            game.BoardArray[7, 3] = new Queen(game, PieceColor.Black, 4, 8);
            game.BoardArray[7, 4] = new King(game, PieceColor.Black, 5, 8);
            game.BlackKing = new King(game, PieceColor.Black, 5, 8);
            game.BoardArray[7, 5] = new Bishop(game, PieceColor.Black, 6, 8);
            game.BoardArray[7, 6] = new Knight(game, PieceColor.Black, 7, 8);
            game.BoardArray[7, 7] = new Rook(game, PieceColor.Black, 8, 8);


            for (var index = 0; index < 8; index++)
            {
                game.BoardArray[6, index] = new Pawn(game, PieceColor.Black, index + 1, 7);
            }

            return game;
        }


    }
}
