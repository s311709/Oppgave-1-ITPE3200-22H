$(function () {

    //hent kunden med kunde-id fra url og vis denne i skjemaet
    const Id = window.location.search.substring(1);
    const url = "UFO/HentEnObservasjon?" + Id;

    $.get(url, function (UFO) {

        //iden
        $("#Id").val(UFO.Id);

        //UFOen:
        $("#UFOnavn").val(UFO.KallenavnUFO);
        $("#modell").val(UFO.Modell);
        $("#dato").val(UFO.TidspunktObservert);
        $("#kommune").val(UFO.KommuneObservert);
        $("#beskrivelse").val(UFO.BeskrivelseAvObservasjon);

        //observatør:
        $("#fornavn").val(UFO.FornavnObservatør);
        $("#etternavn").val(UFO.EtternavnObservatør);
        $("#telefon").val(UFO.TelefonObservatør);
        $("#epost").val(UFO.EpostObservatør);

    });
});

function endreObservasjon() {
    const observasjon = {

        //iden
        Id: $("#Id").val(),

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