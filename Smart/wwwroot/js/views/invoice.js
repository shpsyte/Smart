var taxId = $("#TaxOperationId option:selected").val();
var locationId = $("#WarehouseId option:selected").val();



$('#CustomerName').autocomplete({
    source: function (request, response) {
        $.ajax({
            url: $('#CustomerName').attr('data-url'),
            type: "POST",
            dataType: "json",
            data: { name: request.term },
            headers:
            {
                "RequestVerificationToken": $('#CustomerName').attr('data-token')
            },
            success: function (data) {

                response($.map(data, function (item) {

                    return {
                        label: item.customerId + ", " + item.firstName + (item.lastName ? ", " + item.lastName : ""),
                        value: item.customerId
                    }
                }));
            }
        })
    },
    //  appendTo: "#result",
    select: function (event, ui) {
        $("#CustomerId").val(ui.item ? ui.item.value : "");
        $("#CustomerName").val(ui.item ? ui.item.label : "");
    },
    change: function (event, ui) {
        $("#CustomerId").val(ui.item ? ui.item.value : "");
        $("#CustomerName").val(ui.item ? ui.item.label : "");
    },
    messages: {
        noResults: "",
        results: function (resultsCount) {
            //console.log(resultsCount);
        }
    }
});



$(document).ready(function () {
    var qt = $('#QtdeProdutos').val();
   
    if (qt == 0) {
        AddLineItem('New');
    }


    $(".alert-dismissible").fadeTo(2000, 500).slideUp(500, function () {
        $(".alert-dismissible").alert('close');
    });

   

    iniciarForm(0);
     
     
});



function AddLineItem(Linha) {
    var nro;

    nro = $('#QtdeProdutos').val();

    if (Linha == nro || Linha == 'New') {




        $("<div id='detailItem" + nro + "' class='form-row'>").insertBefore("#itens");
        html = `<div class="form-row" id=${nro} style="margin-bottom:-28px;">
                        <input id="ProductId_${nro}" name="product[${nro}].ProductId" type="hidden" data-id="${nro}" />
                        <input id="WarehouseId_${nro}" name="product[${nro}].WarehouseId" type="hidden" data-id="${nro}" />
                        <input id="ProductNumber_${nro}" name="product[${nro}].ProductNumber" type="hidden" data-id="${nro}" />
                        <input id="StandartCost_${nro}" name="product[${nro}].StandartCost" type="hidden" data-id="${nro}" />
                        <input id="TaxOperationId_${nro}" name="product[${nro}].TaxOperationId" type="hidden" data-id="${nro}" />

                        <div class="form-group col-md-4">
                            <div class="input-group mb-3">
                                <div class="input-group-prepend">
                                    <span class="input-group-text" id="basic-addon1">${parseInt(nro) + 1}</span >
                                    <a href="javascript:void(0)" onclick="ExcluirLinha(${nro})" class="input-group-text text-danger"  data-code="@seq"  data-tooltip="Remover produto"><i class="far fa-trash-alt"></i></a>
                                </div>
                                <input id="Name_${nro}" name="product[${nro}].Name" data-id="${nro}" 
                                        data-url="/Invoice/GetProduct/"   class="form-control nameProduct${nro}"
                                        autocomplete="on" value="" />
                            </div>
                        </div>

                        
                        <div class="form-group col-md-1">
                            <input id="Qty_${nro}" data-id="${nro}" name="product[${nro}].Qty" class="form-control totalprice" data-thousands="." data-decimal="," data-precision="0" data-allow-zero="true" value="1"  />
                        </div>

                        <div class="form-group col-md-1">
                            <input  id="UnitPrice_${nro}" data-id="${nro}" name="product[${nro}].UnitPrice" class="form-control totalprice" data-thousands="." data-decimal="," data-precision="2" data-allow-zero="true" value="0"  />
                        </div>

                        <div class="form-group col-md-1">
                            <input   id="TaxProduction_${nro}" data-id="${nro}" name="product[${nro}].TaxProduction" class="form-control totalprice" data-thousands="." data-decimal="," data-precision="2" data-allow-zero="true" value="0" />
                        </div>

                        <div class="form-group col-md-1">

                            <input id="TaxSales_${nro}" data-id="${nro}" name="product[${nro}].TaxSales" class="form-control totalprice" data-thousands="." data-decimal="," data-precision="2"  data-allow-zero="true" value="0"/>
                        </div>

                        <div class="form-group col-md-1">
                            <input  id="CodOper_${nro}" data-id="${nro}" name="product[${nro}].CodOper" class="form-control" data-thousands="" data-decimal="" data-precision="0" data-allow-zero="true" value="0"  />
                        </div>
                        <div class="form-group col-md-1">
                            <input   id="Discont_${nro}" data-id="${nro}" name="product[${nro}].Discont" class="form-control totalprice" data-thousands="." data-decimal="," data-precision="2" data-allow-zero="true" value="0" />
                        </div>

                        <div class="form-group col-md-2">
                                <input  id="Total_${nro}" data-id="${nro}" name="product[${nro}].Total" disabled="disabled" class="form-control subtotalitem" data-thousands="." data-decimal="," data-precision="2" data-allow-zero="true" value="0" />
                        </div>
             </div>`;



        var theDiv = document.getElementById("listproduct");

        $(html).appendTo("#listproduct");



        iniciarAutoCompleteProduto(nro);
        iniciarForm(nro);
        $('#Name_' + nro).focus();

        nro++;
        $('#QtdeProdutos').val(nro);
    }
}

