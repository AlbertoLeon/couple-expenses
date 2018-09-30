using Newtonsoft.Json;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace CouplesExpenses.DAL
{
    public class Expense
    {
        string id;
        string name;
        double amount;
        Guid coupleId;
        Guid payerId;

        [JsonProperty(PropertyName = "id")]
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        [JsonProperty(PropertyName = "name")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        [JsonProperty(PropertyName = "amount")]
        public double Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        [JsonProperty(PropertyName = "coupleId")]
        public Guid CoupleId
        {
            get { return coupleId; }
            set { coupleId = value; }
        }

        [JsonProperty(PropertyName = "payerId")]
        public Guid PayerId
        {
            get { return payerId; }
            set { payerId = value; }
        }

        [Version]
        public string Version { get; set; }
    }
}
