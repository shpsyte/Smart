using Core.Domain.Base;
using Core.Domain.Business;
using Core.Domain.Production;
using System;
using System.Collections.Generic;

namespace Core.Domain.Sale
{
    public partial class InvoiceDetail : BaseEntity
    {
        public InvoiceDetail()
        {
            this.TaxProduction = 0;
            this.TaxSales = 0;
            this.UnitPriceDiscount = 0;
            this.UnitPrice = 0;
            this.OrderQty = 1;
        }
        public int InvoiceId { get; set; }
        public int InvoiceDetailId { get; set; }
        public int ProductId { get; set; }
        public string ProductNumber { get; set; }
        public string CodOper { get; set; }
        public int? TaxOperationId { get; set; }
        public decimal? TaxProduction { get; set; }
        public decimal? TaxSales { get; set; }
        public decimal? StandartCost { get; set; }


        public string CarrierTrackingNumber { get; set; }
        public int? WarehouseId { get; set; }
        public decimal OrderQty { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal? UnitPriceDiscount { get; set; }
        public decimal LineTotal { get; set; }
        public DateTime ModifiedDate { get; set; }

        



        public Invoice Invoice { get; set; }
        public Product Product { get; set; }
        public Location Warehouse { get; set; }
    }
}
