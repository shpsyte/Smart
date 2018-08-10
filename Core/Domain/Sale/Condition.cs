using Core.Domain.Base;
using Core.Domain.Finance;
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
        public Condition(string name, int? paymentQty, int? paymentDays, decimal? tax, int? paymentUse)
        {
            Name = name;
            PaymentQty = paymentQty;
            PaymentDays = paymentDays;
            Tax = tax;
            PaymentUse = paymentUse;
        }

        #region property
        public int ConditionId { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public int? PaymentQty { get; set; }
        public int? PaymentDays { get; set; }
        public decimal? Tax { get; set; }
        public int? PaymentUse { get; set; }
      
        public bool Active { get; set; }
        public bool Deleted { get; set; }
        #endregion


        public ICollection<Revenue> Revenue { get; set; }
        public ICollection<Expense> Expense { get; set; }
        public ICollection<RevenueTrans> RevenueTrans { get; set; }
        public ICollection<ExpenseTrans> ExpenseTrans { get; set; }
    }
}
