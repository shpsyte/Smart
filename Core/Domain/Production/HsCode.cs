
using Core.Domain.Base;
using Core.Domain.Business;

namespace Core.Domain.Production
{
    public partial class HsCode : BaseEntity
    {
        public int HsCodeId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal? NationalFederalTaxes { get; set; }
        public decimal? ImportFederalTaxes { get; set; }
        public decimal? StateTaxes { get; set; }
        public decimal? CityTaxes { get; set; }

        
    }
}
