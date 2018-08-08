using Core.Domain.Base;
using System;
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

        public int CountryId { get; private set; }
        [Required]
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string CountryRegionCode { get; set; }
        public string SpecialCodeRegion { get; set; }
      

        public ICollection<StateProvince> StateProvince { get; set; }
    }
}
