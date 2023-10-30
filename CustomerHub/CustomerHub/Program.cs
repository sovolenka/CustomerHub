using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.SQLite;

namespace CustomerHub
{
    internal partial class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Data Source=CustomerHub";


            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                PrintUsers(connectionString);
                PrintClients(connectionString);
                PrintProducts(connectionString);
                PrintCharacteristics(connectionString);
                PrintReminders(connectionString);
            }
        }
    }

}