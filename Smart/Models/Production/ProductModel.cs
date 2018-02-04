using Core.Domain.Production;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smart.Models.Production
{
    public class ProductModel 
    {
        public IEnumerable<Image> Image { get; set; }
    }
}
