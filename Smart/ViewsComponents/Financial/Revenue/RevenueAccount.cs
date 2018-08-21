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
    public class RevenueAccount : ViewComponent
    {
        private readonly IServices<VRevenue> _revenueAccount;

        public RevenueAccount(IServices<VRevenue> revenueAccount)
        {
            this._revenueAccount = revenueAccount;
        }


        public async Task<IViewComponentResult> InvokeAsync(
                                bool payed, 
                                int? top,
                                DateTime? dueStartDate, 
                                DateTime? dueEndDate,
                                string RenderView)
        {
            var revenues = await _revenueAccount.QueryAsync();

            if (payed)
            { 
                revenues = revenues.Where(a => a.DuePayment.HasValue == true);
            }
            else
            {
                revenues = revenues.Where(a => a.DueDate.HasValue == true && a.DuePayment.HasValue == false);
            }

            if (dueStartDate.HasValue)
                revenues = revenues.Where(a => a.DueDate.Value.Date >= dueStartDate.Value.Date);

            if (dueEndDate.HasValue)
                revenues = revenues.Where(a => a.DueDate.Value.Date <= dueEndDate.Value.Date);



            RevenueAccountModel data = new RevenueAccountModel()
            {
                List = revenues.Include(a => a.CategoryFinancial).Take(top.HasValue ? top.Value : revenues.Count()),
                Qty = revenues.Count(),
                TotalAmount = revenues.Select(a => a.AmountFinal).DefaultIfEmpty(0).Sum(),
                RenderView = RenderView
            };

            return View(data);
        }


    }


}
