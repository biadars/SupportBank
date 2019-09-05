using NLog;
using SupportBank.FileHandling;
using SupportBank.InternalStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportBank.UI
{
    public class UserInterface
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

        public static void PrintTransactions(Account account)
        {
            logger.Info("Printing transactions for account " + account.Name);
            Transaction transaction;
            List<Transaction> transactions = account.Transactions;
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
            logger.Info(transactions.Count + " transactions printed.");
        }

        public static void PrintBalances(Dictionary<string, decimal> balances)
        {
            logger.Info("Printing all account balances.");
            foreach (string name in balances.Keys)
            {
                if (balances[name] <= 0)
                    Console.WriteLine(name + " owes " + -1 * balances[name]);
                else
                    Console.WriteLine(name + " is owed " + balances[name]);
            }
            logger.Info("Printed balances for all " + balances.Keys.Count + " accounts.");
        }

        private static List<Transaction> GetUserFile(string path)
        {
            try
            {
                List<Transaction> transactions = FileImporter.ImportFile(path);
                Console.WriteLine("Successfully imported file " + path);
                logger.Info("Successfully imported file " + path);
                return transactions;
            }
            catch (Exception exception)
            {
                logger.Error(exception.Message);
                Console.WriteLine(exception.Message);
                throw exception;
            }
        }

        public static Bank InitialiseBank()
        {
            List<Transaction> transactions;
            while (true)
            {
                try
                {
                    Console.WriteLine("Input the file path relative to the applicaion input directory: " + FileReader.GetHomeFolder());
                    string path = Console.ReadLine();
                    transactions = GetUserFile(path);
                    return new Bank(transactions);
                }
                catch (Exception exception)
                {
                    continue;
                }
            }
        }

        public static List<Transaction> UpdateBank(string path)
        {
            List<Transaction> transactions;
            try
            {
                transactions = GetUserFile(path);
                return transactions;
            }
            catch (Exception exception)
            {
                return new List<Transaction>();
            }
        }
    }
}
