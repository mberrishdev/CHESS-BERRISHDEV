
namespace ChessGameCore.Constants
{

    public enum MoveType
    {
        Free,
        Kill,
        Castle,
        SrartPosition,
        Danger,
        NotDanger,
        Promotion
    }
    public enum PieceType
    {
        King,
        Queen,
        Rook,
        Bishop,
        Knight,
        Pawn
    }


    public enum PieceColor
    {
        White,
        Black
    }

    public enum Checker
    {
        NotCheckForDanger,
        CheckForDanger,
        NotCheckForKingUnderDanger,
        CheckForKingUnderDanger
    }

}
