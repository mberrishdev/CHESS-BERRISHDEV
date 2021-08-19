
var fromid;
var toid;
var cheker = 0;
var step = 0;
var board;
var ToHorizontal;
var ToVertical;

const ConnectionArray = {
    "King": King,
    "Queen": Queen,
    "Rook": Rook,
    "Bishop": Bishop,
    "Knight": Knight,
    "Pawn": Pawn
};

board = new Board(8, 8);
var piece = new Piece();

function createBoard(boardHorizontalSize, boardverticalSize) {
    var colorFromUrl = window.location.href.split("&")[2];
    if (colorFromUrl == undefined) {
        boardDrowing(boardHorizontalSize, boardverticalSize, 0, 9, -64, 9, ColorWhite);
    } else {

        var colorFromUrl = colorFromUrl.split("=")[1];

    }
    if (colorFromUrl == ColorWhite) {
        boardDrowing(boardHorizontalSize, boardverticalSize, 0, 9, -64, 9, ColorWhite);
    } else if (colorFromUrl == ColorBlack) {
        boardDrowing(boardHorizontalSize, boardverticalSize, 9, 0, 73, 0, ColorBlack);
    }


    $.ajaxSetup({ async: false });
    $.get('/Game/GetBoard',
        { gameId: url.searchParams.get("Game") },
        function (data) {
            boardArray = data;

        });

    for (var index = 0; index < boardArray.length; index++) {

        var pieceNameStart = boardArray[index].name;
        var pieceColorStart = boardArray[index].color;

        var horizontal = boardArray[index].horizontal;
        var vertical = boardArray[index].vertical;

        board.board[vertical - 1][horizontal - 1] = new ConnectionArray[pieceNameStart](pieceNameStart, pieceColorStart, horizontal, vertical);
    }
    addPieceOnBoard(board.board);
}

function mainFunction(startId, name, color) {

    var WherePieceCanGo = null;
    var horizontal = Number(startId.split(":")[0]);
    var vertical = Number(startId.split(":")[1]);
    console.log(horizontal,vertical)

    $.ajaxSetup({ async: false });
    $.get('/Game/WherePieceCanGo',
        { horizontal: horizontal, vertical: vertical, gameId: url.searchParams.get("Game"), color: color },
        function (data) {
            WherePieceCanGo = data;
        });
    return WherePieceCanGo;

}

//Board Update. It check if board is changed or not evey 1 second
var versionBoardOnFront = 0;
setInterval(function () {

    $.ajaxSetup({ async: false });
    $.get('/Game/GetVersionBoard',
        { gameId: url.searchParams.get("Game") },
        function (data) {
            versionBoardOnBack = data;
            if (versionBoardOnFront != versionBoardOnBack) {
                GetBoard(url.searchParams.get("Game"));
                versionBoardOnFront++;
            }
        });


}, 1000);

//Function which GetBoardArray
function GetBoard(gameid) {
    $.ajaxSetup({ async: false });
    $.get('/Game/GetBoard',
        { gameId: gameid },
        function (data) {
            if (data == null) {
                alert(url.searchParams.get("Player") + "is winner. Game has ended.")
                document.location.href = Domain + "Game/PlayerPage";
                return 0;
            }
            boardArray = data;
            removePieceFromBoard();
            addPieceOnBoard(BoardJsonConverterToBoardArray(boardArray));
        });
}

function BoardJsonConverterToBoardArray(JsonBoard) {
    var boardArray = [];
    var supporterArray = [];
    for (var index = 0; index < 8; index++) {

        var supporterArray = [];
        for (var verticalIndex = 0; verticalIndex < 8; verticalIndex++) {
            supporterArray.push(0);
        }
        boardArray.push(supporterArray);
    }

    JsonBoard.forEach(element => {
        var horizontal = element.horizontal
        var vertical = element.vertical;
        var pieceName = element.name;
        var pieceColor = element.color;
        boardArray[vertical - 1][horizontal - 1] = new ConnectionArray[pieceName](pieceName, pieceColor, horizontal, vertical);
    });

    return boardArray;
}

function PossibleMoveArrayConverterToArray(possibleMoveArray) {
    var returnArray = [];

    possibleMoveArray.forEach(element => {
        var supporterArray = [];
        var MoveType;
        console.log(element);
        if (element.moveType == 0) {
            MoveType = "Free";
        } else if (element.moveType == 1) {
            MoveType = "Kill";
        } else if (element.moveType == 2) {
            MoveType = "Castle";
        } else if (element.moveType == 3) {
            MoveType = "SrartPosition";
        } else if (element.moveType == 6) {
            MoveType = "Promotion";
        }
        // if (element.position != null) {
        //     console.log(element.position)
        //     ToHorizontal = element.position.split(":")[0];
        //     ToVertical = element.position.split(":")[1];
        // }
        supporterArray.push(element.toHorizontal);
        supporterArray.push(element.toVertical);
        supporterArray.push(MoveType);
        returnArray.push(supporterArray);
    });

    return returnArray;
}

