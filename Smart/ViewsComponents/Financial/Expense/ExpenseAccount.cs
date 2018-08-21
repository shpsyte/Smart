using Core.Domain.Finance.Views;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using Smart.Models.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smart.ViewsComponents.Financial.Revenue
{
    public class ExpenseAccount : ViewComponent
    {
        private readonly IServices<VExpense> _expenseAccount;

        public ExpenseAccount(IServices<VExpense> expenseAccount)
        {
            this._expenseAccount = expenseAccount;
        }


        public async Task<IViewComponentResult> InvokeAsync(
                                bool payed,
                                int? top,
                                DateTime? dueStartDate,
                                DateTime? dueEndDate,
                                string RenderView)
        {
            var expenses = await _expenseAccount.QueryAsync();

            if (payed)
                expenses = expenses.Where(a => a.DuePayment.HasValue == true);
            else
            {
                expenses = expenses.Where(a => a.DueDate.HasValue == true && a.DuePayment.HasValue == false);
            }

            if (dueStartDate.HasValue)
                expenses = expenses.Where(a => a.DueDate.Value.Date >= dueStartDate.Value.Date);

            if (dueEndDate.HasValue)
                expenses = expenses.Where(a => a.DueDate.Value.Date <= dueEndDate.Value.Date);



            ExpenseAccountModel data = new ExpenseAccountModel()
            {
                List = expenses.Include(a => a.CategoryFinancial).Take(top.HasValue ? top.Value : expenses.Count()),
                Qty = expenses.Count(),
                TotalAmount = expenses.Select(a => a.AmountFinal).DefaultIfEmpty(0).Sum(),
                RenderView = RenderView
            };

            return View(data);
        }
    }
}
