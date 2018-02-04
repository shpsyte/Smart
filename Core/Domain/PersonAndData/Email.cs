using Core.Domain.Base;
using Core.Domain.Business;
using System;
using System.Collections.Generic;

namespace Core.Domain.PersonAndData
{
    public partial class Email : BaseEntity
    {
        public Email()
        {
            PersonEmail = new HashSet<PersonEmail>();
        }

        public int EmailId { get; set; }
        public string Email1 { get; set; }
        public int Type { get; set; }

        
        public ICollection<PersonEmail> PersonEmail { get; set; }
    }
}
