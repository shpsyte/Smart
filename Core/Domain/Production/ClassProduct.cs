using Core.Domain.Base;
using Core.Domain.Business;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Domain.Production
{
    public partial class ClassProduct : BaseEntity
    {
        public ClassProduct()
        {
            Product = new HashSet<Product>();
        }

        public int ClassId { get; private set; }
        
        [Required]
        public string Name { get; set; }

        
        public ICollection<Product> Product { get; set; }
    }
}
