"use strict";

// Aanmaken connectie tussen server en client
var connection = new signalR.HubConnectionBuilder().withUrl("/reversiHub").build();

connection.start().then(function () {
}).catch(function (err) {
    return console.error(err.toString());
});

// Voeg een click event toe aan alle tiles op het bord
const addEventListenersToBoard = function () {
    let tiles = document.getElementsByClassName("tile");
    for (const tile of tiles) {
        tile.addEventListener("click", function (event) {
            let rij = this.style.gridArea.substring(1, 2);
            let kolom = this.style.gridArea.substring(4, 5);
            connection.invoke("DoeZet", rij, kolom).catch(function (err) {
                return console.error(err.toString());
            });

            event.preventDefault();
        });

    };
}

connection.on("UpdateBord", function (bord) {
    let b = $('#board');
    $(b).empty();
    b.append(Game.Template.parseTemplate("board", JSON.parse(bord)));
});

connection.on("AddBoardFunctionality", function() {
    addEventListenersToBoard();
});