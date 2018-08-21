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
    public class CostCenterController : BaseController
    {
        #region vars
        private readonly  IServices<CostCenter> _costCenterServices;
        #endregion
        #region ctor
            public CostCenterController(
                                    IServices<CostCenter> costCenterServices, 
                                    IUser currentUser, 
                                    IEmailSender emailSender, 
                                    IHttpContextAccessor accessor,
                                    IServices<Core.Domain.Business.BusinessEntity> businessEntity
                                    ) : base(currentUser, emailSender, accessor, businessEntity)
            {
            this._costCenterServices = costCenterServices;
            }
        #endregion
        #region methods
        // GET: CostCenter
        [Route("costcenter-management/costcenter-list")]
        public async Task<IActionResult> List(string search)
        {
            ViewData["search"] = search;
            var data = await _costCenterServices.QueryAsync();
            if (!string.IsNullOrEmpty(search)) 
            {
               data = data.Where(p =>  
                           p.Name.Contains(search)
                );
            }
               return View(data);
        }
       // GET: CostCenter/Add
        [Route("costcenter-management/costcenter-add")]
        public IActionResult Add()
        {
          var data = new CostCenter();
          return View(data);
        }
        // POST: CostCenter/Add
        [HttpPost, ValidateAntiForgeryToken]
        [Route("costcenter-management/costcenter-add")]
        public async Task<IActionResult> Add([Bind("CostCenterId,Name,Active,BusinessEntityId")] CostCenter costCenter, bool continueAdd)
        {
            if (ModelState.IsValid)
            {
                 await _costCenterServices.AddAsync(costCenter);
                 return continueAdd ? RedirectToAction(nameof(Add)) : RedirectToAction(nameof(List));
            }
            return View(costCenter);
        }
        // GET: CostCenter/Edit/5
        [Route("costcenter-management/costcenter-edit/{id?}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var costCenter = await _costCenterServices.SingleOrDefaultAsync(m => m.CostCenterId == id);
            if (costCenter == null)
            {
                return NotFound();
            }
            return View(costCenter);
        }
        // POST: CostCenter/Edit/5
        [HttpPost, ValidateAntiForgeryToken]
        [Route("costcenter-management/costcenter-edit/{id?}")]
        public async Task<IActionResult> Edit(int id, [Bind("CostCenterId,Name,Active,BusinessEntityId")] CostCenter costCenter, bool continueAdd)
        {
            if (id != costCenter.CostCenterId)
            {
                return NotFound();
            }
                 if (ModelState.IsValid)
            {
                try
                {
                    await _costCenterServices.UpdateAsync(costCenter);
                }
                catch (DbUpdateConcurrencyException)
                {
                        throw;
                }
                return continueAdd ? RedirectToAction(nameof(Edit), new { id = costCenter.CostCenterId }) : RedirectToAction(nameof(List));
            }
            return View(costCenter);
        }
        [Route("costcenter-management/costcenter-delete/{id?}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
               var costCenter = await  _costCenterServices.SingleOrDefaultAsync(m => m.CostCenterId == id);
               if (costCenter == null)
               {
                    return NotFound();
                }
                    return View(costCenter);
         }  
         [HttpPost, ValidateAntiForgeryToken]
         [Route("costcenter-management/costcenter-delete/{id?}")]
         public async Task<IActionResult> Delete (CostCenter costCenter)
         {
             await _costCenterServices.DeleteAsync(costCenter);
             return RedirectToAction(nameof(List));
         }
        #endregion methods
    }
}
