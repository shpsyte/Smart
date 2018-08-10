using Core.Domain.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Domain.Finance
{
    public partial class AccountBank : BaseEntity
    {
        public AccountBank()
        {
            this.BankTrans = new HashSet<BankTrans>();
            this.RevenueTrans = new HashSet<RevenueTrans>();
            this.ExpenseTrans = new HashSet<ExpenseTrans>();
            this.Active = true;
        }
        public AccountBank(string name, string code, string agency, string digitAgency, string account, string digitAccount) : this()
        {
            Name = name;
            Code = code;
            Agency = agency;
            DigitAgency = digitAgency;
            Account = account;
            DigitAccount = digitAccount;
        }
        #region property
        [Key]
        public int AccountBankId { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(10)]
        public string Code { get; set; }
        [StringLength(50)]
        public string Agency { get; set; }
        [StringLength(5)]
        public string DigitAgency { get; set; }
        [StringLength(50)]
        public string Account { get; set; }
        [StringLength(5)]
        public string DigitAccount { get; set; }
        public bool Active { get; set; }
        #endregion

        public ICollection<BankTrans> BankTrans { get; set; }
        public ICollection<RevenueTrans> RevenueTrans { get; set; }
        public ICollection<ExpenseTrans> ExpenseTrans { get; set; }
    }
}
