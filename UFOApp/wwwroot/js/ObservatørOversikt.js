$(function () {
    $.get("UFO/HentAlleObservatører", function (Observatører) {
        formaterUFOer(Observatører);
    });
});

function formaterUFOer(Observatører) {
    let ut = "<table class='table'>" +
        "<tr>" +
        "<th>Id</th><th>Fornavn</th><th>Etternavn</th>" +
        "<th>Telefon</th><th>Epost</th>" +
        "<th>Antall observasjoner</th><th>Siste observasjon</th>" +
        "</tr>";
    for (let Observatør of Observatører) {
        ut += "<tr>" +
            "<td>" + Observatør.id + "</td>" +
            "<td>" + Observatør.fornavn + "</td>" +
            "<td>" + Observatør.etternavn + "</td>" +
            "<td>" + Observatør.telefon + "</td>" +
            "<td>" + Observatør.epost + "</td>" +
            "<td>" + Observatør.antallRegistrerteObservasjoner + "</td>" +
            "<td>" + Observatør.sisteObservasjon + "</td>" +
            "</tr>";
    }
    ut += "</table>";
    $("#Observatører").html(ut);
}
