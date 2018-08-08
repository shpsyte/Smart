using Core.Domain.Base;
using Core.Domain.Sale;
using System;
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

        public int WarehouseId { get; private set; }
        [Required]
        public string Name { get; set; }
        public bool DefaultLocation { get; set; }


        public ICollection<Invoice> Invoice { get; set; }
        public ICollection<InvoiceDetail> InvoiceDetail { get; set; }
        public ICollection<ProductInventory> ProductInventory { get; set; }
    }
}
