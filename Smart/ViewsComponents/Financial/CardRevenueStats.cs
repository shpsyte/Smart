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

    public class CardRevenueStats : ViewComponent
    {
        private readonly IServices<VRevenue> _vrevenueServices;

        public CardRevenueStats(IServices<VRevenue> vrevenueServices)
        {
            this._vrevenueServices = vrevenueServices;
        }

        public async Task<IViewComponentResult> InvokeAsync(bool payed, DateTime? dueStartDate, DateTime? dueEndDate, string searchTerm, string model, string title, string cssCard)
        {
            var revenue = await _vrevenueServices.QueryAsync();

            if (payed)
            {
                revenue = revenue.Where(a => a.DuePayment.HasValue == true);

                if (dueStartDate.HasValue)
                    revenue = revenue.Where(a => a.DuePayment.Value.Date >= dueStartDate.Value.Date);

                if (dueEndDate.HasValue)
                    revenue = revenue.Where(a => a.DuePayment.Value.Date <= dueEndDate.Value.Date);
            }
            else
            {
                revenue = revenue.Where(a => a.DueDate.HasValue == true && a.DuePayment.HasValue == false);


                if (dueStartDate.HasValue)
                    revenue = revenue.Where(a => a.DueDate.Value.Date >= dueStartDate.Value.Date);

                if (dueEndDate.HasValue)
                    revenue = revenue.Where(a => a.DueDate.Value.Date <= dueEndDate.Value.Date);
            }


            if (!string.IsNullOrEmpty(searchTerm))
                revenue = revenue.Where(a => a.Name.Contains(searchTerm) || a.Person.FirstName.Contains(searchTerm) || a.Person.LastName.Contains(searchTerm));


            CardFinancialModel data = ReturnDataModel(payed, model, title, cssCard, revenue);

            return View(data);
        }



        private static CardFinancialModel ReturnDataModel(bool payed, string model, string title, string cssCard, IQueryable<VRevenue> revenue)
        {

            var data = new CardFinancialModel
            {
                Qty = revenue.Count(),
                Revenues = revenue,
                Amount = payed ? revenue.Sum(a => a.Credit) : revenue.Sum(a => a.AmountFinal),
                payed = payed,
                HtmlModel = model,
                title = title,
                cssCard = cssCard
            };


            return data;




        }


    }
}
