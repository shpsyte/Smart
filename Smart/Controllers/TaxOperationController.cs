using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Services.Interfaces;
using Smart.Services;
using Core.Domain.Accounting;
using Smart.Data;
namespace Smart.Controllers
{
    [Authorize]    
    public class TaxOperationController : BaseController
    {
        #region vars
        private readonly  IServices<TaxOperation> _taxOperationServices;
        #endregion
        #region ctor
            public TaxOperationController(
                                    IServices<TaxOperation> taxOperationServices, 
                                    IUser currentUser, 
                                    IEmailSender emailSender, 
                                    IHttpContextAccessor accessor
                                    ) : base(currentUser, emailSender, accessor)
            {
            this._taxOperationServices = taxOperationServices;
            }
        #endregion
        #region methods
        // GET: TaxOperation
        [Route("taxoperation-management/taxoperation-list")]
        public async Task<IActionResult> List(string search)
        {
            ViewData["search"] = search;
            var data = await _taxOperationServices.QueryAsync();
            if (!string.IsNullOrEmpty(search)) 
            {
               data = data.Where(p =>  
                           p.Name.Contains(search)
                   || p.DefaultCode.Contains(search)
                );
            }
               return View(data);
        }
       // GET: TaxOperation/Add
        [Route("taxoperation-management/taxoperation-add")]
        public IActionResult Add()
        {
          var data = new TaxOperation();
          return View(data);
        }
        // POST: TaxOperation/Add
        [HttpPost, ValidateAntiForgeryToken]
        [Route("taxoperation-management/taxoperation-add")]
        public async Task<IActionResult> Add([Bind("TaxOperationId,Name,TaxFunction,TaxWay,DefaultCode,StockTrigger,CostTrigger,BusinessEntityId")] TaxOperation taxOperation, bool continueAdd)
        {
            if (ModelState.IsValid)
            {
                 await _taxOperationServices.AddAsync(taxOperation);
                 return continueAdd ? RedirectToAction(nameof(Add)) : RedirectToAction(nameof(List));
            }
            return View(taxOperation);
        }
        // GET: TaxOperation/Edit/5
        [Route("taxoperation-management/taxoperation-edit/{id?}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var taxOperation = await _taxOperationServices.SingleOrDefaultAsync(m => m.TaxOperationId == id);
            if (taxOperation == null)
            {
                return NotFound();
            }
            return View(taxOperation);
        }
        // POST: TaxOperation/Edit/5
        [HttpPost, ValidateAntiForgeryToken]
        [Route("taxoperation-management/taxoperation-edit/{id?}")]
        public async Task<IActionResult> Edit(int id, [Bind("TaxOperationId,Name,TaxFunction,TaxWay,DefaultCode,StockTrigger,CostTrigger,BusinessEntityId")] TaxOperation taxOperation, bool continueAdd)
        {
            if (id != taxOperation.TaxOperationId)
            {
                return NotFound();
            }
                 if (ModelState.IsValid)
            {
                try
                {
                    await _taxOperationServices.UpdateAsync(taxOperation);
                }
                catch (DbUpdateConcurrencyException)
                {
                        throw;
                }
                return continueAdd ? RedirectToAction(nameof(Edit), new { id = taxOperation.TaxOperationId }) : RedirectToAction(nameof(List));
            }
            return View(taxOperation);
        }
        [Route("taxoperation-management/taxoperation-delete/{id?}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
               var taxOperation = await  _taxOperationServices.SingleOrDefaultAsync(m => m.TaxOperationId == id);
               if (taxOperation == null)
               {
                    return NotFound();
                }
                    return View(taxOperation);
         }  
         [HttpPost, ValidateAntiForgeryToken]
         [Route("taxoperation-management/taxoperation-delete/{id?}")]
         public async Task<IActionResult> Delete (TaxOperation taxOperation)
         {
             await _taxOperationServices.DeleteAsync(taxOperation);
             return RedirectToAction(nameof(List));
         }
        #endregion methods
    }
}
