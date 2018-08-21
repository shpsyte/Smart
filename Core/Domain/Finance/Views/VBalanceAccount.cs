using Core.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.Finance.Views
{
    public class VBalanceAccount : BaseEntity, IView
    {
        #region properties
         public int AccountBankId { get; set; }
        public string Name { get; set; }
        public decimal Saldo { get; set; }
        #endregion

    }
}
