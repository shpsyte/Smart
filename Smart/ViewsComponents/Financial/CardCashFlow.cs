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

    public class CardCashFlow : ViewComponent
    {
        private readonly IServices<VCashFlow> _vCashFlowServices;

        public CardCashFlow(IServices<VCashFlow> vCashFlowServices)
        {
            this._vCashFlowServices = vCashFlowServices;
        }

        public async Task<IViewComponentResult> InvokeAsync(bool payed, DateTime? dueStartDate, DateTime? dueEndDate, string searchTerm,  string model, string title, string cssCard, decimal? initial)
        {
            var cashFlow = await _vCashFlowServices.QueryAsync (a => a.DuePayment.HasValue == false);


            if (dueStartDate.HasValue)
                cashFlow = cashFlow.Where(a => a.DueDate.Value.Date >= dueStartDate.Value.Date);

            if (dueEndDate.HasValue)
                    cashFlow = cashFlow.Where(a => a.DueDate.Value.Date <= dueEndDate.Value.Date);

            if (!string.IsNullOrEmpty(searchTerm))
                cashFlow = cashFlow.Where(a => a.Name.Contains(searchTerm) || a.Person.FirstName.Contains(searchTerm) || a.Person.LastName.Contains(searchTerm));


            CardFinancialModel data = ReturnDataModel(model, title, cssCard, cashFlow, initial);

            return View(data);
        }



        private static CardFinancialModel ReturnDataModel(string model, string title, string cssCard, IQueryable<VCashFlow> cashFlow, decimal? initial)
        {
            decimal creditAll = cashFlow.Where(a => a.Tp == "R").Select(a => a.Total).DefaultIfEmpty(0).Sum();
            decimal debitAll = cashFlow.Where(a => a.Tp == "E").Select(a => a.Total).DefaultIfEmpty(0).Sum();
            decimal AmountTotal = creditAll - debitAll;


            var data = new CardFinancialModel
            {
                Qty = cashFlow.Count(),
                HtmlModel = model,
                CashFlows = cashFlow.Include(a => a.CategoryFinancial),
                Amount = AmountTotal,
                initial = initial,
                title = title,
                cssCard = cssCard
            };

            
            return data;


        }


    }
}
