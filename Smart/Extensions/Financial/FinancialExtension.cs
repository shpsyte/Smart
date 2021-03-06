﻿using Core.Domain.Finance;
using Core.Domain.Finance.Views;
using Core.Fake;
using Services.Interfaces;
using Smart.Models.Financial;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smart.Extensions.Financial
{
    public class FinancialExtension
    {
        private readonly IServices<TempFinancialSplit> _tempFinancialSplit;
        private readonly IServices<VExpense> _vExpenseServices;
        private readonly IServices<VRevenue> _vRevenueServices;
        private readonly IServices<BankTrans> _bankTransServices;
        private readonly IServices<VCashFlow> _cVCashFlowServices;
        public FinancialExtension(IServices<VCashFlow> cVCashFlowServices, IServices<BankTrans> bankTransServices, IServices<VRevenue> vRevenueServices, IServices<VExpense> vExpenseServices, IServices<TempFinancialSplit> tempFinancialSplit)
        {
            this._tempFinancialSplit = tempFinancialSplit;
            this._vExpenseServices = vExpenseServices;
            this._vRevenueServices = vRevenueServices;
            this._bankTransServices = bankTransServices;
            this._cVCashFlowServices = cVCashFlowServices;

        }
        public IEnumerable<TempFinancialSplit> GenerateSplitPay(int TotalSeq, decimal Value, DateTime? DataInicial, IUser user)
        {
            decimal parcela = decimal.Round(Value / TotalSeq, 2);
            decimal diferenca = Math.Abs((parcela * TotalSeq) - Value);
            var tempFinancialSplit = new List<TempFinancialSplit>();

            

            for (int i = 1; i <= TotalSeq; i++)
            {
                var atempFinancialSplit = new TempFinancialSplit()
                {
                    BusinessEntityId = user.BusinessEntityId() ,
                    Session = user.Id(),
                    Seq = i,
                    Total = parcela,
                    DueDate = DataInicial.HasValue ? DataInicial.Value.AddMonths(i - 1) : DateTime.Now.AddMonths(i - 1)
                };

                if (i == TotalSeq)
                {
                    if (Math.Abs(diferenca) != 0)
                    {
                        atempFinancialSplit.Total = decimal.Round(atempFinancialSplit.Total.Value + diferenca, 2);
                    }
                }
                // _tempFinancialSplit.Add(tempFinancialSplit);
                tempFinancialSplit.Add(atempFinancialSplit);
            }
            return tempFinancialSplit;// _tempFinancialSplit.Query(a => a.Session == user.Id());
        }

        public ExpenseTrans GetExpenseTrans(Expense expense, int businessId)
        {
            return new ExpenseTrans()
            {
                BusinessEntityId = businessId,
                ConditionId = expense.ConditionId,
                Description = expense.Name,
                CreateDate = expense.CreateDate.HasValue ? expense.CreateDate.Value : System.DateTime.UtcNow,
                Midledesc = "INC",
                Signal = 1,
                Total = expense.Total,
                Expense = expense
            };

        }
        public ExpenseTrans GetExpenseTrans(PayAccount data, int businessId, string midleDesc, int signal)
        {
            return new ExpenseTrans()
            {
                AccountBankId = data.AccountBankId,
                BusinessEntityId = businessId,
                CreateDate = data.DueDate,
                Description = data.Comment,
                Midledesc = midleDesc,
                ConditionId = data.ConditionId,
                ExpenseId = data.ExpenseId,
                Signal = signal,
                Total = data.Payment.Value
            };
        }


        public Expense GetExpense(Expense expense, int businessId, TempFinancialSplit item)
        {
            return new Expense()
            {

                BusinessEntityId = businessId,
                Name = expense.Name,
                Total = (decimal)item.Total,
                DueDate = item.DueDate,
                ExpenseSeq = item.Seq,
                ChartAccountId = expense.ChartAccountId,
                Comment = expense.Comment,
                CostCenterId = expense.CostCenterId,
                CreateDate = expense.CreateDate,
                Deleted = expense.Deleted,
                DuePayment = expense.DuePayment,
                ModifiedDate = expense.ModifiedDate,
                ConditionId = expense.ConditionId,
                PersonId = expense.PersonId,
                ExpenseNumber = expense.ExpenseNumber,
                ExpenseTotalSeq = expense.ExpenseTotalSeq
            };
        }

        public BankTrans GetBankTrans(PayAccount data, ExpenseTrans expense, int businessId, int? categorId)
        {
            return new BankTrans()
            {
                Bank = expense.Bank,
                AccountBankId = expense.AccountBankId.Value,
                CreateDate = System.DateTime.UtcNow,
                DueDate = data.DueDate,
                BusinessEntityId = businessId,
                Description = data.Comment,
                MidleDesc = expense.Midledesc,
                ChartAccountId = categorId,
                Signal = 2,
                Total = expense.Total,
                Deleted = false,
                ExpenseTrans = expense
            };
        }

        public BankTrans GetBankTrans(PayAccount data, RevenueTrans revenue, int businessId, int? categoryId)
        {
            return new BankTrans()
            {
                Bank = revenue.Bank,
                AccountBankId = revenue.AccountBankId.Value,
                CreateDate = System.DateTime.UtcNow,
                DueDate = data.DueDate,
                BusinessEntityId = businessId,
                Description = data.Comment,
                MidleDesc = revenue.Midledesc,
                ChartAccountId = categoryId,
                Signal = 1,
                Total = revenue.Total,
                Deleted = false,
                RevenueTrans = revenue
            };
        }


        public RevenueTrans GetRevenueTrans(Revenue revenue, int businessId)
        {
            return new RevenueTrans()
            {
                BusinessEntityId = businessId,
                ConditionId = revenue.ConditionId,
                Description = revenue.Name,
                CreateDate = revenue.CreateDate.HasValue ? revenue.CreateDate.Value : System.DateTime.UtcNow,
                Midledesc = "INC",
                Signal = 1,
                Total = revenue.Total,
                Revenue = revenue
            };

        }

        public RevenueTrans GetRevenueTrans(PayAccount data, int businessId, string midleDesc, int signal)
        {
            return new RevenueTrans()
            {
                AccountBankId = data.AccountBankId,
                BusinessEntityId = businessId,
                CreateDate = data.DueDate,
                Description = data.Comment,
                Midledesc = midleDesc,
                ConditionId = data.ConditionId,
                RevenueId = data.RevenueId,
                Signal = signal,
                Total = data.Payment.Value
            };
        }

        public Revenue GetRevenue(Revenue revenue, int businessId, TempFinancialSplit item)
        {
            return new Revenue()
            {

                BusinessEntityId = businessId,
                Name = revenue.Name,
                Total = (decimal)item.Total,
                DueDate = item.DueDate,
                RevenueSeq = item.Seq,
                ChartAccountId = revenue.ChartAccountId,
                Comment = revenue.Comment,
                CostCenterId = revenue.CostCenterId,
                CreateDate = revenue.CreateDate,
                Deleted = revenue.Deleted,
                DuePayment = revenue.DuePayment,
                ModifiedDate = revenue.ModifiedDate,
                ConditionId = revenue.ConditionId,
                PersonId = revenue.PersonId,
                RevenueNumber = revenue.RevenueNumber,
                RevenueTotalSeq = revenue.RevenueTotalSeq
            };
        }


        public async Task<IQueryable<VExpense>> GetVExpense(FinancialReportsModel filter)
        {
            var data = await _vExpenseServices.QueryAsync();

            if (!string.IsNullOrEmpty(filter.suplier))
                data = data.Where(a => a.Person.FirstName.Contains(filter.suplier) || a.Person.LastName.Contains(filter.suplier));

            if (!string.IsNullOrEmpty(filter.name))
                data = data.Where(a => a.Name.Contains(filter.name));

            if (!string.IsNullOrEmpty(filter.ExpenseNumber))
                data = data.Where(a => a.ExpenseNumber.Contains(filter.ExpenseNumber));

            if ((filter.dueDateStart.HasValue))
                data = data.Where(a => a.DueDate.Value.Date >= filter.dueDateStart.Value.Date);

            if ((filter.dueDateEnd.HasValue))
                data = data.Where(a => a.DueDate.Value.Date <= filter.dueDateEnd.Value.Date);

            if ((filter.duePayStart.HasValue))
                data = data.Where(a => a.DuePayment.Value.Date >= filter.duePayStart.Value.Date);

            if ((filter.duePayEnd.HasValue))
                data = data.Where(a => a.DuePayment.Value.Date <= filter.duePayEnd.Value.Date);

            if ((filter.createDateStart.HasValue))
                data = data.Where(a => a.CreateDate.Value.Date >= filter.createDateStart.Value.Date);

            if ((filter.createDateEnd.HasValue))
                data = data.Where(a => a.CreateDate.Value.Date <= filter.createDateEnd.Value.Date);

            if ((filter.CategoryId.HasValue))
                data = data.Where(a => a.Expense.ChartAccountId == filter.CategoryId);

            if ((filter.value.HasValue))
                data = data.Where(a => a.Total == filter.value.Value);


            return data;
        }


        public async Task<IQueryable<VRevenue>> GetVRevenue(FinancialReportsModel filter)
        {
            var data = await  _vRevenueServices.QueryAsync ();

            if (!string.IsNullOrEmpty(filter.suplier))
                data = data.Where(a => a.Person.FirstName.Contains(filter.suplier) || a.Person.LastName.Contains(filter.suplier));

            if (!string.IsNullOrEmpty(filter.name))
                data = data.Where(a => a.Name.Contains(filter.name));

            if (!string.IsNullOrEmpty(filter.ExpenseNumber))
                data = data.Where(a => a.RevenueNumber.Contains(filter.ExpenseNumber));

            if ((filter.dueDateStart.HasValue))
                data = data.Where(a => a.DueDate.Value.Date >= filter.dueDateStart.Value.Date);

            if ((filter.dueDateEnd.HasValue))
                data = data.Where(a => a.DueDate.Value.Date <= filter.dueDateEnd.Value.Date);

            if ((filter.duePayStart.HasValue))
                data = data.Where(a => a.DuePayment.Value.Date >= filter.duePayStart.Value.Date);

            if ((filter.duePayEnd.HasValue))
                data = data.Where(a => a.DuePayment.Value.Date <= filter.duePayEnd.Value.Date);

            if ((filter.createDateStart.HasValue))
                data = data.Where(a => a.CreateDate.Value.Date >= filter.createDateStart.Value.Date);

            if ((filter.createDateEnd.HasValue))
                data = data.Where(a => a.CreateDate.Value.Date <= filter.createDateEnd.Value.Date);

            if ((filter.CategoryId.HasValue))
                data = data.Where(a => a.Revenue.ChartAccountId == filter.CategoryId);

            if ((filter.value.HasValue))
                data = data.Where(a => a.Total == filter.value.Value);


            return data;
        }



        public async Task<IQueryable<BankTrans>> GetBankTans(FinancialReportsModel filter)
        {
            var data = await _bankTransServices.QueryAsync (a => a.Deleted == false);

            if (!string.IsNullOrEmpty(filter.suplier))
                data = data.Where(a => a.Description.Contains(filter.suplier) );


            if ((filter.createDateStart.HasValue))
                data = data.Where(a => a.CreateDate.Date >= filter.createDateStart.Value.Date);

            if ((filter.createDateEnd.HasValue))
                data = data.Where(a => a.CreateDate.Date <= filter.createDateEnd.Value.Date);



            if ((filter.dueDateStart.HasValue))
                data = data.Where(a => a.DueDate.Value.Date >= filter.dueDateStart.Value.Date);

            if ((filter.dueDateEnd.HasValue))
                data = data.Where(a => a.DueDate.Value.Date <= filter.dueDateEnd.Value.Date);
             

            if ((filter.CategoryId.HasValue))
                data = data.Where(a => a.ChartAccountId == filter.CategoryId);

            if ((filter.AccountBankId.HasValue))
                data = data.Where(a => a.AccountBankId == filter.AccountBankId);



            if ((filter.value.HasValue))
                data = data.Where(a => a.Total == filter.value.Value);

            return data;
        }



        public async Task<IQueryable<VCashFlow>> GetVCashFlow(FinancialReportsModel filter)
        {
            var data = await _cVCashFlowServices.QueryAsync();


            if (!string.IsNullOrEmpty(filter.suplier))
                data = data.Where(a => a.Person.FirstName.Contains(filter.suplier) || a.Person.LastName.Contains(filter.suplier));

            if (!string.IsNullOrEmpty(filter.name))
                data = data.Where(a => a.Name.Contains(filter.name));

            if (!string.IsNullOrEmpty(filter.ExpenseNumber))
                data = data.Where(a => a.CashFlowNumber.Contains(filter.ExpenseNumber));

            if ((filter.dueDateStart.HasValue))
                data = data.Where(a => a.DueDate.Value.Date >= filter.dueDateStart.Value.Date);

            if ((filter.dueDateEnd.HasValue))
                data = data.Where(a => a.DueDate.Value.Date <= filter.dueDateEnd.Value.Date);

            if ((filter.duePayStart.HasValue))
                data = data.Where(a => a.DuePayment.Value.Date >= filter.duePayStart.Value.Date);

            if ((filter.duePayEnd.HasValue))
                data = data.Where(a => a.DuePayment.Value.Date <= filter.duePayEnd.Value.Date);

            if ((filter.createDateStart.HasValue))
                data = data.Where(a => a.CreateDate.Value.Date >= filter.createDateStart.Value.Date);

            if ((filter.createDateEnd.HasValue))
                data = data.Where(a => a.CreateDate.Value.Date <= filter.createDateEnd.Value.Date);

             

            if ((filter.value.HasValue))
                data = data.Where(a => a.Total == filter.value.Value);


            return data;
        }

    }
}
