using CouplesExpenses;
using CouplesExpenses.DAL;
using System;

namespace LocalTests
{
    class Program
    {
        static void Main(string[] args)
        {
            Expense expense = new Expense
            {
                Name = "Luz",
                CoupleId = Constants.CoupeId,
                PayerId = Constants.member2,
                Amount = 120,

            };

            ExpenseDAOManager dao = ExpenseDAOManager.DefaultManager;
            //var expenses = dao.GetExpensesAsyncByCoupleId(Constants.CoupeId).GetAwaiter().GetResult();
            var table = dao.CurrentClient.GetTable<Expense>();
            table.InsertAsync(expense).GetAwaiter().GetResult();
            //Console.WriteLine("Hello World! " + expenses.Count.ToString());
        }
    }
}
