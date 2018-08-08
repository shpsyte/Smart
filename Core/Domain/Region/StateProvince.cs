using Core.Domain.Base;
using Core.Domain.PersonAndData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Domain.Region
{
    public partial class StateProvince : BaseEntity
    {
        public StateProvince()
        {
            City = new HashSet<City>();
            Address = new HashSet<Address>();
            this.IsOnlyStateProvinceFlag = false;
        }
        public int StateProvinceId { get; private set; }
        [Required]
        public string StateProvinceCode { get; set; }
        public bool IsOnlyStateProvinceFlag { get; set; }
        [Required]
        public string Name { get; set; }
        public int CountryID { get; set; }
       

        public Country Country { get; set; }
        public ICollection<City> City { get; set; }
        public ICollection<Address> Address { get; set; }
    }
}
