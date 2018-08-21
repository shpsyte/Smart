var dueStartDate = $('#dueStartDate').val();
var dueEndDate = $('#dueEndDate').val();

var btnBalance = document.querySelector("#update-balance-btn");
var urlBalance = "../Components/BalanceAccount";
var divBalance = document.querySelector("#balance-account");

var btnSummarize = document.querySelector("#update-summarize-btn");
var urlSummarize = "../Components/SummarizeFinancial";
var divSummarize = document.querySelector("#summarize-financial");

var btnExpense = document.querySelector("#update-expense-btn");
var urlExpense = "../Components/ExpenseAccount";
var divExpense = document.querySelector("#update-expense");

var btnRevenue = document.querySelector("#update-revenue-btn");
var urlRevenue = "../Components/RevenueAccount";
var divRevenue = document.querySelector("#update-revenue");


btnBalance.addEventListener("click", UpdateBalanceAccount);
btnSummarize.addEventListener("click", UpdateSummarize);
btnExpense.addEventListener("click", UpdateExpense);
btnRevenue.addEventListener("click", UpdateRevenue);



function UpdateDashBoard() {
    UpdateBalanceAccount();
    UpdateSummarize();
    UpdateExpense();
    UpdateRevenue();
}


setInterval(UpdateDashBoard, 60000);
UpdateDashBoard();

function UpdateRevenue() {
    let parametro = "top=10&payed=false&dueStartDate=" + dueStartDate + "&dueEndDate=" + dueEndDate + "&RenderView=GetTable";
    UpdateData(btnRevenue, urlRevenue, divRevenue, parametro);
}

function UpdateExpense() {
    let parametro = "top=10&payed=false&dueStartDate=" + dueStartDate + "&dueEndDate=" + dueEndDate + "&RenderView=GetTable";
    UpdateData(btnExpense, urlExpense, divExpense, parametro);
}

function UpdateSummarize() {

    let parametro = "dueStartDate=" + dueStartDate + "&dueEndDate=" + dueEndDate + "&RenderView=GetTable";
    UpdateData(btnSummarize, urlSummarize, divSummarize, parametro);
}

function UpdateBalanceAccount() {
    UpdateData(btnBalance, urlBalance, divBalance, "AccountBankId=nulld&RenderView=GetTable");
}

function UpdateData(btn, url, target, parametros) {
    VisualRefresh(btn, target, true);
    

    var xhr = new XMLHttpRequest();
    xhr.open("POST", url, true);
    xhr.setRequestHeader('Content-type', 'application/x-www-form-urlencoded');
    xhr.addEventListener("load", function (event) {
        if (xhr.status === 200) {
            target.innerHTML = (xhr.responseText);
        }
        else {
            target.innerHTML = "Error for Update" + ": " + xhr.status;
        }
    });
    xhr.send(parametros);

    setTimeout(function () {
        VisualRefresh(btn, target, false)
    }, 2000);

    
}

function VisualRefresh(btn, target, ini) {
    let i = btn.querySelector("i");
    if (ini) {
        i.classList.add("fa-spin");
    } else {
        i.classList.remove("fa-spin");
    }
}