$(document).ready(function () {
    //henter ut UFO-objekter til dropdown list
    $(function () {
        $.get("UFO/HentAlleUFOer", function (UFOer) {
            formaterUFOer(UFOer);
        });
    });

    function formaterUFOer(UFOer) {
        for (let UFO of UFOer) {
            $("#inputform").append('<option>' + UFO.kallenavn + '</option>');
        }
    }
});


function lagreObservasjon() {
    const observasjon = {

        // observasjon:
        /* for å lagre dato+tid i en input er det bare å bruke noe som likner dette
 *  <input type='datetime-local' id='dato'>
 *  Trenger ikke parse eller noe, bare å sende rett til db */
        //UFO = kallenavn
        //        UFO: $("#inputform").val(),
        //hendelse == også kallenavn
        KallenavnUFO: $("#hendelse").val(),
        TidspunktObservert: $("#dato").val(),
        KommuneObservert: $("#kommune").val(),
        BeskrivelseAvObservasjon: $("#beskrivelse").val(),
        Modell: $("#modell").val(),
        // observatør:
        FornavnObservatør: $("#fornavn").val(),
        EtternavnObservatør: $("#etternavn").val(),
        TelefonnummerObservatør: $("#telefonnummer").val(),
        EpostObservatør: $("#epost").val()

    }
    const url = "Observasjon/Lagre";
    $.post(url, observasjon, function (OK) {
        if (OK) {
            window.location.href = 'index.html';
        }
        else {
            $("#feil").html("Feil i db ved lagring - prøv igjen senere");
        }
    });
}


/*
function nyUFO() {
    if ($("#inputform").value = "ikke på listen") {
        $("#nyUfo").show()
    }
}
*/
//hvis inputform-elementet er "noe nytt", lagre modell-verdien i det nye objektet