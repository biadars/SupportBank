using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportBank
{
    public class Bank
    {
        private Dictionary<string, Account> accounts;

        public Bank()
        {
            accounts = new Dictionary<string, Account>();
        }

        private bool Exists(string name)
        {
            return accounts.ContainsKey(name);
        }

        private void AddAccount(string name)
        {
            accounts.Add(name, new Account(name));
        }

        public Account GetAccount(string name)
        {
            if (!Exists(name))
                AddAccount(name);
            return accounts[name];
        }
    }
}
