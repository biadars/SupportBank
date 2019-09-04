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

        public void ListAccounts()
        {
            foreach (string name in accounts.Keys)
            {
                if (accounts[name].GetBalance() <= 0)
                    Console.WriteLine(name + " owes " + -1 * accounts[name].GetBalance());
                else
                    Console.WriteLine(name + " is owed " + accounts[name].GetBalance());
            }
        }
    }
}
