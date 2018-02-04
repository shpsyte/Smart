

var payed;
var dueEndDate;
var search;
var BankId;
var parametros;
var payed;
var signal;
var bankamount;

$("#bpanel").click(function () {
    $("#mins").collapse(!$("#painelStats").hasClass("show") ? 'hide' : 'show');
    $(this).text(function (i, old) {
        return old == 'Panel +' ? 'Panel -' : 'Panel +';
    });
});


$(document).ready(function () {
    //setInterval(function () {
    //    refreshStats();
    //}, 10000);
    

    $("#refresh").click(function () {
        refreshStats();
    });
});


function loadVars(model, initial) {
    dueStartDate = $('#dueStartDate').val();
    dueEndDate = $('#dueEndDate').val();
    search = $('#search').val();
    BankId = $('#BankId option:selected').val();
    payed = $('#payed').is(":checked");
    signal = 2;
    parametros = { signal: signal, payed: payed, dueStartDate: dueStartDate, dueEndDate: dueEndDate, BankId: BankId, searchTerm: search, model: model, title: '', cssCard: '', initial: initial };
    
}
function refreshStats() {
    loadrevenueValue();
    loadrexpenseValue();
    loadBankValue();
    loadCashValue();
}
function loadrevenueValue() {
    loadVars('SimpleTableRow');
    if (payed) {
        $("#ListRevenue").load("../Components/CardRevenueTransStats", parametros, function () {
            $("#revenueValue").empty();
            $("#revenueValue").append($("#valueidRevenue").val());
        });
    } else {
        $("#ListRevenue").load("../Components/CardRevenueStats", parametros, function () {
            $("#revenueValue").empty();
            $("#revenueValue").append($("#valueidRevenue").val());
        });
    }
}
function loadrexpenseValue() {
    loadVars('SimpleTableRow');
    if (payed) {
        $("#ListExpense").load("../Components/CardExpenseTransStats", parametros, function () {
            $("#expenseValue").empty();
            $("#expenseValue").append($("#valueidExpense").val());
        });
    } else {
        $("#ListExpense").load("../Components/CardExpenseStats", parametros, function () {
            $("#expenseValue").empty();
            $("#expenseValue").append($("#valueidExpense").val());
        });
    }
}
function loadBankValue() {
    
    loadVars('SimpleTableRow');
    $("#ListBank").load("../Components/CardBankStats", parametros, function () {
        $("#BankValue").empty();
        $("#BankValue").append($("#valueidbank").val());
    });
}
function loadCashValue() {
    dueStartDate = $('#dueStartDate').val();
    dueEndDate = $('#dueEndDate').val();
    search = $('#search').val();
    BankId = $('#BankId option:selected').val();
    payed = $('#payed').is(":checked");
    signal = 2;
    parametros = { time: 0, refDate: dueStartDate, BankId: BankId, model: 'SimpleNumber', title: '', cssCard: '' };
    $("#ListBank").load("../Components/CardBankFixStats", parametros, function () {
        $("#BankValueAmountLate").empty();
        $("#BankValueAmountLate").append($("#valueidbankfix").val());



        $("#ListCash").empty();
        $("#ListCash").text('');

        //bankamount = bankamount.replace('R$', '');
        
        var inicial = $("#valueidbankfix").val();
        inicial = inicial.replace('R$', '');
        //$("#valueidbankfix").val()

        loadVars('SimpleTableRow', inicial);
        if (payed == true) {
            $("#ListCash").text('No Data');
        } else {
            $("#ListCash").load("../Components/CardCashFlowStats", parametros);
        }


    });
}
 