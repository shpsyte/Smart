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
namespace Smart.Controllers
{
    [Authorize]    
    public class FinancialCategoryController : BaseController
    {
        #region vars
        private readonly  IServices<CategoryFinancial> _categoryFinancialServices;
        #endregion
        #region ctor
            public FinancialCategoryController(
                                    IServices<CategoryFinancial> categoryFinancialServices, 
                                    IUser currentUser, 
                                    IEmailSender emailSender, 
                                    IHttpContextAccessor accessor
                                    ) : base(currentUser, emailSender, accessor)
            {
            this._categoryFinancialServices = categoryFinancialServices;
            }
        #endregion
        #region methods
        // GET: FinancialCategory
        [Route("categoryfinancial-management/categoryfinancial-list")]
        public async Task<IActionResult> List(string search)
        {
            ViewData["search"] = search;
            var data = await _categoryFinancialServices.QueryAsync();
            if (!string.IsNullOrEmpty(search)) 
            {
               data = data.Where(p =>  
                           p.Name.Contains(search)
                );
            }
               return View(data);
        }
       // GET: FinancialCategory/Add
        [Route("categoryfinancial-management/categoryfinancial-add")]
        public IActionResult Add()
        {
          var data = new CategoryFinancial();
          return View(data);
        }
        // POST: FinancialCategory/Add
        [HttpPost, ValidateAntiForgeryToken]
        [Route("categoryfinancial-management/categoryfinancial-add")]
        public async Task<IActionResult> Add([Bind("ChartAccountId,Name,Type,CreateDate,ModifiedDate,Active,BusinessEntityId")] CategoryFinancial categoryFinancial, bool continueAdd)
        {
            if (ModelState.IsValid)
            {
                 await _categoryFinancialServices.AddAsync(categoryFinancial);
                 return continueAdd ? RedirectToAction(nameof(Add)) : RedirectToAction(nameof(List));
            }
            return View(categoryFinancial);
        }
        // GET: FinancialCategory/Edit/5
        [Route("categoryfinancial-management/categoryfinancial-edit/{id?}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var categoryFinancial = await _categoryFinancialServices.SingleOrDefaultAsync(m => m.ChartAccountId == id);
            if (categoryFinancial == null)
            {
                return NotFound();
            }
            return View(categoryFinancial);
        }
        // POST: FinancialCategory/Edit/5
        [HttpPost, ValidateAntiForgeryToken]
        [Route("categoryfinancial-management/categoryfinancial-edit/{id?}")]
        public async Task<IActionResult> Edit(int id, [Bind("ChartAccountId,Name,Type,CreateDate,ModifiedDate,Active,BusinessEntityId")] CategoryFinancial categoryFinancial, bool continueAdd)
        {
            if (id != categoryFinancial.ChartAccountId)
            {
                return NotFound();
            }
                 if (ModelState.IsValid)
            {
                try
                {
                    await _categoryFinancialServices.UpdateAsync(categoryFinancial);
                }
                catch (DbUpdateConcurrencyException)
                {
                        throw;
                }
                return continueAdd ? RedirectToAction(nameof(Edit), new { id = categoryFinancial.ChartAccountId }) : RedirectToAction(nameof(List));
            }
            return View(categoryFinancial);
        }
        [Route("categoryfinancial-management/categoryfinancial-delete/{id?}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
               var categoryFinancial = await  _categoryFinancialServices.SingleOrDefaultAsync(m => m.ChartAccountId == id);
               if (categoryFinancial == null)
               {
                    return NotFound();
                }
                    return View(categoryFinancial);
         }  
         [HttpPost, ValidateAntiForgeryToken]
         [Route("categoryfinancial-management/categoryfinancial-delete/{id?}")]
         public async Task<IActionResult> Delete (CategoryFinancial categoryFinancial)
         {
             await _categoryFinancialServices.DeleteAsync(categoryFinancial);
             return RedirectToAction(nameof(List));
         }
        #endregion methods
    }
}
