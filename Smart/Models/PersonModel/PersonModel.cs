using Core.Domain.PersonAndData;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Smart.Models.PersonModel
{
    public class PersonModel 
    {

        public PersonModel()
        {
            this.Active = true;
            this.CreateDate = System.DateTime.UtcNow;
            this.ModifiedDate = System.DateTime.UtcNow;
        }
        public int PersonId { get; set; }
        public int BusinessEntityId { get; set; }
        public int PersonCode { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string RegistrationCode { get; set; }
        public string RegistrationState { get; set; }
        public int Type { get; set; }
        public int PersonType { get; set; }
        public int? CategoryId { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public byte[] Image { get; set; }
        public string Comments { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
        public IFormFile avatarImage { get; set; }



        
            public int AddressId { get; set; }
        public string PostalCode { get; set; }
        public string StreetAddress { get; set; }
        public string Number { get; set; }
        public string StreetAddressLine2 { get; set; }
        public string StreetAddressLine3 { get; set; }
        public string District { get; set; }
        public int? CityId { get; set; }
        public string CityName { get; set; }
        public int? StateProvinceId { get; set; }
        public string StateProvinceName { get; set; }
        public string CityCode { get; set; }
        public string SpatialLocation { get; set; }

        


    }
}
