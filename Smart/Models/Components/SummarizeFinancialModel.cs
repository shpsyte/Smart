using Core.Domain.Finance;
using Core.Domain.Finance.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smart.Models.Components
{
    public class SummarizeFinancialModel  
    {
        public decimal? RevenueTotal { get; set; }
        public decimal? ExpenseTotal { get; set; }
        public decimal? PreviousAmmont { get; set; }
        public decimal? Expected { get; set; }
        public bool HasRow { get; set; }
        public string RenderView { get; set; }

        public IQueryable<VExpense> Expenses { get; set; }
        public IQueryable<VRevenue> Revenues { get; set; }
        public IQueryable<BankTrans> Balances { get; set; }
        


    }
}
