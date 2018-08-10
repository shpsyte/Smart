using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Core.Domain.Identity
{
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// Custom coluns, this column use for filter data user
        /// </summary>
        public int BusinessEntityId { get; set; }
        [StringLength(150)]
        public string FirstName { get; set; }
        [StringLength(150)]
        public string LastName { get; set; }
        [StringLength(100)]
        public string MidleName { get; set; }
        public byte[] AvatarImage { get; set; }


    }
   
}
