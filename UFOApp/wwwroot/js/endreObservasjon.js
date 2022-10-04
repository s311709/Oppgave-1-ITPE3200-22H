$(function () {

    //hent kunden med kunde-id fra url og vis denne i skjemaet
    const id = window.location.search.substring(1);
    const url = "UFO/HentEnObservasjon?" + id;

    $.get(url, function (UFO) {

        //iden
        $("#id").val(UFO.id);

        //UFOen:
        $("#UFOnavn").val(UFO.fornavn);
        $("#modell").val(UFO.etternavn);
        $("#dato").val(UFO.id);
        $("#kommune").val(UFO.id);
        $("#beskrivelse").val(UFO.id);

        //observatør:
        $("#fornavn").val(UFO.id);
        $("#etternavn").val(UFO.id);
        $("#telefon").val(UFO.id);
        $("#epost").val(UFO.id);

    });
});

function endreObservasjon() {
    const observasjon = {

        //iden
        id: $("#id").val(),

        //UFOen:
        KallenavnUFO: $("#UFOnavn").val();
        Modell: $("#modell").val();
        TidspunktObservert: $("#dato").val();
        KommuneObservert: $("#kommune").val();
        BeskrivelseAvObservasjon: $("#beskrivelse").val();

        //observatør:
        FornavnObservatør: $("#fornavn").val();
        EtternavnObservatør: $("#etternavn").val();
        TelefonObservatør: $("#telefon").val();
        EpostObservatør: $("#epost").val();

    };

    const url= "UFO/EndreObservasjon"
    $.post(url, observasjon, function (OK) {
        if (OK) {
            window.location.href = 'index.html';
        }
        else {
            $("#feil").html("Feil i db ved endring - prøv igjen senere")
        }
    });
};