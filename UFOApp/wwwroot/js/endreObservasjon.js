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

function finnModellnavn(UFOnavn) {
    $.get("UFO/HentEnUFO?kallenavn=" + UFOnavn, function (UFO) {
        $("#modell").val(UFO.modell);

    });
}

$(function () {

    //hent UFO med id fra url og viser denne i skjemaet
    const id = window.location.search.substring(1);
    const url = "UFO/HentEnObservasjon?" + id;

    $.get(url, function (observasjon) {

        //iden

        $("#id").val(observasjon.id);

        //UFOen:

        $("#UFOnavn").val(observasjon.kallenavnUFO);
        $("#modell").val(observasjon.modell);
        $("#dato").val(observasjon.tidspunktObservert);
        $("#kommune").val(observasjon.kommuneObservert);
        $("#beskrivelse").val(observasjon.beskrivelseAvObservasjon);

        //observatør:

        $("#fornavn").val(observasjon.fornavnObservatør);
        $("#etternavn").val(observasjon.etternavnObservatør);
        $("#telefon").val(observasjon.telefonObservatør);
        $("#epost").val(observasjon.epostObservatør);

    });
});

function endreObservasjon() {
    const observasjon = {

        //iden
        id: $("#id").val(),

        //UFO
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

    };

    const url = "UFO/EndreObservasjon"
    $.post(url, observasjon, function () {
        window.location.href = 'index.html';
    })
    .fail(function () {
            $("#feil").html("Feil på server - prøv igjen senere");
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