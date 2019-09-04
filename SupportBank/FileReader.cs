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
    public abstract class FileReader
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
        public static string GetHomeFolder()
        {
            logger.Debug("Calculating home folder");
            string folder = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
            folder = Directory.GetParent(folder).FullName;
            folder = Directory.GetParent(folder).FullName;
            folder = Directory.GetParent(folder).FullName;
            logger.Debug("Found home folder: " + folder);
            return folder;
        }

        public static bool ValidateData(string date, string amount, long line)
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
