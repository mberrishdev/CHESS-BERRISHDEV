var checker = 0;
var returnBoard;
var previousId;
var startWhereGo;
var activePiece = false;
var activePieceTimer = 0;
var activePieceColor;
var pieceColor = ColorWhite;
var x;
var versionFromServer = 0;
var versionBoard = 0;
var canMove;
var PlayerWhoCreteGameStartTime;
var PlayerWhoJoinGameStartTime;
var GameCanStart = false;

$(document).ready(function () {

    createBoard(8, 8);

    $("td").click(function () {


        if (window.location.href.split("=")[2] == "Spectator") {
            return 0;
        }


        var IsSecondPlayerIsInGame = setInterval(function () {
            $.ajaxSetup({ async: false });
            $.get('/Game/IsSecondPlayerIsInGame',
                { gameId: url.searchParams.get("Game") },
                function (data) {
                    playerData = data;
                });

            if (playerData.length != 0) {
                canMove = true;
                clearInterval(IsSecondPlayerIsInGame);
            }
        }, 500);

        if (canMove == true) {
            onClickFunction(this.id);
        }
    });
});


function endGameTime(playerColor) {
    $.ajaxSetup({ async: false });
    $.get('/Game/RemoveGame',
        { gameId: url.searchParams.get("Game") },
        function (data) {
            //  alert("Winner is " + data);
        });
}

var checkerInterva = setInterval(function () {
    $.ajaxSetup({ async: false });
    $.get('/Game/WhoseTurnIsIt',
        { gameId: url.searchParams.get("Game") },
        function (data) {
            activePieceColor = data;

        })

    if (activePieceColor == ColorWhite) {
        $.ajaxSetup({ async: false });
        $.get('/Game/Timer',
            { gameId: url.searchParams.get("Game"), playerColor: ColorWhite },
            function (data) {
                if (data == -112) {
                    endGameTime(ColorWhite);
                    clearInterval(checkerInterva);
                    data = 0;
                }
                var minutes = Math.floor(data / 60);
                var seconds = data - minutes * 60;
                document.getElementById("player2time").innerHTML = minutes + " : " + seconds;
            });
        document.getElementById("player2").style.backgroundColor = "White";
        document.getElementById("player2").style.color = "Black";
        document.getElementById("player1").style.backgroundColor = "Gray";

    } else {
        $.ajaxSetup({ async: false });
        $.get('/Game/Timer',
            { gameId: url.searchParams.get("Game"), playerColor: ColorBlack },
            function (data) {
                if (data == -911) {
                    endGameTime(ColorWhite);
                    clearInterval(checkerInterva);
                    data = 0;
                }
                var minutes = Math.floor(data / 60);
                var seconds = data - minutes * 60;
                document.getElementById("player1time").innerHTML = minutes + " : " + seconds;
            });

        document.getElementById("player1").style.backgroundColor = "White";
        document.getElementById("player1").style.color = "Black";
        document.getElementById("player2").style.backgroundColor = "Gray";
    }

}, 1000);


