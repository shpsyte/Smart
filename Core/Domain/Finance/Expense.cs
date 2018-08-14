using Core.Domain.Base;
using Core.Domain.Finance.Views;
using Core.Domain.PersonAndData;
using Core.Domain.Sale;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Domain.Finance
{
    public partial class Expense : BaseEntity
    {
         
        public Expense()
        {
            this.ExpenseTrans = new HashSet<ExpenseTrans>();
            this.DueDate = System.DateTime.Now.AddDays(7);
            this.CreateDate = ModelExtension.TimestampProvider();
        }

       

        public Expense(string expenseNumber, int? expenseSeq, int? expenseTotalSeq, string name, int? personId, int? categoryId, int? costCenterId, int? conditionId, decimal total, DateTime? createDate, DateTime? dueDate, DateTime? duePayment, string comment) : this()
        {
            ExpenseNumber = expenseNumber;
            ExpenseSeq = expenseSeq;
            ExpenseTotalSeq = expenseTotalSeq;
            Name = name;
            PersonId = personId;
            ChartAccountId = categoryId;
            CostCenterId = costCenterId;
            ConditionId = conditionId;
            Total = total;
            CreateDate = createDate;
            DueDate = dueDate;
            DuePayment = duePayment;
            Comment = comment;
        }
         
        #region property

        public int ExpenseId { get; set; }
        [StringLength(80)]
        public string ExpenseNumber { get; set; }

        public int? ExpenseSeq { get; set; }
        public int? ExpenseTotalSeq { get; set; }
        [Required]
        [StringLength(80)]
        public string Name { get; set; }
        public int? PersonId { get; set; }
        public int? ChartAccountId { get; set; }
        public int? CostCenterId { get; set; }
        public int? ConditionId { get; set; }
        public decimal Total { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? CreateDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DueDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DuePayment { get; set; }
        public string Comment { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool Deleted { get; set; }
        #endregion

        public CategoryFinancial CategoryFinancial { get; set; }
        public CostCenter CostCenter { get; set; }
        public Condition PaymentCondition { get; set; }
        public Person Person { get; set; }
        public ICollection<ExpenseTrans> ExpenseTrans { get; set; }
        public VExpense VExpense  { get; set; }
    }
}
