using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CouplesExpenses.Models;

[assembly: Xamarin.Forms.Dependency(typeof(CouplesExpenses.Services.ExpenseConceptsStore))]
namespace CouplesExpenses.Services
{
    public class ExpenseConceptsStore : IDataStore<ExpenseConcept>
    {
        List<ExpenseConcept> items;

        public ExpenseConceptsStore()
        {
            items = new List<ExpenseConcept>();
        }

        public async Task<bool> AddItemAsync(ExpenseConcept item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(ExpenseConcept item)
        {
            var _item = items.Where((ExpenseConcept arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(_item);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(ExpenseConcept item)
        {
            var _item = items.Where((ExpenseConcept arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(_item);

            return await Task.FromResult(true);
        }

        public async Task<ExpenseConcept> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<ExpenseConcept>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}