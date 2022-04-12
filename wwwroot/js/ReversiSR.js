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

const addEventListenerToPassButton = function () {
    let button = document.getElementById("passButton");
    console.log("click");

    button.addEventListener("click", function (event) {
        connection.invoke("Passen").catch(function (err) {
            return console.error(err.toString());
        });

        event.preventDefault();
    });
}

const addEventListenerToForfeitButton = function () {
    let button = document.getElementById("opgevenButton");
    console.log("click");

    button.addEventListener("click", function (event) {
        connection.invoke("Opgeven").catch(function (err) {
            return console.error(err.toString());
        });

        event.preventDefault();
    });
}

connection.on("UpdateScore", function (aantalWit, aantalZwart) {
    updateScore(aantalWit, aantalZwart);
});

connection.on("UpdateBord", function (bord) {
    updateBord(bord);
});

connection.on("AddBoardFunctionality", function () {
    addEventListenersToBoard();
});

connection.on("DisablePassButton", function () {
    document.getElementById("passButton").disabled = true;
});

connection.on("EnablePassButton", function () {
    document.getElementById("passButton").disabled = false;
});


connection.on("DisableForfeitButton", function () {
    document.getElementById("opgevenButton").disabled = true;
});

connection.on("EnableForfeitButton", function () {
    document.getElementById("opgevenButton").disabled = false;
});

connection.on("Gewonnen", function () {
    let modal = document.getElementById("gewonnenModal");
    modal.showModal();
});

connection.on("Verloren", function () {
    let modal = document.getElementById("verlorenModal");
    modal.showModal();
});

const updateBord = function (bord) {
    let b = $('#board');
    $(b).empty();
    b.append(Game.Template.parseTemplate("board", JSON.parse(bord)));
}

let updateScore = function (aantalWit, aantalZwart) {
    let labelWit = $('#pointsWhite');
    labelWit.text(aantalWit);

    let labelZwart = $('#pointsBlack');
    labelZwart.text(aantalZwart);
}