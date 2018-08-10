using Core.Domain.Base;
using Core.Domain.PersonAndData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Domain.Finance.Views
{
    public class VCashFlow : BaseEntity, IView
    {
        
        public int Id { get; set; }
        public string Tp { get; set; }
        public int CashFlowSeq { get; set; }
        public int CashFlowTotalSeq { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public int? PersonId { get; set; }
        public string CashFlowNumber { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DueDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DuePayment { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]

        public DateTime? CreateDate { get; set; }
        public decimal Total { get; set; }
        public decimal Saldo { get; set; }
        public Person Person { get; set; }

        
        public override string ToString() => string.Concat(this.CashFlowNumber, "-", this.CashFlowSeq);
    }
}
