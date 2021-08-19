using ChessGameCore.Constants;
using ChessGameCore.pairs;
using ChessGameCore.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;



namespace ChessGameCore.Games
{
    public class GameManager
    {
        public GameManager()
        {
            GameManagerList = new List<Game>();
            Version = 0;
        }

        public List<Game> GameManagerList { get; set; }
        public long CurrentDate { get; set; }
        public string StartTimeForWhoCreate { get; set; }
        public string StartTimeForWhoJoin { get; set; }
        public int VersionBoard { get; set; } = 0;
        public int Version { get; set; }

        public const string Colon = ":";


        //Functions
        public List<Pair> GetInfoAboutBoard(string gameId)
        {
            int index = GameManagerList.FindIndex(a => a.GameId == gameId);
            if (index == -1)
            {
                return null;
            }
            List<Pair> jsonBoard = new();
            MakeBoardPair jsonBoardPair = new();
            foreach (var item in GameManagerList[index].GameBoard.BoardArray)
            {
                if (item != null)
                {
                    jsonBoard.Add(jsonBoardPair.Create(item.Name.ToString(), item.Color.ToString(), item.HorizontalCordinate, item.VerticalCordinate));
                }
            }

            return jsonBoard;
        }


        //Functions which create/join/remove game
        public string CreateGame(string playerWhoCreatedGame, string choosenColor)
        {
            AddToGameList(playerWhoCreatedGame, choosenColor);

            int index = GameManagerList.Count;
            if (index == -1)
            {
                return null;
            }
            GameManagerList[index - 1].GameId = GameManagerList[index - 1].GenerateId();

            return GameManagerList[index - 1].GameId;
        }

        public string JoinPlayer(string gameId, string joinPlayer)
        {
            int index = GameManagerList.FindIndex(a => a.GameId == gameId);

            GameManagerList[index].JoinPlayer(joinPlayer);

            return null;
        }

        public string RemoveGameToList(string gameId)
        {
            int index = GameManagerList.FindIndex(a => a.GameId == gameId);
            if (index != -1)
            {
                GameManagerList[index].IsFinished = true;
                GameManagerList.RemoveAt(index);
            }
            return null;
        }

        public string AddToGameList(string playerWhoMadeGame, string choosenColor)
        {
            if (Enum.TryParse(choosenColor, out PieceColor pieceColor))
            {
                GameManagerList.Add(new Game(playerWhoMadeGame, pieceColor));
                Version++;
                return GameManagerList.Last().GameId;
            }
            return null;
        }


        //End game/ draw
        public string EndGame(string gameId, string playerId)
        {
            int index = GameManagerList.FindIndex(a => a.GameId == gameId);
            if (index == -1)
            {
                return null;
            }



            if (playerId == GameManagerList[index].PlayerWhoMadeGame.PlayerName)
            {
                return GameManagerList[index].PlayerWhoJoined.PlayerName;
            }
            else if (playerId == GameManagerList[index].PlayerWhoJoined.PlayerName)
            {
                return GameManagerList[index].PlayerWhoMadeGame.PlayerName;
            }
            return null;
        }
        public string EndGameColor(string gameId, string playerColor)
        {
            int index = GameManagerList.FindIndex(a => a.GameId == gameId);

            if (index != -1)
            {
                if (GameManagerList[index].PlayerWhoMadeGame?.PlayerColor.ToString() == playerColor)
                {
                    return GameManagerList[index].PlayerWhoMadeGame.PlayerName;
                }
                if (GameManagerList[index].PlayerWhoJoined.PlayerColor.ToString() == playerColor)
                {
                    return GameManagerList[index].PlayerWhoMadeGame.PlayerName;
                }
            }
            return null;
        }


        public string Draw(string gameId, string playerId)
        {
            int index = GameManagerList.FindIndex(a => a.GameId == gameId);
            if (index == -1)
            {
                return null;
            }
            GameManagerList[index].OfferofDraw = true;
            GameManagerList[index].WhoOfferofDraw = playerId;
            return null;
        }

        public Tuple<string, string> OfferOfDrawChecker(string gameId)
        {
            int index = GameManagerList.FindIndex(a => a.GameId == gameId);
            if (index != -1)
            {
                Tuple<string, string> data = new(GameManagerList[index].OfferofDraw.ToString(), GameManagerList[index].WhoOfferofDraw);;
                return data;
            }
            return null;
        }

