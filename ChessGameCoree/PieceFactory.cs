using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessGameCore.Pieces;
using ChessGameCore;
using ChessGameCore.pairs;
using ChessGameCore.Constants;
using ChessGameCore.Games;

namespace ChessGameCore
{
    public abstract class PieceFactory
    {
        public abstract Piece CreatePiece(ChessBoard board, PieceType name, PieceColor color, int horizontalCordinate, int verticalCordinate);

    }

    public class RookFactory : PieceFactory
    {
        public override Piece CreatePiece(ChessBoard board, PieceType name, PieceColor color, int horizontalCordinate, int verticalCordinate)
        {
            return new Rook(board, color, horizontalCordinate, verticalCordinate);
        }
    }


    public class KingFactory : PieceFactory
    {
        public override Piece CreatePiece(ChessBoard board, PieceType name, PieceColor color, int horizontalCordinate, int verticalCordinate)
        {
            return new King(board, color, horizontalCordinate, verticalCordinate);
        }
    }

    public class QueenFactory : PieceFactory
    {
        public override Piece CreatePiece(ChessBoard board, PieceType name, PieceColor color, int horizontalCordinate, int verticalCordinate)
        {
            return new Queen(board, color, horizontalCordinate, verticalCordinate);
        }
    }

    public class KnightFactory : PieceFactory
    {
        public override Piece CreatePiece(ChessBoard board, PieceType name, PieceColor color, int horizontalCordinate, int verticalCordinate)
        {
            return new Queen(board, color, horizontalCordinate, verticalCordinate);
        }
    }

}