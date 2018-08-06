using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Services.Interfaces;
using Smart.Services;
using Core.Domain.Sale;
using Smart.Data;
namespace Smart.Controllers
{
    [Authorize]    
    public class ConditionController : BaseController
    {
        #region vars
        private readonly  IServices<Condition> _conditionServices;
        #endregion
        #region ctor
            public ConditionController(
                                    IServices<Condition> conditionServices, 
                                    IUser currentUser, 
                                    IEmailSender emailSender, 
                                    IHttpContextAccessor accessor
                                    ) : base(currentUser, emailSender, accessor)
            {
            this._conditionServices = conditionServices;
            }
        #endregion
        #region methods
        // GET: Condition
        [Route("condition-management/condition-list")]
        public async Task<IActionResult> List(string search)
        {
            ViewData["search"] = search;
            var data = await _conditionServices.QueryAsync();
                data = data.Where(p => p.Deleted == false);
            if (!string.IsNullOrEmpty(search)) 
            {
               data = data.Where(p =>  
                           p.Name.Contains(search)
                );
            }
               return View(data);
        }
       // GET: Condition/Add
        [Route("condition-management/condition-add")]
        public IActionResult Add()
        {
          var data = new Condition();
          return View(data);
        }
        // POST: Condition/Add
        [HttpPost, ValidateAntiForgeryToken]
        [Route("condition-management/condition-add")]
        public async Task<IActionResult> Add([Bind("ConditionId,Name,PaymentQty,PaymentDays,Tax,PaymentUse,Active,Deleted,BusinessEntityId")] Condition condition, bool continueAdd)
        {
            if (ModelState.IsValid)
            {
                 await _conditionServices.AddAsync(condition);
                 return continueAdd ? RedirectToAction(nameof(Add)) : RedirectToAction(nameof(List));
            }
            return View(condition);
        }
        // GET: Condition/Edit/5
        [Route("condition-management/condition-edit/{id?}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var condition = await _conditionServices.SingleOrDefaultAsync(m => m.ConditionId == id);
            if (condition == null)
            {
                return NotFound();
            }
            return View(condition);
        }
        // POST: Condition/Edit/5
        [HttpPost, ValidateAntiForgeryToken]
        [Route("condition-management/condition-edit/{id?}")]
        public async Task<IActionResult> Edit(int id, [Bind("ConditionId,Name,PaymentQty,PaymentDays,Tax,PaymentUse,Active,Deleted,BusinessEntityId")] Condition condition, bool continueAdd, bool addTrash)
        {
            if (id != condition.ConditionId)
            {
                return NotFound();
            }
                  typeof(Condition).GetProperty("Deleted").SetValue(condition, addTrash);
                 if (ModelState.IsValid)
            {
                try
                {
                    await _conditionServices.UpdateAsync(condition);
                }
                catch (DbUpdateConcurrencyException)
                {
                        throw;
                }
                return continueAdd ? RedirectToAction(nameof(Edit), new { id = condition.ConditionId }) : RedirectToAction(nameof(List));
            }
            return View(condition);
        }
             #endregion methods
    }
}
