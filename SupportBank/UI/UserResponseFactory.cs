using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportBank.UI
{
    public class UserResponseFactory
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
        public static UserResponse ProcessUserInput()
        {
            Console.WriteLine("Type command (list all / list <name> / import <path> / exit)");
            string input = Console.ReadLine();
            if (input == "exit")
            {
                return new UserResponse("exit");
            }
            if (input.Length > 4 && input.Substring(0, 4) == "list")
            {
                logger.Info("Listing all accounts.");
                return new UserResponse("list", input.Substring(5));
            }
            if (input.Length > 6 && input.Substring(0, 6) == "import")
                return new UserResponse("import", input.Substring(7));
            logger.Info("Invalid user command: " + input);
            Console.WriteLine("Unkown command: " + input);
            return new UserResponse("invalid");
        }
    }
}
