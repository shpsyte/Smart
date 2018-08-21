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
using Core.Domain.Business;

namespace Smart.Controllers
{
    [Authorize]    
    public class CategoryProductController : BaseController
    {
        #region vars
        private readonly  IServices<CategoryProduct> _categoryProductServices;
        #endregion
        #region ctor
            public CategoryProductController(
                                    IServices<CategoryProduct> categoryProductServices, 
                                    IUser currentUser, 
                                    IEmailSender emailSender, 
                                    IHttpContextAccessor accessor,
                                    IServices<BusinessEntity> businessEntity
                                    ) : base(currentUser, emailSender, accessor, businessEntity)
            {
            this._categoryProductServices = categoryProductServices;
            }
        #endregion
        #region methods
        // GET: CategoryProduct
        [Route("categoryproduct-management/categoryproduct-list")]
        public async Task<IActionResult> List(string search)
        {
            ViewData["search"] = search;
            var data = await _categoryProductServices.QueryAsync();
            if (!string.IsNullOrEmpty(search)) 
            {
               data = data.Where(p =>  
                           p.Name.Contains(search)
                );
            }
               return View(data);
        }
       // GET: CategoryProduct/Add
        [Route("categoryproduct-management/categoryproduct-add")]
        public IActionResult Add()
        {
          var data = new CategoryProduct();
          return View(data);
        }
        // POST: CategoryProduct/Add
        [HttpPost, ValidateAntiForgeryToken]
        [Route("categoryproduct-management/categoryproduct-add")]
        public async Task<IActionResult> Add([Bind("CategoryId,Name,BusinessEntityId")] CategoryProduct categoryProduct, bool continueAdd)
        {
            if (ModelState.IsValid)
            {
                 await _categoryProductServices.AddAsync(categoryProduct);
                 return continueAdd ? RedirectToAction(nameof(Add)) : RedirectToAction(nameof(List));
            }
            return View(categoryProduct);
        }
        // GET: CategoryProduct/Edit/5
        [Route("categoryproduct-management/categoryproduct-edit/{id?}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var categoryProduct = await _categoryProductServices.SingleOrDefaultAsync(m => m.CategoryId == id);
            if (categoryProduct == null)
            {
                return NotFound();
            }
            return View(categoryProduct);
        }
        // POST: CategoryProduct/Edit/5
        [HttpPost, ValidateAntiForgeryToken]
        [Route("categoryproduct-management/categoryproduct-edit/{id?}")]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryId,Name,BusinessEntityId")] CategoryProduct categoryProduct, bool continueAdd)
        {
            if (id != categoryProduct.CategoryId)
            {
                return NotFound();
            }
                 if (ModelState.IsValid)
            {
                try
                {
                    await _categoryProductServices.UpdateAsync(categoryProduct);
                }
                catch (DbUpdateConcurrencyException)
                {
                        throw;
                }
                return continueAdd ? RedirectToAction(nameof(Edit), new { id = categoryProduct.CategoryId }) : RedirectToAction(nameof(List));
            }
            return View(categoryProduct);
        }
        [Route("categoryproduct-management/categoryproduct-delete/{id?}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
               var categoryProduct = await  _categoryProductServices.SingleOrDefaultAsync(m => m.CategoryId == id);
               if (categoryProduct == null)
               {
                    return NotFound();
                }
                    return View(categoryProduct);
         }  
         [HttpPost, ValidateAntiForgeryToken]
         [Route("categoryproduct-management/categoryproduct-delete/{id?}")]
         public async Task<IActionResult> Delete (CategoryProduct categoryProduct)
         {
             await _categoryProductServices.DeleteAsync(categoryProduct);
             return RedirectToAction(nameof(List));
         }
        #endregion methods
    }
}
