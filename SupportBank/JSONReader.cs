using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportBank
{
    public class JSONReader : FileReader
    {
        public static Bank ReadJSON(string filepath)
        {
            Bank bank = new Bank();
            Account account;
            string folder = GetHomeFolder();
            string path = folder + filepath;
            List<Transaction> transactions = JsonConvert.DeserializeObject<List<Transaction>>(System.IO.File.ReadAllText(path));
            var mytransactions = JsonConvert.DeserializeObject<List<JObject>>(System.IO.File.ReadAllText(path));
            var amount = mytransactions[0].GetValue("Amount").ToObject<decimal>();

            for (int i = 0; i < transactions.Count; i++)
            {
                account = bank.GetOrCreateAccount(transactions[i].From);
                account.AddTransaction(transactions[i]);
                account = bank.GetOrCreateAccount(transactions[i].To);
                account.AddTransaction(transactions[i]);
            }
            return bank;
        }
    }


}
