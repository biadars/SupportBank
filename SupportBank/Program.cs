using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportBank
{
    class Program
    {
        static void Main(string[] args)
        {
            Bank bank = CSVReader.ReadCSV();
            UserInterface(bank);
        }


        static void UserInterface(Bank bank)
        {
            string input;
            while(true)
            {
                Console.WriteLine("Type command (list all / list <name> / exit)");
                input = Console.ReadLine();
                if (input == "exit")
                    return;
                if (input == "list all")
                    bank.ListAccounts();
                else if (input.Length > 4 && input.Substring(0, 4) == "list")
                {
                    string name = input.Substring(5);
                    if (bank.Exists(name))
                        bank.GetAccount(name).PrintTransactions();
                    else
                        Console.WriteLine("No account with name " + name);
                }
                else
                    Console.WriteLine("Unkown command: " + input);
            }
        }
    }
}
