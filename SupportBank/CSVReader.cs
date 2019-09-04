using Microsoft.VisualBasic.FileIO;
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
        public static Bank ReadCSV(string filepath)
        {
            Bank bank = new Bank();
            string folder = GetHomeFolder();
            string path = folder + filepath;
            using (TextFieldParser csvParser = new TextFieldParser(path))
            {
                csvParser.CommentTokens = new string[] { "#" };
                csvParser.SetDelimiters(new string[] { "," });
                csvParser.HasFieldsEnclosedInQuotes = true;
                csvParser.ReadLine();

                while (!csvParser.EndOfData)
                {
                    string[] fields = csvParser.ReadFields();
                    Transaction transaction = new Transaction(
                        transactionDate: Convert.ToDateTime(fields[0]),
                        transactionFrom: fields[1],
                        transactionTo: fields[2], 
                        transactionNarrative: fields[3], 
                        transactionAmount: float.Parse(fields[4]));
                    Account from = bank.GetOrCreateAccount(transaction.From);
                    from.AddTransaction(transaction);
                    Account to = bank.GetOrCreateAccount(transaction.To);
                    to.AddTransaction(transaction);
                }
            }
            return bank;
        }

        private static string GetHomeFolder()
        {
            string folder = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
            folder = Directory.GetParent(folder).FullName;
            folder = Directory.GetParent(folder).FullName;
            folder = Directory.GetParent(folder).FullName;
            return folder;
        }
    }
}
