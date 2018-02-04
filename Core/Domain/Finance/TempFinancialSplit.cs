using Core.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.Finance
{
    public class TempFinancialSplit : BaseEntity
    {
     

        public int Id { get; set; }
        public int Seq { get; set; }
        public string Session { get; set; }
        public DateTime DueDate { get; set; }
        public decimal? Total { get; set; }

    }
}
