using ChessGameCore.Constants;
using ChessGameCore.Games;
using ChessGameCore.pairs;
using System;
using System.Collections.Generic;

namespace ChessGameCore.Pieces
{
    public class King : Piece
    {
        private readonly List<Pair> _PossibleMoveArray = new();
        private readonly List<Pair> _OtherPieceMoveArray = new();
        private readonly List<Pair> _MainOtherPieceMoveArray = new();

        public King(ChessBoard board, PieceColor color, int horizontalCordinate, int verticalCordinate)
               : base(board, Name, color, horizontalCordinate, verticalCordinate)
        {
        }

        public static new PieceType Name = PieceType.King;
        public int MoveCount { get; set; } = 0;



        public override List<Pair> MoveFunction(Checker DangerChecker, Checker CheckerForKingDanger)
        {

            _PossibleMoveArray.Clear();
            MakeMovePair PositionPair = new();
            MakePiecePair MoveTypePair = new();

            var Position = String.Concat(HorizontalCordinate.ToString(), Colon, VerticalCordinate.ToString());

            _PossibleMoveArray.Add(MoveTypePair.Create(Name, Color, EmptySymbol));
            _PossibleMoveArray.Add(PositionPair.Create(HorizontalCordinate, VerticalCordinate, MoveType.SrartPosition, MoveType.NotDanger));

            int[,] whereGoArray = new int[,] { { 0, 1 }, { 1, 1 }, { 1, 0 }, { 1, -1 }, { 0, -1 }, { -1, -1 }, { -1, 0 }, { -1, 1 } };
            int maxLoop = 1;


            if (_PossibleMoveArray[1].ToHorizontal == 5 && _PossibleMoveArray[1].ToVertical == 1 && BoardArray[0, 7] != null && BoardArray[0, 7].Name == PieceType.Rook)
            {
                _PossibleMoveArray.AddRange(Castle(HorizontalCordinate, VerticalCordinate, 1));

            }
            if (_PossibleMoveArray[1].ToHorizontal == 5 && _PossibleMoveArray[1].ToVertical == 1 && BoardArray[0, 0] != null && BoardArray[0, 0].Name == PieceType.Rook)
            {
                _PossibleMoveArray.AddRange(Castle(HorizontalCordinate, VerticalCordinate, -1));
            }

            if (_PossibleMoveArray[1].ToHorizontal == 5 && _PossibleMoveArray[1].ToVertical == 8 && BoardArray[7, 7] != null && BoardArray[7, 7].Name == PieceType.Rook)
            {
                _PossibleMoveArray.AddRange(Castle(HorizontalCordinate, VerticalCordinate, 1));
            }

            if (_PossibleMoveArray[1].ToHorizontal == 5 && _PossibleMoveArray[1].ToVertical == 8 && BoardArray[7, 0] != null && BoardArray[7, 0].Name == PieceType.Rook)
            {
                _PossibleMoveArray.AddRange(Castle(HorizontalCordinate, VerticalCordinate, -1));
            }


            for (var counter = 0; counter < whereGoArray.GetLength(0); counter++)
            {
                _PossibleMoveArray.AddRange(WherePieceCanGo(HorizontalCordinate, VerticalCordinate, whereGoArray[counter, 0],
                                                                whereGoArray[counter, 1], maxLoop, Color, BoardArray, CheckerForKingDanger));
            }


            if (DangerChecker == Checker.CheckForDanger)
            {

                for (var horizontalIndex = 0; horizontalIndex < 8; horizontalIndex++)
                {
                    for (var verticalindex = 0; verticalindex < 8; verticalindex++)
                    {

                        if (BoardArray[verticalindex, horizontalIndex] != null)
                        {
                            if (BoardArray[verticalindex, horizontalIndex].Color != Color)
                            {
                                _OtherPieceMoveArray.AddRange(BoardArray[verticalindex, horizontalIndex].MoveFunction(Checker.NotCheckForDanger, Checker.CheckForKingUnderDanger));
                            }
                        }

                    }
                }


                foreach (var exactMoveCordinate in _OtherPieceMoveArray)
                {
                    if (exactMoveCordinate.DangerChecker == MoveType.Danger)
                    {
                        _MainOtherPieceMoveArray.Add(exactMoveCordinate);
                    }
                }

                for (int index = 2; index < _PossibleMoveArray.Count; index++)
                {
                    if (_PossibleMoveArray[index].DangerChecker == MoveType.Danger)
                    {

                        if (_MainOtherPieceMoveArray.Count != 0)
                        {
                            foreach (var exactMoveCordinateOtherPiece in _MainOtherPieceMoveArray)
                            {
                                if (_PossibleMoveArray[index].ToHorizontal != 0 && _PossibleMoveArray[index].ToVertical != 0)
                                {
                                    if (_PossibleMoveArray[index].ToHorizontal.ToString().IndexOf(exactMoveCordinateOtherPiece.ToHorizontal.ToString()) == 0
                                      && _PossibleMoveArray[index].ToVertical.ToString().IndexOf(exactMoveCordinateOtherPiece.ToVertical.ToString()) == 0)
                                    {
                                        _PossibleMoveArray.Remove(_PossibleMoveArray[index]);
                                        index--;
                                    }
                                    //if (exactMoveCordinateOtherPiece.ToHorizontal == 6 && exactMoveCordinateOtherPiece.ToVertical == 1
                                    //    && _PossibleMoveArray[index].ToHorizontal == 7 && _PossibleMoveArray[index].ToVertical == 1
                                    //    && _PossibleMoveArray[index].MoveType == MoveType.Castle
                                    //    || exactMoveCordinateOtherPiece.ToHorizontal == 3 && exactMoveCordinateOtherPiece.ToVertical == 1
                                    //    && _PossibleMoveArray[index].ToHorizontal == 4 && _PossibleMoveArray[index].ToVertical == 1
                                    //    && _PossibleMoveArray[index].MoveType == MoveType.Castle
                                    //    || exactMoveCordinateOtherPiece.ToHorizontal == 6 && exactMoveCordinateOtherPiece.ToVertical == 8
                                    //    && _PossibleMoveArray[index].ToHorizontal == 4 && _PossibleMoveArray[index].ToVertical == 1
                                    //    && _PossibleMoveArray[index].MoveType == MoveType.Castle
                                    //    || exactMoveCordinateOtherPiece.ToHorizontal == 3 && exactMoveCordinateOtherPiece.ToVertical == 8
                                    //    && _PossibleMoveArray[index].ToHorizontal == 4 && _PossibleMoveArray[index].ToVertical == 8
                                    //    && _PossibleMoveArray[index].MoveType == MoveType.Castle)
                                    //{

                                    //    _PossibleMoveArray.Remove(exactMoveCordinateOtherPiece);
                                    //    index--;
                                    //}
                                }
                            }
                        }
                    }
                }

            }
            else if (DangerChecker == Checker.NotCheckForDanger)
            {
                return _PossibleMoveArray;
            }

            return _PossibleMoveArray;

        }

        public List<Pair> Castle(int horizontal, int vertical, int direction)
        {
            List<Pair> possibleMoveArray = new();
            MakeMovePair MoveTypePair = new();

            if (this.IsEmpty(horizontal + direction * 1, vertical, BoardArray) && this.IsEmpty(horizontal + direction * 2, vertical, BoardArray))
            {
                var Position = String.Concat((horizontal + direction * 2).ToString(), Colon, vertical.ToString());

                if (this.IsEmpty(horizontal + direction * 2, vertical, BoardArray))
                {
                    possibleMoveArray.Add(MoveTypePair.Create(horizontal, vertical, MoveType.Castle, MoveType.Danger));
                }
            }


            return possibleMoveArray;
        }


    }
}
