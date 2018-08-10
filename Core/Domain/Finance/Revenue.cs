using Core.Domain.Base;
using Core.Domain.Finance.Views;
using Core.Domain.PersonAndData;
using Core.Domain.Sale;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Core.Domain.Finance
{
    public partial class Revenue : BaseEntity
    {
        
        public Revenue()
        {
            this.RevenueSeq = 1;
            this.RevenueTotalSeq = 1;
            this.DueDate = System.DateTime.Now.AddDays(7);
            this.RevenueTrans = new HashSet<RevenueTrans>();
            this.CreateDate = DateTime.Now;
        }
        public Revenue(string revenueNumber, int? revenueSeq, int? revenueTotalSeq, string name, int? personId, int? categoryId, int? costCenterId, int? conditionId, decimal total, DateTime? createDate, DateTime? dueDate, DateTime? duePayment, string comment) : this()
        {
            RevenueNumber = revenueNumber;
            RevenueSeq = revenueSeq;
            RevenueTotalSeq = revenueTotalSeq;
            Name = name;
            PersonId = personId;
            CategoryId = categoryId;
            CostCenterId = costCenterId;
            ConditionId = conditionId;
            Total = total;
            CreateDate = createDate;
            DueDate = dueDate;
            DuePayment = duePayment;
            Comment = comment;
        }
        
        #region property
        public int RevenueId { get; set; }
        [Required]
        [StringLength(80)]
        public string Name { get; set; }
        [StringLength(80)]
        public string RevenueNumber { get; set; }
        public int? RevenueSeq { get; set; }
        public int? RevenueTotalSeq { get; set; }
        public int? PersonId { get; set; }
        public int? CategoryId { get; set; }
        public int? CostCenterId { get; set; }
        public int? ConditionId { get; set; }
        public decimal Total { get; set; }


        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? CreateDate { get; set; }


        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]

        public DateTime? DueDate { get; set; }


        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DuePayment { get; set; }
        public string Comment { get; set; }
        public DateTime ModifiedDate { get; set; }

        public bool Deleted { get; set; }

        #endregion

        public CategoryFinancial CategoryFinancial { get; set; }
        public CostCenter CostCenter { get; set; }
        public Condition PaymentCondition { get; set; }
        public Person Person { get; set; }
        public VRevenue VRevenue { get; set; }
        public ICollection<RevenueTrans> RevenueTrans { get; set; }
        
    }
}
