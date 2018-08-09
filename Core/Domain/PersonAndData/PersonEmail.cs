using Core.Domain.Base;
using Core.Domain.Business;

namespace Core.Domain.PersonAndData
{
    public partial class PersonEmail : BaseEntity
    {
        public int PersonEmailId { get; set; }
        public int EmailId { get; set; }
        public int PersonId { get; set; }

        public Email Email { get; set; }
        public Person Person { get; set; }

        
    }
}
