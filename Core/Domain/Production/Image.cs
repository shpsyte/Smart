using Core.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Domain.Production
{
    public partial class Image : BaseEntity
    {
        public Image()
        {
            ProductImage = new HashSet<ProductImage>();
            this.Active = true;
            this.Deleted = false;
        }
        public Image(int imageId, byte[] largeImage, string largeImageFileName, string comments, DateTime createDate, DateTime modifiedDate) : this()
        {
            this.ImageId = imageId;
            this.LargeImage = largeImage;
            this.LargeImageFileName = largeImageFileName;
            this.Comments = comments;
            this.CreateDate = createDate;
            this.ModifiedDate = modifiedDate;
        }

        #region property
        public int ImageId { get; set; }
        public byte[] LargeImage { get; set; }
        [StringLength(50)]
        public string LargeImageFileName { get; set; }
        public string Comments { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool? Active { get; set; }
        public bool Deleted { get; set; }
        #endregion



        public ICollection<ProductImage> ProductImage { get; set; }
    }
}
