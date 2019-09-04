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
            try
            {
                using (TextFieldParser csvParser = new TextFieldParser(path))
                {
                    logger.Debug("Found file " + path + ". Parsing.");
                    ConfigureCSVParser(csvParser);
                    bank = ParseCSV(csvParser);
                    logger.Info("Finished parsing CSV");
                }
                return bank;
            }
            catch (FileNotFoundException exception)
            {
                logger.Error(exception.Message);
                Console.WriteLine("File " + path + " was not found.");
                return bank;
            }
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
            csvParser.ReadLine();
            while (!csvParser.EndOfData)
            {
                string[] fields = csvParser.ReadFields();
                logger.Debug("Read CSV line " + (csvParser.LineNumber - 1) + ": " + string.Join(",", fields));
                if (!ValidateData(fields[0], fields[4], csvParser.LineNumber - 1))
                    continue;
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
            return bank;
        }

        private static bool ValidateData(string date, string amount, long line)
        {
            try
            {
                Convert.ToDateTime(date);
            }
            catch (System.FormatException exception)
            {
                logger.Error(exception.Message);
                Console.WriteLine("Error: Could not parse line " + line + ". " + date + " is not a validly formated date.");
                return false;
            }
            try
            {
                decimal.Parse(amount);
            }
            catch (System.FormatException exception)
            {
                logger.Error(exception.Message);
                Console.WriteLine("Error: Could not parse line " + line + ". " + amount + " is not a floating point number.");
                return false;
            }
            return true;
        }
    }
}
