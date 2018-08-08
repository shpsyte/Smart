using Core.Domain.Finance;
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

    public class CardBankTransStats : ViewComponent
    {
        private readonly IServices<BankTrans> _bankTransServices;

        public CardBankTransStats(IServices<BankTrans> bankTransServices)
        {
            this._bankTransServices = bankTransServices;
        }

        public async Task<IViewComponentResult> InvokeAsync(bool payed, int? signal, DateTime? dueStartDate, DateTime? dueEndDate, string searchTerm, int? BankId,  string model, string title, string cssCard)
        {
            var banktrans = await _bankTransServices.QueryAsync();

            if (!payed)
                banktrans = banktrans.Where(a => a.DueDate.HasValue == false);

            if (signal.HasValue)
                banktrans = banktrans.Where(a => a.Signal == signal.Value);

            if (BankId.HasValue)
                banktrans = banktrans.Where(a => a.BankId == BankId.Value);

            if (dueStartDate.HasValue)
                banktrans = banktrans.Where(a => a.DueDate.Value.Date >= dueStartDate.Value.Date);

            if (dueEndDate.HasValue)
                banktrans = banktrans.Where(a => a.DueDate.Value.Date <= dueEndDate.Value.Date);

            if (!string.IsNullOrEmpty(searchTerm))
                banktrans = banktrans.Where(a => a.Description.Contains(searchTerm) || a.MidleDesc.Contains(searchTerm) );



            CardFinancialModel data = ReturnDataModel( model, title, cssCard, banktrans);

            return View(data);
        }



        private static CardFinancialModel ReturnDataModel( string model,  string title, string cssCard, IQueryable<BankTrans> bankTrans)
        {
          
            decimal creditAll = bankTrans.Where(a => a.Signal == 1).Select(a => a.Total).DefaultIfEmpty(0).Sum();
            decimal debitAll = bankTrans.Where(a => a.Signal == 2).Select(a => a.Total).DefaultIfEmpty(0).Sum();
            decimal AmountTotal = creditAll - debitAll;
            var data = new CardFinancialModel
            {
                Qty = bankTrans.Count(),
                HtmlModel = model,
                BankTrans = bankTrans,
                title = title,
                cssCard = cssCard,
                Amount = AmountTotal
            };

            

            return data;




        }


    }
}
