/// <reference path="../Views/Oprettelse/OprettelsePersonale.cshtml" />
$(document).ready(function () {




  

    $('#afdelinger').change(function () {

        $("#postkasser").empty();
        $("#grupper").empty();
       
        var afdeling = this.value;
        var område = $('#område').val();

        var serviceURL = '/Ajax/HentPostkasserOgGrupper';

        
        $.ajax({
            type: "GET",
            url: serviceURL,
            data: { afdeling: afdeling, område: område },
            contentType: "text/plain",
            dataType: "json",
            success: successFunc,
            error: errorFunc
        });
        function successFunc(data, status) {

            var grupper = data.Grupper;
            var postkasser = data.Postkasser;
          

            $.each(postkasser, function (index, element) {
         
            $("#postkasser").append($("<option></option>").attr("value", element).text(element));
            });


            $.each(grupper, function (index, element) {

                $("#grupper").append($("<option></option>").attr("value", element).text(element));

            });

        }

        function errorFunc() {
            alert('error');
        }

    });
});