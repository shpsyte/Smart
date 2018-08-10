using Core.Domain.Base;
using Core.Domain.Sale;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Core.Domain.Production
{
    public partial class Location : BaseEntity
    {
        public Location()
        {
            Invoice = new HashSet<Invoice>();
            InvoiceDetail = new HashSet<InvoiceDetail>();
            ProductInventory = new HashSet<ProductInventory>();
            this.DefaultLocation = false;
        }

        public Location(string name, bool defaultLocation) : this()
        {
            Name = name;
            DefaultLocation = defaultLocation;
        }

        #region property
        public int WarehouseId { get; set; }
        [Required]
        [StringLength(120)]
        public string Name { get; set; }
        public bool DefaultLocation { get; set; }
        #endregion


        public ICollection<Invoice> Invoice { get; set; }
        public ICollection<InvoiceDetail> InvoiceDetail { get; set; }
        public ICollection<ProductInventory> ProductInventory { get; set; }
    }
}
