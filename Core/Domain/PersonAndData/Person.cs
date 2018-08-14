using Core.Domain.Base;
using Core.Domain.Finance;
using Core.Domain.Finance.Views;
using Core.Domain.Sale;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Core.Domain.PersonAndData
{
    public partial class Person : BaseEntity
    {

        public Person()
        {
            Expense = new HashSet<Expense>();
            InvoiceCarrier = new HashSet<Invoice>();
            InvoiceCustomer = new HashSet<Invoice>();
            InvoiceSalesPerson = new HashSet<Invoice>();
            PersonAddress = new HashSet<PersonAddress>();
            PersonEmail = new HashSet<PersonEmail>();
            PersonPhone = new HashSet<PersonPhone>();
            Revenue = new HashSet<Revenue>();
            VRevenueTrans = new HashSet<VRevenueTrans>();
            VExpenseTrans = new HashSet<VExpenseTrans>();

            VRevenue = new HashSet<VRevenue>();
            VExpense = new HashSet<VExpense>();
            this.Active = true;
            this.CreateDate = ModelExtension.TimestampProvider();
            this.ModifiedDate = ModelExtension.TimestampProvider();
        }
        public Person(string firstName, string lastName, string registrationCode, string registrationState, int type, int? personType, int? categoryId, string email, string phone, byte[] image, string comments, DateTime createDate, DateTime modifiedDate) : this()
        {
            FirstName = firstName;
            LastName = lastName;
            RegistrationCode = registrationCode;
            RegistrationState = registrationState;
            Type = type;
            PersonType = personType;
            CategoryId = categoryId;
            Email = email;
            Phone = phone;
            Image = image;
            Comments = comments;
            CreateDate = createDate;
            ModifiedDate = modifiedDate;
         
        }

        #region property

        public int PersonId { get; set; }
        public int PersonCode { get; set; }
        [Required]
        [StringLength(120)]
        public string FirstName { get; set; }
        [StringLength(80)]
        public string LastName { get; set; }
        [StringLength(150)]
        public string RegistrationCode { get; set; }
        [StringLength(150)]
        public string RegistrationState { get; set; }
        public int Type { get; set; }
        public int? PersonType { get; set; }
        public int? CategoryId { get; set; }
        [StringLength(150)]
        public string Email { get; set; }
        [StringLength(50)]
        public string Phone { get; set; }
        public byte[] Image { get; set; }
        public string Comments { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }

        [NotMapped]
        public IFormFile avatarImage { get; set; }

        #endregion

        public CategoryPerson Category { get; set; }
        public ICollection<Expense> Expense { get; set; }
        public ICollection<Invoice> InvoiceCarrier { get; set; }
        public ICollection<Invoice> InvoiceCustomer { get; set; }
        public ICollection<Invoice> InvoiceSalesPerson { get; set; }
        public ICollection<PersonAddress> PersonAddress { get; set; }
        public ICollection<PersonEmail> PersonEmail { get; set; }
        public ICollection<PersonPhone> PersonPhone { get; set; }
        public ICollection<Revenue> Revenue { get; set; }
        public ICollection<VRevenue> VRevenue { get; set; }
        public ICollection<VExpense> VExpense { get; set; }
        public ICollection<VCashFlow> VCashFlow { get; set; }
        public ICollection<VRevenueTrans> VRevenueTrans { get; set; }
        public ICollection<VExpenseTrans> VExpenseTrans { get; set; }

    }
}
