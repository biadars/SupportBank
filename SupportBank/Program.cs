using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using Microsoft.VisualBasic.FileIO;

namespace SupportBank
{
    class Program
    {
        static void Main(string[] args)
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
                    if (!bank.Exists(fields[1]))
                        bank.AddAccount(fields[1]);
                    if (!bank.Exists(fields[2]))
                        bank.AddAccount(fields[2]);
                }
            }
            Console.ReadLine();
        }
    }
}
