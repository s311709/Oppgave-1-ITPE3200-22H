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