using Core.Domain.Base;
using Core.Domain.Business;
using Core.Domain.Production;
using System;
using System.Collections.Generic;

namespace Core.Domain.Sale
{
    public partial class InvoiceDetail : BaseEntity
    {
        public int InvoiceId { get; set; }
        public int InvoiceDetailId { get; set; }
        public int ProductId { get; set; }
        public string CarrierTrackingNumber { get; set; }
        public int? WarehouseId { get; set; }
        public decimal OrderQty { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal UnitPriceDiscount { get; set; }
        public decimal LineTotal { get; set; }
        public DateTime ModifiedDate { get; set; }

        
        public Invoice Invoice { get; set; }
        public Product Product { get; set; }
        public Warehouse Warehouse { get; set; }
    }
}
