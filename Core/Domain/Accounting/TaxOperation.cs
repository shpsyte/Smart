
using Core.Domain.Base;
using Core.Domain.Business;
using Core.Domain.Sale;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Domain.Accounting
{

    public partial class TaxOperation : BaseEntity
    {
        public TaxOperation()
        {
            this.CostTrigger = false;
            this.StockTrigger = true;
            this.Invoice = new HashSet<Invoice>();
        }

        [Key]
        public int TaxOperationId { get; set; }
        [Required]
        public string Name { get; set; }
        public int TaxFunction { get; set; }
        public int TaxWay { get; set; }
        [Required]
        public string DefaultCode { get; set; }
        public bool StockTrigger { get; set; }
        public bool CostTrigger { get; set; }

        public ICollection<Invoice> Invoice { get; set; }
    }
}
