using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.ValueObjects
{
    public partial class EmailAddress 
    {
        public EmailAddress()
        {

        }
        public EmailAddress(string email) : this()
        {
            this.Email = email;
        }

        [DataType(DataType.EmailAddress)]
        public string Email { get; private set; }



       
    }
}
