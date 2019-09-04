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
        public static Bank ReadCSV()
        {
            Bank bank = new Bank();
            string folder = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
            folder = Directory.GetParent(folder).FullName;
            folder = Directory.GetParent(folder).FullName;
            folder = Directory.GetParent(folder).FullName;
            string path = folder + @"\Input\Transactions2014.csv";
            using (TextFieldParser csvParser = new TextFieldParser(path))
            {
                csvParser.CommentTokens = new string[] { "#" };
                csvParser.SetDelimiters(new string[] { "," });
                csvParser.HasFieldsEnclosedInQuotes = true;
                csvParser.ReadLine();

                while (!csvParser.EndOfData)
                {
                    string[] fields = csvParser.ReadFields();
                    Transaction transaction = new Transaction(Convert.ToDateTime(fields[0]), fields[1],
                        fields[2], fields[3], float.Parse(fields[4]));
                    Account from = bank.GetAccount(transaction.GetFrom());
                    from.AddTransaction(transaction);
                    Account to = bank.GetAccount(transaction.GetTo());
                    to.AddTransaction(transaction);
                }
            }
            return bank;
        }
    }
}
