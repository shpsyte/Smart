using Core.Domain.Finance.Views;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using Smart.Models.Components;
using Smart.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smart.ViewsComponents.Financial
{

    public class CardExpenseFixStats : ViewComponent
    {
        private readonly IServices<VExpense> _vexpenseServices;

        public CardExpenseFixStats(IServices<VExpense> vexpenseServices)
        {
            this._vexpenseServices = vexpenseServices;
        }

        public async Task<IViewComponentResult> InvokeAsync(int time, string model, string title, string cssCard)
        {
            var expense = await _vexpenseServices.QueryAsync(a => a.DuePayment.HasValue == false);
            CardFinancialModel data = ReturnDataModel(time, model, title, cssCard, expense);

            return View(data);
        }



        private static CardFinancialModel ReturnDataModel(int time, string model, string title, string cssCard, IQueryable<VExpense> expense)
        {
            var now = System.DateTime.Now.Date;

            var data = new CardFinancialModel
            {
                Qty = expense.Count(),
                time = time,
                HtmlModel = model,
                Expenses = expense.Include(a => a.CategoryFinancial),
                title = title,
                cssCard = cssCard
            };

           
            switch (time)
            {
                case 0:
                    data.Amount = expense.Where(a => a.DueDate.Value.Date < now).Sum(a => a.AmountFinal);
                    break;
                case 1:
                    data.Amount = expense.Where(a => a.DueDate.Value.Date == now).Sum(a => a.AmountFinal);
                    break;
                case 2:
                    data.Amount = expense.Where(a => a.DueDate.Value.Date > now).Sum(a => a.AmountFinal);
                    break;
                case 3:
                    data.Amount = expense.Where(a => a.DueDate.HasValue).Sum(a => a.AmountFinal);
                    break;
                case 4:
                    data.Amount = expense.Where(a => a.DueDate.HasValue == false).Sum(a => a.AmountFinal);
                    break;
                default:
                    data.Amount = expense.Sum(a => a.AmountFinal);
                    break;
            }

            return data;




        }


    }
}
