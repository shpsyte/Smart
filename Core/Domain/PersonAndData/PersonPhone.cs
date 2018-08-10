
using Core.Domain.Base;
 

namespace Core.Domain.PersonAndData
{
    public partial class PersonPhone : BaseEntity
    {
        public PersonPhone(int phoneId, int personId)
        {
            PhoneId = phoneId;
            PersonId = personId;
        }
        #region property
        public int PersonPhoneId { get; set; }
        public int PhoneId { get; set; }
        public int PersonId { get; set; }
        #endregion

        public Person Person { get; set; }
        public Phone Phone { get; set; }
        
    }
}
