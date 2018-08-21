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
using Data.Repository;

namespace Smart.Controllers
{
    [Authorize]    
    public class CountryController : BaseController
    {
        #region vars
        private readonly  IServices<Country> _countryServices;
        #endregion
        #region ctor
            public CountryController(
                                    IServices<Country> countryServices, 
                                    IUser currentUser, 
                                    IEmailSender emailSender, 
                                    IHttpContextAccessor accessor,
                                    IServices<Core.Domain.Business.BusinessEntity> businessEntity
                                    ) : base(currentUser, emailSender, accessor, businessEntity)
            {
            this._countryServices = countryServices;
            }
        #endregion
        #region methods
        // GET: Country
        [Route("country-management/country-list")]
        public async Task<IActionResult> List(string search)
        {
            ViewData["search"] = search;
            var data = await _countryServices.QueryAsync();
            if (!string.IsNullOrEmpty(search)) 
            {
               data = data.Where(p =>  
                           p.Name.Contains(search)
                   || p.MiddleName.Contains(search)
                   || p.CountryRegionCode.Contains(search)
                   || p.SpecialCodeRegion.Contains(search)
                );
            }
               return View(data);
        }
       // GET: Country/Add
        [Route("country-management/country-add")]
        public IActionResult Add()
        {
          var data = new Country();
          return View(data);
        }
        // POST: Country/Add
        [HttpPost, ValidateAntiForgeryToken]
        [Route("country-management/country-add")]
        public async Task<IActionResult> Add([Bind("CountryId,Name,MiddleName,CountryRegionCode,SpecialCodeRegion")] Country country, bool continueAdd)
        {
            if (ModelState.IsValid)
            {
                 await _countryServices.AddAsync(country);
                 return continueAdd ? RedirectToAction(nameof(Add)) : RedirectToAction(nameof(List));
            }
            return View(country);
        }
        // GET: Country/Edit/5
        [Route("country-management/country-edit/{id?}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var country = await _countryServices.SingleOrDefaultAsync(m => m.CountryId == id);
            if (country == null)
            {
                return NotFound();
            }
            return View(country);
        }
        // POST: Country/Edit/5
        [HttpPost, ValidateAntiForgeryToken]
        [Route("country-management/country-edit/{id?}")]
        public async Task<IActionResult> Edit(int id, [Bind("CountryId,Name,MiddleName,CountryRegionCode,SpecialCodeRegion")] Country country, bool continueAdd)
        {
            if (id != country.CountryId)
            {
                return NotFound();
            }
                 if (ModelState.IsValid)
            {
                try
                {
                    await _countryServices.UpdateAsync(country);
                }
                catch (DbUpdateConcurrencyException)
                {
                        throw;
                }
                return continueAdd ? RedirectToAction(nameof(Edit), new { id = country.CountryId }) : RedirectToAction(nameof(List));
            }
            return View(country);
        }
        [Route("country-management/country-delete/{id?}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
               var country = await  _countryServices.SingleOrDefaultAsync(m => m.CountryId == id);
               if (country == null)
               {
                    return NotFound();
                }
                    return View(country);
         }  
         [HttpPost, ValidateAntiForgeryToken]
         [Route("country-management/country-delete/{id?}")]
         public async Task<IActionResult> Delete (Country country)
         {
             await _countryServices.DeleteAsync(country);
             return RedirectToAction(nameof(List));
         }
        #endregion methods
    }
}