function onClickFunction(id) {

    if (document.getElementById(id).firstChild != null) {
        var pieceColor = document.getElementById(id).firstChild.alt.split(Underscore)[0];
    }

    activePieceTimer = false;

    var horizontal = Number(id.split(Colon)[0]);
    var vertical = Number(id.split(Colon)[1]);
    var counter = 0;

    if (checker == 1 && activePiece == true) {
        pieceName = document.getElementById(startId).firstChild.alt.split(Underscore)[1];
        pieceColor = document.getElementById(startId).firstChild.alt.split(Underscore)[0];

        returnBoard = mainFunction(startId, pieceName, pieceColor);

        for (var index = 2; index < returnBoard.length; index++) {
            position = returnBoard[index].toHorizontal + ":" + returnBoard[index].toVertical;

            if (position.indexOf(NoSpace + concatenate(horizontal, Colon, vertical) + NoSpace) == -1
                && position.indexOf(NoSpace + concatenate(horizontal, Colon, vertical) + NoSpace) == -1) {
                counter++;
            }
        }
        if (counter == returnBoard.length - 2) {
            boardColorReset();
            clearArray(possibleMoveArray);
            checker = 0;
            counter = 0;
            activePiece = false;
            return 0;

        }


    }

    if (checker == 1 && activePiece == true) {
        pieceName = document.getElementById(startId).firstChild.alt.split(Underscore)[1];
        pieceColor = document.getElementById(startId).firstChild.alt.split(Underscore)[0];
        horizontalFrom = previousId.split(Colon)[0];
        verticalFrom = previousId.split(Colon)[1];
        horizontalTo = id.split(Colon)[0];
        verticalTo = id.split(Colon)[1];

        returnBoard = mainFunction(startId, pieceName, pieceColor);
        var promotion = false;
        var newPiece = "";

        if (pieceName == PanwType && pieceColor == ColorWhite && verticalFrom == 7 && verticalTo == 8) {

            newPiece = prompt("Which Piece You Want?", "Queen, Rook, Knight, Bishop");
            promotion = true;

        }
        if (pieceName == PanwType && pieceColor == ColorBlack && verticalFrom == 2 && verticalTo == 1) {

            newPiece = prompt("Which Piece You Want?", "Queen, Rook, Knight, Bishop");
            promotion = true;
        }


        var VlidateMove = [];
        $.ajaxSetup({ async: false });
        $.get('/Game/MakeMove',
            { gameId: url.searchParams.get("Game"), fromHorizontal: horizontalFrom, fromVertical: verticalFrom, toHorizontal: horizontalTo, toVertical: verticalTo, Promotion: promotion, NewName: newPiece, pieceColor: pieceColor },
            function (data) {
                VlidateMove = data;
            })


        //King And Rook
        if (previousId == "5:1" && id == "7:1") {
            horizontalFrom = 8;
            verticalFrom = 1;
            horizontalTo = 6;
            verticalTo = 1;
            CastleMove(horizontalFrom, verticalFrom, horizontalTo, verticalTo);
        }
        if (previousId == "5:1" && id == "3:1") {
            horizontalFrom = 1;
            verticalFrom = 1;
            horizontalTo = 4;
            verticalTo = 1;
            CastleMove(horizontalFrom, verticalFrom, horizontalTo, verticalTo);
        }
        if (previousId == "5:8" && id == "7:8") {
            horizontalFrom = 8;
            verticalFrom = 8;
            horizontalTo = 6;
            verticalTo = 8;
            CastleMove(horizontalFrom, verticalFrom, horizontalTo, verticalTo);
        }
        if (previousId == "5:8" && id == "3:8") {
            horizontalFrom = 1;
            verticalFrom = 8;
            horizontalTo = 4;
            verticalTo = 8;
            CastleMove(horizontalFrom, verticalFrom, horizontalTo, verticalTo);
        }
        startid = horizontalFrom + Colon + verticalFrom
        toId = horizontalTo + Colon + verticalTo;
        var AnimationSpeed = 500;

        animate(startId, toId, document.getElementById(startId).firstChild, AnimationSpeed)
        setTimeout(function () {
            removePieceFromBoard();

            addPieceOnBoard(BoardJsonConverterToBoardArray(VlidateMove));
            boardColorReset();
        },
            AnimationSpeed);
        checker = 0;

    } else if (checker == 0 && document.getElementById(id).firstChild != null && activePieceColor == pieceColor) {
        var colorFromUrl = window.location.href.split("&")[2];
        var colorFromUrl = colorFromUrl.split("=")[1];

        returnBoard = mainFunction(id, document.getElementById(id).firstChild.alt.split(Underscore)[1], colorFromUrl);
        startId = id;

        boardColorReset();
        pieceMove(PossibleMoveArrayConverterToArray(returnBoard));

        previousId = id;
        checker = 1;

        activePiece = true;
        activePieceTimer = true;
    }
}

