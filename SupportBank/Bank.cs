using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportBank
{
    public class Bank
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
        private Dictionary<string, Account> accounts;

        public Bank()
        {
            accounts = new Dictionary<string, Account>();
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

        public Dictionary<string, float> GetBalances()
        {
            Dictionary<string, float> balances = new Dictionary<string, float>();
            foreach (string name in accounts.Keys)
                balances[name] = accounts[name].Balance;
            return balances;
        }
    }
}
