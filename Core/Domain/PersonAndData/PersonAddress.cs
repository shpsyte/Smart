using Core.Domain.Base;

namespace Core.Domain.PersonAndData
{
    public partial class PersonAddress : BaseEntity
    {
        public PersonAddress()
        {

        }
        public PersonAddress( int addressId, int personId) : this()
        {
            AddressId = addressId;
            PersonId = personId;
        }


        #region property

        public int Id { get; set; }
        public int AddressId { get; set; }
        public int PersonId { get; set; }

        #endregion

        public Address Address { get; set; }
        public Person Person { get; set; }

        
    }
}
