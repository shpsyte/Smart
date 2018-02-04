using Core.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Domain.Finance
{
    public partial class BankTrans : BaseEntity
    {
        public BankTrans()
        {
            this.DueDate = System.DateTime.UtcNow;
            this.Deleted = false;
        }
        public int Id { get; set; }
        public int BankId { get; set; }
        public string Description { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CreateDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DueDate { get; set; }
        public string MidleDesc { get; set; }
        public int? ExpenseTransId { get; set; }
        public int? RevenueTransId { get; set; }
        public int? CategoryId { get; set; }
        public int? ExcludeId { get; set; }
        public decimal Total { get; set; }
        public int Signal { get; set; }

        public bool? Deleted { get; set; }
        public Bank Bank { get; set; }
        public CategoryFinancial CategoryFinancial { get; set; }
        public RevenueTrans RevenueTrans { get; set; }
        public ExpenseTrans ExpenseTrans { get; set; }
    }
}
