using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microsoft.ProjectOxford.Vision;
using Microsoft.ProjectOxford.Vision.Contract;
using CouplesExpenses.Services;
using CouplesExpenses.Models;
using CouplesExpenses.DAL;

namespace CouplesExpenses.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FullCameraPage : ContentPage
	{
        public FullCameraPage()
        {
            InitializeComponent();
            AddExpenseByPhoto.Clicked += AddExpenseByPhoto_Clicked;
            AddExpenseByHand.Clicked += Add_by_Hand_Clicked;
        }

        public void UpdateData (ExpenseDAOManager manager, CouplesExpensesReview review)
		{
            MessagingCenter.Subscribe<NewExpensePage, DAL.Expense>(this, "AddExpense", async (obj, expense) =>
            {
                var _expense = expense as DAL.Expense;
                review.AddExpense(
                    new Models.Expense
                    {
                        Id = _expense.Name,
                        Amount = _expense.Amount
                    }
                    );
                _expense.CoupleId = review.CoupleId;
                _expense.PayerId = review.User.Id;

                await manager.SaveTaskAsync(_expense);

                OnUserAddExpense(
                    userTotal: review.User.Total.ToString(),
                    situationName: GetSituationText(review.Situation.Status, review.Partner.Name),
                    situationAmount: review.Situation.Amount.ToString()
                );
            });

            SetUp(review);
        }

        public void SetUp(CouplesExpensesReview review)
        {
            UserName.Text = review.User.Name;
            UserTotal.Text = review.User.Total.ToString();
            SituationName.Text = GetSituationText(review.Situation.Status, review.Partner.Name);
            SituationAmount.Text = review.Situation.Amount.ToString();
            PartnerName.Text = review.Partner.Name;
            PartnerTotal.Text = review.Partner.Total.ToString();
        }

        public void OnUserAddExpense(string userTotal, string situationName, string situationAmount)
        {
            SetUserTotal(userTotal);
            RefreshSituation(situationName, situationAmount);
        }

        public void OnPartnerAddExpense(string partnerTotal, string situationName, string situationAmount)
        {
            SetPartnerTotal(partnerTotal);
            RefreshSituation(situationName, situationAmount);
        }

        public void SetUserTotal(string userTotal)
        {
            UserTotal.Text = userTotal;
        }

        public void SetPartnerTotal(string partnerTotal)
        {
            PartnerTotal.Text = partnerTotal;
        }

        public void RefreshSituation(string name, string amount)
        {
            SituationName.Text = name;
            SituationAmount.Text = amount;
        }

        private string GetSituationText(SituationStatus status, string partnerName)
        {
            switch (status)
            {
                case SituationStatus.PartnerOwes:
                    return $"{partnerName} owes you";
                case SituationStatus.YouOwe:
                    return "You owe";
                default:
                    return "Balanced;";
            }
        }

        async void AddExpenseByPhoto_Clicked(object sender, System.EventArgs e)
        {
            var cameraPage = new CameraPage();
            cameraPage.OnPhotoResult += CameraPage_OnPhotoResult;
            await Navigation.PushModalAsync(cameraPage);
        }

        async void Add_by_Hand_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewExpensePage()));
        }


        async void CameraPage_OnPhotoResult(PhotoResultEventArgs result)
        {
            await Navigation.PopModalAsync();
            if (!result.Success)
                return;

            MemoryStream photo = new MemoryStream(result.Image);
            OcrResults text;
            var client = new VisionServiceClient("c56e5e63067b406aa261663d81a89af5");
            text = await client.RecognizeTextAsync(photo);

            // Photo.Source = ImageSource.FromStream(() => new MemoryStream(result.Image));
        }
    }
}