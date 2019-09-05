using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportBank.InternalStructures
{
    public class Bank
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
        private Dictionary<string, Account> accounts;

        public Bank(List<Transaction> transactions)
        {
            accounts = new Dictionary<string, Account>();
            AddTransactions(transactions);
        }

        public bool Exists(string name)
        {
            return accounts.ContainsKey(name);
        }

        private void AddAccount(string name)
        {
            accounts.Add(name, new Account(name));
        }

        public Account GetOrCreateAccount(string name)
        {
            if (!Exists(name))
            {
                logger.Debug("Account \"" + name + "\" did not exist. Creating account.");
                AddAccount(name);
            }
            return accounts[name];
        }

        public void AddTransactions(List<Transaction> transactions)
        {
            for (int i = 0; i < transactions.Count; i++)
            {
                Account account = GetOrCreateAccount(transactions[i].From);
                account.AddTransaction(transactions[i]);
                account = GetOrCreateAccount(transactions[i].To);
                account.AddTransaction(transactions[i]);
            }
        }

        public Dictionary<string, decimal> GetBalances()
        {
            Dictionary<string, decimal> balances = new Dictionary<string, decimal>();
            foreach (string name in accounts.Keys)
            {
                balances[name] = accounts[name].Balance;
            }
            return balances;
        }
    }
}
