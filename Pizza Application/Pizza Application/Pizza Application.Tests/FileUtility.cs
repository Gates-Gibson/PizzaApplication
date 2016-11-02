using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_Application.Tests
{
    public class FileUtility
    {
        public static string GetDatabaseFilePath()
        {
            string filePath = new DirectoryInfo(Environment.CurrentDirectory)
                .Parent
                .Parent
                .Parent
                .GetDirectories().First(d => d.Name == "Pizza Application")
                .GetDirectories().First(d => d.Name == "App_Data").FullName + "\\PizzaOrderDatabase.mdf";

            return filePath.Replace("/", "\\");
        }
    }
}
