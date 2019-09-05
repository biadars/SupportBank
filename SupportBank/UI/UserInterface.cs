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
        public static void RunUI()
        {
            Bank bank = InitialiseBank();
            while (true)
            {
                bank = ProcessUserInput(bank);
            }
        }

        private static void PrintTransactions(Account account)
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

        private static void PrintBalances(Bank bank)
        {
            logger.Info("Printing all account balances.");
            Dictionary<string, decimal> balances = bank.GetBalances();
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

        private static Bank InitialiseBank()
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

        private static Bank UpdateBank(Bank bank, string path)
        {
            List<Transaction> transactions;
            try
            {
                transactions = GetUserFile(path);
                bank.AddTransactions(transactions);
                return bank;
            }
            catch (Exception exception)
            {
                return bank;
            }
        }

        private static Bank ProcessUserInput(Bank bank)
        {
            Console.WriteLine("Type command (list all / list <name> / exit)");
            string input = Console.ReadLine();
            if (input == "exit")
            {
                logger.Info("Exiting application according to user request.");
                Environment.Exit(0);
            }
            if (input == "list all")
            {
                logger.Info("Listing all accounts.");
                PrintBalances(bank);
                return bank;
            }
            if (input.Length > 4 && input.Substring(0, 4) == "list")
            {
                string name = input.Substring(5);
                if (bank.Exists(name))
                {
                    logger.Info("Retrieving account " + name);
                    PrintTransactions(bank.GetOrCreateAccount(name));
                    return bank;
                }
                else
                {
                    logger.Info("No account with name " + name + " found.");
                    Console.WriteLine("No account with name " + name);
                }
            }
            if (input.Length > 6 && input.Substring(0, 6) == "import")
                return UpdateBank(bank, input.Substring(7));
            logger.Info("Invalid user command: " + input);
            Console.WriteLine("Unkown command: " + input);
            return bank;
        }
    }
}
