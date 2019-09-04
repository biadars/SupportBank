using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportBank
{
    public class Account
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
        public string Name { get; private set; }
        public float Balance { get; private set; }
        public List<Transaction> Transactions { get; private set; }

        public Account(string fullName, int initial=0)
        {
            Name = fullName;
            Balance = initial;
            Transactions = new List<Transaction>();
        }

        public void AddTransaction(Transaction transaction)
        {
            if (transaction.From == Name)
                Balance -= transaction.Amount;
            if (transaction.To == Name)
                Balance += transaction.Amount;
            Transactions.Add(transaction);
            logger.Debug("Added transaction to account " + Name + ". Balance updated to " + Balance);
        }
    }
}
