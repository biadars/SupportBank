using NLog;
using NLog.Config;
using NLog.Targets;
using SupportBank.FileHandling;
using SupportBank.InternalStructures;
using SupportBank.UI;
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
        private static Bank bank;
        static void Main(string[] args)
        {
            ConfigureLogging();
            bank = UserInterface.InitialiseBank();
            RunUI();
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

        private static void RunUI()
        {
            while(true)
            {
                UserResponse response = UserResponseFactory.ProcessUserInput();
                switch(response.Command)
                {
                    case "list":
                        ListCommand(response.Argument);
                        break;
                    case "import":
                        List<Transaction> transactions = UserInterface.UpdateBank(response.Argument);
                        bank.AddTransactions(transactions);
                        break;
                    case "exit":
                        logger.Info("Exiting application according to user request.");
                        Environment.Exit(0);
                        break;
                    default:
                        continue;
                } 
            }
        }

        private static void ListCommand(string argument)
        {
            if (argument == "all")
                UserInterface.PrintBalances(bank.GetBalances());
            else
            {
                if (bank.Exists(argument))
                {
                    logger.Info("Retrieving account " + argument);
                    UserInterface.PrintTransactions(bank.GetOrCreateAccount(argument));
                }
                else
                {
                    logger.Info("No account with name " + argument + " found.");
                    Console.WriteLine("No account with name " + argument);
                }
            }
        }

    }
}
