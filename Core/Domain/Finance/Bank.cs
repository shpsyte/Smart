using Core.Domain.Base;
using Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        public int AccountBankId { get; private set; }

        [Required]
        public string Name { get; set; }

        public string Code { get; set; }
        public string Agency { get; set; }
        public string DigitAgency { get; set; }
        public string Account { get; set; }
        public string DigitAccount { get; set; }

        public bool Active { get; set; }

        public ICollection<BankTrans> BankTrans { get; set; }
        public ICollection<RevenueTrans> RevenueTrans { get; set; }
        public ICollection<ExpenseTrans> ExpenseTrans { get; set; }
    }
}
