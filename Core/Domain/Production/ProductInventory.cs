using Core.Domain.Base;
using System;
using System.ComponentModel.DataAnnotations;
namespace Core.Domain.Production
{
    public partial class ProductInventory : BaseEntity
    {
        public ProductInventory()
        {
            this.CreateDate = System.DateTime.Now;
            this.VarId = 1;
        }
        public ProductInventory(int productId, int varId, string shelf, int? bin, DateTime createDate, string description, string numberDoc, decimal quantity, int signal) : this()
        {
            ProductId = productId;
            VarId = varId;
            Shelf = shelf;
            Bin = bin;
            CreateDate = createDate;
            Description = description;
            NumberDoc = numberDoc;
            Quantity = quantity;
            Signal = signal;
        }

        #region property
        public int Id { get; set; }
        public int LocationId { get; set; }
        public int ProductId { get; set; }
        public int VarId { get; set; }
        [StringLength(50)]
        public string Shelf { get; set; }
        public int? Bin { get; set; }
        public DateTime CreateDate { get; set; }
        [StringLength(150)]
        public string Description { get; set; }
        [StringLength(150)]
        public string NumberDoc { get; set; }
        public decimal Quantity { get; set; }
        public int Signal { get; set; }
        #endregion
        public Location Location { get; set; }
        public Product Product { get; set; }
    }
}
