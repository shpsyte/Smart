using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Domain.Finance;
using Core.Domain.Finance.Views;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Smart.Models.Components;

namespace Smart.ViewsComponents.Financial.Bank
{
    public class BalanceAccount : ViewComponent
    {
        private readonly IServices<VBalanceAccount> _balanceAccount;

        public BalanceAccount(IServices<VBalanceAccount> balanceAccount)
        {
            this._balanceAccount = balanceAccount;
        }


        public async Task<IViewComponentResult> InvokeAsync(int? AccountBankId, string RenderView)
        {
            var balanceAccount = await _balanceAccount.QueryAsync();
          
            if (AccountBankId.HasValue)
                balanceAccount = balanceAccount.Where(a => a.AccountBankId == AccountBankId.Value);

            BalanceAccountModel data = new BalanceAccountModel()
            {
                List = balanceAccount,
                Qty = balanceAccount.Count(),
                TotalAmount = balanceAccount.Select(a => a.Saldo).DefaultIfEmpty(0).Sum(),
                RenderView = RenderView
            };

            return View(data);
        }


    }
}
