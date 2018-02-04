using Core.Domain.Finance.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smart.Models.Financial
{
    public class ExpenseAndRevenueModel 
    {
        public IQueryable<VRevenue> Revenues { get; set; }
        public IQueryable<VExpense> Expenses { get; set; }
        public int Qty { get; set; }
        public decimal? AmountToLate { get; set; }
        public decimal? AmountToday { get; set; }
        public decimal? AmountAfter { get; set; }
        public decimal? AmountTotal { get; set; }
        public decimal? AmoutProvision { get; set; }
        
    }
}
