using Core.Domain.Finance.Views;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Smart.Models.Components;
using Smart.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smart.ViewsComponents.Financial
{

    public class CardRevenueFixStats : ViewComponent
    {
        private readonly IServices<VRevenue> _vrevenueServices;

        public CardRevenueFixStats(IServices<VRevenue> vrevenueServices)
        {
            this._vrevenueServices = vrevenueServices;
        }

        public async Task<IViewComponentResult> InvokeAsync(int time, string model, string title, string cssCard)
        {
            var revenue = await _vrevenueServices.QueryAsync(a => a.DuePayment.HasValue == false);
            CardFinancialModel data = ReturnDataModel(time, model, title, cssCard, revenue);

            return View(data);
        }



        private static CardFinancialModel ReturnDataModel(int time, string model, string title, string cssCard, IQueryable<VRevenue> revenue)
        {
            var now = System.DateTime.Now.Date;

            var data = new CardFinancialModel
            {
                Qty = revenue.Count(),
                time = time,
                HtmlModel = model,
                Revenues = revenue,
                title = title,
                cssCard = cssCard
            };

           
            switch (time)
            {
                case 0:
                    data.Amount = revenue.Where(a => a.DueDate.Value.Date < now).Sum(a => a.AmountFinal);
                    break;
                case 1:
                    data.Amount = revenue.Where(a => a.DueDate.Value.Date == now).Sum(a => a.AmountFinal);
                    break;
                case 2:
                    data.Amount = revenue.Where(a => a.DueDate.Value.Date > now).Sum(a => a.AmountFinal);
                    break;
                case 3:
                    data.Amount = revenue.Where(a => a.DueDate.HasValue).Sum(a => a.AmountFinal);
                    break;
                case 4:
                    data.Amount = revenue.Where(a => a.DueDate.HasValue == false).Sum(a => a.AmountFinal);
                    break;
                default:
                    data.Amount = revenue.Sum(a => a.AmountFinal);
                    break;
            }

            return data;




        }


    }
}
