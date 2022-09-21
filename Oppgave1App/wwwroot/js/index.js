$(function () {
    $.get("oppgave1/HentAlle", function (oppgave1er) {
        formaterOppgaver(oppgave1er);
    });
});

function formaterOppgaver(oppgave1er) {
    let ut = "<table class='table'>" +
        "<tr>" +
        "<th>Test</th>" +
        "</tr>";
    for (let oppgave of oppgave1er) {
        ut += "<tr>" +
            "<td>" + oppgave.info + "</td>" +
            
            "</tr>";
    }
    ut += "</table>";
    $("#test").html(ut);
}
