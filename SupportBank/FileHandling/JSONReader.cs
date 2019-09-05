using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SupportBank.InternalStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportBank.FileHandling
{
    public class JSONReader : FileReader
    {
        public static List<Transaction> ReadJSON(string filepath)
        {
            string folder = GetHomeFolder();
            string path = folder + filepath;
            TerminateOnFileNotFound(path);
            List<Transaction> transactions = ParseJSON(path);
            return transactions;
        }

        private static List<Transaction> ParseJSON(string path)
        {
            List<Transaction> transactions = new List<Transaction>();
            bool valid = true;
            List<JSONTransaction> jsontransactions = JsonConvert.DeserializeObject<List<JSONTransaction>>(System.IO.File.ReadAllText(path));

            for (int i = 0; i < jsontransactions.Count; i++)
            {
                if (!ValidateData(jsontransactions[i].Date.ToString(), jsontransactions[i].Amount.ToString(), i))
                    valid = false;
                Transaction transaction = new Transaction(
                    jsontransactions[i].Date,
                    jsontransactions[i].From,
                    jsontransactions[i].To,
                    jsontransactions[i].Narrative,
                    jsontransactions[i].Amount);
                transactions.Add(transaction);
            }
            if (!valid)
            {
                Console.WriteLine("Invalid data supplied. Exiting program.");
                Console.ReadLine();
                Environment.Exit(0);
            }
            return transactions;
        }
    }


}
