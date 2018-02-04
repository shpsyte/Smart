
using Core.Domain.Base;
using Core.Domain.Business;

namespace Core.Domain.PersonAndData
{
    public partial class PersonPhone : BaseEntity
    {
        public int Id { get; set; }
        public int PhoneId { get; set; }
        public int PersonId { get; set; }

        public Person Person { get; set; }
        public Phone Phone { get; set; }
        
    }
}
