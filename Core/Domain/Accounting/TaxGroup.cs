using Core.Domain.Base;
using Core.Domain.Business;
using Core.Domain.Production;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Domain.Accounting
{
    public partial class TaxGroup : BaseEntity
    {
        
        public TaxGroup() => Product = new HashSet<Product>();
        public TaxGroup(string name) : this() 
        {
            this.Name = name;
        }

       
        #region property
        [Key]
        public int TaxGroupId { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        #endregion
        public ICollection<Product> Product { get; set; }
    }
}
