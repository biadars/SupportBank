using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportBank
{
    public class Account
    {
        private string name;
        private float balance;
        private List<Transaction> transactions;

        public Account(string fullName, int initial=0)
        {
            name = fullName;
            balance = initial;
            transactions = new List<Transaction>();
        }

        public void AddTransaction(Transaction transaction)
        {
            if (transaction.GetFrom() == name)
                balance -= transaction.GetAmount();
            if (transaction.GetTo() == name)
                balance += transaction.GetAmount();
            transactions.Add(transaction);
        }

        public void PrintTransactions()
        {
            Transaction transaction;
            Console.WriteLine("Transactions for account " + name + ":");
            Console.WriteLine("{0,");
            for (int i = 0; i < transactions.Count; i++)
            {
                transaction = transactions[i];
                Console.WriteLine("\t" + transaction.GetDate());
            }
        }
    }
}