function CastleMove(horizontalFrom, verticalFrom, horizontalTo, verticalTo) {
    $.ajaxSetup({ async: false });
    $.get('/Game/CastleMove',
        { gameId: url.searchParams.get("Game"), fromHorizontal: horizontalFrom, fromVertical: verticalFrom, toHorizontal: horizontalTo, toVertical: verticalTo },
        function (data) {
            VlidateMove = data;
        })

}

function boardDrowing(hor, ver, checkerForhorizontalId, checkerForverticalId, charCode, numCode, view) {

    var numCode = ver + 1;
    var verticalMax = ver + 2;
    var horizontalMax = hor + 1;
    var cheker = 1;
    if (view == ColorBlack) {
        $("#image2").css({ 'top': '85px', "left": "60px" });
        $("#player2").css({ 'top': '66.5px', "left": "150px" });
        $("#player2time").css({ 'top': '90.5px', "left": "192px" });
        $("#image1").css({ 'top': '840px', "left": "60px" });
        $("#player1").css({ 'top': '821.5px', "left": "150px" });
        $("#player1time").css({ 'top': '844.5px', "left": "192px" });
    }

    for (var verticalId = 1; verticalId < verticalMax; verticalId++) {
        //create tr element
        trChild = document.createElement(Tr);
        for (var horizontalId = 0, color = cheker; horizontalId < horizontalMax; horizontalId++, color++) {

            if (horizontalId == 0) {
                if (verticalId == verticalMax - 1) {
                    tdChild = document.createElement(Th);
                    tdChild.id = T01;
                    trChild.append(tdChild);
                    continue;
                }

                tdChild = document.createElement(Th);
                tdChild.id = T01;
                tdChild.innerText = Math.abs((numCode - verticalId));
                trChild.append(tdChild);
                continue;
            }

            if (verticalId == verticalMax - 1 && horizontalId != 0) {
                tdChild = document.createElement(Th);
                tdChild.id = T01;
                tdChild.innerText = String.fromCharCode(Math.abs((charCode - horizontalId)));
                trChild.append(tdChild)
                continue;
            }

            tdChild = document.createElement(Td);

            if (color % 2 == 1) {
                tdChild.className = ColorWhite;
            } else {
                tdChild.className = ColorBlack;
            }

            tdChild.id = Math.abs((checkerForhorizontalId - horizontalId)) + Colon + Math.abs((checkerForverticalId - verticalId));
            trChild.append(tdChild);
        }
        cheker++;
        $(Table).append(trChild);
    }

}

function removePieceFromBoard() {

    for (var horizontal = 1; horizontal < 9; horizontal++) {
        for (var vertical = 1; vertical < 9; vertical++) {

            if (document.getElementById(NoSpace + concatenate(horizontal, Colon, vertical) + NoSpace).firstChild !== null) {

                document.getElementById(NoSpace + concatenate(horizontal, Colon, vertical) + NoSpace).firstChild.remove()
            }
        }
    }

}

function addPieceOnBoard(board) {

    for (var vertical = 0; vertical < 8; vertical++) {
        for (var horizontal = 0; horizontal < 8; horizontal++) {

            if (board[vertical][horizontal] != 0) {
                var pieceName = board[vertical][horizontal].name;
                var pieceColor = board[vertical][horizontal].color;
                var position = board[vertical][horizontal].horizontalCordinate + ":" + board[vertical][horizontal].verticalCordinate;
                drawPieceOnBoard(position, pieceName, pieceColor)
            }
        }
    }
}

function drawPieceOnBoard(positionStart, pieceNameStart, pieceColorStart) {


    var parentId = positionStart;

    var childName = pieceColorStart + pieceNameStart;
    var childAlt = pieceColorStart + Underscore + pieceNameStart;
    var parentElement = document.getElementById(parentId);
    var childElement = document.createElement(Img);

    childElement.src = Location + childName + Png;
    childElement.alt = childAlt;
    childElement.id = "img";
    parentElement.appendChild(childElement);

}

