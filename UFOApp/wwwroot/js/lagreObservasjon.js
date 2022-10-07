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
//$(document).ready(function () {
    function hentAlleObservatører() {
        $.get("UFO/HentAlleObservatører", function (Observatør) {
        });
    };
//});

function finnModellnavn(UFOnavn) {
    $.get("UFO/HentEnUFO?kallenavn=" + UFOnavn, function (UFO) {
        $("#modell").val(UFO.modell);
        
    });
}




//henter ut observatør fra etternavn
function hentEnObservatør(fornavn, etternavn) {
        try {
            $.get("UFO/HentEnObservatør?fornavn=" + fornavn + "&etternavn=" + etternavn, function (Observatør) {
                if (Observatør.fornavn != null) {
                    $("#telefon").val(Observatør.telefon)
                    $("#epost").val(Observatør.epost)
                    $("#telefon").prop("disabled", true);
                    $("#epost").prop("disabled", true);
                    console.log("fant observatør")
                }
                else {
                    console.log("ingen data")
                }
            });
        }
        catch (error) {
            console.log("error")
        }

}


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
    $("#etternavn").change(function () {
        //nullstiller feltene når etternavn endres
        $("#telefon").val("")
        $("#epost").val("")
        $("#telefon").prop("disabled", false);
        $("#epost").prop("disabled", false);

        //henter verdier fra tekstfelt
        var etternavn = $("#etternavn").val();
        var fornavn = $("#fornavn").val();

        //initierer get-kallet som finner observatøren i DB
        var observatør = hentEnObservatør(fornavn, etternavn)

    });
});