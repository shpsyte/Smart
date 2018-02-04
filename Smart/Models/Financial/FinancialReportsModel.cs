using Core.Domain.Finance;
using Core.Domain.Finance.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Smart.Models.Financial
{
    public class FinancialReportsModel
    {
        public FinancialReportsModel()
        {
          //  this.createDateStart = System.DateTime.Now.Date;
           // this.createDateEnd = System.DateTime.Now.Date;
        }
        public IEnumerable<VExpense> VExpense { get; set; }
        public IEnumerable<VRevenue> VRevenue { get; set; }
        public IEnumerable<BankTrans> BankTrans { get; set; }
        public IEnumerable<VCashFlow> VCashFlow { get; set; }
        public string suplier { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? dueDateStart { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? dueDateEnd { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? duePayStart { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? duePayEnd { get; set; }
        public string name { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? createDateStart { get; set; }
        
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? createDateEnd { get; set; }
        public int? CategoryId { get; set; }
        public int? BankId { get; set; }
        public decimal? value { get; set; }
        public string ExpenseNumber { get; set; }
        public bool? Deleted { get; set; }



    }
}
