using Core.Domain.Base;
using Core.Domain.Business;
using System;
using System.Collections.Generic;

namespace Core.Domain.Production
{
    public partial class Image : BaseEntity
    {
        public Image()
        {
            ProductImage = new HashSet<ProductImage>();
        }

        public int ImageId { get; set; }
        public byte[] LargeImage { get; set; }
        public string LargeImageFileName { get; set; }
        public string Comments { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool? Active { get; set; }
        public bool Deleted { get; set; }

        
        public ICollection<ProductImage> ProductImage { get; set; }
    }
}
