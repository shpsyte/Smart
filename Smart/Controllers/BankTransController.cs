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
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using Smart.Models.Financial;
using Smart.Helpers;
using Data.Context;
using AutoMapper;
namespace Smart.Controllers
{
    [Authorize]
    public class BankTransController : BaseController
    {
        #region vars
        private readonly IServices<BankTrans> _bankTransServices;
        private readonly IServices<Bank> _bankServices;
        private readonly IServices<ExpenseTrans> _expenseTransServices;
        private readonly IServices<RevenueTrans> _revenueTransServices;
        private readonly IServices<Expense> _expenseServices;
        private readonly IServices<Revenue> _revenueServices;
        private readonly IServices<CategoryFinancial> _categoryFinancialServices;
        #endregion
        #region ctor
        public BankTransController(
                                IServices<Bank> bankServices,
                                IServices<ExpenseTrans> expenseTransServices,
                                IServices<RevenueTrans> revenueTransServices,
                                IServices<BankTrans> bankTransServices,
                                IServices<Expense> expenseServices,
                                IServices<Revenue> revenueServices,
                                IServices<CategoryFinancial> categoryFinancialServices,
                                IUser currentUser,
                                IEmailSender emailSender,
                                IHttpContextAccessor accessor,
                                SmartContext context
                                ) : base(currentUser, emailSender, accessor, context)
        {
            this._bankTransServices = bankTransServices;
            this._bankServices = bankServices;
            this._expenseTransServices = expenseTransServices;
            this._revenueTransServices = revenueTransServices;
            this._revenueServices = revenueServices;
            this._expenseServices = expenseServices;
            this._categoryFinancialServices = categoryFinancialServices;
        }
        #endregion
        #region private
        private void LoadDataView()
        {
            ViewData["BankId"] = new SelectList(_bankServices.GetAll(), "BankId", "Name");
            ViewData["CategoryId"] = new SelectList(_categoryFinancialServices.GetAll(a => a.Active == true), "ChartAccountId", "Name");
        }
        #endregion
        #region methods
        // GET: BankTrans
        [Route("banktrans-management/banktrans-list")]
        public async Task<IActionResult> List(string search, string start, string end, int? bankId)
        {
            ViewData["BankId"] = new SelectList(_bankServices.GetAll(), "BankId", "Name", bankId);
            ViewData["search"] = search;
            DateTime ini = !string.IsNullOrEmpty(start) ? start.IsDateTime() ? Convert.ToDateTime(start) : System.DateTime.Now.AddDays(-7) : System.DateTime.Now.AddDays(-7);
            DateTime fim = !string.IsNullOrEmpty(end) ? end.IsDateTime() ? Convert.ToDateTime(end) : System.DateTime.Now : System.DateTime.Now;
            var data = await _bankTransServices.QueryAsync();
            data = data.Where(a => a.DueDate.HasValue == true );
            var all = data;
            data = data.Where(a => a.DueDate.Value.Date >= ini.Date);
            data = data.Where(a => a.DueDate.Value.Date <= fim.Date);
            if (!string.IsNullOrEmpty(search))
            {
                data = data.Where(p =>
                       p.Description.Contains(search)
                    || p.MidleDesc.Contains(search)
                 );
            }
            if (bankId.HasValue)
            { 
                data = data.Where(a => a.BankId == bankId);
                all = all.Where(a => a.BankId == bankId);
            }


            decimal creditBeforeDate = all.Where(a => a.DueDate.Value.Date < ini && a.Signal == 1).Select(a => a.Total).DefaultIfEmpty(0).Sum();
            decimal debitBeforeDate = all.Where(a => a.DueDate.Value.Date < ini && a.Signal == 2).Select(a => a.Total).DefaultIfEmpty(0).Sum();
            decimal AmountBefore = creditBeforeDate - debitBeforeDate;
            decimal creditOnDate = data.Where(a => a.DueDate.Value.Date >= ini && a.DueDate.Value.Date <= fim && a.Signal == 1).Select(a => a.Total).DefaultIfEmpty(0).Sum();
            decimal debitOnDate = data.Where(a => a.DueDate.Value.Date >= ini && a.DueDate.Value.Date <= fim && a.Signal == 2).Select(a => a.Total).DefaultIfEmpty(0).Sum();
            decimal AmountData = creditOnDate - debitOnDate;
            decimal creditAll = all.Where(a => a.Signal == 1).Select(a => a.Total).DefaultIfEmpty(0).Sum();
            decimal debitAll  = all.Where(a => a.Signal == 2).Select(a => a.Total).DefaultIfEmpty(0).Sum();
            decimal AmountTotal = creditAll - debitAll;
            var result = new BankTransModel()
            {
                bankTrans = data.Include(a => a.CategoryFinancial),
                start = ini,
                end = fim,
                AmountData = AmountData,
                AmountTotal = AmountTotal,
                AmountBefore = AmountBefore,
                Expense = debitOnDate,
                Revenue = creditOnDate
            };
            return View(result);
        }
        // GET: BankTrans/Add
        [Route("banktrans-management/banktrans-add")]
        public IActionResult Add()
        {
            LoadDataView();
            var data = new BankTrans();
            return View(data);
        }
        // POST: BankTrans/Add
        [HttpPost, ValidateAntiForgeryToken]
        [Route("banktrans-management/banktrans-add")]
        public async Task<IActionResult> Add([Bind("Id,BankId,Description,CreateDate,DueDate,MidleDesc,ExpenseTransId,RevenueTransId,Total,Signal,BusinessEntityId,CategoryId")] BankTrans bankTrans, bool continueAdd)
        {
            if (ModelState.IsValid)
            {
                await _bankTransServices.AddAsync(bankTrans);
                return continueAdd ? RedirectToAction(nameof(Add)) : RedirectToAction(nameof(List));
            }
            LoadDataView();
            return View(bankTrans);
        }
        // GET: BankTrans/Edit/5
        [Route("banktrans-management/banktrans-edit/{id?}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("AccessDenied", "Account");
            }
            var bankTrans = await _bankTransServices.SingleOrDefaultAsync(m => m.Id == id);
            if (bankTrans == null)
            {
                return RedirectToAction("AccessDenied", "Account");
            }
            LoadDataView();
            return View(bankTrans);
        }
        // POST: BankTrans/Edit/5
        [HttpPost, ValidateAntiForgeryToken]
        [Route("banktrans-management/banktrans-edit/{id?}")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BankId,Description,CreateDate,DueDate,MidleDesc,ExpenseTransId,RevenueTransId,Total,Signal,BusinessEntityId,CategoryId")] BankTrans bankTrans, bool continueAdd, bool addTrash)
        {
            if (id != bankTrans.Id)
            {
                return NotFound();
            }
            typeof(BankTrans).GetProperty("Deleted").SetValue(bankTrans, addTrash);
            BankTrans originalValue = await _bankTransServices.SingleOrDefaultAsync(a => a.Id == id);

            LoadDataView();


            if (ModelState.IsValid)
            {
                bankTrans.Total = originalValue.Total;
                await _bankTransServices.UpdateAsyncNoSave(bankTrans);
                if (addTrash)
                {
                    BankTrans reversal = new BankTrans()
                    {
                        BankId = bankTrans.BankId,
                        BusinessEntityId = bankTrans.BusinessEntityId,
                        CategoryId = bankTrans.CategoryId,
                        CreateDate = bankTrans.CreateDate,
                        Deleted = false,
                        Description = bankTrans.Description,
                        DueDate = bankTrans.DueDate,
                        ExcludeId = id,
                        Total = bankTrans.Total,
                        MidleDesc = "EST:" + bankTrans.Id.ToString(),
                        Signal = bankTrans.Signal == 1 ? 2 : 1
                    };
                        

                    if (bankTrans.RevenueTransId.HasValue)
                    {
                        var revenueTransLanc = await _revenueTransServices.SingleOrDefaultAsync(a => a.Id == bankTrans.RevenueTransId);
                        var revenue = await _revenueServices.SingleOrDefaultAsync(a => a.RevenueId == revenueTransLanc.RevenueId);
                        reversal.MidleDesc = "EST:" + revenue.RevenueNumber + "/" + revenue.RevenueSeq.ToString();
                        RevenueTrans reversalrevenueTrans = new RevenueTrans()
                        {
                            BankId = revenueTransLanc.BankId,
                            BusinessEntityId = revenueTransLanc.BusinessEntityId,
                            CreateDate = revenueTransLanc.CreateDate,
                            Description = revenueTransLanc.Description,
                            Midledesc = "EST:" + revenue.RevenueNumber + "/" + revenue.RevenueSeq.ToString(),
                            PaymentConditionId = revenueTransLanc.PaymentConditionId,
                            RevenueId = revenueTransLanc.RevenueId,
                            Signal = revenueTransLanc.Signal == 3 ? 3 : revenueTransLanc.Signal == 1 ? 2 : 1,
                            Total = revenueTransLanc.Total
                        };
                        await _revenueTransServices.AddAsyncNoSave(reversalrevenueTrans);

                        if (revenue.DuePayment.HasValue)
                        {
                            revenue.DuePayment = null;
                            await _revenueServices.UpdateAsyncNoSave(revenue);
                        }


                    }

                    if (bankTrans.ExpenseTransId.HasValue)
                    {
                        var expenseTransLanc = await _expenseTransServices.SingleOrDefaultAsync(a => a.Id == bankTrans.ExpenseTransId);
                        var expense = await _expenseServices.SingleOrDefaultAsync(a => a.ExpenseId == expenseTransLanc.ExpenseId);
                        reversal.MidleDesc = "EST:" + expense.ExpenseNumber + "/" + expense.ExpenseSeq.ToString();
                        ExpenseTrans reversalexpenseTransLanc = new ExpenseTrans()
                        {
                            BankId = expenseTransLanc.BankId,
                            BusinessEntityId = expenseTransLanc.BusinessEntityId,
                            CreateDate = expenseTransLanc.CreateDate,
                            Description = expenseTransLanc.Description,
                            Midledesc = "EST:" + expense.ExpenseNumber + "/" + expense.ExpenseSeq.ToString(),
                            PaymentConditionId = expenseTransLanc.PaymentConditionId,
                            ExpenseId = expenseTransLanc.ExpenseId,
                            Signal = expenseTransLanc.Signal == 3 ? 3 : expenseTransLanc.Signal == 1 ? 2 : 1,
                            Total = expenseTransLanc.Total
                        };
                        await _expenseTransServices.AddAsyncNoSave(reversalexpenseTransLanc);

                        if (expense.DuePayment.HasValue)
                        {
                            expense.DuePayment = null;
                            await _expenseServices.UpdateAsyncNoSave(expense);
                        }
                    }

                    await _bankTransServices.AddAsyncNoSave(reversal);
                }

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return continueAdd ? RedirectToAction(nameof(Edit), new { id = bankTrans.Id }) : RedirectToAction(nameof(List));

            }
            return View(bankTrans);
        }
        [Route("banktrans-management/banktrans-delete/{id?}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var bankTrans = await _bankTransServices.SingleOrDefaultAsync(m => m.Id == id);
            if (bankTrans == null)
            {
                return NotFound();
            }
            return View(bankTrans);
        }
        [HttpPost, ValidateAntiForgeryToken]
        [Route("banktrans-management/banktrans-delete/{id?}")]
        public async Task<IActionResult> Delete(BankTrans bankTrans)
        {
            await _bankTransServices.DeleteAsync(bankTrans);
            return RedirectToAction(nameof(List));
        }
        #endregion methods
    }
}