        public string AcceptofDraw(string gameId)
        {
            int index = GameManagerList.FindIndex(a => a.GameId == gameId);
            if (index != -1)
            {
                GameManagerList[index].AcceptofDraw = true;
                return null;
            }
            return null;
        }

        public string CheckAcceptOrDecline(string gameId)
        {
            int index = GameManagerList.FindIndex(a => a.GameId == gameId);
            if (index != -1)
            {
                return GameManagerList[index].AcceptofDraw.ToString();
            }
            return null;
        }


        //Functions which give us information about game
        public List<Pair> CurrentGameInfo()
        {
            List<Pair> data = new();
            MakeCurrentGameInfoPair CurrentGameInfo = new();
            foreach (var game in GameManagerList)
            {
                if (game.WhitePlayerId != null && game.BlackPlayerId != null && !game.IsFinished)
                {
                    data.Add(CurrentGameInfo.Create(game.WhitePlayerId, game.BlackPlayerId, game.GameId));
                }
            }

            return data;
        }

        public List<Pair> GameManagerListInfromation()
        {
            List<Pair> data = new();
            MakeCurrentGameInfoPair CurrentGameInfo = new();

            foreach (var item in GameManagerList)
            {
                data.Add(CurrentGameInfo.Create(item.WhitePlayerId, item.BlackPlayerId, item.GameId));
            }
            return data;
        }

        public List<Pair> OnePlayerGameInformation()
        {

            List<Pair> data = new();
            MakeOnePlayerGameInformation OnePlayerGameInformation = new();
            foreach (var item in GameManagerList)
            {
                if (item.WhitePlayerId == null || item.BlackPlayerId == null)
                {
                    if (item.WhitePlayerId == null && item.BlackPlayerId == null)
                    {
                        data.Add(null);
                    }
                    if (item.WhitePlayerId == null)
                    {
                        data.Add(OnePlayerGameInformation.Create(item.BlackPlayerId, PieceColor.Black, item.GameId));
                    }
                    if (item.BlackPlayerId == null)
                    {
                        data.Add(OnePlayerGameInformation.Create(item.WhitePlayerId, PieceColor.White, item.GameId));
                    }
                }
            }

            return data;
        }

        public string MyGame(string PlayerId)
        {
            foreach (var item in GameManagerList)
            {
                if (item.WhitePlayerId == PlayerId || item.BlackPlayerId == PlayerId)
                {
                    return item.GameId;
                }
            }
            return null;
        }

        public List<Pair> IsSecondPlayerIsInGame(string gameId)
        {
            List<Pair> data = new();
            MakeCurrentGameInfoPair CurrentGameInfo = new();
            int index = GameManagerList.FindIndex(element => element.GameId == gameId);
            if (index == -1)
            {
                return null;
            }

            if (GameManagerList[index].WhitePlayerId != null && GameManagerList[index].BlackPlayerId != null)
            {
                data.Add(CurrentGameInfo.Create(GameManagerList[index].WhitePlayerId, GameManagerList[index].BlackPlayerId, GameManagerList[index].GameId));
            }

            return data;
        }

        public int? GetVersionBoard(string gameId)
        {
            int index = GameManagerList.FindIndex(a => a.GameId == gameId);
            if (index == -1)
            {
                return null;
            }
            return GameManagerList[index].VersionBoard;
        }

        public string WhoseTurnIsIt(string gameId)
        {
            int index = GameManagerList.FindIndex(a => a.GameId == gameId);
            if (index == -1)
            {
                return null;
            }
            return GameManagerList[index].WhoseTurnIsIt.ToString();
        }

        public PlayerClass playerWhoCreatedGame(string gameId)
        {
            int index = GameManagerList.FindIndex(a => a.GameId == gameId);
            if (index == -1)
            {
                return null;
            }
            return GameManagerList[index].PlayerWhoMadeGame;
        }

        public PlayerClass playerWhoJoined(string gameId)
        {
            int index = GameManagerList.FindIndex(a => a.GameId == gameId);
            if (index == -1)
            {
                return null;
            }
            return GameManagerList[index].PlayerWhoJoined;
        }


        //Fucntions for moves
        public List<Pair> WherePieceCanGO(int horizontal, int vertical, string gameId, string color)
        {

            int index = GameManagerList.FindIndex(a => a.GameId == gameId);
            if (index == -1)
            {
                return null;
            }
            if (color != GameManagerList[index].WhoseTurnIsIt.ToString())
            {
                return null;
            }
            return GameManagerList[index].GameBoard.BoardArray[vertical - 1, horizontal - 1].MoveFunction(Checker.CheckForDanger, Checker.CheckForKingUnderDanger);
        }

