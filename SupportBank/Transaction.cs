using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportBank
{
    public class Transaction
    {
        private DateTime date;
        private string from;
        private string to;
        private string narrative;
        private float amount;

        public Transaction(DateTime transactionDate, string transactionFrom, string transactionTo, string transactionNarrative, float transactionAmount)
        {
            date = transactionDate;
            from = transactionFrom;
            to = transactionTo;
            narrative = transactionNarrative;
            amount = transactionAmount;
        }

        public DateTime GetDate()
        {
            return date;
        }

        public string GetFrom()
        {
            return from;
        }

        public string GetTo()
        {
            return to;
        }

        public string GetNarrative()
        {
            return narrative;
        }

        public float GetAmount()
        {
            return amount;
        }
    }
}
