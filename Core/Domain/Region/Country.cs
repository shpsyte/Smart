using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Domain.Region
{
    public partial class Country
    {
        public Country()
        {
            StateProvince = new HashSet<StateProvince>();
        }

        public int CountryId { get; set; }
        [Required]
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string CountryRegionCode { get; set; }
        public string SpecialCodeRegion { get; set; }
      

        public ICollection<StateProvince> StateProvince { get; set; }
    }
}
