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
using Smart.Extensions.Financial;
using System;
using Core.Fake;
using System.Collections.Generic;
using Core.Domain.Finance.Views;
using Smart.Helpers;
using Smart.Models.Financial;
namespace Smart.Controllers
{
    [Authorize]
    public class ExpenseController : BaseController
    {
        #region vars
        private readonly IServices<Expense> _expenseServices;
        private readonly IServices<CategoryFinancial> _categoryFinancialServices;
        private readonly IServices<CostCenter> _costCenterServices;
        private readonly IServices<Condition> _conditionServices;
        private readonly IServices<Person> _personServices;
        private readonly IServices<Bank> _bankServices;
        private readonly FinancialExtension _financialExtension;
        private readonly IServices<ExpenseTrans> _expenseTransServices;
        private readonly IServices<VExpense> _vexpenseServices;
        private readonly IServices<BankTrans> _bankTransServices;
        #endregion
        #region ctor
        public ExpenseController(
                                IServices<CategoryFinancial> categoryFinancialServices,
                                IServices<CostCenter> costCenterServices,
                                IServices<Condition> conditionServices,
                                IServices<Person> personServices,
                                IServices<Expense> expenseServices,
                                FinancialExtension financialEtension,
                                IServices<ExpenseTrans> revenueTransServices,
                                IServices<Bank> bankServices,
                                IServices<BankTrans> bankTransServices,
                                IServices<VExpense> vrevenueServices,
                                IUser currentUser,
                                IEmailSender emailSender,
                                IHttpContextAccessor accessor
                                ) : base(currentUser, emailSender, accessor)
        {
            this._expenseServices = expenseServices;
            this._categoryFinancialServices = categoryFinancialServices;
            this._costCenterServices = costCenterServices;
            this._conditionServices = conditionServices;
            this._personServices = personServices;
            this._financialExtension = financialEtension;
            this._expenseTransServices = revenueTransServices;
            this._vexpenseServices = vrevenueServices;
            this._bankServices = bankServices;
            this._bankTransServices = bankTransServices;
        }
        #endregion
        #region private
        private void LoadViewData()
        {
            ViewData["CategoryId"] = new SelectList(_categoryFinancialServices.GetAll(a => a.Type == 1 && a.Active == true), "ChartAccountId", "Name");
            ViewData["CostCenterId"] = new SelectList(_costCenterServices.GetAll(a => a.Active == true), "CostCenterId", "Name");
            ViewData["PaymentConditionId"] = new SelectList(_conditionServices.GetAll(a => a.PaymentUse == 1 || a.PaymentUse == 3), "ConditionId", "Name");
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
        public async Task<IActionResult> PayAccount([Bind("ExpenseId,DueDate,BankId,PaymentConditionId,Payment,Tax,Discont,Comment,Active,Image")] PayAccount data, bool Active, List<IFormFile> Image)
        {
            Expense expense = await _expenseServices.SingleOrDefaultAsync(a => a.ExpenseId == data.ExpenseId);
            if (data.Payment.HasValue)
            {
                ExpenseTrans expenseTrans = _financialExtension.GetExpenseTrans(data, _BusinessId, "PAG", expense.DueDate.HasValue ? 2 : 3);
                BankTrans bankTrans = _financialExtension.GetBankTrans(data, expenseTrans, _BusinessId, expense.CategoryId);
                //await _expenseTransServices.AddAsyncNoSave(expense);
                await _bankTransServices.AddAsyncNoSave(bankTrans);
            }
            if (data.Tax.HasValue)
            {
                ExpenseTrans expenseTrans = _financialExtension.GetExpenseTrans(data, _BusinessId, "JUR", 1);
                await _expenseTransServices.AddAsyncNoSave(expenseTrans);
            }
            if (data.Discont.HasValue)
            {
                ExpenseTrans expenseTrans = _financialExtension.GetExpenseTrans(data, _BusinessId, "DIS", 2);
                await _expenseTransServices.AddAsyncNoSave(expenseTrans);
            }
            if (data.Active)
            {
                expense.DuePayment = data.DueDate;
                await _expenseServices.UpdateAsyncNoSave(expense);
            }
            var insert = await _expenseTransServices.SaveAsync();
            return RedirectToAction(nameof(List));
        }
        // GET: Expense
        [Route("expense-management/expense-list")]
        public async Task<IActionResult> List(string search)
        {
            ViewData["search"] = search;
            LoadViewData();
            var data = await _vexpenseServices.QueryAsync(a => a.DuePayment.HasValue == false);
           
            if (!search.IsDateTime())
            {
                if (!string.IsNullOrEmpty(search))
                {
                    data = data.Where(p =>
                           p.ExpenseNumber.Contains(search)
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
            DateTime now = System.DateTime.Now;
            return View(data);
        }
        // GET: Expense/Add
        [Route("expense-management/expense-add")]
        public IActionResult Add()
        {
            LoadViewData();
            var data = new Expense();
            return View(data);
        }
        // POST: Expense/Add
        [HttpPost, ValidateAntiForgeryToken]
        [Route("expense-management/expense-add")]
        public async Task<IActionResult> Add([Bind("ExpenseId,ExpenseNumber,ExpenseSeq,ExpenseTotalSeq,Name,PersonId,CategoryId,CostCenterId,PaymentConditionId,Total,CreateDate,DueDate,DuePayment,Comment,ModifiedDate,Deleted,BusinessEntityId,Id,Seq,Session,Total")] Expense expense, bool continueAdd, IEnumerable<TempFinancialSplit> parcelas)
        {
            if (ModelState.IsValid)
            {
                int _next = (_expenseServices.Query().Max(p => (int?)p.ExpenseId) ?? 0) + 1;
                expense.ExpenseNumber = string.IsNullOrEmpty(expense.ExpenseNumber) ? expense.Name.ToString().ToString().Truncate(3, false).ToUpper() + _next.ToString() : expense.ExpenseNumber;
                expense.BusinessEntityId = _BusinessId;
                if (parcelas.Any())
                {
                    foreach (var item in parcelas)
                    {
                        Expense newexpense = _financialExtension.GetExpense(expense, _BusinessId, item);
                        //await _revenueServices.AddAsync(revenueParc);
                        ExpenseTrans expenseTrans = _financialExtension.GetExpenseTrans(newexpense, _BusinessId);
                        await _expenseTransServices.AddAsync(expenseTrans);
                    }
                }
                else
                {
                    if (expense.ExpenseTotalSeq > 1)
                    {
                        var parcelamento = _financialExtension.GenerateSplitPay(expense.ExpenseTotalSeq.Value, expense.Total, expense.DueDate, _currentUser);
                        foreach (var item in parcelamento)
                        {
                            Expense newexpense = _financialExtension.GetExpense(expense, _BusinessId, item);
                            newexpense.ExpenseSeq = item.Seq;
                            newexpense.DueDate = item.DueDate;
                            expense.Total = item.Total.Value;
                            ExpenseTrans expenseTrans = _financialExtension.GetExpenseTrans(newexpense, _BusinessId);
                            //await _revenueServices.AddAsync(revenueParc);
                            await _expenseTransServices.AddAsync(expenseTrans);
                        }
                    }
                    else
                    {
                        expense.ExpenseSeq = 1;
                        expense.ExpenseTotalSeq = 1;
                        ExpenseTrans trans = _financialExtension.GetExpenseTrans(expense, _BusinessId);
                        await _expenseTransServices.AddAsync(trans);
                        //await _revenueServices.AddAsync(revenue);
                    }
                }
                return continueAdd ? RedirectToAction(nameof(Add)) : RedirectToAction(nameof(List));
            }
            LoadViewData();
            return View(expense);
        }
        // GET: Expense/Edit/5
        [Route("expense-management/expense-edit/{id?}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("AccessDenied", "Account");
            }
            var expense = await _expenseServices.SingleOrDefaultAsync(m => m.ExpenseId == id);
            if (expense == null)
            {
                return RedirectToAction("AccessDenied", "Account");
            }
            LoadViewData();
            return View(expense);
        }
        // POST: Expense/Edit/5
        [HttpPost, ValidateAntiForgeryToken]
        [Route("expense-management/expense-edit/{id?}")]
        public async Task<IActionResult> Edit(int id, [Bind("ExpenseId,ExpenseNumber,ExpenseSeq,ExpenseTotalSeq,Name,PersonId,CategoryId,CostCenterId,PaymentConditionId,Total,CreateDate,DueDate,DuePayment,Comment,ModifiedDate,Deleted,BusinessEntityId,_Total,addTrash")] Expense expense, bool continueAdd, bool addTrash)
        {
            if (id != expense.ExpenseId)
            {
                return NotFound();
            }
            typeof(Expense).GetProperty("Deleted").SetValue(expense, addTrash);
            Expense originalValue = await _expenseServices.SingleOrDefaultAsync(a => a.ExpenseId == id);
            LoadViewData();
            if (ModelState.IsValid)
            {
                try
                {
                    expense.Total = originalValue.Total;
                    expense.ExpenseSeq = originalValue.ExpenseSeq;
                    expense.ExpenseTotalSeq = originalValue.ExpenseTotalSeq;
                    await _expenseServices.UpdateAsync(expense);
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return continueAdd ? RedirectToAction(nameof(Edit), new { id = expense.ExpenseId }) : RedirectToAction(nameof(List));
            }
            return View(expense);
        }
        #endregion methods
    }
}
