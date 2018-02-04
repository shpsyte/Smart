using Core.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{


    public interface IUser
    {
        string Id();
        string UserName();
        string Email();
        int BusinessEntityId();
        bool IsAuthenticated();
        IEnumerable<Claim> GetClaimsIdentity();
        string NickName();
        byte[] AvatarImage();


    }
}
