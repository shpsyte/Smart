using Core.Domain.Base;
using Core.Domain.Business;
using System;

namespace Core.Domain.Production
{
    public partial class ProductImage : BaseEntity
    {
        public int Id { get; set; }
        public int ImageId { get; set; }
        public int ProductId { get; set; }
        public bool? IsPrimary { get; set; }
        public DateTime? CreateDate { get; set; }

        public Image Image { get; set; }
        public Product Product { get; set; }
        
    }
}
