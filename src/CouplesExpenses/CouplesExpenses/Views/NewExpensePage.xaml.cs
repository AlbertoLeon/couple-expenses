using CouplesExpenses.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CouplesExpenses.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewExpensePage : ContentPage
	{
        public Expense Item { get; set; }
		public NewExpensePage ()
		{
			InitializeComponent ();

            Item = new Expense
            {
                Name = "Expense name",
                Amount = 0.00
            };

            BindingContext = this;
		}

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddExpense", Item);
            await Navigation.PopModalAsync();
        }
	}
}