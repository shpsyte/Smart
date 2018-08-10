using Core.Domain.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Core.Domain.Finance
{
    public partial class CostCenter : BaseEntity
    {
        
        public CostCenter()
        {
            Expense = new HashSet<Expense>();
            Revenue = new HashSet<Revenue>();
            this.Active = true;
        }
        public CostCenter(string name) : this()
        {
            this.Name = name;
        }
       
        #region property
        public int CostCenterId { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public bool Active { get; set; }
        #endregion

        public ICollection<Expense> Expense { get; set; }
        public ICollection<Revenue> Revenue { get; set; }
    }
}
