using NLog;
using NLog.Config;
using NLog.Targets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportBank
{
    class Program
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {
            ConfigureLogging();
            Bank bank = CSVReader.ReadCSV(@"\Input\DodgyTransactions2015.csv");
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
                {
                    logger.Info("Exiting application according to user request.");
                    return;
                }
                if (input == "list all")
                {
                    logger.Info("Listing all accounts.");
                    PrintBalances(bank);
                }
                else if (input.Length > 4 && input.Substring(0, 4) == "list")
                {
                    string name = input.Substring(5);
                    if (bank.Exists(name))
                    {
                        logger.Info("Retrieving account " + name);
                        PrintTransactions(bank.GetOrCreateAccount(name));
                    }
                    else
                    {
                        logger.Info("No account with name " + name + " found.");
                        Console.WriteLine("No account with name " + name);
                    }
                }
                else
                {
                    logger.Info("Invalid user command: " + input);
                    Console.WriteLine("Unkown command: " + input);
                }
            }
        }

        private static void PrintTransactions(Account account)
        {
            logger.Info("Printing transactions for account " + account.Name);
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

        private static void ConfigureLogging()
        {
            var config = new LoggingConfiguration();
            var target = new FileTarget {
                FileName = @"C:\Work\Logs\SupportBank.log",
                Layout = @"${longdate} ${level} - ${logger}: ${message}",
                DeleteOldFileOnStartup = true};
            config.AddTarget("File Logger", target);
            config.LoggingRules.Add(new LoggingRule("*", LogLevel.Debug, target));
            LogManager.Configuration = config;
            logger.Debug("Logging successfully configured");
        }
    }
}
