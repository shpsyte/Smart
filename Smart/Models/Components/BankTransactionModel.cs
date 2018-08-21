using Core.Domain.Finance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smart.Models.Components
{
    public class BankTransactionModel : FinancialMetrics<BankTrans>
    {
       public string ViewModel { get; set; }
    }
}