        private Dictionary<string, PieceFactory> _pieceFactories = new()
        {
            { "Queen", new QueenFactory() },
            { "King", new KingFactory() },
            { "Rook", new RookFactory() },
            { "Knight", new KnightFactory() },
        };

        public List<Pair> IsMoveValidate(string gameId, int fromHorizontal, int fromVertical, int toHorizontal,
                                         int toVertical, bool Promotion, string NewName, string pieceColor)
        {

            int index = GameManagerList.FindIndex(element => element.GameId == gameId);

            if (index == -1)
            {
                return null;
            }

            string a = toHorizontal.ToString() + Colon + toVertical.ToString();

            var positionIndex = GameManagerList[index].GameBoard.BoardArray[fromVertical - 1, fromHorizontal - 1]
                .MoveFunction(Checker.CheckForDanger, Checker.CheckForKingUnderDanger).FindIndex(element => element.ToHorizontal == toHorizontal && element.ToVertical == toVertical);


            if (positionIndex != -1)
            {
                GameManagerList[index].VersionBoard++;


                //this 2 if check if rook or king is already moved
                if (GameManagerList[index].GameBoard.BoardArray[fromVertical - 1, fromHorizontal - 1].Name == PieceType.King
                    && GameManagerList[index].GameBoard.BoardArray[fromVertical - 1, fromHorizontal - 1].Color == PieceColor.White
                    || GameManagerList[index].GameBoard.BoardArray[fromVertical - 1, fromHorizontal - 1].Name == PieceType.Rook
                    && GameManagerList[index].GameBoard.BoardArray[fromVertical - 1, fromHorizontal - 1].Color == PieceColor.White
                    )
                {
                    GameManagerList[index].IsWhiteKingOrRookMoved = true;
                }
                if (GameManagerList[index].GameBoard.BoardArray[fromVertical - 1, fromHorizontal - 1].Name == PieceType.King
                    && GameManagerList[index].GameBoard.BoardArray[fromVertical - 1, fromHorizontal - 1].Color == PieceColor.Black
                    || GameManagerList[index].GameBoard.BoardArray[fromVertical - 1, fromHorizontal - 1].Name == PieceType.Rook
                    && GameManagerList[index].GameBoard.BoardArray[fromVertical - 1, fromHorizontal - 1].Color == PieceColor.Black
                    )
                {
                    GameManagerList[index].IsBlackKingOrRookMoved = true;
                }



                if (GameManagerList[index].GameBoard.BoardArray[fromVertical - 1, fromHorizontal - 1].Color == PieceColor.White)
                {
                    GameManagerList[index].WhoseTurnIsIt = PieceColor.Black;
                }
                else if (GameManagerList[index].GameBoard.BoardArray[fromVertical - 1, fromHorizontal - 1].Color == PieceColor.Black)
                {
                    GameManagerList[index].WhoseTurnIsIt = PieceColor.White;
                }

                // VersionBoard++;
                //change cordinates
                GameManagerList[index].GameBoard.BoardArray[fromVertical - 1, fromHorizontal - 1].HorizontalCordinate = toHorizontal;
                GameManagerList[index].GameBoard.BoardArray[fromVertical - 1, fromHorizontal - 1].VerticalCordinate = toVertical;



                //change piece in BoardArray
                GameManagerList[index].GameBoard.BoardArray[toVertical - 1, toHorizontal - 1] = GameManagerList[index].GameBoard.BoardArray[fromVertical - 1,
                    fromHorizontal - 1];




                if (Promotion)
                {
                    if (NewName == PieceType.Queen.ToString())
                    {
                        if (pieceColor == PieceColor.White.ToString())
                        {
                            GameManagerList[index].GameBoard.BoardArray[toVertical - 1, toHorizontal - 1] = new Queen(GameManagerList[index].GameBoard,
                                PieceColor.White, toHorizontal, toVertical);
                        }
                        else
                        {
                            GameManagerList[index].GameBoard.BoardArray[toVertical - 1, toHorizontal - 1] = new Queen(GameManagerList[index].GameBoard,
                                PieceColor.Black, toHorizontal, toVertical);
                        }
                    }
                    if (NewName == PieceType.Rook.ToString())
                    {
                        if (pieceColor == PieceColor.White.ToString())
                        {
                            GameManagerList[index].GameBoard.BoardArray[toVertical - 1, toHorizontal - 1] = new Rook(GameManagerList[index].GameBoard,
                                PieceColor.White, toHorizontal, toVertical);
                        }
                        else
                        {
                            GameManagerList[index].GameBoard.BoardArray[toVertical - 1, toHorizontal - 1] = new Rook(GameManagerList[index].GameBoard,
                                PieceColor.Black, toHorizontal, toVertical);
                        }
                    }
                    if (NewName == PieceType.Bishop.ToString())
                    {
                        if (pieceColor == PieceColor.White.ToString())
                        {
                            GameManagerList[index].GameBoard.BoardArray[toVertical - 1, toHorizontal - 1] = new Bishop(GameManagerList[index].GameBoard,
                                PieceColor.White, toHorizontal, toVertical);
                        }
                        else
                        {
                            GameManagerList[index].GameBoard.BoardArray[toVertical - 1, toHorizontal - 1] = new Bishop(GameManagerList[index].GameBoard,
                                PieceColor.Black, toHorizontal, toVertical);
                        }
                    }
                    if (NewName == PieceType.Knight.ToString())
                    {
                        if (pieceColor == PieceColor.White.ToString())
                        {
                            GameManagerList[index].GameBoard.BoardArray[toVertical - 1, toHorizontal - 1] = new Knight(GameManagerList[index].GameBoard,
                                PieceColor.White, toHorizontal, toVertical);
                        }
                        else
                        {
                            GameManagerList[index].GameBoard.BoardArray[toVertical - 1, toHorizontal - 1] = new Knight(GameManagerList[index].GameBoard,
                                PieceColor.Black, toHorizontal, toVertical);
                        }
                    }
                }

                //clear from positions
                GameManagerList[index].GameBoard.BoardArray[fromVertical - 1, fromHorizontal - 1] = null;

                List<Pair> jsonBoard = new();
                MakeBoardPair jsonBoardPair = new();

                foreach (var item in GameManagerList[index].GameBoard.BoardArray)
                {
                    if (item != null)
                    {
                        jsonBoard.Add(jsonBoardPair.Create(item.Name.ToString(), item.Color.ToString(), item.HorizontalCordinate, item.VerticalCordinate));
                    }
                }

                if (GameManagerList[index].GameBoard.BoardArray[toVertical - 1, toHorizontal - 1].Mate(pieceColor))
                {
                    if (pieceColor == PieceColor.White.ToString())
                    {
                        GameManagerList[index].IsFinished = true;
                        GameManagerList[index].VersionBoard = -1;
                    }
                    else
                    {
                        GameManagerList[index].IsFinished = true;
                        GameManagerList[index].VersionBoard = -2;
                    }
                }
                return jsonBoard;
            }
            return null;
        }

