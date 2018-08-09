using Core.Domain.Base;
using Core.Domain.Business;
using System.ComponentModel.DataAnnotations;

namespace Core.Domain.Accounting
{
    public partial class Tax : BaseEntity
    {
        [Key]
        public int TaxId { get; set; }
        public int TaxOperationId { get; set; }
        public string TaxGroupId { get; set; }
        
    }
}
