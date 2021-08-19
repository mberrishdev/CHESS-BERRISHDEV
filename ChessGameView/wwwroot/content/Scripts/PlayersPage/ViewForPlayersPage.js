
function viewInformation(playerInformation) {
    for (var index in playerInformation) {
        var playerId = playerInformation[index].playerId;
        var name = playerInformation[index].name;
        var born = playerInformation[index].born;
        var age = playerInformation[index].age;
        var country = playerInformation[index].country;
        
        name = name.split(Space)[0]+name.split(Space)[1];

        document.getElementById(playerId).innerHTML += "<a id=\'link'\ href=\'https://localhost:44348/Game/GameManagerPage?playerName=" + name + "'\ ><img src=\'../content/Images/Players/" + name + ".jpg\' width=\'90%\'></img></a>";
        document.getElementById(playerId).innerHTML += "<h2><a id=\'link'\ href=\'https://localhost:44348/Game/GameManagerPage?playerName=" + name + "'\ >" + name + "</a></h2><p>Born:" + born + "</p><p>Age:" + age + "</p><p>country:" + country + "</p>";
    }
}

