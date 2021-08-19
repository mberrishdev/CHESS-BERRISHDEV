using ChessGameCore.Constants;
using ChessGameCore.Games;
using ChessGameCore.pairs;
using System;
using System.Collections.Generic;


namespace ChessGameCore.Pieces
{
    public class Pawn : Piece
    {
        private readonly List<Pair> _PossibleMoveArray = new();

        public Pawn(ChessBoard board, PieceColor color, int horizontalCordinate, int verticalCordinate)
               : base(board, Name, color, horizontalCordinate, verticalCordinate)
        {
        }

        public static new PieceType Name = PieceType.Pawn;
        public int MoveCount { get; set; } = 0;


        public override List<Pair> MoveFunction(Checker DangerChecker, Checker CheckerForKingUnderDanger)
        {
            _PossibleMoveArray.Clear();

            int[,] whereGoArray = new int[1, 2];
            int maxLoop = 0;

            MakeMovePair PositionPair = new();
            MakePiecePair MoveTypePair = new();

            var Position = String.Concat(HorizontalCordinate.ToString(), Colon, VerticalCordinate.ToString());

            _PossibleMoveArray.Add(MoveTypePair.Create(Name, Color, EmptySymbol));
            _PossibleMoveArray.Add(PositionPair.Create(HorizontalCordinate, VerticalCordinate, MoveType.SrartPosition, MoveType.NotDanger));

            if (Color == PieceColor.White)
            {

                if (VerticalCordinate == 2)
                {
                    whereGoArray[0, 0] = 0;
                    whereGoArray[0, 1] = 1;
                    maxLoop = 2;
                }
                else if (VerticalCordinate != 2)
                {
                    whereGoArray[0, 0] = 0;
                    whereGoArray[0, 1] = 1;
                    maxLoop = 1;

                }
            }
            else
            {
                if (VerticalCordinate == 8 - 1)
                {
                    whereGoArray[0, 0] = 0;
                    whereGoArray[0, 1] = -1;
                    maxLoop = 2;
                }
                else if (VerticalCordinate != 8 - 1)
                {
                    whereGoArray[0, 0] = 0;
                    whereGoArray[0, 1] = -1;
                    maxLoop = 1;
                }
            }

            _PossibleMoveArray.AddRange(WherePawnCanGo(HorizontalCordinate, VerticalCordinate, whereGoArray[0, 0], whereGoArray[0, 1], maxLoop, Color, CheckerForKingUnderDanger));

            return _PossibleMoveArray;
        }

        public List<Pair> WherePawnCanGo(int horizontal, int vertical, int horizontalstep, int verticalstep, int maxstep, PieceColor pieceColor, Checker CheckerForKingUnderDanger)
        {
            List<Pair> possibleMoveArray = new();
            MakeMovePair MoveTypePair = new();
            int step = 0;
            PieceType PieceName =PieceType.Pawn;

            int fromhorizontal = horizontal;
            int fromvertical = vertical;

            while (vertical <= 8 && horizontal <= 8
                && vertical > 0 && horizontal > 0)
            {

                step++;
                horizontal += horizontalstep;
                vertical += verticalstep;


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

                if (vertical <= 8 && horizontal <= 8
                    && vertical > 0 && horizontal > 0
                    && horizontalstep != horizontal && verticalstep != vertical && step <= maxstep)
                {
                    

                    var Position = String.Concat(horizontal.ToString(), Colon, vertical.ToString());
                    if(IsEnemy(horizontal, vertical, pieceColor, BoardArray))
                    {
                        break;
                    }

                    if (IsEmpty(horizontal, vertical, BoardArray))
                    {
                        if (pieceColor == PieceColor.White && vertical == 8)
                        {
                            possibleMoveArray.Add(MoveTypePair.Create(horizontal, vertical, MoveType.Promotion, MoveType.NotDanger));
                        }
                        else if (pieceColor == PieceColor.Black && vertical == 1)
                        {
                            possibleMoveArray.Add(MoveTypePair.Create(horizontal, vertical, MoveType.Promotion, MoveType.NotDanger));
                        }
                        else
                        {
                            possibleMoveArray.Add(MoveTypePair.Create(horizontal, vertical, MoveType.Free, MoveType.NotDanger));
                        }
                        if (horizontal - 1 > 0 && step == 1)
                        {
                            if (IsEnemy(horizontal - 1, vertical, pieceColor, BoardArray))
                            {
                                Position = String.Concat((horizontal - 1).ToString(), Colon, vertical.ToString());
                                possibleMoveArray.Add(MoveTypePair.Create(horizontal, vertical, MoveType.Kill, MoveType.Danger));
                            }
                        }
                        if (horizontal + 1 < 9 && step == 1)
                        {
                            if (IsEnemy(horizontal + 1, vertical, pieceColor, BoardArray))
                            {
                                Position = String.Concat((horizontal + 1).ToString(), Colon, vertical.ToString());
                                possibleMoveArray.Add(MoveTypePair.Create(horizontal, vertical, MoveType.Kill, MoveType.Danger));
                            }
                        }
                    }
                    else if (true)
                    {
                        if (horizontal - 1 > 0 && step == 1)
                        {
                            if (IsEnemy(horizontal - 1, vertical, pieceColor, BoardArray))
                            {
                                Position = String.Concat((horizontal - 1).ToString(), Colon, vertical.ToString());
                                possibleMoveArray.Add(MoveTypePair.Create(horizontal, vertical, MoveType.Kill, MoveType.Danger)); ;
                            }
                        }
                        if (horizontal + 1 < 9 && step == 1)
                        {
                            if (IsEnemy(horizontal + 1, vertical, pieceColor, BoardArray))
                            {
                                Position = String.Concat((horizontal + 1).ToString(), Colon, vertical.ToString());
                                possibleMoveArray.Add(MoveTypePair.Create(horizontal, vertical, MoveType.Kill, MoveType.Danger));
                            }
                        }
                    }
                }
            }


            return possibleMoveArray;
        }


    }
}
