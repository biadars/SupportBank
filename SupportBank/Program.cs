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
        static void Main(string[] args)
        {
            ConfigureLogging();
            Bank bank = CSVReader.ReadCSV(@"\Input\Transactions2014.csv");
            UserInterface.RunUI(bank);
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
