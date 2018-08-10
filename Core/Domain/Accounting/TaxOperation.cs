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
        public TaxOperation(string name, string defaultCode, bool stockTrigger = true, bool costTrigger = false) : this()
        {
            this.Name = name;
            this.DefaultCode = defaultCode;
            this.StockTrigger = stockTrigger;
            this.CostTrigger = costTrigger;
        }

        #region property
        [Key]
        public int TaxOperationId { get; private set; }
        [Required]
        [StringLength(50)]

        public string Name { get; set; }

        public int TaxFunction { get; set; }
        public int TaxWay { get; set; }

        [Required]
        [StringLength(4)]
        public string DefaultCode { get; set; }

        public bool StockTrigger { get; set; }
        public bool CostTrigger { get; set; }
        #endregion

        public ICollection<Invoice> Invoice { get; set; }
    }
}
