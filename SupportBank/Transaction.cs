using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportBank
{
    public class Transaction
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

        public Transaction(DateTime transactionDate, string transactionFrom, string transactionTo, string transactionNarrative, decimal transactionAmount)
        {
            Date = transactionDate;
            From = transactionFrom;
            To = transactionTo;
            Narrative = transactionNarrative;
            Amount = transactionAmount;
        }

        public override string ToString()
        {
            string msg = "Date: " + Date;
            msg += "\tFrom: " + From;
            msg += "\t To: " + To;
            msg += "\t Narrative: " + Narrative;
            msg += "\tAmount: " + Amount;
            return msg;
        }
    }
}
