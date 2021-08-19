using System;
using System.Collections.Generic;
using ChessGameCore.pairs;
using ChessGameCore.Games;
using ChessGameCore.Constants;

namespace ChessGameCore.Pieces
{
    public class Bishop : Piece
    {
        private readonly List<Pair> _PossibleMoveArray = new();

        public Bishop(ChessBoard board, PieceColor color, int horizontalCordinate, int verticalCordinate)
               : base(board, Name, color, horizontalCordinate, verticalCordinate)
        {
        }


        public static new PieceType Name = PieceType.Bishop;
        public int MoveCount { get; set; } = 0;


        public override List<Pair> MoveFunction(Checker DangerChecker, Checker CheckerForKingDanger)
        {
            _PossibleMoveArray.Clear();
            int[,] whereGoArray = new int[,] { { 1, 1 }, { 1, -1 }, { -1, -1 }, { -1, 1 } };

            MakeMovePair PositionPair = new();
            MakePiecePair MoveTypePair = new();

            var Position = String.Concat(HorizontalCordinate.ToString(), Colon, VerticalCordinate.ToString());

            _PossibleMoveArray.Add(MoveTypePair.Create(Name, Color, EmptySymbol));
            _PossibleMoveArray.Add(PositionPair.Create(HorizontalCordinate, VerticalCordinate, MoveType.SrartPosition, MoveType.NotDanger));



            int maxLoop = 10;
            for (var counter = 0; counter < whereGoArray.GetLength(0); counter++)
            {
                _PossibleMoveArray.AddRange(WherePieceCanGo(HorizontalCordinate, VerticalCordinate, whereGoArray[counter, 0],
                                                            whereGoArray[counter, 1], maxLoop, Color, BoardArray, CheckerForKingDanger));
            }

            return _PossibleMoveArray;
        }

       
    }
}