function ExcluirLinha(Linha) {
    if (confirm('Voc\u00ea confirma a exclus\xe3o deste item na nota fiscal?')) {
        let nro = 1;
        if (Linha != 'Limpar') {
            nro = Linha;
            $("#" + nro).fadeOut(300, function () { $(this).css("display", "none"); });
        }

        

        $("#ProductId_" + nro).val('0');
        
    }
}
function iniciarAutoCompleteProduto(nro) {
    var element = $('#Name_' + nro);
    var token = $('input[name = "__RequestVerificationToken"').val();

    $('#Name_' + nro).autocomplete({
        source: function (request, response) {
            $.ajax({
                url: '/Invoice/GetProduct/',
                type: "POST",
                dataType: "json",
                data: { name: request.term, TaxOperationID: taxId, id: nro, locationId: locationId },
                headers:
                {
                    "RequestVerificationToken": token
                },
                success: function (data) {

                    response($.map(data, function (item) {

                        return {
                            label: item.name,
                            value: item.productId,
                            id: item.id,
                            seq: item.seq,
                            qty: item.qty,
                            unitprice: item.unitPrice,
                            taxproduction: item.taxProduction,
                            taxsales: item.taxSales,
                            discont: item.discont,
                            codoper: item.codOper,
                            total: item.total,
                            warehouseId: item.warehouseId,
                            productNumber: item.productNumber,
                            standartCost: item.standartCost,
                            taxOperationId: item.taxOperationId



                        }
                    }));
                }
            })
        },
        //appendTo: "#result",
        select: function (event, ui) {

            setValuesOnElement(element, event, ui, nro);
        },
        change: function (event, ui) {
            setValuesOnElement(element, event, ui, nro);
        },
        messages: {
            noResults: "",
            results: function (resultsCount) {
                //console.log(resultsCount);
            }
        }
    });
}


function iniciarForm(nro) {

    $('[data-decimal]').each(function () {
        var $this = $(this);
        $this.maskMoney();

    });

    $(".totalprice").each(function () {
        var $this = $(this);
        $this.on('blur', function () {
            var $this = $(this);
            var id = $this.attr('data-id');
            var unitprice = DesFormataMoeda($("#UnitPrice_" + id).val()) || 0;
            var qty = $("#Qty_" + id).val() || 1;
            var discont = (DesFormataMoeda($("#Discont_" + id).val()) || 0) / 100.00;
            var unitprice = unitprice * ((1.0) - discont);
            var total = unitprice * qty;
            $("#Total_" + id).val(total);

            atualizaSubTotal();

        });
    });




}


function atualizaSubTotal() {
    var subtotal = 0;
    $(".subtotalitem").each(function () {
        var $this = $(this);
        var subitem = parseFloat($this.val());
        subtotal += subitem;
    });

    $("#SubTotal").val(subtotal);
}


function setValuesOnElement(element, event, ui, id) {

    if (ui.item) {

        $("#ProductId_" + id).val(ui.item.value);
        $("#Qty_" + id).val(ui.item.qty);
        $("#UnitPrice_" + id).val(ui.item.unitprice);
        $("#TaxProduction_" + id).val(ui.item.taxproduction);
        $("#TaxSales_" + id).val(ui.item.taxsales);
        $("#Discont_" + id).val(ui.item.discont);
        $("#CodOper_" + id).val(ui.item.codoper);
        $("#Total_" + id).val(ui.item.total);

        $("#WarehouseId_" + id).val(ui.item.warehouseId);
        $("#ProductNumber_" + id).val(ui.item.productNumber);
        $("#StandartCost_" + id).val(ui.item.standartCost);
        $("#TaxOperationId_" + id).val(ui.item.taxOperationId);



        element.val(ui.item.label);
    } else {
        $("#productid_" + id).val("");
        $("#ProductId_" + id).val("");
        $("#Qty_" + id).val("");
        $("#UnitPrice_" + id).val("");
        $("#TaxProduction_" + id).val("");
        $("#TaxSales_" + id).val("");
        $("#Discont_" + id).val("");
        $("#codoper_" + id).val("");
        $("#Total_" + id).val("");

        $("#WarehouseId_" + id).val("");
        $("#ProductNumber_" + id).val("");
        $("#StandartCost_" + id).val("");
        $("#TaxOperationId_" + id).val("");


        element.val("");

    }

}