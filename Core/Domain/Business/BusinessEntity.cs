using Core.Domain.Base;
using Core.Domain.PersonAndData;
using Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Domain.Business
{
    public partial class BusinessEntity : BaseEntity
    {
       
        public BusinessEntity(SingleName name, EmailAddress email, string extenalcode)
        {
            this.Name = name;
            this.Email = email;
            this.ExternalCode = extenalcode;
            this.CreateDate = DateTime.UtcNow;
            this.Validate = DateTime.UtcNow.AddDays(7);
            this.Active = true;
        }

        public new int BusinessEntityId { get; private set; }

        [Required]
        [StringLength(50)]
        public SingleName Name { get; set; }

        [Required]
        [StringLength(255)]
        
        public EmailAddress Email { get; private set; }

        [Required]
        public string ExternalCode { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        public DateTime? Validate { get; set; }
        public bool? Active { get; set; }


        public bool CheckIsValid() => this.Validate > DateTime.Now;



    }
}
