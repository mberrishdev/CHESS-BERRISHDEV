var object;
var playerInformation = []

var player = new Player();

window.onload = function () {
    $.ajaxSetup({ async: false });
    $.get('/Game/GetPlayersList',
        {}, function (data) {
            viewInformation(data);
        });
}
