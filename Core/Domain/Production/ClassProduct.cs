using Core.Domain.Base;
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
        public ClassProduct(string name)
        {
            this.Name = name;
        }

        #region property

        public int ClassId { get; set; }
        
        [Required]
        [StringLength(120)]
        public string Name { get; set; }

        #endregion


        public ICollection<Product> Product { get; set; }
    }
}
