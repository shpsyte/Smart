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
            this.CreateDate = ModelExtension.TimestampProvider();
        }

        public CategoryPerson(string name): this()
        {
            this.Name = name;
        }

        #region property

        public int CategoryId { get; set; }

        [Required]
        [StringLength(80)]
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        #endregion

        public ICollection<Person> Person { get; set; }
    }
}
