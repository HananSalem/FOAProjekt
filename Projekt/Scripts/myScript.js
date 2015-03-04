
$(document).ready(function () {


    $('#afdelinger').change(function () {


        console.log(this.value);

        var navn = this.value;


        var serviceURL = '/Ajax/GetAfdelingsNummer';
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

            console.log(data);
            $('#afdelingsnummer').val(data);

        }

        function errorFunc() {
            alert('error');
        }


    });


    $('#tilføj').click(function () {
        $("#td").append('<br/><input type="text" name="postkasse"/>');
    });



    $(function () {
        $("#datepicker").datepicker({ dateFormat: 'dd-mm-yy' });
    });



    $("#oprettelsesform").validate({

        rules: {
            medarbejderNavn: {
                required: true,
                minlength: 2
            },
            initialer: {
                required: true,
                minlength: 4,
                maxlength: 5
            },

            stillingsbetegnelse: {
                required: true
            },

            datepicker: {
                required: true
            },

            fiks: {
                required: true
            },
            a360: {
                required: true
            },

            sektionsleder: {
                required: true
            }
        },
        messages:
            {
                medarbejderNavn: {
                    required: "Indtast venligst medarbejderens navn",
                    minlength: "Navnet skal bestå af minimum 2 karakterer"
                },
                initialer: {
                    required: "Indtast venligst medarbejderens initialer",
                    minlength:  "Initialer skal bestå af minimum 4 karakterer",
                    maxlength: "Initialer må bestå af maximum 5 karakterer",
                },

                stillingsbetegnelse: {
                    required: "Indtast venligst medarbejderens stillingsbetegnelse"
                },

                datepicker: {
                    required: "Vælg venligst en startdato"
                },

                fiks: {
                    required: "Vælg venligst en fiks kode<br/>"
                },
                a360: {
                    required: "Vælg venligst en 360 gruppe(r)<br/>"
                },

                sektionsleder: {
                    required: "Indtast venligst lederens initialer"
                }
                
            
            
            },
        errorPlacement: function (error, element) {
            if (element.attr("type") == "radio" || element.attr("type") == "checkbox") {
                error.insertBefore(element);
            } else {
                error.insertAfter(element);
            }
        }

    }

);

});
