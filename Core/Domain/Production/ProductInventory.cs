using Core.Domain.Base;
using System;
using System.Collections.Generic;

namespace Core.Domain.Production
{
    public partial class ProductInventory : BaseEntity
    {
        public ProductInventory()
        {
            this.CreateDate = System.DateTime.Now;
            this.VarId = 1;


        }
        public int Id { get; set; }
        public int LocationId { get; set; }
        public int ProductId { get; set; }
        public int VarId { get; set; }
        public string Shelf { get; set; }
        public int? Bin { get; set; }
        public DateTime CreateDate { get; set; }
        public string Description { get; set; }
        public string NumberDoc { get; set; }
        public decimal Quantity { get; set; }
        public int Signal { get; set; }

        public Location Location { get; set; }
        public Product Product { get; set; }
    }
}
