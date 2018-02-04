using Core.Domain.Base;
using System;
using System.Collections.Generic;

namespace Core.Domain.Finance
{
    public partial class Bank : BaseEntity
    {
        public Bank()
        {
            this.BankTrans = new HashSet<BankTrans>();
            this.RevenueTrans = new HashSet<RevenueTrans>();
            this.ExpenseTrans = new HashSet<ExpenseTrans>();

            this.Active = true;
        }

        public int BankId { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }

        public ICollection<BankTrans> BankTrans { get; set; }
        public ICollection<RevenueTrans> RevenueTrans { get; set; }
        public ICollection<ExpenseTrans> ExpenseTrans { get; set; }
    }
}
