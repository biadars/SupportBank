using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportBank
{
    class Program
    {
        static void Main(string[] args)
        {
            Bank bank = CSVReader.ReadCSV(@"\Input\Transactions2014.csv");
            UserInterface(bank);
        }


        static void UserInterface(Bank bank)
        {
            string input;
            while(true)
            {
                Console.WriteLine("Type command (list all / list <name> / exit)");
                input = Console.ReadLine();
                if (input == "exit")
                    return;
                if (input == "list all")
                    PrintBalances(bank);
                else if (input.Length > 4 && input.Substring(0, 4) == "list")
                {
                    string name = input.Substring(5);
                    if (bank.Exists(name))
                        PrintTransactions(bank.GetOrCreateAccount(name));
                    else
                        Console.WriteLine("No account with name " + name);
                }
                else
                    Console.WriteLine("Unkown command: " + input);
            }
        }

        private static void PrintTransactions(Account account)
        {
            Transaction transaction;
            List < Transaction > transactions = account.Transactions;
            Console.WriteLine("Transactions for account " + account.Name + ":");
            Console.WriteLine("{0,10} {1,15} {2,15} {3,50} {4,10}",
                "Date", "From", "To", "Narrative", "Amount");
            for (int i = 0; i < transactions.Count; i++)
            {
                transaction = transactions[i];
                Console.WriteLine("{0:d} {1,15} {2,15} {3,50} {4,10}",
                    transaction.Date, transaction.From, transaction.To,
                    transaction.Narrative, transaction.Amount);
            }
        }

        private static void PrintBalances(Bank bank)
        {
            Dictionary<string, float> balances = bank.GetBalances();
            foreach (string name in balances.Keys)
            {
                if (balances[name] <= 0)
                    Console.WriteLine(name + " owes " + -1 * balances[name]);
                else
                    Console.WriteLine(name + " is owed " + balances[name]);
            }
        }
    }
}
