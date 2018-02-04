using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Services.Interfaces;
using Smart.Services;
using Core.Domain.PersonAndData;
using Smart.Data;
namespace Smart.Controllers
{
    [Authorize]
    public class CategoryPersonController : BaseController
    {
        #region vars
        private readonly  IServices<CategoryPerson> _categoryPersonServices;
        #endregion
        #region ctor
            public CategoryPersonController(
                                    IServices<CategoryPerson> categoryPersonServices, 
                                    IUser currentUser, 
                                    IEmailSender emailSender, 
                                    IHttpContextAccessor accessor
                                    ) : base(currentUser, emailSender, accessor)
            {
              this._categoryPersonServices = categoryPersonServices;
            }
        #endregion
        #region methods
        // GET: CategoryPerson
        [Route("categoryperson-management/categoryperson-list")]
        public async Task<IActionResult> List(string search)
        {
            ViewData["search"] = search;
            var data = await _categoryPersonServices.QueryAsync();
            if (!string.IsNullOrEmpty(search)) 
            {
               data = data.Where(p =>  
                           p.Name.Contains(search)
                );
            }
               return View(data);
        }
       // GET: CategoryPerson/Add
        [Route("categoryperson-management/categoryperson-add")]
        public IActionResult Add()
        {
          var data = new CategoryPerson();
          return View(data);
        }
        // POST: CategoryPerson/Add
        [HttpPost, ValidateAntiForgeryToken]
        [Route("categoryperson-management/categoryperson-add")]
        public async Task<IActionResult> Add([Bind("CategoryId,Name,CreateDate,ModifiedDate,BusinessEntityId")] CategoryPerson CategoryPerson, bool continueAdd)
        {
            if (ModelState.IsValid)
            {
                 await _categoryPersonServices.AddAsync(CategoryPerson);
                 return continueAdd ? RedirectToAction(nameof(Add)) : RedirectToAction(nameof(List));
            }
            return View(CategoryPerson);
        }
        // GET: CategoryPerson/Edit/5
        [Route("categoryperson-management/categoryperson-edit/{id?}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var CategoryPerson = await _categoryPersonServices.SingleOrDefaultAsync(m => m.CategoryId == id);
            if (CategoryPerson == null)
            {
                return NotFound();
            }
            return View(CategoryPerson);
        }
        // POST: CategoryPerson/Edit/5
        [HttpPost, ValidateAntiForgeryToken]
        [Route("categoryperson-management/categoryperson-edit/{id?}")]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryId,Name,CreateDate,ModifiedDate,BusinessEntityId")] CategoryPerson CategoryPerson, bool continueAdd)
        {
            if (id != CategoryPerson.CategoryId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    await _categoryPersonServices.UpdateAsync(CategoryPerson);
                }
                catch (DbUpdateConcurrencyException)
                {
                        throw;
                }
                return continueAdd ? RedirectToAction(nameof(Edit), new { id = CategoryPerson.CategoryId }) : RedirectToAction(nameof(List));
            }
            return View(CategoryPerson);
        }
        [Route("categoryperson-management/categoryperson-delete/{id?}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
               var CategoryPerson = await  _categoryPersonServices.SingleOrDefaultAsync(m => m.CategoryId == id);
               if (CategoryPerson == null)
               {
                    return NotFound();
                }
                    return View(CategoryPerson);
         }  
         [HttpPost, ValidateAntiForgeryToken]
         [Route("categoryperson-management/categoryperson-delete/{id?}")]
         public async Task<IActionResult> Delete (CategoryPerson CategoryPerson)
         {
             await _categoryPersonServices.DeleteAsync(CategoryPerson);
             return RedirectToAction(nameof(List));
         }
        #endregion methods
    }
}
