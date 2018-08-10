using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Fake
{
    public abstract class PayAccount
    {
        public int RevenueId { get; set; }
        public int ExpenseId { get; set; }
        public DateTime DueDate { get; set; }
        public int? BankId { get; set; }
        public int? ConditionId { get; set; }
        public decimal? Payment { get; set; }
        public decimal? Tax { get; set; }
        public decimal? Discont { get; set; }
        public string Comment { get; set; }
        public bool Active { get; set; }
        public List<IFormFile> Image { get; set; }
        public int Signal { get; set; }

    }
}
