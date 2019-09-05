using Microsoft.VisualBasic.FileIO;
using NLog;
using SupportBank.InternalStructures;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportBank.FileHandling
{
    public class CSVReader : FileReader
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
        public static Bank ReadCSV(string filepath)
        {
            logger.Info("Reading CSV file " + filepath);
            Bank bank = new Bank();
            string folder = GetHomeFolder();
            string path = folder + filepath;
            TerminateOnFileNotFound(path);
            using (TextFieldParser csvParser = new TextFieldParser(path))
            {
                logger.Debug("Found file " + path + ". Parsing.");
                ConfigureCSVParser(csvParser);
                bank = ParseCSV(csvParser);
                logger.Info("Finished parsing CSV");
            }
            return bank;
        }

        private static TextFieldParser ConfigureCSVParser(TextFieldParser csvParser)
        {
            csvParser.CommentTokens = new string[] { "#" };
            csvParser.SetDelimiters(new string[] { "," });
            csvParser.HasFieldsEnclosedInQuotes = true;
            return csvParser;
        }

        private static Bank ParseCSV(TextFieldParser csvParser)
        {
            Bank bank = new Bank();
            bool valid = true;
            csvParser.ReadLine();
            while (!csvParser.EndOfData)
            {
                string[] fields = csvParser.ReadFields();
                logger.Debug("Read CSV line " + (csvParser.LineNumber - 1) + ": " + string.Join(",", fields));
                if (!ValidateData(fields[0], fields[4], csvParser.LineNumber - 1))
                {
                    valid = false;
                    continue;
                }
                Transaction transaction = new Transaction(
                    transactionDate: Convert.ToDateTime(fields[0]),
                    transactionFrom: fields[1],
                    transactionTo: fields[2],
                    transactionNarrative: fields[3],
                    transactionAmount: decimal.Parse(fields[4]));
                Account from = bank.GetOrCreateAccount(transaction.From);
                from.AddTransaction(transaction);
                Account to = bank.GetOrCreateAccount(transaction.To);
                to.AddTransaction(transaction);

            }
            if (!valid)
            {
                Console.WriteLine("Invalid data supplied. Exiting program.");
                Console.ReadLine();
                Environment.Exit(0);
            }
            return bank;
        }
    }
}
