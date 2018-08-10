using Core.Domain.Base;
using Core.Domain.Sale;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Core.Domain.Finance
{
    public class RevenueTrans : BaseEntity
    {
       
        public RevenueTrans()
        {
            this.BankTrans = new HashSet<BankTrans>();
        }
        public RevenueTrans(DateTime createDate, string midledesc, int? conditionId, int? bankId, string description, decimal total, int signal) : this()
        {
            CreateDate = createDate;
            Midledesc = midledesc;
            ConditionId = conditionId;
            BankId = bankId;
            Description = description;
            Total = total;
            Signal = signal;
        }
        
        #region property
        public int RevenueTransId { get; set; }
        public int RevenueId { get; set; }
        [StringLength(150)]
        public string Description { get; set; }
        [StringLength(100)]
        public string Midledesc { get; set; }
        public int? ConditionId { get; set; }
        public int? BankId { get; set; }
        public decimal Total { get; set; }
        public int Signal { get; set; }
        public DateTime CreateDate { get; set; }
        #endregion

        public Revenue Revenue { get; set; }
        public AccountBank Bank { get; set; }
        public Condition Condition { get; set; }
        public ICollection<BankTrans> BankTrans { get; set; }

    }
}
