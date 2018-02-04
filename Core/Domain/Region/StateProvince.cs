using Core.Domain.PersonAndData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Domain.Region
{
    public partial class StateProvince
    {
        public StateProvince()
        {
            City = new HashSet<City>();
            Address = new HashSet<Address>();
            this.IsOnlyStateProvinceFlag = false;
        }
        public int StateProvinceId { get; set; }
        [Required]
        public string StateProvinceCode { get; set; }
        public bool IsOnlyStateProvinceFlag { get; set; }
        [Required]
        public string Name { get; set; }
        public int CountryRegionId { get; set; }
       

        public Country CountryRegion { get; set; }
        public ICollection<City> City { get; set; }
        public ICollection<Address> Address { get; set; }
    }
}
