using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportBank.FileHandling
{
    public class JSONTransaction
    {
        [JsonProperty("Date")]
        public DateTime Date { get; private set; }
        [JsonProperty("FromAccount")]
        public string From { get; private set; }
        [JsonProperty("ToAccount")]
        public string To { get; private set; }
        [JsonProperty("Narrative")]
        public string Narrative { get; private set; }
        [JsonProperty("Amount")]
        public decimal Amount { get; private set; }

        public JSONTransaction(DateTime transactionDate, string transactionFrom, string transactionTo, string transactionNarrative, decimal transactionAmount)
        {
            Date = transactionDate;
            From = transactionFrom;
            To = transactionTo;
            Narrative = transactionNarrative;
            Amount = transactionAmount;
        }
    }
}
