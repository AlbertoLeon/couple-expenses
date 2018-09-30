using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;


namespace CouplesExpenses.Models
{
    public class UserExpenses : IMemberExpenses
    {
        private List<Expense> expenses;
        public UserExpenses(Guid id, string name)
        {
            Id = id;
            Name = name;
            expenses = new List<Expense>();
        }

        public Guid Id { get; }
        public string Name { get; }
        public IEnumerable<Expense> Expenses { get => expenses; }
        public double Total { get; private set; }


        public void AddExpense(Expense expense)
        {
            expenses.Add(expense);

            Total = Total + expense.Amount;
        }


    }

    public class PartnerExpenses : IMemberExpenses
    {
        public PartnerExpenses(Guid Id, string name)
        {
            this.Id = Id;
            Name = name;
            expenses = new List<Expense>();
        }
        private List<Expense> expenses;
        public Guid Id { get; }
        public string Name { get; }
        public IEnumerable<Expense> Expenses { get => expenses; }
        public double Total { get; private set; }
     
        public void LoadExpenses(IEnumerable<Expense> expenses)
        {
            this.expenses.AddRange(expenses);
            this.Total = this.expenses.Sum(exp => exp.Amount);
        }
    }

    public interface IMemberExpenses
    {
        Guid Id { get; }
        string Name { get; }
        IEnumerable<Expense> Expenses { get; }
        double Total { get; }
    }
}