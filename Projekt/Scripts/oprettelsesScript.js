
$(document).ready(function () {

    setAfdeling();
    setFiks();
    setGrupper();
    setUdstyr();
    setFunktionspostkasser();


    $('#tilføjSpecialAdgang').click(function () {
        $("#tdSpecialAdgang").append('<br/><input type="text" name="specialeprogrammer" class="space" />');


    });

 
 


    function setAfdeling()
    {
        var navn = $("#afdelingsnavn").val();
        $("#afdelinger > option").each(function () {
       
            if (this.value == navn)
            {
                $("#afdelinger").val(this.value);
            }
        });

    }

    function setFiks()
    {
        
        var fiksId = $("#fiksId").val();

        $(".fiks").each(function () {
            if (this.value == fiksId)
            {
                this.checked = true;
            }

        });
            
    }

    function setGrupper()
    {
        var jsonString = $('#data').html(),
        jsonValue = (new Function("return( " + jsonString + " );"))();

        if (jsonValue != null) {
            for (var i = 0; i < jsonValue.length; i++) {
                $('#grupper > option').each(function () {
                    if (this.value == jsonValue[i]) {
                        $(this).attr("selected", true);
                    }

                });

            }
        }
    }

    function setUdstyr() {
        var jsonString = $('#data2').html(),
        jsonValue = (new Function("return( " + jsonString + " );"))();

        if (jsonValue != null) {
            for (var i = 0; i < jsonValue.length; i++) {
                $(".udstyr").each(function () {
                    if (this.value == jsonValue[i]) {
                        this.checked = true;
                    }

                });

            }
        }
    }


    function setFunktionspostkasser() {


        var jsonString = $('#data3').html();
        jsonValue = (new Function("return( " + jsonString + " );"))();

        if (jsonValue != null) {
            for (var i = 0; i < jsonValue.length; i++) {
                
                $("#postkasser > option").each(function () {
                    alert(this.value);
                    if (this.value == jsonValue[i]) {
                        $(this).attr("selected", true);
                    }

                });

            }
        }
    }



    //function setFunktionspostkasser() {
       
    //            var jsonString = $('#data3').html(),
    //            jsonValue = (new Function("return( " + jsonString + " );"))();

         
    //            if (jsonValue != null) {
   
    //                $("#postkasseInput").val(jsonValue[0]);
              
    //            for (var i = 1; i < jsonValue.length; i++) {
    //                $("#tdPostKasse").append('<br/><input type="text" id="' + i + '" name="postkasse"/>');
    //                    $("#" + i + "").val(jsonValue[i]);
    //            }
    //            }
        
    //}



    $('#hent360grupper').click(function () {

        $("#grupper").val([]);
        initialer = $("#initialer360").val();
        

        var serviceURL = '/Ajax/Hent360Adgangsgrupper';
        $.ajax({
            type: "GET",
            url: serviceURL,
            data: { initialer: initialer },
            contentType: "text/plain",
            dataType: "json",
            success: successFunc,
            error: errorFunc
        });
        function successFunc(data, status) {

            //looper gennem personens grupper
            $.each(data, function (index, element) {

                $('#grupper > option').each(function () {

                    
                    if (element.indexOf(this.value) != -1)
                    {
                        $(this).attr("selected", true);
                    }
                });
       
            });


        }

        function errorFunc() {
            alert('error');
        }

    });




    $(function () {
        $("#datepicker").datepicker({ dateFormat: 'dd-mm-yy' });
    });




    $('input[name=fiks]:radio').change(function () {

        if ($(this).is(':checked') && $(this).val() == '2') {

            $('#besked').text('En e-mail bliver sendt til A-kasse lederen');


        }

        else {

            $('#besked').text('');
        }

    });

    $.validator.addMethod('letters', function (value) {
        return value.match(/^[a-z]+$/);
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
                maxlength: 5,
                letters: true,
                

                remote: {
                    url: "/Oprettelse/TjekInitialer",
                    type: "post",
                    data: {
                        initialer: function () {
                            return $("#initialer").val();
                        }
                    }
                }
            },

            stillingsbetegnelse: {
                required: true
            },

            afdelinger:
                {
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
            },

            telefon: {
                required: true,
                digits: true
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
                    remote: "Initialet er ikke til rådighed",
                    letters: "Kun små bogstaver mellem a-z er tilladte"
                },

                stillingsbetegnelse: {
                    required: "Indtast venligst medarbejderens stillingsbetegnelse"
                },

                datepicker: {
                    required: "Vælg venligst en startdato"
                },

                afdelinger:
             {
                 required: "Vælg venligst en afdeling"
             },

                fiks: {
                    required: "Vælg venligst en fiks kode<br/>"
                },
                a360: {
                    required: "Vælg venligst en 360 gruppe(r)<br/>"
                },

                sektionsleder: {
                    required: "Indtast venligst lederens initialer"
                },

                telefon: {
                    required: "Indtast venligst medarbejderens telefonnummer",
                    digits: "Indtast venligt et gyldigt nummer."

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
