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
    public class ClassProductController : BaseController
    {
        #region vars
        private readonly  IServices<ClassProduct> _classProductServices;
        #endregion
        #region ctor
            public ClassProductController(
                                    IServices<ClassProduct> classProductServices, 
                                    IUser currentUser, 
                                    IEmailSender emailSender, 
                                    IHttpContextAccessor accessor,
                                    IServices<Core.Domain.Business.BusinessEntity> businessEntity
                                    ) : base(currentUser, emailSender, accessor, businessEntity)
            {
            this._classProductServices = classProductServices;
            }
        #endregion
        #region methods
        // GET: ClassProduct
        [Route("classproduct-management/classproduct-list")]
        public async Task<IActionResult> List(string search)
        {
            ViewData["search"] = search;
            var data = await _classProductServices.QueryAsync();
            if (!string.IsNullOrEmpty(search)) 
            {
               data = data.Where(p =>  
                           p.Name.Contains(search)
                );
            }
               return View(data);
        }
       // GET: ClassProduct/Add
        [Route("classproduct-management/classproduct-add")]
        public IActionResult Add()
        {
          var data = new ClassProduct();
          return View(data);
        }
        // POST: ClassProduct/Add
        [HttpPost, ValidateAntiForgeryToken]
        [Route("classproduct-management/classproduct-add")]
        public async Task<IActionResult> Add([Bind("ClassId,Name,BusinessEntityId")] ClassProduct classProduct, bool continueAdd)
        {
            if (ModelState.IsValid)
            {
                 await _classProductServices.AddAsync(classProduct);
                 return continueAdd ? RedirectToAction(nameof(Add)) : RedirectToAction(nameof(List));
            }
            return View(classProduct);
        }
        // GET: ClassProduct/Edit/5
        [Route("classproduct-management/classproduct-edit/{id?}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var classProduct = await _classProductServices.SingleOrDefaultAsync(m => m.ClassId == id);
            if (classProduct == null)
            {
                return NotFound();
            }
            return View(classProduct);
        }
        // POST: ClassProduct/Edit/5
        [HttpPost, ValidateAntiForgeryToken]
        [Route("classproduct-management/classproduct-edit/{id?}")]
        public async Task<IActionResult> Edit(int id, [Bind("ClassId,Name,BusinessEntityId")] ClassProduct classProduct, bool continueAdd)
        {
            if (id != classProduct.ClassId)
            {
                return NotFound();
            }
                 if (ModelState.IsValid)
            {
                try
                {
                    await _classProductServices.UpdateAsync(classProduct);
                }
                catch (DbUpdateConcurrencyException)
                {
                        throw;
                }
                return continueAdd ? RedirectToAction(nameof(Edit), new { id = classProduct.ClassId }) : RedirectToAction(nameof(List));
            }
            return View(classProduct);
        }
        [Route("classproduct-management/classproduct-delete/{id?}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
               var classProduct = await  _classProductServices.SingleOrDefaultAsync(m => m.ClassId == id);
               if (classProduct == null)
               {
                    return NotFound();
                }
                    return View(classProduct);
         }  
         [HttpPost, ValidateAntiForgeryToken]
         [Route("classproduct-management/classproduct-delete/{id?}")]
         public async Task<IActionResult> Delete (ClassProduct classProduct)
         {
             await _classProductServices.DeleteAsync(classProduct);
             return RedirectToAction(nameof(List));
         }
        #endregion methods
    }
}
