using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Core.Domain.Identity
{
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// Custom coluns, this column use for filter data user
        /// </summary>
        public int BusinessEntityId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MidleName { get; set; }
        public byte[] AvatarImage { get; set; }


    }
   
}
