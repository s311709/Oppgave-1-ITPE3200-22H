$(function () {

    //hent kunden med kunde-id fra url og vis denne i skjemaet
    const id = window.location.search.substring(1);
    const url = "UFO/HentEnObservasjon?" + id;

    $.get(url, function (observasjon) {

        //iden

        $("#id").val(observasjon.id);

        //UFOen:

        $("#UFOnavn").val(observasjon.KallenavnUFO);
        $("#modell").val(observasjon.Modell);
        $("#dato").val(observasjon.TidspunktObservert);
        $("#kommune").val(observasjon.KommuneObservert);
        $("#beskrivelse").val(observasjon.BeskrivelseAvObservasjon);

        //observatør:

        $("#fornavn").val(observasjon.FornavnObservatør);
        $("#etternavn").val(observasjon.EtternavnObservatør);
        $("#telefon").val(observasjon.TelefonObservatør);
        $("#epost").val(observasjon.EpostObservatør);

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

    const url = "UFO/EndreObservasjon"
    $.post(url, observasjon, function (OK) {
        if (OK) {
            window.location.href = 'index.html';
        }
        else {
            $("#feil").html("Feil i db ved endring - prøv igjen senere")
        }
    });
};