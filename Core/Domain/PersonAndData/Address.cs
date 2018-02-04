using Core.Domain.Base;
using Core.Domain.Business;
using Core.Domain.Region;
using Core.Domain.Sale;
using System;
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
        }

        public int AddressId { get; set; }
       
        public string PostalCode { get; set; }
       
        public string StreetAddress { get; set; }
        public string Number { get; set; }
        public string StreetAddressLine2 { get; set; }
        public string StreetAddressLine3 { get; set; }
        public string District { get; set; }
        public int? StateProvinceId { get; set; }
        public string StateProvinceName { get; set; }

        public int? CityId { get; set; }
        public string CityName { get; set; }
        public string CityCode { get; set; }

        public string SpatialLocation { get; set; }
        public bool? Deleted { get; set; }
        

        public City City { get; set; }
        public StateProvince StateProvince { get; set; }
        public ICollection<Invoice> InvoiceBillToAddress { get; set; }
        public ICollection<Invoice> InvoiceShipToAddress { get; set; }
        public ICollection<PersonAddress> PersonAddress { get; set; }
    }
}
