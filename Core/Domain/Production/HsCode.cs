using Core.Domain.Base;
using System.ComponentModel.DataAnnotations;
namespace Core.Domain.Production
{
    public partial class HsCode : BaseEntity
    {
        public HsCode(string code, string name, decimal? nationalFederalTaxes, decimal? importFederalTaxes, decimal? stateTaxes, decimal? cityTaxes)
        {
            Code = code;
            Name = name;
            NationalFederalTaxes = nationalFederalTaxes;
            ImportFederalTaxes = importFederalTaxes;
            StateTaxes = stateTaxes;
            CityTaxes = cityTaxes;
        }
        #region property

        public int HsCodeId { get; set; }
        [Required]
        [StringLength(150)]
        public string Code { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        public decimal? NationalFederalTaxes { get; set; }
        public decimal? ImportFederalTaxes { get; set; }
        public decimal? StateTaxes { get; set; }
        public decimal? CityTaxes { get; set; }
        #endregion
    }
}
