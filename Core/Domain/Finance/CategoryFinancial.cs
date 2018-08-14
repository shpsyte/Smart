using Core.Domain.Base;
using Core.Domain.Finance.Views;
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
        public CategoryFinancial(string name, int type) : this()
        {
            Name = name;
            Type = type;
        }
       
        #region property
        public int ChartAccountId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public int Type { get; set; }
       
        public bool Active { get; set; }
        #endregion

        public ICollection<Expense> Expense { get; set; }
        public ICollection<VExpense> VExpense { get; set; }
        public ICollection<VExpenseTrans> VExpenseTrans { get; set; }

        public ICollection<Revenue> Revenue { get; set; }
        public ICollection<VRevenue> VRevenue { get; set; }
        public ICollection<VRevenueTrans> VRevenueTrans { get; set; }
        public ICollection<VCashFlow> VCashFlow { get; set; }
        public ICollection<BankTrans> BankTrans { get; set; }
        
    }
}
