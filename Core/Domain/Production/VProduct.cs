using Core.Domain.Accounting;
using Core.Domain.Base;
using Core.Domain.Business;
using Core.Domain.Sale;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Domain.Production
{
    public abstract partial class VProduct : BaseEntity, IView
    {

        public int ProductId { get; private set; }
        public string Name { get; set; }
        public string ProductNumber { get; set; }
        public string Manufacturer { get; set; }
        public string Ean { get; set; }
        public string HsCode { get; set; }
        public string Location { get; set; }
        public decimal? ListPrice { get; set; }
        public string ProductAttribute { get; set; }
        public decimal? Stock { get; set; }
        public Product Product { get; set; }

    }
}
