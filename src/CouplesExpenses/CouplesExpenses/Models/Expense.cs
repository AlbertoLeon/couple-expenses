using System;
using System.Collections.Generic;
using System.Text;

namespace CouplesExpenses.Models
{
    public enum ExpenseCreationWay {
        ByCamera,
        ByVoice,
        ByHand
    }
    public class Expense
    {
        public string Id { get; set; }
        public DateTime PaidDate { get; set; }
        public string MediaPath { get; set; }
        public ExpenseCreationWay CreatedBy { get; set; }
        public double Amount { get; set; }
    }
}
