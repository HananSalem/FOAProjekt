
$(document).ready(function ()
{


      $('#afdelinger').change(function () {


          console.log(this.value);

          var navn = this.value;
   

    var serviceURL = '/Ajax/GetAfdelingsNummer';
    $.ajax({
        type: "GET",
        url: serviceURL,
        data: {afdelingsnavn : navn},
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
      






});
