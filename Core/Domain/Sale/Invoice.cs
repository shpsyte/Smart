using Core.Domain.Accounting;
using Core.Domain.Base;
using Core.Domain.Business;
using Core.Domain.PersonAndData;
using Core.Domain.Production;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Domain.Sale
{
    public partial class Invoice : BaseEntity
    {
        public Invoice()
        {
            InvoiceDetail = new HashSet<InvoiceDetail>();
            this.OnlineInvoiceFlag = false;
            this.InvoiceDate =  System.DateTime.Now;
        }

        public int InvoiceId { get; set; }
        public int TaxOperationId { get; set; }
        public byte RevisionNumber { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime InvoiceDate { get; set; }
        public int? InvoiceCodeOper { get; set; }
        public int? Finality { get; set; }
        public int? InvoiceTp { get; set; }
        public int Number { get; set; }
        public int Sequence { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DueDate { get; set; }


        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ShipDate { get; set; }
        public int Status { get; set; }
        public bool OnlineInvoiceFlag { get; set; }
        public string InvoiceNumber { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public string AccountNumber { get; set; }
        public int CustomerId { get; set; }
        public int? SalesPersonId { get; set; }
        public int? CarrierId { get; set; }
        public int? WarehouseId { get; set; }
        public int? BillToAddressId { get; set; }
        public int? ShipToAddressId { get; set; }
        public string CarrierTrackingNumber { get; set; }
        public int? FreightType { get; set; }
        public int? Volumes { get; set; }
        public string Package { get; set; }
        public string CarrierTruckId { get; set; }
        public decimal? Weight { get; set; }
        public decimal? TotalWeight { get; set; }
        public decimal SubTotal { get; set; }
        public decimal? TaxAmt { get; set; }
        public decimal? Discount { get; set; }
        public decimal? Freight { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal TotalDue { get; set; }
        public string Comment { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ModifiedDate { get; set; }


        [NotMapped]
        public string CustomerName {
            get {
                return this.Customer == null ? "" : this.Customer.FirstName;
            }

            set { }
            

        }

        public Address BillToAddress { get; set; }
        
        public Person Carrier { get; set; }
        public Person Customer { get; set; }
        public Person SalesPerson { get; set; }
        public Address ShipToAddress { get; set; }
        public Location Location { get; set; }
        public TaxOperation TaxOperation { get; set; }
        public ICollection<InvoiceDetail> InvoiceDetail { get; set; }
    }
}
