using Core.Domain.Finance;
using Core.Domain.Finance.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smart.Models.Components
{
    public class CardFinancialModel
    {
        public IQueryable<VRevenue> Revenues { get; set; }
        public IQueryable<VRevenueTrans> RevenueTrans { get; set; }
        public IQueryable<VExpense> Expenses { get; set; }
        public IQueryable<VExpenseTrans> ExpenseTrans { get; set; }
        public IQueryable<BankTrans> BankTrans { get; set; }
        public IQueryable<VCashFlow> CashFlows { get; set; }
        public int Qty { get; set; }
        public decimal? Amount { get; set; }
        public string HtmlModel { get; set; }
        public string title { get; set; }
        public string cssCard { get; set; }
        public bool payed { get; set; }
        public int time { get; set; }
        public decimal? initial { get; set; }

    }
}
