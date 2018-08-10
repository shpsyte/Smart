using Core.Domain.Base;
using System;
using System.ComponentModel.DataAnnotations;
namespace Core.Domain.Finance
{
    public class TempFinancialSplit : BaseEntity
    {
       
        public TempFinancialSplit(int seq, string session, DateTime dueDate, decimal? total)
        {
            Seq = seq;
            Session = session;
            DueDate = dueDate;
            Total = total;
        }
         
        #region property
        [Key]
        public int Id { get; set; }
        public int Seq { get; set; }
        [StringLength(100)]
        public string Session { get; set; }
        public DateTime DueDate { get; set; }
        public decimal? Total { get; set; }
        #endregion
    }
}
