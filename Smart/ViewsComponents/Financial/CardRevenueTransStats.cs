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

    public class CardRevenueTransStats : ViewComponent
    {
        private readonly IServices<VRevenueTrans> _vrevenueServices;

        public CardRevenueTransStats(IServices<VRevenueTrans> vrevenueServices)
        {
            this._vrevenueServices = vrevenueServices;
        }

        public async Task<IViewComponentResult> InvokeAsync(bool payed, int? signal, DateTime? dueStartDate, DateTime? dueEndDate, string searchTerm, string model, string title, string cssCard)
        {
            var revenue = await  _vrevenueServices.QueryAsync();


            revenue = revenue.Where(a => a.DueDate.HasValue == true);

            if (signal.HasValue)
                revenue = revenue.Where(a => a.Signal == signal.Value);

            if (dueStartDate.HasValue)
                revenue = revenue.Where(a => a.DueDate.Value.Date >= dueStartDate.Value.Date);

            if (dueEndDate.HasValue)
                revenue = revenue.Where(a => a.DueDate.Value.Date <= dueEndDate.Value.Date);

            if (!string.IsNullOrEmpty(searchTerm))
                revenue = revenue.Where(a => a.Name.Contains(searchTerm));


            CardFinancialModel data = ReturnDataModel(payed, model, title, cssCard, revenue);

            return View(data);
        }



        private static CardFinancialModel ReturnDataModel(bool payed, string model, string title, string cssCard, IQueryable<VRevenueTrans> revenue)
        {

            var data = new CardFinancialModel
            {
                Qty = revenue.Count(),
                RevenueTrans = revenue.Include(a => a.CategoryFinancial),
                Amount = revenue.Sum(a => a.Total),
                payed = payed,
                HtmlModel = model,
                title = title,
                cssCard = cssCard
            };


            return data;




        }


    }
}
