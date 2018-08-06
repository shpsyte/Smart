using Core.Domain.Base;
using Core.Domain.PersonAndData;
using System;
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
        public int CityId { get; set; }
        [Required]
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string SpecialCodeRegion { get; set; }
        public int? StateProvinceId { get; set; }

        public StateProvince StateProvince { get; set; }
        public ICollection<Address> Address { get; set; }
    }
}
