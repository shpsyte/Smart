using Core.Domain.Base;
using Core.Domain.Business;
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
        }

        public int ExpenseId { get; set; }
        public string ExpenseNumber { get; set; }

        public int? ExpenseSeq { get; set; }
        public int? ExpenseTotalSeq { get; set; }
        [Required]
        public string Name { get; set; }
        public int? PersonId { get; set; }
        public int? CategoryId { get; set; }
        public int? CostCenterId { get; set; }
        public int? PaymentConditionId { get; set; }
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

        
        public CategoryFinancial CategoryFinancial { get; set; }
        public CostCenter CostCenter { get; set; }
        public Condition PaymentCondition { get; set; }
        public Person Person { get; set; }
        public ICollection<ExpenseTrans> ExpenseTrans { get; set; }
        public VExpense VExpense  { get; set; }
    }
}
