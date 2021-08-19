
function JoinGame() {

    var playerName = window.location.href.split("=")[1];

    var gameId = document.getElementById("JoinGameId").firstChild.textContent;
    var enemyColor = document.getElementById("JoinGameColor").firstChild.textContent;

    if (enemyColor == "White") {
        color = "Black";
    } else if (enemyColor == "Black") {
        color = "White";
    }
    $.ajaxSetup({ async: false });
    $.get('/Game/JoinPlayer',
        { gameId: gameId, playerWhoJoined: playerName }, function (data) {
        });

    document.location.href = "GamePage?Game=" + gameId + "&Player=" + playerName + "&Color=" + color;
}

var cheker = 0;

function CreateGame() {

    cheker++;

    if (cheker <= 1) {
        var playerName = window.location.href.split("=")[1];
        var color = document.getElementById("choosColor").value;
        var gameId;

        $.ajaxSetup({ async: false });
        $.get('/Game/CreateGame',
            { playerWhoCreatedGame: playerName, choosenColor: color }, function (data) {
                gameId = data;
            });

        // document.getElementById("MyGame").innerHTML += "<p><a href=" + gameId + ">" + gameId + "</a></p>";
        document.location.href = "GamePage?Game=" + gameId + "&Player=" + playerName + "&Color=" + color;
    }

    else {
        alert("Server Is Full");
    }
}


//Game Where Place Is Empty
setInterval(function () {

    $.ajaxSetup({ async: false });
    $.get('/Game/OnePlayerGameInfo',
        {},
        function (data) {
            playerData = data;
            console.log(playerData)
        });

    document.getElementById("JoinGame").innerHTML = "<h1>Join Game</h1>";
    playerData.forEach(element => {
        console.log(element.pieceColor)
        var Player = element.playerId;
        var PieceColor;
        if (element.pieceColor == 0) {
            PieceColor = ColorWhite;
        }else{
            PieceColor = ColorBlack;
        }
        document.getElementById("JoinGame").innerHTML += "<div id='activeplayers' ><button onclick='JoinGame()'>Click me</button><img src=\'../content/Images/Players/" + Player + ".jpg\' width=\'50%\' height='50%'></img></a><p>" + Player + "</p><p id='JoinGameId'>" + element.gameId + "</p><p id='JoinGameColor'>" + PieceColor + "</p></div>";

    });
}, 1000);


//Current Games
setInterval(function () {

    $.ajaxSetup({ async: false });
    $.get('/Game/CurrentGameInfo',
        {},
        function (data) {
            playerData = data;
        });

    document.getElementById("Currentgames").innerHTML = "<h1>Current games</h1>";

    playerData.forEach(element => {
        var firstPlayer = element.whitePlayerId;
        var secondPlayer = element.blackPlayerId;
        document.getElementById("Currentgames").innerHTML += "<p><a href=\'https://localhost:44348/Game/GamePage?Game=" + element.item3 + "&Player=Spectator'\>" + firstPlayer + " VS " + secondPlayer + "</a></p>";

    });
}, 1000);


//My Games Update
setInterval(function () {
    var playerName = window.location.href.split("=")[1];

    var color = document.getElementById("choosColor").value;
    var gameId;
    $.ajaxSetup({ async: false });
    $.get('/Game/MyGame',
        { PlayerId: playerName },
        function (data) {
            gameId = data;
        });

    if (gameId != undefined) {
        document.getElementById("MyGame").innerHTML = "<h1>My Game</h1>";
        document.getElementById("MyGame").innerHTML += "<p><a href=\'https://localhost:44348/Game/GamePage?Game=" + gameId + "&Player=" + playerName + "&Color=" + color + "'>" + gameId + "</a></p>";
    }
}, 1000);

