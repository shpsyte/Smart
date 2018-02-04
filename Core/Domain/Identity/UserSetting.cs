using Core.Domain.Base;
using Core.Domain.Business;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.Identity
{
    public partial class UserSetting : BaseEntity
    {
        public int UserSettingId { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MidleName { get; set; }
        public byte[] AvatarImage { get; set; }

        

    }
}
