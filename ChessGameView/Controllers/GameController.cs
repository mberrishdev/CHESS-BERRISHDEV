using ChessGameCore.Constants;
using ChessGameCore.Games;
using ChessGameCore.Players;
using Microsoft.AspNetCore.Mvc;

namespace ChessGameView.Controllers
{
    public class GameController : Controller
    {
        private readonly GameManager _Data;
        public GameController(GameManager gameManager)
        {
            _Data = gameManager;
        }

        //Fucntions for moves
        public IActionResult WherePieceCanGo(int horizontal, int vertical, string gameId, string color)
        {
            return Json(_Data.WherePieceCanGO(horizontal, vertical, gameId, color));
        }

        public IActionResult MakeMove(string gameId, int fromHorizontal, int fromVertical, int toHorizontal,
                                      int toVertical, bool Promotion, string NewName, string pieceColor)
        {
            return Json(_Data.IsMoveValidate(gameId, fromHorizontal, fromVertical, toHorizontal, toVertical, Promotion, NewName, pieceColor));
        }

        public IActionResult CastleMove(string gameId, int fromHorizontal, int fromVertical, int toHorizontal,
                                        int toVertical)
        {
            return Json(_Data.CastleMove(gameId, fromHorizontal, fromVertical, toHorizontal, toVertical));
        }


        //Functions which create/join/remove game
        public IActionResult CreateGame(string playerWhoCreatedGame, string choosenColor)
        {
            return Json(_Data.CreateGame(playerWhoCreatedGame, choosenColor));
        }

        public IActionResult JoinPlayer(string gameId, string playerWhoJoined)
        {
            return Json(_Data.JoinPlayer(gameId, playerWhoJoined));
        }

        public IActionResult RemoveGame(string gameId)
        {
            return Json(_Data.RemoveGameToList(gameId));
        }


        //End game/ draw
        public IActionResult EndGame(string gameId, string playerId)
        {
            var winner = _Data.EndGame(gameId, playerId);
            _Data.RemoveGameToList(gameId);

            return Json(winner);
        }

        public IActionResult EndGameColor(string gameId, string playerColor)
        {
            return Json(EndGameColor(gameId, playerColor));
        }
        public IActionResult Draw(string gameId, string playerId)
        {
            return Json(_Data.Draw(gameId, playerId));
        }

        public IActionResult OfferOfDrawChecker(string gameId)
        {
            return Json(_Data.OfferOfDrawChecker(gameId));
        }

        public IActionResult AcceptofDraw(string gameId)
        {
            return Json(_Data.AcceptofDraw(gameId));
        }

        public IActionResult CheckAcceptOrDecline(string gameId)
        {
            return Json(_Data.AcceptofDraw(gameId));
        }


        //Functions which give us information about game
        public IActionResult CurrentGameInfo()
        {
            return Json(_Data.CurrentGameInfo());
        }

        public IActionResult IsSecondPlayerIsInGame(string gameId)
        {
            return Json(_Data.IsSecondPlayerIsInGame(gameId));
        }

        public IActionResult PlayersInformation(string gameId)
        {
            int index = _Data.GameManagerList.FindIndex(a => a.GameId == gameId);
            if (index != -1)
            {
                var playerInformation = new { Player1 = _Data.GameManagerList[index].WhitePlayerId, Player2 = _Data.GameManagerList[index].BlackPlayerId };
                return Json(playerInformation);
            }
            return Json(null);

        }

        public IActionResult GameManagerList()
        {
            return Json(_Data.GameManagerListInfromation());
        }

        public IActionResult OnePlayerGameInfo()
        {
            return Json(_Data.OnePlayerGameInformation());
        }

        public IActionResult MyGame(string PlayerId)
        {
            return Json(_Data.MyGame(PlayerId));
        }

        public IActionResult GetBoard(string gameId)
        {
            return Json(_Data.GetInfoAboutBoard(gameId));
        }

        public IActionResult GetPlayersList()
        {
            Players playersList = new();
            return Json(playersList.PlayerList);
        }

        public IActionResult GetVersionBoard(string gameId)
        {
            return Json(_Data.GetVersionBoard(gameId));
        }

        public IActionResult WhoseTurnIsIt(string gameId)
        {
            return Json(_Data.WhoseTurnIsIt(gameId));
        }

        public IActionResult playerWhoCreatedGame(string gameId)
        {
            return Json(_Data.playerWhoCreatedGame(gameId));
        }

        public IActionResult playerWhoJoined(string gameId)
        {
            return Json(_Data.playerWhoJoined(gameId));
        }


        //Timer and ping of game
        public IActionResult PingPlayer(string gameId, string playerId, int date)
        {
            _Data.PingPlayer(gameId, playerId, date);
            return Json(_Data.CurrentDate);
        }

        public IActionResult Timer(string gameId, string playerColor)
        {
            return Json(_Data.Timer(gameId, playerColor));
        }

        public IActionResult IsKingOrRookMoved(string gameId, string color)
        {
            int index = _Data.GameManagerList.FindIndex(a => a.GameId == gameId);
            if (index == -1)
            {
                return null;
            }

            if (color == PieceColor.White.ToString())
            {
                return Json(_Data.GameManagerList[index].IsWhiteKingOrRookMoved);
            }
            else if (color == PieceColor.Black.ToString())
            {
                return Json(_Data.GameManagerList[index].IsBlackKingOrRookMoved);
            }
            return null;
        }


        public IActionResult PlayerPage()
        {
            return View();
        }

        public IActionResult PlayerPageMobile()
        {
            return View();
        }
        public IActionResult GameManagerPage()
        {
            return View();
        }

        public IActionResult GameManagerPageMobile()
        {
            return View();
        }
        public IActionResult GamePage()
        {
            return View();
        }

        public IActionResult GameView()
        {
            return View();
        }

        public IActionResult GamePageMobile()
        {
            return View();
        }

        public IActionResult Pc()
        {
            return View();
        }

    }
}
