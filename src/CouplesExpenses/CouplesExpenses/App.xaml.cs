using System;
using System.Linq;
using CouplesExpenses.DAL;
using CouplesExpenses.Models;
using CouplesExpenses.Services;
using CouplesExpenses.Views;
using Xamarin.Forms;

namespace CouplesExpenses
{
	public partial class App : Application
	{

		public App ()
		{
			InitializeComponent();


            // MainPage = new MainPage();
            MainPage = new FullCameraPage();

        }

		protected override async void OnStart ()
		{
            // Handle when your app starts
            var manager = ExpenseDAOManager.DefaultManager;

            var expenseDAOs = await manager.GetExpensesAsyncByCoupleId(Constants.CoupeId);
            var expensesByMember = expenseDAOs.GroupBy(exp => exp.PayerId);
            MockExpensesStore store = new MockExpensesStore();
            CouplesExpensesReview review = store.GetCurrent().GetAwaiter().GetResult();
            if (expensesByMember.Any(x => x.Key == Constants.member2))
            {
                var partnerExpenses = expensesByMember.Single(x => x.Key == Constants.member2)
                .Select(exp => new Models.Expense
                {
                    Id = exp.Name,
                    Amount = exp.Amount
                });
                review.Partner.LoadExpenses(partnerExpenses);
                review.LoadSituation();
            }

            if(expensesByMember.Any(x => x.Key == Constants.member1))
            {
                var userExpenses = expensesByMember.Single(x => x.Key == Constants.member1)
                    .Select(exp => new Models.Expense
                    {
                        Id = exp.Name,
                        Amount = exp.Amount
                    });

                foreach(var exp in userExpenses)
                {
                    review.AddExpense(exp);
                }
                
            }

            

            ((FullCameraPage)MainPage).UpdateData(manager, review);

        }

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
