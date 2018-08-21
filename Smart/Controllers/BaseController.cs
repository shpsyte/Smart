using Core.Domain.Business;
using Core.Domain.Identity;
using Data.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Services.Interfaces;
using Smart.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Smart.Controllers
{
    public class BaseController : Controller
    {
        protected IUser _currentUser;
        protected IEmailSender _emailSender;
        protected IHttpContextAccessor _accessor;
        protected SmartContext _context;
        protected int _BusinessId;
        private IUser currentUser;
        private IEmailSender emailSender;
        private IHttpContextAccessor accessor;
        protected IServices<BusinessEntity> _businessEntity;
        public BaseController(IUser currentUser, IEmailSender emailSender, IHttpContextAccessor accessor, IServices<BusinessEntity> businessEntity)
        {
            this._currentUser = currentUser;
            this.emailSender = emailSender;
            this.accessor = accessor;
            this._BusinessId = currentUser.BusinessEntityId();
            this._businessEntity = businessEntity;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var entity = _businessEntity.SingleOrDefault();
            DateTime valid = entity.Validate.Value;
            DateTime now = DateTime.Now.Date;
            var days = (valid - now).Days;

            if (days <= 7)
            {
                ViewData["msg"] = $"{entity.Validate.Value.ToShortDateString()}";
            }



            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
          
            
            base.OnActionExecuted(context);
        }

    }
}
