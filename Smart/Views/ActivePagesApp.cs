using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Smart.Views
{
    public static class ActivePagesApp
    {
        public static string ActivePageKey => "ActivePageKey";
        public static string Person => "Person";
        public static string Revenue => "Revenue";
        public static string Bank => "Bank";
        public static string Expense => "Expense";
        public static string Dashboard => "Dashboard";
        public static string BankTrans => "BankTrans";
        public static string CashFlow => "CashFlow";
        public static string FinanceReports => "FinanceReports";
        public static string Product => "Product";
        public static string IndexPersonNavClass(ViewContext viewContext) => PageNavClassApp(viewContext, Person);
        public static string RevenueNavClass(ViewContext viewContext) => PageNavClassApp(viewContext, Revenue);
        public static string ExpenseNavClass(ViewContext viewContext) => PageNavClassApp(viewContext, Expense);
        public static string DashboardNavClass(ViewContext viewContext) => PageNavClassApp(viewContext, Dashboard);
        public static string BankTransNavClass(ViewContext viewContext) => PageNavClassApp(viewContext, BankTrans);
        public static string CashFlowNavClass(ViewContext viewContext) => PageNavClassApp(viewContext, CashFlow);
        public static string ProductNavClass(ViewContext viewContext) => PageNavClassApp(viewContext, Product);

        public static string FinanceNavClass(ViewContext viewContext)
        {
            return string.Concat(
                PageNavClassApp(viewContext, BankTrans),
                PageNavClassApp(viewContext, Revenue),
                PageNavClassApp(viewContext, Expense),
                PageNavClassApp(viewContext, CashFlow)


                );
        }
        public static string FinanceReportsNavClass(ViewContext viewContext)
        {
            return string.Concat(
                PageNavClassApp(viewContext, FinanceReports) 


                );
        }

        public static string RegNavClass(ViewContext viewContext)
        {
            return string.Concat(
                PageNavClassApp(viewContext, Person),
                PageNavClassApp(viewContext, Product)
                );
        }

        public static string PageNavClassApp(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePageKey"] as string;
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }
        public static void AddActivePageApp(this ViewDataDictionary viewData, string activePage) => viewData[ActivePageKey] = activePage;
    }
}
