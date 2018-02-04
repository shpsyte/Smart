class ExpenseJS{
    InitAdd() {
        var botao = document.querySelector('#btGeraParcela');

        botao.addEventListener('click', () => {

            var parametros = { TotalSeq: $("#ExpenseTotalSeq").val(), Value: $("#Total").val(), DataInicial: $("#DueDate").val() };
            var url = botao.getAttribute('data-url');
            var token = botao.getAttribute('data-token');

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
                    $('#tableparcelas').empty();
                    $.each(data, function (i, data) {

                        var vcto = new Date(data.dueDate).toLocaleDateString('pt-BR');
                        //var valor = data.total.toFixed(2).replace(".", ",");
                        var valor = ConverterMoedaDecimais(data.total);


                        var body = "<tr>";
                        body += "<td>" + '<input type="hidden" value="' + data.seq + '" name="parcelas[' + i + '].Seq"/>' + data.seq + "</td>";
                        body += "<td>" + '<input class="form-control datepicker"  type="text" value="' + vcto + '" name="parcelas[' + i + '].DueDate" /></td>"';
                        body += "<td>" + '<input class="form-control" onkeypress="return (soNums(event,\'\'));" onkeydown="Formata(this,20,event,2)" data-thousands="." data-decimal="," type="text" value="' + valor + '" name="parcelas[' + i + '].Total" /></td>"';
                        body += "</tr>";
                        //console.log(body);
                        $('#tableparcelas').append(body);
                    });

                    //data.forEach(function (item) {

                    //    var row = '<tr><td>' + item.seq + '</td><td>' + item.dueDate. + '</td><td>' + item.total + '</td></tr>';
                    //    console.log(item);
                    //    console.log(row);
                    //   $('#tableparcelas tr:last').after(row);
                    //})
                },
                dataType: "json"
            });

        });
    }


    InitList() {
        var original;
        var hasPayDate;
        



        var pay = $("#Payment");
        var payment = document.querySelector('#Payment');
        payment.addEventListener('blur', function (evt) {

            var value = $(this).val();
            var pago = parseFloat(value.replace(".", "").replace(",", "."));
            var orig = parseFloat(original.replace(".", "").replace(",", "."));


            if (pago < orig || orig == 0 || !hasPayDate) {
                document.querySelector("#Active").checked = false;
                

            } else {
                document.querySelector("#Active").checked = true;
              

            }


        });

        payment.addEventListener('change', function (evt) {

            var value = $(this).val();

            var pago = parseFloat(value.replace(",", "."));
            var orig = parseFloat(original.replace(",", "."));


            if (pago < orig || orig == 0 || !hasPayDate) {
                document.querySelector("#Active").checked = false;
            

            } else {
                document.querySelector("#Active").checked = true;
              

            }


        });



        $(document).on("click", ".pay", function () {
            $(".modal-body #ExpenseId").val($(this).data('id'));
            $("#Name").innerHTML = $(this).data('name');
            var valor = ConverterMoedaDecimais($(this).data('value').replace(",", "."));
            $(".modal-body #Payment").val(valor);
            original = $(this).data('value');
            hasPayDate = $(this).data('payday').length > 0;

            if (!hasPayDate) 
              document.querySelector("#Active").checked = false;

            $('#Comment').val($(this).data('name') + ": " + $(this).data('conta'));
            $('#payexpense').modal('show')
        });
    }

    Confirma() {
        var x = confirm("Você deseja pagar esta conta ?");
        if (x)
            return true;
        else
            return false;
    }
}
var expenseJS = new ExpenseJS();
 