using Core.Domain.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Core.Domain.Region
{
    public partial class Country : BaseEntity
    {
        public Country()
        {
            StateProvince = new HashSet<StateProvince>();
        }

        public Country(string name, string middleName, string countryRegionCode, string specialCodeRegion) : this()
        {
            Name = name;
            MiddleName = middleName;
            CountryRegionCode = countryRegionCode;
            SpecialCodeRegion = specialCodeRegion;
        }
        #region property
        public int CountryId { get; set; }
        [Required]
        [StringLength(80)]
        public string Name { get; set; }
        [StringLength(80)]
        public string MiddleName { get; set; }
        [StringLength(6)]
        public string CountryRegionCode { get; set; }
        [StringLength(10)]
        public string SpecialCodeRegion { get; set; }
        #endregion


        public ICollection<StateProvince> StateProvince { get; set; }
    }
}
