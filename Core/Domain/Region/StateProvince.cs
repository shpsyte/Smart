using Core.Domain.Base;
using Core.Domain.PersonAndData;
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
        public StateProvince(string stateProvinceCode, bool isOnlyStateProvinceFlag, string name, int countryID) : this()
        {
            StateProvinceCode = stateProvinceCode;
            IsOnlyStateProvinceFlag = isOnlyStateProvinceFlag;
            Name = name;
            CountryID = countryID;
        }
        #region property

        public int StateProvinceId { get; set; }
        [Required]
        [StringLength(50)]
        public string StateProvinceCode { get; set; }
        public bool IsOnlyStateProvinceFlag { get; set; }
        [Required]
        [StringLength(80)]
        public string Name { get; set; }
        public int CountryID { get; set; }
        #endregion

        public Country Country { get; set; }
        public ICollection<City> City { get; set; }
        public ICollection<Address> Address { get; set; }
    }
}
