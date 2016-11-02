using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;

namespace Pizza_Application
{
    public class DatabaseConnectionUtility
    {
        /// <summary>
        /// Creates the connection string so that the db is useable on any system.
        /// </summary>
        /// <returns></returns>
        public static string GetDatabaseFilePath()
        {
            string path = HostingEnvironment.MapPath("~/app_data/PizzaOrderDatabase.mdf");
            string connectionString = string.Format(@"Data Source=(LocalDB)\MSSQLLocalDB;
                          AttachDbFilename={0};
                          Integrated Security=True;
                          Connect Timeout=30", path.Replace("/","\\"));

            return connectionString;
        }
    }
}
