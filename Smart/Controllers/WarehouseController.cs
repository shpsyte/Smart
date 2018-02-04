using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Services.Interfaces;
using Smart.Services;
using Core.Domain.Production;
using Smart.Data;
namespace Smart.Controllers
{
    [Authorize]    
    public class WarehouseController : BaseController
    {
        #region vars
        private readonly  IServices<Location> _warehouseServices;
        #endregion
        #region ctor
            public WarehouseController(
                                    IServices<Location> warehouseServices, 
                                    IUser currentUser, 
                                    IEmailSender emailSender, 
                                    IHttpContextAccessor accessor
                                    ) : base(currentUser, emailSender, accessor)
            {
            this._warehouseServices = warehouseServices;
            }
        #endregion
        #region methods
        // GET: Warehouse
        [Route("warehouse-management/warehouse-list")]
        public async Task<IActionResult> List(string search)
        {
            ViewData["search"] = search;
            var data = await _warehouseServices.QueryAsync();
            if (!string.IsNullOrEmpty(search)) 
            {
               data = data.Where(p =>  
                           p.Name.Contains(search)
                );
            }
               return View(data);
        }
       // GET: Warehouse/Add
        [Route("warehouse-management/warehouse-add")]
        public IActionResult Add()
        {
          var data = new Location();
          return View(data);
        }
        // POST: Warehouse/Add
        [HttpPost, ValidateAntiForgeryToken]
        [Route("warehouse-management/warehouse-add")]
        public async Task<IActionResult> Add([Bind("WarehouseId,Name,BusinessEntityId")] Location warehouse, bool continueAdd)
        {
            if (ModelState.IsValid)
            {
                 await _warehouseServices.AddAsync(warehouse);
                 return continueAdd ? RedirectToAction(nameof(Add)) : RedirectToAction(nameof(List));
            }
            return View(warehouse);
        }
        // GET: Warehouse/Edit/5
        [Route("warehouse-management/warehouse-edit/{id?}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var warehouse = await _warehouseServices.SingleOrDefaultAsync(m => m.WarehouseId == id);
            if (warehouse == null)
            {
                return NotFound();
            }
            return View(warehouse);
        }
        // POST: Warehouse/Edit/5
        [HttpPost, ValidateAntiForgeryToken]
        [Route("warehouse-management/warehouse-edit/{id?}")]
        public async Task<IActionResult> Edit(int id, [Bind("WarehouseId,Name,BusinessEntityId")] Location warehouse, bool continueAdd)
        {
            if (id != warehouse.WarehouseId)
            {
                return NotFound();
            }
                 if (ModelState.IsValid)
            {
                try
                {
                    await _warehouseServices.UpdateAsync(warehouse);
                }
                catch (DbUpdateConcurrencyException)
                {
                        throw;
                }
                return continueAdd ? RedirectToAction(nameof(Edit), new { id = warehouse.WarehouseId }) : RedirectToAction(nameof(List));
            }
            return View(warehouse);
        }
        [Route("warehouse-management/warehouse-delete/{id?}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
               var warehouse = await  _warehouseServices.SingleOrDefaultAsync(m => m.WarehouseId == id);
               if (warehouse == null)
               {
                    return NotFound();
                }
                    return View(warehouse);
         }  
         [HttpPost, ValidateAntiForgeryToken]
         [Route("warehouse-management/warehouse-delete/{id?}")]
         public async Task<IActionResult> Delete (Location warehouse)
         {
             await _warehouseServices.DeleteAsync(warehouse);
             return RedirectToAction(nameof(List));
         }
        #endregion methods
    }
}
