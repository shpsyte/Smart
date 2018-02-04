using Core.Domain.Base;
using Core.Domain.Business;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Domain.Production
{
    public partial class CategoryProduct : BaseEntity
    {
        public CategoryProduct()
        {
            Product = new HashSet<Product>();
        }

        public int CategoryId { get; set; }
        [Required]
        public string Name { get; set; }

        
        public ICollection<Product> Product { get; set; }
    }
}
