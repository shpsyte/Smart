using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Services.Interfaces;
using Smart.Services;
using Core.Domain.Region;
using Smart.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Data.Repository;
using Core.Domain.Business;

namespace Smart.Controllers
{
    [Authorize]
    public class StateProvinceController : BaseController
    {
        #region vars
        private readonly IServices<StateProvince> _stateProvinceServices;
        private readonly IServices<Country> _countryServices;
        #endregion
        #region ctor
        public StateProvinceController(
                                IServices<Country> countryServices,
                                IServices<StateProvince> stateProvinceServices,
                                IUser currentUser,
                                IEmailSender emailSender,
                                IHttpContextAccessor accessor,
                                IServices<BusinessEntity> businessEntity
                                ) : base(currentUser, emailSender, accessor, businessEntity)
        {
            this._stateProvinceServices = stateProvinceServices;
            this._countryServices = countryServices;
        }
        #endregion
        #region methods
        // GET: StateProvince
        [Route("stateprovince-management/stateprovince-list")]
        public async Task<IActionResult> List(string search)
        {
            ViewData["search"] = search;
            var data = await _stateProvinceServices.QueryAsync();
            if (!string.IsNullOrEmpty(search))
            {
                data = data.Where(p =>
                            p.StateProvinceCode.Contains(search)
                    || p.Name.Contains(search)
                 );
            }
            return View(data);
        }
        // GET: StateProvince/Add
        [Route("stateprovince-management/stateprovince-add")]
        public IActionResult Add()
        {
            ViewData["CountryID"] = new SelectList(_countryServices.GetAll(), "CountryId", "Name");
            var data = new StateProvince();
            return View(data);
        }
        // POST: StateProvince/Add
        [HttpPost, ValidateAntiForgeryToken]
        [Route("stateprovince-management/stateprovince-add")]
        public async Task<IActionResult> Add([Bind("StateProvinceId,StateProvinceCode,IsOnlyStateProvinceFlag,Name,CountryID")] StateProvince stateProvince, bool continueAdd)
        {
            if (ModelState.IsValid)
            {
                await _stateProvinceServices.AddAsync(stateProvince);
                return continueAdd ? RedirectToAction(nameof(Add)) : RedirectToAction(nameof(List));
            }
            ViewData["CountryID"] = new SelectList(_countryServices.GetAll(), "CountryId", "Name");
            return View(stateProvince);
        }
        // GET: StateProvince/Edit/5
        [Route("stateprovince-management/stateprovince-edit/{id?}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var stateProvince = await _stateProvinceServices.SingleOrDefaultAsync(m => m.StateProvinceId == id);
            if (stateProvince == null)
            {
                return NotFound();
            }
            ViewData["CountryID"] = new SelectList(_countryServices.GetAll(), "CountryId", "Name");
            return View(stateProvince);
        }
        // POST: StateProvince/Edit/5
        [HttpPost, ValidateAntiForgeryToken]
        [Route("stateprovince-management/stateprovince-edit/{id?}")]
        public async Task<IActionResult> Edit(int id, [Bind("StateProvinceId,StateProvinceCode,IsOnlyStateProvinceFlag,Name,CountryID")] StateProvince stateProvince, bool continueAdd)
        {
            if (id != stateProvince.StateProvinceId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    await _stateProvinceServices.UpdateAsync(stateProvince);
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return continueAdd ? RedirectToAction(nameof(Edit), new { id = stateProvince.StateProvinceId }) : RedirectToAction(nameof(List));
            }
            return View(stateProvince);
        }
        [Route("stateprovince-management/stateprovince-delete/{id?}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var stateProvince = await _stateProvinceServices.SingleOrDefaultAsync(m => m.StateProvinceId == id);
            if (stateProvince == null)
            {
                return NotFound();
            }
            return View(stateProvince);
        }
        [HttpPost, ValidateAntiForgeryToken]
        [Route("stateprovince-management/stateprovince-delete/{id?}")]
        public async Task<IActionResult> Delete(StateProvince stateProvince)
        {
            await _stateProvinceServices.DeleteAsync(stateProvince);
            return RedirectToAction(nameof(List));
        }
        #endregion methods
    }
}
