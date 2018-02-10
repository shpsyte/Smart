using Core.Domain.Production;
using Core.Domain.Sale;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smart.Models.InvoiceModel
{
     
    public class InvoiceProductModel
    {
        public int id { get; set; }
        public int Seq { get; set; }
        public int ProductId { get; set; }
        public string ProductNumber { get; set; }
        public string Name { get; set; }
        public decimal Qty { get; set; }
        public decimal? TaxProduction { get; set; }
        public decimal? TaxSales { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal? Discont { get; set; }
        public int? TaxOperationId { get; set; }
        public decimal? StandartCost { get; set; }
        public string CodOper { get; set; }
        public decimal Total { get; set; }
        public decimal? Stock { get; set; }
        public int? WarehouseId { get; set; }

        public Product Product { get; set; }
       
    }


}