function createPieceClass(pieceName, horizontalPos, verticalPos, pieceColor) {

    if (pieceName != NoSpace && pieceColor != NoSpace && horizontalPos > 0 && verticalPos > 0) {
        var returnBoard = board.board[verticalPos - 1][horizontalPos - 1].special();
    }

    return returnBoard;
}

function clearArray(array) {
    while (array.length > 0) {
        array.pop();
    }
}

function concatenate(first, second, third) {
    return first + second + third;
}


//givup and draw
function endGame() {
    $.ajaxSetup({ async: false });
    $.get('/Game/EndGame',
        { gameId: url.searchParams.get("Game"), playerId: url.searchParams.get("Player") },
        function (data) {
            alert("Winner is " + data);
        });
    document.location.href = Domain + "Game/PlayerPage";
}

function draw() {
    $.ajaxSetup({ async: false });
    $.get('/Game/draw',
        { gameId: url.searchParams.get("Game"), playerId: url.searchParams.get("Player") },

        function (data) {
            setInterval(function () {
                $.get('/Game/CheckAcceptOrDecline',
                    { gameId: url.searchParams.get("Game") },
                    function (data) {
                        if (data == "True") {
                            alert("Draw!");
                            $.ajaxSetup({ async: false });
                            $.get('/Game/EndGame',
                                { gameId: url.searchParams.get("Game"), playerId: url.searchParams.get("Player") },
                                function (data) {
                                });
                            document.location.href = Domain + "Game/PlayerPage";
                        }
                    }
                )
            }, 1000);
        })

}

//Check if advice is for draw
setInterval(function () {
    $.get('/Game/OfferOfDrawChecker',
        { gameId: url.searchParams.get("Game") },
        function (data) {
            abs = data;
            if (data.item1 == "True") {
                if (confirm(data.item2 + "want DRAW. accept or decline")) {
                    alert("Draw!");
                    $.ajaxSetup({ async: false });
                    $.get('/Game/EndGame',
                        { gameId: url.searchParams.get("Game"), playerId: url.searchParams.get("Player") },
                        function (data) {
                        });
                    document.location.href = Domain + "Game/PlayerPage";
                }
            }
        });
}, 1000);


//function for ping player and check if player is active or not
pingInterval = setInterval(function () {
    $.get('/Game/PingPlayer',
        { gameId: url.searchParams.get("Game"), playerId: url.searchParams.get("Player"), date: Math.round(Date.now() / 1000) }, function (data) {
        });
}, 1000);


//playerWhoCreatedGame
var playerWhoCreatedGame = null;
$.ajaxSetup({ async: false });
$.get('/Game/playerWhoCreatedGame',
    { gameId: url.searchParams.get("Game") },
    function (data) {
        playerWhoCreatedGame = data;
    });

//playerWhoJoined
var playerWhoJoined = null;
$.ajaxSetup({ async: false });
$.get('/Game/playerWhoJoined',
    { gameId: url.searchParams.get("Game") },
    function (data) {
        playerWhoJoined = data;
    });


//function for get information about player has open tab or not
var PlayerIsInGameChecker = setInterval(function () {

    $.ajaxSetup({ async: false });
    $.get('/Game/GetVersionBoard',
        { gameId: url.searchParams.get("Game") },
        function (data) {
            versionBoard = data;
        });
        if (versionBoard == -1) {
            clearInterval(PlayerIsInGameChecker);
            clearInterval(pingInterval);
            alert("White Player is Winner");
            window.location.href = Domain + "Game/PlayerPage";
            return 0;
        }
        if (versionBoard == -2) {
            clearInterval(PlayerIsInGameChecker);
            clearInterval(pingInterval);
            alert("Black Player is Winner");
            window.location.href = Domain + "Game/PlayerPage";
            return 0;
        }
    if (versionBoard == -11) {
        clearInterval(PlayerIsInGameChecker);
        clearInterval(pingInterval);
        alert(playerWhoCreatedGame.playerName + " left the game");
        window.location.href = Domain + "Game/PlayerPage";
        return 0;
    }

    if (versionBoard == -20) {
        clearInterval(PlayerIsInGameChecker);
        clearInterval(pingInterval);
        alert(playerWhoJoined.playerName + " left the game")
        window.location.href = Domain + "Game/PlayerPage";
        return 0;
    }
    if (versionBoard == -30) {
        clearInterval(PlayerIsInGameChecker);
        clearInterval(pingInterval);
        alert("both players left the game")
        window.location.href = Domain + "Game/PlayerPage";
        return 0;
    }
    if (versionBoard == -40) {
        clearInterval(PlayerIsInGameChecker);
        clearInterval(pingInterval);
        alert("game did not start")
        window.location.href = Domain + "Game/PlayerPage";
        return 0;
    }

}, 1000);

// versionboard data meaning
// -11 -- PlayerWhoCreateGame's tab is closed
// -20 -- playerWhoJoined's tab is closed
// -6 -- both players left the game
// -7 -- game did not start


