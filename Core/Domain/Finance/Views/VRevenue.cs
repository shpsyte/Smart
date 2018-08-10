using Core.Domain.Base;
using Core.Domain.PersonAndData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
namespace Core.Domain.Finance.Views
{
    public  class VRevenue : BaseEntity, IView
    {

      
        public int RevenueId { get; set; }
        public int RevenueSeq { get; set; }
        public int RevenueTotalSeq { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public int? PersonId { get; set; }
        public string RevenueNumber { get; set; }
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
        

        public override string ToString() => string.Concat(this.RevenueNumber, "-", this.RevenueSeq);
        public Person Person { get; set; }

        public Revenue Revenue { get; set; }
        public ICollection<VRevenueTrans> VRevenueTrans { get; set; }

    }
}
