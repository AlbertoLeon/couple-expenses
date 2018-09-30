using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CouplesExpenses.Models;

[assembly: Xamarin.Forms.Dependency(typeof(CouplesExpenses.Services.MockExpensesStore))]
namespace CouplesExpenses.Services
{
    public class MockExpensesStore : IDataStore<Expense>
    {
        List<Expense> items;
        CouplesExpensesReview review;

        public MockExpensesStore()
        {
            items = new List<Expense>();
            UserExpenses ue = new UserExpenses(Constants.member1, "Alberto");
            PartnerExpenses pe = new PartnerExpenses(Constants.member2, "Esther");
            review = new CouplesExpensesReview(Constants.CoupeId, ue, pe);
        }

        public async Task<CouplesExpensesReview> GetCurrent()
        {
            return review;
        }

        public async Task<bool> AddItemAsync(Expense item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Expense item)
        {
            var _item = items.Where((Expense arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(_item);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(Expense item)
        {
            var _item = items.Where((Expense arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(_item);

            return await Task.FromResult(true);
        }

        public async Task<Expense> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Expense>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}