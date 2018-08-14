using Core.Domain.Base;
using System;
using System.ComponentModel.DataAnnotations;
namespace Core.Domain.Production
{
    public partial class ProductImage : BaseEntity
    {
        public ProductImage()
        {
            CreateDate = ModelExtension.TimestampProvider();

        }
        public ProductImage(int imageId, int productId, bool? isPrimary) : this()
        {
            ImageId = imageId;
            ProductId = productId;
            IsPrimary = isPrimary;
        }

        #region property
        [Key]
        public int Id { get; set; }
        public int ImageId { get; set; }
        public int ProductId { get; set; }
        public bool? IsPrimary { get; set; }
        public DateTime? CreateDate { get; set; }
        #endregion

        public Image Image { get; set; }
        public Product Product { get; set; }
        
    }
}
