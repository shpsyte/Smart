using Core.Domain.Base;
using Core.Domain.PersonAndData;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Core.Domain.Region
{
    public partial class City : BaseEntity
    {
        public City()
        {
            Address = new HashSet<Address>();
        }

        public City(string name, string middleName, string specialCodeRegion, int? stateProvinceId) : this()
        {
            Name = name;
            MiddleName = middleName;
            SpecialCodeRegion = specialCodeRegion;
            StateProvinceId = stateProvinceId;
        }

        public int CityId { get;  set; }
        [Required]
        [StringLength(80)]
        public string Name { get; set; }
        [StringLength(50)]
        public string MiddleName { get; set; }
        [StringLength(80)]
        public string SpecialCodeRegion { get; set; }
        public int? StateProvinceId { get; set; }

        public StateProvince StateProvince { get; set; }
        public ICollection<Address> Address { get; set; }
    }
}
