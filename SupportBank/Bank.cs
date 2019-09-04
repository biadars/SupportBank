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

        public bool Exists(string name)
        {
            return accounts.ContainsKey(name);
        }

        public void AddAccount(string name)
        {
            accounts.Add(name, new Account(name));
        }

        public void PrintAccounts()
        {
            foreach (string key in accounts.Keys)
                Console.WriteLine(key);
        }
    }
}
