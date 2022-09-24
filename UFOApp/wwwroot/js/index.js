$(function () {
    $.get("UFO/HentAlleObservasjoner", function (observasjoner) {
        formaterObservasjoner(observasjoner);
    });
});

function formaterObservasjoner(observasjoner) {
    let ut = "<table class='table'>" +
        "<tr>" +
        "<th>UFO</th><th>Tidspunkt</th>" +
        "<th>Kommune</th><th>Beskrivelse</th><th>Observatør</th><th></th><th></th>" +
        "</tr>";
    for (let observasjon of observasjoner) {
        ut += "<tr>" +
            "<td>" + observasjon.kallenavnUFO + "</td>" +
            "<td>" + observasjon.tidspunktObservert + "</td>" +
            "<td>" + observasjon.kommuneObservert + "</td>" +
            "<td>" + observasjon.beskrivelseAvObservasjon + "</td>" +
            "<td>" + observasjon.fornavnObservatør + " " + observasjon.etternavnObservatør + "</td>" +
            "<td> <a class='btn btn-primary' href='endreObservasjon.html?id=" + observasjon.id + "'>Endre</a></td>" +
            "<td><button class='btn btn-danger' onclick='slettObservasjon(" + observasjon.id + ")'>Slett</button></td>" +
            "</tr>";
    }
    ut += "</table>";
    $("#observasjoner").html(ut);
}
