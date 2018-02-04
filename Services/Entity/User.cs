using Core.Domain.Identity;
using Data.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.Entity
{
    public class User : IUser
    {

        private readonly IHttpContextAccessor _accessor;
        private SmartContext _context;
        private readonly UserManager<ApplicationUser> _userManager;


        public User(IHttpContextAccessor acessor, SmartContext context , UserManager<ApplicationUser> userManager)
        {
            this._accessor = acessor;
            this._context = context;
            this._userManager = userManager;
        }


        public string Id() => _accessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

        public string UserName() => _accessor.HttpContext.User.Identity.Name;

        public string Email() => _accessor.HttpContext.User.Identity.Name;

        public IEnumerable<Claim> GetClaimsIdentity() => _accessor.HttpContext.User.Claims;

        public bool IsAuthenticated() => _accessor.HttpContext.User.Identity.IsAuthenticated;

        public string NickName() => _userManager.GetUserAsync(_accessor.HttpContext.User).Result.MidleName;

        public byte[] AvatarImage() => _accessor.HttpContext.Session.Get("User.Settings.AvatarImage");

        public int BusinessEntityId()
        {
            var user = _userManager.GetUserAsync(_accessor.HttpContext.User).Result;
            if (user != null)
            {
                return user.BusinessEntityId;
            }else
            {
                return 0;
            }

        }
    }
}
