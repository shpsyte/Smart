using Core.Domain.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Core.Domain.PersonAndData
{
    public partial class Phone : BaseEntity
    {
        public Phone()
        {
            PersonPhone = new HashSet<PersonPhone>();
        }

        public Phone(string phone1, int type) : this()
        {
            Phone1 = phone1;
            Type = type;
        }

        #region property
        public int PhoneId { get; set; }
        [Required]
        [StringLength(150)]
        public string Phone1 { get; set; }
        public int Type { get; set; }
        #endregion


        public ICollection<PersonPhone> PersonPhone { get; set; }
    }
}
