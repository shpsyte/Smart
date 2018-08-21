using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Core.Domain.Accounting;
using Core.Domain.Business;
using Core.Domain.Finance;
using Data.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using Smart.Configuration;
using Smart.Models;
using Smart.Services;
namespace Smart.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {

        private readonly IServices<AccountBank> _bankServices;

        

        public HomeController(IUser currentUser, IEmailSender emailSender, IHttpContextAccessor accessor, IServices<AccountBank> bankServices, IServices<BusinessEntity> businessEntity) : base(currentUser, emailSender, accessor, businessEntity)
        {
            _bankServices = bankServices;
        }


        public IActionResult About()
        {
            return View();
        }
        public IActionResult Index(string s = null)
        {
            ViewData["Message"] = s;
            return View();
        }

        [Route("home-free-account-for-me")]
        public async Task<IActionResult> Free()
        {
            var myAccount = _businessEntity.SingleOrDefault();
            myAccount.Validate = System.DateTime.Now.AddDays(30);
            await _businessEntity.UpdateAsync(myAccount);
             
        
            return RedirectToAction("Index", new { s = "Renovação Efetuada com Sucesso" });
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [Route("home-blank-page-for-developers")]
        public IActionResult Blankpage()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );
            return LocalRedirect(returnUrl);
        }
    }
}
