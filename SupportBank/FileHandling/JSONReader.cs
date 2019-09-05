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
        public static Bank ReadJSON(string filepath)
        {
            Bank bank = new Bank();
            Account account;
            Transaction transaction;
            bool valid = true;
            string folder = GetHomeFolder();
            string path = folder + filepath;
            TerminateOnFileNotFound(path);
            List<JSONTransaction> jsontransactions = JsonConvert.DeserializeObject<List<JSONTransaction>>(System.IO.File.ReadAllText(path));

            for (int i = 0; i < jsontransactions.Count; i++)
            {
                if (!ValidateData(jsontransactions[i].Date.ToString(), jsontransactions[i].Amount.ToString(), i))
                    valid = false;
                transaction = new Transaction(
                    jsontransactions[i].Date,
                    jsontransactions[i].From,
                    jsontransactions[i].To,
                    jsontransactions[i].Narrative,
                    jsontransactions[i].Amount);
                account = bank.GetOrCreateAccount(transaction.From);
                account.AddTransaction(transaction);
                account = bank.GetOrCreateAccount(transaction.To);
                account.AddTransaction(transaction);
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
