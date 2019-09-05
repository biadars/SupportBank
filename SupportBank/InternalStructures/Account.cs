using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportBank.InternalStructures
{
    public class Account
    {
        public string Name { get; private set; }
        public decimal Balance { get; private set; }
        public List<Transaction> Transactions { get; private set; }

        public Account(string fullName, decimal initial = 0)
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
        }
    }
}
