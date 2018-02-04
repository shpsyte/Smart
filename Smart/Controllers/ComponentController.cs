using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Smart.Services;
namespace Smart.Controllers
{
    public class ComponentController : BaseController
    {
        public ComponentController(IUser currentUser, IEmailSender emailSender, IHttpContextAccessor accessor) : base(currentUser, emailSender, accessor)
        {
        }
        [Route("easypie-charts")]
        public IActionResult EasyPieCharts() => View();
        [Route("sparkline-charts")]
        public IActionResult SparkLine() => View();
        [Route("flot-chats")]
        public IActionResult FlotCharts() => View();
        [Route("morris-chats")]
        public IActionResult MorrisCharts() => View();
        [Route("high-charts")]
        public IActionResult HighChartTable() => View();
        public IActionResult DataTables() => View();
    }
}