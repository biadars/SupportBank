using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SupportBank.FileHandling
{
    public class XMLReader : FileReader
    {
        public static List<Transaction> ReadXML(string filepath)
        {
            string folder = GetHomeFolder();
            string path = folder + filepath;
            TerminateOnFileNotFound(path);
            List<Transaction> transactions = ParseXML(path);
            return transactions;
        }

        private static List<Transaction> ParseXML(string path)
        {
            List<Transaction> transactions = new List<Transaction>();
            XDocument doc = XDocument.Load(path);
            var xmltransactions = doc.Descendants("SupportTransaction").ToList();
            for (int i = 0; i < xmltransactions.Count; i++)
            {
                Transaction transaction = new Transaction(
                    DateTime.FromOADate(double.Parse(xmltransactions[i].Attribute("Date").Value)),
                    xmltransactions[i].Element("Parties").Element("From").Value,
                    xmltransactions[i].Element("Parties").Element("To").Value,
                    xmltransactions[i].Element("Description").Value,
                    decimal.Parse(xmltransactions[i].Element("Value").Value));
                transactions.Add(transaction);
            }
            return transactions;
        }
    }
}
