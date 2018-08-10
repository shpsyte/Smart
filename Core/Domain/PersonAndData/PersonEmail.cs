using Core.Domain.Base;

namespace Core.Domain.PersonAndData
{
    public partial class PersonEmail : BaseEntity
    {
        public PersonEmail(int emailId, int personId)
        {
            EmailId = emailId;
            PersonId = personId;
        }
        #region property
        public int PersonEmailId { get; set; }
        public int EmailId { get; set; }
        public int PersonId { get; set; }

        #endregion
        public Email Email { get; set; }
        public Person Person { get; set; }
    }
}
