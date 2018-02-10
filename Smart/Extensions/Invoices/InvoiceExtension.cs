using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Domain.Sale;
using Core.Domain.Production;
using Smart.Models.InvoiceModel;

namespace Smart.Extensions.Invoices
{

    public class InvoiceExtension
    {
        private readonly IServices<Invoice> _invoiceServices;
        private readonly IServices<InvoiceDetail> _invoinceDetailsServices;
        private readonly IServices<Product> _productServices;
        private readonly IUser _currentUser;

        public InvoiceExtension(IServices<Invoice> invoiceServices, IServices<InvoiceDetail> invoinceDetailsServices, IServices<Product> productServices, IUser currentUser)
        {
            this._invoiceServices = invoiceServices;
            this._invoinceDetailsServices = invoinceDetailsServices;
            this._productServices = productServices;
            this._currentUser = currentUser;

        }


        public IEnumerable<InvoiceDetail> CreateInvoiceDetail(IEnumerable<InvoiceProductModel> product, Invoice invoice)
        {
            List<InvoiceDetail> products = new List<InvoiceDetail>();

            foreach (var item in product)
            {
                var invoiceDetail = new InvoiceDetail()
                {
                    BusinessEntityId = _currentUser.BusinessEntityId(),
                    CarrierTrackingNumber = "",
                    Invoice = invoice == null ? null : invoice,
                    ProductId = item.ProductId,
                    WarehouseId = item.WarehouseId,
                    OrderQty = item.Qty,
                    UnitPrice = item.UnitPrice,
                    UnitPriceDiscount = item.Discont.HasValue ? item.Discont.Value : 0,
                    CodOper = item.CodOper,
                    ModifiedDate = System.DateTime.Now,
                    ProductNumber = item.ProductNumber,
                    LineTotal = Convert.ToDecimal( item.UnitPrice * ( 1 - item.Discont) * item.Qty ),
                    StandartCost = item.StandartCost,
                    TaxOperationId = item.TaxOperationId,
                    TaxProduction = item.TaxProduction,
                    TaxSales = item.TaxSales
                    
                };
                products.Add(invoiceDetail);
            }
            return products;
        }


        public InvoiceDetail CreateInvoiceDetail(InvoiceProductModel product, Invoice invoice)
        {
            List<InvoiceProductModel> data = new List<InvoiceProductModel>();
            data.Add(product);
             
            return CreateInvoiceDetail(data, invoice).First();
        }
    }
}
