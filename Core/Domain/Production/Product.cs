using Core.Domain.Accounting;
using Core.Domain.Base;
using Core.Domain.Business;
using Core.Domain.Sale;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Domain.Production
{
    public partial class Product : BaseEntity
    {
        public Product()
        {
            InvoiceDetail = new HashSet<InvoiceDetail>();
            ProductImage = new HashSet<ProductImage>();
            ProductInventory = new HashSet<ProductInventory>();
            this.FinishedGoodsFlag = true;
            this.SellStartDate = System.DateTime.Now.Date;
            this.CreateDate = System.DateTime.Now.Date;
            this.ModifiedDate = System.DateTime.Now.Date;
            this.Active = true;
            this.Deleted = false;
        }
        public Product(string name, string productnumber) : base ()
        {
            this.Name = name;
            this.ProductNumber = productnumber;

        }

        public int ProductId { get; private set; }
        [Required]
        [StringLength(120)]
        public string Name { get; set; }
        [Required]
        [StringLength(30)]
        public string ProductNumber { get; set; }
        public string Manufacturer { get; set; }
        public string Ean { get; set; }
        public string HsCode { get; set; }
        public string HsCodeTax { get; set; }
        public string Location { get; set; }
        public bool MakeFlag { get; set; }
        public bool VariableFlag { get; set; }

        public bool FinishedGoodsFlag { get; set; }
        public decimal? SafetyStockLevel { get; set; }
        public decimal? MaximumStocklevel { get; set; }
        public decimal? ReorderPoint { get; set; }
        public decimal? StandardCost { get; set; }
        public decimal? ListPrice { get; set; }
        public string SizeUnitMeasureCode { get; set; }
        public decimal? Weight { get; set; }
        public decimal? WeightTotal { get; set; }
        public decimal? Height { get; set; }
        public decimal? Width { get; set; }
        public decimal? Length { get; set; }
        public int? DaysToManufacture { get; set; }
        public string ProductAttribute { get; set; }
        public int? ProductSourceId { get; set; }
        public int? ClassId { get; set; }
        public int? CategoryId { get; set; }
        public int? TaxGroupId { get; set; }
        public decimal? TaxIva { get; set; }
        public decimal? TaxImport { get; set; }
        public decimal? TaxProduction { get; set; }
        public decimal? TaxSale { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime SellStartDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? SellEndDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ModifiedDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CreateDate { get; set; }
        
        public bool Active { get; set; }
        public bool Deleted { get; set; }


        public CategoryProduct Category { get; set; }
        public VProduct VProduct { get; set; }
        public ClassProduct Class { get; set; }
        public TaxGroup TaxGroup { get; set; }
        public ICollection<InvoiceDetail> InvoiceDetail { get; set; }
        public ICollection<ProductImage> ProductImage { get; set; }
        public ICollection<ProductInventory> ProductInventory { get; set; }
    }
}
