using System;
using System.Collections.Generic;
using ChessGameCore.Games;
using ChessGameCore.Pieces;
using ChessGameCore.Constants;


namespace ChessGameCore.Games
{
    public class ChessBoard
    {
        public ChessBoard(int height, int width)
        {
            Height = height;
            Width = width;
            BoardArray = CreateBoard();
        }

        public King WhiteKing { get; set; }
        public King BlackKing { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public Piece[,] BoardArray { get; set; }

        public Piece[,] CreateBoard()
        {
            Piece[,] boardarray = new Piece[Height, Width];

            for (var verticalindex = 0; verticalindex < Height; verticalindex++)
            {
                for (var index = 0; index < Width; index++)
                {
                    boardarray[verticalindex, index] = null;
                }

            }

            return boardarray;
        }
    }

}
