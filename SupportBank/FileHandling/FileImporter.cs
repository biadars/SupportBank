using SupportBank.InternalStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportBank.FileHandling
{
    class FileImporter
    {
        public static List<Transaction> ImportFile(string filepath)
        {
            string extension;
            try
            {
                extension = filepath.Split('.')[1];
            }
            catch (Exception)
            {
                throw new Exception("File must include extension.");
            }
            switch (extension)
            {
                case "csv":
                    return FileHandling.CSVReader.ReadCSV(filepath);
                case "json":
                    return FileHandling.JSONReader.ReadJSON(filepath);
                default:
                    throw new Exception("File extension \"" + extension + "\"not recognised.");
            }
        }
    }
}
