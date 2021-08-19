using ChessGameCore.Constants;
using ChessGameCore.Games;
using ChessGameCore.pairs;
using System;
using System.Collections.Generic;

namespace ChessGameCore.Pieces
{
    public class Knight : Piece
    {
        private readonly List<Pair> _PossibleMoveArray = new();

        public Knight(ChessBoard board, PieceColor color, int horizontalCordinate, int verticalCordinate)
               : base(board, Name, color, horizontalCordinate, verticalCordinate)
        {
        }


        public static new PieceType Name = PieceType.Knight;
        public int MoveCount { get; set; } = 0;

        public override List<Pair> MoveFunction(Checker DangerChecker, Checker CheckerForKingUnderDanger)
        {
            _PossibleMoveArray.Clear();
            MakeMovePair PositionPair = new();
            MakePiecePair MoveTypePair = new();

            var Position = String.Concat(HorizontalCordinate.ToString(), Colon, VerticalCordinate.ToString());

            _PossibleMoveArray.Add(MoveTypePair.Create(Name, Color, EmptySymbol));
            _PossibleMoveArray.Add(PositionPair.Create(HorizontalCordinate, VerticalCordinate, MoveType.SrartPosition, MoveType.NotDanger));


            int[,] whereGoArray;
            int maxLoop;


            whereGoArray = new int[,] { { 1, 2 }, { 2, 1 }, { 2, -1 }, { 1, -2 }, { -1, -2 }, { -2, -1 }, { -2, 1 }, { -1, 2 } };
            maxLoop = 1;


            for (var counter = 0; counter < whereGoArray.GetLength(0); counter++)
            {
                _PossibleMoveArray.AddRange(WhereKnightCanGo(HorizontalCordinate, VerticalCordinate, whereGoArray[counter, 0],
                                                            whereGoArray[counter, 1], maxLoop, Color, CheckerForKingUnderDanger));
            }


            return _PossibleMoveArray;
        }

        public List<Pair> WhereKnightCanGo(int horizontal, int vertical, int horizontalstep, int verticalstep,
                                                                     int maxstep, PieceColor pieceColor, Checker CheckerForKingUnderDanger)
        {
            int fromhorizontal = horizontal;
            int fromvertical = vertical;

            List<Pair> possibleMoveArray = new();
            MakeMovePair MoveTypePair = new();
            int step = 0;

            PieceType PieceName = PieceType.Knight;


            while (vertical <= 8 && horizontal <= 8
                && vertical > 0 && horizontal > 0)
            {
                step++;
                horizontal += horizontalstep;
                vertical += verticalstep;

                if (vertical <= 8 && horizontal <= 8
                    && vertical > 0 && horizontal > 0
                    && horizontalstep != horizontal && verticalstep != vertical && step <= maxstep)
                {
                    var Position = String.Concat(horizontal.ToString(), Colon, vertical.ToString());

                    if (CheckerForKingUnderDanger == Checker.CheckForKingUnderDanger)
                    {
                        if (IsKingUnderDanger(pieceColor, PieceName, horizontal, vertical, BoardArray))
                        {
                            IsKingUnderDangerChecker = true;

                            if (!ImaginaryMove(horizontal, vertical, fromhorizontal, fromvertical, pieceColor, BoardArray))
                            {
                                break;
                            }
                        }
                    }

                    if (IsEmpty(horizontal, vertical, BoardArray))
                    {
                        possibleMoveArray.Add(MoveTypePair.Create(horizontal, vertical, MoveType.Free, MoveType.Danger));
                    }
                    else if (IsEnemy(horizontal, vertical, pieceColor, BoardArray))
                    {
                        possibleMoveArray.Add(MoveTypePair.Create(horizontal, vertical, MoveType.Kill, MoveType.Danger));
                    }
                }
            }

            return possibleMoveArray;
        }


    }
}
