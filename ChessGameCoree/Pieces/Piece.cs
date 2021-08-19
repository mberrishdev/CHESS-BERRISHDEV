using ChessGameCore.Constants;
using ChessGameCore.Games;
using ChessGameCore.pairs;
using System;
using System.Collections.Generic;


namespace ChessGameCore.Pieces
{
    public abstract class Piece
    {
        public const string Colon = ":";
        public const string EmptySymbol = "";

        public Piece(ChessBoard board, PieceType name, PieceColor color, int horizontalCordinate, int verticalCordinate)
        {
            Name = name;
            Color = color;
            BoardArray = board.BoardArray;
            HorizontalCordinate = horizontalCordinate;
            VerticalCordinate = verticalCordinate;

        }

        public int HorizontalCordinate { get; set; }
        public int VerticalCordinate { get; set; }
        public virtual PieceType Name { get; set; }
        public PieceColor Color { get; set; }
        public Piece[,] BoardArray { get; set; }
        public ChessBoard Board { get; }
        public int KingCordinatesToHorizontal { get; set; } = 0;
        public int KingCordinatesToVertical { get; set; } = 0;

        public bool IsKingUnderDangerChecker { get; set; } = false;

        public List<Pair> WherePieceCanGo(int horizontal, int vertical, int horizontalstep,
            int verticalstep, int maxstep, PieceColor pieceColor, Piece[,] BoardArray, Checker CheckerForKingUnderDanger)
        {

            List<Pair> possibleMoveArray = new();
            possibleMoveArray.Clear();
            int fromhorizontal = horizontal;
            int fromvertical = vertical;

            PieceType PieceName = BoardArray[fromvertical - 1, fromhorizontal - 1].Name;

            int step = 0;
            MakeMovePair MoveTypePair = new();
            while (vertical <= 8 && horizontal <= 8
                && vertical > 0 && horizontal > 0)
            {

                step++;
                horizontal += horizontalstep;
                vertical += verticalstep;

                if (vertical <= 8 && horizontal <= 8
                    && vertical > 0 && horizontal > 0
                    && horizontalstep != horizontal
                    && verticalstep != vertical
                    && step <= maxstep)
                {

                    var Position = String.Concat(horizontal.ToString(), Colon, vertical.ToString());


                    if (CheckerForKingUnderDanger == Checker.CheckForKingUnderDanger && PieceName!= PieceType.King)
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
                        break;
                    }
                    else if (IsNotEnemy(horizontal, vertical, pieceColor, BoardArray))
                    {
                        break;
                    }

                }
            }

            return possibleMoveArray;
        }




        public bool IsEmpty(int horizontal, int vertical, Piece[,] BoardArray)
        {

            if (BoardArray[vertical - 1, horizontal - 1] == null)
            {
                return true;
            }

            return false;
        }

        public bool IsEnemy(int horizontal, int vertical, PieceColor pieceColor, Piece[,] BoardArray)
        {

            if (BoardArray[vertical - 1, horizontal - 1] != null && BoardArray[vertical - 1, horizontal - 1].Color != pieceColor)
            {
                return true;
            }

            return false;
        }

        public bool IsNotEnemy(int horizontal, int vertical, PieceColor pieceColor, Piece[,] BoardArray)
        {

            if (BoardArray[vertical - 1, horizontal - 1] != null && BoardArray[vertical - 1, horizontal - 1].Color == pieceColor)
            {
                return true;
            }

            return false;
        }


