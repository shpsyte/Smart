using Core.Domain.Base;
using Core.Domain.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Domain.PersonAndData
{
    public partial class CategoryPerson : BaseEntity
    {
        public CategoryPerson()
        {
            Person = new HashSet<Person>();
            this.CreateDate = DateTime.UtcNow;
        }

         
        public int CategoryId { get; set; }

        [Required]
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }

        public ICollection<Person> Person { get; set; }
    }
}
