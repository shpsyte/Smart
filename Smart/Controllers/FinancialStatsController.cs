using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Domain.Finance;
using Core.Domain.Finance.Views;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using Smart.Extensions.Financial;
using Smart.Models.Financial;
using Smart.Services;

namespace Smart.Controllers
{
    public class FinancialStats : BaseController
    {
        private readonly IServices<AccountBank> _bankServices;
        private readonly IServices<VExpense> _vExpenseServices;
        private readonly IServices<CategoryFinancial> _categoryFinancialServices;
        private readonly FinancialExtension _financialExtension;


        public FinancialStats(FinancialExtension financialExtension, IServices<CategoryFinancial> categoryFinancialServices, IServices<VExpense> VExpenseServices, IServices<AccountBank> bankServices, IUser currentUser, IEmailSender emailSender, IHttpContextAccessor accessor, IServices<Core.Domain.Business.BusinessEntity> businessEntity) : base(currentUser, emailSender, accessor, businessEntity)
        {
            this._bankServices = bankServices;
            this._vExpenseServices = VExpenseServices;
            this._categoryFinancialServices = categoryFinancialServices;
            this._financialExtension = financialExtension;
           
        }

        private void LoadViewData()
        {
            ViewData["CategoryId"] = new SelectList(_categoryFinancialServices.GetAll(a => a.Type == 1 && a.Active == true), "ChartAccountId", "Name");
            ViewData["AccountBankId"] = new SelectList(_bankServices.GetAll(), "AccountBankId", "Name");
        }

        [Route("financial-management/cash-flow")]
        public IActionResult CashFlow()
        {
            LoadViewData();
            return View();
        }

        [Route("financial-reports/stats-management")]
        public IActionResult Reports() => View();



        [Route("financial-reports/expense")]
        [HttpGet]
        public IActionResult Expense(FinancialReportsModel filter)
        {
            return View(filter);
        }

        [Route("financial-reports/expense")]
        [HttpPost]
        public async Task<IActionResult> Expense(FinancialReportsModel filter, string trash)
        {
            LoadViewData();
            IQueryable<VExpense> data = await _financialExtension.GetVExpense(filter);
            filter.VExpense = data.Include(a => a.Expense).ThenInclude(a => a.Person);
            return View(filter);
        }



        [Route("financial-reports/revenue")]
        public IActionResult Revenue(FinancialReportsModel filter)
        {
            return View(filter);

        }

        [Route("financial-reports/revenue")]
        [HttpPost]
        public async Task<IActionResult> Revenue(FinancialReportsModel filter, string name)
        {
            LoadViewData();
            IQueryable<VRevenue> data = await _financialExtension.GetVRevenue(filter);
            filter.VRevenue = data.Include(a => a.Revenue).ThenInclude(a => a.Person);
            return View(filter);
        }

        [Route("financial-reports/bankCash")]
        public IActionResult BankCash(FinancialReportsModel filter)
        {
            LoadViewData();
            return View(filter);
        }

        [Route("financial-reports/bankCash")]
        [HttpPost]
        public async Task<IActionResult> BankCash(FinancialReportsModel filter, string trahs)
        {
            LoadViewData();
            IQueryable<BankTrans> data = await _financialExtension.GetBankTans(filter);
            filter.BankTrans = data;
            return View(filter);
        }

        [Route("financial-reports/cash-flow-report")]
        public IActionResult CashflowReports(FinancialReportsModel filter)
        {
            LoadViewData();
            return View(filter);
        }

        [Route("financial-reports/cash-flow-report")]
        [HttpPost]
        public async Task<IActionResult> CashflowReports(FinancialReportsModel filter, string trahs)
        {
            LoadViewData();
            IQueryable<VCashFlow> data = await _financialExtension.GetVCashFlow(filter);
            filter.VCashFlow = data;
            return View(filter);
        }





    }
}