using Core.Domain.Finance;
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

    public class CardBankTransFixStats : ViewComponent
    {
        private readonly IServices<BankTrans> _bankTransServices;

        public CardBankTransFixStats(IServices<BankTrans> bankTransServices)
        {
            this._bankTransServices = bankTransServices;
        }

        public async Task<IViewComponentResult> InvokeAsync(int time, int? AccountBankId, DateTime? refDate, string model, string title, string cssCard)
        {
            var banktrans = await _bankTransServices.QueryAsync(a => a.DueDate.HasValue == true);
            if (AccountBankId.HasValue)
                banktrans = banktrans.Where(a => a.AccountBankId == AccountBankId.Value);

            CardFinancialModel data = ReturnDataModel(time, model, refDate, title, cssCard, banktrans);

            return View(data);
        }



        private static CardFinancialModel ReturnDataModel(int time, string model, DateTime? _now, string title, string cssCard, IQueryable<BankTrans> bankTrans)
        {
            DateTime now = _now.HasValue ? _now.Value.Date : System.DateTime.Now.Date;

            var data = new CardFinancialModel
            {
                Qty = bankTrans.Count(),
                time = time,
                HtmlModel = model,
                BankTrans = bankTrans.Include(a => a.CategoryFinancial),
                title = title,
                cssCard = cssCard
            };



            switch (time)
            {
                case 0:
                    decimal creditBeforeDate = bankTrans.Where(a => a.DueDate.Value.Date < now && a.Signal == 1).Select(a => a.Total).DefaultIfEmpty(0).Sum();
                    decimal debitBeforeDate = bankTrans.Where(a => a.DueDate.Value.Date < now && a.Signal == 2).Select(a => a.Total).DefaultIfEmpty(0).Sum();
                    decimal AmountBefore = creditBeforeDate - debitBeforeDate;
                    data.Amount = AmountBefore;
                    break;
                case 1:
                    decimal creditAll = bankTrans.Where(a => a.Signal == 1).Select(a => a.Total).DefaultIfEmpty(0).Sum();
                    decimal debitAll = bankTrans.Where(a => a.Signal == 2).Select(a => a.Total).DefaultIfEmpty(0).Sum();
                    decimal AmountTotal = creditAll - debitAll;
                    data.Amount = AmountTotal;
                    break;
                default:
                    decimal creditAlls = bankTrans.Where(a => a.Signal == 1).Select(a => a.Total).DefaultIfEmpty(0).Sum();
                    decimal debitAlls = bankTrans.Where(a => a.Signal == 2).Select(a => a.Total).DefaultIfEmpty(0).Sum();
                    decimal AmountTotals = creditAlls - debitAlls;
                    data.Amount = AmountTotals;
                    break;
            }

            return data;




        }


    }
}
