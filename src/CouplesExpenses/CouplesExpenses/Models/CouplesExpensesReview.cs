using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Text;

namespace CouplesExpenses.Models
{
    public enum SituationStatus
    {
        Balanced,
        YouOwe,
        PartnerOwes
    }

    public class Situation
    {
        public Situation(double amount)
        {
            Status = GetStatus(amount);
            Amount = Math.Abs(amount);
        }

        private SituationStatus GetStatus(double amount)
        {
            if (amount < 0) return SituationStatus.PartnerOwes;
            if (amount > 0) return SituationStatus.YouOwe;
            return SituationStatus.Balanced;
        }

        public SituationStatus Status { get; }
        public double Amount { get; }
    }

    public class CouplesExpensesReview
    {
        public Guid CoupleId { get; }
        
        public CouplesExpensesReview(Guid coupleId, UserExpenses user, PartnerExpenses partner)
        {
            User = user;
            Partner = partner;
            FromDate = DateTime.Now;
            CoupleId = coupleId;
        }

        public UserExpenses User { get; }
        public PartnerExpenses Partner { get; }
        public double Total { get; private set; }
        public DateTime FromDate { get; }
        public Situation Situation { get; private set; }

        public void AddExpense(Expense expense)
        {
            User.AddExpense(expense);
            Total = Total + expense.Amount;
            ReduceDifference(expense.Amount);
            UpdateSituation();
        }

        public void UpdateSituation()
        {
            Situation = new Situation(DifferenceWithOtherMember);
        }

        public void LoadSituation()
        {
            Total = User.Total + Partner.Total;
            DifferenceWithOtherMember =  Partner.Total - User.Total;
            UpdateSituation();
        }

        public double DifferenceWithOtherMember { get; private set; }

        private void ReduceDifference(double amount)
        {
            DifferenceWithOtherMember = DifferenceWithOtherMember - amount;
        }
    }
}
