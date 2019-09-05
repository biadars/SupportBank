using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportBank.FileHandling
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

        public static void TerminateOnFileNotFound(string path)
        {
            if (!File.Exists(path))
            {
                logger.Error("File " + path + " was not found.");
                Console.WriteLine("File " + path + " was not found.");
                Console.ReadLine();
                Environment.Exit(0);
            }
        }

        public static bool ValidateData(string date, string amount, long line)
        {
            bool valid = true;
            try
            {
                Convert.ToDateTime(date);
            }
            catch (FormatException exception)
            {
                logger.Error(exception.Message);
                Console.WriteLine("Error: Could not parse line " + line + ". " + date + " is not a validly formated date.");
                valid = false;
            }
            try
            {
                decimal.Parse(amount);
            }
            catch (FormatException exception)
            {
                logger.Error(exception.Message);
                Console.WriteLine("Error: Could not parse line " + line + ". " + amount + " is not a floating point number.");
                valid = false;
            }
            return valid;
        }
    }
}
