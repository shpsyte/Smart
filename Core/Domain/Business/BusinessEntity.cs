using Core.Domain.Accounting;
using Core.Domain.Base;
using Core.Domain.Finance;
using Core.Domain.Identity;
using Core.Domain.PersonAndData;
using Core.Domain.Production;
using Core.Domain.Sale;
using System;
using System.Collections.Generic;

namespace Core.Domain.Business
{
    public partial class BusinessEntity : BaseEntity
    {
       
        public BusinessEntity(string name, string email, string extenalcode)
        {
            this.Name = name;
            this.EmailCreate = email;
            this.ExternalCode = extenalcode;
        }

        public new int BusinessEntityId { get; set; }
        public string Name { get; set; }
        public string EmailCreate { get; set; }
        public string ExternalCode { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? Validate { get; set; }
        public bool? Active { get; set; }
        
    }
}
