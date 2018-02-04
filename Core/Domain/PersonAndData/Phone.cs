
using Core.Domain.Base;
using Core.Domain.Business;
using System.Collections.Generic;

namespace Core.Domain.PersonAndData
{
    public partial class Phone : BaseEntity
    {
        public Phone()
        {
            PersonPhone = new HashSet<PersonPhone>();
        }

        public int PhoneId { get; set; }
        public string Phone1 { get; set; }
        public string Type { get; set; }

        
        public ICollection<PersonPhone> PersonPhone { get; set; }
    }
}
