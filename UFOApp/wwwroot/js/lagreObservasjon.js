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

//tester om man får hentet ut observasjoner
$(document).ready(function () {
    $(function () {
        $.get("Observasjon/HentAlleObservasjoner", function (Observasjoner) {
            console.log(Observasjoner)
        });
    });
});

function finnModellnavn(UFOnavn) {
    $.get("UFO/HentEnUFO?kallenavn=" + UFOnavn, function (UFO) {
        $("#modell").val(UFO.modell);
        
    });
}




//henter ut observatør fra etternavn
function hentEnObservatør(fornavn, etternavn) {
    console.log("prøver get")
    $.get("Observatør/HentEnObservatør?fornavn=" + fornavn + "&etternavn=" + etternavn, function (Observatør) {
        $("#telefon").val(Observatør.telefonObservatør)
        console.log(Observatør)
        //returnerer en observatør
    });
}

/*
//henter ut observatør fra etternavn
function hentEnObservatør(fornavn, etternavn) {
    console.log("prøver get")
    $.get("Observatør/HentEnObservatør", { fornavn, etternavn}).done( function (Observatør) {
        $("#telefon").val(Observatør.telefonObservatør)
        console.log(Observatør)
        //returnerer en observatør
    });
}
*/

function lagreObservasjon() {
    const observasjon = {
        //UFOen
        //kallenavnUFO vil være enten selv-innskrevet eller navnet på en allerede sett UFO
        KallenavnUFO: $("#UFOnavn").val(),
        Modell: $("#modell").val(),
        TidspunktObservert: $("#dato").val(),
        KommuneObservert: $("#kommune").val(),
        BeskrivelseAvObservasjon: $("#beskrivelse").val(),
        // observatør:
        FornavnObservatør: $("#fornavn").val(),
        EtternavnObservatør: $("#etternavn").val(),
        TelefonObservatør: $("#telefon").val(),
        EpostObservatør: $("#epost").val()

    }
    const url = "UFO/LagreObservasjon";
    $.post(url, observasjon, function (OK) {
        if (OK) {
            window.location.href = 'index.html';
        }
        else {
            $("#feil").html("Feil i db ved lagring - prøv igjen senere");
        }
    });
}

//  disabler modell/navn på UFO om man velger en allerede oppdaget UFO i dropdown
$(function () {
    $("#inputform").change(function () {

        // henter ut text fra selected option
        var UFOnavn = $(this).children("option:selected").text();

        if (UFOnavn != "ikke på listen") {
            finnModellnavn(UFOnavn);
            $("#modell").prop("disabled", true);
            $("#UFOnavn").prop("disabled", true);
            $("#UFOnavn").val(UFOnavn)
        }
        else {
            $("#modell").prop("disabled", false);
            $("#UFOnavn").prop("disabled", false);
            $("#UFOnavn").val("")
            $("#modell").val("")
        }
    });
});

//ikke ferdig, sjekk inputfelt og sjekk om get-kallet er rett
$(function () {
    //må tracke endring i etternavn eller fornavn?
    $("#etternavn").change(function () {

        //verdien av etternavn-feltet
        var etternavn = $("#etternavn").val();
        var fornavn = $("#fornavn").val();
        var Observatør = hentEnObservatør(fornavn, etternavn)
        console.log(etternavn)
        console.log("observatør:" + Observatør);
        //hvis en match er funnet med fornavn og etternavn
        if (hentEnObservatør.etternavn == etternavn) {
            $("#telefon").prop("disabled", true);
            $("#epost").prop("disabled", true);
            console.log("inni henten != null");
           // $("#telefon").val(TelefonObservatør);
           // $("#epost").val(EpostObservatør);
        }
        else {
            $("#telefon").prop("disabled", false);
            $("#epost").prop("disabled", false);
            $("#telefon").val("")
            $("#epost").val("")
            console.log("inni henten else");
        }
    });
});