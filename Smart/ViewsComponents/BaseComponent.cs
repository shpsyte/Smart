using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smart.ViewsComponents
{
    public class BaseComponent : ViewComponent
    {
        protected readonly IUser _currentUser;
        protected IHttpContextAccessor _accessor;

        public BaseComponent(IUser currentuserServices, IHttpContextAccessor accessor)
        {
            this._currentUser = currentuserServices;
            this._accessor = accessor;
        }


        public IViewComponentResult Invoke()
        {
            return View();

        }
    }
}
