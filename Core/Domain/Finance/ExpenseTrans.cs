using Core.Domain.Base;
using Core.Domain.Sale;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Domain.Finance
{
    public class ExpenseTrans : BaseEntity
    {

        public ExpenseTrans()
        {
            this.BankTrans = new HashSet<BankTrans>();
        }

        public int Id { get; set; }
        public int ExpenseId { get; set; }
        public DateTime CreateDate { get; set; }
        public string Midledesc { get; set; }
        public int? PaymentConditionId { get; set; }
        public int? BankId { get; set; }
        public string Description { get; set; }
        public decimal Total { get; set; }

        public int Signal { get; set; }

        public Expense Expense { get; set; }
        public Bank Bank { get; set; }
        public Condition Condition { get; set; }

        public ICollection<BankTrans> BankTrans { get; set; }
    }
}
