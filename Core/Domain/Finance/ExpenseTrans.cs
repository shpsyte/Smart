using Core.Domain.Base;
using Core.Domain.Sale;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Core.Domain.Finance
{
    public class ExpenseTrans : BaseEntity
    {
         
        public ExpenseTrans()
        {
            this.BankTrans = new HashSet<BankTrans>();
            this.CreateDate = System.DateTime.UtcNow;
        }

        public ExpenseTrans(string description, string midledesc, int? conditionId, int? bankId, decimal total, int signal)
        {
            this.Description = description;
            this.Midledesc = midledesc;
            this.ConditionId = conditionId;
            this.BankId = bankId;
            this.Total = total;
            this.Signal = signal;
        }
         
        #region property

        public int ExpenseTransId { get; set; }
        public int ExpenseId { get; set; }
        public DateTime CreateDate { get; set; }
        [StringLength(150)]
        public string Description { get; set; }
        [StringLength(100)]
        public string Midledesc { get; set; }
        public int? ConditionId { get; set; }
        public int? BankId { get; set; }
        public decimal Total { get; set; }

        public int Signal { get; set; }
        #endregion

        public Expense Expense { get; set; }
        public AccountBank Bank { get; set; }
        public Condition Condition { get; set; }
        public ICollection<BankTrans> BankTrans { get; set; }
    }
}
