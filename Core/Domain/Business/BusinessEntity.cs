using Core.Domain.Base;
using Core.ValueObjects;
using System;
using System.ComponentModel.DataAnnotations;
namespace Core.Domain.Business
{
    public partial class BusinessEntity : BaseEntity
    {
        public BusinessEntity()
        {

        }
        public BusinessEntity(SingleName name, EmailAddress email, string extenalcode) : this()
        {
            this.Name = name;
            this.Email = email;
            this.ExternalCode = extenalcode;
            this.CreateDate = ModelExtension.TimestampProvider();
            this.Validate = ModelExtension.TimestampProvider().AddDays(7);
            this.Active = true;
        }

        #region property
        public new int BusinessEntityId { get; set; }

        [Required]
        [StringLength(50)]
        public SingleName Name { get; set; }

        [Required]
        [StringLength(255)]
        public EmailAddress Email { get; private set; }

        [Required]
        [StringLength(80)]
        public string ExternalCode { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime? Validate { get; set; }
        public bool? Active { get; set; }

        #endregion

        public bool CheckIsValid() => this.Validate > DateTime.Now;
    }
}
