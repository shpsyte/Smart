using Core.Domain.Base;
using Core.Domain.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Domain.PersonAndData
{
    public partial class Email : BaseEntity
    {
        public Email()
        {
            PersonEmail = new HashSet<PersonEmail>();
        }
        public Email(string address, int type) : this()
        {
            Address = address;
            Type = type;
        }

        #region property
        public int EmailId { get; set; }
        [StringLength(250)]
        public string Address { get; set; }
        public int Type { get; set; }
        #endregion

        public ICollection<PersonEmail> PersonEmail { get; set; }
    }
}
