using Core.Domain.Finance;
using Core.Domain.Finance.Views;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Smart.Models.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smart.ViewsComponents.Financial.Revenue
{
    public class SummarizeFinancial : ViewComponent
    {
        private readonly IServices<VExpense> _expenseAccount;
        private readonly IServices<VRevenue> _revenueAccount;
        private readonly IServices<BankTrans> _balanceAccount;

        public SummarizeFinancial(IServices<VExpense> expenseAccount, IServices<VRevenue> revenueAccount, IServices<BankTrans> balanceAccount)
        {
            this._expenseAccount = expenseAccount;
            this._revenueAccount = revenueAccount;
            this._balanceAccount = balanceAccount;

        }


        public async Task<IViewComponentResult> InvokeAsync(
                                DateTime? dueStartDate,
                                DateTime? dueEndDate,
                                string RenderView)
        {
            var expenses = await _expenseAccount.QueryAsync();
            var revenues = await _revenueAccount.QueryAsync();
            var balances = await _balanceAccount.QueryAsync();

            if (dueStartDate.HasValue)
            {
                balances = balances.Where(a => a.DueDate.Value.Date >= dueStartDate.Value.Date);
                expenses = expenses.Where(a => a.DueDate.Value.Date >= dueStartDate.Value.Date);
                revenues = revenues.Where(a => a.DueDate.Value.Date >= dueStartDate.Value.Date);
            }

            if (dueEndDate.HasValue)
            {
                balances = balances.Where(a => a.DueDate.Value.Date <= dueEndDate.Value.Date);
                expenses = expenses.Where(a => a.DueDate.Value.Date <= dueEndDate.Value.Date);
                revenues = revenues.Where(a => a.DueDate.Value.Date <= dueEndDate.Value.Date);
            }

            decimal creditAll = balances.Where(a => a.Signal == 1).Select(a => a.Total).DefaultIfEmpty(0).Sum();
            decimal debitAll = balances.Where(a => a.Signal == 2).Select(a => a.Total).DefaultIfEmpty(0).Sum();
            decimal AmountTotal = creditAll - debitAll;
            decimal RevenueTotal = revenues.Select(a => a.AmountFinal).DefaultIfEmpty(0).Sum();
            decimal ExpenseTotal = expenses.Select(a => a.AmountFinal).DefaultIfEmpty(0).Sum();


            SummarizeFinancialModel data = new SummarizeFinancialModel()
            {
                RevenueTotal = RevenueTotal,
                ExpenseTotal = ExpenseTotal,
                RenderView = RenderView,
                PreviousAmmont = AmountTotal,
                Expected = (AmountTotal + RevenueTotal) - (ExpenseTotal),
                HasRow = (balances.Any() && expenses.Any() && revenues.Any()),
                Balances = balances,
                Expenses = expenses,
                Revenues = revenues
            };
            return View(data);
        }
    }
}
