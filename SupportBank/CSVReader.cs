using Microsoft.VisualBasic.FileIO;
using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportBank
{
    public class CSVReader
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
        public static Bank ReadCSV(string filepath)
        {
            logger.Info("Reading CSV file " + filepath);
            Bank bank = new Bank();
            string folder = GetHomeFolder();
            string path = folder + filepath;
            using (TextFieldParser csvParser = new TextFieldParser(path))
            {
                logger.Debug("Found file " + path + ". Parsing.");
                csvParser.CommentTokens = new string[] { "#" };
                csvParser.SetDelimiters(new string[] { "," });
                csvParser.HasFieldsEnclosedInQuotes = true;
                csvParser.ReadLine();

                while (!csvParser.EndOfData)
                {
                    string[] fields = csvParser.ReadFields();
                    logger.Debug("Read CSV line: " + string.Join(",", fields));
                    Transaction transaction = new Transaction(
                        transactionDate: Convert.ToDateTime(fields[0]),
                        transactionFrom: fields[1],
                        transactionTo: fields[2], 
                        transactionNarrative: fields[3], 
                        transactionAmount: float.Parse(fields[4]));
                    logger.Debug("Created transaction " + transaction);
                    Account from = bank.GetOrCreateAccount(transaction.From);
                    from.AddTransaction(transaction);
                    Account to = bank.GetOrCreateAccount(transaction.To);
                    to.AddTransaction(transaction);
                }
                logger.Info("Finished parsing CSV");
            }
            return bank;
        }

        private static string GetHomeFolder()
        {
            logger.Debug("Calculating home folder");
            string folder = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
            folder = Directory.GetParent(folder).FullName;
            folder = Directory.GetParent(folder).FullName;
            folder = Directory.GetParent(folder).FullName;
            logger.Debug("Found home folder: " + folder);
            return folder;
        }
    }
}
