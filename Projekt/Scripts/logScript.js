$(document).ready(function () {



    $('#søg').click(function () {

        $("#log tbody").empty();
        $("#log thead").empty();
        var serviceURL = '/Ajax/HentLog';


        $.ajax({
            type: "GET",
            url: serviceURL,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: successFunc,
            error: errorFunc
        });
        function successFunc(data, status) {

            var check = false;

            $.each(data, function (index, element) {

                if (element.områdeId == $("#område :selected").val()) {
                    check = true;

                    $("#log tbody").append(

                    "<tr>" +
                    "<td>" + element.medarbejderInitialer + "</td>" +
                    "<td>" + element.medarbejderNavn + "</td>" +
                    "<td>" + element.status + "</td>" +
                    "<td>" + element.dato + "</td>" +
                    "<td>" + element.initialer + "</td>" +
                    "</tr>"

                        );
                }

            });
            if (check) {
                $("#log thead").append("<tr><th>Initialer</th><th>Navn</th><th>Status</th><th>Dato</th><th>Opdateret af</th></tr>");
            }
            else {
                $("#log tbody").append(
                    "<tr><td>" + "Der er ikke oprettet nogen medarbejder indenfor det valgte område" + "</td></tr>"
                    );
            }


        }

        function errorFunc(xhr, status, error) {
            alert(xhr.responseStatus);
        }


    });

});