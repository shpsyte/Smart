using Core.Domain.Base;
using Core.Domain.Region;
using Core.Domain.Sale;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Domain.PersonAndData
{
    public partial class Address : BaseEntity
    {
        public Address()
        {
            InvoiceBillToAddress = new HashSet<Invoice>();
            InvoiceShipToAddress = new HashSet<Invoice>();
            PersonAddress = new HashSet<PersonAddress>();
            this.Deleted = false;

        }
        public Address(string postalCode, string streetAddress, string number, string streetAddressLine2, string streetAddressLine3, string district, string stateProvinceName, int? stateProvinceId, int? cityId, string cityName, string cityCode, string spatialLocation) : this()
        {
            this.PostalCode = postalCode;
            this.StreetAddress = streetAddress;
            this.Number = number;
            this.StreetAddressLine2 = streetAddressLine2;
            this.StreetAddressLine3 = streetAddressLine3;
            this.District = district;
            this.StateProvinceName = stateProvinceName;
            this.StateProvinceId = stateProvinceId;
            this.CityId = cityId;
            this.CityName = cityName;
            this.CityCode = cityCode;
            this.SpatialLocation = spatialLocation;
        }

        #region property
        public int AddressId { get; set; }
       
        [StringLength(50)]
        public string PostalCode { get; set; }
        [StringLength(150)]
        public string StreetAddress { get; set; }
        [StringLength(150)]
        public string StreetAddressLine2 { get; set; }
        [StringLength(150)]
        public string StreetAddressLine3 { get; set; }
        [StringLength(20)]
        public string Number { get; set; }
        [StringLength(50)]
        public string District { get; set; }
        [StringLength(150)]
        public string StateProvinceName { get; set; }

        public int? StateProvinceId { get; set; }
        public int? CityId { get; set; }
        [StringLength(150)]
        public string CityName { get; set; }
        [StringLength(150)]
        public string CityCode { get; set; }

        [StringLength(150)]
        public string SpatialLocation { get; set; }
        public bool Deleted { get; set; }
        #endregion

        public City City { get; set; }
        public StateProvince StateProvince { get; set; }
        public ICollection<Invoice> InvoiceBillToAddress { get; set; }
        public ICollection<Invoice> InvoiceShipToAddress { get; set; }
        public ICollection<PersonAddress> PersonAddress { get; set; }
    }
}
