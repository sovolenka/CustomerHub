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
            string connectionString = "Data Source=D:\\лну\\5 семестр\\СustomerHub_database\\CustomerHub";

            //for (int i = 1; i <= 50; i++)
            //{
            //    string email = $"user{i}@example.com";
            //    string password = $"password{i}";

            //    AddUser(connectionString, i, email, password);
            //}

            //for (int i = 1; i <= 50; i++)
            //{
            //    string productType = $"ProductType{i}";
            //    string category = $"Category{i}";
            //    string description = $"Description{i}";
            //    string manufacturer = $"Manufacturer{i}";
            //    string country = $"Country{i}";
            //    string manufactureDate = "2023-10-10";
            //    string characteristicsStatus = "new";
            //    int productsId = i;

            //    AddCharacteristics(connectionString, i, productType, category, description, manufacturer, country, manufactureDate, characteristicsStatus, productsId);
            //}

            //for (int i = 1; i <= 50; i++)
            //{
            //    string name = $"Product Name{i}";
            //    double price = 19.99 + i;
            //    int clientsId = i;
            //    int usersId = i;
            //    int characteristicsId = i;

            //    AddProduct(connectionString, i, name, price, clientsId, usersId, characteristicsId);
            //}

            //for (int i = 1; i <= 50; i++)
            //{
            //    string firstName = $"First Name{i}";
            //    string secondName = $"Second Name{i}";
            //    string thirdName = $"Third Name{i}";
            //    string phoneNumber = $"+123456789{i}";
            //    string email = $"client{i}@example.com";
            //    string address = $"Address{i}";
            //    string factory = $"Factory{i}";
            //    string dateAdded = "2023-10-10";
            //    string clientStatus = "active";
            //    int usersId = i;
            //    int productsId = i;
            //    int remindersId = i;

            //    AddClient(connectionString, i, firstName, secondName, thirdName, phoneNumber, email, address, factory, dateAdded, clientStatus, usersId, productsId, remindersId);
            //}

            //for (int i = 1; i <= 50; i++)
            //{
            //    string note = $"Note{i}";
            //    string reminder = "2023-10-10 10:00:00";
            //    int clientsId = i;
            //    int usersId = i;
            //    AddReminder(connectionString, i, note, reminder, clientsId, usersId);
            //}

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