using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Smart.Services;

namespace Smart.Controllers
{
    public class ComponentsController : BaseController
    {
        public ComponentsController(IUser currentUser, IEmailSender emailSender, IHttpContextAccessor accessor) : base(currentUser, emailSender, accessor)
        {

        }


        public IActionResult CardRevenueStats(bool payed,   DateTime? dueStartDate, DateTime? dueEndDate, int? BankId, string searchTerm, string model, string title, string cssCard)
        {
            object par = new {  payed = payed, dueStartDate = dueStartDate, dueEndDate = dueEndDate, searchTerm = searchTerm, model = model, title = title, cssCard = cssCard };
            return ViewComponent("CardRevenueStats", par);
        }

        public IActionResult CardRevenueTransStats(bool payed, int? signal, DateTime? dueStartDate, DateTime? dueEndDate, int? BankId, string searchTerm, string model, string title, string cssCard)
        {
            object par = new { payed = payed, signal = signal, dueStartDate = dueStartDate, dueEndDate = dueEndDate, searchTerm = searchTerm, model = model, title = title, cssCard = cssCard };
            return ViewComponent("CardRevenueTransStats", par);
        }


        public IActionResult CardExpenseStats(bool payed, DateTime? dueStartDate, DateTime? dueEndDate, int? BankId, string searchTerm, string model, string title, string cssCard)
        {
            object par = new { payed = payed, dueStartDate = dueStartDate, dueEndDate = dueEndDate, searchTerm = searchTerm, model = model, title = title, cssCard = cssCard };
            return ViewComponent("CardExpenseStats", par);
        }


        public IActionResult CardExpenseTransStats(bool payed, int? signal, DateTime? dueStartDate, DateTime? dueEndDate, int? BankId, string searchTerm, string model, string title, string cssCard)
        {
            object par = new { payed = payed, signal = signal, dueStartDate = dueStartDate, dueEndDate = dueEndDate, searchTerm = searchTerm, model = model, title = title, cssCard = cssCard };
            return ViewComponent("CardExpenseTransStats", par);
        }






        public IActionResult CardBankStats(bool payed, DateTime? dueStartDate, DateTime? dueEndDate, int? BankId, string searchTerm, string model, string title, string cssCard)
        {
            object par = new { payed = payed, dueStartDate = dueStartDate, dueEndDate = dueEndDate, searchTerm = searchTerm, BankId = BankId, model = model, title = title, cssCard = cssCard };
            return ViewComponent("CardBankTransStats", par);
        }

        public IActionResult CardBankFixStats(int time, int? BankId, DateTime? refDate,    string model, string title, string cssCard)
        {
            object par = new { time = time,   BankId = BankId, refDate = refDate, model = model, title = title, cssCard = cssCard };
            return ViewComponent("CardBankTransFixStats", par);
        }





        public IActionResult CardCashFlowStats(bool payed, DateTime? dueStartDate, DateTime? dueEndDate, int? BankId, string searchTerm, string model, string title, string cssCard, decimal? initial)
        {
            object par = new { payed = payed, dueStartDate = dueStartDate, dueEndDate = dueEndDate, searchTerm = searchTerm, model = model, title = title, cssCard = cssCard , initial  = initial };
            return ViewComponent("CardCashFlow", par);
        }


    }
}