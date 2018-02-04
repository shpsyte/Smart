using Core.Domain.Base;
using Core.Domain.Sale;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Domain.Production
{
    public partial class Warehouse : BaseEntity
    {
        public Warehouse()
        {
            Invoice = new HashSet<Invoice>();
            InvoiceDetail = new HashSet<InvoiceDetail>();
        }

        public int WarehouseId { get; set; }
        [Required]
        public string Name { get; set; }
     

        public ICollection<Invoice> Invoice { get; set; }
        public ICollection<InvoiceDetail> InvoiceDetail { get; set; }
    }
}
