using Core.Domain.Base;
using Core.Domain.PersonAndData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
namespace Core.Domain.Finance.Views
{
    public   class VExpenseTrans : BaseEntity, IView
    {
        
        public int ExpenseTransId { get; set; }
        public int ExpenseId { get; set; }
        public int ExpenseSeq { get; set; }
        public int ExpenseTotalSeq { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public int? PersonId { get; set; }
        public string ExpenseNumber { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? CreateDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DueDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DuePayment { get; set; }
        public decimal Total { get; set; }
        public decimal AmountFinal { get; set; }
        public decimal Credit { get; set; }
        public decimal Debit { get; set; }
        public int Signal { get; set; }
        

        public override string ToString() => string.Concat(this.Name, "-", this.ExpenseNumber.ToString());
        
        public VExpense VExpense { get; set; }
        public Person Person { get; set; }
    }
}
