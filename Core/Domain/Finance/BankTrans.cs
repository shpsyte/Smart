using Core.Domain.Base;
using System;
using System.ComponentModel.DataAnnotations;
namespace Core.Domain.Finance
{
    public partial class BankTrans : BaseEntity
    {
        public BankTrans()
        {
            this.DueDate = ModelExtension.TimestampProvider();
            this.Deleted = false;
        }
        public BankTrans(string description, DateTime createDate, DateTime? dueDate, string midleDesc, int? expenseTransId, int? revenueTransId, int? categoryId, decimal total, int signal) : this()
        {
            Description = description;
            CreateDate = createDate;
            DueDate = dueDate;
            MidleDesc = midleDesc;
            ExpenseTransId = expenseTransId;
            RevenueTransId = revenueTransId;
            ChartAccountId = categoryId;
            Total = total;
            Signal = signal;
        }
        #region property
        public int BankTransId { get; set; }
        public int AccountBankId { get; set; }
        [StringLength(150)]
        public string Description { get; set; }

      
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DueDate { get; set; }
        [StringLength(100)]
        public string MidleDesc { get; set; }
        public int? ExpenseTransId { get; set; }
        public int? RevenueTransId { get; set; }
        public int? ChartAccountId { get; set; }
        public int? ExcludeId { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CreateDate { get; set; }
        public decimal Total { get; set; }
        public int Signal { get; set; }
        public bool? Deleted { get; set; }

        #endregion
        public AccountBank Bank { get; set; }
        public CategoryFinancial CategoryFinancial { get; set; }
        public RevenueTrans RevenueTrans { get; set; }
        public ExpenseTrans ExpenseTrans { get; set; }
    }
}
