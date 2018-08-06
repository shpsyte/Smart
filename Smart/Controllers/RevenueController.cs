using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Services.Interfaces;
using Smart.Services;
using Core.Domain.Finance;
using Smart.Data;
using Core.Domain.Sale;
using Core.Domain.PersonAndData;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using AutoMapper;
using Smart.Helpers;
using Smart.Extensions.Financial;
using Core.Fake;
using Smart.Models.Financial;
using Core.Domain.Finance.Views;
using Data.Context;
using System.Data.Common;
namespace Smart.Controllers
{
    [Authorize]
    public class RevenueController : BaseController
    {
        #region vars
        private readonly IServices<Revenue> _revenueServices;
        private readonly IServices<CategoryFinancial> _categoryFinancialServices;
        private readonly IServices<CostCenter> _costCenterServices;
        private readonly IServices<Condition> _conditionServices;
        private readonly IServices<Person> _personServices;
        private readonly IServices<Bank> _bankServices;
        private readonly IServices<TempFinancialSplit> _tempFinancialSplit;
        private readonly IServices<RevenueTrans> _revenueTransServices;
        private readonly IServices<VRevenue> _vrevenueServices;
        private readonly FinancialExtension _financialExtension;
        private readonly IServices<BankTrans> _bankTransServices;
        #endregion
        #region ctor
        public RevenueController(
                                IServices<CategoryFinancial> categoryFinancialServices,
                                IServices<CostCenter> costCenterServices,
                                IServices<Condition> conditionServices,
                                IServices<Person> personServices,
                                IServices<Revenue> revenueServices,
                                IServices<TempFinancialSplit> tempFinancialSplit,
                                FinancialExtension financialEtension,
                                IServices<RevenueTrans> revenueTrans,
                                IServices<BankTrans> bankTransServices,
                                IServices<Bank> bankServices,
                                IServices<VRevenue> vrevenueServices,
                                IUser currentUser,
                                IEmailSender emailSender,
                                IHttpContextAccessor accessor,
                                SmartContext context
                                ) : base(currentUser, emailSender, accessor, context)
        {
            this._revenueServices = revenueServices;
            this._categoryFinancialServices = categoryFinancialServices;
            this._costCenterServices = costCenterServices;
            this._conditionServices = conditionServices;
            this._personServices = personServices;
            this._tempFinancialSplit = tempFinancialSplit;
            this._financialExtension = financialEtension;
            this._revenueTransServices = revenueTrans;
            this._bankServices = bankServices;
            this._vrevenueServices = vrevenueServices;
            this._bankTransServices = bankTransServices;
        }
        #endregion
        #region metodosInternos
        private void LoadViewData()
        {
            ViewData["CategoryId"] = new SelectList(_categoryFinancialServices.GetAll(a => a.Type == 2 && a.Active == true), "ChartAccountId", "Name");
            ViewData["CostCenterId"] = new SelectList(_costCenterServices.GetAll(a => a.Active == true), "CostCenterId", "Name");
            ViewData["PaymentConditionId"] = new SelectList(_conditionServices.GetAll(a => a.PaymentUse == 2 || a.PaymentUse == 3), "ConditionId", "Name");
            ViewData["PersonId"] = new SelectList(_personServices.GetAll(), "PersonId", "FirstName");
            ViewData["BankId"] = new SelectList(_bankServices.GetAll(), "BankId", "Name");
        }
        #endregion
        #region methods
        [HttpPost]
        public JsonResult GetSplit(int TotalSeq, decimal Value, DateTime? DataInicial)
        {
            var split = _financialExtension.GenerateSplitPay(TotalSeq, Value, DataInicial, _currentUser);
            return Json(split);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> PayAccount([Bind("RevenueId,DueDate,BankId,PaymentConditionId,Payment,Tax,Discont,Comment,Active,Image,Id")] PayAccount data, bool Active, List<IFormFile> Image)
        {
            Revenue revenue = await _revenueServices.SingleOrDefaultAsync(a => a.RevenueId == data.RevenueId);
            if (data.Payment.HasValue)
            {
                RevenueTrans revenueTrans = _financialExtension.GetRevenueTrans(data,_BusinessId,"PAG", revenue.DueDate.HasValue ? 2 : 3);
                BankTrans bankTrans = _financialExtension.GetBankTrans(data, revenueTrans, _BusinessId, revenue.CategoryId);
                //await _revenueTransServices.AddAsyncNoSave(revenue);
                await _bankTransServices.AddAsync (bankTrans, false);
            }
            if (data.Tax.HasValue)
            {
                RevenueTrans revenueTrans = _financialExtension.GetRevenueTrans(data, _BusinessId, "JUR", 1);
                await _revenueTransServices.AddAsync(revenueTrans,false);
            }
            if (data.Discont.HasValue)
            {
                RevenueTrans revenueTrans = _financialExtension.GetRevenueTrans(data, _BusinessId, "DIS", 2);
                await _revenueTransServices.AddAsync(revenueTrans,false);
            }
            if (data.Active)
            {
                revenue.DuePayment = data.DueDate;
                await _revenueServices.UpdateAsync(revenue, false);
            }
            var insert = await _revenueTransServices.SaveAsync();
            return RedirectToAction(nameof(List));
        }
        // GET: Revenue
        [Route("revenue-management/revenue-list")]
        public async Task<IActionResult> List(string search)
        {
            ViewData["search"] = search;
            LoadViewData();
            var data = await _vrevenueServices.QueryAsync(a => a.DuePayment.HasValue == false);
            var all = data;
            if (!search.IsDateTime())
            {
                if (!string.IsNullOrEmpty(search))
                {
                    data = data.Where(p =>
                           p.RevenueNumber.Contains(search)
                        || p.Name.Contains(search)
                        || p.Comment.Contains(search)
                        || p.Person.FirstName.Contains(search)
                        || p.Person.LastName.Contains(search)
                        || p.Total.ToString().Contains(search)
                     );
                }
            }
            else
            {
                DateTime Duedata = Convert.ToDateTime(search);
                data = data.Where(p => p.DueDate == Duedata);
            }
           
            return View(data);
        }
        // GET: Revenue/Add
        [Route("revenue-management/revenue-add")]
        public IActionResult Add()
        {
            ///Load Views Data for Dropdownlist
            LoadViewData();
            var data = new Revenue();
            return View(data);
        }
        // POST: Revenue/Add
        [HttpPost, ValidateAntiForgeryToken]
        [Route("revenue-management/revenue-add")]
        public async Task<IActionResult> Add([Bind("RevenueId,RevenueNumber,RevenueSeq,RevenueTotalSeq,Name,PersonId,CategoryId,CostCenterId,PaymentConditionId,Total,CreateDate,DueDate,DuePayment,Comment,ModifiedDate,Deleted,BusinessEntityId,Id,Seq,Session,Total,Id")]Revenue revenue, bool continueAdd, IEnumerable<TempFinancialSplit> parcelas)
        {
            if (ModelState.IsValid)
            {
                int _next = (_revenueServices.Query().Max(p => (int?)p.RevenueId) ?? 0) + 1;
                revenue.RevenueNumber = string.IsNullOrEmpty(revenue.RevenueNumber) ? revenue.Name.ToString().ToString().Truncate(3, false).ToUpper() + _next.ToString() : revenue.RevenueNumber;
                revenue.BusinessEntityId = _BusinessId;
                if (parcelas.Any())
                {
                    foreach (var item in parcelas)
                    {
                        Revenue revenueParc = _financialExtension.GetRevenue(revenue, _BusinessId, item);
                        //await _revenueServices.AddAsync(revenueParc);
                        RevenueTrans revenueTrans = _financialExtension.GetRevenueTrans(revenueParc, _BusinessId);
                        await _revenueTransServices.AddAsync(revenueTrans);
                    }
                }
                else
                {
                    if (revenue.RevenueTotalSeq > 1)
                    {
                        var parcelamento = _financialExtension.GenerateSplitPay(revenue.RevenueTotalSeq.Value, revenue.Total, revenue.DueDate, _currentUser);
                        foreach (var item in parcelamento)
                        {
                            Revenue revenueParc = _financialExtension.GetRevenue(revenue, _BusinessId, item);
                            revenueParc.RevenueSeq = item.Seq;
                            revenueParc.DueDate = item.DueDate;
                            revenue.Total = item.Total.Value;
                            RevenueTrans revenueTrans = _financialExtension.GetRevenueTrans(revenueParc, _BusinessId);
                            //await _revenueServices.AddAsync(revenueParc);
                            await _revenueTransServices.AddAsync(revenueTrans);
                        }
                    }
                    else
                    {
                        revenue.RevenueSeq = 1;
                        revenue.RevenueTotalSeq = 1;
                        RevenueTrans trans = _financialExtension.GetRevenueTrans(revenue, _BusinessId);
                        await _revenueTransServices.AddAsync(trans);
                        //await _revenueServices.AddAsync(revenue);
                    }
                }
                return continueAdd ? RedirectToAction(nameof(Add)) : RedirectToAction(nameof(List));
            }
            LoadViewData();
            return View(revenue);
        }
        // GET: Revenue/Edit/5
        [Route("revenue-management/revenue-edit/{id?}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("AccessDenied", "Account");
            }
            var revenue = await _revenueServices.SingleOrDefaultAsync(m => m.RevenueId == id);
            if (revenue == null)
            {
                return RedirectToAction("AccessDenied", "Account");
            }
            LoadViewData();
            return View(revenue);
        }
        // POST: Revenue/Edit/5
        [HttpPost, ValidateAntiForgeryToken]
        [Route("revenue-management/revenue-edit/{id?}")]
        public async Task<IActionResult> Edit(int id, [Bind("RevenueId,RevenueNumber,Name,Total,PersonId,CategoryId,CostCenterId,PaymentConditionId,CreateDate,DueDate,DuePayment,Comment,ModifiedDate,Deleted,BusinessEntityId,RevenueSeq,RevenueTotalSeq,_Total,addTrash,Id")] Revenue revenue, bool continueAdd, bool addTrash)
        {
            if (id != revenue.RevenueId)
            {
                return NotFound();
            }
            typeof(Revenue).GetProperty("Deleted").SetValue(revenue, addTrash);
            Revenue originalValue = await _revenueServices.SingleOrDefaultAsync(a => a.RevenueId == id);
            LoadViewData();
            if (ModelState.IsValid)
            {
                try
                {
                    revenue.Total = originalValue.Total;
                    revenue.RevenueSeq = originalValue.RevenueSeq;
                    revenue.RevenueTotalSeq = originalValue.RevenueTotalSeq;
                    await _revenueServices.UpdateAsync(revenue);
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return continueAdd ? RedirectToAction(nameof(Edit), new { id = revenue.RevenueId }) : RedirectToAction(nameof(List));
            }
            return View(revenue);
        }
        #endregion methods
    }
}
