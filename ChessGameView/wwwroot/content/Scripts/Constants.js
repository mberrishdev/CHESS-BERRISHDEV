const KingPosition = [[0, 1], [1, 1], [1, 0], [1, -1], [0, -1], [-1, -1], [-1, 0], [-1, 1]];
const PawnPosition = [[0, 1], [0, 2]];
const RookPosition = [
  [0, 1], [0, 2], [0, 3], [0, 4], [0, 5], [0, 6], [0, 7],
  [0, -1], [0, -2], [0, -3], [0, -4], [0, -5], [0, -6], [0, -7],
  [-1, 0], [-2, 0], [-3, 0], [-4, 0], [-5, 0], [-6, 0], [-7, 7],
  [1, 0], [2, 0], [3, 0], [4, 0], [5, 0], [6, 0], [7, 0]
]
const KnightPosition = [[1, 2], [2, 1], [2, -1], [1, -2], [-1, -2], [-2, -1], [-2, 1], [-1, 2]];


const PanwType = "Pawn";
const RookType = "Rook";
const KingType = "King";
const BishopType = "Bishop";
const QueenType = "Queen";
const KnightType = "Knight";

const Body = "body";
const Enter = "#enter";
const Table = ".table";

const PieceConst = "piece";
const ColorConst = "color";
const HorizontalConst = "horizontal";
const VerticalConst = "vertical";

const Img = "img";
const Location = "../Content/Images/Pieces/";
const Png = ".png";

const Colon = ":";
const Underscore = "_";
const Space = " ";
const NoSpace = "";

const Yellow = "yellow";
const Green = "Green";
const Red = "Red";

const ColorBlack = "Black";
const ColorWhite = "White";

const BlackTableColor = "#70a2a3";
const WhiteTableColor = "#b1e4b8";

const Td = "td";
const Th = "th";
const Tr = "tr";

const T01 = "t01";

const White = ".White";
const Black = ".Black";
const Input = "input";
const Button = "button";

const EnterBoard = "#enterBoard";
const ResetBoard = "#resetBoard";
const NumberH = "numberH";
const NumberW = "numberW";

const Normal = "normal";
const Unusual = "unusual";

const AnimationSpeed = 450;

const Danger ="danger";
const NotDanger = "notDanger";
const Color ="color";
const NotColor = "notColor";

const Domain = "https://localhost:44348/";

var url= new URL(window.location.href)