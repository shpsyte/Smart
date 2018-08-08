using Core.Domain.Finance.Views;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

    public class CardExpenseStats : ViewComponent
    {
        private readonly IServices<VExpense> _vexpenseServices;

        public CardExpenseStats(IServices<VExpense> vexpenseServices)
        {
            this._vexpenseServices = vexpenseServices;
        }

        public async Task<IViewComponentResult> InvokeAsync(bool payed, DateTime? dueStartDate, DateTime? dueEndDate, string searchTerm,  string model, string title, string cssCard)
        {
            var expense =  await _vexpenseServices.QueryAsync();

            if (payed)
            {
                expense = expense.Where(a => a.DuePayment.HasValue == true);


                if (dueStartDate.HasValue)
                    expense = expense.Where(a => a.DuePayment.Value.Date >= dueStartDate.Value.Date);

                if (dueEndDate.HasValue)
                    expense = expense.Where(a => a.DuePayment.Value.Date <= dueEndDate.Value.Date);
            }
            else
            {
                expense = expense.Where(a => a.DueDate.HasValue == true && a.DuePayment.HasValue == false);

                if (dueStartDate.HasValue)
                    expense = expense.Where(a => a.DueDate.Value.Date >= dueStartDate.Value.Date);

                if (dueEndDate.HasValue)
                    expense = expense.Where(a => a.DueDate.Value.Date <= dueEndDate.Value.Date);
            }



            if (!string.IsNullOrEmpty(searchTerm))
                expense = expense.Where(a => a.Name.Contains(searchTerm) || a.Person.FirstName.Contains(searchTerm) || a.Person.LastName.Contains(searchTerm));


            CardFinancialModel data = ReturnDataModel(payed, model, title, cssCard, expense);

            return View(data);
        }



        private static CardFinancialModel ReturnDataModel(bool payed, string model, string title, string cssCard, IQueryable<VExpense> expense)
        {

            var data = new CardFinancialModel
            {
                Qty = expense.Count(),
                payed = payed,
                HtmlModel = model,
                Expenses = expense,
                Amount = payed ? expense.Sum(a => a.Credit) : expense.Sum(a => a.AmountFinal),
                title = title,
                cssCard = cssCard
            };

            
            return data;


        }


    }
}
