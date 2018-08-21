using Core.Domain.Finance.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smart.Models.Components
{
    public class BalanceAccountModel : FinancialMetrics<VBalanceAccount>
    {
        public string RenderView { get; set; }
    }
}
