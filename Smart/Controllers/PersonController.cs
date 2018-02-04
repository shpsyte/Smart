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
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using Data.Context;
using Smart.Models.PersonModel;
using AutoMapper;
using Core.Domain.Region;
using Core.Interfaces;
namespace Smart.Controllers
{
    [Authorize]
    public class PersonController : BaseController
    {
        #region vars
        private readonly IServices<Person> _personServices;
        private readonly IServices<CategoryPerson> _categoryPersonServices;
        private readonly IServices<PersonAddress> _personAddressServices;
        private readonly IServices<Address> _addressServices;
        private readonly IRepository<City> _cityServices;
        private readonly IRepository<StateProvince> _stateProvinceServices;
        #endregion
        #region ctor
        public PersonController(
                                IServices<CategoryPerson> categoryPersonServices,
                                IServices<Person> personServices,
                                IServices<PersonAddress> personAddressServices,
                                IServices<Address> addressServices,
                                IRepository<City> cityServices,
                                IRepository<StateProvince> stateProvinceServices,
                                IUser currentUser,
                                IEmailSender emailSender,
                                IHttpContextAccessor accessor
                                ) : base(currentUser, emailSender, accessor)
        {
            this._personServices = personServices;
            this._categoryPersonServices = categoryPersonServices;
            this._cityServices = cityServices;
            this._stateProvinceServices = stateProvinceServices;
            this._personAddressServices = personAddressServices;
            this._addressServices = addressServices;
        }
        #endregion
        #region methods
        [TempData]
        public string StatusMessage { get; set; }
        // GET: Person
        [Route("person-management/person-list")]
        public async Task<IActionResult> List(string search)
        {
            ViewData["search"] = search;
            var data = await _personServices.QueryAsync();
            data = data.Where(p => p.Deleted == false);
            if (!string.IsNullOrEmpty(search))
            {
                data = data.Where(p =>
                            p.FirstName.Contains(search)
                    || p.LastName.Contains(search)
                    || p.RegistrationCode.Contains(search)
                    || p.RegistrationState.Contains(search)
                    || p.Email.Contains(search)
                    || p.Comments.Contains(search)
                 );
            }
            return View(data);
        }
        // GET: Person/Add
        [Route("person-management/person-add")]
        public IActionResult Add()
        {
            ViewData["CategoryId"] = new SelectList(_categoryPersonServices.GetAll(), "CategoryId", "Name");
            ViewData["CityId"] = new SelectList(_cityServices.GetAll(), "CityId", "Name");
            ViewData["StateProvinceId"] = new SelectList(_stateProvinceServices.GetAll(), "StateProvinceId", "Name");
            var data = new PersonModel();
            return View(data);
        }
        // POST: Person/Add
        [HttpPost, ValidateAntiForgeryToken]
        [Route("person-management/person-add")]
        public async Task<IActionResult> Add([Bind("PersonId,PersonCode,FirstName,LastName,RegistrationCode," +
            "RegistrationState,Type,PersonType,CategoryId,Email,Image,Comments,CreateDate,ModifiedDate,Active,Deleted,BusinessEntityId,avatarImage," +
            "PostalCode,StreetAddress,Number,StreetAddressLine2,StreetAddressLine3,District,CityId,StateProvinceId,SpatialLocation," +
            "CityName","StateProvinceName","CityCode","AddressId","Phone")] PersonModel data, bool continueAdd, IFormFile files)
        {
            var address = Mapper.Map<PersonModel, Address>(data);
            var person = Mapper.Map<PersonModel, Person>(data);
            var addressperson = new PersonAddress() { Address = address, Person = person };
            if (ModelState.IsValid)
            {
                person.PersonCode = (_personServices.Query().Max(p => (int?)p.PersonCode) ?? 0) + 1;
                person.BusinessEntityId = _BusinessId;
                address.BusinessEntityId = _BusinessId;
                try
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        person.avatarImage.CopyTo(memoryStream);
                        person.Image = memoryStream.ToArray();
                    }
                }
                catch {; }
                //await _personServices.AddAsync(person);
                await _personAddressServices.AddAsync(addressperson);
                return continueAdd ? RedirectToAction(nameof(Add)) : RedirectToAction(nameof(List));
            }
            LoadViewData();
            return View(person);
        }
        private void LoadViewData()
        {
            ViewData["CategoryId"] = new SelectList(_categoryPersonServices.GetAll(), "CategoryId", "Name");
            //  ViewData["CityId"] = new SelectList(_cityServices.GetAll(), "CityId", "Name");
            ViewData["StateProvinceId"] = new SelectList(_stateProvinceServices.GetAll(), "StateProvinceId", "Name");
        }
        // GET: Person/Edit/5
        [Route("person-management/person-edit/{id?}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("AccessDenied", "Account");
            }
            var person = await _personServices.SingleOrDefaultAsync(m => m.PersonId == id);
            if (person == null)
            {
                return RedirectToAction("AccessDenied", "Account");
            }
            var addressperson = _personAddressServices.SingleOrDefaultAsync(m => m.PersonId == id).Result;
            var address = await _addressServices.SingleOrDefaultAsync(m => m.AddressId == addressperson.AddressId);
            LoadViewData();
            var editPerson = Mapper.Map<PersonModel>(person);
            editPerson.AddressId = address.AddressId;
            editPerson.PostalCode = address.PostalCode;
            editPerson.StreetAddress = address.StreetAddress;
            editPerson.Number = address.Number;
            editPerson.StreetAddressLine2 = address.StreetAddressLine2;
            editPerson.StreetAddressLine3 = address.StreetAddressLine3;
            editPerson.District = address.District;
            editPerson.CityId = address.CityId;
            editPerson.CityName = address.CityName;
            editPerson.StateProvinceId = address.StateProvinceId;
            editPerson.StateProvinceName = address.StateProvinceName;
            editPerson.CityCode = address.CityCode;
            editPerson.SpatialLocation = address.SpatialLocation;
            return View(editPerson);
        }
        // POST: Person/Edit/5
        [HttpPost, ValidateAntiForgeryToken]
        [Route("person-management/person-edit/{id?}")]
        public async Task<IActionResult> Edit(int id, [Bind("PersonId,PersonCode,FirstName,LastName,RegistrationCode," +
            "RegistrationState,Type,PersonType,CategoryId,Email,Image,Comments,CreateDate,ModifiedDate,Active,Deleted,BusinessEntityId,avatarImage," +
            "PostalCode,StreetAddress,Number,StreetAddressLine2,StreetAddressLine3,District,CityId,StateProvinceId,SpatialLocation," +
            "CityName","StateProvinceName","CityCode","AddressId","Phone")] PersonModel data, bool continueAdd, bool addTrash, IFormFile files)
        {
            if (id != data.PersonId)
            {
                return NotFound();
            }
            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    data.avatarImage.CopyTo(memoryStream);
                    data.Image = memoryStream.ToArray();
                }
            }
            catch {; }
            if (ModelState.IsValid)
            {
                var address = Mapper.Map<PersonModel, Address>(data);
                var person = Mapper.Map<PersonModel, Person>(data);
                person.BusinessEntityId = _BusinessId;
                address.BusinessEntityId = _BusinessId;
                person.ModifiedDate = System.DateTime.UtcNow;
                try
                {
                    await _personServices.UpdateAsyncNoSave(person);
                    await _addressServices.UpdateAsyncNoSave(address);
                    await _personAddressServices.SaveAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return continueAdd ? RedirectToAction(nameof(Edit), new { id = person.PersonId }) : RedirectToAction(nameof(List));
            }
            LoadViewData();
            return View(data);
        }
        public ActionResult Avatar(int id)
        {
            byte[] data;
            try
            {
                data = _personServices.Find(id).Image;
            }
            catch (System.Exception)
            {
                data = null;
            }
            if (data == null)
            { data = new byte[0]; }
            return File(data, "image/png");
        }
        #endregion methods
    }
}
