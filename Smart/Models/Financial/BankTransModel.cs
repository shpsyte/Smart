using Core.Domain.Finance;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Smart.Models.Financial
{
    public class BankTransModel
    {
        public IQueryable<BankTrans> bankTrans { get; set; }
        public decimal? AmountBefore { get; set; }
        public decimal? Revenue { get; set; }
        public decimal? Expense { get; set; }
        public decimal? AmountData { get; set; }
        public decimal? AmountTotal { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? start { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? end { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? duedate { get; set; }
    }
}
