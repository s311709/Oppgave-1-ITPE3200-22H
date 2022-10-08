$(function () {
    $.get("UFO/HentAlleUFOer", function (UFOer) {
        formaterUFOer(UFOer);
    });
});

function formaterUFOer(UFOer) {
    let ut = "<table class='table table-striped'>" +
        "<tr>" +
        "<th>ID</th><th>Kallenavn</th>" +
        "<th>Modell</th><th>Antall Observasjoner</th><th>Sist observert</th>" +
        "</tr>";
    for (let UFO of UFOer) {
        ut += "<tr>" +
            "<td>" + UFO.id + "</td>" +
            "<td>" + UFO.kallenavn + "</td>" +
            "<td>" + UFO.modell + "</td>" +
            "<td>" + UFO.gangerObservert + "</td>" +
            "<td>" + UFO.sistObservert + "</td>" +
            "</tr>";
    }
    ut += "</table>";
    $("#UFOer").html(ut);
}