        public bool IsKingUnderDanger(PieceColor pieceColor,PieceType PieceName, int horizontal, int vertical, Piece[,] BoardArray)
        {
            List<Pair> otherPieceMoveArray = new();
            List<Pair> mainOtherPieceMoveArray = new();
            if (PieceName == PieceType.King)
            {

                KingCordinatesToHorizontal = horizontal + 1;
                KingCordinatesToVertical = vertical + 1;
            }
            else
            {
                bool BreakChecker = false;
                for (var verticalindex = 0; verticalindex < 8; verticalindex++)
                {
                    for (var horizontalIndex = 0; horizontalIndex < 8; horizontalIndex++)
                    {

                        if (BoardArray[verticalindex, horizontalIndex] != null)
                        {
                            if (BoardArray[verticalindex, horizontalIndex].Color == pieceColor && BoardArray[verticalindex, horizontalIndex].Name == PieceType.King)
                            {
                                KingCordinatesToHorizontal = horizontalIndex + 1;
                                KingCordinatesToVertical = verticalindex + 1;
                                BreakChecker = true;
                                break;
                            }
                        }

                    }

                    if (BreakChecker)
                    {
                        break;
                    }
                }
            }

            for (var verticalindex = 0; verticalindex < 8; verticalindex++)
            {
                for (var horizontalIndex = 0; horizontalIndex < 8; horizontalIndex++)
                {

                    if (BoardArray[verticalindex, horizontalIndex] != null)
                    {
                        if (BoardArray[verticalindex, horizontalIndex].Color != pieceColor && BoardArray[verticalindex, horizontalIndex].Name != PieceType.King)
                        {
                            otherPieceMoveArray.AddRange(BoardArray[verticalindex, horizontalIndex].MoveFunction(Checker.NotCheckForDanger, Checker.NotCheckForKingUnderDanger));
                        }
                    }

                }
            }

            foreach (var exactMoveCordinate in otherPieceMoveArray)
            {
                if (exactMoveCordinate.DangerChecker == MoveType.Danger)
                {
                    mainOtherPieceMoveArray.Add(exactMoveCordinate);
                }
            }

            foreach (var MoveCordinate in mainOtherPieceMoveArray)
            {
                if (MoveCordinate.ToHorizontal == KingCordinatesToHorizontal && MoveCordinate.ToVertical == KingCordinatesToVertical)
                {
                    return true;
                }
            }
            return false;
        }

        public bool ImaginaryMove(int toHorizontal, int toVertical, int fromHorizontal, int fromVertical, PieceColor pieceColor, Piece[,] BoardArray)
        {
            List<Pair> otherPieceMoveArray = new();
            List<Pair> mainOtherPieceMoveArray = new();

            //Imaginary move

            Piece pieceThatMoves = BoardArray[fromVertical - 1, fromHorizontal - 1];
            Piece pieceThatMightGetKilled = BoardArray[toVertical - 1, toHorizontal - 1];

            BoardArray[toVertical - 1, toHorizontal - 1] = pieceThatMoves;
            BoardArray[fromVertical - 1, fromHorizontal - 1] = null;


            for (var verticalindex = 0; verticalindex < 8; verticalindex++)
            {
                for (var horizontalIndex = 0; horizontalIndex < 8; horizontalIndex++)
                {

                    if (BoardArray[verticalindex, horizontalIndex] != null)
                    {
                        if (BoardArray[verticalindex, horizontalIndex].Color != pieceColor && BoardArray[verticalindex, horizontalIndex].Name != PieceType.King)
                        {
                            otherPieceMoveArray.AddRange(BoardArray[verticalindex, horizontalIndex].MoveFunction(Checker.NotCheckForDanger, Checker.NotCheckForKingUnderDanger));
                        }
                    }

                }
            }

            ////Remove Imaginary move
            BoardArray[fromVertical - 1, fromHorizontal - 1] = pieceThatMoves;
            BoardArray[toVertical - 1, toHorizontal - 1] = pieceThatMightGetKilled;


            foreach (var exactMoveCordinate in otherPieceMoveArray)
            {
                if (exactMoveCordinate.DangerChecker == MoveType.Danger)
                {
                    mainOtherPieceMoveArray.Add(exactMoveCordinate);
                }
            }

            foreach (var MoveCordinate in mainOtherPieceMoveArray)
            {
                if (MoveCordinate.ToHorizontal == KingCordinatesToHorizontal && MoveCordinate.ToVertical == KingCordinatesToVertical)
                {
                    return false;
                }
            }
            return true;
        }

        abstract public List<Pair> MoveFunction(Checker DangerChecker, Checker CheckerForKingUnderDanger);

        public bool Mate(string pieceColor)
        {
            List<Pair> possibleMoveArray = new();

            for (var verticalindex = 0; verticalindex < 8; verticalindex++)
            {
                for (var horizontalIndex = 0; horizontalIndex < 8; horizontalIndex++)
                {

                    if (BoardArray[verticalindex, horizontalIndex] != null)
                    {
                        if (BoardArray[verticalindex, horizontalIndex].Color.ToString() != pieceColor)
                        {
                            possibleMoveArray.AddRange(BoardArray[verticalindex, horizontalIndex].MoveFunction(Checker.CheckForDanger, Checker.CheckForKingUnderDanger));
                        }
                    }
                }
            }
            if (possibleMoveArray.Count == 0)
            {
                return true;
            }
            return false;
        }
    }

}
