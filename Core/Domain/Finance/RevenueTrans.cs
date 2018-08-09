using Core.Domain.Base;
using Core.Domain.Sale;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Domain.Finance
{
    public class RevenueTrans : BaseEntity
    {
        public RevenueTrans()
        {
            this.BankTrans = new HashSet<BankTrans>();
        }
        
        public int RevenueTransId { get; private set; }
        public int RevenueId { get; set; }

        public DateTime CreateDate { get; set; }
        public string Midledesc { get; set; }
        public int? ConditionId { get; set; }
        public int? BankId { get; set; }
        public string Description { get; set; }
        public decimal Total { get; set; }
        public int Signal { get; set; }
      
        public Revenue Revenue { get; set; }
        public AccountBank Bank { get; set; }
        public Condition Condition { get; set; }
        public ICollection<BankTrans> BankTrans { get; set; }

    }
}