        public List<Pair> CastleMove(string gameId, int fromHorizontal, int fromVertical, int toHorizontal,
                                     int toVertical)
        {
            int index = GameManagerList.FindIndex(element => element.GameId == gameId);
            if (index == -1)
            {
                return null;
            }

            string a = toHorizontal.ToString() + Colon + toVertical.ToString();
            var positionIndex = GameManagerList[index].GameBoard.BoardArray[fromVertical - 1, fromHorizontal - 1].MoveFunction(Checker.CheckForDanger,
                                                                                                                               Checker.CheckForKingUnderDanger).FindIndex(element => element.ToHorizontal == toHorizontal && element.ToVertical == toVertical);



            GameManagerList[index].VersionBoard++;

            if (GameManagerList[index].GameBoard.BoardArray[fromVertical - 1, fromHorizontal - 1].Color == PieceColor.White)
            {
                GameManagerList[index].WhoseTurnIsIt = PieceColor.Black;
            }
            else if (GameManagerList[index].GameBoard.BoardArray[fromVertical - 1, fromHorizontal - 1].Color == PieceColor.Black)
            {
                GameManagerList[index].WhoseTurnIsIt = PieceColor.White;
            }

            // VersionBoard++;
            //change cordinates
            GameManagerList[index].GameBoard.BoardArray[fromVertical - 1, fromHorizontal - 1].HorizontalCordinate = toHorizontal;
            GameManagerList[index].GameBoard.BoardArray[fromVertical - 1, fromHorizontal - 1].VerticalCordinate = toVertical;

            //change piece in BoardArray
            GameManagerList[index].GameBoard.BoardArray[toVertical - 1, toHorizontal - 1] = GameManagerList[index].GameBoard
                                                                                                     .BoardArray[fromVertical - 1, fromHorizontal - 1];

            //clear from positions
            GameManagerList[index].GameBoard.BoardArray[fromVertical - 1, fromHorizontal - 1] = null;


            List<Pair> jsonBoard = new();
            MakeBoardPair jsonBoardPair = new();

            foreach (var item in GameManagerList[index].GameBoard.BoardArray)
            {
                if (item != null)
                {
                    jsonBoard.Add(jsonBoardPair.Create(item.Name.ToString(), item.Color.ToString(), item.HorizontalCordinate, item.VerticalCordinate));
                }
            }

            return jsonBoard;

        }


