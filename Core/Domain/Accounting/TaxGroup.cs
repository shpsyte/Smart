using Core.Domain.Base;
using Core.Domain.Business;
using Core.Domain.Production;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Domain.Accounting
{
    public partial class TaxGroup : BaseEntity
    {
        public TaxGroup()
        {
            Product = new HashSet<Product>();
        }

        [Key]
        public int TaxGroupId { get; set; }
        [Required]
        public string Name { get; set; }

        
        public ICollection<Product> Product { get; set; }
    }
}
