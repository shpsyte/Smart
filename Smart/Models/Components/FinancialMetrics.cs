using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smart.Models.Components
{
    public abstract class FinancialMetrics<T>  where T : class
    {
        public IQueryable<T> List { get; set; }
        public int Qty { get; set; }
        public decimal? TotalAmount { get; set; }



        
    }
}