        //Timer and ping of game
        public void PingPlayer(string gameId, string playerId, int date)
        {
            int index = GameManagerList.FindIndex(a => a.GameId == gameId);

            if (index != -1)
            {
                if (GameManagerList[index].PlayerWhoMadeGame?.PlayerName == playerId)
                {
                    GameManagerList[index].PlayerWhoMadeGame.LastPing = date;
                }
                if (GameManagerList[index].PlayerWhoJoined?.PlayerName == playerId)
                {
                    GameManagerList[index].PlayerWhoJoined.LastPing = date;
                }
            }
        }

        public void TimerForTab(int _timeLimitForPlayerToRejoin)
        {
            CurrentDate = (Convert.ToInt64(GetUnixEpoch(DateTime.Now)));

            //foreach (var game in GameManagerList.ToList())
            //{
            //    //check if game has started
            //    if (game.IsStarted && game.PlayerWhoJoined.LastPing > 0)
            //    {
            //        //PlayerWhoCreateGame's tab is closed
            //        if (Math.Abs(game.PlayerWhoMadeGame.LastPing - CurrentDate) > _timeLimitForPlayerToRejoin)
            //        {
            //            Version++;
            //            game.IsFinished = true;
            //            game.VersionBoard = -11;
            //        }

            //        //playerWhoJoined's tab is closed
            //        if (Math.Abs(game.PlayerWhoJoined.LastPing - CurrentDate) > _timeLimitForPlayerToRejoin)
            //        {
            //            Version++;
            //            game.IsFinished = true;
            //            game.VersionBoard = -20;
            //        }

            //        //both players left the game
            //        if (Math.Abs(game.PlayerWhoMadeGame.LastPing - CurrentDate) > _timeLimitForPlayerToRejoin
            //            && Math.Abs(game.PlayerWhoJoined.LastPing - CurrentDate) > _timeLimitForPlayerToRejoin)
            //        {
            //            Version++;
            //            game.IsFinished = true;
            //            game.VersionBoard = -30;
            //        }
            //    }

            //    //game hasnot started
            //    if (!game.IsStarted && game.PlayerWhoMadeGame.LastPing > 0)
            //    {
            //        if (Math.Abs(game.PlayerWhoMadeGame.LastPing - CurrentDate) > _timeLimitForPlayerToRejoin)
            //        {
            //            Version++;
            //            game.IsFinished = true;
            //            game.VersionBoard = -40;
            //        }
            //    }
            //}
        }

        private static double GetUnixEpoch(DateTime dateTime)
        {
            var unixTime = dateTime.ToUniversalTime() -
                new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            return unixTime.TotalSeconds;
        }

        public long Timer(string gameId, string playerColor)
        {
            int index = GameManagerList.FindIndex(a => a.GameId == gameId);

            if (index != -1)
            {
                if (GameManagerList[index].PlayerWhoMadeGame != null && GameManagerList[index].PlayerWhoJoined != null)
                {
                    if (GameManagerList[index].PlayerWhoMadeGame?.PlayerColor.ToString() == playerColor)
                    {
                        long ping = GameManagerList[index].PlayerWhoMadeGame.Ping--;
                        if (ping <= 0)
                        {
                            return -112;
                        }
                        return ping;
                    }
                    if (GameManagerList[index].PlayerWhoJoined.PlayerColor.ToString() == playerColor)
                    {
                        long ping = GameManagerList[index].PlayerWhoJoined.Ping--;
                        if (ping <= 0)
                        {
                            return -911;
                        }
                        return ping;
                    }
                }
            }
            return 0;
        }
    }
}
