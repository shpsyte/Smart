using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Core.Domain.Accounting;
using Data.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
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
        public HomeController(IUser currentUser, IEmailSender emailSender, IHttpContextAccessor accessor) : base(currentUser, emailSender, accessor)
        {
        }

        public IActionResult About()
        {
            return View();
        }
        public IActionResult Index()
        {
           
            return RedirectToAction("CashFlow", "FinancialStats");
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
