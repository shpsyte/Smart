using Core.Domain.Base;
using Core.Domain.Business;
using System;
using System.Collections.Generic;

namespace Core.Domain.PersonAndData
{
    public partial class PersonAddress : BaseEntity
    {
        public int Id { get; set; }
        public int AddressId { get; set; }
        public int PersonId { get; set; }

        public Address Address { get; set; }
        public Person Person { get; set; }

        
    }
}
