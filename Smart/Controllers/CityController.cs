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
    public class CityController : BaseController
    {
        #region vars
        private readonly IServices<City> _cityServices;
        private readonly IServices<StateProvince> _stateProvinceServices;
        #endregion
        #region ctor

        
        public CityController(
                                IServices<StateProvince> stateProvinceServices,
                                IServices<City> cityServices,
                                IUser currentUser,
                                IEmailSender emailSender,
                                IHttpContextAccessor accessor,
                                IServices<BusinessEntity> businessEntity
                                ) : base(currentUser, emailSender, accessor, businessEntity)
        {
            this._cityServices = cityServices;
            this._stateProvinceServices = stateProvinceServices;
        }

     
        #endregion
        #region methods
        // GET: City
        [Route("city-management/city-list")]
        public async Task<IActionResult> List(string search)
        {
            ViewData["search"] = search;
            var data = await _cityServices.QueryAsync();
            if (!string.IsNullOrEmpty(search))
            {
                data = data.Where(p =>
                       p.Name.Contains(search)
                    || p.MiddleName.Contains(search)
                    || p.SpecialCodeRegion.Contains(search)
                    || p.StateProvince.Name.Contains(search)
                    || p.StateProvince.StateProvinceCode.Contains(search)
                 );
            }
            return View(data.Include(a => a.StateProvince));
        }
        // GET: City/Add
        [Route("city-management/city-add")]
        public IActionResult Add()
        {
            ViewData["StateProvinceId"] = new SelectList(_stateProvinceServices.GetAll(), "StateProvinceId", "Name");
            var data = new City();
            return View(data);
        }
        // POST: City/Add
        [HttpPost, ValidateAntiForgeryToken]
        [Route("city-management/city-add")]
        public async Task<IActionResult> Add([Bind("CityId,Name,MiddleName,SpecialCodeRegion,StateProvinceId")] City city, bool continueAdd)
        {
            if (ModelState.IsValid)
            {
                await _cityServices.AddAsync(city);
                return continueAdd ? RedirectToAction(nameof(Add)) : RedirectToAction(nameof(List));
            }
            ViewData["StateProvinceId"] = new SelectList(_stateProvinceServices.GetAll(), "StateProvinceId", "Name");
            return View(city);
        }
        // GET: City/Edit/5
        [Route("city-management/city-edit/{id?}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var city = await _cityServices.SingleOrDefaultAsync(m => m.CityId == id);
            if (city == null)
            {
                return NotFound();
            }
            ViewData["StateProvinceId"] = new SelectList(_stateProvinceServices.GetAll(), "StateProvinceId", "Name");
            return View(city);
        }
        // POST: City/Edit/5
        [HttpPost, ValidateAntiForgeryToken]
        [Route("city-management/city-edit/{id?}")]
        public async Task<IActionResult> Edit(int id, [Bind("CityId,Name,MiddleName,SpecialCodeRegion,StateProvinceId")] City city, bool continueAdd)
        {
            if (id != city.CityId)
            {
                return NotFound();
            }
            ViewData["StateProvinceId"] = new SelectList(_stateProvinceServices.GetAll(), "StateProvinceId", "Name");
            if (ModelState.IsValid)
            {
                try
                {
                    await _cityServices.UpdateAsync(city);
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return continueAdd ? RedirectToAction(nameof(Edit), new { id = city.CityId }) : RedirectToAction(nameof(List));
            }
            return View(city);
        }
        [Route("city-management/city-delete/{id?}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var city = await _cityServices.SingleOrDefaultAsync(m => m.CityId == id);
            if (city == null)
            {
                return NotFound();
            }
            return View(city);
        }
        [HttpPost, ValidateAntiForgeryToken]
        [Route("city-management/city-delete/{id?}")]
        public async Task<IActionResult> Delete(City city)
        {
            await _cityServices.DeleteAsync(city);
            return RedirectToAction(nameof(List));
        }
        #endregion methods
    }
}
