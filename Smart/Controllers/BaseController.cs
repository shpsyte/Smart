using Core.Domain.Identity;
using Data.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public BaseController(IUser currentUser, IEmailSender emailSender, IHttpContextAccessor accessor) 
        {
            this._currentUser = currentUser;
            this.emailSender = emailSender;
            this.accessor = accessor;
            this._BusinessId = currentUser.BusinessEntityId();
        }
        public BaseController(IUser currentUser,  IEmailSender emailSender,  IHttpContextAccessor accessor, SmartContext context)
        {
            this._currentUser = currentUser;
            this._emailSender = emailSender;
            this._accessor = accessor;
            this._BusinessId = currentUser.BusinessEntityId();
            this._context = context;
        }
    }
}
