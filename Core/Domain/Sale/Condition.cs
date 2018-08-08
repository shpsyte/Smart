using Core.Domain.Base;
using Core.Domain.Business;
using Core.Domain.Finance;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Domain.Sale
{
    public partial class Condition : BaseEntity
    {
        public Condition()
        {
            this.Revenue = new HashSet<Revenue>();
            this.Expense = new HashSet<Expense>();
            this.RevenueTrans = new HashSet<RevenueTrans>();
            this.ExpenseTrans = new HashSet<ExpenseTrans>();
            this.Active = true;
            this.Deleted = false;
            this.PaymentQty = 1;
        }

        public int ConditionId { get; private set; }
        [Required]
        public string Name { get; set; }
        public int? PaymentQty { get; set; }
        public int? PaymentDays { get; set; }
        public decimal? Tax { get; set; }
        public int? PaymentUse { get; set; }
      
        public bool Active { get; set; }
        public bool Deleted { get; set; }

        
        public ICollection<Revenue> Revenue { get; set; }
        public ICollection<Expense> Expense { get; set; }
        public ICollection<RevenueTrans> RevenueTrans { get; set; }
        public ICollection<ExpenseTrans> ExpenseTrans { get; set; }
    }
}
