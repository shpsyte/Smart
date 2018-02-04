document.querySelector('#Image').addEventListener("change", previewImages, false);
$(document).ready(function () {


    $('body').on('blur', '#Name', function (event) {

        GetNextCodProduct();
    });

})

function removeimg(e) {
    

    var url = e.getAttribute('data-url');
    var token = e.getAttribute('data-token');
    var id = e.getAttribute('data-id')
    var parametros = { id: id };

     
    


    
        $.ajax({
            type: "POST",
            headers:
            {
                "RequestVerificationToken": token
            },
            url: url,
            data: parametros,
            error: function (data) {
                console.log(data);
            },
            success: function (data) {
                if (data.ok == "ok") {
                    $('#' + id).hide('slow');
                }
            },
            dataType: "json"
        });
    
}



function GetNextCodProduct() {
    var botao = document.getElementById('getCodproduct');
    var url = botao.getAttribute('data-url');
    var token = botao.getAttribute('data-token');
    var parametros = { partialName: $("#Name").val() };
    if ($('#ProductNumber').val() == '') {

        $.ajax({
            type: "POST",
            headers:
            {
                "RequestVerificationToken": token
            },
            url: url,
            data: parametros,
            error: function (data) {
                console.log(data);
            },
            success: function (data) {

                $('#ProductNumber').empty();
                $('#ProductNumber').val(data);
            },
            dataType: "json"
        });
    }

}

