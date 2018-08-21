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
    public class TaxGroupController : BaseController
    {
        #region vars
        private readonly  IServices<TaxGroup> _taxGroupServices;
        #endregion
        #region ctor
            public TaxGroupController(
                                    IServices<TaxGroup> taxGroupServices, 
                                    IUser currentUser, 
                                    IEmailSender emailSender, 
                                    IHttpContextAccessor accessor,
                                    IServices<Core.Domain.Business.BusinessEntity> businessEntity
                                    ) : base(currentUser, emailSender, accessor, businessEntity)
            {
            this._taxGroupServices = taxGroupServices;
            }
        #endregion
        #region methods
        // GET: TaxGroup
        [Route("taxgroup-management/taxgroup-list")]
        public async Task<IActionResult> List(string search)
        {
            ViewData["search"] = search;
            var data = await _taxGroupServices.QueryAsync();
            if (!string.IsNullOrEmpty(search)) 
            {
               data = data.Where(p =>  
                           p.Name.Contains(search)
                );
            }
               return View(data);
        }
       // GET: TaxGroup/Add
        [Route("taxgroup-management/taxgroup-add")]
        public IActionResult Add()
        {
          var data = new TaxGroup();
          return View(data);
        }
        // POST: TaxGroup/Add
        [HttpPost, ValidateAntiForgeryToken]
        [Route("taxgroup-management/taxgroup-add")]
        public async Task<IActionResult> Add([Bind("TaxGroupId,Name,BusinessEntityId")] TaxGroup taxGroup, bool continueAdd)
        {
            if (ModelState.IsValid)
            {
                 await _taxGroupServices.AddAsync(taxGroup);
                 return continueAdd ? RedirectToAction(nameof(Add)) : RedirectToAction(nameof(List));
            }
            return View(taxGroup);
        }
        // GET: TaxGroup/Edit/5
        [Route("taxgroup-management/taxgroup-edit/{id?}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var taxGroup = await _taxGroupServices.SingleOrDefaultAsync(m => m.TaxGroupId == id);
            if (taxGroup == null)
            {
                return NotFound();
            }
            return View(taxGroup);
        }
        // POST: TaxGroup/Edit/5
        [HttpPost, ValidateAntiForgeryToken]
        [Route("taxgroup-management/taxgroup-edit/{id?}")]
        public async Task<IActionResult> Edit(int id, [Bind("TaxGroupId,Name,BusinessEntityId")] TaxGroup taxGroup, bool continueAdd)
        {
            if (id != taxGroup.TaxGroupId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _taxGroupServices.UpdateAsync(taxGroup);
                }
                catch (DbUpdateConcurrencyException)
                {
                        throw;
                }
                return continueAdd ? RedirectToAction(nameof(Edit), new { id = taxGroup.TaxGroupId }) : RedirectToAction(nameof(List));
            }
            return View(taxGroup);
        }
        [Route("taxgroup-management/taxgroup-delete/{id?}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
               var taxGroup = await  _taxGroupServices.SingleOrDefaultAsync(m => m.TaxGroupId == id);
               if (taxGroup == null)
               {
                    return NotFound();
                }
                    return View(taxGroup);
         }  
         [HttpPost, ValidateAntiForgeryToken]
         [Route("taxgroup-management/taxgroup-delete/{id?}")]
         public async Task<IActionResult> Delete (TaxGroup taxGroup)
         {
             await _taxGroupServices.DeleteAsync(taxGroup);
             return RedirectToAction(nameof(List));
         }
        #endregion methods
    }
}
