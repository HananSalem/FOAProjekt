$(document).ready(function () {

    $('#afdelinger').change(function () {


        var navn = this.value;


        var serviceURL = '/Ajax/HentLederPrAfdeling';
        $.ajax({
            type: "GET",
            url: serviceURL,
            data: { afdelingsnavn: navn },
            contentType: "text/plain",
            dataType: "text",
            success: successFunc,
            error: errorFunc
        });
        function successFunc(data, status) {

            $('#leder').val(data);

        }

        function errorFunc() {
            alert('error');
        }

    });
});