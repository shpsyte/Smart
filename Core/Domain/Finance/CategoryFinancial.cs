using Core.Domain.Base;
using Core.Domain.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Domain.Finance
{
    public partial class CategoryFinancial : BaseEntity
    {
        public CategoryFinancial()
        {
            Expense = new HashSet<Expense>();
            Revenue = new HashSet<Revenue>();
            BankTrans = new HashSet<BankTrans>();
            this.Active = true;

        }


        public int ChartAccountId { get; private set; }

        [Required]
        public string Name { get; set; }

        public int Type { get; set; }
       
        public bool Active { get; set; }

        
        public ICollection<Expense> Expense { get; set; }
        public ICollection<Revenue> Revenue { get; set; }
        public ICollection<BankTrans> BankTrans { get; set; }
    }
}
