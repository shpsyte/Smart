using Core.Domain.Base;
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
        public CategoryProduct(string name)
        {
            this.Name = name;
        }


        #region property
        public int CategoryId { get; set; }
        [Required]
        [StringLength(120)]
        public string Name { get; set; }
        #endregion


        public ICollection<Product> Product { get; set; }
    }
}