function pieceMove(possibleMoveArray) {

    var color;

    for (var cheker = 1; cheker < possibleMoveArray.length; cheker++) {
        if (possibleMoveArray[cheker] != 0 && possibleMoveArray[cheker] != undefined) {

            var horizontal = possibleMoveArray[cheker][0];
            var vertical = possibleMoveArray[cheker][1];
            console.log(possibleMoveArray[cheker]);
            if (possibleMoveArray[cheker][2] == "SrartPosition") {
                color = "Yellow";
            }
            if (possibleMoveArray[cheker][2] == "Kill") {
                color = "Red";
            }
            if (possibleMoveArray[cheker][2] == "Free") {
                color = "Green";
            }
            if (possibleMoveArray[cheker][2] == "Castle") {
                color = "Green";
            }
            if (possibleMoveArray[cheker][2] == "Promotion") {
                color = "Green";
            }

            if (cheker == 1) {
                changeColor(horizontal, vertical, color);
                continue;
            }

            changeColor(horizontal, vertical, color)
        }
    }

    clearArray(possibleMoveArray);
}

function changeColor(horizontal, vertical, color) {

    if (document.getElementById(concatenate(horizontal, Colon, vertical)) != null) {

        document.getElementById(concatenate(horizontal, Colon, vertical)).style.background = color;
        // if (color == Green) {
        //     document.getElementById(horizontal + Colon + vertical).innerText = String.fromCharCode(64+Number(horizontal)) + Colon + Number(vertical);
        // }
    }
}

function boardColorReset() {

    var blackTable = document.getElementsByClassName(ColorBlack);
    var whiteTable = document.getElementsByClassName(ColorWhite);

    for (var index = 0; index < (8 * 8) / 2; index++) {

        if (blackTable[index] != undefined
            && whiteTable[index] != undefined) {

            if (blackTable[index].style.backgroundColor == "green") {

                blackTable[index].style.backgroundColor = BlackTableColor;

            }

            blackTable[index].style.backgroundColor = BlackTableColor;
            whiteTable[index].style.backgroundColor = WhiteTableColor;
        }
    }
}

function animate(starting, ending, piece, speed) {
    var startingCordinate = starting;
    var endingCordinate = ending;

    var ending = document.getElementById(endingCordinate);
    var starting = document.getElementById(startingCordinate);

    var startCoordinateTop = starting.offsetTop;
    var endCoordinateTop = ending.offsetTop;
    var startCoordinateLeft = starting.offsetLeft;
    var endCoordinateLeft = ending.offsetLeft;

    var colorFromUrl = window.location.href.split("&")[2];
    var colorFromUrl = colorFromUrl.split("=")[1];
    var view = colorFromUrl;

    var lengthTop = endCoordinateTop - startCoordinateTop;
    var lengthLeft = endCoordinateLeft - startCoordinateLeft;

    $(piece).css("position", "relative")
        .animate(
            {
                top: lengthTop,
                left: lengthLeft
            }
            , speed,
            function () {

                $(piece).css("position", "static");

            });


}


var playesrInfromtaionInterval = setInterval(function () {

    $.ajaxSetup({ async: false });
    $.get('/Game/PlayersInformation',
        { gameId: url.searchParams.get("Game") },
        function (data) {
            PlayersInformation = data;
        })

    if (PlayersInformation.player1 == null) {
        document.getElementById("player2").innerText = "Waiting...";
        document.getElementById("img2").src = "../content/Images/Profile.jpg";
    } else {
        var PlayerWhiteName = PlayersInformation.player1;
        document.getElementById("player2").innerText = "White " + PlayerWhiteName;
        document.getElementById("img2").src = "../content/Images/Players/" + PlayerWhiteName + ".jpg";
    }
    if (PlayersInformation.player2 == null) {
        document.getElementById("player1").innerText = "Waiting...";
        document.getElementById("img1").src = "../content/Images/Profile.jpg";
    } else {
        var PlayerBlackName = PlayersInformation.player2;
        document.getElementById("player1").innerText = "White " + PlayerBlackName;
        document.getElementById("img1").src = "../content/Images/Players/" + PlayerBlackName + ".jpg"
    }

    if (PlayersInformation.player2 != null && PlayersInformation.player1 != null) {
        clearInterval(playesrInfromtaionInterval);
    }
}, 1000);
