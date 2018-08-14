using Core.Domain.Finance.Views;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using Smart.Helpers;
using Smart.Models.Components;
using Smart.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smart.ViewsComponents.Financial
{

    public class CardExpenseTransStats : ViewComponent
    {
        private readonly IServices<VExpenseTrans> _vExpenseServices;

        public CardExpenseTransStats(IServices<VExpenseTrans> vExpenseServices)
        {
            this._vExpenseServices = vExpenseServices;
        }

        public async Task<IViewComponentResult> InvokeAsync(bool payed, int? signal, DateTime? dueStartDate, DateTime? dueEndDate, string searchTerm, string model, string title, string cssCard)
        {
            var Expense = await  _vExpenseServices.QueryAsync();


            Expense = Expense.Where(a => a.DueDate.HasValue == true);

            if (signal.HasValue)
                Expense = Expense.Where(a => a.Signal == signal.Value);

            if (dueStartDate.HasValue)
                Expense = Expense.Where(a => a.DueDate.Value.Date >= dueStartDate.Value.Date);

            if (dueEndDate.HasValue)
                Expense = Expense.Where(a => a.DueDate.Value.Date <= dueEndDate.Value.Date);

            if (!string.IsNullOrEmpty(searchTerm))
                Expense = Expense.Where(a => a.Name.Contains(searchTerm));


            CardFinancialModel data = ReturnDataModel(payed, model, title, cssCard, Expense);

            return View(data);
        }



        private static CardFinancialModel ReturnDataModel(bool payed, string model, string title, string cssCard, IQueryable<VExpenseTrans> Expense)
        {

            var data = new CardFinancialModel
            {
                Qty = Expense.Count(),
                ExpenseTrans = Expense.Include(a => a.CategoryFinancial),
                Amount = Expense.Sum(a => a.Total),
                payed = payed,
                HtmlModel = model,
                title = title,
                cssCard = cssCard
            };


            return data;




        }


    }
}
